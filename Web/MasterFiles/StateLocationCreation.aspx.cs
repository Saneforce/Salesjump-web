using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using Bus_EReport;

public partial class MasterFiles_StateLocationCreation : System.Web.UI.Page
{

    #region "Declaration"
    DataSet dsState = null;
    string strstatecode = string.Empty;
    int statecode = 0;
    string statename = string.Empty;
    string shortname = string.Empty;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int time;
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
       
        Session["backurl"] = "StateLocationList.aspx";        
        strstatecode = Request.QueryString["State_Code"];
     
        if (!Page.IsPostBack)
        {
            menu1.Title = this.Page.Title;
            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);
            //ClientScript.RegisterStartupScript(this.GetType(), "load", script, true);
            txtShortName.Focus();
            if (strstatecode != "" && strstatecode != null)
            {
                State st = new State();
                dsState = st.getStateEd(strstatecode);

                if (dsState.Tables[0].Rows.Count > 0)
                {
                    txtShortName.Text = dsState.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                    txtShortName.Enabled = false;                  
                    txtStateName.Text = dsState.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
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
        statename = txtStateName.Text;
        shortname = txtShortName.Text;
        if (strstatecode == null)
        {

            // Add New State
            State dv = new State();
            
            int iReturn = dv.RecordAdd(shortname, statename);

             if (iReturn > 0 )
            {
              //  menu1.Status = "State/Location created Successfully";
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Created Successfully');</script>");               
                Resetall();
            }

            else if (iReturn == -2)
            {
               // menu1.Status = "State/Location already Exist";
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('State Name Already Exist');</script>");
                txtStateName.Focus();
            }
             else if (iReturn == -3)
             {
                  ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Short Name Already Exist');</script>");
                  txtShortName.Focus();
             }
           
        }
        else
        {
            // Update State
            State dv = new State();
   
          
            int stcode = Convert.ToInt16(strstatecode);
            int iReturn = dv.RecordUpdate(stcode, statename);
             if (iReturn > 0 )
            {
               // menu1.Status = "State/Location Updated Successfully ";
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Updated Successfully');window.location='StateLocationList.aspx';</script>");
                
            }
             else if (iReturn == -2)
             {
                 // menu1.Status = "State/Location already Exist";
                 ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('State Name Already Exist');</script>");
                 txtStateName.Focus();
             }            
        }
    }
    private void Resetall()
    {
        txtShortName.Text = "";
        txtStateName.Text = "";
    }    
}