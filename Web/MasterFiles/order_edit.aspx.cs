using DBase_EReport;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterFiles_order_edit : System.Web.UI.Page
{
    public static string orderno = string.Empty;
    public static string sfcode = string.Empty;
    public static string stkcode = string.Empty;
    public static string ordate = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        orderno = Request.QueryString["odcode"];
        sfcode = Request.QueryString["sfcode"];
        stkcode = Request.QueryString["stckde"];
        ordate= Request.QueryString["ordete"];
    }
    [WebMethod]
    public static string GetProducts(string divcode)
    {
        DataSet ds = getProdall(divcode.Replace(",", ""));
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    [WebMethod]
    public static string Get_Product_unit(string divcode,string sfcode)
    {
        divcode = divcode.TrimEnd(',');
        secord sm = new secord();
        DataSet ds = sm.gets_Product_unit_details(divcode, sfcode);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    [WebMethod]
    public static string get_product_fedit(string divcode, string trans_slno,string sfcode)
    {
        DataSet ds = getDataSet("exec get_product_byorderno '" + trans_slno + "','" + divcode + "','"+ sfcode + "'");
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    [WebMethod]
    public static string getratenew(string Div_Code, string sf_Code,string stk_code)
    {
        DataSet ds = new DataSet();
        secord sm = new secord();
        ds = sm.get_rate_new(Div_Code.TrimEnd(','), sf_Code, stk_code);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    [WebMethod]
    public static string saveorders(string NewOrd, string Ordrval, string RecDate, string Ntwt, string Type, string ref_order, string sub_total, string dis_total, string tax_total, string Ord_id)
    {
        string msg = string.Empty;
		string dcrupd_pcode = string.Empty;
        string dcrupd_pname = string.Empty;
        string popval = string.Empty;
        DB_EReporting db_ER = new DB_EReporting();
        string Div_Code = HttpContext.Current.Session["div_code"].ToString();
        Div_Code = Div_Code.TrimEnd(',');
        secord Ord = new secord();
        try
        {
            var items = JsonConvert.DeserializeObject<List<svorders>>(NewOrd);
			 popval = Ordrval;
            string sxml = "<ROOT>";
            for (int k = 0; k < items.Count; k++)
            {
                if (items[k].PCd != "" && items[k].PName != "" && (items[k].Qty != 0 || items[k].Qty_c != "0"))
                {
					 if(items[k].trans_ordno=="")
                    {
                        DataSet qry = getorderno(Ord_id);
                        items[k].trans_ordno =qry.Tables[0].Rows[0].ItemArray[0].ToString();

                    }
					dcrupd_pcode += items[k].PCd + '~' + items[k].Rate + '$' + items[k].Qty + '#';
                    dcrupd_pname += items[k].PName + '~' + items[k].Rate + '$' + items[k].Qty + '#';
                    //sxml += "<Prod PCode=\"" + items[k].PCd + "\" PName=\"" + items[k].PName + "\" Rate=\"" + items[k].Rate + "\" Qty=\"" + items[k].Qty + "\" Val=\"" + items[k].Gross_Amt + "\" FQty=\"" + items[k].Free + "\" DAmt=\"" + items[k].Dis_value + "\" Dval=\"" + items[k].Discount + "\" Md=\"free\" Mfg=\" \" Cl=\" \" Offer_ProductNm=\"" + items[k].of_Pro_Name + "\" Offer_ProductCd=\"" + items[k].of_Pro_Code + "\" Unit=\"" + items[k].Unit + "\"  of_Pro_Unit=\"" + items[k].of_Pro_Unit + "\"  Umo_Code=\"" + items[k].umo_unit + "\"  Qty_C=\"" + items[k].Qty_c + "\" Tax_value=\"" + items[k].Tax_value + "\" Sl_No=\"" + k + "\" con_fac=\"" + items[k].con_fac + "\" baserate=\""+ items[k].dbrate + "\" order_no=\""+items[k].trans_ordno + "\"  />";
					sxml += "<Prod PCode=\"" + items[k].PCd.Trim() + "\" PName=\"" + items[k].PName.Trim() + "\" Rate=\"" + items[k].Rate + "\" Qty=\"" + items[k].Qty + "\" Val=\"" + items[k].Gross_Amt + "\" FQty=\"" + items[k].Free + "\" DAmt=\"" + items[k].Dis_value + "\" Dval=\"" + items[k].Discount + "\" Md=\"free\" Mfg=\"\" Cl=\"\" Offer_ProductNm=\"" + items[k].of_Pro_Name.Trim() + "\" Offer_ProductCd=\"" + items[k].of_Pro_Code.Trim() + "\" Unit=\"" + items[k].Unit.Trim() + "\"  of_Pro_Unit=\"" + items[k].of_Pro_Unit.Trim() + "\"  Umo_Code=\"" + items[k].umo_unit.Trim() + "\"  Qty_C=\"" + items[k].Qty_c.Trim() + "\" Tax_value=\"" + items[k].Tax_value.Trim() + "\" Sl_No=\"" + k + "\" con_fac=\"" + items[k].con_fac.Trim() + "\" baserate=\""+ items[k].dbrate.Trim() + "\" order_no=\""+items[k].trans_ordno.Trim() + "\"  />";
                }
            }
            sxml += "</ROOT>";
            //string strQry = "exec sp_saveEdit_secOrder '" + Ordrval + "','" + RecDate + "','" + Ntwt + "','" + Type + "','" + ref_order + "','" + sub_total + "','" + dis_total + 
            //    "','" + tax_total + "','" + Ord_id + "'," + Div_Code + ",'" + sxml + "'";
			string strQry = "exec sp_saveEdit_secOrder '" + Ordrval.Trim() + "','" + RecDate.Trim() + "','" + Ntwt.Trim() + "','" + Type.Trim() + "','" + ref_order.Trim() + "','" + sub_total.Trim() + "','" + dis_total.Trim() + 
                "','" + tax_total.Trim() + "','" + Ord_id.Trim() + "'," + Div_Code.Trim() + ",'" + sxml.Replace("&", "and") + "'";
            int result = 0;

            result = db_ER.ExecQry(strQry);
			db_ER.ExecQry("update dcrdetail_lst_trans set Product_Code='" + dcrupd_pcode + "',Product_Detail='" + dcrupd_pname + "',POB_Value='" + popval + "' where Trans_Detail_Slno=(select DCR_Code from trans_order_head where Trans_Sl_No='" + Ord_id + "')");
            msg = "Success";
        }
        catch (Exception exp)
        {
            throw exp;
        }
        return msg;
    }
    public class svorders
    {
        public string PCd { get; set; }
        public string PName { get; set; }
        public string Unit { get; set; }
        public float Rate { get; set; }
        public int Qty { get; set; }
        public float Gross_Amt { get; set; }
        public float Free { get; set; }
        public float Dis_value { get; set; }
        public float Discount { get; set; }
        public string of_Pro_Code { get; set; }
        public string of_Pro_Name { get; set; }
        public string of_Pro_Unit { get; set; }
        public string umo_unit { get; set; }
        public string Qty_c { get; set; }
        public string Tax_value { get; set; }
        public string con_fac { get; set; }
        public string dbrate { get; set; }
        public string trans_ordno { get; set; }
    }
    public static DataSet getProdall(string divcode)
    {
        DB_EReporting db_ER = new DB_EReporting();
        DataSet dsProCat = null;

        string strQry = "select Product_Detail_Code,Product_Detail_Name from mas_product_detail where Product_Active_Flag=0 and division_code='" + divcode + "' order by Product_Detail_Name";

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
    public static DataSet getDataSet(string qrystring)
    {

        DB_EReporting db_ER = new DB_EReporting();
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
	 public static DataSet getorderno(string orid)
    {
        DB_EReporting db_ER = new DB_EReporting();
        DataSet dsProCat = null;
        DataSet dsPro = null;

        string strQry = "SELECT Division_SName+cast(SLNo as varchar)+'-'+cast(isnull(OrdDsl,0)+1 as varchar) SLNo FROM Mas_SF_SlNo where SF_Code=(select SF_Code from Trans_Order_Head where Trans_Sl_No='" + orid + "')";
        string qry = "update Mas_SF_SlNo set OrdDsl=isnull(OrdDsl,0)+1 where SF_Code=(select SF_Code from Trans_Order_Head where Trans_Sl_No='" + orid + "')";
        try
        {
            dsProCat = db_ER.Exec_DataSet(strQry);
            dsPro = db_ER.Exec_DataSet(qry);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return dsProCat;
    }
    public class secord
    {
        public DataSet gets_Product_unit_details(string Div_Code, string stk_code)
        {
            DataSet ds = null;
            DB_EReporting db_ER = new DB_EReporting();

            string strQry = "Exec get_pro_unit '" + Div_Code + "','" + stk_code + "'";

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
        public DataSet get_rate_new(string Div_Code, string sf_code,string stk_code)
        {
            DataSet ds = null;
            DB_EReporting db_ER = new DB_EReporting();

            string strQry = "Exec sp_getprod_rate '" + Div_Code + "','" + sf_code + "','"+ stk_code+"'";

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
    }
    }