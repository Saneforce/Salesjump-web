//using Bus_EReport;
//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Data.SqlClient;
//using System.Linq;
//using System.Web;
//using System.Web.Services;
//using System.Web.UI;
//using System.Web.UI.WebControls;
//using Newtonsoft.Json;
//using DBase_EReport;

using Bus_EReport;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class MIS_Reports_new_view_dcrcals : System.Web.UI.Page
{
    
    public static string sfn = string.Empty;
    public static string subd = string.Empty;
    public static string div = string.Empty;
    public static string sf_code = string.Empty;
    public static string fdt = string.Empty;
    public static string tdt = string.Empty;
    public static string state = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        sfn = Request.QueryString["Sf_Name"].ToString();
        subd = Request.QueryString["subdiv"].ToString();
        div = Request.QueryString["div_code"].ToString();
        sf_code = Request.QueryString["sf_code"].ToString();
        fdt = Request.QueryString["FromDate"].ToString();
        tdt = Request.QueryString["ToDate"].ToString();
        state= Request.QueryString["state"].ToString();

        DateTime d1 = Convert.ToDateTime(fdt);
        DateTime d2 = Convert.ToDateTime(tdt);
        lblHead.Text = "Channel DCR Entry Report From" + "  " + d1.ToString("dd-MM-yyyy") + " to " + d2.ToString("dd-MM-yyyy");
        lblsf_name.Text = Convert.ToString(sfn);

    }

    [WebMethod(EnableSession = true)]
    public static string getSFdets()
    {
        Chnl sf = new Chnl();
        DataSet dsProd = sf.get_dcr_channelcall(div, sf_code, fdt, tdt, subd, state);
        return JsonConvert.SerializeObject(dsProd.Tables[0]);
    }

    [WebMethod(EnableSession = true)]
    public static string getchnlval()
    {
        Chnl sf = new Chnl();
        DataSet dsProd = sf.get_dcr_channelval(div, sf_code, fdt, tdt, subd, state);
        return JsonConvert.SerializeObject(dsProd.Tables[0]);

    }
    [WebMethod(EnableSession = true)]
    public static string getcallval()
    {
        Chnl sf = new Chnl();
        DataSet dsProd = sf.get_dcr_calval(div, sf_code, fdt, tdt, subd, state);
        return JsonConvert.SerializeObject(dsProd.Tables[0]);

    }
    [WebMethod(EnableSession = true)]
    public static string getchnl()
    {
        Chnl sf = new Chnl();
        DataSet dsProd = sf.Get_channel(div);
        return JsonConvert.SerializeObject(dsProd.Tables[0]);
    }
    [WebMethod(EnableSession = true)]
    public static string getcall()
    {
        Chnl sf = new Chnl();
        DataSet dsProd = sf.Get_call(div);
        return JsonConvert.SerializeObject(dsProd.Tables[0]);
    }
    
}