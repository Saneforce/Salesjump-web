using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DBase_EReport;
using System.Configuration;
using System.Net;
using System.Web;
namespace Bus_EReport
{
    public class Expense
    {
        private string strQry = string.Empty;

        #region tsr
        public DataSet getExpenseDetailsname(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsState = null;
            // strQry = "exec productratewise '" + div_code  + "','" + Sub_Div_Code + "'";

            int divcode = 0;

            if (div_code == "" || div_code == null)
            { divcode = 0; }
            else { divcode = Convert.ToInt32(div_code); }

            strQry = " Select expCode,expName from Trans_Daily_User_Expense where Division_Code= " + divcode + "  ";
            strQry += " Group by expCode,expName Order By expName asc   ";

            //strQry = "select Product_Detail_Code,Product_Short_Name,Product_Detail_Name from Mas_Product_Detail where Division_Code='" + div_code + "' and Product_Active_Flag='0' ";
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

        public DataSet Get_ExpenseClaim(string div_code, string sfcode, string Fdate, string Tdate, string stcode = "0")
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            int divcode = 0;

            if (div_code == "" || div_code == null)
            {
                divcode = 0;
            }
            else { divcode = Convert.ToInt32(div_code); }

            strQry = "EXEC get_TSR_ExpenseClaimReport " + divcode + ",'" + sfcode + "','" + Fdate + "','" + Tdate + "','" + stcode + "'";

            try
            {
                dsAdmin = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsAdmin;
        }


        public DataSet GetTsrUserExpense(string div_code, string sfcode, string Fdate, string Tdate, string stcode = "0")
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;

            int divcode = 0;

            if (div_code == "" || div_code == null)
            { divcode = 0; }
            else { divcode = Convert.ToInt32(div_code); }

            if (sfcode == "" || sfcode == null)
            { sfcode = "admin"; }

            if (Fdate == "" || Fdate == null)
            { Fdate = Convert.ToString(DateTime.Now.Date); }

            if (Tdate == "" || Tdate == null)
            { Tdate = Convert.ToString(DateTime.Now.Date); }

            if (stcode == "" || stcode == null)
            { stcode = "0"; }

            strQry = "exec Get_Tsr_UserExpenseDet  " + divcode + ",'" + sfcode + "','" + Fdate + "','" + Tdate + "','" + stcode + "'";

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

        public DataSet getTSRrptIssueSlip_Month_brand1(string div_code, string sf_code, string Fdate, string Tdate, string sub_Div_Code, string stcode = "0", string distcode = "0")
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            strQry = "exec [GET_IssuSlip_Month_Brand1] '" + div_code + "','" + sf_code + "','" + Fdate + "','" + Tdate + "','" + sub_Div_Code + "','" + stcode + "','" + distcode + "'";
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


        #endregion 
        public DataSet GetExpenseDetails(string SF, string Mn, string Yr,string EmpID)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            strQry = "exec GetNewExpenseDet '" + SF + "','" + Mn + "','" + Yr + "','" + EmpID + "'";
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

        public DataSet getrptIssueSlip_Month_PQ(string div_code, string sf_code, string Fdate, string Tdate, string sub_Div_Code, string stcode = "0", string distcode = "0")
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            strQry = "exec [GET_IssuSlip_Month_pqv] '" + div_code + "','" + sf_code + "','" + Fdate + "','" + Tdate + "','" + sub_Div_Code + "','" + stcode + "','" + distcode + "'";
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

        

        public DataSet getrptIssueSlip_Month_brand1(string div_code, string sf_code, string Fdate, string Tdate, string sub_Div_Code, string stcode = "0", string distcode = "0")
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            strQry = "exec [GET_IssuSlip_Month_Brand1] '" + div_code + "','" + sf_code + "','" + Fdate + "','" + Tdate + "','" + sub_Div_Code + "','" + stcode + "','" + distcode + "'";
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


        public DataSet GetUserExpense(string SF, string Mn, string Yr)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            strQry = "exec GetUserExpenseDet '" + SF + "','" + Mn + "','" + Yr + "'";
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
        public int AutoExpenseNd(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            int dsDivision;
            strQry = "select cast(isnull(Exp_Web_auto,0)as int) from Access_Master where division_code='" + divcode + "'";
            try
            {
                dsDivision = db_ER.Exec_Scalar(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsDivision;
        }
        public DataSet GetUserDAilyAdditional(string SF, string Mn, string Yr, string EmpID)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            strQry = "exec getExpDailAdd '" + SF + "','" + Mn + "','" + Yr + "','" + EmpID + "'";
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
        public DataSet GetUserExpAdditional(string SF, string Mn, string Yr, string EmpID)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            strQry = "exec getExpAdditional '" + SF + "'," + Mn + "," + Yr + ",'" + EmpID + "'";
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
        public DataSet GetAllowanceDets(string sf_code, string Mn, string Yr)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet Alws = null;
            strQry = "exec GetAllowanceDets '" + sf_code + "','" + Mn + "','" + Yr + "'";
            try
            {
                Alws = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Alws;
        }



        public DataSet getEmptyTerritory()
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            strQry = " SELECT TOP 10 '' Territory_Name,'' Territory_SName " +
                     " FROM  sys.tables ";
            try
            {
                dsProCat = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProCat;
        }

        public int DeActivate(string terr_code)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "UPDATE Mas_Territory_Creation " +
                            " SET territory_active_flag=1  " +
                            " WHERE Territory_Code = '" + terr_code + "' ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;

        }

        public int RecordUpdate(string Territory_Code, string Territory_Name, string Territory_SName, string Territory_Type)
        {
            int iReturn = -1;
            //if (!RecordExist(div_code, div_sname))
            //{
            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "UPDATE Mas_Territory_Creation " +
                         " SET Territory_Name = '" + Territory_Name + "', " +
                         " Territory_Cat = '" + Territory_Type + "', " +
                         " Territory_SName = '" + Territory_SName + "' " +
                         " WHERE Territory_Code = '" + Territory_Code + "' ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            //}
            //else
            //{
            //    iReturn = -3;
            //}
            return iReturn;

        }


        public int TransferTerritory(string Territory_Code, string Target_Territory)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "UPDATE Mas_Chemists " +
                            " SET Territory_Code = '" + Target_Territory + "'  " +
                            " WHERE Territory_Code = '" + Territory_Code + "'";

                iReturn = db.ExecQry(strQry);

                if (iReturn != -1)
                {
                    strQry = "UPDATE Mas_ListedDr " +
                                " SET Territory_Code = '" + Target_Territory + "'  " +
                                " WHERE Territory_Code = '" + Territory_Code + "' ";

                    iReturn = db.ExecQry(strQry);

                    if (iReturn != -1)
                    {
                        strQry = "UPDATE Mas_UnListedDr " +
                                    " SET Territory_Code = '" + Target_Territory + "'  " +
                                    " WHERE Territory_Code = '" + Territory_Code + "' ";

                        iReturn = db.ExecQry(strQry);

                        if (iReturn != -1)
                        {
                            strQry = "UPDATE Mas_Hospital " +
                                        " SET Territory_Code = '" + Target_Territory + "'  " +
                                        " WHERE Territory_Code = '" + Territory_Code + "' ";

                            iReturn = db.ExecQry(strQry);

                            if (iReturn != -1)
                            {
                                strQry = "UPDATE Mas_Territory_Creation " +
                                            " SET territory_active_flag=1  " +
                                            " WHERE Territory_Code = '" + Territory_Code + "' ";

                                iReturn = db.ExecQry(strQry);
                            }


                        }

                    }

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;

        }




        public DataSet getTerritory(string sf_code, string terr_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTerr = null;

            strQry = " SELECT '-1' Territory_Code, '---Select---' Territory_Name " +
                     " UNION " +
                     " SELECT Territory_Code, Territory_Name " +
                     " FROM  Mas_Territory_Creation " +
                     " where Sf_code = '" + sf_code + "' " +
                     " AND territory_code != '" + terr_code + "' AND territory_active_flag=0 ";
            try
            {
                dsTerr = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsTerr;
        }

        public DataSet getTerritory(string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTerr = null;

            strQry = " SELECT a.Territory_Code, a.Territory_Name, a.Territory_SName, " +
                     " case when a.Territory_Cat=1 then 'HQ' " +
                     " else case when a.Territory_Cat=2 then 'EX' " +
                     " else case when a.Territory_Cat=3 then 'OS' " +
                     " else 'OS-EX' " +
                     " end end end as Territory_Cat, " +
                     //   " (select COUNT(b.ListedDrCode) from Mas_ListedDr b where a.Territory_Code=b.Territory_Code " +
                     " (select COUNT(b.ListedDrCode) from Mas_ListedDr b where b.Territory_Code = cast(a.Territory_Code as varchar ) " +
                     " and b.ListedDr_Active_Flag=0) ListedDR_Count, " +
                     " (select COUNT(b.Chemists_Code) from Mas_Chemists b where a.Territory_Code=b.Territory_Code " +
                     " and b.Chemists_Active_Flag=0) Chemists_Count, " +
                     " (select COUNT(b.Hospital_Code) from Mas_Hospital b where a.Territory_Code=b.Territory_Code " +
                     " and b.Hospital_Active_Flag =0) Hospital_Count, " +
                     " (select COUNT(b.UnListedDrCode) from Mas_UnListedDr b where a.Territory_Code=b.Territory_Code " +
                     " and b.UnListedDr_Active_Flag=0) UnListedDR_Count " +
                     " FROM  Mas_Territory_Creation a where a.Sf_Code = '" + sf_code + "'  and a.Territory_Active_Flag=0" +
                     " order by Territory_SNo";
            try
            {
                dsTerr = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsTerr;
        }

        // Sorting For Territory Creation

        public DataTable getTerritory_DataTable(string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dtTerr = null;

            strQry = " SELECT a.Territory_Code, a.Territory_Name, a.Territory_SName, " +
                     " case when a.Territory_Cat=1 then 'HQ' " +
                     " else case when a.Territory_Cat=2 then 'EX' " +
                     " else case when a.Territory_Cat=3 then 'OS' " +
                     " else 'OS-EX' " +
                     " end end end as Territory_Cat, " +
                     " (select COUNT(b.ListedDrCode) from Mas_ListedDr b where a.Territory_Code=b.Territory_Code " +
                     " and b.ListedDr_Active_Flag=0) ListedDR_Count, " +
                     " (select COUNT(b.Chemists_Code) from Mas_Chemists b where a.Territory_Code=b.Territory_Code " +
                     " and b.Chemists_Active_Flag=0) Chemists_Count, " +
                     " (select COUNT(b.Hospital_Code) from Mas_Hospital b where a.Territory_Code=b.Territory_Code " +
                     " and b.Hospital_Active_Flag =0) Hospital_Count, " +
                     " (select COUNT(b.UnListedDrCode) from Mas_UnListedDr b where a.Territory_Code=b.Territory_Code " +
                     " and b.UnListedDr_Active_Flag=0) UnListedDR_Count " +
                     " FROM  Mas_Territory_Creation a where a.Sf_Code = '" + sf_code + "' AND a.territory_active_flag=0 ";
            try
            {
                dtTerr = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtTerr;
        }
        // Sorting For Territory List 
        public DataTable getTerritorylist_DataTable(string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dtTerr = null;

            strQry = " SELECT a.Territory_Code, a.Territory_Name, a.Territory_SName, " +
                     " case when a.Territory_Cat=1 then 'HQ' " +
                     " else case when a.Territory_Cat=2 then 'EX' " +
                     " else case when a.Territory_Cat=3 then 'OS' " +
                     " else 'OS-EX' " +
                     " end end end as Territory_Cat, " +
                     " (select COUNT(b.ListedDrCode) from Mas_ListedDr b where convert(varchar(10),a.Territory_Code)=b.Territory_Code " +
                     " and b.ListedDr_Active_Flag=0) ListedDR_Count, " +
                     " (select COUNT(b.Chemists_Code) from Mas_Chemists b where a.Territory_Code=b.Territory_Code " +
                     " and b.Chemists_Active_Flag=0) Chemists_Count, " +
                     " (select COUNT(b.Hospital_Code) from Mas_Hospital b where a.Territory_Code=b.Territory_Code " +
                     " and b.Hospital_Active_Flag =0) Hospital_Count, " +
                     " (select COUNT(b.UnListedDrCode) from Mas_UnListedDr b where a.Territory_Code=b.Territory_Code " +
                     " and b.UnListedDr_Active_Flag=0) UnListedDR_Count " +
                     " FROM  Mas_Territory_Creation a where a.Sf_Code = '" + sf_code + "' AND a.territory_active_flag=0 ";
            try
            {
                dtTerr = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtTerr;
        }
        //Sorting for G.No
        public DataTable getTerr_DtTable(string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dtTerr = null;

            strQry = " SELECT Territory_Code, Territory_Name, Territory_SName, " +
                     " case when Territory_Cat=1 then 'HQ' " +
                     " else case when Territory_Cat=2 then 'EX' " +
                     " else case when Territory_Cat=3 then 'OS' " +
                     " else 'OS-EX' " +
                     " end end end as Territory_Cat " +
                     " FROM  Mas_Territory_Creation where Sf_Code = '" + sf_code + "' AND territory_active_flag=0 ";
            try
            {
                dtTerr = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtTerr;
        }

        //End

        public DataSet getTerritory_Det(string sf_code, string Territory_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTerr = null;

            strQry = " SELECT Territory_Code, Territory_Name, Territory_SName, " +
                     " case when Territory_Cat=1 then 'HQ' " +
                     " else case when Territory_Cat=2 then 'EX' " +
                     " else case when Territory_Cat=3 then 'OS' " +
                     " else 'OS-EX' " +
                     " end end end as Territory_Cat,Alias_Name " +
                     " FROM  Mas_Territory_Creation where Sf_Code = '" + sf_code + "' AND Territory_Code='" + Territory_Code + "' and territory_active_flag=0 ";
            try
            {
                dsTerr = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsTerr;
        }
        public int getTerritory_Al(string Territory_Code, string Alias_Name, string Territory_SName)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "UPDATE Mas_Territory_Creation " +
                             " SET Alias_Name='" + Alias_Name + "', Territory_SName='" + Territory_SName + "' " +
                             " WHERE Territory_Code = '" + Territory_Code + "' and territory_active_flag=0  ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;
        }

        public int Terr_SlNO(string Territory_Code, string Sno)
        {
            int iReturn = -1;
            //if (!RecordExist(Product_Cat_Code, Product_Cat_SName))
            //{
            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "UPDATE Mas_Territory_Creation " +
                         " SET Territory_SNo = '" + Sno + "', " +
                        " LastUpdt_Date = getdate() " +
                         " WHERE Territory_Code = '" + Territory_Code + "' ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }
        public DataSet getTerritory_Slno(string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTerr = null;

            strQry = " SELECT Territory_Code, Territory_Name, Territory_SName, " +
                     " case when Territory_Cat=1 then 'HQ' " +
                     " else case when Territory_Cat=2 then 'EX' " +
                     " else case when Territory_Cat=3 then 'OS' " +
                     " else 'OS-EX' " +
                     " end end end as Territory_Cat " +
                     " FROM  Mas_Territory_Creation where Sf_Code = '" + sf_code + "' AND territory_active_flag=0 " +
                     " ORDER BY Territory_SNo"; ;
            try
            {
                dsTerr = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsTerr;
        }

        //Changes done by Saravana     

        //public DataSet getWorkAreaName()
        //{
        //    DB_EReporting db_ER = new DB_EReporting();

        //    DataSet dsProCat = null;

        //    strQry = "select wrk_area_Name from Admin_Setups";
        //    try
        //    {
        //        dsProCat = db_ER.Exec_DataSet(strQry);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    return dsProCat;
        //}
        //End
        public bool RecordExist(string Territory_Name, string sf_code)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(Territory_Name) FROM Mas_Territory_Creation WHERE Territory_Name='" + Territory_Name + "' and sf_code = '" + sf_code + "'";
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

        public int RecordAdd(string Territory_Name, string Territory_SName, string Territory_Type, string sf_code)
        {
            int iReturn = -1;

            if (!RecordExist(Territory_Name, sf_code))
            {
                try
                {

                    DB_EReporting db = new DB_EReporting();

                    int Division_Code = -1;
                    int Territory_Code = -1;

                    strQry = "select division_code from Mas_Salesforce_AM where Sf_Code = '" + sf_code + "' ";
                    Division_Code = db.Exec_Scalar(strQry);

                    //strQry = "SELECT ISNULL(MAX(Territory_Code),0)+1 FROM Mas_Territory_Creation WHERE Division_Code = '" + Division_Code + "' ";
                    strQry = "SELECT ISNULL(MAX(Territory_Code),0)+1 FROM Mas_Territory_Creation ";
                    Territory_Code = db.Exec_Scalar(strQry);

                    strQry = "insert into Mas_Territory_Creation (Territory_Code,Territory_Name,Territory_Cat,Territory_Sname,division_code, " +
                             " SF_Code,Territory_Active_Flag,Created_date) " +
                             " VALUES('" + Territory_Code + "', '" + Territory_Name + "', '" + Territory_Type + "', '" + Territory_SName + "', " +
                             " '" + Division_Code + "', '" + sf_code + "', 0, getdate())";

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
        public int RecordCount(string sf_code)
        {
            int iReturn = -1;

            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(Territory_Code) FROM Mas_Territory_Creation WHERE Sf_Code = '" + sf_code + "' and Territory_Active_Flag = 0";

                iReturn = db.Exec_Scalar(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }
        //Changes done by Saravanan
        public DataSet getTerrritoryView(string Sf_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            strQry = " select SF_Name,Sf_HQ from Mas_Salesforce where Sf_Code='" + Sf_Code + "'";

            try
            {
                dsProCat = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProCat;
        }
        //Changes done by Saravana     

        public DataSet getWorkAreaName(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            strQry = "select wrk_area_Name,No_of_TP_View,wrk_area_SName from Admin_Setups where Division_Code='" + div_code + "'";

            try
            {
                dsProCat = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProCat;
        }
        public DataSet getWorkAreaName()
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            strQry = "select wrk_area_Name,No_of_TP_View,wrk_area_SName from Admin_Setups ";

            try
            {
                dsProCat = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProCat;
        }
        //End
        public DataSet getSFCode(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            strQry = " SELECT '' as sf_code, '---Select---' as Sf_Name " +
                     " UNION " +
                     " select sf_code,Sf_Name from Mas_Salesforce where (Division_Code like '" + div_code + ',' + "%'  or " +
                      " Division_Code like '%" + ',' + div_code + ',' + "%') " +
                     " and sf_type=1 ";
            try
            {
                dsProCat = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProCat;
        }

        public DataSet getOSEXCondn(string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            strQry = " select T.Territory_Code FCode,T.Territory_Name FName, case when T.Territory_Cat=1 then 'HQ' " +
                       " else case when T.Territory_Cat=2 then 'EX' " +
                       " else case when T.Territory_Cat=3 then 'OS' " +
                       " else 'OS-EX' " +
                        " end end end as Fcat,T1.Territory_Name TName, " +
          " T1.Territory_Code TCode, case when T1.Territory_Cat=1 then 'HQ' " +
                       " else case when T1.Territory_Cat=2 then 'EX' " +
                       " else case when T1.Territory_Cat=3 then 'OS' " +
                      " else 'OS-EX' " +
                       "  end end end as Tcat, '' Distance " +
         " from Mas_Territory_Creation  As T,Mas_Territory_Creation  As T1 " +
          " where T.sf_code='" + sf_code + "'  and T.Territory_Cat='3' AND  " +
         " t1.Territory_Code != t.Territory_Code and T1.Territory_Cat='4' and T1.sf_code='" + sf_code + "'" +
          " AND t.Territory_Active_Flag='0' " +
         " AND t1.Territory_Active_Flag='0' order by T.Territory_Name ";
            try
            {
                dsProCat = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProCat;
        }

        public DataSet getOSEXDistCondn(string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            strQry = " Select Sf_Code,From_Code,To_Code,Distance_in_kms as Distance from Mas_Distance_Fixation Where Sf_Code='" + sf_code + "'";
            try
            {
                dsProCat = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProCat;
        }
        public DataSet getHQ_Dist(string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTerr = null;

            strQry = " SELECT a.Territory_Code, a.Territory_Name, a.Territory_SName, " +
                       " case when a.Territory_Cat=1 then 'HQ' " +
                       " else case when a.Territory_Cat=2 then 'EX' " +
                       " else case when a.Territory_Cat=3 then 'OS' " +
                       " else 'OS-EX' " +
                       " end end end as Territory_Cat, " +
                       " (select COUNT(b.ListedDrCode) from Mas_ListedDr b where b.Territory_Code= CAST(a.Territory_Code as varchar) " +
                       " and b.ListedDr_Active_Flag=0) ListedDR_Count  " +
                       " FROM  Mas_Territory_Creation a where a.Sf_Code = '" + sf_code + "' and  Territory_Cat=1  and a.Territory_Active_Flag=0" +

                       " order by Territory_Name";
            try
            {
                dsTerr = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsTerr;
        }
        public DataSet getDist(string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTerr = null;

            strQry = "select From_Code,Sf_HQ,To_Code,Territory_Name,Territory_Cat,Territory_Code,town_cat,Distance_In_Kms Distance,m.sf_code " +
                " from Mas_Territory_Creation m inner join Mas_Salesforce S on S.Sf_Code=m.SF_Code " +
                "left outer join Mas_Distance_Fixation d on m.SF_Code=d.SF_Code and m.territory_code=d.to_code " +
                " where m.SF_Code='" + sf_code + "'";


            try
            {
                dsTerr = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsTerr;
        }
        public DataSet getDist1(string sf_code, int cat)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTerr = null;

            strQry = " SELECT a.Territory_Code, a.Territory_Name,  " +
                     " case when a.Territory_Cat=1 then 'HQ' " +
                     " else case when a.Territory_Cat=2 then 'EX' " +
                     " else case when a.Territory_Cat=3 then 'OS' " +
                     " else 'OS-EX' " +
                     " end end end as Territory_Cat, Distance_In_Kms Distance,c.Sf_HQ,a.sf_code,Territory_Cat " +
                     " FROM  Mas_Territory_Creation a, Mas_Salesforce c,Mas_Distance_Fixation b where a.Sf_Code = '" + sf_code + "' and Territory_Cat=" + cat + "  and a.Territory_Active_Flag=0" +
                      " and a.Sf_Code=c.Sf_Code and a.SF_Code=b.SF_Code and a.Territory_Code=b.To_Code" +
                     " order by Territory_Name";
            Console.WriteLine(strQry);
            try
            {
                dsTerr = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsTerr;
        }
        public DataSet getOS_Dist(string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTerr = null;

            strQry = " SELECT a.Territory_Code, a.Territory_Name,  " +
                     " case when a.Territory_Cat=1 then 'HQ' " +
                     " else case when a.Territory_Cat=2 then 'EX' " +
                     " else case when a.Territory_Cat=3 then 'OS' " +
                     " else 'OS-EX' " +
                     " end end end as Territory_Cat,a.OS_Distance,c.Sf_HQ " +
                     //" (select COUNT(b.ListedDrCode) from Mas_ListedDr b where b.Territory_Code= CAST(a.Territory_Code as varchar) " +
                     //" and b.ListedDr_Active_Flag=0) ListedDR_Count " +
                     " FROM  Mas_Territory_Creation a, Mas_Salesforce c where a.Sf_Code = '" + sf_code + "' and Territory_Cat=3  and a.Territory_Active_Flag=0" +
                      " and a.Sf_Code=c.Sf_Code " +
                     " order by Territory_Name";
            try
            {
                dsTerr = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsTerr;
        }
        public DataSet getEX_Dist(string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTerr = null;

            strQry = " SELECT a.Territory_Code, a.Territory_Name,a.sf_code,  " +
                     " case when a.Territory_Cat=1 then 'HQ' " +
                     " else case when a.Territory_Cat=2 then 'EX' " +
                     " else case when a.Territory_Cat=3 then 'OS' " +
                     " else 'OS-EX' " +
                     " end end end as Territory_Cat,a.Ex_Distance, c.Sf_HQ" +
                     //" (select COUNT(b.ListedDrCode) from Mas_ListedDr b where b.Territory_Code= CAST(a.Territory_Code as varchar) " +
                     //" and b.ListedDr_Active_Flag=0) ListedDR_Count " +
                     " FROM  Mas_Territory_Creation a, Mas_Salesforce c where a.Sf_Code = '" + sf_code + "' and Territory_Cat=2  and a.Territory_Active_Flag=0" +
                      " and a.Sf_Code=c.Sf_Code " +
                     " order by Territory_Name";
            try
            {
                dsTerr = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsTerr;
        }

        public DataSet getOSEX_Dist(string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTerr = null;

            //strQry = " SELECT * FROM( SELECT a.Territory_Code, a.Territory_Name, a.Territory_SName,case when a.Territory_Cat=1 then 'HQ' "+ 
            //         " else case when a.Territory_Cat=2 then 'EX'  else case when a.Territory_Cat=3 then 'OS' "+
            //          " else 'OSEX'  end end end as Territory_Cat,  (select COUNT(b.ListedDrCode) "+                      
            //           " from Mas_ListedDr b where a.Territory_Code=b.Territory_Code  and "+                       
            //           " b.ListedDr_Active_Flag=0) ListedDR_Count   FROM  Mas_Territory_Creation a where a.Sf_Code = '" + sf_code + "'" +                
            //           " and  (Territory_Cat=3 or Territory_Cat=4) and a.Territory_Active_Flag=0 ) as a PIVOT ( max(a.Territory_Name)  FOR [Territory_Cat] IN (OS,OSEX)) AS pivot1";

            strQry = ";with terr as (select * from Mas_Territory_Creation where Territory_Cat=3 and Territory_Active_Flag=0),terr1 as" +
                   " (select * from Mas_Territory_Creation where Territory_Cat=4 and Territory_Active_Flag=0) select f.SF_Code,f.Territory_Name as Terr_From,e.Territory_Name as Terr_To,e.Distance from terr1 e, " +
                   " terr f where f.SF_Code='" + sf_code + "' and e.sf_code='" + sf_code + "'  ";


            try
            {
                dsTerr = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsTerr;
        }

        public int get_ExKms(string sf_code, string EX_Distance, string Territory_Name)
        {
            int iReturn = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "UPDATE Mas_Territory_Creation " +
                         " SET Distance = '" + EX_Distance + "' " +
                         " WHERE Sf_Code = '" + sf_code + "' and Territory_Name='" + Territory_Name + "'  ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }
        public int get_OsKms(string sf_code, string OS_Distance, string Territory_Name)
        {
            int iReturn = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "UPDATE Mas_Territory_Creation " +
                         " SET Distance = '" + OS_Distance + "' " +
                         " WHERE Sf_Code = '" + sf_code + "' and Territory_Name='" + Territory_Name + "'  ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }
        public int get_OsExKms(string sf_code, string OSEX_Distance)
        {
            int iReturn = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "UPDATE Mas_Territory_Creation " +
                         " SET Distance = '" + OSEX_Distance + "' " +
                         " WHERE Sf_Code = '" + sf_code + "'   ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }

        //Changes done by Priya

        public DataSet getSfname_Desig(string Sf_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            strQry = " select a.sf_name,a.Sf_HQ,a.sf_emp_id,b.Designation_Name,c.Division_Name " +
                     " from Mas_Salesforce a,Mas_SF_Designation b, Mas_Division c where Sf_Code= '" + Sf_Code + "'" +
                     " and a.Designation_Code=b.Designation_Code ";

            try
            {
                dsProCat = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProCat;
        }
        // Changes done by Saravanan 20/03/15
        public int WrkTypeMGR_Expense_Update(string Worktype_Name, string Expense_Type)
        {
            int iReturn = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "Update Mas_WorkType_Mgr set Expense_Type='" + Expense_Type + "' where Worktype_Name_M='" + Worktype_Name + "'";

                iReturn = db.ExecQry(strQry);

            }
            catch (Exception ex)
            {

            }
            return iReturn;
        }

        public int ExpenseParameter_Insert(string Exp_Parameter_Name, string Exp_Type, string Div_Code, string Level_Value, string Level_Code, string lblWorktype_Code)
        {
            int iReturn = -1;
            if (!ExpParameter_RecordExist(Exp_Parameter_Name))
            {
                try
                {
                    DB_EReporting db = new DB_EReporting();

                    int Parameter_Code = 0;
                    int ExpensePara_Orby_Code = 0;
                    int Count = 0;
                    int InsertColumn = 0;

                    strQry = "select isnull(max(Expense_Parameter_Code),0)+1 Expense_Parameter_Code from Mas_Expense_Parameter";
                    Parameter_Code = db.Exec_Scalar(strQry);

                    strQry = "select isnull(max(ExpensePara_OrderBy_Code),0)+1 Expense_Parameter_Code from Mas_Expense_Parameter";
                    ExpensePara_Orby_Code = db.Exec_Scalar(strQry);

                    strQry = "Insert into Mas_Expense_Parameter (Expense_Parameter_Code,ExpensePara_OrderBy_Code," +
                             "Expense_Parameter_Name,Division_Code,Expense_Type," + Level_Code + ") values('" + Parameter_Code + "','" + ExpensePara_Orby_Code + "'," +
                             "'" + Exp_Parameter_Name + "','" + Div_Code + "','" + Exp_Type + "','" + Level_Value + "')";


                    iReturn = db.ExecQry(strQry);


                    //DataSet dsTerr = null;
                    //DataTable dt=null;
                    //strQry =  "SELECT REPLACE(LTRIM(RTRIM(Expense_Parameter_Name)), SPACE(1), '_' ) Expense_Parameter_Name "+
                    //          " FROM Mas_Expense_Parameter where Expense_Type=1 and Expense_Parameter_Name='" + Exp_Parameter_Name + "' ";
                    //dsTerr = db.Exec_DataSet(strQry);
                    //dt = dsTerr.Tables[0];

                    //for (int i = 0; i < dsTerr.Tables[0].Rows.Count; i++)
                    //{
                    //    //string strColumn = dt.Rows[i].Field<string>(0);
                    //    string strColumn = "Fixed_Column"+Convert.ToString(i);

                    //    //strQry = "ALTER TABLE Mas_Allowance_Fixation DROP COLUMN"+strColumn+"";
                    //    //InsertColumn = db.ExecQry(strQry);

                    //    strQry = "Alter table Mas_Allowance_Fixation add " + strColumn + " float";
                    //    InsertColumn = db.ExecQry(strQry);

                    //}

                }
                catch (Exception ex)
                {

                }
            }
            else
            {
                iReturn = -2;
            }
            return iReturn;

        }

        public DataSet getExp_Parameter()
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTerr = null;

            strQry = "select Expense_Parameter_Code,Expense_Parameter_Name,case when Expense_Type=1 then 'Fixed' " +
                     "when Expense_Type=2  then 'Variable' " +
                     "end Expense_Type from Mas_Expense_Parameter";

            try
            {
                dsTerr = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsTerr;
        }

        public bool ExpParameter_RecordExist(string Exp_Parameter_Name)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "select count(Expense_Parameter_Name) Expense_Parameter_Name from Mas_Expense_Parameter where Expense_Parameter_Name='" + Exp_Parameter_Name + "'";

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

        public int ExpRecordUpdate(string Exp_Parameter_Code, string ExpParameter_Name, string Exp_Type)
        {
            int iReturn = -1;
            int InsertColumn = 0;
            //if (!RecordExist(ExpParameter_Name))
            //{
            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "Update Mas_Expense_Parameter set Expense_Parameter_Name='" + ExpParameter_Name + "',Expense_Type='" + Exp_Type + "' where Expense_Parameter_Code='" + Exp_Parameter_Code + "'";

                iReturn = db.ExecQry(strQry);


                DataSet dsTerr = null;
                DataTable dt = null;
                strQry = "SELECT REPLACE(LTRIM(RTRIM(Expense_Parameter_Name)), SPACE(1), '_' ) Expense_Parameter_Name " +
                          " FROM Mas_Expense_Parameter where Expense_Type=1 and Expense_Parameter_Code='" + Exp_Parameter_Code + "'";
                dsTerr = db.Exec_DataSet(strQry);

                dt = dsTerr.Tables[0];

                for (int i = 0; i < dsTerr.Tables[0].Rows.Count; i++)
                {
                    string strColumn = dt.Rows[i].Field<string>(0);

                    //strQry = "ALTER TABLE Mas_Allowance_Fixation DROP COLUMN" + strColumn + "";
                    //InsertColumn = db.ExecQry(strQry);

                    strQry = "Alter table Mas_Allowance_Fixation add " + strColumn + " float";
                    InsertColumn = db.ExecQry(strQry);

                }


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

        public int Exp_Parameter_RecordDelete(int Expense_Parameter_Code)
        {
            int iReturn = -1;
            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "DELETE FROM  Mas_Expense_Parameter WHERE Expense_Parameter_Code = '" + Expense_Parameter_Code + "' ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }



        public DataSet getExp_ParameterBL(int BL_workcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTerr = null;

            strQry = "select Expense_Parameter_Code,Expense_Parameter_Name,case when Expense_Type=1 then 'Fixed' " +
                     "when Expense_Type=2  then 'Variable' " +
                     "end Expense_Type from Mas_Expense_Parameter where BL_workcode='" + BL_workcode + "'";

            try
            {
                dsTerr = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsTerr;
        }

        public DataSet getExp_ParameterMGR(int MGR_workcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTerr = null;

            strQry = "select Expense_Parameter_Code,Expense_Parameter_Name,case when Expense_Type=1 then 'Fixed' " +
                     "when Expense_Type=2  then 'Variable' " +
                     "end Expense_Type from Mas_Expense_Parameter where MGR_workcode='" + MGR_workcode + "'";

            try
            {
                dsTerr = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsTerr;
        }

        public DataSet getExp_Managers(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTerr = null;

            strQry = " select a.Sf_Code,a.Sf_HQ+' - '+Sf_Name+' - '+b.Designation_Short_Name Sf_Name from mas_salesforce a," +
                     " Mas_SF_Designation b" +
                     " where a.Designation_Code=b.Designation_Code and sf_TP_Active_Flag in (0,2) " +
                     " and Sf_Code in  (select TP_Reporting_SF from Mas_Salesforce where sf_code != 'admin' and  " +
                     " (Division_Code like '" + div_code + ',' + "%'  or  Division_Code like '%" + ',' + div_code + ',' + "%' ))";



            try
            {
                dsTerr = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsTerr;
        }

        public DataSet getExp_FixedType()
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTerr = null;
            int iCount = 0;

            strQry = "select count(Expense_Parameter_Name) from Mas_Expense_Parameter where Expense_Type=1";
            iCount = db_ER.Exec_Scalar(strQry);

            if (iCount != 0)
            {
                strQry = "DECLARE @cols AS NVARCHAR(MAX),    @query  AS NVARCHAR(MAX) select @cols = STUFF((SELECT ',' + QUOTENAME(Expense_Parameter_Name) " +
                         " from Mas_Expense_Parameter where Expense_Type=1 " +
                         " group by Expense_Parameter_Name, Expense_Parameter_Code " +
                         " order by Expense_Parameter_Code " +
                         " FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'') " +
                         "set @query = N'SELECT ' + @cols + N' from (select Expense_Parameter_Code, Expense_Parameter_Name from Mas_Expense_Parameter ) x " +
                         "pivot " +
                         "(max(Expense_Parameter_Code) " +
                         "for Expense_Parameter_Name in (' + @cols + N') ) p ' " +
                         "exec sp_executesql @query;";

                //strQry = "select Expense_Parameter_Name from Mas_Expense_Parameter where Expense_Type=1";

            }
            else
            {
                strQry = "select Expense_Parameter_Name from Mas_Expense_Parameter where Expense_Type=1";
            }

            try
            {
                dsTerr = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsTerr;
        }

        public int WrkTypeBase_Expense_Update(string Worktype_Name, string Expense_Type)
        {
            int iReturn = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "Update Mas_WorkType_BaseLevel set Expense_Type='" + Expense_Type + "' where Worktype_Name_B='" + Worktype_Name + "'";

                iReturn = db.ExecQry(strQry);

            }
            catch (Exception ex)
            {

            }
            return iReturn;

        }

        public int get_OsExKms(string sf_code, string OSEX_Distance, string Terr_To)
        {
            int iReturn = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "UPDATE Mas_Territory_Creation " +
                         " SET Distance = '" + OSEX_Distance + "' " +
                         " WHERE Sf_Code = '" + sf_code + "' and Territory_Name='" + Terr_To + "' ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }
        //Changes done by Priya
        public DataSet getTerritory_Transfer(string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTerr = null;

            strQry = " SELECT '-1' Territory_Code, '---Select---' Territory_Name " +
                     " UNION " +
                     " SELECT Territory_Code, Territory_Name " +
                     " FROM  Mas_Territory_Creation " +
                     " where Sf_code = '" + sf_code + "' ";

            try
            {
                dsTerr = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsTerr;
        }

        //Changes done by Priya

        public int GetterrCode()
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT isnull(max(Territory_Code)+1,'1') Territory_Code from Mas_Territory_Creation";

                iReturn = db.Exec_Scalar(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;

        }

        public int Insert_Allowance_Data(string div_code, string sf_code, string alw_code, string des_code, string alw_value)
        {
            int iReturn = -1;
            string strSfCode = string.Empty;
            DB_EReporting db = new DB_EReporting();
            try
            {
                strQry = "exec [Insert_Allowance_Values] '" + div_code + "','" + sf_code + "','" + alw_code + "','" + des_code + "','" + alw_value + "'";
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

        public DataSet get_Allowance_Values(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            strQry = "select * from Mas_Allowance_Entry " +
                     " where Division_Code ='" + div_code + "'";
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
        public DataSet get_Expance_Data(string div_code, string sf_code, string years, string monthns)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            strQry = "exec Get_Expance_details '" + div_code + "','" + sf_code + "','" + years + "','" + monthns + "'";
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


        public DataSet get_Expance_Values(string div_code, string sf_code, string years, string monthns)
        {
            DB_EReporting db_ER = new DB_EReporting();
            strQry = "";
            DataSet dsDivision = null;
            // strQry = "select * from Mas_Allowance_Entry where division_code='" + div_code + "' and sf_code='" + sf_code + "'";// and  year(Created_Date)='" + years + "' and  month(Created_Date)='" + monthns + "'";
            strQry = "select * from Mas_Allowance_Entry where sf_code='" + sf_code + "'"; // and  year(Created_Date)='" + years + "' and  month(Created_Date)='" + monthns + "'
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
        public DataSet get_Visit_Details(string div_code, string sf_code, string years, string monthns)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            strQry = "select * from trans_visit_detail where sf='" + sf_code + "' and year(vdate)='" + years + "' and month(vdate)='" + monthns + "' order by SlNo,vDate,Route_Code";
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

        public DataSet get_Distance_Details(string div_code, string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            //strQry = "select * from Mas_Distance_Entry where  charindex(','+ '" + sf_code + "'+',',','+sf_code+',')>0";
            strQry = "GetExpDistanceDetails '" + sf_code + "'";
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
        public DataSet get_Fare_Details(string div_code, string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            strQry = "	SELECT * FROM Mas_Salesforcefare_KM where  charindex(','+ '" + sf_code + "'+',',','+Sf_Code+',')>0";
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
        public DataSet get_appexp_Details(string div_code, string sf_code, string exp_year, string exp_month)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            //strQry = "	SELECT * FROM Trans_Daily_User_Expense   where Division_Code='" + div_code + "' and sf_code='" + sf_code + "' and year(eDate)='" + exp_year + "' and  month(eDate)='" + exp_month + "'";
            strQry = "	SELECT expCode,expName,cast(convert(varchar,eDate,101) as datetime) as eDate,sum(Amt) as Amt FROM Trans_Daily_User_Expense due inner join Mas_Allowance_Type al on al.ID = due.expCode  where due.Division_Code='" + div_code + "' and due.sf_code='" + sf_code + "' and year(due.eDate)='" + exp_year + "' and  month(due.eDate)='" + exp_month + "'  group by expCode,expName,cast(convert(varchar,eDate,101) as datetime)";
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
        public DataSet getrptIssueSlip(string div_code, string sf_code, string fromdate, string sub_Div_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            strQry = "exec GET_IssuSlip '" + div_code + "','" + sf_code + "','" + fromdate + "','" + sub_Div_Code + "'";
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

        public DataSet getrptIssueSlip_Month(string div_code, string sf_code, string FYear, string FMonth, string sub_Div_Code, string stcode = "0")
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            strQry = "exec [GET_IssuSlip_Month] '" + div_code + "','" + sf_code + "','" + FYear + "','" + FMonth + "','" + sub_Div_Code + "'," + stcode + "";
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

        public DataSet getrptIssueSlip_MonthTCEC(string div_code, string sf_code, string FYear, string FMonth, string sub_Div_Code, string stcode = "0")
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            strQry = "exec GET_IssuSlip_Month_TcEc '" + div_code + "','" + sf_code + "','" + FYear + "','" + FMonth + "','" + sub_Div_Code + "'";
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

        public DataSet getrptIssueSlip_MonthTCEC1_tsr(string div_code, string sf_code, string Fdate, string Tdate, string sub_Div_Code, string stcode = "0", string distcode = "0")
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            strQry = "exec GET_IssuSlip_Month_TcEc1_tsr '" + div_code + "','" + sf_code + "','" + Fdate + "','" + Tdate + "','" + sub_Div_Code + "','" + stcode + "','" + distcode + "'";
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

        public DataSet getrptIssueSlip_MonthTCEC11(string div_code, string sf_code, string FYear, string FMonth, string sub_Div_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            strQry = "exec GET_IssuSlip_Month_TcEc1 '" + div_code + "','" + sf_code + "','" + FYear + "','" + FMonth + "','" + sub_Div_Code + "'";
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

        public DataSet getrptIssueSlip_Month_tp(string div_code, string sf_code, string FMonth, string FYear, string SFDepot = "")
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            strQry = "exec [getTP_statusHyrSFList_MGR] '" + div_code + "','" + sf_code + "','" + FMonth + "','" + FYear + "','" + SFDepot + "'";
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


        public DataSet getrptFree(string div_code, string sf_code, string FYear, string FMonth, string sub_Div_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            strQry = "exec [GET_Dis_Detail] '" + div_code + "','" + sf_code + "','" + FYear + "','" + FMonth + "','" + sub_Div_Code + "'";
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
        public DataSet GetPONumbers(string div_code, string suppCode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            strQry = "select Order_No,Sup_Name from Trans_PriOrder_Head where division_code='" + div_code + "' and order_flag='1' and Sup_No='" + suppCode + "'";
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

        public DataSet GetPOQTYValues(string div_code, string transNo)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            strQry = "select * from Trans_PriOrder_Details where division_Code='" + div_code + "' and Trans_Sl_No='" + transNo + "'";
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

        public DataSet getrptIssueSlip_Month_sec(string div_code, string sf_code, string FYear, string FMonth, string sub_Div_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            strQry = "exec [GET_Emp_Month_call] '" + div_code + "','" + sf_code + "','" + FYear + "','" + FMonth + "','" + sub_Div_Code + "'";
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

        public DataSet getrptIssueSlip_Month_pro(string div_code, string sf_code, string FYear, string FMonth, string sub_Div_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            strQry = "exec [GET_Emp_Month_call_pro] '" + div_code + "','" + sf_code + "','" + FYear + "','" + FMonth + "','" + sub_Div_Code + "'";
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

        public DataSet getrptDay(string sf_code, string div_code, string FMonth, string FYear, string sub_Div_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            strQry = "exec [Get_Day] '" + sf_code + "','" + div_code + "','" + FMonth + "','" + FYear + "'";
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
        public DataSet getrptIssueSlip_Month1(string div_code, string sf_code, string FYear, string FMonth, string sub_Div_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            strQry = "exec [GET_Pro_val_sum] '" + div_code + "','" + sf_code + "','" + FYear + "','" + FMonth + "','" + sub_Div_Code + "'";
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

        public DataSet getrptIssueSlip_MonthTCEC1(string div_code, string sf_code, string FYear, string FMonth, string sub_Div_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            strQry = "exec GET_IssuSlip_Month_TcEc '" + div_code + "','" + sf_code + "','" + FYear + "','" + FMonth + "','" + sub_Div_Code + "'";
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

        public DataSet getrptIssueSlip_Month2(string div_code, string sf_code, string FYear, string FMonth, string sub_Div_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            strQry = "exec [GET_Pro_val_sum_cus] '" + div_code + "','" + sf_code + "','" + FYear + "','" + FMonth + "','" + sub_Div_Code + "'";
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

        public DataSet getrptIssueSlip_Month2_dis(string div_code, string sf_code, string FYear, string FMonth, string sub_Div_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            strQry = "exec [GET_Pro_val_sum_cus_dis] '" + div_code + "','" + sf_code + "','" + FYear + "','" + FMonth + "','" + sub_Div_Code + "'";
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

        public DataSet getrptIssueSlip_Month2_cus_dy(string div_code, string sf_code, string Cus, string FYear, string FMonth, string sub_Div_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            strQry = "exec [GET_Pro_val_sum_cus_dy] '" + div_code + "','" + sf_code + "','" + Cus + "','" + FYear + "','" + FMonth + "','" + sub_Div_Code + "'";
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

        public DataSet getrptIssueSlip_Month2_cus_dy_dis(string div_code, string sf_code, string Cus, string FYear, string FMonth, string sub_Div_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            strQry = "exec [GET_Pro_val_sum_cus_dy_dis] '" + div_code + "','" + sf_code + "','" + Cus + "','" + FYear + "','" + FMonth + "','" + sub_Div_Code + "'";
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

        public DataSet getrptProVsSec(string div_code, string sf_code, string FYear, string FMonth, string sub_Div_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            strQry = "exec [GET_Pro_vs_Sec] '" + div_code + "','" + sf_code + "','" + FYear + "','" + FMonth + "','" + sub_Div_Code + "'";
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
        public DataSet getrptProVsSec_sec(string div_code, string sf_code, string FYear, string FMonth, string sub_Div_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            strQry = "exec [GET_Pro_vs_Sec_sec] '" + div_code + "','" + sf_code + "','" + FYear + "','" + FMonth + "','" + sub_Div_Code + "'";
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

        public DataSet getrptProVsSec1(string div_code, string sf_code, string FYear, string FMonth, string sub_Div_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            strQry = "exec [GET_Pro_vs_Sec1] '" + div_code + "','" + sf_code + "','" + FYear + "','" + FMonth + "','" + sub_Div_Code + "'";
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
        public DataSet getrptProVsSec_pro(string div_code, string sf_code, string FYear, string FMonth, string sub_Div_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            strQry = "exec [GET_Pro_vs_Sec_pro] '" + div_code + "','" + sf_code + "','" + FYear + "','" + FMonth + "','" + sub_Div_Code + "'";
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
        public DataSet getCategorywiseorderDetails(string div_code, string sf_code, string FYear, string FMonth, string sub_Div_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            strQry = "exec [Get_Categorywise_Order] '" + div_code + "','" + sf_code + "','" + FYear + "','" + FMonth + "','" + sub_Div_Code + "'";
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

        public int insertmgrallowance(string div_code, string sf_code, string mgrsf, string allwType)
        {
            int iReturn = -1;
            string strSfCode = string.Empty;
            DB_EReporting db = new DB_EReporting();
            try
            {
                strQry = "exec Insert_Allowance_Type_FFO '" + div_code + "','" + sf_code + "','" + mgrsf + "','" + allwType + "'";
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
        public DataSet sfexpense_entry(string SF, string Mn, string Yr, string EmpID)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            strQry = "exec getSfManualExpense'" + SF + "'," + Mn + "," + Yr + ",'" + EmpID + "'";
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
        public DataSet GetApprovalFlag(string SF, string SSf, string Div_code, string Mn, string Yr, string EmpID)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            strQry = "exec getApproval '" + SF + "','" + SSf + "','" + Div_code + "'," + Mn + "," + Yr + ",'" + EmpID + "'";
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
        public string RejectExpense(string SF, string Mn, string Yr, string rejdt, string Rtype, string EmpID)
        {
            DB_EReporting db_ER = new DB_EReporting();

            string dsDivision = string.Empty;
            strQry = "exec RejectExpense'" + SF + "'," + Mn + "," + Yr + ",'" + rejdt + "'," + Rtype + ",'" + EmpID + "'";
            try
            {
                dsDivision = db_ER.Exec_Scalar_s(strQry);
                dsDivision = "Expense Rejected";
            }
            catch (Exception ex)
            {
                dsDivision = ex.Message;
                throw ex;
            }
            return dsDivision;
        }
        public DataSet getExpenseDets(string sf_code, string div, string Mn, string Yr, string exptype)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;

            strQry = "exec getExpenseDets '" + sf_code + "','" + div + "'," + Mn + "," + Yr + ",'" + exptype + "'";

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
        public DataSet getExpApproveDets(string sf_code, string div, string Mn, string Yr)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;

            strQry = "exec getEXPApprDets '" + sf_code + "','" + div + "'," + Mn + "," + Yr + "";

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

        public DataSet getPrimaryCategorywiseorderDetails(string div_code, string sf_code, string FYear, string FMonth, string sub_Div_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            strQry = "exec GET_Primary_Categorywise_Order '" + div_code + "','" + sf_code + "','" + FYear + "','" + FMonth + "','" + sub_Div_Code + "'";
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
        public DataSet getrptPrimaryIssueSlip_Month(string div_code, string sf_code, string FYear, string FMonth, string sub_Div_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            strQry = "exec [GET_PrimaryIssuSlip_Month] '" + div_code + "','" + sf_code + "','" + FYear + "','" + FMonth + "','" + sub_Div_Code + "'";
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

        public DataSet getrptPrimaryIssueSlip_MonthTCEC(string div_code, string sf_code, string FYear, string FMonth, string sub_Div_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            strQry = "exec GET_PrimaryIssuSlip_Month_TcEc '" + div_code + "','" + sf_code + "','" + FYear + "','" + FMonth + "','" + sub_Div_Code + "'";
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

        public DataSet getINV_ExpenseDets(string sf_code, string div, string Mn, string Yr)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;

            strQry = "exec getINV_Expense '" + sf_code + "','" + div + "'," + Mn + "," + Yr + "";

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
        public DataSet new_getExpenseDets(string sf_code, string div, string Mn, string Yr, string exptype)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;

            strQry = "exec expensereport_dtl '" + sf_code + "','" + div + "'," + Mn + "," + Yr + ",'" + exptype + "'";

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
        public DataSet sfexpense_entry_period(string SF, string Mn, string Yr, string EmpID)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            strQry = "exec getSfManualExpense1'" + SF + "'," + Mn + "," + Yr + ",'" + EmpID + "'";
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
        public string RejectExpensePeriodical(string SF, string Mn, string Yr, string rejdt, string Rtype, string EmpID)
        {
            DB_EReporting db_ER = new DB_EReporting();

            string dsDivision = string.Empty;
            strQry = "exec RejectExpense_Periodical'" + SF + "'," + Mn + "," + Yr + ",'" + rejdt + "'," + Rtype + ",'" + EmpID + "'";
            try
            {
                dsDivision = db_ER.Exec_Scalar_s(strQry);
                dsDivision = "Expense Rejected";
            }
            catch (Exception ex)
            {
                dsDivision = ex.Message;
                throw ex;
            }
            return dsDivision;
        }

        public string SaveDistanceDetails(string SF, string xml)
        {
            ExpenseDL DL = new ExpenseDL();
            try
            {
                return DL.SaveDistanceDetails(SF, xml);
            }
            catch { throw; }
            finally { DL = null; }
        }

        public DataSet getrptIssueSlip_Month_dd_tt1(string div_code, string sf_code, string Fdate, string Tdate, string sub_Div_Code, string stcode = "0", string distcode = "0")
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            strQry = "exec [GET_IssuSlip_Month_dd_tt1] '" + div_code + "','" + sf_code + "','" + Fdate + "','" + Tdate + "','" + sub_Div_Code + "','" + stcode + "','" + distcode + "'";
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

        public DataSet getrptIssueSlip_Month1(string div_code, string sf_code, string Fdate, string Tdate, string sub_Div_Code, string statecode = "0", string distcode = "0")
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            strQry = "exec [GET_IssuSlip_Month1] '" + div_code + "','" + sf_code + "','" + Fdate + "','" + Tdate + "','" + sub_Div_Code + "','" + statecode + "','" + distcode + "'";
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

    }
}