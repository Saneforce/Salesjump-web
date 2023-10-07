using System;
using System.Collections.Generic;
using System.Data;
using Bus_EReport;
using System.Web.Services;
using Newtonsoft.Json;
using System.Data.SqlClient;
using ClosedXML.Excel;
using System.IO;
using DBase_EReport;

public partial class MasterFiles_Distributor_Master : System.Web.UI.Page
{
    string div_code;
    public static string sf_code;
    public string sf_type = string.Empty;
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
    public static SqlConnection conn = new SqlConnection(Globals.ConnString);
    protected void Page_Load(object sender, EventArgs e)
    {
        sf_code = Session["sf_code"].ToString();
        try
        {
            div_code = Session["div_code"].ToString();
        }
        catch
        {
            div_code = Session["Division_Code"].ToString();
        }
    }

    [WebMethod]
    public static string getStates(string divcode)
    {
        //Territory SFD = new Territory();
        //DataSet ds = SFD.getRo_States(divcode);
        DataSet ds = getRo_States(divcode);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }

    public static DataSet getRo_States(string divcode)
    {
        //DB_EReporting db_ER = new DB_EReporting();

        DataSet dsAdmin = new DataSet();
        string strQry = String.Empty;


        strQry = " SELECT st.State_Code,StateName from mas_state st  ";
        strQry += " inner join Mas_Division md  ";
        strQry += " on charindex(','+cast(st.State_Code as varchar)+',',','+md.State_Code+',')>0 ";
        strQry += " WHERE  Division_Code=@divcode  order by StateName ";

        //strQry = "select st.State_Code,StateName from mas_state st inner join Mas_Division md on charindex(','+cast(st.State_Code as varchar)+',',','+md.State_Code+',')>0 where Division_Code="+ divcode + " order by StateName";

        try
        {
            using (var con = new SqlConnection(Globals.ConnString))
            {
                using (var cmd = con.CreateCommand())
                {
                    cmd.CommandText = strQry;
                    cmd.Parameters.AddWithValue("@divcode", Convert.ToString(divcode));
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
    [WebMethod]
    public static string getRoute_Retailers(string divcode, string stockist_code)
    {
        Stockist SFD = new Stockist();
        DataSet ds = SFD.getRoute_Stockist(divcode, stockist_code);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    [WebMethod]
    public static string DownloadDistwiseOutlets(string divcode, string DistId)
    {
        DCR dv = new DCR();
        DataTable iReturn = new DataTable();
        iReturn = dv.getDataTable("exec getDistwiseOutlets '" + divcode + "','" + DistId + "'");
        if (divcode != "126")
        {
            iReturn.Columns.Remove("Breeder");
            iReturn.Columns.Remove("Broiler");
            iReturn.Columns.Remove("Layer");
        }
        return JsonConvert.SerializeObject(iReturn);
    }
    [WebMethod]
    public static string getStockist(string divcode, string sf)
    {
        Stockist SFD = new Stockist();
        DataSet ds = SFD.getStockistDetails(divcode, sf);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    [WebMethod]
    public static string deactivateStockist(string stockist_code, string stat)
    {
        Stockist dv = new Stockist();
        int iReturn = dv.DeActivate(stockist_code, stat);
        if (iReturn > 0)
        {
            return "Success";
        }
        else
        {
            return "Failure";
        }
    }

    [WebMethod]
    public static string getRouCount(string divcode)
    {
        Stockist SFD = new Stockist();
        DataTable ds = new DataTable();

        if ((divcode == "" || divcode == null))
        { divcode = "0"; }

        try
        {
            using (conn = new SqlConnection(Globals.ConnString))
            {
                using (SqlCommand cmmd = new SqlCommand())
                {
                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }
                    cmmd.Connection = conn;
                    cmmd.CommandText = "getTerri_route_count";
                    cmmd.CommandType = CommandType.StoredProcedure;                    
                    cmmd.Parameters.AddWithValue("@Div", Convert.ToString(divcode).Trim());
                    SqlDataAdapter da = new SqlDataAdapter(cmmd);

                    da.Fill(ds);
                    conn.Close();
                }
            }
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
        finally
        {
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }

        return JsonConvert.SerializeObject(ds);
    }

    protected void ExportToExcel(object sender, EventArgs e)
    {
        DataTable dsProd1 = null;
        Stockist LstDoc = new Stockist();
        try
        {
            dsProd1 = LstDoc.getStockist_Ex_MGR(div_code, "", "", "", "0");
            DataTable dt = dsProd1;
            if (div_code != "109")
            {
                dt.Columns.Remove("Vendor_Code");
            }
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt, "DistributorList");
                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename=DistributorMaster.xlsx");
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