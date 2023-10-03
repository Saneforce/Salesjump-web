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
using ClosedXML.Excel;
using System.Xml;
using System.IO;
using System.Data.OleDb;
public partial class MIS_Reports_rpt_Claim_Status : System.Web.UI.Page
{
    public static DataTable dt = new DataTable();
    public static string SF_code = string.Empty;
    public static string sfCode = string.Empty;
    public static string gifttype = string.Empty;
 public static string ddldes = string.Empty;
 public static string ddldescription = string.Empty;
 public static string Div_Code = string.Empty;
 public string ffilter = "All";
    protected void Page_Load(object sender, EventArgs e)
    {
        Div_Code = Session["div_code"].ToString();
        sfCode = Session["sf_code"].ToString();
    SF_code= Request.QueryString["sf_code"].ToString();    
      ddldes = Request.QueryString["ddldes"].ToString();
        ddldescription = Request.QueryString["ddldescription"].ToString();
        gifttype = Request.QueryString["giftype"].ToString();
        if (!IsPostBack)
        {
            hffilter.Value = "All";
        }
       
    }
    [WebMethod(EnableSession = true)]
    public static string getGiftApproval()
    {
        CallPlan dsm = new CallPlan();
       // Notice dsm = new Notice();
        DataSet ds = new DataSet();
        string Div_Code = HttpContext.Current.Session["div_code"].ToString();
        ds = dsm.getGiftAdApproval(SF_code, ddldes, ddldescription, Div_Code, gifttype, sfCode);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    [WebMethod(EnableSession = true)]
    public static string getGiftEvent()
    {
        CallPlan dsm = new CallPlan();
        // Notice dsm = new Notice();
        DataSet ds = new DataSet();
        string Div_Code = HttpContext.Current.Session["div_code"].ToString();
        ds = dsm.getGiftEventIMG(SF_code, ddldes, ddldescription, Div_Code, gifttype);
        //ds = dsm.getGiftEventNSM(SF_code, Div_Code);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    [WebMethod(EnableSession = true)]
    public static string getGiftsummery()
    {
        CallPlan dsm = new CallPlan();
        // Notice dsm = new Notice();
        DataSet ds = new DataSet();
        string Div_Code = HttpContext.Current.Session["div_code"].ToString();
        ds = dsm.getclaimstatussummryWeb(SF_code, ddldes, ddldescription, Div_Code, gifttype);
        //ds = dsm.getGiftEventNSM(SF_code, Div_Code);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    [WebMethod(EnableSession = true)]
    public static string getGiftsummerysf()
    {
        CallPlan dsm = new CallPlan();
        // Notice dsm = new Notice();
        DataSet ds = new DataSet();
        string Div_Code = HttpContext.Current.Session["div_code"].ToString();
        ds = dsm.getclaimstatussummryWebsf(SF_code, ddldes, ddldescription, Div_Code, gifttype);
        //ds = dsm.getGiftEventNSM(SF_code, Div_Code);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    [WebMethod(EnableSession = true)]
    public static string savedata(string data)
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
        CallPlan st = new CallPlan();
        try
        {
            var items = JsonConvert.DeserializeObject<List<claim>>(data);
            int rr = -1;

            for (int i = 0; i < items.Count; i++)
            {
                string xml = "<root>";
                xml += "<ASSD claimid=\"" + items[i].claimid + "\"  flag=\"" + items[i].flag + "\" adminreject=\"" + items[i].adminreject + "\" />";//");//, empList[i].T_detail_no);

                xml += "</root>";

                rr = st.Update_claim(xml, sfCode, Div_Code);
            }
            //   if (rr > 0)
        }

        catch (Exception ex)
        {
            return "update fail...";
        }
        return "updated";
        // return items[0].week_name.ToString();

    }
    public class claim
    {
        public string claimid{ get; set; }
        public string flag{ get; set; }
        public string adminreject { get; set; }
    }
    protected void ExportToExcel(object sender, EventArgs e)
    {

        CallPlan sf = new CallPlan();
        DataTable dtclaim = sf.getGiftApprovalxl(SF_code, ddldes, ddldescription, Div_Code, gifttype, sfCode, hffilter.Value.ToString());
        DataTable dt = dtclaim;
        dt.Columns.Remove("Aproval_Flag");
        dt.Columns.Remove("isApproved");
        using (XLWorkbook wb = new XLWorkbook())
        {
            wb.Worksheets.Add(dt, hffilter.Value.ToString());
            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=Claim status "+ ddldescription + " - " + hffilter.Value.ToString() +".xlsx");
            using (MemoryStream MyMemoryStream = new MemoryStream())
            {
                wb.SaveAs(MyMemoryStream);
                MyMemoryStream.WriteTo(Response.OutputStream);
                Response.Flush();
                Response.End();
            }
        }

    }
}