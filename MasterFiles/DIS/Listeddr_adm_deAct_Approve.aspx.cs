using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
public partial class MasterFiles_MGR_Listeddr_adm_deAct_Approve : System.Web.UI.Page
{
    #region "Declaration"
    DataSet dsDoc = null;
    string sfCode = string.Empty;
    string sf_code = string.Empty;
    string div_code = string.Empty;
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        sfCode = Session["sf_code"].ToString();
        div_code = Session["div_code"].ToString();
        if (!Page.IsPostBack)
        {
            FillDoc();
            menu1.Title = this.Page.Title;
            menu1.FindControl("btnBack").Visible = false;
        }
    }
    private void FillDoc()
    {
        grdListedDR.DataSource = null;
        grdListedDR.DataBind();

        ListedDR LstDoc = new ListedDR();
        dsDoc = LstDoc.getListedDr_MGR(sfCode, 3, div_code);
        if (dsDoc.Tables[0].Rows.Count > 0)
        {
            if (sfCode == "admin")
            {
                grdListedDR.Visible = true;
                grdListedDR.DataSource = dsDoc;
                grdListedDR.DataBind();
            }
        }
        else
        {
            grdListedDR.DataSource = dsDoc;
            grdListedDR.DataBind();
        }
    }
}