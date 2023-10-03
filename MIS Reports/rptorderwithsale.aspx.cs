using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
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
using iTextSharp.tool.xml;
using System.Web.UI.HtmlControls;


public partial class MIS_Reports_rptorderwithsale : System.Web.UI.Page
{
    decimal iTotLstCount1 = 0;
    decimal iTotLstCount2 = 0;
    DataSet dsDoc = null;
    DataSet getweek = null;
    DataSet dsDr = null;
    string sCmd = string.Empty;
    string div_code = string.Empty;
    string ProdBrdCode = string.Empty;
    string ProdCode = string.Empty;
    string ProdSaleUnit = string.Empty;
    string ProdName = string.Empty;
    string strFMonthName = string.Empty;
    string statecode = string.Empty;
    string Year = string.Empty;
    string Month = string.Empty;
     int FYear = 0;
    int FMonth =0;
    string State_name = string.Empty;
    string sf_name = string.Empty;
    string sfcode = string.Empty;
    string sCurrentDate = string.Empty;    
    string tot_dr = string.Empty;
    string tot_drr = string.Empty;
    int iCount = -1;
string subdivision=string.Empty;
    DataSet dsDrr = null;
    string sURL = string.Empty;
 string dtype=string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();    
        Year = Request.QueryString["year"];
        Month = Request.QueryString["month"];
        sfcode = Request.QueryString["sfcode"].ToString();
          sf_name= Request.QueryString["sfname"].ToString();
      subdivision= Request.QueryString["subdivision"].ToString();

   FMonth = Convert.ToInt32(Month);
        FYear = Convert.ToInt32(Year);
         System.Globalization.DateTimeFormatInfo mfi = new System.Globalization.DateTimeFormatInfo();
        strFMonthName = mfi.GetMonthName(Convert.ToInt16(Month)).ToString();
        //sf_name = sf_name.TrimStart(',');
         

            CreateDynamicTable();
        
       
        if (!Page.IsPostBack)
        {
           
            lblProd.Text = "Order With Sale Report for " + strFMonthName + " " + Year + " ";
            lblProd.Font.Bold = true;
            lblProd.Font.Underline = true;
            lblname.Text = sf_name;
          
           
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
                Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
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

     private void CreateDynamicTable()
    {
        string stCrtDtaPnt = string.Empty;
        string stCrtDtaPnt1 = string.Empty;
//        int df = Convert.ToInt32(getweek.Tables[0].Rows[0].ItemArray.GetValue(0));

//        int wee = df + 1;

//        if (getweek.Tables[0].Rows.Count > 0)
//        {
//            for (int i = 1; i < wee; i++)
//            {
//                TableCell tc_catg_namee = new TableCell();
//                tc_catg_namee.BorderStyle = BorderStyle.Solid;
//                tc_catg_namee.BorderWidth = 1;
//                //text-align: center;

//                tc_catg_namee.ColumnSpan = 2;
//                HyperLink lit_catg_namee = new HyperLink();
//                lit_catg_namee.Text = " Week-" + i.ToString();
//dtype="value";

//                //sURL = "rpt_state_daywise.aspx?weekno=" + i + " &FYear=" + FYear + "&FMonth=" + FMonth + "&State_Code=" + statecode + "&State_Name=" + State_name + "&Subdivision=" + subdivision + "&Dtype="+ dtype +"";
//                //lit_catg_namee.Attributes["onclick"] = "javascript:window.open('" + sURL + "' ,'','resizable=yes,toolbar=no,menubar=no,scrollbars=1,status=no,width=500,height=600,left=0,top=0,zoom=50%');";
//                //lit_catg_namee.Style.Add("Color", "Blue");

//                lit_catg_namee.NavigateUrl = "#";
//                tc_catg_namee.Attributes.Add("Class", "rptCellBorder");
//                tc_catg_namee.HorizontalAlign = HorizontalAlign.Center;
//                tc_catg_namee.Controls.Add(lit_catg_namee);
//                tr_det_head.Cells.Add(tc_catg_namee);
//            }


//        }
        //tbl.Rows.Add(tr_det_head);



        SalesForce dr = new SalesForce();
        dsDr = dr.Secondary_sales_productdetail(div_code,subdivision);


        if (dsDr.Tables[0].Rows.Count > 0)
        {
            TableRow tr_catg = new TableRow();
            tr_catg.BackColor = System.Drawing.Color.FromName("#0097AC");
            tr_catg.Style.Add("Color", "White");



            TableCell tc_det_head_SNo = new TableCell();
            tc_det_head_SNo.BorderStyle = BorderStyle.Solid;
            tc_det_head_SNo.BorderWidth = 1;
            tc_det_head_SNo.Width = 70;
            Literal lit_det_head_SNo = new Literal();
            lit_det_head_SNo.Text = "<b>S.No</b>";
            tc_det_head_SNo.Attributes.Add("Class", "tblHead");
            tc_det_head_SNo.Controls.Add(lit_det_head_SNo);
            tc_det_head_SNo.HorizontalAlign = HorizontalAlign.Center;
            tr_catg.Cells.Add(tc_det_head_SNo);


            TableCell tc_det_head_doc = new TableCell();
            tc_det_head_doc.Width = 120;
            tc_det_head_doc.BorderStyle = BorderStyle.Solid;
            tc_det_head_doc.BorderWidth = 1;
            Literal lit_det_head_doc = new Literal();
            lit_det_head_doc.Text = "<b>Product Name</b>";
            tc_det_head_doc.Attributes.Add("Class", "tblHead");
            tc_det_head_doc.Controls.Add(lit_det_head_doc);
            tc_det_head_doc.HorizontalAlign = HorizontalAlign.Center;
            tr_catg.Cells.Add(tc_det_head_doc);

            TableCell tc_det_head_hq = new TableCell();
            tc_det_head_hq.Width = 70;
            tc_det_head_hq.BorderStyle = BorderStyle.Solid;
            tc_det_head_hq.BorderWidth = 1;
            Literal lit_det_head_hq = new Literal();
            lit_det_head_hq.Text = "<b>UOM</b>";
            tc_det_head_hq.Attributes.Add("Class", "tblHead");
            tc_det_head_hq.Controls.Add(lit_det_head_hq);
            tc_det_head_hq.HorizontalAlign = HorizontalAlign.Center;
            tr_catg.Cells.Add(tc_det_head_hq);
            //if (getweek.Tables[0].Rows.Count > 0)
            //{

                //for (int i = 1; i < wee; i++)
                //{

                    TableCell tc_det_head_OP = new TableCell();
                    tc_det_head_OP.BorderStyle = BorderStyle.Solid;
                    tc_det_head_OP.BorderWidth = 1;
                    tc_det_head_OP.Width = 70;
                  
                    Literal lit_det_head_OP = new Literal();
                    lit_det_head_OP.Text = "<b>Sale</b>";
                    tc_det_head_OP.Attributes.Add("Class", "tblHead");
                    tc_det_head_OP.Controls.Add(lit_det_head_OP);
                    tc_det_head_OP.HorizontalAlign = HorizontalAlign.Center;
                    tr_catg.Cells.Add(tc_det_head_OP);

                    TableCell tc_det_head_Qua = new TableCell();
                    tc_det_head_Qua.Width = 70;
                  
                    tc_det_head_Qua.BorderStyle = BorderStyle.Solid;
                    tc_det_head_Qua.BorderWidth = 1;
                    Literal lit_det_head_qua = new Literal();
                    lit_det_head_qua.Text = "<b>Order ( case.pcs )</b>";
                    tc_det_head_Qua.Attributes.Add("Class", "tblHead");
                    tc_det_head_Qua.Controls.Add(lit_det_head_qua);
                    tc_det_head_Qua.HorizontalAlign = HorizontalAlign.Center;
                    tr_catg.Cells.Add(tc_det_head_Qua);


                    TableCell orderquan = new TableCell();
                    orderquan.Width = 70;

                    orderquan.BorderStyle = BorderStyle.Solid;
                    orderquan.BorderWidth = 1;
                    Literal orderquan_lit = new Literal();
                    orderquan_lit.Text = "<b>Percentage</b>";
                    orderquan.Attributes.Add("Class", "tblHead");
                    orderquan.Controls.Add(orderquan_lit);
                    orderquan.HorizontalAlign = HorizontalAlign.Center;
                    tr_catg.Cells.Add(orderquan);

                    TableCell percentdiff = new TableCell();
                    percentdiff.Width = 70;

                    percentdiff.BorderStyle = BorderStyle.Solid;
                    percentdiff.BorderWidth = 1;
                    Literal percentdiff_lit = new Literal();
                    percentdiff_lit.Text = "<b>Difference</b>";
                    percentdiff.Attributes.Add("Class", "tblHead");
                    percentdiff.Controls.Add(percentdiff_lit);
                    orderquan.HorizontalAlign = HorizontalAlign.Center;
                    tr_catg.Cells.Add(percentdiff);


                    tbl.Rows.Add(tr_catg);



                //}
            }



            iCount = 0;
            foreach (DataRow drdoctor in dsDr.Tables[0].Rows)
            {
                TableRow tr_det_sno = new TableRow();
                TableCell tc_det_SNo = new TableCell();
                iCount += 1;
                Literal lit_det_SNo = new Literal();
                lit_det_SNo.Text = "<center>" + iCount.ToString() + "</center>";
                stCrtDtaPnt += "{label:\"" + iCount.ToString() + "\",y:";
                stCrtDtaPnt1 += "{label:\"" + iCount.ToString() + "\",y:";
                tc_det_SNo.Attributes.Add("Class", "tblRow");
                tc_det_SNo.BorderStyle = BorderStyle.Solid;
                tc_det_SNo.BorderWidth = 1;
                tc_det_SNo.Controls.Add(lit_det_SNo);
                tr_det_sno.Cells.Add(tc_det_SNo);

                TableCell tc_det_Sl_No = new TableCell();
                tc_det_Sl_No.Visible = false;
                Literal lit_det_Sl_No = new Literal();
                lit_det_Sl_No.Text = "&nbsp;" + drdoctor["Product_Detail_Code"].ToString();
                tc_det_Sl_No.BorderStyle = BorderStyle.Solid;
                tc_det_Sl_No.BorderWidth = 1;
                tc_det_Sl_No.Attributes.Add("Class", "rptCellBorder");
                tc_det_Sl_No.Controls.Add(lit_det_Sl_No);
                tr_det_sno.Cells.Add(tc_det_Sl_No);

                TableCell tc_det_usr = new TableCell();
                Literal lit_det_usr = new Literal();
                lit_det_usr.Text = "&nbsp;" + drdoctor["Product_Detail_Name"].ToString();
                //stCrtDtaPnt += "{label:\"" + drdoctor["Product_Detail_Name"].ToString() + "\",y:";
                //stCrtDtaPnt1 += "{label:\"" + drdoctor["Product_Detail_Name"].ToString() + "\",y:";
                tc_det_usr.BorderStyle = BorderStyle.Solid;
                tc_det_usr.BorderWidth = 1;

                tc_det_usr.Attributes.Add("Class", "rptCellBorder");
                tc_det_usr.Controls.Add(lit_det_usr);
                tr_det_sno.Cells.Add(tc_det_usr);

                TableCell tc_det_sfcode = new TableCell();
                tc_det_sfcode.HorizontalAlign = HorizontalAlign.Center;
              
                Literal lit_det_sfcode = new Literal();
                lit_det_sfcode.Text = "&nbsp;" + drdoctor["Product_Sale_Unit"].ToString();
                tc_det_sfcode.BorderStyle = BorderStyle.Solid;
                tc_det_sfcode.BorderWidth = 1;
                tc_det_sfcode.Attributes.Add("Class", "rptCellBorder");
                tc_det_sfcode.Controls.Add(lit_det_sfcode);
                tr_det_sno.Cells.Add(tc_det_sfcode);
         





                //if (getweek.Tables[0].Rows.Count > 0)
                //{

//                    for (int i = 1; i < wee; i++)
//                    {
                Notice sf = new Notice();
   
     
//                        dsDrr = sd.Statewise_purchase_productdetail(div_code, FMonth, FYear, i, drdoctor["Product_Detail_Code"].ToString(), statecode);
//                        if (dsDrr.Tables[0].Rows.Count > 0)
//                        {
//                            foreach (DataRow drrdoctor in dsDrr.Tables[0].Rows)
//                            {

                dsDoc = sf.get_sale_orderwithsale(div_code, sfcode, Month, Year, drdoctor["Product_Detail_Code"].ToString());


                if (dsDoc.Tables[0].Rows.Count > 0)
                    tot_dr = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                if (tot_dr != "0")
                {

                    if (tot_dr != "")
                    {
                        iTotLstCount2 = (Decimal.Parse(tot_dr));
                    }
                }
                if (iTotLstCount2 != 0)
                {
                    TableCell tc_det_dr_name = new TableCell();
                    tc_det_dr_name.HorizontalAlign = HorizontalAlign.Center;

                    Literal lit_det_dr_name = new Literal();
                    lit_det_dr_name.Text = tot_dr.ToString();
                    stCrtDtaPnt1 += Convert.ToString(iTotLstCount2) + "},";
                    //lit_det_dr_name.Text = drrdoctor["Rec_Qty"].ToString();
                    tc_det_dr_name.Attributes.Add("Class", "tblRow");
                    tc_det_dr_name.BorderStyle = BorderStyle.Solid;
                    tc_det_dr_name.BorderWidth = 1;
                    tc_det_dr_name.Controls.Add(lit_det_dr_name);
                    tr_det_sno.Cells.Add(tc_det_dr_name);
                }
                else
                {
                    TableCell tc_det_dr_name = new TableCell();
                    tc_det_dr_name.HorizontalAlign = HorizontalAlign.Center;

                    Literal lit_det_dr_name = new Literal();
                    lit_det_dr_name.Text = tot_dr.ToString();
                    stCrtDtaPnt1 += "0},";
                    //lit_det_dr_name.Text = drrdoctor["Rec_Qty"].ToString();
                    tc_det_dr_name.Attributes.Add("Class", "tblRow");
                    tc_det_dr_name.BorderStyle = BorderStyle.Solid;
                    tc_det_dr_name.BorderWidth = 1;
                    tc_det_dr_name.Controls.Add(lit_det_dr_name);
                    tr_det_sno.Cells.Add(tc_det_dr_name);
                }
                iTotLstCount2 = 0;
                dsDoc = sf.get_order_orderwithsale(div_code, sfcode, Month, Year, drdoctor["Product_Detail_Code"].ToString());

                string ordCQty = "";
                if (dsDoc.Tables[0].Rows.Count > 0)
                {
                    tot_drr = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                    ordCQty = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                }
                if (tot_drr != "0")
                {

                    if (tot_drr != "")
                    {
                        iTotLstCount1 = (Decimal.Parse(tot_drr));
                    }
                }
                if (iTotLstCount1 != 0)
                {

                    TableCell tc_det_dr_hq = new TableCell();
                    tc_det_dr_hq.HorizontalAlign = HorizontalAlign.Center;
                    Literal lit_det_dr_hq = new Literal();
                    lit_det_dr_hq.Text = ordCQty.ToString();
                      
                    stCrtDtaPnt += Convert.ToString(iTotLstCount1) + "},";
                    tc_det_dr_hq.Attributes.Add("Class", "tblRow");
                    tc_det_dr_hq.BorderStyle = BorderStyle.Solid;
                    tc_det_dr_hq.BorderWidth = 1;
                    tc_det_dr_hq.Controls.Add(lit_det_dr_hq);
                    tr_det_sno.Cells.Add(tc_det_dr_hq);
                }
                else
                {
                    TableCell tc_det_dr_hq = new TableCell();
                    tc_det_dr_hq.HorizontalAlign = HorizontalAlign.Center;
                    Literal lit_det_dr_hq = new Literal();
                    lit_det_dr_hq.Text = ordCQty.ToString();

                    stCrtDtaPnt += "0},";
                    tc_det_dr_hq.Attributes.Add("Class", "tblRow");
                    tc_det_dr_hq.BorderStyle = BorderStyle.Solid;
                    tc_det_dr_hq.BorderWidth = 1;
                    tc_det_dr_hq.Controls.Add(lit_det_dr_hq);
                    tr_det_sno.Cells.Add(tc_det_dr_hq);

                }
                iTotLstCount1 = 0;

             
                if (tot_dr == "")
                {
                    tot_dr = "0";
                }
                if (tot_drr == "")
                {
                    tot_drr = "0";
                }
                int salevalue = int.Parse(tot_dr);
                int ordervalue = int.Parse(tot_drr);
                //if (salevalue == 0)
                //{
                //    salevalue = 1;
                //}
                //if (ordervalue == 0)
                //{
                //    ordervalue = 1;
                //}
                //int percentagee = (ordervalue) / (salevalue);
                if (salevalue == 0)
                {
                    double percentagee = (double)ordervalue / salevalue;
                    string percentcvalue ="0";
                    TableCell percentge = new TableCell();
                    Literal percentge_lit = new Literal();
                    percentge.HorizontalAlign = HorizontalAlign.Center;

                    percentge_lit.Text = percentcvalue;
                    percentge.Attributes.Add("Class", "tblRow");
                    percentge.BorderStyle = BorderStyle.Solid;
                    percentge.BorderWidth = 1;
                    percentge.Controls.Add(percentge_lit);
                    tr_det_sno.Cells.Add(percentge);

                }
                else
                {
                    double percentagee = (double)ordervalue / salevalue;
                    string percentcvalue = Convert.ToString(percentagee);
                    TableCell percentge = new TableCell();
                    Literal percentge_lit = new Literal();
                    percentge.HorizontalAlign = HorizontalAlign.Center;

                    percentge_lit.Text = percentcvalue;
                    percentge.Attributes.Add("Class", "tblRow");
                    percentge.BorderStyle = BorderStyle.Solid;
                    percentge.BorderWidth = 1;
                    percentge.Controls.Add(percentge_lit);
                    tr_det_sno.Cells.Add(percentge);

                }
                int diffe = ordervalue - salevalue;
                string difference = Convert.ToString(diffe);
                TableCell diff = new TableCell();
                diff.HorizontalAlign = HorizontalAlign.Center;
                Literal diff_lit = new Literal();
                diff_lit.Text = difference; 
                diff.Attributes.Add("Class", "tblRow");
                diff.BorderStyle = BorderStyle.Solid;
                diff.BorderWidth = 1;
                diff.Controls.Add(diff_lit);
                tr_det_sno.Cells.Add(diff);

//                        }


                        tbl.Rows.Add(tr_det_sno);


                    //}
                //}

              

            }

            string scrpt = "arr=[" + stCrtDtaPnt + "];aar1=[" + stCrtDtaPnt1 + "];[window.onload = function () { genChart(aar1);genChart1(arr,aar1);}]";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "SetupData", scrpt, true);
        }


    }

    
