using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
using Newtonsoft.Json;
using System.Web.Services;
using DBase_EReport;


public partial class MasterFiles_MR_ListedDoctor_ListedDR_DetailAdd : System.Web.UI.Page
{
    #region "Declaration"
    DataSet dsListedDR = null;
    DataSet dsDivision = null;
    DataSet dsTerritory = null;
    DataSet newcontactDR = null;
    string state_cd = string.Empty;
    string sf_type = string.Empty;
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
    string routenos = string.Empty;
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
    protected void Page_Load(object sender, EventArgs e)
    {
        divcode.Value = Session["div_code"].ToString();
        div_code = Session["div_code"].ToString();
        try
        {
            sf_code = Session["T_code"].ToString();
            Terr_Code = Request.QueryString["terrcode"];
        }
        catch (Exception)
        {

        }
        doctorcode = Request.QueryString["ListedDrCode"];

        Num();
        if (Session["sf_type"].ToString() == "1")
        {
            sf_code = Session["sf_code"].ToString();
            // UserControl_MR_Menu Usc_MR =
            //(UserControl_MR_Menu)LoadControl("~/UserControl/MR_Menu.ascx");
            // Divid.Controls.Add(Usc_MR);
            // Usc_MR.Title = this.Page.Title;
            lblTerrritory.Text = "( " + "<span style='font-weight: bold;color:Maroon;'>For " + Session["sf_Name"] + " </span>" + " - " +

                                "<span style='font-weight: bold;color:Maroon;'>  " + Session["Terr_Name"] + "</span>" + " )";
            btnBack.Visible = false;

        }
        else
        {
            try
            {
                sf_code = Session["T_code"].ToString();
            }
            catch (Exception)
            {

            }

            //UserControl_MenuUserControl Usc_Menu =
            // (UserControl_MenuUserControl)LoadControl("~/UserControl/MenuUserControl.ascx");
            //Divid.Controls.Add(Usc_Menu);
            //Divid.FindControl("btnBack").Visible = false;
            //Usc_Menu.Title = this.Page.Title;
            //menu1.Visible = false;
            Session["backurl"] = "../Retailer_Details.aspx";
            lblTerrritory.Text = "( " + "<span style='font-weight: bold;color:Maroon;'>For " + Session["sf_Name"] + " </span>" + " - " +

                                 "<span style='font-weight: bold;color:Maroon;'>  " + Session["Terr_Name"] + "</span>" + " )";
        }
        if (!Page.IsPostBack)
        {
            Session["backurl"] = "../Retailer_Details.aspx";
            //menu1.Title = this.Page.Title;

            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);
            FillView();
            FillTerritory();
            FillSpeciality();
            FillCatagry();
            FillClass();
            FillState();
            Call_Date();
            GetWorkName();
            FillUOM();
            FillFzyofVisit();
            FillddlCC();
            FillType(div_code);
            if (Request.QueryString["type"] != null)
            {
                if ((Request.QueryString["type"].ToString() == "1") || (Request.QueryString["type"].ToString() == "2"))
                {
                    request_doctor = Convert.ToString(Request.QueryString["ListedDrCode"]);
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

            lblTerritory.Text = "<span style='Color:Red'>" + "*" + "</span>" + dsTerritory.Tables[0].Rows[0]["wrk_area_Name"].ToString();
        }
    }
    private void Num()
    {
        int num = 0;
        ListedDR sk = new ListedDR();
        dsListedDR = sk.getCheck(div_code);
        num = Convert.ToInt32(dsListedDR.Tables[0].Rows[0].ItemArray.GetValue(0).ToString());

        Txt_id.Text = (num + 1).ToString();


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
    private void FillUOM()
    {
        Division dv = new Division();
        dsTerritory = dv.getStatePerDivision(div_code);
        if (dsTerritory.Tables[0].Rows.Count > 0)
        {
            State st = new State();

            dsTerritory = st.getUOMChkBox(div_code);
            ddl_uom.DataTextField = "Move_MailFolder_Name";
            ddl_uom.DataValueField = "Move_MailFolder_Id";
            ddl_uom.DataSource = dsTerritory;
            ddl_uom.DataBind();

        }
    }
    public void FillView()
    {
        if (div_code == "4")
        {
            Lab_Milk_Potential.Visible = true;
            Txt_Mil_Pot.Visible = true;
            Lab_UOM.Visible = true;
            ddl_uom.Visible = true;
        }
        else
        {

        }

    }

    private void LoadDoctor(int request_type, string request_doctor)
    {
        ListedDR lst = new ListedDR();
        lisdr lst1 = new lisdr();
        if (div_code == "70")
        {
            newcontactDR = lst.ViewnewcontactDr(request_doctor, div_code);
            if (newcontactDR.Tables[0].Rows.Count > 0)
            {
                Listed_DR_Code = newcontactDR.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                txtName.Text = newcontactDR.Tables[0].Rows[0].ItemArray.GetValue(3).ToString();
                txtMobile.Text = newcontactDR.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();
                Txt_id.Text = newcontactDR.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                txtERBCode.Text = newcontactDR.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                txtDOW.Text = newcontactDR.Tables[0].Rows[0].ItemArray.GetValue(4).ToString();
                ddlSpec.SelectedValue = newcontactDR.Tables[0].Rows[0].ItemArray.GetValue(5).ToString();
                //ddlSpec.SelectedItem.Text = newcontactDR.Tables[0].Rows[0].ItemArray.GetValue(6).ToString();

                salestaxno.Text = newcontactDR.Tables[0].Rows[0].ItemArray.GetValue(7).ToString();
                TinNO.Text = newcontactDR.Tables[0].Rows[0].ItemArray.GetValue(8).ToString();
                ddlTerritory.SelectedValue = newcontactDR.Tables[0].Rows[0].ItemArray.GetValue(9).ToString();
                terri.SelectedValue = newcontactDR.Tables[0].Rows[0].ItemArray.GetValue(9).ToString();
                //ddlTerritory.SelectedItem.Text = newcontactDR.Tables[0].Rows[0].ItemArray.GetValue(15).ToString();
                creditdays.Text = newcontactDR.Tables[0].Rows[0].ItemArray.GetValue(34).ToString();
                ddlClass.SelectedValue = newcontactDR.Tables[0].Rows[0].ItemArray.GetValue(11).ToString();
                ddlClass.SelectedItem.Text = newcontactDR.Tables[0].Rows[0].ItemArray.GetValue(12).ToString();
                Txt_advanceamt.Text = newcontactDR.Tables[0].Rows[0].ItemArray.GetValue(13).ToString();
                Txt_Mil_Pot.Text = newcontactDR.Tables[0].Rows[0].ItemArray.GetValue(14).ToString();
                ddl_uom.SelectedValue = newcontactDR.Tables[0].Rows[0].ItemArray.GetValue(15).ToString();
                ddl_uom.SelectedItem.Text = newcontactDR.Tables[0].Rows[0].ItemArray.GetValue(16).ToString();
                txtAddress.Text = newcontactDR.Tables[0].Rows[0].ItemArray.GetValue(17).ToString();
                txtStreet.Text = newcontactDR.Tables[0].Rows[0].ItemArray.GetValue(18).ToString();
                DDL_Re_Type.SelectedValue = newcontactDR.Tables[0].Rows[0].ItemArray.GetValue(19).ToString();
                txtoutstanding.Text = newcontactDR.Tables[0].Rows[0].ItemArray.GetValue(21).ToString();
                txtcreditlimit.Text = newcontactDR.Tables[0].Rows[0].ItemArray.GetValue(22).ToString();
                RblAlt.SelectedValue = newcontactDR.Tables[0].Rows[0].ItemArray.GetValue(23).ToString();
                DDL_category.SelectedValue = newcontactDR.Tables[0].Rows[0].ItemArray.GetValue(24).ToString();
                txtlat.Text = newcontactDR.Tables[0].Rows[0].ItemArray.GetValue(25).ToString();
                txtlong.Text = newcontactDR.Tables[0].Rows[0].ItemArray.GetValue(26).ToString();
                txtDMP.Text = newcontactDR.Tables[0].Rows[0].ItemArray.GetValue(27).ToString();
                txtmonA.Text = newcontactDR.Tables[0].Rows[0].ItemArray.GetValue(28).ToString();
                txtMFPM.Text = newcontactDR.Tables[0].Rows[0].ItemArray.GetValue(29).ToString();
                txtMCL.Text = newcontactDR.Tables[0].Rows[0].ItemArray.GetValue(30).ToString();
                ddlfzy.SelectedValue = newcontactDR.Tables[0].Rows[0].ItemArray.GetValue(31).ToString();
                hdnbreedname.Value = newcontactDR.Tables[0].Rows[0].ItemArray.GetValue(32).ToString();
                ddlCC.SelectedValue = newcontactDR.Tables[0].Rows[0].ItemArray.GetValue(33).ToString();
                hdnukey.Value = newcontactDR.Tables[0].Rows[0].ItemArray.GetValue(35).ToString();
                txtmail.Text = newcontactDR.Tables[0].Rows[0].ItemArray.GetValue(36).ToString();
            }

        }
        else
        {
            dsListedDR = lst1.ViewListedDr(request_doctor);
            if (dsListedDR.Tables[0].Rows.Count > 0)
            {
                Listed_DR_Code = dsListedDR.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                txtName.Text = dsListedDR.Tables[0].Rows[0].ItemArray.GetValue(3).ToString();
                txtMobile.Text = dsListedDR.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();
                Txt_id.Text = dsListedDR.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                txtERBCode.Text = dsListedDR.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                txtDOW.Text = dsListedDR.Tables[0].Rows[0].ItemArray.GetValue(4).ToString();
                ddlSpec.SelectedValue = dsListedDR.Tables[0].Rows[0].ItemArray.GetValue(5).ToString();
                //ddlSpec.SelectedItem.Text = dsListedDR.Tables[0].Rows[0].ItemArray.GetValue(6).ToString();

                salestaxno.Text = dsListedDR.Tables[0].Rows[0].ItemArray.GetValue(7).ToString();
                TinNO.Text = dsListedDR.Tables[0].Rows[0].ItemArray.GetValue(8).ToString();
                ddlTerritory.SelectedValue = dsListedDR.Tables[0].Rows[0].ItemArray.GetValue(9).ToString();
                terri.SelectedValue = dsListedDR.Tables[0].Rows[0].ItemArray.GetValue(9).ToString();
                //ddlTerritory.SelectedItem.Text = dsListedDR.Tables[0].Rows[0].ItemArray.GetValue(15).ToString();
                creditdays.Text = dsListedDR.Tables[0].Rows[0].ItemArray.GetValue(10).ToString();
                ddlClass.SelectedValue = dsListedDR.Tables[0].Rows[0].ItemArray.GetValue(11).ToString();
                ddlClass.SelectedItem.Text = dsListedDR.Tables[0].Rows[0].ItemArray.GetValue(12).ToString();
                Txt_advanceamt.Text = dsListedDR.Tables[0].Rows[0].ItemArray.GetValue(13).ToString();
                Txt_Mil_Pot.Text = dsListedDR.Tables[0].Rows[0].ItemArray.GetValue(14).ToString();
                ddl_uom.SelectedValue = dsListedDR.Tables[0].Rows[0].ItemArray.GetValue(15).ToString();
                ddl_uom.SelectedItem.Text = dsListedDR.Tables[0].Rows[0].ItemArray.GetValue(16).ToString();
                txtAddress.Text = dsListedDR.Tables[0].Rows[0].ItemArray.GetValue(17).ToString();
                txtStreet.Text = dsListedDR.Tables[0].Rows[0].ItemArray.GetValue(18).ToString();
                DDL_Re_Type.SelectedValue = dsListedDR.Tables[0].Rows[0].ItemArray.GetValue(19).ToString();
                txtoutstanding.Text = dsListedDR.Tables[0].Rows[0].ItemArray.GetValue(21).ToString();
                txtcreditlimit.Text = dsListedDR.Tables[0].Rows[0].ItemArray.GetValue(22).ToString();
                RblAlt.SelectedValue = dsListedDR.Tables[0].Rows[0].ItemArray.GetValue(23).ToString();
                DDL_category.SelectedValue = dsListedDR.Tables[0].Rows[0].ItemArray.GetValue(24).ToString();
                txtlat.Text = dsListedDR.Tables[0].Rows[0].ItemArray.GetValue(25).ToString();
                txtlong.Text = dsListedDR.Tables[0].Rows[0].ItemArray.GetValue(26).ToString();
                txtmail.Text = dsListedDR.Tables[0].Rows[0].ItemArray.GetValue(27).ToString();

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
    }



    private void FillType(string div_code)
    {
        ListedDR sk = new ListedDR();
        dsListedDR = sk.getIn_Type(div_code);
        if (dsListedDR.Tables[0].Rows.Count > 0)
        {
            DDL_Re_Type.DataTextField = "Name";
            DDL_Re_Type.DataValueField = "Type_Id";
            DDL_Re_Type.DataSource = dsListedDR;
            DDL_Re_Type.DataBind();
            //DDL_Re_Type.Controls.Add("")
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
    private void FillFzyofVisit()
    {
        ListedDR lstDR = new ListedDR();
        dsListedDR = lstDR.get_frequencyofvisit(div_code);
        ddlfzy.DataTextField = "name";
        ddlfzy.DataValueField = "id";
        ddlfzy.DataSource = dsListedDR;
        ddlfzy.DataBind();
    }
    [WebMethod(EnableSession = true)]
    public static clsbreed[] Fillddlbreed()
    {
        // string Stockist_Code = HttpContext.Current.Session["Sf_Code"].ToString();
        string Div_code = HttpContext.Current.Session["div_code"].ToString();
        DataSet dsAlowtype = null;
        ListedDR sm = new ListedDR();
        List<clsbreed> pid = new List<clsbreed>();
        dsAlowtype = sm.get_breed(Div_code);

        foreach (DataRow row in dsAlowtype.Tables[0].Rows)
        {
            clsbreed p = new clsbreed();
            p.name = row["name"].ToString();
            p.id = row["id"].ToString();
            pid.Add(p);
        }
        return pid.ToArray();
    }

    public class clsbreed
    {
        public string name { get; set; }
        public string id { get; set; }

    }


    private void FillddlCC()
    {
        ListedDR lstDR = new ListedDR();
        dsListedDR = lstDR.get_curentCompetitor(div_code);
        ddlCC.DataTextField = "name";
        ddlCC.DataValueField = "id";
        ddlCC.DataSource = dsListedDR;
        ddlCC.DataBind();
    }
    private void FillTerritory()
    {
        ListedDR lstDR = new ListedDR();
        //ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('" + sf_code + "');</script>");
        dsListedDR = lstDR.FetchTerritory(div_code);
        ddlTerritory.DataTextField = "Territory_Name";
        ddlTerritory.DataValueField = "Territory_Code";
        ddlTerritory.DataSource = dsListedDR;
        ddlTerritory.DataBind();
        terri.DataTextField = "Territory_Name";
        terri.DataValueField = "Territory_Code";
        terri.DataSource = dsListedDR;
        terri.DataBind(); 
        if (dsListedDR.Tables[0].Rows.Count <= 1)
        {

            //ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Territory must be created prior to Customer creation');</script>");
        }

    }



    private void FillSpeciality()
    {
        ListedDR lstDR = new ListedDR();
        dsListedDR = lstDR.FetchSpeciality(div_code);
        ddlSpec.DataTextField = "Doc_Special_Name";
        ddlSpec.DataValueField = "Doc_Special_Code";
        ddlSpec.DataSource = dsListedDR;
        ddlSpec.DataBind();
    }


    private void FillCatagry()
    {
        ListedDR lstDR = new ListedDR();
        dsListedDR = lstDR.FetchCatagory(div_code);
        DDL_category.DataTextField = "Doc_Cat_Name";
        DDL_category.DataValueField = "Doc_Cat_Code";
        DDL_category.DataSource = dsListedDR;
        DDL_category.DataBind();
    }


    private void FillClass()
    {
        ListedDR lstDR = new ListedDR();
        dsListedDR = lstDR.FetchClass(div_code);
        ddlClass.DataTextField = "Doc_ClsName";
        ddlClass.DataValueField = "Doc_ClsCode";
        ddlClass.DataSource = dsListedDR;
        ddlClass.DataBind();
    }

    public int Checkroutenos()
    {
        DB_EReporting db = new DB_EReporting();
        string DR_Terr = ddlTerritory.SelectedValue;
        DataSet ds = db.Exec_DataSet("Select count(ListeddrCode)Numbers from  Mas_ListedDr wHERE  Territory_Code='" + DR_Terr + "'  and  Division_Code='" + div_code + "'  ");
        int routenos = Convert.ToInt16(ds.Tables[0].Rows[0]["Numbers"]);
        return routenos;
    }
    
    public int masoutletcount()
    {
        DB_EReporting db = new DB_EReporting();
        string DR_Terr = ddlTerritory.SelectedValue;
        DataSet ds = db.Exec_DataSet("Select maxrouteOutlet Numbers  from Access_Master Where division_code='" + div_code + "'");
        int routenum = Convert.ToInt16(ds.Tables[0].Rows[0]["Numbers"]);
        return routenum;
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);

        string DR_Name = txtName.Text.Trim();
        string Mobile_No = txtMobile.Text.Trim();
        string retail_code = Txt_id.Text.Trim();
        string erbCode = string.Empty;

        if (txtERBCode.Text == string.Empty)
        {
            erbCode = retail_code;
        }
        else
        {
            erbCode = txtERBCode.Text;
        }
        string advance_amount = txtDOW.Text.Trim();
        string DR_Spec = ddlSpec.SelectedValue;
        string dr_spec_name = ddlSpec.SelectedItem.ToString();
        string sales_Tax = salestaxno.Text.Trim();
        string Tinno = TinNO.Text.Trim();
        string DR_Terr = ddlTerritory.SelectedValue;
        string oldTerr = terri.SelectedValue;
        //string dr_terr_name = ddlTerritory.SelectedItem.ToString();
        string credit_days = creditdays.Text.Trim();
        string DR_Class = ddlClass.SelectedValue;
        string drcategory = DDL_category.SelectedValue;
        string dscategoryName = DDL_category.SelectedItem.ToString();
        string dr_class_name = ddlClass.SelectedItem.ToString();
        string ad = Txt_advanceamt.Text.Trim();
        string DR_Address1 = txtAddress.Text.ToString().Trim();
        string DR_Address2 = txtStreet.Text.Trim();
        string Milk_pon = Txt_Mil_Pot.Text.Trim();
        string UOM_Name = ddl_uom.SelectedItem.ToString();
        string UOM = ddl_uom.SelectedValue;
        string outstandng = txtoutstanding.Text.Trim();
        string creditlmt = txtcreditlimit.Text.Trim();
        string Cus_Alter = Convert.ToString(RblAlt.SelectedItem.Value);
        string latitude = txtlat.Text.Trim();
        string longitude = txtlong.Text.Trim();
        string DFDairyMP = txtDMP.Text.Trim();
        string MonthlyAI = txtmonA.Text.Trim();
        string MCCNFPM = txtMFPM.Text.Trim();
        string MCCMilkColDaily = txtMCL.Text.Trim();
        string FrequencyOfVisit = ddlfzy.SelectedValue;
        string Breed = hdnbreedname.Value;
        string ukeys = hdnukey.Value;
        string curentCom = ddlCC.SelectedValue;
        string curentCompitat = ddlCC.SelectedItem.Text.Trim();
        //if ((DR_Name.Trim().Length > 0) && (retail_code.Trim().Length > 0) && (DR_Spec.Trim().Length > 0) && (DR_Class.Trim().Length > 0) && (DR_Terr.Trim().Length > 0))
        if (retail_code != "" && DR_Name != "" && DR_Address1 != "")
        {
            if (doctorcode == null)
            { // Add New Listed Doctor
                ListedDR lstDR = new ListedDR();
                if (div_code == "109")
                {                                
                    int routecount = Checkroutenos();
                    int allotroutno = masoutletcount();
                    if (routecount >= allotroutno)
                    {
                        string message = "The seleted Route has more than alloted customers,Select Another Route.";
                        System.Text.StringBuilder sb = new System.Text.StringBuilder();
                        sb.Append("<script type = 'text/javascript'>");
                        sb.Append("window.onload=function(){");
                        sb.Append("alert('");
                        sb.Append(message);
                        sb.Append("')};");
                        sb.Append("</script>");
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", sb.ToString());
                        return;
                    }
                }

                // iReturn = lstDR.RecordAdd(DR_Name, sf_code, Mobile_No, retail_code, advance_amount, DR_Spec, dr_spec_name, sales_Tax, Tinno, DR_Terr, credit_days, DR_Class, dr_class_name, ad, DR_Address1, DR_Address2, div_code, Milk_pon, UOM, UOM_Name, DDL_Re_Type.SelectedValue, outstandng, creditlmt, Cus_Alter, drcategory, dscategoryName, erbCode);
                if (div_code == "70")
                    iReturn = lstDR.RecordAdd11(DR_Name, curentCompitat, sf_code, Mobile_No, retail_code, advance_amount, DR_Spec, dr_spec_name, sales_Tax, Tinno, DR_Terr, credit_days, DR_Class, dr_class_name, ad, DR_Address1, DR_Address2, div_code, Milk_pon, UOM, UOM_Name, DDL_Re_Type.SelectedValue, outstandng, creditlmt, Cus_Alter, drcategory, dscategoryName, erbCode, latitude, longitude, DFDairyMP, MonthlyAI, MCCNFPM, MCCMilkColDaily, FrequencyOfVisit, Breed, curentCom, txtmail.Text);

                else
                    iReturn = lstDR.RecordAdd(DR_Name, sf_code, Mobile_No, retail_code, advance_amount, DR_Spec, dr_spec_name, sales_Tax, Tinno, DR_Terr, credit_days, DR_Class, dr_class_name, ad, DR_Address1, DR_Address2, div_code, Milk_pon, UOM, UOM_Name, DDL_Re_Type.SelectedValue, outstandng, creditlmt, Cus_Alter, drcategory, dscategoryName, erbCode, latitude, longitude, txtmail.Text);
                if (iReturn > 0)
                {

                    // menu1.Status = "Sub Division created Successfully ";
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Created Successfully');</script>");
                    btnClear_Click(sender, e);
                }
                else if (iReturn == -2)
                {

                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert(' Name Already Exist');</script>");

                }
                else if (iReturn == -3)
                {
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert(' Code Already Exist');</script>");

                }
                else if (iReturn == -4)
                {
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert(' ERP Code Already Emxist');</script>");

                }
            }
            else
            {
                if (div_code == "109")
                {
                    if (oldTerr != DR_Terr)
                    {
                        int routecount = Checkroutenos();
                        int allotroutno = masoutletcount();
                        if (routecount >= allotroutno)
                        {
                            string message = "The seleted Route has more than alloted customers,Select Another Route.";
                            System.Text.StringBuilder sb = new System.Text.StringBuilder();
                            sb.Append("<script type = 'text/javascript'>");
                            sb.Append("window.onload=function(){");
                            sb.Append("alert('");
                            sb.Append(message);
                            sb.Append("')};");
                            sb.Append("</script>");
                            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", sb.ToString());
                            return;
                        }

                    }
                }


                ListedDR lstDR = new ListedDR();
                string subdivcode = Convert.ToString(doctorcode);
                //iReturn = lstDR.Recordupdate_detail(subdivcode, DR_Name, sf_code, Mobile_No, retail_code, advance_amount, DR_Spec, dr_spec_name, sales_Tax, Tinno, DR_Terr, credit_days, DR_Class, dr_class_name, ad, DR_Address1, DR_Address2, div_code, Milk_pon, UOM, UOM_Name, DDL_Re_Type.SelectedValue, outstandng, creditlmt, Cus_Alter, drcategory, dscategoryName, erbCode);
                if (div_code == "70")
                    iReturn = lstDR.Recordupdate_detail1(subdivcode, curentCompitat, DR_Name, sf_code, Mobile_No, retail_code, advance_amount, DR_Spec, dr_spec_name, sales_Tax, Tinno, DR_Terr, credit_days, DR_Class, dr_class_name, ad, DR_Address1, DR_Address2, div_code, Milk_pon, UOM, UOM_Name, DDL_Re_Type.SelectedValue, outstandng, creditlmt, Cus_Alter, drcategory, dscategoryName, erbCode, latitude, longitude, DFDairyMP, MonthlyAI, MCCNFPM, MCCMilkColDaily, FrequencyOfVisit, Breed, curentCom, ukeys, txtmail.Text);
                else
                    iReturn = lstDR.Recordupdate_detail(subdivcode, DR_Name, sf_code, Mobile_No, retail_code, advance_amount, DR_Spec, dr_spec_name, sales_Tax, Tinno, DR_Terr, credit_days, DR_Class, dr_class_name, ad, DR_Address1, DR_Address2, div_code, Milk_pon, UOM, UOM_Name, DDL_Re_Type.SelectedValue, outstandng, creditlmt, Cus_Alter, drcategory, dscategoryName, erbCode, latitude, longitude, txtmail.Text);//31
                if (iReturn == 1)
                {
                    // menu1.Status = "Sub Division Updated Successfully ";
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Updated Successfully');window.location='../Retailer_Details.aspx';</script>");
                }
                else if (iReturn == -2)
                {

                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert(' Name Already Exist');</script>");

                }
                else if (iReturn == -3)
                {
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert(' Code Already Exist');</script>");

                }
                else if (iReturn == -4)
                {
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert(' ERP Code Already Exist');</script>");

                }

            }


        }
        else
        {

            //ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Invalid Data!!!');</script>");
            if (retail_code == "")
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Enter Retailer Code');</script>");
                Txt_id.Focus();
            }
            else if (DR_Name == "")
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Enter Retailer Name');</script>");
                txtName.Focus();
            }
            else if (DR_Address1 == "")
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Enter Address...');</script>");
                txtAddress.Focus();
            }

        }

    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        txtName.Text = "";
        txtMobile.Text = "";
        Txt_id.Text = "";
        txtERBCode.Text = "";
        txtDOW.Text = "";
        salestaxno.Text = "";
        TinNO.Text = "";
        creditdays.Text = "";
        Txt_advanceamt.Text = "";
        txtAddress.Text = "";
        txtStreet.Text = "";
        Txt_Mil_Pot.Text = "";
        txtlat.Text = "";
        txtlong.Text = "";
        FillTerritory();
        FillSpeciality();
        FillCatagry();
        FillClass();
        FillState();
        FillUOM();
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        try
        {
            Server.Transfer("../Retailer_Details.aspx");
        }
        catch (Exception ex)
        {

        }
    }
    public class lisdr
    {
        public DataSet ViewListedDr(string drcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            string strQry = " SELECT d.ListedDrCode,d.Code,isnull(d.ListedDr_Mobile,d.ListedDr_phone) ListedDr_Mobile,d.ListedDr_Name,d.contactperson Contact_Person_Name,d.Doc_Special_Code,d.Doc_Spec_ShortName, " +
                    " d.Tin_No,d.Sales_Taxno,d.Territory_Code,d.Credit_Days,d.Doc_ClsCode,d.Doc_Class_ShortName,d.Advance_amount,d.Milk_Potential,d.UOM,d.UOM_Name,  " +
                    " d.ListedDr_Address1,d.ListedDr_Address2,Retailer_Type,stuff((select ', '+territory_Name from Mas_Territory_Creation t where t.Territory_Code = d.Territory_Code " +
                    " and charindex(cast(t.Territory_Code as varchar)+',',d.Territory_Code+',')>0 for XML path('')),1,2,'') territory_Name ,Outstanding,Credit_Limit,Cus_Alter,d.Doc_Cat_Code,d.ListedDr_Class_Patients,d.ListedDr_Consultation_Fee,ListedDr_Email" +
                    " FROM  Mas_ListedDr d WHERE d.ListedDrCode =  '" + drcode + "'  and d.ListedDr_Active_Flag = 0 ";


            try
            {
                dsListedDR = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsListedDR;
        }
    }
}