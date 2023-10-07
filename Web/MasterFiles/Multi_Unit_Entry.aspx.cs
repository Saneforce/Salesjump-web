using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;

public partial class MasterFiles_Multi_Unit_Entry : System.Web.UI.Page
{
    #region Declaration
    string div_code = string.Empty;
    string sf_type = string.Empty;
    string sCmd = string.Empty;
    string Move_MailFolder_Id = string.Empty;
    int Mail_Id = 0;
    DataSet dsdiv = new DataSet();
    DataSet dsMail = null;
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
                FillMail();
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
    protected void grdmail_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        if (e.CommandName == "Deactivate")
        {
            //Label lblfolder_id = (Label)grdmail.Rows[e.RowIndex].Cells[1].FindControl("lblfolder_id");

            Mail_Id = Convert.ToInt16(e.CommandArgument);

            //Deactivate
            AdminSetup adm = new AdminSetup();
            int iReturn = adm.DeActtype_Id(Mail_Id);

            if (iReturn > 0)
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Deleted Successfully');</script>");
            }

            else if (iReturn == -2)
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Unable to Delete');</script>");
            }
            FillMail();
        }
    }

    private void FillMail()
    {
        AdminSetup adm = new AdminSetup();
        dsMail = adm.getTypeName(div_code);
        if (dsMail.Tables[0].Rows.Count > 0)
        {
            //btnTransfer_mail.Visible = true;
            btnaddnew.Visible = false;
            txtaddnew.Visible = false;
            btnUpdate.Visible = true;
            grdmail.DataSource = dsMail;
            grdmail.DataBind();


            foreach (GridViewRow row in grdmail.Rows)
            {
                LinkButton lnkbutDelete = (LinkButton)row.FindControl("lnkbutDelete");
                Label lblimg = (Label)row.FindControl("lblimg");
                Label lblmailCount = (Label)row.FindControl("lblmailCount");
                //if (Convert.ToInt32(dsDocCat.Tables[0].Rows[row.RowIndex][4].ToString()) > 0)
                if (lblmailCount.Text != "0")
                {
                    // grdProCat.Rows[row.RowIndex].Cells[7].Enabled = false;
                    lnkbutDelete.Visible = false;
                    lblimg.Visible = true;
                }
            }

        }
        else
        {
            //btnTransfer_mail.Visible = false;
            btnaddnew.Visible = true;
            txtaddnew.Text = string.Empty;
            txtaddnew.Visible = true;  
            btnUpdate.Visible = false;
            grdmail.DataSource = dsMail;
            grdmail.DataBind();
        }

    }

    protected void grdmail_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "this.className='highlight'");
            e.Row.Attributes.Add("onmouseout", "this.className='normal'");
        }
    }

    protected void grdmail_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdmail.PageIndex = e.NewPageIndex;
        sCmd = Session["GetcmdArgChar"].ToString();
        if (sCmd == "All")
        {
            FillMail();
        }
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        string MailFolder_Id = string.Empty;
        string MailFolder_Name = string.Empty;
        string Name = string.Empty;

        AdminSetup adm = new AdminSetup();

        foreach (GridViewRow gridRow in grdmail.Rows)
        {
            int iReturn = -1;

            AdminSetup ad = new AdminSetup();
            TextBox txtname = (TextBox)grdmail.FooterRow.FindControl("txt_Name");
            Name = txtname.Text.ToString();


            if (Name == "")
            {
                //ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Enter Folder Name');</script>");
                //txtname.Focus();
            }
            else
            {
                iReturn = adm.RecordAdded(Name, div_code);


                if (iReturn > 0)
                {
                    //ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Created Successfully');</script>");
                    FillMail();
                }
            }


          
            Label lblfolder_id = (Label)gridRow.Cells[1].FindControl("lblfolder_id");
            MailFolder_Id = lblfolder_id.Text.ToString();
            TextBox txtfolder_name = (TextBox)gridRow.Cells[1].FindControl("txtfolder_name");
            MailFolder_Name = txtfolder_name.Text.ToString();

            //Update 
            iReturn = adm.TypeUpdate(Convert.ToInt16(MailFolder_Id), MailFolder_Name, div_code);

            if (iReturn > 0)
            {
                //ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Updated Successfully');</script>");
                FillMail();
            }
        }


    
    }


    protected void grdmail_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        AdminSetup adm = new AdminSetup();
        TextBox txtname = (TextBox)grdmail.FooterRow.FindControl("txt_Name");

        if (txtname.Text == "")
        {
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Enter Type Name');</script>");
            txtname.Focus();
        }
        else
        {
            int iReturn = adm.RecordAdded(txtname.Text, div_code);


            if (iReturn > 0)
            {
                //ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Created Successfully');</script>");
                FillMail();
            }
        }

       
    }

    
   

    protected void btnaddnew_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        string MailFolder_Id = string.Empty;
        string MailFolder_Name = string.Empty;
        MailFolder_Name = txtaddnew.Text;

        if (MailFolder_Id == "")
        {
            AdminSetup adm = new AdminSetup();


            int iReturn = adm.TypeAdd(MailFolder_Name, div_code);

            if (iReturn > 0)
            {
                FillMail();
            }
        }

    }
}