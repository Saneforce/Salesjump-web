using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;

public partial class MasterFiles_MR_Chemist_BulkEdit_Chemists : System.Web.UI.Page
{
    #region "Declaration"
    DataSet dsChemists = null;
    DataSet dsTerritory = null;
    string div_code = string.Empty;
    string Chemists_Code = string.Empty;
    string Chemists_Name = string.Empty;
    string Chemists_Address = string.Empty;
    string Chemists_Contact = string.Empty;
    string Chemists_Phone = string.Empty;
    string Chemists_Fax = string.Empty;
    string Chemists_EMail = string.Empty;
    string Chemists_Terr = string.Empty;
    string Chemists_Mobile = string.Empty;
    string Terr_Code = string.Empty;
    string sf_code = string.Empty;
    int i;
    int iReturn = -1;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int time;
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        //sf_code = Session["sf_code"].ToString();
        div_code = Session["div_code"].ToString();
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
            //menu1.Visible = false;
            Session["backurl"] = "ChemistList.aspx";
            lblTerrritory.Text = "( " + "<span style='font-weight: bold;color:Maroon;'>For " + Session["sfName"] + " </span>" + " - " +
                              "<span style='font-weight: bold;color:Maroon;'> " + Session["sf_Designation_Short_Name"] + " </span>" + " - " +
                               "<span style='font-weight: bold;color:Maroon;'>  " + Session["sf_HQ"] + "</span>" + " )";
        }
        if (!Page.IsPostBack)
        {
            Session["backurl"] = "ChemistList.aspx";
            //menu1.Title = this.Page.Title;
            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);
            getWorkName();
        }
    }
    private void getWorkName()
    {
        Territory terr = new Territory();
        dsTerritory = terr.getWorkAreaName(div_code);
        if (dsTerritory.Tables[0].Rows.Count > 0)
        {
          //  string str = "Doctor " + dsTerritory.Tables[0].Rows[0]["wrk_area_Name"]; 
            grdChemists.Columns[10].HeaderText = dsTerritory.Tables[0].Rows[0]["wrk_area_Name"].ToString();
            CblChemistsCode.Items.Add(new ListItem(" " +dsTerritory.Tables[0].Rows[0]["wrk_area_Name"].ToString(), "Territory_Code", true));     
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

   
    protected void grdChemist_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            Territory terr = new Territory();
            DataSet dsTerritory = new DataSet();
            dsTerritory = terr.getWorkAreaName(div_code);
            if (dsTerritory.Tables[0].Rows.Count > 0)
            {
                e.Row.Cells[10].Text = dsTerritory.Tables[0].Rows[0]["wrk_area_Name"].ToString();
              //  CblChemistsCode.Items.Add(new ListItem(dsTerritory.Tables[0].Rows[0]["wrk_area_Name"].ToString(), "Territory_Code", true));

            }
        }
    }    

    private void FillChemists()
    {        
        Chemist chem = new Chemist();
        dsChemists = chem.getChemistsDetails(sf_code);

        if (dsChemists.Tables[0].Rows.Count > 0)
        {
            btnSave.Visible = true;
            btnUpdate.Visible = true;
            grdChemists.Visible = true;
            grdChemists.DataSource = dsChemists;
            grdChemists.DataBind();

            foreach (GridViewRow gridRow in grdChemists.Rows)
            {
                DropDownList ddlTerr = (DropDownList)gridRow.Cells[9].FindControl("Territory_Code");

                Label lblChemists_Code = (Label)gridRow.Cells[1].FindControl("Chemists_Code");
                Chemists_Code = lblChemists_Code.Text.ToString();

                DataSet dsTerr = chem.getTerritory_Chemists(Chemists_Code, sf_code);
                if (dsTerr.Tables[0].Rows.Count > 0)
                {
                    ddlTerr.SelectedValue = dsTerr.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                }
            }
        }
        else
        {
            btnSave.Visible = false;
            btnUpdate.Visible = false;
            grdChemists.DataSource = dsChemists;
            grdChemists.DataBind();
        }
    }

    protected DataSet FillTerritory()
    {
        ListedDR lstDR = new ListedDR();
        dsChemists = lstDR.FetchTerritory(sf_code);
        return dsChemists;
    }

    protected void btnOk_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        btnSave.Visible = true;
        tblDoctor.Visible = true;
        FillChemists();
        CblChemistsCode.Enabled = false;

        for (i = 2; i < ((grdChemists.Columns.Count)); i++)
        {
            grdChemists.Columns[i].Visible = false;
        }

        for (int j = 0; j < CblChemistsCode.Items.Count; j++)
        {
            for (i = 2; i < grdChemists.Columns.Count; i++)
            {
                if (CblChemistsCode.Items[j].Selected == true)
                {
                    if (grdChemists.Columns[i].HeaderText.Trim() == CblChemistsCode.Items[j].Text.Trim())
                    {
                        grdChemists.Columns[i].Visible = true;
                    }
                }              
            }
        }

        if (CblChemistsCode.Items[0].Selected == false)
        {
            grdChemists.Columns[2].Visible = true;
        }
        else
        {
            grdChemists.Columns[2].Visible = false;
        }

        //if (CblChemistsCode.Items[1].Selected == false)
        //{
        //    grdChemists.Columns[3].Visible = true;
        //}
        //else
        //{
        //    grdChemists.Columns[3].Visible = false;
        //}

    }


    protected void btnClr_Click(object sender, EventArgs e)
    {
        for (i = 0; i < CblChemistsCode.Items.Count; i++)
        {
            CblChemistsCode.Items[i].Enabled = true;
            CblChemistsCode.Items[i].Selected = false;
        }
        CblChemistsCode.Enabled = true;
        btnSave.Visible = false;
        btnUpdate.Visible = false;
        grdChemists.DataSource = null;
        grdChemists.DataBind();
        tblDoctor.Visible = false;
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        string cntrl = string.Empty;
        string prod_code = string.Empty;
        string strTextBox = string.Empty;

        foreach (GridViewRow gridRow in grdChemists.Rows)
        {
            for (i = 0; i < CblChemistsCode.Items.Count; i++)
            {
                if (CblChemistsCode.Items[i].Selected == true)
                {
                    cntrl = CblChemistsCode.Items[i].Value.ToString();

                    if (i != 7) 
                    {
                        TextBox sTextBox = (TextBox)gridRow.Cells[1].FindControl(cntrl);
                        string stxt = sTextBox.Text.ToString();
                        Label lblChemists_Code = (Label)gridRow.Cells[1].FindControl("Chemists_Code");
                        Chemists_Code = lblChemists_Code.Text.ToString();
                        strTextBox = strTextBox + CblChemistsCode.Items[i].Value + "= '" + stxt + "',";
                    }
                    else
                    {
                        DropDownList sDDL = (DropDownList)gridRow.Cells[1].FindControl(cntrl);
                        string stxt = sDDL.SelectedValue.ToString();
                        Label lblChemists_Code = (Label)gridRow.Cells[1].FindControl("Chemists_Code");
                        Chemists_Code = lblChemists_Code.Text.ToString();
                        strTextBox = strTextBox + CblChemistsCode.Items[i].Value + "= '" + stxt + "',";
                    }
                }
            }

            if (strTextBox.Trim().Length > 0)
            {
                //strTextBox = strTextBox.Substring(0, strTextBox.Length - 1);
                strTextBox = strTextBox + " LastUpdt_Date = getdate() ";
                Chemist chem = new Chemist();
                iReturn = chem.BulkEdit(strTextBox, Chemists_Code);
                strTextBox = "";
            }
        }

        if (iReturn > 0)
        {
            //menu1.Status = "Chemists detail(s) have been updated Successfully";
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Updated Successfully');</script>");

        }

    }


    protected void btnSave_Click(object sender, EventArgs e)
    {
        btnUpdate_Click(sender, e);
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        try
        {
            Server.Transfer("ChemistList.aspx");
        }
        catch (Exception ex)
        {

        }
    }
}