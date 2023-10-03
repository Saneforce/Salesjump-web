using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using Newtonsoft.Json;
using System.Data;
using Bus_EReport;

public partial class MasterFiles_Rpt_TPMyDayPlan : System.Web.UI.Page
{   
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    [WebMethod(EnableSession =true)]
    public static string GetRptMyDayPlanMnth(string FDT, string TDT)
    {
        string div_code = HttpContext.Current.Session["division_code"].ToString();
        string Sf_Code = HttpContext.Current.Session["Sf_Code"].ToString(); 
        DataSet dsAdmin = new DataSet(); DCR dc = new DCR();
        dsAdmin = dc.GetTPDayPlan_MGR_Sub(div_code, Sf_Code, FDT.Trim(), TDT.Trim());
        return JsonConvert.SerializeObject(dsAdmin.Tables[0]);
    }   
}