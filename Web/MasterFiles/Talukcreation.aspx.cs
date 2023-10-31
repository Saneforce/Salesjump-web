using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data;
using System.Configuration;
using Bus_EReport;
using DBase_EReport;
using System.Web.Services;
using Newtonsoft.Json;

public partial class MasterFiles_Talukcreation : System.Web.UI.Page
{
    #region "Declaration"
    #region "Declaration"
    DataSet dsSubDiv = null;
    string Subdivision_Code = string.Empty;
    public static string divcode = string.Empty;
    string subdiv_sname = string.Empty;
    string subdiv_name = string.Empty;
    string Dist_name = string.Empty;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int time;
    DataSet dsDivision = null;
    DataSet dsState = null;
    DataSet dsStockist = null;
    string state_cd = string.Empty;
    string sState = string.Empty;
    string Dist_cd = string.Empty;
    string Terr_Cd = string.Empty;
    string Terr_name = string.Empty;
    string Tr = string.Empty;
	string sf_type = string.Empty;
    string[] statecd;
    #endregion
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        divcode = Convert.ToString(Session["div_code"]);
        Session["backurl"] = "TownList.aspx";
        Subdivision_Code = Request.QueryString["Subdivision_Code"];

        if (!Page.IsPostBack)
        {

            //menu1.Title = this.Page.Title;
            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);
           
        }
    }
   
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

    [WebMethod]
    public static string saveData(string nme, string hoid, string divcode)
    {
        string getreturn;

        try
        {
            tlk sf = new tlk();
            getreturn = sf.addtaluk(nme, hoid, divcode);

        }
        catch (Exception ex)
        {
            throw (ex);
        }
        return getreturn;
    }

    [WebMethod]
    public static string filldata(int hoid)
    {
        tlk sf = new tlk();
        string divcode = HttpContext.Current.Session["Div_code"].ToString();
        DataSet ds = sf.gettalk_textfill(hoid, divcode);
        return JsonConvert.SerializeObject(ds.Tables[0]);

    }
    public class tlk
    {
        public string addtaluk(string nme, string hoid, string divcode)
        {
            DataSet i = null;
            DB_EReporting db = new DB_EReporting();
            string msg = "";
            if ((hoid == null || hoid == "" || hoid == "null"))
            {
                if (!RecordExistertlog(nme, divcode))
                {
                    try
                    {
                        //string strQry = "SELECT isnull(max(TownCode)+1,'1')TownCode from Route_Town_master";
                        //int getid = db.Exec_Scalar(strQry);

                        string strQry = "INSERT INTO Route_Town_master(TownCode,TownName,Division_Code,Active_Flag,CreatedDate,Last_UpdtDate)";
                        //strQry += "values('" + getid + "','" + nme + "','" + divcode + "','0',getdate(),getdate())";
                        strQry += " SELECT CONVERT(numeric, isnull(max(TownCode)+1,1)) TownCode,'" + nme + "' TownName,'" + divcode + "' Division_Code,";
                        strQry += "0 Active_Flag,getdate() CreatedDate,getdate() Last_UpdtDate FROM Route_Town_master WHERE Division_Code=" + divcode + "";
                        i = db.Exec_DataSet(strQry);
                        msg = "Saved";
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }                    

                }
                else
                {
                    msg = "Exist";
                }
            }
            else
            {
                string strQry = "update Route_Town_master set TownName='" + nme + "' where division_code='" + divcode + "'  and  TownCode = '" + hoid + "'";
                i = db.Exec_DataSet(strQry);
                msg = "Update";
            }
            return msg;
        }
        public bool RecordExistertlog(string nme, string divcode)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                string strQry = "SELECT COUNT(*) FROM Route_Town_master where  Division_Code='" + divcode + "' AND Townname='" + nme + "'";
                int iRecordExist = db.Exec_Scalar(strQry);

                if (iRecordExist > 0)
                    bRecordExist = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return bRecordExist;

        }
        public DataSet gettalk_textfill(int hoid, string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;

            string strQry = "select * from Route_Town_master where TownCode='" + hoid + "' and charindex(',' + cast('" + divcode + "' as varchar) + ',',',' + cast(Division_Code as varchar)  + ',')> 0 ";
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

    }
  
}