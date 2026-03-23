/**
 * src/components/Navbar.jsx
 * ==========================
 * סרגל ניווט עליון — מופיע בכל הדפים.
 * מציג קישורים ואת שם המשתמש המחובר.
 * כפתור "התנתק" קורא ל-logout() מה-Context.
 */


import { Link,useNavigate } from "react-router-dom";
import { useAuth } from "../context/AuthContext";
import "../styles/navbar.css"

export default function Navbar(){
    const {isLoggedIn,user,logout} = useAuth();
    const navigate = useNavigate();
    
    function handleLogout(){
        logout();
        navigate("/login");
    }

  // אם לא מחובר — לא מציגים navbar
  if(!isLoggedIn) return null;

  return (
  <nav className="nav">
    <div className="links">
      <Link className="link" to="/">שליחים</Link>
      <Link className="link" to="/packages">חבילות</Link>
      <Link className="link" to="/recipients">נמענים</Link>
    </div>

    <div className="userSection">
      <span className="userName">
        שלום, {user?.unique_name || user?.name || "משתמש"}
      </span>
      <button className="logoutBtn" onClick={handleLogout}>התנתק</button>
    </div>
  </nav>
);
}