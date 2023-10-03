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
using DBase_EReport;

public partial class MIS_Reports_rpt_Pri_Order_View : System.Web.UI.Page
{
    string currentsfid = string.Empty;
    public string sfCode = string.Empty;
    string sfname = string.Empty;
    public string divcode = string.Empty;
    int iTotLstCount1 = 0;
    int iTotLstCount = 0;
    int iTotLstCount2 = 0;
    string sMode = string.Empty;
    public string Date = string.Empty;
    public string TDate = string.Empty;
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
    decimal subTotal = 0;
    decimal nettotal = 0;
	decimal qtytot = 0;
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
    decimal catquan = 0;
    public string year = string.Empty;
    public static string div_code = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        DateTime dateTime = DateTime.UtcNow.Date;
        string todate = dateTime.ToString("dd-MM-yyyy");
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
            TDate = Request.QueryString["ToDate"].ToString();
            year = Request.QueryString["cur_year"].ToString();
            Feild.Text = sfname;
            DateTime d1 = Convert.ToDateTime(Date);
            DateTime d4 = Convert.ToDateTime(TDate);
            lblHead.Text = "Primary Order View ";
            if (d1 != d4)
            {
                Label2.Text = "Date : " + d1.ToString("dd-MM-yyyy") + " To" + d4.ToString("dd-MM-yyyy");
            }
            else
            {
                Label2.Text = "Date : " + d1.ToString("dd-MM-yyyy");
            }

            FillSF();

        }
        else
        {

            divcode = Session["div_code"].ToString();
            sfCode = Request.QueryString["sf_code"].ToString();
            sfname = Request.QueryString["Sf_Name"].ToString();
            Date = Request.QueryString["Date"].ToString();
            TDate = Request.QueryString["ToDate"].ToString();
            year = Request.QueryString["cur_year"].ToString();
            subdivision_code = Request.QueryString["subdiv"].ToString();
            Feild.Text = sfname;
            DateTime d2 = Convert.ToDateTime(Date);
            DateTime d3 = Convert.ToDateTime(TDate);
            lblHead.Text = "Primary Order View ";
            if (d2 != d3)
            {
                Label2.Text = "Date : " + d2.ToString("dd-MM-yyyy") + " To " + d3.ToString("dd-MM-yyyy");
            }
            else
            {
                Label2.Text = "Date : " + d2.ToString("dd-MM-yyyy");
            }

            FillSF();
        }

    }

    private void FillSF()
    {
        string sURL = string.Empty;
        string stURL = string.Empty;
        DataSet dsGV = new DataSet();
        DataSet dsGc = new DataSet();
        DCR dc = new DCR();
        loc dc1 = new loc();
        string stCrtDtaPnt = string.Empty;
        if (sf_type == "4")
        {
            dsGV = dc1.view_total_order_view1(divcode, sfCode, Date, TDate);
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

            dsGV = dc1.view_pri_order_view_sub(divcode, sfCode, Date, TDate, subdivision_code);
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
            dsGc = dc1.view_total_pri_order_view_categorywise(divcode, sfCode, Date, TDate, subdivision_code);
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
                stCrtDtaPnt += "{label:\"" + drFF["Product_Cat_Name"].ToString() + "\",y:";

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
		qtytot = 0;
        ovtotal = 0;
        disctprice = 0;
        freetotall = 0;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataTable dt = (e.Row.DataItem as DataRowView).DataView.Table;
            string sfID = Convert.ToString(dt.Rows[e.Row.RowIndex]["SF_Code"]);
            int orderId = Convert.ToInt32(dt.Rows[e.Row.RowIndex]["Stockist_Code"]);
            total += Convert.ToDecimal(dt.Rows[e.Row.RowIndex]["Order_value"]);
            nttotal += Convert.ToDecimal(dt.Rows[e.Row.RowIndex]["net_weight"]);
			//qtytot += Convert.ToDecimal(dt.Rows[e.Row.RowIndex]["quantity"]);
            ovtot += Convert.ToDecimal(dt.Rows[e.Row.RowIndex]["Collected_Amount"]);
            disc_tot += Convert.ToDecimal(dt.Rows[e.Row.RowIndex]["discount"]);
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
						qtytot += Convert.ToDecimal(((Label)gvtotalorder.Rows[i].FindControl("qty")).Text);
                        ovtotal += Convert.ToDecimal(((Label)gvtotalorder.Rows[i].FindControl("toval")).Text);
                        disctprice += Convert.ToDecimal(((Label)gvtotalorder.Rows[i].FindControl("discountpri")).Text);
                        freetotall += Convert.ToDecimal(((Label)gvtotalorder.Rows[i].FindControl("freept")).Text);
                    }
                    int stkcd = Convert.ToInt32(dt.Rows[e.Row.RowIndex - 1]["Stockist_Code"]);
                    this.AddTotalRow("Sub Total", ovtotal.ToString("N2"), disctprice.ToString("N2"), freetotall.ToString("N2"), nettotal.ToString("N2"), subTotal.ToString("N2"), stkcd.ToString(),qtytot.ToString("N2"));
                    subTotalRowIndex = e.Row.RowIndex;
                }
                currentId = orderId;
                currentsfid = sfID;
            }
        }
    }

    private void AddTotalRow(string labelText, string totvalue, string disprice, string free, string netvalue, string value, string stk,string qtytot)
    {
        GridViewRow row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Normal);
        row.BackColor = ColorTranslator.FromHtml("#dbf7d9");
        row.CssClass = "subTot";
        row.Attributes.Add("data-stk", stk);
        row.Cells.AddRange(new TableCell[8] { new TableCell (), //Empty Cell
                                        new TableCell { Text = labelText, HorizontalAlign = HorizontalAlign.Right,ColumnSpan=((divcode=="32")?3:3)},
                                        new TableCell { Text = netvalue, HorizontalAlign = HorizontalAlign.Right },
										new TableCell { Text = qtytot, HorizontalAlign = HorizontalAlign.Right },
                                        new TableCell { Text = free, HorizontalAlign = HorizontalAlign.Right },
                                        new TableCell { Text = totvalue, HorizontalAlign = HorizontalAlign.Right },
                                        new TableCell { Text = disprice, HorizontalAlign = HorizontalAlign.Right } ,
                                        new TableCell { Text = value, HorizontalAlign = HorizontalAlign.Right,CssClass="sub" }
                                        //new TableCell { Text = "", HorizontalAlign = HorizontalAlign.Right,CssClass="hidecancl"  }
										});
        gvtotalorder.Controls[0].Controls.Add(row);
    }

    private void AddTotalRoww(string labelText, string ttvalue, string dispric, string fretot, string ntvalue, string value,string qtytot)
    {
        GridViewRow row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Normal);
        row.BackColor = ColorTranslator.FromHtml("#ecf19f");
        row.CssClass = "GrndTot";
        row.Cells.AddRange(new TableCell[8] { new TableCell (), //Empty Cell
                                        new TableCell { Text = labelText, HorizontalAlign = HorizontalAlign.Right,ColumnSpan=((divcode=="32")?3:3)},
                                        new TableCell { Text = ntvalue, HorizontalAlign = HorizontalAlign.Right },
										new TableCell { Text = qtytot, HorizontalAlign = HorizontalAlign.Right },
                                        new TableCell { Text = fretot, HorizontalAlign = HorizontalAlign.Right },
                                        new TableCell { Text = ttvalue, HorizontalAlign = HorizontalAlign.Right },
                                        new TableCell { Text = dispric, HorizontalAlign = HorizontalAlign.Right },
                                        new TableCell { Text = value, HorizontalAlign = HorizontalAlign.Right }
                                        //new TableCell { Text = "", HorizontalAlign = HorizontalAlign.Right,CssClass="hidecancl" }
										});
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
			qtytot += Convert.ToDecimal(((Label)gvtotalorder.Rows[i].FindControl("qty")).Text);
            ovtotal += Convert.ToDecimal(((Label)gvtotalorder.Rows[i].FindControl("toval")).Text);
            disctprice += Convert.ToDecimal(((Label)gvtotalorder.Rows[i].FindControl("discountpri")).Text);
            freetotall += Convert.ToDecimal(((Label)gvtotalorder.Rows[i].FindControl("freept")).Text);
            stkcode = ((HiddenField)gvtotalorder.Rows[i].FindControl("stkcode")).Value;
        }
        this.AddTotalRow("Sub Total", ovtotal.ToString("N2"), disctprice.ToString("N2"), freetotall.ToString("N2"), nettotal.ToString("N2"), subTotal.ToString("N2"), stkcode.ToString(),qtytot.ToString("N2"));
        this.AddTotalRoww("Total", ovtot.ToString("N2"), disc_tot.ToString("N2"), freetot.ToString("N2"), nttotal.ToString("N2"), total.ToString("N2"),qtytot.ToString("N2"));

    }
    protected void catOnDataBound(object sender, EventArgs e)
    {
        for (int i = 0; i < GridViewcat.Rows.Count; i++)
        {
            // subTotal += Convert.ToDecimal(gvtotalorder.Rows[i].Cells[4].Text);
            catnwgt += Convert.ToDecimal(((Label)GridViewcat.Rows[i].FindControl("lnetweight")).Text);
            catquan += Convert.ToDecimal(((Label)GridViewcat.Rows[i].FindControl("lquantity")).Text);
            catval += Convert.ToDecimal(((Label)GridViewcat.Rows[i].FindControl("cval")).Text);            
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
                                           new TableCell { Text = catvalt, HorizontalAlign = HorizontalAlign.Right }
                                        });


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



    protected void btnClose_Click(object sender, EventArgs e)
    {

    }

    public class loc
    {
        public DataSet view_total_pri_order_view_categorywise(string div_code, string sf_code, string date, string TDate, string subdivcode = "0")
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            string strQry = "EXEC new_TODAY_PRI_ORDER_CATEGORY_VIEW '" + sf_code + "','" + div_code + "','" + date + "','" + TDate + "','" + subdivcode + "'";
            //string strQry = "exec [TODAY_PRI_ORDER_CATEGORY_VIEW] '" + sf_code + "','" + div_code + "','" + date + "','" + TDate + "','" + subdivcode + "'";
            try
            {
                dsAdmin = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsAdmin;
        }
        public DataSet view_total_order_view1(string div_code, string sf_code, string date, string Tdate)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;


            string strQry = "SELECT d.Trans_sl_no,d.SF_Code,(select Sf_Name from Mas_Salesforce g where Sf_Code=D.Sf_Code) Sf_Name,d.Stockist_Code,D.Cust_Code,(select TP.Stockist_Name from mas_stockist TP where Stockist_Code=D.Stockist_Code) Stockist_Name," +
                   "d.Order_value,d.net_weight_value,d.Order_date,d.Order_Flag," +
                   "(select t.Territory_Name from Mas_Territory_creation t where t.Territory_code=d.route and t.Territory_Active_Flag=0) as routename," +
                   "(select l.ListedDr_Name from Mas_Listeddr l where l.ListedDrCode=d.Cust_Code and l.ListedDr_Active_Flag=0) as retailername " +
                   "FROM Trans_Order_Head d " +
                   "where (CAST(CONVERT(VARCHAR, D.Order_date, 101) AS DATETIME) between CAST(CONVERT(VARCHAR, '" + date + "' , 101) AS DATETIME) and CAST(CONVERT(VARCHAR, '" + Tdate + "' , 101) AS DATETIME)) and D.STOCKIST_CODE='" + sf_code + "'" +
                   "group by d.Trans_sl_no,d.SF_Code,d.Stockist_Code,D.Cust_Code,d.Order_value,d.net_weight_value,d.Order_date,d.route,d.Order_Flag " +
                   "order by d.Stockist_Code,D.Cust_Code,d.route,d.Order_date,d.Order_value,d.net_weight_value,d.Order_Flag";
            try
            {
                dsAdmin = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsAdmin;
        }
        public DataSet view_pri_order_view_sub(string div_code, string sf_code, string date, string Tdate, string subdivcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            if (div_code == null || div_code == "")
            { div_code = "0"; }
                       

            string strQry = "EXEC new_TODAY_PRI_VIEW_DISCOUNT_FREEVAL1 '" + sf_code + "','" + div_code + "','" + date + "','" + Tdate + "','" + subdivcode + "'";
            //string strQry = "exec [TODAY_PRI_VIEW_DISCOUNT_FREEVAL1] '" + sf_code + "','" + div_code + "','" + date + "','" + Tdate + "','" + subdivcode + "'";
            try
            {
                dsAdmin = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsAdmin;
        }
    }

}


//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Web.UI;
//using System.Web.UI.WebControls;
//using System.IO;
//using System.Data;
//using iTextSharp.text;
//using iTextSharp.text.pdf;
//using iTextSharp.text.html;
//using iTextSharp.text.html.simpleparser;
//using System.Text;
//using Bus_EReport;
//using System.Net;
//using iTextSharp.tool.xml;
//using System.Web.UI.HtmlControls;
//using System.Drawing;
//public partial class MIS_Reports_rpt_Pri_Order_View : System.Web.UI.Page
//{
//    string currentsfid = string.Empty;
//    public string sfCode = string.Empty;
//    string sfname = string.Empty;
//    public string divcode = string.Empty;
//    int iTotLstCount1 = 0;
//    int iTotLstCount = 0;
//    int iTotLstCount2 = 0;
//    string sMode = string.Empty;
//    string Date = string.Empty;
//    string FYear = string.Empty;
//    string TMonth = string.Empty;
//    string TYear = string.Empty;
//    string Prod = string.Empty;
//    string Prod_Name = string.Empty;
//    string sReturn = string.Empty;
//    string sDay = string.Empty;
//    string sCnt = string.Empty;
//    string vMode = string.Empty;
//    DataSet dsSalesForce = new DataSet();
//    DataSet dsmgrsf = new DataSet();
//    DataSet dsDoc = null;
//    DateTime dtCurrent;
//    string tot_fldwrk = string.Empty;
//    string ChemistPOB_visit = string.Empty;
//    string tot_Sub_days = string.Empty;
//    string tot_dr = string.Empty;
//    string Chemist_visit = string.Empty;
//    string Stock_Visit = string.Empty;
//    string tot_Stock_Calls_Seen = string.Empty;
//    string tot_Dcr_Leave = string.Empty;
//    string UnlistVisit = string.Empty;
//    string tot_dcr_dr = string.Empty;
//    string tot_doc_met = string.Empty;
//    string tot_doc_calls_seen = string.Empty;
//    string tot_doc_Unlstcalls_seen = string.Empty;
//    string tot_CSH_calls_seen = string.Empty;
//    string Monthsub = string.Empty;
//    string strSf_Code = string.Empty;
//    string MultiSf_Code = string.Empty;
//    DataSet dsprd = new DataSet();
//    string distributor_code = string.Empty;
//    string Multi_Prod = string.Empty;
//    string sCurrentDate = string.Empty;
//    int currentId = 0;
//    decimal subTotal = 0;
//    decimal nettotal = 0;
//    decimal nttotal = 0;
//    decimal total = 0;
//    decimal disctprice = 0;
//    decimal ovtotal = 0;
//    decimal ovtot = 0;
//    decimal disc_tot = 0;
//    int subTotalRowIndex = 0;
//    string sf_type = string.Empty;
//    decimal freetot = 0;
//    decimal freetotall = 0;
//    string subdivision_code = "0";
//    decimal catnwgt = 0;
//    decimal catval = 0;
//    decimal catquan = 0;
//    public string year = string.Empty;
//    public static string div_code = string.Empty;

//    protected void Page_Load(object sender, EventArgs e)
//    {
//        div_code = Session["div_code"].ToString();
//        DateTime dateTime = DateTime.UtcNow.Date;
//        string todate = dateTime.ToString("dd-MM-yyyy");
//        try
//        {
//            sf_type = Request.QueryString["Type"].ToString();
//        }
//        catch (Exception)
//        { }
//        if (sf_type == "4")
//        {
//            //divcode = Session["div_code"].ToString();
//            sfname = Request.QueryString["Sf_Name"].ToString();
//            sfCode = Session["Sf_Code"].ToString();
//            divcode = Session["Division_Code"].ToString().Replace(",", "");
//            Date = Request.QueryString["Date"].ToString();
//            year = Request.QueryString["cur_year"].ToString();
//            Feild.Text = sfname;
//            DateTime d1 = Convert.ToDateTime(Date);
//            lblHead.Text = "Primary Order View ";
//            Label2.Text = "Date : " + d1.ToString("dd-MM-yyyy");
//            FillSF();
//        }
//        else
//        {
//            divcode = Session["div_code"].ToString();
//            sfCode = Request.QueryString["sf_code"].ToString();
//            sfname = Request.QueryString["Sf_Name"].ToString();
//            Date = Request.QueryString["Date"].ToString();
//            year = Request.QueryString["cur_year"].ToString();
//            subdivision_code = Request.QueryString["subdiv"].ToString();
//            Feild.Text = sfname;
//            DateTime d2 = Convert.ToDateTime(Date);
//            lblHead.Text = "Primary Order View ";
//            Label2.Text = "Date : " + d2.ToString("dd-MM-yyyy");
//            FillSF();
//        }
//    }

//    private void FillSF()
//    {
//        string sURL = string.Empty;
//        string stURL = string.Empty;
//        DataSet dsGV = new DataSet();
//        DataSet dsGc = new DataSet();
//        DCR dc = new DCR();

//        string stCrtDtaPnt = string.Empty;
//        if (sf_type == "4")
//        {
//            dsGV = dc.view_total_order_view1(divcode, sfCode, Date);
//            if (dsGV.Tables[0].Rows.Count > 0)
//            {
//                gvtotalorder.DataSource = dsGV;
//                gvtotalorder.DataBind();
//            }
//            else
//            {
//                gvtotalorder.DataSource = null;
//                gvtotalorder.DataBind();
//            }

//            Label1.Visible = false;
//        }
//        else
//        {
//            dsGV = dc.view_pri_order_view_sub(divcode, sfCode, Date, subdivision_code);
//            if (dsGV.Tables[0].Rows.Count > 0)
//            {
//                gvtotalorder.DataSource = dsGV;
//                gvtotalorder.DataBind();
//            }
//            else
//            {
//                gvtotalorder.DataSource = null;
//                gvtotalorder.DataBind();
//            }
//            dsGc = dc.view_total_pri_order_view_categorywise(divcode, sfCode, Date, subdivision_code);
//            if (dsGc.Tables[0].Rows.Count > 0)
//            {
//                GridViewcat.DataSource = dsGc;
//                GridViewcat.DataBind();
//            }
//            else
//            {
//                GridViewcat.DataSource = null;
//                GridViewcat.DataBind();
//            }
//            foreach (DataRow drFF in dsGc.Tables[0].Rows)
//            {
//                stCrtDtaPnt += "{label:\"" + drFF["Product_Cat_Name"].ToString() + "\",y:";

//                stCrtDtaPnt += Convert.ToString(drFF["value"]) + "},";
//            }
//            string scrpt = "arr=[" + stCrtDtaPnt + "];window.onload = function () {genChart(arr);}";
//            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "SetupData", scrpt, true);
//        }
//    }

//    protected void OnRowCreated(object sender, GridViewRowEventArgs e)
//    {
//        subTotal = 0;
//        nettotal = 0;
//        ovtotal = 0;
//        disctprice = 0;
//        freetotall = 0;

//        if (e.Row.RowType == DataControlRowType.DataRow)
//        {
//            DataTable dt = (e.Row.DataItem as DataRowView).DataView.Table;
//            string sfID = Convert.ToString(dt.Rows[e.Row.RowIndex]["SF_Code"]);
//            int orderId = Convert.ToInt32(dt.Rows[e.Row.RowIndex]["Stockist_Code"]);
//            total += Convert.ToDecimal(dt.Rows[e.Row.RowIndex]["Order_value"]);
//            nttotal += Convert.ToDecimal(dt.Rows[e.Row.RowIndex]["net_weight"]);
//            ovtot += Convert.ToDecimal(dt.Rows[e.Row.RowIndex]["Collected_Amount"]);
//            disc_tot += Convert.ToDecimal(dt.Rows[e.Row.RowIndex]["discount"]);
//            freetot += Convert.ToDecimal(dt.Rows[e.Row.RowIndex]["Free"]);

//            if (sfID != currentsfid)
//            {
//                if (e.Row.RowIndex > 0)
//                {
//                    for (int i = subTotalRowIndex; i < e.Row.RowIndex; i++)
//                    {
//                        subTotal += Convert.ToDecimal(((Label)gvtotalorder.Rows[i].FindControl("orderval")).Text);
//                        nettotal += Convert.ToDecimal(((Label)gvtotalorder.Rows[i].FindControl("netval")).Text);
//                        ovtotal += Convert.ToDecimal(((Label)gvtotalorder.Rows[i].FindControl("toval")).Text);
//                        disctprice += Convert.ToDecimal(((Label)gvtotalorder.Rows[i].FindControl("discountpri")).Text);
//                        freetotall += Convert.ToDecimal(((Label)gvtotalorder.Rows[i].FindControl("freept")).Text);
//                    }
//                    int stkcd = Convert.ToInt32(dt.Rows[e.Row.RowIndex - 1]["Stockist_Code"]);
//                    this.AddTotalRow("Sub Total", ovtotal.ToString("N2"), disctprice.ToString("N2"), freetotall.ToString("N2"), nettotal.ToString("N2"), subTotal.ToString("N2"), stkcd.ToString());
//                    subTotalRowIndex = e.Row.RowIndex;
//                    //this.AddTotalRow("Sub Total", subTotal.ToString("N2"));
//                    //subTotalRowIndex = e.Row.RowIndex;
//                }
//                currentId = orderId;
//                currentsfid = sfID;
//            }
//        }
//    }

//    private void AddTotalRow(string labelText, string totvalue, string disprice, string free, string netvalue, string value, string stk)
//    {
//        GridViewRow row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Normal);
//        row.BackColor = ColorTranslator.FromHtml("#dbf7d9");
//        row.CssClass = "subTot";
//        row.Attributes.Add("data-stk", stk);
//        row.Cells.AddRange(new TableCell[8] { new TableCell (), //Empty Cell
//                                        new TableCell { Text = labelText, HorizontalAlign = HorizontalAlign.Right,ColumnSpan=((divcode=="32")?8:3)},
//                                        new TableCell { Text = netvalue, HorizontalAlign = HorizontalAlign.Right },
//                                        new TableCell { Text = free, HorizontalAlign = HorizontalAlign.Right },
//                                        new TableCell { Text = totvalue, HorizontalAlign = HorizontalAlign.Right },
//                                        new TableCell { Text = disprice, HorizontalAlign = HorizontalAlign.Right } ,
//                                        new TableCell { Text = value, HorizontalAlign = HorizontalAlign.Right,CssClass="sub" },
//                                        new TableCell { Text = "", HorizontalAlign = HorizontalAlign.Right,CssClass="hidecancl"  }
//        });
//        gvtotalorder.Controls[0].Controls.Add(row);
//    }

//    private void AddTotalRoww(string labelText, string ttvalue, string dispric, string fretot, string ntvalue, string value)
//    {
//        GridViewRow row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Normal);
//        row.BackColor = ColorTranslator.FromHtml("#ecf19f");
//        row.CssClass = "GrndTot";
//        row.Cells.AddRange(new TableCell[8] { new TableCell (), //Empty Cell
//                                        new TableCell { Text = labelText, HorizontalAlign = HorizontalAlign.Right,ColumnSpan=((divcode=="32")?6:3)},
//                                        new TableCell { Text = ntvalue, HorizontalAlign = HorizontalAlign.Right },
//                                        new TableCell { Text = fretot, HorizontalAlign = HorizontalAlign.Right },
//                                        new TableCell { Text = ttvalue, HorizontalAlign = HorizontalAlign.Right },
//                                        new TableCell { Text = dispric, HorizontalAlign = HorizontalAlign.Right },
//                                        new TableCell { Text = value, HorizontalAlign = HorizontalAlign.Right },
//                                        new TableCell { Text = "", HorizontalAlign = HorizontalAlign.Right,CssClass="hidecancl" }});
//        gvtotalorder.Controls[0].Controls.Add(row);
//    }

//    protected void OnDataBound(object sender, EventArgs e)
//    {
//        string stkcode = "";
//        for (int i = subTotalRowIndex; i < gvtotalorder.Rows.Count; i++)
//        {
//            // subTotal += Convert.ToDecimal(gvtotalorder.Rows[i].Cells[4].Text);
//            subTotal += Convert.ToDecimal(((Label)gvtotalorder.Rows[i].FindControl("orderval")).Text);
//            nettotal += Convert.ToDecimal(((Label)gvtotalorder.Rows[i].FindControl("netval")).Text);
//            ovtotal += Convert.ToDecimal(((Label)gvtotalorder.Rows[i].FindControl("toval")).Text);
//            disctprice += Convert.ToDecimal(((Label)gvtotalorder.Rows[i].FindControl("discountpri")).Text);
//            freetotall += Convert.ToDecimal(((Label)gvtotalorder.Rows[i].FindControl("freept")).Text);
//            stkcode = ((HiddenField)gvtotalorder.Rows[i].FindControl("stkcode")).Value;
//        }
//        this.AddTotalRow("Sub Total", ovtotal.ToString("N2"), disctprice.ToString("N2"), freetotall.ToString("N2"), nettotal.ToString("N2"), subTotal.ToString("N2"), stkcode.ToString());
//        this.AddTotalRoww("Total", ovtot.ToString("N2"), disc_tot.ToString("N2"), freetot.ToString("N2"), nttotal.ToString("N2"), total.ToString("N2"));

//    }

//    protected void catOnDataBound(object sender, EventArgs e)
//    {
//        for (int i = 0; i < GridViewcat.Rows.Count; i++)
//        {          
//            catnwgt += Convert.ToDecimal(((Label)GridViewcat.Rows[i].FindControl("lnetweight")).Text);
//            catquan += int.Parse(((Label)GridViewcat.Rows[i].FindControl("lquantity")).Text);
//            catval += Convert.ToDecimal(((Label)GridViewcat.Rows[i].FindControl("cval")).Text);           
//        }

//        this.AddTotalRowwcat("Total", catnwgt.ToString("N2"), catquan.ToString(), catval.ToString("N2"));
//    }
//    private void AddTotalRowwcat(string labelText, string catnwgtt, string catquant, string catvalt)
//    {
//        GridViewRow row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Normal);
//        row.BackColor = ColorTranslator.FromHtml("#ecf19f");

//        row.CssClass = "GrndTot";
//        row.Cells.AddRange(new TableCell[5] { new TableCell (), //Empty Cell
//                                        new TableCell { Text = labelText, HorizontalAlign = HorizontalAlign.Right,ColumnSpan=1},
//                                           new TableCell { Text = catnwgtt, HorizontalAlign = HorizontalAlign.Right },
//                                            new TableCell { Text = catquant, HorizontalAlign = HorizontalAlign.Right },
//                                        new TableCell { Text = catvalt, HorizontalAlign = HorizontalAlign.Right,ForeColor=ColorTranslator.FromHtml("red"), CssClass ="cat" }});


//        GridViewcat.Controls[0].Controls.Add(row);
//    }

//    protected void btnPrint_Click(object sender, EventArgs e)
//    {
//        // Session["ctrl"] = pnlContents;
//        //  Control ctrl = (Control)Session["ctrl"];
//        //   PrintWebControl(ctrl);
//    }

//    public static void PrintWebControl(Control ControlToPrint)
//    {
//        StringWriter stringWrite = new StringWriter();
//        System.Web.UI.HtmlTextWriter htmlWrite = new System.Web.UI.HtmlTextWriter(stringWrite);
//        if (ControlToPrint is WebControl)
//        {
//            Unit w = new Unit(100, UnitType.Percentage);
//            ((WebControl)ControlToPrint).Width = w;
//        }
//        Page pg = new Page();
//        pg.EnableEventValidation = false;
//        HtmlForm frm = new HtmlForm();
//        pg.Controls.Add(frm);
//        frm.Attributes.Add("runat", "server");
//        frm.Controls.Add(ControlToPrint);
//        pg.DesignerInitialize();
//        pg.RenderControl(htmlWrite);
//        string strHTML = stringWrite.ToString();
//        HttpContext.Current.Response.Clear();
//        HttpContext.Current.Response.Write(strHTML);
//        HttpContext.Current.Response.Write("<script>window.print();</script>");
//        HttpContext.Current.Response.End();

//    }

//    protected void btnClose_Click(object sender, EventArgs e)
//    {

//    }


//}