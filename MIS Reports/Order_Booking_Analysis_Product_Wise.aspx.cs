using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data;
using System.Configuration;
using Bus_EReport;
using System.Windows;
using System.IO;
using System.Data;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html;
using iTextSharp.text.html.simpleparser;
using System.Text;
using Bus_EReport;
using iTextSharp.tool.xml;
using System.Net;
using System.Text.RegularExpressions;

using System.Web.Services;
using System.Data.SqlClient;
using Newtonsoft.Json;

public partial class MIS_Reports_Order_Booking_Analysis_Product_Wise : System.Web.UI.Page
{
    #region "Declaration"
    string sfCode = string.Empty;
    string sfname = string.Empty;
    string divcode = string.Empty;
    string sf_type = string.Empty;
    string FMonth = string.Empty;
    string FYear = string.Empty;
    string type = string.Empty;
    string h = string.Empty;
    string wrktypename = string.Empty;
    int sum_time = 0;
    DataSet dsSalesForce = new DataSet();
    DataSet dsdatee = new DataSet();
    DataSet dsDoc = null;
    DataSet dsGV = null;
    DateTime dtCurrent;
    TimeSpan ff;
    int rowspan = 0;
    string sCurrentDate = string.Empty;
    string endTime = string.Empty;
    string startTime = string.Empty;
    string tot_dr = string.Empty;
    string tot_value = string.Empty;
    string con_qty = string.Empty;
    string ec = string.Empty;
    string Monthsub = string.Empty;
    string date = string.Empty;
    string endd = string.Empty;
    DataSet dsDoctor = new DataSet();
    string gg = string.Empty;
    string imagepath = string.Empty;
    int quantity2 = 0;
    string mode = string.Empty;
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        divcode = Session["div_code"].ToString();

        sfname = Request.QueryString["sfname"].ToString();
        sfCode = Request.QueryString["sfcode"].ToString();
        imagepath = Request.QueryString["imgpath"].ToString();

        logoo.ImageUrl = imagepath;
        //sfCode = "Admin";

        if (sfCode.Contains("MGR"))
        {
            sf_type = "2";
        }
        else if (sfCode.Contains("MR"))
        {
            sf_type = "1";
        }
        else
        {
            sf_type = "0";
        }


        FMonth = Request.QueryString["FMonth"].ToString();
        FYear = Request.QueryString["FYear"].ToString();

        System.Globalization.DateTimeFormatInfo mfi = new System.Globalization.DateTimeFormatInfo();
        string strFMonthName = mfi.GetMonthName(Convert.ToInt16(FMonth)).ToString().Substring(0, 3);
        lblHead.Text = "Order Booking Analysis Product wise View for the Month of " + strFMonthName + " " + FYear;

        ddlFMonth.Value = FMonth;
        lblsf_name.Text = sfname;
        lblyear.Value = FYear;
        hidn_sf_code.Value = Request.QueryString["sfCode"].ToString();


    }
   
    protected void btnPrint_Click(object sender, EventArgs e)
    {
       // Session["ctrl"] = pnlContents;
        Control ctrl = (Control)Session["ctrl"];
        PrintWebControl(ctrl);
    }
    public static void PrintWebControl(Control ControlToPrint)
    {
        StringWriter stringWrite = new StringWriter();
        System.Web.UI.HtmlTextWriter htmlWrite = new System.Web.UI.HtmlTextWriter(stringWrite);
        if (ControlToPrint is WebControl)
        {
            Unit w = new Unit(100, UnitType.Percentage);
            ((WebControl)ControlToPrint).Width = w;
        }
        Page pg = new Page();
        pg.EnableEventValidation = false;
        HtmlForm frm = new HtmlForm();
        pg.Controls.Add(frm);
        frm.Attributes.Add("runat", "server");
        frm.Controls.Add(ControlToPrint);
        pg.DesignerInitialize();
        pg.RenderControl(htmlWrite);
        string strHTML = stringWrite.ToString();
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.Write(strHTML);
        HttpContext.Current.Response.Write("<script>window.print();</script>");
        HttpContext.Current.Response.End();

    }

    protected void btnExcel_Click(object sender, EventArgs e)
    {
    //    string strFileName = Page.Title;
    //    string attachment = "attachment; filename=" + strFileName + ".xls";
    //    Response.ClearContent();
    //    Response.AddHeader("content-disposition", attachment);
    //    Response.ContentType = "application/ms-excel";
    //    StringWriter sw = new StringWriter();
    //    HtmlTextWriter htw = new HtmlTextWriter(sw);
    //    HtmlForm frm = new HtmlForm();
    //    form1.Parent.Controls.Add(frm);
    //    frm.Attributes["runat"] = "server";
    //    frm.Controls.Add(pnlContents);
    //    frm.RenderControl(htw);
    //    Response.Write(sw.ToString());
    //    Response.End();
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {

        string strFileName = Page.Title;

        using (StringWriter sw = new StringWriter())
        {
            using (HtmlTextWriter hw = new HtmlTextWriter(sw))
            {
                //this.Page.RenderControl(hw);
                this.RenderControl(hw);
                StringReader sr = new StringReader(sw.ToString());
                Document pdfDoc = new Document(PageSize.A4,
                    10f, 10f, 10f, 0f);
                PdfWriter writer = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                pdfDoc.Open();
                XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                pdfDoc.Close();
                Response.ContentType = "application/pdf";
                Response.AddHeader("content-disposition", "attachment;filename= '" + strFileName + "'.pdf");
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.Write(pdfDoc);
                Response.End();
            }

        }
    }



    protected void btnClose_Click(object sender, EventArgs e)
    {

    }
    public class Item
    {
        public string product_id { get; set; }
        public string product_name { get; set; }
    }

    [WebMethod(EnableSession = true)]
    public static Item[] getdata()
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
        DataSet dsAccessmas = pro.getproductname(div_code.TrimEnd(','));
        foreach (DataRow row in dsAccessmas.Tables[0].Rows)
        {
            Item emp = new Item();
            emp.product_id = row["Product_Detail_Code"].ToString();
            emp.product_name = row["Product_Short_Name"].ToString();
            empList.Add(emp);
        }
        return empList.ToArray();
    }



    [WebMethod(EnableSession = true)]
    public static IssueDetails[] getIssuData(string SF_Code, string FYera, string FMonth)
    {
        string div_code = "";
        string sf_Code = "";
        try
        {
            div_code = HttpContext.Current.Session["div_code"].ToString();

        }
        catch
        {
            div_code = HttpContext.Current.Session["Division_Code"].ToString();
        }

        sf_Code = HttpContext.Current.Session["SF_Code"].ToString();


        Product pro = new Product();
        List<IssueDetails> empList = new List<IssueDetails>();
        Expense exp = new Expense();
        DataSet dsPro = exp.getrptIssueSlip_Month(div_code, SF_Code, FYera, FMonth, "0");
        foreach (DataRow row in dsPro.Tables[0].Rows)
        {
            IssueDetails emp = new IssueDetails();
            emp.sfCode = row["SF_Code"].ToString();
            //emp.sfName = row["Sf_Name"].ToString();
            emp.proCode = row["Product_Detail_Code"].ToString();
            //emp.proName = row["Product_Short_Name"].ToString();
            emp.caseRate = row["Quantity"].ToString();
            emp.amount = row["order_val"].ToString();
           // emp.TC_Count = row["TC_Count"].ToString();
           // emp.EC_Count = row["EC_Count"].ToString();
            empList.Add(emp);
        }
        return empList.ToArray();
    }

    public class IssueDetails
    {
        public string sfCode { get; set; }
        public string sfName { get; set; }
        public string proCode { get; set; }
        public string proName { get; set; }
        public string caseRate { get; set; }
        public string piceRate { get; set; }
        public string amount { get; set; }
        public string TC_Count { get; set; }
        public string EC_Count { get; set; }

    }

     [WebMethod(EnableSession = true)]
    public static FFNames[] getSalesforce(string SF_Code)
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

        SalesForce sf = new SalesForce();
        List<FFNames> empList = new List<FFNames>();
        DataSet dsAccessmas = sf.SalesForceList(div_code.TrimEnd(','),SF_Code);
        foreach (DataRow row in dsAccessmas.Tables[0].Rows)
        {
            FFNames emp = new FFNames();
            emp.sfCode = row["Sf_Code"].ToString();
            emp.sfName = row["Sf_Name"].ToString();
            empList.Add(emp);
        }
        return empList.ToArray();
    }

     public class FFNames
     {
         public string sfCode { get; set; }
         public string sfName { get; set; }
     }

     [WebMethod(EnableSession = true)]
     public static TotEcTc[] getIssuDataTcEc(string SF_Code, string FYera, string FMonth)
     {
         string div_code = "";
         string sf_Code = "";
         try
         {
             div_code = HttpContext.Current.Session["div_code"].ToString();

         }
         catch
         {
             div_code = HttpContext.Current.Session["Division_Code"].ToString();
         }

         sf_Code = HttpContext.Current.Session["SF_Code"].ToString();

         //   SubDiv = "0";

         Product pro = new Product();
         List<TotEcTc> empList = new List<TotEcTc>();
         Expense exp = new Expense();
         DataSet dsPro = exp.getrptIssueSlip_MonthTCEC(div_code, SF_Code, FYera, FMonth, "0");
         foreach (DataRow row in dsPro.Tables[0].Rows)
         {
             TotEcTc emp = new TotEcTc();
             emp.sfCode = row["SF_Code"].ToString();
             emp.TC_Count = row["TC_Count"].ToString();
             emp.EC_Count = row["EC_Count"].ToString();
             empList.Add(emp);
         }
         return empList.ToArray();
     }

     public class TotEcTc
     {
         public string sfCode { get; set; }
         public string TC_Count { get; set; }
         public string EC_Count { get; set; }

     }
    
}