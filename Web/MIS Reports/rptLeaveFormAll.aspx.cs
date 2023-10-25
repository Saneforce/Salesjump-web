using Bus_EReport;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MIS_Reports_rptLeaveFormAll : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        lblhead.Text = "Leave Availablity Status for Year : " + Request.QueryString["FYear"].ToString();
        subhead.Text = "Team : " + Request.QueryString["SFName"].ToString();
        hsfcode.Value = Request.QueryString["SFCode"].ToString();
        hfyear.Value = Request.QueryString["FYear"].ToString();
        hsubdiv.Value = Request.QueryString["SubDiv"].ToString();
    }

    public class TotOrder
    {
        public string sfCode { get; set; }
        public string sfName { get; set; }
        public string LeaveCode { get; set; }
        public string LeaveValue { get; set; }
        public string LeaveAvailability { get; set; }
        public string LeaveTaken { get; set; }
    }

    [WebMethod(EnableSession = true)]
    public static TotOrder[] GetLeaveSF(string SF_Code, string FYear, string SubDiv)
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
        Holiday sf = new Holiday();
        DataSet dsSalesForce = sf.GetLeaveSFWise(div_code, SF_Code, FYear, "0");
        List<TotOrder> vList = new List<TotOrder>();
        foreach (DataRow row in dsSalesForce.Tables[0].Rows)
        {
            TotOrder vl = new TotOrder();
            vl.sfCode = row["SFCode"].ToString();
            vl.sfName = row["sf_name"].ToString();
            vl.LeaveCode = row["LeaveCode"].ToString();
            vl.LeaveValue = row["LeaveValue"].ToString();
            vl.LeaveAvailability = row["LeaveAvailability"].ToString();
            vl.LeaveTaken = row["LeaveTaken"].ToString();


            vList.Add(vl);
        }
        return vList.ToArray();
    }

    public class LeaveType
    {
        public string Leave_code { get; set; }
        public string Leave_Name { get; set; }
    }


    [WebMethod(EnableSession = true)]
    public static LeaveType[] getLeaveCode()
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
        Holiday sf = new Holiday();
        DataSet dsSalesForce = sf.GetLeaveTypes(div_code);
        List<LeaveType> vList = new List<LeaveType>();
        foreach (DataRow row in dsSalesForce.Tables[0].Rows)
        {
            LeaveType vl = new LeaveType();
            vl.Leave_code = row["Leave_code"].ToString();
            vl.Leave_Name = row["Leave_Name"].ToString();
            vList.Add(vl);
        }
        return vList.ToArray();
    }
}