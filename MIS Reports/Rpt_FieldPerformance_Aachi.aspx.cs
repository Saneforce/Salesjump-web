using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
using System.Data.SqlClient;
using System.Web.Services;
using System.Configuration;
using Newtonsoft.Json;
using System.Globalization;
using ClosedXML.Excel;
using System.Xml;
using System.IO;
using System.Data.OleDb;
using DocumentFormat.OpenXml.Vml;
using Org.BouncyCastle.Utilities;
using DocumentFormat.OpenXml.Drawing.Charts;
using iTextSharp.tool.xml.html;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.ExtendedProperties;

public partial class MIS_Reports_Rpt_FieldPerformance_Aachi : System.Web.UI.Page
{

    public static string Div = string.Empty;
    public static string sf_type = string.Empty;
    public static string sfcode = string.Empty;
    public static string sfname = string.Empty;
    public static string subdiv = string.Empty;
    public static string FDT = string.Empty;
    public static string TDT = string.Empty;
    public DateTime rdt;
    public DateTime sdt;

    
    public static System.Data.DataTable dpdt = new System.Data.DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {

        Div = Session["div_code"].ToString();
        sfcode = Request.QueryString["SF_Code"].ToString();
        sfname = Request.QueryString["SF_Name"].ToString();
        FDT = Request.QueryString["Fdate"].ToString();
        TDT = Request.QueryString["Tdate"].ToString();
        subdiv = Request.QueryString["Sub_Div"].ToString();

        Label2.Text = "Field Force Name : " + Convert.ToString(sfname);

        if (sfcode == null || sfcode == "")
        { sfcode = "admin"; }

        if (subdiv == null || subdiv == "")
        { subdiv = "0"; }

        if (FDT == null || FDT == "")
        { FDT = DateTime.Now.ToString("dd-mm-yyyy"); }

        if (TDT == null || TDT == "")
        { TDT = DateTime.Now.ToString("dd-mm-yyyy"); }


        //if (!Page.IsPostBack)
        //{ BindGridView(); }

    }


    [WebMethod]
    public static string getDayPlan()
    {
        DCR SFD = new DCR();
        DataSet ds = SFD.getDayplan(sfcode, Div, FDT, TDT, subdiv);
        System.Data.DataTable fdt = new System.Data.DataTable();

        if(ds.Tables.Count > 0)
        {
            fdt = ds.Tables[0];
        }
        return JsonConvert.SerializeObject(fdt);
    }


    private void BindGridView()
    {

        DCR SFD = new DCR();
        DataSet ds = SFD.getDayplan(sfcode, Div, FDT, TDT, subdiv);
        System.Data.DataTable fdt = new System.Data.DataTable();
        fdt.Columns.Add("SLNo");
        fdt.Columns.Add("Date");
        fdt.Columns.Add("StateName");
        fdt.Columns.Add("HQ");
        fdt.Columns.Add("UserName");
        fdt.Columns.Add("UserRank");
        fdt.Columns.Add("SRContactNo");
        fdt.Columns.Add("WType");
        fdt.Columns.Add("WorkedWithName");
        fdt.Columns.Add("Reason");
        fdt.Columns.Add("Distributors");
        fdt.Columns.Add("DistributorsAddress");
        fdt.Columns.Add("SelectedBeats");
        fdt.Columns.Add("AC");
        fdt.Columns.Add("TC");
        fdt.Columns.Add("PC");
        fdt.Columns.Add("Productivity");
        fdt.Columns.Add("NewRetailers");
        fdt.Columns.Add("NewRetailersPOB");
        fdt.Columns.Add("TelephonicOrders");
        fdt.Columns.Add("LoginTime");
        fdt.Columns.Add("LogOutTime");
        fdt.Columns.Add("TotalTime");
        fdt.Columns.Add("Lat");
        fdt.Columns.Add("Long");
        fdt.Columns.Add("FirstCall");
        fdt.Columns.Add("LastCall");
        fdt.Columns.Add("TotalRetailTime");
        fdt.Columns.Add("Value"); DataRow rows = fdt.NewRow();

        if (ds.Tables.Count > 0)
        {
            dpdt = new System.Data.DataTable();
            dpdt = ds.Tables[0];
             
            for (int k = 0; k < dpdt.Rows.Count; k++)
            {

                string End_Datetime = Convert.ToString(dpdt.Rows[k]["End_Datetime"]);
                string Start_Datetime = Convert.ToString(dpdt.Rows[k]["Start_Datetime"]);
                string totaltime = "", TotalRTime = "";

                string minmumTime = Convert.ToString(dpdt.Rows[k]["minmumTime"]);
                string MaximumTime = Convert.ToString(dpdt.Rows[k]["MaximumTime"]);

                string sdate = Convert.ToString(dpdt.Rows[k]["ActDate"]);


                if ((End_Datetime != "" || End_Datetime != null || End_Datetime != "1900-01-01 00:00:00.000"))
                {
                    TimeSpan tspan = (Convert.ToDateTime(dpdt.Rows[k]["End_Datetime"]) - Convert.ToDateTime(dpdt.Rows[k]["Start_Datetime"]));

                    totaltime = tspan.Hours + ":" + tspan.Minutes + ":" + tspan.Seconds;
                }
                else
                {
                    totaltime = "00:00:00";
                }

                if ((minmumTime != "" || minmumTime != null) && (MaximumTime != "" || MaximumTime != null) && (sdate != null || sdate != ""))
                {
                    string stime = sdate + " " + minmumTime;
                    string etime = sdate + " " + MaximumTime;

                    DateTime stimet = Convert.ToDateTime(DateTime.ParseExact(stime.ToString(), "yyyy-MM-dd HH:MM:ss tt", CultureInfo.InvariantCulture));
                    DateTime etimet = Convert.ToDateTime(DateTime.ParseExact(etime.ToString(), "yyyy-MM-dd HH:MM:ss tt", CultureInfo.InvariantCulture));

                    TimeSpan tspan = (Convert.ToDateTime(etimet) - Convert.ToDateTime(stimet));
                    TotalRTime = tspan.Hours + ":" + tspan.Minutes + ":" + tspan.Seconds;
                }
                else
                {
                    TotalRTime = "00:00:00";
                }

                rows = fdt.NewRow();

                rows["SLNo"] = k + 1;
                rows["Date"] = Convert.ToString(dpdt.Rows[k]["actdt"]);
                rows["StateName"] = Convert.ToString(dpdt.Rows[k]["StateName"]);
                rows["HQ"] = Convert.ToString(dpdt.Rows[k]["sf_hq"]);
                rows["UserName"] = Convert.ToString(dpdt.Rows[k]["SF_Name"]);
                rows["UserRank"] = Convert.ToString(dpdt.Rows[k]["Designation"]);
                rows["SRContactNo"] = Convert.ToString(dpdt.Rows[k]["SF_Mobile"]);
                rows["WType"] = Convert.ToString(dpdt.Rows[k]["Wtype"]);
                rows["WorkedWithName"] = Convert.ToString(dpdt.Rows[k]["worked_with_name"]);
                rows["SelectedBeats"] = Convert.ToString(dpdt.Rows[k]["ClstrName"]);
                rows["Distributors"] = Convert.ToString(dpdt.Rows[k]["StkName"]);
                rows["DistributorsAddress"] = Convert.ToString(dpdt.Rows[k]["Stockist_Address"]);
                rows["Reason"] = Convert.ToString(dpdt.Rows[k]["remarks"]);
                rows["AC"] = Convert.ToString(dpdt.Rows[k]["rcount"]);
                rows["TC"] = Convert.ToString(dpdt.Rows[k]["TCall"]);
                rows["PC"] = Convert.ToString(dpdt.Rows[k]["EFCall"]);
                rows["Productivity"] = Convert.ToString(dpdt.Rows[k]["productivity"]);
                rows["NewRetailers"] = Convert.ToString(dpdt.Rows[k]["NRC"]);
                rows["NewRetailersPOB"] = Convert.ToString(dpdt.Rows[k]["NRVAL"]);
                rows["Productivity"] = Convert.ToString(dpdt.Rows[k]["productivity"]);
                rows["TelephonicOrders"] = Convert.ToString(dpdt.Rows[k]["phoneOrderCnt"]);
                rows["LoginTime"] = Convert.ToString(dpdt.Rows[k]["Start_Time"]);
                rows["LogOutTime"] = Convert.ToString(dpdt.Rows[k]["End_Time"]);
                rows["TotalTime"] = Convert.ToString(totaltime);
                rows["Lat"] = Convert.ToString(dpdt.Rows[k]["Start_Lat"]);
                rows["Long"] = Convert.ToString(dpdt.Rows[k]["Start_Long"]);
                rows["FirstCall"] = Convert.ToString(dpdt.Rows[k]["minmumTime"]);
                rows["LastCall"] = Convert.ToString(dpdt.Rows[k]["MaximumTime"]);
                rows["TotalRetailTime"] = TotalRTime;
                rows["Value"] = Convert.ToString(dpdt.Rows[k]["orderAmt"]);

                fdt.Rows.Add(rows);
            }
        }             

        //gvffs.DataSource = fdt;
        //gvffs.DataBind();

    }

    protected void gvffs_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
       // gvffs.PageIndex = e.NewPageIndex;
        BindGridView();
    }

    protected void Export_to_Excel_Click(object sender, EventArgs e)
    {
        DCR SFD = new DCR();
        DataSet ds = SFD.getDayplan(sfcode, Div, FDT, TDT, subdiv);
        System.Data.DataTable objDS = new System.Data.DataTable();
        objDS.Columns.Add("SLNo");
        objDS.Columns.Add("Date");
        objDS.Columns.Add("StateName");
        objDS.Columns.Add("HQ");
        objDS.Columns.Add("UserName");
        objDS.Columns.Add("UserRank");
        objDS.Columns.Add("SRContactNo");
        objDS.Columns.Add("WType");
        objDS.Columns.Add("WorkedWithName");
        objDS.Columns.Add("Reason");
        objDS.Columns.Add("Distributors");
        objDS.Columns.Add("DistributorsAddress");
        objDS.Columns.Add("SelectedBeats");
        objDS.Columns.Add("AC");
        objDS.Columns.Add("TC");
        objDS.Columns.Add("PC");
        objDS.Columns.Add("Productivity");
        objDS.Columns.Add("NewRetailers");
        objDS.Columns.Add("NewRetailersPOB");
        objDS.Columns.Add("TelephonicOrders");
        objDS.Columns.Add("LoginTime");
        objDS.Columns.Add("LogOutTime");
        objDS.Columns.Add("TotalTime");
        objDS.Columns.Add("Lat");
        objDS.Columns.Add("Long");
        objDS.Columns.Add("FirstCall");
        objDS.Columns.Add("LastCall");
        objDS.Columns.Add("TotalRetailTime");
        objDS.Columns.Add("Value"); DataRow rows = objDS.NewRow();

        if (ds.Tables.Count > 0)
        {
            dpdt = new System.Data.DataTable();
            dpdt = ds.Tables[0];

            for (int k = 0; k < dpdt.Rows.Count; k++)
            {

                string End_Datetime = Convert.ToString(dpdt.Rows[k]["End_Datetime"]);
                string Start_Datetime = Convert.ToString(dpdt.Rows[k]["Start_Datetime"]);
                string totaltime = "", TotalRTime = "";

                string minmumTime = Convert.ToString(dpdt.Rows[k]["minmumTime"]);
                string MaximumTime = Convert.ToString(dpdt.Rows[k]["MaximumTime"]);

                string sdate = Convert.ToString(dpdt.Rows[k]["ActDate"]);


                if ((End_Datetime != "" || End_Datetime != null || End_Datetime != "1900-01-01 00:00:00.000"))
                {
                    TimeSpan tspan = (Convert.ToDateTime(dpdt.Rows[k]["End_Datetime"]) - Convert.ToDateTime(dpdt.Rows[k]["Start_Datetime"]));

                    totaltime = tspan.Hours + ":" + tspan.Minutes + ":" + tspan.Seconds;
                }
                else
                {
                    totaltime = "00:00:00";
                }

                if ((minmumTime != "" || minmumTime != null) && (MaximumTime != "" || MaximumTime != null) && (sdate != null || sdate != ""))
                {
                    string stime = sdate + " " + minmumTime;
                    string etime = sdate + " " + MaximumTime;

                    TimeSpan tspan = (Convert.ToDateTime(etime) - Convert.ToDateTime(stime));
                    TotalRTime = tspan.Hours + ":" + tspan.Minutes + ":" + tspan.Seconds;
                }
                else
                {
                    TotalRTime = "00:00:00";
                }

                rows = objDS.NewRow();

                rows["SLNo"] = k + 1;
                rows["Date"] = Convert.ToString(dpdt.Rows[k]["actdt"]);
                rows["StateName"] = Convert.ToString(dpdt.Rows[k]["StateName"]);
                rows["HQ"] = Convert.ToString(dpdt.Rows[k]["sf_hq"]);
                rows["UserName"] = Convert.ToString(dpdt.Rows[k]["SF_Name"]);
                rows["UserRank"] = Convert.ToString(dpdt.Rows[k]["Designation"]);
                rows["SRContactNo"] = Convert.ToString(dpdt.Rows[k]["SF_Mobile"]);
                rows["WType"] = Convert.ToString(dpdt.Rows[k]["Wtype"]);
                rows["WorkedWithName"] = Convert.ToString(dpdt.Rows[k]["worked_with_name"]);
                rows["SelectedBeats"] = Convert.ToString(dpdt.Rows[k]["ClstrName"]);
                rows["Distributors"] = Convert.ToString(dpdt.Rows[k]["StkName"]);
                rows["DistributorsAddress"] = Convert.ToString(dpdt.Rows[k]["Stockist_Address"]);
                rows["Reason"] = Convert.ToString(dpdt.Rows[k]["remarks"]);
                rows["AC"] = Convert.ToString(dpdt.Rows[k]["rcount"]);
                rows["TC"] = Convert.ToString(dpdt.Rows[k]["TCall"]);
                rows["PC"] = Convert.ToString(dpdt.Rows[k]["EFCall"]);
                rows["Productivity"] = Convert.ToString(dpdt.Rows[k]["productivity"]);
                rows["NewRetailers"] = Convert.ToString(dpdt.Rows[k]["NRC"]);
                rows["NewRetailersPOB"] = Convert.ToString(dpdt.Rows[k]["NRVAL"]);
                rows["Productivity"] = Convert.ToString(dpdt.Rows[k]["productivity"]);
                rows["TelephonicOrders"] = Convert.ToString(dpdt.Rows[k]["phoneOrderCnt"]);
                rows["LoginTime"] = Convert.ToString(dpdt.Rows[k]["Start_Time"]);
                rows["LogOutTime"] = Convert.ToString(dpdt.Rows[k]["End_Time"]);
                rows["TotalTime"] = Convert.ToString(totaltime);
                rows["Lat"] = Convert.ToString(dpdt.Rows[k]["Start_Lat"]);
                rows["Long"] = Convert.ToString(dpdt.Rows[k]["Start_Long"]);
                rows["FirstCall"] = Convert.ToString(dpdt.Rows[k]["minmumTime"]);
                rows["LastCall"] = Convert.ToString(dpdt.Rows[k]["MaximumTime"]);
                rows["TotalRetailTime"] = TotalRTime;
                rows["Value"] = Convert.ToString(dpdt.Rows[k]["orderAmt"]);

                objDS.Rows.Add(rows);
            }
        }

        using (XLWorkbook wb = new XLWorkbook())
        {
            try
            {
                //creating worksheet
                var ws = wb.Worksheets.Add("Report");

                //adding columms header
                int columnscount = objDS.Columns.Count;
                char a = 'A';
                for (int j = 1; j <= columnscount; j++)
                {
                    string str = a + "1";
                    ws.Cell(str).Value = objDS.Columns[j - 1].ColumnName.ToString();
                    ws.Cell(str).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    a++;
                }
                ws.Columns().AdjustToContents();

                //formatting columns header 
                var rngheaders = ws.Range("A1:J1");
                rngheaders.FirstRow().Style
                    .Font.SetBold()
                    .Font.SetFontSize(12)
                    .Font.SetFontColor(XLColor.Black)
                    .Fill.SetBackgroundColor(XLColor.DeepSkyBlue)
                    .Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center)
                    .Border.OutsideBorder = XLBorderStyleValues.Thin;


                ////adding data to excel
                int k = 2;
                foreach (DataRow row in objDS.Rows)
                {
                    char b = 'A';
                    string str = b + "" + k;
                    for (int i = 0; i < objDS.Columns.Count; i++)
                    {
                        ws.Cell(str).Value = row[i].ToString();
                        ws.Cell(str).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        ws.Cell(str).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        b++;
                        str = b + "" + k;
                    }
                    k++;
                }

                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename=FieldForceSummary.xlsx");
            }
            catch { }

            using (MemoryStream MyMemoryStream = new MemoryStream())
            {
                wb.SaveAs(MyMemoryStream);
                MyMemoryStream.WriteTo(Response.OutputStream);
                Response.Flush();
                Response.End();
            }
        }
    }
}