using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;

public partial class MasterFiles_VacantSFList : System.Web.UI.Page
{

    DataSet dsSalesForce = null;
    string div_code = string.Empty;
    string sCmd = string.Empty;
    string search = string.Empty;
    string state_cd = string.Empty;
    string sState = string.Empty;
    string[] statecd;
    DataSet dsDivision = null;
    DataSet dsState = null;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int time;
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["backurl"] = "SalesForceList.aspx";
        div_code = Session["div_code"].ToString();
        if (!Page.IsPostBack)
        {
            FillSalesForce();
            menu1.Title = this.Page.Title;
            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);
            ddlFieldForceType.Focus();
            FillSF_Alpha();
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
    private void FillSalesForce()
    {
        SalesForce sf = new SalesForce();
        if (ddlFieldForceType.SelectedValue == "0")
        {
            dsSalesForce = sf.getSalesForceVaclist(div_code);
        }
        else
        {
            dsSalesForce = sf.getSalesForceVaclist(div_code,ddlFieldForceType.SelectedValue);
        }
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            grdSalesForce.Visible = true;
            grdSalesForce.DataSource = dsSalesForce;
            grdSalesForce.DataBind();
        }
        else
        {
            grdSalesForce.DataSource = dsSalesForce;
            grdSalesForce.DataBind();
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
            FillSalesForce();
     
    }

    protected void grdSalesForce_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Activate")
        {
            string sf_code = Convert.ToString(e.CommandArgument);

            //Deactivate
            SalesForce sf = new SalesForce();
            int iReturn = sf.VacActivate(sf_code);
            if (iReturn > 0)
            {
              //  menu1.Status = "SalesForce has been Activated Successfully";
                //ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Activated Successfully');</script>");
                //Response.Redirect("~/MasterFiles/ReMapReportingStructure.aspx?reporting_to=" + sf_code);
                Response.Write("<script>alert('Activated Successfully') ; location.href='~/MasterFiles/ReMapReportingStructure.aspx?reporting_to=" + sf_code  + "'</script>");
              
            }
            else
            {
               // menu1.Status = "Unable to Activate";
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Unable to Activate');</script>");
            }
            FillSalesForce();
        }
        if (e.CommandName == "Deactivate")
        {
            string sf_code = Convert.ToString(e.CommandArgument);

            //Deactivate

            SalesForce sf = new SalesForce();
            int iReturn = sf.DeActivate(sf_code);

            if (iReturn > 0)
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Deactivated Successfully');</script>");
            }
            else
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Unable to Deactivate');</script>");
            }
            FillSalesForce();
        }
    }

    protected void grdSalesForce_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdSalesForce.PageIndex = e.NewPageIndex;
        sCmd = Session["GetCmdArgChar"].ToString();

        if (sCmd == "All")
        {
            FillSalesForce();
        }
        else if (sCmd != "")
        {
            FillSalesForce(sCmd);
        }
        else if (txtsearch.Text != "")
        {
            FindSalesForce(ddlFields.SelectedValue, txtsearch.Text, Session["div_code"].ToString());
        }
        else if (ddlSrc.SelectedIndex > 0)
        {
            Search();
        }
    }

    protected void ddlFields_SelectedIndexChanged(object sender, EventArgs e)
    {
        search = ddlFields.SelectedValue.ToString();
        txtsearch.Text = string.Empty;
        grdSalesForce.PageIndex = 0;

        if (search == "UsrDfd_UserName" || search == "Sf_Name" || search == "Sf_HQ" || search == "sf_emp_id")
        {
            txtsearch.Visible = true;
            btnGo.Visible = true;
            ddlSrc.Visible = false;
            txtsearch.Focus();
        }
        else
        {
            txtsearch.Visible = false;
            ddlSrc.Visible = true;
            btnGo.Visible = true;
        }

        if (search == "StateName")
        {
            FillState(div_code);
        }

        if (search == "Designation_Name")
        {
            FillDesignation();
            ddlSrc.Focus();
        }
    }

    private void FillDesignation()
    {
        SalesForce sf = new SalesForce();
        dsSalesForce = sf.getDesignation_SN(div_code);
        ddlSrc.DataTextField = "Designation_Name";
        ddlSrc.DataValueField = "Designation_Code";
        ddlSrc.DataSource = dsSalesForce;
        ddlSrc.DataBind();
    }
    protected void ddlSrc_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void btnGo_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        Session["GetCmdArgChar"] = string.Empty;
        grdSalesForce.PageIndex = 0;
        Search();
    }

    private void Search()
    {
        search = ddlFields.SelectedValue.ToString();

        if (search == "UsrDfd_UserName" || search == "Sf_Name" || search == "Sf_HQ" || search == "sf_emp_id")
        {
            FindSalesForce(ddlFields.SelectedValue, txtsearch.Text, Session["div_code"].ToString());
        }

        else if (search == "StateName")
        {
            txtsearch.Text = string.Empty;
            SalesForce sf = new SalesForce();
            dsSalesForce = sf.getSalesForce_st_vacant(div_code, ddlSrc.SelectedValue);
            if (dsSalesForce.Tables[0].Rows.Count > 0)
            {
                grdSalesForce.Visible = true;
                grdSalesForce.DataSource = dsSalesForce;
                grdSalesForce.DataBind();
            }
            else
            {
                grdSalesForce.DataSource = dsSalesForce;
                grdSalesForce.DataBind();
            }
        }
        else if (search == "Designation_Name")
        {
            txtsearch.Text = string.Empty;
            SalesForce sf = new SalesForce();
            dsSalesForce = sf.getSalesForce_desVacant(div_code, ddlSrc.SelectedValue);
            if (dsSalesForce.Tables[0].Rows.Count > 0)
            {
                grdSalesForce.Visible = true;
                grdSalesForce.DataSource = dsSalesForce;
                grdSalesForce.DataBind();
            }
            else
            {
                grdSalesForce.DataSource = dsSalesForce;
                grdSalesForce.DataBind();
            }
        }
    }

    private void FindSalesForce(string sSearchBy, string sSearchText, string div_code)
    {
        string sFind = string.Empty;
        sFind = " AND a." + sSearchBy + " like '" + sSearchText + "%' AND a.Division_Code = '" + div_code + ",' ";

        SalesForce sf = new SalesForce();
        dsSalesForce = sf.FindSalesForceVacant(sFind);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            grdSalesForce.Visible = true;
            grdSalesForce.DataSource = dsSalesForce;
            grdSalesForce.DataBind();
        }
        else
        {
            grdSalesForce.DataSource = dsSalesForce;
            grdSalesForce.DataBind();
        }
    }

    protected void dlAlpha_ItemCommand(object source, DataListCommandEventArgs e)//done by resh
    {
        LinkButton lnk = (LinkButton)e.CommandSource;
        string sCmd = e.CommandArgument.ToString();
        Session["GetCmdArgChar"] = sCmd;

        if (sCmd == "All")
        {
            grdSalesForce.PageIndex = 0;
            FillSalesForce();
        }
        else
        {
            grdSalesForce.PageIndex = 0;
            FillSalesForce(sCmd);
        }
    }

    private void FillSalesForce(string sAlpha)
    {
        SalesForce sf = new SalesForce();
        dsSalesForce = sf.getSalesForceAlpha_vacant(div_code, sAlpha);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            grdSalesForce.Visible = true;
            grdSalesForce.DataSource = dsSalesForce;
            grdSalesForce.DataBind();
        }
    }

    private void FillSF_Alpha()
    {
        SalesForce sf = new SalesForce();
        dsSalesForce = sf.getSalesForcelist_Alphabet_ForVacant(div_code);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            dlAlpha.DataSource = dsSalesForce;
            dlAlpha.DataBind();
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
            dsState = st.getState(state_cd);
            ddlSrc.DataTextField = "statename";
            ddlSrc.DataValueField = "state_code";
            ddlSrc.DataSource = dsState;
            ddlSrc.DataBind();
        }
    }

}