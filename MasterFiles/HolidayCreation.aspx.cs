using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using Bus_EReport;
public partial class MasterFiles_HolidayCreation : System.Web.UI.Page
{
    DateTime ServerStartTime;
    DateTime ServerEndTime;

    int time;
    string div_code = string.Empty;
    string HolidayId = string.Empty;
    string sf_type = string.Empty;
    DataSet dsHoliday = null;
    DataSet dsdiv = null;
    DataSet dsDivision = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        menu1.Title = this.Page.Title;
        sf_type = Session["sf_type"].ToString();
        if (sf_type == "3")
        {
            div_code = Session["division_code"].ToString();
        }
        else
        {
            div_code = Session["div_code"].ToString();
        }        
        HolidayId = Request.QueryString["Holiday_Id"];
        txtHolidayName.Focus();
        if (!Page.IsPostBack)
        {
            Session["backurl"] = "Holiday_List.aspx";
         
            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);
            Filldiv();
            if (HolidayId != "" && HolidayId != null)
            {
                Holiday holi = new Holiday();
                dsHoliday = holi.getHoli_Ed(HolidayId);

                if (dsHoliday.Tables[0].Rows.Count > 0)
                {
                    txtHolidayName.Text = dsHoliday.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                    ddlMulti.SelectedValue = dsHoliday.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();
                    ddlMonth.SelectedValue = dsHoliday.Tables[0].Rows[0].ItemArray.GetValue(3).ToString();
                    txtFix_Date.Text = dsHoliday.Tables[0].Rows[0].ItemArray.GetValue(4).ToString();
                    ddlDivision.SelectedValue = dsHoliday.Tables[0].Rows[0].ItemArray.GetValue(5).ToString();
                    // txtStateName.Text = dsState.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                }

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
    private void Filldiv()
    {
        Division dv = new Division();
        if (sf_type == "3")
        {
            string[] strDivSplit = div_code.Split(',');
            foreach (string strdiv in strDivSplit)
            {
                if (strdiv != "")
                {
                    dsdiv = dv.getDivisionHO(strdiv);
                    ListItem liTerr = new ListItem();
                    liTerr.Value = dsdiv.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                    liTerr.Text = dsdiv.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                    ddlDivision.Items.Add(liTerr);
                }
            }
        }
        else
        {
            dsDivision = dv.getDivision_Name();
            if (dsDivision.Tables[0].Rows.Count > 0)
            {
                ddlDivision.DataTextField = "Division_Name";
                ddlDivision.DataValueField = "Division_Code";
                ddlDivision.DataSource = dsDivision;
                ddlDivision.DataBind();
            }
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (HolidayId == null)
        {
            int Multi_Date = Convert.ToInt32(ddlMulti.SelectedValue);
            Holiday holi = new Holiday();
            int iReturn = holi.Add_Holiday(txtHolidayName.Text, Multi_Date, txtFix_Date.Text, ddlMonth.SelectedValue, ddlDivision.SelectedValue.ToString());

            if (iReturn > 0)
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Created Successfully');</script>");
                ResetAll();
            }
            else if (iReturn == -2)
            {              
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Name Already Exist');</script>");
            }
        }
        else
        {
            Holiday holi = new Holiday();
            int Holiday = Convert.ToInt16(HolidayId);
            int Multi_Date = Convert.ToInt32(ddlMulti.SelectedValue);
            int iReturn = holi.Update_Holiday(Holiday, txtHolidayName.Text, Multi_Date, txtFix_Date.Text, ddlMonth.SelectedValue, ddlDivision.SelectedValue.ToString());
            if (iReturn > 0)
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Updated Successfully');window.location='Holiday_List.aspx';</script>");
            }
            else if (iReturn == -2)
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Holiday Name Already Exist');</script>");
            }
        }

    }
    private void ResetAll()
    {
        txtFix_Date.Text = "";
        txtHolidayName.Text = "";
        ddlMulti.SelectedIndex = 0;
        ddlMonth.SelectedIndex = 0;
    }
}