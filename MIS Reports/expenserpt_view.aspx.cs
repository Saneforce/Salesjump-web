using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using System.Web.Services;
using System.IO;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using DBase_EReport;
using Bus_EReport;
using DocumentFormat.OpenXml.Drawing;
using System.Web.UI.HtmlControls;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html;
using iTextSharp.text.html.simpleparser;
using System.Text;
using System.Net;
using iTextSharp.tool.xml;
using System.Drawing;

public partial class MIS_Reports_expenserpt_view : System.Web.UI.Page
{
    public static string divcode = string.Empty;
    public static string Sf_Code = string.Empty;
    public static string sfname = string.Empty;
    public static string subdiv_code = string.Empty;
    public static string exptype = string.Empty;
    public static string Yr = string.Empty;
    public static string Mnth = string.Empty;

    public static string mnthname = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        divcode = Session["div_code"].ToString();
        Sf_Code = Request.QueryString["SF_code"].ToString();
        sfname = Request.QueryString["SF_Name"].ToString();
        exptype = Request.QueryString["Type"].ToString();
        Mnth = Request.QueryString["Mnth"].ToString();
        Yr = Request.QueryString["Yr"].ToString();
        mnthname = Request.QueryString["MnthName"].ToString();
        lblHead.Text = "Expense Report-" + mnthname + Yr;

    }
    [WebMethod(EnableSession = true)]
    public static string expensedtls()
    {
        DataSet dsGV = new DataSet();
        Expense SF = new Expense();
        string div_code = HttpContext.Current.Session["div_code"].ToString();
        //string subdiv_code = HttpContext.Current.Session["subdivision_code"].ToString();
		if(DBase_EReport.Global.ExpenseType!="2")
			dsGV = SF.new_getExpenseDets(Sf_Code, divcode, Mnth, Yr, exptype);
		else 
			dsGV = new_getExpenseDets(Sf_Code, divcode, Mnth, Yr, exptype);
        return JsonConvert.SerializeObject(dsGV.Tables[0]);
    }
	public static DataSet new_getExpenseDets(string sf_code, string div, string Mn, string Yr, string exptype)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;

            string strQry = "exec expensereport_dt_Periodic '" + sf_code + "','" + div + "'," + Mn + "," + Yr + ",'" + exptype + "'";

            try
            {
                dsSF = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsSF;
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



}