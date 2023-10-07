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

public partial class MasterFiles_Goods_Received_List : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    public class pro_years
    {
        public string years { get; set; }
    }
    [WebMethod(EnableSession = true)]
    public static pro_years[] Get_Year()
    {
        List<pro_years> product = new List<pro_years>();
        TourPlan tp = new TourPlan();
        DataSet dsTP = null;
        string Div_code = HttpContext.Current.Session["div_code"].ToString();
        dsTP = tp.Get_TP_Edit_Year(Div_code);
        if (dsTP.Tables[0].Rows.Count > 0)
        {
            for (int k = Convert.ToInt16(dsTP.Tables[0].Rows[0]["Year"]); k <= DateTime.Now.Year + 1; k++)
            {
                pro_years pd = new pro_years();
                pd.years = k.ToString();
                product.Add(pd);

            }
        }
        return product.ToArray();
    }

    public class GoodsDetails
    {
        public string GRN_No { get; set; }
        public string GRN_Date { get; set; }
        public string Supp_Code { get; set; }
        public string Supp_Name { get; set; }    

    }

    [WebMethod(EnableSession = true)]
    public static GoodsDetails[] Get_GoodsRecived(string years, string months)
    {       
       
        string Div_code = HttpContext.Current.Session["div_code"].ToString();
        string Sf_Code = HttpContext.Current.Session["Sf_Code"].ToString();        
        DataSet dsDetails = null;
        string exp_mode = string.Empty;

        Product prd = new Product();
        dsDetails = prd.Get_GoodsReceived_List(Div_code, months, years);
        List<GoodsDetails> GDtls = new List<GoodsDetails>();

        foreach (DataRow row in dsDetails.Tables[0].Rows)
        {
            GoodsDetails gd = new GoodsDetails();
            gd.GRN_No = row["GRN_No"].ToString();
            gd.GRN_Date = Convert.ToDateTime(row["GRN_Date"]).ToString("dd/MM/yyyy");
            gd.Supp_Code = row["Supp_Code"].ToString();
            gd.Supp_Name = row["Supp_Name"].ToString();
            GDtls.Add(gd);
        }
        return GDtls.ToArray();
    }
    public class Supplierss
    {
        public string disName { get; set; }
        public string disCode { get; set; }
    }
    [WebMethod(EnableSession = true)]
    public static Supplierss[] GetSupplier()
    {
        string DDiv_code = HttpContext.Current.Session["div_code"].ToString();
        string DSf_Code = HttpContext.Current.Session["Sf_Code"].ToString();
        string DSub_DivCode = string.Empty;
        List<Supplierss> distributor = new List<Supplierss>();
        DataSet dsDistributor = null;
        Stockist stk = new Stockist();
        dsDistributor = stk.GetSuppliers(DDiv_code.TrimEnd(','));
        if (dsDistributor.Tables[0].Rows.Count > 0)
        {
            foreach (DataRow row in dsDistributor.Tables[0].Rows)
            {
                Supplierss dis = new Supplierss();
                dis.disCode = row["S_No"].ToString();
                dis.disName = row["S_Name"].ToString();
                distributor.Add(dis);
            }
        }
        return distributor.ToArray();
    }
}