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

public partial class MIS_Reports_DMSRetailersDetailsSTwise : System.Web.UI.Page
{
    string divCode = string.Empty;
    public static string st = string.Empty;
  public static string route = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
  if (Request.QueryString["Route"].ToString() != "0")
            route = Request.QueryString["Route"].ToString();
        else
            route = "0";
        divCode = Session["div_code"].ToString();
  Stname.Text = Session["sf_name"].ToString();
    }
    [WebMethod]
    public static string GetList(string divcode, string SF)
    {
        SalesForce sf = new SalesForce();
        string sd = null;
        st = SF;
          DataSet ds = sf.retail_detail_stwise( SF, divcode.TrimEnd(','),route);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    protected void ExportToExcel(object sender, EventArgs e)
    {
        Response.ClearContent();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "RetailerList.xls"));
        Response.ContentType = "application/ms-excel";
        SalesForce sf = new SalesForce();
         DataTable dssalesforce1 = sf.retail_detail_stwisexl(st,divCode.TrimEnd(','), route);
        DataTable dt = dssalesforce1;
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
}
