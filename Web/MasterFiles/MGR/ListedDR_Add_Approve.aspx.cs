using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;

public partial class MasterFiles_MGR_ListedDR_Add_Approve : System.Web.UI.Page
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
        if (div_code.Contains(','))
            div_code = div_code.Substring(0, div_code.Length - 1);

        dsDoc = LstDoc.getListedDr_MGR(sfCode,2,div_code);
        if (dsDoc.Tables[0].Rows.Count > 0)
        {
            grdListedDR.Visible = true;
            grdListedDR.DataSource = dsDoc;
            grdListedDR.DataBind();
        }
        else
        {
            grdListedDR.DataSource = dsDoc;
            grdListedDR.DataBind();
        }
    }

    //protected void grdListedDR_RowCommand(object sender, GridViewCommandEventArgs e)
    //{
    //    if (e.CommandName == "Approve")
    //    {
    //        sf_code = Convert.ToString(e.CommandArgument);

    //        //Approve
    //        ListedDR LstDoc = new ListedDR();
    //        int iReturn = LstDoc.ApproveAdd(sf_code, 0, 2);
    //        if (iReturn > 0)
    //        {
    //           // menu1.Status = "Listed Doctor has been Approved Successfully";
    //            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Listed Doctor has been Approved Successfully');</script>");
    //        }
    //        else
    //        {
    //            //menu1.Status = "Unable to Approve";
    //            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Unable to Approve');</script>");
    //        }

    //        FillDoc();
    //    }
    //}

}