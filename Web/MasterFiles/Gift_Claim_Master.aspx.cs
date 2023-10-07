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

public partial class MasterFiles_Gift_Claim_Master : System.Web.UI.Page
{
	string sf_type = string.Empty;
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

    }

    [WebMethod(EnableSession = true)]
    public static string getRetailBusiness(string divcode)
    {
        Sales ast = new Sales();
        DataSet ds = ast.getAllRetailBusiness(divcode);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    [WebMethod(EnableSession = true)]
    public static string getGiftDets(string divcode)
    {
        Sales ast = new Sales();
        DataSet ds = ast.getAllGift(divcode);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    [WebMethod(EnableSession = true)]
    public static string saveRetailBusiness(string data)
    {
        string msg = string.Empty;
        Bus_EReport.Sales.SaveRetailBusiness Data = JsonConvert.DeserializeObject<Bus_EReport.Sales.SaveRetailBusiness>(data);
        Sales dsm = new Sales();
        msg = dsm.saveRetailBusiness(Data);
        return msg;
    }
    [WebMethod(EnableSession = true)]
    public static string saveGift(string data)
    {
        string msg = string.Empty;
        Bus_EReport.Sales.SaveGiftSlab Data = JsonConvert.DeserializeObject<Bus_EReport.Sales.SaveGiftSlab>(data);
        Sales dsm = new Sales();
        msg = dsm.saveGift(Data);
        return msg;
    }

    [WebMethod(EnableSession = true)]
    public static string getRetailBusinessID(string divcode)
    {
        Sales cp = new Sales();
        DataSet ds = cp.getRetailBusinessID(divcode);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }

    [WebMethod(EnableSession = true)]
    public static string getGiftID(string divcode)
    {
        Sales cp = new Sales();
        DataSet ds = cp.getGiftID(divcode);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }

    [WebMethod(EnableSession = true)]
    public static string getRetailUpdt(string divcode, string scode)
    {
        Sales cp = new Sales();
        DataSet ds = cp.getRetailBSlab(scode, divcode);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }

    [WebMethod(EnableSession = true)]
    public static string getGiftUpdt(string divcode, string scode)
    {
        Sales cp = new Sales();
        DataSet ds = cp.getGiftSlab(scode, divcode);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    [WebMethod(EnableSession = true)]
    public static int SetNewStatus_Retail(string SF, string stus)
    {
        Sales ast = new Sales();
        int iReturn = ast.Retail_DeActivate(SF, stus);
        return iReturn;
    }
    [WebMethod(EnableSession = true)]
    public static int SetNewStatus_Gift(string SF, string stus)
    {
        Sales ast = new Sales();
        int iReturn = ast.Gift_DeActivate(SF, stus);
        return iReturn;
    }
    [WebMethod(EnableSession = true)]
    public static string getAllGiftProducts(string divcode)
    {
        Sales ast = new Sales();
        DataSet ds = ast.getAllGiftsProd(divcode);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    public class Products
    {
        public string label { get; set; }
        public string value { get; set; }
        public string id { get; set; }

    }
    [WebMethod(EnableSession = true)]
    public static Products[] GetDesgnName()
    {
        string div_code = "";
        div_code = HttpContext.Current.Session["div_code"].ToString();
        List<Products> HDay = new List<Products>();
        DataSet ds = null;
        Product sm = new Product();

        ds = sm.getProd(div_code.TrimEnd(','));

        foreach (DataRow row in ds.Tables[0].Rows)
        {
            Products d = new Products();
            d.label = row["Product_Detail_Name"].ToString();
            d.value = row["Product_Detail_Name"].ToString();
            d.id = row["Product_Detail_Code"].ToString();
            HDay.Add(d);
        }
        return HDay.ToArray();
    }
    [WebMethod(EnableSession = true)]
    public static string getGiftProdUpdt(string divcode, string scode)
    {
        Sales cp = new Sales();
        DataSet ds = cp.getGiftProducts(scode, divcode);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    [WebMethod(EnableSession = true)]
    public static string saveGiftProduct(string data)
    {
        string msg = string.Empty;
        Bus_EReport.Sales.SaveGiftProducts Data = JsonConvert.DeserializeObject<Bus_EReport.Sales.SaveGiftProducts>(data);
        Sales dsm = new Sales();
        msg = dsm.saveGift_Products(Data);
        return msg;
    }

    [WebMethod(EnableSession = true)]
    public static string getGiftProductsID(string divcode)
    {
        Sales cp = new Sales();
        DataSet ds = cp.getGiftProdID(divcode);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
	[WebMethod(EnableSession = true)]
    public static string getsHQ(string divcode)
    {
        Sales dv = new Sales();
        string div_code = HttpContext.Current.Session["div_code"].ToString();
        DataSet dsProd = dv.giftslabhq(divcode);
        return JsonConvert.SerializeObject(dsProd.Tables[0]);
    }
}