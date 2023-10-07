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

public partial class MasterFiles_addwtype : System.Web.UI.Page
{
    public static string Div_Code = string.Empty;
    public static string wcode = string.Empty;

   

    protected void Page_Load(object sender, EventArgs e)
    {
        Div_Code = Session["Div_Code"].ToString();
        wcode = Request.QueryString["wcode"];
    }
    [WebMethod(EnableSession = true)]
    public static string savewtype(string data)
    {
        string msg = string.Empty;
        Savewtyp Data = JsonConvert.DeserializeObject<Savewtyp>(data);
        
        msg = save_wtypemas(Data);
        return msg;
    }
    public class Savewtyp
    {
        [JsonProperty("Divcode")]
        public object divcode { get; set; }        

        [JsonProperty("wname")]
        public object wn { get; set; }

        [JsonProperty("wshnam")]
        public object wsn { get; set; }

        [JsonProperty("races")]
        public object rac { get; set; }

        [JsonProperty("rplac")]
        public object splc { get; set; }

        [JsonProperty("fldi")]
        public object fld { get; set; }


    }
    public static string save_wtypemas(Savewtyp sw)
    {
        DB_EReporting db = new DB_EReporting();
        DataSet ds = null;
        string msg = string.Empty;
        string strQry = "exec insertworktype '" + sw.divcode + "','" + sw.wn + "','" + sw.wsn + "','" + sw.rac + "','" + sw.splc + "','" + sw.fld + "'";
        try
        {
            ds = db.Exec_DataSet(strQry);
            msg = "Success";
        }
        catch (Exception ex)
        {
            msg = ex.Message;
        }
        return msg;
    }

    public class workData
    {
        public string wcode { get; set; }
        public string wname { get; set; }
        public string wsname { get; set; }
        public string races { get; set; }
        public string rplac { get; set; }
        public string fldi { get; set; }
       

    }

    [WebMethod]
    public static workData[] getwtypdets(string wcode)
    {
        string msg = string.Empty;
        
        DataSet dsm =getworkdets(wcode);
        List<workData> ad = new List<workData>();
        foreach (DataRow row in dsm.Tables[0].Rows)
        {
            workData asd = new workData();
            asd.wcode = row["WorkType_Code_B"].ToString();
            asd.wname = row["Worktype_Name_B"].ToString();
            asd.wsname = row["WType_SName"].ToString();
            asd.races = row["TP_DCR"].ToString();            
            asd.rplac = row["Place_Involved"].ToString();            
            asd.fldi = row["FieldWork_Indicator"].ToString();            
            ad.Add(asd);
        }
       
        return ad.ToArray(); ;
    }

    public static DataSet getworkdets(string wcode)
    {
        DB_EReporting db = new DB_EReporting();
        DataSet dsAdmin = new DataSet();
        
        String strQry = "exec getwtypeDets '" + wcode + "'";
        try
        {
            dsAdmin = db.Exec_DataSet(strQry);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return dsAdmin;
    }

    [WebMethod(EnableSession = true)]
    public static string updwtype(string data)
    {
        string msg = string.Empty;
        updatewtyp Data = JsonConvert.DeserializeObject<updatewtyp>(data);

        msg = update_wtypemas(Data);
        return msg;
    }
    public class updatewtyp
    {
        [JsonProperty("wcode")]
        public object wcode { get; set; }

        [JsonProperty("Divcode")]
        public object divcode { get; set; }

        [JsonProperty("wname")]
        public object wn { get; set; }

        [JsonProperty("wshnam")]
        public object wsn { get; set; }

        [JsonProperty("races")]
        public object rac { get; set; }

        [JsonProperty("rplac")]
        public object splc { get; set; }

        [JsonProperty("fldi")]
        public object fld { get; set; }


    }
    public static string update_wtypemas(updatewtyp sw)
    {
        DB_EReporting db = new DB_EReporting();
        DataSet ds = null;
        string msg = string.Empty;
        string strQry = "exec updateworktype '" + sw.wcode + "','" + sw.wn + "','" + sw.wsn + "','" + sw.rac + "','" + sw.splc + "','" + sw.fld + "'";
        try
        {
            ds = db.Exec_DataSet(strQry);
            msg = "Success";
        }
        catch (Exception ex)
        {
            msg = ex.Message;
        }
        return msg;
    }
}