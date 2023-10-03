using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;

public partial class SecondarySales_SetUp : System.Web.UI.Page
{

    #region "Variable Declarations"
        DataSet dsSale = null;
        string div_code = string.Empty;
        string strError = string.Empty;
        int iErrReturn = -1;
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {        
        div_code = Session["div_code"].ToString();
        if (!Page.IsPostBack)
        {
            menu1.Title = this.Page.Title;
            menu1.FindControl("btnBack").Visible = false;
            btnSubmit.Visible = false;
            btnClear.Visible = false;
            FillSecSales();
        }
    }

    private void FillSecSales()
    {
        try
        {
            SecSale ss = new SecSale();

            //Positive fields
            dsSale = ss.getSaleMaster("+", div_code);
            if (dsSale.Tables[0].Rows.Count > 0)
            {
                grdSecSales.DataSource = dsSale;
                grdSecSales.DataBind();
                lblPlus.Visible = true;
                btnSubmit.Visible = true;
                btnClear.Visible = true;
        
            }
            else
            {
                lblPlus.Visible = false;
            }
            //Negative fields
            dsSale = ss.getSaleMaster("-", div_code);
            if (dsSale.Tables[0].Rows.Count > 0)
            {
                grdSecSalesMinus.DataSource = dsSale;
                grdSecSalesMinus.DataBind();
                lblMinus.Visible = true;
                btnSubmit.Visible = true;
                btnClear.Visible = true;
        
            }
            else
            {
                lblMinus.Visible = false;
            }
            //Closing Balance
            dsSale = ss.getSaleMaster("C", div_code);
            if (dsSale.Tables[0].Rows.Count > 0)
            {
                grdSecSalesOthers.DataSource = dsSale;
                grdSecSalesOthers.DataBind();
                LblOth.Visible = true;
                btnSubmit.Visible = true;
                btnClear.Visible = true;
        
            }
            else
            {
                LblOth.Visible = false;
            }

            dsSale = ss.getSaleMaster("D", div_code);
            if (dsSale.Tables[0].Rows.Count > 0)
            {
                grdCol.DataSource = dsSale;
                grdCol.DataBind();
                lblCol.Visible = true;
                btnSubmit.Visible = true;
                btnClear.Visible = true;

            }
            else
            {
                lblCol.Visible = false;
            }
        
        }
        catch (Exception ex)
        {
            ErrorLog err = new ErrorLog();
            iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Setup", "FillSecSales()");
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Unknown error exists. Please contact customer care for support');</script>");                        
        }
    }

    protected void grdSecSales_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblSaleCode = (Label)e.Row.FindControl("lblSaleCode");
                CheckBox chkDisplay = (CheckBox)e.Row.FindControl("chkDisplay");
                CheckBox chkValue = (CheckBox)e.Row.FindControl("chkValue");
                CheckBox chkCarryFwd = (CheckBox)e.Row.FindControl("chkCarryFwd");
                CheckBox chkDisable = (CheckBox)e.Row.FindControl("chkDisable");
                CheckBox chkCalc = (CheckBox)e.Row.FindControl("chkCalc");
                CheckBox chkCalcDis = (CheckBox)e.Row.FindControl("chkCalcDis");
                CheckBox chkCalcSale = (CheckBox)e.Row.FindControl("chkCalcSale");
                CheckBox chkCarryFld = (CheckBox)e.Row.FindControl("chkCarryFld");
                CheckBox chkSub = (CheckBox)e.Row.FindControl("chkSub");
                TextBox txtSub = (TextBox)e.Row.FindControl("txtSub");
                TextBox txtOrder = (TextBox)e.Row.FindControl("txtOrder");

                if ((lblSaleCode != null) && (lblSaleCode.Text.Trim().Length > 0))
                {
                    SecSale ss = new SecSale();
                    DataSet dsSale = new DataSet();
                    dsSale = ss.getSaleSetup(Convert.ToInt32(div_code), Convert.ToInt32(lblSaleCode.Text.Trim()));
                    if (dsSale.Tables[0].Rows.Count > 0)
                    {
                        if (dsSale.Tables[0].Rows[0]["Display_Needed"].ToString() == "1")
                            chkDisplay.Checked = true;
                        if (dsSale.Tables[0].Rows[0]["Value_Needed"].ToString() == "1")
                            chkValue.Checked = true;
                        if (dsSale.Tables[0].Rows[0]["Carry_Fwd_Needed"].ToString() == "1")
                            chkCarryFwd.Checked = true;
                        if (dsSale.Tables[0].Rows[0]["Disable_Mode"].ToString() == "1")
                            chkDisable.Checked = true;
                        if (dsSale.Tables[0].Rows[0]["Calc_Needed"].ToString() == "1")
                            chkCalc.Checked = true;
                        if (dsSale.Tables[0].Rows[0]["Calc_Disable"].ToString() == "1")
                            chkCalcDis.Checked = true;
                        if (dsSale.Tables[0].Rows[0]["Sale_Calc"].ToString() == "1")
                            chkCalcSale.Checked = true;
                        if (dsSale.Tables[0].Rows[0]["Carry_Fwd_Field"].ToString() == "1")
                            chkCarryFld.Checked = true;
                        if (dsSale.Tables[0].Rows[0]["Sub_Needed"].ToString() == "1")
                            chkSub.Checked = true;
                        if (dsSale.Tables[0].Rows[0]["Sub_Label"].ToString().Length > 0)
                            txtSub.Text = dsSale.Tables[0].Rows[0]["Sub_Label"].ToString().Trim();
                        if (dsSale.Tables[0].Rows[0]["Order_by"].ToString().Length > 0)
                            txtOrder.Text = dsSale.Tables[0].Rows[0]["Order_by"].ToString().Trim();
                    }
                }

                //if (chkSub.Checked == false)
                //    txtSub.Enabled = false;
            }

            
        }
        catch (Exception ex)
        {
            ErrorLog err = new ErrorLog();
            iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Setup", "grdSecSales_RowDataBound()");
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Unknown error exists. Please contact customer care for support');</script>");
        }
    }

    protected void grdSecSalesMinus_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblSaleCode = (Label)e.Row.FindControl("lblSaleCode");
                CheckBox chkDisplay = (CheckBox)e.Row.FindControl("chkDisplay");
                CheckBox chkValue = (CheckBox)e.Row.FindControl("chkValue");
                CheckBox chkCarryFwd = (CheckBox)e.Row.FindControl("chkCarryFwd");
                CheckBox chkDisable = (CheckBox)e.Row.FindControl("chkDisable");
                CheckBox chkCalc = (CheckBox)e.Row.FindControl("chkCalc");
                CheckBox chkCalcDis = (CheckBox)e.Row.FindControl("chkCalcDis");
                CheckBox chkCalcSale = (CheckBox)e.Row.FindControl("chkCalcSale");
                CheckBox chkCarryFld = (CheckBox)e.Row.FindControl("chkCarryFld");
                CheckBox chkSub = (CheckBox)e.Row.FindControl("chkSub");
                TextBox txtSub = (TextBox)e.Row.FindControl("txtSub");
                TextBox txtOrder = (TextBox)e.Row.FindControl("txtOrder");

                if ((lblSaleCode != null) && (lblSaleCode.Text.Trim().Length > 0))
                {
                    SecSale ss = new SecSale();
                    DataSet dsSale = new DataSet();
                    dsSale = ss.getSaleSetup(Convert.ToInt32(div_code), Convert.ToInt32(lblSaleCode.Text.Trim()));
                    if (dsSale.Tables[0].Rows.Count > 0)
                    {
                        if (dsSale.Tables[0].Rows[0]["Display_Needed"].ToString() == "1")
                            chkDisplay.Checked = true;
                        if (dsSale.Tables[0].Rows[0]["Value_Needed"].ToString() == "1")
                            chkValue.Checked = true;
                        if (dsSale.Tables[0].Rows[0]["Carry_Fwd_Needed"].ToString() == "1")
                            chkCarryFwd.Checked = true;
                        if (dsSale.Tables[0].Rows[0]["Disable_Mode"].ToString() == "1")
                            chkDisable.Checked = true;
                        if (dsSale.Tables[0].Rows[0]["Calc_Needed"].ToString() == "1")
                            chkCalc.Checked = true;
                        if (dsSale.Tables[0].Rows[0]["Calc_Disable"].ToString() == "1")
                            chkCalcDis.Checked = true;
                        if (dsSale.Tables[0].Rows[0]["Sale_Calc"].ToString() == "1")
                            chkCalcSale.Checked = true;
                        if (dsSale.Tables[0].Rows[0]["Carry_Fwd_Field"].ToString() == "1")
                            chkCarryFld.Checked = true;
                        if (dsSale.Tables[0].Rows[0]["Sub_Needed"].ToString() == "1")
                            chkSub.Checked = true;
                        if (dsSale.Tables[0].Rows[0]["Sub_Label"].ToString().Length > 0)
                            txtSub.Text = dsSale.Tables[0].Rows[0]["Sub_Label"].ToString().Trim();
                        if (dsSale.Tables[0].Rows[0]["Order_by"].ToString().Length > 0)
                            txtOrder.Text = dsSale.Tables[0].Rows[0]["Order_by"].ToString().Trim();
                    }
                }

                //if (chkSub.Checked == false)
                //    txtSub.Enabled = false;
            }


        }
        catch (Exception ex)
        {
            ErrorLog err = new ErrorLog();
            iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Setup", "grdSecSalesMinus_RowDataBound()");
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Unknown error exists. Please contact customer care for support');</script>");
        }
    }



    protected void grdSecSalesOthers_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblSaleCode = (Label)e.Row.FindControl("lblSaleCode");
                CheckBox chkDisplay = (CheckBox)e.Row.FindControl("chkDisplay");
                CheckBox chkValue = (CheckBox)e.Row.FindControl("chkValue");
                CheckBox chkCarryFwd = (CheckBox)e.Row.FindControl("chkCarryFwd");
                CheckBox chkDisable = (CheckBox)e.Row.FindControl("chkDisable");
                CheckBox chkCalc = (CheckBox)e.Row.FindControl("chkCalc");
                CheckBox chkCalcDis = (CheckBox)e.Row.FindControl("chkCalcDis");
                CheckBox chkCalcSale = (CheckBox)e.Row.FindControl("chkCalcSale");
                CheckBox chkCarryFld = (CheckBox)e.Row.FindControl("chkCarryFld");
                CheckBox chkSub = (CheckBox)e.Row.FindControl("chkSub");
                TextBox txtSub = (TextBox)e.Row.FindControl("txtSub");
                TextBox txtOrder = (TextBox)e.Row.FindControl("txtOrder");

                if ((lblSaleCode != null) && (lblSaleCode.Text.Trim().Length > 0))
                {
                    SecSale ss = new SecSale();
                    DataSet dsSale = new DataSet();
                    dsSale = ss.getSaleSetup(Convert.ToInt32(div_code), Convert.ToInt32(lblSaleCode.Text.Trim()));
                    if (dsSale.Tables[0].Rows.Count > 0)
                    {
                        if (dsSale.Tables[0].Rows[0]["Display_Needed"].ToString() == "1")
                            chkDisplay.Checked = true;
                        if (dsSale.Tables[0].Rows[0]["Value_Needed"].ToString() == "1")
                            chkValue.Checked = true;
                        if (dsSale.Tables[0].Rows[0]["Carry_Fwd_Needed"].ToString() == "1")
                            chkCarryFwd.Checked = true;
                        if (dsSale.Tables[0].Rows[0]["Disable_Mode"].ToString() == "1")
                            chkDisable.Checked = true;
                        if (dsSale.Tables[0].Rows[0]["Calc_Needed"].ToString() == "1")
                            chkCalc.Checked = true;
                        if (dsSale.Tables[0].Rows[0]["Calc_Disable"].ToString() == "1")
                            chkCalcDis.Checked = true;
                        if (dsSale.Tables[0].Rows[0]["Sale_Calc"].ToString() == "1")
                            chkCalcSale.Checked = true;
                        if (dsSale.Tables[0].Rows[0]["Carry_Fwd_Field"].ToString() == "1")
                            chkCarryFld.Checked = true;
                        if (dsSale.Tables[0].Rows[0]["Sub_Needed"].ToString() == "1")
                            chkSub.Checked = true;
                        if (dsSale.Tables[0].Rows[0]["Sub_Label"].ToString().Length > 0)
                            txtSub.Text = dsSale.Tables[0].Rows[0]["Sub_Label"].ToString().Trim();
                        if (dsSale.Tables[0].Rows[0]["Order_by"].ToString().Length > 0)
                            txtOrder.Text = dsSale.Tables[0].Rows[0]["Order_by"].ToString().Trim();
                    }
                }

                //if (chkSub.Checked == false)
                //    txtSub.Enabled = false;
            }


        }
        catch (Exception ex)
        {
            ErrorLog err = new ErrorLog();
            iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Setup", "grdSecSalesOthers_RowDataBound()");
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Unknown error exists. Please contact customer care for support');</script>");
        }
    }


    protected void grdCol_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblSaleCode = (Label)e.Row.FindControl("lblSaleCode");
                CheckBox chkDisplay = (CheckBox)e.Row.FindControl("chkDisplay");
                CheckBox chkValue = (CheckBox)e.Row.FindControl("chkValue");
                CheckBox chkCarryFwd = (CheckBox)e.Row.FindControl("chkCarryFwd");
                CheckBox chkDisable = (CheckBox)e.Row.FindControl("chkDisable");
                CheckBox chkCalc = (CheckBox)e.Row.FindControl("chkCalc");
                CheckBox chkCalcDis = (CheckBox)e.Row.FindControl("chkCalcDis");
                CheckBox chkCalcSale = (CheckBox)e.Row.FindControl("chkCalcSale");
                CheckBox chkCarryFld = (CheckBox)e.Row.FindControl("chkCarryFld");
                CheckBox chkSub = (CheckBox)e.Row.FindControl("chkSub");
                TextBox txtSub = (TextBox)e.Row.FindControl("txtSub");
                TextBox txtOrder = (TextBox)e.Row.FindControl("txtOrder");

                if ((lblSaleCode != null) && (lblSaleCode.Text.Trim().Length > 0))
                {
                    SecSale ss = new SecSale();
                    DataSet dsSale = new DataSet();
                    dsSale = ss.getSaleSetup(Convert.ToInt32(div_code), Convert.ToInt32(lblSaleCode.Text.Trim()));
                    if (dsSale.Tables[0].Rows.Count > 0)
                    {
                        if (dsSale.Tables[0].Rows[0]["Display_Needed"].ToString() == "1")
                            chkDisplay.Checked = true;
                        if (dsSale.Tables[0].Rows[0]["Value_Needed"].ToString() == "1")
                            chkValue.Checked = true;
                        if (dsSale.Tables[0].Rows[0]["Carry_Fwd_Needed"].ToString() == "1")
                            chkCarryFwd.Checked = true;
                        if (dsSale.Tables[0].Rows[0]["Disable_Mode"].ToString() == "1")
                            chkDisable.Checked = true;
                        if (dsSale.Tables[0].Rows[0]["Calc_Needed"].ToString() == "1")
                            chkCalc.Checked = true;
                        if (dsSale.Tables[0].Rows[0]["Calc_Disable"].ToString() == "1")
                            chkCalcDis.Checked = true;
                        if (dsSale.Tables[0].Rows[0]["Sale_Calc"].ToString() == "1")
                            chkCalcSale.Checked = true;
                        if (dsSale.Tables[0].Rows[0]["Carry_Fwd_Field"].ToString() == "1")
                            chkCarryFld.Checked = true;
                        if (dsSale.Tables[0].Rows[0]["Sub_Needed"].ToString() == "1")
                            chkSub.Checked = true;
                        if (dsSale.Tables[0].Rows[0]["Sub_Label"].ToString().Length > 0)
                            txtSub.Text = dsSale.Tables[0].Rows[0]["Sub_Label"].ToString().Trim();
                        if (dsSale.Tables[0].Rows[0]["Order_by"].ToString().Length > 0)
                            txtOrder.Text = dsSale.Tables[0].Rows[0]["Order_by"].ToString().Trim();
                    }
                }

                //if (chkSub.Checked == false)
                //    txtSub.Enabled = false;
            }


        }
        catch (Exception ex)
        {
            ErrorLog err = new ErrorLog();
            iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Setup", "grdCol_RowDataBound()");
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Unknown error exists. Please contact customer care for support');</script>");
        }
    }
    
    protected void btnClear_Click(object sender, EventArgs e)
    {
        try
        {
            foreach (GridViewRow gridRow in grdSecSales.Rows)
            {
                CheckBox chkDisplay = (CheckBox)gridRow.FindControl("chkDisplay");
                CheckBox chkValue = (CheckBox)gridRow.FindControl("chkValue");
                CheckBox chkCarryFwd = (CheckBox)gridRow.FindControl("chkCarryFwd");
                CheckBox chkDisable = (CheckBox)gridRow.FindControl("chkDisable");
                CheckBox chkCalc = (CheckBox)gridRow.FindControl("chkCalc");
                CheckBox chkCalcDis = (CheckBox)gridRow.FindControl("chkCalcDis");
                CheckBox chkCalcSale = (CheckBox)gridRow.FindControl("chkCalcSale");
                CheckBox chkCarryFld = (CheckBox)gridRow.FindControl("chkCarryFld");
                CheckBox chkSub = (CheckBox)gridRow.FindControl("chkSub");
                TextBox txtSub = (TextBox)gridRow.FindControl("txtSub");
                TextBox txtOrder = (TextBox)gridRow.FindControl("txtOrder");
                chkDisplay.Checked = false;
                chkValue.Checked = false;
                chkCarryFwd.Checked = false;
                chkDisable.Checked = false;
                chkCalc.Checked = false;
                chkCalcDis.Checked = false;
                chkCalcSale.Checked = false;
                chkCarryFld.Checked = false;
                chkSub.Checked = false;
                txtSub.Text = "";
                txtOrder.Text = "";
            }
            foreach (GridViewRow gridRow in grdSecSalesMinus.Rows)
            {
                CheckBox chkDisplay = (CheckBox)gridRow.FindControl("chkDisplay");
                CheckBox chkValue = (CheckBox)gridRow.FindControl("chkValue");
                CheckBox chkCarryFwd = (CheckBox)gridRow.FindControl("chkCarryFwd");
                CheckBox chkDisable = (CheckBox)gridRow.FindControl("chkDisable");
                CheckBox chkCalc = (CheckBox)gridRow.FindControl("chkCalc");
                CheckBox chkCalcDis = (CheckBox)gridRow.FindControl("chkCalcDis");
                CheckBox chkCalcSale = (CheckBox)gridRow.FindControl("chkCalcSale");
                CheckBox chkCarryFld = (CheckBox)gridRow.FindControl("chkCarryFld");
                CheckBox chkSub = (CheckBox)gridRow.FindControl("chkSub");
                TextBox txtSub = (TextBox)gridRow.FindControl("txtSub");
                TextBox txtOrder = (TextBox)gridRow.FindControl("txtOrder");
                chkDisplay.Checked = false;
                chkValue.Checked = false;
                chkCarryFwd.Checked = false;
                chkDisable.Checked = false;
                chkCalc.Checked = false;
                chkCalcDis.Checked = false;
                chkCalcSale.Checked = false;
                chkCarryFld.Checked = false;
                chkSub.Checked = false;
                txtSub.Text = "";
                txtOrder.Text = "";
            }
            foreach (GridViewRow gridRow in grdSecSalesOthers.Rows)
            {
                CheckBox chkDisplay = (CheckBox)gridRow.FindControl("chkDisplay");
                CheckBox chkValue = (CheckBox)gridRow.FindControl("chkValue");
                CheckBox chkCarryFwd = (CheckBox)gridRow.FindControl("chkCarryFwd");
                CheckBox chkDisable = (CheckBox)gridRow.FindControl("chkDisable");
                CheckBox chkCalc = (CheckBox)gridRow.FindControl("chkCalc");
                CheckBox chkCalcDis = (CheckBox)gridRow.FindControl("chkCalcDis");
                CheckBox chkCalcSale = (CheckBox)gridRow.FindControl("chkCalcSale");
                CheckBox chkCarryFld = (CheckBox)gridRow.FindControl("chkCarryFld");
                CheckBox chkSub = (CheckBox)gridRow.FindControl("chkSub");
                TextBox txtSub = (TextBox)gridRow.FindControl("txtSub");
                TextBox txtOrder = (TextBox)gridRow.FindControl("txtOrder");
                chkDisplay.Checked = false;
                chkValue.Checked = false;
                chkCarryFwd.Checked = false;
                chkDisable.Checked = false;
                chkCalc.Checked = false;
                chkCalcDis.Checked = false;
                chkCalcSale.Checked = false;
                chkCarryFld.Checked = false;
                chkSub.Checked = false;
                txtSub.Text = "";
                txtOrder.Text = "";
            }

            foreach (GridViewRow gridRow in grdCol.Rows)
            {
                CheckBox chkDisplay = (CheckBox)gridRow.FindControl("chkDisplay");
                CheckBox chkValue = (CheckBox)gridRow.FindControl("chkValue");
                CheckBox chkCarryFwd = (CheckBox)gridRow.FindControl("chkCarryFwd");
                CheckBox chkDisable = (CheckBox)gridRow.FindControl("chkDisable");
                CheckBox chkCalc = (CheckBox)gridRow.FindControl("chkCalc");
                CheckBox chkCalcDis = (CheckBox)gridRow.FindControl("chkCalcDis");
                CheckBox chkCalcSale = (CheckBox)gridRow.FindControl("chkCalcSale");
                CheckBox chkCarryFld = (CheckBox)gridRow.FindControl("chkCarryFld");
                CheckBox chkSub = (CheckBox)gridRow.FindControl("chkSub");
                TextBox txtSub = (TextBox)gridRow.FindControl("txtSub");
                TextBox txtOrder = (TextBox)gridRow.FindControl("txtOrder");
                chkDisplay.Checked = false;
                chkValue.Checked = false;
                chkCarryFwd.Checked = false;
                chkDisable.Checked = false;
                chkCalc.Checked = false;
                chkCalcDis.Checked = false;
                chkCalcSale.Checked = false;
                chkCarryFld.Checked = false;
                chkSub.Checked = false;
                txtSub.Text = "";
                txtOrder.Text = "";
            }
        }
        catch (Exception ex)
        {
            ErrorLog err = new ErrorLog();
            iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Setup", "btnClear_Click()");
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Unknown error exists. Please contact customer care for support');</script>");
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        bool bRecordUpdated = false;
        int iSecSaleCode = 0;
        int iDisplay = 0;
        int iValue = 0;
        int iCarryFwd = 0;
        int iDisable = 0;
        int iCalc = 0;
        int iCalcDis = 0;
        int iCalcSale = 0;
        int iCarryFld = 0;
        int iSub = 0;
        string sSubLabel = string.Empty;
        int iOrder = 0;
        int iRet = -1;
        bool bRecordExist = false;
        string sOrder = string.Empty;

        try
        {
            foreach (GridViewRow gridRow in grdSecSales.Rows)
            {
                iSecSaleCode = 0;
                iDisplay = 0;
                iValue = 0;
                iCarryFwd = 0;
                iDisable = 0;
                iCalc = 0;
                iCalcDis = 0;
                iCalcSale = 0;
                iCarryFld = 0;
                iSub = 0;
                iOrder = 0;
                sSubLabel = "";

                Label lblSaleCode = (Label)gridRow.FindControl("lblSaleCode");
                CheckBox chkDisplay = (CheckBox)gridRow.FindControl("chkDisplay");
                CheckBox chkValue = (CheckBox)gridRow.FindControl("chkValue");
                CheckBox chkCarryFwd = (CheckBox)gridRow.FindControl("chkCarryFwd");
                CheckBox chkDisable = (CheckBox)gridRow.FindControl("chkDisable");
                CheckBox chkCalc = (CheckBox)gridRow.FindControl("chkCalc");
                CheckBox chkCalcDis = (CheckBox)gridRow.FindControl("chkCalcDis");
                CheckBox chkCalcSale = (CheckBox)gridRow.FindControl("chkCalcSale");
                CheckBox chkCarryFld = (CheckBox)gridRow.FindControl("chkCarryFld");
                CheckBox chkSub = (CheckBox)gridRow.FindControl("chkSub");
                TextBox txtSub = (TextBox)gridRow.FindControl("txtSub");
                TextBox txtOrder = (TextBox)gridRow.FindControl("txtOrder");

                if (chkDisplay.Checked)
                    iDisplay = 1;
                if (chkValue.Checked)
                    iValue = 1;
                if (chkCarryFwd.Checked)
                    iCarryFwd = 1;
                if (chkDisable.Checked)
                    iDisable = 1;
                if (chkCalc.Checked)
                    iCalc = 1;
                if (chkCalcDis.Checked)
                    iCalcDis = 1;
                if (chkCalcSale.Checked)
                    iCalcSale = 1;
                if (chkCarryFld.Checked)
                    iCarryFld = 1;
                if (chkSub.Checked)
                    iSub = 1;

                if (txtSub.Text.Trim().Length > 0)
                    sSubLabel = txtSub.Text.Trim();

                if (txtOrder.Text.Trim().Length > 0)
                    iOrder = Convert.ToInt32(txtOrder.Text.Trim());

                if (sOrder.IndexOf(iOrder.ToString()) == -1)
                {
                    iSecSaleCode = Convert.ToInt32(lblSaleCode.Text.Trim());

                    SecSale ss = new SecSale();

                    // Checks for Setup exists for this division. If so, then the setup records will be updated, Else, it will be created
                    bRecordExist = ss.sRecordExist(div_code.Trim(), iSecSaleCode);

                    iRet = ss.RecordAdd(Convert.ToInt32(div_code), iSecSaleCode, iDisplay, iValue, iCarryFwd, iDisable, iCalc, iCalcDis, iCalcSale, iCarryFld, iOrder, bRecordExist, iSub, sSubLabel);
                    if (iRet > 0)
                        bRecordUpdated = true;
                    sOrder += iOrder.ToString() + ",";
                }
                else
                {
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Duplicate Order ID exists');</script>");
                    break;
                }
            }


            //Minus
            sOrder = "";
            foreach (GridViewRow gridRow in grdSecSalesMinus.Rows)
            {
                iSecSaleCode = 0;
                iDisplay = 0;
                iValue = 0;
                iCarryFwd = 0;
                iDisable = 0;
                iCalc = 0;
                iCalcDis = 0;
                iCalcSale = 0;
                iCarryFld = 0;
                iSub = 0;
                iOrder = 0;
                sSubLabel = "";

                Label lblSaleCode = (Label)gridRow.FindControl("lblSaleCode");
                CheckBox chkDisplay = (CheckBox)gridRow.FindControl("chkDisplay");
                CheckBox chkValue = (CheckBox)gridRow.FindControl("chkValue");
                CheckBox chkCarryFwd = (CheckBox)gridRow.FindControl("chkCarryFwd");
                CheckBox chkDisable = (CheckBox)gridRow.FindControl("chkDisable");
                CheckBox chkCalc = (CheckBox)gridRow.FindControl("chkCalc");
                CheckBox chkCalcDis = (CheckBox)gridRow.FindControl("chkCalcDis");
                CheckBox chkCalcSale = (CheckBox)gridRow.FindControl("chkCalcSale");
                CheckBox chkCarryFld = (CheckBox)gridRow.FindControl("chkCarryFld");
                CheckBox chkSub = (CheckBox)gridRow.FindControl("chkSub");
                TextBox txtSub = (TextBox)gridRow.FindControl("txtSub");
                TextBox txtOrder = (TextBox)gridRow.FindControl("txtOrder");

                if (chkDisplay.Checked)
                    iDisplay = 1;
                if (chkValue.Checked)
                    iValue = 1;
                if (chkCarryFwd.Checked)
                    iCarryFwd = 1;
                if (chkDisable.Checked)
                    iDisable = 1;
                if (chkCalc.Checked)
                    iCalc = 1;
                if (chkCalcDis.Checked)
                    iCalcDis = 1;
                if (chkCalcSale.Checked)
                    iCalcSale = 1;
                if (chkCarryFld.Checked)
                    iCarryFld = 1;
                if (chkSub.Checked)
                    iSub = 1;

                if (txtSub.Text.Trim().Length > 0)
                    sSubLabel = txtSub.Text.Trim();

                if (txtOrder.Text.Trim().Length > 0)
                    iOrder = Convert.ToInt32(txtOrder.Text.Trim());

                if (sOrder.IndexOf(iOrder.ToString()) == -1)
                {
                    iSecSaleCode = Convert.ToInt32(lblSaleCode.Text.Trim());

                    SecSale ss = new SecSale();

                    // Checks for Setup exists for this division. If so, then the setup records will be updated, Else, it will be created
                    bRecordExist = ss.sRecordExist(div_code.Trim(), iSecSaleCode);

                    iRet = ss.RecordAdd(Convert.ToInt32(div_code), iSecSaleCode, iDisplay, iValue, iCarryFwd, iDisable, iCalc, iCalcDis, iCalcSale, iCarryFld, iOrder, bRecordExist, iSub, sSubLabel);
                    if (iRet > 0)
                        bRecordUpdated = true;
                    sOrder += iOrder.ToString() + ",";
                }
                else
                {
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Duplicate Order ID exists');</script>");
                    break;
                }
            }


            // Closing Balance
            sOrder = "";
            foreach (GridViewRow gridRow in grdSecSalesOthers.Rows)
            {
                iSecSaleCode = 0;
                iDisplay = 0;
                iValue = 0;
                iCarryFwd = 0;
                iDisable = 0;
                iCalc = 0;
                iCalcDis = 0;
                iCalcSale = 0;
                iCarryFld = 0;
                iSub = 0;
                iOrder = 0;
                sSubLabel = "";

                Label lblSaleCode = (Label)gridRow.FindControl("lblSaleCode");
                CheckBox chkDisplay = (CheckBox)gridRow.FindControl("chkDisplay");
                CheckBox chkValue = (CheckBox)gridRow.FindControl("chkValue");
                CheckBox chkCarryFwd = (CheckBox)gridRow.FindControl("chkCarryFwd");
                CheckBox chkDisable = (CheckBox)gridRow.FindControl("chkDisable");
                CheckBox chkCalc = (CheckBox)gridRow.FindControl("chkCalc");
                CheckBox chkCalcDis = (CheckBox)gridRow.FindControl("chkCalcDis");
                CheckBox chkCalcSale = (CheckBox)gridRow.FindControl("chkCalcSale");
                CheckBox chkCarryFld = (CheckBox)gridRow.FindControl("chkCarryFld");
                CheckBox chkSub = (CheckBox)gridRow.FindControl("chkSub");
                TextBox txtSub = (TextBox)gridRow.FindControl("txtSub");
                TextBox txtOrder = (TextBox)gridRow.FindControl("txtOrder");

                if (chkDisplay.Checked)
                    iDisplay = 1;
                if (chkValue.Checked)
                    iValue = 1;
                if (chkCarryFwd.Checked)
                    iCarryFwd = 1;
                if (chkDisable.Checked)
                    iDisable = 1;
                if (chkCalc.Checked)
                    iCalc = 1;
                if (chkCalcDis.Checked)
                    iCalcDis = 1;
                if (chkCalcSale.Checked)
                    iCalcSale = 1;
                if (chkCarryFld.Checked)
                    iCarryFld = 1;
                if (chkSub.Checked)
                    iSub = 1;

                if (txtSub.Text.Trim().Length > 0)
                    sSubLabel = txtSub.Text.Trim();

                if (txtOrder.Text.Trim().Length > 0)
                    iOrder = Convert.ToInt32(txtOrder.Text.Trim());

                if (sOrder.IndexOf(iOrder.ToString()) == -1)
                {
                    iSecSaleCode = Convert.ToInt32(lblSaleCode.Text.Trim());

                    SecSale ss = new SecSale();

                    // Checks for Setup exists for this division. If so, then the setup records will be updated, Else, it will be created
                    bRecordExist = ss.sRecordExist(div_code.Trim(), iSecSaleCode);

                    iRet = ss.RecordAdd(Convert.ToInt32(div_code), iSecSaleCode, iDisplay, iValue, iCarryFwd, iDisable, iCalc, iCalcDis, iCalcSale, iCarryFld, iOrder, bRecordExist, iSub, sSubLabel);
                    if (iRet > 0)
                        bRecordUpdated = true;
                    sOrder += iOrder.ToString() + ",";
                }
                else
                {
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Duplicate Order ID exists');</script>");
                    break;
                }
            }

            // Closing Balance
            sOrder = "";
            foreach (GridViewRow gridRow in grdCol.Rows)
            {
                iSecSaleCode = 0;
                iDisplay = 0;
                iValue = 0;
                iCarryFwd = 0;
                iDisable = 0;
                iCalc = 0;
                iCalcDis = 0;
                iCalcSale = 0;
                iCarryFld = 0;
                iSub = 0;
                iOrder = 0;
                sSubLabel = "";

                Label lblSaleCode = (Label)gridRow.FindControl("lblSaleCode");
                CheckBox chkDisplay = (CheckBox)gridRow.FindControl("chkDisplay");
                CheckBox chkValue = (CheckBox)gridRow.FindControl("chkValue");
                CheckBox chkCarryFwd = (CheckBox)gridRow.FindControl("chkCarryFwd");
                CheckBox chkDisable = (CheckBox)gridRow.FindControl("chkDisable");
                CheckBox chkCalc = (CheckBox)gridRow.FindControl("chkCalc");
                CheckBox chkCalcDis = (CheckBox)gridRow.FindControl("chkCalcDis");
                CheckBox chkCalcSale = (CheckBox)gridRow.FindControl("chkCalcSale");
                CheckBox chkCarryFld = (CheckBox)gridRow.FindControl("chkCarryFld");
                CheckBox chkSub = (CheckBox)gridRow.FindControl("chkSub");
                TextBox txtSub = (TextBox)gridRow.FindControl("txtSub");
                TextBox txtOrder = (TextBox)gridRow.FindControl("txtOrder");

                if (chkDisplay.Checked)
                    iDisplay = 1;
                if (chkValue.Checked)
                    iValue = 1;
                if (chkCarryFwd.Checked)
                    iCarryFwd = 1;
                if (chkDisable.Checked)
                    iDisable = 1;
                if (chkCalc.Checked)
                    iCalc = 1;
                if (chkCalcDis.Checked)
                    iCalcDis = 1;
                if (chkCalcSale.Checked)
                    iCalcSale = 1;
                if (chkCarryFld.Checked)
                    iCarryFld = 1;
                if (chkSub.Checked)
                    iSub = 1;

                if (txtSub.Text.Trim().Length > 0)
                    sSubLabel = txtSub.Text.Trim();

                if (txtOrder.Text.Trim().Length > 0)
                    iOrder = Convert.ToInt32(txtOrder.Text.Trim());

                if (sOrder.IndexOf(iOrder.ToString()) == -1)
                {
                    iSecSaleCode = Convert.ToInt32(lblSaleCode.Text.Trim());

                    SecSale ss = new SecSale();

                    // Checks for Setup exists for this division. If so, then the setup records will be updated, Else, it will be created
                    bRecordExist = ss.sRecordExist(div_code.Trim(), iSecSaleCode);

                    iRet = ss.RecordAdd(Convert.ToInt32(div_code), iSecSaleCode, iDisplay, iValue, iCarryFwd, iDisable, iCalc, iCalcDis, iCalcSale, iCarryFld, iOrder, bRecordExist, iSub, sSubLabel);
                    if (iRet > 0)
                        bRecordUpdated = true;
                    sOrder += iOrder.ToString() + ",";
                }
                else
                {
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Duplicate Order ID exists');</script>");
                    break;
                }
            }

            if (bRecordUpdated)
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Secondary Sale Setup has been updated Successfully');</script>");
        }
        catch (Exception ex)
        {
            ErrorLog err = new ErrorLog();
            iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Setup", "btnSubmit_Click()");
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Unknown error exists. Please contact customer care for support');</script>");                        
        }
    }

    protected void grdSecSales_RowEditing(object sender, GridViewEditEventArgs e)
    {
        grdSecSales.EditIndex = e.NewEditIndex;
        FillSecSales();
        TextBox ctrl = (TextBox)grdSecSales.Rows[e.NewEditIndex].Cells[2].FindControl("txtSaleName");
        ctrl.Focus();
    }

    protected void grdSecSales_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        grdSecSales.EditIndex = -1;
        FillSecSales();
    }

    protected void grdSecSales_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        grdSecSales.EditIndex = -1;
        Label lblSaleCode = (Label)grdSecSales.Rows[e.RowIndex].Cells[1].FindControl("lblSaleCode");
        TextBox txtSaleName = (TextBox)grdSecSales.Rows[e.RowIndex].Cells[2].FindControl("txtSaleName");
        SecSale ss = new SecSale();
        int iRet = ss.ParamRecordUpdate(Session["div_code"].ToString(), txtSaleName.Text.Trim(), lblSaleCode.Text.Trim());
        FillSecSales();
    }


    protected void grdSecSalesMinus_RowEditing(object sender, GridViewEditEventArgs e)
    {
        grdSecSalesMinus.EditIndex = e.NewEditIndex;
        FillSecSales();
        TextBox ctrl = (TextBox)grdSecSalesMinus.Rows[e.NewEditIndex].Cells[2].FindControl("txtSaleName");
        ctrl.Focus();
    }

    protected void grdSecSalesMinus_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        grdSecSalesMinus.EditIndex = -1;
        FillSecSales();
    }

    protected void grdSecSalesMinus_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        grdSecSalesMinus.EditIndex = -1;
        Label lblSaleCode = (Label)grdSecSalesMinus.Rows[e.RowIndex].Cells[1].FindControl("lblSaleCode");
        TextBox txtSaleName = (TextBox)grdSecSalesMinus.Rows[e.RowIndex].Cells[2].FindControl("txtSaleName");
        SecSale ss = new SecSale();
        int iRet = ss.ParamRecordUpdate(Session["div_code"].ToString(), txtSaleName.Text.Trim(), lblSaleCode.Text.Trim());
        FillSecSales();
    }

    protected void grdSecSalesOthers_RowEditing(object sender, GridViewEditEventArgs e)
    {
        grdSecSalesOthers.EditIndex = e.NewEditIndex;
        FillSecSales();
        TextBox ctrl = (TextBox)grdSecSalesOthers.Rows[e.NewEditIndex].Cells[2].FindControl("txtSaleName");
        ctrl.Focus();
    }

    protected void grdSecSalesOthers_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        grdSecSalesOthers.EditIndex = -1;
        FillSecSales();
    }

    protected void grdSecSalesOthers_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        grdSecSalesOthers.EditIndex = -1;
        Label lblSaleCode = (Label)grdSecSalesOthers.Rows[e.RowIndex].Cells[1].FindControl("lblSaleCode");
        TextBox txtSaleName = (TextBox)grdSecSalesOthers.Rows[e.RowIndex].Cells[2].FindControl("txtSaleName");
        SecSale ss = new SecSale();
        int iRet = ss.ParamRecordUpdate(Session["div_code"].ToString(), txtSaleName.Text.Trim(), lblSaleCode.Text.Trim());
        FillSecSales();
    }

    protected void grdCol_RowEditing(object sender, GridViewEditEventArgs e)
    {
        grdCol.EditIndex = e.NewEditIndex;
        FillSecSales();
        TextBox ctrl = (TextBox)grdSecSalesOthers.Rows[e.NewEditIndex].Cells[2].FindControl("txtSaleName");
        ctrl.Focus();
    }

    protected void grdCol_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        grdCol.EditIndex = -1;
        FillSecSales();
    }

    protected void grdCol_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        grdCol.EditIndex = -1;
        Label lblSaleCode = (Label)grdCol.Rows[e.RowIndex].Cells[1].FindControl("lblSaleCode");
        TextBox txtSaleName = (TextBox)grdCol.Rows[e.RowIndex].Cells[2].FindControl("txtSaleName");
        SecSale ss = new SecSale();
        int iRet = ss.ParamRecordUpdate(Session["div_code"].ToString(), txtSaleName.Text.Trim(), lblSaleCode.Text.Trim());
        FillSecSales();
    }

    protected void btnAddParam_Click(object sender, EventArgs e)
    {
        bool bIsValid = true;
        int iReturn = -1;

        //if (txtParamName.Text.Trim().Length == 0)
        //{
        //    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Param Name should not be empty');</script>");
        //    bIsValid = false;
        //}
        //else if (txtParamName.Text.Trim().Length > 50)
        //{
        //    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Param Name should not exceed 50 characters');</script>");
        //    bIsValid = false;
        //}

        //if (txtShortName.Text.Trim().Length == 0)
        //{
        //    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Short Name should not be empty');</script>");
        //    bIsValid = false;
        //}
        //else if (txtShortName.Text.Trim().Length > 50)
        //{
        //    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Short Name should not exceed 30 characters');</script>");
        //    bIsValid = false;
        //}

        if (bIsValid)
        {
            SecSale ss = new SecSale();
            bIsValid = ss.sParamRecordExist(div_code, txtParamName.Text.Trim());
            if (bIsValid)
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Param already exists');</script>");
            }
            else
            {
                iReturn = ss.ParamRecordAdd(Session["div_code"].ToString(), txtParamName.Text.Trim(), txtShortName.Text.Trim(), ddlType.SelectedValue.ToString());

                if (iReturn > 0)
                {
                    txtParamName.Text = "";
                    txtShortName.Text = "";
                    ddlType.SelectedIndex = 0;
                    FillSecSales();
                }
            }
        }
    }
}