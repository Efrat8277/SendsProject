/**
 * src/pages/LoginPage.jsx
 * ========================
 * דף כניסה מעוצב ומודרני - SwiftDrop
 */

import { useState } from "react";
import { useNavigate } from "react-router-dom";
import { authApi } from "../services/api";
import { useAuth } from "../context/AuthContext";

// ייבוא אייקונים מ-react-icons
import { FiUser, FiLock, FiLogIn, FiAlertCircle, FiTruck } from "react-icons/fi";

import "../styles/login-page.css";

export default function LoginPage() {
  // --- לוגיקה מקורית (לא שונתה) ---
  const [userName, setUserName] = useState("");
  const [password, setPassword] = useState("");
  const [error, setError] = useState("");
  const [loading, setLoading] = useState(false);
  const { login } = useAuth();
  const navigate = useNavigate();

  async function handleSubmit(e) {
    e.preventDefault();
    setError("");
    setLoading(true);
    try {
      const data = await authApi.login(userName, password);
      console.log("תגובה מהשרת:", data);
      console.log("Token:", data.token);
      login(data.token);
      navigate("/");
    } catch (err) {
      setError("שם משתמש או סיסמה שגויים");
    } finally {
      setLoading(false);
    }
  }
  // --- סוף לוגיקה מקורית ---

  return (
    <div className="login-page">
      <div className="login-card">
        {/* כותרת ולוגו מעוצבים */}
        <div className="login-header">
          <div className="login-logo-icon">
            <FiTruck />
          </div>
          <h2 className="login-title">SwiftDrop</h2>
          <p className="login-subtitle">ניהול משלוחים חכם ומהיר</p>
        </div>

        {/* טופס כניסה */}
        <form onSubmit={handleSubmit} className="login-form">
          
          {/* שדה שם משתמש */}
          <div className="input-container">
            <label className="input-label">שם משתמש</label>
            <div className="input-wrapper">
              <input
                className="login-input"
                value={userName}
                onChange={(e) => setUserName(e.target.value)}
                placeholder="הכניסי את שם המשתמש שלך"
                required
                autoComplete="username"
              />
              <FiUser className="input-icon" />
            </div>
          </div>

          {/* שדה סיסמה */}
          <div className="input-container">
            <label className="input-label">סיסמה</label>
            <div className="input-wrapper">
              <input
                className="login-input"
                type="password"
                value={password}
                onChange={(e) => setPassword(e.target.value)}
                placeholder="הכניסי את הסיסמה שלך"
                required
                autoComplete="current-password"
              />
              <FiLock className="input-icon" />
            </div>
          </div>

          {/* הצגת שגיאה מעוצבת עם אנימציה */}
          {error && (
            <div className="login-error-msg">
              <FiAlertCircle size={18} />
              <span>{error}</span>
            </div>
          )}

          {/* כפתור כניסה עם מצב טעינה */}
          <button className="login-button" type="submit" disabled={loading}>
            {loading ? (
              <>
                <CircularProgress size={20} />
                <span>מתחבר...</span>
              </>
            ) : (
              <>
                <FiLogIn size={18} />
                <span>כניסה למערכת</span>
              </>
            )}
          </button>
        </form>
      </div>
    </div>
  );
}

// קומפוננטת טעינה קטנה עבור הכפתור (Inline למניעת ייבוא נוסף)
function CircularProgress({ size }) {
  return (
    <svg
      width={size}
      height={size}
      viewBox="0 0 50 50"
      style={{ animation: 'rotate 1s linear infinite' }}
    >
      <circle
        cx="25"
        cy="25"
        r="20"
        fill="none"
        stroke="currentColor"
        strokeWidth="5"
        strokeDasharray="90, 150"
        strokeLinecap="round"
      />
      <style>{`
        @keyframes rotate { 100% { transform: rotate(360deg); } }
      `}</style>
    </svg>
  );
}