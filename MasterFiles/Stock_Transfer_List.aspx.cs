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
public partial class MasterFiles_Stock_Transfer_List : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public class GoodsDetails
    {
        public string Transfer_No { get; set; }
        public string Transfer_Date { get; set; }
        public string Trans_SlNo { get; set; }
        public string Transfer_From { get; set; }
        public string Transfer_To { get; set; }
        public string Transfer_From_Nm { get; set; }
        public string Transfer_To_Nm { get; set; }


    }

    [WebMethod(EnableSession = true)]
    public static GoodsDetails[] Get_Stock_Transfer_List(string years, string months)
    {

        string Div_code = HttpContext.Current.Session["div_code"].ToString();
        string Sf_Code = HttpContext.Current.Session["Sf_Code"].ToString();
        DataSet dsDetails = null;
        string exp_mode = string.Empty;

        Product prd = new Product();
        dsDetails = prd.Get_Stock_Transfer_Head_List(Div_code, years, months);
        List<GoodsDetails> GDtls = new List<GoodsDetails>();

        foreach (DataRow row in dsDetails.Tables[0].Rows)
        {
            GoodsDetails gd = new GoodsDetails();
            gd.Trans_SlNo = row["Trans_SlNo"].ToString();
            gd.Transfer_No = row["Transfer_No"].ToString();
            gd.Transfer_Date = Convert.ToDateTime(row["Transfer_Date"]).ToString("dd/MM/yyyy");
            gd.Transfer_From = row["Transfer_From"].ToString();
            gd.Transfer_From_Nm = row["Transfer_From_Nm"].ToString();
            gd.Transfer_To = row["Transfer_To"].ToString();
            gd.Transfer_To_Nm = row["Transfer_To_Nm"].ToString();
            GDtls.Add(gd);
        }
        return GDtls.ToArray();
    }
    public class Distributor
    {
        public string disName { get; set; }
        public string disCode { get; set; }
        public string wType { get; set; }
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
                dis.wType = row["type"].ToString();
                distributor.Add(dis);
            }
        }
        return distributor.ToArray();
    }

}