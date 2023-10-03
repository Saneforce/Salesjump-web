using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using Bus_EReport;

public partial class MasterFiles_DoctorSpeciality : System.Web.UI.Page
{
#region "Declaration"
    DataSet dsDoc = null;
    string Doc_Special_Code = string.Empty;
    string divcode = string.Empty;
    string DocSpe_SName = string.Empty;
    string DocSpeName = string.Empty;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int time;
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
        Session["backurl"] = "DoctorSpecialityList.aspx";
        Doc_Special_Code = Request.QueryString["Doc_Special_Code"];
        if (!Page.IsPostBack)
        {
         
            //menu1.Title = this.Page.Title;
            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);
            if (Doc_Special_Code != "" && Doc_Special_Code != null)
            {
                
                Doctor dv = new Doctor();
                dsDoc = dv.getDocSpe(divcode, Doc_Special_Code);

                if (dsDoc.Tables[0].Rows.Count > 0)
                {
                    txtDoc_Spe_SName.Text = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                    txtDocSpeName.Text = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                }

            }

        }
        txtDoc_Spe_SName.Focus();
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
        DocSpe_SName = txtDoc_Spe_SName.Text.Trim();
        DocSpeName = txtDocSpeName.Text.Trim();
		if (DocSpe_SName != "" && DocSpeName != "")
        {
        if ( Doc_Special_Code == null)
        {

            // Add New Doctor Speciality
            Doctor dv = new Doctor();
            int iReturn = dv.RecordAddDocSpl(divcode, DocSpe_SName, DocSpeName);

             if (iReturn > 0 )
            {
                //menu1.Status = "Doctor Speciality Created Successfully ";
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Created Successfully');</script>");
                Resetall();
            }
             else if (iReturn == -2)
             {               
                 ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Name Already Exist');</script>");
                 txtDocSpeName.Focus();
             }
             else if (iReturn == -3)
             {
                 ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Channel Code Already Exist');</script>");
                 txtDoc_Spe_SName.Focus();
             }
        }
        else
        {  
            // Update Doctor Speciality
            Doctor dv = new Doctor();
             int DocSplCode = Convert.ToInt16(Doc_Special_Code);
            int iReturn = dv.RecordUpdateDocSpl(DocSplCode, DocSpe_SName, DocSpeName, divcode);
             if (iReturn > 0 )
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Updated Successfully');window.location='DoctorSpecialityList.aspx';</script>");
            }
             else if (iReturn == -2)
             {
                 ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Name Already Exist');</script>");
                 txtDocSpeName.Focus();
             }
             else if (iReturn == -3)
             {
                 ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Channel Code Already Exist');</script>");
                 txtDoc_Spe_SName.Focus();
             }
        }
		}
		else
        {
            if (DocSpe_SName == "")
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Enter Channel Code...');</script>");
                txtDoc_Spe_SName.Focus();
            }
            else if (DocSpeName == "")
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Enter Channel Name...');</script>");
                txtDocSpeName.Focus();
            }
        }
     }
    private void Resetall()
    {
        txtDoc_Spe_SName.Text = "";
        txtDocSpeName.Text = "";
    }     
}