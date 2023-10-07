using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;

public partial class MIS_Reports_Visit_Details_Basedonvisit_Level4 : System.Web.UI.Page
{
    string div_code = string.Empty;
    DataSet dsSalesForce = null;
    DataSet dsUserList = null;
    DataSet dsDoc = null;
    DataSet dsDCR = null;
    DateTime dtCurrent;
    DataSet dsDoctor = null;
    DataSet dsCatg = null;

    int MonColspan = 0;
    string tot_dr = string.Empty;
    string tot_dcr_dr = string.Empty;
    int imissed_dr = -1;
    string sCurrentDate = string.Empty;
    int doctor_total = 0;
    string sMode = string.Empty;
    string sf_code = string.Empty;
    string FMonth = string.Empty;
    string FYear = string.Empty;
    string TMonth = string.Empty;
    string TYear = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        sMode = Request.QueryString["cMode"].ToString();
        sf_code = Request.QueryString["sfcode"].ToString();
        FMonth = Request.QueryString["FMonth"].ToString();
        FYear = Request.QueryString["FYear"].ToString();
        TMonth = Request.QueryString["TMonth"].ToString();
        TYear = Request.QueryString["TYear"].ToString();

        SalesForce sf = new SalesForce();
        DataSet dssf = sf.getSfName(sf_code);
        if (dssf.Tables[0].Rows.Count > 0)
        {
            lblText.Text = lblText.Text + Convert.ToString(dssf.Tables[0].Rows[0].ItemArray.GetValue(0).ToString());
        }

        if (sMode == "1")
        {
            FillCatg();
        }
        else if (sMode == "2")
        {
            FillSpec();
        }
        else if (sMode == "3")
        {
            FillClass();
        }

    }
    private void FillCatg()
    {
        tbl.Rows.Clear();
        doctor_total = 0;


        SalesForce sf = new SalesForce();
        dsSalesForce = sf.SF_ReportingTo_TourPlan(div_code, sf_code);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            TableRow tr_header = new TableRow();
            tr_header.BorderStyle = BorderStyle.Solid;
            tr_header.BorderWidth = 1;

            TableCell tc_SNo = new TableCell();
            tc_SNo.BorderStyle = BorderStyle.Solid;
            tc_SNo.BorderWidth = 1;
            tc_SNo.Width = 50;
            tc_SNo.RowSpan = 3;
            Literal lit_SNo = new Literal();
            lit_SNo.Text = "S.No";
            tc_SNo.Attributes.Add("Class", "rptCellBorder");
            tc_SNo.Controls.Add(lit_SNo);
            tr_header.Cells.Add(tc_SNo);
            //tr_header.BackColor = System.Drawing.Color.FromName("#A6A6D2");

            if (Session["sf_type"].ToString() == "1")
            {
                tr_header.Attributes.Add("Class", "MRBackColor");
            }
            else if (Session["sf_type"].ToString() == "2")
            {
                tr_header.Attributes.Add("Class", "MGRBackColor");
            }
            else
            {
                tr_header.Attributes.Add("Class", "Backcolor");
            }

            TableCell tc_DR_Code = new TableCell();
            tc_DR_Code.BorderStyle = BorderStyle.Solid;
            tc_DR_Code.BorderWidth = 1;
            tc_DR_Code.Width = 400;
            tc_DR_Code.RowSpan = 3;
            Literal lit_DR_Code = new Literal();
            lit_DR_Code.Text = "<center>SF Code</center>";
            tc_DR_Code.Controls.Add(lit_DR_Code);
            tc_DR_Code.Visible = false;
            tr_header.Cells.Add(tc_DR_Code);

            TableCell tc_DR_Name = new TableCell();
            tc_DR_Name.BorderStyle = BorderStyle.Solid;
            tc_DR_Name.BorderWidth = 1;
            tc_DR_Name.Width = 900;
            tc_DR_Name.RowSpan = 3;
            Literal lit_DR_Name = new Literal();
            lit_DR_Name.Text = "<center>Field Force</center>";
            tc_DR_Name.Attributes.Add("Class", "rptCellBorder");
            tc_DR_Name.Controls.Add(lit_DR_Name);
            tr_header.Cells.Add(tc_DR_Name);

            TableCell tc_DR_HQ = new TableCell();
            tc_DR_HQ.BorderStyle = BorderStyle.Solid;
            tc_DR_HQ.BorderWidth = 1;
            tc_DR_HQ.Width = 500;
            tc_DR_HQ.RowSpan = 3;
            Literal lit_DR_HQ = new Literal();
            tc_DR_HQ.Attributes.Add("Class", "rptCellBorder");
            lit_DR_HQ.Text = "<center>HQ</center>";
            tc_DR_HQ.Controls.Add(lit_DR_HQ);
            tr_header.Cells.Add(tc_DR_HQ);

            TableCell tc_DR_Des = new TableCell();
            tc_DR_Des.BorderStyle = BorderStyle.Solid;
            tc_DR_Des.BorderWidth = 1;
            tc_DR_Des.Width = 500;
            tc_DR_Des.RowSpan = 3;
            Literal lit_DR_Des = new Literal();
            tc_DR_Des.Attributes.Add("Class", "rptCellBorder");
            lit_DR_Des.Text = "<center>Designation</center>";
            tc_DR_Des.Controls.Add(lit_DR_Des);
            tr_header.Cells.Add(tc_DR_Des);

            int months = (Convert.ToInt32(TYear) - Convert.ToInt32(FYear)) * 12 + Convert.ToInt32(TMonth) - Convert.ToInt32(FMonth); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
            int cmonth = Convert.ToInt32(FMonth);
            int cyear = Convert.ToInt32(FYear);

            ViewState["months"] = months;
            ViewState["cmonth"] = cmonth;
            ViewState["cyear"] = cyear;

            Doctor dr = new Doctor();
            dsDoctor = dr.getDocCat(div_code);

            if (months > 0)
            {
                foreach (DataRow dataRow in dsDoctor.Tables[0].Rows)
                {
                    MonColspan = MonColspan + Convert.ToInt16(dataRow["No_of_Visit"].ToString());
                }

                for (int j = 1; j <= months + 1; j++)
                {
                    TableCell tc_month = new TableCell();
                    tc_month.ColumnSpan = MonColspan + 3;
                    //tc_month.RowSpan = 2;
                    Literal lit_month = new Literal();
                    lit_month.Text = "&nbsp;" + sf.getMonthName(cmonth.ToString()) + "-" + cyear;
                    tc_month.Attributes.Add("Class", "rptCellBorder");
                    tc_month.BorderStyle = BorderStyle.Solid;
                    tc_month.BorderWidth = 1;

                    tc_month.HorizontalAlign = HorizontalAlign.Center;
                    //tc_month.Width = 200;
                    tc_month.Controls.Add(lit_month);
                    tr_header.Cells.Add(tc_month);
                    cmonth = cmonth + 1;
                    if (cmonth == 13)
                    {
                        cmonth = 1;
                        cyear = cyear + 1;
                    }
                }
            }

            tbl.Rows.Add(tr_header);

            //Sub Header
            months = Convert.ToInt16(ViewState["months"].ToString());
            cmonth = Convert.ToInt16(ViewState["cmonth"].ToString());
            cyear = Convert.ToInt16(ViewState["cyear"].ToString());


            if (months > 0)
            {
                TableRow tr_lst_det = new TableRow();

                for (int j = 1; j <= months + 1; j++)
                {

                    //TableCell tc_DR_fwd_mon = new TableCell();
                    //tc_DR_fwd_mon.BorderStyle = BorderStyle.Solid;
                    //tc_DR_fwd_mon.BorderWidth = 1;
                    ////tc_DR_fwd_mon.BackColor = System.Drawing.Color.LavenderBlush;
                    //tc_DR_fwd_mon.Width = 200;
                    //tc_DR_fwd_mon.RowSpan = 2;
                    //Literal lit_DR_fwd_mon = new Literal();
                    //tc_DR_fwd_mon.Attributes.Add("Class", "rptCellBorder");
                    //lit_DR_fwd_mon.Text = "<center>No of</br>FWD</center>";
                    //tc_DR_fwd_mon.Controls.Add(lit_DR_fwd_mon);
                    //tr_lst_det.Cells.Add(tc_DR_fwd_mon);

                    TableCell tc_DR_Total_mon = new TableCell();
                    tc_DR_Total_mon.BorderStyle = BorderStyle.Solid;
                    tc_DR_Total_mon.BorderWidth = 1;
                    //tc_DR_Total_mon.BackColor = System.Drawing.Color.Honeydew;
                    tc_DR_Total_mon.Width = 200;
                    tc_DR_Total_mon.RowSpan = 2;
                    Literal lit_DR_Total_mon = new Literal();
                    tc_DR_Total_mon.Attributes.Add("Class", "rptCellBorder");
                    lit_DR_Total_mon.Text = "<center>Total Drs List</center>";
                    tc_DR_Total_mon.Controls.Add(lit_DR_Total_mon);
                    tr_lst_det.Cells.Add(tc_DR_Total_mon);

                    if (dsDoctor.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dataRow in dsDoctor.Tables[0].Rows)
                        {
                            TableCell tc_catg_name = new TableCell();
                            tc_catg_name.BorderStyle = BorderStyle.Solid;
                            tc_catg_name.BorderWidth = 1;
                            tc_catg_name.BackColor = System.Drawing.Color.LemonChiffon;
                            tc_catg_name.Width = 30;
                            tc_catg_name.ColumnSpan = Convert.ToInt16(dataRow["No_of_Visit"].ToString());
                            Literal lit_catg_name = new Literal();
                            lit_catg_name.Text = "<center>" + dataRow["Doc_Cat_SName"].ToString() + '(' + dataRow["No_of_visit"].ToString() + ')' + "</center>";
                            tc_catg_name.Controls.Add(lit_catg_name);
                            tr_lst_det.Cells.Add(tc_catg_name);
                        }

                        TableCell tc_DR_Total_met = new TableCell();
                        tc_DR_Total_met.BorderStyle = BorderStyle.Solid;
                        tc_DR_Total_met.BorderWidth = 1;
                        //tc_DR_Total_met.BackColor = System.Drawing.Color.Honeydew;
                        tc_DR_Total_met.Width = 200;
                        tc_DR_Total_met.RowSpan = 2;
                        Literal lit_DR_Total_met = new Literal();
                        tc_DR_Total_met.Attributes.Add("Class", "rptCellBorder");
                        lit_DR_Total_met.Text = "<center>Total Met Drs</center>";
                        tc_DR_Total_met.Controls.Add(lit_DR_Total_met);
                        tr_lst_det.Cells.Add(tc_DR_Total_met);
                        tbl.Rows.Add(tr_lst_det);


                        //tbl.Rows.Add(tr_catg);
                    }

                    cmonth = cmonth + 1;
                    if (cmonth == 13)
                    {
                        cmonth = 1;
                        cyear = cyear + 1;
                    }
                }

                //tr_lst_det.BackColor = System.Drawing.Color.FromName("#A6A6D2");

                if (Session["sf_type"].ToString() == "1")
                {
                    tr_lst_det.Attributes.Add("Class", "MRBackColor");
                }
                else if (Session["sf_type"].ToString() == "2")
                {
                    tr_lst_det.Attributes.Add("Class", "MGRBackColor");
                }
                else
                {
                    tr_lst_det.Attributes.Add("Class", "Backcolor");
                }
                tbl.Rows.Add(tr_lst_det);
            }

            if (months > 0)
            {
                TableRow tr_catg = new TableRow();
                //tr_catg.BackColor = System.Drawing.Color.FromName("#A6A6D2");

                if (Session["sf_type"].ToString() == "1")
                {
                    tr_catg.Attributes.Add("Class", "MRBackColor");                    
                }
                else if (Session["sf_type"].ToString() == "2")
                {
                    tr_catg.Attributes.Add("Class", "MGRBackColor");
                }
                else
                {
                    tr_catg.Attributes.Add("Class", "Backcolor");
                }

                for (int j = 1; j <= (months + 1); j++)
                {
                    if (dsDoctor.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dataRow in dsDoctor.Tables[0].Rows)
                        {
                            if (Convert.ToInt16(dataRow["No_of_Visit"].ToString()) > 0)
                            {
                                int nvisit = Convert.ToInt16(dataRow["No_of_Visit"].ToString());
                                for (int i = 1; i <= nvisit; i++)
                                {
                                    TableCell tc_catg_name = new TableCell();
                                    tc_catg_name.BorderStyle = BorderStyle.Solid;
                                    tc_catg_name.BorderWidth = 1;
                                    //tc_catg_name.BackColor = System.Drawing.Color.LemonChiffon;
                                    tc_catg_name.Width = 30;
                                    Literal lit_catg_name = new Literal();
                                    lit_catg_name.Text = "<center>" + i + " Visit" + "</center>";
                                    tc_catg_name.Attributes.Add("Class", "rptCellBorder");
                                    tc_catg_name.Controls.Add(lit_catg_name);
                                    tr_catg.Cells.Add(tc_catg_name);
                                }
                            }
                        }

                        tbl.Rows.Add(tr_catg);


                        //tbl.Rows.Add(tr_catg);
                    }


                }
            }

            // Details Section
            string sURL = string.Empty;
            int iCount = 0;
            int iCnt = 0;
            int tot_met = 0;
            int tot_miss = 0;

            foreach (DataRow drFF in dsSalesForce.Tables[0].Rows)
            {
                ListedDR lstDR = new ListedDR();
                iCnt = lstDR.RecordCount(drFF["sf_code"].ToString());

                TableRow tr_det = new TableRow();
                tr_det.Attributes.Add("Class", "tblCellFont");
                tr_det.BackColor = System.Drawing.Color.White;
                iCount += 1;
                TableCell tc_det_SNo = new TableCell();
                Literal lit_det_SNo = new Literal();
                lit_det_SNo.Text = "<center>" + iCount.ToString() + "</center>";
                tc_det_SNo.BorderStyle = BorderStyle.Solid;
                tc_det_SNo.BorderWidth = 1;
                tc_det_SNo.Width = 50;
                tc_det_SNo.Controls.Add(lit_det_SNo);
                tr_det.Cells.Add(tc_det_SNo);

                TableCell tc_det_doc_code = new TableCell();
                Literal lit_det_doc_code = new Literal();
                lit_det_doc_code.Text = "&nbsp;" + drFF["sf_code"].ToString();
                tc_det_doc_code.BorderStyle = BorderStyle.Solid;
                tc_det_doc_code.BorderWidth = 1;
                tc_det_doc_code.Controls.Add(lit_det_doc_code);
                tc_det_doc_code.Visible = false;
                tr_det.Cells.Add(tc_det_doc_code);

                TableCell tc_det_doc_name = new TableCell();
                //Literal lit_det_doc_name = new Literal();
                //lit_det_doc_name.Text = "&nbsp;" + drFF["SF_Name"].ToString();
                HyperLink lit_det_doc_name = new HyperLink();
                lit_det_doc_name.Text = "&nbsp;" + drFF["SF_Name"].ToString();
                tc_det_doc_name.HorizontalAlign = HorizontalAlign.Left;
                if (drFF["SF_Type"].ToString() == "2")
                {
                    lit_det_doc_name.Attributes.Add("href", "javascript:showModalPopUp('" + drFF["sf_code"].ToString() + "', '" + FMonth + "', '" + FYear + "', '" + TMonth + "', '" + TYear + "', '" + sMode + "')");
                }
                tc_det_doc_name.BorderStyle = BorderStyle.Solid;
                tc_det_doc_name.BorderWidth = 1;
                tc_det_doc_name.Width = 900;
                tc_det_doc_name.Controls.Add(lit_det_doc_name);
                tr_det.Cells.Add(tc_det_doc_name);

                TableCell tc_det_sf_HQ = new TableCell();
                Literal lit_det_sf_hq = new Literal();
                lit_det_sf_hq.Text = "&nbsp;" + drFF["Sf_HQ"].ToString();
                tc_det_sf_HQ.BorderStyle = BorderStyle.Solid;
                tc_det_sf_HQ.BorderWidth = 1;
                tc_det_sf_HQ.Controls.Add(lit_det_sf_hq);
                //tc_det_sf_HQ.Visible = false;
                tr_det.Cells.Add(tc_det_sf_HQ);


                TableCell tc_det_sf_des = new TableCell();
                Literal lit_det_sf_des = new Literal();
                lit_det_sf_des.Text = "&nbsp;" + drFF["Designation_Short_Name"].ToString();
                tc_det_sf_des.BorderStyle = BorderStyle.Solid;
                tc_det_sf_des.BorderWidth = 1;
                tc_det_sf_des.Controls.Add(lit_det_sf_des);
                //tc_det_sf_HQ.Visible = false;
                tr_det.Cells.Add(tc_det_sf_des);


                months = Convert.ToInt16(ViewState["months"].ToString());
                cmonth = Convert.ToInt16(ViewState["cmonth"].ToString());
                cyear = Convert.ToInt16(ViewState["cyear"].ToString());

                if (months > 0)
                {
                    for (int j = 1; j <= months + 1; j++)
                    {
                        tot_met = 0;
                        tot_miss = 0;
                        doctor_total = 0;
                        if (cmonth == 12)
                        {
                            sCurrentDate = "01-01-" + (cyear + 1);
                        }
                        else
                        {
                            sCurrentDate = (cmonth + 1) + "-01-" + cyear;
                        }

                        dtCurrent = Convert.ToDateTime(sCurrentDate);

                        if (dsDoctor.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow dataRow in dsDoctor.Tables[0].Rows)
                            {
                                dsDoc = sf.Catg_LST_Report(drFF["sf_code"].ToString(), div_code, dtCurrent,cmonth,cyear, Convert.ToInt32(dataRow["Doc_Cat_Code"].ToString()));

                                if (dsDoc.Tables[0].Rows.Count > 0)
                                    tot_dr = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

                                doctor_total = doctor_total + Convert.ToInt16(tot_dr);
                            }

                            //TableCell tc_det_sf_fwd = new TableCell();
                            //Literal lit_det_sf_fwd = new Literal();
                            //lit_det_sf_fwd.Text = "&nbsp;" + doctor_total.ToString();
                            //tc_det_sf_fwd.BorderStyle = BorderStyle.Solid;
                            ////tc_det_sf_fwd.BackColor = System.Drawing.Color.LavenderBlush;
                            //tc_det_sf_fwd.BorderWidth = 1;
                            //tc_det_sf_fwd.HorizontalAlign = HorizontalAlign.Center;
                            //tc_det_sf_fwd.VerticalAlign = VerticalAlign.Middle;
                            //tc_det_sf_fwd.Controls.Add(lit_det_sf_fwd);
                            //tr_det.Cells.Add(tc_det_sf_fwd);


                            TableCell tc_det_sf_tot = new TableCell();
                            Literal lit_det_sf_tot = new Literal();
                            lit_det_sf_tot.Text = "&nbsp;" + doctor_total.ToString();
                            tc_det_sf_tot.BorderStyle = BorderStyle.Solid;
                            //tc_det_sf_tot.BackColor = System.Drawing.Color.Honeydew;
                            tc_det_sf_tot.BorderWidth = 1;
                            tc_det_sf_tot.HorizontalAlign = HorizontalAlign.Center;
                            tc_det_sf_tot.VerticalAlign = VerticalAlign.Middle;
                            tc_det_sf_tot.Controls.Add(lit_det_sf_tot);
                            tr_det.Cells.Add(tc_det_sf_tot);

                            DCR dc = new DCR();
                            foreach (DataRow dataRow in dsDoctor.Tables[0].Rows)
                            {
                                if (Convert.ToInt16(dataRow["No_of_Visit"].ToString()) > 0)
                                {
                                    int nvisit = Convert.ToInt16(dataRow["No_of_Visit"].ToString());
                                    for (int i = 1; i <= nvisit; i++)
                                    {
                                        dsDCR = dc.Catg_Visit_Report_noofvisit(drFF["sf_code"].ToString(), div_code, cmonth, cyear, Convert.ToInt32(dataRow["Doc_Cat_Code"].ToString()), i);

                                        if (dsDCR.Tables[0].Rows.Count > 0)
                                        {
                                            tot_dcr_dr = dsDCR.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                                        }
                                        else
                                        {
                                        }
                                        TableCell tc_lst_month = new TableCell();
                                        HyperLink hyp_lst_month = new HyperLink();
                                        if (tot_dcr_dr != "0" && tot_dcr_dr != "")
                                        {
                                            tot_met = tot_met + Convert.ToInt16(tot_dcr_dr);
                                            hyp_lst_month.Text = tot_dcr_dr;
                                            hyp_lst_month.Attributes.Add("href", "javascript:showVisitDR_type('" + drFF["sf_code"].ToString() + "', '" + cmonth.ToString() + "', '" + cyear.ToString() + "', '" + sMode + "','" + Convert.ToInt32(dataRow["Doc_Cat_Code"].ToString()) + "','" + i.ToString() + "')");
                                        }
                                        else
                                        {
                                            hyp_lst_month.Text = "-";
                                        }
                                        tc_lst_month.BorderStyle = BorderStyle.Solid;
                                        tc_lst_month.BorderWidth = 1;
                                        //tc_lst_month.BackColor = System.Drawing.Color.LemonChiffon;
                                        tc_lst_month.Width = 200;
                                        tc_lst_month.HorizontalAlign = HorizontalAlign.Center;
                                        tc_lst_month.VerticalAlign = VerticalAlign.Middle;
                                        tc_lst_month.Controls.Add(hyp_lst_month);
                                        tr_det.Cells.Add(tc_lst_month);
                                    }
                                }
                            }

                            TableCell tc_det_sf_tot_met = new TableCell();
                            HyperLink hy_det_sf_tot_met = new HyperLink();

                            if (tot_met > 0)
                            {
                                hy_det_sf_tot_met.Text = "&nbsp;" + tot_met.ToString();
                                hy_det_sf_tot_met.Attributes.Add("href", "javascript:showVisitDR_type('" + drFF["sf_code"].ToString() + "', '" + cmonth.ToString() + "', '" + cyear.ToString() + "', '" + sMode + "','' )");
                            }
                            else
                            {
                                hy_det_sf_tot_met.Text = "-";
                            }


                            tc_det_sf_tot_met.BorderStyle = BorderStyle.Solid;
                            //tc_det_sf_tot_met.BackColor = System.Drawing.Color.Honeydew;
                            tc_det_sf_tot_met.BorderWidth = 1;
                            tc_det_sf_tot_met.HorizontalAlign = HorizontalAlign.Center;
                            tc_det_sf_tot_met.VerticalAlign = VerticalAlign.Middle;
                            tc_det_sf_tot_met.Controls.Add(hy_det_sf_tot_met);
                            tr_det.Cells.Add(tc_det_sf_tot_met);

                            cmonth = cmonth + 1;
                            if (cmonth == 13)
                            {
                                cmonth = 1;
                                cyear = cyear + 1;
                            }

                        }
                    }

                    tbl.Rows.Add(tr_det);

                }
            }
        }
    }
    private void FillSpec()
    {
        tbl.Rows.Clear();
        doctor_total = 0;
        SalesForce sf = new SalesForce();
        dsSalesForce = sf.SF_ReportingTo_TourPlan(div_code, sf_code);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            TableRow tr_header = new TableRow();
            tr_header.BorderStyle = BorderStyle.Solid;
            tr_header.BorderWidth = 1;

            TableCell tc_SNo = new TableCell();
            tc_SNo.BorderStyle = BorderStyle.Solid;
            tc_SNo.BorderWidth = 1;
            tc_SNo.Width = 50;
            tc_SNo.RowSpan = 3;
            Literal lit_SNo = new Literal();
            lit_SNo.Text = "S.No";
            tc_SNo.Attributes.Add("Class", "rptCellBorder");
            tc_SNo.Controls.Add(lit_SNo);
            tr_header.Cells.Add(tc_SNo);
            //tr_header.BackColor = System.Drawing.Color.FromName("#A6A6D2");

            if (Session["sf_type"].ToString() == "1")
            {
                tr_header.Attributes.Add("Class", "MRBackColor");
            }
            else if (Session["sf_type"].ToString() == "2")
            {
                tr_header.Attributes.Add("Class", "MGRBackColor");
            }
            else
            {
                tr_header.Attributes.Add("Class", "Backcolor");
            }

            TableCell tc_DR_Code = new TableCell();
            tc_DR_Code.BorderStyle = BorderStyle.Solid;
            tc_DR_Code.BorderWidth = 1;
            tc_DR_Code.Width = 400;
            tc_DR_Code.RowSpan = 3;
            Literal lit_DR_Code = new Literal();
            lit_DR_Code.Text = "<center>SF Code</center>";
            tc_DR_Code.Controls.Add(lit_DR_Code);
            tc_DR_Code.Visible = false;
            tr_header.Cells.Add(tc_DR_Code);

            TableCell tc_DR_Name = new TableCell();
            tc_DR_Name.BorderStyle = BorderStyle.Solid;
            tc_DR_Name.BorderWidth = 1;
            tc_DR_Name.Width = 500;
            tc_DR_Name.RowSpan = 3;
            Literal lit_DR_Name = new Literal();
            lit_DR_Name.Text = "<center>Field Force</center>";
            tc_DR_Name.Attributes.Add("Class", "rptCellBorder");
            tc_DR_Name.Controls.Add(lit_DR_Name);
            tr_header.Cells.Add(tc_DR_Name);

            TableCell tc_DR_HQ = new TableCell();
            tc_DR_HQ.BorderStyle = BorderStyle.Solid;
            tc_DR_HQ.BorderWidth = 1;
            tc_DR_HQ.Width = 500;
            tc_DR_HQ.RowSpan = 3;
            Literal lit_DR_HQ = new Literal();
            lit_DR_HQ.Text = "<center>HQ</center>";
            tc_DR_HQ.Attributes.Add("Class", "rptCellBorder");
            tc_DR_HQ.Controls.Add(lit_DR_HQ);
            tr_header.Cells.Add(tc_DR_HQ);

            TableCell tc_DR_Des = new TableCell();
            tc_DR_Des.BorderStyle = BorderStyle.Solid;
            tc_DR_Des.BorderWidth = 1;
            tc_DR_Des.Width = 500;
            tc_DR_Des.RowSpan = 3;
            Literal lit_DR_Des = new Literal();
            lit_DR_Des.Text = "<center>Designation</center>";
            tc_DR_Des.Attributes.Add("Class", "rptCellBorder");
            tc_DR_Des.Controls.Add(lit_DR_Des);
            tr_header.Cells.Add(tc_DR_Des);

            int months = (Convert.ToInt32(TYear) - Convert.ToInt32(FYear)) * 12 + Convert.ToInt32(TMonth) - Convert.ToInt32(FMonth); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
            int cmonth = Convert.ToInt32(FMonth);
            int cyear = Convert.ToInt32(FYear);

            ViewState["months"] = months;
            ViewState["cmonth"] = cmonth;
            ViewState["cyear"] = cyear;

            Doctor dr = new Doctor();
            dsDoctor = dr.getDocSpec(div_code);

            if (months > 0)
            {
                for (int j = 1; j <= months + 1; j++)
                {

                    TableCell tc_month = new TableCell();
                    tc_month.ColumnSpan = (dsDoctor.Tables[0].Rows.Count * 2) + 3;
                    //tc_month.RowSpan = 2;
                    Literal lit_month = new Literal();
                    lit_month.Text = "&nbsp;" + sf.getMonthName(cmonth.ToString()) + "-" + cyear;
                    tc_month.Attributes.Add("Class", "rptCellBorder");
                    tc_month.BorderStyle = BorderStyle.Solid;
                    tc_month.BorderWidth = 1;
                    tc_month.HorizontalAlign = HorizontalAlign.Center;
                    //tc_month.Width = 200;
                    tc_month.Controls.Add(lit_month);
                    tr_header.Cells.Add(tc_month);
                    cmonth = cmonth + 1;
                    if (cmonth == 13)
                    {
                        cmonth = 1;
                        cyear = cyear + 1;
                    }
                }
            }

            tbl.Rows.Add(tr_header);

            //Sub Header
            months = Convert.ToInt16(ViewState["months"].ToString());
            cmonth = Convert.ToInt16(ViewState["cmonth"].ToString());
            cyear = Convert.ToInt16(ViewState["cyear"].ToString());


            if (months > 0)
            {
                TableRow tr_lst_det = new TableRow();
                for (int j = 1; j <= months + 1; j++)
                {
                    TableCell tc_DR_Total_mon = new TableCell();
                    tc_DR_Total_mon.BorderStyle = BorderStyle.Solid;
                    tc_DR_Total_mon.BorderWidth = 1;
                    //tc_DR_Total_mon.BackColor = System.Drawing.Color.LavenderBlush;
                    tc_DR_Total_mon.Width = 500;
                    tc_DR_Total_mon.RowSpan = 2;
                    Literal lit_DR_Total_mon = new Literal();
                    lit_DR_Total_mon.Text = "<center>Total Drs List</center>";
                    tc_DR_Total_mon.Attributes.Add("Class", "rptCellBorder");
                    tc_DR_Total_mon.Controls.Add(lit_DR_Total_mon);
                    tr_lst_det.Cells.Add(tc_DR_Total_mon);

                    TableCell tc_lst_month = new TableCell();
                    HyperLink lit_lst_month = new HyperLink();
                    lit_lst_month.Text = "Lst Drs";
                    tc_lst_month.Attributes.Add("Class", "rptCellBorder");
                    tc_lst_month.BorderStyle = BorderStyle.Solid;
                    tc_lst_month.HorizontalAlign = HorizontalAlign.Center;
                    tc_lst_month.BorderWidth = 1;
                    //tc_lst_month.BackColor = System.Drawing.Color.Honeydew;
                    tc_lst_month.ColumnSpan = dsDoctor.Tables[0].Rows.Count;
                    tc_lst_month.Controls.Add(lit_lst_month);
                    tr_lst_det.Cells.Add(tc_lst_month);

                    TableCell tc_msd_month = new TableCell();
                    HyperLink lit_msd_month = new HyperLink();
                    lit_msd_month.Text = "Met Drs";

                    tc_msd_month.BorderStyle = BorderStyle.Solid;
                    tc_msd_month.HorizontalAlign = HorizontalAlign.Center;
                    tc_msd_month.BorderWidth = 1;
                    //tc_msd_month.BackColor = System.Drawing.Color.PapayaWhip;
                    tc_msd_month.Attributes.Add("Class", "rptCellBorder");
                    tc_msd_month.ColumnSpan = dsDoctor.Tables[0].Rows.Count;
                    tc_msd_month.Controls.Add(lit_msd_month);
                    tr_lst_det.Cells.Add(tc_msd_month);

                    TableCell tc_DR_Total_mon_tot = new TableCell();
                    tc_DR_Total_mon_tot.BorderStyle = BorderStyle.Solid;
                    tc_DR_Total_mon_tot.BorderWidth = 1;
                    tc_DR_Total_mon_tot.BackColor = System.Drawing.Color.LemonChiffon;
                    tc_DR_Total_mon_tot.Width = 500;
                    tc_DR_Total_mon_tot.ColumnSpan = 2;
                    Literal lit_DR_Total_mon_tot = new Literal();
                    lit_DR_Total_mon_tot.Text = "<center>Total</center>";
                    tc_DR_Total_mon_tot.Controls.Add(lit_DR_Total_mon_tot);
                    tr_lst_det.Cells.Add(tc_DR_Total_mon_tot);

                    cmonth = cmonth + 1;
                    if (cmonth == 13)
                    {
                        cmonth = 1;
                        cyear = cyear + 1;
                    }
                }

                if (Session["sf_type"].ToString() == "1")
                {
                    tr_lst_det.Attributes.Add("Class", "MRBackColor");

                }
                else if (Session["sf_type"].ToString() == "2")
                {
                    tr_lst_det.Attributes.Add("Class", "MGRBackColor");
                }
                else
                {
                    tr_lst_det.Attributes.Add("Class", "Backcolor");
                }
                //tr_lst_det.BackColor = System.Drawing.Color.FromName("#A6A6D2");
                tbl.Rows.Add(tr_lst_det);
            }

            if (months > 0)
            {
                TableRow tr_catg = new TableRow();
                //tr_catg.BackColor = System.Drawing.Color.FromName("#A6A6D2");

                if (Session["sf_type"].ToString() == "1")
                {
                    tr_catg.Attributes.Add("Class", "MRBackColor");

                }
                else if (Session["sf_type"].ToString() == "2")
                {
                    tr_catg.Attributes.Add("Class", "MGRBackColor");
                }
                else
                {
                    tr_catg.Attributes.Add("Class", "Backcolor");
                }

                for (int j = 1; j <= (months + 1) * 2; j++)
                {
                    if (dsDoctor.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dataRow in dsDoctor.Tables[0].Rows)
                        {
                            TableCell tc_catg_name = new TableCell();
                            tc_catg_name.BorderStyle = BorderStyle.Solid;
                            tc_catg_name.BorderWidth = 1;
                            tc_catg_name.Width = 30;
                            if ((j % 2) == 1)
                            {
                                //tc_catg_name.BackColor = System.Drawing.Color.Honeydew;
                            }
                            else
                            {
                                //tc_catg_name.BackColor = System.Drawing.Color.PapayaWhip;
                            }
                            Literal lit_catg_name = new Literal();
                            lit_catg_name.Text = "<center>" + dataRow["Doc_Cat_Name"].ToString() + "</center>";
                            tc_catg_name.Controls.Add(lit_catg_name);
                            tr_catg.Cells.Add(tc_catg_name);
                        }


                        if ((j % 2) == 1)
                        {
                        }
                        else
                        {
                            TableCell tc_catg_tot_met = new TableCell();
                            tc_catg_tot_met.BorderStyle = BorderStyle.Solid;
                            tc_catg_tot_met.BorderWidth = 1;
                            //tc_catg_tot_met.BackColor = System.Drawing.Color.LemonChiffon;
                            tc_catg_tot_met.Width = 30;
                            Literal lit_catg_tot_met = new Literal();
                            lit_catg_tot_met.Text = "<center>Met</center>";
                            tc_catg_tot_met.Attributes.Add("Class", "rptCellBorder");
                            tc_catg_tot_met.Controls.Add(lit_catg_tot_met);
                            tr_catg.Cells.Add(tc_catg_tot_met);

                            TableCell tc_catg_tot_miss = new TableCell();
                            tc_catg_tot_miss.BorderStyle = BorderStyle.Solid;
                            tc_catg_tot_miss.BorderWidth = 1;
                            //tc_catg_tot_miss.BackColor = System.Drawing.Color.LemonChiffon;
                            tc_catg_tot_miss.Width = 30;
                            Literal lit_catg_tot_miss = new Literal();
                            lit_catg_tot_miss.Text = "<center>Missed</center>";
                            tc_catg_tot_miss.Attributes.Add("Class", "rptCellBorder");
                            tc_catg_tot_miss.Controls.Add(lit_catg_tot_miss);
                            tr_catg.Cells.Add(tc_catg_tot_miss);
                        }

                        tbl.Rows.Add(tr_catg);
                    }
                }
            }

            // Details Section
            string sURL = string.Empty;
            int iCount = 0;
            int iCnt = 0;
            int tot_met = 0;
            int tot_miss = 0;

            foreach (DataRow drFF in dsSalesForce.Tables[0].Rows)
            {
                ListedDR lstDR = new ListedDR();
                iCnt = lstDR.RecordCount(drFF["sf_code"].ToString());

                TableRow tr_det = new TableRow();
                tr_det.Attributes.Add("Class", "tblCellFont");
                tr_det.BackColor = System.Drawing.Color.White;
                iCount += 1;
                TableCell tc_det_SNo = new TableCell();
                Literal lit_det_SNo = new Literal();
                lit_det_SNo.Text = "<center>" + iCount.ToString() + "</center>";
                tc_det_SNo.BorderStyle = BorderStyle.Solid;
                tc_det_SNo.BorderWidth = 1;
                tc_det_SNo.Width = 50;
                tc_det_SNo.Controls.Add(lit_det_SNo);
                tr_det.Cells.Add(tc_det_SNo);

                TableCell tc_det_doc_code = new TableCell();
                Literal lit_det_doc_code = new Literal();
                lit_det_doc_code.Text = "&nbsp;" + drFF["sf_code"].ToString();
                tc_det_doc_code.BorderStyle = BorderStyle.Solid;
                tc_det_doc_code.BorderWidth = 1;
                tc_det_doc_code.Controls.Add(lit_det_doc_code);
                tc_det_doc_code.Visible = false;
                tr_det.Cells.Add(tc_det_doc_code);

                TableCell tc_det_doc_name = new TableCell();
                //Literal lit_det_doc_name = new Literal();
                //lit_det_doc_name.Text = "&nbsp;" + drFF["SF_Name"].ToString();

                HyperLink lit_det_doc_name = new HyperLink();
                lit_det_doc_name.Text = "&nbsp;" + drFF["SF_Name"].ToString();
                tc_det_doc_name.HorizontalAlign = HorizontalAlign.Left;
                if (drFF["SF_Type"].ToString() == "2")
                {

                    lit_det_doc_name.Attributes.Add("href", "javascript:showModalPopUp('" + drFF["sf_code"].ToString() + "', '" + FMonth + "', '" + FYear + "', '" + TMonth + "', '" + TYear + "', '" + sMode + "')");
                }
                tc_det_doc_name.BorderStyle = BorderStyle.Solid;
                tc_det_doc_name.BorderWidth = 1;
                tc_det_doc_name.Width = 500;
                tc_det_doc_name.Controls.Add(lit_det_doc_name);
                tr_det.Cells.Add(tc_det_doc_name);

                TableCell tc_det_sf_HQ = new TableCell();
                Literal lit_det_sf_hq = new Literal();
                lit_det_sf_hq.Text = "&nbsp;" + drFF["Sf_HQ"].ToString();
                tc_det_sf_HQ.BorderStyle = BorderStyle.Solid;
                tc_det_sf_HQ.BorderWidth = 1;
                tc_det_sf_HQ.Controls.Add(lit_det_sf_hq);
                //tc_det_sf_HQ.Visible = false;
                tr_det.Cells.Add(tc_det_sf_HQ);


                TableCell tc_det_sf_des = new TableCell();
                Literal lit_det_sf_des = new Literal();
                lit_det_sf_des.Text = "&nbsp;" + drFF["Designation_Short_Name"].ToString();
                tc_det_sf_des.BorderStyle = BorderStyle.Solid;
                tc_det_sf_des.BorderWidth = 1;
                tc_det_sf_des.Controls.Add(lit_det_sf_des);
                //tc_det_sf_HQ.Visible = false;
                tr_det.Cells.Add(tc_det_sf_des);

                months = Convert.ToInt16(ViewState["months"].ToString());
                cmonth = Convert.ToInt16(ViewState["cmonth"].ToString());
                cyear = Convert.ToInt16(ViewState["cyear"].ToString());

                if (months > 0)
                {
                    for (int j = 1; j <= months + 1; j++)
                    {
                        tot_met = 0;
                        tot_miss = 0;
                        doctor_total = 0;
                        if (cmonth == 12)
                        {
                            sCurrentDate = "01-01-" + (cyear + 1);
                        }
                        else
                        {
                            sCurrentDate = (cmonth + 1) + "-01-" + cyear;
                        }

                        dtCurrent = Convert.ToDateTime(sCurrentDate);

                        //dsDoc = sf.MissedCallReport(drFF["sf_code"].ToString(), div_code, dtCurrent);
                        //dsDCR = sf.DCR_MissedCallReport(drFF["sf_code"].ToString(), div_code, cmonth, cyear);

                        //if (dsDoc.Tables[0].Rows.Count > 0)
                        //    tot_dr = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                        //if (dsDCR.Tables[0].Rows.Count > 0)
                        //    tot_dcr_dr = dsDCR.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

                        if (dsDoctor.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow dataRow in dsDoctor.Tables[0].Rows)
                            {
                                dsDoc = sf.Spec_LST_Report(drFF["sf_code"].ToString(), div_code, dtCurrent, cmonth, cyear, Convert.ToInt32(dataRow["Doc_Cat_Code"].ToString()));

                                if (dsDoc.Tables[0].Rows.Count > 0)
                                    tot_dr = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

                                doctor_total = doctor_total + Convert.ToInt16(tot_dr);
                            }

                            TableCell tc_det_sf_tot = new TableCell();
                            Literal lit_det_sf_tot = new Literal();
                            lit_det_sf_tot.Text = "&nbsp;" + doctor_total.ToString();
                            tc_det_sf_tot.BorderStyle = BorderStyle.Solid;
                            //tc_det_sf_tot.BackColor = System.Drawing.Color.LavenderBlush;
                            tc_det_sf_tot.BorderWidth = 1;
                            tc_det_sf_tot.HorizontalAlign = HorizontalAlign.Center;
                            tc_det_sf_tot.VerticalAlign = VerticalAlign.Middle;
                            tc_det_sf_tot.Controls.Add(lit_det_sf_tot);
                            tr_det.Cells.Add(tc_det_sf_tot);


                            foreach (DataRow dataRow in dsDoctor.Tables[0].Rows)
                            {
                                dsDoc = sf.Spec_LST_Report(drFF["sf_code"].ToString(), div_code, dtCurrent, cmonth, cyear, Convert.ToInt32(dataRow["Doc_Cat_Code"].ToString()));

                                if (dsDoc.Tables[0].Rows.Count > 0)
                                    tot_dr = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

                                TableCell tc_lst_month = new TableCell();
                                HyperLink hyp_lst_month = new HyperLink();
                                if (tot_dr != "0")
                                {

                                    tot_miss = tot_miss + Convert.ToInt16(tot_dr);
                                    hyp_lst_month.Text = tot_dr;
                                }
                                else
                                {
                                    hyp_lst_month.Text = "-";
                                }


                                tc_lst_month.BorderStyle = BorderStyle.Solid;
                                tc_lst_month.BorderWidth = 1;
                                tc_lst_month.Width = 200;
                                //tc_lst_month.BackColor = System.Drawing.Color.Honeydew;
                                tc_lst_month.HorizontalAlign = HorizontalAlign.Center;
                                tc_lst_month.VerticalAlign = VerticalAlign.Middle;
                                tc_lst_month.Controls.Add(hyp_lst_month);
                                tr_det.Cells.Add(tc_lst_month);
                            }
                        }

                        if (dsDoctor.Tables[0].Rows.Count > 0)
                        {
                            DCR dc = new DCR();
                            foreach (DataRow dataRow in dsDoctor.Tables[0].Rows)
                            {
                                dsDoc = sf.Spec_LST_Report(drFF["sf_code"].ToString(), div_code, dtCurrent, cmonth, cyear, Convert.ToInt32(dataRow["Doc_Cat_Code"].ToString()));
                                if (dsDoc.Tables[0].Rows.Count > 0)
                                    tot_dr = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

                                dsDCR = dc.Spec_Visit_Report(drFF["sf_code"].ToString(), div_code, cmonth, cyear, Convert.ToInt32(dataRow["Doc_Cat_Code"].ToString()));
                                if (dsDCR.Tables[0].Rows.Count > 0)
                                    tot_dcr_dr = dsDCR.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

                                TableCell tc_msd_month = new TableCell();
                                HyperLink hyp_msd_month = new HyperLink();
                                //if (tot_dr != "0")
                                if (tot_dcr_dr != "0")
                                {
                                    //imissed_dr = Convert.ToInt16(tot_dr) - Convert.ToInt16(tot_dcr_dr);
                                    imissed_dr = Convert.ToInt16(tot_dcr_dr);
                                    hyp_msd_month.Text = imissed_dr.ToString();
                                    if (imissed_dr > 0)
                                    {
                                        tot_met = tot_met + imissed_dr;
                                        hyp_msd_month.Attributes.Add("href", "javascript:showVisitDR_type('" + drFF["sf_code"].ToString() + "', '" + cmonth.ToString() + "', '" + cyear.ToString() + "', '" + sMode + "','" + Convert.ToInt32(dataRow["Doc_Cat_Code"].ToString()) + "')");
                                    }
                                }
                                else
                                {
                                    hyp_msd_month.Text = "-";
                                }

                                tc_msd_month.BorderStyle = BorderStyle.Solid;
                                tc_msd_month.BorderWidth = 1;
                                //tc_msd_month.BackColor = System.Drawing.Color.PapayaWhip;
                                tc_msd_month.Width = 200;
                                tc_msd_month.HorizontalAlign = HorizontalAlign.Center;
                                tc_msd_month.VerticalAlign = VerticalAlign.Middle;
                                tc_msd_month.Controls.Add(hyp_msd_month);
                                tr_det.Cells.Add(tc_msd_month);
                            }
                        }
                        TableCell tc_det_sf_tt = new TableCell();
                        //Literal lit_det_sf_tt = new Literal();
                        HyperLink lit_det_sf_tt = new HyperLink();
                        if (tot_met > 0)
                        {
                            lit_det_sf_tt.Text = tot_met.ToString();
                            lit_det_sf_tt.Attributes.Add("href", "javascript:showVisitDR_type('" + drFF["sf_code"].ToString() + "', '" + cmonth.ToString() + "', '" + cyear.ToString() + "', '" + sMode + "','' )");
                        }
                        else
                        {
                            lit_det_sf_tt.Text = "-";
                        }

                        tc_det_sf_tt.BorderStyle = BorderStyle.Solid;
                        //tc_det_sf_tt.BackColor = System.Drawing.Color.LemonChiffon;
                        tc_det_sf_tt.BorderWidth = 1;
                        tc_det_sf_tt.HorizontalAlign = HorizontalAlign.Center;
                        tc_det_sf_tt.VerticalAlign = VerticalAlign.Middle;
                        tc_det_sf_tt.Controls.Add(lit_det_sf_tt);
                        tr_det.Cells.Add(tc_det_sf_tt);

                        //if(tot_met > 
                        tot_miss = tot_miss - tot_met;

                        TableCell tc_det_sf_tt_miss = new TableCell();
                        Literal lit_det_sf_tt_miss = new Literal();
                        lit_det_sf_tt_miss.Text = "&nbsp;" + tot_miss.ToString();
                        tc_det_sf_tt_miss.BorderStyle = BorderStyle.Solid;
                        //tc_det_sf_tt_miss.BackColor = System.Drawing.Color.LemonChiffon;
                        tc_det_sf_tt_miss.BorderWidth = 1;
                        tc_det_sf_tt_miss.HorizontalAlign = HorizontalAlign.Center;
                        tc_det_sf_tt_miss.VerticalAlign = VerticalAlign.Middle;
                        tc_det_sf_tt_miss.Controls.Add(lit_det_sf_tt_miss);
                        tr_det.Cells.Add(tc_det_sf_tt_miss);

                        cmonth = cmonth + 1;
                        if (cmonth == 13)
                        {
                            cmonth = 1;
                            cyear = cyear + 1;
                        }
                    }
                }
                tbl.Rows.Add(tr_det);

            }
        }
    }

    private void FillClass()
    {
        tbl.Rows.Clear();
        doctor_total = 0;

        SalesForce sf = new SalesForce();
        dsSalesForce = sf.SF_ReportingTo_TourPlan(div_code, sf_code);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            TableRow tr_header = new TableRow();
            tr_header.BorderStyle = BorderStyle.Solid;
            tr_header.BorderWidth = 1;

            TableCell tc_SNo = new TableCell();
            tc_SNo.BorderStyle = BorderStyle.Solid;
            tc_SNo.BorderWidth = 1;
            tc_SNo.Width = 50;
            tc_SNo.RowSpan = 3;
            Literal lit_SNo = new Literal();
            lit_SNo.Text = "S.No";
            tc_SNo.Attributes.Add("Class", "rptCellBorder");
            tc_SNo.Controls.Add(lit_SNo);
            tr_header.Cells.Add(tc_SNo);
            //tr_header.BackColor = System.Drawing.Color.FromName("#A6A6D2");

            if (Session["sf_type"].ToString() == "1")
            {
                tr_header.Attributes.Add("Class", "MRBackColor");

            }
            else if (Session["sf_type"].ToString() == "2")
            {
                tr_header.Attributes.Add("Class", "MGRBackColor");
            }
            else
            {
                tr_header.Attributes.Add("Class", "Backcolor");
            }

            TableCell tc_DR_Code = new TableCell();
            tc_DR_Code.BorderStyle = BorderStyle.Solid;
            tc_DR_Code.BorderWidth = 1;
            tc_DR_Code.Width = 400;
            tc_DR_Code.RowSpan = 3;
            Literal lit_DR_Code = new Literal();
            lit_DR_Code.Text = "<center>SF Code</center>";
            tc_DR_Code.Controls.Add(lit_DR_Code);
            tc_DR_Code.Visible = false;
            tr_header.Cells.Add(tc_DR_Code);

            TableCell tc_DR_Name = new TableCell();
            tc_DR_Name.BorderStyle = BorderStyle.Solid;
            tc_DR_Name.BorderWidth = 1;
            tc_DR_Name.Width = 900;
            tc_DR_Name.RowSpan = 3;
            Literal lit_DR_Name = new Literal();
            tc_DR_Name.Attributes.Add("Class", "rptCellBorder");
            lit_DR_Name.Text = "<center>Field Force</center>";
            tc_DR_Name.Controls.Add(lit_DR_Name);
            tr_header.Cells.Add(tc_DR_Name);

            TableCell tc_DR_HQ = new TableCell();
            tc_DR_HQ.BorderStyle = BorderStyle.Solid;
            tc_DR_HQ.BorderWidth = 1;
            tc_DR_HQ.Width = 500;
            tc_DR_HQ.RowSpan = 3;
            Literal lit_DR_HQ = new Literal();
            tc_DR_HQ.Attributes.Add("Class", "rptCellBorder");
            lit_DR_HQ.Text = "<center>HQ</center>";
            tc_DR_HQ.Controls.Add(lit_DR_HQ);
            tr_header.Cells.Add(tc_DR_HQ);

            TableCell tc_DR_Des = new TableCell();
            tc_DR_Des.BorderStyle = BorderStyle.Solid;
            tc_DR_Des.BorderWidth = 1;
            tc_DR_Des.Width = 500;
            tc_DR_Des.RowSpan = 3;
            Literal lit_DR_Des = new Literal();
            tc_DR_Des.Attributes.Add("Class", "rptCellBorder");
            lit_DR_Des.Text = "<center>Designation</center>";
            tc_DR_Des.Controls.Add(lit_DR_Des);
            tr_header.Cells.Add(tc_DR_Des);

            int months = (Convert.ToInt32(TYear) - Convert.ToInt32(FYear)) * 12 + Convert.ToInt32(TMonth) - Convert.ToInt32(FMonth); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
            int cmonth = Convert.ToInt32(FMonth);
            int cyear = Convert.ToInt32(FYear);

            ViewState["months"] = months;
            ViewState["cmonth"] = cmonth;
            ViewState["cyear"] = cyear;

            Doctor dr = new Doctor();
            dsDoctor = dr.getDocClass(div_code);

            if (months > 0)
            {
                for (int j = 1; j <= months + 1; j++)
                {

                    TableCell tc_month = new TableCell();
                    tc_month.ColumnSpan = (dsDoctor.Tables[0].Rows.Count * 2) + 3;
                    //tc_month.RowSpan = 2;
                    Literal lit_month = new Literal();
                    tc_month.Attributes.Add("Class", "rptCellBorder");
                    lit_month.Text = "&nbsp;" + sf.getMonthName(cmonth.ToString()) + "-" + cyear;
                    tc_month.BorderStyle = BorderStyle.Solid;
                    tc_month.BorderWidth = 1;

                    tc_month.HorizontalAlign = HorizontalAlign.Center;
                    //tc_month.Width = 200;
                    tc_month.Controls.Add(lit_month);
                    tr_header.Cells.Add(tc_month);
                    cmonth = cmonth + 1;
                    if (cmonth == 13)
                    {
                        cmonth = 1;
                        cyear = cyear + 1;
                    }
                }
            }

            tbl.Rows.Add(tr_header);

            //Sub Header
            months = Convert.ToInt16(ViewState["months"].ToString());
            cmonth = Convert.ToInt16(ViewState["cmonth"].ToString());
            cyear = Convert.ToInt16(ViewState["cyear"].ToString());


            if (months > 0)
            {
                TableRow tr_lst_det = new TableRow();
                for (int j = 1; j <= months + 1; j++)
                {
                    TableCell tc_DR_Total_mon = new TableCell();
                    tc_DR_Total_mon.BorderStyle = BorderStyle.Solid;
                    tc_DR_Total_mon.BorderWidth = 1;
                    tc_DR_Total_mon.BackColor = System.Drawing.Color.LavenderBlush;
                    tc_DR_Total_mon.Width = 500;
                    tc_DR_Total_mon.RowSpan = 2;
                    Literal lit_DR_Total_mon = new Literal();
                    tc_DR_Total_mon.Attributes.Add("Class", "rptCellBorder");
                    lit_DR_Total_mon.Text = "<center>Total Drs List</center>";
                    tc_DR_Total_mon.Controls.Add(lit_DR_Total_mon);
                    tr_lst_det.Cells.Add(tc_DR_Total_mon);

                    TableCell tc_lst_month = new TableCell();
                    HyperLink lit_lst_month = new HyperLink();
                    lit_lst_month.Text = "Lst Drs";

                    tc_lst_month.BorderStyle = BorderStyle.Solid;
                    tc_lst_month.HorizontalAlign = HorizontalAlign.Center;
                    tc_lst_month.BorderWidth = 1;
                    //tc_lst_month.BackColor = System.Drawing.Color.Honeydew;
                    tc_lst_month.Attributes.Add("Class", "rptCellBorder");
                    tc_lst_month.ColumnSpan = dsDoctor.Tables[0].Rows.Count;
                    tc_lst_month.Controls.Add(lit_lst_month);
                    tr_lst_det.Cells.Add(tc_lst_month);

                    TableCell tc_msd_month = new TableCell();
                    HyperLink lit_msd_month = new HyperLink();
                    lit_msd_month.Text = "Met Drs";

                    tc_msd_month.BorderStyle = BorderStyle.Solid;
                    tc_msd_month.HorizontalAlign = HorizontalAlign.Center;
                    tc_msd_month.BorderWidth = 1;
                    //tc_msd_month.BackColor = System.Drawing.Color.PapayaWhip;
                    tc_msd_month.Attributes.Add("Class", "rptCellBorder");
                    tc_msd_month.ColumnSpan = dsDoctor.Tables[0].Rows.Count;
                    tc_msd_month.Controls.Add(lit_msd_month);
                    tr_lst_det.Cells.Add(tc_msd_month);

                    TableCell tc_DR_Total_mon_tot = new TableCell();
                    tc_DR_Total_mon_tot.BorderStyle = BorderStyle.Solid;
                    tc_DR_Total_mon_tot.BorderWidth = 1;
                    //tc_DR_Total_mon_tot.BackColor = System.Drawing.Color.LemonChiffon;
                    tc_DR_Total_mon_tot.Width = 500;
                    tc_DR_Total_mon_tot.ColumnSpan = 2;
                    Literal lit_DR_Total_mon_tot = new Literal();
                    tc_DR_Total_mon_tot.Attributes.Add("Class", "rptCellBorder");
                    lit_DR_Total_mon_tot.Text = "<center>Total</center>";
                    tc_DR_Total_mon_tot.Controls.Add(lit_DR_Total_mon_tot);
                    tr_lst_det.Cells.Add(tc_DR_Total_mon_tot);

                    cmonth = cmonth + 1;
                    if (cmonth == 13)
                    {
                        cmonth = 1;
                        cyear = cyear + 1;
                    }
                }

                //tr_lst_det.BackColor = System.Drawing.Color.FromName("#A6A6D2");
                if (Session["sf_type"].ToString() == "1")
                {
                    tr_lst_det.Attributes.Add("Class", "MRBackColor");
                }
                else if (Session["sf_type"].ToString() == "2")
                {
                    tr_lst_det.Attributes.Add("Class", "MGRBackColor");
                }
                else
                {
                    tr_lst_det.Attributes.Add("Class", "Backcolor");
                }
                tbl.Rows.Add(tr_lst_det);
            }

            if (months > 0)
            {
                TableRow tr_catg = new TableRow();
                //tr_catg.BackColor = System.Drawing.Color.FromName("#A6A6D2");

                if (Session["sf_type"].ToString() == "1")
                {
                    tr_catg.Attributes.Add("Class", "MRBackColor");
                }
                else if (Session["sf_type"].ToString() == "2")
                {
                    tr_catg.Attributes.Add("Class", "MGRBackColor");
                }
                else
                {
                    tr_catg.Attributes.Add("Class", "Backcolor");
                }

                for (int j = 1; j <= (months + 1) * 2; j++)
                {
                    if (dsDoctor.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dataRow in dsDoctor.Tables[0].Rows)
                        {
                            TableCell tc_catg_name = new TableCell();
                            tc_catg_name.BorderStyle = BorderStyle.Solid;
                            tc_catg_name.BorderWidth = 1;
                            if ((j % 2) == 1)
                            {
                                //tc_catg_name.BackColor = System.Drawing.Color.Honeydew;
                            }
                            else
                            {
                                //tc_catg_name.BackColor = System.Drawing.Color.PapayaWhip;
                            }
                            tc_catg_name.Width = 30;
                            Literal lit_catg_name = new Literal();
                            lit_catg_name.Text = "<center>" + dataRow["Doc_Cat_Name"].ToString() + "</center>";
                            tc_catg_name.Attributes.Add("Class", "rptCellBorder");
                            tc_catg_name.Controls.Add(lit_catg_name);
                            tr_catg.Cells.Add(tc_catg_name);
                        }


                        if ((j % 2) == 1)
                        {
                        }
                        else
                        {
                            TableCell tc_catg_tot_met = new TableCell();
                            tc_catg_tot_met.BorderStyle = BorderStyle.Solid;
                            tc_catg_tot_met.BorderWidth = 1;
                            //tc_catg_tot_met.BackColor = System.Drawing.Color.LemonChiffon;
                            tc_catg_tot_met.Width = 30;
                            Literal lit_catg_tot_met = new Literal();
                            lit_catg_tot_met.Text = "<center>Met</center>";
                            tc_catg_tot_met.Attributes.Add("Class", "rptCellBorder");
                            tc_catg_tot_met.Controls.Add(lit_catg_tot_met);
                            tr_catg.Cells.Add(tc_catg_tot_met);

                            TableCell tc_catg_tot_miss = new TableCell();
                            tc_catg_tot_miss.BorderStyle = BorderStyle.Solid;
                            tc_catg_tot_miss.BorderWidth = 1;
                            //tc_catg_tot_miss.BackColor = System.Drawing.Color.LemonChiffon;
                            tc_catg_tot_miss.Width = 30;
                            Literal lit_catg_tot_miss = new Literal();
                            lit_catg_tot_miss.Text = "<center>Missed</center>";
                            tc_catg_tot_met.Attributes.Add("Class", "rptCellBorder");
                            tc_catg_tot_miss.Controls.Add(lit_catg_tot_miss);
                            tr_catg.Cells.Add(tc_catg_tot_miss);
                        }

                        tbl.Rows.Add(tr_catg);
                    }
                }
            }

            // Details Section
            string sURL = string.Empty;
            int iCount = 0;
            int iCnt = 0;
            int tot_met = 0;
            int tot_miss = 0;

            foreach (DataRow drFF in dsSalesForce.Tables[0].Rows)
            {
                ListedDR lstDR = new ListedDR();
                iCnt = lstDR.RecordCount(drFF["sf_code"].ToString());

                TableRow tr_det = new TableRow();
                tr_det.Attributes.Add("Class", "tblCellFont");
                tr_det.BackColor = System.Drawing.Color.White;
                iCount += 1;
                TableCell tc_det_SNo = new TableCell();
                Literal lit_det_SNo = new Literal();
                lit_det_SNo.Text = "<center>" + iCount.ToString() + "</center>";
                tc_det_SNo.BorderStyle = BorderStyle.Solid;
                tc_det_SNo.BorderWidth = 1;
                tc_det_SNo.Width = 50;
                tc_det_SNo.Controls.Add(lit_det_SNo);
                tr_det.Cells.Add(tc_det_SNo);

                TableCell tc_det_doc_code = new TableCell();
                Literal lit_det_doc_code = new Literal();
                lit_det_doc_code.Text = "&nbsp;" + drFF["sf_code"].ToString();
                tc_det_doc_code.BorderStyle = BorderStyle.Solid;
                tc_det_doc_code.BorderWidth = 1;
                tc_det_doc_code.Controls.Add(lit_det_doc_code);
                tc_det_doc_code.Visible = false;
                tr_det.Cells.Add(tc_det_doc_code);

                TableCell tc_det_doc_name = new TableCell();
                //Literal lit_det_doc_name = new Literal();
                //lit_det_doc_name.Text = "&nbsp;" + drFF["SF_Name"].ToString();
                HyperLink lit_det_doc_name = new HyperLink();
                lit_det_doc_name.Text = "&nbsp;" + drFF["SF_Name"].ToString();
                tc_det_doc_name.HorizontalAlign = HorizontalAlign.Left;
                lit_det_doc_name.Attributes.Add("href", "javascript:showModalPopUp('" + drFF["sf_code"].ToString() + "', '" + FMonth + "', '" + FYear + "', '" + TMonth + "', '" + TYear + "', '" + sMode + "')");

                tc_det_doc_name.BorderStyle = BorderStyle.Solid;
                tc_det_doc_name.BorderWidth = 1;
                tc_det_doc_name.Width = 900;
                tc_det_doc_name.Controls.Add(lit_det_doc_name);
                tr_det.Cells.Add(tc_det_doc_name);

                TableCell tc_det_sf_HQ = new TableCell();
                Literal lit_det_sf_hq = new Literal();
                lit_det_sf_hq.Text = "&nbsp;" + drFF["Sf_HQ"].ToString();
                tc_det_sf_HQ.BorderStyle = BorderStyle.Solid;
                tc_det_sf_HQ.BorderWidth = 1;
                tc_det_sf_HQ.Controls.Add(lit_det_sf_hq);
                //tc_det_sf_HQ.Visible = false;
                tr_det.Cells.Add(tc_det_sf_HQ);


                TableCell tc_det_sf_des = new TableCell();
                Literal lit_det_sf_des = new Literal();
                lit_det_sf_des.Text = "&nbsp;" + drFF["Designation_Short_Name"].ToString();
                tc_det_sf_des.BorderStyle = BorderStyle.Solid;
                tc_det_sf_des.BorderWidth = 1;
                tc_det_sf_des.Controls.Add(lit_det_sf_des);
                //tc_det_sf_HQ.Visible = false;
                tr_det.Cells.Add(tc_det_sf_des);

                months = Convert.ToInt16(ViewState["months"].ToString());
                cmonth = Convert.ToInt16(ViewState["cmonth"].ToString());
                cyear = Convert.ToInt16(ViewState["cyear"].ToString());

                if (months > 0)
                {
                    for (int j = 1; j <= months + 1; j++)
                    {
                        tot_met = 0;
                        tot_miss = 0;
                        doctor_total = 0;
                        if (cmonth == 12)
                        {
                            sCurrentDate = "01-01-" + (cyear + 1);
                        }
                        else
                        {
                            sCurrentDate = (cmonth + 1) + "-01-" + cyear;
                        }

                        dtCurrent = Convert.ToDateTime(sCurrentDate);

                        if (dsDoctor.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow dataRow in dsDoctor.Tables[0].Rows)
                            {
                                dsDoc = sf.Class_LST_Report(drFF["sf_code"].ToString(), div_code, dtCurrent, cmonth, cyear, Convert.ToInt32(dataRow["Doc_Cat_Code"].ToString()));

                                if (dsDoc.Tables[0].Rows.Count > 0)
                                    tot_dr = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

                                doctor_total = doctor_total + Convert.ToInt16(tot_dr);
                            }

                            TableCell tc_det_sf_tot = new TableCell();
                            Literal lit_det_sf_tot = new Literal();
                            lit_det_sf_tot.Text = "&nbsp;" + doctor_total.ToString();
                            tc_det_sf_tot.BorderStyle = BorderStyle.Solid;
                            //tc_det_sf_tot.BackColor = System.Drawing.Color.LavenderBlush;
                            tc_det_sf_tot.BorderWidth = 1;
                            tc_det_sf_tot.HorizontalAlign = HorizontalAlign.Center;
                            tc_det_sf_tot.VerticalAlign = VerticalAlign.Middle;
                            tc_det_sf_tot.Controls.Add(lit_det_sf_tot);
                            tr_det.Cells.Add(tc_det_sf_tot);

                            foreach (DataRow dataRow in dsDoctor.Tables[0].Rows)
                            {
                                dsDoc = sf.Class_LST_Report(drFF["sf_code"].ToString(), div_code, dtCurrent, cmonth, cyear, Convert.ToInt32(dataRow["Doc_Cat_Code"].ToString()));

                                if (dsDoc.Tables[0].Rows.Count > 0)
                                    tot_dr = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

                                TableCell tc_lst_month = new TableCell();
                                HyperLink hyp_lst_month = new HyperLink();
                                if (tot_dr != "0")
                                {

                                    tot_miss = tot_miss + Convert.ToInt16(tot_dr);
                                    hyp_lst_month.Text = tot_dr;
                                }
                                else
                                {
                                    hyp_lst_month.Text = "-";
                                }

                                tc_lst_month.BorderStyle = BorderStyle.Solid;
                                tc_lst_month.BorderWidth = 1;
                                //tc_lst_month.BackColor = System.Drawing.Color.Honeydew;
                                tc_lst_month.Width = 200;
                                tc_lst_month.HorizontalAlign = HorizontalAlign.Center;
                                tc_lst_month.VerticalAlign = VerticalAlign.Middle;
                                tc_lst_month.Controls.Add(hyp_lst_month);
                                tr_det.Cells.Add(tc_lst_month);
                            }
                        }

                        if (dsDoctor.Tables[0].Rows.Count > 0)
                        {
                            DCR dc = new DCR();
                            foreach (DataRow dataRow in dsDoctor.Tables[0].Rows)
                            {
                                dsDoc = sf.Class_LST_Report(drFF["sf_code"].ToString(), div_code, dtCurrent, cmonth, cyear, Convert.ToInt32(dataRow["Doc_Cat_Code"].ToString()));
                                if (dsDoc.Tables[0].Rows.Count > 0)
                                    tot_dr = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

                                dsDCR = dc.Class_Visit_Report(drFF["sf_code"].ToString(), div_code, cmonth, cyear, Convert.ToInt32(dataRow["Doc_Cat_Code"].ToString()));
                                if (dsDCR.Tables[0].Rows.Count > 0)
                                    tot_dcr_dr = dsDCR.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

                                TableCell tc_msd_month = new TableCell();
                                HyperLink hyp_msd_month = new HyperLink();
                                //if (tot_dr != "0")
                                if (tot_dcr_dr != "0")
                                {
                                    //imissed_dr = Convert.ToInt16(tot_dr) - Convert.ToInt16(tot_dcr_dr);
                                    imissed_dr = Convert.ToInt16(tot_dcr_dr);
                                    hyp_msd_month.Text = imissed_dr.ToString();
                                    if (imissed_dr > 0)
                                    {
                                        tot_met = tot_met + imissed_dr;
                                        hyp_msd_month.Attributes.Add("href", "javascript:showVisitDR_type('" + drFF["sf_code"].ToString() + "', '" + cmonth.ToString() + "', '" + cyear.ToString() + "', '" + sMode + "','" + Convert.ToInt32(dataRow["Doc_Cat_Code"].ToString()) + "')");
                                    }
                                    //    hyp_msd_month.Attributes.Add("href", "javascript:showModalPopUp('" + drFF["sf_code"].ToString() + "', '" + cmonth + "', '" + cyear + "')");
                                }
                                else
                                {
                                    hyp_msd_month.Text = "-";
                                }

                                tc_msd_month.BorderStyle = BorderStyle.Solid;
                                tc_msd_month.BorderWidth = 1;
                                //tc_msd_month.BackColor = System.Drawing.Color.PapayaWhip;
                                tc_msd_month.Width = 200;
                                tc_msd_month.HorizontalAlign = HorizontalAlign.Center;
                                tc_msd_month.VerticalAlign = VerticalAlign.Middle;
                                tc_msd_month.Controls.Add(hyp_msd_month);
                                tr_det.Cells.Add(tc_msd_month);
                            }
                        }

                        TableCell tc_det_sf_tt = new TableCell();
                        //Literal lit_det_sf_tt = new Literal();
                        HyperLink lit_det_sf_tt = new HyperLink();
                        if (tot_met > 0)
                        {
                            lit_det_sf_tt.Text = tot_met.ToString();
                            lit_det_sf_tt.Attributes.Add("href", "javascript:showVisitDR_type('" + drFF["sf_code"].ToString() + "', '" + cmonth.ToString() + "', '" + cyear.ToString() + "', '" + sMode + "','' )");
                        }
                        else
                        {
                            lit_det_sf_tt.Text = "-";
                        }

                        tc_det_sf_tt.BorderStyle = BorderStyle.Solid;
                        //tc_det_sf_tt.BackColor = System.Drawing.Color.LemonChiffon;
                        tc_det_sf_tt.BorderWidth = 1;
                        tc_det_sf_tt.HorizontalAlign = HorizontalAlign.Center;
                        tc_det_sf_tt.VerticalAlign = VerticalAlign.Middle;
                        tc_det_sf_tt.Controls.Add(lit_det_sf_tt);
                        tr_det.Cells.Add(tc_det_sf_tt);

                        //if(tot_met > 
                        tot_miss = tot_miss - tot_met;

                        TableCell tc_det_sf_tt_miss = new TableCell();
                        Literal lit_det_sf_tt_miss = new Literal();
                        lit_det_sf_tt_miss.Text = "&nbsp;" + tot_miss.ToString();
                        tc_det_sf_tt_miss.BorderStyle = BorderStyle.Solid;
                        //tc_det_sf_tt_miss.BackColor = System.Drawing.Color.LemonChiffon;
                        tc_det_sf_tt_miss.BorderWidth = 1;
                        tc_det_sf_tt_miss.HorizontalAlign = HorizontalAlign.Center;
                        tc_det_sf_tt_miss.VerticalAlign = VerticalAlign.Middle;
                        tc_det_sf_tt_miss.Controls.Add(lit_det_sf_tt_miss);
                        tr_det.Cells.Add(tc_det_sf_tt_miss);

                        cmonth = cmonth + 1;
                        if (cmonth == 13)
                        {
                            cmonth = 1;
                            cyear = cyear + 1;
                        }

                    }
                }

                tbl.Rows.Add(tr_det);

            }
        }
    }


}