using Bus_EReport;
using ClosedXML.Excel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Web.UI.HtmlControls;
using System.Drawing;

public partial class MIS_Reports_Rpt_Secondary_Order : System.Web.UI.Page
{
    public static string divcode = string.Empty;
    public static string Sf_Code = string.Empty;
    public string sfname = string.Empty;
    public string subdiv_code = string.Empty;
    public static string statecode = "0";
    public static string TDate = string.Empty;
    public static string FDate = string.Empty;
    decimal catnwgt = 0;
    decimal catval = 0;
    int catquan = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        divcode = Session["div_code"].ToString();
        Sf_Code = Request.QueryString["sf_code"].ToString();
        sfname = Request.QueryString["Sf_Name"].ToString();
        subdiv_code = Request.QueryString["subdiv"].ToString();
		statecode= Request.QueryString["state"].ToString();
        //Feild.Text = sfname;
        if (divcode == "35")
        {
            TDate = Request.QueryString["ToDate"].ToString();
        }
        else
        {
            TDate = Request.QueryString["ToDate"].ToString();
            FDate = Request.QueryString["FromDate"].ToString();
        }
        DateTime d1 = Convert.ToDateTime(FDate);
        DateTime d2 = Convert.ToDateTime(TDate);
        FillSF();
        lblHead.Text = "Secondary Order View of  "+ sfname + "  from " + d1.ToString("dd-MM-yyyy") + " to " + d2.ToString("dd-MM-yyyy");
    }

    [WebMethod]
    public static string GetRetDetails()
    {
        string msg = string.Empty;
        DataSet ds = null;
        DataTable dtt = new DataTable();
        DCR dc = new DCR();
        if (FDate == TDate)
        {
            ds = dc.getretdetsaa(Sf_Code, divcode, FDate, TDate);
            dtt = ds.Tables[0];
        }
        return JsonConvert.SerializeObject(dtt);
    }
    private void FillSF()
    {
        string sURL = string.Empty;
        string stURL = string.Empty;
        DataSet dsGV = new DataSet();
        DataSet dsGc = new DataSet();
        DCR dc = new DCR();


        string stCrtDtaPnt = string.Empty;
        //if (sf_type == "4")
        //{
        SalesForce SF = new SalesForce();
        
        dsGc = dc.view_total_order_view_categorywise1(divcode, Sf_Code, FDate, TDate, subdiv_code);

        if (dsGc.Tables[0].Rows.Count > 0)
        {
            //dsGc.Tables[0].Columns.RemoveAt(1);
            //dsGc.Tables[0].Columns.RemoveAt(3);
            //dsts.Tables[0].Columns.RemoveAt(1);
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
    [WebMethod]
    public static string GetOrderGroup(string divcode, string SF, string FDt, string TDt, string subdiv)
    {
        Product sf = new Product();
        DataSet ds = sf.getProdGrpName(divcode, SF, FDt, TDt, subdiv,statecode);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    [WebMethod]
    public static string GetOrderProducts(string divcode, string SF, string FDt, string TDt, string subdiv)
    {
        SalesForce sf = new SalesForce();
        DataSet ds = sf.GetProduct_total_forcompany(divcode, FDt, TDt, subdiv,statecode);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    [WebMethod]
    public static string GetOrderQty(string divcode, string SF, string FDt, string TDt, string subdiv)
    {
        DCR sf = new DCR();
        DataSet ds = sf.secordernewqty(divcode, SF, FDt, TDt, subdiv,statecode);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    [WebMethod]
    public static string GetOrderHead(string divcode, string SF, string FDt, string TDt, string subdiv)
    {
        DCR sf = new DCR();
        DataSet ds = sf.secneworderview(divcode, SF, FDt, TDt, subdiv,statecode);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    [WebMethod]
    public static string GetUOM(string divcode, string SF, string FDt, string TDt, string subdiv)
    {
        Product sf = new Product();
        DataSet ds = sf.getProdUOM(divcode);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }    protected void catOnDataBound(object sender, EventArgs e)
    {
        for (int i = 0; i < GridViewcat.Rows.Count; i++)
        {

            catnwgt += Convert.ToDecimal(((Label)GridViewcat.Rows[i].FindControl("lnetweight")).Text);
            catquan += Int32.Parse(((Label)GridViewcat.Rows[i].FindControl("lquantity")).Text);
            catval += Convert.ToDecimal(((Label)GridViewcat.Rows[i].FindControl("cval")).Text);

        }
        //this.AddTotalRow("Sub Total", ovtotal.ToString("N2"), disctprice.ToString("N2"), freetotall.ToString("N2"), nettotal.ToString("N2"), subTotal.ToString("N2"));
        this.AddTotalRowwcat("Total", catnwgt.ToString("N2"), catquan.ToString(""), catval.ToString("N2"));
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
                                        new TableCell { Text = catvalt, HorizontalAlign = HorizontalAlign.Right }});


        GridViewcat.Controls[0].Controls.Add(row);
    }
    protected void btnExcel_Click(object sender, EventArgs e)
    {
        string attachment = "attachment; filename=Secondary_Order_View.xls";
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