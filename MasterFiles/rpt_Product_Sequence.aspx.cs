using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;



using System.IO;
using System.Data;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html;
using iTextSharp.text.html.simpleparser;
using System.Text;
using Bus_EReport;
using System.Net;
using iTextSharp.tool.xml;

using System.Web.Services;
using System.Globalization;
using Newtonsoft.Json;

public partial class MasterFiles_rpt_Product_Sequence : System.Web.UI.Page
{
    #region "Declaration"
   
    string divcode = string.Empty;
    string subDivCode = string.Empty;
    string subDivName = string.Empty;
    decimal iTotLstCount1 = 0;
    decimal iTotLstCount = 0;
    decimal iTotLstCount2 = 0;
    int iMonth = -1;
    int iYear = -1;
    DataSet dsDoctor = null;
   
    string Prod_Name = string.Empty;
    

    DataSet dsSalesForce = new DataSet();

    DataSet dsMGR = new DataSet();
    DataSet dsMr = new DataSet();
    DataSet dsDoc = null;
    DateTime dtCurrent;

    DataSet dsprd = new DataSet();
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        divcode = Session["div_code"].ToString();
        subDivCode = Request.QueryString["subdiv_Code"].ToString();
        subDivName = Request.QueryString["subdiv_Name"].ToString();
        subdiv_code.Value = subDivCode;

        lblyear.Text = "Product Sequence for "+subDivName;
    }
    public class Category
    {
        public string catName { get; set; }
        public string catCode { get; set; }

    }
    [WebMethod(EnableSession = true)]
    public static Category[] getCategory(string subdivcode)
    {
        string div_code = "";
        try
        {
            div_code = HttpContext.Current.Session["div_code"].ToString();
        }
        catch
        {
            div_code = HttpContext.Current.Session["Division_Code"].ToString();
        }
        RoutePlan rop = new RoutePlan();
        Product pro = new Product();
        DataSet dsProCat = pro.getProductBrands(div_code, subdivcode);
        List<Category> vList = new List<Category>();
        foreach (DataRow row in dsProCat.Tables[0].Rows)
        {
            Category vl = new Category();
            vl.catName = row["Product_Brd_Name"].ToString();
            vl.catCode = row["Product_Brd_Code"].ToString();
            vList.Add(vl);
        }
        return vList.ToArray();
    }

    [WebMethod(EnableSession = true)]
    public static ProductSequence[] getProductSequence(string subdivcode)
    {
        string div_code = "";
        try
        {
            div_code = HttpContext.Current.Session["div_code"].ToString();
        }
        catch
        {
            div_code = HttpContext.Current.Session["Division_Code"].ToString();
        }
        ListedDR ldr = new ListedDR();
        Product pro = new Product();
        DataSet dsPro = pro.getProCat(div_code,subdivcode);

        List<ProductSequence> vList = new List<ProductSequence>();
        foreach (DataRow row in dsPro.Tables[0].Rows)
        {
            ProductSequence vl = new ProductSequence();
            vl.pCode = row["Product_Detail_Code"].ToString();
            vl.pName = row["Product_Detail_Name"].ToString();
            vl.cCode = row["Product_Brd_Code"].ToString();
            vl.sequenceNo = row["Prod_Detail_Sl_No"].ToString();
            vl.printsequence = row["Product_Code_SlNo"].ToString();
            vList.Add(vl);
        }
        return vList.ToArray();
    }

    public class ProductSequence
    {
        public string pCode { get; set; }
        public string pName { get; set; }
        public string cCode { get; set; }
        public string sequenceNo { get; set; }
        public string printsequence { get; set; }

    }

    [WebMethod]
    public static string SaveSequence(string Data)
    {
        string div_code = "";
        try
        {
            div_code = HttpContext.Current.Session["div_code"].ToString();
        }
        catch
        {
            div_code = HttpContext.Current.Session["Division_Code"].ToString();
        }
        var items = JsonConvert.DeserializeObject<List<ProductSequence>>(Data);
        int count = 0;
        ListedDR drd = new ListedDR();
        Product pro = new Product();
        int iReturn = -1;
        for (int i = 0; i < items.Count; i++)
        {
            iReturn = pro.updateSequence(div_code,items[i].pCode.ToString(), items[i].sequenceNo.ToString(), items[i].printsequence.ToString());
            count++;
        }
        if (count == 0)
        {
            return "Fail";
        }
        else
        {
            return "Suucess";
        }

        //return items.Count.ToString();
    }
}