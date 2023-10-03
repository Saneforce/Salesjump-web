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

using System.Web.Services;


public partial class MIS_Reports_rpt_Month_wise_Activity_Data : System.Web.UI.Page
{
    string sfCode = string.Empty;
    string sfname = string.Empty;
    string divcode = string.Empty;
    decimal iTotLstCount1 = 0;
    decimal iTotLstCount = 0;
    decimal iTotLstCount2 = 0;
    int iMonth = -1;
    int iYear = -1;
    DataSet dsDoctor = null;
    string sMode = string.Empty;
    string FMonth = string.Empty;
    string FYear = string.Empty;
    string TMonth = string.Empty;
    string TYear = string.Empty;
    string stype = string.Empty;
    string Prod_Name = string.Empty;
    string sReturn = string.Empty;

    DataSet dsSalesForce = new DataSet();

    DataSet dsMGR = new DataSet();
    DataSet dsMr = new DataSet();
    DataSet dsDoc = null;
    DateTime dtCurrent;
    string tot_dr = string.Empty;
    string tot_dr1 = string.Empty;
    string tot_Drr = string.Empty;

    string Monthsub = string.Empty;
    string strSf_Code = string.Empty;

    DataSet dsprd = new DataSet();
    string sCurrentDate = string.Empty;
    string stockist_code = string.Empty;
    string stURL = string.Empty;
    string Stock_name = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        divcode = Session["div_code"].ToString();
        sfCode = Request.QueryString["SF_Code"].ToString();
        FMonth = Request.QueryString["FMonth"].ToString();
        FYear = Request.QueryString["FYear"].ToString();
        TMonth = Request.QueryString["TMonth"].ToString();
        TYear = Request.QueryString["TYear"].ToString();
        hidn_sf_code.Value = sfCode;
        hidnYears.Value = FYear;
        lblsf_name.Text = Request.QueryString["SF_Name"].ToString(); ;
        lblyear.Text = FYear;
        System.Globalization.DateTimeFormatInfo mfi = new System.Globalization.DateTimeFormatInfo();
        string strFMonthName = mfi.GetMonthName(Convert.ToInt16(FMonth)).ToString().Substring(0, 3);
        string strTMonthName = mfi.GetMonthName(Convert.ToInt16(TMonth)).ToString().Substring(0, 3);
        lblHead.Text = "Month wise Activity Data for  " + strFMonthName + " " + FYear + " " + "To" + " " + strTMonthName + " " + TYear;

        FillSF();
    }

    private void FillSF()
    {
        GV_DATA.DataSource = null;
        GV_DATA.DataBind();
        //RoutePlan rop = new RoutePlan();
        //DataSet DsRoute = rop.get_Route_Name(divcode, sfCode);

        //ListedDR ldr = new ListedDR();
        //DataSet DsRetailer = ldr.Get_Retailer_sal(divcode, FYear, sfCode);

        DataTable dsData = new DataTable();
        dsData.Columns.Add("S.NO", typeof(string));
        dsData.Columns.Add("DESCRIPTION", typeof(string));
        int months = (Convert.ToInt32(TYear) - Convert.ToInt32(FYear)) * 12 + Convert.ToInt32(TMonth) - Convert.ToInt32(FMonth); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
        int cmonth = Convert.ToInt32(FMonth);
        int cyear = Convert.ToInt32(FYear);


        ViewState["months"] = months;
        ViewState["cmonth"] = cmonth;
        ViewState["cyear"] = cyear;

        SalesForce sal = new SalesForce();


        if (months >= 0)
        {
            for (int j = 1; j <= months + 1; j++)
            {
               
                Monthsub = sal.getMonthName(cmonth.ToString()) + "-" + cyear;
                dsData.Columns.Add(Monthsub.Substring(0, 3) + "-" + cyear, typeof(decimal));
                cmonth = cmonth + 1;
                if (cmonth == 13)
                {
                    cmonth = 1;
                    cyear = cyear + 1;
                }
            }
        }

        //dsData.Columns.Add("Average", typeof(decimal));
        int i = 1;
        dsData.Rows.Add("1", "No.Of Days Worked in field");
        dsData.Rows.Add("2", "Leave Taken");
        dsData.Rows.Add("3", "Total no.Of Customers");
        dsData.Rows.Add("4", "No.Of Customers Visited");
        dsData.Rows.Add("5", "No.Of Cutomers given orders");
        dsData.Rows.Add("6", "New Customers - Current Month");
        dsData.Rows.Add("7", "PenBooking- Target");
        dsData.Rows.Add("8", "PenBooking- Sales");
        dsData.Rows.Add("9", "Secondary - Target");
        dsData.Rows.Add("10", "Secondary- Sales");
        dsData.Rows.Add("11", "Primary- Target");
        dsData.Rows.Add("12", "Primary- Sales");
        dsData.Rows.Add("", " ");

       
        int cou = 1;
        //dsData.Rows[0][cou + 1] = jan_count;


        months = Convert.ToInt16(ViewState["months"].ToString());
        cmonth = Convert.ToInt16(ViewState["cmonth"].ToString());
        cyear = Convert.ToInt16(ViewState["cyear"].ToString());

        if (months >= 0)
        {

            for (int j = 1; j <= months + 1; j++)
            {

                dsDoc = sal.Act_Data_FL(divcode,sfCode, cyear,cmonth);
                


                if (dsDoc.Tables[0].Rows.Count > 0)
                    tot_dr = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                    tot_dr1= dsDoc.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();

                dsData.Rows[0][cou + j] = tot_dr;
                dsData.Rows[1][cou + j] = tot_dr1;

                //tot_dr = "0";

                cmonth = cmonth + 1;
                if (cmonth == 13)
                {
                    cmonth = 1;
                    cyear = cyear + 1;
                }


            }
            
           
        }

        Order ad = new Order();
        //total_ret
        months = Convert.ToInt16(ViewState["months"].ToString());
        cmonth = Convert.ToInt16(ViewState["cmonth"].ToString());
        cyear = Convert.ToInt16(ViewState["cyear"].ToString());

        if (months >= 0)
        {

            for (int j = 1; j <= months + 1; j++)
            {

                dsDoc = ad.View_total_Ret_Ac_data(divcode, sfCode, cyear, cmonth); 



                if (dsDoc.Tables[0].Rows.Count > 0)
                    tot_dr = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                //tot_dr1 = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();

                dsData.Rows[2][cou + j] = tot_dr;
                //dsData.Rows[1][cou + j] = tot_dr1;

                //tot_dr = "0";

                cmonth = cmonth + 1;
                if (cmonth == 13)
                {
                    cmonth = 1;
                    cyear = cyear + 1;
                }


            }


        }

       

        //total_ret_View
        months = Convert.ToInt16(ViewState["months"].ToString());
        cmonth = Convert.ToInt16(ViewState["cmonth"].ToString());
        cyear = Convert.ToInt16(ViewState["cyear"].ToString());

        if (months >= 0)
        {

            for (int j = 1; j <= months + 1; j++)
            {

                dsDoc = sal.Act_Data_TC_EC(divcode, sfCode, cyear, cmonth);



                if (dsDoc.Tables[0].Rows.Count > 0)
                    tot_dr = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                    tot_dr1 = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();

                dsData.Rows[3][cou + j] = tot_dr;
                dsData.Rows[4][cou + j] = tot_dr1;

                //tot_dr = "0";

                cmonth = cmonth + 1;
                if (cmonth == 13)
                {
                    cmonth = 1;
                    cyear = cyear + 1;
                }


            }


        }

      

        //total_ret_new
        months = Convert.ToInt16(ViewState["months"].ToString());
        cmonth = Convert.ToInt16(ViewState["cmonth"].ToString());
        cyear = Convert.ToInt16(ViewState["cyear"].ToString());

        if (months >= 0)
        {

            for (int j = 1; j <= months + 1; j++)
            {

                dsDoc = ad.View_total_Ret_Ac_data_new(divcode, sfCode, cyear, cmonth);



                if (dsDoc.Tables[0].Rows.Count > 0)
                    tot_dr = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                //tot_dr1 = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();

                dsData.Rows[5][cou + j] = tot_dr;
                //dsData.Rows[1][cou + j] = tot_dr1;

                //tot_dr = "0";

                cmonth = cmonth + 1;
                if (cmonth == 13)
                {
                    cmonth = 1;
                    cyear = cyear + 1;
                }


            }


        }
        //sale_tag
        months = Convert.ToInt16(ViewState["months"].ToString());
        cmonth = Convert.ToInt16(ViewState["cmonth"].ToString());
        cyear = Convert.ToInt16(ViewState["cyear"].ToString());

        if (months >= 0)
        {

            for (int j = 1; j <= months + 1; j++)
            {

                dsDoc = sal.Act_Data_Tag_val(divcode, sfCode, cyear, cmonth);



                if (dsDoc.Tables[0].Rows.Count > 0)
                    tot_dr = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                //tot_dr1 = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();

                dsData.Rows[6][cou + j] = tot_dr;
                

                //dsData.Rows[1][cou + j] = tot_dr1;

                //tot_dr = "0";

                cmonth = cmonth + 1;
                if (cmonth == 13)
                {
                    cmonth = 1;
                    cyear = cyear + 1;
                }


            }


        }

        //sale_val
        months = Convert.ToInt16(ViewState["months"].ToString());
        cmonth = Convert.ToInt16(ViewState["cmonth"].ToString());
        cyear = Convert.ToInt16(ViewState["cyear"].ToString());

        if (months >= 0)
        {

            for (int j = 1; j <= months + 1; j++)
            {

                dsDoc = sal.Act_Data_Sec_sale(divcode, sfCode, cyear, cmonth);



                if (dsDoc.Tables[0].Rows.Count > 0)
                    tot_dr = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                //tot_dr1 = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();

                dsData.Rows[7][cou + j] = tot_dr;
               
                //dsData.Rows[1][cou + j] = tot_dr1;

                //tot_dr = "0";

                cmonth = cmonth + 1;
                if (cmonth == 13)
                {
                    cmonth = 1;
                    cyear = cyear + 1;
                }


            }


        }



        GV_DATA.DataSource = dsData;
        GV_DATA.DataBind();

    }

    protected void Dgv_SKU_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            GridViewRow grv = e.Row;
            if (grv.Cells[0].Text.Equals("0"))
            {
                e.Row.BackColor = System.Drawing.Color.FromName("#39435C");
                e.Row.ForeColor = System.Drawing.Color.FromName("#fff");
                e.Row.Font.Bold = true;

            }
            //if (grv.Cells[0].Text.Equals("1"))
            //{
            //    e.Row.BackColor = System.Drawing.Color.FromName("#6b7794");
            //    e.Row.ForeColor = System.Drawing.Color.FromName("#fff");
            //    e.Row.Font.Bold = true;

            //}
            if (grv.Cells[1].Text.Equals("Total"))
            {
                e.Row.BackColor = System.Drawing.Color.FromName("#99FFFF");
                e.Row.Font.Bold = true;

            }

            //e.Row.Cells[0].Visible = true;
            //        e.Row.Cells[0].Width = 250;
            //        e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Left;
            //        e.Row.Cells[1].Width = 250;
            //        e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Left;
            //        e.Row.Cells[2].Width = 200;
            //        e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Left;
            //        e.Row.Cells[3].Width = 250;
            //        e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Left;

            //for (int i = 5; i < e.Row.Cells.Count; i++)
            //{
            //    e.Row.Cells[i].Width = 250;
            //    e.Row.Cells[i].HorizontalAlign = HorizontalAlign.Right;

            //}


        }
        catch (Exception ex)
        { }

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
        string attachment = "attachment; filename=" + strFileName + ".xls";

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
     // Response.Write("Purchase_Register_Distributor_wise.aspx");

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
                Document pdfDoc = new Document(PageSize.A3, 10f, 10f, 10f, 0f);
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

    public class VisitedList
    {
        public string custCode { get; set; }
        public string months { get; set; }
        public string counts { get; set; }
    }
    [WebMethod(EnableSession = true)]
    public static VisitedList[] GetVisitedDtls(string SF_Code, string FYears)
    {
        string div_code = "";
        try
        {
            div_code = HttpContext.Current.Session["div_code"].ToString();
        }
        catch
        {
            div_code = HttpContext.Current.Session["Division_Code"].ToString();
        }
        ListedDR ldr = new ListedDR();
        DataSet dsVisited = new DataSet();
        dsVisited = ldr.Get_Retailer_salVisted(div_code, SF_Code, FYears);
        List<VisitedList> vList = new List<VisitedList>();
        foreach (DataRow row in dsVisited.Tables[0].Rows)
        {
            VisitedList vl = new VisitedList();
            vl.custCode = row["Trans_Detail_Info_Code"].ToString();
            vl.months = row["Months"].ToString();
            vl.counts = row["CNTS"].ToString();
            vList.Add(vl);
        }
        return vList.ToArray();
    }



}