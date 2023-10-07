using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;

public partial class MasterFiles_BulkEditDoc_Qua : System.Web.UI.Page
{
    #region "Declaration"
    DataSet dsDocQua = null;
    string div_code = string.Empty;
    int i;
    int iReturn = -1;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int time;
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        if (!Page.IsPostBack)
        {
            Session["backurl"] = "DoctorQualificationList.aspx";
            menu1.Title = this.Page.Title;
            FillDocQua();
            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);
        }
    }

    private void FillDocQua()
    {
        Doctor dv = new Doctor();
        dsDocQua = dv.getDocQua(div_code);
        if (dsDocQua.Tables[0].Rows.Count > 0)
        {
            grdDocQua.Visible = true;
            grdDocQua.DataSource = dsDocQua;
            grdDocQua.DataBind();
        }
        else
        {
            btnSubmit.Visible = false;
            grdDocQua.DataSource = dsDocQua;
            grdDocQua.DataBind();
        }
    }
    protected override void OnLoadComplete(EventArgs e)
    {

        ServerEndTime = DateTime.Now;
        TrackPageTime();//It will give you page load time  
    }
    public void TrackPageTime()
    {
        TimeSpan serverTimeDiff = ServerEndTime.Subtract(ServerStartTime);
        time = serverTimeDiff.Minutes;

    } 

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        string doc_Qua_code = string.Empty;
        string doc_Qua_sname = string.Empty;
        string doc_Qua_name = string.Empty;
        Doctor dv = new Doctor();
        int iReturn = -1;
        bool err = false;
        foreach (GridViewRow gridRow in grdDocQua.Rows)
        {
            Label lblDocQuaCode = (Label)gridRow.Cells[1].FindControl("lblDocQuaCode");
            doc_Qua_code = lblDocQuaCode.Text.ToString();
           // TextBox txtDoc_Qua_SName = (TextBox)gridRow.Cells[1].FindControl("txtDoc_Qua_SName");
           // doc_Qua_sname = txtDoc_Qua_SName.Text.ToString();
            TextBox txtDocQuaName = (TextBox)gridRow.Cells[1].FindControl("txtDocQuaName");
            doc_Qua_name = txtDocQuaName.Text.ToString();
            iReturn = dv.RecordUpdateQua(Convert.ToInt16(doc_Qua_code), doc_Qua_sname, doc_Qua_name, div_code);

            if (iReturn > 0)
                err = false;
            if ((iReturn == -2))
            {
                txtDocQuaName.Focus();
                err = true;
                break;
            }
        }

        if (err==false)
        {
           // menu1.Status = "Doctor Qualification(s) have been updated Successfully";
        
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Updated Successfully');window.location='DoctorQualificationList.aspx';</script>");
        }
        else if (err == true)
        {
            if (iReturn == -2)
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Qualification Name Already Exist');</script>");
            }
            
        }
    }
}