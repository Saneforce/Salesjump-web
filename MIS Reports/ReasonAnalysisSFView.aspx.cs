using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Globalization;
using System.IO;
using System.Data;
using Bus_EReport;
using Newtonsoft.Json;
using System.Web.Services;
using System.Data.SqlClient;
public partial class MIS_Reports_ReasonAnalysisSFView : System.Web.UI.Page
{
    #region "Declaration"      
    DataSet dsSalesForce = null;
    string sf_code = string.Empty;
    string div_code = string.Empty;
    string SubDiv = string.Empty;
    string FYear = string.Empty;
    string FMonth = string.Empty;
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        sf_code = Request.QueryString["SFCode"].ToString();
        FYear = Request.QueryString["FYear"].ToString();
        FMonth = Request.QueryString["FMonth"].ToString();
        SubDiv = Request.QueryString["SubDiv"].ToString();

        ddlFieldForce.Value = sf_code;
        ddlFYear.Value = FYear;
        ddlFMonth.Value = FMonth;
        SubDivCode.Value = SubDiv;
        Label1.Text = Request.QueryString["SFName"].ToString();

		string  monthName = DateTimeFormatInfo.CurrentInfo.GetAbbreviatedMonthName( Convert.ToInt16(FMonth));

		Label2.Text = "Field Force wise Reason Analysis for "+ monthName +"-"+FYear;
    }


    [WebMethod(EnableSession = true)]
    public static GetDatas[] GetData(string SF_Code, string FYera, string FMonth)
    {
        string div_code = "";
        string sf_Code = "";
        try
        {
            div_code = HttpContext.Current.Session["div_code"].ToString();

        }
        catch
        {
            div_code = HttpContext.Current.Session["Division_Code"].ToString();
        }

        sf_Code = HttpContext.Current.Session["SF_Code"].ToString();



        List<GetDatas> empList = new List<GetDatas>();
        DCR_New dcn = new DCR_New();
        DataSet dsPro = dcn.getRemarksVal(div_code, SF_Code, FYera, FMonth, "0");
        foreach (DataRow row in dsPro.Tables[0].Rows)
        {
            GetDatas emp = new GetDatas();
            emp.sfCode = row["sf_code"].ToString();
            emp.sfName = row["Sf_Name"].ToString();
            emp.remarks = row["Activity_Remarks"].ToString();
            emp.cnt = row["cnt"].ToString();
            empList.Add(emp);
        }
        return empList.ToArray();
    }

    public class GetDatas
    {
        public string sfCode { get; set; }
        public string sfName { get; set; }
        public string remarks { get; set; }
        public string cnt { get; set; }


    }
}