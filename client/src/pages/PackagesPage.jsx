import { useEffect, useState } from "react";
import { packageApi, deliveryPersonApi, recipientApi } from "../services/api";
import useApi from "../hooks/useApi";
import "../styles/PackagesPage.css";

export default function PackagesPage() {
    // --- נתונים מהשרת ---
    const { data: packages, loading, error, execute: fetchPackages } = useApi(packageApi.getAll);
    const { execute: executeCreate, loading: creating } = useApi(packageApi.create);
    const { execute: executeDelete, loading: deleting } = useApi(packageApi.delete);
    const { execute: executeUpdate } = useApi(packageApi.update);

    const [deliveryPersons, setDeliveryPersons] = useState([]);
    const [recipients, setRecipients] = useState([]);

    // --- ערכי טופס ---
    const [form, setForm] = useState({
        weight: "",
        senderName: "",
        sendDate: new Date().toISOString().slice(0, 10),
        isSentToRecipient: false,
        deliveryPersonId: "",
        recipientId: "",
    });

    useEffect(() => {
        fetchPackages();
        async function loadDropDowns() {
            const [persons, recs] = await Promise.all([
                deliveryPersonApi.getAll(),
                recipientApi.getAll(),
            ]);
            setDeliveryPersons(persons || []);
            setRecipients(recs || []);
        }
        loadDropDowns();
    }, []);

    // --- לוגיקת חישוב מחיר ---
    const calculatePrice = (weight) => {
        const numericWeight = parseFloat(weight);
        if (isNaN(numericWeight) || numericWeight <= 0) return 0;
        // נוסחה: 20 ש"ח בסיס + 5 ש"ח לכל קילו
        return 20 + (numericWeight * 5);
    };

    // --- פונקציית העזר לבחירת שליח רנדומלי שעובד היום ---
    const assignRandomDeliveryPerson = () => {
        if (deliveryPersons.length === 0) return "";
        const daysMapping = ["Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday"];
        const todayName = daysMapping[new Date().getDay()];

        const availableToday = deliveryPersons.filter(p => 
            p.workDays && p.workDays.includes(todayName)
        );

        if (availableToday.length === 0) {
            alert("שים לב: אין שליחים שרשומים כעובדים היום. המערכת תבחר שליח כללי.");
            const randomIdx = Math.floor(Math.random() * deliveryPersons.length);
            return deliveryPersons[randomIdx].deliveryPersonId;
        }

        const randomIdx = Math.floor(Math.random() * availableToday.length);
        return availableToday[randomIdx].deliveryPersonId;
    };

    function handleChange(e) {
        const { name, value, type, checked } = e.target;
        setForm((prev) => ({
            ...prev, [name]: type === "checkbox" ? checked : value
        }));
    }

    async function handleCreate(e) {
        e.preventDefault();
        const autoSelectedId = assignRandomDeliveryPerson();
        
        if (!autoSelectedId) {
            alert("לא ניתן להוסיף חבילה ללא שליח במערכת");
            return;
        }

        await executeCreate({
            ...form,
            weight: parseFloat(form.weight),
            deliveryPersonId: parseInt(autoSelectedId),
            recipientId: parseInt(form.recipientId),
            sendDate: new Date(form.sendDate).toISOString(),
        });

        fetchPackages();
        setForm({
            weight: "",
            senderName: "",
            sendDate: new Date().toISOString().slice(0, 10),
            isSentToRecipient: false,
            deliveryPersonId: "",
            recipientId: "",
        });
    }

    async function handleToggleStatus(pkg) {
        await executeUpdate(pkg.id, { ...pkg, isSentToRecipient: true });
        fetchPackages();
    }

    async function handleDelete(id) {
        await executeDelete(id);
        fetchPackages();
    }

    return (
        <div className="page">
            <h1 className="page-title">ניהול חבילות</h1>

            <form className="form-card" onSubmit={handleCreate}>
                <h2 className="form-titel">הוסף חבילה חדשה</h2>
                <div className="form-row">
                    <input className="form-input" name="senderName" value={form.senderName} onChange={handleChange} placeholder="שם שולח" required />
                    <input className="form-input" name="weight" type="number" step="0.1" min="0" value={form.weight} onChange={handleChange} placeholder="משקל (ק״ג)" required />
                    <input className="form-input" name="sendDate" type="date" value={form.sendDate} onChange={handleChange} required />
                </div>
                
                <div className="form-row">
                    <div className="form-input price-display" style={{ backgroundColor: "#eef9f5", border: "1px solid #1D9E75", color: "#1D9E75", fontWeight: "bold", display: "flex", alignItems: "center", padding: "0 10px" }}>
                        💰 מחיר משלוח: {calculatePrice(form.weight)} ₪
                    </div>

                    <select className="form-input" name="recipientId" value={form.recipientId} onChange={handleChange} required>
                        <option value="">בחר נמען</option>
                        {recipients.map((r) => (
                            <option key={r.recipientId} value={r.recipientId}>{r.name}</option>
                        ))}
                    </select>

                    <label className="checkbox-label">
                        <input type="checkbox" name="isSentToRecipient" checked={form.isSentToRecipient} onChange={handleChange} />
                        נשלח לנמען
                    </label>
                </div>
                
                <div className="form-info-row" style={{ fontSize: "0.85em", color: "#666", marginBottom: "15px" }}>
                    ✨ שליח יוקצה אוטומטית לפי יום עבודה נוכחי.
                </div>

                <button className="btn btn-primary" type="submit" disabled={creating}>
                    {creating ? "מוסיף..." : "הוסף חבילה ושמור"}
                </button>
            </form>

            {loading && <p className="status-msg">טוען נתונים...</p>}
            {error && <p className="status-msg error">{error}</p>}

            {packages && (
                <table className="data-table">
                    <thead>
                        <tr>
                            <th>שולח</th>
                            <th>משקל</th>
                            <th>מחיר</th>
                            <th>תאריך</th>
                            <th>שליח</th>
                            <th>נמען</th>
                            <th>סטטוס</th>
                            <th>פעולות</th>
                        </tr>
                    </thead>
                    <tbody>
                        {packages.map((pkg) => (
                            <tr key={pkg.id}>
                                <td style={{ fontWeight: "bold" }}>{pkg.senderName}</td>
                                <td>{pkg.weight} ק״ג</td>
                                <td style={{ color: "#1D9E75", fontWeight: "bold" }}>{calculatePrice(pkg.weight)} ₪</td>
                                <td>{new Date(pkg.sendDate).toLocaleDateString("he-IL")}</td>
                                <td>
                                    {deliveryPersons.find((p) => p.deliveryPersonId === pkg.deliveryPersonId)?.name || "אוטומטי"}
                                </td>
                                <td>
                                    {recipients.find((r) => r.recipientId === pkg.recipientId)?.name || "---"}
                                </td>
                                <td>
                                    <span className={`badge ${pkg.isSentToRecipient ? "badge-success" : "badge-pending"}`}>
                                        {pkg.isSentToRecipient ? "נמסר" : "בדרך"}
                                    </span>
                                </td>
                                <td>
                                    {!pkg.isSentToRecipient && (
                                        <button className="btn btn-primary" onClick={() => handleToggleStatus(pkg)} style={{ marginLeft: "8px" }}>
                                            נמסר
                                        </button>
                                    )}
                                    <button className="btn btn-danger" onClick={() => handleDelete(pkg.id)} disabled={deleting}>
                                        מחק
                                    </button>
                                </td>
                            </tr>
                        ))}
                    </tbody>
                </table>
            )}
        </div>
    );
}