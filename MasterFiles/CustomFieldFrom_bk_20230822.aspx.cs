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

public partial class MasterFiles_CustomFieldFrom_bk_20230822 : System.Web.UI.Page
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
        DB_EReporting.SaveCustomFormField Data = JsonConvert.DeserializeObject<DB_EReporting.SaveCustomFormField>(Formdata);
        DB_EReporting Ad = new DB_EReporting();
        msg = Ad.saveCustomFormFields(Data, SfCode, cusxml);
        return msg;
    }

    [WebMethod]
    public static string AddCustomForm(string ModuleName, string ModuleType, string ModuleCate)
    {        
        string msg = string.Empty;
        msg = saveCustomForms(ModuleName, ModuleType, ModuleCate);
        return msg;
    }

    public static string saveCustomForms(string ModuleName, string ModuleType, string ModuleCate)
    {
        string msg = string.Empty;

        try
        {
            using (SqlConnection con = new SqlConnection(Globals.ConnString))
            {
                using (SqlCommand cmd = con.CreateCommand())
                {
                    try
                    {
                        con.Open();
                        cmd.CommandText = "Create_CustomForms";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ModuleName", ModuleName.ToString().Trim());
                        cmd.Parameters.AddWithValue("@ModuleType", ModuleType.ToString().Trim());
                        cmd.Parameters.AddWithValue("@ModuleCate", ModuleCate.ToString().Trim());
                        cmd.Parameters.Add("@returnMessage", SqlDbType.NVarChar, 150).Direction = ParameterDirection.Output;
                        //cmd.Parameters.AddRange(parameters);
                        cmd.ExecuteNonQuery();
                        con.Close();

                        msg = Convert.ToString(cmd.Parameters["@returnMessage"].Value);

                        if (msg == "New Form Module Created")
                        {

                            string codefile = "" + ModuleName.Trim() + ".aspx.cs";
                            string inherit = HttpContext.Current.Server.MapPath("" + ModuleName.Trim() + "");

                            string[] aspxLines = {@"<%@ Page Language=""C#"" AutoEventWireup=""true"" CodeFile="+ModuleName.Trim()+" Inherits="+inherit+" %>",
                                "<!DOCTYPE html>",
                                "<head>",
                                "<title>The New Page</title>",
                                "</head>",
                                "<body>",
                                @"<form id=""form1"" runat=""server"">",
                                "<div>",
                                @"<asp:literal id=""output"" runat=""server"" />",
                                "</div>",
                                "</form>",
                                "</body>",
                                "</html>"};
                            string[] csLines = {
                                "using System;",
                                "using System.Collections.Generic;",
                                "using System.Linq;",
                                "using System.Web;",
                                "using System.Web.UI;",
                                "using System.Web.UI.WebControls;",
                                "using System.Data;",
                                "using Bus_EReport;",
                                "using System.Data.SqlClient;",
                                "using System.Web.Services;",
                                "using System.Configuration;",
                                "using Newtonsoft.Json;",
                                "using System.Globalization;",
                                "using ClosedXML.Excel;",
                                "using System.Xml;",
                                "using System.IO;",
                                "using System.Data.OleDb;",
                                "using DBase_EReport;",

                                "public partial class " + inherit.Trim()+ " : System.Web.UI.Page {",
                                "protected void Page_Load(object sender, EventArgs e) {","output.Text=;",  "}","}"
                            };
                            File.WriteAllLines(HttpContext.Current.Server.MapPath("" + ModuleName.Trim() + ".aspx"), aspxLines);
                            File.WriteAllLines(HttpContext.Current.Server.MapPath("" + ModuleName.Trim() + ".aspx.cs"), csLines);
                            msg = "New Form Module Created";
                        }

                    }
                    catch (Exception ex)
                    {
                        msg = ex.Message.ToString();
                    }
                    finally { if (con.State == ConnectionState.Open) { con.Close(); } con.Dispose(); }
                }
            }
        }
        catch (Exception ex)
        { msg = ex.Message.ToString(); }
        return msg;
    }

    [WebMethod]
    public static string GetCustomModuleList(string divcode)
    {
        DataSet ds = new DataSet();
        
        ds = getmodulelist(divcode);
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
}