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

public partial class MIS_Reports_rpt_retailer_outstanding : System.Web.UI.Page
{
    string sfCode = string.Empty;
    string sfname = string.Empty;
    string divcode = string.Empty;
    string Year = string.Empty;
    string Month = string.Empty;
    DataSet dsSalesForce = new DataSet();
    DataSet dsmgrsf = new DataSet();
    DataSet dsDoc = null;
    DateTime dtCurrent;
    string tot_dr = string.Empty;
    string Monthsub = string.Empty;
    string Sf_Code = string.Empty;
    string MultiSf_Code = string.Empty;
    DataSet dsprd = new DataSet();
    string distributor_code = string.Empty;
    int subTotalRowIndex = 0;
    string sf_type = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {


        divcode = Session["div_code"].ToString();
        Sf_Code = Request.QueryString["SF_Code"].ToString();
        sfname = Request.QueryString["SF_Name"].ToString();
        Month = Request.QueryString["Month"].ToString();
        Year = Request.QueryString["Year"].ToString();
        System.Globalization.DateTimeFormatInfo mfi = new System.Globalization.DateTimeFormatInfo();
        string strFMonthName = mfi.GetMonthName(Convert.ToInt16(Month)).ToString().Substring(0, 3);
        lblHead.Text = "Retailerwise Outstanding Analysis For  " + strFMonthName + " " + Year;
        Feild.Text = sfname;
        FillSF();


    }

    private void FillSF()
    {


        string sURL = string.Empty;
        string stURL = string.Empty;
        DataSet dsGV = new DataSet();
        DataSet dsGc = new DataSet();
        DCR dc = new DCR();

        string stCrtDtaPnt = string.Empty;
        //if (sf_type == "4")
        //{
        dsGV = dc.retailer_otstanding_value(Sf_Code, divcode, Year, Month);
        if (dsGV.Tables[0].Rows.Count > 0)
        {
            gvoutstandinganalysis.DataSource = dsGV;
            gvoutstandinganalysis.DataBind();
        }
        else
        {
            gvoutstandinganalysis.DataSource = null;
            gvoutstandinganalysis.DataBind();
        }
        //Label1.Visible = false;

        //}
        //else
        //{




        //dsGV = dc.view_total_order_view(divcode, sfCode, Date);
        //if (dsGV.Tables[0].Rows.Count > 0)
        //{
        //    gvtotalorder.DataSource = dsGV;
        //    gvtotalorder.DataBind();
        //}
        //else
        //{
        //    gvtotalorder.DataSource = null;
        //    gvtotalorder.DataBind();
        //}


        // }
    }


    protected void ExportToExcel(object sender, EventArgs e)
    {
        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=Retaileroutstanding.xls");
        Response.Charset = "";
        Response.ContentType = "application/vnd.ms-excel";
        using (StringWriter sw = new StringWriter())
        {
            HtmlTextWriter hw = new HtmlTextWriter(sw);

            //To Export all pages
            //gvclosingstockanalysis.AllowPaging = false;
            //this.BindGrid();

            gvoutstandinganalysis.HeaderRow.BackColor = Color.White;
            foreach (TableCell cell in gvoutstandinganalysis.HeaderRow.Cells)
            {
                cell.BackColor = gvoutstandinganalysis.HeaderStyle.BackColor;
            }
            foreach (GridViewRow row in gvoutstandinganalysis.Rows)
            {
                row.BackColor = Color.White;
                foreach (TableCell cell in row.Cells)
                {
                    if (row.RowIndex % 2 == 0)
                    {
                        cell.BackColor = gvoutstandinganalysis.AlternatingRowStyle.BackColor;
                    }
                    else
                    {
                        cell.BackColor = gvoutstandinganalysis.RowStyle.BackColor;
                    }
                    cell.CssClass = "textmode";
                }
            }

            gvoutstandinganalysis.RenderControl(hw);

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
        /* Verifies that the control is rendered */
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