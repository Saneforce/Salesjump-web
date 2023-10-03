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

public partial class MIS_Reports_rpt_Order_Detail_View1 : System.Web.UI.Page
{
     int iTotLstCount1 = 0;
     int iTotLstCount = 0;  
    int iTotLstCount2 = 0;   
    DataSet dsProduct = null;
    DataSet dsDr = null;
    DataSet dsDrr = null;
    DataSet getweek = null;
    DateTime dtCurrent;
    string sCmd = string.Empty;
    string div_code = string.Empty;
    string ProdBrdCode = string.Empty;
    string ProdCode = string.Empty;
    string ProdSaleUnit = string.Empty;
    string ProdName = string.Empty;
    string val = string.Empty;
    string distributor = string.Empty;
    string stockist = string.Empty;
    string Year = string.Empty;
    string Month = string.Empty;
    string Stock_name = string.Empty;
    string sf_name = string.Empty;
    string type = string.Empty;
    string sCurrentDate = string.Empty;
    string Sf_Code_multiple = string.Empty;
    string Territory_Name = string.Empty;
    string Retailer = string.Empty;
    string ORDER_DATE = string.Empty;
    string order_value=string.Empty;
    string order_no = string.Empty;
    string tot_dr = string.Empty;
    string strFMonthName = string.Empty;
    string dcr_code = string.Empty;
    int iCount = -1;
    DataSet dsDoc = null;
    
    int FYear=0; 
    int FMonth=0 ;

    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["Division_Code"].ToString().Replace(",", "");    
        Month = Request.QueryString["FMonth"].ToString();
        Year = Request.QueryString["FYear"].ToString();       
        FMonth = Convert.ToInt32(Month);
        FYear  = Convert.ToInt32(Year);
        distributor = Request.QueryString["Distributor"].ToString();
        Territory_Name = Request.QueryString["Territory_Name"].ToString();
        Retailer = Request.QueryString["ListedDr_Name"].ToString();
        ORDER_DATE = Request.QueryString["ORDER_DATE"].ToString();
        order_value = Request.QueryString["Order_Value"].ToString();
        dcr_code = Request.QueryString["DCR_code"].ToString();
        lblRegionName.Text = sf_name;
        System.Globalization.DateTimeFormatInfo mfi = new System.Globalization.DateTimeFormatInfo();
        strFMonthName = mfi.GetMonthName(Convert.ToInt16(Month)).ToString();


       lblHead.Text = "Order Details - View for the Month of " + strFMonthName + " " + FYear;
        Lbl_Route_name.Text =" "+" Route Name: " +" "+Territory_Name+" ";
        Lbl_Retailer.Text =" "+" Retailer Name: " +" "+ Retailer + " ";
        Lbl_Date.Text = " " + "Order Date :" + " " + ORDER_DATE + " ";
        lbl_order_no.Text = " " + "Order No :" + " "+ "  " + " ";
        //lblIDMonth.Visible = false;
        //lblIDYear.Visible = false;
       
        
        FillSF_Leave_Type();
       

    }
    private void FillSF_Leave_Type()
    {
        string sURL = string.Empty;
        string stURL = string.Empty;
        string stCrtDtaPnt = string.Empty;
        SalesForce dr1 = new SalesForce();
        dsDrr = dr1.Gettransno(distributor, dcr_code);

        foreach (DataRow d in dsDrr.Tables[0].Rows)
        {
            order_no = d["Trans_Sl_No"].ToString();



            int tot = 0;
            SalesForce dr = new SalesForce();

            dsDr = dr.Getorder(order_no);


            if (dsDr.Tables[0].Rows.Count > 0)
            {
                TableRow tr_det = new TableRow();


                TableRow tr_det_head = new TableRow();
                tr_det_head.BorderStyle = BorderStyle.Dotted;
                tr_det_head.BorderWidth = 1;
                tr_det_head.BackColor = System.Drawing.Color.FromName("rgba(128, 128, 128, 0.36)");
                tr_det_head.Style.Add("Color", "black");
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




                TableCell tc_det_head_Qua = new TableCell();
                tc_det_head_Qua.BorderStyle = BorderStyle.Solid;
                tc_det_head_Qua.BorderWidth = 1;
                Literal lit_det_head_qua = new Literal();
                lit_det_head_qua.Text = "<b>Product Name</b>";
                tc_det_head_Qua.Attributes.Add("Class", "tblHead");
                tc_det_head_Qua.Controls.Add(lit_det_head_qua);
                tc_det_head_Qua.HorizontalAlign = HorizontalAlign.Center;
                tr_det_head.Cells.Add(tc_det_head_Qua);

                TableCell tc_det_head_Quai = new TableCell();
                tc_det_head_Quai.BorderStyle = BorderStyle.Solid;
                tc_det_head_Quai.BorderWidth = 1;
                Literal lit_det_head_quai = new Literal();
                lit_det_head_quai.Text = "<b>Quantity</b>";
                tc_det_head_Quai.Attributes.Add("Class", "tblHead");
                tc_det_head_Quai.Controls.Add(lit_det_head_quai);
                tc_det_head_Quai.HorizontalAlign = HorizontalAlign.Center;
                tr_det_head.Cells.Add(tc_det_head_Quai);

                TableCell tc_det_head_min = new TableCell();
                tc_det_head_min.BorderStyle = BorderStyle.Solid;
                tc_det_head_min.BorderWidth = 1;
                Literal lit_det_head_min = new Literal();
                lit_det_head_min.Text = "<b>value</b>";
                tc_det_head_min.Attributes.Add("Class", "tblHead");
                tc_det_head_min.Controls.Add(lit_det_head_min);
                tc_det_head_min.HorizontalAlign = HorizontalAlign.Center;
                tr_det_head.Cells.Add(tc_det_head_min);



                tbl.Rows.Add(tr_det_head);



                int iCount = 0;
                foreach (DataRow drdoctor in dsDr.Tables[0].Rows)
                {
                    TableRow tr_det_sno = new TableRow();
                    tr_det_sno.BackColor = System.Drawing.Color.FromName("rgba(209, 209, 209, 0.30)");
                    TableCell tc_det_SNo = new TableCell();
                    iCount += 1;
                    Literal lit_det_SNo = new Literal();
                    lit_det_SNo.Text = "<center>" + iCount.ToString() + "</center>";
                    tc_det_SNo.Attributes.Add("Class", "tblRow");
                    tc_det_SNo.BorderStyle = BorderStyle.Solid;
                    tc_det_SNo.BorderWidth = 1;
                    tc_det_SNo.Controls.Add(lit_det_SNo);
                    tr_det_sno.Cells.Add(tc_det_SNo);

                    //TableCell tc_det_usr = new TableCell();
                    //Literal lit_det_usr = new Literal();
                    //lit_det_usr.Text = "&nbsp;" + drdoctor["Trans_Order_No"].ToString();
                    //stCrtDtaPnt += "{label:\"" + drdoctor["Trans_Order_No"].ToString() + "\",y:";
                    //tc_det_usr.BorderStyle = BorderStyle.Solid;
                    //tc_det_usr.BorderWidth = 1;
                    //tc_det_usr.Attributes.Add("Class", "rptCellBorder");
                    //tc_det_usr.Controls.Add(lit_det_usr);
                    //tr_det_sno.Cells.Add(tc_det_usr);


                    TableCell tc_det_dr_name = new TableCell();
                    Literal lit_det_dr_name = new Literal();
                    lit_det_dr_name.Text = "<center>" + drdoctor["Product_Name"].ToString() + "<center>";
                    tc_det_dr_name.Attributes.Add("Class", "tblRow");
                    tc_det_dr_name.BorderStyle = BorderStyle.Solid;
                    tc_det_dr_name.BorderWidth = 1;
                    tc_det_dr_name.Controls.Add(lit_det_dr_name);
                    tr_det_sno.Cells.Add(tc_det_dr_name);

                    TableCell tc_det_dr_hq = new TableCell();
                    Literal lit_det_dr_hq = new Literal();
                    lit_det_dr_hq.Text = "<center>" + drdoctor["Quantity"].ToString() + "<center>";
                    tc_det_dr_hq.Attributes.Add("Class", "tblRow");
                    tc_det_dr_hq.BorderStyle = BorderStyle.Solid;
                    tc_det_dr_hq.BorderWidth = 1;
                    tc_det_dr_hq.Controls.Add(lit_det_dr_hq);
                    tr_det_sno.Cells.Add(tc_det_dr_hq);

                    TableCell tc_det_FF = new TableCell();
                    tc_det_FF.Width = 250;
                    HyperLink lit_det_FF = new HyperLink();
                    lit_det_FF.Text = "<center>" + drdoctor["value"].ToString() + "<center>";
                    stCrtDtaPnt += "{label:\"" + drdoctor["value"].ToString() + "\",y:";

                    tc_det_FF.BorderStyle = BorderStyle.Solid;
                    tc_det_FF.BorderWidth = 1;
                    tc_det_FF.Attributes.Add("Class", "rptCellBorder");
                    tc_det_FF.Controls.Add(lit_det_FF);
                    tr_det_sno.Cells.Add(tc_det_FF);

                    //TableCell tc_det_dr_min = new TableCell();
                    //Literal lit_det_dr_min = new Literal();
                    //lit_det_dr_min.Text = drdoctor["Order_Value"].ToString();
                    //tc_det_dr_min.Attributes.Add("Class", "tblRow");
                    //tc_det_dr_min.BorderStyle = BorderStyle.Solid;
                    //tc_det_dr_min.BorderWidth = 1;
                    //tc_det_dr_min.Controls.Add(lit_det_dr_min);
                    //tr_det_sno.Cells.Add(tc_det_dr_min);




                    tbl.Rows.Add(tr_det_sno);


                }
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
}