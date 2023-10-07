using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using System.Web.Services;
using Bus_EReport;
using System.Data;
using System.IO;
using System.Drawing;
using DBase_EReport;

public partial class MasterFiles_ProductDetail_List : System.Web.UI.Page
{
    #region "Declaration"
    public string div_code;
    public string div_code1;
    public string sf_code = string.Empty;
    public string sf_type = string.Empty;
    public string state_code = string.Empty;
    public static string strQry = string.Empty;
    #endregion
    protected override void OnPreInit(EventArgs e)
    {
        base.OnPreInit(e);
        sf_type = Session["sf_type"].ToString();
        //if (sf_type == "3")
        //{
        //    //this.MasterPageFile = "~/Master.master";
        //    this.MasterPageFile = "~/Master_DIS.master";
        //}
        //else if (sf_type == "2")
        //{
        //    this.MasterPageFile = "~/Master_MGR.master";
        //}
        //else if (sf_type == "1")
        //{
        //    this.MasterPageFile = "~/Master_MR.master";
        //}
        this.MasterPageFile = "~/Master_SS.master";
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            div_code = Session["div_code"].ToString();
            div_code1 = Session["div_code"].ToString();
            sf_code = Session["Sf_Code"].ToString();
            state_code = Session["State"].ToString();

        }
        catch
        {
            div_code = Session["Division_Code"].ToString();
            state_code = Session["State"].ToString();
        }

        //Session["Div_code"] = div_code.ToString();

    }
    [WebMethod]
    public static string GetProducts(string div)
    {
        Product Rut = new Product();
        string state_code = HttpContext.Current.Session["State"].ToString();
        string Stockist_Code = HttpContext.Current.Session["Sf_Code"].ToString();
        DataSet ds = getProdall(div.Replace(",", ""), state_code, Stockist_Code);
        //DataSet ds = Rut.getProdall(div.Replace(",", ""), state_code, Stockist_Code);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    public static  DataSet getProdall(string divcode, string state_code, string stk)
    {
        DB_EReporting db_ER = new DB_EReporting();
        DataSet dsProCat = null;

        strQry = "EXEC get_product_details_view '" + divcode + "','" + state_code + "','" + stk + "'";

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

    [WebMethod]
    public static string GetProdBrand(string div)
    {
        string Stockist_Code = HttpContext.Current.Session["Sf_Code"].ToString();
        DataSet ds = getProdBrand(div.Replace(",", ""), Stockist_Code);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }

    public static DataSet getProdBrand(string divcode, string stk)
    {
        DB_EReporting db_ER = new DB_EReporting();
        DataSet dsProCat = null;

        strQry = "EXEC get_product_brands '" + divcode + "','" + stk + "'";

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

}

