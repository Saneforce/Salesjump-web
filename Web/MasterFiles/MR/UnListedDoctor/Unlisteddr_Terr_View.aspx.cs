using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
public partial class MasterFiles_MR_UnListedDoctor_Unlisteddr_Terr_View : System.Web.UI.Page
{
    DataSet dsSalesForce = null;
    DataSet dsDoctor = null;
    DataSet dsCatg = null;
    DataSet dsSpec = null;
    DataSet dsClass = null;
    DataSet dsQual = null;
    DataSet dsDivision = null;
    DataSet dsSalesforce = null;
    bool isff = false;
    int iDRCatg = -1;
    int iTotal_FF = 0;
    int[] iTotal_Catg = new int[200];
    int tot_catg = 0;
    int i = -1;
    string sDRCatg_Count = string.Empty;
    string div_code = string.Empty;
    string sf_code = string.Empty;
    DataSet dsState = null;
    string sState = string.Empty;
    string[] statecd;
    string slno;
    string state_cd = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        sf_code = Session["sf_code"].ToString();

        if (!Page.IsPostBack)
        {
           // menu1.Title = this.Page.Title;
           
           // menu1.FindControl("btnBack").Visible = false;
            ViewState["dsSalesForce"] = null;
            ViewState["dsDoctor"] = null;
            if (Session["sf_type"].ToString() == "2")
            {
                FillMR();
            }
            
        }
        if (Session["sf_type"].ToString() == "1")
        {
            sf_code = Session["sf_code"].ToString();
            UserControl_MR_Menu c1 =
                (UserControl_MR_Menu)LoadControl("~/UserControl/MR_Menu.ascx");
            Divid.Controls.Add(c1);
            c1.FindControl("btnBack").Visible = false;
            c1.Title = this.Page.Title;
            pnlSf.Visible = false;
            pnltype.Visible = true;

        }
        else if (Session["sf_type"].ToString() == "2")
        {
            sf_code = Session["sf_code"].ToString();
            UserControl_MGR_Menu c1 =
            (UserControl_MGR_Menu)LoadControl("~/UserControl/MGR_Menu.ascx");
            Divid.Controls.Add(c1);
            c1.FindControl("btnBack").Visible = false;
            c1.Title = this.Page.Title;
            pnlSf.Visible = true;
            pnltype.Visible = true;
          //  FillMR();
            btnGo_Click(sender, e);
        }
        FillSalesForce();
    }

    private void FillMR()
    {
        SalesForce sf = new SalesForce();
        DataSet dsmgrsf = new DataSet();
        SalesForce ds = new SalesForce();

        // Check if the manager has a team
        DataSet DsAudit = ds.SF_Hierarchy(div_code, sf_code);
        if (DsAudit.Tables[0].Rows.Count > 0)
        {
            dsSalesforce = sf.SalesForceList_New(div_code, sf_code);
        }
        else
        {
            // Fetch Managers Audit Team
            DataTable dt = ds.getAuditManagerTeam_GetMR(div_code, sf_code, 0);
            dsmgrsf.Tables.Add(dt);
            dsSalesforce = dsmgrsf;
        }
        if (dsSalesforce.Tables[0].Rows.Count > 0)
        {
            ddlFieldForce.DataValueField = "SF_Code";
            ddlFieldForce.DataTextField = "Sf_Name";
            ddlFieldForce.DataSource = dsSalesforce;
            ddlFieldForce.DataBind();

        }
    }
    private void FillSalesForce()
    {
        int tot_rows = 0;
        int tot_cols = 0;

        // Fetch the total rows for the table
        SalesForce sf = new SalesForce();
        Territory terr = new Territory();
        if (Session["sf_type"].ToString() == "1")
        {
            sf_code = Session["sf_code"].ToString();

        }
        else
        {
            sf_code = ddlFieldForce.SelectedValue.ToString();
        }
        dsSalesForce = terr.getTerritory(sf_code);

        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            tot_rows = dsSalesForce.Tables[0].Rows.Count;
            ViewState["dsSalesForce"] = dsSalesForce;
        }
        // Fetch the total columns for the table
        Doctor dr = new Doctor();
        dsDoctor = dr.getDocCat_terr(div_code);
        if (dsDoctor.Tables[0].Rows.Count > 0)
        {
            tot_cols = dsDoctor.Tables[0].Rows.Count;
            ViewState["dsDoctor"] = dsDoctor;
        }

        // Fetch the total columns for the table
        dsSpec = dr.getDocSpec(div_code);
        if (dsSpec.Tables[0].Rows.Count > 0)
        {
            ViewState["dsSpec"] = dsSpec;
        }

        dsClass = dr.getDocClass(div_code);
        if (dsClass.Tables[0].Rows.Count > 0)
        {
            ViewState["dsClass"] = dsClass;
        }

        dsQual = dr.getDocQual(div_code);
        if (dsQual.Tables[0].Rows.Count > 0)
        {
            ViewState["dsQual"] = dsQual;
        }
        
        CreateDynamicTable(tot_rows, tot_cols);
    }

    private void CreateDynamicTable(int tblRows, int tblCols)
    {

        if (ViewState["dsSalesForce"] != null)
        {
            ViewState["HQ_Det"] = null;
            dsDoctor = (DataSet)ViewState["dsDoctor"];
            dsSpec = (DataSet)ViewState["dsSpec"];
            dsClass = (DataSet)ViewState["dsClass"];

            TableRow tr_header = new TableRow();
            tr_header.BorderStyle = BorderStyle.Solid;
            tr_header.BorderWidth = 1;

            TableCell tc_SNo = new TableCell();
            tc_SNo.BorderStyle = BorderStyle.Solid;
            tc_SNo.BorderColor = System.Drawing.Color.Black;
            tc_SNo.BorderWidth = 1;
          
            tc_SNo.ForeColor = System.Drawing.Color.White;
            Literal lit_SNo = new Literal();
            lit_SNo.Text = "<center>S.No</center>";

            tc_SNo.Controls.Add(lit_SNo);
            tc_SNo.RowSpan = 2;
            tc_SNo.Style.Add("font-family", "Calibri");
            tc_SNo.Style.Add("font-size", "10pt");
            tr_header.Cells.Add(tc_SNo);
            if (Session["sf_type"].ToString() == "1")
            {
                tr_header.BackColor = System.Drawing.Color.FromName("#7AA3CC");

            }
            else
            {
                tr_header.BackColor = System.Drawing.Color.FromName("#99B7B7");
            }
            TableCell tc_FF = new TableCell();
            tc_FF.BorderStyle = BorderStyle.Solid;
            tc_FF.BorderWidth = 1;
       
            tc_FF.BorderColor = System.Drawing.Color.Black;
            Literal lit_FF = new Literal();
          //  lit_FF.Text = "<center>Territory</center>";
            Territory terr = new Territory();
            DataSet dsTerritory = new DataSet();
            dsTerritory = terr.getWorkAreaName(div_code);
            if (dsTerritory.Tables[0].Rows.Count > 0)
            {
                lit_FF.Text = "<center>" +dsTerritory.Tables[0].Rows[0]["wrk_area_Name"].ToString() + "</center>";
            }
            else
            {
                lit_FF.Text = "<center>Territory</center>";
            }
            tc_FF.Controls.Add(lit_FF);
            tc_FF.ForeColor = System.Drawing.Color.White;
            tc_FF.RowSpan = 2;
            tc_FF.Style.Add("margin-top", "20px");
            tc_FF.Style.Add("font-family", "Calibri");
            tc_FF.Style.Add("font-size", "10pt");
            tr_header.Cells.Add(tc_FF);

            //TableCell tc_catg = new TableCell();
            //Literal lit_catg = new Literal();
            //lit_catg.Text = "<center>Category</center>";

            //tc_catg.Controls.Add(lit_catg);
            //tc_catg.BorderStyle = BorderStyle.Solid;
            //tc_catg.BorderColor = System.Drawing.Color.Black;
            //tc_catg.BorderWidth = 1;
            //tc_catg.ForeColor = System.Drawing.Color.White;
            //tc_catg.Style.Add("font-family", "Calibri");
            //tc_catg.Style.Add("font-size", "10pt");
            //tc_catg.ColumnSpan = dsDoctor.Tables[0].Rows.Count;
            //tr_header.Cells.Add(tc_catg);
            TableCell tc_catg = new TableCell();
            Literal lit_catg = new Literal();
            tc_catg.ForeColor = System.Drawing.Color.White;
            tc_catg.Style.Add("font-family", "Calibri");
            tc_catg.Style.Add("font-size", "10pt");
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

            if (rdoType.SelectedValue.ToString().Trim() == "0")
            {
                tc_catg.ColumnSpan = dsDoctor.Tables[0].Rows.Count;
            }
            else if (rdoType.SelectedValue.ToString().Trim() == "1")
            {
                tc_catg.ColumnSpan = dsSpec.Tables[0].Rows.Count;
            }
            else if (rdoType.SelectedValue.ToString().Trim() == "2")
            {
                tc_catg.ColumnSpan = dsClass.Tables[0].Rows.Count;
            }
            else if (rdoType.SelectedValue.ToString().Trim() == "3")
            {
                tc_catg.ColumnSpan = dsQual.Tables[0].Rows.Count;
            }
            tr_header.Cells.Add(tc_catg);
            TableCell tc_Total = new TableCell();
            tc_Total.BorderStyle = BorderStyle.Solid;
            tc_Total.BorderWidth = 1;
            tc_Total.BorderColor = System.Drawing.Color.Black;
        
            tc_Total.ForeColor = System.Drawing.Color.White;
            Literal lit_Total = new Literal();
            lit_Total.Text = "<center>Total</center>";
            tc_Total.Controls.Add(lit_Total);
            tc_Total.RowSpan = 2;
            tc_Total.Style.Add("font-family", "Calibri");
            tc_Total.Style.Add("font-size", "10pt");
            tr_header.Cells.Add(tc_Total);

            tbl.Rows.Add(tr_header);

            TableRow tr_catg = new TableRow();
            if (Session["sf_type"].ToString() == "1")
            {
                tr_catg.BackColor = System.Drawing.Color.FromName("#7AA3CC");

            }
            else
            {
                tr_catg.BackColor = System.Drawing.Color.FromName("#99B7B7");
            }
          //  dsDoctor = (DataSet)ViewState["dsDoctor"];
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
                tc_catg_name.HorizontalAlign = HorizontalAlign.Center;
                tc_catg_name.BorderWidth = 1;
                tc_catg_name.BorderColor = System.Drawing.Color.Black;
             
                tc_catg_name.ForeColor = System.Drawing.Color.White;
                Literal lit_catg_name = new Literal();
                lit_catg_name.Text = dataRow["Doc_Cat_Name"].ToString();
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


                TableRow tr_det = new TableRow();
                iCount += 1;
                TableCell tc_det_SNo = new TableCell();
                Literal lit_det_SNo = new Literal();
                lit_det_SNo.Text = "<center>" + iCount.ToString() + "</center>";
                tc_det_SNo.BorderStyle = BorderStyle.Solid;
                tc_det_SNo.BorderWidth = 1;

                tc_det_SNo.Controls.Add(lit_det_SNo);
                //tc_det_SNo.Height = 10;
                tr_det.Style.Add("font-family", "Calibri");
                tr_det.Style.Add("font-size", "10pt");
                tr_det.Cells.Add(tc_det_SNo);
                //tr_det.Height = 10;
                tr_det.BackColor = System.Drawing.Color.White;
                TableCell tc_det_FF = new TableCell();
                Literal lit_det_FF = new Literal();
                lit_det_FF.Text = "&nbsp" + drFF["Territory_Name"].ToString();
                tc_det_FF.BorderStyle = BorderStyle.Solid;
                tc_det_FF.BorderWidth = 1;
                tc_det_FF.Controls.Add(lit_det_FF);
                tc_det_FF.Style.Add("text-align", "left");
                tc_det_FF.Style.Add("font-family", "Calibri");
                tr_det.Cells.Add(tc_det_FF);

                iTotal_FF = 0;
                i = 0;
                foreach (DataRow dataRow in dsDoctor.Tables[0].Rows)
                {
                    TableCell tc_catg_det_name = new TableCell();
                    HyperLink hyp_catg_det_name = new HyperLink();

                    Doctor dr_cat = new Doctor();

                   // iDRCatg = dr_cat.getUnlistDoctorMRcount(drFF["Territory_Code"].ToString(), dataRow["Doc_Cat_Code"].ToString());
                    if (rdoType.SelectedValue.ToString().Trim() == "0")
                    {
                        iDRCatg = dr_cat.getUnlist_Cat_Count(drFF["Territory_Code"].ToString(), dataRow["Doc_Cat_Code"].ToString());
                    }
                    else if (rdoType.SelectedValue.ToString().Trim() == "1")
                    {
                        iDRCatg = dr_cat.getUnlist_Spec_Count(drFF["Territory_Code"].ToString(), dataRow["Doc_Cat_Code"].ToString());
                    }
                    else if (rdoType.SelectedValue.ToString().Trim() == "2")
                    {
                        iDRCatg = dr_cat.getUnlist_Cls_Count(drFF["Territory_Code"].ToString(), dataRow["Doc_Cat_Code"].ToString());
                    }
                    else if (rdoType.SelectedValue.ToString().Trim() == "3")
                    {
                        iDRCatg = dr_cat.getUnlist_Qua_Count(drFF["Territory_Code"].ToString(), dataRow["Doc_Cat_Code"].ToString());
                    }

                    if (Session["sf_type"].ToString() == "1")
                    {
                        sf_code = Session["sf_code"].ToString();

                    }
                    else
                    {
                        sf_code = ddlFieldForce.SelectedValue.ToString();
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

                        sURL = "rptUnlisteddr_Terr_View.aspx?sf_code=" + sf_code + "&terr_code=" + drFF["Territory_Code"].ToString() + " &terr_Name=" + drFF["Territory_Name"].ToString() + "&cat_code=" + dataRow["Doc_Cat_Code"].ToString() + "&cat_name=" + dataRow["Doc_Cat_name"].ToString() + "&type=" + rdoType.SelectedValue.ToString() + "&div=" + div_code;
                        hyp_catg_det_name.Attributes["onclick"] = "javascript:window.open('" + sURL + "',null,'resizable=yes,toolbar=no,menubar=no,scrollbars=yes,status=no,width=600,height=800,left=0,top=0');";
                        hyp_catg_det_name.NavigateUrl = "#";


                        iTotal_Catg[i] = iTotal_Catg[i] + iDRCatg;
                    }

                    hyp_catg_det_name.Text = "<center>" + sDRCatg_Count + "</center>";

                    tc_catg_det_name.BorderStyle = BorderStyle.Solid;
                    tc_catg_det_name.VerticalAlign = VerticalAlign.Middle;
                    tc_catg_det_name.BorderWidth = 1;

                    tc_catg_det_name.Controls.Add(hyp_catg_det_name);
                    tr_det.Style.Add("text-align", "left");
                    tr_det.Style.Add("font-family", "Calibri");
                    tr_det.Cells.Add(tc_catg_det_name);

                    i++;
                }

                TableCell tc_det_FF_Count = new TableCell();
                //Literal lit_det_FF_Count = new Literal();
                HyperLink hyp_FF_det_total = new HyperLink();
                //lit_det_FF_Count.Text = "&nbsp;" + "<center>" + iTotal_FF.ToString() + "</center>";                
               
                hyp_FF_det_total.Text = "<center>" + iTotal_FF.ToString() + "</center>";
                if (Session["sf_type"].ToString() == "1")
                {
                    sf_code = Session["sf_code"].ToString();

                }
                else
                {
                    sf_code = ddlFieldForce.SelectedValue.ToString();
                }
                if (iTotal_FF > 0)
                    if (iTotal_FF == 0)
                    {
                        iTotal_FF = 0;
                    }
                    else
                    {
                        //hyp_FF_det_total.NavigateUrl = "rptDoctorCategory.aspx?sf_code=" + drFF["sf_code"].ToString() + "&sf_name=" + drFF["sf_name"].ToString() + "&type=" + rdoType.SelectedValue.ToString() + "&div=" + div_code;

                        sURL = "rptUnlisteddr_Terr_View.aspx?sf_code=" + sf_code + "&terr_code=" + drFF["Territory_Code"].ToString() + " &terr_Name=" + drFF["Territory_Name"].ToString() + "&type=" + rdoType.SelectedValue.ToString() + "&div=" + div_code;
                        hyp_FF_det_total.Attributes["onclick"] = "javascript:window.open('" + sURL + "',null,'resizable=yes,toolbar=no,menubar=no,scrollbars=yes,status=no,width=600,height=800,left=0,top=0');";
                        hyp_FF_det_total.NavigateUrl = "#";
                    }
                tc_det_FF_Count.BorderStyle = BorderStyle.Solid;
                tc_det_FF_Count.VerticalAlign = VerticalAlign.Middle;
                tc_det_FF_Count.HorizontalAlign = HorizontalAlign.Center;
                tc_det_FF_Count.BorderWidth = 1;
               
                //tc_det_FF_Count.Controls.Add(lit_det_FF_Count);
                tc_det_FF_Count.Controls.Add(hyp_FF_det_total);
                tr_det.Cells.Add(tc_det_FF_Count);

                tbl.Rows.Add(tr_det);
            }

            TableRow tr_catg_total = new TableRow();
            TableCell tc_catg_Total = new TableCell();
            tc_catg_Total.BorderStyle = BorderStyle.Solid;
            tc_catg_Total.BorderWidth = 1;
            //tc_catg_Total.Width = 25;

            Literal lit_catg_Total = new Literal();
            lit_catg_Total.Text = "<center>Total</center>";
            tc_catg_Total.Controls.Add(lit_catg_Total);
            tc_catg_Total.BackColor = System.Drawing.Color.White;
            tc_catg_Total.ColumnSpan = 2;
            tc_catg_Total.Style.Add("text-align", "left");
            tc_catg_Total.Style.Add("font-family", "Calibri");
            tr_catg_total.Cells.Add(tc_catg_Total);
            i = 0;

            foreach (DataRow dataRow in dsDoctor.Tables[0].Rows)
            {
                TableCell tc_FF_Total = new TableCell();
                Literal lit_catg_det_name = new Literal();
                HyperLink hyp_catg_det_total = new HyperLink();
                //lit_catg_det_name.Text = "<center>" + iTotal_Catg[i].ToString() + "</center>";
                if (Session["sf_type"].ToString() == "1")
                {
                    sf_code = Session["sf_code"].ToString();

                }
                else
                {
                    sf_code = ddlFieldForce.SelectedValue.ToString();
                }
                if (iTotal_Catg[i] > 0)
                    // hyp_catg_det_total.NavigateUrl = "rptDoctorCategory.aspx?cat_code=" + dataRow["Doc_Cat_Code"].ToString() + "&cat_name=" + dataRow["Doc_Cat_name"].ToString() + "&type=" + rdoType.SelectedValue.ToString() + "&div=" + div_code;
                    if (iTotal_Catg[i] == 0)
                    {
                        iTotal_Catg[i] = 0;
                    }
                    else
                    {
                        sURL = "rptUnlisteddr_Terr_View.aspx?sf_code=" + sf_code + "&cat_code=" + dataRow["Doc_Cat_Code"].ToString() + "&cat_name=" + dataRow["Doc_Cat_name"].ToString() + "&type=" + rdoType.SelectedValue.ToString() + "&div=" + div_code;
                        hyp_catg_det_total.Attributes["onclick"] = "javascript:window.open('" + sURL + "',null,'resizable=yes,toolbar=no,menubar=no,scrollbars=yes,status=no,width=600,height=800,left=0,top=0');";
                        hyp_catg_det_total.NavigateUrl = "#";
                    }

                hyp_catg_det_total.Text = "<center>" + iTotal_Catg[i].ToString() + "</center>";
                tot_catg = tot_catg + iTotal_Catg[i];

                tc_FF_Total.BorderStyle = BorderStyle.Solid;
                tc_FF_Total.VerticalAlign = VerticalAlign.Middle;
                tc_FF_Total.BorderWidth = 1;
                tc_FF_Total.BackColor = System.Drawing.Color.White;
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
            if (Session["sf_type"].ToString() == "1")
            {
                sf_code = Session["sf_code"].ToString();

            }
            else
            {
                sf_code = ddlFieldForce.SelectedValue.ToString();
            }
            if (tot_catg > 0)
                // hyp_tot_catg_det_total.NavigateUrl = "rptDoctorCategory.aspx?mgr_code=" + sf_code + "&type=" + rdoMGRState.SelectedValue.ToString() + "&div=" + div_code;
                sURL = "rptUnlisteddr_Terr_View.aspx?mgr_code=" + sf_code + "&type=" + rdoType.SelectedValue.ToString() + "&div=" + div_code;
            hyp_tot_catg_det_total.Attributes["onclick"] = "javascript:window.open('" + sURL + "',null,'resizable=yes,toolbar=no,menubar=no,scrollbars=yes,status=no,width=600,height=800,left=0,top=0');";
            hyp_tot_catg_det_total.NavigateUrl = "#";

            tc_tot_catg.Controls.Add(hyp_tot_catg_det_total);
            tr_catg_total.Style.Add("text-align", "left");
            tr_catg_total.Style.Add("font-family", "Calibri");
            tr_catg_total.Cells.Add(tc_tot_catg);
            tr_catg_total.BackColor = System.Drawing.Color.White;
            tbl.Rows.Add(tr_catg_total);

            ViewState["dynamictable"] = true;
            lblNoRecord.Visible = false;
        }
        else
        {
            lblNoRecord.Visible = true;
            pnltype.Visible = false;
        }    
    }
    protected void btnGo_Click(object sender, EventArgs e)
    {
        ViewState["dsSalesForce"] = null;
        ViewState["dsDoctor"] = null;
       // FillSalesForce();
    }
}