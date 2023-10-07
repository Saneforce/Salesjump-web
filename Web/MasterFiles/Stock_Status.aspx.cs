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


public partial class MasterFiles_Stock_Status : System.Web.UI.Page
{
    string sf_type = string.Empty;
    protected void Page_PreInit(object sender, EventArgs e)
    {
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
    public class Distributor
    {
        public string disName { get; set; }
        public string disCode { get; set; }
    }
    [WebMethod(EnableSession = true)]
    public static Distributor[] GetDistributor()
    {
        string DDiv_code = HttpContext.Current.Session["div_code"].ToString();
        string DSf_Code = HttpContext.Current.Session["Sf_Code"].ToString();
        string DSub_DivCode = "0";
        List<Distributor> distributor = new List<Distributor>();
        DataSet dsDistributor = null;
        Stockist stk = new Stockist();
        dsDistributor = stk.GetStockist_subdivisionwise(divcode: DDiv_code.TrimEnd(','), subdivision: DSub_DivCode, sf_code: DSf_Code);
        if (dsDistributor.Tables[0].Rows.Count > 0)
        {
            foreach (DataRow row in dsDistributor.Tables[0].Rows)
            {
                Distributor dis = new Distributor();
                dis.disCode = row["Stockist_code"].ToString();
                dis.disName = row["Stockist_Name"].ToString();
                distributor.Add(dis);
            }
        }
        return distributor.ToArray();
    }


    public class Products
    {
        public string pName { get; set; }
        public string pCode { get; set; }
        public string pUOM { get; set; }
        public string pUOM_Name { get; set; }
        public string opning { get; set; }
        public string ppurch { get; set; }
        public string preturn { get; set; }
        public string totadd { get; set; }
        public string psal { get; set; }
        public string piss { get; set; }
        public string totdet { get; set; }
        public string closing { get; set; }
        public string pval { get; set; }

        public string trsIn { get; set; }
        public string trsOut { get; set; }
        public string adjIn { get; set; }
        public string adjOut { get; set; }

    }


    [WebMethod(EnableSession = true)]
    public static Products[] GetProduct(string Cust_Code,string FromDate,string ToDate)
    {
        string PDiv_code = HttpContext.Current.Session["div_code"].ToString();
        List<Products> product = new List<Products>();
        DataSet dsProduct = null;
        DataSet dsOpning = null;
        DataSet dsPurRet = null;
        Product p = new Product();


        DateTime dtfrom = DateTime.ParseExact(FromDate, "dd/MM/yyyy", null);
        string strfrom = dtfrom.ToString("yyyy-MM-dd");


        DateTime dtto = DateTime.ParseExact(ToDate, "dd/MM/yyyy", null);
        string strto = dtto.ToString("yyyy-MM-dd");



        dsProduct = p.getproductname(PDiv_code.TrimEnd(','));
        dsOpning = p.Get_Opning_Ledger(PDiv_code.TrimEnd(','), Cust_Code, strfrom, strto);
        dsPurRet = p.Get_Ledger_PurRet(PDiv_code.TrimEnd(','), Cust_Code, strfrom, strto);





        if (dsProduct != null)
        {
            foreach (DataRow row in dsProduct.Tables[0].Rows)
            {
                Products pro = new Products();
                pro.pName = row["Product_Detail_Name"].ToString();
                pro.pCode = row["Product_Detail_Code"].ToString();
                pro.pUOM = row["Unit_code"].ToString();
                pro.pUOM_Name = row["Move_MailFolder_Name"].ToString();
                decimal opn = 0; 
                foreach (DataRow opR in dsOpning.Tables[0].Rows)
                {
                    if (row["Product_Detail_Code"].ToString() == opR["prod_code"].ToString())
                    {
                        pro.opning =  opR["tot"].ToString();
                        opn += opR["tot"] == DBNull.Value ? 0 : Convert.ToDecimal(opR["tot"].ToString());
                    }
                }

                decimal pur =0; 
                decimal ret =0;
                decimal iss =0;
                decimal sal = 0;
				decimal trsin = 0;
                decimal trsout = 0;
                decimal adjin = 0;
                decimal adjout = 0;

                foreach (DataRow opP in dsPurRet.Tables[0].Rows)
                {
                    if (row["Product_Detail_Code"].ToString() == opP["prod_code"].ToString())
                    {
                        if (opP["ENTRYbY"].ToString() == "GRN")
                        {
                            pro.ppurch = opP["Goods"].ToString();
                            pur += opP["Goods"] == DBNull.Value ? 0 : Convert.ToDecimal(opP["Goods"].ToString());
                        }

                        if (opP["ENTRYbY"].ToString() == "RET")
                        {
                           
                            ret += opP["Goods"] == DBNull.Value ? 0 : Convert.ToDecimal(opP["Goods"].ToString());
                        }
                        if (opP["ENTRYbY"].ToString() == "DayEndInv")
                        {                           
                            ret += opP["Goods"] == DBNull.Value ? 0 : Convert.ToDecimal(opP["Goods"].ToString());
                        }

                        if (opP["ENTRYbY"].ToString() == "adj")
                        {
                            if (opP["calType"].ToString() == "1")
                            {
                                ret += opP["Goods"] == DBNull.Value ? 0 : Convert.ToDecimal(opP["Goods"].ToString());
                            }
                        }
                        pro.preturn = ret.ToString("0.00");

                        if (opP["ENTRYbY"].ToString() == "DayInv")
                        {
                            pro.psal = opP["Goods"].ToString();
                            sal += opP["Goods"] == DBNull.Value ? 0 : Convert.ToDecimal(opP["Goods"].ToString());
                        }


                        if (opP["ENTRYbY"].ToString() == "TRS")
                        {
                            if (opP["calType"].ToString() == "0")
                            {
								pro.trsOut = opP["Goods"].ToString(); 
                                trsout += opP["Goods"] == DBNull.Value ? 0 : Convert.ToDecimal(opP["Goods"].ToString());
                            }
                            else
                            {
								pro.trsIn = opP["Goods"].ToString(); 		
                                trsin += opP["Goods"] == DBNull.Value ? 0 : Convert.ToDecimal(opP["Goods"].ToString());
                            }
                        }

                        if (opP["ENTRYbY"].ToString() == "ISS")
                        {
                            iss += opP["Goods"] == DBNull.Value ? 0 : Convert.ToDecimal(opP["Goods"].ToString());
                        }

                        if (opP["ENTRYbY"].ToString() == "adj")
                        {
                            if (opP["calType"].ToString() == "0")
                            {
								pro.adjOut =  opP["Goods"].ToString();
                                adjout += opP["Goods"] == DBNull.Value ? 0 : Convert.ToDecimal(opP["Goods"].ToString());
                            }
							else
                            {
								pro.adjIn =opP["Goods"].ToString();
                                adjin += opP["Goods"] == DBNull.Value ? 0 : Convert.ToDecimal(opP["Goods"].ToString());
                            }
                        }

                        pro.piss = iss.ToString("0.00");
                        pro.totadd = (opn+pur + ret + trsin + adjin).ToString("0.00");
                        pro.totdet = (sal + iss +trsout +adjout).ToString("0.00");

                        pro.closing = ((opn + pur + ret + trsin + adjin) - (sal + iss + trsout + adjout)).ToString("0.00");
                    }
                }

                product.Add(pro);
            }
        }
        return product.ToArray();
    }
}