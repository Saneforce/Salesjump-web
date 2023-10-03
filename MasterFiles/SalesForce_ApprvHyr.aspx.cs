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
using System.Xml;
using System.IO;
using System.Data.OleDb;
using DBase_EReport;

public partial class MasterFiles_SalesForce_ApprvHyr : System.Web.UI.Page
{
    public string sf_code = string.Empty;
    public static string chksfcode = string.Empty;
    string sf_type = string.Empty;
    public static string Div = string.Empty;
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
        sf_code = Request.QueryString["sfcode"];
        chksfcode = sf_code;
        Div = Session["div_code"].ToString();
    }
    [WebMethod]
    public static string getpnding(string sf)
    {
        DataSet ds = new DataSet();
        SqlConnection con = new SqlConnection(Globals.ConnString);
        con.Open();
        SqlCommand cmd = new SqlCommand("SELECT * FROM Trans_Expense_Head_Periodic th inner join Trans_Expense_Detail_Periodic td with(nolock) on th.Trans_Sl_No=td.trans_dt_slno where th.SF_Code='" + chksfcode + "' and Appr_By='" + sf + "'", con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(ds);
        con.Close();
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    [WebMethod]
    public static string getSFWorkedWith(string sfcode)
    {    
        DataSet dstp = get_DayPlanWorkedWith(sfcode,Div);
        return JsonConvert.SerializeObject(dstp.Tables[0]);
    }
    public static DataSet get_DayPlanWorkedWith(string sf_Code,string Div)
    {
        DB_EReporting db_ER = new DB_EReporting();
        DataSet dsSF = null;
        string strQry = "exec getJointWork '" + sf_Code + "','"+ Div + "'";
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
    [WebMethod]
    public static string getApprSFDets(string SF, string div, string apprTyp)
    {
        DataSet ds = new DataSet();
        DB_EReporting db_ER = new DB_EReporting();
        string strQry = "exec getApprSFDetails '" + SF + "'," + div + "," + apprTyp + "";
        try
        {
            ds = db_ER.Exec_DataSet(strQry);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        //SqlConnection con = new SqlConnection(Globals.ConnString);
        //con.Open();
        //SqlCommand cmd = new SqlCommand("exec getApprSFDetails '" + SF + "'," + div + "," + apprTyp + "", con);
        //SqlDataAdapter da = new SqlDataAdapter(cmd);
        //da.Fill(ds);
        //con.Close();
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }

    [WebMethod]
    public static string getDepts(string divcode, string subdiv)
    {
        SalesForce SFD = new SalesForce();
        DataSet dds = SFD.getAllSF_Dept(divcode, subdiv.TrimEnd(','));
        return JsonConvert.SerializeObject(dds.Tables[0]);
    }

    [WebMethod]
    public static string getDesignation(string divcode)
    {
        SalesForce SFD = new SalesForce();
        DataSet dds = SFD.getAllSF_Designation(divcode);
        return JsonConvert.SerializeObject(dds.Tables[0]);
    }

    [WebMethod]
    public static string getDivision(string divcode)
    {
        SalesForce SFD = new SalesForce();
        DataSet dds = SFD.Getsubdivisionwise(divcode);
        return JsonConvert.SerializeObject(dds.Tables[0]);
    }

    [WebMethod]
    public static string getStates(string divcode)
    {
        SalesForce SFD = new SalesForce();
        DataSet dds = SFD.getAllSF_States(divcode);
        return JsonConvert.SerializeObject(dds.Tables[0]);
    }

    [WebMethod]
    public static string getDivisions(string divcode)
    {
        SalesForce SFD = new SalesForce();
        DataSet dds = SFD.Getsubdivisionwise(divcode);
        return JsonConvert.SerializeObject(dds.Tables[0]);
    }

    [WebMethod]
    public static string getDivisionsname(string divcode)
    {
        SalesForce SFD = new SalesForce();
        DataSet dds = SFD.getDivisname(divcode);
        return JsonConvert.SerializeObject(dds.Tables[0]);
    }
    [WebMethod]
    public static string getDistrict(string divcode)
    {
        SalesForce SFD = new SalesForce();
        DataSet dds = SFD.getAllSF_District(divcode);
        return JsonConvert.SerializeObject(dds.Tables[0]);
    }
    [WebMethod]
    public static string getTerritory(string divcode)
    {
        SalesForce SFD = new SalesForce();
        DataSet dds = SFD.getAllSF_Territory(divcode);
        return JsonConvert.SerializeObject(dds.Tables[0]);
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
        SalesForce SFD = new SalesForce();
        DataSet dds = SFD.getSFType(divcode);
        return JsonConvert.SerializeObject(dds.Tables[0]);
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
        DataTable dds = SFD.getEmployeeAdditionalFields(divcode);
        return JsonConvert.SerializeObject(dds);
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
    public static string GetCustomFormsFieldsList(string divcode, string ModuleId)
    {
        DataSet ds = new DataSet();
        AdminSetup Ad = new AdminSetup();
        ds = Ad.GetCustomFormsFieldsData(divcode, ModuleId);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
}