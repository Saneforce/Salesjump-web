using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
public partial class MasterFiles_ProductReminder_View : System.Web.UI.Page
{
    DataSet dsDivision = null;
    DataSet dsState = null;
    DataSet dsProd = null;
    string div_code = string.Empty;
    string state_cd = string.Empty;
    string sState = string.Empty;   
    string[] statecd;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int time;
    protected void Page_Load(object sender, EventArgs e)
    {
        grdGift.Visible = false;
        tblState.Visible = false;
        DataSet dsTP = null;
        Session["backurl"] = "ProductReminderList.aspx";
        div_code = Session["div_code"].ToString();
       
        if (!Page.IsPostBack)
        {
            TourPlan tp = new TourPlan();
            dsTP = tp.Get_TP_Edit_Year(div_code);
            if (dsTP.Tables[0].Rows.Count > 0)
            {
                for (int k = Convert.ToInt16(dsTP.Tables[0].Rows[0]["Year"]); k <= DateTime.Now.Year + 1; k++)
                {
                    ddlFromYear.Items.Add(k.ToString());
                    ddlToYear.Items.Add(k.ToString());
                    ddlFromYear.SelectedValue = DateTime.Now.Year.ToString();
                    ddlToYear.SelectedValue = DateTime.Now.Year.ToString();
                }
            }
            FillState(div_code);
            menu1.Title = this.Page.Title;
            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);
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
    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void grdGift_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DropDownList ddlGiftType = (DropDownList)e.Row.FindControl("ddlGiftType");
            if (ddlGiftType != null)
            {
                DataRowView row = (DataRowView)e.Row.DataItem;
                ddlGiftType.SelectedIndex = ddlGiftType.Items.IndexOf(ddlGiftType.Items.FindByText(row["Gift_Type"].ToString()));
            }
        }
    }
   
    private void FillState(string div_code)
    {
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
            dsState = st.getState(state_cd);
            ddlState.DataTextField = "statename";
            ddlState.DataValueField = "state_code";
            ddlState.DataSource = dsState;
            ddlState.DataBind();
        }
    }
    protected void btnSubmit_Onclick(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        ViewGift();
    }

    private void ViewGift()
    {
        if ((ddlState.SelectedIndex > 0) && (ddlFromYear.SelectedIndex > 0) && (ddlToYear.SelectedIndex > 0))
        {
            Product prd = new Product();
            dsProd = prd.ViewGift(div_code, Convert.ToInt32(ddlState.SelectedValue), Convert.ToInt32(ddlFromYear.SelectedValue), Convert.ToInt32(ddlToYear.SelectedValue));
            if (dsProd.Tables[0].Rows.Count > 0)
            {
                tblState.Visible = true;
                lblStatename.Text = Convert.ToString(ddlState.SelectedItem.ToString());

            }            
        }


        if ((ddlState.SelectedIndex > 0) && (ddlFromYear.SelectedIndex > 0) && (ddlToYear.SelectedIndex > 0))
        {
            Product prd = new Product();
            dsProd = prd.ViewGift(div_code, Convert.ToInt32(ddlState.SelectedValue), Convert.ToInt32(ddlFromYear.SelectedValue), Convert.ToInt32(ddlToYear.SelectedValue));
            if (dsProd.Tables[0].Rows.Count > 0)
            {
                grdGift.Visible = true;
                grdGift.DataSource = dsProd;
                grdGift.DataBind();
            }
            else
            {
                grdGift.Visible = true;
                grdGift.DataSource = dsProd;
                grdGift.DataBind();
            }

        }      
        
    }
}