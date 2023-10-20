using DBase_EReport;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterFiles_Asset_Request_creation : System.Web.UI.Page
{
    public static string div_code = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
    }
    [WebMethod]
    public static string getAllcategory()
    {
        DB_EReporting db_ER = new DB_EReporting();
        DataSet ds = new DataSet();
        ds = db_ER.Exec_DataSet("select category_Id,category_Name from Mas_Asset_Category where division_code='"+ div_code + "' and active_flag=0");
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    [WebMethod]
    public static string getAllmodel()
    {
        DB_EReporting db_ER = new DB_EReporting();
        DataSet ds = new DataSet();
        ds = db_ER.Exec_DataSet("select Model_Id,Model_Name,category_Id from Mas_Asset_Model where division_code='" + div_code+"' and Model_Active_Flag=0");
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    [WebMethod]
    public static string getAllvendor()
    {
        DB_EReporting db_ER = new DB_EReporting();
        DataSet ds = new DataSet();
        ds = db_ER.Exec_DataSet("select Vendor_Id,Vendor_Name from Mas_Asset_Vendor where Division_Code='"+ div_code+"' and Vendor_Active_Flag=0");
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    [WebMethod]
    public static string getAlllocation()
    {
        DB_EReporting db_ER = new DB_EReporting();
        DataSet ds = new DataSet();
        ds = db_ER.Exec_DataSet("select Location_Id,Location_Name from Mas_Asset_Location where Division_Code='"+ div_code+"' and Location_Active_Flag=0");
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    [WebMethod]
    public static string SaveAssetRequest(string assetnam,string assetcode,string asstcat,string asstloc,string asststs,string brndnam,string asstmod,string )
    {
        DB_EReporting db_ER = new DB_EReporting();
        DataSet ds = new DataSet();
        ds = db_ER.Exec_DataSet("select Vendor_Id,Vendor_Name from Mas_Asset_Vendor where Division_Code='" + div_code + "' and Vendor_Active_Flag=0");
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
}