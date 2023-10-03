using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using System.Web.Services;
using Bus_EReport;
using System.Data;
using System.Data.SqlClient;
using DBase_EReport;

public partial class MasterFiles_SalesForce_List_New : System.Web.UI.Page
{
    #region "Declaration"
    public static string divCode = string.Empty;
    public static string sub_division = string.Empty;
    public static string sf_code = string.Empty;
    string sf_type = string.Empty;
    public static string baseUrl = "";
    #endregion

    #region OnPreInit
    protected override void OnPreInit(EventArgs e)
    {
        baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority + Request.ApplicationPath.TrimEnd('/') + "/";
        if ((Convert.ToString(Session["div_code"]) != null || Convert.ToString(Session["div_code"]) != ""))
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
        else { Page.Response.Redirect(baseUrl, true); }
    }
    #endregion

    #region Page_Load
    protected void Page_Load(object sender, EventArgs e)
    {
        baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority + Request.ApplicationPath.TrimEnd('/') + "/";
        if ((Convert.ToString(Session["div_code"]) != null || Convert.ToString(Session["div_code"]) != ""))
        {
            sf_type = Session["sf_type"].ToString();
            sub_division = Session["sub_division"].ToString();
            sf_code = Session["sf_code"].ToString();

            if (sf_type == "4")
            {

                divCode = Session["Division_Code"].ToString();

                divCode = divCode.TrimEnd(',');
            }
            else
            {

                divCode = Session["div_code"].ToString();
            }
        }
        else { Page.Response.Redirect(baseUrl, true); }

    }
    #endregion

    #region GetList
    [WebMethod]
    public static string GetList(string divcode, string sfcode)
    {
        SalesForce sf = new SalesForce();

        //string sfcode = (sf == "") ? sf_code : sf;

        if ((divcode == null || divcode == ""))
        { divcode = "0"; }

        if ((sub_division == null || sub_division == ""))
        { sub_division = "0"; }

        if ((sfcode == null || sfcode == ""))
        { sfcode = "0"; }

        DataTable dtnew = new DataTable();

        DataSet ds = sf.getusrList_All(divcode, sub_division, sfcode);

        if (ds.Tables.Count > 0)
        { dtnew = ds.Tables[0]; }

        return JsonConvert.SerializeObject(dtnew);
    }
    #endregion

    #region sfDeact
    [WebMethod]
    public static int sfDeact(string SF, int stus)
    {

        SalesForce sf = new SalesForce();
        int iReturn = sf.DeActivate(SF, stus);
        return iReturn;
    }
    #endregion

    #region sfMGR
    public class sfMGR
    {
        public string sfname { get; set; }
        public string sfcode { get; set; }
    }
    #endregion

    #region getApprSFDets
    [WebMethod]
    public static string getApprSFDets(string SF, string div, string apprTyp)
    {
        DataSet ds = new DataSet(); DataTable dtnew = new DataTable();

        if ((div == null || div == ""))
        { div = "0"; }

        if ((apprTyp == null || apprTyp == ""))
        { apprTyp = "0"; }

        if ((SF == null || SF == ""))
        { SF = "0"; }

        SqlConnection con = new SqlConnection(Globals.ConnString);
        con.Open();
        SqlCommand cmd = new SqlCommand("exec getApprSFDetails '" + SF + "'," + div + "," + apprTyp + "", con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(ds);
        con.Close();

        if (ds.Tables.Count > 0)
        { dtnew = ds.Tables[0]; }

        return JsonConvert.SerializeObject(dtnew);
    }
    #endregion

    #region getMGR
    [WebMethod(EnableSession = true)]
    public static sfMGR[] getMGR(string divcode)
    {
        SalesForce dsf = new SalesForce();

        if ((divcode == null || divcode == ""))
        { divcode = "0"; }

        DataSet dsSalesForce = dsf.UserList_Hierarchy(divcode, "Admin");
        List<sfMGR> sf = new List<sfMGR>();
        if (dsSalesForce.Tables.Count > 0)
        {
            foreach (DataRow rows in dsSalesForce.Tables[0].Rows)
            {
                sfMGR rt = new sfMGR();
                rt.sfcode = rows["SF_Code"].ToString();
                rt.sfname = rows["Sf_Name"].ToString();
                sf.Add(rt);
            }
        }
        return sf.ToArray();
    }
    #endregion

    #region ExportToExcel
    protected void ExportToExcel(object sender, EventArgs e)
    {
        Response.ClearContent();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "FieldForceList.xls"));
        Response.ContentType = "application/ms-excel";
        SalesForce sf = new SalesForce();
        DataTable dssalesforce1 = sf.getSalesForcelistEX(divCode);
        DataTable dt = dssalesforce1;
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
    #endregion

    #region display
    [WebMethod(EnableSession = true)]
    public static Details[] display()
    {
        MenuCreation mc1 = new MenuCreation();
        DataTable dt1 = new DataTable();
        dt1 = mc1.getMenuBycompany(divCode);
        List<Details> details = new List<Details>();
        foreach (DataRow row1 in dt1.Rows)
        {
            Details dt = new Details();
            dt.Menu_ID = Convert.ToInt32(row1["Menu_ID"]);
            dt.Menu_Name = row1["Menu_Name"].ToString();
            dt.Menu_Type = row1["Menu_Type"].ToString();
            dt.Parent_Menu = row1["Parent_Menu"].ToString();
            //dt.MMnu = row1["MMnu"].ToString();
            //dt.lvl = row1["lvl"].ToString();
            details.Add(dt);

        }

        return details.ToArray();
    }
    #endregion

    #region Details
    public class Details
    {
        public int Menu_ID { get; set; }
        public string Menu_Name { get; set; }
        public string Menu_Type { get; set; }
        public string Parent_Menu { get; set; }
        public string lvl { get; set; }
        public string MMnu { get; set; }
    }
    #endregion

    #region savedata
    [WebMethod(EnableSession = true)]
    public static int savedata(string sf_codes, string arr)
    {
        int getreturn;
        MenuCreation mc = new MenuCreation();
        getreturn = mc.newAddMenuPermissionValues(sf_codes, arr);

        if (getreturn > 0)
        {

        }

        return getreturn;

    }
    #endregion

    #region svApprHierarchy
    [WebMethod]
    public static string svApprHierarchy(string sf, string Div, string Appr_Type, string Appr_Name, string cusxml)
    {
        string msg = string.Empty;
        SqlConnection conn = new SqlConnection(Globals.ConnString);
        try
        {
            conn.Open();
            SqlTransaction objTrans = conn.BeginTransaction();
            try
            {
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure; ;
                    cmd.CommandText = "svApprovalHierarchy";
                    SqlParameter[] parameters = new SqlParameter[]
                    {
                                new SqlParameter("@Sf_Code",sf),
                                new SqlParameter("@Appr_Type", Appr_Type),
                                new SqlParameter("@Appr_Name",Appr_Name),
                                new SqlParameter("@Div",Div),
                                new SqlParameter("@cusxml",cusxml)
                    };
                    cmd.Parameters.AddRange(parameters);
                    try
                    {
                        if (conn.State != ConnectionState.Open)
                        {
                            conn.Open();
                        }
                        cmd.Transaction = objTrans;
                        cmd.ExecuteNonQuery();
                        msg = "Success";
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
                objTrans.Commit();
            }
            catch (Exception ex)
            {
                objTrans.Rollback();
                throw ex;
            }
        }
        catch (Exception ex)
        {
            msg = ex.Message.ToString();
        }
        return msg;
    }
    #endregion

    #region getSFWorkedWith
    [WebMethod]
    public static string getSFWorkedWith(string sfcode)
    {
        TourPlan sf = new TourPlan();

        if ((sfcode == null || sfcode == ""))
        { sfcode = "0"; }

        DataSet dstp = sf.get_DayPlanWorkedWith(sfcode);

        DataTable dt = new DataTable();

        if (dstp.Tables.Count > 0)
        { dt = dstp.Tables[0]; }

        return JsonConvert.SerializeObject(dt);
    }
    #endregion
}