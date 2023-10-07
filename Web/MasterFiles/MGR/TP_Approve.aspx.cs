using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;

public partial class MasterFiles_MGR_TP_Approve : System.Web.UI.Page
{
    #region "Declaration"
    DataSet dsTP = null;
    string sfCode = string.Empty;
    string strTPView = string.Empty;
    string div_Code = string.Empty;
    string sf_code = string.Empty;
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        sfCode = Session["sf_code"].ToString();
        div_Code = Session["div_code"].ToString();

        if (!Page.IsPostBack)
        {
            FillTourPlan();
            //menu1.Title = this.Page.Title;
            //menu1.FindControl("btnBack").Visible = false;
            //Session["backurl"] = "~/MasterFiles/MGR/TP_Approve.aspx";
        }
    }
    
    private void FillTourPlan()
    {
        TourPlan tp = new TourPlan();
        dsTP = tp.get_TP_Pending_Approval(sfCode, div_Code  );
        if (dsTP.Tables[0].Rows.Count > 0)
        {
            //if (sfCode != "admin")
            //{
              
                grdTP.Visible = true;
                grdTP.DataSource = dsTP;
                grdTP.DataBind();               
            //}
            
        }
        else
        {
            grdTP.DataSource = dsTP;
            grdTP.DataBind();    


        }
    }

    private void GetYear()
    {
        TourPlan tp = new TourPlan();
        dsTP = tp.get_TP_Submission_Date(sf_code);
        if (dsTP.Tables[0].Rows.Count > 0)
        {

        }
    }
    

    protected void grdTP_RowDataBound(object sender, GridViewRowEventArgs e)
    {
                  
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblMonth = (Label)e.Row.FindControl("lblMonth");
            lblMonth.Text = getMonthName(lblMonth.Text);
           // e.Row.Cells[5].Text = "Click here to Approve " + lblMonth.Text + " "+ dsTP.Tables[0].Rows[0]["Tour_Year"].ToString();
        }
       
    }

    private string getMonthName(string sMonth)
    {
        string sReturn = string.Empty;

        if (sMonth == "1")
        {
            sReturn = "January";
        }
        else if (sMonth == "2")
        {
            sReturn = "February";
        }
        else if (sMonth == "2")
        {
            sReturn = "February";
        }
        else if (sMonth == "3")
        {
            sReturn = "March";
        }
        else if (sMonth == "4")
        {
            sReturn = "April";
        }
        else if (sMonth == "5")
        {
            sReturn = "May";
        }
        else if (sMonth == "6")
        {
            sReturn = "June";
        }
        else if (sMonth == "7")
        {
            sReturn = "July";
        }
        else if (sMonth == "8")
        {
            sReturn = "August";
        }
        else if (sMonth == "9")
        {
            sReturn = "September";
        }
        else if (sMonth == "10")
        {
            sReturn = "October";
        }
        else if (sMonth == "11")
        {
            sReturn = "November";
        }
        else if (sMonth == "12")
        {
            sReturn = "December";
        }

        return sReturn;
    }

}