using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
using DBase_EReport;
using System.Data.SqlClient;
using System.Web.Services;
using System.Configuration;
public partial class Menu_Creation_Default : System.Web.UI.Page
{

    string div_code = string.Empty;
    string Menu_Name = string.Empty;
    string Menu_Type = string.Empty;
    string Link_Url = string.Empty;
    string Icon = string.Empty;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    string strQry = string.Empty;
    int time;
    int ddlvalue = 0;
    public string Type = String.Empty;
    public static int iReturn = -1;

    static string MasterCon = ConfigurationManager.ConnectionStrings["MasterDB"].ToString();
    SqlConnection con = new SqlConnection(MasterCon);

    protected void Page_Load(object sender, EventArgs e)
    {
        //div_code = Session["div_code"].ToString();
      

        if (Page.IsPostBack)
        {


        }
        //getparentmenu();
    }

    protected override void OnLoadComplete(EventArgs e)
    {
        ServerEndTime = DateTime.Now;
        TrackPageTime();
    }

    public void TrackPageTime()
    {
        TimeSpan serverTimeDiff = ServerEndTime.Subtract(ServerStartTime);
        time = serverTimeDiff.Minutes;
    }

    
    [WebMethod]
    public static string saveData(string menu_name, string menu_icon, string menutype, string parentmenu, string menuurl, string menu_id, string Active_flag, string Dfault_Screen, string order_id, string Tbar_id)
    {
        String msg = "";
        int getreturn = 0;
        try
        {
            Menu_Creation_Default mc = new Menu_Creation_Default();
            string strqry = "Exec MenuCreation " + menu_id + ",'" + menu_name + "'," + parentmenu + ",'" + menu_icon + "','" + menuurl + "'," + menutype + "," + Active_flag + "," + Dfault_Screen + "," + order_id + "," + Tbar_id + " ";
            DataSet ds = Exec_DataSet(strqry);
            msg = ds.Tables[0].Rows[0]["msg"].ToString();

        }
        catch
        {

        }
        return msg;
    }
    public int AddRecord(string Menu_Name, string Menu_type, string parentmenuvlue, string Link_Url, string icon, string menu_id, string Active_flag, string Dfault_Screen, string order_id, string Tbar_id)
    {
        int i = 0; strQry = "";
        try
        {
            strQry = "select isnull(max(Menu_ID)+1,'1')  Menu_ID from Master..Menu_Creation";
            int getid = Exec_Scalar(strQry);
            if (!RecordExist(Menu_Name, Link_Url, getid))
            {
                strQry = "insert into Menu_Creation(Menu_ID,Menu_Name,Menu_Type,Parent_Menu,Link_Url,Menu_icon,Active_flag,Dfault_Screen,order_id,Tbar_id)" +
                   "values('" + getid + "','" + Menu_Name + "','" + Menu_type + "','" + parentmenuvlue + "','" + Link_Url + "','" + icon + "'," + Active_flag + "," + getid + "," + order_id + "," + Tbar_id + ")";

                i = ExecQry(strQry);
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
        return i;
    }

    public bool RecordExist(string Menu_Name,string LinkURL,int getid)
    {

        bool recordexists = false;
		string splitqry = (LinkURL != "") ? " or Link_Url=" + LinkURL + "" : "";        
        try
        {
            Menu_Creation_Default db = new Menu_Creation_Default();          

            strQry = "select count(Menu_ID) from Master..Menu_Creation where Menu_Name='" + Menu_Name + "' or Menu_ID=" + getid + " "+ splitqry +"";
            int a = Exec_Scalar(strQry);

            if (a > 0)
            {
                recordexists = true;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }

        return recordexists;
    }
    public static DataSet Exec_DataSet(string strQry)
    {
        DataSet ds_EReport = new DataSet();

        SqlConnection _conn = new SqlConnection(MasterCon);
        try
        {


            SqlCommand selectCMD = new SqlCommand(strQry, _conn);
            selectCMD.CommandTimeout = 120;

            SqlDataAdapter da_EReport = new SqlDataAdapter();
            da_EReport.SelectCommand = selectCMD;

            _conn.Open();

            da_EReport.Fill(ds_EReport, "Customers");

            // _conn.Close();
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            _conn.Close();
        }
        return ds_EReport;
    }
    public static int Exec_Scalar(string strQry)
    {
        SqlConnection _conn = new SqlConnection(MasterCon);
        try
        {
            iReturn = -1;
            // SqlConnection _conn = new SqlConnection(strConn);
            SqlCommand selectCMD = new SqlCommand(strQry, _conn);
            selectCMD.CommandTimeout = 30;
            _conn.Open();
            iReturn = Convert.ToInt32(selectCMD.ExecuteScalar());
            // _conn.Close();
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            _conn.Close();
        }
        return iReturn;
    }
    public static int ExecQry(string sQry)
    {
       
        SqlConnection _conn = new SqlConnection(MasterCon);
        try
        {
            //SqlConnection _conn = new SqlConnection(strConn);
            System.Data.SqlClient.SqlCommand cmd;
            cmd = new System.Data.SqlClient.SqlCommand();
            cmd.Connection = _conn;
            cmd.CommandText = sQry;
            _conn.Open();
            iReturn = cmd.ExecuteNonQuery();
            // _conn.Close();
        }
        catch (Exception ex)
        {
            //return 0;
            throw ex;
        }
        finally
        {
            _conn.Close();
        }
        return iReturn;
    }
   
    protected void viewpage_Click(object sender, EventArgs e)
    {
        Response.Redirect("view.aspx");
    }
    protected void ddlmenutype_SelectedIndexChanged(object sender, EventArgs e)
    {
       
       

        
    }

    public void getparentmenu()
    {
        MenuCreation mc = new MenuCreation();
        DataSet dsmenucreation = new DataSet();
        dsmenucreation = mc.menutype();
       
    }
    //public void getLinkUrl()
    // {
    //     MenuCreation mc = new MenuCreation();
    //     DataSet dslinkurl = new DataSet();
    //     dslinkurl = mc.getLinkUrl();
    //     if (dslinkurl.Tables[0].Rows.Count > 0)
    //     {
    //         ddlparentmenu.DataTextField = "Menu_Type";
    //         ddlparentmenu.DataValueField = "Menu_Id";
    //         ddlparentmenu.DataSource = dslinkurl;
    //         ddlparentmenu.DataBind();   
    //     }
    // }
}