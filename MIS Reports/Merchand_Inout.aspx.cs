using Bus_EReport;
using DBase_EReport;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MIS_Reports_Merchand_Inout : System.Web.UI.Page
{
    static string div_code = string.Empty;
    static string sf_code = string.Empty;
    static string subdiv_code = string.Empty;
    static string FDate = string.Empty;
    static string TDate = string.Empty;
    static string Sfname = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        sf_code = Request.QueryString["sfcode"].ToString();
        subdiv_code = Request.QueryString["subdiv"].ToString();
        FDate = Request.QueryString["Fdate"].ToString();
        TDate = Request.QueryString["Tdate"].ToString();
        Sfname = Request.QueryString["sfname"].ToString();
		lblHead.Text = "Activity of Field Force :" + Sfname + "  " +"From "+ FDate +"  "+"To " + TDate;
    }
    [WebMethod]
    public static string GetRetailersIn_Out()
    {
        DataSet ds = new DataSet();
        ret sf = new ret();
        ds = sf.Get_RetailersIn_Out(div_code, sf_code, FDate, TDate);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }

    public class ret
    {
        public DataSet Get_RetailersIn_Out(string Division_code, string sf_code, string Fdate, string Tdate)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet dsSF = null;
            string strQry = "Exec getInOut_Retailer '" + Division_code + "','"+ sf_code + "','" + Fdate + "','" + Tdate + "'";
            try
            {
                dsSF = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;

            }
            return dsSF;
        }
    }
}