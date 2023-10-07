using Bus_EReport;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterFiles_AllowanceMasterFFO : System.Web.UI.Page
{
    #region MyRegion
    string div_code = string.Empty;
    DataSet dsTP = null;
    string tot_dr = string.Empty;
    string tot_FWD = string.Empty;
    string tot_dcr_dr = string.Empty;
    string sCurrentDate = string.Empty;
    string sf_code = string.Empty;
    string sf_type = string.Empty;
    string sf_name = string.Empty;
    string MultiSf_Code = string.Empty;
    DateTime ServerStartTime;
    DataSet dsSalesForce = null;
    #endregion

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
        sf_code = Session["sf_code"].ToString();
        sf_type = Session["sf_type"].ToString();
        sf_name = Session["sf_name"].ToString();
        if (sf_type == "3")
        {
            div_code = Session["div_code"].ToString();
        }
        else
        {
            div_code = Session["div_code"].ToString();
        }
        if (!Page.IsPostBack)
        {
            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);
            fillsubdivision();
            FillMRManagers("0");
        }
    }
    private void fillsubdivision()
    {
        SalesForce sd = new SalesForce();
        dsSalesForce = sd.Getsubdivisionwise(div_code);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            subdiv.DataTextField = "subdivision_name";
            subdiv.DataValueField = "subdivision_code";
            subdiv.DataSource = dsSalesForce;
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
    private void FillMRManagers(string Sub_Div_Code)
    {
        SalesForce sf = new SalesForce();
        dsSalesForce = sf.SalesForceListMgrGet_MgrOnly(div_code, sf_code, Sub_Div_Code);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            ddlFieldForce.DataTextField = "sf_name";
            ddlFieldForce.DataValueField = "sf_code";
            ddlFieldForce.DataSource = dsSalesForce;
            ddlFieldForce.DataBind();
            ddlFieldForce.Items.Insert(0, new ListItem("---Select Manager ---", "0"));
        }
    }
    public class FFO
    {
        public string sfCode { get; set; }
        public string sfName { get; set; }
        public string allwType { get; set; }
        public string empID { get; set; }
        public string mgrsf { get; set; }

    }

    [WebMethod]
    public static List<FFO> GetFieldForce(string sfCode, string SubDiv)
    {
        List<FFO> Lists = new List<FFO>();
        SalesForce sf = new SalesForce();
        string divcode = HttpContext.Current.Session["div_code"].ToString();
        DataSet dsSalesForce = sf.getSalesForceAllwoType(divcode, sfCode, SubDiv);

        foreach (DataRow row in dsSalesForce.Tables[0].Rows)
        {
            FFO list = new FFO();
            if (row["sf_code"].ToString() != "admin")
            {
                list.sfCode = row["sf_code"].ToString();
                list.sfName = row["sf_name"].ToString();
                list.allwType = row["allow_type"].ToString();
                list.empID = row["sf_emp_id"].ToString();
                list.mgrsf = row["Reporting_To_SF"].ToString();

                Lists.Add(list);
            }
        }
        return Lists.ToList();
    }

    public class Allowance_Data
    {
        public string sfCode { get; set; }
        public string mgrsf { get; set; }
        public string allType { get; set; }
    }

    [WebMethod]
    public static string savedata(string data)
    {
        var items = JsonConvert.DeserializeObject<List<Allowance_Data>>(data);
        string divcode = HttpContext.Current.Session["div_code"].ToString();
        int co = 0;
        Expense nt = new Expense();
        for (int i = 0; i < items.Count; i++)
        {
            int iReturn = nt.insertmgrallowance(divcode.TrimEnd(',').ToString(), items[i].sfCode.ToString(), items[i].mgrsf.ToString(), items[i].allType.ToString());
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


}