using System;
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
using iTextSharp.tool.xml;
using Bus_EReport;
using System.Net;

public partial class MIS_Reports_rpt_PrimaryVsSecondary : System.Web.UI.Page
{
    string Div_Code = string.Empty;
    string Year = string.Empty;
    string Type = string.Empty;
    string field = string.Empty;
    string sub_div = string.Empty;
    string Field_name = string.Empty;

    DataTable salesforce_primary = new DataTable();
    DataTable salesforce_secondary = new DataTable();
    DataTable distributor_primary = new DataTable();
    DataTable distributor_secondary = new DataTable();
    DataSet Salesforce_binddata = new DataSet();
    DataSet Distributor_binddata = new DataSet();
    protected void Page_Load(object sender, EventArgs e)
    {
       

        Div_Code = Session["div_code"].ToString();

        Year = Request.QueryString["Year"].ToString();
        Type = Request.QueryString["Type"].ToString();
        field = Request.QueryString["Field_force"].ToString();
        sub_div = Request.QueryString["Sub_Div_Code"].ToString();
        Field_name = Request.QueryString["Field_name"].ToString();
        
        lblHead.Text = "Primary Vs Secondary Sales -" + Year;
        Feild.Text = Field_name;
        if (Type == "0")
        {
            SalesForce sf = new SalesForce();
            salesforce_primary = sf.Get_HQ_Primary_Order(Div_Code, field, Year);
            salesforce_secondary = sf.Get_HQ_Secondary_Order(Div_Code, field, Year);
            Salesforce_binddata = sf.Bind_hq_type(Div_Code, field);
            if (Salesforce_binddata.Tables[0].Rows.Count > 0)
            {
                pri_vs_sec_sales_grid.DataSource = Salesforce_binddata;
                pri_vs_sec_sales_grid.DataBind();
            }
        }
        else
        {
            SalesForce sf = new SalesForce();
            distributor_primary = sf.Get_state_Primary_Order(Div_Code, field, Year);
            distributor_secondary = sf.Get_state_secondary_Order(Div_Code, field, Year);
            Distributor_binddata = sf.Bind_state_type(Div_Code, field);
            if (Distributor_binddata.Tables[0].Rows.Count > 0)
            {
                pri_vs_sec_sales_grid.DataSource = Distributor_binddata;
                pri_vs_sec_sales_grid.DataBind();
            }


        }
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {

    }

    protected void btnExport_Click(object sender, EventArgs e)
    {

    }

    protected void btnClose_Click(object sender, EventArgs e)
    {

    }

    protected void pri_vs_sec_sales_grid_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {

            GridView HeaderGrid = (GridView)sender;
            GridViewRow HeaderGridRow0 = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            HeaderGridRow0.ForeColor = System.Drawing.Color.Black;

            GridViewRow HeaderGridRow1 = new GridViewRow(0, 1, DataControlRowType.Header, DataControlRowState.Insert);
            HeaderGridRow1.ForeColor = System.Drawing.Color.Black;
            GridViewRow HeaderGridRow2 = new GridViewRow(0, 2, DataControlRowType.Header, DataControlRowState.Insert);
            HeaderGridRow2.ForeColor = System.Drawing.Color.Black;
            TableCell HeaderCell = new TableCell();

            if (Type == "0")
            {
                HeaderCell = new TableCell();
                HeaderCell.Width = 110;
                HeaderCell.Height = 35;
                HeaderCell.ForeColor = System.Drawing.Color.FromName("#fff");
                HeaderCell.BackColor = System.Drawing.Color.FromName("#496a9a");
                HeaderCell.Text = "State";
                HeaderCell.RowSpan = 2;
                HeaderCell.Attributes["style"] = "font: Andalus";
                HeaderGridRow0.Cells.Add(HeaderCell);

                HeaderCell = new TableCell();
                HeaderCell.Width = 110;
                HeaderCell.Height = 35;
                HeaderCell.ForeColor = System.Drawing.Color.FromName("#fff");
                HeaderCell.BackColor = System.Drawing.Color.FromName("#496a9a");
                HeaderCell.Style.Add("Color", "White");
                HeaderCell.Text = "Block";
                HeaderCell.RowSpan = 2;
                HeaderCell.Attributes["style"] = "font: Andalus";
                HeaderGridRow0.Cells.Add(HeaderCell);

                HeaderCell = new TableCell();
                HeaderCell.Width = 110;
                HeaderCell.Height = 35;
                HeaderCell.BackColor = System.Drawing.Color.FromName("#496a9a");
                HeaderCell.Style.Add("Color", "White");
                HeaderCell.Text = "H.Q";
                HeaderCell.RowSpan = 2;
                HeaderGridRow0.Cells.Add(HeaderCell);

                HeaderCell = new TableCell();
                HeaderCell.Width = 110;
                HeaderCell.Height = 35;
                HeaderCell.BackColor = System.Drawing.Color.FromName("#496a9a");
                HeaderCell.Style.Add("Color", "White");
                HeaderCell.Text = "Emp Code";
                HeaderCell.RowSpan = 2;
                HeaderGridRow0.Cells.Add(HeaderCell);

                HeaderCell = new TableCell();
                HeaderCell.Width = 110;
                HeaderCell.Height = 35;
                HeaderCell.BackColor = System.Drawing.Color.FromName("#496a9a");
                HeaderCell.Style.Add("Color", "White");
                HeaderCell.Text = "Name of SR";
                HeaderCell.RowSpan = 2;
                HeaderGridRow0.Cells.Add(HeaderCell);
            }
            else
            {
                HeaderCell = new TableCell();
                HeaderCell.Width = 110;
                HeaderCell.Height = 35;
                HeaderCell.ForeColor = System.Drawing.Color.FromName("#fff");
                HeaderCell.BackColor = System.Drawing.Color.FromName("#496a9a");
                HeaderCell.Text = "State";
                HeaderCell.RowSpan = 2;
                HeaderCell.Attributes["style"] = "font: Andalus";
                HeaderGridRow0.Cells.Add(HeaderCell);

                HeaderCell = new TableCell();
                HeaderCell.Width = 110;
                HeaderCell.Height = 35;
                HeaderCell.ForeColor = System.Drawing.Color.FromName("#fff");
                HeaderCell.BackColor = System.Drawing.Color.FromName("#496a9a");
                HeaderCell.Style.Add("Color", "White");
                HeaderCell.Text = "Block";
                HeaderCell.RowSpan = 2;
                HeaderCell.Attributes["style"] = "font: Andalus";
                HeaderGridRow0.Cells.Add(HeaderCell);

                HeaderCell = new TableCell();
                HeaderCell.Width = 110;
                HeaderCell.Height = 35;
                HeaderCell.BackColor = System.Drawing.Color.FromName("#496a9a");
                HeaderCell.Style.Add("Color", "White");
                HeaderCell.Text = "H.Q";
                HeaderCell.RowSpan = 2;
                HeaderGridRow0.Cells.Add(HeaderCell);

                HeaderCell = new TableCell();
                HeaderCell.Width = 110;
                HeaderCell.Height = 35;
                HeaderCell.BackColor = System.Drawing.Color.FromName("#496a9a");
                HeaderCell.Style.Add("Color", "White");
                HeaderCell.Text = "Erp Code";
                HeaderCell.RowSpan = 2;
                HeaderGridRow0.Cells.Add(HeaderCell);

                HeaderCell = new TableCell();
                HeaderCell.Width = 110;
                HeaderCell.Height = 35;
                HeaderCell.BackColor = System.Drawing.Color.FromName("#496a9a");
                HeaderCell.Style.Add("Color", "White");
                HeaderCell.Text = "Stockist Name";
                HeaderCell.RowSpan = 2;
                HeaderGridRow0.Cells.Add(HeaderCell);

            }

            string[] arr = new string[] { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };


            for (int a = 0; a < arr.Length; a++)
            {
                HeaderCell = new TableCell();
                HeaderCell.Width = 110;
                HeaderCell.Height = 35;
                HeaderCell.BackColor = System.Drawing.Color.FromName("#496a9a");
                HeaderCell.Style.Add("Color", "White");
                HeaderCell.Text = " " + arr[a] + Year;
                HeaderCell.ColumnSpan = 2;
                HeaderGridRow0.Cells.AddAt(HeaderGridRow0.Cells.Count, HeaderCell);

            }

            pri_vs_sec_sales_grid.Controls[0].Controls.AddAt(0, HeaderGridRow0);

            for (int i = 0; i < arr.Length; i++)
            {
                HeaderCell = new TableCell();

                HeaderCell.Width = 55;
                HeaderCell.Height = 35;
                HeaderCell.BackColor = System.Drawing.Color.FromName("#496a9a");
                HeaderCell.ForeColor = System.Drawing.Color.FromName("#fff");
                HeaderCell.Text = "Primary Sales";
                HeaderGridRow1.Cells.Add(HeaderCell);
                pri_vs_sec_sales_grid.Controls[0].Controls.AddAt(1, HeaderGridRow1);
                HeaderCell = new TableCell();

                HeaderCell.Width = 55;
                HeaderCell.Height = 35;
                HeaderCell.BackColor = System.Drawing.Color.FromName("#496a9a");
                HeaderCell.ForeColor = System.Drawing.Color.FromName("#fff");
                HeaderCell.Text = "Secondary Sales";
                HeaderGridRow1.Cells.Add(HeaderCell);
                pri_vs_sec_sales_grid.Controls[0].Controls.AddAt(1, HeaderGridRow1);

            }


        }

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            GridView HeaderGrid = (GridView)sender;
            GridViewRow HeaderGridRow0 = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);

            TableRow tableRow = new TableRow();
            DataTable dt = (e.Row.DataItem as DataRowView).DataView.Table;

            string[] arr1 = new string[] { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };       

            if (Type == "0")
            {
                e.Row.Cells[5].Visible = false;
                string sf_Code = Convert.ToString(dt.Rows[e.Row.RowIndex]["sf_code"]);

                for (int a = 0; a < arr1.Length; a++)
                {
                    DataRow[] drp = salesforce_primary.Select("Mnth='" + arr1[a] + "' and Sf_Code='" + sf_Code + "'");
                    DataRow[] drp2 = salesforce_secondary.Select("Mnth='" + arr1[a] + "' and Sf_Code='" + sf_Code + "'");

                    TableCell tableCell = new TableCell();
                    tableCell.Attributes["style"] = "font: Andalus";
                    tableCell.Attributes["style"] = "font: Bold";
                    tableCell.Text = drp.Length > 0 ? drp[0]["Primary_Order_Value"].ToString() : "0";
                    e.Row.Cells.AddAt(e.Row.Cells.Count, tableCell);

                    TableCell tableCell2 = new TableCell();
                    tableCell2.Attributes["style"] = "font: Andalus";
                    tableCell2.Attributes["style"] = "font: Bold";
                    tableCell2.Text = drp2.Length > 0 ? drp2[0]["sec_Order_Value"].ToString() : "0";
                    e.Row.Cells.AddAt(e.Row.Cells.Count, tableCell2);


                }
            }
            else
            {
                e.Row.Cells[4].Visible = false;
                string Stockist_Code = Convert.ToString(dt.Rows[e.Row.RowIndex]["stockist_code"]);

                for (int a = 0; a < arr1.Length; a++)
                {
                    DataRow[] drp = distributor_primary.Select("Mnth='" + arr1[a] + "' and Stockist_Code='" + Stockist_Code + "'");
                    DataRow[] drp2 = distributor_secondary.Select("Mnth='" + arr1[a] + "' and Stockist_Code='" + Stockist_Code + "'");

                    TableCell tableCell = new TableCell();
                    tableCell.Attributes["style"] = "font: Andalus";
                    tableCell.Attributes["style"] = "font: Bold";
                    tableCell.Text = drp.Length > 0 ? drp[0]["Primary_Order_Value"].ToString() : "0";
                    e.Row.Cells.AddAt(e.Row.Cells.Count, tableCell);

                    TableCell tableCell2 = new TableCell();
                    tableCell2.Attributes["style"] = "font: Andalus";
                    tableCell2.Attributes["style"] = "font: Bold";
                    tableCell2.Text = drp2.Length > 0 ? drp2[0]["sec_order_value"].ToString() : "0";
                    e.Row.Cells.AddAt(e.Row.Cells.Count, tableCell2);


                }
            }
        }
    }
}
