using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.IO;
using System.Data;
using Bus_EReport;
using Newtonsoft.Json;
using System.Web.Services;
using System.Data.SqlClient;
using System.Globalization;
using DBase_EReport;

public partial class MasterFiles_Missed_Date_Approve : System.Web.UI.Page
{
    #region "Declaration"
    DateTime ServerStartTime;
    DataSet dsSalesForce = null;
	  DataSet dsDivision = null;
    string sf_code = string.Empty;
    DataSet dsTP = null;
    DateTime ServerEndTime;
    public static string div_code = string.Empty;
    string sf_type = string.Empty;
    int time;
	public static string sub_division = string.Empty;
    #endregion
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
		sub_division = Session["sub_division"].ToString();
        if (sf_type == "3")
        {
            div_code = Session["div_code"].ToString();
        }
        else
        {
            div_code = Session["div_code"].ToString();
        }
        sf_code = Session["sf_code"].ToString();
        if (!Page.IsPostBack)
        {
            FillYear();
            fillsubdivision();
            
        }
    }
    private void fillsubdivision()
    {
        SalesForce sd = new SalesForce();
        dsSalesForce = sd.Getsubdivisionwise(div_code,sub_division);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            subdiv.DataTextField = "subdivision_name";
            subdiv.DataValueField = "subdivision_code";
            subdiv.DataSource = dsSalesForce;
            subdiv.DataBind();
            subdiv.Items.Insert(0, new ListItem("--Select--", "0"));
        }
    }
    protected void subdiv_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (subdiv.SelectedValue.ToString() != "0")
        {
            FillState(div_code);    
            FillMRManagers();
        }
        else
        {
            FillMRManagers();
        }
    }
     private void FillState(string div_code)
    {
        SalesForce dv = new SalesForce();
        ddlstate.Items.Clear();
        dsDivision = dv.getsubdiv_States(div_code, sf_code, subdiv.SelectedValue);
        if (dsDivision.Tables[0].Rows.Count > 0)
        {
            ddlstate.DataTextField = "StateName";
            ddlstate.DataValueField = "State_code";
            ddlstate.DataSource = dsDivision;
            ddlstate.DataBind();
            ddlstate.Items.Insert(0, new ListItem("--Select--", "0"));

        }

    }
    protected void ddlstate_SelectIndexchanged(object sender, EventArgs e)
    {
        FillMRManagers();
    }
    protected override void OnLoadComplete(EventArgs e)
    {
        ServerEndTime = DateTime.Now;
        TrackPageTime();//It will give you page load time  
    }
    public void TrackPageTime()
    {
        TimeSpan serverTimeDiff = ServerEndTime.Subtract(ServerStartTime);
        time = serverTimeDiff.Minutes;
    }
    private void FillYear()
    {
        TourPlan tp = new TourPlan();
        dsTP = tp.Get_TP_Edit_Year(div_code);
        if (dsTP.Tables[0].Rows.Count > 0)
        {
            for (int k = Convert.ToInt16(dsTP.Tables[0].Rows[0]["Year"]); k <= DateTime.Now.Year + 1; k++)
            {
                ddlFYear.Items.Add(k.ToString());
                ddlFYear.SelectedValue = DateTime.Now.Year.ToString();
            }
        }
        ddlFMonth.SelectedValue = DateTime.Now.Month.ToString();
    }
    private void FillMRManagers()
    {
        SalesForce sf = new SalesForce();
		  ddlFieldForce.Items.Clear();
        dsSalesForce = sf.SalesForceList(div_code, sf_code, subdiv.SelectedValue,"1",ddlstate.SelectedValue);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            ddlFieldForce.DataTextField = "sf_name";
            ddlFieldForce.DataValueField = "sf_code";
            ddlFieldForce.DataSource = dsSalesForce;
            ddlFieldForce.DataBind();
            ddlFieldForce.Items.Insert(0, "---Select Field Force---");

        }
    }
    [WebMethod(EnableSession = true)]
    public static GetDatas[] GetMissedData(string sf_Code, string FYear, string FMonth, string SubDiv)
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

        Product pro = new Product();
        List<GetDatas> empList = new List<GetDatas>();
        DCR dcn = new DCR();
		query dc = new query();
        DateTime first = new DateTime(Convert.ToInt32(FYear), Convert.ToInt32(FMonth), 1);
        DateTime last = first.AddMonths(1).AddSeconds(-1);
		DataSet updt = dc.updt_status(sf_Code, div_code, FYear, FMonth);
        //DataSet lst = dcn.gen_missed_data(sf_Code, div_code, first.ToString("yyyy-MM-dd"), last.ToString("yyyy-MM-dd"));
        DataSet dsPro = dc.get_missed_data(sf_Code, FYear, FMonth,div_code);
        foreach (DataRow row in dsPro.Tables[0].Rows)
        {
            GetDatas emp = new GetDatas();
            emp.MDate = row["mDate"].ToString();
			emp.sun_val = row["sun_val"].ToString();
            empList.Add(emp);
        }
        return empList.ToArray();
    }

    [WebMethod(EnableSession = true)]
    public static string LeavedDate(string sf_Code, string Dates)
    {
        query qr = new query();
        bool ds = qr.LeaveApplied(sf_Code, Dates, div_code);
        if (ds == true)
        {
            return "Leave";
        }
        else
        {
            return "Error";
        }
    }
	
	[WebMethod(EnableSession = true)]
    public static string leavedel_rel(string sf_Code, string Dates)
    {
        query qr = new query();
        bool ds = qr.dcrleave_delt(sf_Code, Dates, div_code);
        if (ds == true)
        {
            return "Success";
        }
        else
        {
            return "Error";
        }
    }
	
    [WebMethod(EnableSession = true)]
    public static string FieldWDate(string sf_Code, string Dates,string stcode,string year, string month)
    {
        query qr = new query();
        DataSet ds = qr.chkfieldwork(sf_Code, Dates, div_code, stcode, year,month);
        if (ds.Tables[0].Rows.Count > 0)
            return "FW";
        else
            return "Error";
    }
    public class query
    {
		public DataSet updt_status(string sf_code, string div_code, string FYear, string FMonth)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet dsAdmin = null;
            string strQry = "update dcr_misseddates set status=0 where sf_code='"+ sf_code+"' and status=4 and month='"+ FMonth+"' and year='"+ FYear+ "' and cast(Dcr_Missed_Date as date)<>cast(getdate() as date) and division_code='"+ div_code + "' ";
            try
            {
                dsAdmin = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsAdmin;
        }
		
		public int Releas_missed_data(string sf_code, string date,string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            int iReturn = -1;
            string strQry = "exec release_misseddate '"+ sf_code + "','"+ date + "','"+ div_code + "'";
            try
            {
                iReturn = db_ER.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }
		
		public DataSet get_missed_data(string sf_code, string FYear, string FMonth,string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            //strQry = "select convert(varchar, Dcr_Missed_Date,103) as mDate  from DCR_MissedDates d inner join Mas_Salesforce s on s.Sf_Code = d.sf_code where d.Sf_Code = '" + sf_code + "' and year = '" + FYear + "' and month = '" + FMonth + "' and Status=0 group by convert(varchar, Dcr_Missed_Date,103) order by mDate";
            string strQry = "exec Missed_dates_New_test '" + sf_code + "','" + FYear + "','" + FMonth + "','"+div_code+"'";
            try
            {
                dsAdmin = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsAdmin;
        }
		
		public bool LeaveApplied(string sfcode, string date, string div_code)
        {
            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();
                
                string strQry = "select count(*) from dcrmain_trans where sf_code='"+ sfcode + "' and fieldwork_indicator='L' and division_code='"+ div_code + "' and cast(activity_Date as date)='"+ date + "'";
                int iRecordExist = db.Exec_Scalar(strQry);

                if (iRecordExist > 0)
                    bRecordExist = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return bRecordExist;
        }
		
		public bool dcrleave_delt(string sfcode, string date, string div_code)
        {
            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();
                
                string strQry = "exec dcrLeave_delt '"+ sfcode + "','"+ div_code + "','"+ date + "'";
                int iRecordExist = db.Exec_Scalar(strQry);

                if (iRecordExist > 0)
                    bRecordExist = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return bRecordExist;
        }
		
        public DataSet chkfieldwork(string sfcode, string date, string div_code,string stcode,string year,string month)
        {
            DataSet bRecordExist = null;
            try
            {
                DB_EReporting db = new DB_EReporting();

                string strQry = "exec checkFF_inattendance_rpt '"+ sfcode + "','"+ date + "','"+ div_code + "','"+ stcode + "','"+ month + "','"+ year + "'";
                bRecordExist = db.Exec_DataSet(strQry);

                //if (iRecordExist > 0)
                //    bRecordExist = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return bRecordExist;
        }
    }
    public class GetDatas
    {
        public string MDate { get; set; }
		
		public string sun_val { get; set; }
    }

    [WebMethod(EnableSession = true)]
    public static string ReleasMissedDate(string sf_Code, string Dates)
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
        Product pro = new Product();
        query qr = new query();
        Dates = Dates.TrimEnd(',');
        string[] strArr = null;
        strArr = Dates.Split(',');
        DCR dcn = new DCR();
        int count = 0;
        for (int i = 0; i < strArr.Length; i++)
        {
            //DateTime dt = DateTime.ParseExact(strArr[i], "dd/MM/yyyy", CultureInfo.InvariantCulture);
            int succ = qr.Releas_missed_data(sf_Code, strArr[i],div_code);
            count++;
        }
        if (strArr.Length == count)
        {
            return "Success";
        }
        else
        {
            return "Error";
        }
    }
}