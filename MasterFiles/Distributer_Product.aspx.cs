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
using DBase_EReport;
using Newtonsoft.Json;
using System.Web.Services;
using System.Data.SqlClient;

public partial class MasterFiles_Distributer_Product : System.Web.UI.Page
{
    public string sf_type = string.Empty;
    string div_code = string.Empty;
    string sf_code = string.Empty;
    DataSet dsSalesForce = null;
    DataSet dsTP = null;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int time;
    protected void Page_PreInit(object sender, EventArgs e)
    {
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
        else if (sf_type == "4")
        {
            this.MasterPageFile = "~/Master_DIS.master";
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {

        div_code = Session["div_code"].ToString();
        sf_code = Session["sf_code"].ToString();

        if (!Page.IsPostBack)
        {
            fillsubdivision();
            FillYear();
            FillMRManagers();
        }

    }
    private void fillsubdivision()
    {
        SalesForce sd = new SalesForce();
        DataSet dsDiv = sd.Getsubdivisionwise(div_code);
        if (dsDiv.Tables[0].Rows.Count > 0)
        {

            subdiv.DataTextField = "subdivision_name";
            subdiv.DataValueField = "subdivision_code";
            subdiv.DataSource = dsDiv;
            subdiv.DataBind();
            subdiv.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        else
        {
            subdiv.Items.Insert(0, new ListItem("--Select--", "0"));
        }
    }
    private void FillYear()
    {
        TourPlan tp = new TourPlan();
        dsTP = tp.Get_TP_Edit_Year(div_code);
        if (dsTP.Tables[0].Rows.Count > 0)
        {
            for (int k = Convert.ToInt16(dsTP.Tables[0].Rows[0]["Year"]); k <= DateTime.Now.Year + 1; k++)
            {
                ddlFYear.Items.Add(k.ToString());

                ddlFYear.SelectedValue = DateTime.Now.Year.ToString();
            }
        }
        ddlFMonth.SelectedValue = DateTime.Now.Month.ToString();

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

    private void FillMRManagers()
    {
        SalesForce sf = new SalesForce();
        //dsSalesForce = sf.SalesForceListMgrGet(div_code, sf_code);

        
        ddlFieldForce.DataSource = null;
        ddlFieldForce.Items.Clear();
        ddlFieldForce.Items.Insert(0, "---Select Field Force---");

        dsSalesForce = sf.UserList_getMR(div_code.TrimEnd(','), sf_code);


        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            ddlFieldForce.DataTextField = "Sf_Name";
            ddlFieldForce.DataValueField = "Sf_Code";
            ddlFieldForce.DataSource = dsSalesForce;
            ddlFieldForce.DataBind();
            ddlFieldForce.Items.Insert(0, "---Select Field Force---");

        }
    }
    protected void ddlFieldForce_SelectedIndexChanged(object sender, EventArgs e)
    {


    }

    protected void btnSubmit_Click1(object sender, EventArgs e)
    {


    }
    [WebMethod(EnableSession = true)]
    public static Item[] getdata(string data)
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

        prouct pro = new prouct();
        List<Item> empList = new List<Item>();
        DataSet dsAccessmas = pro.getproductname(div_code.TrimEnd(','), data);
        foreach (DataRow row in dsAccessmas.Tables[0].Rows)
        {
            Item emp = new Item();
            emp.product_id = row["Product_Detail_Code"].ToString();
            emp.product_name = row["Product_Detail_Name"].ToString();
            empList.Add(emp);
        }
        return empList.ToArray();
    }
    public class Item
    {
        public string product_id { get; set; }
        public string product_name { get; set; }
    }
   
    [WebMethod(EnableSession = true)]
    public static user[] getfo(string subdiv,string sfcode)
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
        SalesForce sf = new SalesForce();
        List<user> empList = new List<user>();
        DataSet dsAccessmas = sf.GetStockist_subdiv_sfwise(div_code, subdiv, sfcode);
        foreach (DataRow row in dsAccessmas.Tables[0].Rows)
        {
            user emp = new user();
            emp.Stockist_code = row["Stockist_code"].ToString();
            emp.Stockist_Name = row["Stockist_Name"].ToString();
            emp.State_Code = row["State_Code"].ToString();
            empList.Add(emp);
        }
        return empList.ToArray();
    }
   public class user
    {
        public string Stockist_code { get; set; }
        public string Stockist_Name { get; set; }
        public string State_Code { get; set; }

    }
    [WebMethod(EnableSession = true)]
    public static string inserttarget(string target, string div, string Sf_code, string year, string Mon)
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
        Stockist st = new Stockist();
        try
        {
            List<insertTarget> empList =JsonConvert.DeserializeObject<List<insertTarget>>(target);
         int rr = -1;
            // try
            //{
            //for (int i = 0; i < empList.Count; i++)
            //{
            //    rr = st.inserttarget(empList[i].div, empList[i].St_code, empList[i].Mon, empList[i].year, empList[i].P_code, empList[i].T_Qnty, empList[i].Sf_code);
            //}
            string xml = "<root>";

            for (int i = 0; i < empList.Count; i++)
            {
                xml += "<ASSD St_code=\"" + empList[i].St_code + "\"  P_code=\"" + empList[i].P_code + "\" T_Qnty=\"" + empList[i].T_Qnty +  "\"/>";//");//, empList[i].T_detail_no);
            }
            xml += "</root>";

            rr = st.inserttarget(xml, Mon, year, Sf_code,div);
         //   if (rr > 0)
              
            }
        catch (Exception ex)
        {
            return "update fail...";
        }
        return "updated Sucessfull...";
    }
    public class insertTarget
    {
        public string Target_sl_no { get; set; }
        public string St_code { get; set; }
        public string Mon { get; set; }
        public string year { get; set; }
        public string div { get; set; }
        public string Sf_code { get; set; }
        public string T_detail_no { get; set; }
        public string P_code { get; set; }
        public string T_Qnty { get; set; }      

    }

   

    [WebMethod(EnableSession = true)]
    public static ItemRates[] getqnty(string div,string sfcode, string Mon, string year)
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

        Product pro = new Product();
        List<ItemRates> empList = new List<ItemRates>();
        DataSet dsAccessmas = pro.get_Target_Qnty(div, sfcode, Mon, year);
        foreach (DataRow row in dsAccessmas.Tables[0].Rows)
        {
            ItemRates emp = new ItemRates();
            emp.Stockist_code = row["Stockist_code"].ToString();
            emp.Product_code = row["Product_code"].ToString();
            emp.Target_Qnty = row["Target_Qnty"].ToString();
            emp.Trans_target_detail = row["Trans_target_detail"].ToString();
            emp.Target_sl_no = row["Target_sl_no"].ToString();


            empList.Add(emp);
        }
        return empList.ToArray();
    }

    public class ItemRates
    {
        public string Stockist_code { get; set; }
        public string Product_code { get; set; }
        public string Target_Qnty { get; set; }
        public string Trans_target_detail { get; set; }
        public string Target_sl_no { get; set; }

    }
	public class prouct
	{
		
	public DataSet getproductname(string div_code, string Sub_Div_Code = "0")
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsState = null;
            // strQry = "exec productratewise '" + div_code  + "','" + Sub_Div_Code + "'";

            string strQry = "select Product_Detail_Code,isnull(Product_Short_Name,Product_Detail_Name)Product_Short_Name,Product_Detail_Name,Unit_code,Move_MailFolder_Name,Sample_Erp_Code from Mas_Product_Detail MPD INNER JOIN Mas_Multi_Unit_Entry MUE ON MPD.Unit_code = MUE.Move_MailFolder_Id AND MPD.Division_Code =MUE.Division_Code where MPD.Division_Code='" + div_code + "' and ('" + Sub_Div_Code + "' ='0' or  subdivision_code  IS NULL or  charindex(','+cast('" + Sub_Div_Code + "' as varchar)+',',','+subdivision_code+',')>0  ) and Product_Active_Flag='0' order by Product_Cat_Code,Product_Detail_Name";
            //strQry = "select Product_Detail_Code,Product_Short_Name,Product_Detail_Name from Mas_Product_Detail where Division_Code='" + div_code + "' and Product_Active_Flag='0' ";
            try
            {
                dsState = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsState;
        }
	}
}