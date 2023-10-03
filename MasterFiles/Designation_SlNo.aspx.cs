using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using Bus_EReport;
public partial class MasterFiles_Designation_SlNo : System.Web.UI.Page
{
    #region "Declaration"
    DataSet dsDesignation = null;
    int Designation_Code = 0;
    string Designation_Short_Name = string.Empty;
    string Designation_Name = string.Empty;
    string Design_Code = string.Empty;
    string txtSlNo = string.Empty;
    string txtBaseSlNo = string.Empty;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    DataSet dsdiv = null;
    DataSet dsDivision = null;
    int time;
    string division_code = string.Empty;
    string sf_type = string.Empty;
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        menu1.Title = this.Page.Title;
       // menu1.FindControl("btnBack").Visible = false;
        ServerStartTime = DateTime.Now;
        base.OnPreInit(e);
        Session["backurl"] = "Designation.aspx";
        sf_type = Session["sf_type"].ToString();
        if (sf_type == "3")
        {
            division_code = Session["division_code"].ToString();
        }
        else
        {
            division_code = Session["div_code"].ToString();
        }
        if (!Page.IsPostBack)
        {
            Filldiv();
            ddlDivision.SelectedIndex = 0;
        FillBaseleve();
        FillManagers();
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
    private void Filldiv()
    {
        Division dv = new Division();
        if (sf_type == "3")
        {
            string[] strDivSplit = division_code.Split(',');
            foreach (string strdiv in strDivSplit)
            {
                if (strdiv != "")
                {
                    dsdiv = dv.getDivisionHO(strdiv);
                    ListItem liTerr = new ListItem();
                    liTerr.Value = dsdiv.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                    liTerr.Text = dsdiv.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                    ddlDivision.Items.Add(liTerr);
                }
            }
        }
        else
        {
            dsDivision = dv.getDivision_Name();
            if (dsDivision.Tables[0].Rows.Count > 0)
            {
                ddlDivision.DataTextField = "Division_Name";
                ddlDivision.DataValueField = "Division_Code";
                ddlDivision.DataSource = dsDivision;
                ddlDivision.DataBind();
            }
        }
    }
    private void FillBaseleve()
    {
        Designation des = new Designation();
        dsDesignation = des.getDesign_Baselevel(ddlDivision.SelectedValue.ToString());
        if (dsDesignation.Tables[0].Rows.Count > 0)
        {
            grdBaselevel.DataSource = dsDesignation;
            grdBaselevel.DataBind();
        }
        else
        {
            grdBaselevel.DataSource = dsDesignation;
            grdBaselevel.DataBind();
        }
    }
    private void FillManagers()
    {
        Designation des = new Designation();
        dsDesignation = des.getDesign_Managerlevel(ddlDivision.SelectedValue.ToString());
        if (dsDesignation.Tables[0].Rows.Count > 0)
        {
            grdmanager.DataSource = dsDesignation;
            grdmanager.DataBind();
        }
        else
        {
            grdmanager.DataSource = dsDesignation;
            grdmanager.DataBind();
        }
             
    }
  
    protected void btnbase_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        // Save
        foreach (GridViewRow gridRow in grdBaselevel.Rows)
        {
            TextBox txtBasno = (TextBox)gridRow.Cells[3].FindControl("txtBaseSlNo");
           // txtBaseSlNo = txtBaseSlno.Text;

            Label Designation_Code = (Label)gridRow.Cells[0].FindControl("lblDesign_code");
            Design_Code = Designation_Code.Text;
            // Update Division
            Designation des = new Designation();
            int iReturn = des.Update_BaselevelSno(Design_Code, txtBasno.Text, ddlDivision.SelectedValue.ToString());
            if (iReturn > 0)
            {
               
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Sl No Updated Successfully');</script>");
            }
            else if (iReturn == -2)
            {
                //  menu1.Status = "SlNo could not be updated!!";
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('SlNo could not be updated');</script>");
            }
        }
    }
    protected void btnManager_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        // Save
        foreach (GridViewRow gridRow in grdmanager.Rows)
        {
            TextBox txtSNo = (TextBox)gridRow.Cells[3].FindControl("txtManSlNo");
           

            Label Designation_Code = (Label)gridRow.Cells[0].FindControl("Des_Code");
            Design_Code = Designation_Code.Text;         
            Designation des = new Designation();
            int iReturn = des.Update_ManagerSno(Design_Code, txtSNo.Text, ddlDivision.SelectedValue.ToString());
            if (iReturn > 0)
            {
                //menu1.Status = "Sl No Updated Successfully ";
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Sl No Updated Successfully');</script>");
            }
            else if (iReturn == -2)
            {
                //  menu1.Status = "SlNo could not be updated!!";
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('SlNo could not be updated');</script>");
            }
        }
    }
    protected void ddlDivision_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillBaseleve();
        FillManagers();
    }
}