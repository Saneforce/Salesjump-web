using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using Newtonsoft.Json;
using Bus_EReport;
using System.Web.Services;
using DBase_EReport;
using System.IO;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Spreadsheet;

public partial class MasterFiles_RetailerApproval : System.Web.UI.Page
{
    #region "Declaration"
    string div_code = string.Empty;
    string sf_code = string.Empty;
    string sf_type = string.Empty;
    DataSet dsSalesForce = null;
    DataSet dsDivision = null;
    DataSet dsTP = null;
	public static DataTable dtt = null;
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
        div_code = Session["div_code"].ToString();
        sf_code = Session["sf_code"].ToString();
        sf_type = Session["sf_type"].ToString();
        if (!Page.IsPostBack)
        {

        }
    }

    public class newRetailer
    {
        public string cCode { get; set; }
        public string cName { get; set; }
        public string channel { get; set; }
        public string code { get; set; }
        public string routeName { get; set; }
        public string createDate { get; set; }
        public string sfName { get; set; }
        public string picture { get; set; }
        public string Address { get; set; }
        public string Area_Name { get; set; }
        public string City_Name { get; set; }
        public string Landmark { get; set; }
        public string PIN_Code { get; set; }
        public string Contact_Person { get; set; }
        public string Designation { get; set; }
        public string Phone_No { get; set; }
        public string lat { get; set; }
        public string longn { get; set; }
    }

    [WebMethod]
    public static List<newRetailer> getNewRetailer()
    {

        List<newRetailer> Lists = new List<newRetailer>();

        DataSet ds = new DataSet();
        ListedDR ldr = new ListedDR();

        string div_code = "";
        try
        {
            div_code = HttpContext.Current.Session["div_code"].ToString();
        }
        catch
        {
            div_code = HttpContext.Current.Session["Division_Code"].ToString();
        }
        string SFCode = HttpContext.Current.Session["sf_code"].ToString();

        DateTime dts = new DateTime();

        if (SFCode.Contains("Admin") || SFCode.Contains("admin"))
        {
            ds = ldr.GetNewRetailers(div_code, SFCode, "", dts.Year.ToString(),"3");
        }
        else
        {
            ds = ldr.GetNewRetailers(div_code, SFCode, "", dts.Year.ToString(),"3");
        }
		dtt = ds.Tables[0];

        foreach (DataRow row in ds.Tables[0].Rows)
        {
            newRetailer list = new newRetailer();
            list.cCode = row["ListedDrCode"].ToString();
            list.cName = row["ListedDr_Name"].ToString();
			list.channel = row["channel"].ToString();										 
            list.code = row["code"].ToString();
            list.picture = row["Visit_Hours"].ToString();
            list.routeName = row["Territory_Name"].ToString();
            list.createDate = row["ListedDr_Created_Date"].ToString();
            list.sfName = row["SFName"].ToString();
            list.lat = row["ListedDr_Class_Patients"].ToString();
            list.longn = row["ListedDr_Consultation_Fee"].ToString();
            list.Address = row["ListedDr_Address1"].ToString();
            list.Area_Name = row["ListedDr_Address2"].ToString();
            list.City_Name = row["cityname"].ToString();
            list.Landmark = row["Land_Mark"].ToString();
            list.PIN_Code = row["PIN_Code"].ToString();
            list.Contact_Person = row["contactperson"].ToString();
            list.Designation = row["designation"].ToString();
            list.Phone_No = row["Mobile"].ToString();
            Lists.Add(list);
        }
        return Lists.ToList();
    }

    [WebMethod(EnableSession = true)]
    public static string UpdateApprove(string custCode,string erp, string lat, string longi)
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
        string SFCode = HttpContext.Current.Session["sf_code"].ToString();

        ListedDR adm = new ListedDR();
        int ret = -1;
        adm.updatelatlong(erp, lat, longi, custCode);
        if (SFCode.Contains("Admin") || SFCode.Contains("admin"))
        {

             ret = adm.Retailer_Appprove_Manager(SFCode, custCode, "1", "",0);
        }
        else
        {
             ret = adm.Retailer_Appprove_Manager(SFCode, custCode, "1", "",0);
        }
        if (ret > 0)
        {
            return "Retailer Approved Successfully..!";
        }
        else
        {
            return "Retailer Approved UnSuccessfull..!";
        }
    }
    [WebMethod(EnableSession = true)]
    public static string UpdateReject(string custCode, string reasion)
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
        string SFCode = HttpContext.Current.Session["sf_code"].ToString();

        ListedDR adm = new ListedDR();
        int ret = adm.Retailer_Appprove_Manager(SFCode, custCode, "2", reasion,4);
        if (ret > 0)
        {
            return "Retailer Rejected Successfully..!";
        }
        else
        {
            return "Retailer Rejected UnSuccessfull..!";
        }
    }
    [WebMethod(EnableSession = true)]
	public static string updChannel(string custCode,string spcode,string spname)
    {
        int ret = -1;
        try
        {
            DB_EReporting dber = new DB_EReporting();
            ret = dber.ExecQry("update Mas_ListedDr set Doc_Special_Code='" + spcode + "',Doc_Spec_ShortName='" + spname + "' where ListedDrCode=" + custCode + "");
            if (ret > 0)
            {
                return "Channel Updated.";
            }
            else
            {
                return "Channel Update Failed.";
            }
        }
        catch
        {
            return "Updated Failed.";
        }
    }
    [WebMethod(EnableSession = true)]
    public static string Updatelatlong(string sCus)
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
        string SFCode = HttpContext.Current.Session["sf_code"].ToString();
        IList<Customer>  Data = JsonConvert.DeserializeObject<IList<Customer>>(sCus); ;
        ListedDR adm = new ListedDR();
        int ret = -1;
        try
        {
            for (int il = 0; il < Data.Count; il++)
            {
                ret = adm.updatelatlong( Data[il].erp, Data[il].lat, Data[il].longi, Data[il].custCode);
            }
            return "Updated Successfully..!";
        }
        catch {
            return "Updated Failed..!";
        }
    }
    [WebMethod(EnableSession = true)]
    public static string getChannel()
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

        DataTable dtchannel = new DataTable();

        dtchannel = getsfcc("select Doc_Special_Code,Doc_Special_Name from Mas_Doctor_Speciality where Division_Code=" + div_code.TrimEnd(',') + " and Doc_Special_Active_Flag=0 order by Doc_Special_Name");

        return JsonConvert.SerializeObject(dtchannel);
    }
    public static DataTable getsfcc(string qrystring)
    {

        DB_EReporting db_ER = new DB_EReporting();

        DataTable dsSF = null;

        string strQry = qrystring;

        try
        {
            dsSF = db_ER.Exec_DataTable(strQry);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return dsSF;
    }
    public class Customer
    {
        public string custCode { get; set; }
        public string erp { get; set; }
        public string lat { get; set; }
        public string longi { get; set; }
}
 protected void btnexl_Click(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("FieldForce Name", typeof(string));
        dt.Columns.Add("Created Date", typeof(string));
        dt.Columns.Add("RetailerName", typeof(string));
        dt.Columns.Add("ERPCode", typeof(string));
        dt.Columns.Add("Address", typeof(string));
        dt.Columns.Add("Latitude", typeof(string));
        dt.Columns.Add("Longitude", typeof(string));
        dt.Columns.Add("City Name", typeof(string));
        dt.Columns.Add("Landmark", typeof(string));
        dt.Columns.Add("PIN Code", typeof(string));
        dt.Columns.Add("Contact Person", typeof(string));
        dt.Columns.Add("Designation", typeof(string));
        dt.Columns.Add("Mobile", typeof(string));
        dt.Columns.Add("Route Name", typeof(string));
        dt.Columns.Add("Channel", typeof(string));
        for (int i = 0; i < dtt.Rows.Count; i++)
        {
            DataRow dr = dt.NewRow();
            dr["FieldForce Name"] = dtt.Rows[i]["SFName"].ToString();
            dr["Created Date"] = dtt.Rows[i]["ListedDr_Created_Date"].ToString();
            dr["RetailerName"] = dtt.Rows[i]["ListedDr_Name"].ToString();
            dr["ERPCode"] = dtt.Rows[i]["code"].ToString();
            dr["Address"] = dtt.Rows[i]["ListedDr_Address1"].ToString();
            dr["Latitude"] = dtt.Rows[i]["ListedDr_Class_Patients"].ToString();
            dr["Longitude"] = dtt.Rows[i]["ListedDr_Consultation_Fee"].ToString();
            dr["City Name"] = dtt.Rows[i]["cityname"].ToString();
            dr["Landmark"] = dtt.Rows[i]["Land_Mark"].ToString();
            dr["PIN Code"] = dtt.Rows[i]["PIN_Code"].ToString();
            dr["Contact Person"] = dtt.Rows[i]["contactperson"].ToString();
            dr["Designation"] = dtt.Rows[i]["designation"].ToString();
            dr["Mobile"] = dtt.Rows[i]["Mobile"].ToString();
            dr["Route Name"] = dtt.Rows[i]["Territory_Name"].ToString();
            dr["Channel"] = dtt.Rows[i]["channel"].ToString();
            dt.Rows.Add(dr);
        }
        string filename = System.IO.Path.GetTempPath() + "POB Secondary_" + div_code + "_" + (System.DateTime.Now.ToString()).Replace(":", "_").Replace("/", "_").Replace(" ", "_") + ".xlsx";
        if (File.Exists(filename))
        {
            filename = System.IO.Path.GetTempPath() + "New Retailers_" + div_code + "_" + (System.DateTime.Now.ToString()).Replace(":", "_").Replace("/", "_").Replace(" ", "_") + ".xlsx";
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
        foreach (DataColumn column in dt.Columns)
        {
            var cell = new Cell { DataType = CellValues.String, CellValue = new CellValue(column.ColumnName) };
            headerRow.AppendChild(cell);
        }
        sheetData.AppendChild(headerRow);
        foreach (DataRow row in dt.Rows)
        {
            var newRow = new Row();
            foreach (DataColumn col in dt.Columns)
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
                Response.AddHeader("content-disposition", string.Format("attachment; filename ={0}", "New Retailers.xlsx"));
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
}