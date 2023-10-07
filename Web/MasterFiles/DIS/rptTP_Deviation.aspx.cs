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

public partial class MIS_Reports_rptTP_Deviation : System.Web.UI.Page
{

    string sfCode = string.Empty;
    string sfname = string.Empty;
    string divcode = string.Empty;
    int iMonth = -1;
    int iYear = -1;
    DataSet dsDoctor = null;
    string sMode = string.Empty;
    string FMonth = string.Empty;
    string FYear = string.Empty;
    string TMonth = string.Empty;
    string TYear = string.Empty;
    string Prod = string.Empty;
    string Prod_Name = string.Empty;
    string sReturn = string.Empty;
    string sDay = string.Empty;
    string sCnt = string.Empty;
    string vMode = string.Empty;
    DataSet dsSalesForce = new DataSet();
    DataSet dsSales = new DataSet();
    DataSet dsmgrsf = new DataSet();
    DataSet dsDoc = null;
    DataSet dsLeave = null;
    string Monthsub = string.Empty;
    string tot_dr = string.Empty;
    string Days = string.Empty;
    string strSf_Code = string.Empty;
    string sCurrentDate = string.Empty;
    string strDayName = string.Empty;
    int count = 0;
    string type = string.Empty;
    int iIndex;


    protected void Page_Load(object sender, EventArgs e)
    {
        divcode = Session["div_code"].ToString();
        sfCode = Request.QueryString["sfcode"].ToString();
        FMonth = Request.QueryString["FMonth"].ToString();
        FYear = Request.QueryString["FYear"].ToString();
        sfname = Request.QueryString["sf_name"].ToString();
        lblRegionName.Text = sfname;

        System.Globalization.DateTimeFormatInfo mfi = new System.Globalization.DateTimeFormatInfo();
        string strFMonthName = mfi.GetMonthName(Convert.ToInt16(FMonth)).ToString().Substring(0, 3);

        lblHead.Text = "TP - Deviation for the Month of " + strFMonthName + " " + FYear;

        //lblIDMonth.Visible = false;
        //lblIDYear.Visible = false;


        type = sfCode;

        if (type.Contains("MR"))
        {
            type = "MR";
        }
        else if (type.Contains("MGR"))
        {
            type = "MGR";
        }

        FillDeviation();
    }

    private void FillDeviation()
    {

        SalesForce sal = new SalesForce();
        dsSalesForce = sal.getTP_Deviation(divcode, sfCode, Convert.ToInt16(FMonth), Convert.ToInt16(FYear), type);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            grdTP.Visible = true;
            grdTP.DataSource = dsSalesForce;
            grdTP.DataBind();
        }
        else
        {
            grdTP.DataSource = dsSalesForce;
            grdTP.DataBind();
        }


    }

    protected void btnExcel_Click(object sender, EventArgs e)
    {
        string strFileName = Page.Title;
        string attachment = "attachment; filename='" + strFileName + "'.xls";
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
    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        Session["ctrl"] = pnlContents;
        Control ctrl = (Control)Session["ctrl"];
        PrintWebControl(ctrl);
    }

    public static void PrintWebControl(Control ControlToPrint)
    {
        StringWriter stringWrite = new StringWriter();
        System.Web.UI.HtmlTextWriter htmlWrite = new System.Web.UI.HtmlTextWriter(stringWrite);
        if (ControlToPrint is WebControl)
        {
            Unit w = new Unit(100, UnitType.Percentage);
            ((WebControl)ControlToPrint).Width = w;
        }
        Page pg = new Page();
        pg.EnableEventValidation = false;
        HtmlForm frm = new HtmlForm();
        pg.Controls.Add(frm);
        frm.Attributes.Add("runat", "server");
        frm.Controls.Add(ControlToPrint);
        pg.DesignerInitialize();
        pg.RenderControl(htmlWrite);
        string strHTML = stringWrite.ToString();
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.Write(strHTML);
        HttpContext.Current.Response.Write("<script>window.print();</script>");
        HttpContext.Current.Response.End();

    }

    protected void btnClose_Click(object sender, EventArgs e)
    {

    }

    protected void grdTP_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (type == "MR")
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblterritory_Code = (Label)e.Row.FindControl("lblterritory_Code");
                Label lblASper_Dcr = (Label)e.Row.FindControl("lblASper_Dcr");
                Label lbldate = (Label)e.Row.FindControl("lbldate");
                Label lblDate_Name = (Label)e.Row.FindControl("lblDate_Name");
                Label lblAsper_Tp = (Label)e.Row.FindControl("lblAsper_Tp");
                Label lblASper_Dcr_match = (Label)e.Row.FindControl("lblASper_Dcr_match");
                Label lblSNo = (Label)e.Row.FindControl("lblSNo");
                Label lbltrans_sl = (Label)e.Row.FindControl("lbltrans_sl");
                Label lblworkDCR = (Label)e.Row.FindControl("lblworkDCR");
                string workDCR = lblworkDCR.Text;
                string workDCR1 = workDCR.ToUpper();
                Label lblworkTP = (Label)e.Row.FindControl("lblworkTP");
                string workTP = lblworkTP.Text;
                string workTP1 = workTP.ToUpper();

                DateTime date = Convert.ToDateTime(lbldate.Text);

                lbldate.Text = date.ToString("dd/MM/yyyy");
                //lbldate.Text = date.Day.ToString();
                string DCR_Terr = string.Empty;

                if ((lblworkDCR.Text == "Field Work") && (lblworkTP.Text == "Field Work"))
                {
                    SalesForce sal = new SalesForce();
                    dsSales = sal.getTP_Deviation_Transl_terr(lbltrans_sl.Text);

                    foreach (DataRow drFF in dsSales.Tables[0].Rows)
                    {
                        DCR_Terr += drFF[1].ToString() + ',';
                    }

                    //string fullName = DCR_worked_with;
                    //var names = fullName.Split(' ');
                    bool same = false;
                    string stringToCheck = lblAsper_Tp.Text;
                    string[] name;
                    name = stringToCheck.Split(',');
                    string[] stringArray = { DCR_Terr };

                    foreach (string check in name)
                    {
                        foreach (string x in stringArray)
                        {
                            if (x.Contains(check))
                            {

                                e.Row.Cells[0].Visible = false;
                                e.Row.Cells[3].Visible = false;
                                e.Row.Cells[4].Visible = false;
                                e.Row.Cells[5].Visible = false;
                                e.Row.Cells[6].Visible = false;
                                same = true;
                                //break;  
                            }
                         
                            else
                            {
                                same = false;
                                count += 1;
                                lblSNo.Text = Convert.ToString(count);
                                e.Row.Cells[0].Visible = true;
                                e.Row.Cells[3].Visible = true;
                                e.Row.Cells[4].Visible = true;
                                e.Row.Cells[5].Visible = true;
                                e.Row.Cells[6].Visible = true;
                                lblAsper_Tp.Text = stringToCheck + "&nbsp;&nbsp;&nbsp;(Field Work)";
                                lblASper_Dcr_match.Text = DCR_Terr + "&nbsp;&nbsp;&nbsp;(Field Work)";

                            }

                        }

                        if (same == true)
                        {
                            break;
                        }
                    }

                }
                else
                {
                    if (workDCR1 != workTP1)
                    {
                        if (workDCR1 == "FIELD WORK")
                        {
                            lblASper_Dcr_match.Text = lblASper_Dcr_match.Text + "&nbsp;&nbsp;&nbsp;(Field Work)";
                        }
                        else if (workTP1 == "FIELD WORK")
                        {
                            lblAsper_Tp.Text = lblAsper_Tp.Text + "&nbsp;&nbsp;&nbsp;(Field Work)";
                        }
                        count += 1;
                        lblSNo.Text = Convert.ToString(count);
                        e.Row.Cells[0].Visible = true;
                        e.Row.Cells[3].Visible = true;
                        e.Row.Cells[4].Visible = true;
                        e.Row.Cells[5].Visible = true;
                        e.Row.Cells[6].Visible = true;
                    }
                    else
                    {
                        e.Row.Cells[0].Visible = false;
                        e.Row.Cells[3].Visible = false;
                        e.Row.Cells[4].Visible = false;
                        e.Row.Cells[5].Visible = false;
                        e.Row.Cells[6].Visible = false;
                    }
                }
            }
        }
        else if (type == "MGR")
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblterritory_Code = (Label)e.Row.FindControl("lblterritory_Code");
                Label lblASper_Dcr = (Label)e.Row.FindControl("lblASper_Dcr");
                Label lbldate = (Label)e.Row.FindControl("lbldate");
                Label lblDate_Name = (Label)e.Row.FindControl("lblDate_Name");
                Label lblAsper_Tp = (Label)e.Row.FindControl("lblAsper_Tp");
                Label lblASper_Dcr_match = (Label)e.Row.FindControl("lblASper_Dcr_match");
                Label lblSNo = (Label)e.Row.FindControl("lblSNo");
                Label lblworkDCR = (Label)e.Row.FindControl("lblworkDCR");
                string workDCR = lblworkDCR.Text;
                string workDCR1 = workDCR.ToUpper();
                Label lblworkTP = (Label)e.Row.FindControl("lblworkTP");
                string workTP = lblworkTP.Text;
                string workTP1 = workTP.ToUpper();

                Label lbltrans_sl = (Label)e.Row.FindControl("lbltrans_sl");

                DateTime date = Convert.ToDateTime(lbldate.Text);

                lbldate.Text = date.ToString("dd/MM/yyyy");
                //string str = lblASper_Dcr_match.Text;
                string DCR_worked_with = string.Empty;
                
                if ((lblworkDCR.Text == "Field Work") && (lblworkTP.Text == "Field Work"))
                {
                    SalesForce sal = new SalesForce();
                    //dsSales = sal.getTP_Deviation_Transl(lbltrans_sl.Text);

                    foreach (DataRow drFF in dsSales.Tables[0].Rows)
                    {
                        DCR_worked_with += drFF[0].ToString();
                    }

                    //string fullName = DCR_worked_with;
                    //var names = fullName.Split(' ');
                    bool same = false;
                    string stringToCheck = lblAsper_Tp.Text;
                    string[] name;
                    name =stringToCheck.Split(',');
                    string[] stringArray ={ DCR_worked_with};

                    foreach (string check in name)
                    {
                        foreach (string x in stringArray)
                        {
                            if (x.Contains(check))
                            {

                                e.Row.Cells[0].Visible = false;
                                e.Row.Cells[3].Visible = false;
                                e.Row.Cells[4].Visible = false;
                                e.Row.Cells[5].Visible = false;
                                e.Row.Cells[6].Visible = false;
                                same =true;
                                //break;  
                            }
                            else if (x == "SELF," && check == "Independent")
                            {
                                e.Row.Cells[0].Visible = false;
                                e.Row.Cells[3].Visible = false;
                                e.Row.Cells[4].Visible = false;
                                e.Row.Cells[5].Visible = false;
                                e.Row.Cells[6].Visible = false;
                                same = true;
                            }
                            else if (x == "SELF, " && check == "Independent")
                            {
                                e.Row.Cells[0].Visible = false;
                                e.Row.Cells[3].Visible = false;
                                e.Row.Cells[4].Visible = false;
                                e.Row.Cells[5].Visible = false;
                                e.Row.Cells[6].Visible = false;
                                same = true;
                            }
                            else
                            {
                                same = false;
                                count += 1;
                                lblSNo.Text = Convert.ToString(count);
                                e.Row.Cells[0].Visible = true;
                                e.Row.Cells[3].Visible = true;
                                e.Row.Cells[4].Visible = true;
                                e.Row.Cells[5].Visible = true;
                                e.Row.Cells[6].Visible = true;
                                lblAsper_Tp.Text = stringToCheck + "&nbsp;&nbsp;&nbsp;(Field Work)";
                                lblASper_Dcr_match.Text = DCR_worked_with + "&nbsp;&nbsp;&nbsp;(Field Work)";

                            }
                           
                        }

                        if (same == true)
                        {
                            break;
                        }
                    }                 

                }
                else
                {
                    if (workDCR1 != workTP1)
                    {
                        if (workDCR1 == "FIELD WORK")
                        {
                            lblASper_Dcr_match.Text = lblASper_Dcr_match.Text + "&nbsp;&nbsp;&nbsp;(Field Work)";
                        }
                        else if (workTP1 == "FIELD WORK")
                        {
                            lblAsper_Tp.Text = lblAsper_Tp.Text + "&nbsp;&nbsp;&nbsp;(Field Work)";
                        }
                        count += 1;
                        lblSNo.Text = Convert.ToString(count);
                        e.Row.Cells[0].Visible = true;
                        e.Row.Cells[3].Visible = true;
                        e.Row.Cells[4].Visible = true;
                        e.Row.Cells[5].Visible = true;
                        e.Row.Cells[6].Visible = true;
                    }
                    else
                    {
                        e.Row.Cells[0].Visible = false;
                        e.Row.Cells[3].Visible = false;
                        e.Row.Cells[4].Visible = false;
                        e.Row.Cells[5].Visible = false;
                        e.Row.Cells[6].Visible = false;
                    }
                }
            }
        }
    }
}
