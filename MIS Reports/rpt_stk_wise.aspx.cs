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

public partial class MIS_Reports_rpt_stk_wise : System.Web.UI.Page
{
    #region
    public string SfR = string.Empty;
    public string sf_code = string.Empty;
    public string sfname = string.Empty;
    public string div_code = string.Empty;
    //int div_co = 0;
    //int years = 0;
    public string Year = string.Empty;
    public string Mnth = string.Empty;
    public string SF_Name = string.Empty;
    DataSet dsSalesForce = new DataSet();
    DataSet dsStk = new DataSet();
    DataSet dsState = new DataSet();
    Decimal iTotLstCount = 0;
    Decimal iTotLstCountt=0;
    //public string Stockist_Code = string.Empty;
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            div_code = Session["div_code"].ToString();
            sf_code = Request.QueryString["SF_Code"].ToString();
            if (Request.QueryString["SfR"] != null)
                SfR = Request.QueryString["SfR"].ToString();
            else
                SfR = sf_code;// Session["SF_code"].ToString();
            Year = Request.QueryString["Year"].ToString();
            Mnth = Request.QueryString["Mnth"].ToString();
            FillSF(sf_code, div_code, Mnth, Year);
        }
        if (IsPostBack)
        {
            div_code = Session["div_code"].ToString();
            sf_code = ddlff.SelectedValue.ToString();
            SfR = ddlff.SelectedValue.ToString();
            Mnth = ddlmnth.SelectedValue.ToString();
            Year = ddlYR.SelectedValue.ToString();
        }
        Fillddl();
        BindDate();
    }

    private void BindDate()
    {
        TourPlan tp = new TourPlan();
        DataSet dsTP = new DataSet();
        dsTP = tp.Get_TP_Edit_Year(div_code);
        if (dsTP.Tables[0].Rows.Count > 0)
        {
            for (int k = Convert.ToInt16(dsTP.Tables[0].Rows[0]["Year"]); k <= DateTime.Now.Year + 1; k++)
            {
                ddlYR.Items.Add(k.ToString());
            }
            ddlYR.Text = DateTime.Now.Year.ToString();
            //ddlMonth.SelectedValue = DateTime.Now.Month.ToString();
        }
    }

    private void Fillddl()
    {
        SalesForce sf = new SalesForce();
        dsState = sf.SalesForceList(div_code, "admin");
        ddlff.DataTextField = "Sf_Name";
        ddlff.DataValueField = "Sf_Code";
        ddlff.DataSource = dsState;
        ddlff.DataBind();
        ddlff.SelectedValue = sf_code;
    }
    private void FillSF(string sfc,string divc,string Month,string Yr)
    {
       decimal r ; decimal p; decimal tqc = 0; decimal tqp = 0; decimal tqcc = 0;decimal tqpp = 0;
        int j, k, q;
        string sprd = "";
        tbl.Rows.Clear();
        Order sf = new Order();
        dsSalesForce = sf.get_product_details(sfc, divc, Month, Yr);
        dsStk = sf.get_Stockist(sfc, divc, Month, Yr);

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
            tc_SNodd.ColumnSpan = 3;
            Literal lit_SNodd = new Literal();
            lit_SNodd.Text = drFF["Last_Updation_Date"].ToString();
            tc_SNodd.Controls.Add(lit_SNodd);
            tc_SNodd.Attributes.Add("Class", "rptCellBorder");
            tr_headerr.Cells.Add(tc_SNodd);
        }
        TableRow tr_headerr2 = new TableRow();
        TableRow tr_headerr1 = new TableRow();
        TableCell tc_SNo = new TableCell();
        tc_SNo.BorderStyle = BorderStyle.Solid;
        tc_SNo.BorderWidth = 1;
        tc_SNo.Width = 50;
        tc_SNo.RowSpan = 2;
        Literal lit_SNo = new Literal();
        lit_SNo.Text = "Product Name";
        tc_SNo.BorderColor = System.Drawing.Color.Black;
        tc_SNo.Controls.Add(lit_SNo);
        tc_SNo.Attributes.Add("Class", "rptCellBorder");
        tr_headerr1.Cells.Add(tc_SNo);

  TableCell tc_total = new TableCell();
        tc_total.BorderStyle = BorderStyle.Solid;
        tc_total.BorderWidth = 1;
        tc_total.Width = 50;
        tc_total.RowSpan = 2;
        tc_total.ColumnSpan = 3;
        Literal lit_total = new Literal();
        lit_total.Text = "Total";
        tc_total.BorderColor = System.Drawing.Color.Black;
        tc_total.Controls.Add(lit_total);
        tc_total.Attributes.Add("style", "color:red;Font-weight:bold;");
        tc_total.Style.Add("text-align", "center");
        tc_total.Style.Add("font-family", "Calibri");
        tc_total.Attributes.Add("Class", "rptCellBorder");
        tc_total.Style.Add("font-size", "10pt");
        tr_headerr.Cells.Add(tc_total);


        foreach (DataRow drStk in dsStk.Tables[0].Rows)
        {
            TableCell tc_DR_Code = new TableCell();
            tc_DR_Code.BorderStyle = BorderStyle.Solid;
            tc_DR_Code.BorderWidth = 1;
            tc_DR_Code.Width = 400;
            tc_DR_Code.RowSpan = 1;
            tc_DR_Code.ColumnSpan = 3;
            HyperLink lit_DR_Code = new HyperLink();
            lit_DR_Code.Text = drStk["Stockist_Name"].ToString();
            lit_DR_Code.NavigateUrl = "rpt_stkmfg_wise.aspx?SF_Code=" + sf_code + "&Year=" + Year + "&stkcd=" + drStk["Stockist_code"].ToString() + "&stkname=" + drStk["Stockist_Name"].ToString() + "";
            tc_DR_Code.Controls.Add(lit_DR_Code);
            tc_DR_Code.Attributes.Add("Class", "rptCellBorder");
            tc_DR_Code.BorderColor = System.Drawing.Color.Black;
            tr_headerr1.Cells.Add(tc_DR_Code);

        
            TableCell tc_SNodd1 = new TableCell();
            tc_SNodd1.HorizontalAlign = HorizontalAlign.Left;
            tc_SNodd1.Width = 50;
            tc_SNodd1.RowSpan = 1;
            tc_SNodd1.ColumnSpan = 1;
            Literal lit_SNodd1 = new Literal();
            lit_SNodd1.Text = "Quantity";
            tc_SNodd1.Controls.Add(lit_SNodd1);
            tc_SNodd1.Attributes.Add("Class", "rptCellBorder");
            tr_headerr2.Cells.Add(tc_SNodd1);

            TableCell tc_SNoddd = new TableCell();
            tc_SNoddd.HorizontalAlign = HorizontalAlign.Left;
            tc_SNoddd.Width = 50;
            tc_SNoddd.RowSpan = 1;
            tc_SNoddd.ColumnSpan = 1;
            Literal lit_SNoddd = new Literal();
            lit_SNoddd.Text = "Value";
            tc_SNoddd.Controls.Add(lit_SNoddd);
            tc_SNoddd.Attributes.Add("Class", "rptCellBorder");
            tr_headerr2.Cells.Add(tc_SNoddd);

            TableCell tc_SNodd2 = new TableCell();
            tc_SNodd2.HorizontalAlign = HorizontalAlign.Left;
            tc_SNodd2.Width = 50;
            tc_SNodd2.RowSpan = 1;
            tc_SNodd2.ColumnSpan = 1;
            Literal lit_SNodd2 = new Literal();
            lit_SNodd2.Text = "Net Weight";
            tc_SNodd2.Controls.Add(lit_SNodd2);
            tc_SNodd2.Attributes.Add("Class", "rptCellBorder");
            tr_headerr2.Cells.Add(tc_SNodd2);

        }

  TableCell tc_SNodd12 = new TableCell();
        tc_SNodd12.HorizontalAlign = HorizontalAlign.Left;
        tc_SNodd12.Width = 50;
        tc_SNodd12.RowSpan = 1;
        tc_SNodd12.ColumnSpan = 1;
        Literal lit_SNodd12 = new Literal();
        lit_SNodd12.Text = "Quantity";
        tc_SNodd12.Controls.Add(lit_SNodd12);
        tc_SNodd12.Attributes.Add("Class", "rptCellBorder");
        tr_headerr2.Cells.Add(tc_SNodd12);

        TableCell tc_SNoddd11 = new TableCell();
        tc_SNoddd11.HorizontalAlign = HorizontalAlign.Left;
        tc_SNoddd11.Width = 50;
        tc_SNoddd11.RowSpan = 1;
        tc_SNoddd11.ColumnSpan = 1;
        Literal lit_SNoddd11 = new Literal();
        lit_SNoddd11.Text = "Value";
        tc_SNoddd11.Controls.Add(lit_SNoddd11);
        tc_SNoddd11.Attributes.Add("Class", "rptCellBorder");
        tr_headerr2.Cells.Add(tc_SNoddd11);

        TableCell tc_SNodd21 = new TableCell();
        tc_SNodd21.HorizontalAlign = HorizontalAlign.Left;
        tc_SNodd21.Width = 50;
        tc_SNodd21.RowSpan = 1;
        tc_SNodd21.ColumnSpan = 1;
        Literal lit_SNodd21 = new Literal();
        lit_SNodd21.Text = "Net Weight";
        tc_SNodd21.Controls.Add(lit_SNodd21);
        tc_SNodd21.Attributes.Add("Class", "rptCellBorder");
        tr_headerr2.Cells.Add(tc_SNodd21);

        tbl.Rows.Add(tr_headerr);
        tbl.Rows.Add(tr_headerr1);
        tbl.Rows.Add(tr_headerr2);

        TableRow tr_total = new TableRow();

        TableCell tc_Count_Total = new TableCell();
        tc_Count_Total.BorderStyle = BorderStyle.Solid;
        tc_Count_Total.BorderWidth = 1;

        Literal lit_Count_Total = new Literal();
        lit_Count_Total.Text = "<center>Total</center>";
        tc_Count_Total.Attributes.Add("style", "color:Red;font-weight:bold;");
        tc_Count_Total.Controls.Add(lit_Count_Total);
        tc_Count_Total.Font.Bold.ToString();
        tc_Count_Total.BackColor = System.Drawing.Color.White;
        tc_Count_Total.Attributes.Add("Class", "tbldetail_main");
        tc_Count_Total.ColumnSpan = 1;
        tc_Count_Total.Style.Add("text-align", "left");
        tc_Count_Total.Style.Add("font-family", "Calibri");
        tc_Count_Total.Attributes.Add("Class", "rptCellBorder");
        tc_Count_Total.Style.Add("font-size", "10pt");

        tr_total.Cells.Add(tc_Count_Total);
        decimal[] tarr=new decimal[dsStk.Tables[0].Rows.Count];
        decimal[] tarr1 = new decimal[dsStk.Tables[0].Rows.Count];
 TableRow tr_det = null;
        int CnvQty = 1;
        decimal[] nums=new decimal[2];
        decimal tv = 0; decimal tnw = 0;p = 0;r = 0;
        foreach (DataRow drFF in dsSalesForce.Tables[0].Rows)
        {
             tr_det = new TableRow();
            TableRow tr_det1 = new TableRow();

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
               j = 0; k = 0; string tp ; q = 0; 
                foreach (DataRow drStk in dsStk.Tables[0].Rows)
                {
                    DataRow[] ro = dsSalesForce.Tables[0].Select("product_code='" + drFF["Product_code"].ToString() + "' and stockist_code='" + drStk["Stockist_code"].ToString() + "'");
                    if (ro.Length > 0)
                    {
                          TableCell tc_det_FF = new TableCell();
                        tc_det_FF.Width = 200;
                        Literal lit_det_FF = new Literal();
                        lit_det_FF.Text = ro[0]["qty"].ToString();
                        tp = Convert.ToString(ro[0]["qty"].ToString());

                        CnvQty = Convert.ToInt16(ro[0]["Conversion_Qty"].ToString());
                        nums = tp.Split(',').Select(decimal.Parse).ToArray();
                        if (nums.Length>0)
                        {
                            tqc += nums[0];
                            tqp += nums[1];
                            tqcc +=nums[0];
                            tqpp += nums[1];
                        }                       

                        tc_det_FF.HorizontalAlign = HorizontalAlign.Center;
                        tc_det_FF.BorderStyle = BorderStyle.Solid;
                        tc_det_FF.BorderWidth = 1;
                        tc_det_FF.Attributes.Add("Class", "rptCellBorder");
                        tc_det_FF.Controls.Add(lit_det_FF);
                        tr_det.Cells.Add(tc_det_FF);


                        TableCell tc_det_FFF = new TableCell();
                        tc_det_FFF.Width = 200;
                        Literal lit_det_FFF = new Literal();
                        lit_det_FFF.Text = ro[0]["StateRate"].ToString();
                        tarr1[k] += Convert.ToDecimal(ro[0]["StateRate"].ToString());
  tv += Decimal.Parse(ro[0]["StateRate"].ToString()); 
                        p+= Decimal.Parse(ro[0]["StateRate"].ToString());
                        tc_det_FFF.HorizontalAlign = HorizontalAlign.Center;
                        tc_det_FFF.BorderStyle = BorderStyle.Solid;
                        tc_det_FFF.BorderWidth = 1;
                        tc_det_FFF.Attributes.Add("Class", "rptCellBorder");
                        tc_det_FFF.Controls.Add(lit_det_FFF);
                        tr_det.Cells.Add(tc_det_FFF);


                        TableCell tc_det_FF1 = new TableCell();
                        tc_det_FF1.Width = 200;
                        Literal lit_det_FF1 = new Literal();
                        lit_det_FF1.Text = ro[0]["netweight"].ToString();
                        tarr[j] += Convert.ToDecimal(ro[0]["netweight"].ToString());
r+= Convert.ToDecimal(ro[0]["netweight"].ToString());
                        tnw += Decimal.Parse(ro[0]["netweight"].ToString());
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
                       

                        TableCell tc_det_FFF = new TableCell();
                        tc_det_FFF.Width = 200;
                        Literal lit_det_FFF = new Literal();
                        lit_det_FFF.Text = " ";
                        tc_det_FFF.HorizontalAlign = HorizontalAlign.Center;
                        tc_det_FFF.BorderStyle = BorderStyle.Solid;
                        tc_det_FFF.BorderWidth = 1;
                        tc_det_FFF.Attributes.Add("Class", "rptCellBorder");
                        tc_det_FFF.Controls.Add(lit_det_FFF);
                        tr_det.Cells.Add(tc_det_FFF);
                       tarr1[k] += 0;


                        TableCell tc_det_FF1 = new TableCell();
                        tc_det_FF1.Width = 200;
                        Literal lit_det_FF1 = new Literal();
                        lit_det_FF1.Text = " ";
                        tc_det_FF1.HorizontalAlign = HorizontalAlign.Center;
                        tc_det_FF1.BorderStyle = BorderStyle.Solid;
                        tc_det_FF1.BorderWidth = 1;
                        tc_det_FF1.Attributes.Add("Class", "rptCellBorder");
                        tc_det_FF1.Controls.Add(lit_det_FF1);
                        tr_det.Cells.Add(tc_det_FF1);
                        tarr[j] += 0;
                    }
                    j++;
                    k++;
                  /* */

                
                }
TableCell tc_tot_month = new TableCell();
                    Literal lit_qnty = new Literal();
                int pCQ =(int)(tqp / CnvQty);
                int num=Convert.ToInt32(tqc + pCQ);
               int numc = Convert.ToInt32(tqp-(pCQ* CnvQty));
                //num = Convert.ToString(string.Format("{0}{1}", tqc[0], nums[1]));
                //lit_qnty.Text = Convert.ToString(nums[0] + ',' + nums[1]);
                //lit_qnty.Text = Convert.ToString(tqc + ":" + tqp + "*" + CnvQty + "=" + num + "," + numc);
                lit_qnty.Text = Convert.ToString(num + ","+ numc);
                tqc = 0;
                tqp = 0;
                    //iTotLstCount += Decimal.Parse(drFF["qty"].ToString());
                    tc_tot_month.BorderStyle = BorderStyle.Solid;
                    tc_tot_month.BorderWidth = 1;
                    tc_tot_month.BackColor = System.Drawing.Color.White;
                    tc_tot_month.Width = 200;
                    tc_tot_month.Style.Add("font-family", "Calibri");
                    tc_tot_month.Style.Add("font-size", "10pt");
                    tc_tot_month.HorizontalAlign = HorizontalAlign.Center;
                    tc_tot_month.VerticalAlign = VerticalAlign.Middle;
                    tc_tot_month.Controls.Add(lit_qnty);
                    tc_tot_month.Attributes.Add("style", "font-weight:bold;");
                    tc_tot_month.Attributes.Add("Class", "rptCellBorder");
                    tr_det.Cells.Add(tc_tot_month);

                    TableCell tc_tot_month1 = new TableCell();
                    Literal lit_qnty1 = new Literal();
                lit_qnty1.Text = Convert.ToString(tv.ToString());
                tv = 0;
                    //iTotLstCount += Decimal.Parse(drFF["qty"].ToString());
                    tc_tot_month1.BorderStyle = BorderStyle.Solid;
                    tc_tot_month1.BorderWidth = 1;
                    tc_tot_month1.BackColor = System.Drawing.Color.White;
                    tc_tot_month1.Width = 200;
                    tc_tot_month1.Style.Add("font-family", "Calibri");
                    tc_tot_month1.Style.Add("font-size", "10pt");
                    tc_tot_month1.HorizontalAlign = HorizontalAlign.Center;
                    tc_tot_month1.VerticalAlign = VerticalAlign.Middle;
                    tc_tot_month1.Controls.Add(lit_qnty1);
                    tc_tot_month1.Attributes.Add("style", "font-weight:bold;");
                    tc_tot_month1.Attributes.Add("Class", "rptCellBorder");
                    tr_det.Cells.Add(tc_tot_month1);

                    TableCell tc_tot_montht = new TableCell();
                    Literal lit_montht = new Literal();
                    //int iTotLstCount = 0;
                    lit_montht.Text = Convert.ToString(tnw.ToString());
                //iTotLstCountt += Decimal.Parse(drFF["netweight"].ToString());
                tnw = 0;
                    tc_tot_montht.BorderStyle = BorderStyle.Solid;
                    tc_tot_montht.BorderWidth = 1;
                    tc_tot_montht.BackColor = System.Drawing.Color.White;
                    tc_tot_montht.Width = 200;
                    tc_tot_montht.Style.Add("font-family", "Calibri");
                    tc_tot_montht.Style.Add("font-size", "10pt");
                    tc_tot_montht.HorizontalAlign = HorizontalAlign.Center;
                    tc_tot_montht.VerticalAlign = VerticalAlign.Middle;
                    tc_tot_montht.Controls.Add(lit_montht);
                    tc_tot_montht.Attributes.Add("style", "font-weight:bold;");
                    tc_tot_montht.Attributes.Add("Class", "rptCellBorder");
                    tr_det.Cells.Add(tc_tot_montht);
                    tbl.Rows.Add(tr_det);
           
                   
            p++;
            r++;
                tbl.Rows.Add(tr_det);
               	
            }

        }
        j = 0;k = 0;
        foreach (DataRow drStk in dsStk.Tables[0].Rows)
        {
            TableCell tc_tot_month = new TableCell();
            Literal lit_qnty = new Literal();

            lit_qnty.Text = "";

            //iTotLstCount += Decimal.Parse(drFF["qty"].ToString());
            tc_tot_month.BorderStyle = BorderStyle.Solid;
            tc_tot_month.BorderWidth = 1;
            tc_tot_month.BackColor = System.Drawing.Color.White;
            tc_tot_month.Width = 200;
            tc_tot_month.Style.Add("font-family", "Calibri");
            tc_tot_month.Style.Add("font-size", "10pt");
            tc_tot_month.HorizontalAlign = HorizontalAlign.Center;
            tc_tot_month.VerticalAlign = VerticalAlign.Middle;
            tc_tot_month.Controls.Add(lit_qnty);
            tc_tot_month.Attributes.Add("style", "font-weight:bold;");
            tc_tot_month.Attributes.Add("Class", "rptCellBorder");
            tr_total.Cells.Add(tc_tot_month);

            TableCell tc_tot_month1 = new TableCell();
            Literal lit_qnty1 = new Literal();
            lit_qnty1.Text = tarr1[k].ToString();
            //iTotLstCount += Decimal.Parse(drFF["qty"].ToString());
            tc_tot_month1.BorderStyle = BorderStyle.Solid;
            tc_tot_month1.BorderWidth = 1;
            tc_tot_month1.BackColor = System.Drawing.Color.White;
            tc_tot_month1.Width = 200;
            tc_tot_month1.Style.Add("font-family", "Calibri");
            tc_tot_month1.Style.Add("font-size", "10pt");
            tc_tot_month1.HorizontalAlign = HorizontalAlign.Center;
            tc_tot_month1.VerticalAlign = VerticalAlign.Middle;
            tc_tot_month1.Controls.Add(lit_qnty1);
            tc_tot_month1.Attributes.Add("style", "font-weight:bold;");
            tc_tot_month1.Attributes.Add("Class", "rptCellBorder");
            tr_total.Cells.Add(tc_tot_month1);




            TableCell tc_tot_montht = new TableCell();
            Literal lit_montht = new Literal();
            //int iTotLstCount = 0;
            lit_montht.Text = tarr[j].ToString();
            //iTotLstCountt += Decimal.Parse(drFF["netweight"].ToString());

            tc_tot_montht.BorderStyle = BorderStyle.Solid;
            tc_tot_montht.BorderWidth = 1;
            tc_tot_montht.BackColor = System.Drawing.Color.White;
            tc_tot_montht.Width = 200;
            tc_tot_montht.Style.Add("font-family", "Calibri");
            tc_tot_montht.Style.Add("font-size", "10pt");
            tc_tot_montht.HorizontalAlign = HorizontalAlign.Center;
            tc_tot_montht.VerticalAlign = VerticalAlign.Middle;
            tc_tot_montht.Controls.Add(lit_montht);
            tc_tot_montht.Attributes.Add("style", "font-weight:bold;");
            tc_tot_montht.Attributes.Add("Class", "rptCellBorder");
            tr_total.Cells.Add(tc_tot_montht);

             tbl.Rows.Add(tr_total);
            j++;
            k++;
        }
 TableCell tc_to_month = new TableCell();
        Literal lit_qnty11 = new Literal();
        tqcc += nums[0];
        tqpp += nums[1];
        lit_qnty11.Text =Convert.ToString(tqcc+","+ tqpp) ;

        //iTotLstCount += Decimal.Parse(drFF["qty"].ToString());
        tc_to_month.BorderStyle = BorderStyle.Solid;
        tc_to_month.BorderWidth = 1;
        tc_to_month.BackColor = System.Drawing.Color.White;
        tc_to_month.Width = 200;
        tc_to_month.Style.Add("font-family", "Calibri");
        tc_to_month.Style.Add("font-size", "10pt");
        tc_to_month.HorizontalAlign = HorizontalAlign.Center;
        tc_to_month.VerticalAlign = VerticalAlign.Middle;
        tc_to_month.Controls.Add(lit_qnty11);
        tc_to_month.Attributes.Add("style", "font-weight:bold;");
        tc_to_month.Attributes.Add("Class", "rptCellBorder");
        tr_total.Cells.Add(tc_to_month);

        TableCell tc_to_month1 = new TableCell();
        Literal li_qnty1 = new Literal();
        li_qnty1.Text = p.ToString();        //iTotLstCount += Decimal.Parse(drFF["qty"].ToString());
        tc_to_month1.BorderStyle = BorderStyle.Solid;
        tc_to_month1.BorderWidth = 1;
        tc_to_month1.BackColor = System.Drawing.Color.White;
        tc_to_month1.Width = 200;
        tc_to_month1.Style.Add("font-family", "Calibri");
        tc_to_month1.Style.Add("font-size", "10pt");
        tc_to_month1.HorizontalAlign = HorizontalAlign.Center;
        tc_to_month1.VerticalAlign = VerticalAlign.Middle;
        tc_to_month1.Controls.Add(li_qnty1);
        tc_to_month1.Attributes.Add("style", "font-weight:bold;");
        tc_to_month1.Attributes.Add("Class", "rptCellBorder");
        tr_total.Cells.Add(tc_to_month1);




        TableCell tc_to_montht = new TableCell();
        Literal li_montht = new Literal();
        //int iTotLstCount = 0;
        li_montht.Text = r.ToString();
        //iTotLstCountt += Decimal.Parse(drFF["netweight"].ToString());

        tc_to_montht.BorderStyle = BorderStyle.Solid;
        tc_to_montht.BorderWidth = 1;
        tc_to_montht.BackColor = System.Drawing.Color.White;
        tc_to_montht.Width = 200;
        tc_to_montht.Style.Add("font-family", "Calibri");
        tc_to_montht.Style.Add("font-size", "10pt");
        tc_to_montht.HorizontalAlign = HorizontalAlign.Center;
        tc_to_montht.VerticalAlign = VerticalAlign.Middle;
        tc_to_montht.Controls.Add(li_montht);
        tc_to_montht.Attributes.Add("style", "font-weight:bold;");
        tc_to_montht.Attributes.Add("Class", "rptCellBorder");
        tr_total.Cells.Add(tc_to_montht);

        tbl.Rows.Add(tr_total);

    }

    protected void btngoclick(object sender, EventArgs e)
    {
        FillSF(ddlff.SelectedValue.ToString(),div_code,ddlmnth.SelectedValue.ToString(),ddlYR.SelectedValue.ToString());
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
        // Response.Write("Purchase_Register_Distributor_wise.aspx");

    }
    
}
