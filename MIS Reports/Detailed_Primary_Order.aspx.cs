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

public partial class MIS_Reports_Detailed_Primary_Order : System.Web.UI.Page
{
    string FDate = string.Empty;
    string TDate = string.Empty;
    string currentsfid = string.Empty;
    string sfCode = string.Empty;
    string sfname = string.Empty;
    string subdiv_code = string.Empty;
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
    decimal subTotal = 0;
    decimal nettotal = 0;
    decimal nttotal = 0;
    decimal total = 0;
    decimal catnwgt = 0;
    decimal catval = 0;
    int catquan = 0;
    int subTotalRowIndex = 0;
    int gridcnt = 0;
    DataSet dsGV = new DataSet();
    string sfID = string.Empty;
    decimal subTotal1 = 0;
    decimal nettotal1 = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        divcode = Session["div_code"].ToString();
        sfCode = Request.QueryString["sf_code"].ToString();
        sfname = Request.QueryString["Sf_Name"].ToString();
        //Date = Request.QueryString["Date"].ToString();
        subdiv_code = Request.QueryString["subdiv"].ToString();
        HiddenField1.Value = divcode;
        lblHead.Text = "Primary Order View " + Date;
        Feild.Text = sfname;
        if (divcode == "35")
        {

            TDate = Request.QueryString["ToDate"].ToString();
        }
        else
        {
            TDate = Request.QueryString["ToDate"].ToString();
            FDate = Request.QueryString["FromDate"].ToString();
        }

        FillSF();

    }
    public class DynamicTemplateField : ITemplate
    {

        public void InstantiateIn(Control container)
        {
            string divcode = "8";

            SalesForce SF = new SalesForce();
            DataSet ff = new DataSet();
            ff = SF.GetProduct_Name(divcode);
            int cnt = ff.Tables[0].Rows.Count;

        }
    }
    private void FillSF()
    {


        string sURL = string.Empty;
        string stURL = string.Empty;


        string stCrtDtaPnt = string.Empty;

        DataSet dsGc = new DataSet();
        DCR dc = new DCR();
        TemplateField temp1 = new TemplateField();  //Create instance of Template field


        temp1.ItemTemplate = new DynamicTemplateField(); //Set the properties **ItemTemplate** as the instance of DynamicTemplateField class.


        gvtotalorder.Columns.Add(temp1);

        dsGV = dc.view_total_primaryorder_view(divcode, sfCode, FDate, TDate, subdiv_code);

        if (dsGV.Tables[0].Rows.Count > 0)
        {
            dsGV.Tables[0].Columns.RemoveAt(0);
            dsGV.Tables[0].Columns.RemoveAt(0);
            dsGV.Tables[0].Columns.RemoveAt(0);
            dsGV.Tables[0].Columns.RemoveAt(0);

            dsGV.Tables[0].Columns.RemoveAt(8);
            dsGV.Tables[0].Columns.RemoveAt(8);
            dsGV.Tables[0].Columns.RemoveAt(8);          
            gridcnt = dsGV.Tables[0].Columns.Count;
            dsGV.Tables[0].Columns[6].SetOrdinal(gridcnt - 1);
            dsGV.Tables[0].Columns[6].SetOrdinal(gridcnt - 1);
            gvtotalorder.DataSource = dsGV;
            gvtotalorder.DataBind();
        }
        else
        {
            gvtotalorder.DataSource = null;
            gvtotalorder.DataBind();
        }

    }
    protected void OnRowCreated(object sender, GridViewRowEventArgs e)
    {

        subTotal1 = 0;
        nettotal1 = 0;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            DataTable dt = (e.Row.DataItem as DataRowView).DataView.Table;
            total += Convert.ToDecimal(dt.Rows[e.Row.RowIndex]["Order_value"]);
            nttotal += Convert.ToDecimal(dt.Rows[e.Row.RowIndex]["net_weight_value"]);
            sfID = Convert.ToString(dt.Rows[e.Row.RowIndex]["Sf_Name"]);
        }


        SalesForce SF = new SalesForce();
        DataSet ff = new DataSet();

        ff = SF.GetProduct_Name(divcode);

        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridView HeaderGrid = (GridView)sender;
            GridViewRow HeaderGridRow0 = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);


            TableHeaderCell HeaderCell = new TableHeaderCell();
            HeaderCell.Width = 25;
            HeaderCell.Height = 35;
            HeaderCell.Font.Bold = true;
            //HeaderCell.ApplyStyle.
            HeaderCell.BackColor = System.Drawing.Color.FromName("#496a9a");

            TableHeaderCell Distributor = new TableHeaderCell();
            HeaderCell.Width = 25;
            HeaderCell.Height = 35;
            HeaderCell.BackColor = System.Drawing.Color.FromName("#496a9a");
            HeaderCell.Text = "S.No";

            HeaderGridRow0.Cells.Add(HeaderCell);

            HeaderGridRow0.Cells.Add(HeaderCell);
            HeaderCell = new TableHeaderCell();
            HeaderCell.Width = 180;
            HeaderCell.Height = 35;
            HeaderCell.Attributes["style"] = "font: Courier";
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderCell.BackColor = System.Drawing.Color.FromName("#496a9a");
            HeaderCell.Text = "Order_date";
            //HeaderCell. = "Andalus";
            HeaderGridRow0.Cells.Add(HeaderCell);

            HeaderCell = new TableHeaderCell();
            HeaderCell.Width = 180;
            HeaderCell.Height = 35;
            HeaderCell.Attributes["style"] = "font: Courier";
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderCell.BackColor = System.Drawing.Color.FromName("#496a9a");
            HeaderCell.Text = "Distributor";
            //HeaderCell. = "Andalus";
            HeaderGridRow0.Cells.Add(HeaderCell);
            HeaderCell = new TableHeaderCell();
            HeaderCell.Width = 110;
            HeaderCell.Height = 35;
            HeaderCell.BackColor = System.Drawing.Color.FromName("#496a9a");
            HeaderCell.Text = "Order Taken By";
            HeaderCell.Attributes["style"] = "font: Courier";
            HeaderGridRow0.Cells.Add(HeaderCell);
            HeaderCell = new TableHeaderCell();
            HeaderCell.Width = 110;
            HeaderCell.Height = 35;
            HeaderCell.BackColor = System.Drawing.Color.FromName("#496a9a");
            HeaderCell.Text = "Territory";
            HeaderCell.Attributes["style"] = "font: Courier";
            HeaderGridRow0.Cells.Add(HeaderCell);
            HeaderCell = new TableHeaderCell();
            HeaderCell.Width = 110;
            HeaderCell.Height = 35;
            HeaderCell.BackColor = System.Drawing.Color.FromName("#496a9a");
            HeaderCell.Text = "Address";
            HeaderCell.Attributes["style"] = "font: Courier";
            HeaderGridRow0.Cells.Add(HeaderCell);


            foreach (DataRow drdoctor in ff.Tables[0].Rows)
            {
                try
                {
                    DataRow[] drp = dsGV.Tables[0].Select("[" + drdoctor["Product_Detail_Code"] + "]" + ">0");

                    if (drp.Length > 0)
                    {
                        HeaderCell = new TableHeaderCell();
                        HeaderCell.Height = 35;
                        HeaderCell.BackColor = System.Drawing.Color.FromName("#496a9a");

                        HeaderCell.Attributes["style"] = "font: Andalus";
                        HeaderCell.Attributes["style"] = "font: Bold";
                        HeaderCell.Text = drdoctor["Product_Detail_Name"].ToString();// +" ( " + drdoctor["Product_Detail_Code"] + ")";
                        HeaderGridRow0.Cells.Add(HeaderCell);
                    }
                }
                catch { }
            }
            HeaderCell = new TableHeaderCell();
            HeaderCell.Width = 110;
            HeaderCell.Height = 35;
            HeaderCell.BackColor = System.Drawing.Color.FromName("#496a9a");
            HeaderCell.Text = "Net weight";
            HeaderCell.Attributes["style"] = "font: Courier";
            HeaderGridRow0.Cells.Add(HeaderCell);
            HeaderCell = new TableHeaderCell();
            HeaderCell.Width = 110;
            HeaderCell.Height = 35;
            HeaderCell.BackColor = System.Drawing.Color.FromName("#496a9a");
            HeaderCell.Text = "Order Value";
            HeaderCell.Attributes["style"] = "font: Courier";
            HeaderGridRow0.Cells.Add(HeaderCell);


            gvtotalorder.Controls[0].Controls.AddAt(0, HeaderGridRow0);


        }

       


        if (sfID != currentsfid)
        {
            if (e.Row.RowIndex > 0)
            {
                for (int i = subTotalRowIndex; i < e.Row.RowIndex; i++)
                {
                    subTotal1 += Convert.ToDecimal(gvtotalorder.Rows[i].Cells[gridcnt].Text);
                    nettotal1 += Convert.ToDecimal(gvtotalorder.Rows[i].Cells[gridcnt - 1].Text);
                    // subTotal += Convert.ToDecimal(gvtotalorder.Rows[i].Cells[4].Text);
                    //subTotal += Convert.ToDecimal(((Label)gvtotalorder.Rows[i].FindControl("orderval")).Text);
                    //nettotal += Convert.ToDecimal(((Label)gvtotalorder.Rows[i].FindControl("netval")).Text);
                }
                this.AddTotalRow("Sub Total", nettotal1.ToString("N2"), subTotal1.ToString("N2"));
                subTotalRowIndex = e.Row.RowIndex;
            }
           // currentId = orderId;
            currentsfid = sfID;
        }
    }

        
    private void AddTotalRow(string labelText, string netvalue, string value)
    {
        GridViewRow row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Normal);
        row.BackColor = ColorTranslator.FromHtml("#dbf7d9");
        row.CssClass = "subTot";
        row.Cells.AddRange(new TableCell[4] { new TableCell (), //Empty Cell
                                        new TableCell { Text = labelText, HorizontalAlign = HorizontalAlign.Right,  CssClass="ff",ColumnSpan=gridcnt-3},
                                          new TableCell { Text = netvalue, HorizontalAlign = HorizontalAlign.Right },
                                        new TableCell { Text = value, HorizontalAlign = HorizontalAlign.Right } });

        gvtotalorder.Controls[0].Controls.Add(row);
    }
    private void AddTotalRoww(string labelText, string ntvalue, string value)
    {

        GridViewRow row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Normal);
        row.BackColor = ColorTranslator.FromHtml("#ecf19f");

        row.CssClass = "GrndTot";
        row.Cells.AddRange(new TableCell[4] { new TableCell (), //Empty Cell
                                        new TableCell { Text = labelText, HorizontalAlign = HorizontalAlign.Right, CssClass="ff",ColumnSpan=gridcnt-3},
                                          new TableCell { Text = ntvalue, HorizontalAlign = HorizontalAlign.Right },
                                        new TableCell { Text = value, HorizontalAlign = HorizontalAlign.Right } });

        gvtotalorder.Controls[0].Controls.Add(row);
    }
    protected void OnDataBound(object sender, EventArgs e)
    {
        subTotal = 0;
            nettotal = 0;
            if (gvtotalorder.Rows.Count > 0)
            {
                for (int i = subTotalRowIndex; i < gvtotalorder.Rows.Count; i++)
                {
              
                        subTotal += Convert.ToDecimal(gvtotalorder.Rows[i].Cells[gridcnt].Text);
                        nettotal += Convert.ToDecimal(gvtotalorder.Rows[i].Cells[gridcnt - 1].Text);
                        // subTotal += Convert.ToDecimal(gvtotalorder.Rows[i].Cells[4].Text);
                        //subTotal += Convert.ToDecimal(((Label)gvtotalorder.Rows[i].FindControl("orderval")).Text);
                        //nettotal += Convert.ToDecimal(((Label)gvtotalorder.Rows[i].FindControl("netval")).Text);
                  }
            this.AddTotalRow("Sub Total", nettotal.ToString("N2"), subTotal.ToString("N2"));
            this.AddTotalRoww("Total", nttotal.ToString("N2"), total.ToString("N2"));
             }
                
                      
        
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowIndex != -1)
        {
            e.Row.Cells[2].Visible = false;
            e.Row.Cells[0].Text = (e.Row.RowIndex + 1).ToString();
        }


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
}