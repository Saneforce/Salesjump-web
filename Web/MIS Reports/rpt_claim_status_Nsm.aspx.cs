using Bus_EReport;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Services;
public partial class MIS_Reports_rpt_claim_status_Nsm : System.Web.UI.Page
{
    public static DataTable dt = new DataTable();
    public static string ddldes = string.Empty;
    public static string ddldescription = string.Empty;
    public static string SF_code = string.Empty;
    public static string sfCode = string.Empty;
    public static string gifttype = string.Empty;
    public static string Div_Code = string.Empty;
    public static string approve = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        Div_Code = Session["div_code"].ToString();
        sfCode = Session["sf_code"].ToString();
        SF_code = Request.QueryString["sf_code"].ToString();
        ddldes = Request.QueryString["ddldes"].ToString();
        ddldescription = Request.QueryString["ddldescription"].ToString();
        gifttype = Request.QueryString["giftype"].ToString();
        fillapprovel();
    }
    private void fillapprovel()
    {
        CallPlan clp = new CallPlan();
        DataSet ds = new DataSet();
        ds = clp.setapprovel(SF_code, Div_Code);
        approve = ds.Tables[0].Rows[0]["approveflag"].ToString();
        hdnfield.Value = ds.Tables[0].Rows[0]["sf_status"].ToString();
    }
    [WebMethod(EnableSession = true)]
    public static string getGiftApproval()
    {
        CallPlan dsm = new CallPlan();
        // Notice dsm = new Notice();
        DataSet ds = new DataSet();
        string Div_Code = HttpContext.Current.Session["div_code"].ToString();
        ds = dsm.getGiftApprovalNSM(SF_code, ddldes, ddldescription, Div_Code, gifttype, sfCode);
        //ds = dsm.getGiftApprovalNSM(SF_code,  Div_Code);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    [WebMethod(EnableSession = true)]
    public static string getGiftEvent()
    {
        CallPlan dsm = new CallPlan();
        // Notice dsm = new Notice();
        DataSet ds = new DataSet();
        string Div_Code = HttpContext.Current.Session["div_code"].ToString();
        ds = dsm.getGiftEventNSM(SF_code, ddldes, ddldescription, Div_Code, gifttype);
        //ds = dsm.getGiftEventNSM(SF_code, Div_Code);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }

    [WebMethod(EnableSession = true)]
    public static string savedata(string data)
    {

        string div_code = "";
        string msg = string.Empty;
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

            string xml = "<root>";
            for (int i = 0; i < items.Count; i++)
            {
                xml += "<ASSD claimid=\"" + items[i].claimid + "\"  flag=\"" + items[i].flag + "\" adminreject=\"" + items[i].adminreject + "\" sf=\"" + items[i].sf + "\" />";//");//, empList[i].T_detail_no);
            }
            xml += "</root>";
            string consString = Globals.ConnString;
            using (SqlConnection con = new SqlConnection(consString))
            {
                using (SqlCommand cmd = con.CreateCommand())
                {

                    cmd.CommandType = CommandType.StoredProcedure; ;
                    cmd.CommandText = "updateNSMClaim";

                    SqlParameter[] parameters = new SqlParameter[]
                    {
                                        new SqlParameter("@SXML", xml),
                                        new SqlParameter("@sf",sfCode)
                    };
                    cmd.Parameters.AddRange(parameters);
                    try
                    {
                        if (con.State != ConnectionState.Open)
                        {
                            con.Open();
                        }
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception exp)
                    {
                        throw exp;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            return "update fail...";
        }
        return "updated";
    }
    public class claim
    {
        public string claimid { get; set; }
        public string flag { get; set; }
        public string adminreject { get; set; }
        public string sf { get; set; }

    }
    protected void ExportToExcel(object sender, EventArgs e)
    {
        Response.ClearContent();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "Claim status.xls"));
        Response.ContentType = "application/ms-excel";
        CallPlan sf = new CallPlan();
        DataTable dtclaim = sf.getGiftApprovalNSMxl(SF_code, ddldes, ddldescription, Div_Code, gifttype, sfCode);
        DataTable dt = dtclaim;
        string str = string.Empty;
        foreach (DataColumn dtcol in dt.Columns)
        {
            Response.Write(str + dtcol.ColumnName);
            str = "\t";
        }
        Response.Write("\n");
        foreach (DataRow dr in dt.Rows)
        {
            str = "";
            for (int j = 0; j < dt.Columns.Count; j++)
            {
                Response.Write(str + Convert.ToString(dr[j]));
                str = "\t";
            }
            Response.Write("\n");
        }
        Response.End();

    }
    [WebMethod(EnableSession = true)]
    public static string getGiftData()
    {
        string strQry = string.Empty;
        DataSet dsTerritory = new DataSet();
        SqlConnection con = new SqlConnection(Globals.ConnString);
        con.Open();
        SqlCommand cmd = new SqlCommand("exec getNSMGiftData '" + SF_code + "','" + Div_Code.TrimEnd(',') + "','" + ddldescription + "','" + ddldes + "','" + gifttype + "','" + sfCode + "'", con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dsTerritory);
        con.Close();
        return JsonConvert.SerializeObject(dsTerritory.Tables[0]);
    }
    [WebMethod(EnableSession = true)]
    public static string getGiftINV()
    {
        string strQry = string.Empty;
        DataSet dsTerritory = new DataSet();
        SqlConnection con = new SqlConnection(Globals.ConnString);
        con.Open();
        SqlCommand cmd = new SqlCommand("exec getNSMGiftData_INV '" + SF_code + "','" + Div_Code.TrimEnd(',') + "','" + ddldescription + "','" + ddldes + "','" + gifttype + "','" + sfCode + "'", con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dsTerritory);
        con.Close();
        return JsonConvert.SerializeObject(dsTerritory.Tables[0]);
    }
    [WebMethod(EnableSession = true)]
    public static string getGiftDataRT()
    {
        string strQry = string.Empty;
        DataSet dsTerritory = new DataSet();
        SqlConnection con = new SqlConnection(Globals.ConnString);
        con.Open();
        SqlCommand cmd = new SqlCommand("exec getNSMGiftData_RT '" + SF_code + "','" + Div_Code.TrimEnd(',') + "','" + ddldescription + "','" + ddldes + "','" + gifttype + "','" + sfCode + "'", con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dsTerritory);
        con.Close();
        return JsonConvert.SerializeObject(dsTerritory.Tables[0]);
    }
    [WebMethod(EnableSession = true)]
    public static string getGiftDataGF()
    {
        string strQry = string.Empty;
        DataSet dsTerritory = new DataSet();
        SqlConnection con = new SqlConnection(Globals.ConnString);
        con.Open();
        SqlCommand cmd = new SqlCommand("exec getNSMGiftData_GF '" + SF_code + "','" + Div_Code.TrimEnd(',') + "','" + ddldescription + "','" + ddldes + "','" + gifttype + "','" + sfCode + "'", con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dsTerritory);
        con.Close();
        return JsonConvert.SerializeObject(dsTerritory.Tables[0]);
    }
    [WebMethod(EnableSession = true)]
    public static string getGiftDataIMG()
    {
        string strQry = string.Empty;
        DataSet dsTerritory = new DataSet();
        SqlConnection con = new SqlConnection(Globals.ConnString);
        con.Open();
        SqlCommand cmd = new SqlCommand("exec getNSMGiftData_IMG '" + SF_code + "','" + Div_Code.TrimEnd(',') + "','" + ddldescription + "','" + ddldes + "','" + gifttype + "','" + sfCode + "'", con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dsTerritory);
        con.Close();
        return JsonConvert.SerializeObject(dsTerritory.Tables[0]);
    }
}