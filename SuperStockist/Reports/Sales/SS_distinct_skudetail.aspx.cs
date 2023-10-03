using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
using System.Data.SqlClient;
using System.Web.Services;
using System.Configuration;
using Newtonsoft.Json;
using System.Globalization;
using ClosedXML.Excel;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System.Xml;
using System.IO;
using System.Data.OleDb;
using DBase_EReport;

public partial class SuperStockist_Reports_Sales_distinct_skudetail : System.Web.UI.Page
{
   public static string div_code = string.Empty;
    public static string sf_code = string.Empty;
    public static string sf_type = string.Empty;
    protected override void OnPreInit(EventArgs e)
    {
        base.OnPreInit(e);
        sf_type = Session["sf_type"].ToString();
        if (sf_type == "3" || sf_type == "2" || sf_type == "1")
        {
            this.MasterPageFile = "~/MasterForAll.master";
        }
        if (sf_type == "4")
        {
            this.MasterPageFile = "~/Master_DIS.master";
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString().TrimEnd(',');
        sf_code = Session["sf_code"].ToString();
    }
    [WebMethod]
    public static string binddistributor(string sf_code, string Div)
    {
        DataSet ds = new DataSet();
        ds = getDistributordetails(sf_code, Div.TrimEnd(','));
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }

    public static DataSet getDistributordetails(string sf_code, string Div_Code)
    {
        string strQry = string.Empty;
        DataSet ds = null;
        DBase_EReport.DB_EReporting db_ER = new DBase_EReport.DB_EReporting();

        strQry = "select S_NO Stockist_Code,S_Name Stockist_Name,ERP_Code from supplier_master   where Division_Code = '" + Div_Code + "' and S_NO='" + sf_code + "' order by S_Name";

        try
        {
            ds = db_ER.Exec_DataSet(strQry);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return ds;
    }
    public static DataTable generateSecondaryExcel(string divcode, string sf_code, string FDate, string TDate)
    {
        prexc dc = new prexc();
        DataSet ss = null;
        DataSet dsGV = null;


        ss = dc.dist_skusales_qty(divcode, sf_code, FDate, TDate);
        dsGV = dc.dist_skusales_user(divcode, sf_code, FDate, TDate);

        //DataSet ss = dc.dist_skusales_qty(divcode, sf_code, FDate, TDate); 
        //DataSet dsGV = dc.dist_skusales_user(divcode, sf_code, FDate, TDate);
        DataTable dtOrderHead = new DataTable();
        DataTable dtOrderDetails = new DataTable();
        DataTable dtOrders = new DataTable();
        //dtOrders.Columns.Add("Distributor Name", typeof(string));
        dtOrders.Columns.Add("Distributor Name", typeof(string));


        var ProductNames = (from row in ss.Tables[0].AsEnumerable()
                            orderby row.Field<string>("Product_Detail_Name")
                            select new
                            {
                                Product_Code = row.Field<string>("Product_Code"),
                                Product_Name = row.Field<string>("Product_Detail_Name")
                            }).Distinct().ToList();

        foreach (var str in ProductNames)
        {
            dtOrders.Columns.Add(str.Product_Name.ToString(), typeof(double));
        }

        foreach (DataRow dr in dsGV.Tables[0].Rows)
        {
            DataRow rw = dtOrders.NewRow();
            string transSlNO = dr["ListedDrCode"].ToString();
            // rw["Distributor Name"] = dr["Stockist_name"].ToString();
            rw["Distributor Name"] = dr["ListedDr_Name"].ToString();

            DataRow[] drp = ss.Tables[0].Select("Cus_Code='" + transSlNO + "'");
            for (int i = 0; i < drp.Length; i++)
            {
                rw[(drp[i]["Product_Detail_Name"].ToString())] =
                    Convert.ToDouble((string.IsNullOrEmpty(Convert.ToString(rw[drp[i]["Product_Detail_Name"].ToString()]))) ? 0 : rw[(drp[i]["Product_Detail_Name"].ToString())]) + Convert.ToDouble(drp[i]["Quantity"]);
            }
            dtOrders.Rows.Add(rw);
        }
        return dtOrders;
    }
    public class prexc
    {
        public DataSet dist_skusales_user(string div, string sf_code, string Mn, string Yr)
        {
            DBase_EReport.DB_EReporting db_ER = new DBase_EReport.DB_EReporting();
            DataSet dsAdmin = null;

            var strQry = "exec [dist_SS_skusales_user] '" + div + "','" + sf_code + "','" + Mn + "','" + Yr + "'";
            //var strQry = "exec [dist_skusales_user] '" + div + "','" + sf_code + "','" + Mn + "','" + Yr + "'";

            try
            {
               

                dsAdmin = db_ER.Exec_DataSet(strQry);
               
            }

            catch (Exception ex)
            {
                throw ex;
            }
            return dsAdmin;
        }
        public DataSet dist_skusales_qty(string div, string sf_code, string Mn, string Yr)
        {
            DBase_EReport.DB_EReporting db_ER = new DBase_EReport.DB_EReporting();
            DataSet dsAdmin = null;

            var strQry = "exec [dist_SS_skusales_qty] '" + div + "','" + sf_code + "','" + Mn + "','" + Yr + "'";
            //var strQry = "exec [dist_skusales_qty] '" + div + "','" + sf_code + "','" + Mn + "','" + Yr + "'";

            try
            {
                dsAdmin = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsAdmin;
        }
    }
    [WebMethod]
    protected void exceldld_Click(object sender, EventArgs e)
    {
        DataTable ot = null;
        //if (sf_type != "4")
        //{
        //    ot = generateSecondaryExcel(div_code, stk_code.Value.ToString(), hfdt.Value.ToString(), htdt.Value.ToString());
        //}
        //else
            ot = generateSecondaryExcel(div_code, sf_code, hfdt.Value.ToString(), htdt.Value.ToString());
        using (XLWorkbook wb = new XLWorkbook())
        {
            wb.Worksheets.Add(ot, "Exception List");
            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=Retailer_Distinct_SkuWise.xlsx");
            using (MemoryStream MyMemoryStream = new MemoryStream())
            {
                wb.SaveAs(MyMemoryStream);
                MyMemoryStream.WriteTo(Response.OutputStream);
                Response.Flush();
                Response.End();
            }

        }

    }
    [WebMethod]
    public static string VIEW_STOCK(string sf_code ,string hfdt, string htdt)
    {
        DataTable ot = null;
        //if (sf_type != "4")
        //{
        //    ot = generateSecondaryExcel(div_code, stk_code.Value.ToString(), hfdt.Value.ToString(), htdt.Value.ToString());
        //}
        //else
        ot = generateSecondaryExcel(div_code, sf_code, hfdt, htdt);
        return JsonConvert.SerializeObject(ot);

    }
}