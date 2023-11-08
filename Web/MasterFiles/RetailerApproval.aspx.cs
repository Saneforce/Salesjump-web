using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Newtonsoft.Json;
using Bus_EReport;
using System.Web.Services;
using DBase_EReport;
using System.IO;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Spreadsheet;
using System.Data.SqlClient;
using Amazon.S3;
using Amazon.S3.Transfer;
using Amazon.S3.Model;
using System.Net;


public partial class MasterFiles_RetailerApproval : System.Web.UI.Page
{
    #region "Declaration"

    public static string sf_code = string.Empty;
    string sf_type = string.Empty;
    DataSet dsSalesForce = null;
    DataSet dsDivision = null;
    DataSet dsTP = null;
    public static DataTable dtt = null;
    public static string div_code = string.Empty;
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
    public static string GetAdditionalRetailer(string divcode, string ModuleId, string ColumnName)
    {

        DataSet ds = new DataSet();
        //AdminSetup Ad = new AdminSetup();

        ds = GetAdditionalRetailers(divcode, ModuleId, ColumnName);
        //ds = Ad.GetAdditionalRetailer(divcode, ModuleId);
        return JsonConvert.SerializeObject(ds.Tables[0]);

    }


    public static DataSet GetAdditionalRetailers(string divcode, string ModuleId, string ColumnName)
    {

        DB_EReporting db_ER = new DB_EReporting();

        DataSet dsAdmin = null;

        string strQry = "EXEC [GetDataForDynamicFields] '" + divcode + "','" + ModuleId + "','" + ColumnName + "' ";

        //strQry = "EXEC [GetDataForDynamicFields] '" + ModeleId + "' ,'" + divcode + "','" + ColumnName + "' ";

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



    [WebMethod]
    public static string GetCustomFormsFieldsColumns(string divcode, string ModuleId, string Sf)
    {
        DataSet ds = new DataSet();
        AdminSetup Ad = new AdminSetup();
        ds = Ad.GetCustomFormsFieldsColumns(divcode, ModuleId, Sf);

        return JsonConvert.SerializeObject(ds.Tables[0]);
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
            ds = ldr.GetNewRetailers(div_code, SFCode, "", dts.Year.ToString(), "3");
        }
        else
        {
            ds = ldr.GetNewRetailers(div_code, SFCode, "", dts.Year.ToString(), "3");
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

    [WebMethod]
    public static string GetBindCustomFieldData(string listeddrcode, string columnName)
    {
        DataSet ds = get_RetailerCustomField(listeddrcode, columnName, div_code);

        //DataSet ds = lst.get_RetailerCustomField(listeddrcode);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }

    public static DataSet get_RetailerCustomField(string listeddrcode, string columnName, string divcode)
    {
        DB_EReporting db_ER = new DB_EReporting();

        DataSet dsTerr = null;

        string strQry = "EXEC [Get_Retailer_CustomFieldDetails] '" + listeddrcode + "','" + columnName + "','" + divcode + "'";

        try
        {
            dsTerr = db_ER.Exec_DataSet(strQry);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return dsTerr;
    }


    [WebMethod]
    public static string DisPlayCutomFields(string ModuleId)
    {
        DataTable ds = new DataTable();
        //AdminSetup Ad = new AdminSetup();
       
        ds = getDCFields(div_code, ModuleId, sf_code);
        //ds = Ad.GetCustomFormsFieldsColumns(divcode, ModuleId, Sf);

        return JsonConvert.SerializeObject(ds);
    }


    public static DataTable getDCFields(string divcode, string ModuleId, string Sf_Code)
    {
        
        DataTable dsAdmin = new DataTable();


        if (divcode == null || divcode == "")
        { divcode = "0"; }

        if (ModuleId == null || ModuleId == "")
        { ModuleId = "0"; }

        if (Sf_Code == null || Sf_Code == "")
        { Sf_Code = ""; }


        string strQry = " SELECT *FROM DisplayFields  (NOLOCK) ";
        strQry += " WHERE ModuleId=3 AND ActiveView=1 AND Division_Code=@Division_Code AND Sf_Code=@Sf_Code";

        try
        {
            using (var con = new SqlConnection(Globals.ConnString))
            {
                using (var cmd = con.CreateCommand())
                {
                    cmd.CommandText = strQry;
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@Division_Code", Convert.ToString(divcode));
                    cmd.Parameters.AddWithValue("@Sf_Code", Convert.ToString(Sf_Code));
                    SqlDataAdapter dap = new SqlDataAdapter();
                    dap.SelectCommand = cmd;
                    con.Open();
                    dap.Fill(dsAdmin);
                    con.Close();
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;

        }
        return dsAdmin;


    }

    [WebMethod]
    public static string UpdateCutomRetailerData(string columnName, string ActiveView)
    {
        string update = "";
        update = Update_RetailerCustomField(columnName, ActiveView, div_code);

        //DataSet ds = lst.get_RetailerCustomField(listeddrcode);
        //return JsonConvert.SerializeObject(ds.Tables[0]);
        return update;
    }


    public static string Update_RetailerCustomField(string columnName, string ActiveView, string DivCode)
    {
        string update = "";
        string sql = @"UPDATE Trans_Custom_Fields_Details  SET ActiveView=@ActiveView WHERE Field_Name=@Field_Name AND Div_code=@Div_code";

        using (var con = new SqlConnection(Global.ConnString))
        {
            con.Open();
            using (var cmd = new SqlCommand(sql, con))
            {
                cmd.Parameters.AddWithValue("@ActiveView", ActiveView);
                cmd.Parameters.AddWithValue("@Field_Name", columnName);
                cmd.Parameters.AddWithValue("@Div_code", DivCode);
                cmd.ExecuteNonQuery();
                update = "Updated";
            }
        }

        return update;
    }


    [WebMethod(EnableSession = true)]
    public static string DownloadImageFromS3(string filename)
    {
        string msg = "";
        try
        {
           
            DataSet dsDivision = getStatePerDivision(div_code);
            string Folder = Convert.ToString(dsDivision.Tables[0].Rows[0]["Url_Short_Name"]);


            Folder = Folder.ToString().ToLower() + "_" + "Retailer";

            string filepath = HttpContext.Current.Server.MapPath("~/" + Folder + "/");

            //Create the Directory.
            if (!Directory.Exists(filepath))
            {
                Directory.CreateDirectory(filepath);
            }

            string currentDirectory = HttpContext.Current.Server.MapPath("~");
            string relativePath = Folder;

            // Iterate through the static fields for image fields and retrieve/save them
            msg = RetrieveAndSaveImage(currentDirectory, Folder, filename);


        }
        catch (Exception exception)
        {
            //Console.WriteLine(exception.Message, exception.InnerException);
            msg = " " + exception.Message + " , " + exception.InnerException + " ";
        }


        return msg;

    }



    private static string RetrieveAndSaveImage(string currentDirectory, string Folder, string fileName)
    {
        string msg = "";
        if (!string.IsNullOrWhiteSpace(fileName))
        {
            string localFilePath = Path.Combine(currentDirectory, Folder, fileName);


            try
            {
                string accessKey = "AKIA5OS74MUCASG7HSCG", accessSecret = "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";
                AmazonS3Client client = new AmazonS3Client(accessKey, accessSecret, Amazon.RegionEndpoint.APSouth1);

                var transferUtility = new TransferUtility(client);
                string bucketName = "happic";
                string objectKey = Folder + "/" + fileName;
                //string objectKey = Folder + "/" + objectKey;

                string localFilePaths = currentDirectory + "\\" + Folder + "\\" + fileName + "";

                //string localFilePaths = "http://fmcg.sanfmcg.com/MasterFiles/Reports/AudFiles/MR4126_1694754881446.mp3";                
                try
                {
                    //transferUtility.Download(localFilePath, bucketName, fileName);


                    GetObjectRequest request = new GetObjectRequest
                    {
                        BucketName = bucketName,
                        Key = objectKey
                    };

                    GetObjectResponse response = client.GetObject(request);

                    using (Stream responseStream = response.ResponseStream)
                    {
                        using (FileStream fileStream = File.Create(localFilePath))
                        {
                            responseStream.CopyTo(fileStream);
                            fileStream.Close();
                        }
                    }


                    //GetObjectResponse response = client.GetObject(bucketName, fileName);
                    //MemoryStream memoryStream = new MemoryStream();

                    //using (Stream responseStream = response.ResponseStream)
                    //{
                    //    responseStream.CopyTo(memoryStream);
                    //    memoryStream.Close();
                    //}



                    msg = "File downloaded locally on the server successfully.";
                }
                catch (AmazonS3Exception ex)
                {
                    msg = "S3 Error:: " + ex.Message.ToString() + "";
                    //Console.WriteLine("S3 Error: {ex.Message}");
                }
                catch (WebException ex)
                {
                    msg = "Web Error:: " + ex.Message.ToString() + "";
                    // Console.WriteLine("Web Error: {ex.Message}");
                }
                catch (Exception ex)
                {
                    msg = "Error:: " + ex.Message.ToString() + "";
                    //Console.WriteLine("Error: {ex.Message}");
                }

            }
            catch (Exception ex)
            {
                msg = "Error:: " + ex.Message.ToString() + " ";
                //throw ex;
            }
        }
        if (msg == "File downloaded locally on the server successfully.")
        {
            string sUrls = HttpContext.Current.Request.Url.Host.ToLower();
            string baseUrl = HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority + HttpContext.Current.Request.ApplicationPath.TrimEnd('/');
            //string sUrl = "http://localhost:58964";

            string ex = Path.GetExtension(fileName);
            if ((ex == ".png" || ex == ".jpg" || ex == ".jpeg" || ex == ".Webp"))
            {
                //msg = "<img style='width:30px;height:30px;' class='phimg' onclick='imgPOP(this)' src='" + baseUrl.ToString().Trim() + "/" + Folder + "/" + fileName + "'>";
                msg = "<img style='width:30px;height:30px;' class='picc'  src=" + baseUrl.ToString().Trim() + "/" + Folder + "/" + fileName + " />";
            }
            else if ((ex == ".pdf"))
            {
                //msg = "<a class='fa fa-file' style='width:50px;height:50px;' target=_blank' href=" + baseUrl.ToString().Trim() + "/" + Folder + "/" + fileName + "></a>";

                string imgsrc = baseUrl.ToString().Trim() + "/FileImage/pdf.jpg";

                string linkurl = baseUrl.ToString().Trim() + "/" + Folder + "/" + fileName;

                msg = "<a target=_blank' href=" + linkurl + "><img style='width:30px;height:30px;' src='" + imgsrc + "'  /></a>";
            }
            else if ((ex == ".xls" || ex == ".xlsx"))
            {
                string imgsrc = baseUrl.ToString().Trim() + "/FileImage/Excel.jpg";

                string linkurl = baseUrl.ToString().Trim() + "/" + Folder + "/" + fileName;

                msg = "<a target=_blank' href=" + linkurl + "><img style='width:30px;height:30px;' src='" + imgsrc + "'  /></a>";
            }
            else if ((ex == ".doc" || ex == ".docx"))
            {
                string imgsrc = baseUrl.ToString().Trim() + "/FileImage/doc.jpg";

                string linkurl = baseUrl.ToString().Trim() + "/" + Folder + "/" + fileName;

                msg = "<a target=_blank' href=" + linkurl + "><img style='width:30px;height:30px;' src='" + imgsrc + "'  /></a>";
            }
            else if ((ex == ".txt"))
            {
                string imgsrc = baseUrl.ToString().Trim() + "/FileImage/txt.jpg";

                string linkurl = baseUrl.ToString().Trim() + "/" + Folder + "/" + fileName;

                msg = "<a target=_blank' href=" + linkurl + "><img style='width:30px;height:30px;' src='" + imgsrc + "'  /></a>";
            }
            else
            {
                string linkurl = baseUrl.ToString().Trim() + "/" + Folder + "/" + fileName;


                msg = "<a  target=_blank' href=" + linkurl + ">link</a>";
            }

            //msg = baseUrl.ToString().Trim() + "/" + Folder + "/" + fileName;
        }

        return msg;



    }

    public static DataSet getStatePerDivision(string div_code)
    {
        DataSet dsAdmin = new DataSet();

        string strQry = "SELECT State_Code,Division_Name,Division_SName,Url_Short_Name  FROM Mas_Division ";
        strQry += " Where Division_Code = @Division_Code  GROUP BY State_Code,Division_Name,Division_SName,Url_Short_Name ";

        try
        {
            using (var con = new SqlConnection(Global.ConnString))
            {
                using (var cmd = con.CreateCommand())
                {
                    cmd.CommandText = strQry;
                    cmd.Parameters.AddWithValue("@Division_Code", Convert.ToInt32(div_code));
                    cmd.CommandType = CommandType.Text;
                    SqlDataAdapter dap = new SqlDataAdapter();
                    dap.SelectCommand = cmd;
                    con.Open();
                    dap.Fill(dsAdmin);
                    con.Close();
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;

        }
        return dsAdmin;
    }

    [WebMethod(EnableSession = true)]
    public static string UpdateApprove(string custCode, string erp, string lat, string longi)
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

            ret = adm.Retailer_Appprove_Manager(SFCode, custCode, "1", "", 0);
        }
        else
        {
            ret = adm.Retailer_Appprove_Manager(SFCode, custCode, "1", "", 0);
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
        int ret = adm.Retailer_Appprove_Manager(SFCode, custCode, "2", reasion, 4);
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
    public static string updChannel(string custCode, string spcode, string spname)
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
        IList<Customer> Data = JsonConvert.DeserializeObject<IList<Customer>>(sCus); ;
        ListedDR adm = new ListedDR();
        int ret = -1;
        try
        {
            for (int il = 0; il < Data.Count; il++)
            {
                ret = adm.updatelatlong(Data[il].erp, Data[il].lat, Data[il].longi, Data[il].custCode);
            }
            return "Updated Successfully..!";
        }
        catch
        {
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