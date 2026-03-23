/**
 * src/context/AuthContext.jsx
 * ============================
 * Context API — מאפשר לכל דף באפליקציה לדעת:
 *   - האם המשתמש מחובר?
 *   - מה התפקיד שלו? (recipient / delivary_person)
 *   - איך מתנתקים?
 *
 * איך זה עובד?
 * React Context הוא כמו "משתנה גלובלי" מסודר.
 * במקום להעביר props מדף לדף, עוטפים את כל האפליקציה
 * ב-<AuthProvider> ואז כל component יכול לקרוא את המידע
 * עם useContext(AuthContext).
 */
import { createContext,useContext,useState } from "react";
import {jwtDecode} from "jwt-decode"


// 1. יוצרים את ה"קופסה" שתחזיק את המידע
const AuthContext = createContext(null)

// ─────────────────────────────────────────────
// AuthProvider — עוטף את כל האפליקציה ב-App.jsx
// ─────────────────────────────────────────────

export function AuthProvider({children}){
    const [token, setToken] = useState(localStorage.getItem("token"));

    /**
   * נקרא מדף ה-Login אחרי שהשרת מחזיר טוקן.
   * שומר את הטוקן ב-state וגם ב-localStorage
   * (כדי שישרוד רענון דף).
   */

    function login(newToken){
        localStorage.setItem("token",newToken);
        setToken(newToken);
    }

      /** מנקה הכל ומחזיר לדף ה-Login */

      function logout(){
        localStorage.removeItem("token");
        setToken(null);
      }

       /**
   * מפענח את הטוקן כדי לשלוף פרטי משתמש.
   * הטוקן JWT מכיל בתוכו claims — בשרת שלך:
   *   ClaimTypes.Name  → שם המשתמש
   *   ClaimTypes.Role  → recipient / delivary_person
   */
  const user = token ? jwtDecode(token): null;

    // מה שיהיה זמין לכל component שיקרא useAuth()
    const value = {
    token,
    user,
    isLoggedIn: !!token,
    role: user?.role || user?.Role||null,
    login,
    logout,
};

return <AuthContext.Provider value = {value}>{children}</AuthContext.Provider>
}

// ─────────────────────────────────────────────
// useAuth — ה-hook שכל דף ישתמש בו
// שימוש: const { user, logout, isLoggedIn } = useAuth();
// ─────────────────────────────────────────────

export function useAuth(){
    return useContext(AuthContext)
}
