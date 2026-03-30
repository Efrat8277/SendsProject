import { BrowserRouter, Routes, Route, Navigate } from 'react-router-dom';
import { ThemeProvider, createTheme, CssBaseline, Box } from '@mui/material';
import { AuthProvider, useAuth } from './context/AuthContext';
import LoginPage from './pages/LoginPage';
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
      {isLoggedIn && <Navbar />}
      
      <Box component="main" sx={{ flexGrow: 1 }}>
        <Routes>
          <Route path="/login" element={!isLoggedIn ? <LoginPage /> : <Navigate to="/" />} />
          
          {/* כאן התיקון: נתיב הבית מציג את השליחים לפי הנאבבר שלך */}
          <Route path="/" element={<PrivateRoute><DeliveryPersonsPage /></PrivateRoute>} />
          
          <Route path="/packages" element={<PrivateRoute><PackagesPage /></PrivateRoute>} />
          <Route path="/recipients" element={<PrivateRoute><RecipientsPage /></PrivateRoute>} />
          
          {/* אם הוקלד נתיב לא קיים, חזרה לשליחים (דף הבית) */}
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