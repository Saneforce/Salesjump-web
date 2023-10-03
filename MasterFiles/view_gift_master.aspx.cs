using System;
using System.Collections.Generic;
using System.Data;
using Bus_EReport;
using System.Web.Services;
using Newtonsoft.Json;
using System.Data.SqlClient;
using DBase_EReport;

public partial class MasterFiles_view_gift_master : System.Web.UI.Page
{
    string div_code;
    string sf_type = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            div_code = Session["div_code"].ToString();
        }
        catch
        {
            div_code = Session["Division_Code"].ToString();
        }
    }
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
    [WebMethod]
    public static string getfillgft(string divcode)
    {
        Stockist dv = new Stockist();
        DataTable ds = new DataTable();
        SqlConnection con = new SqlConnection(Globals.ConnString);
        con.Open();
        SqlCommand cmd = new SqlCommand("exec getgiftMaster '" + divcode + "'", con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(ds);
        con.Close();
        return JsonConvert.SerializeObject(ds);

    }
    [WebMethod]
    public static string deactivategift(string arcode, string stat)
    {
        vgft dv = new vgft();
        int iReturn = dv.arDeActivate(arcode, stat);
        if (iReturn > 0)
        {
            return "Success";
        }
        else
        {
            return "Failure";
        }
    }
    public class vgft
    {
        public int arDeActivate(string arcode, string stat)
     {
        int iReturn = -1;
        try
        {
            DB_EReporting db = new DB_EReporting();

            string strQry = "exec gft_Deactivate '" + arcode + "'," + stat + " ";
            iReturn = db.ExecQry(strQry);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return iReturn;
    }
}
}