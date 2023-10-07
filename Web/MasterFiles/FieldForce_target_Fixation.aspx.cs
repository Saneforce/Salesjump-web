using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Services;
using Bus_EReport;
using Bus_Objects;
using Newtonsoft.Json;

public partial class MasterFiles_FieldForce_target_Fixation : System.Web.UI.Page
{
    #region "declaration"

    string sfType = string.Empty;
    string sfCode = string.Empty;
    string DivCode = string.Empty;
    string SubDiv = string.Empty;
    string custCode = string.Empty;

    DataSet dsDiv = null;
    DataSet dsFF = null;
    DataSet dsTeam = null;
    DataSet dsCustomer = null;
    DataSet dsTP = null;

    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int time;

    #endregion
    protected void Page_PreInit(object sender, EventArgs e)
    {
        sfType = Session["sf_type"].ToString();
        if (sfType == "3")
        {
            this.MasterPageFile = "~/Master.master";
        }
        else if (sfType == "2")
        {
            this.MasterPageFile = "~/Master_MGR.master";
        }
        else if (sfType == "1")
        {
            this.MasterPageFile = "~/Master_MR.master";
        }
        else if (sfType == "4")
        {
            this.MasterPageFile = "~/Master_DIS.master";
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        DivCode = Session["div_code"].ToString();
        sfCode = Session["sf_code"].ToString();

        if (!Page.IsPostBack)
        {
            FillYear();
            FillMRManagers("0");
            fillsubdivision();
        }
    }
    private void FillYear()
    {
        TourPlan tp = new TourPlan();
        dsTP = tp.Get_TP_Edit_Year(DivCode);
        if (dsTP.Tables[0].Rows.Count > 0)
        {
            for (int k = Convert.ToInt16(dsTP.Tables[0].Rows[0]["Year"]); k <= DateTime.Now.Year + 1; k++)
            {
                ddlFYear.Items.Add(k.ToString());

                ddlFYear.SelectedValue = DateTime.Now.Year.ToString();

            }
        }

    }
    private void FillMRManagers(string Sub_Div_Code)
    {
        SalesForce sf = new SalesForce();

        dsTeam = sf.SalesForceListMgrGet_MgrOnly(DivCode, sfCode, Sub_Div_Code);
        if (dsTeam.Tables[0].Rows.Count > 0)
        {
            ddlFieldForce.DataTextField = "sf_name";
            ddlFieldForce.DataValueField = "sf_code";
            ddlFieldForce.DataSource = dsTeam;
            ddlFieldForce.DataBind();
            ddlFieldForce.Items.Insert(0, new ListItem("---Select Manager ---", "0"));
        }
    }
    private void fillsubdivision()
    {
        SalesForce sd = new SalesForce();
        dsDiv = sd.Getsubdivisionwise(DivCode);
        if (dsDiv.Tables[0].Rows.Count > 0)
        {

            subdiv.DataTextField = "subdivision_name";
            subdiv.DataValueField = "subdivision_code";
            subdiv.DataSource = dsDiv;
            subdiv.DataBind();
            subdiv.Items.Insert(0, new ListItem("--Select--", "0"));

        }
    }
    protected void subdiv_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (subdiv.SelectedValue.ToString() != "0")
        {
            FillMRManagers(subdiv.SelectedValue.ToString());
        }
        else
        {
            FillMRManagers(subdiv.SelectedValue.ToString());
        }
    }
    [WebMethod(EnableSession = true)]
    public static string GetFF(string Stockist_Code, string ddlFieldForce, string subdiv)
    {
        string StkCode = HttpContext.Current.Session["Sf_Code"].ToString();
        string Div_Code = HttpContext.Current.Session["div_code"].ToString();
        DataSet dsFF = new DataSet();
        SalesForce sf = new SalesForce();   
        dsFF = sf.UserList_getMR(Div_Code, ddlFieldForce, subdiv);
        return JsonConvert.SerializeObject(dsFF.Tables[0]);

    }
    [WebMethod(EnableSession = true)]
    public static string inserttarget(string target, string div, string Sf_code, string year,string subdiv)
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
            List<insertTarget> empList = JsonConvert.DeserializeObject<List<insertTarget>>(target);
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
                xml += "<ASSD FF_code=\"" + empList[i].FF_code + "\"  month=\"" + empList[i].month + "\" T_Qnty=\"" + empList[i].T_Qnty + "\" />";//");//, empList[i].T_detail_no);
            }
            xml += "</root>";

            rr = st.inserttargetFF(xml, year, Sf_code, div, subdiv);
            //   if (rr > 0)

        }
        catch (Exception ex)
        {
            return "update fail...";
        }
        return "updated";
    }
    public class insertTarget
    {
        public string Target_sl_no { get; set; }
        public string FF_code { get; set; }
        public string month { get; set; }
        public string year { get; set; }
        public string div { get; set; }
        public string Sf_code { get; set; }
        public string T_detail_no { get; set; }
        public string P_code { get; set; }
        public string T_Qnty { get; set; }

    }
    [WebMethod(EnableSession = true)]
    public static ItemRates[] getqnty(string div, string sfcode,  string year)
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
        DataSet dsAccessmas = pro.get_Target_QntyFF(div, sfcode,  year);
        foreach (DataRow row in dsAccessmas.Tables[0].Rows)
        {
            ItemRates emp = new ItemRates();
            emp.SF_Code = row["SF_Code"].ToString();
            emp.month = row["month"].ToString();
            emp.Target_Value = row["Target_Value"].ToString();
            emp.Trans_target_detail = row["TransFTDlSlno"].ToString();
            emp.Target_sl_no = row["TransFTslno"].ToString();


            empList.Add(emp);
        }
        return empList.ToArray();
    }

    public class ItemRates
    {
        public string SF_Code { get; set; }
        public string month { get; set; }
        public string Target_Value { get; set; }
        public string Trans_target_detail { get; set; }
        public string Target_sl_no { get; set; }

    }
}