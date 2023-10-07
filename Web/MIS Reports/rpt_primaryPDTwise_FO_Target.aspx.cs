using Bus_EReport;
using DBase_EReport;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MIS_Reports_rpt_primaryPDTwise_FO_Target : System.Web.UI.Page
{
    string sfCode = string.Empty;
    string divcode = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        divcode = Session["div_code"].ToString();
        sfCode = Request.QueryString["SF_Code"].ToString();
        // FYear = Request.QueryString["FYear"].ToString();
        hidn_sf_code.Value = sfCode;
        hFYear.Value = Request.QueryString["FYear"].ToString();
        hFMonth.Value = Request.QueryString["FMonth"].ToString();
        hTYear.Value = Request.QueryString["TYear"].ToString();
        hTMonth.Value = Request.QueryString["TMonth"].ToString();
        hTMonth.Value = Request.QueryString["TMonth"].ToString();
        lblsf_name.Text = Request.QueryString["SF_Name"].ToString();

        subDiv.Value = Request.QueryString["subDiv"].ToString();

        DateTimeFormatInfo mfi = new DateTimeFormatInfo();
        string strFmonth = mfi.GetMonthName(Convert.ToInt32(Request.QueryString["FMonth"].ToString())).ToString(); //August

        string strTmonth = mfi.GetMonthName(Convert.ToInt32(Request.QueryString["TMonth"].ToString())).ToString(); //August



        lblyear.Text = strFmonth.Substring(0, 3) + " - " + Request.QueryString["FYear"].ToString() + " To " + strTmonth.Substring(0, 3) + " - " + Request.QueryString["TYear"].ToString(); ;
    }
    public class Item
    {
        public string product_id { get; set; }
        public string product_name { get; set; }
    }
    [WebMethod(EnableSession = true)]
    public static Item[] getdata(string SubDiv)
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

        Product pro = new Product();
        List<Item> empList = new List<Item>();
        DataSet dsAccessmas = pro.getproductname(div_code.TrimEnd(','), SubDiv);
        foreach (DataRow row in dsAccessmas.Tables[0].Rows)
        {
            Item emp = new Item();
            emp.product_id = row["Product_Detail_Code"].ToString();
            emp.product_name = row["Product_Detail_Name"].ToString();
            empList.Add(emp);
        }
        return empList.ToArray();
    }
    [WebMethod(EnableSession = true)]
    public static targetvsSales[] getProductTargetSale(string sf_Code, string fYear, string fMonth, string tYear, string tMonth)
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

        targetvsSales pro = new targetvsSales();
        List<targetvsSales> prolist = new List<targetvsSales>();
        DataSet dsAccessmas = pro.getPriProductwiseTargetSales(sf_Code, fYear, fMonth, tYear, tMonth, div_code);
        foreach (DataRow row in dsAccessmas.Tables[0].Rows)
        {
            targetvsSales pds = new targetvsSales();
            pds.pCode = row["Product_Code"].ToString();
            pds.cyear = row["cyear"].ToString();
            pds.cmonth = row["cmonth"].ToString();
            pds.orderVal = row["ord_val"].ToString();
            pds.target = row["target"].ToString();

            pds.CQty = row["CQty"].ToString();
            pds.PQty = row["PQty"].ToString();
            pds.Ctarqnty = row["Ctarqnty"].ToString();
            pds.Ptarqnty = row["Ptarqnty"].ToString();

            prolist.Add(pds);
        }
        return prolist.ToArray();
    }
    public class targetvsSales
    {
        public string pCode { get; set; }
        public string cyear { get; set; }
        public string cmonth { get; set; }
        public string orderVal { get; set; }
        public string target { get; set; }
        public string CQty { get; set; }
        public string PQty { get; set; }
        public string Ctarqnty { get; set; }
        public string Ptarqnty { get; set; }


        public DataSet getPriProductwiseTargetSales(string sfCode, string fYear, string fMonth, string tYear, string tMonth, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            string strQry = "[Get_PriProduct_Target_vs_Sal_FO] '" + sfCode + "','" + fYear + "','" + fMonth + "','" + tYear + "','" + tMonth + "','" + div_code + "'";
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