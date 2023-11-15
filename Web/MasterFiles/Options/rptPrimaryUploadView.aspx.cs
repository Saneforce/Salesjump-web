using DBase_EReport;
using DocumentFormat.OpenXml.Bibliography;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterFiles_Options_rptPrimaryUploadView : System.Web.UI.Page
{

    #region "Declaration"
    DataSet dsSalesForce = null;
    public static string sf_code = string.Empty;
    public static string div_code = string.Empty;
    public static string FYear = string.Empty;
    public static string FMonth = string.Empty;   
    #endregion

    #region "Page_Load"
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            div_code = Session["div_code"].ToString();
            FYear = Request.QueryString["FYear"].ToString();
            FMonth = Request.QueryString["FMonth"].ToString();

            ddlFYear.Value = FYear;
            ddlFMonth.Value = FMonth;

            Label2.Text = "Product wise Primary Order Upload Details Month : " + FMonth + " Year : " + FYear;
        }
    }
    #endregion

    public class Item
    {
        public string product_id { get; set; }

        public string product_name { get; set; }
    }

    [WebMethod(EnableSession = true)]
    public static Item[] getdata(string FYear, string FMonth)
    {
        div_code = HttpContext.Current.Session["div_code"].ToString();
        List<Item> empList = new List<Item>();

        try
        {
            uplaov uc = new uplaov();
            DataSet dsAccessmas = uc.getproductname(div_code.TrimEnd(','), FYear, FMonth);
            foreach (DataRow row in dsAccessmas.Tables[0].Rows)
            {
                Item emp = new Item();
                emp.product_id = row["Product_Detail_Code"].ToString();
                emp.product_name = row["Product_Short_Name"].ToString();
                empList.Add(emp);
            }
        }
        catch (Exception ex)
        { ex.Message.ToString().Trim(); }

        return empList.ToArray();
    }


    [WebMethod(EnableSession = true)]
    public static IssueDetails[] getIssuData(string FYear, string FMonth)
    {        
        uplaov pro = new uplaov();
        List<IssueDetails> empList = new List<IssueDetails>();
        try
        {            
            div_code = HttpContext.Current.Session["div_code"].ToString();
            DataSet dsPro = pro.getrptIssueSlip_Month(div_code, FYear, FMonth);
            foreach (DataRow row in dsPro.Tables[0].Rows)
            {
                IssueDetails emp = new IssueDetails();

                emp.OrderNo = row["Trans_Sl_No"].ToString();
                emp.DisCode = row["Stockist_Code"].ToString();
                emp.proCode = row["Product_Detail_Code"].ToString();
                emp.caseRate = row["Quantity"].ToString();
                emp.amount = row["order_val"].ToString();
                emp.netWeight = row["net_weight"].ToString();

                empList.Add(emp);
            }
        }
        catch(Exception ex) { ex.Message.ToString(); }
       
        return empList.ToArray();
    }

    public class IssueDetails
    {
        public string OrderNo { get; set; }

        public string DisCode { get; set; }

        public string proCode { get; set; }

        public string caseRate { get; set; }

        public string amount { get; set; }

        public string netWeight { get; set; }

    }

    [WebMethod(EnableSession = true)]
    public static TotEcTc[] getIssuDataTcEc(string FYear, string FMonth)
    {
        string div_code = "";
        string sf_Code = "";
        uplaov pro = new uplaov();
        List<TotEcTc> empList = new List<TotEcTc>();
        try
        {
            sf_Code = HttpContext.Current.Session["SF_Code"].ToString();
            div_code = HttpContext.Current.Session["div_code"].ToString();
            DataSet dsPro = pro.getrptIssueSlip_MonthTCEC(div_code, FYear, FMonth);
            foreach (DataRow row in dsPro.Tables[0].Rows)
            {
                TotEcTc emp = new TotEcTc();
                emp.OrderNo = row["Trans_Sl_No"].ToString();
                emp.DisCode = row["Stockist_Code"].ToString();
                emp.Order_Value = row["Order_Value"].ToString(); 
                emp.TotalPack = row["TotalPack"].ToString();
                emp.TotalQtymt = row["TotalQtymt"].ToString();
                empList.Add(emp);
            }
        }
        catch (Exception ex) { ex.Message.ToString(); }

        return empList.ToArray();
    }

    public class TotEcTc
    {
        public string OrderNo { get; set; }

        public string DisCode { get; set; }

        public string Order_Value { get; set; }

        public string TotalPack { get; set; }

        public string TotalQtymt { get; set; }

    }


    [WebMethod(EnableSession = true)]
    public static FFNames[] getDistributor(string FYear, string FMonth)
    {
        string div_code = "";
        uplaov sf = new uplaov();
        List<FFNames> empList = new List<FFNames>();
        try
        {
            div_code = HttpContext.Current.Session["div_code"].ToString();

            DataSet dsAccessmas = sf.GetDistributorList(div_code, FYear, FMonth);
            foreach (DataRow row in dsAccessmas.Tables[0].Rows)
            {
                FFNames emp = new FFNames();
                emp.OrderNo = row["Trans_Sl_No"].ToString();
                emp.DisCode = row["Stockist_Code"].ToString();
                emp.DisName = row["Stockist_Name"].ToString();
                emp.OrderDate = row["Activity_Date"].ToString();
                empList.Add(emp);
            }
        }
        catch (Exception ex) { throw ex; }

        return empList.ToArray();
    }

    public class FFNames
    {
        public string OrderNo { get; set; }

        public string DisCode { get; set; }

        public string DisName { get; set; }

        public string OrderDate { get; set; }
    }


    public class uplaov
    {
        public DataSet getproductname(string div_code, string FYear, string FMonth)
        {
            DB_EReporting db_ER = new DB_EReporting();
            string strQry = string.Empty;
            DataSet dsAdmin = new DataSet();

            strQry = "EXEC [GET_PriUploadProductNamesList] '" + div_code + "','" + FYear + "','" + FMonth + "'";

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

        public DataSet getrptIssueSlip_Month(string div_code, string FYear, string FMonth)
        {
            DB_EReporting db_ER = new DB_EReporting();
            string strQry = string.Empty;
            DataSet dsDivision = null;


            strQry = "exec [GET_PrimarySalesValueUploadIssuSlip_Month] '" + div_code + "','" + FYear + "','" + FMonth + "'";

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

        public DataSet getrptIssueSlip_MonthTCEC(string div_code, string FYear, string FMonth)
        {
            DB_EReporting dB_EReporting = new DB_EReporting();
            DataSet dataSet = new DataSet();
            string strQry = "EXEC GET_PrimaryUploadTotalQtyPACK '" + div_code + "','" + FYear + "','" + FMonth + "'";
            try
            {

                dataSet = dB_EReporting.Exec_DataSet(strQry);
                //return dB_EReporting.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dataSet;
        }

        public DataSet GetDistributorList(string div_code, string FYear, string FMonth)
        {
            DB_EReporting dB_EReporting = new DB_EReporting();
            DataSet dataSet = new DataSet();
            string strQry = "exec getHyrDistributorList '" + div_code + "','" + FYear + "','" + FMonth + "'";
            try
            {
                return dB_EReporting.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }   
}