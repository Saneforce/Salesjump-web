using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;


public partial class MasterFiles_Options_TPEdit : System.Web.UI.Page
{
    string div_code = string.Empty;
    DataSet dsSalesForce = null;
    DataSet dsTP = null;
    string sfCode = string.Empty;
    string StrSelectiveDate;
    string strDate;
    string sDate = string.Empty;
  

    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        sfCode = Session["sf_code"].ToString();
        if (!Page.IsPostBack)
        {
            //FillSalesForce();
            FillManagers();
            FillColor();
            
            //menu1.Title = this.Page.Title;
            //menu1.FindControl("btnBack").Visible = false;

            lblDate.Visible = false;
            txtDate.Visible = false;
                 
            TourPlan tp = new TourPlan();
            dsTP = tp.Get_TP_Edit_Year(div_code);
            if (dsTP.Tables[0].Rows.Count > 0)
            {
                for (int k = Convert.ToInt16(dsTP.Tables[0].Rows[0]["Year"]); k <= DateTime.Now.Year + 1; k++)
                {
                    ddlYear.Items.Add(k.ToString());
                    ddlYear.SelectedValue = DateTime.Now.Year.ToString();
                }
                ddlMonth.SelectedValue = DateTime.Now.Month.ToString();               
            }            
        
        }
    }

    private void FillColor()
    {
        int j = 0;

        foreach (ListItem ColorItems in ddlSF.Items)
        {
            string bcolor = "#" + ColorItems.Text;
            ddlFieldForce.Items[j].Attributes.Add("style", "background-color:" + bcolor);
            //ddlFieldForce.Items[j].Selected = true;

            //if (ColorItems.Text == "Level1")
            //    //ColorItems.Attributes.Add("style", "background-color: Wheat");
            //    ddlFieldForce.Items[j].Attributes.Add("style", "background-color: Wheat");

            //if (ColorItems.Text == "Level2")
            //    //ColorItems.Attributes.Add("style", "background-color: Blue");
            //    ddlFieldForce.Items[j].Attributes.Add("style", "background-color: LightGreen");

            //if (ColorItems.Text == "Level3")
            //    //ColorItems.Attributes.Add("style", "background-color: Cyan");
            //    ddlFieldForce.Items[j].Attributes.Add("style", "background-color: Pink");

            //if (ColorItems.Text == "Level4")
            //    //ColorItems.Attributes.Add("style", "background-color: Lavendar");
            //    ddlFieldForce.Items[j].Attributes.Add("style", "background-color: Lavendar");

            j = j + 1;
        }
    }

    protected void ddlAlpha_SelectedIndexChanged(object sender, EventArgs e)
    {
        SalesForce sf = new SalesForce();
        dsSalesForce = sf.UserListTP_Alphasearch(div_code, "admin", ddlAlpha.SelectedValue);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            ddlFieldForce.DataTextField = "sf_name";
            ddlFieldForce.DataValueField = "sf_code";
            ddlFieldForce.DataSource = dsSalesForce;
            ddlFieldForce.DataBind();

            ddlSF.DataTextField = "des_Color";
            ddlSF.DataValueField = "sf_code";
            ddlSF.DataSource = dsSalesForce;
            ddlSF.DataBind();

        }

         

    }

    private void FillManagers()
    {
        SalesForce sf = new SalesForce();

        if (ddlFFType.SelectedValue.ToString() == "1")
        {
            ddlAlpha.Visible = false;
            dsSalesForce = sf.getSalesForcelist_New(div_code);
        }
        else if (ddlFFType.SelectedValue.ToString() == "0")
        {
            FillSF_Alpha();
            ddlAlpha.Visible = true;
            dsSalesForce = sf.UserListTP_Alpha(div_code, "admin");
        }
        else if (ddlFFType.SelectedValue.ToString() == "2")
        {
            dsSalesForce = sf.UserList_HQ(div_code, "admin");
        }

        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            ddlFieldForce.DataTextField = "sf_name";
            ddlFieldForce.DataValueField = "sf_code";
            ddlFieldForce.DataSource = dsSalesForce;
            ddlFieldForce.DataBind();

            ddlSF.DataTextField = "Desig_Color";
            ddlSF.DataValueField = "sf_code";
            ddlSF.DataSource = dsSalesForce;
            ddlSF.DataBind();

        }
    }

    private void FillSF_Alpha()
    {
        SalesForce sf = new SalesForce();
        dsSalesForce = sf.getSalesForcelist_Alphabet(div_code);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            ddlAlpha.DataTextField = "sf_name";
            ddlAlpha.DataValueField = "val";
            ddlAlpha.DataSource = dsSalesForce;
            ddlAlpha.DataBind();
            ddlAlpha.SelectedIndex = 0;
        }
    }

    protected void ddlFFType_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillManagers();
        FillColor();
    }

   

    private void FillSalesForce()
    {
        SalesForce sf = new SalesForce();
        dsSalesForce = sf.getSalesForcelist(div_code);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            ddlFieldForce.DataTextField = "sf_name";
            ddlFieldForce.DataValueField = "sf_code";
            ddlFieldForce.DataSource = dsSalesForce;
            ddlFieldForce.DataBind();
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        int iReturn = -1;

        TourPlan tp = new TourPlan();
        iReturn = tp.TP_Edit_RecordAdd(ddlFieldForce.SelectedValue.ToString(), ddlMonth.SelectedValue.ToString(), ddlYear.SelectedValue.ToString(), txtDate.Text);
        if (iReturn > 0)
        {
           // Response.Write("TP has been updated successfully");
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('TP has been updated successfully');window.location='TPEdit.aspx'</script>");
            txtDate.Text = "";
        }
    }

    protected void btnGo_Click(object sender, EventArgs e)
    {

        if (ddlDay.SelectedValue.ToString() == "0")
        {
            txtDate.Text = "All";
        }
        else
        {
            TourPlan tp = new TourPlan();
            DataSet dsTP = new DataSet();
            dsTP = tp.get_TP_Count_FieldForce(ddlFieldForce.SelectedValue.ToString(), ddlMonth.SelectedValue.ToString(), ddlYear.SelectedValue.ToString(), ddlDay.SelectedValue.ToString());
            if (dsTP.Tables[0].Rows[0][0].ToString() != "0")
            {
                
                if (ddlDay.SelectedValue.ToString() != "All")
                {
                    sDate = ddlDay.SelectedValue.ToString() + "-" + ddlMonth.SelectedValue.ToString() + "-" + ddlYear.SelectedValue.ToString();
                    txtDate.Text = txtDate.Text + sDate + ",";
                    lblDate.Visible = true;
                    txtDate.Visible = true;
                    btnSubmit.Visible = true;
                }
                else
                {
                    txtDate.Text = ddlDay.SelectedValue.ToString();
                    lblDate.Visible = true;
                    txtDate.Visible = true;
                    btnSubmit.Visible = true;
                }
                

            }
            else
            {
                lblDate.Visible = false;
                txtDate.Visible = false;
                btnSubmit.Visible = false;
                // Response.Write("TP Not entered for the selected month");
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('TP Not entered for the selected month');</script>");
            }
        }
        FillColor();
    }

    protected void ddlMonth_SelectedIndex(object sender, EventArgs e)
    {
        if (ddlMonth.SelectedValue != "0")
        {
            string strDate = getMonthDate(Convert.ToInt16(ddlMonth.SelectedValue));
            ddlDay.Items.Clear();
            ddlDay.Items.Add("---Select---");
            ddlDay.Items.Add("All");

            string str = "";

            for (int k = 1; k <= Convert.ToInt16(strDate); k++)
            {
                string ilength = 0 + k.ToString();
                if (ilength.Length > 2)
                {
                    
                    str = ilength.ToString();
                    str=str.Remove(0, 1);
                }
                else
                {
                    str = ilength.ToString();
                }

                ddlDay.Items.Add(str);
            }
        }
        else
        {
            ddlDay.Items.Clear();
            ddlDay.Items.Add("---Select---");
        }

        FillColor();
    }

    private string getMonthDate(int iMonth)
    {
        string sReturn = string.Empty;

        if (iMonth == 01)
        {
            sReturn = "31";
        }
        else if (iMonth == 02)
        {
            sReturn = "28";
        }
        else if (iMonth == 03)
        {
            sReturn = "31";
        }
        else if (iMonth == 04)
        {
            sReturn = "30";
        }
        else if (iMonth == 05)
        {
            sReturn = "31";
        }
        else if (iMonth == 06)
        {
            sReturn = "30";
        }
        else if (iMonth == 07)
        {
            sReturn = "31";
        }
        else if (iMonth == 08)
        {
            sReturn = "31";
        }
        else if (iMonth == 09)
        {
            sReturn = "30";
        }
        else if (iMonth == 10)
        {
            sReturn = "31";
        }
        else if (iMonth == 11)
        {
            sReturn = "30";
        }
        else if (iMonth == 12)
        {
            sReturn = "31";
        }
        return sReturn;
    }

    private string getDays(int iDay)
    {
        string sWeek = string.Empty;

        if (iDay == 0)
        {
            sWeek = "Sunday";
        }
        else if (iDay == 1)
        {
            sWeek = "Monday";
        }
        else if (iDay == 2)
        {
            sWeek = "Tuesday";
        }
        else if (iDay == 3)
        {
            sWeek = "Wednesday";
        }
        else if (iDay == 4)
        {
            sWeek = "Thursday";
        }
        else if (iDay == 5)
        {
            sWeek = "Friday";
        }
        else if (iDay == 6)
        {
            sWeek = "Saturday";
        }

        return sWeek;
    }
}