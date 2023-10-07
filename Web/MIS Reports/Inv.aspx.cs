using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bus_EReport;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Web.Services;
using System.Configuration;  

public partial class MIS_Reports_Inv : System.Web.UI.Page
{
    string sfCode = string.Empty;
   string sf_code=string.Empty;
  string sf_type =string.Empty;
    SalesForce sf = new SalesForce();
    DataSet dsDoc = null;
    DataSet dsdoc = null;
    int search = 0;
    int time = 0;
    Territory objTerritory = new Territory();
    DataSet dsSalesForce = null;
    ListedDR lstDR = new ListedDR();
    DataSet dsTP = null;
    DataSet dsListedDR = null;

    string div_code = string.Empty;
    string connection = Globals.ConnString;
    protected void Page_Load(object sender, EventArgs e)
    { 

   sf_code = Session["sf_code"].ToString();
        sf_type = Session["sf_type"].ToString();

        if (sf_type == "3")
        {
            div_code = Session["Division_Code"].ToString().Replace(",", "");
        }
        else
        {
            div_code = Session["Division_Code"].ToString().Replace(",", "");
        }
      
        BindGridd();
        if (!Page.IsPostBack)
        {
            fillRetailer();

        }
    }
    private void fillRetailer()
    {
        SalesForce sd = new SalesForce();
        dsSalesForce = sd.retailer_divisionwise(div_code);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            Ret_name.DataTextField = "ListedDr_Name";
            Ret_name.DataValueField = "ListedDrCode";
            Ret_name.DataSource = dsSalesForce;
            Ret_name.DataBind();
            Ret_name.Items.Insert(0, new ListItem("----------------------------Select  Retailer------------------------", "0"));
        }

    }
    protected void BindGridd()
    {

        SalesForce gg = new SalesForce();
        DataSet ff = new DataSet();

        ff = gg.Secondary_sales_productdetail_withrate(div_code);


        if (ff.Tables.Count > 0)
        {
            if (ff.Tables[0].Rows.Count > 0)
            {


                GridView1.DataSource = ff;
                GridView1.DataBind();


            }
        }
    }
 [System.Web.Services.WebMethod]
    public static string CheckEmail(string useroremail)
    {
        string retval = "";
        SqlConnection con = new SqlConnection(Globals.ConnString);
        con.Open();
        SqlCommand cmd = new SqlCommand("select Inv_ID from Trans_Dis_Ret_Invoice_Head where Inv_No=@UserNameorEmail", con);
        cmd.Parameters.AddWithValue("@UserNameorEmail", useroremail);
        SqlDataReader dr = cmd.ExecuteReader();
        if (dr.HasRows)
        {
            retval = "true";
        }
        else
        {
            retval = "false";
        }

        return retval;
    }  

    [WebMethod]
    public static string insertdata(string Product_name, string Product_code, string Quantity, string Pieces, string Value,string Inv_no)
    {
        string msg = "";
        string Product_namee = Product_name.Trim();
        string Product_codee = Product_code.Trim();
        string  myString =string.Empty;
        if (Product_namee != "" && Product_codee != "")
        {
            
            SqlConnection con = new SqlConnection(Globals.ConnString);
            con.Open();
            SqlCommand cmdp = new SqlCommand(" select Inv_ID from Trans_Dis_Ret_Invoice_Head where Inv_No='" + Inv_no + "' ", con);

            using (SqlDataReader rdr = cmdp.ExecuteReader())
            {
                while (rdr.Read())
                {
                    myString = rdr["Inv_ID"].ToString(); ; //The 0 stands for "the 0'th column", so the first column of the result.
                  

                }
            }
          
            SqlCommand cmd = new SqlCommand("insert into Trans_Dis_Ret_Invoice_Detail  (Inv_ID, Product_Name,Product_Code,Qty_Case,Qty_Piece,Product_Value) values(@Inv_id,@Product_name, @Product_code,@Quantity,@Pieces,@Value)", con);
            cmd.Parameters.AddWithValue("@Inv_id", myString);
            cmd.Parameters.AddWithValue("@Product_name", Product_namee);
            cmd.Parameters.AddWithValue("@Product_code", Product_codee);
            cmd.Parameters.AddWithValue("@Quantity", Quantity);
            cmd.Parameters.AddWithValue("@Pieces", Pieces);
            cmd.Parameters.AddWithValue("@Value", Value);
           
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


    [WebMethod]
    public static string insertheaddata(string Invoice_no, string Retailer_code, string Invoice_Date, string Cust_code, string Total_Amount)
    {
        string id = string.Empty;
        string msg = "";
        
     
        if (Invoice_no != "" && Retailer_code != "" )
        {
            SqlConnection con = new SqlConnection(Globals.ConnString);

         
            SqlCommand cmd = new SqlCommand("insert into Trans_Dis_Ret_Invoice_Head  (Inv_No,Cus_Code,Inv_Date,Dist_Code,Inv_Amount) values(@Inv_No, @Cus_Code,@Inv_Date,@Dist_Code,@Inv_Amount)", con);
            cmd.Parameters.AddWithValue("@Inv_No", Invoice_no);
            cmd.Parameters.AddWithValue("@Cus_Code", Retailer_code);
            cmd.Parameters.AddWithValue("@Inv_Date", Invoice_Date);
            cmd.Parameters.AddWithValue("@Dist_Code", Cust_code);
            cmd.Parameters.AddWithValue("@Inv_Amount", Total_Amount);
            con.Open();
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

    protected void btnSelect_Click(object sender, EventArgs e)
    {


        lblSelectedValue.Text = Ret_name.SelectedValue;
    }
}