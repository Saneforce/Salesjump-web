using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Globalization;
using Bus_EReport;
using System.Data;
using System.Web.Services;

public partial class MIS_Reports_rptProductcatAnalysis : System.Web.UI.Page
{
    #region "Declaration"    
    string DivCode = string.Empty;
    string FYear = string.Empty;
    string FMonth = string.Empty;
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        DivCode = Session["div_code"].ToString();
        FYear = Request.QueryString["FYear"].ToString();
        FMonth = Request.QueryString["FMonth"].ToString();
        hdnMonth.Value = FMonth;
        hdnYear.Value = FYear;
        DateTimeFormatInfo mfi = new DateTimeFormatInfo();
        lblhead1.Text = "Product wise Analysis for " + mfi.GetAbbreviatedMonthName(Convert.ToInt32(FMonth)) + " - " + FYear;
        Label2.Text = "Loading Please Wait..!";
    }
    public class ProductList
    {
        public string PCode { get; set; }
        public string PName { get; set; }
        public string OrdQty { get; set; }
        public string OrdVal { get; set; }
        public string OrdPer { get; set; }
        public string TSR { get; set; }
        public string CntSR { get; set; }
        public string TPC { get; set; }
        public string CntPC { get; set; }
        public string TUPC { get; set; }
        public string CntUPC { get; set; }
    }
    [WebMethod(EnableSession = true)]
    public static ProductList[] GetProductDtls(string FYera, string FMonth)
    {
        string div_code = "";
        try
        {
            div_code = HttpContext.Current.Session["div_code"].ToString();
        }
        catch
        {
            div_code = HttpContext.Current.Session["Division_Code"].ToString();
        }
        Product prd = new Product();
        DataSet dsPrdDtls = new DataSet();
        dsPrdDtls = prd.GetProductAnalysisData(div_code,"", FYera, FMonth,"");
        List<ProductList> PrdList = new List<ProductList>();
        foreach (DataRow row in dsPrdDtls.Tables[0].Rows)
        {
            ProductList pl = new ProductList();
            pl.PCode = row["Product_Detail_Code"].ToString();
            pl.PName = row["Product_Detail_Name"].ToString();
            pl.OrdQty = row["OrdQty"].ToString();
            pl.OrdVal = row["OrdVal"].ToString();
            pl.OrdPer = row["OrdPer"].ToString();
            pl.TSR = row["TSR"].ToString();
            pl.CntSR = row["CntSR"].ToString();
            pl.TPC = row["TPC"].ToString();
            pl.CntPC = row["CntPC"].ToString();
            pl.TUPC = row["TUPC"].ToString();
            pl.CntUPC = row["CntUPC"].ToString();
            PrdList.Add(pl);
        }
        return PrdList.ToArray();
    }
}
