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
using Bus_EReport;
using System.Net;
using iTextSharp.tool.xml;
using System.Web.UI.HtmlControls;
public partial class MIS_Reports_rpt_emp_order_valueDAYwise : System.Web.UI.Page
{
    #region "Declaration"
    public int div_code;
    public string territorycode = string.Empty;
    public string sfCode = string.Empty;
    DataSet dsSalesForce = new DataSet();
    string Stockist_Code = string.Empty;
    public string sfname = string.Empty;
    public string Stockist_name = string.Empty;
    public  string year = string.Empty;
    int years;
    decimal subTotal = 0;
    decimal nettotal = 0;
    decimal ovtotal = 0;
    decimal disctprice = 0;
    decimal freetotall = 0;
    decimal total = 0;
    decimal nttotal = 0;
    decimal ovtoovtottal = 0;
    decimal ovtot = 0;
    decimal disc_tot = 0;
    string currentsfid = string.Empty;

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        div_code =Convert.ToInt16(Session["div_code"]);
        sfCode = Request.QueryString["Sf_Code"].ToString();
        Stockist_Code = Request.QueryString["Stockist_Code"].ToString();
        sfname = Request.QueryString["Sf_name"].ToString();
        Stockist_name = Request.QueryString["Stockist_Name"].ToString();
        year = Request.QueryString["Year"].ToString();
        Feild.Text = sfname;
        Label2.Text = Stockist_name;
        years = Convert.ToInt16(Request.QueryString["Year"]);
        loadData();
    }
    private void loadData()
    {
        

        
        SalesForce SF = new SalesForce();

       DataSet dsCounts = new DataSet();
        dsCounts = SF.RetailersDetailsDAYwiselist(sfCode, div_code, Stockist_Code, years);
        
        gvtotalorder.DataSource = dsCounts;
        gvtotalorder.DataBind();

    }

   /* protected void OnRowCreated(object sender, GridViewRowEventArgs e)
    {
        subTotal = 0;
        nettotal = 0;
        ovtotal = 0;
        disctprice = 0;
        freetotall = 0;
        int orderId = 0;
       
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataTable dt = (e.Row.DataItem as DataRowView).DataView.Table;
            string sfID = Convert.ToString(dt.Rows[e.Row.RowIndex]["SF_Code"]);
            //orderId = Convert.ToInt32(dt.Rows[e.Row.RowIndex]["Stockist_Code"]);
            total += Convert.ToDecimal(dt.Rows[e.Row.RowIndex]["net_weight_value"]);
            nttotal += Convert.ToDecimal(dt.Rows[e.Row.RowIndex]["free"]);
            ovtot += Convert.ToDecimal(dt.Rows[e.Row.RowIndex]["Totalvalue"]);
            disc_tot += Convert.ToDecimal(dt.Rows[e.Row.RowIndex]["discount_price"]);
            freetotall += Convert.ToDecimal(dt.Rows[e.Row.RowIndex]["Order_value"]);
            //Response.Write(gvtotalorder.Rows[0].Cells[2].Text);
            if (sfID != currentsfid)
            {
                if (e.Row.RowIndex > 0)
                {
                    for (int i = subTotalRowIndex; i < e.Row.RowIndex; i++)
                    {
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
                                        new TableCell { Text = labelText, HorizontalAlign = HorizontalAlign.Right,ColumnSpan=4},


                                        new TableCell { Text = value, HorizontalAlign = HorizontalAlign.Right } });

        gvtotalorder.Controls[0].Controls.Add(row);
    }
    private void AddTotalRoww(string labelText, string value0, string value1, string value2, string value3)
    {
        GridViewRow row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Normal);
        //row.BackColor = ColorTranslator.FromHtml("#ecf19f");

        row.CssClass = "GrndTot";
        row.Cells.AddRange(new TableCell[6] { new TableCell (), //Empty Cell
                                        new TableCell { Text = labelText, HorizontalAlign = HorizontalAlign.Right,ColumnSpan=4},

                                        new TableCell { Text = value0, HorizontalAlign = HorizontalAlign.Right } ,
                                        new TableCell { Text = value1, HorizontalAlign = HorizontalAlign.Right } ,
                                        new TableCell { Text = value2, HorizontalAlign = HorizontalAlign.Right } ,
                                        new TableCell { Text = value3, HorizontalAlign = HorizontalAlign.Right } ,


        });

        gvtotalorder.Controls[0].Controls.Add(row);
    }
    protected void OnDataBound(object sender, EventArgs e)
    {
        for (int i = subTotalRowIndex; i < gvtotalorder.Rows.Count; i++)
        {
            // subTotal += Convert.ToDecimal(gvtotalorder.Rows[i].Cells[4].Text);
            //subTotal += Convert.ToDecimal(((Label)gvtotalorder.Rows[i].FindControl("orderval")).Text);
            // nettotal += Convert.ToDecimal(((Label)gvtotalorder.Rows[i].FindControl("netval")).Text);
            //ovtotal += Convert.ToDecimal(((Label)gvtotalorder.Rows[i].FindControl("toval")).Text);
            // disctprice += Convert.ToDecimal(((Label)gvtotalorder.Rows[i].FindControl("discountpri")).Text);
            // freetotall += Convert.ToDecimal(((Label)gvtotalorder.Rows[i].FindControl("freept")).Text);
        }
        //this.AddTotalRow("Sub Total", subTotal.ToString("N2"));
        this.AddTotalRoww("Total", total.ToString(), nttotal.ToString(), ovtot.ToString(), disc_tot.ToString());

    }
    */



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


    protected void gvtotalorder_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
}
