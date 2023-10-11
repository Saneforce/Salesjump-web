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

public partial class MasterFiles_AssetLocationMaster : System.Web.UI.Page
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
    public static string Getcntry()
    {
        loc ast = new loc();
        DataSet ds = ast.getcnty();
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    [WebMethod(EnableSession = true)]
    public static string Getstate()
    {
        loc ast = new loc();
        DataSet ds = ast.getstat();
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    [WebMethod(EnableSession = true)]
    public static string GetDetails()
    {
        loc ast = new loc();
        DataSet ds = ast.getAssetLocation();
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    [WebMethod(EnableSession = true)]
    public static string Save_Location(string locnam, string addln1,string addln2,string cntrycd,string city,string statcd,string zipcode,string contactnm,string contactnum,string contactemail,string hiddenid)
    {
        string ds = string.Empty;
        loc ast = new loc();
        ds = ast.insertlocation(locnam, addln1, addln2, cntrycd, city, statcd, zipcode, contactnm, contactnum, contactemail, hiddenid);
        return ds;
    }
    [WebMethod(EnableSession = true)]
    public static string getAssetLoc_Id(string lcode)
    {
        loc cp = new loc();
        DataSet ds = cp.getlocation(lcode);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    [WebMethod(EnableSession = true)]
    public static int SetNewStatus(string ID, string stus)
    {
        loc ast = new loc();
        int iReturn = ast.DeActivate(ID, stus);
        return iReturn;
    }
    public class loc
    {
        public int DeActivate(string plcode, string stus)
        {
            int iReturn = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();
                string strQry = "update Mas_Asset_Location set Location_Active_Flag='" + stus + "',LastUpt_Date=getdate() where Location_Id='" + plcode + "' and Division_Code='" + div_code + "'";
                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }
        public DataSet getlocation(string ccode)
        {
            DB_EReporting db = new DB_EReporting();
            DataSet ds = null;
            string strQry = "select Location_Name,Address_Line1,Address_Line2,Contact_Name,Contact_Number,Contact_Email,Location_Country,Location_State,Location_City,Location_pincode from Mas_Asset_Location where Location_Active_Flag=0 and Location_Id='" + ccode + "'";
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
        public DataSet getAssetLocation()
        {
            DB_EReporting db = new DB_EReporting();
            DataSet ds = null;
            string strQry = "select Location_Id,Location_Name,Location_Active_Flag,case when Location_Active_Flag=0 then 'Active' else 'Deactivate' end as Status,Division_Code,Contact_Name,Contact_Number,'0' Assets,Contact_Email,c.Country_name,StateName,Location_City,Location_pincode  from Mas_Asset_Location l inner join mas_Country c on l.Location_Country=c.Country_code inner join mas_state s on s.State_Code=l.Location_State where l.Division_Code='" + div_code + "' and Location_Active_Flag=0 group by Location_Id,Location_Name,Location_Active_Flag,Division_Code,Contact_Name,Contact_Number,Contact_Email,c.Country_name,StateName,Location_City,Location_pincode";
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
        public DataSet getcnty()
        {
            DB_EReporting db = new DB_EReporting();
            DataSet ds = null;
            string strQry = "select Country_name,Country_code,S_name from mas_Country";
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
        public DataSet getstat()
        {
            DB_EReporting db = new DB_EReporting();
            DataSet ds = null;
            string strQry = "select StateName,State_Code,ShortName,Country_code from Mas_State where State_Active_Flag=0";
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
        public string insertlocation(string locnam, string addln1, string addln2, string cntrycd, string city, string statcd, string zipcode, string contactnm, string contactnum, string contactemail,string hiddenid)
        {
            DB_EReporting db = new DB_EReporting();
            DataSet ds = null;
            string msg = string.Empty;
            string strQry = "exec insert_asset_location '" + locnam + "','" + addln1 + "','" + addln2 + "','" + cntrycd + "','" + city + "','" + statcd + "','" + zipcode + "','" + contactnm + "','" + contactnum + "','" + contactemail + "','" + div_code + "','"+ hiddenid + "'";
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