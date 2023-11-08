using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
using System.Web.Services;
using System.Data.SqlClient;
using Newtonsoft.Json;
using System.Globalization;

public partial class MasterFiles_HolidayMaster : System.Web.UI.Page
{
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
        Page.Header.DataBind();
    }

    public class Holodays
    {
        public string HolidayDate { get; set; }
        public string HolidayName { get; set; }
        public string HolidayRemarks { get; set; }
        public string CreateDate { get; set; }
        public string HolidayID { get; set; }
        public string odFormat { get; set; }
        public string stateCode { get; set; }

    }


    public class Holodayss
    {
        public string label { get; set; }
        public string value { get; set; }
        public string id { get; set; }

    }

    [WebMethod(EnableSession = true)]
    public static Holodayss[] GetHolidays()
    {
        string div_code = "";
        try
        {
            div_code = HttpContext.Current.Session["div_code"].ToString();
        }
        catch
        {
            div_code = HttpContext.Current.Session["Division_Code"].ToString();
        }


        List<Holodayss> HDay = new List<Holodayss>();
        DataSet dsAlowtype = null;
        Holiday hd = new Holiday();

        dsAlowtype = hd.GetHolidaysNew(div_code);

        foreach (DataRow row in dsAlowtype.Tables[0].Rows)
        {
            Holodayss d = new Holodayss();
            d.label = row["HolidayName"].ToString();
            d.value = row["HolidayName"].ToString();
            d.id = row["HolidayID"].ToString();
            HDay.Add(d);
        }
        return HDay.ToArray();
    }


    [WebMethod(EnableSession = true)]
    public static Holodays[] GetDate(string FYear, string stateCode)
    {
        string div_code = "";
        try
        {
            div_code = HttpContext.Current.Session["div_code"].ToString();
        }
        catch
        {
            div_code = HttpContext.Current.Session["Division_Code"].ToString();
        }

        Holiday hd = new Holiday();
        List<Holodays> HDay = new List<Holodays>();
        DataSet dsAlowtype = new DataSet(); 
        //dsAlowtype = hd.GetHolidaysDataNew(div_code, FYear, stateCode);
        dsAlowtype = GetHolidaysDataNew(div_code, FYear, stateCode);
        foreach (DataRow row in dsAlowtype.Tables[0].Rows)
        {
            Holodays d = new Holodays();
            d.HolidayDate = Convert.ToDateTime(row["HolidayDate"]).ToString("dd/MM/yyyy");
            d.odFormat = Convert.ToDateTime(row["HolidayDate"]).ToString("yyyy-MM-dd");
            d.HolidayName = row["HolidayName"].ToString();
            d.HolidayRemarks = row["HolidayRemarks"].ToString();
            d.HolidayID = row["HolidayID"].ToString();
            d.stateCode = row["stateCode"].ToString();
            HDay.Add(d);
        }
        return HDay.ToArray();
    }

    public static DataSet GetHolidaysDataNew(string divcode, string FYear, string stateCode)
    {
        //DB_EReporting db_ER = new DB_EReporting();

        DataSet dsAdmin = new DataSet();
        string strQry = String.Empty;

        //strQry = "select HolidayDate,HolidayID,HolidayName,HolidayRemarks,stateCode from Mas_Holiday_Dates_Detail where DivisionCode='" + divcode + "'  and ActiveFlag='0' and stateCode='" + stateCode + "' order by HolidayDate ";
        strQry = " SELECT HolidayDate,HolidayID,HolidayName,HolidayRemarks,stateCode FROM Mas_Holiday_Dates_Detail ";
        strQry += " WHERE DivisionCode = @divcode  AND ActiveFlag='0' AND stateCode = @stateCode  ORDER BY HolidayDate   ";
         
        try 
        {
            using (var con = new SqlConnection(Globals.ConnString))
            {
                using (var cmd = con.CreateCommand())
                {
                    cmd.CommandText = strQry;
                    cmd.Parameters.AddWithValue("@divcode", Convert.ToString(divcode));
                    cmd.Parameters.AddWithValue("@stateCode", Convert.ToString(stateCode));
                    cmd.CommandType = CommandType.Text;

                    SqlDataAdapter dap = new SqlDataAdapter();
                    dap.SelectCommand = cmd;
                    con.Open();
                    dap.Fill(dsAdmin);
                    con.Close();
                }
            }
            //dsAdmin = db_ER.Exec_DataSet(strQry);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return dsAdmin;

    }


    [WebMethod(EnableSession = true)]
    public static string InsertHoliday(string FYear, string FDate, string TDate, string HolidayId, string HolidayName, string HolidayRemarks, string stateCode)
    {
        string div_code = "";
        try
        {
            div_code = HttpContext.Current.Session["div_code"].ToString();
        }
        catch
        {
            div_code = HttpContext.Current.Session["Division_Code"].ToString();
        }





        DateTime dt = new DateTime();
        dt = Convert.ToDateTime(FDate);

        DateTime dt1 = new DateTime();
        dt1 = Convert.ToDateTime(TDate);


        int iReturn = 0;
        Holiday hd = new Holiday();
        for (var day = dt.Date; day.Date <= dt1.Date; day = day.AddDays(1))
        {
            iReturn = hd.AddMasHolidays(div_code, HolidayName, HolidayRemarks, day.ToString("yyyy/MM/dd"), FYear, HolidayId, stateCode);
        }
        if (iReturn > 0)
        {
            return "Sucess";
        }
        else
        {
            return "Fail";
        }
        //Get_State_Division_Wise
    }

    public class states
    {
        public string stCode { get; set; }
        public string stName { get; set; }
        public string stSName { get; set; }
    }


    [WebMethod(EnableSession = true)]
    public static states[] GetState()
    {
        string div_code = "";
        try
        {
            div_code = HttpContext.Current.Session["div_code"].ToString();
        }
        catch
        {
            div_code = HttpContext.Current.Session["Division_Code"].ToString();
        }

        State hd = new State();
        List<states> HDay = new List<states>();
        DataSet dsAlowtype = null;
        dsAlowtype = hd.Get_State_Division_Wise(div_code);
        foreach (DataRow row in dsAlowtype.Tables[0].Rows)
        {
            states d = new states();
            d.stCode = row["STATE_CODE"].ToString();
            d.stName = row["STATENAME"].ToString();

            HDay.Add(d);
        }
        return HDay.ToArray();
    }
    [WebMethod(EnableSession = true)]
    public static int Deletehoilday(string holiID, string holidate, string statecode)
    {
        string Div_Code = HttpContext.Current.Session["div_code"].ToString();
        Holiday dsm = new Holiday();
        int iReturn = dsm.deleteholiday(holiID, holidate, statecode, Div_Code);
        return iReturn;
    }

}