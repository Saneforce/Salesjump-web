using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bus_EReport;
using DBase_EReport;
using Newtonsoft.Json;
using System.Globalization;
using ClosedXML.Excel;
using System.Xml;
using System.IO;
using System.Data.OleDb;
using System.Configuration;

public partial class MasterFiles_Company_Creation : System.Web.UI.Page
{
    public string sf_code = string.Empty;
    public static string chksfcode = string.Empty;
    string sf_type = string.Empty;
    public static string Div = string.Empty;
    public string ccode = string.Empty;
    private static object Comp_ID;
    private string strConn = ConfigurationManager.ConnectionStrings["MasterDB"].ToString();

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
        ccode = Request.QueryString["ccode"];
        chksfcode = sf_code;
        Div = Session["div_code"].ToString();
    }
    [WebMethod]
    public static string getcountry(string divcode)
    {
        
        DataSet dds = getAllCountry(divcode);
        return JsonConvert.SerializeObject(dds.Tables[0]);
    }

    public  static DataSet getAllCountry(string divcode)
    {
        MasterFiles_Company_Creation Mc = new MasterFiles_Company_Creation();
        DataSet ds = null;
        string strQry = "select mc.Country_code ccode,mc.S_name sname,mc.Country_name cname from Mas_country mc ";
        try
        {
            ds = Mc.Exec_DataSet(strQry);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return ds;
    }
    [WebMethod]
    public static string getstates(string ccode)
    {

        DataSet dds = getAllstates(ccode);
        return JsonConvert.SerializeObject(dds.Tables[0]);
    }

    public static DataSet getAllstates(string ccode)
    {
        MasterFiles_Company_Creation Mc = new MasterFiles_Company_Creation();
        DataSet ds = null;
        string strQry = "select ms.State_Code scode, shortName sshname,ms.StateName sname from Mas_State ms  where ms.Country_code = " + ccode + "";
        try
        {
            ds = Mc.Exec_DataSet(strQry);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return ds;
    }
    public DataSet Exec_DataSet(string strQry)
    {
        DataSet ds_EReport = new DataSet();

        SqlConnection _conn = new SqlConnection(strConn);
        try
        {


            SqlCommand selectCMD = new SqlCommand(strQry, _conn);
            selectCMD.CommandTimeout = 120;

            SqlDataAdapter da_EReport = new SqlDataAdapter();
            da_EReport.SelectCommand = selectCMD;

            _conn.Open();

            da_EReport.Fill(ds_EReport, "Customers");

            // _conn.Close();
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            _conn.Close();
            _conn.Dispose();
        }
        return ds_EReport;
    }
    [WebMethod]
    public static string SaveCountry(string sXML)
    {
        DCR sf = new DCR();
        int iReturn = -1;

        MasterFiles_Company_Creation Mc = new MasterFiles_Company_Creation();
        string strQry = "exec Addnewcountry '" + sXML + "'";
        try
        {
            iReturn = Mc.ExecQry(strQry);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        if (iReturn > 0)
        {
            return "Successfully added";
        }
        else
        {
            return "Failure";
        }
    }
    public int ExecQry(string sQry)
    {
        int iReturn = -1;
        SqlConnection _conn = new SqlConnection(strConn);
        try
        {
            //SqlConnection _conn = new SqlConnection(strConn);
            System.Data.SqlClient.SqlCommand cmd;
            cmd = new System.Data.SqlClient.SqlCommand();
            cmd.Connection = _conn;
            cmd.CommandText = sQry;
            _conn.Open();
            iReturn = cmd.ExecuteNonQuery();
            // _conn.Close();
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            _conn.Close();
            _conn.Dispose();
        }
        return iReturn;
    }
    [WebMethod]
    public static string SaveState(string sXML)
    {
        DCR sf = new DCR();
        int iReturn = -1;

        MasterFiles_Company_Creation Mc = new MasterFiles_Company_Creation();
        string strQry = "exec Addnewstate '" + sXML + "'";
        try
        {
            iReturn = Mc.ExecQry(strQry);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        if (iReturn > 0)
        {
            return "Successfully added";
        }
        else
        {
            return "Failure";
        }
    }
  
    [WebMethod]
    public static string savecompanycreate(string data)
    {
        string msg = string.Empty;
        SaveCompany Data = JsonConvert.DeserializeObject<SaveCompany>(data);
       // SalesForce dss = new SalesForce();
        msg = saveNewcompany(Data);
        return msg;
    }
    public class SaveCompany
    {
        [JsonProperty("COMPANY_NAME")]
        public string cmpnyname { get; set; }

        [JsonProperty("CODE")]
        public object code { get; set; }

        [JsonProperty("logo_Img")]
        public object logo_Img { get; set; }

        [JsonProperty("URL")]
        public object purl { get; set; }

        [JsonProperty("ADDRESS")]
        public object address { get; set; }

        [JsonProperty("COUNTRY")]
        public object country { get; set; }

        [JsonProperty("State")]
        public object state { get; set; }

        [JsonProperty("CITY")]
        public string city { get; set; }

        [JsonProperty("Pincode")]
        public object pincode { get; set; }

        [JsonProperty("ID")]
        public object usrID { get; set; }

        [JsonProperty("USR_Name")]
        public object usrName { get; set; }

        [JsonProperty("Req_DATE")]
        public object DT { get; set; }

        [JsonProperty("Bill_Name")]
        public object billname { get; set; }

        [JsonProperty("NoOfUser")]
        public object nousr { get; set; }

        [JsonProperty("BillMode")]
        public object billmod { get; set; }

        [JsonProperty("Type")]
        public object biltype { get; set; }

        [JsonProperty("StartMn")]
        public object month { get; set; }

        [JsonProperty("RngVal")]
        public object range { get; set; }

        [JsonProperty("Amount")]
        public object amount { get; set; }

        [JsonProperty("AddUserCost")]
        public object acost { get; set; }

        [JsonProperty("GSTNO")]
        public object gstno { get; set; }

        [JsonProperty("Proposalpath")]
        public object prop { get; set; }

        [JsonProperty("CBArr")]
        public List<contact_for_Bill> cfbarr { get; set; }

        [JsonProperty("CDArr")]
        public List<contact_for_Data> cfdarr { get; set; }


    }
    public class contact_for_Bill
    {
        [JsonProperty("bname")]
        public string bname { get; set; }

        [JsonProperty("bmobile")]
        public string bmobile { get; set; }

        [JsonProperty("bmail")]
        public string bgmail { get; set; }
    }
    public class contact_for_Data
    {
        [JsonProperty("dname")]
        public string dname { get; set; }

        [JsonProperty("dmobile")]
        public string dmobile { get; set; }

        [JsonProperty("dmail")]
        public string dgmail { get; set; }
    }
    public static string saveNewcompany(SaveCompany sd)
    {
        int iReturn = -1;
        string dup = string.Empty;
        MasterFiles_Company_Creation Mc = new MasterFiles_Company_Creation();
        string strSfCode = string.Empty;

        string msg = string.Empty;
        List<contact_for_Bill> a = sd.cfbarr;
        List<contact_for_Data> b = sd.cfdarr;

        string sxml = "<ROOT>";
        for (int i = 0; i < a.Count; i++)
        {
            if (a[i].bname != "" && a[i].bname != null)
            {
                sxml += "<ASSD stype=\"billing\" bname=\"" + a[i].bname + "\" bmob=\"" + a[i].bmobile + "\" bmail=\"" + a[i].bgmail + "\" />";
            }
        }
        sxml += "</ROOT>";
        string sxml1 = "<ROOT>";
        for (int i = 0; i < b.Count; i++)
        {
            if (b[i].dname != "" && b[i].dname != null)
            {
                sxml1 += "<ASSD stype=\"Data\" dname=\"" + b[i].dname + "\" dmob=\"" + b[i].dmobile + "\" dmail=\"" + b[i].dgmail + "\" />";
            }
        }
        sxml1 += "</ROOT>";

        try
        {          

            SqlParameter[] parameters = new SqlParameter[]
            {

                        new SqlParameter("@COMPANY_NAME", sd.cmpnyname),
                        new SqlParameter("@CODE", sd.code),                        
                        new SqlParameter("@URL", sd.purl),
                        new SqlParameter("@ADDRESS", sd.address),
                        new SqlParameter("@COUNTRY", sd.country),
                        new SqlParameter("@State", sd.state),
                        new SqlParameter("@CITY", sd.city),
                        new SqlParameter("@Pincode", sd.pincode),
                        new SqlParameter("@ID", sd.usrID),
                        new SqlParameter("@USR_Name", sd.usrName),
                        new SqlParameter("@Req_DATE", sd.DT),
                        new SqlParameter("@logo_Img", sd.logo_Img),
                        new SqlParameter("@Bill_Name", sd.billname),
                        new SqlParameter("@NoOfUser", sd.nousr),
                        new SqlParameter("@BillMode", sd.billmod),
                        new SqlParameter("@Type", sd.biltype),
                        new SqlParameter("@StartMn", sd.month),
                        new SqlParameter("@RngVal", sd.range),
                        new SqlParameter("@Amount", sd.amount),
                        new SqlParameter("@AddUserCost", sd.acost),
                        new SqlParameter("@GSTNO", sd.gstno),
                        new SqlParameter("@Proposalpath", sd.prop),
                        new SqlParameter("@sxml", sxml),
                        new SqlParameter("@sxml1", sxml1)

            };
            //if (Comp_ID.ToString() != "")
            //{
            try
            {
                Mc.Exec_NonQueryWithParamNew("insertCompanycreate", CommandType.StoredProcedure, parameters);
                msg = "Success";
            }
            catch (Exception ex)
            {
                throw ex;
            }

            // }
        }
        catch (Exception ex)
        {
            msg = ex.Message;
        }
        if (msg == "Success")
        {
           
            msg = "Company Profile Created";
        }

        return msg;
    }
    public bool Exec_NonQueryWithParamNew(string CommandName, CommandType cmdType, SqlParameter[] pars)
    {
        int result = 0;

        using (SqlConnection con = new SqlConnection(strConn))
        {
            using (SqlCommand cmd = con.CreateCommand())
            {
                cmd.CommandType = cmdType;
                cmd.CommandText = CommandName;
                cmd.Parameters.AddRange(pars);

                try
                {
                    if (con.State != ConnectionState.Open)
                    {
                        con.Open();
                    }

                    result = cmd.ExecuteNonQuery();
                }
                catch
                {
                    throw;
                }
            }
        }

        return (result > 0);
    }
}

