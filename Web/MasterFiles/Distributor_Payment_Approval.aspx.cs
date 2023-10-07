using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
using System.Drawing.Imaging;
using System.Drawing;
using System.Data.SqlClient;
using System.Configuration;
using System.Windows.Forms;
using System.Web.UI.HtmlControls;
using System.Web.Services;
using Newtonsoft.Json;

public partial class MasterFiles_Distributor_Payment_Approval : System.Web.UI.Page
{
    public static string div = string.Empty;
    public static string sf_code = string.Empty;
	public static SqlConnection conn = new SqlConnection(Globals.ConnString);

    protected void Page_Load(object sender, EventArgs e)
    {
        div = Session["div_code"].ToString();
        sf_code = Session["sf_code"].ToString();
    }

    [WebMethod(EnableSession = true)]
    public static string get_distributor()
    {
        DataSet ds = new DataSet();
        SalesForce sf = new SalesForce();
        ds = sf.GetStockName_Cus1(div, sf_code);
        return JsonConvert.SerializeObject(ds.Tables[0]);

    }

    [WebMethod(EnableSession = true)]
    public static string Get_Pending_Payment_Approval()
    {
        DataSet ds = new DataSet();
        SalesForce sf = new SalesForce();
		SqlCommand cmd = new SqlCommand("EXEC sp_get_pen_pri_pay_details", conn);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(ds);                 
        return JsonConvert.SerializeObject(ds.Tables[0]);

    }
    [WebMethod(EnableSession = true)]
    public static int SetNewStatus(string SF ,string stus,string Order_Id)
    {
        SalesForce sf = new SalesForce();
		DataSet ds=new DataSet();
		SqlCommand cmd = new SqlCommand("EXEC Approval_Primary_Order '"+SF+"','"+stus+"','"+Order_Id+"' ", conn);
        int iReturn = Convert.ToInt32(cmd.ExecuteScalar());
        return iReturn;
    }



}