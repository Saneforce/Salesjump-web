using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
using Bus_Objects;
using System.Web.Services;
using System.Web.Script.Serialization;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Text;
using System.Transactions;
using System.Data.SqlClient;

public partial class MasterFiles_Sec_Entry : System.Web.UI.Page
{
    #region "Declaration"
    DataSet dsStockist = null;
    DataSet dsSalesForce = null;
    DataSet dsdistributor = null;
    DataSet dsTerritory = null;
    DataSet dsTerritory1 = null;
    DataSet dsTerritory2 = null;
    string sf_code = string.Empty;
    string sf_type = string.Empty;
    string Division_code = string.Empty;
    string Territory_Type = string.Empty;
    string Territory_Name = string.Empty;
    string Territory_SName = string.Empty;
    string Alias_Name = string.Empty;
    string terr_code = string.Empty;
    string Territory_Code = string.Empty;
    string dis_code = string.Empty;
    string dis_name = string.Empty;
    string distributor_name = string.Empty;
    string sf_code_name = string.Empty;
    int iIndex = -1;
    int iReturn = -1;
    DataSet dsSf_or = null;
    #endregion
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
        sf_code = Session["sf_code"].ToString();
        Division_code = Session["Div_code"].ToString();

        DCREntryBL BL = new DCREntryBL();
        DCREntry.SFDetails SFDet = BL.get_SFDetails(sf_code);
        hdnSfcode.Value = sf_code;

        SFInf.InnerHtml = SFDet.SFName;
        string mr_sf_code = SFDet.SFCode;
        string state_code= Convert.ToString(SFDet.State);
        int sff_type = SFDet.SFtype;
        Session["state"] = state_code;
        Session["mr_sf_code"] = mr_sf_code;
        Session["mr_sff_type"] = sff_type;
        DCREntry.DCRSetup Setup = BL.getSetups(SFDet);
        string Today = DateTime.Today.ToString("yyyy-MM-dd");
        //string Today = "2019-04-11";
        var DtDet = BL.GetDCRDtDet(SFDet, Setup);
        System.IO.Path.GetFileName(Request.Url.AbsolutePath);
        //DtInf.InnerHtml = DateTime.Today.ToString("dd/MM/yyyy") + " - " + DateTime.Today.DayOfWeek.ToString() + " " + ((DtDet.DTRem != "") ? " - <span class='stat" + DtDet.Type + "'>" + DtDet.DTRem + "</span>" : "");
        DtInf.InnerHtml = DtDet.DCR_Date.ToString("dd/MM/yyyy") + " - " + DtDet.DCR_Date.DayOfWeek.ToString() + " " + ((DtDet.DTRem != "") ? " - <span class='stat" + DtDet.Type + "'>" + DtDet.DTRem + "</span>" : "");
        HdnDate.Value = DtDet.DCR_Date.ToString("yyyy-MM-dd");
        


        Session["Dcr_Date"] = HdnDate.Value;

        if (Convert.ToDateTime(Today) >= Convert.ToDateTime(HdnDate.Value))
        {

        }
        else
        {
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Order Date Exits!!!');window.location='../../../../DashBoard_MR.aspx';</script>");
        }

        //if (Convert.ToDateTime(HdnDate.Value) > Convert.ToDateTime(Today))
        //{
 
        //}



        if (!Page.IsPostBack)
        {


            filldis();
            GetTerritoryName();
            GetWorktypeName();
            theDiv.Visible = false;

            if (sf_type == "2")
            {
                GetHQName();
                ddl_HQ.Visible = true;
                theDiv.Visible = true;
            }

            if (Territory_Code != "" && Territory_Code != null)
            {

                Territory sd = new Territory();
                dsTerritory = sd.get_Territory(sf_code, Territory_Code, Division_code);
                if (dsTerritory.Tables[0].Rows.Count > 0)
                {

                    ddlTerritoryName.SelectedValue = dsTerritory.Tables[0].Rows[0].ItemArray.GetValue(6).ToString();
                    distributor_name = dsTerritory.Tables[0].Rows[0].ItemArray.GetValue(7).ToString();
                    sf_code_name = dsTerritory.Tables[0].Rows[0].ItemArray.GetValue(5).ToString();

                    string st = dsTerritory.Tables[0].Rows[0].ItemArray.GetValue(8).ToString();
                    string ddlst = dsTerritory.Tables[0].Rows[0].ItemArray.GetValue(9).ToString();



                }
            }
            else
            {


            }


        }
        if (Session["sf_type"].ToString() == "1")
        {
            terr_code = Convert.ToString(Request.QueryString["Territory_Code"]); ;
            sf_code = Session["sf_code"].ToString();

        }
        else
        {
            terr_code = Convert.ToString(Request.QueryString["Territory_Code"]); ;
            sf_code = Session["sf_code"].ToString();

            ViewTerritory();

        }



    }

   

    private void GetWorktypeName()
    {



        if (sf_code != "0")
        {
            Territorys dv = new Territorys();
            dsStockist = dv.getSF_Code_Wtyp(Division_code, sf_code);
            if (dsStockist.Tables[0].Rows.Count > 0)
            {
                ddl_worktype.DataTextField = "name";
                ddl_worktype.DataValueField = "id";
                //ddl_worktype.DataValueField = "FWFlg";

                ddl_worktype.DataSource = dsStockist;
                ddl_worktype.DataBind();

            }
        }

    }

     private void GetHQName()
    {
       
      

        if (sf_code != "0")
        {
            Territorys dv = new Territorys();
            dsStockist = dv.getSF_Code_MR(sf_code,Division_code);
            if (dsStockist.Tables[0].Rows.Count > 0)
            {
                ddl_HQ.DataTextField = "Sf_Name";
                ddl_HQ.DataValueField = "Sf_Code";
                //ddl_worktype.DataValueField = "FWFlg";

                ddl_HQ.DataSource = dsStockist;
                ddl_HQ.DataBind();
                ddl_HQ.Items.Insert(0, new ListItem("--Select--", "0"));
              
            }
        }

    }

    protected void ddl_worktype_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddl_worktype.SelectedIndex == 0)
        {
            ter.Visible = true;
            ddlTerritoryName.Visible = true;
            Panel1.Visible = true;
            GetTerritoryName();
            Panel2.Visible = false;
            ddl_HQ.Visible = true;
        }
        else
        {
            ter.Visible = false;
            ddlTerritoryName.Visible = false;
            Panel1.Visible = false;
            Panel2.Visible = true;
            ddl_HQ.Visible = false;

        }

    }





    protected void btnSave_Click(object sender, EventArgs e)
    {




    }
    public void Clear()
    {

        ddlTerritoryName.SelectedIndex = -1;



    }
    private void ViewTerritory()
    {

        Territory terr = new Territory();
        dsTerritory = terr.getTerritory_dm(sf_code);
        if (dsTerritory.Tables[0].Rows.Count > 0)
        {

            dis_name = dsTerritory.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
            Territory_Type = dsTerritory.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();

        }
    }
    private void filldis()
    {

        ddl_dis.DataSource = null;
        ddl_dis.Items.Clear();
        ddl_dis.Items.Insert(0, new ListItem("--Select--", "0"));

        if (sf_code != "0")
        {
            Order sd = new Order();
            dsSalesForce = sd.view_stockist_feildforcewise(Division_code, sf_code, "");
            if (dsSalesForce.Tables[0].Rows.Count > 0)
            {
                ddl_dis.DataTextField = "Stockist_Name";
                ddl_dis.DataValueField = "Stockist_Code";
                ddl_dis.DataSource = dsSalesForce;
                ddl_dis.DataBind();
                ddl_dis.Items.Insert(0, new ListItem("--Select--", "0"));

            }
        }
    }

    //protected void ddl_dis_SelectedIndexChanged(object sender, EventArgs e)
    //{
        

    //    if (ddl_dis.SelectedValue != "0")
    //    {
    //        SalesForce sd = new SalesForce();
    //        dsSalesForce = sd.GetRouteName_Customer(ddl_dis.SelectedValue, Division_code);
    //        if (dsSalesForce.Tables[0].Rows.Count > 0)
    //        {
    //            ddlTerritoryName.DataTextField = "Territory_Name";
    //            ddlTerritoryName.DataValueField = "Territory_Code";
    //            ddlTerritoryName.DataSource = dsSalesForce;
    //            ddlTerritoryName.DataBind();



    //        }
    //    }


    //    else
    //    {
    //        ddlTerritoryName.DataSource = null;
    //        ddlTerritoryName.Items.Clear();
    //        ddlTerritoryName.Items.Insert(0, new ListItem("--Select--", "0"));

    //    }

    //}

    protected void btnClear_Click(object sender, EventArgs e)
    {
        Clear();

    }


    private void GetTerritoryName()
    {
        // Stockist sk = new Stockist();
        // dsStockist = sk.getTer_Name(Division_code);
        ddlTerritoryName.DataSource = null;
          ddlTerritoryName.Items.Clear();
        ddlTerritoryName.Items.Insert(0, new ListItem("--Select--", "0"));

        if (sf_code != "0")
        {
            Territorys dv = new Territorys();
            dsStockist = dv.getSF_Code_Route(Division_code,sf_code);
            if (dsStockist.Tables[0].Rows.Count > 0)
            {
                ddlTerritoryName.DataTextField = "Territory_Name";
                ddlTerritoryName.DataValueField = "Territory_Code";

                ddlTerritoryName.DataSource = dsStockist;
                ddlTerritoryName.DataBind();
                ddlTerritoryName.Items.Insert(0, new ListItem("--Select--", "0"));
            }
        }

    }


    [WebMethod()]
    public static string GetCheckBoxDetails(string TransSlNo)
    {


        List<CheckBoxItem> chkListAppointments = GetControlDetailsDB(TransSlNo);
        JavaScriptSerializer ser = new JavaScriptSerializer();
        return ser.Serialize(chkListAppointments);
    }

    private static List<CheckBoxItem> GetControlDetailsDB(string TransSlNo)
    {

        List<CheckBoxItem> lst = new List<CheckBoxItem>();
        DataTable dt = new DataTable();
        SubDivision dv = new SubDivision();
        dt = dv.getListedDr_new_tb(TransSlNo);// GetDataTable("location");
        foreach (DataRow dr in dt.Rows)
        {
            lst.Add(new CheckBoxItem { Name = Convert.ToString(dr["ListedDr_Name"]), Value = Convert.ToString(dr["ListedDrCode"]), address = Convert.ToString(dr["Address"]), Mobile = Convert.ToString(dr["Mobile_No"]), Remark = Convert.ToString("") });
        }
        return lst;

    }

    public class CheckBoxItem
    {
        public string Name { get; set; }
        public string Value { get; set; }

        public string address { get; set; }

        public string Mobile { get; set; }

        public string Remark { get; set; }
    }

    [WebMethod()]
    public static string GetCheckBoxDetails1(string TransSlNo)
    {


        List<CheckBoxItem1> chkListAppointments1 = GetControlDetailsDB1(TransSlNo);
        JavaScriptSerializer ser1 = new JavaScriptSerializer();
        return ser1.Serialize(chkListAppointments1);
    }

    private static List<CheckBoxItem1> GetControlDetailsDB1(string TransSlNo)
    {
        string div_code = "";
        string sf_Code = "";
        try
        {
            div_code = HttpContext.Current.Session["div_code"].ToString();

        }
        catch
        {
            div_code = HttpContext.Current.Session["Division_Code"].ToString();
        }

        sf_Code = HttpContext.Current.Session["SF_Code"].ToString();

        List<CheckBoxItem1> lst1 = new List<CheckBoxItem1>();
        DataTable dt1 = new DataTable();
        Order sd = new Order();
        dt1 = sd.view_stockist_Pri(div_code, sf_Code, ""); ;// GetDataTable("location");
        foreach (DataRow dr1 in dt1.Rows)
        {
            lst1.Add(new CheckBoxItem1 { Name1 = Convert.ToString(dr1["Stockist_Name"]), Value1 = Convert.ToString(dr1["Stockist_Code"]), address1 = Convert.ToString(dr1["Stockist_Address"]), Mobile1 = Convert.ToString(dr1["Stockist_Mobile"]), Remark1 = Convert.ToString("") });
        }
        return lst1;

    }

    public class CheckBoxItem1
    {
        public string Name1 { get; set; }
        public string Value1 { get; set; }

        public string address1 { get; set; }

        public string Mobile1 { get; set; }

        public string Remark1 { get; set; }
        
    }

    [WebMethod(EnableSession = true)]
    public static GetDatas[] GetData(string TransSlNo)
    {
        string div_code = "";
        string sf_Code = "";
        try
        {
            div_code = HttpContext.Current.Session["div_code"].ToString();

        }
        catch
        {
            div_code = HttpContext.Current.Session["Division_Code"].ToString();
        }

        sf_Code = HttpContext.Current.Session["SF_Code"].ToString();



        List<GetDatas> empList = new List<GetDatas>();

        Product dc = new Product();
        DataSet dsSf_or = dc.getProd_Cus(div_code, TransSlNo);
        foreach (DataRow row in dsSf_or.Tables[0].Rows)
        {
            GetDatas emp = new GetDatas();
            emp.ListedDrCode = row["ListedDrCode"].ToString();

        }
        return empList.ToArray();
    }

    public class GetDatas
    {
        public string ListedDrCode { get; set; }


    }

    public class Products
    {
        public string pName { get; set; }
        public string pCode { get; set; }
        public string pUOM { get; set; }
        public string pUOM_Name { get; set; }
        public string Con_unit { get; set; }
    }


    [WebMethod(EnableSession = true)]
    public static Products[] GetProduct(string TransSlNo)
    {
        string PDiv_code = HttpContext.Current.Session["div_code"].ToString();
        string state_code = HttpContext.Current.Session["state"].ToString();
        List<Products> product = new List<Products>();
        DataSet dsProduct = null;
        Product p = new Product();

        dsProduct = p.getProductRate(state_code,PDiv_code.TrimEnd(','));
        if (dsProduct != null)
        {
            foreach (DataRow row in dsProduct.Tables[0].Rows)
            {
                Products pro = new Products();
                pro.pName = row["product_Detail_Name"].ToString();
                pro.pCode = row["product_Detail_Code"].ToString();
                pro.pUOM = row["RP_Base_Rate"].ToString();
                pro.pUOM_Name = row["product_netwt"].ToString();
                pro.Con_unit = row["Sample_Erp_Code"].ToString();
                //pro.pUMO = row["Stockist_Name"].ToString();
                product.Add(pro);
            }
        }
        return product.ToArray();
    }


    public class Products1
    {
        public string pName1 { get; set; }
        public string pCode1 { get; set; }
        public string pUOM1 { get; set; }
        public string pUOM_Name1 { get; set; }
        public string Con_unit1 { get; set; }
    }


    [WebMethod(EnableSession = true)]
    public static Products1[] GetProduct1(string TransSlNo)
    {
        string PDiv_code = HttpContext.Current.Session["div_code"].ToString();
        string state_code = HttpContext.Current.Session["state"].ToString();
        List<Products1> product1 = new List<Products1>();
        DataSet dsProduct1 = null;
        Product p = new Product();

        dsProduct1 = p.getProductRate(state_code, PDiv_code.TrimEnd(','));
        if (dsProduct1 != null)
        {
            foreach (DataRow row in dsProduct1.Tables[0].Rows)
            {
                Products1 pro1 = new Products1();
                pro1.pName1 = row["product_Detail_Name"].ToString();
                pro1.pCode1 = row["product_Detail_Code"].ToString();
                pro1.pUOM1 = row["DP_Base_Rate"].ToString();
                pro1.pUOM_Name1 = row["DP_Case_Rate"].ToString();
                pro1.Con_unit1 = row["Sample_Erp_Code"].ToString();
                //pro.pUMO = row["Stockist_Name"].ToString();
                product1.Add(pro1);
            }
        }
        return product1.ToArray();
    }

    //final Save Methods

    public class JSonHelper
    {
        public string ConvertObjectToJSon<T>(T obj)
        {
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
            MemoryStream ms = new MemoryStream();
            ser.WriteObject(ms, obj);
            string jsonString = Encoding.UTF8.GetString(ms.ToArray());
            ms.Close();
            return jsonString;

        }
        public T ConverJSonToObject<T>(string jsonString)
        {
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T));
            MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonString));
            T obj = (T)serializer.ReadObject(ms);
            return obj;
        }
    }

    public class MainTransSP
    {
        public List<Trans_Sec> TransS = new List<Trans_Sec>();
        public List<Trans_Pri> TransP = new List<Trans_Pri>();
    }

    public class Trans_Sec
    {
        public string Value { get; set; }
        public string Name { get; set; }
        public string Mobile { get; set; }
        public string address { get; set; }
        public string Remark { get; set; }
        

        public List<product> Product { get; set; }
        public Trans_Sec()
        {
            Product = new List<product>();
        }
    }

    public class Trans_Pri
    {

        public string Value1 { get; set; }
        public string Name1 { get; set; }
        public string Mobile1 { get; set; }
        public string address1 { get; set; }
        public string Remark1 { get; set; }


        public List<product1> product1 { get; set; }
        public Trans_Pri()
        {
            product1 = new List<product1>();
        }
    }

    public class product1
    {
        public string pCode12 { get; set; }
        public string pName12 { get; set; }

        public string prate12 { get; set; }

        public string pqty { get; set; }

        public string cqty { get; set; }

        public string pval { get; set; }

        public string Nval { get; set; }

        public string TwnCd { get; set; }

        public string TwnNw { get; set; }

        public string DisNm { get; set; }

        public string DisCd { get; set; }


    }

    public class product
    {
        public string pCode { get; set; }
        public string pName { get; set; }

        public string prate { get; set; }

        public string disco { get; set; }

        public string free { get; set; }

        public string pqty { get; set; }

        public string pval { get; set; }

        public string Nval { get; set; }

        public string TwnCd { get; set; }

        public string TwnNw { get; set; }

        public string DisNm { get; set; }

        public string DisCd { get; set; }


    }

    [WebMethod(EnableSession = true)]
    public static string SaveDate(string data)
    {
        string ARCd = string.Empty;
        string SF = string.Empty;
        string STy = string.Empty;
        string ADt = string.Empty;
        string TwnNw = string.Empty;
        int Wtyp = 0;
        string TwnCd = string.Empty;
        string DisNm = string.Empty;
        string DisCd = string.Empty;
        int div = 0;
        string Rmks = string.Empty;
        string SysIP = string.Empty;
        string ETyp = string.Empty;
        ETyp = "Web";

        string Div_Code = HttpContext.Current.Session["div_code"].ToString();
        string SF_Code = HttpContext.Current.Session["mr_sf_code"].ToString();
        string SFf_type = HttpContext.Current.Session["mr_sff_type"].ToString();

        string date = DateTime.Today.ToString("yyyy-MM-dd");
        ADt = HttpContext.Current.Session["Dcr_Date"].ToString();

        string dt = Convert.ToDateTime(ADt).ToString("yyyy-MM-dd");

        MainTransSP sp = new MainTransSP();
        JSonHelper helper = new JSonHelper();
        sp = helper.ConverJSonToObject<MainTransSP>(data);
        string msg = string.Empty;
        string Remark = string.Empty;
        string Remark1 = string.Empty;
        int iReturn = -1;

        Product prd = new Product();
        using (var scope = new TransactionScope())
        {
            try
            {

                for (int i = 0; i < sp.TransS.Count; i++)
                {
                    string Retail_code = sp.TransS[i].Value.ToString();
                    string Retail_Name = sp.TransS[i].Name.ToString();
                    Remark = sp.TransS[i].Remark.ToString();
                    int Prod_qty = 0;
                    decimal Prod_val = 0;
                    decimal Prod_Rate = 0;
                    decimal net_val = 0;
                    decimal Prod_dis = 0;
                    decimal Prod_free = 0;
                    string pProd = string.Empty;
                    string npProd = string.Empty;
                    string @OrdDet = "<ROOT>";
                    for (int j = 0; j < sp.TransS[i].Product.Count; j++)
                    {
                        string Prod_code = sp.TransS[i].Product[j].pCode.ToString();
                        string Prod_Name = sp.TransS[i].Product[j].pName.ToString();
                        Prod_Rate += Convert.ToDecimal(sp.TransS[i].Product[j].prate.ToString());
                        Prod_dis += Convert.ToDecimal(sp.TransS[i].Product[j].disco.ToString());
                        Prod_free += Convert.ToDecimal(sp.TransS[i].Product[j].free.ToString());
                        Prod_qty += Convert.ToInt32(sp.TransS[i].Product[j].pqty.ToString());
                        Prod_val += Convert.ToDecimal(sp.TransS[i].Product[j].pval.ToString());
                        net_val += Convert.ToDecimal(sp.TransS[i].Product[j].Nval.ToString());
                        TwnCd = sp.TransS[i].Product[j].TwnCd.ToString();
                        TwnNw = sp.TransS[i].Product[j].TwnNw.ToString();
                        DisNm = sp.TransS[i].Product[j].DisNm.ToString();
                        DisCd = sp.TransS[i].Product[j].DisCd.ToString();

                        Decimal Prod_Rate1 = Convert.ToDecimal(sp.TransS[i].Product[j].prate.ToString());
                        Decimal Prod_dis1 = Convert.ToDecimal(sp.TransS[i].Product[j].disco.ToString());
                        Decimal Prod_free1 = Convert.ToDecimal(sp.TransS[i].Product[j].free.ToString());
                        Decimal Prod_qty1 = Convert.ToInt32(sp.TransS[i].Product[j].pqty.ToString());
                        Decimal Prod_val1 = Convert.ToDecimal(sp.TransS[i].Product[j].pval.ToString());
                        Decimal net_val1 = Convert.ToDecimal(sp.TransS[i].Product[j].Nval.ToString());
                        // PCode varchar(50), Qty int, Val float, Rate float, FQty float, DAmt float, Dval int, Md varchar(50), Mfg varchar(80), Cl float
                        @OrdDet += "<Prod PCode=\"" + Prod_code + "\" Qty=\"" + Prod_qty1 + "\" Val=\"" + Prod_val1 + "\" Rate=\"" + Prod_Rate1 + "\" FQty=\"" + Prod_free1 + "\" DAmt=\"" + 0 + "\" Dval=\"" + Prod_dis1 + "\"  Md=\"" + 0 + "\" Mfg=\"" + 0 + "\" Cl=\"" + 0 + "\" />";
                        pProd += ((pProd != "") ? "#" : "") + Prod_code + "~" + Prod_qty1 + "$" + Prod_val1;
                        npProd += ((npProd != "") ? "#" : "") + Prod_Name + "~" + Prod_qty1 + "$" + Prod_val1;
                    }
                    @OrdDet += "</ROOT>";




                        string dsStock = prd.InsertsvDCRMain(ARCd, SF_Code, SFf_type, ADt, Wtyp, TwnCd, Div_Code, Remark, SysIP, ETyp);
                        if (dsStock != "")
                        {
                            int iReturn1 = prd.MydayPlanRecordAdd(SF_Code, "", ADt, TwnCd, Remark, Div_Code, SFf_type, TwnNw, DisCd);

                        }
                        else
                        {
                            dsStock = prd.Get_svDCRMain(Div_Code, SF_Code, ADt);
                        }

                        string ST2 = prd.InsertDCRLstDet(dsStock, "0", SF_Code, "1", Retail_code, Retail_Name, dt, "0", "", pProd, npProd, "", "", "", "", "", "", "", TwnCd, Remark, Div_Code, "", dt, "", "", SF_Code, "", Convert.ToString(Prod_val), Convert.ToString(net_val), DisCd, DisNm, Convert.ToString(Prod_dis), "Nil", "");
                        if (sp.TransS[i].Product.Count != 0)
                        {
                            string ST3 = prd.InsertSecOrder(SF_Code, Retail_code, DisCd, TwnCd, "0", ADt, Convert.ToString(Prod_val), "0", Convert.ToString(net_val), Remark , "", Convert.ToString(Prod_dis), "free", dsStock, Div_Code, @OrdDet);

                    }

                }

                for (int k = 0; k < sp.TransP.Count; k++)
                {
                    string Dis_code = sp.TransP[k].Value1.ToString();
                    string Dis_Name = sp.TransP[k].Name1.ToString();
                    Remark1 = sp.TransP[k].Remark1.ToString();
                    decimal Prod_val11 = 0;
                    decimal Prod_Rate11 = 0;
                    decimal Prod_Pqty11 = 0;
                    decimal Prod_Cqty11 = 0;
                    string SPProds = string.Empty;
                    string nSPProds = string.Empty;


                    string @OrdDet1 = "<ROOT>";
                    for (int l = 0; l < sp.TransP[k].product1.Count; l++)
                    {
                        string Prod_code = sp.TransP[k].product1[l].pCode12.ToString();
                        string Prod_Name = sp.TransP[k].product1[l].pName12.ToString();
                        Prod_Rate11 += Convert.ToDecimal(sp.TransP[k].product1[l].prate12.ToString());
                        Prod_Pqty11 += Convert.ToDecimal(sp.TransP[k].product1[l].pqty.ToString());
                        Prod_Cqty11 += Convert.ToDecimal(sp.TransP[k].product1[l].cqty.ToString());
                        Prod_val11 += Convert.ToDecimal(sp.TransP[k].product1[l].pval.ToString());

                        TwnCd = sp.TransP[k].product1[l].TwnCd.ToString();
                        TwnNw = sp.TransP[k].product1[l].TwnNw.ToString();

                        Decimal Prod_Rate12 = Convert.ToDecimal(sp.TransP[k].product1[l].prate12.ToString());
                        Decimal Prod_Pqty12 = Convert.ToDecimal(sp.TransP[k].product1[l].pqty.ToString());
                        Decimal Prod_Cqty12 = Convert.ToDecimal(sp.TransP[k].product1[l].cqty.ToString());
                        Decimal Prod_val12 = Convert.ToDecimal(sp.TransP[k].product1[l].pval.ToString());

                        // PCode varchar(50), Qty int, Val float, Rate float, FQty float, DAmt float, Dval int, Md varchar(50), Mfg varchar(80), Cl float
                        @OrdDet1 += "<Prod PCode=\"" + Prod_code + "\" PName=\"" + Prod_Name + "\" CQty=\"" + Prod_Cqty12 + "\" PQty=\"" + Prod_Pqty12 + "\"  />";

                        // $PPXML = $PPXML. "<Prod PCode=\"".$samp[$j]["product_code"]."\" PName=\"".$samp[$j]["product_Name"]."\" CQty=\"".$samp[$j]["Qty"]."\" PQty=\"".$samp[$j]["PQty"]."\" />";
                        SPProds += SPProds + Prod_code + "~" + Prod_Cqty12 + "$" + Prod_Pqty12 + "#";
                        nSPProds += nSPProds + Prod_Name + "~" + Prod_Cqty12 + "$" + Prod_Pqty12 + "#";

                    }
                    @OrdDet1 += "</ROOT>";


                    string dsStock = prd.InsertsvDCRMain(ARCd, SF_Code, SFf_type, ADt, Wtyp, TwnCd, Div_Code, Remark1, SysIP, ETyp);

                        if (dsStock != "")
                        {
                            int iReturn1 = prd.MydayPlanRecordAdd(SF_Code, "", ADt, TwnCd, Remark1, Div_Code, SFf_type, TwnNw, Dis_code);

                        }
                        else
                        {
                            dsStock = prd.Get_svDCRMain(Div_Code, SF_Code, ADt);
                        }

                        string ST2 = prd.InsertDCRCSHLstDet(dsStock, "0", SF_Code, "3", Dis_code, Dis_Name, dt, "0", "", SPProds, nSPProds, "", "", TwnCd, Remark1, Div_Code, "", dt, "", "", SF_Code, "", Convert.ToString(Prod_val11), "", "");
                        if (sp.TransP[k].product1.Count != 0)
                        {
                            string ST3 = prd.InsertPriOrder(SF_Code, Dis_code, ADt, Convert.ToString(Prod_val11), "", "1900-01-01 00:00:00.000", "0", Remark1, dsStock, Div_Code, @OrdDet1, "", "");


                    }



                }
                scope.Complete();
                scope.Dispose();
                msg = "Update Successfully..!!";
            }



            catch (SqlException e)
            {
                if (e.Number == 2627)
                {
                    msg = "Transfer No. Already Exist.!!";
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                scope.Dispose();
            }
            finally
            {
                scope.Dispose();
            }
        }

        return msg;
    }

    //remarks

    [WebMethod(EnableSession = true)]
    public static string RemarkDate(string data, string fwlg)
    {
        string ARCd = string.Empty;
        string SF = string.Empty;
        string STy = string.Empty;
        string ADt = string.Empty;
        string TwnNw = string.Empty;
        int Wtyp = 0;
        string TwnCd = string.Empty;
        string DisNm = string.Empty;
        string DisCd = string.Empty;
        int div = 0;
        string Rmks = string.Empty;
        string SysIP = string.Empty;
        string ETyp = string.Empty;
        ETyp = "Web";

        string Div_Code = HttpContext.Current.Session["div_code"].ToString();
        string SF_Code = HttpContext.Current.Session["mr_sf_code"].ToString();
        string SFf_type = HttpContext.Current.Session["mr_sff_type"].ToString();

        string date = DateTime.Today.ToString("yyyy-MM-dd");
        ADt = HttpContext.Current.Session["Dcr_Date"].ToString();

        string dt = Convert.ToDateTime(ADt).ToString("yyyy-MM-dd");

        string wflg = string.Empty;
        string msg = string.Empty;
        int iReturn = -1;

        Product prd = new Product();
        using (var scope = new TransactionScope())
        {
            Rmks = data.ToString();

            SysIP = fwlg.ToString();

           

            try
            {

                string dsStock = prd.InsertsvDCRMain(ARCd, SF_Code, SFf_type, ADt, Wtyp, TwnCd, Div_Code, Rmks, SysIP, ETyp);
                if (dsStock != "")
                {
                    int iReturn1 = prd.MydayPlanRecordAdd(SF_Code, SysIP, ADt, TwnCd, Rmks, Div_Code, SFf_type, TwnNw, DisCd);
                }
                else
                {
                    dsStock = prd.Get_svDCRMain(Div_Code, SF_Code, ADt);
                    int iReturn2 = prd.MydayPlanRecordAdd(SF_Code, SysIP, ADt, TwnCd, Rmks, Div_Code, SFf_type, TwnNw, DisCd);
                }

                scope.Complete();
                scope.Dispose();
                msg = "Update Successfully..!!";
            }



            catch (SqlException e)
            {
                if (e.Number == 2627)
                {
                    msg = "Transfer No. Already Exist.!!";
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                scope.Dispose();
            }
            finally
            {
                scope.Dispose();
            }
        }

        return msg;
    }


    protected void ddl_HQ_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlTerritoryName.DataSource = null;
        ddlTerritoryName.DataBind();
        ddlTerritoryName.Items.Clear();
        ddlTerritoryName.Items.Insert(0, new ListItem("--Select--", "0"));
        if (ddl_HQ.SelectedValue != "0")
        {
            Territorys dv = new Territorys();
            dsStockist = dv.getSF_Code_Route(Division_code, ddl_HQ.SelectedValue.ToString());
            if (dsStockist.Tables[0].Rows.Count > 0)
            {
                ddlTerritoryName.DataTextField = "Territory_Name";
                ddlTerritoryName.DataValueField = "Territory_Code";
              //  ddlTerritoryName.Items.Clear();
                ddlTerritoryName.DataSource = dsStockist;
                ddlTerritoryName.DataBind();
                ddlTerritoryName.Items.Insert(0, new ListItem("--Select--", "0"));
            }
            else
            {
            }
        }
       

    }
}