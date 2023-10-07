using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Bus_EReport;
using System.Data;

using Newtonsoft.Json;
using System.Web.Services;
using System.Globalization;

using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Web.Script.Serialization;
using System.IO;
using System.Text;
using System.Configuration;


public partial class MIS_Reports_rptDailyInventorySummary : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        hsfCode.Value = Request.QueryString["SFCode"].ToString();
        hSubDiv.Value = Request.QueryString["SubDiv"].ToString();
        hFDate.Value = Request.QueryString["FDate"].ToString();
        hTDate.Value = Request.QueryString["TDate"].ToString();
        lblHead.Text = "Daily Inventory Summary From : " + Request.QueryString["FDate"].ToString() + " To " + Request.QueryString["TDate"].ToString();

        lblsfname.Text = "Team : <span style='font-weight: bold;'> " + Request.QueryString["SFName"].ToString() + "</span>";
    }
    public class Item
    {
        public string product_id { get; set; }
        public string product_name { get; set; }

    }
    public class user
    {
        public string sf_code { get; set; }
        public string sf_name { get; set; }
    }
    public class OrderData
    {
        public string sfCode { get; set; }
        public string pCode { get; set; }
        public string OrderQty { get; set; }
    }

    public class vanOrder
    {
        public string sfCode { get; set; }
        public string pCode { get; set; }
        public string OrderQty { get; set; }
    }

    public class totOrders
    {
        public List<vanOrder> van = new List<vanOrder>();
       public  List<OrderData> sal = new List<OrderData>();
        
    }


    [WebMethod(EnableSession = true)]
    public static Item[] getProduct(string SubDiv)
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

        Product pro = new Product();
        List<Item> empList = new List<Item>();
        DataSet dsAccessmas = pro.getproductname(div_code.TrimEnd(','), SubDiv);
        foreach (DataRow row in dsAccessmas.Tables[0].Rows)
        {
            Item emp = new Item();
            emp.product_id = row["Product_Detail_Code"].ToString();
            emp.product_name = row["Product_Short_Name"].ToString();
            empList.Add(emp);
        }
        return empList.ToArray();
    }
    [WebMethod(EnableSession = true)]
    public static user[] getfo(string sfCode, string SubDiv)
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
        SalesForce sf = new SalesForce();
        List<user> empList = new List<user>();
        DataSet dsAccessmas = sf.UserList_getMR(div_code.TrimEnd(','), sfCode, SubDiv);
        foreach (DataRow row in dsAccessmas.Tables[0].Rows)
        {
            user emp = new user();
            emp.sf_code = row["sf_code"].ToString();
            emp.sf_name = row["sf_name"].ToString();
            empList.Add(emp);
        }
        return empList.ToArray();
    }

    [WebMethod(EnableSession = true)]
    public static string getInventorySummary(string sfCode, string SubDiv, string fDate, string tDate)
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



        string fDt = DateTime.ParseExact(fDate, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
        string tDt = DateTime.ParseExact(tDate, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");

        totOrders tOrder = new totOrders();

     


        Order od = new Order();       
        DataSet dsOrder = od.GetInventorySummary(div_code.TrimEnd(','), sfCode, fDt, tDt, SubDiv);
        foreach (DataRow row in dsOrder.Tables[0].Rows)
        {
            vanOrder  Ord = new vanOrder();
            Ord.sfCode = row["Sf_Code"].ToString();
            Ord.pCode = row["Prod_Code"].ToString();
            Ord.OrderQty = row["PiceQty"].ToString();
            tOrder.van.Add(Ord);
        }
        foreach (DataRow row in dsOrder.Tables[1].Rows)
        {
            OrderData Ord = new OrderData();
            Ord.sfCode = row["Sf_Code"].ToString();
            Ord.pCode = row["Prod_Code"].ToString();
            Ord.OrderQty = row["PiceQty"].ToString();
            tOrder.sal.Add(Ord);
        }

        JSonHelper helper = new JSonHelper();
        String jsonResult = helper.ConvertObjectToJSon(tOrder);
        return jsonResult;
    }
    public class JSonHelper
    {
        public string ConvertObjectToJSon<T>(T obj)
        {
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
            MemoryStream ms = new MemoryStream();
            ser.WriteObject(ms, obj);
            string jsonString = Encoding.UTF8.GetString(ms.ToArray());
            ms.Close();
            return jsonString;

        }
        public T ConverJSonToObject<T>(string jsonString)
        {
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T));
            MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonString));
            T obj = (T)serializer.ReadObject(ms);
            return obj;
        }
    }
}