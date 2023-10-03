using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
using System.Data.SqlClient;
using System.Web.Services;
using System.Configuration;
using Newtonsoft.Json;
using System.Globalization;
using ClosedXML.Excel;
using System.Xml;
using System.IO;
using System.Data.OleDb;

public partial class MasterFiles_Reports_Secondary_Order : System.Web.UI.Page
{
    public string sf_type = string.Empty;
    public static string sf_code = string.Empty;
    public static string sub_division = string.Empty;
    protected void Page_PreInit(object sender, EventArgs e)
    {
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
        else if (sf_type == "4")
        {
            this.MasterPageFile = "~/Master_DIS.master";
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        sf_code = Session["sf_code"].ToString();
        sub_division = Session["sub_division"].ToString();
    }
    [WebMethod]
    public static string getStates(string divcode)
    {
        SalesForce SFD = new SalesForce();
        DataSet ds = SFD.getsubdiv_States(divcode, sf_code, sub_division);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }

    [WebMethod]
    public static string getDivision(string divcode)
    {
        DataSet ds = new DataSet();
        //SalesForce sd = new SalesForce();
        //ds = sd.Getsubdivisionwise(divcode, sub_division);
        ds = Getsubdivisionwise(divcode, sub_division);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }

    public static DataSet Getsubdivisionwise(string divcode, string subdiv = "0")
    {
        //DB_EReporting db_ER = new DB_EReporting();

        DataSet dsAdmin = new DataSet();
        string strQry = String.Empty;

        ////strQry = "select subdivision_code,subdivision_name from mas_subdivision where Div_Code='" + divcode + "' and SubDivision_Active_Flag=0 and charindex(','+cast(subdivision_code as varchar)+',',','+iif('" + subdiv + "'='0',cast(subdivision_code as varchar),'" + subdiv + "')+',')>0";
        strQry = " SELECT subdivision_code,subdivision_name FROM mas_subdivision ";
        strQry += " WHERE Div_Code = @divcode AND SubDivision_Active_Flag=0 ";
        strQry += " AND charindex(','+cast(subdivision_code as varchar)+',',','+iif(@subdiv='0',cast(subdivision_code as varchar),@subdiv)+',')>0 ";

        try
        {
            using (var con = new SqlConnection(Globals.ConnString))
            {
                using (var cmd = con.CreateCommand())
                {
                    cmd.CommandText = strQry;
                    cmd.Parameters.AddWithValue("@divcode", Convert.ToString(divcode));
                    cmd.Parameters.AddWithValue("@subdiv", Convert.ToString(subdiv));
                    cmd.CommandType = CommandType.Text;

                    SqlDataAdapter dap = new SqlDataAdapter();
                    dap.SelectCommand = cmd;
                    con.Open();
                    dap.Fill(dsAdmin);
                    con.Close();
                }
            }
            //dsAdmin = db_ER.Exec_DataSet(strQry);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return dsAdmin;

    }

    [WebMethod]
    public static string getFieldForce(string divcode)
    {
        SalesForce sd = new SalesForce();
        DataSet ds = new DataSet();
        ds = sd.SalesForceList(divcode, sf_code, sub_division);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
}