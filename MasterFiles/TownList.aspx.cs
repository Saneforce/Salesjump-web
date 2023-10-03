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

public partial class MasterFiles_TownList : System.Web.UI.Page
{
    int subdivision_code = 0;
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
        divcode = Convert.ToString(Session["div_code"]);
    }
    [WebMethod(EnableSession = true)]
    public static string GetDetails(string divcode)
    {
        Towntst dv = new Towntst();
        DataSet ds = dv.TowngetSubDiv_New(divcode);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    [WebMethod(EnableSession = true)]
    public static string deactivate(int subdivision_code, string stat)
    {
        Towntst dv = new Towntst();
        int iReturn = dv.TownDeActivate(subdivision_code, stat);
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
        Towntst dv = new Towntst();
        //Product dv = new Product();
        dsProd1 = dv.getTownList_EX(divcode);
        using (XLWorkbook wb = new XLWorkbook())
        {
            wb.Worksheets.Add(dsProd1, "TownList");
            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=TownList.xlsx");
            using (MemoryStream MyMemoryStream = new MemoryStream())
            {
                wb.SaveAs(MyMemoryStream);
                MyMemoryStream.WriteTo(Response.OutputStream);
                Response.Flush();
                Response.End();
            }
        }
    }
    public class Towntst
    {
        public DataSet TowngetSubDiv_New(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSubDiv = null;


            string strQry = " select a.Town_code,a.Town_sname,a.Town_name,a.Dist,a.Territory_Name,a.Town_Active_Flag " +
                     " From " +
                     " Mas_Town a WHERE a.Div_Code= '" + divcode + "' group by a.Town_name,a.Town_code,a.Town_sname,a.Dist,a.Territory_Name,a.Town_Active_Flag";
            try
            {
                dsSubDiv = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsSubDiv;
        }
        public int TownDeActivate(int subdivision_code,string stat)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();


                string strQry = "UPDATE Mas_Town" +
                            " SET Town_Active_Flag='" + stat + "', " +
                            " LastUpdt_Date = getdate() " +
                            " WHERE Town_code = '" + subdivision_code + "' ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;

        }
        public DataTable getTownList_EX(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dsDivision = null;


            string strQry = " select a.Town_sname as TalukCode,a.Town_name as TalukName,a.Dist as District From" +
                      " Mas_Town a WHERE a.Town_Active_Flag=0 and a.Div_Code= '" + divcode + "' group by a.Town_name,a.Town_code,a.Town_sname,a.Dist,a.Territory_Name,a.Town_Active_Flag";

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