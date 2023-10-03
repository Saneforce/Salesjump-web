using System;
using System.Data;
using Bus_EReport;
using System.Web.Services;
using Newtonsoft.Json;

public partial class MasterFiles_SubDivisionList : System.Web.UI.Page
{
    int subdivcode = 0;
    string divcode = string.Empty;
    string sf_type = string.Empty;
    string div_code = string.Empty;
    string ProdCatCode = string.Empty;
    string ProdName = string.Empty;
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
    public static string Prod_Count(string divcode, string sdcode)
    {
        Product dv = new Product();
        DataSet dsProd = dv.getProdforDiv(divcode, sdcode);
        return JsonConvert.SerializeObject(dsProd.Tables[0]);
    }
    [WebMethod(EnableSession = true)]
    public static string Field_Count(string divcode, string sdcode)
    {
        Product dv = new Product();
        DataSet dsProCat = dv.getFieldforcou(divcode, sdcode);
        return JsonConvert.SerializeObject(dsProCat.Tables[0]);
    }
    [WebMethod(EnableSession = true)]
    public static string GetDetails(string divcode)
    {
        SubDivision dv = new SubDivision();
        DataSet ds = dv.getSubDiv(divcode);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    [WebMethod(EnableSession = true)]
    public static string deactivate(string sdcode, string stat)
    {
        SubDivision dv = new SubDivision();
        int iReturn = dv.DeActivate1(sdcode, stat);
        if (iReturn > 0)
        {
            return "Success";
        }
        else
        {
            return "Failure";
        }
    }
}