using Bus_EReport;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using System.Activities.Expressions;
using DBase_EReport;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Interop;


public partial class MasterFiles_CustomFieldFrom : System.Web.UI.Page
{

    string sf_type = string.Empty;
    public static string sf_code = string.Empty;
    public string frm_id = string.Empty;

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
            this.MasterPageFile = "~/MasterForAll.master";
        }
        else if (sf_type == "1")
        {
            this.MasterPageFile = "~/MasterForAll.master";
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        frm_id = Request.QueryString["FrmID"];
        sf_code = Session["Sf_Code"].ToString();
    }

    [WebMethod]
    public static string GetCustomMasterTableVal()
    {

        string div_code = HttpContext.Current.Session["div_code"].ToString();

        AdminSetup Ad = new AdminSetup();
        DataSet ds = new DataSet();

        ds = Ad.CustomMasterTableVal(div_code);

        return JsonConvert.SerializeObject(ds.Tables[0]);

        //List<ListItem> customers = new List<ListItem>();
        //if (ds.Tables[0].Rows.Count > 0)
        //{
        //    foreach (DataRow sdr in ds.Tables[0].Rows)
        //    {
        //        customers.Add(new ListItem
        //        {
        //            Value = sdr["TableActualName"].ToString(),
        //            Text = sdr["TableName"].ToString()
        //        });
        //    }
        //}

        //return customers;
    }

    [WebMethod]
    public static string CustomGetTableColumns(string TableName)
    {
        string div_code = HttpContext.Current.Session["div_code"].ToString();

        AdminSetup Ad = new AdminSetup();
        DataSet ds = Ad.CustomGetTableColumns(TableName);
        DataTable dt = new DataTable();
        if ((ds.Tables.Count > 0 || ds != null))
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                dt = ds.Tables[0];
            }
        }

        return JsonConvert.SerializeObject(dt);

        //List<ListItem> customers = new List<ListItem>();
        //if (ds.Tables[0].Rows.Count > 0)
        //{
        //    foreach (DataRow sdr in ds.Tables[0].Rows)
        //    {
        //        customers.Add(new ListItem
        //        {
        //            Value = sdr["Cols"].ToString(),
        //            Text = sdr["Cols"].ToString()
        //        });
        //    }
        //}

        //return customers;
    }

    [WebMethod]
    public static string AddCustomField(string Formdata, string sf_Code, string cusxml)
    {
        string msg = string.Empty;
        string SfCode = sf_Code + "_" + DateTime.Now.Ticks.ToString();

        cusffclass ad = new cusffclass();

        cusffclass.SaveCustomFormField Data = JsonConvert.DeserializeObject<cusffclass.SaveCustomFormField>(Formdata);
        //DB_EReporting Ad = new DB_EReporting();
        msg = ad.saveCustomFormFields(Data, SfCode, cusxml);
        return msg;
    }

    [WebMethod]
    public static string AddCustomForm(string ModuleName, string ModuleType, string ModuleCate)
    {
        string msg = string.Empty;
        msg = saveCustomForms(ModuleName, ModuleType, ModuleCate);
        return msg;
    }

    [WebMethod]
    public static string AddCustomFormFieldGroup(string ModuleId, string txtFGroupName)
    {
        string msg = string.Empty;
        msg = saveCustomFormsFieldGroup(ModuleId, txtFGroupName);
        return msg;
    }

    [WebMethod]
    public static string GetCustomModuleList(string divcode)
    {
        DataSet ds = new DataSet();

        ds = getmodulelist(divcode);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }

    [WebMethod]
    public static string GetCustomFieldGroupList(string divcode, string ModuleId)
    {
        DataSet ds = new DataSet();

        ds = getFieldGrouplist(divcode, ModuleId);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }


    [WebMethod]
    public static string GetCustomFormsFieldsList(string divcode, string ModuleId)
    {
        DataSet ds = new DataSet();
        AdminSetup Ad = new AdminSetup();
        ds = Ad.GetCustomFormsFieldsData(divcode, ModuleId);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }

    [WebMethod]
    public static string GetCustomFormsFieldsDataById(string divcode, string FieldId)
    {
        DataSet ds = new DataSet();
        AdminSetup Ad = new AdminSetup();
        ds = Ad.GetCustomFormsFieldsDataById(divcode, FieldId);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }

    [WebMethod]
    public static string getCustomEditFld(string divcode, string FldID)
    {
        DataSet ds = new DataSet();
        AdminSetup Ad = new AdminSetup();
        ds = Ad.getCustomFields_Edit(divcode, FldID);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }

    public static DataSet getmodulelist(string divcode)
    {
        DataSet dsAdmin = new DataSet();

        string strQry = "SELECT ModuleId,ModuleName FROM Mas_ModuleTable GROUP BY ModuleId,ModuleName ";

        try
        {
            using (var con = new SqlConnection(Globals.ConnString))
            {
                using (var cmd = con.CreateCommand())
                {
                    cmd.CommandText = strQry;
                    //cmd.Parameters.AddWithValue("@divcode", Convert.ToString(divcode));
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

    public static DataSet getFieldGrouplist(string divcode, string ModuleId)
    {
        DataSet dsAdmin = new DataSet();

        string strQry = "SELECT FieldGroupId,FGroupName,FGTableName FROM Mas_FieldGroupTable Where ModuleId = @ModuleId  GROUP BY FieldGroupId,FGroupName,FGTableName ";

        try
        {
            using (var con = new SqlConnection(Globals.ConnString))
            {
                using (var cmd = con.CreateCommand())
                {
                    cmd.CommandText = strQry;
                    cmd.Parameters.AddWithValue("@ModuleId", Convert.ToInt32(ModuleId));
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

    public static string saveCustomFormsFieldGroup(string ModuleId, string txtFGroupName)
    {
        string msg = string.Empty;

        try
        {
            using (SqlConnection con = new SqlConnection(Globals.ConnString))
            {
                //using (SqlCommand cmd = con.CreateCommand())
                //{
                try
                {

                    DateTime _now = DateTime.Now;
                    string _dd = _now.ToString("dd"); //
                    string _mm = _now.ToString("MM");
                    string _yy = _now.ToString("yyyy");
                    string _hh = _now.Hour.ToString();
                    string _min = _now.Minute.ToString();
                    string _ss = _now.Second.ToString();

                    string FGTableName = Convert.ToString("CFGT" + ModuleId + "_" + _yy + _mm + _dd + _hh + _min + _ss);

                    string number = Convert.ToString(DateTime.Now.Ticks);
                    //string FGTableName = "CFG" + ModuleId+"_" + number;                       


                    con.Open();
                    SqlCommand cmd = new SqlCommand("Create_CustomFormsFieldsGroup", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@FGroupName", txtFGroupName.ToString().Trim());
                    cmd.Parameters.AddWithValue("@FGTableName", FGTableName.ToString().Trim());
                    cmd.Parameters.AddWithValue("@FGModuleId", ModuleId.ToString().Trim());
                    //cmd.Parameters.Add("@returnMessage", SqlDbType.NVarChar, 150).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@returnMessage", SqlDbType.NVarChar, 150);
                    cmd.Parameters["@returnMessage"].Direction = ParameterDirection.Output;
                    cmd.ExecuteNonQuery();
                    msg = (string)cmd.Parameters["@returnMessage"].Value;

                    //cmd.Parameters.AddRange(parameters);
                    //cmd.ExecuteNonQuery();
                    con.Close();
                }
                catch (Exception ex)
                {
                    msg = ex.Message.ToString();
                }
                finally { if (con.State == ConnectionState.Open) { con.Close(); } con.Dispose(); }
                //}
            }
        }
        catch (Exception ex)
        { msg = ex.Message.ToString(); }
        return msg;
    }

    public static string saveCustomForms(string ModuleName, string ModuleType, string ModuleCate)
    {
        string msg = string.Empty;

        try
        {
            using (SqlConnection con = new SqlConnection(Globals.ConnString))
            {
                //using (SqlCommand cmd = con.CreateCommand())
                //{
                try
                {

                    con.Open();
                    SqlCommand cmd = new SqlCommand("Create_CustomForms", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ModuleName", ModuleName.ToString().Trim());
                    cmd.Parameters.AddWithValue("@ModuleType", ModuleType.ToString().Trim());
                    cmd.Parameters.AddWithValue("@ModuleCate", ModuleCate.ToString().Trim());
                    //cmd.Parameters.Add("@returnMessage", SqlDbType.NVarChar, 150).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@returnMessage", SqlDbType.NVarChar, 150);
                    cmd.Parameters["@returnMessage"].Direction = ParameterDirection.Output;
                    cmd.ExecuteNonQuery();
                    msg = (string)cmd.Parameters["@returnMessage"].Value;

                    //cmd.Parameters.AddRange(parameters);
                    //cmd.ExecuteNonQuery();
                    con.Close();

                    //if (msg == "New Form Module Created")
                    //{

                    //    string codefile = "" + ModuleName.Trim() + ".aspx.cs";
                    //    string inherit = HttpContext.Current.Server.MapPath("" + ModuleName.Trim() + "");

                    //    string[] aspxLines = {@"<%@ Page Language=""C#"" AutoEventWireup=""true"" CodeFile="+ModuleName.Trim()+" Inherits="+inherit+" %>",
                    //        "<!DOCTYPE html>",
                    //        "<head>",
                    //        "<title>The New Page</title>",
                    //        "</head>",
                    //        "<body>",
                    //        @"<form id=""form1"" runat=""server"">",
                    //        "<div>",
                    //        @"<asp:literal id=""output"" runat=""server"" />",
                    //        "</div>",
                    //        "</form>",
                    //        "</body>",
                    //        "</html>"};
                    //    string[] csLines = {
                    //        "using System;",
                    //        "using System.Collections.Generic;",
                    //        "using System.Linq;",
                    //        "using System.Web;",
                    //        "using System.Web.UI;",
                    //        "using System.Web.UI.WebControls;",
                    //        "using System.Data;",
                    //        "using Bus_EReport;",
                    //        "using System.Data.SqlClient;",
                    //        "using System.Web.Services;",
                    //        "using System.Configuration;",
                    //        "using Newtonsoft.Json;",
                    //        "using System.Globalization;",
                    //        "using ClosedXML.Excel;",
                    //        "using System.Xml;",
                    //        "using System.IO;",
                    //        "using System.Data.OleDb;",
                    //        "using DBase_EReport;",

                    //        "public partial class " + inherit.Trim()+ " : System.Web.UI.Page {",
                    //        "protected void Page_Load(object sender, EventArgs e) {","output.Text=;",  "}","}"
                    //    };
                    //    File.WriteAllLines(HttpContext.Current.Server.MapPath("" + ModuleName.Trim() + ".aspx"), aspxLines);
                    //    File.WriteAllLines(HttpContext.Current.Server.MapPath("" + ModuleName.Trim() + ".aspx.cs"), csLines);
                    //  }
                    
                }
                catch (Exception ex)
                {
                    msg = ex.Message.ToString();
                }
                finally { if (con.State == ConnectionState.Open) { con.Close(); } con.Dispose(); }
                //}
            }
        }
        catch (Exception ex)
        { msg = ex.Message.ToString(); }
        return msg;
    }

    public class cusffclass
    {
        public string saveCustomFormFields(SaveCustomFormField sd, string sfcode, string cusxml)
        {

            string msg = string.Empty;
            string msgs = string.Empty;

            try
            {
                string FldId = Convert.ToString(sd.hfldid);
                string fldname = sd.FldName.ToString();
                string Fld_Col = "";

                //if (fldname.Contains(" "))
                //{ Fld_Col = fldname.Replace(" ", "_") + "_" + sd.divcode.ToString().Trim(); }
                //else { Fld_Col = fldname + "_" + sd.divcode.ToString().Trim(); }
                Fld_Col = "Fld" + sd.ModuleId.ToString().Trim() + "" + sd.FGroupId.ToString().Trim() + "" + sfcode.ToString();
                //Fld_Col = sd.ModuleId.ToString().Trim() + "" + sd.FGroupId.ToString().Trim() + "" + sfcode.ToString().Trim();

                string SrtNo = sd.SrtNo.ToString();

                string sfldtyp = sd.sfldtyp.ToString();

                if ((sfldtyp == null || sfldtyp == ""))
                {
                    sd.ModuleId = 0;
                }

                if ((SrtNo == "" || SrtNo == null))
                { sd.SrtNo = "0"; }

                if ((FldId == "" || FldId == null || FldId == "0"))
                {
                    using (SqlConnection con = new SqlConnection(Globals.ConnString))
                    {
                        //using (SqlCommand cmd = con.CreateCommand())
                        //{
                        try
                        {

                            //cmd.CommandText = "Create_CustomForms_Fields";
                            con.Open();
                            SqlCommand cmd = new SqlCommand("Create_CustomForms_Fields", con);
                            cmd.CommandType = CommandType.StoredProcedure;                            
                            cmd.Parameters.AddWithValue("@Div", sd.divcode.ToString());
                            cmd.Parameters.AddWithValue("@Fld_Id", sfcode.ToString());
                            cmd.Parameters.AddWithValue("@ModuleId", sd.ModuleId.ToString().Trim());
                            cmd.Parameters.AddWithValue("@FGroupId", sd.FGroupId.ToString().Trim());
                            cmd.Parameters.AddWithValue("@Fld_Name", sd.FldName.ToString().Trim());
                            cmd.Parameters.AddWithValue("@Field_Col", Fld_Col.ToString().Trim());
                            cmd.Parameters.AddWithValue("@Fldtyp", sd.sfldtyp.ToString().Trim());
                            cmd.Parameters.AddWithValue("@Fld_Src_Name", sd.Fld_Src_Name.ToString().Trim());
                            cmd.Parameters.AddWithValue("@Fld_Src_Field", (sd.Fld_Src_Field).ToString().TrimEnd(','));
                            cmd.Parameters.AddWithValue("@Fld_Symbol", sd.currtyp.ToString().Trim());
                            cmd.Parameters.AddWithValue("@Fld_Length", sd.MaxLen.ToString().Trim());
                            cmd.Parameters.AddWithValue("@Fld_Mandatory", sd.Fld_Mandatory.ToString().Trim());
                            cmd.Parameters.AddWithValue("@Active_flag", sd.Active_flag.ToString().Trim());
                            cmd.Parameters.AddWithValue("@Fldtype", sd.Fldtype.ToString().Trim());
                            cmd.Parameters.AddWithValue("@Order_by", sd.Order_by.ToString().Trim());
                            cmd.Parameters.AddWithValue("@AccessPoint", sd.AccessPoint.ToString().Trim());
                            cmd.Parameters.AddWithValue("@SrtNo", Convert.ToInt32(sd.SrtNo));
                            cmd.Parameters.AddWithValue("@Control_id", Convert.ToInt32(sd.Control_id));
                            cmd.Parameters.AddWithValue("@cusxml", cusxml.ToString().Trim());
                            //cmd.Parameters.Add("@returnMessage", SqlDbType.NVarChar, 250).Direction = ParameterDirection.Output;
                            cmd.Parameters.Add("@returnMessage", SqlDbType.NVarChar, 250);
                            cmd.Parameters["@returnMessage"].Direction = ParameterDirection.Output;
                            cmd.ExecuteNonQuery();
                            msgs = (string)cmd.Parameters["@returnMessage"].Value;
                            con.Close();
                            ////cmd.Parameters.AddRange(parameters);                           
                            //int i = cmd.ExecuteNonQuery();
                            //msg = Convert.ToString(cmd.Parameters["@returnMessage"].Value);
                            //con.Close();

                            msg = Fld_Col + " " + Convert.ToString(msgs);
                        }
                        catch (Exception ex)
                        {
                            msg = Fld_Col + " " + ex.Message.ToString();
                        }
                        finally { if (con.State == ConnectionState.Open) { con.Close(); } con.Dispose(); }
                        //}
                    }
                }
                else
                {
                    using (SqlConnection con = new SqlConnection(Globals.ConnString))
                    {
                        //using (SqlCommand cmd = con.CreateCommand())
                        //{
                        try
                        {
                            con.Open();
                            SqlCommand cmd = new SqlCommand("Update_CustomForms_Fields", con);
                            //cmd.CommandText = "Update_CustomForms_Fields";
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@Div", sd.divcode.ToString());
                            cmd.Parameters.AddWithValue("@Fld_Id", FldId.ToString());
                            //cmd.Parameters.AddWithValue("@Fld_Id", sfcode.ToString());
                            cmd.Parameters.AddWithValue("@ModuleId", sd.ModuleId.ToString().Trim());
                            cmd.Parameters.AddWithValue("@Fld_Name", sd.FldName.ToString().Trim());
                            cmd.Parameters.AddWithValue("@Field_Col", Fld_Col.ToString().Trim());
                            cmd.Parameters.AddWithValue("@Fldtyp", sd.sfldtyp.ToString().Trim());
                            cmd.Parameters.AddWithValue("@Fld_Src_Name", sd.Fld_Src_Name.ToString().Trim());
                            cmd.Parameters.AddWithValue("@Fld_Src_Field", (sd.Fld_Src_Field).ToString().TrimEnd(','));
                            cmd.Parameters.AddWithValue("@Fld_Symbol", sd.currtyp.ToString().Trim());
                            cmd.Parameters.AddWithValue("@Fld_Length", sd.MaxLen.ToString().Trim());
                            cmd.Parameters.AddWithValue("@Fld_Mandatory", sd.Fld_Mandatory.ToString().Trim());
                            cmd.Parameters.AddWithValue("@Active_flag", sd.Active_flag.ToString().Trim());
                            cmd.Parameters.AddWithValue("@Fldtype", sd.Fldtype.ToString().Trim());
                            cmd.Parameters.AddWithValue("@Order_by", sd.Order_by.ToString().Trim());
                            cmd.Parameters.AddWithValue("@AccessPoint", sd.AccessPoint.ToString().Trim());
                            cmd.Parameters.AddWithValue("@SrtNo", Convert.ToInt32(sd.SrtNo));
                            cmd.Parameters.AddWithValue("@Control_id", sd.Control_id.ToString().Trim());
                            cmd.Parameters.AddWithValue("@cusxml", cusxml.ToString().Trim());
                            cmd.Parameters.Add("@returnMessage", SqlDbType.NVarChar, 250);
                            cmd.Parameters["@returnMessage"].Direction = ParameterDirection.Output;
                            cmd.ExecuteNonQuery();
                            msgs = (string)cmd.Parameters["@returnMessage"].Value;
                            con.Close();

                            msg = Fld_Col + " " + Convert.ToString(msgs);

                        }
                        catch (Exception ex)
                        {
                            msg = ex.Message.ToString();
                        }
                        finally { if (con.State == ConnectionState.Open) { con.Close(); } con.Dispose(); }
                        //}
                    }
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            return msg;
        }

        public class SaveCustomFormField
        {
            [JsonProperty("DivCode")]
            public string divcode { get; set; }

            [JsonProperty("FldID")]
            public object hfldid { get; set; }

            [JsonProperty("ModuleId")]
            public object ModuleId { get; set; }

            [JsonProperty("FldTyp")]
            public object sfldtyp { get; set; }

            [JsonProperty("FldName")]
            public string FldName { get; set; }

            [JsonProperty("Fld_Src_Name")]
            public object Fld_Src_Name { get; set; }

            [JsonProperty("Fld_Src_Field")]
            public object Fld_Src_Field { get; set; }

            [JsonProperty("Fld_Length")]
            public object MaxLen { get; set; }

            [JsonProperty("Fld_Symbol")]
            public object currtyp { get; set; }

            [JsonProperty("Fld_Mandatory")]
            public object Fld_Mandatory { get; set; }

            [JsonProperty("Active_flag")]
            public object Active_flag { get; set; }

            [JsonProperty("Order_by")]
            public object Order_by { get; set; }

            [JsonProperty("AccessPoint")]
            public object AccessPoint { get; set; }

            [JsonProperty("SrtNo")]
            public object SrtNo { get; set; }

            [JsonProperty("Control_id")]
            public object Control_id { get; set; }

            [JsonProperty("Fldtype")]
            public object Fldtype { get; set; }

            [JsonProperty("lctarget")]
            public object lctarget { get; set; }

            [JsonProperty("altqry")]
            public object altqry { get; set; }

            [JsonProperty("FGroupId")]
            public object FGroupId { get; set; }
        }

    }
}