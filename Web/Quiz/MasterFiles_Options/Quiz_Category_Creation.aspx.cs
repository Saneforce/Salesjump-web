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
public partial class MasterFiles_Options_Quiz_Category_Creation : System.Web.UI.Page
{
    #region "Declaration"
    DataSet dsCategory = null;
   
    string divcode = string.Empty;
    string Category_sname = string.Empty;
    string Category_name = string.Empty;
    string sf_type = string.Empty;
     int Category_Id;

    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int time;
    #endregion
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
        Session["backurl"] = "Quiz_Category_List.aspx";

        Category_Id = Convert.ToInt32(Request.QueryString["Category_Id"]);
             
        if (!Page.IsPostBack)
        {

            //menu1.Title = this.Page.Title;
            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);
            txtCategory_Sname.Focus();

            if (Category_Id != 0 && Category_Id != null)
            {
                Product sd = new Product();
                dsCategory = sd.Get_Quiz_Category(divcode, Category_Id);

                if (dsCategory.Tables[0].Rows.Count > 0)
                {
                    txtCategory_Sname.Text = dsCategory.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                    txtCategory_Name.Text = dsCategory.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();

                    btnSubmit.Text = "Update";
                }
                else
                {
                    btnSubmit.Text = "Save";
                }
            }
            else
            {
                btnSubmit.Text = "Save";
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
        //System.Threading.Thread.Sleep(time);

        Category_sname = txtCategory_Sname.Text;
        Category_name = txtCategory_Name.Text;

        if (Category_Id == null || Category_Id == 0)
        {

            Product objProduct = new Product();

            int iReturn = objProduct.AddQuiz_Category_Details(Category_sname, Category_name, divcode);

            if (iReturn > 0)
            {

                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Created Successfully');window.location='Quiz_Category_List.aspx';</script>");
                //Resetall();
            }
            else if (iReturn == -2)
            {

                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Category Name Already Exist');</script>");
                txtCategory_Name.Focus();
            }
            else if (iReturn == -3)
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Category Short Name Already Exist');</script>");
                txtCategory_Sname.Focus();
            }
        }
        else
        {
            Product dv = new Product();
            int iReturn = dv.Update_Quiz_Category(Category_sname, Category_name, divcode, Category_Id);
            if (iReturn > 0)
            {

                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Updated Successfully');window.location='Quiz_Category_List.aspx';</script>");
                //Resetall();
            }
            else if (iReturn == -2)
            {

                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Category Name Already Exist');</script>");

                txtCategory_Name.Focus();
            }
            else if (iReturn == -3)
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Category Short Name Already Exist');</script>");
                txtCategory_Sname.Focus();
            }
        }
       

    }
    private void Resetall()
    {
        txtCategory_Sname.Text = "";
        txtCategory_Name.Text = "";
        btnSubmit.Text = "Save";
    }        
}