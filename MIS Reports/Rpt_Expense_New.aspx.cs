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


public partial class MIS_Reports_Rpt_Expense_New : System.Web.UI.Page
{
    string divcode = string.Empty;
    string Sf_Code = string.Empty;
    string sfname = string.Empty;
    string subdiv_code = string.Empty;
    string exptype = string.Empty;
    string Yr = string.Empty;
    string Mnth = string.Empty;
    DataSet ff = new DataSet();
    DataSet ss = new DataSet();
    int gridcnt = 0;
    string mnthname = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        divcode = Session["div_code"].ToString();
        Sf_Code = Request.QueryString["SF_code"].ToString();
        sfname = Request.QueryString["SF_Name"].ToString();
        exptype = Request.QueryString["Type"].ToString();
        Feild.Visible = false;
        Mnth = Request.QueryString["Mnth"].ToString();
        Yr = Request.QueryString["Yr"].ToString();
        mnthname = Request.QueryString["MnthName"].ToString();
        lblHead.Text = "Expense Report for " + mnthname + "-" + Yr;
        FillSF();
    }


    private void FillSF()
    {
        DataSet dsGV = new DataSet();
        Expense SF = new Expense();
        ff = new DataSet();
        ss = new DataSet();
        dsGV = SF.getExpenseDets(Sf_Code, divcode, Mnth, Yr, exptype);
        ff = SF.getExpApproveDets(Sf_Code, divcode, Mnth, Yr);
        if (dsGV.Tables[0].Rows.Count > 0)
        {
            if (divcode == "100") { dsGV.Tables[0].Columns.RemoveAt(10); } else { dsGV.Tables[0].Columns.RemoveAt(9); dsGV.Tables[0].Columns.RemoveAt(1); };
            gridcnt = dsGV.Tables[0].Columns.Count;
            gdprimary.DataSource = dsGV;
            gdprimary.DataBind();
        }
        else
        {
            gdprimary.DataSource = null;
            gdprimary.DataBind();
        }

    }

    protected void OnRowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataTable dt = (e.Row.DataItem as DataRowView).DataView.Table;
        }



        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridView HeaderGrid = (GridView)sender;
            GridViewRow HeaderGridRow0 = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);

            GridViewRow HeaderGridRow1 = new GridViewRow(0, 1, DataControlRowType.Header, DataControlRowState.Insert);
            HeaderGridRow1.ForeColor = System.Drawing.Color.Black;
            TableCell HeaderCell = new TableCell();
            HeaderCell.Font.Bold = true;
            HeaderCell.BackColor = System.Drawing.Color.FromName("#496a9a");
            HeaderCell.ForeColor = System.Drawing.Color.FromName("#fff");

            HeaderCell = new TableCell();
            HeaderCell.Width = 310;
            HeaderCell.Height = 35;
            HeaderCell.BackColor = System.Drawing.Color.FromName("#496a9a");
            HeaderCell.ForeColor = System.Drawing.Color.FromName("#fff");
            HeaderCell.Text = "SlNo.";
            HeaderGridRow0.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Width = 310;
            HeaderCell.Height = 35;
            HeaderCell.BackColor = System.Drawing.Color.FromName("#496a9a");
            HeaderCell.ForeColor = System.Drawing.Color.FromName("#fff");
            HeaderCell.Text = "Employee ID";
            HeaderGridRow0.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Width = 310;
            HeaderCell.Height = 35;
            HeaderCell.BackColor = System.Drawing.Color.FromName("#496a9a");
            HeaderCell.ForeColor = System.Drawing.Color.FromName("#fff");
            HeaderCell.Text = "Employee Name";
            HeaderGridRow0.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Width = 310;
            HeaderCell.Height = 35;
            HeaderCell.BackColor = System.Drawing.Color.FromName("#496a9a");
            HeaderCell.ForeColor = System.Drawing.Color.FromName("#fff");
            HeaderCell.Text = "Geography";
            HeaderGridRow0.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Width = 310;
            HeaderCell.Height = 35;
            HeaderCell.BackColor = System.Drawing.Color.FromName("#496a9a");
            HeaderCell.ForeColor = System.Drawing.Color.FromName("#fff");
            HeaderCell.Text = "Reporting To";
            HeaderGridRow0.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Width = 310;
            HeaderCell.Height = 35;
            HeaderCell.BackColor = System.Drawing.Color.FromName("#496a9a");
            HeaderCell.ForeColor = System.Drawing.Color.FromName("#fff");
            HeaderCell.Text = "Last Submitted Date";
            HeaderGridRow0.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Width = 310;
            HeaderCell.Height = 35;
            HeaderCell.BackColor = System.Drawing.Color.FromName("#496a9a");
            HeaderCell.ForeColor = System.Drawing.Color.FromName("#fff");
            HeaderCell.Text = "Applied Amount";
            HeaderGridRow0.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Width = 310;
            HeaderCell.Height = 35;
            HeaderCell.BackColor = System.Drawing.Color.FromName("#496a9a");
            HeaderCell.ForeColor = System.Drawing.Color.FromName("#fff");
            HeaderCell.Text = "Approval Status";
            HeaderGridRow0.Cells.Add(HeaderCell);
            if (divcode == "100")
            {

                HeaderCell = new TableCell();
                HeaderCell.Width = 310;
                HeaderCell.Height = 35;
                HeaderCell.BackColor = System.Drawing.Color.FromName("#496a9a");
                HeaderCell.ForeColor = System.Drawing.Color.FromName("#fff");
                HeaderCell.Text = "Approved Date";
                HeaderGridRow0.Cells.Add(HeaderCell);

                HeaderCell = new TableCell();
                HeaderCell.Width = 310;
                HeaderCell.Height = 35;
                HeaderCell.BackColor = System.Drawing.Color.FromName("#496a9a");
                HeaderCell.ForeColor = System.Drawing.Color.FromName("#fff");
                HeaderCell.Text = "Manager Approved Amount";
                HeaderGridRow0.Cells.Add(HeaderCell);

                HeaderCell = new TableCell();
                HeaderCell.Width = 310;
                HeaderCell.Height = 35;
                HeaderCell.BackColor = System.Drawing.Color.FromName("#496a9a");
                HeaderCell.ForeColor = System.Drawing.Color.FromName("#fff");
                HeaderCell.Text = "NSM Approved Amount";
                HeaderGridRow0.Cells.Add(HeaderCell);

                HeaderCell = new TableCell();
                HeaderCell.Width = 310;
                HeaderCell.Height = 35;
                HeaderCell.BackColor = System.Drawing.Color.FromName("#496a9a");
                HeaderCell.ForeColor = System.Drawing.Color.FromName("#fff");
                HeaderCell.Text = "Admin Approved Amount";
                HeaderGridRow0.Cells.Add(HeaderCell);

            }
            else
            {
                HeaderCell = new TableCell();
                HeaderCell.Width = 310;
                HeaderCell.Height = 35;
                HeaderCell.BackColor = System.Drawing.Color.FromName("#496a9a");
                HeaderCell.ForeColor = System.Drawing.Color.FromName("#fff");
                HeaderCell.Text = "Approved Amount";
                HeaderGridRow0.Cells.Add(HeaderCell);
            }
            gdprimary.Controls[0].Controls.AddAt(0, HeaderGridRow0);
        }

        if (divcode == "100")
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                GridView HeaderGrid = (GridView)sender;
                GridViewRow HeaderGridRow0 = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);
                TableRow tableRow = new TableRow();
                DataTable dt = (e.Row.DataItem as DataRowView).DataView.Table;
                string Trans_Sl_No = Convert.ToString(dt.Rows[e.Row.RowIndex]["Sf_Code"]);
                e.Row.Cells[1].Visible = false;
                List<String> list = new List<String>();
                list.Add("Manager");
                list.Add("NSM");
                list.Add("Admin");
                for (int j = 0; j < list.Count; j++)
                {
                    try
                    {
                        DataRow[] drp = ff.Tables[0].Select("Sf_Code='" + Trans_Sl_No + "' and Approved_By='" + list[j].ToString() + "'");
                        if (drp.Length > 0)
                        {
                            TableCell tableCell = new TableCell();
                            tableCell.Attributes["style"] = "font: Andalus";
                            tableCell.Attributes["style"] = "font: Bold";
                            tableCell.Text = drp[0]["Approved_Amnt"].ToString();
                            e.Row.Cells.AddAt(e.Row.Cells.Count, tableCell);
                        }
                        else
                        {
                            TableCell tableCell = new TableCell();
                            tableCell.Attributes["style"] = "font: Andalus";
                            tableCell.Attributes["style"] = "font: Bold";
                            tableCell.Text = "";
                            e.Row.Cells.AddAt(e.Row.Cells.Count, tableCell);
                        }
                    }
                    catch { }
                }
            }
        }
    }

    protected void OnDataBound(object sender, EventArgs e)
    {
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
        string attachment = "attachment; filename=Expense_Report "+ mnthname + "-" + Yr+".xls";
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

    }
}