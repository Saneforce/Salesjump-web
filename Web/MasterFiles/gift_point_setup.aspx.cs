using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using Bus_EReport;
using DocumentFormat.OpenXml.Drawing;
using System.IO;
using System.Data.SqlClient;
using Newtonsoft.Json;
using System.Web.Services;
using DBase_EReport;

public partial class MasterFiles_gift_point_setup : System.Web.UI.Page
{
    DataTable dsProd1 = null;
    DataSet dsProd = null;
    DataSet dsProduct = null;
    DataSet dsDivision = null;
    DataSet dsState = null;
    string divcode = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    [WebMethod(EnableSession = true)]
    public static string Filldata(string divcode)
    {
        proddtl dv = new proddtl();
        DataSet dsProd = dv.get_gftpoint_all(divcode);
        return JsonConvert.SerializeObject(dsProd.Tables[0]);
    }
    [WebMethod(EnableSession = true)]
    public static string Savepoint(string Data, string divcode)
    {
        var items = JsonConvert.DeserializeObject<List<proddtl>>(Data);
        string div_code = HttpContext.Current.Session["div_code"].ToString();
        Product prd = new Product();

        DataSet ds_EReport = new DataSet();
        string sxml = "<ROOT>";
        for (int k = 0; k < items.Count; k++)
        {
            if (items[k].point != "" || items[k].pval != "" || items[k].pnam != "") 
            {
                sxml += "<ASSD point=\"" + items[k].point + "\" pval=\"" + items[k].pval + "\" pnam=\"" + items[k].pnam + "\" />";
            }
        }
        sxml += "</ROOT>";
        string consString = Globals.ConnString;
        using (SqlConnection con = new SqlConnection(consString))
        {
            using (SqlCommand cmd = con.CreateCommand())
            {

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "insertMasgiftpoint";

                SqlParameter[] parameters = new SqlParameter[]
                {
                                        new SqlParameter("@sxml", sxml),
                                        new SqlParameter("@divcode", divcode),
                };
                cmd.Parameters.AddRange(parameters);

                try
                {
                    if (con.State != ConnectionState.Open)
                    {
                        con.Open();
                    }
                    cmd.ExecuteNonQuery();
                }
                catch
                {
                    throw;
                }


            }
        }
        return "Success";
    }
    public class proddtl
    {
        public string point { get; set; }
        public string pval { get; set; }
        public string pnam { get; set; }
        public DataSet get_gftpoint_all(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocCat = null;


            string strQry = " EXEC get_gift_point '" + divcode + "' ";

            try
            {
                dsDocCat = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsDocCat;
        }

    }
}