using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using Bus_EReport;

public partial class MasterFiles_Options_Mob_App_Setting : System.Web.UI.Page
{

    string div_code = string.Empty;
    DataSet dsadmin = new DataSet();
    DataSet dsadm = new DataSet();
    DataSet dsSalesForce = new DataSet();
    DataSet dsadmi = new DataSet();
    int iIndex = -1;
    string chkhaf = string.Empty;

    int check = 0;
    int geo_code = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();

        if (!Page.IsPostBack)
        {
            Fillsalesforce();
            FillHalfDay_Work();
            //menu1.Title = this.Page.Title;
            //menu1.FindControl("btnBack").Visible = false;
            AdminSetup dv = new AdminSetup();
            dsadmin = dv.getMobApp_Setting(div_code);

            if (dsadmin.Tables[0].Rows.Count > 0)
            {
                if (dsadmin.Tables[0].Rows[0].ItemArray.GetValue(0).ToString() == "0")
                {
                    rdomandt.SelectedValue = "0";
                }
                else
                {
                    rdomandt.SelectedValue = "1";
                }

                if (dsadmin.Tables[0].Rows[0].ItemArray.GetValue(1).ToString() == "1")
                {
                    rdogeo.SelectedValue = "1";
                }
                else
                {
                    rdogeo.SelectedValue = "0";
                }
                txtcover.Text = dsadmin.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();
                txtvisit1.Text = dsadmin.Tables[0].Rows[0].ItemArray.GetValue(3).ToString();
                txtvisit2.Text = dsadmin.Tables[0].Rows[0].ItemArray.GetValue(4).ToString();
                txtvisit3.Text = dsadmin.Tables[0].Rows[0].ItemArray.GetValue(5).ToString();
                txtvisit4.Text = dsadmin.Tables[0].Rows[0].ItemArray.GetValue(6).ToString();

                if (dsadmin.Tables[0].Rows[0].ItemArray.GetValue(7).ToString() == "0")
                {
                    rdoprd_entry_doc.SelectedValue = "0";
                }
                else
                {
                    rdoprd_entry_doc.SelectedValue = "1";
                }
                txtRx_Cap_doc.Text = dsadmin.Tables[0].Rows[0].ItemArray.GetValue(8).ToString();
                txtSamQty_Cap_doc.Text = dsadmin.Tables[0].Rows[0].ItemArray.GetValue(9).ToString();

                if (dsadmin.Tables[0].Rows[0].ItemArray.GetValue(10).ToString() == "0")
                {
                    rdoinput_Ent_doc.SelectedValue = "0";
                }
                else
                {
                    rdoinput_Ent_doc.SelectedValue = "1";
                }

                if (dsadmin.Tables[0].Rows[0].ItemArray.GetValue(11).ToString() == "0")
                {
                    rdoNeed_chem.SelectedValue = "0";
                }
                else
                {
                    rdoNeed_chem.SelectedValue = "1";
                }

                if (dsadmin.Tables[0].Rows[0].ItemArray.GetValue(12).ToString() == "0")
                {
                    rdoProduct_entr_chem.SelectedValue = "0";
                }
                else
                {
                    rdoProduct_entr_chem.SelectedValue = "1";
                }

                txtqty_Cap_chem.Text = dsadmin.Tables[0].Rows[0].ItemArray.GetValue(13).ToString();

                if (dsadmin.Tables[0].Rows[0].ItemArray.GetValue(14).ToString() == "0")
                {
                    rdoinpu_entry_chem.SelectedValue = "0";
                }
                else
                {
                    rdoinpu_entry_chem.SelectedValue = "1";
                }

                if (dsadmin.Tables[0].Rows[0].ItemArray.GetValue(15).ToString() == "0")
                {
                    rdoNeed_stock.SelectedValue = "0";
                }
                else
                {
                    rdoNeed_stock.SelectedValue = "1";
                }

                if (dsadmin.Tables[0].Rows[0].ItemArray.GetValue(16).ToString() == "0")
                {
                    rdoprdentry_stock.SelectedValue = "0";
                }
                else
                {
                    rdoprdentry_stock.SelectedValue = "1";
                }

                txtQty_Cap_stock.Text = dsadmin.Tables[0].Rows[0].ItemArray.GetValue(17).ToString();

                if (dsadmin.Tables[0].Rows[0].ItemArray.GetValue(18).ToString() == "0")
                {
                    rdoinpu_entry_stock.SelectedValue = "0";
                }
                else
                {
                    rdoinpu_entry_stock.SelectedValue = "1";
                }

                if (dsadmin.Tables[0].Rows[0].ItemArray.GetValue(19).ToString() == "0")
                {
                    rdoneed_unlistDr.SelectedValue = "0";
                }
                else
                {
                    rdoneed_unlistDr.SelectedValue = "1";
                }

                if (dsadmin.Tables[0].Rows[0].ItemArray.GetValue(20).ToString() == "0")
                {
                    rdoprdentry_unlistDr.SelectedValue = "0";
                }
                else
                {
                    rdoprdentry_unlistDr.SelectedValue = "1";
                }

                txtRxQty_Cap_unlistDr.Text = dsadmin.Tables[0].Rows[0].ItemArray.GetValue(21).ToString();

                txtSamQty_Cap_unlistDr.Text = dsadmin.Tables[0].Rows[0].ItemArray.GetValue(22).ToString();

                if (dsadmin.Tables[0].Rows[0].ItemArray.GetValue(23).ToString() == "0")
                {
                    rdoinpuEnt_Need_unlistDr.SelectedValue = "0";
                }
                else
                {
                    rdoinpuEnt_Need_unlistDr.SelectedValue = "1";
                }
            }

            for (int i = 0; i < chkhaf_work.Items.Count; i++)
            {
               
                AdminSetup adm = new AdminSetup();
                dsadm = dv.getMobApp_Setting_halfday(div_code, chkhaf_work.Items[i].Value);

                if (dsadm.Tables[0].Rows[0].ItemArray.GetValue(0).ToString() == "1")
                {
                    chkhaf_work.Items[i].Selected = true;
                }
                else
                {
                    chkhaf_work.Items[i].Selected = false;
                }

            }



            foreach (GridViewRow gridRow in grdgps.Rows)
            {
                CheckBox chkId = (CheckBox)gridRow.Cells[0].FindControl("chkId");
                bool bCheck = chkId.Checked;
                Label lblSF_Code = (Label)gridRow.Cells[2].FindControl("lblSF_Code");
                string sf_Code = lblSF_Code.Text.ToString();

                if (sf_Code != "")
                {

                    AdminSetup ad = new AdminSetup();
                    dsadmi = dv.getMobApp_geo(sf_Code);

                    if (dsadmi.Tables[0].Rows[0].ItemArray.GetValue(0).ToString() == "0")
                    {

                        chkId.Checked = true;

                    }
                    else
                    {
                        chkId.Checked = false;
                    }
                }
            }
            
        }

    }

    private void FillHalfDay_Work()
    {
        AdminSetup adm = new AdminSetup();

        dsadmin = adm.gethalf_Daywrk(div_code);

        chkhaf_work.DataSource = dsadmin;
        chkhaf_work.DataTextField = "Worktype_Name_B";
        chkhaf_work.DataValueField = "WorkType_Code_B";
        chkhaf_work.DataBind();
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        AdminSetup admin = new AdminSetup();
        int iReturn = admin.RecordUpdate_MobApp(Convert.ToInt16(rdomandt.SelectedValue.ToString()),Convert.ToInt16(rdogeo.SelectedValue.ToString()),float.Parse(txtcover.Text.ToString()), txtvisit1.Text.ToString(), txtvisit2.Text.ToString(), txtvisit3.Text.ToString(), txtvisit4.Text.ToString(),Convert.ToInt16(rdoprd_entry_doc.SelectedValue.ToString()), txtRx_Cap_doc.Text.ToString(), txtSamQty_Cap_doc.Text.ToString(),Convert.ToInt16(rdoinput_Ent_doc.SelectedValue.ToString()),Convert.ToInt16(rdoNeed_chem.SelectedValue.ToString()),Convert.ToInt16(rdoProduct_entr_chem.SelectedValue.ToString()), txtqty_Cap_chem.Text.ToString(),Convert.ToInt16(rdoinpu_entry_chem.SelectedValue.ToString()),Convert.ToInt16(rdoNeed_stock.SelectedValue.ToString()),Convert.ToInt16(rdoprdentry_stock.SelectedValue.ToString()), txtQty_Cap_stock.Text.ToString(),Convert.ToInt16(rdoinpu_entry_stock.SelectedValue.ToString()),Convert.ToInt16(rdoneed_unlistDr.SelectedValue.ToString()),Convert.ToInt16(rdoprdentry_unlistDr.SelectedValue.ToString()), txtRxQty_Cap_unlistDr.Text.ToString(), txtSamQty_Cap_unlistDr.Text.ToString(),Convert.ToInt16(rdoinpuEnt_Need_unlistDr.SelectedValue.ToString()),div_code);
        for (int i = 0; i < chkhaf_work.Items.Count; i++)
        {
            if (chkhaf_work.Items[i].Selected)
            {

                check = 1;
                AdminSetup dv = new AdminSetup();
                iReturn = dv.RecordUpdate_Forhalfday(chkhaf_work.Items[i].Value, div_code, check);
            }
            else
            {
                check = 0;
                AdminSetup dv = new AdminSetup();
                iReturn = dv.RecordUpdate_Forhalfday(chkhaf_work.Items[i].Value, div_code, check);
            }
        }



        if (iReturn > 0)
        {
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Setup has been updated Successfully');</script>");
        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {

    }

    private void Fillsalesforce()
    {
        SalesForce sf = new SalesForce();
        dsSalesForce = sf.UserListTP_Hierarchy(div_code, "admin");

        dsSalesForce.Tables[0].Rows[0].Delete();
        dsSalesForce.Tables[0].Rows[0].Delete();
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            grdgps.Visible = true;
            grdgps.DataSource = dsSalesForce;
            grdgps.DataBind();
        }
     
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
      
        int iReturn = -1;

        foreach (GridViewRow gridRow in grdgps.Rows)
        {
            CheckBox chkId = (CheckBox)gridRow.Cells[0].FindControl("chkId");
            bool bCheck = chkId.Checked;
            Label lblSF_Code = (Label)gridRow.Cells[2].FindControl("lblSF_Code");
            string sf_Code = lblSF_Code.Text.ToString();

            if ((sf_Code.Trim().Length > 0) && (bCheck == true))
            {
                            
                AdminSetup ad = new AdminSetup();
                geo_code = 0;

                iReturn = ad.RecordUpdate_geosf_code(sf_Code, geo_code);
            }
            else
            {
                if ((sf_Code.Trim().Length > 0) && (bCheck == false))
                {

                    AdminSetup ad = new AdminSetup();
                    geo_code = 1;

                    iReturn = ad.RecordUpdate_geosf_code(sf_Code, geo_code);
                }
            }
        }

        if (iReturn != -1)
        {
            //  menu1.Status = "Chemists De-Activated Successfully!!";
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Saved Successfully');</script>");
           
   
        }
        //pnlpopup.Style.Add("display", "none");
        //pnlpopup.Style.Add("visibility", "hidden");
    }
    protected void linkgps_Click(object sender, EventArgs e)
    {
        pnlpopup.Style.Add("display", "block");
        pnlpopup.Style.Add("visibility", "visible");
    }

   
}