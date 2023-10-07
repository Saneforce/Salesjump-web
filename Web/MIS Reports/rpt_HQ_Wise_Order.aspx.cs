using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Drawing;
using Bus_EReport;
using System.IO;
using System.Data;

public partial class MIS_Reports_rpt_HQ_Wise_Order : System.Web.UI.Page
{
    string divcode = string.Empty;
    string sfCode = string.Empty;
    string sfname = string.Empty;
    string stCrtDtaPnt = string.Empty;
    string Pdtcatcode = string.Empty;
    string Pdtcatname = string.Empty;
    string Date = string.Empty;
    string subdivision_code = "0";
    decimal catnwgt = 0;
    decimal catval = 0;
    int catquan = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        divcode = Session["Division_Code"].ToString().Replace(",", "");
        sfCode = Session["Sf_Code"].ToString();
        Date = Request.QueryString["Order_date"].ToString();
        // Date = Convert.ToString(Request.QueryString["Order_date"].ToString());
        //DateTime d1 = Convert.ToDateTime(Date);

        // Pdtcatcode = Request.QueryString["Product_Cat_Code"].ToString();
        // Pdtcatname = Request.QueryString["Product_Cat_Name"].ToString();
        lblHead.Text = "HQ detail";
        //Label2.Text = "Product Name : " + d1.ToString("dd-MM-yyyy");
        //Feild.Text = Pdtcatname;
        FillSF();
    }
    private void FillSF()
    {
        string sURL = string.Empty;
        string stURL = string.Empty;
        DataSet dsGc = new DataSet();
        DCR dc = new DCR();

        dsGc = dc.CATEGORY_VIEW_HQ_order(divcode, sfCode, Date, subdivision_code);
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

    protected void catOnDataBound(object sender, EventArgs e)
    {
        for (int i = 0; i < GridViewcat.Rows.Count; i++)
        {
            // subTotal += Convert.ToDecimal(gvtotalorder.Rows[i].Cells[4].Text);
            catnwgt += Convert.ToDecimal(((Label)GridViewcat.Rows[i].FindControl("net_weight")).Text);
            catquan += int.Parse(((Label)GridViewcat.Rows[i].FindControl("quantity")).Text);
            catval += Convert.ToDecimal(((Label)GridViewcat.Rows[i].FindControl("value")).Text);
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
                                        new TableCell { Text = labelText, HorizontalAlign = HorizontalAlign.Right,ColumnSpan=2},
                                           new TableCell { Text = catnwgtt, HorizontalAlign = HorizontalAlign.Right },
                                            new TableCell { Text = catquant, HorizontalAlign = HorizontalAlign.Right },
                                        new TableCell { Text = catvalt, HorizontalAlign = HorizontalAlign.Right }});
       GridViewcat.Controls[0].Controls.Add(row);      

    }
}