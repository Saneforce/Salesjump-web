using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data;
using Bus_EReport;
public partial class MasterFiles_MGR_Vacant_MR_Login : System.Web.UI.Page
{
    string div_code = string.Empty;
    string divcode = string.Empty;
    string sfCode = string.Empty;
    string SfName = string.Empty;
    DataSet dsLogin = null;
    DataSet dsSalesforce = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        divcode = Session["div_code"].ToString();
        sfCode = Session["sf_code"].ToString();
        // SfName = Session["Sf_Name"].ToString();
        if (!Page.IsPostBack)
        {
            menu1.Title = this.Page.Title;
            menu1.FindControl("btnBack").Visible = false;
            FillVacantMR();
          Vacant_List();
        }
    }
    private void FillVacantMR()
    {
        SalesForce dv = new SalesForce();
        dsSalesforce = dv.getVacantlist_MR(sfCode);
        if (dsSalesforce.Tables[0].Rows.Count > 0)
        {
            lstVacant.DataValueField = "SF_Code";
            lstVacant.DataTextField = "Sf_Name";
            lstVacant.SelectedIndex = 0;

            lstVacant.DataSource = dsSalesforce;
            lstVacant.DataBind();
        }
    }

    protected void btnLogin_Click(object sender, EventArgs e)
    {
        UserLogin ul = new UserLogin();
        dsLogin = ul.ProcessMgr_LoginMr(lstVacant.SelectedValue.ToString(), txtPassword.Text, sfCode);
        if (dsLogin.Tables[1].Rows.Count == 0)
        {
            txtPassword.Text = "";
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Invalid Password');</script>");
        }
        else
        {
            Session["sf_code"] = dsLogin.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
            // Session["div_code"] = dsLogin.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();
            Session["sf_name"] = dsLogin.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
            Session["sf_type"] = dsLogin.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();
            Session["Designation_Short_Name"] = dsLogin.Tables[0].Rows[0].ItemArray.GetValue(3).ToString();
            Session["Sf_HQ"] = dsLogin.Tables[0].Rows[0].ItemArray.GetValue(4).ToString();
            if (Session["sf_type"].ToString() == "1")
            {
                //   Server.Transfer("Default_MR.aspx");
                Response.Redirect("~/Default_MR.aspx");
            }
            else if (Session["sf_type"].ToString() == "2")
            {
                Response.Redirect("~/Default_MGR.aspx");
            }
        }
    }
    protected void lstVacant_SelectedIndexChanged(object sender, EventArgs e)
    {
        Vacant_List();
      
    }
    private void Vacant_List()
    {
        if (lstVacant.SelectedIndex != -1)
        {
            pnlSf.Visible = true;
            string SfName = lstVacant.SelectedItem.Text.ToString();
            lblLogin.Text = SfName;
        }
    }
}