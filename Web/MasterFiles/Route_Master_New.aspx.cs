using System;
using System.Collections.Generic;
using System.Data;
using Bus_EReport;
using System.Web.Services;
using Newtonsoft.Json;
using System.Data.SqlClient;

public partial class MasterFiles_Route_Master_New : System.Web.UI.Page
{

    #region "Declaration"
    public static string div_code;
    public static string sf_type = string.Empty;
    public static string sf_code;
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
        sf_code = Session["sf_code"].ToString();
        try
        {
            div_code = Session["div_code"].ToString();
        }
        catch
        {
            div_code = Session["Division_Code"].ToString();
        }
    }

    [WebMethod]
    public static string getStates(string divcode)
    {
        Territory SFD = new Territory();
        DataSet ds = SFD.getRo_States(divcode);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }

    [WebMethod]
    public static string getHQ(string divcode, string Sstate)
    {
        Territory SFD = new Territory();
        DataSet ds = SFD.getSFHQ(divcode, Sstate);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }

    [WebMethod]
    public static string getSF(string divcode, string Hq)
    {
        Territory SFD = new Territory();
        DataSet ds = SFD.getSF_HQ(divcode, Hq);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }

    [WebMethod]
    public static string getRoute_Retailers(string divcode, string route_code)
    {
        Territory SFD = new Territory();
        DataSet ds = SFD.getRoute_Retailers(divcode, route_code);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }

    [WebMethod]
    public static string getRoutes(string divcode, string sf)
    {
        Territory SFD = new Territory();
        DataSet ds = SFD.getRouteDetails(divcode, sf);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }

    [WebMethod]
    public static string deactivateRoutes(string Territory_Code, string stat)
    {
        Territory Terr = new Territory();
        int iReturn = Terr.DeActivate(Territory_Code, stat);
        if (iReturn > 0)
        {
            return "Success";
        }
        else
        {
            return "Failure";
        }
    }

    [WebMethod(EnableSession = true)]
    public static string getSalesforce()
    {
        string strQry = string.Empty;
        DataSet dsTerritory = new DataSet();
        SqlConnection con = new SqlConnection(Globals.ConnString);
        con.Open();
        SqlCommand cmd = new SqlCommand("select Sf_Code,Sf_Name from Mas_Salesforce where CHARINDEX('," + div_code.TrimEnd(',') + ",',','+Division_Code+',')>0 and SF_Status<>2", con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dsTerritory);
        con.Close();
        return JsonConvert.SerializeObject(dsTerritory.Tables[0]);
    }

    [WebMethod(EnableSession = true)]
    public static string getStockist()
    {
        string strQry = string.Empty;
        DataSet dsTerritory = new DataSet();
        SqlConnection con = new SqlConnection(Globals.ConnString);
        con.Open();
        SqlCommand cmd = new SqlCommand("select Stockist_Code,Stockist_Name from Mas_Stockist where Division_Code=" + div_code.TrimEnd(',') + " and Stockist_Active_Flag=0", con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dsTerritory);
        con.Close();
        return JsonConvert.SerializeObject(dsTerritory.Tables[0]);
    }

    protected void ExportToExcel(object sender, EventArgs e)
    {
        DataTable dsProd1 = null;
        Territory LstDoc = new Territory();
        try
        {
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "Route.xls"));
            Response.ContentType = "application/ms-excel";
            dsProd1 = LstDoc.getTerritorydiv1_Ex(div_code);
            DataTable dt = dsProd1;
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
        catch (Exception ex)
        {
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('" + ex.Message + "');</script>");
        }
    }

    [WebMethod]
    public static string GetAdditionalRoute(string divcode, string ModuleId)
    {
        DataSet ds = new DataSet();
        AdminSetup Ad = new AdminSetup();
        ds = Ad.GetAdditionalRoute(divcode, ModuleId);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }

    [WebMethod]
    public static string GetCustomFormsFieldsColumns(string divcode, string ModuleId, string Sf)
    {
        DataSet ds = new DataSet();
        AdminSetup Ad = new AdminSetup();
        ds = Ad.GetCustomFormsFieldsColumns(divcode, ModuleId, Sf);

        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
}