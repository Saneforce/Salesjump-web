using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Bus_Objects;
using DBase_EReport;
using Newtonsoft.Json;

namespace Bus_EReport
{
    public class Invoice
    {
        private string strQry = string.Empty;
        public DataSet getPendingBillSF(string DivCode, string SFCode,string SubDiv="0")
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = "exec getPendingBillSF '" + DivCode + "','" + SFCode + "','" + SubDiv + "' ";
            try
            {
                dsListedDR = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsListedDR;
        }
        public DataSet getPendingBillRetailer(string SFCode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = "exec [getPendingBillRetailer] '" + SFCode + "'";
            try
            {
                dsListedDR = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsListedDR;
        }
        public DataSet getCollectionDetails(string DivCode, string SFCode, string FromDt, string ToDt, string SubDivCode = "0")
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet dsListedDR = null;
            strQry = "exec getCollectDetails '" + DivCode + "','" + SFCode + "','" + FromDt + "','" + ToDt + "','" + SubDivCode + "'";
            try
            {
                dsListedDR = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsListedDR;
        }

        public DataSet getCollectionDetailsSFWise(string DivCode, string SFCode, string FromDt, string ToDt, string SubDivCode = "0")
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet dsListedDR = null;
            strQry = "exec getCollectDetailsSF '" + DivCode + "','" + SFCode + "','" + FromDt + "','" + ToDt + "','" + SubDivCode + "'";
            try
            {
                dsListedDR = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsListedDR;
        }

    }
}
