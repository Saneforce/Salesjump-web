using System;
using System.Web;
using System.Data;
using Bus_EReport;
using System.Web.Services;
using Newtonsoft.Json;
using ClosedXML.Excel;
using System.IO;
using System.Data.SqlClient;


public partial class MIS_Reports_Rpt_Distributor_Closing_Stock : System.Web.UI.Page
{
    public static string sfCode = string.Empty;
    public static string Dist_Code = string.Empty;
    public static string div_code = string.Empty;
    public static string FDT = string.Empty;
    public static string TDT = string.Empty;
    public static string fieldforce = string.Empty;
    public static string subdiv = string.Empty;

    public static DataTable dt = new DataTable();
    
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            div_code = Session["div_code"].ToString();
            sfCode = Request.QueryString["sfCode"].ToString();
            Dist_Code = Request.QueryString["Dist_Code"].ToString();
            FDT = Request.QueryString["fdate"].ToString();
            TDT = Request.QueryString["tdate"].ToString();
            fieldforce = Request.QueryString["fieldforce_name"].ToString();
            subdiv = Request.QueryString["subdiv"].ToString();
            DateTime d1 = Convert.ToDateTime(FDT);
            DateTime d2 = Convert.ToDateTime(TDT);
            lblHead.Text = "Distributor_Closing_Stock From " + d1.ToString("dd-MM-yyyy") + " to " + d2.ToString("dd-MM-yyyy");
            lblsf_name.Text = fieldforce;
        }
        catch
        {
            div_code = Session["Division_Code"].ToString();
        }
     


    }
    [WebMethod]
    public static string Get_Closing_Stock_Details()
    {

        DataTable dt = new DataTable();

        string ConnectionString = Globals.ConnString;
        SqlConnection con = new SqlConnection(ConnectionString);
        //SqlConnection con = new SqlConnection(Globals.ConnString);
        con.Open();
        SqlCommand cmd = new SqlCommand("exec Get_Closing_Stock_Details '" + sfCode + "','" + div_code + "','" + Dist_Code + "','" + subdiv + "','" + FDT + "','" + TDT + "'", con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dt);
        con.Close();
        return JsonConvert.SerializeObject(dt);

    }
}