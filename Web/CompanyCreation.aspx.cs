using System;
using System.Web.Services;

/*public partial class CompanyCreation : System.Web.UI.Page
{

    private const string SERVER_IP = "37.61.220.198";// put your ip address
    private const int PORT = 80;
    private const string WEB_DOMAIN_PATH = @"F:\\web\domains\{0}\";

    //Live server
    //private const string SERVER_IP = "192.168.111.111";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request.QueryString["user"]))
        {

            try
            {
                string username = Request.QueryString["user"];
                string status = CreateUserSite(username, "sanfmcg.com");//change your Domain id

                Response.Write(status);
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }
        else
        {
            Response.Write("user parameter not supplied");
        }


    }
    [WebMethod]
    public static string CreateWebSite(string SiteName, string IP)
    {
        string Msg = "";

        try
        {
            string username = SiteName;
            Msg = CreateUserSite(username, "sanfmcg.com");//change your Domain id
        }
        catch (Exception ae)
        {
            Msg = "Error: " + ae.Message;
        }
        return Msg;
    }
    private static string CreateUserSite(string user, string domain)
    {


        string path = string.Format(WEB_DOMAIN_PATH, domain);

        string userpath = path + user;

        string userUrl = user + "." + domain;

        using (ServerManager serverManager = new ServerManager())
        {

            bool siteExists = false;
            int number = serverManager.Sites.Where(p => p.Name.ToLower().Equals(userUrl.ToLower())).Count();

            if (number == 0)
            {
                siteExists = false;
            }
            else
            {
                siteExists = true;
            }

            if (!siteExists)
            {

                //create user directory
                Directory.CreateDirectory(userpath);

                //copy every files from a-base to a new created folder
                FileInfo[] d = new DirectoryInfo(path + @"\a-base").GetFiles();
                foreach (FileInfo fi in d)
                {
                    File.Copy(fi.FullName, userpath + @"\" + fi.Name, true);
                }

                //create a directory
                Directory.CreateDirectory(userpath + @"\swfobject");

                FileInfo[] d1 = new DirectoryInfo(path + @"\a-base\swfobject").GetFiles();
                foreach (FileInfo fi in d1)
                {
                    File.Copy(fi.FullName, userpath + @"\swfobject\" + fi.Name, true);
                }



                //create site
                Site mySite = serverManager.Sites.Add(userUrl, path + user, PORT);
                mySite.ServerAutoStart = true;
                mySite.Applications[0].ApplicationPoolName = domain;

                //create bindings
                mySite.Bindings.Clear();
                mySite.Bindings.Add(string.Format("{0}:{2}:{1}", SERVER_IP, userUrl, PORT), "http");
                mySite.Bindings.Add(string.Format("{0}:{2}:www.{1}", SERVER_IP, userUrl, PORT), "http");


                Configuration config = serverManager.GetApplicationHostConfiguration();
                ConfigurationSection httpLoggingSection = config.GetSection("system.webServer/httpLogging", userUrl);
                httpLoggingSection["dontLog"] = true;

                serverManager.CommitChanges();

                // ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('" + userUrl + " created');", true);

            }
            else
            {
                //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "error", "alert('user exists. Please use other name');", true);
                throw new Exception("user exists. Please use other name");
            }


            return userUrl + " has been successfully created";
        }
    }
}*/
/*
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;

//37.61.220.198*/

using Microsoft.Web.Administration;
public partial class CompanyCreation : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }


    [WebMethod]
    public static string CreateWebSite(string SiteName, string IP)
    {
        string Msg = "";

        try
        {
            ServerManager serverMgr = new ServerManager();
            string strhostname = "sanfmcg.com"; //abc.com
            string strWebsitename = SiteName + "." + strhostname; // abc
            string strApplicationPool = SiteName+"."+ strhostname;  // set your deafultpool :4.0 in IIS
            string stripaddress = IP;// ip address
            string bindinginfo =  "*:80:" + strhostname;
            //check if website name already exists in IIS
            Boolean bWebsite = IsWebsiteExists(strWebsitename, serverMgr);
            if (!bWebsite)
            {
                serverMgr.ApplicationPools.Add(strApplicationPool);
                Site mySite = serverMgr.Sites.Add(strWebsitename.ToString(), "http", bindinginfo, "F:\\websites\\dev.sanfmcg.com\\E-Report_DotNet");
                mySite.ApplicationDefaults.ApplicationPoolName = strApplicationPool;


                //create bindings
                mySite.Bindings.Clear();
                mySite.Bindings.Add(string.Format("{0}:{2}:{1}", "*", strhostname, "80"), "https");
                mySite.Bindings.Add(string.Format("{0}:{2}:www.{1}", "*", strhostname, "80"), "https");
                mySite.Bindings.Add(string.Format("{0}:{2}:{1}", "*", strhostname, "80"), "http");
                mySite.Bindings.Add(string.Format("{0}:{2}:www.{1}", "*", strhostname, "80"), "http");

                serverMgr.CommitChanges();
                Msg = "New website  " + strWebsitename + " added sucessfully";
            }
            else
            {
                Msg = "Name should be unique, " + strWebsitename + "  is already exists. ";
            }
        }
        catch (Exception ae)
        {
            Msg = "Error: " + ae.Message;
        }
        return Msg;
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
