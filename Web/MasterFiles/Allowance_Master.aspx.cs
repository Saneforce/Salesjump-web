using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;




using System.Data;
using Bus_EReport;

using System.Web.Services;
using System.Data.SqlClient;
using Newtonsoft.Json;


public partial class MasterFiles_Allowance_Master : System.Web.UI.Page
{
	string sf_type = string.Empty;
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

    }

    [WebMethod(EnableSession = true)]
    public static string SaveData(string alName, string aShName, string alType, string uentr, string alw_code)
    {
        string div_code = "";
        try
        {
            div_code = HttpContext.Current.Session["div_code"].ToString();
        }
        catch
        {
            div_code = HttpContext.Current.Session["Division_Code"].ToString();
        }

        Notice nt = new Notice();

        string iReturn = nt.Add_Allowance_Type(div_code, alName, aShName, alType, uentr, alw_code);

        return "Sucess";
    }



    [WebMethod(EnableSession = true)]
    public static string DeactivateAllow(string alw_code)
    {
        string div_code = "";
        try
        {
            div_code = HttpContext.Current.Session["div_code"].ToString();
        }
        catch
        {
            div_code = HttpContext.Current.Session["Division_Code"].ToString();
        }

        Notice nt = new Notice();

        string iReturn = nt.deactive_Allowance_Type(div_code, alw_code);

        return "Sucess";
    }


    public class AllwonceType_Details
    {
        public string ALW_code { get; set; }
        public string ALW_name { get; set; }
        public string ALW_type { get; set; }
        public string ALW_shname { get; set; }
        public string user_enter { get; set; }

    }

    [WebMethod(EnableSession = true)]
    public static AllwonceType_Details[] GetData()
    {
        string div_code = "";
        try
        {
            div_code = HttpContext.Current.Session["div_code"].ToString();
        }
        catch
        {
            div_code = HttpContext.Current.Session["Division_Code"].ToString();
        }

        Notice nt = new Notice();
        List<AllwonceType_Details> FFD = new List<AllwonceType_Details>();
        DataSet dsAlowtype = null;
        dsAlowtype = nt.get_Allowance_Type(div_code);

        foreach (DataRow row in dsAlowtype.Tables[0].Rows)
        {
            AllwonceType_Details ffd = new AllwonceType_Details();
            ffd.ALW_code = row["ID"].ToString();
            ffd.ALW_name = row["Allowance_Name"].ToString();
            ffd.ALW_type = row["type"].ToString();
            ffd.ALW_shname = row["Short_Name"].ToString();
            ffd.user_enter = row["user_enter"].ToString();
            FFD.Add(ffd);
        }
        return FFD.ToArray();
    }

}