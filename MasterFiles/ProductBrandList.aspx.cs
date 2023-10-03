using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bus_EReport;
using System.Data;
using System.Web.Services;
using Newtonsoft.Json;
using DBase_EReport;
using ClosedXML.Excel;
using System.IO;

public partial class MasterFiles_ProductBrandList : System.Web.UI.Page
{
    string divcode = string.Empty;
    string sf_type = string.Empty;
    string div_code = string.Empty;
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
        //Product dv = new Product();
        Brdtst dv = new Brdtst();
        DataSet ds = dv.getProBrd(divcode);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    [WebMethod(EnableSession = true)]
    public static string Prod_Count(string div_code, string ProdBrdCode)
    {
        Product dv = new Product();
        DataSet dsProd = dv.getProdforbrd(div_code, ProdBrdCode);
        return JsonConvert.SerializeObject(dsProd.Tables[0]);
    }
	[WebMethod(EnableSession = true)]
    public static string deactivate(int ProBrdCode, string stat)
    {
        //Product dv = new Product();
        Brdtst dv = new Brdtst();
        int iReturn = dv.Brd_DeActivate1(ProBrdCode, stat);
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
        Brdtst dv = new Brdtst();
        //Product dv = new Product();
        dsProd1 = dv.getProdbrd_EX(div_code);
        using (XLWorkbook wb = new XLWorkbook())
        {
            wb.Worksheets.Add(dsProd1, "ProductBrandList");
            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=ProductBrandList.xlsx");
            using (MemoryStream MyMemoryStream = new MemoryStream())
            {
                wb.SaveAs(MyMemoryStream);
                MyMemoryStream.WriteTo(Response.OutputStream);
                Response.Flush();
                Response.End();
            }
        }
    }
    public class Brdtst
    {
        public DataSet getProBrd(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProBrd = null;

            string strQry = " SELECT b.Product_Brd_Code,b.Product_Brd_SName,b.Product_Brd_Name,b.Product_Cat_Name,b.Product_Cat_Div_Code,b.Product_Cat_Div_Name,b.Product_Brd_Active_Flag," +
                     " (select COUNT(p.Product_Brd_Code) from Mas_Product_Detail p where p.Product_Active_Flag=0 and p.Product_Brd_Code = b.Product_Brd_Code ) as brd_count   FROM  Mas_Product_Brand b" +
                     " WHERE Division_Code= '" + divcode + "' " +
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
        public int Brd_DeActivate1(int ProBrdCode, string stat)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                string strQry = "UPDATE Mas_Product_Brand " +
                            " SET Product_Brd_Active_Flag='"+stat+"' ," +
                           " LastUpdt_Date = getdate() " +
                            " WHERE Product_Brd_Code = '" + ProBrdCode + "'";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;
        }
        public DataTable getProdbrd_EX(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dsDivision = null;


            string strQry = " SELECT b.Product_Brd_SName as BrandCode,b.Product_Brd_Name as BrandName,b.Product_Cat_Name as Category,b.Product_Cat_Div_Name as Division, " +
                     " (select COUNT(p.Product_Brd_Code) from Mas_Product_Detail p where p.Product_Active_Flag=0 and p.Product_Brd_Code = b.Product_Brd_Code ) as NoofProducts   FROM  Mas_Product_Brand b" +
                     " WHERE b.Product_Brd_Active_Flag=0 AND Division_Code='" + divcode + "' " +
                     " ORDER BY b.Product_Brd_SNO";

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