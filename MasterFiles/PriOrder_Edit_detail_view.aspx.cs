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

public partial class MasterFiles_PriOrder_Edit_detail_view : System.Web.UI.Page
{
    DataSet dsSalesForce = null;
    DataSet dsdis = new DataSet();
    DataSet dsTP = null;
    DataSet dsDivision = null;
    string strMultiDiv = string.Empty;
    DataSet dsSf = null;
    string stockist = string.Empty;
     public  string StartDate = string.Empty;
     public  string EndDate = string.Empty;
     string sf_code = string.Empty;
    string Stockist_Name = string.Empty;
    public string Order_no = string.Empty;
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
        Order_no = Request.QueryString["Order_no"].ToString();
        txtdis.Text = Request.QueryString["Stockist_Name"].ToString();
        txtff.Text = Request.QueryString["Sf_Name"].ToString();
        txtorderno.Text = Request.QueryString["Order_no"].ToString();
        txtSuperStockist.Text = Request.QueryString["Superstockist"].ToString();
        lblsscode.Text = Request.QueryString["Superstockistcode"].ToString();
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
        dsST = sd.get_superstockist(div_code);
        SalesForce sf = new SalesForce();
        dsSf = sf.GET_priProduct_detail(Order_no, ViewState["StartDate"].ToString(), ViewState["EndDate"].ToString());
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
            DropDownList DropDownList1 = (e.Row.FindControl("ddldist") as DropDownList);

            if (dsST.Tables[0].Rows.Count > 0)
            {
                DropDownList1.DataTextField = "Division_Name";
                DropDownList1.DataValueField = "division_code";
                DropDownList1.DataSource = dsST;
                DropDownList1.DataBind();
            }

        }
    }
   [WebMethod]
   public static string updatepriorderdetails(string primary)
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
        IList<priorder_details> priorder = JsonConvert.DeserializeObject<IList<priorder_details>>(primary);
        Order sd = new Order();
        int ret = -1;
        try
        {
            for (int i = 0; i < priorder.Count; i++)
            {
                ret = sd.updatepriorderdetail(priorder[i].orderflag, priorder[i].orderno, priorder[i].ordervalue, priorder[i].productcode, priorder[i].productname, priorder[i].cqty, priorder[i].pQty, priorder[i].rate, priorder[i].values, priorder[i].casecnf, priorder[i].Pcscnf, priorder[i].superstockist, priorder[i].Trans_POrd_No, priorder[i].newOrderno, div_code, Order_date, priorder[i].sf_code);
            }
            sucess = "updated";
            return "Update Sucessfully....!";
        }
        catch
        {
            return "updated failed....!";
        }
    }
    public class priorder_details
    {
        public string productcode { get; set; }
        public string productname { get; set; }
        public string cqty { get; set; }
        public string casecnf { get; set; }
        public string pQty { get; set; }
        public string Pcscnf { get; set; }
        public string rate { get; set; }
        public string values { get; set; }
        public string orderflag { get; set; }    
        public string orderno { get; set; }
        public string sf_code { get; set; }
        public string ordervalue { get; set; }
        public string  superstockist{ get; set; }
        public string Trans_POrd_No { get; set; }
        public string newOrderno { get; set; }
    }
}
