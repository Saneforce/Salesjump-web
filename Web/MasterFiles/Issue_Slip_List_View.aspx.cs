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

public partial class MasterFiles_Issue_Slip_List_View : System.Web.UI.Page
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
        dsDetails = prd.Get_GoodsReceived_List1(Div_code, months, years);
        List<GoodsDetails> GDtls = new List<GoodsDetails>();

        foreach (DataRow row in dsDetails.Tables[0].Rows)
        {
            GoodsDetails gd = new GoodsDetails();
            gd.GRN_No = row["P_ID"].ToString();
            gd.GRN_Date = Convert.ToDateTime(row["Issue_Dt"]).ToString("dd/MM/yyyy");
            gd.Supp_Code = row["Iss_From"].ToString();
            gd.Supp_Name = row["Iss_From_Name"].ToString();
            GDtls.Add(gd);
        }
        return GDtls.ToArray();
    }
}