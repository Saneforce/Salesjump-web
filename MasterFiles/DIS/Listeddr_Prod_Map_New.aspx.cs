using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
using System.Data.SqlClient;
using System.Configuration;

public partial class MasterFiles_MR_ListedDoctor_Listeddr_Prod_Map_New : System.Web.UI.Page
{
    DataSet dsListedDR = null;
    DataSet dsProdDR = null;
    string sf_code = string.Empty;
    string div_code = string.Empty;
    DataSet dsDocSubCat = null;
    string state_code = string.Empty;
    DataSet dsCatgType = null;
    string Listed_DR_Code = string.Empty;
    string doctype = string.Empty;
    string chkCampaign = string.Empty;
    string Doc_SubCatCode = string.Empty;
    int iIndex = -1;
    string sCmd = string.Empty;
    int iReturn = -1;
    int time;

    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        sf_code = Session["sf_code"].ToString();

        if (!Page.IsPostBack)
        {
            //menu1.Title = this.Page.Title;
            //menu1.FindControl("btnBack").Visible = false;


            if (Session["sf_type"].ToString() == "1")
            {
                // sfCode = Session["sf_code"].ToString();
                DataList1.BackColor = System.Drawing.Color.White;

                UserControl_MR_Menu Usc_MR =
                (UserControl_MR_Menu)LoadControl("~/UserControl/MR_Menu.ascx");
                Divid.Controls.Add(Usc_MR);
                Usc_MR.Title = this.Page.Title;
                //    Usc_MR.FindControl("btnBack").Visible = false;

                FillDoc();

            }
            else
            {
                DataList1.BackColor = System.Drawing.Color.White;
                UserControl_MenuUserControl Usc_Menu =
                (UserControl_MenuUserControl)LoadControl("~/UserControl/MenuUserControl.ascx");
                Divid.Controls.Add(Usc_Menu);

                Usc_Menu.Title = this.Page.Title;
                Session["backurl"] = "LstDoctorList.aspx";
                //  Usc_Menu.FindControl("btnBack").Visible = false;
                FillDoc();

                //  getWorkName();
            }
        }
        else
        {
            if (Session["sf_type"].ToString() == "1")
            {
                UserControl_MR_Menu Usc_MR1 =
                        (UserControl_MR_Menu)LoadControl("~/UserControl/MR_Menu.ascx");
                Divid.Controls.Add(Usc_MR1);
                Usc_MR1.Title = this.Page.Title;

            }
            else
            {
                UserControl_MenuUserControl Usc_Menu =
               (UserControl_MenuUserControl)LoadControl("~/UserControl/MenuUserControl.ascx");
                Divid.Controls.Add(Usc_Menu);
                Session["backurl"] = "LstDoctorList.aspx";

            }
        }
    }

    private void FillDoc()
    {
        lblSelect.Visible = true;
        lblSelect.Text = "Select Listed Customer Name";
        ListedDR LstDoc = new ListedDR();
        dsListedDR = LstDoc.getListedDr_for_Mapp(sf_code, div_code);
        //ViewState["DrCode"] = dsListedDR;
        if (dsListedDR.Tables[0].Rows.Count > 0)
        {


            ddldr.DataTextField = "ListedDr_Name";
            ddldr.DataValueField = "ListedDrCode";
            ddldr.DataSource = dsListedDR;
            ddldr.DataBind();

        }
        else
        {
            ddldr.DataSource = dsListedDR;
            ddldr.DataBind();

        }
    }
    protected void btnGo_Click(object sender, EventArgs e)
    {
        //chkprd.Visible = true;
        lblSelect.Visible = true;
        DataList1.Visible = true;
        FillPrd();
    }

    private void FillPrd()
    {
        //btnGo.Enabled = false;
        lblSelect.Text = "Select the Product";
        btnSubmit.Visible = true;
        Product spec = new Product();
        DataSet dsChkSp = new DataSet();
        dsChkSp = spec.getPrd_For_Mapp(div_code);   
        if (dsChkSp.Tables[0].Rows.Count > 0)
        {
            //btnSave.Visible = true;
            DataList1.Visible = true;
            DataList1.DataSource = dsChkSp;
            DataList1.DataBind();
        }
        else
        {
            DataList1.DataSource = dsChkSp;
            DataList1.DataBind();
        }


        string str_CateCode = "";
        Product prd = new Product();
        dsProdDR = prd.getprdfor_Mappdr(ddldr.SelectedValue);

        if (dsProdDR.Tables[0].Rows.Count > 0)
        {

            for (int i = 0; i < dsProdDR.Tables[0].Rows.Count; i++)
            {
                str_CateCode = dsProdDR.Tables[0].Rows[i].ItemArray.GetValue(0).ToString();

                foreach (DataListItem grid in DataList1.Items)
                {
                    Label chk = (Label)grid.FindControl("lblPrdCode");

                    string[] Salesforce;
                    if (str_CateCode != "")
                    {
                        iIndex = -1;
                        Salesforce = str_CateCode.Split(',');
                        foreach (string sf in Salesforce)
                        {

                            CheckBox chkCatName = (CheckBox)grid.FindControl("chkCatName");
                            Label hf = (Label)grid.FindControl("lblPrdCode");

                            if (sf == hf.Text)
                            {
                                chkCatName.Checked = true;
                                chkCatName.Attributes.Add("style", "Color: Red; font-weight:Bold; font-size:16px; ");
                            }
                        }
                    }
                }
            }
        }       
        
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);

        string strPrd = "";
        string srtpd = "";


        foreach (DataListItem grid in DataList1.Items)
        {
            Label chk = (Label)grid.FindControl("lblPrdCode");
            CheckBox chkCatName = (CheckBox)grid.FindControl("chkCatName");

            if (chkCatName.Checked == true)
            {
                strPrd += chk.Text + ",";
            }
        }


        if (strPrd != "")
        {

            strPrd = strPrd.Remove(strPrd.Length - 1);

            //string[] Prd = strPrd_name.Split(',');
            string[] Prd_code = strPrd.Split(',');

            foreach (string Prd_Cod in Prd_code)
            {
                ListedDR lst = new ListedDR();
                int iReturn = lst.RecordAdd_ProductMap_New(ddldr.SelectedValue, Prd_Cod, ddldr.SelectedItem.Text, sf_code, div_code);
                if (iReturn > 0)
                {
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Mapped Successfully');</script>");
                    DataList1.Visible = false;
                    btnSubmit.Visible = false;
                    lblSelect.Text = "Select Listed Doctor Name";
                }
            }
        }
        foreach (DataListItem grid in DataList1.Items)
        {
            Label chk = (Label)grid.FindControl("lblPrdCode");
            CheckBox chkCatName = (CheckBox)grid.FindControl("chkCatName");

            if (!chkCatName.Checked)
            {
                if (chkCatName.Checked == false)
                {
                    srtpd += chk.Text + ",";
                }
            }
        }


        if (srtpd != "")
        {
            srtpd = srtpd.Remove(srtpd.Length - 1);

            string[] Prd_cod = srtpd.Split(',');

            foreach (string Prd_Co in Prd_cod)
            {
                ListedDR lstdr = new ListedDR();
                int iReturn = lstdr.Delete_ProductMap(ddldr.SelectedValue, Prd_Co, sf_code, div_code);
                if (iReturn == -1)
                {
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Mapped Successfully');</script>");
                    DataList1.Visible = false;
                    btnSubmit.Visible = false;
                    lblSelect.Text = "Select Listed Doctor Name";
                }
            }


        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        ddldr.SelectedValue = "0";
        FillDoc();
        DataList1.Visible = false;
        btnSubmit.Visible = false;
        lblSelect.Text = "Select Listed Customer Name";
    }
}

