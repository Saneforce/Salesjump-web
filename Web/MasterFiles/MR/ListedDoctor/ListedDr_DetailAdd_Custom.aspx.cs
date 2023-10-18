using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Data;
using Bus_EReport;
using Newtonsoft.Json;
using System.Web.Services;
using DBase_EReport;
using System.Data.SqlClient;
using System.IO;
using Amazon;
using Amazon.S3;
using Amazon.S3.Transfer;
using Microsoft.Ajax.Utilities;
using Amazon.S3.Model;
using Amazon.Runtime;
using System.Net;
using System.Web.Script.Serialization;
using Amazon.Runtime.Internal;

public partial class MasterFiles_MR_ListedDoctor_ListedDr_DetailAdd_Custom : System.Web.UI.Page
{
    #region "Declaration"
    DataSet dsListedDR = null;
    DataSet dsDivision = null;
    DataSet dsTerritory = null;
    DataSet newcontactDR = null;
    public static string state_cd = string.Empty;
    public static string sf_type = string.Empty;
    public static string sState = string.Empty;
    public static string[] statecd;
    public static string Listed_DR_Code = string.Empty;
    public static string Listed_DR_Name = string.Empty;
    public static string Listed_DR_Address = string.Empty;
    public static string Listed_DR_Catg = string.Empty;
    public static string Listed_DR_Spec = string.Empty;
    public static string Listed_DR_Class = string.Empty;
    public static string Listed_DR_Qual = string.Empty;
    public static string Listed_DR_Terr = string.Empty;
    public static string Catg_Code = string.Empty;
    public static string Spec_Code = string.Empty;
    public static string Doc_ClsCode = string.Empty;
    public static string Qual_Code = string.Empty;
    public static string Terr_Code = string.Empty;
    public static string sf_code = string.Empty;
    public static string listeddrcode = string.Empty;
    public static string ListedDrCode = string.Empty;
    public static int request_type = -1;
    public static string request_doctor = string.Empty;
    public static int i;
    public static int iReturn = -1;
    public static string doctorcode = string.Empty;
    public static string div_code = string.Empty;
    //public static string divcode = string.Empty;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    public static int time;
    public static string SF_Code = string.Empty;
    public static string STATE = string.Empty;
    public static string HQ = string.Empty;
    public static string HQNm = string.Empty;
    public static string baseUrl = "";
    #endregion

    protected override void OnPreInit(EventArgs e)
    {
        baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority + Request.ApplicationPath.TrimEnd('/') + "/Index.apx";
        //if ((Convert.ToString(Session["sf_type"]) != null || Convert.ToString(Session["sf_type"]) != ""))
        //{
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
        //}
        //else { Page.Response.Redirect(baseUrl, true); }

    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority + Request.ApplicationPath.TrimEnd('/') + "/Index.apx";

        //if ((Convert.ToString(Session["div_code"]) != null || Convert.ToString(Session["div_code"]) != ""))
        //{
       
        div_code = Session["div_code"].ToString();
        try
        {
            sf_code = Session["Sf_Code"].ToString();
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
            lblTerrritory.Text = "( " + "<span style='font-weight: bold;color:Maroon;'>For " + Session["sf_Name"] + " </span>" + " - " + "<span style='font-weight: bold;color:Maroon;'>  " + Session["Terr_Name"] + "</span>" + " )";
            btnBack.Visible = false;

        }
        else
        {
            try
            {
                //sf_code = Session["
                //"].ToString();
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
        //}
        //else { Page.Response.Redirect(baseUrl, true); }

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

    private void FillUOM()
    {
        lisdr ldr = new lisdr();
        dsTerritory = ldr.getStatePerDivision(div_code);

        //Division dv = new Division();
        //dsTerritory = dv.getStatePerDivision(div_code);
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
        { }
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

        lisdr ldr = new lisdr();
        dsDivision = ldr.getStatePerDivision(divcode);

        //Division dv = new Division();
        //dsDivision = dv.getStatePerDivision(divcode);    

        if (dsDivision.Tables[0].Rows.Count > 0)
        {
            int i = 0;
            state_cd = string.Empty;
            sState = dsDivision.Tables[0].Rows[0]["state_code"].ToString();
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

    [WebMethod]
    public static string GetCustomFormsFieldsList(string divcode, string ModuleId)
    {
        DataSet ds = new DataSet();
        lisdr ad = new lisdr();
        ds = ad.GetCustomFormsFieldsData(divcode, ModuleId);
        //AdminSetup Ad = new AdminSetup();
        //ds = Ad.GetCustomFormsFieldsData(divcode, ModuleId);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }

    [WebMethod]
    public static string GetCustomFormsFieldsGroups(string divcode, string ModuleId)
    {
        DataSet ds = new DataSet();

        //AdminSetup Ad = new AdminSetup();

        lisdr Ad = new lisdr();

        ds = Ad.GetCustomFormsFieldsGroupData(divcode, ModuleId);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }


    [WebMethod]
    public static string GetCustomFormsSeclectionMastesList(string TableName)
    {
        DataSet ds = new DataSet();
        lisdr ad = new lisdr();

        string DivCode = Convert.ToString(HttpContext.Current.Session["div_Code"]);

        if ((DivCode == null || DivCode == ""))
        { DivCode = "0"; }

        ds = GetCustomMatersData(TableName, DivCode);
        DataTable dt = new DataTable();
        dt = ds.Tables[0];

        return JsonConvert.SerializeObject(dt);
    }

    public static DataSet GetCustomMatersData(string TableName, string Divcode)
    {
        DB_EReporting db_ER = new DB_EReporting();

        DataSet dsAdmin = null;

        //strQry = "EXEC [Get_CustomForms_SeclectionMastesList] '" + TableName + "' ,'" + ColumnsName + "'";

        string strQry = "EXEC [Get_CustomForms_MastesTablesData] '" + TableName + "' ," + Divcode + "";

        try
        {
            dsAdmin = db_ER.Exec_DataSet(strQry);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return dsAdmin;
    }




    [WebMethod]
    public static string GetBindCustomFieldData(string listeddrcode, string columnName)
    {
        //ListedDR lst = new ListedDR();
        lisdr lstd = new lisdr();
        DataSet ds = lstd.get_RetailerCustomField(listeddrcode, columnName, div_code);

        //DataSet ds = lst.get_RetailerCustomField(listeddrcode);
        return JsonConvert.SerializeObject(ds.Tables[0]);
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
        string DR_Spec = ddlSpec.SelectedValue.Trim();
        string dr_spec_name = ddlSpec.SelectedItem.ToString();
        string sales_Tax = salestaxno.Text.Trim();
        string Tinno = TinNO.Text.Trim();
        string DR_Terr = ddlTerritory.SelectedValue;
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

                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Name Already Exist');</script>");

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
            else
            {


                ListedDR lstDR = new ListedDR();
                string subdivcode = Convert.ToString(doctorcode);
                //iReturn = lstDR.Recordupdate_detail(subdivcode, DR_Name, sf_code, Mobile_No, retail_code, advance_amount, DR_Spec, dr_spec_name, sales_Tax, Tinno, DR_Terr, credit_days, DR_Class, dr_class_name, ad, DR_Address1, DR_Address2, div_code, Milk_pon, UOM, UOM_Name, DDL_Re_Type.SelectedValue, outstandng, creditlmt, Cus_Alter, drcategory, dscategoryName, erbCode);
                if (div_code == "70")
                    iReturn = lstDR.Recordupdate_detail1(subdivcode, curentCompitat, DR_Name, sf_code, Mobile_No, retail_code, advance_amount, DR_Spec, dr_spec_name, sales_Tax, Tinno, DR_Terr, credit_days, DR_Class, dr_class_name, ad, DR_Address1, DR_Address2, div_code, Milk_pon, UOM, UOM_Name, DDL_Re_Type.SelectedValue, outstandng, creditlmt, Cus_Alter, drcategory, dscategoryName, erbCode, latitude, longitude, DFDairyMP, MonthlyAI, MCCNFPM, MCCMilkColDaily, FrequencyOfVisit, Breed, curentCom, ukeys, txtmail.Text);
                else
                    iReturn = lstDR.Recordupdate_detail(subdivcode, DR_Name, sf_code, Mobile_No, retail_code, advance_amount, DR_Spec, dr_spec_name, sales_Tax, Tinno, DR_Terr, credit_days, DR_Class, dr_class_name, ad, DR_Address1, DR_Address2, div_code, Milk_pon, UOM, UOM_Name, DDL_Re_Type.SelectedValue, outstandng, creditlmt, Cus_Alter, drcategory, dscategoryName, erbCode, latitude, longitude, txtmail.Text);
                if (iReturn == 1)
                {
                    // menu1.Status = "Sub Division Updated Successfully ";
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Updated Successfully');window.location='../Retailer_Details.aspx';</script>");
                }
                else if (iReturn == -2)
                {

                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Name Already Exist');</script>");

                }
                else if (iReturn == -3)
                {
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Code Already Exist');</script>");

                }
                else if (iReturn == -4)
                {
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('ERP Code Already Exist');</script>");

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

    public class AddtionalfieldDetails
    {
        [JsonProperty("Fields")]
        public string Fields { get; set; }

        [JsonProperty("Values")]
        public string Values { get; set; }
    }

    public class AfileUploadDetails
    {
        [JsonProperty("FileName")]
        public string FileId { get; set; }

        [JsonProperty("FileName")]
        public string FileName { get; set; }
    }

    public class RetailerMainfld
    {
        [JsonProperty("DR_Name")]
        public string DR_Name { get; set; }

        [JsonProperty("Mobile_No")]
        public string Mobile_No { get; set; }

        [JsonProperty("retail_code")]
        public string retail_code { get; set; }

        [JsonProperty("ERBCode")]
        public string ERBCode { get; set; }

        [JsonProperty("advance_amount")]
        public string advance_amount { get; set; }

        [JsonProperty("DR_Spec")]
        public string DR_Spec { get; set; }

        [JsonProperty("dr_spec_name")]
        public string dr_spec_name { get; set; }

        [JsonProperty("sales_Tax")]
        public string sales_Tax { get; set; }

        [JsonProperty("Tinno")]
        public string Tinno { get; set; }

        [JsonProperty("DR_Terr")]
        public string DR_Terr { get; set; }

        //string dr_terr_name { get; set; }

        [JsonProperty("credit_days")]
        public string credit_days { get; set; }

        [JsonProperty("DR_Class")]
        public string DR_Class { get; set; }

        [JsonProperty("drcategory")]
        public string drcategory { get; set; }

        [JsonProperty("dscategoryName")]
        public string dscategoryName { get; set; }

        [JsonProperty("dr_class_name")]
        public string dr_class_name { get; set; }

        [JsonProperty("ad")]
        public string ad { get; set; }

        [JsonProperty("DR_Address1")]
        public string DR_Address1 { get; set; }

        [JsonProperty("DR_Address2")]
        public string DR_Address2 { get; set; }

        [JsonProperty("Milk_pon")]
        public string Milk_pon { get; set; }

        [JsonProperty("UOM_Name")]
        public string UOM_Name { get; set; }

        [JsonProperty("UOM")]
        public string UOM { get; set; }

        [JsonProperty("DDL_Re_Type")]
        public string DDL_Re_Type { get; set; }

        [JsonProperty("outstandng")]
        public string outstandng { get; set; }

        [JsonProperty("creditlmt")]
        public string creditlmt { get; set; }

        [JsonProperty("Cus_Alter")]
        public string Cus_Alter { get; set; }

        [JsonProperty("latitude")]
        public string latitude { get; set; }

        [JsonProperty("longitude")]
        public string longitude { get; set; }

        [JsonProperty("DFDairyMP")]
        public string DFDairyMP { get; set; }

        [JsonProperty("MonthlyAI")]
        public string MonthlyAI { get; set; }

        [JsonProperty("MCCNFPM")]
        public string MCCNFPM { get; set; }

        [JsonProperty("MCCMilkColDaily")]
        public string MCCMilkColDaily { get; set; }

        [JsonProperty("FrequencyOfVisit")]
        public string FrequencyOfVisit { get; set; }

        [JsonProperty("Breed")]
        public string Breed { get; set; }

        [JsonProperty("ukeys")]
        public string ukeys { get; set; }

        [JsonProperty("curentCom")]
        public string curentCom { get; set; }

        [JsonProperty("curentCompitat")]
        public string curentCompitat { get; set; }

        [JsonProperty("Email")]
        public string Email { get; set; }

        [JsonProperty("Additionsfld")]
        public List<AddtionalfieldDetails> Additionsfld { get; set; }

        [JsonProperty("Additionalfileud")]
        public List<AfileUploadDetails> Additionalfileud { get; set; }
    }

    [WebMethod(EnableSession = true)]
    public static string SaveAdditionalField(string fdata)
    {
        string msg = "";

        RetailerMainfld sd = JsonConvert.DeserializeObject<RetailerMainfld>(fdata);

        List<AddtionalfieldDetails> addfields = sd.Additionsfld;
        List<AfileUploadDetails> addfileuds = sd.Additionalfileud;

        string DR_Name = Convert.ToString(sd.DR_Name);
        string Mobile_No = Convert.ToString(sd.Mobile_No);
        string retail_code = Convert.ToString(sd.retail_code);

        string erbCode = "";
        if (sd.ERBCode == null || sd.ERBCode == "")
        { erbCode = retail_code; }
        else { erbCode = sd.ERBCode; }

        string advance_amount = Convert.ToString(sd.advance_amount);
        string DR_Spec = Convert.ToString(sd.DR_Spec);
        string dr_spec_name = Convert.ToString(sd.dr_spec_name);
        string sales_Tax = Convert.ToString(sd.sales_Tax);
        string Tinno = Convert.ToString(sd.Tinno);
        string DR_Terr = Convert.ToString(sd.DR_Terr);
        string credit_days = Convert.ToString(sd.credit_days);
        string DR_Class = Convert.ToString(sd.DR_Class);
        string drcategory = Convert.ToString(sd.drcategory);
        string dscategoryName = Convert.ToString(sd.dscategoryName);
        string dr_class_name = Convert.ToString(sd.dr_class_name);
        string ad = Convert.ToString(sd.ad);
        string DR_Address1 = Convert.ToString(sd.DR_Address1);
        string DR_Address2 = Convert.ToString(sd.DR_Address2);
        string Milk_pon = Convert.ToString(sd.Milk_pon);
        string UOM_Name = Convert.ToString(sd.UOM_Name);
        string UOM = Convert.ToString(sd.UOM);
        string DDL_Re_Type = Convert.ToString(sd.DDL_Re_Type);
        string outstandng = Convert.ToString(sd.outstandng);
        string creditlmt = Convert.ToString(sd.creditlmt);
        string Cus_Alter = Convert.ToString(sd.Cus_Alter);
        string latitude = Convert.ToString(sd.latitude);
        string longitude = Convert.ToString(sd.longitude);
        string DFDairyMP = Convert.ToString(sd.DFDairyMP);
        string MonthlyAI = Convert.ToString(sd.MonthlyAI);
        string MCCNFPM = Convert.ToString(sd.MCCNFPM);
        string MCCMilkColDaily = Convert.ToString(sd.MCCMilkColDaily);
        string FrequencyOfVisit = Convert.ToString(sd.FrequencyOfVisit);
        string Breed = Convert.ToString(sd.Breed);
        string ukeys = Convert.ToString(sd.ukeys);
        string curentCom = Convert.ToString(sd.curentCom);
        string curentCompitat = Convert.ToString(sd.curentCompitat);
        string Email = Convert.ToString(sd.Email);

        if (retail_code != "" && DR_Name != "" && DR_Address1 != "")
        {
            if (doctorcode == null)
            { // Add New Listed Doctor
              //
              //ListedDR lstDR = new ListedDR();
                lisdr lstdr = new lisdr();
                // iReturn = lstDR.RecordAdd(DR_Name, sf_code, Mobile_No, retail_code, advance_amount, DR_Spec, dr_spec_name, sales_Tax, Tinno, DR_Terr, credit_days, DR_Class, dr_class_name, ad, DR_Address1, DR_Address2, div_code, Milk_pon, UOM, UOM_Name, DDL_Re_Type.SelectedValue, outstandng, creditlmt, Cus_Alter, drcategory, dscategoryName, erbCode);
                if (div_code == "70")
                {
                    iReturn = lstdr.RecordAdd11(DR_Name, curentCompitat, sf_code, Mobile_No, retail_code, advance_amount, DR_Spec, dr_spec_name, sales_Tax, Tinno, DR_Terr, credit_days, DR_Class, dr_class_name, ad, DR_Address1, DR_Address2, div_code, Milk_pon, UOM, UOM_Name, DDL_Re_Type, outstandng, creditlmt, Cus_Alter, drcategory, dscategoryName, erbCode, latitude, longitude, DFDairyMP, MonthlyAI, MCCNFPM, MCCMilkColDaily, FrequencyOfVisit, Breed, curentCom, Email);
                    //iReturn = lstDR.RecordAdd11(DR_Name, curentCompitat, sf_code, Mobile_No, retail_code, advance_amount, DR_Spec, dr_spec_name, sales_Tax, Tinno, DR_Terr, credit_days, DR_Class, dr_class_name, ad, DR_Address1, DR_Address2, div_code, Milk_pon, UOM, UOM_Name, DDL_Re_Type, outstandng, creditlmt, Cus_Alter, drcategory, dscategoryName, erbCode, latitude, longitude, DFDairyMP, MonthlyAI, MCCNFPM, MCCMilkColDaily, FrequencyOfVisit, Breed, curentCom, Email);
                }
                else
                {
                    iReturn = lstdr.RecordAdd(DR_Name, sf_code, Mobile_No, retail_code, advance_amount, DR_Spec, dr_spec_name, sales_Tax, Tinno, DR_Terr, credit_days, DR_Class, dr_class_name, ad, DR_Address1, DR_Address2, div_code, Milk_pon, UOM, UOM_Name, DDL_Re_Type, outstandng, creditlmt, Cus_Alter, drcategory, dscategoryName, erbCode, latitude, longitude, Email);
                    //iReturn = lstDR.RecordAdd(DR_Name, sf_code, Mobile_No, retail_code, advance_amount, DR_Spec, dr_spec_name, sales_Tax, Tinno, DR_Terr, credit_days, DR_Class, dr_class_name, ad, DR_Address1, DR_Address2, div_code, Milk_pon, UOM, UOM_Name, DDL_Re_Type, outstandng, creditlmt, Cus_Alter, drcategory, dscategoryName, erbCode, latitude, longitude, Email);
                }
                if (iReturn > 0)
                {

                    string Squery = "SELECT MAX(ListedDrCode) ListedDrCode FROM Mas_ListedDr  Where Division_Code=" + div_code + "";

                    DB_EReporting db_ER = new DB_EReporting();
                    DataSet ds = db_ER.Exec_DataSet(Squery);

                    string RetailerID = Convert.ToString(ds.Tables[0].Rows[0]["ListedDrCode"]);

                    lisdr ld = new lisdr();

                    if (addfields.Count > 0)
                    {
                        int i = 0; string fld = ""; string val = "";

                        for (int k = 0; k < addfields.Count; k++)
                        {
                            if ((addfields[k].Fields != "'undefined'" || addfields[k].Fields != "undefined") && (addfields[k].Values != "'undefined'" || addfields[k].Values != "undefined"))
                            {
                                fld = addfields[k].Fields;

                                val = addfields[k].Values;

                                string uquery = "EXEC [Insert_CustomRetailerDetails] '" + div_code + "', '" + fld + "', '" + val + "','" + RetailerID + "'";
                                i = db_ER.ExecQry(uquery);
                            }
                        }
                    }

                    if (addfileuds.Count > 0)
                    {
                        int i = 0; string fld = ""; string val = "";

                        for (int k = 0; k < addfileuds.Count; k++)
                        {
                            if ((addfileuds[k].FileId != "'undefined'" || addfileuds[k].FileId != "undefined") && (addfileuds[k].FileName != "'undefined'" || addfileuds[k].FileName != "undefined"))
                            {
                                fld = addfileuds[k].FileId;
                                val = addfileuds[k].FileName;

                                string uquery = "EXEC [Insert_CustomRetailerDetails] '" + div_code + "', '" + fld + "', '" + val + "','" + RetailerID + "'";
                                i = db_ER.ExecQry(uquery);
                            }
                        }
                    }

                    msg = "Created Successfully";
                    //btnClear_Click(sender, e);
                }
                else if (iReturn == -2)
                {
                    //ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert(' Name Already Exist');</script>");
                    msg = "Name Already Exist";
                }
                else if (iReturn == -3)
                {
                    //ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert(' Code Already Exist');</script>");
                    msg = "Code Already Exist";
                }
                else if (iReturn == -4)
                {
                    //ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert(' ERP Code Already Exist');</script>");
                    msg = "ERP Code Already Exist";
                }
            }
            else
            {

                //ListedDR lstDR = new ListedDR();

                lisdr lstDR = new lisdr();

                string subdivcode = Convert.ToString(doctorcode);
                //iReturn = lstDR.Recordupdate_detail(subdivcode, DR_Name, sf_code, Mobile_No, retail_code, advance_amount, DR_Spec, dr_spec_name, sales_Tax, Tinno, DR_Terr, credit_days, DR_Class, dr_class_name, ad, DR_Address1, DR_Address2, div_code, Milk_pon, UOM, UOM_Name, DDL_Re_Type.SelectedValue, outstandng, creditlmt, Cus_Alter, drcategory, dscategoryName, erbCode);
                if (div_code == "70")
                    iReturn = lstDR.Recordupdate_detail1(subdivcode, curentCompitat, DR_Name, sf_code, Mobile_No, retail_code, advance_amount, DR_Spec, dr_spec_name, sales_Tax, Tinno, DR_Terr, credit_days, DR_Class, dr_class_name, ad, DR_Address1, DR_Address2, div_code, Milk_pon, UOM, UOM_Name, DDL_Re_Type, outstandng, creditlmt, Cus_Alter, drcategory, dscategoryName, erbCode, latitude, longitude, DFDairyMP, MonthlyAI, MCCNFPM, MCCMilkColDaily, FrequencyOfVisit, Breed, curentCom, ukeys, Email);
                else
                    iReturn = lstDR.Recordupdate_detailCustom(subdivcode, DR_Name, sf_code, Mobile_No, retail_code, advance_amount, DR_Spec, dr_spec_name, sales_Tax, Tinno, DR_Terr, credit_days, DR_Class, dr_class_name, ad, DR_Address1, DR_Address2, div_code, Milk_pon, UOM, UOM_Name, DDL_Re_Type, outstandng, creditlmt, Cus_Alter, drcategory, dscategoryName, erbCode, latitude, longitude, Email);

                if (iReturn == 1)
                {

                    DB_EReporting db_ER = new DB_EReporting();

                    if (addfields.Count > 0)
                    {
                        int i = 0; string fld = ""; string val = "";

                        for (int k = 0; k < addfields.Count; k++)
                        {
                            if ((addfields[k].Fields != "'undefined'" || addfields[k].Fields != "undefined") && (addfields[k].Values != "'undefined'" || addfields[k].Values != "undefined"))
                            {
                                fld = addfields[k].Fields;
                                val = addfields[k].Values;
                                string uquery = "EXEC [Insert_CustomRetailerDetails] '" + div_code + "', '" + fld + "', '" + val + "','" + doctorcode + "'";
                                i = db_ER.ExecQry(uquery);
                            }
                        }
                    }

                    if (addfileuds.Count > 0)
                    {
                        int i = 0; string fld = ""; string val = "";

                        for (int k = 0; k < addfileuds.Count; k++)
                        {
                            if ((addfileuds[k].FileId != "'undefined'" || addfileuds[k].FileId != "undefined") && (addfileuds[k].FileName != "'undefined'" || addfileuds[k].FileName != "undefined"))
                            {
                                fld = addfileuds[k].FileId;
                                val = addfileuds[k].FileName;

                                string uquery = "EXEC [Insert_CustomRetailerDetails] '" + div_code + "', '" + fld + "', '" + val + "','" + doctorcode + "'";
                                i = db_ER.ExecQry(uquery);
                            }
                        }
                    }

                    msg = "Updated Successfully";
                }
                else if (iReturn == -2)
                {
                    //ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert(' Name Already Exist');</script>");
                    msg = "Name Already Exist";
                }
                else if (iReturn == -3)
                {
                    //ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert(' Code Already Exist');</script>");
                    msg = "Code Already Exist";
                }
                else if (iReturn == -4)
                {
                    //ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert(' ERP Code Already Exist');</script>");
                    msg = "ERP Code Already Exist";
                }
            }
        }
        else
        {

            //ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Invalid Data!!!');</script>");
            if (retail_code == "")
            {
                //ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Enter Retailer Code');</script>");
                msg = "Enter Retailer Code";
                //Txt_id.Focus();
            }
            else if (DR_Name == "")
            {
                //ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Enter Retailer Name');</script>");
                msg = "Enter Retailer Name";
                //txtName.Focus();
            }
            else if (DR_Address1 == "")
            {
                //ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Enter Address...');</script>");
                msg = "Please Enter Address";
                //txtAddress.Focus();
            }
        }
        return msg;
    }

    public static void CopyStream(Stream input, Stream output)
    {
        byte[] buffer = new byte[8 * 1024];
        int len;
        while ((len = input.Read(buffer, 0, buffer.Length)) > 0)
        {
            output.Write(buffer, 0, len);
        }
    }


    [WebMethod(EnableSession = true)]
    public static string SaveFileS3Bucket(string filename)
    {

        lisdr ld = new lisdr();
        string msg = "";
        DataSet dsDivision = ld.getStatePerDivision(div_code);
        string urlshotName = Convert.ToString(dsDivision.Tables[0].Rows[0]["Url_Short_Name"]);
        string directoryPath = urlshotName + "_" + "Retailer";

        //string currentDirectory = HttpContext.Current.Server.MapPath("~");
        //string relativePath = "FMCGWebRetailer";
        string filepath =   HttpContext.Current.Server.MapPath("~/" + directoryPath + "/");

        int bufferSize = 1024;
        byte[] buffer = new byte[bufferSize];
        int bytesRead = 0;

        //Create the Directory.
        if (!Directory.Exists(filepath))
        {
            Directory.CreateDirectory(filepath);
        }

        File.Copy(filename, @"~\" + filepath + "\"" + filename);


        string awsKey = "AKIA5OS74MUCASG7HSCG";
        string awsSecretKey = "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";        
        string bucketName = "happic";
        string prefix = directoryPath + "/" + filename;       
        string localFilePath = System.IO.Path.Combine(filepath);
        string filePath = localFilePath;
        try
        {
            string keyName = filename;
                      

            // Set up your AWS credentials
            BasicAWSCredentials credentials = new BasicAWSCredentials(awsKey, awsSecretKey);

            // Create a new Amazon S3 client
            AmazonS3Client s3Client = new AmazonS3Client(credentials, Amazon.RegionEndpoint.APSouth1);
            // Upload the file to Amazon S3
            TransferUtility fileTransferUtility = new TransferUtility(s3Client);

            fileTransferUtility.UploadAsync(filePath, bucketName + @"/" + directoryPath);
            Console.WriteLine("Upload 1 completed");

            fileTransferUtility.UploadAsync(filePath, bucketName + @"/" + directoryPath, keyName);
            Console.WriteLine("Upload 2 completed");


            using (var fileToUpload =  new FileStream(filePath, FileMode.Open, FileAccess.Read))     
            {
                fileTransferUtility.UploadAsync(fileToUpload, bucketName, keyName);
            }
            Console.WriteLine("Upload 3 completed");


            // Option 4. Specify advanced settings.
            var fileTransferUtilityRequest = new TransferUtilityUploadRequest
            {
                BucketName = bucketName,
                FilePath = filePath,
                StorageClass = S3StorageClass.StandardInfrequentAccess,
                PartSize = 6291456, // 6 MB.
                Key = keyName,
                CannedACL = S3CannedACL.PublicRead
            };
            fileTransferUtilityRequest.Metadata.Add("param1", "Value1");
            fileTransferUtilityRequest.Metadata.Add("param2", "Value2");

            fileTransferUtility.UploadAsync(fileTransferUtilityRequest);
            Console.WriteLine("Upload 4 completed");


            //fileTransferUtility.Upload(bucketName + @"/" + directoryPath, keyName);


            GetObjectRequest request = new GetObjectRequest
            {
                BucketName = bucketName + @"/" + directoryPath,
                Key = filename
            };


            GetObjectResponse response = s3Client.GetObject(request);

            using (Stream responseStream = response.ResponseStream)
            using (FileStream fileStream = File.Create(localFilePath))
            {

                while ((bytesRead = responseStream.Read(buffer, 0, bufferSize)) != 0)
                {
                    fileStream.Write(buffer, 0, bytesRead);
                } // end while

                //responseStream.CopyTo(fileStream);
            }


            //Console.WriteLine("Upload completed!");

            msg = "Upload  completed !!";
        }
        catch (AmazonS3Exception e)
        {
            //Console.WriteLine("Error encountered on server. Message:'{0}' when writing an object", e.Message);
            msg = "Error encountered on server. Message:'{0}' when writing an object  " + e.Message + " ";
        }
        catch (Exception e)
        {
            //Console.WriteLine("Unknown encountered on server. Message:'{0}' when writing an object", e.Message);
            msg = "Unknown encountered on server. Message:'{0}' when writing an object  " + e.Message + " ";
        }

        return msg;
    }

    public class lisdr
    {
        public DataTable GetCustomFieldsDetailsdetails(string div_code)
        {

            DataTable dsAdmin = new DataTable();

            string strQry = " SELECT Field_Col,CF.FieldGroupId,CF.FGTableName,MFG.FGroupName FROM Trans_Custom_Fields_Details CF (NOLOCK) ";
            strQry += " INNER JOIN Mas_FieldGroupTable MFG (NOLOCK) ON CF.FieldGroupId = MFG.FieldGroupId ";
            strQry += " WHERE CF.ModuleId=3 AND Div_code=@Division_Code ";

            try
            {
                using (var con = new SqlConnection(Globals.ConnString))
                {
                    using (var cmd = con.CreateCommand())
                    {
                        cmd.CommandText = strQry;
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@Division_Code", Convert.ToInt32(div_code));
                        SqlDataAdapter dap = new SqlDataAdapter();
                        dap.SelectCommand = cmd;
                        con.Open();
                        dap.Fill(dsAdmin);
                        con.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;

            }
            return dsAdmin;
        }


        string strQry = string.Empty;
        public DataSet ViewListedDr(string drcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            string strQry = " EXEC Get_Retailer_Details '" + drcode + "' ";

            //string strQry = " SELECT d.ListedDrCode,d.Code,isnull(d.ListedDr_Mobile,d.ListedDr_phone) ListedDr_Mobile,d.ListedDr_Name,d.contactperson Contact_Person_Name,d.Doc_Special_Code,d.Doc_Spec_ShortName, " +
            //        " d.Tin_No,d.Sales_Taxno,d.Territory_Code,d.Credit_Days,d.Doc_ClsCode,d.Doc_Class_ShortName,d.Advance_amount,d.Milk_Potential,d.UOM,d.UOM_Name,  " +
            //        " d.ListedDr_Address1,d.ListedDr_Address2,Retailer_Type,stuff((select ', '+territory_Name from Mas_Territory_Creation t where t.Territory_Code = d.Territory_Code " +
            //        " and charindex(cast(t.Territory_Code as varchar)+',',d.Territory_Code+',')>0 for XML path('')),1,2,'') territory_Name ,Outstanding,Credit_Limit,Cus_Alter,d.Doc_Cat_Code,d.ListedDr_Class_Patients,d.ListedDr_Consultation_Fee,ListedDr_Email" +
            //        " FROM  Mas_ListedDr d WHERE d.ListedDrCode =  '" + drcode + "'  and d.ListedDr_Active_Flag = 0 ";


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

        public int RecordAdd11(string DR_Name, string curentCompitat, string sf_code, string Mobile_No, string retail_code, string advance_amount, string DR_Spec, string dr_spec_name, string sales_Tax, string Tinno, string DR_Terr, string credit_days, string DR_Class, string dr_class_name, string ad, string DR_Address1, string DR_Address2, string Div_code, string Milk_pot, string Uom, string Uom_Name, string re_type, string outstanding, string credit_limit, string Cus_Alt, string drcategory, string drcategoryName, string erbCode, string latitude, string longitude, string DFDairyMP, string MonthlyAI, string MCCNFPM, string MCCMilkColDaily, string FrequencyOfVisit, string Breed, string curentCom, string txtmail)
        {
            int iReturn = -1;
            //int jReturn = -1;
            //int Listed_DR_Code;
            if (!sRecordExist(retail_code, DR_Name, Div_code))
            {
                if (!RecordExist(DR_Name, retail_code, Div_code))
                {

                    if (!ERPRecordExist(erbCode, Div_code, retail_code))
                    {
                        try
                        {

                            DB_EReporting db = new DB_EReporting();

                            string Division_Code = "-1";
                            //Listed_DR_Code = -1;

                            //strQry = "SELECT ISNULL(MAX(ListedDrCode),0)+1 FROM Mas_ListedDr";
                            //Listed_DR_Code = db.Exec_Scalar(strQry);

                            strQry = "select sf_code,Dist_Name,Division_Code from  Mas_Territory_Creation where Territory_Code ='" + DR_Terr + "'";
                            DataSet ds = db.Exec_DataSet(strQry);

                            string sfcode = "";
                            string distname = "";
                            foreach (DataRow dd in ds.Tables[0].Rows)
                            {
                                sfcode = dd["sf_code"].ToString();
                                distname = dd["Dist_Name"].ToString();
                                Division_Code = dd["Division_Code"].ToString();
                            }
                            strQry = "select   'EK" + sfcode + "-'+  replace(convert(varchar, getdate(),101),'/','') + replace(convert(varchar, getdate(),108),':','') as ukey ";
                            string UKey = db.Exec_Scalar_s(strQry).ToString();


                            if ((DR_Name == null || DR_Name == ""))
                            { DR_Name = ""; }

                            if ((Milk_pot == null || Milk_pot == ""))
                            { Milk_pot = ""; }

                            if ((credit_days == null || credit_days == ""))
                            { credit_days = ""; }

                            if ((Mobile_No == null || Mobile_No == ""))
                            { Mobile_No = ""; }

                            if ((erbCode == null || erbCode == ""))
                            { erbCode = ""; }

                            if ((advance_amount == null || advance_amount == ""))
                            { advance_amount = ""; }

                            if ((sales_Tax == null || sales_Tax == ""))
                            { sales_Tax = ""; }

                            if ((Tinno == null || Tinno == ""))
                            { Tinno = ""; }

                            if ((Uom == null || Uom == ""))
                            { Uom = ""; }

                            if ((Uom_Name == null || Uom_Name == ""))
                            { Uom_Name = ""; }

                            if ((DR_Class == null || DR_Class == ""))
                            { DR_Class = "0"; }


                            if ((drcategory == null || drcategory == ""))
                            { drcategory = "0"; }


                            SqlParameter[] parameters = new SqlParameter[]
                            {
                                new SqlParameter("@UKey", Convert.ToString(UKey)),
                                new SqlParameter("@DR_Name", Convert.ToString(DR_Name)),
                                new SqlParameter("@DFDairyMP", Convert.ToString(DFDairyMP)),
                                new SqlParameter("@MonthlyAI", Convert.ToString(MonthlyAI)),
                                new SqlParameter("@curentCompitat", Convert.ToString(curentCompitat)),
                                new SqlParameter("@MCCNFPM", Convert.ToString(curentCompitat)),
                                new SqlParameter("@MCCMilkColDaily", Convert.ToString(MCCMilkColDaily)),
                                new SqlParameter("@DR_Spec", Convert.ToString(DR_Spec)),
                                new SqlParameter("@Milk_pot", Convert.ToString(Milk_pot)),
                                new SqlParameter("@curentCom", Convert.ToString(curentCom)),
                                new SqlParameter("@FrequencyOfVisit", Convert.ToString(FrequencyOfVisit)),
                                new SqlParameter("@sfcode", Convert.ToString(sfcode)),
                                new SqlParameter("@Mobile_No", Convert.ToString(Mobile_No)),
                                new SqlParameter("@erbCode", Convert.ToString(erbCode)),
                                new SqlParameter("@advance_amount", Convert.ToString(advance_amount)),
                                new SqlParameter("@dr_spec_name", Convert.ToString(dr_spec_name)),
                                new SqlParameter("@sales_Tax", Convert.ToString(sales_Tax)),
                                new SqlParameter("@Tinno", Convert.ToString(Tinno)),
                                new SqlParameter("@DR_Terr", Convert.ToString(DR_Terr)),
                                new SqlParameter("@credit_days", Convert.ToString(credit_days)),
                                new SqlParameter("@DR_Class", Convert.ToString(DR_Class)),
                                new SqlParameter("@dr_class_name", Convert.ToString(dr_class_name)),
                                new SqlParameter("@ad", Convert.ToString(ad)),
                                new SqlParameter("@DR_Address1", Convert.ToString(DR_Address1)),
                                new SqlParameter("@DR_Address2", Convert.ToString(DR_Address2)),
                                new SqlParameter("@Division_Code", Convert.ToString(Division_Code)),
                                new SqlParameter("@Uom", Convert.ToString(Uom)),
                                new SqlParameter("@Uom_Name", Convert.ToString(Uom_Name)),
                                new SqlParameter("@sf_code", Convert.ToString(sf_code)),
                                new SqlParameter("@distname", Convert.ToString(distname)),
                                new SqlParameter("@re_type", Convert.ToString(re_type)),
                                new SqlParameter("@outstanding", Convert.ToString(outstanding)),
                                new SqlParameter("@credit_limit", Convert.ToString(credit_limit)),
                                new SqlParameter("@Cus_Alt", Convert.ToString(Cus_Alt)),
                                new SqlParameter("@drcategory", Convert.ToString(drcategory)),
                                new SqlParameter("@drcategoryName", Convert.ToString(drcategoryName)),
                                new SqlParameter("@latitude", Convert.ToString(latitude)),
                                new SqlParameter("@longitude", Convert.ToString(longitude)),
                                new SqlParameter("@Breed", Convert.ToString(Breed)),
                                new SqlParameter("@txtmail", Convert.ToString(txtmail))
                            };

                            iReturn = db.Exec_QueryWithParamNew("Insert_RetailerDetails", CommandType.StoredProcedure, parameters);

                            //strQry = " insert into NewContact_Dr(Ukey, FormarName, DFDairyMP, MonthlyAI, AITCU,MCCNFPM, MCCMilkColDaily, CreatedDate," +
                            //    " ListedDrCode,CustomerCategory,PotentialFSD,CurrentlyUFSD,FrequencyOfVisit)" +
                            //    "VALUES('" + UKey + "','" + DR_Name + "','" + DFDairyMP + "','" + MonthlyAI + "','" + curentCompitat + "','" + MCCNFPM + "'," +
                            //    "'" + MCCMilkColDaily + "',getdate(),'" + Listed_DR_Code + "','" + DR_Spec + "'," +
                            //    "'" + Milk_pot + "','" + curentCom + "','" + FrequencyOfVisit + "')";

                            //jReturn = db.ExecQry(strQry);

                            //strQry = " insert into Mas_ListedDr (ListedDrCode,SF_Code,ListedDr_Name,ListedDr_Mobile,Code,contactperson,Doc_Special_Code,Doc_Spec_ShortName, " +
                            //           " Tin_No,sales_Taxno,Territory_Code,Credit_Days,Doc_ClsCode,Doc_Class_ShortName,Advance_amount,ListedDr_Address1,ListedDr_Address2,Division_Code, ListedDr_Active_Flag, ListedDr_Created_Date, " +
                            //           " LastUpdt_Date,Milk_Potential,UOM,UOM_Name,TERRCODE,DIST_NAME,Retailer_Type,outstanding,Credit_Limit,Cus_Alter,Doc_Cat_Code,Doc_Cat_ShortName,ListedDr_Class_Patients,ListedDr_Consultation_Fee, " +
                            //            "Breed,Ukey,NoOfAnimal,Entry_Mode,ListedDr_Email) " +
                            //           " VALUES('" + Listed_DR_Code + "', '" + sfcode + "', '" + DR_Name + "', '" + Mobile_No + "', '" + erbCode + "', '" + advance_amount + "', " +
                            //           " '" + DR_Spec + "','" + dr_spec_name + "','" + sales_Tax + "','" + Tinno + "','" + DR_Terr + "','" + credit_days + "','" + DR_Class + "','" + dr_class_name + "','" + ad + "','" + DR_Address1 + "'," +
                            //           "'" + DR_Address2 + "', '" + Division_Code + "',0,getdate(),getdate(),'" + Milk_pot + "','" + Uom + "','" + Uom_Name + "','" + sf_code + "','" + distname + "','" + re_type + "','" + outstanding + "'," +
                            //           "'" + credit_limit + "','" + Cus_Alt + "','" + drcategory + "','" + drcategoryName + "','" + latitude + "','" + longitude + "'," +
                            //           " '" + Breed + "','" + UKey + "','" + credit_days + "','Web','" + txtmail + "')";                          

                            //iReturn = db.ExecQry(strQry);


                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                    }
                    else
                    {
                        iReturn = -4;
                    }

                }
                else
                {
                    iReturn = -2;
                }

            }
            else
            {
                iReturn = -3;
            }

            return iReturn;
        }

        public int RecordAdd(string DR_Name, string sf_code, string Mobile_No, string retail_code, string advance_amount, string DR_Spec, string dr_spec_name, string sales_Tax, string Tinno, string DR_Terr, string credit_days, string DR_Class, string dr_class_name, string ad, string DR_Address1, string DR_Address2, string Div_code, string Milk_pot, string Uom, string Uom_Name, string re_type, string outstanding, string credit_limit, string Cus_Alt, string drcategory, string drcategoryName, string erbCode, string latitude, string longitude, string txtmail)
        {
            int iReturn = -1;
            //int Listed_DR_Code = -1;
            if (!sRecordExist(retail_code, DR_Name, Div_code))
            {
                if (!RecordExist(DR_Name, retail_code, Div_code))
                {

                    if (!ERPRecordExist(erbCode, Div_code, retail_code))
                    {
                        try
                        {

                            DB_EReporting db = new DB_EReporting();

                            string Division_Code = "-1";


                            //Listed_DR_Code = -1;

                            //strQry = "SELECT ISNULL(MAX(ListedDrCode),0)+1 FROM Mas_ListedDr";
                            //Listed_DR_Code = db.Exec_Scalar(strQry);

                            strQry = "select sf_code,Dist_Name,Division_Code from  Mas_Territory_Creation where Territory_Code ='" + DR_Terr + "'";
                            DataSet ds = db.Exec_DataSet(strQry);

                            string sfcode = "";
                            string distname = "";
                            foreach (DataRow dd in ds.Tables[0].Rows)
                            {
                                sfcode = dd["sf_code"].ToString();
                                distname = dd["Dist_Name"].ToString();
                                Division_Code = dd["Division_Code"].ToString();
                            }

                            if ((DR_Name == null || DR_Name == ""))
                            { DR_Name = ""; }

                            if ((Milk_pot == null || Milk_pot == ""))
                            { Milk_pot = ""; }

                            if ((credit_days == null || credit_days == ""))
                            { credit_days = ""; }

                            if ((Mobile_No == null || Mobile_No == ""))
                            { Mobile_No = ""; }

                            if ((erbCode == null || erbCode == ""))
                            { erbCode = ""; }

                            if ((advance_amount == null || advance_amount == ""))
                            { advance_amount = ""; }

                            if ((sales_Tax == null || sales_Tax == ""))
                            { sales_Tax = ""; }

                            if ((Tinno == null || Tinno == ""))
                            { Tinno = ""; }

                            if ((Uom == null || Uom == ""))
                            { Uom = ""; }

                            if ((Uom_Name == null || Uom_Name == ""))
                            { Uom_Name = ""; }

                            if ((DR_Class == null || DR_Class == ""))
                            { DR_Class = "0"; }


                            if ((drcategory == null || drcategory == ""))
                            { drcategory = "0"; }


                            SqlParameter[] parameters = new SqlParameter[]
                            {
                                new SqlParameter("@UKey", ""),
                                new SqlParameter("@DR_Name", Convert.ToString(DR_Name)),
                                new SqlParameter("@DFDairyMP", ""),
                                new SqlParameter("@MonthlyAI", ""),
                                new SqlParameter("@curentCompitat", ""),
                                new SqlParameter("@MCCNFPM", ""),
                                new SqlParameter("@MCCMilkColDaily", ""),
                                new SqlParameter("@DR_Spec",  Convert.ToString(DR_Spec)),
                                new SqlParameter("@Milk_pot", Convert.ToString(Milk_pot)),
                                new SqlParameter("@curentCom", ""),
                                new SqlParameter("@FrequencyOfVisit", ""),
                                new SqlParameter("@sfcode", Convert.ToString(sfcode)),
                                new SqlParameter("@Mobile_No", Convert.ToString(Mobile_No)),
                                new SqlParameter("@erbCode", Convert.ToString(erbCode)),
                                new SqlParameter("@advance_amount", Convert.ToString(advance_amount)),
                                new SqlParameter("@dr_spec_name", Convert.ToString(dr_spec_name)),
                                new SqlParameter("@sales_Tax", Convert.ToString(sales_Tax)),
                                new SqlParameter("@Tinno", Convert.ToString(Tinno)),
                                new SqlParameter("@DR_Terr", Convert.ToString(DR_Terr)),
                                new SqlParameter("@credit_days", Convert.ToString(credit_days)),
                                new SqlParameter("@DR_Class", Convert.ToString(DR_Class)),
                                new SqlParameter("@dr_class_name", Convert.ToString(dr_class_name)),
                                new SqlParameter("@ad", Convert.ToString(ad)),
                                new SqlParameter("@DR_Address1", Convert.ToString(DR_Address1)),
                                new SqlParameter("@DR_Address2", Convert.ToString(DR_Address2)),
                                new SqlParameter("@Division_Code", Convert.ToString(Division_Code)),
                                new SqlParameter("@Uom", Convert.ToString(Uom)),
                                new SqlParameter("@Uom_Name", Convert.ToString(Uom_Name)),
                                new SqlParameter("@sf_code", Convert.ToString(sf_code)),
                                new SqlParameter("@distname", Convert.ToString(distname)),
                                new SqlParameter("@re_type", Convert.ToString(re_type)),
                                new SqlParameter("@outstanding", Convert.ToString(outstanding)),
                                new SqlParameter("@credit_limit", Convert.ToString(credit_limit)),
                                new SqlParameter("@Cus_Alt", Convert.ToString(Cus_Alt)),
                                new SqlParameter("@drcategory", Convert.ToString(drcategory)),
                                new SqlParameter("@drcategoryName", Convert.ToString(drcategoryName)),
                                new SqlParameter("@latitude", Convert.ToString(latitude)),
                                new SqlParameter("@longitude", Convert.ToString(longitude)),
                                new SqlParameter("@Breed", ""),
                                new SqlParameter("@txtmail", Convert.ToString(txtmail))
                            };

                            iReturn = db.Exec_QueryWithParamNew("Insert_RetailerDetails", CommandType.StoredProcedure, parameters);

                            //strQry = " insert into Mas_ListedDr (ListedDrCode,SF_Code,ListedDr_Name,ListedDr_Mobile,Code,contactperson,Doc_Special_Code,Doc_Spec_ShortName, " +
                            //           " Tin_No,sales_Taxno,Territory_Code,Credit_Days,Doc_ClsCode,Doc_Class_ShortName,Advance_amount,ListedDr_Address1,ListedDr_Address2,Division_Code, ListedDr_Active_Flag, ListedDr_Created_Date, " +
                            //           " LastUpdt_Date,Milk_Potential,UOM,UOM_Name,TERRCODE,DIST_NAME,Retailer_Type,outstanding,Credit_Limit,Cus_Alter,Doc_Cat_Code,Doc_Cat_ShortName,ListedDr_Class_Patients,ListedDr_Consultation_Fee,Entry_Mode,ListedDr_Email) " +
                            //           " VALUES('" + Listed_DR_Code + "', '" + sfcode + "', '" + DR_Name + "', '" + Mobile_No + "', '" + erbCode + "', '" + advance_amount + "', " +
                            //           " '" + DR_Spec + "','" + dr_spec_name + "','" + sales_Tax + "','" + Tinno + "','" + DR_Terr + "','" + credit_days + "','" + DR_Class + "','" + dr_class_name + "','" + ad + "','" + DR_Address1 + "'," +
                            //           "'" + DR_Address2 + "', '" + Division_Code + "',0,getdate(),getdate(),'" + Milk_pot + "','" + Uom + "','" + Uom_Name + "','" + sf_code + "','" + distname + "','" + re_type + "','" + outstanding + "'," +
                            //           "'" + credit_limit + "','" + Cus_Alt + "','" + drcategory + "','" + drcategoryName + "','" + latitude + "','" + longitude + "','Web','" + txtmail + "')";

                            //iReturn = db.ExecQry(strQry);
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                    }
                    else
                    {
                        iReturn = -4;
                    }

                }
                else
                {
                    iReturn = -2;
                }

            }
            else
            {
                iReturn = -3;
            }

            return iReturn;
        }

        public bool sRecordExist(string retail_code, string DR_Name, string Div_code)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(code) FROM Mas_ListedDr WHERE ListedDr_Name='" + DR_Name + "' and Code='" + retail_code + "' and Division_Code='" + Div_code + "' AND  (ListedDr_Active_Flag=0 or ListedDr_Active_Flag=2) ";
                int iRecordExist = db.Exec_Scalar(strQry);

                if (iRecordExist > 0)
                    bRecordExist = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return bRecordExist;
        }

        public bool RecordExist(string Listed_DR_Name, string retail_code, string Div_Code)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(ListedDr_Name) FROM Mas_ListedDr WHERE Code='" + retail_code + "' AND ListedDr_Name='" + Listed_DR_Name + "' and Division_Code='" + Div_Code + "' AND  (ListedDr_Active_Flag=0 or ListedDr_Active_Flag=2) ";
                int iRecordExist = db.Exec_Scalar(strQry);

                if (iRecordExist > 0)
                    bRecordExist = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return bRecordExist;
        }

        public bool ERPRecordExist(string retail_code, string Div_code, string rtCode)
        {

            bool bRecordExist = false;
            try
            {
                if (retail_code != string.Empty)
                {
                    DB_EReporting db = new DB_EReporting();

                    strQry = "SELECT ListedDrCode FROM Mas_ListedDr WHERE  Code='" + retail_code + "' and Division_Code='" + Div_code + "' AND  (ListedDr_Active_Flag=0 or ListedDr_Active_Flag=2) ";

                    DataSet ds = db.Exec_DataSet(strQry);

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0][0].ToString() == rtCode)
                        {

                        }
                        else
                        {
                            bRecordExist = true;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return bRecordExist;
        }

        public DataSet get_RetailerCustomField(string listeddrcode, string columnName, string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTerr = null;

            strQry = "EXEC [Get_Retailer_CustomFieldDetails] '" + listeddrcode + "','" + columnName + "','" + divcode + "'";

            //strQry = " SELECT *FROM Trans_Retailer_Custom_Field Where RetailerID = " + listeddrcode + "";

            try
            {
                dsTerr = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsTerr;
        }

        public DataSet GetCustomFormsFieldsData(string divcode, string ModeleId)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "EXEC [Get_CustomForms_Fields] '" + divcode + "' ,'" + ModeleId + "' ";

            try
            {
                dsAdmin = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsAdmin;
        }

        public int Recordupdate_detail1(string Dr_Code, string curentCompitat, string DR_Name, string sf_code, string Mobile_No, string retail_code, string advance_amount, string DR_Spec, string dr_spec_name, string sales_Tax, string Tinno, string DR_Terr, string credit_days, string DR_Class, string dr_class_name, string ad, string DR_Address1, string DR_Address2, string div_code, string Milk_Potential, string UOM, string UOM_Name, string Retailer_Type, string outstanding, string credit_limit, string Cus_alt, string catgoryCode, string catgoryName, string erbCode, string latitude, string longitude, string DFDairyMP, string MonthlyAI, string MCCNFPM, string MCCMilkColDaily, string FrequencyOfVisit, string Breed, string curentCom, string ukey, string txtmail)
        {
            int iReturn = -1;
            DB_EReporting db = new DB_EReporting();
            if (!sRecordExist1(erbCode, Dr_Code, div_code))
            {
                if (!ERPRecordExist(erbCode, div_code, Dr_Code))
                {

                    strQry = "select sf_code,Dist_Name,Division_Code,Territory_SName from  Mas_Territory_Creation where Territory_Code ='" + DR_Terr + "'";
                    DataSet ds = db.Exec_DataSet(strQry);

                    string Division_Code = "-1";
                    string sfcode = "";
                    string distname = "";
                    string terr_code = "";

                    foreach (DataRow dd in ds.Tables[0].Rows)
                    {
                        sfcode = dd["sf_code"].ToString();
                        distname = dd["Dist_Name"].ToString();
                        Division_Code = dd["Division_Code"].ToString();
                        terr_code = dd["Territory_SName"].ToString();
                    }

                    //strQry = " update NewContact_Dr" +
                    //          " set FormarName='" + DR_Name + "',DFDairyMP='" + DFDairyMP + "', MonthlyAI='" + MonthlyAI + "', AITCU='" + curentCompitat + "', " +
                    //          "MCCNFPM ='" + MCCNFPM + "', MCCMilkColDaily ='" + MCCMilkColDaily + "', CreatedDate=getdate()," +
                    //  "ListedDrCode='" + Dr_Code + "',CustomerCategory='" + DR_Spec + "',PotentialFSD='" + Milk_Potential + "'," +
                    //  "CurrentlyUFSD='" + curentCom + "',FrequencyOfVisit='" + FrequencyOfVisit + "'" +
                    //  " where ListedDrCode='" + Dr_Code + "'  and Ukey='" + ukey + "'";

                    //jReturn = db.ExecQry(strQry);

                    //strQry = " update Mas_ListedDr" +
                    //           " set ListedDr_Name='" + DR_Name + "',Territory_Code='" + DR_Terr + "', Doc_ClsCode='" + DR_Class + "', Code='" + erbCode + "', terrcode ='" + terr_code + "', dist_name ='" + distname + "'" +
                    //           ", Doc_Special_Code='" + DR_Spec + "',ListedDr_Mobile='" + Mobile_No + "',contactperson='" + advance_amount + "',Doc_Spec_ShortName='" + dr_spec_name + "',Tin_No='" + sales_Tax + "'" +
                    //           ",sales_Taxno='" + Tinno + "',Credit_Days='" + credit_days + "',Doc_Class_ShortName='" + dr_class_name + "',Advance_amount='" + ad + "',ListedDr_Address1='" + DR_Address1 + "'" +
                    //           ",ListedDr_Address2='" + DR_Address2 + "',LastUpdt_Date= getdate(),Milk_Potential='" + Milk_Potential + "',UOM='" + UOM + "',UOM_Name='" + UOM_Name + "'" +
                    //           ",Retailer_Type='" + Retailer_Type + "' ,outstanding='" + outstanding + "' ,credit_limit='" + credit_limit + "',Cus_Alter='" + Cus_alt + "',Doc_Cat_Code='" + catgoryCode + "'" +
                    //           ",Doc_Cat_ShortName='" + catgoryName + "',ListedDr_Class_Patients='" + latitude + "',ListedDr_Consultation_Fee='" + longitude + "', " +
                    //           "NoOfAnimal='" + credit_days + "', Breed='" + Breed + "',ListedDr_Email='" + txtmail + "'" +
                    //           " where ListedDrCode='" + Dr_Code + "' and Division_Code='" + div_code + "' and Ukey='" + ukey + "'";

                    //iReturn = db.ExecQry(strQry);

                    if ((DR_Name == null || DR_Name == ""))
                    { DR_Name = ""; }

                    if ((Milk_Potential == null || Milk_Potential == ""))
                    { Milk_Potential = ""; }

                    if ((credit_days == null || credit_days == ""))
                    { credit_days = ""; }

                    if ((Mobile_No == null || Mobile_No == ""))
                    { Mobile_No = ""; }

                    if ((erbCode == null || erbCode == ""))
                    { erbCode = ""; }

                    if ((advance_amount == null || advance_amount == ""))
                    { advance_amount = ""; }

                    if ((sales_Tax == null || sales_Tax == ""))
                    { sales_Tax = ""; }

                    if ((Tinno == null || Tinno == ""))
                    { Tinno = ""; }

                    if ((UOM == null || UOM == ""))
                    { UOM = ""; }

                    if ((UOM_Name == null || UOM_Name == ""))
                    { UOM_Name = ""; }

                    if ((DR_Class == null || DR_Class == ""))
                    { DR_Class = "0"; }


                    if ((catgoryCode == null || catgoryCode == ""))
                    { catgoryCode = "0"; }


                    SqlParameter[] parameters = new SqlParameter[]
                    {
                        new SqlParameter("@UKey", Convert.ToString(ukey)),
                        new SqlParameter("@DR_Name", Convert.ToString(DR_Name)),
                        new SqlParameter("@DFDairyMP", Convert.ToString(DFDairyMP)),
                        new SqlParameter("@MonthlyAI", Convert.ToString(MonthlyAI)),
                        new SqlParameter("@curentCompitat", Convert.ToString(curentCompitat)),
                        new SqlParameter("@MCCNFPM", Convert.ToString(MCCNFPM)),
                        new SqlParameter("@MCCMilkColDaily", Convert.ToString(MCCMilkColDaily)),
                        new SqlParameter("@Dr_Code", Convert.ToString(Dr_Code)),
                        new SqlParameter("@DR_Spec", Convert.ToString(DR_Spec)),
                        new SqlParameter("@Milk_Potential", Convert.ToString(Milk_Potential)),
                        new SqlParameter("@curentCom", Convert.ToString(curentCom)),
                        new SqlParameter("@FrequencyOfVisit", Convert.ToString(FrequencyOfVisit)),
                        new SqlParameter("@DR_Terr", Convert.ToString(DR_Terr)),
                        new SqlParameter("@DR_Class", Convert.ToString(DR_Class)),
                        new SqlParameter("@erbCode", Convert.ToString(erbCode)),
                        new SqlParameter("@terr_code", Convert.ToString(terr_code)),
                        new SqlParameter("@distname", Convert.ToString(distname)),
                        new SqlParameter("@Mobile_No", Convert.ToString(Mobile_No)),
                        new SqlParameter("@advance_amount", Convert.ToString(advance_amount)),
                        new SqlParameter("@dr_spec_name", Convert.ToString(dr_spec_name)),
                        new SqlParameter("@sales_Tax", Convert.ToString(sales_Tax)),
                        new SqlParameter("@Tinno", Convert.ToString(Tinno)),
                        new SqlParameter("@credit_days", Convert.ToString(credit_days)),
                        new SqlParameter("@dr_class_name", Convert.ToString(dr_class_name)),
                        new SqlParameter("@ad", Convert.ToString(ad)),
                        new SqlParameter("@DR_Address1", Convert.ToString(DR_Address1)),
                        new SqlParameter("@DR_Address2", Convert.ToString(DR_Address2)),
                        new SqlParameter("@Uom", Convert.ToString(UOM)),
                        new SqlParameter("@Uom_Name", Convert.ToString(UOM_Name)),
                        new SqlParameter("@Retailer_Type", Convert.ToString(Retailer_Type)),
                        new SqlParameter("@outstanding", Convert.ToString(outstanding)),
                        new SqlParameter("@credit_limit", Convert.ToString(credit_limit)),
                        new SqlParameter("@Cus_Alt", Convert.ToString(Cus_alt)),
                        new SqlParameter("@catgoryCode", Convert.ToString(catgoryCode)),
                        new SqlParameter("@catgoryName", Convert.ToString(catgoryName)),
                        new SqlParameter("@latitude", Convert.ToString(latitude)),
                        new SqlParameter("@longitude", Convert.ToString(longitude)),
                        new SqlParameter("@div_code", Convert.ToString(Division_Code)),
                        new SqlParameter("@Breed", Convert.ToString(Breed)),
                        new SqlParameter("@txtmail", Convert.ToString(txtmail))
                    };

                    iReturn = db.Exec_QueryWithParamNew("Update_RetailerDetails", CommandType.StoredProcedure, parameters);


                }
                else
                {
                    iReturn = -4;
                }
            }
            else
            {
                iReturn = -3;
            }
            return iReturn;
        }

        public int Recordupdate_detailCustom(string Dr_Code, string DR_Name, string sf_code, string Mobile_No, string retail_code, string advance_amount, string DR_Spec, string dr_spec_name, string sales_Tax, string Tinno, string DR_Terr, string credit_days, string DR_Class, string dr_class_name, string ad, string DR_Address1, string DR_Address2, string div_code, string Milk_Potential, string UOM, string UOM_Name, string Retailer_Type, string outstanding, string credit_limit, string Cus_alt, string catgoryCode, string catgoryName, string erbCode, string latitude, string longitude, string txtmail)
        {
            int iReturn = -1;
            DB_EReporting db = new DB_EReporting();

            strQry = "SELECT ListedDrCode FROM Mas_ListedDr WHERE  ListedDrCode  =" + Dr_Code + " AND Division_Code='" + div_code + "' AND  (ListedDr_Active_Flag=0 or ListedDr_Active_Flag=2) ";

            DataSet ds = db.Exec_DataSet(strQry);

            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    strQry = "select sf_code,Dist_Name,Division_Code,Territory_SName from  Mas_Territory_Creation where Territory_Code ='" + DR_Terr + "'";
                    DataSet ds1 = db.Exec_DataSet(strQry);

                    string Division_Code = "-1";
                    string sfcode = "";
                    string distname = "";
                    string terr_code = "";

                    foreach (DataRow dd in ds1.Tables[0].Rows)
                    {
                        sfcode = dd["sf_code"].ToString();
                        distname = dd["Dist_Name"].ToString();
                        Division_Code = dd["Division_Code"].ToString();
                        terr_code = dd["Territory_SName"].ToString();
                    }

                    //strQry = " update Mas_ListedDr " +
                    //       " set ListedDr_Name='" + DR_Name + "',Territory_Code='" + DR_Terr + "', Doc_ClsCode='" + DR_Class + "', Code='" + erbCode + "', terrcode ='" + terr_code + "', dist_name ='" + distname + "'" +
                    //       ", Doc_Special_Code='" + DR_Spec + "',ListedDr_Mobile='" + Mobile_No + "',contactperson='" + advance_amount + "',Doc_Spec_ShortName='" + dr_spec_name + "',Tin_No='" + sales_Tax + "'" +
                    //       ",sales_Taxno='" + Tinno + "',Credit_Days='" + credit_days + "',Doc_Class_ShortName='" + dr_class_name + "',Advance_amount='" + ad + "',ListedDr_Address1='" + DR_Address1 + "'" +
                    //       ",ListedDr_Address2='" + DR_Address2 + "',ListedDr_Created_Date=getdate(),Milk_Potential='" + Milk_Potential + "',UOM='" + UOM + "',UOM_Name='" + UOM_Name + "'" +
                    //       ",Retailer_Type='" + Retailer_Type + "' ,outstanding='" + outstanding + "' ,credit_limit='" + credit_limit + "',Cus_Alter='" + Cus_alt + "',Doc_Cat_Code='" + catgoryCode + "'" +
                    //       ",Doc_Cat_ShortName='" + catgoryName + "',ListedDr_Class_Patients='" + latitude + "',ListedDr_Consultation_Fee='" + longitude + "',ListedDr_Email='" + txtmail + "'" +
                    //       " where ListedDrCode='" + Dr_Code + "' and Division_Code='" + div_code + "' ";

                    if ((DR_Name == null || DR_Name == ""))
                    { DR_Name = ""; }

                    if ((Milk_Potential == null || Milk_Potential == ""))
                    { Milk_Potential = ""; }

                    if ((credit_days == null || credit_days == ""))
                    { credit_days = ""; }

                    if ((Mobile_No == null || Mobile_No == ""))
                    { Mobile_No = ""; }

                    if ((erbCode == null || erbCode == ""))
                    { erbCode = ""; }

                    if ((advance_amount == null || advance_amount == ""))
                    { advance_amount = ""; }

                    if ((sales_Tax == null || sales_Tax == ""))
                    { sales_Tax = ""; }

                    if ((Tinno == null || Tinno == ""))
                    { Tinno = ""; }

                    if ((UOM == null || UOM == ""))
                    { UOM = ""; }

                    if ((UOM_Name == null || UOM_Name == ""))
                    { UOM_Name = ""; }

                    if ((DR_Class == null || DR_Class == ""))
                    { DR_Class = "0"; }


                    if ((catgoryCode == null || catgoryCode == ""))
                    { catgoryCode = "0"; }

                    if ((credit_limit == null || credit_limit == ""))
                    { credit_limit = "0"; }


                    SqlParameter[] parameters = new SqlParameter[]
                    {
                        new SqlParameter("@UKey", ""),
                        new SqlParameter("@DR_Name", Convert.ToString(DR_Name)),
                        new SqlParameter("@DFDairyMP", ""),
                        new SqlParameter("@MonthlyAI", ""),
                        new SqlParameter("@curentCompitat", ""),
                        new SqlParameter("@MCCNFPM", ""),
                        new SqlParameter("@MCCMilkColDaily", ""),
                        new SqlParameter("@Dr_Code", Convert.ToString(Dr_Code)),
                        new SqlParameter("@DR_Spec", Convert.ToString(DR_Spec)),
                        new SqlParameter("@Milk_Potential", Convert.ToString(Milk_Potential)),
                        new SqlParameter("@curentCom", ""),
                        new SqlParameter("@FrequencyOfVisit", ""),
                        new SqlParameter("@DR_Terr", Convert.ToString(DR_Terr)),
                        new SqlParameter("@DR_Class", Convert.ToString(DR_Class)),
                        new SqlParameter("@erbCode", Convert.ToString(erbCode)),
                        new SqlParameter("@terr_code", Convert.ToString(terr_code)),
                        new SqlParameter("@distname", Convert.ToString(distname)),
                        new SqlParameter("@Mobile_No", Convert.ToString(Mobile_No)),
                        new SqlParameter("@advance_amount", Convert.ToString(advance_amount)),
                        new SqlParameter("@dr_spec_name", Convert.ToString(dr_spec_name)),
                        new SqlParameter("@sales_Tax", Convert.ToString(sales_Tax)),
                        new SqlParameter("@Tinno", Convert.ToString(Tinno)),
                        new SqlParameter("@credit_days", Convert.ToString(credit_days)),
                        new SqlParameter("@dr_class_name", Convert.ToString(dr_class_name)),
                        new SqlParameter("@ad", Convert.ToString(ad)),
                        new SqlParameter("@DR_Address1", Convert.ToString(DR_Address1)),
                        new SqlParameter("@DR_Address2", Convert.ToString(DR_Address2)),
                        new SqlParameter("@Uom", Convert.ToString(UOM)),
                        new SqlParameter("@Uom_Name", Convert.ToString(UOM_Name)),
                        new SqlParameter("@Retailer_Type", Convert.ToString(Retailer_Type)),
                        new SqlParameter("@outstanding", Convert.ToString(outstanding)),
                        new SqlParameter("@credit_limit", Convert.ToString(credit_limit)),
                        new SqlParameter("@Cus_Alt", Convert.ToString(Cus_alt)),
                        new SqlParameter("@catgoryCode", Convert.ToString(catgoryCode)),
                        new SqlParameter("@catgoryName", Convert.ToString(catgoryName)),
                        new SqlParameter("@latitude", Convert.ToString(latitude)),
                        new SqlParameter("@longitude", Convert.ToString(longitude)),
                        new SqlParameter("@div_code", Convert.ToString(Division_Code)),
                        new SqlParameter("@Breed", ""),
                        new SqlParameter("@txtmail", Convert.ToString(txtmail))
                   };

                    iReturn = db.Exec_QueryWithParamNew("Update_RetailerDetails", CommandType.StoredProcedure, parameters);


                    //iReturn = db.ExecQry(strQry);
                }
                else
                { iReturn = -4; }
            }
            else
            { iReturn = -3; }

            return iReturn;
        }

        public bool sRecordExist1(string retail_code, string DR_Code, string Div_code)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "  SELECT COUNT(code) FROM Mas_ListedDr WHERE Code='" + retail_code + "' and  ListedDrCode ! =" + DR_Code + " and Division_Code !='" + Div_code + "' and (ListedDr_Active_Flag=0 or ListedDr_Active_Flag=2) ";
                int iRecordExist = db.Exec_Scalar(strQry);

                if (iRecordExist > 0)
                    bRecordExist = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return bRecordExist;
        }

        public DataSet GetCustomFormsFieldsGroupData(string divcode, string ModeleId)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "EXEC [Get_CustomForms_FieldsGroups] '" + divcode + "' ,'" + ModeleId + "' ";

            try
            {
                dsAdmin = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsAdmin;
        }

        public DataSet getStatePerDivision(string div_code)
        {
            DataSet dsAdmin = new DataSet();

            string strQry = "SELECT State_Code,Division_Name,Division_SName,Url_Short_Name  FROM Mas_Division ";
            strQry += " Where Division_Code = @Division_Code  GROUP BY State_Code,Division_Name,Division_SName,Url_Short_Name ";

            try
            {
                using (var con = new SqlConnection(Global.ConnString))
                {
                    using (var cmd = con.CreateCommand())
                    {
                        cmd.CommandText = strQry;
                        cmd.Parameters.AddWithValue("@Division_Code", Convert.ToInt32(div_code));
                        cmd.CommandType = CommandType.Text;
                        SqlDataAdapter dap = new SqlDataAdapter();
                        dap.SelectCommand = cmd;
                        con.Open();
                        dap.Fill(dsAdmin);
                        con.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;

            }
            return dsAdmin;
        }

    }
}