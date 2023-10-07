using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;

public partial class MasterFiles_BulkEditSalesForce : System.Web.UI.Page
{
    #region "Declaration"
    DataSet dsSalesForce = null;
    DataSet dsDivision = null;
    DataSet dsSF = null;
    DataSet dsState = null;
    DataSet dsReport = null;
    DataSet dsSubDivision = null;
    DataSet dsSub = null;
    string[] statecd;
    string state_cd = string.Empty;
    string div_code = string.Empty;
    string SF_Code = string.Empty;
    string SF_Name = string.Empty;
    string SF_UserName = string.Empty;
    string state_code = string.Empty;
    string ReportingMGR = string.Empty;
    string sState = string.Empty;
    string search = string.Empty;
    int i;
    int iReturn = -1;
    int iIndex;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    DateTime Sf_Joining_Date;
    int time;
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        if (!Page.IsPostBack)
        {
            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);
            FillReporting();
            Session["backurl"] = "SalesForceList.aspx";
            menu1.Title = this.Page.Title;
            
        }
        FillColor();
    }
    protected override void OnLoadComplete(EventArgs e)
    {

        ServerEndTime = DateTime.Now;
        TrackPageTime();//It will give you page load time  
    }
    public void TrackPageTime()
    {
        TimeSpan serverTimeDiff = ServerEndTime.Subtract(ServerStartTime);
        //ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('" + serverTimeDiff.Ticks + "');</script>");
        time = serverTimeDiff.Minutes;

    } 
    private void FillReporting()
    {
        SalesForce sf = new SalesForce();
      //  dsSalesForce = sf.getUserList_Reporting(div_code);
        dsSalesForce = sf.UserList_Hierarchy(div_code, "admin");
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            ddlFilter.DataTextField = "sf_name";
            ddlFilter.DataValueField = "sf_code";
            ddlFilter.DataSource = dsSalesForce;
            ddlFilter.DataBind();

            ddlSF.DataTextField = "des_color";
            ddlSF.DataValueField = "sf_code";
            ddlSF.DataSource = dsSalesForce;
            ddlSF.DataBind();

        }
    }
    private void FillColor()
    {
        int j = 0;


        foreach (ListItem ColorItems in ddlSF.Items)
        {
            string bcolor = "#" + ColorItems.Text;
            ddlFilter.Items[j].Attributes.Add("style", "background-color:" + bcolor);
          
            j = j + 1;

        }
    }

    private void FillSalesForce()
    {
        SalesForce sf = new SalesForce ();
        dsSalesForce = sf.getSalesForce_BulkEdit(div_code);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            grdSalesForce.Visible = true;
            grdSalesForce.DataSource = dsSalesForce;
            grdSalesForce.DataBind();
            getState();
            getDesignation();
        }
        else
        {
            grdSalesForce.DataSource = dsSalesForce;
            grdSalesForce.DataBind();
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

    protected DataSet FillDesignation()
    {
        SalesForce sf = new SalesForce();
        dsSalesForce = sf.getDesignation_SN(div_code);
        return dsSalesForce;
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        lblSelect.Visible = false;
        tblSalesForce.Visible = true;
        btnUpdate.Visible = true;
        //if (ddlFields.SelectedIndex > 0)
        //{
        //    FindSalesForce(ddlFields.SelectedValue, txtsearch.Text, Session["div_code"].ToString());
        //}
        //else if(ddlFilter.SelectedIndex == 0)
        //{
        //    FillSalesForce();
        //}

        search = ddlFields.SelectedValue.ToString();
        if (search == "UsrDfd_UserName" || search == "Sf_Name" || search == "Sf_HQ")
        {           
            FindSalesForce(ddlFields.SelectedValue, txtsearch.Text, Session["div_code"].ToString());           
        }
        if (search == "StateName")
        {          
       
            SalesForce sf = new SalesForce();
            dsSalesForce = sf.getSalesForce_st(div_code, ddlSrc.SelectedValue);
            if (dsSalesForce.Tables[0].Rows.Count > 0)
            {
                grdSalesForce.Visible = true;
                grdSalesForce.DataSource = dsSalesForce;
                grdSalesForce.DataBind();               
            }
            else
            {
                btnUpdate.Visible = false;
                grdSalesForce.DataSource = dsSalesForce;
                grdSalesForce.DataBind();
            }
        }
        if (search == "Designation_Name")
        {          
            SalesForce sf = new SalesForce();
            dsSalesForce = sf.getSalesForce_des(div_code, ddlSrc.SelectedValue);
            if (dsSalesForce.Tables[0].Rows.Count > 0)
            {
                grdSalesForce.Visible = true;
                grdSalesForce.DataSource = dsSalesForce;
                grdSalesForce.DataBind();               
            }
            else
            {
                btnUpdate.Visible = false;
                grdSalesForce.DataSource = dsSalesForce;
                grdSalesForce.DataBind();
            }
        }
        getState();
        getDesignation();
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
    private void FillDesignation_new()
    {
        SalesForce sf = new SalesForce();
        dsSalesForce = sf.getDesignation_SN(div_code);
        ddlSrc.DataTextField = "Designation_Name";
        ddlSrc.DataValueField = "Designation_Code";
        ddlSrc.DataSource = dsSalesForce;
        ddlSrc.DataBind();
    }
    private void FindSalesForce(string sSearchBy, string sSearchText, string div_code)
    {
        string sFind = string.Empty;
        sFind = " AND " + sSearchBy + " like '" + sSearchText + "%' AND  (a.Division_Code like '" + div_code + ',' + "%'  or " +
                     " a.Division_Code like '%" + ',' + div_code + ',' + "%') ";
        SalesForce sf = new SalesForce();
        dsSalesForce = sf.getSalesForce_BulkEditFind(sFind);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            grdSalesForce.Visible = true;
            grdSalesForce.DataSource = dsSalesForce;
            grdSalesForce.DataBind();
        }
        else
        {
            btnUpdate.Visible = false;
            grdSalesForce.DataSource = dsSalesForce;
            grdSalesForce.DataBind();
        }
        getState();
        getDesignation();

    }
    private void getState()
    {

        foreach (GridViewRow gridRow in grdSalesForce.Rows)
        {
            DropDownList ddlState = (DropDownList)gridRow.Cells[1].FindControl("State_Code");
            Label lblSFCode = (Label)gridRow.Cells[1].FindControl("lblSFCode");
            SF_Code = lblSFCode.Text.ToString();
            SalesForce sf = new SalesForce();
            DataSet dsState = sf.getState_BulkEdit(SF_Code, div_code);
            if (dsState.Tables[0].Rows.Count > 0)
            {
                ddlState.SelectedValue = dsState.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
            }
        }
    }
    private void getDesignation()
    {
        foreach (GridViewRow gridRow in grdSalesForce.Rows)
        {
            DropDownList ddlDesignation = (DropDownList)gridRow.Cells[1].FindControl("Designation_Code");
            Label lblSFCode = (Label)gridRow.Cells[1].FindControl("lblSFCode");
            SF_Code = lblSFCode.Text.ToString();
            SalesForce sf = new SalesForce();
            DataSet dsState = sf.getDesignation_BulkEdit(SF_Code, div_code);
            if (dsState.Tables[0].Rows.Count > 0)
            {
                ddlDesignation.SelectedValue = dsState.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
            }
        }
    }
    protected void btnGo_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        tblSalesForce.Visible = true;
        lblSelect.Visible = false;
        if (ddlFilter.SelectedIndex > 0)
        {
            FillSalesForce_Reporting();
        }
        else if(ddlFields.SelectedIndex == 0)
        {
            FillSalesForce();
        }
    }
    private void FillSalesForce_Reporting()
    {
        string sReport = ddlFilter.SelectedValue.ToString();
        SalesForce sf = new SalesForce();
        dsReport = sf.getReportingTo(sReport);
        ReportingMGR = dsReport.Tables[0].Rows[0].ItemArray.GetValue(0).ToString(); //His Reporting Mananger 
        //dsSalesForce = sf.getSalesForce_BulkEdit_Rpt(div_code, sReport, ReportingMGR); 


        //dsSalesForce = sf.UserList_BulkEditStartDate(div_code, sReport);

        DataTable dtUserList = new DataTable();
        dtUserList = sf.getUserListReportingToNew_for_all(div_code, sReport, 0, Session["sf_type"].ToString()); // 28-Aug-15 -Sridevi
        if (sReport == "admin")
        {
            dtUserList.Rows[0].Delete();
            dtUserList.Rows[0].Delete();
        }
        else
        {
            dtUserList.Rows[1].Delete();
        }
        if (dtUserList.Rows.Count > 0)
        {
            grdSalesForce.Visible = true;
            grdSalesForce.DataSource = dtUserList;
            grdSalesForce.DataBind();
        }
        else
        {
            btnUpdate.Visible = false;
            grdSalesForce.DataSource = dtUserList;
            grdSalesForce.DataBind();
        }
      
        getState();
        getDesignation();
    }

    protected void btnOk_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        SearchBy.Visible = true;
        ddlFields.Visible = true;
        //txtsearch.Visible = true;
        btnSearch.Visible = true;
        lblFilter.Visible = true;
        ddlFilter.Visible = true;
        btnGo.Visible = true;
        tblSalesForce.Visible = false;
        lblSelect.Visible = true;
        //FillSalesForce();
        for (i = 5; i < grdSalesForce.Columns.Count; i++)
        {
            grdSalesForce.Columns[i].Visible = false;
        }

        for (int j = 0; j < CblSFCode.Items.Count; j++)
        {
            for (i = 5; i < grdSalesForce.Columns.Count; i++)
            {
                if (CblSFCode.Items[j].Selected == true)
                {
                    if (grdSalesForce.Columns[i].HeaderText.Trim() == CblSFCode.Items[j].Text.Trim())
                    {
                        grdSalesForce.Columns[i].Visible = true;
                    }
                }
            }
        }

    }

    //protected void grdSalesForce_PageIndexChanging(object sender, GridViewPageEventArgs e)
    //{
    //    grdSalesForce.PageIndex = e.NewPageIndex;
    //    FillSalesForce();
    //}
    protected void btnClr_Click(object sender, EventArgs e)
    {
        for (i = 0; i < CblSFCode.Items.Count; i++)
        {
            CblSFCode.Items[i].Enabled = true;
            CblSFCode.Items[i].Selected = false;
        }

        grdSalesForce.DataSource = null;
        grdSalesForce.DataBind();
        tblSalesForce.Visible = false;
    }
    protected void grdSalesForce_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            CheckBoxList check = (CheckBoxList)e.Row.FindControl("subdivision_code");
            TextBox txtSubDivision = (TextBox)e.Row.FindControl("TextBox1");
            HiddenField hdnSubDivisionId = (HiddenField)e.Row.FindControl("hdnSubDivisionId");
            Label lblSFCode = (Label)e.Row.FindControl("lblSFCode");
            SF_Code = lblSFCode.Text.ToString();
            SalesForce dv = new SalesForce();
            dsSub = dv.getSubDiv_Selected(div_code, SF_Code);

            // DataTable dtDiv = new DataTable();
            string strDiv = dsSub.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
            string[] strDivSplit = strDiv.Split(',');
            //if (strDiv == "")
            //{
            //    TextBox1.Text = "NIL";
            //}
            foreach (string strsubdv in strDivSplit)
            {
                if (strsubdv.ToString() != "")
                {

                    dsSubDivision.Tables[0].DefaultView.RowFilter = "subdivision_code in ('" + strsubdv + "')";
                    DataTable dtDiv = dsSubDivision.Tables[0].DefaultView.ToTable();
                    txtSubDivision.Text += dtDiv.Rows[0].ItemArray.GetValue(2).ToString() + ",";
                }

                string[] strchkdiv;
                strchkdiv = txtSubDivision.Text.Split(',');
                foreach (string chkdiv in strchkdiv)
                {
                    for (iIndex = 0; iIndex < check.Items.Count; iIndex++)
                    {
                        if (chkdiv.Trim() == check.Items[iIndex].Text.Trim())
                        {
                            check.Items[iIndex].Selected = true;

                        }

                    }
                }
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
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        string cntrl = string.Empty;
        string prod_code = string.Empty;
        string strTextBox = string.Empty;
        string stxt = string.Empty;
        DateTime Dt;   
         foreach (GridViewRow gridRow in grdSalesForce.Rows)
        {
            for (i = 0; i < CblSFCode.Items.Count; i++)
            {
                if (CblSFCode.Items[i].Selected == true)
                {
                    cntrl = CblSFCode.Items[i].Value.ToString();

                    if (i != 4 && i != 6 && i != 7 && i != 8)
                    {                      
                        TextBox sTextBox = (TextBox)gridRow.Cells[1].FindControl(cntrl);
                        stxt = sTextBox.Text.ToString();
                        Label lblSFCode = (Label)gridRow.Cells[1].FindControl("lblSFCode");
                        SF_Code  = lblSFCode.Text.ToString();
                        strTextBox = strTextBox + CblSFCode.Items[i].Value + "= '" + stxt + "',";
                    }
                    else if (i != 1 && i != 2 && i != 3 && i != 5 && i != 7 && i != 8)
                    {
                        DropDownList sDDL = (DropDownList)gridRow.Cells[1].FindControl(cntrl);
                        stxt = sDDL.SelectedValue.ToString();
                        Label lblSFCode = (Label)gridRow.Cells[1].FindControl("lblSFCode");
                        SF_Code  = lblSFCode.Text.ToString();
                        strTextBox = strTextBox + CblSFCode.Items[i].Value + "= '" + stxt + "',";
                    }
                    else if (i == 7)
                    {
                        TextBox Date = (TextBox)gridRow.Cells[1].FindControl(cntrl);
                       // string Dt = Date.Text.ToString();
                        Label lblSFCode = (Label)gridRow.Cells[1].FindControl("lblSFCode");
                        SF_Code = lblSFCode.Text.ToString();
                        Dt = Convert.ToDateTime(Date.Text.ToString());
                        stxt = Dt.Month.ToString() + "-" + Dt.Day + "-" + Dt.Year;
                        strTextBox = strTextBox + CblSFCode.Items[i].Value + "= '" + stxt + "',";
                    }

                    else if (i == 8)
                    {

                        string SubId = "";
                        Label lblSFCode = (Label)gridRow.Cells[1].FindControl("lblSFCode");
                        SF_Code = lblSFCode.Text.ToString();

                        HiddenField hdnSubDivisionId = (HiddenField)gridRow.Cells[1].FindControl("hdnSubDivisionId");
                        CheckBoxList check = (CheckBoxList)gridRow.Cells[1].FindControl(cntrl);
                        for (int j = 0; j < check.Items.Count; j++)
                        {
                            if (check.Items[j].Selected)
                            {
                                if (check.Items[j].Text != "ALL")
                                {
                                    //name1 += chkst.Items[i].Text + ",";
                                    SubId += check.Items[j].Value + ",";
                                }

                            }
                        }
                        //SubId = cntrl;
                        stxt = SubId;

                        strTextBox = strTextBox + CblSFCode.Items[i].Value + "= '" + stxt + "',";
                    }
                }
            }

            if (strTextBox.Trim().Length > 0)
            {
                //strTextBox = strTextBox.Substring(0, strTextBox.Length - 1);
                strTextBox = strTextBox + " LastUpdt_Date = getdate() ";
                SalesForce sf= new SalesForce ();
                iReturn = sf.BulkEdit(strTextBox, SF_Code);
                strTextBox = "";
               
            }
        }

        if (iReturn > 0)
        {
            //menu1.Status = "Salesforce detail(s) have been updated Successfully";
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Updated Successfully');window.location='SalesForceList.aspx'; </script>");
        }

    }

    protected void ddlSrc_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ddlFields_SelectedIndexChanged(object sender, EventArgs e)
    {
           search = ddlFields.SelectedValue.ToString();
           txtsearch.Text = string.Empty;
           if (search == "UsrDfd_UserName" || search == "Sf_Name" || search == "Sf_HQ")
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
            FillDesignation_new();
        }    
    
    }

    protected void subdivision_code_SelectedIndexChanged(object sender, EventArgs e)
    {

        string name1 = "";
        string id1 = "";
        GridViewRow gv = (GridViewRow)((Control)sender).NamingContainer;
        CheckBoxList check = (CheckBoxList)gv.FindControl("subdivision_code");
        TextBox txtSubDivision = (TextBox)gv.FindControl("TextBox1");
        HiddenField hdnSubDivisionId = (HiddenField)gv.FindControl("hdnSubDivisionId");
        txtSubDivision.Text = "";
        hdnSubDivisionId.Value = "";
        for (int i = 0; i < check.Items.Count; i++)
        {
            if (check.Items[i].Selected)
            {
                name1 += check.Items[i].Text + ",";
                id1 += check.Items[i].Value + ",";
            }
        }
        //if (name1 == "")
        //{
        // //   name1 = "NIL";
        //} 


        txtSubDivision.Text = name1.TrimEnd(',');
        hdnSubDivisionId.Value = id1.TrimEnd(',');
    }

    protected void btnClose_Click(object sender, EventArgs e)
    {

    }
    protected DataSet FillCheckBoxList()
    {

        SubDivision dv = new SubDivision();
        dsSubDivision = dv.getSubDiv(div_code);
        return dsSubDivision;
    }
}