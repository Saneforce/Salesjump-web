using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bus_EReport;
using System.Data;

public partial class MIS_Reports_rpt_PrimaryOrderVsSale : System.Web.UI.Page
{
    DataSet ds = null;
    string SF_Code = string.Empty;
    string year = string.Empty;
    string month = string.Empty;
    string Mgr_name = string.Empty;
    public   string div = string.Empty;  
    protected void Page_Load(object sender, EventArgs e)
    {
            
        year = Request.QueryString["year"].ToString();
        month = Request.QueryString["month"].ToString();
        if (Request.QueryString["div"].ToString() != "0")
        {
            div = Request.QueryString["div"].ToString();
        }
        else {
            div = Session["div_code"].ToString();
                }
        Mgr_name = Request.QueryString["Mgr_name"].ToString();
        if (Convert.ToString(Request.QueryString["SF_Code"].ToString()) == "0")
            SF_Code = Request.QueryString["Mgr_code"].ToString();
        else
            SF_Code = Request.QueryString["SF_Code"].ToString();
        fillgvdata();
            }
    private void fillgvdata()
    {
        SalesForce sf = new SalesForce();

        ds = sf.priordercnf(div, SF_Code, month, year);
        if(ds.Tables[0].Rows.Count>0)
        {
            gvdata.DataSource = ds;
            gvdata.DataBind();
            decimal total = ds.Tables[0].AsEnumerable().Sum(row => Convert.ToDecimal(row.Field<Double>("order_value")));
            gvdata.FooterRow.Cells[1].Text = "Total";
            gvdata.FooterRow.Cells[1].ColumnSpan = 4;
            gvdata.FooterRow.Cells[1].HorizontalAlign = HorizontalAlign.Right;
            gvdata.FooterRow.Cells[2].Text = total.ToString("N2");
            gvdata.FooterRow.Cells.RemoveAt(3);
            gvdata.FooterRow.Cells.RemoveAt(3);
            gvdata.FooterRow.Cells.RemoveAt(3);
            gvdata.FooterRow.Cells.RemoveAt(3);
        }
    }
}