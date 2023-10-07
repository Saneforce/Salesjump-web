using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;

public partial class SecondarySales_CustomizedColumn : System.Web.UI.Page
{
    #region "Variable Declarations"
    DataSet dsSale = null;
    string div_code = string.Empty;
    string strError = string.Empty;
    string sFormula = string.Empty;
    string sFormula_Det = string.Empty;
    string SecSale_ShortName = string.Empty;
    bool orderbyExists = false;
    bool bIsValid = false;
    int iErrReturn = -1;
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        if (!Page.IsPostBack)
        {
            menu1.Title = this.Page.Title;
            menu1.FindControl("btnBack").Visible = false;
            FillSecSales();
            FillSecSales_Formula();
        }
    }

    private void FillSecSales()
    {
        try
        {
            SecSale ss = new SecSale();
            dsSale = ss.getSaleMaster(div_code, true);
            if (dsSale.Tables[0].Rows.Count > 0)
            {
                ddlParam.DataValueField = "Sec_Sale_Code";
                ddlParam.DataTextField = "Sec_Sale_Short_Name";
                ddlParam.DataSource = dsSale;
                ddlParam.DataBind();
            }
        }
        catch (Exception ex)
        {
            ErrorLog err = new ErrorLog();
            iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Setup", "FillSecSales()");
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Unknown error exists. Please contact customer care for support');</script>");
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        int iRet = -1;
        int i = 0;
        //int sec_sale_code = -1;

        string col_name = string.Empty;
        string dis_mode = string.Empty;
        string order_by = string.Empty;
        string der_formula = string.Empty;

        col_name = txtColName.Text.Trim();        
        dis_mode = rdoDisable.SelectedValue.ToString();
        order_by = "0"; // txtOrderBy.Text.Trim();
        der_formula = hidFormula.Value; //litFormula.Text.Trim();

        bIsValid = true;

        if (col_name.Length == 0)
        {
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Enter Column Name');</script>");
            bIsValid = false;
        }

        if (dis_mode.Length == 0)
        {
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Select option for Disable Mode');</script>");
            bIsValid = false;
        }

        //if (order_by.Length == 0)
        //{
        //    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Enter Order By');</script>");
        //    bIsValid = false;
        //}

        SecSale ss = new SecSale();
        //orderbyExists = ss.OrderByExists(div_code, order_by.Trim());
        //if (orderbyExists)
        //{
        //    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('This Order By is already used in Transaction');</script>");
        //    bIsValid = false;
        //}
        //else
        //{
        //    orderbyExists = ss.OrderByExists_Formula(div_code, order_by.Trim());
        //    if (orderbyExists)
        //    {
        //        ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('This Order By is already used in this setup screen');</script>");
        //        bIsValid = false;
        //    }
        //}

        if (bIsValid)
        {
            bool bRecordExist = ss.FormulaRecordExist(div_code.Trim(), col_name);
            if (bRecordExist == false)
            {
                if (hid_Col_SNo.Value.Trim().Length > 0)
                    iRet = ss.Formula_RecordUpdate(div_code, hid_Col_SNo.Value.Trim(), col_name, dis_mode, order_by, der_formula);
                else
                    iRet = ss.Formula_RecordAdd(div_code, col_name, dis_mode, order_by, der_formula, bRecordExist);
            }
            else
                iRet = ss.Formula_RecordUpdate(div_code, hid_Col_SNo.Value.Trim(), col_name, dis_mode, order_by, der_formula);

            if (iRet > 0)
            {
                if (bRecordExist)
                {
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Secondary Sale Formula Setup has been updated Successfully');</script>");
                    btnSubmit.Text = "Submit";
                    hid_Col_SNo.Value = "";
                }
                else
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Secondary Sale Formula Setup has been created Successfully');</script>");


                hidFormula.Value = "";
                ClearAll();
                FillSecSales_Formula();
            }
        }

    }

    protected void grdSecSales_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblFormula_Edit = (Label)e.Row.FindControl("lblFormula_Edit");
                sFormula = lblFormula_Edit.Text.Trim();
                lblFormula_Edit.Text = Fill_Formula(sFormula); 
                //string[] strFormulaSplit = sFormula.Split(' ');
                //SecSale ss = new SecSale();
                //sFormula_Det = "";
                //foreach (string strForm in strFormulaSplit)
                //{
                //    if (strForm != "")
                //    {
                //        //if (strForm.Trim() != "+") //&& (strForm.Trim() != "-"))
                //        if (strForm.Trim() != "+")
                //        {
                //            if (strForm.Trim() != "-")
                //            {
                //                dsSale = ss.getSaleMaster_Det(div_code, Convert.ToInt16(strForm));
                //                if (dsSale != null)
                //                {
                //                    sFormula_Det = sFormula_Det + dsSale.Tables[0].Rows[0].ItemArray.GetValue(1).ToString() + " ";
                //                }
                //            }
                //            else
                //            {
                //                sFormula_Det = sFormula_Det + strForm.Trim() + " ";
                //            }
                //        }
                //        else
                //        {
                //            sFormula_Det = sFormula_Det + strForm.Trim() + " ";
                //        }
                //    }
                //}
                //lblFormula_Edit.Text = sFormula_Det;
            }
        }
        catch (Exception ex)
        {
            ErrorLog err = new ErrorLog();
            iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Setup", "grdSecSales_RowDataBound()");
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Unknown error exists. Please contact customer care for support');</script>");
        }
    }

    private string Fill_Formula(string sFormula)
    {
        string[] strFormulaSplit = sFormula.Split(' ');
        SecSale ss = new SecSale();
        sFormula_Det = "";
        foreach (string strForm in strFormulaSplit)
        {
            if (strForm != "")
            {
                //if (strForm.Trim() != "+") //&& (strForm.Trim() != "-"))
                if (strForm.Trim() != "+")
                {
                    if (strForm.Trim() != "-")
                    {
                        dsSale = ss.getSaleMaster_Det(div_code, Convert.ToInt16(strForm));
                        if (dsSale != null)
                        {
                            sFormula_Det = sFormula_Det + dsSale.Tables[0].Rows[0].ItemArray.GetValue(1).ToString() + " ";
                        }
                    }
                    else
                    {
                        sFormula_Det = sFormula_Det + strForm.Trim() + " ";
                    }
                }
                else
                {
                    sFormula_Det = sFormula_Det + strForm.Trim() + " ";
                }
            }
        }

        return sFormula_Det; 
        //lblFormula_Edit.Text = sFormula_Det;
    }

    protected void grdSecSales_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Edit")
        {
            string sformula = "";
            int col_sno = Convert.ToInt16(e.CommandArgument);
            SecSale ss = new SecSale();
            dsSale = ss.getSaleMaster_Formula(div_code, col_sno);
            if (dsSale != null)
            {
                if (dsSale.Tables[0].Rows.Count > 0)
                {
                    txtColName.Text = dsSale.Tables[0].Rows[0]["Col_Name"].ToString();
                    rdoDisable.SelectedValue = dsSale.Tables[0].Rows[0]["Dis_Mode"].ToString();
                    //txtOrderBy.Text = dsSale.Tables[0].Rows[0]["Order_By"].ToString();
                    sformula = dsSale.Tables[0].Rows[0]["Der_Formula"].ToString();
                    litFormula.Text = Fill_Formula(dsSale.Tables[0].Rows[0]["Der_Formula"].ToString());
                    hidFormula.Value = sformula; // dsSale.Tables[0].Rows[0]["Der_Formula"].ToString();
                    btnSubmit.Text = "Update";
                    hid_Col_SNo.Value = col_sno.ToString();
                }
            }
        }
        else if (e.CommandName == "Delete")
        {
            int col_sno = Convert.ToInt16(e.CommandArgument);
            string entry_done = string.Empty;
            SecSale ss = new SecSale();
            dsSale = ss.getSaleMaster_Formula(div_code, col_sno);
            if (dsSale != null)
            {
                if (dsSale.Tables[0].Rows.Count > 0)
                    entry_done  = dsSale.Tables[0].Rows[0]["SS_Entry_Done"].ToString();

                if (entry_done == "1")
                {
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Cant delete as this column is already used in transaction');</script>");
                    ViewState["entry_done"] = "1";
                }
            }
        }
    }

    protected void grdSecSales_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        bool bDelete = false;

        if (ViewState["entry_done"] != null)
        {
            if (ViewState["entry_done"].ToString() == "1")
                bDelete = false;
            else
                bDelete = true;
        }
        else
            bDelete = true;

        if (bDelete)
        {
            Label lblColSNo_Edit = (Label)grdSecSales.Rows[e.RowIndex].Cells[1].FindControl("lblColSNo_Edit");
            int col_sno = Convert.ToInt16(lblColSNo_Edit.Text);
            SecSale ss = new SecSale();
            int ret = ss.Formula_RecordDelete(div_code, col_sno.ToString());
            if (ret > 0)
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Secondary Sale Formula Setup has been deleted Successfully');</script>");
                FillSecSales_Formula();
            }
        }
    }


    protected void grdSecSales_RowEditing(object sender, GridViewEditEventArgs e)
    {
    }

    protected void grdSecSales_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
    }

    protected void grdSecSales_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
    }

    private void FillSecSales_Formula()
    {
        try
        {
            SecSale ss = new SecSale();
            dsSale = ss.getSaleMaster_Formula(div_code);
            if (dsSale.Tables[0].Rows.Count > 0)
            {
                grdSecSales.DataSource = dsSale;
                grdSecSales.DataBind();
            }
        }
        catch (Exception ex)
        {
            ErrorLog err = new ErrorLog();
            iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Setup - Formula", "FillSecSales_Formula()");
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Unknown error exists. Please contact customer care for support');</script>");
        }
    }

    private void ClearAll()
    {
        txtColName.Text = "";
        //txtOrderBy.Text = "";
        ddlOpr.SelectedIndex = 0;
        ddlParam.SelectedIndex = 0;
        rdoDisable.SelectedIndex = 0;
        litFormula.Text = " ----- ";
    }

    protected void btnReset_Click(object sender, EventArgs e)
    {

    }

    protected void btnClear_Click(object sender, EventArgs e)
    {

    }
}