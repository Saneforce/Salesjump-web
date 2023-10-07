using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;

public partial class MasterFiles_App_CallRemarks : System.Web.UI.Page
{
    #region Declaration;
    string div_code = string.Empty;
    string sf_type = string.Empty;
    string sCmd = string.Empty;
    string Remarks_Id = string.Empty;
    int Id = 0;
    DataSet dsdiv = new DataSet();
    DataSet dsRemarks = null;
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
        sf_type = Session["sf_type"].ToString();
        div_code = Convert.ToString(Session["div_code"]);

        if (sf_type == "3")
        {
            div_code = Session["div_code"].ToString();
        }
        else
        {
            div_code = Session["div_code"].ToString();
        }

        if (!Page.IsPostBack)
        {
            if (Session["sf_type"].ToString() == "" || Session["sf_type"].ToString() == null || Session["sf_type"].ToString() == "3")
            {
                FillRemarks();
                //menu1.Title = this.Page.Title;
                //menu1.FindControl("btnBack").Visible = false;
                ServerStartTime = DateTime.Now;
                base.OnPreInit(e);
            }
        }

    }

    protected override void OnLoadComplete(EventArgs e)
    {
        ServerEndTime = DateTime.Now;
        TrackPageTime();
    }
    public void TrackPageTime()
    {
        TimeSpan serverTimeDiff = ServerEndTime.Subtract(ServerStartTime);
        time = serverTimeDiff.Minutes;
    }

    private void FillRemarks()
    {
        AdminSetup adm = new AdminSetup();
        dsRemarks = adm.getRemarks(div_code);
        if (dsRemarks.Tables[0].Rows.Count > 0)
        {
            btnaddnew.Visible = false;
            txtaddnew.Visible = false;
            ddlCountries2.Visible = false;
            btnUpdate.Visible = true;
            grdRemarks.DataSource = dsRemarks;
            grdRemarks.DataBind();
        }
        else
        {
            btnaddnew.Visible = true;
            ddlCountries2.Visible = true;
            txtaddnew.Text = string.Empty;
            txtaddnew.Visible = true;
            btnUpdate.Visible = false;
            grdRemarks.DataSource = dsRemarks;
            grdRemarks.DataBind();
        }
    }

    protected void grdRemarks_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "this.className='highlight'");
            e.Row.Attributes.Add("onmouseout", "this.className='normal'");
        }
    }

    protected void grdRemarks_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdRemarks.PageIndex = e.NewPageIndex;
        sCmd = Session["GetcmdArgChar"].ToString();

        if (sCmd == "All")
        {
            FillRemarks();
        }
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {

        System.Threading.Thread.Sleep(time);
        string Remarks_Id = string.Empty;
        string Remarks_Content = string.Empty;
        string Name = string.Empty;

        AdminSetup adm = new AdminSetup();

        foreach (GridViewRow gridRow in grdRemarks.Rows)
        {
            int iReturn = -1;
            AdminSetup ad = new AdminSetup();
            TextBox txtname = (TextBox)grdRemarks.FooterRow.FindControl("txt_Name");
            DropDownList ddlCountries1 = (DropDownList)grdRemarks.FooterRow.FindControl("ddlCountries1");
            Name = txtname.Text.ToString();

            if (Name == "")
            {

            }
            else
            {
                //iReturn = adm.RemarksAdd(Name, div_code);
                iReturn = adm.RemarksAdd(Name, div_code, ddlCountries1.SelectedValue.ToString());


                if (iReturn > 0)
                {
                    //ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Created Successfully');</script>");
                    FillRemarks();
                }
            }

            Label lblremarks_id = (Label)gridRow.Cells[1].FindControl("lblremarks_id");
            Remarks_Id = lblremarks_id.Text.ToString();
            TextBox txtremarks_name = (TextBox)gridRow.Cells[1].FindControl("txtremarks_name");
            Remarks_Content = txtremarks_name.Text.ToString();

            DropDownList ddlCountries = (DropDownList)gridRow.Cells[1].FindControl("ddlCountries");

            //Update

            iReturn = adm.RemarksUpdate(Convert.ToInt16(Remarks_Id), Remarks_Content, div_code, ddlCountries.SelectedValue.ToString());


            if (iReturn > 0)
            {
                //ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Updated Successfully');</script>");
                FillRemarks();
            }
        }
    }

    protected void grdRemarks_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        AdminSetup adm = new AdminSetup();

        TextBox txtname = (TextBox)grdRemarks.FooterRow.FindControl("txt_Name");
        DropDownList ddlCountries = (DropDownList)grdRemarks.FooterRow.FindControl("ddlCountries1");
        if (txtname.Text == "")
        {
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Enter Name');</script>");
            txtname.Focus();
        }
        else
        {
            int iReturn = adm.RemarksAdd(txtname.Text, div_code, ddlCountries.SelectedValue.ToString());

            if (iReturn > 0)
            {
                //ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Created Successfully');</script>");
                FillRemarks();
            }
        }
    }

    protected void grdRemarks_RowCommand(object sender, GridViewCommandEventArgs e)
    {


        if (e.CommandName == "Deactivate")
        {
            Remarks_Id = Convert.ToString(e.CommandArgument);

            //Deactivate
            AdminSetup adm = new AdminSetup();
            int iReturn = adm.RemarksDelete(Remarks_Id);
            if (iReturn > 0)
            {
               
                ClientScript.RegisterStartupScript(GetType(), "TestAlert", "alert(\'Deleted Successfully.\');", true);
            }
            else
            {
                // menu1.Status ="Unable to Deactivate";
                ClientScript.RegisterStartupScript(GetType(), "TestAlert", "alert(\'Unable to Deactivate.\');", true);
            }
            FillRemarks();
        }
    }

    protected void btnaddnew_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        string Remarks_Id = string.Empty;
        string Remarks_Content = string.Empty;
        string Types = string.Empty;

        Remarks_Content = txtaddnew.Text;
        Remarks_Content = txtaddnew.Text;
        Types = ddlCountries2.SelectedValue.ToString();

        if (Remarks_Id == "")
        {
            AdminSetup adm = new AdminSetup();


            int iReturn = adm.RemarksAdd(Remarks_Content, div_code, Types);

            if (iReturn > 0)
            {
                FillRemarks();
            }
        }
    }
    protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DropDownList ddlCountries = (e.Row.FindControl("ddlCountries") as DropDownList);
            string country = (e.Row.FindControl("lblCountry") as Label).Text;
            ddlCountries.Items.FindByValue(country).Selected = true;
        }
    }
}