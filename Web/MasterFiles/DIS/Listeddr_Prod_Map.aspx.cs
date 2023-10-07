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


public partial class MasterFiles_MR_ListedDoctor_Listeddr_Prod_Map : System.Web.UI.Page
{
    DataSet dsListedDR = null;
    DataSet dsProdDR = null;
    string sf_code = string.Empty;
    string div_code = string.Empty;
    DataSet dsDocSubCat = null;

    DataSet dsCatgType = null;
    string Listed_DR_Code = string.Empty;
    string doctype = string.Empty;
    string chkCampaign = string.Empty;
    string Doc_SubCatCode = string.Empty;

    string sCmd = string.Empty;
    int iReturn = -1;

    int iIndex = -1;
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

                UserControl_MR_Menu Usc_MR =
                (UserControl_MR_Menu)LoadControl("~/UserControl/MR_Menu.ascx");
                Divid.Controls.Add(Usc_MR);
                Usc_MR.Title = this.Page.Title;
            //    Usc_MR.FindControl("btnBack").Visible = false;

                FillDoc();
                FillCampaign();

            }
            else
            {

                UserControl_MenuUserControl Usc_Menu =
                (UserControl_MenuUserControl)LoadControl("~/UserControl/MenuUserControl.ascx");
                Divid.Controls.Add(Usc_Menu);

                Usc_Menu.Title = this.Page.Title;
                Session["backurl"] = "LstDoctorList.aspx";
              //  Usc_Menu.FindControl("btnBack").Visible = false;
                FillDoc();
                FillCampaign();

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

    private void GetCampaign()
    {
        foreach (GridViewRow row in grdDoctor.Rows)
        {
            GridView grdCampaign = row.FindControl("grdCampaign") as GridView;
            foreach (GridViewRow grid in grdCampaign.Rows)
            {
                CheckBox chk = (CheckBox)grid.FindControl("chkCatName");
                Label hf = (Label)grid.FindControl("cbSubCat");

                if (chk.Checked == false)
                {
                    //sChkSalesforce = sChkSalesforce + hf.Value + ",";
                    chk.Checked = false;
                    chk.Style.Add("color", "Black");

                }



            }
        }
    }
    private void FillCampaign()
    {
        foreach (GridViewRow row in grdDoctor.Rows)
        {
            Label lblcode = (Label)row.FindControl("lblDrcode");
            GridView grdCampaign = row.FindControl("grdCampaign") as GridView;
            string str_CateCode = "";
            ListedDR LstDoc = new ListedDR();
            dsProdDR = LstDoc.get_Prod(lblcode.Text);
            if (dsProdDR.Tables[0].Rows.Count > 0)
            {

                for (int i = 0; i < dsProdDR.Tables[0].Rows.Count; i++)
                {
                    str_CateCode = dsProdDR.Tables[0].Rows[i].ItemArray.GetValue(2).ToString();



                    foreach (GridViewRow grid in grdCampaign.Rows)
                    {
                        Label SubCatCode = (Label)grid.FindControl("cbSubCat");

                        //ListedDR LstDoc = new ListedDR();
                        //dsListedDR = LstDoc.get_Camp(lblcode.Text);
                        //if (dsListedDR.Tables[0].Rows.Count > 0)
                        //{
                        //    SubCatCode.Text = dsDocSubCat.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                        //}

                        string[] Salesforce;
                        if (str_CateCode != "")
                        {
                            iIndex = -1;
                            Salesforce = str_CateCode.Split(',');
                            foreach (string sf in Salesforce)
                            {

                                CheckBox chk = (CheckBox)grid.FindControl("chkCatName");
                                Label hf = (Label)grid.FindControl("cbSubCat");
                                DropDownList prior = (DropDownList)grid.FindControl("ddlPriority");

                                if (sf == hf.Text)
                                {
                                    //sChkSalesforce = sChkSalesforce + hf.Value + ",";
                                    chk.Checked = true;
                                    prior.SelectedValue = dsProdDR.Tables[0].Rows[i].ItemArray.GetValue(3).ToString();
                                    chk.Attributes.Add("style", "Color: Red; font-weight:Bold; ");


                                }


                            }
                        }
                    }

                }
            }
        }

    }
    private void FillDoc()
    {
        ListedDR LstDoc = new ListedDR();
        dsListedDR = LstDoc.getListedDr_Product(sf_code);
        ViewState["DrCode"] = dsListedDR;
        if (dsListedDR.Tables[0].Rows.Count > 0)
        {

            grdDoctor.Visible = true;
            grdDoctor.DataSource = dsListedDR;
            grdDoctor.DataBind();

        }
        else
        {
            grdDoctor.DataSource = dsListedDR;
            grdDoctor.DataBind();

        }
        foreach (DataRow drFF in dsListedDR.Tables[0].Rows)
        {
            foreach (GridViewRow grid_row in grdDoctor.Rows)
            {
                Label lblDrName = (Label)grid_row.FindControl("lblDrName");
                Label lblDrcode = (Label)grid_row.FindControl("lblDrcode");
                if (drFF["ListedDrCode"].ToString() == lblDrcode.Text)
                {
                    if (drFF["Product_Detail_Name"].ToString().Length > 0)
                    {
                        lblDrName.ForeColor = System.Drawing.Color.White;
                        lblDrName.BackColor = System.Drawing.Color.BlueViolet;
                    }
                }
            }
        }
    }
    protected void grdCampaign_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            e.Row.Attributes.Add("checked", Page.ClientScript.GetPostBackEventReference(sender as GridView, "Select$" + e.Row.RowIndex.ToString()));
        }

    }
    protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            //e.Row.Attributes.Add("onclick", Page.ClientScript.GetPostBackEventReference(sender as GridView,"Select$" + e.Row.RowIndex.ToString()));

            GridView GCamp = (GridView)e.Row.FindControl("grdCampaign");

            Product dv = new Product();
            dsDocSubCat = dv.getProd(div_code);
            if (dsDocSubCat.Tables[0].Rows.Count > 0)
            {
                GCamp.Visible = true;
                GCamp.DataSource = dsDocSubCat;
                GCamp.DataBind();
            }
            else
            {
                GCamp.DataSource = dsDocSubCat;
                GCamp.DataBind();
            }

        }

        //else
        //{
        //    GCamp.DataSource = dsDocSubCat;
        //    GCamp.DataBind();
        //}
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DropDownList ddlType = (DropDownList)e.Row.FindControl("ddlType");
            if (ddlType != null)
            {
                DataRowView row = (DataRowView)e.Row.DataItem;
                ddlType.SelectedIndex = ddlType.Items.IndexOf(ddlType.Items.FindByText(row["Doc_Type"].ToString()));
            }
        }
        if (e.Row.RowType == DataControlRowType.Header)
        {
            Territory terr = new Territory();
            DataSet dsTerritory = new DataSet();
            dsTerritory = terr.getWorkAreaName(div_code);
            if (dsTerritory.Tables[0].Rows.Count > 0)
            {
                e.Row.Cells[7].Text = dsTerritory.Tables[0].Rows[0]["wrk_area_Name"].ToString();

            }
        }

    }
    protected void chkCatName_OnCheckedChanged(object sender, EventArgs e)
    {
        string CampaignName = string.Empty;
        foreach (GridViewRow row in grdDoctor.Rows)
        {
            Label lblcode = (Label)row.FindControl("lblDrcode");
            Label lblCatName = (Label)row.FindControl("Doc_SubCatName");

            chkCampaign = "";
            CampaignName = "";
            GridView grdCampaign = row.FindControl("grdCampaign") as GridView;
            foreach (GridViewRow grid in grdCampaign.Rows)
            {

                CheckBox chk = (CheckBox)grid.FindControl("chkCatName");
                Label hf = (Label)grid.FindControl("cbSubCat");

                if (chk.Checked)
                {
                    // str = lblcode.Text;
                    //chkCampaign = chkCampaign + chk.Text + ",";

                    chkCampaign = chkCampaign + hf.Text + ",";
                    CampaignName = CampaignName + chk.Text + ",";
                }

            }
            lblCatName.Text = CampaignName;


        }
    }
    protected void Show_Hide_OrdersGrid(object sender, EventArgs e)
    {
        ImageButton imgShowHide = (sender as ImageButton);
        GridViewRow row = (imgShowHide.NamingContainer as GridViewRow);
        if (imgShowHide.CommandArgument == "Show")
        {
            row.FindControl("pnlOrders").Visible = true;
            imgShowHide.CommandArgument = "Hide";
            imgShowHide.ImageUrl = "~/images/minus.png";
            row.Focus();
        }
        else
        {
            row.FindControl("pnlOrders").Visible = false;
            imgShowHide.CommandArgument = "Show";
            imgShowHide.ImageUrl = "~/images/plus.png";

        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string Priority = string.Empty;

        foreach (GridViewRow row in grdDoctor.Rows)
        {
            Label lblcode = (Label)row.FindControl("lblDrcode");
            Label lblDrName = (Label)row.FindControl("lblDrName");


            chkCampaign = "";
            GridView grdCampaign = row.FindControl("grdCampaign") as GridView;
            foreach (GridViewRow grid in grdCampaign.Rows)
            {
                DropDownList prior = (DropDownList)grid.FindControl("ddlPriority");
                Priority = prior.SelectedValue;

                CheckBox chk = (CheckBox)grid.FindControl("chkCatName");
                Label hf = (Label)grid.FindControl("cbSubCat");
                chkCampaign = "";
                if (chk.Checked)
                {
                    // str = lblcode.Text;
                    //chkCampaign = chkCampaign + chk.Text + ",";
                    chkCampaign = chkCampaign + hf.Text + ",";

                    string[] strProductSplit = chkCampaign.Split(',');
                    foreach (string strprod in strProductSplit)
                    {
                        if (strprod != "")
                        {
                            if (Priority == "0")
                            {
                                Priority = null;
                            }
                            ListedDR lst = new ListedDR();
                            int iReturn = lst.RecordAdd_ProductMap(lblcode.Text, strprod, Priority, chk.Text, lblDrName.Text, sf_code, div_code);
                            if (iReturn > 0)
                            {
                                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Mapped Successfully');</script>");
                                FillDoc();
                                FillCampaign();
                            }
                        }
                    }

                }
                else
                {
                dsListedDR = (DataSet)ViewState["DrCode"];
                foreach (DataRow drFF in dsListedDR.Tables[0].Rows)
                {
                    if (drFF["ListedDrCode"].ToString() == lblcode.Text)
                    {
                      

                            chkCampaign = chkCampaign + hf.Text + ",";

                            string[] strProductSplit = chkCampaign.Split(',');
                            foreach (string strprod in strProductSplit)
                            {
                                if (strprod != "")
                                {
                                    ListedDR lstdr = new ListedDR();
                                    int iReturn = lstdr.Delete_ProductMap(lblcode.Text, strprod, sf_code, div_code);
                                }
                            }
                        }
                    }
                }


            }

        }
    }


    protected void btnSave_Click(object sender, EventArgs e)
    {
        btnSubmit_Click(sender, e);
    }
}