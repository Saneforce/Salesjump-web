//
#region Assembly
using System;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Windows;
using System.IO;
using System.Data;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.xml.simpleparser;
using iTextSharp.text.xml;
using iTextSharp.text.io;
using Bus_EReport;
using DBase_EReport;
using System.Net;
using System.Data;
using System.Data.SqlClient;
using System.Web.Services;
using System.Text;
#endregion
//
//
public partial class MasterFiles_AnalysisReports_Coverage_New : System.Web.UI.Page
{
    //
    #region Variables
    string sMode = string.Empty;
    string sf_code = string.Empty;
    string FMonth = string.Empty;
    string FYear = string.Empty;
    string TMonth = string.Empty;
    string TYear = string.Empty;
    string div_code = string.Empty;
    string sReturn = string.Empty;
    string sDay = string.Empty;
    string sCnt = string.Empty;
    string vMode = string.Empty;
    string strmode = string.Empty;
    string sUsr_Name = string.Empty;
    string strFieledForceName = string.Empty;
    string sCurrentDate = string.Empty;
    DataSet dsFF = new DataSet();
    DataSet dsdoc = new DataSet();
    DataTable dtrowClr = new System.Data.DataTable();
    SalesForce dcrdoc = new SalesForce();
    List<DataTable> result = new List<System.Data.DataTable>();
    List<DataTable> dlyd = new List<System.Data.DataTable>();
    string sDesignation="";
    string sfName;
    int iDelayedTblCnt = 0;
    int iDevCount = 0;
    #endregion
    //
    #region Page Load
    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        sUsr_Name = Session["div_name"].ToString();
        sf_code = Request.QueryString["sfcode"].ToString();
        FMonth = Request.QueryString["FMonth"].ToString();
        FYear = Request.QueryString["FYear"].ToString();
        //strFieledForceName = Request.QueryString["sf_name"].ToString();
        //sDesignation = Request.QueryString["Designation"].ToString();
        SalesForce sf = new SalesForce();
        string strFrmMonth = "";//sf.getMonthName(FMonth);
        lblHead.Text = "Tour Plan - Consolidated View for - (" + strFrmMonth + " - " + FYear + ")";
        LblForceName.Text = "Field Force Name : " + strFieledForceName;
        //
        ViewReports();
    }
    #endregion
    //
    #region ViewReports
    private void ViewReports()
    {
        #region Get Data from Database
        //
        string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["Ereportcon"].ToString();
        SqlConnection con = new SqlConnection(strConn);
        con.Open();
        SqlCommand cmd = new SqlCommand("DCR_Analysis", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Div_Code", div_code);
        cmd.Parameters.AddWithValue("@Msf_code", sf_code);
        cmd.Parameters.AddWithValue("@cMnth", Convert.ToInt32(FMonth));
        cmd.Parameters.AddWithValue("@cYr", Convert.ToInt32(FYear));
        cmd.CommandTimeout = 150;
        //
        string sDate = "";
        if (FMonth=="12")
            sDate = "01-01-" + (Convert.ToInt32(FYear) + 1).ToString();
        else
            sDate = (Convert.ToInt32(FMonth)+1).ToString() + "-01-" + FYear;
        //
        cmd.Parameters.AddWithValue("@cDate", sDate);
        cmd.CommandTimeout = 120;
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        DataSet ds = new DataSet();
        SalesForce sf = new SalesForce();
        da.Fill(ds);
        dt = ds.Tables[0];
        con.Close();
        
        dt.Columns.RemoveAt(12);
        dt.Columns.RemoveAt(11);
        dt.Columns.RemoveAt(7);
        dt.Columns.RemoveAt(6);        
        dt.Columns.RemoveAt(0);
        
        //
        #endregion
        // Spliting DataTable based on Sf_code
        result = dt.AsEnumerable()
            .GroupBy(row => row.Field<string>("main_Sf_Code"))
            .Select(g => g.CopyToDataTable())
            .ToList();
        //
        dt = ds.Tables[1];
        dt.Columns.RemoveAt(0);
        //
        dlyd = dt.AsEnumerable()
            .GroupBy(row => row.Field<string>("main_Sf_Code"))
            .Select(g => g.CopyToDataTable())
            .ToList();
        //
        for (int k = 0; k < result.Count; k++)
        {
            StringBuilder html = new StringBuilder();
            //
            #region Titles
            //
            sfName = result[k].Rows[0][1].ToString();
            string sEmp_Id = result[k].Rows[0][21].ToString();
            string sTtl_Dr_Mt = result[k].Rows[0][22].ToString();
            string sChemist_Mt = result[k].Rows[0][23].ToString();
            string sfName_w_Desig = sfName + " - ( " + result[k].Rows[0][2].ToString() + " )";
            string sClr = result[k].Rows[0][4].ToString();
            html.Append("<table align='left' width='99%' cellspacing='0' style='border-collapse: collapse; margin-left:10px;'>");
            html.Append("<tr style='height:30px;'><td colspan='8'></td></tr>");
            html.Append("<tr><td colspan='8'></td></tr>");
            html.Append("<tr><td colspan='8'></td></tr>");
            html.Append("<tr><td colspan='2' align='left' style='float:left; margin-left:10px;'><b><h4><font color='green'>" + sUsr_Name + " </font></h4></b></td><td colspan='2'></td><td colspan='4' align='right' style='float:right; margin-right:10px;'><i><h6><b> Date : (" + System.DateTime.Now.ToString() + ")</b></h6></i></td></tr>");
            html.Append("<tr><td colspan='8' align='center'><u><b><h3> DCR Analysis For the Month of :  " + sf.getMonthName(FMonth).ToString() + " - " + FYear + " </h3></b></u></td></tr>");
            html.Append("<tr><td colspan='2' bgcolor='#" + sClr + "' style='float:left; margin-left:15px;' align='left'> FieldForce Name : <b>" + sfName_w_Desig + "</b></td><td colspan='2' bgcolor='#" + sClr + "' align='center'><b> Emp Code :  " + sEmp_Id + " </b></td><td colspan='5' bgcolor='#" + sClr + "' style='float:right; margin-right:20px;' align='right'> HQ Name : <b>" + result[k].Rows[0][3].ToString() + "</b></td></tr></table>");
            
            pnlTbl.Controls.Add(new Literal { Text = html.ToString() });
            html = new StringBuilder();            
            html.Append("<table align='left' width='99%' cellspacing='0' border='1' style='border-collapse: collapse; margin-left:10px;'>");
            //
            #endregion
            //
            string sTtl_Drs = "-";
            for (int i = 0; i < 3; i++)
            {
                html.Append("<tr>");
                if (i == 0)
                {
                    iDevCount = 0;                 
                    html.Append("<td align='center' colspan=8 valign='top'>");
                    pnlTbl.Controls.Add(new Literal { Text = html.ToString() });
                    html = new StringBuilder();
                    //
                    #region Gridview Main
                    GridView gv = new GridView();                        
                    gv.Attributes.Add("width", "99.8%");
                    gv.HeaderStyle.BackColor = System.Drawing.Color.SkyBlue;
                    gv.EmptyDataText = "*** No Data Found ***";
                    gv.EmptyDataRowStyle.BackColor = System.Drawing.Color.MidnightBlue;
                    gv.EmptyDataRowStyle.HorizontalAlign = HorizontalAlign.Center;
                    gv.EmptyDataRowStyle.ForeColor = System.Drawing.Color.White;
                    gv.EmptyDataRowStyle.Font.Bold = true;
                    gv.EmptyDataRowStyle.Font.Size = 12;
                    gv.ShowHeader = false;

                    result[k].Columns.RemoveAt(23);
                    result[k].Columns.RemoveAt(22);
                    result[k].Columns.RemoveAt(21);
                    result[k].Columns.RemoveAt(4);
                    result[k].Columns.RemoveAt(3);
                    result[k].Columns.RemoveAt(2);
                    result[k].Columns.RemoveAt(1);
                    result[k].Columns.RemoveAt(0);
                    
                    sTtl_Drs=result[k].Rows[0][10].ToString();
                    //
                    if (result[k].Rows[0][0].ToString() == "")
                        result[k].Rows.RemoveAt(0);    
                    gv.DataSource = result[k];
                    //
                    gv.RowCreated += new GridViewRowEventHandler(this.grdMain_RowCreated);
                    gv.RowDataBound += new GridViewRowEventHandler(this.grdMain_RowDataBound);
                    gv.DataBind();                    
                    
                    pnlTbl.Controls.Add(gv);
                    #endregion
                    //
                    html.Append("</td>");
                    html.Append("</tr>");
                    pnlTbl.Controls.Add(new Literal { Text = html.ToString() });
                    html = new StringBuilder();
                }
                else if (i == 1 && dlyd.Count>=k)
                {
                    html.Append("<td colspan=8 align='center' bgcolor='#B0C4DE'>DCR Delayed Staus</td><tr><td align='center' colspan=8 valign='top' nowrap>");
                    pnlTbl.Controls.Add(new Literal { Text = html.ToString() });
                    html = new StringBuilder();
                    //
                    #region Gridview DelayedDate
                    GridView gv = new GridView();
                    gv.Attributes.Add("width", "99.8%");
                    gv.ShowHeader = false;                    
                    if (dlyd[k].Rows[0][1].ToString() == null || dlyd[k].Rows[0][1].ToString() == "&nbsp;" || dlyd[k].Rows[0][1].ToString() == "")
                    {
                        DataTable dts = new DataTable();
                        dts.Columns.Add("Name");
                        dts.Columns.Add("Sample");
                        dts.Rows.Add("Locked Date", "Nil");
                        dts.Rows.Add("Released Date", "Nil");
                        gv.ShowHeader = false;
                        gv.DataSource = dts;
                    }
                    else
                    {
                        var delayed = from a in dlyd[k].AsEnumerable()                                   
                                   select new
                                   {
                                       subject=a.Field<string>("subject"),
                                       dates = a.Field<string>("dates").Replace("[", "").Replace("]", "")                                       
                                   };
                        DataTable dttbldlyd = new System.Data.DataTable();
                        dttbldlyd.Columns.Add("dlyed");
                        dttbldlyd.Columns.Add("dt");
                        
                        foreach (var item in delayed)
                        {
                            List<int> iDt = new List<int>();
                            string sdt = "";
                            string[] dtt= item.dates.ToString().Split(',');
                            foreach (string sdts in dtt)
                            {
                                if (sdts.TrimStart()!="")
                                    iDt.Add(Convert.ToInt32(sdts.TrimStart()));
                            }
                            iDt.Sort();
                            foreach (var newval in iDt)
	                        {
                                sdt += newval + ", ";
	                        }
                            dttbldlyd.Rows.Add(item.subject, sdt.Remove(sdt.LastIndexOf(",")));
                        }
                        gv.DataSource = dttbldlyd;
                    }
                    gv.DataBind();                                        
                    pnlTbl.Controls.Add(gv);
                    #endregion                    
                    //
                    html.Append("</td></tr>");
                }
                else
                {
                    for (int j = 0; j < 3; j++)
                    {
                        //
                        #region Gridview WorkType
                        GridView gv = new GridView();
                        gv.Attributes.Add("width", "99.8%");
                        DataTable dts = new DataTable();
                        if (j == 0)
                        {
                            html.Append("<td align='center' valign='top' nowrap>");
                            pnlTbl.Controls.Add(new Literal { Text = html.ToString() });
                            html = new StringBuilder();
                            
                            var groupedData = from b in result[k].AsEnumerable()
                                              where b.Field<string>("WorkType")!=null
                                              group b by b.Field<string>("WorkType") into g
                                              select new
                                              {
                                                  WorkType = g.Key,
                                                  Count = g.Count()
                                              };
                            //
                            DataTable dtAttendance = new System.Data.DataTable();
                            dtAttendance.Columns.Add("Attendance Details");
                            dtAttendance.Columns.Add("Total");                            
                            int iTtl = 0;
                            foreach (var item in groupedData)
                            {
                                dtAttendance.Rows.Add(item.WorkType, item.Count);
                                iTtl += Convert.ToInt32(item.Count);
                            }
                            if (iTtl != 0)
                                dtAttendance.Rows.Add("Total No of Days", iTtl);
                            gv.HeaderStyle.BackColor = System.Drawing.Color.Thistle;
                            gv.EmptyDataText = "*** No Data ***";
                            gv.EmptyDataRowStyle.Font.Bold = true;
                            gv.EmptyDataRowStyle.Font.Size = 12;
                            gv.EmptyDataRowStyle.ForeColor = System.Drawing.Color.Red;
                            gv.EmptyDataRowStyle.HorizontalAlign = HorizontalAlign.Center;
                            gv.EmptyDataRowStyle.BackColor = System.Drawing.Color.Thistle;
                            gv.DataSource = dtAttendance;
                            //gv.RowDataBound += new GridViewRowEventHandler(this.grdAttendance_RowDataBound);
                        }
                        //
                        #endregion
                        //
                        #region GridView Chemist Details
                        //
                        /*else if (j == 1)
                        {
                            html.Append("<td align='center' valign='top' nowrap>");
                            pnlTbl.Controls.Add(new Literal { Text = html.ToString() });
                            html = new StringBuilder();
                            
                            gv.HeaderStyle.BackColor = System.Drawing.Color.LightSalmon;                            
                            //
                            var ttlChmst = from p in result[k].AsEnumerable()
                                      group p by "Total" into g
                                      select new
                                      {
                                          Category = g.Key,
                                          Chemist_Met = g.Sum(x => x.IsNull("Chm_Mt") ? 0 : x.Field<Int32>("Chm_Mt")),
                                          No_of_Chemist_POB = g.Count(x => x.Field<Double?>("Chm POB") > 0),
                                          Chemist_POB_Value = g.Sum(x => x.IsNull("Chm POB") ? 0 : x.Field<Double>("Chm POB")),
                                          Chemist_Call_Average = Decimal.Divide(g.Sum(x => x.IsNull("Chm_Mt") ? 0 : x.Field<Int32>("Chm_Mt")),
                                          (g.Count(x => x.Field<string>("WorkType") == "Field Work")) == 0 ? -1 : g.Count(x => x.Field<string>("WorkType") == "Field Work")).ToString("#.##")
                                      };
                            //
                            DataTable dttbls = new System.Data.DataTable();
                            dttbls.Columns.Add("Chemist Details");
                            dttbls.Columns.Add("Total");
                            //
                            foreach (var item in ttlChmst)
                            {
                                dttbls.Rows.Add("Chemist Met", item.Chemist_Met);
                                dttbls.Rows.Add("No of Chemist POB", item.No_of_Chemist_POB);
                                dttbls.Rows.Add("Chemist POB Value", item.Chemist_POB_Value);
                                string val = "-";
                                if (item.Chemist_Call_Average != "")
                                    val = item.Chemist_Call_Average;
                                    
                                dttbls.Rows.Add("Chemist Call Average", val);
                            }                            
                            gv.DataSource = dttbls;
                        }*/
                        //
                        #endregion
                        //
                        #region GridView Consolidate View
                        //
                        else if (j == 1)
                        {
                            html.Append("<td align='center' valign='top' nowrap>");
                            pnlTbl.Controls.Add(new Literal { Text = html.ToString() });
                            html = new StringBuilder();
                            gv.EmptyDataText = "*** No Data ***";
                            gv.EmptyDataRowStyle.Font.Bold = true;
                            gv.EmptyDataRowStyle.Font.Size = 12;
                            gv.EmptyDataRowStyle.ForeColor = System.Drawing.Color.Red;
                            gv.EmptyDataRowStyle.HorizontalAlign = HorizontalAlign.Center;
                            gv.EmptyDataRowStyle.BackColor = System.Drawing.Color.PaleGreen;
                            if (sTtl_Drs == "")
                                sTtl_Drs = "0";
                            if (sTtl_Dr_Mt == "")
                                sTtl_Dr_Mt = "0";
                            gv.HeaderStyle.BackColor = System.Drawing.Color.PaleGreen;
                            var ttlConsolidateView = from p in result[k].AsEnumerable()
                                                     group p by "Total" into g
                                                     select new
                                                     {
                                                         ConsolidateView = g.Key,
                                                         Ttl_Dr = sTtl_Drs,
                                                         Dr_Mt = sTtl_Dr_Mt,
                                                         Dr_Sn = g.Sum(x => x.IsNull("dr Sn") ? 0 : x.Field<Int32>("dr Sn")),
                                                         NL_Mt = g.Sum(x => x.IsNull("Ul Mt") ? 0 : x.Field<Int32>("Ul Mt")),
                                                         Tp_Dev = iDevCount,
                                                         No_Joint_Wrk = g.Count(x => x.Field<Int32?>("wrkd with cnt") != 0 && x.Field<Int32?>("wrkd with cnt") != null),
                                                         Joint_Wrk_Ttl = g.Sum(x => x.IsNull("wrkd with cnt") ? 0 : x.Field<Int32>("wrkd with cnt")),
                                                         No_Of_FldWrk_Dys = g.Count(x => x.Field<string>("WorkType") == "Field Work") == 0 ? 0 : g.Count(x => x.Field<string>("WorkType") == "Field Work"),
                                                         Chemist_Met = sChemist_Mt,
                                                         Chemist_Sn = g.Sum(x => x.IsNull("Chm_Mt") ? 0 : x.Field<Int32>("Chm_Mt")),
                                                         Chemist_POB_Value = g.Sum(x => x.IsNull("Chm POB") ? 0 : x.Field<Double>("Chm POB"))
                                                     };
                            //
                            DataTable dtconsl = new System.Data.DataTable();
                            dtconsl.Columns.Add("Calls Details");
                            dtconsl.Columns.Add("Total");
                            foreach (var item in ttlConsolidateView)
                            {
                                dtconsl.Rows.Add("Total No Of Doctors", item.Ttl_Dr);
                                dtconsl.Rows.Add("Total No Of Doctors Met", item.Dr_Mt);
                                dtconsl.Rows.Add("Total Calls Seen", item.Dr_Sn);
                                dtconsl.Rows.Add("No of N.L Drs Met", item.NL_Mt);
                                string val = "-";
                                if (item.Ttl_Dr != "0")
                                    val = (Decimal.Divide(Convert.ToInt32(item.Dr_Mt), Convert.ToInt32(item.Ttl_Dr)) * 100).ToString("#.##");
                                dtconsl.Rows.Add("Coverage", val);

                                if (item.No_Of_FldWrk_Dys != 0)
                                    val = (item.Dr_Sn / item.No_Of_FldWrk_Dys).ToString("#.##");
                                dtconsl.Rows.Add("Call Average", val);
                                dtconsl.Rows.Add("No of TP Deviation", item.Tp_Dev);
                                dtconsl.Rows.Add("No of Joint Work Days", item.No_Joint_Wrk);
                                if (item.No_Of_FldWrk_Dys != 0)
                                    val = (item.Joint_Wrk_Ttl/item.No_Of_FldWrk_Dys).ToString("#.##");
                                dtconsl.Rows.Add("Joint Work Call Avg", val);
                                dtconsl.Rows.Add("Chemist POB Value", item.Chemist_POB_Value);
                                dtconsl.Rows.Add("Chemist Met", item.Chemist_Met);
                                dtconsl.Rows.Add("Chemist Seen", item.Chemist_Sn);
                                if (item.No_Of_FldWrk_Dys != 0)
                                    val = (item.Chemist_Sn / item.No_Of_FldWrk_Dys).ToString("#.##");
                                dtconsl.Rows.Add("Chemist Call Avg", val);
                            }
                            gv.DataSource = dtconsl;                            
                        }
                        //
                        #endregion
                        //
                        #region GridView Worked With
                        //
                        else
                        {
                            html.Append("<td align='center' valign='top'>");
                            pnlTbl.Controls.Add(new Literal { Text = html.ToString() });
                            html = new StringBuilder();

                            //calculation based on Worked With Name
                            var workedWithData = from b in result[k].AsEnumerable()                                                 
                                              group b by b.Field<string>("wrkd with name") into g
                                             select new
                                             {
                                                 WorkedWithName = g.Key,
                                                 DaysWorked = g.Count(),
                                                 CallsSeen=g.Sum(x=>x.Field<Int32?>("wrkd with cnt"))
                                             };
                            // Remove Additional Symbols in Worked With Name
                            var wrkw = from a in workedWithData
                                       where a.WorkedWithName != null && a.WorkedWithName.Replace("[", "").Replace(",]", "").Replace(", ]", "").Trim() != sfName
                                       select new
                                       {
                                           Joint_Work_Details = a.WorkedWithName.Replace("[", "").Replace(",]", "").Replace(", ]", "").Trim()
                                           .Replace(sfName, "Self").Replace("Self,", ",").Replace("Self ,", ",").Replace(",Self", ",").Replace(", Self", ",").Replace(",,", ",").Replace(", ,", ",").TrimStart().TrimEnd().Trim(),
                                           a.DaysWorked,
                                           a.CallsSeen
                                       };
                            DataTable dtJointWork = new System.Data.DataTable();
                            dtJointWork.Columns.Add("Joint Work Details");
                            dtJointWork.Columns.Add("No of Days");
                            dtJointWork.Columns.Add("No of Calls");
                            var value=0;
                            foreach (var item in wrkw)
                            {
                                var jName = item.Joint_Work_Details;
                                if (jName.Substring(0, 1) == ",")
                                    jName = jName.Remove(0, 1);
                                try
                                {
                                    if (jName.Substring(jName.Length - 1, 1) == ",")
                                        jName = jName.Remove(jName.Length - 1, 1);
                                }
                                catch (Exception ex)
                                { }
                                //
                                dtJointWork.Rows.Add(jName, item.DaysWorked, item.CallsSeen);
                            }
                            //dtJointWork.Rows.Add("SELF (Including Joint Work)", value);
                            //Empty Text
                            gv.EmptyDataText = "*** No Joint Work ***";
                            gv.EmptyDataRowStyle.Font.Bold = true;
                            gv.EmptyDataRowStyle.Font.Size = 12;
                            gv.EmptyDataRowStyle.ForeColor = System.Drawing.Color.Red;
                            gv.EmptyDataRowStyle.HorizontalAlign = HorizontalAlign.Center;
                            gv.EmptyDataRowStyle.BackColor = System.Drawing.Color.PaleGoldenrod;
                            gv.HeaderStyle.BackColor = System.Drawing.Color.PaleGoldenrod;
                            //
                            gv.DataSource = dtJointWork;  
                        }
                        //
                        #endregion
                        //
                        gv.DataBind();
                        pnlTbl.Controls.Add(gv);
                                             
                        html.Append("</td>");
                    }
                }
                html.Append("</tr>");
            }
            html.Append("</table><br>");
            pnlTbl.Controls.Add(new Literal { Text = html.ToString() });
            iDelayedTblCnt++;
        }
    }
    #endregion
    //
    #region GridMain
    private void grdMain_RowDataBound(object sender, GridViewRowEventArgs e)
    {        
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[10].Visible = false;
            e.Row.Cells[11].Visible = false;
        }
        else if (e.Row.RowType==DataControlRowType.DataRow)
        {
            for (int r = 0; r < e.Row.Cells.Count; r++)
            {
                if (r == 0)
                {
                    string sDlydDate = dlyd[iDelayedTblCnt].Rows[0][2].ToString().Replace("[", "").Replace("]", "").Trim();
                    if (sDlydDate != "")
                    {
                        string[] dates = sDlydDate.Split(',');
                        foreach (string date in dates)
                        {
                            if (date.TrimStart() == e.Row.Cells[0].Text && date != "")
                            {
                                e.Row.Cells[2].Text = e.Row.Cells[2].Text + " (delay)";
                                e.Row.Cells[2].Attributes.Add("style", "color:red;");                                
                                break;
                            }
                        }
                    }
                }
                else if (r == 10 || r == 11)
                {
                    e.Row.Cells[r].Visible = false;
                    /*
                    HyperLink hyLnk = new HyperLink();
                    hyLnk.Text = e.Row.Cells[r].Text;
                    hyLnk.NavigateUrl = "#";
                    e.Row.Cells[r].Controls.Add(hyLnk);*/
                }
                else if (r == 3 || r == 6)
                {
                    e.Row.Cells[r].Text = e.Row.Cells[r].Text.Replace("[", "").Replace(",]", "").Replace(", ]", "").Replace("]", "");
                    e.Row.Cells[r].Text = e.Row.Cells[r].Text.Replace(sfName, "Self");
                    e.Row.Cells[r].Text = e.Row.Cells[r].Text.Replace(",Self", ",").Replace(", Self", ",").Replace("Self,", ",").Replace(",,", ",");
                    
                    if (e.Row.Cells[r].Text.Substring(0, 1) == ",")
                        e.Row.Cells[r].Text = e.Row.Cells[r].Text.Remove(0, 1);
                    if (e.Row.Cells[r].Text.Substring(e.Row.Cells[r].Text.Length - 1, 1) == ",")
                        e.Row.Cells[r].Text = e.Row.Cells[r].Text.Remove(e.Row.Cells[r].Text.Length - 1, 1);

                    if (r == 6)
                    {
                        if (e.Row.Cells[r - 1].Text.Replace("&nbsp;", "") != "" || e.Row.Cells[r].Text.Replace("&nbsp;", "") != "")
                        {
                            string[] sPlan = e.Row.Cells[r - 1].Text.Replace("&nbsp;", "").Split(',');
                            if (sPlan.Length > 0 && sPlan[0] != "")
                            {
                                e.Row.Cells[r + 1].Text = "*";
                                iDevCount++;
                                foreach (string plan in sPlan)
                                {
                                    if (e.Row.Cells[r].Text.Replace("&nbsp;", "").Contains(plan))
                                    {
                                        e.Row.Cells[r + 1].Text = "";
                                        iDevCount--;
                                        break;
                                    }
                                }
                            }
                            else
                            {
                                e.Row.Cells[r + 1].Text = "*";
                                iDevCount++;
                            }
                        }
                    }
                }
                else if (r == 2 && e.Row.Cells[r].Text.ToLower().Trim() == "weekly off" || e.Row.Cells[r].Text.ToLower().Trim().Contains("leave")
                    || e.Row.Cells[r].Text.ToLower().Trim().Contains("holiday") )
                {
                    e.Row.Attributes.Add("style", "background-color:#FFEAEA;");
                }

                if (e.Row.Cells[r].Text == "0")
                {
                    e.Row.Cells[r].Text = "";
                }
            }
        }
    }
    #endregion
    //
    #region grdMain_RowCreated
    private void grdMain_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            //Creating a gridview object            
            GridView objGridView = (GridView)sender;

            //Creating a gridview row object
            GridViewRow objgridviewrow = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);
            //Creating a table cell object
            TableCell objtablecell = new TableCell();

            AddMergedCells(objgridviewrow, objtablecell, "Date", false, 0);
            AddMergedCells(objgridviewrow, objtablecell, "Submitted Date", true, 0);
            AddMergedCells(objgridviewrow, objtablecell, "Work Type", false, 0);
            AddMergedCells(objgridviewrow, objtablecell, "Worked With", false, 0);
            AddMergedCells(objgridviewrow, objtablecell, "Joint Calls", true, 0);
            AddMergedCells(objgridviewrow, objtablecell, "As Per TP", false, 0);
            AddMergedCells(objgridviewrow, objtablecell, "Worked", false, 0);
            AddMergedCells(objgridviewrow, objtablecell, "Dev", false, 0);
            AddMergedCells(objgridviewrow, objtablecell, "Listed Dr Met", true, 0);
            AddMergedCells(objgridviewrow, objtablecell, "Drs POB", true, 0);
            AddMergedCells(objgridviewrow, objtablecell, "Unlist Dr Met", true, 0);
            AddMergedCells(objgridviewrow, objtablecell, "Chemist Met", true, 0);
            AddMergedCells(objgridviewrow, objtablecell, "Chemist POB", true, 0);
            AddMergedCells(objgridviewrow, objtablecell, "Stockist Met", true, 0);
            objGridView.Controls[0].Controls.AddAt(0, objgridviewrow);
        }
    }
    #endregion
    //
    #region AddMergedCells
    protected void AddMergedCells(GridViewRow objgridviewrow, TableCell objtablecell, string celltext, bool wrap, int iVal)
    {
        objtablecell = new TableCell();
        objtablecell.Text = celltext;
        if (iVal == 0)
        {
            objtablecell.Style.Add("background-color", "#191970");
            objtablecell.ForeColor = System.Drawing.Color.White;
        }
        else
        {
            objtablecell.Style.Add("background-color", "#EEE8AA");
            objtablecell.ForeColor = System.Drawing.Color.Black;
        }
        objtablecell.Font.Bold = true;
        objtablecell.HorizontalAlign = HorizontalAlign.Center;
        objtablecell.VerticalAlign = VerticalAlign.Middle;
        objtablecell.Wrap = wrap;
        objtablecell.Width = 25;
        objgridviewrow.Cells.Add(objtablecell);
    }
    #endregion
    // 
    #region grdJoint_RowCreated
    private void grdJoint_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            //Creating a gridview object            
            GridView objGridView = (GridView)sender;

            //Creating a gridview row object
            GridViewRow objgridviewrow = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);
            //Creating a table cell object
            TableCell objtablecell = new TableCell();

            AddMergedCells(objgridviewrow, objtablecell, "", true, 1);
            AddMergedCells(objgridviewrow, objtablecell, "", true, 1);
            AddMergedCells(objgridviewrow, objtablecell, "", true, 1);
            objGridView.Controls[0].Controls.AddAt(0, objgridviewrow);
        }
    }
    #endregion
    //
    #region GridWorkedWithName
    private void grdWorkedWith_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        string txt = "";
        if (e.Row.RowType == DataControlRowType.DataRow)
        {       
            if (e.Row.Cells[0].Text=="&nbsp;")
            {                
                
            }
        }
    }
    #endregion
    //
}