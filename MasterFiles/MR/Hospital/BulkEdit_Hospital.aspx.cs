using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;

public partial class MasterFiles_MR_Hospital_BulkEdit_Hospital : System.Web.UI.Page
{
    #region "Declaration"
    DataSet dsHospital = null;
    DataSet dsTerritory = null;
    string Hospital_Code = string.Empty;
    string Hospital_Name = string.Empty;
    string Hospital_Address = string.Empty;
    string Hospital_Contact = string.Empty;
    string Hospital_Phone = string.Empty;
    string Hospital_Fax = string.Empty;
    string Hospital_EMail = string.Empty;
    string Hospital_Terr = string.Empty;
    string Hospital_Mobile = string.Empty;
    string Terr_Code = string.Empty;
    string sf_code = string.Empty;
    string div_code = string.Empty;
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
            //menu1.Visible = false;
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
            //menu1.Title = this.Page.Title;
            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);
            getWorkAreaName();
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
    private void getWorkAreaName()
    {
        Territory terr = new Territory();
        dsTerritory = terr.getWorkAreaName(div_code);
        if (dsTerritory.Tables[0].Rows.Count > 0)
        {
            grdHospital.Columns[10].HeaderText = dsTerritory.Tables[0].Rows[0]["wrk_area_Name"].ToString();
            CblHospitalCode.Items.Add(new ListItem(" " +dsTerritory.Tables[0].Rows[0]["wrk_area_Name"].ToString(), "Territory_Code", true));
        }

    }
    protected void grdHospital_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.Header)
        {
            Territory terr = new Territory();
            DataSet dsTerritory = new DataSet();
            dsTerritory = terr.getWorkAreaName(div_code);
            if (dsTerritory.Tables[0].Rows.Count > 0)
            {
                e.Row.Cells[10].Text = dsTerritory.Tables[0].Rows[0]["wrk_area_Name"].ToString();

            }
        }
    }
    private void FillChemists()
    {
        Hospital hosp = new Hospital();
        dsHospital = hosp.getHospitalDetails(sf_code);

        if (dsHospital.Tables[0].Rows.Count > 0)
        {
            grdHospital.Visible = true;
            grdHospital.DataSource = dsHospital;
            grdHospital.DataBind();

            foreach (GridViewRow gridRow in grdHospital.Rows)
            {
                DropDownList ddlTerr = (DropDownList)gridRow.Cells[9].FindControl("Territory_Code");

                Label lblHospital_Code = (Label)gridRow.Cells[1].FindControl("Hospital_Code");
                Hospital_Code = lblHospital_Code.Text.ToString();

                DataSet dsTerr = hosp.getTerritory_Hospital(Hospital_Code, sf_code);
                if (dsTerr.Tables[0].Rows.Count > 0)
                {
                    ddlTerr.SelectedValue = dsTerr.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                }
            }
        }
    }

    protected DataSet FillTerritory()
    {
        ListedDR lstDR = new ListedDR();
        dsHospital = lstDR.FetchTerritory(sf_code);
        return dsHospital;
    }

    protected void btnOk_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        btnSave.Visible = true;
        tblDoctor.Visible = true;
        CblHospitalCode.Enabled = false;
        FillChemists();     
        for (i = 2; i < ((grdHospital.Columns.Count)); i++)
        {
            grdHospital.Columns[i].Visible = false;
        }

        for (int j = 0; j < CblHospitalCode.Items.Count; j++)
        {
            for (i = 2; i < grdHospital.Columns.Count; i++)
            {
                if (CblHospitalCode.Items[j].Selected == true)
                {
                    if (grdHospital.Columns[i].HeaderText.Trim() == CblHospitalCode.Items[j].Text.Trim())
                    {
                        grdHospital.Columns[i].Visible = true;
                    }
                }               
            }
        }

        if (CblHospitalCode.Items[0].Selected == false)
        {
            grdHospital.Columns[2].Visible = true;
        }
        else
        {
            grdHospital.Columns[2].Visible = false;
        }
        

    }


    protected void btnClr_Click(object sender, EventArgs e)
    {
        for (i = 0; i < CblHospitalCode.Items.Count; i++)
        {
            CblHospitalCode.Items[i].Enabled = true;
            CblHospitalCode.Items[i].Selected = false;
        }
        CblHospitalCode.Enabled = true;
        btnSave.Visible = false;
        btnUpdate.Visible = false;
        grdHospital.DataSource = null;
        grdHospital.DataBind();
        tblDoctor.Visible = false;
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        string cntrl = string.Empty;
        string prod_code = string.Empty;
        string strTextBox = string.Empty;

        foreach (GridViewRow gridRow in grdHospital.Rows)
        {
            for (i = 0; i < CblHospitalCode.Items.Count; i++)
            {
                if (CblHospitalCode.Items[i].Selected == true)
                {
                    cntrl = CblHospitalCode.Items[i].Value.ToString();

                    if (i != 7)
                    {
                        TextBox sTextBox = (TextBox)gridRow.Cells[1].FindControl(cntrl);
                        string stxt = sTextBox.Text.ToString();
                        Label lblChemists_Code = (Label)gridRow.Cells[1].FindControl("Hospital_Code");
                        Hospital_Code = lblChemists_Code.Text.ToString();
                        strTextBox = strTextBox + CblHospitalCode.Items[i].Value + "= '" + stxt + "',";
                    }
                    else
                    {
                        DropDownList sDDL = (DropDownList)gridRow.Cells[1].FindControl(cntrl);
                        string stxt = sDDL.SelectedValue.ToString();
                        Label lblChemists_Code = (Label)gridRow.Cells[1].FindControl("Hospital_Code");
                        Hospital_Code = lblChemists_Code.Text.ToString();
                        strTextBox = strTextBox + CblHospitalCode.Items[i].Value + "= '" + stxt + "',";
                    }
                }
            }

            if (strTextBox.Trim().Length > 0)
            {
                //strTextBox = strTextBox.Substring(0, strTextBox.Length - 1);
                strTextBox = strTextBox + " LastUpdt_Date = getdate() ";
                Hospital chem = new Hospital();
                iReturn = chem.BulkEdit(strTextBox, Hospital_Code);
                strTextBox = "";
            }
        }

        if (iReturn > 0)
        {
            //menu1.Status = "Hospital detail(s) have been updated Successfully";
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
            Server.Transfer("HospitalList.aspx");
        }
        catch (Exception ex)
        {

        }
    }
}