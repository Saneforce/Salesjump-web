using DBase_EReport;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Interop;

public partial class MasterFiles_Asset_Request_creation : System.Web.UI.Page
{
    public static string div_code = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
    }
    [WebMethod]
    public static string getAllcategory()
    {
        DB_EReporting db_ER = new DB_EReporting();
        DataSet ds = new DataSet();
        ds = db_ER.Exec_DataSet("select category_Id,category_Name from Mas_Asset_Category where division_code='" + div_code + "' and active_flag=0");
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    [WebMethod]
    public static string getAllmodel()
    {
        DB_EReporting db_ER = new DB_EReporting();
        DataSet ds = new DataSet();
        ds = db_ER.Exec_DataSet("select Model_Id,Model_Name,category_Id from Mas_Asset_Model where division_code='" + div_code + "' and Model_Active_Flag=0");
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    [WebMethod]
    public static string getAllvendor()
    {
        DB_EReporting db_ER = new DB_EReporting();
        DataSet ds = new DataSet();
        ds = db_ER.Exec_DataSet("select Vendor_Id,Vendor_Name from Mas_Asset_Vendor where Division_Code='" + div_code + "' and Vendor_Active_Flag=0");
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    [WebMethod]
    public static string getAlllocation()
    {
        DB_EReporting db_ER = new DB_EReporting();
        DataSet ds = new DataSet();
        ds = db_ER.Exec_DataSet("select Location_Id,Location_Name from Mas_Asset_Location where Division_Code='" + div_code + "' and Location_Active_Flag=0");
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    [WebMethod]
    public static string SaveAssetRequest(string assetnam, string assetcode, string asstcat, string asstloc, string asststs, string brndnam, string asstmod, string serlnum, string asstcond, string desc, string astvend, string purdate, string invnum, string invdate, string expstartdt, string expenddt, string purval, string purtype, string caprice, string capdate, string valdeprec, string anuldepre, string asstendlife, string astpic_name, string astivnfile)
    {
        DB_EReporting db_ER = new DB_EReporting();
        DataSet ds = new DataSet();
        string msg = string.Empty;
        ds = db_ER.Exec_DataSet("exec sp_save_asset '" + assetnam + "','" + assetcode + "','" + asstcat + "','" + asstloc + "','" + asststs + "','" + brndnam + "','" + asstmod + "','" + serlnum + "','" + asstcond + "','" + desc + "','" + astvend + "','" + purdate + "','" + invnum + "','" + invdate + "','" + expstartdt + "','" + expenddt + "','" + purval + "','" + purtype + "','" + caprice + "','" + capdate + "','" + valdeprec + "','" + anuldepre + "','" + asstendlife + "','" + div_code + "','" + astpic_name + "','" + astivnfile + "'");
        msg = "Success";
        return msg;
    }
    protected void OnLnkUpload_Click(object sender, EventArgs e)
    {
        string filename = string.Empty;
        string contenttype = string.Empty;
        string serverfolder = string.Empty;
        string serverpath = string.Empty;
        filename = Path.GetFileName(FlUploadcsv.PostedFile.FileName);
        string filetype = Path.GetExtension(FlUploadcsv.FileName);
        switch (filetype)
        {
            case ".doc":
            case ".docx":
                contenttype = "application/msword";
                break;
            case ".pdf":
                contenttype = "application/pdf";
                break;

            case ".txt":
                contenttype = "text/plain";
                break;
            case ".mp3":
            case ".wav":
                filetype = "A";
                contenttype = "audio/mpeg";
                break;

            case ".mp4":
            case ".avi":
            case ".wmv":
            case ".flv":
            case ".mpg":
            case ".mpeg":
            case ".mov":
                filetype = "V";
                contenttype = "video/mp4";
                break;

            case ".bmp":
            case ".gif":
            case ".ico":
            case ".jpg":
            case ".jpeg":
            case ".png":
                filetype = "I";
                contenttype = "image/jpeg";
                break;
        }
        serverfolder = Server.MapPath("../Asset_Imags/");

        if (!Directory.Exists(serverfolder))
        {
            Directory.CreateDirectory(serverfolder);
        }
        serverpath = serverfolder + Path.GetFileName(filename);
        FlUploadcsv.SaveAs(serverpath);
    }
}