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


public partial class MIS_Reports_rptProduct_Exp_specat1 : System.Web.UI.Page
{
    DataSet dsProduct = null;
    DataSet dsDr = null;
    DataSet dsVisit = null;
    string sCmd = string.Empty;
    string div_code = string.Empty;
    string ProdBrdCode = string.Empty;
    string ProdCode = string.Empty;
    string ProdSaleUnit = string.Empty;
    string ProdName = string.Empty;
    string val = string.Empty;
    string search = string.Empty;
    string sf_code = string.Empty;
    string Year = string.Empty;
    string Month = string.Empty;
    string Prod_Name = string.Empty;
    string sf_name = string.Empty;
    string Prod = string.Empty;
    string sCurrentDate = string.Empty;
    string Sf_Code_multiple = string.Empty;
    int iCount = -1;
    string Doc_SpeCat_Code = string.Empty;
    string Type = string.Empty;
    string sReturn = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        sf_code = Request.QueryString["sf_code"];
        Year = Request.QueryString["Year"];
        Month = Request.QueryString["Month"];
        Prod_Name = Request.QueryString["Prod_Name"];
        Prod = Request.QueryString["Prod"];
        sf_name = Request.QueryString["sf_name"];
        sCurrentDate = Request.QueryString["sCurrentDate"];
        Doc_SpeCat_Code = Request.QueryString["Doc_Special_Code"];
        Type = Request.QueryString["Type"];


        System.Globalization.DateTimeFormatInfo mfi = new System.Globalization.DateTimeFormatInfo();
        string strMonthName = mfi.GetMonthName(Convert.ToInt16(Month)).ToString().Substring(0, 3);
        sf_name = sf_name.TrimStart(',');

        if (!Page.IsPostBack)
        {
            FillDr();
            //CreateDynamicTable();

            lblProd.Text = "Product Exposure for" + "<span style='color:red'> " + "(" + Prod_Name + ")" + "</span>" + "<br />" + " Listed Doctor Details for the Month of " + "" + strMonthName + "" + " " + Year + "";
            lblProd.Font.Bold = true;
        }

    }
    private void FillDr()
    {
        if (Type == "0")
        {
            Doctor dr = new Doctor();
            if (sf_code != "0")
            {
                lblname.Text = sf_name;
                dsDr = dr.getDr_Pro_Exp_SpeCat(div_code, sf_code, Convert.ToInt16(Year), Convert.ToInt16(Month), Convert.ToInt16(Prod), sCurrentDate, Convert.ToInt16(Doc_SpeCat_Code));
            }
            else
            {
                Sf_Code_multiple = Session["Sf_Code_multiple"].ToString();
                lblfieldname.Visible = false;
                dsDr = dr.getDr_Pro_Expall_SpeCat(div_code, Sf_Code_multiple, Convert.ToInt16(Year), Convert.ToInt16(Month), Convert.ToInt16(Prod), sCurrentDate, Convert.ToInt16(Doc_SpeCat_Code));
            }
            if (dsDr.Tables[0].Rows.Count > 0)
            {
                grdDr.Visible = true;
                grdDr.DataSource = dsDr;
                grdDr.DataBind();
            }
            else
            {
                grdDr.DataSource = dsDr;
                grdDr.DataBind();
            }
        }
        else if (Type == "1")
        {
            Doctor dr = new Doctor();
            if (sf_code != "0")
            {
                lblname.Text = sf_name;
                dsDr = dr.getDr_Pro_Exp_categ(div_code, sf_code, Convert.ToInt16(Year), Convert.ToInt16(Month), Convert.ToInt16(Prod), sCurrentDate, Convert.ToInt16(Doc_SpeCat_Code));
            }
            else
            {
                Sf_Code_multiple = Session["Sf_Code_multiple"].ToString();
                lblfieldname.Visible = false;
                dsDr = dr.getDr_Pro_Expall_categ(div_code, Sf_Code_multiple, Convert.ToInt16(Year), Convert.ToInt16(Month), Convert.ToInt16(Prod), sCurrentDate, Convert.ToInt16(Doc_SpeCat_Code));
            }
            if (dsDr.Tables[0].Rows.Count > 0)
            {
                grdDr.Visible = true;
                grdDr.DataSource = dsDr;
                grdDr.DataBind();
            }
            else
            {
                grdDr.DataSource = dsDr;
                grdDr.DataBind();
            }
        }
    }

    protected void btnClose_Click(object sender, EventArgs e)
    {

    }
    protected void grdDr_RowDataBound(object sender, GridViewRowEventArgs e)
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

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblDRCode = (Label)e.Row.FindControl("lblDrCode");
            Label lblVisitCount = (Label)e.Row.FindControl("lblVisitCount");
            Label lblVisitDate = (Label)e.Row.FindControl("lblVisitDate");

            DCR dc = new DCR();
            dsVisit = dc.Visit_Doc(lblDRCode.Text.Trim(), Convert.ToInt16(Month), Convert.ToInt16(Year));
            if (dsVisit.Tables[0].Rows.Count > 0)
            {
                string[] visit;
                int i = 0;
                sReturn = dsVisit.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                visit = sReturn.Trim().Split('~');
                foreach (string sInd in visit)
                {
                    if (i == 0)
                        lblVisitDate.Text = sInd.Substring(0, sInd.Length - 1);
                    else
                        lblVisitCount.Text = sInd;

                    i = i + 1;
                }
            }

        }
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
        pnlContents.Parent.Controls.Add(frm);
        frm.Attributes["runat"] = "server";
        frm.Controls.Add(pnlContents);
        frm.RenderControl(htw);
        Response.Write(sw.ToString());
        Response.End();
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

}

