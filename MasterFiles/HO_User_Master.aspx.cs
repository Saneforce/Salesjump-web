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
using System.Drawing;
using System.IO;
using DBase_EReport;

public partial class MasterFiles_HO_User_Master : System.Web.UI.Page
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

        //Session["Div_code"] = div_code.ToString();

    }
    [WebMethod]
    public static string GetHOUsers(string div)
    {
        Division dv = new Division();
        DataSet ds = dv.getHOUsers_List(div.Replace(",", ""));
        return JsonConvert.SerializeObject(ds.Tables[0]);//"['div':" + strQry + "]";//
    }
	
    [WebMethod]
    public static string getDivision(string div)
    {
        Division dv = new Division();
        DataSet ds = dv.getSubDivisionHO(div.Replace(",", "")); 
        return JsonConvert.SerializeObject(ds.Tables[0]);//"['div':" + strQry + "]";//
    }
    [WebMethod]
    public static string saveHOUser(string div,string hoID, string hoName, string hoUID, string hoPWD,string subDivs)
    {
        string msg = string.Empty;
        Division dv = new Division();
        msg = dv.saveHOUser(div.Replace(",", ""),hoID, hoName, hoUID, hoPWD,subDivs);
        return msg;
    }

    [WebMethod]
    public static string editHOUser(string div, string HOID)
    {
        Division Dv = new Division();
        DataSet ds = Dv.editHOUser(div.Replace(",", ""), HOID);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
	
	
	 [WebMethod]
    public static string getdataliprod(string div, string prodcode)
    {
        grp Rut = new grp();
        DataTable ds = Rut.get_proddtl(div.Replace(",", ""), prodcode);
        return JsonConvert.SerializeObject(ds);
    }
    public class grp
    {

        public DataTable get_proddtl(string div, string prodcode)
        {
            DB_EReporting db_ER = new DB_EReporting();
            string sub = "";
            DataTable dsStockist = null;

            var strQry = "exec [get_productgrp_detail] '" + div + "','" + prodcode + "'";
            try
            {
                dsStockist = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsStockist;
        }
    }
}