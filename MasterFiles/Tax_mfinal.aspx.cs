using System;
using System.Web;
using Bus_EReport;
using System.Data;
using System.Web.Services;
using Newtonsoft.Json;
using DBase_EReport;

public partial class MasterFiles_Tax_mfinal : System.Web.UI.Page
{
    public static string tax_code = string.Empty;
    public static string div_code = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        tax_code = Request.QueryString["tax_code"];

    }
    protected void ClearControls()
    {
        //comment.Text = "";
        //commenttype.Items.Insert(0, new ListItem("--Select--", "0"));
        //commenttype.SelectedItem.ToString() = "";
        //Request.Form["tdate"] = "";

    }
    [WebMethod(EnableSession = true)]
    public static string selecttaxdata(string Tax_Name,string Tax_Type, string Value)
    {
        string div_code;
        div_code = HttpContext.Current.Session["div_code"].ToString();
        adminsetup cp = new adminsetup();
        DataSet ds = cp.gettaxmaster(Tax_Name,Tax_Type, Value, div_code);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    [WebMethod]
    public static string inserttaxdata(string Tax_Name, string Tax_Type, string Value,string tax_code)
    {
        string id = string.Empty;
        string msg = "";
        //string Active_flag = "0";
        string date = System.DateTime.Now.ToString();
        DateTime created_date = Convert.ToDateTime(date);
        string div_code;
        div_code = HttpContext.Current.Session["div_code"].ToString();
        adminsetup cm = new adminsetup();
        int iReturn = cm.saveTax(Tax_Name, Tax_Type, Value, div_code,tax_code);
        if (iReturn > 0)
            {
                msg = "true";
            }
            else
            {
                msg = "false";
            }
        return msg;

    }
    [WebMethod]
    public static string getTax1(string divcode, string scode)
    {
        AdminSetup cp = new AdminSetup();
        DataSet ds = cp.gettaxid(divcode, scode);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
	public class adminsetup
    {
        public DataSet gettaxmaster(string Tax_Name, string Tax_Type, string Value, string div_code)
        {
            DB_EReporting db = new DB_EReporting();
            DataSet ds = null;
            string strQry = "select Tax_Name,Tax_Id,Tax_Type,Value,Division_Code from  Tax_Master where Tax_Name='" + Tax_Name + "'and Value='" + Value + "'and Tax_Type='" + Tax_Type + "'and Division_Code='" + div_code + "'";
            try
            {
                ds = db.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
		 public int saveTax(string Tax_Name, string Tax_Type, string Value, string div_code, string tax_code)
            {
                DB_EReporting db = new DB_EReporting();
                int ds = -1;
                string strQry = " exec Save_Tax'" + Tax_Name + "','" + Tax_Type + "','" + Value + "','" + div_code + "','" + tax_code + "' ";
                try
                {
                    ds = db.ExecQry(strQry);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                return ds;
            }
    }
}

