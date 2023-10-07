using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
using System.Drawing.Imaging;
public partial class Reports_DCR_My_Date_Plan : System.Web.UI.Page
{
    DataSet dsSalesForce = null;
    DataSet dsdiv = new DataSet();
    DataSet dsTP = null;
    DataSet dsDivision = null;
    string strMultiDiv = string.Empty;
    string div_code = string.Empty;
    string sf_code = string.Empty;
    DataSet dsSf = null;
    string sf_type = string.Empty;
    string sQryStr = string.Empty;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    string Plandate=string.Empty;
    int time;
    //my day plan
    string work_type_name = string.Empty;
    string work_type_code = string.Empty;
    string Head_Quarters_name = string.Empty;
    string Head_Quarters_code = string.Empty;
    string Dist_name = string.Empty;
    string Dist_code = string.Empty;
    string Route_name = string.Empty;
    string Route_code = string.Empty;
    string Remarks = string.Empty;

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
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        sf_code = Session["sf_code"].ToString();
        sf_type = Session["sf_type"].ToString();
        if (Session["sf_type"].ToString() == "1")
        {
            if (!Page.IsPostBack)
            {

                FillWorkType();
                FillMRManagers();
                FillDSM();
                Filldist();
               
            }

        }

        else if (Session["sf_type"].ToString() == "2")
        {

            if (!Page.IsPostBack)
            {

                FillWorkType();
                FillMRManagers();
                FillDSM();
                Filldist();
               
            }

        }

        else if (Session["sf_type"].ToString() == "3")
        {


            if (!Page.IsPostBack)
            {

                FillWorkType();
                FillMRManagers();
                FillDSM();
                Filldist();
               
            }

        }



    }

    private void FillMRManagers()
    {
            SalesForce sf = new SalesForce();
            dsSalesForce = sf.SalesForceList(div_code, sf_code);
            if (dsSalesForce.Tables[0].Rows.Count > 0)
            {
                email.DataTextField = "Sf_Name";
                email.DataValueField = "Sf_Code";
                email.DataSource = dsSalesForce;
                email.DataBind();

            }
    }

    protected DataSet FillWorkType()
    {
        TP_New tp = new TP_New();
        dsTP = tp.FetchWorkType_New(div_code);

        if (dsTP.Tables[0].Rows.Count > 0)
        {
            name.DataTextField = "WorkType_Name_B";
            name.DataValueField = "WorkType_Code_B";
            name.DataSource = dsTP;
            name.DataBind();
        }
        return dsTP;
    }

    protected DataSet FillDSM()
    {
        TP_New tp = new TP_New();

        dsTP = tp.getDistByFF11(sf_code, div_code);
        if (dsTP.Tables[0].Rows.Count > 0)
        {
            username.DataTextField = "Stockist_Name";
            username.DataValueField = "Stockist_code";
            username.DataSource = dsTP;
            username.DataBind();
            username.Items.Insert(0, new ListItem("---Select---", "0"));
        }
        else
        {
            //getddlSF_Code();
        }
        return dsTP;
    }

    private void getddlSF_Code()
    {
        TP_New tp = new TP_New();

        dsTP = tp.FetchDIS(email.SelectedValue);
        if (dsTP.Tables[0].Rows.Count > 0)
        {
            username.DataTextField = "Stockist_Name";
            username.DataValueField = "Distributor_Code";
            username.DataSource = dsTP;
            username.DataBind();
            username.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        else
        {
            Territory terr = new Territory();
            dsTP = terr.getSF_Code_distributor(div_code);
            if (dsTP.Tables[0].Rows.Count > 0)
            {
                username.DataTextField = "stockist_Name";
                username.DataValueField = "Stockist_Code";
                username.DataSource = dsTP;
                username.DataBind();

                if (Session["sf_code"] == null || Session["sf_code"].ToString() == "admin")
                {
                    username.SelectedIndex = 0;
                    //Session["sf_code"] = ddlSFCode.SelectedValue;
                    //Session["sf_Name"] = ddlSFCode.SelectedItem.ToString();
                }

            }
            else
            {
                username.SelectedIndex = 0;
            }
        }

    }



    public void Filldist()
    {
        TP_New tpRP = new TP_New();
        DataSet dsRPDisable = new DataSet();


        dsRPDisable = tpRP.getRouteByDist(username.SelectedValue);
        if (dsRPDisable.Tables[0].Rows.Count > 0)
        {

            password.DataTextField = "Territory_Name";
            password.DataValueField = "Territory_Code";
            password.DataSource = dsRPDisable;
            password.DataBind();
            //password.Items.Insert(0, new ListItem("--Select--", "0"));
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
    protected void username_SelectedIndexChanged(object sender, EventArgs e)
    {

        Filldist();
    }
    protected void email_SelectedIndexChanged(object sender, EventArgs e)
    {
        getddlSF_Code();
        password.Items.Insert(0, new ListItem("--Select--", "0"));
    }
    protected void button_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        Plandate = DateTime.Now.ToString();
        work_type_name = name.SelectedItem.ToString();
        work_type_code = name.SelectedValue.ToString();
        Head_Quarters_name = email.SelectedItem.ToString();
        Head_Quarters_code = email.SelectedValue.ToString();
        Dist_name = username.SelectedItem.ToString();
        Dist_code = username.SelectedValue.ToString();
        Route_name = password.SelectedItem.ToString();
        Route_code = password.SelectedValue.ToString();
        Remarks = Txt_Remark.Text;

 if (work_type_name == "Meeting")
        {
            TP_New tpRP = new TP_New();

            int iReturn = tpRP.MydayPlanRecordAdd(sf_code, Head_Quarters_code, Plandate, Route_code, Remarks, div_code, work_type_code, Route_name, Dist_code);

            if (iReturn > 0)
            {

                // menu1.Status = "Sub Division created Successfully ";
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Created Successfully');</script>");
                //Resetall();
            }
            else if (iReturn == -2)
            {

                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Name Already Exist');</script>");

            }
            else if (iReturn == -3)
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Code Already Exist');</script>");

            }
        }
        else
        {

        if ((work_type_name != "") && (Head_Quarters_name != "") && (Dist_name != "") && (Route_name != "") && (work_type_name != "---Select---") && (Dist_name != "---Select---") && Route_name != "---Select---")
        {
            // Add 
            TP_New tpRP = new TP_New();

            int iReturn = tpRP.MydayPlanRecordAdd(sf_code, Head_Quarters_code, Plandate, Route_code, Remarks, div_code, work_type_code, Route_name, Dist_code);

            if (iReturn > 0)
            {

                // menu1.Status = "Sub Division created Successfully ";
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Created Successfully');</script>");
                //Resetall();
            }
            else if (iReturn == -2)
            {

                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Name Already Exist');</script>");

            }
            else if (iReturn == -3)
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Code Already Exist');</script>");

            }
        }
        else
        {

            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Plz select Value');</script>");

        }
  }
    }
    protected void email_SelectedIndexChanged1(object sender, EventArgs e)
    {
        TP_New tp = new TP_New();

        dsTP = tp.getDistByFF11(email.SelectedValue, div_code);
        if (dsTP.Tables[0].Rows.Count > 0)
        {
            username.DataTextField = "Stockist_Name";
            username.DataValueField = "Stockist_code";
            username.DataSource = dsTP;
            username.DataBind();
            username.Items.Insert(0, new ListItem("---Select---", "0"));
        }
        else
        {
            //getddlSF_Code();
        }
       

    }
}

