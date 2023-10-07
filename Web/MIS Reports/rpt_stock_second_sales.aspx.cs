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
using iTextSharp.tool.xml;

public partial class MIS_Reports_rpt_stock_second_sales : System.Web.UI.Page
{
    string divcode = string.Empty;
    string sfcode = string.Empty;
    string distcode = string.Empty;
    string sfname = string.Empty;
    string distname = string.Empty;
    string Fmonth = string.Empty;
    string Fyear = string.Empty;
    DataSet dsgv = new DataSet();
    DataSet ff = new DataSet();
    DataSet ss = new DataSet();
    DataSet dl = new DataSet();


    protected void Page_Load(object sender, EventArgs e)
    {
        divcode = Session["div_code"].ToString();
        distcode = Request.QueryString["Dist_Code"].ToString();
        distname= Request.QueryString["Dist_Name"].ToString();
        sfname= Request.QueryString["SFName"].ToString();
        sfcode = Request.QueryString["SFCode"].ToString();
        Fmonth = Request.QueryString["FMonth"].ToString();
        Fyear = Request.QueryString["FYear"].ToString();
        Fillstock();

        Feild.Text = "FieldForce:" + sfname;
    }

    protected void Fillstock()
    {
        tbl.Rows.Clear();
        Product pro = new Product();
        string brndcode = string.Empty;
        string currbrndcode = string.Empty;

        dsgv = pro.stockProdBrnd(divcode);
        ss = pro.brndprodcnt(divcode);
        ff = pro.stksecsales(divcode, distcode, Fmonth, Fyear);
        dl = pro.prodtar(divcode, sfcode, Fmonth, Fyear);
        if (dsgv.Tables[0].Rows.Count>0)
        {
            TableRow tr_header = new TableRow();
            tr_header.BorderStyle = BorderStyle.Solid;
            tr_header.BorderWidth = 1;
            tr_header.BackColor = System.Drawing.Color.FromName("#0097AC");
            tr_header.Style.Add("Color", "White");
            tr_header.BorderColor = System.Drawing.Color.Black;

            TableCell tc_non = new TableCell();
            tc_non.BorderStyle = BorderStyle.Solid;
            tc_non.BorderWidth = 1;
            tc_non.Width = 50;
            tc_non.RowSpan = 3;
            Literal lit_non = new Literal();
            tc_non.BorderColor = System.Drawing.Color.Black;
            tc_non.Controls.Add(lit_non);
            tc_non.Attributes.Add("Class", "rptCellBorder");
            tr_header.Cells.Add(tc_non);
            tbl.Rows.Add(tr_header);

            foreach (DataRow drff in dsgv.Tables[0].Rows)
            {
                brndcode = drff["Product_Brd_Code"].ToString();
                if (brndcode != currbrndcode)
                {
                    DataRow[] drp = ss.Tables[0].Select("Product_Brd_Code='" + brndcode + "'");
                    
                    TableCell tc_GP = new TableCell();
                    tc_GP.BorderStyle = BorderStyle.Solid;
                    tc_GP.BorderWidth = 1;
                    tc_GP.Width = 50;
                    tc_GP.RowSpan = 1;
                    tc_GP.ColumnSpan = Convert.ToInt16(drp[0]["cnt"]);
                    Literal lit_GP = new Literal();
                    lit_GP.Text = drff["Product_Brd_Name"].ToString();
                    tc_GP.BorderColor = System.Drawing.Color.Black;
                    tc_GP.Controls.Add(lit_GP);
                    tc_GP.Attributes.Add("Class", "rptCellBorder");
                    tr_header.Cells.Add(tc_GP);
                    tbl.Rows.Add(tr_header);

                    currbrndcode = brndcode;
                }

            }

            TableCell tc_net = new TableCell();
            tc_net.BorderStyle = BorderStyle.Solid;
            tc_net.BorderWidth = 1;
            tc_net.Width = 50;
            tc_net.RowSpan = 3;
            Literal lit_net = new Literal();
            lit_net.Text = "Net Weight";
            tc_net.BorderColor = System.Drawing.Color.Black;
            tc_net.Controls.Add(lit_net);
            tc_net.Attributes.Add("Class", "rptCellBorder");
            tr_header.Cells.Add(tc_net);
            tbl.Rows.Add(tr_header);


            TableRow tr_header1 = new TableRow();
            tr_header1.BorderStyle = BorderStyle.Solid;
            tr_header1.BorderWidth = 1;
            tr_header1.BackColor = System.Drawing.Color.FromName("#0097AC");
            tr_header1.Style.Add("Color", "White");
            tr_header1.BorderColor = System.Drawing.Color.Black;
            foreach (DataRow dff in dsgv.Tables[0].Rows)
            {
                TableCell tc_PN = new TableCell();
                tc_PN.BorderStyle = BorderStyle.Solid;
                tc_PN.BorderWidth = 1;
                tc_PN.Width = 50;
                tc_PN.RowSpan = 1;
                Literal lit_PN = new Literal();
                lit_PN.Text = dff["Product_Short_Name"].ToString();
                tc_PN.BorderColor = System.Drawing.Color.Black;
                tc_PN.Controls.Add(lit_PN);
                tc_PN.Attributes.Add("Class", "rptCellBorder");
                tr_header1.Cells.Add(tc_PN);
                tbl.Rows.Add(tr_header1);

            }

            TableRow tr_header2 = new TableRow();
            tr_header2.BorderStyle = BorderStyle.Solid;
            tr_header2.BorderWidth = 1;
            tr_header2.BackColor = System.Drawing.Color.FromName("#0097AC");
            tr_header2.Style.Add("Color", "White");
            tr_header2.BorderColor = System.Drawing.Color.Black;
            foreach (DataRow dff in dsgv.Tables[0].Rows)
            {
                TableCell tc_Netw = new TableCell();
                tc_Netw.BorderStyle = BorderStyle.Solid;
                tc_Netw.BorderWidth = 1;
                tc_Netw.Width = 50;
                tc_Netw.RowSpan = 1;
                Literal lit_Netw = new Literal();
                lit_Netw.Text = dff["product_netwt"].ToString();
                tc_Netw.BorderColor = System.Drawing.Color.Black;
                tc_Netw.Controls.Add(lit_Netw);
                tc_Netw.Attributes.Add("Class", "rptCellBorder");
                tr_header2.Cells.Add(tc_Netw);
                tbl.Rows.Add(tr_header2);
            }


            TableRow tr_row1 = new TableRow();
            TableCell tc_tar = new TableCell();
            Literal lit_tar = new Literal();
            lit_tar.Text = "Target";
            tc_tar.BorderStyle = BorderStyle.Solid;
            tc_tar.BorderWidth = 1;
            tc_tar.Attributes.Add("Class", "rptCellBorder");
            tc_tar.Controls.Add(lit_tar);
            tr_row1.Cells.Add(tc_tar);
            tr_row1.BackColor = System.Drawing.Color.White;
            tbl.Rows.Add(tr_row1);

            foreach (DataRow dff in dsgv.Tables[0].Rows)
            {
                DataRow[] dr = dl.Tables[0].Select("PRODUCT_CODE='" + dff["Product_Detail_Code"].ToString() + "'");
                TableCell tc_tctar = new TableCell();
                Literal lit_tctar = new Literal();
                lit_tctar.Text = (dr.Length > 0) ? dr[0]["TARGET"].ToString() : "";
                tc_tctar.BorderStyle = BorderStyle.Solid;
                tc_tctar.BorderWidth = 1;
                tc_tctar.Attributes.Add("Class", "rptCellBorder");
                tc_tctar.Controls.Add(lit_tctar);
                tr_row1.Cells.Add(tc_tctar);
                tr_row1.BackColor = System.Drawing.Color.White;
                tbl.Rows.Add(tr_row1);
            }

            TableCell tc_tarn = new TableCell();
            Literal lit_tarn = new Literal();
            tc_tarn.BorderStyle = BorderStyle.Solid;
            tc_tarn.BorderWidth = 1;
            tc_tarn.Attributes.Add("Class", "rptCellBorder");
            tc_tarn.Controls.Add(lit_tarn);
            tr_row1.Cells.Add(tc_tarn);
            tr_row1.BackColor = System.Drawing.Color.White;
            tbl.Rows.Add(tr_row1);

            TableRow tr_row2 = new TableRow();
            TableCell tc_stk = new TableCell();
            Literal lit_stk = new Literal();
            lit_stk.Text = "Opening Stock";
            tc_stk.BorderStyle = BorderStyle.Solid;
            tc_stk.BorderWidth = 1;
            tc_stk.Attributes.Add("Class", "rptCellBorder");
            tc_stk.Controls.Add(lit_stk);
            tr_row2.Cells.Add(tc_stk);
            tr_row2.BackColor = System.Drawing.Color.White;
            tbl.Rows.Add(tr_row2);

            decimal totop = 0;
            int i = 0;
            foreach (DataRow dff in dsgv.Tables[0].Rows)
            {
                DataRow[] dr = ff.Tables[0].Select("Product_Code='" + dff["Product_Detail_Code"].ToString() + "'");
                TableCell tc_opstk = new TableCell();
                Literal lit_opstk = new Literal();
                lit_opstk.Text = (dr.Length > 0)?dr[0]["OP"].ToString():"";
                if(i<ff.Tables[0].Rows.Count)
                {
                    totop += Convert.ToDecimal(ff.Tables[0].Rows[i].ItemArray.GetValue(5).ToString());
                    i++;
                }
                tc_opstk.BorderStyle = BorderStyle.Solid;
                tc_opstk.BorderWidth = 1;
                tc_opstk.Attributes.Add("Class", "rptCellBorder");
                tc_opstk.Controls.Add(lit_opstk);
                tr_row2.Cells.Add(tc_opstk);
                tr_row2.BackColor = System.Drawing.Color.White;
                tbl.Rows.Add(tr_row2);
            }

            TableCell tc_opnet = new TableCell();
            Literal lit_opnet = new Literal();
            lit_opnet.Text = totop.ToString();
            tc_opnet.BorderStyle = BorderStyle.Solid;
            tc_opnet.BorderWidth = 1;
            tc_opnet.Attributes.Add("Class", "rptCellBorder");
            tc_opnet.Controls.Add(lit_opnet);
            tr_row2.Cells.Add(tc_opnet);
            tr_row2.BackColor = System.Drawing.Color.White;
            tbl.Rows.Add(tr_row2);


            TableRow tr_row3 = new TableRow();
            TableCell tc_pri = new TableCell();
            Literal lit_pri = new Literal();
            lit_pri.Text = "Primary";
            tc_pri.BorderStyle = BorderStyle.Solid;
            tc_pri.BorderWidth = 1;
            tc_pri.Attributes.Add("Class", "rptCellBorder");
            tc_pri.Controls.Add(lit_pri);
            tr_row3.Cells.Add(tc_pri);
            tr_row3.BackColor = System.Drawing.Color.White;
            tbl.Rows.Add(tr_row3);

            decimal totpri = 0;
            int j = 0;
            foreach (DataRow dff in dsgv.Tables[0].Rows)
            {
                DataRow[] dr = ff.Tables[0].Select("Product_Code='" + dff["Product_Detail_Code"].ToString() + "'");
                TableCell tc_rec = new TableCell();
                Literal lit_rec = new Literal();
                lit_rec.Text = (dr.Length > 0) ? dr[0]["Rec"].ToString() : "";
                if (j < ff.Tables[0].Rows.Count)
                {
                    totpri += Convert.ToDecimal(ff.Tables[0].Rows[j].ItemArray.GetValue(6).ToString());
                    j++;
                }
                tc_rec.BorderStyle = BorderStyle.Solid;
                tc_rec.BorderWidth = 1;
                tc_rec.Attributes.Add("Class", "rptCellBorder");
                tc_rec.Controls.Add(lit_rec);
                tr_row3.Cells.Add(tc_rec);
                tr_row3.BackColor = System.Drawing.Color.White;
                tbl.Rows.Add(tr_row3);
            }

            TableCell tc_totpri = new TableCell();
            Literal lit_totpri = new Literal();
            lit_totpri.Text = totpri.ToString();
            tc_totpri.BorderStyle = BorderStyle.Solid;
            tc_totpri.BorderWidth = 1;
            tc_totpri.Attributes.Add("Class", "rptCellBorder");
            tc_totpri.Controls.Add(lit_totpri);
            tr_row3.Cells.Add(tc_totpri);
            tr_row3.BackColor = System.Drawing.Color.White;
            tbl.Rows.Add(tr_row3);

            TableRow tr_row4 = new TableRow();
            TableCell tc_tot = new TableCell();
            Literal lit_tot = new Literal();
            lit_tot.Text = "Total";
            tc_tot.BorderStyle = BorderStyle.Solid;
            tc_tot.BorderWidth = 1;
            tc_tot.Attributes.Add("Class", "rptCellBorder");
            tc_tot.Controls.Add(lit_tot);
            tr_row4.Cells.Add(tc_tot);
            tr_row4.BackColor = System.Drawing.Color.White;
            tbl.Rows.Add(tr_row4);

            decimal tot1 = 0;
            int k = 0;
            foreach (DataRow dff in dsgv.Tables[0].Rows)
            {
                DataRow[] dr = ff.Tables[0].Select("Product_Code='" + dff["Product_Detail_Code"].ToString() + "'");
                TableCell tc_tpri = new TableCell();
                Literal lit_tpri = new Literal();
                lit_tpri.Text = (dr.Length > 0) ? dr[0]["TPri"].ToString() : "";
                if (k < ff.Tables[0].Rows.Count)
                {
                    tot1 += Convert.ToDecimal(ff.Tables[0].Rows[k].ItemArray.GetValue(7).ToString());
                    k++;
                }
                tc_tpri.BorderStyle = BorderStyle.Solid;
                tc_tpri.BorderWidth = 1;
                tc_tpri.Attributes.Add("Class", "rptCellBorder");
                tc_tpri.Controls.Add(lit_tpri);
                tr_row4.Cells.Add(tc_tpri);
                tr_row4.BackColor = System.Drawing.Color.White;
                tbl.Rows.Add(tr_row4);
            }

            TableCell tc_tot1 = new TableCell();
            Literal lit_tot1 = new Literal();
            lit_tot1.Text = tot1.ToString();
            tc_tot1.BorderStyle = BorderStyle.Solid;
            tc_tot1.BorderWidth = 1;
            tc_tot1.Attributes.Add("Class", "rptCellBorder");
            tc_tot1.Controls.Add(lit_tot1);
            tr_row4.Cells.Add(tc_tot1);
            tr_row4.BackColor = System.Drawing.Color.White;
            tbl.Rows.Add(tr_row4);

            TableRow tr_row5 = new TableRow();
            TableCell tc_secsale = new TableCell();
            Literal lit_secsale = new Literal();
            lit_secsale.Text = "Sec.Sales";
            tc_secsale.BorderStyle = BorderStyle.Solid;
            tc_secsale.BorderWidth = 1;
            tc_secsale.Attributes.Add("Class", "rptCellBorder");
            tc_secsale.Controls.Add(lit_secsale);
            tr_row5.Cells.Add(tc_secsale);
            tr_row5.BackColor = System.Drawing.Color.White;
            tbl.Rows.Add(tr_row5);

            decimal totsec = 0;
            int a = 0;
            foreach (DataRow dff in dsgv.Tables[0].Rows)
            {
                DataRow[] dr = ff.Tables[0].Select("Product_Code='" + dff["Product_Detail_Code"].ToString() + "'");
                TableCell tc_sal = new TableCell();
                Literal lit_sal = new Literal();
                lit_sal.Text = (dr.Length > 0) ? dr[0]["Sal"].ToString() : "";
                if (a < ff.Tables[0].Rows.Count)
                {
                    totsec += Convert.ToDecimal(ff.Tables[0].Rows[a].ItemArray.GetValue(8).ToString());
                    a++;
                }
                tc_sal.BorderStyle = BorderStyle.Solid;
                tc_sal.BorderWidth = 1;
                tc_sal.Attributes.Add("Class", "rptCellBorder");
                tc_sal.Controls.Add(lit_sal);
                tr_row5.Cells.Add(tc_sal);
                tr_row5.BackColor = System.Drawing.Color.White;
                tbl.Rows.Add(tr_row5);
            }
            TableCell tc_totsec = new TableCell();
            Literal lit_totsec = new Literal();
            lit_totsec.Text = totsec.ToString();
            tc_totsec.BorderStyle = BorderStyle.Solid;
            tc_totsec.BorderWidth = 1;
            tc_totsec.Attributes.Add("Class", "rptCellBorder");
            tc_totsec.Controls.Add(lit_totsec);
            tr_row5.Cells.Add(tc_totsec);
            tr_row5.BackColor = System.Drawing.Color.White;
            tbl.Rows.Add(tr_row5);

            TableRow tr_row6 = new TableRow();
            TableCell tc_clstk = new TableCell();
            Literal lit_clstk = new Literal();
            lit_clstk.Text = "Closing Stock";
            tc_clstk.BorderStyle = BorderStyle.Solid;
            tc_clstk.BorderWidth = 1;
            tc_clstk.Attributes.Add("Class", "rptCellBorder");
            tc_clstk.Controls.Add(lit_clstk);
            tr_row6.Cells.Add(tc_clstk);
            tr_row6.BackColor = System.Drawing.Color.White;
            tbl.Rows.Add(tr_row6);

            decimal totcl = 0;
            int b = 0;
            foreach (DataRow dff in dsgv.Tables[0].Rows)
            {
                DataRow[] dr = ff.Tables[0].Select("Product_Code='" + dff["Product_Detail_Code"].ToString() + "'");
                TableCell tc_cl = new TableCell();
                Literal lit_cl = new Literal();
                lit_cl.Text = (dr.Length > 0) ? dr[0]["ClStk"].ToString() : "";
                if (b < ff.Tables[0].Rows.Count)
                {
                    totcl += Convert.ToDecimal(ff.Tables[0].Rows[b].ItemArray.GetValue(9).ToString());
                    b++;
                }
                tc_cl.BorderStyle = BorderStyle.Solid;
                tc_cl.BorderWidth = 1;
                tc_cl.Attributes.Add("Class", "rptCellBorder");
                tc_cl.Controls.Add(lit_cl);
                tr_row6.Cells.Add(tc_cl);
                tr_row6.BackColor = System.Drawing.Color.White;
                tbl.Rows.Add(tr_row6);
            }

            TableCell tc_totcl = new TableCell();
            Literal lit_totcl = new Literal();
            lit_totcl.Text = totcl.ToString();
            tc_totcl.BorderStyle = BorderStyle.Solid;
            tc_totcl.BorderWidth = 1;
            tc_totcl.Attributes.Add("Class", "rptCellBorder");
            tc_totcl.Controls.Add(lit_totcl);
            tr_row6.Cells.Add(tc_totcl);
            tr_row6.BackColor = System.Drawing.Color.White;
            tbl.Rows.Add(tr_row6);

            TableRow tr_row7 = new TableRow();
            TableCell tc_totlitr = new TableCell();
            Literal lit_totlitr = new Literal();
            lit_totlitr.Text = "Total Litres";
            tc_totlitr.BorderStyle = BorderStyle.Solid;
            tc_totlitr.BorderWidth = 1;
            tc_totlitr.Attributes.Add("Class", "rptCellBorder");
            tc_totlitr.Controls.Add(lit_totlitr);
            tr_row7.Cells.Add(tc_totlitr);
            tr_row7.BackColor = System.Drawing.Color.White;
            tbl.Rows.Add(tr_row7);

            foreach (DataRow dff in dsgv.Tables[0].Rows)
            {
                DataRow[] dr = ff.Tables[0].Select("Product_Code='" + dff["Product_Detail_Code"].ToString() + "'");
                TableCell tc_clk = new TableCell();
                Literal lit_clk = new Literal();
                lit_clk.Text = (dr.Length > 0) ? dr[0]["Cl"].ToString() : "";
                tc_clk.BorderStyle = BorderStyle.Solid;
                tc_clk.BorderWidth = 1;
                tc_clk.Attributes.Add("Class", "rptCellBorder");
                tc_clk.Controls.Add(lit_clk);
                tr_row7.Cells.Add(tc_clk);
                tr_row7.BackColor = System.Drawing.Color.White;
                tbl.Rows.Add(tr_row7);
            }
            
            TableCell tc_totn = new TableCell();
            Literal lit_mfg = new Literal();
            tc_totn.BorderStyle = BorderStyle.Solid;
            tc_totn.BorderWidth = 1;
            tc_totn.Attributes.Add("Class", "rptCellBorder");
            tc_totn.Controls.Add(lit_mfg);
            tr_row7.Cells.Add(tc_totn);
            tr_row7.BackColor = System.Drawing.Color.White;
            tbl.Rows.Add(tr_row7);

            //foreach (DataRow dff in dsgv.Tables[0].Rows)
            //{
            //    DataRow[] dr = ff.Tables[0].Select("Product_Code='" + dff["Product_Detail_Code"].ToString() + "'");
            //    TableCell tc_sal = new TableCell();
            //    Literal lit_sal = new Literal();
            //    lit_sal.Text = (dr.Length > 0) ? dr[0]["Sal"].ToString() : "";
            //    tc_sal.BorderStyle = BorderStyle.Solid;
            //    tc_sal.BorderWidth = 1;
            //    tc_sal.Attributes.Add("Class", "rptCellBorder");
            //    tc_sal.Controls.Add(lit_sal);
            //    tr_row8.Cells.Add(tc_sal);
            //    tr_row8.BackColor = System.Drawing.Color.White;
            //    tbl.Rows.Add(tr_row8);
            //}
        }
    }

    protected void btnExcel_Click(object sender, EventArgs e)
    {
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
}