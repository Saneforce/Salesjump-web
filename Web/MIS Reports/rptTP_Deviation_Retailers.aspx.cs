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

using System.Web.Services;

using System;
using System.IO;
using System.Globalization;

public partial class MIS_Reports_rptTP_Deviation_Retailers : System.Web.UI.Page
{
    string DivCode = string.Empty;
    string SFCode = string.Empty;
    string Dstes = string.Empty;
    string rType = string.Empty;
    string oType = string.Empty;
    string SFName = string.Empty;
    string phType = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        DivCode = Session["div_code"].ToString();
        SFCode = Request.QueryString["sfcode"].ToString();
        Dstes = Request.QueryString["FDate"].ToString();
        oType = Request.QueryString["oType"].ToString();
        rType = Request.QueryString["rType"].ToString();
        //  SFName = Request.QueryString["SFName"].ToString();
        try
        {
            phType =  Request.QueryString["phType"].ToString();
        }
        catch (Exception ex )
        {
            phType = "0";
        }
        if (oType == "Sec")
        {
            Label1.Text = " Secondary  TP - Deviation for " + Dstes;
        }
        else
        {
            Label1.Text = " Primary TP - Deviation for " + Dstes;
        }


        //   Label2.Text = "Field Force  : " + SFName;
        loadData();

    }

    private void loadData()
    {
        Order tpn = new Order();
       // DateTime date = DateTime.ParseExact(Dstes, "dd/MM/yyyy",
          //                          CultureInfo.InvariantCulture);
        DataSet dsPro = tpn.GetTPDeivationRetailers(SFCode, Dstes, oType, rType, phType);

        if (dsPro.Tables[0].Rows.Count > 0)
        {
            if (oType == "Sec")
            {
                grdData.DataSource = dsPro;
                grdData.DataBind();
                decimal total = dsPro.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("Order_Value"));
                grdData.FooterRow.Cells[1].Text = "Total";
                grdData.FooterRow.Cells[1].Font.Bold = true;
                grdData.FooterRow.Cells[1].HorizontalAlign = HorizontalAlign.Left;
                grdData.FooterRow.Cells[6].HorizontalAlign = HorizontalAlign.Right;
                grdData.FooterRow.Cells[6].Text = total.ToString("N2");
                grdData.FooterRow.Cells[6].Font.Bold = true;
            }
            else
            {

                GridView1.DataSource = dsPro;
                GridView1.DataBind();
                decimal total = dsPro.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("Order_Value"));
                GridView1.FooterRow.Cells[1].Text = "Total";
                GridView1.FooterRow.Cells[1].Font.Bold = true;
                GridView1.FooterRow.Cells[1].HorizontalAlign = HorizontalAlign.Left;
                GridView1.FooterRow.Cells[4].HorizontalAlign = HorizontalAlign.Right;
                GridView1.FooterRow.Cells[4].Text = total.ToString("N2");
                GridView1.FooterRow.Cells[4].Font.Bold = true;
            }
        }
        else
        {
            if (oType == "Sec")
            {
                grdData.DataSource = null;
                grdData.DataBind();
            }
            else
            {

                GridView1.DataSource = null;
                GridView1.DataBind();
            }
        }
    }
}