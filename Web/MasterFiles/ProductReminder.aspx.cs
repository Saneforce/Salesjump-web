using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;

public partial class MasterFiles_ProductReminder : System.Web.UI.Page
{
    DataSet dsDivision = null;
    DataSet dsgift = null;
    DataSet dsState = null;
    DataSet dsSubDivision = null;
    string subdivision_code = string.Empty;
    string giftcode = string.Empty;
    string state_cd = string.Empty;
    string sState = string.Empty;
    string div_code = string.Empty;
    string state_code = string.Empty;
    string State_Code = string.Empty; 
    string sChkLocation = string.Empty;
    string sChkLocation1 = string.Empty;
    int iIndex; 
    string[] statecd;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int time;
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["backurl"] = "ProductReminderList.aspx";
        div_code = Session["div_code"].ToString();
        giftcode = Request.QueryString["Gift_Code"];
        txtGift_SName.Focus();
        if (!Page.IsPostBack)
        {
            FillCheckBoxList();
            FillCheckBoxList_New();
            menu1.Title = this.Page.Title;
            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);
            if (giftcode != "" && giftcode != null)
            {
                Product dv = new Product();
                dsgift = dv.getGift(div_code, giftcode);
                if (dsgift.Tables[0].Rows.Count > 0)
                {
                    txtGift_SName.Text = dsgift.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                    txtGiftName.Text = dsgift.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                    txtGiftValue.Text = dsgift.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();
                    ddlGiftType.Text = dsgift.Tables[0].Rows[0].ItemArray.GetValue(3).ToString();
                    txtEffFrom.Text = dsgift.Tables[0].Rows[0].ItemArray.GetValue(4).ToString();
                    txtEffTo.Text = dsgift.Tables[0].Rows[0].ItemArray.GetValue(5).ToString();
                    state_code = dsgift.Tables[0].Rows[0].ItemArray.GetValue(6).ToString();
                    subdivision_code = dsgift.Tables[0].Rows[0].ItemArray.GetValue(7).ToString();
                }
                FillCheckBoxList();
                FillCheckBoxList_New();
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
    private void FillCheckBoxList()
    {
        //List of States are loaded into the checkbox list from Division Class
        Division dv = new Division();
        dsDivision = dv.getStatePerDivision(div_code);
        if (dsDivision.Tables[0].Rows.Count > 0)
        {
            int i = 0;
            state_cd = string.Empty;
            sState = dsDivision.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
            statecd = sState.Split(',');
            foreach (string st_cd in statecd)
            {
                if (i == 0)
                {
                    state_cd = state_cd + st_cd;
                }
                else
                {
                    if (st_cd.Trim().Length > 0)
                    {
                        state_cd = state_cd + "," + st_cd;
                    }
                }
                i++;
            }

            State st = new State();
            dsState = st.getSt(state_cd);
            chkboxLocation.DataTextField = "statename";
            chkboxLocation.DataValueField = "state_code";
            chkboxLocation.DataSource = dsState;
            chkboxLocation.DataBind();
        }
        string[] state;
        if (state_code != "")
        {
            iIndex = -1;
            state = state_code.Split(',');
            foreach (string st in state)
            {
                for (iIndex = 0; iIndex < chkboxLocation.Items.Count; iIndex++)
                {
                    if (st == chkboxLocation.Items[iIndex].Value)
                    {
                        chkboxLocation.Items[iIndex].Selected = true;
                        chkboxLocation.Items[iIndex].Attributes.Add("style", "Color: Red;font-weight:Bold");
                    }
                }
            }
        }

    }
    private void FillCheckBoxList_New()
    {
        //List of Sub division are loaded into the checkbox list from Division Class

        SubDivision dv = new SubDivision();
        dsSubDivision = dv.getSubDiv(div_code);
        chkSubdiv.DataTextField = "subdivision_name";
        chkSubdiv.DataSource = dsSubDivision;
        chkSubdiv.DataBind();
        string[] subdiv;

        if (subdivision_code != "")
        {
            iIndex = -1;
            subdiv = subdivision_code.Split(',');
            foreach (string st in subdiv)
            {
                for (iIndex = 0; iIndex < chkSubdiv.Items.Count; iIndex++)
                {
                    if (st == chkSubdiv.Items[iIndex].Value)
                    {
                        chkSubdiv.Items[iIndex].Selected = true;
                        chkSubdiv.Items[iIndex].Attributes.Add("style", "Color: Red; font-weight:Bold");
                      //  chkNil.Checked = false;

                    }
                }
            }

        }

    }
    protected void Submit_Click(object sender, EventArgs e)
    {
         System.Threading.Thread.Sleep(time);
        for (int i = 0; i < chkboxLocation.Items.Count; i++)
        {
            if (chkboxLocation.Items[i].Selected)
            {
                sChkLocation = sChkLocation + chkboxLocation.Items[i].Value + ",";
            }
        }
        for (int i = 0; i < chkSubdiv.Items.Count; i++)
        {
            if (chkSubdiv.Items[i].Selected)
            {
                sChkLocation1 = sChkLocation1 + chkSubdiv.Items[i].Value + ",";
            }
        }
        if (giftcode == null)
        {
            // Add New Product Reminder           
            Product prd = new Product();
            int iReturn = prd.RecordAddGift(txtGift_SName.Text, txtGiftName.Text, Convert.ToInt32(ddlGiftType.SelectedValue), txtGiftValue.Text, Convert.ToDateTime(txtEffFrom.Text), Convert.ToDateTime(txtEffTo.Text), Convert.ToInt32(Session["div_code"].ToString()), sChkLocation, sChkLocation1);

            if (iReturn > 0)
            {
               // menu1.Status = "Gift details Created Successfully";
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Created Successfully');</script>");
                ResetAll();
            }

            else if (iReturn == -2)
            {                
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Gift Name Already exist');</script>");
                txtGiftName.Focus();
            }
            else if (iReturn == -3)
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Gift Short Name Already exist');</script>");
                txtGift_SName.Focus();
            }
        }
        else
        {
            // Update Product Reminder
            Product dv = new Product();
            int iReturn = dv.RecordUpdateGift(giftcode, txtGift_SName.Text, txtGiftName.Text, Convert.ToInt32(ddlGiftType.SelectedValue), txtGiftValue.Text, Convert.ToDateTime(txtEffFrom.Text), Convert.ToDateTime(txtEffTo.Text), Convert.ToInt32(Session["div_code"].ToString()), sChkLocation, sChkLocation1);
             if (iReturn > 0 )
            {
               // menu1.Status = "Gift details updated Successfully ";
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Updated Successfully');window.location='ProductReminderList.aspx';</script>");
            }
             else if (iReturn == -2)
             {
                 ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Gift Name Already exist');</script>");
                 txtGiftName.Focus();
             }
             else if (iReturn == -3)
             {
                 ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Gift Short Name Already exist');</script>");
                 txtGift_SName.Focus();
             }

        }
    }
    private void ResetAll()
    {
        txtGift_SName.Text = "";
        txtGiftName.Text = "";
        txtGiftValue.Text = "";
        ddlGiftType.SelectedIndex = 0;
        txtEffFrom.Text = "";
        txtEffTo.Text = "";
         for (iIndex = 0; iIndex < chkboxLocation.Items.Count; iIndex++)
        {
            chkboxLocation.Items[iIndex].Selected = false;
        }
      //   chkNil.Checked = true;
         for (iIndex = 0; iIndex < chkSubdiv.Items.Count; iIndex++)
         {
             chkSubdiv.Items[iIndex].Selected = false;
         }
    }
    //protected void chkNil_CheckedChanged(object sender, EventArgs e)
    //{
    //    chkSubdiv.Attributes.Add("onclick", "checkNIL(this);");
    //} 
}