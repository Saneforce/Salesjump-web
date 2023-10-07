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

public partial class MasterFiles_Leave_Type_Creation : System.Web.UI.Page
{
    #region "Declaration"
    DataSet dsSubDiv = null;
    string Subdivision_Code = string.Empty;
    string divcode = string.Empty;
    string subdiv_sname = string.Empty;
    string subdiv_name = string.Empty;
    string Area_name = string.Empty;
    string Username = string.Empty;
    string Password = string.Empty;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int time;
    DataSet dsDSM = null;
    DataSet dsDivision = null;
    DataSet dsState = null;
    DataSet dsStockist = null;
    string state_cd = string.Empty;
    string sState = string.Empty;
    string Area_cd = string.Empty;
    string Town_name = string.Empty;
    string Town_code = string.Empty;
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
        Session["backurl"] = "Leave_Type_List.aspx";
        Subdivision_Code = Request.QueryString["Subdivision_Code"];

        if (!Page.IsPostBack)
        {

            //menu1.Title = this.Page.Title;
            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);
            txtSup_Sname.Focus();
            if (Subdivision_Code != "" && Subdivision_Code != null)
            {
                DSM sd = new DSM();
                dsSubDiv = sd.getLeaveType(divcode, Subdivision_Code);
                if (dsSubDiv.Tables[0].Rows.Count > 0)
                {

                    txtSup_Sname.Text = dsSubDiv.Tables[0].Rows[0].ItemArray.GetValue(0).ToString().Trim();
                    txtSupCon_Name.Text = dsSubDiv.Tables[0].Rows[0].ItemArray.GetValue(1).ToString().Trim();

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
        if (txtSup_Sname.Text.Length <= 50 && txtSupCon_Name.Text.Length <= 100)
        {
            subdiv_sname = txtSup_Sname.Text.Trim();
            subdiv_name = txtSupCon_Name.Text.Trim();


            if (Subdivision_Code == null)
            {

                // Add New Sub Division
                DSM dv = new DSM();
                int iReturn = dv.LeaveRecordAdd(divcode, subdiv_sname, subdiv_name);
                if (iReturn > 0)
                {

                    // menu1.Status = "Sub Division created Successfully ";
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Created Successfully');</script>");
                    Resetall();
                }
                else if (iReturn == -2)
                {

                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Leave Type  Name Already Exist');</script>");
                    txtSupCon_Name.Focus();
                }
                else if (iReturn == -3)
                {
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Leave Type Short Name Already Exist');</script>");
                    txtSup_Sname.Focus();
                }
            }
            else
            {
                // Update Sub Division
                DSM dv = new DSM();
                //int subdivcode = Convert.ToInt16(Subdivision_Code);
                int iReturn = dv.LeaveRecordUpdate(divcode,subdiv_sname, subdiv_name, Subdivision_Code);
                if (iReturn > 0)
                {
                    // menu1.Status = "Sub Division Updated Successfully ";
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Updated Successfully');window.location='Leave_Type_List.aspx';</script>");
                }
                else if (iReturn == -2)
                {

                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Leave Type  Already Exist');</script>");
                    txtSupCon_Name.Focus();
                }
                else if (iReturn == -3)
                {
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Leave Type Short Name Already Exist');</script>");
                    txtSup_Sname.Focus();
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
        txtSup_Sname.Text = "";
        txtSupCon_Name.Text = "";

    }


}