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

public partial class MasterFiles_HQ_Creation : System.Web.UI.Page
{
    public static string hqcode = string.Empty;
	string sf_type = string.Empty;
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
        hqcode = Request.QueryString["hqcode"];

    }

    [WebMethod(EnableSession = true)]
    public static string getHQID(string divcode)
    {
        SalesForce cp = new SalesForce();
        DataSet ds = cp.getSFHQID(divcode);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }

    [WebMethod(EnableSession = true)]
    public static string savehq(string data)
    {
        string msg = string.Empty;
        SaveSFHQ Data = JsonConvert.DeserializeObject<SaveSFHQ>(data);
        salfrce dsm = new salfrce();
        msg = dsm.saveSF_HQ(Data);
        return msg;
    }

    [WebMethod(EnableSession = true)]
    public static string getHQ1(string divcode, string scode)
    {
        SalesForce cp = new SalesForce();
        DataSet ds = cp.getSF_HQ(scode, divcode);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
	
	 public class SaveSFHQ
    {
        [JsonProperty("Divcode")]
        public object divcode { get; set; }

        [JsonProperty("HQName")]
        public object hqname { get; set; }

        [JsonProperty("HQCode")]
        public object hqcode { get; set; }

        [JsonProperty("HQtyp")]
        public object hqtyp { get; set; }

        [JsonProperty("Htyp")]
        public object htyp { get; set; }

        [JsonProperty("LatLong")]
        public object latlong { get; set; }
    }
	 public class salfrce
    {
		 public string saveSF_HQ(SaveSFHQ ss)
        {
            DB_EReporting db = new DB_EReporting();
             int iReturn = -1;
            DataSet ds = null;
            string msg = string.Empty;
            string strQry = "exec insertHQDets '" + ss.divcode + "'," + ss.hqcode + ",'" + ss.hqname + "','" + ss.hqtyp + "','" + ss.htyp + "','" + ss.latlong + "'";
            if (!chkrecexist(Convert.ToString(ss.hqname), Convert.ToString(ss.htyp), Convert.ToString(ss.divcode)))
            {
                try
                {
                    ds = db.Exec_DataSet(strQry);
                    msg = "Success";
                }
                catch (Exception ex)
                {
                    msg = ex.Message;
                }
            }
            else
            {
                 string qry = "update Mas_HQuarters set HQ_Name='" + ss.hqname + "',HQ_Type='" + ss.hqtyp + "',HQ_Type_ID='" + ss.htyp + "',latlong='" + ss.latlong + "' where HQ_ID='" + ss.hqcode + "'";
                iReturn = db.ExecQry(strQry);
                if (iReturn > 0)
                {
                    msg = "Exist";
                }
                else
                {
                    msg = "";
                }
            }
            return msg;
        }
		 public bool chkrecexist(string hqname,string htyp,string divcode)
        {
            DataSet ds = null;
            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                 string strQry = "select * from Mas_HQuarters where HQ_Name='"+ hqname + "' and HQ_Type_ID='"+ htyp + "' and Division_Code='"+ divcode + "'";

                ds = db.Exec_DataSet(strQry);
                if (ds.Tables[0].Rows.Count > 0)
                {
                        bRecordExist = true;
                    }
                    else
                    {
                        bRecordExist = false;
                    }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return bRecordExist;
        }
	}
}