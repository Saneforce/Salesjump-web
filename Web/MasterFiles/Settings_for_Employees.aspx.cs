using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Drawing;
using Bus_EReport;



using System.Web.Services;
using System.Data.SqlClient;
using Newtonsoft.Json;


public partial class MasterFiles_Settings_for_Employees : System.Web.UI.Page
{
    #region "Declaration"
    DataSet dsDivision = null;
    DataSet dsEmployee = null;
    DataSet dsAccessmas = null;
    string div_code = string.Empty;
    string div_name = string.Empty;
    string div_addr1 = string.Empty;
    string div_addr2 = string.Empty;
    string div_city = string.Empty;
    string div_pin = string.Empty;
    string div_state = string.Empty;
    string div_sname = string.Empty;
    string div_alias = string.Empty;
    string state_code = string.Empty;
    string sChkLocation = string.Empty;
    string txtNewSlNo = string.Empty;
    string sf_type = string.Empty;
    string sf_code = string.Empty;
    string dsdoc = string.Empty;
    string str = string.Empty;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int time;
    int iLength = -1;
    #endregion


    // DataTable dssalesforce1 = null;
    DataSet dsSalesForce = null;

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

        sf_type = Session["sf_type"].ToString();

        sf_code = Session["sf_code"].ToString();

        if (sf_type == "3")
        {
            div_code = Session["division_code"].ToString();
        }
        else
        {
            div_code = Session["div_code"].ToString();
        }


        if (!Page.IsPostBack)
        {
            //menu1.Title = this.Page.Title;
            //menu1.FindControl("btnBack").Visible = false;
            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);
           // grdEmployee.DataSource = FillEmployee(div_code, sf_code);
           // grdEmployee.DataBind();
           // FillReporting();

           
            //UpdatePanel1.Visible = true;
        }
    }

   
    protected override void OnLoadComplete(EventArgs e)
    {
        ServerEndTime = DateTime.Now;
        TrackPageTime();//It will give you page load time  
    }
    public void TrackPageTime()
    {
        TimeSpan serverTimeDiff = ServerEndTime.Subtract(ServerStartTime);
        time = serverTimeDiff.Minutes;

    }
   
   


    [WebMethod]
    public static string getDBInfo(string param)
    {
        //DataTable dt = FillEmployee("4", param);
        //dt.TableName = "dtEmployee";
        //DataSet ds = new DataSet();
        //ds.Tables.Add(dt);
        //return ds.GetXml();
        return param;
    }


    
   
 public class empdata
    {
        public string Reporting_To_SF { get; set; }
        public string sfcode { get; set; }
        public string Sf_name { get; set; }
        public Int16 geoneed { get; set; }
		public Int16 Van_Sales { get; set; }
        public Int16 Geo_Track { get; set; }
        public Int16 Geo_Fencing { get; set; }
        public Int16 Eddy_Sumry { get; set; }
		public Int16 DCR_Summary_ND { get; set; }
		public Int16 fcs { get; set; }
        public string sf_type { get; set; }
    }

[WebMethod]
    public static empdata[] getdata(string div_code, string sf_code)
    {
        AdminSetup adm = new AdminSetup();
        //if (sf_type == "3")
        //{


        DataTable dt = new DataTable();
        dt.Columns.Add("Reporting_To_SF", typeof(string));
        dt.Columns.Add("Sf_code", typeof(string));
        dt.Columns.Add("Sf_name", typeof(string));

        dt.Columns.Add("geoneed", typeof(int));
        dt.Columns.Add("Geo_Track", typeof(int));
        dt.Columns.Add("Geo_Fencing", typeof(int));
        dt.Columns.Add("Eddy_Sumry", typeof(int));
        dt.Columns.Add("sf_type", typeof(string));
		dt.Columns.Add("DCR_Summary_ND", typeof(string));
        dt.Columns.Add("Van_Sales", typeof(int));
        dt.Columns.Add("fcs", typeof(int));

        List<empdata> empList = new List<empdata>();



        DataSet dsEmployee = adm.GetEmployees_sp(div_code.TrimEnd(','), sf_code);
        DataSet dsAccessmas = adm.GetAccessmas_sp(div_code.TrimEnd(','));

        foreach (DataRow row in dsEmployee.Tables[0].Rows)
        {
            string str = row["Sf_code"].ToString().TrimEnd(',');

            empdata emp = new empdata();


            //GridView1.DataSource = dsAccessmas.Tables[0];
            //GridView1.DataBind();
            Int32 count = 0;
            if (!row["Sf_code"].ToString().TrimEnd(',').Equals("admin"))
            {
                count = dsAccessmas.Tables[0].Select("sf_code = '" + row["Sf_code"].ToString().TrimEnd(',') + "'").Length;

                if (count > 0)
                {
                    foreach (DataRow rowa in dsAccessmas.Tables[0].Select("Sf_code = '" + row["Sf_code"].ToString() + "'"))
                    {
                        //rbtnnre.SelectedValue = row["UNLNeed"] == DBNull.Value ? "1" : row["UNLNeed"].ToString();// row["UNLNeed"].ToString(); 
                        //   bool geoneed = rowa["GeoNeed"] == DBNull.Value ? true : !Convert.ToBoolean(Convert.ToInt16(rowa["GeoNeed"]));
                        Int16 geoneed = rowa["GeoNeed"]== DBNull.Value ? Convert.ToInt16(0) : Convert.ToInt16(rowa["GeoNeed"]); 
                        Int16 geotrack = rowa["geo_track"]== DBNull.Value ? Convert.ToInt16(0) : Convert.ToInt16(rowa["geo_track"]); // rowa["geo_track"] == DBNull.Value ? true : !Convert.ToBoolean(Convert.ToInt16(rowa["geo_track"]));
                        Int16 geofencing =rowa["geo_Fencing"]== DBNull.Value ? Convert.ToInt16(0) : Convert.ToInt16(rowa["geo_Fencing"]); // rowa["geo_Fencing"] == DBNull.Value ? false : Convert.ToBoolean(Convert.ToInt16(rowa["geo_Fencing"]));
                        Int16 eddysumry =rowa["Eddy_Sumry"]== DBNull.Value ? Convert.ToInt16(0) : Convert.ToInt16(rowa["Eddy_Sumry"]); // rowa["Eddy_Sumry"] == DBNull.Value ? true : !Convert.ToBoolean(Convert.ToInt16(rowa["Eddy_Sumry"]));
                        Int16 chnen = rowa["DCR_Summary_ND"] == DBNull.Value ? Convert.ToInt16(0) : Convert.ToInt16(rowa["DCR_Summary_ND"]);
						Int16 vsal = rowa["Van_Sales"] == DBNull.Value ? Convert.ToInt16(0) : Convert.ToInt16(rowa["Van_Sales"]);
						Int16 fcs = rowa["firstcall_selfie"] == DBNull.Value ? Convert.ToInt16(0) : Convert.ToInt16(rowa["firstcall_selfie"]);
						
						dt.Rows.Add(row["Reporting_To_SF"].ToString(), row["Sf_code"].ToString(), row["Sf_Name"].ToString(), geoneed, geotrack, geofencing, eddysumry,vsal,chnen,fcs, row["sf_type"].ToString());

                        emp.Reporting_To_SF = row["Reporting_To_SF"].ToString();
                        emp.sfcode = row["Sf_code"].ToString();
                        emp.Sf_name = row["Sf_Name"].ToString();
                        emp.geoneed = geoneed;
                        emp.Geo_Track = geotrack;
                        emp.Geo_Fencing = geofencing;
                        emp.Eddy_Sumry = eddysumry;
						emp.DCR_Summary_ND = chnen;
						emp.Van_Sales = vsal;
						emp.fcs = fcs;
                        emp.sf_type = row["sf_type"].ToString();
                        empList.Add(emp);
                    }
                }
                else
                {
                    Int16 geoneed = 0;
                    Int16 geotrack = 0;
                    Int16 geofencing = 0;
                    Int16 eddysumry = 0;
					Int16 chnen = 0;
					 Int16 vsal = 0;
					 Int16 fcs = 0;
                    dt.Rows.Add(row["Reporting_To_SF"].ToString(), row["Sf_code"].ToString(), row["Sf_Name"].ToString(), geoneed, geotrack, geofencing, eddysumry,vsal, chnen,fcs, row["sf_type"].ToString());

                    emp.Reporting_To_SF = row["Reporting_To_SF"].ToString();
                    emp.sfcode = row["Sf_code"].ToString();
                    emp.Sf_name = row["Sf_Name"].ToString();
                    emp.geoneed = geoneed;
                    emp.Geo_Track = geotrack;
                    emp.Geo_Fencing = geofencing;
                    emp.Eddy_Sumry = eddysumry;
					emp.DCR_Summary_ND = chnen;
				    emp.Van_Sales = vsal;
				    emp.fcs = fcs;
                    emp.sf_type = row["sf_type"].ToString();
                    empList.Add(emp);
                }
            }
        }
        DataSet ds = new DataSet();
        ds.Tables.Add(dt);
        return empList.ToArray();
    }


[WebMethod]
    public static List<ListItem> GetFieldforce(string div_code)
    {
        SalesForce sf = new SalesForce();        
        DataSet dsSalesForce = sf.UserList_Hierarchy(div_code.TrimEnd(','), "Admin");
        List<ListItem> customers = new List<ListItem>();
        foreach (DataRow rows in dsSalesForce.Tables[0].Rows)
        {
            customers.Add(new ListItem
            {
                Value = rows["Sf_Code"].ToString(),

                Text = rows["Sf_Name"].ToString()
            });
        }
        return customers;

    }


	[WebMethod]
    public static string savedata(string data)
    {
        MasterFiles_Settings_for_Employees ms = new MasterFiles_Settings_for_Employees();
        return ms.save(data);
    }

    private string save(string data)
    {
        sf_type = Session["sf_type"].ToString();
        sf_code = Session["sf_code"].ToString();


        if (sf_type == "3")
        {
            div_code = Session["division_code"].ToString();
        }
        else
        {
            div_code = Session["div_code"].ToString();
        }


        string[] str = data.Split(',');
        string[,] arr1 = new string[str.Length, str[0].Split('*').Length];
        string[] tmp = new string[] { };
        
        for (int i = 0; i < str.Length; i++)
        {
            tmp = str[i].Split('*');
            for (int j = 0; j < tmp.Length; j++)
            {
                arr1[i, j] = tmp[j];
            }
        }

        var items = JsonConvert.DeserializeObject<List<Item>>(data);
        int co = 0;
        for (int i = 0; i < items.Count; i++)
        {
            AdminSetup admin = new AdminSetup();

            int iReturn = admin.insert_acmas(items[i].sf_code, items[i].sf_name,"", items[i].geoneed, items[i].geotrack, items[i].geofencing, items[i].eddysumry, items[i].vsal, items[i].chnen, items[i].fcs, div_code.ToString().TrimEnd(',')  , Convert.ToInt16(items[i].sf_type));
            co++;






        }


        if (co > 0)
        {
            return "Sucess";
        }
        else
        {
            return "Error";
        }


    }
	 public class Item
    {
        public string sf_code { get; set; }
        public string sf_name { get; set; }
        public byte geoneed { get; set; }
        public byte geotrack { get; set; }
        public byte geofencing { get; set; }
        public byte eddysumry { get; set; }
		  public byte vsal { get; set; }
		public byte chnen { get; set; }
		public byte fcs { get; set; }
		 public string sf_type { get; set; }

    }



}