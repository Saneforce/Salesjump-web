using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Bus_EReport;
using iTextSharp.tool.xml;
using System.Web.UI.HtmlControls;
using DBase_EReport;

public partial class MIS_Reports_rptattendancefinal_daywise : System.Web.UI.Page
{
    string sfCode = string.Empty;
    string sfname = string.Empty;
    string divcode = string.Empty;
    string sf_type = string.Empty;
    string FDate = string.Empty;
    string TDate = string.Empty;
    string type = string.Empty;
    string h = string.Empty;
    string wrktypename = string.Empty;
    int sum_time = 0;
    DataSet dsSalesForce = new DataSet();
    DataSet dsdatee = new DataSet();
    DataSet dsDoc = null;
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
    int quantity2 = 0;
    string mode = string.Empty;
    string subdiv_code = string.Empty;
    DataSet dsGV = new DataSet();
    protected void Page_Load(object sender, EventArgs e)
    {
        divcode = Session["div_code"].ToString();

        sfname = Request.QueryString["Sf_Name"].ToString();
        sfCode = Request.QueryString["sf_code"].ToString();
        // imagepath = Request.QueryString["imgpath"].ToString();
        FDate = Request.QueryString["FromDate"].ToString();
        TDate = Request.QueryString["ToDate"].ToString();
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
        DateTime d1 = Convert.ToDateTime(FDate);
        DateTime d2 = Convert.ToDateTime(TDate);

        lblHead.Text = "Attendance Status View for the Month of " + d1.ToString("dd-MM-yyyy") + " to " + d2.ToString("dd-MM-yyyy");


        lblsf_name.Text = sfname;

        Fillstatusview();

        //else
        //{
        //    Fillmaximisedview();
        //}

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
                    row.Cells[0].RowSpan = previousRow.Cells[0].RowSpan < 2 ? 2 :
                                           previousRow.Cells[0].RowSpan + 1;

                    previousRow.Cells[0].Visible = false;
                    row.Cells[1].RowSpan = previousRow.Cells[0].RowSpan < 2 ? 2 :
                                           previousRow.Cells[0].RowSpan + 1;

                    previousRow.Cells[1].Visible = false;
                    row.Cells[2].RowSpan = previousRow.Cells[0].RowSpan < 2 ? 2 :
                                           previousRow.Cells[0].RowSpan + 1;

                    previousRow.Cells[2].Visible = false;
                }
                // }
            }
        }
    }
    private void Fillstatusview()
    {

        DataSet dsGV = new DataSet();
        DataSet dsGc = new DataSet();
        DCR dc = new DCR();

        dsGV = attendance_view_daywise(sfCode, divcode, FDate, TDate, subdiv_code);
        if (dsGV.Tables[0].Rows.Count > 0)
        {
            if (divcode == "107")
            {
                dsGV.Tables[0].Columns.RemoveAt(0);
                dsGV.Tables[0].Columns.RemoveAt(4);
                dsGV.Tables[0].Columns.RemoveAt(4);
                dsGV.Tables[0].Columns.RemoveAt(5);
                dsGV.Tables[0].Columns.RemoveAt(5);
                dsGV.Tables[0].Columns.RemoveAt(5);
                dsGV.Tables[0].Columns.RemoveAt(5);
                dsGV.Tables[0].Columns.Add("Total Leave", typeof(string));
            }
            else
            {
                dsGV.Tables[0].Columns.RemoveAt(4);
                dsGV.Tables[0].Columns.RemoveAt(0);
                dsGV.Tables[0].Columns.RemoveAt(3);
                dsGV.Tables[0].Columns.RemoveAt(3);
                dsGV.Tables[0].Columns.RemoveAt(4);
                dsGV.Tables[0].Columns.RemoveAt(4);
                dsGV.Tables[0].Columns.RemoveAt(4);
                dsGV.Tables[0].Columns.RemoveAt(4);
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
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        int tot = 0;
        try
        {

            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[0].Text = "Name";
                e.Row.Cells[0].Width = 325;
                e.Row.Cells[1].Width = 70;
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int i = (divcode == "107") ? 5 : 4;
                for (int colIndex = i; colIndex < e.Row.Cells.Count; colIndex++)
                {
                    int rowIndex = colIndex;
                    Label txtName = new Label();
                    txtName.Width = 25;
                    txtName.ID = "txtboxname" + colIndex;
                    txtName.Text = e.Row.Cells[colIndex].Text;
                    //txtName.AutoPostBack = true;  
                    //e.Row.Cells[colIndex].Controls.Add(txtName);  
                    Label txtName1 = new Label();
                    txtName1.Width = 25;
                    if ((divcode == "32") || (divcode == "47"))
                    {
                        if (txtName.Text == "&nbsp;")
                        {

                        }
                        else
                        {
                            //txtName1.Style.Add("Color", "Green");
                            //txtName1.Text = "&#61692;" + txtName;
                            txtName1.Text = txtName.Text;
                            txtName1.Style.Add("font-size", "11px");
                            txtName1.Style.Add("font", "Bold");
                        }
                    }
                    else
                    {
                        if ((divcode == "107" && txtName.Text == "L"))
                        {
                            tot++;
                            txtName1.Text = "L";
                            txtName1.Style.Add("font-size", "15px");
                            txtName1.Style.Add("font-weight", "900");
                        }
                        else if ((divcode == "107" && txtName.Text == "S"))
                        {
                            txtName1.Text = "S";
                            txtName1.Style.Add("font-size", "15px");
                            txtName1.Style.Add("font-weight", "900");
                        }
                        else if ((divcode == "107" && txtName.Text == "FU"))
                        {
                            txtName1.Text = "";
                        }
                        else
                        {
                            if (txtName.Text == "&nbsp;")
                            {
                                txtName1.Style.Add("Color", "Red");
                                txtName1.Text = "&#61691;";
                            }
                            else
                            {
                                txtName1.Style.Add("Color", "Green");
                                txtName1.Text = "&#61692;";
                            }
                            txtName1.Style.Add("font-family", "Wingdings, Times, serif");
                            txtName1.Style.Add("font-size", "20px");
                        }
                    }

                    //"font-family","Wingdings, Times, serif";color:Green;font-size:25px;);
                    //txtName1.BackColor = System.Drawing.Color.LightBlue;  
                    txtName1.ID = "txtboxname1" + colIndex;
                    //txtName1.ReadOnly = true;  
                    e.Row.Cells[colIndex].Controls.Add(txtName1);
                }
                if (divcode == "107")
                {
                    e.Row.Cells[e.Row.Cells.Count - 1].Text = tot.ToString();
                }
            }
        }


        catch (Exception ex)
        {
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
    public static DataSet attendance_view_daywise(string sf_code, string div_code, string month, string year, string subdiv_code)
    {
        DB_EReporting db_ER = new DB_EReporting();
        DataSet dsAdmin = null;
        string strQry = "exec [attendancedatewise] '" + sf_code + "','" + div_code + "','" + month + "','" + year + "','" + subdiv_code + "'";
        try
        {
            dsAdmin = db_ER.Exec_DataSet(strQry);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return dsAdmin;
    }
}