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

public partial class MIS_Reports_Scheme_Master : System.Web.UI.Page
{
    string div_code = string.Empty;
    DataSet dsSalesForce = null;
    DataSet dsUserList = null;
    DataSet dsDoc = null;
    DataSet dsDCR = null;
    int product_total = 0;
    int FWD_total = 0;
    DateTime dtCurrent;
    DataSet dsProduct = null;
    DataSet dsCatg = null;
    int MonColspan = 0;
    DataSet dsTP = null;
    string tot_dr = string.Empty;
    string sCurrentDate = string.Empty;
    string sf_code = string.Empty;
    DataSet dsSf = null;
    string sf_type = string.Empty;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    Notice Addcomment = new Notice();
    DataSet dsState = new DataSet();
    string state_code = string.Empty;
    string sub_code = string.Empty;
    string sState = string.Empty;
    DataSet dsDivision = null;
    string[] statecd;
    string state_cd = string.Empty;
    DataSet dsSub = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        if (!Page.IsPostBack)
        {
            FillState(div_code);

            btsubmit.Visible = false;
            btncancel.Visible = false;

        }
    }
    private void FillState(string div_code)
    {
        Division dv = new Division();
        dsDivision = dv.getStatePerDivision(div_code);
        if (dsDivision.Tables[0].Rows.Count > 0)
        {
            int i = 0;
            state_cd = string.Empty;
            sState = dsDivision.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
            statecd = sState.Split(',');
            foreach (string st_cd in statecd)
            {
                if (i == 0)
                {
                    state_cd = state_cd + st_cd;
                }
                else
                {
                    if (st_cd.Trim().Length > 0)
                    {
                        state_cd = state_cd + "," + st_cd;
                    }
                }
                i++;
            }

            State st = new State();
            dsState = st.getState_new(state_cd);
            ddlState.DataTextField = "statename";
            ddlState.DataValueField = "state_code";
            ddlState.DataSource = dsState;
            ddlState.DataBind();
            ddlState.Items.Insert(0, new ListItem("--Select--", "0"));
        }
    }
    protected void groupby_SelectedIndexChanged(object sender, EventArgs e)
    {

        gridbrand.Visible = false;
        gridcat.Visible = false;
        GridView1.Visible = false;


        //if(groupby.SelectedItem.Text==""0
        //{
        //}
        //else if

    }
    protected void btnstate_Click(object sender, EventArgs e)
    {

       
            BindGridd();
            btsubmit.Visible = true;
            btncancel.Visible = true;

       
    }
    protected void BindGridd()
    {

        Notice gg = new Notice();
        DataSet ff = new DataSet();
           
 if (groupby.SelectedItem.Text == "Categorywise")
        {
            GridView1.Visible = false;
            gridbrand.Visible = false;
            gridcat.Visible = true;
            SalesForce sf = new SalesForce();
            ff = sf.GetcategoryName_Scheme(div_code, ddlState.SelectedValue.ToString(), Txt_Date.Text, Distributor.SelectedValue.ToString(),"");
      if (ff.Tables.Count > 0)
        {
            if (ff.Tables[0].Rows.Count > 0)
            {


                gridcat.DataSource = ff;
                gridcat.DataBind();


            }
        }
        }
        else if (groupby.SelectedItem.Text == "Brandwise")
        {
            GridView1.Visible = false;
            gridcat.Visible = false;
            gridbrand.Visible = true;
            SalesForce sf = new SalesForce();
            ff = sf.GetBrandName_Scheme(div_code, ddlState.SelectedValue.ToString(), Txt_Date.Text, Distributor.SelectedValue.ToString());
            if (ff.Tables.Count > 0)
            {
                if (ff.Tables[0].Rows.Count > 0)
                {


                    gridbrand.DataSource = ff;
                    gridbrand.DataBind();


                }
            }
        }
        else
        {
            gridcat.Visible = false;
            gridbrand.Visible = false;
            GridView1.Visible = true;
            ff = gg.Getproduct_schememaster(div_code, ddlState.SelectedValue.ToString(), Txt_Date.Text, Distributor.SelectedValue.ToString());
             if (ff.Tables.Count > 0)
        {
            if (ff.Tables[0].Rows.Count > 0)
            {


                GridView1.DataSource = ff;
                GridView1.DataBind();


            }
        }
}  
     
        

       
    }
    [WebMethod]
    public static string deletedata(string state_code,string Effective_Date,string stockist_code)
    {
        string msg = "";
        //string Product_namee = Product_name.Trim();

        string State_Codee = state_code.Trim();
        //string Discounte = Discount.Trim();
        //string Packagee = Package.Trim();
        //string myString = string.Empty;

        
        string divcode = null;

        if (HttpContext.Current.Session["div_code"] != null)
        {
            if (HttpContext.Current.Session["div_code"].ToString() != "")
            {
                divcode = HttpContext.Current.Session["div_code"].ToString();

            }
        }
        if (State_Codee != "")
        {

            SqlConnection con = new SqlConnection(Globals.ConnString);
            con.Open();



            SqlCommand cmd = new SqlCommand("delete from mas_scheme  where state_code='" + State_Codee + "' and Division_Code='" + divcode + "' and  CAST(Effective_From AS DATE)='" + Effective_Date + "'  and stockist_code='"+stockist_code+"'", con);

            

            int i = cmd.ExecuteNonQuery();
            if (i == 1)
            {
                msg = "true";
            }
            else
            {
                msg = "false";
            }
            //}


        }
        return msg;
    }
    [WebMethod]
    public static string insertdata(string Product_name, string Product_code, string Scheme, string Free, string Discount, string Package, string state_code, string Eff_Date,string stockist_code)
    {
        string msg = "";
        string Product_namee = Product_name.Trim();
        string Product_codee = Product_code.Trim();
        string Schemee = Scheme.Trim();
        string Freee = Free.Trim();
        string Discounte = Discount.Trim();
       
        string Packagee = Package.Trim();
        string myString = string.Empty;
        if (Package != "Y")
        {
            Package = "N";
        }
        string div_code = string.Empty;
        if (HttpContext.Current.Session["div_code"] != null)
        {
            if (HttpContext.Current.Session["div_code"].ToString() != "")
            {
                div_code = HttpContext.Current.Session["div_code"].ToString();

            }
        }

            if (Product_namee != "" && Product_codee != "")
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

                SqlCommand cmd = new SqlCommand("insert into Mas_Scheme (Product_name,Product_Code,Scheme,Free,Discount,Package,Division_Code,state_code,Effective_From,Stockist_Code) values(@Product_name, @Product_code,@Scheme,@Free,@Discount,@Package,@divcode,@state_code,@Effective_From,@stockist_code)", con);

                cmd.Parameters.AddWithValue("@Product_name", Product_namee);
                cmd.Parameters.AddWithValue("@Product_code", Product_codee);
                cmd.Parameters.AddWithValue("@Scheme", Schemee);
                cmd.Parameters.AddWithValue("@Free", Freee);
                cmd.Parameters.AddWithValue("@Discount", Discounte);
                cmd.Parameters.AddWithValue("@Package", Package);
                cmd.Parameters.AddWithValue("@divcode", div_code);
                cmd.Parameters.AddWithValue("@state_code", state_code);
                cmd.Parameters.AddWithValue("@Effective_From", Eff_Date);
                cmd.Parameters.AddWithValue("@stockist_code", stockist_code);
                
                int i = cmd.ExecuteNonQuery();
                if (i == 1)
                {
                    msg = "true";
                }
                else
                {
                    msg = "false";
                }
            //}
         

        }
        return msg;
    }
 


    [WebMethod]
    public static string insertdatacategorywise(string category_name, string category_code, string Scheme, string Free, string Discount, string Package,  string state_code, string Eff_Date, string stockist_code)
    {
        string msg = "";
        string Product_namee = category_name.Trim();
        string Product_codee = category_code.Trim();
        string Schemee = Scheme.Trim();
        string Freee = Free.Trim();
        string Discounte = Discount.Trim();
        string Packagee = Package.Trim();
        string myString = string.Empty;
        string div_code = string.Empty;
        


            if (Package != "Y")
            {
                Package = "N";
            }


            div_code = HttpContext.Current.Session["div_code"].ToString();


            if (category_name != "" && category_code != "")
        {

            SqlConnection con = new SqlConnection(Globals.ConnString);
            con.Open();

            SqlCommand cmdp = new SqlCommand("select * from mAS_PRODUCT_DETAIL    WHERE PRODUCT_CAT_CODE='" + category_code + "' AND DIVISION_CODE='" + div_code + "' and Product_Active_Flag=0 ", con);

            SqlDataAdapter da = new SqlDataAdapter(cmdp);
            DataSet dsTerritory = new DataSet();
            da.Fill(dsTerritory);
           
                foreach (DataRow pro1 in dsTerritory.Tables[0].Rows)
                {

                    SqlCommand cmd = new SqlCommand("insert into Mas_Scheme (Product_name,Product_Code,Scheme,Free,Discount,Package,Division_Code,state_code,Effective_From,stockist_code) values(@Product_name, @Product_code,@Scheme,@Free,@Discount,@Package,@divcode,@state_code,@Effective_From,@stockist_code)", con);

                    cmd.Parameters.AddWithValue("@Product_name", pro1["Product_Detail_Name"].ToString());
                    cmd.Parameters.AddWithValue("@Product_code", pro1["Product_Detail_Code"].ToString());
                    cmd.Parameters.AddWithValue("@Scheme", Schemee);
                    cmd.Parameters.AddWithValue("@Free", Freee);
                    cmd.Parameters.AddWithValue("@Discount", Discounte);
                    cmd.Parameters.AddWithValue("@Package", Package);
                    cmd.Parameters.AddWithValue("@divcode", div_code);
                    cmd.Parameters.AddWithValue("@state_code", state_code);
                    cmd.Parameters.AddWithValue("@Effective_From", Eff_Date);
                    cmd.Parameters.AddWithValue("@stockist_code", stockist_code);
                
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

            //using (SqlDataReader rdr = cmdp.ExecuteReader())
            //{
            //    while (rdr.Read())
            //    {
            //        myString = rdr["Inv_ID"].ToString(); ; //The 0 stands for "the 0'th column", so the first column of the result.


            //    }
            //}

           
            

        }
        return msg;
    }



    [WebMethod]
    public static string insertdataBrandwise(string Brand_name, string Brand_code, string Scheme, string Free, string Discount, string Package, string state_code, string Eff_Date, string stockist_code)
    {
        string msg = "";
        string Product_namee = Brand_name.Trim();
        string Product_codee = Brand_code.Trim();
        string Schemee = Scheme.Trim();
        string Freee = Free.Trim();
        string Discounte = Discount.Trim();
        string Packagee = Package.Trim();
        string myString = string.Empty;
        string div_code = string.Empty;
       

            if (Package != "Y")
            {
                Package = "N";
            }


            div_code = HttpContext.Current.Session["div_code"].ToString();


            if (Brand_name != "" && Brand_code != "")
            {

                SqlConnection con = new SqlConnection(Globals.ConnString);
                con.Open();

                SqlCommand cmdp = new SqlCommand("select * from mAS_PRODUCT_DETAIL    WHERE PRODUCT_Brd_CODE='" + Brand_code + "' AND DIVISION_CODE='" + div_code + "' and Product_Active_Flag=0 ", con);

                SqlDataAdapter da = new SqlDataAdapter(cmdp);
                DataSet dsTerritory = new DataSet();
                da.Fill(dsTerritory);

                foreach (DataRow pro1 in dsTerritory.Tables[0].Rows)
                {

                    SqlCommand cmd = new SqlCommand("insert into Mas_Scheme (Product_name,Product_Code,Scheme,Free,Discount,Package,Division_Code,state_code,Effective_From,stockist_code) values(@Product_name, @Product_code,@Scheme,@Free,@Discount,@Package,@divcode,@state_code,@Effective_From,@stockist_code)", con);

                    cmd.Parameters.AddWithValue("@Product_name", pro1["Product_Detail_Name"].ToString());
                    cmd.Parameters.AddWithValue("@Product_code", pro1["Product_Detail_Code"].ToString());
                    cmd.Parameters.AddWithValue("@Scheme", Schemee);
                    cmd.Parameters.AddWithValue("@Free", Freee);
                    cmd.Parameters.AddWithValue("@Discount", Discounte);
                    cmd.Parameters.AddWithValue("@Package", Package);
                    cmd.Parameters.AddWithValue("@divcode", div_code);
                    cmd.Parameters.AddWithValue("@state_code", state_code);
                    cmd.Parameters.AddWithValue("@Effective_From", Eff_Date);
                    cmd.Parameters.AddWithValue("@stockist_code", stockist_code);
                

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

                //using (SqlDataReader rdr = cmdp.ExecuteReader())
                //{
                //    while (rdr.Read())
                //    {
                //        myString = rdr["Inv_ID"].ToString(); ; //The 0 stands for "the 0'th column", so the first column of the result.


                //    }
                //}


            


        }
        return msg;
    }
    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {
        filldistributor_statewise();
    }
    private void filldistributor_statewise()
    {
        Distributor.DataSource = null;
        Distributor.Items.Clear();
        Distributor.Items.Insert(0, new ListItem("--Select--", "0"));

        Order sd = new Order();
        dsSalesForce = sd.view_stockist_statewise(div_code, ddlState.SelectedValue);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            Distributor.DataTextField = "Stockist_Name";
            Distributor.DataValueField = "Stockist_code";
            Distributor.DataSource = dsSalesForce;
            Distributor.DataBind();
            Distributor.Items.Insert(0, new ListItem("--Select--", "0"));


        }
    }
}