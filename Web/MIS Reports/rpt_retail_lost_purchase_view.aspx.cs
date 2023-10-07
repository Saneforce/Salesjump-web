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

public partial class rpt_retail_lost_purchase_view : System.Web.UI.Page
{
    string divcode = string.Empty;
    int FMonth = 0;
    int FYear = 0;
    string TMonth = string.Empty;
    string TYear = string.Empty;
    string Monthsub = string.Empty;
    string sCurrentDate = string.Empty;
    string tot_dr = string.Empty;
    string territory_code = string.Empty;
    string terriname_name = string.Empty;
    DataSet dsSalesForce = new DataSet();
    DataSet dsmgrsf = new DataSet();
    DataSet dsDoc = new DataSet();
    
string distributor_code=string.Empty;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        divcode = Session["div_code"].ToString();

        TMonth = Request.QueryString["FMonth"].ToString();
        TYear = Request.QueryString["FYear"].ToString();
        territory_code = Request.QueryString["Territory_Code"].ToString();
distributor_code=Request.QueryString["distributor_code"].ToString();
        terriname_name = Request.QueryString["Territory_Name"].ToString();
        FMonth = Convert.ToInt32(TMonth);
        FYear = Convert.ToInt32(TYear);
        System.Globalization.DateTimeFormatInfo mfi = new System.Globalization.DateTimeFormatInfo();
        string strFMonthName = mfi.GetMonthName(Convert.ToInt16(FMonth)).ToString().Substring(0, 3);
        string strTMonthName = mfi.GetMonthName(Convert.ToInt16(TMonth)).ToString().Substring(0, 3);

        lblHead.Text = "Lost-Retailer View for  " + strFMonthName + " " + FYear + " " + "To" + " " + strTMonthName + " " + TYear;


        lblIDMonth.Visible = false;
        lblIDYear.Visible = false;
        FillSF();

    }

    private void FillSF()
    {
        string sURL = string.Empty;
        string stURL = string.Empty;

        tbl.Rows.Clear();

        SalesForce sf = new SalesForce();
        dsSalesForce = sf.retail_lostretail_non_available_retailer_view(distributor_code,divcode,territory_code,FMonth, FYear);


        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            TableRow tr_header = new TableRow();
            tr_header.BorderStyle = BorderStyle.Solid;
            tr_header.BorderWidth = 1;
            tr_header.BackColor = System.Drawing.Color.FromName("#0097AC");
            tr_header.Style.Add("Color", "White");
            tr_header.BorderColor = System.Drawing.Color.Black;

            TableCell tc_SNo = new TableCell();
            tc_SNo.BorderStyle = BorderStyle.Solid;
            tc_SNo.BorderWidth = 1;
            tc_SNo.Width = 50;
            tc_SNo.RowSpan = 1;
            Literal lit_SNo =
                new Literal();
            lit_SNo.Text = "S.No";
            tc_SNo.BorderColor = System.Drawing.Color.Black;
            tc_SNo.Controls.Add(lit_SNo);
            tc_SNo.Attributes.Add("Class", "rptCellBorder");
            tr_header.Cells.Add(tc_SNo);

            TableCell tc_DR_Code = new TableCell();
            tc_DR_Code.BorderStyle = BorderStyle.Solid;
            tc_DR_Code.BorderWidth = 1;
            tc_DR_Code.Width = 400;
            tc_DR_Code.RowSpan = 1;
            Literal lit_DR_Code = new Literal();
            lit_DR_Code.Text = "<center>Retailer Name</center>";
            tc_DR_Code.Controls.Add(lit_DR_Code);
            tc_DR_Code.Attributes.Add("Class", "rptCellBorder");
            tc_DR_Code.BorderColor = System.Drawing.Color.Black;
            tr_header.Cells.Add(tc_DR_Code);

            TableCell tc_DR_Name = new TableCell();
            tc_DR_Name.BorderStyle = BorderStyle.Solid;
            tc_DR_Name.BorderWidth = 1;
            tc_DR_Name.Width = 250;
            tc_DR_Name.RowSpan = 1;
            Literal lit_DR_Name = new Literal();
            lit_DR_Name.Text = "<center>Last Order Date</center>";
            tc_DR_Name.BorderColor = System.Drawing.Color.Black;
            tc_DR_Name.Attributes.Add("Class", "rptCellBorder");
            tc_DR_Name.Controls.Add(lit_DR_Name);
            tr_header.Cells.Add(tc_DR_Name);





            tbl.Rows.Add(tr_header);
            //Sub Header



            if (dsSalesForce.Tables[0].Rows.Count > 0)
                ViewState["dsSalesForce"] = dsSalesForce;


            int iCount = 0;
            //string iTotLstCount ="0";
            dsSalesForce = (DataSet)ViewState["dsSalesForce"];

            foreach (DataRow drFF in dsSalesForce.Tables[0].Rows)
            {
                TableRow tr_det = new TableRow();
                iCount += 1;


                //S.No
                TableCell tc_det_SNo = new TableCell();
                Literal lit_det_SNo = new Literal();
                lit_det_SNo.Text = "<center>" + iCount.ToString() + "</center>";
                tc_det_SNo.BorderStyle = BorderStyle.Solid;
                tc_det_SNo.BorderWidth = 1;
                tc_det_SNo.Attributes.Add("Class", "rptCellBorder");
                tc_det_SNo.Controls.Add(lit_det_SNo);
                tr_det.Cells.Add(tc_det_SNo);
                tr_det.BackColor = System.Drawing.Color.White;

                //SF_code
                TableCell tc_det_usr = new TableCell();
                Literal lit_det_usr = new Literal();
                lit_det_usr.Text = "&nbsp;" + drFF["ListedDr_Name"].ToString();
                tc_det_usr.BorderStyle = BorderStyle.Solid;
                tc_det_usr.BorderWidth = 1;
                tc_det_usr.Attributes.Add("Class", "rptCellBorder");
                tc_det_usr.Controls.Add(lit_det_usr);
                tr_det.Cells.Add(tc_det_usr);
                TableCell code = new TableCell();
                code.Visible = false;
                Literal codee = new Literal();
                code.Text = "&nbsp;" + drFF["ListedDrCode"].ToString();
                code.BorderStyle = BorderStyle.Solid;
                code.BorderWidth = 1;
                code.Attributes.Add("Class", "rptCellBorder");
                code.Controls.Add(codee);
                tr_det.Cells.Add(code);



                dsDoc = sf.retail_lostretail_non_available_retailerlastdate(distributor_code, divcode, territory_code,FMonth, FYear, drFF["ListedDrCode"].ToString());

                        if (dsDoc.Tables[0].Rows.Count > 0)
                            tot_dr = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();


                
                //SF Name
                TableCell tc_det_FF = new TableCell();
                tc_det_FF.HorizontalAlign = HorizontalAlign.Center;
                tc_det_FF.Width = 200;
                HyperLink lit_det_FF = new HyperLink();
                //lit_det_FF.Text = "&nbsp;" + drFF["ListedDrCode"].ToString();
                lit_det_FF.Text = tot_dr;
                tc_det_FF.BorderStyle = BorderStyle.Solid;
                tc_det_FF.BorderWidth = 1;
                tc_det_FF.Attributes.Add("Class", "rptCellBorder");
                tc_det_FF.Controls.Add(lit_det_FF);
                tr_det.Cells.Add(tc_det_FF);



                tbl.Rows.Add(tr_det);

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

    protected void btnClose_Click(object sender, EventArgs e)
    {

    }
}