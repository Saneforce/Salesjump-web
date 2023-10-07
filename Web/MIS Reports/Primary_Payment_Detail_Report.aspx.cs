using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bus_EReport;
using System.Web.Services;
using System.Data;
using System.Data.SqlClient;
using Newtonsoft.Json;

public partial class MIS_Reports_Primary_Payment_Detail_Report : System.Web.UI.Page
{
    public static string div_code = string.Empty;
    public static DataSet ds1 = null;
    public static string[] statecd;
    public static DataSet dsState = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
    }

    [WebMethod(EnableSession = true)]
    public static string DivName()
    {
        string div_code = HttpContext.Current.Session["div_code"].ToString();
        div_code = div_code.TrimEnd(',');
        DataSet ds = new DataSet();
        SalesForce sd = new SalesForce();
        ds = sd.Getsubdivisionwise(div_code);
        return JsonConvert.SerializeObject(ds.Tables[0]);

    }

    [WebMethod(EnableSession = true)]
    public static string GetState()
    {
        string div_code = HttpContext.Current.Session["div_code"].ToString();
        string sfcode = HttpContext.Current.Session["Sf_Code"].ToString();
        Division dv = new Division();
        ds1 = dv.getStatePerDivision(div_code);
        if (ds1.Tables[0].Rows.Count > 0)
        {
            int i = 0;
            string state_cd = string.Empty;
            string sState = ds1.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
            statecd = sState.Split(',');
            foreach (string st_cd in statecd)
            {
                if (i == 0)
                {
                    state_cd = state_cd + st_cd;
                }
                else
                {
                    if (st_cd.Trim().Length > 0)
                    {
                        state_cd = state_cd + "," + st_cd;
                    }
                }
                i++;
            }

            State st = new State();
            dsState = st.getStateProd(state_cd);
        }
        return JsonConvert.SerializeObject(dsState.Tables[0]);

    }
    [WebMethod(EnableSession = true)]
    public static string getDistributor(string StateCode, string sub_div)
    {
        DataSet ds = new DataSet();
        SqlConnection con = new SqlConnection(Globals.ConnString);
        con.Open();
        SqlCommand cmd = new SqlCommand("select Stockist_Code,Stockist_Name from Mas_Stockist where division_code='" + div_code + "' and State_Code='" + StateCode + "' and ( '" + sub_div + "'='0' or charindex(','+'" + sub_div + "'+',',','+subdivision_code +',')>0)", con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(ds);
        con.Close();
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }

    [WebMethod(EnableSession = true)]
    public static string Get_Primary_Payment_Details(string stockist_code)
    {
        string div_code = HttpContext.Current.Session["div_code"].ToString();
        div_code = div_code.TrimEnd(',');
        DataSet ds = new DataSet();
        SalesForce sd = new SalesForce();
		SqlConnection con = new SqlConnection(Globals.ConnString);
		SqlCommand cmd = new SqlCommand("EXEC Sp_get_diswise_primary_payment_Details '" + stockist_code + "','" + div_code + "'", con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(ds);
        con.Close();
      //  ds = sd.get_diswise_primary_payment_Details(stockist_code,div_code);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
}