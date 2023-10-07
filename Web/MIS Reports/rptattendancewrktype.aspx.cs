using Bus_EReport;
using DBase_EReport;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class MIS_Reports_rptattendancewrktype : System.Web.UI.Page
{
    #region declaration
    public static string sfCode = string.Empty;
    public static string sfname = string.Empty;
    public static string divcode = string.Empty;
    public static string sf_type = string.Empty;
    public static string FMonth = string.Empty;
    public string FYear = string.Empty;
    public static string type = string.Empty;
    public static string h = string.Empty;
    public static string wrktypename = string.Empty;
    public static int sum_time = 0;
    DataSet dsSalesForce = new DataSet();
    DataSet dsdatee = new DataSet();
    DataSet dsDoc = null;
    DateTime dtCurrent;
    TimeSpan ff;
    public static int rowspan = 0;
    public static string sCurrentDate = string.Empty;
    public static string endTime = string.Empty;
    public static string startTime = string.Empty;
    public static string tot_dr = string.Empty;
    public static string tot_value = string.Empty;
    public static string con_qty = string.Empty;
    public static string ec = string.Empty;
    public static string Monthsub = string.Empty;
    public static string date = string.Empty;
    public static string endd = string.Empty;
    DataSet dsDoctor = new DataSet();
    public static string gg = string.Empty;
    public static string imagepath = string.Empty;
    public static int quantity2 = 0;
    public static string mode = string.Empty;
    public static string subdiv_code = string.Empty;
    public string strFMonthName = string.Empty;
    public static string statec = string.Empty;
    public static string statev = string.Empty;
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        divcode = Session["div_code"].ToString();
        sfname = Request.QueryString["sfname"].ToString();
        sfCode = Request.QueryString["sfcode"].ToString();
        imagepath = Request.QueryString["imgpath"].ToString();
        subdiv_code = Request.QueryString["subdiv"].ToString();
        statec = Request.QueryString["stcode"];
        statev = Request.QueryString["stval"];
        logoo.ImageUrl = imagepath;

        if (sfCode.Contains("MGR"))
        { sf_type = "2"; }
        else if (sfCode.Contains("MR"))
        { sf_type = "1"; }
        else
        { sf_type = "0"; }

        FMonth = Request.QueryString["FMonth"].ToString();
        FYear = Request.QueryString["FYear"].ToString();
        ddlFieldForce.Value = sfCode;
        ddlFYear.Value = FYear;
        ddlFMonth.Value = FMonth;
        SubDivCode.Value = subdiv_code;
        System.Globalization.DateTimeFormatInfo mfi = new System.Globalization.DateTimeFormatInfo();
        strFMonthName = mfi.GetMonthName(Convert.ToInt16(FMonth)).ToString().Substring(0, 3);
        lblHead.Text = "Attendance WorkTypewise View for the " + strFMonthName + " " + FYear + "-" + statev;

        lblsf_name.Text = sfname;

        Fillworktypeview();
    }

    protected void gridView_PreRender(object sender, EventArgs e)
    {
        GridDecorator.MergeRows(gvtotalorder);
    }

    public class GridDecorator
    {
        DataSet dsGV = new DataSet();
        public static void MergeRows(GridView gridView)
        {
            for (int rowIndex = gridView.Rows.Count - 2; rowIndex >= 0; rowIndex--)
            {
                GridViewRow row = gridView.Rows[rowIndex];
                GridViewRow previousRow = gridView.Rows[rowIndex + 1];

                // for (int i = 0; i < row.Cells.Count - 32; i++)
                // {
                if (row.Cells[0].Text == previousRow.Cells[0].Text)
                {
                    DataSet dsGV = new DataSet();
                    DCR dc = new DCR();
                    row.Cells[0].RowSpan = previousRow.Cells[0].RowSpan < 2 ? 2 :
                                           previousRow.Cells[0].RowSpan + 1;

                    previousRow.Cells[0].Visible = false;
                    row.Cells[1].RowSpan = previousRow.Cells[0].RowSpan < 2 ? 2 :
                                           previousRow.Cells[0].RowSpan + 1;

                    previousRow.Cells[1].Visible = false;
                    row.Cells[2].RowSpan = previousRow.Cells[0].RowSpan < 2 ? 2 :
                                           previousRow.Cells[0].RowSpan + 1;

                    previousRow.Cells[2].Visible = false;
                }
                // }
            }
        }
    }

    private void Fillworktypeview()
    {

        DataSet dsGV = new DataSet();
        DataSet dsGc = new DataSet();
        test dc = new test();

        //mode = "Minimised";

        dsGV = dc.attendance_view_wortypewise(sfCode, divcode, FMonth, FYear, subdiv_code, statec);

        if (dsGV.Tables.Count > 0)
        {
            gvtotalorder.DataSource = dsGV.Tables[0];
            gvtotalorder.DataBind();
        }
        else
        {
            gvtotalorder.DataSource = null;
            gvtotalorder.DataBind();
        }
        //if (dsGV.Tables[0].Rows.Count > 0)
        //{
        //    //dsGV.Tables[0].Columns.Remove("sf_type");
        //    //dsGV.Tables[0].Columns.Remove("sf_code"); 

        //    gvtotalorder.DataSource = dsGV;
        //    gvtotalorder.DataBind();
        //}
        //else
        //{
        //    gvtotalorder.DataSource = null;
        //    gvtotalorder.DataBind();
        //}
    }

    protected void PrintSundays(int year, int month, DayOfWeek dayName)
    { }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            int wd = 0;
            int lcnt = 0;
            int oworktype = 0;
            int holidayct = 0;
            int weeklyoffct = 0;
            int retailingct = 0;
            int absntcnt = 0;
            int suncnt = 0;
            int missdt = 0;
            
            test tt = new test();

            DataSet jd = tt.sfjoining_date(divcode, sfCode);
            DataSet dc = tt.misseddates(divcode, sfCode, FMonth, FYear);

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int colIndex = Convert.ToInt32(e.Row.Cells.Count);
                e.Row.Cells[2].Width = 325;
                e.Row.Cells[2].Style.Add("white-space", "nowrap");
                Label txtNamewd = new Label();
                txtNamewd.Width = 20;
                Label leavecnt = new Label();
                leavecnt.Width = 20;
                Label otherwrk = new Label();
                otherwrk.Width = 20;
                Label holiday = new Label();
                holiday.Width = 20;
                Label weeklyoff = new Label();
                weeklyoff.Width = 20;
                Label retailingwrk = new Label();
                retailingwrk.Width = 20;
                Label lblabsent = new Label();
                lblabsent.Width = 20;

                for (colIndex = 22; colIndex < e.Row.Cells.Count; colIndex++)
                {
					int tst = 0;
                    int rowIndex = colIndex;
                    Label txtName = new Label();
                    txtName.Width = 20;
                    txtName.ID = "txtboxname" + colIndex;
                    txtName.Text = e.Row.Cells[colIndex].Text;

                    Label txtName1 = new Label();
                    txtName1.Width = 20;

                    DateTime dateValue1 = new DateTime(Convert.ToInt16(FYear), Convert.ToInt16(FMonth), (colIndex - 21));



                    if (dateValue1.DayOfWeek == System.DayOfWeek.Sunday)
                    {
                        suncnt += 1;
                    }
                    if (txtName.Text == "&nbsp;")
                    {

                        txtName1.Style.Add("Color", "Red");

                        int day = Convert.ToInt32(DateTime.Now.ToString("dd"));
                        int mon = Convert.ToInt32(DateTime.Now.ToString("MM"));

                        DateTime dateValue = new DateTime(Convert.ToInt16(FYear), Convert.ToInt16(FMonth), (colIndex - 21));
                        for (int i = 0; i < dc.Tables[0].Rows.Count; i++)
                        {
                            if (dateValue.Date == Convert.ToDateTime(dc.Tables[0].Rows[i].ItemArray[0]))
                            {
                                txtName1.Text = "MD";
                                txtName1.Style.Add("font-size", "10px");
                                txtName1.Style.Add("font", "Bold");
                                missdt += 1;
                                break;
                            }

                        }
                        if (dateValue.DayOfWeek == System.DayOfWeek.Sunday)
                        {
                            txtName1.Text = "S";
                            txtName1.Style.Add("font-size", "10px");
                            txtName1.Style.Add("font", "Bold");
                        }
                        else if (dateValue.Date < Convert.ToDateTime(jd.Tables[0].Rows[0].ItemArray[0]))
                        {
                            txtName1.Text = " ";
                        }
                        else if (Convert.ToDateTime(dateValue.Date.ToString()) < Convert.ToDateTime(DateTime.Now.ToString("dd/M/yyyy")))
                        {
                            txtName1.Text = "MD";
                            txtName1.Style.Add("font-size", "10px");
                            txtName1.Style.Add("font", "Bold");
                            missdt += 1;
                        }
                        else if (Convert.ToInt16(FMonth) == (mon))
                        {
                            if (txtName1.Text != "MD")
                            {
                                if (colIndex - 21 <= day)
                                {
                                    txtName1.Text = "A";
                                    txtName1.Style.Add("Color", "Red");
                                    txtName1.Style.Add("font-size", "10px");
                                    txtName1.Style.Add("font", "Bold");
                                    absntcnt += 1;
                                }
                            }

                        }
                        else if (Convert.ToInt16(FMonth) > (mon))
                        {

                        }
                        else
                        {
                            if (txtName1.Text != "MD")
                            {
                                txtName1.Text = "A";
                                txtName1.Style.Add("font-size", "10px");
                                txtName1.Style.Add("font", "Bold");
                                absntcnt += 1;
                            }

                        }
                    }
                    else
                    {

                        txtName1.Style.Add("font-size", "10px");
                        txtName1.Style.Add("font", "Bold");
                        //if (colIndex != e.Row.Cells.Count - 2 && colIndex != e.Row.Cells.Count - 8)
                        //{
                        DateTime dateValue = new DateTime(Convert.ToInt16(FYear), Convert.ToInt16(FMonth), (colIndex - 21));
                        //DateTime dateValue = new DateTime(Convert.ToInt16(FYear), Convert.ToInt16(FMonth), (colIndex - 19));
                        string empcode = e.Row.Cells[3].Text;
                        DataSet dcc = tt.callsubdates(empcode, FMonth, FYear);

                        wd += 1;
                        for (int i = 0; i < dcc.Tables[0].Rows.Count; i++)
                        {
                            if (dateValue.Date == Convert.ToDateTime(dcc.Tables[0].Rows[i].ItemArray[0]))
                            {
                                txtName1.Style.Add("background-color", "orange");
                                txtName1.Style.Add("font-size", "10px");
                                txtName1.Style.Add("font", "Bold");
                               tst = 1;
                                break;
                            }
                        }
                        if (tst == 0)
                        {
                            if (txtName.Text == "L" || (txtName.Text).Contains("L")==true)
                            {
                                lcnt += 1;
                                txtName1.Style.Add("Background-color", "yellow");
                                txtName1.Style.Add("font-size", "10px");
                                txtName1.Style.Add("font", "Bold");
                            }
                            else if (txtName.Text == "H")
                            {
                                holidayct += 1;
                                txtName1.Style.Add("Background-color", "yellow");
                                txtName1.Style.Add("font-size", "10px");
                                txtName1.Style.Add("font", "Bold");

                            }
                            else if (txtName.Text == "WO")
                            {
                                weeklyoffct += 1;
                                txtName1.Style.Add("Background-color", "yellow");
                                txtName1.Style.Add("font-size", "10px");
                                txtName1.Style.Add("font", "Bold");
                            }
							else if ((txtName.Text != "L" || (txtName.Text).Contains("L")!=true) && txtName.Text != "FW" && txtName.Text != "WO" && txtName.Text != "H")
                            {
                                oworktype += 1;
                                txtName1.Style.Add("Background-color", "Lightgreen");
                                txtName1.Style.Add("font-size", "10px");
                                txtName1.Style.Add("font", "Bold");
                            }
                        }
                        txtName1.Text = txtName.Text;

                    }


                    string pp = (wd - (lcnt + holidayct + weeklyoffct)).ToString();

                    txtNamewd.Text = pp.ToString();

                    //txtName.AutoPostBack = true;  
                    //e.Row.Cells[colIndex].Controls.Add(txtName);
                    leavecnt.Text = lcnt.ToString();
                    otherwrk.Text = oworktype.ToString();
                    holiday.Text = holidayct.ToString();
                    weeklyoff.Text = weeklyoffct.ToString();
                    retailingct = wd - (oworktype + lcnt + holidayct + weeklyoffct);
                    retailingwrk.Text = retailingct.ToString();
                    lblabsent.Text = absntcnt.ToString();
                    int days = DateTime.DaysInMonth(Convert.ToInt16(FYear), Convert.ToInt16(FMonth));
                    days = days + 1;

                    e.Row.Cells[12].Controls.Add(txtNamewd);
                    e.Row.Cells[13].Controls.Add(retailingwrk);
                    e.Row.Cells[14].Controls.Add(otherwrk);
                    e.Row.Cells[15].Controls.Add(leavecnt);
                    e.Row.Cells[16].Controls.Add(holiday);
                    e.Row.Cells[17].Controls.Add(lblabsent);
                    e.Row.Cells[18].Controls.Add(weeklyoff);

                    txtName1.ID = "txtboxname1" + colIndex;

                    e.Row.Cells[colIndex].Controls.Add(txtName1);
                }
            }
        }

        catch (Exception ex)
        {
        }
    }

    [WebMethod(EnableSession = true)]
    public static GetDatasD[] GetDataD(string SF_Code, string FYera, string FMonth)
    {
        string div_code = "";
        string sf_Code = "";
        try
        { div_code = HttpContext.Current.Session["div_code"].ToString(); }
        catch
        { div_code = HttpContext.Current.Session["Division_Code"].ToString(); }

        sf_Code = HttpContext.Current.Session["SF_Code"].ToString();

        List<GetDatasD> empListD = new List<GetDatasD>();
        SalesForce dcn = new SalesForce();
        DataSet dsPro = dcn.SalesForceListMgrGet_MgrOnly(div_code, "admin", "0");
        foreach (DataRow row in dsPro.Tables[0].Rows)
        {
            GetDatasD emp = new GetDatasD();

            emp.Sf_Name = row["Sf_Name"].ToString();
            emp.Sf_Code = row["Sf_Code"].ToString();
            emp.RSF_Code = row["Reporting_To_SF"].ToString();
            emp.Designation = row["Designation"].ToString();

            empListD.Add(emp);
        }
        return empListD.ToArray();
    }

    public class GetDatasD
    {
        public string Sf_Name { get; set; }
        public string Sf_Code { get; set; }
        public string RSF_Code { get; set; }
        public string Designation { get; set; }

    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        Session["ctrl"] = pnlContents;
        Control ctrl = (Control)Session["ctrl"];
        PrintWebControl(ctrl);
    }

    public static void PrintWebControl(Control ControlToPrint)
    {
        StringWriter stringWrite = new StringWriter();
        System.Web.UI.HtmlTextWriter htmlWrite = new System.Web.UI.HtmlTextWriter(stringWrite);
        if (ControlToPrint is WebControl)
        {
            Unit w = new Unit(100, UnitType.Percentage);
            ((WebControl)ControlToPrint).Width = w;
        }
        Page pg = new Page();
        pg.EnableEventValidation = false;
        HtmlForm frm = new HtmlForm();
        pg.Controls.Add(frm);
        frm.Attributes.Add("runat", "server");
        frm.Controls.Add(ControlToPrint);
        pg.DesignerInitialize();
        pg.RenderControl(htmlWrite);
        string strHTML = stringWrite.ToString();
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.Write(strHTML);
        HttpContext.Current.Response.Write("<script>window.print();</script>");
        HttpContext.Current.Response.End();

    }

    protected void btnExcel_Click(object sender, EventArgs e)
    {
        string strFileName = Page.Title;
        string attachment = "attachment; filename='" + strFileName + "'.xls";
        Response.ClearContent();
        Response.AddHeader("content-disposition", attachment);
        Response.ContentType = "application/ms-excel";
        StringWriter sw = new StringWriter();
        HtmlTextWriter htw = new HtmlTextWriter(sw);
        HtmlForm frm = new HtmlForm();
        form1.Parent.Controls.Add(frm);
        frm.Attributes["runat"] = "server";
        frm.Controls.Add(pnlContents);
        frm.RenderControl(htw);
        Response.Write(sw.ToString());
        Response.End();
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        string strFileName = Page.Title;

        using (StringWriter sw = new StringWriter())
        {
            using (HtmlTextWriter hw = new HtmlTextWriter(sw))
            {
                //this.Page.RenderControl(hw);
                this.RenderControl(hw);
                StringReader sr = new StringReader(sw.ToString());
                Document pdfDoc = new Document(PageSize.A4,
                    10f, 10f, 10f, 0f);
                PdfWriter writer = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                pdfDoc.Open();
                XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                pdfDoc.Close();
                Response.ContentType = "application/pdf";
                Response.AddHeader("content-disposition", "attachment;filename= '" + strFileName + "'.pdf");
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.Write(pdfDoc);
                Response.End();
            }

        }
    }

    [WebMethod]
    public static string gethints(string divcode)
    {
        test SFD = new test();
        DataSet ds = SFD.get_attend_hint(divcode);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }

    protected void btnClose_Click(object sender, EventArgs e)
    { }

    public class test
    {
        public DataSet attendance_view_wortypewise(string sf_code, string div_code, string month, string year, string subdiv_code, string statec)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet dsAdmin = null;
            string strQry = " EXEC [GET_WORKTYPEWISE_MONTHLY_ATTANDANCE_REPORT] '" + sf_code + "','" + div_code + "','" + month + "','" + year + "','" + subdiv_code + "','" + statec + "'";
            try
            {
                dsAdmin = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsAdmin;
        }
        public DataSet get_attend_hint(string divcode)
        {
            DB_EReporting db = new DB_EReporting();
            DataSet ds = null;

            //string strQry = "select WType_SName,Wtype from vwmas_worktype_all where division_code='" + divcode + "' and Active_Flag=0 group by WType_SName,Wtype";

            string strQry = " EXEC GET_AttendHint_WorkTypes '" + divcode + "' ";

            try
            {
                ds = db.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet misseddates(string divcode, string sfCode, string FMonth, string FYear)
        {
            DB_EReporting db = new DB_EReporting();
            DataSet ds = null;

            string strQry = " EXEC GET_MissDatesList  '" + divcode + "' ,'" + sfCode + "','" + FMonth + "','" + FYear + "'";

            //string strQry = "select cast(dcr_missed_date as date) from dcr_misseddates where sf_code='" + sfCode + "' and month='" + FMonth + "' and year='" + FYear + "' and status=0 order by dcr_missed_date";
            try
            {
                ds = db.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet callsubdates(string sfCode, string FMonth, string FYear)
        {
            DB_EReporting db = new DB_EReporting();
            DataSet ds = null;
            //string strQry = "select cast(dcr_missed_date as date) from mas_salesforce ms inner join dcr_misseddates dcr on sf_emp_id='" + sfCode + "' and ms.sf_code=dcr.sf_code where month='" + FMonth + "' and year='" + FYear + "' and status=2 order by dcr_missed_date";
            string strQry = " EXEC GET_CALLS_SubDateaList  '" + sfCode + "','" + FMonth + "','" + FYear + "'";

            try
            {
                ds = db.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet sfjoining_date(string divcode, string sfCode)
        {
            DB_EReporting db = new DB_EReporting();
            DataSet ds = null;
            //string strQry = "select WType_SName,Wtype from vwmas_worktype_all where division_code='" + divcode + "' and Active_Flag=0 group by WType_SName,Wtype";

            string strQry = " EXEC GET_AttendHint '" + divcode + "' ,'" + sfCode + "'";
            try
            {
                ds = db.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
    }
}