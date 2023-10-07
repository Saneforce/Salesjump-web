using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;

public partial class MasterFiles_Salesforce_Approval_Changes : System.Web.UI.Page
{
    #region "Declaration"
    DataSet dsSalesForce = null;
    DataSet dsDivision = null;
    DataSet dsUserList = null;
    DataSet dsSF = null;
    DataSet dsReport = null;
    string div_code = string.Empty;
    string sf_code = string.Empty;
    string Reporting_To_SF = string.Empty;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int time;
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        Session["backurl"] = "Approval_List.aspx";
        div_code = Session["div_code"].ToString();
        if (!Page.IsPostBack)
        {

            FillReporting();
            FillSalesForce_Reporting();
            FillSalesForce();
            //btnApproval_Click(sender,e);
            menu1.Title = this.Page.Title;
            menu1.FindControl("btnBack").Visible = true;
            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);
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
        time = serverTimeDiff.Minutes;

    } 
    // Function of Grid Values
    private void FillSalesForce()
    {
        if ((ddlMode.SelectedIndex > 0) && (ddlFilter.SelectedIndex > 0))
        {
            string sReport = ddlFilter.SelectedValue.ToString();
            SalesForce sf = new SalesForce();
            dsSalesForce = sf.getSalesForcelist_Reporting_Approval(div_code, sReport);
            if (dsSalesForce.Tables[0].Rows.Count > 0)
            {
                grdSalesForce.Visible = true;
                grdSalesForce.DataSource = dsSalesForce;
                grdSalesForce.DataBind();
                Fill_Approved_By();
            }
        }
    }

    private void FillSalesForce_Reporting()
    {
        string sReport = ddlFilter.SelectedValue.ToString();
        if ((ddlMode.SelectedIndex > 0) && (ddlFilter.SelectedIndex > 0))
        {

            SalesForce sf = new SalesForce();
            dsSalesForce = sf.getSalesForcelist_Reporting_Approval(div_code, sReport);
            if (dsSalesForce.Tables[0].Rows.Count > 0)
            {
                grdSalesForce.Visible = true;
                grdSalesForce.DataSource = dsSalesForce;
                grdSalesForce.DataBind();
                getState();
            }
        }
    }
    protected DataSet Fill_Approved_By()
    {
        string sReport = ddlFilter.SelectedValue.ToString();
            SalesForce sf = new SalesForce();
            dsSalesForce = sf.Change_Rep(sReport);
            return dsSalesForce;
        
    }
    protected void grdSalesForce_RowDataBound(object sender, GridViewRowEventArgs e)
    {
    }
    private void getState()
    {
        foreach (GridViewRow gridRow in grdSalesForce.Rows)
        {
            SalesForce sf = new SalesForce();
            DropDownList ddlNew = (DropDownList)gridRow.Cells[1].FindControl("ddlNew");
            string sMgr = ddlNew.SelectedValue.ToString();
            dsSalesForce = sf.Change_Rep(sMgr);
            if (dsSalesForce.Tables[0].Rows.Count > 0)
            {
                if (sMgr == dsSalesForce.Tables[0].Rows[0].ItemArray.GetValue(0).ToString())
                {
                    ddlNew.SelectedValue = sMgr;
                }

               // ddlNew.SelectedValue = dsSalesForce.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
            }
        }
    }

    // Select Current Manager Filter
    private void FillReporting()
    {
        SalesForce sf = new SalesForce();
        //dsSalesForce = sf.getUserList_Reporting(div_code);
        dsSalesForce = sf.UserList_Hierarchy(div_code, "admin");
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            ddlFilter.DataTextField = "Sf_Name";
            ddlFilter.DataValueField = "Sf_Code";
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

    protected void grdSalesForce_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdSalesForce.PageIndex = e.NewPageIndex;
        FillSalesForce_Reporting();
    }

    protected void ddlFilter_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ddlMode_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillReporting();
    }
    protected void btnGo_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        btnApproval.Visible = true;
        FillSalesForce();
        //FillSalesForce_Reporting();
    }

    protected void btnApproval_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        int iReturn = -1;
        foreach (GridViewRow gridRow in grdSalesForce.Rows)
        {
            Label lblSF_Code = (Label)gridRow.Cells[1].FindControl("lblSF_Code");
            sf_code = lblSF_Code.Text;
            DropDownList ddlNew = (DropDownList)gridRow.Cells[1].FindControl("ddlNew");
            string sMgr = ddlNew.SelectedValue.ToString();
            // DropDownList ddlMode = (DropDownList)gridRow.Cells[1].FindControl("ddlMode");
            string sMode = ddlMode.SelectedValue.ToString();

            //DropDownList ddlSF_Code = (DropDownList)gridRow.Cells[3].FindControl("ddlNew");
            //string DCR = ddlSF_Code.SelectedValue;
            //DropDownList ddlNew = (DropDownList)gridRow.Cells[3].FindControl("ddlNew");
            //string TP = ddlNew.SelectedValue.ToString();
            //string LDR = ddlNew.SelectedValue.ToString();
            //string Leave = ddlNew.SelectedValue.ToString();
            //string SS = ddlNew.SelectedValue.ToString();
            //string Expense = ddlNew.SelectedValue.ToString();
            //string Otr = ddlNew.SelectedValue.ToString(); 
            SalesForce sf = new SalesForce();
            //iReturn = sf.Update_App(sf_code, DCR, TP, LDR, Leave, SS, Expense, Otr);
            iReturn = sf.Update_App(sf_code, sMgr, sMode);
            //FillSalesForce_Reporting();


        }
        if (iReturn > 0)
        {
            //menu1.Status = "Approval Changes have been updated successfully";
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Updated successfully');window.location='Approval_List.aspx';</script>");
        }

        //FillSalesForce();
    }


}