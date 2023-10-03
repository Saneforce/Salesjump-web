using Bus_EReport;
using ClosedXML.Excel;
using DBase_EReport;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI.WebControls;

public partial class MIS_Reports_Claim_Approvals : System.Web.UI.Page
{
    public static DataTable dt = new DataTable();
    public static string SF_code = string.Empty;
    public static string sfCode = string.Empty;
    public static string gifttype = string.Empty;
    public static string ddldes = string.Empty;
    public static string ddldescription = string.Empty;
    public static string Div_Code = string.Empty;
    public static string ffilter = "All";
	public static DataTable dtclaimOrder;
    public static DataTable dtclaim = new DataTable();
	
    protected void Page_Load(object sender, EventArgs e)
    {
        Div_Code = Session["div_code"].ToString();
        sfCode = Session["sf_code"].ToString();
        SF_code = Request.QueryString["sf_code"].ToString();
        ddldes = Request.QueryString["ddldes"].ToString();
        ddldescription = Request.QueryString["ddldescription"].ToString();
        gifttype = Request.QueryString["giftype"].ToString();
        if (!IsPostBack)
        {
            hffilter.Value = "All";
        }
    }
    [WebMethod]
    public static string GetClaim(string divcode, string SF, string Mn, string Yr)
    {
        CallPlan sf = new CallPlan();
		dtclaim = new DataTable();
        dtclaim = sf.getGiftApprovalxl(SF_code, ddldes, ddldescription, Div_Code, gifttype, sfCode, ffilter);
		DB_EReporting db = new DB_EReporting();
        DataTable dtgift = new DataTable();
        dtgift = db.Exec_DataTable("exec getClaimGiftDetails '" + SF_code + "','" + ddldescription + "','" + Div_Code + "','" + gifttype + "','" + sfCode + "','" + ffilter + "'");
        DataTable dtslab = new DataTable();
        dtslab = db.Exec_DataTable("exec getClaimRetailSLabDetails '" + SF_code + "','" + ddldescription + "','" + Div_Code + "','" + gifttype + "','" + sfCode + "','" + ffilter + "'");
        DataTable dtimg = new DataTable();
        dtimg = db.Exec_DataTable("exec getClaimImageCapture '" + SF_code + "','" + ddldescription + "','" + Div_Code + "','" + gifttype + "','" + sfCode + "','" + ffilter + "'");
        for (int i = 0; i < dtclaim.Rows.Count; i++)
        {
            //string giftname = string.Empty;
            //string giftdesc = string.Empty;
            //string slabname = string.Empty;
            //string images = string.Empty;
            var slno = dtclaim.Rows[i]["ClaimID"];
            List<DataRow> drpt = dtgift.Select("Sl_No='" + slno + "'").ToList();
            if (drpt.Count > 0)
            {
                dtclaim.Rows[i]["Gift"] = string.Join(", ", drpt.Select(row => row[1].ToString()).ToArray());
                dtclaim.Rows[i]["GiftDescription"] = string.Join(", ", drpt.Select(row => row[2].ToString()).ToArray());
            }
            List<DataRow> drslb = dtslab.Select("Sl_No='" + slno + "'").ToList();
            if (drslb.Count > 0)
            {
                dtclaim.Rows[i]["SlabName"] = string.Join(", ", drslb.Select(row => row[1].ToString()).ToArray());
            }
            List<DataRow> drpImg = dtimg.Select("Sl_No='" + slno + "'").ToList();
            if (drpImg.Count > 0)
            {
                dtclaim.Rows[i]["imgurl"] = string.Join(", ", drpImg.Select(row => row[1].ToString()).ToArray());
            }
        }
        return JsonConvert.SerializeObject(dtclaim);
    }
    [WebMethod]
    public static string GetOrders(string divcode, string SF, string Mn, string Yr)
    {
        CallPlan sf = new CallPlan();
		dtclaimOrder = new DataTable();
        dtclaimOrder = sf.getGiftApprovalOrders(SF_code, ddldes, ddldescription, Div_Code, gifttype, sfCode, ffilter);
        return JsonConvert.SerializeObject(dtclaimOrder);
    }
    [WebMethod]
    public static string svTpApprove(string sxml)
    {
        string msg = string.Empty;
        string sfcode = HttpContext.Current.Session["sf_code"].ToString();
        sxml = "<ROOT>" + sxml + "</ROOT>";
        string consString = Globals.ConnString;
        using (SqlConnection con = new SqlConnection(consString))
        {
            using (SqlCommand cmd = con.CreateCommand())
            {
                cmd.CommandType = CommandType.StoredProcedure; ;
                cmd.CommandText = "updateClaimNew";
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@SSF", sfcode),
                    new SqlParameter("@Div", Div_Code),
                    new SqlParameter("@Sxml", sxml)
                };
                cmd.Parameters.AddRange(parameters);
                try
                {
                    if (con.State != ConnectionState.Open)
                    {
                        con.Open();
                    }
                    cmd.ExecuteNonQuery();
                    msg = "Action Completed";
                }
                catch (Exception ex)
                {
                    msg = ex.Message;
                    throw ex;
                }
            }
        }
        return msg;
    }
    protected void ExportToExcel(object sender, EventArgs e)
    {
        DataTable dt = dtclaim.Copy();
        foreach (DataRow dr in dt.Rows)
        {
            DataRow[] drp = dtclaimOrder.Select("ListedDrCode='" + dr["Customer_Code"].ToString() + "'");
            if (drp.Length > 0)
            {
                dr["Order_Value"] = drp[0]["value"].ToString();
            }
        }
        dt.Columns.Remove("Aproval_Flag");
        dt.Columns.Remove("isApproved");
        using (XLWorkbook wb = new XLWorkbook())
        {
            wb.Worksheets.Add(dt, hffilter.Value.ToString());
            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=Claim status " + ddldescription + " - " + hffilter.Value.ToString() + ".xlsx");
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