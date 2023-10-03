using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
public partial class MasterFiles_Doc_Spec_React : System.Web.UI.Page
{
    #region "Declaration"
    DataSet dsDocSpe = null;
    int DocSpeCode = 0;
    string divcode = string.Empty;
    string DocSpe_SName = string.Empty;
    string DocSpeName = string.Empty;
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        divcode = Convert.ToString(Session["div_code"]);
        Session["backurl"] = "DoctorSpecialityList.aspx";
        if (!Page.IsPostBack)
        {
            FillDocSpe();
            menu1.Title = this.Page.Title;
          
        }
    }
    private void FillDocSpe()
    {

        Doctor dv = new Doctor();
        dsDocSpe = dv.getDocSpe_Re(divcode);
        if (dsDocSpe.Tables[0].Rows.Count > 0)
        {
            grdDocSpe.Visible = true;
            grdDocSpe.DataSource = dsDocSpe;
            grdDocSpe.DataBind();
        }
        else
        {
            grdDocSpe.DataSource = dsDocSpe;
            grdDocSpe.DataBind();
        }
    }
    protected void grdDocSpe_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Reactivate")
        {
            DocSpeCode = Convert.ToInt16(e.CommandArgument);

            //Deactivate
            Doctor dv = new Doctor();
            int iReturn = dv.RectivateDocSpl(DocSpeCode);
            if (iReturn > 0)
            {
                //menu1.Status = "Doctor Speciality has been Deactivated Successfully";
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Reactivated Successfully');</script>");
            }
            else
            {
                // menu1.Status = "Unable to Deactivate";
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Unable to Reactivate');</script>");
            }
            FillDocSpe();
        }
    }
    protected void grdDocSpe_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdDocSpe.PageIndex = e.NewPageIndex;
        FillDocSpe();
    }
}