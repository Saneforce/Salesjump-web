using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using Bus_EReport;
using System.Web.UI.HtmlControls;
using System.Drawing;
using System.Web.Services;
using Newtonsoft.Json;

public partial class MasterFiles_Reports_New_Rpt_Secondary_Order : System.Web.UI.Page
{
    public static string divcode = string.Empty;
    public static string Sf_Code = string.Empty;
    public static string sfname = string.Empty;
    string subdiv_code = string.Empty;
    string statecode = "0";
    public static string FDate = string.Empty;
    public static string TDate = string.Empty;
    DataSet ff = new DataSet();
    DataSet ss = new DataSet();
    DataSet dsd = new DataSet();
    DataSet dsGV = new DataSet();
    int gridcnt;
    protected void Page_Load(object sender, EventArgs e)
    {
        divcode = Request.QueryString["div_code"].ToString();
        Sf_Code = Request.QueryString["SF_code"].ToString();
        sfname = Request.QueryString["SF_Name"].ToString();
        subdiv_code = Request.QueryString["subdiv"].ToString();
        statecode = Request.QueryString["state"].ToString();
        FDate = Request.QueryString["FromDate"].ToString();
        TDate = Request.QueryString["ToDate"].ToString();

        DateTime result4 = Convert.ToDateTime(FDate);
        DateTime result10 = Convert.ToDateTime(TDate);
        lblhead1.Text = "Secondary Order Summary for " + sfname + " from " + result4.ToString("dd-MM-yyyy") + " to " + result10.ToString("dd-MM-yyyy");
        FillSF();
    }
    private void FillSF()
    {
        DataSet dsGc = new DataSet();
        Order od = new Order();
        dsGV = od.GetSecondaryOrders(Sf_Code, divcode, FDate, TDate, subdiv_code,statecode);
        grdsec.DataSource = dsGV;
        grdsec.DataBind();
    }
    protected void OnRowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Visible = false;
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            GridView HeaderGrid = (GridView)sender;
            GridViewRow HeaderGridRow0 = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);

            TableRow tableRow = new TableRow();

            DataTable dt = (e.Row.DataItem as DataRowView).DataView.Table;
            string RSf_Code = Convert.ToString(dt.Rows[e.Row.RowIndex]["SF_Code"]);
            string RSf_Name = Convert.ToString(dt.Rows[e.Row.RowIndex]["Employee_Name"]);
            e.Row.Cells[0].Visible = false;
            e.Row.Cells[6].Attributes["sf"] = RSf_Code;
            e.Row.Cells[6].Attributes["sfn"] = RSf_Name;
            e.Row.Cells[6].Attributes["FDate"] = FDate.Split('-')[1].ToString();
            e.Row.Cells[6].Attributes["TDate"] = TDate.Split('-')[1].ToString();
            e.Row.Cells[6].Attributes["FYear"] = FDate.Split('-')[0].ToString();
            e.Row.Cells[6].Attributes["TYear"] = TDate.Split('-')[0].ToString();
            e.Row.Cells[6].Attributes["subdiv"] = subdiv_code;
            e.Row.Cells[6].CssClass = "retcount";
        }
    }
    protected void OnDataBound(object sender, EventArgs e)
    {

        GridViewRow rw = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);

        TableCell TotalCell = new TableCell();

        TotalCell.Width = 55;
        TotalCell.Height = 35;
        TotalCell.BackColor = System.Drawing.Color.FromName("#496a9a");
        TotalCell.ForeColor = System.Drawing.Color.FromName("#fff");
        TotalCell.ColumnSpan = 4;
        TotalCell.Text = "Total";
        rw.Cells.Add(TotalCell);
        decimal gtotal = 0;
        int[] arr = new int[dsGV.Tables[0].Columns.Count - 6];
        for (int i = 0; i < dsGV.Tables[0].Rows.Count; i++)
        {
            gtotal += Convert.ToDecimal(dsGV.Tables[0].Rows[i].ItemArray.GetValue(dsGV.Tables[0].Columns.Count - 1).ToString());
            int n = 0;
            gridcnt = dsGV.Tables[0].Rows[i].ItemArray.Length - 6;
            for (int j = 5; j < (dsGV.Tables[0].Columns.Count - 1); j++)
            {
                arr[n] += Convert.ToInt32(dsGV.Tables[0].Rows[i][j].ToString());
                n++;
            }
        }
        for (int k = 0; k < arr.Length; k++)
        {
            TableCell HeaderCell = new TableCell();

            HeaderCell.Width = 55;
            HeaderCell.Height = 35;
            HeaderCell.BackColor = System.Drawing.Color.FromName("#496a9a");
            HeaderCell.ForeColor = System.Drawing.Color.FromName("#fff");
            HeaderCell.Text = arr[k].ToString();
            rw.Cells.Add(HeaderCell);
        }
        TableCell HeaderCell1 = new TableCell();

        HeaderCell1.Width = 55;
        HeaderCell1.Height = 35;
        HeaderCell1.BackColor = System.Drawing.Color.FromName("#496a9a");
        HeaderCell1.ForeColor = System.Drawing.Color.FromName("#fff");
        HeaderCell1.Text = gtotal.ToString();
        rw.Cells.Add(HeaderCell1);

        grdsec.Controls[0].Controls.Add(rw);
    }
}