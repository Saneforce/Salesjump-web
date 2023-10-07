﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;

public partial class Reports_rptTPView : System.Web.UI.Page
{
    string div_code = string.Empty;
    DataSet dsSalesForce = null;
    DataSet dsTP = null;
    DataSet dsTourPlan = null;
    DataSet dsTourPlanReport = null;
    string sfCode = string.Empty;
    string sfname = string.Empty;
    int iMonth = -1;
    int iYear = -1;
    int sftype = -1;
    string sTerr = string.Empty;
    string[] sWork;
    int iIndex = -1;
    int iLevel = -1;
    string sLevel = string.Empty;
    DateTime dtTourDate;
    string sTerritory = string.Empty;
    DataSet dsWorkType = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {            
            sfCode = Request.QueryString["sf_code"].ToString();
            sfname = Request.QueryString["sf_name"].ToString();
            iMonth = Convert.ToInt32(Request.QueryString["cur_month"].ToString());
            iYear = Convert.ToInt32(Request.QueryString["cur_year"].ToString());
            iLevel = Convert.ToInt32(Request.QueryString["level"].ToString());
            //sftype = Convert.ToInt32(Request.QueryString["sf_type"].ToString());
            string sMonth = getMonthName(iMonth) + " - " + iYear.ToString();
            lblHead.Text = lblHead.Text + sMonth + " for " + sfname;
            FillSF(sfCode);
            FillTourPlan();
            
        }
    }

    private void FillSF(string sf_code)
    {
        SalesForce sf = new SalesForce();
        dsTP = sf.getSalesForce(sf_code);
        if (dsTP.Tables[0].Rows.Count > 0)
        {
            sftype  = Convert.ToInt32(dsTP.Tables[0].Rows[0].ItemArray.GetValue(30).ToString());
        }
    }

    private string getMonthName(int iMonth)
    {
        string sReturn = string.Empty;

        if (iMonth == 1)
        {
            sReturn = "January";
        }
        else if (iMonth == 2)
        {
            sReturn = "February";
        }
        else if (iMonth == 3)
        {
            sReturn = "March";
        }
        else if (iMonth == 4)
        {
            sReturn = "April";
        }
        else if (iMonth == 5)
        {
            sReturn = "May";
        }
        else if (iMonth == 6)
        {
            sReturn = "June";
        }
        else if (iMonth == 7)
        {
            sReturn = "July";
        }
        else if (iMonth == 8)
        {
            sReturn = "August";
        }
        else if (iMonth == 9)
        {
            sReturn = "September";
        }
        else if (iMonth == 10)
        {
            sReturn = "October";
        }
        else if (iMonth == 11)
        {
            sReturn = "November";
        }
        else if (iMonth == 12)
        {
            sReturn = "December";
        }
        return sReturn;
    }

    private void FillTourPlan()
    {
        if (iLevel == -1)
        {
            if ((iMonth > 0) && (iYear > 0))
            {
                TourPlan tp = new TourPlan();
                if (sftype == 1)
                {
                    dsTP = tp.get_TP_Entry(sfCode, iMonth, iYear);
                }
                else
                {
                    dsTP = tp.get_TP_EntryforMGR(sfCode, iMonth, iYear);
                }

                if (dsTP.Tables[0].Rows.Count > 0)
                {
                    grdTP.Visible = true;
                    grdTP.DataSource = dsTP;
                    grdTP.DataBind();
                }
                //dsTP = tp.get_TP_Status(sfCode, iMonth, iYear);
                //if (dsTP.Tables[0].Rows.Count > 0)
                //{
                //    //tblStatus.Visible = true;
                //    lblCompletedDate.Text = Convert.ToString(dsTP.Tables[0].Rows[0].ItemArray.GetValue(0).ToString());
                //    lblApprovedDate.Text = Convert.ToString(dsTP.Tables[0].Rows[0].ItemArray.GetValue(1).ToString());
                //    lblFFName.Text = sfname;
                //    //lblStatusDesc.Text = Convert.ToString(dsTP.Tables[0].Rows[0].ItemArray.GetValue(2).ToString());
                //    DateTime dtCompleted = Convert.ToDateTime(lblCompletedDate.Text.ToString());
                //    lblCompletedDate.Text = dtCompleted.ToString("MM/dd/yyyy");
                //    if (lblApprovedDate.Text.Trim().Length > 0)
                //    {
                //        DateTime dtApproved = Convert.ToDateTime(lblApprovedDate.Text.ToString());
                //        lblApprovedDate.Text = dtApproved.ToString("MM/dd/yyyy");
                //    }
                //}
            }
        }
        else
        {
            FillSalesForce();
        }
    }

    private void FillSalesForce()
    {
        TourPlan tp = new TourPlan();
        dsTP = tp.get_UserList_TP_Report_Level(Session["div_code"].ToString(), sfCode);
        if (dsTP.Tables[0].Rows.Count > 0)
        {
            foreach (DataRow dataRow in dsTP.Tables[0].Rows)
            {
                sLevel = dataRow["cnt_level"].ToString();
            }
        }

        if (sLevel == "1" && iLevel == 4)
        {
            //Rep
            dsTourPlan = tp.get_UserList_TP_Report(Session["div_code"].ToString(), sfCode);
            //ViewState["dsTourPlan"] = dsTourPlan;
        }
        else if (sLevel == "2" && ((iLevel == 3) || (iLevel == 4)))
        {
            //AM & Rep
        }
        else if (sLevel == "3" && ((iLevel == 2) || (iLevel == 3) || (iLevel == 4)))
        {
            //RM, AM & Rep
        }
        else if (sLevel == "4" && ((iLevel == 1) || (iLevel == 2) || (iLevel == 3) || (iLevel == 4)))
        {
            //ZM, RM, AM & Rep
        }

        // Fetch the total columns for the table
        //Doctor dr = new Doctor();
        //if (iLevel == 1)
        //{
        //    dsDoctor = dr.getDocCat(Session["div_code"].ToString());
        //    if (dsDoctor.Tables[0].Rows.Count > 0)
        //    {
        //        tot_cols = dsDoctor.Tables[0].Rows.Count;
        //        ViewState["dsDoctor"] = dsDoctor;
        //    }
        //}
        CreateDynamicTable(dsTourPlan);
    }

    private void CreateDynamicTable(DataSet dsTourPlan)
    {

        //dsDoctor = (DataSet)ViewState["dsDoctor"];
        //TableRow tr_catg = new TableRow();
        ////tr_catg.BackColor = System.Drawing.Color.Pink;

        ////if (type == "0")
        ////{
        ////    dsDoctor = (DataSet)ViewState["dsDoctor"];
        ////}        
        int i = 0;
        if (dsTourPlan.Tables[0].Rows.Count > 0)
        {
            TableRow tr_sf = new TableRow();
            foreach (DataRow dataRow in dsTourPlan.Tables[0].Rows)
            {
                TourPlan tp_report = new TourPlan();
                dsTourPlanReport = tp_report.get_TP_Detail(dataRow["sf_code"].ToString(), iMonth, iYear);
                if (dsTourPlanReport.Tables[0].Rows.Count > 0)
                {
                    tbl.BorderStyle = BorderStyle.None;
                    tbl.CellPadding = 5;
                    tbl.CellSpacing = 5;

                    //Create Dynamic Table
                    TableCell tc_sf = new TableCell();
                    //tc_sf.BorderStyle = BorderStyle.None;
                    tc_sf.VerticalAlign = VerticalAlign.Top;
                    Table tbl_sf = new Table();
                    tbl_sf.CellPadding = 0;
                    tbl_sf.CellSpacing = 0;

                    TableRow tr_tp_sf = new TableRow();                    
                    TableCell tc_tp_sf = new TableCell();
                    tc_tp_sf.BorderStyle = BorderStyle.Solid;
                    tc_tp_sf.BorderWidth = 1;
                    tc_tp_sf.ColumnSpan = 4;
                    //tc_tp_sf.Width = 10;

                    Literal lit_tp_sf = new Literal();
                    lit_tp_sf.Text = "<center><b>" + dataRow["sf_name"].ToString() + "</b></center>";
                    tc_tp_sf.Controls.Add(lit_tp_sf);
                    tr_tp_sf.Cells.Add(tc_tp_sf);

                    tbl_sf.Rows.Add(tr_tp_sf);

                    TableRow tr_tp_header = new TableRow();
                    //tr_tp_header
                    TableCell tc_tp_date_header = new TableCell();
                    tc_tp_date_header.BorderStyle = BorderStyle.Solid;
                    tc_tp_date_header.BorderWidth = 1;                    
                    tc_tp_date_header.Width = 10;
                    
                    Literal lit_tp_date_header = new Literal();
                    lit_tp_date_header.Text = "<center><b>Tour Date</b></center>";
                    tc_tp_date_header.Controls.Add(lit_tp_date_header);
                    tr_tp_header.Cells.Add(tc_tp_date_header);

                    TableCell tc_tp_worktype_header = new TableCell();
                    tc_tp_worktype_header.BorderStyle = BorderStyle.Solid;
                    tc_tp_worktype_header.BorderWidth = 1;
                    tc_tp_worktype_header.Width = 150;
                    Literal lit_tp_worktype_header = new Literal();
                    lit_tp_worktype_header.Text = "<center><b>Work Type</b></center>";
                    tc_tp_worktype_header.Controls.Add(lit_tp_worktype_header);
                    tr_tp_header.Cells.Add(tc_tp_worktype_header);

                    TableCell tc_tp_Terr_header = new TableCell();
                    tc_tp_Terr_header.BorderStyle = BorderStyle.Solid;
                    tc_tp_Terr_header.BorderWidth = 1;
                    tc_tp_Terr_header.Width = 300;
                    Literal lit_tp_Terr_header = new Literal();
                    lit_tp_Terr_header.Text = "<center><b>Territory Planned</b></center>";
                    tc_tp_Terr_header.Controls.Add(lit_tp_Terr_header);
                    tr_tp_header.Cells.Add(tc_tp_Terr_header);

                    TableCell tc_tp_obj_header = new TableCell();
                    tc_tp_obj_header.BorderStyle = BorderStyle.Solid;
                    tc_tp_obj_header.BorderWidth = 1;
                    Literal lit_tp_obj_header = new Literal();
                    lit_tp_obj_header.Text = "<center><b>Objective</b></center>";
                    tc_tp_obj_header.Controls.Add(lit_tp_obj_header);
                    tr_tp_header.Cells.Add(tc_tp_obj_header);

                    tbl_sf.Rows.Add(tr_tp_header);
                    
                    foreach (DataRow dr in dsTourPlanReport.Tables[0].Rows)
                    {
                        TableRow tr_tp = new TableRow();

                        TableCell tc_tp_date = new TableCell();
                        tc_tp_date.BorderStyle = BorderStyle.Solid;
                        tc_tp_date.BorderWidth = 1;
                        Literal lit_tp_date = new Literal();
                        dtTourDate = Convert.ToDateTime(dr["tour_date"].ToString());
                        lit_tp_date.Text = dtTourDate.ToString("MM-dd-yyyy");
                        tc_tp_date.Controls.Add(lit_tp_date);
                        tr_tp.Cells.Add(tc_tp_date);
                        
                        TableCell tc_tp_worktype = new TableCell();
                        tc_tp_worktype.BorderStyle = BorderStyle.Solid;
                        tc_tp_worktype.BorderWidth = 1;
                        Literal lit_tp_worktype = new Literal();
                        lit_tp_worktype.Text = dr["WorkType"].ToString();

                        if (dr["WorkType"].ToString().Length > 0)
                        {
                            TourPlan trp = new TourPlan();
                            dsWorkType = trp.FetchWorkType(dr["WorkType"].ToString());
                            if (dsWorkType.Tables[0].Rows.Count > 0)
                            {
                                lit_tp_worktype.Text = Convert.ToString(dsWorkType.Tables[0].Rows[0].ItemArray.GetValue(0).ToString());
                            }
                        }

                        tc_tp_worktype.Controls.Add(lit_tp_worktype);
                        tr_tp.Cells.Add(tc_tp_worktype);

                        //----Territory-----
                        TourPlan tp = new TourPlan();
                        if (iLevel == 4)
                        {
                            dsTP = tp.FetchTerritory(dataRow["sf_code"].ToString(), dr["Worked_With_SF_Code"].ToString());
                            if (dsTP.Tables[0].Rows.Count > 0)
                            {
                                sTerritory = Convert.ToString(dsTP.Tables[0].Rows[0].ItemArray.GetValue(0).ToString());
                            }
                            else
                            {
                                dsTP = tp.FetchWorkType(dr["Worked_With_SF_Code"].ToString());
                                if (dsTP.Tables[0].Rows.Count > 0)
                                {
                                    sTerritory = Convert.ToString(dsTP.Tables[0].Rows[0].ItemArray.GetValue(0).ToString());
                                }
                            }

                        }
                        else
                        {
                            sTerr = "";
                            string sTerrMgr = dr["Worked_With_SF_Code"].ToString();
                            sWork = sTerrMgr.Split(',');
                            foreach (string sw in sWork)
                            {
                                if (sw.Trim().Length > 0)
                                {
                                    dsTP = tp.FetchTerritory_MGR(sw);
                                    if (dsTP.Tables[0].Rows.Count > 0)
                                    {
                                        foreach (DataRow dRow in dsTP.Tables[0].Rows)
                                        {
                                            if (sTerr.Trim().Length > 0)
                                            {
                                                sTerr = dRow["Territory_Name"].ToString();
                                            }
                                            else
                                            {
                                                sTerr = sTerr + "," + dRow["Territory_Name"].ToString();
                                            }
                                        }
                                    }
                                    else
                                    {
                                        dsTP = tp.FetchWorkType(dr["Worked_With_SF_Code"].ToString());
                                        if (dsTP.Tables[0].Rows.Count > 0)
                                        {
                                            sTerritory = Convert.ToString(dsTP.Tables[0].Rows[0].ItemArray.GetValue(0).ToString());
                                        }
                                    }
                                }

                            }
                            sTerritory = sTerr;
                        }
                        

                        //----Territory-----
                        TableCell tc_tp_Terr = new TableCell();
                        tc_tp_Terr.BorderStyle = BorderStyle.Solid;
                        tc_tp_Terr.BorderWidth = 1;
                        Literal lit_tp_Terr = new Literal();
                        lit_tp_Terr.Text = "&nbsp;" + sTerritory;
                        tc_tp_Terr.Controls.Add(lit_tp_Terr);
                        tr_tp.Cells.Add(tc_tp_Terr);

                        TableCell tc_tp_obj = new TableCell();
                        tc_tp_obj.BorderStyle = BorderStyle.Solid;
                        tc_tp_obj.BorderWidth = 1;
                        Literal lit_tp_obj = new Literal();
                        lit_tp_obj.Text = dr["objective"].ToString();
                        tc_tp_obj.Controls.Add(lit_tp_obj);
                        tr_tp.Cells.Add(tc_tp_obj);
                        
                        tbl_sf.Rows.Add(tr_tp);
                    }

                    tc_sf.Controls.Add(tbl_sf);
                    tr_sf.Cells.Add(tc_sf);

                }
                tbl.Rows.Add(tr_sf);
            }
        }

        //foreach (DataRow dataRow in dsDoctor.Tables[0].Rows)
        //{
        //    TableCell tc_catg_name = new TableCell();
        //    tc_catg_name.BorderStyle = BorderStyle.Solid;
        //    tc_catg_name.BorderWidth = 1;
        //    Literal lit_catg_name = new Literal();
        //    lit_catg_name.Text = "<center><b>" + dataRow["Doc_Cat_Name"].ToString() + "</b></center>";
        //    tc_catg_name.Controls.Add(lit_catg_name);
        //    tr_catg.Cells.Add(tc_catg_name);
        //}

        //tbl.Rows.Add(tr_catg);

        //TableRow tr_det = new TableRow();
        //iTotal_FF = 0;
        //i = 0;
        //foreach (DataRow dataRow in dsDoctor.Tables[0].Rows)
        //{
        //    TableCell tc_catg_det_name = new TableCell();
        //    Literal lit_catg_det_name = new Literal();

        //    Doctor dr_cat = new Doctor();
        //    if (type == "0")
        //    {
        //        iDRCatg = dr_cat.getDoctorcount(sf_code, dataRow["Doc_Cat_Code"].ToString());
        //        lblCatg.Text = "Listed Doctor count - Categorywise";
        //    }
        //    else if (type == "1")
        //    {
        //        iDRCatg = dr_cat.getSpecialcount(sf_code, dataRow["Doc_Cat_Code"].ToString());
        //        lblCatg.Text = "Listed Doctor count - Specialitywise";
        //    }
        //    else if (type == "2")
        //    {
        //        iDRCatg = dr_cat.getClasscount(sf_code, dataRow["Doc_Cat_Code"].ToString());
        //        lblCatg.Text = "Listed Doctor count - Classwise";
        //    }
        //    else if (type == "3")
        //    {
        //        iDRCatg = dr_cat.getQualcount(sf_code, dataRow["Doc_Cat_Code"].ToString());
        //        lblCatg.Text = "Listed Doctor count - Qualificationwise";
        //    }

        //    if (iDRCatg == 0)
        //    {
        //        sDRCatg_Count = " - ";
        //    }
        //    else
        //    {
        //        sDRCatg_Count = iDRCatg.ToString();
        //    }

        //    lit_catg_det_name.Text = "<center>" + sDRCatg_Count + "</center>";

        //    tc_catg_det_name.BorderStyle = BorderStyle.Solid;
        //    tc_catg_det_name.VerticalAlign = VerticalAlign.Middle;
        //    tc_catg_det_name.BorderWidth = 1;
        //    tc_catg_det_name.Controls.Add(lit_catg_det_name);
        //    tr_det.Cells.Add(tc_catg_det_name);

        //    tbl.Rows.Add(tr_det);
        //}
    }
    
    protected void grdTP_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (sftype == 1)
            e.Row.Cells[3].Visible = false;

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblTourPlan = (Label)e.Row.FindControl("lblTourPlan");
            Label lblterr = (Label)e.Row.FindControl("lblterr");
            Label lblWorkType = (Label)e.Row.FindControl("lblWorkType");
            Label lblObjective = (Label)e.Row.FindControl("lblObjective");

            DateTime dtTourPlan = Convert.ToDateTime(lblTourPlan.Text.ToString());
            if (lblterr != null)
            {
                lblTourPlan.Text = dtTourPlan.ToString("MM/dd/yyyy");
                TourPlan tp = new TourPlan();
                if (sftype == 1)
                {
                    dsTP = tp.FetchTerritory(sfCode, lblterr.Text);
                    if (dsTP.Tables[0].Rows.Count > 0)
                    {
                        lblterr.Text = Convert.ToString(dsTP.Tables[0].Rows[0].ItemArray.GetValue(0).ToString());
                    }
                    else
                    {
                        dsTP = tp.FetchWorkType(lblterr.Text);
                        if (dsTP.Tables[0].Rows.Count > 0)
                        {
                            lblterr.Text = Convert.ToString(dsTP.Tables[0].Rows[0].ItemArray.GetValue(0).ToString());
                        }
                    }

                }
                else
                {
                    sTerr = "";
                    string sTerrMgr = lblterr.Text.Trim();
                    sWork = sTerrMgr.Split(',');
                    foreach (string sw in sWork)
                    {
                        if (sw.Trim().Length > 0)
                        {
                            dsTP = tp.FetchTerritory_MGR(sw);
                            if (dsTP.Tables[0].Rows.Count > 0)
                            {
                                foreach (DataRow dataRow in dsTP.Tables[0].Rows)
                                {
                                    if (sTerr.Trim().Length > 0)
                                    {
                                        sTerr = dataRow["Territory_Name"].ToString();
                                    }
                                    else
                                    {
                                        sTerr = sTerr + "," + dataRow["Territory_Name"].ToString();
                                    }
                                }
                            }
                            else
                            {
                                dsTP = tp.FetchWorkType(lblterr.Text);
                                if (dsTP.Tables[0].Rows.Count > 0)
                                {
                                    lblterr.Text = Convert.ToString(dsTP.Tables[0].Rows[0].ItemArray.GetValue(0).ToString());
                                }
                            }
                        }
                    
                    }

                    lblterr.Text = sTerr;
                    //dsTP = tp.FetchTerritory_MGR(lblterr.Text);
                    Label lblFieldForceName = (Label)e.Row.FindControl("lblFieldForceName");
                    lblFieldForceName.Text = lblFieldForceName.Text.Replace(",", " ");
                    lblFieldForceName.Text = lblFieldForceName.Text.Trim();
                    SalesForce sf = new SalesForce();
                    dsSalesForce  = sf.getSfName(lblFieldForceName.Text.Trim());
                    if (dsSalesForce.Tables[0].Rows.Count > 0)
                    {
                        lblFieldForceName.Text = Convert.ToString(dsSalesForce.Tables[0].Rows[0].ItemArray.GetValue(0).ToString());
                    }
                    
                }

                if (lblterr.Text == "0")
                {
                    lblterr.Text = "";
                    lblWorkType.Text = lblObjective.Text;
                    lblObjective.Text = "";
                }
                else
                {
                    lblWorkType.Text = "Field Work";
                }

            }
        }
    }

}