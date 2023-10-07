using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using System.Web.Services;
using Bus_EReport;
using System.Data;
using System.Data.SqlClient;
using DBase_EReport;

public partial class MasterFiles_rpt_Expense_Entry_Modified_Aachi : System.Web.UI.Page
{
    public static string SF_Code; public static string Mn; public static string Yr;
    public static string Frm_date; public static string To_Date;
    public static string div_code = string.Empty;
    public static string SFname = string.Empty;
    public static string EmpID = string.Empty;
    public static string Pri_ID = string.Empty;    
    public static string Pri_Nm = string.Empty;    
    public static string Approval_flag;
	public static string SF_DesgID = string.Empty;
	public static string Url = string.Empty;
	public static string state = string.Empty;

    
    protected void Page_Load(object sender, EventArgs e)
    {
        Url = Request.Url.Host.ToLower().Replace("www.", "").Replace("/MasterFiles/rpt_Expense_Entry_Modified_Aachi.aspx", "").ToLower();
        div_code = Session["div_code"].ToString();
        SF_Code = Request.QueryString["SF_Code"].ToString();
        SFname = Request.QueryString["SF_name"].ToString();
        EmpID = Request.QueryString["SF_EmpID"].ToString();
        Mn = Request.QueryString["FMonth"].ToString();
        Yr = Request.QueryString["FYear"].ToString();
        Frm_date = Request.QueryString["Fdt"].ToString();
        To_Date = Request.QueryString["Tdt"].ToString();
        Pri_ID = Request.QueryString["PID"].ToString();
        Pri_Nm = Request.QueryString["PNm"].ToString();
		SF_DesgID = Request.QueryString["SF_DesgID"].ToString();
	state = Request.QueryString["statecode"].ToString();
        sfname.Value = SFname;
    }
	 [WebMethod]
    public static string GetApprNm(string Sfcode)
    {
        DataSet ds = new DataSet();
        string Divi_code = HttpContext.Current.Session["div_code"].ToString();
        DB_EReporting db_ElR = new DB_EReporting();
        string result = "";
        string Qry = "select * from Mas_SalesForce where SF_Code='"+ Sfcode + "'";
        try
        {
            ds = db_ElR.Exec_DataSet(Qry);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        result = JsonConvert.SerializeObject(ds.Tables[0]);
        return result;
    }
	 [WebMethod]
    public static string rejectExpense_Condition(string SF, string sEmpID)
    {
        DB_EReporting db_ER = new DB_EReporting();
        DataSet ds = new DataSet();
        string msg = string.Empty;
        string strQry = "select iif(SF_Status!= 0,'This user is in vacant/deactive so this expense cannot reject!!!','NotExist')Msg from Mas_Salesforce where Sf_Code ='" + SF + "' and sf_emp_id ='" + sEmpID + "' and Charindex(',' + '" + div_code + "' + ',',',' + Division_Code + ',')> 0";
        try
        {
            ds = db_ER.Exec_DataSet(strQry);
            if (ds.Tables[0].Rows.Count != 0)
            {
                msg = ds.Tables[0].Rows[0]["Msg"].ToString();
            }
        }
        catch (Exception ex)
        {
            msg = ex.Message;
            throw ex;
        }
        return msg;
    }
    [WebMethod]
    public static string GetApprNmDetails()
    {
        DataSet ds = new DataSet();
        string Divi_code = HttpContext.Current.Session["div_code"].ToString();
        DB_EReporting db_ElR = new DB_EReporting();
        string result = "";
        string Qry = "select * from Trans_Expense_Head_Periodic where Periodic_ID="+Pri_ID+" and Expense_Month="+Mn+" and Expense_Year="+Yr+" and SF_Code='"+ SF_Code + "'";
        try
        {
            ds = db_ElR.Exec_DataSet(Qry);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        result = JsonConvert.SerializeObject(ds.Tables[0]);
        return result;
    }
 [WebMethod]
    public static string GetDivisionNm()
    {
        DataSet ds = new DataSet();        
        DB_EReporting db_ElR = new DB_EReporting();
        string result = "";
        string Qry = "select Division_Name from Mas_Division where Division_Code="+ div_code +"";
        try
        {
            ds = db_ElR.Exec_DataSet(Qry);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        result = JsonConvert.SerializeObject(ds.Tables[0].Rows[0]["Division_Name"].ToString());
        return result;
    }
    [WebMethod]
    public static string GetMotDet()
    {
        DB_EReporting db_ER = new DB_EReporting();

        DataTable dsDivision = new DataTable();
        string dsDivisionstr = "";
        string strQry = "select * from Mas_Modeof_Travel where Division_Code='" + div_code + "'";
        try
        {
            dsDivision = db_ER.Exec_DataTable(strQry);
        }
        catch (Exception ex)
        {
            dsDivision.Dispose();
            throw ex;
        }
        dsDivisionstr = JsonConvert.SerializeObject(dsDivision).ToString();
        dsDivision.Dispose();
        return dsDivisionstr;
    }
    [WebMethod]
    public static string GetApprovedDet()
    {
        DB_EReporting db_ER = new DB_EReporting();
        string result = "";
        DataSet AppDet = null;
        string strQry = "select iif(isnull(Appr_By,'')='',ad.Appr_SF_Name,ms.sf_name)ApprvNm from Trans_Expense_Head_Periodic th inner join Approval_Hyr_Head ah on th.sf_code=ah.Sf_Code " +
            " inner join Approval_Hyr_Detail ad on ad.FKSlno=ah.Slno left join Mas_Salesforce ms on ms.Sf_Code=th.Approved_By " +
            " where th.SF_Code='" + SF_Code + "' and ad.Appr_Lvl=1 and Appr_Name='Expense' and Expense_Month=" + Mn + " and Expense_Year=" + Yr + " and Periodic_ID=" + Pri_ID + "";
        try
        {
            AppDet = db_ER.Exec_DataSet(strQry);
        }
        catch (Exception ex)
        {
            throw ex;
        }
		if (AppDet.Tables[0].Rows.Count > 0)
			result = AppDet.Tables[0].Rows[0]["ApprvNm"].ToString();
        AppDet.Dispose();
        return result;
    }
    [WebMethod]
    public static string GetExpenseModeoftravel()
    {
        DB_EReporting db_ER = new DB_EReporting();
        DataSet ds = new DataSet();
        string strQry = "select 0 Fuel_Charge,MOT,Alw_Eligibilty,Sl_No,'' Effective_Date,'' rw from Mas_Modeof_Travel where MOT='None' and Division_Code=" + div_code + " and Active_Flag=0 " +
                             "union all " +
                             "select Fuel_Charge,MOT,Alw_Eligibilty,Sl_No,Effective_Date,rw from ( " +
                             "select iif(Fuel_Charge='',0,ISNULL(Fuel_Charge,0))Fuel_Charge,IIF(MOT='','None',ISNULL(MOT,'None'))MOT,Alw_Eligibilty,mt.Sl_No, " +
                             "Effective_Date,ROW_NUMBER() over(partition by MOT, mt.Sl_No order by Effective_Date desc) rw  " +
                             "from Mas_Modeof_Travel mt left join Mas_Statewise_FuelCharge mf on mt.Sl_No=mf.MOT_ID  " +
                             "where  mt.Division_Code=" + div_code + " and Active_Flag=0 and StateCode="+state+" and Design_code=" + SF_DesgID + " "+
                             "Group by Fuel_Charge,MOT,Alw_Eligibilty,mt.Sl_No,Effective_Date " +
                             ") as t  where rw=1";
        try
        {
            ds = db_ER.Exec_DataSet(strQry);

        }
        catch (Exception ex)
        {
            throw ex;
        }
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    [WebMethod]
    public static string Get_HotelBillExpense(string SF, string dt)
    {
        DataSet ds = null;
        string Divi_code = HttpContext.Current.Session["div_code"].ToString();
        DB_EReporting db_ElR = new DB_EReporting();
        string Qry = "select ('http://fmcg.sanfmcg.com/photos/'+Isnull(imgurl,''))Img_Url,Activity_Report_Code  from Activity_Event_Captures where Activity_Report_Code = '" + SF + "' and Identification = 'BillAmount' and Convert(date, Dateandtime)= '" + dt + "' and Division_Code='" + Divi_code + "'" +
                     " Group by imgurl,Activity_Report_Code";
        try
        {
            ds = db_ElR.Exec_DataSet(Qry);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    [WebMethod]
    public static string GetSFDetails(string SF)
    {
        SalesForce SFDet = new SalesForce();
        DataSet ds = SFDet.GetSFDetails(SF);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }

    [WebMethod]
    public static string GetExpense(string SF, string Mn, string Yr, string SFEMP)
    {
        Expense Exp = new Expense();
        DataSet ds = Exp.GetExpenseDetails(SF, Mn, Yr, SFEMP);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }

    [WebMethod]
    public static string GetExpenseKm(string SF, string Mn, string Yr)
    {
        DB_EReporting Exp = new DB_EReporting();
        DataTable ds = Exp.Exec_DataTable("exec getExpenseStartEndKm '" + SF + "','" + Mn + "','" + Yr + "'");
        return JsonConvert.SerializeObject(ds);
    }

    [WebMethod]
    public static Distance_Details[] GetDistanceDetails(string sf_code)
    {
        Expense nt = new Expense();
        List<Distance_Details> FFD = new List<Distance_Details>();

        DataSet dsAlowtype = null;
        dsAlowtype = nt.get_Distance_Details("", sf_code);

        foreach (DataRow row in dsAlowtype.Tables[0].Rows)
        {
            Distance_Details ffd = new Distance_Details();
            ffd.Territory_Ho = row["Territory_Ho"].ToString();
            ffd.Frm_Plc_Code = row["Frm_Plc_Code"].ToString();
            ffd.To_Plc_Code = row["To_Plc_Code"].ToString();
            ffd.Distance_KM = row["Distance_KM"].ToString();
            ffd.Place_Type = row["Place_Type"].ToString();
            ffd.Routes = row["ExpRoutes"].ToString();

            FFD.Add(ffd);
        }
        return FFD.ToArray();
    }

    [WebMethod]
    public static Fare_Details[] GetfAREDetails(string sf_code)
    {
        Expense nt = new Expense();
        List<Fare_Details> FFD = new List<Fare_Details>();

        DataSet dsAlowtype = null;
        var firstDayOfMonth = new DateTime(Convert.ToInt32(Yr), Convert.ToInt32(Mn), 1);
        DateTime ldt = firstDayOfMonth.AddMonths(1).AddDays(-1);
        dsAlowtype = get_Fare_Details("", sf_code, ldt.ToString("yyyy-MM-dd"));

        foreach (DataRow row in dsAlowtype.Tables[0].Rows)
        {
            Fare_Details ffd = new Fare_Details();
            ffd.Sf_Code = row["Sf_Code"].ToString();
            ffd.Fare = row["Fare"].ToString();
            ffd.Fareid = row["Fareid"].ToString();
            FFD.Add(ffd);
        }
        return FFD.ToArray();
    }


    public static DataSet get_Fare_Details(string div_code, string sf_code, string Dt)
    {
        DB_EReporting db_ER = new DB_EReporting();

        DataSet dsDivision = null;
        string strQry = "exec getSalesforceKMFare '" + sf_code + "','" + div_code + "','" + Dt + "'";
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

    [WebMethod]
    public static Allowances[] GetAllowanceDetails(string sf_code, string Mn, string Yr)
    {


        List<Allowances> FFD = new List<Allowances>();

        DataSet dsAlowtype = null;
        dsAlowtype = GetAllowanceDets(sf_code, Mn, Yr);

        foreach (DataRow row in dsAlowtype.Tables[0].Rows)
        {
            Allowances ffd = new Allowances();
            ffd.AlwCd = row["ID"].ToString();
            ffd.AlwName = row["Name"].ToString();
            ffd.AlwSName = row["SName"].ToString();
            ffd.AlwType = row["Type"].ToString();
            ffd.AlwUEntry = row["UsrAccess"].ToString();
            ffd.AlwAmt = row["Amt"].ToString();
            ffd.Effective_Date = row["Effective_Date"].ToString();
            FFD.Add(ffd);
        }
        return FFD.ToArray();
    }
    public static DataSet GetAllowanceDets(string sf_code, string Mn, string Yr)
    {
        DB_EReporting db_ER = new DB_EReporting();

        DataSet Alws = null;
        string strQry = "exec GetDesg_AllowanceDets_Pri '" + sf_code + "','" + Mn + "','" + Yr + "'," + Pri_ID + "";
        try
        {
            Alws = db_ER.Exec_DataSet(strQry);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return Alws;
    }

    [WebMethod]
    public static string GetUserExpense(string SF, string Mn, string Yr)
    {
        Expense Exp = new Expense();
        DataSet ds = Exp.GetUserExpense(SF, Mn, Yr);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }

    [WebMethod]
    public static string UserDailyAdditional(string SF, string Mn, string Yr)
    {        
        DataSet ds = GetUserDAilyAdditional(SF, Mn, Yr, EmpID);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
	public static DataSet GetUserDAilyAdditional(string SF, string Mn, string Yr, string EmpID)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            string strQry = "exec getExpDailAdd_Periodic '" + SF + "','" + Mn + "','" + Yr + "','" + EmpID + "',"+ Pri_ID +"";
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
    [WebMethod]
    public static string UserExpAdditional(string SF, string Mn, string Yr)
    {        
        DataSet ds = GetUserExpAdditional(SF, Mn, Yr, EmpID);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
	public static DataSet GetUserExpAdditional(string SF, string Mn, string Yr, string EmpID)
	{
		DB_EReporting db_ER = new DB_EReporting();

		DataSet dsDivision = null;
		string strQry = "exec getExpAdditional_Period '" + SF + "'," + Mn + "," + Yr + ",'" + EmpID + "',"+ Pri_ID +"";
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
    [WebMethod]
    public static string AutoExpense(string SF, string Mn, string Yr)
    {
        DCR Exp = new DCR();
        DataTable AutoExp = new DataTable();
        AutoExp = Exp.getDataTable("select cast(isnull(Exp_Web_auto,0)as int)AutoExpense,cast(isnull(Exp_StEnd_Km,0)as int)StartEndKM from Access_Master where division_code='" + div_code + "'");
        return JsonConvert.SerializeObject(AutoExp);
    }
    [WebMethod]
    public static string getTransMonthExp(string SF, string Mn, string Yr)
    {
        DataTable dtt = new DataTable();
        string strQry = string.Empty;
        SqlConnection con = new SqlConnection(Globals.ConnString);
        con.Open();
        SqlCommand cmd = new SqlCommand("exec getSFMonthlyAllowance '" + SF + "','" + Mn + "','" + Yr + "','" + EmpID + "'", con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dtt);
        con.Close();
        return JsonConvert.SerializeObject(dtt);
    }
    [WebMethod]
    public static string ManualSfExpense(string SF, string Mn, string Yr)
    {
        string Divi_code = HttpContext.Current.Session["div_code"].ToString();
        DB_EReporting db_ER = new DB_EReporting();
        DataSet dsDivision = null;
        string strQry = "exec getSfExpense_Approved_Demo '" + SF + "','" + Frm_date + "','" + To_Date + "'," + Mn + "," + Yr + ",'" + EmpID + "',"+Pri_ID+"";
        try
        {
            dsDivision = db_ER.Exec_DataSet(strQry);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return JsonConvert.SerializeObject(dsDivision.Tables[0]);
    }

    [WebMethod]
    public static string rejectExpense(string SF, string Mn, string Yr, string RejDt, string Rtype, string sEmpID, string RjectRm)
    {
        Expense Exp = new Expense();
        string msg = string.Empty;
        if (div_code == "107")
        {
            msg = Exp.RejectExpensePeriodical(SF, Mn, Yr, RejDt, Rtype, sEmpID);
        }
        else
        {
            msg = RejectExpense(SF, Mn, Yr, RejDt, Rtype, sEmpID, RjectRm);
        }
        return msg;
    }
    public static string RejectExpense(string SF, string Mn, string Yr, string rejdt, string Rtype, string EmpID, string Remarks)
    {
        DB_EReporting db_ER = new DB_EReporting();

        string dsDivision = string.Empty;
        string strQry = "exec RejectExpense_Native_Period '" + SF + "'," + Mn + "," + Yr + ",'" + rejdt + "'," + Rtype + ",'" + EmpID + "','" + Remarks + "',"+ Pri_ID +"";
        try
        {
            dsDivision = db_ER.Exec_Scalar_s(strQry);
            dsDivision = "Expense Rejected";
        }
        catch (Exception ex)
        {
            dsDivision = ex.Message;
            throw ex;
        }
        return dsDivision;
    }
    [WebMethod]
    public static string getTPDetails(string SF, string Mn, string Yr)
    {
        TourPlan Exp = new TourPlan();
        string SSf_Code = HttpContext.Current.Session["Sf_Code"].ToString();
        DataTable ds = Exp.getTpDetails(SF, Mn, Yr);
        return JsonConvert.SerializeObject(ds);
    }
    [WebMethod]
    public static string getApproval(string SF, string Mn, string Yr)
    {
        Expense Exp = new Expense();
        string Divi_code = HttpContext.Current.Session["div_code"].ToString();
        string SSf_Code = HttpContext.Current.Session["Sf_Code"].ToString();
        DataSet ds = GetApprovalFlag(SF, SSf_Code, Divi_code, Mn, Yr, EmpID);
        Approval_flag = ds.Tables[0].Rows[0]["AFL"].ToString();
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    public static DataSet GetApprovalFlag(string SF, string SSf, string Div_code, string Mn, string Yr, string EmpID)
    {
        DB_EReporting db_ER = new DB_EReporting();

        DataSet dsDivision = null;
        string strQry = "exec getApproval_Demo '" + SF + "','" + SSf + "','" + Div_code + "'," + Mn + "," + Yr + ",'" + EmpID + "'," + Pri_ID + "";
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

    public class Distance_Details
    {
        public string Territory_Ho { get; set; }
        public string Frm_Plc_Code { get; set; }
        public string To_Plc_Code { get; set; }
        public string Distance_KM { get; set; }
        public string Place_Type { get; set; }
        public string Routes { get; set; }
    }

    public class Fare_Details
    {
        public string Sf_Code { get; set; }
        public string Sf_Name { get; set; }
        public string Fare { get; set; }
        public string Fareid { get; set; }
    }

    public class Allowances
    {
        public string AlwCd { get; set; }
        public string AlwName { get; set; }
        public string AlwSName { get; set; }
        public string AlwType { get; set; }
        public string AlwUEntry { get; set; }
        public string AlwAmt { get; set; }
        public string Effective_Date { get; set; }
    }
    public class ExpDaily
    {
        public object E_CD { get; set; }
        public object E_EDT { get; set; }
        public object E_EAMT { get; set; }
    }
    public class ExpMonthly
    {
        public object E_MCD { get; set; }
        public object E_MTYP { get; set; }
        public object E_MAMT { get; set; }
    }
    public class ExpHead
    {
        public object SF { get; set; }
        public object Mn { get; set; }
        public object Yr { get; set; }
        public object md { get; set; }
        public object tamt { get; set; }
        public object ApprNm { get; set; }
    }
    public class AddExp
    {
        public object Desc { get; set; }
        public object Val { get; set; }
        public object Asub { get; set; }
    }
    public class DAddD
    {
        public object EAdt { get; set; }
        public object EAde { get; set; }
        public object EAdd { get; set; }
        public object Dsub { get; set; }
    }
    public class ExpDetails
    {
        public object E_Dt { get; set; }
        public object E_WT { get; set; }
        public object E_Twn { get; set; }
        public object E_Typ { get; set; }
        public object E_Dis { get; set; }
        public object E_Fare { get; set; }
        public object E_Alw { get; set; }
        public object E_TAmt { get; set; }
        public object E_Twnn { get; set; }
        public object E_MDP { get; set; }
    }
    [WebMethod]
    public static string saveExpense(string EHead, string ExDetails, string DDetails, string MDetails, string ExpAdd, string MAuto, string ApExp, string ExpDAdd, string AppSFC, string sEmpId)
    {
        Expense nt = new Expense();
        string msg = string.Empty;
        string ssfcode = AppSFC;// HttpContext.Current.Session["sf_code"].ToString();
        if (AppSFC == "" || AppSFC == null)
        {
            ssfcode = HttpContext.Current.Session["sf_code"].ToString();
        }
        var eHead = JsonConvert.DeserializeObject<List<ExpHead>>(EHead);
        DataTable eDetails = JsonConvert.DeserializeObject<DataTable>(ExDetails);
        var dAllow = JsonConvert.DeserializeObject<List<ExpDaily>>(DDetails);
        var mAllow = JsonConvert.DeserializeObject<List<ExpMonthly>>(MDetails);
        var eAdd = JsonConvert.DeserializeObject<List<AddExp>>(ExpAdd);
        var dAdde = JsonConvert.DeserializeObject<List<DAddD>>(ExpDAdd);
        string edxml = "<ROOT>";
        for (int i = 0; i < eDetails.Rows.Count; i++)
        {
            if (eDetails.Rows[i]["E_WT"].ToString() != "No Claim" && eDetails.Rows[i]["E_WT"].ToString() != "N/A")
                edxml += "<ASSD EDt=\"" + eDetails.Rows[i]["E_Dt"].ToString() + "\" EWt=\"" + eDetails.Rows[i]["E_WT"].ToString() + "\" Etwn=\"" + eDetails.Rows[i]["E_Twn"].ToString() + "\" ETyp=\"" + eDetails.Rows[i]["E_Typ"].ToString() + "\" EDis=\"" + eDetails.Rows[i]["E_Dis"].ToString() + "\" EFR=\"" + eDetails.Rows[i]["E_Fare"].ToString() + "\" EDA=\"" + eDetails.Rows[i]["E_Alw"].ToString() + "\" EAmt=\"" + eDetails.Rows[i]["E_TAmt"].ToString() + "\" Twn=\"" + eDetails.Rows[i]["E_Twnn"].ToString() + "\" MDP=\"" + eDetails.Rows[i]["E_MDP"].ToString() + "\" MOT=\"" + eDetails.Rows[i]["E_mot"].ToString() + "\" sttype=\"" + eDetails.Rows[i]["E_st_typ"].ToString() + "\" HtlBillamnt=\"" + eDetails.Rows[i]["E_H_Billamnt"].ToString() + "\" MOTID=\"" + eDetails.Rows[i]["E_mot_id"].ToString() + "\" StEdNeed=\"" + eDetails.Rows[i]["E_StEndNeed"].ToString() + "\" MxKm=\"" + eDetails.Rows[i]["E_MaxKm"].ToString() + "\" FuelCharge=\"" + eDetails.Rows[i]["E_Fuel_Charge"].ToString() + "\" ExpenseKm=\"" + eDetails.Rows[i]["E_Expense_Km"].ToString() + "\" ExpAmount=\"" + eDetails.Rows[i]["E_Exp_Amount"].ToString() + "\" />";
            //edxml += "<ASSD EDt=\"" + eDetails[i].E_Dt.ToString() + "\" EWt=\"" + eDetails[i].E_WT.ToString() + "\" Etwn=\"" + eDetails[i].E_Twn.ToString() + "\" ETyp=\"" + eDetails[i].E_Typ.ToString() + "\" EDis=\"" + eDetails[i].E_Dis.ToString() + "\" EFR=\"" + eDetails[i].E_Fare.ToString() + "\" EDA=\"" + eDetails[i].E_Alw.ToString() + "\" EAmt=\"" + eDetails[i].E_TAmt.ToString() + "\" Twn=\"" + eDetails[i].E_Twnn.ToString() + "\" MDP=\"" + eDetails[i].E_MDP.ToString() + "\" />";
        }
        edxml += "</ROOT>";
        string EAxml = "<ROOT>";
        for (int i = 0; i < eAdd.Count; i++)
        {
            EAxml += "<ASSD Desp=\"" + eAdd[i].Desc.ToString() + "\" AVal=\"" + eAdd[i].Val.ToString() + "\" Asub=\"" + eAdd[i].Asub.ToString() + "\"/>";
        }
        EAxml += "</ROOT>";
        string DAxml = "<ROOT>";
        for (int i = 0; i < dAllow.Count; i++)
        {
            DAxml += "<ASSD ECD=\"" + dAllow[i].E_CD.ToString() + "\" EEDt=\"" + dAllow[i].E_EDT.ToString() + "\" EEAmt=\"" + dAllow[i].E_EAMT.ToString() + "\"/>";
        }
        DAxml += "</ROOT>";
        string MAxml = "<ROOT>";
        for (int i = 0; i < mAllow.Count; i++)
        {
            MAxml += "<ASSD Acd=\"" + mAllow[i].E_MCD.ToString() + "\" Atp=\"" + mAllow[i].E_MTYP.ToString() + "\" Amt=\"" + mAllow[i].E_MAMT.ToString() + "\"/>";
        }
        MAxml += "</ROOT>";
        string dailyAd = "<ROOT>";
        for (int i = 0; i < dAdde.Count; i++)
        {
            dailyAd += "<ASSD Ddt=\"" + dAdde[i].EAdt.ToString() + "\" Ddesc=\"" + dAdde[i].EAde.ToString() + "\" DAdd=\"" + dAdde[i].EAdd.ToString() + "\" Dsub=\"" + dAdde[i].Dsub.ToString() + "\"/>";
        }
        dailyAd += "</ROOT>";
        string consString = Globals.ConnString;
        if (eDetails.Rows.Count > 0)
        {
            using (SqlConnection con = new SqlConnection(consString))
            {
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "svTransExpense_Demo";
                    //cmd.CommandText = "svTransExpenseHead";
                    SqlParameter[] parameters = new SqlParameter[]
                            {
                                        new SqlParameter("@SF", eHead[0].SF.ToString()),
                                        new SqlParameter("@Div", div_code),
                                        new SqlParameter("@Mn", eHead[0].Mn.ToString()),
                                        new SqlParameter("@Yr", eHead[0].Yr.ToString()),
                                        new SqlParameter("@ExpAmt", eHead[0].tamt.ToString()),
                                        new SqlParameter("@Edxml", edxml),
                                        new SqlParameter("@DAxml", DAxml),
                                        new SqlParameter("@MAxml", MAxml),
                                        new SqlParameter("@EAxml", EAxml),
                                        new SqlParameter("@Approve_Flag", Approval_flag),
                                        new SqlParameter("@SaveFlag", MAuto),
                                        new SqlParameter("@Appexp",ApExp),
                                        new SqlParameter("@Dailyxml",dailyAd),
                                        new SqlParameter("@ApSfc",ssfcode),
                                        new SqlParameter("@EmpID",sEmpId),                                        
                                        new SqlParameter("@FrmDt",Frm_date),
                                        new SqlParameter("@ToDt",To_Date),
										new SqlParameter("@PriodicID",Pri_ID),
                                        new SqlParameter("@PriodicNm",Pri_Nm),
					 new SqlParameter("@ApprNm",eHead[0].ApprNm.ToString())
                            };
                    cmd.Parameters.AddRange(parameters);
                    try
                    {
                        if (con.State != ConnectionState.Open)
                        {
                            con.Open();
                        }
                         cmd.ExecuteNonQuery();
                        if (MAuto == "0")
                        {
                            msg = "Expense Saved for Approval";
                        }
                        if (MAuto == "1")
                        {
                            msg = "Expense Submitted for Approval";
                        }
                    }
                    catch (Exception ex)
                    {
                        msg = ex.Message;
                        throw ex;
                    }
                }
            }
        }
        else
        {
            msg = "Error in Daily Expense Details";
        }
        return msg;
    }
}