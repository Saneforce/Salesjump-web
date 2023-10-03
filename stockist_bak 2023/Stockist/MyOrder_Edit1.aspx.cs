using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
using System.Data.SqlClient;
using System.Web.Services;
using System.Configuration;
using Newtonsoft.Json;
using System.Globalization;
using ClosedXML.Excel;
using System.Xml;
using System.IO;
using System.Transactions;
using DBase_EReport;

public partial class MyOrder_Edit1 : System.Web.UI.Page
{
    public static DataSet ds = new DataSet();
    public static StockistMaster sm = new StockistMaster();
    public static string Order_ID = string.Empty;
    public static string Div = string.Empty;
    public static string stk = string.Empty;
    public static string Cust_Code = string.Empty;
    public static string Order_date = string.Empty;
	public static string type = string.Empty;
    public static SqlConnection conn = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString);

    protected void Page_Load(object sender, EventArgs e)
    {
        Order_ID = Request.QueryString["Order_No"].ToString();
        Div = Request.QueryString["Div_Code"].ToString();
        stk = Request.QueryString["Stockist_Code"].ToString();
        Cust_Code = Request.QueryString["Cust_Code"].ToString();
        Order_date = Request.QueryString["Order_Date"].ToString();
    }

    [WebMethod(EnableSession = true)]
    public static string GetSecOrderDetails(string Order_No)
    {
        string Div_Code = HttpContext.Current.Session["div_code"].ToString();
        ds = sm.getallSecorderbystockist(stk, Div_Code.TrimEnd(','), Order_No);
        //strQry = "EXEC sp_get_Price_Details '" + Div_Code.TrimEnd(',') + "','" + Stockist_Code + "','" + state_code + "','" + retailercode + "'";
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }

    [WebMethod(EnableSession = true)]
    public static string GetProducts()
    {
        Product Rut = new Product();
        string state_code = HttpContext.Current.Session["State"].ToString();
        //ds = Rut.getProdall(Div, state_code, stk);
        ds = getProdall(Div, state_code, stk);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    public static  DataSet getProdall(string divcode, string state_code, string stk)
    {
        DB_EReporting db_ER = new DB_EReporting();
        DataSet dsProCat = null;

       string strQry = "EXEC get_product_details_view '" + divcode + "','" + state_code + "','" + stk + "'";

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
    [WebMethod(EnableSession = true)]
    public static string Get_Product_unit()
    {
        string div_code = HttpContext.Current.Session["div_code"].ToString();
        string stk = HttpContext.Current.Session["Sf_Code"].ToString();
        div_code = div_code.TrimEnd(',');
        ds = sm.gets_Product_unit_details(div_code, stk);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }

    [WebMethod(EnableSession = true)]
    public static string GetCustWise_price()
    {
        string div_code = HttpContext.Current.Session["div_code"].ToString();
        div_code = div_code.TrimEnd(',');
        string strQry = "EXEC sp_get_Price_Details '" + Div + "','" + stk + "','" + "" + "','" + Cust_Code + "'";
        //ds = getDataSet("select * from Mas_Retailer_Wise_Price_details with (nolock) where Division_Code = '" + div_code + "'");
        ds = getDataSet(strQry);
        // ds = getDataSet("select h.*,d.* from Mas_Retailer_Wise_Price_Head h WITH (NOLOCK) inner join Mas_Retailer_Wise_Price_details d WITH (NOLOCK) on h.Retailer_code=d.Retailer_code where Division_Code = '" + div_code + "'";
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    [WebMethod(EnableSession = true)]
    public static string GetWise_price(string retailercode)
    {
        string Stockist_Code = HttpContext.Current.Session["Sf_Code"].ToString();
        string div_code = HttpContext.Current.Session["div_code"].ToString();
        div_code = div_code.TrimEnd(',');
        string state_code = HttpContext.Current.Session["State"].ToString();
        DataSet ds = new DataSet();
        StockistMaster sm = new StockistMaster();
        string strQry = string.Empty;
        DB_EReporting db_ER = new DB_EReporting();
        strQry = "EXEC sp_get_Price_Details '" + div_code + "','" + Stockist_Code + "','" + state_code + "','" + retailercode + "'";
        try
        {
            ds = db_ER.Exec_DataSet(strQry);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }

    public static DataSet getDataSet(string qrystring)
    {

        DB_EReporting db_ER = new DB_EReporting();
        DataSet dsSF = null;
        string strQry = qrystring;

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

    [WebMethod(EnableSession = true)]
    public static string Get_Product_Tax()
    {
        string Stockist_Code = HttpContext.Current.Session["Sf_Code"].ToString();
        string div_code = HttpContext.Current.Session["div_code"].ToString();
        string state_code = HttpContext.Current.Session["State"].ToString();
        div_code = div_code.TrimEnd(',');
        //ds = sm.gets_Product_Tax_details(div_code, Stockist_Code, state_code);
        ds = gets_Product_Tax_details(div_code, Stockist_Code, state_code);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    public static DataSet gets_Product_Tax_details(string div, string stk, string state)
    {
        DataSet ds = null;
        DB_EReporting db_ER = new DB_EReporting();

        string strQry = "Exec get_tax_Details '" + div + "','" + stk + "','" + state + "'";

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
    [WebMethod]
    public static string saveorders(string NewOrd, string Remark, string Ordrval, string RetCode, string RecDate, string Ntwt, string retnm, string Type, string ref_order, string sub_total, string dis_total, string tax_total, string Ord_id)
    {
        string msg = string.Empty;
        SqlTransaction tran;

        string Stockist_Code = HttpContext.Current.Session["Sf_Code"].ToString();
        string stockist_name = HttpContext.Current.Session["sf_name"].ToString();
        string Div_Code = HttpContext.Current.Session["div_code"].ToString();
        Div_Code = Div_Code.TrimEnd(',');
        string sf_type = HttpContext.Current.Session["sf_type"].ToString();
        string routecode = string.Empty;
        string mtranssl = string.Empty;
        string dcrcode = string.Empty;
        DataTable dslstDR = new DataTable();
        DB_EReporting db_ER = new DB_EReporting();
        Order Ord = new Order();
        if (sf_type == "4")
        {
            if (conn.State != ConnectionState.Closed)
            {
                conn.Close();
            }
            conn.Open();
            // tran = conn.BeginTransaction();
            using (var scope = new TransactionScope())
            {
                try
                {

                    DateTime localDate1 = DateTime.Now;
                    string localDate = localDate1.ToString("yyyy-MM-dd HH:mm:ss");
                    var items = JsonConvert.DeserializeObject<List<svorders>>(NewOrd);
                    ///*Finding Route Code*/
                    //SqlCommand cmd = new SqlCommand("select Territory_Code from Mas_ListedDr where ListedDrCode=" + RetCode + "", conn);
                    ////  cmd.Transaction = tran;
                    //SqlDataAdapter da = new SqlDataAdapter(cmd);
                    //da.Fill(dslstDR);
                    //routecode = dslstDR.Rows[0].ItemArray.GetValue(0).ToString();

                    ///*Insert into DCRMain_Trans*/
                    //cmd = new SqlCommand("exec svDCRMain_App_1 '" + Stockist_Code + "','" + localDate1.ToString("yyyy-MM-dd") + "',61,'" + routecode + "'," + Div_Code + ",'','',''", conn);
                    ////  cmd.Transaction = tran;
                    //da = new SqlDataAdapter(cmd);
                    //dslstDR = new DataTable();
                    //da.Fill(dslstDR);

                    //cmd = new SqlCommand("select Trans_SlNo from DCRMain_Trans where sf_code='" + Stockist_Code + "' and convert(date,Activity_Date)=convert(date,'" + localDate + "')", conn);
                    ////   cmd.Transaction = tran;
                    //dslstDR = new DataTable();
                    //da = new SqlDataAdapter(cmd);
                    //da.Fill(dslstDR);
                    //mtranssl = dslstDR.Rows[0].ItemArray.GetValue(0).ToString();//Main_Trans-->trans_sl_no

                    ///*Insert into DCRDetail_Lst_Trans*/
                    //cmd = new SqlCommand("exec svDCRLstDet_App '" + mtranssl + "',0,'" + Stockist_Code + "',1," + RetCode + ",'" + retnm + "','" + localDate + "',0,'admin','','','','','','','','','','" + routecode + "','" + Remark + "'," + Div_Code + ",0,'" + localDate + "','','','admin','NA','" + Ordrval + "','" + Ntwt + "','" + Stockist_Code + "','" + stockist_name + "','','',''", conn);
                    ////  cmd.Transaction = tran;
                    //da = new SqlDataAdapter(cmd);
                    //dslstDR = new DataTable(); ;
                    //da.Fill(dslstDR);

                    //cmd = new SqlCommand("select Trans_Detail_Slno from DCRDetail_Lst_Trans where Trans_SlNo='" + mtranssl + "' and Trans_Detail_Info_Code='" + RetCode + "'", conn);
                    ////  cmd.Transaction = tran;
                    //dslstDR = new DataTable();
                    //da = new SqlDataAdapter(cmd);
                    //da.Fill(dslstDR);
                    //dcrcode = dslstDR.Rows[0].ItemArray.GetValue(0).ToString();
                    // conn.Close();

                    string sxml = "<ROOT>";
                    for (int k = 0; k < items.Count; k++)
                    {
                        if (items[k].PCd != "" && items[k].PName != "" && (items[k].Qty != 0 || items[k].Qty_c != "0"))
                        {
                            sxml += "<Prod PCode=\"" + items[k].PCd + "\" PName=\"" + items[k].PName + "\" Rate=\"" + items[k].Rate + "\" Qty=\"" + items[k].Qty + "\" Val=\"" + items[k].Sub_Total + "\" FQty=\"" + items[k].Free + "\" DAmt=\"" + items[k].Dis_value + "\" Dval=\"" + items[k].Discount + "\" Md=\"free\" Mfg=\" \" Cl=\" \" Offer_ProductNm=\"" + items[k].of_Pro_Name + "\" Offer_ProductCd=\"" + items[k].of_Pro_Code + "\" Unit=\"" + items[k].Unit + "\"  of_Pro_Unit=\"" + items[k].of_Pro_Unit + "\"  Umo_Code=\"" + items[k].umo_unit + "\"  Qty_C=\"" + items[k].Qty_c + "\" Tax_value=\"" + items[k].Tax_value + "\" Sl_No=\"" + k + "\" con_fac=\"" + items[k].con_fac + "\"  />";
                        }
                    }
                    sxml += "</ROOT>";

                    string routc = null; string routar = null; string CollAmt = null;  string Disc = null; string DisAmt = null;string RateMode = null; string ARC = null;
                    //string qry = "exec sp_Sec_Order_Edit_Save  '" + Stockist_Code + "','" + RetCode + "','" + Stockist_Code + "','" + routc + "','" + routar + "','" + RecDate + "','" + Ordrval + "','" + CollAmt + "','" + Ntwt + "','" + Remark + "','" + Disc + "','" + DisAmt + "','" + RateMode + "','" + ARC + "'," + Div_Code + ",'" + sxml + "','" + Type + "','" + ref_order + "','" + sub_total + "','" + dis_total + "','" + tax_total + "','" + Ord_id + "'";
                    string strQry = "exec sp_Sec_Order_Edit_Save  '" + Stockist_Code + "','" + RetCode + "','" + Stockist_Code + "','" + routc + "','" + routar + "','" + RecDate + "','" + Ordrval + "','" + CollAmt + "','" + Ntwt + "','" + Remark + "','" + Disc + "','" + DisAmt + "','" + RateMode + "','" + ARC + "'," + Div_Code + ",'" + sxml + "','" + Type + "','" + ref_order + "','" + sub_total + "','" + dis_total + "','" + tax_total + "','" + Ord_id + "'";
                    int result = 0;
                    
                        result = db_ER.ExecQry(strQry);
                    
                            
                    //SqlCommand cmd = new SqlCommand("exec sp_Sec_Order_Edit_Save  '" + Stockist_Code + "','" + RetCode + "','" + Stockist_Code + "','" + routc + "','" + routar + "','" + RecDate + "','" + Ordrval + "','" + CollAmt + "','" + Ntwt + "','" + Remark + "','" + Disc + "','" + DisAmt + "','" + RateMode + "','" + ARC + "'," + Div_Code + ",'" + sxml + "','" + Type + "','" + ref_order + "','" + sub_total + "','" + dis_total + "','" + tax_total + "','" + Ord_id + "'", conn);
                    //SqlDataAdapter da = new SqlDataAdapter(cmd);
                    //cmd.ExecuteNonQuery();
                    msg = "Success";
                    conn.Close();
                    scope.Complete();
                    scope.Dispose();
                }
                catch (Exception exp)
                {
                    scope.Dispose();
                    throw exp;
                }
            }
        }

        return msg;
    }

    public class svorders
    {
        public string PCd { get; set; }
        public string PName { get; set; }
        public string Unit { get; set; }
        public float Rate { get; set; }
        public int Qty { get; set; }
        public float Sub_Total { get; set; }
        public float Free { get; set; }
        public float Dis_value { get; set; }
        public float Discount { get; set; }
        public string of_Pro_Code { get; set; }
        public string of_Pro_Name { get; set; }
        public string of_Pro_Unit { get; set; }
        public string umo_unit { get; set; }
        public string Qty_c { get; set; }
        public string Tax_value { get; set; }
        public string con_fac { get; set; }
    }
}