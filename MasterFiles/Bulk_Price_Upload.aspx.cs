using Bus_EReport;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using DBase_EReport;
using ClosedXML.Excel;
using System.IO;

public partial class MasterFiles_Bulk_Price_Upload : System.Web.UI.Page
{
    public string div_code = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = HttpContext.Current.Session["div_code"].ToString();
    }

    [WebMethod(EnableSession = true)]
    public static string DivName()
    {
        string div_code = HttpContext.Current.Session["div_code"].ToString();
        div_code = div_code.TrimEnd(',');
        DataSet ds = new DataSet();
        SalesForce sd = new SalesForce();
        ds = sd.Getsubdivisionwise(div_code);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    [WebMethod(EnableSession = true)]
    public static string Get_rate_Details(string Subdiv_code, string Name,string Code,string DatePeriod)
    {
        string div_code = HttpContext.Current.Session["div_code"].ToString();
        DataSet ds = new DataSet();
        SqlConnection con = new SqlConnection(Globals.ConnString);
        con.Open();
        SqlCommand cmd = new SqlCommand("Exec sp_bulkPrice '" + div_code + "','" + Subdiv_code + "','" + Name + "','" + Code + "','" + DatePeriod + "'", con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(ds);
        con.Close();
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }

    [WebMethod(EnableSession = true)]
    public static string GetDesgnName()
    {
        string div_code = HttpContext.Current.Session["div_code"].ToString();
        DataSet ds = new DataSet();
        SqlConnection con = new SqlConnection(Globals.ConnString);
        con.Open();
		SqlCommand cmd = new SqlCommand("Exec sp_rate_card_edit '" + div_code + "'", con);						  
        //SqlCommand cmd = new SqlCommand("select h.Division_Code,h.subdivision_name,Price_list_Sl_No,Price_list_Name,convert(varchar,Effective_From_Date,101) as Effective_From_Date,convert(varchar,Effective_To_Date,101) as Effective_To_Date,sub_div_code from Mas_Product_Wise_Bulk_rate_head h where h.Division_Code="+ div_code, con);
        //SqlCommand cmd = new SqlCommand("select h.Division_Code,d.subdivision_sname,Price_list_Sl_No,Price_list_Name,convert(varchar,Effective_From_Date,105) as Effective_From_Date,sub_div_code from Mas_Product_Wise_Bulk_rate_head h inner join mas_subdivision d on charindex(',' + cast(d.subdivision_code as varchar) + ',',',' + h.sub_div_code + ',')> 0 and h.Division_Code=div_code", con);
		SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(ds);
        con.Close();
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }

    [WebMethod(EnableSession = true)]
    public static string SaveBulkPriceOrder(string BodyBulkData,  string divcode,string subdivCode, string Name, string EffectiveDate,string EffectiveTo,string subdivName,string PeriodSlno)
    {
        DataSet ds = new DataSet();
        SqlConnection con = new SqlConnection(Globals.ConnString);
        con.Open();
        using (var scope = new TransactionScope())
        {
            try
            {

                IList<BulkProductBodyData> BodyData = Newtonsoft.Json.JsonConvert.DeserializeObject<IList<BulkProductBodyData>>(BodyBulkData);
                string sxm1 = "<ROOT>";
                for (int i = 0; i < BodyData.Count; i++)
                {
                    sxm1 += "<Prod Product_Code=\"" + BodyData[i].product_detail_code +"\" Ret_Rate=\"" + BodyData[i].Ret_Rate + "\" Unit=\"" + BodyData[i].Unit + "\"  UnitCode=\"" + BodyData[i].UnitCode + "\"  MRP_Price=\"" + BodyData[i].MRP_Price + "\"  Ret_Rate_in_piece=\"" + BodyData[i].Ret_Rate_in_piece + "\" Dis_Rate=\"" + BodyData[i].Dis_Rate + "\" Dis_Rate_in_piece=\"" + BodyData[i].Dis_Rate_in_piece + "\" />";
                }
                sxm1 += "</ROOT>";

                SqlCommand cmd = new SqlCommand("exec sp_Insert_BulkWise_ProductRate '" + divcode + "','" + subdivCode + "','" + Name + "','" + EffectiveDate + "','" + EffectiveTo + "','" + subdivName + "','" + PeriodSlno + "','" + sxm1 + "'", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                scope.Dispose();
                throw ex;
            }
        }
        con.Close();
        return "Bulk Wise Price Successfully saved..";
    }

    [WebMethod(EnableSession = true)]
    public static string Get_Product_unit()
    {
        DataSet ds = new DataSet();
        string div_code = HttpContext.Current.Session["div_code"].ToString();
        string Stockist_Code = HttpContext.Current.Session["Sf_Code"].ToString();
        div_code = div_code.TrimEnd(',');
        StockistMaster sm = new StockistMaster();
        ds = gets_Product_unit_details(div_code, Stockist_Code);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }

    public static DataSet gets_Product_unit_details(string Div_Code, string Stockist_Code)
    {
        DataSet ds = null;
        DB_EReporting db_ER = new DB_EReporting();

        string strQry = "Exec get_Hap_pro_unit '" + Div_Code + "','" + Stockist_Code + "'";
        try
        {
            ds = db_ER.Exec_DataSet(strQry);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return ds;
    }

    public static DataTable get_product_excel(string Subdiv_code, string Div_Code)
    {
        DataTable ds = null;
        DB_EReporting db_ER = new DB_EReporting();

        string strQry = "Exec get_product_excel '" + Subdiv_code + "','" + Div_Code + "'";
        try
        {
            ds = db_ER.Exec_DataTable(strQry);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return ds;
    }

    [WebMethod(EnableSession = true)]
    public static string Get_All_product()
    {
        DataSet ds = new DataSet();
        string div_code = HttpContext.Current.Session["div_code"].ToString();
        div_code = div_code.TrimEnd(',');
        ds = get_All_product(div_code);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    public static DataSet get_All_product(string Div_Code)
    {
        DataSet ds = null;
        DB_EReporting db_ER = new DB_EReporting();

        string strQry = "Exec sp_get_All_product '" + Div_Code + "'";
        try
        {
            ds = db_ER.Exec_DataSet(strQry);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return ds;
    }


    public class BulkProductHeadData
    {
        public string divCode { get; set; }
        public string Name { get; set; }
    }

    public class BulkProductBodyData
    {
        public string product_detail_code { get; set; }
        public string product_detail_name { get; set; }
        public string Unit { get; set; }
        public string UnitCode { get; set; }
        public string Ret_Rate { get; set; }
        public string MRP_Price { get; set; }
        public float Ret_Rate_in_piece { get; set; }
        public string Dis_Rate { get; set; }
        public float Dis_Rate_in_piece { get; set; }
    }

    protected void Upldbt_Click1(object sender, EventArgs e)
    {
        string sub_div_code = hidden_div.Value;
        DataTable dt = new DataTable();
        dt = get_product_excel(sub_div_code, div_code);        
        dt.Columns.Remove("subdivision_code");
		
        using (XLWorkbook wb = new XLWorkbook())
        {
            var ws = wb.Worksheets.Add(dt, "Account Transaction Details");
            ws.Tables.FirstOrDefault().ShowAutoFilter = false;
            // wb.Worksheets.Add(dt, "Product List");
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=Rate_Card_Product_Details.xlsx");
            using (MemoryStream MyMemoryStream = new MemoryStream())
            {
                wb.SaveAs(MyMemoryStream);
                MyMemoryStream.WriteTo(Response.OutputStream);
                Response.Flush();
                Response.End();
            }
        }
    }
	[WebMethod(EnableSession = true)]
    public static string Get_Product_unit_new()
    {
        DataSet ds = new DataSet();
        string div_code = HttpContext.Current.Session["div_code"].ToString();
        string Stockist_Code = HttpContext.Current.Session["Sf_Code"].ToString();
        div_code = div_code.TrimEnd(',');
        StockistMaster sm = new StockistMaster();
        ds = gets_Product_unit_details_new(div_code, Stockist_Code);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }

    public static DataSet gets_Product_unit_details_new(string Div_Code, string Stockist_Code)
    {
        DataSet ds = null;
        DBase_EReport.DB_EReporting db_ER = new DBase_EReport.DB_EReporting();

        string strQry = "select Move_MailFolder_Id, Move_MailFolder_Name from Mas_Multi_Unit_Entry where Division_Code = " + Div_Code + " and Folder_Act_flag = 0 order by Move_MailFolder_Name";

       
        try
        {
            ds = db_ER.Exec_DataSet(strQry);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return ds;
    }

    [WebMethod(EnableSession = true)]
    public static string get_view_unit()
    {
        DataSet ds = new DataSet();
        string div_code = HttpContext.Current.Session["div_code"].ToString();
        div_code = div_code.TrimEnd(',');
        StockistMaster sm = new StockistMaster();
        ds = get_view_unit_details(div_code);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }

    public static DataSet get_view_unit_details(string Div_Code)
    {
        DataSet ds = null;
        DBase_EReport.DB_EReporting db_ER = new DBase_EReport.DB_EReporting();

        string strQry = "select * from vwMas_MUnitDets where Division_Code = " + Div_Code + "";

        try
        {
            ds = db_ER.Exec_DataSet(strQry);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return ds;
    }
	[WebMethod]
    public static string GetRatePeriods(string Price_list_Code)
    {

        DataTable dt = null;
        string strQry = string.Empty;

        strQry = "Exec sp_GetRatePeriod '" + Price_list_Code + "'";
        dt = execQuery(strQry);

        return JsonConvert.SerializeObject(dt);
    }
    public static DataTable execQuery(string strQry)
    {
        DataTable dt = null;
        DBase_EReport.DB_EReporting db_ER = new DBase_EReport.DB_EReporting();

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

    [WebMethod]
    public static string Get_Product_unitconv(string pcode,string Div)
    {

        DataTable dt = null;
        string strQry = string.Empty;

        strQry = "Select UOM_Id,UOM_Nm,Division_Code,PCode from vwMas_MUnitDets where Division_Code='"+ Div + "' and  PCode='"+ pcode + "'";
        dt = execQuery(strQry);

        return JsonConvert.SerializeObject(dt);
    }
}