using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
public partial class MasterFiles_Options_Quote : System.Web.UI.Page
{
    string div_code = string.Empty;
 
    DataSet dsAdmin = null;
    int iRet = -1;
    string Sl_No = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        
        if (!Page.IsPostBack)
        {
            //menu1.Title = this.Page.Title;
            //menu1.FindControl("btnBack").Visible = false;  
            
                AdminSetup adm = new AdminSetup();
                dsAdmin = adm.Get_Quote(div_code);
                txtQuote.Focus();
                if (dsAdmin.Tables[0].Rows.Count > 0)
                {
                    txtQuote.Text = dsAdmin.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                    if (dsAdmin.Tables[0].Rows[0].ItemArray.GetValue(5).ToString() == "1")
                    {
                        chkback.Checked = true;
                    }
                    else
                    {
                        chkback.Checked = false;
                    }


                }
               
                if ((txtQuote.Text != "") && (txtQuote.Text != null))
                {
                    btnSubmit.Text = "Update";
                   // chkback.Checked = true;

                }
        }

    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (txtQuote.Text.Trim().Length > 0)
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
            AdminSetup adm = new AdminSetup();
            iRet = adm.QuoteAdd(txtQuote.Text.Trim(), div_code, strback);
            if (iRet > 0)
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Quote has been Updated Sucessfully');</script>");
            }
            btnSubmit.Text = "Update";
            
       
        }
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {

        if (txtQuote.Text.Trim().Length > 0)
        {
            AdminSetup adm = new AdminSetup();
            iRet = adm.Delete_Quote(txtQuote.Text.Trim(), div_code);
            if (iRet > 0)
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Quote Deleted Sucessfully');</script>");
                txtQuote.Text = "";
            }
            btnSubmit.Text = "Submit";
            chkback.Checked = false;
            txtQuote.Focus();
           }
    }
}