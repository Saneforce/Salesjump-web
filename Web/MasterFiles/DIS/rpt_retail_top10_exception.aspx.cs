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
using System.Configuration;
using System.Data.SqlClient;

public partial class MIS_Reports_rpt_retail_top10_exception : System.Web.UI.Page
{
    string sfCode = string.Empty;
    string sfname = string.Empty;
    string divcode = string.Empty;   
    string sMode = string.Empty;
    string FMonth = string.Empty;
    string FYear = string.Empty;
    string viewby = string.Empty;
    string viewtop = string.Empty;  
    DataSet dsSalesForce = new DataSet();
    DataSet dsmgrsf = new DataSet();  
    string tot_dr = string.Empty;   
    string Monthsub = string.Empty;
    string route = string.Empty;
    DataSet dsprd = new DataSet();   
    string sCurrentDate = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        divcode = Session["div_code"].ToString();


        FYear = Request.QueryString["FYear"].ToString();    
        viewtop = Request.QueryString["viewtop"].ToString();
        route = Request.QueryString["route"].ToString();


        System.Globalization.DateTimeFormatInfo mfi = new System.Globalization.DateTimeFormatInfo();



        lblIDMonth.Visible = false;
        lblIDYear.Visible = false;
       
            lblHead.Text = "Top  " + viewtop + "  Retail Exception Route Wise for   " + FYear;
            FillSF();
      
       
        
        

    }
   


    private void FillSF()
    {
        string sURL = string.Empty;
        string stURL = string.Empty;

        tbl.Rows.Clear();

        SalesForce sf = new SalesForce();
        dsSalesForce = sf.retail_Gettop10value_route(divcode, FYear, route);


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

         

            TableCell tc_DR_Name = new TableCell();
            tc_DR_Name.BorderStyle = BorderStyle.Solid;
            tc_DR_Name.BorderWidth = 1;
            tc_DR_Name.Width = 150;
            tc_DR_Name.RowSpan = 1;
            Literal lit_DR_Name = new Literal();
            lit_DR_Name.Text = "<center>Customer Name</center>";
            tc_DR_Name.BorderColor = System.Drawing.Color.Black;
            tc_DR_Name.Attributes.Add("Class", "rptCellBorder");
            tc_DR_Name.Controls.Add(lit_DR_Name);
            tr_header.Cells.Add(tc_DR_Name);


            TableCell tc_DR_value = new TableCell();
            tc_DR_value.BorderStyle = BorderStyle.Solid;
            tc_DR_value.BorderWidth = 1;
            tc_DR_value.Width = 150;
            tc_DR_value.RowSpan = 1;
            Literal lit_DR_value = new Literal();
            lit_DR_value.Text = "<center>Value</center>";
            tc_DR_value.BorderColor = System.Drawing.Color.Black;
            tc_DR_value.Attributes.Add("Class", "rptCellBorder");
            tc_DR_value.Controls.Add(lit_DR_value);
            tr_header.Cells.Add(tc_DR_value);

            tbl.Controls.Add(tr_header);

                       

            SalesForce sal = new SalesForce();


            if (dsSalesForce.Tables[0].Rows.Count > 0)
                ViewState["dsSalesForce"] = dsSalesForce;


            int iCount = 0;
            //string iTotLstCount ="0";
            dsSalesForce = (DataSet)ViewState["dsSalesForce"];
            StringBuilder output = new StringBuilder();
            foreach (DataRow drFF in dsSalesForce.Tables[0].Rows)
            {
                

                output.AppendLine();
                Response.Write(output);
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
                tc_det_usr.Width = 150;
                Literal lit_det_usr = new Literal();
                lit_det_usr.Text = "&nbsp;" + drFF["Cust_Code"].ToString();
                tc_det_usr.BorderStyle = BorderStyle.Solid;
                tc_det_usr.BorderWidth = 1;
                tc_det_usr.Visible = false;
                tc_det_usr.Attributes.Add("Class", "rptCellBorder");
                tc_det_usr.Controls.Add(lit_det_usr);
                tr_det.Cells.Add(tc_det_usr);

                //SF Name
                TableCell tc_det_FF = new TableCell();
                tc_det_FF.Width = 150;
                HyperLink lit_det_FF = new HyperLink();
                lit_det_FF.Text = "&nbsp;" + drFF["ListedDr_Name"].ToString();
                tc_det_FF.HorizontalAlign = HorizontalAlign.Center;
                tc_det_FF.BorderStyle = BorderStyle.Solid;
                tc_det_FF.BorderWidth = 1;
                tc_det_FF.Attributes.Add("Class", "rptCellBorder");
                tc_det_FF.Controls.Add(lit_det_FF);
                tr_det.Cells.Add(tc_det_FF);


              


                TableCell tc_lst_month = new TableCell();
                Literal hyp_lst_month = new Literal();
                tc_lst_month.Width = 150;
                hyp_lst_month.Text = "&nbsp;" + drFF["value"].ToString();
                tc_lst_month.BorderStyle = BorderStyle.Solid;
                tc_lst_month.BorderWidth = 1;
                tc_lst_month.BackColor = System.Drawing.Color.White;
                tc_lst_month.Width = 200;
                tc_lst_month.HorizontalAlign = HorizontalAlign.Center;
                tc_lst_month.VerticalAlign = VerticalAlign.Middle;
                tc_lst_month.Controls.Add(hyp_lst_month);
                tc_lst_month.Attributes.Add("Class", "rptCellBorder");
                tr_det.Cells.Add(tc_lst_month);


                if (iCount <= int.Parse(viewtop))
                {


                    tc_det_SNo.BackColor = System.Drawing.Color.Beige;
                    tc_lst_month.BackColor = System.Drawing.Color.Beige;
                    tc_det_FF.BackColor = System.Drawing.Color.Beige;
                    tc_det_usr.BackColor = System.Drawing.Color.Beige;
                }
                tbl.Controls.Add(tr_det);




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
        string attachment = "attachment; filename=DCRView.xls";
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
    protected void Button1_Click(object sender, EventArgs e)
    {
        Response.Redirect("viewreort.aspx");
    }
}