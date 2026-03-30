import { useNavigate } from "react-router-dom";
import { FiTruck, FiShield, FiClock, FiArrowLeft } from "react-icons/fi";
import "../styles/HomePage.css";

export default function HomePage() {
    const navigate = useNavigate();

    return (
        <div className="home-container">
            {/* Hero Section - החלק העליון המרשים */}
            <header className="hero-section">
                <div className="hero-content">
                    <h1 className="hero-title">SwiftDrop</h1>
                    <p className="hero-subtitle">
                        הפתרון החכם לניהול מערך משלוחים מקצה לקצה. 
                        מהירות, אמינות ושליטה מלאה בזמן אמת.
                    </p>
                    <div className="hero-buttons">
                        <button className="btn-primary-lg" onClick={() => navigate("/register")}>
                            התחילי עכשיו - זה חינם
                        </button>
                        <button className="btn-secondary-lg" onClick={() => navigate("/login")}>
                            כניסה למערכת
                        </button>
                    </div>
                </div>
                <div className="hero-image">
                    <FiTruck size={200} className="floating-truck" />
                </div>
            </header>

            {/* Features Section - למה להשתמש באתר? */}
            <section className="features-section">
                <h2 className="section-title">למה SwiftDrop?</h2>
                <div className="features-grid">
                    <div className="feature-card">
                        <div className="feature-icon"><FiClock /></div>
                        <h3>חיסכון בזמן</h3>
                        <p>אלגוריתם חכם להקצאת שליחים אוטומטית לפי ימי עבודה ועומסים.</p>
                    </div>

                    <div className="feature-card">
                        <div className="feature-icon"><FiShield /></div>
                        <h3>בקרה מלאה</h3>
                        <p>מעקב אחרי סטטוס חבילות, נמענים ושליחים בממשק ניהול אחד נוח.</p>
                    </div>

                    <div className="feature-card">
                        <div className="feature-icon"><FiTruck /></div>
                        <h3>ניהול חכם</h3>
                        <p>חישוב מחירים אוטומטי לפי משקל וניהול מאגר נמענים מסודר.</p>
                    </div>
                </div>
            </section>

            {/* Footer / Final CTA */}
            <footer className="home-footer">
                <p>מוכנה לייעל את מערך המשלוחים שלך?</p>
                <button className="btn-footer" onClick={() => navigate("/register")}>
                    צרי חשבון חדש <FiArrowLeft />
                </button>
                <div className="footer-bottom">
                    © 2026 SwiftDrop System. כל הזכויות שמורות.
                </div>
            </footer>
        </div>
    );
}