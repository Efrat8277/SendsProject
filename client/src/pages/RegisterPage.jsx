import React, { useState } from 'react';
import { useNavigate, Link } from 'react-router-dom';
import { FiUser, FiLock, FiUserPlus, FiShield } from 'react-icons/fi';
import { authApi } from '../services/api'; // ודאי שהקובץ api.jsx מעודכן עם register
import '../styles/RegisterPage.css';

export default function RegisterPage() {
    const [formData, setFormData] = useState({
        userName: '',
        password: '',
        role: 'delivary_person'
    });
    const [error, setError] = useState('');
    const [loading, setLoading] = useState(false);
    const navigate = useNavigate();

    const handleSubmit = async (e) => {
        e.preventDefault();
        setLoading(true);
        setError('');

        try {
            // התיקון הקריטי: מיפוי ידני לאותיות גדולות כדי שהשרת יבין אותנו
            await authApi.register({
                UserName: formData.userName,
                Password: formData.password,
                Role: formData.role
            }); 
            
            alert('החשבון נוצר בהצלחה! כעת ניתן להתחבר.');
            navigate('/login');
        } catch (err) {
            // אם השרת מחזיר שגיאה (כמו משתמש קיים), נציג אותה
            setError(err.message || 'שגיאה בתקשורת עם השרת');
        } finally {
            setLoading(false);
        }
    };

    return (
        <div className="login-page">
            <div className="login-card">
                <div className="login-header">
                    <div className="login-logo-container">
                        <FiUserPlus className="login-logo-icon" />
                    </div>
                    <h1 className="login-title">הצטרפות ל-SwiftDrop</h1>
                    <p className="login-subtitle">צרו חשבון חדש במערכת</p>
                </div>

                <form className="login-form" onSubmit={handleSubmit}>
                    <div className="input-container">
                        <label className="input-label">שם משתמש</label>
                        <div className="input-wrapper">
                            <input 
                                type="text" 
                                className="login-input" 
                                placeholder="בחרי שם משתמש"
                                required
                                value={formData.userName}
                                onChange={(e) => setFormData({...formData, userName: e.target.value})}
                            />
                            <FiUser className="input-icon" />
                        </div>
                    </div>

                    <div className="input-container">
                        <label className="input-label">סיסמה</label>
                        <div className="input-wrapper">
                            <input 
                                type="password" 
                                className="login-input" 
                                placeholder="••••••••"
                                required
                                value={formData.password}
                                onChange={(e) => setFormData({...formData, password: e.target.value})}
                            />
                            <FiLock className="input-icon" />
                        </div>
                    </div>

                    <div className="input-container">
                        <label className="input-label">סוג חשבון</label>
                        <div className="input-wrapper">
                            <select 
                                className="login-input select-input"
                                value={formData.role}
                                onChange={(e) => setFormData({...formData, role: e.target.value})}
                            >
                                <option value="delivary_person">שליח</option>
                                <option value="recipient">לקוח / נמען</option>
                            </select>
                            <FiShield className="input-icon" />
                        </div>
                    </div>

                    {error && (
                        <div className="login-error-msg" style={{ 
                            color: '#e11d48', 
                            backgroundColor: '#fff1f2', 
                            padding: '10px', 
                            borderRadius: '8px',
                            marginBottom: '15px',
                            fontSize: '14px',
                            textAlign: 'center',
                            border: '1px solid #fecaca'
                        }}>
                            {error}
                        </div>
                    )}

                    <button type="submit" className="login-button" disabled={loading}>
                        {loading ? 'מעבד...' : 'צור חשבון'}
                    </button>
                </form>

                <div className="login-link-container">
                    כבר יש לך חשבון? <Link to="/login" className="login-link">התחברי כאן</Link>
                </div>
            </div>
        </div>
    );
}