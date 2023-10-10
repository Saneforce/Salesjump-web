using Bus_EReport;
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

public partial class MasterFiles_AssetCategoryMaster : System.Web.UI.Page
{
    public static string div_code = string.Empty;
    public static string sf_code = string.Empty;
    public static string sub_division = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        sf_code = Session["sf_code"].ToString();
        div_code = Session["div_code"].ToString();
        sub_division = Session["sub_division"].ToString();
    }
    [WebMethod(EnableSession = true)]
    public static string GetCatType()
    {
        loc ast = new loc();
        DataSet ds = ast.gettype();
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    [WebMethod(EnableSession = true)]
    public static string GetDetails()
    {
        loc ast = new loc();
        DataSet ds = ast.getAssetCat();
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    [WebMethod(EnableSession = true)]
    public static string Save_category(string catname, string cattype)
    {
        string ds = string.Empty;
        loc ast = new loc();
        ds = ast.insertcategory(catname, cattype);
        return ds;
    }
    [WebMethod(EnableSession = true)]
    public static int SetNewStatus(string ID, string stus)
    {
        loc ast = new loc();
        int iReturn = ast.DeActivate(ID, stus);
        return iReturn;
    }
    [WebMethod(EnableSession = true)]
    public static string getAssetCat_Id(string ccode)
    {
        loc cp = new loc();
        DataSet ds = cp.getcategory(ccode);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    public class loc
    {
        public DataSet getcategory(string ccode)
        {
            DB_EReporting db = new DB_EReporting();
            DataSet ds = null;
            string strQry = "select category_Id,category_Name,category_type from Mas_Asset_Category where division_code='"+ div_code + "' and category_Id='"+ ccode + "'";
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
        public int DeActivate(string plcode, string stus)
        {
            int iReturn = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();
                string strQry = "update Mas_Asset_Category set active_flag='" + stus + "',Lastupdt_date=getdate(),Modified_by='"+ sf_code + "' where category_Id='" + plcode + "' and division_code='" + div_code + "'";
                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }
        public DataSet gettype()
        {
            DB_EReporting db = new DB_EReporting();
            DataSet ds = null;
            string strQry = "select cattype_Id,cattype_Name from Mas_Asset_cattype where division_code='"+ div_code + "' and active_flag='0'";
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
        public DataSet getAssetCat()
        {
            DB_EReporting db = new DB_EReporting();
            DataSet ds = null;
            string strQry = "select category_Id,category_Name,cast(c.created_date as datetime)created_date,Lastupdt_date,cattype_Name category_type,c.active_flag,isnull(craeted_by,'-')craeted_by,isnull(Modified_by,'-')Modified_by,case when c.active_flag=0 then 'Active' else 'Deactivate' end as Status,'0' as Assets from Mas_Asset_Category c inner join Mas_Asset_cattype t on c.category_type=t.cattype_Id where c.division_code='" + div_code + "'";
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
        public string insertcategory(string nam, string typ)
        {
            DB_EReporting db = new DB_EReporting();
            DataSet ds = null;
            string msg = string.Empty;
            string strQry = "exec sp_insert_assetcat '" + nam + "','" + typ + "','"+ sf_code + "','"+ div_code + "'";
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
    }
}