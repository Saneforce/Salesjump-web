using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data;
using Bus_EReport;

public partial class MasterFiles_MR_SecSales_SecSalesEntry : System.Web.UI.Page
{
    #region "Variable Declarations"
        DataSet dsProduct = null;
        DataSet dsYear = null;
        string sf_code = string.Empty;
        string div_code = string.Empty;                
        DataSet dsState = new DataSet();
        DataSet dsSale = new DataSet();
        DataSet dsSecSale = new DataSet();
        DataSet dsReject = new DataSet();
        DataSet dsRate = new DataSet();
        DataSet dsOption = new DataSet();
        DataSet dsclbal = new DataSet();
        string cl_bal_sub = string.Empty;
        string state_code = string.Empty;
        string Prod_Grp = string.Empty;
        bool bTotalField = false;
        bool bCustCol = false;
        bool bCustCol_1 = false;
    
        int iErrReturn = -1;
        int iDay = -1;
        int iMonth = -1;
        int iYear = -1;
        int iStockiest_code = -1;
        DateTime SelDate;
        string sDate = string.Empty;
        static int iSNo = 0;
        int prod_cnt = 0;
        string refer = string.Empty;
        string[] sec_val;
        bool total_needed;
        bool value_needed;
        string sHeadNo = string.Empty;
        DataSet dsSF = new DataSet();
        string sMgr = string.Empty;
        int stock_code = -1;
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        //Get the sf_code & div_code from session
        sf_code = Session["sf_code"].ToString();
        div_code = Session["div_code"].ToString();

        if (!Page.IsPostBack) // Only on first time page load
        {
            menu1.Title = this.Page.Title;
            menu1.FindControl("btnBack").Visible = false;

            //Populate Year dropdown
            FillYear();

            lblStatus.Visible = false;

            //Populate Stockiest dropdown as per sf_code
            FillStockiest();

            //Option_Edit();

            //This querystring "refer" will populate when the manager clicks the entry for approval / rejection from manager index page
            if (Request.QueryString["refer"] != null)
            {
                //This querystring "refer" contains sf_code, month, year & stockiest
                refer = Request.QueryString["refer"].ToString().Trim();
                sec_val = refer.Split('-');
                sf_code = sec_val[0].ToString();

                ////Populate Stockiest dropdown as per sf_code
                //FillStockiest();

                //Set the default month for the entry for approval / rejection 
                ddlMonth.SelectedValue = sec_val[1].ToString();

                //Set the default year for the entry for approval / rejection 
                ddlYear.SelectedValue = sec_val[2].ToString();

                //Set the default stockiest for the entry for approval / rejection 
                ddlStockiest.SelectedValue = sec_val[3].ToString();

                //Populate the entered values by the FieldForce (i.e., MR)
                ShowSecSale();
                PopulateEnteredValues();

                //Hide the Save & Submit button for approval / rejection
                btn.Visible = false;
                btnSubmit.Visible = false;

                //Show the Save & Submit button for approval / rejection
                btnApprove.Visible = true;
                btnReject.Visible = true;                
            }
            else
            {
                ////Populate Stockiest dropdown as per sf_code
                //FillStockiest();
                
                //Fetch Rejection Details
                SecSale ss = new SecSale();
                dsReject = ss.get_SecSales_Rejection(sf_code, div_code);
                // If Rejection is done by the manager then 
                if (dsReject.Tables[0].Rows.Count > 0) 
                {
                    //Populate the title as Rejection
                    menu1.Title = menu1.Title + " ( Resubmit for Rejection ) ";
                    
                    // Provide the rejection reason on tooltip
                    lblReject.ToolTip = dsReject.Tables[0].Rows[0]["Reject_Reason"].ToString(); //"Rejected by Manager";

                    //Set the default month, year & stockiest for rejection 
                    ddlMonth.SelectedValue = dsReject.Tables[0].Rows[0]["Month"].ToString();
                    ddlYear.SelectedValue = dsReject.Tables[0].Rows[0]["Year"].ToString();
                    ddlStockiest.SelectedValue = dsReject.Tables[0].Rows[0]["Stockiest_Code"].ToString();

                    //Disable month, year & stockiest dropdown 
                    ddlMonth.Enabled = false;
                    ddlStockiest.Enabled = false;
                    ddlYear.Enabled = false;

                    //Disable Go button
                    btnGo.Enabled = false;

                    //Show the reject reason label
                    lblReject.Visible = true;

                    //Populate the entered values by the FieldForce (i.e., MR)
                    ShowSecSale();
                    PopulateEnteredValues();
                }
            }
        }

        iSNo = 0;

        Option_Edit();

        if (ViewState["option_edit"] != null)
        {
            if (ViewState["option_edit"].ToString() == "1")
            {
                //if (ViewState["OB_Tot_Row"] != null)
                //    ViewState["OB_Tot_Row"] = 0;

                ViewState["option_edit"] = null;
                ViewState["OB_Tot_Row"] = null;
                ViewState["disable_needed"] = null;
                ViewState["tot_plus"] = null;
                ViewState["cl_bal"] = null;
                ViewState["opr_cd"] = null;
                ViewState["disable"] = null;
                ViewState["total_row"] = null;
                ViewState["cl_bal_qty"] = null;
            }
        }

        ////if (ViewState["prod_cnt"] != null)
        ////    hidprdcnt.Value = ViewState["prod_cnt"].ToString();

        ScriptManager.RegisterStartupScript(this, GetType(), "hide_total_row", "hide_total_row();", true);
        ScriptManager.RegisterStartupScript(this, GetType(), "hide_catg_row", "hide_catg_row();", true);
        
    }

    private void Option_Edit()
    {
        //Option Edit
        SecSale ss = new SecSale();
        dsOption = ss.Get_SS_Option_Edit(div_code, sf_code, 4);
        if (dsOption != null)
        {
            if (dsOption.Tables[0].Rows.Count > 0)
            {
                ViewState["option_edit"] = "1";

                if (ViewState["mon"] != null)
                {
                    if ((ViewState["mon"].ToString().Trim() == dsOption.Tables[0].Rows[0].ItemArray.GetValue(1).ToString()) &&
                        (ViewState["yr"].ToString().Trim() == dsOption.Tables[0].Rows[0].ItemArray.GetValue(2).ToString()) &&
                        (ViewState["stk"].ToString().Trim() == dsOption.Tables[0].Rows[0].ItemArray.GetValue(4).ToString()))
                    {
                        return;
                    }
                }

                sHeadNo = dsOption.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                iMonth = Convert.ToInt32(dsOption.Tables[0].Rows[0].ItemArray.GetValue(1).ToString());
                iYear = Convert.ToInt32(dsOption.Tables[0].Rows[0].ItemArray.GetValue(2).ToString());
                sMgr = dsOption.Tables[0].Rows[0].ItemArray.GetValue(3).ToString();
                stock_code = Convert.ToInt32(dsOption.Tables[0].Rows[0].ItemArray.GetValue(4).ToString());

                ViewState["mon"] = iMonth.ToString().Trim();
                ViewState["yr"] = iYear.ToString().Trim();
                ViewState["stk"] = stock_code.ToString().Trim();

                SalesForce sf = new SalesForce();
                dsSF = sf.getSfName(sMgr);
                if (dsSF.Tables[0].Rows.Count > 0)
                    sMgr = dsSF.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

                ddlMonth.SelectedValue = iMonth.ToString();
                ddlYear.SelectedValue = iYear.ToString();
                ddlStockiest.SelectedValue = stock_code.ToString();

                //lblStatus.Visible = true;
                //lblStatus.Text = "Stockiest " + ddlStockiest.SelectedItem.Text + " is allowed to edit the existing entry by " + sMgr;

                //menu1.Title = menu1.Title + " ( Resubmit for Edit ) ";


                ddlMonth.Enabled = false;
                ddlYear.Enabled = false;
                ddlStockiest.Enabled = false;
                btnGo.Enabled = false;

                //lblStatus.Visible = false;
                ShowSecSale();
                PopulateEnteredValues();

            }
            else
            {
                ViewState["option_edit"] = null;
            }
        }
        else
        {
            ViewState["option_edit"] = null;
        }
        //Option Edit

    }

    //Populate the Year dropdown
    private void FillYear()
    {
        try
        {
            TourPlan tp = new TourPlan();
            dsYear = tp.Get_TP_Edit_Year(div_code); // Get the Year for the Division
            if (dsYear.Tables[0].Rows.Count > 0)
            {
                for (int k = Convert.ToInt16(dsYear.Tables[0].Rows[0]["Year"]); k <= DateTime.Now.Year + 1; k++)
                {
                    ddlYear.Items.Add(k.ToString());                
                }
            }
            ddlYear.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            ErrorLog err = new ErrorLog();
            iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Entry", "FillYear()");
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Unknown error exists. Please contact customer care for support');</script>");
        }
    }

    //Populate the Stockiest dropdown based on sf_code
    private void FillStockiest()
    {
        try
        {
            DCR dc = new DCR();
            dsSale = dc.getStockiest(sf_code);
            if (dsSale.Tables[0].Rows.Count > 0)
            {
                ddlStockiest.DataValueField = "Stockist_Code";
                ddlStockiest.DataTextField = "Stockist_Name";
                ddlStockiest.DataSource = dsSale;
                ddlStockiest.DataBind();
            }
            ddlStockiest.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            ErrorLog err = new ErrorLog();
            iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Entry", "FillStockiest()");
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Unknown error exists. Please contact customer care for support');</script>");
        }
    }

    //Get the last day for the given month & year
    private int GetLastDay(int cMonth, int cYear)
    {
        int cday = 0;
        
        if (cMonth == 1)
            cday = 31;
        else if (cMonth == 2)
        {
            if (cYear % 4 == 0)
                cday = 29;
            else
                cday = 28;
        }
        else if (cMonth == 3)
            cday = 31;
        else if (cMonth == 4)
            cday = 30;
        else if (cMonth == 5)
            cday = 31;
        else if (cMonth == 6)
            cday = 30;
        else if (cMonth == 7)
            cday = 31;
        else if (cMonth == 8)
            cday = 31;
        else if (cMonth == 9)
            cday = 30;
        else if (cMonth == 10)
            cday = 31;
        else if (cMonth == 11)
            cday = 30;
        else if (cMonth == 12)
            cday = 31;

        return cday; 
    }

    private void Clear_Viewstate() 
    {
        if (ViewState["OB_Tot_Row"] != null)
            ViewState["OB_Tot_Row"] = null;

        if (ViewState["tot_plus"] != null)
            ViewState["tot_plus"] = null;

        if (ViewState["disable"] != null)
            ViewState["disable"] = null;

        if (ViewState["disable_needed"] != null)
            ViewState["disable_needed"] = null;

        if (ViewState["total_row"] != null)
            ViewState["total_row"] = null;
    }

    protected void btnGo_Click(object sender, EventArgs e)
    {
        try
        {
            Clear_Viewstate();
            
            if ((ddlMonth.SelectedIndex > 0) && (ddlYear.SelectedIndex > 0))
            {
                //Check for Record Exists for the month
                SecSale ss = new SecSale();
                bool bRecordExist = ss.sRecordExist(div_code, sf_code, Convert.ToInt16(ddlMonth.SelectedValue.ToString()), Convert.ToInt16(ddlYear.SelectedValue.ToString()), Convert.ToInt16(ddlStockiest.SelectedValue.ToString()), 2);
                if(bRecordExist )
                {
                    lblStatus.Visible=true;
                    pnlSecSale.Visible = false;
                    btnSubmit.Visible = false;
                    btn.Visible = false;
                }
                else
                {
                    //Populate the Secondary Sales grid for entry
                    lblStatus.Visible= false;
                    ShowSecSale();
                    PopulateEnteredValues();
                    //ScriptManager.RegisterStartupScript(this, GetType(), "disable_ctrl", "disable_ctrl();", true);
                    //ClientScript.RegisterStartupScript(GetType(), "disable_ctrl", "<SCRIPT LANGUAGE='javascript'>disable_ctrl()</script>", true);
                }

                ddlStockiest.Enabled = false;
                ddlMonth.Enabled = false;
                ddlYear.Enabled = false;
            }
            else
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Select Month and Year');</script>");
        }
        catch (Exception ex)
        {
            ErrorLog err = new ErrorLog();
            iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Entry", "btnGo_Click()");
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Unknown error exists. Please contact customer care for support');</script>");
        }
    }

    private void ShowSecSale()
    {
        try
        {
            //Show the Save &Submit button
            pnlSecSale.Visible = true;
            btn.Visible = true;
            btnSubmit.Visible = true;

            //Get Secondary Sales master data from DB and bind it with Secondary Sales Header Repeater
            SecSale ss = new SecSale();
            //dsRate = ss.getAddionalRptSaleMaster(div_code);
            //if (dsRate.Tables[0].Rows.Count > 0)
            //{
            //    ViewState["total_needed"] = dsRate.Tables[0].Rows[0].ItemArray.GetValue(0).ToString().Trim();

            //    if (dsRate.Tables[0].Rows[0].ItemArray.GetValue(0).ToString().Trim() == "1")
            //        total_needed = true;
            //    else
            //        total_needed = false;

            //    ViewState["value_needed"] = dsRate.Tables[0].Rows[0].ItemArray.GetValue(1).ToString().Trim();
            //    if (dsRate.Tables[0].Rows[0].ItemArray.GetValue(1).ToString().Trim() == "1")
            //        value_needed = true;
            //    else
            //        value_needed = false;
            
            //}

            //if(total_needed)
            //    dsSale = ss.getSaleMaster(true, div_code);
            //else
                dsSale = ss.getSaleMaster(false, div_code);

            rptSecSaleHeader.DataSource = dsSale;
            rptSecSaleHeader.DataBind();

            dsSecSale = ss.getSaleMaster_ValueNeeded(false, div_code);
            rptSecSaleHdrVal.DataSource = dsSecSale;
            rptSecSaleHdrVal.DataBind();

            //Get the state for the MR
            UnListedDR LstDR = new UnListedDR();
            dsState = LstDR.getState(sf_code);
            if (dsState.Tables[0].Rows.Count > 0)
                state_code = dsState.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

            //Get the selected month
            if (ddlMonth.SelectedIndex > 0)
                iMonth = Convert.ToInt16(ddlMonth.SelectedValue.ToString());

            //Get the selected year
            if (ddlYear.SelectedIndex > 0)
                iYear = Convert.ToInt16(ddlYear.SelectedValue.ToString());

            //Get the last date of the selected month for the year
            if ((iMonth > 0) && (iYear > 0))
                iDay = GetLastDay(iMonth, iYear);

            sDate = iDay.ToString().Trim() + "-" + iMonth.ToString().Trim() + "-" + iYear.ToString().Trim();
            SelDate = Convert.ToDateTime(sDate);

            //Get Product master data from DB and bind it with Product Repeater
            //DataSet dsProd = ss.getProduct(div_code, state_code, SelDate);
            dsRate = ss.getAddionalRptSaleMaster(div_code);
            if (dsRate != null)
            {
                if (dsRate.Tables[0].Rows.Count > 0)
                    Prod_Grp = dsRate.Tables[0].Rows[0].ItemArray.GetValue(3).ToString().Trim();
            }

            DataSet dsProd = ss.getProduct_Total(div_code, state_code, SelDate, Prod_Grp);
            rptProduct.DataSource = dsProd;
            rptProduct.DataBind();

            //Store the Product Dataset to Viewstate for future reference. This will avoid to fetch the data from DB. 
            if (dsProd != null)
                ViewState["dsProd"] = dsProd;

            //Populate the secondary sale repeater as per the product. i.e., secondary sale fields for each product
            foreach (RepeaterItem ri in rptProduct.Items)
            {
                prod_cnt += 1;
                Repeater rptDetSecSale = (Repeater)ri.FindControl("rptDetSecSale");
                
                //if (ViewState["total_needed"] != null)
                //{
                //    if (ViewState["total_needed"].ToString() == "1")
                //        total_needed = true;
                //    else
                //        total_needed = false;
                //}

                //if (ViewState["value_needed"] != null)
                //{
                //    if (ViewState["value_needed"].ToString() == "1")
                //        value_needed = true;
                //    else
                //        value_needed = false;
                //}

                //if(total_needed)
                //    dsSecSale = ss.getSaleMaster(true, div_code);
                //else
                    dsSecSale = ss.getSaleMaster(false, div_code);

                rptDetSecSale.DataSource = dsSecSale;
                rptDetSecSale.DataBind();

                foreach (RepeaterItem checkItem in rptDetSecSale.Items)
                {
                    TextBox txtSecSale = (TextBox)checkItem.FindControl("txtSecSale");
                    txtSecSale.Visible = true;
                }
            }

            hidprdcnt.Value = prod_cnt.ToString();

            ViewState["prod_cnt"] = prod_cnt.ToString();
        }
        catch (Exception ex)
        {
            ErrorLog err = new ErrorLog();
            iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Entry", "ShowSecSale()");
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Unknown error exists. Please contact customer care for support');</script>");
        }

    }

    protected void rptProduct_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        try
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                HiddenField hidRate = (HiddenField)e.Item.FindControl("hidRate");
                HiddenField hidMRPRate = (HiddenField)e.Item.FindControl("hidMRPRate");
                HiddenField hidDistRate = (HiddenField)e.Item.FindControl("hidDistRate");
                HiddenField hidNSRRate = (HiddenField)e.Item.FindControl("hidNSRRate");
                HiddenField hidTargRate = (HiddenField)e.Item.FindControl("hidTargRate");

                Literal litsno = (Literal)e.Item.FindControl("litsno");
                //iSNo += 1;
                //litsno.Text = "&nbsp;&nbsp;" + iSNo.ToString();

                Literal litpname = (Literal)e.Item.FindControl("litpname");
                litpname.Text = "&nbsp;&nbsp;" + litpname.Text;

                Literal litpack = (Literal)e.Item.FindControl("litpack");
                litpack.Text = "&nbsp;&nbsp;" + litpack.Text;

                Literal litrate = (Literal)e.Item.FindControl("litrate");
                litrate.Text = "";
                SecSale ss = new SecSale();
                if (ViewState["rate"] != null)
                {
                    if (ViewState["rate"].ToString().Trim() == "R")
                        litrate.Text = hidRate.Value.ToString();
                    else if (ViewState["rate"].ToString().Trim() == "M")
                        litrate.Text = hidMRPRate.Value.ToString();
                    else if (ViewState["rate"].ToString().Trim() == "D")
                        litrate.Text = hidDistRate.Value.ToString();
                    else if (ViewState["rate"].ToString().Trim() == "N")
                        litrate.Text = hidNSRRate.Value.ToString();
                    else if (ViewState["rate"].ToString().Trim() == "T")
                        litrate.Text = hidTargRate.Value.ToString();
                }
                else
                {
                    dsRate = ss.getAddionalRptSaleMaster(div_code);
                    if (dsRate.Tables[0].Rows.Count > 0)
                    {
                        ViewState["rate"] = dsRate.Tables[0].Rows[0].ItemArray.GetValue(2).ToString().Trim();
                        if (dsRate.Tables[0].Rows[0].ItemArray.GetValue(2).ToString().Trim() == "R")
                            litrate.Text = hidRate.Value.ToString();
                        else if (dsRate.Tables[0].Rows[0].ItemArray.GetValue(2).ToString().Trim() == "M")
                            litrate.Text = hidMRPRate.Value.ToString();
                        else if (dsRate.Tables[0].Rows[0].ItemArray.GetValue(2).ToString().Trim() == "D")
                            litrate.Text = hidDistRate.Value.ToString();
                        else if (dsRate.Tables[0].Rows[0].ItemArray.GetValue(2).ToString().Trim() == "N")
                            litrate.Text = hidNSRRate.Value.ToString();
                        else if (dsRate.Tables[0].Rows[0].ItemArray.GetValue(2).ToString().Trim() == "T")
                            litrate.Text = hidTargRate.Value.ToString();
                    }
                }
                litrate.Text = "&nbsp;&nbsp;" + litrate.Text;

                //Populate the secondary sale repeater as per the product. i.e., secondary sale fields for each product
                
                Repeater rptDetSecSale = (Repeater)e.Item.FindControl("rptDetSecSale");
                //dsSecSale = ss.getSaleMaster(true, div_code);

                //if (ViewState["total_needed"] != null)
                //{
                //    if (ViewState["total_needed"].ToString() == "1")
                //        total_needed = true;
                //    else
                //        total_needed = false;
                //}

                //if (ViewState["value_needed"] != null)
                //{
                //    if (ViewState["value_needed"].ToString() == "1")
                //        value_needed = true;
                //    else
                //        value_needed = false;
                //}

                //if (total_needed)
                //    dsSecSale = ss.getSaleMaster(true, div_code);
                //else
                    dsSecSale = ss.getSaleMaster(false, div_code);

                rptDetSecSale.DataSource = dsSecSale;
                rptDetSecSale.DataBind();

                if(dsSecSale != null)
                    ViewState["dsSecSale"] = dsSecSale;

                //Populate Sec Sale Qty & Values1
                HiddenField hidPCode = (HiddenField)e.Item.FindControl("hidPCode");

                if (hidPCode.Value == "Tot_Prod")
                {                    

                    var pnamecol = (HtmlTableCell)e.Item.FindControl("tdpname");
                    pnamecol.Visible = false;

                    var tdpunit = (HtmlTableCell)e.Item.FindControl("tdpunit");
                    tdpunit.Visible = false;

                    var tdprate = (HtmlTableCell)e.Item.FindControl("tdprate");
                    tdprate.Visible = false;

                    var prodcol = (HtmlTableCell)e.Item.FindControl("tdpcode");                    
                    prodcol.ColSpan = 4;
                    prodcol.Align = "Center";
                    prodcol.BgColor = "Cyan";

                    litsno.Text = " Total ";
                    

                }


                //Merge Category / Group Row for Header
                HiddenField hidPDesc = (HiddenField)e.Item.FindControl("hidPDesc");
                if ((hidPDesc.Value == "Catg_Code") || (hidPDesc.Value == "Grp_Code"))
                {
                    ViewState["prod_grp"] = "Catg_Code";

                    var pnamecol = (HtmlTableCell)e.Item.FindControl("tdpname");
                    pnamecol.Visible = false;

                    var tdpunit = (HtmlTableCell)e.Item.FindControl("tdpunit");
                    tdpunit.Visible = false;

                    var tdprate = (HtmlTableCell)e.Item.FindControl("tdprate");
                    tdprate.Visible = false;

                    var prodcol = (HtmlTableCell)e.Item.FindControl("tdpcode");
                    prodcol.ColSpan = 4;
                    prodcol.Style.Add("borderRightStyle", "none");
                    //prodcol.Align = "Center";
                    prodcol.BgColor = "LightGrey"; // "Beige"; //"LightYellow";
                    //prodcol.BgColor = "BurlyWood"; //"LightYellow";
                    litsno.Text = "<span><b>" + litpname.Text + "</b></span>"; //" Total ";
                    
                }
                else
                {
                    ViewState["prod_grp"] = "1";

                    if (litsno.Text != " Total ")
                    {
                        iSNo += 1;
                        litsno.Text = "&nbsp;&nbsp;" + iSNo.ToString();
                    }
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLog err = new ErrorLog();
            iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Entry", "rptProduct_ItemDataBound()");
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Unknown error exists. Please contact customer care for support');</script>");
        }

    }

    private int GetLastMonth(string sMonth)
    {
        int iMonth = 0;

        if (sMonth == "1")
            iMonth = 12;
        else if(sMonth == "2")
            iMonth = 1;
        else if (sMonth == "3")
            iMonth = 2;
        else if (sMonth == "4")
            iMonth = 3;
        else if (sMonth == "5")
            iMonth = 4;
        else if (sMonth == "6")
            iMonth = 5;
        else if (sMonth == "7")
            iMonth = 6;
        else if (sMonth == "8")
            iMonth = 7;
        else if (sMonth == "9")
            iMonth = 8;
        else if (sMonth == "10")
            iMonth = 9;
        else if (sMonth == "11")
            iMonth = 10;
        else if (sMonth == "12")
            iMonth = 11;

        return iMonth;

    }

    protected void rptDetSecSale_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        try
        {
            

            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {  
                HiddenField hidSecSaleVal = (HiddenField)e.Item.FindControl("hidSecSaleVal");
                HiddenField hidSecSaleCode = (HiddenField)e.Item.FindControl("hidSecSaleCode");
                HiddenField hidSecSaleOpr = (HiddenField)e.Item.FindControl("hidSecSaleOpr");
                HiddenField hidSecSaleName = (HiddenField)e.Item.FindControl("hidSecSaleName");
                HiddenField hidSecSaleSub = (HiddenField)e.Item.FindControl("hidSecSaleSub");
                HiddenField hidSecSaleOB = (HiddenField)e.Item.FindControl("hidSecSaleOB");    
                
                //Setting Backcolor on Total Row
                //if (hidSecSaleSub.Value.ToString().Trim() == "OB")
                if(hidSecSaleOB.Value.ToString().Trim() == "1")
                {
                    if(ViewState["OB_Tot_Row"] == null)
                        ViewState["OB_Tot_Row"] = 0;

                    ViewState["OB_Tot_Row"] = Convert.ToInt16(ViewState["OB_Tot_Row"].ToString()) + 1;
                }

                TextBox txtSecSale = (TextBox)e.Item.FindControl("txtSecSale");
                TextBox txtval = (TextBox)e.Item.FindControl("txtval");
                //Label txtval = (Label)e.Item.FindControl("lblSecSale");
                TextBox txtSub = (TextBox)e.Item.FindControl("txtSub");

                txtval.Attributes.Add("readOnly", "true");
                //txtval.Text = txtval.Text.ToString("#,0.00");

                //Calculating & Disabling Closing Balance field
                if (hidSecSaleName.Value.ToString() == "Closing Balance")
                {
                    SecSale sss = new SecSale();
                    bool bDisableNeeded = sss.isDisableNeeded(div_code, Convert.ToInt16(Convert.ToDecimal(hidSecSaleCode.Value.ToString())), 1);
                    if (bDisableNeeded)
                        ViewState["disable_needed"] = 1;
                }

                //Total '+' field
                if (hidSecSaleSub.Value.Trim() == "Tot+")
                {
                    if (ViewState["tot_plus"] != null)
                    {
                        if (bTotalField == false)
                        {
                            hidPlus.Value = ViewState["tot_plus"].ToString();
                            ScriptManager.RegisterStartupScript(this, GetType(), "disable_control", "disable_control();", true);
                            
                            bTotalField = true;
                        }
                        //ViewState["tot_plus"]  = "";
                    }
                    //if (ViewState["plus_field"] != null)
                    //{
                    //    if (ViewState["plus_field"].ToString() == "1")
                    //    {
                    //        hidPlus.Value = ViewState["tot_plus"].ToString();
                    //        ScriptManager.RegisterStartupScript(this, GetType(), "disable_control", "disable_control();", true);
                    //    }
                    //}
                    //else
                    //{
                    //    if (ViewState["tot_plus"] == null)
                    //        ViewState["tot_plus"] = 0;
                    //    else
                    //    {
                    //        if (ViewState["plus_field"] == null)
                    //        {
                    //            ViewState["tot_plus"] = Convert.ToInt16(ViewState["tot_plus"].ToString()) + 1;
                    //            if (hidSecSaleOpr.Value == "-")
                    //            {
                    //                ViewState["tot_plus"] = Convert.ToInt16(ViewState["tot_plus"].ToString()) - 1;
                    //                ViewState["plus_field"] = "1";
                    //            }
                    //        }
                    //    }
                    //}
                }
                else
                {
                    if (bTotalField == false) //Not a Tot+ Field
                    {
                        if (hidSecSaleOpr.Value == "+")
                        {
                            if (ViewState["tot_plus"] == null)
                                ViewState["tot_plus"] = 1;
                            else
                            {
                                ViewState["tot_plus"] = Convert.ToInt16(ViewState["tot_plus"].ToString()) + 1;
                            }
                        }
                    }

                }
                //End of Total '+' field


                ////Total '-' field
                //if (ViewState["minus_field"] != null)
                //{
                //    if (ViewState["minus_field"].ToString() == "1")
                //    {
                //        hidMinus.Value = ViewState["tot_minus"].ToString();
                //        ScriptManager.RegisterStartupScript(this, GetType(), "disable_control", "disable_control();", true);
                //    }
                //}
                //else
                //{
                //    if (ViewState["tot_minus"] == null)
                //        ViewState["tot_minus"] = 0;
                //    else
                //    {
                //        if (ViewState["minus_field"] == null)
                //        {
                //            ViewState["tot_minus"] = Convert.ToInt16(ViewState["tot_minus"].ToString()) + 1;
                //            if (hidSecSaleOpr.Value == "C")
                //            {
                //                ViewState["tot_minus"] = Convert.ToInt16(ViewState["tot_minus"].ToString()) - 1;
                //                ViewState["minus_field"] = "1";
                //            }
                //        }
                //    }
                //}
                ////End of Total '-' field

                //Disable Closing Balance field
                if (ViewState["cl_bal"] != null)
                {
                    if (ViewState["cl_bal"].ToString() == "1")
                    {
                        hidClBal.Value = ViewState["opr_cd"].ToString();
                        if (hidClBal.Value.Trim().Length > 0)
                        {
                            if (ViewState["disable"] == null)
                            {
                                if (ViewState["disable_needed"] != null)
                                {
                                    if (ViewState["disable_needed"].ToString() == "1")
                                    {
                                        ViewState["disable"] = 1;
                                        ScriptManager.RegisterStartupScript(this, GetType(), "disable_control", "disable_control();", true);
                                        
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    if (ViewState["opr_cd"] == null)
                    {
                        ViewState["opr_cd"] = 0;
                    }
                    else
                    {
                        ViewState["opr_cd"] = Convert.ToInt16(ViewState["opr_cd"].ToString()) + 1; // To identify the closing balance location
                        if (hidSecSaleOpr.Value == "C")
                        {
                            ViewState["cl_bal"] = "1";
                        }
                    }
                }
                
            
                //Populate Value field for detail row as per setup
                //if ((hidSecSaleCode.Value.ToString() == "3.1") || (hidSecSaleCode.Value.ToString() == "9.1"))
                //{
                //    SecSale ss = new SecSale();
                //    bool bValueNeeded = ss.isTotalValueNeeded(div_code, 0);
                //    if (!bValueNeeded)
                //    {
                //        var col = e.Item.FindControl("tdSecVal");
                //        col.Visible = false;
                //    }
                //}
                //else
                //{
                    SecSale ss = new SecSale();
                    bool bValueNeeded = ss.isValueNeeded(div_code, Convert.ToInt16(Convert.ToDecimal(hidSecSaleCode.Value.ToString())), 1);
                    if (!bValueNeeded)
                    {
                        var col = e.Item.FindControl("tdSecVal");
                        col.Visible = false;
                    }

                    bool bSubNeeded = ss.isSubNeeded(div_code, Convert.ToInt16(Convert.ToDecimal(hidSecSaleCode.Value.ToString())), 1);
                    if (!bSubNeeded)
                    {
                        var subcol = e.Item.FindControl("tdSecSub");
                        subcol.Visible = false;
                    }

                //}
            }

            if (ViewState["total_row"] == null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "hide_total_row", "hide_total_row();", true);
                ViewState["total_row"] = "1";
            }

            //Total Row Highlight with Cyan
            if (ViewState["dsProd"] != null)
            {
                dsProduct = (DataSet)ViewState["dsProd"];
                if (dsProduct != null)
                {
                    int iprodcnt = dsProduct.Tables[0].Rows.Count;
                    if (iprodcnt > 0)
                    {
                        if (ViewState["OB_Tot_Row"] != null)
                        {
                            int ob_tot_row = Convert.ToInt32(ViewState["OB_Tot_Row"].ToString());

                            if (ob_tot_row == iprodcnt * 2)
                            {
                                var tdSecQty = (HtmlTableCell)e.Item.FindControl("tdSecQty");
                                tdSecQty.BgColor = "Cyan";
                                var tdSecVal = (HtmlTableCell)e.Item.FindControl("tdSecVal");
                                tdSecVal.BgColor = "Cyan";
                            }
                            else
                            {
                                var tdSecVal = (HtmlTableCell)e.Item.FindControl("tdSecVal");
                                tdSecVal.BgColor = "#FFF0E5";
                            }
                        }
                    }
                }
            }

            ////Product Group
            //if (ViewState["prod_grp"] != null)
            //{
            //    if (ViewState["prod_grp"].ToString() == "Catg_Code")
            //    {
            //        var tdSecQty = (HtmlTableCell)e.Item.FindControl("tdSecQty");
            //        tdSecQty.BgColor = "LightGreen";
            //        var tdSecVal = (HtmlTableCell)e.Item.FindControl("tdSecVal");
            //        tdSecVal.BgColor = "LightGreen";
            //    }
            //}

            //var trDet = (HtmlTableCell)e.Item.FindControl("trDet");
            //trDet.BgColor = "Cyan";

        }
        catch (Exception ex)
        {
            ErrorLog err = new ErrorLog();
            iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Entry", "rptDetSecSale_ItemDataBound()");
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Unknown error exists. Please contact customer care for support');</script>");
        }

    }

    protected void rptSecSaleHdrVal_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        try
        {
            HiddenField hidHdrSecSaleCode = (HiddenField)e.Item.FindControl("hidHdrSecSaleCode");


            //if ((hidHdrSecSaleCode.Value.ToString() == "3.1") || (hidHdrSecSaleCode.Value.ToString() == "9.1"))
            //{
            //    SecSale ss = new SecSale();
            //    bool bValueNeeded = ss.isTotalValueNeeded(div_code, 0);
            //    if (!bValueNeeded)
            //    {
            //        var col = e.Item.FindControl("tdHdrSecVal");
            //        col.Visible = false;
            //    }
            //}
            //else
            //{
                //Populate Value field for header row as per setup
                SecSale ss = new SecSale();
                bool bValueNeeded = ss.isValueNeeded(div_code, Convert.ToInt16(Convert.ToDecimal(hidHdrSecSaleCode.Value.ToString())), 1);
                if (!bValueNeeded)
                {
                    var col = e.Item.FindControl("tdHdrSecVal");
                    col.Visible = false;
                }

                //Populate Sub field for header row as per setup
                bool bSubNeeded = ss.isSubNeeded(div_code, Convert.ToInt16(Convert.ToDecimal(hidHdrSecSaleCode.Value.ToString())), 1);
                if (!bSubNeeded)
                {
                    var colsub = e.Item.FindControl("tdHdrSecSub");
                    colsub.Visible = false;
                }
            //}
        }
        catch (Exception ex)
        {
            ErrorLog err = new ErrorLog();
            iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Entry", "rptSecSaleHdrVal_ItemDataBound()");
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Unknown error exists. Please contact customer care for support');</script>");
        }

    }

    protected void rptSecSaleHeader_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        try
        {
            //Populate Value field for header row as per setup
            HiddenField hidMainHdrSecSaleCode = (HiddenField)e.Item.FindControl("hidMainHdrSecSaleCode");
            SecSale ss = new SecSale();
            bool bValueNeeded = ss.isValueNeeded(div_code, Convert.ToInt16(Convert.ToDecimal(hidMainHdrSecSaleCode.Value.ToString())), 1);
            var hdrcol = (HtmlTableCell)e.Item.FindControl("tdMainHdrSecVal");
            if (bValueNeeded)
            {
                hdrcol.ColSpan = 2;

                tdSNo.RowSpan = 2;
                tdPName.RowSpan = 2;
                tdPack.RowSpan = 2;
                tdRate.RowSpan = 2;
                //var col = e.Item.FindControl("tdMainHdrVal");
                //col.Visible = false;
            }
            else
            {
                //if ((hidMainHdrSecSaleCode.Value.ToString() == "3.1") || (hidMainHdrSecSaleCode.Value.ToString() == "9.1"))
                //{
                //    bValueNeeded = ss.isTotalValueNeeded(div_code, 0);
                //    if (bValueNeeded)
                //    {
                //        hdrcol.ColSpan = 2;
                //        //tdSNo.RowSpan = 2;
                //        //tdPName.RowSpan = 2;
                //        //tdPack.RowSpan = 2;
                //        //tdRate.RowSpan = 2;
                //    }
                //    else
                //        hdrcol.RowSpan = 2;
                //}
                //else
                    hdrcol.RowSpan = 2;

            }
            //bool bSubNeeded = ss.isSubNeeded(div_code, Convert.ToInt16(Convert.ToDecimal(hidMainHdrSecSaleCode.Value.ToString())), 1);
            //if (bValueNeeded && bSubNeeded)
            //    hdrcol.ColSpan = 3;
            //else if (!bValueNeeded && bSubNeeded)
            //    hdrcol.ColSpan = 2;

        }
        catch (Exception ex)
        {
            ErrorLog err = new ErrorLog();
            iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Entry", "rptSecSaleHeader_ItemDataBound()");
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Unknown error exists. Please contact customer care for support');</script>");
        }

    }

    private void PopulateEnteredValues()
    {
        try
        {
            SecSale ss = new SecSale();
            string sop = string.Empty;
            int lmonth = -1;
            int lyear = -1;
            int secsalecd = -1;
            int plus_qty = 0;
            int minus_qty = 0;
            double plus_val;
            double minus_val;
            int totpls_qty = 0;
            int cls_qty = 0;
            double totpls_val;
            double cls_bal;

            //calculating opening balance from last month opening balance
            lmonth = GetLastMonth(ddlMonth.SelectedValue.ToString());

            DataSet ds;
            Repeater rptDetSecSale;
            TextBox txtSecSale;
            TextBox txtval;
            //Label txtval;
            TextBox txtSub;
            HiddenField hidSecSaleCode;
            HiddenField hidPCode;
            HiddenField hidDistRate;
            HiddenField hidSecSaleSub;
            HiddenField hidSecSaleOB;
            HiddenField hidSecSaleCB;
            HiddenField hidSecSaleOpr;

            //Get the selected month, year & stockiest from the respective dropdowns
            iMonth = Convert.ToInt32(ddlMonth.SelectedValue.ToString());
            iYear = Convert.ToInt32(ddlYear.SelectedValue.ToString());
            iStockiest_code = Convert.ToInt32(ddlStockiest.SelectedValue.ToString());

            if (lmonth == 12)
                lyear = iYear - 1;
            else
                lyear = iYear;

            dsclbal = ss.Get_SS_ClBal_Sub(div_code);
            if (dsclbal != null)
            {
                if (dsclbal.Tables[0].Rows.Count > 0)
                {
                    cl_bal_sub = dsclbal.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                }
            }

            foreach (RepeaterItem pitem in rptProduct.Items)
            {
                hidPCode = (HiddenField)pitem.FindControl("hidPCode");
                hidDistRate = (HiddenField)pitem.FindControl("hidDistRate");
                rptDetSecSale = (Repeater)pitem.FindControl("rptDetSecSale");

                plus_qty = 0;
                minus_qty = 0;
                plus_val = 0.00;
                minus_val = 0.00;

                foreach (RepeaterItem item in rptDetSecSale.Items)
                {
                    hidSecSaleCode = (HiddenField)item.FindControl("hidSecSaleCode");
                    hidSecSaleSub = (HiddenField)item.FindControl("hidSecSaleSub");
                    hidSecSaleOB = (HiddenField)item.FindControl("hidSecSaleOB");
                    hidSecSaleCB = (HiddenField)item.FindControl("hidSecSaleCB");
                    hidSecSaleOpr = (HiddenField)item.FindControl("hidSecSaleOpr");

                    txtSecSale = (TextBox)item.FindControl("txtSecSale");
                    txtval = (TextBox)item.FindControl("txtval");
                    //txtval = (Label)item.FindControl("lblSecSale");
                    txtSub = (TextBox)item.FindControl("txtSub");


                    //Fetch the entered (exist) values from DB for the selected month, year & stockiest of sf_code and populate that into qty & value textboxes
                    dsSecSale = ss.getSaleEnteredQty(div_code, sf_code, iMonth, iYear, iStockiest_code, hidPCode.Value.ToString(), hidSecSaleCode.Value.ToString());

                    if (dsSecSale.Tables[0].Rows.Count > 0)
                    {
                        txtSecSale.Text = dsSecSale.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

                        if (txtSecSale.Text.Trim() == "0")
                            txtSecSale.Text = "";

                        txtval.Text = dsSecSale.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                        txtSub.Text = dsSecSale.Tables[0].Rows[0].ItemArray.GetValue(2).ToString(); // Ram

                        //Included Opening Balance logic for existing records
                        //if (hidSecSaleSub.Value.ToString() == "OB")
                        if (hidSecSaleOB.Value.ToString().Trim() == "1")
                        {
                            //ds = ss.getsecsalecode(div_code, "CB");
                            ds = ss.getsecsalecode(div_code, cl_bal_sub);
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                secsalecd = Convert.ToInt16(ds.Tables[0].Rows[0].ItemArray.GetValue(0).ToString());

                                dsProduct = ss.getClosingBalance(Convert.ToInt16(div_code), sf_code, iStockiest_code, lmonth, lyear, hidPCode.Value.ToString(), secsalecd);
                                if (dsProduct.Tables[0].Rows.Count > 0)
                                {
                                    if (dsProduct.Tables[0].Rows[0].ItemArray.GetValue(0).ToString().Trim().Length > 0)
                                    {
                                        if (Convert.ToInt32(dsProduct.Tables[0].Rows[0].ItemArray.GetValue(0).ToString()) > 0)
                                        {
                                            txtSecSale.Text = dsProduct.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                                            txtSecSale.Enabled = false;
                                            if (txtSecSale.Text.Trim().Length > 0)
                                            {
                                                ViewState["cl_bal_qty"] = txtSecSale.Text;
                                                txtval.Text = Convert.ToString(Convert.ToDecimal(txtSecSale.Text) * Convert.ToDecimal(hidDistRate.Value));
                                                if (Convert.ToDecimal(txtSecSale.Text.Trim()) > 0)
                                                {
                                                    // Do Nothing
                                                }
                                                else
                                                {
                                                    txtSecSale.Enabled = true;

                                                    if (txtSecSale.Text.Trim() == "0")
                                                        txtSecSale.Text = "";
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }

                    }
                    else // Closing Balance -- 9
                    {
                        //if (hidSecSaleSub.Value.ToString() == "OB")
                        if (hidSecSaleOB.Value.ToString().Trim() == "1")
                        {
                            //ds = ss.getsecsalecode(div_code , "CB");
                            ds = ss.getsecsalecode(div_code, cl_bal_sub);
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                secsalecd = Convert.ToInt16(ds.Tables[0].Rows[0].ItemArray.GetValue(0).ToString());

                                dsProduct = ss.getClosingBalance(Convert.ToInt16(div_code), sf_code, iStockiest_code, lmonth, lyear, hidPCode.Value.ToString(), secsalecd);
                                if (dsProduct.Tables[0].Rows.Count > 0)
                                {
                                    txtSecSale.Text = dsProduct.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                                    txtSecSale.Enabled = false;
                                    if (txtSecSale.Text.Trim().Length > 0)
                                    {
                                        ViewState["cl_bal_qty"] = txtSecSale.Text;
                                        txtval.Text = Convert.ToString(Convert.ToDecimal(txtSecSale.Text) * Convert.ToDecimal(hidDistRate.Value));
                                        if (Convert.ToDecimal(txtSecSale.Text.Trim()) > 0)
                                        {
                                            // Do Nothing
                                        }
                                        else
                                        {
                                            txtSecSale.Enabled = true;

                                            if (txtSecSale.Text.Trim() == "0")
                                                txtSecSale.Text = "";
                                        }
                                    }
                                }
                            }
                        }
                        //else if (hidSecSaleSub.Value.ToString() == "CB")
                        if (hidSecSaleCB.Value.ToString().Trim() == "1")
                        {
                            if (ViewState["cl_bal_qty"] != null)
                            {
                                txtSecSale.Text = ViewState["cl_bal_qty"].ToString();
                                txtval.Text = Convert.ToString(Convert.ToDecimal(txtSecSale.Text) * Convert.ToDecimal(hidDistRate.Value));
                            }
                        }
                    }

                    if (hidSecSaleOpr.Value == "+")
                    {
                        if (txtSecSale.Text.Trim().Length > 0)
                        {
                            if (hidSecSaleSub.Value == "Tot+")
                            {
                                txtSecSale.Text = plus_qty.ToString();
                                txtval.Text = Convert.ToString(Convert.ToDecimal(txtSecSale.Text) * Convert.ToDecimal(hidDistRate.Value));
                            }
                            else
                                plus_qty += Convert.ToInt32(txtSecSale.Text.Trim());
                        }
                    }
                    else if (hidSecSaleOpr.Value == "-")
                    {
                        if (txtSecSale.Text.Trim().Length > 0)
                        {
                            minus_qty += Convert.ToInt32(txtSecSale.Text.Trim());
                        }
                    }

                    //Closing Balance
                    if (hidSecSaleCB.Value.ToString().Trim() == "1")
                    {
                        totpls_qty = plus_qty - minus_qty;
                        txtSecSale.Text = totpls_qty.ToString();
                        txtval.Text = Convert.ToString(Convert.ToDecimal(txtSecSale.Text) * Convert.ToDecimal(hidDistRate.Value));
                    }

                } //End of SecSale For Loop

            } //End of Product For Loop
        }
        catch (Exception ex)
        {
            ErrorLog err = new ErrorLog();
            iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Entry", "PopulateEnteredValues()");
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Unknown error exists. Please contact customer care for support');</script>");
        }
    }

    //private void PopulateEnteredValues()
    //{
    //    try
    //    {
    //        SecSale ss = new SecSale();
    //        string sop = string.Empty;
    //        int lmonth = -1;
    //        int lyear = -1;
    //        int secsalecd = -1;

    //        //calculating opening balance from last month opening balance
    //        lmonth = GetLastMonth(ddlMonth.SelectedValue.ToString());

    //        DataSet ds;
    //        Repeater rptDetSecSale;
    //        TextBox txtSecSale;
    //        TextBox txtval;
    //        //Label txtval;
    //        TextBox txtSub;
    //        HiddenField hidSecSaleCode;
    //        HiddenField hidPCode;
    //        HiddenField hidDistRate;
    //        HiddenField hidSecSaleSub;
    //        HiddenField hidSecSaleOB;
    //        HiddenField hidSecSaleCB;

    //        //Get the selected month, year & stockiest from the respective dropdowns
    //        iMonth = Convert.ToInt32(ddlMonth.SelectedValue.ToString());
    //        iYear = Convert.ToInt32(ddlYear.SelectedValue.ToString());
    //        iStockiest_code = Convert.ToInt32(ddlStockiest.SelectedValue.ToString());

    //        if (lmonth == 12)
    //            lyear = iYear - 1;
    //        else
    //            lyear = iYear;

    //        dsclbal = ss.Get_SS_ClBal_Sub(div_code);
    //        if (dsclbal != null)
    //        {
    //            if (dsclbal.Tables[0].Rows.Count > 0)
    //            {
    //                cl_bal_sub = dsclbal.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
    //            }
    //        }

    //        foreach (RepeaterItem pitem in rptProduct.Items)
    //        {
    //            hidPCode = (HiddenField)pitem.FindControl("hidPCode");
    //            hidDistRate = (HiddenField)pitem.FindControl("hidDistRate");  
    //            rptDetSecSale = (Repeater)pitem.FindControl("rptDetSecSale");
    //            foreach (RepeaterItem item in rptDetSecSale.Items)
    //           {
    //                hidSecSaleCode = (HiddenField)item.FindControl("hidSecSaleCode");
    //                hidSecSaleSub = (HiddenField)item.FindControl("hidSecSaleSub");
    //                hidSecSaleOB = (HiddenField)item.FindControl("hidSecSaleOB");
    //                hidSecSaleCB = (HiddenField)item.FindControl("hidSecSaleCB");
    //                txtSecSale = (TextBox)item.FindControl("txtSecSale");
    //                txtval = (TextBox)item.FindControl("txtval");
    //                //txtval = (Label)item.FindControl("lblSecSale");
    //                txtSub = (TextBox)item.FindControl("txtSub");


    //                //Fetch the entered (exist) values from DB for the selected month, year & stockiest of sf_code and populate that into qty & value textboxes
    //                dsSecSale = ss.getSaleEnteredQty(div_code, sf_code, iMonth, iYear, iStockiest_code, hidPCode.Value.ToString(), hidSecSaleCode.Value.ToString());

    //                if (dsSecSale.Tables[0].Rows.Count > 0)
    //                {
    //                    txtSecSale.Text = dsSecSale.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                        
    //                    if (txtSecSale.Text.Trim() == "0")
    //                        txtSecSale.Text = "";
                        
    //                    txtval.Text = dsSecSale.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
    //                    txtSub.Text = dsSecSale.Tables[0].Rows[0].ItemArray.GetValue(2).ToString(); // Ram

    //                    //Included Opening Balance logic for existing records
    //                    //if (hidSecSaleSub.Value.ToString() == "OB")
    //                    if (hidSecSaleOB.Value.ToString().Trim() == "1")
    //                    {
    //                        //ds = ss.getsecsalecode(div_code, "CB");
    //                        ds = ss.getsecsalecode(div_code, cl_bal_sub);
    //                        if (ds.Tables[0].Rows.Count > 0)
    //                        {
    //                            secsalecd = Convert.ToInt16(ds.Tables[0].Rows[0].ItemArray.GetValue(0).ToString());

    //                            dsProduct = ss.getClosingBalance(Convert.ToInt16(div_code), sf_code, iStockiest_code, lmonth, lyear, hidPCode.Value.ToString(), secsalecd);
    //                            if (dsProduct.Tables[0].Rows.Count > 0)
    //                            {
    //                                txtSecSale.Text = dsProduct.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
    //                                txtSecSale.Enabled = false;
    //                                if (txtSecSale.Text.Trim().Length > 0)
    //                                {
    //                                    ViewState["cl_bal_qty"] = txtSecSale.Text;
    //                                    txtval.Text = Convert.ToString(Convert.ToDecimal(txtSecSale.Text) * Convert.ToDecimal(hidDistRate.Value));
    //                                    if (Convert.ToDecimal(txtSecSale.Text.Trim()) > 0)
    //                                    {
    //                                        // Do Nothing
    //                                    }
    //                                    else
    //                                    {
    //                                        txtSecSale.Enabled = true;

    //                                        if (txtSecSale.Text.Trim() == "0")
    //                                            txtSecSale.Text = "";
    //                                    }
    //                                }
    //                            }
    //                        }
    //                    }

    //                }
    //                else // Closing Balance -- 9
    //                {
    //                    //if (hidSecSaleSub.Value.ToString() == "OB")
    //                    if (hidSecSaleOB.Value.ToString().Trim() == "1")
    //                    {
    //                        //ds = ss.getsecsalecode(div_code , "CB");
    //                        ds = ss.getsecsalecode(div_code, cl_bal_sub);
    //                        if (ds.Tables[0].Rows.Count > 0)
    //                        {
    //                            secsalecd = Convert.ToInt16( ds.Tables[0].Rows[0].ItemArray.GetValue(0).ToString());

    //                            dsProduct = ss.getClosingBalance(Convert.ToInt16(div_code), sf_code, iStockiest_code, lmonth, lyear, hidPCode.Value.ToString(), secsalecd);
    //                            if (dsProduct.Tables[0].Rows.Count > 0)
    //                            {
    //                                txtSecSale.Text = dsProduct.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
    //                                txtSecSale.Enabled = false;
    //                                if (txtSecSale.Text.Trim().Length > 0)
    //                                {
    //                                    ViewState["cl_bal_qty"] = txtSecSale.Text;
    //                                    txtval.Text = Convert.ToString(Convert.ToDecimal(txtSecSale.Text) * Convert.ToDecimal(hidDistRate.Value));
    //                                    if (Convert.ToDecimal(txtSecSale.Text.Trim()) > 0)
    //                                    {
    //                                        // Do Nothing
    //                                    }
    //                                    else
    //                                    {
    //                                        txtSecSale.Enabled = true;

    //                                        if (txtSecSale.Text.Trim() == "0")
    //                                            txtSecSale.Text = "";
    //                                    }
    //                                }
    //                            }
    //                        }
    //                    }
    //                    //else if (hidSecSaleSub.Value.ToString() == "CB")
    //                    if (hidSecSaleCB.Value.ToString().Trim() == "1")
    //                    {
    //                        if (ViewState["cl_bal_qty"] != null)
    //                        {
    //                            txtSecSale.Text = ViewState["cl_bal_qty"].ToString();
    //                            txtval.Text = Convert.ToString(Convert.ToDecimal(txtSecSale.Text) * Convert.ToDecimal(hidDistRate.Value));
    //                        }
    //                    }
    //                }
    //            }
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        ErrorLog err = new ErrorLog();
    //        iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Entry", "PopulateEnteredValues()");
    //        ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Unknown error exists. Please contact customer care for support');</script>");
    //    }
    //}

    protected void btn_Click(object sender, EventArgs e)
    {
        //Status should be 0 for Save
        SecSaleEntry(0);
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        //Status should be 1 for Final Submit
        int approval_needed = -1;

        SecSale ss = new SecSale();
        dsSale = ss.getAddionalSaleMaster(div_code);
        if (dsSale.Tables[0].Rows.Count > 0)
        {
            if ((dsSale.Tables[0].Rows[0].ItemArray.GetValue(3) != null) && (dsSale.Tables[0].Rows[0].ItemArray.GetValue(3).ToString().Trim().Length > 0))
            {
                approval_needed = Convert.ToInt16(dsSale.Tables[0].Rows[0].ItemArray.GetValue(3).ToString());
                ViewState["approval_needed"] = approval_needed.ToString();
            }
        }

        if(approval_needed == 0)
            SecSaleEntry(1);// Yes
        else
            SecSaleEntry(2);// No

        if (ViewState["bIsValid"] != null)
        {
            if (ViewState["bIsValid"].ToString().Trim() == "1")
            {
                // Do Nothing
            }
            else
            {
                lblStatus.Visible = true;
                pnlSecSale.Visible = false;
                btnSubmit.Visible = false;
                btn.Visible = false;
            }
        }
        else
        {
            if (ViewState["option_edit"] != null)
            {
                //Do Nothing
            }
            else
            {
                //lblStatus.Visible = true;
                pnlSecSale.Visible = false;
                btnSubmit.Visible = false;
                btn.Visible = false;
            }
        }

        ddlMonth.Enabled = true;
        ddlYear.Enabled = true;

    }

    private void SecSaleEntry(int iStatus)
    {
        try
        {
            bool bDataEntry = true;
            SecSale ss = new SecSale();
            string sop = string.Empty;
            bool bIsValid = true;

            Repeater rptDetSecSale;
            TextBox txtSecSale;
            TextBox txtval;
            //Label txtval;
            TextBox txtSub;
            HiddenField hidSecSaleCode;
            HiddenField hidSecSaleSub;
            HiddenField hidPCode;
            HiddenField hidRate;
            HiddenField hidMRPRate;
            HiddenField hidDistRate;
            HiddenField hidNSRRate;
            HiddenField hidTargRate;
            HiddenField hidSecSaleCB;   
            Literal litpack;
            int stockiest_code = -1;
            int iReturn = -1;
            int head_sl_no = -1;
            int det_sl_no = -1;

            //Querystring "refer" will populate for manager approval / rejection
            if (Request.QueryString["refer"] != null)
            {
                refer = Request.QueryString["refer"].ToString().Trim();
                sec_val = refer.Split('-');
                sf_code = sec_val[0].ToString();
            }


            //Validation on Closing Balance
            foreach (RepeaterItem pitem in rptProduct.Items)
            {
                rptDetSecSale = (Repeater)pitem.FindControl("rptDetSecSale");
                foreach (RepeaterItem item in rptDetSecSale.Items)
                {
                    hidSecSaleCB = (HiddenField)item.FindControl("hidSecSaleCB");
                    hidSecSaleSub = (HiddenField)item.FindControl("hidSecSaleSub");
                    txtSecSale = (TextBox)item.FindControl("txtSecSale");

                    //if (hidSecSaleSub.Value.ToString() == "CB")
                    if (hidSecSaleCB.Value.ToString() == "1")
                    {
                        if (txtSecSale.Text.Trim().Length > 0)
                        {
                            if (Convert.ToInt32(txtSecSale.Text.Trim()) < 0)
                            {
                                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Negative Stock found');</script>");
                                txtSecSale.BackColor = System.Drawing.Color.Red; //System.Drawing.ColorTranslator.FromHtml("#0097AC");
                                bIsValid = false;
                                ViewState["bIsValid"] = "1";
                            }
                            else
                                txtSecSale.BackColor = System.Drawing.Color.White;
                        }
                    }
                }
            }

            if (bIsValid)
            {
                //Get the selected month, year & stockiest from the respective dropdowns
                iMonth = Convert.ToInt32(ddlMonth.SelectedValue.ToString());
                iYear = Convert.ToInt32(ddlYear.SelectedValue.ToString());
                stockiest_code = Convert.ToInt32(ddlStockiest.SelectedValue.ToString());

                //Check for existing reconrd (sale entry for the month, year & stockiest of sf_code)
                bool sRecordExists = ss.sRecordExist(div_code, sf_code, iMonth, iYear, stockiest_code);

                //Get state_code for sf_code
                UnListedDR LstDR = new UnListedDR();
                dsSale = LstDR.getState(sf_code);
                if (dsSale.Tables[0].Rows.Count > 0)
                    state_code = dsSale.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

                //Populate Secondary Sales tables.
                iReturn = ss.RecordAdd(div_code, sf_code, Convert.ToInt32(state_code), stockiest_code, iMonth, iYear, iStatus, sRecordExists);

                if (iStatus == 3) // Rejection           
                    iReturn = ss.RecordUpdate(div_code, sf_code, stockiest_code, iMonth, iYear, Session["sf_code"].ToString(), txtReason.Text.Trim());
                else if (iStatus == 2) // Approval
                    iReturn = ss.RecordUpdate(div_code, sf_code, stockiest_code, iMonth, iYear, Session["sf_code"].ToString());

                head_sl_no = ss.getmaxrecord(div_code);

                foreach (RepeaterItem pitem in rptProduct.Items)
                {
                    hidPCode = (HiddenField)pitem.FindControl("hidPCode");
                    hidRate = (HiddenField)pitem.FindControl("hidRate");
                    hidMRPRate = (HiddenField)pitem.FindControl("hidMRPRate");
                    hidDistRate = (HiddenField)pitem.FindControl("hidDistRate");
                    hidNSRRate = (HiddenField)pitem.FindControl("hidNSRRate");
                    hidTargRate = (HiddenField)pitem.FindControl("hidTargRate");
                    litpack = (Literal)pitem.FindControl("litpack");

                    //Inserting data into the table "Trans_SS_Entry_Detail"
                    sRecordExists = ss.sDetailRecordExist(div_code, hidPCode.Value.ToString(), head_sl_no.ToString());
                    iReturn = ss.DetailRecordAdd(head_sl_no, div_code, hidPCode.Value.ToString(), hidMRPRate.Value.ToString(), hidRate.Value.ToString(), hidDistRate.Value.ToString(), hidTargRate.Value.ToString(), hidNSRRate.Value.ToString(), sRecordExists);

                    //det_sl_no = ss.getDetmaxrecord(div_code);
                    dsProduct = ss.getDtlSNo(div_code, sf_code, iMonth, iYear, stockiest_code, hidPCode.Value.ToString());

                    if (dsProduct.Tables[0].Rows.Count > 0)
                        det_sl_no = Convert.ToInt32(dsProduct.Tables[0].Rows[0].ItemArray.GetValue(0).ToString());

                    if ((head_sl_no > 0) && (det_sl_no > 0))
                    {
                        if (pitem.ItemType == ListItemType.Item || pitem.ItemType == ListItemType.AlternatingItem)
                        {
                            rptDetSecSale = (Repeater)pitem.FindControl("rptDetSecSale");
                            foreach (RepeaterItem item in rptDetSecSale.Items)
                            {
                                hidSecSaleCode = (HiddenField)item.FindControl("hidSecSaleCode");
                                txtSecSale = (TextBox)item.FindControl("txtSecSale");
                                txtval = (TextBox)item.FindControl("txtval");
                                //txtval = (Label)item.FindControl("lblSecSale");
                                txtSub = (TextBox)item.FindControl("txtSub");

                                //if (iStatus == 1)
                                //{
                                //    if (txtSecSale.Text == "0" || txtSecSale.Text.Trim().Length == 0)
                                //    {
                                //        bDataEntry = false;
                                //    }

                                //}

                                //Inserting data into the table "Trans_SS_Entry_Detail_Value"
                                sRecordExists = ss.sDetailValRecordExist(div_code, iMonth, iYear, hidPCode.Value.ToString(), hidSecSaleCode.Value.ToString(), stockiest_code);
                                iReturn = ss.DetailValueRecordAdd(det_sl_no, div_code, hidSecSaleCode.Value.ToString().Trim(), txtSecSale.Text.Trim(), txtval.Text.Trim(), txtSub.Text.Trim(), sRecordExists);
                            }
                        }
                    }
                }

                if (iReturn > 0)
                {
                    if (iStatus == 0)
                    {
                        ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Saved Successfully');</script>");
                        ViewState["total_row"] = null;
                        ViewState["tot_plus"] = null;
                    }
                    else if (iStatus == 1)
                    {
                        ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Submitted Successfully');</script>");
                        ResetAll();
                        Option_Edit();
                    }
                    else if (iStatus == 2)
                    {
                        if (ViewState["approval_needed"] != null)
                        {
                            if (ViewState["approval_needed"].ToString() == "0")
                            {
                                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Approved Successfully');</script>");
                                ViewState["total_row"] = null;
                                ViewState["tot_plus"] = null;
                            }
                            else
                            {
                                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Submitted Successfully');</script>");
                                ResetAll();
                                Option_Edit();
                                ViewState["Option_Edit_Done"] = "1";
                            }
                        }
                        else
                        {
                            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Submitted Successfully');</script>");
                            ResetAll();
                            Option_Edit();
                            ViewState["Option_Edit_Done"] = "1";
                        }
                    }
                    else if (iStatus == 3)
                    {
                        ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Rejected Successfully');</script>");
                        ViewState["total_row"] = null;
                        ViewState["tot_plus"] = null;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLog err = new ErrorLog();
            iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Entry", "SecSaleEntry()");
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Unknown error exists. Please contact customer care for support');</script>");
        }
    }

    protected void btnApprove_Click(object sender, EventArgs e)
    {
        //Status should be 2 for Approval
        SecSaleEntry(2);
    }
    
    protected void btnReject_Click(object sender, EventArgs e)
    {
        txtReason.Visible = true;
        btnApprove.Enabled = false;
        btnReject.Enabled = false;
        btnBack.Visible = true; 
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        //Status should be 3 for Rejection
        SecSaleEntry(3);
        Response.Redirect("~/MasterFiles/MGR/MGR_Index.aspx");
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        ResetAll();
    }

    private void ResetAll()
    {
        ddlStockiest.SelectedIndex = 0;
        ddlMonth.SelectedIndex = 0;
        ddlYear.SelectedIndex = 0;
        ddlStockiest.Enabled = true;
        ddlYear.Enabled = true;
        ddlMonth.Enabled = true;
        pnlSecSale.Visible = false;
        btnSubmit.Visible = false;
        btn.Visible = false;
        //lblStatus.Text = "";
        lblStatus.Visible = false;
        lblReject.Text = "";
        btnGo.Enabled = true;
        ViewState["option_edit"] = null;
        ViewState["OB_Tot_Row"] = null;
        ViewState["total_row"] = null;
        ViewState["tot_plus"] = null;
    }
}   