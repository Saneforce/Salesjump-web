using System;
using System.Data;
using Bus_EReport;
using System.Web.Services;
using Newtonsoft.Json;
using ClosedXML.Excel;
using System.IO;
using DBase_EReport;

public partial class MasterFiles_ProductList : System.Web.UI.Page
{
    #region "Declaration"
    DataTable dsProd1 = null;
    DataSet dsProd = null;
    DataSet dsProduct = null;
    DataSet dsDivision = null;
    DataSet dsState = null;
    string[] statecd;
    string state_cd = string.Empty;
    string sState = string.Empty;
    string div_code = string.Empty;
    string ProdCode = string.Empty;
    string ProdDescr = string.Empty;
    string ProdName = string.Empty;
    string ProdSaleUnit = string.Empty;
    string sCmd = string.Empty;
    string Char = string.Empty;
    string val = string.Empty;
    string search = string.Empty;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int time;
    public static string sUSR = string.Empty;
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        sUSR = Request.Url.Host;//.ToLower().Replace("www.", "").Replace(".sanfmcg.com", "").ToLower();
        try
        {
            div_code = Session["div_code"].ToString();
        }
        catch
        {
            div_code = Session["Division_Code"].ToString();
        }

    }
    [WebMethod]
    public static string GetcatDetails(string divcode)
    {
        Product SFD = new Product();
        DataSet dds = SFD.getProductCategory(divcode);
        return JsonConvert.SerializeObject(dds.Tables[0]);
    }
    [WebMethod]
    public static string GetDetails(string divcode)
    {
        DCR dv = new DCR();
        DataTable ds = new DataTable();
        ds = dv.getDataTable("exec getProdetailMaster '" + divcode + "','http://" + sUSR + "'");
        return JsonConvert.SerializeObject(ds);
    }


    [WebMethod]
    public static string getbrand(string divcode)
    {
        Product SFD = new Product();
        DataSet dds = SFD.getProductBrand(divcode);
        return JsonConvert.SerializeObject(dds.Tables[0]);
    }
    [WebMethod]
    public static string getsubDivisions(string divcode)
    {
        Product SFD = new Product();
        DataSet dds = SFD.getSubdiv(divcode);
        return JsonConvert.SerializeObject(dds.Tables[0]);
    }
    [WebMethod]
    public static string getstat(string divcode)
    {
        SalesForce SFD = new SalesForce();
        DataSet dds = SFD.getAllSF_States(divcode);
        return JsonConvert.SerializeObject(dds.Tables[0]);
    }
    [WebMethod]
    public static string deactivateprodtl(string arcode, string stat)
    {
        Stockist dv = new Stockist();
        int iReturn = dv.prodtlDeActivate(arcode, stat);
        if (iReturn > 0)
        {
            return "Success";
        }
        else
        {
            return "Failure";
        }
    }
    protected void lnkDownload_Click(object sender, EventArgs e)
    {

        DataTable dsProd1 = null;
        prod prd = new prod();
        dsProd1 = prd.getProdall_EX(div_code);
        using (XLWorkbook wb = new XLWorkbook())
        {
            wb.Worksheets.Add(dsProd1, "Product List Master");
            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=Product_List.xlsx");
            using (MemoryStream MyMemoryStream = new MemoryStream())
            {
                wb.SaveAs(MyMemoryStream);
                MyMemoryStream.WriteTo(Response.OutputStream);
                Response.Flush();
                Response.End();
            }
        }
    }
	public class prod
	{
		public DataTable getProdall_EX(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dsProCat = null;

            string strQry = "  SELECT a.Product_Detail_Code as ProductCode ,a.Product_Detail_Name as ProductName,a.Product_Short_Name as Short_Name,a.Product_Description as ProductDescription,a.Sample_Erp_Code as ConversionFactor," +
                     " a.product_grosswt as Grossweight ,a.product_netwt as Netweight,a.product_unit as UOM,a.Product_Sale_Unit as Base_UOM,c.subdivision_name as Sub_Division_Name, a.Sale_Erp_Code as ERP_Code, d.Product_Brd_Name as Brand, b.Product_Cat_Name " +
                     " FROM  Mas_Product_Detail a,Mas_Product_Category b,mas_subdivision c,Mas_Product_Brand d " +
                     " WHERE a.product_cat_code = b.product_cat_code AND " +
                     "a.Product_Brd_Code = d.Product_Brd_Code AND " +
                     " CharIndex(',' + Cast(c.subdivision_code as varchar) + ',', ',' + Cast(a.subdivision_code as varchar) + ',') > 0 AND "+
                     "a.Product_Active_Flag=0 AND a.Division_Code= '" + divcode + "' " +
                     " ORDER BY Prod_Detail_Sl_No";
            try
            {
                dsProCat = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProCat;
        }
	}
}