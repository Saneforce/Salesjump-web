using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DBase_EReport;
using System.Data;

namespace Bus_EReport
{
    public class SampleDespatch
    {
        private string strQry = string.Empty;


        public string RecordHeadAdd(string sf_code, string divition_code, string transMonth, string transYear)
        {
            int iReturn = -1;
            string strSfCode = string.Empty;
            DB_EReporting db = new DB_EReporting();
            
            try
            {
                int Division_Code = -1;

                strQry = "select division_code from Mas_Salesforce_AM where Sf_Code = '" + sf_code + "' ";
                Division_Code = db.Exec_Scalar(strQry);
                //strQry = "SELECT ISNULL(MAX(sf_Sl_No),0)+1 FROM mas_salesforce WHERE Sf_Code !='admin'";
                //sf_sl_no = db.Exec_Scalar(strQry);

                int transSlNo = this.RecordExistSampleDespatchHead(sf_code, Convert.ToString(Division_Code), transMonth, transYear);
                if (transSlNo > 0)
                {
                    this.RecordDelete_SampleDespatchDetails(Convert.ToString(transSlNo));
                    this.RecordDelete_SampleDespatchHead(Convert.ToString(transSlNo));
                }

                strQry = " INSERT INTO [Trans_Sample_Despatch_Head]([Sf_Code] ,[Division_Code] ,[Trans_Month] ,[Trans_Year] ,[Created_Date]) " +
                       " VALUES ( '" + sf_code + "' , '" + Division_Code + "', '" + transMonth + "', '" + transYear + "', getdate()) ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
            if (iReturn > 0)
            {
                strQry = "SELECT MAX(Trans_sl_No) FROM Trans_Sample_Despatch_Head";
                iReturn = db.Exec_Scalar(strQry);
            }
            return iReturn.ToString();
            //}
            //else
            //{
            //    return "Dup";
            //}

        }

        public string RecordDetailsAdd(string TransSlNo, string sf_code, string divition_code, string productCode, int Quantity)
        {
            int iReturn = -1;
            string strSfCode = string.Empty;
            DB_EReporting db = new DB_EReporting();
            
            try
            {
                int Division_Code = -1;

                strQry = "select division_code from Mas_Salesforce_AM where Sf_Code = '" + sf_code + "' ";
                Division_Code = db.Exec_Scalar(strQry);

                strQry = " INSERT INTO [Trans_Sample_Despatch_Details]([Trans_sl_No],[Division_Code],[Product_Code],[Despatch_Qty]) " +
                       " VALUES ( '" + TransSlNo + "' , '" + Division_Code + "', '" + productCode + "', '" + Quantity + "') ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            if (iReturn > 0)
            {
                return iReturn.ToString();
            }
            else
            {
                return "0";
            }
        }

        public int RecordDelete_SampleDespatchHead(string TransSlNo)
        {
            int iReturn = -1;
            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "DELETE FROM Trans_Sample_Despatch_Head WHERE Trans_sl_No = '" + TransSlNo + "' ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;

        }

        public int RecordDelete_SampleDespatchDetails(string TransSlNo)
        {
            int iReturn = -1;
            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "DELETE FROM Trans_Sample_Despatch_Details WHERE Trans_sl_No = '" + TransSlNo + "' ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;

        }

        public int RecordUpdate_SampleDespatchHead(string TransSlNo)
        {
            int iReturn = -1;
            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "UPDATE Trans_Sample_Despatch_Head " +
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

        public DataSet GetSampleDespatchProducts(string sf_code, string month, string year)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            strQry = "EXEC sp_DCR_SampleDespatch_Products '" + sf_code + "','" + month + "','" + year + "' ";

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

        public DataSet GetSampleDespatchStatus(string sf_code, string month, string year)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            strQry = "EXEC sp_DCR_SampleDespatch_Status '" + sf_code + "','" + month + "','" + year + "' ";

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

        public int RecordExistSampleDespatchHead(string sfCode,string divCode, string strmonth, string stryear)
        {

            int iTransSlNo = 0;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "Select Trans_sl_No from Trans_Sample_Despatch_Head WHERE Sf_Code = '" + sfCode + "' AND Division_Code='" + divCode + "' AND Trans_Month='" + strmonth + "' AND Trans_Year ='" + stryear + "' ";
                iTransSlNo = db.Exec_Scalar(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iTransSlNo;
        }
    }
}
