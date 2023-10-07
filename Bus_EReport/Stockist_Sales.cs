using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DBase_EReport;

namespace Bus_EReport
{
    public class Stockist_Sales
    {
        private string strQry = string.Empty;
        DataTable dt = new DataTable();
        DataSet ds = null;

        public DataSet Bind_salebycust_Count(string Stockist_Code, string FDT, string TDT)
        {
            DB_EReporting db_ER = new DB_EReporting();

            strQry = "EXEC Bind_Sales_By_Cust_count '" + Stockist_Code + "','" + FDT + "','" + TDT + "'";

            try
            {
                ds = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet Bind_salebycust_details(string Stockist_Code, string FDT, string TDT, string Div_Code, string Cust_No)
        {
            DB_EReporting db_ER = new DB_EReporting();

            strQry = "EXEC Bind_salebycust_details '" + Cust_No + "','" + FDT + "','" + TDT + "','" + Div_Code + "','" + Stockist_Code + "'";

            try
            {
                ds = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet Bind_salebyitem_Count(string Stockist_Code, string FDT, string TDT, string Div_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            strQry = "EXEC Bind_salebyItem_count '" + FDT + "','" + TDT + "','" + Div_Code + "','" + Stockist_Code + "'";
            try
            {
                ds = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
        public DataSet Bind_salebyitem_details(string Stockist_Code, string FDT, string TDT, string Div_Code, string Pro_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            strQry = "EXEC Bind_salebyItem_Details '" + FDT + "','" + TDT + "','" + Div_Code + "','" + Stockist_Code + "','" + Pro_Code + "'";
            try
            {
                ds = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
        public DataSet Bind_salebysalesman_Count(string Stockist_Code, string FDT, string TDT, string Div_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            strQry = "EXEC Bind_salebysalesman '" + FDT + "','" + TDT + "','" + Div_Code + "','" + Stockist_Code + "'";
            try
            {
                ds = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
        public DataSet Bind_Get_saleman_details(string Stockist_Code, string FDT, string TDT, string Div_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            strQry = "EXEC Bind_salebysalesman_details '" + FDT + "','" + TDT + "','" + Div_Code + "','" + Stockist_Code + "'";
            try
            {
                ds = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
        public DataSet Bind_ac_trans_details(string Stockist_Code, string FDT, string TDT, string Div_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            strQry = "EXEC sp_account_transaction '" + Stockist_Code + "','" + Div_Code + "','" + FDT + "','" + TDT + "'";
            try
            {
                ds = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
        public DataSet Bind_cust_Bal(string Stockist_Code, string Div_Code, string From_Year, string To_Year,string from_mth, string to_mnth, string Type)
        {
            DB_EReporting db_ER = new DB_EReporting();

            strQry = "EXEC sp_bind_cust_bal '" + Stockist_Code + "','" + Div_Code + "','" + From_Year + "','" + To_Year + "','" + from_mth + "','" + to_mnth + "','" + Type + "'";
            try
            {
                ds = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet Bind_Cust_Bal_details(string Cust_Code, string Div_Code,string From_Year,string To_Year, string From_Month, string To_Month, string Typee)
        {
            DB_EReporting db_ER = new DB_EReporting();

            strQry = "EXEC sp_cust_bal_details '" + Cust_Code + "','" + Div_Code + "','" + From_Year + "','" + To_Year + "','" + From_Month + "','" + To_Month + "','" + Typee + "'";
            try
            {
                ds = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet Bind_Payment_received_details(string Stockist_Code, string FDT, string TDT, string Div_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            strQry = "EXEC sp_bind_payment_received_details '" + Stockist_Code + "','" + FDT + "','" + TDT + "','" + Div_Code + "'";
            try
            {
                ds = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet Bind_Purchase_by_vendor_Count(string Stockist_Code, string FDT, string TDT, string Div_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            strQry = "EXEC sp_Bind_Purchase_by_vendor_Count '" + Stockist_Code + "','" + FDT + "','" + TDT + "','" + Div_Code + "'";
            try
            {
                ds = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet Bind_Purchase_by_vendor_Details(string Stockist_Code, string FDT, string TDT, string Div_Code, string Vendor_Id)
        {
            DB_EReporting db_ER = new DB_EReporting();

            strQry = "EXEC sp_Bind_Purchase_by_vendor_Details '" + Stockist_Code + "','" + FDT + "','" + TDT + "','" + Div_Code + "','" + Vendor_Id + "'";
            try
            {
                ds = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet Bind_Purchasebyitem_Count(string Stockist_Code, string FDT, string TDT, string Div_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            strQry = "EXEC Bind_purchasebyItem_count '" + Stockist_Code + "','" + FDT + "','" + TDT + "','" + Div_Code + "'";
            try
            {
                ds = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet get_sec_order(string Order_id)
        {
            DB_EReporting db_ER = new DB_EReporting();

            strQry = "select ph.Trans_Sl_No,ERP_Code,Plant_id,Product_code,CQty from Trans_PriOrder_Head ph inner join Trans_PriOrder_Details pd on ph.Trans_Sl_No = pd.Trans_Sl_No inner join Mas_Stockist ms on ms.Stockist_Code = ph.Stockist_Code where ph.Trans_Sl_No in(" + Order_id + ") and sap_code =0";
                //strQry = "Exec Get_Sap_order '" + Order_id + "'";
            try
            {
                ds = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet Bind_productwise_stock_details(string Stockist_Code, string FDT, string TDT, string Div_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            strQry = "EXEC sp_get_product_wise_stock '" + Stockist_Code + "','" + FDT + "','" + TDT + "','" + Div_Code + "'";
            try
            {
                ds = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet Get_year_details_Data(string Div_Code,string fm,string tm,string type)
        {
            DB_EReporting db_ER = new DB_EReporting();

            strQry = "EXEC get_financial_year '" + Div_Code + "','" + type + "','" + fm + "','" + tm + "'";
            try
            {
                ds = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
    }
}

