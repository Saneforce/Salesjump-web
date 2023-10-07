using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;



using System.Data;
using Bus_EReport;

using System.Web.Services;
using System.Data.SqlClient;
using Newtonsoft.Json;

using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Web.Script.Serialization;
using System.IO;
using System.Text;
using System.Configuration;
using System.Globalization;

public partial class MasterFiles_Supplier_Master : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    [WebMethod(EnableSession = true)]
    public static string SaveDate(string SName, string SContact, string SMobile, string SERP)
    {
        string div_code = "";
        try
        {
            div_code = HttpContext.Current.Session["div_code"].ToString();

        }
        catch
        {
            div_code = HttpContext.Current.Session["Division_Code"].ToString();
        }
        string Sub_div_code = HttpContext.Current.Session["Division_Code"].ToString();
        Stockist stk = new Stockist();


        int iReturn = stk.insert_Supplier(div_code.TrimEnd(','), Sub_div_code.TrimEnd(','), SName, SContact, SMobile, SERP, "0");
        if (iReturn > 0)
        {
            return "Sucess";
        }
        else
        {
            return "Error";
        }

    }
}