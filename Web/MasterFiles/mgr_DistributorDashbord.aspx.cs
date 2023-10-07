using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
using System.IO;
using System.Web.UI.HtmlControls;
using DBase_EReport;

public partial class MasterFiles_mgr_DistributorDashbord : System.Web.UI.Page
{
    #region "Declaration"
    string div_code = string.Empty;
    public static string sf_code = string.Empty;
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        sf_code = Request.QueryString["SFCode"].ToString();
        loadData();
    }


    private void loadData()
    {
        DataSet dsStockist = new DataSet();
        stk sk = new stk();
        dsStockist = sk.GET_DISTRIBUTOR_Home(div_code, sf_code, "", "", "0");
        DistributorGrd.DataSource = dsStockist;
        DistributorGrd.DataBind();
    }
    protected void btnExcel_Click(object sender, EventArgs e)
    {
        string strFileName = Page.Title;
        string attachment = "attachment; filename='" + strFileName + "'.xls";

        Response.ClearContent();
        Response.AddHeader("content-disposition", attachment);
        Response.ContentType = "application/ms-excel";
        StringWriter sw = new StringWriter();
        HtmlTextWriter htw = new HtmlTextWriter(sw);
        HtmlForm frm = new HtmlForm();
        form1.Parent.Controls.Add(frm);
        frm.Attributes["runat"] = "server";
        frm.Controls.Add(pnlContents);
        frm.RenderControl(htw);
        Response.Write(sw.ToString());
        Response.End();
    }
    public class stk
    {
        public DataSet GET_DISTRIBUTOR_Home(string divcode, string sf_code, string Alpa, string sFind, string subDivCode = "0")
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet dsStockist = null;
            string strQry = "exec [new_GET_DISTRIBUTOR_Home] '" + divcode + "','" + sf_code + "','" + Alpa + "','" + sFind + "','" + subDivCode + "'";
            try
            {
                dsStockist = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsStockist;
        }
    }
}