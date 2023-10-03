using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using Bus_EReport;

public partial class MasterFiles_DoctorCategory : System.Web.UI.Page
{
    #region "Declaration"
    DataSet dsDocCat = null;
    string divcode = string.Empty;
    string Doc_Cat_SName = string.Empty;
    string DocCatName = string.Empty;
    string Doc_Cat_Code = string.Empty;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int time;
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        Session["backurl"] = "DoctorCategoryList.aspx";
        divcode = Convert.ToString(Session["div_code"]);
        Doc_Cat_Code = Request.QueryString["Doc_Cat_Code"];
        if (!Page.IsPostBack)
        {
            menu1.Title = this.Page.Title;
            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);
            menu1.FindControl("btnBack").Visible = true;
          
            if (Doc_Cat_Code != "" && Doc_Cat_Code != null)
            {
                Doctor dv = new Doctor();
                dsDocCat = dv.getDocCat(divcode, Doc_Cat_Code);
                if (dsDocCat.Tables[0].Rows.Count > 0)
                {
                    txtDoc_Cat_SName.Text = dsDocCat.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                    txtDocCatName.Text = dsDocCat.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                }
            }
        }
        txtDoc_Cat_SName.Focus();
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
        Doctor dv = new Doctor();
        Doc_Cat_SName = txtDoc_Cat_SName.Text;
        DocCatName = txtDocCatName.Text;
        if (Doc_Cat_Code == null)
        {
            // Add New Doctor Category

            int iReturn = dv.RecordAdd(divcode, Doc_Cat_SName, DocCatName);

            if (iReturn > 0)
            {
                //menu1.Status = "Doctor Category Created Successfully ";
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Created Successfully');</script>");
                Resetall();
            }
            else if (iReturn == -2)
            {
                //menu1.Status = "Doctor Category already Exist!!";
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Name Already Exist');</script>");
                txtDocCatName.Focus();
            }
            else if (iReturn == -3)
            {
                //menu1.Status = "Doctor Category already Exist!!";
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Short Name Already Exist');</script>");
                txtDoc_Cat_SName.Focus();
            }
        }
        else
        {
            int DocCatCode = Convert.ToInt16(Doc_Cat_Code);
            int iReturn = dv.RecordUpdateCat(DocCatCode, Doc_Cat_SName, DocCatName, divcode);
            if (iReturn > 0)
            {              
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Updated Successfully');window.location='DoctorCategoryList.aspx';</script>");
            }
            else if (iReturn == -2)
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Name Already Exist');</script>");
                txtDocCatName.Focus();
            }
            else if (iReturn == -3)
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Short Name Already Exist');</script>");
            }
        }
    }
    private void Resetall()
    {
        txtDoc_Cat_SName.Text = "";
        txtDocCatName.Text = "";
    }
}