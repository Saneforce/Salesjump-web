using Bus_EReport;
using Newtonsoft.Json;
using System;
using System.Data;
using System.Web.Services;
using System.Data.SqlClient;
using DBase_EReport;

public partial class MIS_Reports_new_primary_filter : System.Web.UI.Page
{
    public static string divcode = string.Empty;
    public static string Sf_Code = string.Empty;
    public static string subdiv_code = string.Empty;
    public static string statecode = string.Empty;
    public static string rpt = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        Sf_Code = "admin";
        subdiv_code = "0";
        statecode = "0";
        rpt = "Primary Order";
    }
    [WebMethod(EnableSession = true)]
    public static string Binddivname(string divcode)
    {
        SalesForce dv = new SalesForce();
        DataTable ds = new DataTable();
        SqlConnection con = new SqlConnection(Globals.ConnString);
        con.Open();
        string strQry = "select * from mas_division where Division_Code = '" + divcode + "'";
        SqlCommand cmd = new SqlCommand(strQry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(ds);
        con.Close();
        return JsonConvert.SerializeObject(ds);
    }
    [WebMethod(EnableSession = true)]
    public static string Bindfillqty(string divcode, int pageindex, int plimt, string fdate, string tdate, string odatv, string distv, string ordbyv, string routv, string selovals, string selorbvals, string seldvals, string selrtvals,string arrayval)
    {
        SalesForce dv = new SalesForce();
        DataTable ds = new DataTable();
        SqlConnection con = new SqlConnection(Globals.ConnString);
        con.Open();
        string strQry = " WITH UserCTE AS(SELECT SF_Code,Sf_Name +' - '+sf_Designation_Short_Name + ' - ' + sf_hq as SF_Name, Reporting_To_SF,sf_type,0 AS steps, subdivision_code  FROM Mas_Salesforce WHERE Sf_Code ='" + Sf_Code + "' " +
                        " and charindex(','+cast('" + divcode + "' as varchar)+',',','+Division_Code+',')>0 and sf_status=0  UNION ALL SELECT mgr.SF_Code, mgr.Sf_Name +' - '+mgr.sf_Designation_Short_Name + ' - ' + mgr.sf_hq as SF_Name, " +
                        " mgr.Reporting_To_SF,mgr.sf_type, usr.steps +1 AS steps,mgr.subdivision_code FROM UserCTE AS usr INNER JOIN Mas_Salesforce AS mgr ON usr.Sf_Code = mgr.Reporting_To_SF and charindex(','+cast('" + divcode + "' as varchar)+',',','+mgr.Division_Code+',')>0 " +
                        " and sf_status=0 UNION ALL SELECT mgr.SF_Code, mgr.Sf_Name +' - '+mgr.sf_Designation_Short_Name + ' - ' + mgr.sf_hq as SF_Name, null Reporting_To_SF,mgr.sf_type, usr.steps +1 AS " +
                        " steps,mgr.subdivision_code FROM UserCTE AS usr INNER JOIN Mas_Sf_Audit_Team AS aud ON charindex(','+usr.SF_code+',',','+aud.Audit_Team+',')>0 INNER JOIN Mas_Salesforce" +
                        " AS mgr on mgr.SF_Code=aud.Sf_Code and charindex(','+cast('" + divcode + "' as varchar)+',',','+mgr.Division_Code+',')>0 and sf_status=0)," +
                        " transh AS(select * from trans_priorder_head where Division_Code='" + divcode + "'  and CONVERT(date,order_date) between '" + fdate + "' and '" + tdate + "')," +
                        " tab as(select pd.Trans_Sl_No,convert(date,ph.Order_Date) date,convert(varchar,ph.Order_Date,103) Order_Date,ph.sf_code mr,ct.Sf_Name,ms.Territory,ms.Stockist_Address,ms.Stockist_Code,ms.stockist_name," +
                        " pd.Product_Code,pd.Product_Name,((isnull(d.Sample_Erp_Code,0)*pd.CQty)+pd.PQty)QTY,Free,Cl_Bal from " +
                        " trans_priorder_details pd inner join transh ph on ph.Trans_Sl_No=pd.Trans_Sl_No inner join Mas_Product_Detail d on d.Product_Detail_Code=pd.Product_Code " +
                        " inner join UserCTE ct on ct.SF_Code=ph.Sf_Code inner join mas_stockist ms on ms.stockist_code=ph.stockist_code) ";
        if (arrayval == "")
        {
            strQry += " select Trans_Sl_No,Order_Date,mr,Sf_Name,Territory,Stockist_Address,stockist_name,Product_Code,Product_Name,sum(QTY)QTY from tab where date between '" + fdate + "' and '" + tdate + "' ";
        }
        if (arrayval != "")
        {
            strQry += " select " + arrayval + ",Product_Code,Product_Name,sum(QTY)QTY from tab where date between '" + fdate + "' and '" + tdate + "'";
        }

        if (odatv != "")
        {
            strQry += " and CONVERT(varchar," + selovals + ") in(select * from SplitString('" + odatv + "',',')) ";
        }
        if (distv != "")
        {
            strQry += " and CONVERT(varchar," + seldvals + ") in(select * from SplitString('" + distv + "',','))";
        }
        if (ordbyv != "")
        {
            strQry += " and CONVERT(varchar," + selorbvals + ") in(select * from SplitString('" + ordbyv + "',','))";
        }
        if (routv != "")
        {
            strQry += " and CONVERT(varchar," + selrtvals + ") in(select * from SplitString('" + routv + "',','))";
        }
        if (arrayval == "")
        {
            strQry += " group by Trans_Sl_No,Order_Date,mr,Sf_Name,Territory,Stockist_Address,stockist_name,Product_Code,Product_Name ";
        }
        if (arrayval != "")
        {
            strQry += " group by " + arrayval + ",Product_Code,Product_Name ";
        }

        SqlCommand cmd = new SqlCommand(strQry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(ds);
        con.Close();
        return JsonConvert.SerializeObject(ds);
    }
    [WebMethod(EnableSession = true)]
    public static string Bindfillqty_view(string divcode, int pageindex, int plimt, string fdate, string tdate, string odatv, string distv, string ordbyv, string routv, string selovals, string selorbvals, string seldvals, string selrtvals, string arrayval)
    {
        SalesForce dv = new SalesForce();
        DataTable ds = new DataTable();
        SqlConnection con = new SqlConnection(Globals.ConnString);
        con.Open();
        string strQry = " WITH UserCTE AS(SELECT SF_Code,Sf_Name +' - '+sf_Designation_Short_Name + ' - ' + sf_hq as SF_Name, Reporting_To_SF,sf_type,0 AS steps, subdivision_code  " +
                        " FROM Mas_Salesforce WHERE Sf_Code = '" + Sf_Code + "' and charindex(','+cast('" + divcode + "' as varchar)+',',','+Division_Code+',')>0 and sf_status=0  " +
                        " UNION ALL SELECT mgr.SF_Code, mgr.Sf_Name +' - '+mgr.sf_Designation_Short_Name + ' - ' + mgr.sf_hq as SF_Name, mgr.Reporting_To_SF,mgr.sf_type," +
                        " usr.steps +1 AS steps,mgr.subdivision_code FROM UserCTE AS usr INNER JOIN Mas_Salesforce AS mgr ON usr.Sf_Code = mgr.Reporting_To_SF and " +
                        " charindex(','+cast('" + divcode + "' as varchar)+',',','+mgr.Division_Code+',')>0 and sf_status=0  UNION ALL SELECT mgr.SF_Code, " +
                        " mgr.Sf_Name +' - '+mgr.sf_Designation_Short_Name + ' - ' + mgr.sf_hq as SF_Name, null Reporting_To_SF,mgr.sf_type, usr.steps +1 AS steps," +
                        " mgr.subdivision_code FROM UserCTE AS usr INNER JOIN Mas_Sf_Audit_Team AS aud ON charindex(','+usr.SF_code+',',','+aud.Audit_Team+',')>0 INNER JOIN " +
                        " Mas_Salesforce AS mgr on mgr.SF_Code=aud.Sf_Code and charindex(','+cast('" + divcode + "' as varchar)+',',','+mgr.Division_Code+',')>0 and sf_status=0), " +
                        " transh AS(select * from trans_priorder_head where Division_Code='" + divcode + "'  and CONVERT(date, order_date) between '" + fdate + "' and '" + tdate + "' ), " +
                        " tab as(select ph.Trans_sl_No,CONVERT(date,ph.Order_Date)date,CONVERT(varchar,ph.Order_Date,103)Order_Date,ms.Stockist_Code,ms.stockist_name,ph.sf_code mr,u.Sf_Name,ms.Territory,ms.Stockist_Address,round(((isnull(d.Sample_Erp_Code,0)*pd.CQty)+pd.PQty)*isnull(d.product_netwt,0),2) net_weight_value," +
                        " ph.Order_Value from transh ph inner join mas_stockist ms on ms.stockist_code=ph.stockist_code inner join UserCTE u on u.SF_Code=ph.Sf_Code " +
                        " inner join trans_priorder_details pd on pd.Trans_Sl_No=ph.Trans_Sl_No inner join mas_product_detail d on d.Product_Detail_Code=pd.Product_Code " +
                        " where ph.Order_Value!=0 )";
        if (arrayval == "")
        {
            strQry += " select Trans_sl_No,Order_Date,stockist_name,mr,Sf_Name,Territory,Stockist_Address,sum(net_weight_value)net_weight_value,Order_Value " +
                      " from tab where date between '" + fdate + "' and '" + tdate + "'";
        }
        if (arrayval != "")
        {
            strQry += " select " + arrayval + ",sum(net_weight_value)net_weight_value,round(sum(distinct Order_Value),2)Order_Value from " +
                      " tab where date between '" + fdate + "' and '" + tdate + "'";
        }
        if (odatv != "")
        {
            strQry += " and CONVERT(varchar," + selovals + ") in(select * from SplitString('" + odatv + "',',')) ";
        }
        if (distv != "")
        {
            strQry += " and CONVERT(varchar," + seldvals + ") in(select * from SplitString('" + distv + "',','))";
        }
        if (ordbyv != "")
        {
            strQry += " and CONVERT(varchar," + selorbvals + ") in(select * from SplitString('" + ordbyv + "',','))";
        }
        if (routv != "")
        {
            strQry += " and CONVERT(varchar," + selrtvals + ") in(select * from SplitString('" + routv + "',','))";
        }
        if (arrayval == "")
        {
            strQry += " group by Trans_sl_No,Order_Date,Stockist_Code,stockist_name,mr,Sf_Name,Territory,Stockist_Address,Order_Value " +
                      " order by Order_Date  OFFSET  " + pageindex + " ROWS FETCH NEXT " + plimt + " ROWS ONLY";
        }
        if (arrayval != "")
        {
            strQry += " GROUP BY  " + arrayval + " order by Order_Date  OFFSET  " + pageindex + " ROWS FETCH NEXT " + plimt + " ROWS ONLY ";
         }
        SqlCommand cmd = new SqlCommand(strQry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(ds);
        con.Close();
        return JsonConvert.SerializeObject(ds);
    }
    [WebMethod(EnableSession = true)]
    public static string totalcnt(string divcode, string fdate, string tdate, string odatv, string distv, string ordbyv, string routv, string selovals, string selorbvals, string seldvals, string selrtvals ,string arrayval)
    {
        SalesForce dv = new SalesForce();
        DataTable ds = new DataTable();
        SqlConnection con = new SqlConnection(Globals.ConnString);
        con.Open();
        string strQry = " WITH UserCTE AS(SELECT SF_Code,Sf_Name +' - '+sf_Designation_Short_Name + ' - ' + sf_hq as SF_Name, Reporting_To_SF,sf_type,0 AS steps, subdivision_code  " +
                         " FROM Mas_Salesforce WHERE Sf_Code = '" + Sf_Code + "' and charindex(','+cast('" + divcode + "' as varchar)+',',','+Division_Code+',')>0 and sf_status=0  " +
                         " UNION ALL SELECT mgr.SF_Code, mgr.Sf_Name +' - '+mgr.sf_Designation_Short_Name + ' - ' + mgr.sf_hq as SF_Name, mgr.Reporting_To_SF,mgr.sf_type," +
                         " usr.steps +1 AS steps,mgr.subdivision_code FROM UserCTE AS usr INNER JOIN Mas_Salesforce AS mgr ON usr.Sf_Code = mgr.Reporting_To_SF and " +
                         " charindex(','+cast('" + divcode + "' as varchar)+',',','+mgr.Division_Code+',')>0 and sf_status=0  UNION ALL SELECT mgr.SF_Code, " +
                         " mgr.Sf_Name +' - '+mgr.sf_Designation_Short_Name + ' - ' + mgr.sf_hq as SF_Name, null Reporting_To_SF,mgr.sf_type, usr.steps +1 AS steps," +
                         " mgr.subdivision_code FROM UserCTE AS usr INNER JOIN Mas_Sf_Audit_Team AS aud ON charindex(','+usr.SF_code+',',','+aud.Audit_Team+',')>0 INNER JOIN " +
                         " Mas_Salesforce AS mgr on mgr.SF_Code=aud.Sf_Code and charindex(','+cast('" + divcode + "' as varchar)+',',','+mgr.Division_Code+',')>0 and sf_status=0), " +
                         " transh AS(select * from trans_priorder_head where Division_Code='" + divcode + "'  and CONVERT(date, order_date) between '" + fdate + "' and '" + tdate + "' ), " +
                         " tab as(select ph.Trans_sl_No,CONVERT(date,ph.Order_Date)date,CONVERT(varchar,ph.Order_Date,103)Order_Date,ms.Stockist_Code,ms.stockist_name,ph.sf_code mr,u.Sf_Name,ms.Territory," +
                         "ms.Stockist_Address,((isnull(d.Sample_Erp_Code,0)*pd.CQty)+pd.PQty)*isnull(d.product_netwt,0) net_weight_value," +
                         " ph.Order_Value from transh ph inner join mas_stockist ms on ms.stockist_code=ph.stockist_code inner join UserCTE u on u.SF_Code=ph.Sf_Code " +
                         " inner join trans_priorder_details pd on pd.Trans_Sl_No=ph.Trans_Sl_No inner join mas_product_detail d on d.Product_Detail_Code=pd.Product_Code " +
                         " where ph.Order_Value!=0 )";
        if (arrayval == "")
        {
            strQry += " select Trans_sl_No,Order_Date,Stockist_Code,stockist_name,mr,Sf_Name,Territory,Stockist_Address,sum(net_weight_value)net_weight_value,Order_Value " +
                      " from tab where date between '" + fdate + "' and '" + tdate + "'";
        }
        if (arrayval != "")
        {
            strQry += " select " + arrayval + ",sum(net_weight_value)net_weight_value,round(sum(distinct Order_Value),2)Order_Value from " +
                      " tab where date between '" + fdate + "' and '" + tdate + "'";
        }
        if (odatv != "")
        {
            strQry += " and CONVERT(varchar," + selovals + ") in(select * from SplitString('" + odatv + "',',')) ";
        }
        if (distv != "")
        {
            strQry += " and CONVERT(varchar," + seldvals + ") in(select * from SplitString('" + distv + "',','))";
        }
        if (ordbyv != "")
        {
            strQry += " and CONVERT(varchar," + selorbvals + ") in(select * from SplitString('" + ordbyv + "',','))";
        }
        if (routv != "")
        {
            strQry += " and CONVERT(varchar," + selrtvals + ") in(select * from SplitString('" + routv + "',','))";
        }
        if (arrayval == "")
        {
            strQry += " group by Trans_sl_No,Order_Date,Stockist_Code,stockist_name,mr,Sf_Name,Territory,Stockist_Address,Order_Value ";
        }
        if (arrayval != "")
        {
            strQry += " GROUP BY  " + arrayval + "";
        }
        SqlCommand cmd = new SqlCommand(strQry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(ds);
        con.Close();
        return JsonConvert.SerializeObject(ds);
    }
    [WebMethod(EnableSession = true)]
    public static string Getorderdateval(string divcode, string fdate, string tdate, string odatv, string distv, string ordbyv, string routv, string selovals, string selorbvals, string seldvals, string selrtvals)
    {
        SalesForce dv = new SalesForce();
        DataTable ds = new DataTable();
        SqlConnection con = new SqlConnection(Globals.ConnString);
        con.Open();
        string strQry = " WITH UserCTE AS(SELECT SF_Code,Sf_Name +' - '+sf_Designation_Short_Name + ' - ' + sf_hq as SF_Name, Reporting_To_SF,sf_type,0 AS steps, subdivision_code  " +
                       " FROM Mas_Salesforce WHERE Sf_Code = '" + Sf_Code + "' and charindex(','+cast('" + divcode + "' as varchar)+',',','+Division_Code+',')>0 and sf_status=0  " +
                       " UNION ALL SELECT mgr.SF_Code, mgr.Sf_Name +' - '+mgr.sf_Designation_Short_Name + ' - ' + mgr.sf_hq as SF_Name, mgr.Reporting_To_SF,mgr.sf_type," +
                       " usr.steps +1 AS steps,mgr.subdivision_code FROM UserCTE AS usr INNER JOIN Mas_Salesforce AS mgr ON usr.Sf_Code = mgr.Reporting_To_SF and " +
                       " charindex(','+cast('" + divcode + "' as varchar)+',',','+mgr.Division_Code+',')>0 and sf_status=0  UNION ALL SELECT mgr.SF_Code, " +
                       " mgr.Sf_Name +' - '+mgr.sf_Designation_Short_Name + ' - ' + mgr.sf_hq as SF_Name, null Reporting_To_SF,mgr.sf_type, usr.steps +1 AS steps," +
                       " mgr.subdivision_code FROM UserCTE AS usr INNER JOIN Mas_Sf_Audit_Team AS aud ON charindex(','+usr.SF_code+',',','+aud.Audit_Team+',')>0 INNER JOIN " +
                       " Mas_Salesforce AS mgr on mgr.SF_Code=aud.Sf_Code and charindex(','+cast('" + divcode + "' as varchar)+',',','+mgr.Division_Code+',')>0 and sf_status=0), " +
                       " tab as(select ph.Trans_sl_No,CONVERT(varchar, ph.Order_Date,103)Order_Date, convert(date,ph.Order_Date)date,ms.Stockist_Code,ms.stockist_name,ph.sf_code mr,u.Sf_Name,ms.Territory,ms.Stockist_Address," +
                       " ((isnull(d.Sample_Erp_Code,0)*pd.CQty)+pd.PQty)*isnull(d.product_netwt,0) net_weight_value," +
                       " ph.Order_Value from trans_priorder_head ph inner join mas_stockist ms on ms.stockist_code=ph.stockist_code inner join UserCTE u on u.SF_Code=ph.Sf_Code " +
                       " inner join trans_priorder_details pd on pd.Trans_Sl_No=ph.Trans_Sl_No inner join mas_product_detail d on d.Product_Detail_Code=pd.Product_Code where ph.Order_Value!=0 and " +
                       " convert(date,ph.Order_Date) between '" + fdate + "' and '" + tdate + "')";
        strQry += " select Trans_sl_No,Order_Date,Stockist_Code,stockist_name,mr,Sf_Name,Territory,Stockist_Address,sum(net_weight_value)net_weight_value,Order_Value from tab" +
                   " where convert(date,date) between '" + fdate + "' and '" + tdate + "'";

        if (odatv != "")
        {
            strQry += " and charindex(','+ CONVERT(varchar," + selovals + ")+',',','+ IIF('" + odatv + "'='',CONVERT(varchar," + selovals + "),'" + odatv + "')+',')>0 ";
        }
        if (distv != "")
        {
            strQry += " and charindex(','+ CONVERT(varchar," + seldvals + ")+',',','+ IIF('" + distv + "'='',CONVERT(varchar," + seldvals + "),'" + distv + "')+',')>0";
        }
        if (ordbyv != "")
        {
            strQry += " and charindex(','+ CONVERT(varchar," + selorbvals + ")+',',','+ IIF('" + ordbyv + "'='',CONVERT(varchar," + selorbvals + "),'" + ordbyv + "')+',')>0";
        }
        if (routv != "")
        {
            strQry += " and charindex(','+ CONVERT(varchar," + selrtvals + ")+',',','+ IIF('" + routv + "'='',CONVERT(varchar," + selrtvals + "),'" + routv + "')+',')>0";
        }


        strQry += " group by Trans_sl_No,Order_Date,Stockist_Code,stockist_name,mr,Sf_Name,Territory,Stockist_Address,net_weight_value,Order_Value";

        SqlCommand cmd = new SqlCommand(strQry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(ds);
        con.Close();
        return JsonConvert.SerializeObject(ds);
    }
    [WebMethod]
    public static string savetemplate(string divcode, string tplist)
    {

        string dsSF = "";
        SalesForce Ad = new SalesForce();
        SqlConnection con = new SqlConnection(Globals.ConnString);
        con.Open();
        string strQry = "EXEC save_sec_fil_fields '" + divcode + "','" + rpt + "','" + Sf_Code + "','" + tplist + "'";
        SqlCommand cmd = new SqlCommand(strQry, con);
        cmd.ExecuteNonQuery();
        con.Close();
        return dsSF;

    }
    [WebMethod]
    public static string get_selectfields(string divcode)
    {

        SalesForce dv = new SalesForce();
        DataTable ds = new DataTable();
        SqlConnection con = new SqlConnection(Globals.ConnString);
        con.Open();
        string strQry = "select * from Rpt_field_setting_sec where Division_Code='" + divcode + "' and Report_name='" + rpt + "'";
        SqlCommand cmd = new SqlCommand(strQry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(ds);
        con.Close();
        return JsonConvert.SerializeObject(ds);

    }
}