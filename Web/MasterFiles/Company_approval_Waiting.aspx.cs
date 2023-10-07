using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
using DBase_EReport;
using System.Configuration;
using System.Web.Services;
using Newtonsoft.Json;
using System.Data.SqlClient;

public partial class MasterFiles_Company_approval_Waiting : System.Web.UI.Page
{
    string div_code = string.Empty;
    string Comp_id = string.Empty;
    string Company_Name = string.Empty;
    string sf_type = string.Empty;
    DataSet dsCompanylist = null;  
    
    private string strConn = ConfigurationManager.ConnectionStrings["MasterDB"].ToString();
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

        if (sf_type == "3")
        {
            div_code = Session["div_code"].ToString();
        }
        else
        {
            div_code = Session["div_code"].ToString();
        }
        if (div_code.Contains(','))
        {
            div_code = div_code.Remove(div_code.Length - 1);
        }
        
    }
    [WebMethod]
    public static string GetList(string divcode)
    {
        
        DataSet ds = getComplist(divcode );
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }

    public static DataSet getComplist(string divcode)
    {
        MasterFiles_Company_approval_Waiting db_ER = new MasterFiles_Company_approval_Waiting();

        DataSet dsSF = null;
         string strQry = "Select Comp_id,Comp_Name,Comp_Code,CCountry from mas_SiteRequest where isnull(CmpStatus,0)=0 order by cast(Comp_id as numeric)";
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
    public DataSet Exec_DataSet(string strQry)
    {
        DataSet ds_EReport = new DataSet();
        SqlDataAdapter da_EReport = new SqlDataAdapter();

        SqlConnection _conn = new SqlConnection(strConn);
        try
        {


            SqlCommand selectCMD = new SqlCommand(strQry, _conn);
            selectCMD.CommandTimeout = 120;

            da_EReport.SelectCommand = selectCMD;

            _conn.Open();

            da_EReport.Fill(ds_EReport, "Customers");

            // _conn.Close();
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            _conn.Close();
            da_EReport.Dispose();
            _conn.Dispose();
        }
        return ds_EReport;
    }

}