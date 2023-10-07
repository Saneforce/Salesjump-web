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
using iTextSharp.tool.xml;
using System.Text;
using Bus_EReport;
using System.Net;
using System.Web.Services;
using Newtonsoft.Json;
using DBase_EReport;

public partial class MIS_Reports_new_retailer_distributorwise : System.Web.UI.Page
{
    public static string sfCode = string.Empty;
    public static string sf_type = string.Empty;
    public static string divcode = string.Empty;
    public static string FMonth = string.Empty;
    public static string FYear = string.Empty;
    public static string TMonth = string.Empty;
    public static string TYear = string.Empty;
    public static string subdivision_code = string.Empty;
    public static string stcode = string.Empty;
    public static string stname = string.Empty;
    public static string FromDate = string.Empty;
    public static string ToDate = string.Empty;


    protected void Page_Load(object sender, EventArgs e)
    {
        sfCode = Session["sf_code"].ToString();
        sf_type = Session["sf_type"].ToString();
        divcode = Session["div_code"].ToString();
        FMonth = Request.QueryString["FMonth"].ToString();
        FYear = Request.QueryString["FYear"].ToString();
        TMonth = Request.QueryString["TMonth"].ToString();
        TYear = Request.QueryString["TYear"].ToString();
        subdivision_code = Request.QueryString["subdivision"].ToString();
        stcode = Request.QueryString["state"].ToString();
        stname = Request.QueryString["vstate"].ToString();
        FromDate = Request.QueryString["Fdates"].ToString();
        ToDate = Request.QueryString["Tdates"].ToString();

        System.Globalization.DateTimeFormatInfo mfi = new System.Globalization.DateTimeFormatInfo();
        //string strFMonthName = mfi.GetMonthName(Convert.ToInt16(FMonth)).ToString().Substring(0, 3);
        //string strTMonthName = mfi.GetMonthName(Convert.ToInt16(TMonth)).ToString().Substring(0, 3);
        lblHead.Text = "Distributor Wise Order Value for  " + FromDate + " To " + ToDate;
    }
    [WebMethod(EnableSession = true)]
    public static string Filldtl()
    {
        newrtl sf = new newrtl();
        DataSet dsProd = sf.Get_retail_dis();
        return JsonConvert.SerializeObject(dsProd.Tables[0]);
    }
    [WebMethod(EnableSession = true)]
    public static string Fillcat()
    {
        newrtl sf = new newrtl();
        DataSet dsProd = sf.Get_retail_dis_cat();
        return JsonConvert.SerializeObject(dsProd.Tables[0]); 
    }
    [WebMethod(EnableSession = true)]
    public static string Fillprodcnt()
    {
        newrtl sf = new newrtl();
        DataSet dsProd = sf.Get_retail_prodcnt();
        return JsonConvert.SerializeObject(dsProd.Tables[0]);
    }
    [WebMethod(EnableSession = true)]
    public static string Fillroutecnt()
    {
        newrtl sf = new newrtl();
        DataSet dsProd = sf.Get_retail_routecount();
        return JsonConvert.SerializeObject(dsProd.Tables[0]);
    }
    [WebMethod(EnableSession = true)]
    public static string Fillretaildetail(string stckcode,string prdcode)
    {
        newrtl sf = new newrtl();
        DataSet dsProd = sf.Get_retail_list(stckcode, prdcode);
        return JsonConvert.SerializeObject(dsProd.Tables[0]);
    }
    public class newrtl
    {
        public DataSet Get_retail_dis()
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            //string strQry = "exec testGET_Retailer_Distributor_Wise '" + divcode + "','" + sfCode + "','" + FYear + "','" + TYear + "','" + FMonth + "','" + TMonth + "'," + stcode + ",'" + subdivision_code + "'";
            string strQry = "exec testGET_Retailer_Distributor_Wise1 '" + divcode + "','" + sfCode + "','" + FromDate + "','" + ToDate + "'," + stcode + ",'" + subdivision_code + "'";

            try
            {
                dsDivision = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsDivision;
        }
        
        public DataSet Get_retail_list(string stckcode, string prdcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            //string strQry = "exec [testGET_Retailer_Distributor_Wise_retaildetails] '" + divcode + "','" + sfCode + "','" + FYear + "','" + TYear + "','" + FMonth + "','" + TMonth + "'," + stcode + ",'" + subdivision_code + "','" + stckcode + "','" + prdcode + "'";
            string strQry = "exec [testGET_Retailer_Distributor_Wise_retaildetails1] '" + divcode + "','" + sfCode + "','" + FromDate + "','" + ToDate + "'," + stcode + ",'" + subdivision_code + "','" + stckcode + "','" + prdcode + "'";

            
            try
            {
                dsDivision = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsDivision;
        }
        public DataSet Get_retail_prodcnt()
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            //string strQry = "exec [testGET_Retailer_Distributor_Wise_prodcnt] '" + divcode + "','" + sfCode + "','" + FYear + "','" + TYear + "','" + FMonth + "','" + TMonth + "'," + stcode + ",'" + subdivision_code + "'";
            string strQry = "exec [testGET_Retailer_Distributor_Wise_prodcnt1] '" + divcode + "','" + sfCode + "','" + FromDate + "','" + ToDate + "'," + stcode + ",'" + subdivision_code + "'";

            
            try
            {
                dsDivision = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsDivision;
        }
        public DataSet Get_retail_routecount()
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            string strQry = "exec [testGET_Retailer_Distributor_Wise_routeretail] '" + divcode + "','" + subdivision_code + "'";
            try
            {
                dsDivision = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsDivision;
        }
        public DataSet Get_retail_dis_cat()
           {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;

            
            string strQry = " SELECT c.Product_Cat_Code,c.Product_Cat_SName,c.Product_Cat_Name,c.Product_Cat_Div_Name, " +
                     " (select COUNT(p.Product_Cat_Code) from Mas_Product_Detail p where p.Product_Active_Flag=0 and p.Product_Cat_Code = c.Product_Cat_Code ) as cat_count   FROM  Mas_Product_Category c" +
                     " WHERE c.Product_Cat_Active_Flag=0 AND Product_Cat_Div_Code= '" + subdivision_code + "' " +
                     " ORDER BY c.ProdCat_SNo"; 
            try
            {
                dsDivision = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsDivision;
        }
    }
}