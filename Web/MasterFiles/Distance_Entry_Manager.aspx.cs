using Bus_EReport;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterFiles_Distance_Entry_Manager : System.Web.UI.Page
{

    #region "Declaration"
    DataSet dsStockist = null;
    string Division_code = string.Empty;
    string sf_code = string.Empty;
    string sf_type = string.Empty;
    #endregion
    protected override void OnPreInit(EventArgs e)
    {
        base.OnPreInit(e);
        sf_type = Session["sf_type"].ToString();
        if (sf_type == "3")
        {
            this.MasterPageFile = "~/Master.master";
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

        sf_code = Session["SF_Code"].ToString();
        Division_code = Session["Div_code"].ToString();
        if (!Page.IsPostBack)
        {
            GetTerritoryName();
        }
    }
    private void GetTerritoryName()
    {
        SalesForce sk = new SalesForce();
        dsStockist = sk.Get_HyrSFList_All_Mangaer(Division_code, "0", sf_code);
        //dsStockist = execQuery(" Exec HyrSFList_All_Managers '" + Division_code + "','"+ subdivision_code +"','" + sf_code + "'");
        //dsStockist = sk.Get_HyrSFList_All_Mangaer(Division_code,sf_code);
        if (dsStockist.Tables[0].Rows.Count > 0)
        {
            ddlTerritoryName.DataTextField = "SF_Name";
            ddlTerritoryName.DataValueField = "SF_Code";

            ddlTerritoryName.DataSource = dsStockist;
            ddlTerritoryName.DataBind();
        }

    }

    [WebMethod]
    public static string GetRoutes(string SF)
    {
        Territorys Emp = new Territorys();
        DataSet ds = Emp.getRoutesByManager(SF);
        //DataSet ds = execQuery("EXEC GetTerritoryByManagerID '" + SF + "'");
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }

    [WebMethod]
    public static string SaveDistance(string SF, string xml)
    {
        Expense Exp = new Expense();
        xml = "<ROOT>" + xml + "</ROOT>";
        return Exp.SaveDistanceDetails(SF, xml);
    }
     
    public static DataSet execQuery(string strQry)
    {
        DataSet dsSF = null;
        DBase_EReport.DB_EReporting db_ER = new DBase_EReport.DB_EReporting();
          
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

}