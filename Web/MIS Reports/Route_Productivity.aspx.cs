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
using iTextSharp.tool.xml;

public partial class MIS_Reports_Route_Productivity : System.Web.UI.Page
{
    string sfCode = string.Empty;
    string sfname = string.Empty;
    string divcode = string.Empty;
    string iTotLstCount1 = string.Empty;
    int iTotLstCount = 0;
    int iTotLstCount2 = 0;
    string sMode = string.Empty;
    string FMonth = string.Empty;
    string FYear = string.Empty;
    string TMonth = string.Empty;
    string TYear = string.Empty;
    
    string Prod = string.Empty;
    string Prod_Name = string.Empty;
    string sReturn = string.Empty;
    string sDay = string.Empty;
    string sCnt = string.Empty;
    string vMode = string.Empty;
    DataSet dsSalesForce = new DataSet();
    DataSet dsmgrsf = new DataSet();
    DataSet dsDoc = null;
    DateTime dtCurrent;
    string tot_fldwrk = string.Empty;
    string ChemistPOB_visit = string.Empty;
    string tot_Sub_days = string.Empty;
    string tot_dr = string.Empty;
    string Chemist_visit = string.Empty;
    string Stock_Visit = string.Empty;
    string tot_Stock_Calls_Seen = string.Empty;
    string tot_Dcr_Leave = string.Empty;
    string UnlistVisit = string.Empty;
    string tot_dcr_dr = string.Empty;
    string tot_doc_met = string.Empty;
    string tot_doc_calls_seen = string.Empty;
    string tot_doc_Unlstcalls_seen = string.Empty;
    string tot_CSH_calls_seen = string.Empty;
    string Monthsub = string.Empty;
    string strSf_Code = string.Empty;
    string MultiSf_Code = string.Empty;
    DataSet dsprd = new DataSet();
    string MultiProd_Code = string.Empty;
    string Multi_Prod = string.Empty;
    string sCurrentDate = string.Empty;
    DataSet dsDr = null;
    string str_code = string.Empty;
    private object tr_det_row;

    protected void Page_Load(object sender, EventArgs e)
    {
        divcode = Session["div_code"].ToString();
        sfCode = Request.QueryString["sfcode"].ToString();
        sfname = Request.QueryString["Sf_Name"].ToString();
        FMonth = Request.QueryString["FMonth"].ToString();
        FYear = Request.QueryString["FYear"].ToString();
        TMonth = Request.QueryString["TMonth"].ToString();
        TYear = Request.QueryString["TYear"].ToString();
     
        System.Globalization.DateTimeFormatInfo mfi = new System.Globalization.DateTimeFormatInfo();
        string strFMonthName = mfi.GetMonthName(Convert.ToInt16(FMonth)).ToString().Substring(0, 3);
        string strTMonthName = mfi.GetMonthName(Convert.ToInt16(TMonth)).ToString().Substring(0, 3);

        lblHead.Text = "Route Productivity for  " + strFMonthName + " " + FYear + " " + "To" + " " + strTMonthName + " " + TYear;
        Feild.Text = sfname;
        string scrpt = "arr=[" + Fillcate() + "];window.onload = function () {genChart('chartContainer',arr,'Route Productivity');}";
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "SetupData", scrpt, true);

        //lblIDMonth.Visible = false;
        //lblIDYear.Visible = false;
        //FillSF();
        CreateDynamicTable();

    }
    private string Fillcate()
    {
        string sURL = string.Empty;
        string stCrtDtaPnt = string.Empty;
        string stCrtDtaPnts = string.Empty;
        string stCrtDtaPntss = string.Empty;
        //Year = viewdrop.SelectedItem.ToString();
        SalesForce sf = new SalesForce();
        dsSalesForce = sf.tGetPro(sfCode, FMonth, TMonth, FYear, TYear);
        foreach (DataRow drFF in dsSalesForce.Tables[0].Rows)
        {
            iTotLstCount = drFF["OVal"] == DBNull.Value ? 0 : Convert.ToInt32(drFF["OVal"]);

            stCrtDtaPnt += "{label:\"" + drFF["Territory_Name"].ToString() + "\",y: ";
            stCrtDtaPnt += iTotLstCount + "},";


        }
        return stCrtDtaPnt;

    }
    private void CreateDynamicTable()
    {
        string stCrtDtaPnt = string.Empty;
        string stCrtDtaPnts = string.Empty;
        string stCrtDtaPntss = string.Empty;
        int tot = 0;
        SalesForce dr = new SalesForce();

        dsDr = dr.tGetPro(sfCode, FMonth, TMonth, FYear, TYear);


        if (dsDr.Tables[0].Rows.Count > 0)
        {

            TableRow tr_det_head = new TableRow();
            tr_det_head.BorderStyle = BorderStyle.Solid;
            tr_det_head.BorderWidth = 1;
            tr_det_head.BackColor = System.Drawing.Color.FromName("#496a9a");
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
            lit_det_head_doc.Text = "<b>Route Name</b>";
            tc_det_head_doc.Attributes.Add("Class", "tblHead");
            tc_det_head_doc.Controls.Add(lit_det_head_doc);
            tc_det_head_doc.HorizontalAlign = HorizontalAlign.Center;
            tr_det_head.Cells.Add(tc_det_head_doc);

            TableCell tc_det_head_cdn = new TableCell();        
            tc_det_head_cdn.BorderStyle = BorderStyle.Solid;
            tc_det_head_cdn.BorderWidth = 1;            
            Literal lit_det_head_cdn = new Literal();
            lit_det_head_cdn.Text = "<b>Route code</b>";          
            tc_det_head_cdn.Attributes.Add("Class", "tblHead");          
            tc_det_head_cdn.Controls.Add(lit_det_head_cdn);            
            tc_det_head_cdn.HorizontalAlign = HorizontalAlign.Center;            
            tr_det_head.Cells.Add(tc_det_head_cdn);
            //TableCell tc_other_cell = new TableCell();
            tc_det_head_cdn.Attributes.Add("Class", "hiddenColumn");

           
            


            TableCell tc_det_head_Quai = new TableCell();
            tc_det_head_Quai.BorderStyle = BorderStyle.Solid;
            tc_det_head_Quai.BorderWidth = 1;
            Literal lit_det_head_quai = new Literal();
            lit_det_head_quai.Text = "<b>Total Retailer</b>";
            tc_det_head_Quai.Attributes.Add("Class", "tblHead");
            tc_det_head_Quai.Controls.Add(lit_det_head_quai);
            tc_det_head_Quai.HorizontalAlign = HorizontalAlign.Center;
            tr_det_head.Cells.Add(tc_det_head_Quai);

            TableCell tc_det_head_min = new TableCell();
            tc_det_head_min.BorderStyle = BorderStyle.Solid;
            tc_det_head_min.BorderWidth = 1;
            Literal lit_det_head_min = new Literal();
            lit_det_head_min.Text = "<b>Productive Retailer</b>";
            tc_det_head_min.Attributes.Add("Class", "tblHead");
            tc_det_head_min.Controls.Add(lit_det_head_min);
            tc_det_head_min.HorizontalAlign = HorizontalAlign.Center;
            tr_det_head.Cells.Add(tc_det_head_min);


            TableCell tc_det_head_dis = new TableCell();
            tc_det_head_dis.BorderStyle = BorderStyle.Solid;
            tc_det_head_dis.BorderWidth = 1;
            Literal lit_det_head_dis = new Literal();
            lit_det_head_dis.Text = "<b>Distributor</b>";
            tc_det_head_dis.Attributes.Add("Class", "tblHead");
            tc_det_head_dis.Controls.Add(lit_det_head_dis);
            tc_det_head_dis.HorizontalAlign = HorizontalAlign.Center;
            tr_det_head.Cells.Add(tc_det_head_dis);

            TableCell tc_det_head_min1 = new TableCell();
            tc_det_head_min1.BorderStyle = BorderStyle.Solid;
            tc_det_head_min1.BorderWidth = 1;
            Literal lit_det_head_min1 = new Literal();
            lit_det_head_min1.Text = "<b>Productive Calls%</b>";
            tc_det_head_min1.Attributes.Add("Class", "tblHead");
            tc_det_head_min1.Controls.Add(lit_det_head_min1);
            tc_det_head_min1.HorizontalAlign = HorizontalAlign.Center;
            tr_det_head.Cells.Add(tc_det_head_min1);


            TableCell tc_det_head_tar = new TableCell();
            tc_det_head_tar.BorderStyle = BorderStyle.Solid;
            tc_det_head_tar.BorderWidth = 1;
            Literal lit_det_head_tar = new Literal();
            lit_det_head_tar.Text = "<b>Route Target</b>";
            tc_det_head_tar.Attributes.Add("Class", "tblHead");
            tc_det_head_tar.Controls.Add(lit_det_head_tar);
            tc_det_head_tar.HorizontalAlign = HorizontalAlign.Center;
            tr_det_head.Cells.Add(tc_det_head_tar);

            TableCell tc_det_head_ach = new TableCell();
            tc_det_head_ach.BorderStyle = BorderStyle.Solid;
            tc_det_head_ach.BorderWidth = 1;
            Literal lit_det_head_ach = new Literal();
            lit_det_head_ach.Text = "<b>Achieved Target</b>";
            tc_det_head_ach.Attributes.Add("Class", "tblHead");
            tc_det_head_ach.Controls.Add(lit_det_head_ach);
            tc_det_head_ach.HorizontalAlign = HorizontalAlign.Center;
            tr_det_head.Cells.Add(tc_det_head_ach);

            TableCell tc_det_head_achp = new TableCell();
            tc_det_head_achp.BorderStyle = BorderStyle.Solid;
            tc_det_head_achp.BorderWidth = 1;
            Literal lit_det_head_achp = new Literal();
            lit_det_head_achp.Text = "<b>Target Achieved %</b>";
            tc_det_head_achp.Attributes.Add("Class", "tblHead");
            tc_det_head_achp.Controls.Add(lit_det_head_achp);
            tc_det_head_achp.HorizontalAlign = HorizontalAlign.Center;
            tr_det_head.Cells.Add(tc_det_head_achp);

            TableCell tc_det_head_achp1 = new TableCell();
            tc_det_head_achp1.BorderStyle = BorderStyle.Solid;
            tc_det_head_achp1.BorderWidth = 1;
            Literal lit_det_head_achp1 = new Literal();
            lit_det_head_achp1.Text = "<b>Net Weight Value</b>";
            tc_det_head_achp1.Attributes.Add("Class", "tblHead");
            tc_det_head_achp1.Controls.Add(lit_det_head_achp1);
            tc_det_head_achp1.HorizontalAlign = HorizontalAlign.Center;
            tr_det_head.Cells.Add(tc_det_head_achp1);

            tbl.Rows.Add(tr_det_head);



            int iCount = 0;
            foreach (DataRow drdoctor in dsDr.Tables[0].Rows)
            {
                TableRow tr_det_sno = new TableRow();
                TableCell tc_det_SNo = new TableCell();
                iCount += 1;
                Literal lit_det_SNo = new Literal();
                lit_det_SNo.Text = iCount.ToString();
                tc_det_SNo.Attributes.Add("Class", "tblRow");
                tc_det_SNo.BorderStyle = BorderStyle.Solid;
                tc_det_SNo.BorderWidth = 1;
                tc_det_SNo.HorizontalAlign = HorizontalAlign.Right;
                tc_det_SNo.Controls.Add(lit_det_SNo);
                tr_det_sno.Cells.Add(tc_det_SNo);

                TableCell tc_det_usr = new TableCell();
                Literal lit_det_usr = new Literal();
                lit_det_usr.Text = "&nbsp;" + drdoctor["Territory_Name"].ToString();
                stCrtDtaPnt += "{label:\"" + drdoctor["Territory_Name"].ToString() + "\",y:";
                stCrtDtaPntss =drdoctor["Territory_Name"].ToString();
                tc_det_usr.BorderStyle = BorderStyle.Solid;
                tc_det_usr.BorderWidth = 1;
                tc_det_usr.Attributes.Add("Class", "rptCellBorder");
                tc_det_usr.Controls.Add(lit_det_usr);
                tr_det_sno.Cells.Add(tc_det_usr);

                TableCell tc_det_code = new TableCell();
                Literal lit_det_code = new Literal();
                lit_det_code.Text = "&nbsp;" + drdoctor["Territory_Code"].ToString();
                stCrtDtaPnts =  drdoctor["Territory_Code"].ToString();
                tc_det_code.BorderStyle = BorderStyle.Solid;
                tc_det_code.BorderWidth = 1;

                tc_det_code.Attributes.Add("Class", "rptCellBorder");
                tc_det_code.Controls.Add(lit_det_code);
                //TableCell tc_other_cells = new TableCell();
                tc_det_code.Attributes.Add("Class", "hiddenColumn");
                tr_det_sno.Cells.Add(tc_det_code);


                //                TableCell tc_det_dr_name = new TableCell();
                //                Literal lit_det_dr_name = new Literal();
                //                lit_det_dr_name.Text = drdoctor["DSM_name"].ToString();
                //                tc_det_dr_name.Attributes.Add("Class", "tblRow");
                //                tc_det_dr_name.BorderStyle = BorderStyle.Solid;
                //                tc_det_dr_name.BorderWidth = 1;
                //tc_det_dr_name.HorizontalAlign = HorizontalAlign.Right;
                //                tc_det_dr_name.Controls.Add(lit_det_dr_name);
                //                tr_det_sno.Cells.Add(tc_det_dr_name);

                TableCell tc_det_dr_hq = new TableCell();
                Literal lit_det_dr_hq = new Literal();
                lit_det_dr_hq.Text = drdoctor["RetCnt"].ToString();
                tc_det_dr_hq.Attributes.Add("Class", "tblRow");
                tc_det_dr_hq.BorderStyle = BorderStyle.Solid;
                tc_det_dr_hq.BorderWidth = 1;

                tc_det_dr_hq.HorizontalAlign = HorizontalAlign.Right;
                HtmlAnchor anchor = new HtmlAnchor();
                anchor.HRef = "javascript:void(0);";               
                anchor.Controls.Add(lit_det_dr_hq);                
                tc_det_dr_hq.Controls.Add(anchor);                
                string jsClickFunction = "openPopupWindow('"+sfCode+"','"+ FMonth+"','"+ TMonth+"','"+ FYear+"','"+ TYear+"','"+ sfname + "','"+ stCrtDtaPnts + "','"+ stCrtDtaPntss + "');";
                anchor.Attributes.Add("onclick", jsClickFunction);                               
                anchor.Controls.Add(lit_det_dr_hq);                                
               // tc_det_dr_hq.Controls.Add(lit_det_dr_hq);
                tr_det_sno.Cells.Add(tc_det_dr_hq);


                TableCell tc_det_dr_min = new TableCell();
                Literal lit_det_dr_min = new Literal();
                lit_det_dr_min.Text = Convert.ToDecimal(drdoctor["Cnt"]).ToString("00");
                tc_det_dr_min.Attributes.Add("Class", "tblRow");
                tc_det_dr_min.BorderStyle = BorderStyle.Solid;
                tc_det_dr_min.BorderWidth = 1;
                tc_det_dr_min.HorizontalAlign = HorizontalAlign.Right;
                tc_det_dr_min.Controls.Add(lit_det_dr_min);
                tr_det_sno.Cells.Add(tc_det_dr_min);

                TableCell tc_det_dis_min = new TableCell();
                Literal lit_det_dis_min = new Literal();
                lit_det_dis_min.Text = drdoctor["Stkname"].ToString().TrimEnd(',');
                tc_det_dis_min.Attributes.Add("Class", "tblRow");
                tc_det_dis_min.BorderStyle = BorderStyle.Solid;
                tc_det_dis_min.BorderWidth = 1;
                tc_det_dis_min.HorizontalAlign = HorizontalAlign.Right;
                tc_det_dis_min.Controls.Add(lit_det_dis_min);
                tr_det_sno.Cells.Add(tc_det_dis_min);

                TableCell tc_det_dr_min1 = new TableCell();
                Literal lit_det_dr_min1 = new Literal();
                Decimal per = Convert.ToDecimal(lit_det_dr_hq.Text) <= 0 ? 0 : Math.Round(Convert.ToDecimal(lit_det_dr_min.Text) / Convert.ToDecimal(lit_det_dr_hq.Text), 2) * 100;
                lit_det_dr_min1.Text = per.ToString("0.00");
                tc_det_dr_min1.Attributes.Add("Class", "tblRow");
                tc_det_dr_min1.BorderStyle = BorderStyle.Solid;
                tc_det_dr_min1.BorderWidth = 1;
                tc_det_dr_min1.HorizontalAlign = HorizontalAlign.Right;
                tc_det_dr_min1.Controls.Add(lit_det_dr_min1);
                tr_det_sno.Cells.Add(tc_det_dr_min1);


                TableCell tc_det_dr_tar = new TableCell();
                Literal lit_det_dr_tar = new Literal();
                lit_det_dr_tar.Text = Convert.ToDecimal(drdoctor["RouteTar"]).ToString("0.00");
                tc_det_dr_tar.Attributes.Add("Class", "tblRow");
                tc_det_dr_tar.BorderStyle = BorderStyle.Solid;
                tc_det_dr_tar.BorderWidth = 1;
                tc_det_dr_tar.HorizontalAlign = HorizontalAlign.Right;
                tc_det_dr_tar.Controls.Add(lit_det_dr_tar);
                tr_det_sno.Cells.Add(tc_det_dr_tar);

                TableCell tc_det_dr_ach = new TableCell();
                Literal lit_det_dr_ach = new Literal();
                lit_det_dr_ach.Text = drdoctor["OVal"] == DBNull.Value ? "0" : Convert.ToDecimal(drdoctor["OVal"]).ToString("0.00");
                tc_det_dr_ach.Attributes.Add("Class", "tblRow");
                tc_det_dr_ach.BorderStyle = BorderStyle.Solid;
                tc_det_dr_ach.BorderWidth = 1;
                tc_det_dr_ach.HorizontalAlign = HorizontalAlign.Right;
                tc_det_dr_ach.Controls.Add(lit_det_dr_ach);
                tr_det_sno.Cells.Add(tc_det_dr_ach);

                TableCell tc_det_dr_achp = new TableCell();
                Literal lit_det_dr_achp = new Literal();
                Decimal per1 = Convert.ToDecimal(lit_det_dr_tar.Text) <= 0 ? 0 : Math.Round(Convert.ToDecimal(lit_det_dr_ach.Text) / Convert.ToDecimal(lit_det_dr_tar.Text), 2) * 100;


                lit_det_dr_achp.Text = per1.ToString("0.00");
                tc_det_dr_achp.Attributes.Add("Class", "tblRow");
                tc_det_dr_achp.BorderStyle = BorderStyle.Solid;
                tc_det_dr_achp.BorderWidth = 1;
                tc_det_dr_achp.HorizontalAlign = HorizontalAlign.Right;
                tc_det_dr_achp.Controls.Add(lit_det_dr_achp);
                tr_det_sno.Cells.Add(tc_det_dr_achp);


                TableCell tc_det_dr_ach1 = new TableCell();
                Literal lit_det_dr_ach1 = new Literal();
                lit_det_dr_ach1.Text = Convert.ToDecimal(drdoctor["Net"]).ToString("0.00");
                tc_det_dr_ach1.Attributes.Add("Class", "tblRow");
                tc_det_dr_ach1.BorderStyle = BorderStyle.Solid;
                tc_det_dr_ach1.BorderWidth = 1;
                tc_det_dr_ach1.HorizontalAlign = HorizontalAlign.Right;
                tc_det_dr_ach1.Controls.Add(lit_det_dr_ach1);
                tr_det_sno.Cells.Add(tc_det_dr_ach1);

                tbl.Rows.Add(tr_det_sno);

                string scrpt = "arr=[" + stCrtDtaPnt + "];window.onload = function () {genChart(arr);}";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "SetupData", scrpt, true);
            }
            TableRow tr_tot = new TableRow();

            TableCell TotalCell = new TableCell();
            TotalCell.BorderStyle = BorderStyle.Solid;
            TotalCell.BorderWidth = 1;
            Literal lit_TotalCell = new Literal();
            lit_TotalCell.Text = "<b>Total</b>";
            TotalCell.Attributes.Add("Class", "tblHead");
            TotalCell.Controls.Add(lit_TotalCell);
            TotalCell.ColumnSpan = 5;
            TotalCell.HorizontalAlign = HorizontalAlign.Center;
            tr_tot.Cells.Add(TotalCell);

            tbl.Rows.Add(tr_tot);
           
            decimal totVAl = 0;
            decimal totVAlrt = 0;
            decimal totVAltach = 0;
            foreach (DataRow row in dsDr.Tables[0].Rows)
            {
               decimal  Val = row["OVal"] == DBNull.Value ? 0 : Convert.ToDecimal(row["OVAL"]);
                totVAl += Val;
                decimal rt = row["RouteTar"] == DBNull.Value ? 0 : Convert.ToDecimal(row["RouteTar"]);
                totVAlrt += rt;
                decimal ta= Convert.ToDecimal(row["RouteTar"]) <= 0 ? 0 : Math.Round(Convert.ToDecimal(row["OVAL"]) / Convert.ToDecimal(row["RouteTar"]),2) * 100;
                totVAltach += ta;
            }
                TableCell pcall = new TableCell();
                Literal lit_pcall = new Literal();
                lit_pcall.Text = ("");
                pcall.Attributes.Add("Class", "tblRow");
                pcall.BorderStyle = BorderStyle.Solid;
                pcall.BorderWidth = 1;
                pcall.HorizontalAlign = HorizontalAlign.Right;
                pcall.Controls.Add(lit_pcall);
                tr_tot.Cells.Add(pcall);

                TableCell rtar = new TableCell();
                Literal lit_rtar = new Literal();
                lit_rtar.Text = (totVAlrt.ToString("0.00"));
                rtar.Attributes.Add("Class", "tblRow");
                rtar.BorderStyle = BorderStyle.Solid;
                rtar.BorderWidth = 1;
                rtar.HorizontalAlign = HorizontalAlign.Right;
                rtar.Controls.Add(lit_rtar);
                tr_tot.Cells.Add(rtar);

                TableCell Atar = new TableCell();
                Literal lit_Atar = new Literal();
                lit_Atar.Text = (totVAl.ToString("0.00"));
                Atar.Attributes.Add("Class", "tblRow");
                Atar.BorderStyle = BorderStyle.Solid;
                Atar.BorderWidth = 1;
                Atar.HorizontalAlign = HorizontalAlign.Right;
                Atar.Controls.Add(lit_Atar);
                tr_tot.Cells.Add(Atar);

                TableCell TArac = new TableCell();
                Literal lit_TArac = new Literal();
                lit_TArac.Text = (totVAltach.ToString("0.00"));
                TArac.Attributes.Add("Class", "tblRow");
                TArac.BorderStyle = BorderStyle.Solid;
                TArac.BorderWidth = 1;
                TArac.HorizontalAlign = HorizontalAlign.Right;
                TArac.Controls.Add(lit_TArac);
                tr_tot.Cells.Add(TArac);

                TableCell netv = new TableCell();
                Literal lit_netv = new Literal();
                lit_netv.Text = ("");
                netv.Attributes.Add("Class", "tblRow");
                netv.BorderStyle = BorderStyle.Solid;
                netv.BorderWidth = 1;
                netv.HorizontalAlign = HorizontalAlign.Right;
                netv.Controls.Add(lit_netv);
                tr_tot.Cells.Add(netv);

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

    protected void btnClose_Click(object sender, EventArgs e)
    {

    }
}