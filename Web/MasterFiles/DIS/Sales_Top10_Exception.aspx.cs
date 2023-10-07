using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;


public partial class MIS_Reports_Sales_Top10_Exception : System.Web.UI.Page
{
    string div_code = string.Empty;
    DataSet dsmgrsf = new DataSet();
    DataSet dsTP = null;
    string tot_dr = string.Empty;
    string sf_code = string.Empty;
    string FMonth = string.Empty;
    string FYear = string.Empty;
    DataSet dsSf = null;
    string tot_dcr_dr = string.Empty;
    string sCurrentDate = string.Empty;
    string sf_type = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {

        div_code = Session["div_code"].ToString();
        sf_code = Session["sf_code"].ToString();


        if (Session["sf_type"].ToString() == "1")
        {
            UserControl_MR_Menu c1 =
                (UserControl_MR_Menu)LoadControl("~/UserControl/MR_Menu.ascx");
            Divid.Controls.Add(c1);
            c1.FindControl("btnBack").Visible = false;
            c1.Title = this.Page.Title;
            ViewState["sf_type"] = "";
            SalesForce sf = new SalesForce();
            dsSf = sf.getReportingTo(sf_code);
            if (dsSf.Tables[0].Rows.Count > 0)
            {
                sf_code = dsSf.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
            }

            if (!Page.IsPostBack)
            {

            }



        }

        else if (Session["sf_type"].ToString() == "2")
        {
            UserControl_MGR_Menu c1 =
            (UserControl_MGR_Menu)LoadControl("~/UserControl/MGR_Menu.ascx");
            Divid.Controls.Add(c1);
            c1.FindControl("btnBack").Visible = false;
            c1.Title = this.Page.Title;
            ViewState["sf_type"] = "";
            if (!Page.IsPostBack)
            {

            }

        }
        else
        {
            ViewState["sf_type"] = "admin";
            UserControl_MenuUserControl c1 =
            (UserControl_MenuUserControl)LoadControl("~/UserControl/MenuUserControl.ascx");
            Divid.Controls.Add(c1);
            c1.Title = "Monthly DCR Analysis";
            c1.FindControl("btnBack").Visible = false;
            c1.Title = this.Page.Title;


        }


        if (!Page.IsPostBack)
        {

            TourPlan tp = new TourPlan();
            dsTP = tp.Get_TP_Edit_Year(div_code);
            if (dsTP.Tables[0].Rows.Count > 0)
            {
                for (int k = Convert.ToInt16(dsTP.Tables[0].Rows[0]["Year"]); k <= DateTime.Now.Year + 1; k++)
                {
                    ddlFYear.Items.Add(k.ToString());
                    ddlFYear.SelectedValue = DateTime.Now.Year.ToString();

                }
            }



        }

    }
    protected void btnGo_Click(object sender, EventArgs e)
    {
        string sURL = string.Empty;

        sURL = "rptSales_Top10_Exception.aspx?FYear=" + ddlFYear.SelectedValue.ToString() + "&viewby=" + viewdrop.SelectedItem.Text + "&viewtop=" + topdrop.SelectedItem.Text;


        string newWin = "window.open('" + sURL + "',null,'resizable=yes,toolbar=no,scrollbars=yes,menubar=no,status=no,width=800,height=600,left=0,top=0');";
        ClientScript.RegisterStartupScript(this.GetType(), "pop", newWin, true);

    }
}