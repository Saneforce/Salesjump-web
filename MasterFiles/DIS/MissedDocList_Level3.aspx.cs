using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;

public partial class MIS_Reports_MissedDocList_Level3 : System.Web.UI.Page
{
    DataSet dsDoctor = null;
    string sMode = string.Empty;
    string sf_code = string.Empty;
    string FMonth = string.Empty;
    string FYear = string.Empty;
    string TMonth = string.Empty;
    string TYear = string.Empty;
    string div_code = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        sMode = Request.QueryString["cMode"].ToString();
        sf_code = Request.QueryString["sfcode"].ToString();
        FMonth = Request.QueryString["FMonth"].ToString();
        FYear = Request.QueryString["FYear"].ToString();

        FillDoctor();

    }

    private void FillDoctor()
    {
        DateTime dtCurrent;
        string sCurrentDate = string.Empty;

        if (Convert.ToInt16(FMonth) == 12)
        {
            sCurrentDate = "01-01-" + (Convert.ToInt16(FYear) + 1);
        }
        else
        {
            sCurrentDate = (Convert.ToInt16(FMonth) + 1) + "-01-" + Convert.ToInt16(FYear);
        }

        dtCurrent = Convert.ToDateTime(sCurrentDate);
        Doctor dc = new Doctor();
        dsDoctor = dc.Missed_Doc(div_code, sf_code, Convert.ToInt16(FMonth), Convert.ToInt16(FYear), dtCurrent, sMode );

        //Doctor dc = new Doctor();
        //dsDoctor = dc.Missed_Doc(div_code, sf_code, Convert.ToInt16(FMonth), Convert.ToInt16(FYear) );

        if (dsDoctor.Tables[0].Rows.Count > 0)
        {
            grdDoctor.Visible = true;
            grdDoctor.DataSource = dsDoctor;
            grdDoctor.DataBind();
        }

    }


}