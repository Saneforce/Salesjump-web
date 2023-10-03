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

public partial class Reports_rptMRStatus : System.Web.UI.Page
{
    DataSet dsFF = null;
    string sf_code = string.Empty;
    string catg = string.Empty;
    string cat_name = string.Empty;
    string sf_name = string.Empty;
    string type = string.Empty;
    string sDRCatg_Count = string.Empty;
    string strSF_code = string.Empty;
    string div_code = string.Empty;
    int iType = -1;

    protected void Page_Load(object sender, EventArgs e)
    {
        sf_code = Request.QueryString["sf_code"].ToString();
        sf_name = Request.QueryString["sf_name"].ToString();


        if (Request.QueryString["div_code"] != null)
        {
            div_code = Request.QueryString["div_code"].ToString();
        }

        if (Session["StrDiv_Code"] != null)
        {
            div_code = Session["StrDiv_Code"].ToString();
        }
        type = Request.QueryString["type"].ToString();
        iType = Convert.ToInt32( Request.QueryString["status"].ToString());
        strSF_code = Session["Sf_Code_multiple"].ToString();
        if (!Page.IsPostBack)
        {
            if (sf_code != "0")
            {
                lblHead.Text = lblHead.Text + "(Rep: " + sf_name + " )";
            }
        }

        

        SalesForce sf = new SalesForce();

        if (type == "1")
        {
            if (iType == 0)
            {
                lblActiveHeader.Text = "Active Territory Details";
            }
            else
            {
                lblActiveHeader.Text = "De-Active Territory Details";
            }
            Territory terr = new Territory();
            if (sf_code != "0")
            {
                //dsFF = sf.getTerritory_Rep(sf_code, iType);
                
                dsFF = terr.getTerritory(sf_code);
            }
            else
            {
                //dsFF = sf.getTerritory_Rep_Total(div_code, iType, strSF_code);
                dsFF = terr.getTerritory_Total(strSF_code);
            }

            if (dsFF.Tables[0].Rows.Count > 0)
            {
                grdTerritory.Visible = true;
                grdTerritory.DataSource = dsFF;
                grdTerritory.DataBind();
            }

            grdDoctor.DataSource = null;
            grdDoctor.DataBind();
            grdNonDR.DataSource = null;
            grdNonDR.DataBind();
            grdChem.DataSource = null;
            grdChem.DataBind();
            grdStok.DataSource = null;
            grdStok.DataBind();
        }
        else if (type == "2")
        {

            if (iType == 0)
            {
                lblActiveHeader.Text = "Active Listed Doctor Details";
            }
            else
            {
                lblActiveHeader.Text = "De-Active Listed Doctor Details";
            }

            if (sf_code != "0")
            {
                dsFF = sf.getDoctor_Rep(sf_code, iType);
            }
            else
            {
                dsFF = sf.getDoctor_Rep_Total(div_code, iType, strSF_code);
            }

            if (dsFF.Tables[0].Rows.Count > 0)
            {
                grdDoctor.Visible = true;
                grdDoctor.DataSource = dsFF;
                grdDoctor.DataBind();
            }

            grdTerritory.DataSource = null;
            grdTerritory.DataBind();
            grdNonDR.DataSource = null;
            grdNonDR.DataBind();
            grdChem.DataSource = null;
            grdChem.DataBind();
            grdStok.DataSource = null;
            grdStok.DataBind();
        }
        else if (type == "3")
        {

            if (iType == 0)
            {
                lblActiveHeader.Text = "Active UnListed Doctor Details";
            }
            else
            {
                lblActiveHeader.Text = "De-Active UnListed Doctor Details";
            }

            if (sf_code != "0")
            {
                dsFF = sf.getNonDoctor_Rep(sf_code, iType);
            }
            else
            {
                dsFF = sf.getNonDoctor_Rep_Total(div_code, iType, strSF_code);
            }

            if (dsFF.Tables[0].Rows.Count > 0)
            {
                grdNonDR.Visible = true;
                grdNonDR.DataSource = dsFF;
                grdNonDR.DataBind();
            }

            grdTerritory.DataSource = null;
            grdTerritory.DataBind();
            grdDoctor.DataSource = null;
            grdDoctor.DataBind();
            grdChem.DataSource = null;
            grdChem.DataBind();
            grdStok.DataSource = null;
            grdStok.DataBind();
        }
        else if (type == "4")
        {

            if (iType == 0)
            {
                lblActiveHeader.Text = "Active Chemists Details";
            }
            else
            {
                lblActiveHeader.Text = "De-Active Chemists Details";
            }

            if (sf_code != "0")
            {
                dsFF = sf.getChemists_Rep(sf_code, iType);
            }
            else
            {
                dsFF = sf.getChemists_Rep_Total(div_code, iType, strSF_code);
            }

            if (dsFF.Tables[0].Rows.Count > 0)
            {
                grdChem.Visible = true;
                grdChem.DataSource = dsFF;
                grdChem.DataBind();
            }

            grdTerritory.DataSource = null;
            grdTerritory.DataBind();
            grdDoctor.DataSource = null;
            grdDoctor.DataBind();
            grdNonDR.DataSource = null;
            grdNonDR.DataBind();
            grdStok.DataSource = null;
            grdStok.DataBind();
        }
        else if (type == "5")
        {

            if (iType == 0)
            {
                lblActiveHeader.Text = "Active Stockist Details";
            }
            else
            {
                lblActiveHeader.Text = "De-Active Stockist Details";
            }

            if (sf_code != "0")
            {
                dsFF = sf.getStockiest_Rep(sf_code, iType);
            }
            else
            {
                dsFF = sf.getStockiest_Rep_Total(div_code, iType, strSF_code);
            }

            if (dsFF.Tables[0].Rows.Count > 0)
            {
                grdStok.Visible = true;
                grdStok.DataSource = dsFF;
                grdStok.DataBind();
            }

            grdTerritory.DataSource = null;
            grdTerritory.DataBind();
            grdDoctor.DataSource = null;
            grdDoctor.DataBind();
            grdNonDR.DataSource = null;
            grdNonDR.DataBind();
            grdChem.DataSource = null;
            grdChem.DataBind();
           
        }
        Exportbutton();
    }

    private void Exportbutton()
    {
        btnExcel.Visible = true;
        btnPDF.Visible = false;
        btnPrint.Visible = true;
        btnClose.Visible = true;
    }
    protected void grdTerritory_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            Territory terr = new Territory();
            DataSet dsTerritory = new DataSet();
            dsTerritory = terr.getWorkAreaName(div_code);
            if (dsTerritory.Tables[0].Rows.Count > 0)
            {
                e.Row.Cells[2].Text = dsTerritory.Tables[0].Rows[0]["wrk_area_Name"].ToString();
            }
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
    protected void grdNonDR_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            Territory terr = new Territory();
            DataSet dsTerritory = new DataSet();
            dsTerritory = terr.getWorkAreaName(div_code);
            if (dsTerritory.Tables[0].Rows.Count > 0)
            {
                e.Row.Cells[6].Text = dsTerritory.Tables[0].Rows[0]["wrk_area_Name"].ToString();
            }
        }
    }
    protected void grdChem_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            Territory terr = new Territory();
            DataSet dsTerritory = new DataSet();
            dsTerritory = terr.getWorkAreaName(div_code);
            if (dsTerritory.Tables[0].Rows.Count > 0)
            {
                e.Row.Cells[5].Text = dsTerritory.Tables[0].Rows[0]["wrk_area_Name"].ToString();
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
        string attachment = "attachment; filename=MRStatus.xls";
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
        string strFileName = "rptMRStatusView";
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