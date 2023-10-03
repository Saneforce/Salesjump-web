using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
using System.Data.SqlClient;
using Newtonsoft.Json;
using System.Web.Services;

public partial class MasterFiles_WrkTypeWise_Allowance : System.Web.UI.Page
{
    bool isSavedPage = false;
    string div_code = string.Empty;
    string sfCode = string.Empty;
    string sftype = string.Empty;
    DataSet dsSalesForce = new DataSet();
    DataSet dsTP = new DataSet();
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    string Month = string.Empty;
    string Year = string.Empty;
    string sf_name = string.Empty;
    string sf_code = string.Empty;
    static string para = string.Empty;
    static string cal_Type = string.Empty;
    static string amount = string.Empty;
    static string mon = string.Empty;
    static string year = string.Empty;
    static string sf = string.Empty;
    int num1 = 0;
    int time;
    int total = 0;
    int total1 = 0;
    int fare = 0;
    int allow = 0;
    int dis = 0;

    protected void Page_Load(object sender, EventArgs e)
    {

        Month = Request.QueryString["Month"].ToString();
        mon = Month;
        Year = Request.QueryString["Year"].ToString();
        year = Year;
        try
        {
            div_code = Session["div_code"].ToString();
            //sftype = Session["sf_type"].ToString();
           // string SesSFCode = Convert.ToString(Session["Sf_Code"]);
           // if (Request.QueryString["SF"] != null)
            //{
                sfCode = Request.QueryString["sf_code"].ToString();
                sf = sfCode;
                sf_name = Request.QueryString["sf_name"].ToString();
            //}
           // else if (sftype != "3" && sfCode == "")
            //{
                //sfCode = "1";//SesSFCode;
            //}

            if (!Page.IsPostBack)
            {
                menu1.Title = Page.Title;
                menu1.FindControl("btnBack").Visible = true;
                //btnSave.Visible = false;
                base.OnPreInit(e);
                FillBaseLevel();
                ServerStartTime = DateTime.Now;

            }
        }
        catch (Exception)
        {

        }
        if (!this.IsPostBack)
        {
            this.BindGrid();
        }

        //overtotal();
    }
    private void overtotal(GridView grd, string control_name)
    {
        var footerRow = grd.FooterRow;
        Label Tot_Exp = (Label)footerRow.FindControl(control_name);
        num1 += Convert.ToInt32(Tot_Exp.Text);
        Txt_OverallTotal.Text = num1.ToString();
    }
    private void BindGrid()
    {
        TourPlan tp = new TourPlan();
        dsSalesForce = tp.GetExp(div_code, sfCode);

        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            gvCustomers.DataSource = dsSalesForce;
            gvCustomers.DataBind();
            overtotal(gvCustomers, "lblAmountTotal");

        }
    }


    private void FillBaseLevel()
    {
        TourPlan tp = new TourPlan();
        dsSalesForce = tp.GetBaseWorkType(sfCode, div_code, Month, Year);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            grdWTAllowance.DataSource = dsSalesForce;
            grdWTAllowance.DataBind();
            //lblSelect.Visible = false;
            overtotal(grdWTAllowance, "lblGrandTotal");
            //btnSave.Visible = true;
        }
    }

    public DataSet FillRou()
    {
        TourPlan tp = new TourPlan();
        dsSalesForce = tp.GetRouType(div_code);
        return dsSalesForce;
    }
    public DataSet Fillsf()
    {
        TourPlan tp = new TourPlan();
        dsSalesForce = tp.Getsf(div_code);
        return dsSalesForce;
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

    //protected void btnSave_Click(object sender, EventArgs e)
    //{
    //    System.Threading.Thread.Sleep(time);

    //    Territory terr = new Territory();
    //    int iReturn = 0;
    //    //int iMaxState = 0;


    //    foreach (GridViewRow gridRow in grdWTAllowance.Rows)
    //    {
    //        Label lblDCR_Date = (Label)gridRow.Cells[1].FindControl("lblDCR_Date");
    //        string date = lblDCR_Date.Text;
    //        Label lblWorktype_Name = (Label)gridRow.Cells[1].FindControl("lblWorktype_Name");
    //        string Name = lblWorktype_Name.Text;

    //        DropDownList ddlAllowanceType = (DropDownList)gridRow.Cells[1].FindControl("Territory_Type");
    //        string Place = ddlAllowanceType.SelectedItem.ToString();
    //        string Place_no = ddlAllowanceType.SelectedValue;


    //        TextBox txtAlw = (TextBox)gridRow.Cells[1].FindControl("txtAlw");
    //        string allow = txtAlw.Text;
    //        TextBox txtDis = (TextBox)gridRow.Cells[1].FindControl("txtDis");
    //        string dis = txtDis.Text;
    //        TextBox txtFare = (TextBox)gridRow.Cells[1].FindControl("txtFare");
    //        string fare = txtFare.Text;
    //        TextBox TxtTotal = (TextBox)gridRow.Cells[1].FindControl("txtTotal");
    //        string tot = TxtTotal.Text;




    //        if (iReturn >= 0)
    //        {

    //            iReturn = terr.WrkType_Expense_Update(date, Name, Place, Place_no, div_code, allow, dis, fare, tot, sf_name, sfCode);

    //        }
    //        else
    //        {

    //        }
    //        if (iReturn == 0)
    //        {

    //            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Updated Successfully')</script>");


    //        }

    //    }
    //    //Refresh the page to load GridView with records from database table.
    //    Response.Redirect(Request.Url.AbsoluteUri);
    //}



    protected void grdWTAllowance_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                TextBox lblqy = (TextBox)e.Row.FindControl("txtTotal");
                int qty = Int32.Parse(lblqy.Text);
                total = total + qty;
                TextBox lblqy1 = (TextBox)e.Row.FindControl("txtFare");
                int qty1 = Int32.Parse(lblqy1.Text);
                fare = fare + qty1;
                TextBox lblqy2 = (TextBox)e.Row.FindControl("txtAlw");
                int qty2 = Int32.Parse(lblqy2.Text);
                allow = allow + qty2;
                TextBox lblqy3 = (TextBox)e.Row.FindControl("txtDis");
                int qty3 = Int32.Parse(lblqy3.Text);
                dis = dis + qty3;

            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[0].ColumnSpan = 4;
                e.Row.Cells[1].Visible = false;
                e.Row.Cells[2].Visible = false;
                e.Row.Cells[3].Visible = false;
                Label lblGrandTotal = (Label)e.Row.FindControl("lblGrandTotal");
                lblGrandTotal.Text = total.ToString();
                Label lblGrandTotal3 = (Label)e.Row.FindControl("lblGrandTotal3");
                lblGrandTotal3.Text = fare.ToString();
                Label lblGrandTotal1 = (Label)e.Row.FindControl("lblGrandTotal1");
                lblGrandTotal1.Text = allow.ToString();
                Label lblGrandTotal2 = (Label)e.Row.FindControl("lblGrandTotal2");
                lblGrandTotal2.Text = dis.ToString();


            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                DropDownList Territory_Type = (DropDownList)e.Row.FindControl("Territory_Type");
                DropDownList SF_Name = (DropDownList)e.Row.FindControl("SF_Name");
                if (Territory_Type != null && SF_Name != null)
                {
                    DataRowView row = (DataRowView)e.Row.DataItem;
                    Territory_Type.SelectedIndex = Territory_Type.Items.IndexOf(Territory_Type.Items.FindByText(row["Plan_Name"].ToString()));
                    SF_Name.SelectedIndex = SF_Name.Items.IndexOf(SF_Name.Items.FindByText(row["Sf_Name"].ToString()));
                }

                if (Territory_Type != null && SF_Name != null)
                {
                    DataRowView row = (DataRowView)e.Row.DataItem;
                    Territory_Type.SelectedIndex = Territory_Type.Items.IndexOf(Territory_Type.Items.FindByText(row["Plan_Name"].ToString()));
                    SF_Name.SelectedIndex = SF_Name.Items.IndexOf(SF_Name.Items.FindByText(row["Sf_Name"].ToString()));
                }

            }
        }
        catch (Exception)
        {

        }
    }
    [WebMethod(EnableSession = true)]
    public static string SaveData(string empdata)//WebMethod to Save the data  
    {
        TourPlan terr = new TourPlan();
        int iReturn = 0;
        string flag = "1";

        var serializeData = JsonConvert.DeserializeObject<List<Employee>>(empdata);

        foreach (var data in serializeData)
        {
            para = data.FName;
            cal_Type = data.LName;
            amount = data.EmailId;
            if (iReturn >= 0)
            {
                terr.Expense_Update(para, amount, HttpContext.Current.Session["div_code"].ToString(),
                sf, cal_Type,
                HttpContext.Current.Session["Sf_Code"].ToString(), mon, year, flag);

            }
            else
            { }



        }



        return "yes";
    }



    protected void gvCustomers_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lbl_cre_by = (Label)e.Row.FindControl("lbl_cre_by");

            Label lblqym = (Label)e.Row.FindControl("lbl3");
            Label lblqy = (Label)e.Row.FindControl("lbl2");
            if (lblqy.Text == "") lblqy.Text = "0";
            int qty = Int32.Parse(lblqy.Text.Replace("(-)", "").Trim());
            if (lblqym.Text == "0")
            {

                total1 = total1 + qty;
            }
            else
            {

                total1 = total1 - qty;
            }
            if (lbl_cre_by.Text.Substring(0, 3).ToLower() == "mgr")
                e.Row.Attributes.Add("style", "background-color:lightblue;");
            else if (lbl_cre_by.Text.ToLower() == "admin")
                e.Row.Attributes.Add("style", "background-color:lightpink;");
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            Label lblAmountTotal = (Label)e.Row.FindControl("lblAmountTotal");
            lblAmountTotal.Text = total1.ToString();
            //lblAmountTot.Text = lblAmountTotal.Text;
        }

    }


    protected void Button2_Click(object sender, EventArgs e)
    {
        saveData("2");
    }
    private void saveData(string flag)
    {
        System.Threading.Thread.Sleep(time);
        TourPlan terr = new TourPlan();
        int iReturn = 0;
      
            
                if (iReturn >= 0)
                {
                    terr.Expense_App_MGR(sfCode, Month, Year, flag, div_code, sf_name);
                }
                else
                { }
                if (iReturn == 0)
                {

                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Send To Apperval');</script>");

                }

            


            //Response.Redirect(Request.Url.AbsoluteUri);
     
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);

        Territory terr = new Territory();
        int iReturn = 0;
        var footerRow = grdWTAllowance.FooterRow;
        Label Total_allow = (Label)footerRow.FindControl("lblGrandTotal1");
        string Tot_allow = Total_allow.Text;
        Label Tot_Dis = (Label)footerRow.FindControl("lblGrandTotal2");
        string Tot_dis = Tot_Dis.Text;
        Label Tot_Fare = (Label)footerRow.FindControl("lblGrandTotal3");
        string tot_Fare = Tot_Fare.Text;
        Label Tot_Exp = (Label)footerRow.FindControl("lblGrandTotal");
        string Tot_exp = Tot_Exp.Text;
        var footerrow1 = gvCustomers.FooterRow;
        Label Tot_Add = (Label)footerrow1.FindControl("lblAmountTotal");
        string Tot_Additial = Tot_Add.Text;
        string GrandTotal = Txt_OverallTotal.Text;


        if (iReturn >= 0)
        {

            iReturn = terr.Expense_Amount_Update(sf_name, sfCode, Month, Year, Tot_allow, Tot_dis, tot_Fare, Tot_exp, Tot_Additial, GrandTotal, div_code);

        }
        else
        {

        }
        if (iReturn == 0)
        {

            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Updated Successfully')</script>");


        }

        //}


        //Refresh the page to load GridView with records from database table.
        Response.Redirect(Request.Url.AbsoluteUri);

    }
}
