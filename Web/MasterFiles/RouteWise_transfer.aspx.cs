using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
using System.Data.SqlClient;
using System.Web.Services;
using System.Configuration;
using Newtonsoft.Json;
using System.Globalization;
using ClosedXML.Excel;
using System.Xml;
using System.IO;
using System.Data.OleDb;
using DBase_EReport;

public partial class MasterFiles_RouteWise_transfer : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    [WebMethod]
    public static string getFieldForce(string divcode)
    {
        DataSet ds = getDataSet("SELECT a.SF_Code,a.Sf_Name +' - '+ a.sf_Designation_Short_Name + ' - ' +a.sf_hq SF_Nm,Sf_Name,SF_Status from Mas_Salesforce a where charindex(','+'" + divcode + "'+',',',' + a.Division_Code+',')>0 and sf_type='1'");
        //DataSet ds = getDataSet("SELECT a.SF_Code,a.Sf_Name +' - '+ a.sf_Designation_Short_Name + ' - ' +a.sf_hq SF_Nm,Sf_Name,SF_Status from Mas_Salesforce a where charindex(','+'" + divcode + "'+',',',' + a.Division_Code+',')>0 and sf_type='1' and Territory_Code='"+TrCode+"'");
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }   

    [WebMethod]
    public static string bindDisWiseRoute(string divcode, string DisCode,string Field_Code)
    {
        SalesForce sd = new SalesForce();
        DataSet ds = getDataSet("EXEC get_distWise_Route '" + divcode + "','"+ DisCode + "','"+ Field_Code + "'");
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }

    [WebMethod]
    public static string Get_FieldWise_Dist(string divcode, string DisCode)
    {
        SalesForce sd = new SalesForce();
        DataSet ds = getDataSet("EXEC get_FieldWiseDist '" + divcode + "','" + DisCode + "'");
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }

    public class Route_Data
    {
        public string RouteCode { get; set; }
    }

    public class Field_Data
    {
        public string RouteCode { get; set; }
    }

    public class Copy_Data
    {
        public string RouteCode { get; set; }
        public string Type { get; set; }
    }

    public class Trans_Data
    {
        public string RouteCode { get; set; }
    }

    [WebMethod]
    public static string Save_Transfer(string divcode,string To_Route_Details,string ToFF, string ToDis,string FromFF,string FromDis,string FR_Route_Details,string Cpy_Route,string Trf_Route)
    {
        string msg = string.Empty;
        var To_Route = JsonConvert.DeserializeObject<List<Route_Data>>(To_Route_Details);
        var From_Route = JsonConvert.DeserializeObject<List<Field_Data>>(FR_Route_Details);

        var Copy_route = JsonConvert.DeserializeObject<List<Copy_Data>>(Cpy_Route);
        var Transfer_Route = JsonConvert.DeserializeObject<List<Trans_Data>>(Trf_Route);

        string sxml5 = "<ROOT>";
        for (int k = 0; k < Copy_route.Count; k++)
        {
            if (Copy_route[k].Type == "Copy")
            {
                string sf = ToFF + "," + FromFF;
                string Dis = ToDis + "," + FromDis;
                sxml5 += "<Cpy_Route_xml Route=\"" + Copy_route[k].RouteCode + "\" Type=\"" + Copy_route[k].Type + "\" SF=\"" + sf + "\"  Dis=\"" + Dis + "\"  />";
            }
            if (Copy_route[k].Type == "Transfer")
            {
                sxml5 += "<Cpy_Route_xml Route=\"" + Copy_route[k].RouteCode + "\" Type=\"" + Copy_route[k].Type + "\" SF=\"" + ToFF + "\" Dis=\"" + ToDis + "\"  />";
            }           
        }
        sxml5 += "</ROOT>";

        int h = Get_Update("EXEC sp_transferRoute '"+ divcode  + "','"+ ToFF + "','"+ ToDis + "','" + FromFF + "','" + FromDis + "','" + sxml5 + "'");
        if (h > 0)
            msg = "Success";
        else
            msg = "Fail";       
        return msg;
    }

    public static int Get_Update(string qrystring)
    {

        DB_EReporting db_ER = new DB_EReporting();
        int i;
        string strQry = qrystring;

        try
        {
            i = db_ER.ExecQry(strQry);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return i;
    }

    public static DataSet getDataSet(string qrystring)
    {

        DB_EReporting db_ER = new DB_EReporting();
        DataSet dsSF = null;
        string strQry = qrystring;

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