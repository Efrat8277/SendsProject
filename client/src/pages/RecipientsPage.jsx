import { recipientApi } from "../services/api"
import useApi from "../hooks/useApi"
import { useEffect, useState } from "react"
import "../styles/RecipientsPage.css"

export default function RecipientsPage() {
    const { data: recipients, loading, error, execute: fetchAll } = useApi(recipientApi.getAll)
    const { loading: creating, execute: executeCreate } = useApi(recipientApi.create)
    
    // 1. הוספת Hook לעדכון
    const { loading: updating, execute: executeUpdate } = useApi(recipientApi.update)

    // 2. סטייט חדש לשמירת ה-ID של הנמען שאנחנו עורכים כרגע
    const [editingId, setEditingId] = useState(null)

    const [form, setForm] = useState({
        name: "",
        identity: "",
        phone: "",
        address: "",
        password: "",
        userName: ""
    })

    useEffect(() => {
        fetchAll()
    }, [])

    function handleChange(e) {
        const { name, value } = e.target
        setForm((prev) => ({ ...prev, [name]: value }))
    }

    // 3. פונקציה שמעבירה את נתוני השורה לטופס למעלה
    function handleEditClick(recipient) {
        setEditingId(recipient.recipientId)
        setForm({
            name: recipient.name,
            identity: recipient.identity,
            phone: recipient.phone,
            address: recipient.address,
            password: "", // משאירים ריק בעריכה אלא אם רוצים לשנות
            userName: recipient.userName || ""
        })
        window.scrollTo({ top: 0, behavior: 'smooth' })
    }

    // 4. פונקציית שליחה מאוחדת (הוספה או עריכה)
    async function handleSubmit(e) {
    e.preventDefault()
    try {
        if (editingId) {
            // שולחים RecipientDTO — בדיוק כמו מה שהשרת מצפה
            await executeUpdate(editingId, {
                recipientId: editingId,
                name: form.name,
                identity: form.identity,
                phone: form.phone,
                address: form.address,
                packageId: 0
            })
            // alert("הפרטים עודכנו בהצלחה!");
        } else {
            await executeCreate(form)
        }
        
        setEditingId(null)
        setForm({ name: "", identity: "", phone: "", address: "", password: "", userName: "" })
        fetchAll()
    } catch (err) {
        console.error("Operation failed", err)
    }
}

    return (
        <div className="page">
            <h1 className="page-titel">ניהול נמענים</h1>

            {/* ── טופס (משתנה לפי המצב) ── */}
            <form className="form-card" onSubmit={handleSubmit}>
                <h2 className="form-titel">
                    {editingId ? `עריכת נמען: ${form.name}` : "הוסף נמען חדש"}
                </h2>

                <div className="form-row">
                    <input className="form-input" name="name" value={form.name} onChange={handleChange} placeholder="שם" required />
                    <input className="form-input" name="phone" value={form.phone} onChange={handleChange} placeholder="טלפון" required />
                    <input className="form-input" name="identity" value={form.identity} onChange={handleChange} placeholder="תעודת זהות" required />
                    <input className="form-input" name="address" value={form.address} onChange={handleChange} placeholder="כתובת" required />
                    <input className="form-input" name="password" type="password" value={form.password} onChange={handleChange} placeholder={editingId ? "סיסמה חדשה (אופציונלי)" : "סיסמה"} required={!editingId} />
                </div>

                <div style={{ display: 'flex', gap: '10px' }}>
                    <button className="btn btn-primary" type="submit" disabled={creating || updating}>
                        {editingId ? (updating ? "מעדכן..." : "שמור שינויים") : (creating ? "מוסיף..." : "הוסף נמען")}
                    </button>
                    
                    {editingId && (
                        <button className="btn" type="button" onClick={() => {
                            setEditingId(null)
                            setForm({ name: "", identity: "", phone: "", address: "", password: "", userName: "" })
                        }}>ביטול</button>
                    )}
                </div>
            </form>

            {loading && <p className="status-msg">...טוען</p>}
            {error && <p className="status-msg error">{error}</p>}

            {/* ── טבלת נמענים ── */}
            {recipients && (
                <table className="data-table">
                    <thead>
                        <tr>
                            <th>שם</th>
                            <th>תעודת זהות</th>
                            <th>טלפון</th>
                            <th>כתובת</th>
                            <th>פעולות</th>
                        </tr>
                    </thead>
                    <tbody>
                        {recipients.map((recipient) => (
                            <tr key={recipient.recipientId}>
                                <td>{recipient.name}</td>
                                <td>{recipient.identity}</td>
                                <td>{recipient.phone}</td>
                                <td>{recipient.address}</td>
                                <td>
                                    {/* 5. כפתור עריכה במקום מחיקה */}
                                    <button
                                        className="btn btn-primary"
                                        style={{ backgroundColor: '#0b7e56' }}
                                        onClick={() => handleEditClick(recipient)}
                                    >
                                        עריכה
                                    </button>
                                </td>
                            </tr>
                        ))}
                    </tbody>
                </table>
            )}
        </div>
    )
}