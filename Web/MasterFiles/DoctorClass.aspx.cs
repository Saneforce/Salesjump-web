using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using Bus_EReport;

public partial class MasterFiles_DoctorClass : System.Web.UI.Page
{
    #region "Declaration"
    DataSet dsDoc = null;
    string Doc_ClsCode = string.Empty;
    string divcode = string.Empty;
    string Doc_Cls_SName = string.Empty;
    string DocClsName = string.Empty;
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
        Session["backurl"] = "DoctorClassList.aspx";
        divcode = Convert.ToString(Session["div_code"]);
        Doc_ClsCode = Request.QueryString["Doc_ClsCode"];
        if (!Page.IsPostBack)
        {
            //menu1.Title = this.Page.Title;
            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);
           
            if (Doc_ClsCode != "" && Doc_ClsCode != null)
            {
                Doctor dv = new Doctor();
                dsDoc = dv.getDocCls(divcode, Doc_ClsCode);

                if (dsDoc.Tables[0].Rows.Count > 0)
                {
                    txtDoc_Cls_SName.Text = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                    txtDocClsName.Text = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                }

            }
           
        }
        txtDoc_Cls_SName.Focus();
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
        Doc_Cls_SName = txtDoc_Cls_SName.Text;
        DocClsName = txtDocClsName.Text;
        if (Doc_ClsCode == null)
        {
            // Add New Doctor Class
            Doctor dv = new Doctor();
            int iReturn = dv.RecordAddCls(divcode, Doc_Cls_SName, DocClsName);

             if (iReturn > 0 )
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Created Successfully');</script>");
                ResetAll();
            }
             else if (iReturn == -2)
             {
                 ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Name Already Exist');</script>");
                 txtDocClsName.Focus();
             }
             else if (iReturn == -3)
             {
                 ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Class Code Already Exist');</script>");
                 txtDoc_Cls_SName.Focus();
             }
        }
        else
        {
            Doctor dv = new Doctor();
            int DocClsCode = Convert.ToInt16(Doc_ClsCode);
            int iReturn = dv.RecordUpdateCls(DocClsCode, Doc_Cls_SName, DocClsName,divcode);
             if (iReturn > 0 )
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Updated Successfully');window.location='DoctorClassList.aspx';</script>");
            }
             else if (iReturn == -2)
             {
                 ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Name Already Exist');</script>");
                 txtDocClsName.Focus();
             }
             else if (iReturn == -3)
             {
                 ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Class Code Already Exist');</script>");
                 txtDoc_Cls_SName.Focus();
             }
        }
    }
    private void ResetAll()
    {
        txtDoc_Cls_SName.Text = "";
        txtDocClsName.Text = "";
    }
}