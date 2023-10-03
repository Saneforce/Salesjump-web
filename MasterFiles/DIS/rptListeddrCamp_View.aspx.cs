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
using iTextSharp.text.xml.simpleparser;
using iTextSharp.text.xml;
using iTextSharp.text.io;
using Bus_EReport;
using System.Net;

public partial class MIS_Reports_rptListeddrCamp_View : System.Web.UI.Page
{
    DataSet dsDoctor = null;
    DataSet dsListedDR = null;
    DataSet dsworkwith = new DataSet();
    string sMode = string.Empty;
    string sf_code = string.Empty;
    string FMonth = string.Empty;
    string FYear = string.Empty;
    string TMonth = string.Empty;
    string TYear = string.Empty;
    string div_code = string.Empty;
    string sReturn = string.Empty;
    string sDay = string.Empty;
    string sCnt = string.Empty;
    string vMode = string.Empty;
    string strCampaign = string.Empty;
    string strCamp_code = string.Empty;
    string strFrmMonth = string.Empty;
    string strToMonth = string.Empty;
    string strdate = string.Empty;
    string strwork = string.Empty;
    DataSet dsSalesForce = null;
    DataSet dsDoc = null;
    DataTable dtRP = null;
    string strFieledForceName = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        //sMode = Request.QueryString["cMode"].ToString();
        sf_code = Request.QueryString["sf_code"].ToString();
        FMonth = Request.QueryString["Frm_Month"].ToString();
        FYear = Request.QueryString["Frm_year"].ToString();
        TMonth = Request.QueryString["To_Month"].ToString();
        TYear = Request.QueryString["To_year"].ToString();
        strCampaign = Request.QueryString["campaign"].ToString();
        strCamp_code = Request.QueryString["camp_code"].ToString();
        strFieledForceName = Request.QueryString["sf_name"].ToString();
        if (Session["sf_type"].ToString() == "1")
        {
            div_code = Session["div_code"].ToString();
            sf_code = Session["sf_code"].ToString();
        }

        //sType = Request.QueryString["Type"].ToString();
        SalesForce sf = new SalesForce();
        strFrmMonth = sf.getMonthName(FMonth);
         strToMonth = sf.getMonthName(TMonth);

       // lblHead.Text = "Campaign Drs - View For " + "<span style='color:Red'>" + strFieledForceName + "</span>" + "  - For The Month Of  : " + "<span style='color:Blue'>" + strFrmMonth + " " + FYear + "</span>" + " To " + "<span style='color:Blue'>" + strToMonth + " " + " " + TYear + "</span>";
      //  LblForceName.Text = "Campaign : " + strCampaign;
       // FillSF();
       CreateDynamicTable();
        ExportButton();
    }
    private void CreateDynamicTable()
    {
        int iCount = 0;        

        SalesForce sf = new SalesForce();
        if (sf_code.Contains("MR"))
        {
            dsSalesForce = sf.getSfName(sf_code);
        }
        else
        {
            dsSalesForce = sf.SalesForceList_New(div_code,sf_code);
         
        }
        
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            foreach (DataRow drFF in dsSalesForce.Tables[0].Rows)
            {
                iCount = 0;

                Table tbldetail_main3 = new Table();
                tbldetail_main3.HorizontalAlign = HorizontalAlign.Center;
                tbldetail_main3.BorderStyle = BorderStyle.None;
                tbldetail_main3.Width = 1000;
                TableRow tr_det_head_main3 = new TableRow();
                TableCell tc_det_head_main3 = new TableCell();
                tc_det_head_main3.Style.Add("font-family", "Calibri");
                tc_det_head_main3.Style.Add("font-size", "10pt");
                tc_det_head_main3.Width = 100;
                Literal lit_det_main3 = new Literal();
                lit_det_main3.Text = "Campaign Drs - View For " + "<span style='color:Red'>" + drFF["sf_name"].ToString() + "</span>" + "  - For The Month Of  : " + "<span style='color:Blue'>" + strFrmMonth + " " + FYear + "</span>" + " To " + "<span style='color:Blue'>" + strToMonth + " " + " " + TYear + "</span>";
                tc_det_head_main3.Controls.Add(lit_det_main3);
                tr_det_head_main3.Cells.Add(tc_det_head_main3);
                tr_det_head_main3.HorizontalAlign = HorizontalAlign.Center;
                tbldetail_main3.Rows.Add(tr_det_head_main3);
                form1.Controls.Add(tbldetail_main3);

                Table tbl_head_empty = new Table();
                tbl_head_empty.HorizontalAlign = HorizontalAlign.Left;
                TableRow tr_head_empty = new TableRow();
                TableCell tc_head_empty = new TableCell();
                tc_head_empty.Style.Add("font-family", "Calibri");
                tc_head_empty.Style.Add("font-size", "10pt");
                Literal lit_head_empty = new Literal();
                lit_head_empty.Text = "Campaign : " + strCampaign;
                tc_head_empty.Controls.Add(lit_head_empty);
                tr_head_empty.Cells.Add(tc_head_empty);
                tbl_head_empty.Rows.Add(tr_head_empty);
                form1.Controls.Add(tbl_head_empty);

                Table tbl_head_empty1 = new Table();
                tbl_head_empty.HorizontalAlign = HorizontalAlign.Center;
                TableRow tr_head_empty1 = new TableRow();
                TableCell tc_head_empty1 = new TableCell();
                tc_head_empty1.Style.Add("font-family", "Calibri");
                tc_head_empty1.Style.Add("font-size", "10pt");
                Literal lit_head_empty1 = new Literal();
                lit_head_empty1.Text = "<BR>";
                tc_head_empty1.Controls.Add(lit_head_empty1);
                tr_head_empty1.Cells.Add(tc_head_empty1);
                tbl_head_empty1.Rows.Add(tr_head_empty1);
                form1.Controls.Add(tbl_head_empty);

                Table tbl = new Table();
                tbl.HorizontalAlign = HorizontalAlign.Center;
                tbl.BorderStyle = BorderStyle.Solid;
                tbl.BorderWidth = 1;
                tbl.GridLines = GridLines.Both;
                tbl.Width = 1500;

                TableRow tr_header = new TableRow();
                tr_header.BorderStyle = BorderStyle.Solid;
                tr_header.BorderWidth = 1;
             //   tr_header.Attributes.Add("Class", "mGrid");

                TableCell tc_SNo = new TableCell();
                tc_SNo.BorderStyle = BorderStyle.Solid;
                tc_SNo.BorderColor = System.Drawing.Color.Black;
                tc_SNo.BorderWidth = 1;
                tc_SNo.RowSpan = 3;
                tc_SNo.Style.Add("font-family", "Calibri");
                tc_SNo.Style.Add("font-size", "10pt");
                Literal lit_SNo = new Literal();
                tc_SNo.Attributes.Add("Class", "tr_det_head");
                lit_SNo.Text = "<center><b>S.No</b></center>";
                tc_SNo.Controls.Add(lit_SNo);
                tr_header.Cells.Add(tc_SNo);

                //TableCell tc_sf = new TableCell();
                //tc_sf.BorderStyle = BorderStyle.Solid;
                //tc_sf.BorderWidth = 1;
                ////tc_FF.Width = 400;
                //tc_sf.BorderColor = System.Drawing.Color.Black;
                //tc_sf.Style.Add("font-family", "Calibri");
                //tc_sf.Style.Add("font-size", "10pt");

                //Literal lit_sf = new Literal();
                //lit_sf.Text = "<center><b>Field Force Name</b></center>";
                //tc_sf.Controls.Add(lit_sf);
                //tr_header.Cells.Add(tc_sf);

                Table tbldetail = new Table();
                tbldetail.BorderStyle = BorderStyle.Solid;
                tbldetail.BorderWidth = 1;
                tbldetail.GridLines = GridLines.Both;
                tbldetail.Width = 1000;
                tbldetail.Style.Add("border-collapse", "collapse");
                tbldetail.Style.Add("border", "solid 1px Black");
                TableCell tc_doc = new TableCell();
                tc_doc.BorderStyle = BorderStyle.Solid;
                tc_doc.BorderWidth = 1;

                //tc_FF.Width = 400;
                tc_doc.BorderColor = System.Drawing.Color.Black;
                tc_doc.Style.Add("font-family", "Calibri");
                tc_doc.Style.Add("font-size", "10pt");
                tc_doc.RowSpan = 3;
                tc_doc.Attributes.Add("Class", "tr_det_head");
                Literal lit_doc = new Literal();
                lit_doc.Text = "<center><b>Listed DR Name</b></center>";
                tc_doc.Controls.Add(lit_doc);
                tr_header.Cells.Add(tc_doc);



                TableCell tc_spec = new TableCell();
                tc_spec.BorderStyle = BorderStyle.Solid;
                tc_spec.BorderColor = System.Drawing.Color.Black;
                tc_spec.BorderWidth = 1;
                tc_spec.Style.Add("font-family", "Calibri");
                tc_spec.Style.Add("font-size", "10pt");
                tc_spec.RowSpan =3;
                Literal lit_spec = new Literal();
                tc_spec.Attributes.Add("Class", "tr_det_head");
                lit_spec.Text = "<center><b>Specialty</b></center>";
                tc_spec.Controls.Add(lit_spec);
                tr_header.Cells.Add(tc_spec);

                TableCell tc_catg = new TableCell();
                tc_catg.BorderStyle = BorderStyle.Solid;
                tc_catg.BorderColor = System.Drawing.Color.Black;
                tc_catg.BorderWidth = 1;
                tc_catg.Style.Add("font-family", "Calibri");
                tc_catg.Style.Add("font-size", "10pt");
                tc_catg.RowSpan = 3;
                Literal lit_catg = new Literal();
                tc_catg.Attributes.Add("Class", "tr_det_head");
                lit_catg.Text = "<center><b>Category</b></center>";
                tc_catg.Controls.Add(lit_catg);
                tr_header.Cells.Add(tc_catg);

                TableCell tc_qual = new TableCell();
                tc_qual.BorderStyle = BorderStyle.Solid;
                tc_qual.BorderColor = System.Drawing.Color.Black;
                tc_qual.BorderWidth = 1;
                tc_qual.Style.Add("font-family", "Calibri");
                tc_qual.Style.Add("font-size", "10pt");
                tc_qual.RowSpan = 3;
                Literal lit_qual = new Literal();
                tc_qual.Attributes.Add("Class", "tr_det_head");
                lit_qual.Text = "<center><b>Qual</b></center>";
                tc_qual.Controls.Add(lit_qual);
                tr_header.Cells.Add(tc_qual);

                TableCell tc_class = new TableCell();
                tc_class.BorderStyle = BorderStyle.Solid;
                tc_class.BorderColor = System.Drawing.Color.Black;
                tc_class.BorderWidth = 1;
                tc_class.Style.Add("font-family", "Calibri");
                tc_class.Style.Add("font-size", "10pt");
                tc_class.RowSpan = 3;
                Literal lit_class = new Literal();
                tc_class.Attributes.Add("Class", "tr_det_head");
                lit_class.Text = "<center><b>Class</b></center>";
                tc_class.Controls.Add(lit_class);
                tr_header.Cells.Add(tc_class);


                //tc_lst_month.BackColor = System.Drawing.Color.LavenderBlush;
                //tc_lst_month.ColumnSpan = dsDoctor.Tables[0].Rows.Count;
                //tc_lst_month.Controls.Add(lit_lst_month);
                //tr_lst_det.Cells.Add(tc_lst_month);

                tbl.Rows.Add(tr_header);


                int months = (Convert.ToInt32(TYear) - Convert.ToInt32(FYear)) * 12 + Convert.ToInt32(TMonth) - Convert.ToInt32(FMonth); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
              //  int months1 = (Convert.ToInt32(TYear) - Convert.ToInt32(FYear)) * 12 + Convert.ToInt32(TMonth) - Convert.ToInt32(FMonth); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
                int cmonth = Convert.ToInt32(FMonth);
                int cyear = Convert.ToInt32(FYear);
              
                ViewState["months"] = months;               
                ViewState["cmonth"] = cmonth;
                //ViewState["cmonthact"] = cmonthact;
                //ViewState["cyearact"] = cyearact;
                ViewState["cyear"] = cyear;

                Doctor dr = new Doctor();
                DataSet dsmgrsf = new DataSet();
               // DataTable dsDoctor = new DataTable();
                SalesForce sf1 = new SalesForce();
                if (sf_code.Contains("MR"))
                {
                    DataTable dt = sf1.getMRJointWork_camp(div_code, sf_code, 0);
                 
                    dsmgrsf.Tables.Add(dt);
                    dsDoctor = dsmgrsf;
                    
                }
                else
                {
                    DataTable dt = sf1.getMRJointWork_camp(div_code, drFF["sf_code"].ToString(), 0);
                    dsmgrsf.Tables.Add(dt);
                    dsDoctor = dsmgrsf;
                }
             
                    TableCell tc_Visit = new TableCell();
                    tc_Visit.BorderStyle = BorderStyle.Solid;
                    tc_Visit.ColumnSpan = dsDoctor.Tables[0].Rows.Count * (months +1);
                    tc_Visit.BorderColor = System.Drawing.Color.Black;
                    tc_Visit.BorderWidth = 1;
                    tc_Visit.Style.Add("font-family", "Calibri");
                    tc_Visit.Style.Add("font-size", "10pt");
                    // tc_Visit.ColumnSpan = 3;
                    //  tc_Visit.RowSpan = 1;
                    Literal lit_Visit = new Literal();
                    tc_Visit.Attributes.Add("Class", "tr_det_head");
                    lit_Visit.Text = "<center><b>Visit Date</b></center>";
                    tc_Visit.Controls.Add(lit_Visit);
                    tr_header.Cells.Add(tc_Visit);
                    tbl.Rows.Add(tr_header);

                    //for (int j = 1; j <= months + 1; j++)
                    //{
                    TableRow tr_catg1 = new TableRow();



                    //    cmonth = cmonth + 1;
                    //    if (cmonth == 13)
                    //    {
                    //        cmonth = 1;
                    //        cyear = cyear + 1;
                    //    }
                    //}
                    //tr_lst_det.BackColor = System.Drawing.Color.FromName("#A6A6D2");
                    //   tbl.Rows.Add(tr_lst_det);
                
                if (months >= 0)
                {
                   
                    for (int j = 1; j <= months + 1; j++)
                    {
                        TableCell tc_month = new TableCell();
                        tc_month.ColumnSpan = dsDoctor.Tables[0].Rows.Count ;
                        //tc_month.RowSpan = 2;
                        Literal lit_month = new Literal();
                        lit_month.Text = "&nbsp;" + sf.getMonthName(cmonth.ToString()) + "-" + cyear;
                        tc_month.Style.Add("font-family", "Calibri");
                        tc_month.Style.Add("font-size", "10pt");
                        tc_month.Attributes.Add("Class", "tr_det_head");
                        tc_month.BorderStyle = BorderStyle.Solid;
                        tc_month.BorderWidth = 1;

                        tc_month.HorizontalAlign = HorizontalAlign.Center;
                        //tc_month.Width = 200;
                        tc_month.Controls.Add(lit_month);
                       // tr_header.Cells.Add(tc_month);
                        tr_catg1.Cells.Add(tc_month);
                        cmonth = cmonth + 1;
                        if (cmonth == 13)
                        {
                            cmonth = 1;
                            cyear = cyear + 1;
                        }
                    }
                }
                TableCell tc_Work = new TableCell();
                tc_Work.BorderStyle = BorderStyle.Solid;
                tc_Work.BorderColor = System.Drawing.Color.Black;
                tc_Work.ColumnSpan = months + 1;
                tc_Work.BorderWidth = 1;
                //  tc_Work.RowSpan = 1;
                tc_Work.Style.Add("font-family", "Calibri");
                tc_Work.Style.Add("font-size", "10pt");
                Literal lit_Work = new Literal();
                tc_Work.Attributes.Add("Class", "tr_det_head");
                lit_Work.Text = "<center><b>Work With</b></center>";
                tc_Work.Controls.Add(lit_Work);
                tr_header.Cells.Add(tc_Work);


                TableCell tc_Prod = new TableCell();
                tc_Prod.BorderStyle = BorderStyle.Solid;
                tc_Prod.BorderColor = System.Drawing.Color.Black;
                tc_Prod.BorderWidth = 1;
                tc_Prod.Style.Add("font-family", "Calibri");
                tc_Prod.Style.Add("font-size", "10pt");
                tc_Prod.RowSpan = 3;
                Literal lit_Prod = new Literal();
                tc_Prod.Attributes.Add("Class", "tr_det_head");
                lit_Prod.Text = "<center><b>Sample Given Product</b></center>";
                tc_Prod.Controls.Add(lit_Prod);
                tr_header.Cells.Add(tc_Prod);

                TableCell tc_Input = new TableCell();
                tc_Input.BorderStyle = BorderStyle.Solid;
                tc_Input.BorderColor = System.Drawing.Color.Black;
                tc_Input.BorderWidth = 1;
                tc_Input.Style.Add("font-family", "Calibri");
                tc_Input.Style.Add("font-size", "10pt");
                tc_Input.RowSpan = 3;
                Literal lit_Input = new Literal();
                tc_Input.Attributes.Add("Class", "tr_det_head");
                lit_Input.Text = "<center><b>Input Given</b></center>";
                tc_Input.Controls.Add(lit_Input);
                tr_header.Cells.Add(tc_Input);


                //TableRow tr_catg3 = new TableRow();
                int months1 = (Convert.ToInt32(TYear) - Convert.ToInt32(FYear)) * 12 + Convert.ToInt32(TMonth) - Convert.ToInt32(FMonth); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
                //  int months1 = (Convert.ToInt32(TYear) - Convert.ToInt32(FYear)) * 12 + Convert.ToInt32(TMonth) - Convert.ToInt32(FMonth); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
                int cmonth1 = Convert.ToInt32(FMonth);
                int cyear1 = Convert.ToInt32(FYear);

                ViewState["months"] = months1;
                ViewState["cmonth"] = cmonth1;
                ViewState["cyear"] = cyear1;

                if (months1 >= 0)
                {
                    // TableRow tr_catg3 = new TableRow();
                    for (int j = 1; j <= months1 + 1; j++)
                    {
                        TableCell tc_month1 = new TableCell();
                        // tc_month1.ColumnSpan = months + 1;
                        tc_month1.RowSpan =2;
                        Literal lit_month1 = new Literal();
                        lit_month1.Text = "&nbsp;" + sf.getMonthName(cmonth1.ToString()) + "-" + cyear1;
                        tc_month1.Style.Add("font-family", "Calibri");
                        tc_month1.Style.Add("font-size", "10pt");
                        tc_month1.Attributes.Add("Class", "tr_det_head");
                        tc_month1.BorderStyle = BorderStyle.Solid;
                        tc_month1.BorderWidth = 1;

                        tc_month1.HorizontalAlign = HorizontalAlign.Center;
                        //tc_month.Width = 200;
                        tc_month1.Controls.Add(lit_month1);
                        // tr_header.Cells.Add(tc_month);
                        tr_catg1.Cells.Add(tc_month1);
                        cmonth1 = cmonth1 + 1;
                        if (cmonth1 == 13)
                        {
                            cmonth1 = 1;
                            cyear1 = cyear1 + 1;
                        }
                    }


                }
                tbl.Rows.Add(tr_catg1);
              //  tbl.Rows.Add(tr_catg1);

                //Sub Header
                months = Convert.ToInt16(ViewState["months"].ToString());
                cmonth = Convert.ToInt16(ViewState["cmonth"].ToString());
                cyear = Convert.ToInt16(ViewState["cyear"].ToString());
               // cmonthact = Convert.ToInt16(ViewState["cmonthact"].ToString());

              //  ViewState["cmonthact"] = cmonthact;

                TableRow tr_catg2 = new TableRow();
                if (months >= 0)
                {
                   

                    //tr_catg.BackColor = System.Drawing.Color.FromName("#A6A6D2");

                    for (int j = 1; j <= (months + 1) ; j++)
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
                                    //tc_catg_name.BackColor = System.Drawing.Color.LavenderBlush;
                                }
                                else
                                {
                                    //tc_catg_name.BackColor = System.Drawing.Color.PapayaWhip;
                                }
                                tc_catg_name.Width = 30;
                                tc_catg_name.Style.Add("font-family", "Calibri");
                                tc_catg_name.Style.Add("font-size", "10pt");
                                tc_catg_name.Attributes.Add("Class", "tr_det_head");
                                Literal lit_catg_name = new Literal();
                                lit_catg_name.Text = "<center>" + dataRow["sf_Designation_Short_Name"].ToString() + "</center>";
                             //   tc_catg_name.Attributes.Add("Class", "Backcolor");
                                tc_catg_name.Controls.Add(lit_catg_name);
                                tr_catg2.Cells.Add(tc_catg_name);
                            }

                            tbl.Rows.Add(tr_catg2);
                        }
                    }
                }

                Table tbl_head_empty2 = new Table();
                tbl_head_empty2.HorizontalAlign = HorizontalAlign.Center;
                TableRow tr_head_empty2 = new TableRow();
                TableCell tc_head_empty2 = new TableCell();
                tc_head_empty2.Style.Add("font-family", "Calibri");
                tc_head_empty2.Style.Add("font-size", "10pt");
                Literal lit_head_empty2 = new Literal();
                lit_head_empty2.Text = "<BR>";
                tc_head_empty2.Controls.Add(lit_head_empty1);
                tr_head_empty2.Cells.Add(tc_head_empty1);
                tbl_head_empty2.Rows.Add(tr_head_empty1);
                form1.Controls.Add(tbl_head_empty2);
                //new
               
                 Doctor camp = new Doctor();
                if (sf_code.Contains("MR"))
                {
                    dtRP = camp.getCamp_list_doc(sf_code, strCamp_code);
                }
                else
                {
                    dtRP = camp.getCamp_list_doc(drFF["sf_code"].ToString(), strCamp_code);
                }
                if (dtRP.Rows.Count > 0)
                {
                    foreach (DataRow drRP in dtRP.Rows)
                    {
                        iCount += 1;
                        TableRow tr_det = new TableRow();
                        tr_header.BorderStyle = BorderStyle.Solid;
                        tr_header.BorderWidth = 1;

                        TableCell tc_SNo_det = new TableCell();
                        tc_SNo_det.BorderStyle = BorderStyle.Solid;
                        tc_SNo_det.BorderColor = System.Drawing.Color.Black;
                        tc_SNo_det.BorderWidth = 1;
                        tc_SNo_det.Style.Add("font-family", "Calibri");
                        tc_SNo_det.Style.Add("font-size", "10pt");

                        Literal lit_SNo_det = new Literal();
                        lit_SNo_det.Text = iCount.ToString();
                        tc_SNo_det.Controls.Add(lit_SNo_det);
                        tr_det.Cells.Add(tc_SNo_det);                     


                        TableCell tc_doc_det = new TableCell();
                        tc_doc_det.BorderStyle = BorderStyle.Solid;
                        tc_doc_det.BorderWidth = 1;
                        //tc_FF.Width = 400;
                        tc_doc_det.BorderColor = System.Drawing.Color.Black;
                        tc_doc_det.Style.Add("font-family", "Calibri");
                        tc_doc_det.Style.Add("font-size", "10pt");

                        Literal lit_doc_det = new Literal();
                        lit_doc_det.Text = "&nbsp;" + drRP["ListedDr_Name"].ToString();
                        tc_doc_det.Controls.Add(lit_doc_det);
                        tr_det.Cells.Add(tc_doc_det);

                        TableCell tc_spec_det = new TableCell();
                        tc_spec_det.BorderStyle = BorderStyle.Solid;
                        tc_spec_det.BorderColor = System.Drawing.Color.Black;
                        tc_spec_det.BorderWidth = 1;
                        tc_spec_det.Style.Add("font-family", "Calibri");
                        tc_spec_det.Style.Add("font-size", "10pt");

                        Literal lit_spec_det = new Literal();
                        lit_spec_det.Text = "&nbsp;" + drRP["Doc_Spec_ShortName"].ToString();
                        tc_spec_det.Controls.Add(lit_spec_det);
                        tr_det.Cells.Add(tc_spec_det);

                        TableCell tc_catg_det = new TableCell();
                        tc_catg_det.BorderStyle = BorderStyle.Solid;
                        tc_catg_det.BorderColor = System.Drawing.Color.Black;
                        tc_catg_det.BorderWidth = 1;
                        tc_catg_det.Style.Add("font-family", "Calibri");
                        tc_catg_det.Style.Add("font-size", "10pt");

                        Literal lit_catg_det = new Literal();
                        lit_catg_det.Text = "&nbsp;" + drRP["Doc_Cat_ShortName"].ToString();
                        tc_catg_det.Controls.Add(lit_catg_det);
                        tr_det.Cells.Add(tc_catg_det);

                        TableCell tc_qual_det = new TableCell();
                        tc_qual_det.BorderStyle = BorderStyle.Solid;
                        tc_qual_det.BorderColor = System.Drawing.Color.Black;
                        tc_qual_det.BorderWidth = 1;
                        tc_qual_det.Style.Add("font-family", "Calibri");
                        tc_qual_det.Style.Add("font-size", "10pt");

                        Literal lit_qual_det = new Literal();
                        lit_qual_det.Text = "&nbsp;" + drRP["Doc_Qua_Name"].ToString();
                        tc_qual_det.Controls.Add(lit_qual_det);
                        tr_det.Cells.Add(tc_qual_det);

                        TableCell tc_class_det = new TableCell();
                        tc_class_det.BorderStyle = BorderStyle.Solid;
                        tc_class_det.BorderColor = System.Drawing.Color.Black;
                        tc_class_det.BorderWidth = 1;
                        tc_class_det.Style.Add("font-family", "Calibri");
                        tc_class_det.Style.Add("font-size", "10pt");

                        Literal lit_class_det = new Literal();
                        lit_class_det.Text = "&nbsp;" + drRP["Doc_Class_ShortName"].ToString();
                        tc_class_det.Controls.Add(lit_class_det);
                        tr_det.Cells.Add(tc_class_det);

                        DataSet dsMrDate = new DataSet();
                        if (months1 >= 0)
                        {
                            int cmonthact = Convert.ToInt32(FMonth);
                            int cyearact = Convert.ToInt32(FYear);
           
                            // TableRow tr_catg3 = new TableRow();
                            for (int j = 1; j <= months1 + 1; j++)
                            {
                                foreach (DataRow dataRow in dsDoctor.Tables[0].Rows)
                                {
                                    dsMrDate = camp.getDoc_MRCamp(div_code, drRP["ListedDrCode"].ToString(), dataRow["sf_code"].ToString(), cmonthact, cyearact);


                                    TableCell tc_class_MR = new TableCell();
                                    tc_class_MR.BorderStyle = BorderStyle.Solid;
                                    tc_class_MR.BorderColor = System.Drawing.Color.Black;
                                    tc_class_MR.BorderWidth = 1;
                                    tc_class_MR.Style.Add("font-family", "Calibri");
                                    tc_class_MR.Style.Add("font-size", "10pt");
                                    Literal lit_class_MR = new Literal();
                                    strdate = "";
                                    if (dsMrDate.Tables[0].Rows.Count > 0)
                                    {
                                        for (int i = 0; i < dsMrDate.Tables[0].Rows.Count; i++)
                                        {
                                            strdate += dsMrDate.Tables[0].Rows[i]["Activity_Date"].ToString() + ",";
                                            lit_class_MR.Text = strdate;
                                        }
                                    }

                                    tc_class_MR.Controls.Add(lit_class_MR);
                                    tr_det.Cells.Add(tc_class_MR);                                    

                                }

                                cmonthact = cmonthact + 1;
                                if (cmonthact == 13)
                                {
                                    cmonthact = 1;
                                    cyear1 = cyear1 + 1;
                                }
                            }
                        }
                        Literal lit_class_Product = new Literal();
                        Literal lit_det_gift = new Literal();
                        if (months1 >= 0)
                        {
                            int cmonthWorkthwith = Convert.ToInt32(FMonth);
                            int cyearWorkthwith = Convert.ToInt32(FYear);
                            Doctor camp1 = new Doctor();
                            // TableRow tr_catg3 = new TableRow();
                            for (int j = 1; j <= months1 + 1; j++)
                            {
                                strwork = "";
                                TableCell tc_workwith = new TableCell();
                                Literal lit_workwith = new Literal();
                                tc_workwith.BorderStyle = BorderStyle.Solid;
                                tc_workwith.BorderColor = System.Drawing.Color.Black;
                                tc_workwith.BorderWidth = 1;
                                tc_workwith.Style.Add("font-family", "Calibri");
                                tc_workwith.Style.Add("font-size", "10pt");
                                foreach (DataRow dataRow in dsDoctor.Tables[0].Rows)
                                {
                                    dsMrDate = camp.getDoc_MRCamp(div_code, drRP["ListedDrCode"].ToString(), dataRow["sf_code"].ToString(), cmonthWorkthwith, cyearWorkthwith);
                                    for (int i = 0; i < dsMrDate.Tables[0].Rows.Count; i++)
                                    {
                                        string strday = dsMrDate.Tables[0].Rows[i]["Activity_Date"].ToString();
                                        dsworkwith = camp1.getDoc_MRCamp_WorkedWith(div_code, drRP["ListedDrCode"].ToString(), dataRow["sf_code"].ToString(), cmonthWorkthwith, Convert.ToInt32(strday), cyearWorkthwith);                             
                                        

                                        
                                        //  lit_workwith.Text = "<BR>";

                                        
                                        if (dsworkwith.Tables[0].Rows.Count > 0)
                                        {
                                            for (int k = 0; k < dsworkwith.Tables[0].Rows.Count; k++)
                                            {
                                                strwork += dsworkwith.Tables[0].Rows[k]["Worked_with_Name"].ToString();
                                                string[] strworkwith = strwork.Split(',');
                                                foreach (string str in strworkwith)
                                                {
                                                    if (str != "SELF")
                                                    {
                                                        if (lit_workwith.Text.Contains(str))
                                                        {

                                                        }
                                                        else
                                                        {
                                                            lit_workwith.Text += str + ",";
                                                        }
                                                    }
                                                }
                                                
                                            }
                                        }

                                        
                                    }
                                    if (dsMrDate.Tables[0].Rows.Count > 0)
                                    {
                                        if (dsworkwith.Tables[0].Rows.Count > 0)
                                        {
                                            for (int i = 0; i < dsworkwith.Tables[0].Rows.Count; i++)
                                            {
                                                strdate += dsworkwith.Tables[0].Rows[i]["Product_Detail"].ToString().Replace("~", "(").Trim();

                                                lit_class_Product.Text = strdate;
                                            }
                                            //  }
                                        }
                                        if (dsworkwith.Tables[0].Rows.Count > 0)
                                        {
                                            for (int i = 0; i < dsworkwith.Tables[0].Rows.Count; i++)
                                            {
                                                lit_det_gift.Text = "&nbsp;&nbsp;" + dsworkwith.Tables[0].Rows[i]["Gift_Name"].ToString().Replace("~", "").Trim();
                                            }
                                        }
                                    }
                                   
                                }

                                tc_workwith.Controls.Add(lit_workwith);
                                tr_det.Cells.Add(tc_workwith);
                               

                                cmonthWorkthwith = cmonthWorkthwith + 1;
                                if (cmonthWorkthwith == 13)
                                {
                                    cmonthWorkthwith = 1;
                                    cyear1 = cyear1 + 1;
                                }
                            }
                        }

                        //TableCell tc_sample = new TableCell();
                        //tc_sample.BorderStyle = BorderStyle.Solid;
                        //tc_sample.BorderColor = System.Drawing.Color.Black;
                        //tc_sample.BorderWidth = 1;
                        //tc_sample.Style.Add("font-family", "Calibri");
                        //tc_sample.Style.Add("font-size", "10pt");
                        //Literal lit_sample = new Literal();
                        //lit_sample.Text = "<BR>";
                        //tc_sample.Controls.Add(lit_sample);
                        //tr_det.Cells.Add(tc_sample);

                        TableCell tc_class_Product = new TableCell();
                        tc_class_Product.BorderStyle = BorderStyle.Solid;
                        tc_class_Product.BorderColor = System.Drawing.Color.Black;
                        tc_class_Product.BorderWidth = 1;
                        tc_class_Product.Style.Add("font-family", "Calibri");
                        tc_class_Product.Style.Add("font-size", "10pt");
                        
                        //foreach (DataRow dataRow in dsDoctor.Tables[0].Rows)
                        //{
                        //    if (dsworkwith.Tables[0].Rows.Count > 0)
                           

                        lit_class_Product.Text = lit_class_Product.Text.Replace("$", ")").Trim();
                        lit_class_Product.Text = "&nbsp;&nbsp;" + lit_class_Product.Text.Replace("#", "  ").Trim();
                       // lit_class_Product.Text = "";
                        tc_class_Product.Controls.Add(lit_class_Product);
                        tr_det.Cells.Add(tc_class_Product);


                        TableCell tc_det_gift = new TableCell();
                        tc_det_gift.BorderStyle = BorderStyle.Solid;
                        tc_det_gift.BorderColor = System.Drawing.Color.Black;
                        tc_det_gift.BorderWidth = 1;
                        tc_det_gift.Style.Add("font-family", "Calibri");
                        tc_det_gift.Style.Add("font-size", "10pt");
                        
                        //if (dsworkwith.Tables[0].Rows.Count > 0)
                        //{
                        //    for (int i = 0; i < dsworkwith.Tables[0].Rows.Count; i++)
                        //    {
                        //        lit_det_gift.Text = "&nbsp;&nbsp;" + dsworkwith.Tables[0].Rows[i]["Gift_Name"].ToString().Replace("~", "-").Trim();
                        //    }
                        //}
                        tc_det_gift.BorderStyle = BorderStyle.Solid;
                        tc_det_gift.BorderWidth = 1;
                        tc_det_gift.Controls.Add(lit_det_gift);
                        tr_det.Cells.Add(tc_det_gift);
                     
                        tbl.Rows.Add(tr_det);

                    }

                }
                else
                {

                   tbl.Visible = false;

                    Table tbl_new = new Table();
                    TableRow tr_det_Pending = new TableRow();
                    TableCell tc_det_Pending = new TableCell();
                    tbl_new.HorizontalAlign = HorizontalAlign.Center;
                    iCount += 1;
                    Literal lit_det_SNo = new Literal();
                    lit_det_SNo.Text = "<center> <b> No Campaign Doctors Mapped... </b> </center>";
                    tc_det_Pending.Style.Add("color", "Red");
                    tc_det_Pending.Style.Add("border", "none");
                    tc_det_Pending.BorderStyle = BorderStyle.Solid;
                    tc_det_Pending.BorderWidth = 1;
                    tc_det_Pending.Controls.Add(lit_det_SNo);
                    tr_det_Pending.Cells.Add(tc_det_Pending);


                    tbl_new.Rows.Add(tr_det_Pending);
                    form1.Controls.Add(tbl_new);
                  //  lblNoRecord.Visible = true;
                    //pnltype.Visible = false;

                }

                form1.Controls.Add(tbl);

                

                Table tbl_line = new Table();
                tbl_line.BorderStyle = BorderStyle.None;
                tbl_line.Width = 1000;
                tbl_line.Style.Add("border-collapse", "collapse");
                tbl_line.Style.Add("border-top", "none");
                tbl_line.Style.Add("border-right", "none");
                tbl_line.Style.Add("margin-left", "150px");
                tbl_line.Style.Add("border-bottom ", "solid 1px Black");

               // form1.Controls.Add(tbldetail_mainHoliday);

                TableRow tr_line = new TableRow();

                TableCell tc_line0 = new TableCell();
                tc_line0.Width = 100;
                Literal lit_line0 = new Literal();
                lit_line0.Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                tc_line0.Controls.Add(lit_line0);
                tr_line.Cells.Add(tc_line0);

                TableCell tc_line = new TableCell();
                tc_line.Width = 1000;
                Literal lit_line = new Literal();
                // lit_line.Text = "<u>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</u>";
                tc_line.Controls.Add(lit_line);
                tr_line.Cells.Add(tc_line);
                tbl_line.Rows.Add(tr_line);
                form1.Controls.Add(tbl_line);

            }
           

        }
      
    }
    private void ExportButton()
    {
        btnPDF.Visible = false;
    }
    protected void btnPDF_Click(object sender, EventArgs e)
    {
        string strFileName = "rptTPView";
        Response.ContentType = "application/pdf";
        Response.AddHeader("content-disposition", "attachment;filename=" + strFileName + ".pdf");
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        StringWriter sw = new StringWriter();
        HtmlTextWriter hw = new HtmlTextWriter(sw);
        HtmlForm frm = new HtmlForm();
        pnlContents.Parent.Controls.Add(frm);
        frm.Attributes["runat"] = "server";
        frm.Controls.Add(pnlContents);
        frm.RenderControl(hw);
        StringReader sr = new StringReader(sw.ToString());
        Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 4f);
        iTextSharp.text.html.simpleparser.HTMLWorker htmlparser = new iTextSharp.text.html.simpleparser.HTMLWorker(pdfDoc);
        PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
        pdfDoc.Open();
        htmlparser.Parse(sr);
        pdfDoc.Close();
        Response.Write(pdfDoc);
        Response.End();


    }

    public override void VerifyRenderingInServerForm(Control txt_salutaion)
    {
        /* Verifies that the control is rendered */
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
    protected void btnExcel_Click(object sender, EventArgs e)
    {
        string Export = this.Page.Title;
        string attachment = "attachment; filename=" + Export + ".xls";
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

    //protected void TimerTick(object sender, EventArgs e)
    //{
    //    this.CreateDynamicTable();
    //    Timer1.Enabled = false;
    //    ShowProgressDiv.Visible = false;

    //}
}