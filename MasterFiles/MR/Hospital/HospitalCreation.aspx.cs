using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;

public partial class MasterFiles_MR_Hospital_HospitalCreation : System.Web.UI.Page
{
    DataSet dsHospital = null;
    string sf_code = string.Empty;
    string Hospital_Name = string.Empty;
    string Hospital_Address1 = string.Empty;
    string Hospital_Contact = string.Empty;
    string Hospital_Phone = string.Empty;
    string Hospital_Terr = string.Empty;
    string div_code = string.Empty;
    int iCnt = -1;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int time;
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
            Session["backurl"] = "HospitalList.aspx";
            lblTerrritory.Text = "( " + "<span style='font-weight: bold;color:Maroon;'>For " + Session["sfName"] + " </span>" + " - " +
                               "<span style='font-weight: bold;color:Maroon;'> " + Session["sf_Designation_Short_Name"] + " </span>" + " - " +
                                "<span style='font-weight: bold;color:Maroon;'>  " + Session["sf_HQ"] + "</span>" + " )";
        }
        if (!Page.IsPostBack)
        {
            Session["backurl"] = "HospitalList.aspx";
            //menu1.Title = this.Page.Title;
            FillHospital();
            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);
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
    protected void grdHospital_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "this.className='highlight_clr'");
            e.Row.Attributes.Add("onmouseout", "this.className='normal'");
        }
    }
    private void FillHospital()
    {
        Hospital hosp = new Hospital();
        iCnt = hosp.RecordCount(sf_code);
        ViewState["iCnt"] = iCnt.ToString();

        dsHospital = hosp.getEmptyHospital();
        if (dsHospital.Tables[0].Rows.Count > 0)
        {
            grdHospital.Visible = true;
            grdHospital.DataSource = dsHospital;
            grdHospital.DataBind();
            grdHospital.Focus();
        }

    }

    protected void grdHospital_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblSNo = (Label)e.Row.FindControl("lblSNo");
            lblSNo.Text = Convert.ToString( Convert.ToInt32(lblSNo.Text) + Convert.ToInt32(ViewState["iCnt"].ToString()));
        }
        if (e.Row.RowType == DataControlRowType.Header)
        {
            Territory terr = new Territory();
            DataSet dsTerritory = new DataSet();
            dsTerritory = terr.getWorkAreaName(div_code);
            if (dsTerritory.Tables[0].Rows.Count > 0)
            {
                e.Row.Cells[5].Text = dsTerritory.Tables[0].Rows[0]["wrk_area_Name"].ToString();

            }
        }
    }

    protected DataSet FillTerritory()
    {
        ListedDR lstDR = new ListedDR();
        dsHospital = lstDR.FetchTerritory(sf_code);
        return dsHospital;
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        int iReturn = -1;
        foreach (GridViewRow gridRow in grdHospital.Rows)
        {
            TextBox txt_Chemist_Name = (TextBox)gridRow.Cells[1].FindControl("Hospital_Name");
            Hospital_Name = txt_Chemist_Name.Text.ToString();
            TextBox txt_chemist_address = (TextBox)gridRow.Cells[2].FindControl("Hospital_Address1");
            Hospital_Address1 = txt_chemist_address.Text.ToString();
            TextBox txt_Chemist_contact = (TextBox)gridRow.Cells[3].FindControl("Hospital_Contact");
            Hospital_Contact = txt_Chemist_contact.Text.ToString();
            TextBox txt_Chemists_Phone = (TextBox)gridRow.Cells[4].FindControl("Hospital_Phone");
            Hospital_Phone = txt_Chemists_Phone.Text.ToString();
            DropDownList ddl_Terr = (DropDownList)gridRow.Cells[5].FindControl("ddlTerr");
            Hospital_Terr = ddl_Terr.SelectedValue.ToString();            
            if ((Hospital_Name.Trim().Length > 0) && (Hospital_Address1.Trim().Length > 0) && (Hospital_Contact.Trim().Length > 0) && (Hospital_Phone.Trim().Length > 0) && (Hospital_Terr.Trim().Length > 0))
            {
                // Add New Listed Doctor
                Hospital hosp = new Hospital();
                iReturn = hosp.RecordAdd(Hospital_Name, Hospital_Address1, Hospital_Contact, Hospital_Phone, Hospital_Terr, Session["sf_code"].ToString());
            }
            else
            {
                //menu1.Status = "Enter all the values!!";
            }
        }

        if (iReturn > 0)
        {
            //menu1.Status = "Hospital Created Successfully!!";
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Created Successfully');</script>");
            FillHospital();
        }
        else if (iReturn == -2)
        {
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Hospital Name Already Exist');</script>");

        }

    }
    
    protected void btnClear_Click(object sender, EventArgs e)
    {
        FillHospital();

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