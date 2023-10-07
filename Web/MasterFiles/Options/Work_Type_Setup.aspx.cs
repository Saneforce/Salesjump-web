using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
public partial class MasterFiles_Options_Work_Type_Setup : System.Web.UI.Page
{
    #region Declaration
    string dive_code = string.Empty;
    DataSet dsadm = null;
    DataSet dsButt = null;
    string sCmd = string.Empty;
    int WorkType_Code_B = 0;
    int i;
    string[] desigcd;
    string[] descd;
    string WorkType_Code = string.Empty;
    string Slno = string.Empty;
    DataSet dsDivision = null;
    DataSet dsWrktype = null;
    DataSet dsdiv = new DataSet();
    DataSet dsDesignation = null;
    DataSet dsDes = null;
    int iIndex;
    string strMultiDiv = string.Empty;
    string sf_type = string.Empty;
    string desig_cd = string.Empty;
    string des_cd = string.Empty;
    string sDesig = string.Empty;
    string sDes = string.Empty;
    string Designation_Short_Name = string.Empty;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int time;
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        sf_type = Session["sf_type"].ToString();
        dive_code = Convert.ToString(Session["div_code"]);
        if (sf_type == "3")
        {
            dive_code = Session["div_code"].ToString();
        }
        else
        {
            dive_code = Session["div_code"].ToString();
        }
        if (!Page.IsPostBack)
        {
            if (Session["sf_type"].ToString() == "" || Session["sf_type"].ToString() == null || Session["sf_type"].ToString() == "3")
            {
                //FillWorkType();
                fillDivision();
                menu1.Title = this.Page.Title;
                Session["backurl"] = "AdminSetup.aspx";
                ServerStartTime = DateTime.Now;
                base.OnPreInit(e);
                btnUpdate.Visible = false;
                btnDeactivate.Visible = false;
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
    private void fillDivision()
    {
        Division dv = new Division();
        if (sf_type == "3")
        {
            string[] strDivSplit = dive_code.Split(',');
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

    private void FillWorkType()
    {
        // Get values from DB(Mas_WorkType_BaseLevel)
        AdminSetup adm = new AdminSetup();
        dsWrktype = adm.getWorkTye(ddlDivision.SelectedValue, ddltype.SelectedIndex);
        if (dsWrktype.Tables[0].Rows.Count > 0)
        {
            btnUpdate.Visible = true;
            btnDeactivate.Visible = true;
            grdwrktype.Visible = true;
            grdwrktype.DataSource = dsWrktype;
            grdwrktype.DataBind();

            foreach (GridViewRow gridRow in grdwrktype.Rows)
            {
                DropDownList ddlTp_Flag = (DropDownList)gridRow.Cells[1].FindControl("ddlTp_Flag");
                DropDownList ddlTp_Dcr = (DropDownList)gridRow.Cells[1].FindControl("ddlTp_Dcr");
                DropDownList ddlPlace_inv = (DropDownList)gridRow.Cells[1].FindControl("ddlPlace_inv");
                DropDownList ddl_indicator = (DropDownList)gridRow.Cells[1].FindControl("ddl_indicator");



                Label lblWrkCode = (Label)gridRow.Cells[1].FindControl("lblWrkCode");
                WorkType_Code = lblWrkCode.Text.ToString();

                TextBox txtButt_Acc = (TextBox)gridRow.Cells[1].FindControl("txtButt_Acc");
                TextBox txtDesig = (TextBox)gridRow.Cells[1].FindControl("txtDesig");

                CheckBoxList chkButt_Acc = (CheckBoxList)gridRow.Cells[1].FindControl("chkButt_Acc");
                CheckBoxList ChkDesig = (CheckBoxList)gridRow.Cells[1].FindControl("ChkDesig");

                DataSet dsWrk = adm.getWrkTP_Indicator(WorkType_Code, ddlDivision.SelectedValue, ddltype.SelectedIndex);
                if (dsWrk.Tables[0].Rows.Count > 0)
                {
                    ddlTp_Flag.SelectedValue = dsWrk.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                    ddlTp_Dcr.SelectedValue = dsWrk.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                    ddlPlace_inv.SelectedValue = dsWrk.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();
                    ddl_indicator.SelectedValue = dsWrk.Tables[0].Rows[0].ItemArray.GetValue(3).ToString();

                    string str = dsWrk.Tables[0].Rows[0].ItemArray.GetValue(4).ToString();

                    string strtxtButt_Name = string.Empty;
                    string[] strButt;
                    iIndex = -1;
                    strButt = str.Split(',');
                    // Session["Value"] = str.Remove(str.Length - 1);
                    Session["Value"] = str;
                    foreach (string bt in strButt)
                    {
                        for (iIndex = 0; iIndex < chkButt_Acc.Items.Count; iIndex++)
                        {
                            if (bt == chkButt_Acc.Items[iIndex].Value)
                            {
                                chkButt_Acc.Text = "";
                                chkButt_Acc.Items[iIndex].Selected = true;

                                if (chkButt_Acc.Items[iIndex].Selected == true)
                                {
                                    strtxtButt_Name += chkButt_Acc.Items[iIndex].Text + ",";

                                }
                            }
                        }
                    }

                    if (strtxtButt_Name != "")
                    {
                        txtButt_Acc.Text = strtxtButt_Name.Remove(strtxtButt_Name.Length - 1);
                    }

                    string value = dsWrk.Tables[0].Rows[0].ItemArray.GetValue(5).ToString();
                    string strtxtDes_Value = string.Empty;
                    string[] strDes;
                    iIndex = -1;
                    strDes = value.Split(',');
                    // Session["Value"] = str.Remove(str.Length - 1);
                    Session["Value"] = value;
                    foreach (string dg in strDes)
                    {
                        for (iIndex = 0; iIndex < ChkDesig.Items.Count; iIndex++)
                        {
                            if (dg == ChkDesig.Items[iIndex].Text)
                            {
                                ChkDesig.Text = "";
                                ChkDesig.Items[iIndex].Selected = true;

                                if (ChkDesig.Items[iIndex].Selected == true)
                                {
                                    strtxtDes_Value += ChkDesig.Items[iIndex].Text + ",";

                                }
                            }
                        }
                    }

                    if (strtxtDes_Value != "")
                    {
                        txtDesig.Text = strtxtDes_Value.Remove(strtxtDes_Value.Length - 1);
                    }                  

                }
            }

        }
        else
        {
            btnUpdate.Visible = false;
            btnDeactivate.Visible = false;
            grdwrktype.DataSource = dsWrktype;
            grdwrktype.DataBind();
        }
    }

    protected void grdwrktype_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "this.className='highlight'");
            e.Row.Attributes.Add("onmouseout", "this.className='normal'");
        }
    }
    protected DataSet FillTp_Flag()
    {
        AdminSetup adm = new AdminSetup();
        dsadm = adm.getTp_Flag(ddlDivision.SelectedValue);
        return dsadm;
    }

    protected DataSet FillWork_indicator()
    {
        AdminSetup adm = new AdminSetup();
        dsadm = adm.getFieldWork_Indicator(ddlDivision.SelectedValue, ddltype.SelectedIndex);
        return dsadm;
    }

    protected void grdwrktype_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdwrktype.PageIndex = e.NewPageIndex;
        sCmd = Session["GetcmdArgChar"].ToString();
        //Fill the State Grid
        if (sCmd == "All")
        {
            FillWorkType();
        }
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        bool isError = false;
        System.Threading.Thread.Sleep(time);
        int i = 1;
        int iVal = 1;
        string sVal = string.Empty;
        sVal = ",";
        foreach (GridViewRow gridRow in grdwrktype.Rows)
        {
            TextBox txtSNo = (TextBox)gridRow.Cells[1].FindControl("txtWrkOrder");
            if (txtSNo.Text.Length > 0)
            {
                if (grdwrktype.Rows.Count >= Convert.ToInt16(txtSNo.Text.Trim())) // This is to check if the user entered a valid number
                {
                    sVal = sVal + txtSNo.Text + ',';
                }
                else // to validate
                {
                    isError = true;
                    txtSNo.Focus();
                    break;// User entered a number greater than the count..
                }
            }
            i++;
        }
        if (isError == false) // Included to validate
        {
            if (sVal == "")
            {
                foreach (GridViewRow gridRow in grdwrktype.Rows)
                {
                    Label lblSNo = (Label)gridRow.Cells[1].FindControl("lblSNo");
                    TextBox txtSNo = (TextBox)gridRow.Cells[1].FindControl("txtWrkOrder");
                    txtSNo.Text = lblSNo.Text;
                }
            }
            else
            {
                iVal = 1;
                System.Threading.Thread.Sleep(time);
                foreach (GridViewRow gridRow in grdwrktype.Rows)
                {
                    TextBox txtSNo = (TextBox)gridRow.Cells[1].FindControl("txtWrkOrder");
                    if (txtSNo.Text.Length <= 0)
                    {
                        for (iVal = 1; iVal <= i; iVal++)
                        {
                            string schk = ',' + iVal.ToString() + ',';
                            if (sVal.IndexOf(schk) != -1)
                            {
                                //Do Nothing
                            }
                            else
                            {
                                sVal = sVal + iVal.ToString() + ',';
                                break;
                            }
                        }
                        txtSNo.Text = iVal.ToString();
                    }

                }
            }
            btnUpdate.Focus();
        }
        else if (isError == true)
        {
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Enter Valid Number');</script>");
        }

        if (isError == false)
        {

            System.Threading.Thread.Sleep(time);
            string WorkType_Code = string.Empty;
            string WType_SName = string.Empty;
            string WorkType_Orderly = string.Empty;
            string TP_Flag = string.Empty;
            string TP_DCR = string.Empty;
            string Place_Involved = string.Empty;
            //string Button_Access = string.Empty;
            string FieldWork_Indicator = string.Empty;
            AdminSetup adm = new AdminSetup();
            int iReturn = -1;
            bool err = false;
            foreach (GridViewRow gridRow in grdwrktype.Rows)
            {
                string order_by = string.Empty;
                AdminSetup ad = new AdminSetup();
                TextBox name = (TextBox)grdwrktype.FooterRow.FindControl("name");
                TextBox short_name = (TextBox)grdwrktype.FooterRow.FindControl("short");
                TextBox order = (TextBox)grdwrktype.FooterRow.FindControl("order");
                order_by = order.Text.ToString();
                DropDownList ddlTpFlag = (DropDownList)grdwrktype.FooterRow.FindControl("ddlTpFlag");
                DropDownList ddlTpDcr = (DropDownList)grdwrktype.FooterRow.FindControl("ddlTpDcr");
                DropDownList ddlplace = (DropDownList)grdwrktype.FooterRow.FindControl("ddlplace");
                TextBox butt = (TextBox)grdwrktype.FooterRow.FindControl("butt");
                butt.Text = "R";
                DropDownList ddlindi = (DropDownList)grdwrktype.FooterRow.FindControl("ddlindi");

                TextBox txtDes = (TextBox)grdwrktype.FooterRow.FindControl("txtDes");
                string desig = txtDes.Text.ToString();

                string strtxtDesig_text = string.Empty;
                string strtxtDesig_Value = string.Empty;
                string[] strDesig;
                iIndex = -1;
                strDesig = desig.Split(',');
                Session["Value"] = desig;

                foreach (string dg in strDesig)
                {
                    for (iIndex = 0; iIndex < ChkDes.Items.Count; iIndex++)
                    {
                        if (dg == ChkDes.Items[iIndex].Text)
                        {

                            ChkDes.Items[iIndex].Selected = true;

                            if (ChkDes.Items[iIndex].Selected == true)
                            {
                                strtxtDesig_text += ChkDes.Items[iIndex].Text + ",";
                                strtxtDesig_Value += ChkDes.Items[iIndex].Value + ",";

                            }
                        }
                    }
                }
                if (strtxtDesig_text != "")
                {
                    txtDes.Text = strtxtDesig_text.Remove(strtxtDesig_text.Length - 1);
                    strtxtDesig_Value = strtxtDesig_Value.Remove(strtxtDesig_Value.Length - 1);
                }               

                if (name.Text == "")
                {
                    butt.Text = "Remarks";
                }
                else
                {

                    iReturn = ad.Addwrktype(name.Text, short_name.Text, order_by, ddlTpFlag.SelectedValue, ddlTpDcr.SelectedValue, ddlplace.SelectedValue, butt.Text, ddlindi.SelectedValue, ddlDivision.SelectedValue, ddltype.SelectedIndex, strtxtDesig_Value, txtDes.Text);


                    if (iReturn > 0)
                    {
                        //ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Created Successfully');</script>");
                        FillWorkType();
                    }

                    else if (iReturn == -2)
                    {
                        ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Short Name Already Exist');</script>");
                        butt.Text = "Remarks";
                        short_name.Focus();
                    }
                    else if (iReturn == -3)
                    {
                        ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Name Already Exist');</script>");
                        butt.Text = "Remarks";
                        name.Focus();
                    }
                }


                Label lblWrkCode = (Label)gridRow.Cells[1].FindControl("lblWrkCode");
                WorkType_Code = lblWrkCode.Text.ToString();
                TextBox txtWrkSName = (TextBox)gridRow.Cells[1].FindControl("txtWrkSName");
                WType_SName = txtWrkSName.Text.ToString();
                TextBox txtWrkOrder = (TextBox)gridRow.Cells[1].FindControl("txtWrkOrder");
                WorkType_Orderly = txtWrkOrder.Text.ToString();
                DropDownList ddlTp_Flag = (DropDownList)gridRow.Cells[1].FindControl("ddlTp_Flag");
                TP_Flag = ddlTp_Flag.SelectedValue.ToString();
                DropDownList ddlTp_Dcr = (DropDownList)gridRow.Cells[1].FindControl("ddlTp_Dcr");
                TP_DCR = ddlTp_Dcr.SelectedValue.ToString();
                DropDownList ddlPlace_inv = (DropDownList)gridRow.Cells[1].FindControl("ddlPlace_inv");
                Place_Involved = ddlPlace_inv.SelectedValue.ToString();
                TextBox txtButt_Acc = (TextBox)gridRow.Cells[1].FindControl("txtButt_Acc");
                string str = txtButt_Acc.Text.ToString();

                string strtxtButt_Value = string.Empty;
                string[] strButt;
                iIndex = -1;
                strButt = str.Split(',');
                //Session["Value"] = str.Remove(str.Length - 1);
                Session["Value"] = str;
                foreach (string bt in strButt)
                {
                    for (iIndex = 0; iIndex < chkButt_Acc.Items.Count; iIndex++)
                    {
                        if (bt == chkButt_Acc.Items[iIndex].Text)
                        {

                            chkButt_Acc.Items[iIndex].Selected = true;

                            if (chkButt_Acc.Items[iIndex].Selected == true)
                            {
                                strtxtButt_Value += chkButt_Acc.Items[iIndex].Value + ",";

                            }
                        }
                    }
                }
                if (strtxtButt_Value != "")
                {
                    txtButt_Acc.Text = strtxtButt_Value.Remove(strtxtButt_Value.Length - 1);
                }

                DropDownList ddl_indicator = (DropDownList)gridRow.Cells[1].FindControl("ddl_indicator");
                FieldWork_Indicator = ddl_indicator.SelectedValue.ToString();

                TextBox txtDesig = (TextBox)gridRow.Cells[1].FindControl("txtDesig");
                string des = txtDesig.Text.ToString();

                string strtxtDes_text = string.Empty;
                string strtxtDes_Value = string.Empty;
                string[] strDes;
                iIndex = -1;
                strDes = des.Split(',');
                Session["Value"] = des;

                foreach (string dg in strDes)
                {
                    for (iIndex = 0; iIndex < ChkDesig.Items.Count;  iIndex++)
                    {
                        if (dg == ChkDesig.Items[iIndex].Text)
                        {

                            ChkDesig.Items[iIndex].Selected = true;

                            if (ChkDesig.Items[iIndex].Selected == true)
                            {
                                strtxtDes_text += ChkDesig.Items[iIndex].Text + ",";
                                strtxtDes_Value += ChkDesig.Items[iIndex].Value + ",";

                            }
                        }
                    }
                }
                if (strtxtDes_text != "")
                {
                    txtDesig.Text = strtxtDes_text.Remove(strtxtDes_text.Length - 1);
                    strtxtDes_Value = strtxtDes_Value.Remove(strtxtDes_Value.Length - 1);
                }               


                //Update
                iReturn = adm.Wrk_RecordUpdate(Convert.ToInt16(WorkType_Code), WType_SName, WorkType_Orderly, TP_Flag, TP_DCR, Place_Involved, txtButt_Acc.Text, FieldWork_Indicator, ddlDivision.SelectedValue, ddltype.SelectedIndex, txtDesig.Text, strtxtDes_Value);



                if (iReturn > 0)
                    err = false;

                if ((iReturn == -2))
                {
                    txtWrkSName.Focus();
                    err = true;
                    break;
                }
                //if ((iReturn == -3))
                //{
                //    txtWrkOrder.Focus();
                //    err = true;
                //    break;
                //}
            }

            foreach (GridViewRow gridRow in grdwrktype.Rows)
            {
                DropDownList ddlTp_Flag = (DropDownList)gridRow.Cells[1].FindControl("ddlTp_Flag");
                DropDownList ddlTp_Dcr = (DropDownList)gridRow.Cells[1].FindControl("ddlTp_Dcr");
                DropDownList ddlPlace_inv = (DropDownList)gridRow.Cells[1].FindControl("ddlPlace_inv");
                DropDownList ddl_indicator = (DropDownList)gridRow.Cells[1].FindControl("ddl_indicator");


                Label lblWrkCode = (Label)gridRow.Cells[1].FindControl("lblWrkCode");
                WorkType_Code = lblWrkCode.Text.ToString();

                TextBox txtButt_Acc = (TextBox)gridRow.Cells[1].FindControl("txtButt_Acc");

                CheckBoxList chkButt_Acc = (CheckBoxList)gridRow.Cells[1].FindControl("chkButt_Acc");

                DataSet dsWrk = adm.getWrkTP_Indicator(WorkType_Code, ddlDivision.SelectedValue, ddltype.SelectedIndex);
                if (dsWrk.Tables[0].Rows.Count > 0)
                {
                    //ddlTp_Flag.SelectedValue = dsWrk.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                    ddlTp_Dcr.SelectedValue = dsWrk.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                    ddlPlace_inv.SelectedValue = dsWrk.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();
                    ddl_indicator.SelectedValue = dsWrk.Tables[0].Rows[0].ItemArray.GetValue(3).ToString();

                    string str = dsWrk.Tables[0].Rows[0].ItemArray.GetValue(4).ToString();

                    string strtxtButt_Name = string.Empty;
                    string[] strButt;
                    iIndex = -1;
                    strButt = str.Split(',');
                    // Session["Value"] = str.Remove(str.Length - 1);
                    Session["Value"] = str;
                    foreach (string bt in strButt)
                    {
                        for (iIndex = 0; iIndex < chkButt_Acc.Items.Count; iIndex++)
                        {
                            if (bt == chkButt_Acc.Items[iIndex].Value)
                            {
                                //chkButt_Acc.Text = "";
                                chkButt_Acc.Items[iIndex].Selected = true;

                                if (chkButt_Acc.Items[iIndex].Selected == true)
                                {
                                    strtxtButt_Name += chkButt_Acc.Items[iIndex].Text + ",";

                                }
                            }
                        }
                    }

                    if (strtxtButt_Name != "")
                    {
                        txtButt_Acc.Text = strtxtButt_Name.Remove(strtxtButt_Name.Length - 1);
                    }

                }


                if (err == false)
                {
                    //Updated Successfully
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Updated Successfully');</script>");
                    FillWorkType();
                }
                else if (err == true)
                {
                    if (iReturn == -2)
                    {   //Short name already exist
                        ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Short Name Already Exist');</script>");

                    }
                    //else if (iReturn == -3)
                    //{
                    //    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Number Already Exist');</script>");
                    //}

                }
            }
        }
    }
    protected void chkButt_Acc_SelectedIndexChanged(object sender, EventArgs e)
    {
        string name = "";
        string id = "";
        GridViewRow gv = (GridViewRow)((Control)sender).NamingContainer;
        CheckBoxList checkst = (CheckBoxList)gv.FindControl("chkButt_Acc");
        TextBox txtButt_Acc = (TextBox)gv.FindControl("txtButt_Acc");
        HiddenField hdnButtId = (HiddenField)gv.FindControl("hdnButtId");
        txtButt_Acc.Text = "";
        hdnButtId.Value = "";

        int countSelected = checkst.Items.Cast<ListItem>().Where(i => i.Selected).Count();
        if (countSelected == checkst.Items.Count - 1)
        {
            for (int i = 0; i < checkst.Items.Count; i++)
            {

                checkst.Items[i].Selected = false;
                //checkst.Items[i].Selected = true; 
            }

        }

        for (int i = 0; i < checkst.Items.Count; i++)
        {
            if (checkst.Items[i].Selected)
            {
                if (checkst.Items[i].Text != "ALL")
                {
                    name += checkst.Items[i].Text + ",";
                    id += checkst.Items[i].Value + ",";
                }

            }
        }

        txtButt_Acc.Text = name.TrimEnd(',');
        hdnButtId.Value = id.TrimEnd(',');
    }

    protected void btnGo_Click(object sender, EventArgs e)
    {
        //if (ddltype.SelectedIndex == 1)
        //{
        FillWorkType();
        //}
        //else
        //{
        //    btnUpdate.Visible = false;
        //    grdwrktype.Visible = false;
        //}        
    }
    protected void btnClose_Click(object sender, EventArgs e)
    {

    }

    protected void grdwrktype_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {

        string order_by = string.Empty;
        AdminSetup adm = new AdminSetup();
        TextBox name = (TextBox)grdwrktype.FooterRow.FindControl("name");
        TextBox short_name = (TextBox)grdwrktype.FooterRow.FindControl("short");
        TextBox order = (TextBox)grdwrktype.FooterRow.FindControl("order");
        order_by = order.Text.ToString();
        DropDownList ddlTpFlag = (DropDownList)grdwrktype.FooterRow.FindControl("ddlTpFlag");
        DropDownList ddlTpDcr = (DropDownList)grdwrktype.FooterRow.FindControl("ddlTpDcr");
        DropDownList ddlplace = (DropDownList)grdwrktype.FooterRow.FindControl("ddlplace");
        TextBox butt = (TextBox)grdwrktype.FooterRow.FindControl("butt");
        butt.Text = "R";
        DropDownList ddlindi = (DropDownList)grdwrktype.FooterRow.FindControl("ddlindi");

        TextBox txtDes = (TextBox)grdwrktype.FooterRow.FindControl("txtDes");
        string desig = txtDes.Text.ToString();

        string strtxtDesig_text = string.Empty;
        string strtxtDesig_Value = string.Empty;
        string[] strDesig;
        iIndex = -1;
        strDesig = desig.Split(',');
        Session["Value"] = desig;

        foreach (string dg in strDesig)
        {
            for (iIndex = 0; iIndex < ChkDes.Items.Count; iIndex++)
            {
                if (dg == ChkDes.Items[iIndex].Text)
                {

                    ChkDes.Items[iIndex].Selected = true;

                    if (ChkDes.Items[iIndex].Selected == true)
                    {
                        strtxtDesig_text += ChkDes.Items[iIndex].Text + ",";
                        strtxtDesig_Value += ChkDes.Items[iIndex].Value + ",";

                    }
                }
            }
        }
        if (strtxtDesig_text != "")
        {
            txtDes.Text = strtxtDesig_text.Remove(strtxtDesig_text.Length - 1);
            strtxtDesig_Value = strtxtDesig_Value.Remove(strtxtDesig_Value.Length - 1);
        }         

        if (name.Text == "")
        {
            butt.Text = "Remarks";
        }
        else
        {

            int iReturn = adm.Addwrktype(name.Text, short_name.Text, order_by, ddlTpFlag.SelectedValue, ddlTpDcr.SelectedValue, ddlplace.SelectedValue, butt.Text, ddlindi.SelectedValue, ddlDivision.SelectedValue, ddltype.SelectedIndex, strtxtDesig_Value, txtDes.Text);


            if (iReturn > 0)
            {
                //ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Created Successfully');</script>");
                FillWorkType();
            }

            else if (iReturn == -2)
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Short Name Already Exist');</script>");
                butt.Text = "Remarks";
                short_name.Focus();
            }
            else if (iReturn == -3)
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Name Already Exist');</script>");
                butt.Text = "Remarks";
                name.Focus();
            }
        }

    }

    protected void ChkDesig_SelectedIndexChanged(object sender, EventArgs e)
    {
        string name1 = "";
        string id1 = "";

        GridViewRow gv1 = (GridViewRow)((Control)sender).NamingContainer;
        CheckBoxList chkDes = (CheckBoxList)gv1.FindControl("ChkDesig");

        TextBox txtDesig = (TextBox)gv1.FindControl("txtDesig");
        HiddenField hdnDesigId = (HiddenField)gv1.FindControl("hdnDesigId");
        txtDesig.Text = "";
        hdnDesigId.Value = "";

        if (chkDes.Items[0].Text == "ALL" && chkDes.Items[0].Selected == true)
        {
            for (int i = 0; i < chkDes.Items.Count; i++)
            {
                chkDes.Items[i].Selected = true;
            }
        }

        int countSelected = chkDes.Items.Cast<ListItem>().Where(i => i.Selected).Count();
        if (countSelected == chkDes.Items.Count - 1)
        {
            for (int i = 0; i < chkDes.Items.Count; i++)
            {
                chkDes.Items[i].Selected = false;
            }
        }

        for (int i = 0; i < chkDes.Items.Count; i++)
        {
            if (chkDes.Items[i].Selected)
            {
                if (chkDes.Items[i].Text != "ALL")
                {
                    name1 += chkDes.Items[i].Text + ",";
                    id1 +=chkDes.Items[i].Text + ",";
                }
            }
        }

        if (name1 == "")
        {
            name1 = "----Select----";
        }

        txtDesig.Text = name1.TrimEnd(',');
        hdnDesigId.Value = id1.TrimEnd(',');
    }

    protected void grdwrktype_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            CheckBoxList ChkDesig = (CheckBoxList)e.Row.FindControl("ChkDesig");
            TextBox txtDesig = (TextBox)e.Row.FindControl("txtDesig");
            HiddenField hdnDesigId = (HiddenField)e.Row.FindControl("hdnDesigId");
            Designation des = new Designation();
            dsDesignation = des.getDesinationCode(dive_code, ddltype.SelectedIndex);
            if (dsDesignation.Tables[0].Rows.Count > 0)
            {
                int i = 0;
                desig_cd = string.Empty;

                   
                for (int j = 0; j < dsDesignation.Tables[0].Rows.Count; j++)
                {
                    sDesig += dsDesignation.Tables[0].Rows[j].ItemArray.GetValue(0).ToString() + ",";
                }

                desigcd = sDesig.Split(',');

                foreach (string st_cd in desigcd)
                {
                    if (i == 0)
                    {
                        desig_cd = desig_cd + st_cd;
                    }
                    else
                    {
                        if (st_cd.Trim().Length > 0)
                        {
                            desig_cd = desig_cd + "," + st_cd;
                        }
                    }
                    i++;
                }
                Designation desig = new Designation();
                dsDesignation = desig.getDesignationAddChkBox(desig_cd);
                ChkDesig.DataTextField = "Designation_Short_Name";
                ChkDesig.DataValueField = "Designation_Code";
                ChkDesig.DataSource = dsDesignation;
                ChkDesig.DataBind();
            }

            txtDesig.Text = "----Select----";

            string[] Desination;
            if (Designation_Short_Name != "")
            {
                iIndex = -1;
                Desination = desig_cd.Split(',');
                foreach (string st in Desination)
                {
                    for (iIndex = 0; iIndex < ChkDesig.Items.Count; iIndex++)
                    {
                        if (st == ChkDesig.Items[iIndex].Value)
                        {
                            ChkDesig.Items[iIndex].Selected = true;
                        }
                    }
                }
            }

        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            CheckBoxList ChkDes = (CheckBoxList)e.Row.FindControl("ChkDes");
            TextBox txtDes = (TextBox)e.Row.FindControl("txtDes");
            HiddenField hdnDesId = (HiddenField)e.Row.FindControl("hdnDesId");
            Designation des = new Designation();
            dsDes = des.getDesinationCode(dive_code, ddltype.SelectedIndex);
            if (dsDes.Tables[0].Rows.Count > 0) 
            {
                int i = 0;
                des_cd = string.Empty;



                for (int j = 0; j < dsDes.Tables[0].Rows.Count; j++)
                {

                    sDes += dsDes.Tables[0].Rows[j].ItemArray.GetValue(0).ToString() + ",";

                }

                descd = sDes.Split(',');

                foreach (string st_cd in descd)
                {
                    if (i == 0)
                    {
                        des_cd = des_cd + st_cd;
                    }
                    else
                    {
                        if (st_cd.Trim().Length > 0)
                        {
                            des_cd = des_cd + "," + st_cd;
                        }
                    }
                    i++;
                }
                Designation desig = new Designation();
                dsDes = desig.getDesignationAddChkBox(des_cd);
                ChkDes.DataTextField = "Designation_Short_Name";
                ChkDes.DataValueField = "Designation_Code";
                ChkDes.DataSource = dsDes;
                ChkDes.DataBind();
            }


            txtDes.Text = "---Select---";
            string[] Desination;
            if (Designation_Short_Name != "")
            {
                iIndex = -1;
                Desination = des_cd.Split(',');
                foreach (string st in Desination)
                    for (iIndex = 0; iIndex < ChkDes.Items.Count; iIndex++)
                {
                    {
                        if (st == ChkDes.Items[iIndex].Value)
                        {
                            ChkDes.Items[iIndex].Selected = true;
                        }
                    }
                }
            }

        }
    }

    protected void ChkDes_SelectedIndexChanged(object sender, EventArgs e)
    {
        string name1 = "";
        string id1 = "";

        GridViewRow gv1 = (GridViewRow)((Control)sender).NamingContainer;
        CheckBoxList chkDes = (CheckBoxList)gv1.FindControl("ChkDes");

        TextBox txtDesig = (TextBox)gv1.FindControl("txtDes");
        HiddenField hdnDesigId = (HiddenField)gv1.FindControl("hdnDesId");
        txtDesig.Text = "";
        hdnDesigId.Value = "";

        if (chkDes.Items[0].Text == "ALL" && chkDes.Items[0].Selected == true)
        {
            for (int i = 0; i < chkDes.Items.Count; i++)
            {
                chkDes.Items[i].Selected = true;
            }
        }

        int countSelected = chkDes.Items.Cast<ListItem>().Where(i => i.Selected).Count();
        if (countSelected == chkDes.Items.Count - 1)
        {
            for (int i = 0; i < chkDes.Items.Count; i++)
            {
                chkDes.Items[i].Selected = false;
            }
        }

        for (int i = 0; i < chkDes.Items.Count; i++)
        {
            if (chkDes.Items[i].Selected)
            {
                if (chkDes.Items[i].Text != "ALL")
                {
                    name1 += chkDes.Items[i].Text + ",";
                    id1 += chkDes.Items[i].Text + ",";
                }
            }
        }

        if (name1 == "")
        {
            name1 = "---Select---";
        }

        txtDesig.Text = name1.TrimEnd(',');
        hdnDesigId.Value = id1.TrimEnd(',');
    }
    protected void btnDeactivate_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        int iReturn = -1;

        foreach (GridViewRow gridRow in grdwrktype.Rows)
        {
            CheckBox chkWorktype = (CheckBox)gridRow.Cells[0].FindControl("chkWorktype");
            bool bCheck = chkWorktype.Checked;

            Label lblWrkCode = (Label)gridRow.Cells[3].FindControl("lblWrkCode");
            string worktype_Code = lblWrkCode.Text.ToString();

            if ((worktype_Code.Trim().Length > 0) && (bCheck == true))
            {
                AdminSetup adm = new AdminSetup();
                iReturn = adm.DeactivateWorktype(worktype_Code, ddltype.SelectedIndex);

            }
            else
            {

            }
        }
        if (iReturn != -1)
        {
            //  menu1.Status = "Chemists De-Activated Successfully!!";
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('De-Activated Successfully');</script>");
            FillWorkType();
        }
    }
}