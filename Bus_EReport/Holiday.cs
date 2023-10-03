using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DBase_EReport;

namespace Bus_EReport
{
    public class Holiday
    {
        private string strQry = string.Empty;
        private string strNowYear = DateTime.Now.Year.ToString();

        public DataSet getHolidays(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsHoliday = null;
            //convert(varchar,Sf_Joining_Date,103) Sf_Joining_Date
            strQry = "SELECT a.sl_no,a.Academic_Year, a.state_code, convert(varchar,a.Holiday_Date,101) Holiday_Date, a.Holiday_Name_Sl_No, a.Holiday_Name, a.Created_Date, a.Division_Code, b.StateName as State_Name" +
                     " FROM Mas_Statewise_Holiday_Fixation a, mas_state b " +
                     " WHERE   a.Division_Code = '" + divcode + "' AND a.state_code=b.state_code " +
                     " ORDER BY 1";
            try
            {
                dsHoliday = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsHoliday;
        }
        // Sorting For HolidayList(Admin)
        //Change getHolidaylist_DataTable parameter done by saravanan 07-08-2014
        public DataTable getHolidaylist_DataTable(string divcode, string strYear, string state_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dtHoliday = null;
            //strQry = "SELECT a.sl_no,a.Academic_Year, a.state_code, convert(varchar,a.Holiday_Date,101) Holiday_Date, a.Holiday_Name_Sl_No, a.Holiday_Name, a.Created_Date, a.Division_Code, b.StateName as State_Name" +
            //         " FROM Mas_Statewise_Holiday_Fixation a, mas_state b " +
            //         " WHERE   a.Division_Code = '" + divcode + "' AND a.state_code='" + statcode + "' AND a.Academic_Year='" + strYear + "' AND  a.state_code=b.state_code " +
            //         " ORDER BY 1";

            strQry = "SELECT a.sl_no,a.Academic_Year, a.state_code, convert(varchar,a.Holiday_Date,105) Holiday_Date, a.Holiday_Name_Sl_No, a.Holiday_Name, a.Created_Date, a.Division_Code, b.StateName as State_Name, CONVERT(varchar(3), Holiday_Date, 100) as month" +
                " FROM mas_state b join Mas_Statewise_Holiday_Fixation a " +
                " on a.state_code like '" + state_code + ',' + "%'  or " +
                " a.state_code like '%" + ',' + state_code + "' or" +
                " a.state_code like '%" + ',' + state_code + ',' + "%' or a.state_code like '" + state_code + "'" +
                " WHERE convert(varchar,b.state_code)='" + state_code + "' AND a.Academic_Year='" + strYear + "' " +
                " and a.Division_Code = '" + divcode + "' " +
                " ORDER BY month(Holiday_Date) ,day(Holiday_Date)";
            try
            {
                dtHoliday = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtHoliday;
        }
        //Change Strqry done by saravanan 07-08-2014
        public DataSet getHolidays(string state_code, string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsHoliday = null;

            //strQry = "SELECT a.sl_no,a.Academic_Year, a.state_code, convert(varchar,a.Holiday_Date,105) Holiday_Date, a.Holiday_Name_Sl_No, a.Holiday_Name, a.Created_Date, a.Division_Code, b.StateName as State_Name" +
            //         " FROM Mas_Statewise_Holiday_Fixation a, mas_state b " +
            //         " WHERE a.Division_Code = '" + divcode + "' AND a.state_code=b.state_code AND a.state_code = '" + state_code + "' " +
            //         " and a.Academic_Year ='" + strNowYear + "' ORDER BY 3";

            strQry = "SELECT a.sl_no,a.Academic_Year, a.state_code, convert(varchar,a.Holiday_Date,105) Holiday_Date, a.Holiday_Name_Sl_No, a.Holiday_Name, a.Created_Date, a.Division_Code, b.StateName as State_Name" +
                   " FROM mas_state b join Mas_Statewise_Holiday_Fixation a " +
                   " on a.state_code like '" + state_code + ',' + "%'  or " +
                   " a.state_code like '%" + ',' + state_code + "' or" +
                   " a.state_code like '%" + ',' + state_code + ',' + "%' or a.state_code like '" + state_code + "'" +
                   " WHERE convert(varchar,b.state_code)='" + state_code + "' " +
                   " and a.Division_Code = '" + divcode + "' " +
                   " ORDER BY month(Holiday_Date) ,day(Holiday_Date)";
            try
            {
                dsHoliday = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsHoliday;
        }


        public DataSet getHolidayYear(string state_code, string divcode, string ddlYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsHoliday = null;
            strQry = "SELECT a.sl_no,a.Academic_Year, a.state_code, convert(varchar,a.Holiday_Date,105) Holiday_Date, a.Holiday_Name_Sl_No, a.Holiday_Name, a.Created_Date, a.Division_Code, b.StateName as State_Name" +
                     " FROM Mas_Statewise_Holiday_Fixation a, mas_state b " +
                     " WHERE a.Division_Code = '" + divcode + "' AND a.state_code=b.state_code AND a.state_code = '" + state_code + "' " +
                     " and a.Academic_Year ='" + ddlYear + "' ORDER BY 3";
            try
            {
                dsHoliday = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsHoliday;
        }
        //Change done by saravanan 07-08-2014

        // Sorting For MR_HolidayList 
        public DataTable getHolidayslistMR_DataTable(string state_code, string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dtHoliday = null;
            //strQry = "SELECT a.sl_no,a.Academic_Year, a.state_code, convert(varchar,a.Holiday_Date,101) Holiday_Date, a.Holiday_Name_Sl_No, a.Holiday_Name, a.Created_Date, a.Division_Code, b.StateName as State_Name" +
            //         " FROM Mas_Statewise_Holiday_Fixation a, mas_state b " +
            //         " WHERE a.Division_Code = '" + divcode + "' AND a.state_code=b.state_code AND a.state_code = '" + state_code + "' " +
            //         " ORDER BY 3";
            strQry = "SELECT a.sl_no,a.Academic_Year, a.state_code, convert(varchar,a.Holiday_Date,105) Holiday_Date, a.Holiday_Name_Sl_No, a.Holiday_Name, a.Created_Date, a.Division_Code, b.StateName as State_Name" +
                 " FROM mas_state b join Mas_Statewise_Holiday_Fixation a " +
                 " on a.state_code like '" + state_code + ',' + "%'  or " +
                 " a.state_code like '%" + ',' + state_code + "' or" +
                 " a.state_code like '%" + ',' + state_code + ',' + "%' or a.state_code like '" + state_code + "'" +
                 " WHERE convert(varchar,b.state_code)='" + state_code + "' " +
                 " and a.Division_Code = '" + divcode + "' " +
                 " ORDER BY month(Holiday_Date) ,day(Holiday_Date)";
            try
            {
                dtHoliday = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtHoliday;
        }

        public DataSet getHoli(string divcode, string slno)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsHoliday = null;
            strQry = "SELECT  state_code, Academic_Year, Holiday_Name,convert(varchar,Holiday_Date,105) Holiday_Date " +
                     " FROM Mas_Statewise_Holiday_Fixation " +
                     " WHERE Sl_No = '" + slno + "' AND  Division_Code = '" + divcode + "' " +
                     " ORDER BY 1";
            try
            {
                dsHoliday = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsHoliday;
        }

        public DataSet getState(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet dsHoliday = null;
            strQry = "SELECT  distinct a.state_code as statecode,b.statename as statename FROM Mas_Statewise_Holiday_Fixation a,mas_State b" +
                     " WHERE  a.state_code = b.state_code and Division_Code = '" + divcode + "' " +
                     " ORDER BY 2";
            try
            {
                dsHoliday = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsHoliday;
        }

        public DataSet getYear(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsHoliday = null;
            strQry = "SELECT  max(a.Academic_Year)as Academic_Year, a.state_code as statecode FROM Mas_Statewise_Holiday_Fixation a,mas_State b" +
                     " WHERE  a.state_code = b.state_code and Division_Code = '" + divcode + "' " +
                     " ORDER BY 2";
            try
            {
                dsHoliday = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsHoliday;
        }

        ////public int RecordAdd(int year, string state_code, string holiday_date, string holiday_Name, string div_code, int hdnHolidayID, string lblMulti, string existingState)
        ////{
        ////    int iReturn = -1;

        ////    try
        ////    {
        ////        DB_EReporting db = new DB_EReporting();
        ////        int Sl_No = -1;
        ////        strQry = "SELECT ISNULL(MAX(Sl_No),0)+1 FROM Mas_Statewise_Holiday_Fixation ";
        ////        Sl_No = db.Exec_Scalar(strQry);

        ////        if (lblMulti == "0")
        ////        {
        ////            if (Holiday_RecordExist(div_code, holiday_Name, holiday_date))
        ////            {
        ////                state_code += existingState;

        ////                string[] str = state_code.Split(',');
        ////                string st = string.Empty;
        ////                foreach (string strar in str)
        ////                {
        ////                    if (st.Contains(strar))
        ////                    {

        ////                    }
        ////                    else
        ////                    {
        ////                        st += strar + ",";
        ////                    }
        ////                }

        ////                //if (state_code != "")
        ////                //{
        ////                strQry = "UPDATE Mas_Statewise_Holiday_Fixation " +
        ////                     " SET Academic_Year = " + year + ", State_code = '" + st + "', Holiday_Date = '" + holiday_date.Substring(6, 4) + "-" + holiday_date.Substring(3, 2) + "-" + holiday_date.Substring(0, 2) + "' , " +
        ////                     " holiday_Name='" + holiday_Name + "',LastUpdt_Date = getdate() " +
        ////                     " WHERE Division_Code = '" + div_code + "' and Holiday_Name_Sl_No='" + hdnHolidayID + "' and convert(char(10),Holiday_Date,105)='" + holiday_date + "'  ";
        ////                //}
        ////                //else
        ////                //{
        ////                //    strQry = "Delete Mas_Statewise_Holiday_Fixation WHERE Division_Code = '" + div_code + "' and Holiday_Name_Sl_No='" + hdnHolidayID + "' and convert(char(10),Holiday_Date,105)='" + holiday_date + "'";
        ////                //}
        ////                //strQry = "EXEC SaveHolidayList '" + state_code + "', '" + holiday_date.Substring(6, 4) + "-" + holiday_date.Substring(3, 2) + "-" + holiday_date.Substring(0, 2) + "','" + hdnHolidayID + "','" + div_code + "' ";
        ////            }
        ////            else if (state_code != "")
        ////            {

        ////                strQry = "INSERT INTO Mas_Statewise_Holiday_Fixation(Sl_No,Academic_Year, State_code, Holiday_Date, Holiday_Name_Sl_No, Holiday_Name, Created_Date, Division_Code,LastUpdt_Date) " +
        ////                         " values( " + Sl_No + ", " + year + ", '" + state_code + "', '" + holiday_date.Substring(6, 4) + "-" + holiday_date.Substring(3, 2) + "-" + holiday_date.Substring(0, 2) + "', " + hdnHolidayID + ", '" + holiday_Name + "', GETDATE(), '" + div_code + "',getdate()) ";
        ////            }
        ////        }
        ////        else
        ////        {
        ////            if (Holiday_SingleRecordExist(div_code, holiday_Name))
        ////            {
        ////                 state_code += existingState;
        ////                 string[] str = state_code.Split(',');
        ////                 string st = string.Empty;
        ////                 foreach (string strar in str)
        ////                 {
        ////                     if (st.Contains(strar))
        ////                     {

        ////                     }
        ////                     else
        ////                     {
        ////                         st += strar + ",";
        ////                     }
        ////                 }
        ////                 //if (state_code != "")
        ////                 //{
        ////                     strQry = "UPDATE Mas_Statewise_Holiday_Fixation " +
        ////                          " SET Academic_Year = " + year + ", State_code = '" + st + "', Holiday_Date = '" + holiday_date.Substring(6, 4) + "-" + holiday_date.Substring(3, 2) + "-" + holiday_date.Substring(0, 2) + "' , " +
        ////                          " holiday_Name='" + holiday_Name + "',LastUpdt_Date = getdate() " +
        ////                          " WHERE Division_Code = '" + div_code + "' and Holiday_Name_Sl_No='" + hdnHolidayID + "'  ";
        ////                 //}
        ////                 //else 
        ////                 //{
        ////                 //    strQry = "Delete Mas_Statewise_Holiday_Fixation WHERE Division_Code = '" + div_code + "' and Holiday_Name_Sl_No='" + hdnHolidayID + "'  ";
        ////                 //}
        ////                //strQry = "EXEC SaveHolidayList '" + state_code + "', '" + holiday_date.Substring(6, 4) + "-" + holiday_date.Substring(3, 2) + "-" + holiday_date.Substring(0, 2) + "','" + hdnHolidayID + "','" + div_code + "' ";

        ////            }
        ////            else if (state_code != "")
        ////            {

        ////                strQry = "INSERT INTO Mas_Statewise_Holiday_Fixation(Sl_No,Academic_Year, State_code, Holiday_Date, Holiday_Name_Sl_No, Holiday_Name, Created_Date, Division_Code,LastUpdt_Date) " +
        ////                         " values( " + Sl_No + ", " + year + ", '" + state_code + "', '" + holiday_date.Substring(6, 4) + "-" + holiday_date.Substring(3, 2) + "-" + holiday_date.Substring(0, 2) + "', " + hdnHolidayID + ", '" + holiday_Name + "', GETDATE(), '" + div_code + "',getdate()) ";
        ////            }

        ////        }
        ////        iReturn = db.ExecQry(strQry);
        ////    }
        ////    catch (Exception ex)
        ////    {
        ////        throw ex;
        ////    }
        ////    //}
        ////    //else
        ////    //{
        ////    //    iReturn = -2;
        ////    //}
        ////    return iReturn;
        ////}

        public int RecordAdd(int year, string state_code, string holiday_date, string holiday_Name, string div_code, int hdnHolidayID, string lblMulti, string existingState)
        {
            int iReturn = -1;

            try
            {
                DB_EReporting db = new DB_EReporting();
                int Sl_No = -1;
                strQry = "SELECT ISNULL(MAX(Sl_No),0)+1 FROM Mas_Statewise_Holiday_Fixation ";
                Sl_No = db.Exec_Scalar(strQry);

                if (lblMulti == "0")
                {
                    if (Holiday_RecordExist(div_code, holiday_Name, holiday_date))
                    {
                        state_code += existingState;
                        if (state_code != "")
                        {
                            strQry = "UPDATE Mas_Statewise_Holiday_Fixation " +
                                 " SET Academic_Year = " + year + ", State_code = '" + state_code + "', Holiday_Date = '" + holiday_date.Substring(6, 4) + "-" + holiday_date.Substring(3, 2) + "-" + holiday_date.Substring(0, 2) + "' , " +
                                 " holiday_Name='" + holiday_Name + "',LastUpdt_Date = getdate() " +
                                 " WHERE Division_Code = '" + div_code + "' and Holiday_Name_Sl_No='" + hdnHolidayID + "' and convert(char(10),Holiday_Date,105)='" + holiday_date + "'  ";
                        }
                        else
                        {
                            strQry = "Delete Mas_Statewise_Holiday_Fixation WHERE Division_Code = '" + div_code + "' and Holiday_Name_Sl_No='" + hdnHolidayID + "' and convert(char(10),Holiday_Date,105)='" + holiday_date + "'";
                        }
                        //strQry = "EXEC SaveHolidayList '" + state_code + "', '" + holiday_date.Substring(6, 4) + "-" + holiday_date.Substring(3, 2) + "-" + holiday_date.Substring(0, 2) + "','" + hdnHolidayID + "','" + div_code + "' ";

                    }
                    else if (state_code != "")
                    {

                        strQry = "INSERT INTO Mas_Statewise_Holiday_Fixation(Sl_No,Academic_Year, State_code, Holiday_Date, Holiday_Name_Sl_No, Holiday_Name, Created_Date, Division_Code,LastUpdt_Date) " +
                                 " values( " + Sl_No + ", " + year + ", '" + state_code + "', '" + holiday_date.Substring(6, 4) + "-" + holiday_date.Substring(3, 2) + "-" + holiday_date.Substring(0, 2) + "', " + hdnHolidayID + ", '" + holiday_Name + "', GETDATE(), '" + div_code + "',getdate()) ";
                    }
                }
                else
                {
                    //if (Holiday_SingleRecordExist(div_code, holiday_Name))
                    if (Holiday_RecordExist(div_code, holiday_Name, holiday_date))
                    {
                        state_code += existingState;
                        if (state_code != "")
                        {
                            strQry = "UPDATE Mas_Statewise_Holiday_Fixation " +
                                 " SET Academic_Year = " + year + ", State_code = '" + state_code + "', Holiday_Date = '" + holiday_date.Substring(6, 4) + "-" + holiday_date.Substring(3, 2) + "-" + holiday_date.Substring(0, 2) + "' , " +
                                 " holiday_Name='" + holiday_Name + "',LastUpdt_Date = getdate() " +
                                 " WHERE Division_Code = '" + div_code + "' and Holiday_Name_Sl_No='" + hdnHolidayID + "'  ";
                        }
                        else
                        {
                            strQry = "Delete Mas_Statewise_Holiday_Fixation WHERE Division_Code = '" + div_code + "' and Holiday_Name_Sl_No='" + hdnHolidayID + "'  ";
                        }
                        //strQry = "EXEC SaveHolidayList '" + state_code + "', '" + holiday_date.Substring(6, 4) + "-" + holiday_date.Substring(3, 2) + "-" + holiday_date.Substring(0, 2) + "','" + hdnHolidayID + "','" + div_code + "' ";

                    }
                    else if (state_code != "")
                    {

                        strQry = "INSERT INTO Mas_Statewise_Holiday_Fixation(Sl_No,Academic_Year, State_code, Holiday_Date, Holiday_Name_Sl_No, Holiday_Name, Created_Date, Division_Code,LastUpdt_Date) " +
                                 " values( " + Sl_No + ", " + year + ", '" + state_code + "', '" + holiday_date.Substring(6, 4) + "-" + holiday_date.Substring(3, 2) + "-" + holiday_date.Substring(0, 2) + "', " + hdnHolidayID + ", '" + holiday_Name + "', GETDATE(), '" + div_code + "',getdate()) ";
                    }

                }
                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            //}
            //else
            //{
            //    iReturn = -2;
            //}
            return iReturn;
        }
        public bool Holiday_SingleRecordExist(string Division_Code, string Holiday_Name)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "Select count(*) from Mas_Statewise_Holiday_Fixation " +
                         " where Division_Code='" + Division_Code + "'  and Holiday_Name='" + Holiday_Name + "'";

                int iRecordExist = db.Exec_Scalar(strQry);

                if (iRecordExist > 0)
                    bRecordExist = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return bRecordExist;
        }
        public int RecordUpdate(string statecode, string Slno, string HolName, string Holdate, string divcode)
        {
            int iReturn = -1;
            //if (!RecordExist(statecode, shortname))
            //{
            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "UPDATE Mas_Statewise_Holiday_Fixation " +
                         " SET Holiday_Name = '" + HolName + "', " +
                         " Holiday_Date = '" + Holdate.Substring(6, 4) + "-" + Holdate.Substring(3, 2) + "-" + Holdate.Substring(0, 2) + "' , " +
                         " LastUpdt_Date = getdate() " +
                         " WHERE Sl_No = '" + Slno + "'  AND  State_code ='" + statecode + "' AND  Division_Code = '" + divcode + "' ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            //}
            //else
            //{
            //    iReturn = -2;
            //}
            return iReturn;

        }
        public int deleteholiday(string holiID, string holidate, string statecode, string div)
        {
            int iReturn = -1;

            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "exec deleteholiday '" + holiID + "','" + holidate + "','" + statecode + "','" + div + "'";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;

        }
        public int RecordEdit(int year, int state, DateTime Holdate, string holiday, string divcode, string Slno)
        {
            int iReturn = -1;
            //if (!RecordExist(statecode, shortname))
            //{
            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "UPDATE Mas_Statewise_Holiday_Fixation " +
                         " SET Holiday_Name = '" + holiday + "', " +
                         " Holiday_Date = '" + Holdate + "', " +
                         " Academic_Year = " + year + " ," +
                         " State_code = " + state + "," +
                         " LastUpdt_Date = getdate() " +
                         " WHERE Sl_No = '" + Slno + "'  AND  Division_Code = '" + divcode + "' ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            //}
            //else
            //{
            //    iReturn = -2;
            //}
            return iReturn;

        }
        public int RecordDelete(string slno, string Division_Code)
        {
            int iReturn = -1;
            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "DELETE FROM  Mas_Statewise_Holiday_Fixation WHERE Sl_No = '" + slno + "' and Division_Code = '" + Division_Code + "' ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;

        }
        public DataSet getHoliday_View(string state_code, string divcode, string ddlYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsHoliday = null;
            //strQry = "SELECT a.sl_no,a.Academic_Year, a.state_code, convert(varchar,a.Holiday_Date,105) Holiday_Date, a.Holiday_Name_Sl_No, a.Holiday_Name, a.Created_Date, a.Division_Code, b.StateName as State_Name, MONTH(holiday_date) [Month]" +
            //         " FROM Mas_Statewise_Holiday_Fixation a, mas_state b " +
            //         " WHERE a.Division_Code = '" + divcode + "' AND a.state_code=b.state_code AND a.state_code = '" + state_code + "' " +
            //         " and a.Academic_Year ='" + ddlYear + "'  ORDER BY 3";

            strQry = "SELECT a.sl_no,a.Academic_Year, a.state_code, convert(varchar,a.Holiday_Date,105) Holiday_Date, " +
                    " a.Holiday_Name_Sl_No, a.Holiday_Name, a.Created_Date, " +
                    " a.Division_Code, stuff((select ', '+StateName from mas_state b where charindex(cast(b.state_code as varchar)+',',a.state_code+',')>0 for XML path('')),1,2,'') StateName, " +
                    " MONTH(holiday_date) [Month] FROM Mas_Statewise_Holiday_Fixation a   WHERE a.Division_Code = '" + divcode + "' " +
                    " and a.Academic_Year = '" + ddlYear + "'" +
                    " and (a.state_code like '" + state_code + ',' + "%'  or " +
                    " a.state_code like '%" + ',' + state_code + "' or" +
                    " a.state_code like '%" + ',' + state_code + ',' + "%' or a.state_code like '" + state_code + "' )";
            try
            {
                dsHoliday = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsHoliday;
        }

        public bool Holiday_RecordExist(string Division_Code, string Holiday_Name, string StrDate)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "Select count(*) from Mas_Statewise_Holiday_Fixation " +
                         " where Division_Code='" + Division_Code + "'  and Holiday_Name='" + Holiday_Name + "' and convert(char(10),Holiday_Date,105)='" + StrDate + "' ";

                int iRecordExist = db.Exec_Scalar(strQry);

                if (iRecordExist > 0)
                    bRecordExist = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return bRecordExist;
        }
        public DataSet getmulti_Days()
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsHoliday = null;
            //convert(varchar,Sf_Joining_Date,103) Sf_Joining_Date
            strQry = "SELECT Holiday_Id,Holiday_Name,Multiple_Date" +
                     " FROM Holidaylist " +
                     " WHERE  Holiday_Active_Flag=0  and Multiple_Date=0";

            try
            {
                dsHoliday = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsHoliday;
        }
        public DataSet get_Holidays(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsHoliday = null;
            //convert(varchar,Sf_Joining_Date,103) Sf_Joining_Date
            strQry = "SELECT Holiday_Id,Holiday_Name,Multiple_Date,Fixed_date,Month,datename(month,dateadd(month, [Month] - 1, 0)) as MonthName" +
                     " FROM Holidaylist where Holiday_Active_Flag=0 and Division_Code = '" + div_code + "'" +
                     " order by Holiday_SlNo";


            try
            {
                dsHoliday = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsHoliday;
        }
        //Changes done by Priya --jan6
        public DataSet getHoliday_List(string state_code, string divcode, string ddlYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsHoliday = null;

            strQry = "SELECT a.sl_no,a.Academic_Year, a.state_code, convert(varchar,a.Holiday_Date,105) Holiday_Date, a.Holiday_Name_Sl_No, a.Holiday_Name, a.Created_Date, a.Division_Code, b.StateName as State_Name, CONVERT(varchar(3), Holiday_Date, 100) as month" +
                     " FROM mas_state b join Mas_Statewise_Holiday_Fixation a " +
                     " on a.state_code like '" + state_code + ',' + "%'  or " +
                     " a.state_code like '%" + ',' + state_code + "' or" +
                     " a.state_code like '%" + ',' + state_code + ',' + "%' or a.state_code like '" + state_code + "'" +
                     " WHERE convert(varchar,b.state_code)='" + state_code + "' " +
                     " and a.Division_Code = '" + divcode + "' " +
                     " and a.Academic_Year ='" + ddlYear + "' ORDER BY month(Holiday_Date) ,day(Holiday_Date)";
            try
            {
                dsHoliday = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsHoliday;
        }
        public DataSet getHoldayDate(string state_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsHolidayState = null;

            //strQry = " select Sl_No,convert(char(10),Holiday_Date,103) Holiday_Date,Holiday_Name,State_Code from Mas_Statewise_Holiday_Fixation " +
            //         " where State_Code in ('" + state_code + "') " +
            //         " ORDER BY 2";

            strQry = "SELECT a.sl_no,a.Academic_Year, a.state_code, convert(varchar,a.Holiday_Date,105) Holiday_Date, a.Holiday_Name_Sl_No, a.Holiday_Name, a.Created_Date, a.Division_Code, b.StateName as State_Name" +
                      " FROM mas_state b join Mas_Statewise_Holiday_Fixation a " +
                      " on a.state_code like '" + state_code + ',' + "%'  or " +
                      " a.state_code like '%" + ',' + state_code + "' or" +
                      " a.state_code like '%" + ',' + state_code + ',' + "%' or a.state_code like '" + state_code + "'" +
                      " WHERE convert(varchar,b.state_code)='" + state_code + "' ";

            try
            {
                dsHolidayState = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsHolidayState;
        }
        //public int RecordDelete(string slno)
        //{
        //    int iReturn = -1;
        //    try
        //    {

        //        DB_EReporting db = new DB_EReporting();

        //        strQry = "DELETE FROM  Mas_Statewise_Holiday_Fixation WHERE Sl_No = '" + slno + "' ";

        //        iReturn = db.ExecQry(strQry);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    return iReturn;

        //}

        public int Add_Holiday(string Holiday_Name, int Multiple_Date, string Fixed_date, string Month, string div_code)
        {
            int iReturn = -1;
            if (!RecordExistAdd(Holiday_Name, div_code))
            {
                try
                {

                    DB_EReporting db = new DB_EReporting();
                    strQry = "SELECT isnull(max(Holiday_Id)+1,'1') Holiday_Id from Holidaylist ";
                    int Holiday_Id = db.Exec_Scalar(strQry);
                    if (Fixed_date != "")
                    {
                        strQry = "INSERT INTO Holidaylist(Holiday_Id,Holiday_Name,Multiple_Date,Fixed_date,Month,Created_Date, Holiday_Active_Flag, Division_Code)" +
                        "values('" + Holiday_Id + "','" + Holiday_Name + "', " + Multiple_Date + ",'" + Fixed_date.Substring(6, 4) + "-" + Fixed_date.Substring(3, 2) + "-" + Fixed_date.Substring(0, 2) + "','" + Month + "', GETDATE(), 0, '" + div_code + "') ";
                    }
                    else
                    {
                        strQry = "INSERT INTO Holidaylist(Holiday_Id,Holiday_Name,Multiple_Date,Month,Created_Date, Holiday_Active_Flag, Division_Code)" +
                 "values('" + Holiday_Id + "','" + Holiday_Name + "', " + Multiple_Date + ",'" + Month + "', GETDATE(), 0,'" + div_code + "') ";
                    }

                    iReturn = db.ExecQry(strQry);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
                iReturn = -2;
            }
            return iReturn;

        }
        public bool RecordExist(string Holiday_Name, int hdnHolidayID, string div_code)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(Holiday_Id) FROM Holidaylist WHERE Holiday_Name='" + Holiday_Name + "'  AND Holiday_Id !='" + hdnHolidayID + "' and Division_Code = '" + div_code + "'";
                int iRecordExist = db.Exec_Scalar(strQry);

                if (iRecordExist > 0)
                    bRecordExist = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return bRecordExist;
        }
        public int Update_Holiday(int hdnHolidayID, string Holiday_Name, int Multiple_Date, string Fixed_date, string Month, string div_code)
        {
            int iReturn = -1;
            if (!RecordExist(Holiday_Name, hdnHolidayID, div_code))
            {
                try
                {

                    DB_EReporting db = new DB_EReporting();
                    if (Fixed_date != "")
                    {
                        strQry = "Update Holidaylist set Holiday_Name = '" + Holiday_Name + "',Multiple_Date =" + Multiple_Date + ",Fixed_date='" + Fixed_date.Substring(6, 4) + "-" + Fixed_date.Substring(3, 2) + "-" + Fixed_date.Substring(0, 2) + "',Month='" + Month + "',Division_Code = '" + div_code + "' " +
                       " where Holiday_Id=" + hdnHolidayID + " and Holiday_Active_Flag=0 ";
                    }
                    else
                    {
                        strQry = "Update Holidaylist set Holiday_Name = '" + Holiday_Name + "',Multiple_Date =" + Multiple_Date + ",Month='" + Month + "', Division_Code = '" + div_code + "' " +
                                 " where Holiday_Id=" + hdnHolidayID + " and Holiday_Active_Flag=0 ";
                    }

                    iReturn = db.ExecQry(strQry);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
                iReturn = -2;
            }
            return iReturn;

        }
        public DataSet getHolidayName_Alphabet()
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsHoliday = null;
            strQry = "select '1' val,'All' Holiday_Name " +
                     " union " +
                     " select distinct LEFT(Holiday_Name,1) val, LEFT(Holiday_Name,1) Holiday_Name" +
                     " FROM HolidayList where Holiday_Active_Flag=0 " +
                     " ORDER BY 1";
            try
            {
                dsHoliday = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsHoliday;
        }
        public DataSet getHolidayName_Alphabet(string sAlpha, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsHoliday = null;
            strQry = "SELECT Holiday_Id,Holiday_Name,datename(month,dateadd(month, [Month] - 1, 0)) as MonthName " +
                     " FROM HolidayList " +
                     " where LEFT(Holiday_Name,1) = '" + sAlpha + "' and Holiday_Active_Flag=0 and Division_Code = '" + div_code + "' " +
                     " ORDER BY 1";
            try
            {
                dsHoliday = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsHoliday;
        }
        public DataSet getHoli_Ed(string Holiday_Id)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsState = null;
            strQry = "SELECT Holiday_Id, Holiday_Name,Multiple_Date,Month, convert(varchar,Fixed_date,105) Fixed_date, Division_Code" +
                     " FROM HolidayList WHERE Holiday_Active_Flag=0 And Holiday_Id= '" + Holiday_Id + "'" +
                     " ORDER BY 2";
            try
            {
                dsState = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsState;
        }
        public int Delete_Holiday(int Holiday_Id)
        {
            int iReturn = -1;
            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "DELETE FROM  Holidaylist WHERE Holiday_Id = '" + Holiday_Id + "' ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;

        }
        public int Update_Inline_Holiday(int Holiday_Id, string Holiday_Name, string div_code)
        {
            int iReturn = -1;
            if (!RecordExist(Holiday_Name, Holiday_Id, div_code))
            {
                try
                {

                    DB_EReporting db = new DB_EReporting();
                    strQry = "Update Holidaylist set Holiday_Name = '" + Holiday_Name + "'" +
                   " where Holiday_Id=" + Holiday_Id + " and Holiday_Active_Flag=0 and Division_Code = '" + div_code + "'";



                    iReturn = db.ExecQry(strQry);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
                iReturn = -2;
            }
            return iReturn;

        }
        //Changes done by saravana
        public DataSet getHolidaysMGR(string state_code, string divcode, string stateName, string strYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsHoliday = null;

            strQry = "SELECT a.sl_no,a.Academic_Year, a.state_code, convert(varchar,a.Holiday_Date,105) Holiday_Date, a.Holiday_Name_Sl_No, a.Holiday_Name, a.Created_Date, a.Division_Code, b.StateName as State_Name" +
                   " FROM mas_state b join Mas_Statewise_Holiday_Fixation a " +
                   " on a.state_code like '" + state_code + ',' + "%'  or " +
                   " a.state_code like '%" + ',' + state_code + "' or" +
                   " a.state_code like '%" + ',' + state_code + ',' + "%' or a.state_code like '" + state_code + "'" +
                   " WHERE convert(varchar,b.state_code)='" + state_code + "' " +
                   " and a.Division_Code = '" + divcode + "' and a.Academic_Year='" + strYear + "' and b.StateName='" + stateName + "' " +
                   " ORDER BY month(Holiday_Date) ,day(Holiday_Date)";
            try
            {
                dsHoliday = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsHoliday;
        }

        public bool RecordExist(int Holiday_Id)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                //strQry = "SELECT COUNT(state_code) FROM mas_division WHERE state_code = '" + State_Code + "'  ";
                strQry = " SELECT COUNT(Sl_No) FROM Mas_Statewise_Holiday_Fixation WHERE Holiday_Name_Sl_No =   '" + Holiday_Id + "'";


                int iRecordExist = db.Exec_Scalar(strQry);

                if (iRecordExist > 0)
                    bRecordExist = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return bRecordExist;
        }

        public int DeActivateHoli(int Holiday_Id)
        {
            int iReturn = -1;
            if (!RecordExist(Holiday_Id))
            {
                try
                {

                    DB_EReporting db = new DB_EReporting();

                    strQry = "UPDATE Holidaylist " +
                                 " SET Holiday_Active_Flag=1 , " +
                                 " LastUpdt_Date = getdate() " +
                                 " WHERE Holiday_Id = '" + Holiday_Id + "' ";

                    iReturn = db.ExecQry(strQry);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
                iReturn = -2;
            }
            return iReturn;
        }
        //Changes done by Priya

        public DataTable get_Holidays_sort(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dsHoliday = null;

            strQry = "SELECT Holiday_Id,Holiday_Name,Multiple_Date,Fixed_date,Month,datename(month,dateadd(month, [Month] - 1, 0)) as MonthName" +
                     " FROM Holidaylist where Holiday_Active_Flag=0 and Division_Code = '" + div_code + "'" +
                     " order by Month";


            try
            {
                dsHoliday = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsHoliday;
        }
        public DataTable get_Holidays_sort(string sAlpha, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dsHoliday = null;
            strQry = "SELECT Holiday_Id,Holiday_Name,datename(month,dateadd(month, [Month] - 1, 0)) as MonthName " +
                     " FROM HolidayList " +
                     " where LEFT(Holiday_Name,1) = '" + sAlpha + "' and Holiday_Active_Flag=0 and Division_Code = '" + div_code + "'" +
                     " ORDER BY 1";
            try
            {
                dsHoliday = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsHoliday;
        }
        public bool RecordExistAdd(string Holiday_Name, string div_code)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(Holiday_Id) FROM Holidaylist WHERE Holiday_Name='" + Holiday_Name + "' and Division_Code = '" + div_code + "'";
                int iRecordExist = db.Exec_Scalar(strQry);

                if (iRecordExist > 0)
                    bRecordExist = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return bRecordExist;
        }

        public string getHolidayState(string divcode, string holidayid, string holiday_date)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsHoliday = null;
            string states = string.Empty;
            strQry = "SELECT  state_code" +
                     " FROM Mas_Statewise_Holiday_Fixation " +
                     " WHERE Holiday_Name_Sl_No= '" + holidayid + "' AND  Division_Code = '" + divcode + "' and convert(char(10),Holiday_Date,105)='" + holiday_date + "' ";

            try
            {
                dsHoliday = db_ER.Exec_DataSet(strQry);
                if (dsHoliday.Tables[0].Rows.Count > 0)
                {
                    states = dsHoliday.Tables[0].Rows[0][0].ToString();

                }
                else
                    states = "";
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return states;
        }

        //Changes done by Priya

        public int Update_HolidaySlNO(string Hol_Sl_No, string div_code, string Holiday_Id)
        {
            int iReturn = -1;
            //if (!sRecordExist(Div_Sl_No))
            //{

            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "UPDATE Holidaylist " +
                         " SET Holiday_SlNo = '" + Hol_Sl_No + "', " +
                         " LastUpdt_Date = getdate()  WHERE Division_Code = '" + div_code + "' and Holiday_Id='" + Holiday_Id + "' and Holiday_Active_Flag = 0 ";


                iReturn = db.ExecQry(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            //}
            //else
            //{
            //    iReturn = -2;
            //}


            return iReturn;


        }

        //New Holiday Mr

        public DataSet getHolidays_Mr(string state_code, string divcode, string year)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsHoliday = null;



            strQry = "SELECT a.sl_no,a.Academic_Year, a.state_code, convert(varchar,a.Holiday_Date,105) Holiday_Date, a.Holiday_Name_Sl_No, a.Holiday_Name, a.Created_Date, a.Division_Code, b.StateName as State_Name" +
                   " FROM mas_state b join Mas_Statewise_Holiday_Fixation a " +
                   " on a.state_code like '" + state_code + ',' + "%'  or " +
                   " a.state_code like '%" + ',' + state_code + "' or" +
                   " a.state_code like '%" + ',' + state_code + ',' + "%' or a.state_code like '" + state_code + "'" +
                   " WHERE convert(varchar,b.state_code)='" + state_code + "' and a.Academic_Year = '" + year + "'" +
                   " and a.Division_Code = '" + divcode + "' " +
                   " ORDER BY month(Holiday_Date) ,day(Holiday_Date)";
            try
            {
                dsHoliday = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsHoliday;
        }

        public int RecordAdd_Date(int year, string state_code, string holiday_date, string holiday_Name, string div_code, int hdnHolidayID, string lblMulti)
        {
            int iReturn = -1;

            try
            {
                DB_EReporting db = new DB_EReporting();
                int Sl_No = -1;
                strQry = "SELECT ISNULL(MAX(Sl_No),0)+1 FROM Mas_Statewise_Holiday_Fixation ";
                Sl_No = db.Exec_Scalar(strQry);

                if (lblMulti == "0")
                {
                    if (Holiday_RecordExist(div_code, holiday_Name, holiday_date))
                    {
                        // state_code += existingState;
                        if (state_code != "")
                        {
                            strQry = "UPDATE Mas_Statewise_Holiday_Fixation " +
                                 " SET Academic_Year = " + year + ", State_code = '" + state_code + "', Holiday_Date = '" + holiday_date.Substring(6, 4) + "-" + holiday_date.Substring(3, 2) + "-" + holiday_date.Substring(0, 2) + "' , " +
                                 " holiday_Name='" + holiday_Name + "',LastUpdt_Date = getdate() " +
                                 " WHERE Division_Code = '" + div_code + "' and Holiday_Name_Sl_No='" + hdnHolidayID + "' and convert(char(10),Holiday_Date,105)='" + holiday_date + "'  ";
                        }
                        else
                        {
                            strQry = "Delete Mas_Statewise_Holiday_Fixation WHERE Division_Code = '" + div_code + "' and Holiday_Name_Sl_No='" + hdnHolidayID + "' and convert(char(10),Holiday_Date,105)='" + holiday_date + "'";
                        }
                        //strQry = "EXEC SaveHolidayList '" + state_code + "', '" + holiday_date.Substring(6, 4) + "-" + holiday_date.Substring(3, 2) + "-" + holiday_date.Substring(0, 2) + "','" + hdnHolidayID + "','" + div_code + "' ";

                    }
                    else if (state_code != "")
                    {

                        strQry = "INSERT INTO Mas_Statewise_Holiday_Fixation(Sl_No,Academic_Year, State_code, Holiday_Date, Holiday_Name_Sl_No, Holiday_Name, Created_Date, Division_Code,LastUpdt_Date) " +
                                 " values( " + Sl_No + ", " + year + ", '" + state_code + "', '" + holiday_date.Substring(6, 4) + "-" + holiday_date.Substring(3, 2) + "-" + holiday_date.Substring(0, 2) + "', " + hdnHolidayID + ", '" + holiday_Name + "', GETDATE(), '" + div_code + "',getdate()) ";
                    }
                }
                else
                {
                    if (Holiday_SingleRecordExist(div_code, holiday_Name))
                    {
                        //   state_code += existingState;
                        if (state_code != "")
                        {
                            strQry = "UPDATE Mas_Statewise_Holiday_Fixation " +
                                 " SET Academic_Year = " + year + ", State_code = '" + state_code + "', Holiday_Date = '" + holiday_date.Substring(6, 4) + "-" + holiday_date.Substring(3, 2) + "-" + holiday_date.Substring(0, 2) + "' , " +
                                 " holiday_Name='" + holiday_Name + "',LastUpdt_Date = getdate() " +
                                 " WHERE Division_Code = '" + div_code + "' and Holiday_Name_Sl_No='" + hdnHolidayID + "'  ";
                        }
                        else
                        {
                            strQry = "Delete Mas_Statewise_Holiday_Fixation WHERE Division_Code = '" + div_code + "' and Holiday_Name_Sl_No='" + hdnHolidayID + "'  ";
                        }
                        //strQry = "EXEC SaveHolidayList '" + state_code + "', '" + holiday_date.Substring(6, 4) + "-" + holiday_date.Substring(3, 2) + "-" + holiday_date.Substring(0, 2) + "','" + hdnHolidayID + "','" + div_code + "' ";

                    }
                    else if (state_code != "")
                    {

                        strQry = "INSERT INTO Mas_Statewise_Holiday_Fixation(Sl_No,Academic_Year, State_code, Holiday_Date, Holiday_Name_Sl_No, Holiday_Name, Created_Date, Division_Code,LastUpdt_Date) " +
                                 " values( " + Sl_No + ", " + year + ", '" + state_code + "', '" + holiday_date.Substring(6, 4) + "-" + holiday_date.Substring(3, 2) + "-" + holiday_date.Substring(0, 2) + "', " + hdnHolidayID + ", '" + holiday_Name + "', GETDATE(), '" + div_code + "',getdate()) ";
                    }

                }
                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            //}
            //else
            //{
            //    iReturn = -2;
            //}
            return iReturn;
        }
        public DataSet getStateCode(string StateName)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet dsSF = null;
            strQry = " SELECT State_Code FROM  Mas_State " +
                     " WHERE StateName= '" + StateName + "' ";

            try
            {
                dsSF = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsSF;
        }

        public DataSet getHolidays_Consol(string state_code, string divcode, string year)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsHoliday = null;



            strQry = "SELECT  ROW_NUMBER() OVER (ORDER BY month(Holiday_Date),day(Holiday_Date)) AS [SlNo] ,  " +
                     " convert(varchar,a.Holiday_Date,105) as HolidayDate , LEFT(DATENAME(WEEKDAY,Holiday_Date),3) AS [Day],a.Holiday_Name as HolidayName, b.StateName " +

                   " FROM mas_state b join Mas_Statewise_Holiday_Fixation a " +
                   " on a.state_code like '" + state_code + ',' + "%'  or " +
                   " a.state_code like '%" + ',' + state_code + "' or" +
                   " a.state_code like '%" + ',' + state_code + ',' + "%' or a.state_code like '" + state_code + "'" +
                   " WHERE convert(varchar,b.state_code)='" + state_code + "' and a.Academic_Year = '" + year + "'" +
                   " and a.Division_Code = '" + divcode + "' " +
                   " ORDER BY month(Holiday_Date) ,day(Holiday_Date)";
            try
            {
                dsHoliday = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsHoliday;
        }


        public DataSet GetLeaveTypes(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsHoliday = null;



            strQry = "select Leave_code,Leave_Name from mas_leave_type where division_code='" + divcode + "'  and Active_Flag='0'";
            try
            {
                dsHoliday = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsHoliday;
        }


        public int AddLeavesFo(string DivCode, string SFCode, string LType, string FDate, string TDate, string CDate, string Reason, string NoofDay, string sf)
        {
            //int iReturn = -1;
            //try
            //{
            //    DB_EReporting db = new DB_EReporting();
            //    strQry = "insert into mas_leave_form(Leave_Id,Leave_Type,From_Date,To_Date,Reason,No_of_Days,Created_Date,sf_code,Division_Code,Leave_Active_Flag) " +
            //             " values((SELECT MAX( Leave_Id )+1 FROM mas_leave_form cust), '" + LType + "','" + FDate + "','" + TDate + "','" + Reason + "','" + NoofDay + "','" + CDate + "','" + SFCode + "','" + DivCode + "','0')";

            //    iReturn = db.ExecQry(strQry);
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
            //return iReturn;
            int iReturn = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();
                // DataSet dsWorkType = null;
                // DateTime Fdtgrn = DateTime.ParseExact(FDate, "dd/MM/yyyy", null);
                //string FDate1 = Fdtgrn.ToString("yyyy-MM-dd");


                // DateTime Tdtgrn = DateTime.ParseExact(TDate, "dd/MM/yyyy", null);
                //string TDate1 = Tdtgrn.ToString("yyyy-MM-dd");




                //   iReturn = db.ExecQry(strQry);

                //   for (DateTime date = Fdtgrn; date <= Tdtgrn; date += TimeSpan.FromDays(1))
                //   {
                //       strQry = "insert into TbMyDayPlan(sf_code,Pln_Date,remarks,Division_Code,wtype,FWFlg) " +
                //          " values('" + SFCode + "','" + date.ToString("yyyy-MM-dd") + "','" + Reason + "','" + DivCode + "','" + TypeCode + "','L')";
                //       iReturn = db.ExecQry(strQry);


                //       DataSet dsDcrMAin = null;
                //       strQry = "exec svDCRMain '','" + SFCode + "','" + sType + "','" + date.ToString("yyyy-MM-dd") + "','" + TypeCode + "','','" + DivCode + "','" + Reason + "','','' ";
                //       try
                //       {
                //           dsDcrMAin = db.Exec_DataSet(strQry);
                //       }
                //       catch (Exception ex)
                //       {
                //           throw ex;
                //       }
                //   }



                DataSet dsDcrMAin = null;

                if (sf != "Admin")
                {

                    strQry = "select sf_type from Mas_Salesforce where division_code='" + DivCode + ",' and sf_code='" + SFCode + "'";
                    Int32 sType = db.Exec_Scalar(strQry);

                    strQry = "select type_code from vwMas_WorkType_all where division_code = '" + DivCode + "'  and sftyp = '" + sType + "' and fwflg = 'l'";
                    Int32 TypeCode = db.Exec_Scalar(strQry);
                 
                    strQry = "insert into mas_leave_form(Leave_Id,Leave_Type,From_Date,To_Date,Reason,No_of_Days,Created_Date,sf_code,Division_Code,Leave_Active_Flag) " +
                             " values((SELECT isnull(MAX( Leave_Id ),0)+1 FROM mas_leave_form cust), '" + LType + "','" + FDate + "','" + TDate + "','" + Reason + "','" + NoofDay + "',getdate(),'" + SFCode + "','" + DivCode + "','2')";
                    iReturn = db.ExecQry(strQry);

                   // strQry = "update MasEntitlement set LeaveTaken = (LeaveTaken + cast('" + NoofDay + "' as int )) , LeaveAvailability = (LeaveAvailability - cast('" + NoofDay + "' as int )) where SFCode='" + SFCode + "' and LeaveCode='" + LType + "'";
                   // iReturn = db.ExecQry(strQry);
                }
                else
                {

                    strQry = "EXEC masLeaveInsert '" + DivCode + "','" + SFCode + "','" + LType + "','" + FDate + "','" + TDate + "','" + Reason + "','" + NoofDay + "'";
                    try
                    {
                        dsDcrMAin = db.Exec_DataSet(strQry);

                        if (dsDcrMAin.Tables[0].Rows.Count > 0)
                        {
                            iReturn = 1;
                        }
                        else
                        {
                            iReturn = 0;
                        }

                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }

                // iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }


        public int AddMasHolidays(string DivCode, string HolidayName, string Remarks, string FDate, string FYear, string HolidayId, string stateCode)
        {
            int iReturn = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();
                if (HolidayId == "0")
                {
                    int Holiday_Id = 0;
                    strQry = "SELECT HolidayID from Mas_Holiday_List where DivisionCode='" + DivCode + "' and HolidayName='" + HolidayName + "'";
                    DataSet dsho = db.Exec_DataSet(strQry);

                    if (dsho.Tables[0].Rows.Count > 0)
                    {
                        Holiday_Id = Convert.ToInt32(dsho.Tables[0].Rows[0][0]);
                    }
                    else
                    {
                        strQry = "SELECT isnull(max(HolidayID)+1,'1') HolidayID from Mas_Holiday_List";
                        Holiday_Id = db.Exec_Scalar(strQry);

                        strQry = "insert into Mas_Holiday_List(HolidayID,HolidayName,ActiveFlag,DivisionCode,CreateDate) " +
                             " values('" + Holiday_Id + "', '" + HolidayName + "','0','" + DivCode + "',getdate())";
                        iReturn = db.ExecQry(strQry);
                    }

                    HolidayId = Holiday_Id.ToString();
                }

                int listId = 0;
                strQry = "SELECT count(Slno)cnt from Mas_Holiday_Dates_Detail where DivisionCode='" + DivCode + "' and HolidayDate='" + FDate + "' and stateCode='" + stateCode + "'";
                listId = db.Exec_Scalar(strQry);
                if (listId > 0)
                {
                    strQry = "Update Mas_Holiday_Dates_Detail set HolidayName='" + HolidayName + "' , HolidayRemarks='" + Remarks + "', ActiveFlag='0' where DivisionCode='" + DivCode + "'and HolidayDate='" + FDate + "' and stateCode='" + stateCode + "'";

                    iReturn = db.ExecQry(strQry);
                }
                else
                {

                    strQry = "SELECT isnull(max(Slno)+1,'1') Slno from Mas_Holiday_Dates_Detail";
                    int Slnos = db.Exec_Scalar(strQry);
                    strQry = "insert into Mas_Holiday_Dates_Detail(Slno,HolidayDate,HolidayID,HolidayName,HolidayRemarks,CreateDate,DivisionCode,ActiveFlag,stateCode) " +
                         " values('" + Slnos + "', '" + FDate + "','" + HolidayId + "','" + HolidayName + "','" + Remarks + "',getdate(),'" + DivCode + "','0','" + stateCode + "')";
                    iReturn = db.ExecQry(strQry);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }

        public DataSet GetHolidaysNew(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet dsHoliday = null;
            strQry = "select HolidayID,HolidayName from Mas_Holiday_List where DivisionCode='" + divcode + "'  and ActiveFlag='0'";
            try
            {
                dsHoliday = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsHoliday;
        }

        public DataSet GetProdNew(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet dsHoliday = null;
            strQry = "select Product_Detail_Code,Product_Detail_Name from Mas_Product_Detail where Division_Code='" + divcode + "'  and Product_Active_Flag='0'";
            try
            {
                dsHoliday = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsHoliday;
        }

        public DataSet GetHolidaysDataNew(string divcode, string FYear, string stateCode)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet dsHoliday = null;
            strQry = "select HolidayDate,HolidayID,HolidayName,HolidayRemarks,stateCode from Mas_Holiday_Dates_Detail where DivisionCode='" + divcode + "'  and ActiveFlag='0' and stateCode='" + stateCode + "' order by HolidayDate ";
            try
            {
                dsHoliday = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsHoliday;
        }


        public DataSet GetLeaveType(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet dsHoliday = null;
            strQry = "select Leave_code,Leave_Name,Leave_SName from mas_Leave_Type where Division_Code='" + divcode + "' and Active_Flag='0'";
            try
            {
                dsHoliday = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsHoliday;
        }



        public int Insert_LeaveEligibility_Data(string div_code, string sf_code, string des_code, string LeaveCode, string LeaveValue, string year)
        {
            int iReturn = -1;
            string strSfCode = string.Empty;
            DB_EReporting db = new DB_EReporting();
            try
            {
                strQry = "exec Insert_LeaveEligibility '" + div_code + "','" + sf_code + "','" + LeaveCode + "','" + des_code + "','" + LeaveValue + "','" + year + "'";
                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            if (iReturn > 0)
            {
                return iReturn;
            }
            else
            {
                return 0;
            }
        }

        public DataSet get_Leave_Values(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            strQry = "select * from MasEntitlement " +
                     " where DivisionCode ='" + div_code + "'";
            try
            {
                dsDivision = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsDivision;
        }

        public DataSet LeaveCheck(string SFCode, string FYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            strQry = "select SFCode,LeaveCode,LeaveValue,LeaveAvailability,LeaveTaken,Leave_SName,Leave_Name from MasEntitlement  inner join mas_Leave_Type l on Leave_code=LeaveCode where SFCode='" + SFCode + "' and year(createDate)='" + FYear + "'";
            try
            {
                dsDivision = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsDivision;
        }

        public DataSet GetLvlValidate(string sfCode, string fDate, string tDate, string lCode,string sfAdmin="0")
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsHoliday = null;



            strQry = "exec iOS_getLvlValidate '" + sfCode + "','" + fDate + "','" + tDate + "','" + lCode + "','" + sfAdmin + "'";
            try
            {
                dsHoliday = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsHoliday;
        }

        public int LeaveApprovalforMGR(string DivCode, string SFCode, string LCode, string Mode, string Reason = "")
        {

            int iReturn = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();

                DataSet dsDcrMAin = null;
                strQry = "EXEC masLeaveApproval '" + DivCode + "','" + SFCode + "','" + LCode + "','" + Mode + "','" + Reason + "'";
                try
                {
                    dsDcrMAin = db.Exec_DataSet(strQry);

                    if (dsDcrMAin.Tables[0].Rows.Count > 0)
                    {
                        iReturn = 1;
                    }
                    else
                    {
                        iReturn = 0;
                    }

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }
        public DataSet GetLeaveSFWise(string DivCode, string sfCode, string fYear, string subDiv)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet dsHoliday = null;
            strQry = "exec GET_Leave_All '" + DivCode + "','" + sfCode + "','" + fYear + "','" + subDiv + "'";
            try
            {
                dsHoliday = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsHoliday;
        }
        public int LeaveApprovalCancel(string LCode)
        {
            int iReturn = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();

                DataSet dsDcrMAin = null;
                strQry = "EXEC LeaveCancelApprove '" + LCode + "'";
                try
                {
                    dsDcrMAin = db.Exec_DataSet(strQry);
                    if (dsDcrMAin.Tables[0].Rows.Count > 0)
                    {
                        iReturn = 1;
                    }
                    else
                    {
                        iReturn = 0;
                    }

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }


    }
}
