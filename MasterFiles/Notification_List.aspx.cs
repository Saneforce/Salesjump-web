using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using Newtonsoft.Json;
using Bus_EReport;
using System.Data;

public partial class MasterFiles_Notification_List : System.Web.UI.Page
{
    public static DataTable dt = new DataTable();
    string sf_type = string.Empty;
    string Div_Code = string.Empty;
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
    public static string getnotice_board()
    {
            Notice dsm = new Notice();
        DataSet ds = new DataSet();
        string Div_Code = HttpContext.Current.Session["div_code"].ToString();
        ds = dsm.notice_board(Div_Code);     
          return JsonConvert.SerializeObject(ds.Tables[0]); 
    }

    [WebMethod(EnableSession = true)]
    public static int notifyDelete(string sl_no, string Divcode)
    {
        Notice dsm = new Notice();
        int iReturn = dsm.deletenotice(sl_no, Divcode);
        return iReturn;
    }
}