using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserInfo_ffapwd : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
       txtUsername.Focus();
    }
    protected void btnlogin_Click(object sender, EventArgs e)
    {
        if (txtUsername.Text == "administrator")
        {
            if (txtUsername.Text == "administrator" && txtPassword.Text == "saneforce_pmc05" && txtsecurty.Text == "54321")
            {
                Response.Redirect("ffagetpwd.aspx");
            }
            else
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Invalid Details');</script>");
            }
        }
        else if (txtUsername.Text == "adminquery")
        {
            if (txtUsername.Text == "adminquery" && txtPassword.Text == "saneforcequery" && txtsecurty.Text == "987654321")
            {
                Response.Redirect("~/MasterFiles/Query_Box_List.aspx");
            }
            else
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Invalid Details');</script>");
            }
        }
          else
        {
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Invalid Details');</script>");
        }
    }

}