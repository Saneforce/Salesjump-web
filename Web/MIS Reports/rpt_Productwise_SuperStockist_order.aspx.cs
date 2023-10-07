using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
using System.Data.SqlClient;
using System.Web.Services;
using System.Configuration;
using Newtonsoft.Json;
using System.Globalization;
using ClosedXML.Excel;
using System.Xml;
using System.IO;
using System.Data.OleDb;
using DBase_EReport;


public partial class MIS_Reports_rpt_Productwise_SuperStockist_Order : System.Web.UI.Page
{
    public static string Div = string.Empty;
    public static string sf_type = string.Empty;
    public static string sfcode = string.Empty;
    public static string sfname = string.Empty;
    public static string subdiv = string.Empty;
    public static string FDT = string.Empty;
    public static string TDT = string.Empty;
    DataSet ss = new DataSet();
    DataSet ff = new DataSet();
    DataSet dsSalesForce = new DataSet();
    DataSet dsGV = new DataSet();

    protected void Page_Load(object sender, EventArgs e)
    {
        Div = Session["div_code"].ToString();
        sfcode = Request.QueryString["SF_Code"].ToString();
        sfname = Request.QueryString["SF_Name"].ToString();
        FDT = Request.QueryString["Fdate"].ToString();
        TDT = Request.QueryString["Tdate"].ToString();
        DateTime d1 = Convert.ToDateTime(FDT);
        DateTime d2 = Convert.ToDateTime(TDT);
        lblHead.Text = "Productwise SuperStockist Order From " + d1.ToString("dd-MM-yyyy") + " to " + d2.ToString("dd-MM-yyyy");
        lblsf_name.Text = sfname;
        FillSF();
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
        ss = dc.get_product_order(sfcode, Div, FDT, TDT);


        SalesForce SF = new SalesForce();
        dsGV = SF.GetProduct_Name(Div);

        ff = dc.get_distributor_order(sfcode, Div, FDT, TDT);

        if (ff.Tables[0].Rows.Count > 0)
        {
            // dsGV.Tables[0].Columns.RemoveAt(1);
            gdorder.DataSource = ff;
            gdorder.DataBind();
        }
        else
        {
            gdorder.DataSource = null;
            gdorder.DataBind();
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
            HeaderCell.Text = "Order_Date";
            HeaderGridRow0.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.BackColor = System.Drawing.Color.FromName("#496a9a");
            HeaderCell.ForeColor = System.Drawing.Color.FromName("#fff");
            HeaderCell.Text = "Distributor_Name";
            HeaderGridRow0.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.BackColor = System.Drawing.Color.FromName("#496a9a");
            HeaderCell.ForeColor = System.Drawing.Color.FromName("#fff");
            HeaderCell.Text = "Order_Taken_By";
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

            HeaderCell = new TableCell();
            HeaderCell.BackColor = System.Drawing.Color.FromName("#496a9a");
            HeaderCell.ForeColor = System.Drawing.Color.FromName("#fff");
            HeaderCell.Text = "Total_Qty (Nos)";
            HeaderGridRow0.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.BackColor = System.Drawing.Color.FromName("#496a9a");
            HeaderCell.ForeColor = System.Drawing.Color.FromName("#fff");
            HeaderCell.Text = "Total_Order Value(Rs)";
            HeaderGridRow0.Cells.Add(HeaderCell);


            gdorder.Controls[0].Controls.AddAt(0, HeaderGridRow0);
        }

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            GridView HeaderGrid = (GridView)sender;
            GridViewRow HeaderGridRow0 = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);

            TableRow tableRow = new TableRow();
            DataTable dt = (e.Row.DataItem as DataRowView).DataView.Table;            
            string order_date = Convert.ToString(dt.Rows[e.Row.RowIndex]["Order_Date"]);            
            int jk = 0;
            float tot = 0;
            float totv = 0;
            
            foreach (DataRow item in dsGV.Tables[0].Rows)
            {
                try
                {
                    //DataRow[] drp = ss.Tables[0].Select(" Product_Name='" + item["Product_Detail_Name"] + "' and Product_Code='" + item["Product_Detail_Code"] + "'");
                    DataRow[] drp = ss.Tables[0].Select("Order_Date='" + order_date + "' and Product_Code='" + item["Product_Detail_Code"] + "'");
                    if (drp.Length > 0)
                    {

                        TableCell tableCell = new TableCell();
                        tableCell.Attributes["style"] = "font: Andalus";
                        tableCell.Attributes["style"] = "font: Bold";
                        tableCell.Attributes["values"] = drp[0]["Order_Date"].ToString();
                        tableCell.Attributes["code"] = item["Product_Detail_Code"].ToString();
                        tableCell.Attributes["name"] = item["Product_Detail_Name"].ToString();
                        tableCell.CssClass = "remark";
                        tableCell.Text = drp[0]["OrderQty"].ToString(); // +" ( " + drdoctor["Product_Detail_Code"] + ")";
                        tot += Convert.ToUInt32(drp[0]["OrderQty"]);
                        totv += Convert.ToUInt32(drp[0]["order_value"]);
                        
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
                catch (Exception ex)
                {
                    throw ex;
                }
                jk++;
            }

            TableCell tableCell1 = new TableCell();
            tableCell1.Text = tot.ToString();
            e.Row.Cells.Add(tableCell1);
            TableCell tableCell2 = new TableCell();
            tableCell2.Text = totv.ToString();
            e.Row.Cells.Add(tableCell2);
            gdorder.Controls[0].Controls.AddAt(0, HeaderGridRow0);

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

    }
	
    protected void btnClose_Click(object sender, EventArgs e)
    {

    }
}