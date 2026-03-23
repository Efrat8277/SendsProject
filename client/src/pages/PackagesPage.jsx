import { useEffect,useState } from "react";
import { packageApi,deliveryPersonApi,recipientApi } from "../services/api";
import useApi from "../hooks/useApi";
import "../styles/PackagesPage.css"

export default function PackagesPage(){
     // ─────────────────────────────────────────
  // נתונים מהשרת
  // ─────────────────────────────────────────

  const {data:packages,loading,error,execute:fetchPackages}=
  useApi(packageApi.getAll)

  const {execute: executeCreate,loading:creating}=
  useApi(packageApi.create);

  const {execute:executeDelete,loading:deleting}=
  useApi(packageApi.delete);

  // רשימות לתפריטים הנפתחים
  const [deliveryPersons,setDeliveryPersons]=useState([])
  const [recipients,setRecipients]=useState([])

   // ─────────────────────────────────────────
  // ערכי טופס
  // ─────────────────────────────────────────

  const [form,setForm]=useState({
    weight:"",
    senderName:"",
    sendDate:new Date().toISOString().slice(0, 10), // תאריך היום כברירת מחדל
    isSentToRecipient:false,
     deliveryPersonId: "",
    recipientId: "",
  })

  useEffect(()=>{
    fetchPackages();


    async function loadDropDowns(){
        const [persons,recs]=await Promise.all([
            deliveryPersonApi.getAll(),
            recipientApi.getAll(),
        ])
        console.log("שליח לדוגמא",persons[0])
        setDeliveryPersons(persons||[])
        setRecipients(recs||[])
    }

    loadDropDowns()
  },[])

  // ─────────────────────────────────────────
  // פונקציות
  // ─────────────────────────────────────────

  function handleChange(e){
    const {name,value,type,checked}=e.target
    setForm((prev)=>({
        ...prev,[name]:type==="checkbox"?checked:value
    }))
  }

  async function handleCreate(e){
    e.preventDefault()
    await executeCreate({
        ...form,
        weight:parseFloat(form.weight),
        deliveryPersonId:parseInt(form.deliveryPersonId),
        recipientId:parseInt(form.recipientId),
        sendDate:new Date(form.sendDate).toISOString(),
    })
    fetchPackages();
    setForm({
        weight:"",
        senderName:"",
        sendDate:new Date().toISOString().slice(0,10),
        isSentToRecipient:false,
        deliveryPersonId:"",
        recipientId:"",
    })
  }

  const { execute: executeUpdate } = useApi(packageApi.update);

  async function handleToggleStatus(pkg) {
  await executeUpdate(pkg.id, {
    id: pkg.id,
    weight: pkg.weight,
    senderName: pkg.senderName,
    sendDate: pkg.sendDate,
    isSentToRecipient: true,
    deliveryPersonId: pkg.deliveryPersonId,
    recipientId: pkg.recipientId,
  });
  fetchPackages();
}

  async function handleDelete(id){
    await executeDelete(id);
    fetchPackages();
  }

  function getWorkDays(workDays) {
  if (!workDays) return "";
  const map = {
    Sunday: "א", Monday: "ב", Tuesday: "ג",
    Wednesday: "ד", Thursday: "ה", Friday: "ו", Saturday: "ש",
  };
  return Object.entries(map)
    .filter(([day]) => workDays.includes(day))
    .map(([, letter]) => letter)
    .join(", ");
}

  return(
    <div className="page">
      <h1 className="page-title">ניהול חבילות</h1>

      <form className="form-card" onSubmit={handleCreate}>
        <h2 className="form-titel">הוסף חבילה חדשה</h2>
        <div className="form-row">
            <input
            className="form-input"
            name="senderName"
            value={form.senderName}
            onChange={handleChange}
            placeholder="שם שולח"
            required
            />
            <input
            className="form-input"
            name="weight"
            type="number"
            step="0.1"
            min="0"
            value={form.weight}
            onChange={handleChange}
            placeholder="משקל (ק״ג)"
            required
            />
              <input
            className="form-input"
            name="sendDate"
            type="date"
            value={form.sendDate}
            onChange={handleChange}
            required
          />
        </div>
        <div className="form-row">
            <select
            className="form-input"
            name="deliveryPersonId"
            value={form.deliveryPersonId}
            onChange={handleChange}
            required
            >
                <option value="">בחר שליח</option>
            {deliveryPersons.map((p) => (
              <option key={p.deliveryPersonId} value={p.deliveryPersonId}>
            {p.name}-{getWorkDays(p.workDays)}
              </option>
            ))}
            </select>

            <select
            className="form-input"
            name="recipientId"
            value={form.recipientId}
            onChange={handleChange}
            required
            >
                <option value="">בחר נמען</option>
                {recipients.map((r)=>(
                    <option key={r.recipientId} value={r.recipientId}>
                        {r.name}
                    </option>
                ))}


            </select>
            <label className="checkbox-label">
            <input
              type="checkbox"
              name="isSentToRecipient"
              checked={form.isSentToRecipient}
              onChange={handleChange}
            />
            נשלח לנמען
          </label>
        </div>
        {/*---------------------------- */}
         <button className="btn btn-primary" type="submit" disabled={creating}>
          {creating ? "מוסיף..." : "הוסף חבילה"}
        </button>
      </form>


      {loading && <p className="status-msg">טוען...</p>}
      {error && <p className="status-msg error">{error}</p>}
 
      {/* ── טבלת חבילות ── */}
      {packages && (
        <table className="data-table">
          <thead>
            <tr>
              <th>שולח</th>
              <th>משקל</th>
              <th>תאריך שליחה</th>
              <th>שליח</th>
              <th>נמען</th>
              <th>סטטוס</th>
              <th>פעולות</th>
            </tr>
          </thead>
          <tbody>
            {packages.map((pkg) => (
              <tr key={pkg.id}>
                <td>{pkg.senderName}</td>
                <td>{pkg.weight} ק״ג</td>
                <td>{new Date(pkg.sendDate).toLocaleDateString("he-IL")}</td>
                <td>
                  {/* מחפש את שם השליח לפי ה-ID */}
                  {deliveryPersons.find(
                    (p) => p.deliveryPersonId === pkg.deliveryPersonId
                  )?.name || pkg.deliveryPersonId}
                </td>
                <td>
                  {recipients.find((r) => r.recipientId === pkg.recipientId)
                    ?.name || pkg.recipientId}
                </td>
                <td>
                  <span className={`badge ${pkg.isSentToRecipient ? "badge-success" : "badge-pending"}`}>
                    {pkg.isSentToRecipient ? "נמסר" : "בדרך"}
                  </span>
                </td>
                <td>
                  {!pkg.isSentToRecipient && (
  <button
    className="btn btn-primary"
    onClick={() => handleToggleStatus(pkg)}
    style={{ marginLeft: "8px" }}
  >
    סמן כנמסר
  </button>
)}

                 
                  <button
                    className="btn btn-danger"
                    onClick={() => handleDelete(pkg.id)}
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