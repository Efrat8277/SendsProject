import { useEffect, useState } from "react";
import { deliveryPersonApi } from "../services/api";
import useApi from "../hooks/useApi";
import "../styles/DeliveryPersonsPage.css";

const DAYS_OF_WEEK = [
    { id: 1, name: "א'" }, { id: 2, name: "ב'" }, { id: 4, name: "ג'" },
    { id: 8, name: "ד'" }, { id: 16, name: "ה'" }, { id: 32, name: "ו'" },
];

export default function DeliveryPersonsPage() {
    const [isModalOpen, setIsModalOpen] = useState(false);
    const { data: persons, execute: fetchAll } = useApi(deliveryPersonApi.getAll);
    const { loading: creating, execute: executeCreate } = useApi(deliveryPersonApi.create);
    const { execute: executeDelete } = useApi(deliveryPersonApi.delete);

    const [form, setForm] = useState({
        name: "", userName: "", password: "", phone: "",
        startTime: "08:00", endTime: "17:00", workDays: 0
    });

    useEffect(() => { fetchAll(); }, []);

    const handleDayChange = (dayValue) => {
        const currentDays = form.workDays;
        const newDays = (currentDays & dayValue) ? currentDays ^ dayValue : currentDays | dayValue;
        setForm({ ...form, workDays: newDays });
    };

    // פונקציית רינדור חכמה שתומכת גם ב-Enum טקסטואלי וגם במספרי
    const renderDays = (person) => {
        const val = person.workDays ?? person.WorkDays;
        
        if (val === undefined || val === null || val === 0) return "---";

        // אם השרת שלח מחרוזת (כמו "Sunday, Monday")
        if (typeof val === 'string') {
            return val.replace(/Sunday/g, "א'")
                      .replace(/Monday/g, "ב'")
                      .replace(/Tuesday/g, "ג'")
                      .replace(/Wednesday/g, "ד'")
                      .replace(/Thursday/g, "ה'")
                      .replace(/Friday/g, "ו'");
        }

        // אם השרת שלח מספר (Bitwise)
        const selected = DAYS_OF_WEEK
            .filter(day => (val & day.id))
            .map(day => day.name);

        return selected.length > 0 ? selected.join(", ") : "---";
    };

    const handleCreate = async (e) => {
        e.preventDefault();
        const formattedForm = {
            ...form,
            startTime: form.startTime.length === 5 ? `${form.startTime}:00` : form.startTime,
            endTime: form.endTime.length === 5 ? `${form.endTime}:00` : form.endTime,
            WorkDays: form.workDays // שליחה כמספר לשרת
        };

        try {
            await executeCreate(formattedForm);
            setIsModalOpen(false);
            setForm({ name: "", userName: "", password: "", phone: "", startTime: "08:00", endTime: "17:00", workDays: 0 });
            fetchAll();
        } catch (err) {
            console.error("Save error:", err);
        }
    };

    const handleDelete = async (id) => {
        if (window.confirm("האם למחוק שליח זה?")) {
            await executeDelete(id);
            fetchAll();
        }
    };

    return (
        <div className="dashboard-container">
            <div className="main-card">
                <div className="card-header">
                    <button className="btn-add-main" onClick={() => setIsModalOpen(true)}>+ הוספת שליח חדש</button>
                    <h1>השליחים שלנו</h1>
                </div>
                <div className="table-responsive">
                    <table className="modern-table">
                        <thead>
                            <tr>
                                <th>שם</th>
                                <th>טלפון</th>
                                <th>ימי עבודה</th>
                                <th>שעת התחלה</th>
                                <th>שעת סיום</th>
                                <th>חבילות</th>
                                <th>פעולות</th>
                            </tr>
                        </thead>
                        <tbody>
                            {persons?.map(person => (
                                <tr key={person.deliveryPersonId}>
                                    <td>{person.name}</td>
                                    <td>{person.phone || "---"}</td>
                                    <td className="days-cell" style={{ fontWeight: '500', color: '#1D9E75' }}>
                                        {renderDays(person)}
                                    </td>
                                    <td>{person.startTime?.slice(0,5)}</td>
                                    <td>{person.endTime?.slice(0,5)}</td>
                                    <td><span className="pkg-badge">{person.packages?.length || 0} חבילות</span></td>
                                    <td><button className="btn-delete-action" onClick={() => handleDelete(person.deliveryPersonId)}>הסרת שליח</button></td>
                                </tr>
                            ))}
                        </tbody>
                    </table>
                </div>
            </div>

            {isModalOpen && (
                <div className="modal-overlay" onClick={() => setIsModalOpen(false)}>
                    <div className="modal-content" onClick={e => e.stopPropagation()}>
                        <button className="close-btn" onClick={() => setIsModalOpen(false)}>×</button>
                        <h2 className="modal-title">הוספת שליח חדש</h2>
                        <form onSubmit={handleCreate}>
                            <div className="input-grid">
                                <div className="input-group"><label>שם מלא</label><input value={form.name} onChange={e => setForm({...form, name: e.target.value})} required /></div>
                                <div className="input-group"><label>טלפון</label><input value={form.phone} onChange={e => setForm({...form, phone: e.target.value})} /></div>
                                <div className="input-group"><label>שם משתמש</label><input value={form.userName} onChange={e => setForm({...form, userName: e.target.value})} required /></div>
                                <div className="input-group"><label>סיסמה</label><input type="password" value={form.password} onChange={e => setForm({...form, password: e.target.value})} required /></div>
                                <div className="input-group"><label>שעת התחלה</label><input type="time" value={form.startTime} onChange={e => setForm({...form, startTime: e.target.value})} /></div>
                                <div className="input-group"><label>שעת סיום</label><input type="time" value={form.endTime} onChange={e => setForm({...form, endTime: e.target.value})} /></div>
                                <div className="input-group" style={{ gridColumn: "span 2" }}>
                                    <label>ימי עבודה</label>
                                    <div className="days-selector-final">
                                        {DAYS_OF_WEEK.map(day => (
                                            <div 
                                                key={day.id} 
                                                className={`day-option ${form.workDays & day.id ? 'active' : ''}`} 
                                                onClick={() => handleDayChange(day.id)}
                                            >
                                                {day.name}
                                            </div>
                                        ))}
                                    </div>
                                </div>
                            </div>
                            <button type="submit" className="btn-submit" disabled={creating}>{creating ? "שומר..." : "צור שליח ושמור"}</button>
                        </form>
                    </div>
                </div>
            )}
        </div>
    );
}