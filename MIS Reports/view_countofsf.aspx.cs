using Bus_EReport;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using Newtonsoft.Json;
using DBase_EReport;

public partial class MIS_Reports_view_countofsf : System.Web.UI.Page
{
    public string wtype = string.Empty;
    public string sfc = string.Empty;
    public string dt = string.Empty;
    public string sd = string.Empty;
   public string datez = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        sfc = Request.QueryString["SFCode"].ToString();
        dt = Request.QueryString["Dates"].ToString();
        wtype = Request.QueryString["wtype"].ToString();
        sd = Request.QueryString["subdiv"].ToString();
        DateTime d1 = Convert.ToDateTime(dt);
        datez = d1.ToString("dd/M/yyyy");

        wt.Value = wtype;
        sc.Value = sfc;
        dat.Value = dt;
        sdv.Value = sd;
        ddd.Value = datez;
        if (wtype == "NL")
        {
            lblHead.Text = "Not Login Details"+" "+ d1.ToString("dd-MM-yyyy");
        }
        if (wtype == "F")
        {
            lblHead.Text = "Field Work Details"+" "+ d1.ToString("dd-MM-yyyy");
        }
        if (wtype == "L")
        {
            lblHead.Text = "Leave Details"+" "+ d1.ToString("dd-MM-yyyy");
        }
        if (wtype == "N")
        {
            lblHead.Text = "Other Work Details"+" "+d1.ToString("dd-MM-yyyy");
        }
    }
    [WebMethod(EnableSession = true)]
    public static string getdatasf(string wtyp, string sfc, string cdate, string subc, string divc)
    {
        tst dv = new tst();
        DataSet dsProd = dv.dashbwtype__sflist(wtyp, sfc, cdate, subc, divc);
        return JsonConvert.SerializeObject(dsProd.Tables[0]);
    }
    [WebMethod(EnableSession = true)]

    public static string getdatanlsf(string wtyp, string sfc, string cdate, string subc, string divc)
    {
        tst dv = new tst();
        DataSet dsProd = dv.dashbcntrywisenotlogin__sflist(wtyp, sfc, cdate, subc, divc);
        return JsonConvert.SerializeObject(dsProd.Tables[0]);
    }
	public class tst
    {
        public DataSet dashbwtype__sflist(string wtyp, string sfc, string cdate, string subc, string divc)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            string strQry = "exec getdashboard_wtype_Oth '" + sfc + "','" + divc + "','" + cdate + "','" + subc + "','" + wtyp + "'";
            try
            {
                dsDivision = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsDivision;
        }
        public DataSet dashbcntrywisenotlogin__sflist(string wtyp, string sfc, string cdate, string subc, string divc)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            string strQry = "exec getdashboard_wtype_nlg '" + sfc + "','" + divc + "','" + cdate + "','" + subc + "'";
            try
            {
                dsDivision = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsDivision;
        }
    }
}