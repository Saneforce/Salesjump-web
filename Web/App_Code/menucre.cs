using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Bus_EReport;
using System.Data;
using DBase_EReport;

/// <summary>
/// Summary description for menucre
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class menucre : System.Web.Services.WebService
{

    public menucre()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }
    public class Details
    {
        public int Menu_IDs { get; set; }
        public string Menu_Names { get; set; }
        public string Menu_Types { get; set; }
        public string Parent_Menus { get; set; }
        public string Link_Urls { get; set; }

        public string Menu_Icons { get; set; }
        public string lvls { get; set; }
        public string MMnu { get; set; }
        public int order_ids { get; set; }
        public int Dfault_Screens { get; set; }
        public int Tbar_ids { get; set; }

    }

    [WebMethod(EnableSession = true)]
    public Details[] getMenuCreationData()
    {
        string div_code = HttpContext.Current.Session["division_code"].ToString();
        div_code = div_code.TrimEnd(',');
        List<Details> det = new List<Details>();
        MenuCreation mc = new MenuCreation();
        DataTable dt1 = new DataTable();
        dt1 = mc.getMenuByParent("");

        foreach (DataRow row in dt1.Rows)
        {
            Details d = new Details();
            d.Menu_IDs = Convert.ToInt32(row["Menu_ID"]);
            d.Menu_Names = row["Menu_Name"].ToString();
            d.Menu_Types = row["Menu_Type"].ToString();
            d.Parent_Menus = row["Parent_Menu"].ToString();
            d.Link_Urls = row["Link_Url"].ToString();
            d.Menu_Icons = row["Menu_icon"].ToString();
            d.lvls = row["Parent_Menu"].ToString();
            d.MMnu = row["Parent_Menu"].ToString();
            d.order_ids = Convert.ToInt32(row["order_id"]);
            d.Dfault_Screens = Convert.ToInt32(row["Dfault_Screen"]);
                d.Tbar_ids = Convert.ToInt32(row["Tbar_id"]);

            det.Add(d);
        }

        return det.ToArray();
    }
    [WebMethod(EnableSession = true)]
    public Details[] getalldatafrommenupermission()
    {
        string div_code = HttpContext.Current.Session["division_code"].ToString();
        div_code = div_code.TrimEnd(',');
        List<Details> det = new List<Details>();
            MenuCreation mc = new MenuCreation();
            DataTable dt1 = new DataTable();
            dt1 = mc.getMenuCompany(div_code);
           
            foreach (DataRow row in dt1.Rows)
            {
                Details d = new Details();
                d.Menu_IDs = Convert.ToInt32(row["Menu_ID"]);
                d.Menu_Names = row["Menu_Name"].ToString();
                d.Menu_Types = row["Menu_Type"].ToString();
                d.Parent_Menus = row["Parent_Menu"].ToString();
                d.Link_Urls = row["Link_Url"].ToString();
                d.Menu_Icons = row["Menu_icon"].ToString();
                d.lvls = row["Parent_Menu"].ToString();
                d.MMnu = row["Parent_Menu"].ToString();
                d.order_ids = Convert.ToInt32(row["order_id"]);
                d.Dfault_Screens = Convert.ToInt32(row["Dfault_Screen"]);
                d.Tbar_ids = Convert.ToInt32(row["Tbar_id"]);

            det.Add(d);
            }

        return det.ToArray();
    }

    public DataTable getMenuCompany_Mas(string CmpID)
    {
        DB_EReporting db_ER = new DB_EReporting();
        DataTable ds = null;

        try
        {
            string strQry = "exec getCompanywiseMenus_1 " + CmpID + "";
            ds = db_ER.Exec_DataTable(strQry);
        }
        catch (Exception ex)
        {
            throw ex;
        }

        return ds;
    }


    [WebMethod(EnableSession = true)]
    public menu[] getmenubypermission(string SF)
    {
        DataSet ds = new DataSet();
        MenuCreation mc = new MenuCreation();
        List<menu> men = new List<menu>();
        ds = mc.MenuByPermission(SF);
        foreach (DataRow row in ds.Tables[0].Rows)
        {
            menu m = new menu();
            m.Menu_IDs = Convert.ToInt32(row["Menu_ID"]);
            m.Menu_Names = row["Menu_Name"].ToString();
            m.Menu_Types = row["Menu_Type"].ToString();
            m.Parent_Menus = row["Parent_Menu"].ToString();
            m.Link_Urls = row["Link_Url"].ToString();
            m.Menu_Icons = row["Menu_icon"].ToString();
            m.order_ids = Convert.ToInt32(row["order_id"]);
            m.Dfault_Screens = Convert.ToInt32(row["Dfault_Screen"]);
            m.Tbar_ids = Convert.ToInt32(row["Tbar_id"]);

            men.Add(m);
        }


        return men.ToArray();
    }

    public class menu
    {


        public int Menu_IDs { get; set; }
        public string Menu_Names { get; set; }
        public string Menu_Types { get; set; }
        public string Parent_Menus { get; set; }
        public string Link_Urls { get; set; }

        public string Menu_Icons { get; set; }
        public int order_ids { get; set; }
        public int Dfault_Screens { get; set; }
        public int Tbar_ids { get; set; }

    }
	  [WebMethod(EnableSession = true)] 
    public menu[] getalldatahoid(string SF,string mentyp)
    {
        string div_code = HttpContext.Current.Session["division_code"].ToString();
        div_code = div_code.TrimEnd(',');
        DataSet ds = new DataSet();
        MenuCreation mc = new MenuCreation();
        List<menu> men = new List<menu>();
        ds = mc.getMenuByParenthoid(SF, div_code, mentyp);
        foreach (DataRow row in ds.Tables[0].Rows)
        {
            menu m = new menu();
            m.Menu_IDs = Convert.ToInt32(row["Menu_ID"]);
            m.Menu_Names = row["Menu_Name"].ToString();
            m.Menu_Types = row["Menu_Type"].ToString();
            m.Parent_Menus = row["Parent_Menu"].ToString();
            m.Link_Urls = row["Link_Url"].ToString();
            m.Menu_Icons = row["Menu_icon"].ToString();
            m.order_ids = Convert.ToInt32(row["order_id"]);
            m.Dfault_Screens = Convert.ToInt32(row["Dfault_Screen"]);
            m.Tbar_ids = Convert.ToInt32(row["Tbar_id"]);
            men.Add(m);
        }


        return men.ToArray();
    }
}
