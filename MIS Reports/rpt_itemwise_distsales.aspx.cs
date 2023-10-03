using DBase_EReport;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class MIS_Reports_rpt_itemwise_distsales : System.Web.UI.Page
{
    public static string divcode = string.Empty;
    public static string Sf_Code = string.Empty;
    public static string sfname = string.Empty;
    public static string subdiv_code = string.Empty;
    public static string month = string.Empty;
    public static string year = string.Empty;
    DataSet ff = new DataSet();
    protected void Page_Load(object sender, EventArgs e)
    {
        divcode = Session["div_code"].ToString();
        month = Request.QueryString["Month"].ToString();
        year = Request.QueryString["Year"].ToString();
        subdiv_code = Request.QueryString["subdiv"].ToString();
        Sf_Code = Request.QueryString["sf_code"].ToString();
        sfname = Request.QueryString["Sf_Name"].ToString();
        lblHead.Text = "Itemwise Distributor sales View from" + month + " to " + year;
        Feild.Text = sfname;
        FillSF();
    }
    private void FillSF()
    {
        loc SF = new loc();
        ff = new DataSet();
        ff = SF.get_details(divcode, month, year, Sf_Code);
        if (ff.Tables[0].Rows.Count > 0)
        {
            gvtotalorder.DataSource = ff;
            gvtotalorder.DataBind();
        }
        else
        {
            gvtotalorder.DataSource = null;
            gvtotalorder.DataBind();
        }
    }
    protected void btnExcel_Click(object sender, EventArgs e)
    {
        string attachment = "attachment; filename=Secondary_Monthly_sales.xls";
        Response.ClearContent();
        Response.AddHeader("content-disposition", attachment);
        Response.ContentType = "application/ms-excel";
        StringWriter sw = new StringWriter();
        HtmlTextWriter htw = new HtmlTextWriter(sw);
        HtmlForm frm = new HtmlForm();
        pnlContents.Parent.Controls.Add(frm);
        frm.Attributes["runat"] = "server";
        frm.Controls.Add(pnlContents);
        frm.RenderControl(htw);
        Response.Write(sw.ToString());
        Response.End();
    }
    public class loc
    {
        public DataSet get_details(string divcode, string month, string year, string Sf_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;

            string strQry = "exec sp_itmewise_distsale '" + Sf_Code + "','" + month + "','" + year + "','" + divcode + "'";

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