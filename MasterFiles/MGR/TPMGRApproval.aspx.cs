using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;

public partial class MasterFiles_MGR_TPMGRApproval : System.Web.UI.Page
{

    string div_code = string.Empty;
    DataSet dsSalesForce = null;
    DataSet dsTP = null;
    string strTPView = string.Empty;
    string sf_Code = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        sf_Code = Session["sf_code"].ToString();
        hypLinkApproval.Visible = false;
        if (!Page.IsPostBack)
        {
            FillManager();
            menu1.FindControl("btnBack").Visible = false;
        }
    }

    private void FillManager()
    {

        TourPlan tp = new TourPlan();
        dsTP = tp.get_TP_Submission_Date(sf_Code);
        //lblNextMonthTitle.Text = "You Can't Enter the Tour Plan For the Month Of "+dsTP.Tables[0].Rows[0][0].ToString();
        dsTP = tp.get_TP_Active_Date(sf_Code);
        string str = getMonthName(dsTP.Tables[0].Rows[0][0].ToString().Substring(3, 2));
        lblNextMonthTitle.Text = "You Can't Enter the Tour Plan For the Month Of " + str.Substring(0, 3) + " - " + dsTP.Tables[0].Rows[0][0].ToString().Substring(6, 4);

        SalesForce sf = new SalesForce();
        dsSalesForce = sf.SalesForceList_TP_StartingDt_Get(div_code, sf_Code, dsTP.Tables[0].Rows[0][0].ToString().Substring(3, 2));
        if (dsSalesForce.Tables[0].Rows.Count > 0 || dsSalesForce.Tables[1].Rows.Count > 0)
        {
            //grdSalesForce.DataSource = dsSalesForce;
            //grdSalesForce.DataBind();

            TableRow tr_header = new TableRow();
            tr_header.BorderStyle = BorderStyle.Solid;
            tr_header.BorderWidth = 1;
            TableCell tc_SNo = new TableCell();
            tc_SNo.BorderStyle = BorderStyle.Solid;
            tc_SNo.BorderWidth = 1;
            tc_SNo.Width = 50;
            tc_SNo.Height = 25;
            tc_SNo.RowSpan = 1;
            Literal lit_SNo = new Literal();
            lit_SNo.Text = "S.No";
            tc_SNo.Controls.Add(lit_SNo);
            tc_SNo.BackColor = System.Drawing.ColorTranslator.FromHtml("#0097AC");
            tc_SNo.Style.Add("color", "White");
            tc_SNo.Style.Add("font-weight", "bold");
            tc_SNo.Style.Add("border-color", "Black");
            tc_SNo.Style.Add("Font-Size", "10pt");
            tc_SNo.HorizontalAlign = HorizontalAlign.Center;
            tc_SNo.Style.Add("font-family", "Calibri");
            //tc_SNo.Visible = false;
            tr_header.Cells.Add(tc_SNo);
            //tr_header.BackColor = System.Drawing.Color.FromName("#A6A6D2");

            TableCell tc_DR_Code = new TableCell();
            tc_DR_Code.BorderStyle = BorderStyle.Solid;
            tc_DR_Code.BorderWidth = 1;
            tc_DR_Code.Width = 40;
            tc_DR_Code.RowSpan = 1;
            Literal lit_DR_Code = new Literal();
            lit_DR_Code.Text = "<center>SF Code</center>";
            tc_DR_Code.Controls.Add(lit_DR_Code);
            tc_DR_Code.BackColor = System.Drawing.ColorTranslator.FromHtml("#0097AC");
            tc_DR_Code.Style.Add("color", "White");
            tc_DR_Code.Style.Add("font-weight", "bold");
            tc_DR_Code.Style.Add("font-family", "Calibri");
            tc_DR_Code.Style.Add("Font-Size", "10pt");
            tc_DR_Code.Style.Add("border-color", "Black");
            tc_DR_Code.Visible = false;
            tr_header.Cells.Add(tc_DR_Code);

            TableCell tc_DR_Name = new TableCell();
            tc_DR_Name.BorderStyle = BorderStyle.Solid;
            tc_DR_Name.BorderWidth = 1;
            tc_DR_Name.Width = 200;
            tc_DR_Name.RowSpan = 1;
            Literal lit_DR_Name = new Literal();
            lit_DR_Name.Text = "<center>Field Force</center>";
            tc_DR_Name.Controls.Add(lit_DR_Name);
            tc_DR_Name.BackColor = System.Drawing.ColorTranslator.FromHtml("#0097AC");
            tc_DR_Name.Style.Add("color", "White");
            tc_DR_Name.Style.Add("font-weight", "bold");
            tc_DR_Name.Style.Add("font-family", "Calibri");
            tc_DR_Name.Style.Add("Font-Size", "10pt");
            tc_DR_Name.Style.Add("border-color", "Black");
            tr_header.Cells.Add(tc_DR_Name);

            TableCell tc_DR_HQ = new TableCell();
            tc_DR_HQ.BorderStyle = BorderStyle.Solid;
            tc_DR_HQ.BorderWidth = 1;
            tc_DR_HQ.Width = 50;
            tc_DR_HQ.RowSpan = 1;
            Literal lit_DR_HQ = new Literal();
            lit_DR_HQ.Text = "<center>HQ</center>";
            tc_DR_HQ.Controls.Add(lit_DR_HQ);
            tc_DR_HQ.BackColor = System.Drawing.ColorTranslator.FromHtml("#0097AC");
            tc_DR_HQ.Style.Add("color", "White");
            tc_DR_HQ.Style.Add("font-weight", "bold");
            tc_DR_HQ.Style.Add("font-family", "Calibri");
            tc_DR_HQ.Style.Add("Font-Size", "10pt");
            tc_DR_HQ.Style.Add("border-color", "Black");
            tr_header.Cells.Add(tc_DR_HQ);

            TableCell tc_DR_Des = new TableCell();
            tc_DR_Des.BorderStyle = BorderStyle.Solid;
            tc_DR_Des.BorderWidth = 1;
            tc_DR_Des.Width = 50;
            tc_DR_Des.RowSpan = 1;
            Literal lit_DR_Des = new Literal();
            lit_DR_Des.Text = "<center>Designation</center>";
            tc_DR_Des.HorizontalAlign = HorizontalAlign.Center;
            tc_DR_Des.Controls.Add(lit_DR_Des);
            tc_DR_Des.BackColor = System.Drawing.ColorTranslator.FromHtml("#0097AC");
            tc_DR_Des.Style.Add("color", "White");
            tc_DR_Des.Style.Add("font-weight", "bold");
            tc_DR_Des.Style.Add("font-family", "Calibri");
            tc_DR_Des.Style.Add("Font-Size", "10pt");
            tc_DR_Des.Style.Add("border-color", "Black");
            tr_header.Cells.Add(tc_DR_Des);

            TableCell tc_DR_Approved = new TableCell();
            tc_DR_Approved.BorderStyle = BorderStyle.Solid;
            tc_DR_Approved.BorderWidth = 1;
            tc_DR_Approved.Width = 50;
            tc_DR_Approved.RowSpan = 1;
            Literal lit_DR_Approved = new Literal();
            lit_DR_Approved.Text = "<center>Approved by</center>";
            tc_DR_Approved.HorizontalAlign = HorizontalAlign.Center;
            tc_DR_Approved.Controls.Add(lit_DR_Approved);
            tc_DR_Approved.BackColor = System.Drawing.ColorTranslator.FromHtml("#0097AC");
            tc_DR_Approved.Style.Add("color", "White");
            tc_DR_Approved.Style.Add("font-weight", "bold");
            tc_DR_Approved.Style.Add("font-family", "Calibri");
            tc_DR_Approved.Style.Add("Font-Size", "10pt");
            tc_DR_Approved.Style.Add("border-color", "Black");
            tr_header.Cells.Add(tc_DR_Approved); 

            TableCell tc_DR_Status = new TableCell();
            tc_DR_Status.BorderStyle = BorderStyle.Solid;
            tc_DR_Status.BorderWidth = 1;
            tc_DR_Status.Width = 50;
            tc_DR_Status.RowSpan = 1;
            Literal lit_DR_Status = new Literal();
            lit_DR_Status.Text = "<center>TP Status</center>";
            tc_DR_Status.HorizontalAlign = HorizontalAlign.Center;
            tc_DR_Status.Controls.Add(lit_DR_Status);
            tc_DR_Status.BackColor = System.Drawing.ColorTranslator.FromHtml("#0097AC");
            tc_DR_Status.Style.Add("color", "White");
            tc_DR_Status.Style.Add("font-weight", "bold");
            tc_DR_Status.Style.Add("font-family", "Calibri");
            tc_DR_Status.Style.Add("Font-Size", "10pt");
            tc_DR_Status.Style.Add("border-color", "Black");
            tr_header.Cells.Add(tc_DR_Status);

            

            tbl.Rows.Add(tr_header);

            int iCount = 1;
            int iCnt = 0;

            foreach (DataRow drFF in dsSalesForce.Tables[0].Rows)
            {
                DataSet dsStatus = new DataSet();
                TourPlan TP = new TourPlan();
                dsStatus = TP.get_TP_ApprovalStatus(drFF["sf_code"].ToString(), dsTP.Tables[0].Rows[0][0].ToString().Substring(3, 2), dsTP.Tables[0].Rows[0][0].ToString().Substring(6, 4));

                ListedDR lstDR = new ListedDR();
                iCnt = lstDR.RecordCount(drFF["sf_code"].ToString());
                string sf_code = dsSalesForce.Tables[0].Rows[0]["sf_TP_Active_Dt"].ToString();

                TableRow tr_det = new TableRow();
                //tr_det.Attributes.Add("style", "background-color:" + "#" + drFF["des_color"].ToString());
                tr_det.BackColor = System.Drawing.Color.White;
                
                TableCell tc_det_SNo = new TableCell();
                Literal lit_det_SNo = new Literal();
                lit_det_SNo.Text = "<center>" + iCount.ToString() + "</center>";
                tc_det_SNo.BorderStyle = BorderStyle.Solid;
                tc_det_SNo.BorderWidth = 1;
                tc_det_SNo.Width = 50;
                tc_det_SNo.Style.Add("Font-Size", "10pt");
                //tc_det_SNo.Visible = false;
                tc_det_SNo.Controls.Add(lit_det_SNo);
                tr_det.Cells.Add(tc_det_SNo);               

                TableCell tc_det_doc_code = new TableCell();
                Literal lit_det_doc_code = new Literal();
                lit_det_doc_code.Text = "&nbsp;" + drFF["sf_code"].ToString();
                tc_det_doc_code.BorderStyle = BorderStyle.Solid;
                tc_det_doc_code.BorderWidth = 1;
                tc_det_doc_code.Controls.Add(lit_det_doc_code);
                tc_det_doc_code.Style.Add("Font-Size", "10pt");
                tc_det_doc_code.Visible = false;
                tr_det.Cells.Add(tc_det_doc_code);

                TableCell tc_det_doc_name = new TableCell();
                HyperLink lit_det_doc_name = new HyperLink();
                lit_det_doc_name.Text = "&nbsp;" + drFF["SF_Name"].ToString();
                tc_det_doc_name.HorizontalAlign = HorizontalAlign.Left;
                tc_det_doc_name.BorderStyle = BorderStyle.Solid;
                tc_det_doc_name.Style.Add("font-family", "Calibri");
                tc_det_doc_name.BorderWidth = 1;
                tc_det_doc_name.Width = 200;
                tc_det_doc_name.Controls.Add(lit_det_doc_name);
                tc_det_doc_name.Style.Add("Font-Size", "10pt");
                tr_det.Cells.Add(tc_det_doc_name);

                TableCell tc_det_sf_HQ = new TableCell();
                Literal lit_det_sf_hq = new Literal();
                lit_det_sf_hq.Text = "&nbsp;" + drFF["Sf_HQ"].ToString();
                tc_det_sf_HQ.Style.Add("font-family", "Calibri");
                tc_det_sf_HQ.Width = 50;
                tc_det_sf_HQ.BorderStyle = BorderStyle.Solid;
                tc_det_sf_HQ.BorderWidth = 1;
                tc_det_sf_HQ.HorizontalAlign = HorizontalAlign.Left;
                tc_det_sf_HQ.Controls.Add(lit_det_sf_hq);
                tc_det_sf_HQ.Style.Add("Font-Size", "10pt");
                //tc_det_sf_HQ.Visible = false;
                tr_det.Cells.Add(tc_det_sf_HQ);

                TableCell tc_det_sf_des = new TableCell();
                Literal lit_det_sf_des = new Literal();
                lit_det_sf_des.Text = "&nbsp;" + drFF["Designation_Short_Name"].ToString();
                tc_det_sf_des.BorderStyle = BorderStyle.Solid;
                tc_det_sf_des.BorderWidth = 1;
                tc_det_sf_des.Style.Add("font-family", "Calibri");
                tc_det_sf_des.Width = 50;
                tc_det_sf_des.HorizontalAlign = HorizontalAlign.Center;
                tc_det_sf_des.Controls.Add(lit_det_sf_des);
                tc_det_sf_des.Style.Add("Font-Size", "10pt");
                //tc_det_sf_HQ.Visible = false;
                tr_det.Cells.Add(tc_det_sf_des);

                TableCell tc_det_sf_Approved = new TableCell();
                Literal lit_det_sf_Approved = new Literal();
                lit_det_sf_Approved.Text = "&nbsp;" + drFF["Reporting_To_SF"].ToString();
                tc_det_sf_Approved.BorderStyle = BorderStyle.Solid;
                tc_det_sf_Approved.BorderWidth = 1;
                tc_det_sf_Approved.Style.Add("font-family", "Calibri");
                tc_det_sf_Approved.Width = 50;
                tc_det_sf_Approved.HorizontalAlign = HorizontalAlign.Center;
                tc_det_sf_Approved.Controls.Add(lit_det_sf_Approved);
                tc_det_sf_Approved.Style.Add("Font-Size", "10pt");
                //tc_det_sf_HQ.Visible = false;
                tr_det.Cells.Add(tc_det_sf_Approved);
              
                    TableCell tc_det_sf_doj = new TableCell();
                    Literal lit_det_sf_doj = new Literal();
                    if (dsStatus.Tables[0].Rows.Count > 0)
                    {
                        lit_det_sf_doj.Text = "&nbsp;" + dsStatus.Tables[0].Rows[0][1].ToString();

                        tc_det_sf_doj.BorderStyle = BorderStyle.Solid;
                        tc_det_sf_doj.BorderWidth = 1;
                        tc_det_sf_doj.Style.Add("font-family", "Calibri");
                        tc_det_sf_doj.HorizontalAlign = HorizontalAlign.Left;
                        tc_det_sf_doj.Controls.Add(lit_det_sf_doj);
                        tc_det_sf_doj.Style.Add("Font-Size", "10pt");
                        tc_det_sf_doj.Width = 100;
                        tr_det.Cells.Add(tc_det_sf_doj);


                        iCount += 1;
                        tbl.Rows.Add(tr_det);
                        if (dsStatus.Tables[0].Rows[0][1].ToString() == "Completed")
                        {
                            tr_det.Cells.Clear();
                        }
                        
                    }
                    else
                    {
                        iCount += 1;
                        
                        lit_det_sf_doj.Text = "&nbsp;" +"<span style='color:Red'>"+"Not Prepared"+"</span>";

                        tc_det_sf_doj.BorderStyle = BorderStyle.Solid;
                        tc_det_sf_doj.BorderWidth = 1;
                        tc_det_sf_doj.Style.Add("font-family", "Calibri");
                        tc_det_sf_doj.HorizontalAlign = HorizontalAlign.Left;
                        tc_det_sf_doj.Controls.Add(lit_det_sf_doj);
                        tc_det_sf_doj.Style.Add("Font-Size", "10pt");
                        tc_det_sf_doj.Width = 100;
                        tr_det.Cells.Add(tc_det_sf_doj); 
                        tbl.Rows.Add(tr_det);
                    }

                    if (lit_det_sf_doj.Text == "&nbsp;" + "Prepared & Not Completed")
                    {
                        hypLinkApproval.Visible = true;
                    }
            }
            
        }
    }

    public string getMonthName(string sMonth)
    {
        string sReturn = string.Empty;

        if (sMonth == "01")
        {
            sReturn = "January";
        }
        else if (sMonth == "02")
        {
            sReturn = "February";
        }
       
        else if (sMonth == "03")
        {
            sReturn = "March";
        }
        else if (sMonth == "04")
        {
            sReturn = "April";
        }
        else if (sMonth == "05")
        {
            sReturn = "May";
        }
        else if (sMonth == "06")
        {
            sReturn = "June";
        }
        else if (sMonth == "07")
        {
            sReturn = "July";
        }
        else if (sMonth == "08")
        {
            sReturn = "August";
        }
        else if (sMonth == "09")
        {
            sReturn = "September";
        }
        else if (sMonth == "10")
        {
            sReturn = "October";
        }
        else if (sMonth == "11")
        {
            sReturn = "November";
        }
        else if (sMonth == "12")
        {
            sReturn = "December";
        }

        return sReturn;
    }
}