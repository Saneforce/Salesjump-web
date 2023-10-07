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
using System.Text;
using Bus_EReport;
using iTextSharp.tool.xml;
using System.Net;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using System.Web.Services;
using System.Data.SqlClient;
using System.Globalization;

public partial class MIS_Reports_rptattendancewrktype_1 : System.Web.UI.Page
{
    string sfCode = string.Empty;
    string sfname = string.Empty;
    string divcode = string.Empty;
    string sf_type = string.Empty;
    string FMonth = string.Empty;
    public string FYear = string.Empty;
    string type = string.Empty;
    string h = string.Empty;
    string wrktypename = string.Empty;
    int sum_time = 0;
    DataSet dsSalesForce = new DataSet();
    DataSet dsdatee = new DataSet();
    DataSet dsDoc = null;
    DateTime dtCurrent;
    TimeSpan ff;
    int rowspan = 0;
    string sCurrentDate = string.Empty;
    string endTime = string.Empty;
    string startTime = string.Empty;
    string tot_dr = string.Empty;
    string tot_value = string.Empty;
    string con_qty = string.Empty;
    string ec = string.Empty;
    string Monthsub = string.Empty;
    string date = string.Empty;
    string endd = string.Empty;
    DataSet dsDoctor = new DataSet();
    string gg = string.Empty;
    string imagepath = string.Empty;
    int quantity2 = 0;
    string mode = string.Empty;
    string subdiv_code = string.Empty;
	public string strFMonthName = string.Empty;
	 string statec = string.Empty;
    string statev = string.Empty;
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
        //sfCode = "Admin";

        if (sfCode.Contains("MGR"))
        {
            sf_type = "2";
        }
        else if (sfCode.Contains("MR"))
        {
            sf_type = "1";
        }
        else
        {
            sf_type = "0";
        }
        //type = Request.QueryString["type"].ToString();

        FMonth = Request.QueryString["FMonth"].ToString();
        FYear = Request.QueryString["FYear"].ToString();
        ddlFieldForce.Value = sfCode;
        ddlFYear.Value = FYear;
        ddlFMonth.Value = FMonth;
        SubDivCode.Value = subdiv_code;
        System.Globalization.DateTimeFormatInfo mfi = new System.Globalization.DateTimeFormatInfo();
        strFMonthName = mfi.GetMonthName(Convert.ToInt16(FMonth)).ToString().Substring(0, 3);
        lblHead.Text = "Attendance WorkTypewise View for the " + strFMonthName + " " + FYear+ "-" + statev;


        lblsf_name.Text = sfname;
        //if (type == "WorkTypewise")
        //{
        Fillworktypeview();
        //}
        //else
        //{
        //    Fillmaximisedview();
        //}

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
        DCR dc = new DCR();


        //mode = "Minimised";


        dsGV = attendance_view_wortypewise(sfCode, divcode, FMonth, FYear, subdiv_code,statec);
        //dsGV = dc.attendance_view_wortypewise(sfCode, divcode, FMonth, FYear, subdiv_code,statec);
        if (dsGV.Tables[0].Rows.Count > 0)
        {
            //dsGV.Tables[0].Columns.RemoveAt(3);
            /*dsGV.Tables[0].Columns.RemoveAt(0); Today
            dsGV.Tables[0].Columns.RemoveAt(6);
            dsGV.Tables[0].Columns.RemoveAt(7);
            dsGV.Tables[0].Columns.RemoveAt(7);
            dsGV.Tables[0].Columns.RemoveAt(15);
            dsGV.Tables[0].Columns.RemoveAt(15);Today*/
			
            dsGV.Tables[0].Columns.RemoveAt(0);
            dsGV.Tables[0].Columns.RemoveAt(6);
            dsGV.Tables[0].Columns.RemoveAt(7);
            dsGV.Tables[0].Columns.RemoveAt(7);
            dsGV.Tables[0].Columns.RemoveAt(8);
            dsGV.Tables[0].Columns.RemoveAt(9);
            dsGV.Tables[0].Columns.RemoveAt(17);
            dsGV.Tables[0].Columns.RemoveAt(17);


            //dsGV.Tables[0].Columns.RemoveAt(5);
            gvtotalorder.DataSource = dsGV;
            gvtotalorder.DataBind();
        }
        else
        {
            gvtotalorder.DataSource = null;
            gvtotalorder.DataBind();
        }
    }

    protected void PrintSundays(int year, int month, DayOfWeek dayName)
    {
       
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        try
        {

            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[0].Text = "Name";
                e.Row.Cells[0].Width = 325;
                e.Row.Cells[1].Width = 70;
               // e.Row.Cells[7].Visible = false; Today
               // e.Row.Cells[8].Visible = false; Today
                e.Row.Cells[15].Text = "Absent";
                e.Row.Cells[16].Text = "Weekly-Off";

                e.Row.Cells[2].Style.Add("white-space", "nowrap");
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
               // e.Row.Cells[7].Visible = false;Today
               // e.Row.Cells[8].Visible = false;Today
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

                //txtNamewd.ID = "txtboxnamewd" + colIndex;
                int wd = 0;
                int lcnt = 0;
                int oworktype = 0;
                int holidayct = 0;
                int weeklyoffct = 0;
                int retailingct = 0;
                int absntcnt = 0;
                int suncnt = 0;

                for (int colIndex = 18; colIndex < e.Row.Cells.Count; colIndex++)
                {




                   
                    int rowIndex = colIndex;
                    Label txtName = new Label();
                    txtName.Width = 20;
                    txtName.ID = "txtboxname" + colIndex;
                    txtName.Text = e.Row.Cells[colIndex].Text;
                    //txtName.AutoPostBack = true;  
                    //e.Row.Cells[colIndex].Controls.Add(txtName);  
                    Label txtName1 = new Label();
                    txtName1.Width = 20;
                    //if (divcode == "32")
                    //{

                    DateTime dateValue1 = new DateTime(Convert.ToInt16(FYear), Convert.ToInt16(FMonth), (colIndex - 17));
                    if (dateValue1.DayOfWeek == System.DayOfWeek.Sunday)
                    {
                        suncnt += 1;
                    }
                    if (txtName.Text == "&nbsp;")
                    {

                        txtName1.Style.Add("Color", "Red");
                        //txtName1.Style.Add("font-family", "Wingdings, Times, serif");
                        //txtName1.Style.Add("font-size", "20px");
                        //if (colIndex != e.Row.Cells.Count - 5 && colIndex != e.Row.Cells.Count - 4 && colIndex != e.Row.Cells.Count - 3 && colIndex != e.Row.Cells.Count - 6 && colIndex != e.Row.Cells.Count - 2 && colIndex != e.Row.Cells.Count - 1 && colIndex != e.Row.Cells.Count - 7)
                        //{
                        int day = Convert.ToInt32(DateTime.Now.ToString("dd"));
                        int mon = Convert.ToInt32(DateTime.Now.ToString("MM"));

                        //for (int colIndex1 = 16; colIndex1 < day; colIndex1++)
                        //{

                        //        if (colIndex != e.Row.Cells.Count - 1 && colIndex != e.Row.Cells.Count - 2 && colIndex != e.Row.Cells.Count - 3 && colIndex != e.Row.Cells.Count - 4
                        //&& colIndex != e.Row.Cells.Count - 5 && colIndex != e.Row.Cells.Count - 6 && colIndex != e.Row.Cells.Count - 7 && colIndex != e.Row.Cells.Count - 8 && colIndex != e.Row.Cells.Count - 9
                        //&& colIndex != e.Row.Cells.Count - 10 && colIndex != e.Row.Cells.Count - 11 && colIndex != e.Row.Cells.Count - 12 && colIndex != e.Row.Cells.Count - 13 && colIndex != e.Row.Cells.Count - 14
                        //&& colIndex != e.Row.Cells.Count - 15 && colIndex != e.Row.Cells.Count - 16 && colIndex != e.Row.Cells.Count - 17 && colIndex != e.Row.Cells.Count - 18
                        //&& colIndex != e.Row.Cells.Count - 19 && colIndex != e.Row.Cells.Count - 20 && colIndex != e.Row.Cells.Count - 21 && colIndex != e.Row.Cells.Count - 22 && colIndex != e.Row.Cells.Count - 23
                        //&& colIndex != e.Row.Cells.Count - 24 && colIndex != e.Row.Cells.Count - 25)
                        //            {

                      

                        if (Convert.ToInt16(FMonth) == (mon))
                        {

                            if (colIndex - 17 <= day)
                            {
                                txtName1.Text = "A";
                                txtName1.Style.Add("font", "Bold");
                                absntcnt += 1;
                            }
                          
                        }
                        else if (Convert.ToInt16(FMonth) > (mon))
                        {

                           

                        }
                        else
                        {
                            txtName1.Text = "A";
                            txtName1.Style.Add("font", "Bold");
                            absntcnt += 1;

                        }
                        DateTime dateValue = new DateTime(Convert.ToInt16(FYear), Convert.ToInt16(FMonth), (colIndex - 17));
                        if (dateValue.DayOfWeek == System.DayOfWeek.Sunday)
                        {
                            txtName1.Text = "S";
                        }
                      
                        //}



                        //}
                    }


                    else
                    {
                        txtName1.Style.Add("font-size", "10px");
                        txtName1.Style.Add("font", "Bold");
                        //if (colIndex != e.Row.Cells.Count - 2 && colIndex != e.Row.Cells.Count - 8)
                        //{

                        wd += 1;
                        if (txtName.Text == "L")
                        {
                            lcnt += 1;
                            txtName1.Style.Add("Background-color", "yellow");
                            txtName1.Style.Add("font-size", "10px");
                            txtName1.Style.Add("font", "Bold");
                        }
                        else if (txtName.Text != "L" && txtName.Text != "FW" && txtName.Text != "WO" && txtName.Text != "H")
                        {
                            oworktype += 1;
                            txtName1.Style.Add("Background-color", "Lightgreen");
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

                        //}

                        txtName1.Text = txtName.Text;



                    }


                    string pp = (wd-(lcnt + holidayct + weeklyoffct)).ToString();

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
                    e.Row.Cells[e.Row.Cells.Count - (days + 1)].Controls.Add(weeklyoff);
                    e.Row.Cells[e.Row.Cells.Count - (days + 2)].Controls.Add(lblabsent);
                    e.Row.Cells[e.Row.Cells.Count - (days + 3)].Controls.Add(holiday);
                    e.Row.Cells[e.Row.Cells.Count - (days + 4)].Controls.Add(leavecnt);
                    e.Row.Cells[e.Row.Cells.Count - (days + 6)].Controls.Add(retailingwrk);

                    e.Row.Cells[e.Row.Cells.Count - (days + 5)].Controls.Add(otherwrk);

                    e.Row.Cells[e.Row.Cells.Count - (days + 7)].Controls.Add(txtNamewd);


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
        {
            div_code = HttpContext.Current.Session["div_code"].ToString();

        }
        catch
        {
            div_code = HttpContext.Current.Session["Division_Code"].ToString();
        }

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
        SalesForce SFD = new SalesForce();
        DataSet ds = get_attend_hint(divcode);
       // DataSet ds = SFD.get_attend_hint(divcode);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }


    protected void btnClose_Click(object sender, EventArgs e)
    {

    }
    public DataSet attendance_view_wortypewise(string sf_code, string div_code, string month, string year, string subdiv_code, string statec)
    {
        DBase_EReport.DB_EReporting db_ER = new DBase_EReport.DB_EReporting();

        DataSet dsAdmin = null;
        string strQry = string.Empty;


        strQry = "exec [attendancemaximisedcfinal_wortypewise_1] '" + sf_code + "','" + div_code + "','" + month + "','" + year + "','" + subdiv_code + "','" + statec + "'";

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
    public static DataSet get_attend_hint(string divcode)
    {
        DBase_EReport.DB_EReporting db = new DBase_EReport.DB_EReporting();
        DataSet ds = null;
        string strQry = string.Empty;

        strQry = "select WType_SName,Wtype from vwmas_worktype_all where division_code='" + divcode + "' and Active_Flag=0 group by WType_SName,Wtype";
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