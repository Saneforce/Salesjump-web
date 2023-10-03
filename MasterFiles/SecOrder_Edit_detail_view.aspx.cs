using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
using System.Drawing.Imaging;
using System.Web.Services;
using Newtonsoft.Json;
using System.Globalization;

public partial class MasterFiles_SecOrder_Edit_detail_view : System.Web.UI.Page
{
    DataSet dsSalesForce = null;
    DataSet dsdis = new DataSet();
    DataSet dsTP = null;
    DataSet dsDivision = null;
    string strMultiDiv = string.Empty;
    DataSet dsSf = null;
    string stockist = string.Empty;
    public string StartDate = string.Empty;
    public string EndDate = string.Empty;
    string sf_code = string.Empty;
    string Stockist_Name = string.Empty;
    public string Transslno = string.Empty;
    string Superstockist = string.Empty;
    public string div_code = string.Empty;
    DataSet dsST;
    public static string sucess = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        StartDate = Request.QueryString["StartDate"].ToString();
        EndDate = Request.QueryString["EndDate"].ToString();
        txtffc.Text = Request.QueryString["sf_code"].ToString();
        txttransslno.Text = Request.QueryString["Transslno"].ToString();
       
        txtdis.Text = Request.QueryString["Stockist_Name"].ToString();
        txtff.Text = Request.QueryString["Sf_Name"].ToString();
        txtretailer.Text = Request.QueryString["RetailerName"].ToString();
        lbldiscode.Text = Request.QueryString["stockist_code"].ToString();
        if (!Page.IsPostBack)
        {
            ViewState["StartDate"] = StartDate;
            ViewState["EndDate"] = EndDate;

            FillManagers();
        }
        if (sucess == "updated")
        {
            FillManagers();
        }  
}
    private void FillManagers()
    {
        Order sd = new Order();
        dsST = sd.get_secStockist(div_code, txtffc.Text);
        SalesForce sf = new SalesForce();
        dsSf = sf.GET_SecProduct_detail(txttransslno.Text);
        GVData.DataSource = dsSf;
        GVData.DataBind();
        if (dsSf.Tables[0].Rows.Count > 0)
        {
            btnSubmit.Visible = true;
            btnclose.Visible = true;
        }
    }
   protected void GVData_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DropDownList DropDownList1 = (e.Row.FindControl("ddlstockist") as DropDownList);

            if (dsST.Tables[0].Rows.Count > 0)
            {
                DropDownList1.DataTextField = "Stockist_Name";
                DropDownList1.DataValueField = "Stockist_Code";
                DropDownList1.DataSource = dsST                    ;
                DropDownList1.DataBind();
            }
        }
    }
    [WebMethod]
    public static List<scheme> schemedetail(string dis, string prod, string date,string qnty)
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
        List<scheme> scheme = new List<scheme>();
        Order orcs = new Order();
        DataSet dast;
        dast = orcs.schemedetail(div_code, prod, dis, date, qnty);

        foreach(DataRow row in dast.Tables[0].Rows)
        {
            scheme scheme1 = new scheme();
            scheme1.Scheme = row["Scheme"].ToString();
            scheme1.Free = row["Free"].ToString();
            scheme1.Discount = row["Discount"].ToString();
            scheme.Add(scheme1);
        }
        return scheme;
    }

    [WebMethod]
    public static string updatesecorderdetails(string sec)
    {

        string div_code = "";
        DateTime datetime = DateTime.UtcNow.Date;
        string Order_date = datetime.ToString("dd/MM/yyyy");
        try
        {
            div_code = HttpContext.Current.Session["div_code"].ToString();
        }
        catch
        {
            div_code = HttpContext.Current.Session["Division_Code"].ToString();
        }
        IList<secorder_details> priorder = JsonConvert.DeserializeObject<IList<secorder_details>>(sec);
        Order sd = new Order();
        int ret = -1;
        try
        {
            for (int i = 0; i < priorder.Count; i++)
            {
                ret = sd.updateSecorderdetail(priorder[i].orderflag, priorder[i].Transslno, priorder[i].productcode, priorder[i].qty, priorder[i].qtycnf, priorder[i].free, priorder[i].rate, priorder[i].freecnf,div_code, priorder[i].trans_order_no, priorder[i].neworderno, priorder[i].stockistcode,priorder[i].txtdiscount,priorder[i].txtdisprice);
            }
            sucess = "updated";
            return "Update Sucessfully....!";
        }
        catch
        {
            return "updated failed....!";
        }
    }

    public class secorder_details
    {
        public string rate { get; set; }
        public string freecnf { get; set; }
        public string qty { get; set; }
        public string qtycnf { get; set; }
        public string free { get; set; }
        public string orderflag { get; set; }
        public string productcode { get; set; }
        public string Transslno { get; set; }
        public string neworderno { get; set; }
        public string stockistcode { get; set; }
        public string trans_order_no { get; set; }
        public string txtdiscount { get; set; }
        public string txtdisprice { get; set; }


    }
    public class scheme
    {
        public string Scheme { get; set; }
        public string Free { get; set; }
        public string Discount { get; set; }
    }
}