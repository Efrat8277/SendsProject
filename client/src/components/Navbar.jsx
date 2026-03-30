import { Link, useLocation, useNavigate } from "react-router-dom";
import { useAuth } from "../context/AuthContext";
import { FiTruck, FiPackage, FiUsers } from "react-icons/fi";
import "../styles/navbar.css";

export default function Navbar() {
    const { isLoggedIn, user, logout } = useAuth();
    const navigate = useNavigate();
    const location = useLocation();

    if (!isLoggedIn) return null;

    const handleLogout = () => {
        logout();
        navigate("/login");
    };

    const isActive = (path) => location.pathname === path ? "active" : "";

   return (
    <>
        <div className="nav-logo">
            <svg viewBox="0 0 220 70" xmlns="http://www.w3.org/2000/svg" height="50">
                <line x1="18" y1="5" x2="18" y2="32" stroke="#001529" strokeWidth="5" strokeLinecap="round"/>
                <polyline points="8,25 18,37 28,25" fill="none" stroke="#001529" strokeWidth="5" strokeLinecap="round" strokeLinejoin="round"/>
                <circle cx="18" cy="48" r="7" fill="#1D9E75"/>
                <circle cx="18" cy="48" r="3" fill="white"/>
                <text x="55" y="32" fontFamily="Arial" fontSize="26" fontWeight="800" fill="#001529" letterSpacing="-1">Swift</text>
                <text x="55" y="54" fontFamily="Arial" fontSize="18" fontWeight="400" fill="#1D9E75" letterSpacing="3">DROP</text>
            </svg>
        </div>

        <nav className="nav" dir="rtl">
            <div className="nav-sep"></div>
            <div className="links">
                <Link className={`link ${isActive("/dashboard")}`} to="/dashboard">
                    <FiTruck color="#1D9E75" size={17} /> שליחים
                </Link>
                <Link className={`link ${isActive("/packages")}`} to="/packages">
                    <FiPackage color="#1D9E75" size={17} /> חבילות
                </Link>
                <Link className={`link ${isActive("/recipients")}`} to="/recipients">
                    <FiUsers color="#1D9E75" size={17} /> נמענים
                </Link>
            </div>
            <div className="userSection">
                <span className="userName">שלום, {user?.name || "אורח"}</span>
                <button className="logoutBtn" onClick={handleLogout}>התנתק</button>
            </div>
        </nav>
    </>
);
}