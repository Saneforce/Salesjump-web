using System;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Windows;
using System.IO;
using System.Data;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html;
using iTextSharp.text.html.simpleparser;
using System.Text;
using Bus_EReport;
using System.Net;
using iTextSharp.tool.xml;

using System.Web.Services;
using System.Runtime.Serialization.Json;

public partial class MIS_Reports_rpt_Customer_sales_analysis_days : System.Web.UI.Page
{
    string sfCode = string.Empty;
    string sfname = string.Empty;
    string divcode = string.Empty;
    decimal iTotLstCount1 = 0;
    decimal iTotLstCount = 0;
    decimal iTotLstCount2 = 0;
    int iMonth = -1;
    int iYear = -1;
    DataSet dsDoctor = null;
    string sMode = string.Empty;
    string FMonth = string.Empty;
    string FYear = string.Empty;
    string TMonth = string.Empty;
    string TYear = string.Empty;
    string stype = string.Empty;
    string Prod_Name = string.Empty;
    string sReturn = string.Empty;

    DataSet dsSalesForce = new DataSet();

    DataSet dsMGR = new DataSet();
    DataSet dsMr = new DataSet();
    DataSet dsDoc = null;
    DateTime dtCurrent;
    string tot_dr = string.Empty;
    string tot_Drr = string.Empty;

    string Monthsub = string.Empty;
    string strSf_Code = string.Empty;

    DataSet dsprd = new DataSet();
    string sCurrentDate = string.Empty;
    string stockist_code = string.Empty;
    string stURL = string.Empty;
    string Stock_name = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        divcode = Session["div_code"].ToString();
        sfCode = Request.QueryString["SF_Code"].ToString();
        FYear = Request.QueryString["FYear"].ToString();
        lblsf_name.Text = Request.QueryString["SF_Name"].ToString();
        FMonth = Request.QueryString["FMonth"].ToString();
        string month_na = Request.QueryString["MonthNa"].ToString();
        lblyear.Text = month_na + " - " + FYear;
        hidn_sf_code.Value = sfCode;
        hidnYears.Value = FYear;
        hidnMonths.Value = FMonth;
        FillSF();
    }

    private void FillSF()
    {
     GV_DATA.DataSource = null;
        GV_DATA.DataBind();
        RoutePlan rop = new RoutePlan();
        DataSet DsRoute = rop.get_Route_Name(divcode, sfCode);

        ListedDR ldr = new ListedDR();
        DataSet DsRetailer = ldr.Get_Retailer_sal_Days(divcode, FYear, FMonth, sfCode);

        DataTable dsData = new DataTable();

        dsData.Columns.Add("Code", typeof(string));
        dsData.Columns.Add("Name", typeof(string));
        dsData.Columns.Add("Category", typeof(string));
        dsData.Columns.Add("Channel", typeof(string));
        dsData.Columns.Add("Phone", typeof(string));        
        dsData.Columns.Add("Visited", typeof(string));
        for (int k = 1; k < DsRetailer.Tables[0].Columns.Count - 5; k++)
        {
            dsData.Columns.Add((k).ToString(), typeof(string));
        }
        dsData.Columns.Add("Total", typeof(string));
        dsData.Rows.Add("1", "Order Given Customers");
        dsData.Rows.Add("1", "Order Total");
        int i = 1;

        decimal[] over_tot = new decimal[50];
        Int32[] over_count = new Int32[50];
        foreach (DataRow dr in DsRoute.Tables[0].Rows)
        {
            decimal[] tot = new decimal[50];
          
            DataRow[] drow = DsRetailer.Tables[0].Select("Route = '" + dr["Territory_Code"].ToString() + "'");

            int stCount = drow.Length;
            dsData.Rows.Add("0", "Route no:- " + (i++) + " " + dr["Territory_Name"].ToString() + ",(" + stCount.ToString() + ") ");
            if (drow.Length > 0)
            {

                foreach (DataRow row in drow)
                {
                    string f1 = row[1].ToString();
                    string f2 = row[2].ToString();
                    string f3 = row[3].ToString();
                    string f4 = row[4].ToString();
                    string f5 = row[5].ToString();
                    string f6 = "";
                    dsData.Rows.Add(f1, f2,f3,f4,f5,f6);
                    int count = dsData.Rows.Count;
                    int m=0;
                    decimal rtot = 0;
                    for (int k = 6; k < DsRetailer.Tables[0].Columns.Count; k++)
                    {
                        decimal str = row[k] == DBNull.Value ? 0: Convert.ToDecimal(row[k]);
                        dsData.Rows[count - 1][k - 0] = str;
                        if (str > 0)
                            over_count[k - 6]++;
                        tot[k -6] += str <=0 ? 0 : Convert.ToDecimal(str);
                        rtot += str;
                        m = k;
                    }
                    dsData.Rows[count - 1][m+1] = rtot; 
                }
            }
            dsData.Rows.Add("", "Total","","","");
            int counts = dsData.Rows.Count;
            int mm = 0;
            decimal trtot = 0;
            for (int k = 6; k < DsRetailer.Tables[0].Columns.Count; k++)
            {
                dsData.Rows[counts - 1][k - 0] = tot[k -6].ToString();
                over_tot[k - 6] += Convert.ToDecimal(tot[k - 6]);
                trtot += tot[k - 6];
                mm = k;
            }
            dsData.Rows[counts - 1][mm+1] = trtot;
        }

        //totordervalues row
        int j = 0;
        decimal ttot = 0;
        for (int k = 6; k < DsRetailer.Tables[0].Columns.Count; k++)
        {
            dsData.Rows[1][k-0] = over_tot[k-6];
            ttot += over_tot[k - 6];
            j = k;
        }
        dsData.Rows[1][j+1] = ttot;

        // totordercount row
        decimal ctot = 0;
        j = 0;
        for (int k = 6; k < DsRetailer.Tables[0].Columns.Count; k++)
        {
            dsData.Rows[0][k-0] = over_count[k - 6];
            ctot += over_count[k - 6];
            j = k;
        }
        dsData.Rows[0][j+1] = ctot;

        GV_DATA.DataSource = dsData;
        GV_DATA.DataBind();

    }

    protected void Dgv_SKU_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            GridViewRow grv = e.Row;
            if (grv.Cells[0].Text.Equals("0"))
            {
                e.Row.BackColor = System.Drawing.Color.FromName("#39435C");
                e.Row.ForeColor = System.Drawing.Color.FromName("#fff");
                e.Row.Font.Bold = true;


            }
            if (grv.Cells[0].Text.Equals("1"))
            {
                e.Row.BackColor = System.Drawing.Color.FromName("#6b7794");
                e.Row.ForeColor = System.Drawing.Color.FromName("#fff");
                e.Row.Font.Bold = true;

            }
            if (grv.Cells[1].Text.Equals("Total"))
            {
                e.Row.BackColor = System.Drawing.Color.FromName("#99FFFF");
                e.Row.Font.Bold = true;

            }

            e.Row.Cells[0].Visible = true;
            e.Row.Cells[0].Width = 250;
            e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Left;
            e.Row.Cells[1].Width = 250;
            e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Left;
            e.Row.Cells[2].Width = 200;
            e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Left;
            e.Row.Cells[3].Width = 250;
            e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Left;
        }
        catch (Exception ex)
        { }

    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        Session["ctrl"] = pnlContents;
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
        string strFileName = Page.Title;
        string attachment = "attachment; filename='" + strFileName + "'.xls";

        Response.ClearContent();
        Response.AddHeader("content-disposition", attachment);
        Response.ContentType = "application/ms-excel";
        StringWriter sw = new StringWriter();
        HtmlTextWriter htw = new HtmlTextWriter(sw);
        HtmlForm frm = new HtmlForm();
        form1.Parent.Controls.Add(frm);
        frm.Attributes["runat"] = "server";
        frm.Controls.Add(pnlContents);
        frm.RenderControl(htw);
        Response.Write(sw.ToString());
        Response.End();
    }
    protected void btnClose_Click(object sender, EventArgs e)
    {
        // Response.Write("Purchase_Register_Distributor_wise.aspx");

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
                Document pdfDoc = new Document(PageSize.A3, 10f, 10f, 10f, 0f);
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



    public class VisitedList
    {
        public string custCode { get; set; }
        public string months { get; set; }
        public string counts { get; set; }
    }


    public class RepeatOrders
    {
        public string custCode { get; set; }
        public string months { get; set; }
        public string counts { get; set; }
    }


    public class TotCounts
    {
        public List<RepeatOrders> rOrder = new List<RepeatOrders>();
        public List<VisitedList> rVisit = new List<VisitedList>();

    }


    [WebMethod(EnableSession = true)]
    public static string GetVisitedDtls(string SF_Code, string FYears, string FMonths)
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
        ListedDR ldr = new ListedDR();
        DataSet dsVisited = new DataSet();
        dsVisited = ldr.Get_Retailer_salVisted_months(div_code, SF_Code, FYears, FMonths);
        TotCounts Tcount = new TotCounts();       


        foreach (DataRow row in dsVisited.Tables[0].Rows)
        {
            VisitedList vl = new VisitedList();
            vl.custCode = row["Trans_Detail_Info_Code"].ToString();
            vl.months = row["Months"].ToString();
            vl.counts = row["CNTS"].ToString();
            Tcount.rVisit.Add(vl);
        }

        foreach (DataRow row in dsVisited.Tables[1].Rows)
        {
            RepeatOrders vl = new RepeatOrders();
            vl.custCode = row["Trans_Detail_Info_Code"].ToString();
            vl.months = row["Months"].ToString();
            vl.counts = row["CNTS"].ToString();
            Tcount.rOrder.Add(vl);
        }

        JSonHelper helper = new JSonHelper();
        String jsonResult = helper.ConvertObjectToJSon(Tcount);
        return jsonResult;
        
    }
    public class JSonHelper
    {
        public string ConvertObjectToJSon<T>(T obj)
        {
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
            MemoryStream ms = new MemoryStream();
            ser.WriteObject(ms, obj);
            string jsonString = Encoding.UTF8.GetString(ms.ToArray());
            ms.Close();
            return jsonString;

        }
        public T ConverJSonToObject<T>(string jsonString)
        {
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T));
            MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonString));
            T obj = (T)serializer.ReadObject(ms);
            return obj;
        }
    }



}