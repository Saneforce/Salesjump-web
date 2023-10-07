using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;

public partial class MasterFiles_MultiDivision : System.Web.UI.Page
{
    DataSet dsSalesForce = null;
    DataSet dsDivision = null;
    DataSet dsSF = null;
    DataSet dsSalesForce1 = null;
    DataSet dsState = null;
    string sState = string.Empty;
    string div_code = string.Empty;
    string mainvalue = string.Empty;
    string[] statecd;
    string state_cd = string.Empty;
    string sf_code = string.Empty;
    string sf_name = string.Empty;
    string usr_name = string.Empty;
    string division_code = string.Empty;
    int state = -1;
    int div_count = 0;
    int ismultidivision = 0;
    int iReturn = -1;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int time;
    string sCmd = string.Empty;
    string hq = string.Empty;
    string search = string.Empty;
    string state_code = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        Session["backurl"] = "SalesForceList.aspx";
        if (!Page.IsPostBack)
        {
            Session["GetCmdArgChar"] = "All";
            menu1.Title = this.Page.Title;

            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);
            FillSalesForce();
        }

    }

    protected void ddlFields_SelectedIndexChanged(object sender, EventArgs e)
    {
        // search = Convert.ToInt32(ddlFields.SelectedValue);
        search = ddlFields.SelectedValue.ToString();
        grdSalesForce.PageIndex = 0;

        if (search == "Sf_UserName" || search == "Sf_Name" || search == "Sf_HQ")
        {
            txtsearch.Visible = true;
            btnSearch.Visible = true;
            ddlSrc.Visible = false;
        }
        else
        {
            txtsearch.Visible = false;
            ddlSrc.Visible = true;
            btnSearch.Visible = true;
        }

        if (search == "StateName")
        {
            FillState(div_code);
        }
        if (search == "Designation_Name")
        {
            FillDesignation();
        }
    }

    protected DataSet FillState()
    {
        string div_code = string.Empty;
        div_code = Session["div_code"].ToString();
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
        }
        return dsState;
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

    private void FillDesignation()
    {
        SalesForce sf = new SalesForce();
        dsSalesForce = sf.getsfDesignation(div_code);
        ddlSrc.DataTextField = "Designation_Short_Name";
        ddlSrc.DataValueField = "Designation_Code";
        ddlSrc.DataSource = dsSalesForce;
        ddlSrc.DataBind();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        Session["GetCmdArgChar"] = string.Empty;
        grdSalesForce.PageIndex = 0;
        Search();

    }

    private void Search()
    {
        search = ddlFields.SelectedValue.ToString();
        if (search == "Sf_UserName" || search == "Sf_Name" || search == "Sf_HQ")
        {
            FindSalesForce(ddlFields.SelectedValue, txtsearch.Text, Session["div_code"].ToString());
        }
        else if (search == "StateName")
        {
            txtsearch.Text = string.Empty;
            SalesForce sf = new SalesForce();
            dsSalesForce = sf.getSalesForce_st_mgr(div_code, ddlSrc.SelectedValue);
            if (dsSalesForce.Tables[0].Rows.Count > 0)
            {
                grdSalesForce.Visible = true;
                grdSalesForce.DataSource = dsSalesForce;
                grdSalesForce.DataBind();
                loaddivision();
            }
            else
            {
                grdSalesForce.DataSource = dsSalesForce;
                grdSalesForce.DataBind();
                loaddivision();
            }
        }
        else if (search == "Designation_Name")
        {

            txtsearch.Text = string.Empty;
            //ddlSrc.SelectedIndex = 0;
            SalesForce sf = new SalesForce();
            dsSalesForce = sf.getSalesForce_des_mgr(div_code, ddlSrc.SelectedValue);
            if (dsSalesForce.Tables[0].Rows.Count > 0)
            {
                grdSalesForce.Visible = true;
                grdSalesForce.DataSource = dsSalesForce;
                grdSalesForce.DataBind();
                loaddivision();
            }
            else
            {
                grdSalesForce.DataSource = dsSalesForce;
                grdSalesForce.DataBind();
                loaddivision();
            }
        }
    }


    protected void grdSalesForce_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "this.className='highlight'");
            e.Row.Attributes.Add("onmouseout", "this.className='normal'");
        }
    }


    private void loaddivision()
    {
        foreach (GridViewRow gridRow in grdSalesForce.Rows)
        {
            Label lblSfcode = (Label)gridRow.Cells[1].FindControl("lblSF_Code");
            TextBox TextBox1 = (TextBox)gridRow.Cells[5].FindControl("TextBox1");
            CheckBoxList CheckBoxList1 = (CheckBoxList)gridRow.Cells[5].FindControl("CheckBoxList1");
            SalesForce sf = new SalesForce();
            SalesForce sf1 = new SalesForce();
            dsSalesForce = sf.getSfDivision(lblSfcode.Text.ToString());
            dsSalesForce1 = sf1.getSfDivision_Main(lblSfcode.Text.ToString());
            if (dsSalesForce1.Tables[0].Rows.Count > 0)
            {
                mainvalue = dsSalesForce1.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
            }
            if (dsSalesForce.Tables[0].Rows.Count > 0)
            {
                string value = dsSalesForce.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                string[] strStateSplit = value.Split(',');
                foreach (string strstate in strStateSplit)
                {
                    if (strstate != "")
                    {
                        dsDivision.Tables[0].DefaultView.RowFilter = "Division_Code in ('" + strstate + "')";
                        DataTable dt = dsDivision.Tables[0].DefaultView.ToTable();
                        TextBox1.Text += dt.Rows[0].ItemArray.GetValue(1).ToString() + ", ";
                    }


                    string[] strchkstate;
                    strchkstate = TextBox1.Text.Split(',');
                    foreach (string chkst in strchkstate)
                    {
                        for (int iIndex = 0; iIndex < CheckBoxList1.Items.Count; iIndex++)
                        {
                            if (chkst.Trim() == CheckBoxList1.Items[iIndex].Text.Trim())
                            {
                                CheckBoxList1.Items[iIndex].Selected = true;
                            }
                            if (mainvalue == CheckBoxList1.Items[iIndex].Value.ToString())
                            {
                                CheckBoxList1.Items[iIndex].Enabled = false;
                            }
                        }
                    }
                }
            }
        }
    }
    protected void grdSalesForce_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            //Multi Select Division
            CheckBoxList check = (CheckBoxList)e.Row.FindControl("CheckBoxList1");
            TextBox txtSubDivision = (TextBox)e.Row.FindControl("TextBox1");
            HiddenField hdnSubDivisionId = (HiddenField)e.Row.FindControl("hdnDivisionId");

            Division dv = new Division();
            dsDivision = dv.getDivision(); //.getSubDiv(div_code);
            check.DataValueField = "Division_Code";
            check.DataTextField = "Division_Name";
            check.DataSource = dsDivision;
            check.DataBind();

        }

    }

    protected void CheckBoxList1_SelectedIndexChanged(object sender, EventArgs e)
    {

        string name1 = "";
        string id1 = "";
        GridViewRow gv = (GridViewRow)((Control)sender).NamingContainer;
        CheckBoxList check = (CheckBoxList)gv.FindControl("CheckBoxList1");
        TextBox txtSubDivision = (TextBox)gv.FindControl("TextBox1");
        HiddenField hdnDivisionId = (HiddenField)gv.FindControl("hdnDivisionId");
        txtSubDivision.Text = "";
        hdnDivisionId.Value = "";
        for (int i = 0; i < check.Items.Count; i++)
        {
            if (check.Items[i].Selected)
            {
                name1 += check.Items[i].Text + ",";
                id1 += check.Items[i].Value + ",";
            }
        }
        if (name1 == "")
        {
            // name1 = "NIL";
        }


        txtSubDivision.Text = name1.TrimEnd(',');
        hdnDivisionId.Value = id1.TrimEnd(',');
    }

    private void FillSalesForce()
    {
        SalesForce sf = new SalesForce();
        dsSalesForce = sf.FillSalesForcelist_Mgr(div_code);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            grdSalesForce.Visible = true;
            grdSalesForce.DataSource = dsSalesForce;
            grdSalesForce.DataBind();
            loaddivision();
        }
    }


    private void FindSalesForce(string sSearchBy, string sSearchText, string div_code)
    {
        string sFind = string.Empty;
        sFind = " AND a." + sSearchBy + " like '" + sSearchText + "%' AND " +
                " (a.Division_Code like '" + div_code + ',' + "%' " +
                " OR a.Division_Code like '%" + ',' + div_code + ',' + "%') ";

        SalesForce sf = new SalesForce();
        dsSalesForce = sf.FindSalesForcelist_Mgr(sFind);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            grdSalesForce.Visible = true;
            grdSalesForce.DataSource = dsSalesForce;
            grdSalesForce.DataBind();
            loaddivision();
        }
        else
        {
            grdSalesForce.DataSource = dsSalesForce;
            grdSalesForce.DataBind();
            loaddivision();
        }

    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);

        foreach (GridViewRow gridRow in grdSalesForce.Rows)
        {
            Label lblSfcode = (Label)gridRow.Cells[1].FindControl("lblSF_Code");
            TextBox TextBox1 = (TextBox)gridRow.Cells[5].FindControl("TextBox1");
            CheckBoxList CheckBoxList1 = (CheckBoxList)gridRow.Cells[5].FindControl("CheckBoxList1");
            division_code = "";
            div_count = 0;
            for (int k = 0; k < CheckBoxList1.Items.Count; k++)
            {
                if (CheckBoxList1.Items[k].Selected)
                {
                    div_count = div_count + 1;
                    division_code += CheckBoxList1.Items[k].Value + ",";
                }
            }

            if (div_count > 1)
            {
                ismultidivision = 1;
            }
            else
            {
                ismultidivision = 0;
            }
            SalesForce sf = new SalesForce();
            iReturn = sf.UpdateDivisionCode(lblSfcode.Text.ToString(), division_code, ismultidivision);

        }
        if (iReturn > 0)
        {
            // menu1.Status = "Listed Doctor detail(s) have been updated Successfully";
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Updated Successfully');</script>");
        }
    }
}
    