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

public partial class MIS_Reports_rptsampleproduct_details1 : System.Web.UI.Page
{
    DataSet dsProduct = null;
    DataSet dsDr = null;
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
    string MultiProd_Code = string.Empty;
    string tot_dr = string.Empty;
    int iCount = -1;
    DataSet dsDoc = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        sf_code = Request.QueryString["sf_code"];
        Year = Request.QueryString["Year"];
        Month = Request.QueryString["Month"];
        Prod_Name = Request.QueryString["Prod_Name"];
        Prod = Request.QueryString["Product_Code_SlNo"];
        sf_name = Request.QueryString["sf_name"];
        sCurrentDate = Request.QueryString["sCurrentDate"];
        //MultiProd_Code = Session["MultiProd_Code"].ToString();

        System.Globalization.DateTimeFormatInfo mfi = new System.Globalization.DateTimeFormatInfo();
        string strMonthName = mfi.GetMonthName(Convert.ToInt16(Month)).ToString().Substring(0, 3);
        sf_name = sf_name.TrimStart(',');


        if (!Page.IsPostBack)
        {
            //FillProd();
            lblProd.Text = "Sample Details for the Period of " + strMonthName + " " + Year + " ";
            lblProd.Font.Bold = true;
            lblname.Text = sf_name;
            CreateDynamicTable();
        }

    }

    //private void FillProd()
    //{
    //    int tot = 0;
    //    Doctor dr = new Doctor();
    //    //string[] Product_Code;

    //    //Product_Code = MultiProd_Code.Split(',');

    //    //foreach (string prd in Product_Code)
    //    //{
    //        dsDr = dr.getDr_Pro_Sample(div_code, sf_code, Convert.ToInt16(Year), Convert.ToInt16(Month),sCurrentDate);

    //        if (dsDr.Tables[0].Rows.Count > 0)
    //        {
    //            grdDr.Visible = true;
    //            grdDr.DataSource = dsDr;
    //            grdDr.DataBind();
    //        }
    //        else
    //        {
    //            grdDr.DataSource = dsDr;
    //            grdDr.DataBind();
    //        }
    //    //}

    //}
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
        pnlContents.Parent.Controls.Add(frm);
        frm.Attributes["runat"] = "server";
        frm.Controls.Add(pnlContents);
        frm.RenderControl(htw);
        Response.Write(sw.ToString());
        Response.End();
    }
    protected void btnClose_Click(object sender, EventArgs e)
    {

    }

    private void CreateDynamicTable()
    {
        //TableRow tr_day = new TableRow();
        //TableCell tc_day = new TableCell();
        //tc_day.BorderStyle = BorderStyle.None;
        //tc_day.ColumnSpan = 2;

        //tc_day.HorizontalAlign = HorizontalAlign.Center;
        //Literal lit_day = new Literal();
        //// lit_day.Text = "<u>Daily Work Report on " + StrDate + "</u>";
        //tc_day.Controls.Add(lit_day);
        //tr_day.Cells.Add(tc_day);
        //tbl.Rows.Add(tr_day);

        //TableRow tr_ff = new TableRow();


        //tbl.Rows.Add(tr_ff);


        //TableRow tr_det = new TableRow();
        //TableCell tc_det = new TableCell();
        //tc_det.BorderStyle = BorderStyle.None;
        //tc_det.Width = 400;
        //tc_det.ColumnSpan = 2;

        //Table tbldetail = new Table();
        //tbldetail.BorderStyle = BorderStyle.Solid;
        //tbldetail.BorderWidth = 1;
        //tbldetail.Style.Add("border-collapse", "collapse");
        //tbldetail.Style.Add("border", "solid 1px Black");
        //tbldetail.GridLines = GridLines.Both;
        //tbldetail.Width = 1000;


        int tot = 0;
        Doctor dr = new Doctor();
        //string[] Product_Code;

        //Product_Code = MultiProd_Code.Split(',');

        //foreach (string prd in Product_Code)
        //{
            dsDr = dr.getDr_Pro_Sample(div_code, sf_code, Convert.ToInt16(Year), Convert.ToInt16(Month),sCurrentDate);

            
            if (dsDr.Tables[0].Rows.Count > 0)
            {
                TableRow tr_det_head = new TableRow();
                tr_det_head.BorderStyle = BorderStyle.Solid;
                tr_det_head.BorderWidth = 1;
                tr_det_head.BackColor = System.Drawing.Color.FromName("#0097AC");
                tr_det_head.Style.Add("Color", "White");
                tr_det_head.BorderColor = System.Drawing.Color.Black;

                TableCell tc_det_head_SNo = new TableCell();
                tc_det_head_SNo.BorderStyle = BorderStyle.Solid;
                tc_det_head_SNo.BorderWidth = 1;
                Literal lit_det_head_SNo = new Literal();
                lit_det_head_SNo.Text = "<b>S.No</b>";
                tc_det_head_SNo.Attributes.Add("Class", "tblHead");
                tc_det_head_SNo.Controls.Add(lit_det_head_SNo);
                tc_det_head_SNo.HorizontalAlign = HorizontalAlign.Center;
                tr_det_head.Cells.Add(tc_det_head_SNo);


                TableCell tc_det_head_doc = new TableCell();
                tc_det_head_doc.BorderStyle = BorderStyle.Solid;
                tc_det_head_doc.BorderWidth = 1;
                Literal lit_det_head_doc = new Literal();
                lit_det_head_doc.Text = "<b>Fieldforce Name</b>";
                tc_det_head_doc.Attributes.Add("Class", "tblHead");
                tc_det_head_doc.Controls.Add(lit_det_head_doc);
                tc_det_head_doc.HorizontalAlign = HorizontalAlign.Center;
                tr_det_head.Cells.Add(tc_det_head_doc);

                TableCell tc_det_head_hq = new TableCell();
                tc_det_head_hq.BorderStyle = BorderStyle.Solid;
                tc_det_head_hq.BorderWidth = 1;
                Literal lit_det_head_hq = new Literal();
                lit_det_head_hq.Text = "<b>HQ</b>";
                tc_det_head_hq.Attributes.Add("Class", "tblHead");
                tc_det_head_hq.Controls.Add(lit_det_head_hq);
                tc_det_head_hq.HorizontalAlign = HorizontalAlign.Center;
                tr_det_head.Cells.Add(tc_det_head_hq);

                TableCell tc_det_head_Qua = new TableCell();
                tc_det_head_Qua.BorderStyle = BorderStyle.Solid;
                tc_det_head_Qua.BorderWidth = 1;
                Literal lit_det_head_qua = new Literal();
                lit_det_head_qua.Text = "<b>Product Name</b>";
                tc_det_head_Qua.Attributes.Add("Class", "tblHead");
                tc_det_head_Qua.Controls.Add(lit_det_head_qua);
                tc_det_head_Qua.HorizontalAlign = HorizontalAlign.Center;
                tr_det_head.Cells.Add(tc_det_head_Qua);

                TableCell tc_det_head_Territory = new TableCell();
                tc_det_head_Territory.BorderStyle = BorderStyle.Solid;
                tc_det_head_Territory.BorderWidth = 1;
                Literal lit_det_head_Territory = new Literal();
                lit_det_head_Territory.Text = "<b>Sample</b>";
                tc_det_head_Territory.Attributes.Add("Class", "tblHead");
                tc_det_head_Territory.Controls.Add(lit_det_head_Territory);
                tc_det_head_Territory.HorizontalAlign = HorizontalAlign.Center;
                tr_det_head.Cells.Add(tc_det_head_Territory);



                tbl.Rows.Add(tr_det_head);

                iCount = 0;
                foreach (DataRow drdoctor in dsDr.Tables[0].Rows)
                {
                    TableRow tr_det_sno = new TableRow();
                    TableCell tc_det_SNo = new TableCell();
                    iCount += 1;
                    Literal lit_det_SNo = new Literal();
                    lit_det_SNo.Text = "<center>" + iCount.ToString() + "</center>";
                    tc_det_SNo.Attributes.Add("Class", "tblRow");
                    tc_det_SNo.BorderStyle = BorderStyle.Solid;
                    tc_det_SNo.BorderWidth = 1;
                    tc_det_SNo.Controls.Add(lit_det_SNo);
                    tr_det_sno.Cells.Add(tc_det_SNo);

                    TableCell tc_det_usr = new TableCell();
                    Literal lit_det_usr = new Literal();
                    lit_det_usr.Text = "&nbsp;" + drdoctor["Product_Code_SlNo"].ToString();
                    tc_det_usr.BorderStyle = BorderStyle.Solid;
                    tc_det_usr.BorderWidth = 1;
                    tc_det_usr.Visible = false;
                    tc_det_usr.Attributes.Add("Class", "rptCellBorder");
                    tc_det_usr.Controls.Add(lit_det_usr);
                    tr_det_sno.Cells.Add(tc_det_usr);

                    TableCell tc_det_sfcode = new TableCell();
                    Literal lit_det_sfcode = new Literal();
                    lit_det_sfcode.Text = "&nbsp;" + drdoctor["sf_code"].ToString();
                    tc_det_sfcode.BorderStyle = BorderStyle.Solid;
                    tc_det_sfcode.BorderWidth = 1;
                    tc_det_sfcode.Visible = false;
                    tc_det_sfcode.Attributes.Add("Class", "rptCellBorder");
                    tc_det_sfcode.Controls.Add(lit_det_sfcode);
                    tr_det_sno.Cells.Add(tc_det_sfcode);


                    TableCell tc_det_dr_name = new TableCell();
                    Literal lit_det_dr_name = new Literal();
                    lit_det_dr_name.Text = drdoctor["Fieldforce_Name"].ToString();
                    tc_det_dr_name.Attributes.Add("Class", "tblRow");
                    tc_det_dr_name.BorderStyle = BorderStyle.Solid;
                    tc_det_dr_name.BorderWidth = 1;
                    tc_det_dr_name.Controls.Add(lit_det_dr_name);
                    tr_det_sno.Cells.Add(tc_det_dr_name);

                    TableCell tc_det_dr_hq = new TableCell();
                    Literal lit_det_dr_hq = new Literal();
                    lit_det_dr_hq.Text = drdoctor["sf_HQ"].ToString();
                    tc_det_dr_hq.Attributes.Add("Class", "tblRow");
                    tc_det_dr_hq.BorderStyle = BorderStyle.Solid;
                    tc_det_dr_hq.BorderWidth = 1;
                    tc_det_dr_hq.Controls.Add(lit_det_dr_hq);
                    tr_det_sno.Cells.Add(tc_det_dr_hq);

                    TableCell tc_det_dr_qua = new TableCell();
                    Literal lit_det_dr_qua = new Literal();
                    lit_det_dr_qua.Text = drdoctor["Product_Detail_Name"].ToString();
                    tc_det_dr_qua.Attributes.Add("Class", "tblRow");
                    tc_det_dr_qua.BorderStyle = BorderStyle.Solid;
                    tc_det_dr_qua.BorderWidth = 1;
                    tc_det_dr_qua.Controls.Add(lit_det_dr_qua);
                    tr_det_sno.Cells.Add(tc_det_dr_qua);

                    //TableCell tc_det_sample = new TableCell();
                    //Literal lit_det_sample = new Literal();

                    TableCell tc_lst_month = new TableCell();
                    HyperLink hyp_lst_month = new HyperLink();


                    if (dsDr.Tables[0].Rows.Count > 0)
                        tot_dr = dsDr.Tables[0].Rows[0].ItemArray.GetValue(4).ToString();

                    if (tot_dr != "0")
                    {
                        hyp_lst_month.Text = tot_dr;
                        //iTotLstCount +=Convert.ToInt16( tot_dr);

                        hyp_lst_month.Attributes.Add("href", "javascript:showModalPopUp('" + drdoctor["sf_code"].ToString() + "', '" + drdoctor["Fieldforce_Name"] + "', '" + Year + "', '" + Month + "','" + sCurrentDate + "','" + drdoctor["Product_Code_SlNo"] + "')");
                        //hyp_lst_month.Attributes["onclick"] = "javascript:showModalPopUp('" + sURL + "')";
                        hyp_lst_month.NavigateUrl = "#";

                    }

                    else
                    {
                        hyp_lst_month.Text = "";
                    }


                    tc_lst_month.BorderStyle = BorderStyle.Solid;
                    tc_lst_month.BorderWidth = 1;
                    hyp_lst_month.Text = drdoctor["Sample"].ToString();
                    tc_lst_month.BackColor = System.Drawing.Color.White;
                    tc_lst_month.Width = 200;
                    tc_lst_month.HorizontalAlign = HorizontalAlign.Center;
                    tc_lst_month.VerticalAlign = VerticalAlign.Middle;
                    tc_lst_month.Controls.Add(hyp_lst_month);
                    tc_lst_month.Attributes.Add("Class", "rptCellBorder");
                    tr_det_sno.Cells.Add(tc_lst_month);

                    //lit_det_sample.Text = drdoctor["Sample"].ToString();
                    //tc_det_sample.Attributes.Add("Class", "tblRow");
                    //tc_det_sample.BorderStyle = BorderStyle.Solid;
                    //tc_det_sample.BorderWidth = 1;
                    //tc_det_sample.Controls.Add(lit_det_sample);
                    //tc_det_sample.HorizontalAlign = HorizontalAlign.Center;
                    //tr_det_sno.Cells.Add(tc_det_sample);

                    tbl.Rows.Add(tr_det_sno);
                }

            }

            //tc_det.HorizontalAlign = HorizontalAlign.Center;
            //tc_day.Controls.Add(tbldetail);
            //tr_day.Cells.Add(tc_det);
            //tbl.Rows.Add(tr_det_head);
        }
    }
//}