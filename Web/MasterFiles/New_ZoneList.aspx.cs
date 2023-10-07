using System;
using System.Collections.Generic;
using System.Data;
using Bus_EReport;
using System.Web.Services;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System.Web;
using System.Configuration;

public partial class MasterFiles_New_ZoneList : System.Web.UI.Page
{
    string div_code;
    string sf_type = string.Empty;
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
    public static string getfillzone(string divcode)
    {
        Stockist dv = new Stockist();
        DataTable ds = new DataTable();

        string sWBConnectionString = Globals.ConnString;
        SqlConnection con = new SqlConnection(sWBConnectionString);
        //SqlConnection con = new SqlConnection(Globals.ConnString);
        con.Open();
        SqlCommand cmd = new SqlCommand("exec getzoneMaster '" + divcode + "'", con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(ds);
        con.Close();
        return JsonConvert.SerializeObject(ds);

    }
    [WebMethod]
    public static string deactivatezone(string zncode, string stat)
    {
        Stockist dv = new Stockist();
        int iReturn = dv.znDeActivate(zncode, stat);
        if (iReturn > 0)
        {
            return "Success";
        }
        else
        {
            return "Failure";
        }
    }
    protected void ExportToExcel(object sender, EventArgs e)
    {

        DataTable dsProd1 = null;
        Stockist LstDoc = new Stockist();
        try
        {
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "Zonelist.xls"));
            Response.ContentType = "application/ms-excel";
            dsProd1 = LstDoc.getzonemaster(div_code);
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
}