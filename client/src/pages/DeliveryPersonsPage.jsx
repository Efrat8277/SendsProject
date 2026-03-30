//DeliveryPersonsPage.jsx

import { useEffect, useState } from "react";
import { deliveryPersonApi } from "../services/api";
import useApi from "../hooks/useApi";
import "../styles/DeliveryPersonsPage.css";

export default function DeliveryPersonsPage() {
    const [isModalOpen, setIsModalOpen] = useState(false);
    const { data: persons, execute: fetchAll } = useApi(deliveryPersonApi.getAll);
    const { loading: creating, execute: executeCreate } = useApi(deliveryPersonApi.create);
    const { execute: executeDelete } = useApi(deliveryPersonApi.delete);

    const [form, setForm] = useState({
        name: "", userName: "", password: "", phone: "",
        startTime: "08:00:00", endTime: "17:00:00", workDays: 0
    });

    useEffect(() => { fetchAll(); }, []);

    const handleCreate = async (e) => {
        e.preventDefault();
        await executeCreate(form);
        setIsModalOpen(false);
        setForm({ name: "", userName: "", password: "", phone: "", startTime: "08:00:00", endTime: "17:00:00", workDays: 0 });
        fetchAll();
    };

    const handleDelete = async (id) => {
        if (window.confirm("האם את בטוחה שברצונך למחוק שליח זה?")) {
            await executeDelete(id);
            fetchAll();
        }
    };

    return (
        <div className="dashboard-container">
            {/* איורים דקורטיביים */}
            <div className="decoration-icon" style={{top: '5%', left: '5%', fontSize: '80px'}}>📦</div>
            <div className="decoration-icon" style={{bottom: '5%', right: '5%', fontSize: '80px'}}>🚚</div>

            <div className="main-card">
                <div className="card-header">
                    <button className="btn-add-main" onClick={() => setIsModalOpen(true)}>
                        + הוספת שליח חדש
                    </button>
                    <h1>השליחים שלנו</h1>
                </div>

                <div className="table-responsive">
                    <table className="modern-table">
                        <thead>
                            <tr>
                                <th>שם</th>
                                <th>טלפון</th>
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
                                    <td>{person.startTime?.slice(0,5)}</td>
                                    <td>{person.endTime?.slice(0,5)}</td>
                                    <td><span className="pkg-badge">{person.packages?.length || 0} חבילות</span></td>
                                    <td>
                                        <button className="btn-delete-action" onClick={() => handleDelete(person.deliveryPersonId)}>
                                            הסרת שליח
                                        </button>
                                    </td>
                                </tr>
                            ))}
                        </tbody>
                    </table>
                </div>
            </div>

            {/* חלון קופץ (Modal) להוספת שליח */}
            {isModalOpen && (
                <div className="modal-overlay" onClick={() => setIsModalOpen(false)}>
                    <div className="modal-content" onClick={e => e.stopPropagation()}>
                        <button className="close-btn" onClick={() => setIsModalOpen(false)}>×</button>
                        <h2 className="modal-title">הוספת שליח חדש</h2>
                        <p className="modal-subtitle">מלאי את פרטי השליח ליצירת חשבון חדש במערכת</p>
                        
                        <form onSubmit={handleCreate}>
                            <div className="input-grid">
                                <div className="input-group">
                                    <label>שם מלא</label>
                                    <input value={form.name} onChange={e => setForm({...form, name: e.target.value})} required placeholder="ישראל ישראלי" />
                                </div>
                                <div className="input-group">
                                    <label>שם משתמש</label>
                                    <input value={form.userName} onChange={e => setForm({...form, userName: e.target.value})} required placeholder="israel123" />
                                </div>
                                <div className="input-group">
                                    <label>סיסמה</label>
                                    <input type="password" value={form.password} onChange={e => setForm({...form, password: e.target.value})} required placeholder="••••••••" />
                                </div>
                                <div className="input-group">
                                    <label>טלפון</label>
                                    <input value={form.phone} onChange={e => setForm({...form, phone: e.target.value})} placeholder="050-0000000" />
                                </div>
                            </div>
                            <button type="submit" className="btn-submit" disabled={creating}>
                                {creating ? "שומר..." : "צור שליח ושמור"}
                            </button>
                        </form>
                    </div>
                </div>
            )}
        </div>
    );
}
