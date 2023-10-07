using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
using System.Drawing.Imaging;
public partial class Reports_DoctorList_Reportaspx : System.Web.UI.Page
{
    DataSet dsSalesForce = null;
    DataSet dsDoctor = new DataSet();
    DataSet dsCatg = null;
    DataSet dsSpec = null;
    DataSet dsClass = null;
    DataSet dsQual = null;
    DataSet dsDivision = null;
    DataSet dsdiv = null;
    string strSf_Code = string.Empty;
    bool isff = false;
    int iDRCatg = -1;
    int iTotal_FF = 0;
    int[] iTotal_Catg = new int[500];
    int tot_catg = 0;
    int i = -1;
    string sDRCatg_Count = string.Empty;
    string div_code = string.Empty;
    string sfCode = string.Empty;
    string sf_type = string.Empty;
    DataSet dsState = null;
    string sState = string.Empty;
    string[] statecd;
    string slno;
    string state_cd = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
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
                if (rdoMGRState.SelectedValue.ToString() == "0")
                {
                    //lblFF.Text = "Field Force";
                    if (Session["sf_type"].ToString() == "" || Session["sf_type"].ToString() == null || Session["sf_type"].ToString() == "3")
                    {
                        FillManagers();
                        FillColor();
                        //   ddlDivision.SelectedIndex = 1;
                        ddlDivision_SelectedIndexChanged(sender, e);
                        ddlFieldForce.SelectedIndex = 1;
                        btnSubmit.Focus();
                        rdoType.SelectedIndex = 1;
                    }
                }
                else
                {
                    lblFF.Text = "State";
                    FillState(ddlDivision.SelectedValue.ToString());
                }
                if (Session["sf_type"].ToString() == "2")
                {
                    FillMGRLogin();
                }
                //menu1.Title = this.Page.Title;
                //menu1.FindControl("btnBack").Visible = false;

            }
            if (rdoMGRState.SelectedValue.ToString() == "0")
            {               
               //FillColor();                
            }


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
                lblFF.Visible = false;
                FillColor();
            }
            else if (Session["sf_type"].ToString() == "")
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
                    //FillColor();
                }
            }


            else if (Session["sf_type"].ToString() == "2")
            {
                UserControl_MGR_Menu c1 =
                (UserControl_MGR_Menu)LoadControl("~/UserControl/MGR_Menu.ascx");
                Divid.Controls.Add(c1);
                c1.FindControl("btnBack").Visible = false;
                c1.Title = Page.Title;

                lblView.Visible = false;
                ddlDivision.Visible = false;
                ddlFFType.Visible = false;
                lblDivision.Visible = false;
                rdoMGRState.Visible = false;
                lblView.Visible = false;
                FillColor();

            }
        }
        catch (Exception ex)
        {
            Response.Redirect(ex.Message);
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
    private void FillSalesForce()
    {
        int tot_rows = 0;
        int tot_cols = 0;

        // Fetch the total rows for the table
        SalesForce sf = new SalesForce();
        if (Session["sf_type"].ToString() == "" || Session["sf_type"].ToString() == "3")
        {
            if (rdoMGRState.SelectedValue.ToString() == "0")
            {
                DataSet dsmgrsf = new DataSet();
                SalesForce ds = new SalesForce();

                // Check if the manager has a team
                DataSet DsAudit = ds.SF_Hierarchy(ddlDivision.SelectedValue.ToString(), ddlFieldForce.SelectedValue.ToString());
                if (DsAudit.Tables[0].Rows.Count > 0)
                {
                    dsSalesForce = sf.sp_UserListMr_Doc_List_Vacant(ddlFieldForce.SelectedValue.ToString(), ddlDivision.SelectedValue);
                  //  dsSalesForce = sf.sp_UserList_getMR_Doc_List(ddlDivision.SelectedValue.ToString(), ddlFieldForce.SelectedValue.ToString());
                }
                else
                {
                    // Fetch Managers Audit Team - MR

                    DataTable dt = ds.getAuditManagerTeam_GetMR_Sfname_With_Vacant(ddlDivision.SelectedValue.ToString(), ddlFieldForce.SelectedValue.ToString(), 0);
                    dsmgrsf.Tables.Add(dt);
                    dsSalesForce = dsmgrsf;
                }
                //dsSalesForce = sf.getDoctorCount_SFWise(div_code, ddlFieldForce.SelectedValue.ToString());
                FillColor();
            }
            else
            {
                dsSalesForce = sf.getDoctorCount_statewise(ddlDivision.SelectedValue.ToString(), ddlFieldForce.SelectedValue.ToString());
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

        }

        if (Session["sf_type"].ToString() == "1")
        {
            dsSalesForce = sf.sp_UserMRLogin_With_Vacant(div_code, sfCode);
        }

        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            tot_rows = dsSalesForce.Tables[0].Rows.Count;
            ViewState["dsSalesForce"] = dsSalesForce;
        }



        // Fetch the total columns for the table
        Doctor dr = new Doctor();
        if (Session["sf_type"].ToString() == "" || Session["sf_type"].ToString() == "3")
        {
            dsDoctor = dr.getDocCat(ddlDivision.SelectedValue.ToString());
            dsSpec = dr.getDocSpec(ddlDivision.SelectedValue.ToString());
            dsClass = dr.getDocClass(ddlDivision.SelectedValue.ToString());
            dsQual = dr.getDocQual(ddlDivision.SelectedValue.ToString());

        }
        else if (Session["sf_type"].ToString() == "2")
        {
            dsDoctor = dr.getDocCat(div_code);
            dsSpec = dr.getDocSpec(div_code);
            dsClass = dr.getDocClass(div_code);
            dsQual = dr.getDocQual(div_code);

        }
        else if (Session["sf_type"].ToString() == "1")
        {
            dsDoctor = dr.getDocCat(div_code);
            dsSpec = dr.getDocSpec(div_code);
            dsClass = dr.getDocClass(div_code);
            dsQual = dr.getDocQual(div_code);
        }

        if (dsDoctor.Tables[0].Rows.Count > 0)
        {
            tot_cols = dsDoctor.Tables[0].Rows.Count;
            ViewState["dsDoctor"] = dsDoctor;
        }


        // Fetch the total columns for the table
        //if (Session["sf_type"].ToString() == "")
        //{

        //}
        //else if (Session["sf_type"].ToString() == "2")
        //{

        //}

        if (dsSpec.Tables[0].Rows.Count > 0)
        {
            ViewState["dsSpec"] = dsSpec;
        }


        if (dsClass.Tables[0].Rows.Count > 0)
        {
            ViewState["dsClass"] = dsClass;
        }


        if (dsQual.Tables[0].Rows.Count > 0)
        {
            ViewState["dsQual"] = dsQual;
        }

        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            CreateDynamicTable(tot_rows, tot_cols);
            GrdDoctor.Visible = false;
        }
        else
        {
            GrdDoctor.Visible = true;
            GrdDoctor.DataSource = null;
            GrdDoctor.DataBind();

        }


        //FillColor();
    }

    private void CreateDynamicTable(int tblRows, int tblCols)
    {

        if (ddlDivision.SelectedValue == "0")
        {
            ddlDivision.SelectedValue = div_code;
        }

        if (ViewState["dsSalesForce"] != null)
        {

            ViewState["HQ_Det"] = null;

            dsDoctor = (DataSet)ViewState["dsDoctor"];
            dsSpec = (DataSet)ViewState["dsSpec"];
            dsClass = (DataSet)ViewState["dsClass"];

            TableRow tr_header = new TableRow();
            tr_header.BorderStyle = BorderStyle.Solid;
            tr_header.Width = 200;

            tr_header.BorderWidth = 1;
            tr_header.Attributes.Add("border-collapse", "collapse");
            tr_header.BorderColor = System.Drawing.Color.Black;
            TableCell tc_SNo = new TableCell();
            tc_SNo.BorderStyle = BorderStyle.Solid;
            tc_SNo.BorderColor = System.Drawing.Color.Black;
            tc_SNo.BorderWidth = 1;
            tc_SNo.Width = 50;
            tc_SNo.ForeColor = System.Drawing.Color.White;
            Literal lit_SNo = new Literal();
            lit_SNo.Text = "S.No";

            tc_SNo.Controls.Add(lit_SNo);
            tc_SNo.RowSpan = 2;
            tc_SNo.Style.Add("font-family", "Calibri");
            tc_SNo.Style.Add("font-size", "10pt");
            tr_header.Cells.Add(tc_SNo);

            tr_header.BackColor = System.Drawing.Color.FromName("#336277");

            TableCell tc_UserName = new TableCell();
            tc_UserName.BorderStyle = BorderStyle.Solid;
            tc_UserName.BorderWidth = 1;
            tc_UserName.Width = 200;
            tc_UserName.BorderColor = System.Drawing.Color.Black;
            Literal lit_UserName = new Literal();
            lit_UserName.Text = "<center>User Name</center>";
            tc_UserName.Controls.Add(lit_UserName);
            tc_UserName.Visible = false;
            tc_UserName.ForeColor = System.Drawing.Color.White;
            tc_UserName.RowSpan = 2;
            tc_UserName.Style.Add("font-family", "Calibri");
            tc_UserName.Style.Add("font-size", "10pt");
            tr_header.Cells.Add(tc_UserName);

            TableCell tc_FF = new TableCell();
            tc_FF.BorderStyle = BorderStyle.Solid;
            tc_FF.BorderWidth = 1;
            tc_FF.Width = 800;
            tc_FF.BorderColor = System.Drawing.Color.Black;
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
            tc_Designation.BorderWidth = 1;
            tc_Designation.Width = 200;
            tc_Designation.BorderColor = System.Drawing.Color.Black;
            Literal lit_Designation = new Literal();
            lit_Designation.Text = "<center>Designation</center>";
            tc_Designation.Controls.Add(lit_Designation);
            tc_Designation.ForeColor = System.Drawing.Color.White;
            tc_Designation.RowSpan = 2;
            tc_Designation.Style.Add("font-family", "Calibri");
            tc_Designation.Style.Add("font-size", "10pt");
            tr_header.Cells.Add(tc_Designation);

            TableCell tc_HQ = new TableCell();
            tc_HQ.BorderStyle = BorderStyle.Solid;
            tc_HQ.BorderWidth = 1;
            tc_HQ.Width = 400;
            tc_HQ.BorderColor = System.Drawing.Color.Black;
            Literal lit_HQ = new Literal();
            lit_HQ.Text = "<center>HQ</center>";
            tc_HQ.Controls.Add(lit_HQ);
            tc_HQ.ForeColor = System.Drawing.Color.White;
            tc_HQ.RowSpan = 2;
            tc_HQ.Style.Add("font-family", "Calibri");
            tc_HQ.Style.Add("font-size", "10pt");
            tr_header.Cells.Add(tc_HQ);

            TableCell tc_catg = new TableCell();

            Literal lit_catg = new Literal();
            tc_catg.Width = 400;
            if (rdoType.SelectedValue.ToString().Trim() == "0")
            {
                lit_catg.Text = "<center>Category</center>";
            }
            else if (rdoType.SelectedValue.ToString().Trim() == "1")
            {
                lit_catg.Text = "<center>Speciality</center>";
            }
            else if (rdoType.SelectedValue.ToString().Trim() == "2")
            {
                lit_catg.Text = "<center>Class</center>";
            }
            else if (rdoType.SelectedValue.ToString().Trim() == "3")
            {
                lit_catg.Text = "<center>Qualification</center>";
            }

            tc_catg.Controls.Add(lit_catg);
            tc_catg.BorderStyle = BorderStyle.Solid;
            tc_catg.BorderColor = System.Drawing.Color.Black;
            tc_catg.BorderWidth = 1;
            tc_catg.ForeColor = System.Drawing.Color.White;
            tc_catg.Style.Add("font-family", "Calibri");
            tc_catg.Style.Add("font-size", "10pt");

            if (rdoType.SelectedValue.ToString().Trim() == "0")
            {

                tc_catg.ColumnSpan = dsDoctor.Tables[0].Rows.Count;

            }
            else if (rdoType.SelectedValue.ToString().Trim() == "1")
            {
                if (dsSpec.Tables[0].Rows.Count > 0)
                {
                    tc_catg.ColumnSpan = dsSpec.Tables[0].Rows.Count;
                }
            }
            else if (rdoType.SelectedValue.ToString().Trim() == "2")
            {
                if (dsClass.Tables[0].Rows.Count > 0)
                {
                    tc_catg.ColumnSpan = dsClass.Tables[0].Rows.Count;
                }
            }
            else if (rdoType.SelectedValue.ToString().Trim() == "3")
            {
                if (dsQual.Tables[0].Rows.Count > 0)
                {
                    tc_catg.ColumnSpan = dsQual.Tables[0].Rows.Count;
                }
            }

            tr_header.Cells.Add(tc_catg);

            TableCell tc_Total = new TableCell();
            tc_Total.BorderStyle = BorderStyle.Solid;
            tc_Total.BorderWidth = 1;
            tc_Total.BorderColor = System.Drawing.Color.Black;
            tc_Total.Width = 40;
            tc_Total.ForeColor = System.Drawing.Color.White;
            Literal lit_Total = new Literal();
            lit_Total.Text = "<center>Total</center>";
            tc_Total.Controls.Add(lit_Total);
            tc_Total.RowSpan = 2;
            tr_header.Cells.Add(tc_Total);
            tc_Total.Style.Add("font-family", "Calibri");
            tc_Total.Style.Add("font-size", "10pt");
            tbl.Rows.Add(tr_header);

            TableRow tr_catg = new TableRow();
            tr_catg.BackColor = System.Drawing.Color.FromName("#336277");


            if (rdoType.SelectedValue.ToString().Trim() == "0")
            {
                dsDoctor = (DataSet)ViewState["dsDoctor"];
            }
            else if (rdoType.SelectedValue.ToString().Trim() == "1")
            {
                dsDoctor = (DataSet)ViewState["dsSpec"];
            }
            else if (rdoType.SelectedValue.ToString().Trim() == "2")
            {
                dsDoctor = (DataSet)ViewState["dsClass"];
            }
            else if (rdoType.SelectedValue.ToString().Trim() == "3")
            {
                dsDoctor = (DataSet)ViewState["dsQual"];
            }

            foreach (DataRow dataRow in dsDoctor.Tables[0].Rows)
            {
                TableCell tc_catg_name = new TableCell();
                tc_catg_name.BorderStyle = BorderStyle.Solid;
                tc_catg_name.BorderWidth = 1;
                tc_catg_name.BorderColor = System.Drawing.Color.Black;
                tc_catg_name.Width = 60;
                tc_catg_name.ForeColor = System.Drawing.Color.White;
                Literal lit_catg_name = new Literal();
                lit_catg_name.Text = "<center>" + dataRow["Doc_Cat_SName"].ToString() + "</center>";
                tc_catg_name.Controls.Add(lit_catg_name);
                tc_catg_name.Style.Add("font-family", "Calibri");
                tc_catg_name.Style.Add("font-size", "10pt");

                tr_catg.Cells.Add(tc_catg_name);

            }


            tbl.Rows.Add(tr_catg);

            // Details Section
            string sURL = string.Empty;
            int iCount = 0;
            dsSalesForce = (DataSet)ViewState["dsSalesForce"];
            foreach (DataRow drFF in dsSalesForce.Tables[0].Rows)
            {

                ViewState["HQ_Det"] = drFF["sf_hq"].ToString();

                TableRow tr_det = new TableRow();
                iCount += 1;
                strSf_Code += "'" + drFF["sf_code"].ToString() + "'" + ",";

                TableCell tc_det_SNo = new TableCell();
                Literal lit_det_SNo = new Literal();
                lit_det_SNo.Text = "<center>" + iCount.ToString() + "</center>";
                tc_det_SNo.BorderStyle = BorderStyle.Solid;
                tc_det_SNo.BorderWidth = 1;
                tc_det_SNo.Style.Add("font-family", "Calibri");
                tc_det_SNo.Style.Add("font-size", "10pt");
                tc_det_SNo.BackColor = System.Drawing.Color.White;
                tc_det_SNo.Controls.Add(lit_det_SNo);
                //tc_det_SNo.Height = 10;
                tr_det.Cells.Add(tc_det_SNo);
                //tr_det.Height = 10;

                TableCell tc_det_UserName = new TableCell();
                Literal lit_det_UserName = new Literal();
                lit_det_UserName.Text = "&nbsp;" + drFF["Sf_UserName"].ToString();
                tc_det_UserName.Visible = false;
                tc_det_UserName.BorderStyle = BorderStyle.Solid;
                tc_det_UserName.BorderWidth = 1;
                tc_det_UserName.Style.Add("font-family", "Calibri");
                tc_det_UserName.Style.Add("font-size", "10pt");
                tc_det_UserName.Style.Add("text-align", "left");
                tc_det_UserName.Controls.Add(lit_det_UserName);
                tr_det.Cells.Add(tc_det_UserName);
                tc_det_UserName.BackColor = System.Drawing.Color.White;

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
                tc_det_FF.BackColor = System.Drawing.Color.White;

                TableCell tc_det_Designation = new TableCell();
                Literal lit_det_Designation = new Literal();
                if (rdoMGRState.SelectedValue.ToString() == "1")
                {
                    lit_det_Designation.Text = "&nbsp;" + drFF["sf_Designation_Short_Name"].ToString();
                }
                else
                {
                    lit_det_Designation.Text = "&nbsp;" + drFF["Designation_Short_Name"].ToString();
                }
                tc_det_Designation.BorderStyle = BorderStyle.Solid;
                tc_det_Designation.BorderWidth = 1;
                tc_det_Designation.Style.Add("font-family", "Calibri");
                tc_det_Designation.Style.Add("font-size", "10pt");
                tc_det_Designation.Style.Add("text-align", "left");
                tc_det_Designation.Controls.Add(lit_det_Designation);
                tr_det.Cells.Add(tc_det_Designation);
                tc_det_Designation.BackColor = System.Drawing.Color.White;

                TableCell tc_det_HQ = new TableCell();
                Literal lit_det_HQ = new Literal();
                lit_det_HQ.Text = "&nbsp;" + drFF["sf_hq"].ToString();
                tc_det_HQ.BorderStyle = BorderStyle.Solid;
                tc_det_HQ.BorderWidth = 1;
                tc_det_HQ.Style.Add("font-family", "Calibri");
                tc_det_HQ.Style.Add("font-size", "10pt");
                tc_det_HQ.Style.Add("text-align", "left");
                tc_det_HQ.Controls.Add(lit_det_HQ);
                tr_det.Cells.Add(tc_det_HQ);
                tc_det_HQ.BackColor = System.Drawing.Color.White;

                iTotal_FF = 0;
                i = 0;
                foreach (DataRow dataRow in dsDoctor.Tables[0].Rows)
                {
                    TableCell tc_catg_det_name = new TableCell();
                    HyperLink hyp_catg_det_name = new HyperLink();
                    tc_catg_det_name.Style.Add("font-family", "Calibri");
                    tc_catg_det_name.Style.Add("font-size", "10pt");
                    Doctor dr_cat = new Doctor();
                    if (rdoType.SelectedValue.ToString().Trim() == "0")
                    {
                        iDRCatg = dr_cat.getDoctorcount(drFF["sf_code"].ToString(), dataRow["Doc_Cat_Code"].ToString());
                    }
                    else if (rdoType.SelectedValue.ToString().Trim() == "1")
                    {
                        iDRCatg = dr_cat.getSpecialcount(drFF["sf_code"].ToString(), dataRow["Doc_Cat_Code"].ToString());
                    }
                    else if (rdoType.SelectedValue.ToString().Trim() == "2")
                    {
                        iDRCatg = dr_cat.getClasscount(drFF["sf_code"].ToString(), dataRow["Doc_Cat_Code"].ToString());
                    }
                    else if (rdoType.SelectedValue.ToString().Trim() == "3")
                    {
                        iDRCatg = dr_cat.getQualcount(drFF["sf_code"].ToString(), dataRow["Doc_Cat_Code"].ToString());
                    }

                    if (iDRCatg == 0)
                    {
                        sDRCatg_Count = " - ";
                    }
                    else
                    {
                        sDRCatg_Count = iDRCatg.ToString();
                        iTotal_FF = iTotal_FF + iDRCatg;



                        //hyp_catg_det_name.NavigateUrl = "rptDoctorCategory.aspx?sf_code=" + drFF["sf_code"].ToString() + "&cat_code=" + dataRow["Doc_Cat_Code"].ToString() + "&cat_name=" + dataRow["Doc_Cat_name"].ToString() + "&sf_name=" + drFF["sf_name"].ToString() + "&type=" + rdoType.SelectedValue.ToString();
                        sURL = "rptDoctorCategory.aspx?sf_code=" + drFF["sf_code"].ToString() + "&cat_code=" + dataRow["Doc_Cat_Code"].ToString() + "&cat_name=" + dataRow["Doc_Cat_name"].ToString() + "&sf_name=" + drFF["sf_name"].ToString() + "&type=" + rdoType.SelectedValue.ToString() + "&div=" + ddlDivision.SelectedValue.ToString();
                        hyp_catg_det_name.Attributes["onclick"] = "javascript:window.open('" + sURL + "',null,'resizable=yes,toolbar=no,menubar=no,scrollbars=1,status=no,width=500,height=600,left=0,top=0');";
                        hyp_catg_det_name.NavigateUrl = "#";


                        iTotal_Catg[i] = iTotal_Catg[i] + iDRCatg;
                    }

                    hyp_catg_det_name.Text = "<center>" + sDRCatg_Count + "</center>";

                    tc_catg_det_name.BorderStyle = BorderStyle.Solid;
                    tc_catg_det_name.VerticalAlign = VerticalAlign.Middle;
                    tc_catg_det_name.BorderWidth = 1;
                    tc_catg_det_name.Style.Add("font-family", "Calibri");
                    tc_catg_det_name.Style.Add("font-size", "10pt");
                    tc_catg_det_name.BackColor = System.Drawing.Color.White;
                    tc_catg_det_name.Controls.Add(hyp_catg_det_name);
                    tr_det.Cells.Add(tc_catg_det_name);

                    i++;
                }

                TableCell tc_det_FF_Count = new TableCell();
                //Literal lit_det_FF_Count = new Literal();
                HyperLink hyp_FF_det_total = new HyperLink();
                //lit_det_FF_Count.Text = "&nbsp;" + "<center>" + iTotal_FF.ToString() + "</center>";                
                //hyp_FF_det_total.Height = 10;
                //hyp_FF_det_total.Width = 20;
                hyp_FF_det_total.Text = "<center>" + iTotal_FF.ToString() + "</center>";

                if (iTotal_FF > 0)
                    if (iTotal_FF == 0)
                    {
                        iTotal_FF = 0;
                    }
                    else
                    {

                        //hyp_FF_det_total.NavigateUrl = "rptDoctorCategory.aspx?sf_code=" + drFF["sf_code"].ToString() + "&sf_name=" + drFF["sf_name"].ToString() + "&type=" + rdoType.SelectedValue.ToString() + "&div=" + ddlDivision.SelectedValue.ToString();
                        sURL = "rptDoctorCategory.aspx?sf_code=" + drFF["sf_code"].ToString() + "&sf_name=" + drFF["sf_name"].ToString() + "&type=" + rdoType.SelectedValue.ToString() + "&div=" + ddlDivision.SelectedValue.ToString();
                        hyp_FF_det_total.Attributes["onclick"] = "javascript:window.open('" + sURL + "',null,'resizable=yes,toolbar=no,menubar=no,scrollbars=1,status=no,width=500,height=600,left=0,top=0');";
                        hyp_FF_det_total.NavigateUrl = "#";
                    }
                tc_det_FF_Count.BorderStyle = BorderStyle.Solid;
                tc_det_FF_Count.VerticalAlign = VerticalAlign.Middle;
                tc_det_FF_Count.HorizontalAlign = HorizontalAlign.Center;
                tc_det_FF_Count.BorderWidth = 1;
                //     tc_det_FF_Count.Width  = 25;
                tc_det_FF_Count.Style.Add("font-family", "Calibri");
                tc_det_FF_Count.Style.Add("font-size", "10pt");
                tc_det_FF_Count.Style.Add("text-align", "center");
                tc_det_FF_Count.BackColor = System.Drawing.Color.White;
                //tc_det_FF_Count.Controls.Add(lit_det_FF_Count);
                tc_det_FF_Count.Controls.Add(hyp_FF_det_total);
                tr_det.Cells.Add(tc_det_FF_Count);

                tbl.Rows.Add(tr_det);
            }

            Session["Sf_Code_multiple"] = strSf_Code.Remove(strSf_Code.Length - 1);

            TableRow tr_catg_total = new TableRow();
            TableCell tc_catg_Total = new TableCell();
            tc_catg_Total.BorderStyle = BorderStyle.Solid;
            tc_catg_Total.BorderWidth = 1;
            //tc_catg_Total.Width = 25;

            if (Session["sf_type"].ToString() != "1")
            {

                Literal lit_catg_Total = new Literal();
                lit_catg_Total.Text = "<center>Total</center>";
                tc_catg_Total.Controls.Add(lit_catg_Total);
                tc_catg_Total.ColumnSpan = 4;
                tc_catg_Total.Style.Add("font-family", "Calibri");
                tc_catg_Total.Style.Add("font-size", "10pt");
                tr_catg_total.Cells.Add(tc_catg_Total);

                i = 0;

                foreach (DataRow dataRow in dsDoctor.Tables[0].Rows)
                {
                    TableCell tc_FF_Total = new TableCell();
                    Literal lit_catg_det_name = new Literal();
                    HyperLink hyp_catg_det_total = new HyperLink();
                    //lit_catg_det_name.Text = "<center>" + iTotal_Catg[i].ToString() + "</center>";
                    if (iTotal_Catg[i] > 0)
                        if (iTotal_Catg[i] == 0)
                        {
                            iTotal_Catg[i] = 0;
                        }
                        else
                        {
                            // hyp_catg_det_total.NavigateUrl = "rptDoctorCategory.aspx?cat_code=" + dataRow["Doc_Cat_Code"].ToString() + "&cat_name=" + dataRow["Doc_Cat_name"].ToString() + "&type=" + rdoType.SelectedValue.ToString() + "&div=" + ddlDivision.SelectedValue.ToString();

                            sURL = "rptDoctorCategory.aspx?cat_code=" + dataRow["Doc_Cat_Code"].ToString() + "&cat_name=" + dataRow["Doc_Cat_name"].ToString() + "&type=" + rdoType.SelectedValue.ToString() + "&div=" + ddlDivision.SelectedValue.ToString();
                            hyp_catg_det_total.Attributes["onclick"] = "javascript:window.open('" + sURL + "',null,'resizable=yes,toolbar=no,menubar=no,scrollbars=1,status=no,width=500,height=600,left=0,top=0');";
                            hyp_catg_det_total.NavigateUrl = "#";

                        }
                    hyp_catg_det_total.Text = "<center>" + iTotal_Catg[i].ToString() + "</center>";
                    tot_catg = tot_catg + iTotal_Catg[i];

                    tc_FF_Total.BorderStyle = BorderStyle.Solid;
                    tc_FF_Total.VerticalAlign = VerticalAlign.Middle;
                    tc_FF_Total.BorderWidth = 1;
                    tc_FF_Total.Style.Add("font-family", "Calibri");
                    tc_FF_Total.Style.Add("font-size", "10pt");
                    //tc_FF_Total.Controls.Add(lit_catg_det_name);
                    tc_FF_Total.Controls.Add(hyp_catg_det_total);
                    tr_catg_total.Cells.Add(tc_FF_Total);
                    i++;
                }




                TableCell tc_tot_catg = new TableCell();
                tc_tot_catg.BorderStyle = BorderStyle.Solid;
                tc_tot_catg.BorderWidth = 1;
                HyperLink hyp_tot_catg_det_total = new HyperLink();
                //Literal lit_tot_catg = new Literal();
                hyp_tot_catg_det_total.Text = "<center>" + tot_catg.ToString() + "</center>";
                if (tot_catg > 0)

                    // hyp_tot_catg_det_total.NavigateUrl = "rptDoctorCategory.aspx?mgr_code=" + ddlFieldForce.SelectedValue.ToString() + "&type=" + rdoMGRState.SelectedValue.ToString() + "&div=" + ddlDivision.SelectedValue.ToString();
                    sURL = "rptDoctorCategory.aspx?mgr_code=" + ddlFieldForce.SelectedValue.ToString() + "&type=" + rdoMGRState.SelectedValue.ToString() + "&div=" + ddlDivision.SelectedValue.ToString();
                hyp_tot_catg_det_total.Attributes["onclick"] = "javascript:window.open('" + sURL + "',null,'resizable=yes,toolbar=no,menubar=no,scrollbars=1,status=no,width=500,height=600,left=0,top=0');";
                hyp_tot_catg_det_total.NavigateUrl = "#";



                tr_catg_total.Cells.Add(tc_tot_catg);
                tc_tot_catg.Controls.Add(hyp_tot_catg_det_total);
                tr_catg_total.Style.Add("font-family", "Calibri");
                tr_catg_total.Style.Add("font-size", "10pt");
                tr_catg_total.BackColor = System.Drawing.Color.White;
                tbl.Rows.Add(tr_catg_total);

                ViewState["dynamictable"] = true;
            }
        }
    }

    private void FillColor()
    {
        int j = 0;


        foreach (ListItem ColorItems in ddlSF.Items)
        {
            //ddlFieldForce.Items[j].Selected = true;
            string bcolor = "#" + ColorItems.Text;
            ddlFieldForce.Items[j].Attributes.Add("style", "background-color:" + bcolor);

            j = j + 1;

        }
    }

    private void FillManagers()
    {
        SalesForce sf = new SalesForce();
        isff = true;
        if (ddlFFType.SelectedValue.ToString() == "1")
        {
            ddlAlpha.Visible = false;
            if (Session["sf_type"].ToString() == "" || Session["sf_type"].ToString() == "3")
            {
                dsSalesForce = sf.UserList_Hierarchy(ddlDivision.SelectedValue.ToString(), "admin");
            }

        }
        else if (ddlFFType.SelectedValue.ToString() == "0")
        {
            FillSF_Alpha();
            ddlAlpha.Visible = true;
            if (Session["sf_type"].ToString() == "" || Session["sf_type"].ToString() == "3")
            {
                dsSalesForce = sf.UserList_Alpha(ddlDivision.SelectedValue.ToString(), "admin");
            }

        }
        else if (ddlFFType.SelectedValue.ToString() == "2")
        {
            if (Session["sf_type"].ToString() == "" || Session["sf_type"].ToString() == "3")
            {
                dsSalesForce = sf.UserList_HQ(ddlDivision.SelectedValue.ToString(), "admin");
            }

        }

        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            ddlFieldForce.DataTextField = "sf_name";
            ddlFieldForce.DataValueField = "sf_code";
            ddlFieldForce.DataSource = dsSalesForce;
            ddlFieldForce.DataBind();

            if (Session["sf_type"].ToString() == "" || Session["sf_type"].ToString() == "3")
            {
                ddlSF.DataTextField = "des_color";
            }
            else
            {
                ddlSF.DataTextField = "desig_color";
            }
            ddlSF.DataValueField = "sf_code";
            ddlSF.DataSource = dsSalesForce;
            ddlSF.DataBind();
            FillColor();

        }
    }

    private void FillMGRLogin()
    {
        SalesForce sf = new SalesForce();
        if (Session["sf_type"].ToString() == "2")
        {
            DataSet dsmgrsf = new DataSet();
            SalesForce ds = new SalesForce();

            // Check if the manager has a team
            DataSet DsAudit = ds.SF_Hierarchy(div_code, sfCode);
            if (DsAudit.Tables[0].Rows.Count > 0)
            {
                dsSalesForce = sf.UserList_Hierarchy(div_code, sfCode);
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


            ddlSF.DataTextField = "des_color";
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

    private void FillState(string div_code)
    {
        Division dv = new Division();
        if (Session["sf_type"].ToString() == "" || Session["sf_type"].ToString() == "3")
        {
            dsDivision = dv.getStatePerDivision(ddlDivision.SelectedValue.ToString());
        }
        else if (Session["sf_type"].ToString() == "2")
        {
            dsDivision = dv.getStatePerDivision(div_code);
        }
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

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        //if (ddlFieldForce.SelectedValue.ToString().Trim().Length > 0)
        //{
        ViewState["dsSalesForce"] = null;
        ViewState["dsDoctor"] = null;
        FillSalesForce();
        // }

    }

    protected void rdoMGRState_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rdoMGRState.SelectedValue.ToString() == "0")
        {
            lblFF.Text = "Field Force";
            FillManagers();
            FillColor();
            ddlFFType.Visible = true;
            rdoType.SelectedIndex = 1;
        }
        else
        {
            lblFF.Text = "State";
            FillState(div_code);
            ddlFFType.Visible = false;
            ddlAlpha.Visible = false;
            rdoType.SelectedIndex = 1;
        }

    }
    protected void ddlAlpha_SelectedIndexChanged(object sender, EventArgs e)
    {
        SalesForce sf = new SalesForce();
        if (ddlAlpha.SelectedIndex == 0)
        {
            if (Session["sf_type"].ToString() == "" || Session["sf_type"].ToString() == "3")
            {
                dsSalesForce = sf.UserList_Alpha(ddlDivision.SelectedValue.ToString(), "admin");
            }
            else if (Session["sf_type"].ToString() == "2")
            {
                dsSalesForce = sf.UserList_Alpha(div_code, sfCode);
            }

        }
        else
        {
            dsSalesForce = sf.UserList_Alphasearch(ddlDivision.SelectedValue.ToString(), "admin", ddlAlpha.SelectedValue);
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
            FillColor();
        }


    }
    protected void ddlFFType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rdoMGRState.SelectedValue.ToString() == "0")
        {
            lblFF.Text = "Field Force";
            FillManagers();
            FillColor();
        }
        else
        {
            lblFF.Text = "State";
            ddlAlpha.Visible = false;
            FillState(ddlDivision.SelectedValue.ToString());
        }

    }
    protected void ddlDivision_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillManagers();
        FillColor();

        //FillSalesForce();
    }


}