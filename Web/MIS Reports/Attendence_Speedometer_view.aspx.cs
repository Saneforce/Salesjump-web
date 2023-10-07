using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MIS_Reports_Attendence_Speedometer_view : System.Web.UI.Page
{
    public static string Div = string.Empty;
    public static string sf_type = string.Empty;
    public static string sfcode = string.Empty;
    public static string sfname = string.Empty;
    public static string subdiv = string.Empty;
    public static string FDT = string.Empty;
    public static string TDT = string.Empty;
    public DateTime rdt;
    public DateTime sdt;    

    
    
    protected void Page_Load(object sender, EventArgs e)
    {
        Div = Session["div_code"].ToString();         
        sfcode = Request.QueryString["SF_Code"].ToString();
        sfname = Request.QueryString["SF_Name"].ToString();
        FDT = Request.QueryString["Fdate"].ToString();
        TDT = Request.QueryString["Tdate"].ToString();
        subdiv = Request.QueryString["Sub_Div"].ToString();
        DateTime d1 = Convert.ToDateTime(FDT);
        DateTime d2 = Convert.ToDateTime(TDT);
        lblHead.Text = "Attendance Speedo Meter Data From " + d1.ToString("dd-MM-yyyy") + " to " + d2.ToString("dd-MM-yyyy");
        lblsf_name.Text = sfname;
    } 
    
    [WebMethod]
    public static string getSFdata()
    {
        DataTable dt = null;
        string strQry = string.Empty;
        
        strQry = "exec getSpeedoMeterDetails '" + sfcode + "'," + Div + ",'" + FDT + "','" + TDT + "','" + subdiv + "'";
        dt = execQuery(strQry);

        return JsonConvert.SerializeObject(dt);        
    }
    [WebMethod]
    public static string getSFimg()
    {
        DataTable dt = null;
        string strQry = string.Empty;

        //strQry = "select Activity_Report_Code,CONVERT(date,Insert_Date_Time)Insert_Date_Time,Identification,imgurl from Activity_Event_Captures where Activity_Report_Code='" + sfcode + "' and CONVERT(date,Insert_Date_Time) between'" + FDT + "'and'"+ TDT +"'" ;
        strQry = "exec getSpeedoMeterpicDetails '" + sfcode + "'," + Div + ",'" + FDT + "','" + TDT + "'";
        dt = execQuery(strQry);

        return JsonConvert.SerializeObject(dt);
    }
    public static DataTable execQuery(string strQry)
    {
        DataTable dt = null;
        DBase_EReport.DB_EReporting db_ER = new DBase_EReport.DB_EReporting();

        try
        {
            dt = db_ER.Exec_DataTable(strQry);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return dt;
    }
}