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

public partial class Stockist_Retailer_List : System.Web.UI.Page
{
    string sf_type = string.Empty;
    protected override void OnPreInit(EventArgs e)
    {
        base.OnPreInit(e);
        sf_type = Session["sf_type"].ToString();
        if (sf_type == "3")
        {
            this.MasterPageFile = "~/Master.master";
            //sf_code = Session["sf_code"].ToString();
        }
        else if (sf_type == "2")
        {
            this.MasterPageFile = "~/Master_MGR.master";
            //sf_code = Session["sf_code"].ToString();
        }
        else if (sf_type == "1")
        {
            this.MasterPageFile = "~/Master_MR.master";
            //sf_code = Session["sf_code"].ToString();
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    [WebMethod]
    public static string getRetailers(string divcode, string sfcode, string rtcode)
    {
        if (rtcode != "0")
        {
            DataSet ds = new DataSet();
            SqlConnection con = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("exec getRetRouteMGR '" + sfcode + "','" + divcode + "','" + rtcode + "'", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            con.Close();
            return JsonConvert.SerializeObject(ds.Tables[0]);
        }
        else
        {
            ListedDR sf = new ListedDR();
            DataSet dtUserList = new DataSet();
            dtUserList = sf.getRetailerMGR(divcode, sfcode);
            return JsonConvert.SerializeObject(dtUserList.Tables[0]);
        }
    }
    public class rout
    {
        public string rtCode { get; set; }
        public string rtName { get; set; }
    }

    [WebMethod(EnableSession = true)]
    public static rout[] getRoute(string divcode, string sf)
    {
       string strQry = string.Empty;
        DataSet dsTerritory = new DataSet();
        //SqlConnection con = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString);
        //con.Open();
        //SqlCommand cmd = new SqlCommand("exec getRouteMGRdist '" + sf + "','" + divcode + "'", con);
        DBase_EReport.DB_EReporting db = new DBase_EReport.DB_EReporting();
        string sqlQry = "exec getRouteMGRdist '" + sf + "','" + divcode + "'";
        dsTerritory = db.Exec_DataSet(sqlQry);
        //SqlDataAdapter da = new SqlDataAdapter(cmd);
        //da.Fill(dsTerritory);
        //con.Close();

        List<rout> lrrt = new List<rout>();
        foreach (DataRow rw in dsTerritory.Tables[0].Rows)
        {
            rout robj = new rout();
            robj.rtCode = rw["Territory_Code"].ToString();
            robj.rtName = rw["Territory_Name"].ToString();
            lrrt.Add(robj);
        }
        return lrrt.ToArray();
    }

    public class sfMGR
    {
        public string sfname { get; set; }
        public string sfcode { get; set; }
    }

    [WebMethod(EnableSession = true)]
    public static int SetNewStatus(string SF, string stus)
    {
        Stockist dv = new Stockist();
        int iReturn = dv.DeActivate1(SF, stus);
        return iReturn;
    }
}