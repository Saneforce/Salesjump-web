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
using System.Net;
using System.Web.UI.DataVisualization.Charting;
public partial class MasterFiles_MR_UnListedDoctor_rptUnlisteddr_Terr_View : System.Web.UI.Page
{
    DataSet dsDoctor = null;
    DataSet dsTerritory = null;
    string sf_code = string.Empty;
    string terr_code = string.Empty;
    string catg = string.Empty;
    string cat_name = string.Empty;
    string sf_name = string.Empty;
    string terr_Name = string.Empty;
    string type = string.Empty;
    int iDRCatg = -1;
    int iTotal_FF = 0;
    int[] iTotal_Catg = new int[20];
    int tot_catg = 0;
    int i = -1;
    string sDRCatg_Count = string.Empty;
    string div_code = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        sf_code = Session["sf_code"].ToString();
        div_code = Session["div_code"].ToString();
        if (Request.QueryString["sf_code"] != null)
        {
            sf_code = Request.QueryString["sf_code"].ToString();
        }
        else
        {
            sf_code = "-1";
        }
        if (Request.QueryString["terr_code"] != null)
        {
            terr_code = Request.QueryString["terr_code"].ToString();
        }
        else
        {
            terr_code = "-1";
        }



        if (Request.QueryString["terr_Name"] != null)
            terr_Name = Request.QueryString["terr_Name"].ToString();

        if (Request.QueryString["cat_code"] != null)
        {
            catg = Request.QueryString["cat_code"].ToString();
        }
        else
        {
            catg = "-1";
        }

        if (Request.QueryString["cat_name"] != null)
            cat_name = Request.QueryString["cat_name"].ToString();

        type = Request.QueryString["type"].ToString();

        Session["div_code"] = Request.QueryString["div"].ToString();
        Territory terr = new Territory();
        dsTerritory = terr.getWorkAreaName(div_code);

        if ((type == "0") && (terr_code != "-1") && (catg != "-1"))
        {
            if (dsTerritory.Tables[0].Rows.Count > 0)
            {
                lblHead.Text = lblHead.Text + "(" + dsTerritory.Tables[0].Rows[0]["wrk_area_Name"].ToString() + ":" + terr_Name + " / Category: " + cat_name + ")";
            }
        }
        else if ((type == "0") && (terr_code == "-1") && (catg != "-1"))
        {
            lblHead.Text = lblHead.Text + "(Category: " + cat_name + ")";
        }
        else if ((type == "0") && (terr_code != "-1") && (catg == "-1"))
        {
            if (dsTerritory.Tables[0].Rows.Count > 0)
            {
                lblHead.Text = lblHead.Text + "(" + dsTerritory.Tables[0].Rows[0]["wrk_area_Name"].ToString() + ":" + terr_Name + ")";
            }
        }
        else if ((type == "1") && (terr_code != "-1") && (catg != "-1"))
        {
            if (dsTerritory.Tables[0].Rows.Count > 0)
            {
                lblHead.Text = lblHead.Text + "(" + dsTerritory.Tables[0].Rows[0]["wrk_area_Name"].ToString() + ":" + terr_Name + " / Speciality: " + cat_name + ")";
            }
        }
        else if ((type == "1") && (terr_code == "-1") && (catg != "-1"))
        {
            lblHead.Text = lblHead.Text + "(Speciality: " + cat_name + ")";
        }
        else if ((type == "1") && (terr_code != "-1") && (catg == "-1"))
        {
            if (dsTerritory.Tables[0].Rows.Count > 0)
            {
                lblHead.Text = lblHead.Text + "(" + dsTerritory.Tables[0].Rows[0]["wrk_area_Name"].ToString() + ":" + terr_Name + ")";
            }
        }
        else if ((type == "2") && (terr_code != "-1") && (catg != "-1"))
        {
            if (dsTerritory.Tables[0].Rows.Count > 0)
            {
                lblHead.Text = lblHead.Text + "(" + dsTerritory.Tables[0].Rows[0]["wrk_area_Name"].ToString() + ":" + terr_Name + " / Class: " + cat_name + ")";
            }
        }
        else if ((type == "2") && (terr_code == "-1") && (catg != "-1"))
        {
            lblHead.Text = lblHead.Text + "(Class: " + cat_name + ")";
        }
        else if ((type == "2") && (terr_code != "-1") && (catg == "-1"))
        {
            if (dsTerritory.Tables[0].Rows.Count > 0)
            {
                lblHead.Text = lblHead.Text + "(" + dsTerritory.Tables[0].Rows[0]["wrk_area_Name"].ToString() + ":" + terr_Name + ")";
            }
        }
        else if ((type == "3") && (terr_code != "-1") && (catg != "-1"))
        {
            if (dsTerritory.Tables[0].Rows.Count > 0)
            {
                lblHead.Text = lblHead.Text + "(" + dsTerritory.Tables[0].Rows[0]["wrk_area_Name"].ToString() + ":" + terr_Name + " / Qualification: " + cat_name + ")";
            }
        }
        else if ((type == "3") && (terr_code == "-1") && (catg != "-1"))
        {
            lblHead.Text = lblHead.Text + "(Qualification: " + cat_name + ")";
        }
        else if ((type == "3") && (terr_code != "-1") && (catg == "-1"))
        {
            if (dsTerritory.Tables[0].Rows.Count > 0)
            {
                lblHead.Text = lblHead.Text + "(" + dsTerritory.Tables[0].Rows[0]["wrk_area_Name"].ToString() + ":" + terr_Name + ")";
            }
        }

        if (Request.QueryString["mgr_code"] != null)
        {
            Doctor dc = new Doctor();
            dsDoctor = dc.getUnListDoctorMgr_list(Request.QueryString["mgr_code"].ToString());

            if (dsDoctor.Tables[0].Rows.Count > 0)
            {
                grdDoctor.Visible = true;
                grdDoctor.DataSource = dsDoctor;
                grdDoctor.DataBind();
            }

        }
        else
        {
            Doctor dc = new Doctor();
            dsDoctor = dc.getUnlistDoctorCategory_list(sf_code, catg, terr_code, type);

            if (dsDoctor.Tables[0].Rows.Count > 0)
            {
                grdDoctor.Visible = true;
                grdDoctor.DataSource = dsDoctor;
                grdDoctor.DataBind();
            }

            FillSalesForce();

        }
    }
    protected void grdDoctor_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            Territory terr = new Territory();
            DataSet dsTerritory = new DataSet();
            dsTerritory = terr.getWorkAreaName(div_code);
            if (dsTerritory.Tables[0].Rows.Count > 0)
            {
                e.Row.Cells[7].Text = dsTerritory.Tables[0].Rows[0]["wrk_area_Name"].ToString();

            }
        }
    }
    private void FillSalesForce()
    {
        int tot_rows = 0;
        int tot_cols = 0;

        // Fetch the total columns for the table
        Doctor dr = new Doctor();

        dsDoctor = dr.getDocCat(Session["div_code"].ToString());
        if (dsDoctor.Tables[0].Rows.Count > 0)
        {
            tot_cols = dsDoctor.Tables[0].Rows.Count;
            ViewState["dsDoctor"] = dsDoctor;
        }


        CreateDynamicTable();
    }

    private void CreateDynamicTable()
    {
        dsDoctor = (DataSet)ViewState["dsDoctor"];
        TableRow tr_catg = new TableRow();
        foreach (DataRow dataRow in dsDoctor.Tables[0].Rows)
        {
            TableCell tc_catg_name = new TableCell();
            tc_catg_name.BorderStyle = BorderStyle.Solid;
            tc_catg_name.BorderWidth = 1;
            Literal lit_catg_name = new Literal();
            lit_catg_name.Text = "<center><b>" + dataRow["Doc_Cat_Name"].ToString() + "</b></center>";
            tc_catg_name.Controls.Add(lit_catg_name);
            tr_catg.Cells.Add(tc_catg_name);
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
        string attachment = "attachment; filename=Export.xls";
        Response.ClearContent();
        Response.AddHeader("content-disposition", attachment);
        Response.ContentType = "application/ms-excel";
        StringWriter sw = new StringWriter();
        HtmlTextWriter htw = new HtmlTextWriter(sw);
        HtmlForm frm = new HtmlForm();
        pnlContents.Parent.Controls.Add(frm);
        frm.Attributes["runat"] = "server";
        frm.Controls.Add(pnlContents);
        frm.RenderControl(htw);
        Response.Write(sw.ToString());
        Response.End();
    }
    protected void btnPDF_Click(object sender, EventArgs e)
    {
        string strFileName = "rptTPView";
        Response.ContentType = "application/pdf";
        Response.AddHeader("content-disposition", "attachment;filename=" + strFileName + ".pdf");
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        StringWriter sw = new StringWriter();
        HtmlTextWriter hw = new HtmlTextWriter(sw);
        HtmlForm frm = new HtmlForm();
        pnlContents.Parent.Controls.Add(frm);
        frm.Attributes["runat"] = "server";
        frm.Controls.Add(pnlContents);
        frm.RenderControl(hw);
        StringReader sr = new StringReader(sw.ToString());
        Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 4f);
        iTextSharp.text.html.simpleparser.HTMLWorker htmlparser = new iTextSharp.text.html.simpleparser.HTMLWorker(pdfDoc);
        PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
        pdfDoc.Open();
        htmlparser.Parse(sr);
        pdfDoc.Close();
        Response.Write(pdfDoc);
        Response.End();
    }
}