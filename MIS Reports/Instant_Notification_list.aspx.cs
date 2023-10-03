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
using DBase_EReport;

public partial class MIS_Reports_Instant_Notification_list : System.Web.UI.Page
{
    public static DataTable dt = new DataTable();
    string sf_type = string.Empty;
    string Div_Code = string.Empty;
    private string strQry;

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
        string Div_Code = HttpContext.Current.Session["div_code"].ToString();
        DB_EReporting db = new DB_EReporting();
        DataSet ds = db.Exec_DataSet(" select Trans_Sl_No,SF_Code,SF_Name,Notification_Message,convert(varchar,Notification_Sent_Date,103) start_Date from Mas_ins_Notification  where Division_Code = '"+ Div_Code + "'");
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }

    [WebMethod(EnableSession = true)]
    public static int notifyDelete(string sl_no, string Divcode)
    {
        
        DB_EReporting db = new DB_EReporting();
        int iReturn = deletenotice(sl_no, Divcode);
        return iReturn;
    }
    public static int deletenotice(string slno, string divCode)
    {
        int iReturn = -1;

        try
        {
            DB_EReporting db = new DB_EReporting();

            string strQry = "delete from  Mas_ins_Notification where Division_Code='" + divCode + "' and Trans_Sl_No='" + slno + "' ";

            iReturn = db.ExecQry(strQry);
        }
        catch (Exception ex)
        {
            throw ex;
        }

        return iReturn;

    }
}