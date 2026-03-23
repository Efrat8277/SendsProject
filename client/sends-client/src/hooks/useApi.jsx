/**
 * src/hooks/useApi.jsx
 * =====================
 * Custom Hook — אחד המושגים החשובים ביותר ב-React.
 *
 * בעיה שהוא פותר:
 * בכל דף שמושך נתונים מהשרת, נצטרך לכתוב את אותו קוד שוב ושוב:
 *   const [data, setData] = useState([])
 *   const [loading, setLoading] = useState(false)
 *   const [error, setError] = useState("")
 *   ... try/catch/finally ...
 *
 * הפתרון — hook אחד שעושה את כל זה.
 * כל דף פשוט קורא: const { data, loading, error, execute } = useApi()
 *
 * מה זה Hook?
 * פונקציה שמתחילה ב-"use" ויכולה להשתמש ב-useState, useEffect וכו'.
 * React מזהה אותה כ-hook ומתנהג בהתאם.
 */

import { useState,useCallback } from "react";

/**
 * @param {Function} apiFunction — הפונקציה מ-api.jsx שרוצים לקרוא
 *
 * מחזיר:
 *   data    — התוצאה מהשרת
 *   loading — האם יש בקשה פעילה כרגע
 *   error   — הודעת שגיאה אם היה כשל
 *   execute — פונקציה שמפעילה את הקריאה (מקבלת ארגומנטים)
 */

export default function useApi(apiFunction){
    const [data, setData] = useState(null)
    const [loading, setLoading] = useState(false)
    const [error, setError] = useState(null)

    /**
   * useCallback — מונע יצירה מחדש של הפונקציה בכל render.
   * חשוב כאן כי execute עלול להיות תלות ב-useEffect של הדף.
   */

    const execute = useCallback(
        async (...args) =>{
            setLoading(true)
            setError(null)
            try{
                const result = await apiFunction(...args)
                setData(result)
                return result
            }catch (err){
                setError(err.message||"שגיאה")
                return null
            }finally{
                setLoading(false)
            }
        },
        [apiFunction]
    )
    return {data, loading, error, execute};
}