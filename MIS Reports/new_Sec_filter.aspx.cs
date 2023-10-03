using Bus_EReport;
using Newtonsoft.Json;
using System;
using System.Data;
using System.Web.Services;
using System.Data.SqlClient;
using DBase_EReport;

public partial class MIS_Reports_new_Sec_filter : System.Web.UI.Page
{
    public static string divcode = string.Empty;
    public static string Sf_Code = string.Empty;
    public static string sfname = string.Empty;
    public static string subdiv_code = string.Empty;
    public static string statecode = string.Empty;
    public static string FDate = string.Empty;
    public static string TDate = string.Empty;
    public static string rpt = string.Empty;
    DataSet dsProds = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        //divcode = Session["div_code"].ToString();
        Sf_Code = "admin";
        subdiv_code = "0";
        statecode = "0";
        FDate = "2021-07-01";
        TDate = "2021-07-01";
        rpt = "Secondary Order";
        //pageindex = 0;
        //plimt = 50;
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
    public static string Bindfillqty(string divcode, int pageindex, int plimt, string fdate, string tdate, string odatv, string distv, string ordbyv, string mgrv, string erpv, string rutv, string retailv, string chnlv, string selovals, string selorbvals, string seldvals, string selmvals, string selerps, string selruts, string selrtls, string selchnls,string arrayval)
    {
        SalesForce dv = new SalesForce();
        DataTable ds = new DataTable();
        SqlConnection con = new SqlConnection(Globals.ConnString);
        con.Open();
        string strQry = " WITH UserCTE AS(SELECT SF_Code, Sf_Name, Reporting_To_SF, sf_type, 0 AS steps, subdivision_code, State_Code " +
                      " FROM Mas_Salesforce WHERE Sf_Code = '" + Sf_Code + "' and charindex(','+cast('" + divcode + "' as varchar)+',',','+Division_Code+',')>0 " +
                      " UNION ALL SELECT mgr.SF_Code, mgr.Sf_Name, mgr.Reporting_To_SF,mgr.sf_type, usr.steps +1 AS steps,mgr.subdivision_code,mgr.State_Code " +
                      " FROM UserCTE AS usr INNER JOIN Mas_Salesforce AS mgr ON usr.Sf_Code = mgr.Reporting_To_SF and charindex(','+cast('" + divcode + "' as varchar)+',',','+mgr.Division_Code+',')>0 " +
                      " UNION ALL SELECT mgr.SF_Code, mgr.Sf_Name, null Reporting_To_SF,mgr.sf_type, usr.steps +1 AS steps,mgr.subdivision_code,mgr.State_Code FROM UserCTE AS usr " +
                      " INNER JOIN Mas_Sf_Audit_Team AS aud ON charindex(','+usr.SF_code+',',','+aud.Audit_Team+',')>0 INNER JOIN Mas_Salesforce AS mgr " +
                      " on mgr.SF_Code=aud.Sf_Code and charindex(','+cast('" + divcode + "' as varchar)+',',','+mgr.Division_Code+',')>0), " +
                      " transh AS(select * from Trans_Order_Head where Div_ID='" + divcode + "'  and CONVERT(date,order_date) between '" + fdate + "' and '" + tdate + "'), " +
                      " tab as(select pd.Trans_Sl_No,convert(varchar,Order_Date,103)Order_Date,h.sf_code,pd.Product_Code,d.Product_Detail_Name,d.Product_Short_Name,pd.Quantity,pd.Con_Qty,pd.Uom_Id,ct.subdivision_code, " +
                      " ct.State_Code,ms.Stockist_name,ms.stockist_code,t.Territory_code,t.Territory_Name routename,l.ListedDrCode,l.ListedDr_Name retailername,l.Doc_Special_Code,l.Doc_Spec_ShortName channel " +
                      " ,ct.Sf_Code mr,ct.SF_Name,ct.Reporting_To_SF rsfc,mm.Sf_Name RSF,ms.ERP_Code from trans_order_details pd inner join transh h on h.Trans_Sl_No=pd.Trans_Sl_No inner join " +
                      " Mas_Product_Detail d on d.Product_Detail_Code=pd.Product_Code inner join UserCTE ct on ct.SF_Code=h.Sf_Code inner join  Mas_Territory_creation t on t.Territory_code = h.route and " +
                      " t.Territory_Active_Flag = 0 inner join Mas_Listeddr l on l.ListedDrCode=h.Cust_Code and l.ListedDr_Active_Flag=0 inner join Mas_Salesforce mm on mm.Sf_Code = ct.Reporting_To_SF " +
                      " inner join Mas_Stockist ms  on ms.stockist_code=h.Stockist_Code)";
        if (arrayval == "")
        {
            strQry += "select Trans_Sl_No,Order_Date,Product_Code,Product_Detail_Name,sum(Quantity)Quantity from tab where ";
        }
        if (arrayval != "")
        {
            strQry += "select " + arrayval + ",Product_Code,Product_Detail_Name,sum(Quantity)Quantity from tab where";
        }

        strQry += " CHARINDEX(',' + iif('" + subdiv_code + "'= 0, subdivision_code, '" + subdiv_code + "') + ',', ',' + subdivision_code + ',') > 0 and  " +
                  " State_Code=IIF('" + statecode + "'=0,State_Code,'" + statecode + "')";

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
        if (mgrv != "")
        {
            strQry += " and CONVERT(varchar," + selmvals + ") in(select * from SplitString('" + mgrv + "',','))";
        }
        if (erpv != "")
        {
            strQry += " and CONVERT(varchar," + selerps + ") in(select * from SplitString('" + erpv + "',',')) ";
        }
        if (rutv != "")
        {
            strQry += " and CONVERT(varchar," + selruts + ") in(select * from SplitString('" + rutv + "',',')) ";
        }
        if (retailv != "")
        {
            strQry += " and CONVERT(varchar," + selrtls + ") in(select * from SplitString('" + retailv + "',',')) ";
        }
        if (chnlv != "")
        {
            strQry += " and CONVERT(varchar," + selchnls + ") in(select * from SplitString('" + chnlv + "',',')) ";
        }
        if (arrayval == "")
        {
            strQry += " GROUP BY Trans_Sl_No,Order_Date,Product_Code,Product_Detail_Name";
            strQry += " order by Order_Date asc ";
        }
        if (arrayval != "")
        {
            strQry += " GROUP BY  " + arrayval + ",Product_Code,Product_Detail_Name";
            strQry += " order by " + arrayval + " asc ";
        }
        //strQry += " order by Order_Date asc OFFSET " + pageindex + " ROWS FETCH NEXT " + plimt + " ROWS ONLY ";
      
        SqlCommand cmd = new SqlCommand(strQry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(ds);
        con.Close();
        return JsonConvert.SerializeObject(ds);
    }
    [WebMethod(EnableSession = true)]
    public static string Bindfillqty_view(string divcode, int pageindex, int plimt, string fdate, string tdate, string odatv, string distv, string ordbyv, string mgrv, string erpv, string rutv, string retailv, string chnlv, string selovals, string selorbvals, string seldvals, string selmvals, string selerps, string selruts, string selrtls, string selchnls,string arrayval)
    {
        SalesForce dv = new SalesForce();
        DataTable ds = new DataTable();
        SqlConnection con = new SqlConnection(Globals.ConnString);
        con.Open();
        string strQry = " WITH UserCTE AS (SELECT SF_Code,Sf_Name, Reporting_To_SF,sf_type,0 AS steps, subdivision_code,State_Code " +
                       " FROM Mas_Salesforce WHERE Sf_Code = '" + Sf_Code + "' and charindex(','+cast('" + divcode + "' as varchar)+',',','+Division_Code+',')>0 " +
                       " UNION ALL SELECT mgr.SF_Code, mgr.Sf_Name, mgr.Reporting_To_SF,mgr.sf_type, usr.steps +1 AS steps,mgr.subdivision_code,mgr.State_Code " +
                       " FROM UserCTE AS usr INNER JOIN Mas_Salesforce AS mgr ON usr.Sf_Code = mgr.Reporting_To_SF and charindex(','+cast('" + divcode + "' as varchar)+',',','+mgr.Division_Code+',')>0 " +
                       " UNION ALL SELECT mgr.SF_Code, mgr.Sf_Name, null Reporting_To_SF,mgr.sf_type, usr.steps +1 AS steps,mgr.subdivision_code,mgr.State_Code FROM UserCTE AS usr " +
                       " INNER JOIN Mas_Sf_Audit_Team AS aud ON charindex(','+usr.SF_code+',',','+aud.Audit_Team+',')>0 INNER JOIN Mas_Salesforce AS mgr " +
                       " on mgr.SF_Code=aud.Sf_Code and charindex(','+cast('" + divcode + "' as varchar)+',',','+mgr.Division_Code+',')>0), " +
                       " transh AS(select * from Trans_Order_Head where Div_ID='" + divcode + "'  and CONVERT(date,order_date) between '" + fdate + "' and '" + tdate + "'), " +
                       " tab as(select CONVERT(varchar,h.Order_Date,113)OrderDate,h.Trans_Sl_No,CONVERT(varchar, h.Order_Date,103)Order_Date, " +
                       " IIF(h.OrderType = '0', 'Field Order', iif(h.OrderType = '1', 'Phone Order', 'Van Sales'))OrderType,ms.stockist_code,ms.Stockist_name,ms.ERP_Code," +
                       " ct.Sf_Code mr,ct.SF_Name,ct.Reporting_To_SF rsfc,mm.Sf_Name RSF,(select t.Territory_Name from Mas_Territory_creation t where t.Territory_code = h.route and t.Territory_Active_Flag = 0) as routename,  " +
                       " t.Territory_code,l.ListedDrCode,l.Doc_Special_Code,(select l.ListedDr_Name from Mas_Listeddr l where l.ListedDrCode=h.Cust_Code and l.ListedDr_Active_Flag=0) as retailername,  " +
                       " (select l.Doc_Spec_ShortName from Mas_Listeddr l where l.ListedDrCode = h.Cust_Code and l.ListedDr_Active_Flag = 0) as channel, " +
                       " h.net_weight_value,round(isnull(h.Order_value,0),2) Order_value,ct.subdivision_code,ct.State_Code from transh h inner join Trans_Order_Details d on h.Trans_Sl_No = d.Trans_Sl_No " +
                       " inner join UserCTE ct on ct.Sf_Code = h.Sf_Code inner join Mas_Salesforce mm on mm.Sf_Code = ct.Reporting_To_SF  " +
                       " inner join  Mas_Territory_creation t on t.Territory_code = h.route and  t.Territory_Active_Flag = 0 " +
                       " inner join Mas_Listeddr l on l.ListedDrCode=h.Cust_Code and l.ListedDr_Active_Flag=0 " +
                       " inner join Mas_Stockist ms on ms.stockist_code=h.Stockist_Code)";
        if (arrayval == "")
        {
            strQry += " select Trans_Sl_No,Order_Date,Stockist_name,ERP_Code,SF_Name,RSF,OrderType,routename,retailername,channel,net_weight_value,Order_value from tab where ";
        }
        if (arrayval != "")
        {
            strQry += " select " + arrayval + ",round(sum(net_weight_value),2)net_weight_value,round(sum(distinct Order_value),2)Order_value from tab where";
        }
        strQry += " CHARINDEX(',' + iif('" + subdiv_code + "'= 0, subdivision_code, '" + subdiv_code + "') + ',', ',' + subdivision_code + ',') > 0 and  " +
                  " State_Code=IIF('" + statecode + "'=0,State_Code,'" + statecode + "')";

       
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
        if (mgrv != "")
        {
            strQry += " and CONVERT(varchar," + selmvals + ") in(select * from SplitString('" + mgrv + "',','))";
        }
        if (erpv != "")
        {
            strQry += " and CONVERT(varchar," + selerps + ") in(select * from SplitString('" + erpv + "',',')) ";
        }
        if (rutv != "")
        {
            strQry += " and CONVERT(varchar," + selruts + ") in(select * from SplitString('" + rutv + "',',')) ";
        }
        if (retailv != "")
        {
            strQry += " and CONVERT(varchar," + selrtls + ") in(select * from SplitString('" + retailv + "',',')) ";
        }
        if (chnlv != "")
        {
            strQry += " and CONVERT(varchar," + selchnls + ") in(select * from SplitString('" + chnlv + "',',')) ";
        }
        if (arrayval == "")
        {
            strQry += " GROUP BY Trans_Sl_No,Order_Date,Stockist_name,ERP_Code,SF_Name,RSF,OrderType,routename,retailername,channel,net_weight_value,Order_value";
            strQry += " order by Order_Date asc  OFFSET " + pageindex + " ROWS FETCH NEXT " + plimt + " ROWS ONLY";
        }
        if (arrayval != "")
        {
            strQry += " GROUP BY  " + arrayval + "";
            strQry += " order by " + arrayval + " asc  OFFSET " + pageindex + " ROWS FETCH NEXT " + plimt + " ROWS ONLY";
        }
        SqlCommand cmd = new SqlCommand(strQry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(ds);
        con.Close();
        return JsonConvert.SerializeObject(ds);
    }

    [WebMethod(EnableSession = true)]
    public static string totalcnt(string divcode, string fdate, string tdate, string odatv, string distv, string ordbyv, string mgrv, string erpv, string rutv, string retailv, string chnlv, string selovals, string selorbvals, string seldvals, string selmvals, string selerps, string selruts, string selrtls, string selchnls,string arrayval)
    {
        SalesForce dv = new SalesForce();
        DataTable ds = new DataTable();
        SqlConnection con = new SqlConnection(Globals.ConnString);
        con.Open();
        string strQry = " WITH UserCTE AS (SELECT SF_Code,Sf_Name, Reporting_To_SF,sf_type,0 AS steps, subdivision_code,State_Code " +
                      " FROM Mas_Salesforce WHERE Sf_Code = '" + Sf_Code + "' and charindex(','+cast('" + divcode + "' as varchar)+',',','+Division_Code+',')>0 " +
                      " UNION ALL SELECT mgr.SF_Code, mgr.Sf_Name, mgr.Reporting_To_SF,mgr.sf_type, usr.steps +1 AS steps,mgr.subdivision_code,mgr.State_Code " +
                      " FROM UserCTE AS usr INNER JOIN Mas_Salesforce AS mgr ON usr.Sf_Code = mgr.Reporting_To_SF and charindex(','+cast('" + divcode + "' as varchar)+',',','+mgr.Division_Code+',')>0 " +
                      " UNION ALL SELECT mgr.SF_Code, mgr.Sf_Name, null Reporting_To_SF,mgr.sf_type, usr.steps +1 AS steps,mgr.subdivision_code,mgr.State_Code FROM UserCTE AS usr " +
                      " INNER JOIN Mas_Sf_Audit_Team AS aud ON charindex(','+usr.SF_code+',',','+aud.Audit_Team+',')>0 INNER JOIN Mas_Salesforce AS mgr " +
                      " on mgr.SF_Code=aud.Sf_Code and charindex(','+cast('" + divcode + "' as varchar)+',',','+mgr.Division_Code+',')>0), " +
                      " transh AS(select * from Trans_Order_Head where Div_ID='" + divcode + "'  and CONVERT(date,order_date) between '" + fdate + "' and '" + tdate + "'), " +
                      " tab as(select CONVERT(varchar,h.Order_Date,113)OrderDate,h.Trans_Sl_No,CONVERT(varchar, h.Order_Date,103)Order_Date, " +
                      " IIF(h.OrderType = '0', 'Field Order', iif(h.OrderType = '1', 'Phone Order', 'Van Sales'))OrderType,ms.stockist_code,ms.Stockist_name,ms.ERP_Code," +
                      " ct.Sf_Code mr,ct.SF_Name,ct.Reporting_To_SF rsfc,mm.Sf_Name RSF,(select t.Territory_Name from Mas_Territory_creation t where t.Territory_code = h.route and t.Territory_Active_Flag = 0) as routename,  " +
                      " t.Territory_code,l.ListedDrCode,l.Doc_Special_Code,(select l.ListedDr_Name from Mas_Listeddr l where l.ListedDrCode=h.Cust_Code and l.ListedDr_Active_Flag=0) as retailername,  " +
                      " (select l.Doc_Spec_ShortName from Mas_Listeddr l where l.ListedDrCode = h.Cust_Code and l.ListedDr_Active_Flag = 0) as channel, " +
                      " h.net_weight_value,round(isnull(h.Order_value,0),2) Order_value,ct.subdivision_code,ct.State_Code from transh h inner join Trans_Order_Details d on h.Trans_Sl_No = d.Trans_Sl_No " +
                      " inner join UserCTE ct on ct.Sf_Code = h.Sf_Code inner join Mas_Salesforce mm on mm.Sf_Code = ct.Reporting_To_SF  " +
                      " inner join  Mas_Territory_creation t on t.Territory_code = h.route and  t.Territory_Active_Flag = 0 " +
                      " inner join Mas_Listeddr l on l.ListedDrCode=h.Cust_Code and l.ListedDr_Active_Flag=0 " +
                      " inner join Mas_Stockist ms on ms.stockist_code=h.Stockist_Code)";
        if (arrayval == "")
        {
            strQry += " select Trans_Sl_No,Order_Date,Stockist_name,ERP_Code,SF_Name,RSF,OrderType,routename,retailername,channel,net_weight_value,Order_value from tab where ";
        }
        if (arrayval != "")
        {
            strQry += " select " + arrayval + ",round(sum(net_weight_value),21)net_weight_value,round(sum(distinct Order_value),2)Order_value from tab where";
        }
        strQry += " CHARINDEX(',' + iif('" + subdiv_code + "'= 0, subdivision_code, '" + subdiv_code + "') + ',', ',' + subdivision_code + ',') > 0 and  " +
                  " State_Code=IIF('" + statecode + "'=0,State_Code,'" + statecode + "')";


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
        if (mgrv != "")
        {
            strQry += " and CONVERT(varchar," + selmvals + ") in(select * from SplitString('" + mgrv + "',','))";
        }
        if (erpv != "")
        {
            strQry += " and CONVERT(varchar," + selerps + ") in(select * from SplitString('" + erpv + "',',')) ";
        }
        if (rutv != "")
        {
            strQry += " and CONVERT(varchar," + selruts + ") in(select * from SplitString('" + rutv + "',',')) ";
        }
        if (retailv != "")
        {
            strQry += " and CONVERT(varchar," + selrtls + ") in(select * from SplitString('" + retailv + "',',')) ";
        }
        if (chnlv != "")
        {
            strQry += " and CONVERT(varchar," + selchnls + ") in(select * from SplitString('" + chnlv + "',',')) ";
        }
        if (arrayval == "")
        {
            strQry += " GROUP BY Trans_Sl_No,Order_Date,Stockist_name,ERP_Code,SF_Name,RSF,OrderType,routename,retailername,channel,net_weight_value,Order_value";
            strQry += " order by Order_Date asc";
        }
        if (arrayval != "")
        {
            strQry += " GROUP BY  " + arrayval + "";
            strQry += " order by " + arrayval + " asc";
        }
    
        SqlCommand cmd = new SqlCommand(strQry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(ds);
        con.Close();
        return JsonConvert.SerializeObject(ds);
    }

    [WebMethod(EnableSession = true)]
    public static string Getorderdateval(string divcode, string fdate, string tdate, string odatv, string distv, string ordbyv, string mgrv, string erpv, string rutv, string retailv, string chnlv, string selovals, string selorbvals, string seldvals, string selmvals, string selerps, string selruts, string selrtls, string selchnls)
    {
        SalesForce dv = new SalesForce();
        DataTable ds = new DataTable();
        SqlConnection con = new SqlConnection(Globals.ConnString);
        con.Open();
        string strQry = " WITH UserCTE AS (SELECT SF_Code,Sf_Name, Reporting_To_SF,sf_type,0 AS steps, subdivision_code,State_Code " +
                       " FROM Mas_Salesforce WHERE Sf_Code = '" + Sf_Code + "' and charindex(','+cast('" + divcode + "' as varchar)+',',','+Division_Code+',')>0 " +
                       " UNION ALL SELECT mgr.SF_Code, mgr.Sf_Name, mgr.Reporting_To_SF,mgr.sf_type, usr.steps +1 AS steps,mgr.subdivision_code,mgr.State_Code " +
                       " FROM UserCTE AS usr INNER JOIN Mas_Salesforce AS mgr ON usr.Sf_Code = mgr.Reporting_To_SF and charindex(','+cast('" + divcode + "' as varchar)+',',','+mgr.Division_Code+',')>0 " +
                       " UNION ALL SELECT mgr.SF_Code, mgr.Sf_Name, null Reporting_To_SF,mgr.sf_type, usr.steps +1 AS steps,mgr.subdivision_code,mgr.State_Code FROM UserCTE AS usr " +
                       " INNER JOIN Mas_Sf_Audit_Team AS aud ON charindex(','+usr.SF_code+',',','+aud.Audit_Team+',')>0 INNER JOIN Mas_Salesforce AS mgr " +
                       " on mgr.SF_Code=aud.Sf_Code and charindex(','+cast('" + divcode + "' as varchar)+',',','+mgr.Division_Code+',')>0), " +
                       "  tab as(select CONVERT(date, h.Order_Date)date,CONVERT(varchar, h.Order_Date,103)Order_Date,ct.Sf_Code mr,ct.Sf_Name mrn,mm.Sf_Code mgr,mm.Sf_Name mgrn," +
                       " ms.stockist_code,ms.Stockist_Name,ms.ERP_Code,t.Territory_Code,t.Territory_Name,l.ListedDrCode,l.ListedDr_Name,l.Doc_Special_Code,l.Doc_Spec_ShortName " +
                       " from Trans_Order_Head h inner join UserCTE ct on ct.Sf_Code=h.Sf_Code  " +
                       " inner join  Mas_Territory_creation t on t.Territory_code = h.route and  t.Territory_Active_Flag = 0 " +
                       " inner join Mas_Listeddr l on l.ListedDrCode=h.Cust_Code and l.ListedDr_Active_Flag=0" +
                       " inner join Mas_Stockist ms on ms.stockist_code=h.Stockist_Code " +
                       " inner join Mas_Salesforce mm on mm.Sf_Code=ct.Reporting_To_SF " +
                       " where CHARINDEX(','+iif('" + subdiv_code + "'=0,ct.subdivision_code,'" + subdiv_code + "')+',',','+ct.subdivision_code+',')>0 and h.Div_ID='" + divcode + "' and " +
                       " ct.State_Code=IIF('" + statecode + "'=0,ct.State_Code,'" + statecode + "'))";

               strQry += " select Order_Date,mr,mrn,mgr,mgrn,stockist_code,Stockist_Name,ERP_Code,Territory_Code,Territory_Name,ListedDrCode,ListedDr_Name,Doc_Special_Code," +
                         " Doc_Spec_ShortName from tab where CONVERT(date,date) between '" + fdate + "' and '" + tdate + "'";

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
        if (mgrv != "")
        {
            strQry += " and charindex(','+ CONVERT(varchar," + selmvals + ")+',',','+ IIF('" + mgrv + "'='',CONVERT(varchar," + selmvals + "),'" + mgrv + "')+',')>0";
        }
        if (erpv != "")
        {
            strQry += " and charindex(','+ CONVERT(varchar," + selerps + ")+',',','+ IIF('" + erpv + "'='',CONVERT(varchar," + selerps + "),'" + erpv + "')+',')>0";
        }
        if (rutv != "")
        {
            strQry += " and charindex(','+ CONVERT(varchar," + selruts + ")+',',','+ IIF('" + rutv + "'='',CONVERT(varchar," + selruts + "),'" + rutv + "')+',')>0";
        }
        if (retailv != "")
        {
            strQry += " and charindex(','+ CONVERT(varchar," + selrtls + ")+',',','+ IIF('" + retailv + "'='',CONVERT(varchar," + selrtls + "),'" + retailv + "')+',')>0";
        }
        if (chnlv != "")
        {
            strQry += " and charindex(','+ CONVERT(varchar," + selchnls + ")+',',','+ IIF('" + chnlv + "'='',CONVERT(varchar," + selchnls + "),'" + chnlv + "')+',')>0";
        }

        strQry += " group by Order_Date,mr,mrn,mgr,mgrn,stockist_code,Stockist_Name,ERP_Code,Territory_Code,Territory_Name,ListedDrCode,ListedDr_Name,Doc_Special_Code,Doc_Spec_ShortName";

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
