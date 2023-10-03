using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
using System.Drawing.Imaging;
public partial class MasterFiles_SalesForce_React : System.Web.UI.Page
{
    DataSet dsSalesForce = null;
    DataSet dsDivision = null;
    string sf_design = string.Empty;
    string sState = string.Empty;
    string div_code = string.Empty;
    string[] statecd;
    string state_cd = string.Empty;
    string sf_code = string.Empty;
    string sf_name = string.Empty;
    string usr_name = string.Empty;
    string search = string.Empty;
    string state_code = string.Empty;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int time;
    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        Session["backurl"] = "SalesForceList.aspx";
        if (!Page.IsPostBack)
        {
            FillSalesForce();
            menu1.Title = this.Page.Title;
            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);
        }
    }

    protected override void OnLoadComplete(EventArgs e)
    {
        ServerEndTime = DateTime.Now;
        TrackPageTime();
    }

    public void TrackPageTime()
    {
        TimeSpan serverTimeDiff = ServerEndTime.Subtract(ServerStartTime);
        time = serverTimeDiff.Minutes;
    }

    private void FillSalesForce()
    {
        SalesForce sf = new SalesForce();
        dsSalesForce = sf.getSalesForce_React(div_code);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            grdSalesForce.Visible = true;
            grdSalesForce.DataSource = dsSalesForce;
            grdSalesForce.DataBind();
        }
        else
        {
            grdSalesForce.DataSource = dsSalesForce;
            grdSalesForce.DataBind();
        }

    }

    protected void grdSalesForce_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Reactivate")
        {
            string sf_code = Convert.ToString(e.CommandArgument); 
            SalesForce sales = new SalesForce();
            int iReturn = sales.Reactivate_Sales(sf_code);
            if (iReturn > 0)
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Reactivated Successfully');</script>");
            }
            else
            {
                ClientScript.RegisterStartupScript(GetType(), "TestAlert", "alert(\'Unable to Reactivate.\');", true);
            }
            FillSalesForce();
        }
    }

    protected void grdSalesForce_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            Label lblS = (Label)e.Row.FindControl("lblS");

            if (lblS.Text == "1")
            {
                lblS.Text = "From Vacant";
                lblS.ForeColor = System.Drawing.Color.Red;
                lblS.Style.Add("font-size", "9pt");
                lblS.Style.Add("font-weight", "Bold");
            }
            else if (lblS.Text == "0")
            {
                lblS.Text = "From Active";
                lblS.ForeColor = System.Drawing.Color.Red;
                lblS.Style.Add("font-size", "9pt");
                lblS.Style.Add("font-weight", "Bold");
            }

            //ListedDR lstdr = new ListedDR();
            //DataSet dsdr = new DataSet();
        }
    }
}