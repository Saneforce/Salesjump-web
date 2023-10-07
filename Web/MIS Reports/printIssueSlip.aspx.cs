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

public partial class MIS_Reports_printIssueSlip : System.Web.UI.Page
{
    string sfCode = string.Empty;
    string frmDt = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        sfCode = Request.QueryString["SF_Code"].ToString();
        frmDt = Request.QueryString["fromDate"].ToString();
        hdnsfcode.Value = sfCode;
        fromDate.Value = frmDt;
    }

    [WebMethod(EnableSession = true)]
    public static IssueDetails[] getIssuData(string fromDate, string SFCode)
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



       // sf_Code = HttpContext.Current.Session["SF_Code"].ToString();
        DateTime dtgrn = DateTime.ParseExact(fromDate, "dd/MM/yyyy", null);
        string strgrn = dtgrn.ToString("yyyy-MM-dd");

        Product pro = new Product();
        List<IssueDetails> empList = new List<IssueDetails>();
        Expense exp = new Expense();
        DataSet dsPro = exp.getrptIssueSlip(div_code, SFCode, strgrn, "0");
        foreach (DataRow row in dsPro.Tables[0].Rows)
        {
            IssueDetails emp = new IssueDetails();
            emp.sfCode = row["sf_code"].ToString();
            emp.sfName = row["sf_name"].ToString();
            emp.proCode = row["Product_Detail_Code"].ToString();
            emp.proName = row["Product_Short_Name"].ToString();
            emp.caseRate = row["CaseQty"].ToString();
            emp.piceRate = row["PiceQty"].ToString();
            emp.amount = row["amount"].ToString();
            emp.rate = row["RateP_Retail"].ToString();
            emp.StkNm = row["StkNm"].ToString();
            empList.Add(emp);
        }
        return empList.ToArray();
    }

    public class IssueDetails
    {
        public string sfCode { get; set; }
        public string sfName { get; set; }
        public string proCode { get; set; }
        public string proName { get; set; }
        public string caseRate { get; set; }
        public string piceRate { get; set; }
        public string amount { get; set; }
        public string rate { get; set; }
        public string StkNm { get; set; }
    }

    [WebMethod(EnableSession = true)]
    public static string getReportingtoSF(string SFCode)
    {
        SalesForce sf = new SalesForce();
        DataSet dsFo = sf.getReportingTo(SFCode);
        string ReprtingSF  = string.Empty;
        if(dsFo.Tables[0].Rows.Count>0)
        {
            ReprtingSF = dsFo.Tables[0].Rows[0][0].ToString();
        }
        DataSet dsFF = sf.getR_ToSFName(ReprtingSF);
        string sf_Name  = string.Empty;
        if (dsFF.Tables[0].Rows.Count > 0)
        {
            sf_Name = dsFF.Tables[0].Rows[0]["sf_name"].ToString();
        }
        return sf_Name;
    }  

}