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
using System.Xml;
using System.IO;
using System.Configuration;
using System.Net.Mail;
using System.Net.Configuration;
using System.Net;
using System.Text;
using Microsoft.Web.Administration;
using System.Security.Cryptography.X509Certificates;

public partial class MasterFiles_Company_Approval_form : System.Web.UI.Page
{
    public string sf_code = string.Empty;
    public string divcode = string.Empty;
    public static string chksfcode = string.Empty;
    string sf_type = string.Empty;
    public static string Div = string.Empty;
    public string ccode = string.Empty;
    public string Comp_id = string.Empty;
    private string Comp_Name = string.Empty;
    private string strConn = ConfigurationManager.ConnectionStrings["MasterDB"].ToString();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            divcode = Session["div_code"].ToString();
            Comp_id = Request.QueryString["Comp_id"].ToString();
            //Comp_Name = Request.QueryString["Comp_Name"].ToString();
        }
    }
    public class CompanyData
    {
        public string comp_id { get; set; }
        public string cmpnyname { get; set; }
        public string code { get; set; }
        public string address { get; set; }
        public string country { get; set; }
        public string city { get; set; }
        public string gstno { get; set; }
        public string purl { get; set; }
        public string logo_Img { get; set; }
        public string state { get; set; }
        public string pincode { get; set; }
        public string billname { get; set; }
        public string billmod { get; set; }
        public string month { get; set; }
        public string amount { get; set; }
        public string nousr { get; set; }
        public string biltype { get; set; }
        public string range { get; set; }
        public string acost { get; set; }
        public string prop { get; set; }
        public string bname { get; set; }
        public string bmobile { get; set; }
        public string bgmail { get; set; }
        public string dname { get; set; }
        public string dmobile { get; set; }
        public string dgmail { get; set; }
        public string statecd { get; set; }
        public string cntryId { get; set; }


    }

    [WebMethod]
    public static string getCert()
    {
        string Msg = "";
        try
        {
            string certificateName = "CN=salesjump";
            X509Store store = new X509Store(StoreName.My, StoreLocation.LocalMachine);
            store.Open(OpenFlags.OpenExistingOnly | OpenFlags.ReadWrite);
            X509Certificate2Collection certificates =
                            store.Certificates.Find(X509FindType.FindBySubjectName,
                            certificateName,
                            true);
            X509Certificate2 certSel=null;
            foreach (X509Certificate2 cert in store.Certificates)
            {
                Msg += "\n certificates :" + cert.FriendlyName;
                if (cert.FriendlyName == "salesjump") certSel = cert;
            }
            if(certSel!=null)
                Msg += "\n Select certificate :" + certSel.FriendlyName;
        }
        catch (Exception ae)
        {
            Msg += "Error: " + ae.Message;
        }
        return "{\"Msg\":\"" + Msg + "\"}";
    }

    [WebMethod]
    public static CompanyData[] getcompdets(string Comp_id)
    {
        string msg = string.Empty;
        MasterFiles_Company_Approval_form dss = new MasterFiles_Company_Approval_form();
        DataSet dsm = dss.getcompanydets(Comp_id);
        List<CompanyData> ad = new List<CompanyData>();
        foreach (DataRow row in dsm.Tables[0].Rows)
        {
            CompanyData asd = new CompanyData();
            asd.comp_id = row["Comp_id"].ToString();
            asd.cmpnyname = row["Comp_Name"].ToString();
            asd.code = row["Comp_Code"].ToString().Trim();
            asd.address = row["Addr1"].ToString();
            asd.country = row["cCountry"].ToString();
            asd.cntryId = row["Country_code"].ToString();
            
            asd.city = row["cCity"].ToString();
            asd.gstno = row["GSTNo"].ToString();
            asd.purl = row["PrefURL"].ToString().Trim();
            asd.logo_Img = row["Logo_Img"].ToString();
            asd.state = row["cState"].ToString();
            asd.statecd = row["State_Code"].ToString();
            asd.pincode = row["cPinCd"].ToString();
            asd.prop = row["Propsalpath"].ToString();
            asd.billname = row["BillNm"].ToString();
            asd.billmod = row["BillMode"].ToString();
            asd.month = row["StartMn"].ToString();
            asd.amount = row["Amount"].ToString();
            asd.nousr = row["NofUser"].ToString();
            asd.biltype = row["Type"].ToString();
            asd.range = row["RngVal"].ToString();
            asd.acost = row["AddUserCost"].ToString();
            ad.Add(asd);
        }
        foreach (DataRow row in dsm.Tables[1].Rows)
        {
            CompanyData asd = new CompanyData();

            {
                asd.bname = row["bContactNm"].ToString();
                asd.bmobile = row["bMobileNo"].ToString();
                asd.bgmail = row["bEmail"].ToString();
                ad.Add(asd);
            }

        }
        foreach (DataRow row in dsm.Tables[2].Rows)
        {
            CompanyData asd = new CompanyData();

            {
                asd.dname = row["dContactNm"].ToString();
                asd.dmobile = row["dMobileNo"].ToString();
                asd.dgmail = row["dEmail"].ToString();
                ad.Add(asd);
            }

        }
        return ad.ToArray(); ;
    }

    public DataSet getcompanydets(string Comp_id)
    {
        DataSet dsAdmin = new DataSet();
        MasterFiles_Company_Approval_form dbER = new MasterFiles_Company_Approval_form();

        String strQry = "exec getCompanyDets '" + Comp_id + "'";
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
    public DataSet Exec_DataSet(string strQry)
    {
        DataSet ds_EReport = new DataSet();
        SqlDataAdapter da_EReport = new SqlDataAdapter();
        SqlConnection _conn = new SqlConnection(strConn);
        try
        {


            SqlCommand selectCMD = new SqlCommand(strQry, _conn);
            selectCMD.CommandTimeout = 120;

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
            da_EReport.Dispose();
            _conn.Dispose();
        }
        return ds_EReport;
    }
    [WebMethod]
    public static string DownloadFile(string fileName)
    {
        //Set the File Folder Path.
        string path = HttpContext.Current.Server.MapPath("~/Proposals/");

        //Read the File as Byte Array.
        byte[] bytes = File.ReadAllBytes(path + fileName);

        //Convert File to Base64 string and send to Client.
        return Convert.ToBase64String(bytes, 0, bytes.Length);
    }
    [WebMethod]
    public static string SendEmail(string toEmail)
    {
        string to = toEmail; //To address    
        string from = "sender@gmail.com"; //From address    
        MailMessage message = new MailMessage(from, to);
        string mailbody = "Your Company Requisition has been approved from SANEFORCE";
        message.Subject = "Company Approval from saneForce";
        message.Body = mailbody;
        message.BodyEncoding = Encoding.UTF8;
        message.IsBodyHtml = true;
        SmtpClient client = new SmtpClient("smtp.gmail.com", 587); //Gmail smtp    
        System.Net.NetworkCredential basicCredential1 = new
        System.Net.NetworkCredential("yourmail id", "Password");
        client.EnableSsl = true;
        client.UseDefaultCredentials = false;
        client.Credentials = basicCredential1;
        try
        {
            client.Send(message);
        }

        catch (Exception ex)
        {
            throw ex;
        }
        return "Mail Sent";
    }
	[WebMethod]
	public static string CreateDB(string IP,string SiteName,string CmpName,string SHNM,string States,string CountryCd,int UserCnt,int Cnt,int Rate){
		string msg="DB Created";
		using (SqlConnection con = new SqlConnection(Globals.MasterConnString))
            {
                using (SqlCommand cmd = con.CreateCommand())
                {
		            SqlParameter[] parameters = new SqlParameter[]
                    {
                        new SqlParameter("@Comp", CmpName),
                        new SqlParameter("@SHNM", SHNM),
                        new SqlParameter("@Url", SiteName),
                        new SqlParameter("@States", States),
                        new SqlParameter("@CountryCd", CountryCd),
                        new SqlParameter("@UserCnt", UserCnt),
                        new SqlParameter("@Cnt", Cnt),
                        new SqlParameter("@Rate", Rate),
                    };
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "CreateNewSiteDB";
                    cmd.Parameters.AddRange(parameters);

                    try
                    {
                        if (con.State != ConnectionState.Open)
                        {
                            con.Open();
                        }

                        cmd.ExecuteNonQuery();
			
                    }
                    catch
                    {
                        throw;
			            msg= "Error";
                    }
                }
            }
		return msg;
               
	}
    

	[WebMethod]
    public static string CreateWebSite(string SiteName, string IP)
    {
        string Msg = "";
        int res = 0;
        string ErrNo = "0";
        try
        {
             ServerManager serverMgr = new ServerManager();
             string strhostname = "salesjump.in"; //abc.com
             string strWebsitename = SiteName.ToLower() + "." + strhostname; // abc
             string strApplicationPool = SiteName.ToLower() + "." + strhostname;  // set your deafultpool :4.0 in IIS
             string stripaddress = IP;// ip address
             string bindinginfo = "*:80:" + strhostname;
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;

            //check if website name already exists in IIS
            Boolean bWebsite = IsWebsiteExists(strWebsitename, serverMgr);
             if (!bWebsite)
             {
                 serverMgr.ApplicationPools.Add(strApplicationPool);
                 Site mySite = serverMgr.Sites.Add(strWebsitename.ToString(), "http", bindinginfo, "D:\\Websites\\SalesJump\\E-Report_DotNet\\"); //"D:\\Website\\fmcg"
                 mySite.ApplicationDefaults.ApplicationPoolName = strApplicationPool;

                ErrNo = "1";
                 //create bindings
                    mySite.Bindings.Clear();
                    mySite.Bindings.Add(string.Format("{0}:{2}:{1}", "*", strWebsitename, "443"), "https");
                    mySite.Bindings.Add(string.Format("{0}:{2}:www.{1}", "*", strWebsitename, "433"), "https");
                    mySite.Bindings.Add(string.Format("{0}:{2}:{1}", "*", strWebsitename, "80"), "http");
                    mySite.Bindings.Add(string.Format("{0}:{2}:www.{1}", "*", strWebsitename, "80"), "http");

                //////X509Store store = new X509Store(StoreName.My, StoreLocation.LocalMachine);
                //////store.Open(OpenFlags.OpenExistingOnly | OpenFlags.ReadWrite);

                //////X509Certificate2 certSel = null;
                //////foreach (X509Certificate2 cert in store.Certificates)
                //////{
                //////    if (cert.FriendlyName == "salesjump") certSel = cert;
                //////}
                //////if (certSel != null)
                //////{
                //////    ErrNo = "1.0.9";
                //////    var binding = mySite.Bindings.Add("*:443:" + strWebsitename, certSel.GetCertHash(), "My");
                //////    binding.Protocol = "https";
                //////}
                //////store.Close();
                ErrNo = "1.1.9";
                // mySite.VirtualDirectoryDefaults.UserName = "opc";
                //mySite.VirtualDirectoryDefaults.Password = "SanMedia#1234";

                serverMgr.CommitChanges();



                Msg = "New website  " + strWebsitename + " added sucessfully";
             }
             else
             {
            	Msg = "Name should be unique, ";//+ strWebsitename + "  is already exists. ";
            }
            ErrNo = "1.5";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://api.godaddy.com/api/v1/domains/salesjump.in/records");
            //ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
            ErrNo = "2";
            request.ContentType = "application/json";
            request.Method = "PATCH";
            request.Headers.Add("Authorization", "sso-key 9EXfajmCWKM_4iSbwgVvxRtnqwbehwJXon:2c6dFMhKsSXi5gTHYNKb36"); //Add a valid API Key
            string postData = "[{\"data\": \"" + IP + "\",\"name\": \"" + SiteName.ToLower() + "\",\"ttl\": 600,\"type\": \"A\"}]";
            ErrNo = "3";
            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write(postData);
            }

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            res = 1;
        }
        catch (Exception ae)
        {
            Msg += "Error: " + ae.Message;
        }
        return "{\"res\":\"" + res+"-"+ ErrNo + "\",\"Msg\":\"" + Msg + "\"}";
    }
    public static bool IsWebsiteExists(string strWebsitename, ServerManager serverMgr)
    {
        Boolean flagset = false;
        SiteCollection sitecollection = serverMgr.Sites;
        foreach (Site site in sitecollection)
        {
            if (site.Name == strWebsitename.ToString())
            {
                flagset = true;
                break;
            }
            else
            {
                flagset = false;
            }
        }
        return flagset;
    }
}