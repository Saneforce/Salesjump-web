using Bus_EReport;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using DBase_EReport;


public partial class MasterFiles_Admin_Menu_Permission : System.Web.UI.Page
{
    public static string division_code = string.Empty;
	string sf_type = string.Empty;
	protected override void OnPreInit(EventArgs e)
    {
        base.OnPreInit(e);
        sf_type = Session["sf_type"].ToString();
        if (sf_type == "3")
        {
            this.MasterPageFile = "~/Master.master";
        }
        else if (sf_type == "2")
        {
            this.MasterPageFile = "~/Master_MGR.master";
        }
        else if (sf_type == "1")
        {
            this.MasterPageFile = "~/Master_MR.master";
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    [WebMethod(EnableSession = true)]
    public static List<ListItem> bindadmn(string comnm)
    {
        MenuCreation mc = new MenuCreation();
        List<ListItem> adminnm = new List<ListItem>();
        DataSet ds = new DataSet();
        ds = mc.getadminName_mashoid(comnm);
        foreach (DataRow row in ds.Tables[0].Rows)
        {
            adminnm.Add(new ListItem
            {
                Value = row["HO_ID"].ToString(),
                Text = row["User_Name"].ToString(),

            });

        }
        return adminnm;
    }
    [WebMethod(EnableSession = true)]
   public static string findmenuids(string adname)
    {
        MenuCreation mc1 = new MenuCreation();


        DataSet dsProd = mc1.getselids(adname);
        return JsonConvert.SerializeObject(dsProd.Tables[0]);
    }
	public DataSet getselids(string comname)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet ds = null;
            try
            {
                string strQry = "select Cust_Name Comp_Name,(','+Menu_IDs+',') Menu_IDs from Master..Mas_Customers where Cust_DivID='" + comname + "'";
                ds = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
    [WebMethod(EnableSession = true)]
    public static string findhomenuids(string adname)
    {
        MenuCreation mc1 = new MenuCreation();


        DataSet dsProd = mc1.admingethoselids(adname);
        return JsonConvert.SerializeObject(dsProd.Tables[0]);
    }
    [WebMethod(EnableSession = true)]
    public static List<ListItem> bindcomdrop(string divcode)
    {
        MenuCreation mc = new MenuCreation();
        List<ListItem> comname = new List<ListItem>();
        DataSet ds = new DataSet();
        ds = mc.getComNamebydiv(divcode);
        foreach (DataRow row in ds.Tables[0].Rows)
        {
            comname.Add(new ListItem
            {
                Value = row["Division_Code"].ToString(),
                Text = row["Division_Name"].ToString(),

            });

        }
        return comname;
    }
    [WebMethod(EnableSession = true)]
    public static int savedata(int adid, string adname, string arr)
    {
        int getreturn;
        MenuCreation mc = new MenuCreation();
        getreturn = mc.AddMenuPermissionValues_mashoid(adid, adname, arr); 

        if (getreturn > 0)
        {

        }

        return getreturn;

    }
    public class Details
    {
        public int Menu_ID { get; set; }
        public string Menu_Name { get; set; }
        public string Menu_Type { get; set; }
        public string Parent_Menu { get; set; }
        public string lvl { get; set; }
        public string MMnu { get; set; }
    }

    [WebMethod(EnableSession = true)]
    public static Details[] display(string divcode)
    {
        MasterFiles_Admin_Menu_Permission mc1 = new MasterFiles_Admin_Menu_Permission();
        DataTable dt1 = new DataTable();
        dt1 = mc1.selgetMenuBycompany(divcode);
        List<Details> details = new List<Details>();
        foreach (DataRow row1 in dt1.Rows)
        {
            Details dt = new Details();
            dt.Menu_ID = Convert.ToInt32(row1["Menu_ID"]);
            dt.Menu_Name = row1["Menu_Name"].ToString();
            dt.Menu_Type = row1["Menu_Type"].ToString();
            dt.Parent_Menu = row1["Parent_Menu"].ToString();
            //dt.MMnu = row1["MMnu"].ToString();
            //dt.lvl = row1["lvl"].ToString();
            details.Add(dt);

        }

        return details.ToArray();
    }
	public DataTable selgetMenuBycompany(string divcode)
	{
		DB_EReporting db_ER = new DB_EReporting();
		DataSet ds = null;

		try
		{

			string strQry = "exec sel_get_companymenus " + divcode + "";
			ds = db_ER.Exec_DataSet(strQry);

		}
		catch (Exception ex)
		{
			throw ex;
		}

		return ds.Tables[0];
	}
}