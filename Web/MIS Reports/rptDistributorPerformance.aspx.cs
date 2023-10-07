using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;


using Bus_EReport;
using System.Data;
using System.Web.Services;

public partial class MIS_Reports_rptDistributorPerformance : System.Web.UI.Page
{
    #region "Declaration"
    DataSet dsSalesForce = new DataSet();

    DataSet dsDisAllDtls = new DataSet();
    string DivCode = string.Empty;
    string SFCode = string.Empty;
    string FDate = string.Empty;
    string TDate = string.Empty;
    string sub_Div_Code = string.Empty;
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        DivCode = Session["div_code"].ToString();
        SFCode = Session["Sf_Code"].ToString();
        SFCode = Request.QueryString["SF_Code"].ToString();
         FDate = Request.QueryString["Fdates"].ToString();
        TDate = Request.QueryString["Tdates"].ToString();
        sub_Div_Code = Request.QueryString["Sub_Div"].ToString();
		DateTime d1 = Convert.ToDateTime(FDate);
        DateTime d2 = Convert.ToDateTime(TDate);
        hdnSFCode.Value = SFCode;
        hdnSubDivCode.Value = sub_Div_Code;
        hdnMonth.Value = FDate;
        hdnYear.Value = TDate;
        DateTimeFormatInfo mfi = new DateTimeFormatInfo();
        lblhead1.Text = "Distributor Performance for  " + d1.ToString("dd-MM-yyyy") + " to " + d2.ToString("dd-MM-yyyy");
        Label1.Text = "<b>Team </b>: " + Request.QueryString["SF_Name"].ToString();
        Label2.Text = "Loading Please Wait..!";

    }

    public class Designation
    {
        public string DesigCode { get; set; }
        public string DesigName { get; set; }

    }

    [WebMethod(EnableSession = true)]
    public static Designation[] GetDesg()
    {
        List<Designation> DesList = new List<Designation>();
        return DesList.ToArray();
    }
    public class DistList
    {
        public string sfCode { get; set; }
        public string sfName { get; set; }
        public string DistCode { get; set; }
        public string DistName { get; set; }
        public string EmpID { get; set; }
        public string Desig { get; set; }
        public string sfHQ { get; set; }
        public string stateName { get; set; }
        public string TRetailes { get; set; }
        public string RSFs { get; set; }
    }
    [WebMethod(EnableSession = true)]
    public static DistList[] GetDistriButers(string SF_Code, string SubDiv)
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
        Stockist stk = new Stockist();
        DataSet dsDistributor = new DataSet();
        dsDistributor = stk.GetStockist_FieldForceWise(div_code, SubDiv, SF_Code);
        List<DistList> dsList = new List<DistList>();
        foreach (DataRow row in dsDistributor.Tables[0].Rows)
        {
            DistList dl = new DistList();
            dl.sfCode = row["sf_code"].ToString();
            dl.sfName = row["sf_name"].ToString();
            dl.DistCode = row["Stockist_code"].ToString();
            dl.DistName = row["Stockist_Name"].ToString();
            dl.EmpID = row["sf_emp_id"].ToString();
            dl.Desig = row["Designation"].ToString();
            dl.sfHQ = row["sf_hq"].ToString();
            dl.stateName = row["StateName"].ToString();
            dl.TRetailes = row["TRC"].ToString();
            dl.RSFs = row["Reporting_To_SF"].ToString();

            dsList.Add(dl);
        }
        return dsList.ToArray();
    }


    public class DistDetaile
    {
        public string Sf_Code { get; set; }
        public string DistCode { get; set; }
        public string Sf_Name { get; set; }        
        public string DistName { get; set; }
        public string TVisited { get; set; }
        public string TNonVisited { get; set; }
        public string TProductive { get; set; }
        public string TNonProductive { get; set; }
        public string Coverd { get; set; }
        public string Bill_TVisited { get; set; }
        public string Bill_TRetailers { get; set; }
        public string NewRetailers { get; set; }
        public string NewRetValues { get; set; }
        public string TotValues { get; set; }
    }



    [WebMethod(EnableSession = true)]
    public static DistDetaile[] GetDistDetailsValue(string SF_Code, string FYera, string FMonth, string SubDiv)
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

        Stockist stk = new Stockist();
        DataSet dsDetaile = new DataSet();

        dsDetaile = stk.GetStockist_All_Details(div_code, SF_Code, FYera, FMonth, SubDiv);
        List<DistDetaile> dsDtls = new List<DistDetaile>();
        foreach (DataRow row in dsDetaile.Tables[0].Rows)
        {
            DistDetaile dtls = new DistDetaile();
            dtls.Sf_Code = row["sf_code"].ToString();
            dtls.DistCode = row["Stockist_code"].ToString();
            dtls.TVisited = row["TVRC"].ToString();
            dtls.TProductive = row["TPRC"].ToString();
            dtls.TNonProductive = row["TZOR"].ToString();
            dtls.NewRetailers = row["NRC"].ToString();
            dtls.NewRetValues = row["NRVAL"].ToString();
            dtls.TotValues = row["Order_Value"].ToString();
            dsDtls.Add(dtls);
        }
        return dsDtls.ToArray();
    }




    [WebMethod(EnableSession = true)]
    public static DistDetaile[] GetDistDetailsValueNotMatch(string SF_Code, string FYera, string FMonth, string SubDiv)
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

        Stockist stk = new Stockist();
        DataSet dsDetaile = new DataSet();

        dsDetaile = stk.GetStockist_All_Details_notmatch(div_code, SF_Code, FYera, FMonth, SubDiv);
        List<DistDetaile> dsDtls = new List<DistDetaile>();
        foreach (DataRow row in dsDetaile.Tables[0].Rows)
        {
            DistDetaile dtls = new DistDetaile();
            dtls.Sf_Code = row["sf_code"].ToString();
            dtls.Sf_Name = row["sf_name"].ToString();
            dtls.DistCode = row["Stockist_code"].ToString();
            dtls.DistName = row["stockist_name"].ToString();
            dtls.TVisited = row["TVRC"].ToString();
            dtls.TProductive = row["TPRC"].ToString();
            dtls.TNonProductive = row["TZOR"].ToString();
            dtls.NewRetailers = row["NRC"].ToString();
            dtls.NewRetValues = row["NRVAL"].ToString();
            dtls.TotValues = row["Order_Value"].ToString();
            dsDtls.Add(dtls);
        }
        return dsDtls.ToArray();
    }



    public class ReportingSF
    {
        public string Sf_Code { get; set; }
        public string sf_Name { get; set; }
        public string RSF_Code { get; set; }
        public string Designation { get; set; }
    }

    [WebMethod(EnableSession = true)]
    public static ReportingSF[] GetReportingToSF(string SF_Code, string SubDiv)
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
        SalesForce sf = new SalesForce();
        DataSet dsSFHead = new DataSet();
        dsSFHead = sf.SalesForceListMgrGet_MgrOnly(div_code, "admin", "0");
        List<ReportingSF> dsDtls = new List<ReportingSF>();
        foreach (DataRow row in dsSFHead.Tables[0].Rows)
        {
            ReportingSF dtls = new ReportingSF();
            dtls.Sf_Code = row["Sf_Code"].ToString();
            dtls.sf_Name = row["Sf_Name"].ToString();
            dtls.RSF_Code = row["Reporting_To_SF"].ToString();
            dtls.Designation = row["Designation"].ToString();

            dsDtls.Add(dtls);
        }
        return dsDtls.ToArray();
    }


    [WebMethod(EnableSession = true)]
    public static Categorys[] GetCategoryHead()
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

        Product prd = new Product();
        DataTable dtCategory = new DataTable();

        dtCategory = prd.getProductCategorylist_DataTable(div_code);
        List<Categorys> dsDtls = new List<Categorys>();
        foreach (DataRow row in dtCategory.Rows)
        {
            Categorys dtls = new Categorys();
            dtls.Prd_Cat_Code = row["Product_Cat_Code"].ToString();
            dtls.Prd_Cat_Name = row["Product_Cat_Name"].ToString();
            dsDtls.Add(dtls);
        }
        return dsDtls.ToArray();
    }




    public class Categorys
    {
        public string SF_Code { get; set; }
        public string Dist_Code { get; set; }
        public string Prd_Cat_Code { get; set; }
        public string Prd_Cat_Name { get; set; }
        public string Cat_Values { get; set; }
        public string Cat_Qty { get; set; }
		public string Cat_RtCount { get; set; } 

    }


    [WebMethod(EnableSession = true)]
    public static Categorys[] GetCategoryValue(string SF_Code, string FYera, string FMonth, string SubDiv)
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


        Stockist stk = new Stockist();
        DataSet dsCategory = new DataSet();

        dsCategory = stk.CategorywiseDistandSF(div_code, SF_Code, FYera, FMonth, SubDiv);
        List<Categorys> dsDtls = new List<Categorys>();
        foreach (DataRow row in dsCategory.Tables[0].Rows)
        {
            Categorys dtls = new Categorys();
            dtls.SF_Code = row["sf_code"].ToString();
            dtls.Dist_Code = row["stockist_code"].ToString();
            dtls.Prd_Cat_Code = row["Product_Cat_Code"].ToString();
            //  dtls.Prd_Cat_Name = row["TNVRC"].ToString();
            dtls.Cat_Values = row["value"].ToString();
            dtls.Cat_Qty = row["Qty"].ToString();
			dtls.Cat_RtCount = row["Rtcnt"].ToString();
	

            dsDtls.Add(dtls);
        }
        return dsDtls.ToArray();
    }


    [WebMethod(EnableSession = true)]
    public static Categorys[] GetCategoryValuenotmatch(string SF_Code, string FYera, string FMonth, string SubDiv)
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


        Stockist stk = new Stockist();
        DataSet dsCategory = new DataSet();

        dsCategory = stk.CategorywiseDistandSF_notmatch(div_code, SF_Code, FYera, FMonth, SubDiv);
        List<Categorys> dsDtls = new List<Categorys>();
        foreach (DataRow row in dsCategory.Tables[0].Rows)
        {
            Categorys dtls = new Categorys();
            dtls.SF_Code = row["sf_code"].ToString();
            dtls.Dist_Code = row["stockist_code"].ToString();
            dtls.Prd_Cat_Code = row["Product_Cat_Code"].ToString();
            //  dtls.Prd_Cat_Name = row["TNVRC"].ToString();
            dtls.Cat_Values = row["value"].ToString();
            dtls.Cat_Qty = row["Qty"].ToString();
            dtls.Cat_RtCount = row["Rtcnt"].ToString();

            dsDtls.Add(dtls);
        }
        return dsDtls.ToArray();
    }


    private void FillGrid()
    {//CategorywiseDistandSF


        //Stockist stk = new Stockist();
        //dsDistributor = stk.GetStockist_FieldForceWise(DivCode, SFCode);
        //dsDisAllDtls = stk.GetStockist_All_Details(DivCode, FYear, FMonth);
        //if (dsDistributor.Tables[0].Rows.Count > 0)
        //{
        //    grdDistributor.DataSource = dsDistributor;
        //    grdDistributor.DataBind();
        //}
        //else
        //{
        //    grdDistributor.DataSource = null;
        //    grdDistributor.DataBind();
        //}
    }




}