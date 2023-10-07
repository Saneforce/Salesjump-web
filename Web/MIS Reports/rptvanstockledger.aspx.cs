using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
using System.Data.SqlClient;
using System.Web.Services;
using System.Configuration;  

public partial class MIS_Reports_rptvanstockledger : System.Web.UI.Page
{
    string sfCode = string.Empty;
    string sfname = string.Empty;
    string div_code = string.Empty;
    decimal iTotLstCount1 = 0;
    decimal iTotLstCount = 0;
    decimal iTotLstCount2 = 0;
    decimal value1 = 0;
    decimal value2 = 0;
    decimal val = 0;
    string sf_code = string.Empty;
    string FMonth = string.Empty;
    string FYear = string.Empty;
    string TMonth = string.Empty;
    string TYear = string.Empty;
    string feildforce = string.Empty;
    string route_name = string.Empty;
    DataSet dsSalesForce = new DataSet();
    DataSet dsDoc = null;
    DateTime dtCurrent;
    string sCurrentDate = string.Empty;
    string retailer_code = string.Empty;
    string Prod_name = string.Empty;
    string Prod_value = string.Empty;
    string Monthsub = string.Empty;
    string dist_name = string.Empty;
    string sfff_code = string.Empty;
    string date1 = string.Empty;
    string date = string.Empty;
    string subdivision = string.Empty;
    DataSet dsprd = new DataSet();
    DataSet dsDoctor = new DataSet();
    string gg = string.Empty;
    string gg1 = string.Empty;
    string hdate = string.Empty;
    string tdate = string.Empty;
    string hdate1 = string.Empty;
    string tdate1 = string.Empty;
    string Fdate = string.Empty;


    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        feildforce = Request.QueryString["Feildforce"].ToString();


        sf_code = Request.QueryString["fieldforceval"].ToString();
        sfff_code = sf_code.Trim();
        Prod_name = Request.QueryString["product"].ToString();
        Prod_value = Request.QueryString["productval"].ToString();
        //gg = Request.QueryString["DATE"].ToString();
        //date = gg.Trim();
        //DateTime dt = Convert.ToDateTime(date);
        //gg1 = Request.QueryString["TODATE"].ToString();
        //date1 = gg1.Trim();
        //DateTime dt1 = Convert.ToDateTime(date1);
        //hdate = dt.ToString("yyyy-MM-dd");
        //tdate = dt1.ToString("yyyy-MM-dd");
        //hdate1 = dt.ToString("dd-MM-yyyy");
        //tdate1 = dt1.ToString("dd-MM-yyyy");
        Fdate = Request.QueryString["FDate"].ToString();
        tdate = Request.QueryString["TDate"].ToString();
        fdatee.Text = Fdate;
        todatee.Text = tdate;
        System.Globalization.DateTimeFormatInfo mfi = new System.Globalization.DateTimeFormatInfo();


        //lblHead.Text = " Retailer Potential  for  " + hdate;
        fdate.Text = Fdate;
        lblretailerval.Text = feildforce.Trim();

        lblrouteval.Text = Prod_name.Trim();
        //feildforc.Text = feildforce;
        //FillSF();
        BindGridd();
    }
    protected void BindGridd()
    {

        Notice gg = new Notice();
        DataSet gg1 = new DataSet();
        gg1 = gg.Getvan_statement_outstanding_value(sfff_code, div_code, Fdate, tdate, Prod_value);
        if (gg1.Tables.Count > 0)
        {
            if (gg1.Tables[0].Rows.Count > 0)
            {
                if (gg1.Tables[0].Rows[0][0].ToString() != "")
                {
                    fdatebal.Text = gg1.Tables[0].Rows[0][1].ToString();
                }
            }

            else
            {
                fdatebal.Text = "0";
            }
        }
        else
        {
            fdatebal.Text = "0";
        }

        DataSet ff = new DataSet();
        ff = gg.Getvanaccount_statement(sfff_code, div_code, Fdate, tdate, Prod_value);


        if (ff.Tables.Count > 0)
        {
            if (ff.Tables[0].Rows.Count > 0)
            {


                GridView1.DataSource = ff;
                GridView1.DataBind();


            }
        }
    }

    protected void btsubmit_Click(object sender, EventArgs e)
    {

    }
}