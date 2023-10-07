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

public partial class MasterFiles_Designation : System.Web.UI.Page
{
    public string sf_type = string.Empty;
    public string div_code = string.Empty;
    
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
    [WebMethod]
    public static string GetList(string div_code)
    {
        Degtst dv = new Degtst();

        DataSet ds = dv.getDivision_DataTable_New(div_code);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    [WebMethod(EnableSession = true)]
    public static string Field_Count(string divcode, string dsgcode)
    {
        Degtst dv = new Degtst();
        DataSet dsProCat = dv.getFieldforcou(divcode, dsgcode);
        return JsonConvert.SerializeObject(dsProCat.Tables[0]);
    }
    [WebMethod(EnableSession = true)]
    public static string deactivate(string dsgcode, string stat,string div_code)
    {
        Degtst dv = new Degtst();
        int iReturn = dv.DeActivate(dsgcode, stat, div_code);
        if (iReturn > 0)
        {
            return "Success";
        }
        else
        {
            return "Failure";
        }
    }
    [WebMethod(EnableSession = true)]
    public static Details[] display()
    {
        Degtst dv = new Degtst();
        DataTable dt1 = new DataTable();
        dt1 = dv.getMenuBycompany(HttpContext.Current.Session["division_code"].ToString());
        List<Details> details = new List<Details>();
        foreach (DataRow row1 in dt1.Rows)
        {
            Details dt = new Details();
            dt.Menu_ID = Convert.ToInt32(row1["Menu_ID"]);
            dt.Menu_Name = row1["Menu_Name"].ToString();
            dt.Menu_Type = row1["Menu_Type"].ToString();
            dt.Parent_Menu = row1["Parent_Menu"].ToString();
            //dt.MMnu = row1["MMnu"].ToString();
            // dt.lvl = row1["lvl"].ToString();
            details.Add(dt);

        }

        return details.ToArray();
    }
    public class Details
    {
        public int Menu_ID { get; set; }
        public string Menu_Name { get; set; }
        public string Menu_Type { get; set; }
        public string Parent_Menu { get; set; }
        public string lvl { get; set; }
        public string MMnu { get; set; }
    }
    [WebMethod(EnableSession = true)]
    public static int savedata(string des_codes, string arr)
    {
        int getreturn;
        Degtst dv = new Degtst();
        getreturn = dv.DesMenuPermissionValues(des_codes, arr);

        if (getreturn > 0)
        {

        }

        return getreturn;

    }
    
    protected void ExportToExcel(object sender, EventArgs e)
    {

        DataTable dsProd1 = null;
        Degtst dv = new Degtst();
        dsProd1 = dv.getDegnation_Excel(div_code);
        using (XLWorkbook wb = new XLWorkbook())
        {
            wb.Worksheets.Add(dsProd1, "DesignationList");
            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=DesignationList.xlsx");
            using (MemoryStream MyMemoryStream = new MemoryStream())
            {
                wb.SaveAs(MyMemoryStream);
                MyMemoryStream.WriteTo(Response.OutputStream);
                Response.Flush();
                Response.End();
            }
        }
    }
    public class Degtst
    {
        
        public DataSet getDivision_DataTable_New(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDeg = null;

            //string strQry = " SELECT D.Designation_Code,D.Designation_Short_Name,D.Designation_Name,D.Type, " +
            //         " d.Baselevel_SNo,d.Manager_SNo, (select COUNT(s.Designation_Code) from Mas_Salesforce S where D.Designation_Code=S.Designation_Code and SF_Status='0') as sf_count,D.Division_Code,isnull(D.Des_Rights,'') Menuid   " +
            //         " from Mas_SF_Designation D where D.Designation_Active_Flag = 0 and D.Division_Code ='" + div_code + "' " +
            //         " order by d.Manager_SNo,d.Baselevel_SNo ";

            string strQry = " SELECT D.Designation_Code,D.Designation_Short_Name,D.Designation_Name,D.Type,D.Designation_Active_Flag, " +
                       " d.Baselevel_SNo,d.Manager_SNo, (select COUNT(s.Designation_Code) from Mas_Salesforce S where D.Designation_Code=S.Designation_Code and SF_Status='0' ) as sf_count,D.Division_Code,isnull(D.Des_Rights,'') Menuid " +
                       " from Mas_SF_Designation D where Division_Code ='" + div_code + "'" +
                       " order by d.Designation_Name ";
            try
            {
                dsDeg = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsDeg;
        }
        public DataSet getFieldforcou(string divcode, string val)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDeg = null;

            
            string strQry = " SELECT a.Sf_Code,a.Sf_Name,a.SF_Mobile,a.Sf_HQ " +
                   "  FROM  Mas_Salesforce a,Mas_SF_Designation d " +
                   "  WHERE   charindex(','+ cast(d.Designation_Code as varchar)+',',','+ cast(a.Designation_Code as varchar) +',')>0  AND " +
                   " a.SF_Status=0 AND a.Division_Code= '" + divcode + ",' and d.Designation_Code='" + val + "' " +
                   "  ORDER BY 2";
            try
            {
                dsDeg = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsDeg;
        }
        public int DeActivate(string descd, string stat,string Division_Code)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                string strQry = "UPDATE Mas_SF_Designation " +
                            " SET Designation_Active_Flag='" + stat + "', " +
                            " LastUpdt_Date = getdate() " +
                            " WHERE Designation_Code = '" + descd + "' and Division_Code='" + Division_Code + "'";
                //strQry = "Update Mas_SF_Designation " +
                //          "SET Designation_Active_Flag=1 , " +
                //          "LastUpdt_Date = getdate() " +
                //          "WHERE Designation_Code = '" + Designation_Code + "' and Division_Code='" + Division_Code + "' ";
                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;

        }
        public DataTable getMenuBycompany(string div)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet ds = null;

            try
            {

                string strQry = "exec get_companymenus '" + div + "'";
                ds = db_ER.Exec_DataSet(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return ds.Tables[0];
        }
        public int DesMenuPermissionValues(string des_codes, string arr)
        {
            int i = 0;
            DB_EReporting db_ER = new DB_EReporting();
            try
            {

                // strQry = "update Mas_SF_Designation set Des_Rights='" + arr + "' where Designation_Code='" + des_codes + "'";
                string strQry = "exec Des_MenuRights '" + des_codes + "','" + arr + "'";
                i = db_ER.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return i;
        }
        public DataTable getDegnation_Excel(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dsDivision = null;


            string strQry = " SELECT D.Designation_Short_Name as ShortName,D.Designation_Name as Designation," +
                       "  (select COUNT(s.Designation_Code) from Mas_Salesforce S where D.Designation_Code=S.Designation_Code and SF_Status='0' ) as FieldforcewiseCount " +
                       " from Mas_SF_Designation D where Designation_Active_Flag=0 and Division_Code ='" + div_code + "'" +
                       " order by d.Designation_Name ";

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