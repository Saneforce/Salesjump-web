using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using Bus_EReport;

public partial class MasterFiles_DoctorCampaign : System.Web.UI.Page
{

#region "Declaration"
DataSet dsDoc = null;
string DocSubCatCode = string.Empty;
string  divcode = string.Empty ;
string Doc_SubCat_SName = string.Empty;
string DocSubCatName = string.Empty;
string EffFrom = string.Empty;
string EffTo = string.Empty;
DateTime ServerStartTime;
DateTime ServerEndTime;
int time;
#endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["backurl"] = "DoctorCampaignList.aspx";
        divcode = Convert.ToString(Session["div_code"]);
        DocSubCatCode = Request.QueryString["Doc_SubCatCode"];
        if (!Page.IsPostBack)
        {           
            menu1.Title = this.Page.Title;
            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);
            if (DocSubCatCode != "" && DocSubCatCode != null)
            {
                Doctor dv = new Doctor();
                dsDoc = dv.getDocSubCat(divcode, DocSubCatCode);

                if (dsDoc.Tables[0].Rows.Count > 0)
                {
                    txtDoc_SubCat_SName.Text = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                    txtDocSubCatName.Text = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                    txtEffFrom.Text = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();
                    txtEffTo.Text = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(3).ToString();
                }

            }
        }
        txtDoc_SubCat_SName.Focus();
        
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
        Doc_SubCat_SName = txtDoc_SubCat_SName.Text;
        DocSubCatName = txtDocSubCatName.Text;
        //EffFrom = Convert.ToDateTime(txtEffFrom.Text);
        //EffTo = Convert.ToDateTime(txtEffTo.Text);
        if (DocSubCatCode == null)
        {

            // Add New Doctor Sub-Category
            Doctor dv = new Doctor();
            int iReturn = dv.RecordAddSubCat(divcode, Doc_SubCat_SName, DocSubCatName, Convert.ToDateTime(txtEffFrom.Text), Convert.ToDateTime(txtEffTo.Text));

             if (iReturn > 0 )
            {
                //menu1.Status = "Doctor Sub-Category Created Successfully ";
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Created Successfully');</script>");
                Resetall();
            }
             else if (iReturn == -2)
             {
                 // menu1.Status = "Doctor Sub-Category already Exist";
                 ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Campaign Name Already Exist');</script>");
                 txtDocSubCatName.Focus();
             }
             else if (iReturn == -3)
             {
                 // menu1.Status = "Doctor Sub-Category already Exist";
                 ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Short Name Already Exist');</script>");
                 txtDoc_SubCat_SName.Focus();
             }
        }
        else
        {
            // Update Doctor Sub-Category
            Doctor dv = new Doctor();
            int DocSCatCode = Convert.ToInt16(DocSubCatCode);
            int iReturn = dv.RecordUpdateSubCatnew(DocSCatCode, Doc_SubCat_SName, DocSubCatName, Convert.ToDateTime(txtEffFrom.Text), Convert.ToDateTime(txtEffTo.Text),divcode);
             if (iReturn > 0 )
            {
               // menu1.Status = "Doctor Sub-Category Updated Successfully ";
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Updated Successfully');window.location='DoctorCampaignList.aspx';</script>");
            }
             else if (iReturn == -2)
             {
                 // menu1.Status = "Doctor Sub-Category already Exist";
                 ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Campaign Name Already Exist');</script>");
                 txtDocSubCatName.Focus();
             }
             else if (iReturn == -3)
             {
                 // menu1.Status = "Doctor Sub-Category already Exist";
                 ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Short Name Already Exist');</script>");
                 txtDoc_SubCat_SName.Focus();
               
             }
        }
    }
    private void Resetall()
    {
        txtDoc_SubCat_SName.Text = "";
        txtDocSubCatName.Text = "";
        txtEffFrom.Text = "";
        txtEffTo.Text = "";
    }
           
}