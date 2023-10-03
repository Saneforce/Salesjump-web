using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
using System.Web.Services;
using Newtonsoft.Json;
using DBase_EReport;
using ClosedXML.Excel;
using System.IO;

public partial class MasterFiles_ProductCategoryList : System.Web.UI.Page
{
    string divcode = string.Empty;
    string div_code = string.Empty;
    string sf_type = string.Empty;
    string ProdCatCode = string.Empty;
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
        div_code = Session["div_code"].ToString();
    }
    [WebMethod(EnableSession = true)]
    public static string GetDetails(string divcode)
    {
        Cattst dv = new Cattst();
        DataSet dsProCat = dv.getProCat(divcode);
        return JsonConvert.SerializeObject(dsProCat.Tables[0]);
    }
    [WebMethod(EnableSession = true)]
    public static string Prod_Count(string div_code, string ProdCatCode)
    {
        Product dv = new Product();
        DataSet dsProd = dv.getProdforcat(div_code, ProdCatCode);
        return JsonConvert.SerializeObject(dsProd.Tables[0]);
    }
    [WebMethod(EnableSession = true)]
    public static string deactivate(int ProCatCode, string stat)
    {
        //Product dv = new Product();
        Cattst dv = new Cattst();
        int iReturn = dv.DeActivate1(ProCatCode, stat);
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
        Cattst dv = new Cattst();
        //Product dv = new Product();
        dsProd1 = dv.getProdcat_EX(div_code);
        using (XLWorkbook wb = new XLWorkbook())
        {
            wb.Worksheets.Add(dsProd1, "ProductCategoryList");
            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=ProductCategoryList.xlsx");
            using (MemoryStream MyMemoryStream = new MemoryStream())
            {
                wb.SaveAs(MyMemoryStream);
                MyMemoryStream.WriteTo(Response.OutputStream);
                Response.Flush();
                Response.End();
            }
        }
    }
    public class Cattst
    {
        public DataSet getProCat(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            string strQry = " SELECT c.Product_Cat_Code,c.Product_Cat_SName,c.Product_Cat_Name,c.Product_Cat_Div_Name,c.Product_Cat_Active_Flag, " +
                     " (select COUNT(p.Product_Cat_Code) from Mas_Product_Detail p where p.Product_Active_Flag=0 and p.Product_Cat_Code = c.Product_Cat_Code ) as cat_count   FROM  Mas_Product_Category c" +
                     " WHERE Division_Code= '" + divcode + "' " +
                     " ORDER BY c.ProdCat_SNo";
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
        public int DeActivate1(int Product_Cat_Code, string stat)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                string strQry = "UPDATE Mas_Product_Category " +
                            " SET Product_Cat_Active_Flag='" + stat + "', " +
                           " LastUpdt_Date = getdate() " +
                            " WHERE Product_Cat_Code = '" + Product_Cat_Code + "' ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;

        }
        public DataTable getProdcat_EX(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dsDivision = null;


            string strQry = " SELECT c.Product_Cat_SName as CategoryCode,c.Product_Cat_Name as CategoryName,c.Product_Cat_Div_Name as Division, " +
                     " (select COUNT(p.Product_Cat_Code) from Mas_Product_Detail p where p.Product_Active_Flag=0 and p.Product_Cat_Code = c.Product_Cat_Code ) as NoofProducts FROM  Mas_Product_Category c" +
                     " WHERE Product_Cat_Active_Flag=0 and Division_Code= '" + divcode + "' " +
                     " ORDER BY c.ProdCat_SNo";

            try
            {
                dsDivision = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsDivision;
        }
    }

}