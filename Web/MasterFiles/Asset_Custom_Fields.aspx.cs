using Bus_EReport;
using DBase_EReport;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterFiles_Asset_Custom_Fields : System.Web.UI.Page
{
    string sf_type = string.Empty;
    public static string sf_code = string.Empty;
    public string frm_id = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        frm_id = Request.QueryString["FrmID"];
        sf_code = Session["Sf_Code"].ToString();
    }
    [WebMethod]
    public static string AddCustomField(string Formdata, string sf_Code, string cusxml)
    {
        string msg = string.Empty;
        string SfCode = sf_Code + "_" + DateTime.Now.Ticks.ToString();
        cusffclass ad = new cusffclass();
        cusffclass.SaveCustomFormField Data = JsonConvert.DeserializeObject<cusffclass.SaveCustomFormField>(Formdata);
        msg = ad.saveCustomFormFields(Data, SfCode, cusxml);
        return msg;
    }
    [WebMethod]
    public static string GetCustomFormsFieldsList(string divcode, string ModuleId)
    {
        DataSet ds = new DataSet();
        cusffclass Ad = new cusffclass();
        ds = Ad.GetCustomFormsFieldsData(divcode, ModuleId);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    [WebMethod(EnableSession = true)]
    public static int SetNewStatus(string ID, string stus)
    {
        cusffclass ast = new cusffclass();
        int iReturn = ast.DeActivate(ID, stus);
        return iReturn;
    }
    [WebMethod]
    public static string GetCustomFormsFieldsDataById(string divcode, string FieldId)
    {
        DataSet ds = new DataSet();
        cusffclass Ad = new cusffclass();
        ds = Ad.GetCustomFormsFieldsDataById(divcode, FieldId);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    public class cusffclass
    {
        public DataSet GetCustomFormsFieldsDataById(string divcode, string FieldId)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            string strQry = "EXEC [Get_CustomAssetFields_ById] '" + divcode + "' ,'" + FieldId + "' ";

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
        public int DeActivate(string plcode, string stus)
        {
            int iReturn = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();
                string strQry = "update Asset_Custom_Fields_Details set flag='" + stus + "' where Field_Id='" + plcode + "'";
                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }
        public DataSet GetCustomFormsFieldsData(string divcode, string ModeleId)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            string strQry = "EXEC [Get_Asset_CustomFields] '" + divcode + "' ,'" + ModeleId + "' ";

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
        public string saveCustomFormFields(SaveCustomFormField sd, string sfcode, string cusxml)
        {

            string msg = string.Empty;
            string msgs = string.Empty;

            try
            {
                string FldId = Convert.ToString(sd.hfldid);
                string fldname = sd.FldName.ToString();
                string Fld_Col = "";

                Fld_Col = "Fld" + sd.ModuleId.ToString().Trim() + "" + sfcode.ToString();

                string SrtNo = Convert.ToString(sd.SrtNo);

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
                        try
                        {
                            con.Open();
                            SqlCommand cmd = new SqlCommand("Create_Custom_Fields", con);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@Div", sd.divcode.ToString());
                            cmd.Parameters.AddWithValue("@Fld_Id", sfcode.ToString());
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
                            cmd.Parameters.AddWithValue("@Control_id", Convert.ToInt32(sd.Control_id));
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
                        try
                        {
                            //con.Open();
                            //SqlCommand cmd = new SqlCommand("Update_CustomForms_Fields", con);
                            //cmd.CommandType = CommandType.StoredProcedure;
                            //cmd.Parameters.AddWithValue("@Div", sd.divcode.ToString());
                            //cmd.Parameters.AddWithValue("@Fld_Id", FldId.ToString());
                            //cmd.Parameters.AddWithValue("@ModuleId", sd.ModuleId.ToString().Trim());
                            //cmd.Parameters.AddWithValue("@Fld_Name", sd.FldName.ToString().Trim());
                            //cmd.Parameters.AddWithValue("@Field_Col", Fld_Col.ToString().Trim());
                            //cmd.Parameters.AddWithValue("@Fldtyp", sd.sfldtyp.ToString().Trim());
                            //cmd.Parameters.AddWithValue("@Fld_Src_Name", sd.Fld_Src_Name.ToString().Trim());
                            //cmd.Parameters.AddWithValue("@Fld_Src_Field", (sd.Fld_Src_Field).ToString().TrimEnd(','));
                            //cmd.Parameters.AddWithValue("@Fld_Symbol", sd.currtyp.ToString().Trim());
                            //cmd.Parameters.AddWithValue("@Fld_Length", sd.MaxLen.ToString().Trim());
                            //cmd.Parameters.AddWithValue("@Fld_Mandatory", sd.Fld_Mandatory.ToString().Trim());
                            //cmd.Parameters.AddWithValue("@Active_flag", sd.Active_flag.ToString().Trim());
                            //cmd.Parameters.AddWithValue("@Fldtype", sd.Fldtype.ToString().Trim());
                            //cmd.Parameters.AddWithValue("@Order_by", sd.Order_by.ToString().Trim());
                            //cmd.Parameters.AddWithValue("@AccessPoint", sd.AccessPoint.ToString().Trim());
                            //cmd.Parameters.AddWithValue("@SrtNo", Convert.ToInt32(sd.SrtNo));
                            //cmd.Parameters.AddWithValue("@Control_id", sd.Control_id.ToString().Trim());
                            //cmd.Parameters.AddWithValue("@cusxml", cusxml.ToString().Trim());
                            //cmd.Parameters.Add("@returnMessage", SqlDbType.NVarChar, 250);
                            //cmd.Parameters["@returnMessage"].Direction = ParameterDirection.Output;
                            //cmd.ExecuteNonQuery();
                            //msgs = (string)cmd.Parameters["@returnMessage"].Value;
                            //con.Close();

                            msg = Fld_Col + " " + Convert.ToString(msgs);

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

            //[JsonProperty("FGroupId")]
            //public object FGroupId { get; set; }
        }
    }
}