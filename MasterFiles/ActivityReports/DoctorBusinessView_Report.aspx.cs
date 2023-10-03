using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bus_EReport;

public partial class DoctorBusinessView_Report : System.Web.UI.Page
{
    string strSfCode = string.Empty, fromMonth = string.Empty, fromYear = string.Empty, toMonth = string.Empty, toYear = string.Empty;
    string strFromMonthYear = string.Empty, strToMonthYear = string.Empty;
    DCRBusinessEntry objBusiness = new DCRBusinessEntry();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["sf_Code"] != null)
            {
                strSfCode = Convert.ToString(Request.QueryString["sf_Code"]);
            }
            if (Request.QueryString["fromMonth"] != null)
            {
                fromMonth = Convert.ToString(Request.QueryString["fromMonth"]);
            }
            if (Request.QueryString["toMonth"] != null)
            {
                toMonth = Convert.ToString(Request.QueryString["toMonth"]);
            }
            if (Request.QueryString["fromYear"] != null)
            {
                fromYear = Convert.ToString(Request.QueryString["fromYear"]);
            }
            if (Request.QueryString["toYear"] != null)
            {
                toYear = Convert.ToString(Request.QueryString["toYear"]);
            }

            if (fromMonth != string.Empty && fromYear != string.Empty)
            {
                strFromMonthYear = Convert.ToDateTime("01/" + fromMonth + "/" + fromYear).ToString("yyyyMMdd");
            }

            if (toMonth != string.Empty && toYear != string.Empty)
            {
                strToMonthYear = Convert.ToDateTime("01/" + toMonth + "/" + toYear).ToString("yyyyMMdd");
            }

            this.BindGrid(strSfCode, strFromMonthYear, strToMonthYear);
        }
    }

    private void BindGrid(string sfCode, string fromMonthYear,string strToMonthYear)
    {
        DataSet dsTarget = null;
        dsTarget = objBusiness.GetDCRBusinessReport(sfCode, fromMonthYear, strToMonthYear);
        gvDoctorBusiness.DataSource = dsTarget;
        gvDoctorBusiness.DataBind();
    }
    protected void gvDoctorBusiness_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            GridViewRow grow = e.Row;
            for (int cnt = 4; cnt < grow.Cells.Count; cnt++)
            {
                HyperLink link = new HyperLink();
                link.Text = e.Row.Cells[cnt].Text;
                string headerText = gvDoctorBusiness.HeaderRow.Cells[cnt].Text;
                string strSfCode = e.Row.Cells[0].Text;
                link.NavigateUrl = "javascript:OpenReport('" + strSfCode + "','" + headerText + "');";
                e.Row.Cells[cnt].Controls.Add(link);
                
            }

            e.Row.Cells[0].Visible = false;
        }
    }

    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> GetDoctorList(string prefixText, int count)
    {
        DataSet dsListedDR = new DataSet();
        ListedDR lstDR = new ListedDR();
        List<string> doctorList = new List<string>();
        DataTable _objdt = new DataTable();
        dsListedDR = lstDR.GetDataFromDataBase(prefixText);
        if (dsListedDR.Tables[0].Rows.Count > 0)
        {
            for (int i = 0; i < dsListedDR.Tables[0].Rows.Count; i++)
            {
                doctorList.Add(dsListedDR.Tables[0].Rows[i]["ListedDr_Name"].ToString());
            }
        }

        // Find All Matching Products
        //var list = from p in doctorList
        //           where p.Contains(prefixText)
        //           select p;

        ////Convert to Array as We need to return Array
        //string[] prefixTextArray = doctorList.ToArray<string>();

        //Return Selected doctors
        return doctorList;
    }
}