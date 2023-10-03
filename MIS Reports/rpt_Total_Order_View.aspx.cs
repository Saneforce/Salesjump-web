using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
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
using System.Web.UI.HtmlControls;
using System.Drawing;
using System.Web.Services;
public partial class MIS_Reports_rpt_Total_Order_View : System.Web.UI.Page
{
    string currentsfid = string.Empty;
    string sfCode = string.Empty;
    public string sfname = string.Empty;
    public string divcode = string.Empty;
    int iTotLstCount1 = 0;
    int iTotLstCount = 0;
    int iTotLstCount2 = 0;
    string sMode = string.Empty;
    public string Date11 = string.Empty;
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
    public string SecOrderCap = "Secondary Order View";
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
    string currentId = "0";
    decimal subTotal = 0;
    decimal nettotal = 0;
    decimal nttotal = 0;
    decimal total = 0;
    decimal disctprice = 0;
    decimal ovtotal = 0;
    decimal ovtot = 0;
    decimal disc_tot = 0;
    int subTotalRowIndex = 0;
    string sf_type = string.Empty;
    decimal freetot = 0;
    decimal freetotall = 0;
    string subdivision_code = "0";
    decimal catnwgt = 0;
    decimal catval = 0;
    int catquan = 0;
    public string month = string.Empty;
    public string year = string.Empty;
    public string date = string.Empty;
    public string sfcode = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        Date11 = Convert.ToString(Request.QueryString["Date"].ToString());
        sfname = Request.QueryString["Sf_Name"].ToString();
        sfcode = Request.QueryString["sf_code"].ToString();
        month = Request.QueryString["cur_month"].ToString();
        year = Request.QueryString["cur_year"].ToString();
        date = Request.QueryString["Date"].ToString();
        //subdivision_code = Request.QueryString["Sub_Div"].ToString();
        try
        {
            sf_type = Request.QueryString["Type"].ToString();
        }
        catch (Exception)
        { }

        if (sf_type == "4")
        {
            //divcode = Session["div_code"].ToString();
            sfname = Request.QueryString["Sf_Name"].ToString();
            sfCode = Session["Sf_Code"].ToString();
            divcode = Session["Division_Code"].ToString().Replace(",", "");
            Date = Request.QueryString["Date"].ToString();

            DateTime d1 = Convert.ToDateTime(Date);

            lblHead.Text = SecOrderCap;// "Secondary Order View ";

            Label2.Text = "Date : " + d1.ToString("dd-MM-yyyy");

            Feild.Text = sfname;

            FillSF();

        }
        else
        {
            divcode = Session["div_code"].ToString();
            sfCode = Request.QueryString["sf_code"].ToString();
            sfname = Request.QueryString["Sf_Name"].ToString();
            Date = Request.QueryString["Date"].ToString();
            DateTime d1 = Convert.ToDateTime(Date);

            Label2.Text = "Date : " + d1.ToString("dd-MM-yyyy");
            Feild.Text = sfname;
            FillSF();
        }

        if (divcode == "98") SecOrderCap = "Daily Calls Report";
        lblHead.Text = SecOrderCap; //"Secondary Order View ";
    }

    private void FillSF()
    {


        string sURL = string.Empty;
        string stURL = string.Empty;
        DataSet dsGV = new DataSet();
        DataSet dsGc = new DataSet();
        DCR dc = new DCR();

        string stCrtDtaPnt = string.Empty;
        if (sf_type == "4")
        {
            dsGV = dc.view_total_order_view1(divcode, sfCode, Date);
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
            Label1.Visible = false;

        }
        else
        {
            dsGV = dc.view_total_order_view(divcode, sfCode, Date, subdivision_code);
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
            dsGc = dc.view_total_order_view_categorywise(divcode, sfCode, Date, subdivision_code);
            if (dsGc.Tables[0].Rows.Count > 0)
            {
                GridViewcat.DataSource = dsGc;
                GridViewcat.DataBind();
            }
            else
            {
                GridViewcat.DataSource = null;
                GridViewcat.DataBind();
            }
            foreach (DataRow drFF in dsGc.Tables[0].Rows)
            {
                stCrtDtaPnt += "{name:\"" + drFF["Product_Cat_Name"].ToString() + "\",y:";

                stCrtDtaPnt += Convert.ToString(drFF["value"]) + "},";
            }
            string scrpt = "arr=[" + stCrtDtaPnt + "];window.onload = function () {genChart(arr);}";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "SetupData", scrpt, true);
        }
    }
    protected void OnRowCreated(object sender, GridViewRowEventArgs e)
    {
        subTotal = 0;
        nettotal = 0;
        ovtotal = 0;
        disctprice = 0;
        freetotall = 0;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataTable dt = (e.Row.DataItem as DataRowView).DataView.Table;
            string sfID = Convert.ToString(dt.Rows[e.Row.RowIndex]["SF_Code"]);
            // int orderId = Convert.ToInt32(dt.Rows[e.Row.RowIndex]["Stockist_Code"]);
            string orderId = Convert.ToString(dt.Rows[e.Row.RowIndex]["Stockist_Code"]);
            total += Convert.ToDecimal(dt.Rows[e.Row.RowIndex]["Order_value"]);
            nttotal += Convert.ToDecimal(dt.Rows[e.Row.RowIndex]["net_weight_value"]);
            ovtot += Convert.ToDecimal(dt.Rows[e.Row.RowIndex]["Totalvalue"]);
            disc_tot += Convert.ToDecimal(dt.Rows[e.Row.RowIndex]["discount_price"]);
            freetot += Convert.ToDecimal(dt.Rows[e.Row.RowIndex]["free"]);
            //Response.Write(gvtotalorder.Rows[0].Cells[2].Text);
            if (sfID != currentsfid)
            {
                if (e.Row.RowIndex > 0)
                {
                    for (int i = subTotalRowIndex; i < e.Row.RowIndex; i++)
                    {
                        //subTotal += Convert.ToDecimal(gvtotalorder.Rows[i].Cells[4].Text);
                        subTotal += Convert.ToDecimal(((Label)gvtotalorder.Rows[i].FindControl("orderval")).Text);
                        nettotal += Convert.ToDecimal(((Label)gvtotalorder.Rows[i].FindControl("netval")).Text);
                        ovtotal += Convert.ToDecimal(((Label)gvtotalorder.Rows[i].FindControl("toval")).Text);
                        disctprice += Convert.ToDecimal(((Label)gvtotalorder.Rows[i].FindControl("discountpri")).Text);
                        freetotall += Convert.ToDecimal(((Label)gvtotalorder.Rows[i].FindControl("freept")).Text);
                    }
                    string stkcd = Convert.ToString(dt.Rows[e.Row.RowIndex - 1]["Stockist_Code"]);
                    this.AddTotalRow("Sub Total", ovtotal.ToString("N2"), disctprice.ToString("N2"), freetotall.ToString("N2"), nettotal.ToString("N2"), subTotal.ToString("N2"), stkcd.ToString());
                    subTotalRowIndex = e.Row.RowIndex;
                }
                currentId = orderId;
                currentsfid = sfID;
            }
        }
    }
    private void AddTotalRow(string labelText, string totvalue, string disprice, string free, string netvalue, string value, string stk)
    {
        GridViewRow row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Normal);
        row.BackColor = ColorTranslator.FromHtml("#dbf7d9");
        row.CssClass = "subTot";
        row.Attributes.Add("data-stk", stk);
        row.Cells.AddRange(new TableCell[8] { new TableCell (), //Empty Cell
                                        new TableCell { Text = labelText, HorizontalAlign = HorizontalAlign.Right,ColumnSpan=((divcode=="32")?6:5)},
                                        new TableCell { Text = netvalue, HorizontalAlign = HorizontalAlign.Right },
                                        new TableCell { Text = free, HorizontalAlign = HorizontalAlign.Right },
                                        new TableCell { Text = totvalue, HorizontalAlign = HorizontalAlign.Right },
                                        new TableCell { Text = disprice, HorizontalAlign = HorizontalAlign.Right } ,
                                        new TableCell { Text = value, HorizontalAlign = HorizontalAlign.Right,CssClass="sub" },
                                        new TableCell { Text = "", HorizontalAlign = HorizontalAlign.Right,CssClass="hidecancl"  }});
        gvtotalorder.Controls[0].Controls.Add(row);
    }
    private void AddTotalRoww(string labelText, string ttvalue, string dispric, string fretot, string ntvalue, string value)
    {
        GridViewRow row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Normal);
        row.BackColor = ColorTranslator.FromHtml("#ecf19f");
        row.CssClass = "GrndTot";
        row.Cells.AddRange(new TableCell[8] { new TableCell (), //Empty Cell
                                        new TableCell { Text = labelText, HorizontalAlign = HorizontalAlign.Right,ColumnSpan=((divcode=="32")?6:5)},
                                        new TableCell { Text = ntvalue, HorizontalAlign = HorizontalAlign.Right },
                                        new TableCell { Text = fretot, HorizontalAlign = HorizontalAlign.Right },
                                        new TableCell { Text = ttvalue, HorizontalAlign = HorizontalAlign.Right },
                                        new TableCell { Text = dispric, HorizontalAlign = HorizontalAlign.Right },
                                        new TableCell { Text = value, HorizontalAlign = HorizontalAlign.Right },
                                        new TableCell { Text = "", HorizontalAlign = HorizontalAlign.Right,CssClass="hidecancl" }});
        gvtotalorder.Controls[0].Controls.Add(row);
    }
    protected void OnDataBound(object sender, EventArgs e)
    {
        string stkcode = "";
        for (int i = subTotalRowIndex; i < gvtotalorder.Rows.Count; i++)
        {
            // subTotal += Convert.ToDecimal(gvtotalorder.Rows[i].Cells[4].Text);
            subTotal += Convert.ToDecimal(((Label)gvtotalorder.Rows[i].FindControl("orderval")).Text);
            nettotal += Convert.ToDecimal(((Label)gvtotalorder.Rows[i].FindControl("netval")).Text);
            ovtotal += Convert.ToDecimal(((Label)gvtotalorder.Rows[i].FindControl("toval")).Text);
            disctprice += Convert.ToDecimal(((Label)gvtotalorder.Rows[i].FindControl("discountpri")).Text);
            freetotall += Convert.ToDecimal(((Label)gvtotalorder.Rows[i].FindControl("freept")).Text);
            stkcode = ((HiddenField)gvtotalorder.Rows[i].FindControl("stkcode")).Value;
        }
        this.AddTotalRow("Sub Total", ovtotal.ToString("N2"), disctprice.ToString("N2"), freetotall.ToString("N2"), nettotal.ToString("N2"), subTotal.ToString("N2"), stkcode.ToString());
        this.AddTotalRoww("Total", ovtot.ToString("N2"), disc_tot.ToString("N2"), freetot.ToString("N2"), nttotal.ToString("N2"), total.ToString("N2"));

    }
    protected void catOnDataBound(object sender, EventArgs e)
    {
        for (int i = 0; i < GridViewcat.Rows.Count; i++)
        {
            // subTotal += Convert.ToDecimal(gvtotalorder.Rows[i].Cells[4].Text);
            catnwgt += Convert.ToDecimal(((Label)GridViewcat.Rows[i].FindControl("lnetweight")).Text);
            catquan += int.Parse(((Label)GridViewcat.Rows[i].FindControl("lquantity")).Text);
            catval += Convert.ToDecimal(((Label)GridViewcat.Rows[i].FindControl("cval")).Text);
            //subTotal += Convert.ToDecimal(((Label)gvtotalorder.Rows[i].FindControl("orderval")).Text);
            //nettotal += Convert.ToDecimal(((Label)gvtotalorder.Rows[i].FindControl("netval")).Text);
            //ovtotal += Convert.ToDecimal(((Label)gvtotalorder.Rows[i].FindControl("toval")).Text);
            //disctprice += Convert.ToDecimal(((Label)gvtotalorder.Rows[i].FindControl("discountpri")).Text);
            //freetotall += Convert.ToDecimal(((Label)gvtotalorder.Rows[i].FindControl("freept")).Text);
        }
        //this.AddTotalRow("Sub Total", ovtotal.ToString("N2"), disctprice.ToString("N2"), freetotall.ToString("N2"), nettotal.ToString("N2"), subTotal.ToString("N2"));
        this.AddTotalRowwcat("Total", catnwgt.ToString("N2"), catquan.ToString(), catval.ToString("N2"));
    }
    private void AddTotalRowwcat(string labelText, string catnwgtt, string catquant, string catvalt)
    {
        GridViewRow row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Normal);
        row.BackColor = ColorTranslator.FromHtml("#ecf19f");

        row.CssClass = "GrndTot";
        row.Cells.AddRange(new TableCell[5] { new TableCell (), //Empty Cell
                                        new TableCell { Text = labelText, HorizontalAlign = HorizontalAlign.Right,ColumnSpan=1},
                                           new TableCell { Text = catnwgtt, HorizontalAlign = HorizontalAlign.Right },
                                            new TableCell { Text = catquant, HorizontalAlign = HorizontalAlign.Right },
                                        new TableCell { Text = catvalt, HorizontalAlign = HorizontalAlign.Right,ForeColor=ColorTranslator.FromHtml("red"), CssClass ="cat" }});


        GridViewcat.Controls[0].Controls.Add(row);
    }


    protected void btnPrint_Click(object sender, EventArgs e)
    {
        // Session["ctrl"] = pnlContents;
        //  Control ctrl = (Control)Session["ctrl"];
        //   PrintWebControl(ctrl);
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

    [WebMethod]
    public static string deleteOrder(string TransslNo)
    {
        DCR ndc = new DCR();
        int iReturn = ndc.delete_Order(TransslNo);
        if (iReturn > 0)
            return "Success";
        else
            return "Error";
    }


    protected void btnClose_Click(object sender, EventArgs e)
    {

    }


    protected void gvtotalorder_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
}