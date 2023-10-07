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

public partial class Stockist_myOrders : System.Web.UI.Page
{
    #region "Declaration"
    public string div_code;
    public string div_code1;
    public string sf_code = string.Empty;
    public static string sf_type = string.Empty;

    public static SqlConnection conn = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString);
    #endregion
    protected override void OnPreInit(EventArgs e)
    {
        base.OnPreInit(e);
        sf_type = Session["sf_type"].ToString();
        if (sf_type == "3")
        {
            this.MasterPageFile = "~/Master_DIS.master";
        }
        else if (sf_type == "2")
        {
            this.MasterPageFile = "~/Master_MGR.master";
        }
        else if (sf_type == "1")
        {
            this.MasterPageFile = "~/Master_MR.master";
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
    }
    [WebMethod]
    public static string GetOrders(string stk, string FDt, string TDt)
    {
        Bus_EReport.Order Ord = new Bus_EReport.Order();
        string typ = (HttpContext.Current.Session["sf_type"].ToString()) == "4" ? "4" : "5";
        //DataSet ds = Ord.getRetailOrdersByDist(HttpContext.Current.Session["Sf_Code"].ToString(), FDt, TDt, typ, HttpContext.Current.Session["div_code"].ToString());
        DataSet ds = getRetailOrdersByDist(HttpContext.Current.Session["Sf_Code"].ToString(), FDt, TDt, typ, HttpContext.Current.Session["div_code"].ToString());
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    public static DataSet getRetailOrdersByDist(string Stk, string FDate, string TDate, string type, string div)
    {
        DB_EReporting db_ER = new DB_EReporting();
        DataSet dsState = null;

        string strQry = "exec getRetailOrderByStk '" + Stk + "','" + FDate + "','" + TDate + "','" + type + "','" + div + "'";
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

    [WebMethod]
    public static string GetProducts(string div)
    {
        Product Rut = new Product();
        string stk = HttpContext.Current.Session["Sf_Code"].ToString();
        string state_code = HttpContext.Current.Session["State"].ToString();
        //DataSet ds = Rut.getProdall(div.Replace(",", ""), state_code, stk);
        DataSet ds = getProdall(div.Replace(",", ""), state_code, stk);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    public static DataSet getProdall(string divcode, string state_code, string stk)
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
    [WebMethod]
    public static string bindretailer(string stk, string Div)
    {
        StockistMaster sm = new StockistMaster();
        List<ListItem> retailer = new List<ListItem>();
        DataSet ds = new DataSet();
       // ds = sm.getretailerdetails(stk, Div.TrimEnd(',').TrimEnd(','), sf_type);
        //ds = sm.getretailerdetailsmyorder(stk, Div.TrimEnd(','));
        ds = getretailerdetailsmyorder(stk, Div.TrimEnd(','));
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    public static DataSet getretailerdetailsmyorder(string scode, string divcode)
    {
        DB_EReporting db_ER = new DB_EReporting();
        DataSet ds = null;

        try
        {
           string  strQry = "EXEC spGetRetailerName '" + scode + "','" + divcode + "'";
            ds = db_ER.Exec_DataSet(strQry);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return ds;
    }
    public class svorders
    {
        public string PCd { get; set; }
        public string PName { get; set; }
        public string Unit { get; set; }
		public string Unitcode { get; set; }
        public float Rate { get; set; }
        public float Rate_in_peice { get; set; }
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
        public string mrp { get; set; }
    }

    public class svorders_tax_details
    {
        public string pro_code { get; set; }
        public string Tax_Code { get; set; }
        public float Tax_Amt { get; set; }
        public string Tax_Name { get; set; }
        public float Tax_Per { get; set; }
		public string umo_code { get; set; }
    }


    [WebMethod]
    public static string saveorders(string NewOrd,string NewOrd_Tax_Details, string Remark, string Ordrval, string RetCode, string RecDate, string Ntwt, string retnm, string Type, string ref_order, string sub_total, string dis_total, string tax_total,
        string Extra_Tax_type, string Extra_Tax_value)
    {
        string order_id = string.Empty;
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
        DataSet ds1 = new DataSet();
        Bus_EReport.Order Ord = new Bus_EReport.Order();
        if (sf_type == "4")
        {
            if (conn.State != ConnectionState.Closed)
            {
                conn.Close();
            }
            conn.Open();
            using (var scope = new TransactionScope())
            {
                string strQry = "";
                try
                {
                    DateTime localDate1 = DateTime.Now;
                    string localDate = localDate1.ToString("yyyy-MM-dd HH:mm:ss");
                    var items = JsonConvert.DeserializeObject<List<svorders>>(NewOrd);
                    var Tax_items = JsonConvert.DeserializeObject<List<svorders_tax_details>>(NewOrd_Tax_Details);
                    /*Finding Route Code*/
                    //SqlCommand cmd = new SqlCommand("select Territory_Code from Mas_ListedDr where ListedDrCode=" + RetCode + "", conn);
                    //SqlDataAdapter da = new SqlDataAdapter(cmd);
                    //da.Fill(dslstDR);
                    DB_EReporting db_ER = new DB_EReporting();
                    dslstDR = new DataTable();
                    strQry = "select Territory_Code from Mas_ListedDr where ListedDrCode=" + RetCode + "";
                    ds1 = db_ER.Exec_DataSet(strQry);
                    dslstDR = ds1.Tables[0];
                    routecode = dslstDR.Rows[0].ItemArray.GetValue(0).ToString();

                    /*Insert into DCRMain_Trans*/
                    //SqlCommand cmd = new SqlCommand("exec svDCRMain_App_1 '" + Stockist_Code + "','" + localDate1.ToString("yyyy-MM-dd") + "',61,'" + routecode + "'," + Div_Code + ",'','',''", conn);
                    //strQry = "second";
                    //SqlDataAdapter da = new SqlDataAdapter(cmd);
                    dslstDR = new DataTable();
                    strQry = "exec svDCRMain_App_1 '" + Stockist_Code + "','" + localDate1.ToString("yyyy-MM-dd") + "',61,'" + routecode + "'," + Div_Code + ",'','',''";
                    ds1 = db_ER.Exec_DataSet(strQry);
                    //dslstDR = ds1.Tables[0];
                    //da.Fill(dslstDR);

                    //cmd = new SqlCommand("select Trans_SlNo from DCRMain_Trans where sf_code='" + Stockist_Code + "' and convert(date,Activity_Date)=convert(date,'" + localDate + "')", conn);
                   
                    //da = new SqlDataAdapter(cmd);
                    //da.Fill(dslstDR);
                    dslstDR = new DataTable();
                    strQry = "select Trans_SlNo from DCRMain_Trans where sf_code='" + Stockist_Code + "' and convert(date,Activity_Date)=convert(date,'" + localDate + "')";
                    ds1 = db_ER.Exec_DataSet(strQry);
                    dslstDR = ds1.Tables[0];
                    mtranssl = dslstDR.Rows[0].ItemArray.GetValue(0).ToString();//Main_Trans-->trans_sl_no
                    
                    /*Insert into DCRDetail_Lst_Trans*/
                    //SqlCommand cmd = new SqlCommand("exec svDCRLstDet_App '" + mtranssl + "',0,'" + Stockist_Code + "',1," + RetCode + ",'" + retnm + "','" + localDate + "',0,'admin','','','','','','','','','','" + routecode + "','" + Remark + "'," + Div_Code + ",0,'" + localDate + "','','','admin','NA','" + Ordrval + "','" + Ntwt + "','" + Stockist_Code + "','" + stockist_name + "','','',''", conn);
                    //SqlDataAdapter da = new SqlDataAdapter(cmd);
                    dslstDR = new DataTable();
                    strQry = "exec svDCRLstDet_App '" + mtranssl + "',0,'" + Stockist_Code + "',1," + RetCode + ",'" + retnm + "','" + localDate + "',0,'admin','','','','','','','','','','" + routecode + "','" + Remark + "'," + Div_Code + ",0,'" + localDate + "','','','admin','NA','" + Ordrval + "','" + Ntwt + "','" + Stockist_Code + "','" + stockist_name + "','','',''";
                    ds1 = db_ER.Exec_DataSet(strQry);
                    //dslstDR = ds1.Tables[0];
                    

                    //cmd = new SqlCommand("select Trans_Detail_Slno from DCRDetail_Lst_Trans where Trans_SlNo='" + mtranssl + "' and Trans_Detail_Info_Code='" + RetCode + "'", conn);
                    ////  cmd.Transaction = tran;
                    //dslstDR = new DataTable();
                    //da = new SqlDataAdapter(cmd);
                    //da.Fill(dslstDR);
                    dslstDR = new DataTable();
                    strQry = "select Trans_Detail_Slno from DCRDetail_Lst_Trans where Trans_SlNo='" + mtranssl + "' and Trans_Detail_Info_Code='" + RetCode + "'";
                    ds1 = db_ER.Exec_DataSet(strQry);
                    dslstDR = ds1.Tables[0];
                    dcrcode = dslstDR.Rows[0].ItemArray.GetValue(0).ToString();

                    string sxml = "<ROOT>";
                    for (int k = 0; k < items.Count; k++)
                    {
                        if (items[k].PCd != "" && items[k].PName != "" && items[k].Qty != 0)
                        {
                            sxml += "<Prod PCode=\"" + items[k].PCd + "\" PName=\"" + items[k].PName + "\" Rate=\"" + items[k].Rate + "\" Rate_in_peice=\"" + items[k].Rate_in_peice + "\" Qty=\"" + items[k].Qty + "\" Val=\"" + items[k].Sub_Total + "\" FQty=\"" + items[k].Free + "\" DAmt=\"" + items[k].Dis_value + "\" Dval=\"" + items[k].Discount + "\" Md=\"free\" Mfg=\" \" Cl=\" \" Offer_ProductNm=\"" + items[k].of_Pro_Name + "\" Offer_ProductCd=\"" + items[k].of_Pro_Code + "\" Unit=\"" + items[k].Unit + "\"  of_Pro_Unit=\"" + items[k].of_Pro_Unit + "\"  Umo_Code=\"" + items[k].umo_unit + "\"  Qty_C=\"" + items[k].Qty_c + "\" Tax_value=\"" + items[k].Tax_value + "\" Sl_No=\"" + k + "\" con_fac=\"" + items[k].con_fac + "\" mrp=\"" + items[k].mrp + "\"  />";
                        }
                    }
                    sxml += "</ROOT>";
                    string sxml1 = "<ROOT>";
                    if (NewOrd_Tax_Details != null && NewOrd_Tax_Details!="")
                    {
                       
                        for (int k = 0; k < Tax_items.Count; k++)
                        {
                            sxml1 += "<Tax_details pro_code=\"" + Tax_items[k].pro_code + "\" Tax_Code=\"" + Tax_items[k].Tax_Code + "\" Tax_Amt=\"" + Tax_items[k].Tax_Amt + "\" Tax_Name=\"" + Tax_items[k].Tax_Name + "\" Tax_Per=\"" + Tax_items[k].Tax_Per + "\" umo_code=\"" + Tax_items[k].umo_code + "\"  />";
                        }
                        sxml1 += "</ROOT>";
                    }
                    else
                        sxml1 += "</ROOT>";



                    string routc = null; string routar = null; string CollAmt = null; string Disc = null; string DisAmt = null; string RateMode = null; string ARC = null;
                    //SqlCommand cmd1 = new SqlCommand("EXEC svsecorder '" + Stockist_Code + "','" + RetCode + "','" + Stockist_Code + "','" + routc + "','" + routar + "','" + RecDate + "','" + Ordrval + "','" + CollAmt + "','" + Ntwt + "','" + Remark + "','" + Disc + "','" + DisAmt + "','" + RateMode + "','" + ARC + "'," + Div_Code + ",'" + sxml + "','" + Type + "','" + ref_order + "','" + sub_total + "','" + dis_total + "','" + tax_total + "','"+ sxml1 + "'", conn);

                    //SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
                    //da1.Fill(ds1);
                    strQry = "EXEC svsecorder '" + Stockist_Code + "','" + RetCode + "','" + Stockist_Code + "','" + routc + "','" + routar + "','" + RecDate + "','" + Ordrval + "','" + CollAmt + "','" + Ntwt + "','" + Remark + "','" + Disc + "','" + DisAmt + "','" + RateMode + "','" + ARC + "'," + Div_Code + ",'" + sxml + "','" + Type + "','" + ref_order + "','" + sub_total + "','" + dis_total + "','" + tax_total + "','" + sxml1 + "'";
                    ds1 = db_ER.Exec_DataSet(strQry);
                    order_id = ds1.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                    conn.Close();
                    scope.Complete();
                    scope.Dispose();
                }
                catch (Exception exp)
                {
                    scope.Dispose();
                    return strQry;
                }
            }
        }
        return order_id;
    }
    [WebMethod]
    public static string PendingOrders(string NewOrd, string Remark, string Ordrval, string RetCode, string RecDate, string Ntwt, string retnm, string Type, string ref_order, string sub_total, string dis_total, string tax_total)
    {
        string order_id = string.Empty;
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
        DataSet ds1 = new DataSet();
        Bus_EReport.Order Ord = new Bus_EReport.Order();
        if (sf_type == "4")
        {
            if (conn.State != ConnectionState.Closed)
            {
                conn.Close();
            }
            conn.Open();
            using (var scope = new TransactionScope())
            {
                string strQry = "";
                try
                {
                    DateTime localDate1 = DateTime.Now;
                    string localDate = localDate1.ToString("yyyy-MM-dd HH:mm:ss");
                    var items = JsonConvert.DeserializeObject<List<svorders>>(NewOrd);
                   // var Tax_items = JsonConvert.DeserializeObject<List<svorders_tax_details>>(NewOrd_Tax_Details);
                    /*Finding Route Code*/
                    //SqlCommand cmd = new SqlCommand("select Territory_Code from Mas_ListedDr where ListedDrCode=" + RetCode + "", conn);
                    //SqlDataAdapter da = new SqlDataAdapter(cmd);
                    //da.Fill(dslstDR);
                    DB_EReporting db_ER = new DB_EReporting();
                    dslstDR = new DataTable();
                    strQry = "select Territory_Code from Mas_ListedDr where ListedDrCode=" + RetCode + "";
                    ds1 = db_ER.Exec_DataSet(strQry);
                    dslstDR = ds1.Tables[0];
                    routecode = dslstDR.Rows[0].ItemArray.GetValue(0).ToString();

                    /*Insert into DCRMain_Trans*/
                    //SqlCommand cmd = new SqlCommand("exec svDCRMain_App_1 '" + Stockist_Code + "','" + localDate1.ToString("yyyy-MM-dd") + "',61,'" + routecode + "'," + Div_Code + ",'','',''", conn);
                    //strQry = "second";
                    //SqlDataAdapter da = new SqlDataAdapter(cmd);
                    dslstDR = new DataTable();
                    strQry = "exec svDCRMain_App_1 '" + Stockist_Code + "','" + localDate1.ToString("yyyy-MM-dd") + "',61,'" + routecode + "'," + Div_Code + ",'','',''";
                    ds1 = db_ER.Exec_DataSet(strQry);
                    //dslstDR = ds1.Tables[0];
                    //da.Fill(dslstDR);

                    //cmd = new SqlCommand("select Trans_SlNo from DCRMain_Trans where sf_code='" + Stockist_Code + "' and convert(date,Activity_Date)=convert(date,'" + localDate + "')", conn);

                    //da = new SqlDataAdapter(cmd);
                    //da.Fill(dslstDR);
                    dslstDR = new DataTable();
                    strQry = "select Trans_SlNo from DCRMain_Trans where sf_code='" + Stockist_Code + "' and convert(date,Activity_Date)=convert(date,'" + localDate + "')";
                    ds1 = db_ER.Exec_DataSet(strQry);
                    dslstDR = ds1.Tables[0];
                    mtranssl = dslstDR.Rows[0].ItemArray.GetValue(0).ToString();//Main_Trans-->trans_sl_no

                    /*Insert into DCRDetail_Lst_Trans*/
                    //SqlCommand cmd = new SqlCommand("exec svDCRLstDet_App '" + mtranssl + "',0,'" + Stockist_Code + "',1," + RetCode + ",'" + retnm + "','" + localDate + "',0,'admin','','','','','','','','','','" + routecode + "','" + Remark + "'," + Div_Code + ",0,'" + localDate + "','','','admin','NA','" + Ordrval + "','" + Ntwt + "','" + Stockist_Code + "','" + stockist_name + "','','',''", conn);
                    //SqlDataAdapter da = new SqlDataAdapter(cmd);
                    dslstDR = new DataTable();
                    strQry = "exec svDCRLstDet_App '" + mtranssl + "',0,'" + Stockist_Code + "',1," + RetCode + ",'" + retnm + "','" + localDate + "',0,'admin','','','','','','','','','','" + routecode + "','" + Remark + "'," + Div_Code + ",0,'" + localDate + "','','','admin','NA','" + Ordrval + "','" + Ntwt + "','" + Stockist_Code + "','" + stockist_name + "','','',''";
                    ds1 = db_ER.Exec_DataSet(strQry);
                    //dslstDR = ds1.Tables[0];


                    //cmd = new SqlCommand("select Trans_Detail_Slno from DCRDetail_Lst_Trans where Trans_SlNo='" + mtranssl + "' and Trans_Detail_Info_Code='" + RetCode + "'", conn);
                    ////  cmd.Transaction = tran;
                    //dslstDR = new DataTable();
                    //da = new SqlDataAdapter(cmd);
                    //da.Fill(dslstDR);
                    dslstDR = new DataTable();
                    strQry = "select Trans_Detail_Slno from DCRDetail_Lst_Trans where Trans_SlNo='" + mtranssl + "' and Trans_Detail_Info_Code='" + RetCode + "'";
                    ds1 = db_ER.Exec_DataSet(strQry);
                    //dslstDR = ds1.Tables[0];
                    //dcrcode = dslstDR.Rows[0].ItemArray.GetValue(0).ToString();

                    string sxml = "<ROOT>";
                    for (int k = 0; k < items.Count; k++)
                    {
                        if (items[k].PCd != "" && items[k].PName != "" && items[k].Qty != 0)
                        {
                            sxml += "<Prod PCode=\"" + items[k].PCd + "\" PName=\"" + items[k].PName + "\" Rate=\"" + items[k].Rate + "\" Qty=\"" + items[k].Qty + "\" Val=\"" + items[k].Sub_Total + "\" FQty=\"" + items[k].Free + "\" DAmt=\"" + items[k].Dis_value + "\" Dval=\"" + items[k].Discount + "\" Md=\"free\" Mfg=\" \" Cl=\" \" Offer_ProductNm=\"" + items[k].of_Pro_Name + "\" Offer_ProductCd=\"" + items[k].of_Pro_Code + "\" Unit=\"" + items[k].Unit + "\"  of_Pro_Unit=\"" + items[k].of_Pro_Unit + "\"  Umo_Code=\"" + items[k].Unitcode + "\"  Qty_C=\"" + items[k].Qty_c + "\" Tax_value=\"" + items[k].Tax_value + "\" Sl_No=\"" + k + "\" con_fac=\"" + items[k].con_fac + "\" mrp=\"" + items[k].mrp + "\"  />";
                        }
                    }
                    sxml += "</ROOT>";
                    string sxml1 = "<ROOT>";
                    //if (NewOrd_Tax_Details != null && NewOrd_Tax_Details != "")
                    //{

                    //    for (int k = 0; k < Tax_items.Count; k++)
                    //    {
                    //        sxml1 += "<Tax_details pro_code=\"" + Tax_items[k].pro_code + "\" Tax_Code=\"" + Tax_items[k].Tax_Code + "\" Tax_Amt=\"" + Tax_items[k].Tax_Amt + "\" Tax_Name=\"" + Tax_items[k].Tax_Name + "\" Tax_Per=\"" + Tax_items[k].Tax_Per + "\" umo_code=\"" + Tax_items[k].umo_code + "\"  />";
                    //    }
                    //    sxml1 += "</ROOT>";
                    //}
                    //else
                        sxml1 += "</ROOT>";



                    string routc = null; string routar = null; string CollAmt = null; string Disc = null; string DisAmt = null; string RateMode = null; string ARC = null;
                    //SqlCommand cmd1 = new SqlCommand("EXEC svsecorder '" + Stockist_Code + "','" + RetCode + "','" + Stockist_Code + "','" + routc + "','" + routar + "','" + RecDate + "','" + Ordrval + "','" + CollAmt + "','" + Ntwt + "','" + Remark + "','" + Disc + "','" + DisAmt + "','" + RateMode + "','" + ARC + "'," + Div_Code + ",'" + sxml + "','" + Type + "','" + ref_order + "','" + sub_total + "','" + dis_total + "','" + tax_total + "','"+ sxml1 + "'", conn);

                    //SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
                    //da1.Fill(ds1);
                    strQry = "EXEC svsecorder '" + Stockist_Code + "','" + RetCode + "','" + Stockist_Code + "','" + routc + "','" + routar + "','" + RecDate + "','" + Ordrval + "','" + CollAmt + "','" + Ntwt + "','" + Remark + "','" + Disc + "','" + DisAmt + "','" + RateMode + "','" + ARC + "'," + Div_Code + ",'" + sxml + "','" + Type + "','" + ref_order + "','" + sub_total + "','" + dis_total + "','" + tax_total + "','" + sxml1 + "'";
                    ds1 = db_ER.Exec_DataSet(strQry);
                    order_id = ds1.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                    conn.Close();
                    scope.Complete();
                    scope.Dispose();
                }
                catch (Exception exp)
                {
                    scope.Dispose();
                    return strQry;
                }
            }
        }
        return order_id;
    }

    [WebMethod(EnableSession = true)]
    public static string getscheme(string date, string Div_Code, string Stockist_Code)
    {
        DataSet ds = new DataSet();
        StockistMaster sm = new StockistMaster();
       // ds = sm.Get_Sec_scheme(Stockist_Code, Div_Code.TrimEnd(','), date);
        ds = Get_Sec_scheme(Stockist_Code, Div_Code.TrimEnd(','), date);
        return JsonConvert.SerializeObject(ds.Tables[0]);

    }

    public static DataSet Get_Sec_scheme(string Stockist_Code, string Div_Code, string date)
    {

        DB_EReporting db_ER = new DB_EReporting();
        DataSet ds = null;

        try
        {
           string  strQry = "EXEC get_secondary_scheme '" + Stockist_Code + "','" + Div_Code + "','" + date + "'";
            //strQry = "select ms.*,mp.Sale_Erp_Code,Sample_Erp_Code from mas_scheme ms left join mas_product_detail mp on ms.Product_Code=mp.Product_Detail_Code where ms.Division_Code='" + Div_Code + "' and  CHARINDEX(','+cast('" + Stockist_Code + "' as varchar)+',',','+Stockist_Code+',')>0 and cast(convert(varchar, Effective_To, 101) as datetime) >= cast(convert(varchar,'" + date + "' ,101) as datetime)   and cast(convert(varchar, Effective_From, 101) as datetime) <= cast(convert(varchar, '" + date + "', 101) as datetime) order by Product_Code,cast(scheme as int) desc ";
            ds = db_ER.Exec_DataSet(strQry);
        }
        catch (Exception ex)
        {
            throw ex;
        }

        return ds;

    }
    [WebMethod(EnableSession = true)]
    public static string getratenew(string Div_Code, string Stockist_Code)
    {
        string State = HttpContext.Current.Session["State"].ToString();
        DataSet ds = new DataSet();
        StockistMaster sm = new StockistMaster();
        //ds = sm.get_rate_new_bystk(Div_Code.TrimEnd(','), Stockist_Code, State);
        ds = get_rate_new_bystk(Div_Code.TrimEnd(','), Stockist_Code, State);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    public static DataSet get_rate_new_bystk(string Div_Code, string Stockist_Code, string State)
    {
        DataSet ds = null;
        DB_EReporting db_ER = new DB_EReporting();

       string  strQry = "Exec getrate_new '" + Div_Code + "','" + Stockist_Code + "','" + State + "'";

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
    public static string Get_Product_unit()
    {
        string Stockist_Code = HttpContext.Current.Session["Sf_Code"].ToString();
        string div_code = HttpContext.Current.Session["div_code"].ToString();
        div_code = div_code.TrimEnd(',');
        DataSet ds = new DataSet();
        StockistMaster sm = new StockistMaster();
       // ds = sm.gets_Product_unit_details(div_code, Stockist_Code);
        ds = gets_Product_unit_details(div_code, Stockist_Code);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }

    public static DataSet gets_Product_unit_details(string Div_Code, string Stockist_Code)
    {
        DataSet ds = null;
        DB_EReporting db_ER = new DB_EReporting();

        string strQry = "Exec get_Hap_pro_unit '" + Div_Code + "','" + Stockist_Code + "'";

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
    public static string Get_Product_Tax()
    {
        string Stockist_Code = HttpContext.Current.Session["Sf_Code"].ToString();
        string div_code = HttpContext.Current.Session["div_code"].ToString();
        string state_code = HttpContext.Current.Session["State"].ToString();
        div_code = div_code.TrimEnd(',');
        DataSet ds = new DataSet();
        StockistMaster sm = new StockistMaster();
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

    [WebMethod(EnableSession = true)]
    public static int SetNewStatus(string SF, string stus)
    {
        DSM dsm = new DSM();
        int iReturn = CancelOrder(SF, stus);
        return iReturn;
    }
    public static int CancelOrder(string orderid, string stus)
    {
        int iReturn = -1;
        string strQry;
        try
        {
            DB_EReporting db = new DB_EReporting();
            strQry = "update Trans_Order_Head set Order_Flag ='" + stus + "'where Trans_Sl_No = '" + orderid + "'";
            iReturn = db.ExecQry(strQry);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return iReturn;
    }
    [WebMethod(EnableSession = true)]
    public static int cancelorder(string orderid,string stockist,string div)
    {
        int iReturn = -1;
        string strQry;
        try
        {
            DB_EReporting db = new DB_EReporting();
            strQry = "exec Cancel_tran_order '"+orderid+ "','" + stockist + "','" + div + "'";
            iReturn = db.ExecQry(strQry);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return iReturn;
    }

    [WebMethod(EnableSession = true)]
    public static string Get_Product_Cat_Details()
    {
        string Stockist_Code = HttpContext.Current.Session["Sf_Code"].ToString();
        string div_code = HttpContext.Current.Session["div_code"].ToString();   
        div_code = div_code.TrimEnd(',');
        DataSet ds = new DataSet();
        StockistMaster sm = new StockistMaster();
        //ds = sm.Get_Product_Cat_Details(div_code, Stockist_Code);
        ds = Get_Product_Cat_Details(div_code, Stockist_Code);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    public static DataSet Get_Product_Cat_Details(string Div_Code, string stk)
    {
        DB_EReporting db_ER = new DB_EReporting();
        DataSet ds = null;
        string strQry = "EXEC sp_get_cat_Details '" + Div_Code + "','" + stk + "'";
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
    public static string Get_Route()
    {
        string Stockist_Code = HttpContext.Current.Session["Sf_Code"].ToString();
        string div_code = HttpContext.Current.Session["div_code"].ToString();
        div_code = div_code.TrimEnd(',');
        DataSet ds = getDataSet("SELECT a.Territory_Code,a.Route_Code,a.Territory_Name,a.Dist_Name FROM  Mas_Territory_Creation a with (nolock) where charindex(',' + cast('"+ Stockist_Code + "' as varchar) + ',',',' + a.Dist_Name + ',')> 0 and a.Territory_Active_Flag = 0 and a.Division_Code = '"+ div_code + "' order by Territory_SNo");
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
    public static string GetCustWise_price()
    {
        string Stockist_Code = HttpContext.Current.Session["Sf_Code"].ToString();
        string retailercode = "";
        string div_code = HttpContext.Current.Session["div_code"].ToString();
        div_code = div_code.TrimEnd(',');
        string state_code = HttpContext.Current.Session["State"].ToString();
        DataSet ds = new DataSet();
        StockistMaster sm = new StockistMaster();
        string strQry = string.Empty;
        DB_EReporting db_ER = new DB_EReporting();
        strQry = "EXEC sp_get_Price_Details '" + div_code + "','" + Stockist_Code + "','"+ state_code + "','"+retailercode+"'";
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

    [WebMethod]
    public static string ChangeOrder(string OrderNo,string Invoice_Date)
    {
        DataSet ds = new DataSet();
        string st = "";
        try
        {
            string div_code = HttpContext.Current.Session["div_code"].ToString();
            string StkCode = HttpContext.Current.Session["Sf_Code"].ToString();
            //string[] split = OrderNo.Split(',');
            //for (int k = 0; k < split.Length; k++)
            //{
            //    int l = sv_inv(split[k], StkCode, Type);
            //}

             ds = new_sv_inv(OrderNo, StkCode, Invoice_Date);

        }
        catch (Exception ex)
        {
            throw ex;
        }
        if(ds.Tables.Count > 0)
        {
            if (ds.Tables[0].Rows[0]["Result"].ToString() == "No_stock")
            {
                st = "No_stock";
            }
            else
                st = "success";
        }

        return st;

    }

    public static int sv_inv(string Order_Data, string StkCode,string invDate)
    {
        DB_EReporting db_ER = new DB_EReporting();
        int i = 0;
        string strQry = "Exec sp_Save_Trans_InvoiceData '" + Order_Data + "','" + StkCode + "','" + invDate + "'";
        try
        {
            i = db_ER.ExecQry(strQry);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return i;
    }
	public static DataSet new_sv_inv(string Order_Data, string StkCode, string invDate)
    {
        DB_EReporting db_ER = new DB_EReporting();
        DataSet i = new DataSet();
        string strQry = "Exec new_sp_Save_Trans_InvoiceData '" + Order_Data + "','" + StkCode + "','" + invDate + "'";
        try
        {
            i = db_ER.Exec_DataSet(strQry);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return i;
    }	

    [WebMethod]
    public static string AllPendingOrder(string OrderNo,string Inv_Date)
    {       
        using (var scope = new TransactionScope())
        {
            try
            {
                IList<Trans_Order> Orderhead = Newtonsoft.Json.JsonConvert.DeserializeObject<IList<Trans_Order>>(OrderNo);
                string StkCode = HttpContext.Current.Session["Sf_Code"].ToString();
				
                for (int i = 0; i < Orderhead.Count; i++)
                {
                    int l = sv_inv(Orderhead[i].OrderNum, StkCode, Inv_Date);
                }
                scope.Complete();
                scope.Dispose();
            }
            catch (Exception ex)
            {
                scope.Dispose();
                throw ex;
            }
        }

        return "success";

    }
    public class Trans_Order
    {
        public string OrderNum { get; set; }
       
    }

    public static string save_AllPendingOrder(string sxml1, string StkCode)
    {
        DB_EReporting db_ER = new DB_EReporting();
        string i = "";
        DataSet ds = null;
        string strQry = "Exec sp_Save_All_PendingOrder '" + sxml1 + "','" + StkCode + "'";
        try
        {
            ds = db_ER.Exec_DataSet(strQry);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return i;
    }
	
	//Print Related

    [WebMethod]
    public static string GetProductdetails(string Order_Id)
    {
        DataSet ds = getDataSet("exec Sp_Get_Sec_Print_Prd '" + Order_Id + "'");
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }

    [WebMethod]
    public static string GetDistributor(string Order_Id, string Stockist, string Division, string Cust_code)
    {
        DataSet ds = getDataSet("exec Sp_Get_Sec_Print_DIs '" + Division + "','" + Stockist + "','" + Cust_code + "','" + Order_Id + "'");
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    [WebMethod]
    public static string Get_Sec_Print_tax_Details(string Order_Id)
    {
        DataSet ds = getDataSet("exec Sp_Get_Sec_Print_tax_Details '" + Order_Id + "'");
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
}
