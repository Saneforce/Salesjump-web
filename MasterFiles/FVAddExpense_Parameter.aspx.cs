using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
using System.Data.SqlClient;

public partial class MasterFiles_FVAddExpense_Parameter : System.Web.UI.Page
{
    string div_code = string.Empty;
    string sfCode = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        sfCode = Session["sf_code"].ToString();
        
        if (!Page.IsPostBack)
        {
            txtFixedAmount.Visible = false;
            lblFixedAmount.Visible = false;
            Session["backurl"] = "FVExpense_Parameter.aspx";
            menu1.Title = Page.Title;
        }
    }

    private void clearData()
    {
        txtParameter.Text = "";
        ddlExpenseType.SelectedIndex = 0;
        ddlLevel.SelectedIndex = 0;
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            int iReturn = -1;
            Territory terr = new Territory();

            iReturn = terr.ExpenseParameter_Insert(txtParameter.Text, ddlExpenseType.SelectedValue, div_code,txtFixedAmount.Text, ddlLevel.SelectedItem.Value);

            if (iReturn > 0)
            {
                //   menu1.Status = "Division Created Successfully ";
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Created Successfully');</script>");
               Server.Transfer("FVExpense_Parameter.aspx");
                // Resetall();
            }
            else if (iReturn == -2)
            {
                //  menu1.Status = "Division already Exist!!";
                ClientScript.RegisterStartupScript(GetType(), "TestAlert", "alert(\'Parameter Name already Exist.\');", true);
            }
            clearData();

        }
        catch (Exception ex)
        {

        }
    }

    protected void OnSelectedIndex_ddlExpenseType(object sender, EventArgs e)
    {
        if (ddlExpenseType.SelectedValue == "L")
        {
            txtFixedAmount.Visible = true;
            lblFixedAmount.Visible = true;
        }
        else
        {
            txtFixedAmount.Visible = false;
            lblFixedAmount.Visible = false;
        }
    }
}