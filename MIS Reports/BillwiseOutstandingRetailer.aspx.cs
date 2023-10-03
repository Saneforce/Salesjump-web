using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


using System.Data;
using Bus_EReport;
public partial class MIS_Reports_BillwiseOutstandingRetailer : System.Web.UI.Page
{
    #region "Declaration"
    string DivCode = string.Empty;
    string SFCode = string.Empty;
    string SFName = string.Empty;
    string FYear = string.Empty;
    string FMonth = string.Empty;
    string Remark = string.Empty;
    string SubDivCode = string.Empty;
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        DivCode = Session["div_code"].ToString();
        SFCode = Request.QueryString["SF_Code"].ToString();
        SFName = Request.QueryString["SF_Name"].ToString();
        SubDivCode = Request.QueryString["Sub_Div"].ToString();
        Label1.Text = "Field Force : " + SFName;
        GetRetailrs();
    }
    private void GetRetailrs()
    {
        Invoice iv = new Invoice();
        DataSet dsPenBill = iv.getPendingBillRetailer(SFCode);
        GVRetailer.DataSource = dsPenBill;
        GVRetailer.DataBind();
	
		if (dsPenBill.Tables[0].Rows.Count > 0)
        {

	        GVRetailer.FooterRow.Cells[0].Text = "Total";
	        decimal total = 0;
	
	        for (int k = 6; k < dsPenBill.Tables[0].Columns.Count; k++)
	        {
	            total = dsPenBill.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>(dsPenBill.Tables[0].Columns[k].ToString()));
	            GVRetailer.FooterRow.Cells[k - 1].Text = total.ToString("N2");
	            GVRetailer.FooterRow.Cells[k - 1].HorizontalAlign = HorizontalAlign.Right;
	
	        }
	        GVRetailer.FooterRow.Cells[0].ColumnSpan = 4;
	        GVRetailer.FooterRow.Cells[1].Visible = false;
	        GVRetailer.FooterRow.Cells[2].Visible = false;
	        GVRetailer.FooterRow.Cells[3].Visible = false;
		}
    }
}