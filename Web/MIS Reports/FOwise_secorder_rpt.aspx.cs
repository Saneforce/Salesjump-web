using Bus_EReport;
using DBase_EReport;
using Newtonsoft.Json;
using System;
using System.Data;
using System.Web.Services;
using System.Web.UI.WebControls;

public partial class MIS_Reports_FOwise_secorder_rpt : System.Web.UI.Page
{

    #region "Declaration"
    DateTime ServerStartTime;
    DataSet dsSalesForce = null;
    public static string sf_code = string.Empty;
    DataSet dsTP = null;
    DateTime ServerEndTime;
    string div_code = string.Empty;
    public string sf_type = string.Empty;
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
        div_code = Session["div_code"].ToString();
        sf_code = Session["sf_code"].ToString();
        sub_division = Session["sub_division"].ToString();
    }

    protected void gridView_PreRender(object sender, EventArgs e)
    {
        //GridDecorator.MergeRows(GridView1);

    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        
    }
    [WebMethod]
    public static string getFieldForce(string Div)
    {
        SalesForce sd = new SalesForce();
        DataSet ds = new DataSet();
        ds = sd.SalesForceList(Div, sf_code, sub_division);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    [WebMethod]
    public static string getdatalist(string Div, string fdate, string tdate,string sfcode)
    {

        //Form1 frm1 = new Form1();
        //BindGridd(fdate, tdate);
        empval sd = new empval();
        DataSet ds = new DataSet();
        ds = sd.empordervalue(Div, sfcode, fdate, tdate);
        return JsonConvert.SerializeObject(ds.Tables[0]);
        //return "success";
    }
    public class empval
    {
        public DataSet empordervalue(string div_code, string sfcode, string fdate, string tdate)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsState = null;
            string strQry = "exec sp_secorder_empval '" + sfcode + "','" + div_code + "','" + fdate + "','" + tdate + "'";

            try
            {
                dsState = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsState;
        }
    }
}