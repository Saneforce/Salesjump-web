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

public partial class MasterFiles_Options_Primary_Upload_View : System.Web.UI.Page
{
    string divCode = string.Empty;
    public static DataTable ds = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        divCode = Session["div_code"].ToString();
        if (!Page.IsPostBack)
        {
            BindDate();
        }
    }
    [WebMethod]
    public static string GetDetails(string Div, string Mn, string Yr)
    {
        Sales SFD = new Sales();
        ds = SFD.GetPrimary_Sales_Value(Div, Mn, Yr);
        return JsonConvert.SerializeObject(ds);
    }
    private void BindDate()
    {
        TourPlan tp = new TourPlan();
        DataSet dsTP = new DataSet();
        dsTP = tp.Get_TP_Edit_Year(divCode);
        if (dsTP.Tables[0].Rows.Count > 0)
        {
            for (int k = Convert.ToInt16(dsTP.Tables[0].Rows[0]["Year"]); k <= DateTime.Now.Year + 1; k++)
            {
                ddlyr.Items.Add(k.ToString());
            }
            ddlyr.Text = DateTime.Now.Year.ToString();
        }
    }
    protected void lnkDownload_Click(object sender, EventArgs e)
    {
        SalesForce SFD = new SalesForce();
        DataTable dss = new DataTable();
        dss = ds;
        using (XLWorkbook wb = new XLWorkbook())
        {
            wb.Worksheets.Add(dss, "Primary_Sales_Upload");
            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=Primary_Sales_Upload for " + ddlmnth.SelectedItem.Text + "-" + ddlyr.SelectedItem.Text + ".xlsx");
            using (MemoryStream MyMemoryStream = new MemoryStream())
            {
                wb.SaveAs(MyMemoryStream);
                MyMemoryStream.WriteTo(Response.OutputStream);
                Response.Flush();
                Response.End();
            }
        }
    }
}