using Bus_EReport;
using Newtonsoft.Json;
using System;
using System.Data;
using System.Web.Services;
using System.Data.SqlClient;
using DBase_EReport;
public partial class MIS_Reports_new_Secsales_filter : System.Web.UI.Page
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
        rpt = "Secondary Sales Order";
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
    public static string Bindfillqty_view(string divcode, int pageindex, int plimt, string fdate, string tdate, string invdtv, string ordtv, string orbyv, string cusvv, string disvv, string selinvdtv, string selordtv, string selorbvalv, string selcusvv, string seldisvv, string arrayval)
    {
        SalesForce dv = new SalesForce();
        DataTable ds = new DataTable();
        SqlConnection con = new SqlConnection(Globals.ConnString);
        con.Open();
        string strQry = " WITH  trans AS(select * from Trans_Invoice_Head where  CONVERT(date, Invoice_Date) between '" + fdate + "' and '" + tdate + "' " +
                        " and Division_Code='" + divcode + "'  " +
                        " order by Order_Date OFFSET   " + pageindex + " ROWS FETCH NEXT  " + plimt + " ROWS ONLY), " +
                        " tab as(select Trans_Inv_Slno,th.Sf_Code,th.Sf_Name,Cus_Name,Dis_Code,Dis_Name,CONVERT(varchar,Invoice_Date,103) as Invoice_Date,convert(varchar,Order_Date,103) as Order_Date,Adv_Paid,Amt_Due," +
                        " Total,Division_Code from trans th)";
        if (arrayval == "")
        {
            strQry += " select Trans_Inv_Slno,Sf_Code,Sf_Name,Cus_Name,Dis_Code,Dis_Name,Invoice_Date,Order_Date,Adv_Paid,Amt_Due,Total from tab  where Division_Code='" + divcode + "'";
        }
        if (arrayval != "")
        {
            strQry += " select " + arrayval + ",sum(Total)Total from tab where  Division_Code='" + divcode + "'";
        }
        if (invdtv != "")
        {
            strQry += " and CONVERT(varchar," + selinvdtv + ") in(select * from SplitString('" + invdtv + "',',')) ";
        }
        if (ordtv != "")
        {
            strQry += " and CONVERT(varchar," + selordtv + ") in(select * from SplitString('" + ordtv + "',','))";
        }
        if (orbyv != "")
        {
            strQry += " and CONVERT(varchar," + selorbvalv + ") in(select * from SplitString('" + orbyv + "',','))";
        }
        if (cusvv != "")
        {
            strQry += " and CONVERT(varchar," + selcusvv + ") in(select * from SplitString('" + cusvv + "',','))";
        }
        if (disvv != "")
        {
            strQry += " and CONVERT(varchar," + seldisvv + ") in(select * from SplitString('" + disvv + "',','))";
        }
        if (arrayval == "")
        {
            strQry += " group by Trans_Inv_Slno,Sf_Code,Sf_Name,Cus_Name,Dis_Code,Dis_Name,Invoice_Date,Order_Date,Adv_Paid,Amt_Due,Total";
        }
        if (arrayval != "")
        {
            strQry += " group by " + arrayval + "";
        }
        SqlCommand cmd = new SqlCommand(strQry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(ds);
        con.Close();
        return JsonConvert.SerializeObject(ds);
    }
    [WebMethod(EnableSession = true)]
    public static string totalcnt(string divcode, string fdate, string tdate, string invdtv, string ordtv, string orbyv, string cusvv, string disvv, string selinvdtv, string selordtv, string selorbvalv, string selcusvv, string seldisvv, string arrayval)
    {
        SalesForce dv = new SalesForce();
        DataTable ds = new DataTable();
        SqlConnection con = new SqlConnection(Globals.ConnString);
        con.Open();
        string strQry = " WITH  trans AS(select * from Trans_Invoice_Head where  CONVERT(date, Invoice_Date) between '" + fdate + "' and '" + tdate + "' " +
                        " and Division_Code='" + divcode + "' ), " +
                        " tab as(select Trans_Inv_Slno,th.Sf_Code,th.Sf_Name,Cus_Name,Dis_Code,Dis_Name,CONVERT(varchar,Invoice_Date,103) as Invoice_Date,convert(varchar,Order_Date,103) as Order_Date,Adv_Paid,Amt_Due," +
                        " Total,Division_Code from trans th)";
        if (arrayval == "")
        {
            strQry += " select Trans_Inv_Slno,Sf_Code,Sf_Name,Cus_Name,Dis_Code,Dis_Name,Invoice_Date,Order_Date,Adv_Paid,Amt_Due,Total from tab  where Division_Code='" + divcode + "'";
        }
        if (arrayval != "")
        {
            strQry += " select " + arrayval + ",sum(Total)Total from tab where  Division_Code='" + divcode + "'";
        }
        if (invdtv != "")
        {
            strQry += " and CONVERT(varchar," + selinvdtv + ") in(select * from SplitString('" + invdtv + "',',')) ";
        }
        if (ordtv != "")
        {
            strQry += " and CONVERT(varchar," + selordtv + ") in(select * from SplitString('" + ordtv + "',','))";
        }
        if (orbyv != "")
        {
            strQry += " and CONVERT(varchar," + selorbvalv + ") in(select * from SplitString('" + orbyv + "',','))";
        }
        if (cusvv != "")
        {
            strQry += " and CONVERT(varchar," + selcusvv + ") in(select * from SplitString('" + cusvv + "',','))";
        }
        if (disvv != "")
        {
            strQry += " and CONVERT(varchar," + seldisvv + ") in(select * from SplitString('" + disvv + "',','))";
        }
        if (arrayval == "")
        {
            strQry += " group by Trans_Inv_Slno,Sf_Code,Sf_Name,Cus_Name,Dis_Code,Dis_Name,Invoice_Date,Order_Date,Adv_Paid,Amt_Due,Total";
        }
        if (arrayval != "")
        {
            strQry += " group by " + arrayval + "";
        }
        SqlCommand cmd = new SqlCommand(strQry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(ds);
        con.Close();
        return JsonConvert.SerializeObject(ds);
    }
    [WebMethod(EnableSession = true)]
    public static string Getorderdateval(string divcode, string fdate, string tdate, string invdtv, string ordtv, string orbyv, string cusvv, string disvv, string selinvdtv, string selordtv, string selorbvalv, string selcusvv, string seldisvv)
    {
        SalesForce dv = new SalesForce();
        DataTable ds = new DataTable();
        SqlConnection con = new SqlConnection(Globals.ConnString);
        con.Open();
        string strQry = " WITH trans AS(select * from Trans_Invoice_Head where  CONVERT(date, Invoice_Date) between '" + fdate + "' and '" + tdate + "'  " +
                        " and Division_Code='" + divcode + "') " +
                        " select Trans_Inv_Slno,th.Sf_Code,th.Sf_Name,Cus_Code,Cus_Name,Dis_Code,Dis_Name,CONVERT(varchar,Invoice_Date,103) as " +
                        " Invoice_Date,convert(varchar(100),Order_Date,103) as Order_Date,Adv_Paid,Amt_Due," +
                        " Total from trans th where  Division_Code='" + divcode + "'";

        if (invdtv != "")
        {
            strQry += " and charindex(','+ CONVERT(varchar," + selinvdtv + ")+',',','+ IIF('" + invdtv + "'='',CONVERT(varchar," + selinvdtv + "),'" + invdtv + "')+',')>0 ";
        }
        if (ordtv != "")
        {
            strQry += " and charindex(','+ CONVERT(varchar," + selordtv + ")+',',','+ IIF('" + ordtv + "'='',CONVERT(varchar," + selordtv + "),'" + ordtv + "')+',')>0";
        }
        if (orbyv != "")
        {
            strQry += " and charindex(','+ CONVERT(varchar," + selorbvalv + ")+',',','+ IIF('" + orbyv + "'='',CONVERT(varchar," + selorbvalv + "),'" + orbyv + "')+',')>0";
        }
        if (cusvv != "")
        {
            strQry += " and charindex(','+ CONVERT(varchar," + selcusvv + ")+',',','+ IIF('" + cusvv + "'='',CONVERT(varchar," + selcusvv + "),'" + cusvv + "')+',')>0";
        }
        if (disvv != "")
        {
            strQry += " and charindex(','+ CONVERT(varchar," + seldisvv + ")+',',','+ IIF('" + disvv + "'='',CONVERT(varchar," + seldisvv + "),'" + disvv + "')+',')>0";
        }
        strQry += " group by Trans_Inv_Slno,th.Sf_Code,th.Sf_Name,Cus_Code,Cus_Name,Dis_Code,Dis_Name,CONVERT(varchar,Invoice_Date,103),convert(varchar(100),Order_Date,103),Adv_Paid,Amt_Due,Total";
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
        secsale dv = new secsale();
        DataSet dsProd = dv.get_selectfields_md(divcode, rpt);
        return JsonConvert.SerializeObject(dsProd.Tables[0]);

    }
    public class secsale
    { 
    public DataSet get_selectfields_md(string divcode, string rpt)
    {
        DB_EReporting db_ER = new DB_EReporting();

        DataSet dsDivision = null;
        string strQry = "select * from Rpt_field_setting_sec where Division_Code='" + divcode + "' and Report_name='" + rpt + "'";
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