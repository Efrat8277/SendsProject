
import { recipientApi } from "../services/api"
import useApi from "../hooks/useApi"
import { useEffect,useState } from "react"
import "../styles/RecipientsPage.css"


export default function RecipientsPage(){
    const {data:recipients,loading,error,execute:fetchAll}=
    useApi(recipientApi.getAll)

    const {loading:creating,execute:executeCreate}=
    useApi(recipientApi.create)

    const {loading:deleting,execute:executeDelete}=
    useApi(recipientApi.delete)

    const [form,setForm]=useState({
        name: "",
        identity:"",
        phone:"",
        address:"",
        password:"",
    })

    useEffect(()=>{
        fetchAll()
    },[])

    function handleChange(e){
        const {name,value}=e.target
        setForm((prev)=>({...prev,[name]: value}))
    }

    async function handleCreate(e){
        e.preventDefult()
        await executeCreate(form)
        fetchAll()
        setForm({name:"",identity:"",phone:"",adress:"",password:""})
    }

    async function handleDelete(id){
        await executeDelete(id)
        fetchAll()
    }

    return(
        <div className="page">
            <h1 className="page-titel">ניהול נמענים</h1>

            {/* ── טופס הוספה ── */}
            <form className="form-card" onSubmit={handleCreate}>
                <h2 className="form-titel">הוסף נמען חדש</h2>

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
                    name="identity"
                    value={form.identity}
                    onChange={handleChange}
                    placeholder="תעודת זהות"
                    required
                    />
                    <input
                    className="form-input"
                    name="address"
                    value={form.address}
                    onChange={handleChange}
                    placeholder="כתובת"
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

                </div>
                <button className="btn btn-primary" type="submit" disabled={creating}>
                    {creating?"...מוסיף":"הוסף המען"}
                </button>
            </form>

            {loading&&<p className="status-msg">...טוען</p>}
            {error && <p className="status-msg error">{error}</p>}

            {/* ── טבלת נמענים ── */}
            {recipients &&(
                <table className="data-table">
                    <thead>
                        <tr>
                            <th>שם</th>
                            <th>תעודת זהות</th>
                            <th>טלפון</th>
                            <th>כתובת</th>
                            <th>פעולות</th>
                        </tr>
                    </thead>
                    <tbody>
                        {recipients.map((recipient)=>(
                            <tr key={recipient.recipientId}>
                                <td>{recipient.name}</td>
                                <td>{recipient.identity}</td>
                                <td>{recipient.phone}</td>
                                <td>{recipient.address}</td>
                                <td>
                                    <button
                                    className="btn btn-danger"
                                    onClick={()=>handleDelete(recipient.recipientId)}
                                    disabled={deleting}
                                    >מחק</button>
                                </td>
                            </tr>
                        ))}
                    </tbody>
                </table>
            )}
        </div>
    )
}