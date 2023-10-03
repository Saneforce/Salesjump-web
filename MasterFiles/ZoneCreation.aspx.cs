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

public partial class MasterFiles_ZoneCreation : System.Web.UI.Page
{
    #region "Declaration"
    DataSet dsSubDiv = null;
    string Subdivision_Code = string.Empty;
    string divcode = string.Empty;
    string subdiv_sname = string.Empty;
    string subdiv_name = string.Empty;
    string Area_name = string.Empty;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int time;
    DataSet dsDivision = null;
    DataSet dsState = null;
    string state_cd = string.Empty;
    string sState = string.Empty;
    string Area_cd = string.Empty;
    string[] statecd;
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
        Session["backurl"] = "ZoneList.aspx";
        Subdivision_Code = Request.QueryString["Subdivision_Code"];

        if (!Page.IsPostBack)
        {

            //menu1.Title = this.Page.Title;
            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);
            FillState(divcode);
            txtSubDivision_Sname.Focus();
            if (Subdivision_Code != "" && Subdivision_Code != null)
            {
                Zone sd = new Zone();
                dsSubDiv = sd.getZone(divcode, Subdivision_Code);
                if (dsSubDiv.Tables[0].Rows.Count > 0)
                {
                    txtSubDivision_Sname.Text = dsSubDiv.Tables[0].Rows[0].ItemArray.GetValue(0).ToString().Trim();
                    txtSubDivision_Name.Text = dsSubDiv.Tables[0].Rows[0].ItemArray.GetValue(1).ToString().Trim();
                    string st = dsSubDiv.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();
                    ddlState.DataSource = dsState;
                    ddlState.DataTextField = "Areaname";
                    ddlState.DataValueField = "Area_code";
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
                }
            }

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
            Area_name = ddlState.SelectedItem.ToString();
            Area_cd = ddlState.SelectedValue;
            Zone dv_code = new Zone();
           if (subdiv_sname != "" && subdiv_name != "")
            {
            if (Subdivision_Code == null)
            {

                // Add New Sub Division
                Zone dv = new Zone();
                int iReturn = dv.ZoneRecordAdd(divcode, subdiv_sname, subdiv_name, Area_name, Area_cd);

                if (iReturn > 0)
                {

                    // menu1.Status = "Sub Division created Successfully ";
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Created Successfully');</script>");
                    Resetall();
                }
                else if (iReturn == -2)
                {

                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Zone Name Already Exist');</script>");
                    txtSubDivision_Name.Focus();
                }
                else if (iReturn == -3)
                {
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Zone Code Already Exist');</script>");
                    txtSubDivision_Sname.Focus();
                }
            }
            else
            {
                // Update Sub Division
                Zone dv = new Zone();
                int subdivcode = Convert.ToInt16(Subdivision_Code);
                int iReturn = dv.ZoneRecordUpdate(subdivcode, subdiv_sname, subdiv_name, Area_name, divcode,Area_cd);
                if (iReturn > 0)
                {
                    // menu1.Status = "Sub Division Updated Successfully ";
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Updated Successfully');window.location='ZoneList.aspx';</script>");
                }
                else if (iReturn == -2)
                {

                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Zone Name Already Exist');</script>");
                    txtSubDivision_Name.Focus();
                }
                else if (iReturn == -3)
                {
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Zone Code Already Exist');</script>");
                    txtSubDivision_Sname.Focus();
                }
            }
			}
			else
            {
                if (subdiv_sname == "")
                {
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Enter Zone Code');</script>");
                    txtSubDivision_Sname.Focus();
                }
                else if (subdiv_name == "")
                {
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Enter Zone Name...');</script>");
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
    }
    //AreaList Create by Giri 14-06-2016
    private void FillState(string div_code)
    {
        Zone dv = new Zone();
        dsDivision = dv.getStatePerDivision(div_code);
        if (dsDivision.Tables[0].Rows.Count > 0)
        {
            //int i = 0;
            state_cd = string.Empty;
            state_cd = dsDivision.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
            //statecd = sState.Split(',');
            //foreach (string st_cd in statecd)
            //{
            //    if (i == 0)
            //    {
            //        state_cd = state_cd + st_cd;
            //    }
            //    else
            //    {
            //        if (st_cd.Trim().Length > 0)
            //        {
            //            state_cd = state_cd + "," + st_cd;
            //        }
            //    }
            //    i++;
            //}

            Zone st = new Zone();
            dsState = st.getStateProd(div_code);
            ddlState.DataTextField = "Areaname";
            ddlState.DataValueField = "Area_code";
            ddlState.DataSource = dsState;
            ddlState.DataBind();
        }
    }
    
}