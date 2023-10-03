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
using System.Web.UI.DataVisualization.Charting;

public partial class Reports_rptDoctorCategory : System.Web.UI.Page
{
    DataSet dsDoctor = null;
    string sf_code = string.Empty;
    string catg = string.Empty;
    string div = string.Empty;
    string cat_name = string.Empty;
    string sf_name = string.Empty;
    string type = string.Empty;
    string strSF_code = string.Empty;
    int iDRCatg = -1;
    int iTotal_FF = 0;
    int[] iTotal_Catg = new int[70];
    int tot_catg = 0;
    int i = -1;
    string sDRCatg_Count = string.Empty;
    string div_code = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
      //  div_code = Session["div_code"].ToString();
        div_code = Request.QueryString["div"].ToString();
        strSF_code = Session["Sf_Code_multiple"].ToString();
        if (!Page.IsPostBack)
        {
           
            if (Request.QueryString["sf_code"] != null)
            {
                sf_code = Request.QueryString["sf_code"].ToString();
            }
            else
            {
                sf_code = "-1";
            }

            if (Request.QueryString["sf_name"] != null)
                sf_name = Request.QueryString["sf_name"].ToString();

            if (Request.QueryString["cat_code"] != null)
            {
                catg = Request.QueryString["cat_code"].ToString();
            }
            else
            {
                catg = "-1";
            }

            if (Request.QueryString["cat_name"] != null)
                cat_name = Request.QueryString["cat_name"].ToString();

            type = Request.QueryString["type"].ToString();
            Session["div_code"] = Request.QueryString["div"].ToString();

            if ((type == "0") && (sf_code != "-1") && (catg != "-1"))
            {
                lblHead.Text = lblHead.Text + "(Rep: " + sf_name + " / Category: " + cat_name + ")";
            }
            else if ((type == "0") && (sf_code == "-1") && (catg != "-1"))
            {
                lblHead.Text = lblHead.Text + "(Category: " + cat_name + ")";
            }
            else if ((type == "0") && (sf_code != "-1") && (catg == "-1"))
            {
                lblHead.Text = lblHead.Text + "(Rep: " + sf_name + ")";
            }
            else if ((type == "1") && (sf_code != "-1") && (catg != "-1"))
            {
                lblHead.Text = lblHead.Text + "(Rep: " + sf_name + " / Speciality: " + cat_name + ")";
            }
            else if ((type == "1") && (sf_code == "-1") && (catg != "-1"))
            {
                lblHead.Text = lblHead.Text + "(Speciality: " + cat_name + ")";
            }
            else if ((type == "1") && (sf_code != "-1") && (catg == "-1"))
            {
                lblHead.Text = lblHead.Text + "(Rep: " + sf_name + ")";
            }
            else if ((type == "2") && (sf_code != "-1") && (catg != "-1"))
            {
                lblHead.Text = lblHead.Text + "(Rep: " + sf_name + " / Class: " + cat_name + ")";
            }
            else if ((type == "2") && (sf_code == "-1") && (catg != "-1"))
            {
                lblHead.Text = lblHead.Text + "(Class: " + cat_name + ")";
            }
            else if ((type == "2") && (sf_code != "-1") && (catg == "-1"))
            {
                lblHead.Text = lblHead.Text + "(Rep: " + sf_name + ")";
            }
            else if ((type == "3") && (sf_code != "-1") && (catg != "-1"))
            {
                lblHead.Text = lblHead.Text + "(Rep: " + sf_name + " / Qualification: " + cat_name + ")";
            }
            else if ((type == "3") && (sf_code == "-1") && (catg != "-1"))
            {
                lblHead.Text = lblHead.Text + "(Qualification: " + cat_name + ")";
            }
            else if ((type == "3") && (sf_code != "-1") && (catg == "-1"))
            {
                lblHead.Text = lblHead.Text + "(Rep: " + sf_name + ")";
            }

            if (Request.QueryString["mgr_code"] != null)
            {
                Doctor dc = new Doctor();
                dsDoctor = dc.getDoctorMgr_View(strSF_code, type, div_code);

                if (dsDoctor.Tables[0].Rows.Count > 0)
                {
                    grdDoctor.Visible = true;
                    grdDoctor.DataSource = dsDoctor;
                    grdDoctor.DataBind();
                }
                Chart1.Visible = false;

                //  BindBarChart();
            }
            else
            {
                Doctor dc = new Doctor();
                dsDoctor = dc.getDoctorCategory(sf_code, catg, type, strSF_code);

                if (dsDoctor.Tables[0].Rows.Count > 0)
                {
                    grdDoctor.Visible = true;
                    grdDoctor.DataSource = dsDoctor;
                    grdDoctor.DataBind();
                }
                Chart1.Visible = true;
                FillSalesForce();
                BindChart();
                // BindBarChart();
            }

            Exportbutton();
        }
        

    }

    protected void BindBarChart()
    {        
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();

        Doctor dr = new Doctor();
        ds = dr.getDoctorCategory_Chart(sf_code, type, strSF_code);

        dt = ds.Tables[0];

        //  adp.Fill(ds);
        // dt = ds.Tables[0];

        //string category = "";
        //decimal[] values = new decimal[dt.Rows.Count];
        //for (int i = 0; i < dt.Rows.Count; i++)
        //{
        //    category = category + "," + dt.Rows[i]["Doc_Cat_Name"].ToString();
        //    values[i] = Convert.ToDecimal(dt.Rows[i]["ListedDrCode"]);
        //}

        //BarChart1.CategoriesAxis = category.Remove(0, 1);
        //BarChart1.Series.Add(new AjaxControlToolkit.BarChartSeries { Data = values, BarColor = "#2fd1f9", Name = "Doctor count - Categorywise" });
   
      //   DataTable dt = new DataTable();  

       //  da.Fill(dt);  

         string[] x = new string[dt.Rows.Count];  
         decimal[] y = new decimal[dt.Rows.Count];  
         for (int i = 0; i < dt.Rows.Count; i++)  
         {  
             x[i] = dt.Rows[i][3].ToString();  
             y[i] = Convert.ToInt32(dt.Rows[i][1]);  
         }  

       //  BarChart1.Series.Add(new AjaxControlToolkit.BarChartSeries { Data = y });  
         BarChart1.CategoriesAxis = string.Join(",", x);
         BarChart1.Series.Add(new AjaxControlToolkit.BarChartSeries { Data = y, BarColor = "#2fd1f9", Name = "Doctor count - Categorywise" });
         BarChart1.ChartWidth = (x.Length * 100).ToString();  
         BarChart1.ChartHeight = (y.Length * 60).ToString();  

            



    }
    protected void BindChart()
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();

        Doctor dr = new Doctor();
        ds = dr.getDoctorCategory_Chart(sf_code, type, strSF_code);

        dt = ds.Tables[0];

        string[] x = new string[dt.Rows.Count];

        int[] y = new int[dt.Rows.Count];

        for (int i = 0; i < dt.Rows.Count; i++)
        {

            x[i] = dt.Rows[i][0].ToString();

            y[i] = Convert.ToInt32(dt.Rows[i][1]);

        }

        Chart1.Series[0].Points.DataBindXY(x, y);

        Chart1.Series[0].ChartType = SeriesChartType.Pie;

        Chart1.Series[0]["PieLabelStyle"] = "Disabled";

        Chart1.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = false;

        Chart1.Legends[0].Enabled = true;

    }

    protected void grdDoctor_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblDOB = (Label)e.Row.FindControl("lblDOB");
            if (lblDOB.Text == "01/Jan/1900")
            {
                lblDOB.Text = "";
            }
        }
        if (e.Row.RowType == DataControlRowType.Header)
        {
            Territory terr = new Territory();
            DataSet dsTerritory = new DataSet();
            dsTerritory = terr.getWorkAreaName(div_code);
            if (dsTerritory.Tables[0].Rows.Count > 0)
            {
                e.Row.Cells[10].Text = dsTerritory.Tables[0].Rows[0]["wrk_area_Name"].ToString();
            }
        }
    }

    private void FillSalesForce()
    {
        int tot_rows = 0;
        int tot_cols = 0;

        // Fetch the total columns for the table
        Doctor dr = new Doctor();
        if (type == "0")
        {
            dsDoctor = dr.getDocCat(Session["div_code"].ToString());
            if (dsDoctor.Tables[0].Rows.Count > 0)
            {
                tot_cols = dsDoctor.Tables[0].Rows.Count;
                ViewState["dsDoctor"] = dsDoctor;
            }
        }
        else if (type == "1")
        {
            dsDoctor = dr.getDocSpec(Session["div_code"].ToString());
            if (dsDoctor.Tables[0].Rows.Count > 0)
            {
                ViewState["dsDoctor"] = dsDoctor;
            }
        }
        else if (type == "2")
        {
            dsDoctor = dr.getDocClass(Session["div_code"].ToString());
            if (dsDoctor.Tables[0].Rows.Count > 0)
            {
                ViewState["dsDoctor"] = dsDoctor;
            }
        }
        else if (type == "3")
        {
            dsDoctor = dr.getDocQual(Session["div_code"].ToString());
            if (dsDoctor.Tables[0].Rows.Count > 0)
            {
                ViewState["dsDoctor"] = dsDoctor;
            }
        }
        CreateDynamicTable();
    }

    private void CreateDynamicTable()
    {
        dsDoctor = (DataSet)ViewState["dsDoctor"];
        TableRow tr_catg = new TableRow();

        //tr_catg.BackColor = System.Drawing.Color.Pink;

        //if (type == "0")
        //{
        //    dsDoctor = (DataSet)ViewState["dsDoctor"];
        //}

        foreach (DataRow dataRow in dsDoctor.Tables[0].Rows)
        {
            TableCell tc_catg_name = new TableCell();
            tc_catg_name.BorderStyle = BorderStyle.Solid;
            tc_catg_name.BorderWidth = 1;
            Literal lit_catg_name = new Literal();
            lit_catg_name.Text = "<center><b>" + dataRow["Doc_Cat_Name"].ToString() + "</b></center>";
            tc_catg_name.Controls.Add(lit_catg_name);
            tr_catg.Cells.Add(tc_catg_name);
        }

        tbl.Rows.Add(tr_catg);

        TableRow tr_det = new TableRow();
        iTotal_FF = 0;
        i = 0;
        foreach (DataRow dataRow in dsDoctor.Tables[0].Rows)
        {
            TableCell tc_catg_det_name = new TableCell();
            Literal lit_catg_det_name = new Literal();

            Doctor dr_cat = new Doctor();
            if (type == "0")
            {
                iDRCatg = dr_cat.getDoctorcount_Total(sf_code, dataRow["Doc_Cat_Code"].ToString(), strSF_code);
                lblCatg.Text = "Listed Doctor count - Categorywise";
            }
            else if (type == "1")
            {
                iDRCatg = dr_cat.getSpecialcount_Total(sf_code, dataRow["Doc_Cat_Code"].ToString(), strSF_code);
                lblCatg.Text = "Listed Doctor count - Specialitywise";
            }
            else if (type == "2")
            {
                iDRCatg = dr_cat.getClasscount_Total(sf_code, dataRow["Doc_Cat_Code"].ToString(), strSF_code);
                lblCatg.Text = "Listed Doctor count - Classwise";
            }
            else if (type == "3")
            {
                iDRCatg = dr_cat.getQualcount_Total(sf_code, dataRow["Doc_Cat_Code"].ToString(), strSF_code);
                lblCatg.Text = "Listed Doctor count - Qualificationwise";
            }

            if (iDRCatg == 0)
            {
                sDRCatg_Count = " - ";
            }
            else
            {
                sDRCatg_Count = iDRCatg.ToString();
            }

            lit_catg_det_name.Text = "<center>" + sDRCatg_Count + "</center>";

            tc_catg_det_name.BorderStyle = BorderStyle.Solid;
            tc_catg_det_name.VerticalAlign = VerticalAlign.Middle;
            tc_catg_det_name.BorderWidth = 1;
            tc_catg_det_name.Controls.Add(lit_catg_det_name);
            tr_det.Cells.Add(tc_catg_det_name);

            tbl.Rows.Add(tr_det);
        }
    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        try
        {
            Session["ctrl"] = pnlContents;
            Control ctrl = (Control)Session["ctrl"];
            PrintWebControl(ctrl);
        }
        catch (Exception ex)
        {

        }
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

    private void Exportbutton()
    {
        btnExcel.Visible = true;
        btnPDF.Visible = false;
        btnPrint.Visible = true;
        btnClose.Visible = true;
    }
    protected void btnExcel_Click(object sender, EventArgs e)
    {
        try
        {
            string Export = Title;
            string attachment = "attachment; filename=" + Export + ".xls";
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
        catch (Exception ex)
        {

        }
    }
    protected void btnPDF_Click(object sender, EventArgs e)
    {
        try
        {
            string strFileName = Title;
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
        catch (Exception ex)
        {

        }
    }
}