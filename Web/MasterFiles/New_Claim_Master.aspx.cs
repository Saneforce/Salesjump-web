using Bus_EReport;
using DBase_EReport;
using Newtonsoft.Json;
using System;
using System.Data;
using System.Web.Services;

public partial class MasterFiles_New_Claim_Master : System.Web.UI.Page
{
    public static string div_code = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
    }
    [WebMethod(EnableSession = true)]
    public static string getSalesSlab(string clmtyp)
    {
        loc ast = new loc();
        DataSet ds = ast.getslabs(div_code,clmtyp);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    [WebMethod(EnableSession = true)]
    public static string getBilledClm(string clmtyp)
    {
        loc ast = new loc();
        DataSet ds = ast.getslabs(div_code, clmtyp);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    [WebMethod(EnableSession = true)]
    public static string GetProductMaster(string divcode)
    {
        loc ast = new loc();
        DataSet ds = ast.getAllProds(divcode);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    [WebMethod(EnableSession = true)]
    public static string GetRetCategoryMaster(string divcode)
    {
        loc ast = new loc();
        DataSet ds = ast.getAllCatgry(divcode);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    [WebMethod(EnableSession = true)]
    public static string GetRetailerMaster(string divcode)
    {
        loc ast = new loc();
        DataSet ds = ast.getAllRetails(divcode);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    [WebMethod(EnableSession = true)]
    public static string GetHQMaster(string divcode)
    {
        loc ast = new loc();
        DataSet ds = ast.getAllHQ(divcode);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    [WebMethod(EnableSession = true)]
    public static string GetFilteredRet(string hq)
    {
        loc ast = new loc();
        DataSet ds = ast.getFillRet(hq);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    [WebMethod(EnableSession = true)]
    public static string GetClaimProduct(string divcode)
    {
        loc ast = new loc();
        DataSet ds = ast.getAllGift(divcode);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }

    [WebMethod(EnableSession = true)]
    public static string Save_Gift(string prodnm, string prodprice, string fdt, string tdt,string gpscode,string prodtyp)
    {
        string ds = string.Empty;
        loc ast = new loc();
        ds = ast.insertgift(div_code, prodnm, prodprice, fdt,tdt, gpscode, prodtyp);
        return ds;
    }

    [WebMethod(EnableSession = true)]
    public static string Save_SalesSlab(string data)
    {
        string ds = string.Empty;
        loc.SaveGiftSlab Data = JsonConvert.DeserializeObject<loc.SaveGiftSlab>(data);
        loc ast = new loc();
        ds = ast.insertsaleslab(Data);
        return ds;
    }
    [WebMethod(EnableSession = true)]
    public static string Save_BilledProd(string data)
    {
        string ds = string.Empty;
        loc.SaveBillSlab Data = JsonConvert.DeserializeObject<loc.SaveBillSlab>(data);
        loc ast = new loc();
        ds = ast.insertbilledslab(Data);
        return ds;
    }
    [WebMethod(EnableSession = true)]
    public static string getAllGiftProducts()
    {
        Sales ast = new Sales();
        DataSet ds = ast.getAllGiftsProd(div_code);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    [WebMethod(EnableSession = true)]
    public static string getGiftProductsID(string divcode)
    {
        Sales cp = new Sales();
        DataSet ds = cp.getGiftProdID(divcode);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }

    [WebMethod(EnableSession = true)]
    public static string getGiftProdUpdt(string scode)
    {
        Sales cp = new Sales();
        DataSet ds = cp.getGiftProducts(scode, div_code);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }

    [WebMethod(EnableSession = true)]
    public static string getGiftUpdt(string scode)
    {
        loc cp = new loc();
        DataSet ds = cp.getGiftSlab(scode, div_code);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    [WebMethod(EnableSession = true)]
    public static string getBillUpdt(string bcode)
    {
        loc cp = new loc();
        DataSet ds = cp.getGiftSlab(bcode, div_code);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }

    [WebMethod(EnableSession = true)]
    public static string Hq_Count(string sdcode)
    {
        loc cp = new loc();
        DataSet ds = cp.gethqdet(sdcode);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
	[WebMethod(EnableSession = true)]
    public static string billed_product(string sdcode)
    {
        loc cp = new loc();
        DataSet ds = cp.getbilprod(sdcode);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    [WebMethod(EnableSession = true)]
    public static string Retail_Count(string sdcode)
    {
        loc cp = new loc();
        DataSet ds = cp.getretdet(sdcode);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }

    [WebMethod(EnableSession = true)]
    public static int SetNewStatus(string ID, string stus)
    {
        loc ast = new loc();
        int iReturn = ast.SlabDeActivate(ID, stus);
        return iReturn;
    }
    [WebMethod(EnableSession = true)]
    public static int NewStatus(string ID, string stus)
    {
        loc ast = new loc();
        int iReturn = ast.GiftDeActivate(ID, stus);
        return iReturn;
    }

    public class loc
    {
        public class SaveBillSlab
        {
            [JsonProperty("billid")]
            public object billid { get; set; }

            [JsonProperty("billnm")]
            public object billnm { get; set; }

            [JsonProperty("clmdesc")]
            public object clmdesc { get; set; }

            [JsonProperty("invnd")]
            public object invnd { get; set; }

            [JsonProperty("billval")]
            public object billval { get; set; }

            [JsonProperty("clmtyp")]
            public object clmtyp { get; set; }

            [JsonProperty("biltyp")]
            public object biltyp { get; set; }

            [JsonProperty("clmval")]
            public object clmval { get; set; }

            [JsonProperty("Gtype")]
            public object gtype { get; set; }

            [JsonProperty("FDT")]
            public object gfdt { get; set; }

            [JsonProperty("TDT")]
            public object gtdt { get; set; }

            [JsonProperty("hqdtl")]
            public object hqid { get; set; }

            [JsonProperty("retail")]
            public object retail { get; set; }

            [JsonProperty("Gprod")]
            public object Gprod { get; set; }

            [JsonProperty("ClmEndDt")]
            public object ClmEndDt { get; set; }
            [JsonProperty("pcode")]
            public object pcode { get; set; }


        }
        public class SaveGiftSlab
        {
            [JsonProperty("slbid")]
            public object slbid { get; set; }

            [JsonProperty("slabnm")]
            public object slabnm { get; set; }

            [JsonProperty("clmdesc")]
            public object clmdesc { get; set; }

            [JsonProperty("mnval")]
            public object mnval { get; set; }

            [JsonProperty("mxval")]
            public object mxval { get; set; }

            [JsonProperty("clmtyp")]
            public object clmtyp { get; set; }

            [JsonProperty("clmval")]
            public object clmval { get; set; }

            [JsonProperty("Gtype")]
            public object gtype { get; set; }

            [JsonProperty("FDT")]
            public object gfdt { get; set; }

            [JsonProperty("TDT")]
            public object gtdt { get; set; }

            [JsonProperty("hqdtl")]
            public object hqid { get; set; }

            [JsonProperty("retail")]
            public object retail { get; set; }

            [JsonProperty("Gprod")]
            public object Gprod { get; set; }

            [JsonProperty("ClmEndDt")]
            public object ClmEndDt { get; set; }

            
        }
        public int GiftDeActivate(string plcode, string stus)
        {
            int iReturn = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();
                string strQry = "update Mas_Gift_Products set Active_Flag='" + stus + "' where sl_no='" + plcode + "'";
                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }
        public int SlabDeActivate(string plcode, string stus)
        {
            int iReturn = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();
                string strQry = "update mas_gift_slab set ActiveFlag='" + stus + "' where GiftSlabID='" + plcode + "'";
                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }
        public DataSet getGiftSlab(string scode, string divcode)
        {
            DB_EReporting db = new DB_EReporting();
            DataSet ds = null;
            string strQry = "select SUBSTRING(GiftDesc, CHARINDEX('-', GiftDesc)  + 1, LEN(GiftDesc))+'-'+cast((RIGHT('00'+cast(charindex(SUBSTRING(GiftDesc, 0, CHARINDEX('-', GiftDesc)),'JAN FEB MAR APR MAY JUN JUL AUG SEP OCT NOV DEC')/4+1 as varchar),2)) as varchar)as GiftDesc1, * from Mas_Gift_Slab where Division_Code=" + divcode + " and GiftSlabID='" + scode + "'";
            try
            {
                ds = db.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
        public string insertbilledslab(SaveBillSlab ss)
        {
            DB_EReporting db = new DB_EReporting();
            DataSet ds = null;
            string msg = string.Empty;

            DataSet billid = null;

            billid = db.Exec_DataSet("select replace('B-'+convert(varchar(5),cast(getdate() as time),126)+'-SLAB-'+cast((select Mx_slno from max_sl_no)+1 as varchar),':','-') as SLABID");
            db.Exec_DataSet("update max_sl_no set Mx_slno=Mx_slno+1");

            string strQry = "exec sp_insertsalesslab '" + ss.billnm + "','" + ss.clmdesc + "','','','" + ss.clmtyp + "','" + ss.clmval + "','" + ss.gfdt + "','" + ss.gtdt + "','" + ss.retail + "','" + ss.gtype + "','" + ss.hqid + "','" + ss.Gprod + "','" + ss.ClmEndDt + "','" + billid.Tables[0].Rows[0].ItemArray[0] + "','" + div_code + "','" + ss.billid + "','"+ ss.biltyp + "','"+ ss.invnd + "','"+ ss.billval + "','P','"+ss.pcode + "'";
            try
            {
                ds = db.Exec_DataSet(strQry);
                msg = "Success";
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            return msg;
        }
        public string insertsaleslab(SaveGiftSlab ss)
        {
            DB_EReporting db = new DB_EReporting();
            DataSet ds = null;
            string msg = string.Empty;

            DataSet slabid = null;

            slabid = db.Exec_DataSet("select replace('G-'+convert(varchar(5),cast(getdate() as time),126)+'-SLAB-'+cast((select Mx_slno from max_sl_no)+1 as varchar),':','-') as SLABID");
            db.Exec_DataSet("update max_sl_no set Mx_slno=Mx_slno+1");

            string strQry = "exec sp_insertsalesslab '" + ss.slabnm + "','" + ss.clmdesc + "','" + ss.mnval + "','" + ss.mxval + "','" + ss.clmtyp + "','" + ss.clmval + "','" + ss.gfdt + "','" + ss.gtdt + "','" + ss.retail + "','" + ss.gtype + "','" + ss.hqid + "','"+ss.Gprod + "','"+ss.ClmEndDt + "','"+ slabid.Tables[0].Rows[0].ItemArray[0] + "','"+ div_code + "','"+ss.slbid + "','','','','S',''";
            try
            {
                ds = db.Exec_DataSet(strQry);
                msg = "Success";
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            return msg;
        }
        public DataSet getslabs(string divcode,string clmtyp)
        {
            DB_EReporting db = new DB_EReporting();
            DataSet ds = null;
            string strQry = "exec sp_get_slab '"+ divcode + "','"+ clmtyp + "'";
            try
            {
                ds = db.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
        public string insertgift(string divcode, string prodnm, string prodprice, string fdt, string tdt, string gpscode,string prodtyp)
        {
            DB_EReporting db = new DB_EReporting();
            DataSet ds = null;
            string msg = string.Empty;
            string strQry = "exec insertClaimProducts '" + gpscode + "','" + divcode + "','" + prodnm + "','" + prodprice + "','" + fdt + "','" + tdt + "','"+ prodtyp + "'";
            try
            {
                ds = db.Exec_DataSet(strQry);
                msg = "Success";
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            return msg;
        }
        public DataSet getAllCatgry(string divcode)
        {
            DB_EReporting db = new DB_EReporting();
            DataSet ds = null;
            string strQry = "select Doc_Cat_Code,Doc_Cat_Name from Mas_Doctor_Category where Doc_Cat_Active_Flag=0 and Division_Code='" + divcode + "'";
            try
            {
                ds = db.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
        public DataSet getretdet(string divcode)
        {
            DB_EReporting db = new DB_EReporting();
            DataSet ds = null;
            string strQry = "select ListeddrCode,Listeddr_Name from Gift_Map_Customer where Slab_Id='" + divcode + "'";
            try
            {
                ds = db.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
        public DataSet gethqdet(string divcode)
        {
            DB_EReporting db = new DB_EReporting();
            DataSet ds = null;
            string strQry = "select Hq_Id HQ_ID,Hq_Name HQ_Name from Gift_Map_Hq where Slab_Id='"+ divcode + "'";
            try
            {
                ds = db.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
		public DataSet getbilprod(string divcode)
        {
            DB_EReporting db = new DB_EReporting();
            DataSet ds = null;
            string strQry = "select Product_Detail_Code,Product_Detail_Name from mas_gift_slab s inner join Mas_Product_Detail pd on charindex(','+pd.Product_Detail_Code +',',','+s.billed_products+',')>0 where s.Division_Code='" + div_code + "' and GiftSlabID='" + divcode + "'";
            try
            {
                ds = db.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
        public DataSet getAllGift(string divcode)
        {
            DB_EReporting db = new DB_EReporting();
            DataSet ds = null;
            string strQry = "select Product_Code,Product_Name from mas_gift_products where Active_Flag=0 and Division_Code='" + divcode + "'";
            try
            {
                ds = db.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
        public DataSet getAllHQ(string divcode)
        {
            DB_EReporting db = new DB_EReporting();
            DataSet ds = null;
            string strQry = "select HQ_ID,HQ_Name from mas_hquarters where HQ_Active_Flag=0 and division_code='" + divcode + "'";
            try
            {
                ds = db.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
        public DataSet getFillRet(string hq)
        {
            DB_EReporting db = new DB_EReporting();
            DataSet ds = null;
            string strQry = "select distinct listeddrcode,ListedDr_Name from mas_salesforce ms inner join mas_listeddr dr on charindex(','+ms.sf_code+',',','+dr.Sf_Code+',')>0 where dr.division_code = '"+ div_code + "' and charindex(',' + Hq_Code + ',',',"+ hq +",')> 0 and sf_status = 0 and listeddr_active_flag = 0 ";
            try
            {
                ds = db.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet getAllRetails(string divcode)
        {
            DB_EReporting db = new DB_EReporting();
            DataSet ds = null;
            string strQry = "select listeddrcode,listeddr_name from mas_listeddr where listeddr_active_flag=0 and division_code='" + divcode + "'";
            try
            {
                ds = db.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
        public DataSet getAllProds(string divcode)
        {
            DB_EReporting db = new DB_EReporting();
            DataSet ds = null;
            string strQry = "select row_number() over(partition by pd.Product_Cat_Code order by pd.Product_Cat_Code)row_nm,Product_Detail_Name,Product_Detail_Code,Product_Cat_Name,pd.Product_Cat_Code from mas_product_detail pd inner join mas_product_category pc on pd.Product_Cat_Code=pc.Product_Cat_Code where product_active_flag=0 and pd.division_code='" + divcode + "'";
            try
            {
                ds = db.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
    }
}