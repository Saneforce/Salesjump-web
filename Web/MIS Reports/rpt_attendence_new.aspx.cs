using DBase_EReport;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class MIS_Reports_rpt_attendence_new : System.Web.UI.Page
{
    public static string div_code = string.Empty;
    string statec = string.Empty;
    string statev = string.Empty;
    public static string sfCode = string.Empty;
    string sfname = string.Empty;
    public static string FMonth = string.Empty;
    public static string FYear = string.Empty;
    string imagepath = string.Empty;
    string subdiv_code = string.Empty;
    public string strFMonthName = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        sfname = Request.QueryString["sfname"].ToString();
        sfCode = Request.QueryString["sfcode"].ToString();
        imagepath = Request.QueryString["imgpath"].ToString();
        subdiv_code = Request.QueryString["subdiv"].ToString();
        statec = Request.QueryString["stcode"];
        statev = Request.QueryString["stval"];
        FMonth = Request.QueryString["FMonth"].ToString();
        FYear = Request.QueryString["FYear"].ToString();
        System.Globalization.DateTimeFormatInfo mfi = new System.Globalization.DateTimeFormatInfo();
        strFMonthName = mfi.GetMonthName(Convert.ToInt16(FMonth)).ToString().Substring(0, 3);
        lblHead.Text = "Attendance WorkTypewise View for the " + strFMonthName + " " + FYear + "-" + statev;


        lblsf_name.Text = sfname;
    }
    [WebMethod]
    public static string getUserdata()
    {
        loc SFD = new loc();
        DataSet ds = SFD.get_User_data();
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    [WebMethod]
    public static string getUserWTdata()
    {
        loc SFD = new loc();
        DataSet ds = SFD.get_Userwt_data();
        return JsonConvert.SerializeObject(ds);
    }
    [WebMethod]
    public static string gethints(string divcode)
    {
        loc SFD = new loc();
        DataSet ds = SFD.get_attend_hint(divcode);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    protected void btnExcel_Click(object sender, EventArgs e)
    {
        string attachment = "attachment; filename=Secondary_Order_View.xls";
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
    //protected void btnPrint_Click(object sender, EventArgs e)
    //{
    //    Session["ctrl"] = pnlContents;
    //    Control ctrl = (Control)Session["ctrl"];
    //    PrintWebControl(ctrl);
    //}
    //public static void PrintWebControl(Control ControlToPrint)
    //{
    //    StringWriter stringWrite = new StringWriter();
    //    System.Web.UI.HtmlTextWriter htmlWrite = new System.Web.UI.HtmlTextWriter(stringWrite);
    //    if (ControlToPrint is WebControl)
    //    {
    //        Unit w = new Unit(100, UnitType.Percentage);
    //        ((WebControl)ControlToPrint).Width = w;
    //    }
    //    Page pg = new Page();
    //    pg.EnableEventValidation = false;
    //    HtmlForm frm = new HtmlForm();
    //    pg.Controls.Add(frm);
    //    frm.Attributes.Add("runat", "server");
    //    frm.Controls.Add(ControlToPrint);
    //    pg.DesignerInitialize();
    //    pg.RenderControl(htmlWrite);
    //    string strHTML = stringWrite.ToString();
    //    HttpContext.Current.Response.Clear();
    //    HttpContext.Current.Response.Write(strHTML);
    //    HttpContext.Current.Response.Write("<script>window.print();</script>");
    //    HttpContext.Current.Response.End();

    //}
    public class loc
    {
        public DataSet get_User_data()
        {
            DB_EReporting db_ER = new DB_EReporting();
            string strQry = string.Empty;
            DataSet dsDivision = null;

                strQry = "exec [sp_getUser_atten] '" + div_code + "','"+ sfCode + "'";
      
            try
            {
                dsDivision = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsDivision;
        }
        public DataSet get_Userwt_data()
        {
            DB_EReporting db_ER = new DB_EReporting();
            string strQry = string.Empty;
            DataSet dsDivision = null;

            strQry = "exec [sp_Attend_WT] '" + div_code + "','" + FMonth + "','"+ FYear + "'";

            try
            {
                dsDivision = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsDivision;
        }
        public DataSet get_attend_hint(string divcode)
        {
            DB_EReporting db = new DB_EReporting();
            DataSet ds = null;
            string strQry = "select WType_SName,Wtype from vwmas_worktype_all where division_code='" + divcode + "' and Active_Flag=0 group by WType_SName,Wtype";
            try
            {
                ds = db.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
    }
}