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
using System.Configuration;
using System.IO;
using System.Drawing;
using DBase_EReport;

public partial class MasterFiles_ProductBrandList : System.Web.UI.Page
{

    #region "Declaration"
    public string div_code;
    public string div_code1;
    public string sf_code = string.Empty;
    public string sf_type = string.Empty;
    #endregion
    protected override void OnPreInit(EventArgs e)
    {
        base.OnPreInit(e);
        sf_type = Session["sf_type"].ToString();
        if (sf_type == "3")
        {
            this.MasterPageFile = "~/Master_DIS.master";
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

        try
        {
            div_code = Session["div_code"].ToString();
            div_code1 = Session["div_code"].ToString();
            sf_code = Session["Sf_Code"].ToString();
        }
        catch
        {
            div_code = Session["Division_Code"].ToString();
        }
    }
    [WebMethod]
    public static string GetProductBrands(string div)
    {
        StockistMaster Rut = new StockistMaster();
        DataSet ds = Stockist_getProBrd(div.Replace(",", ""));
      //  DataSet ds = Rut.Stockist_getProBrd(div.Replace(",", ""));
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    public static DataSet Stockist_getProBrd(string divcode)
    {

        DB_EReporting db_ER = new DB_EReporting();

        DataSet dsProBrd = null;

        string strQry = " SELECT b.Product_Brd_Code,b.Product_Brd_SName,b.Product_Brd_Name,b.Product_Cat_Name,b.Product_Cat_Div_Code,b.Product_Cat_Div_Name," +
                 " (select COUNT(p.Product_Brd_Code) from Mas_Product_Detail p where p.Product_Active_Flag=0 and p.Product_Brd_Code = b.Product_Brd_Code ) as brd_count   FROM  Mas_Product_Brand b" +
                 " WHERE b.Product_Brd_Active_Flag=0 AND Division_Code= '" + divcode + "' " +
                 " ORDER BY b.Product_Brd_SNO";
        try
        {
            dsProBrd = db_ER.Exec_DataSet(strQry);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return dsProBrd;
    }
    [WebMethod]
    public static string Get_product_details(string div)
    {
        StockistMaster Rut = new StockistMaster();
        DataSet ds = Getdivisionproduct(div.Replace(",", ""));
        //DataSet ds = Rut.Getdivisionproduct(div.Replace(",", ""));
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    public static DataSet Getdivisionproduct(string Div_Code)
    {
        DB_EReporting db_ER = new DB_EReporting();
        DataSet ds = null;

        string strQry = "select * from mas_product_detail where Division_Code='" + Div_Code + "' and Product_Active_Flag='0'";

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