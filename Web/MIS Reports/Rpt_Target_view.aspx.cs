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
using iTextSharp.tool.xml;
using Bus_EReport;
using System.Net;
public partial class Rpt_Target_view : System.Web.UI.Page
{
    string divcode = string.Empty;
    int FMonth = 0;
    int FYear = 0;
    string TMonth = string.Empty;
    string TYear = string.Empty;
    string Monthsub = string.Empty;
    string sCurrentDate = string.Empty;
    string tot_dr = string.Empty;
    string sf_code = string.Empty;
    string subdivision = string.Empty;
    string dist_name = string.Empty;
    string fdate = string.Empty;
    string tdate = string.Empty;
    string type = string.Empty;
    string Mode = string.Empty;
    DataSet dsSalesForce = new DataSet();
    DataSet dstarget = new DataSet();
    DataSet dsQuantity = new DataSet();

    DataSet dsDoc = new DataSet();

    protected void Page_Load(object sender, EventArgs e)
    {
        divcode = Session["div_code"].ToString();
        TMonth = Request.QueryString["Month"].ToString();
        TYear = Request.QueryString["Year"].ToString();
        sf_code = Request.QueryString["SF_Code"].ToString();
        Mode = Request.QueryString["Mode"].ToString();
        type = Request.QueryString["type"].ToString();
        subdivision = Request.QueryString["Sub_Div"].ToString();
        lblIDMonth.Text = "<span style='color:blue'>Field Force : </span>" + Request.QueryString["SF_Name"].ToString();

        FMonth = Convert.ToInt32(TMonth);
        FYear = Convert.ToInt32(TYear);

        if (Mode == "1")
        {

            FillSF();
        }
        else
        {
            Product_details();
        }
    }

    private void Product_details()
    {
        string sURL = string.Empty;
        string stURL = string.Empty;

        tbl.Rows.Clear();

        Product pro = new Product();
        dsSalesForce = pro.getproductname(divcode,subdivision);

        dstarget = pro.gettarget(divcode, sf_code, TYear, TMonth);

        dsQuantity = pro.getquantity(divcode, sf_code, TYear, TMonth);

        DataSet dsmrp = pro.get_mrp(divcode, sf_code);


        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            TableRow tr_header = new TableRow();
            tr_header.BorderStyle = BorderStyle.Solid;
            tr_header.BorderWidth = 1;
            tr_header.BackColor = System.Drawing.Color.FromName("#496a9a");
            tr_header.Style.Add("Color", "White");
            tr_header.BorderColor = System.Drawing.Color.Black;

            TableCell tc_SNo = new TableCell();
            tc_SNo.BorderStyle = BorderStyle.Solid;
            tc_SNo.BorderWidth = 1;
            tc_SNo.Width = 50;
            tc_SNo.RowSpan = 1;
            Literal lit_SNo =
                new Literal();
            lit_SNo.Text = "S.No";
            tc_SNo.BorderColor = System.Drawing.Color.Black;
            tc_SNo.Controls.Add(lit_SNo);
            tc_SNo.Attributes.Add("Class", "rptCellBorder");
            tr_header.Cells.Add(tc_SNo);

            TableCell tc_DR_Code = new TableCell();
            tc_DR_Code.BorderStyle = BorderStyle.Solid;
            tc_DR_Code.BorderWidth = 1;
            tc_DR_Code.Width = 400;
            tc_DR_Code.RowSpan = 1;
            Literal lit_DR_Code = new Literal();
            lit_DR_Code.Text = "<center>Product Name</center>";
            tc_DR_Code.Controls.Add(lit_DR_Code);
            tc_DR_Code.Attributes.Add("Class", "rptCellBorder");
            tc_DR_Code.BorderColor = System.Drawing.Color.Black;
            tr_header.Cells.Add(tc_DR_Code);




            TableCell tc_DR_Name = new TableCell();
            tc_DR_Name.BorderStyle = BorderStyle.Solid;
            tc_DR_Name.BorderWidth = 1;
            tc_DR_Name.Width = 250;
            tc_DR_Name.RowSpan = 1;
            Literal lit_DR_Name = new Literal();
            lit_DR_Name.Text = "<center>Target</center>";
            tc_DR_Name.BorderColor = System.Drawing.Color.Black;
            tc_DR_Name.Attributes.Add("Class", "rptCellBorder");
            tc_DR_Name.Controls.Add(lit_DR_Name);
            tr_header.Cells.Add(tc_DR_Name);


            TableCell tc_qty_name = new TableCell();
            tc_qty_name.BorderStyle = BorderStyle.Solid;
            tc_qty_name.BorderWidth = 1;
            tc_qty_name.Width = 250;
            tc_qty_name.RowSpan = 1;
            Literal lit_qty_Name = new Literal();
            lit_qty_Name.Text = "Achieved";
            tc_qty_name.BorderColor = System.Drawing.Color.Black;
            tc_qty_name.Attributes.Add("Class", "rptCellBorder");
            tc_qty_name.Style.Add("text-align", "center");
            tc_qty_name.Controls.Add(lit_qty_Name);
            tr_header.Cells.Add(tc_qty_name);


            TableCell tc_bal_name = new TableCell();
            tc_bal_name.BorderStyle = BorderStyle.Solid;
            tc_bal_name.BorderWidth = 1;
            tc_bal_name.Width = 250;
            tc_bal_name.RowSpan = 1;
            Literal lit_bal_Name = new Literal();
            lit_bal_Name.Text = "<center>Balance</center>";
            tc_bal_name.BorderColor = System.Drawing.Color.Black;
            tc_bal_name.Attributes.Add("Class", "rptCellBorder");
            tc_bal_name.Controls.Add(lit_bal_Name);
            tr_header.Cells.Add(tc_bal_name);

            TableCell tc_ach_name = new TableCell();
            tc_ach_name.BorderStyle = BorderStyle.Solid;
            tc_ach_name.BorderWidth = 1;
            tc_ach_name.Width = 250;
            tc_ach_name.RowSpan = 1;
            Literal lit_ach_Name = new Literal();
            lit_ach_Name.Text = "<center>Achievement %</center>";
            tc_ach_name.BorderColor = System.Drawing.Color.Black;
            tc_ach_name.Attributes.Add("Class", "rptCellBorder");
            tc_ach_name.Controls.Add(lit_ach_Name);
            tr_header.Cells.Add(tc_ach_name);



            tbl.Rows.Add(tr_header);
            //Sub Header



            if (dsSalesForce.Tables[0].Rows.Count > 0)
                ViewState["dsSalesForce"] = dsSalesForce;


            int iCount = 0;
            //string iTotLstCount ="0";
            dsSalesForce = (DataSet)ViewState["dsSalesForce"];

            foreach (DataRow drFF in dsSalesForce.Tables[0].Rows)
            {
                TableRow tr_det = new TableRow();
                iCount += 1;


                //S.No
                TableCell tc_det_SNo = new TableCell();
                Literal lit_det_SNo = new Literal();
                lit_det_SNo.Text = "<center>" + iCount.ToString() + "</center>";
                tc_det_SNo.BorderStyle = BorderStyle.Solid;
                tc_det_SNo.BorderWidth = 1;
                tc_det_SNo.Attributes.Add("Class", "rptCellBorder");
                tc_det_SNo.Controls.Add(lit_det_SNo);
                tr_det.Cells.Add(tc_det_SNo);
               // tr_det.BackColor = System.Drawing.Color.White;

                //SF_code
                TableCell tc_det_usr = new TableCell();
                Literal lit_det_usr = new Literal();
                lit_det_usr.Text = "&nbsp;" + drFF["Product_Detail_Name"].ToString();
                tc_det_usr.BorderStyle = BorderStyle.Solid;
                tc_det_usr.BorderWidth = 1;
                tc_det_usr.Attributes.Add("Class", "rptCellBorder");
                tc_det_usr.Controls.Add(lit_det_usr);
                tr_det.Cells.Add(tc_det_usr);
                TableCell tc_det_usrr = new TableCell();
                tc_det_usrr.Visible = false;
                Literal lit_det_usrr = new Literal();
                lit_det_usrr.Text = "&nbsp;" + drFF["Product_Detail_Code"].ToString();
                tc_det_usrr.BorderStyle = BorderStyle.Solid;
                tc_det_usrr.BorderWidth = 1;
                tc_det_usrr.Attributes.Add("Class", "rptCellBorder");
                tc_det_usrr.Controls.Add(lit_det_usrr);
                tr_det.Cells.Add(tc_det_usrr);


                //target
                TableCell tc_det_FF = new TableCell();
                tc_det_FF.Width = 200;
                HyperLink lit_det_FF = new HyperLink();
                DataTable dt = new DataTable();
                DataTable dtmrp = new DataTable();
                DataView dv = dstarget.Tables[0].DefaultView;
                dv.RowFilter = "PRODUCT_CODE='" + drFF["Product_Detail_Code"].ToString() + "'";
                dt = dv.ToTable();

                DataView mrp = dsmrp.Tables[0].DefaultView;

                mrp.RowFilter = "Product_Detail_Code='" + drFF["Product_Detail_Code"].ToString() + "'";
                dtmrp = mrp.ToTable();

                string tar = "0.00";
                if (dt.Rows.Count > 0)
                {
                    if (type == "1")
                    {
                        tar = Convert.ToDecimal(dt.Rows[0][1]).ToString("0.00");
                    }
                    else
                    {
                        if (dtmrp.Rows.Count > 0)
                        {
                            tar = (Convert.ToDecimal(dt.Rows[0][1]) * Convert.ToDecimal(dtmrp.Rows[0][1])).ToString("0.00");
                        }
                        else
                        {
                            tar = (Convert.ToDecimal(dt.Rows[0][1]) * 0).ToString("0.00");
                        }
                    }
                }
                lit_det_FF.Text = tar;
                tc_det_FF.HorizontalAlign = HorizontalAlign.Left;
                tc_det_FF.BorderStyle = BorderStyle.Solid;
                tc_det_FF.BorderWidth = 1;
                tc_det_FF.Attributes.Add("Class", "rptCellBorder");
                tc_det_FF.Style.Add("text-align", "right");
                tc_det_FF.Controls.Add(lit_det_FF);
                tr_det.Cells.Add(tc_det_FF);


                //qty
                TableCell tc_qty = new TableCell();
                tc_qty.Width = 200;
                dt = new DataTable();
                dv = dsQuantity.Tables[0].DefaultView;
                dv.RowFilter = "Product_Code='" + drFF["Product_Detail_Code"].ToString() + "'";
                dt = dv.ToTable();
                string qty = "0.00";
                if (dt.Rows.Count > 0)
                {
                    if (type == "1")
                    {
                        qty = Convert.ToDecimal(dt.Rows[0][1]).ToString("0.00");
                    }
                    else
                    {
                        if (dtmrp.Rows.Count > 0)
                        {
                            qty = (Convert.ToDecimal(dt.Rows[0][1]) * Convert.ToDecimal(dtmrp.Rows[0][1])).ToString("0.00");
                        }
                        else
                        {
                            qty = (Convert.ToDecimal(dt.Rows[0][1]) * 0).ToString("0.00");
                        }
                    }
                }
                Literal lit_qty = new Literal();
                lit_qty.Text = qty;
                tc_qty.HorizontalAlign = HorizontalAlign.Left;
                tc_qty.BorderStyle = BorderStyle.Solid;
                tc_qty.BorderWidth = 1;
                tc_qty.Attributes.Add("Class", "rptCellBorder");
                tc_qty.Style.Add("text-align", "right");
                tc_qty.Controls.Add(lit_qty);
                tr_det.Cells.Add(tc_qty);

                //bal
                TableCell tc_bal = new TableCell();
                tc_bal.Width = 200;

                Literal lit_bal = new Literal();
                decimal tot_qty = qty == "" ? 0 : Convert.ToDecimal(qty);
                decimal tot_tar = tar == "" ? 0 : Convert.ToDecimal(tar);
                lit_bal.Text = (tot_tar - tot_qty).ToString();
                tc_bal.HorizontalAlign = HorizontalAlign.Left;
                tc_bal.BorderStyle = BorderStyle.Solid;
                tc_bal.BorderWidth = 1;
                tc_bal.Attributes.Add("Class", "rptCellBorder");
                tc_bal.Style.Add("text-align", "right");
                tc_bal.Controls.Add(lit_bal);
                tr_det.Cells.Add(tc_bal);


                //ach
                TableCell tc_ach = new TableCell();
                tc_ach.Width = 200;

                Literal lit_ach = new Literal();

                if (tot_tar > 0)
                {
                    decimal tot_ach = tot_qty / tot_tar * 100;
                    lit_ach.Text = tot_ach.ToString("0.00");
                }
                else
                {
                    lit_ach.Text = "T. N/F";
                }


                tc_ach.HorizontalAlign = HorizontalAlign.Left;
                tc_ach.BorderStyle = BorderStyle.Solid;
                tc_ach.BorderWidth = 1;
                tc_ach.Attributes.Add("Class", "rptCellBorder");
                tc_ach.Style.Add("text-align", "right");
                tc_ach.Controls.Add(lit_ach);
                tr_det.Cells.Add(tc_ach);

                tbl.Rows.Add(tr_det);

            }
        }

    }



    private void FillSF()
    {
        string sURL = string.Empty;
        string stURL = string.Empty;

        tbl.Rows.Clear();
        SalesForce sf = new SalesForce();

        dsSalesForce = sf.UserList_getMR(divcode, sf_code, subdivision);

        Product pro = new Product();

        dstarget = pro.gettarget_Fo(divcode, sf_code, TYear, TMonth);

        dsQuantity = pro.getquantity_Fo(divcode, sf_code, TYear, TMonth);

        //DataSet dsmrp = pro.get_mrp(divcode, sf_code);


        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            TableRow tr_header = new TableRow();
            tr_header.BorderStyle = BorderStyle.Solid;
            tr_header.BorderWidth = 1;
            tr_header.BackColor = System.Drawing.Color.FromName("#496a9a");
            tr_header.Style.Add("Color", "White");
            tr_header.BorderColor = System.Drawing.Color.Black;

            TableCell tc_SNo = new TableCell();
            tc_SNo.BorderStyle = BorderStyle.Solid;
            tc_SNo.BorderWidth = 1;
            tc_SNo.Width = 50;
            tc_SNo.RowSpan = 1;
            Literal lit_SNo =
                new Literal();
            lit_SNo.Text = "S.No";
            tc_SNo.BorderColor = System.Drawing.Color.Black;
            tc_SNo.Controls.Add(lit_SNo);
            tc_SNo.Attributes.Add("Class", "rptCellBorder");
            tr_header.Cells.Add(tc_SNo);

            TableCell tc_DR_Code = new TableCell();
            tc_DR_Code.BorderStyle = BorderStyle.Solid;
            tc_DR_Code.BorderWidth = 1;
            tc_DR_Code.Width = 400;
            tc_DR_Code.RowSpan = 1;
            Literal lit_DR_Code = new Literal();
            lit_DR_Code.Text = "<center>Sales Force Name</center>";
            tc_DR_Code.Controls.Add(lit_DR_Code);
            tc_DR_Code.Attributes.Add("Class", "rptCellBorder");
            tc_DR_Code.BorderColor = System.Drawing.Color.Black;
            tr_header.Cells.Add(tc_DR_Code);




            TableCell tc_DR_Name = new TableCell();
            tc_DR_Name.BorderStyle = BorderStyle.Solid;
            tc_DR_Name.BorderWidth = 1;
            tc_DR_Name.Width = 250;
            tc_DR_Name.RowSpan = 1;
            Literal lit_DR_Name = new Literal();
            lit_DR_Name.Text = "<center>Target</center>";
            tc_DR_Name.BorderColor = System.Drawing.Color.Black;
            tc_DR_Name.Attributes.Add("Class", "rptCellBorder");
            tc_DR_Name.Controls.Add(lit_DR_Name);
            tr_header.Cells.Add(tc_DR_Name);


            TableCell tc_qty_name = new TableCell();
            tc_qty_name.BorderStyle = BorderStyle.Solid;
            tc_qty_name.BorderWidth = 1;
            tc_qty_name.Width = 250;
            tc_qty_name.RowSpan = 1;
            Literal lit_qty_Name = new Literal();
            lit_qty_Name.Text = "Achieved";
            tc_qty_name.BorderColor = System.Drawing.Color.Black;
            tc_qty_name.Attributes.Add("Class", "rptCellBorder");
            tc_qty_name.Style.Add("text-align", "center");
            tc_qty_name.Controls.Add(lit_qty_Name);
            tr_header.Cells.Add(tc_qty_name);


            TableCell tc_bal_name = new TableCell();
            tc_bal_name.BorderStyle = BorderStyle.Solid;
            tc_bal_name.BorderWidth = 1;
            tc_bal_name.Width = 250;
            tc_bal_name.RowSpan = 1;
            Literal lit_bal_Name = new Literal();
            lit_bal_Name.Text = "<center>Balance</center>";
            tc_bal_name.BorderColor = System.Drawing.Color.Black;
            tc_bal_name.Attributes.Add("Class", "rptCellBorder");
            tc_bal_name.Controls.Add(lit_bal_Name);
            tr_header.Cells.Add(tc_bal_name);

            TableCell tc_ach_name = new TableCell();
            tc_ach_name.BorderStyle = BorderStyle.Solid;
            tc_ach_name.BorderWidth = 1;
            tc_ach_name.Width = 250;
            tc_ach_name.RowSpan = 1;
            Literal lit_ach_Name = new Literal();
            lit_ach_Name.Text = "<center>Achievement %</center>";
            tc_ach_name.BorderColor = System.Drawing.Color.Black;
            tc_ach_name.Attributes.Add("Class", "rptCellBorder");
            tc_ach_name.Controls.Add(lit_ach_Name);
            tr_header.Cells.Add(tc_ach_name);



            tbl.Rows.Add(tr_header);
            //Sub Header



            if (dsSalesForce.Tables[0].Rows.Count > 0)
                ViewState["dsSalesForce"] = dsSalesForce;


            int iCount = 0;
            //string iTotLstCount ="0";
            dsSalesForce = (DataSet)ViewState["dsSalesForce"];

            foreach (DataRow drFF in dsSalesForce.Tables[0].Rows)
            {
                TableRow tr_det = new TableRow();
                iCount += 1;


                //S.No
                TableCell tc_det_SNo = new TableCell();
                Literal lit_det_SNo = new Literal();
                lit_det_SNo.Text = "<center>" + iCount.ToString() + "</center>";
                tc_det_SNo.BorderStyle = BorderStyle.Solid;
                tc_det_SNo.BorderWidth = 1;
                tc_det_SNo.Attributes.Add("Class", "rptCellBorder");
                tc_det_SNo.Controls.Add(lit_det_SNo);
                tr_det.Cells.Add(tc_det_SNo);
                tr_det.BackColor = System.Drawing.Color.White;

                //SF_code
                TableCell tc_det_usr = new TableCell();
                Literal lit_det_usr = new Literal();
                lit_det_usr.Text = "&nbsp;" + drFF["Sf_Name"].ToString();
                tc_det_usr.BorderStyle = BorderStyle.Solid;
                tc_det_usr.BorderWidth = 1;
                tc_det_usr.Attributes.Add("Class", "rptCellBorder");
                tc_det_usr.Controls.Add(lit_det_usr);
                tr_det.Cells.Add(tc_det_usr);
                TableCell tc_det_usrr = new TableCell();
                tc_det_usrr.Visible = false;
                Literal lit_det_usrr = new Literal();
                lit_det_usrr.Text = "&nbsp;" + drFF["SF_Code"].ToString();
                tc_det_usrr.BorderStyle = BorderStyle.Solid;
                tc_det_usrr.BorderWidth = 1;
                tc_det_usrr.Attributes.Add("Class", "rptCellBorder");
                tc_det_usrr.Controls.Add(lit_det_usrr);
                tr_det.Cells.Add(tc_det_usrr);


                //target
                TableCell tc_det_FF = new TableCell();
                tc_det_FF.Width = 200;
                HyperLink lit_det_FF = new HyperLink();
                DataTable dt = new DataTable();
                DataTable dtmrp = new DataTable();
                DataView dv = dstarget.Tables[0].DefaultView;
                dv.RowFilter = "SF_Code='" + drFF["SF_Code"].ToString() + "'";
                dt = dv.ToTable();
                string tar = "0";
                if (dt.Rows.Count > 0)
                {
                    if (type == "1")
                    {
                        tar = dt.Rows[0][1].ToString();
                    }
                    else
                    {
                        tar = dt.Rows[0][2].ToString();
                    }
                }
                lit_det_FF.Text = tar;
                tc_det_FF.HorizontalAlign = HorizontalAlign.Left;
                tc_det_FF.BorderStyle = BorderStyle.Solid;
                tc_det_FF.BorderWidth = 1;
                tc_det_FF.Attributes.Add("Class", "rptCellBorder");
                tc_det_FF.Style.Add("text-align", "right");
                tc_det_FF.Controls.Add(lit_det_FF);
                tr_det.Cells.Add(tc_det_FF);


                //qty
                TableCell tc_qty = new TableCell();
                tc_qty.Width = 200;
                dt = new DataTable();
                dv = dsQuantity.Tables[0].DefaultView;
                dv.RowFilter = "SF_Code='" + drFF["SF_Code"].ToString() + "'";
                dt = dv.ToTable();
                string qty = "0";
                if (dt.Rows.Count > 0)
                {
                    if (type == "1")
                    {
                        qty = dt.Rows[0][1].ToString();
                    }
                    else
                    {
                        qty = dt.Rows[0][2].ToString();
                    }
                }
                Literal lit_qty = new Literal();
                lit_qty.Text = qty;
                tc_qty.HorizontalAlign = HorizontalAlign.Left;
                tc_qty.BorderStyle = BorderStyle.Solid;
                tc_qty.BorderWidth = 1;
                tc_qty.Attributes.Add("Class", "rptCellBorder");
                tc_qty.Style.Add("text-align", "right");
                tc_qty.Controls.Add(lit_qty);
                tr_det.Cells.Add(tc_qty);

                //bal
                TableCell tc_bal = new TableCell();
                tc_bal.Width = 200;

                Literal lit_bal = new Literal();
                decimal tot_qty = qty == "" ? 0 : Convert.ToDecimal(qty);
                decimal tot_tar = tar == "" ? 0 : Convert.ToDecimal(tar);
                lit_bal.Text = (tot_tar - tot_qty).ToString();
                tc_bal.HorizontalAlign = HorizontalAlign.Left;
                tc_bal.BorderStyle = BorderStyle.Solid;
                tc_bal.BorderWidth = 1;
                tc_bal.Attributes.Add("Class", "rptCellBorder");
                tc_bal.Style.Add("text-align", "right");
                tc_bal.Controls.Add(lit_bal);
                tr_det.Cells.Add(tc_bal);


                //ach
                TableCell tc_ach = new TableCell();
                tc_ach.Width = 200;

                Literal lit_ach = new Literal();

                if (tot_tar > 0)
                {
                    decimal tot_ach = tot_qty / tot_tar * 100;
                    lit_ach.Text = tot_ach.ToString("0.00");
                }
                else
                {
                    lit_ach.Text = "T. N/F";
                }


                tc_ach.HorizontalAlign = HorizontalAlign.Left;
                tc_ach.BorderStyle = BorderStyle.Solid;
                tc_ach.BorderWidth = 1;
                tc_ach.Attributes.Add("Class", "rptCellBorder");
                tc_ach.Style.Add("text-align", "right");
                tc_ach.Controls.Add(lit_ach);
                tr_det.Cells.Add(tc_ach);

                tbl.Rows.Add(tr_det);

            }
        }

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
        form1.Parent.Controls.Add(frm);
        frm.Attributes["runat"] = "server";
        frm.Controls.Add(pnlContents);
        frm.RenderControl(htw);
        Response.Write(sw.ToString());
        Response.End();
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {

        string strFileName = Page.Title;

        using (StringWriter sw = new StringWriter())
        {
            using (HtmlTextWriter hw = new HtmlTextWriter(sw))
            {
                //this.Page.RenderControl(hw);
                this.RenderControl(hw);
                StringReader sr = new StringReader(sw.ToString());
                Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
                PdfWriter writer = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                pdfDoc.Open();
                XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                pdfDoc.Close();
                Response.ContentType = "application/pdf";
                Response.AddHeader("content-disposition", "attachment;filename= '" + strFileName + "'.pdf");
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.Write(pdfDoc);
                Response.End();
            }

        }
    }



    protected void btnClose_Click(object sender, EventArgs e)
    {

    }
}