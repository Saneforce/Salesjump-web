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
using System.IO;
using System.Drawing;
using DBase_EReport;

public partial class MasterFiles_MR_Territory : System.Web.UI.Page
{
    #region "Declaration"
    static public string div_code;
    public string div_code1;
    public string sf_code = string.Empty;
    public string sf_type = string.Empty;
    #endregion
    protected override void OnPreInit(EventArgs e)
    {
        base.OnPreInit(e);
        sf_type = Session["sf_type"].ToString();
        if (sf_type == "3")
        {
            //this.MasterPageFile = "~/Master.master";
            this.MasterPageFile = "~/Master_DIS.master";
        }
        else if (sf_type == "2")
        {
            this.MasterPageFile = "~/Master_MGR.master";
        }
        else if (sf_type == "1")
        {
            this.MasterPageFile = "~/Master_MR.master";
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {

        try
        {
            div_code = Session["div_code"].ToString();
            div_code1 = Session["div_code"].ToString();
            sf_code = Session["Sf_Code"].ToString();
        }
        catch
        {
            div_code = Session["Division_Code"].ToString();
        }

        //Session["Div_code"] = div_code.ToString();
        
    }
    [WebMethod]
    public static string GetRoutes(string stk)
    {
        Territory Rut = new Territory();
        DataSet ds = getTerritory(stk);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }


    public static DataSet getTerritory(string Sf_Code)
    {
        DB_EReporting db_ER = new DB_EReporting();

        DataSet dsTerr = null;

        //string strQry = " SELECT a.Territory_Code,a.Route_Code,a.Territory_Name, a.Dist_Name,a.Target,a.Min_Prod," +
        //         " (select COUNT(b.ListedDrCode) " +
        //         " from Mas_ListedDr b where charindex(','+cast(a.Territory_Code as varchar)+',',','+cast(b.Territory_Code as varchar)+',')>0  and b.ListedDr_Active_Flag=0) ListedDR_Count, " +
        //         " (select COUNT(b.Dist_Name) from Mas_Territory_Creation b where b.Territory_Code=a.Territory_Code and " +
        //         "  b.Territory_Active_Flag=0) Chemists_Count " +
        //         " FROM  Mas_Territory_Creation a where (charindex(','+ cast('" + Sf_Code + "' as varchar)+',',','+a.Sf_Code+',')>0   or charindex(','+ cast('" + Sf_Code + "' as varchar)+',',','+a.Dist_Name+',')>0)  and a.Territory_Active_Flag=0" +
        //         " order by Territory_SNo";
        //strQry = " SELECT Territory_Code,Route_Code,Territory_Name,Dist_Name,Target,Min_Prod " +
        //         " FROM  Mas_Territory_Creation a where a.Sf_Code = '" + Sf_Code + "'  and a.Territory_Active_Flag=0 " +
        //         " order by Territory_Code";
       string  strQry = "SELECT a.Territory_Code,a.Route_Code,a.Territory_Name, a.Dist_Name,a.Target,a.Min_Prod,isnull(retcode,0) as ListedDR_Count,isnull(distcount,0) as Chemists_Count " +
                "FROM Mas_Territory_Creation a " +
                "left join (select Territory_Code, COUNT(b.ListedDrCode) retcode  from Mas_ListedDr b " +
                " where b.Division_Code = '"+ div_code + "' and  charindex(',' + cast('"+ Sf_Code + "' as varchar) + ',', ',' + b.Dist_Name + ',') > 0  group by Territory_Code) BB on BB.Territory_Code = a.Territory_Code " +
                "left join (select Territory_Code, COUNT(c.Dist_Name) distcount from Mas_Territory_Creation c " +
                "where c.Territory_Active_Flag= 0 and c.Division_Code= '" + div_code + "' and c.Dist_Name= '" + Sf_Code + "' group by Territory_Code, Route_Code) CC on CC.Territory_Code = a.Territory_Code " +
                "where(charindex(',' + cast('" + Sf_Code + "' as varchar) + ',', ',' + a.Sf_Code + ',') > 0 " +
                "or charindex(',' + cast('" + Sf_Code + "' as varchar) + ',', ',' + a.Dist_Name + ',') > 0)  and a.Territory_Active_Flag = 0 " +
                "group by a.Territory_Code,a.Route_Code,a.Territory_Name, a.Dist_Name,a.Target,a.Min_Prod,retcode,distcount";
        try
        {
            dsTerr = db_ER.Exec_DataSet(strQry);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return dsTerr;
    }

}


// FillReporting();