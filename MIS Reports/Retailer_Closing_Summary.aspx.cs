using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
using System.IO;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Spreadsheet;
using DBase_EReport;

public partial class MIS_Reports_Retailer_Closing_Summary : System.Web.UI.Page
{
    string div_code = string.Empty;
    DataSet dsSalesForce = null;
    DataSet dsUserList = null;
    DataSet dsDoc = null;
    DataSet dsDCR = null;
    int product_total = 0;
    int FWD_total = 0;
    DateTime dtCurrent;
    DataSet dsProduct = null;
    DataSet dsCatg = null;
    int MonColspan = 0;
    DataSet dsTP = null;
    string tot_dr = string.Empty;
    string tot_FWD = string.Empty;
    string tot_dcr_dr = string.Empty;
    int imissed_dr = -1;
    string sCurrentDate = string.Empty;
    string sf_code = string.Empty;
    string subdiv_code = string.Empty;
    DataSet dsSf = null;
    string sf_type = string.Empty;
    string MultiSf_Code = string.Empty;
    int mode = -1;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    string strSf_Code = string.Empty;
    string Monthsub = string.Empty;
    DataSet dsSalesforce = null;
    DataSet dsDoctor = null;
    int tot_days = -1;
    int cday = 1;
    string sDCR = string.Empty;
    int ddate = 0;

    string divcode = string.Empty;
    string sfname = string.Empty;
    public string Sf_Code = string.Empty;
    public string fmonth = string.Empty;
    public string fyear = string.Empty;
    public string remarksid = string.Empty;
    string subdivcode = string.Empty;
    string mname = string.Empty;
    string yname = string.Empty;
    DataSet ss = new DataSet();
    DataSet ff = new DataSet();

    DataSet dsGV = new DataSet();
    Int64[] Rcnt;
    Int64 Pcnt = 0;
    Int64 avail = 0;



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
        
        sf_code = Session["sf_code"].ToString();
        sf_type = Session["sf_type"].ToString();
       
        if (sf_type == "3")
        {
            div_code = Session["div_code"].ToString();
        }
        else
        {
            div_code = Session["div_code"].ToString();
        }
        if (div_code.Contains(','))
        {
            div_code = div_code.Remove(div_code.Length - 1);
        }

        if (!Page.IsPostBack)
        {
            //menu1.Title = this.Page.Title;
            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);
            //menu1.FindControl("btnBack").Visible = false;
            fillsubdivision();
            FillYear();
            if (subdiv.Items.Count > 0)
            {
                subdiv.SelectedIndex = 1;
                subdiv_SelectedIndexChanged(sender, e);
            }
        }


        FillColor();
    }
    protected void subdiv_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (subdiv.SelectedValue != "0")
        {
            
            if (sf_type == "3")
            {
                FillMRManagers();
            }
            else if (sf_type == "2")
            {
                FillMRManagers();
            }
            else if (sf_type == "1")
            {
                FillMRManagers();
                
            }
        }
        else
        {
            ddlFieldForce.DataSource = null;
            ddlFieldForce.Items.Clear();
            ddlFieldForce.Items.Insert(0, new ListItem("--Select--", "0"));


            ddlSF.DataSource = null;
            ddlSF.Items.Clear();
            ddlSF.Items.Insert(0, new ListItem("--Select--", "0"));       
        }

    }
    private void fillsubdivision()
    {

        SalesForce sd = new SalesForce();
        // dsSalesForce = sd.Getsubdivisionwise(div_code);

        if (sf_type == "3")
            dsSalesForce = sd.Getsubdivisionwise(div_code);
        else
            dsSalesForce = sd.Getsubdivisionwise_sfcode(div_code, sf_code);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            subdiv.DataTextField = "subdivision_name";
            subdiv.DataValueField = "subdivision_code";
            subdiv.DataSource = dsSalesForce;
            subdiv.DataBind();
            subdiv.Items.Insert(0, new ListItem("--All Division--", ""));

        }
    }

    private void FillYear()
    {
        TourPlan tp = new TourPlan();
        dsTP = tp.Get_TP_Edit_Year(div_code);
        if (dsTP.Tables[0].Rows.Count > 0)
        {
            for (int k = Convert.ToInt16(dsTP.Tables[0].Rows[0]["Year"]); k <= DateTime.Now.Year + 1; k++)
            {
                ddlYear.Items.Add(k.ToString());
                ddlYear.SelectedValue = DateTime.Now.Year.ToString();
            }
        }
        ddlMonth.SelectedValue = DateTime.Now.Month.ToString();
    }

    private void FillMR()
    {
        SalesForce sf = new SalesForce();
        dsSalesforce = sf.SalesForceList_New_GetMr(div_code, sf_code);
        if (dsSalesforce.Tables[0].Rows.Count > 0)
        {
            ddlFieldForce.DataValueField = "SF_Code";
            ddlFieldForce.DataTextField = "Sf_Name";
            ddlFieldForce.DataSource = dsSalesforce;
            ddlFieldForce.DataBind();

        }
    }

    private void FillMRManagers()
    {

        try
        {
            //SalesForce sf = new SalesForce();
            dsSalesForce = SalesForceList(sf_code, div_code); ;
            //if (sf_type == "3")
            //{
            //    dsSalesForce.Tables[0].Rows[0].Delete();
            //}
            //else
            //{

            //}

            if (dsSalesForce.Tables[0].Rows.Count > 0)
            {
                ddlFieldForce.DataTextField = "sf_name";
                ddlFieldForce.DataValueField = "sf_code";
                ddlFieldForce.DataSource = dsSalesForce;
                ddlFieldForce.DataBind();

                ddlSF.DataTextField = "Desig_Color";
                ddlSF.DataValueField = "sf_code";
                ddlSF.DataSource = dsSalesForce;
                ddlSF.DataBind();

            }
            FillColor();
        }
        catch (Exception)
        {

        }
    }
    public DataSet SalesForceList(string sf_code, string div_code, string Sub_Div_Code = "0", string Alpha = "1", string stcode = "0")
    {
        DB_EReporting db_ER = new DB_EReporting();

        DataSet dsSF = null;
        string strQry = "EXEC getHyrSFList '" + sf_code + "','" + div_code + "','" + Sub_Div_Code + "','" + Alpha + "'," + stcode + "";

        try
        {
            dsSF = db_ER.Exec_DataSet(strQry);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return dsSF;
    }

    private void FillColor()
    {
        int j = 0;

        foreach (ListItem ColorItems in ddlSF.Items)
        {
            //ddlFieldForce.Items[j].Selected = true;
            string bcolor = "#" + ColorItems.Text;
            ddlFieldForce.Items[j].Attributes.Add("style", "background-color:" + bcolor);

            j = j + 1;

        }
    }

    protected void btnGo_Click(object sender, EventArgs e)
    {
        string sURL = string.Empty;

        sURL = "rpt_Retailer_Closing_Summary.aspx?div_code=" + div_code.ToString() + "&subdiv_code=" + subdiv.SelectedValue + "&sfcode=" + ddlFieldForce.SelectedValue + "&FMonth=" + ddlMonth.SelectedValue.ToString() + "&FYear=" + ddlYear.SelectedValue.ToString() + "&sf_name=" + ddlFieldForce.SelectedItem.ToString();
        string newWin = "window.open('" + sURL + "',null,'resizable=yes,toolbar=no,scrollbars=yes,menubar=no,status=no,width=800,height=600,left=0,top=0');";
        ClientScript.RegisterStartupScript(this.GetType(), "pop", newWin, true);
    }

    protected void exceldld_Click(object sender, EventArgs e)
    {
        divcode = div_code.ToString();
        string sfcode = ddlFieldForce.SelectedValue.ToString();

        if (sfcode == "" || sfcode == null)
        {
            sfcode = "admin";
        }

        string FMonth = ddlMonth.SelectedValue.ToString();
        string FYear = ddlYear.SelectedValue.ToString();
        string sf_name = ddlFieldForce.SelectedItem.ToString();


        DataTable ot = generateRetailerCloseingExcel(divcode, sfcode, FMonth, FYear);
        string filename = System.IO.Path.GetTempPath() + "Retailer_Closeing_Summary_Report" + div_code + "_" + (System.DateTime.Now.ToString()).Replace(":", "_").Replace("/", "_").Replace(" ", "_") + ".xlsx";

        if (File.Exists(filename))
        {
            filename = System.IO.Path.GetTempPath() + "Retailer_Closeing_Summary_Report_" + div_code + "_" + (System.DateTime.Now.ToString()).Replace(":", "_").Replace("/", "_").Replace(" ", "_") + ".xlsx";
        }
        uint sheetId = 1;
        SpreadsheetDocument spreadsheetDocument = SpreadsheetDocument.Create(filename, SpreadsheetDocumentType.Workbook);
        WorkbookPart workbookpart = spreadsheetDocument.AddWorkbookPart();
        workbookpart.Workbook = new Workbook();
        var worksheetPart = workbookpart.AddNewPart<WorksheetPart>();
        var sheetData = new SheetData();
        worksheetPart.Worksheet = new Worksheet(sheetData);
        Sheets sheets;
        sheets = spreadsheetDocument.WorkbookPart.Workbook.AppendChild<Sheets>(new Sheets());
        var sheet = new Sheet()
        {
            Id = spreadsheetDocument.WorkbookPart.
                       GetIdOfPart(worksheetPart),
            SheetId = sheetId,
            Name = "Sheet" + sheetId
        };
        sheets.Append(sheet);
        var headerRow = new Row();
        foreach (DataColumn column in ot.Columns)
        {
            var cell = new Cell { DataType = CellValues.String, CellValue = new CellValue(column.ColumnName) };
            headerRow.AppendChild(cell);
        }
        sheetData.AppendChild(headerRow);
        foreach (DataRow row in ot.Rows)
        {
            var newRow = new Row();
            foreach (DataColumn col in ot.Columns)
            {
                var cell = new Cell
                {
                    DataType = CellValues.String,
                    CellValue = new CellValue(row[col].ToString())
                };
                newRow.AppendChild(cell);
            }

            sheetData.AppendChild(newRow);
        }
        workbookpart.Workbook.Save();
        spreadsheetDocument.Close();
        try
        {
            Response.ClearContent();
            using (FileStream objFileStream = new FileStream(filename, FileMode.Open, FileAccess.Read))
            {
                byte[] data1 = new byte[objFileStream.Length];
                objFileStream.Read(data1, 0, data1.Length);
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", string.Format("attachment; filename ={0}", "Secondary Order Report.xlsx"));
                Response.BinaryWrite(data1);
            }
            FileInfo currentfile = new FileInfo(filename);
            currentfile.Delete();
        }
        catch (Exception ex)
        {
        }
        Response.End();


    }

    private DataTable generateRetailerCloseingExcel(string divcode, string Sf_Code, string fmonth, string fyear)
    {
        DataTable dtOrders = new DataTable();
        DataSet dsGc = new DataSet();
        DCR dc = new DCR();

        string stCrtDtaPnt = string.Empty;

        //dsGV = dc.getRemarks(divcode);
        //Rcnt = new Int64[dsGV.Tables[0].Rows.Count];

        divcode = div_code.ToString();
        //string sfcode = Convert.ToString(ddlFieldForce.SelectedValue);
        //string FMonth = ddlMonth.SelectedValue.ToString();
        //string FYear = ddlYear.SelectedValue.ToString();
        //string sf_name = ddlFieldForce.SelectedItem.ToString();

        ss = new DataSet();
        ss = dc.getRetClosingQty(divcode, Sf_Code,"", fmonth, fyear);


        SalesForce SF = new SalesForce();
        dsGV = SF.GetProduct_Name(divcode, "0");

        ff = dc.getRetailerClosing(divcode, Sf_Code, "", fmonth, fyear);


        //dtOrders.Columns.Add("Sf_Name", typeof(string));
        //dtOrders.Columns.Add("Retailer", typeof(string));
        //dtOrders.Columns.Add("Order_Date", typeof(string));
		
		dtOrders.Columns.Add("SF Name", typeof(string));
        dtOrders.Columns.Add("Retailer", typeof(string));
        dtOrders.Columns.Add("Retailer Code", typeof(string));
        dtOrders.Columns.Add("Route Name", typeof(string));
        dtOrders.Columns.Add("Order Date", typeof(string));



        var ProductNames = (from row in ss.Tables[0].AsEnumerable()
                            orderby row.Field<string>("Product_Detail_Name")
                            select new
                            {
                                //Product_Code = row.Field<string>("Product_Code"),
                                Product_Name = row.Field<string>("Product_Detail_Name")
                            }).Distinct().ToList();

        foreach (var str in ProductNames)
        {
            dtOrders.Columns.Add(str.Product_Name.ToString(), typeof(double));
        }

        foreach (DataRow dr in ff.Tables[0].Rows)
        {
            DataRow rw = dtOrders.NewRow();
            string transSlNO = dr["Trans_sl_no"].ToString();
			
			
			
            //rw["Sf_Name"] = dr["SF_Name"].ToString();
            //rw["Retailer"] = dr["Retailer"].ToString();
            //rw["Order_Date"] = dr["Order_Date"].ToString();
			
			
			rw["SF Name"] = dr["SF Name"].ToString();
            rw["Retailer"] = dr["Retailer"].ToString();
            rw["Retailer Code"] = dr["Retailer Code"].ToString();
            rw["Route Name"] = dr["Route Name"].ToString();
            rw["Order Date"] = dr["Order Date"].ToString();
			
            //rw["Order Type"] = dr["OrderType"].ToString();
            //rw["Distributor Name"] = dr["Stockist_name"].ToString();
            //rw["ERP Code"] = dr["ERP_Code"].ToString();
            //rw["Order taken by"] = dr["SF_Name"].ToString();
            //rw["Retailer Name"] = dr["retailername"].ToString();
            //rw["Channel"] = dr["channel"].ToString();
            //rw["Route"] = dr["routename"].ToString();
            //rw["Net Weight Value"] = (dr["net_weight_value"]).Equals(DBNull.Value) ? 0 : Convert.ToDouble(dr["net_weight_value"]);
            //rw["Order Value"] = (dr["Order_value"]).Equals(DBNull.Value) ? 0 : Convert.ToDouble(dr["Order_value"]);
            DataRow[] drp = ss.Tables[0].Select("Trans_Sl_No='" + transSlNO + "'");
            for (int i = 0; i < drp.Length; i++)
            {
                rw[(drp[i]["Product_Detail_Name"].ToString())] =
                    Convert.ToInt32((string.IsNullOrEmpty(Convert.ToString(rw[drp[i]["Product_Detail_Name"].ToString()]))) ? 0 : rw[(drp[i]["Product_Detail_Name"].ToString())]) + Convert.ToInt32(drp[i]["Closing"]);
            }
            dtOrders.Rows.Add(rw);
        }

        return dtOrders;
    }
}