using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bus_EReport;
using System.Data;
using System.Data.SqlClient;


public partial class Birthday_Wish : System.Web.UI.Page
{

    #region Declaration
    string sfCode = string.Empty;
    string sf_code = string.Empty;
    DataSet dsSalesForce = null;
    public static string baseUrl = "";
    #endregion

    #region Page_Load
    protected void Page_Load(object sender, EventArgs e)
    {
        baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority + Request.ApplicationPath.TrimEnd('/') + "/";
        if ((Convert.ToString(Session["sf_code"]) != null || Convert.ToString(Session["sf_code"]) != ""))
        {
            LblUser.Text = "Dear " + Session["sf_name"];
            sfCode = Session["sf_code"].ToString();
            if (!Page.IsPostBack)
            {
                SalesForce sf = new SalesForce();
                dsSalesForce = sf.getSalesForce(sfCode);
                if (dsSalesForce.Tables[0].Rows.Count > 0)
                {

                    lbldob.Text = dsSalesForce.Tables[0].Rows[0].ItemArray.GetValue(13).ToString(); // DOB
                }
            }
        }
        else { Page.Response.Redirect(baseUrl, true); }
    }
    #endregion

    #region btnHome_Click
    protected void btnHome_Click(object sender, EventArgs e)
    {
        int Count;
        AdminSetup admin = new AdminSetup();

        Count = admin.get_Mail_MR_MGR_Count(Session["sf_code"].ToString());

        if (Count != 0) // MR Login
        {
            Response.Redirect("MasterFiles/Mails/Mail_Head.aspx");
        }
        else // MGR Login
        {
            Response.Redirect("~/MasterFiles/Rejection_ReEntries.aspx");
        }
    }
    #endregion

    #region btnLogout_Click
    protected void btnLogout_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Index.aspx");
    }
    #endregion
}