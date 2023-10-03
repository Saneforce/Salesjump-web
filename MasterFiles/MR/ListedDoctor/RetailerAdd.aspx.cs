using Bus_EReport;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using DBase_EReport;
using ClosedXML.Excel;
using System.IO;
public partial class MasterFiles_MR_ListedDoctor_RetailerAdd : System.Web.UI.Page
{
    public static string div_code = string.Empty;
    public static string sf_type = string.Empty;
    public static string sf_code = string.Empty;
    public static string Sub_div_Code = string.Empty;
    public static DataSet dsListedDR = null;
    public static string strQry = string.Empty;
    public static string retailer_code = string.Empty;

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
        else if (sf_type == "4")
        {
            this.MasterPageFile = "~/Master_DIS.master";
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        HiddenFieldRetCode.Value = Request.QueryString["ListedDrCode"];
        retailer_code = Request.QueryString["ListedDrCode"];
        sf_code = Session["Sf_Code"].ToString();
        sf_type = Session["sf_type"].ToString();
        //Sub_div_Code = Session["Sub_Div"].ToString();
        if (sf_type == "4") { Sub_div_Code = Session["Sub_Div"].ToString(); }
        //if (sf_type == "4") { Sub_div_Code = "41"; }
    }

    [WebMethod]
    public static string FillRetNumber(string divcode)
    {
        long num = 0;
        ListedDR lstDR = new ListedDR();
        dsListedDR = lstDR.getCheck(div_code);
        num = Convert.ToInt64(dsListedDR.Tables[0].Rows[0].ItemArray.GetValue(0).ToString());
        num = num + 1;
        string newww = num.ToString();
        return newww;
    }
    [WebMethod]
    public static string FillChannel(string divcode)
    {
        ListedDR lstDR = new ListedDR();
        dsListedDR = lstDR.FetchSpeciality(divcode);
        return JsonConvert.SerializeObject(dsListedDR.Tables[0]);
    }
    [WebMethod]
    public static string FillTerritory(string divcode, string sf_type, string sf_code)
    {
        ListedDR lstDR = new ListedDR();
        dsListedDR = lstDR.FetchTerritory(divcode, sf_type, sf_code);
        return JsonConvert.SerializeObject(dsListedDR.Tables[0]);
    }
	
	
	
    [WebMethod]
    public static string FillClass(string divcode)
    {
        ListedDR lstDR = new ListedDR();
        dsListedDR = lstDR.FetchClass(divcode);
        return JsonConvert.SerializeObject(dsListedDR.Tables[0]);
    }
    [WebMethod]
    public static string FillType(string divcode)
    {
        ListedDR lstDR = new ListedDR();
        dsListedDR = lstDR.getIn_Type(divcode);
        return JsonConvert.SerializeObject(dsListedDR.Tables[0]);
    }

    [WebMethod]
    public static string FillCatagry(string divcode)
    {
        DataSet ds = new DataSet();
        if (sf_type == "4")
        {
            // ds=getDataSet("SELECT 0 as Doc_Cat_Code, '---Select---' as Doc_Cat_SName, '---Select---' as Doc_Cat_Name UNION  SELECT Doc_Cat_Code,Doc_Cat_SName,Doc_Cat_Name FROM  Mas_Doctor_Category  where division_Code = '" + div_code + "' and subdivision_code='"+ Sub_div_Code + "' AND Doc_Cat_Active_Flag=0 order by Doc_Cat_Name");

            ds = getDataSet("SELECT 0 as Doc_Cat_Code, '---Select---' as Doc_Cat_SName, '---Select---' as Doc_Cat_Name UNION SELECT Doc_ClsCode Doc_Cat_Code, Doc_ClsSName Doc_Cat_SName, Doc_ClsName  Doc_Cat_Name from Mas_Doc_Class where Division_Code = '" + div_code + "' and Sub_Div = '" + Sub_div_Code + "' and Doc_Cls_ActiveFlag= 0");

        }
        else
        {
            // ListedDR lstDR = new ListedDR();
            //  ds = lstDR.FetchCatagory(divcode);
            ds = getDataSet("SELECT 0 as Doc_Cat_Code, '---Select---' as Doc_Cat_SName, '---Select---' as Doc_Cat_Name UNION SELECT Doc_ClsCode Doc_Cat_Code, Doc_ClsSName Doc_Cat_SName, Doc_ClsName  Doc_Cat_Name from Mas_Doc_Class where Division_Code = '" + div_code + "' and Doc_Cls_ActiveFlag= 0");
        }
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }

    [WebMethod]
    public static string FillSubCatagry(string divcode, string cat_code)
    {
        DataSet ds = new DataSet();
        if (sf_type == "4")
        {
            // ds = getDataSet("SELECT 0 as Doc_SubCatCode, '---Select---' as Doc_SubCatSName, '---Select---' as Doc_SubCatName UNION SELECT Doc_SubCatCode,Doc_SubCatSName,Doc_SubCatName FROM  Mas_Doc_SubCategory where Division_Code='" + div_code + "' and sub_div_code='"+ Sub_div_Code + "' and Doc_SubCat_ActiveFlag=0 and charindex(','+'"+cat_code+"'+',',','+Doc_Cat_Code+',')>0 order by Doc_SubCatName");

            ds = getDataSet("SELECT 0 as Doc_SubCatCode, '---Select---' as Doc_SubCatSName, '---Select---' as Doc_SubCatName UNION SELECT Doc_Special_Code Doc_SubCatCode, Doc_Special_SName Doc_SubCatSName, Doc_Special_Name Doc_SubCatName FROM Mas_Doctor_Speciality where Doc_Special_Active_Flag=0 and charindex(','+'" + cat_code + "'+',',','+Class_Code+',')>0 and Division_Code='" + div_code + "'");
        }
        else
        {
            ds = getDataSet("SELECT 0 as Doc_SubCatCode, '---Select---' as Doc_SubCatSName, '---Select---' as Doc_SubCatName UNION SELECT Doc_Special_Code Doc_SubCatCode, Doc_Special_SName Doc_SubCatSName, Doc_Special_Name Doc_SubCatName FROM Mas_Doctor_Speciality where Doc_Special_Active_Flag=0 and charindex(','+'" + cat_code + "'+',',','+Class_Code+',')>0 and Division_Code='" + div_code + "'");
           // ds = getDataSet(" SELECT 0 as Doc_SubCatCode,'---Select---' as Doc_SubCatSName, '---Select---' as Doc_SubCatName UNION SELECT Doc_SubCatCode,Doc_SubCatSName,Doc_SubCatName FROM  Mas_Doc_SubCategory where Division_Code='" + div_code + "' and Doc_SubCat_ActiveFlag=0 order by Doc_SubCatName");
        }
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }

    [WebMethod]
    public static string Filldrslab(string divcode)
    {
        Stockist dist = new Stockist();
        dsListedDR = dist.getMasPurchase(div_code);
        return JsonConvert.SerializeObject(dsListedDR.Tables[0]);
    }
    [WebMethod]
    public static string FillUOM(string divcode)
    {
        Division dv = new Division();
        dsListedDR = dv.getStatePerDivision(div_code);
        return JsonConvert.SerializeObject(dsListedDR.Tables[0]);
    }
    [WebMethod]
    public static string FillState(string divcode)
    {
        string Stockist_Code = HttpContext.Current.Session["Sf_Code"].ToString();
        div_code = div_code.TrimEnd(',');
        DataSet ds = null;
        if (sf_type == "4")
        {
            ds = getDataSet("select ma.State_Code,ma.StateName from Mas_State ma inner join mas_stockist ms on ms.State_Code = ma.State_Code where Stockist_Code='" + Stockist_Code + "'");
        }
        else
        {
            ds = getDataSet("select State_Code,StateName from Mas_State order by StateName");
        }
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    [WebMethod]
    public static string FillPrice(string divcode)
    {
        dsListedDR = getDataSet(" select Price_list_Sl_No,Price_list_Name from Mas_Retailer_Wise_Price_Head group by Price_list_Sl_No,Price_list_Name");
        return JsonConvert.SerializeObject(dsListedDR.Tables[0]);
    }
    [WebMethod]
    public static string FillGroup(string divcode)
    {
        dsListedDR = getDataSet("select * from Mas_Doc_Qualification where Division_Code = '" + div_code + "'  and Doc_Qua_ActiveFlag = 0");
        return JsonConvert.SerializeObject(dsListedDR.Tables[0]);
    }

    [WebMethod]
    public static string saveRetailer(string data, string Rate_Effective_Date)
    {
        string msg = string.Empty;
        SaveRetailerDataa Data = JsonConvert.DeserializeObject<SaveRetailerDataa>(data);
        msg = saveRetailerData(Data, Rate_Effective_Date);
        return msg;
    }

    [WebMethod]
    public static string updateRetailer(string data, string Rate_Effective_Date)
    {
        string msg = string.Empty;
        SaveRetailerDataa Data = JsonConvert.DeserializeObject<SaveRetailerDataa>(data);
        msg = saveRetailerData(Data, Rate_Effective_Date);
        return msg;
    }

    public class SaveRetailerDataa
    {
        [JsonProperty("DivCode")]
        public string div_code { get; set; }

        [JsonProperty("DrCode")]
        public string dr_code { get; set; }

        [JsonProperty("StateCode")]
        public string state_code { get; set; }

        [JsonProperty("DrName")]
        public string DR_Name { get; set; }

        [JsonProperty("SfCode")]
        public string sf_code { get; set; }

        [JsonProperty("SfType")]
        public string sf_type { get; set; }

        [JsonProperty("RetailCode")]
        public string retail_code { get; set; }

        [JsonProperty("MobileNo")]
        public object Mobile_No { get; set; }

        [JsonProperty("ErbCode")]
        public string erbCode { get; set; }

        [JsonProperty("ContactPerson")]
        public object Contact_Person { get; set; }

        [JsonProperty("DrSpec")]
        public object DR_Spec { get; set; }

        [JsonProperty("DrSpecName")]
        public object dr_spec_name { get; set; }

        [JsonProperty("SalesTax")]
        public object sales_Tax { get; set; }

        [JsonProperty("Tinno")]
        public object Tinno { get; set; }

        [JsonProperty("DrTerr")]
        public object DR_Terr { get; set; }

        [JsonProperty("CreditDays")]
        public object credit_days { get; set; }

        [JsonProperty("DrClass")]
        public object DR_Class { get; set; }

        [JsonProperty("DrClassName")]
        public object dr_class_name { get; set; }

        [JsonProperty("DrCategory")]
        public object drcategory { get; set; }

        [JsonProperty("DrCategoryName")]
        public object drcategoryName { get; set; }

        [JsonProperty("Outstandng")]
        public object outstandng { get; set; }

        [JsonProperty("Creditlmt")]
        public object creditlmt { get; set; }

        [JsonProperty("Ad")]
        public object ad { get; set; }

        [JsonProperty("DdlReType")]
        public object DDL_Re_Type { get; set; }

        [JsonProperty("MilkPon")]
        public object Milk_pon { get; set; }

        [JsonProperty("SlbVal")]
        public object slbval { get; set; }

        [JsonProperty("Email")]
        public object email { get; set; }

        [JsonProperty("Uom")]
        public object UOM { get; set; }

        [JsonProperty("UomName")]
        public object UOM_Name { get; set; }

        [JsonProperty("CusAlter")]
        public object Cus_Alter { get; set; }

        [JsonProperty("Latitude")]
        public object latitude { get; set; }

        [JsonProperty("Longitude")]
        public object longitude { get; set; }

        [JsonProperty("FreezerTagNo")]
        public object Freezer_tag_no { get; set; }

        [JsonProperty("FreezerStatus")]
        public object Freezer_status { get; set; }

        [JsonProperty("FreezerType")]
        public object Freezer_Type { get; set; }

        [JsonProperty("PanNo")]
        public object pan_no { get; set; }

        [JsonProperty("TcsApp")]
        public object Tcs_App { get; set; }

        [JsonProperty("TdsApp")]
        public object Tds_App { get; set; }

        [JsonProperty("DrAddress1")]
        public object DR_Address1 { get; set; }

        [JsonProperty("DrAddress2")]
        public object DR_Address2 { get; set; }

        [JsonProperty("CArr")]
        public List<AdditionalDetails> carr { get; set; }

        [JsonProperty("FrzArr")]
        public List<AdditionalFreezType> farr { get; set; }

        [JsonProperty("RetType")]
        public object RetType { get; set; }

        [JsonProperty("FssiNo")]
        public object FssiNo { get; set; }

        [JsonProperty("Taxgrp")]
        public object Taxgrp { get; set; }

        [JsonProperty("Frezdate")]
        public string frezdate { get; set; }

        [JsonProperty("DrSubcategory")]
        public string drSubcategory { get; set; }

        [JsonProperty("PriceListName")]
        public string PriceListName { get; set; }

        [JsonProperty("DrQuaCode")]
        public string drQuaCode { get; set; }

        [JsonProperty("DrQuaName")]
        public string drQuaName { get; set; }

        [JsonProperty("BodyBulkData")]
        public List<BulkProductBodyData> BodyBulkData { get; set; }

        [JsonProperty("outletType")]
        public string outletType { get; set; }

        [JsonProperty("Delivery_Mode")]
        public string Delivery_Mode { get; set; }

        [JsonProperty("Dist_Code")]
        public string Dist_Code { get; set; }

        [JsonProperty("Sub_Name")]
        public string Sub_Name { get; set; }

        [JsonProperty("SecMobile_No")]
        public string SecMobile_No { get; set; }

        [JsonProperty("Aadhar_No")]
        public string Aadhar_No { get; set; }
    }

    public class AdditionalDetails
    {
        [JsonProperty("cfield")]
        public object cfield { get; set; }

        [JsonProperty("cval")]
        public object cvalue { get; set; }
    }

    public class AdditionalFreezType
    {
        [JsonProperty("ffield")]
        public object ffield { get; set; }

        [JsonProperty("fval")]
        public object fvalue { get; set; }
    }

    public class BulkProductBodyData
    {
        public string product_detail_code { get; set; }
        public string product_detail_name { get; set; }
        public string OffInvPrice { get; set; }
        public string NetPrice { get; set; }
        public string Unit { get; set; }
        public string UnitCode { get; set; }
        public string Ret_Rate { get; set; }
        public string MRP_Price { get; set; }
        public string Ret_Rate_in_piece { get; set; }
        public string Dis_Rate { get; set; }
        public string Dis_Rate_in_piece { get; set; }
        public int Active_flag { get; set; }
        public string Retailer_Code { get; set; }
        public string Approvel_To { get; set; }

    }


    public static string saveRetailerData(SaveRetailerDataa sd, string Rate_Effective_Date)
    {
        int iReturn = -1;
        string dup = string.Empty;
        string strSfCode = string.Empty;
        string divcode = string.Empty;
        string Subdivisioncode = string.Empty;
        string msg = string.Empty;
        List<AdditionalFreezType> a = sd.farr;
        List<AdditionalDetails> b = sd.carr;
        List<BulkProductBodyData> BodyData = sd.BodyBulkData;
        DBase_EReport.DB_EReporting db = new DBase_EReport.DB_EReporting();

        string sxml = "<ROOT>";
        for (int i = 0; i < a.Count; i++)
        {
            sxml += "<ASSD stype=\"frztype\" fld=\"" + a[i].ffield + "\" val=\"" + a[i].fvalue + "\" />";
        }
        for (int i = 0; i < b.Count; i++)
        {
            sxml += "<ASSD stype=\"contact\" fld=\"" + b[i].cfield + "\" val=\"" + b[i].cvalue + "\" />";
        }
        sxml += "</ROOT>";

        string Active_flag_p = "0";
        if (sf_type == "4")
        {
            Active_flag_p = "1";
        }

        string sxm11 = "<ROOT>";
        for (int i = 0; i < BodyData.Count; i++)
        {
            sxm11 += "<Prod Product_Code=\"" + BodyData[i].product_detail_code + "\" Product_Name=\"" + BodyData[i].product_detail_name + "\" Ret_Rate=\"" + BodyData[i].Ret_Rate +
                "\" OffInvPrice=\"" + BodyData[i].OffInvPrice + "\" NetPrice=\"" + BodyData[i].NetPrice + "\" Unit=\"" + BodyData[i].Unit + "\"  UnitCode=\"" + BodyData[i].UnitCode + "\"  MRP_Price=\"" + BodyData[i].MRP_Price + "\"  Ret_Rate_in_piece=\"" +
                BodyData[i].Ret_Rate_in_piece + "\" Dis_Rate_in_piece=\"" + BodyData[i].Dis_Rate_in_piece + "\" Active_flag=\"" + Active_flag_p + "\"  Approvel_To=\"" + BodyData[i].Approvel_To + "\" />";
        }
        sxm11 += "</ROOT>";

        long Listed_DR_Code1;
        int active_flag = 0;
        if (sd.dr_code == "")
        {
            if (!sRecordExist(sd.retail_code, sd.DR_Name, sd.div_code))
            {
                if (!RecordExist(sd.DR_Name, sd.retail_code, sd.div_code))
                {
                    if (!ERPRecordExist(sd.erbCode, sd.div_code, sd.retail_code))
                    {
                        try
                        {
                            //  Listed_DR_Code = -1;
                            string Division_Code = "-1";
                            DataSet ddd = getDataSet("select isnull(MAX(ListedDrCode), 0) + 1 as Num from Mas_ListedDr");
                            Listed_DR_Code1 = Convert.ToInt64(ddd.Tables[0].Rows[0].ItemArray.GetValue(0).ToString());
                            Listed_DR_Code1 = Listed_DR_Code1 + 1;
                            string Listed_DR_Code = Listed_DR_Code1.ToString();
                            Listed_DR_Code = Listed_DR_Code.ToString();

                            //   strQry = "SELECT ISNULL(MAX(ListedDrCode),0)+1 FROM Mas_ListedDr";
                            //    Listed_DR_Code = db.Exec_Scalar(strQry);

                            strQry = "select Territory_SName,sf_code,Dist_Name,Division_Code from  Mas_Territory_Creation where Territory_Code ='" + sd.DR_Terr + "'";
                            DataSet ds = db.Exec_DataSet(strQry);

                            string sfcode = "";
                            // string distname = "";
                            string distname = sd.Dist_Code;

                            string terr_code = "";
                            foreach (DataRow dd in ds.Tables[0].Rows)
                            {
                                sfcode = dd["sf_code"].ToString();
                                // distname = dd["Dist_Name"].ToString();
                                Division_Code = dd["Division_Code"].ToString();
                                terr_code = dd["Territory_SName"].ToString();
                            }
                            strQry = "select OutletCode,ms.subdivision_code from mas_stockist ms inner join Mas_subdivision msd on cast(msd.subdivision_code as varchar) = (select Replace(ms.subdivision_code,',',' ')) where Stockist_Code in (select top 1 * from SplitString('" + distname + "',','))";
                            //strQry = "select OutletCode,* from Mas_subdivision  msd inner join mas_stockist ms on ms.subdivision_code = msd.subdivision_code where Stockist_Code ="+ distname;
                            DataSet dss = db.Exec_DataSet(strQry);

                            string outletcode = "";
                            Subdivisioncode = "";
                            foreach (DataRow dd1 in dss.Tables[0].Rows)
                            {
                                outletcode = dd1["OutletCode"].ToString();
                                Subdivisioncode = dd1["subdivision_code"].ToString();
                            }
                            outletcode = outletcode + Listed_DR_Code;

                            if (sd.sf_type == "4" && Division_Code == "3")
                            {
                                active_flag = 3;
                            }

                            SqlParameter[] parameters = new SqlParameter[]
                            {
                                    new SqlParameter("@LstDrcode", Listed_DR_Code),
                                    new SqlParameter("@LstDrname", sd.DR_Name),
                                    new SqlParameter("@SfCode", sfcode),
                                    new SqlParameter("@Code", outletcode),
                                    //new SqlParameter("@Code", sd.erbCode),
                                    new SqlParameter("@MobilNo", sd.Mobile_No),
                                    new SqlParameter("@ContactPerson", sd.Contact_Person),
                                    new SqlParameter("@DrSpec", sd.DR_Spec),
                                    new SqlParameter("@DrSpecName", sd.dr_spec_name),
                                    new SqlParameter("@SalesTax", sd.sales_Tax),
                                    new SqlParameter("@Tinno", sd.Tinno),
                                    new SqlParameter("@DrTerr", sd.DR_Terr),
                                    new SqlParameter("@CreditDays", sd.credit_days),
                                    new SqlParameter("@DrClass", sd.DR_Class),
                                    new SqlParameter("@DrClassName", sd.dr_class_name),
                                    new SqlParameter("@Ad", sd.ad),
                                    new SqlParameter("@DrAddress1", sd.DR_Address1),
                                    new SqlParameter("@DrAddress2", sd.DR_Address2),
                                    new SqlParameter("@Division_Code", Division_Code),
                                    new SqlParameter("@Active_flag", active_flag),
                                    new SqlParameter("@Milkpon", sd.Milk_pon),
                                    new SqlParameter("@Uom", sd.UOM),
                                    new SqlParameter("@UomName", sd.UOM_Name),
                                    new SqlParameter("@TerrCode", terr_code),
                                    new SqlParameter("@DistName", distname.TrimEnd(',')),
                                    new SqlParameter("@ReType", sd.DDL_Re_Type),
                                    new SqlParameter("@OutStanding", sd.outstandng),
                                    new SqlParameter("@Creditlmt", sd.creditlmt),
                                    new SqlParameter("@CustAltr", sd.Cus_Alter),
                                    new SqlParameter("@DrCategory", sd.drcategory),
                                    new SqlParameter("@DrCategoryName", sd.drcategoryName),
                                    new SqlParameter("@Lattitd", sd.latitude),
                                    new SqlParameter("@Longtd", sd.longitude),
                                    new SqlParameter("@SlbVal", sd.slbval),
                                    new SqlParameter("@Email", sd.email),
                                    new SqlParameter("@PanNo", sd.pan_no),
                                    new SqlParameter("@FrzTagNo", sd.Freezer_tag_no),
                                    new SqlParameter("@FrzStatus", sd.Freezer_status),
                                    new SqlParameter("@Tds", sd.Tds_App),
                                    new SqlParameter("@Tcs", sd.Tcs_App),
                                    new SqlParameter("@StCode",sd.state_code),
                                    new SqlParameter("@Freezer_Type",sd.Freezer_Type),
                                    new SqlParameter("@RetType",sd.RetType),
                                    new SqlParameter("@FssiNo",sd.FssiNo),
                                    new SqlParameter("@Taxgrp",sd.Taxgrp),
                                    new SqlParameter("@sxml", sxml),
                                    new SqlParameter("@frezdate",sd.frezdate),
                                    new SqlParameter("@DrSubcategory",sd.drSubcategory),
                                   // new SqlParameter("@PriceListName",sd.PriceListName),
                                    new SqlParameter("@Doc_QuaCode",sd.drQuaCode),
                                    new SqlParameter("@Doc_Qua_Name",sd.drQuaName),
                                    new SqlParameter("@OutletType",sd.outletType),
                                    //new SqlParameter("@Price_active_flag",Active_flag_p),
                                    new SqlParameter("@Delivery_Mode",sd.Delivery_Mode),
                                    //, new SqlParameter("@sub_div_code",Subdivisioncode)
                                    new SqlParameter("@Sub_Name",sd.Sub_Name),
                                    new SqlParameter("@SecMobile_No",sd.SecMobile_No),
                                    new SqlParameter("@Aadhar_No",sd.Aadhar_No)

                            };

                            using (SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Ereportcon"].ToString()))
                            {
                                using (SqlCommand cmd = con.CreateCommand())
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.CommandText = "insertNewRetailerData";
                                    cmd.Parameters.AddRange(parameters);

                                    try
                                    {
                                        if (con.State != ConnectionState.Open)
                                        {
                                            con.Open();
                                        }
                                        cmd.ExecuteNonQuery();
                                        con.Close();
                                        if (BodyData.Count > 0)
                                        {
                                            string response = SaveBulkPriceOrder(sd.div_code, sxm11, Listed_DR_Code.ToString(), Listed_DR_Code.ToString(), Active_flag_p, Subdivisioncode, Rate_Effective_Date);
                                        }
                                        msg = "Created Successfully";
                                    }
                                    catch (Exception ex)
                                    {
                                        msg = ex.Message;
                                        throw ex;
                                    }
                                    finally
                                    {
                                        con.Close();
                                    }
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            msg = ex.Message;
                            throw ex;
                        }
                    }
                    else
                    {
                        msg = "ERP Code Already Exist";
                    }
                }
                else
                {
                    msg = "Name Already Exist";
                }
            }
            else
            {
                msg = "Code Already Exist";
            }
        }
        else
        {
            if (!sRecordExist1(sd.erbCode, sd.dr_code, sd.div_code))
            {
                if (!ERPRecordExist(sd.erbCode, sd.div_code, sd.dr_code))
                {
                    try
                    {
                        strQry = "select sf_code,Dist_Name,Division_Code,Territory_SName from  Mas_Territory_Creation where Territory_Code ='" + sd.DR_Terr + "'";
                        DataSet ds = db.Exec_DataSet(strQry);

                        string Division_Code = "-1";
                        string sfcode = "";
                        // string distname = "";
                        string terr_code = "";
                        string distname = sd.Dist_Code;

                        foreach (DataRow dd in ds.Tables[0].Rows)
                        {
                            sfcode = dd["sf_code"].ToString();
                            //distname = dd["Dist_Name"].ToString();
                            Division_Code = dd["Division_Code"].ToString();
                            terr_code = dd["Territory_SName"].ToString();
                        }

                        SqlParameter[] parameters = new SqlParameter[]
                        {
                                new SqlParameter("@LstDrcode", sd.dr_code),
                                new SqlParameter("@LstDrname", sd.DR_Name),
                                new SqlParameter("@SfCode", sfcode),
                               // new SqlParameter("@Code", sd.erbCode),
                                new SqlParameter("@MobilNo", sd.Mobile_No),
                                new SqlParameter("@ContactPerson", sd.Contact_Person),
                                new SqlParameter("@DrSpec", sd.DR_Spec),
                                new SqlParameter("@DrSpecName", sd.dr_spec_name),
                                new SqlParameter("@SalesTax", sd.sales_Tax),
                                new SqlParameter("@Tinno", sd.Tinno),
                                new SqlParameter("@DrTerr", sd.DR_Terr),
                                new SqlParameter("@CreditDays", sd.credit_days),
                                new SqlParameter("@DrClass", sd.DR_Class),
                                new SqlParameter("@DrClassName", sd.dr_class_name),
                                new SqlParameter("@Ad", sd.ad),
                                new SqlParameter("@DrAddress1", sd.DR_Address1),
                                new SqlParameter("@DrAddress2", sd.DR_Address2),
                                new SqlParameter("@Division_Code", Division_Code),
                                new SqlParameter("@Milkpon", sd.Milk_pon),
                                new SqlParameter("@Uom", sd.UOM),
                                new SqlParameter("@UomName", sd.UOM_Name),
                                new SqlParameter("@TerrCode", terr_code),
                                new SqlParameter("@DistName", distname.TrimEnd(',')),
                                new SqlParameter("@ReType", sd.DDL_Re_Type),
                                new SqlParameter("@OutStanding", sd.outstandng),
                                new SqlParameter("@Creditlmt", sd.creditlmt),
                                new SqlParameter("@CustAltr", sd.Cus_Alter),
                                new SqlParameter("@DrCategory", sd.drcategory),
                                new SqlParameter("@DrCategoryName", sd.drcategoryName),
                                new SqlParameter("@Lattitd", sd.latitude),
                                new SqlParameter("@Longtd", sd.longitude),
                                new SqlParameter("@SlbVal", sd.slbval),
                                new SqlParameter("@Email", sd.email),
                                new SqlParameter("@PanNo", sd.pan_no),
                                new SqlParameter("@FrzTagNo", sd.Freezer_tag_no),
                                new SqlParameter("@FrzStatus", sd.Freezer_status),
                                new SqlParameter("@Tds", sd.Tds_App),
                                new SqlParameter("@Tcs", sd.Tcs_App),
                                new SqlParameter("@StCode",sd.state_code),
                                new SqlParameter("@Freezer_Type",sd.Freezer_Type),
                                new SqlParameter("@RetType",sd.RetType),
                                new SqlParameter("@FssiNo",sd.FssiNo),
                                new SqlParameter("@Taxgrp",sd.Taxgrp),
                                new SqlParameter("@sxml", sxml),
                                new SqlParameter("@frezdate",sd.frezdate),
                                new SqlParameter("@DrSubcategory",sd.drSubcategory),
                               // new SqlParameter("@PriceListName ",sd.PriceListName),
                                new SqlParameter("@Doc_QuaCode",sd.drQuaCode),
                                new SqlParameter("@Doc_Qua_Name",sd.drQuaName),
                                //new SqlParameter("@Price_active_flag",Active_flag_p),
                                new SqlParameter("@Delivery_Mode",sd.Delivery_Mode),
                                new SqlParameter("@Sub_Name",sd.Sub_Name),
                                new SqlParameter("@SecMobile_No",sd.SecMobile_No),
                                new SqlParameter("@Aadhar_No",sd.Aadhar_No)
                        };

                        using (SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Ereportcon"].ToString()))
                        {
                            using (SqlCommand cmd = con.CreateCommand())
                            {
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.CommandText = "updateRetailerData";
                                cmd.Parameters.AddRange(parameters);

                                try
                                {
                                    if (con.State != ConnectionState.Open)
                                    {
                                        con.Open();
                                    }
                                    //iReturn = db.ExecQry(strQry);
                                    cmd.ExecuteNonQuery();
                                    con.Close();
                                    if (BodyData.Count > 0)
                                    {
                                        string response = SaveBulkPriceOrder(sd.div_code, sxm11, sd.dr_code, sd.dr_code, Active_flag_p, Subdivisioncode, Rate_Effective_Date);
                                        //if (response == "success")
                                        //{
                                        //   
                                        //}
                                    }
                                    msg = "Updated Successfully";
                                }
                                catch (Exception ex)
                                {
                                    msg = ex.Message;
                                    throw ex;
                                }
                                finally
                                {
                                    con.Close();
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        msg = ex.Message;
                        throw ex;
                    }
                }
                else
                {
                    msg = "ERP Code Already Exist";
                }
            }
            else
            {
                msg = "Code Already Exist";
            }
        }
        return msg;
    }

    public static bool sRecordExist1(string retail_code, string DR_Code, string Div_code)
    {

        bool bRecordExist = false;
        try
        {
            DBase_EReport.DB_EReporting db = new DBase_EReport.DB_EReporting();

            strQry = "  SELECT COUNT(code) FROM Mas_ListedDr WHERE Code='" + retail_code + "' and  ListedDrCode ! =" + DR_Code + " and Division_Code !='" + Div_code + "' and (ListedDr_Active_Flag=0 or ListedDr_Active_Flag=2) ";
            int iRecordExist = db.Exec_Scalar(strQry);

            if (iRecordExist > 0)
                bRecordExist = true;
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return bRecordExist;
    }

    public static bool sRecordExist(string retail_code, string DR_Name, string Div_code)
    {

        bool bRecordExist = false;
        try
        {
            DBase_EReport.DB_EReporting db = new DBase_EReport.DB_EReporting();

            strQry = "SELECT COUNT(code) FROM Mas_ListedDr WHERE ListedDr_Name='" + DR_Name + "' and Code='" + retail_code + "' and Division_Code='" + Div_code + "' AND  (ListedDr_Active_Flag=0 or ListedDr_Active_Flag=2) ";
            int iRecordExist = db.Exec_Scalar(strQry);

            if (iRecordExist > 0)
                bRecordExist = true;
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return bRecordExist;
    }

    public static bool RecordExist(string Listed_DR_Name, string retail_code, string Div_Code)
    {

        bool bRecordExist = false;
        try
        {
            DBase_EReport.DB_EReporting db = new DBase_EReport.DB_EReporting();

            strQry = "SELECT COUNT(ListedDr_Name) FROM Mas_ListedDr WHERE Code='" + retail_code + "' AND ListedDr_Name='" + Listed_DR_Name + "' and Division_Code='" + Div_Code + "' AND  (ListedDr_Active_Flag=0 or ListedDr_Active_Flag=2) ";
            int iRecordExist = db.Exec_Scalar(strQry);

            if (iRecordExist > 0)
                bRecordExist = true;
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return bRecordExist;
    }

    public static bool ERPRecordExist(string retail_code, string Div_code, string rtCode)
    {

        bool bRecordExist = false;
        try
        {
            if (retail_code != string.Empty)
            {
                DBase_EReport.DB_EReporting db = new DBase_EReport.DB_EReporting();

                strQry = "SELECT ListedDrCode FROM Mas_ListedDr WHERE  Code='" + retail_code + "' and Division_Code='" + Div_code + "' AND  (ListedDr_Active_Flag=0 or ListedDr_Active_Flag=2) ";

                DataSet ds = db.Exec_DataSet(strQry);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == rtCode)
                    {

                    }
                    else
                    {
                        bRecordExist = true;
                    }
                }
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
        return bRecordExist;
    }

    public class RetailerData
    {
        public string DrCode { get; set; }
        public string DrName { get; set; }
        public string MobileNo { get; set; }
        public string RetailCode { get; set; }
        public string ErbCode { get; set; }
        public string ContactPerson { get; set; }
        public string DrSpec { get; set; }
        public string DrSpecName { get; set; }
        public string SalesTax { get; set; }
        public string Tinno { get; set; }
        public string DrTerr { get; set; }
        public string CreditDays { get; set; }
        public string DrClass { get; set; }
        public string DrClassName { get; set; }
        public string DrCategory { get; set; }
        public string DrCategoryName { get; set; }
        public string Outstandng { get; set; }
        public string Creditlmt { get; set; }
        public string Ad { get; set; }
        public string DdlReType { get; set; }
        public string MilkPon { get; set; }
        public string SlbVal { get; set; }
        public string Email { get; set; }
        public string Uom { get; set; }
        public string UomName { get; set; }
        public string CusAlter { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string FreezerTagNo { get; set; }
        public string FreezerStatus { get; set; }
        public string PanNo { get; set; }
        public string TcsApp { get; set; }
        public string TdsApp { get; set; }
        public string DrAddress1 { get; set; }
        public string DrAddress2 { get; set; }
        public string cname { get; set; }
        public string cval { get; set; }
        public string ftypename { get; set; }
        public string ftypeval { get; set; }
        public string StateCode { get; set; }
        public string Freezer_Type { get; set; }
        public string RetType { get; set; }
        public string Taxgrp { get; set; }
        public string FssiNo { get; set; }
        public string Frezdate { get; set; }
        public string DrSubcategory { get; set; }
        public string PriceListName { get; set; }
        public string DrQuaCode { get; set; }
        public string DrQuaName { get; set; }
        public string Outlet_Type { get; set; }
        public string Delivery_Mode { get; set; }
        public string Dist_Code { get; set; }
        public string Sub_Name { get; set; }
        public string SecMobile_No { get; set; }
        public string Aadhar_No { get; set; }

        public string Allowance_Type { get; set; }
    }

    [WebMethod]
    public static RetailerData[] getRetDets(string retcode)
    {
        string msg = string.Empty;
        DataSet dsm = getRetailerDetails(retcode);
        List<RetailerData> ad = new List<RetailerData>();
        foreach (DataRow row in dsm.Tables[0].Rows)
        {
            RetailerData asd = new RetailerData();
            asd.DrCode = row["ListedDrCode"].ToString();
            asd.DrName = row["ListedDr_Name"].ToString();
            asd.MobileNo = row["ListedDr_Mobile"].ToString();
            asd.ErbCode = row["Code"].ToString();
            asd.ContactPerson = row["Contact_Person_Name"].ToString();
            asd.DrSpec = row["Doc_Special_Code"].ToString();
            asd.DrSpecName = row["Doc_Spec_ShortName"].ToString();
            asd.Tinno = row["Tin_No"].ToString();
            asd.SalesTax = row["sales_Taxno"].ToString();
            asd.DrTerr = row["Territory_Code"].ToString();
            asd.CreditDays = row["Credit_Days"].ToString();
            asd.DrClass = row["Doc_ClsCode"].ToString();
            asd.DrClassName = row["Doc_Class_ShortName"].ToString();
            asd.Ad = row["Advance_amount"].ToString();
            asd.DrAddress1 = row["ListedDr_Address1"].ToString();
            asd.DrAddress2 = row["ListedDr_Address2"].ToString();
            asd.MilkPon = row["Milk_Potential"].ToString();
            asd.Uom = row["UOM"].ToString();
            asd.UomName = row["UOM_Name"].ToString();
            asd.DdlReType = row["Retailer_Type"].ToString();
            asd.Outstandng = row["outstanding"].ToString();
            asd.Creditlmt = row["Credit_Limit"].ToString();
            asd.CusAlter = row["Cus_Alter"].ToString();
            asd.DrCategory = row["Doc_Cat_Code"].ToString();
            asd.DrCategoryName = row["Doc_Cat_ShortName"].ToString();
            asd.Latitude = row["ListedDr_Class_Patients"].ToString();
            asd.Longitude = row["ListedDr_Consultation_Fee"].ToString();
            asd.SlbVal = row["Purchase_slab"].ToString();
            asd.Email = row["ListedDr_Email"].ToString();
            asd.PanNo = row["Pan_No"].ToString();
            asd.FreezerTagNo = row["Freezer_Tag_no"].ToString();
            asd.FreezerStatus = row["Freezer_status"].ToString();
            asd.TdsApp = row["Tds"].ToString();
            asd.TcsApp = row["Tcs"].ToString();
            asd.StateCode = row["State_Code"].ToString();
            asd.Freezer_Type = row["Freezer_Type"].ToString();
            asd.RetType = row["RetType"].ToString();
            asd.FssiNo = row["FssiNo"].ToString();
            asd.Taxgrp = row["Taxgrp"].ToString();
            asd.Frezdate = row["Frezdate"].ToString();
            asd.DrSubcategory = row["Doc_SubCatCode"].ToString();
            asd.PriceListName = row["Price_List_Name"].ToString();
            asd.DrQuaCode = row["Doc_QuaCode"].ToString();
            asd.DrQuaName = row["Doc_Qua_Name"].ToString();
            asd.Outlet_Type = row["Outlet_Type"].ToString();
            asd.Delivery_Mode = row["Delivery_Mode"].ToString();
            asd.Dist_Code = row["Dist_name"].ToString();
            asd.Sub_Name = row["Sub_Name"].ToString();
            asd.SecMobile_No = row["SecMobile_No"].ToString();
            asd.Aadhar_No = row["Aadhar_No"].ToString(); 
            asd.Allowance_Type = row["Allowance_Type"].ToString();
            ad.Add(asd);
        }
        foreach (DataRow row in dsm.Tables[1].Rows)
        {
            RetailerData asd = new RetailerData();

            if (row["Field_Type"].ToString() == "contact")
            {
                asd.cname = row["Field_Name"].ToString();
                asd.cval = row["Field_Value"].ToString();
                ad.Add(asd);
            }
        }

        foreach (DataRow row in dsm.Tables[1].Rows)
        {
            RetailerData asd = new RetailerData();

            if (row["Field_Type"].ToString() == "frztype")
            {
                asd.ftypename = row["Field_Name"].ToString();
                asd.ftypeval = row["Field_Value"].ToString();
                ad.Add(asd);
            }
        }
        return ad.ToArray();
    }

    public static DataSet getRetailerDetails(string lstdrcode)
    {
        string strQry = "";
        DataSet dsAdmin = new DataSet();
        DBase_EReport.DB_EReporting dbER = new DBase_EReport.DB_EReporting();
        strQry = "select ListedDrCode,SF_Code,ListedDr_Name,ListedDr_Mobile,Code,Contact_Person_Name,Doc_Special_Code,Doc_Spec_ShortName," +
            "Tin_No,sales_Taxno,Territory_Code,Credit_Days,Doc_ClsCode,Doc_Class_ShortName,Advance_amount,ListedDr_Address1,ListedDr_Address2,Division_Code,ListedDr_Active_Flag," +
            "ListedDr_Created_Date,LastUpdt_Date,Milk_Potential,UOM,UOM_Name,TERRCODE,DIST_NAME,Retailer_Type,outstanding,Credit_Limit,Cus_Alter,Doc_Cat_Code,Doc_Cat_ShortName," +
            "ListedDr_Class_Patients,ListedDr_Consultation_Fee,Purchase_slab,ListedDr_Email,Pan_No, Freezer_Tag_no, Freezer_status,Freezer_Type,Tds,Tcs,isnull(State_Code,'')State_Code," +
            "Retailer_Type RetType,FssiNo as FssiNo, TaxGroup as Taxgrp, isnull(convert(varchar(10),Freezer_DOD,23),'') as Frezdate,Doc_SubCatCode,Price_List_Name,Doc_QuaCode,Doc_Qua_Name,Outlet_Type,Delivery_Mode,Allowance_Type,Dist_name,isnull(ListedDr_SubName,'')Sub_Name,isnull(ListedDr_Phone,'')SecMobile_No,isnull(Aadhar_No,'')Aadhar_No " +
            " from Mas_ListedDr  where ListedDrCode='" + lstdrcode + "' " +
            "select * from Retailer_AddFields where Sf_Code='" + lstdrcode + "'";
        try
        {
            dsAdmin = dbER.Exec_DataSet(strQry);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return dsAdmin;
    }

    [WebMethod]
    public static string GetProducts()
    {
        string div_code = HttpContext.Current.Session["div_code"].ToString();
        div_code = div_code.TrimEnd(',');
        DataSet ds = new DataSet();
        if (sf_type == "4")
        {
            ds = getDataSet("select e.Move_MailFolder_Id,e.Move_MailFolder_Name,d.* from Mas_Product_Detail d inner join Mas_Multi_Unit_Entry e on cast(d.Default_UOM as varchar)= cast(e.Move_MailFolder_Id as varchar) where d.Division_Code='" + div_code + "' and charindex(','+'" + Sub_div_Code + "'+',',',' + d.subdivision_code +',')>0 and Product_Active_Flag=0 ");
        }
        else
        {
            ds = getDataSet("select e.Move_MailFolder_Id,e.Move_MailFolder_Name,d.* from Mas_Product_Detail d inner join Mas_Multi_Unit_Entry e on cast(d.Default_UOM as varchar)= cast(e.Move_MailFolder_Id as varchar) where d.Division_Code='" + div_code + "' and Product_Active_Flag=0 ");
        }
        //DataSet ds = getDataSet("select * from Mas_Product_Detail where Division_Code='" + div_code + "' and Product_Active_Flag=0");        
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }

    [WebMethod(EnableSession = true)]
    public static string Get_MRPRate_detail()
    {
        DataSet ds = new DataSet();

        ds = getDataSet("Exec sp_get_mrp_details '" + sf_code + "'");

        //   if (sf_type == "4")
        // {
        //   ds = getDataSet("Exec sp_get_mrp_details '" + sf_code + "'");
        //}
        //else
        //{
        //  ds = getDataSet("select Product_Code,MRP from Mas_Distributor_Rate_details group by Product_Code,MRP order by MRP desc");
        //}
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }

    [WebMethod(EnableSession = true)]
    public static string Get_price_card_details(string retailerCode)
    {
        string div_code = HttpContext.Current.Session["div_code"].ToString();
        div_code = div_code.TrimEnd(',');
        DataSet ds = getDataSet("Exec Sp_Get_price_card_details '" + div_code + "','" + retailerCode + "'");
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }

    public static string SaveBulkPriceOrder(string divcode, string BodyBulkDataxml, string Retail_code, string Name, string Active_flag, string subdivcode, string Rate_Effective_Date)
    {
        string strQry = string.Empty;
        DataSet ds = new DataSet();
        SqlConnection con = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString);
        con.Open();
        using (var scope = new TransactionScope())
        {
            try
            {
                SqlCommand cmd = new SqlCommand("exec new_sp_Insert_BulkWise_ProductRate '" + divcode + "','" + BodyBulkDataxml + "','" + Retail_code + "','" + Name + "','" + Active_flag + "','" + subdivcode + "','" + sf_code + "','" + Rate_Effective_Date + "'", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                scope.Dispose();
                throw ex;
            }
        }
        con.Close();
        return "success";
    }

    [WebMethod(EnableSession = true)]
    public static string Get_Product_unit()
    {
        string Stockist_Code = HttpContext.Current.Session["Sf_Code"].ToString();
        string div_code = HttpContext.Current.Session["div_code"].ToString();
        div_code = div_code.TrimEnd(',');
        DataSet ds = getDataSet("Exec get_Hap_pro_unit '" + div_code + "','" + Stockist_Code + "'");
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }

    [WebMethod(EnableSession = true)]
    public static string Get_hierarchy_detail()
    {
        string div_code = HttpContext.Current.Session["div_code"].ToString();
        div_code = div_code.TrimEnd(',');
        DataSet ds = getDataSet("Exec Sp_Get_hierarchy_details '" + div_code + "','" + sf_code + "'");
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }

    [WebMethod(EnableSession = true)]
    public static string Get_Dis_detail()
    {
        string div_code = HttpContext.Current.Session["div_code"].ToString();
        string Stockist_Code = HttpContext.Current.Session["Sf_Code"].ToString();
        div_code = div_code.TrimEnd(',');
        DataSet ds = null;
        if (sf_type == "4")
        {
            ds = getDataSet("select * from mas_stockist where Stockist_Active_Flag=0  and Stockist_Code = '" + Stockist_Code + "' order by Stockist_Name");
        }
        else
        {
            ds = getDataSet("select * from mas_stockist where Stockist_Active_Flag=0 order by Stockist_Name");
        }
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    [WebMethod(EnableSession = true)]
    public static string FillSubName()
    {
        DataSet ds = getDataSet("select isnull(ListedDr_SubName,'')ListedDr_SubName from Mas_ListedDr group by ListedDr_SubName order by ListedDr_SubName");
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }

    public static DataSet getDataSet(string qrystring)
    {
        DBase_EReport.DB_EReporting db_ER = new DBase_EReport.DB_EReporting();
        DataSet dsSF = null;
        string strQry = qrystring;

        try
        {
            dsSF = db_ER.Exec_DataSet(strQry);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return dsSF;
    }




}