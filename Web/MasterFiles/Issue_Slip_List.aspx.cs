using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
using System.Data.SqlClient;

public partial class MasterFiles_Issue_Slip_List : System.Web.UI.Page
{
    DataSet dsDivision = null;
    DataSet dsState = null;
    DataSet dsProd = null;
    DataSet dsTP = null;
    string sState = string.Empty;
    string div_code = string.Empty;
    string[] statecd;
    string state_cd = string.Empty;
    string prod_code = string.Empty;
    string prod_name = string.Empty;
    decimal mrp_amt;
    decimal ret_amt;
    decimal dist_amt;
    decimal nsr_amt;
    decimal target_amt;
    string effective_from = string.Empty;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int time;
    string Div_Code = string.Empty;
    string SF_Code = string.Empty;
    string Sub_DivCode = string.Empty;
    string mode = string.Empty;
    string grn_no = string.Empty;
    string grn_dt = string.Empty;
    string supp_code = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {

        div_code = Session["div_code"].ToString();
        if (!Page.IsPostBack)
        {
            
            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);
            mode = Request.QueryString["Mode"].ToString();
           
            if (mode == "1")
            {
                Product dv = new Product();
                grn_no = Request.QueryString["GRN_No"].ToString();
                //Iss_No.Text = " Issue slip No : "+grn_no;
                grn_dt = Request.QueryString["GRN_Date"].ToString();
                supp_code = Request.QueryString["Supp_Code"].ToString();
                dsProd = dv.getIssue_slip_Head(supp_code.ToString(), grn_no, div_code, grn_dt);
                if (dsProd.Tables[0].Rows.Count > 0)
                {
                    Label1.Text = " Issue From : " + dsProd.Tables[0].Rows[0].ItemArray.GetValue(3).ToString().Trim();
                    Label2.Text = " Issue To : " + dsProd.Tables[0].Rows[0].ItemArray.GetValue(5).ToString().Trim();
                    Iss_No.Text = " Issue slip No : "+ grn_no;
                    Label3.Text = " Issue slip Date : " + grn_dt;
                }
               
            }
            FillProd();
        }
    }
    protected override void OnLoadComplete(EventArgs e)
    {

        ServerEndTime = DateTime.Now;
        TrackPageTime();//It will give you page load time  
    }
    public void TrackPageTime()
    {
        TimeSpan serverTimeDiff = ServerEndTime.Subtract(ServerStartTime);
        time = serverTimeDiff.Minutes;

    }
   
    private void FillProd()
    {
        Product dv = new Product();

        if (supp_code == " ")
        {
            //dsProd = dv.getProductRate_all(div_code);
        }
        else
        {
            dsProd = dv.getIssue_slip(supp_code.ToString(), grn_no, div_code, grn_dt);
        }

        DataSet DsAudit = dv.getProductRate_all(div_code);
        if (DsAudit.Tables[0].Rows.Count > 0 || Session["sf_type"].ToString() == "3")
        {
            dsProd = dv.getIssue_slip(supp_code.ToString(), grn_no, div_code, grn_dt);
           
            GrdDoctor.DataSource = dsProd;
            GrdDoctor.DataBind();
            //btnSubmit.Visible = false;
        }
    }

}