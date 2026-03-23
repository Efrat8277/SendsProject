/**
 * src/services/api.jsx
 * ====================
 * שכבת ה-HTTP של האפליקציה.
 * כל הפניות לשרת ה-.NET נמצאות כאן — ורק כאן.
 *
 * למה זה חשוב?
 * אם כתובת השרת תשתנה, נשנה שורה אחת (BASE_URL).
 * ה-components לא יודעים כלום על fetch/HTTP.
 */

const BASE_URL= "https://localhost:7042/api"

// ─────────────────────────────────────────────
// פונקציית עזר פנימית — לא מיוצאת
// ─────────────────────────────────────────────

/**
 * עוטפת fetch עם headers אוטומטיים.
 * שולפת את הטוקן מה-localStorage ומוסיפה אותו לכל בקשה.
 * זורקת שגיאה אם ה-server החזיר status שגוי (4xx / 5xx).
 */

async function request(endpoint, options = {}) {
      // שלוף טוקן JWT אם קיים (נשמר בעת ה-login)
      const token = localStorage.getItem("token")

      const response = await fetch(`${BASE_URL}${endpoint}`,{
        headers: {
            "Content-Type" : "application/json",
             // אם יש טוקן — הוסף אותו. השרת מצפה לפורמט: "Bearer <token>"
             ...(token&&{Authorization: `Bearer ${token}`}),
             ...options.headers,

        },
        ...options
      })

     // תגובה ריקה (למשל DELETE שהחזיר 200 בלי גוף)
      if(response.status===204)return null

      //שגיאה מהשרת
      if(!response.ok){
        const errorText = await response.text()
        throw new Error(errorText||`שגיאה${response.status}`)
      }

      return response.json();

}

// ─────────────────────────────────────────────
// AUTH — מתאים ל: AuthController.cs
// POST /api/auth  → מקבל {UserName, Password}, מחזיר {Token}
// ─────────────────────────────────────────────

export const authApi = {
  /**
   * כניסה למערכת.
   * @param {string} userName
   * @param {string} password
   * @returns {Promise<{Token: string}>}
   */
  login: (userName, password) =>
    request("/auth", {
      method: "POST",
      body: JSON.stringify({ UserName: userName, Password: password }),
    }),
};

// ─────────────────────────────────────────────
// DELIVERY PERSONS — מתאים ל: DeliveryPersonController.cs
// ─────────────────────────────────────────────
 
export const deliveryPersonApi = {
    /** GET /api/deliveryperson — רשימת כל השליחים עם החבילות שלהם */
    getAll: ()=> request("/deliveryperson"),
    /** GET /api/deliveryperson/:id — שליח לפי ID */
    getById: (id)=>request(`/deliveryperson/${id}`),

    /**
   * POST /api/deliveryperson — הוספת שליח חדש
   * הגוף מתאים ל-DeliveryPersonPostModel בשרת
   */

    create: (deliveryPerson) =>
        request("/deliveryperson",{
            method: "POST",
            body: JSON.stringify(deliveryPerson),
        }),

    /** PUT /api/deliveryperson/:id — עדכון שליח קיים */

    update: (id,deliveryPerson) =>
        request(`/deliveryperson/${id}`,{
            method:"PUT",
            body: JSON.stringify(deliveryPerson),
        }),

  /** DELETE /api/deliveryperson/:id — מחיקת שליח */
    delete: (id)=>
        request(`/deliveryperson/${id}`,{
            method: "DELETE"
        }),

};

// ─────────────────────────────────────────────
// PACKAGES — מתאים ל: PackageController.cs
// ─────────────────────────────────────────────

export const packageApi = {
      /** GET /api/package — כל החבילות */
    getAll: ()=>request("/package"),
  /** GET /api/package/:id */
    getById: (id) => request(`/package/${id}`),

/**
   * POST /api/package
   * מתאים ל-PackagePostModel
   * מחזיר את ה-ID של החבילה שנוצרה
   */

    create: (pkg) =>
        request("/package",{
            method: "POST",
            body: JSON.stringify(pkg),
        }),
  /** PUT /api/package/:id */
    update: (id,pkg) =>
        request(`/package/${id}`,{
            method:"PUT",
            body: JSON.stringify(pkg),
        }),

  /** DELETE /api/package/:id */
    delete: (id) => request(`/package/${id}`, { method: "DELETE" })

}

// ─────────────────────────────────────────────
// RECIPIENTS — מתאים ל: RecipientController.cs
// ─────────────────────────────────────────────
 
export const recipientApi = {
  /** GET /api/recipient — כל הנמענים */
    getAll: ()=>request("/recipient"),
  /** GET /api/recipient/:id */
getById: (id) => request(`/recipient/${id}`),
/**
   * POST /api/recipient
   * מתאים ל-RecipientPostModel (כולל שם + סיסמה ליצירת User)
   */

    create: (recipient) =>
        request("/recipient",{
            method: "POST",
            body: JSON.stringify(recipient),
        }),

  /** PUT /api/recipient/:id */
    update: (id,recipient)=>
        request(`/recipient/${id}`,{
            method: "PUT",
            body: JSON.stringify(recipient),
        }),
  /** DELETE /api/recipient/:id */
    delete: (id)=>
        request(`/recipient${id}`,{
            method: "DELETE",
        })
}