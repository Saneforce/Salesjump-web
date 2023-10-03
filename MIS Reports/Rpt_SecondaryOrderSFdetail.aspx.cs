using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bus_EReport;
using System.Data;
using System.Web.Services;
using Newtonsoft.Json;

public partial class MIS_Reports_Rpt_SecondaryOrderSFdetail : System.Web.UI.Page
{
    DataSet ds = null;
    static string sf_code = string.Empty;
    static string month = string.Empty;
    static string year = string.Empty;
    static string flag= string.Empty;
    //string Sf_Name = string.Empty;
    //string Stockist_Name = string.Empty;
    //string Trans_Sl_No = string.Empty;
    //string order_value = string.Empty;
    static string div = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {

        sf_code = Request.QueryString["sf_code"].ToString();
        month = Request.QueryString["month"].ToString();
        year = Request.QueryString["year"].ToString();
        flag = Request.QueryString["flag"].ToString();
        div = Session["div_code"].ToString();
        //Sf_Name = Request.QueryString["Sf_Name"].ToString();
        //txtsfname.Text = Request.QueryString["Sf_Name"].ToString();
        //txtstockist.Text = Request.QueryString["Stockist_Name"].ToString();
        //Stockist_Name = Request.QueryString["Stockist_Name"].ToString();
        //Trans_Sl_No = Request.QueryString["Trans_Sl_No"].ToString();
        //order_value = Request.QueryString["order_value"].ToString();
        //if (Request.QueryString["div"].ToString() != "0")
        //{
        //    div = Request.QueryString["div"].ToString();
        //}
        //else
        //{
        //    div = Session["div_code"].ToString();
        //}
        //fillgvdata();
        var N = int.Parse(month);
        if (flag == "1")
        {
            lblHead.Text = "Confirmed order for the month " + getAbbreviatedName(N) + " year  " + year;
        }
        else
        {
            lblHead.Text = "Rejected order for the month " + getAbbreviatedName(N) + " year  " + year;
        }
    }
    //private void fillgvdata()
    //{
    //    SalesForce sf = new SalesForce();

    //    ds = sf.Secordercnfdetail(div, SF_Code, Trans_Sl_No);
    //    if (ds.Tables[0].Rows.Count > 0)
    //    {
    //        gvdata.DataSource = ds;
    //        gvdata.DataBind();
    //    }
    //}
    static string getAbbreviatedName(int month)
    {
        DateTime date = new DateTime(2020, month, 1);

        return date.ToString("MMM");
    }
    [WebMethod]
    public static string getorderdetSF(string Div)
    {
        DataSet ds = null;
        string strQry = string.Empty;

        ds = getorderdetails(Div, sf_code, month, year,flag );

        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    public static DataSet getorderdetails(string divcode, string SF_Code, string month, string year ,string flag)
    {
        string strQry = string.Empty;
        DataSet dsRemark = null;

        DBase_EReport.DB_EReporting db_ER = new DBase_EReport.DB_EReporting();

        //strQry = "Select oh.Sf_Code,SF_Name,Convert(Date,Order_Date)Order_Date,od.Trans_Sl_No,Stockist_Name,(ListedDr_Name)Retailer_Name,od.value Order_value ,Remarks rejRemarks,od.Order_Flag from Trans_Order_Head oh inner join trans_order_details od on oh.trans_sl_no=od.trans_sl_no " +
        //         "inner join Mas_Stockist ms on oh.Stockist_Code = ms.Stockist_Code inner join Mas_ListedDr dr on dr.ListedDrCode = oh.Cust_Code inner join Mas_Salesforce sa on sa.Sf_Code=oh.Sf_Code " +
         //        "where oh.Sf_Code =  '" + sf_code + "' and month(order_date)= '" + month + "' and year(Order_date)= '" + year + "' and od.Order_Flag = '"+ flag +"' and od.Div_ID = '" + divcode + "'";
		strQry ="Select oh.Sf_Code,SF_Name,Convert(Date,Order_Date)Order_Date,Trans_Sl_No,Stockist_Name,(ListedDr_Name)Retailer_Name,Order_value ,Order_Flag,rejRemarks from Trans_Order_Head oh " +
                 "inner join Mas_Stockist ms on oh.Stockist_Code = ms.Stockist_Code inner join Mas_ListedDr dr on dr.ListedDrCode = oh.Cust_Code inner join Mas_Salesforce sa on sa.Sf_Code=oh.Sf_Code " +
                 "where oh.Sf_Code =  '" + sf_code + "' and month(order_date)= '" + month + "' and year(Order_date)= '" + year + "' and Order_Flag = '"+ flag +"' and Div_ID = '" + divcode + "'";
        try
        {
            dsRemark = db_ER.Exec_DataSet(strQry);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return dsRemark;
    }
   
}