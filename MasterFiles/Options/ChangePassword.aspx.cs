using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
using System.Drawing.Imaging;
public partial class MasterFiles_Options_ChangePassword : System.Web.UI.Page
{
    string div_code = string.Empty;
    DataSet dsSalesForce = null;
    string sfCode = string.Empty;
	string sf_type = string.Empty;
    string old_pwd = string.Empty;
    int iRet = -1;
	protected void Page_PreInit(object sender, EventArgs e)
        {
           sf_type = Session["sf_type"].ToString();
           if (sf_type == "3")
           {
               this.MasterPageFile = "~/Master.master";
           }
           else if(sf_type == "2")
           {
               this.MasterPageFile = "~/Master_MGR.master";
           }
 	   else if(sf_type == "1")
           {
               this.MasterPageFile = "~/Master_MR.master";
           }
        }
    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        if (Session["sf_code"] != null && Session["sf_code"].ToString() != "")
        {
            sfCode = Session["sf_code"].ToString();
        }

        if (!Page.IsPostBack)
        {
            FillManagers();
            FillColor();
            txtOldPwd.Focus();
            //FillSalesForce();
            //menu1.Title = this.Page.Title;
            //menu1.FindControl("btnBack").Visible = false;        

        }
        if (Session["sf_type"].ToString() == "1")
        {
            sfCode = Session["sf_code"].ToString();
            //UserControl_MR_Menu c1 =
            //    (UserControl_MR_Menu)LoadControl("~/UserControl/MR_Menu.ascx");
            //Divid.Controls.Add(c1);
            //c1.FindControl("btnBack").Visible = false;
            //c1.Title = this.Page.Title;
            lblFF.Visible = false;
            ddlFFType.Visible = false;
            ddlFieldForce.Visible = false;
        }
        else if (Session["sf_type"].ToString() == "2")
        {
            //UserControl_MGR_Menu c1 =
            //(UserControl_MGR_Menu)LoadControl("~/UserControl/MGR_Menu.ascx");
            //Divid.Controls.Add(c1);
            //c1.FindControl("btnBack").Visible = false;
            //c1.Title = this.Page.Title;
            lblFF.Visible = false;
            ddlFFType.Visible = false;
            ddlFieldForce.Visible = false;

        }
        else
        {
            //UserControl_MenuUserControl c1 =
            //(UserControl_MenuUserControl)LoadControl("~/UserControl/MenuUserControl.ascx");
            //Divid.Controls.Add(c1);
            //c1.FindControl("btnBack").Visible = false;
            //c1.Title = this.Page.Title;
           

        }
        FillColor();

    }

    private void FillSalesForce()
    {
        SalesForce sf = new SalesForce();
        dsSalesForce = sf.getSalesForcelist(div_code);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            ddlFieldForce.DataTextField = "sf_name";
            ddlFieldForce.DataValueField = "sf_code";
            ddlFieldForce.DataSource = dsSalesForce;
            ddlFieldForce.DataBind();
            
        }
        
    }

    private void FillColor()
    {
        int j = 0;


        foreach (ListItem ColorItems in ddlSF.Items)
        {
            //ddlFieldForce.Items[j].Selected = true;
            string bcolor = "#" + ColorItems.Text;
            ddlFieldForce.Items[j].Attributes.Add("style", "background-color:" + bcolor);

            j = j + 1;

        }
    }

    protected void ddlAlpha_SelectedIndexChanged(object sender, EventArgs e)
    {
        SalesForce sf = new SalesForce();
        dsSalesForce = sf.UserListTP_Alphasearch(div_code, "admin", ddlAlpha.SelectedValue);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            ddlFieldForce.DataTextField = "sf_name";
            ddlFieldForce.DataValueField = "sf_code";
            ddlFieldForce.DataSource = dsSalesForce;
            ddlFieldForce.DataBind();

            ddlSF.DataTextField = "des_color";
            ddlSF.DataValueField = "sf_code";
            ddlSF.DataSource = dsSalesForce;
            ddlSF.DataBind();

        }

    }

    private void FillManagers()
    {
        SalesForce sf = new SalesForce();

        if (ddlFFType.SelectedValue.ToString() == "1")
        {
            ddlAlpha.Visible = false;
            dsSalesForce = sf.UserListTP_Hierarchy(div_code, "admin");
        }
        else if (ddlFFType.SelectedValue.ToString() == "0")
        {
            FillSF_Alpha();
            ddlAlpha.Visible = true;
            dsSalesForce = sf.UserListTP_Alpha(div_code, "admin");
        }
        else if (ddlFFType.SelectedValue.ToString() == "2")
        {
            dsSalesForce = sf.UserList_HQ(div_code, "admin");
        }

        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            ddlFieldForce.DataTextField = "sf_name";
            ddlFieldForce.DataValueField = "sf_code";
            ddlFieldForce.DataSource = dsSalesForce;
            ddlFieldForce.DataBind();

            ddlSF.DataTextField = "des_color";
            ddlSF.DataValueField = "sf_code";
            ddlSF.DataSource = dsSalesForce;
            ddlSF.DataBind();

        }
    }

    private void FillSF_Alpha()
    {
        SalesForce sf = new SalesForce();
        dsSalesForce = sf.getSalesForcelist_Alphabet(div_code);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            ddlAlpha.DataTextField = "sf_name";
            ddlAlpha.DataValueField = "val";
            ddlAlpha.DataSource = dsSalesForce;
            ddlAlpha.DataBind();
            ddlAlpha.SelectedIndex = 0;
        }
    }

    //private void FillSalesForce()
    //{
        //AdminSetup adm = new AdminSetup();
        //dsSalesForce = adm.getMR_MGR(ddlFieldForce.SelectedValue.ToString());
        //if (dsSalesForce.Tables[0].Rows.Count > 0)
        //{
        //    grdSalesForce.Visible = true;
        //    grdSalesForce.DataSource = dsSalesForce;
        //    grdSalesForce.DataBind();
        //}
        //else
        //{
        //    grdSalesForce.Visible = true;
        //    grdSalesForce.DataSource = null;
        //    grdSalesForce.DataBind();
        //    btnSubmit.Visible = false;
        //    btnClear.Visible = false;

        //}
    //}

    protected void ddlFFType_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillManagers();
        FillColor();
    }



    protected void btnGo_Click(object sender, EventArgs e)
    {
        //if (ddlFieldForce.SelectedValue.ToString().Trim().Length > 0)
        //{
            if (Session["sf_type"].ToString() == "1" || Session["sf_type"].ToString() == "2")
            {
                sfCode = Session["sf_code"].ToString();
                
            }
            else
            {
                sfCode = ddlFieldForce.SelectedValue.ToString();
            }

            if (sfCode == "admin")
            {

                SalesForce sf = new SalesForce();
                dsSalesForce = sf.getAdmin_Password(div_code);
                if (dsSalesForce.Tables[0].Rows.Count > 0)
                {
                    old_pwd = dsSalesForce.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                }

                if (old_pwd.ToLower().Trim() == txtOldPwd.Text.ToLower().Trim())
                {
                    if (txtNewPwd.Text.ToLower().Trim() == txtConfirmPwd.Text.ToLower().Trim())
                    {
                        iRet = sf.AdminUpdatePassword(div_code, txtConfirmPwd.Text.Trim());
                        if (iRet > 0)
                            //  menu1.Status = "Password has been updated successfully";
                            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Password has been updated successfully');</script>");
                    }
                    else
                    {
                        //  menu1.Status = "New Password and Confirm Password does not match. Please try again";
                        ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('New Password and Confirm Password does not match. Please try again');</script>");
                        txtNewPwd.Focus();
                    }
                }
                else
                {
                    //menu1.Status = "Invalid Old Password";
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Invalid Old Password');</script>");
                    txtOldPwd.Focus();
                }
            }

            else
            {
            SalesForce sf = new SalesForce();
            dsSalesForce = sf.getSF_Password(sfCode);
            if (dsSalesForce.Tables[0].Rows.Count > 0)
            {
                 old_pwd = dsSalesForce.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
            }

            if (old_pwd.ToLower().Trim() == txtOldPwd.Text.ToLower().Trim())
            {
                if (txtNewPwd.Text.ToLower().Trim() == txtConfirmPwd.Text.ToLower().Trim())
                {
                    iRet = sf.UpdatePassword(sfCode, txtConfirmPwd.Text.Trim());
                    if(iRet > 0)
                      //  menu1.Status = "Password has been updated successfully";
                        ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Password has been updated successfully');</script>");
                }
                else
                {
                  //  menu1.Status = "New Password and Confirm Password does not match. Please try again";
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('New Password and Confirm Password does not match. Please try again');</script>");
                    txtNewPwd.Focus();
                }
            }
            else
            {
                //menu1.Status = "Invalid Old Password";
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Invalid Old Password');</script>");
                txtOldPwd.Focus();
            }
		}
        
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        txtConfirmPwd.Text = "";
        txtNewPwd.Text = "";
        txtOldPwd.Text = "";
        ddlFFType.SelectedIndex = 0;
        ddlFieldForce.SelectedIndex = 0;
    }
}