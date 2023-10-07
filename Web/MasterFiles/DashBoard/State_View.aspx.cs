using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Web.UI.HtmlControls;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Data;
using Bus_EReport;
using System.Data.SqlClient;
using System.Configuration;
using System.Drawing;
using System.ComponentModel;

public partial class Customer : System.Web.UI.Page
{
    string sfCode = string.Empty;
    string sfName = string.Empty;
    string Year = string.Empty;
    int count = 0;
    int Month = -1;
    SqlConnection con = new SqlConnection(Globals.ConnString);
    protected void Page_Load(object sender, EventArgs e)
    {

        sfName = Request.QueryString["Division_name"].ToString();
        sfCode = Request.QueryString["SF_Name"].ToString();
        Year = Request.QueryString["Year"].ToString();
        Month = Convert.ToInt32(Request.QueryString["Month"].ToString());
        count = Convert.ToInt32(Request.QueryString["Count"].ToString());
        System.Globalization.DateTimeFormatInfo mfi = new System.Globalization.DateTimeFormatInfo();
        string strMonthName = mfi.GetMonthName(Month).ToString();

        lblHead.Text = sfName + " " + "for" + " " + strMonthName + " " + Year;
        BindGrid();


    }
    private void BindGrid()
    {
        DataSet dsRemarks1 = new DataSet();
        DCR Dcr = new DCR();
       // dsRemarks1 = Dcr.get_DCRRemarks1(sfCode, Month, Year);

        if (dsRemarks1.Tables[0].Rows.Count > 0)
        {

            GridView1.DataSource = dsRemarks1;
            GridView1.DataBind();
            btnPrint.Visible = false;
            btnExcel.Visible = true;
            btnPDF.Visible = false;
            btnClose.Visible = true;

        }
        else
        {
            GridView1.DataSource = null;
            GridView1.DataBind();
            btnPrint.Visible = false;
            btnExcel.Visible = true;
            btnPDF.Visible = false;
            btnClose.Visible = true;

        }

    }


    protected void btnClose_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
    }



    protected void myDataList_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
           
           
            HiddenField Hidden_field = (HiddenField)e.Item.FindControl("HiddenField1");

            if (Hidden_field != null)
            {
                
                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    string num = Hidden_field.Value;
                    con.Open();
                    SqlCommand cmd = new SqlCommand("select (select COUNT(sf_code)as num from DCRMain_Temp where Sf_Code="+ num +" and MONTH(activity_date)="+ Month +" and YEAR(activity_date)="+ Year +")+(select COUNT(sf_code)as num from DCRMain_Trans  where Sf_Code="+ num +" and MONTH(activity_date)="+ Month +" and YEAR(activity_date)="+ Year +")as num ", con);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet dt = new DataSet();
                    da.Fill(dt);
                    //DataRowView drv = e.Item.DataItem as DataRowView;
                    //if (drv != null)
                    //{
                    //    string temp = "";
                       Label LblError = (Label)e.Item.FindControl("lblnum");
                    //    for (int i = 0; i < dt.Tables[0].Rows.Count; i++)
                    //    {

                    //        temp += dt.Tables[0].Rows[i][0].ToString();

                    //    }
                       LblError.Text = dt.Tables[0].Rows[0]["num"].ToString();
                    //}
                    


                }
                con.Close();
            }
            
            

        }
    }
    
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {


            HiddenField Hidden_field = (HiddenField)e.Row.FindControl("HiddenField1");

            if (Hidden_field != null)
            {
                string num = Hidden_field.Value;
                con.Open();
                SqlCommand cmd = new SqlCommand("select (select COUNT(sf_code)as num from DCRMain_Temp where Sf_Code='" + num + "' and MONTH(activity_date)=" + Month + " and YEAR(activity_date)=" + Year + ")+(select COUNT(sf_code)as num from DCRMain_Trans  where Sf_Code='" + num + "' and MONTH(activity_date)=" + Month + " and YEAR(activity_date)=" + Year + ")as num", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet dt = new DataSet();
                da.Fill(dt);
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView drv = e.Row.DataItem as DataRowView;
                    //if (drv != null)
                    //{
                        string temp = "";
                        Label LblError = (Label)e.Row.FindControl("lblnum");
                        //for (int i = 0; i < dt.Tables[0].Rows.Count; i++)
                       // {

                            //temp += dt.Tables[0].Rows[i][0].ToString();

                       // }
                        LblError.Text = dt.Tables[0].Rows[0][0].ToString();
                   // }
                    con.Close();


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
        string attachment = "attachment; filename=Export.xls";
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
    protected void btnPDF_Click(object sender, EventArgs e)
    {
        string strFileName = "rptTPView";
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

}
