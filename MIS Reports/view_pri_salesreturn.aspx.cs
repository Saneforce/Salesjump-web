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

public partial class MIS_Reports_view_pri_salesreturn : System.Web.UI.Page
{
    public static string Div = string.Empty;
    public static string sf_type = string.Empty;
    public static string sfcode = string.Empty;
    public static string sfname = string.Empty;
    public static string subdiv = string.Empty;
    public static string FDT = string.Empty;
    public static string TDT = string.Empty;
    public DateTime rdt;
    public DateTime sdt;
    protected void Page_Load(object sender, EventArgs e)
    {
        Div = Session["div_code"].ToString();
        sfcode = Request.QueryString["SF_Code"].ToString();
        sfname = Request.QueryString["SF_Name"].ToString();
        FDT = Request.QueryString["Fdate"].ToString();
        TDT = Request.QueryString["Tdate"].ToString();
        subdiv = Request.QueryString["Sub_Div"].ToString();
        DateTime d1 = Convert.ToDateTime(FDT);
        DateTime d2 = Convert.ToDateTime(TDT);
        lblHead.Text = "Sales Return Entry Primary Order From " + d1.ToString("dd-MM-yyyy") + " to " + d2.ToString("dd-MM-yyyy");
        lblsf_name.Text = sfname;
    }

    [WebMethod]
    public static string getSFdets(string Div)
    {
        salpri SFD = new salpri();
        DataSet ds = SFD.getsalespridtl(sfcode, Div, FDT, TDT, subdiv);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
  
    public class salpri
    {
        public DataSet getsalespridtl(string sfcode, string DivCode, string fdt, string tdt, string subdiv)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            string strQry = "exec [salesreturn_priorder_dtls] '" + sfcode + "'," + DivCode.TrimEnd(',') + ",'" + fdt + "','" + tdt + "','" + subdiv + "'";

            try
            {
                dsAdmin = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsAdmin;
        }
        
    }
}