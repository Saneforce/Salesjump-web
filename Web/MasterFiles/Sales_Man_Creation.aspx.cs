using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using System.Web.Services;
using Bus_EReport;
using System.Data;
using DBase_EReport;
using System.Data.SqlClient;

public partial class MasterFiles_Sales_Man_Creation : System.Web.UI.Page
{
    public string divCode = string.Empty;
    string sf_type = string.Empty;
    public static  string sf_code = string.Empty;
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
        else if (sf_type == "4")
        {
            this.MasterPageFile = "~/Master_DIS.master";
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        sf_code = Request.QueryString["sfcode"];
        hdsmcode.Value = sf_code;
    }

    [WebMethod(EnableSession=true)]
    public static string getstockist(string divcode)
    {
        Stockist st = new Stockist();
        DataSet ds = new DataSet();
        ds = st.getStockist_Name(divcode);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }

    [WebMethod(EnableSession=true)]
    public static string savedsm(string data){

		string msg=string.Empty;
        if(sf_code==null || sf_code=="") {

            //Bus_EReport.DSM.SaveDSM Data = JsonConvert.DeserializeObject<Bus_EReport.DSM.SaveDSM>(data);
            SaveDSM Data = JsonConvert.DeserializeObject<SaveDSM>(data);
            DSM dsm = new DSM();
            msg = insertDSM(Data);
            return msg;
        }
        else
        {
            //Bus_EReport.DSM.SaveDSM Data = JsonConvert.DeserializeObject<Bus_EReport.DSM.SaveDSM>(data);
            //DSM dsm = new DSM();
            // msg = updateDSM(Data,sf_code);
            SaveDSM Data = JsonConvert.DeserializeObject<SaveDSM>(data);
            msg = updateDSM(Data, sf_code);
            return msg;
        }
    }
    public static string updateDSM(SaveDSM sd, string sf_code)
    {
        DB_EReporting db_ER = new DB_EReporting();
        DataSet dss = new DataSet();
        string msg = string.Empty;
        string strQry = "exec updateDSM '" + sd.divcode + "','" + sf_code + "','" + sd.dname + "','" + sd.dtype + "','" + sd.status + "','" + sd.stype + "','" + sd.usrname + "','" + sd.pwd + "','" + sd.dist + "','" + sd.distname + "','" + sd.mobile + "','" + sd.email + "'";
        try
        {
            dss = db_ER.Exec_DataSet(strQry);
            msg = "success";
        }
        catch (Exception ex)
        {
            msg = ex.Message;
        }
        return msg;
    }
    public class SaveDSM
    {
        [JsonProperty("DivCode")]
        public object divcode { get; set; }

        [JsonProperty("DSMName")]
        public object dname { get; set; }

        [JsonProperty("DType")]
        public object dtype { get; set; }

        [JsonProperty("Status")]
        public object status { get; set; }

        [JsonProperty("Salestype")]
        public object stype { get; set; }

        [JsonProperty("UsrName")]
        public object usrname { get; set; }

        [JsonProperty("PWD")]
        public object pwd { get; set; }

        [JsonProperty("Dist")]
        public object dist { get; set; }

        [JsonProperty("Distname")]
        public object distname { get; set; }

        [JsonProperty("Email")]
        public object email { get; set; }

        [JsonProperty("Mobile")]
        public object mobile { get; set; }
    }
    public static string insertDSM(SaveDSM sd)
   {
        DB_EReporting db_ER = new DB_EReporting();
        DataSet dss = new DataSet();

        //strQry = "exec insertDSM '" + sd.divcode + "','" + sd.dname + "','" + sd.dtype + "','" + sd.status + "','" + sd.stype + "','" + sd.usrname + "','" + sd.pwd + "','" + sd.dist + "','" + sd.distname + "','" + sd.mobile + "','" + sd.email + "'";
        //try
        // {
        //   dss = db_ER.Exec_DataSet(strQry);
        // }
        //catch (Exception ex)
        //{
        //   throw ex;
        //}
        DB_EReporting db = new DB_EReporting();

        string msg = string.Empty;
        //using (SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Ereportcon"].ToString()))
        //{
        //    using (SqlCommand cmd = con.CreateCommand())
        //    {
        //        cmd.CommandType = CommandType.StoredProcedure; ;
        //        cmd.CommandText = "insertDSM";
        //        SqlParameter[] parameters = new SqlParameter[]
        //                {
        //                                new SqlParameter("@Div", sd.divcode),
        //                                new SqlParameter("@dname", sd.dname),
        //                                new SqlParameter("@dtype", sd.dtype),
        //                                new SqlParameter("@status", sd.status),
        //                                new SqlParameter("@stype", sd.stype),
        //                                new SqlParameter("@usrname", sd.usrname),
        //                                new SqlParameter("@pwd",sd.pwd),
        //                                new SqlParameter("@stkcode", sd.dist),
        //                                new SqlParameter("@stkname",sd.distname),
        //                                new SqlParameter("@mobile",sd.mobile),
        //                                new SqlParameter("@email",sd.email)

        //                };
        //        cmd.Parameters.AddRange(parameters);
                try
                {
                    //if (con.State != ConnectionState.Open)
                    //{
                    //    con.Open();
                    //}
                    //cmd.ExecuteNonQuery();
                    string result = "";
                    string sqlQry = "exec insertDSM '" + sd.divcode + "','" + sd.dname + "','" + sd.dtype + "','" + sd.status + "','" + sd.stype + "','" + sd.usrname + "'," +
                        "'" + sd.pwd + "','" + sd.dist + "','" + sd.distname + "','" + sd.mobile + "','" + sd.email + "'";
                     result=db.Exec_DataTable(sqlQry).Rows[0][0].ToString();
                    if(result!="")
                        msg = "Success";
                }
                catch (Exception ex)
                {
                    msg = ex.Message;
                }
            return msg;
    }


    [WebMethod(EnableSession=true)]
    public static string fillData(string divcode, string dsmcode)
    {
        DSM dsm = new DSM();
        DataSet ds = new DataSet();
        ds = dsm.getSalesman(divcode, dsmcode);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    [WebMethod(EnableSession = true)]
    public static List<string> GetDesignDetails(string prefix)
    {
        string Stockist_Code = HttpContext.Current.Session["Sf_Code"].ToString();
        string Div_Code = HttpContext.Current.Session["div_code"].ToString();
        List<string> design = new List<string>();
        DataSet dataSet = null;
        StockistMaster sm = new StockistMaster();
        dataSet = sm.GetDesignName(prefix);
        if (dataSet.Tables[0].Rows.Count > 0)
        {
            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                design.Add(row["Desig_Name"].ToString());
            }
        }
        return design;
    }
    public class Desingation
    {
        public string label { get; set; }
        public string value { get; set; }
        public string id { get; set; }

    }

    [WebMethod(EnableSession = true)]
    public static Desingation[] GetDesgnName()
    {
        string div_code = "";
        div_code = HttpContext.Current.Session["div_code"].ToString();
        List<Desingation> HDay = new List<Desingation>();
        DataSet ds = null;
        StockistMaster sm = new StockistMaster();

        ds = sm.GetDesignAllName(div_code.TrimEnd(','));

        foreach (DataRow row in ds.Tables[0].Rows)
        {
            Desingation d = new Desingation();
            d.label = row["Desig_Name"].ToString();
            d.value = row["Desig_Name"].ToString();
            d.id = row["Desig_id"].ToString();
            HDay.Add(d);
        }
        return HDay.ToArray();
    }

}