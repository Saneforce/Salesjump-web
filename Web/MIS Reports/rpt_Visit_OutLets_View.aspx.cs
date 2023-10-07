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

public partial class MIS_Reports_rpt_Visit_OutLets_View : System.Web.UI.Page
{
    public static string currentsfid = string.Empty;
    public static string sfCode = string.Empty;
    public static string sfname = string.Empty;
    public static string divcode = string.Empty;
    int iTotLstCount1 = 0;
    int iTotLstCount = 0;
    int iTotLstCount2 = 0;
    public static string sMode = string.Empty;
    public static string Date = string.Empty;
    public static string FYear = string.Empty;
    public static string TMonth = string.Empty;
    public static string TYear = string.Empty;
    public static string Prod = string.Empty;
    public static string Prod_Name = string.Empty;
    public static string sReturn = string.Empty;
    public static string sDay = string.Empty;
    public static string sCnt = string.Empty;
    public static string vMode = string.Empty;
    DataSet dsSalesForce = new DataSet();
    DataSet dsmgrsf = new DataSet();
    DataSet dsDoc = null;
    DateTime dtCurrent;
    public static string tot_fldwrk = string.Empty;
    public static string ChemistPOB_visit = string.Empty;
    public static string tot_Sub_days = string.Empty;
    public static string tot_dr = string.Empty;
    public static string Chemist_visit = string.Empty;
    public static string Stock_Visit = string.Empty;
    public static string tot_Stock_Calls_Seen = string.Empty;
    public static string tot_Dcr_Leave = string.Empty;
    public static string UnlistVisit = string.Empty;
    public static string tot_dcr_dr = string.Empty;
    public static string tot_doc_met = string.Empty;
    public static string tot_doc_calls_seen = string.Empty;
    public static string tot_doc_Unlstcalls_seen = string.Empty;
    public static string tot_CSH_calls_seen = string.Empty;
    public static string Monthsub = string.Empty;
    public static string strSf_Code = string.Empty;
    public static string MultiSf_Code = string.Empty;
    DataSet dsprd = new DataSet();
    public static string distributor_code = string.Empty;
    public static string Multi_Prod = string.Empty;
    public static string sCurrentDate = string.Empty;
    int currentId = 0;
    public static decimal subTotal = 0;
    public static decimal nettotal = 0;
    public static decimal nttotal = 0;
    public static decimal total = 0;
    public static decimal disctprice = 0;
    public static decimal ovtotal = 0;
    public static decimal ovtot = 0;
    public static decimal disc_tot = 0;
    int subTotalRowIndex = 0;
    public static string sf_type = string.Empty;
    public static decimal freetot = 0;
    public static decimal freetotall = 0;
    public static string subdivision_code = "0";

    protected void Page_Load(object sender, EventArgs e)
    {
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
			
			DateTime dateTime1 = DateTime.UtcNow.Date;
            dateTime1  = Convert.ToDateTime(Date);
            Date = dateTime1.ToString("yyyy-MM-dd 00:00:00");
			
            Hid_date.Value = Date;
			Feild.Text=sfname;
            DateTime d1 = Convert.ToDateTime(Date);
            lblHead.Text = "Visited Outlets View for the Day of " + d1.ToString("dd-MM-yyyy");
            //FillSF();
        }
        else
        {
			 
            divcode = Session["div_code"].ToString();
            sfCode = Request.QueryString["sf_code"].ToString();
            sfname = Request.QueryString["Sf_Name"].ToString();
			
            Date = Request.QueryString["Date"].ToString();
           		   
		    DateTime dateTime1 = DateTime.UtcNow.Date;
            dateTime1  = Convert.ToDateTime(Date);
            Date = dateTime1.ToString("yyyy-MM-dd 00:00:00");
		   
			subdivision_code = Request.QueryString["subdiv"].ToString();
			Feild.Text=sfname;
            DateTime d2 = Convert.ToDateTime(Date);
            lblHead.Text = "Visited Outlets View for the Day of " + d2.ToString("dd-MM-yyyy");
            Hid_date.Value = d2.ToString("yyyy-MM-dd");
            //FillSF();
        }
		
		if (!IsPostBack)
        {
            fillsubdivision();

            FillSF();
            
        }
    }
	
	private void fillsubdivision()
    {
        SalesForce sd = new SalesForce();
        dsSalesForce = sd.Getsubdivisionwise(divcode);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            ddlsubdiv.DataTextField = "subdivision_name";
            ddlsubdiv.DataValueField = "subdivision_code";
            ddlsubdiv.DataSource = dsSalesForce;
            ddlsubdiv.DataBind();
            ddlsubdiv.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select All--", "0"));
        }
    }

    private void FillSF()
    {


        string sURL = string.Empty;
        string stURL = string.Empty;
        DataSet dsGV = new DataSet();
        DataSet dsGc = new DataSet();
        DCR dc = new DCR();

        string stCrtDtaPnt = string.Empty;
		 int sudiv = Convert.ToInt32(ddlsubdiv.SelectedIndex);
        if (sf_type == "4")
        {
            //dsGV = dc.view_Visited_Outlets_view_sub(divcode, sfCode, Date, subdivision_code);
			dsGV = dc.view_Visited_Outlets_view_sub(divcode, Convert.ToString(sudiv), sfCode, Date);
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

            //dsGV = dc.view_Visited_Outlets_view_sub(divcode, sfCode, Date, subdivision_code);
			dsGV = dc.view_Visited_Outlets_view_sub(divcode, Convert.ToString(sudiv), sfCode, Date);
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
                stCrtDtaPnt += "{label:\"" + drFF["Product_Cat_Name"].ToString() + "\",y:";

                stCrtDtaPnt += Convert.ToString(drFF["value"]) + "},";
            }
            //string scrpt = "arr=[" + stCrtDtaPnt + "];window.onload = function () {genChart(arr);}";
            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "SetupData", scrpt, true);
        }
    }
    protected void OnRowCreated(object sender, GridViewRowEventArgs e)
    {
        subTotal = 0;
        nettotal = 0;
        ovtotal = 0;
        disctprice = 0;
        freetotall = 0;
		int orderId=0;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataTable dt = (e.Row.DataItem as DataRowView).DataView.Table;
            string sfID = Convert.ToString(dt.Rows[e.Row.RowIndex]["SF_Code"]);
            //orderId = Convert.ToInt32(dt.Rows[e.Row.RowIndex]["Stockist_Code"]);
            total += Convert.ToDecimal(dt.Rows[e.Row.RowIndex]["Total_Call"]);
            nttotal += Convert.ToDecimal(dt.Rows[e.Row.RowIndex]["Productive"]);
            ovtot += Convert.ToDecimal(dt.Rows[e.Row.RowIndex]["Nonproductive"]);
            disc_tot += Convert.ToDecimal(dt.Rows[e.Row.RowIndex]["Order_Value"]);
            //freetot += Convert.ToDecimal(dt.Rows[e.Row.RowIndex]["free"]);
            //Response.Write(gvtotalorder.Rows[0].Cells[2].Text);
            if (sfID != currentsfid)
            {
                if (e.Row.RowIndex > 0)
                {
                    for (int i = subTotalRowIndex; i < e.Row.RowIndex; i++)
                    {
                        //subTotal += Convert.ToDecimal(gvtotalorder.Rows[i].Cells[4].Text);
                        //subTotal += Convert.ToDecimal(((Label)gvtotalorder.Rows[i].FindControl("orderval")).Text);
                        //nettotal += Convert.ToDecimal(((Label)gvtotalorder.Rows[i].FindControl("netval")).Text);
                        //ovtotal += Convert.ToDecimal(((Label)gvtotalorder.Rows[i].FindControl("toval")).Text);
                        //disctprice += Convert.ToDecimal(((Label)gvtotalorder.Rows[i].FindControl("discountpri")).Text);
                        //freetotall += Convert.ToDecimal(((Label)gvtotalorder.Rows[i].FindControl("freept")).Text);
                    }
                    //this.AddTotalRow("Sub Total", subTotal.ToString("N2"));
                    //subTotalRowIndex = e.Row.RowIndex;
                }
                currentId = orderId;
                currentsfid = sfID;
            }
        }
    }
    private void AddTotalRow(string labelText, string value)
    {
        GridViewRow row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Normal);
        row.BackColor = ColorTranslator.FromHtml("#dbf7d9");
        row.CssClass = "subTot";
        row.Cells.AddRange(new TableCell[3] { new TableCell (), //Empty Cell
                                        new TableCell { Text = labelText, HorizontalAlign = HorizontalAlign.Right,ColumnSpan=5},
                                         
                                        
                                        new TableCell { Text = value, HorizontalAlign = HorizontalAlign.Right } });

        gvtotalorder.Controls[0].Controls.Add(row);
    }
    private void AddTotalRoww(string labelText, string value0, string value1, string value2, string value3,string value4)
    {
        GridViewRow row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Normal);
        //row.BackColor = ColorTranslator.FromHtml("#ecf19f");

        row.CssClass = "GrndTot";
        row.Cells.AddRange(new TableCell[7] { new TableCell (), //Empty Cell
                                        new TableCell { Text = labelText, HorizontalAlign = HorizontalAlign.Right,ColumnSpan=5},
                                           
                                        new TableCell { Text = value0, HorizontalAlign = HorizontalAlign.Right } ,
                                        new TableCell { Text = value1, HorizontalAlign = HorizontalAlign.Right } ,
                                        new TableCell { Text = value2, HorizontalAlign = HorizontalAlign.Right } ,
                                        new TableCell { Text = value3, HorizontalAlign = HorizontalAlign.Right } ,
                                        new TableCell { Text = value4, HorizontalAlign = HorizontalAlign.Right } ,
        
        });

        gvtotalorder.Controls[0].Controls.Add(row);
    }
    protected void OnDataBound(object sender, EventArgs e)
    {
        total = 0; nettotal = 0; nttotal = 0; ovtot = 0; disc_tot = 0;
        for (int i = subTotalRowIndex; i < gvtotalorder.Rows.Count; i++)
        {
            // subTotal += Convert.ToDecimal(gvtotalorder.Rows[i].Cells[4].Text);
            //subTotal += Convert.ToDecimal(((Label)gvtotalorder.Rows[i].FindControl("orderval")).Text);
            total += Convert.ToDecimal(((Label)gvtotalorder.Rows[i].FindControl("Total_Call")).Text);
            nttotal += Convert.ToDecimal(((Label)gvtotalorder.Rows[i].FindControl("Productive")).Text);
            ovtot += Convert.ToDecimal(((Label)gvtotalorder.Rows[i].FindControl("Nonproductive")).Text);
            disc_tot += Convert.ToDecimal(((Label)gvtotalorder.Rows[i].FindControl("Order_Value")).Text);
            
            // disctprice += Convert.ToDecimal(((Label)gvtotalorder.Rows[i].FindControl("discountpri")).Text);
            // freetotall += Convert.ToDecimal(((Label)gvtotalorder.Rows[i].FindControl("freept")).Text);
        }
        //this.AddTotalRow("Sub Total", subTotal.ToString("N2"));
        this.AddTotalRoww("Total", total.ToString(), nttotal.ToString(),Math.Round(((nttotal/(total == 0 ? 1 : total))*100), 2).ToString() + "%", ovtot.ToString(), disc_tot.ToString());
       
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
	
	protected void ddlsubdiv_SelectedIndexChanged(object sender, EventArgs e)
    {
        string sURL = string.Empty;
        string stURL = string.Empty;
        DataSet dsGV = new DataSet();
        DataSet dsGc = new DataSet();
        DCR dc = new DCR();

        string stCrtDtaPnt = string.Empty;

        int sudiv = Convert.ToInt32(ddlsubdiv.SelectedValue);

        dsGV = dc.view_Visited_Outlets_view_sub(divcode, Convert.ToString(sudiv), sfCode, Date);
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



    protected void btnClose_Click(object sender, EventArgs e)
    {

    }


}