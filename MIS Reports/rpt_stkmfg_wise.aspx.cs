using System;
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
using System.Web.UI.HtmlControls;

public partial class MIS_Reports_rpt_stkmfg_wise : System.Web.UI.Page
{
    #region
    public string sf_code = string.Empty;
    public string sfname = string.Empty;
    string div_code = string.Empty;
    //int div_co = 0;
    //int years = 0;
    public string Year = string.Empty;
 public string stkname = string.Empty;
    public string SF_Name = string.Empty;
 public string stkcode = string.Empty;
    DataSet dsSalesForce = new DataSet();
    DataSet dsStk = new DataSet();
    //public string Stockist_Code = string.Empty;
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        sf_code = Request.QueryString["SF_Code"].ToString(); // Session["SF_code"].ToString();
        Year = Request.QueryString["Year"].ToString();
  stkcode= Request.QueryString["stkcd"].ToString();
stkname=Request.QueryString["stkname"].ToString();
lbl.Text = "Closing Stock View of " + stkname + "";
        FillSF();

    }
    private void FillSF()
    {
        tbl.Rows.Clear();
        Order sf = new Order();
        dsSalesForce = sf.get_product_basStockist(div_code,stkcode);
        dsStk = sf.get_allStockist(div_code,stkcode);

        TableRow tr_headerr = new TableRow();
        tr_headerr.BackColor = System.Drawing.Color.FromName("#4aced6");
        tr_headerr.Style.Add("Color", "Black");
        tr_headerr.Style.Add("Font", "Bold");

        TableCell tc_SNod = new TableCell();
        tc_SNod.HorizontalAlign = HorizontalAlign.Right;
        tc_SNod.Width = 50;
        tc_SNod.RowSpan = 1;
        tc_SNod.ColumnSpan = 1;
        Literal lit_SNod = new Literal();
        lit_SNod.Text = "Date";
        tc_SNod.Controls.Add(lit_SNod);
        tc_SNod.Attributes.Add("Class", "rptCellBorder");
        tr_headerr.Cells.Add(tc_SNod);

        foreach (DataRow drFF in dsStk.Tables[0].Rows)
        {

            TableCell tc_SNodd = new TableCell();
            tc_SNodd.HorizontalAlign = HorizontalAlign.Left;
            tc_SNodd.Width = 50;
            tc_SNodd.RowSpan = 1;
            tc_SNodd.ColumnSpan =4;
            Literal lit_SNodd = new Literal();
            lit_SNodd.Text = drFF["up_date"].ToString();
            tc_SNodd.Controls.Add(lit_SNodd);
            tc_SNodd.Attributes.Add("Class", "rptCellBorder");
            tr_headerr.Cells.Add(tc_SNodd);
        }

        TableRow tr_headerr1 = new TableRow();
        TableCell tc_SNo = new TableCell();
        tc_SNo.BorderStyle = BorderStyle.Solid;
        tc_SNo.BorderWidth = 1;
        tc_SNo.Width = 50;
        tc_SNo.RowSpan = 1;
        Literal lit_SNo =
            new Literal();
        lit_SNo.Text = "Product Name";
        tc_SNo.BorderColor = System.Drawing.Color.Black;
        tc_SNo.Controls.Add(lit_SNo);
        tc_SNo.Attributes.Add("Class", "rptCellBorder");
        tr_headerr1.Cells.Add(tc_SNo);

        foreach (DataRow drStk in dsStk.Tables[0].Rows)
        {
            TableCell tc_DR_Code = new TableCell();
            tc_DR_Code.BorderStyle = BorderStyle.Solid;
            tc_DR_Code.BorderWidth = 1;
            tc_DR_Code.Width = 400;
            tc_DR_Code.RowSpan = 1;
            Literal lit_DR_Code = new Literal();
            lit_DR_Code.Text = "Quantity";
            tc_DR_Code.Controls.Add(lit_DR_Code);
            tc_DR_Code.Attributes.Add("Class", "rptCellBorder");
            tc_DR_Code.BorderColor = System.Drawing.Color.Black;
            tr_headerr1.Cells.Add(tc_DR_Code);

            TableCell tc_ValCell = new TableCell();
            tc_ValCell.BorderStyle = BorderStyle.Solid;
            tc_ValCell.BorderWidth = 1;
            tc_ValCell.Width = 400;
            tc_ValCell.RowSpan = 1;
            Literal lit_Valtx = new Literal();
            lit_Valtx.Text = "Value";
            tc_ValCell.Controls.Add(lit_Valtx);
            tc_ValCell.Attributes.Add("Class", "rptCellBorder");
            tc_ValCell.BorderColor = System.Drawing.Color.Black;
            tr_headerr1.Cells.Add(tc_ValCell);

            TableCell tc_NWCell = new TableCell();
            tc_NWCell.BorderStyle = BorderStyle.Solid;
            tc_NWCell.BorderWidth = 1;
            tc_NWCell.Width = 400;
            tc_NWCell.RowSpan = 1;
            Literal lit_NWtx = new Literal();
            lit_NWtx.Text = "Net Weight";
            tc_NWCell.Controls.Add(lit_NWtx);
            tc_NWCell.Attributes.Add("Class", "rptCellBorder");
            tc_NWCell.BorderColor = System.Drawing.Color.Black;
            tr_headerr1.Cells.Add(tc_NWCell);

            TableCell tc_DR_Code1 = new TableCell();
            tc_DR_Code1.BorderStyle = BorderStyle.Solid;
            tc_DR_Code1.BorderWidth = 1;
            tc_DR_Code1.Width = 400;
            tc_DR_Code1.RowSpan = 1;
            Literal lit_DR_Code1 = new Literal();
            lit_DR_Code1.Text = "Mfg Date";
            tc_DR_Code1.Controls.Add(lit_DR_Code1);
            tc_DR_Code1.Attributes.Add("Class", "rptCellBorder");
            tc_DR_Code1.BorderColor = System.Drawing.Color.Black;
            tr_headerr1.Cells.Add(tc_DR_Code1);
        }
        tbl.Rows.Add(tr_headerr);
        tbl.Rows.Add(tr_headerr1);
        string sprd = "";
        foreach (DataRow drFF in dsSalesForce.Tables[0].Rows)
        {
            TableRow tr_det = new TableRow();

            TableCell tc_det_usr = new TableCell();
            Literal lit_det_usr = new Literal();
            lit_det_usr.Text = "&nbsp;" + drFF["Product_Name"].ToString();
            tc_det_usr.BorderStyle = BorderStyle.Solid;
            tc_det_usr.BorderWidth = 1;
            tc_det_usr.Attributes.Add("Class", "rptCellBorder");
            tc_det_usr.Controls.Add(lit_det_usr);
            tr_det.Cells.Add(tc_det_usr);
            if (("," + sprd).IndexOf("," + drFF["Product_code"].ToString() + ",") < 0)
            {
                sprd += drFF["Product_code"].ToString() + ",";
                foreach (DataRow drStk in dsStk.Tables[0].Rows)
                {
                    DataRow[] ro = dsSalesForce.Tables[0].Select("product_code='" + drFF["Product_code"].ToString() + "' and up_date='" + drStk["up_date"].ToString() + "'");
                      if (ro.Length > 0)
                    {
                        TableCell tc_det_FF = new TableCell();
                        tc_det_FF.Width = 200;
                        Literal lit_det_FF = new Literal();
                        lit_det_FF.Text = ro[0]["qty"].ToString();
                        tc_det_FF.HorizontalAlign = HorizontalAlign.Center;
                        tc_det_FF.BorderStyle = BorderStyle.Solid;
                        tc_det_FF.BorderWidth = 1;
                        tc_det_FF.Attributes.Add("Class", "rptCellBorder");
                        tc_det_FF.Controls.Add(lit_det_FF);
                        tr_det.Cells.Add(tc_det_FF);

                        TableCell tc_ValCell = new TableCell();
                        tc_ValCell.Width = 200;
                        Literal lit_Valtx = new Literal();
                        lit_Valtx.Text = ro[0]["StateRate"].ToString();
                        tc_ValCell.HorizontalAlign = HorizontalAlign.Center;
                        tc_ValCell.BorderStyle = BorderStyle.Solid;
                        tc_ValCell.BorderWidth = 1;
                        tc_ValCell.Attributes.Add("Class", "rptCellBorder");
                        tc_ValCell.Controls.Add(lit_Valtx);
                        tr_det.Cells.Add(tc_ValCell);

                        TableCell tc_NWCell = new TableCell();
                        tc_NWCell.Width = 200;
                        Literal lit_NWtx = new Literal();
                        lit_NWtx.Text = ro[0]["netweight"].ToString();
                        tc_NWCell.HorizontalAlign = HorizontalAlign.Center;
                        tc_NWCell.BorderStyle = BorderStyle.Solid;
                        tc_NWCell.BorderWidth = 1;
                        tc_NWCell.Attributes.Add("Class", "rptCellBorder");
                        tc_NWCell.Controls.Add(lit_NWtx);
                        tr_det.Cells.Add(tc_NWCell);

                        TableCell tc_det_FF1 = new TableCell();
                        tc_det_FF1.Width = 200;
                        Literal lit_det_FF1 = new Literal();
                        lit_det_FF1.Text =ro[0]["MFG"].ToString();;
                        tc_det_FF1.HorizontalAlign = HorizontalAlign.Center;
                        tc_det_FF1.BorderStyle = BorderStyle.Solid;
                        tc_det_FF1.BorderWidth = 1;
                        tc_det_FF1.Attributes.Add("Class", "rptCellBorder");
                        tc_det_FF1.Controls.Add(lit_det_FF1);
                        tr_det.Cells.Add(tc_det_FF1);

                    }
                 else
                    {
                        TableCell tc_det_FF = new TableCell();
                        tc_det_FF.Width = 200;
                        Literal lit_det_FF = new Literal();
                        lit_det_FF.Text = " ";
                        tc_det_FF.HorizontalAlign = HorizontalAlign.Center;
                        tc_det_FF.BorderStyle = BorderStyle.Solid;
                        tc_det_FF.BorderWidth = 1;
                        tc_det_FF.Attributes.Add("Class", "rptCellBorder");
                        tc_det_FF.Controls.Add(lit_det_FF);
                        tr_det.Cells.Add(tc_det_FF);



                        TableCell tc_ValCell = new TableCell();
                        tc_ValCell.Width = 200;
                        Literal lit_Valtx = new Literal();
                        lit_Valtx.Text = "";
                        tc_ValCell.HorizontalAlign = HorizontalAlign.Center;
                        tc_ValCell.BorderStyle = BorderStyle.Solid;
                        tc_ValCell.BorderWidth = 1;
                        tc_ValCell.Attributes.Add("Class", "rptCellBorder");
                        tc_ValCell.Controls.Add(lit_Valtx);
                        tr_det.Cells.Add(tc_ValCell);

                        TableCell tc_NWCell = new TableCell();
                        tc_NWCell.Width = 200;
                        Literal lit_NWtx = new Literal();
                        lit_NWtx.Text = "";
                        tc_NWCell.HorizontalAlign = HorizontalAlign.Center;
                        tc_NWCell.BorderStyle = BorderStyle.Solid;
                        tc_NWCell.BorderWidth = 1;
                        tc_NWCell.Attributes.Add("Class", "rptCellBorder");
                        tc_NWCell.Controls.Add(lit_NWtx);
                        tr_det.Cells.Add(tc_NWCell);


                        TableCell tc_det_FF1 = new TableCell();
                        tc_det_FF1.Width = 200;
                        Literal lit_det_FF1 = new Literal();
                        lit_det_FF1.Text = "";
                        tc_det_FF1.HorizontalAlign = HorizontalAlign.Center;
                        tc_det_FF1.BorderStyle = BorderStyle.Solid;
                        tc_det_FF1.BorderWidth = 1;
                        tc_det_FF1.Attributes.Add("Class", "rptCellBorder");
                        tc_det_FF1.Controls.Add(lit_det_FF1);
                        tr_det.Cells.Add(tc_det_FF1);

                    }
                }

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
        //lblhead1.Visible = true;
        string strFileName = Page.Title;
        string attachment = "attachment; filename=" + strFileName + ".xls";

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

    protected void btnClose_Click(object sender, EventArgs e)
    {
        // Response.Write("Purchase_Register_Distributor_wise.aspx");

    }
    
}