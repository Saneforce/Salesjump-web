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
using System.Xml;
using System.Xml.XPath;
using System.Net;
using System.Drawing;
using System.Web.UI.DataVisualization.Charting;
using System.Globalization;
using Newtonsoft.Json;
using System.Web.Services;
using System.Data.SqlClient;



public partial class MasterFiles_Reports_tsr_ExpClaimReport : System.Web.UI.Page
{
    #region "Declaration"
    DataTable tbl1 = null;
    DataSet dsSalesForce = null;
    DataSet dcrcou = null;
    DataSet dsDCR = null;
    DataSet dsDrr = null;
    DataSet dsTerritory = null;
    DataSet dsTerritory1 = null;
    DataSet dsdoc = null;
    DataSet dsdoc1 = null;
    DataSet dssf = null;
    decimal detorderval = 0;
    decimal detnetval = 0;
    string div_code = string.Empty;
    string strDelay = string.Empty;
    string sf_code = string.Empty;
    string Sf_Name = string.Empty;
    string strMode = string.Empty;
    string sURL = string.Empty;
    string Sf_HQ = string.Empty;
    string Fdate = string.Empty;
    string Tdate = string.Empty;
    string stURL = string.Empty;
    int cmonth = -1;
    int cyear = -1;
    int tot_days = -1;
    int cday = 1;
    int iCount = -1;
    int iFieldWrkCount = -1;
    string sDCR = string.Empty;
    Decimal iTotLstCount = 0;
    Decimal iTotLstCountt;
    Decimal Tot_Sec = 0m;
    string dt = string.Empty;
    string dt1 = string.Empty;
    string dtt = string.Empty;
    string dtt1 = string.Empty;
    string sMonth = string.Empty;
    public static string distcode = string.Empty;
    public static string distnm = string.Empty;
    public static string stcode = string.Empty;
    public static string stnm = string.Empty;
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        //div_code = Session["div_code"].ToString();
        div_code = Request.QueryString["div_code"].ToString();
        sf_code = Request.QueryString["sf_code"].ToString();
        Sf_Name = Request.QueryString["Sf_Name"].ToString();
        cmonth = 0;
        cyear = 0;
        distcode = Request.QueryString["Dst_code"].ToString();
        distnm = Request.QueryString["Dst_name"].ToString();
        stnm = Request.QueryString["st_name"].ToString();
        stcode = Request.QueryString["st_code"].ToString();
        Fdate = Request.QueryString["FDate"].ToString();
        Tdate = Request.QueryString["TDate"].ToString();
        strMode = Request.QueryString["Mode"].ToString();
       
        strMode = strMode.Trim();

        //Label2.Text = "Expense Claim Details From : " + Fdate + " To " + Tdate;

        Label1.Text = " <span style='color:blue'>Team Name :</span>" + Request.QueryString["Sf_Name"].ToString();

        lblTitle.Text = "Expense Claim For The Day Of <span style='color:Red'>" + "( " + Fdate + " ) - ( " + Tdate + ")" + "</span>";

        //if (!this.IsPostBack)
        //{
        //    DataSet dsGV = new DataSet();
        //    Expense dc = new Expense();
        //    dsGV = dc.Get_ExpenseClaim(div_code,sf_code,Fdate, Tdate, stcode);

        //    DataTable dt = new DataTable();

        //    dt = dsGV.Tables[0];

        //    if (dt.Rows.Count > 0)
        //    {
        //        GridView1.DataSource = dt;
        //        GridView1.DataBind();
        //    }
        //    else
        //    {
        //        GridView1.DataSource = null;
        //        GridView1.DataBind();
        //    }

        //}

        DataSet dsGV = new DataSet();
        Expense dc = new Expense();

        TemplateField temp1 = new TemplateField();  

        temp1.ItemTemplate = new DynamicTemplateField(); 
                
        dsGV = dc.Get_ExpenseClaim(div_code, sf_code, Fdate, Tdate, stcode);

        if (dsGV.Tables[0].Rows.Count > 0)
        {
            //dsGV.Tables[0].Columns.RemoveAt(2);
            //dsGV.Tables[0].Columns.RemoveAt(2);
            //for (int i = 0; i < dsGV.Tables[0].Rows.Count; i++)
            //{
            //    for (int j = 0; j < dsGV.Tables[0].Columns.Count; j++)
            //    {
            //        if (string.IsNullOrEmpty(dsGV.Tables[0].Rows[i][j].ToString()))
            //        {
            //            dsGV.Tables[0].Rows[i][j] = "0";
            //        }
            //        else
            //        {
            //            if (j > 8 && j < dsGV.Tables[0].Columns.Count - 1)
            //            {
            //                decimal tot = 0;
            //                tot = Convert.ToDecimal(Math.Round(Convert.ToDecimal(dsGV.Tables[0].Rows[i][j]), 7));

            //                dsGV.Tables[0].Rows[i][j] = Convert.ToDecimal(Math.Round(Convert.ToDecimal(dsGV.Tables[0].Rows[i][j]), 7));
            //                //dsGV.Tables[0].Rows[i][j] = Math.Round(Convert.ToDecimal(dsGV.Tables[0].Rows[i][j]), 7).ToString();
            //            }
            //        }
            //    }
            //}

            if (dsGV.Tables[0].Rows.Count > 0)
            {

                //int kuy = dsGV.Tables[0].Rows.Count;
                GridView1.Columns.Add(temp1);
                GridView1.DataSource = dsGV;
                GridView1.DataBind();
                //GridView1.FooterRow.Cells[8].Text = "TOTAL";
                //GridView1.FooterRow.Cells[8].Font.Bold = true;
                //GridView1.FooterRow.Cells[8].HorizontalAlign = HorizontalAlign.Center;

                //for (int k = 8; k < dsGV.Tables[0].Columns.Count - 1; k++)
                //{

                //    string total = "0";

                //    try
                //    {

                //        total = dsGV.Tables[0].AsEnumerable().Sum(x => Convert.ToDouble(x.Field<decimal>(dsGV.Tables[0].Columns[k].ToString()) == 0 ? 0 : Convert.ToDouble(x.Field<decimal>(dsGV.Tables[0].Columns[k].ToString())))).ToString();
                //    }
                //    catch
                //    {
                       
                //    }
                   
                //    GridView1.FooterRow.Cells[k + 1].Font.Bold = true;
                //    GridView1.FooterRow.Cells[k + 1].HorizontalAlign = HorizontalAlign.Left;
                //    GridView1.FooterRow.Cells[k + 1].Text = total.ToString();
                //    GridView1.FooterRow.Cells[k + 1].Font.Bold = true;
                //    GridView1.FooterRow.BackColor = System.Drawing.Color.Beige;

                //}
            }
            else
            {
                GridView1.DataSource = null;
                GridView1.DataBind();
            }
        }


    }

    [WebMethod]
    public static string GetSFDetails(string div_code, string sf_code, string Fdate, string Tdate, string stcode = "0")
    {
     
        Expense SFDet = new Expense();
        DataSet dsGV = SFDet.Get_ExpenseClaim(div_code, sf_code, Fdate, Tdate, stcode);

        
        return JsonConvert.SerializeObject(dsGV.Tables[0]);
    }

    [WebMethod]
    public static string GetUserExpense(string div_code, string sf_code, string Fdate, string Tdate, string stcode = "0")
    {
        Expense Exp = new Expense();
        DataSet ds = Exp.GetTsrUserExpense(div_code, sf_code, Fdate, Tdate, stcode);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }

    

    public class DynamicTemplateField : ITemplate
    {

        public void InstantiateIn(Control container)
        {
            string divcode = "156";

            Expense SF = new Expense();
            DataSet ff = new DataSet();
            ff = SF.getExpenseDetailsname(divcode);
            int cnt = ff.Tables[0].Rows.Count;           
        }
    }
    protected void GridView1_DataBound(object sender, EventArgs e)
    {

    }

    protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
    {
        Expense SF = new Expense();
        DataSet ff = new DataSet();
        ff = SF.getExpenseDetailsname(div_code);

        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridViewRow row = new GridViewRow(0, -1, DataControlRowType.Header, DataControlRowState.Insert);

            TableCell th = new TableHeaderCell();
            th.Width = 25;
            th.Height = 35;
            th.Font.Bold = true;
            //HeaderCell.ApplyStyle.
            th.BackColor = System.Drawing.Color.FromName("#33CCCC");

            TableCell Distributor = new TableCell();
            th.Width = 25;
            th.Height = 35;
            th.BackColor = System.Drawing.Color.FromName("#33CCCC");
            th.Text = "S.No";

            row.Cells.Add(th);

            th = new TableCell();
            th.Width = 500;
            th.Height = 40;
            th.Attributes["style"] = "font: Courier";
            th.HorizontalAlign = HorizontalAlign.Center;
            th.BackColor = System.Drawing.Color.FromName("#33CCCC");
            th.Text = "State";
            //HeaderCell. = "Andalus";
            row.Cells.Add(th);

            th = new TableCell();
            th.Width = 500;
            th.Height = 40;
            th.Attributes["style"] = "font: Courier";
            th.HorizontalAlign = HorizontalAlign.Center;
            th.BackColor = System.Drawing.Color.FromName("#33CCCC");
            th.Text = "Zone";
            //HeaderCell. = "Andalus";
            row.Cells.Add(th);

            th = new TableCell();
            th.Width = 250;
            th.Height = 35;
            th.Attributes["style"] = "font: Courier";
            th.HorizontalAlign = HorizontalAlign.Center;
            th.BackColor = System.Drawing.Color.FromName("#33CCCC");
            th.Text = "Area";
            //HeaderCell. = "Andalus";
            row.Cells.Add(th);

            th = new TableCell();
            th.Width = 250;
            th.Height = 35;
            th.Attributes["style"] = "font: Courier";
            th.HorizontalAlign = HorizontalAlign.Center;
            th.BackColor = System.Drawing.Color.FromName("#33CCCC");
            th.Text = "User";
            //HeaderCell. = "Andalus";
            row.Cells.Add(th);

            th = new TableCell();
            th.Width = 250;
            th.Height = 35;
            th.Attributes["style"] = "font: Courier";
            th.HorizontalAlign = HorizontalAlign.Center;
            th.BackColor = System.Drawing.Color.FromName("#33CCCC");
            th.Text = "HQ";
            //HeaderCell. = "Andalus";
            row.Cells.Add(th);

            th = new TableCell();
            th.Width = 250;
            th.Height = 35;
            th.Attributes["style"] = "font: Courier";
            th.HorizontalAlign = HorizontalAlign.Center;
            th.BackColor = System.Drawing.Color.FromName("#33CCCC");
            th.Text = "Designation";
            //HeaderCell. = "Andalus";
            row.Cells.Add(th);

            th = new TableCell();
            th.Width = 250;
            th.Height = 35;
            th.Attributes["style"] = "font: Courier";
            th.HorizontalAlign = HorizontalAlign.Center;
            th.BackColor = System.Drawing.Color.FromName("#33CCCC");
            th.Text = "Claim Date";
            //HeaderCell. = "Andalus";
            row.Cells.Add(th);

            //th = new TableCell();
            //th.Width = 250;
            //th.Height = 35;
            //th.Attributes["style"] = "font: Courier";
            //th.HorizontalAlign = HorizontalAlign.Center;
            //th.BackColor = System.Drawing.Color.FromName("#33CCCC");
            //th.Text = "Daily Total";
            ////HeaderCell. = "Andalus";
            //row.Cells.Add(th);

            //foreach (DataRow drdoctor in ff.Tables[0].Rows)
            //{
            //    th = new TableCell();
            //    th.Height = 35;
            //    th.Width = 80;
            //    th.BackColor = System.Drawing.Color.FromName("#33CCCC");

            //    th.Attributes["style"] = "font: Andalus";
            //    th.Attributes["style"] = "font: Bold";
            //    th.Text = drdoctor["expName"].ToString();
            //    row.Cells.Add(th);
            //}

            //th = new TableCell();
            //th.Width = 250;
            //th.Height = 35;
            //th.Attributes["style"] = "font: Courier";
            //th.HorizontalAlign = HorizontalAlign.Center;
            //th.BackColor = System.Drawing.Color.FromName("#33CCCC");
            //th.Text = "Image";
            ////HeaderCell. = "Andalus";
            //row.Cells.Add(th);

            row.HorizontalAlign = HorizontalAlign.Center;
            GridView1.Controls[0].Controls.AddAt(0, row);

        }
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        GridView grid = (GridView)sender;

        if (e.Row.RowType.Equals(DataControlRowType.DataRow))
        {

            e.Row.Cells[0].Text = "" + ((((GridView)sender).PageIndex * ((GridView)sender).PageSize) + (e.Row.RowIndex + 1));


            int numColunas = e.Row.Cells.Count;

        }

        if (e.Row.RowIndex != -1)
        {
            
            string id = e.Row.Cells[0].Text;
         
            string id2 = e.Row.Cells[1].Text;
           
        }
    }

    //protected void btnPrint_Click(object sender, EventArgs e)
    //{
    //    Session["ctrl"] = pnlContents;
    //    Control ctrl = (Control)Session["ctrl"];
    //    PrintWebControl(ctrl);

    //}

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
        //HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.Write(strHTML);
        HttpContext.Current.Response.Write("<script>window.print();</script>");
        HttpContext.Current.Response.End();

    }
    protected void btnExcel_Click(object sender, EventArgs e)
    {
        string attachment = "attachment; filename=ExpenseClaimReport_" + DateTime.Now.ToString("dd/MM/yyyy") + ".xls";
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

    protected void btnPDF_Click(object sender, EventArgs e)
    {
        string strFileName = "ExpenseClaim";
        Response.ContentType = "application/pdf";
        Response.AddHeader("content-disposition", "attachment;filename=" + strFileName + ".pdf");
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        StringWriter sw = new StringWriter();
        HtmlTextWriter hw = new HtmlTextWriter(sw);
        HtmlForm frm = new HtmlForm();
        pnlContents.Parent.Controls.Add(frm);
        frm.Attributes["runat"] = "server";
        frm.Controls.Add(pnlContents);
        frm.RenderControl(hw);
        StringReader sr = new StringReader(sw.ToString());
        Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 4f);
        iTextSharp.text.html.simpleparser.HTMLWorker htmlparser = new iTextSharp.text.html.simpleparser.HTMLWorker(pdfDoc);
        PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
        pdfDoc.Open();
        htmlparser.Parse(sr);
        pdfDoc.Close();
        Response.Write(pdfDoc);
        Response.End();
    }

    
}