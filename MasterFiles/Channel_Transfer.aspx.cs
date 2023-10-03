using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;

public partial class MasterFiles_Channel_Transfer : System.Web.UI.Page
{
    string div_code = string.Empty;
    DataSet dsTP = null;
    string tot_dr = string.Empty;
    string tot_FWD = string.Empty;
    string tot_dcr_dr = string.Empty;
    string sCurrentDate = string.Empty;
    string sf_code = string.Empty;
    string sf_type = string.Empty;
    string MultiSf_Code = string.Empty;
    DateTime ServerStartTime;
    DataSet dsSalesForce = null;
    DataSet dsListedDR = null;
 
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
        sf_code = Session["sf_code"].ToString();
        sf_type = Session["sf_type"].ToString();

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
            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);


            DDL_CHANNEL1.DataSource = null;
            DDL_CHANNEL1.Items.Clear();
            DDL_CHANNEL1.Items.Insert(0, "---Select---");

            DDL_CHANNEL2.DataSource = null;
            DDL_CHANNEL2.Items.Clear();
            DDL_CHANNEL2.Items.Insert(0, "---Select---");


            FILLCHANNEL();
        }

    }
    private void FILLCHANNEL()
    {
        ListedDR lstDR = new ListedDR();
        dsListedDR = lstDR.GET_CHANNEL(div_code);
        DDL_CHANNEL1.DataTextField = "Doc_Special_Name";
        DDL_CHANNEL1.DataValueField = "Doc_Special_Code";
        DDL_CHANNEL1.DataSource = dsListedDR;
        DDL_CHANNEL1.DataBind();
        DDL_CHANNEL1.Items.Insert(0,"---Select---");
    }


    protected void DDL_CHANNEL1_Change(object sender, EventArgs e)
    {
		if (DDL_CHANNEL1.SelectedIndex > 0)
        {
        ListedDR lstDR = new ListedDR();
        dsListedDR = lstDR.GET_CHANNEL(div_code);        
        DataTable dt = dsListedDR.Tables[0].Select("Doc_Special_Code <>" + DDL_CHANNEL1.SelectedValue.ToString()).CopyToDataTable();             
        DDL_CHANNEL2.DataTextField = "Doc_Special_Name";
        DDL_CHANNEL2.DataValueField = "Doc_Special_Code";
        DDL_CHANNEL2.DataSource = dt;
        DDL_CHANNEL2.DataBind();
        DDL_CHANNEL2.Items.Insert(0, "---Select---");
		}
		else
		{
	
            DDL_CHANNEL2.DataSource = null;
            DDL_CHANNEL2.Items.Clear();
            DDL_CHANNEL2.Items.Insert(0, "---Select---");

		}
    }
    protected void btnchange_Click(object sender, EventArgs e)
    {
        ListedDR lstDR = new ListedDR();
        int iReturn = lstDR.AddLDr_Change(div_code, DDL_CHANNEL1.SelectedValue.ToString(), DDL_CHANNEL2.SelectedValue.ToString());
        if (iReturn > 0)
        {
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Updated Successfully');window.location='Channel_Transfer.aspx';</script>");
        }   
    }
}