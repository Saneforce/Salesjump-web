using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web;
using Newtonsoft.Json;

namespace DBase_EReport
{
   public class DB_EReporting
    {
        private string ClientID="";
        private string strConn = Global.ConnString;
        private int iReturn = -1;
        private string iReturn1 = "";
        public DB_EReporting( ) {

            //Console.WriteLine(Global.ConnString);

           /* string mClientId = HttpContext.Current.Request.Url.Host.ToLower().Replace("www.", "").Replace(".sanfmcg.com", "").ToLower();
            if (mClientId=="shivatex") strConn=ConfigurationManager.ConnectionStrings["Ereportcon_shivatex"].ConnectionString;
            if (mClientId == "arasan") strConn = ConfigurationManager.ConnectionStrings["Ereportcon_araran"].ConnectionString;
            if (mClientId == "trident") strConn = ConfigurationManager.ConnectionStrings["Ereportcon_Trident"].ConnectionString;
            if (mClientId == "avantika") strConn = ConfigurationManager.ConnectionStrings["Ereportcon_Avantika"].ConnectionString;
            if (mClientId == "organomix") strConn = ConfigurationManager.ConnectionStrings["Ereportcon_Organomix"].ConnectionString;
            if (mClientId == "marie") strConn = ConfigurationManager.ConnectionStrings["Ereportcon_Marie"].ConnectionString;
            if (mClientId == "tiesar") strConn = ConfigurationManager.ConnectionStrings["Ereportcon_TSR"].ConnectionString;
            if (mClientId == "easysol") strConn = ConfigurationManager.ConnectionStrings["Ereportcon_easysol"].ConnectionString;
            if (mClientId == "durga") strConn = ConfigurationManager.ConnectionStrings["Ereportcon_durga"].ConnectionString;
			if (mClientId == "pgkala") strConn = ConfigurationManager.ConnectionStrings["Ereportcon_pgkala"].ConnectionString; 
			if (mClientId == "pgdb") strConn = ConfigurationManager.ConnectionStrings["Ereportcon_pgdb"].ConnectionString; */


        }
        // public static object HttpContext { get; private set; }
        public DataTable Exec_DataTable(string strQry)
        {
            DataSet ds_EReport = null;
            DataTable dt_EReport = null;

            try
            {
                ds_EReport = Exec_DataSet(strQry);
                dt_EReport = ds_EReport.Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dt_EReport;
        }

        
        public DataSet Exec_DataSet(string strQry)
        {
            DataSet ds_EReport = new DataSet();

            SqlConnection _conn = new SqlConnection();
            try
            {
                                
                 using (_conn = new SqlConnection(strConn))
                {
                    SqlCommand selectCMD = new SqlCommand(strQry, _conn);
                    selectCMD.CommandTimeout = 300;
                    SqlDataAdapter da_EReport = new SqlDataAdapter();
                    da_EReport.SelectCommand = selectCMD;

                    if (_conn.State == ConnectionState.Closed)
                    {
                        _conn.Open();
                
                    }
					
                    da_EReport.Fill(ds_EReport, "Customers");       
					 _conn.Close();
                }

               // _conn.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (_conn.State == ConnectionState.Open)
                {
                    _conn.Close();
                }
                _conn.Dispose();
            }
            return ds_EReport;
        }

        public DataSet Exec_DataSet(string strQry, SqlCommand cmd)
        {
            DataSet ds_EReport = new DataSet();
            //SqlConnection _conn = new SqlConnection(strConn);
            SqlConnection _conn = new SqlConnection();
            try
            {
                using(_conn = new SqlConnection(strConn))
                {
                    //SqlCommand selectCMD = new SqlCommand(strQry, _conn);
                    cmd.Connection = _conn;
                    cmd.CommandText = strQry;
                    //cmd.CommandTimeout = 30;
                    cmd.CommandTimeout = 300;

                    SqlDataAdapter da_EReport = new SqlDataAdapter();
                    da_EReport.SelectCommand = cmd;

                    if (_conn.State == ConnectionState.Closed)
                    {
                        _conn.Open();                       

                    }
					da_EReport.Fill(ds_EReport, "Customers");
                    
                    _conn.Close();
                }                
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (_conn.State == ConnectionState.Open)
                {
                    _conn.Close();
                }
                _conn.Dispose();
            }
            return ds_EReport;
        }

        public int Exec_Scalar(string strQry)
        {
            SqlConnection _conn = new SqlConnection();
            try
            {
                iReturn = -1;

                using(_conn = new SqlConnection(strConn))
                {
                    // SqlConnection _conn = new SqlConnection(strConn);
                    SqlCommand selectCMD = new SqlCommand(strQry, _conn);
                    selectCMD.CommandTimeout = 30;
                    if (_conn.State == ConnectionState.Closed)
                    {
                        _conn.Open();
                    }
                   
                    iReturn = Convert.ToInt32(selectCMD.ExecuteScalar());
                    _conn.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (_conn.State == ConnectionState.Open)
                {
                    _conn.Close();
                }
                _conn.Dispose();
            }
            return iReturn;
        }

        public string Exec_Scalar_s(string strQry)
        {
            SqlConnection _conn = new SqlConnection();
            try
            {
                iReturn1 = "";

                using (_conn = new SqlConnection(strConn))
                {
                    // SqlConnection _conn = new SqlConnection(strConn);
                    SqlCommand selectCMD = new SqlCommand(strQry, _conn);
                    selectCMD.CommandTimeout = 30;
                    if (_conn.State == ConnectionState.Closed)
                    {
                        _conn.Open();
                    }
                    
                    iReturn1 = Convert.ToString(selectCMD.ExecuteScalar());
                    _conn.Close();
                }                               
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (_conn.State == ConnectionState.Open)
                {
                    _conn.Close();
                }
                _conn.Dispose();
            }
            return iReturn1;
        }

       public int Exec_Scalar(string strQry, SqlCommand cmd)
        {
            SqlConnection _conn = new SqlConnection();
            try
            {
                iReturn = -1;
                using (_conn = new SqlConnection(strConn))
                {
                    //SqlConnection _conn = new SqlConnection(strConn);
                    //SqlCommand selectCMD = new SqlCommand(strQry, _conn);
                    cmd.Connection = _conn;
                    cmd.CommandText = strQry;
                    cmd.CommandTimeout = 30;

                    if (_conn.State == ConnectionState.Closed)
                    {
                        _conn.Open();
                       
                    }
					 iReturn = Convert.ToInt32(cmd.ExecuteScalar());
                    _conn.Close();
                }
                //_conn.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (_conn.State == ConnectionState.Open)
                {
                    _conn.Close();
                }
                _conn.Dispose();
            }
            return iReturn;
        }

         public int ExecQry(string sQry)
        {
            iReturn = -1;

            using (SqlConnection _conn = new SqlConnection(strConn))
            {
                try
                {                    
                    System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(sQry, _conn);
                    cmd.CommandType = CommandType.Text;
                    if (_conn.State == ConnectionState.Closed)
                    {
                        _conn.Open();
                        
                    }
					iReturn = cmd.ExecuteNonQuery();
                       
                    _conn.Close();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (_conn.State == ConnectionState.Open)
                    {
                        _conn.Close();
                    }
                    _conn.Dispose();
                }
            }

            return iReturn;
        }


        public int ExecQry(string sQry, SqlCommand cmd)
        {
            iReturn = -1;
            using (SqlConnection _conn = new SqlConnection(strConn))
            {
                try
                {
                    cmd.Connection = _conn;
                    cmd.CommandText = sQry;

                    if (_conn.State == ConnectionState.Closed)
                    {
                        _conn.Open();
                                            
                    }
					iReturn = cmd.ExecuteNonQuery();  
					  _conn.Close();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (_conn.State == ConnectionState.Open)
                    {
                        _conn.Close();
                    }
                    _conn.Dispose();
                }
            }
            return iReturn;
        }
		
        public int ExecQry(string sQry, SqlConnection _conn, SqlTransaction tran)
        {
            iReturn = -1;
            try
            {
                using (_conn = new SqlConnection(strConn))
                {
                    //SqlConnection _conn = new SqlConnection(strConn);
                    System.Data.SqlClient.SqlCommand cmd;
                    cmd = new System.Data.SqlClient.SqlCommand();
                    cmd.Connection = _conn;
                    cmd.CommandText = sQry;
                    cmd.Transaction = tran;
                    //_conn.Open();
                    if (_conn.State == ConnectionState.Closed)
                    {
                        _conn.Open();
                    }
                    //_conn.Open();
                    iReturn = cmd.ExecuteNonQuery();
                    _conn.Close();
                    
                }

                //_conn.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (_conn.State == ConnectionState.Open)
                { _conn.Close(); }
                _conn.Dispose();
            }
            return iReturn;
        }

       public int Exec_Scalar(string strQry, SqlConnection _conn, SqlTransaction tran)
        {
            try
            {
                iReturn = -1;
                using (_conn = new SqlConnection(strConn))
                {
                    //SqlConnection _conn = new SqlConnection(strConn);
                    SqlCommand selectCMD = new SqlCommand(strQry, _conn, tran);
                    selectCMD.CommandTimeout = 30;
                    if (_conn.State == ConnectionState.Closed)
                    {
                        _conn.Open();
                    }
                    //_conn.Open();
                    iReturn = Convert.ToInt32(selectCMD.ExecuteScalar());
                    _conn.Close();
                }
                //_conn.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (_conn.State == ConnectionState.Open)
                { _conn.Close(); }
                _conn.Dispose();
            }
            return iReturn;
        }
		
        internal DataTable Exec_DataTableWithParam(string CommandName, CommandType cmdType, SqlParameter[] param)
        {
            DataTable table = new DataTable();

            using (SqlConnection con = new SqlConnection(strConn))
            {
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandType = cmdType;
                    cmd.CommandText = CommandName;
                    cmd.Parameters.AddRange(param);

                    try
                    {
                        if (con.State == ConnectionState.Closed)
                        {
                            con.Open();
                        }
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(table);
                        }
						con.Close();
                    }
                    catch
                    {
                        throw;
                    }
                    finally { if (con.State == ConnectionState.Open) { con.Close(); } con.Dispose(); }
                }
            }

            return table;
        }

        internal bool Exec_NonQueryWithParam(string CommandName, CommandType cmdType, SqlParameter[] pars)
        {
            int result = 0;

            using (SqlConnection con = new SqlConnection(strConn))
            {
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandType = cmdType;
                    cmd.CommandText = CommandName;
                    cmd.Parameters.AddRange(pars);

                    try
                    {
                        if (con.State == ConnectionState.Closed)
                        {
                            con.Open();
                        }
                        
                        result = cmd.ExecuteNonQuery();
                        con.Close();
                    }
                    catch
                    {
                        throw;
                    }
                    finally { if (con.State == ConnectionState.Open) { con.Close(); } con.Dispose(); }
                }
            }

            return (result > 0);
        }
        
         public bool Exec_NonQueryWithParamNew(string CommandName, CommandType cmdType, SqlParameter[] pars)
        {
            int result = 0;

            using (SqlConnection con = new SqlConnection(strConn))
            {
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandType = cmdType;
                    cmd.CommandText = CommandName;
                    cmd.Parameters.AddRange(pars);

                    try
                    {
                        if (con.State == ConnectionState.Closed)
                        {
                            con.Open();
                        }
						 result = cmd.ExecuteNonQuery();
						con.Close();
                    }
                    catch
                    {
                        throw;
                    }
                    finally { if (con.State == ConnectionState.Open) { con.Close(); } con.Dispose(); }
                }
            }

            return (result > 0);
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


        public int Exec_QueryWithParamNew(string CommandName, CommandType cmdType, SqlParameter[] pars)
        {
            int result = 0;

            using (SqlConnection con = new SqlConnection(strConn))
            {
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandType = cmdType;
                    cmd.CommandText = CommandName;
                    cmd.Parameters.AddRange(pars);

                    try
                    {
                        if (con.State == ConnectionState.Closed)
                        {
                            con.Open();
                        }
                        result = cmd.ExecuteNonQuery();
                        con.Close();
                    }
                    catch
                    {
                        throw;
                    }
                    finally { if (con.State == ConnectionState.Open) { con.Close(); } con.Dispose(); }
                }
            }

            return result;
        }

         
        public string saveCustomFormFields(SaveCustomFormField sd, string sfcode, string cusxml)
        {         

            string msg = string.Empty;
                      

            try
            {                
                string FldId = Convert.ToString(sd.hfldid);
                string fldname = sd.FldName.ToString();
                string Fld_Col = "";

                if (fldname.Contains(" "))
                { Fld_Col = fldname.Replace(" ", "_") + "_" + sd.divcode.ToString().Trim(); }
                else { Fld_Col = fldname + "_" + sd.divcode.ToString().Trim(); }

                string SrtNo = sd.SrtNo.ToString();

                if ((SrtNo==""||SrtNo==null))
                { sd.SrtNo = "0"; }

                if ((FldId == "" || FldId == null || FldId == "0"))
                {
                    using (SqlConnection con = new SqlConnection(strConn))
                    {
                        using (SqlCommand cmd = con.CreateCommand())
                        {
                            try
                            {
                                con.Open();

                                cmd.CommandText = "Create_CustomForms_Fields";
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
                                cmd.Parameters.AddWithValue("@SrtNo",Convert.ToInt32(sd.SrtNo));
                                cmd.Parameters.AddWithValue("@Control_id", sd.Control_id.ToString().Trim());
                                cmd.Parameters.AddWithValue("@cusxml", cusxml.ToString().Trim());
                                cmd.Parameters.Add("@returnMessage", SqlDbType.NVarChar, 150).Direction = ParameterDirection.Output;
                                //cmd.Parameters.AddRange(parameters);
                                cmd.ExecuteNonQuery();
                                con.Close();

                                msg = Convert.ToString(cmd.Parameters["@returnMessage"].Value);
                            }
                            catch (Exception ex)
                            {
                                msg = ex.Message.ToString();
                            }
							finally
						{
							 if (con.State == ConnectionState.Open)
							 {
								 con.Close();
							 }
							 con.Dispose();
						}
                        }
                    }
                }
                else
                {
                    using (SqlConnection con = new SqlConnection(strConn))
                    {
                        using (SqlCommand cmd = con.CreateCommand())
                        {
                            try
                            {
                                con.Open();

                                cmd.CommandText = "Update_CustomForms_Fields";
                                
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
                                cmd.Parameters.Add("@returnMessage", SqlDbType.NVarChar, 150).Direction = ParameterDirection.Output;
                                //cmd.Parameters.AddRange(parameters);
                                cmd.ExecuteNonQuery();
                                con.Close();


                                msg = Convert.ToString(cmd.Parameters["@returnMessage"].Value);
                            }
                            catch (Exception ex)
                            {
                                msg = ex.Message.ToString();
                            }
							finally
						{
							 if (con.State == ConnectionState.Open)
							 {
								 con.Close();
							 }
							 con.Dispose();
						}
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            return msg;
        }

       public string Outletsmerging(string divcode, string sfcode,string SfRoute, string FromListedDrCode,string ToListedDrCode)
        {
            string msg = string.Empty;
            try
            {
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    using (SqlCommand cmd = con.CreateCommand())
                    {
                        try
                        {
                            con.Open();

                            cmd.CommandText = "Pro_OuletsMerging";
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@DivCode",Convert.ToString(divcode));
                            cmd.Parameters.AddWithValue("@Sf_Code", Convert.ToString(sfcode));
                            cmd.Parameters.AddWithValue("@SfRoute", Convert.ToString(SfRoute));
                            cmd.Parameters.AddWithValue("@FromListedDrCode", Convert.ToString(FromListedDrCode));
                            cmd.Parameters.AddWithValue("@ToListedDrCode", Convert.ToString(ToListedDrCode));                           
                            cmd.Parameters.Add("@returnMessage", SqlDbType.NVarChar, 150).Direction = ParameterDirection.Output;
                            //cmd.Parameters.AddRange(parameters);
                            cmd.ExecuteNonQuery();
                            con.Close();

                            msg = Convert.ToString(cmd.Parameters["@returnMessage"].Value);
                        }
                        catch (Exception ex)
                        {
                            msg = ex.Message.ToString();
                        }
						finally
						{
							 if (con.State == ConnectionState.Open)
							 {
								 con.Close();
							 }
							 con.Dispose();
						}
                    }
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            return msg;
        }

    }
}
