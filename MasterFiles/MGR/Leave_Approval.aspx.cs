using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
public partial class MasterFiles_MGR_Leave_Approval : System.Web.UI.Page
{
    #region "Declaration"
    DataSet dsDoc = null;
    DataSet dsAdminSetup = null;
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
            
            FillExp();
        }

    }
    private void FillExp()
    {
        
        TP_New tp = new TP_New();

        dsDoc = tp.get_Exp_Approval(div_code);
        if (dsDoc.Tables[0].Rows.Count > 0)
        {
            
            GridView1.Visible = true;
            GridView1.DataSource = dsDoc;
            GridView1.DataBind();

          
        }
        else
        {
            
            GridView1.Visible = true;
            GridView1.DataSource = dsDoc;
            GridView1.DataBind();
        }


    }
}