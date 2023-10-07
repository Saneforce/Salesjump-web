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

public partial class MIS_Reports_rpt_stockist_accountstatement : System.Web.UI.Page
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
    string distributor = string.Empty;
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
    string tot_dr = string.Empty;
    string tot_value = string.Empty;
    string Monthsub = string.Empty;
    string dist_name = string.Empty;
    string dist_code = string.Empty;
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
string Prod_name= string.Empty;
    string Prod_value = string.Empty;


    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        feildforce = Request.QueryString["Feildforce"].ToString();
      
        dist_name = Request.QueryString["stockist_name"].ToString();
        distributor = Request.QueryString["stockist_code"].ToString();
        dist_code = distributor.Trim();
        Prod_name = Request.QueryString["product"].ToString();
        Prod_value = Request.QueryString["productval"].ToString();
        gg = Request.QueryString["DATE"].ToString();
        date = gg.Trim();
        DateTime dt = Convert.ToDateTime(date);
        gg1 = Request.QueryString["TODATE"].ToString();
        date1 = gg1.Trim();
        DateTime dt1 = Convert.ToDateTime(date1);
        hdate = dt.ToString("yyyy-MM-dd");
        tdate = dt1.ToString("yyyy-MM-dd");
        hdate1 = dt.ToString("dd-MM-yyyy");
        tdate1 = dt1.ToString("dd-MM-yyyy");
        fdatee.Text = hdate1;
        todatee.Text = tdate1;
        subdivision = Request.QueryString["subdivision"].ToString();
        System.Globalization.DateTimeFormatInfo mfi = new System.Globalization.DateTimeFormatInfo();

        //lblHead.Text = " Retailer Potential  for  " + hdate;
        fdate.Text = hdate1;
        lblretailerval.Text = dist_name;
        lblrouteval.Text = Prod_name;

        //feildforc.Text = feildforce;
        //FillSF();
        BindGridd();
    }
    protected void BindGridd()
    {

        Notice gg = new Notice();
        DataSet gg1 = new DataSet();
        gg1 = gg.Getstockistaccount_statement_outstanding_value(distributor, hdate, tdate,Prod_value);
        if (gg1.Tables.Count > 0)
        {
            if (gg1.Tables[0].Rows.Count > 0)
            {
                if (gg1.Tables[0].Rows[0][0].ToString() != "")
                {
                    fdatebal.Text = Math.Abs(Convert.ToInt32(gg1.Tables[0].Rows[0][0])).ToString();
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
        ff = gg.Getstockistaccount_statement(distributor, hdate, tdate,Prod_value);


        if (ff.Tables.Count > 0)
        {
            if (ff.Tables[0].Rows.Count > 0)
            {


                GridView1.DataSource = ff;
                GridView1.DataBind();


            }
        }
    }
    [WebMethod]
    public static string insertdata(string sf_name, string sf_code, string Quantity)
    {
        string msg = "";
        string sf_namee = sf_name.Trim();
        string sf_codee = sf_code.Trim();
        string myString = string.Empty;
        if (sf_namee != "" && sf_codee != "")
        {

            SqlConnection con = new SqlConnection(Globals.ConnString);
            con.Open();
            //SqlCommand cmdp = new SqlCommand(" select Inv_ID from Trans_Dis_Ret_Invoice_Head where Inv_No='" + Inv_no + "' ", con);

            //using (SqlDataReader rdr = cmdp.ExecuteReader())
            //{
            //    while (rdr.Read())
            //    {
            //        myString = rdr["Inv_ID"].ToString(); ; //The 0 stands for "the 0'th column", so the first column of the result.


            //    }
            //}

            SqlCommand cmd = new SqlCommand("insert into Mas_Salesforcefare_KM (Sf_Name,Sf_Code,Fare) values(@sf_name, @sf_code,@Quantity)", con);

            cmd.Parameters.AddWithValue("@sf_name", sf_namee);
            cmd.Parameters.AddWithValue("@sf_code", sf_codee);
            cmd.Parameters.AddWithValue("@Quantity", Quantity);

            int i = cmd.ExecuteNonQuery();
            if (i == 1)
            {
                msg = "true";
            }
            else
            {
                msg = "false";
            }
        }
        return msg;

    }
   }