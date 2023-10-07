using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
using System.Drawing.Imaging;
public partial class DCR_My_Day_Plan_View : System.Web.UI.Page
{
    DataSet dsSalesForce = null;
    DataSet dsdiv = new DataSet();
    DataSet dsTP = null;
    DataSet dsDivision = null;
    string strMultiDiv = string.Empty;
    string div_code = string.Empty;
    public static string sf_code = string.Empty;
    DataSet dsSf = null;
    public string sf_type = string.Empty;
	 public static string sub_division = string.Empty;
protected void Page_PreInit(object sender, EventArgs e)
        {
           sf_type = Session["sf_type"].ToString();
           if (sf_type == "3")
           {
               this.MasterPageFile = "~/Master.master";
           }
           else if(sf_type == "2")
           {
               this.MasterPageFile = "~/Master_MGR.master";
           }
 	   else if(sf_type == "1")
           {
               this.MasterPageFile = "~/Master_MR.master";
           }
        }
    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        sf_code = Session["sf_code"].ToString();
        sf_type = Session["sf_type"].ToString();
       sub_division = Session["sub_division"].ToString();
        if (Session["sf_type"].ToString() == "1")
        {
            
            ViewState["sf_type"] = "";
            SalesForce sf = new SalesForce();
            dsSf = sf.getReportingTo(sf_code);
            if (dsSf.Tables[0].Rows.Count > 0)
            {
                sf_code = dsSf.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
            }
            if (!Page.IsPostBack)
            {
                FillMRManagers();
            }
            ddlFieldForce.SelectedValue = Session["sf_code"].ToString();
            ddlFieldForce.Enabled = false;
          
        }

        else if (Session["sf_type"].ToString() == "2")
        {
            ViewState["sf_type"] = "";
            if (!Page.IsPostBack)
            {
                 DataSet dsmgrsf = new DataSet();
                SalesForce sf = new SalesForce();
                DataSet DsAudit = sf.SF_Hierarchy(div_code, sf_code);
                if (DsAudit.Tables[0].Rows.Count > 0)
                {
					fillsubdivision();
                    FillMRManagers();
                    ddlFieldForce.SelectedValue = sf_code;
                }
                else
                {
                    DataTable dt = sf.getAuditManagerTeam(div_code, sf_code, 0);
                    dsmgrsf.Tables.Add(dt);
                    dsTP = dsmgrsf;

                    ddlFieldForce.DataTextField = "sf_name";
                    ddlFieldForce.DataValueField = "sf_code";
                    ddlFieldForce.DataSource = dsTP;
                    ddlFieldForce.DataBind();

                }
            }
          
           // lblDivision.Visible = false;
           // ddlDivision.Visible = false;
        }
        else
        {
            ViewState["sf_type"] = "admin";
            //UserControl_MenuUserControl c1 =
            //(UserControl_MenuUserControl)LoadControl("~/UserControl/MenuUserControl.ascx");
            //Divid.Controls.Add(c1);
            //c1.FindControl("btnBack").Visible = false;
            //c1.Title = this.Page.Title;
            if (!Page.IsPostBack)
            {
                fillsubdivision();
                
            }

            if (Session["div_code"] != null)
            {
               
              
            }
        }

        if (!Page.IsPostBack)
        {
             Filldesination();
       
           // menu1.FindControl("btnBack").Visible = false;
            TourPlan tp = new TourPlan();
            DataSet dsTP = new DataSet();
            dsTP = tp.Get_TP_Edit_Year(div_code);
            if (dsTP.Tables[0].Rows.Count > 0)
            {
                for (int k = Convert.ToInt16(dsTP.Tables[0].Rows[0]["Year"]); k <= DateTime.Now.Year + 1; k++)
                {
                    ddlYear.Items.Add(k.ToString());
                    ddlYear.SelectedValue = DateTime.Now.Year.ToString();
                }
            }
            ddlMonth.SelectedValue = DateTime.Now.Month.ToString();
            
            Product prd = new Product();
            dsdiv = prd.getMultiDivsf_Name(sf_code);
            if (dsdiv.Tables[0].Rows.Count > 0)
            {
                if (dsdiv.Tables[0].Rows[0]["IsMultiDivision"].ToString() == "1")
                {
                    strMultiDiv = dsdiv.Tables[0].Rows[0][1].ToString().Remove(dsdiv.Tables[0].Rows[0][1].ToString().Length - 1, 1);

                    fillsubdivision();
                }
                else
                {
                  
                }
            }
        }
      
    }
 private void FillState(string div_code)
    {
        SalesForce dv = new SalesForce();
        ddlstate.Items.Clear();
        dsDivision = dv.getsubdiv_States(div_code, sf_code, subdiv.SelectedValue);
        if (dsDivision.Tables[0].Rows.Count > 0)
        {
            ddlstate.DataTextField = "StateName";
            ddlstate.DataValueField = "State_code";
            ddlstate.DataSource = dsDivision;
            ddlstate.DataBind();
            ddlstate.Items.Insert(0, new ListItem("--Select--", "0"));

        }

    }
    protected void ddlstate_SelectIndexchanged(object sender, EventArgs e)
    {
        FillMRManagers();
    }
    private void FillMRManagers(string Sub_Div_Code)
    {
        SalesForce sf = new SalesForce();
        // dsSalesForce = sf.SalesForceListMgrGet(div_code, sf_code);
        dsSalesForce = sf.SalesForceList(div_code,sf_code,Sub_Div_Code);

        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            ddlFieldForce.DataTextField = "sf_name"; 
            ddlFieldForce.DataValueField = "sf_code";
            ddlFieldForce.DataSource = dsSalesForce;
            ddlFieldForce.DataBind();
            ddlFieldForce.Items.Insert(0, "---Select Field Force---");

        }
    }

    private void fillsubdivision()
    {
        SalesForce sd = new SalesForce();
        dsSalesForce = sd.Getsubdivisionwise(div_code,sub_division);
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
            FillState(div_code);
            FillMRManagers();
        }
        else
        {
            FillMRManagers();
        }
    }


 private void Filldesination()
    {
        SalesForce sf = new SalesForce();
        dsSalesForce = sf.getsfDesignation(div_code);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            Dropdesignation.DataTextField = "Designation_Short_Name";
            Dropdesignation.DataValueField = "Designation_Code";
            Dropdesignation.DataSource = dsSalesForce;
            Dropdesignation.DataBind();
            Dropdesignation.SelectedIndex = 0;
        }
    }

    private void FillMRManagers()
    {
        SalesForce sf = new SalesForce();
        ddlFieldForce.Items.Clear();
        dsSalesForce = sf.SalesForceList(div_code, sf_code,subdiv.SelectedValue,"1",ddlstate.SelectedValue);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            ddlFieldForce.DataTextField = "sf_name";
            ddlFieldForce.DataValueField = "sf_code";
            ddlFieldForce.DataSource = dsSalesForce;
            ddlFieldForce.DataBind();

            //ddlSF.DataTextField = "Desig_Color";
            //ddlSF.DataValueField = "sf_code";
            //ddlSF.DataSource = dsSalesForce;
            //ddlSF.DataBind(); 


        }
    }
    
  
    

  


   

    protected void btnSubmit_Click1(object sender, EventArgs e)
    {
        try
        {
            string StartDate = Convert.ToDateTime(Request.Form["txtFrom"]).ToString("yyyy-MM-dd");


            SalesForce sf = new SalesForce();
            dsSf = sf.CheckSFType(ddlFieldForce.SelectedValue.ToString());

            if (dsSf.Tables[0].Rows.Count > 0)
            {
                if (ViewState["sf_type"].ToString() != "admin")
                    sf_type = dsSf.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
            }
            /*
            if (ddlMR.SelectedIndex != -1 && ddlMR.SelectedIndex != 0)
            {
                string sURL = "Rpt_My_Day_Plan_View.aspx?sf_code=" + ddlMR.SelectedValue + "&div_code=" + div_code + "&cur_month=" + ddlMonth.SelectedValue + "&cur_year=" + ddlYear.SelectedItem.Text +
                    "&Mode=" + rbnList.SelectedItem.Value + "&Sf_Name=" + ddlMR.SelectedItem.Text + "&Date=" + StartDate + "&Sub_Div=" + subdiv.SelectedValue;
                string newWin = "window.open('" + sURL + "',null,'resizable=yes,toolbar=no,scrollbars=yes,menubar=no,status=no,width=800,height=600,left=0,top=0');";
                ClientScript.RegisterStartupScript(this.GetType(), "pop", newWin, true);

            }
            else
            {*/
                 if (Dropdesignation.SelectedValue != "0")
            {
                string sURL = "Rpt_My_Day_Plan_View.aspx?sf_code=" + ddlFieldForce.SelectedValue + "&div_code=" + div_code + "&cur_month=" + ddlMonth.SelectedValue + "&cur_year=" + ddlYear.SelectedItem.Text +
   "&Mode=" + rbnList.SelectedItem.Value + "&Designation_code=" + Dropdesignation.SelectedItem.Value + "&Sf_Name=" + ddlFieldForce.SelectedItem.Text + "&Date=" + StartDate + "&Sub_Div=" + subdiv.SelectedValue;
                string newWin = "window.open('" + sURL + "',null,'resizable=yes,toolbar=no,scrollbars=yes,menubar=no,status=no,width=800,height=600,left=0,top=0');";
                ClientScript.RegisterStartupScript(this.GetType(), "pop", newWin, true);
            }
            else
            {
                string sURL = "Rpt_My_Day_Plan_View.aspx?sf_code=" + ddlFieldForce.SelectedValue + "&div_code=" + div_code + "&cur_month=" + ddlMonth.SelectedValue + "&cur_year=" + ddlYear.SelectedItem.Text +
"&Mode=" + rbnList.SelectedItem.Value + "&Designation_code=" + null + "&Sf_Name=" + ddlFieldForce.SelectedItem.Text + "&Date=" + StartDate + "&Sub_Div=" + subdiv.SelectedValue;
                string newWin = "window.open('" + sURL + "',null,'resizable=yes,toolbar=no,scrollbars=yes,menubar=no,status=no,width=800,height=600,left=0,top=0');";
                ClientScript.RegisterStartupScript(this.GetType(), "pop", newWin, true);
            }


            //}

        }
        catch (Exception)
        {
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Select Date!!!');</script>");
        }
    }
   
    }