using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;

public partial class MasterFiles_MR_UnListedDoctor_UnListedDr_View : System.Web.UI.Page
{
    #region "Declaration"
    DataSet dsListedDR = null;
    DataSet dsDivision = null;
    DataSet dsTerritory = null;
    string state_cd = string.Empty;
    string sState = string.Empty;
    string[] statecd;
    string Listed_DR_Code = string.Empty;
    string Listed_DR_Name = string.Empty;
    string Listed_DR_Address = string.Empty;
    string Listed_DR_Catg = string.Empty;
    string Listed_DR_Spec = string.Empty;
    string Listed_DR_Class = string.Empty;
    string Listed_DR_Qual = string.Empty;
    string Listed_DR_Terr = string.Empty;
    string Catg_Code = string.Empty;
    string Spec_Code = string.Empty;
    string Doc_ClsCode = string.Empty;
    string Doc_QuaCode = string.Empty; 
    string Terr_Code = string.Empty;
    string sf_code = string.Empty;
    int request_type = -1;
    string request_doctor = string.Empty;
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
        if (Session["sf_type"].ToString() == "1")
        {
            sf_code = Session["sf_code"].ToString();
            //menu1.Visible = true;
            //menu1.FindControl("btnBack").Visible = true;
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
           // menu1.Visible = false;
            UserControl_MenuUserControl c1 =
              (UserControl_MenuUserControl)LoadControl("~/UserControl/MenuUserControl.ascx");
            Divid.Controls.Add(c1);
            c1.Title = this.Page.Title;
            btnBack.Visible = false;
            Session["backurl"] = "UnLstDoctorList.aspx";
            lblTerrritory.Text = "( " + "<span style='font-weight: bold;color:Maroon;'>For " + Session["sfName"] + " </span>" + " - " +
                               "<span style='font-weight: bold;color:Maroon;'> " + Session["sf_Designation_Short_Name"] + " </span>" + " - " +
                                "<span style='font-weight: bold;color:Maroon;'>  " + Session["sf_HQ"] + "</span>" + " )";
        }
       // sf_code = Session["sf_code"].ToString();
        if (!Page.IsPostBack)
        {
            Session["backurl"] = "UnLstDoctorList.aspx";
          //  menu1.Title = this.Page.Title;
            FillCategory();
            FillTerritory();
            FillSpeciality();
            FillClass();
            GetWorkName();
            FillQualification();
            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);
            if (Request.QueryString["type"] != null)
            {
                if ((Request.QueryString["type"].ToString() == "1") || (Request.QueryString["type"].ToString() == "2"))
                {
                    request_doctor = Convert.ToString(Request.QueryString["UnListedDrCode"]);
                    request_type = Convert.ToInt16(Request.QueryString["type"]);
                    LoadDoctor(request_type, request_doctor);
                }
            }
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
    private void LoadDoctor(int request_type, string request_doctor)
    {
        UnListedDR lst = new UnListedDR();

        dsListedDR = lst.ViewListedDr(request_doctor);
        if (dsListedDR.Tables[0].Rows.Count > 0)
        {
            Listed_DR_Code = dsListedDR.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
            txtName.Text = dsListedDR.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
            txtAddress1.Text = dsListedDR.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();
            ddlCatg.SelectedValue  = dsListedDR.Tables[0].Rows[0].ItemArray.GetValue(3).ToString();
            ddlCatg.SelectedItem.Text = dsListedDR.Tables[0].Rows[0].ItemArray.GetValue(4).ToString();
            ddlSpec.SelectedValue = dsListedDR.Tables[0].Rows[0].ItemArray.GetValue(5).ToString();
            ddlSpec.SelectedItem.Text = dsListedDR.Tables[0].Rows[0].ItemArray.GetValue(6).ToString();
            //ddlQual.SelectedValue = dsListedDR.Tables[0].Rows[0].ItemArray.GetValue(7).ToString();
            //ddlQual.SelectedItem.Text = dsListedDR.Tables[0].Rows[0].ItemArray.GetValue(8).ToString();
            ddlClass.SelectedValue = dsListedDR.Tables[0].Rows[0].ItemArray.GetValue(9).ToString();
            ddlClass.SelectedItem.Text = dsListedDR.Tables[0].Rows[0].ItemArray.GetValue(10).ToString();
            ddlTerritory.SelectedValue = dsListedDR.Tables[0].Rows[0].ItemArray.GetValue(11).ToString();
            ddlTerritory.SelectedItem.Text = dsListedDR.Tables[0].Rows[0].ItemArray.GetValue(12).ToString();
        }

        if (request_type == 1)
        {
            txtAddress1.Enabled = false;
            txtName.Enabled = false;
            ddlCatg.Enabled = false;
            ddlClass.Enabled = false;
            //ddlQual.Enabled = false;
            ddlSpec.Enabled = false;
            ddlTerritory.Enabled = false;
            btnSave.Enabled = false;
            btnClear.Enabled = false;
        }
    }


    private void FillTerritory()
    {
        UnListedDR lstDR = new UnListedDR();
        dsListedDR = lstDR.FetchTerritory(sf_code);
        ddlTerritory.DataTextField = "Territory_Name";
        ddlTerritory.DataValueField = "Territory_Code";
        ddlTerritory.DataSource = dsListedDR;
        ddlTerritory.DataBind();
        if (dsListedDR.Tables[0].Rows.Count <= 1)
        {
            Response.Redirect("../Territory/TerritoryCreation.aspx");
            //menu1.Status = "Territory must be created prior to Doctor creation";
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Territory must be created prior to Doctor creation');</script>");
        }

    }

    private void FillCategory()
    {
        UnListedDR lstDR = new UnListedDR();
        dsListedDR = lstDR.FetchCategory(sf_code);
        ddlCatg.DataTextField = "Doc_Cat_Name";
        ddlCatg.DataValueField = "Doc_Cat_Code";
        ddlCatg.DataSource = dsListedDR;
        ddlCatg.DataBind();
    }

    private void FillSpeciality()
    {
        UnListedDR lstDR = new UnListedDR();
        dsListedDR = lstDR.FetchSpeciality(sf_code);
        ddlSpec.DataTextField = "Doc_Special_Name";
        ddlSpec.DataValueField = "Doc_Special_Code";
        ddlSpec.DataSource = dsListedDR;
        ddlSpec.DataBind();
    }

    private void FillClass()
    {
        UnListedDR lstDR = new UnListedDR();
        dsListedDR = lstDR.FetchClass(sf_code);
        ddlClass.DataTextField = "Doc_ClsName";
        ddlClass.DataValueField = "Doc_ClsCode";
        ddlClass.DataSource = dsListedDR;
        ddlClass.DataBind();
    }

    private void FillQualification()
    {
        UnListedDR lstDR = new UnListedDR();
        dsListedDR = lstDR.FetchQualification(sf_code);
        //ddlQual.DataTextField = "Doc_QuaName";
        //ddlQual.DataValueField = "Doc_QuaCode";
        //ddlQual.DataSource = dsListedDR;
        //ddlQual.DataBind();
    }


    protected void btnSave_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        string DR_Name = txtName.Text.Trim();
        //string DR_Qual = ddlQual.SelectedValue;
        string DR_Spec = ddlSpec.SelectedValue;
        string DR_Catg = ddlCatg.SelectedValue;
        string DR_Terr = ddlTerritory.SelectedValue;
        string DR_Class = ddlClass.SelectedValue;
        string DR_Address1 = txtAddress1.Text.ToString();
        request_doctor = Convert.ToString(Request.QueryString["UnListedDrCode"]);

        //if ((DR_Name.Trim().Length > 0) && (DR_Address1.Trim().Length > 0) && (DR_Catg.Trim().Length > 0) && (DR_Spec.Trim().Length > 0) && (DR_Qual.Trim().Length > 0) && (DR_Class.Trim().Length > 0) && (DR_Terr.Trim().Length > 0))
        //{
        //    // Add New Listed Doctor
        //    UnListedDR lstDR = new UnListedDR();
        //    iReturn = lstDR.RecordUpdate(request_doctor, DR_Name, DR_Address1, DR_Catg, DR_Spec, DR_Qual, DR_Class, DR_Terr, Session["sf_code"].ToString());

        //    if (iReturn != -1)
        //    {
        //        //menu1.Status = "UnListed Doctor Updated Successfully!!";
        //        ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Updated Successfully');</script>");
        //        //FillListedDR();
        //    }

        //}
        //else
        //{
        //    //menu1.Status = "Enter all the values!!";
        //}

    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
       
        FillCategory();
        FillTerritory();
        FillSpeciality();
        FillClass();
        FillQualification(); 
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        try
        {
            Server.Transfer("UnLstDoctorList.aspx");
        }
        catch (Exception ex)
        {

        }
    }
}