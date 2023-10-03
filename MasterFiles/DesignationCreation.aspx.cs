using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using Bus_EReport;

public partial class MasterFiles_DesignationCreation : System.Web.UI.Page
{
    #region "Declaration"
    DataSet dsDesignation = null;
    DataSet dsdiv = null;
    DataSet dsDivision = null;
    string strDesignation_Code = string.Empty;
    int statecode = 0;
    string Designation_Name = string.Empty;
    string Designation_Short_Name = string.Empty;
    string Desig_Color = string.Empty;
    string sf_type = string.Empty;
    string dis_type = string.Empty;
    string div_code = string.Empty;
    string Division_Code = string.Empty;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int time;
    #endregion
	
    protected override void OnPreInit(EventArgs e)
    {
        base.OnPreInit(e);
        sf_type = Session["sf_type"].ToString();
        if (sf_type == "3")
        {
            this.MasterPageFile = "~/Master.master";
        }
        else if (sf_type == "2")
        {
            this.MasterPageFile = "~/Master_MGR.master";
        }
        else if (sf_type == "1")
        {
            this.MasterPageFile = "~/Master_MR.master";
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["backurl"] = "Designation.aspx";
        strDesignation_Code = Request.QueryString["Designation_Code"];
        Division_Code = Request.QueryString["Division_Code"];
        sf_type = Session["sf_type"].ToString();
        if (sf_type == "3")
        {
            div_code = Session["division_code"].ToString();
        }
        else
        {
            div_code = Session["div_code"].ToString();
        }
        if (!Page.IsPostBack)
        {
            //menu1.Title = this.Page.Title;
            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);
            Filldiv();
            if (strDesignation_Code != "" && strDesignation_Code != null)
            {
                Designation des = new Designation();
                dsDesignation = des.getDesignationEd(strDesignation_Code, Division_Code);

                if (dsDesignation.Tables[0].Rows.Count > 0)
                {
                    txtShortName.Text = dsDesignation.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                    txtDesignation.Text = dsDesignation.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                    txtColor.Text = dsDesignation.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();
                    ddlDesType.SelectedValue = dsDesignation.Tables[0].Rows[0].ItemArray.GetValue(3).ToString();
                    ddlDivision.SelectedValue = dsDesignation.Tables[0].Rows[0].ItemArray.GetValue(4).ToString();
                    if (Convert.ToInt32(dsDesignation.Tables[0].Rows[0].ItemArray.GetValue(5).ToString()) > 0)
                    {
                        ddlDesType.Enabled = false;
                        ddlDivision.Enabled = false;
                    }
                }
            }

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
            string[] strDivSplit = div_code.Split(',');
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
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        if (txtDesignation.Text.Length <= 100 && txtShortName.Text.Length <= 100)
        {
            Designation_Name = txtDesignation.Text;
            Designation_Short_Name = txtShortName.Text;
            Desig_Color = txtColor.Text;

            if (strDesignation_Code == null)
            {

                // Add New State
                Designation des = new Designation();
                int iReturn = des.RecordAdd(Designation_Short_Name, Designation_Name, Desig_Color, ddlDesType.SelectedValue.ToString(), ddlDivision.SelectedValue.ToString());

                if (iReturn > 0)
                {
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Created Successfully');</script>");
                    Resetall();
                }

                if (iReturn == -2)
                {
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Name Already Exist');</script>");
                    txtDesignation.Focus();
                }
                else if (iReturn == -3)
                {
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Short Name Already Exist');</script>");

                }
            }
            else
            {
                // Update State
                Designation des = new Designation();
                int Designation_Code = Convert.ToInt16(strDesignation_Code);
                int iReturn = des.RecordUpdate(Designation_Code, Designation_Short_Name, Designation_Name, Desig_Color, ddlDesType.SelectedValue.ToString(), ddlDivision.SelectedValue.ToString());
                if (iReturn > 0)
                {
                    // menu1.Status = "State/Location Updated Successfully ";
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Updated Successfully');window.location='Designation.aspx';</script>");
                }
                else if (iReturn == -2)
                {
                    //  menu1.Status = "State/Location already Exist";
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Already Exist');</script>");
                }
            }
        }
        else
        {
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('please Enter minimum length Value');</script>");
        }
    }
    private void Resetall()
    {
        txtShortName.Text = "";
        txtDesignation.Text = "";
    }
}