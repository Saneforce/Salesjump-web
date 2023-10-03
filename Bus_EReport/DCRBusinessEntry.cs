using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DBase_EReport;
using System.Data.SqlClient;

namespace Bus_EReport
{
    public class DCRBusinessEntry
    {
        private string strQry = string.Empty;

        public string RecordHeadAdd(string sf_code, string divition_code, string transMonth, string transYear, string active)
        {
            int iReturn = -1;
            string strSfCode = string.Empty;
            DB_EReporting db = new DB_EReporting();
            //if (!CheckDupUserName(UsrDfd_UserName))
            //{
            try
            {

                

                int Division_Code = -1;
                
                strQry = "select division_code from Mas_Salesforce_AM where Sf_Code = '" + sf_code + "' ";
                Division_Code = db.Exec_Scalar(strQry);
                //strQry = "SELECT ISNULL(MAX(sf_Sl_No),0)+1 FROM mas_salesforce WHERE Sf_Code !='admin'";
                //sf_sl_no = db.Exec_Scalar(strQry);

                strQry = "SELECT isnull(max(Trans_sl_No)+1,'1') Trans_sl_No from Trans_DCR_BusinessEntry_Head ";
                int Trans_sl_No = db.Exec_Scalar(strQry);

                strQry = " INSERT INTO Trans_DCR_BusinessEntry_Head(Trans_sl_No,Sf_Code, Division_Code, Trans_Month, Trans_Year,Active, Created_Date) " +
                       " VALUES ('" + Trans_sl_No + "' ,'" + sf_code + "' , '" + Division_Code + "', '" + transMonth + "', '" + transYear + "', '" + active + "', getdate()) ";

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
            if (iReturn > 0)
            {
                strQry = "SELECT MAX(Trans_sl_No) FROM Trans_DCR_BusinessEntry_Head";
                iReturn = db.Exec_Scalar(strQry);
            }
            return iReturn.ToString();
            //}
            //else
            //{
            //    return "Dup";
            //}

        }

        public string RecordDetailsAdd(string TransSlNo,string sf_code, string divition_code, string doctorCode, string productCode, int Quantity)
        {
            int iReturn = -1;
            string strSfCode = string.Empty;
            DB_EReporting db = new DB_EReporting();
            int idoctorCode = 0;
            //if (!CheckDupUserName(UsrDfd_UserName))
            //{
            try
            {
                int Division_Code = -1;
                
                strQry = "select division_code from Mas_Salesforce_AM where Sf_Code = '" + sf_code + "' ";
                Division_Code = db.Exec_Scalar(strQry);

                //strQry = "Select  ListedDrCode FROM Mas_ListedDr WHERE ListedDr_Name = '" + doctorCode + "'";
                //idoctorCode = db.Exec_Scalar(strQry);

                strQry = " INSERT INTO [Trans_DCR_BusinessEntry_Details]([Trans_sl_No],[Division_Code],[Doctor_Code],[Product_Code],[Product_Quantity]) " +
                       " VALUES ( '" + TransSlNo + "' , '" + Division_Code + "', '" + doctorCode + "', '" + productCode + "', '" + Quantity + "') ";

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
            if (iReturn > 0)
            {
                return iReturn.ToString();
            }
            else
            {
                return "0";
            }
            
            //}
            //else
            //{
            //    return "Dup";
            //}

        }

        public DataSet GetDCRBusinessReport(string sf_code, string fromMonthYear, string toMonthYear)
        {
            DB_EReporting db_ER = new DB_EReporting();
            int Division_Code = -1;

            strQry = "select division_code from Mas_Salesforce_AM where Sf_Code = '" + sf_code + "' ";
            Division_Code = db_ER.Exec_Scalar(strQry);


            DataSet dsSF = null;
            strQry = "EXEC sp_DCR_Business '" + Division_Code + "','" + sf_code + "','" + fromMonthYear + "','" + toMonthYear + "' ";

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

        public DataSet GetDCRBusinessProducts(string sf_code, string month, string year)
        {
            DB_EReporting db_ER = new DB_EReporting();
            
            DataSet dsSF = null;
            strQry = "EXEC sp_DCR_Business_Products '" + sf_code + "','" + month + "','" + year + "' ";

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

        public DataSet GetDCRExistingBusiness(string sf_code, string processyear, string processmonth)
        {
            DB_EReporting db_ER = new DB_EReporting();
            int Division_Code = -1;

            strQry = "select division_code from Mas_Salesforce_AM where Sf_Code = '" + sf_code + "' ";
            Division_Code = db_ER.Exec_Scalar(strQry);


            DataSet dsSF = null;
            strQry = "EXEC sp_DCR_GetBusinessByUser '" + Division_Code + "','" + sf_code + "','" + processyear + "','" + processmonth + "' ";

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

        public DataSet GetDCRBusinessStatus(string sf_code, string processyear, string processmonth)
        {
            DB_EReporting db_ER = new DB_EReporting();
            int Division_Code = -1;

            strQry = "select division_code from Mas_Salesforce_AM where Sf_Code = '" + sf_code + "' ";
            Division_Code = db_ER.Exec_Scalar(strQry);


            DataSet dsSF = null;
            strQry = "EXEC sp_DCR_Business_Status '" + Division_Code + "','" + sf_code + "','" + processyear + "','" + processmonth + "' ";

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

        public int RecordUpdate_BusinessEntryHead(string TransSlNo)
        {
            int iReturn = -1;
            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "UPDATE Trans_DCR_BusinessEntry_Head " +
                            " SET Updated_Date = getdate() " +
                            " WHERE Trans_sl_No = '" + TransSlNo + "' ";


                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;

        }

        public int RecordDelete_BusinessEntryDetails(string TransSlNo)
        {
            int iReturn = -1;
            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "DELETE FROM Trans_DCR_BusinessEntry_Details WHERE Trans_sl_No = '" + TransSlNo + "' ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;

        }

        public int RecordUpdate_BusinessStatus(string TransSlNo, string status)
        {
            int iReturn = -1;
            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "UPDATE Trans_DCR_BusinessEntry_Head " +
                            " SET Active = '" + status + "',Updated_Date = getdate() " +
                            " WHERE Trans_sl_No = '" + TransSlNo + "' ";


                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;

        }

        public int GetDoctorCodeByName(string doctorName)
        {
            int iReturn = -1;
            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "Select  ListedDrCode FROM Mas_ListedDr WHERE ListedDr_Name = '" + doctorName + "'";
                
                iReturn = db.Exec_Scalar(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;

        }
   }
}
