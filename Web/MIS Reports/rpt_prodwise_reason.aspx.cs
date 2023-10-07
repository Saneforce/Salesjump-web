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

public partial class MIS_Reports_rpt_prodwise_reason : System.Web.UI.Page
{

    string divcode = string.Empty;
    string sfname = string.Empty;
    public string Sf_Code = string.Empty;
    string sf_type = string.Empty;
    public string date = string.Empty;   
    public string todate = string.Empty;
    public string remarksid = string.Empty;
    string subdivcode = string.Empty;
    DataSet ss = new DataSet();
    DataSet ff = new DataSet();

    DataSet dsGV = new DataSet();
    Int64[] Rcnt;
    Int64 Pcnt = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        divcode = Session["div_code"].ToString();
        sfname = Request.QueryString["SF_Name"].ToString();
        Sf_Code = Request.QueryString["Sf_Code"].ToString();
        date = Request.QueryString["date"].ToString();
        todate = Request.QueryString["tdate"].ToString();
        subdivcode = Request.QueryString["subdiv"].ToString();
        FillSF();
        DateTime d1 = Convert.ToDateTime(date);
        DateTime d2 = Convert.ToDateTime(todate);

        lblHead.Text = "Productwise Reason Analysis from " + d1.ToString("dd-MM-yyyy") + " to " + d2.ToString("dd-MM-yyyy");
        Feild.Text = sfname;
    }


    private void FillSF()
    {


        string sURL = string.Empty;
        string stURL = string.Empty;
        DataSet dsGc = new DataSet();
        DCR dc = new DCR();

        string stCrtDtaPnt = string.Empty;

        dsGV = dc.getRemarks(divcode);
        Rcnt = new Int64[dsGV.Tables[0].Rows.Count];

            ss = new DataSet();
            ss = dc.getRemarksCount(divcode, Sf_Code, date, todate);


            ff = dc.prodreason(divcode, Sf_Code, date, todate);
            if (ff.Tables[0].Rows.Count > 0)
            {
                // dsGV.Tables[0].Columns.RemoveAt(1);
                gvincentive.DataSource = ff;
                gvincentive.DataBind();
            }
            else
            {
                gvincentive.DataSource = null;
                gvincentive.DataBind();
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
            HeaderCell.Text = "Product";
            HeaderGridRow0.Cells.Add(HeaderCell);


            foreach (DataRow drdoctor in dsGV.Tables[0].Rows)
            {
                HeaderCell = new TableCell();
                HeaderCell.Height = 35;
                HeaderCell.BackColor = System.Drawing.Color.FromName("#496a9a");
                HeaderCell.ForeColor = System.Drawing.Color.FromName("#fff");
                string remarkid = Convert.ToString(drdoctor["Remarks_Id"]);

                HeaderCell.Attributes["style"] = "font: Andalus";
                HeaderCell.Attributes["style"] = "font: Bold";
                HeaderCell.Text = drdoctor["Remarks_Content"].ToString();
                HeaderGridRow0.Cells.Add(HeaderCell);
            }

            HeaderCell = new TableCell();
            HeaderCell.BackColor = System.Drawing.Color.FromName("#496a9a");
            HeaderCell.ForeColor = System.Drawing.Color.FromName("#fff");
            HeaderCell.Text = "Productwise Total";
            HeaderGridRow0.Cells.Add(HeaderCell);


            gvincentive.Controls[0].Controls.AddAt(0, HeaderGridRow0);

            //GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
        }



        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            GridView HeaderGrid = (GridView)sender;
            GridViewRow HeaderGridRow0 = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);

            TableRow tableRow = new TableRow();

            DataTable dt = (e.Row.DataItem as DataRowView).DataView.Table;
            string pcode = Convert.ToString(dt.Rows[e.Row.RowIndex]["Product_Code"]);
            string pname = Convert.ToString(dt.Rows[e.Row.RowIndex]["Product_Name"]);
            e.Row.Cells[0].Visible = false;
            int jk = 0;
            foreach (DataRow item in dsGV.Tables[0].Rows)
            {
                try
                {
                    DataRow[] drp = ss.Tables[0].Select("Product_Code='" + pcode +  "' and Remarks='" + item["Remarks_Content"] +"'");


                    if (drp.Length > 0)
                    {

                        TableCell tableCell = new TableCell();
                        tableCell.Attributes["style"] = "font: Andalus";
                        tableCell.Attributes["style"] = "font: Bold";
                        tableCell.Attributes["values"] = item["Remarks_Id"].ToString();
                        tableCell.Attributes["value"] = item["Remarks_Content"].ToString();
                        tableCell.Attributes["code"] = pcode.ToString();
                        tableCell.Attributes["name"] = pname.ToString();
                        tableCell.CssClass = "remark";
                        tableCell.Text = drp[0]["CNT"].ToString();// +" ( " + drdoctor["Product_Detail_Code"] + ")";
                        Rcnt[jk] += Convert.ToInt64(drp[0]["CNT"]);
                        Pcnt += Convert.ToInt64(drp[0]["CNT"]);
                        e.Row.Cells.Add(tableCell);
                        
                        
                    }
                    else
                    {

                        TableCell tableCell = new TableCell();
                        tableCell.Attributes["style"] = "font: Andalus";
                        tableCell.Attributes["style"] = "font: Bold";
                        tableCell.Text = "0";
                        e.Row.Cells.Add(tableCell);

                    }
                }
                catch { }
                jk++;
            }
            try
            {

                TableCell tableCellBal = new TableCell();
                tableCellBal.Attributes["style"] = "font: Andalus";
                tableCellBal.Attributes["style"] = "font: Bold";
                tableCellBal.Text = Pcnt.ToString();
                e.Row.Cells.Add(tableCellBal);

            }
            catch
            {
            }
            Pcnt = 0;


            gvincentive.Controls[0].Controls.AddAt(0, HeaderGridRow0);

            //GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
        }

    }
    protected void gridView_PreRender(object sender, EventArgs e)
    {
        //  GridDecorator.MergeRows(gvincentive);
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
        GridViewRow rw = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);

        TableCell TotalCell = new TableCell();

        TotalCell.Width = 55;
        TotalCell.Height = 35;
        TotalCell.BackColor = System.Drawing.Color.FromName("#496a9a");
        TotalCell.ForeColor = System.Drawing.Color.FromName("#fff");
        TotalCell.Text = "Reasonwise Total";
        rw.Cells.Add(TotalCell);

        for (int i = 0; i < Rcnt.Length; i++)
        {
            TableCell HeaderCell = new TableCell();

            HeaderCell.Width = 55;
            HeaderCell.Height = 35;
            HeaderCell.BackColor = System.Drawing.Color.FromName("#496a9a");
            HeaderCell.ForeColor = System.Drawing.Color.FromName("#fff");
            HeaderCell.Text = Rcnt[i].ToString();
            rw.Cells.Add(HeaderCell);
        }
        TableCell totcell = new TableCell();
        totcell.Width = 55;
        totcell.Height = 35;
        totcell.BackColor = System.Drawing.Color.FromName("#496a9a");
        totcell.ForeColor = System.Drawing.Color.FromName("#fff");
        totcell.Text = "";
        rw.Cells.Add(totcell);


        gvincentive.Controls[0].Controls.Add(rw);
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


    protected void btnExcel_Click(object sender, EventArgs e)
    {
        DataTable dsProd1 = null;
        DCR ret = new DCR();
        try
        {
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "Reasonwise Retailer from " + date + " to " + todate + ".xls"));
            Response.ContentType = "application/ms-excel";
            dsProd1 = ret.reasonretailerraw(divcode, subdivcode, Sf_Code, date, todate);
            DataTable dt = dsProd1;
            string str = string.Empty;
            foreach (DataColumn dtcol in dt.Columns)
            {
                Response.Write(str + dtcol.ColumnName);
                str = "\t";
            }
            Response.Write("\n");
            foreach (DataRow dr in dt.Rows)
            {
                str = "";
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    Response.Write(str + Convert.ToString(dr[j]));
                    str = "\t";
                }
                Response.Write("\n");
            }
            Response.End();
        }
        catch (Exception ex)
        {
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('" + ex.Message + "');</script>");
        }
    }


    protected void btnClose_Click(object sender, EventArgs e)
    {

    }


}