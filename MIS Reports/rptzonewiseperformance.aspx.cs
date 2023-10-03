using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
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
using System.Web.UI.HtmlControls;
using System.Drawing;

public partial class MIS_Reports_rptzonewiseperformance : System.Web.UI.Page
{
    string desigCode = string.Empty;
    string designame = string.Empty;
    string subdiv_code = string.Empty;
    string divcode = string.Empty;
    int iTotLstCount1 = 0;
    int iTotLstCount = 0;
    int iTotLstCount2 = 0;
   
   
   
    string TMonth = string.Empty;
    string TYear = string.Empty;
   
    DataSet dsSalesForce = new DataSet();
    DataSet dsmgrsf = new DataSet();
    DataSet dsDoc = null;
    DateTime dtCurrent;
  
    string tot_dr = string.Empty;
   

    string tot_dcr_dr = string.Empty;
  
    string Monthsub = string.Empty;
   
    string sCurrentDate = string.Empty;
  
    int gridcnt = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        divcode = Session["div_code"].ToString();
        desigCode = Request.QueryString["designation_code"].ToString();
        designame = Request.QueryString["designation_name"].ToString();
        TMonth = Request.QueryString["FMonth"].ToString();
        TYear = Request.QueryString["FYear"].ToString();
        HiddenField1.Value = divcode;

        System.Globalization.DateTimeFormatInfo mfi = new System.Globalization.DateTimeFormatInfo();
        string strFMonthName = mfi.GetMonthName(Convert.ToInt16(TMonth)).ToString().Substring(0, 3);

        lblHead.Text = "Zonewise Performance  for the Month of " + strFMonthName + " " + TYear + " ";
        desig.Text = designame;

        FillSF();

    }
    public class DynamicTemplateField : ITemplate
    {

        public void InstantiateIn(Control container)
        {
            string divcode = "32";

            SalesForce SF = new SalesForce();
            DataSet ff = new DataSet();
            ff = SF.GetProduct_Name(divcode);
            int cnt = ff.Tables[0].Rows.Count;
          
        }
    }
    private void FillSF()
    {


        string sURL = string.Empty;
        string stURL = string.Empty;


        string stCrtDtaPnt = string.Empty;

        DataSet dsGV = new DataSet();
        DataSet dsGc = new DataSet();
        DCR dc = new DCR();
        //TemplateField temp1 = new TemplateField();  //Create instance of Template field


        //temp1.ItemTemplate = new DynamicTemplateField(); //Set the properties **ItemTemplate** as the instance of DynamicTemplateField class.


        //gvtotalorder.Columns.Add(temp1);

        dsGV = dc.zonewiseperformance(divcode, TMonth, TYear,desigCode);
        
        if (dsGV.Tables[0].Rows.Count > 0)
        {
            dsGV.Tables[0].Columns.RemoveAt(0);
            dsGV.Tables[0].Columns.RemoveAt(0);
         
            dsGV.Tables[0].Columns.RemoveAt(12);
           

            
            gvtotalorder.DataSource = dsGV;
            gvtotalorder.DataBind();
        }
        else
        {
            gvtotalorder.DataSource = null;
            gvtotalorder.DataBind();
        }
      

    }
    protected void OnRowCreated(object sender, GridViewRowEventArgs e)
    {

      
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridView HeaderGrid = (GridView)sender;
            GridViewRow HeaderGridRow0 = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            GridViewRow HeaderGridRow1 = new GridViewRow(0, 1, DataControlRowType.Header, DataControlRowState.Insert);
            HeaderGridRow0.Font.Size =8;
            //HeaderGridRow0.Font.Name = "Andalus";

            TableCell HeaderCell = new TableCell();
            HeaderCell.Font.Size = 8;
            HeaderCell.Width = 25;
            HeaderCell.Height = 20;HeaderCell.RowSpan = 2;
          
            //HeaderCell.ApplyStyle.
            HeaderCell.BackColor = System.Drawing.Color.FromName("#496a9a");

            TableCell Distributor = new TableCell();
            //HeaderCell = new TableHeaderCell();
            //HeaderCell.Width = 180;
            //HeaderCell.Height = 20;HeaderCell.RowSpan = 2;
            //HeaderCell.Attributes["style"] = "font: Courier";
            //HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            //HeaderCell.BackColor = System.Drawing.Color.FromName("#496a9a");
            //HeaderCell.Text = "S.No";
            //HeaderCell.Width = 85;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell.ForeColor = System.Drawing.Color.White;
            HeaderCell.Height = 20;HeaderCell.RowSpan = 2;
            HeaderCell.Font.Size = 8;
            HeaderCell.BackColor = System.Drawing.Color.FromName("#496a9a");
            HeaderCell.Text = designame;
            HeaderCell.Attributes["style"] = "font: Andalus";
            HeaderGridRow0.Cells.Add(HeaderCell);
            HeaderCell = new TableHeaderCell();
            HeaderCell.Width = 180;
            HeaderCell.Height = 20;HeaderCell.RowSpan = 2;
            HeaderCell.Font.Size = 8;
            HeaderCell.Attributes["style"] = "font: Andalus";
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderCell.BackColor = System.Drawing.Color.FromName("#496a9a");
            HeaderCell.Text = "Total Outlets";
            HeaderCell.Attributes["style"] = "font: Andalus";
            HeaderGridRow0.Cells.Add(HeaderCell);
            HeaderCell = new TableHeaderCell();
            HeaderCell.Width = 110;
            HeaderCell.Height = 20;HeaderCell.RowSpan = 2;
            HeaderCell.Font.Size = 8;
            HeaderCell.BackColor = System.Drawing.Color.FromName("#496a9a");
            HeaderCell.Text = "UTC";
            HeaderCell.Attributes["style"] = "font: Andalus";
            HeaderGridRow0.Cells.Add(HeaderCell);
            HeaderCell = new TableHeaderCell();
            HeaderCell.Width = 110;
            HeaderCell.Height = 20;HeaderCell.RowSpan = 2;
            HeaderCell.Font.Size = 8;
            HeaderCell.BackColor = System.Drawing.Color.FromName("#496a9a");
            HeaderCell.Text = "UPC";
            HeaderCell.Attributes["style"] = "font: Andalus";
            HeaderGridRow0.Cells.Add(HeaderCell);
            HeaderCell = new TableHeaderCell();
            HeaderCell.Width = 110;
            HeaderCell.Height = 20;HeaderCell.RowSpan = 2;
            HeaderCell.Font.Size = 8;
            HeaderCell.BackColor = System.Drawing.Color.FromName("#496a9a");
            HeaderCell.Text = "Zero Billed";
            HeaderGridRow0.Cells.Add(HeaderCell);


           
            HeaderCell = new TableHeaderCell();
            HeaderCell.Width = 110;
            HeaderCell.Height = 20;HeaderCell.RowSpan = 2;
            HeaderCell.Font.Size = 8;
            HeaderCell.BackColor = System.Drawing.Color.FromName("#496a9a");
            HeaderCell.Text = "%Coverage";
            HeaderCell.Attributes["style"] = "font: Andalus";
            HeaderGridRow0.Cells.Add(HeaderCell);
            HeaderCell = new TableHeaderCell();
            HeaderCell.Width = 150;
            HeaderCell.Height = 20;HeaderCell.RowSpan = 2;
            HeaderCell.Font.Size = 8;
            HeaderCell.BackColor = System.Drawing.Color.FromName("#496a9a");
            HeaderCell.Text = "% Billed (wrt UTC)";
            HeaderCell.Attributes["style"] = "font: Andalus";
            HeaderGridRow0.Cells.Add(HeaderCell);
            HeaderCell = new TableHeaderCell();
            HeaderCell.Width = 150;
            HeaderCell.Height = 20;HeaderCell.RowSpan = 2;
            HeaderCell.Font.Size = 8;
            HeaderCell.BackColor = System.Drawing.Color.FromName("#496a9a");
            HeaderCell.Text = "% Billed (wrt Total Outlets)";
            HeaderCell.Attributes["style"] = "font: Andalus";
            HeaderGridRow0.Cells.Add(HeaderCell);
            HeaderCell = new TableHeaderCell();
            HeaderCell.Width = 110;
            HeaderCell.Height = 20;HeaderCell.RowSpan = 2;
            HeaderCell.Font.Size = 8;
            HeaderCell.BackColor = System.Drawing.Color.FromName("#496a9a");
            HeaderCell.Text = "New Outlets";
            HeaderCell.Attributes["style"] = "font: Andalus";
            HeaderGridRow0.Cells.Add(HeaderCell);
            HeaderCell = new TableHeaderCell();
            HeaderCell.Width = 110;
            HeaderCell.Height = 20;HeaderCell.RowSpan = 2;
            HeaderCell.Font.Size = 8;
            HeaderCell.BackColor = System.Drawing.Color.FromName("#496a9a");
            HeaderCell.Text = "New Outlets Value";
            HeaderCell.Attributes["style"] = "font: Andalus";
            HeaderGridRow0.Cells.Add(HeaderCell);
            HeaderCell = new TableHeaderCell();
            HeaderCell.Width = 110;
            HeaderCell.Height = 20;HeaderCell.RowSpan = 2;
            HeaderCell.Font.Size = 8;
            HeaderCell.BackColor = System.Drawing.Color.FromName("#496a9a");
            HeaderCell.Text = "Total Quantity";
            HeaderCell.Attributes["style"] = "font: Andalus";
            HeaderGridRow0.Cells.Add(HeaderCell);
            HeaderCell = new TableHeaderCell();
            HeaderCell.Width = 110;
            HeaderCell.Height = 20;HeaderCell.RowSpan = 2;
            HeaderCell.Font.Size = 8;
            HeaderCell.BackColor = System.Drawing.Color.FromName("#496a9a");
            HeaderCell.Text = "Total Value";
            HeaderCell.Attributes["style"] = "font: Andalus";
            HeaderGridRow0.Cells.Add(HeaderCell);

            SalesForce SF = new SalesForce();
            DataSet ff = new DataSet();
            ff = SF.GetcategoryName_Customer(divcode);
            foreach (DataRow drdoctor in ff.Tables[0].Rows)
            {
                HeaderCell = new TableCell();
                HeaderCell.Height = 20;
                HeaderCell.BackColor = System.Drawing.Color.FromName("#496a9a");
                HeaderCell.ForeColor = System.Drawing.Color.White;
                HeaderCell.ColumnSpan = 2;
                HeaderCell.Font.Size = 8;
                HeaderCell.Font.Bold = true;
               HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                HeaderCell.Attributes["style"] = "font: Andalus";
                HeaderCell.Text = drdoctor["Product_Cat_Name"].ToString();
                HeaderGridRow0.Cells.Add(HeaderCell);
            }
            gvtotalorder.Controls[0].Controls.AddAt(0, HeaderGridRow0);

            for (int i = 0; i < ff.Tables[0].Rows.Count; i++)
            {

                TableCell HeaderCell1 = new TableCell();
                HeaderCell1.Attributes["style"] = "font: Andalus";
                HeaderCell1.Width = 55;
                HeaderCell1.Height = 35;
                HeaderCell1.Font.Bold = true;
                HeaderCell1.Font.Size = 8;




                HeaderCell1.BackColor = System.Drawing.Color.FromName("#496a9a");
                HeaderCell1.Text = "Outletwise Penetration";
                HeaderCell1.ForeColor = System.Drawing.Color.White;
                HeaderGridRow1.Cells.Add(HeaderCell1);
                gvtotalorder.Controls[0].Controls.AddAt(1, HeaderGridRow1);
                HeaderCell1 = new TableHeaderCell();

                HeaderCell1.Width = 80;
                HeaderCell1.Height = 35;
                HeaderCell1.Attributes["style"] = "font: Andalus";
                HeaderCell1.Font.Bold = true;
                HeaderCell1.BackColor = System.Drawing.Color.FromName("#496a9a");
                HeaderCell1.Text = "Value";
                HeaderCell1.Font.Size = 8;
                HeaderCell1.ForeColor = System.Drawing.Color.White;
                HeaderGridRow1.Cells.Add(HeaderCell1);
                gvtotalorder.Controls[0].Controls.AddAt(1, HeaderGridRow1);

            }
            //GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
        }

    }

  
 
    
    
  protected void ExportToExcel(object sender, EventArgs e)
    {
        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=ZonewisePerformance.xls");
        Response.Charset = "";
        Response.ContentType = "application/vnd.ms-excel";
        using (StringWriter sw = new StringWriter())
        {
            HtmlTextWriter hw = new HtmlTextWriter(sw);

            //To Export all pages
            //gvclosingstockanalysis.AllowPaging = false;
            //this.BindGrid();

            gvtotalorder.HeaderRow.BackColor = Color.Blue;

            foreach (GridViewRow row in gvtotalorder.Rows)
            {
                row.BackColor = Color.White;
                foreach (TableCell cell in row.Cells)
                {
                    if (row.RowIndex % 2 == 0)
                    {
                        cell.BackColor = gvtotalorder.AlternatingRowStyle.BackColor;
                    }
                    else
                    {
                        cell.BackColor = gvtotalorder.RowStyle.BackColor;
                    }
                    cell.CssClass = "textmode";
                }
            }

            gvtotalorder.RenderControl(hw);

            //style to format numbers to string
            string style = @"<style> .textmode { } </style>";
            Response.Write(style);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
        }
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
           server control at run time. */
    }
   
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        // Session["ctrl"] = pnlContents;
        //  Control ctrl = (Control)Session["ctrl"];
        //   PrintWebControl(ctrl);
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



    protected void btnClose_Click(object sender, EventArgs e)
    {
    }
}