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
using System.Data.SqlClient;
using System.Transactions;

public partial class MasterFiles_RetailerTransfer : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    [WebMethod]
    public static string GetRouteDetails(string divcode)
    {
        DataTable routemaster = new DataTable();
        SqlConnection con = new SqlConnection(Globals.ConnString);
        con.Open();
        SqlCommand cmd = new SqlCommand("select Territory_Code,Territory_Name,Territory_SName from Mas_Territory_Creation where Territory_Active_Flag=0 and Division_Code='" + divcode + "' order by 2", con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da = new SqlDataAdapter(cmd);
        da.Fill(routemaster);
        con.Close();
        return JsonConvert.SerializeObject(routemaster);
    }

    [WebMethod]
    public static string saveTransfer(string Divcode, string fromRoute, string toRoute, string retailerData)
    {
        string msg = string.Empty;
        SqlConnection con = new SqlConnection(Globals.ConnString);
        using (var scope = new TransactionScope())
        {
            try
            {
                DataTable routemaster = new DataTable();
                con.Open();
                SqlCommand cmd = new SqlCommand("select SF_Code,Dist_Name from Mas_Territory_Creation where Territory_Code='" + toRoute + "'", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da = new SqlDataAdapter(cmd);
                da.Fill(routemaster);
                con.Close();
                string sfcode = string.Empty;
                string distname = string.Empty;
                if (routemaster.Rows.Count > 0)
                {
                    sfcode = routemaster.Rows[0]["SF_Code"].ToString();
                    distname = routemaster.Rows[0]["Dist_Name"].ToString();
                }
                string sql = "update Mas_ListedDr set sf_Code='" + sfcode + "',Dist_name='" + distname + "',Territory_Code='" + toRoute + "',LastUpdt_Date=GETDATE() where ListedDr_Active_Flag=0 and Division_Code='" + Divcode + "' and Territory_Code='" + fromRoute + "' and cast(ListedDrCode as varchar) in (" + retailerData.TrimEnd(',') + ")";
                con.Open();
                try
                {
                    SqlCommand CmdUpl = con.CreateCommand();
                    CmdUpl.CommandText = sql;
                    int sl = CmdUpl.ExecuteNonQuery();
                    if (sl > -1)
                    {
                        msg = "Success";
                    }
                    else
                    {
                        msg = "Failure";
                    }
                    con.Close();
                }
                catch (Exception ex)
                {
                    msg = "Failure";
                    con.Close();
                    throw ex;
                }
                scope.Complete();
                scope.Dispose();
            }
            catch (Exception ex)
            {
                msg = "Failure";
                con.Close();
                scope.Dispose();
                throw ex;
            }
        }
        return msg;
    }

    [WebMethod]
    public static string getRetailers(string divcode, string routecode)
    {

        DataTable routemaster = new DataTable();
        SqlConnection con = new SqlConnection(Globals.ConnString);
        con.Open();
        SqlCommand cmd = new SqlCommand("select ListedDrCode,ListedDr_Name from Mas_ListedDr where ListedDr_Active_Flag=0 and Division_Code='" + divcode + "' and Territory_Code='" + routecode + "' order by 2", con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da = new SqlDataAdapter(cmd);
        da.Fill(routemaster);
        con.Close();
        return JsonConvert.SerializeObject(routemaster);
    }
}