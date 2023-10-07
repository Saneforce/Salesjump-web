using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;

public partial class MasterFiles_Options_TalktoUs : System.Web.UI.Page
{
    string div_code = string.Empty;

    DataSet dsAdmin = null;
    int iRet = -1;
    string Sl_No = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();

        if (!Page.IsPostBack)
        {
            //menu1.Title = this.Page.Title;
            //menu1.FindControl("btnBack").Visible = false;

            AdminSetup adm = new AdminSetup();
            dsAdmin = adm.Get_talktous(div_code);
            txttalk.Focus();
            if (dsAdmin.Tables[0].Rows.Count > 0)
            {
                txttalk.Text = dsAdmin.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
            

            }

            if ((txttalk.Text != "") && (txttalk.Text != null))
            {
                btnSubmit.Text = "Update";
                // chkback.Checked = true;

            }
        }

    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (txttalk.Text.Trim().Length > 0)
        {
           
            AdminSetup adm = new AdminSetup();
            iRet = adm.talkAdd(txttalk.Text.Trim(), div_code);
            if (iRet > 0)
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Talk to us has been Updated Sucessfully');</script>");
            }
            btnSubmit.Text = "Update";


        }
    }
}