﻿using System;
using System.Collections.Generic;
using System.Data;
using Bus_EReport;
using System.Web.Services;
using Newtonsoft.Json;
using System.Data.SqlClient;
using ClosedXML.Excel;
using System.IO;
using DBase_EReport;

public partial class SuperStockist_New_Distributor_Master : System.Web.UI.Page
{
    string div_code;
    public static string sf_code;
    public string sf_type = string.Empty;
    
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
    [WebMethod]
    public static string getRoute_Retailers(string divcode, string stockist_code)
    {
        Bus_EReport.Stockist SFD = new Bus_EReport.Stockist();
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
        DataSet ds = getStockistDetails(divcode, sf);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    public static  DataSet getStockistDetails(string divcode, string sf_Code)
    {
        DB_EReporting db_ER = new DB_EReporting();

        DataSet dsAdmin = null;

       // string strQry = "exec getSS_StockistMaster '" + sf_Code + "','" + divcode + "'";
        string strQry = "exec GET_STOCKIST_MAPEDSS '" + divcode + "' ,'" + sf_Code + "'";

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
    [WebMethod]
    public static string deactivateStockist(string stockist_code, string stat)
    {
        Bus_EReport.Stockist dv = new Bus_EReport.Stockist();
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
        Bus_EReport.Stockist SFD = new Bus_EReport.Stockist();
        DataTable ds = new DataTable();
        if (conn.State != ConnectionState.Closed)
        {
            conn.Close();
        }
        conn.Open();
        SqlCommand cmd = new SqlCommand("exec getTerri_route_count " + divcode + "", conn);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(ds);
        return JsonConvert.SerializeObject(ds);
    }
    protected void ExportToExcel(object sender, EventArgs e)
    {
        DataTable dsProd1 = null;
        Bus_EReport.Stockist LstDoc = new Bus_EReport.Stockist();
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