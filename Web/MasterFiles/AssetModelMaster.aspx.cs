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

public partial class MasterFiles_AssetModelMaster : System.Web.UI.Page
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
        DataSet ds = ast.getAssetModel();
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    [WebMethod(EnableSession = true)]
    public static string Save_Model(string modname, string modno,string modcat)
    {
        string ds = string.Empty;
        loc ast = new loc();
        ds = ast.insertModel(modname, modno, modcat);
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
    public static string getAssetMod_Id(string mcode)
    {
        loc cp = new loc();
        DataSet ds = cp.getModel(mcode);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    public class loc
    {
        public DataSet getModel(string ccode)
        {
            DB_EReporting db = new DB_EReporting();
            DataSet ds = null;
            string strQry = "select Model_Id,Model_Name,Model_No,category_Id from Mas_Asset_Model where Model_Id='" + ccode + "' and division_code='" + div_code + "'";
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
                string strQry = "declare @stas tinyint='"+ stus+"' update Mas_Asset_Model set Model_Active_Flag='" + stus + "',Lastupdt_date=getdate(),Deactive_Date=case when @stas= 1 then  getdate() when @stas=0 then null end where Model_Id='" + plcode + "' and division_code='" + div_code + "'";
                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }
        public string insertModel(string nam, string modno, string modcat)
        {
            DB_EReporting db = new DB_EReporting();
            DataSet ds = null;
            string msg = string.Empty;
            string strQry = "exec insert_asset_Model '" + nam + "','" + modno + "','" + modcat + "','" + div_code + "'";
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
        public DataSet getAssetModel()
        {
            DB_EReporting db = new DB_EReporting();
            DataSet ds = null;
            string strQry = "select Model_Id,Model_Name,Model_No,Model_Active_Flag,category_Name category_Id,case when Model_Active_Flag=0 then 'Active' else 'Deactivate' end as Status,'0' as Assets from Mas_Asset_Model m inner join Mas_Asset_Category c on m.category_Id=c.category_Id where m.division_code='" + div_code + "'";
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
        public DataSet gettype()
        {
            DB_EReporting db = new DB_EReporting();
            DataSet ds = null;
            string strQry = "select category_Id cattype_Id,category_Name cattype_Name from Mas_Asset_Category where division_code='" + div_code + "' and active_flag='0'";
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
    }
}