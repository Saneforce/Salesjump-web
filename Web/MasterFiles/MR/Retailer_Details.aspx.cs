using System;
using Newtonsoft.Json;
using System.Web.Services;
using Bus_EReport;
using System.Data;
using ClosedXML.Excel;
using System.IO;
using DBase_EReport;

public partial class MasterFiles_MR_Retailer_Details : System.Web.UI.Page
{
    string divCode = string.Empty;
    string sf_type = string.Empty;
    public static string sUSR = string.Empty;
	
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
        sUSR = Request.Url.Host;
        sf_type = Session["sf_type"].ToString();
        if (sf_type == "4")
        {
            divCode = Session["Division_Code"].ToString();
            divCode = divCode.TrimEnd(',');
        }
        else
        {
            divCode = Session["div_code"].ToString();
        }
    }
    [WebMethod]
    public static string getStates(string divcode)
    {
        Territory SFD = new Territory();
        DataSet ds = SFD.getRo_States(divcode);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    [WebMethod]
    public static string getHQ(string divcode, string Sstate)
    {
        Territory SFD = new Territory();
        DataSet ds = SFD.getSFHQ(divcode, Sstate);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    [WebMethod]
    public static string getSF(string divcode, string Hq)
    {
        Territory SFD = new Territory();
        DataSet ds = SFD.getSF_HQ(divcode, Hq);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    /*[WebMethod]
    public static string getRetailers(string divcode, string sf)
    {
        ListedDR SFD = new ListedDR();
        DataSet ds = SFD.get_SF_Retailers(divcode, sf);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }*/
    [WebMethod]
    public static string routecnt(string route, string divcode)
    {
        string msg = "";
        DB_EReporting db = new DB_EReporting();       
        DataSet ds = db.Exec_DataSet("Select count(ListeddrCode)Numbers from  Mas_ListedDr wHERE  Territory_Code='" + route + "'  and  Division_Code='" + divcode + "' and ListedDr_Active_Flag=0  ");
        int routenos = Convert.ToInt16(ds.Tables[0].Rows[0]["Numbers"]);
        int allotroutno = masoutletcount(divcode);
        if(routenos >= allotroutno)
        {
            msg = "Overcount";
        }
        else {
            msg = "Belowcount";
        }
        return msg;
    }

    public static int masoutletcount(string divcode)
    {
        DB_EReporting db = new DB_EReporting();
        
        DataSet ds = db.Exec_DataSet("Select maxrouteOutlet Numbers  from Access_Master Where division_code='" + divcode + "'");
        int routenum = Convert.ToInt16(ds.Tables[0].Rows[0]["Numbers"]);
        return routenum;
    }


    [WebMethod]
    public static string retDeact(string retcode, string stat)
    {
        

        Stockist dv = new Stockist();

        int iReturn = dv.DeActivate1(retcode,stat);
        if (iReturn > 0)
        {
            return "Deactivated Successfully";
        }
        else
        {
            return "Unable to Deactivate";
        }
    }
     protected void ExportToExcel(object sender, EventArgs e)
    {
        string name = "admin";
        string sReport = "admin";
        DataTable dsProd1 = null;
        if (divCode == "170")
        {
            try
            {
                DataTable retailerMaster = new DataTable();
                retailerMaster = getRetailerDump(divCode);

                if (divCode != "126")
                {
                    retailerMaster.Columns.Remove("Layer");
                    retailerMaster.Columns.Remove("Breeder");
                    retailerMaster.Columns.Remove("Broiler");
                };

                string attachment = "attachment; filename=RetailerMaster.xls";
                Response.ClearContent();
                Response.AddHeader("content-disposition", attachment);
                Response.ContentType = "application/vnd.ms-excel";
                string tab = "";
                foreach (DataColumn dc in retailerMaster.Columns)
                {
                    Response.Write(tab + dc.ColumnName);
                    tab = "\t";
                }
                Response.Write("\n");
                int i;
                foreach (DataRow dr in retailerMaster.Rows)
                {
                    tab = "";
                    for (i = 0; i < retailerMaster.Columns.Count; i++)
                    {
                        Response.Write(tab + dr[i].ToString());
                        tab = "\t";
                    }
                    Response.Write("\n");
                }
                Response.End();
            }
            catch (Exception es)
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('" + es.Message + "');</script>");
            }
        }
        else
        {
            ListedDR LstDoc = new ListedDR();
            try
            {
                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(getRetailerMaster(), "Retailers");
                    Response.Clear();
                    Response.Buffer = true;
                    Response.Charset = "";
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AddHeader("content-disposition", "attachment;filename=RetailerMaster.xlsx");
                    using (MemoryStream MyMemoryStream = new MemoryStream())
                    {
                        wb.SaveAs(MyMemoryStream);
                        MyMemoryStream.WriteTo(Response.OutputStream);
                        Response.Flush();
                        Response.End();
                    }
                }
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('" + ex.Message + "');</script>");
            }
        }
    }
    /*public class RetailerClass
    {
        public string Doc_ClsCode { get; set; }
        public string Doc_ClsName { get; set; }
    }
    public class StkMaster
    {
        public string Stockist_Code { get; set; }
        public string Stockist_Name { get; set; }
    }
    public class RetailerChannel
    {
        public string Doc_Special_Code { get; set; }
        public string Doc_Special_Name { get; set; }
    }
    public class RouteMater
    {
        public string RouteCode { get; set; }
        public string RouteName { get; set; }
        public string Territory_name { get; set; }
    }
    public class UserMaster
    {
        public string SfCode { get; set; }
        public string SfName { get; set; }
        public string SfHQ { get; set; }
        public string State { get; set; }
        public string Designation { get; set; }
    }*/
    public DataTable getRetailerMaster()
    {
        /*SqlConnection con = new SqlConnection(Globals.ConnString);
        DataTable stkmaster = new DataTable();
        DataTable routemaster = new DataTable();
        DataTable channelmaster = new DataTable();
        DataTable classmaster = new DataTable();
        DataTable usersMaster = new DataTable();*/
        DataTable retailerMaster = new DataTable();
        retailerMaster = getRetailerDump(divCode);
		if(divCode!="126"){
			retailerMaster.Columns.Remove("Layer");
			retailerMaster.Columns.Remove("Breeder");
			retailerMaster.Columns.Remove("Broiler");
		}
        /*con.Open();
        SqlCommand cmd = new SqlCommand("select Stockist_Code,Stockist_Name from Mas_Stockist where Division_Code='" + divCode + "' and Stockist_Active_Flag=0", con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(stkmaster);
        con.Close();
        con.Open();
        cmd = new SqlCommand("select tc.Territory_Code RouteCode,tc.Territory_Name RouteName,mt.Territory_name from Mas_Territory_Creation tc inner join Mas_Territory mt on cast(mt.Territory_code as varchar)=tc.Territory_SName where tc.Division_Code='" + divCode + "'", con);
        da = new SqlDataAdapter(cmd);
        da.Fill(routemaster);
        con.Close();
        con.Open();
        cmd = new SqlCommand("select Doc_ClsCode,Doc_ClsName from Mas_Doc_Class where Division_Code='" + divCode + "'", con);
        da = new SqlDataAdapter(cmd);
        da.Fill(classmaster);
        con.Close();
        con.Open();
        cmd = new SqlCommand("select Doc_Special_Code,Doc_Special_Name from Mas_Doctor_Speciality where Division_Code='" + divCode + "'", con);
        da = new SqlDataAdapter(cmd);
        da.Fill(channelmaster);
        con.Close();
        con.Open();
        cmd = new SqlCommand("EXEC getALLSFUsers 'admin','" + divCode + "',0", con);
        da = new SqlDataAdapter(cmd);
        da.Fill(usersMaster);
        con.Close();
        con.Open();
        cmd = new SqlCommand("EXEC getRetailerDump '" + divCode + "'", con);
        da = new SqlDataAdapter(cmd);
        da.Fill(retailerMaster);
        con.Close();
        List<RetailerClass> drpt = (from row in classmaster.AsEnumerable()
                                    select new RetailerClass
                                    {
                                        Doc_ClsCode = row.Field<Int32>("Doc_ClsCode").ToString(),
                                        Doc_ClsName = row.Field<string>("Doc_ClsName")
                                    }).ToList();

        List<RetailerChannel> drptch = (from row in channelmaster.AsEnumerable()
                                        select new RetailerChannel
                                        {
                                            Doc_Special_Code = row.Field<Int32>("Doc_Special_Code").ToString(),
                                            Doc_Special_Name = row.Field<string>("Doc_Special_Name")
                                        }).ToList();

        List<RouteMater> drroute = (from row in routemaster.AsEnumerable()
                                    select new RouteMater
                                    {
                                        RouteCode = row.Field<Decimal>("RouteCode").ToString(),
                                        RouteName = row.Field<string>("RouteName"),
                                        Territory_name = row.Field<string>("Territory_name")
                                    }).ToList();

        List<StkMaster> drstks = (from row in stkmaster.AsEnumerable()
                                  select new StkMaster
                                  {
                                      Stockist_Code = row.Field<string>("Stockist_Code"),
                                      Stockist_Name = row.Field<string>("Stockist_Name")
                                  }).ToList();

        List<UserMaster> drusers = (from row in usersMaster.AsEnumerable()
                                    select new UserMaster
                                    {
                                        SfCode = row.Field<string>("Sf_Code"),
                                        SfName = row.Field<string>("Sf_Name"),
                                        SfHQ = row.Field<string>("Sf_HQ"),
                                        Designation = row.Field<string>("Designation"),
                                        State = row.Field<string>("StName")
                                    }).ToList();

        for (int i = 0; i < retailerMaster.Rows.Count; i++)
        {
            retailerMaster.Rows[i]["Retailer_Channel"] = string.Join(",", drptch.Where(f => f.Doc_Special_Code == retailerMaster.Rows[i]["Channel"].ToString()).Select(n => n.Doc_Special_Name).ToArray());
            retailerMaster.Rows[i]["Retailer_Class"] = string.Join(",", drpt.Where(f => f.Doc_ClsCode == retailerMaster.Rows[i]["Class"].ToString()).Select(n => n.Doc_ClsName).ToArray());
            retailerMaster.Rows[i]["Route_Name"] = string.Join(",", drroute.Where(f => f.RouteCode == retailerMaster.Rows[i]["Route_Name"].ToString()).Select(n => n.RouteName).ToArray());
            retailerMaster.Rows[i]["Territory_Name"] = string.Join(",", drroute.Where(f => f.RouteCode == retailerMaster.Rows[i]["Route_Name"].ToString()).Select(n => n.Territory_name).ToArray());
            retailerMaster.Rows[i]["Sf_State"] = string.Join(",", drusers.Where(f => (((retailerMaster.Rows[i]["Sf_Code"].ToString())).IndexOf("," + f.SfCode + ",")) > -1).Select(n => n.State).Distinct().ToArray());
            retailerMaster.Rows[i]["Sf_Name"] = string.Join(",", drusers.Where(f => (((retailerMaster.Rows[i]["Sf_Code"].ToString())).IndexOf("," + f.SfCode + ",")) > -1).Select(n => n.SfName).Distinct().ToArray());
            retailerMaster.Rows[i]["Sf_HQ"] = string.Join(",", drusers.Where(f => (((retailerMaster.Rows[i]["Sf_Code"].ToString())).IndexOf("," + f.SfCode + ",")) > -1).Select(n => n.SfHQ).Distinct().ToArray());
            retailerMaster.Rows[i]["SF_Designation"] = string.Join(",", drusers.Where(f => (((retailerMaster.Rows[i]["Sf_Code"].ToString())).IndexOf("," + f.SfCode + ",")) > -1).Select(n => n.Designation).Distinct().ToArray());
            retailerMaster.Rows[i]["DistributorName"] = string.Join(",", drstks.Where(f => ((retailerMaster.Rows[i]["Dist_name"].ToString()).IndexOf(f.Stockist_Code) > -1)).Select(n => n.Stockist_Name).Distinct().ToArray());
        }
        retailerMaster.Columns.Remove("Sf_Code");
        retailerMaster.Columns.Remove("Class");
        retailerMaster.Columns.Remove("Channel");
        retailerMaster.Columns.Remove("Dist_name");*/
        return retailerMaster;
    }
    public DataTable getRetailerDump(string divCode)
    {

        DB_EReporting db_ER = new DB_EReporting();

        DataTable dsSF = null;

        string strQry = "EXEC getRetailerDump '" + divCode + "'";

        try
        {
            dsSF = db_ER.Exec_DataTable(strQry);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return dsSF;
    }
	[WebMethod]
    public static string getRetailerRoutes(string divcode, string hq)
    {
        //ListedDR SFD = new ListedDR();
        //DataSet ds = SFD.get_SF_Retailers(divcode, sf);
        //return JsonConvert.SerializeObject(ds.Tables[0]);
        string strQry = string.Empty;
        DataTable dt = null;
        strQry = "exec get_SF_Retailer_Routes '" + divcode + "','" + hq + "'";

        dt = execQuery(strQry);
        return JsonConvert.SerializeObject(dt);
    }
    [WebMethod]
    public static string getRetailers(string divcode, string sf, string route)
    {
        string strQry = string.Empty;
        DataTable dt = null;
        strQry = "exec get_SF_Retailer '" + sf + "','" + route + "','http://" + sUSR + "'";
        dt = execQuery(strQry);
        return JsonConvert.SerializeObject(dt);
    }
	public static DataTable execQuery(string strQry)
    {
        DataTable dt = null;
        DB_EReporting db_ER = new  DB_EReporting();

        try
        {
            dt = db_ER.Exec_DataTable(strQry);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return dt;
    }
}