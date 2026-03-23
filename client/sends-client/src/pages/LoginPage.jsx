/**
 * src/pages/LoginPage.jsx
 * ========================
 * דף כניסה — מתחבר ל: POST /api/auth
 *
 * מושגים חדשים בקובץ הזה:
 *   - useState    — שמירת ערכי טופס
 *   - useNavigate — מעבר דף לאחר login מוצלח
 *   - useAuth     — שמירת הטוקן ב-Context
 */
 
import { useState } from "react";
import {useNavigate} from "react-router-dom";
import { authApi } from "../services/api";
import {useAuth} from "../context/AuthContext"
import "../styles/login-page.css";


export default function LoginPage(){

    //ערכי שדות הטופס
    const [userName, setUserName] = useState("");
    const [password, setPassword] = useState("");

    //הודעת שגיאה אם ה- Login נכשל
    const [error, setError] = useState("");

    //מניעת לחיצה כפולה בזמן בקשה
    const [loading, setLoading] = useState(false)

    const {login} = useAuth()      //פונקצית Login- מה-Context
    const navigate = useNavigate() //מעבר לדף אחר

    async function  handleSubmit(e) {
        e.preventDefault();
        setError("");
        setLoading(true);

        try{
            //קריאה ל: POST /api/auth
            const data = await authApi.login(userName, password);
            console.log("תגובה מהשרת:", data)
              console.log("Token:", data.token);    // מה הטוקן בדיוק

            //data = {Token: "eyjhbGci...."}
            login(data.token);

            //מעבר לדף הראשי לאחר כניסה
            navigate("/");
        }catch (err){
            setError("שם משתמש או סיסמא שגויים")
        }finally{
            setLoading(false)
        }
    }
return (
  <div className="login-page">
    <div className="card">
      <h2 className="title">כניסה למערכת</h2>

      <form onSubmit={handleSubmit} className="form">
        <label className="label">שם משתמש</label>
        <input
          className="input"
          value={userName}
          onChange={(e) => setUserName(e.target.value)}
          placeholder="הכנס שם משתמש"
          required
        />

        <label className="label">סיסמא</label>
        <input
          className="input"
          type="password"
          value={password}
          onChange={(e) => setPassword(e.target.value)}
          placeholder="הכנס סיסמא"
          required
        />

        {error && <p className="error">{error}</p>}

        <button className="button" type="submit" disabled={loading}>
          {loading ? "מתחבר..." : "כניסה"}
        </button>
      </form>
    </div>
  </div>
);
}

