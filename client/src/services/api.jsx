/**
 * src/services/api.jsx
 */

const BASE_URL = "https://localhost:7042/api";

async function request(endpoint, options = {}) {
  const token = localStorage.getItem("token");

  const response = await fetch(`${BASE_URL}${endpoint}`, {
    headers: {
      "Content-Type": "application/json",
      ...(token && { Authorization: `Bearer ${token}` }),
      ...options.headers,
    },
    ...options,
  });

  if (!response.ok) {
    const errorData = await response.json().catch(() => ({}));
    const error = new Error(errorData.message || `שגיאה ${response.status}`);
    error.response = { data: errorData };
    throw error;
  }

  if (response.status === 204 || response.headers.get("content-length") === "0")
    return null;

  const text = await response.text();
  if (!text) return null;
  return JSON.parse(text);
}

// ─────────────────────────────────────────────
// AUTH - כאן הוספתי את ה-Register
// ─────────────────────────────────────────────

export const authApi = {
  login: (userName, password) =>
  request("/auth", { 
    method: "POST",
    body: JSON.stringify({ UserName: userName, Password: password }),
  }),

  // הרשמה - שולחת את כל הנתונים באותיות גדולות לשרת
  register: (userData) =>
    request("/auth/register", {
      method: "POST",
      body: JSON.stringify({
        UserName: userData.UserName || userData.userName, 
        Password: userData.Password || userData.password, 
        Role: userData.Role || userData.role
      }),
    }),
};

// ─────────────────────────────────────────────
// שאר ה-APIs (נשארים ללא שינוי)
// ─────────────────────────────────────────────

export const deliveryPersonApi = {
  getAll: () => request("/deliveryperson"),
  // ... שאר הפונקציות שלך
  getById: (id) => request(`/deliveryperson/${id}`),
  create: (deliveryPerson) =>
    request("/deliveryperson", {
      method: "POST",
      body: JSON.stringify(deliveryPerson),
    }),
  update: (id, deliveryPerson) =>
    request(`/deliveryperson/${id}`, {
      method: "PUT",
      body: JSON.stringify(deliveryPerson),
    }),
  delete: (id) =>
    request(`/deliveryperson/${id}`, { method: "DELETE" }),

};

export const packageApi = {
  getAll: () => request("/package"),
  // ... שאר הפונקציות שלך
  getById: (id) => request(`/package/${id}`),
  create: (pkg) =>
    request("/package", {
      method: "POST",
      body: JSON.stringify(pkg),
    }),
  update: (id, pkg) =>
    request(`/package/${id}`, {
      method: "PUT",
      body: JSON.stringify(pkg),
    }),
  delete: (id) =>
    request(`/package/${id}`, { method: "DELETE" }),

};

export const recipientApi = {
  getAll: () => request("/recipient"),
  // ... שאר הפונקציות שלך
   getById: (id) => request(`/recipient/${id}`),
  create: (recipient) =>
    request("/recipient", {
      method: "POST",
      body: JSON.stringify(recipient),
    }),
  update: (id, recipient) =>
    request(`/recipient/${id}`, {
      method: "PUT",
      body: JSON.stringify({ ...recipient, RecipientId: id }),
    }),
  delete: (id) =>
    request(`/recipient/${id}`, { method: "DELETE" }),

};