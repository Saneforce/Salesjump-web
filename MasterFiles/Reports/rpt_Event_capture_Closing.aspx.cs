using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using System.Web.Services;
using Bus_EReport;
using System.Data;

public partial class MasterFiles_Reports_rpt_Event_capture_Closing : System.Web.UI.Page
{
    public static string divCode = string.Empty;
    public static string date = string.Empty;
    public static string date1 = string.Empty;
    public static string sfcode = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {

        divCode = Session["div_code"].ToString();
        sfcode = Request.QueryString["sf_code"].ToString();
        date = Request.QueryString["FDate"].ToString();
        date1 = Request.QueryString["TDate"].ToString();
lblhead.Text = "Closing Stock From " +Convert.ToDateTime(date).ToString("dd/MM/yyyy") + " To " + Convert.ToDateTime(date1).ToString("dd/MM/yyyy") ;
    }
    [WebMethod]
    public static string GetList()
    {
        CallPlan sf = new CallPlan();
        string sd = null;
        DataSet ds = sf.EventClosing(divCode, sfcode, date);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    protected void ExportToExcel(object sender, EventArgs e)
    {
        Response.ClearContent();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "Closing Event Capture.xls"));
        Response.ContentType = "application/ms-excel";
        CallPlan sf = new CallPlan();
        DataTable dssalesforce1 = sf.EventClosingxl(divCode, sfcode, date);
        DataTable dt = dssalesforce1;
        string str = string.Empty;
        foreach (DataColumn dtcol in dt.Columns)
        {
            Response.Write(str + dtcol.ColumnName);
            str = "\t";
        }
        Response.Write("\n");
        foreach (DataRow dr in dt.Rows)
        {
            str = "";
            for (int j = 0; j < dt.Columns.Count; j++)
            {
                Response.Write(str + Convert.ToString(dr[j]));
                str = "\t";
            }
            Response.Write("\n");
        }
        Response.End();

    }
}