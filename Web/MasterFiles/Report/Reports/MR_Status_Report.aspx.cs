using System;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Windows;
using System.IO;
using System.Data;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html;
using iTextSharp.text.html.simpleparser;
using System.Text;
using Bus_EReport;
using System.Net;

public partial class Reports_MR_Status_Report : System.Web.UI.Page
{
    string div_code = string.Empty;
    DataSet dsState = null;
    DataSet dsSalesForce = null;
    int iActiveCount = 0;
    int IDActiveCount = 0;
    int iLstActiveCount = 0;
    int iLstDActiveCount = 0;
    int iUnLstActiveCount = 0;
    int iUnLstDActiveCount = 0;
    int iChemistActiveCount = 0;
    int iChemistDActiveCount = 0;
    int iSockistActiveCount = 0;
    int iSockistDActiveCount = 0;
    int ActTerrtotal = 0;
    string strDiv_Code = "";
    int DeActTerrtotal = 0;
    int ActLstDrtotal = 0;
    int DeActive_ListedDR = 0;
    int DeUnLstDrtotal = 0;
    int DeActUnLstDrtotal = 0;
    int ActChemtotal = 0;
    int DeActChemtotal = 0;
    int ActStocktotal = 0;
    int DeActStocktotal = 0;
    string sState = string.Empty;
    string strMultiDiv = string.Empty;
    string sfCode = string.Empty;
    string strSf_Code = string.Empty;
    DataSet dsDivision = null;
    DataSet dsdiv = null;
    DataSet dsRep = null;
    string[] statecd;
    string slno;
    string state_cd = string.Empty;
    string sf_type = string.Empty;
    bool isff = false;
    protected void Page_Load(object sender, EventArgs e)
    {
        //div_code = Session["div_code"].ToString();
        sfCode = Session["sf_code"].ToString();
        sf_type = Session["sf_type"].ToString();
        if (sf_type == "3")
        {
            div_code = Session["division_code"].ToString();
        }
        else
        {
            div_code = Session["div_code"].ToString();
        }

        if (div_code.Contains(','))
        {
            div_code = div_code.Remove(div_code.Length - 1);
        }

        if (!Page.IsPostBack)
        {
            Filldiv();
            //FillState(div_code);
            if (rdoMGRState.SelectedValue.ToString() == "0")
            {
                lblState.Text = "FieldForce Name";
                if (Session["sf_type"].ToString() == "" || Session["sf_type"].ToString() == null || Session["sf_type"].ToString() == "3")
                {
                    FillManagers();
                    FillColor();
                //    ddlDivision.SelectedIndex = 1;
                    ddlDivision_SelectedIndexChanged(sender, e);
                    ddlFieldForce.SelectedIndex = 1;
                }
            }
            else
            {
                lblState.Text = "State";
                FillState(ddlDivision.SelectedValue.ToString());
            }

            if (Session["sf_type"].ToString() == "2")
            {
                FillMGRLogin();
            }

            Product prd = new Product();
            dsdiv = prd.getMultiDivsf_Name(sfCode);
            if (dsdiv.Tables[0].Rows.Count > 0)
            {
                if (dsdiv.Tables[0].Rows[0]["IsMultiDivision"].ToString() == "1")
                {
                    strMultiDiv = dsdiv.Tables[0].Rows[0][1].ToString().Remove(dsdiv.Tables[0].Rows[0][1].ToString().Length - 1, 1);
                    ddlDivision.SelectedValue = div_code;
                    ddlDivision.Visible = true;
                    lblDivision.Visible = true;
                    //getDivision();
                    Session["MultiDivision"] = ddlDivision.SelectedValue;
                }
                else
                {
                    Session["MultiDivision"] = "";
                    ddlDivision.Visible = false;
                    lblDivision.Visible = false;
                }
            }

            btnSubmit.Focus();
        }
        if (rdoMGRState.SelectedValue.ToString() == "0")
        {
           //FillColor();            
        }

        //FillColor();
        if (Session["sf_type"].ToString() == "1")
        {
            UserControl_MR_Menu c1 =
            (UserControl_MR_Menu)LoadControl("~/UserControl/MR_Menu.ascx");
            Divid.Controls.Add(c1);
            c1.FindControl("btnBack").Visible = false;
            c1.Title = Page.Title;
            ddlDivision.Visible = false;
            lblDivision.Visible = false;
            rdoMGRState.Visible = false;
            lblView.Visible = false;
            ddlFieldForce.Visible = false;
            ddlFFType.Visible = false;
            lblState.Visible = false;
            btnSubmit.Visible = false;
            btnSubmit_Click(sender, e);
            //lblFF.Visible = false;
            FillColor();
        }
        else if (Session["sf_type"].ToString() == "" || Session["sf_type"].ToString() == "3")
        {
            UserControl_pnlMenu c1 =
           (UserControl_pnlMenu)LoadControl("~/UserControl/pnlMenu.ascx");
            Divid.Controls.Add(c1);
            c1.FindControl("btnBack").Visible = false;
            c1.Title = Page.Title;
            if (rdoMGRState.SelectedValue.ToString() == "0")
            {
                //FillColor();
            }
            ddlDivision.Visible = true;
            lblDivision.Visible = true;
        }
        else if (Session["sf_type"].ToString() == "3")
        {
            UserControl_pnlMenu c1 =
                (UserControl_pnlMenu)LoadControl("~/UserControl/pnlMenu.ascx");
            Divid.Controls.Add(c1);
            c1.FindControl("btnBack").Visible = false;
            c1.Title = Page.Title;
            if (rdoMGRState.SelectedValue.ToString() == "0")
            {
                FillColor();
            }

            //ddlDivision.Visible = true;
            //lblDivision.Visible = true;
        }

        else if (Session["sf_type"].ToString() == "2")
        {
            UserControl_MGR_Menu c1 =
            (UserControl_MGR_Menu)LoadControl("~/UserControl/MGR_Menu.ascx");
            Divid.Controls.Add(c1);
            c1.FindControl("btnBack").Visible = false;
            c1.Title = Page.Title;            
            lblView.Visible = false;
            //ddlDivision.Visible = false;
            //lblDivision.Visible = false;
            rdoMGRState.Visible = false;
            ddlFFType.Visible = false;
            lblView.Visible = false;
            FillColor();

        }
        
    
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
                    System.Web.UI.WebControls.ListItem liTerr = new System.Web.UI.WebControls.ListItem();
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
    protected void ddlDivision_SelectedIndexChanged(object sender, EventArgs e)
    {
        div_code = ddlDivision.SelectedValue.ToString();
        if (Session["sf_type"].ToString() == "3")
        {
            FillManagers();
            FillColor();
        }
        else
        {
            FillMGRLogin();
            FillColor();
            Session["MultiDivision"] = div_code;
        }

        //FillSalesForce();
    }
    private void FillMRManagers()
    {
        SalesForce sf = new SalesForce();
        ddlFFType.Visible = false;
        ddlAlpha.Visible = false;
        dsSalesForce = sf.SalesForceListMgrGet(div_code,"");
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
        FillColor();

    }

    protected void btnExcel_Click(object sender, EventArgs e)
    {
        FillSalesForce();
        string attachment = "attachment; filename=Export.xls";

        Response.ClearContent();
        Response.AddHeader("content-disposition", attachment);
        Response.ContentType = "application/ms-excel";
        StringWriter sw = new StringWriter();
        HtmlTextWriter htw = new HtmlTextWriter(sw);
        HtmlForm frm = new HtmlForm();
        pnlContents.Parent.Controls.Add(frm);
        frm.Attributes["runat"] = "server";
        frm.Controls.Add(pnlContents);
        frm.RenderControl(htw);
        Response.Write(sw.ToString());
        Response.End();
        GrdDoctor.Visible = false;
    }
    private void FillState(string div_code)
    {
        Division dv = new Division();
        dsDivision = dv.getStatePerDivision(ddlDivision.SelectedValue.ToString());
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
            ddlFieldForce.DataTextField = "statename";
            ddlFieldForce.DataValueField = "state_code";
            ddlFieldForce.DataSource = dsState;
            ddlFieldForce.DataBind();
            //ddlState.SelectedIndex = 1;
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
    //    if (ddlFieldForce.SelectedValue.ToString().Trim().Length > 0)
    //    {
            ViewState["dsSalesForce"] = null;
            ViewState["dsDoctor"] = null;
            FillSalesForce();
        //}

    }

    private void FillColor()
    {
        int j = 0;


        foreach (System.Web.UI.WebControls.ListItem ColorItems in ddlSF.Items)
        {
            string bcolor = "#" + ColorItems.Text;
            ddlFieldForce.Items[j].Attributes.Add("style", "background-color:" + bcolor);

            j = j + 1;

        }
    }

    private void FillManagers()
    {
        SalesForce sf = new SalesForce();

        if (ddlFFType.SelectedValue.ToString() == "1")
        {
            ddlAlpha.Visible = false;
            dsSalesForce = sf.UserList_Hierarchy(ddlDivision.SelectedValue.ToString(), "admin");
        }
        else if (ddlFFType.SelectedValue.ToString() == "0")
        {
            FillSF_Alpha();
            ddlAlpha.Visible = true;
            dsSalesForce = sf.UserList_Alpha(ddlDivision.SelectedValue.ToString(), "admin");
        }
        else if (ddlFFType.SelectedValue.ToString() == "2")
        {
            dsSalesForce = sf.UserList_HQ(ddlDivision.SelectedValue.ToString(), "admin");
        }

        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            ddlFieldForce.DataTextField = "sf_name";
            ddlFieldForce.DataValueField = "sf_code";
            ddlFieldForce.DataSource = dsSalesForce;
            ddlFieldForce.DataBind();

            ddlSF.DataTextField = "des_color";
            ddlSF.DataValueField = "sf_code";
            ddlSF.DataSource = dsSalesForce;
            ddlSF.DataBind();

        }
    }

    protected void rdoMGRState_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rdoMGRState.SelectedValue.ToString() == "0")
        {
            lblState.Text = "Field Force";
            FillManagers();
            FillColor();
            ddlFFType.Visible = true;
            chkDeactive.Visible = true;
            GrdDoctor.Visible = true;
        }
        else
        {
            lblState.Text = "State";
            FillState(ddlDivision.SelectedValue.ToString());
            ddlFFType.Visible = false;
            ddlAlpha.Visible = false;
            chkDeactive.Visible = false;
            GrdDoctor.Visible = false;
        }

    }
    private void FillSF_Alpha()
    {
        SalesForce sf = new SalesForce();
        dsSalesForce = sf.getSalesForcelist_Alphabet(ddlDivision.SelectedValue.ToString());
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            ddlAlpha.DataTextField = "sf_name";
            ddlAlpha.DataValueField = "val";
            ddlAlpha.DataSource = dsSalesForce;
            ddlAlpha.DataBind();
            ddlAlpha.SelectedIndex = 0;
        }
    }

    protected void ddlAlpha_SelectedIndexChanged(object sender, EventArgs e)
    {
        SalesForce sf = new SalesForce();
        if (ddlAlpha.SelectedIndex == 0)
        {
            dsSalesForce = sf.UserList_Alpha(ddlDivision.SelectedValue.ToString(), "admin");
        }
        else
        {
            dsSalesForce = sf.UserList_Alphasearch(ddlDivision.SelectedValue.ToString(), "admin", ddlAlpha.SelectedValue);
        }
        
      //  dsSalesForce = sf.UserList_Alphasearch(ddlDivision.SelectedValue.ToString(), "admin", ddlAlpha.SelectedValue);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            ddlFieldForce.DataTextField = "sf_name";
            ddlFieldForce.DataValueField = "sf_code";
            ddlFieldForce.DataSource = dsSalesForce;
            ddlFieldForce.DataBind();

            ddlSF.DataTextField = "des_color";
            ddlSF.DataValueField = "sf_code";
            ddlSF.DataSource = dsSalesForce;
            ddlSF.DataBind();
            FillColor();
        }

    }
   
    protected void ddlFFType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rdoMGRState.SelectedValue.ToString() == "0")
        {
            lblState.Text = "Field Force";
            FillManagers();
            FillColor();
        }
        else
        {
            lblState.Text = "State";
            FillState(ddlDivision.SelectedValue.ToString());
        }

    }


    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if (ddlFieldForce.SelectedValue.ToString().Trim().Length > 0)
        //{
        //    ViewState["dsSalesForce"] = null;
        //    ViewState["dsDoctor"] = null;
        //    FillSalesForce();
        //}
    }

    private void FillSalesForce()
    {
        // Fetch the total rows for the table
        SalesForce sf = new SalesForce();

        Product prd = new Product();
        dsdiv = prd.getMultiDivsf_Name(sfCode);
        if (dsdiv.Tables[0].Rows.Count > 0)
        {
            if (dsdiv.Tables[0].Rows[0]["IsMultiDivision"].ToString() == "1")
            {
                div_code = ddlDivision.SelectedValue;
            }
        }

        //dsSalesForce = sf.getDoctorCount_statewise(div_code, ddlState.SelectedValue.ToString());
        if (Session["sf_type"].ToString() == "" || Session["sf_type"].ToString() == "3")
        {
            if (rdoMGRState.SelectedValue.ToString() == "0")
            {
                DataSet dsmgrsf = new DataSet();
                SalesForce ds = new SalesForce();

                // Check if the manager has a team
                DataSet DsAudit = ds.SF_Hierarchy(div_code, ddlFieldForce.SelectedValue.ToString());
                if (DsAudit.Tables[0].Rows.Count > 0)
                {
                    //dsSalesForce = sf.sp_UserListMr_Doc_List_Vacant(ddlFieldForce.SelectedValue.ToString(),ddlDivision.SelectedValue.ToString());
                    dsSalesForce = sf.sp_FieldForce(ddlDivision.SelectedValue.ToString(), ddlFieldForce.SelectedValue.ToString());
                    btnExcel.Visible = true;
                    GrdDoctor.DataSource = dsSalesForce;
                    GrdDoctor.DataBind();

                    //dsSalesForce = (DataSet)ViewState["dsSalesForce"];
                    foreach (DataRow drFF in dsSalesForce.Tables[0].Rows)
                    {
                        strSf_Code += "'" + drFF["sf_code"].ToString() + "'" + ",";
                    }
                    Session["Sf_Code_multiple"] = strSf_Code.Remove(strSf_Code.Length - 1);
                    

                }
                else
                {
                    // Fetch Managers Audit Team - MR

                    DataTable dt = ds.getAuditManagerTeam_GetMR_Sfname_With_Vacant(div_code, ddlFieldForce.SelectedValue.ToString(), 0);
                    dsmgrsf.Tables.Add(dt);
                    dsSalesForce = dsmgrsf;
                }
                // dsSalesForce = sf.sp_UserList_getMR_Doc_List(ddlDivision.SelectedValue.ToString(), ddlFieldForce.SelectedValue.ToString());

                FillColor();
            }
            else
            {
                dsSalesForce = sf.get_MR_Status_Report(ddlDivision.SelectedValue.ToString(), Convert.ToInt32(ddlFieldForce.SelectedValue.ToString()));
            }
            

        }
        if (Session["sf_type"].ToString() == "2")
        {
              DataSet dsmgrsf = new DataSet();
            SalesForce ds = new SalesForce();
            
            // Check if the manager has a team
          
            DataSet DsAudit = ds.SF_Hierarchy(div_code, ddlFieldForce.SelectedValue.ToString());
            if (DsAudit.Tables[0].Rows.Count > 0)
            {
                dsSalesForce = sf.sp_UserListMr_Doc_List_Vacant(ddlFieldForce.SelectedValue.ToString(), div_code);
            }
            else
            {
                // Fetch Managers Audit Team - MR

                DataTable dt = ds.getAuditManagerTeam_GetMR_Sfname_With_Vacant(div_code, ddlFieldForce.SelectedValue.ToString(), 0);
                dsmgrsf.Tables.Add(dt);
                dsSalesForce = dsmgrsf;
            }
            
            if (ddlFieldForce.SelectedValue.Contains("MR"))
            {
                dsSalesForce = sf.sp_UserMRLogin_With_Vacant(div_code, ddlFieldForce.SelectedValue.ToString());
            }
             
           // dsSalesForce = sf.sp_UserList_getMR_Doc_List(div_code, ddlFieldForce.SelectedValue.ToString());
        }

        if (Session["sf_type"].ToString() == "1")
        {
            dsSalesForce = sf.sp_UserMRLogin_With_Vacant(div_code, sfCode);
        }

        if (dsSalesForce.Tables[0].Rows.Count > 0)
            ViewState["dsSalesForce"] = dsSalesForce;

        if (Session["sf_type"].ToString() == "1" || Session["sf_type"].ToString() == "2" || rdoMGRState.SelectedValue.ToString()=="1")
        {
            CreateDynamicTable();
        }

        if (chkDeactive.Checked == true)
        {
            GrdDoctor.Columns[11].Visible = true;
            GrdDoctor.Columns[13].Visible = true;
            GrdDoctor.Columns[15].Visible = true;
            GrdDoctor.Columns[17].Visible = true;
            GrdDoctor.Columns[19].Visible = true;

        }
        else
        {
            GrdDoctor.Columns[11].Visible = false;
            GrdDoctor.Columns[13].Visible = false;
            GrdDoctor.Columns[15].Visible = false;
            GrdDoctor.Columns[17].Visible = false;
            GrdDoctor.Columns[19].Visible = false;
        }
        
    }

    protected void GrdDoctor_DataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            string sURL = "";
            
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                strDiv_Code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Division_Code"));
                ActTerrtotal += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Active_Territory"));
                HyperLink lblActive_Territory = (HyperLink)e.Row.FindControl("lblActive_Territory");
                if (lblActive_Territory.Text == "0")
                {
                    lblActive_Territory.Text = "-";
                    lblActive_Territory.Enabled = false;
                }
                Session["StrDiv_Code"] = strDiv_Code;

                DeActTerrtotal += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "DeActive_Territory"));                
                HyperLink lblDeActive_Territory = (HyperLink)e.Row.FindControl("lblDeActive_Territory");
                if (lblDeActive_Territory.Text == "0")
                {
                    lblDeActive_Territory.Text = "-";
                    lblDeActive_Territory.Enabled =false;
                }
                ActLstDrtotal += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Active_ListedDR"));
                HyperLink lblActive_ListedDR = (HyperLink)e.Row.FindControl("lblActive_ListedDR");
                if (lblActive_ListedDR.Text == "0")
                {
                    lblActive_ListedDR.Text = "-";
                    lblActive_ListedDR.Enabled = false;
                }
                DeActive_ListedDR += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "DeActive_ListedDR"));
                HyperLink lblDeActive_ListedDR = (HyperLink)e.Row.FindControl("lblDeActive_ListedDR");
                if (lblDeActive_ListedDR.Text == "0")
                {
                    lblDeActive_ListedDR.Text = "-";
                    lblDeActive_ListedDR.Enabled = false;
                }
                DeUnLstDrtotal += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Active_UnListedDR"));
                HyperLink lblActive_UnListedDR = (HyperLink)e.Row.FindControl("lblActive_UnListedDR");
                if (lblActive_UnListedDR.Text == "0")
                {
                    lblActive_UnListedDR.Text = "-";
                    lblActive_UnListedDR.Enabled = false;
                }

                DeActUnLstDrtotal += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "DeActive_UnListedDR"));
                HyperLink lblDeActive_UnListedDR = (HyperLink)e.Row.FindControl("lblDeActive_UnListedDR");
                if (lblDeActive_UnListedDR.Text == "0")
                {
                    lblDeActive_UnListedDR.Text = "-";
                    lblDeActive_UnListedDR.Enabled = false;
                }

                ActChemtotal += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Active_Chemists"));
                HyperLink lblActive_Chemists = (HyperLink)e.Row.FindControl("lblActive_Chemists");
                if (lblActive_Chemists.Text == "0")
                {
                    lblActive_Chemists.Text = "-";
                    lblActive_Chemists.Enabled = false;
                }
                DeActChemtotal += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "DeActive_Chemists"));
                HyperLink lblDeActive_Chemists = (HyperLink)e.Row.FindControl("lblDeActive_Chemists");
                if (lblDeActive_Chemists.Text == "0")
                {
                    lblDeActive_Chemists.Text = "-";
                    lblDeActive_Chemists.Enabled = false;
                }
                ActStocktotal += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Active_Stockiest"));
                HyperLink lblActive_Stockiest = (HyperLink)e.Row.FindControl("lblActive_Stockiest");
                if (lblActive_Stockiest.Text == "0")
                {
                    lblActive_Stockiest.Text = "-";
                    lblActive_Stockiest.Enabled = false;
                }
                DeActStocktotal += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "DeActive_Stockiest"));
                HyperLink lblDeActive_Stockiest = (HyperLink)e.Row.FindControl("lblDeActive_Stockiest");
                if (lblDeActive_Stockiest.Text == "0")
                {
                    lblDeActive_Stockiest.Text = "-";
                    lblDeActive_Stockiest.Enabled  = false;
                }
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                HyperLink lbl = (HyperLink)e.Row.FindControl("lblActTerrTotal");
                lbl.Text = ActTerrtotal.ToString();
                if (lbl.Text == "0")
                {
                    lbl.Text = "-";
                    lbl.Enabled = false;
                }
                HyperLink lblDeActiveTotal = (HyperLink)e.Row.FindControl("lblDeActiveTotal");
                lblDeActiveTotal.Text = DeActTerrtotal.ToString();
                if (lblDeActiveTotal.Text == "0")
                {
                    lblDeActiveTotal.Text = "-";
                    lblDeActiveTotal.Enabled = false;
                }
                HyperLink lblActiveLstDRTotal = (HyperLink)e.Row.FindControl("lblActiveLstDRTotal");
                lblActiveLstDRTotal.Text = ActLstDrtotal.ToString();
                if (lblActiveLstDRTotal.Text == "0")
                {
                    lblActiveLstDRTotal.Text = "-";
                    lblActiveLstDRTotal.Enabled = false;
                }
                HyperLink lblDeActiveLstDRTotal = (HyperLink)e.Row.FindControl("lblDeActiveLstDRTotal");
                lblDeActiveLstDRTotal.Text = DeActive_ListedDR.ToString();
                if (lblDeActiveLstDRTotal.Text == "0")
                {
                    lblDeActiveLstDRTotal.Text = "-";
                    lblDeActiveLstDRTotal.Enabled = false;
                }
                HyperLink lblActiveUnLstDRTotal = (HyperLink)e.Row.FindControl("lblActiveUnLstDRTotal");
                lblActiveUnLstDRTotal.Text = DeUnLstDrtotal.ToString();
                if (lblActiveUnLstDRTotal.Text == "0")
                {
                    lblActiveUnLstDRTotal.Text = "-";
                    lblActiveUnLstDRTotal.Enabled = false;
                }
                HyperLink lblDeActiveUnLstDRTotal = (HyperLink)e.Row.FindControl("lblDeActiveUnLstDRTotal");
                lblDeActiveUnLstDRTotal.Text = DeActUnLstDrtotal.ToString();
                if (lblDeActiveUnLstDRTotal.Text == "0")
                {
                    lblDeActiveUnLstDRTotal.Text = "-";
                    lblDeActiveUnLstDRTotal.Enabled = false;
                }
                HyperLink lblActiveChemistTotal = (HyperLink)e.Row.FindControl("lblActiveChemistTotal");
                lblActiveChemistTotal.Text = ActChemtotal.ToString();
                if (lblActiveChemistTotal.Text == "0")
                {
                    lblActiveChemistTotal.Text = "-";
                    lblActiveChemistTotal.Enabled = false;
                }
                HyperLink lblDeActiveChemistTotal = (HyperLink)e.Row.FindControl("lblDeActiveChemistTotal");
                lblDeActiveChemistTotal.Text = DeActChemtotal.ToString();
                if (lblDeActiveChemistTotal.Text == "0")
                {
                    lblDeActiveChemistTotal.Text = "-";
                    lblDeActiveChemistTotal.Enabled = false;
                }
                HyperLink lblActiveStockTotal = (HyperLink)e.Row.FindControl("lblActiveStockTotal");
                lblActiveStockTotal.Text = ActStocktotal.ToString();
                if (lblActiveStockTotal.Text == "0")
                {
                    lblActiveStockTotal.Text = "-";
                    lblActiveStockTotal.Enabled = false;
                }
                HyperLink lblDeActiveStockTotal = (HyperLink)e.Row.FindControl("lblDeActiveStockTotal");
                lblDeActiveStockTotal.Text = DeActStocktotal.ToString();
                if (lblDeActiveStockTotal.Text == "0")
                {
                    lblDeActiveStockTotal.Text = "-";
                    lblDeActiveStockTotal.Enabled = false;
                }
            }
            
        }
        catch (Exception ex)
        {
            Response.Redirect(ex.Message);
        }
    }

    private void FillMGRLogin()
    {
        SalesForce sf = new SalesForce();
        if (Session["sf_type"].ToString() == "2")
        {
            chkDeactive.Visible = false;
             DataSet dsmgrsf = new DataSet();
            SalesForce ds = new SalesForce();

            // Check if the manager has a team
            DataSet DsAudit = ds.SF_Hierarchy(div_code, sfCode);
            if (DsAudit.Tables[0].Rows.Count > 0)
            {
           // dsSalesForce = sf.UserList_Hierarchy(div_code, sfCode);
              dsSalesForce = sf.SalesForceListMgrGet(div_code, sfCode);
            }
            else
            {
                // Fetch Managers Audit Team
                DataTable dt = ds.getAuditManagerTeam_GetMGR(div_code, sfCode, 0);
                dsmgrsf.Tables.Add(dt);
                dsSalesForce = dsmgrsf;
            }
        }
        if (dsSalesForce.Tables[0].Rows.Count > 1)
        {
            ddlFieldForce.DataTextField = "sf_name";
            ddlFieldForce.DataValueField = "sf_code";
            ddlFieldForce.DataSource = dsSalesForce;
            ddlFieldForce.DataBind();


            ddlSF.DataTextField = "desig_color";
            ddlSF.DataValueField = "sf_code";
            ddlSF.DataSource = dsSalesForce;
            ddlSF.DataBind();
            FillColor();
        }
        else
        {

            dsSalesForce = sf.sp_UserMGRLogin(div_code, sfCode);

            ddlFieldForce.DataTextField = "sf_name";
            ddlFieldForce.DataValueField = "sf_code";
            ddlFieldForce.DataSource = dsSalesForce;
            ddlFieldForce.DataBind();


            ddlSF.DataTextField = "des_color";
            ddlSF.DataValueField = "sf_code";
            ddlSF.DataSource = dsSalesForce;
            ddlSF.DataBind();
            FillColor();
            ddlFFType.Visible = false;

            ddlFFType.Visible = false;

        }
    }

    private void CreateDynamicTable()
    {
        string sURL = string.Empty;

        if (ViewState["dsSalesForce"] != null)
        {
            ViewState["HQ_Det"] = null;

            TableRow tr_header = new TableRow();
            tr_header.BorderStyle = BorderStyle.Solid;
            tr_header.BorderWidth = 1;

            TableCell tc_SNo = new TableCell();
            tc_SNo.BorderStyle = BorderStyle.Solid;
            tc_SNo.BorderColor = System.Drawing.Color.Black;
            tc_SNo.BorderWidth = 1;
            tc_SNo.Width = 30;
            tc_SNo.ForeColor = System.Drawing.Color.White;
            Literal lit_SNo = new Literal();
            lit_SNo.Text = "S.No";
            tc_SNo.Controls.Add(lit_SNo);
            tc_SNo.RowSpan = 2;
            tc_SNo.Style.Add("font-family", "Calibri");
            tc_SNo.Style.Add("font-size", "10pt");
            tr_header.Cells.Add(tc_SNo);
            tr_header.BackColor = System.Drawing.Color.FromName("#336277");

            TableCell tc_user = new TableCell();
            tc_user.BorderStyle = BorderStyle.Solid;
            tc_user.BorderWidth = 1;
            tc_user.BorderColor = System.Drawing.Color.Black;
            tc_user.Width = 100;
            Literal lit_user = new Literal();
            lit_user.Text = "<center>User Name</center>";
            tc_user.Controls.Add(lit_user);
            tc_user.Visible = false;
            tc_user.ForeColor = System.Drawing.Color.White;
            tc_user.RowSpan = 2;
            tc_user.Style.Add("font-family", "Calibri");
            tc_user.Style.Add("font-size", "10pt"); 
            tr_header.Cells.Add(tc_user);

            TableCell tc_FF = new TableCell();
            tc_FF.BorderStyle = BorderStyle.Solid;
            tc_FF.BorderColor = System.Drawing.Color.Black;
            tc_FF.BorderWidth = 1;
            tc_FF.Width = 500;
            Literal lit_FF = new Literal();
            lit_FF.Text = "<center>Field Force Name</center>";
            tc_FF.Controls.Add(lit_FF);
            tc_FF.ForeColor = System.Drawing.Color.White;
            tc_FF.RowSpan = 2;
            tc_FF.Style.Add("font-family", "Calibri");
            tc_FF.Style.Add("font-size", "10pt"); 
            tr_header.Cells.Add(tc_FF);

            TableCell tc_Designation = new TableCell();
            tc_Designation.BorderStyle = BorderStyle.Solid;
            tc_Designation.BorderColor = System.Drawing.Color.Black;
            tc_Designation.BorderWidth = 1;
            tc_Designation.Width = 200;
            Literal lit_Designation = new Literal();
            lit_Designation.Text = "<center>Designation</center>";
            tc_Designation.Controls.Add(lit_Designation);
            tc_Designation.ForeColor = System.Drawing.Color.White;
            tc_Designation.RowSpan = 2;
            tc_Designation.Style.Add("font-family", "Calibri");
            tc_Designation.Style.Add("font-size", "10pt");
            tr_header.Cells.Add(tc_Designation);

            TableCell tc_hq = new TableCell();
            tc_hq.BorderStyle = BorderStyle.Solid;
            tc_hq.BorderColor = System.Drawing.Color.Black;
            tc_hq.BorderWidth = 1;
            tc_hq.Width = 200;
            Literal lit_hq = new Literal();
            lit_hq.Text = "<center>HQ</center>";
            tc_hq.Controls.Add(lit_hq);
            tc_hq.ForeColor = System.Drawing.Color.White;
            tc_hq.RowSpan = 2;
            tc_hq.Style.Add("font-family", "Calibri");
            tc_hq.Style.Add("font-size", "10pt");
            tr_header.Cells.Add(tc_hq);

            TableCell tc_Terr = new TableCell();
            tc_Terr.BorderStyle = BorderStyle.Solid;
            tc_Terr.BorderColor = System.Drawing.Color.Black;
            tc_Terr.BorderWidth = 1;
            tc_Terr.Width = 100;
            tc_Terr.ForeColor = System.Drawing.Color.White;
            Literal lit_Terr = new Literal();
           // lit_Terr.Text = "<center>Territory</center>";
            Territory terr = new Territory();
            DataSet dsTerritory = new DataSet();
            dsTerritory = terr.getWorkAreaName(ddlDivision.SelectedValue);
            if (dsTerritory.Tables[0].Rows.Count > 0)
            {
                lit_Terr.Text = "<center> " + dsTerritory.Tables[0].Rows[0]["wrk_area_Name"].ToString() + "</center>";

            }
            else
            {
                lit_Terr.Text = "<center>Territory</center>";
            }
            tc_Terr.Controls.Add(lit_Terr);
            tc_Terr.ColumnSpan = 2;
            tc_Terr.Style.Add("font-family", "Calibri");
            tc_Terr.Style.Add("font-size", "10pt"); 
            tr_header.Cells.Add(tc_Terr);

            TableCell tc_doc = new TableCell();
            tc_doc.BorderStyle = BorderStyle.Solid;
            tc_doc.BorderColor = System.Drawing.Color.Black;
            tc_doc.BorderWidth = 1;
            tc_doc.ForeColor = System.Drawing.Color.White;
            tc_doc.Width = 100;
            Literal lit_doc = new Literal();
            lit_doc.Text = "<center>Listed Drs</center>";
            tc_doc.Controls.Add(lit_doc);
            tc_doc.ColumnSpan = 2;
            tc_doc.Style.Add("font-family", "Calibri");
            tc_doc.Style.Add("font-size", "10pt");
            tr_header.Cells.Add(tc_doc);

            TableCell tc_nondoc = new TableCell();
            tc_nondoc.BorderStyle = BorderStyle.Solid;
            tc_nondoc.BorderColor = System.Drawing.Color.Black;
            tc_nondoc.BorderWidth = 1;
            tc_nondoc.Width = 100;
            tc_nondoc.ForeColor = System.Drawing.Color.White;
            Literal lit_nondoc = new Literal();
            lit_nondoc.Text = "<center>Un Listed Drs</center>";
            tc_nondoc.Controls.Add(lit_nondoc);
            tc_nondoc.ColumnSpan = 2;
            tc_nondoc.Style.Add("font-family", "Calibri");
            tc_nondoc.Style.Add("font-size", "10pt");
            tr_header.Cells.Add(tc_nondoc);

            TableCell tc_Chem = new TableCell();
            tc_Chem.BorderStyle = BorderStyle.Solid;
            tc_Chem.BorderColor = System.Drawing.Color.Black;
            tc_Chem.BorderWidth = 1;
            tc_Chem.Width = 100;
            tc_Chem.ForeColor = System.Drawing.Color.White;
            Literal lit_Chem = new Literal();
            lit_Chem.Text = "<center>Chemists</center>";
            tc_Chem.Controls.Add(lit_Chem);
            tc_Chem.ColumnSpan = 2;
            tc_Chem.Style.Add("font-family", "Calibri");
            tc_Chem.Style.Add("font-size", "10pt");
            tr_header.Cells.Add(tc_Chem);

            TableCell tc_Stok = new TableCell();
            tc_Stok.BorderStyle = BorderStyle.Solid;
            tc_Stok.BorderColor = System.Drawing.Color.Black;
            tc_Stok.BorderWidth = 1;
            tc_Stok.Width = 100;
            tc_Stok.ForeColor = System.Drawing.Color.White;
            Literal lit_Stok = new Literal();
            lit_Stok.Text = "<center>Stockiest</center>";
            tc_Stok.Controls.Add(lit_Stok);
            tc_Stok.ColumnSpan = 2;
            tc_Stok.Style.Add("font-family", "Calibri");
            tc_Stok.Style.Add("font-size", "10pt");
            tr_header.Cells.Add(tc_Stok);
            tbl.Rows.Add(tr_header);

            TableRow tr_active = new TableRow();
            tr_active.BackColor = System.Drawing.Color.FromName("#336277");

            TableCell tc_terr_active = new TableCell();
            tc_terr_active.BorderStyle = BorderStyle.Solid;
            tc_terr_active.BorderColor = System.Drawing.Color.Black;
            tc_terr_active.BorderWidth = 1;
            tc_terr_active.Width = 100;
            tc_terr_active.Style.Add("font-family", "Calibri");
            tc_terr_active.Style.Add("font-size", "10pt");
            tc_terr_active.ForeColor = System.Drawing.Color.White;
            Literal lit_terr_active = new Literal();
            lit_terr_active.Text = "<center>Active</center>";
            tc_terr_active.Controls.Add(lit_terr_active);
            tr_active.Cells.Add(tc_terr_active);

            TableCell tc_terr_deactive = new TableCell();
            tc_terr_deactive.BorderStyle = BorderStyle.Solid;
            tc_terr_deactive.BorderColor = System.Drawing.Color.Black;
            tc_terr_deactive.BorderWidth = 1;
            tc_terr_deactive.Width = 100;
            tc_terr_deactive.Style.Add("font-family", "Calibri");
            tc_terr_deactive.Style.Add("font-size", "10pt");
            tc_terr_deactive.ForeColor = System.Drawing.Color.White;
            Literal lit_terr_deactive = new Literal();
            lit_terr_deactive.Text = "<center>DeActive</center>";
            tc_terr_deactive.Controls.Add(lit_terr_deactive);
            tr_active.Cells.Add(tc_terr_deactive);

            TableCell tc_lst_dr_active = new TableCell();
            tc_lst_dr_active.BorderStyle = BorderStyle.Solid;
            tc_lst_dr_active.BorderColor = System.Drawing.Color.Black;
            tc_lst_dr_active.BorderWidth = 1;
            tc_lst_dr_active.Width = 100;
            tc_lst_dr_active.Style.Add("font-family", "Calibri");
            tc_lst_dr_active.Style.Add("font-size", "10pt");
            tc_lst_dr_active.ForeColor = System.Drawing.Color.White;
            Literal lit_lst_dr_active = new Literal();
            lit_lst_dr_active.Text = "<center>Active</center>";
            tc_lst_dr_active.Controls.Add(lit_lst_dr_active);
            tr_active.Cells.Add(tc_lst_dr_active);

            TableCell tc_lst_dr_deactive = new TableCell();
            tc_lst_dr_deactive.BorderStyle = BorderStyle.Solid;
            tc_lst_dr_deactive.BorderColor = System.Drawing.Color.Black;
            tc_lst_dr_deactive.BorderWidth = 1;
            tc_lst_dr_deactive.Width = 100;
            tc_lst_dr_deactive.Style.Add("font-family", "Calibri");
            tc_lst_dr_deactive.Style.Add("font-size", "10pt");
            tc_lst_dr_deactive.ForeColor = System.Drawing.Color.White;
            Literal lit_lst_dr_deactive = new Literal();
            lit_lst_dr_deactive.Text = "<center>DeActive</center>";
            tc_lst_dr_deactive.Controls.Add(lit_lst_dr_deactive);
            tr_active.Cells.Add(tc_lst_dr_deactive);

            TableCell tc_non_lst_dr_active = new TableCell();
            tc_non_lst_dr_active.BorderStyle = BorderStyle.Solid;
            tc_non_lst_dr_active.BorderColor = System.Drawing.Color.Black;
            tc_non_lst_dr_active.BorderWidth = 1;
            tc_non_lst_dr_active.Width = 100;
            tc_non_lst_dr_active.Style.Add("font-family", "Calibri");
            tc_non_lst_dr_active.Style.Add("font-size", "10pt");
            tc_non_lst_dr_active.ForeColor = System.Drawing.Color.White;
            Literal lit_non_lst_dr_active = new Literal();
            lit_non_lst_dr_active.Text = "<center>Active</center>";
            tc_non_lst_dr_active.Controls.Add(lit_non_lst_dr_active);
            tr_active.Cells.Add(tc_non_lst_dr_active);

            TableCell tc_non_lst_dr_deactive = new TableCell();
            tc_non_lst_dr_deactive.BorderStyle = BorderStyle.Solid;
            tc_non_lst_dr_deactive.BorderColor = System.Drawing.Color.Black;
            tc_non_lst_dr_deactive.BorderWidth = 1;
            tc_non_lst_dr_deactive.Width = 100;
            tc_non_lst_dr_deactive.Style.Add("font-family", "Calibri");
            tc_non_lst_dr_deactive.Style.Add("font-size", "10pt");
            tc_non_lst_dr_deactive.ForeColor = System.Drawing.Color.White;
            Literal lit_non_lst_dr_deactive = new Literal();
            lit_non_lst_dr_deactive.Text = "<center>DeActive</center>";
            tc_non_lst_dr_deactive.Controls.Add(lit_non_lst_dr_deactive);
            tr_active.Cells.Add(tc_non_lst_dr_deactive);

            TableCell tc_chem_active = new TableCell();
            tc_chem_active.BorderStyle = BorderStyle.Solid;
            tc_chem_active.BorderColor = System.Drawing.Color.Black;
            tc_chem_active.BorderWidth = 1;
            tc_chem_active.Width = 100;
            tc_chem_active.Style.Add("font-family", "Calibri");
            tc_chem_active.Style.Add("font-size", "10pt");
            tc_chem_active.ForeColor = System.Drawing.Color.White;
            Literal lit_chem_active = new Literal();
            lit_chem_active.Text = "<center>Active</center>";
            tc_chem_active.Controls.Add(lit_chem_active);
            tr_active.Cells.Add(tc_chem_active);

            TableCell tc_chem_deactive = new TableCell();
            tc_chem_deactive.BorderStyle = BorderStyle.Solid;
            tc_chem_deactive.BorderColor = System.Drawing.Color.Black;
            tc_chem_deactive.BorderWidth = 1;
            tc_chem_deactive.Width = 100;
            tc_chem_deactive.Style.Add("font-family", "Calibri");
            tc_chem_deactive.Style.Add("font-size", "10pt");
            tc_chem_deactive.ForeColor = System.Drawing.Color.White;
            Literal lit_chem_deactive = new Literal();
            lit_chem_deactive.Text = "<center>DeActive</center>";
            tc_chem_deactive.Controls.Add(lit_chem_deactive);
            tr_active.Cells.Add(tc_chem_deactive);

            TableCell tc_stok_active = new TableCell();
            tc_stok_active.BorderStyle = BorderStyle.Solid;
            tc_stok_active.BorderColor = System.Drawing.Color.Black;
            tc_stok_active.BorderWidth = 1;
            tc_stok_active.Width = 100;
            tc_stok_active.Style.Add("font-family", "Calibri");
            tc_stok_active.Style.Add("font-size", "10pt");
            tc_stok_active.ForeColor = System.Drawing.Color.White;
            Literal lit_stok_active = new Literal();
            lit_stok_active.Text = "<center>Active</center>";
            tc_stok_active.Controls.Add(lit_stok_active);
            tr_active.Cells.Add(tc_stok_active);

            TableCell tc_stok_deactive = new TableCell();
            tc_stok_deactive.BorderStyle = BorderStyle.Solid;
            tc_stok_deactive.BorderColor = System.Drawing.Color.Black;
            tc_stok_deactive.BorderWidth = 1;
            tc_stok_deactive.Width = 100;
            tc_stok_deactive.Style.Add("font-family", "Calibri");
            tc_stok_deactive.Style.Add("font-size", "10pt");
            tc_stok_deactive.ForeColor = System.Drawing.Color.White;
            Literal lit_stok_deactive = new Literal();
            lit_stok_deactive.Text = "<center>DeActive</center>";
            tc_stok_deactive.Controls.Add(lit_stok_deactive);
            tr_active.Cells.Add(tc_stok_deactive);

            tbl.Rows.Add(tr_active);

            iActiveCount = 0;
            // Details Section
            if (rdoMGRState.SelectedValue.ToString() == "0")
            {
                SalesForce sf = new SalesForce();
                int iCount = 0;
                dsSalesForce = (DataSet)ViewState["dsSalesForce"];
                foreach (DataRow drFF in dsSalesForce.Tables[0].Rows)
                {
                    //if (ViewState["HQ_Det"] != null)
                    //{
                    //    if (ViewState["HQ_Det"].ToString() == drFF["sf_hq"].ToString())
                    //    {
                    //        TableRow tr_HQ_det = new TableRow();
                    //        TableCell tc_HQ_det = new TableCell();
                    //        Literal lit_HQ_det = new Literal();
                    //        lit_HQ_det.Text = drFF["sf_hq"].ToString();
                    //        tc_HQ_det.BorderStyle = BorderStyle.Solid;
                    //        tc_HQ_det.BorderWidth = 1;
                    //        tc_HQ_det.Controls.Add(lit_HQ_det);
                    //        tc_HQ_det.ColumnSpan = 13;
                    //        tc_HQ_det.Style.Add("font-family", "Calibri");
                    //        tc_HQ_det.Style.Add("font-size", "10pt");
                    //        tc_HQ_det.Style.Add("text-align", "left");
                    //        tc_HQ_det.BackColor = System.Drawing.Color.LightSteelBlue;
                    //        tr_HQ_det.Cells.Add(tc_HQ_det);
                    //        tbl.Rows.Add(tr_HQ_det);
                    //        ViewState["HQ_Det"] = drFF["sf_hq"].ToString();
                    //    }
                    //}
                    //else
                    //{
                    //    TableRow tr_HQ_det = new TableRow();
                    //    TableCell tc_HQ_det = new TableCell();
                    //    Literal lit_HQ_det = new Literal();
                    //    lit_HQ_det.Text = drFF["sf_hq"].ToString();
                    //    tc_HQ_det.BorderStyle = BorderStyle.Solid;
                    //    tc_HQ_det.BorderWidth = 1;
                    //    tc_HQ_det.Style.Add("font-family", "Calibri");
                    //    tc_HQ_det.Style.Add("font-size", "10pt");
                    //    tc_HQ_det.Controls.Add(lit_HQ_det);
                    //    tc_HQ_det.ColumnSpan = 13;
                    //    tc_HQ_det.BackColor = System.Drawing.Color.LightSteelBlue;
                    //    tr_HQ_det.Cells.Add(tc_HQ_det);
                    //    tbl.Rows.Add(tr_HQ_det);
                    //    tc_HQ_det.Style.Add("text-align", "left");
                    //    ViewState["HQ_Det"] = drFF["sf_hq"].ToString();
                    //}

                    //ViewState["HQ_Det"] = drFF["sf_hq"].ToString();
                    
                    TableRow tr_det = new TableRow();
                    tr_det.HorizontalAlign = HorizontalAlign.Center;
                    iCount += 1;
                    strSf_Code += "'" + drFF["sf_code"].ToString() + "'" + ",";

                    //S.No
                    TableCell tc_det_SNo = new TableCell();
                    Literal lit_det_SNo = new Literal();
                    lit_det_SNo.Text = "<center>" + iCount.ToString() + "</center>";
                    tc_det_SNo.BorderStyle = BorderStyle.Solid;
                    tc_det_SNo.BorderWidth = 1;
                    tc_det_SNo.Style.Add("font-family", "Calibri");
                    tc_det_SNo.Style.Add("font-size", "10pt");
                    tc_det_SNo.Controls.Add(lit_det_SNo);
                    tr_det.Cells.Add(tc_det_SNo);
                    tr_det.BackColor = System.Drawing.Color.White;                   

                    //SF User Name
                    TableCell tc_det_usr = new TableCell();
                    Literal lit_det_usr = new Literal();
                    lit_det_usr.Text = "&nbsp;" + drFF["sf_username"].ToString();
                    tc_det_usr.BorderStyle = BorderStyle.Solid;
                    tc_det_usr.BorderWidth = 1;
                    tc_det_usr.Visible = false;
                    tc_det_usr.Style.Add("font-family", "Calibri");
                    tc_det_usr.Style.Add("font-size", "10pt");
                    tc_det_usr.Style.Add("text-align", "left");
                    tc_det_usr.Controls.Add(lit_det_usr);
                    tr_det.Cells.Add(tc_det_usr);
                    
                    //SF Name
                    TableCell tc_det_FF = new TableCell();
                    Literal lit_det_FF = new Literal();
                    lit_det_FF.Text = "&nbsp;" + drFF["sf_name"].ToString();
                    tc_det_FF.BorderStyle = BorderStyle.Solid;
                    tc_det_FF.BorderWidth = 1;
                    tc_det_FF.Style.Add("font-family", "Calibri");
                    tc_det_FF.Style.Add("font-size", "10pt");
                    tc_det_FF.Style.Add("text-align", "left");
                    tc_det_FF.Controls.Add(lit_det_FF);
                    tr_det.Cells.Add(tc_det_FF);

                    //SF Designation Short Name
                    TableCell tc_det_Designation = new TableCell();
                    Literal lit_det_Designation = new Literal();
                    lit_det_Designation.Text = "&nbsp;" + drFF["sf_Designation_Short_Name"].ToString();
                    tc_det_Designation.BorderStyle = BorderStyle.Solid;
                    tc_det_Designation.BorderWidth = 1;
                    tc_det_Designation.Style.Add("font-family", "Calibri");
                    tc_det_Designation.Style.Add("font-size", "10pt");
                    tc_det_Designation.Style.Add("text-align", "left");
                    tc_det_Designation.Controls.Add(lit_det_Designation);
                    tr_det.Cells.Add(tc_det_Designation);

                    //hq
                    TableCell tc_det_hq = new TableCell();
                    Literal lit_det_hq = new Literal();
                    lit_det_hq.Text = "&nbsp;" + drFF["sf_hq"].ToString();
                    tc_det_hq.BorderStyle = BorderStyle.Solid;
                    tc_det_hq.BorderWidth = 1;
                    tc_det_hq.Style.Add("font-family", "Calibri");
                    tc_det_hq.Style.Add("font-size", "10pt");
                    tc_det_hq.Style.Add("text-align", "left");
                    tc_det_hq.Controls.Add(lit_det_hq);
                    tr_det.Cells.Add(tc_det_hq);

                    if (Session["sf_type"].ToString() == "" || Session["sf_type"].ToString() == "3")
                    {
                        dsRep = sf.get_Rep_Status_Report(ddlDivision.SelectedValue.ToString(), drFF["sf_code"].ToString());
                    }
                    else
                    {
                        dsRep = sf.get_Rep_Status_Report(div_code, drFF["sf_code"].ToString());
                    }
                    foreach (DataRow drSF in dsRep.Tables[0].Rows)
                    {
                        //Territory - Active 
                        TableCell tc_det_Terr_Act = new TableCell();
                        HyperLink hyp_det_Terr_Act = new HyperLink();
                        hyp_det_Terr_Act.Text = drSF["Active_Territory"].ToString();
                        if (Convert.ToInt32(drSF["Active_Territory"].ToString()) > 0)
                        {   
                            //hyp_det_Terr_Act.NavigateUrl = "rptMRStatus.aspx?sf_code=" + drSF["sf_code"].ToString() + "&sf_name=" + drSF["sf_name"].ToString() + "&type=1&status=0";
                            sURL = "rptMRStatus.aspx?sf_code=" + drSF["sf_code"].ToString() + "&sf_name=" + drSF["sf_name"].ToString() + "&type=1&status=0";
                            hyp_det_Terr_Act.Attributes["onclick"] = "javascript:window.open('" + sURL + "',null,'resizable=yes,scrollbars=1,toolbar=no,menubar=no,status=no,width=500,height=600,left=0,top=0');";
                            hyp_det_Terr_Act.NavigateUrl = "#";
                        }
                        else
                        {
                            hyp_det_Terr_Act.Text = " - ";
                        }
                        iActiveCount += Convert.ToInt16(hyp_det_Terr_Act.Text.Replace("-", "0"));

                        tc_det_Terr_Act.BorderStyle = BorderStyle.Solid;
                        tc_det_Terr_Act.BorderWidth = 1;
                        tc_det_Terr_Act.Style.Add("font-family", "Calibri");
                        tc_det_Terr_Act.Style.Add("font-size", "10pt");
                        tc_det_Terr_Act.Controls.Add(hyp_det_Terr_Act);
                        tr_det.Cells.Add(tc_det_Terr_Act);

                        //Territory - De-Active 
                        TableCell tc_det_Terr_DeAct = new TableCell();
                        HyperLink hyp_det_Terr_DeAct = new HyperLink();
                        hyp_det_Terr_DeAct.Text = drSF["DeActive_Territory"].ToString();
                        if (Convert.ToInt32(drSF["DeActive_Territory"].ToString()) > 0)
                        {
                            //hyp_det_Terr_DeAct.NavigateUrl = "rptMRStatus.aspx?sf_code=" + drSF["sf_code"].ToString() + "&sf_name=" + drSF["sf_name"].ToString() + "&type=1&status=1";
                            sURL = "rptMRStatus.aspx?sf_code=" + drSF["sf_code"].ToString() + "&sf_name=" + drSF["sf_name"].ToString() + "&type=1&status=1";
                            hyp_det_Terr_DeAct.Attributes["onclick"] = "javascript:window.open('" + sURL + "',null,'resizable=yes,toolbar=no,scrollbars=1,menubar=no,status=no,width=500,height=600,left=0,top=0');";
                            hyp_det_Terr_DeAct.NavigateUrl = "#";

                        }
                        else
                        {
                            hyp_det_Terr_DeAct.Text = " - ";
                        }
                        IDActiveCount += Convert.ToInt16(hyp_det_Terr_DeAct.Text.Replace("-", "0"));

                        tc_det_Terr_DeAct.BorderStyle = BorderStyle.Solid;
                        tc_det_Terr_DeAct.BorderWidth = 1;
                        tc_det_Terr_DeAct.Controls.Add(hyp_det_Terr_DeAct);
                        tc_det_Terr_DeAct.Style.Add("font-family", "Calibri");
                        tc_det_Terr_DeAct.Style.Add("font-size", "10pt");
                        tr_det.Cells.Add(tc_det_Terr_DeAct);

                        //Listed Doctor - Active
                        TableCell tc_det_lst_Act = new TableCell();
                        HyperLink hyp_det_lst_Act = new HyperLink();
                        hyp_det_lst_Act.Text =  drSF["Active_ListedDR"].ToString() ;
                        if (Convert.ToInt32(drSF["Active_ListedDR"].ToString()) > 0)
                        {
                            //hyp_det_lst_Act.NavigateUrl = "rptMRStatus.aspx?sf_code=" + drSF["sf_code"].ToString() + "&sf_name=" + drSF["sf_name"].ToString() + "&type=2&status=0";
                            sURL = "rptMRStatus.aspx?sf_code=" + drSF["sf_code"].ToString() + "&sf_name=" + drSF["sf_name"].ToString() + "&type=2&status=0";
                            hyp_det_lst_Act.Attributes["onclick"] = "javascript:window.open('" + sURL + "',null,'resizable=yes,scrollbars=1,toolbar=no,menubar=no,status=no,width=500,height=600,left=0,top=0');";
                            hyp_det_lst_Act.NavigateUrl = "#";
                        }
                        else
                      {
                            hyp_det_lst_Act.Text = " - ";
                        }
                        iLstActiveCount += Convert.ToInt16(hyp_det_lst_Act.Text.Replace("-", "0"));

                        tc_det_lst_Act.BorderStyle = BorderStyle.Solid;
                        tc_det_lst_Act.BorderWidth = 1;
                        tc_det_lst_Act.Style.Add("font-family", "Calibri");
                        tc_det_lst_Act.Style.Add("font-size", "10pt");
                        tc_det_lst_Act.Controls.Add(hyp_det_lst_Act);
                        tr_det.Cells.Add(tc_det_lst_Act);

                        //Listed Doctor - DeActive
                        TableCell tc_det_lst_DeAct = new TableCell();
                        HyperLink hyp_det_lst_DeAct = new HyperLink();
                        hyp_det_lst_DeAct.Text =  drSF["DeActive_ListedDR"].ToString() ;
                        if (Convert.ToInt32(drSF["DeActive_ListedDR"].ToString()) > 0)
                        {
                            //hyp_det_lst_DeAct.NavigateUrl = "rptMRStatus.aspx?sf_code=" + drSF["sf_code"].ToString() + "&sf_name=" + drSF["sf_name"].ToString() + "&type=2&status=1";
                            sURL = "rptMRStatus.aspx?sf_code=" + drSF["sf_code"].ToString() + "&sf_name=" + drSF["sf_name"].ToString() + "&type=2&status=1";
                            hyp_det_lst_DeAct.Attributes["onclick"] = "javascript:window.open('" + sURL + "',null,'resizable=yes,scrollbars=1,toolbar=no,menubar=no,status=no,width=500,height=600,left=0,top=0');";
                            hyp_det_lst_DeAct.NavigateUrl = "#";

                        }
                        else
                        {
                            hyp_det_lst_DeAct.Text = " - ";
                        }
                        iLstDActiveCount += Convert.ToInt16(hyp_det_lst_DeAct.Text.Replace("-", "0"));

                        tc_det_lst_DeAct.BorderStyle = BorderStyle.Solid;
                        tc_det_lst_DeAct.BorderWidth = 1;
                        tc_det_lst_DeAct.Style.Add("font-family", "Calibri");
                        tc_det_lst_DeAct.Style.Add("font-size", "10pt");
                        tc_det_lst_DeAct.Controls.Add(hyp_det_lst_DeAct);
                        tr_det.Cells.Add(tc_det_lst_DeAct);

                        //Un-Listed Doctor - Active
                        TableCell tc_det_unlst_Act = new TableCell();
                        HyperLink hyp_det_unlst_Act = new HyperLink();
                        hyp_det_unlst_Act.Text =  drSF["Active_UnListedDR"].ToString() ;
                        if (Convert.ToInt32(drSF["Active_UnListedDR"].ToString()) > 0)
                        {
                            //hyp_det_unlst_Act.NavigateUrl = "rptMRStatus.aspx?sf_code=" + drSF["sf_code"].ToString() + "&sf_name=" + drSF["sf_name"].ToString() + "&type=3&status=0";
                            sURL = "rptMRStatus.aspx?sf_code=" + drSF["sf_code"].ToString() + "&sf_name=" + drSF["sf_name"].ToString() + "&type=3&status=0";
                            hyp_det_unlst_Act.Attributes["onclick"] = "javascript:window.open('" + sURL + "',null,'resizable=yes,scrollbars=1,toolbar=no,menubar=no,status=no,width=500,height=600,left=0,top=0');";
                            hyp_det_unlst_Act.NavigateUrl = "#";

                        }
                        else
                        {
                            hyp_det_unlst_Act.Text = " - ";
                        }
                        iUnLstActiveCount += Convert.ToInt16(hyp_det_unlst_Act.Text.Replace("-", "0"));

                        tc_det_unlst_Act.BorderStyle = BorderStyle.Solid;
                        tc_det_unlst_Act.BorderWidth = 1;
                        tc_det_unlst_Act.Style.Add("font-family", "Calibri");
                        tc_det_unlst_Act.Style.Add("font-size", "10pt");
                        tc_det_unlst_Act.Controls.Add(hyp_det_unlst_Act);
                        tr_det.Cells.Add(tc_det_unlst_Act);

                        //Un-Listed Doctor - DeActive
                        TableCell tc_det_unlst_DeAct = new TableCell();
                        HyperLink hyp_det_unlst_DeAct = new HyperLink();
                        hyp_det_unlst_DeAct.Text =  drSF["DeActive_UnListedDR"].ToString() ;
                        if (Convert.ToInt32(drSF["DeActive_UnListedDR"].ToString()) > 0)
                        {
                            //hyp_det_unlst_DeAct.NavigateUrl = "rptMRStatus.aspx?sf_code=" + drSF["sf_code"].ToString() + "&sf_name=" + drSF["sf_name"].ToString() + "&type=3&status=1";
                            sURL = "rptMRStatus.aspx?sf_code=" + drSF["sf_code"].ToString() + "&sf_name=" + drSF["sf_name"].ToString() + "&type=3&status=1";
                            hyp_det_unlst_DeAct.Attributes["onclick"] = "javascript:window.open('" + sURL + "',null,'resizable=yes,scrollbars=1,toolbar=no,menubar=no,status=no,width=500,height=600,left=0,top=0');";
                            hyp_det_unlst_DeAct.NavigateUrl = "#";

                        }
                        else
                        {
                            hyp_det_unlst_DeAct.Text = " - ";
                        }
                        tc_det_unlst_DeAct.BorderStyle = BorderStyle.Solid;
                        tc_det_unlst_DeAct.BorderWidth = 1;
                        tc_det_unlst_DeAct.Style.Add("font-family", "Calibri");
                        tc_det_unlst_DeAct.Style.Add("font-size", "10pt");
                        tc_det_unlst_DeAct.Controls.Add(hyp_det_unlst_DeAct);
                        tr_det.Cells.Add(tc_det_unlst_DeAct);

                        iUnLstActiveCount += Convert.ToInt16(hyp_det_unlst_DeAct.Text.Replace("-", "0"));

                        //Chemists - Active
                        TableCell tc_det_chem_Act = new TableCell();
                        HyperLink hyp_det_chem_Act = new HyperLink();
                        hyp_det_chem_Act.Text =  drSF["Active_Chemists"].ToString() ;
                        if (Convert.ToInt32(drSF["Active_Chemists"].ToString()) > 0)
                        {
                            //hyp_det_chem_Act.NavigateUrl = "rptMRStatus.aspx?sf_code=" + drSF["sf_code"].ToString() + "&sf_name=" + drSF["sf_name"].ToString() + "&type=4&status=0";
                            sURL = "rptMRStatus.aspx?sf_code=" + drSF["sf_code"].ToString() + "&sf_name=" + drSF["sf_name"].ToString() + "&type=4&status=0";
                            hyp_det_chem_Act.Attributes["onclick"] = "javascript:window.open('" + sURL + "',null,'resizable=yes,scrollbars=1,toolbar=no,menubar=no,status=no,width=500,height=600,left=0,top=0');";
                            hyp_det_chem_Act.NavigateUrl = "#";
                        }
                        else
                        {
                            hyp_det_chem_Act.Text = " - ";
                        }

                        tc_det_chem_Act.BorderStyle = BorderStyle.Solid;
                        tc_det_chem_Act.BorderWidth = 1;
                        tc_det_chem_Act.Style.Add("font-family", "Calibri");
                        tc_det_chem_Act.Style.Add("font-size", "10pt");
                        tc_det_chem_Act.Controls.Add(hyp_det_chem_Act);
                        tr_det.Cells.Add(tc_det_chem_Act);

                        iChemistActiveCount += Convert.ToInt16(hyp_det_chem_Act.Text.Replace("-", "0"));

                        //Chemists - DeActive
                        TableCell tc_det_chem_DeAct = new TableCell();
                        HyperLink hyp_det_chem_DeAct = new HyperLink();
                        hyp_det_chem_DeAct.Text =  drSF["DeActive_Chemists"].ToString() ;
                        if (Convert.ToInt32(drSF["DeActive_Chemists"].ToString()) > 0)
                        {
                            //hyp_det_chem_DeAct.NavigateUrl = "rptMRStatus.aspx?sf_code=" + drSF["sf_code"].ToString() + "&sf_name=" + drSF["sf_name"].ToString() + "&type=4&status=1";
                            sURL = "rptMRStatus.aspx?sf_code=" + drSF["sf_code"].ToString() + "&sf_name=" + drSF["sf_name"].ToString() + "&type=4&status=1";
                            hyp_det_chem_DeAct.Attributes["onclick"] = "javascript:window.open('" + sURL + "',null,'resizable=yes,scrollbars=1,toolbar=no,menubar=no,status=no,width=500,height=600,left=0,top=0');";
                            hyp_det_chem_DeAct.NavigateUrl = "#";

                        }
                        else
                        {
                            hyp_det_chem_DeAct.Text = " - ";
                        }
                        tc_det_chem_DeAct.BorderStyle = BorderStyle.Solid;
                        tc_det_chem_DeAct.BorderWidth = 1;
                        tc_det_chem_DeAct.Controls.Add(hyp_det_chem_DeAct);
                        tr_det.Cells.Add(tc_det_chem_DeAct);

                        iChemistDActiveCount += Convert.ToInt16(hyp_det_chem_DeAct.Text.Replace("-", "0"));

                        //Stockiest - Active
                        TableCell tc_det_stok_Act = new TableCell();
                        HyperLink hyp_det_stok_Act = new HyperLink();
                        hyp_det_stok_Act.Text =  drSF["Active_Stockiest"].ToString() ;
                        if (Convert.ToInt32(drSF["Active_Stockiest"].ToString()) > 0)
                        {
                            //hyp_det_stok_Act.NavigateUrl = "rptMRStatus.aspx?sf_code=" + drSF["sf_code"].ToString() + "&sf_name=" + drSF["sf_name"].ToString() + "&type=5&status=0";
                            sURL = "rptMRStatus.aspx?sf_code=" + drSF["sf_code"].ToString() + "&sf_name=" + drSF["sf_name"].ToString() + "&type=5&status=0";
                            hyp_det_stok_Act.Attributes["onclick"] = "javascript:window.open('" + sURL + "',null,'resizable=yes,scrollbars=1,toolbar=no,menubar=no,status=no,width=500,height=600,left=0,top=0');";
                            hyp_det_stok_Act.NavigateUrl = "#";

                        }
                        else
                        {
                            hyp_det_stok_Act.Text = " - ";
                        }
                        tc_det_stok_Act.BorderStyle = BorderStyle.Solid;
                        tc_det_stok_Act.BorderWidth = 1;
                        tc_det_stok_Act.Style.Add("font-family", "Calibri");
                        tc_det_stok_Act.Style.Add("font-size", "10pt");
                        tc_det_stok_Act.Controls.Add(hyp_det_stok_Act);
                        tr_det.Cells.Add(tc_det_stok_Act);

                        iSockistActiveCount += Convert.ToInt16(hyp_det_stok_Act.Text.Replace("-", "0"));

                        //Stockiest - DeActive
                        TableCell tc_det_stok_DeAct = new TableCell();
                        HyperLink hyp_det_stok_DeAct = new HyperLink();
                        hyp_det_stok_DeAct.Text = drSF["DeActive_Stockiest"].ToString() ;
                        if (Convert.ToInt32(drSF["DeActive_Stockiest"].ToString()) > 0)
                        {
                            //hyp_det_stok_DeAct.NavigateUrl = "rptMRStatus.aspx?sf_code=" + drSF["sf_code"].ToString() + "&sf_name=" + drSF["sf_name"].ToString() + "&type=5&status=1";
                            sURL = "rptMRStatus.aspx?sf_code=" + drSF["sf_code"].ToString() + "&sf_name=" + drSF["sf_name"].ToString() + "&type=5&status=1";
                            hyp_det_stok_DeAct.Attributes["onclick"] = "javascript:window.open('" + sURL + "',null,'resizable=yes,scrollbars=1,toolbar=no,menubar=no,status=no,width=500,height=600,left=0,top=0');";
                            hyp_det_stok_DeAct.NavigateUrl = "#";

                        }
                        else
                        {
                            hyp_det_stok_DeAct.Text = " - ";
                        }
                        tc_det_stok_DeAct.BorderStyle = BorderStyle.Solid;
                        tc_det_stok_DeAct.BorderWidth = 1;
                        tc_det_stok_DeAct.Style.Add("font-family", "Calibri");
                        tc_det_stok_DeAct.Style.Add("font-size", "10pt");
                        tc_det_stok_DeAct.Controls.Add(hyp_det_stok_DeAct);
                        tr_det.Cells.Add(tc_det_stok_DeAct);

                        iSockistDActiveCount += Convert.ToInt16(hyp_det_stok_DeAct.Text.Replace("-", "0"));

                        tbl.Rows.Add(tr_det);
                    }
                }

                Session["Sf_Code_multiple"] = strSf_Code.Remove(strSf_Code.Length - 1);

                TableRow tr_catg_total = new TableRow();
                tr_catg_total.BackColor = System.Drawing.Color.White;
                tr_catg_total.HorizontalAlign = HorizontalAlign.Center;

                TableCell tc_catg_Total = new TableCell();
                tc_catg_Total.BorderStyle = BorderStyle.Solid;
                tc_catg_Total.BorderWidth = 1;
                Literal lit_catg_Total = new Literal();
                lit_catg_Total.Text = "<center>Total</center>";
                tc_catg_Total.Controls.Add(lit_catg_Total);
                tc_catg_Total.ColumnSpan = 4;
                tc_catg_Total.Style.Add("font-family", "Calibri");
                tc_catg_Total.Style.Add("font-size", "10pt");
                tr_catg_total.Cells.Add(tc_catg_Total);

                // Territory Active

                TableCell tc_catg_TotalValue = new TableCell();
                tc_catg_TotalValue.BorderStyle = BorderStyle.Solid;
                tc_catg_TotalValue.BorderWidth = 1;
                //Literal lit_catg_TotalValue = new Literal();
                //lit_catg_Total.Text = iActiveCount.ToString();
                HyperLink hyp_det_Terr_Act_Total = new HyperLink();
                hyp_det_Terr_Act_Total.Text = iActiveCount.ToString();
                if (Convert.ToInt32(iActiveCount.ToString()) > 0)
                {
                    //hyp_det_Terr_Act.NavigateUrl = "rptMRStatus.aspx?sf_code=" + drSF["sf_code"].ToString() + "&sf_name=" + drSF["sf_name"].ToString() + "&type=1&status=0";
                    sURL = "rptMRStatus.aspx?sf_code=" + 0 + "&sf_name=" + 0 + "&type=1&status=0" + "&div_code=" + div_code + "";
                    hyp_det_Terr_Act_Total.Attributes["onclick"] = "javascript:window.open('" + sURL + "',null,'resizable=yes,scrollbars=1,toolbar=no,menubar=no,status=no,width=500,height=600,left=0,top=0');";
                    hyp_det_Terr_Act_Total.NavigateUrl = "#";
                }
                else
                {
                    hyp_det_Terr_Act_Total.Text = " - ";
                }
                tc_catg_TotalValue.Controls.Add(hyp_det_Terr_Act_Total);
                tc_catg_TotalValue.Style.Add("font-family", "Calibri");
                tc_catg_TotalValue.Style.Add("font-size", "10pt");
                tr_catg_total.Cells.Add(tc_catg_TotalValue);

                // Territory Active

                // Territory DeActive
                TableCell tc_catg_DeActiveTotalValue = new TableCell();
                tc_catg_DeActiveTotalValue.BorderStyle = BorderStyle.Solid;
                tc_catg_DeActiveTotalValue.BorderWidth = 1;
                //Literal lit_catg_DeActiveTotalValue = new Literal();
                //lit_catg_DeActiveTotalValue.Text = IDActiveCount.ToString();
                HyperLink hyp_det_Terr_DeAct_Total = new HyperLink();
                hyp_det_Terr_DeAct_Total.Text = IDActiveCount.ToString();
                if (Convert.ToInt32(IDActiveCount.ToString()) > 0)
                {
                    //hyp_det_Terr_Act.NavigateUrl = "rptMRStatus.aspx?sf_code=" + drSF["sf_code"].ToString() + "&sf_name=" + drSF["sf_name"].ToString() + "&type=1&status=0";
                    sURL = "rptMRStatus.aspx?sf_code=" + 0 + "&sf_name=" + 0 + "&type=1&status=1" + "&div_code=" + div_code + "";
                    hyp_det_Terr_DeAct_Total.Attributes["onclick"] = "javascript:window.open('" + sURL + "',null,'resizable=yes,scrollbars=1,toolbar=no,menubar=no,status=no,width=500,height=600,left=0,top=0');";
                    hyp_det_Terr_DeAct_Total.NavigateUrl = "#";
                }
                else
                {
                    hyp_det_Terr_DeAct_Total.Text = " - ";
                }
                tc_catg_DeActiveTotalValue.Controls.Add(hyp_det_Terr_DeAct_Total);
                tc_catg_DeActiveTotalValue.Style.Add("font-family", "Calibri");
                tc_catg_DeActiveTotalValue.Style.Add("font-size", "10pt");
                tr_catg_total.Cells.Add(tc_catg_DeActiveTotalValue);
                // Territory DeActive

                // LstDrs Active

                TableCell tc_catg_ActiveLstDrsTotal = new TableCell();
                tc_catg_ActiveLstDrsTotal.BorderStyle = BorderStyle.Solid;
                tc_catg_ActiveLstDrsTotal.BorderWidth = 1;
                //Literal lit_catg_ActiveLstDrsTotal = new Literal();
                //lit_catg_ActiveLstDrsTotal.Text = iLstActiveCount.ToString();
                HyperLink hyp_det_Terr_ActLst_Total = new HyperLink();
                hyp_det_Terr_ActLst_Total.Text = iLstActiveCount.ToString();
                if (Convert.ToInt32(iLstActiveCount.ToString()) > 0)
                {
                    //hyp_det_Terr_Act.NavigateUrl = "rptMRStatus.aspx?sf_code=" + drSF["sf_code"].ToString() + "&sf_name=" + drSF["sf_name"].ToString() + "&type=1&status=0";
                    sURL = "rptMRStatus.aspx?sf_code=" + 0 + "&sf_name=" + 0 + "&type=2&status=0" + "&div_code=" + div_code + "";
                    hyp_det_Terr_ActLst_Total.Attributes["onclick"] = "javascript:window.open('" + sURL + "',null,'resizable=yes,scrollbars=1,toolbar=no,menubar=no,status=no,width=500,height=600,left=0,top=0');";
                    hyp_det_Terr_ActLst_Total.NavigateUrl = "#";
                }
                else
                {
                    hyp_det_Terr_ActLst_Total.Text = " - ";
                }
                tc_catg_ActiveLstDrsTotal.Controls.Add(hyp_det_Terr_ActLst_Total);
                tc_catg_ActiveLstDrsTotal.Style.Add("font-family", "Calibri");
                tc_catg_ActiveLstDrsTotal.Style.Add("font-size", "10pt");
                tr_catg_total.Cells.Add(tc_catg_ActiveLstDrsTotal);
                // LstDrs Active

                // LstDrs DeActive
                TableCell tc_catg_DeActiveLstDrsTotal = new TableCell();
                tc_catg_DeActiveLstDrsTotal.BorderStyle = BorderStyle.Solid;
                tc_catg_DeActiveLstDrsTotal.BorderWidth = 1;
                //Literal lit_catg_DeActiveLstDrsTotal = new Literal();
                //lit_catg_DeActiveLstDrsTotal.Text = iLstDActiveCount.ToString();

                HyperLink hyp_det_Terr_DeAvtLst_Total = new HyperLink();
                hyp_det_Terr_DeAvtLst_Total.Text = iLstDActiveCount.ToString();
                if (Convert.ToInt32(iLstDActiveCount.ToString()) > 0)
                {
                    //hyp_det_Terr_Act.NavigateUrl = "rptMRStatus.aspx?sf_code=" + drSF["sf_code"].ToString() + "&sf_name=" + drSF["sf_name"].ToString() + "&type=1&status=0";
                    sURL = "rptMRStatus.aspx?sf_code=" + 0 + "&sf_name=" + 0 + "&type=2&status=1" + "&div_code=" + div_code + "";
                    hyp_det_Terr_DeAvtLst_Total.Attributes["onclick"] = "javascript:window.open('" + sURL + "',null,'resizable=yes,scrollbars=1,toolbar=no,menubar=no,status=no,width=500,height=600,left=0,top=0');";
                    hyp_det_Terr_DeAvtLst_Total.NavigateUrl = "#";
                }
                else
                {
                    hyp_det_Terr_DeAvtLst_Total.Text = " - ";
                }
                tc_catg_DeActiveLstDrsTotal.Controls.Add(hyp_det_Terr_DeAvtLst_Total);
                tc_catg_DeActiveLstDrsTotal.Style.Add("font-family", "Calibri");
                tc_catg_DeActiveLstDrsTotal.Style.Add("font-size", "10pt");
                tr_catg_total.Cells.Add(tc_catg_DeActiveLstDrsTotal);
                // LstDrs DeActive

                // UnLstDrs Active
                TableCell tc_catg_ActiveUnLstDrsTotal = new TableCell();
                tc_catg_ActiveUnLstDrsTotal.BorderStyle = BorderStyle.Solid;
                tc_catg_ActiveUnLstDrsTotal.BorderWidth = 1;
                //Literal lit_catg_ActiveUnLstDrsTotal = new Literal();
                //lit_catg_ActiveUnLstDrsTotal.Text = iUnLstActiveCount.ToString();
                HyperLink hyp_det_Terr_ActUnLst_Total = new HyperLink();
                hyp_det_Terr_ActUnLst_Total.Text = iUnLstActiveCount.ToString();
                if (Convert.ToInt32(iUnLstActiveCount.ToString()) > 0)
                {
                    //hyp_det_Terr_Act.NavigateUrl = "rptMRStatus.aspx?sf_code=" + drSF["sf_code"].ToString() + "&sf_name=" + drSF["sf_name"].ToString() + "&type=1&status=0";
                    sURL = "rptMRStatus.aspx?sf_code=" + 0 + "&sf_name=" + 0 + "&type=3&status=0" + "&div_code=" + div_code + "";
                    hyp_det_Terr_ActUnLst_Total.Attributes["onclick"] = "javascript:window.open('" + sURL + "',null,'resizable=yes,scrollbars=1,toolbar=no,menubar=no,status=no,width=500,height=600,left=0,top=0');";
                    hyp_det_Terr_ActUnLst_Total.NavigateUrl = "#";
                }
                else
                {
                    hyp_det_Terr_ActUnLst_Total.Text = " - ";
                }
                tc_catg_ActiveUnLstDrsTotal.Controls.Add(hyp_det_Terr_ActUnLst_Total);
                tc_catg_ActiveUnLstDrsTotal.Style.Add("font-family", "Calibri");
                tc_catg_ActiveUnLstDrsTotal.Style.Add("font-size", "10pt");
                tr_catg_total.Cells.Add(tc_catg_ActiveUnLstDrsTotal);
                // UnLstDrs Active

                // UnLstDrs DeActive
                TableCell tc_catg_DeActiveUnLstDrsTotal = new TableCell();
                tc_catg_DeActiveUnLstDrsTotal.BorderStyle = BorderStyle.Solid;
                tc_catg_DeActiveUnLstDrsTotal.BorderWidth = 1;
                //Literal lit_catg_DeActiveUnLstDrsTotal = new Literal();
                //lit_catg_DeActiveUnLstDrsTotal.Text = iUnLstDActiveCount.ToString();
                HyperLink hyp_det_Terr_DeActUnLst_Total = new HyperLink();
                hyp_det_Terr_DeActUnLst_Total.Text = iUnLstDActiveCount.ToString();
                if (Convert.ToInt32(iUnLstDActiveCount.ToString()) > 0)
                {
                    //hyp_det_Terr_Act.NavigateUrl = "rptMRStatus.aspx?sf_code=" + drSF["sf_code"].ToString() + "&sf_name=" + drSF["sf_name"].ToString() + "&type=1&status=0";
                    sURL = "rptMRStatus.aspx?sf_code=" + 0 + "&sf_name=" + 0 + "&type=3&status=1" + "&div_code=" + div_code + "";
                    hyp_det_Terr_DeActUnLst_Total.Attributes["onclick"] = "javascript:window.open('" + sURL + "',null,'resizable=yes,scrollbars=1,toolbar=no,menubar=no,status=no,width=500,height=600,left=0,top=0');";
                    hyp_det_Terr_DeActUnLst_Total.NavigateUrl = "#";
                }
                else
                {
                    hyp_det_Terr_DeActUnLst_Total.Text = " - ";
                }
                tc_catg_DeActiveUnLstDrsTotal.Controls.Add(hyp_det_Terr_DeActUnLst_Total);
                tc_catg_DeActiveUnLstDrsTotal.Style.Add("font-family", "Calibri");
                tc_catg_DeActiveUnLstDrsTotal.Style.Add("font-size", "10pt");
                tr_catg_total.Cells.Add(tc_catg_DeActiveUnLstDrsTotal);
                // UnLstDrs DeActive

                // Chemist Active
                TableCell tc_catg_ActiveChemistTotal = new TableCell();
                tc_catg_ActiveChemistTotal.BorderStyle = BorderStyle.Solid;
                tc_catg_ActiveChemistTotal.BorderWidth = 1;
                //Literal lit_catg_ActiveChemistTotal = new Literal();
                //lit_catg_ActiveChemistTotal.Text = iChemistActiveCount.ToString();
                HyperLink hyp_det_Terr_ActChem_Total = new HyperLink();
                hyp_det_Terr_ActChem_Total.Text = iChemistActiveCount.ToString();
                if (Convert.ToInt32(iChemistActiveCount.ToString()) > 0)
                {
                    //hyp_det_Terr_Act.NavigateUrl = "rptMRStatus.aspx?sf_code=" + drSF["sf_code"].ToString() + "&sf_name=" + drSF["sf_name"].ToString() + "&type=1&status=0";
                    sURL = "rptMRStatus.aspx?sf_code=" + 0 + "&sf_name=" + 0 + "&type=4&status=0" + "&div_code=" + div_code + "";
                    hyp_det_Terr_ActChem_Total.Attributes["onclick"] = "javascript:window.open('" + sURL + "',null,'resizable=yes,scrollbars=1,toolbar=no,menubar=no,status=no,width=500,height=600,left=0,top=0');";
                    hyp_det_Terr_ActChem_Total.NavigateUrl = "#";
                }
                else
                {
                    hyp_det_Terr_ActChem_Total.Text = " - ";
                }
                tc_catg_ActiveChemistTotal.Controls.Add(hyp_det_Terr_ActChem_Total);
                tc_catg_ActiveChemistTotal.Style.Add("font-family", "Calibri");
                tc_catg_ActiveChemistTotal.Style.Add("font-size", "10pt");
                tr_catg_total.Cells.Add(tc_catg_ActiveChemistTotal);
                // Chemist Active

                // Chemist DeActive
                TableCell tc_catg_DeActiveChemistTotal = new TableCell();
                tc_catg_DeActiveChemistTotal.BorderStyle = BorderStyle.Solid;
                tc_catg_DeActiveChemistTotal.BorderWidth = 1;
                //Literal lit_catg_DeActiveChemistTotal = new Literal();
                //lit_catg_DeActiveChemistTotal.Text = iChemistDActiveCount.ToString();
                HyperLink hyp_det_Terr_DeActChem_Total = new HyperLink();
                hyp_det_Terr_DeActChem_Total.Text = iChemistDActiveCount.ToString();
                if (Convert.ToInt32(iChemistDActiveCount.ToString()) > 0)
                {
                    //hyp_det_Terr_Act.NavigateUrl = "rptMRStatus.aspx?sf_code=" + drSF["sf_code"].ToString() + "&sf_name=" + drSF["sf_name"].ToString() + "&type=1&status=0";
                    sURL = "rptMRStatus.aspx?sf_code=" + 0 + "&sf_name=" + 0 + "&type=4&status=1" + "&div_code=" + div_code + "";
                    hyp_det_Terr_DeActChem_Total.Attributes["onclick"] = "javascript:window.open('" + sURL + "',null,'resizable=yes,scrollbars=1,toolbar=no,menubar=no,status=no,width=500,height=600,left=0,top=0');";
                    hyp_det_Terr_DeActChem_Total.NavigateUrl = "#";
                }
                else
                {
                    hyp_det_Terr_DeActChem_Total.Text = " - ";
                }
                tc_catg_DeActiveChemistTotal.Controls.Add(hyp_det_Terr_DeActChem_Total);
                tc_catg_DeActiveChemistTotal.Style.Add("font-family", "Calibri");
                tc_catg_DeActiveChemistTotal.Style.Add("font-size", "10pt");
                tr_catg_total.Cells.Add(tc_catg_DeActiveChemistTotal);
                // Chemist DeActive

                // Stockist Active
                TableCell tc_catg_ActiveStockistTotal = new TableCell();
                tc_catg_ActiveStockistTotal.BorderStyle = BorderStyle.Solid;
                tc_catg_ActiveStockistTotal.BorderWidth = 1;
                //Literal lit_catg_ActiveStockistTotal = new Literal();
                //lit_catg_ActiveStockistTotal.Text = iSockistActiveCount.ToString();
                HyperLink hyp_det_Terr_ActStockist_Total = new HyperLink();
                hyp_det_Terr_ActStockist_Total.Text = iSockistActiveCount.ToString();
                if (Convert.ToInt32(iSockistActiveCount.ToString()) > 0)
                {
                    //hyp_det_Terr_Act.NavigateUrl = "rptMRStatus.aspx?sf_code=" + drSF["sf_code"].ToString() + "&sf_name=" + drSF["sf_name"].ToString() + "&type=1&status=0";
                    sURL = "rptMRStatus.aspx?sf_code=" + 0 + "&sf_name=" + 0 + "&type=5&status=0" + "&div_code=" + div_code + "";
                    hyp_det_Terr_ActStockist_Total.Attributes["onclick"] = "javascript:window.open('" + sURL + "',null,'resizable=yes,scrollbars=1,toolbar=no,menubar=no,status=no,width=500,height=600,left=0,top=0');";
                    hyp_det_Terr_ActStockist_Total.NavigateUrl = "#";
                }
                else
                {
                    hyp_det_Terr_ActStockist_Total.Text = " - ";
                }
                tc_catg_ActiveStockistTotal.Controls.Add(hyp_det_Terr_ActStockist_Total);
                tc_catg_ActiveStockistTotal.Style.Add("font-family", "Calibri");
                tc_catg_ActiveStockistTotal.Style.Add("font-size", "10pt");
                tr_catg_total.Cells.Add(tc_catg_ActiveStockistTotal);
                // Stockist Active

                // Stockist DeActive
                TableCell tc_catg_DeActiveStockistTotal = new TableCell();
                tc_catg_DeActiveStockistTotal.BorderStyle = BorderStyle.Solid;
                tc_catg_DeActiveStockistTotal.BorderWidth = 1;
                //Literal lit_catg_DeActiveStockistTotal = new Literal();
                //lit_catg_DeActiveStockistTotal.Text = iSockistDActiveCount.ToString();
                HyperLink hyp_det_Terr_DeActStockist_Total = new HyperLink();
                hyp_det_Terr_DeActStockist_Total.Text = iSockistDActiveCount.ToString();
                if (Convert.ToInt32(iSockistDActiveCount.ToString()) > 0)
                {
                    //hyp_det_Terr_Act.NavigateUrl = "rptMRStatus.aspx?sf_code=" + drSF["sf_code"].ToString() + "&sf_name=" + drSF["sf_name"].ToString() + "&type=1&status=0";
                    sURL = "rptMRStatus.aspx?sf_code=" + 0 + "&sf_name=" + 0 + "&type=5&status=1" + "&div_code=" + div_code + "";
                    hyp_det_Terr_DeActStockist_Total.Attributes["onclick"] = "javascript:window.open('" + sURL + "',null,'resizable=yes,scrollbars=1,toolbar=no,menubar=no,status=no,width=500,height=600,left=0,top=0');";
                    hyp_det_Terr_DeActStockist_Total.NavigateUrl = "#";
                }
                else
                {
                    hyp_det_Terr_DeActStockist_Total.Text = " - ";
                }
                tc_catg_DeActiveStockistTotal.Controls.Add(hyp_det_Terr_DeActStockist_Total);
                tc_catg_DeActiveStockistTotal.Style.Add("font-family", "Calibri");
                tc_catg_DeActiveStockistTotal.Style.Add("font-size", "10pt");
                tr_catg_total.Cells.Add(tc_catg_DeActiveStockistTotal);
                // Stockist DeActive

                tbl.Rows.Add(tr_catg_total);

            }
            else
            {
                int iCount = 0;
                dsSalesForce = (DataSet)ViewState["dsSalesForce"];
                foreach (DataRow drFF in dsSalesForce.Tables[0].Rows)
                {

                    //if (ViewState["HQ_Det"] != null)
                    //{
                    //    if (ViewState["HQ_Det"].ToString() == drFF["sf_hq"].ToString())
                    //    {
                    //        TableRow tr_HQ_det = new TableRow();
                    //        TableCell tc_HQ_det = new TableCell();
                    //        Literal lit_HQ_det = new Literal();
                    //        lit_HQ_det.Text = drFF["sf_hq"].ToString();
                    //        tc_HQ_det.BorderStyle = BorderStyle.Solid;
                    //        tc_HQ_det.BorderWidth = 1;
                    //        tc_HQ_det.Controls.Add(lit_HQ_det);
                    //        tc_HQ_det.ColumnSpan = 13;
                    //        tc_HQ_det.BackColor = System.Drawing.Color.Bisque;
                    //        tr_HQ_det.Cells.Add(tc_HQ_det);
                    //        tbl.Rows.Add(tr_HQ_det);
                    //        ViewState["HQ_Det"] = drFF["sf_hq"].ToString();
                    //    }
                    //}
                    //else
                    //{
                    //    TableRow tr_HQ_det = new TableRow();
                    //    TableCell tc_HQ_det = new TableCell();
                    //    Literal lit_HQ_det = new Literal();
                    //    lit_HQ_det.Text = drFF["sf_hq"].ToString();
                    //    tc_HQ_det.BorderStyle = BorderStyle.Solid;
                    //    tc_HQ_det.BorderWidth = 1;
                    //    tc_HQ_det.Controls.Add(lit_HQ_det);
                    //    tc_HQ_det.ColumnSpan = 13;
                    //    tc_HQ_det.BackColor = System.Drawing.Color.Bisque;
                    //    tr_HQ_det.Cells.Add(tc_HQ_det);
                    //    tbl.Rows.Add(tr_HQ_det);
                    //    ViewState["HQ_Det"] = drFF["sf_hq"].ToString();
                    //}

                    //ViewState["HQ_Det"] = drFF["sf_hq"].ToString();
                    

                    TableRow tr_det = new TableRow();
                    tr_det.HorizontalAlign = HorizontalAlign.Center;
                    iCount += 1;

                    strSf_Code += "'" + drFF["sf_code"].ToString() + "'" + ",";

                    //S.No
                    TableCell tc_det_SNo = new TableCell();
                    Literal lit_det_SNo = new Literal();
                    lit_det_SNo.Text = "<center>" + iCount.ToString() + "</center>";
                    tc_det_SNo.BorderStyle = BorderStyle.Solid;
                    tc_det_SNo.BorderWidth = 1;
                    tc_det_SNo.Controls.Add(lit_det_SNo);
                    tr_det.Cells.Add(tc_det_SNo);
                    tr_det.BackColor = System.Drawing.Color.White; 
                    //SF User Name
                    TableCell tc_det_usr = new TableCell();
                    Literal lit_det_usr = new Literal();
                    lit_det_usr.Text = "&nbsp;" + drFF["sf_username"].ToString();
                    tc_det_usr.BorderStyle = BorderStyle.Solid;
                    tc_det_usr.BorderWidth = 1;
                    tc_det_usr.Visible = false;
                    tc_det_usr.Style.Add("font-family", "Calibri");
                    tc_det_usr.Style.Add("font-size", "10pt");
                    tc_det_usr.Style.Add("text-align", "left");
                    tc_det_usr.Controls.Add(lit_det_usr);
                    tr_det.Cells.Add(tc_det_usr);

                    //SF Name
                    TableCell tc_det_FF = new TableCell();
                    Literal lit_det_FF = new Literal();
                    lit_det_FF.Text = "&nbsp;" + drFF["sf_name"].ToString();
                    tc_det_FF.BorderStyle = BorderStyle.Solid;
                    tc_det_FF.BorderWidth = 1;
                    tc_det_FF.Style.Add("font-family", "Calibri");
                    tc_det_FF.Style.Add("font-size", "10pt");
                    tc_det_FF.Style.Add("text-align", "left");
                    tc_det_FF.Controls.Add(lit_det_FF);
                    tr_det.Cells.Add(tc_det_FF);

                    //SF Designation Short Name
                    TableCell tc_det_Designation = new TableCell();
                    Literal lit_det_Designation = new Literal();
                    lit_det_Designation.Text = "&nbsp;" + drFF["sf_Designation_Short_Name"].ToString();
                    tc_det_Designation.BorderStyle = BorderStyle.Solid;
                    tc_det_Designation.BorderWidth = 1;
                    tc_det_Designation.Style.Add("font-family", "Calibri");
                    tc_det_Designation.Style.Add("font-size", "10pt");
                    tc_det_Designation.Style.Add("text-align", "left");
                    tc_det_Designation.Controls.Add(lit_det_Designation);
                    tr_det.Cells.Add(tc_det_Designation);

                    //hq
                    TableCell tc_det_hq = new TableCell();
                    Literal lit_det_hq = new Literal();
                    lit_det_hq.Text = "&nbsp;" + drFF["sf_hq"].ToString();
                    tc_det_hq.BorderStyle = BorderStyle.Solid;
                    tc_det_hq.BorderWidth = 1;
                    tc_det_hq.Style.Add("font-family", "Calibri");
                    tc_det_hq.Style.Add("font-size", "10pt");
                    tc_det_hq.Style.Add("text-align", "left");
                    tc_det_hq.Controls.Add(lit_det_hq);
                    tr_det.Cells.Add(tc_det_hq);

                    //Territory - Active 
                    TableCell tc_det_Terr_Act = new TableCell();
                    HyperLink hyp_det_Terr_Act = new HyperLink();
                    hyp_det_Terr_Act.Text =  drFF["Active_Territory"].ToString() ;
                    if (Convert.ToInt32(drFF["Active_Territory"].ToString()) > 0)
                    {
                        //hyp_det_Terr_Act.NavigateUrl = "rptMRStatus.aspx?sf_code=" + drFF["sf_code"].ToString() + "&sf_name=" + drFF["sf_name"].ToString() + "&type=1&status=0";
                        sURL = "rptMRStatus.aspx?sf_code=" + drFF["sf_code"].ToString() + "&sf_name=" + drFF["sf_name"].ToString() + "&type=1&status=0";
                        hyp_det_Terr_Act.Attributes["onclick"] = "javascript:window.open('" + sURL + "',null,'resizable=yes,scrollbars=1,toolbar=no,menubar=no,status=no,width=500,height=600,left=0,top=0');";
                        hyp_det_Terr_Act.NavigateUrl = "#";

                    }
                    else
                    {
                        hyp_det_Terr_Act.Text = " - ";
                    }
                    tc_det_Terr_Act.BorderStyle = BorderStyle.Solid;
                    tc_det_Terr_Act.BorderWidth = 1;
                    tc_det_Terr_Act.Style.Add("font-family", "Calibri");
                    tc_det_Terr_Act.Style.Add("font-size", "10pt");
                  
                    tc_det_Terr_Act.Controls.Add(hyp_det_Terr_Act);
                    tr_det.Cells.Add(tc_det_Terr_Act);

                    iActiveCount += Convert.ToInt16(hyp_det_Terr_Act.Text.Replace("-", "0"));

                    //Territory - De-Active 
                    TableCell tc_det_Terr_DeAct = new TableCell();
                    HyperLink hyp_det_Terr_DeAct = new HyperLink();
                    hyp_det_Terr_DeAct.Text = drFF["DeActive_Territory"].ToString() ;
                    if (Convert.ToInt32(drFF["DeActive_Territory"].ToString()) > 0)
                    {
                        //hyp_det_Terr_DeAct.NavigateUrl = "rptMRStatus.aspx?sf_code=" + drFF["sf_code"].ToString() + "&sf_name=" + drFF["sf_name"].ToString() + "&type=1&status=1";
                        sURL = "rptMRStatus.aspx?sf_code=" + drFF["sf_code"].ToString() + "&sf_name=" + drFF["sf_name"].ToString() + "&type=1&status=1";
                        hyp_det_Terr_DeAct.Attributes["onclick"] = "javascript:window.open('" + sURL + "',null,'resizable=yes,scrollbars=1,toolbar=no,menubar=no,status=no,width=500,height=600,left=0,top=0');";
                        hyp_det_Terr_DeAct.NavigateUrl = "#";

                    }
                    else
                    {
                        hyp_det_Terr_DeAct.Text = " - ";
                    }
                    tc_det_Terr_DeAct.BorderStyle = BorderStyle.Solid;
                    tc_det_Terr_DeAct.BorderWidth = 1;
                    tc_det_Terr_DeAct.Style.Add("font-family", "Calibri");
                    tc_det_Terr_DeAct.Style.Add("font-size", "10pt");
                    tc_det_Terr_DeAct.Controls.Add(hyp_det_Terr_DeAct);
                    tr_det.Cells.Add(tc_det_Terr_DeAct);

                    IDActiveCount += Convert.ToInt16(hyp_det_Terr_DeAct.Text.Replace("-", "0"));

                    //Listed Doctor - Active
                    TableCell tc_det_lst_Act = new TableCell();
                    HyperLink hyp_det_lst_Act = new HyperLink();
                    hyp_det_lst_Act.Text =  drFF["Active_ListedDR"].ToString() ;
                    if (Convert.ToInt32(drFF["Active_ListedDR"].ToString()) > 0)
                    {
                        //hyp_det_lst_Act.NavigateUrl = "rptMRStatus.aspx?sf_code=" + drFF["sf_code"].ToString() + "&sf_name=" + drFF["sf_name"].ToString() + "&type=2&status=0";
                        sURL = "rptMRStatus.aspx?sf_code=" + drFF["sf_code"].ToString() + "&sf_name=" + drFF["sf_name"].ToString() + "&type=2&status=0";
                        hyp_det_lst_Act.Attributes["onclick"] = "javascript:window.open('" + sURL + "',null,'resizable=yes,scrollbars=1,toolbar=no,menubar=no,status=no,width=500,height=600,left=0,top=0');";
                        hyp_det_lst_Act.NavigateUrl = "#";

                    }
                    else
                    {
                        hyp_det_lst_Act.Text = " - ";
                    }
                    tc_det_lst_Act.BorderStyle = BorderStyle.Solid;
                    tc_det_lst_Act.BorderWidth = 1;
                    tc_det_lst_Act.Style.Add("font-family", "Calibri");
                    tc_det_lst_Act.Style.Add("font-size", "10pt");
                    tc_det_lst_Act.Controls.Add(hyp_det_lst_Act);
                    tr_det.Cells.Add(tc_det_lst_Act);

                    iLstActiveCount += Convert.ToInt16(hyp_det_lst_Act.Text.Replace("-", "0"));

                    //Listed Doctor - DeActive
                    TableCell tc_det_lst_DeAct = new TableCell();
                    HyperLink hyp_det_lst_DeAct = new HyperLink();
                    hyp_det_lst_DeAct.Text =  drFF["DeActive_ListedDR"].ToString();
                    if (Convert.ToInt32(drFF["DeActive_ListedDR"].ToString()) > 0)
                    {
                        //hyp_det_lst_DeAct.NavigateUrl = "rptMRStatus.aspx?sf_code=" + drFF["sf_code"].ToString() + "&sf_name=" + drFF["sf_name"].ToString() + "&type=2&status=1";
                        sURL = "rptMRStatus.aspx?sf_code=" + drFF["sf_code"].ToString() + "&sf_name=" + drFF["sf_name"].ToString() + "&type=2&status=1";
                        hyp_det_lst_DeAct.Attributes["onclick"] = "javascript:window.open('" + sURL + "',null,'resizable=yes,scrollbars=1,toolbar=no,menubar=no,status=no,width=500,height=600,left=0,top=0');";
                        hyp_det_lst_DeAct.NavigateUrl = "#";

                    }
                    else
                    {
                        hyp_det_lst_DeAct.Text = " - ";
                    }
                    tc_det_lst_DeAct.BorderStyle = BorderStyle.Solid;
                    tc_det_lst_DeAct.BorderWidth = 1;
                    tc_det_lst_DeAct.Style.Add("font-family", "Calibri");
                    tc_det_lst_DeAct.Style.Add("font-size", "10pt");
                    tc_det_lst_DeAct.Controls.Add(hyp_det_lst_DeAct);
                    tr_det.Cells.Add(tc_det_lst_DeAct);

                    iLstDActiveCount += Convert.ToInt16(hyp_det_lst_DeAct.Text.Replace("-", "0"));

                    //Un-Listed Doctor - Active
                    TableCell tc_det_unlst_Act = new TableCell();
                    HyperLink hyp_det_unlst_Act = new HyperLink();
                    hyp_det_unlst_Act.Text =  drFF["Active_UnListedDR"].ToString();
                    if (Convert.ToInt32(drFF["Active_UnListedDR"].ToString()) > 0)
                    {
                        //hyp_det_unlst_Act.NavigateUrl = "rptMRStatus.aspx?sf_code=" + drFF["sf_code"].ToString() + "&sf_name=" + drFF["sf_name"].ToString() + "&type=3&status=0";
                        sURL = "rptMRStatus.aspx?sf_code=" + drFF["sf_code"].ToString() + "&sf_name=" + drFF["sf_name"].ToString() + "&type=3&status=0";
                        hyp_det_unlst_Act.Attributes["onclick"] = "javascript:window.open('" + sURL + "',null,'resizable=yes,scrollbars=1,toolbar=no,menubar=no,status=no,width=500,height=600,left=0,top=0');";
                        hyp_det_unlst_Act.NavigateUrl = "#";

                    }
                    else
                    {
                        hyp_det_unlst_Act.Text = " - ";
                    }
                    tc_det_unlst_Act.BorderStyle = BorderStyle.Solid;
                    tc_det_unlst_Act.BorderWidth = 1;
                    tc_det_unlst_Act.Style.Add("font-family", "Calibri");
                    tc_det_unlst_Act.Style.Add("font-size", "10pt");
                    tc_det_unlst_Act.Controls.Add(hyp_det_unlst_Act);
                    tr_det.Cells.Add(tc_det_unlst_Act);

                    iUnLstActiveCount += Convert.ToInt16(hyp_det_unlst_Act.Text.Replace("-", "0"));

                    //Un-Listed Doctor - DeActive
                    TableCell tc_det_unlst_DeAct = new TableCell();
                    HyperLink hyp_det_unlst_DeAct = new HyperLink();
                    hyp_det_unlst_DeAct.Text =  drFF["DeActive_UnListedDR"].ToString();
                    if (Convert.ToInt32(drFF["DeActive_UnListedDR"].ToString()) > 0)
                    {
                        //hyp_det_unlst_DeAct.NavigateUrl = "rptMRStatus.aspx?sf_code=" + drFF["sf_code"].ToString() + "&sf_name=" + drFF["sf_name"].ToString() + "&type=3&status=1";
                        sURL = "rptMRStatus.aspx?sf_code=" + drFF["sf_code"].ToString() + "&sf_name=" + drFF["sf_name"].ToString() + "&type=3&status=1";
                        hyp_det_unlst_DeAct.Attributes["onclick"] = "javascript:window.open('" + sURL + "',null,'resizable=yes,scrollbars=1,toolbar=no,menubar=no,status=no,width=500,height=600,left=0,top=0');";
                        hyp_det_unlst_DeAct.NavigateUrl = "#";

                    }
                    else
                    {
                        hyp_det_unlst_DeAct.Text = " - ";
                    }
                    tc_det_unlst_DeAct.BorderStyle = BorderStyle.Solid;
                    tc_det_unlst_DeAct.BorderWidth = 1;
                    tc_det_unlst_DeAct.Style.Add("font-family", "Calibri");
                    tc_det_unlst_DeAct.Style.Add("font-size", "10pt");
                    tc_det_unlst_DeAct.Controls.Add(hyp_det_unlst_DeAct);
                    tr_det.Cells.Add(tc_det_unlst_DeAct);

                    iUnLstDActiveCount += Convert.ToInt16(hyp_det_unlst_DeAct.Text.Replace("-", "0"));

                    //Chemists - Active
                    TableCell tc_det_chem_Act = new TableCell();
                    HyperLink hyp_det_chem_Act = new HyperLink();
                    hyp_det_chem_Act.Text = drFF["Active_Chemists"].ToString();
                    if (Convert.ToInt32(drFF["Active_Chemists"].ToString()) > 0)
                    {
                        //hyp_det_chem_Act.NavigateUrl = "rptMRStatus.aspx?sf_code=" + drFF["sf_code"].ToString() + "&sf_name=" + drFF["sf_name"].ToString() + "&type=4&status=0";
                        sURL = "rptMRStatus.aspx?sf_code=" + drFF["sf_code"].ToString() + "&sf_name=" + drFF["sf_name"].ToString() + "&type=4&status=0";
                        hyp_det_chem_Act.Attributes["onclick"] = "javascript:window.open('" + sURL + "',null,'resizable=yes,scrollbars=1,toolbar=no,menubar=no,status=no,width=500,height=600,left=0,top=0');";
                        hyp_det_chem_Act.NavigateUrl = "#";

                    }
                    else
                    {
                        hyp_det_chem_Act.Text = " - ";
                    }

                    tc_det_chem_Act.BorderStyle = BorderStyle.Solid;
                    tc_det_chem_Act.BorderWidth = 1;
                    tc_det_chem_Act.Style.Add("font-family", "Calibri");
                    tc_det_chem_Act.Style.Add("font-size", "10pt");
                    tc_det_chem_Act.Controls.Add(hyp_det_chem_Act);
                    tr_det.Cells.Add(tc_det_chem_Act);

                    iChemistActiveCount += Convert.ToInt16(hyp_det_chem_Act.Text.Replace("-", "0"));

                    //Chemists - DeActive
                    TableCell tc_det_chem_DeAct = new TableCell();
                    HyperLink hyp_det_chem_DeAct = new HyperLink();
                    hyp_det_chem_DeAct.Text =  drFF["DeActive_Chemists"].ToString();
                    if (Convert.ToInt32(drFF["DeActive_Chemists"].ToString()) > 0)
                    {
                        //hyp_det_chem_DeAct.NavigateUrl = "rptMRStatus.aspx?sf_code=" + drFF["sf_code"].ToString() + "&sf_name=" + drFF["sf_name"].ToString() + "&type=4&status=1";
                        sURL = "rptMRStatus.aspx?sf_code=" + drFF["sf_code"].ToString() + "&sf_name=" + drFF["sf_name"].ToString() + "&type=4&status=1";
                        hyp_det_chem_DeAct.Attributes["onclick"] = "javascript:window.open('" + sURL + "',null,'resizable=yes,scrollbars=1,toolbar=no,menubar=no,status=no,width=500,height=600,left=0,top=0');";
                        hyp_det_chem_DeAct.NavigateUrl = "#";

                    }
                    else
                    {
                        hyp_det_chem_DeAct.Text = " - ";
                    }
                    tc_det_chem_DeAct.BorderStyle = BorderStyle.Solid;
                    tc_det_chem_DeAct.BorderWidth = 1;
                    tc_det_chem_DeAct.Style.Add("font-family", "Calibri");
                    tc_det_chem_DeAct.Style.Add("font-size", "10pt");
                    tc_det_chem_DeAct.Controls.Add(hyp_det_chem_DeAct);
                    tr_det.Cells.Add(tc_det_chem_DeAct);

                    iChemistDActiveCount += Convert.ToInt16(hyp_det_chem_DeAct.Text.Replace("-", "0"));

                    //Stockiest - Active
                    TableCell tc_det_stok_Act = new TableCell();
                    HyperLink hyp_det_stok_Act = new HyperLink();
                    hyp_det_stok_Act.Text = drFF["Active_Stockiest"].ToString();
                    if (Convert.ToInt32(drFF["Active_Stockiest"].ToString()) > 0)
                    {
                        //hyp_det_stok_Act.NavigateUrl = "rptMRStatus.aspx?sf_code=" + drFF["sf_code"].ToString() + "&sf_name=" + drFF["sf_name"].ToString() + "&type=5&status=0";
                        sURL = "rptMRStatus.aspx?sf_code=" + drFF["sf_code"].ToString() + "&sf_name=" + drFF["sf_name"].ToString() + "&type=5&status=0";
                        hyp_det_stok_Act.Attributes["onclick"] = "javascript:window.open('" + sURL + "',null,'resizable=yes,scrollbars=1,toolbar=no,menubar=no,status=no,width=500,height=600,left=0,top=0');";
                        hyp_det_stok_Act.NavigateUrl = "#";

                    }
                    else
                    {
                        hyp_det_stok_Act.Text = " - ";
                    }
                    tc_det_stok_Act.BorderStyle = BorderStyle.Solid;
                    tc_det_stok_Act.BorderWidth = 1;
                    tc_det_stok_Act.Style.Add("font-family", "Calibri");
                    tc_det_stok_Act.Style.Add("font-size", "10pt");
                    tc_det_stok_Act.Controls.Add(hyp_det_stok_Act);
                    tr_det.Cells.Add(tc_det_stok_Act);

                    iSockistActiveCount += Convert.ToInt16(hyp_det_stok_Act.Text.Replace("-", "0"));

                    //Stockiest - DeActive
                    TableCell tc_det_stok_DeAct = new TableCell();
                    HyperLink hyp_det_stok_DeAct = new HyperLink();
                    hyp_det_stok_DeAct.Text =  drFF["DeActive_Stockiest"].ToString();
                    if (Convert.ToInt32(drFF["DeActive_Stockiest"].ToString()) > 0)
                    {
                        //hyp_det_stok_DeAct.NavigateUrl = "rptMRStatus.aspx?sf_code=" + drFF["sf_code"].ToString() + "&sf_name=" + drFF["sf_name"].ToString() + "&type=5&status=1";
                        sURL = "rptMRStatus.aspx?sf_code=" + drFF["sf_code"].ToString() + "&sf_name=" + drFF["sf_name"].ToString() + "&type=5&status=1";
                        hyp_det_stok_DeAct.Attributes["onclick"] = "javascript:window.open('" + sURL + "',null,'resizable=yes,scrollbars=1,toolbar=no,menubar=no,status=no,width=500,height=600,left=0,top=0');";
                        hyp_det_stok_DeAct.NavigateUrl = "#";

                    }
                    else
                    {
                        hyp_det_stok_DeAct.Text = " - ";
                    }
                    tc_det_stok_DeAct.BorderStyle = BorderStyle.Solid;
                    tc_det_stok_DeAct.BorderWidth = 1;
                    tc_det_stok_DeAct.Style.Add("font-family", "Calibri");
                    tc_det_stok_DeAct.Style.Add("font-size", "10pt");
                    tc_det_stok_DeAct.Controls.Add(hyp_det_stok_DeAct);
                    tr_det.Cells.Add(tc_det_stok_DeAct);

                    iSockistDActiveCount += Convert.ToInt16(hyp_det_stok_DeAct.Text.Replace("-", "0"));

                    tbl.Rows.Add(tr_det);
                }

                Session["Sf_Code_multiple"] = strSf_Code.Remove(strSf_Code.Length - 1);

                TableRow tr_catg_total = new TableRow();
                tr_catg_total.BackColor = System.Drawing.Color.White;
                tr_catg_total.HorizontalAlign = HorizontalAlign.Center;

                TableCell tc_catg_Total = new TableCell();
                tc_catg_Total.BorderStyle = BorderStyle.Solid;
                tc_catg_Total.BorderWidth = 1;
                Literal lit_catg_Total = new Literal();
                lit_catg_Total.Text = "<center>Total</center>";
                tc_catg_Total.Controls.Add(lit_catg_Total);
                tc_catg_Total.ColumnSpan = 4;
                tc_catg_Total.Style.Add("font-family", "Calibri");
                tc_catg_Total.Style.Add("font-size", "10pt");
                tr_catg_total.Cells.Add(tc_catg_Total);

                // Territory Active

                TableCell tc_catg_TotalValue = new TableCell();
                tc_catg_TotalValue.BorderStyle = BorderStyle.Solid;
                tc_catg_TotalValue.BorderWidth = 1;
                //Literal lit_catg_TotalValue = new Literal();
                //lit_catg_Total.Text = iActiveCount.ToString();
                HyperLink hyp_det_Terr_Act_Total = new HyperLink();
                hyp_det_Terr_Act_Total.Text = iActiveCount.ToString();
                if (Convert.ToInt32(iActiveCount.ToString()) > 0)
                {
                    //hyp_det_Terr_Act.NavigateUrl = "rptMRStatus.aspx?sf_code=" + drSF["sf_code"].ToString() + "&sf_name=" + drSF["sf_name"].ToString() + "&type=1&status=0";
                    sURL = "rptMRStatus.aspx?sf_code=" + 0 + "&sf_name=" + 0 + "&type=1&status=0" + "&div_code=" + div_code + "";
                    hyp_det_Terr_Act_Total.Attributes["onclick"] = "javascript:window.open('" + sURL + "',null,'resizable=yes,scrollbars=1,toolbar=no,menubar=no,status=no,width=500,height=600,left=0,top=0');";
                    hyp_det_Terr_Act_Total.NavigateUrl = "#";
                }
                else
                {
                    hyp_det_Terr_Act_Total.Text = " - ";
                }
                tc_catg_TotalValue.Controls.Add(hyp_det_Terr_Act_Total);
                tc_catg_TotalValue.Style.Add("font-family", "Calibri");
                tc_catg_TotalValue.Style.Add("font-size", "10pt");
                tr_catg_total.Cells.Add(tc_catg_TotalValue);

                // Territory Active

                // Territory DeActive
                TableCell tc_catg_DeActiveTotalValue = new TableCell();
                tc_catg_DeActiveTotalValue.BorderStyle = BorderStyle.Solid;
                tc_catg_DeActiveTotalValue.BorderWidth = 1;
                //Literal lit_catg_DeActiveTotalValue = new Literal();
                //lit_catg_DeActiveTotalValue.Text = IDActiveCount.ToString();
                HyperLink hyp_det_Terr_DeAct_Total = new HyperLink();
                hyp_det_Terr_DeAct_Total.Text = IDActiveCount.ToString();
                if (Convert.ToInt32(IDActiveCount.ToString()) > 0)
                {
                    //hyp_det_Terr_Act.NavigateUrl = "rptMRStatus.aspx?sf_code=" + drSF["sf_code"].ToString() + "&sf_name=" + drSF["sf_name"].ToString() + "&type=1&status=0";
                    sURL = "rptMRStatus.aspx?sf_code=" + 0 + "&sf_name=" + 0 + "&type=1&status=1" + "&div_code=" + div_code + "";
                    hyp_det_Terr_DeAct_Total.Attributes["onclick"] = "javascript:window.open('" + sURL + "',null,'resizable=yes,scrollbars=1,toolbar=no,menubar=no,status=no,width=500,height=600,left=0,top=0');";
                    hyp_det_Terr_DeAct_Total.NavigateUrl = "#";
                }
                else
                {
                    hyp_det_Terr_DeAct_Total.Text = " - ";
                }
                tc_catg_DeActiveTotalValue.Controls.Add(hyp_det_Terr_DeAct_Total);
                tc_catg_DeActiveTotalValue.Style.Add("font-family", "Calibri");
                tc_catg_DeActiveTotalValue.Style.Add("font-size", "10pt");
                tr_catg_total.Cells.Add(tc_catg_DeActiveTotalValue);
                // Territory DeActive

                // LstDrs Active

                TableCell tc_catg_ActiveLstDrsTotal = new TableCell();
                tc_catg_ActiveLstDrsTotal.BorderStyle = BorderStyle.Solid;
                tc_catg_ActiveLstDrsTotal.BorderWidth = 1;
                //Literal lit_catg_ActiveLstDrsTotal = new Literal();
                //lit_catg_ActiveLstDrsTotal.Text = iLstActiveCount.ToString();
                HyperLink hyp_det_Terr_ActLst_Total = new HyperLink();
                hyp_det_Terr_ActLst_Total.Text = iLstActiveCount.ToString();
                if (Convert.ToInt32(iLstActiveCount.ToString()) > 0)
                {
                    //hyp_det_Terr_Act.NavigateUrl = "rptMRStatus.aspx?sf_code=" + drSF["sf_code"].ToString() + "&sf_name=" + drSF["sf_name"].ToString() + "&type=1&status=0";
                    sURL = "rptMRStatus.aspx?sf_code=" + 0 + "&sf_name=" + 0 + "&type=2&status=0" + "&div_code=" + div_code + "";
                    hyp_det_Terr_ActLst_Total.Attributes["onclick"] = "javascript:window.open('" + sURL + "',null,'resizable=yes,scrollbars=1,toolbar=no,menubar=no,status=no,width=500,height=600,left=0,top=0');";
                    hyp_det_Terr_ActLst_Total.NavigateUrl = "#";
                }
                else
                {
                    hyp_det_Terr_ActLst_Total.Text = " - ";
                }
                tc_catg_ActiveLstDrsTotal.Controls.Add(hyp_det_Terr_ActLst_Total);
                tc_catg_ActiveLstDrsTotal.Style.Add("font-family", "Calibri");
                tc_catg_ActiveLstDrsTotal.Style.Add("font-size", "10pt");
                tr_catg_total.Cells.Add(tc_catg_ActiveLstDrsTotal);
                // LstDrs Active

                // LstDrs DeActive
                TableCell tc_catg_DeActiveLstDrsTotal = new TableCell();
                tc_catg_DeActiveLstDrsTotal.BorderStyle = BorderStyle.Solid;
                tc_catg_DeActiveLstDrsTotal.BorderWidth = 1;
                //Literal lit_catg_DeActiveLstDrsTotal = new Literal();
                //lit_catg_DeActiveLstDrsTotal.Text = iLstDActiveCount.ToString();

                HyperLink hyp_det_Terr_DeAvtLst_Total = new HyperLink();
                hyp_det_Terr_DeAvtLst_Total.Text = iLstDActiveCount.ToString();
                if (Convert.ToInt32(iLstDActiveCount.ToString()) > 0)
                {
                    //hyp_det_Terr_Act.NavigateUrl = "rptMRStatus.aspx?sf_code=" + drSF["sf_code"].ToString() + "&sf_name=" + drSF["sf_name"].ToString() + "&type=1&status=0";
                    sURL = "rptMRStatus.aspx?sf_code=" + 0 + "&sf_name=" + 0 + "&type=2&status=1" + "&div_code=" + div_code + "";
                    hyp_det_Terr_DeAvtLst_Total.Attributes["onclick"] = "javascript:window.open('" + sURL + "',null,'resizable=yes,scrollbars=1,toolbar=no,menubar=no,status=no,width=500,height=600,left=0,top=0');";
                    hyp_det_Terr_DeAvtLst_Total.NavigateUrl = "#";
                }
                else
                {
                    hyp_det_Terr_DeAvtLst_Total.Text = " - ";
                }
                tc_catg_DeActiveLstDrsTotal.Controls.Add(hyp_det_Terr_DeAvtLst_Total);
                tc_catg_DeActiveLstDrsTotal.Style.Add("font-family", "Calibri");
                tc_catg_DeActiveLstDrsTotal.Style.Add("font-size", "10pt");
                tr_catg_total.Cells.Add(tc_catg_DeActiveLstDrsTotal);
                // LstDrs DeActive

                // UnLstDrs Active
                TableCell tc_catg_ActiveUnLstDrsTotal = new TableCell();
                tc_catg_ActiveUnLstDrsTotal.BorderStyle = BorderStyle.Solid;
                tc_catg_ActiveUnLstDrsTotal.BorderWidth = 1;
                //Literal lit_catg_ActiveUnLstDrsTotal = new Literal();
                //lit_catg_ActiveUnLstDrsTotal.Text = iUnLstActiveCount.ToString();
                HyperLink hyp_det_Terr_ActUnLst_Total = new HyperLink();
                hyp_det_Terr_ActUnLst_Total.Text = iUnLstActiveCount.ToString();
                if (Convert.ToInt32(iUnLstActiveCount.ToString()) > 0)
                {
                    //hyp_det_Terr_Act.NavigateUrl = "rptMRStatus.aspx?sf_code=" + drSF["sf_code"].ToString() + "&sf_name=" + drSF["sf_name"].ToString() + "&type=1&status=0";
                    sURL = "rptMRStatus.aspx?sf_code=" + 0 + "&sf_name=" + 0 + "&type=3&status=0" + "&div_code=" + div_code + "";
                    hyp_det_Terr_ActUnLst_Total.Attributes["onclick"] = "javascript:window.open('" + sURL + "',null,'resizable=yes,scrollbars=1,toolbar=no,menubar=no,status=no,width=500,height=600,left=0,top=0');";
                    hyp_det_Terr_ActUnLst_Total.NavigateUrl = "#";
                }
                else
                {
                    hyp_det_Terr_ActUnLst_Total.Text = " - ";
                }
                tc_catg_ActiveUnLstDrsTotal.Controls.Add(hyp_det_Terr_ActUnLst_Total);
                tc_catg_ActiveUnLstDrsTotal.Style.Add("font-family", "Calibri");
                tc_catg_ActiveUnLstDrsTotal.Style.Add("font-size", "10pt");
                tr_catg_total.Cells.Add(tc_catg_ActiveUnLstDrsTotal);
                // UnLstDrs Active

                // UnLstDrs DeActive
                TableCell tc_catg_DeActiveUnLstDrsTotal = new TableCell();
                tc_catg_DeActiveUnLstDrsTotal.BorderStyle = BorderStyle.Solid;
                tc_catg_DeActiveUnLstDrsTotal.BorderWidth = 1;
                //Literal lit_catg_DeActiveUnLstDrsTotal = new Literal();
                //lit_catg_DeActiveUnLstDrsTotal.Text = iUnLstDActiveCount.ToString();
                HyperLink hyp_det_Terr_DeActUnLst_Total = new HyperLink();
                hyp_det_Terr_DeActUnLst_Total.Text = iUnLstDActiveCount.ToString();
                if (Convert.ToInt32(iUnLstDActiveCount.ToString()) > 0)
                {
                    //hyp_det_Terr_Act.NavigateUrl = "rptMRStatus.aspx?sf_code=" + drSF["sf_code"].ToString() + "&sf_name=" + drSF["sf_name"].ToString() + "&type=1&status=0";
                    sURL = "rptMRStatus.aspx?sf_code=" + 0 + "&sf_name=" + 0 + "&type=3&status=1" + "&div_code=" + div_code + "";
                    hyp_det_Terr_DeActUnLst_Total.Attributes["onclick"] = "javascript:window.open('" + sURL + "',null,'resizable=yes,scrollbars=1,toolbar=no,menubar=no,status=no,width=500,height=600,left=0,top=0');";
                    hyp_det_Terr_DeActUnLst_Total.NavigateUrl = "#";
                }
                else
                {
                    hyp_det_Terr_DeActUnLst_Total.Text = " - ";
                }
                tc_catg_DeActiveUnLstDrsTotal.Controls.Add(hyp_det_Terr_DeActUnLst_Total);
                tc_catg_DeActiveUnLstDrsTotal.Style.Add("font-family", "Calibri");
                tc_catg_DeActiveUnLstDrsTotal.Style.Add("font-size", "10pt");
                tr_catg_total.Cells.Add(tc_catg_DeActiveUnLstDrsTotal);
                // UnLstDrs DeActive

                // Chemist Active
                TableCell tc_catg_ActiveChemistTotal = new TableCell();
                tc_catg_ActiveChemistTotal.BorderStyle = BorderStyle.Solid;
                tc_catg_ActiveChemistTotal.BorderWidth = 1;
                //Literal lit_catg_ActiveChemistTotal = new Literal();
                //lit_catg_ActiveChemistTotal.Text = iChemistActiveCount.ToString();
                HyperLink hyp_det_Terr_ActChem_Total = new HyperLink();
                hyp_det_Terr_ActChem_Total.Text = iChemistActiveCount.ToString();
                if (Convert.ToInt32(iChemistActiveCount.ToString()) > 0)
                {
                    //hyp_det_Terr_Act.NavigateUrl = "rptMRStatus.aspx?sf_code=" + drSF["sf_code"].ToString() + "&sf_name=" + drSF["sf_name"].ToString() + "&type=1&status=0";
                    sURL = "rptMRStatus.aspx?sf_code=" + 0 + "&sf_name=" + 0 + "&type=4&status=0" + "&div_code=" + div_code + "";
                    hyp_det_Terr_ActChem_Total.Attributes["onclick"] = "javascript:window.open('" + sURL + "',null,'resizable=yes,scrollbars=1,toolbar=no,menubar=no,status=no,width=500,height=600,left=0,top=0');";
                    hyp_det_Terr_ActChem_Total.NavigateUrl = "#";
                }
                else
                {
                    hyp_det_Terr_ActChem_Total.Text = " - ";
                }
                tc_catg_ActiveChemistTotal.Controls.Add(hyp_det_Terr_ActChem_Total);
                tc_catg_ActiveChemistTotal.Style.Add("font-family", "Calibri");
                tc_catg_ActiveChemistTotal.Style.Add("font-size", "10pt");
                tr_catg_total.Cells.Add(tc_catg_ActiveChemistTotal);
                // Chemist Active

                // Chemist DeActive
                TableCell tc_catg_DeActiveChemistTotal = new TableCell();
                tc_catg_DeActiveChemistTotal.BorderStyle = BorderStyle.Solid;
                tc_catg_DeActiveChemistTotal.BorderWidth = 1;
                //Literal lit_catg_DeActiveChemistTotal = new Literal();
                //lit_catg_DeActiveChemistTotal.Text = iChemistDActiveCount.ToString();
                HyperLink hyp_det_Terr_DeActChem_Total = new HyperLink();
                hyp_det_Terr_DeActChem_Total.Text = iChemistDActiveCount.ToString();
                if (Convert.ToInt32(iChemistDActiveCount.ToString()) > 0)
                {
                    //hyp_det_Terr_Act.NavigateUrl = "rptMRStatus.aspx?sf_code=" + drSF["sf_code"].ToString() + "&sf_name=" + drSF["sf_name"].ToString() + "&type=1&status=0";
                    sURL = "rptMRStatus.aspx?sf_code=" + 0 + "&sf_name=" + 0 + "&type=4&status=1" + "&div_code=" + div_code + "";
                    hyp_det_Terr_DeActChem_Total.Attributes["onclick"] = "javascript:window.open('" + sURL + "',null,'resizable=yes,scrollbars=1,toolbar=no,menubar=no,status=no,width=500,height=600,left=0,top=0');";
                    hyp_det_Terr_DeActChem_Total.NavigateUrl = "#";
                }
                else
                {
                    hyp_det_Terr_DeActChem_Total.Text = " - ";
                }
                tc_catg_DeActiveChemistTotal.Controls.Add(hyp_det_Terr_DeActChem_Total);
                tc_catg_DeActiveChemistTotal.Style.Add("font-family", "Calibri");
                tc_catg_DeActiveChemistTotal.Style.Add("font-size", "10pt");
                tr_catg_total.Cells.Add(tc_catg_DeActiveChemistTotal);
                // Chemist DeActive

                // Stockist Active
                TableCell tc_catg_ActiveStockistTotal = new TableCell();
                tc_catg_ActiveStockistTotal.BorderStyle = BorderStyle.Solid;
                tc_catg_ActiveStockistTotal.BorderWidth = 1;
                //Literal lit_catg_ActiveStockistTotal = new Literal();
                //lit_catg_ActiveStockistTotal.Text = iSockistActiveCount.ToString();
                HyperLink hyp_det_Terr_ActStockist_Total = new HyperLink();
                hyp_det_Terr_ActStockist_Total.Text = iSockistActiveCount.ToString();
                if (Convert.ToInt32(iSockistActiveCount.ToString()) > 0)
                {
                    //hyp_det_Terr_Act.NavigateUrl = "rptMRStatus.aspx?sf_code=" + drSF["sf_code"].ToString() + "&sf_name=" + drSF["sf_name"].ToString() + "&type=1&status=0";
                    sURL = "rptMRStatus.aspx?sf_code=" + 0 + "&sf_name=" + 0 + "&type=5&status=0" + "&div_code=" + div_code + "";
                    hyp_det_Terr_ActStockist_Total.Attributes["onclick"] = "javascript:window.open('" + sURL + "',null,'resizable=yes,scrollbars=1,toolbar=no,menubar=no,status=no,width=500,height=600,left=0,top=0');";
                    hyp_det_Terr_ActStockist_Total.NavigateUrl = "#";
                }
                else
                {
                    hyp_det_Terr_ActStockist_Total.Text = " - ";
                }
                tc_catg_ActiveStockistTotal.Controls.Add(hyp_det_Terr_ActStockist_Total);
                tc_catg_ActiveStockistTotal.Style.Add("font-family", "Calibri");
                tc_catg_ActiveStockistTotal.Style.Add("font-size", "10pt");
                tr_catg_total.Cells.Add(tc_catg_ActiveStockistTotal);
                // Stockist Active

                // Stockist DeActive
                TableCell tc_catg_DeActiveStockistTotal = new TableCell();
                tc_catg_DeActiveStockistTotal.BorderStyle = BorderStyle.Solid;
                tc_catg_DeActiveStockistTotal.BorderWidth = 1;
                //Literal lit_catg_DeActiveStockistTotal = new Literal();
                //lit_catg_DeActiveStockistTotal.Text = iSockistDActiveCount.ToString();
                HyperLink hyp_det_Terr_DeActStockist_Total = new HyperLink();
                hyp_det_Terr_DeActStockist_Total.Text = iSockistDActiveCount.ToString();
                if (Convert.ToInt32(iSockistDActiveCount.ToString()) > 0)
                {
                    //hyp_det_Terr_Act.NavigateUrl = "rptMRStatus.aspx?sf_code=" + drSF["sf_code"].ToString() + "&sf_name=" + drSF["sf_name"].ToString() + "&type=1&status=0";
                    sURL = "rptMRStatus.aspx?sf_code=" + 0 + "&sf_name=" + 0 + "&type=5&status=1" + "&div_code=" + div_code + "";
                    hyp_det_Terr_DeActStockist_Total.Attributes["onclick"] = "javascript:window.open('" + sURL + "',null,'resizable=yes,scrollbars=1,toolbar=no,menubar=no,status=no,width=500,height=600,left=0,top=0');";
                    hyp_det_Terr_DeActStockist_Total.NavigateUrl = "#";
                }
                else
                {
                    hyp_det_Terr_DeActStockist_Total.Text = " - ";
                }
                tc_catg_DeActiveStockistTotal.Controls.Add(hyp_det_Terr_DeActStockist_Total);
                tc_catg_DeActiveStockistTotal.Style.Add("font-family", "Calibri");
                tc_catg_DeActiveStockistTotal.Style.Add("font-size", "10pt");
                tr_catg_total.Cells.Add(tc_catg_DeActiveStockistTotal);
                // Stockist DeActive
                tbl.Rows.Add(tr_catg_total);
            }
        }
    }

    
}