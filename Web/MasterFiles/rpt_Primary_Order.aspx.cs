using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Globalization;
using System.Data;
using System.Configuration;
using Newtonsoft.Json;
using Bus_EReport;
using System.Web.Services;
public partial class MasterFiles_rpt_Primary_Order : System.Web.UI.Page
{
    public string div_code = string.Empty;
    public string sf_code = string.Empty;
    public string StartDate = string.Empty;
    public string EndDate = string.Empty;
   public static  string sucess = string.Empty;
    DataSet dsSf = null;
    DataSet dsSalesForce = null;
    DataSet dsST;
    string sf_type = string.Empty;
    Order sd = null;
    DataSet dspri = null;
    DateTime dTime = DateTime.UtcNow.Date;
    DateTime EndDate1 = DateTime.UtcNow.Date;
    DateTime StartDate1 = DateTime.UtcNow.Date;
	
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

        div_code = Session["div_code"].ToString();

        if (!Page.IsPostBack)
        {

            EndDate = DateTime.UtcNow.ToString("yyyy-MM-dd");
            StartDate = DateTime.UtcNow.ToString("yyyy-MM-dd");
            hdnffdate.Value = StartDate;
            hdnttdate.Value = EndDate; 
            FillManagers();
            bindgrid();


        }
       if(Page.IsPostBack)
        {
            EndDate = Convert.ToString(Request.Form["txtFrom"]);
            StartDate = Convert.ToString(Request.Form["txtFrom1"]);
            string EVENTARGUMENT = this.Request["__EVENTARGUMENT"];                      
                string eventTarget = this.Request["__EVENTTARGET"];

            if (EVENTARGUMENT == "a")
            {

                if ((eventTarget != "NaN/NaN/NaN")&&(eventTarget!=""))
                {
                    try
                    {
                        dTime = DateTime.ParseExact(eventTarget, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                        StartDate = dTime.ToString("yyyy-MM-dd");

                        EndDate1 = DateTime.ParseExact(EndDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        hdnffdate.Value = StartDate;
                        hdnttdate.Value = EndDate1.ToString("yyyy-MM-dd");
                    }
                    catch
                    {
                        ClientScript.RegisterStartupScript(GetType(), "Message", "<script Language='javascript'>alert('Please Select Date');</script>");
                    }
                }
               else
                {
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<script Language='javascript'>alert('Please Select Date');</script>");
                } 
            }
            else if (EVENTARGUMENT == "b")
            {
                if ((eventTarget != "NaN/NaN/NaN") && (eventTarget != ""))
                {
                    try
                    {
                        dTime = DateTime.ParseExact(eventTarget, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        EndDate = dTime.ToString("yyyy-MM-dd");
                        StartDate1 = DateTime.ParseExact(StartDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        hdnffdate.Value = StartDate1.ToString("yyyy-MM-dd");
                        hdnttdate.Value = EndDate;
                    }
                    catch
                    {
                        ClientScript.RegisterStartupScript(GetType(), "Message", "<script Language='javascript'>alert('Please Select Date');</script>");
                    }
                }
                   else
                {
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<script Language='javascript'>alert('Please Select Date');</script>");
                } 
            }
            
            if (Convert.ToString(ddlFieldForce.SelectedValue)!="0")
            {
                //  FillManagers();
               // hdnffdate.Value = StartDate;
               // hdnttdate.Value = EndDate;
                bindgrid();
            }
            else
            {
                FillManagers();
                bindgrid();
            }
            }
     
        if (sucess == "updated")
        {
            bindgrid();
        }
    }
   //  public void ddlFieldForce_SelectedIndexChange(object sender, EventArgs e)
  //  {

   //     FillManagers();
    //    bindgrid();
    
   // }

    private void bindgrid()
    {
        Order sd = new Order();
        dsST = sd.get_superstockist(div_code);

        sf_code = Convert.ToString(ddlFieldForce.SelectedValue);
        
        SalesForce sf = new SalesForce();
        dsSf = sf.Get_PrimaryOrder(sf_code, div_code,  hdnffdate.Value, hdnttdate.Value);
        GVData.DataSource = dsSf;
        GVData.DataBind();

        if (dsSf.Tables[0].Rows.Count > 0)
        {
            btnSave.Visible = true;
        }
    }
    private void FillManagers()
    {
        SalesForce sf = new SalesForce();
        dsSalesForce = sf.get_fieldforceList(div_code, hdnffdate.Value, hdnttdate.Value);
        ddlFieldForce.DataTextField = "sf_name";
        ddlFieldForce.DataValueField = "sf_code";
        ddlFieldForce.DataSource = dsSalesForce;
        ddlFieldForce.DataBind();
        ddlFieldForce.Items.Insert(0,new  ListItem("--select--","0"));
    }
  
    protected void GVData_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DropDownList DropDownList1 = (e.Row.FindControl("ddlsuper") as DropDownList);
            if (dsST.Tables[0].Rows.Count > 0)
            {
                DropDownList1.DataTextField = "division_name";
                DropDownList1.DataValueField = "division_code";
                DropDownList1.DataSource = dsST;
                DropDownList1.DataBind();
            }
        }
    }

    [WebMethod]    
    public static string updatepriorder(string priorder)
    {
   
        string div_code = "";
       DateTime dateTime = DateTime.UtcNow.Date;
        string Order_date = dateTime.ToString("dd/MM/yyyy");

        try
        {
            div_code = HttpContext.Current.Session["div_code"].ToString();
        }
        catch
        {
            div_code = HttpContext.Current.Session["Division_Code"].ToString();
        }            
        IList<priOrder_values> prilist = JsonConvert.DeserializeObject < IList<priOrder_values>>(priorder); ;     
        Order sd = new Order();
        int ret = -1;
        try
        {
            for(int i=0;i<prilist.Count;i++)
            {
                ret=sd.updatepriorder(prilist[i].order_flag,prilist[i].order_no,prilist[i].sf_code,prilist[i].stockist,prilist[i].S_No, prilist[i].order_value,div_code, Order_date);
            }
           
            sucess = "updated";
            return "Update Sucessfully....!";          
            
        }
        catch
        {
            return "Updated Failed...! ";
        }
    }

    public class priOrder_values
    {
        public string sf_code { get; set; }
        public string stockist { get; set; }
        public string S_No { get; set; }
        public string order_no {get;set;}
        public string order_value { get; set; }
        public string order_flag { get; set; }      
    }  
}

