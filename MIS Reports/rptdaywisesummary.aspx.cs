using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
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
using System.Drawing;

public partial class MIS_Reports_rptdaywisesummary : System.Web.UI.Page
{
    string sfCode = string.Empty;
    string sfname = string.Empty;
    string divcode = string.Empty;
    string subdiv_code = string.Empty;
    string Year = string.Empty;
    string Month = string.Empty;
  
    DataSet dsSalesForce = new DataSet();
    DataSet dsmgrsf = new DataSet();
    DataSet dsDoc = null;
    DateTime dtCurrent; 
    string tot_dr = string.Empty;   
    string Monthsub = string.Empty;
    string strSf_Code = string.Empty;
    string MultiSf_Code = string.Empty;
    DataSet dsprd = new DataSet();
    string distributor_code = string.Empty;
	DataTable tbl1 = null;
  
   

   
    int subTotalRowIndex = 0;
    string sf_type = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        
     
            divcode = Session["div_code"].ToString();
            subdiv_code = Request.QueryString["subdiv"].ToString();
			sfCode = Request.QueryString["sf_code"].ToString();
            sfname = Request.QueryString["Sf_Name"].ToString();
            Month = Request.QueryString["Month"].ToString();
            Year = Request.QueryString["Year"].ToString();
            System.Globalization.DateTimeFormatInfo mfi = new System.Globalization.DateTimeFormatInfo();
            string strFMonthName = mfi.GetMonthName(Convert.ToInt16(Month)).ToString().Substring(0, 3);
            lblHead.Text = "Daywise Summary For  " + strFMonthName + " " + Year;
			Feild.Text = sfname;

            FillSF();
        

    }

    private void FillSF()
    {


        string sURL = string.Empty;
        string stURL = string.Empty;
        DataSet dsGV = new DataSet();
        DataSet dsGc = new DataSet();

        DCR dc = new DCR();

        string stCrtDtaPnt = string.Empty;
        //if (sf_type == "4")
        //{
        //dsGV = dc.daywisesalessummary(divcode,Year,Month,sfCode);
		tbl1= dc.daywisesalessummary_tbl(divcode,Year,Month,sfCode,subdiv_code);
		for (int i = 0; i < tbl1.Rows.Count; i++)
                        {
                            for (int j = 0; j < tbl1.Columns.Count; j++)
                            {
                                if (string.IsNullOrEmpty(tbl1.Rows[i][j].ToString()))
                                {

                                    tbl1.Rows[i][j] = "0";
                                }
                                else
                                {
                                    if (j > 2)
                                        tbl1.Rows[i][j] = Math.Round(Convert.ToDecimal(tbl1.Rows[i][j]), 2).ToString();
                                }

                            }
                        }
            if (tbl1.Rows.Count > 0)
            {
                gvdaywisesummary.DataSource = tbl1;
                gvdaywisesummary.DataBind();

				gvdaywisesummary.FooterRow.Cells[1].Text = "TOTAL";
                            gvdaywisesummary.FooterRow.Cells[1].Font.Bold = true;
                            gvdaywisesummary.FooterRow.Cells[1].HorizontalAlign = HorizontalAlign.Center;
                            gvdaywisesummary.FooterRow.Cells[1].Font.Underline = true;
                            for (int k = 2; k < tbl1.Columns.Count; k++)
                            {
                                // Int32 td = (tbl1.Columns[k] == null ) ? 0 : Convert.ToInt32(tbl1.Columns[k].ToString());



                                string total = " ";
                                //if (tbl1.Columns[k].ToString() == "presentee")
                                //{

                                //    total = tbl1.AsEnumerable().Sum(x => Convert.ToDecimal(x.Field<Int32>(tbl1.Columns[k].ToString()) == null ? 0 : Convert.ToDecimal(x.Field<Int32>(tbl1.Columns[k].ToString())))).ToString();

                                //}
                                //else if (tbl1.Columns[k].ToString() == "Absent")
                                //{
                                //    total = tbl1.AsEnumerable().Sum(x => Convert.ToDecimal(x.Field<Int32>(tbl1.Columns[k].ToString()) == null ? 0 : Convert.ToDecimal(x.Field<Int32>(tbl1.Columns[k].ToString())))).ToString();
                                //}

                                if (tbl1.Columns[k].ToString() == "sales")
                                {
                                    total = tbl1.AsEnumerable().Sum(x => Convert.ToDecimal(x.Field<Double>(tbl1.Columns[k].ToString()) == null ? 0 : Convert.ToDecimal(x.Field<Double>(tbl1.Columns[k].ToString())))).ToString();
                                }
                                //else if (tbl1.Columns[k].ToString() == "Average")
                                //{
                                //    total = tbl1.AsEnumerable().Sum(x => Convert.ToDecimal(x.Field<Double>(tbl1.Columns[k].ToString()) == null ? 0 : Convert.ToDecimal(x.Field<Double>(tbl1.Columns[k].ToString())))).ToString();
                                //}

                                //string total = tbl1.AsEnumerable().Sum(x => Convert.To(x.Field<string>(tbl1.Columns[k].ToString()))).ToString();

                                gvdaywisesummary.FooterRow.Cells[k].Font.Bold = true;
                                gvdaywisesummary.FooterRow.Cells[k].HorizontalAlign = HorizontalAlign.Left;
                                gvdaywisesummary.FooterRow.Cells[k].Text = total.ToString();
                                gvdaywisesummary.FooterRow.Cells[k].Font.Bold = true;
                                gvdaywisesummary.FooterRow.BackColor = System.Drawing.Color.Beige;
                            }
            }
            else
            {
                gvdaywisesummary.DataSource = null;
                gvdaywisesummary.DataBind();
            }
            //Label1.Visible = false;

        //}
        //else
        //{




            //dsGV = dc.view_total_order_view(divcode, sfCode, Date);
            //if (dsGV.Tables[0].Rows.Count > 0)
            //{
            //    gvtotalorder.DataSource = dsGV;
            //    gvtotalorder.DataBind();
            //}
            //else
            //{
            //    gvtotalorder.DataSource = null;
            //    gvtotalorder.DataBind();
            //}
           
            
       // }
    }
    
   
    
   
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        // Session["ctrl"] = pnlContents;
        //  Control ctrl = (Control)Session["ctrl"];
        //   PrintWebControl(ctrl);
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
        string attachment = "attachment; filename=daywisesummary.xls";
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