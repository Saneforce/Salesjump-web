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
using System.Security.Permissions;
using DBase_EReport;

public partial class MIS_Reports_rpt_Retailer_Closing_Summary : System.Web.UI.Page
{
    string divcode = string.Empty;
    string subdiv = string.Empty;
    string sfname = string.Empty;
    public string Sf_Code = string.Empty;
    string sf_type = string.Empty;
    public string fmonth = string.Empty;
    public string fyear = string.Empty;
    public string remarksid = string.Empty;
    string subdivcode = string.Empty;
    string mname = string.Empty;
    string yname = string.Empty;
    DataSet ss = new DataSet();
    DataSet ff = new DataSet();
    DataSet dsSalesForce = new DataSet();
    DataSet dsGV = new DataSet();
    Int64[] Rcnt;
    Int64 Pcnt = 0;
    Int64 avail = 0;
    private string strQry;

    protected void Page_Load(object sender, EventArgs e)
    {
        divcode = Session["div_code"].ToString();
        subdiv = Request.QueryString["subdiv"].ToString();
        Sf_Code = Request.QueryString["sf_code"].ToString();
        sfname = Request.QueryString["Sf_Name"].ToString();
        fmonth = Request.QueryString["Fmonth"].ToString();
        fyear = Request.QueryString["Fyear"].ToString();
        mname = Request.QueryString["Mname"].ToString();
        yname = Request.QueryString["Yname"].ToString();
        Feild.Text = sfname;
        lblHead.Text = "Retailer Closing Summary for " + mname + " " + yname;
        FillSF();
    }


    public DataSet GetProduct_Name(string divcode, string subdiv)
    {
        DB_EReporting db_ER = new DB_EReporting();

        DataSet dsSF = null;
        if (subdiv != "")
        {
            strQry = "select Product_Detail_Code,Product_Detail_Name from Mas_Product_Detail where Division_Code='" + divcode + "' and CHARINDEX(','+cast(" + subdiv + " as varchar)+',',','+subdivision_code+',')>0  and Product_Active_Flag=0 order by Product_Detail_Code";
        }
        else
        {
            strQry = "select Product_Detail_Code,Product_Detail_Name from Mas_Product_Detail where Division_Code='" + divcode + "' and Product_Active_Flag=0 order by Product_Detail_Code";// and Product_Active_Flag=0
        }

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
            

    private void FillSF()
    {
        string sURL = string.Empty;
        string stURL = string.Empty;
        DataSet dsGc = new DataSet();
        DCR dc = new DCR();

        string stCrtDtaPnt = string.Empty;

        //dsGV = dc.getRemarks(divcode);
        //Rcnt = new Int64[dsGV.Tables[0].Rows.Count];
        ss = new DataSet();
        ss = dc.getRetClosingQty(divcode, Sf_Code, subdiv, fmonth, fyear);


        SalesForce SF = new SalesForce();
        dsGV = GetProduct_Name(divcode, subdiv);

        ff = dc.getRetailerClosing(divcode, Sf_Code, subdiv, fmonth, fyear);

        if (ff.Tables[0].Rows.Count > 0)
        {
            // dsGV.Tables[0].Columns.RemoveAt(1);
            gdclosing.DataSource = ff;
            gdclosing.DataBind();
        }
        else
        {
            gdclosing.DataSource = null;
            gdclosing.DataBind();
        }

    }
    protected void OnRowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridView HeaderGrid = (GridView)sender;
            GridViewRow HeaderGridRow0 = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);

            GridViewRow HeaderGridRow1 = new GridViewRow(0, 1, DataControlRowType.Header, DataControlRowState.Insert);
            HeaderGridRow1.ForeColor = System.Drawing.Color.Black;
            TableCell HeaderCell = new TableCell();
            //HeaderCell.Width = 25;
            //HeaderCell.Height = 35;
            HeaderCell.Font.Bold = true;
            //HeaderCell.ApplyStyle.
            //HeaderCell.BackColor = System.Drawing.Color.FromName("#496a9a");
            HeaderCell.ForeColor = System.Drawing.Color.FromName("#fff");

            HeaderCell = new TableCell();
            HeaderCell.BackColor = System.Drawing.Color.FromName("#496a9a");
            HeaderCell.ForeColor = System.Drawing.Color.FromName("#fff");
            HeaderCell.Text = "SF Name";
            HeaderGridRow0.Cells.Add(HeaderCell);
            
			HeaderCell = new TableCell();
            HeaderCell.BackColor = System.Drawing.Color.FromName("#496a9a");
            HeaderCell.ForeColor = System.Drawing.Color.FromName("#fff");
            HeaderCell.Text = "Retailer";
			
            HeaderGridRow0.Cells.Add(HeaderCell);
            HeaderCell = new TableCell();
            HeaderCell.BackColor = System.Drawing.Color.FromName("#496a9a");
            HeaderCell.ForeColor = System.Drawing.Color.FromName("#fff");
            HeaderCell.Text = "Retailer Code";
			
            HeaderGridRow0.Cells.Add(HeaderCell);
            HeaderCell = new TableCell();
            HeaderCell.BackColor = System.Drawing.Color.FromName("#496a9a");
            HeaderCell.ForeColor = System.Drawing.Color.FromName("#fff");
            HeaderCell.Text = "Route Name";
            HeaderGridRow0.Cells.Add(HeaderCell);
					
            HeaderCell = new TableCell();
            HeaderCell.BackColor = System.Drawing.Color.FromName("#496a9a");
            HeaderCell.ForeColor = System.Drawing.Color.FromName("#fff");
            HeaderCell.Text = "Order Date";
            HeaderGridRow0.Cells.Add(HeaderCell);

            foreach (DataRow drdoctor in dsGV.Tables[0].Rows)
            {
                HeaderCell = new TableCell();
                HeaderCell.Height = 35;
                HeaderCell.BackColor = System.Drawing.Color.FromName("#496a9a");
                HeaderCell.ForeColor = System.Drawing.Color.FromName("#fff");
                //string remarkid = Convert.ToString(drdoctor["Remarks_Id"]);

                HeaderCell.Attributes["style"] = "font: Andalus";
                HeaderCell.Attributes["style"] = "font: Bold";
                HeaderCell.Text = drdoctor["Product_Detail_Name"].ToString();
                HeaderGridRow0.Cells.Add(HeaderCell);
            }
            gdclosing.Controls[0].Controls.AddAt(0, HeaderGridRow0);
            //GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
        }

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            GridView HeaderGrid = (GridView)sender;
            GridViewRow HeaderGridRow0 = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);

            TableRow tableRow = new TableRow();

            DataTable dt = (e.Row.DataItem as DataRowView).DataView.Table;
            string Trans_Sl_No = Convert.ToString(dt.Rows[e.Row.RowIndex]["Trans_sl_no"]);
            //string order_date = Convert.ToString(dt.Rows[e.Row.RowIndex]["Order_Date"]);
			string order_date = Convert.ToString(dt.Rows[e.Row.RowIndex]["Order Date"]);
            e.Row.Cells[5].Visible = false;
            int jk = 0;

            foreach (DataRow item in dsGV.Tables[0].Rows)
            {
                try
                {
                    DataRow[] drp = ss.Tables[0].Select("Trans_sl_no='" + Trans_Sl_No + "' and Product_Name='" + item["Product_Detail_Name"] + "' and Product_Code='" + item["Product_Detail_Code"] + "'");
                    if (drp.Length > 0)
                    {

                        TableCell tableCell = new TableCell();
                        tableCell.Attributes["style"] = "font: Andalus";
                        tableCell.Attributes["style"] = "font: Bold";
                        tableCell.Attributes["values"] = drp[0]["Order_Date"].ToString();
                        tableCell.Attributes["code"] = item["Product_Detail_Code"].ToString();
                        tableCell.Attributes["name"] = item["Product_Detail_Name"].ToString();
                        tableCell.Attributes["sfcode"] = Sf_Code;
                        tableCell.CssClass = "remark";
                        tableCell.Text = drp[0]["Closing"].ToString();// +" ( " + drdoctor["Product_Detail_Code"] + ")";
                        //Rcnt[jk] += Convert.ToInt64(drp[0]["CNT"]);
                        //Pcnt += Convert.ToInt64(drp[0]["CNT"]);
                        e.Row.Cells.Add(tableCell);


                    }
                    else
                    {

                        TableCell tableCell = new TableCell();
                        tableCell.Attributes["style"] = "font: Andalus";
                        tableCell.Attributes["style"] = "font: Bold";
                        tableCell.Text = "";
                        e.Row.Cells.Add(tableCell);

                    }
                }
                catch { }
                jk++;
            }
            //try
            //{

            //    TableCell tableCellBal = new TableCell();
            //    tableCellBal.Attributes["style"] = "font: Andalus";
            //    tableCellBal.Attributes["style"] = "font: Bold";
            //    tableCellBal.Text = Pcnt.ToString();
            //    e.Row.Cells.Add(tableCellBal);

            //}
            //catch
            //{
            //}
            //Pcnt = 0;


            gdclosing.Controls[0].Controls.AddAt(0, HeaderGridRow0);

            //GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
        }

    }
    protected void gridView_PreRender(object sender, EventArgs e)
    {
        //  GridDecorator.MergeRows(gdclosing);
    }
   


    public class GridDecorator
    {
        public static void MergeRows(GridView gridView)
        {
            for (int rowIndex = 0; rowIndex >= 0; rowIndex--)
            {
                GridViewRow row = gridView.Rows[rowIndex];
                GridViewRow previousRow = gridView.Rows[rowIndex + 1];

                for (int i = row.Cells.Count - 1; i < row.Cells.Count; i++)
                {
                    if (row.Cells[i].Text == previousRow.Cells[i].Text)
                    {
                        row.Cells[i].RowSpan = previousRow.Cells[i].RowSpan < 2 ? 2 :
                                               previousRow.Cells[i].RowSpan + 1;
                        previousRow.Cells[i].Visible = false;
                    }
                }
            }
        }
    }

    protected void OnDataBound(object sender, EventArgs e)
    {
        //GridViewRow rw = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);

        //TableCell TotalCell = new TableCell();

        //TotalCell.Width = 55;
        //TotalCell.Height = 35;
        //TotalCell.BackColor = System.Drawing.Color.FromName("#496a9a");
        //TotalCell.ForeColor = System.Drawing.Color.FromName("#fff");
        //TotalCell.Text = "Reasonwise Total";
        //rw.Cells.Add(TotalCell);

        //for (int i = 0; i < Rcnt.Length; i++)
        //{
        //    TableCell HeaderCell = new TableCell();

        //    HeaderCell.Width = 55;
        //    HeaderCell.Height = 35;
        //    HeaderCell.BackColor = System.Drawing.Color.FromName("#496a9a");
        //    HeaderCell.ForeColor = System.Drawing.Color.FromName("#fff");
        //    HeaderCell.Text = Rcnt[i].ToString();
        //    rw.Cells.Add(HeaderCell);
        //}
        //TableCell totcell = new TableCell();
        //totcell.Width = 55;
        //totcell.Height = 35;
        //totcell.BackColor = System.Drawing.Color.FromName("#496a9a");
        //totcell.ForeColor = System.Drawing.Color.FromName("#fff");
        //totcell.Text = "";
        //rw.Cells.Add(totcell);


        //gdclosing.Controls[0].Controls.Add(rw);
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


    //protected void btnExcel_Click(object sender, EventArgs e)
    //{
    //    string attachment = "attachment; filename=ProductwiseReasonAnalysis" +date+ "to" +todate+".xls";
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
    //}

    //protected void btnExcel_Click(object sender, EventArgs e)
    //{
    //    //string attachment = "attachment; filename=Retailer Closing Summary.xls";
    //    //Response.ClearContent();
    //    //Response.AddHeader("content-disposition", attachment);
    //    //Response.ContentType = "File/Data.xls";
    //    //StringWriter sw = new StringWriter();
    //    //HtmlTextWriter htw = new HtmlTextWriter(sw);
    //    //HtmlForm frm = new HtmlForm();
    //    //form1.Parent.Controls.Add(frm);
    //    //frm.Attributes["runat"] = "server";
    //    //frm.Controls.Add(gdclosing);
    //    //frm.RenderControl(htw);
    //    //Response.Write(sw.ToString());
    //    //Response.End();
    //    // Get MyGridView control's HTML representation.
    //    var plainWriter = new StringWriter();
    //    var htmlWriter = new HtmlTextWriter(plainWriter);
    //    gdclosing.RenderControl(htmlWriter);

    //    // Load HTML into ExcelFile.
    //    SpreadsheetInfo.SetLicense("FREE-LIMITED-KEY");
    //    var htmlOptions = new HtmlLoadOptions();
    //    var htmlStream = new MemoryStream(htmlOptions.Encoding.GetBytes(plainWriter.ToString()));
    //    var excel = ExcelFile.Load(htmlStream, htmlOptions);

    //    // Download ExcelFile to current HttpResponse.
    //    excel.Save(this.Response, "Enrollment_Major_Classification.xlsx");
    //}

    protected void btnClose_Click(object sender, EventArgs e)
    {

    }


}