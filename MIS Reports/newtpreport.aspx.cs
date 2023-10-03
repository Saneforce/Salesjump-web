using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using Bus_EReport;
using DocumentFormat.OpenXml.Drawing;
using System.IO;
using System.Data.SqlClient;
using Newtonsoft.Json;
using System.Web.Services;


public partial class MIS_Reports_newtpreport : System.Web.UI.Page
{
    DataTable dsProd1 = null;
    DataSet dsProd = null;
    DataSet dsProduct = null;
    DataSet dsDivision = null;
    DataSet dsState = null;
    string divcode = string.Empty;
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
        divcode = Session["div_code"].ToString();
        if (!Page.IsPostBack)
        {
            GetMR();

        }
    }
    public class sfMR
    {
        public string sfname { get; set; }
        public string sfcode { get; set; }
    }
    public class dist
    {
        public string disname { get; set; }
        public string discode { get; set; }
    }
    [WebMethod(EnableSession = true)]
    public static List<ListItem> GetMR()
    {
        SalesForce dsf = new SalesForce();
        string divcode = HttpContext.Current.Session["div_code"].ToString();
        string sf_code = HttpContext.Current.Session["Sf_Code"].ToString();
        string sftype = HttpContext.Current.Session["sf_type"].ToString();

        List<ListItem> fldname = new List<ListItem>();
        DataSet ds = new DataSet();

        if (sftype == "2")
        { ds = dsf.UserList_getMGR(divcode, sf_code); }
        else { ds = dsf.UserList_getMR(divcode); }
		
        List<sfMR> sf = new List<sfMR>();
        foreach (DataRow rows in ds.Tables[0].Rows)
        {
            fldname.Add(new ListItem
            {
                Value = rows["SF_Code"].ToString(),
                Text = rows["Sf_Name"].ToString(),

            });
        }
        return fldname;
    }
    [WebMethod(EnableSession = true)]
    public static List<ListItem> distname(string fieldforcecode)
    {
        SalesForce dsf = new SalesForce();
        List<ListItem> disname = new List<ListItem>();
        DataSet ds = new DataSet();
        ds = dsf.getdisttributor(fieldforcecode);

        List<dist> sf = new List<dist>();
        foreach (DataRow rows in ds.Tables[0].Rows)
        {
            disname.Add(new ListItem
            {
                Value = rows["Stockist_Code"].ToString(),
                Text = rows["Stockist_Name"].ToString(),

            });
        }
        return disname;
    }
    public class bindyear
    {
        public string value { get; set; }
        public string text { get; set; }
    }

    [WebMethod(EnableSession = true)]
    public static bindyear[] BindDate(string divcode)
    {
        TourPlan tp = new TourPlan();
        DataSet dsTP = new DataSet();
        dsTP = tp.Get_TP_Edit_Year(divcode);
        List<bindyear> sf = new List<bindyear>();
        if (dsTP.Tables[0].Rows.Count > 0)
        {
            for (int k = Convert.ToInt16(dsTP.Tables[0].Rows[0]["Year"]); k <= DateTime.Now.Year + 1; k++)
            {
                bindyear rt = new bindyear();
                rt.value = k.ToString();
                rt.text = k.ToString();
                sf.Add(rt);
            }
        }
        return sf.ToArray();
    }
    [WebMethod(EnableSession = true)]
    public static string clsdata(string ffc, string emonth, string eyear, string distname)
    {
        Product dv = new Product();
        string divcode = HttpContext.Current.Session["div_code"].ToString();

        DataSet dsProd = dv.getclosProdall(ffc, emonth, eyear, divcode, distname);
        return JsonConvert.SerializeObject(dsProd.Tables[0]);
    }
    [WebMethod(EnableSession = true)]
    public static string FillProd(string ffc)
    {
        Product dv = new Product();
        string div_code = HttpContext.Current.Session["div_code"].ToString();
        //string subdiv_code = HttpContext.Current.Session["subdivision_code"].ToString();

        DataSet dsProd = dv.get_pro_all(ffc, div_code);
        return JsonConvert.SerializeObject(dsProd.Tables[0]);
    }
}