using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using Bus_EReport;

public partial class MasterFiles_Stockist_Sale : System.Web.UI.Page
{
    #region "Declaration"
    DataSet dsStockist = null;
    DataSet dsSalesForce = null;
    DataSet dsDivision = null;
    DataSet dsReport = null;
    DataSet dsTerritory = null;
    string stockist_code = string.Empty;
    string divcode = string.Empty;
    string SF_Name = string.Empty;
    string stockist_name = string.Empty;
    string[] stockistname;
    string sStockist = string.Empty;
    string stockist_Address = string.Empty;
    string stockist_ContactPerson = string.Empty;
    string stockist_Designation = string.Empty;
    string stockist_mobilno = string.Empty;
    string sf_code = string.Empty;
    string sf_type = string.Empty;
    string sChkSalesforce = string.Empty;
    string ReportingMGR = string.Empty;
    string div_code = string.Empty;
    int iIndex = -1;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int time;
    #endregion


    protected void Page_Load(object sender, EventArgs e)
    {
        Session["backurl"] = "StockistList.aspx";
        divcode = Convert.ToString(Session["div_code"]);
        stockist_code = Request.QueryString["Stockist_Code"];
        //txtTerritory.Visible = false;
        //lblTerritory.Visible = false;
        if (!Page.IsPostBack)
        {
            FillStockist_Name();
            FillStockist_StockistName();
            FillReporting();
            menu1.FindControl("btnBack").Visible = false;
            menu1.Title = this.Page.Title;
            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);
            GetWorkName();


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
    private void FillCheckBoxList()//changes done by resh in query
    {
        Stockist sk = new Stockist();
        dsStockist = sk.getSalesforce(divcode);

        if (dsStockist.Tables[0].Rows.Count > 0)
            ViewState["dsStockist"] = dsStockist;
        for (int i = 0; i <= dsStockist.Tables[0].Rows.Count - 1; i++)
        {
            if (dsStockist.Tables[0].Rows.Count > 0)
            {

                string[] Salesforce;
                if (sf_code != "")
                {
                    iIndex = -1;
                    Salesforce = sf_code.Split(',');
                    foreach (string sf in Salesforce)
                    {
                        foreach (DataListItem cb in DataList1.Items)
                        {

                            CheckBox chk = (CheckBox)cb.FindControl("chkCategoryNameLabel");
                            HiddenField hf = (HiddenField)cb.FindControl("cbTestID");

                            if (sf == hf.Value)
                            {
                                //sChkSalesforce = sChkSalesforce + hf.Value + ",";
                                chk.Checked = true;
                                chk.Attributes.Add("style", "color:#ff0000;font-weight:bold;font-size:14px;");

                            }

                        }
                    }
                }
            }
        }


        //chkboxSalesforce.DataTextField = "SF_Name";
        //chkboxSalesforce.DataValueField = "sf_code";
        //chkboxSalesforce.DataSource = dsStockist;
        //chkboxSalesforce.DataBind();  
        //string[] Salesforce;
        //    if (sf_code != "")
        //    {
        //        iIndex = -1;
        //        Salesforce = sf_code.Split(',');
        //        foreach (string sf in Salesforce)
        //        {
        //            for (iIndex = 0; iIndex < chkboxSalesforce.Items.Count; iIndex++)
        //            {
        //                if (sf == chkboxSalesforce.Items[iIndex].Value)//chan by resh in color see below
        //                {
        //                    chkboxSalesforce.Items[iIndex].Selected = true;
        //                    chkboxSalesforce.Items[iIndex].Attributes.Add("style", "Color: #FF6347;font-weight:Bold"); //which MR is to be ticked has been highlight the Color
        //                }
        //            }
        //        }
        //    }
    }


    private void FillReporting()
    {
        Stockist sk = new Stockist();
        dsStockist = sk.getStockist_Reporting(divcode);
        if (dsStockist.Tables[0].Rows.Count > 0)
        {
            ddlFilter.DataTextField = "Sf_Name";
            ddlFilter.DataValueField = "Sf_Code";
            ddlFilter.DataSource = dsStockist;
            ddlFilter.DataBind();
        }
    }
    private void FillStockist_Reporting()
    {
        string sReport = ddlFilter.SelectedValue.ToString();
        Stockist sk = new Stockist();
        dsStockist = sk.getStockistCreate_Reporting(divcode, sReport);
        for (int i = 0; i < dsStockist.Tables[0].Rows.Count; i++)
        {
            if (dsStockist.Tables[0].Rows.Count > 0)
            {
                GetHQ_Filter();
            }
        }

        // Stockist sk = new Stockist();
        //dsStockist = sk.getStockist_Create(divcode, stockist_code);

        //if (dsStockist.Tables[0].Rows.Count > 0)
        //{
        //    txtStockist_Name.Text = dsStockist.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
        //    txtStockist_Address.Text = dsStockist.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
        //    txtStockist_ContactPerson.Text = dsStockist.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();
        //    txtStockist_Desingation.Text = dsStockist.Tables[0].Rows[0].ItemArray.GetValue(3).ToString();
        //    txtStockist_Mobile.Text = dsStockist.Tables[0].Rows[0].ItemArray.GetValue(4).ToString();
        //    ddlPoolName.Text = dsStockist.Tables[0].Rows[0].ItemArray.GetValue(5).ToString();
        //    sf_code = dsStockist.Tables[0].Rows[0].ItemArray.GetValue(6).ToString();
        //    ddlPlStatus.SelectedValue = dsStockist.Tables[0].Rows[0].ItemArray.GetValue(7).ToString();
        //    HidStockistCode.Value = stockist_code;
        //}
        string[] Salesforce;
        if (sf_code != "")
        {
            iIndex = -1;
            Salesforce = sf_code.Split(',');
            foreach (string sf in Salesforce)
            {
                foreach (DataListItem cb in DataList1.Items)
                {

                    CheckBox chk = (CheckBox)cb.FindControl("chkCategoryNameLabel");
                    HiddenField hf = (HiddenField)cb.FindControl("cbTestID");

                    if (sf == hf.Value)
                    {
                        //sChkSalesforce = sChkSalesforce + hf.Value + ",";
                        chk.Checked = true;

                    }

                }
            }
        }


    }

    private void FillStockist_Name()
    {
        Stockist sk = new Stockist();
        dsStockist = sk.getStockist_Name(divcode);
        if (dsStockist.Tables[0].Rows.Count > 0)
        {
            ddlStockist.DataTextField = "Stockist_Name";
            ddlStockist.DataValueField = "Stockist_Code";
            ddlStockist.DataSource = dsStockist;
            ddlStockist.DataBind();
        }
    }
    private void FillStockist_StockistName()
    {
        string stockist_code = ddlStockist.SelectedValue.ToString();
        Stockist sk = new Stockist();
        dsStockist = sk.getStockistCreate_StockistName(divcode, stockist_code);
        if (dsStockist.Tables[0].Rows.Count > 0)
        {
            ddlStockist.Visible = true;
            ddlStockist.DataSource = dsStockist;
            ddlStockist.DataBind();
        }
    }
    protected void ddlFilter_SelectedIndexChanged(object sender, EventArgs e)
    {
    }
    protected void btnGo_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        if (ddlFilter.SelectedIndex > 0)
        {
            FillStockist_Reporting();
        }
        else
        {
            FillCheckBoxList();
        }
    }

    private void Resetall()
    {
        txtStockist_Name.Text = "";
        txtStockist_Address.Text = "";
        txtStockist_ContactPerson.Text = "";
        txtStockist_Desingation.Text = "";
        txtStockist_Mobile.Text = "";
        txtTerritory.Text = "";
        //for (iIndex = 0; iIndex < chkboxSalesforce.Items.Count; iIndex++)
        //{
        //    chkboxSalesforce.Items[iIndex].Selected = false;
        //}
    }
    protected void ddlStockist_SelectedIndexChanged(object sender, EventArgs e)
    {

        string ddltext = ddlStockist.SelectedItem.ToString();
        Stockist sk = new Stockist();
        dsStockist = sk.getStockist_N(ddltext);
        if (dsStockist.Tables[0].Rows.Count > 0)
        {
            Label1.Visible = true;
            txtStockist_Name.Visible = true;
            lblStockist_Address.Visible = true;
            txtStockist_Address.Visible = true;
            lblStockist_ContactPerson.Visible = true;
            txtStockist_ContactPerson.Visible = true;
            lblStockist_Designation.Visible = true;
            txtStockist_Desingation.Visible = true;
            lblStockist_Mobile.Visible = true;
            txtStockist_Mobile.Visible = true;
            lblTerritory.Visible = false;
            txtTerritory.Visible = false;
            lblTitle_SalesforceDtls.Visible = true;
            lblFilter.Visible = true;
            ddlFilter.Visible = true;
            btnGo.Visible = true;
            btnSubmit.Visible = true;
            btnSave.Visible = true;
            txtStockist_Name.Text = dsStockist.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
            txtStockist_Address.Text = dsStockist.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();
            txtStockist_ContactPerson.Text = dsStockist.Tables[0].Rows[0].ItemArray.GetValue(3).ToString();
            txtStockist_Desingation.Text = dsStockist.Tables[0].Rows[0].ItemArray.GetValue(4).ToString();
            txtStockist_Mobile.Text = dsStockist.Tables[0].Rows[0].ItemArray.GetValue(5).ToString();
            txtTerritory.Text = dsStockist.Tables[0].Rows[0].ItemArray.GetValue(6).ToString();
            sf_code = dsStockist.Tables[0].Rows[0].ItemArray.GetValue(7).ToString();
            //HidStockistCode.Value = stockist_code;
            GetHQ();
            FillCheckBoxList();




        }

        //}

    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        //for (int i = 0; i < chkboxSalesforce.Items.Count; i++)
        //{
        //    if (chkboxSalesforce.Items[i].Selected)

        //        sChkSalesforce = chkboxSalesforce.Items[i].Value;
        //   chkboxSalesforce.Items[i].Attributes.Add("style", "Color: Red");


        foreach (DataListItem cb in DataList1.Items)
        {
            //  CheckBox chksales;
            //CheckBox cb = li.FindControl("chkCategoryNameLabel") as CheckBox;
            //HiddenField cb1 = li.FindControl("cbTestID") as HiddenField;

            CheckBox chk = (CheckBox)cb.FindControl("chkCategoryNameLabel");
            HiddenField hf = (HiddenField)cb.FindControl("cbTestID");

            if (chk.Checked)
            {
                sChkSalesforce = hf.Value;
                // int iReturn = -1;
                // Update Stockist Details
                Stockist sk = new Stockist();
                int iReturn = sk.RecordUpdate_Sale_Entry(divcode, sChkSalesforce);

                if (iReturn > 0)
                {
                    //menu1.Status = "Stockist updated Successfully";
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Updated Successfully');</script>");
                    Resetall();
                }
                else if (iReturn == -2)
                {
                    // menu1.Status = "Stockist exist with the same stockist name!!";
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Already Exist with the Same Name');</script>");
                }
            }
            //}
        }
    }


    protected void btnSave_Click(object sender, EventArgs e)
    {
        btnSubmit_Click(sender, e);
    }


    protected void dlAlpha_SelectedIndexChanged(object sender, EventArgs e)
    {
        string ddlvar = dlAlpha.SelectedItem.ToString();

        // string ddltext = ddlStockist.SelectedItem.ToString();
        Stockist sk = new Stockist();
        if (ddlvar == "---ALL---")
        {
            dsStockist = sk.getStockist_Alphabet_N();
        }
        else
        {
            //ddlStockist.Items.Add("---Select the Stockist---");
            dsStockist = sk.getStockist_Alphabet(ddlvar);
        }
        if (dsStockist != null)
        {

            // ddlStockist.Items = dsStockist["Stockist_Name"];
            if (dsStockist.Tables[0].Rows.Count > 0)
            {

                ddlStockist.Visible = true;
                ddlStockist.DataSource = dsStockist;
                ddlStockist.DataBind();
            }
        }
    }

    public void GetHQ_Filter()
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();

        dt.Columns.Add("sf_Name");
        dt.Columns.Add("Reporting_To_SF");
        dt.Columns.Add("HQ");
        dt.Columns.Add("sf_code");
        DataRow dr;
        string hq = "";
        string sReport = ddlFilter.SelectedValue.ToString();
        Stockist sk = new Stockist();
        ds = sk.getStockistCreate_Reporting(divcode, sReport);

        //  DataSet ds = GetDbData();
        if (ds.Tables[0].Rows.Count > 0)
        {
            hq = ds.Tables[0].Rows[0]["HQ"].ToString();
            dr = dt.NewRow();
            dr[0] = ds.Tables[0].Rows[0]["sf_Name"].ToString();
            dr[1] = ds.Tables[0].Rows[0]["Reporting_To_SF"].ToString();
            dr[2] = ds.Tables[0].Rows[0]["HQ"].ToString();
            dr[3] = ds.Tables[0].Rows[0]["sf_code"].ToString();
            dt.Rows.Add(dr);
            for (int i = 1; i < ds.Tables[0].Rows.Count; i++)
            {

                dr = dt.NewRow();

                dr[0] = ds.Tables[0].Rows[i][1];
                dr[3] = ds.Tables[0].Rows[i][0];
                //  dr[1] = ds.Tables[0].Rows[i][1];

                if (hq == ds.Tables[0].Rows[i][4].ToString())
                {

                    dr[2] = "";

                }

                else
                {

                    dr[2] = ds.Tables[0].Rows[i][4];

                    hq = ds.Tables[0].Rows[i][4].ToString();

                }

                //dt.Columns.Add("SF_Name");
                //dt.Columns.Add("sf_code");
                dt.Rows.Add(dr);


            }
            DataList1.DataSource = dt;
            DataList1.DataBind();


        }

    }

    public void GetHQ()
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();

        dt.Columns.Add("sf_Name");
        dt.Columns.Add("Reporting_To_SF");
        dt.Columns.Add("HQ");
        dt.Columns.Add("sf_code");
        DataRow dr;
        string hq = "";
        Stockist sk = new Stockist();
        ds = sk.getSalesforce(divcode);

        //  DataSet ds = GetDbData();
        if (ds.Tables[0].Rows.Count > 0)
        {
            hq = ds.Tables[0].Rows[0]["HQ"].ToString();
            dr = dt.NewRow();
            dr[0] = ds.Tables[0].Rows[0]["sf_Name"].ToString();
            dr[1] = ds.Tables[0].Rows[0]["Reporting_To_SF"].ToString();
            dr[2] = ds.Tables[0].Rows[0]["HQ"].ToString();
            dr[3] = ds.Tables[0].Rows[0]["sf_code"].ToString();
            dt.Rows.Add(dr);
            for (int i = 1; i < ds.Tables[0].Rows.Count; i++)
            {

                dr = dt.NewRow();

                dr[0] = ds.Tables[0].Rows[i][1];
                dr[3] = ds.Tables[0].Rows[i][0];
                //  dr[1] = ds.Tables[0].Rows[i][1];

                if (hq == ds.Tables[0].Rows[i][4].ToString())
                {

                    dr[2] = "";

                }

                else
                {

                    dr[2] = ds.Tables[0].Rows[i][4];

                    hq = ds.Tables[0].Rows[i][4].ToString();

                }

                //dt.Columns.Add("SF_Name");
                //dt.Columns.Add("sf_code");
                dt.Rows.Add(dr);


            }
            DataList1.DataSource = dt;
            DataList1.DataBind();


        }

    }

}