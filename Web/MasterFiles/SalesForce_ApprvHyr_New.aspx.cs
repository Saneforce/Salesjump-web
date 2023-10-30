using System;
using System.Collections.Generic;
using System.Data;
using Bus_EReport;
using System.Data.SqlClient;
using System.Web.Services;
using Newtonsoft.Json;
using DBase_EReport;
using System.Web;

public partial class MasterFiles_SalesForce_ApprvHyr_New : System.Web.UI.Page
{

    #region "Declaration"
    public static string sf_code = string.Empty;
    public static string chksfcode = string.Empty;
    public static string sf_type = string.Empty;
    public static string Div = string.Empty;
    public static string divcode = string.Empty;
    public static string baseUrl = "";
    #endregion

    #region OnPreInit
    protected override void OnPreInit(EventArgs e)
    {
        baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority + Request.ApplicationPath.TrimEnd('/') + "/";
        if ((Convert.ToString(Session["sf_type"]) != null || Convert.ToString(Session["sf_type"]) != ""))
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
        else { Page.Response.Redirect(baseUrl, true); }
    }
    #endregion

    #region Page_Load
    protected void Page_Load(object sender, EventArgs e)
    {
        baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority + Request.ApplicationPath.TrimEnd('/') + "/";
        if ((Convert.ToString(Session["div_code"]) != null || Convert.ToString(Session["div_code"]) != ""))
        {
            sf_code = Request.QueryString["sfcode"];
            chksfcode = sf_code;
            Div = Session["div_code"].ToString();
            divcode = Session["div_code"].ToString();
        }
        else { Page.Response.Redirect(baseUrl, true); }
    }
    #endregion

    #region  getpnding
    [WebMethod]
    public static string getpnding(string sf)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        SqlConnection conn = new SqlConnection();
        if ((sf == "" || sf == null))
        { sf = "0"; }

        try
        {
            using (conn = new SqlConnection(Globals.ConnString))
            {
                using (var com = conn.CreateCommand())
                {
                    string Sqry = " SELECT * FROM Trans_Expense_Head_Periodic th ";
                    Sqry += " INNER JOIN Trans_Expense_Detail_Periodic td with(nolock) on th.Trans_Sl_No=td.trans_dt_slno ";
                    Sqry += " WHERE th.SF_Code = @Val1 AND Appr_By = @Val2";
                    com.Connection = conn;
                    com.CommandText = Sqry;
                    com.CommandType = CommandType.Text;
                    com.Parameters.AddWithValue("@val1", Convert.ToString(chksfcode).Trim());
                    com.Parameters.AddWithValue("@val2", Convert.ToString(sf).Trim());
                    com.Prepare();
                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = com;
                    conn.Open();
                    da.Fill(ds);
                    conn.Close();

                    if (ds.Tables.Count > 0)
                    {
                        dt = ds.Tables[0];
                    }

                }
            }
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
        finally
        {
            if (conn.State == ConnectionState.Open)
            { conn.Close(); }
        }
        return JsonConvert.SerializeObject(dt);

        //con.Open();
        //SqlCommand cmd = new SqlCommand("SELECT * FROM Trans_Expense_Head_Periodic th inner join Trans_Expense_Detail_Periodic td with(nolock) on th.Trans_Sl_No=td.trans_dt_slno where th.SF_Code='" + chksfcode + "' and Appr_By='" + sf + "'", con);
        //SqlDataAdapter da = new SqlDataAdapter(cmd);
        //da.Fill(ds);
        //con.Close();
        //return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    #endregion

    #region getSFWorkedWith

    [WebMethod]
    public static string getSFWorkedWith(string sfcode)
    {
        DataSet dstp = get_DayPlanWorkedWith(sfcode, Div);
        return JsonConvert.SerializeObject(dstp.Tables[0]);
    }
    #endregion

    #region  get_DayPlanWorkedWith
    public static DataSet get_DayPlanWorkedWith(string sf_Code, string Div)
    {
        DB_EReporting db_ER = new DB_EReporting();
        DataSet dsSF = new DataSet();

        if (sf_Code == null || sf_Code == "")
        { sf_Code = "0"; }

        if (Div == null || Div == "")
        { Div = "0"; }

        string strQry = "exec getJointWork '" + sf_Code + "','" + Div + "'";
        try
        {
            dsSF = db_ER.Exec_DataSet(strQry);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return dsSF;
    }
    #endregion

    #region getApprSFDets
    [WebMethod]
    public static string getApprSFDets(string SF, string div, string apprTyp)
    {
        DataSet ds = new DataSet(); DataTable dt = new DataTable();
        DB_EReporting db_ER = new DB_EReporting();

        if (SF == null || SF == "")
        { SF = "0"; }

        if (div == null || div == "")
        { div = "0"; }

        if (apprTyp == null || apprTyp == "")
        { apprTyp = "0"; }

        string strQry = " EXEC getApprSFDetails '" + SF + "'," + div + "," + apprTyp + "";

        try
        {
            ds = db_ER.Exec_DataSet(strQry);

            if (ds.Tables.Count > 0)
            { dt = ds.Tables[0]; }
        }
        catch (Exception ex)
        {
            throw ex;
        }

        return JsonConvert.SerializeObject(dt);

        //return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    #endregion

    #region  getDepts
    [WebMethod]
    public static string getDepts(string divcode, string subdiv)
    {
        SalesForce SFD = new SalesForce();

        if (divcode == null || divcode == "")
        { divcode = "0"; }

        if (subdiv == null || subdiv == "")
        { subdiv = "0"; }


        DataSet dds = SFD.getAllSF_Dept(divcode, subdiv.TrimEnd(','));

        DataTable dt = new DataTable();

        if (dds.Tables.Count > 0)
        { dt = dds.Tables[0]; }

        //return JsonConvert.SerializeObject(dds.Tables[0]);

        return JsonConvert.SerializeObject(dt);
    }
    #endregion

    #region getDesignation
    [WebMethod]
    public static string getDesignation(string divcode)
    {
        //SalesForce SFD = new SalesForce();

        if (divcode == null || divcode == "")
        { divcode = "0"; }

        //DataSet dds = SFD.getAllSF_Designation(divcode);
        DataSet dds = getAllSF_Designation(divcode);

        DataTable dt = new DataTable();

        if (dds.Tables.Count > 0)
        { dt = dds.Tables[0]; }


        return JsonConvert.SerializeObject(dt);
    }
    #endregion

    #region getAllSF_Designation
    public static DataSet getAllSF_Designation(string divcode)
    {
        //DB_EReporting db = new DB_EReporting();
        DataSet ds = new DataSet();
        //strQry = " EXEC GET_AllSFDesignation '" + divcode + "' ";
        string strQry = "";
        strQry = " SELECT Designation_Code dcode,Designation_Short_Name dshname,Designation_Name dname,";
        strQry += " Type dtype FROM Mas_SF_Designation where Division_Code=@divcode and Designation_Active_Flag=0";
        try
        {
            if ((divcode == "" || divcode == null))
            { divcode = "0"; }


            using (var con = new SqlConnection(Globals.ConnString))
            {
                using (var cmd = con.CreateCommand())
                {
                    cmd.CommandText = strQry;
                    cmd.Parameters.AddWithValue("@divcode", Convert.ToString(divcode));
                    cmd.CommandType = CommandType.Text;

                    SqlDataAdapter dap = new SqlDataAdapter();
                    dap.SelectCommand = cmd;
                    con.Open();
                    dap.Fill(ds);
                    con.Close();
                }
            }

            //ds = db.ExecDataSet(strQry, divcode);            
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return ds;
    }
    #endregion

    #region getDivision

    [WebMethod]
    public static string getDivision(string divcode)
    {
        //SalesForce SFD = new SalesForce();

        if (divcode == null || divcode == "")
        { divcode = "0"; }

        //DataSet dds = SFD.Getsubdivisionwise(divcode);

        DataSet dds = Getsubdivisionwise(divcode);

        DataTable dt = new DataTable();

        if (dds.Tables.Count > 0)
        { dt = dds.Tables[0]; }

        //return JsonConvert.SerializeObject(dds.Tables[0]);

        return JsonConvert.SerializeObject(dt);
    }
    #endregion

    #region Getsubdivisionwise

    public static DataSet Getsubdivisionwise(string divcode, string subdiv = "0")
    {
        //DB_EReporting db_ER = new DB_EReporting();

        DataSet dsSF = new DataSet();

        //strQry = "select subdivision_code,subdivision_name from mas_subdivision where Div_Code='" + divcode + "' and SubDivision_Active_Flag=0 and charindex(','+cast(subdivision_code as varchar)+',',','+iif('" + subdiv + "'='0',cast(subdivision_code as varchar),'" + subdiv + "')+',')>0";

        try
        {
            using (var con = new SqlConnection(Globals.ConnString))
            {
                using (var cmd = con.CreateCommand())
                {
                    cmd.CommandText = "SELECT subdivision_code,subdivision_name FROM Mas_Subdivision WHERE Div_Code=@divcode  AND SubDivision_Active_Flag=0 AND charindex(','+cast(subdivision_code as varchar)+',',','+iif(@subdiv='0',cast(subdivision_code as varchar),@subdiv)+',')>0 ";
                    cmd.Parameters.AddWithValue("@divcode", Convert.ToString(divcode));
                    cmd.Parameters.AddWithValue("@subdiv", Convert.ToString(subdiv));
                    cmd.CommandType = CommandType.Text;

                    SqlDataAdapter dap = new SqlDataAdapter();
                    dap.SelectCommand = cmd;
                    con.Open();
                    dap.Fill(dsSF);
                    con.Close();
                }
            }

            //dsSF = db_ER.Exec_DataSet(strQry);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return dsSF;
    }
    #endregion

    #region getStates
    [WebMethod]
    public static string getStates(string divcode)
    {
        //SalesForce SFD = new SalesForce();

        if (divcode == null || divcode == "")
        { divcode = "0"; }

        //DataSet dds = SFD.getAllSF_States(divcode);

        DataSet dds = getAllSF_States(divcode);

        DataTable dt = new DataTable();

        if (dds.Tables.Count > 0)
        { dt = dds.Tables[0]; }

        //return JsonConvert.SerializeObject(dds.Tables[0]);

        return JsonConvert.SerializeObject(dt);
    }
    #endregion

    #region getAllSF_States
    public static DataSet getAllSF_States(string divcode)
    {
        //DB_EReporting db = new DB_EReporting();
        DataSet ds = new DataSet();
        string strQry = "";

        //strQry = " EXEC GET_AllSFStates '" + divcode + "'";

        strQry = " SELECT  ms.State_Code scode,shortName sshname,ms.StateName sname FROM Mas_State ms WITH(NOLOCK) ";
        strQry += " INNER  JOIN Mas_Division md WITH(NOLOCK) ON CHARINDEX(',' + cast(ms.State_Code as varchar) + ',',',' + md.State_Code+',')>0 ";
        strQry += " WHERE md.Division_Code=@divcode and ms.State_Active_Flag=0 order by 2";
        try
        {
            using (var con = new SqlConnection(Globals.ConnString))
            {
                using (var cmd = con.CreateCommand())
                {
                    cmd.CommandText = strQry;
                    cmd.Parameters.AddWithValue("@divcode", Convert.ToString(divcode));
                    cmd.CommandType = CommandType.Text;

                    SqlDataAdapter dap = new SqlDataAdapter();
                    dap.SelectCommand = cmd;
                    con.Open();
                    dap.Fill(ds);
                    con.Close();
                }
            }

            //ds = db.Exec_DataSet(strQry);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return ds;
    }
    #endregion

    #region getDivisions
    [WebMethod]
    public static string getDivisions(string divcode)
    {
        SalesForce SFD = new SalesForce();
        DataSet dds = SFD.Getsubdivisionwise(divcode);
        return JsonConvert.SerializeObject(dds.Tables[0]);
    }
    #endregion

    #region getDivisionsname
    [WebMethod]
    public static string getDivisionsname(string divcode)
    {
        //SalesForce SFD = new SalesForce();
        //DataSet dds = SFD.getDivisname(divcode);

        DataSet dds = getDivisname(divcode);
        return JsonConvert.SerializeObject(dds.Tables[0]);
    }
    #endregion 


    public static DataSet getDivisname(string divcode)
    {
        //DB_EReporting db = new DB_EReporting();
        DataSet ds = new DataSet();

        string strQry = " SELECT upper(Division_SName)Division_SName,(select cast(COUNT(*)as varchar) FROM Mas_Salesforce ";
        strQry += " WHERE charindex(','+CAST(@divcode as varchar)+',',','+Division_Code+',')>0)uname,LEN(Division_SName)ulen ";
        strQry += " FROM Mas_Division where division_code=@divcode";

        try
        {
            using (var con = new SqlConnection(Globals.ConnString))
            {
                using (var cmd = con.CreateCommand())
                {
                    cmd.CommandText = strQry;
                    cmd.Parameters.AddWithValue("@divcode", Convert.ToString(divcode));
                    cmd.CommandType = CommandType.Text;

                    SqlDataAdapter dap = new SqlDataAdapter();
                    dap.SelectCommand = cmd;
                    con.Open();

                    dap.Fill(ds);
                    con.Close();
                }
            }

            //ds = db.Exec_DataSet(strQry);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return ds;
    }

    [WebMethod]
    public static string getDistrict(string divcode)
    {
        //SalesForce SFD = new SalesForce();
        //DataSet dds = SFD.getAllSF_District(divcode);
        DataSet dds = getAllSF_District(divcode);
        return JsonConvert.SerializeObject(dds.Tables[0]);
    }

    public static DataSet getAllSF_District(string divcode)
    {
        //DB_EReporting db = new DB_EReporting();
        DataSet ds = new DataSet();
        string strQry = String.Empty;

        //strQry = "  EXEC GET_AllSFDistrict '" + divcode + "' ";

        strQry = "select Dist_code,Dist_name from Mas_District where Div_Code=@divcode and Dist_Active_Flag=0 order by Dist_name";
        try
        {
            using (var con = new SqlConnection(Globals.ConnString))
            {
                using (var cmd = con.CreateCommand())
                {
                    cmd.CommandText = strQry;
                    cmd.Parameters.AddWithValue("@divcode", Convert.ToString(divcode.TrimEnd(',')));
                    cmd.CommandType = CommandType.Text;

                    SqlDataAdapter dap = new SqlDataAdapter();
                    dap.SelectCommand = cmd;
                    con.Open();
                    dap.Fill(ds);
                    con.Close();
                }
            }
            //ds = db.Exec_DataSet(strQry);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return ds;
    }

    [WebMethod]
    public static string getTerritory(string divcode)
    {
        //SalesForce SFD = new SalesForce();
        //DataSet dds = SFD.getAllSF_Territory(divcode);

        DataSet dds = getAllSF_Territory(divcode);
        return JsonConvert.SerializeObject(dds.Tables[0]);
    }

    public static DataSet getAllSF_Territory(string divcode)
    {
        //DB_EReporting db = new DB_EReporting();
        DataSet ds = new DataSet();
        string strQry = String.Empty;
        //strQry = " EXEC GET_AllSFTerritory '" + divcode + "' ";
        strQry = " SELECT Territory_Code tcode,Territory_Name tname FROM Mas_Territory ";
        strQry += " WHERE Div_Code=@divcode and Territory_Active_Flag=0 order by 2";
        try
        {
            using (var con = new SqlConnection(Globals.ConnString))
            {
                using (var cmd = con.CreateCommand())
                {
                    cmd.CommandText = strQry;
                    cmd.Parameters.AddWithValue("@divcode", Convert.ToString(divcode.TrimEnd(',')));
                    cmd.CommandType = CommandType.Text;

                    SqlDataAdapter dap = new SqlDataAdapter();
                    dap.SelectCommand = cmd;
                    con.Open();
                    dap.Fill(ds);
                    con.Close();
                }
            }
            //ds = db.Exec_DataSet(strQry);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return ds;
    }

    [WebMethod]
    public static string GetHQDetails(string divcode)
    {
        SalesForce SFD = new SalesForce();
        DataSet dds = SFD.getAllSFHQ(divcode);
        return JsonConvert.SerializeObject(dds.Tables[0]);
    }

    [WebMethod]
    public static string GetReportingDetails(string divcode)
    {
        //SalesForce SFD = new SalesForce();
        //DataSet dds = SFD.getSFType(divcode);
        DataSet dds = getSFType(divcode);
        return JsonConvert.SerializeObject(dds.Tables[0]);
    }

    public static DataSet getSFType(string div_code)
    {
        //DB_EReporting db_ER = new DB_EReporting();

        DataSet dsSF = new DataSet();
        string strQry = String.Empty;
        //strQry = " EXEC GET_AllSFType '" + div_code + "' ";

        strQry = "SELECT '0' as Sf_Code, '---Select---' as sf_name  ";
        strQry += " UNION ";
        strQry += " SELECT 'admin' as Sf_Code, 'admin' as sf_name ";
        strQry += " UNION ";
        strQry += " SELECT Sf_Code,sf_name + ' - ' + sf_Designation_Short_Name + ' - ' + Sf_HQ  FROM  Mas_Salesforce  ";
        strQry += " WHERE  Division_Code like '%' + @divcode+ '%'  AND sf_type = 2 AND sf_TP_Active_Flag !=1 AND SF_Status !=2";

        try
        {
            using (var con = new SqlConnection(Globals.ConnString))
            {
                using (var cmd = con.CreateCommand())
                {
                    cmd.CommandText = strQry;
                    cmd.Parameters.AddWithValue("@divcode", Convert.ToString(divcode));
                    cmd.CommandType = CommandType.Text;

                    SqlDataAdapter dap = new SqlDataAdapter();
                    dap.SelectCommand = cmd;
                    con.Open();
                    dap.Fill(dsSF);
                    con.Close();
                }
            }
            //dsSF = db_ER.Exec_DataSet(strQry);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return dsSF;
    }

    [WebMethod]
    public static string getNewSfCode(string typ)
    {
        SalesForce SFD = new SalesForce();
        string dds = SFD.getsfcode_new(typ, Div);
        return dds;
    }

    [WebMethod]
    public static string getEmployeeFieldSettings(string divcode)
    {
        SalesForce SFD = new SalesForce();
        DataTable dds = getEmployeeAdditionalFields(divcode);
        return JsonConvert.SerializeObject(dds);
    }

    public static DataTable getEmployeeAdditionalFields(string divcode)
    {
        DataTable ds = new DataTable();
        //DB_EReporting dber = new DB_EReporting();
        //string strQry = "select * from Mas_Employee_Fields where Division_Code=" + divcode + "";
        try
        {
            using (var con = new SqlConnection(Globals.ConnString))
            {
                using (var cmd = con.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM Mas_Employee_Fields WHERE Division_Code=@divcode ";
                    cmd.Parameters.AddWithValue("@divcode", Convert.ToString(divcode));
                    cmd.CommandType = CommandType.Text;

                    SqlDataAdapter dap = new SqlDataAdapter();
                    dap.SelectCommand = cmd;
                    con.Open();
                    dap.Fill(ds);
                    con.Close();
                }
            }
            //ds = dber.Exec_DataTable(strQry);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return ds;
    }

    [WebMethod]
    public static string saveFieldForce(string data, string Itype)
    {
        string msg = string.Empty;
        Bus_EReport.SalesForce.SaveSalesForce Data = JsonConvert.DeserializeObject<Bus_EReport.SalesForce.SaveSalesForce>(data);
        SalesForce dss = new SalesForce();
        msg = dss.saveNewSalesForce(Data, Itype);
        return msg;
    }

    [WebMethod]
    public static string saveFieldForceWithCustom(string data, string Itype)
    {
        string msg = string.Empty;

        locsf.SaveSalesForceCustom Data = JsonConvert.DeserializeObject<locsf.SaveSalesForceCustom>(data);

        //SalesForce dss = new SalesForce();

        locsf dss = new locsf();

        msg = dss.SalesForceNewSaveMainCustom(Data, Itype);
        return msg;
    }

    public class SalesforceData
    {
        public string Sfcode { get; set; }
        public string SfName { get; set; }
        public string SfEmail { get; set; }
        public string divcode { get; set; }
        public string sfusrname { get; set; }
        public string sfpwd { get; set; }
        public string sfempid { get; set; }
        public string sfdob { get; set; }
        public string sfdesg { get; set; }
        public string sfdept { get; set; }
        public string sfhq { get; set; }
        public string sfstate { get; set; }
        public string sfterr { get; set; }
        public string sfmobile { get; set; }
        public string sfaddr { get; set; }
        public string sfarea { get; set; }
        public string sfcity { get; set; }
        public string sfpincode { get; set; }
        public string sfstatus { get; set; }
        public string sftype { get; set; }
        public string sfjdate { get; set; }
        public string resndate { get; set; }
        public string sfreport { get; set; }
        public string sfpimg { get; set; }
        public string pname { get; set; }
        public string sflvl { get; set; }
        public string pval { get; set; }
        public string cname { get; set; }
        public string cval { get; set; }
        public string sfselsdiv { get; set; }
        public string sfdepot { get; set; }
        public string sfdistrict { get; set; }
    }


    [WebMethod]
    public static SalesforceData[] getSfDets(string sfcode)
    {
        string msg = string.Empty;
        SalesForce dss = new SalesForce();
        DataSet dsm = dss.getNewSFDetails(sfcode);
        List<SalesforceData> ad = new List<SalesforceData>();
        foreach (DataRow row in dsm.Tables[0].Rows)
        {
            SalesforceData asd = new SalesforceData();
            asd.Sfcode = row["Sf_Code"].ToString();
            asd.SfName = row["Sf_Name"].ToString();
            asd.sfusrname = row["Sf_UserName"].ToString();
            asd.sfpwd = row["Sf_Password"].ToString();
            asd.sfdob = row["SfDob"].ToString();
            asd.sfdesg = row["Designation_Code"].ToString();
            asd.sfdept = row["sf_desgn"].ToString();
            asd.sfstate = row["State_Code"].ToString();
            asd.sfterr = row["Territory_Code"].ToString();
            asd.sfaddr = row["SfAddr"].ToString();
            asd.sfarea = row["SfArea"].ToString();
            asd.sfcity = row["SfCity"].ToString();
            asd.SfEmail = row["SF_Email"].ToString();
            asd.sflvl = row["sf_type"].ToString();
            asd.sfmobile = row["SfMobile"].ToString();
            asd.sfpimg = row["SfProPic"].ToString();
            asd.sfreport = row["Reporting_To_SF"].ToString();
            asd.sftype = row["Approved_By"].ToString();
            asd.sfstatus = row["SF_Status"].ToString();
            asd.sfempid = row["sf_emp_id"].ToString();
            asd.sfhq = row["Sf_HQ"].ToString();
            asd.sfjdate = row["SfJdate"].ToString();
            asd.sfdistrict = row["sfdistrict"].ToString();
            asd.resndate = row["resndate"].ToString();
            asd.sfdepot = row["sfdepot"].ToString();
            asd.sfselsdiv = row["subdivision_code"].ToString().TrimEnd(',');
            ad.Add(asd);
        }
        foreach (DataRow row in dsm.Tables[1].Rows)
        {
            SalesforceData asd = new SalesforceData();
            if (row["Field_Type"].ToString() == "profile")
            {
                asd.pname = row["Field_Name"].ToString();
                asd.pval = row["Field_Value"].ToString();
                ad.Add(asd);
            }
            if (row["Field_Type"].ToString() == "contact")
            {
                asd.cname = row["Field_Name"].ToString();
                asd.cval = row["Field_Value"].ToString();
                ad.Add(asd);
            }
        }
        return ad.ToArray(); ;
    }

    [WebMethod]
    public static string svApprHierarchy(string sf, string Div, string Appr_Type, string Appr_Name, string cusxml)
    {
        string msg = string.Empty;
        SqlConnection conn = new SqlConnection(Globals.ConnString);
        try
        {
            conn.Open();
            SqlTransaction objTrans = conn.BeginTransaction();
            try
            {
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure; ;
                    cmd.CommandText = "svApprovalHierarchy";
                    SqlParameter[] parameters = new SqlParameter[]
                    {
                                new SqlParameter("@Sf_Code",sf),
                                new SqlParameter("@Appr_Type", Appr_Type),
                                new SqlParameter("@Appr_Name",Appr_Name),
                                new SqlParameter("@Div",Div),
                                new SqlParameter("@cusxml",cusxml)
                    };
                    cmd.Parameters.AddRange(parameters);
                    try
                    {
                        if (conn.State != ConnectionState.Open)
                        {
                            conn.Open();
                        }
                        cmd.Transaction = objTrans;
                        int i = cmd.ExecuteNonQuery();
                        msg = i > 0 ? "Success" : "Error Occured";
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
                objTrans.Commit();
            }
            catch (Exception ex)
            {
                objTrans.Rollback();
                throw ex;
            }
        }
        catch (Exception ex)
        {
            msg = ex.Message.ToString();
        }
        return msg;
    }

    [WebMethod]
    public static string GetCustomFormsFieldsGroups(string divcode, string ModuleId)
    {
        DataSet ds = new DataSet();

        //AdminSetup Ad = new AdminSetup();

        locsf Ad = new locsf();

        ds = Ad.GetCustomFormsFieldsGroupData(divcode, ModuleId);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }


    [WebMethod]
    public static string GetCustomFormsFieldsList(string divcode, string ModuleId)
    {
        DataSet ds = new DataSet();

        //AdminSetup Ad = new AdminSetup();

        locsf Ad = new locsf();

        ds = Ad.GetCustomFormsFieldsData(divcode, ModuleId);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }

    [WebMethod]
    public static string GetCustomFormsSeclectionMastesList(string TableName)
    {
        DataSet ds = new DataSet();
        
        string DivCode = Convert.ToString(HttpContext.Current.Session["div_Code"]);

        if ((DivCode == null || DivCode == ""))
        { DivCode = "0"; }

        ds = GetCustomMatersData(TableName, DivCode);
        DataTable dt = new DataTable();
        dt = ds.Tables[0];

        return JsonConvert.SerializeObject(dt);
    }

    public static DataSet GetCustomMatersData(string TableName, string Divcode)
    {
        DB_EReporting db_ER = new DB_EReporting();

        DataSet dsAdmin = null;

        //strQry = "EXEC [Get_CustomForms_SeclectionMastesList] '" + TableName + "' ,'" + ColumnsName + "'";

        string strQry = "EXEC [Get_CustomForms_MastesTablesData] '" + TableName + "' ," + Divcode + "";

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


    [WebMethod]
    public static string GetBindAddtionalFieldData(string SfCode)
    {
        //SalesForce sd = new SalesForce();

        locsf sd = new locsf();

        DataSet dsTerritory = sd.GetSFAddtionalFieldData(SfCode);

        return JsonConvert.SerializeObject(dsTerritory.Tables[0]);
    }

    public class locsf
    {
        string strQry = "";

        public class ProfileDetails
        {
            [JsonProperty("pfield")]
            public string pfield { get; set; }

            [JsonProperty("pval")]
            public string pvalue { get; set; }
        }

        public class AddtionalfieldDetails
        {
            [JsonProperty("Fields")]
            public string Fields { get; set; }

            [JsonProperty("Values")]
            public string Values { get; set; }
        }

        public class ContactDetails
        {
            [JsonProperty("cfield")]
            public string cfield { get; set; }

            [JsonProperty("cval")]
            public string cvalue { get; set; }
        }

        public class SaveSalesForceCustom
        {
            [JsonProperty("DivCode")]
            public string divcode { get; set; }

            [JsonProperty("SfDiv")]
            public object sfsdiv { get; set; }

            [JsonProperty("SFHQN")]
            public object sfhqname { get; set; }

            [JsonProperty("SfCode")]
            public object sfcode { get; set; }

            [JsonProperty("SF_Name")]
            public object sfname { get; set; }

            [JsonProperty("SfEmail")]
            public object sfemail { get; set; }

            [JsonProperty("SfUsername")]
            public string sfusrname { get; set; }

            [JsonProperty("SfPwd")]
            public object sfpwd { get; set; }

            [JsonProperty("SfEmpId")]
            public object sfempid { get; set; }

            [JsonProperty("SfDOB")]
            public object sfdob { get; set; }

            [JsonProperty("SfDesig")]
            public object sfdesg { get; set; }

            [JsonProperty("SfDept")]
            public object sfdept { get; set; }

            [JsonProperty("SfHQ")]
            public object sfhq { get; set; }

            [JsonProperty("SfState")]
            public object sfstate { get; set; }

            [JsonProperty("SfTerr")]
            public object sfterr { get; set; }

            [JsonProperty("SfMobile")]
            public object sfmobile { get; set; }

            [JsonProperty("SfAddr")]
            public object sfaddr { get; set; }

            [JsonProperty("SfArea")]
            public object sfarea { get; set; }

            [JsonProperty("SfCity")]
            public object sfcity { get; set; }

            [JsonProperty("SfPincode")]
            public object sfpincode { get; set; }

            [JsonProperty("SfStatus")]
            public object sfstatus { get; set; }

            [JsonProperty("SfTyp")]
            public object sftype { get; set; }

            [JsonProperty("SfJDate")]
            public object sfjdate { get; set; }

            [JsonProperty("Sfrdate")]
            public object Sfrdate { get; set; }

            [JsonProperty("SfReport")]
            public object sfreport { get; set; }

            [JsonProperty("SfPImg")]
            public object sfpimg { get; set; }

            [JsonProperty("PArr")]
            public List<ProfileDetails> parr { get; set; }

            [JsonProperty("CArr")]
            public List<ContactDetails> carr { get; set; }

            [JsonProperty("SFlevel")]
            public object sflevel { get; set; }

            [JsonProperty("SFDepot")]
            public object sfdepot { get; set; }

            [JsonProperty("district")]
            public object sfdistrict { get; set; }

            [JsonProperty("ADArr")]
            public List<AddtionalfieldDetails> adarr { get; set; }
        }

        public DataSet GetSFAddtionalFieldData(string sfcode)
        {
            DataSet dsAdmin = new DataSet();
            DB_EReporting dbER = new DB_EReporting();

            if ((sfcode == null || sfcode == ""))
            { sfcode = "0"; }

            strQry = "exec GetSFAddtionalFieldData '" + sfcode + "'";
            try
            {
                dsAdmin = dbER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsAdmin;
        }

        public DataSet GetCustomFormsMastesdata(string TableName, string ColumnsName, string DivisionCode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "EXEC [Get_CustomForms_Mastesdata] '" + TableName + "' ,'" + ColumnsName + "','" + DivisionCode + "' ";

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

        public DataSet GetCustomFormsFieldsData(string divcode, string ModeleId)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "EXEC [Get_CustomForms_Fields] '" + divcode + "' ,'" + ModeleId + "' ";

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

        public DataSet GetCustomFormsFieldsGroupData(string divcode, string ModeleId)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "EXEC [Get_CustomForms_FieldsGroups] '" + divcode + "' ,'" + ModeleId + "' ";

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

        public string SalesForceNewSaveMainCustom(SaveSalesForceCustom sd, string Itype)
        {

            string dup = string.Empty;
            string strSfCode = string.Empty;
            string divcode = string.Empty;
            string msg = string.Empty;
            List<ProfileDetails> a = sd.parr;
            List<ContactDetails> b = sd.carr;
            List<AddtionalfieldDetails> af = sd.adarr;

            string sxml = "<ROOT>";
            for (int i = 0; i < a.Count; i++)
            {
                if (a[i].pvalue != "" && a[i].pvalue != null)
                {
                    sxml += "<ASSD stype=\"profile\" fld=\"" + a[i].pfield + "\" val=\"" + a[i].pvalue + "\" />";
                }
            }
            for (int i = 0; i < b.Count; i++)
            {
                if (b[i].cvalue != "" && b[i].cvalue != null)
                {
                    sxml += "<ASSD stype=\"contact\" fld=\"" + b[i].cfield + "\" val=\"" + b[i].cvalue + "\" />";
                }
            }
            sxml += "</ROOT>";
            if (Itype == "0")
            {
                if (!CheckDupUserName(sd.sfusrname))
                {
                    try
                    {
                        DB_EReporting db = new DB_EReporting();
                        string Emp_ID = string.Empty;
                        int sf_sl_no = -1;
                        strQry = "SELECT ISNULL(MAX(sf_Sl_No),0)+1 FROM mas_salesforce WHERE Sf_Code !='admin'";
                        sf_sl_no = db.Exec_Scalar(strQry);

                        string sub_division = string.Empty; ;
                        sub_division = "";
                        Emp_ID = "E" + sf_sl_no.ToString();
                        sd.sfcode = getsfcode_new(sd.sflevel.ToString(), sd.divcode);
                        SqlParameter[] parameters = new SqlParameter[]
                        {
                            new SqlParameter("@Sfcode", sd.sfcode),
                            new SqlParameter("@Sfname", sd.sfname),
                            new SqlParameter("@SfUserName", sd.sfusrname),
                            new SqlParameter("@Sf_Password", sd.sfpwd),
                            new SqlParameter("@SfJDate", sd.sfjdate),
                            new SqlParameter("@ReportTo", sd.sfreport),
                            new SqlParameter("@State", sd.sfstate),
                            new SqlParameter("@SfAddr", sd.sfaddr),
                            new SqlParameter("@SfArea", sd.sfarea),
                            new SqlParameter("@SfCity", sd.sfcity),
                            new SqlParameter("@SfPincode", sd.sfpincode),
                            new SqlParameter("@SfEmail", sd.sfemail),
                            new SqlParameter("@SfMobile", sd.sfmobile),
                            new SqlParameter("@Sfdob", sd.sfdob),
                            new SqlParameter("@SfStatus", sd.sfstatus),
                            new SqlParameter("@sfhq", sd.sfhq),
                            new SqlParameter("@Div", sd.divcode),
                            new SqlParameter("@sfempid", sd.sfempid),
                            new SqlParameter("@sfdept", sd.sfdept),
                            new SqlParameter("@subdiv", sub_division),
                            new SqlParameter("@SfTyp", sd.sftype),
                            new SqlParameter("@sfdesig", sd.sfdesg),
                            new SqlParameter("@empid", Emp_ID),
                            new SqlParameter("@sfterr", sd.sfterr),
                            new SqlParameter("@FFtype", sd.sflevel),
                            new SqlParameter("@SfSlNo", sf_sl_no),
                            new SqlParameter("@SfPimg", sd.sfpimg),
                            new SqlParameter("@SFHQN", sd.sfhqname),
                            new SqlParameter("@SFSdiv", sd.sfsdiv),
                            new SqlParameter("@SFDepot", sd.sfdepot),
                            new SqlParameter("@SFDistrict",sd.sfdistrict),
                            new SqlParameter("@sxml", sxml)
                        };

                        if (sd.sfcode.ToString() != "")
                        {
                            try
                            {
                                db.Exec_NonQueryWithParamNew("insertNewSalesForce", CommandType.StoredProcedure, parameters);
                                msg = "Success";
                            }
                            catch (Exception ex)
                            {
                                throw ex;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        msg = ex.Message;
                    }
                    if (msg == "Success")
                    {
                        strSfCode = sd.sfcode.ToString(); //Pass Sf Code for Salesforce_ApprovalManager table data creation

                        if (af.Count > 0)
                        {
                            string fld = ""; string val = "";
                            int i = 0;
                            for (int k = 0; k < af.Count; k++)
                            {
                                if ((af[k].Fields == null || af[k].Fields == "'undefined'" || af[k].Fields == "undefined") && (af[k].Values == "'undefined'" || af[k].Values == "undefined"))
                                { }
                                else
                                {
                                    fld += af[k].Fields + ",";

                                    if ((af[k].Values == null || af[k].Values == "")) { val += "''" + ","; }
                                    else { val += af[k].Values + ","; }
                                }
                            }
                            DB_EReporting db_ER = new DB_EReporting();

                            //string Iquery = " EXEC Insert_TransSFCustomField  '" + strSfCode + "', " + fld.TrimEnd(',') + "," + val.TrimEnd(',') + "";

                            string Iquery = "Insert Into Trans_SF_Custom_Field(SFID, " + fld.TrimEnd(',') + ") Values('" + strSfCode + "'," + val.TrimEnd(',') + ")";

                            i = db_ER.ExecQry(Iquery);

                            if (i > 0)
                            { msg = "SalesForce Created"; }
                            else { msg = "SalesForce Created"; }

                        }

                        msg = "SalesForce Created";
                    }
                }
                else
                {
                    msg = "Dup";
                }
            }
            if (Itype == "1")
            {
                if (!CheckDupUserName(sd.sfusrname, sd.sfcode.ToString()))
                {
                    try
                    {
                        DB_EReporting db = new DB_EReporting();
                        string sub_division = string.Empty;
                        sub_division = "";
                        SqlParameter[] parameters = new SqlParameter[]
                        {
                            new SqlParameter("@Sfcode", sd.sfcode),
                            new SqlParameter("@Sfname", sd.sfname),
                            new SqlParameter("@SfUserName", sd.sfusrname),
                            new SqlParameter("@Sf_Password", sd.sfpwd),
                            new SqlParameter("@SfJDate", sd.sfjdate),
                            new SqlParameter("@ReportTo", sd.sfreport),
                            new SqlParameter("@State", sd.sfstate),
                            new SqlParameter("@SfAddr", sd.sfaddr),
                            new SqlParameter("@SfArea", sd.sfarea),
                            new SqlParameter("@SfCity", sd.sfcity),
                            new SqlParameter("@SfPincode", sd.sfpincode),
                            new SqlParameter("@SfEmail", sd.sfemail),
                            new SqlParameter("@SfMobile", sd.sfmobile),
                            new SqlParameter("@Sfdob", sd.sfdob),
                            new SqlParameter("@SfStatus", sd.sfstatus),
                            new SqlParameter("@sfhq", sd.sfhq),
                            new SqlParameter("@Div", sd.divcode),
                            new SqlParameter("@sfempid", sd.sfempid),
                            new SqlParameter("@sfdept", sd.sfdept),
                            new SqlParameter("@subdiv", sub_division),
                            new SqlParameter("@SfTyp", sd.sftype),
                            new SqlParameter("@sfdesig", sd.sfdesg),
                            new SqlParameter("@sfterr", sd.sfterr),
                            new SqlParameter("@FFtype", sd.sflevel),
                            new SqlParameter("@SfPimg", sd.sfpimg),
                            new SqlParameter("@SFHQN", sd.sfhqname),
                            new SqlParameter("@SFSdiv", sd.sfsdiv),
                            new SqlParameter("@SFDepot", sd.sfdepot),
                            new SqlParameter("@sxml", sxml),
                            new SqlParameter("@SFDistrict",sd.sfdistrict),
                            new SqlParameter("@SfRDate", sd.Sfrdate)
                        };

                        if (sd.sfcode.ToString() != "")
                        {
                            try
                            {
                                db.Exec_NonQueryWithParamNew("updateNewSalesForce", CommandType.StoredProcedure, parameters);
                                msg = "Update Success";

                                DB_EReporting db_ER = new DB_EReporting();
                                //string Squery = "SELECT *FROM  Trans_SF_Custom_Field WHERE SFID = '" + sd.sfcode + "'";
                                //DataSet ds = db_ER.Exec_DataSet(Squery);
                                DataSet ds = GetSFAddtionalFieldData(Convert.ToString(sd.sfcode));

                                if (ds.Tables[0].Rows.Count > 0)
                                {
                                    string fld = ""; string val = "";

                                    if (af.Count > 0)
                                    {
                                        int i = 0;

                                        for (int k = 0; k < af.Count; k++)
                                        {
                                            if ((af[k].Fields == "'undefined'" || af[k].Fields == "undefined") && (af[k].Values == "'undefined'" || af[k].Values == "undefined"))
                                            { }
                                            else
                                            {
                                                //fld += addfields[k].Fields + ",";//val += "'" + addfields[k].Values + "',";                                        
                                                fld = af[k].Fields;
                                                if ((af[k].Values == null || af[k].Values == "")) { val = "''"; }
                                                else { val = "'" + af[k].Values + "'"; }


                                                string uquery = "UPDATE Trans_SF_Custom_Field  SET " + fld + " = " + val.TrimEnd(',') + " WHERE SFID = '" + Convert.ToString(sd.sfcode) + "' ";
                                                i = db_ER.ExecQry(uquery);
                                            }

                                        }

                                        if (i > 0)
                                        { msg = "Updated Success"; }
                                        else { msg = "Updated Success"; }
                                    }

                                }
                                else
                                {
                                    string fld = ""; string val = "";
                                    int i = 0;

                                    if (af.Count > 0)
                                    {
                                        for (int k = 0; k < af.Count; k++)
                                        {
                                            if ((af[k].Fields == "'undefined'" || af[k].Fields == "undefined") && (af[k].Values == "'undefined'" || af[k].Values == "undefined"))
                                            { }
                                            else
                                            {
                                                fld += af[k].Fields + ",";

                                                if ((af[k].Values == null || af[k].Values == "")) { val += "'',"; }
                                                else { val += "'" + af[k].Values + "',"; }
                                            }
                                        }

                                        if ((fld != null || fld != "") && (val != null || val != ""))
                                        {
                                            string Sf_Code = Convert.ToString(sd.sfcode);

                                            //string Iquery = " EXEC Insert_TransSFCustomField  '" + Sf_Code + "', " + fld.TrimEnd(',') + "," + val.TrimEnd(',') + "";

                                            string Iquery = "Insert Into Trans_SF_Custom_Field(SFID, " + fld.TrimEnd(',') + ") Values('" + Sf_Code + "'," + val.TrimEnd(',') + ")";

                                            i = db_ER.ExecQry(Iquery);
                                        }

                                        if (i > 0)
                                        { msg = "Updated Success"; }
                                        else { msg = "Updated Success"; }
                                    }
                                    //else { msg = "Error"; }
                                }
                            }
                            catch (Exception ex)
                            {
                                throw ex;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        msg = ex.Message;
                    }
                }
                else
                {
                    msg = "Dup";
                }

            }
            return msg;
        }

        public bool CheckDupUserName(string User_Name)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(UsrDfd_UserName) FROM Mas_Salesforce WHERE UsrDfd_UserName='" + User_Name + "'";
                int iRecordExist = db.Exec_Scalar(strQry);

                if (iRecordExist > 0)
                    bRecordExist = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return bRecordExist;
        }

        public bool CheckDupUserName(string User_Name, string sf_code)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(UsrDfd_UserName) FROM Mas_Salesforce WHERE UsrDfd_UserName='" + User_Name + "'  AND sf_code !='" + sf_code + "'";
                int iRecordExist = db.Exec_Scalar(strQry);

                if (iRecordExist > 0)
                    bRecordExist = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return bRecordExist;
        }

        public string getsfcode_new(string sf_type, string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            string sf_code = string.Empty;

            string sftype = string.Empty;
            string divsname = string.Empty;

            if (divcode == null || divcode == "")
            { divcode = "0"; }

            divsname = db_ER.Exec_Scalar_s("select Division_SName from Mas_Division where Division_Code=" + divcode + "");
            if (sf_type == "1")
            {
                sftype = "MR";
                strQry = "SELECT  isnull(max(Cast(SubString(Sf_Code,3, LEN(Sf_Code)) as int)),0)+1 FROM mas_salesforce where Sf_Code like '" + sftype + "%'  ";
            }
            else
            {
                sftype = "MGR";
                strQry = "SELECT  isnull(max(Cast(SubString(Sf_Code,4, LEN(Sf_Code)) as int)),0)+1 FROM mas_salesforce where Sf_Code like '" + sftype + "%'  ";
            }
            //'" + sftype + "'+ (case len(MXno) when 1 then '000' when 2 then '00' when 3 then '0' else '' end) +
            strQry = "select MXno from(select  isnull(max(cast(REPLACE(SF_Code, '" + divsname + sftype + "', '') as numeric)), 0) + 1  MXno from Mas_Salesforce where isnumeric(REPLACE(SF_Code, '" + divsname + sftype + "', '')) = 1) as t";
            int iCnt = db_ER.Exec_Scalar(strQry);
            if (iCnt.ToString().Length == 1)
            {
                sf_code = "000" + iCnt.ToString();
            }
            else if (iCnt.ToString().Length == 2)
            {
                sf_code = "00" + iCnt.ToString();
            }
            else if (iCnt.ToString().Length == 3)
            {
                sf_code = "0" + iCnt.ToString();
            }
            else
            {
                sf_code = iCnt.ToString();
            }
            sf_code = divsname + sftype + sf_code;

            return sf_code;
        }

    }


}