using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;

public partial class MasterFiles_App_CallFeedback : System.Web.UI.Page
{
    #region Declaration
    string div_code = string.Empty;
    string sf_type = string.Empty;
    string sCmd = string.Empty;
    string Feedback_Id = string.Empty;
    int Id = 0;
    DataSet dsdiv = new DataSet();
    DataSet dsFeedback = null;
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

        if(sf_type == "3")
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
                FillFeedback();
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

    private void FillFeedback()
    {
        AdminSetup adm = new AdminSetup();
        dsFeedback = adm.getFeedback(div_code);
        if (dsFeedback.Tables[0].Rows.Count > 0)
        {
            btnaddnew.Visible = false;
            txtaddnew.Visible = false;
            btnUpdate.Visible = true;
            grdFeedback.DataSource = dsFeedback;
            grdFeedback.DataBind();
        }
        else
        {
            btnaddnew.Visible = true;
            txtaddnew.Text = string.Empty;
            txtaddnew.Visible = true;
            btnUpdate.Visible = false;
            grdFeedback.DataSource = dsFeedback;
            grdFeedback.DataBind();
        }
    }
    protected void grdFeedback_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "this.className='highlight'");
            e.Row.Attributes.Add("onmouseout", "this.className='normal'");
        }
    }
    protected void grdFeedback_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdFeedback.PageIndex = e.NewPageIndex;
        sCmd = Session["GetcmdArgChar"].ToString();

        if (sCmd == "All")
        {
            FillFeedback();
        }
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        string Feed_Id = string.Empty;
        string Feed_Content = string.Empty;
        string Name = string.Empty;

        AdminSetup adm = new AdminSetup();

        foreach (GridViewRow gridRow in grdFeedback.Rows)
        {
            int iReturn = -1;
            AdminSetup ad = new AdminSetup();
            TextBox txtname = (TextBox)grdFeedback.FooterRow.FindControl("txt_Name");
            Name = txtname.Text.ToString();

            if (Name == "")
            {

            }
            else
            {
                iReturn = adm.FeedbackAdd(Name, div_code);


                if (iReturn > 0)
                {
                    //ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Created Successfully');</script>");
                    FillFeedback();
                }
            }

            Label lblfeedback_id = (Label)gridRow.Cells[1].FindControl("lblfeedback_id");
            Feed_Id = lblfeedback_id.Text.ToString();
            TextBox txtfeedback_name = (TextBox)gridRow.Cells[1].FindControl("txtfeedback_name");
            Feed_Content = txtfeedback_name.Text.ToString();

            //Update

            iReturn = adm.FeedbackUpdate(Convert.ToInt16(Feed_Id), Feed_Content, div_code);

            if (iReturn > 0)
            {
                //ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Updated Successfully');</script>");
                FillFeedback();
            }
        }
    }

    protected void grdFeedback_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        AdminSetup adm = new AdminSetup();

        TextBox txtname = (TextBox)grdFeedback.FooterRow.FindControl("txt_Name");

        if (txtname.Text == "")
        {
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Enter Name');</script>");
            txtname.Focus();
        }
        else
        {
            int iReturn = adm.FeedbackAdd(txtname.Text, div_code);

            if (iReturn > 0)
            {
                //ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Created Successfully');</script>");
                FillFeedback();
            }
        }
    }

    protected void grdFeedback_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        if (e.CommandName == "Deactivate")
        {
            Feedback_Id = Convert.ToString(e.CommandArgument);

            //Deactivate
            AdminSetup adm = new AdminSetup();
            int iReturn = adm.FeedbackDelete(Feedback_Id);
            if (iReturn > 0)
            {
                
                ClientScript.RegisterStartupScript(GetType(), "TestAlert", "alert(\'Deleted Successfully.\');", true);
            }
            else
            {
                // menu1.Status ="Unable to Deactivate";
                ClientScript.RegisterStartupScript(GetType(), "TestAlert", "alert(\'Unable to Deactivate.\');", true);
            }
            FillFeedback();
            
        }
    }
    protected void btnaddnew_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        string Feed_Id = string.Empty;
        string Feed_Content = string.Empty;
        Feed_Content = txtaddnew.Text;

        if (Feed_Id == "")
        {
            AdminSetup adm = new AdminSetup();


            int iReturn = adm.FeedbackAdd(Feed_Content, div_code);

            if (iReturn > 0)
            {
                FillFeedback();
            }
        }

    }
}