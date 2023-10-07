using Bus_EReport;
using DBase_EReport;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class MIS_Reports_rpt_Dailyinv_viewNative : System.Web.UI.Page
{
    string sfCode = string.Empty;
    string sfname = string.Empty;
    string divcode = string.Empty;
    int iTotLstCount1 = 0;
    int iTotLstCount = 0;
    int iTotLstCount2 = 0;
    string sMode = string.Empty;
    string Date = string.Empty;
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
    DataSet dsmgrsf = new DataSet();
    DataSet dsDoc = null;
    DateTime dtCurrent;
    string tot_fldwrk = string.Empty;
    string ChemistPOB_visit = string.Empty;
    string tot_Sub_days = string.Empty;
    string tot_dr = string.Empty;
    string Chemist_visit = string.Empty;
    string Stock_Visit = string.Empty;
    string tot_Stock_Calls_Seen = string.Empty;
    string tot_Dcr_Leave = string.Empty;
    string UnlistVisit = string.Empty;
    string tot_dcr_dr = string.Empty;
    string tot_doc_met = string.Empty;
    string tot_doc_calls_seen = string.Empty;
    string tot_doc_Unlstcalls_seen = string.Empty;
    string tot_CSH_calls_seen = string.Empty;
    string Monthsub = string.Empty;
    string strSf_Code = string.Empty;
    string MultiSf_Code = string.Empty;
    DataSet dsprd = new DataSet();
    string distributor_code = string.Empty;
    string Multi_Prod = string.Empty;
    string sCurrentDate = string.Empty;
    int currentId = 0;
    decimal b_inv_Total = 0;
    decimal s_Case_Total = 0;
    decimal s_string_Total = 0;
    decimal s_amount_Total = 0;
    decimal E_Case_Total = 0;
    decimal E_string_Total = 0;
    decimal E_amount_Total = 0;
    decimal TL_Case_Total = 0;
    decimal TL_string_Total = 0;
    decimal TL_amount_Total = 0;
    decimal IM_Case_Total = 0;
    decimal IM_string_Total = 0;
    decimal IN_BAL_Total = 0;
    decimal subTotal = 0;
    decimal nettotal = 0;
    decimal nttotal = 0;
    decimal total = 0;
    decimal disctprice = 0;
    decimal ovtotal = 0;
    decimal ovtot = 0;
    decimal disc_tot = 0;
    int subTotalRowIndex = 0;
    decimal opcase = 0;
    decimal oppiece = 0;
    string sf_type = string.Empty;
    string cat = string.Empty;
    string sub_Div_code = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        sf_type = Request.QueryString["Type"].ToString();
        if (sf_type == "4")
        {
            sfname = Request.QueryString["Sf_Name"].ToString();
            sfCode = Session["Sf_Code"].ToString();
            divcode = Session["Division_Code"].ToString().Replace(",", "");
            Date = Request.QueryString["Date"].ToString();
            sub_Div_code = Request.QueryString["Sub_div"].ToString();
            lblHead.Text = "Daily Inventory Detail ";
            FillSF();

        }
        else
        {
            divcode = Session["div_code"].ToString();
            sfCode = Request.QueryString["sf_code"].ToString();
            sfname = Request.QueryString["Sf_Name"].ToString();
            Date = Request.QueryString["Date"].ToString();
            sub_Div_code = Request.QueryString["Sub_div"].ToString();
            lblHead.Text = "Daily Inventory ImBalance Detail";
            Feild.Text = sfname;
            FillSF();
        }

        DateTime d1 = Convert.ToDateTime(Date);
        Label1.Text = "Date : " + d1.ToString("dd/MM/yyyy");
    }

    private void FillSF()
    {


        string sURL = string.Empty;
        string stURL = string.Empty;
        DataSet dsGV = new DataSet();
        DataSet dsGc = new DataSet();
        local dc = new local();

        string stCrtDtaPnt = string.Empty;
        if (sf_type == "4")
        {
            dsGV = dc.view_Daliy_inv_view(divcode, sfCode, Date, sub_Div_code);
            if (dsGV.Tables[0].Rows.Count > 0)
            {
                gvtotalorder.DataSource = dsGV;
                gvtotalorder.DataBind();
            }
            else
            {
                gvtotalorder.DataSource = null;
                gvtotalorder.DataBind();
            }


        }
        else
        {
            dsGV = dc.view_Daliy_inv_view(divcode, sfCode, Date, sub_Div_code);
            if (dsGV.Tables[0].Rows.Count > 0)
            {
                gvtotalorder.DataSource = dsGV;
                gvtotalorder.DataBind();
            }
            else
            {
                gvtotalorder.DataSource = null;
                gvtotalorder.DataBind();
            }


        }
    }
    protected void OnRowCreated(object sender, GridViewRowEventArgs e)
    {
        subTotal = 0;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataTable dt = (e.Row.DataItem as DataRowView).DataView.Table;
            string cat_code = dt.Rows[e.Row.RowIndex]["Product_Cat_Name"].ToString();

            if (cat_code != cat)
            {
                if (e.Row.RowIndex > 0 || e.Row.RowIndex == 0)
                {
                    for (int i = subTotalRowIndex; i < e.Row.RowIndex + 1; i++)
                    {


                        cat = dt.Rows[e.Row.RowIndex]["Product_Cat_Name"].ToString();


                    }
                    this.AddTotalRow(cat.ToString(), "");

                }

            }
        }
    }
    private void AddTotalRow(string labelText, string totvalue)
    {
        GridViewRow row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Normal);
        row.BackColor = ColorTranslator.FromHtml("#dbf7d9");
        row.CssClass = "subTot";
        row.Cells.AddRange(new TableCell[1] {
                                        new TableCell { Text = labelText, HorizontalAlign = HorizontalAlign.Left,ColumnSpan=21},

                                          });

        gvtotalorder.Controls[0].Controls.Add(row);
    }

    private void AddTotalRoww(string labelText, string ttvalue, string dispric, string ntvalue, string value, string b_inv_Total1, string s_string_Total1, string s_amount_Total1, string E_string_Total1, string E_amount_Total1, string opcc, string opp)
    {
        GridViewRow row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Normal);
        row.BackColor = ColorTranslator.FromHtml("#496a9a");
        row.ForeColor = ColorTranslator.FromHtml("#fff");

        row.CssClass = "GrndTot";
        row.Cells.AddRange(new TableCell[13] { new TableCell (), //Empty Cell
                                        new TableCell { Text = labelText, HorizontalAlign = HorizontalAlign.Right,ColumnSpan=0},

                                        new TableCell { Text = ttvalue, HorizontalAlign = HorizontalAlign.Right },
                                          new TableCell { Text = dispric, HorizontalAlign = HorizontalAlign.Right },
                                          new TableCell { Text = opcc, HorizontalAlign = HorizontalAlign.Right },
                                          new TableCell { Text = opp, HorizontalAlign = HorizontalAlign.Right },
                                        new TableCell { Text = ntvalue, HorizontalAlign = HorizontalAlign.Right } ,
                                         new TableCell { Text = value, HorizontalAlign = HorizontalAlign.Right } ,
                                         new TableCell { Text = b_inv_Total1, HorizontalAlign = HorizontalAlign.Right } ,
                                         new TableCell { Text = s_string_Total1, HorizontalAlign = HorizontalAlign.Right } ,
                                         new TableCell { Text = s_amount_Total1, HorizontalAlign = HorizontalAlign.Right } ,
                                         new TableCell { Text = E_string_Total1, HorizontalAlign = HorizontalAlign.Right } ,
                                        new TableCell { Text = E_amount_Total1, HorizontalAlign = HorizontalAlign.Right }
                                         
        });

        gvtotalorder.Controls[0].Controls.Add(row);
    }
    protected void OnDataBound(object sender, EventArgs e)
    {

        GridViewRow row = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Normal);

        TableHeaderCell cell = new TableHeaderCell();

        cell = new TableHeaderCell();
        cell.RowSpan = 3;
        cell.Text = "S.NO";
        row.Controls.Add(cell);

        cell = new TableHeaderCell();
        cell.RowSpan = 3;
        cell.Text = "Item Description";
        row.Controls.Add(cell);

        cell = new TableHeaderCell();
        cell.RowSpan = 3;
        cell.Text = "LP/ CASE";
        row.Controls.Add(cell);

        cell = new TableHeaderCell();
        cell.RowSpan = 3;
        cell.Text = "LP/ Pieces";
        row.Controls.Add(cell);

        cell = new TableHeaderCell();
        cell.ColumnSpan = 2;
        cell.RowSpan = 2;
        cell.Text = "Opening INV";
        row.Controls.Add(cell);

        cell = new TableHeaderCell();
        cell.ColumnSpan = 3;
        cell.RowSpan = 2;
        cell.Text = "BEG INV";
        row.Controls.Add(cell);

        cell = new TableHeaderCell();
        cell.ColumnSpan = 2;
        cell.RowSpan = 2;
        cell.Text = "SALES FOR THE DAY";
        row.Controls.Add(cell);

        cell = new TableHeaderCell();
        cell.ColumnSpan = 2;
        cell.Text = "END INV";
        row.Controls.Add(cell);

        GridViewRow row1 = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Normal);

        TableHeaderCell cell1 = new TableHeaderCell();

        cell1 = new TableHeaderCell();
        cell1.ColumnSpan = 2;
        cell1.Text = "BALANCE";
        row1.Controls.Add(cell1);

        //cell1 = new TableHeaderCell();
        //cell1.ColumnSpan = 3;
        //cell1.Text = "TL VERFIIED";
        //row1.Controls.Add(cell1);

        //cell1 = new TableHeaderCell();
        //cell1.ColumnSpan = 3;
        //cell1.Text = "IM BALANCE";
        //row1.Controls.Add(cell1);

        row1.BackColor = ColorTranslator.FromHtml("#3AC0F2");
        gvtotalorder.HeaderRow.Parent.Controls.AddAt(0, row);
        gvtotalorder.HeaderRow.Parent.Controls.AddAt(1, row1);


        for (int i = subTotalRowIndex; i < gvtotalorder.Rows.Count; i++)
        {
            subTotal += Convert.ToDecimal(((Label)gvtotalorder.Rows[i].FindControl("SF_name")).Text);
            nettotal += Convert.ToDecimal(((Label)gvtotalorder.Rows[i].FindControl("Route_name")).Text);
            ovtotal += Convert.ToDecimal(((Label)gvtotalorder.Rows[i].FindControl("Retailer_name")).Text);
            disctprice += Convert.ToDecimal(((Label)gvtotalorder.Rows[i].FindControl("netval")).Text);
            b_inv_Total += Convert.ToDecimal(((Label)gvtotalorder.Rows[i].FindControl("toval")).Text);
            opcase += Convert.ToDecimal(((Label)gvtotalorder.Rows[i].FindControl("OpenC")).Text);
            oppiece += Convert.ToDecimal(((Label)gvtotalorder.Rows[i].FindControl("OpenP")).Text);
           // s_Case_Total += Convert.ToDecimal(((Label)gvtotalorder.Rows[i].FindControl("discountpri")).Text);
            s_string_Total += Convert.ToDecimal(((Label)gvtotalorder.Rows[i].FindControl("orderval")).Text);
            s_amount_Total += Convert.ToDecimal(((Label)gvtotalorder.Rows[i].FindControl("orderval1")).Text);
            //E_Case_Total += Convert.ToDecimal(((Label)gvtotalorder.Rows[i].FindControl("discountpri2")).Text);
            E_string_Total += Convert.ToDecimal(((Label)gvtotalorder.Rows[i].FindControl("orderval2")).Text);
            E_amount_Total += Convert.ToDecimal(((Label)gvtotalorder.Rows[i].FindControl("orderval12")).Text);
            //TL_Case_Total += Convert.ToDecimal(((Label)gvtotalorder.Rows[i].FindControl("discountpri3")).Text);
            //TL_string_Total += Convert.ToDecimal(((Label)gvtotalorder.Rows[i].FindControl("orderval3")).Text);
            //TL_amount_Total += Convert.ToDecimal(((Label)gvtotalorder.Rows[i].FindControl("orderval13")).Text);
            //IM_Case_Total += Convert.ToDecimal(((Label)gvtotalorder.Rows[i].FindControl("discountpri4")).Text);
            //IM_string_Total += Convert.ToDecimal(((Label)gvtotalorder.Rows[i].FindControl("orderval4")).Text);
            //IN_BAL_Total += Convert.ToDecimal(((Label)gvtotalorder.Rows[i].FindControl("IN_BAL")).Text);
        }
        this.AddTotalRoww("Total", subTotal.ToString("N2"), nettotal.ToString("N2"), ovtotal.ToString("N2"), disctprice.ToString("N2"), b_inv_Total.ToString("N2"), s_string_Total.ToString("N2"), s_amount_Total.ToString("N2"), E_string_Total.ToString("N2"), E_amount_Total.ToString("N2"),opcase.ToString("N2"), oppiece.ToString("N2"));
        gvtotalorder.HeaderRow.Parent.Controls[2].Controls.RemoveAt(0);
        gvtotalorder.HeaderRow.Parent.Controls[2].Controls.RemoveAt(0);
        gvtotalorder.HeaderRow.Parent.Controls[2].Controls.RemoveAt(0);
        gvtotalorder.HeaderRow.Parent.Controls[2].Controls.RemoveAt(0);
        gvtotalorder.HeaderRow.Parent.Controls[2].Controls.RemoveAt(0);
    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {
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


    protected void gvtotalorder_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //for (int i = 0; i <= gvtotalorder.Rows.Count - 1; i++)
        //{
        //    Label lblparent = (Label)gvtotalorder.Rows[i].FindControl("orderval4");
        //    string gg = lblparent.Text;

        //    if (gg.Contains("-"))
        //    {

        //        gvtotalorder.Rows[i].Cells[18].BackColor = Color.FromName("#496a9a");
        //        lblparent.ForeColor = Color.Black;
        //    }
        //    else
        //    {

        //    }

        //}
    }
    public class local
    {
        public DataSet view_Daliy_inv_view(string div_code, string sf_code, string date, string sub_div_code = "0")
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            DataSet dsTerr = null;

            string state_code = string.Empty;

            string strQry = "select State_Code from Mas_Salesforce where Sf_Code='" + sf_code + "' and SF_Status=0";
            try
            {
                dsTerr = db_ER.Exec_DataSet(strQry);
                if (dsTerr.Tables[0].Rows.Count > 0)
                {
                    state_code = dsTerr.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }


            string strQry1 = "exec [view_Daliy_inv_view_sub_Native] '" + sf_code + "','" + div_code + "','" + date + "','" + state_code + "','" + sub_div_code + "'";

            try
            {
                dsAdmin = db_ER.Exec_DataSet(strQry1);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsAdmin;
        }
    }
}