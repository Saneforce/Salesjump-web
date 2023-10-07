using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;

public partial class Reports_TP_Status_Report : System.Web.UI.Page
{

    DataSet dsDivision = null;
    DataSet dsState = null;
    DataSet dsProd = null;
    string sState = string.Empty;
    string div_code = string.Empty;
    string[] statecd;
    string state_cd = string.Empty;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        if (!Page.IsPostBack)
        {
            menu1.Title = this.Page.Title;
            FillState(div_code);
            menu1.FindControl("btnBack").Visible = false;
        }

    }

    private void FillState(string div_code)
    {
        Division dv = new Division();
        dsDivision = dv.getStatePerDivision(div_code);
        if (dsDivision.Tables[0].Rows.Count > 0)
        {
            int i = 0;
            state_cd = string.Empty;
            sState = dsDivision.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
            statecd = sState.Split(',');
            foreach (string st_cd in statecd)
            {
                if (i == 0)
                {
                    state_cd = state_cd + st_cd;
                }
                else
                {
                    if (st_cd.Trim().Length > 0)
                    {
                        state_cd = state_cd + "," + st_cd;
                    }
                }
                i++;
            }

            State st = new State();
            dsState = st.getStateName(state_cd);
            ddlState.DataTextField = "statename";
            ddlState.DataValueField = "state_code";
            ddlState.DataSource = dsState;
            ddlState.DataBind();
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string sURL = "rptTPStatus.aspx?state_code=" + ddlState.SelectedValue.ToString() + "&vacant=" + chkVacant.Checked + "&cur_month=" + ddlMonth.SelectedValue.ToString() + "&cur_year=" + ddlYear.SelectedValue.ToString();
        string newWin = "window.open('" + sURL + "',null,'resizable=yes,toolbar=no,scrollbars=yes,menubar=no,status=no,width=1000,height=800,left=100,top=100');";
        ClientScript.RegisterStartupScript(this.GetType(), "pop", newWin, true);
    }
}