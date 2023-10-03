using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bus_EReport;
using System.Data;
using System.Web.Services;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System.Configuration;
using DBase_EReport;
//using Razorpay.Api;


public partial class Stockist_Payment : System.Web.UI.Page
{

    public static string Stockist_Code;
    public static string Div_Code;
    public static string State;
    public static string Stockist_Name;
    public static DataSet ds = new DataSet();
    public static StockistMaster sm = new StockistMaster();
    public static SqlConnection conn = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString);

    protected void Page_Load(object sender, EventArgs e)
    {
        Stockist_Code = Session["Sf_Code"].ToString();
        Div_Code = Session["div_code"].ToString();
        State = Session["State"].ToString();
        Stockist_Name = Session["sf_name"].ToString();
    }

    [WebMethod(EnableSession = true)]
    public static string bindretailer(string stk, string Div)
    {
        StockistMaster sm = new StockistMaster();
        DataSet ds = new DataSet();
        ds = sm.getretailerdetailsmyorder(stk, Div.TrimEnd(','));
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }

    [WebMethod(EnableSession = true)]
    public static string bind_invoiced_retailer()
    {
        ds = sm.Get_invoiced_retailer(Stockist_Code, Div_Code.TrimEnd(','));
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }

    [WebMethod(EnableSession = true)]
    public static string Get_invoiced_Order_details(string Customer_Code, string From_Year, string To_Year, string From_Month, string To_Month, string Type)
    {
        ds = sm.Get_invoiced_retailer_order(Stockist_Code, Div_Code.TrimEnd(','), Customer_Code, From_Year, To_Year, From_Month, To_Month, Type);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }

    [WebMethod(EnableSession = true)]
    public static string Get_customerWise_credit_note_details(string Customer_Code)
    {
        Bus_EReport.Order or = new Bus_EReport.Order();
        ds = sm.Get_custWise_credit_note_details(Stockist_Code, Div_Code.TrimEnd(','), Customer_Code);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    [WebMethod(EnableSession = true)]
    public static string Get_customerWise_adv_note_details(string Customer_Code)
    {
        //Bus_EReport.Order or = new Bus_EReport.Order();
        ds = Get_custWise_adv_note_details(Stockist_Code, Div_Code.TrimEnd(','), Customer_Code);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    public static DataSet Get_custWise_adv_note_details(string Stockist_Code, string Div_Code, string Customer_Code)
    {
        DataSet ds = null;
        DB_EReporting db_ER = new DB_EReporting();

        string strQry = "Exec getadv_details '" + Stockist_Code + "','" + Div_Code + "','" + Customer_Code + "'";

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
    public class Credit_details
    {
        public string cre_no { get; set; }
        public string cre_amtt { get; set; }
        public string bal_cre_amt { get; set; }
    }

    public class details
    {
        public string BillAmt { get; set; }
        public string Coll_Amt { get; set; }
        public string bal_cre_amt { get; set; }
        public string cre_no { get; set; }
        public string cre_amtt { get; set; }
    }

    public class Pay_Details
    {
        public string Pay_code { get; set; }
        public string Pay_name { get; set; }

    }
    [WebMethod(EnableSession = true)]
    public static Pay_Details[] GetPayType()
    {

        DCR dc = new DCR();
        List<Pay_Details> FFD = new List<Pay_Details>();
        string Div_code = HttpContext.Current.Session["div_code"].ToString();

        DataSet Trans_mas = null;
        Trans_mas = dc.view_Pay_Mas(Div_code.TrimEnd(','));

        foreach (DataRow row in Trans_mas.Tables[0].Rows)
        {
            Pay_Details ffd = new Pay_Details();
            ffd.Pay_code = row["Code"].ToString();
            ffd.Pay_name = row["Name"].ToString();
            FFD.Add(ffd);
        }
        return FFD.ToArray();
    }

    [WebMethod(EnableSession = true)]
    public static string bind_DSM_Details()
    {
        ds = sm.Get_Dsm_Details_bystockist(Stockist_Code);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }

    public class bill
    {
        public string SF { get; set; }
        public string CustCode { get; set; }
        public string Amt { get; set; }
        public string Mode { get; set; }
        public string Inv_Dt { get; set; }
        public string BillNo { get; set; }
        public string e_date { get; set; }
        public string Bank_Name { get; set; }
        public string Adv_ajusted { get; set; }
        public string invoice_no { get; set; }
        public string advance_pay { get; set; }
        public string Collected_by { get; set; }

    }

    public class det
    {
        public string bill_no { get; set; }
        public string bill_date { get; set; }
        public string bill_amt { get; set; }
        public string Pen_amt { get; set; }
        public string paid_amt { get; set; }
        public string order_no { get; set; }
        public string inv_no { get; set; }
    }
    public class adv
    {
        public string Sl_no { get; set; }
        public string Adv { get; set; }
        public string rem_adv { get; set; }
    }
    public class detcrd
    {
        public string bill_no { get; set; }
        public string adj { get; set; }
        public string bal { get; set; }
        public string Entry { get; set; }

    }


    public static string Save_Payment(string Paymend_Details, string details, string adv ,string detailscrd, string Cust_ID, string Cust_Name, string Total_amount, string Mode, string Reference_No, string Bk_name, string Pay_Date, string collect_by, string Remark, string Advance_pay, string Advance_Adj, string Type, string invoice_no)
    {
        string msg = string.Empty;
        SqlTransaction tran;
        string typ = string.Empty;
        string sqlQry = string.Empty;
        DB_EReporting db = new DB_EReporting();
        DataSet ds_EReport = new DataSet();
        DateTime localDate1 = System.DateTime.Now;
        string date_format = localDate1.ToString("MM-dd-yyy HH:mm:ss");

        var items = JsonConvert.DeserializeObject<List<bill>>(Paymend_Details);
        var detil = JsonConvert.DeserializeObject<List<det>>(details);
        var advdet = JsonConvert.DeserializeObject<List<adv>>(adv);
        var detilcrd = JsonConvert.DeserializeObject<List<detcrd>>(detailscrd);

        DateTime dtgrn = Convert.ToDateTime(Pay_Date);
        string strgrn = dtgrn.ToString("MM-dd-yyy");

        if (Type == "0") { typ = "Credit"; } else if (Type=="2") { typ = "Advance";Type = "0"; } else { typ = "Payment"; }

        if (conn.State != ConnectionState.Closed)
        {
            conn.Close();
        }
        conn.Open();
        tran = conn.BeginTransaction();
        try
        {

            sqlQry = "select Territory_Code from mas_listeddr where ListedDrCode = '" + Cust_ID + "'";
            string route_Code = db.Exec_Scalar(sqlQry).ToString();

            sqlQry = "SELECT ISNULL(MAX(Sl_No),0)+1 FROM trans_payment_detail";
            string Sl_No = db.Exec_Scalar(sqlQry).ToString();
            var DT = System.DateTime.Now.ToString("MM-dd-yyy HH:mm:ss").Replace("-", "");
            DT = DT.Replace(":", "");
            DT = DT.Replace(" ", "");
            var TR = Stockist_Code + "-" + DT;
            sqlQry = "insert into trans_payment_detail(Sl_No,Sf_Code,Sf_Name,Cust_Id,Cus_Name,Amount,Pay_Mode,Pay_Date,Pay_Ref_No,Remarks,Distributor_Code,Route_code,eDate,PaymentName,type,advance_pay,div_code,invoice_no,Adv_ajusted,Collected_by,eKey)";
            if (Type == "0")
            {
                sqlQry += "values('" + Sl_No + "','" + Stockist_Code + "','" + Stockist_Name + "','" + Cust_ID + "','" + Cust_Name + "','" + Total_amount + "','" + Mode + "'" +
                      ",'" + date_format + "','" + Reference_No + "','" + Remark + "','" + Stockist_Code + "','" + route_Code + "','" + strgrn + "','" + Bk_name + "','" + typ + "','0.00'," +
                      "'" + Div_Code + "','" + invoice_no + "','0','" + collect_by + "' ,'" + TR + "')";

            }
            else
            {
                sqlQry += "values('" + Sl_No + "','" + Stockist_Code + "','" + Stockist_Name + "','" + Cust_ID + "','" + Cust_Name + "','" + Total_amount + "','" + Mode + "'" +
                      ",'" + date_format + "','" + Reference_No + "','" + Remark + "','" + Stockist_Code + "','" + route_Code + "','" + strgrn + "','" + Bk_name + "','" + typ + "','" + Advance_pay + "'," +
                      "'" + Div_Code + "','" + invoice_no + "','" + Advance_Adj + "','" + collect_by + "' ,'" + TR + "')";
                
            }
            int y = db.ExecQry(sqlQry);

            for (int i = 0; i < detil.Count; i++)
            {
                var DT1 = System.DateTime.Now.ToString("MM-dd-yyy HH:mm:ss").Replace("-", "");
                DT1 = DT1.Replace(":", "");
                DT1 = DT1.Replace(" ", "");
                var TR1 = Stockist_Code + "-" + DT;
                //var RR= Stockist_Code+'-'+ detil[i].bill_date.Replace()
                sqlQry = "insert into trans_payment_detail_view(Sl_No,bill_no,bill_date,bill_amt,Pen_amt,paid_amt,eKey)";
                sqlQry += "values('" + Sl_No + "','" + detil[i].bill_no + "','" + detil[i].bill_date + "','" + detil[i].bill_amt + "','" + detil[i].Pen_amt + "','" + detil[i].paid_amt + "','" + TR1 + "')";
                int o = db.ExecQry(sqlQry);

            }

            string sxml = "<ROOT>";
            for (int k = 0; k < items.Count; k++)
            {
                if (detil[k].paid_amt != "")
                {
                    sxml += "<Bill Sf_Code=\"" + Stockist_Code + "\" Cust_Code=\"" + Cust_ID + "\" Received_amt=\"" + detil[k].paid_amt + "\" Pay_Date=\"" + detil[k].bill_date + "\" Bill_No=\"" + detil[k].order_no + "\" Inv_no=\"" + detil[k].inv_no + "\"  />";
                }
            }
            sxml += "</ROOT>";
            string sxml2 = "<ROOT>";
            if (adv != "")
            {
                for (int k = 0; k < advdet.Count; k++)
                {

                    sxml2 += "<ADV Sf_Code=\"" + Stockist_Code + "\" Cust_Code=\"" + Cust_ID + "\"  Slno=\"" + advdet[k].Sl_no + "\" Adv=\"" + advdet[k].Adv + "\" rem_adv=\"" + advdet[k].rem_adv + "\"  />";

                }
            }
            sxml2 += "</ROOT>";
            string sxml1 = "<ROOT>";
            string ctyp = "payment";
            if (detailscrd != "")
            {
                // sxml1 = "<ROOT>";
                for (int k = 0; k < detilcrd.Count; k++)
                {

                    sxml1 += "<Bill  bill_no=\"" + detilcrd[k].bill_no + "\" adj=\"" + detilcrd[k].adj + "\" bal=\"" + detilcrd[k].bal + "\"  entry=\"" + detilcrd[k].Entry + "\" />";
                    ctyp = detilcrd[k].Entry;
                }

            }

            sxml1 += "</ROOT>";
            // msg = sm.Update_Pending_bills(sxml, Stockist_Code, Cust_ID, Total_amount, Advance_pay, Type);
            msg = Update_Pending_bills(sxml, sxml1, sxml2, Stockist_Code, Cust_ID, Total_amount, Advance_pay, Type,ctyp);
            tran.Commit();
            conn.Close();
        }
        catch (Exception exp)
        {
            if (tran != null)
                tran.Rollback();
            msg = exp.Message.ToString() + "\nTransaction Rolledback, Tim didn't make it.";
            throw exp;
        }
        return msg;
    }
    public static string Update_Pending_bills(string sxml, string sxml1, string sxml2, string Stockist_Code, string Cust_ID, string Total_amount, string Advance_pay, string Type, string ctyp)
    {
        DataSet ds = null;
        DB_EReporting db_ER = new DB_EReporting();
        string msg = string.Empty;
        string strQry = "exec Update_Pending_bill '" + sxml + "','" + sxml1 + "','" + sxml2 + "','" + Stockist_Code + "','" + Cust_ID + "','" + Total_amount + "','" + Advance_pay + "','" + Type + "','"+ ctyp + "'";
        try
        {
            ds = db_ER.Exec_DataSet(strQry);
            msg = "Success";
        }
        catch (Exception ex)
        {
            msg = ex.Message;
        }
        return msg;
    }
    public static string Update_Pending_bills_new(string sxml, string Stockist_Code, string Cust_ID, string Total_amount, string Advance_pay, string crt_Advance_pay, string Type)
    {
        DataSet ds = null;
        DB_EReporting db_ER = new DB_EReporting();
        string msg = string.Empty;
        string strQry = "exec Update_Pending_bill_new '" + sxml + "','" + Stockist_Code + "','" + Cust_ID + "','" + Total_amount + "','" + Advance_pay + "','" + crt_Advance_pay + "','" + Type + "'";
        try
        {
            ds = db_ER.Exec_DataSet(strQry);
            msg = "Success";
        }
        catch (Exception ex)
        {
            msg = ex.Message;
        }
        return msg;
    }
    [WebMethod(EnableSession = true)]
    public static string Update_credit(string Invoice_details, string details, string detailscrd, string Cust_ID, string Cust_Name, string Total_amount, string Advance_pay, string Advance_Adj, string Type, string invoice_no, string Pay_Date)
    {
        string msg = string.Empty; string Mode = "Adjust"; string Reference_No = ""; string collect_by = ""; string Remark = ""; string Bk_name = "";
        Save_Payment(Invoice_details, details,"",detailscrd, Cust_ID, Cust_Name, Total_amount, Mode, Reference_No, Bk_name, Pay_Date, collect_by, Remark, Advance_pay, Advance_Adj, Type, invoice_no);
        return msg;
    }

    [WebMethod(EnableSession = true)]
    public static string Save_Billing(string Paymend_Details, string details, string detailsdataadv,string Cust_ID, string Cust_Name, string Total_amount, string Mode, string Reference_No, string Bk_name, string Pay_Date, string collect_by, string Remark, string Advance_pay, string Advance_Adj, string Type, string invoice_no)
    {
        string msg = string.Empty;
        Save_Payment(Paymend_Details, details, detailsdataadv, detailsdataadv, Cust_ID, Cust_Name, Total_amount, Mode, Reference_No, Bk_name, Pay_Date, collect_by, Remark, Advance_pay, Advance_Adj, Type, invoice_no);
        return msg;
    }
    [WebMethod(EnableSession = true)]
    public static string Save_Billing_Adjust(string Paymend_Details, string details, string Cust_ID, string Cust_Name, string Total_amount, string crt_Total_amount, string Mode, string Reference_No, string Bk_name, string Pay_Date, string collect_by, string Remark, string Advance_pay, string Advance_Adj, string crt_Advance_pay, string crt_Advance_Adj, string Type, string invoice_no)
    {
        string msg = string.Empty;
        Save_Payment_Adjust(Paymend_Details, details, Cust_ID, Cust_Name, Total_amount, crt_Total_amount, Mode, Reference_No, Bk_name, Pay_Date, collect_by, Remark, Advance_pay, Advance_Adj, crt_Advance_pay, crt_Advance_Adj, Type, invoice_no);
        return msg;
    }
    public static string Save_Payment_Adjust(string Paymend_Details, string details, string Cust_ID, string Cust_Name, string Total_amount, string crt_Total_amount, string Mode, string Reference_No, string Bk_name, string Pay_Date, string collect_by, string Remark, string Advance_pay, string Advance_Adj, string crt_Advance_pay, string crt_Advance_Adj, string Type, string invoice_no)
    {
        string msg = string.Empty;
        SqlTransaction tran;
        string typ = string.Empty;
        string sqlQry = string.Empty;
        DB_EReporting db = new DB_EReporting();
        DataSet ds_EReport = new DataSet();
        DateTime localDate1 = System.DateTime.Now;
        string date_format = localDate1.ToString("MM-dd-yyy HH:mm:ss");

        var items = JsonConvert.DeserializeObject<List<bill>>(Paymend_Details);
        var detil = JsonConvert.DeserializeObject<List<det>>(details);

        DateTime dtgrn = Convert.ToDateTime(Pay_Date);
        string strgrn = dtgrn.ToString("MM-dd-yyy");

        if (Type == "0") { typ = "Credit"; } else if (Type == "2") { typ = "Payment,Credit"; } else { typ = "Payment"; }

        if (conn.State != ConnectionState.Closed)
        {
            conn.Close();
        }
        conn.Open();
        tran = conn.BeginTransaction();
        try
        {


            sqlQry = "select Territory_Code from mas_listeddr where ListedDrCode = '" + Cust_ID + "'";
            string route_Code = db.Exec_Scalar(sqlQry).ToString();

            sqlQry = "SELECT ISNULL(MAX(Sl_No),0)+1 FROM trans_payment_detail";
            string Sl_No = db.Exec_Scalar(sqlQry).ToString();
            float adj = (float)Convert.ToDouble(crt_Advance_Adj == "" ? "0" : crt_Advance_Adj) + (float)Convert.ToDouble(Advance_Adj == "" ? "0" : Advance_Adj);
            float adv = (float)Convert.ToDouble(Advance_pay == "" ? "0" : Advance_pay) + (float)Convert.ToDouble(crt_Advance_pay == "" ? "0" : crt_Advance_pay);
            sqlQry = "insert into trans_payment_detail(Sl_No,Sf_Code,Sf_Name,Cust_Id,Cus_Name,Amount,Pay_Mode,Pay_Date,Pay_Ref_No,Remarks,Distributor_Code,Route_code,eDate,PaymentName,type,advance_pay,div_code,invoice_no,Adv_ajusted,Collected_by)";
            sqlQry += "values('" + Sl_No + "','" + Stockist_Code + "','" + Stockist_Name + "','" + Cust_ID + "','" + Cust_Name + "','" + Total_amount + "','" + Mode + "'" +
                      ",'" + date_format + "','" + Reference_No + "','" + Remark + "','" + Stockist_Code + "','" + route_Code + "','" + strgrn + "','" + Bk_name + "','" + typ + "','" + Advance_pay + "'," +
                      "'" + Div_Code + "','" + invoice_no + "'," + adj + ",'" + collect_by + "')";
            int y = db.ExecQry(sqlQry);


            for (int i = 0; i < detil.Count; i++)
            {
                //var RR= Stockist_Code+'-'+ detil[i].bill_date.Replace()
                sqlQry = "insert into trans_payment_detail_view(Sl_No,bill_no,bill_date,bill_amt,Pen_amt,paid_amt)";
                sqlQry += "values('" + Sl_No + "','" + detil[i].bill_no + "','" + detil[i].bill_date + "','" + detil[i].bill_amt + "','" + detil[i].Pen_amt + "','" + detil[i].paid_amt + "')";
                int K = db.ExecQry(sqlQry);

            }

            string sxml = "<ROOT>";
            for (int k = 0; k < items.Count; k++)
            {
                if (detil[k].paid_amt != "")
                {
                    sxml += "<Bill Sf_Code=\"" + Stockist_Code + "\" Cust_Code=\"" + Cust_ID + "\" Received_amt=\"" + detil[k].paid_amt + "\" Pay_Date=\"" + detil[k].bill_date + "\" Bill_No=\"" + detil[k].order_no + "\" Inv_no=\"" + detil[k].inv_no + "\"  />";
                }
            }
            sxml += "</ROOT>";
            msg = Update_Pending_bills_new(sxml, Stockist_Code, Cust_ID, Total_amount, Advance_pay, crt_Advance_pay, Type);
            tran.Commit();
            conn.Close();
        }
        catch (Exception exp)
        {
            if (tran != null)
                tran.Rollback();
            msg = exp.Message.ToString() + "\nTransaction Rolledback, Tim didn't make it.";
            throw exp;
        }
        return msg;
    }

    [WebMethod(EnableSession = true)]
    public static string Advance_saving(string Customer_ID, string Advance_amt)
    {
        StockistMaster sm = new StockistMaster();
        DataSet ds = new DataSet();
        ds = Save_adv_amt_for_retailer(Customer_ID, Advance_amt, Div_Code, Stockist_Code);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    public static DataSet Save_adv_amt_for_retailer(string c_id, string adv_amt, string div ,string stockist_code)
    {
        DataSet ds = null;
        DB_EReporting db_ER = new DB_EReporting();

        string strQry = "Exec sp_Save_adv_for_retailer '" + c_id + "','" + adv_amt + "','" + div + "','" + stockist_code + "'";
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

    [WebMethod(EnableSession = true)]
    public static string Get_Year_Data(string FM, string TM, string Type)
    {
        string Div_Code = HttpContext.Current.Session["div_code"].ToString();
        DataSet ds = new DataSet();
        //Stockist_Sales ss = new Stockist_Sales();
        //ds = ss.Get_year_details_Data(Div_Code, FM, TM, Type);
        ds = Get_year_details_Data(Div_Code, FM, TM, Type);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    public static DataSet Get_year_details_Data(string Div_Code, string fm, string tm, string type)
    {
        DB_EReporting db_ER = new DB_EReporting();
        string strQry = "EXEC get_financial_year '" + Div_Code + "','" + type + "','" + fm + "','" + tm + "'";
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


    [WebMethod(EnableSession = true)]
    public static string GetAdvanceDetails(string Customer_Code)
    {
        ds = sm.Get_Retailer_Advance(Customer_Code);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }

}