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
using System.Web.Services;
using Newtonsoft.Json;

public partial class MIS_Reports_ReportFinal : System.Web.UI.Page
{
    public static string divcode1 = string.Empty;
    public static string sfcode1 = string.Empty;
    public static string fdt = string.Empty;
    public static string tdt = string.Empty;
    public static string FrmDate = string.Empty;
    public static string ToDate = string.Empty;
    public static string divcode = string.Empty;
    public static string Sf_Code = string.Empty;
    public static string retailer_code = string.Empty;
    public static string cus_code = string.Empty;
    string sfname = string.Empty;
    string subdiv_code = string.Empty;
    string Yr = string.Empty;
    string Mnth = string.Empty;
    DataSet ff = new DataSet();
    DataSet ss = new DataSet();
    int gridcnt = 0;
    string sfID = string.Empty;
    string currentsfid = string.Empty;
    decimal subTotal1 = 0;
    decimal nettotal1 = 0;
    decimal total = 0;
    decimal nttotal = 0;
    int subTotalRowIndex = 0;
    decimal subTotal = 0;
    decimal nettotal = 0;
    string mnthname = string.Empty;
    Int64[] TQty;
    protected void Page_Load(object sender, EventArgs e)
    {
        //ListedDR listedDR = new ListedDR();
        //dsSF.getRetailerDetails
        //try
        //{
        //    Class1 cl = new Class1();
        //    string RetailerName = txtRetailerName.Text;
        //    string contactPerson = txtContactPerson.Text;
        //    //string contactNum = txtContactNo.Text;
        //    string Address = txtAddress.Text;
        //    cl.GetReport(RetailerName, contactPerson, Address);
        //}
        //catch (Exception ex)
        //{
        //    Response.Write(ex);
        //}

        //divcode = Session["div_code"].ToString();
        divcode = Session["div_code"].ToString();
        Sf_Code = Request.QueryString["FieldForce"].ToString();
        retailer_code = Request.QueryString["Retailer"].ToString();
        cus_code = Request.QueryString["Route"].ToString();       
        FrmDate = Request.QueryString["FromDate"].ToString();
        ToDate = Request.QueryString["EndDate"].ToString();
        FillSF();
        BindDetails();
       // sfname = Request.QueryString["SName"].ToString();

    }

    private void FillSF()
    {
        try
        {
            DataSet dsGV = new DataSet();
            SalesForce SF = new SalesForce();
            // ff = new DataSet();
            //ss = new DataSet();
            //dsGV = SF.getFieldDetails(Sf_Code, divcode, cus_code);
            dsGV = SF.getFieldDetails(retailer_code, cus_code);
            if (dsGV.Tables[0].Rows.Count > 0)
            {
                //dsGV.Tables[0].Columns.RemoveAt(0);
                //gridcnt = dsGV.Tables[0].Columns.Count;
                //gdprimary.DataSource = dsGV;
                //gdprimary.DataBind();
                // lblClient.Text = ds.Tables[0].Rows[0].Field<string>("Cname");
                lblRetailerName.Text = dsGV.Tables[0].Rows[0]["ListedDr_Name"].ToString();
                lblRetailerPerson.Text = dsGV.Tables[0].Rows[0]["Contact_Person_Name"].ToString();
                lblRetailerContact.Text = dsGV.Tables[0].Rows[0]["ListedDr_Mobile"].ToString();
                lblAddress.Text = dsGV.Tables[0].Rows[0]["ListedDr_Address1"].ToString();
                lblFromDate.Text = FrmDate;
                lblToDate.Text = ToDate;
                //lblRetailerName.Text = dsGV.Tables[0].Rows[0].Field<string>("ListedDr_Name");
            }
            else
            {
                //gdprimary.DataSource = null;
                //gdprimary.DataBind();
                Response.Write("<scipt>alert('Values not update')</script>");
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }

    }
    private void BindDetails()
    {

        try
        {
            DataSet dsGV = new DataSet();
            SalesForce SF = new SalesForce();
            dsGV = SF.GetFullReport(Sf_Code,divcode,FrmDate, ToDate,retailer_code);
            if (dsGV.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = dsGV;
                GridView1.DataBind();
            }

            else
            {
                //GridView1.DataSource = dsGV;
               // GridView1.DataBind();
                Response.Write("<scipt>alert('GridData not updated')</script>");
            }
        }
        catch(Exception ex)
        {
            Response.Write(ex.Message);
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
        string attachment = "attachment; filename=Expense_Report " + mnthname + "-" + Yr + ".xls";
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