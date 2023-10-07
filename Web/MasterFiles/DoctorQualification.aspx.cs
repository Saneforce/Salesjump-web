using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using Bus_EReport;

public partial class MasterFiles_DoctorQualification : System.Web.UI.Page
{
    //Declaration
    #region "Declaration"
    DataSet dsDoc = null;
    string Doc_QuaCode = string.Empty;
    string divcode = string.Empty;
    string Doc_Qua_SName = string.Empty;
    string DocQuaName = string.Empty;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int time;
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["backurl"] = "DoctorQualificationList.aspx";
        divcode = Convert.ToString(Session["div_code"]);
        Doc_QuaCode = Request.QueryString["Doc_QuaCode"];
        if (!Page.IsPostBack)
        {
            menu1.Title = this.Page.Title;
            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);
          
            if (Doc_QuaCode != "" && Doc_QuaCode != null)
            {
                Doctor dv = new Doctor();
                dsDoc = dv.getDocQua(divcode, Doc_QuaCode);

                if (dsDoc.Tables[0].Rows.Count > 0)
                {
                   // txtDoc_Qua_SName.Text = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                    txtDocQuaName.Text = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                }
            }
        }
        txtDocQuaName.Focus();
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
      //  Doc_Qua_SName = txtDoc_Qua_SName.Text;
        DocQuaName = txtDocQuaName.Text;
        if (Doc_QuaCode == null)
        {

            //Add New Doctor Qualification
            Doctor dv = new Doctor();
            int iReturn = dv.RecordAddQua(divcode, Doc_Qua_SName, DocQuaName);

            if (iReturn > 0)
            {
                //menu1.Status = "Doctor Qualification Created Successfully ";
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Created Successfully');</script>");
                ResetAll();
            }
            else if (iReturn == -2)
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Name Already Exist');</script>");
                txtDocQuaName.Focus();
            }
        }
        else
        {
            // Update Doctor Qualification
            Doctor dv = new Doctor();
            int DocQuaCode = Convert.ToInt16(Doc_QuaCode);
            int iReturn = dv.RecordUpdateQua(DocQuaCode, Doc_Qua_SName, DocQuaName,divcode);
            if (iReturn > 0)
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Updated Successfully');window.location='DoctorQualificationList.aspx';</script>");
            }
            else if (iReturn == -2)
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Name Already Exist');</script>");
                txtDocQuaName.Focus();
            }
        }
    }
    private void ResetAll()
    {
        //txtDoc_Qua_SName.Text = "";
        txtDocQuaName.Text = "";
    }
}








