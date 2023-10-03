using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;

public partial class MasterFiles_Options_NoticeBoard : System.Web.UI.Page
{
    string div_code = string.Empty;
    int iRet = -1;
    DataSet dsAdmin = null;
    string Start_Date = string.Empty;
    string End_Date = string.Empty;
    string NBDate= string.Empty;
     string[] Dt;
    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        CalStartDate.StartDate = DateTime.Now;
        CalEndDate.StartDate = DateTime.Now;
        if (!Page.IsPostBack)
        {
            txtNotice1.Focus();
            //menu1.Title = this.Page.Title;
            //menu1.FindControl("btnBack").Visible = false;
            AdminSetup adm = new AdminSetup();
            dsAdmin = adm.getNBDate(div_code);
        ddlEdit.DataTextField = "NB_Start_End";
        ddlEdit.DataValueField = "NB_Start_End";
        ddlEdit.DataSource = dsAdmin;
        ddlEdit.DataBind();
        }
       // GetValues();
    }

    //protected void btnSubmit_Click(object sender, EventArgs e)
    //{
    //    if (txtNotice1.Text.Trim().Length > 0)
    //    {
    //        string strback = chkback.Text;
    //        if (chkback.Checked == true)
    //        {
    //            strback = "1";
    //        }
    //        else
    //        {
    //            strback = "0";
    //        }
    //        AdminSetup astp = new AdminSetup();
    //        iRet = astp.NB_RecordAdd(txtNotice1.Text.Trim(), txtNotice2.Text.Trim(), txtNotice3.Text.Trim(), txtStartDate.Text.Trim(), txtEndDate.Text.Trim(), div_code, strback);
    //        if (iRet > 0)
    //            //menu1.Status = "Notice Board has been created successfully";
    //            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Notice Board has been created successfully');</script>");

    //    }
    //}
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        DateTime startdate = Convert.ToDateTime(txtStartDate.Text);
        DateTime enddate = Convert.ToDateTime(txtEndDate.Text);
        if (startdate > enddate)
        {
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('To Date must be greater than From Date');</script>");
            txtStartDate.Focus();
        }
        else
        {
            if (txtNotice1.Text.Trim().Length > 0)
            {
                string strback = chkback.Text;
                if (chkback.Checked == true)
                {
                    strback = "1";
                }
                else
                {
                    strback = "0";
                }
                AdminSetup astp = new AdminSetup();
                iRet = astp.NB_RecordAdd(txtNotice1.Text.Trim(), txtNotice2.Text.Trim(), txtNotice3.Text.Trim(), txtStartDate.Text.Trim(), txtEndDate.Text.Trim(), div_code, strback);
                if (iRet > 0)
                    //menu1.Status = "Notice Board has been created successfully";
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Notice Board has been created successfully');</script>");

            }
        }
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        txtNotice1.Text = "";
        txtNotice2.Text = "";
        txtNotice3.Text = "";
        txtStartDate.Text = "";
        txtEndDate.Text = "";
        //menu1.Status = "";
    }

    protected void lnkEdit_Click(object sender, EventArgs e)
    {
        ddlEdit.Visible = true;
        btnGo.Visible = true;
       
    }
    
    //private void GetValues()
    //{
    //    AdminSetup adm = new AdminSetup();
    //    dsAdmin = adm.Get_NB_Record(div_code, Convert.ToDateTime(Start_Date), Convert.ToDateTime(End_Date));
    //    if (dsAdmin.Tables[0].Rows.Count > 0)
    //    {
    //        txtNotice1.Text = dsAdmin.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
    //        txtNotice2.Text = dsAdmin.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();
    //        txtNotice3.Text = dsAdmin.Tables[0].Rows[0].ItemArray.GetValue(3).ToString();
    //    }
    //}

    protected void btnGo_Click(object sender, EventArgs e)
    {
        NBDate = ddlEdit.Text;
        Dt = NBDate.Split(' ');
        if (Dt.Length > 0)
        {
            Start_Date = Dt[0];
            End_Date = Dt[2];
            AdminSetup adm = new AdminSetup();
            dsAdmin = adm.Get_NB_Record(div_code, Start_Date, End_Date);
            if (dsAdmin.Tables[0].Rows.Count > 0)
            {
                txtNotice1.Text = dsAdmin.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                txtNotice2.Text = dsAdmin.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();
                txtNotice3.Text = dsAdmin.Tables[0].Rows[0].ItemArray.GetValue(3).ToString();
            }
        }
       
    }
    protected void ddlEdit_SelectedIndexChanged(object sender, EventArgs e)
    {
           
    }
}