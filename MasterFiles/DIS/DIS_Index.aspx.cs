using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
public partial class MasterFiles_DIS_Index : System.Web.UI.Page
{
    #region "Declaration"
    DataSet dsDoc = null;
    DataSet dsAdmin = null;
    DataSet dsDoc1 = null;
    DataSet dsSalesForce = null;
    DataSet dsTP = new DataSet();
    DataSet dsAdm = null;
    DataSet dsDcr = new DataSet();
    DataSet dsAdmNB = null;
    DataSet dsAdminSetup = null;
    DataSet dsadmin = null;
    DataSet dsSecSales = null;
    string sfCode = string.Empty;
    string sf_code = string.Empty;
    string div_code = string.Empty;
    string strdcrtxt = string.Empty;
    string strtptxt = string.Empty;
    string strleavetxt = string.Empty;
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        LblUser.Text = "Welcome " + Session["sf_name"];
        sfCode = Session["sf_code"].ToString();
        div_code = Session["Division_Code"].ToString();
        if (Session["div_name"] != null)
        {
            //LblDiv.Text = Session["div_name"].ToString();
            
        }
        if (!Page.IsPostBack)
        {
            FillDoc();
            FillDcr();
            FillDoc_Deact();
            FillTourPlan();
            FillExp();
            FillLeave();
            FillDoc_AddDeactivate();
            Session["backurl"] = "~/MasterFiles/DIS/MGR_Index.aspx";
            //if ((dsTP.Tables[0].Rows.Count > 0) || (dsDcr.Tables[0].Rows.Count > 0) || (dsAdminSetup.Tables[0].Rows.Count > 0))
            //{
            //    btnHome.Visible = false;
            //}
          
         //   menu1.Title = this.Page.Title;
           // menu1.FindControl("btnBack").Visible = false;
               AdminSetup dv = new AdminSetup();
            dsadmin = dv.getHomePage_Restrict(div_code);
            if (dsadmin.Tables[0].Rows.Count > 0)
            {
                string strdcr = dsadmin.Tables[0].Rows[0]["DCR_Home"].ToString();
                if (strdcr == "1" && (dsDcr.Tables[0].Rows.Count > 0))
                {
                    lblhomepage.Visible = true;
                    btnHome.Visible = false;

                    strdcrtxt = "DCR ";
                }
                string strTp = dsadmin.Tables[0].Rows[0]["TP_Home"].ToString();
                if (strTp == "1" && (dsTP.Tables[0].Rows.Count > 0))
                {
                    lblhomepage.Visible = true;
                    btnHome.Visible = false;
                    strtptxt = "/ TP";
                }
                string strLeave = dsadmin.Tables[0].Rows[0]["Leave_Home"].ToString();
                if (strLeave == "1" && (dsAdminSetup.Tables[0].Rows.Count > 0))
                {
                    lblhomepage.Visible = true;
                    btnHome.Visible = false;
                    strleavetxt = " / Leave";

                }
                string strExpen = dsadmin.Tables[0].Rows[0]["Expense_Home"].ToString();
                if (strExpen == "1")
                {
                    lblhomepage.Visible = true;
                    //btnHome.Visible = false;
                }
                string strdocadd = dsadmin.Tables[0].Rows[0]["Listeddr_Add_Home"].ToString();
                if (strdocadd == "1")
                {
                    lblhomepage.Visible = true;
                    //btnHome.Visible = false;
                }
                string strdocdeac = dsadmin.Tables[0].Rows[0]["Listeddr_Deact_Home"].ToString();
                if (strdocdeac == "1")
                {
                    lblhomepage.Visible = true;
                    //btnHome.Visible = false;
                }


                //   menu1.Title = this.Page.Title;
                // menu1.FindControl("btnBack").Visible = false;

            }

            lbltext.Text = strdcrtxt + strtptxt + strleavetxt;
        

         
        }

    }
    private void FillDoc()
    {
        grdListedDR.DataSource = null;
        grdListedDR.DataBind();

        ListedDR LstDoc1 = new ListedDR();
        dsDoc = LstDoc1.getListedDr_MGRapp(sfCode, 2, div_code);
        if (dsDoc.Tables[0].Rows.Count > 0)
        {
           
            grdListedDR.Visible = true;
            grdListedDR.DataSource = dsDoc;
            grdListedDR.DataBind();
        }
        else
        {
            btnHome.Visible = true;
            grdListedDR.DataSource = dsDoc;
            grdListedDR.DataBind();
        }
    }

    private void FillDcr()
    {
        grdDCR.DataSource = null;
        grdDCR.DataBind();
        DCR dr = new DCR();
        if (div_code.Contains(','))
            div_code = div_code.Substring(0, div_code.Length - 1);
        dsDcr = dr.get_DCR_Pending_Approval(sfCode, div_code);
        if (dsDcr.Tables[0].Rows.Count > 0)
        {
            grdDCR.Visible = true;
            grdDCR.DataSource = dsDcr;
            grdDCR.DataBind();
        }
        else
        {
            grdDCR.DataSource = dsDcr;
            grdDCR.DataBind();
        }
    }
    private void FillDoc_Deact()
    {
        grdListedDR1.DataSource = null;
        grdListedDR1.DataBind();

        ListedDR LstDoc = new ListedDR();
        dsDoc1 = LstDoc.getListedDr_MGRNew(sfCode, 3, div_code);
        if (dsDoc1.Tables[0].Rows.Count > 0)
        {
          
            grdListedDR1.Visible = true;
            grdListedDR1.DataSource = dsDoc1;
            grdListedDR1.DataBind();
        }
        else
        {
            btnHome.Visible = true;
            grdListedDR1.DataSource = dsDoc1;
            grdListedDR1.DataBind();
        }
    }

    private void FillDoc_AddDeactivate()
    {
        grdadddeactivate.DataSource = null;
        grdadddeactivate.DataBind();

        ListedDR lstAdd = new ListedDR();
        dsDoc = lstAdd.getListedDr_adddeact(sfCode, 2, 3, div_code);
        if (dsDoc.Tables[0].Rows.Count > 0)
        {
            grdadddeactivate.Visible = true;
            grdadddeactivate.DataSource = dsDoc;
            grdadddeactivate.DataBind();
        }
        else
        {
            btnHome.Visible = true;
            grdadddeactivate.DataSource = dsDoc;
            grdadddeactivate.DataBind();
        }

    }
    private void FillLeave()
    {
        grdLeave.DataSource = null;
        grdLeave.DataBind();

        AdminSetup adm = new AdminSetup();
        dsAdminSetup = adm.getLeave_approve(sfCode, 2,div_code);
        if (dsAdminSetup.Tables[0].Rows.Count > 0)
        {
            grdLeave.Visible = true;
            grdLeave.DataSource = dsAdminSetup;
            grdLeave.DataBind();
        }
        else
        {
            btnHome.Visible = true;
            grdLeave.DataSource = dsAdminSetup;
            grdLeave.DataBind();
        }
    }
    private void FillTourPlan()
    {
        //TourPlan tp = new TourPlan();
        TP_New tp = new TP_New();

        dsTP = tp.get_TP_Pending_Approval(sfCode, div_code);
        if (dsTP.Tables[0].Rows.Count > 0)
        {
            //string strGetMR = dsTP.Tables[0].Rows[0]["sf_code"].ToString();
            //if (strGetMR.Substring(0, 2) != "MR")
            //{
                grdTP_Calander.Visible = true;
                grdTP_Calander.DataSource = dsTP;
                grdTP_Calander.DataBind();

        //    }
        //    else
        //    {
        //        btnHome.Visible = true;
        //        grdTP.Visible = true;
        //        grdTP.DataSource = dsTP;
        //        grdTP.DataBind();
        //    }
        }
        else
        {
            btnHome.Visible = true;
            grdTP.Visible = true;
            grdTP.DataSource = dsTP;
            grdTP.DataBind();
        }


    }
    private void FillExp()
    {
        //TourPlan tp = new TourPlan();
        TP_New tp = new TP_New();

        dsTP = tp.get_Exp_Approval(div_code);
        if (dsTP.Tables[0].Rows.Count > 0)
        {
            //string strGetMR = dsTP.Tables[0].Rows[0]["sf_code"].ToString();
            //if (strGetMR.Substring(0, 2) != "MR")
            //{
            GridView1.Visible = true;
            GridView1.DataSource = dsTP;
            GridView1.DataBind();

            //    }
            //    else
            //    {
            //        btnHome.Visible = true;
            //        grdTP.Visible = true;
            //        grdTP.DataSource = dsTP;
            //        grdTP.DataBind();
            //    }
        }
        else
        {
            btnHome.Visible = true;
            GridView1.Visible = true;
            GridView1.DataSource = dsTP;
            GridView1.DataBind();
        }


    }

    //Populate the Secondary Sales grid which are waiting for approval
    private void FillSecSales()
    {
        grdSecSales.DataSource = null;
        grdSecSales.DataBind();
        SecSale ss = new SecSale();
        //Get the approval required list
        dsSecSales = ss.get_SecSales_Pending_Approval(sfCode, 1);
        if (dsSecSales.Tables[0].Rows.Count > 0)
        {
            grdSecSales.Visible = true;
            grdSecSales.DataSource = dsSecSales;
            grdSecSales.DataBind();
        }
        else
        {
            btnHome.Visible = true;
            grdSecSales.DataSource = dsSecSales;
            grdSecSales.DataBind();
        }
    }

    protected void grdSecSales_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //7
            //e.Row.Cells[7].Text = "sdf";
            //lblMonth.Text = getMonthName(lblMonth.Text);

        }
    }
    protected void btnLogout_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Index.aspx");
    }

    protected void grdTP_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        string ActTerrtotal = "";
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblMonth = (Label)e.Row.FindControl("lblMonth");
            lblMonth.Text = getMonthName(lblMonth.Text);
            // e.Row.Cells[5].Text = "Click here to Approve " + lblMonth.Text + " "+ dsTP.Tables[0].Rows[0]["Tour_Year"].ToString();
            ActTerrtotal = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "sf_code"));
            if (ActTerrtotal.Contains("MR"))
            {
                e.Row.Cells[8].Visible = false;
            }
            else
            {
                e.Row.Cells[7].Visible = false;
            }
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

    protected void btnHome_Click(object sender, EventArgs e)
    {
        //Response.Redirect("MGR_Home.aspx");
        if (Session["sf_type"].ToString() == "1") // MR Login
        {

            Response.Redirect("Default_MR.aspx");

        }
        else if (Session["sf_type"].ToString() == "2") // MGR Login
        {

            Response.Redirect("~/MGR_Home.aspx");


        }
        else if (Session["sf_type"].ToString() == "3") // DIS Login
        {

            Response.Redirect("~/DIS_Home.aspx");


        }
    }
    
}