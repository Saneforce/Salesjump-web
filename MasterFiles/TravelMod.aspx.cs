using Bus_EReport;
using ClosedXML.Excel;
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
using System.Web.UI.WebControls;

public partial class MasterFiles_TravelMod : System.Web.UI.Page
{
    public static DataSet ds = new DataSet();
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    [WebMethod]
    public static string saveTravelMode(string data)
    {
        string msg = string.Empty;
        ExpenseEntry dss = new Bus_EReport.ExpenseEntry();
        ExpenseEntry.SaveTravelMode Data = JsonConvert.DeserializeObject<Bus_EReport.ExpenseEntry.SaveTravelMode>(data);   
        msg = dss.SaveNewTravelMode(Data);
        return msg;
    }

    //[WebMethod(EnableSession = true)]
    //public static string Getgrade()
    //{
    //    ExpenseEntry nt = new ExpenseEntry();
    //    string Div_code = HttpContext.Current.Session["div_code"].ToString();
    //    string Sf_Code = HttpContext.Current.Session["Sf_Code"].ToString();
    //    DataSet dsDetails = null;
    //    dsDetails = nt.getDesignationgroup_div(Div_code);
    //    return JsonConvert.SerializeObject(dsDetails.Tables[0]);
    //}
    [WebMethod]
    public static string getStates(string divcode)
    {
        SalesForce sf = new SalesForce();
        DataSet ds = sf.getAllSF_States(divcode);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    [WebMethod]
    public static string getdesign(string divcode)
    {
        SalesForce sf = new SalesForce();
        DataSet ds = sf.getAllSF_design(divcode);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }

    [WebMethod]
    public static string getStatewisePrice(string slno, string divcode)
    {
        ExpenseEntry sf = new ExpenseEntry();
        DataSet ds = getStatewisePricing(slno, divcode);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    [WebMethod]
    public static string getTravelModeFields(string divcode)
    {
        ExpenseEntry Ad = new ExpenseEntry();
        ds = Ad.getTravel_ModeFields(divcode);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }

    [WebMethod(EnableSession = true)]
    public static int SetTravelStatus(string SF, string stus)
    {
        ExpenseEntry ast = new ExpenseEntry();
        int iReturn = ast.DeActivateTravel(SF, stus);
        return iReturn;
    }
    protected void lnkDownload_Click(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("MOT");
        dt.Columns.Add("Meter Reading");        
        dt.Columns.Add("Driver");
        dt.Columns.Add("Allowance");
        dt.Columns.Add("Status");
       
        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            DataRow dtr = dt.NewRow();
            dtr["MOT"] = dr["MOT"].ToString();
            dtr["Meter Reading"] = dr["StEndNeed"].ToString();
            dtr["Driver"] = dr["DriverNeed"].ToString();
            dtr["Allowance"] = dr["Alw_Eligibilty"].ToString();
            dtr["Status"] = dr["Flag"].ToString();
            dt.Rows.Add(dtr);
        }

        using (XLWorkbook wb = new XLWorkbook())
        {
            wb.Worksheets.Add(dt, "Travel Mode Report");
            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=Travel_Mode_Report.xlsx");
            using (MemoryStream MyMemoryStream = new MemoryStream())
            {
                wb.SaveAs(MyMemoryStream);
                MyMemoryStream.WriteTo(Response.OutputStream);
                Response.Flush();
                Response.End();
            }
        }
    }
    public static DataSet getStatewisePricing(string slno, string divcode)
    {
        DB_EReporting db = new DB_EReporting();
        DataSet ds = null;
        string strQry = "exec getStatewiseFuel " + slno + "," + divcode + "";
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