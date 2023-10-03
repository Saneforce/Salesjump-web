using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;

public partial class MasterFiles_BulkEditDocClass : System.Web.UI.Page
{
    #region "Declaration"
    DataSet dsDocCat = null;
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
            Session["backurl"] = "DoctorClassList.aspx";
            menu1.Title = this.Page.Title;
            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);
            FillDocSpe();
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
    private void FillDocSpe()
    {
        Doctor dv = new Doctor();
        dsDocCat = dv.getDocCls(div_code);
        if (dsDocCat.Tables[0].Rows.Count > 0)
        {
            grdDocCat.Visible = true;
            grdDocCat.DataSource = dsDocCat;
            grdDocCat.DataBind();
        }
        else
        {
            btnSubmit.Visible = false;
            grdDocCat.DataSource = dsDocCat;
            grdDocCat.DataBind();
        }
    }


    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        string doc_cat_code = string.Empty;
        string doc_cat_sname = string.Empty;
        string doc_cat_name = string.Empty;
        Doctor dv = new Doctor();
        int iReturn = -1;
        bool err = false;
        foreach (GridViewRow gridRow in grdDocCat.Rows)
        {
            Label lblDocCatCode = (Label)gridRow.Cells[1].FindControl("lblDocCatCode");
            doc_cat_code = lblDocCatCode.Text.ToString();
            TextBox txtDoc_Cat_SName = (TextBox)gridRow.Cells[1].FindControl("txtDoc_Cat_SName");
            doc_cat_sname = txtDoc_Cat_SName.Text.ToString();
            TextBox txtDocCatName = (TextBox)gridRow.Cells[1].FindControl("txtDocCatName");
            doc_cat_name = txtDocCatName.Text.ToString();
            iReturn = dv.RecordUpdateCls(Convert.ToInt16(doc_cat_code), doc_cat_sname, doc_cat_name, div_code);

            if (iReturn > 0)
                err = false;

            if((iReturn == -2))
            {
                txtDocCatName.Focus();
                err = true ;
                break;
            }
            if ((iReturn == -3))
            {
                txtDoc_Cat_SName.Focus();
                err = true;
                break;
            }
        }

        if ( err ==false )
        {
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Updated Successfully');window.location='DoctorClassList.aspx';</script>");
        }
        else if (err == true)
        {
            if (iReturn == -2)
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Class Name Already Exist');</script>");
            }
            else if (iReturn == -3)
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Short Name Already Exist');</script>");
            }
        }
    }
}