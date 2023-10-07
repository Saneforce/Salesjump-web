using System;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Windows;
using System.IO;
using System.Data;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html;
using iTextSharp.text.html.simpleparser;
using iTextSharp.tool.xml;
using System.Text;
using Bus_EReport;
using System.Net;
using System.Web.Services;
using Newtonsoft.Json;

public partial class MIS_Reports_rpt_newstatewise : System.Web.UI.Page
{
    public string stat = string.Empty;
    public string statv = string.Empty;
    public string div = string.Empty;
    public string fdt = string.Empty;
    public string sf_code = string.Empty;
    public string mgrsf_code = string.Empty;
    //string tdt = string.Empty;

    DataSet dsSalesForce = new DataSet();
    protected void Page_Load(object sender, EventArgs e)
    {
        stat = Request.QueryString["state"].ToString();
        statv = Request.QueryString["statev"].ToString(); 
        div = Request.QueryString["divcode"].ToString();
        fdt = Request.QueryString["fyear"].ToString();
        sf_code = Request.QueryString["ffval"].ToString();
        if (sf_code == "0")
        {
            sf_code= Request.QueryString["mgrval"].ToString();
        }
        
        hidn_sf_code.Value = sf_code;
        divcd.Value = div;
        statefl.Value = stat;
        hidnYears.Value = fdt;

        lblHead.Text = "SKU Order Details for  " + fdt ;
       

    }

    [WebMethod(EnableSession = true)]
    public static string Filldtl(string sf_code, string div, string fdt, string stat)
    {
        SalesForce sf = new SalesForce();
       DataSet dsProd = sf.Get_state_wiseo(sf_code, div, fdt, stat);
        return JsonConvert.SerializeObject(dsProd.Tables[0]);
    }
   
}