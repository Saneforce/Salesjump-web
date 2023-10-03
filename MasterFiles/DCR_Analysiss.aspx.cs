using Bus_EReport;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterFiles_DCR_Analysiss : System.Web.UI.Page
{
    public static string divcode = string.Empty;
    public static string sfcode = string.Empty;
    public static string fdt = string.Empty;
    public static string tdt = string.Empty;
    public static string[] ProdCode;
    public static DataTable DCR_Dayplan_dt = new DataTable();
    public static DataTable DCR_FForce_dt = new DataTable();
    public static DataTable DCR_Customer_dt = new DataTable();
    public static DataTable DCR_tourplan_dt = new DataTable();
    public static DataTable DCR_OrderDts_dt = new DataTable();
    public static DataTable DCR_Products_dt = new DataTable();
    public static DataTable DCR_ProdDts_dt = new DataTable();
    public static DataTable DCR_EventCap_dt = new DataTable(); 
    public static DataTable finaldt = new DataTable();
    public static DataTable DCR_All_dt = new DataTable();
    public static DataTable dt = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    [WebMethod]
    public static string getForms(string divcode,string sfcode)
    {
        DataSet ds = new DataSet();
        SalesForce Ad = new SalesForce();
        ds = Ad.SalesForceList(divcode, sfcode);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    [WebMethod]
    public static string getTableForms(string divcode, string sfname, string date ,string settinglist)
    {
        DataSet ds = new DataSet();
        SalesForce Ad = new SalesForce();
        ds = Ad.SalesForceList(divcode, sfname);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    [WebMethod]
    public static string savetemplate(string divcode, string tpname, string tplist)
    {
        string result;
        SalesForce Ad = new SalesForce();
        result = Ad.SaveTemplateList(divcode, tpname, tplist);
        return result;
    }

    [WebMethod]
    public static string getTemplate(string divcode)
    {
        DataTable dt = new DataTable();
        SalesForce Ad = new SalesForce();
        dt = Ad.GetDcrTemplateList(divcode);
        return JsonConvert.SerializeObject(dt);
    }

    [WebMethod] 
    public static string GetDCR_Products(string SF, string Div, string Mn, string Yr)
    {
        divcode = Div;
        sfcode = SF;
        fdt = Mn;
        tdt = Yr;
        DCR_Products_dt = null;
        SalesForce SFD = new SalesForce();
        DataSet ds = new DataSet();
        ds = SFD.GetDCR_Products_Details(SF, Div, Mn, Yr);
        DCR_Products_dt = ds.Tables[0];
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }

    [WebMethod]
    public static string GetDCR_ProdDts(string SF, string Div, string Mn, string Yr)
    {
        DCR_ProdDts_dt = null;
        SalesForce SFD = new SalesForce();
        DataSet ds = new DataSet();
        ds = SFD.GetDCR_ProdDts_Details(SF, Div, Mn, Yr);
        DCR_ProdDts_dt = ds.Tables[0];
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    
    [WebMethod]    
    public static string GetDCR_OrderDts(string SF, string Div, string Mn, string Yr)
    {
        DCR_OrderDts_dt = null;
        SalesForce SFD = new SalesForce();
        DataSet ds = new DataSet();
        ds = SFD.GetDCR_OrderDts_Details(SF, Div, Mn, Yr);
        DCR_OrderDts_dt = ds.Tables[0];
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    [WebMethod]
    public static string GetDCR_EventCap(string SF, string Div, string Mn, string Yr)
    {
        DCR_EventCap_dt = null;
        SalesForce SFD = new SalesForce();
        DataSet ds = new DataSet();
        ds = SFD.GetDCR_EventCap_Details(SF, Div, Mn, Yr);
        DCR_EventCap_dt = ds.Tables[0];
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    [WebMethod]
    public static string GetDCR_FForce(string SF, string Div)
    {
        DCR_FForce_dt = null;
        SalesForce SFD = new SalesForce();
        DataSet ds = new DataSet();
       
        ds = SFD.GetDCR_FForce_Details(SF, Div);
        DCR_FForce_dt = ds.Tables[0];
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    [WebMethod]
    public static string GetDCR_Dayplan(string SF, string Div, string Mn, string Yr)
    {
        DCR_Dayplan_dt = null;
        SalesForce SFD = new SalesForce();
        DataSet ds = new DataSet();        
        ds = SFD.GetDCR_Dayplan_Details(SF, Div, Mn, Yr);
        DCR_Dayplan_dt = ds.Tables[0];
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    [WebMethod]
    public static string GetDCR_Customer(string SF, string Div, string Mn, string Yr)
    {
        DCR_Customer_dt = null;
        SalesForce SFD = new SalesForce();
        DataSet ds = new DataSet();
        ds = SFD.GetDCR_Customer_Details(SF, Div, Mn, Yr);
        DCR_Customer_dt = ds.Tables[0];
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    
    [WebMethod]
    public static string GetDCR_tourplan(string SF, string Div, string Mn, string Yr)
    {
        DCR_tourplan_dt = null;
        SalesForce SFD = new SalesForce();
        DataSet ds = new DataSet();
        ds = SFD.GetDCR_tourplan_Details(SF, Div, Mn, Yr);
        DCR_tourplan_dt = ds.Tables[0];
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    
    [WebMethod]
    public static string[] uniqueFilter( string key )
    {
        string[] F_uniq_column;
        DataView dv;
        DataTable dt;

        dv = new DataView();
        dt = new DataTable();
        dv = null;
        dt = null;
        F_uniq_column = new string[] { };

        if (key == "Sf_Name" || key == "Sf_Joining_Date" || key == "Reporting_Sf" || key == "Reporting" || key == "Designation")
        {
            dv = new DataView(DCR_FForce_dt);
            dt = DCR_FForce_dt;
        }            
        if(key == "Cust_Code" || key == "Cust_Name" || key == "Cust_Spec" || key == "Cust_Cls" || key == "Cust_Addr" || key == "dt")
        {
            dv = new DataView(DCR_Customer_dt);
            dt = DCR_Customer_dt;
        }
        if (key == "Wtype" || key == "ClstrName" || key == "remarks")
        {
            dv = new DataView(DCR_Dayplan_dt);
            dt = DCR_Dayplan_dt;
        }
        if(key == "Imgurl" || key == "Activity_Report_Code")
        {
            dv = new DataView(DCR_EventCap_dt);
            dt = DCR_EventCap_dt;
        }
        if (key == "Activity_Remarks" || key ==  "Activity_Date" || key == "OrderTyp" || key == "POB_Value" || key == "Session" || key == "latlong" || key == "net_weight_value" || key == "stockist_name")
        {
            dv = new DataView(DCR_OrderDts_dt);
            dt = DCR_OrderDts_dt;
        }
        if (key == "Route")
        {
            dv = new DataView(DCR_tourplan_dt);
            dt = DCR_EventCap_dt;
        }
        if (key == "Product_Name" || key == "Product_Code")
        {
            dv = new DataView(DCR_ProdDts_dt);
            dt = DCR_ProdDts_dt;
        }
        if (dt != null)
        {
            F_uniq_column = dt.Columns.Contains(key) ? ((dv.ToTable(true, key)).Rows.OfType<DataRow>().Select(k => k[0].ToString()).ToArray()) : F_uniq_column;
        }
        
        return F_uniq_column;
    } 
}
