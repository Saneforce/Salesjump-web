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
using iTextSharp.tool.xml;
using System.Net;
using System.Text.RegularExpressions;
using System.Web.Services;
using System.Web.Script.Serialization;

public partial class MIS_Reports_rptattendancefinal : System.Web.UI.Page
{
    string sfCode = string.Empty;
    string sfname = string.Empty;
    string divcode = string.Empty;
    string sf_type = string.Empty;
    string FMonth = string.Empty;
    string FYear = string.Empty;
    string type = string.Empty;
    string h = string.Empty;
    string wrktypename = string.Empty;
    int sum_time = 0;
    DataSet dsSalesForce = new DataSet();
    DataSet dsdatee = new DataSet();
    DataSet dsDoc = null;
    DataSet dsGV = null;
    DateTime dtCurrent;
    TimeSpan ff;
    int rowspan = 0;
    string sCurrentDate = string.Empty;
    string endTime = string.Empty;
    string startTime = string.Empty;
    string tot_dr = string.Empty;
    string tot_value = string.Empty;
    string con_qty = string.Empty;
    string ec = string.Empty;
    string Monthsub = string.Empty;
    string date = string.Empty;
    string endd = string.Empty;
    DataSet dsDoctor = new DataSet();
    string gg = string.Empty;
    string imagepath = string.Empty;
    string subdiv_code = string.Empty;
    int quantity2 = 0;
    string mode = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        divcode = Session["div_code"].ToString();
        hdnDiv.Value = divcode.TrimEnd(',');
        sfname = Request.QueryString["sfname"].ToString();
        sfCode = Request.QueryString["sfcode"].ToString();
        imagepath = Request.QueryString["imgpath"].ToString();
        subdiv_code = Request.QueryString["subdiv"].ToString();
        logoo.ImageUrl = imagepath;
        //sfCode = "Admin";



        if (sfCode.Contains("MGR"))
        {
            sf_type = "2";
        }
        else if (sfCode.Contains("MR"))
        {
            sf_type = "1";
        }
        else
        {
            sf_type = "0";
        }
        type = Request.QueryString["type"].ToString();

        FMonth = Request.QueryString["FMonth"].ToString();
        FYear = Request.QueryString["FYear"].ToString();

        hdnYear.Value = FYear;
        hdnMonth.Value = FMonth;
        System.Globalization.DateTimeFormatInfo mfi = new System.Globalization.DateTimeFormatInfo();
        string strFMonthName = mfi.GetMonthName(Convert.ToInt16(FMonth)).ToString().Substring(0, 3);
        lblHead.Text = "Attendance View for the Month of " + strFMonthName + " " + FYear;
        htype.Value = type;

        lblsf_name.Text = sfname;
        if (type == "Minimised")
        {
            Fillminisedview();
        }
        else if (type == "Maximised")
        {
            Fillmaximisedview();
        }
        else
        {
            lblHead.Text = "Sales Value View for the Month of " + strFMonthName + " " + FYear;
            //Fillvaluewise();

        }

    }
    protected void gridView_PreRender(object sender, EventArgs e)
    {
        GridDecorator.MergeRows(gvtotalorder);
    }



    public class GridDecorator
    {
        DataSet dsGV = new DataSet();
        public static void MergeRows(GridView gridView)
        {
            for (int rowIndex = gridView.Rows.Count - 2; rowIndex >= 0; rowIndex--)
            {
                GridViewRow row = gridView.Rows[rowIndex];
                GridViewRow previousRow = gridView.Rows[rowIndex + 1];

                // for (int i = 0; i < row.Cells.Count - 32; i++)
                // {
                if (row.Cells[0].Text == previousRow.Cells[0].Text)
                {
                    DataSet dsGV = new DataSet();
                    DCR dc = new DCR();
                    row.Cells[0].RowSpan = previousRow.Cells[0].RowSpan < 2 ? 2 : previousRow.Cells[0].RowSpan + 1;
                    previousRow.Cells[0].Visible = false;
                    row.Cells[1].RowSpan = previousRow.Cells[0].RowSpan < 2 ? 2 : previousRow.Cells[0].RowSpan + 1;
                    previousRow.Cells[1].Visible = false;
                    row.Cells[2].RowSpan = previousRow.Cells[0].RowSpan < 2 ? 2 : previousRow.Cells[0].RowSpan + 1;
                    previousRow.Cells[2].Visible = false;

                }
                // }
            }
        }
    }
    private void Fillminisedview()
    {

        // DataSet dsGV = new DataSet();
        DataSet dsGc = new DataSet();
        DCR dc = new DCR();


        mode = "Minimised";


        dsGV = dc.attendance_view(sfCode, divcode, FMonth, FYear, mode, subdiv_code);
        if (dsGV.Tables[0].Rows.Count > 0)
        {
            dsGV.Tables[0].Columns.RemoveAt(0);
            dsGV.Tables[0].Columns.RemoveAt(3);
            dsGV.Tables[0].Columns.RemoveAt(3);
            dsGV.Tables[0].Columns.RemoveAt(4);
            dsGV.Tables[0].Columns.RemoveAt(4);
            dsGV.Tables[0].Columns.RemoveAt(4);
            dsGV.Tables[0].Columns.RemoveAt(4);
            gvtotalorder.DataSource = dsGV;
            gvtotalorder.DataBind();
        }
        else
        {
            gvtotalorder.DataSource = null;
            gvtotalorder.DataBind();
        }
    }


    private void Fillmaximisedview()
    {

        //DataSet dsGV = new DataSet();
        DataSet dsGc = new DataSet();
        DCR dc = new DCR();


        mode = "Maximised";

        dsGV = dc.attendance_view(sfCode, divcode, FMonth, FYear, mode, subdiv_code);
        if (dsGV.Tables[0].Rows.Count > 0)
        {
            //dsGV.Tables[0].Columns.RemoveAt(0);
            //dsGV.Tables[0].Columns.RemoveAt(3);
            //dsGV.Tables[0].Columns.RemoveAt(3);
            //dsGV.Tables[0].Columns.RemoveAt(4);
            //dsGV.Tables[0].Columns.RemoveAt(4);
            //dsGV.Tables[0].Columns.RemoveAt(4);
            //dsGV.Tables[0].Columns.RemoveAt(4);

            dsGV.Tables[0].Columns.RemoveAt(4);
            dsGV.Tables[0].Columns.RemoveAt(4);
            //dsGV.Tables[0].Columns.RemoveAt(3);
            dsGV.Tables[0].Columns.RemoveAt(5);
            dsGV.Tables[0].Columns.RemoveAt(5);
            dsGV.Tables[0].Columns.RemoveAt(5);
            dsGV.Tables[0].Columns.RemoveAt(5);
            if (divcode == "20")
            {
                dsGV.Tables[0].Columns.Add("MOBILE AND COURIER", typeof(string));
                dsGV.Tables[0].Columns.Add("Spl Metro Allowance", typeof(string));
            }
            gvtotalorder.DataSource = dsGV;
            gvtotalorder.DataBind();

        }
        else
        {
            gvtotalorder.DataSource = null;
            gvtotalorder.DataBind();
        }

    }
    private void Fillvaluewise()
    {

        //DataSet dsGV = new DataSet();
        DataTable dsGV = new DataTable();

        DCR dc = new DCR();


        mode = "valuewise";

        // dsGV = dc.attendance_view(sfCode, divcode, FMonth, FYear, mode);
        dsGV = dc.attendance_view_tb(sfCode, divcode, FMonth, FYear, mode);
        DataColumn dcol2 = new DataColumn("Total", typeof(string));
        dsGV.Columns.Add(dcol2);
        if (dsGV.Rows.Count > 0)
        {
            dsGV.Columns.RemoveAt(0);
            dsGV.Columns.RemoveAt(1);
            dsGV.Columns.RemoveAt(2);
            dsGV.Columns.RemoveAt(3);
            dsGV.Columns.RemoveAt(4);
            //dsGV.Tables[0].Columns.RemoveAt(5);
            //dsGV.Tables[0].Columns.RemoveAt(6);
            //dsGV.Tables[0].Columns.RemoveAt(7);
            //dsGV.Tables[0].Columns.RemoveAt(8);
            //dsGV.Tables[0].Columns.RemoveAt(9);
            //dsGV.Tables[0].Columns.RemoveAt(10);
            //dsGV.Tables[0].Columns.RemoveAt(11);
            //dsGV.Tables[0].Columns.RemoveAt(12);
            //dsGV.Tables[0].Columns.v;
            GridView1.DataSource = dsGV;
            GridView1.DataBind();
            //int total = 0; ;

            if (dsGV.Rows.Count > 0)
            {
                for (int i = 0; i < dsGV.Rows.Count; i++)
                {
                    for (int j = 0; j < dsGV.Columns.Count; j++)
                    {
                        if (string.IsNullOrEmpty(dsGV.Rows[i][j].ToString()))
                        {

                            dsGV.Rows[i][j] = 0;

                        }
                        else
                        {
                            if (j > 9)
                            {
                                dsGV.Rows[i][j] = Math.Round(Convert.ToDouble(dsGV.Rows[i][j]), 9).ToString();
                            }
                        }

                    }
                }

                dsGV.Columns.RemoveAt(0);
                dsGV.Columns.RemoveAt(0);
                dsGV.Columns.RemoveAt(0);
                dsGV.Columns.RemoveAt(0);
                dsGV.Columns.RemoveAt(0);
                //GridView1.FooterRow.Cells.RemoveAt(0);
                //GridView1.FooterRow.Cells.RemoveAt(0);
                // GridView1.FooterRow.Cells.RemoveAt(0);
                // GridView1.FooterRow.Cells.RemoveAt(0);
                // GridView1.FooterRow.Cells.RemoveAt(0);
                GridView1.FooterRow.Cells[0].Text = "Total";
                GridView1.FooterRow.Cells[0].ColumnSpan = 1;
                //gvtotalorder.FooterRow.Cells[1].Font.Bold = true;
                //gvtotalorder.FooterRow.Cells[1].HorizontalAlign = HorizontalAlign.Left;
                double Gtot = 0;
                for (int h = 0; h < dsGV.Rows.Count; h++)
                {
                    double rtot = 0;

                    for (int k = 0; k < dsGV.Columns.Count; k++)
                    {
                        //string rowtot = "0";

                        // rowtot = dsGV.AsEnumerable().Sum(x => Convert.ToDecimal(x.Field<String>((dsGV.Rows[h].ItemArray[k].ToString())))).ToString();
                        //double otherNumber = dsGV.Rows[h].Field<int>(k);
                        object o = dsGV.Rows[h].ItemArray[k];

                        rtot += Convert.ToDouble(o);

                    }

                    GridView1.Rows[h].Cells[dsGV.Columns.Count + 4].Text = Convert.ToString(rtot);
                    GridView1.Rows[h].Cells[dsGV.Columns.Count + 4].Font.Bold = true;
                    GridView1.Rows[h].Cells[dsGV.Columns.Count + 4].BackColor = System.Drawing.Color.FromName("#D0ECE7");
                    Gtot += rtot;


                }


                int kk = 0;
                for (int k = 5; k < GridView1.Rows[0].Cells.Count; k++)
                {
                    kk = k;

                    string total = "0";

                    total = dsGV.AsEnumerable().Sum(x => Convert.ToDecimal(x.Field<String>((dsGV.Columns[k - 5].ToString())))).ToString();



                    GridView1.FooterRow.Cells[k].Text = total.ToString();
                    GridView1.FooterRow.Cells[k].Font.Bold = true;
                    GridView1.FooterRow.BackColor = System.Drawing.Color.FromName("#D0ECE7");
                }

                GridView1.FooterRow.Cells[kk].Text = Gtot.ToString();
                GridView1.FooterRow.Cells[kk].Font.Bold = true;


            }
            else
            {
                GridView1.DataSource = null;
                GridView1.DataBind();
            }

        }
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
                Document pdfDoc = new Document(PageSize.A4,
                    10f, 10f, 10f, 0f);
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



    protected void btnClose_Click(object sender, EventArgs e)
    {

    }
    protected void gvtotalorder_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //e.Row.Cells[2].Visible = false;
        //e.Row.Cells[3].Visible = false;
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        e.Row.Cells[2].Visible = false;
        e.Row.Cells[3].Visible = false;
        e.Row.Cells[4].Visible = false;
    }




    [WebMethod(EnableSession = true)]
    public static string GetTcEcDetails(string FYear, string FMonth)
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
        SalesForce sf = new SalesForce();
        DataSet dsTcEc = new DataSet();
        dsTcEc = sf.GetSummaryTCECInventz(div_code, FYear, FMonth);

        JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
        List<Dictionary<string, object>> parentRow = new List<Dictionary<string, object>>();
        Dictionary<string, object> childRow;
        foreach (DataRow row in dsTcEc.Tables[0].Rows)
        {
            childRow = new Dictionary<string, object>();
            foreach (DataColumn col in dsTcEc.Tables[0].Columns)
            {
                childRow.Add(col.ColumnName, row[col]);
            }
            parentRow.Add(childRow);
        }
        return jsSerializer.Serialize(parentRow);
    }


}