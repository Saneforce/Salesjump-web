using DBase_EReport;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class MIS_Reports_rpt_monthly_summary : System.Web.UI.Page
{
    public static string divcode = string.Empty;
    public static string Sf_Code = string.Empty;
    public static string sfname = string.Empty;
    public static string subdiv_code = string.Empty;
    public static string month = string.Empty;
    public static string year = string.Empty;
    DataSet ff = new DataSet();
    int gridcnt = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        divcode = Session["div_code"].ToString();
        month = Request.QueryString["Month"].ToString();
        year = Request.QueryString["Year"].ToString();
        subdiv_code = Request.QueryString["subdiv"].ToString();
        Sf_Code = Request.QueryString["sf_code"].ToString();
        sfname = Request.QueryString["Sf_Name"].ToString();
        System.Globalization.DateTimeFormatInfo mfi = new System.Globalization.DateTimeFormatInfo();
        string MonthName = mfi.GetMonthName(Convert.ToInt16(month)).ToString().Substring(0, 3);
        lblHead.Text = "Secondary monthlysales View for " + MonthName + ' '+ year;
        Feild.Text = sfname;
        FillSF();
    }
    private void FillSF()
    {
        loc SF = new loc();
        ff = new DataSet();
        ff = SF.getsales_details(divcode, month, year, Sf_Code, subdiv_code);
        if (ff.Tables[0].Rows.Count > 0)
        {
            gridcnt = ff.Tables[0].Columns.Count;
            gvtotalorder.DataSource = ff;
            gvtotalorder.DataBind();
        }
        else
        {
            gvtotalorder.DataSource = null;
            gvtotalorder.DataBind();
        }
    }
        protected void btnExcel_Click(object sender, EventArgs e)
    {
        string attachment = "attachment; filename=Secondary_Monthly_sales.xls";
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
    //protected void OnRowCreated(object sender, GridViewRowEventArgs e)
    //{
    //    if (e.Row.RowType == DataControlRowType.Header)
    //    {
    //        GridView HeaderGrid = (GridView)sender;
    //        GridViewRow HeaderGridRow0 = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);

    //        GridViewRow HeaderGridRow1 = new GridViewRow(0, 1, DataControlRowType.Header, DataControlRowState.Insert);
    //        HeaderGridRow1.ForeColor = System.Drawing.Color.Black;
    //        TableCell HeaderCell = new TableCell();
    //        HeaderCell.Font.Bold = true;
    //        HeaderCell.BackColor = System.Drawing.Color.FromName("#496a9a");
    //        HeaderCell.ForeColor = System.Drawing.Color.FromName("#fff");

    //        HeaderCell = new TableCell();
    //        HeaderCell.Width = 310;
    //        HeaderCell.Height = 35;
    //        HeaderCell.BackColor = System.Drawing.Color.FromName("#496a9a");
    //        HeaderCell.ForeColor = System.Drawing.Color.FromName("#fff");
    //        HeaderCell.Text = "Order Date";
    //        HeaderGridRow0.Cells.Add(HeaderCell);

    //        HeaderCell = new TableCell();
    //        HeaderCell.BackColor = System.Drawing.Color.FromName("#496a9a");
    //        HeaderCell.ForeColor = System.Drawing.Color.FromName("#fff");
    //        HeaderCell.Text = "Day";
    //        HeaderGridRow0.Cells.Add(HeaderCell);

    //        HeaderCell = new TableCell();
    //        HeaderCell.BackColor = System.Drawing.Color.FromName("#496a9a");
    //        HeaderCell.ForeColor = System.Drawing.Color.FromName("#fff");
    //        HeaderCell.Text = "Distributor Name";
    //        HeaderGridRow0.Cells.Add(HeaderCell);

    //        HeaderCell = new TableCell();
    //        HeaderCell.BackColor = System.Drawing.Color.FromName("#496a9a");
    //        HeaderCell.ForeColor = System.Drawing.Color.FromName("#fff");
    //        HeaderCell.Text = "Area Worked";
    //        HeaderGridRow0.Cells.Add(HeaderCell);

    //        HeaderCell = new TableCell();
    //        HeaderCell.BackColor = System.Drawing.Color.FromName("#496a9a");
    //        HeaderCell.ForeColor = System.Drawing.Color.FromName("#fff");
    //        HeaderCell.Text = "TC";
    //        HeaderGridRow0.Cells.Add(HeaderCell);

    //        HeaderCell = new TableCell();
    //        HeaderCell.BackColor = System.Drawing.Color.FromName("#496a9a");
    //        HeaderCell.ForeColor = System.Drawing.Color.FromName("#fff");
    //        HeaderCell.Text = "PC";
    //        HeaderGridRow0.Cells.Add(HeaderCell);

    //        HeaderCell = new TableCell();
    //        HeaderCell.BackColor = System.Drawing.Color.FromName("#496a9a");
    //        HeaderCell.ForeColor = System.Drawing.Color.FromName("#fff");
    //        HeaderCell.Text = "Sales";
    //        HeaderGridRow0.Cells.Add(HeaderCell);

    //        gvtotalorder.Controls[0].Controls.AddAt(0, HeaderGridRow0);
    //    }
    //}
    public class loc
    {
        public DataSet getsales_details(string divcode, string month, string year, string Sf_Code, string subdiv = "0")
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;

            string strQry = "exec sp_monthly_secsales '" + Sf_Code + "','" + divcode + "','" + subdiv + "','" + month + "'," + year + "";

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