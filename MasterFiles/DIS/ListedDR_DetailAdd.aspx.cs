using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;

public partial class MasterFiles_MR_ListedDoctor_ListedDR_DetailAdd : System.Web.UI.Page
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
    string Qual_Code = string.Empty;
    string Terr_Code = string.Empty;
    string sf_code = string.Empty;
    string listeddrcode = string.Empty;
    string ListedDrCode = string.Empty;
    int request_type = -1;
    string request_doctor = string.Empty;
    int i;
    int iReturn = -1;
    string doctorcode = string.Empty;
    string div_code = string.Empty;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int time;
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["Division_Code"].ToString().Replace(",", "");
        sf_code = Session["sf_code"].ToString();
        doctorcode = Request.QueryString["ListedDrCode"];
		if (Session["sf_type"].ToString() == "3")
        {
            UserControl_DIS_Menu c3 =
                        (UserControl_DIS_Menu)LoadControl("~/UserControl/DIS_Menu.ascx");
            Divid.Controls.Add(c3);
          
        }
        else
        if (Session["sf_type"].ToString() == "1")
        {
            sf_code = Session["sf_code"].ToString();
            UserControl_MR_Menu Usc_MR =
           (UserControl_MR_Menu)LoadControl("~/UserControl/MR_Menu.ascx");
            Divid.Controls.Add(Usc_MR);
            Usc_MR.Title = this.Page.Title;
            lblTerrritory.Text = "( " + "<span style='font-weight: bold;color:Maroon;'>For " + Session["sf_Name"] + " </span>" + " - " +

                                "<span style='font-weight: bold;color:Maroon;'>  " + Session["Terr_Name"] + "</span>" + " )";
            btnBack.Visible = false;

        }
        else
        {
            sf_code = Session["Sf_code"].ToString();
            UserControl_MenuUserControl Usc_Menu =
             (UserControl_MenuUserControl)LoadControl("~/UserControl/MenuUserControl.ascx");
            Divid.Controls.Add(Usc_Menu);
            Divid.FindControl("btnBack").Visible = false;
            Usc_Menu.Title = this.Page.Title;
            //menu1.Visible = false;
            Session["backurl"] = "LstDoctorList.aspx";
            lblTerrritory.Text = "( " + "<span style='font-weight: bold;color:Maroon;'>For " + Session["sf_Name"] + " </span>" + " - " +

                                 "<span style='font-weight: bold;color:Maroon;'>  " + Session["Terr_Name"] + "</span>" + " )";
        }
        if (!Page.IsPostBack)
        {
            Session["backurl"] = "LstDoctorList.aspx";
            //menu1.Title = this.Page.Title;
            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);
          
            FillTerritory();
            FillSpeciality();
            FillClass();
            FillState();
            Call_Date();
            GetWorkName();
            if (Request.QueryString["type"] != null)
            {
                if ((Request.QueryString["type"].ToString() == "1") || (Request.QueryString["type"].ToString() == "2"))
                {
                    request_doctor = Convert.ToString(Request.QueryString["ListedDrCode"]);
                    request_type = Convert.ToInt16(Request.QueryString["type"]);
                    LoadDoctor(request_type, request_doctor);
                }
			 else
                {
                    UserControl_DIS_Menu c3 =
                            (UserControl_DIS_Menu)LoadControl("~/UserControl/DIS_Menu.ascx");
                    Divid.Controls.Add(c3);
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

            lblTerritory.Text = "<span style='Color:Red'>" + "*" + "</span>" + dsTerritory.Tables[0].Rows[0]["wrk_area_Name"].ToString();
        }
    }
    protected void Call_Date()
    {
        //for (int i = 1; i <= 31; i++)
        //{
        //    ddlDobDate.Items.Add(i.ToString());
        //}

        //for (int i = 1; i <= 31; i++)
        //{
        //    ddlDowDate.Items.Add(i.ToString());
        //}

       
       
    }

    private void LoadDoctor(int request_type, string request_doctor)
    {
        ListedDR lst = new ListedDR();
        dsListedDR = lst.ViewListedDr(request_doctor);
        if (dsListedDR.Tables[0].Rows.Count > 0)
        {
            Listed_DR_Code = dsListedDR.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
            txtName.Text = dsListedDR.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();
            txtMobile.Text = dsListedDR.Tables[0].Rows[0].ItemArray.GetValue(17).ToString();
            Txt_id.Text = dsListedDR.Tables[0].Rows[0].ItemArray.GetValue(9).ToString();
            txtDOW.Text = dsListedDR.Tables[0].Rows[0].ItemArray.GetValue(11).ToString();
            ddlSpec.SelectedValue = dsListedDR.Tables[0].Rows[0].ItemArray.GetValue(6).ToString();
            ddlSpec.SelectedItem.Text = dsListedDR.Tables[0].Rows[0].ItemArray.GetValue(5).ToString();

            salestaxno.Text = dsListedDR.Tables[0].Rows[0].ItemArray.GetValue(13).ToString();
            TinNO.Text = dsListedDR.Tables[0].Rows[0].ItemArray.GetValue(12).ToString();
            ddlTerritory.SelectedValue = dsListedDR.Tables[0].Rows[0].ItemArray.GetValue(16).ToString();
            ddlTerritory.SelectedItem.Text = dsListedDR.Tables[0].Rows[0].ItemArray.GetValue(15).ToString();
            creditdays.Text = dsListedDR.Tables[0].Rows[0].ItemArray.GetValue(10).ToString();
            ddlClass.SelectedValue = dsListedDR.Tables[0].Rows[0].ItemArray.GetValue(8).ToString();
            ddlClass.SelectedItem.Text = dsListedDR.Tables[0].Rows[0].ItemArray.GetValue(7).ToString();
            Txt_advanceamt.Text = dsListedDR.Tables[0].Rows[0].ItemArray.GetValue(14).ToString();
            txtAddress.Text = dsListedDR.Tables[0].Rows[0].ItemArray.GetValue(3).ToString();
            txtStreet.Text = dsListedDR.Tables[0].Rows[0].ItemArray.GetValue(4).ToString();
        }

        if (request_type == 1)
        {
            txtAddress.Enabled = false;   
            txtDOW.Enabled = false;
            txtMobile.Enabled = false;          
            txtName.Enabled = false;    
           
            txtStreet.Enabled = false;          
            TinNO.Enabled = false;
            ddlClass.Enabled = false;
            Txt_id.Enabled = false;
            ddlSpec.Enabled = false;           
            ddlTerritory.Enabled = false;
            btnSave.Enabled = false;           
            btnClear.Visible = false;
            btnSave.Visible = false;

        }
    }

  

    
        

    private void FillState()
    {
        ListedDR lst = new ListedDR();
        string divcode = Convert.ToString(lst.Div_Code(Session["sf_code"].ToString()));
        Division dv = new Division();
        dsDivision = dv.getStatePerDivision(divcode);
        if (dsDivision.Tables[0].Rows.Count > 0)
        {
            int i = 0;
            state_cd = string.Empty;
            sState = dsDivision.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
            statecd = sState.Split(',');
            foreach (string st_cd in statecd)
            {
                if (i == 0)
                {
                    state_cd = state_cd + st_cd;
                }
                else
                {
                    if (st_cd.Trim().Length > 0)
                    {
                        state_cd = state_cd + "," + st_cd;
                    }
                }
                i++;
            }

          
           
        }
    }

    private void FillTerritory()
    {
        ListedDR lstDR = new ListedDR();
        dsListedDR = lstDR.FetchTerritory(sf_code);
        ddlTerritory.DataTextField = "Territory_Name";
        ddlTerritory.DataValueField = "Territory_Code";
        ddlTerritory.DataSource = dsListedDR;
        ddlTerritory.DataBind();
        if (dsListedDR.Tables[0].Rows.Count <= 1)
        {
           
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Territory must be created prior to Customer creation');</script>");
        }

    }

   

    private void FillSpeciality()
    {
        ListedDR lstDR = new ListedDR();
        dsListedDR = lstDR.FetchSpeciality(sf_code);
        ddlSpec.DataTextField = "Doc_Special_SName";
        ddlSpec.DataValueField = "Doc_Special_Code";
        ddlSpec.DataSource = dsListedDR;
        ddlSpec.DataBind();
    }

    
    private void FillClass()
    {
        ListedDR lstDR = new ListedDR();
        dsListedDR = lstDR.FetchClass(sf_code);
        ddlClass.DataTextField = "Doc_ClsSName";
        ddlClass.DataValueField = "Doc_ClsCode";
        ddlClass.DataSource = dsListedDR;
        ddlClass.DataBind();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);

        string DR_Name = txtName.Text.Trim();
        string Mobile_No = txtMobile.Text;
        string retail_code = Txt_id.Text;
        string advance_amount = txtDOW.Text;
        string DR_Spec = ddlSpec.SelectedValue;
        string dr_spec_name = ddlSpec.SelectedItem.ToString();
        string sales_Tax = salestaxno.Text.Trim();
        string Tinno = TinNO.Text.Trim();
        string DR_Terr = ddlTerritory.SelectedValue;
        //string dr_terr_name = ddlTerritory.SelectedItem.ToString();
        string credit_days = creditdays.Text.Trim();
        string DR_Class = ddlClass.SelectedValue;
        string dr_class_name = ddlClass.SelectedItem.ToString();
        string ad=Txt_advanceamt.Text.Trim();
        string DR_Address1 = txtAddress.Text.ToString();
        string DR_Address2 = txtStreet.Text.Trim();

        if ((DR_Name.Trim().Length > 0) && (Mobile_No.Trim().Length > 0) && (retail_code.Trim().Length > 0) && (DR_Spec.Trim().Length > 0) && (DR_Class.Trim().Length > 0) && (DR_Terr.Trim().Length > 0))
        {
            // Add New Listed Doctor
            ListedDR lstDR = new ListedDR();
            iReturn = lstDR.RecordAdd(DR_Name, sf_code, Mobile_No, retail_code, advance_amount, DR_Spec, dr_spec_name, sales_Tax, Tinno, DR_Terr, credit_days, DR_Class, dr_class_name, ad, DR_Address1, DR_Address2);

            if (iReturn > 0)
            {

                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Created Successfully');</script>");
                btnClear_Click(sender, e);
            }
            else if (iReturn == -2)
            {
                iReturn = lstDR.Recordupdate_detail(DR_Name, sf_code, Mobile_No, retail_code, advance_amount, DR_Spec, dr_spec_name, sales_Tax, Tinno, DR_Terr, credit_days, DR_Class, dr_class_name, ad, DR_Address1, DR_Address2);
                if (iReturn > 0)
                {
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Updated Successfully');</script>");
                }

            }
        }
        else
        {

            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Retailer Name Already Exist');</script>");

        }

    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        txtName.Text = "";
        txtMobile.Text = "";
        Txt_id.Text = "";
        txtDOW.Text = "";
        salestaxno.Text = "";
        TinNO.Text = "";
        creditdays.Text = "";
        Txt_advanceamt.Text = "";
        txtAddress.Text = "";
        txtStreet.Text = "";
        FillTerritory();
        FillSpeciality();
        FillClass();
        FillState();

    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        try
        {
            Server.Transfer("LstDoctorList.aspx");
        }
        catch (Exception ex)
        {

        }
    }
}