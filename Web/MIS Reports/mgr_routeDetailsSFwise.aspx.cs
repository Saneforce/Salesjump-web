using System;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Windows;
using System.IO;
using System.Data;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html;
using iTextSharp.text.html.simpleparser;
using iTextSharp.tool.xml;
using System.Text;
using Bus_EReport;
using System.Net;
using System.Web.Services;
using Newtonsoft.Json;
using DBase_EReport;

public partial class MIS_Reports_mgr_routeDetailsSFwise : System.Web.UI.Page
{
    public static string div_code = string.Empty;
    public static string sf_code = string.Empty;
    public static string sf_name = string.Empty;
    public static string totcnt = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        sf_code = Session["sf_code"].ToString();
        sf_name = Request.QueryString["SFName"].ToString();
        
        lblHead.Text = "Manager Route Detail";
        lblsf_name.Text = sf_name;
       
    }
    [WebMethod(EnableSession = true)]
    public static string getfilldtl()
    {
        mgrrut sf = new mgrrut();
        DataTable dsProd = sf.Get_mgr_route();
        return JsonConvert.SerializeObject(dsProd);
    }
    [WebMethod(EnableSession = true)]
    public static string getroute()
    {
        mgrrut sf = new mgrrut();
        DataTable dsProd = sf.Get_mgr_route_details();
        return JsonConvert.SerializeObject(dsProd);
    }
    protected void ExportToExcel(object sender, EventArgs e)
    {

        DataTable dsProd1 = null;
        mgrrut LstDoc = new mgrrut();
        try
        {
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "Arealist.xls"));
            Response.ContentType = "application/ms-excel";
            dsProd1 = LstDoc.Get_mgr_route_excel();
            DataTable dt = dsProd1;

            string str = string.Empty;
            foreach (DataColumn dtcol in dt.Columns)
            {
                Response.Write(str + dtcol.ColumnName);
                str = "\t";
            }
            Response.Write("\n");
            foreach (DataRow dr in dt.Rows)
            {
                str = "";
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    Response.Write(str + Convert.ToString(dr[j]));
                    str = "\t";
                }
                Response.Write("\n");
            }
            Response.End();
        }
        catch (Exception ex)
        {
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('" + ex.Message + "');</script>");
        }
    }
    public class mgrrut 
    {
        public DataTable Get_mgr_route()
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dsDivision = null;
            string strQry = "exec [route_dash] '" + sf_code + "','" + div_code + "'";

            try
            {
                dsDivision = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsDivision;
        }
        public DataTable Get_mgr_route_details()
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dsDivision = null;
            string strQry = "exec [GET_route_Cou_details] '" + div_code + "','" + sf_code + "'";

            try
            {
                dsDivision = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsDivision;
        }
        public DataTable Get_mgr_route_excel()
    {
        DB_EReporting db_ER = new DB_EReporting();

        DataTable dsDivision = null;
        string strQry = "exec [route_dash_excel] '" + sf_code + "','" + div_code + "'";

        try
        {
            dsDivision = db_ER.Exec_DataTable(strQry);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return dsDivision;
    }
}
}