using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using Bus_EReport;

public partial class Pay_Entry : System.Web.UI.Page
{
    DataSet dsSalesForce = null;
    DataSet dsdiv = new DataSet();
    DataSet dsTP = null;
    DataSet dsDivision = null;
    string strMultiDiv = string.Empty;
    string div_code = string.Empty;
    string sf_code = string.Empty;
    DataSet dsSf = null;
    string sf_type = string.Empty;
    string sQryStr = string.Empty;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    string Plandate = string.Empty;
    int time;
    //my day plan
    string sf_name = string.Empty;
    string Sf_code = string.Empty;
    string Dist_name = string.Empty;
    string Dist_code = string.Empty;
    string Cus_name = string.Empty;
    string Cus_code = string.Empty;
    string Route_name = string.Empty;
    string Route_code = string.Empty;
    string Amount = string.Empty;
    string Pay_Type = string.Empty;
    string Pay_date = string.Empty;
    string Ref_no = string.Empty;
    string Remarks = string.Empty;

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
        div_code = Session["div_code"].ToString();
        sf_code = Session["sf_code"].ToString();
        sf_type = Session["sf_type"].ToString();

        if (Session["sf_type"].ToString() == "1")
        {
            if (!Page.IsPostBack)
            {


                FillDis();
                //FillRou();
                //FillCus();
                FillPay_Type();
                FillMRManagers();
            }

        }

        else if (Session["sf_type"].ToString() == "2")
        {

            if (!Page.IsPostBack)
            {


                FillDis();
                //FillRou();
                //FillCus();
                FillPay_Type();
                FillMRManagers();
            }

        }

        else if (Session["sf_type"].ToString() == "3")
        {


            if (!Page.IsPostBack)
            {


                FillDis();
                //FillRou();
                //FillCus();
                FillPay_Type();
                FillMRManagers();
            }

        }



    }

    private void FillMRManagers()
    {
        try
        {
            SalesForce sf = new SalesForce();
            dsSalesForce = sf.SalesForceListMgrGet(div_code, sf_code);
            if (sf_type == "3")
            {
                dsSalesForce.Tables[0].Rows[0].Delete();
            }
            else
            {

            }
            if (dsSalesForce.Tables[0].Rows.Count > 0)
            {
                sf_Name.DataTextField = "sf_name";
                sf_Name.DataValueField = "sf_code";
                sf_Name.DataSource = dsSalesForce;
                sf_Name.DataBind();



            }

        }
        catch (Exception)
        {

        }
    }


    protected DataSet FillDis()
    {
        TP_New tp = new TP_New();

        dsTP = tp.Get_Sf_By_Dis(div_code, sf_Name.SelectedValue);
        if (dsTP.Tables[0].Rows.Count > 0)
        {
            Dis_name.DataTextField = "Stockist_Name";
            Dis_name.DataValueField = "Distributor_Code";
            Dis_name.DataSource = dsTP;
            Dis_name.DataBind();
            Dis_name.Items.Insert(0, new ListItem("---Select---", "0"));
        }
        else
        {
            getddlSF_Code();
        }
        return dsTP;
    }

    private void getddlSF_Code()
    {
        TP_New tp = new TP_New();

        dsTP = tp.Get_Sf_By_Dis(div_code, sf_Name.SelectedValue);
        if (dsTP.Tables[0].Rows.Count > 0)
        {
            Dis_name.DataTextField = "Stockist_Name";
            Dis_name.DataValueField = "Distributor_Code";
            Dis_name.DataSource = dsTP;
            Dis_name.DataBind();
            Dis_name.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        else
        {
            Territory terr = new Territory();
            dsTP = terr.getSF_Code_distributor(div_code);
            if (dsTP.Tables[0].Rows.Count > 0)
            {
                Dis_name.DataTextField = "stockist_Name";
                Dis_name.DataValueField = "Stockist_Code";
                Dis_name.DataSource = dsTP;
                Dis_name.DataBind();

                if (Session["sf_code"] == null || Session["sf_code"].ToString() == "admin")
                {
                    Dis_name.SelectedIndex = 0;
                }

            }
            else
            {
                Dis_name.SelectedIndex = 0;
            }
        }

    }



    public void FillRou()
    {
        TP_New tpRP = new TP_New();
        DataSet dsRPDisable = new DataSet();


        dsRPDisable = tpRP.getRouteByDist_Trial(Dis_name.SelectedValue, div_code);
        if (dsRPDisable.Tables[0].Rows.Count > 0)
        {

            Rou.DataTextField = "Territory_Name";
            Rou.DataValueField = "Territory_Code";
            Rou.DataSource = dsRPDisable;
            Rou.DataBind();
            Rou.Items.Insert(0, new ListItem("---Select---", "0"));
        }
    }
    public void FillCus()
    {
        TP_New tpRP = new TP_New();
        DataSet dsRPDisable = new DataSet();


        dsRPDisable = tpRP.getCusByRou_Trial(Rou.SelectedValue, div_code);
        if (dsRPDisable.Tables[0].Rows.Count > 0)
        {

            cus.DataTextField = "ListedDr_Name";
            cus.DataValueField = "ListedDrCode";
            cus.DataSource = dsRPDisable;
            cus.DataBind();
            cus.Items.Insert(0, new ListItem("---Select---", "0"));
        }
    }
    public void FillPay_Type()
    {
        TP_New tpRP = new TP_New();
        DataSet dsRPDisable = new DataSet();


        dsRPDisable = tpRP.getPay_Type(div_code);
        if (dsRPDisable.Tables[0].Rows.Count > 0)
        {

            DDL_type.DataTextField = "Name";
            DDL_type.DataValueField = "Code";
            DDL_type.DataSource = dsRPDisable;
            DDL_type.DataBind();
            //password.Items.Insert(0, new ListItem("--Select--", "0"));
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
    protected void Dis_name_SelectedIndexChanged(object sender, EventArgs e)
    {

        FillRou();
    }
    protected void sf_Name_SelectedIndexChanged(object sender, EventArgs e)
    {
        getddlSF_Code();
        Rou.Items.Insert(0, new ListItem("--Select--", "0"));
    }
    protected void Cus_name_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillCus();
    }
    protected void button_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        try
        {

            sf_name = sf_Name.SelectedItem.ToString();
            Sf_code = sf_Name.SelectedValue.ToString();
            Dist_name = Dis_name.SelectedItem.ToString();
            Dist_code = Dis_name.SelectedValue.ToString();
            Route_name = Rou.SelectedItem.ToString();
            Route_code = Rou.SelectedValue.ToString();
            Cus_name = cus.SelectedItem.ToString();
            Cus_code = cus.SelectedValue.ToString();
            int amout = Convert.ToInt32(Txt_Amout.Text);
            string Pay_type = DDL_type.SelectedValue.ToString();
            DateTime Pay_date = Convert.ToDateTime(Txt_Date.Text);
            string ref_no = Txt_ref_No.Text;
            Remarks = Txt_Remark.Text;


            TP_New tpRP = new TP_New();

            int iReturn = tpRP.Pay_Entry_RecordAdd(Sf_code, sf_name, Cus_code, Cus_name, amout, Pay_type, Pay_date, ref_no, Remarks, Dist_code, Route_code);

            if (iReturn > 0)
            {


                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Created Successfully');</script>");
                Resetall();
            }
           
        }
        catch (Exception ex)
        {

        }

    }
    public void Resetall()
    {
        sf_Name.SelectedIndex = 0;
        Dis_name.SelectedIndex = 0;
        Rou.SelectedIndex = -1;
        cus.SelectedIndex = -1;
        Txt_Amout.Text = "";
        DDL_type.SelectedIndex = 0;
        Txt_Date.Text = "";
        Txt_ref_No.Text = "";
        Txt_Remark.Text = "";
    }


    protected void button1_Click(object sender, EventArgs e)
    {
        Resetall();
    }
}