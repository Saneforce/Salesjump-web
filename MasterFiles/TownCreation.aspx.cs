using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data;
using System.Configuration;
using Bus_EReport;

public partial class MasterFiles_TownCreation : System.Web.UI.Page
{
#region "Declaration"
    #region "Declaration"
    DataSet dsSubDiv = null;
    string Subdivision_Code = string.Empty;
    string divcode = string.Empty;
    string subdiv_sname = string.Empty;
    string subdiv_name = string.Empty;
    string Dist_name = string.Empty;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int time;
    DataSet dsDivision = null;
    DataSet dsState = null;
    DataSet dsStockist = null;
    string state_cd = string.Empty;
    string sState = string.Empty;
    string Dist_cd = string.Empty;
    string Terr_Cd = string.Empty;
    string Terr_name = string.Empty;
    string Tr = string.Empty;
    string[] statecd;
    #endregion
#endregion
string sf_type = string.Empty;
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
        divcode = Convert.ToString(Session["div_code"]);
        Session["backurl"] = "TownList.aspx";
        Subdivision_Code = Request.QueryString["Subdivision_Code"];
      
        if (!Page.IsPostBack)
        {
           
            //menu1.Title = this.Page.Title;
            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);
            FillState(divcode);
            GetTerritoryName();
            txtSubDivision_Sname.Focus();
            if (Subdivision_Code != "" && Subdivision_Code != null)
            {
                Town sd = new Town();
                dsSubDiv = sd.getTown(divcode, Subdivision_Code);                
                if (dsSubDiv.Tables[0].Rows.Count > 0)
                {
                    txtSubDivision_Sname.Text = dsSubDiv.Tables[0].Rows[0].ItemArray.GetValue(0).ToString().Trim();                  
                    txtSubDivision_Name.Text = dsSubDiv.Tables[0].Rows[0].ItemArray.GetValue(1).ToString().Trim();
                    string st = dsSubDiv.Tables[0].Rows[0].ItemArray.GetValue(2).ToString().Trim();
                    Tr = dsSubDiv.Tables[0].Rows[0].ItemArray.GetValue(3).ToString().Trim();
                    ddlState.DataSource = dsState;
                    ddlState.DataTextField = "Distname";
                    ddlState.DataValueField = "Dist_code";
                    ddlState.DataBind();
                    int iCount = 0, iIndex;
                    foreach (ListItem item in ddlState.Items)
                    {
                        if (st == item.ToString())
                        {
                            iIndex = iCount;
                            ddlState.SelectedIndex = iIndex;
                            break;
                        }
                        iCount++;
                    }
                    Get_Terr();
                }
            }
          
        }
    }
    private void Get_Terr()
    {
        ddlTerritoryName.DataSource = dsStockist;
        ddlTerritoryName.DataTextField = "Territory_name";
        ddlTerritoryName.DataValueField = "Territory_Code";
        ddlTerritoryName.DataBind();
        int iCount = 0, iIndex;
        foreach (ListItem item in ddlTerritoryName.Items)
        {
            if (Tr == item.ToString())
            {
                iIndex = iCount;
                ddlTerritoryName.SelectedIndex = iIndex;
                break;
            }
            iCount++;
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
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        if (txtSubDivision_Sname.Text.Length <= 10 && txtSubDivision_Name.Text.Length <= 50)
        {
            subdiv_sname = txtSubDivision_Sname.Text.Trim();
            subdiv_name = txtSubDivision_Name.Text.Trim();
            Dist_name = ddlState.SelectedItem.ToString().Trim();
 			Dist_cd = ddlState.SelectedValue;
            Terr_name = ddlTerritoryName.SelectedItem.ToString().Trim();
            Terr_Cd = ddlTerritoryName.SelectedValue;
             if (subdiv_sname != "" && subdiv_name != "")
            {
            if (Subdivision_Code == null)
            {
                // Add New Sub Division
                Town dv = new Town();
                int iReturn = dv.TownRecordAdd(divcode, subdiv_sname, subdiv_name, Dist_name, Dist_cd,Terr_name,Terr_Cd);

                if (iReturn > 0)
                {

                    // menu1.Status = "Sub Division created Successfully ";
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Created Successfully');</script>");
                    Resetall();
                }
                else if (iReturn == -2)
                {

                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Town Name Already Exist');</script>");
                    txtSubDivision_Name.Focus();
                }
                else if (iReturn == -3)
                {
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Town Code Already Exist');</script>");
                    txtSubDivision_Sname.Focus();
                }
            }
            else
            {
                // Update Sub Division
                Town dv = new Town();
                int subdivcode = Convert.ToInt16(Subdivision_Code);
                int iReturn = dv.TownRecordUpdate(subdivcode, subdiv_sname, subdiv_name, Dist_name, Dist_cd, Terr_name, Terr_Cd, divcode);
                if (iReturn > 0)
                {
                    // menu1.Status = "Sub Division Updated Successfully ";
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Updated Successfully');window.location='TownList.aspx';</script>");
                }
                else if (iReturn == -2)
                {

                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Town Name Already Exist');</script>");
                    txtSubDivision_Name.Focus();
                }
                else if (iReturn == -3)
                {
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Town Code Already Exist');</script>");
                    txtSubDivision_Sname.Focus();
                }
            }
			}
			else
            {
                if (subdiv_sname == "")
                {
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Enter Taluk Code');</script>");
                    txtSubDivision_Sname.Focus();
                }
                else if (subdiv_name=="")
                {

                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Enter Taluk Name');</script>");
                    txtSubDivision_Name.Focus();
                }
            }

        }
        else
        {
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('please Enter minimum length Value');</script>");
        }
    }
     private void Resetall()
     {
         txtSubDivision_Sname.Text = "";
         txtSubDivision_Name.Text = "";
         ddlState.SelectedIndex = 0;
         ddlTerritoryName.SelectedIndex = 0;
     }
     private void FillState(string div_code)
     {

             Town st = new Town();
             dsState = st.getStateProd(div_code);
             ddlState.DataTextField = "Distname";
             ddlState.DataValueField = "Dist_code";
             ddlState.DataSource = dsState;
             ddlState.DataBind();
         
     }
     private void GetTerritoryName()
     {
         Stockist sk = new Stockist();
         dsStockist = sk.getTer_Name(divcode);
         if (dsStockist.Tables[0].Rows.Count > 0)
         {
             ddlTerritoryName.DataTextField = "Territory_name";
             ddlTerritoryName.DataValueField = "Territory_code";
             ddlTerritoryName.DataSource = dsStockist;
             ddlTerritoryName.DataBind();
         }
     }

}