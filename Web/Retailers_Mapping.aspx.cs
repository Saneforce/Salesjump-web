using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
using System.Globalization;
using Newtonsoft.Json;
using System.Web.Services;
using DBase_EReport;

public partial class Retailers_Mapping : System.Web.UI.Page
{
    static DataSet dsSalesForce = null;
    string SFCode = string.Empty;
    static string div_code = string.Empty;
    string sf_type = string.Empty;
    string sSFstr = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        SFCode = Session["SF_Code"].ToString();
        if (!Page.IsPostBack)
        {
            FillReporting();
        }

    }
    [WebMethod]
    public static string GetSFTaggedRetailers(string SFCode)
    {
        SalesForce sf = new SalesForce();
        dsSalesForce = sf.SFTaggedRetailers(SFCode, div_code);
        return JsonConvert.SerializeObject(dsSalesForce.Tables[0]);
    }
    [WebMethod]
    public static string RoutTaggedRetailers(string route, string div_code, string SFcode)
    {
        DB_EReporting db = new DB_EReporting();
        DataSet ds = db.Exec_DataSet("exec [routeret_wise_Mapping] '" + route + "','" + div_code + "','" + SFcode + "'");
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    [WebMethod]
    public static string allocateroute(string SFcode, string div_code)
    {
        DB_EReporting db = new DB_EReporting();
        DataSet ds = db.Exec_DataSet("Select Territory_Code,Territory_Name,Division_Code from Mas_Territory_Creation where charindex(','+cast('" + SFcode + "'  as varchar)+',',','+SF_Code+',')>0 and Division_Code='" + div_code + "' and Territory_Active_Flag=0 ");
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }

    //[WebMethod]
    //public static string rmSFTags(string SFCode)
    //{
    //    SalesForce sf = new SalesForce();
    //    dsSalesForce = sf.SFRMTaggedRetailers(SFCode, div_code);
    //    return "{'success':'true'}";
    //}
    [WebMethod]
    public static string rmallrSFTags(string SFCode, string roueco)
    {
        DB_EReporting db = new DB_EReporting();
        if(roueco != "0")
        {
            DataSet ds = db.Exec_DataSet("update c set StatFlag=1 from map_GEO_Customers c inner join (select ListedDrCode from Mas_ListedDr dr with(nolock) inner join Mas_Territory_Creation tc on tc.Territory_Code=dr.Territory_Code" +
                                    " where CHARINDEX(',' + '" + SFCode + "' + ',', ',' + dr.Sf_Code + ',') > 0 and dr.ListedDr_Active_Flag = 0 and dr.Territory_Code = '" + roueco + "' and tc.Division_Code='" + div_code + "')" +
                                    "d on Cust_Code = ListedDrCode ");
            return "{'success':'true'}";
        }
        else
        {
            DataSet ds = db.Exec_DataSet("update c set StatFlag=1 from map_GEO_Customers c inner join (select ListedDrCode from Mas_ListedDr dr with(nolock) inner join Mas_Territory_Creation tc on tc.Territory_Code=dr.Territory_Code" +
                                    " where CHARINDEX(',' + '" + SFCode + "' + ',', ',' + dr.Sf_Code + ',') > 0 and dr.ListedDr_Active_Flag = 0  and tc.Division_Code='" + div_code + "')" +
                                    "d on Cust_Code = ListedDrCode ");
            return "{'success':'true'}";
        }
        
    }
    private void FillReporting()
    {
        SalesForce sf = new SalesForce();
        dsSalesForce = sf.SalesForceList_Track(div_code, SFCode);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            selSF.DataTextField = "Sf_Name";
            selSF.DataValueField = "Sf_Code";
            selSF.DataSource = dsSalesForce;
            selSF.DataBind();
        }
        for (int il = 0; il < dsSalesForce.Tables[0].Rows.Count; il++)
        {
            sSFstr += "{\"id\":\"" + dsSalesForce.Tables[0].Rows[il]["Sf_Code"].ToString() + "\",\"pId\":\"" + dsSalesForce.Tables[0].Rows[il]["Reporting_To_SF"].ToString() + "\",\"name\":\"" + dsSalesForce.Tables[0].Rows[il]["Sf_Name"].ToString() + "\"}";

            if (il < dsSalesForce.Tables[0].Rows.Count - 1) sSFstr += ",";
        }
        string ScriptValues = "datas=[" + sSFstr + "];genTreeView();";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "DatasLoad", ScriptValues, true);

    }
}
