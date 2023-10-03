using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;

public partial class MasterFiles_SalesForce_Transfer : System.Web.UI.Page
{
    DataSet dsDivision = null;
    DataSet dsSubDivision = null;
    DataSet dsSF = null;
    DataSet dstype = null;
    DataSet dsState = null;
    DataSet dsdiv = null;
    string sState = string.Empty;
    string strSfCode = string.Empty;
    string div_code = string.Empty;
    string sDesSName = string.Empty;
    string division_code = string.Empty;
    string[] statecd;
    string state_cd = string.Empty;
    string sfcode = string.Empty;
    string sf_hq = string.Empty;
    string sfreason = string.Empty;
    DataSet dsSalesForce = null;
    string subdivision_code = string.Empty;
    string sf_type = string.Empty;
    string sfname = string.Empty;
    string sub_division = string.Empty;
    string reporting_to = string.Empty;
    bool isManager = false;
    bool isCreate = true;
    string divvalue = string.Empty;
    int iIndex = -1;
    string sChkLocation = string.Empty;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int time;
    string usrname = string.Empty;
    string desname = string.Empty;
    string Reporting_To_SF = string.Empty;
    string strMultiDiv = string.Empty;
    string HO_ID = string.Empty;
    string type = string.Empty;
    string joiningdate = string.Empty;
    string UserDefin = string.Empty;
    string Sf_emp_id = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        HO_ID = Session["HO_ID"].ToString();
        div_code = Session["div_code"].ToString();
        type = Session["type"].ToString();
        sfcode = Request.QueryString["sfcode"];
        sfname = Request.QueryString["sfname"];
        joiningdate = Request.QueryString["joiningdate"];
        UserDefin = Request.QueryString["UserDefin"];
        Sf_emp_id = Request.QueryString["Sf_emp_id"];

        lblheader.Text = "Selected Transfer Area for " + sfname;
            
        if (!Page.IsPostBack)
        {
            menu1.FindControl("btnBack").Visible = true;
            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);
            getDivision();
        }

    }
    protected void ddlFieldForce_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillVacantManagers();
    }

    protected void rdoMode_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rdoMode.SelectedValue.ToString() == "0")
        {
            ddlrepla.Visible = false;
            lblreplace.Visible = false;
        }
        else
        {
            ddlrepla.Visible = true;
            lblreplace.Visible = true;
            FillVacantManagers();
        }
    }

    private void FillVacantManagers()
    {
        SalesForce sf = new SalesForce();

        dsSF = sf.getVacantManagers(ddlDivision.SelectedValue.ToString(),type,sfcode);

        if (dsSF.Tables[0].Rows.Count > 0)
        {
            ddlrepla.DataTextField = "sf_name";
            ddlrepla.DataValueField = "Sf_Code";
            ddlrepla.DataSource = dsSF;
            ddlrepla.DataBind();
        }
    }

 

    private void getDivision()
    {
        Division div = new Division();
        DataSet dsdiv = new DataSet();
        dsdiv = div.getMultiDiv_For_salesforce(HO_ID);  
        if(dsdiv.Tables[0].Rows.Count > 0)
        {
            //if (dsdiv.Tables[0].Rows[0]["Division_Code"].ToString() == "1")
            //{
                strMultiDiv = dsdiv.Tables[0].Rows[0][1].ToString().Remove(dsdiv.Tables[0].Rows[0][1].ToString().Length - 1, 1);
            //}
        }

        Division dv = new Division();
        DataSet dsDivision = new DataSet();
        dsDivision = dv.getMultiDivision_forTransfer(strMultiDiv);
        if (dsDivision.Tables[0].Rows.Count > 0)
        {
            ddlDivision.DataTextField = "Division_Name";
            ddlDivision.DataValueField = "Division_Code";
            ddlDivision.DataSource = dsDivision;
            ddlDivision.DataBind();
        }
    }

    protected void btnGo_Click(object sender, EventArgs e)
    {
        if (rdoMode.SelectedValue.ToString() == "0")
        {
            System.Threading.Thread.Sleep(time);
            Session["div_code"] = ddlDivision.SelectedValue.ToString();
            Session["type"] = type;
            Session["Effective_Date"] = txteffe.Text;
            Session["joiningdate"] = joiningdate;
            Session["Fieldforce_Name"] = sfname;
            Session["UserDefin"] = UserDefin;
            Session["Sf_emp_id"] = Sf_emp_id;
            Session["Vac_sfcode"] = sfcode;
            Response.Redirect("SalesForce.aspx?rdoMode=" + rdoMode.SelectedValue.ToString());
       
        }
        else
        {
            System.Threading.Thread.Sleep(time);
            Session["div_code"] = ddlDivision.SelectedValue.ToString();
            Session["Effective_Date"] = txteffe.Text;
            Session["joiningdate"] = joiningdate;
            Session["UserDefin"] = UserDefin;
            Session["Sf_emp_id"] = Sf_emp_id;
            Session["Vac_sfcode"] = sfcode;
            Response.Redirect("SalesForce.aspx?sfcode=" + ddlrepla.SelectedValue.ToString() + "&rdoMode=" + rdoMode.SelectedValue.ToString() + "&sfname=" + sfname);
            
        }

      
    }
}