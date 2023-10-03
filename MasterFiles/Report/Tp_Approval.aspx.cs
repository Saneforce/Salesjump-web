using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
using System.Data.SqlClient;
using System.Web.Services;
using System.Configuration;
using Newtonsoft.Json;
using System.Globalization;
using ClosedXML.Excel;
using System.Xml;
using System.IO;
using System.Data.OleDb;
public partial class MasterFiles_Report_Tp_Approval : System.Web.UI.Page
{
    public static string divCode = string.Empty;
    string sf_type = string.Empty;
    public static DataSet dstp = new DataSet();
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
        sf_type = Session["sf_type"].ToString();
        if (sf_type == "4")
        {
            divCode = Session["Division_Code"].ToString();
            divCode = divCode.TrimEnd(',');
        }
        else
        {
            divCode = Session["div_code"].ToString();
        }
		if (!Page.IsPostBack)
        {
        }
    }
    [WebMethod]
    public static string GetList(string divcode, string SF, string Mn, string Yr)
    {
        SalesForce sf = new SalesForce();
        DataSet ds = sf.getTpList_All(divcode, SF, Mn, Yr);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    [WebMethod]
    public static string svTpApprove(string sxml, string Mn, string Yr)
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
                cmd.CommandText = "svTourPlanWeb";
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@SSF", sfcode),
                    new SqlParameter("@Div", divCode),
                    new SqlParameter("@Mn", Mn.ToString()),
                    new SqlParameter("@Yr", Yr.ToString()),
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
                    msg = "Tour Plan Approved";
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
    [WebMethod]
    public static string getSFTP(string divcode, string SF, string Mn, string Yr)
    {
        TourPlan sf = new TourPlan();
        dstp = sf.get_TPPlanApprove(SF, divcode, Mn, Yr);
        return JsonConvert.SerializeObject(dstp.Tables[0]);
    }
    public class sfMGR
    {
        public string sfname { get; set; }
        public string sfcode { get; set; }
    }

    [WebMethod(EnableSession = true)]
    public static sfMGR[] getMGR(string divcode)
    {
        SalesForce dsf = new SalesForce();
        DataSet dsSalesForce = dsf.UserList_Hierarchy(divcode, "Admin");
        List<sfMGR> sf = new List<sfMGR>();
        foreach (DataRow rows in dsSalesForce.Tables[0].Rows)
        {
            sfMGR rt = new sfMGR();
            rt.sfcode = rows["SF_Code"].ToString();
            rt.sfname = rows["Sf_Name"].ToString();
            sf.Add(rt);
        }
        return sf.ToArray();
    }

    public class bindyear
    {
        public string value { get; set; }
        public string text { get; set; }
    }

    [WebMethod(EnableSession = true)]
    public static bindyear[] BindDate(string divcode)
    {
        TourPlan tp = new TourPlan();
        DataSet dsTP = new DataSet();
        dsTP = tp.Get_TP_Edit_Year(divcode);
        List<bindyear> sf = new List<bindyear>();
        if (dsTP.Tables[0].Rows.Count > 0)
        {
            for (int k = Convert.ToInt16(dsTP.Tables[0].Rows[0]["Year"]); k <= DateTime.Now.Year + 1; k++)
            {
                bindyear rt = new bindyear();
                rt.value = k.ToString();
                rt.text = k.ToString();
                sf.Add(rt);
            }
        }
        return sf.ToArray();
    }
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        DataTable dtsummary = new DataTable();
        dtsummary = dstp.Tables[0];
        using (XLWorkbook wb = new XLWorkbook())
        {
            wb.Worksheets.Add(dtsummary, "Tour_Plan");
            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=Tour Plan.xlsx");
            using (MemoryStream MyMemoryStream = new MemoryStream())
            {
                wb.SaveAs(MyMemoryStream);
                MyMemoryStream.WriteTo(Response.OutputStream);
                Response.Flush();
                Response.End();
            }
        }

    }
    [WebMethod]
    public static string getSFTPWtypes(string divcode, string sfcode)
    {
        TourPlan sf = new TourPlan();
        DataSet dstpp = new DataSet();
        dstpp = sf.get_TPPlanWorktypes(sfcode, divcode);
        return JsonConvert.SerializeObject(dstpp.Tables[0]);
    }
    [WebMethod]
    public static string getSFTPDist(string divcode, string sfcode)
    {
        TourPlan sf = new TourPlan();
        DataSet dstpp = new DataSet();
        dstpp = sf.get_TPPlanDist(sfcode, divcode);
        return JsonConvert.SerializeObject(dstpp.Tables[0]);
    }
    [WebMethod]
    public static string getSFTPRoute(string divcode, string sfcode)
    {
        TourPlan sf = new TourPlan();
        DataSet dstpp = new DataSet();
        dstpp = sf.get_TPPlanRoute(sfcode, divcode);
        return JsonConvert.SerializeObject(dstpp.Tables[0]);
    }
    [WebMethod]
    public static string getSFTPRetailer(string divcode, string sfcode, string routecode)
    {
        TourPlan sf = new TourPlan();
        DataSet dstpp = new DataSet();
        dstpp = sf.get_TPPlanRetailers(sfcode, divcode, routecode);
        return JsonConvert.SerializeObject(dstpp.Tables[0]);
    }
    [WebMethod]
    public static string updateSFTP(string sf_Code, string Divcode, string TpDate, string TMn, string TYr, string Distc, string Distname, string Retc, string Retn, string Routec, string wtypc, string wtypn, string Remarks, string POB, string SOB, string Styp)
    {
        string msg = string.Empty;
        TourPlan sf = new TourPlan();
        msg = sf.updateTPPlan(sf_Code, Divcode, TpDate, TMn, TYr, Distc, Distname, Retc, Retn, Routec, wtypc, wtypn, Remarks, POB, SOB, Styp);
        return msg;
    }
	 [WebMethod]
    public static string getSFWorkedWith(string sfcode)
    {
        TourPlan sf = new TourPlan();
        DataSet dstp = sf.get_DayPlanWorkedWith(sfcode);
        return JsonConvert.SerializeObject(dstp.Tables[0]);
    }     
}