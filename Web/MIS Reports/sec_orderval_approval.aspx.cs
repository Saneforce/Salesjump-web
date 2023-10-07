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
using DBase_EReport;

public partial class MIS_Reports_sec_orderval_approval : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    [WebMethod]
    public static string getfilldtl()
    {
        Stockist dv = new Stockist();
        DataTable ds = new DataTable();
        string sf_code = HttpContext.Current.Session["sf_code"].ToString();
        string Div_Code = HttpContext.Current.Session["div_code"].ToString();
        SqlConnection con = new SqlConnection(Globals.ConnString);
        con.Open();
        SqlCommand cmd = new SqlCommand("exec secordervalconfirmation_approval '" + sf_code + "','" + Div_Code + "'", con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(ds);
        con.Close();
        return JsonConvert.SerializeObject(ds);

    }
    [WebMethod(EnableSession = true)]
    public static string Approvaldata(string SF_Code, string dates)
    {
        string err = "";
        int iReturn = -1;

        try
        {
            approval hod = new approval();
            string div_code = HttpContext.Current.Session["div_code"].ToString();
            string sf = HttpContext.Current.Session["sf_code"].ToString();
            iReturn = hod.secorderval_Approval(div_code, SF_Code, dates, "Appr");
            if (iReturn > 0)
            {
                err = "Sucess";
            }

        }
        catch (Exception ex)
        {
            err = "Error";
        }
        return err;
    }
    [WebMethod(EnableSession = true)]
    public static string RejectData(string SF_Code, string dates)
    {
        string err = "";
        int iReturn = -1;
        approval hod = new approval();
        try
        {

            string div_code = HttpContext.Current.Session["div_code"].ToString();
            string sf = HttpContext.Current.Session["sf_code"].ToString();
            iReturn = hod.secorderval_Approval(div_code, SF_Code, dates, "Rej");

            if (iReturn > 0)
            {
                err = "Sucess";
            }
            else
            {
                err = "Sucess";
            }

        }
        catch (Exception ex)
        {
            err = "Error";
        }
        return err;
    }
    public class approval
    {
        public int secorderval_Approval(string div_code, string SF_Code, string dates, string Mode)
        {

            int iReturn = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();

                DataSet dsDcrMAin = null;
                string strQry = "EXEC secorderApproval_update '" + div_code + "','" + SF_Code + "','" + dates + "','" + Mode + "'";
                try
                {
                    dsDcrMAin = db.Exec_DataSet(strQry);

                    if (dsDcrMAin.Tables[0].Rows.Count > 0)
                    {
                        iReturn = 1;
                    }
                    else
                    {
                        iReturn = 0;
                    }

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }
    }
}