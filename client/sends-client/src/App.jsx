/**
 * src/App.jsx
 * ============
 * נקודת הכניסה של האפליקציה.
 * מגדיר:
 *   1. את ה-AuthProvider שעוטף הכל
 *   2. את ה-Router — מפת הדפים
 *   3. הגנה על דפים שדורשים התחברות
 */
 
import { BrowserRouter, Routes, Route, Navigate } from "react-router-dom";
import { AuthProvider, useAuth } from "./context/AuthContext";
import LoginPage from "./pages/LoginPage";
import DeliveryPersonsPage from "./pages/DeliveryPersonsPage";
import PackagesPage from "./pages/PackagesPage";
import RecipientsPage from "./pages/RecipientsPage";
import Navbar from "./components/Navbar";
 
// ─────────────────────────────────────────────
// ProtectedRoute — שומר על דפים שדורשים login
//
// איך זה עובד:
//   אם המשתמש מחובר → מציג את הדף המבוקש (children)
//   אם לא מחובר    → מפנה אוטומטית לדף ה-Login
// ─────────────────────────────────────────────
function ProtectedRoute({ children }) {
  const { isLoggedIn } = useAuth();
  return isLoggedIn ? children : <Navigate to="/login" replace />;
}
 
// ─────────────────────────────────────────────
// AppRoutes — מופרד מ-App כדי שיוכל להשתמש ב-useAuth
// (hooks חייבים להיות בתוך Provider)
// ─────────────────────────────────────────────
function AppRoutes() {
  return (
    <>
      <Navbar />
      <Routes>
        {/* דף כניסה — פתוח לכולם */}
        <Route path="/login" element={<LoginPage />} />
 
        {/* דפים מוגנים — דורשים התחברות */}
        <Route
          path="/"
          element={
            <ProtectedRoute>
              <DeliveryPersonsPage />
            </ProtectedRoute>
          }
        />
        <Route
          path="/packages"
          element={
            <ProtectedRoute>
              <PackagesPage />
            </ProtectedRoute>
          }
        />
        <Route
          path="/recipients"
          element={
            <ProtectedRoute>
              <RecipientsPage />
            </ProtectedRoute>
          }
        />
 
        {/* כל URL לא מוכר → דף הבית */}
        <Route path="*" element={<Navigate to="/" replace />} />
      </Routes>
    </>
  );
}
 
export default function App() {
  return (
    // AuthProvider חייב לעטוף את BrowserRouter
    // כדי שה-useAuth יעבוד גם בתוך Routes
    <AuthProvider>
      <BrowserRouter>
        <AppRoutes />
      </BrowserRouter>
    </AuthProvider>
  );
}