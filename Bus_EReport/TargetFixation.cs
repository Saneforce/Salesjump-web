using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DBase_EReport;
using System.Data;

namespace Bus_EReport
{
    public class TargetFixation
    {
        private string strQry = string.Empty;


        public string RecordHeadAdd(string sf_code, string divition_code, string financialYear)
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
                strQry = "SELECT isnull(max(Trans_sl_No)+1,'1') Trans_sl_No from Trans_TargetFixation_Product_Head ";
                int Trans_sl_No = db.Exec_Scalar(strQry);

                strQry = " INSERT INTO [Trans_TargetFixation_Product_Head]([Trans_sl_No],[Sf_Code] ,[Division_Code] ,[Financial_Year],[Created_Date]) " +
                       " VALUES ( '" + Trans_sl_No + "','" + sf_code + "' , '" + Division_Code + "', '" + financialYear + "', getdate()) ";

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
                strQry = "SELECT MAX(Trans_sl_No) FROM Trans_TargetFixation_Product_Head";
                iReturn = db.Exec_Scalar(strQry);
            }
            return iReturn.ToString();
            //}
            //else
            //{
            //    return "Dup";
            //}

        }

        public string RecordDetailsAdd(string TransSlNo, string productCode, string monthName, int Quantity)
        {
            int iReturn = -1;
            DB_EReporting db = new DB_EReporting();
            //if (!CheckDupUserName(UsrDfd_UserName))
            //{
            try
            {
                strQry = " INSERT INTO [Trans_TargetFixation_Product_Details]([Trans_sl_No],[Product_Code],[Month],[Quantity]) " +
                       " VALUES ( '" + TransSlNo + "' , '" + productCode + "', '" + monthName + "', '" + Quantity + "') ";

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

        public DataSet GetTargetFixationList(string sf_code, string divcode, string financialYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            strQry = "EXEC sp_TargetFixation_Productwise '" + sf_code + "','" + divcode + "','" + financialYear + "' ";

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

        public DataSet GetTargetFixationReport(string sf_code, string fromMonthYear,string toMonthYear)
        {
            DB_EReporting db_ER = new DB_EReporting();
            int Division_Code = -1;

            strQry = "select division_code from Mas_Salesforce_AM where Sf_Code = '" + sf_code + "' ";
            Division_Code = db_ER.Exec_Scalar(strQry);


            DataSet dsSF = null;
            strQry = "EXEC sp_TargetFixation_Report '" + sf_code + "','" + Division_Code + "','" + fromMonthYear + "','" + toMonthYear + "' ";

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

        public int RecordUpdate_TargetMain(string TransSlNo)
        {
            int iReturn = -1;
            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "UPDATE Trans_TargetFixation_Product_Head " +
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
        public DataSet getPriProductwiseTargetSales(string sfCode, string fYear, string fMonth, string tYear, string tMonth, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            strQry = "[Get_PriProduct_Target_vs_Sal] '" + sfCode + "','" + fYear + "','" + fMonth + "','" + tYear + "','" + tMonth + "','" + div_code + "'";
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
        public int RecordDelete_TargetDetails(string TransSlNo)
        {
            int iReturn = -1;
            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "DELETE FROM Trans_TargetFixation_Product_Details WHERE Trans_sl_No = '" + TransSlNo + "' ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;

        }

        public DataSet CustTargetSet(string xmlFormat)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            strQry = "exec InsertXML '" + xmlFormat + "'";
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

        public DataSet getTargets(string sfCode, string fYear, string fMonth)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            strQry = "exec getCustTarget '" + sfCode + "','" + fYear + "','" + fMonth + "'";
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
        public DataSet getProductwiseTargetSales(string sfCode, string fYear, string fMonth, string tYear, string tMonth)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            strQry = "[Get_Product_Target_vs_Sal] '" + sfCode + "','" + fYear + "','" + fMonth + "','" + tYear + "','" + tMonth + "'";
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

        public DataSet InsertDistTarget(string xmlFormat)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            strQry = "exec InsertXML_Distributor_Target '" + xmlFormat + "'";
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
        public DataSet getTargets_for_distributor(string sfCode, string fYear, string fMonth)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            strQry = "exec getDistTarget '" + sfCode + "','" + fYear + "','" + fMonth + "'";
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
    }
}
