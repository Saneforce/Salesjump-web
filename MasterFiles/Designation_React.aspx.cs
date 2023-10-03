using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using Bus_EReport;

public partial class MasterFiles_Designation_React : System.Web.UI.Page
{
    #region "Declaration"
    DataSet dsDesignation = null;
    DataSet dsDivision = null;
    DataSet dsdiv = null;
    int Designation_Code = 0;
    string Designation_Short_Name = string.Empty;
    string division_code = string.Empty;
    string sf_type = string.Empty;
    string Designation_Name = string.Empty;
    string Desig_Color = string.Empty;
    string type = string.Empty;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int time;
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
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
            fillDesignation();
            //menu1.Title = this.Page.Title;
            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);
        }

    }
    protected override void OnLoadComplete(EventArgs e)
    {
        ServerEndTime = DateTime.Now;
        TrackPageTime();
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
    public void TrackPageTime()
    {
        TimeSpan serverTimeDiff = ServerEndTime.Subtract(ServerStartTime);
        time = serverTimeDiff.Minutes;

    }

    private void fillDesignation()
    {
        Designation ds = new Designation();
        dsDesignation = ds.Designation_React(ddlDivision.SelectedValue.ToString());
        if (dsDesignation.Tables[0].Rows.Count > 0)
        {
            grdDesignation.Visible = true;
            grdDesignation.DataSource = dsDesignation;
            grdDesignation.DataBind();
        }
        else
        {
            grdDesignation.DataSource = dsDesignation;
            grdDesignation.DataBind();
        }
    }
    protected void grdDesignation_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Reactivate")
        {
            Designation_Code = Convert.ToInt16(e.CommandArgument);
            Designation des = new Designation();
            int iReturn = des.Reactivate(Designation_Code);
            if (iReturn > 0)
            {
                
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Reactivated Successfully');</script>");
            }
            else
            {
                // menu1.Status ="Unable to Reactivate";
                ClientScript.RegisterStartupScript(GetType(), "TestAlert", "alert(\'Unable to Reactivate.\');", true);
            }

            fillDesignation();
        }
       }
    protected void ddlDivision_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillDesignation();
    }
}
