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
public partial class MasterFiles_Stock_Summary : System.Web.UI.Page
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
        public string Dist_Code { get; set; }
        public string GStock { get; set; }
        public string DStock { get; set; }
        public string BatchNo { get; set; }
    }


    [WebMethod(EnableSession = true)]
    public static Products[] GetProduct()
    {
        string PDiv_code = HttpContext.Current.Session["div_code"].ToString();
        List<Products> product = new List<Products>();
        DataSet dsProduct = null;
        Product p = new Product();

        dsProduct = p.getCurrentStock_withProduct(PDiv_code.TrimEnd(','));
        if (dsProduct != null)
        {
            foreach (DataRow row in dsProduct.Tables[0].Rows)
            {
                Products pro = new Products();
                pro.pName = row["Product_Detail_Name"].ToString();
                pro.pCode = row["Prod_Code"].ToString();
                pro.Dist_Code = row["Dist_Code"].ToString();
                pro.GStock = row["GStock"].ToString();
                pro.DStock = row["DStock"].ToString();
				pro.BatchNo = row["BatchNo"].ToString();
                product.Add(pro);
            }
        }
        return product.ToArray();
    }
}