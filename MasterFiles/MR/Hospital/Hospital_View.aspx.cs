using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;

public partial class MasterFiles_MR_Hospital_Hospital_View : System.Web.UI.Page
{
    string sf_code = string.Empty;
    string hosp_code = string.Empty;
    string div_code = string.Empty;
    DataSet dsHospital = null;
    DataSet dsTerritory = null;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int time;
    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        hosp_code = Convert.ToString(Request.QueryString["Hospital_Code"]);
        if (Session["sf_type"].ToString() == "1")
        {
            sf_code = Session["sf_code"].ToString();
            UserControl_MR_Menu Usc_MR =
   (UserControl_MR_Menu)LoadControl("~/UserControl/MR_Menu.ascx");
            Divid.Controls.Add(Usc_MR);
            Usc_MR.Title = this.Page.Title;
            lblTerrritory.Text = "( " + "<span style='font-weight: bold;color:Maroon;'>For " + Session["sfName"] + " </span>" + " - " +
                                         "<span style='font-weight: bold;color:Maroon;'> " + Session["sf_Designation_Short_Name"] + " </span>" + " - " +
                                          "<span style='font-weight: bold;color:Maroon;'>  " + Session["sf_HQ"] + "</span>" + " )";
            btnBack.Visible = false;

        }
        else
        {
            sf_code = Session["sf_code"].ToString();
            UserControl_MenuUserControl c1 =
         (UserControl_MenuUserControl)LoadControl("~/UserControl/MenuUserControl.ascx");
            Divid.Controls.Add(c1);
            c1.Title = this.Page.Title;
            btnBack.Visible = false;
            Session["backurl"] = "HospitalList.aspx";
            lblTerrritory.Text = "( " + "<span style='font-weight: bold;color:Maroon;'>For " + Session["sfName"] + " </span>" + " - " +
                               "<span style='font-weight: bold;color:Maroon;'> " + Session["sf_Designation_Short_Name"] + " </span>" + " - " +
                                "<span style='font-weight: bold;color:Maroon;'>  " + Session["sf_HQ"] + "</span>" + " )";
        }
        if (!Page.IsPostBack)
        {
            Session["backurl"] = "HospitalList.aspx";
          //  menu1.Title = this.Page.Title;
            GetWorkName();
            FillHospital();
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

    private void GetWorkName()
    {
        Territory terr = new Territory();
        dsTerritory = terr.getWorkAreaName(div_code);
        if (dsTerritory.Tables[0].Rows.Count > 0)
        {
            lblTerritory.Text = dsTerritory.Tables[0].Rows[0]["wrk_area_Name"].ToString();
        }
    }
    private void FillHospital()
    {
        Hospital hosp = new Hospital();
        dsHospital = hosp.getHospital (sf_code, hosp_code);
        if (dsHospital.Tables[0].Rows.Count > 0)
        {
            txtName.Text = dsHospital.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
            txtAddress.Text = dsHospital.Tables[0].Rows[0].ItemArray.GetValue(3).ToString();
            txtContact.Text = dsHospital.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();
            txtPhone.Text = dsHospital.Tables[0].Rows[0].ItemArray.GetValue(4).ToString();
            txtTerritory.Text = dsHospital.Tables[0].Rows[0].ItemArray.GetValue(5).ToString();
        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        try
        {
            Server.Transfer("HospitalList.aspx");
        }
        catch (Exception ex)
        {

        }
    }

}