using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using Bus_EReport;
public partial class MasterFiles_Doc_Class_React : System.Web.UI.Page
{
    #region "Declaration"
    DataSet dsDocCls = null;
    int DocClsCode = 0;
    string divcode = string.Empty;
    string Doc_Cls_SName = string.Empty;
    string DocClsName = string.Empty;
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        divcode = Convert.ToString(Session["div_code"]);
        Session["backurl"] = "DoctorClassList.aspx";
        if (!Page.IsPostBack)
        {
            FillDocCls();
            menu1.Title = this.Page.Title;
            
        }
    }
    private void FillDocCls()
    {
        Doctor dv = new Doctor();
        dsDocCls = dv.getDocCls_Re(divcode);
        if (dsDocCls.Tables[0].Rows.Count > 0)
        {
            grdDocCls.Visible = true;
            grdDocCls.DataSource = dsDocCls;
            grdDocCls.DataBind();
        }
        else
        {
            grdDocCls.DataSource = dsDocCls;
            grdDocCls.DataBind();
        }
    }
    protected void grdDocCls_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Reactivate")
        {
            DocClsCode = Convert.ToInt16(e.CommandArgument);

            //Deactivate
            Doctor dv = new Doctor();
            int iReturn = dv.ReActivateCls(DocClsCode);
            if (iReturn > 0)
            {
                // menu1.Status = "Doctor Class has been Reactivated Successfully";
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Reactivated Successfully');</script>");
            }
            else
            {
                // menu1.Status = "Unable to Reactivate";
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Unable to Reactivate');</script>");
            }
            FillDocCls();
        }
    }
    protected void grdDocCls_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdDocCls.PageIndex = e.NewPageIndex;
        FillDocCls();
    }
}