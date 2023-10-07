using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using Bus_EReport;

public partial class MasterFiles_Options_HomePageRest : System.Web.UI.Page
{
    DataSet dsadmin = null;
    DataSet dsadm = null;
 
    int iDCR = 0;
    int iTP = 0;
    int iLeave = 0;
    int iExpense = 0;
    int iAddDoc = 0;
    int iDeactDoc = 0;
    int iAddDeact = 0;
    int iSSEntry = 0;
    int iDocSer = 0;
    string div_code = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
          div_code = Session["div_code"].ToString();
          if (!Page.IsPostBack)
          {
              menu1.Title = this.Page.Title;
              menu1.FindControl("btnBack").Visible = false;
              AdminSetup dv = new AdminSetup();
              dsadmin = dv.getHomePage_Restrict(div_code);
              if (dsadmin.Tables[0].Rows.Count > 0)
              {
                  string strAdd = dsadmin.Tables[0].Rows[0]["DCR_Home"].ToString();
                  if (strAdd == "1")
                  {
                      chkNew.Items[0].Selected = true;
                  }
                  string strTp = dsadmin.Tables[0].Rows[0]["TP_Home"].ToString();
                  if (strTp == "1")
                  {
                      chkNew.Items[1].Selected = true;
                  }
                  string strLeave = dsadmin.Tables[0].Rows[0]["Leave_Home"].ToString();
                  if (strLeave == "1")
                  {
                      chkNew.Items[2].Selected = true;
                  }
                  string strExpen = dsadmin.Tables[0].Rows[0]["Expense_Home"].ToString();
                  if (strExpen == "1")
                  {
                      chkNew.Items[3].Selected = true;
                  }
                  string strdocadd = dsadmin.Tables[0].Rows[0]["Listeddr_Add_Home"].ToString();
                  if (strdocadd == "1")
                  {
                      chkNew.Items[4].Selected = true;
                  }
                  string strdocdeac = dsadmin.Tables[0].Rows[0]["Listeddr_Deact_Home"].ToString();
                  if (strdocdeac == "1")
                  {
                      chkNew.Items[5].Selected = true;
                  }
              }
          }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
         Division divi = new Division();
      
             if (chkNew.Items[0].Selected)
             {
                 iDCR = 1;
             }
             else
             {
                 iDCR = 0;
             }
             if (chkNew.Items[1].Selected)
             {
                 iTP = 1;
             }
             else
             {
                 iTP = 0;
             }
             if (chkNew.Items[2].Selected)
             {
                 iLeave = 1;
             }
             if (chkNew.Items[3].Selected)
             {
                 iExpense = 1;

             }
             if (chkNew.Items[4].Selected)
             {
                 iAddDoc = 1;
             }
             if (chkNew.Items[5].Selected)
             {
                 iDeactDoc = 1;
             }
             if (chkNew.Items[6].Selected)
             {
                 iAddDeact = 1;
             }

             if (chkNew.Items[7].Selected)
             {
                 iSSEntry = 1;

             }

             if (chkNew.Items[8].Selected)
             {
                 iDocSer = 1;

             }
             AdminSetup admin = new AdminSetup();

             int iReturn = admin.Home_Restrict(iDCR, iTP, iLeave, iExpense, iAddDoc, iDeactDoc, iAddDeact, iSSEntry, iDocSer, div_code);
             if (iReturn > 0)
             {
                 ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Setup has been updated Successfully');</script>");
             }

         
    }
}