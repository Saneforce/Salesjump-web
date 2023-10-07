using Bus_EReport;
using Newtonsoft.Json;
using System;
using System.Data;
using System.Web.Services;
using System.Data.SqlClient;
using DBase_EReport;

public partial class MIS_Reports_new_primarysales_filter : System.Web.UI.Page
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
        rpt = "Primary Sales Order";
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
    public static string Bindfillqty_view(string divcode, int pageindex, int plimt, string fdate, string tdate, string invdtv, string ordtv, string orbyv, string cusvv, string selinvdtv, string selordtv, string selorbvalv, string selcusvv, string arrayval)
    {
        SalesForce dv = new SalesForce();
        DataTable ds = new DataTable();
        SqlConnection con = new SqlConnection(Globals.ConnString);
        con.Open();
        string strQry = " WITH  trans AS(select * from Trans_GRN_head where  CONVERT(date, GRN_Date) between '" + fdate + "' and '" + tdate + "' " +
                        " and Division_Code='" + divcode + "'  " +
                        " order by GRN_Date OFFSET   " + pageindex + " ROWS FETCH NEXT  " + plimt + " ROWS ONLY), " +
                        " tab as(select Trans_Sl_No,GRN_No,CONVERT(varchar(100), GRN_Date, 103) as GRN_Date,CONVERT(varchar, Entry_Date, 103) as Entry_Date, " +
                        " Supp_Code,Supp_Name,Po_No,Net_Tot_Goods,Net_Tot_Tax,Net_Tot_Value,Division_Code from trans)";
        if (arrayval == "")
        {
            strQry += " select Trans_Sl_No,GRN_No,GRN_Date,Entry_Date,Supp_Code,Supp_Name,Po_No,Net_Tot_Goods,Net_Tot_Tax,Net_Tot_Value from tab where Division_Code='" + divcode + "'";
        }
        if (arrayval != "")
        {
            strQry += " select " + arrayval + ",sum(Net_Tot_Goods)Net_Tot_Goods,sum(Net_Tot_Tax)Net_Tot_Tax,sum(Net_Tot_Value)Net_Tot_Value from tab where Division_Code='" + divcode + "'";
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
        if (arrayval == "")
        {
            strQry += " group by Trans_Sl_No,GRN_No,GRN_Date,Entry_Date,Supp_Code,Supp_Name,Po_No,Net_Tot_Goods,Net_Tot_Tax,Net_Tot_Value";
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
    public static string totalcnt(string divcode, string fdate, string tdate, string invdtv, string ordtv, string orbyv, string cusvv,string selinvdtv, string selordtv, string selorbvalv, string selcusvv, string arrayval)
    {
        SalesForce dv = new SalesForce();
        DataTable ds = new DataTable();
        SqlConnection con = new SqlConnection(Globals.ConnString);
        con.Open();
        string strQry = " WITH  trans AS(select * from Trans_GRN_head where  CONVERT(date, GRN_Date) between '" + fdate + "' and '" + tdate + "' " +
                        " and Division_Code='" + divcode + "'), " +
                        " tab as(select Trans_Sl_No,GRN_No,CONVERT(varchar(100), GRN_Date, 103) as GRN_Date,CONVERT(varchar, Entry_Date, 103) as Entry_Date, " +
                        " Supp_Code,Supp_Name,Po_No,Net_Tot_Goods,Net_Tot_Tax,Net_Tot_Value,Division_Code from trans)";
        if (arrayval == "")
        {
            strQry += " select Trans_Sl_No,GRN_No,GRN_Date,Entry_Date,Supp_Code,Supp_Name,Po_No,Net_Tot_Goods,Net_Tot_Tax,Net_Tot_Value from tab where Division_Code='" + divcode + "'";
        }
        if (arrayval != "")
        {
            strQry += " select " + arrayval + ",sum(Net_Tot_Goods)Net_Tot_Goods,sum(Net_Tot_Tax)Net_Tot_Tax,sum(Net_Tot_Value)Net_Tot_Value from tab where Division_Code='" + divcode + "'";
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
        if (arrayval == "")
        {
            strQry += " group by Trans_Sl_No,GRN_No,GRN_Date,Entry_Date,Supp_Code,Supp_Name,Po_No,Net_Tot_Goods,Net_Tot_Tax,Net_Tot_Value";
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
    public static string Getorderdateval(string divcode, string fdate, string tdate, string invdtv, string ordtv, string orbyv, string cusvv, string selinvdtv, string selordtv, string selorbvalv, string selcusvv)
    {
        SalesForce dv = new SalesForce();
        DataTable ds = new DataTable();
        SqlConnection con = new SqlConnection(Globals.ConnString);
        con.Open();
        string strQry = " WITH  trans AS(select * from Trans_GRN_head where  CONVERT(date, GRN_Date) between '" + fdate + "' and '" + tdate + "' " +
                       " and Division_Code='" + divcode + "'), " +
                       " tab as(select Trans_Sl_No,GRN_No,CONVERT(varchar(100), GRN_Date, 103) as GRN_Date,CONVERT(varchar, Entry_Date, 103) as Entry_Date, " +
                       " Supp_Code,Supp_Name,Po_No,Net_Tot_Goods,Net_Tot_Tax,Net_Tot_Value,Division_Code from trans)";

        strQry += "select Trans_Sl_No,GRN_No,GRN_Date,Entry_Date,Supp_Code,Supp_Name,Po_No,Net_Tot_Goods,Net_Tot_Tax,Net_Tot_Value from tab  where Division_Code='" + divcode + "'";
       
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
       
        strQry += " group by Trans_Sl_No,GRN_No,GRN_Date,Entry_Date,Supp_Code,Supp_Name,Po_No,Net_Tot_Goods,Net_Tot_Tax,Net_Tot_Value ";
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