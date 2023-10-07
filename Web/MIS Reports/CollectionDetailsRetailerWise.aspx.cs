using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


using System.Data;
using Bus_EReport;
public partial class MIS_Reports_CollectionDetailsRetailerWise : System.Web.UI.Page
{
    #region "Declaration"
    string DivCode = string.Empty;
    string SFCode = string.Empty;
    string SFName = string.Empty;
    string FYear = string.Empty;
    string FMonth = string.Empty;
    string Remark = string.Empty;
    string FromDate = string.Empty;
    string ToDate = string.Empty;
    string SubDivCode = string.Empty;
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        DivCode = Session["div_code"].ToString();
        SFCode = Request.QueryString["SF_Code"].ToString();
        SFName = Request.QueryString["SF_Name"].ToString();
        SubDivCode = Request.QueryString["Sub_Div"].ToString();
        FromDate = Request.QueryString["FromDt"].ToString();
        ToDate = Request.QueryString["ToDt"].ToString();
        Label1.Text = "Field Force : " + SFName;
        GetRetailrs();
    }
    private void GetRetailrs()
    {
        Invoice iv = new Invoice();

        //  string FromDt = "28/07/2018";
        // string ToDt = "28/07/2018";
        // string SubDiv = "0";

        DataSet dsPenBill = iv.getCollectionDetails(DivCode, SFCode, Convert.ToDateTime(FromDate).ToString("yyyy/MM/dd"), Convert.ToDateTime(ToDate).ToString("yyyy/MM/dd"), SubDivCode);
        GVRetailer.DataSource = dsPenBill;
        GVRetailer.DataBind();
       
        decimal total = 0;

        for (int k = 6; k < dsPenBill.Tables[0].Columns.Count; k++)
        {
            total = dsPenBill.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>(dsPenBill.Tables[0].Columns[k].ToString()));
            GVRetailer.FooterRow.Cells[k - 2].Text = total.ToString("N2");
            GVRetailer.FooterRow.Cells[k - 2].HorizontalAlign = HorizontalAlign.Right;

        }
        if (dsPenBill.Tables[0].Columns.Count > 0)
        {
            GVRetailer.FooterRow.Cells[0].Text = "Total";
            GVRetailer.FooterRow.Cells[0].ColumnSpan = 4;
            GVRetailer.FooterRow.Cells[1].Visible = false;
            GVRetailer.FooterRow.Cells[2].Visible = false;
            GVRetailer.FooterRow.Cells[3].Visible = false;
            // GVRetailer.FooterRow.Cells[4].Visible = false;
        }
    }
}