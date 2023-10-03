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
using Newtonsoft.Json;
using System.Globalization;
using ClosedXML.Excel;
using System.Xml;
using System.IO;
using DBase_EReport;

public partial class MasterFiles_New_PrimaryScheme_Master : System.Web.UI.Page
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
            fillsubdivision();
            FillState(div_code);

            //btsubmit.Visible = false;
            //btncancel.Visible = false;

        }
    }
    private void fillsubdivision()
    {
        SalesForce sd = new SalesForce();
        dsSalesForce = sd.Getsubdivisionwise(div_code);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            //subdiv.DataTextField = "subdivision_name";
            //subdiv.DataValueField = "subdivision_code";
            //subdiv.DataSource = dsSalesForce;
            //subdiv.DataBind();
            //subdiv.Items.Insert(0, new ListItem("--Select--", "0"));            

        }
    }

    [WebMethod]
    public static string getDivision()
    {
        string strQry = string.Empty;
        DataTable dt = null;
        DBase_EReport.DB_EReporting db_ER = new DBase_EReport.DB_EReporting();
        string div_code = HttpContext.Current.Session["div_code"].ToString();
        div_code = div_code.TrimEnd(',');

        strQry = "select subdivision_code,subdivision_name from mas_subdivision where Div_Code=" + div_code + " and SubDivision_Active_Flag=0 order by subdivision_name";

        try
        {
            dt = db_ER.Exec_DataTable(strQry);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return JsonConvert.SerializeObject(dt);
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
            //ddlState.DataTextField = "statename";
            //ddlState.DataValueField = "state_code";
            //ddlState.DataSource = dsState;
            //ddlState.DataBind();
            //ddlState.Items.Insert(0, new ListItem("--Select--", "0"));
        }
    }

    [WebMethod]
    public static string getTerritory_for_Secondary()
    {
        DataSet ds = new DataSet();
        string div_code = "";
        try
        {
            div_code = HttpContext.Current.Session["div_code"].ToString();
        }
        catch
        {
            div_code = HttpContext.Current.Session["Division_Code"].ToString();
        }
        ds = get_Territory_for_Secondary(div_code);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }

    public static DataSet get_Territory_for_Secondary(string div_code)
    {
        string strQry = "";
        DataSet dsSF = null;
        DB_EReporting db = new DB_EReporting();

        strQry = "Exec Sp_get_Territory_for_Secondary '" + div_code + "'";

        try
        {
            dsSF = db.Exec_DataSet(strQry);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return dsSF;
    }
    public class GState
    {
        public string stCode { get; set; }
        public string stName { get; set; }
    }

    [WebMethod]
    public static List<GState> getState()
    {

        List<GState> Lists = new List<GState>();

        DataSet ds = new DataSet();
        Division dv = new Division();
        string div_code = "";
        try
        {
            div_code = HttpContext.Current.Session["div_code"].ToString();
        }
        catch
        {
            div_code = HttpContext.Current.Session["Division_Code"].ToString();
        }
        ds = dv.getStatePerDivision(div_code);
        if (ds.Tables.Count > 0)
        {
            string sts = ds.Tables[0].Rows[0][0].ToString();
            State st = new State();
            ds = st.getState_new(sts.TrimEnd(','));


            foreach (DataRow row in ds.Tables[0].Rows)
            {
                GState list = new GState();
                list.stCode = row["state_code"].ToString();
                list.stName = row["statename"].ToString();
                Lists.Add(list);
            }
        }
        return Lists.ToList();
    }

    //getStockist_Name


    public class GDistributor
    {
        public string stCode { get; set; }
        public string DistCode { get; set; }
        public string DistName { get; set; }
    }

    [WebMethod]
    public static List<GDistributor> getDistributor_for_primary()
    {

        List<GDistributor> Lists = new List<GDistributor>();

        DataSet ds = new DataSet();
        Stockist dv = new Stockist();

        string div_code = "";
        try
        {
            div_code = HttpContext.Current.Session["div_code"].ToString();
        }
        catch
        {
            div_code = HttpContext.Current.Session["Division_Code"].ToString();
        }

        ds = dv.getStockist_Name_Primary(div_code);


        foreach (DataRow row in ds.Tables[0].Rows)
        {
            GDistributor list = new GDistributor();
            list.stCode = row["State_Code"].ToString();
            list.DistCode = row["Stockist_Code"].ToString();
            list.DistName = row["Stockist_Name"].ToString();
            Lists.Add(list);
        }
        return Lists.ToList();
    }

    protected void groupby_SelectedIndexChanged(object sender, EventArgs e)
    {

        //gridbrand.Visible = false;
        //gridcat.Visible = false;
        //GridView1.Visible = false;




    }
    protected void btnstate_Click(object sender, EventArgs e)
    {


        BindGridd();
        //btsubmit.Visible = true;
        //btncancel.Visible = true;


    }
    protected void BindGridd()
    {

        Notice gg = new Notice();
        DataSet ff = new DataSet();

    }
    [WebMethod]
    public static string deletedata(string state_code, string Effective_Date, string stockist_code)
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

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString);
            con.Open();



            SqlCommand cmd = new SqlCommand("delete from mas_scheme  where state_code='" + State_Codee + "' and Division_Code='" + divcode + "' and  CAST(Effective_From AS DATE)='" + Effective_Date + "'  and stockist_code='" + stockist_code + "'", con);



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
			
			con.Close();


        }
        return msg;
    }
    [WebMethod]
    public static string insertdata(string Product_name, string Product_code, string Scheme, string Free, string Discount, string Package, string state_code, string Eff_Date, string stockist_code)
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

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString);
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
			
			con.Close();


        }
        return msg;
    }



    [WebMethod]
    public static string insertdatacategorywise(string category_name, string category_code, string Scheme, string Free, string Discount, string Package, string state_code, string Eff_Date, string stockist_code)
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

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString);
            con.Open();

            SqlCommand cmdp = new SqlCommand("select * from mAS_PRODUCT_DETAIL    WHERE PRODUCT_CAT_CODE='" + category_code + "' AND DIVISION_CODE='" + div_code + "' and Product_Active_Flag=0 and  Product_Type_Code!='O'  ", con);

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


			con.Close();

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

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString);
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




			con.Close();
        }
        return msg;
    }
    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {
        filldistributor_statewise();
    }
    private void filldistributor_statewise()
    {
        //Distributor.DataSource = null;
        //Distributor.Items.Clear();
        //Distributor.Items.Insert(0, new ListItem("--Select--", "0"));

        //Order sd = new Order();
        //dsSalesForce = sd.view_stockist_statewise(div_code, ddlState.SelectedValue);
        //if (dsSalesForce.Tables[0].Rows.Count > 0)
        //{
        //    Distributor.DataTextField = "Stockist_Name";
        //    Distributor.DataValueField = "Stockist_code";
        //    Distributor.DataSource = dsSalesForce;
        //    Distributor.DataBind();
        //    Distributor.Items.Insert(0, new ListItem("--Select--", "0"));


        //}
    }

    //

    public class GProduct
    {
        public string pCode { get; set; }
        public string pName { get; set; }
        public string pTypes { get; set; }
        public string Sample_Erp_Code { get; set; }
        public string Product_Sale_Unit { get; set; }
        public string product_unit { get; set; }
        public string product_Sale_unit_Code { get; set; }
        public string product_unit_Code { get; set; }
        public string product_umo_weight { get; set; }
        public string product_Default_umo { get; set; }
    }

    [WebMethod]
    //public static List<GProduct> getProduct(string Type)
    //{
    //    List<GProduct> Lists = new List<GProduct>();
    //    DataSet ds = new DataSet();
    //    string div_code = "";
    //    try
    //    {
    //        div_code = HttpContext.Current.Session["div_code"].ToString();
    //    }
    //    catch
    //    {
    //        div_code = HttpContext.Current.Session["Division_Code"].ToString();
    //    }
    //    Product prd = new Product();
    //    if (Type == "2")
    //    {
    //        ds = prd.getProd(div_code);            
    //        foreach (DataRow row in ds.Tables[0].Rows)
    //        {
    //            GProduct list = new GProduct();
    //            list.pCode = row["Product_Detail_Code"].ToString();
    //            list.pName = row["Product_Detail_Name"].ToString();
    //            list.pTypes = row["Product_Type_Code"].ToString();
    //            list.Product_Sale_Unit = row["Product_Sale_Unit"].ToString();
    //            list.product_unit = row["product_unit"].ToString();
    //            list.Sample_Erp_Code = row["Sample_Erp_Code"].ToString();
    //            Lists.Add(list);
    //        }
    //    }
    //    else
    //    {
    //        ds = prd.getProductCategory(div_code);
    //        foreach (DataRow row in ds.Tables[0].Rows)
    //        {
    //            GProduct list = new GProduct();
    //            if (row["Product_Cat_Code"].ToString() != "0")
    //            {
    //                list.pCode = row["Product_Cat_Code"].ToString();
    //                list.pName = row["Product_Cat_Name"].ToString();
    //                Lists.Add(list);
    //            }
    //        }
    //    }
    //    return Lists.ToList();
    //}

    public static List<GProduct> getProduct(string Type, string Sub_div)
    {
        List<GProduct> Lists = new List<GProduct>();
        DataSet ds = new DataSet();
        string div_code = "";
        try
        {
            div_code = HttpContext.Current.Session["div_code"].ToString();
        }
        catch
        {
            div_code = HttpContext.Current.Session["Division_Code"].ToString();
        }
        Product prd = new Product();
        if (Type == "2")
        {
            ds = getProd(div_code, Sub_div);
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                GProduct list = new GProduct();
                list.pCode = row["Product_Detail_Code"].ToString();
                list.pName = row["Product_Detail_Name"].ToString();
                list.pTypes = row["Product_Type_Code"].ToString();
                list.Product_Sale_Unit = row["Product_Sale_Unit"].ToString();
                list.product_unit = row["product_unit"].ToString();
                list.Sample_Erp_Code = row["Sample_Erp_Code"].ToString();
                list.product_Sale_unit_Code = row["Base_Unit_code"].ToString();
                list.product_unit_Code = row["Unit_code"].ToString();
                list.product_umo_weight = row["UOM_Weight"].ToString();
                list.product_Default_umo = row["Default_umo"].ToString();
                Lists.Add(list);
            }
        }
        else
        {
            ds = prd.getProductCategory(div_code);
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                GProduct list = new GProduct();
                if (row["Product_Cat_Code"].ToString() != "0")
                {
                    list.pCode = row["Product_Cat_Code"].ToString();
                    list.pName = row["Product_Cat_Name"].ToString();
                    Lists.Add(list);
                }
            }
        }
        return Lists.ToList();
    }

    public static DataSet getProd(string divcode, string sub_div)
    {
        DB_EReporting db_ER = new DB_EReporting();

        DataSet dsProCat = null;

        string strQry = " SELECT Product_Detail_Code,Product_Detail_Name,Product_Sale_Unit,Product_Type_Code,Product_Description,Unit_code,Base_Unit_code,isnull(UOM_Weight,0) UOM_Weight,isnull(Rate_UOM,0) as Default_umo, " +
                 " Product_Code_SlNo,Product_Sale_Unit,product_unit,Sample_Erp_Code,subdivision_code " +
                 " FROM  Mas_Product_Detail " +
                 " WHERE Product_Active_Flag=0 AND Division_Code= '" + divcode + "' and ('" + sub_div + "'='0' or charindex(','+'" + sub_div + "'+',',',' + subdivision_code +',')>0) " +
                 " ORDER BY 2";
        try
        {
            dsProCat = db_ER.Exec_DataSet(strQry);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return dsProCat;
    }
    public class GScheme
    {
        public string sDate { get; set; }
        public string sName { get; set; }
    }

    [WebMethod]
    public static List<GScheme> getScheme()
    {
        List<GScheme> Lists = new List<GScheme>();
        DataSet ds = new DataSet();
        string div_code = "";
        try
        {
            div_code = HttpContext.Current.Session["div_code"].ToString();
        }
        catch
        {
            div_code = HttpContext.Current.Session["Division_Code"].ToString();
        }
        Product prd = new Product();
        ds = Get_Primary_Scheme_Names(div_code);
        foreach (DataRow row in ds.Tables[0].Rows)
        {
            GScheme list = new GScheme();
            list.sDate = row["EfDate"].ToString();
            list.sName = row["Scheme_Name"].ToString();
            Lists.Add(list);
        }
        return Lists.ToList();
    }

    public static DataSet Get_Primary_Scheme_Names(string DivCode)
    {
        DB_EReporting db_ER = new DB_EReporting();

        DataSet dsAdmin = null;
        string strQry = "select convert(varchar,Effective_To,103) EfDate,Scheme_Name from Mas_Primary_Scheme where division_code='" + DivCode + "' and cast(convert(varchar,Effective_To,101)as datetime) >=cast(convert(varchar,getdate(),101)as datetime)  GROUP BY Scheme_Name,convert(varchar,Effective_To,103)";

        try
        {
            dsAdmin = db_ER.Exec_DataSet(strQry);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return dsAdmin;
    }

    public static DataSet Get_Scheme_Names(string DivCode)
    {
        DB_EReporting db_ER = new DB_EReporting();
        string strQry = string.Empty;
        DataSet dsAdmin = null;
        strQry = "select convert(varchar,Effective_To,103) EfDate,Scheme_Name,subdivision_code from mas_scheme where division_code='" + DivCode + "' and cast(convert(varchar,Effective_To,101)as datetime) >=cast(convert(varchar,getdate(),101)as datetime) GROUP BY Scheme_Name,convert(varchar,Effective_To,103),subdivision_code";

        try
        {
            dsAdmin = db_ER.Exec_DataSet(strQry);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return dsAdmin;
    }
    public class GSchemeVal
    {
        public string AutoID { get; set; }
        public string pCode { get; set; }
        public string pName { get; set; }
        public string scheme { get; set; }
        public string scheme_unit { get; set; }
        public string free { get; set; }
        public string free_Unit { get; set; }
        public string discount { get; set; }
        public string Package { get; set; }
        public string State_Code { get; set; }
        public string Stockist_Code { get; set; }
        public string schemeName { get; set; }
        public string FDate { get; set; }
        public string TDate { get; set; }
        public string Against { get; set; }
        public string AgProduct { get; set; }
        public string actFlg { get; set; }
        public string Offer_Product_Name { get; set; }
        public string Scheme_Unit { get; set; }
        public string Free_Unit { get; set; }
        public string Product_Sale_Unit { get; set; }
        public string product_unit { get; set; }
        public string Sample_Erp_Code { get; set; }
        public string offer_unit { get; set; }
        public string scheme_unit_code { get; set; }
        public string free_Unit_code { get; set; }
        public string offer_product_Unit_code { get; set; }
        public string DisType { get; set; }
		public string scheme_txt { get; set; }
        public string sch_inp { get; set; }
		public string free_in_piece { get; set; }
		public string Free_inp { get; set; }


    }

    [WebMethod]
    public static List<GSchemeVal> getSchemeVAl(string schemeName)
    {
        List<GSchemeVal> Lists = new List<GSchemeVal>();
        DataSet ds = new DataSet();
        string div_code = "";
        try
        {
            div_code = HttpContext.Current.Session["div_code"].ToString();
        }
        catch
        {
            div_code = HttpContext.Current.Session["Division_Code"].ToString();
        }
        Product prd = new Product();
        ds = prd.getprischemaval(div_code, schemeName);
        foreach (DataRow row in ds.Tables[0].Rows)
        {
            GSchemeVal list = new GSchemeVal();
            list.pCode = row["Product_Code"].ToString();
            list.pName = row["Product_Detail_Name"].ToString();
            list.scheme = row["scheme"].ToString();
            list.free = row["free"].ToString();
            list.discount = row["discount"].ToString();
            list.Stockist_Code = row["Stockist_Code"].ToString();
            list.Package = row["Package"].ToString();
            list.State_Code = row["State_Code"].ToString();
            list.schemeName = row["Scheme_Name"].ToString();
            list.Against = row["Against"].ToString();
            list.AgProduct = row["Offer_Product"] == DBNull.Value ? "0" : row["Offer_Product"].ToString() == "" ? "0" : row["Offer_Product"].ToString();
            list.actFlg = row["actFlg"].ToString();
            list.AutoID = row["autoID"].ToString();
            list.FDate = row["Effective_From"] == DBNull.Value ? "" : Convert.ToDateTime(row["Effective_From"]).ToString("dd/MM/yyyy");
            list.TDate = row["Effective_To"] == DBNull.Value ? "" : Convert.ToDateTime(row["Effective_To"]).ToString("dd/MM/yyyy");
            list.Scheme_Unit = row["Scheme_Unit"].ToString();
            list.Free_Unit = row["Free_Unit"].ToString();
            list.Product_Sale_Unit = row["Product_Sale_Unit"].ToString();
            list.product_unit = row["product_unit"].ToString();
            list.Sample_Erp_Code = row["Sample_Erp_Code"].ToString();
            list.offer_product_Unit_code = row["offer_product_unit"].ToString();
            list.free_Unit_code = row["Free_Unit"].ToString();
            list.scheme_unit_code = row["Scheme_Unit"].ToString();
            list.offer_unit = row["Offer_Product"].ToString();
            list.Offer_Product_Name = row["Offer_Product_Name"].ToString();
            list.DisType = row["Discount_Type"].ToString();
			list.sch_inp = row["Scheme_Inp"].ToString();
			list.Free_inp = row["Free_Inp"].ToString();
            Lists.Add(list);
        }
        return Lists.ToList();
    }

    public static DataSet getSecSchemaval(string DivCode, string schemName, string subDiv)
    {
        DB_EReporting db_ER = new DB_EReporting();

        DataSet dsAdmin = null;
        string strQry = "getschemaval '" + DivCode + "','" + schemName + "','" + subDiv + "'";

        try
        {
            dsAdmin = db_ER.Exec_DataSet(strQry);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return dsAdmin;
    }


    [WebMethod]
    public static string SaveScheme(string Data, string DistCode, string StateCode, string FDate, string TDate, string SchemeName, string Types, string insertType)
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

        var items = JsonConvert.DeserializeObject<List<GSchemeVal>>(Data);
        List<Catprod> catprod = new List<Catprod>();

        Product prd = new Product();

        DataSet ds_EReport = new DataSet();

        if (Types == "1")
        {
            string sxml = "<ROOT>";
            DataSet dsPro = null;

            for (int i = 0; i < items.Count; i++)
            {
                dsPro = prd.getProductForCategory(div_code, items[i].pCode.ToString(), "");
                string ipcode = items[i].pCode.ToString();
                foreach (DataRow drr in dsPro.Tables[0].Rows)
                {
                    Catprod asd = new Catprod();
                    asd.pCode = drr["Product_Detail_Code"].ToString();
                    asd.pName = drr["Product_Detail_Name"].ToString();


                    catprod.Add(asd);
                }
                for (int k = 0; k < catprod.Count; k++)
                {
                    if (items[i].scheme != "0")
                    {
                        sxml += "<ASSD PCode=\"" + catprod[k].pCode + "\" PName=\"" + catprod[k].pName + "\" Sch=\"" + items[i].scheme + "\" Sch_unit=\"" + items[i].scheme_unit + "\"  Free=\"" + items[i].free + "\" free_unit=\"" + items[i].free_Unit + "\" Dcount=\"" + items[i].discount + "\" Pkg=\"" + items[i].Package + "\" Ag=\"" + items[i].Against + "\" OProd=\"" + items[i].AgProduct + "\" actFlg=\"" + items[i].actFlg + "\" OPName=\"" + items[i].Offer_Product_Name + "\" AID=\"" + items[i].AutoID + "\" Distype=\"" + items[i].DisType + "\" sName=\"" + SchemeName + "\" sch_txt=\"" + items[i].scheme_txt + "\" Fre_in_piece=\"" + items[i].free_in_piece + "\" />";

                    }
                }
                catprod.Clear();
            }

            sxml += "</ROOT>";
            DateTime dt1 = DateTime.ParseExact(FDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime dt2 = DateTime.ParseExact(TDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            string consString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString;
            using (SqlConnection con = new SqlConnection(consString))
            {
                using (SqlCommand cmd = con.CreateCommand())
                {

                    cmd.CommandType = CommandType.StoredProcedure; ;
                    cmd.CommandText = "insertMasPriScheme";

                    SqlParameter[] parameters = new SqlParameter[]
                            {
                                        new SqlParameter("@sxml", sxml),
                                        new SqlParameter("@fdt", dt1.ToString("yyyy/MM/dd")),
                                        new SqlParameter("@tdt", dt2.ToString("yyyy/MM/dd")),
                                        new SqlParameter("@TYPE", insertType),
                                        new SqlParameter("@distcode", DistCode),
                                        new SqlParameter("@stcode", StateCode),
                                        new SqlParameter("@Div", div_code)

                            };
                    cmd.Parameters.AddRange(parameters);

                    try
                    {
                        if (con.State != ConnectionState.Open)
                        {
                            con.Open();
                        }
                        cmd.ExecuteNonQuery();
                    }
                    catch
                    {
                        throw;
                    }
                }
            }
            return "Success";

        }
        else
        {
            string sxml = "<ROOT>";
            DateTime dt1 = DateTime.ParseExact(FDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime dt2 = DateTime.ParseExact(TDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            if (insertType == "1")
            {


                for (int i = 0; i < items.Count; i++)
                {
                    if (items[i].scheme != "0")
                    {

                        sxml += "<ASSD PCode=\"" + items[i].pCode + "\" PName=\"" + items[i].pName + "\" Sch=\"" + items[i].scheme + "\" Sch_unit=\"" + items[i].scheme_unit + "\" Free=\"" + items[i].free + "\" free_unit=\"" + items[i].free_Unit + "\" Offer_unit=\"" + items[i].offer_unit + "\"  Dcount=\"" + items[i].discount + "\" Pkg=\"" + items[i].Package + "\" Ag=\"" + items[i].Against + "\" OProd=\"" + items[i].AgProduct + "\" actFlg=\"" + items[i].actFlg + "\" OPName=\"" + items[i].Offer_Product_Name + "\" AID=\"" + items[i].AutoID + "\" sName=\"" + SchemeName + "\" scheme_unit_code=\"" + items[i].scheme_unit_code + "\" free_unit_code=\"" + items[i].free_Unit_code + "\" offer_product_unit_code=\"" + items[i].offer_product_Unit_code + "\" Distype=\"" + items[i].DisType + "\" sch_txt=\"" + items[i].scheme_txt + "\" Fre_in_piece=\"" + items[i].free_in_piece + "\" />";

                    }
                }
            }
            else
            {
                for (int i = 0; i < items.Count; i++)
                {
                    if (items[i].scheme != "0")
                    {
                        sxml += "<ASSD PCode=\"" + items[i].pCode + "\" PName=\"" + items[i].pName + "\" Sch=\"" + items[i].scheme + "\" Sch_unit=\"" + items[i].scheme_unit + "\" Free=\"" + items[i].free + "\" free_unit=\"" + items[i].free_Unit + "\" Offer_unit=\"" + items[i].offer_unit + "\"  Dcount=\"" + items[i].discount + "\" Pkg=\"" + items[i].Package + "\" Ag=\"" + items[i].Against + "\" OProd=\"" + items[i].AgProduct + "\" actFlg=\"" + items[i].actFlg + "\" OPName=\"" + items[i].Offer_Product_Name + "\" AID=\"" + items[i].AutoID + "\" sName=\"" + SchemeName + "\" scheme_unit_code=\"" + items[i].scheme_unit_code + "\" free_unit_code=\"" + items[i].free_Unit_code + "\" offer_product_unit_code=\"" + items[i].offer_product_Unit_code + "\" Distype=\"" + items[i].DisType + "\" sch_txt=\"" + items[i].scheme_txt + "\" Fre_in_piece=\"" + items[i].free_in_piece + "\" />";
                    }
                }
            }
            sxml += "</ROOT>";

            int u = insertSchemePri(sxml, dt1.ToString("yyyy/MM/dd"), dt2.ToString("yyyy/MM/dd"), insertType, DistCode, StateCode, div_code);


            //string consString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString;
            //using (SqlConnection con = new SqlConnection(consString))
            //{
            //    using (SqlCommand cmd = con.CreateCommand())
            //    {

            //        cmd.CommandType = CommandType.StoredProcedure;
            //        cmd.CommandText = "insertMasPriScheme";

            //        SqlParameter[] parameters = new SqlParameter[]
            //        {
            //                            new SqlParameter("@sxml", sxml),
            //                            new SqlParameter("@fdt", dt1.ToString("yyyy/MM/dd")),
            //                            new SqlParameter("@tdt", dt2.ToString("yyyy/MM/dd")),
            //                            new SqlParameter("@TYPE", insertType),
            //                            new SqlParameter("@distcode", DistCode),
            //                            new SqlParameter("@stcode", StateCode),
            //                            new SqlParameter("@Div", div_code)

            //        };
            //        cmd.Parameters.AddRange(parameters);

            //        try
            //        {
            //            if (con.State != ConnectionState.Open)
            //            {
            //                con.Open();
            //            }
            //            cmd.ExecuteNonQuery();
            //        }
            //        catch
            //        {
            //            throw;
            //        }
            //    }
            //}
            return "Success";
        }

    }


    public static int insertSchemePri(string sxml, string fromDate , string ToDate, string insertType, string DistCode, string StateCode, string div_code)
    {
        int iReturn = -1;
        string strQry;
        try
        {
            DB_EReporting db = new DB_EReporting();
            strQry = "EXEC insertMasPriScheme '" + sxml + "','" + fromDate + "','" + ToDate + "','" + insertType + "','" + DistCode + "','" + StateCode + "','" + div_code + "'";
            iReturn = db.ExecQry(strQry);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return iReturn;
    }




    [WebMethod]
    public static string DeleteScheme(string schemeName)
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
        Product prd = new Product();
        int cnt = prd.Delete_priScheme_Values(div_code, schemeName);
        if (cnt > 0)
        {
            return "Success";
        }
        else
        {
            return "Error Delete Scheme";
        }
    }

    public class Catprod
    {
        public string pCode { get; set; }

        public string pName { get; set; }
    }

	protected void lnkDownload_Click(object sender, EventArgs e)
    {
        //DataTable dt=new DataTable();
        //dt.Columns.AddRange(new DataColumn[8] { new DataColumn("ProductCode", typeof(string)),
        //        new DataColumn("ProductName", typeof(string)),
        //        new DataColumn("Scheme", typeof(string)),
        //        new DataColumn("Free", typeof(string)),
        //        new DataColumn("Discount", typeof(string)),
        //        new DataColumn("Package",typeof(DateTime)),
        //        new DataColumn("Against",typeof(string)),
        //        new DataColumn("OfferProduct",typeof(string))});
        //using (XLWorkbook wb = new XLWorkbook())
        //{
        //    wb.Worksheets.Add(dt, "Product List");
        //    Response.Clear();
        //    Response.Buffer = true;
        //    Response.Charset = "";
        //    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        //    Response.AddHeader("content-disposition", "attachment;filename=Scheme_Master.xlsx");
        //    using (MemoryStream MyMemoryStream = new MemoryStream())
        //    {
        //        wb.SaveAs(MyMemoryStream);
        //        MyMemoryStream.WriteTo(Response.OutputStream);
        //        Response.Flush();
        //        Response.End();
        //    }
        //}


        //DataTable dt = new DataTable();
        //try
        //{
        //    div_code = HttpContext.Current.Session["div_code"].ToString();
        //}
        //catch
        //{
        //    div_code = HttpContext.Current.Session["Division_Code"].ToString();
        //}

        //string sub_div_code = hidden_div.Value;
        //var sub_div_code1 = "";
        //if (sub_div_code == "")
        //{
        //    sub_div_code1 = "0";
        //}
        //else
        //{
        //    sub_div_code1 = sub_div_code;
        //}

        //DataSet ds = new DataSet();
        //ds = getProd(div_code.TrimEnd(','), sub_div_code1);

        //ds.Tables[0].Columns.Remove("Product_Sale_Unit");
        //ds.Tables[0].Columns.Remove("Product_Type_Code");
        //ds.Tables[0].Columns.Remove("Product_Description");
        //ds.Tables[0].Columns.Remove("Product_Code_SlNo");
        //ds.Tables[0].Columns.Remove("Product_Sale_Unit1");
        //ds.Tables[0].Columns.Remove("product_unit");
        //ds.Tables[0].Columns.Remove("Sample_Erp_Code");
        //ds.Tables[0].Columns.Remove("subdivision_code");
        //ds.Tables[0].Columns["Product_Detail_Code"].ColumnName = "ProductCode";
        //ds.Tables[0].Columns["Product_Detail_Name"].ColumnName = "ProductName";
        //ds.Tables[0].Columns.Add("Scheme");
        //ds.Tables[0].Columns.Add("Free");
        //ds.Tables[0].Columns.Add("Discount");
        //ds.Tables[0].Columns.Add("Package");
        //ds.Tables[0].Columns.Add("Against");
        //ds.Tables[0].Columns.Add("OfferProduct");

        //using (XLWorkbook wb = new XLWorkbook())
        //{
        //    var ws = wb.Worksheets.Add(ds.Tables[0]);
        //    Response.Clear();
        //    Response.Buffer = true;
        //    Response.Charset = "";
        //    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        //    Response.AddHeader("content-disposition", "attachment;filename=Primary_Scheme_Product.xlsx");
        //    using (MemoryStream MyMemoryStream = new MemoryStream())
        //    {
        //        wb.SaveAs(MyMemoryStream);
        //        MyMemoryStream.WriteTo(Response.OutputStream);
        //        Response.Flush();
        //        Response.End();
        //    }
        //}



        DataTable dt = new DataTable();
        try
        {
            div_code = HttpContext.Current.Session["div_code"].ToString();
        }
        catch
        {
            div_code = HttpContext.Current.Session["Division_Code"].ToString();
        }

        string sub_div_code = hidden_div.Value;
        if(sub_div_code=="")
        {
            sub_div_code = "0";
        }
        dt = get_Product_for_Secondary_scheme(sub_div_code, div_code.TrimEnd(','));
       
        dt.Columns["Product_Detail_Code"].ColumnName = "ProductCode";
        dt.Columns["Product_Detail_Name"].ColumnName = "ProductName";
        dt.Columns.Add("Against");
        dt.Columns.Add("Offer_Product_code");
        dt.Columns.Add("OfferProduct");


        using (XLWorkbook wb = new XLWorkbook())
        {
            var ws = wb.Worksheets.Add(dt, "Product List");
            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=Primary_Scheme_Product.xlsx");
            using (MemoryStream MyMemoryStream = new MemoryStream())
            {
                wb.SaveAs(MyMemoryStream);
                MyMemoryStream.WriteTo(Response.OutputStream);
                Response.Flush();
                Response.End();
            }
        }
    }

    public static DataTable get_Product_for_Secondary_scheme(string sub_div_code, string Div_Code)
    {
        DataTable dt = null;
        DB_EReporting db_ER = new DB_EReporting();

        string strQry = "Exec Sp_get_Product_for_Secondary_scheme '" + Div_Code + "','" + sub_div_code + "'";

        try
        {
            dt = db_ER.Exec_DataTable(strQry);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return dt;
    }

    [WebMethod(EnableSession = true)]
    public static string getreate(string Div_Code)
    {
        string sf = HttpContext.Current.Session["Sf_Code"].ToString();
        DataSet ds = new DataSet();
        StockistMaster sm = new StockistMaster();
        ds = getratebystk(Div_Code.TrimEnd(','), sf, "0");
        return JsonConvert.SerializeObject(ds.Tables[0]);

    }

    [WebMethod(EnableSession = true)]
    public static string validateScheme(string From_date, string State_Code, string InsertType, string To_date, string Scheme_name, string Dist_Code)
    {

        DateTime dt2 = DateTime.ParseExact(From_date, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        DateTime dt1 = DateTime.ParseExact(To_date, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        string Stockist_Code = HttpContext.Current.Session["Sf_Code"].ToString();
        string PDiv_code = HttpContext.Current.Session["div_code"].ToString();
        DataSet ds = new DataSet();
        SqlConnection con = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString);
        con.Open();
        SqlCommand cmd = new SqlCommand("exec sp_schem_validate '" + dt2.ToString("yyyy-MM-dd") + "','" + dt1.ToString("yyyy-MM-dd") + "','" + State_Code + "','" + InsertType + "','" + PDiv_code + "','" + Scheme_name + "','" + Dist_Code + "'", con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(ds);
        con.Close();
        return JsonConvert.SerializeObject(ds.Tables[0]);

    }

    [WebMethod(EnableSession = true)]
    public static string Validate_Scheme_Name(string Scheme_Name)
    {
        string Div_Code = HttpContext.Current.Session["div_code"].ToString();
        DataSet ds = new DataSet();
        SqlConnection con = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString);
        con.Open();
        SqlCommand cmd = new SqlCommand("select* from Mas_Primary_Scheme where Scheme_Name = '" + Scheme_Name + "' and Division_Code = '" + Div_Code + "'", con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(ds);
        con.Close();
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }

    [WebMethod(EnableSession = true)]
    public static string Get_Product_unit()
    {
        string div_code = HttpContext.Current.Session["div_code"].ToString();
        string stk = HttpContext.Current.Session["Sf_Code"].ToString();
        div_code = div_code.TrimEnd(',');
        DataSet ds = new DataSet();
        StockistMaster sm = new StockistMaster();
        ds = sm.gets_Product_unit_details(div_code, stk);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }

    [WebMethod(EnableSession = true)]
    public static string get_view_unit()
    {
        DataSet ds = new DataSet();
        string div_code = HttpContext.Current.Session["div_code"].ToString();
        div_code = div_code.TrimEnd(',');
        StockistMaster sm = new StockistMaster();
        ds = get_view_unit_details(div_code);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }

    public static DataSet get_view_unit_details(string Div_Code)
    {
        DataSet ds = null;
        DBase_EReport.DB_EReporting db_ER = new DBase_EReport.DB_EReporting();

        string strQry = "select * from vwMas_MUnitDets where Division_Code = " + Div_Code + "";

        try
        {
            ds = db_ER.Exec_DataSet(strQry);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return ds;
    }
	
	     public static DataSet getratebystk(string Div_Code, string Stockist_Code, string State)
        {

            DB_EReporting db_ER = new DB_EReporting();
            DataSet ds = null;

            try
            {
                //strQry = "exec getrate_for_primary '" + Div_Code + "','" + Stockist_Code + "','" + State + "'";				
				string strQry = "exec getrate_new_Admin_Primary_Scheme '" + Div_Code + "','" + Stockist_Code + "','" + State + "'";				

                ds = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return ds;

        }

}