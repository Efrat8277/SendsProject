import { BrowserRouter, Routes, Route, Navigate } from 'react-router-dom';
import { ThemeProvider, createTheme, CssBaseline, Box } from '@mui/material';
import { AuthProvider, useAuth } from './context/AuthContext';
import LoginPage from './pages/LoginPage';
import RegisterPage from './pages/RegisterPage'; // אל תשכחי לייבא את דף ההרשמה
import HomePage from './pages/HomePage';       // אל תשכחי לייבא את דף הבית החדש
import DeliveryPersonsPage from './pages/DeliveryPersonsPage';
import PackagesPage from './pages/PackagesPage';
import RecipientsPage from './pages/RecipientsPage';
import Navbar from './components/Navbar';

const theme = createTheme({
  direction: 'rtl',
  typography: { fontFamily: '"Heebo", sans-serif' },
  palette: {
    primary: { main: '#1D9E75' },
    background: { default: '#F4F7F6' },
  },
});

const PrivateRoute = ({ children }) => {
  const { isLoggedIn } = useAuth();
  return isLoggedIn ? children : <Navigate replace to="/login" />;
};

function AppContent() {
  const { isLoggedIn } = useAuth();

  return (
    <Box sx={{ minHeight: '100vh', display: 'flex', flexDirection: 'column' }}>
      {/* ה-Navbar יופיע רק אם המשתמש מחובר */}
      {isLoggedIn && <Navbar />}
      
      <Box component="main" sx={{ flexGrow: 1 }}>
        <Routes>
          {/* דף הבית הכללי - נגיש לכולם */}
          <Route path="/" element={<HomePage />} />
          
          {/* דפי כניסה והרשמה - רק אם לא מחוברים */}
          <Route path="/login" element={!isLoggedIn ? <LoginPage /> : <Navigate to="/dashboard" />} />
          <Route path="/register" element={!isLoggedIn ? <RegisterPage /> : <Navigate to="/dashboard" />} />
          
          {/* דפי ניהול - מוגנים ב-PrivateRoute */}
          {/* שיניתי את הנתיב של השליחים ל-dashboard כדי שלא יתנגש עם דף הבית */}
          <Route path="/dashboard" element={<PrivateRoute><DeliveryPersonsPage /></PrivateRoute>} />
          <Route path="/packages" element={<PrivateRoute><PackagesPage /></PrivateRoute>} />
          <Route path="/recipients" element={<PrivateRoute><RecipientsPage /></PrivateRoute>} />
          
          {/* הפניה לנתיב ברירת מחדל אם הכתובת לא קיימת */}
          <Route path="*" element={<Navigate to="/" />} />
        </Routes>
      </Box>
    </Box>
  );
}

export default function App() {
  return (
    <BrowserRouter>
      <ThemeProvider theme={theme}>
        <CssBaseline />
        <AuthProvider>
          <AppContent />
        </AuthProvider>
      </ThemeProvider>
    </BrowserRouter>
  );
}