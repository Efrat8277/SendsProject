/**
 * src/pages/DeliveryPersonsPage.jsx
 * ===================================
 * דף ניהול שליחים.
 * מושגים חדשים בקובץ הזה:
 *   - useEffect  — קוד שרץ כשהדף נטען
 *   - useApi     — ה-hook שבנינו בשלב 1
 *   - טופס הוספה עם controlled inputs
 */
 
import { useEffect,useState } from "react";
import { deliveryPersonApi } from "../services/api";
import useApi from "../hooks/useApi";
import "../styles/DeliveryPersonsPage.css"

const WORK_DAYS = [
  { label: "ראשון", value: 1 },
  { label: "שני",   value: 2 },
  { label: "שלישי", value: 4 },
  { label: "רביעי", value: 8 },
  { label: "חמישי", value: 16 },
  { label: "שישי",  value: 32 },
  { label: "שבת",   value: 64 },
];
export default function DeliveryPersonsPage(){
    // ─────────────────────────────────────────
  // useApi — חיבור לפונקציות מה-api.jsx
  // כל אחת מחזירה: { data, loading, error, execute }

  const {data:persons,loading,error,execute:fetchAll}= 
  useApi(deliveryPersonApi.getAll);

  const {loading: deleting,execute:executeDelete}=
  useApi(deliveryPersonApi.delete);

  const {loading: creating,execute:executeCreate}=
  useApi(deliveryPersonApi.create)

  // ─────────────────────────────────────────
  // ערכי טופס ההוספה
  // ─────────────────────────────────────────

  const [form,setForm]= useState({
    name: "",
    phone: "",
    password: "",
    workDays: 0,
    userName: "",
     startTime: "08:00:00",
    endTime: "17:00:00",

  })

  // ─────────────────────────────────────────
  // useEffect — רץ פעם אחת כשהדף נטען
  // הסוגריים הריקים [] אומרים: "רוץ רק בהתחלה"
  // ─────────────────────────────────────────

  useEffect(()=>{
    fetchAll();
  },[]);

  // ─────────────────────────────────────────
  // פונקציות
  // ─────────────────────────────────────────
  /** עדכון שדה בטופס — פונקציה אחת לכל השדות */
  function handleChange(e){
    const {name,value} = e.target;
    setForm((prev)=>({...prev,[name]:value}));
  }

  function handleWorkDayChange(value){
    setForm((prev)=>({
      ...prev,
      workDays:prev.workDays^value,
    }))
  }
  /** שליחת הטופס — הוספת שליח חדש */
  async function handleCreate(e){
    e.preventDefault();
      console.log("שולח לשרת:", form); // ← הוסף את זה
    await executeCreate(form)
    // רענן את הרשימה לאחר הוספה
    fetchAll();
    // אפס את הטופס
    setForm({name:"",phone: "",password:"",workDays:0,startTime:"08:00:00",endTime:"17:00:00"});
  }

    /** מחיקת שליח לפי ID */
  async function handleDelete(id){
    await executeDelete(id);
    fetchAll();
  }

  return(
    <div className="page">
        <h1 className="page-title">ניהול שליחים</h1>

        <form className="form-card" onSubmit={handleCreate}>
            <h2 className="form-titel">הוסף שליח חדש</h2>

            <div className="form-row">
                <input
                className="form-input"
                name="name"
                value={form.name}
                onChange={handleChange}
                placeholder="שם"
                required
                />
                <input
                className="form-input"
                name="userName"
                value={form.userName}
                onChange={handleChange}
                placeholder="שם משתמש"
                required
                />

                <input
                    className="form-input"
                    name="password"
                    type="password"
                    value={form.password}
                    onChange={handleChange}
                    placeholder="סיסמה"
                    required
                />

                <input
                className="form-input"
                name="startTime"
                type="time"
                value={form.startTime.slice(0,5)}
                onChange={(e)=>
                    setForm((prev)=>({...prev,startTime:e.target.value+":00"}))
                }
                />

                <input
                className="form-input"
                name="endTime"
                type="time"
                value={form.endTime.slice(0,5)}
                onChange={(e)=>
                    setForm((prev)=>({...prev,endTime:e.target.value+":00"}))
                }
                />

            </div>

            <div className="form-row">
  {WORK_DAYS.map((day) => (
    <label key={day.value} className="checkbox-label">
      <input
        type="checkbox"
        checked={!!(form.workDays & day.value)}
        onChange={() => handleWorkDayChange(day.value)}
      />
      {day.label}
    </label>
  ))}
</div>

            <button className="btn btn-primary" type="submit" disabled={creating}>
                {creating?"...מוסיף":"הוסף שליח"}
            </button>
        </form>

          {/* ── מצבי טעינה ושגיאה ── */}
      {loading && <p className="status-msg">טוען...</p>}
      {error && <p className="status-msg error">{error}</p>}

      {/* ── טבלת שליחים ── */}
      {persons && (
        <table className="data-table">
            <thead>
                <tr>
                    <th>שם</th>
                    <th>טלפון</th>
                    <th>שעת התחלה</th>
                    <th>שעת סיום</th>
                    <th>חבילות</th>
                    <th>פעולות</th>
                </tr>
            </thead>
            <tbody>
                {persons.map((person)=>(
                    <tr key={person.deliveryPersonId}>
                        <td>{person.name}</td>
                        <td>{person.phone}</td>
                        <td>{person.startTime}</td>
                        <td>{person.endTime}</td>
                        <td>{person.packages?.length ?? 0}</td>
                        <td>
                            <button
                            className="btn btn-danger"
                            onClick={() => handleDelete(person.deliveryPersonId)}
                            disabled={deleting}
                          >
                         מחק
                        </button>
                        </td>
                    </tr>
                ))}
            </tbody>

        </table>
      )}

    </div>
  )
}