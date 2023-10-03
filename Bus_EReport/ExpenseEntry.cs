using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DBase_EReport;
using System.Configuration;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.Data.SqlClient;

namespace Bus_EReport
{
    public class ExpenseEntry
    {
        private string strQry = string.Empty;

        public DataSet getEmptyTerritory()
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            strQry = " SELECT TOP 10 '' Territory_Name,'' Territory_SName " +
                     " FROM  sys.tables ";
            try
            {
                dsProCat = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProCat;
        }
        
public class SaveTravelMode
        {
            [JsonProperty("DivCode")]
            public string divcode { get; set; }

            [JsonProperty("Mode")]
            public object mode { get; set; }

            [JsonProperty("Meter")]
            public object meter { get; set; }

            [JsonProperty("Driver")]
            public object driver { get; set; }

            [JsonProperty("Echange")]
            public object echange { get; set; }

            [JsonProperty("Allowance")]
            public object allowance { get; set; }

            [JsonProperty("GLCode")]
            public object glcode { get; set; }

            [JsonProperty("fuelEffDate")]
            public object effdate { get; set; }

            [JsonProperty("Grade")]
            public object grade { get; set; }

            [JsonProperty("ExceptionNd")]
            public object excepND { get; set; }

            [JsonProperty("chargekm")]
            public object chargekm { get; set; }

            [JsonProperty("fielarr")]
            public List<fuelforfield> fielarr { get; set; }
        }
        public class fuelforfield
        {
            [JsonProperty("scode")]
            public string snme { get; set; }

            [JsonProperty("fieval")]
            public string ffstnv { get; set; }

            [JsonProperty("fieldhead")]
            public string fieldof { get; set; }
        }
        public string SaveNewTravelMode(SaveTravelMode sd)
        {
            string msg = string.Empty;
            List<fuelforfield> a = sd.fielarr;
            string sxml = "<ROOT>";
            if (a != null)
            {
                for (int i = 0; i < a.Count; i++)
                {
                    if (a[i].snme != "" && a[i].snme != null)
                    {
                        sxml += "<ASSD stype=\"field\" scode=\"" + a[i].snme + "\" fieval=\"" + a[i].ffstnv + "\" fieldhead=\"" + a[i].fieldof + "\" />";
                    }
                }
            }                       
            sxml += "</ROOT>";
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                     {
                        new SqlParameter("@Divcode", sd.divcode),
                        new SqlParameter("@Mode", sd.mode),
                        new SqlParameter("@Meter", sd.meter),
                        new SqlParameter("@Driver", sd.driver),
                        new SqlParameter("@Echange", sd.echange),
                        new SqlParameter("@Allowance", (sd.allowance.ToString()).TrimEnd(',')),
                        new SqlParameter("@Effdate", sd.effdate.ToString()),
                        new SqlParameter("@sxml", sxml),
                        new SqlParameter("@GLCode", sd.glcode.ToString()),
                        new SqlParameter("@Grade", sd.grade.ToString()),
                        new SqlParameter("@ExcepND", sd.excepND.ToString()),
                        new SqlParameter("@chargekm", sd.chargekm.ToString())
                     };
                using (SqlConnection con = new SqlConnection(Global.ConnString))
                {
                    using (SqlCommand cmd = con.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "insertTravelMode";
                        cmd.Parameters.AddRange(parameters);

                        try
                        {
                            if (con.State != ConnectionState.Open)
                            {
                                con.Open();
                            }

                            cmd.ExecuteNonQuery();
                            msg = "Success";
                        }
                        catch (Exception ex)
                        {
                            throw ex;
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
        public int DeActivateTravel(string plcode, string stus)
        {
            int iReturn = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();
                strQry = "update Mas_Modeof_Travel set Active_Flag = '" + stus + "' where Sl_No = '" + plcode + "'";
                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }
        public DataSet getTravel_ModeFields(string Div)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "exec getTravel_ModeFields " + Div + "";

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
		public DataSet getDesignationgroup_div(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;

            strQry = " exec getDesignationGroup " + div_code.TrimEnd(',') + "";
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
        
        public DataSet getStatewisePricing(string slno)
        {
            DB_EReporting db = new DB_EReporting();
            DataSet ds = null;
            strQry = "exec getStatewiseFuel "+ slno + "";
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
