using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using Bus_EReport;
public partial class MasterFiles_Doc_Qua_React : System.Web.UI.Page
{
    #region "Declaration"
    DataSet dsDocQua = null;
    int DocQuaCode = 0;
    string divcode = string.Empty;
    string Doc_Qua_SName = string.Empty;
    string DocQuaName = string.Empty;
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        divcode = Convert.ToString(Session["div_code"]);
        if (!Page.IsPostBack)
        {
            Session["backurl"] = "DoctorQualificationList.aspx";
            FillDocQua();
            menu1.Title = this.Page.Title;            
        }
    }
    private void FillDocQua()
    {
        Doctor dv = new Doctor();
        dsDocQua = dv.getDocQua_Re(divcode);
        if (dsDocQua.Tables[0].Rows.Count > 0)
        {
            grdDocQua.Visible = true;
            grdDocQua.DataSource = dsDocQua;
            grdDocQua.DataBind();
        }
        else
        {            
            grdDocQua.DataSource = dsDocQua;
            grdDocQua.DataBind();
        }
    }
    protected void grdDocQua_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Reactivate")
        {
            DocQuaCode = Convert.ToInt16(e.CommandArgument);

            //Deactivate
            Doctor dv = new Doctor();
            int iReturn = dv.ReActivateQua(DocQuaCode);
            if (iReturn > 0)
            {
                //menu1.Status = "Doctor Qualification has been Reactivated Successfully";
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Reactivated Successfully');</script>");
            }
            else
            {
                // menu1.Status = "Unable to Deactivate";
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Unable to Reactivate');</script>");
            }
            FillDocQua();
        }
    }
    protected void grdDocQua_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdDocQua.PageIndex = e.NewPageIndex;
        FillDocQua();
    }
}