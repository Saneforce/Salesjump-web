using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;



using System.Data;
using Bus_EReport;

using System.Web.Services;
using System.Data.SqlClient;
using Newtonsoft.Json;

using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Web.Script.Serialization;
using System.IO;
using System.Text;
using System.Configuration;
using System.Globalization;
using System.Transactions;

using System.Xml.Serialization;
using System.Xml.Schema;
using System.Xml;

public partial class MasterFiles_Stock_Issue_View : System.Web.UI.Page
{

    string mode = string.Empty;
    string grn_no = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            mode = Request.QueryString["Mode"].ToString();
            hdnmode.Value = mode;
            if (mode == "1")
            {
                grn_no = Request.QueryString["Code"].ToString();
                hdntransno.Value = grn_no;
            }
        }
    }


    public class MainTransGRN
    {
        public List<Trans_Head> TransH = new List<Trans_Head>();
        public List<Trans_Details> TransD = new List<Trans_Details>();
    }


    public class Trans_Head
    {
        public string transNo { get; set; }
        public string transDate { get; set; }
        public string stockistFrom { get; set; }
        public string stockistFrom_Nm { get; set; }
        public string stockistTo { get; set; }
        public string stockistTo_Nm { get; set; }
    }

    public class Trans_Details
    {
        public string pCode { get; set; }
        public string pName { get; set; }
        public string pType { get; set; }
        public string pType_Name { get; set; }
        public string prate { get; set; }
        public string pqty { get; set; }
        public string pval { get; set; }
        public string preason { get; set; }

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


    [WebMethod(EnableSession = true)]
    public static string Get_AllValues(string TransSlNo)
    {
        string Div_Code = HttpContext.Current.Session["div_code"].ToString();
        Product prd = new Product();
        MainTransGRN MTG = new MainTransGRN();
        Trans_Head THEA;
        Trans_Details TDET;

        string tranId = string.Empty;
        DataSet dsGoods = null;
        dsGoods = prd.Get_Stock_issue_HeadVal(TransSlNo);
        if (dsGoods.Tables[0].Rows.Count > 0)
        {
            DataSet dsDetails = null;
            dsDetails = prd.Get_Stock_issue_DetailsVal(TransSlNo);



            THEA = new Trans_Head();
            THEA.transNo = dsGoods.Tables[0].Rows[0]["P_ID"].ToString();
            //THEA.transNo  = dsGoods.Tables[0].Rows[0]["Iss_Eno"].ToString();
            THEA.transDate = Convert.ToDateTime(dsGoods.Tables[0].Rows[0]["Issue_Dt"]).ToString("dd/MM/yyyy");
            THEA.stockistFrom = dsGoods.Tables[0].Rows[0]["Iss_From"].ToString();
            THEA.stockistFrom_Nm = dsGoods.Tables[0].Rows[0]["Iss_From_Name"].ToString();
            THEA.stockistTo = dsGoods.Tables[0].Rows[0]["Iss_To"].ToString();
            THEA.stockistTo_Nm = dsGoods.Tables[0].Rows[0]["Iss_To_Name"].ToString();
            MTG.TransH.Add(THEA);

            for (int i = 0; i < dsDetails.Tables[0].Rows.Count; i++)
            {
                DataRow drow = dsDetails.Tables[0].Rows[i];
                TDET = new Trans_Details();
                TDET.pCode = drow["Prod_Code"].ToString();
                TDET.pName = drow["Prod_Name"].ToString();
                TDET.pType = drow["Stock_Type"].ToString();
                TDET.prate = drow["Rate"].ToString();
                TDET.pqty = drow["QTY"].ToString();
                TDET.pval = drow["Value"].ToString();
                TDET.preason = drow["Reason"].ToString();
                MTG.TransD.Add(TDET);
            }
        }
        JSonHelper helper = new JSonHelper();
        String jsonResult = helper.ConvertObjectToJSon(MTG);
        return jsonResult;
    }
}