using Bus_EReport;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using DBase_EReport;
public partial class Menu_Creation_Default2 : System.Web.UI.Page
{
    public static string division_code = string.Empty;
	public static string DbName = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
		//DbName = Session["DBName"].ToString();
        if (!Page.IsPostBack)
        {
            bindcomdrop();
       
        }
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
    public static Details[] display()
    {
        MenuCreation mc1 = new MenuCreation();
        DataTable dt1 = new DataTable();
        dt1 = getMenuByParent("");
        List<Details> details = new List<Details>();
        foreach (DataRow row1 in dt1.Rows)
        {
            Details dt = new Details();
            dt.Menu_ID = Convert.ToInt32(row1["Menu_ID"]);
            dt.Menu_Name = row1["Menu_Name"].ToString();
            dt.Menu_Type = row1["Menu_Type"].ToString();
            dt.Parent_Menu= row1["Parent_Menu"].ToString();
            //dt.MMnu = row1["MMnu"].ToString();
            //dt.lvl = row1["lvl"].ToString();
            details.Add(dt);
           
        }

        return details.ToArray();
    }
    public static DataTable getMenuByParent(string PMnuId, int lvl = 0, string MMnu = "")
    {
        DB_EReporting db_ER = new DB_EReporting();
        DataSet ds = null;
        DataTable dat = null;
        try
        {
            lvl = lvl + 1;
            if (MMnu == "") MMnu = PMnuId;
            string strQry = "exec GetFMCGMenusList '" + PMnuId + "'," + lvl + ",'" + MMnu + "'";
            ds = db_ER.Exec_DataSet(strQry);
            if (dat == null) dat = ds.Tables[0];//.Clone();

            //for (int il = 0; il < ds.Tables[0].Rows.Count; il++)
            //{

            //    dat.Rows.Add(ds.Tables[0].Rows[il].ItemArray);
            //    //getMenuByParent(ds.Tables[0].Rows[il]["Menu_ID"].ToString(), lvl, MMnu);
            //}
        }
        catch (Exception ex)
        {
            throw ex;
        }

        return dat;
    }

    [WebMethod(EnableSession = true)]
   public static List<ListItem> bindcomdrop()
    {
        Menu_Creation_Default2 mc = new Menu_Creation_Default2();
        List<ListItem> comname = new List<ListItem>();        
        DataSet ds = new DataSet();
        ds = mc.getComName(DbName);
        foreach (DataRow row in ds.Tables[0].Rows)
        {
            comname.Add(new ListItem
            {
                Value=row["Cust_DivID"].ToString(),
                Text = row["Cust_Name"].ToString(),

            });

        }
        return comname;
    }
    
    public DataSet getComName(string DBnm)
    {
        DB_EReporting db_ER = new DB_EReporting();
        DataSet ds = null;

        try
        {
            string strQry = "select Cust_DivID,Cust_Name from Master..Mas_Customers where Cust_Status=0 order by Cust_Name";
            ds = db_ER.Exec_DataSet(strQry);
        }

        catch (Exception ex)
        {
            throw ex;
        }

        return ds;

    }

    public class degnbind
    {
        public int desigcode { get; set; }
        public string designame { get; set; }
    }

    [WebMethod(EnableSession = true)]
    public static degnbind[] binddegndrop(string degnid)
    {
        MenuCreation mc1 = new MenuCreation();
        DataSet dt1 = new DataSet();
        dt1 = mc1.getdesignation(degnid);
        List<degnbind> details = new List<degnbind>();
        foreach (DataRow row in dt1.Tables[0].Rows)
        {
            degnbind d = new degnbind();
            d.desigcode = Convert.ToInt32(row["Designation_Code"]);
            d.designame = (row["Designation_Name"]).ToString();
            details.Add(d);
        }
        return details.ToArray();
    }
    [WebMethod(EnableSession = true)]
    public static string findmenuids(string comname) 
    {
        Menu_Creation_Default2 mc1 = new Menu_Creation_Default2();
		DB_EReporting db_ER = new DB_EReporting();
		DataSet ds = null; DataSet dsProd =null;
		try
		{
            string strQry = "select Cust_DBName from Master..Mas_Customers where Cust_DivID='" + comname + "' and Cust_Status=0";
			ds = db_ER.Exec_DataSet(strQry);
			DbName = ds.Tables[0].Rows[0]["Cust_DBName"].ToString();	
			dsProd = mc1.getselids(comname,DbName);			
		}
		catch (Exception ex)
		{
			throw ex;
		}        
        return JsonConvert.SerializeObject(dsProd.Tables[0]);
    }
	public DataSet getselids(string comname,string DbName)
	{
		DB_EReporting db_ER = new DB_EReporting();
		DataSet ds = null;
		try
		{
			string strQry = "select Comp_Name,(','+Menu_IDs+',') Menu_IDs from "+DbName+"..Mas_MenuPermissions_Head where Comp_ID='" + comname + "' ";
			ds = db_ER.Exec_DataSet(strQry);
		}
		catch (Exception ex)
		{
			throw ex;
		}
		return ds;
	}

    [WebMethod(EnableSession = true)]
    public static int savedata(int companyid, string companyname, string arr)
    {
        int getreturn;   
        Menu_Creation_Default2 mc = new Menu_Creation_Default2();
		
        getreturn = mc.AddMenuPermissionValues(companyid, companyname , arr , DbName);
		
            if (getreturn > 0)
            {

            }

        return getreturn;

    }
	
	public int AddMenuPermissionValues(int cid, string cname, string result,string DB)
	{
		int i = 0;
		DB_EReporting db_ER = new DB_EReporting();
		string strQry = "";

		if (RecordExist(cid, cname,DB))
		{
			strQry = "update "+DB+"..Mas_MenuPermissions_Head set Menu_IDs='"+ result + "',Lastupdatedt=GetDate() where Comp_ID='"+cid+ "'";
                i = db_ER.ExecQry(strQry);
		}
		else
		{

			try
			{
				 strQry = "select isnull(max(Menu_Slno)+1,'1')  Menu_Slno from "+DB+"..Mas_MenuPermissions_Head";
                    int getid = db_ER.Exec_Scalar(strQry);


                    strQry = "insert into "+DB+"..Mas_MenuPermissions_Head(Menu_Slno,Comp_ID,Comp_Name,Desig_Name,Menu_IDs,Lastupdatedt)" +
                   "values('" + getid + "','" + cid + "','" + cname + "','','" + result + "',GetDate())";

				i = db_ER.ExecQry(strQry);

			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		return i;

	}
	
	 public bool RecordExist(int cid, string did, string DB)
	{

		bool recordexists = false;

		try
		{
			DB_EReporting db = new DB_EReporting();

			string strQry = "select count(Comp_ID) from "+DB+"..Mas_MenuPermissions_Head where Comp_ID='" + cid + "'";
			int a = db.Exec_Scalar(strQry); 

			if (a > 0)
			{
				recordexists = true;
			}
			else
			{

			}

		}
		catch (Exception ex)
		{
			throw ex;
		}

		return recordexists;
	}
		
    public class menupermission
    {
        public int Menu_ID { get; set; }
        public string Menu_Name { get; set; }
        public string Menu_Type { get; set; }
        public string Parent_Menu { get; set; }
    }

    [WebMethod(EnableSession = true)]
    public static menupermission[] GetByDesginID(int Cid, string Did)
    {
        MenuCreation mc = new MenuCreation();
        DataSet ds = new DataSet();
        List<menupermission> per = new List<menupermission>();
        ds = mc.GetDesingIDValues(Cid, Did);
        if (ds.Tables.Count == 0)
        {
            
        }
        else
        {
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                menupermission p = new menupermission();
                p.Menu_ID = Convert.ToInt32(row["Menu_ID"]);
                p.Menu_Name = (row["Menu_Name"]).ToString();
                p.Menu_Type = (row["Menu_Type"]).ToString();
                p.Parent_Menu = (row["Parent_Menu"]).ToString();
              //  p.User_Menu_IDs = (row["User_Menu_IDs"]).ToString();
                per.Add(p);
            }
        }
        return per.ToArray();
    }



    protected void Button1_Click(object sender, EventArgs e)
    {

    }
}