using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using Bus_EReport;

public partial class MasterFiles_Division_React : System.Web.UI.Page
{
    #region "Declaration"
    DataSet dsDivision = null;
    int Division_Code = 0;
    string div_code = string.Empty;
    string div_name = string.Empty;
    string div_addr1 = string.Empty;
    string div_addr2 = string.Empty;
    string div_city = string.Empty;
    string div_pin = string.Empty;
    string div_state = string.Empty;
    string div_sname = string.Empty;
    string div_alias = string.Empty;
    string state_code = string.Empty;
    string sChkLocation = string.Empty;
    string txtNewSlNo = string.Empty;
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Convert.ToString(Session["div_code"]);
        if (!Page.IsPostBack)
        {
            Session["backurl"] = "DivisionList.aspx";
            FillDivision();
            menu1.Title = this.Page.Title; 
        }

    }
    private void FillDivision()
    {
        Division dv = new Division();
        dsDivision = dv.getDivision_React();
        if (dsDivision.Tables[0].Rows.Count > 0)
        {
            grdDivision.Visible = true;
            grdDivision.DataSource = dsDivision;
            grdDivision.DataBind();
        }
        else
        {
            grdDivision.DataSource = dsDivision;
            grdDivision.DataBind();
        }
    }

    protected void grdDivision_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Reactivate")
        {
            Division_Code = Convert.ToInt16(e.CommandArgument);
            //Reactivate

            Division dv = new Division();
            int iReturn = dv.Reactivate_Divi(Division_Code);
            if (iReturn > 0)
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Reactivated Successfully');</script>");
            }
            else
            {
                // menu1.Status = "Unable to Deactivate";
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Unable to Reactivate');</script>");
            }
            FillDivision();
        }
    }
    protected void grdDivision_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdDivision.PageIndex = e.NewPageIndex;
        FillDivision();
    }
}