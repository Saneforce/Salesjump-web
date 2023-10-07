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

public partial class MasterFiles_Goods_Received_Note_View : System.Web.UI.Page
{
    string Div_Code = string.Empty;
    string SF_Code = string.Empty;
    string Sub_DivCode = string.Empty;
    string mode = string.Empty;
    string grn_no = string.Empty;
    string grn_dt = string.Empty;
    string supp_code = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            mode = Request.QueryString["Mode"].ToString();
            hdnmode.Value = mode;
            if (mode == "1")
            {
                grn_no = Request.QueryString["GRN_No"].ToString();
                hdngrn_no.Value = grn_no;
                grn_dt = Request.QueryString["GRN_Date"].ToString();
                hdngrn_date.Value = grn_dt;
                supp_code = Request.QueryString["Supp_Code"].ToString();
                hdnsupp_code.Value = supp_code;
            }
        }
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

    public class XMLHelper
    {
        public static string Serialize(object dataToSerialize)
        {
            if (dataToSerialize == null) return null;

            using (StringWriter stringwriter = new System.IO.StringWriter())
            {
                var serializer = new XmlSerializer(dataToSerialize.GetType());
                serializer.Serialize(stringwriter, dataToSerialize);
                return stringwriter.ToString();
            }
        }
        public static T Deserialize<T>(string xmlText)
        {
            if (String.IsNullOrWhiteSpace(xmlText)) return default(T);

            using (StringReader stringReader = new System.IO.StringReader(xmlText))
            {
                var serializer = new XmlSerializer(typeof(T));
                return (T)serializer.Deserialize(stringReader);
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
        public string GRN_No { get; set; }
        public string GRN_Date { get; set; }
        public string Entry_Date { get; set; }
        public string Supp_Code { get; set; }
        public string Supp_Name { get; set; }
        public string Po_No { get; set; }
        public string Challan_No { get; set; }
        public string Dispatch_Date { get; set; }
        public string Received_Location { get; set; }
        public string Receved_Name { get; set; }
        public string Received_By { get; set; }
        public string Authorized_By { get; set; }
        public string remarks { get; set; }
        public string goodsTot { get; set; }
        public string taxTot { get; set; }
        public string netTot { get; set; }
    }

    public class Trans_Details
    {
        public string PCode { get; set; }
        public string PDetails { get; set; }
        public string UOM { get; set; }
        public string UOM_Name { get; set; }
        public string Batch_No { get; set; }
        public string POQTY { get; set; }
        public string mfgDate { get; set; }
        public string Price { get; set; }
        public string Good { get; set; }
        public string Damaged { get; set; }
        public string Gross_Value { get; set; }
        public string Net_Value { get; set; }
        public List<Trans_Tax_Details> taxDtls { get; set; }
        public Trans_Details()
        {
            taxDtls = new List<Trans_Tax_Details>(0);
        }
    }
    public class Trans_Tax_Details
    {
        public string Tax_Code { get; set; }
        public string Tax_Name { get; set; }
        public string Tax_Value { get; set; }
    }

    [WebMethod(EnableSession = true)]
    public static string Get_AllValues(string grnNo, string grnDate, string grnSuppcode)
    {
        string Div_Code = HttpContext.Current.Session["div_code"].ToString();
        DateTime dtgrn = DateTime.ParseExact(grnDate, "dd/MM/yyyy", null);
        string strgrn = dtgrn.ToString("MM-dd-yyyy");
        Product prd = new Product();
        MainTransGRN MTG = new MainTransGRN();
        Trans_Head THEA;
        Trans_Details TDET;
        Trans_Tax_Details TTDET;
        string tranId = string.Empty;
        DataSet dsGoods = null;
        dsGoods = prd.Get_GoodsReceived(Div_Code, grnNo, strgrn, grnSuppcode);
        if (dsGoods.Tables[0].Rows.Count > 0)
        {
            DataSet dsDetails = null;
            dsDetails = prd.Get_GoodsReceived_Details(Div_Code, dsGoods.Tables[0].Rows[0][0].ToString());
            DataSet dsTax = null;
            dsTax = prd.Get_GoodsReceived_Tax_Details(Div_Code, dsGoods.Tables[0].Rows[0][0].ToString());


            THEA = new Trans_Head();
            THEA.GRN_No = dsGoods.Tables[0].Rows[0]["GRN_No"].ToString();
            tranId = dsGoods.Tables[0].Rows[0]["Trans_Sl_No"].ToString();
            THEA.GRN_Date = Convert.ToDateTime(dsGoods.Tables[0].Rows[0]["GRN_Date"]).ToString("dd/MM/yyyy");
            THEA.Entry_Date = Convert.ToDateTime(dsGoods.Tables[0].Rows[0]["Entry_Date"]).ToString("dd/MM/yyyy");
            THEA.Supp_Code = dsGoods.Tables[0].Rows[0]["Supp_Code"].ToString();
            THEA.Supp_Name = dsGoods.Tables[0].Rows[0]["Supp_Name"].ToString();
            THEA.Po_No = dsGoods.Tables[0].Rows[0]["Po_No"].ToString();
            THEA.Challan_No = dsGoods.Tables[0].Rows[0]["Challan_No"].ToString();
            THEA.Dispatch_Date = Convert.ToDateTime(dsGoods.Tables[0].Rows[0]["Dispatch_Date"]).ToString("dd/MM/yyyy");
            THEA.Received_Location = dsGoods.Tables[0].Rows[0]["Received_Location"].ToString();
            THEA.Receved_Name = dsGoods.Tables[0].Rows[0]["Receved_Name"].ToString();
            THEA.Received_By = dsGoods.Tables[0].Rows[0]["Received_By"].ToString();
            THEA.Authorized_By = dsGoods.Tables[0].Rows[0]["Authorized_By"].ToString();
            THEA.remarks = dsGoods.Tables[0].Rows[0]["remarks"].ToString();
            THEA.goodsTot = dsGoods.Tables[0].Rows[0]["Net_Tot_Goods"].ToString();
            THEA.taxTot = dsGoods.Tables[0].Rows[0]["Net_Tot_Tax"].ToString();
            THEA.netTot = dsGoods.Tables[0].Rows[0]["Net_Tot_Value"].ToString();
            MTG.TransH.Add(THEA);

            for (int i = 0; i < dsDetails.Tables[0].Rows.Count; i++)
            {
                DataRow drow = dsDetails.Tables[0].Rows[i];
                TDET = new Trans_Details();
                TDET.PCode = drow["PCode"].ToString();
                TDET.PDetails = drow["PDetails"].ToString();
                TDET.UOM = drow["UOM"].ToString();
                TDET.UOM_Name = drow["uom_name"].ToString();
                TDET.Batch_No = drow["Batch_No"].ToString();
                TDET.POQTY = drow["POQTY"].ToString();
                TDET.Price = drow["Price"].ToString();
                TDET.Good = drow["Good"].ToString();
                TDET.Damaged = drow["Damaged"].ToString();
                TDET.Gross_Value = drow["Gross_Value"].ToString();
                TDET.Net_Value = drow["Net_Value"].ToString();
                TDET.mfgDate = Convert.ToDateTime(drow["mfgdate"]).ToString("dd/MM/yyyy");
                MTG.TransD.Add(TDET);
                DataRow[] txtRows = null;
                txtRows = dsTax.Tables[0].Select("Trans_Dtls_Sl_No='" + drow["Trans_Dtls_Sl_No"].ToString() + "'");
                if (txtRows != null)
                {
                    foreach (DataRow trow in txtRows)
                    {
                        TTDET = new Trans_Tax_Details();
                        TTDET.Tax_Code = trow["Tax_Code"].ToString();
                        TTDET.Tax_Name = trow["Tax_Name"].ToString();
                        TTDET.Tax_Value = trow["Tax_Value"].ToString();
                        MTG.TransD[i].taxDtls.Add(TTDET);

                    }
                }
            }
        }
        JSonHelper helper = new JSonHelper();
        String jsonResult = helper.ConvertObjectToJSon(MTG);
        return jsonResult;
    }

}