using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DBase_EReport;
using Bus_EReport;
using System.Web.Services;
using Newtonsoft.Json;

public partial class MasterFiles_SupplierList : System.Web.UI.Page
{
    string divcode = string.Empty;
    string sf_type = string.Empty;
    string div_code = string.Empty;
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
        div_code = Session["div_code"].ToString();
    }
    [WebMethod(EnableSession = true)]
    public static string GetDetails(string divcode)
    {
        //DSM dv = new DSM();
        supptst dv = new supptst();
        DataSet dsSubDiv = dv.SuppliergetSubDiv(divcode);
        return JsonConvert.SerializeObject(dsSubDiv.Tables[0]);
    }
    protected void ExportToExcel(object sender, EventArgs e)
    {

        DataTable dsProd1 = null;
        supptst dv = new supptst();
        //exc LstDoc = new exc();
        try
        {
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "StockistDetails.xls"));
            Response.ContentType = "application/ms-excel";
            dsProd1 = dv.getstckst_excel(div_code);
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
    public class supptst
    {
        public DataSet SuppliergetSubDiv(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSubDiv = null;

            string strQry = " select a.S_No,a.S_Name,a.Contact_Person,a.Mobile,a.Erp_Code,a.Act_flg,a.subdivision_code from" +
                     " supplier_master a WHERE a.Act_flg=0 and a.Division_Code= '" + divcode + "'group by a.S_No,a.S_Name,a.Contact_Person,a.Mobile,a.Erp_Code,a.Act_flg,a.subdivision_code order by a.S_Name";
            try
            {
                dsSubDiv = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsSubDiv;
        }
        public DataTable getstckst_excel(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();
            string sub = "";
            DataTable dsStockist = null;

            string strQry = "exec [Stockistdetailexcel] '" + div_code + "'";
            try
            {
                dsStockist = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsStockist;
        }
    }
}