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
import { createContext, useState, useContext, useEffect } from "react";
import { jwtDecode } from "jwt-decode"; // יבוא הספרייה שהתקנו

const AuthContext = createContext();

export const AuthProvider = ({ children }) => {
  const [token, setToken] = useState(localStorage.getItem("token"));
  const [user, setUser] = useState(null);

  // פונקציה שמחלצת את נתוני המשתמש מהטוקן
 const getUserFromToken = (t) => {
  try {
    const decoded = jwtDecode(t);
    console.log("פענוח מלא של הטוקן:", decoded);

    // אסטרטגיה חסינה: מחפשים מפתח שמכיל את המילה 'name' בסופו
    const nameKey = Object.keys(decoded).find(key => key.endsWith('/name')) || "unique_name";
    const roleKey = Object.keys(decoded).find(key => key.endsWith('/role')) || "role";

    const name = decoded[nameKey];
    const role = decoded[roleKey];

    console.log("השם שנמצא במפתח", nameKey, ":", name);

    return { 
      name: name || "משתמש", 
      role: role 
    };
  } catch (error) {
    console.error("שגיאה בפענוח:", error);
    return null;
  }
};

  useEffect(() => {
    if (token) {
      localStorage.setItem("token", token);
      setUser(getUserFromToken(token));
    } else {
      localStorage.removeItem("token");
      setUser(null);
    }
  }, [token]);

  const login = (newToken) => {
    setToken(newToken);
  };

  const logout = () => {
    setToken(null);
    localStorage.removeItem("token");
  };

  return (
    <AuthContext.Provider value={{ 
        token, 
        user, 
        login, 
        logout, 
        isLoggedIn: !!token 
    }}>
      {children}
    </AuthContext.Provider>
  );
};

export const useAuth = () => useContext(AuthContext);