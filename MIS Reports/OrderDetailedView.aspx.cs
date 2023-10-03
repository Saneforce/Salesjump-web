
using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using Bus_EReport;
using System.Web.UI.HtmlControls;
using System.Drawing;

using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using Rectangle = iTextSharp.text.Rectangle;
using iTextSharp.tool.xml;

using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Spreadsheet;
using System.Globalization;
using ClosedXML.Excel;


public partial class MIS_Reports_OrderDetailedView : System.Web.UI.Page
{
    #region "Declaration"
    DateTime ServerStartTime;
    DataSet dsSalesForce = null;
    string sf_code = string.Empty;
    DataSet dsTP = null;
    DateTime ServerEndTime;
    string div_code = string.Empty;
    string sf_type = string.Empty;
    int time;
	public static string sub_division = string.Empty;
    #endregion
    protected override void OnPreInit(EventArgs e)
    {
        base.OnPreInit(e);
        sf_type = Session["sf_type"].ToString();
        if (sf_type == "3")
        {
            this.MasterPageFile = "~/Master.master";
        }
        else if (sf_type == "2")
        {
            this.MasterPageFile = "~/Master_MGR.master";
        }
        else if (sf_type == "1")
        {
            this.MasterPageFile = "~/Master_MR.master";
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (sf_type == "3")
        {
            div_code = Session["div_code"].ToString();
        }
        else
        {
            div_code = Session["div_code"].ToString();
        }
        sf_code = Session["sf_code"].ToString();
		sub_division = Session["sub_division"].ToString();
        if (!Page.IsPostBack)
        {
            FillYear();
            fillsubdivision();
            FillMRManagers("0");
        }
    }
    private void fillsubdivision()
    {
        SalesForce sd = new SalesForce();
        dsSalesForce = sd.Getsubdivisionwise(div_code,sub_division);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            subdiv.DataTextField = "subdivision_name";
            subdiv.DataValueField = "subdivision_code";
            subdiv.DataSource = dsSalesForce;
            subdiv.DataBind();
            //subdiv.Items.Insert(0, new ListItem("--Select--", "0"));
			subdiv.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));

        }
    }
    protected void subdiv_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (subdiv.SelectedValue.ToString() != "0")
        {

            FillMRManagers(subdiv.SelectedValue.ToString());
        }
        else
        {
            FillMRManagers(subdiv.SelectedValue.ToString());
        }
    }

    protected override void OnLoadComplete(EventArgs e)
    {
        ServerEndTime = DateTime.Now;
        TrackPageTime();//It will give you page load time  
    }
    public void TrackPageTime()
    {
        TimeSpan serverTimeDiff = ServerEndTime.Subtract(ServerStartTime);
        time = serverTimeDiff.Minutes;
    }
    private void FillYear()
    {
        TourPlan tp = new TourPlan();
        dsTP = tp.Get_TP_Edit_Year(div_code);
        if (dsTP.Tables[0].Rows.Count > 0)
        {
            for (int k = Convert.ToInt16(dsTP.Tables[0].Rows[0]["Year"]); k <= DateTime.Now.Year + 1; k++)
            {
                ddlFYear.Items.Add(k.ToString());

                ddlFYear.SelectedValue = DateTime.Now.Year.ToString();

            }
        }

        ddlFMonth.SelectedValue = DateTime.Now.Month.ToString();


    }



    private void FillMRManagers(string Sub_Div_Code)
    {
        SalesForce sf = new SalesForce();
        // dsSalesForce = sf.SalesForceListMgrGet(div_code, sf_code);		  
        dsSalesForce = sf.SalesForceList(div_code, sf_code, Sub_Div_Code);

        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            ddlFieldForce.DataTextField = "sf_name";
            ddlFieldForce.DataValueField = "sf_code";
            ddlFieldForce.DataSource = dsSalesForce;
            ddlFieldForce.DataBind();
            ddlFieldForce.Items.Insert(0, "---Select Field Force---");

        }
    }
	
	 public void ExportData()
    {
        //Exporting to Excel
        string divcode = div_code.ToString();

        string Sf_Code = "";

        Sf_Code = ddlFieldForce.SelectedValue.ToString();

        if (Sf_Code == "-1" || Sf_Code == "0" || Sf_Code == "")
        {
            Sf_Code = ddlFieldForce.SelectedValue.ToString();
        }

        string FDate = txtFrom.Text.ToString();
        string TDate = ttxtdate.Text.ToString();
        string subdiv_code = "0";
        

        string[] FDate1 = FDate.Split('/');
        string[] TDate1 = TDate.Split('/');

        FDate = Convert.ToString(FDate1[2] + "-" + FDate1[1] + "-" + FDate1[0]);
        TDate = Convert.ToString(TDate1[2] + "-" + TDate1[1] + "-" + TDate1[0]);

        Order od = new Order();
        DataSet ds = od.GetOrderDetailsWithPrice(div_code, Sf_Code, FDate, TDate, subdiv_code);

        DataTable ot = ds.Tables[0];
        

        string filename = System.IO.Path.GetTempPath() + "Order_Details_Report_" + div_code + "_" + (System.DateTime.Now.ToString()).Replace(":", "_").Replace("/", "_").Replace(" ", "_") + ".xls";
        if (File.Exists(filename))
        {
            filename = System.IO.Path.GetTempPath() + "Order_Details_Report_" + div_code + "_" + (System.DateTime.Now.ToString()).Replace(":", "_").Replace("/", "_").Replace(" ", "_") + ".xls";
        }

        string attachment = "attachment; filename=" + filename;

        Response.ClearContent();
        Response.AddHeader("content-disposition", attachment);
        Response.ContentType = "application/vnd.ms-excel";


        using (System.IO.StringWriter sw = new System.IO.StringWriter())
        {
            using (System.Web.UI.HtmlTextWriter htw = new System.Web.UI.HtmlTextWriter(sw))
            {

                if (ot.Rows.Count > 0)
                {

                    GridView grid = new GridView();
                    grid.DataSource = ot;
                    grid.DataBind();
                    grid.RenderControl(htw);
                }

                Response.Write(sw.ToString());
            }
        }

        Response.End();


        // Codes for the Closed XML
        //using (XLWorkbook wb = new XLWorkbook())
        //{

        //    foreach (DataTable dt in ds.Tables)
        //    {
        //        var worksheet = wb.Worksheets.Add(dt.TableName);
        //        worksheet.Cell(1, 1).InsertTable(dt);
        //        worksheet.Columns().AdjustToContents();
        //    }

        //    wb.SaveAs(folderPath + "DataGridViewExport.xlsx");
        //    string myName = Server.UrlEncode("Test" + "_" + DateTime.Now.ToShortDateString() + ".xlsx");
        //    MemoryStream stream = GetStream(wb);// The method is defined below
        //    Response.Clear();
        //    Response.Buffer = true;
        //    Response.AddHeader("content-disposition", attachment);
        //    Response.ContentType = "application/vnd.ms-excel";
        //    Response.BinaryWrite(stream.ToArray());
        //    Response.End();
        //}

    }
    public MemoryStream GetStream(XLWorkbook excelWorkbook)
    {
        MemoryStream fs = new MemoryStream();
        excelWorkbook.SaveAs(fs);
        fs.Position = 0;
        return fs;
    }



    protected void exceldld_Click(object sender, EventArgs e)
    {   

        ExportData();
    }

}