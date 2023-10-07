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
using Amazon.S3.IO;
using Amazon.S3;
using Amazon.S3.Transfer;
using System.Net;
using DBase_EReport;

public partial class MasterFiles_Reports_viewaldate_dcr : System.Web.UI.Page
{
    public static string sf_type = string.Empty;
    public static string sfcode = string.Empty;
    public static string sfname = string.Empty;
    public static string subdiv = string.Empty;
    public static string FDTs = string.Empty;
    public static string TDTs = string.Empty;
    public static string FDT = string.Empty;
    public static string TDT = string.Empty;
    public static string BrandCode = string.Empty;
    public string fdate = string.Empty;
    public string tdate = string.Empty;
    public DateTime rdt;
    public DateTime sdt;
	static string strsound = string.Empty;
    static string mp3 = string.Empty;
    string serverpath = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        sfcode = Request.QueryString["sf_code"].ToString();
        sfname = Request.QueryString["Sf_Name"].ToString();
        //BrandCode = Request.QueryString["state"].ToString();
        FDTs = Request.QueryString["fdate"].ToString();
        TDTs = Request.QueryString["tdate"].ToString();
        //subdiv = Request.QueryString["subdiv"].ToString();

        DateTime result4 = DateTime.ParseExact(FDTs, "d/MM/yyyy", CultureInfo.InvariantCulture);
        FDT = result4.ToString("yyyy-MM-dd");

        DateTime result10 = DateTime.ParseExact(TDTs, "d/MM/yyyy", CultureInfo.InvariantCulture);
        TDT = result10.ToString("yyyy-MM-dd");
        rdt = Convert.ToDateTime(FDTs);
        sdt = Convert.ToDateTime(TDTs);

        Label1.Text = "Daily Call Report from " + rdt.ToString("dd/MM/yy") + " to " + sdt.ToString("dd/MM/yy");
        Label2.Text = "FieldForce Name: " + sfname;
    }
	static WMPLib.WindowsMediaPlayer wplayer = new WMPLib.WindowsMediaPlayer();
    private static string serverfolder;
    [WebMethod]
    public static string getBrandwiseSales(string Div)
    {
        Product SFD = new Product();
        DataSet ds = SFD.dcrgetAllBrd_Qty(Div, sfcode, FDT, TDT, subdiv, BrandCode);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    [WebMethod]
    public static string getBrandwiseSalesUsr(string Div)
    {
        Product SFD = new Product();
        DataSet ds = SFD.dcrgetAllBrd_USR(Div, sfcode, FDT, TDT, subdiv, BrandCode);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    [WebMethod]
    public static string getProductBrandlist(string Div)
    {
        Product SFD = new Product();
        DataSet ds = SFD.dcrgetProductBrandlist_DataTable(Div, sfcode, FDT, TDT, subdiv, BrandCode);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    [WebMethod]
    public static string primgetBrandwiseSales(string Div)
    {
        Product SFD = new Product();
        DataSet ds = SFD.pridcrgetAllBrd_Qty(Div, sfcode, FDT, TDT, subdiv, BrandCode);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    [WebMethod]
    public static string primgetBrandwiseSalesUsr(string Div)
    {
        Product SFD = new Product();
        DataSet ds = SFD.pridcrgetAllBrd_USR(Div, sfcode, FDT, TDT, subdiv, BrandCode);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    [WebMethod]
    public static string primgetProductBrandlist(string Div)
    {
        Product SFD = new Product();
        DataSet ds = SFD.primdcrgetProductBrandlist_DataTable(Div, sfcode, FDT, TDT, subdiv, BrandCode);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }     
    
    [WebMethod]
    public static string getRemarksSF(string Div)
    {
        DataSet ds = null;
        string strQry = string.Empty;        

        ds = getAllSFRemark(Div, sfcode, FDT, TDT, subdiv, BrandCode);

        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    public static DataSet getAllSFRemark(string divcode, string sfcode, string FDT, string TDT, string subdiv = "0", string StateCode = "0")
    {
        string strQry = string.Empty;
        DataSet dsRemark = null;

        DBase_EReport.DB_EReporting db_ER = new DBase_EReport.DB_EReporting();
        
        strQry = "exec getRemarksAllDCRdtl_odate '" + sfcode + "','" + divcode + "','" + FDT + "','" + TDT + "','" + subdiv + "','" + StateCode + "'";

        try
        {
            dsRemark = db_ER.Exec_DataSet(strQry);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return dsRemark;
    }
    [WebMethod]
    public static string getdisthuntdet(string Div)
    {
        DataSet ds = null;
        string strQry = string.Empty;

        ds = getdisthuntdetail(Div, sfcode, FDT, TDT,BrandCode);

        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    public static DataSet getdisthuntdetail(string divcode, string sfcode, string FDT, string TDT, string StateCode = "0")
    {
        string strQry = string.Empty;
        DataSet dsRemark = null;

        DBase_EReport.DB_EReporting db_ER = new DBase_EReport.DB_EReporting();

        strQry = "exec Distributor_huntdet '" + sfcode + "','" + divcode + "','" + FDT + "','" + TDT + "','" + StateCode + "'";

        try
        {
            dsRemark = db_ER.Exec_DataSet(strQry);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return dsRemark;
    }
    [WebMethod]
    public static string supergetSalesusr(string Div)
    {
        Product SFD = new Product();
        DataSet ds = superdcrgetAllBrd_Qty(Div, sfcode, FDT, TDT, subdiv, BrandCode);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    public static DataSet superdcrgetAllBrd_Qty(string divcode, string sfcode, string FDT, string TDT, string subdiv = "", string StateCode = "")
    {
        string strQry = string.Empty;
        DataSet dsRemark = null;

        DBase_EReport.DB_EReporting db_ER = new DBase_EReport.DB_EReporting();

        strQry = "exec getSupstokAllDCRdtl_date '" + sfcode + "','" + divcode + "','" + FDT + "','" + TDT + "','" + subdiv + "','" + StateCode + "'";

        try
        {
            dsRemark = db_ER.Exec_DataSet(strQry);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return dsRemark;
    }
    [WebMethod]
    public static string supergetproductwise(string Div)
    {
        Product SFD = new Product();
        DataSet ds = supergetproductwisesales(Div, sfcode, FDT, TDT, subdiv, BrandCode);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    public static DataSet supergetproductwisesales(string divcode, string sfcode, string FDT, string TDT, string subdiv = "", string StateCode = "")
    {
        string strQry = string.Empty;
        DataSet dsRemark = null;

        DBase_EReport.DB_EReporting db_ER = new DBase_EReport.DB_EReporting();

        strQry = "exec getSuperstockAllDCRdtl_odate '" + sfcode + "','" + divcode + "','" + FDT + "','" + TDT + "','" + subdiv + "','" + StateCode + "'";

        try
        {
            dsRemark = db_ER.Exec_DataSet(strQry);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return dsRemark;
    }
	    [WebMethod]
    public static string GetAudiofile(string div_code, string FileName)
    {
        DB_EReporting db = new DB_EReporting();        
        DataSet ds = db.Exec_DataSet("Select Url_Short_Name+'_'+'Audio' foldername from Mas_Division where Division_Code='" + div_code + "'  ");
        string Folder= ds.Tables[0].Rows[0]["foldername"].ToString();
        string bucketName = "happic";
        string folderName = Folder;
        string fileName = FileName;
        MasterFiles_Reports_viewaldate_dcr playa = new MasterFiles_Reports_viewaldate_dcr();
        string msg =playa.downloadaudio( bucketName,folderName,fileName);
        return msg;
    }
    public string downloadaudio(string bucketName,string folderName,string fileName)
    {        
        string accessKey = "AKIA5OS74MUCASG7HSCG", accessSecret = "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";
        AmazonS3Client client = new AmazonS3Client(accessKey, accessSecret, Amazon.RegionEndpoint.APSouth1);

        
        var transferUtility = new TransferUtility(client);
        string objectKey = folderName + "/" + fileName;
        
        string localFilePaths = Server.MapPath(@"AudFiles\" + fileName);
        //string localFilePaths = "http://fmcg.sanfmcg.com/MasterFiles/Reports/AudFiles/MR4126_1694754881446.mp3";
        
        string msg = "";
        try
        {
          
            transferUtility.Download(localFilePaths, bucketName, objectKey);

            msg = "File downloaded locally on the server successfully.";
  

        }
        catch (AmazonS3Exception ex)
        {
            msg = "S3 Error:: " + ex.Message.ToString() + "";
            //Console.WriteLine("S3 Error: {ex.Message}");
        }
        catch (WebException ex)
        {
            msg = "Web Error:: " + ex.Message.ToString() + "";
            // Console.WriteLine("Web Error: {ex.Message}");
        }
        catch (Exception ex)
        {
            msg = "Error:: " + ex.Message.ToString() + "";
            //Console.WriteLine("Error: {ex.Message}");
        }
        return msg;
       
    }

    [WebMethod]
    public static string Pausefile(string sound)
    {
        MasterFiles_Reports_viewaldate_dcr stopaud = new MasterFiles_Reports_viewaldate_dcr();
        stopaud.Pauseaudio(sound);
        string msg = string.Empty;  msg = "Success";      
        return msg;
    }
    public string Pauseaudio(string sound)
    {
        string imagepath = Server.MapPath(@"AudFiles\" + sound);
        string msg = string.Empty;
        wplayer.close();
        File.Delete(imagepath);
        msg = "Success";
        return msg;
    }
   
    
}