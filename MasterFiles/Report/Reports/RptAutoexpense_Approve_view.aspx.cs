using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using Bus_EReport;
public partial class MasterFiles_Subdiv_Salesforcewise : System.Web.UI.Page
{
 string sf_type = string.Empty;
 protected override void OnPreInit(EventArgs e)
 {
     base.OnPreInit(e);
     sf_type = Session["sf_type"].ToString();
     if (sf_type == "3")
     {
         this.MasterPageFile = "~/Master.master";
     }
     else if (sf_type == "2")
     {
         this.MasterPageFile = "~/Master_MGR.master";
     }
     else if (sf_type == "1")
     {
         this.MasterPageFile = "~/Master_MR.master";
     }
 }
   
    DataSet dsSubDivision = null;
    int subdivcode = 0;
    int subdivision_code = 0;
    string divcode = string.Empty;
    string sfcode = string.Empty;
    string subdiv_sname = string.Empty;
    string subdiv_name = string.Empty;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int time;
   
   
	
    protected void Page_Load(object sender, EventArgs e)
    {
        divcode = Convert.ToString(Session["div_code"]);

        if (Session["sf_type"].ToString() == "1")
        {
            sfcode = Session["sf_code"].ToString();
            //UserControl_MR_Menu Usc_MR =
            //(UserControl_MR_Menu)LoadControl("~/UserControl/MR_Menu.ascx");
            //Divid.Controls.Add(Usc_MR);
            //Usc_MR.Title = this.Page.Title;
            //Usc_MR.FindControl("btnBack").Visible = false;
            //lblTerrritory.Text = "<span style='font-weight: bold;color:Blue;font-names:verdana'>HQ :</span>" +
              //                   "<span style='font-weight: bold;color:Red'>  " + Session["sf_HQ"] + "</span>";
            //btnBack.Visible = true;

        }
        else
        {
            sfcode = Session["sf_code"].ToString();
            //UserControl_MGR_Menu c1 =
             //(UserControl_MGR_Menu)LoadControl("~/UserControl/MGR_Menu.ascx");
            //Divid.Controls.Add(c1);
            //c1.FindControl("btnBack").Visible = false;
            //btnBack.Visible = true;
            //c1.Title = this.Page.Title;
            //   Session["backurl"] = "LstDoctorList.aspx";
            //lblTerrritory.Text = "<span style='font-weight: bold;color:Blue;font-names:verdana'>HQ :</span>" +
              //                   "<span style='font-weight: bold;color:Red'>  " + Session["sf_HQ"] + "</span>";

        }
         


        //sfcode = Convert.ToString(Session["Sf_Code"]); 
        if (!Page.IsPostBack)
        {
            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);
            //menu1.Title = this.Page.Title;
            //menu1.FindControl("btnBack").Visible = false;
            FillFieldForcediv(divcode);
            ddlSubdiv.Focus();
            GetMyMonthList();
            bind_year_ddl();
        }
    }
    public void GetMyMonthList()
    {
        DateTime month = Convert.ToDateTime("1/1/2012");
        for (int i = 0; i < 12; i++)
        {
            DateTime nextMonth = month.AddMonths(i);
            ListItem list = new ListItem();
            list.Text = nextMonth.ToString("MMMM");
            list.Value = nextMonth.Month.ToString();
            monthId.Items.Add(list);
        }
        monthId.Items.Insert(0, new ListItem("  Select Month  ", "0"));
    }

    private void bind_year_ddl()
    {
        int year = (System.DateTime.Now.Year);
        for (int intCount = 2015; intCount <= year + 1; intCount++)
        {
            yearID.Items.Add(intCount.ToString());
        }
        yearID.Items.Insert(0, new ListItem("  Select Year  ", "0"));
    }
    protected void ddlSubdiv_SelectedIndexChanged(object sender, EventArgs e)
    {
        sfcode = ddlSubdiv.SelectedValue;
        
    }
    private void FillFieldForcediv(string divcode)
    {
        
        Distance_calculation dv = new Distance_calculation();
        DataTable typeDT=dv.getFieldForce(divcode, sfcode);
        sf_type=typeDT.Rows[0]["sf_type"].ToString();
        if (typeDT.Rows.Count > 0 && sf_type == "1")
        {
            ddlSubdiv.Visible = false;
        }
        else
        {
            ddlSubdiv.Visible = true;
        }
        dsSubDivision = dv.getMR(sfcode,divcode);
        if (dsSubDivision.Tables[0].Rows.Count > 0)
        {
            ddlSubdiv.DataTextField = "sf_name";
            ddlSubdiv.DataValueField = "sf_code";
            ddlSubdiv.DataSource = dsSubDivision;
            ddlSubdiv.DataBind();
        }
        sfcode = ddlSubdiv.SelectedValue;

    }
    
    protected void btnSF_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, typeof(Page), "newWindow", "window.open('RptAutoexpense_zoom.aspx?month=" + monthId.SelectedValue.ToString() + "&year=" + yearID.SelectedValue.ToString() + "&divCode=" + divcode + "&sf_code=" + sfcode + "','_blank','resizable=yes,scrollbars=1,toolbar=no,menubar=no,status=no,width=800,height=600,left=0,top=0');", true);
        //Response.Write("<script>window.open('RptAutoexpense_zoom.aspx?month=" + monthId.SelectedValue.ToString() + "&year=" + yearID.SelectedValue.ToString() + "&divCode=" + divcode + "&sf_code=" + sfcode + "','_blank');</script>");
    }
}