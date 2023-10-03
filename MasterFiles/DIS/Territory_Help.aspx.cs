using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterFiles_MR_Territory_Help : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void ddlLang_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlLang.SelectedValue == "0")
        {
            pnlhindhi.Visible = false;
            pnlenglish.Visible = false;
        }
        else if (ddlLang.SelectedValue == "1")
        {
            pnlhindhi.Visible = false;
            pnlenglish.Visible = true;
        }
        else if (ddlLang.SelectedValue == "2")
        {
            pnlhindhi.Visible = true;
            pnlenglish.Visible = false;
        }
        else if (ddlLang.SelectedValue == "3")
        {
            pnlhindhi.Visible = false;
            pnlenglish.Visible = false;
        }
        else if (ddlLang.SelectedValue == "4")
        {
            pnlhindhi.Visible = false;
            pnlenglish.Visible = false;
        }
        else if (ddlLang.SelectedValue == "5")
        {
            pnlhindhi.Visible = false;
            pnlenglish.Visible = false;
        }
       
    }
}