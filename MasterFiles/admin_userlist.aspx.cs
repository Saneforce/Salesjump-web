using Bus_EReport;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using System.Data.SqlClient;
using DBase_EReport;

public partial class MasterFiles_admin_userlist : System.Web.UI.Page
{
    string divcode = string.Empty;
    public static string division_code = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        divcode = Session["div_code"].ToString();
        if (!Page.IsPostBack)
        {
            bindcomdrop();

        }
    }
    public class bindyear
    {
        public string value { get; set; }
        public string text { get; set; }
    }
    [WebMethod(EnableSession = true)]
    public static bindyear[] BindDate()
    {
        yr tp = new yr();
        DataSet dsTP = new DataSet();
        dsTP = tp.Get_TP_Edit_Year();
        List<bindyear> sf = new List<bindyear>();
        if (dsTP.Tables[0].Rows.Count > 0)
        {
            for (int k = Convert.ToInt16(dsTP.Tables[0].Rows[0]["Year"]); k <= DateTime.Now.Year + 1; k++)
            {
                bindyear rt = new bindyear();
                rt.value = k.ToString();
                rt.text = k.ToString();
                sf.Add(rt);
            }
        }
        return sf.ToArray();
    }
    [WebMethod(EnableSession = true)]
    public static string bindcomdrop()
    {
		/*
        MenuCreation mc = new MenuCreation();
        DataSet ds = new DataSet();
        ds = mc.getComName();
		strQry = "select Division_Code,Division_Name from Mas_Division where Division_Active_Flag=0 ";
		ds = db_ER.Exec_DataSet(strQry);
		*/
            
        DataTable ds = new DataTable();
        SqlConnection con = new SqlConnection(Globals.MasterConnString);
        con.Open();
        SqlCommand cmd = new SqlCommand("select 100000+Cust_id ID,Cust_id Code,Cust_Name,Cust_Add1,Cust_Add2,Cust_City,Cust_Pincode,Cust_GSTN,Cust_DivID,Cust_DBName,Cust_Status,Create_Date,Deactivate_Date,Cus_SHName,Cus_Url,BillRange,ActiveUser,Rate,(select count(name) from sys.databases where name=Cust_DBName) ex from Mas_Customers order by  Cust_ID", con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(ds);
        con.Close();
		
		
        return JsonConvert.SerializeObject(ds);
    }
    [WebMethod] 
    public static string getfilldtl (string eyear)
    {
        Stockist dv = new Stockist();
        DataTable ds = new DataTable();
        SqlConnection con = new SqlConnection(Globals.MasterConnString);
        con.Open();
        SqlCommand cmd = new SqlCommand("exec billing_user_list '" + eyear + "'", con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(ds);
        con.Close();
        return JsonConvert.SerializeObject(ds);

    }
    [WebMethod] 
    public static string ChangeStatus (string id,string status)
    {
        Stockist dv = new Stockist();
        DataTable ds = new DataTable();
        SqlConnection con = new SqlConnection(Globals.MasterConnString);
        con.Open();
        SqlCommand cmd = new SqlCommand("exec UpdateCompanyStatus  '" + id + "','" + status +"'", con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(ds);
        con.Close();
        return JsonConvert.SerializeObject(ds);

    }
    [WebMethod] 
    public static string fatdetails(string divc, string month, string year)
    {
        Stockist dv = new Stockist();
        DataTable ds = new DataTable();
        SqlConnection con = new SqlConnection(Globals.MasterConnString);
        con.Open();
        SqlCommand cmd = new SqlCommand("exec [billing_user_list_details] '" + divc + "','" + month + "','" + year + "'", con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(ds);
        con.Close();
        return JsonConvert.SerializeObject(ds);

    }
    public class yr
    {
        public DataSet Get_TP_Edit_Year()
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            string strQry = "select max([Year]-1) as Year from Mas_Division";
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