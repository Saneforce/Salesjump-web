using System;
using System.Data;
using Bus_EReport;
using System.Web.Services;
using Newtonsoft.Json;

public partial class MasterFiles_Tax_Master_grid : System.Web.UI.Page
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
    public static string GetDetails(string divcode)
    {
        AdminSetup ast = new AdminSetup();
        DataSet ds = ast.view_tax_Master(divcode);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    [WebMethod(EnableSession = true)]
    public static string deactivate(string sdcode, string stat)
    {
        AdminSetup dc = new AdminSetup();
        int iReturn = dc.atcdec_tax_Master(sdcode, stat);
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
    public static string prod_map(string taxcode, string divcode)
    {
        AdminSetup ast = new AdminSetup();
        DataSet ds = ast.view_product(taxcode,divcode);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
}  
