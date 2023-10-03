using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;


public partial class MasterFiles_Options_FlashNews : System.Web.UI.Page
{
    string div_code = string.Empty;
    DataSet dsAdmin = null;
    int iRet = -1;

    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        if (!Page.IsPostBack)
        {
            //menu1.Title = this.Page.Title;
            //menu1.FindControl("btnBack").Visible = false;
            AdminSetup adm = new AdminSetup();
            dsAdmin = adm.Get_Flash_News(div_code);
            txtFlash.Focus();
            if (dsAdmin.Tables[0].Rows.Count > 0)
            {
                txtFlash.Text = txtFlash.Text.Replace("asdf", "'");
                txtFlash.Text = dsAdmin.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                if (dsAdmin.Tables[0].Rows[0].ItemArray.GetValue(5).ToString() == "1")
                {
                    chkback.Checked = true;
                }
                else
                {
                    chkback.Checked = false;
                }
            }
            if ((txtFlash.Text != "") && (txtFlash.Text != null))
            {
                btnSubmit.Text = "Update";
                txtFlash.Text = txtFlash.Text.Replace("asdf", "'");
            }
        }

    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (txtFlash.Text.Trim().Length > 0)
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
            txtFlash.Text = txtFlash.Text.Replace("'", "asdf");
            iRet = astp.FlashRecordAdd(txtFlash.Text.Trim(), div_code, strback);
            if (iRet > 0)
            {
                //menu1.Status = "Flash News has been created successfully";
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Flash News has been Updated successfully');</script>");
            }
            btnSubmit.Text = "Update";
            txtFlash.Text = txtFlash.Text.Replace("asdf", "'");

        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        txtFlash.Text = "";
       // menu1.Status = "";
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        if (txtFlash.Text.Trim().Length > 0)
        {
            AdminSetup adm = new AdminSetup();
            iRet = adm.Delete_Flash(txtFlash.Text.Trim(),div_code);
            if (iRet > 0)
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Flash News Deleted Sucessfully');</script>");
                txtFlash.Text = "";
            }
            btnSubmit.Text = "Submit";
            chkback.Checked = false;
            txtFlash.Focus();
        }
    }
}