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
using DBase_EReport;
using System.Data.SqlClient;
using ClosedXML.Excel;
using System.IO;

public partial class MasterFiles_Company : System.Web.UI.Page
{
    int divcode = 0;
    string div_code = string.Empty;
    string sf_type = string.Empty;


    protected override void OnPreInit(EventArgs e)
    {
        //base.OnPreInit(e);
        //sf_type = Session["sf_type"].ToString();
        //if (sf_type == "3")
        //{
        //    this.MasterPageFile = "~/Master.master";
        //}
        //else if (sf_type == "2")
        //{
        //    this.MasterPageFile = "~/Master_MGR.master";
        //}
        //else if (sf_type == "1")
        //{
        //    this.MasterPageFile = "~/Master_MR.master";
        //}
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
    }
    [WebMethod]
    public static string GetList(string div_code)
    {
        cptst dv = new cptst();
        
        DataSet ds = dv.getDivision_DataTable_New(div_code);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    [WebMethod(EnableSession = true)]
    public static string deactivate(string div_code, string stat)
    {
        cptst dv = new cptst();
        int iReturn = dv.DeActivate(div_code, stat);
        if (iReturn > 0)
        {
            return "Success";
        }
        else
        {
            return "Failure";
        }
    }
    
    protected void ExportToExcel(object sender, EventArgs e)
    {
        Response.ClearContent();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "CompanyList.xls"));
        Response.ContentType = "application/ms-excel";
        cptst dv = new cptst();
        DataTable dssalesforce1 = dv.getDivision_Excel(div_code);
        DataTable dt = dssalesforce1;
        string str = string.Empty;
        foreach (DataColumn dtcol in dt.Columns)
        {
            Response.Write(str + dtcol.ColumnName);
            str = "\t";
        }
        Response.Write("\n");
        foreach (DataRow dr in dt.Rows)
        {
            str = "";
            for (int j = 0; j < dt.Columns.Count; j++)
            {
                Response.Write(str + Convert.ToString(dr[j]));
                str = "\t";
            }
            Response.Write("\n");
        }
        Response.End();

    }

    public class cptst
    {
        public DataSet getDivision_DataTable_New(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;

            string strQry = "SELECT Division_Code,Division_Name,Division_City,Division_SName,Alias_Name,Div_Sl_No ,division_active_flag" +
                     " FROM mas_division " +
                     " WHERE division_active_flag=0 and  Division_Code in (" + div_code + ") " +
                     " ORDER BY Div_Sl_No";
            try
            {
                dsDivision = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsDivision;
        }

        public int DeActivate(string div_code, string stat)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                string strQry = "UPDATE mas_division " +
                            " SET division_active_flag='"+stat+"', " +
                            " LastUpdt_Date = getdate() " +
                            " WHERE division_code = '" + div_code + "' ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;

        }
        
        public DataTable getDivision_Excel(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dsDivision = null;


            string strQry = "SELECT Div_Sl_No as SlNo,Division_Name as CompanyName,Alias_Name as AliasName,Division_City as City" +
                     " FROM mas_division " +
                     " WHERE division_active_flag=0 and  Division_Code in (" + div_code + ") " +
                     " ORDER BY Div_Sl_No";

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
