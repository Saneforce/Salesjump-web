using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data;
using System.Configuration;
using Bus_EReport;
using DBase_EReport;

using System.Web.Services;
using System.Data.SqlClient;
using Newtonsoft.Json;

public partial class MasterFiles_SKU_Analysis : System.Web.UI.Page
{

    #region "Declaration"
    string divcode = string.Empty;
string sf_type = string.Empty;


    #endregion
 protected override void OnPreInit(EventArgs e)
    {
        base.OnPreInit(e);
        sf_type = Session["sf_type"].ToString();
        if (sf_type == "3")
        {
            this.MasterPageFile = "~/Master.master";
        }
        else if (sf_type == "2")
        {
            this.MasterPageFile = "~/Master_MGR.master";
        }
        else if (sf_type == "1")
        {
            this.MasterPageFile = "~/Master_MR.master";
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        divcode = Convert.ToString(Session["div_code"]);
        if (!Page.IsPostBack)
        {

        }
    }


    public class Pro_Data
    {
        public string Product_code { get; set; }
        public string Product_Name { get; set; }
    }
    public class Pro_Values
    {
        public string years { get; set; }
        public decimal tot_val { get; set; }
        public decimal sig_val { get; set; }
		  public decimal tot_cnt { get; set; }
        public decimal sig_cnt{ get; set; }
         public decimal tot_rpt { get; set; }
         public decimal sig_rpt { get; set; }
    }
    public class Pro_Details
    {
        public string Product_code { get; set; }
        public string Product_Name { get; set; }
        public string Product_Short_Name { get; set; }
        public string UOM { get; set; }
        public string Base_UOM { get; set; }
        public string Create_date { get; set; }
        public string product_cat { get; set; }
        public string product_brand { get; set; }
        public string product_target { get; set; }
        public string product_achieved { get; set; }
    }
    public class pro_years
    {
        public string years { get; set; }
    }

    [WebMethod(EnableSession = true)]
    public static Pro_Data[] Get_Product_Name()
    {

        Product pro = new Product();
        List<Pro_Data> product = new List<Pro_Data>();
        string Div_code = HttpContext.Current.Session["div_code"].ToString();
        DataSet dsProduct = pro.getProd(Div_code.TrimEnd(','));
        foreach (DataRow row in dsProduct.Tables[0].Rows)
        {
            Pro_Data pd = new Pro_Data();
            pd.Product_code = row["Product_Detail_Code"].ToString();
            pd.Product_Name = row["Product_Detail_Name"].ToString();
            product.Add(pd);
        }
        return product.ToArray();
    }

    [WebMethod(EnableSession = true)]
    public static pro_years[] Get_Product_Year(string pro_code)
    {

        Product pro = new Product();
        List<pro_years> product = new List<pro_years>();
        string Div_code = HttpContext.Current.Session["div_code"].ToString();
        DataSet dsProduct = null;
        dsProduct = pro.getProdforCode_details(Div_code, pro_code);
        foreach (DataRow row in dsProduct.Tables[0].Rows)
        {
          
            DateTime pyr = Convert.ToDateTime(row["Created_Date"].ToString());
            HttpContext.Current.Session["Pro_Year"] = pyr;
            Int32 py = pyr.Year ;

            DateTime cyr = DateTime.Today;
            Int32 cy = cyr.Year;

            while (py <= cy)
            {
                pro_years pd = new pro_years();            
                pd.years = py.ToString();                
                product.Add(pd);
                py++;
            }
            
        }
        return product.ToArray();
    }

[WebMethod(EnableSession = true)]
    public static Pro_Values[] Get_Product_shopcnt(string Product_Code, string year)
    {
        Productn pro = new Productn();
        List<Pro_Values> product = new List<Pro_Values>();
        string Div_code = HttpContext.Current.Session["div_code"].ToString();
        string Sub_Div = "";//HttpContext.Current.Session["Sub_Div_code"].ToString();
        string SF_Code = HttpContext.Current.Session["SF_Code"].ToString();
        DataSet DsCurrentYear = pro.Get_Product_shopcnt_month(Div_code, Sub_Div, SF_Code, Product_Code, year);
        Int32 y = Convert.ToInt32(year);
        Int32 yr = y - 1;
        DataSet DsLastYear = pro.Get_Product_shopcnt_month(Div_code, Sub_Div, SF_Code, Product_Code, yr.ToString());
        DataTable dt = new DataTable();
        dt.Columns.Add("month", typeof(Int16));
        dt.Columns.Add("months", typeof(String));
        dt.Rows.Add(1, "Jan");
        dt.Rows.Add(2, "Feb");
        dt.Rows.Add(3, "Mar");
        dt.Rows.Add(4, "Apr");
        dt.Rows.Add(5, "May");
        dt.Rows.Add(6, "Jun");
        dt.Rows.Add(7, "Jul");
        dt.Rows.Add(8, "Aug");
        dt.Rows.Add(9, "Sep");
        dt.Rows.Add(10, "Oct");
        dt.Rows.Add(11, "Nov");
        dt.Rows.Add(12, "Dec");

        for (int i = 1; i <= 12; i++)
        {
            Pro_Values pv = new Pro_Values();

            pv.years = dt.Rows[i - 1][1].ToString();
            if (DsCurrentYear.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in DsCurrentYear.Tables[0].Rows)
                {
                    if (i == Convert.ToInt16(row["MONTHS"]))
                    {
                        pv.tot_cnt = Convert.ToDecimal(row["cnt"]);
                    }
                }
            }
            else
            {
                pv.tot_cnt = 0;
            }
            if (DsLastYear.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in DsLastYear.Tables[0].Rows)
                {
                    if (i == Convert.ToInt16(row["MONTHS"]))
                    {
                        pv.sig_cnt = Convert.ToDecimal(row["cnt"]);
                    }
                }
            }
            else
            {
                pv.sig_cnt = 0;
            }
            product.Add(pv);
        }

        return product.ToArray();

    }
    [WebMethod(EnableSession = true)]
    public static Pro_Values[] Get_Product_shoprepeat(string Product_Code, string year)
    {
        Productn pro = new Productn();
        List<Pro_Values> product = new List<Pro_Values>();
        string Div_code = HttpContext.Current.Session["div_code"].ToString();
        string Sub_Div = "";//HttpContext.Current.Session["Sub_Div_code"].ToString();
        string SF_Code = HttpContext.Current.Session["SF_Code"].ToString();
        DataSet DsCurrentYear = pro.Get_Product_shoprpt_month(Div_code, Sub_Div, SF_Code, Product_Code, year);
        Int32 y = Convert.ToInt32(year);
        Int32 yr = y - 1;
        DataSet DsLastYear = pro.Get_Product_shoprpt_month(Div_code, Sub_Div, SF_Code, Product_Code, yr.ToString());
        DataTable dt = new DataTable();
        dt.Columns.Add("month", typeof(Int16));
        dt.Columns.Add("months", typeof(String));
        dt.Rows.Add(1, "Jan");
        dt.Rows.Add(2, "Feb");
        dt.Rows.Add(3, "Mar");
        dt.Rows.Add(4, "Apr");
        dt.Rows.Add(5, "May");
        dt.Rows.Add(6, "Jun");
        dt.Rows.Add(7, "Jul");
        dt.Rows.Add(8, "Aug");
        dt.Rows.Add(9, "Sep");
        dt.Rows.Add(10, "Oct");
        dt.Rows.Add(11, "Nov");
        dt.Rows.Add(12, "Dec");

        for (int i = 1; i <= 12; i++)
        {
            Pro_Values pv = new Pro_Values();

            pv.years = dt.Rows[i - 1][1].ToString();
            if (DsCurrentYear.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in DsCurrentYear.Tables[0].Rows)
                {
                    if (i == Convert.ToInt16(row["MONTHS"]))
                    {
                        pv.tot_rpt = Convert.ToDecimal(row["Rpt"]);
                    }
                }
            }
            else
            {
                pv.tot_rpt = 0;
            }
            if (DsLastYear.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in DsLastYear.Tables[0].Rows)
                {
                    if (i == Convert.ToInt16(row["MONTHS"]))
                    {
                        pv.sig_rpt = Convert.ToDecimal(row["Rpt"]);
                    }
                }
            }
            else
            {
                pv.sig_rpt = 0;
            }
            product.Add(pv);
        }

        return product.ToArray();

    }
    public class Productn
    {
        public DataSet Get_Product_shopcnt_month(string Div_Code, string Sub_Div, string SF_Code, string Product_Code, string year)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet dsState = null;
            string strQry = "EXEC SKY_Analysis_shopcount_Year '" + Div_Code + "','" + Sub_Div + "','" + SF_Code + "','" + Product_Code + "','" + year + "'";
            try
            {
                dsState = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsState;
        }
        public DataSet Get_Product_shoprpt_month(string Div_Code, string Sub_Div, string SF_Code, string Product_Code, string year)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet dsState = null;
            string strQry = "EXEC SKY_Analysis_shopcount_repeat '" + Div_Code + "','" + Sub_Div + "','" + SF_Code + "','" + Product_Code + "','" + year + "'";
            try
            {
                dsState = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsState;
        }
    }

    [WebMethod(EnableSession = true)]
    public static Pro_Details[] Get_Product_Details(string pro_code, string year)
    {

        Product pro = new Product();
        List<Pro_Details> product = new List<Pro_Details>();
        string Div_code = HttpContext.Current.Session["div_code"].ToString();
        string Sf_Code = HttpContext.Current.Session["Sf_Code"].ToString();
        DataSet dsProduct = null;
        dsProduct = pro.getProdforCode_details(Div_code, pro_code);
        DataSet dsTarget = null;
        dsTarget = pro.getProd_target_sfcode(Div_code, Sf_Code, pro_code,year );

        foreach (DataRow row in dsProduct.Tables[0].Rows)
        {
            Pro_Details pd = new Pro_Details();
            pd.Product_code = row["Product_Detail_Code"].ToString();
            pd.Product_Name = row["Product_Detail_Name"].ToString();
            pd.Product_Short_Name = row["Product_Short_Name"].ToString();
            pd.Create_date = row["Created_Date"].ToString();
            pd.Base_UOM = row["Base_Unit_code"].ToString();
            pd.UOM = row["Unit_code"].ToString();
            pd.product_cat = row["Product_Cat_Name"].ToString();
            pd.product_brand = row["Product_Brd_Name"].ToString();
            if (dsTarget.Tables[0].Rows.Count > 0)
            {
               pd.product_target= dsTarget.Tables[0].Rows[0][0].ToString() ;
            }
            else
            {
                pd.product_target = "0";
            }
            product.Add(pd);
        }

        return product.ToArray();
    }




    [WebMethod(EnableSession = true)]
    public static Pro_Values[] Get_Product_Data(string Product_Code,string year)
    {
        Product pro = new Product();
        List<Pro_Values> product = new List<Pro_Values>();
        string Div_code = HttpContext.Current.Session["div_code"].ToString();
        string Sub_Div = "";//HttpContext.Current.Session["Sub_Div_code"].ToString();
        string SF_Code = HttpContext.Current.Session["SF_Code"].ToString();
        DataSet dsAnalysis = pro.Get_SKY_Analysis(Div_code.TrimEnd(','), Sub_Div, SF_Code);
        DataSet dsAnalysis_Pro = pro.Get_SKY_Analysis_Product(Div_code.TrimEnd(','), Sub_Div, SF_Code, Product_Code);
        
        Int32 y = Convert.ToInt32(year);

        DateTime cyr = Convert.ToDateTime(HttpContext.Current.Session["Pro_Year"].ToString());
        Int32 cy = cyr.Year;


        for (int i = cy; i <= y; i++)
        {
            Pro_Values pd = new Pro_Values();
            pd.years = i.ToString();

            if (dsAnalysis_Pro.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in dsAnalysis.Tables[0].Rows)
                {

                    if (Convert.ToInt16(row["years"]) == i)
                    {
                        pd.tot_val = Convert.ToDecimal(row["Order_Value"]);
                    }
                }
            }
            else
            {
                pd.tot_val = 0;
            }

            if (dsAnalysis_Pro.Tables[0].Rows.Count > 0)
            {
                //  DataTable dt = dsAnalysis_Pro.Tables[0].Select("years = '" + row["years"].ToString().TrimEnd(',') + "'").CopyToDataTable();
                DataTable dt = new DataTable();
                dt.Columns.Add("Order_Value", typeof(string));
                DataRow[] dr = dsAnalysis_Pro.Tables[0].Select("years = '" + i.ToString() + "'");

                foreach (DataRow r in dr)
                {
                    dt.Rows.Add(r["Order_Value"].ToString());
                }

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow ro in dt.Rows)
                    {
                        pd.sig_val = Convert.ToDecimal(ro["Order_Value"]);
                    }
                }
                else
                {
                    pd.sig_val = 0;
                }
            }
            else
            {
                pd.sig_val = Convert.ToDecimal(0);
            }
            product.Add(pd);

        }
        return product.ToArray();


    }
    [WebMethod(EnableSession = true)]
    public static Pro_Values[] Get_Product_Growth(string Product_Code, string year)
    {
        Product pro = new Product();
        List<Pro_Values> product = new List<Pro_Values>();
        string Div_code = HttpContext.Current.Session["div_code"].ToString();
        string Sub_Div = "";//HttpContext.Current.Session["Sub_Div_code"].ToString();
        string SF_Code = HttpContext.Current.Session["SF_Code"].ToString();
        DataSet DsCurrentYear = pro.Get_SKY_Analysis_Product_Month(Div_code, Sub_Div, SF_Code, Product_Code, year);
        Int32 y = Convert.ToInt32(year);
        Int32 yr = y - 1;
        DataSet DsLastYear = pro.Get_SKY_Analysis_Product_Month(Div_code, Sub_Div, SF_Code, Product_Code, yr.ToString());
        DataTable dt = new DataTable();
        dt.Columns.Add("month", typeof(Int16));
        dt.Columns.Add("months", typeof(String));
        dt.Rows.Add(1, "Jan");
        dt.Rows.Add(2, "Feb");
        dt.Rows.Add(3, "Mar");
        dt.Rows.Add(4, "Apr");
        dt.Rows.Add(5, "May");
        dt.Rows.Add(6, "Jun");
        dt.Rows.Add(7, "Jul");
        dt.Rows.Add(8, "Aug");
        dt.Rows.Add(9, "Sep");
        dt.Rows.Add(10, "Oct");
        dt.Rows.Add(11, "Nov");
        dt.Rows.Add(12, "Dec");

        for (int i = 1; i <= 12; i++)
        {
            Pro_Values pv = new Pro_Values();

            pv.years = dt.Rows[i - 1][1].ToString();
            if (DsCurrentYear.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in DsCurrentYear.Tables[0].Rows)
                {
                    if (i == Convert.ToInt16(row["MONTHS"]))
                    {
                        pv.tot_val = Convert.ToDecimal(row["Order_Value"]);
                    }
                }
            }
            else
            {
                pv.tot_val = 0;
            }
            if (DsLastYear.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in DsLastYear.Tables[0].Rows)
                {
                    if (i == Convert.ToInt16(row["MONTHS"]))
                    {
                        pv.sig_val = Convert.ToDecimal(row["Order_Value"]);
                    }
                }
            }
            else
            {
                pv.sig_val = 0;
            }
            product.Add(pv);
        }

        return product.ToArray();

    }
    public class Pro_State
    {
        public string Years { get; set; }
        public string State_Code { get; set; }
        public string State_Name { get; set; }
        public decimal values { get; set; }

        
    }

    [WebMethod(EnableSession = true)]
    public static Pro_State[] Get_StateWise(string pro_code,string year)
    {

        Product pro = new Product();
        State st = new State();
        List<Pro_State> product = new List<Pro_State>();
        string Div_code = HttpContext.Current.Session["div_code"].ToString();
        string Sf_Code = HttpContext.Current.Session["Sf_code"].ToString();
        string Sub_Div = "";
        DataSet dsProduct = pro.Get_StateWise_Value(Div_code.TrimEnd(','), Sub_Div, Sf_Code, pro_code);
        DataSet dsState = st.Get_State_Division_Wise(Div_code);

        Int32 Cr_Year = Convert.ToInt32(year);
        
        DateTime cyr = Convert.ToDateTime(HttpContext.Current.Session["Pro_Year"].ToString());
        Int32 Cur_Year = cyr.Year;




        for (int y = Cur_Year; y <= Cr_Year; y++)
        {
            
            foreach (DataRow row in dsState.Tables[0].Rows)
            {
                Pro_State ps = new Pro_State();
                ps.Years = y.ToString();
                ps.State_Code = row["State_code"].ToString();
                ps.State_Name = row["Statename"].ToString();

                if (dsProduct.Tables[0].Rows.Count > 0)
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add("Order_Value", typeof(string));
                    string str = row["state_code"].ToString();
                    DataRow[] dr = dsProduct.Tables[0].Select("years = '" + y.ToString() + "' and state_code='" + str + "'");

                    foreach (DataRow r in dr)
                    {
                        dt.Rows.Add(r["Order_Value"].ToString());
                    }
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow ro in dt.Rows)
                        {
                            ps.values = Convert.ToDecimal(ro["Order_Value"]);
                        }
                    }
                    else
                    {
                        ps.values = 0;
                    }
                }
                else
                {
                    ps.values = 0;
                }
                product.Add(ps);
            }
            
        }
        return product.ToArray();
    }


}
