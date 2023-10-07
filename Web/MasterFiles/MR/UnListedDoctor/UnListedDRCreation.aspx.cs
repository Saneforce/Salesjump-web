using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;

public partial class MasterFiles_MR_UnListedDoctor_UnListedDRCreation : System.Web.UI.Page
{
    #region "Declaration"
    DataSet dsListedDR = null;
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
    string Doc_QuaCode = string.Empty; 
    string Terr_Code = string.Empty;
    string sf_code = string.Empty;
    string div_code = string.Empty;
    int i;
    int iReturn = -1;
    int iCnt = -1;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int time;
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
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
            UserControl_MenuUserControl Admin =
            (UserControl_MenuUserControl)LoadControl("~/UserControl/MenuUserControl.ascx");
            Divid.Controls.Add(Admin);           
            Admin.Title = this.Page.Title;
            btnBack.Visible = false;
            Session["backurl"] = "UnLstDoctorList.aspx";
            lblTerrritory.Text = "( " + "<span style='font-weight: bold;color:Maroon;'>For " + Session["sfName"] + " </span>" + " - " +
                              "<span style='font-weight: bold;color:Maroon;'> " + Session["sf_Designation_Short_Name"] + " </span>" + " - " +
                               "<span style='font-weight: bold;color:Maroon;'>  " + Session["sf_HQ"] + "</span>" + " )";
        }
      
        if (!Page.IsPostBack)
        {
            Session["backurl"] = "UnLstDoctorList.aspx";
            //menu1.Title = this.Page.Title;
            FillListedDR();
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
    private void FillListedDR()
    {
        UnListedDR lstDR = new UnListedDR();
        iCnt = lstDR.RecordCount(sf_code);
        ViewState["iCnt"] = iCnt.ToString();
        dsListedDR = lstDR.getEmptyListedDR();
        if (dsListedDR.Tables[0].Rows.Count > 0)
        {
            grdListedDR.Visible = true;
            grdListedDR.DataSource = dsListedDR;
            grdListedDR.DataBind();
        }
    }
    protected void grdListedDR_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "this.className='highlight_clr'");
            e.Row.Attributes.Add("onmouseout", "this.className='normal'");
        }
    }
    protected void grdListedDR_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblSNo = (Label)e.Row.FindControl("lblSNo");
            lblSNo.Text = Convert.ToString(Convert.ToInt32(lblSNo.Text) + Convert.ToInt32(ViewState["iCnt"].ToString()));
        }
        if (e.Row.RowType == DataControlRowType.Header)
        {
            Territory terr = new Territory();
            DataSet dsTerritory = new DataSet();
            dsTerritory = terr.getWorkAreaName(div_code);
            if (dsTerritory.Tables[0].Rows.Count > 0)
            {
                  e.Row.Cells[8].Text = dsTerritory.Tables[0].Rows[0]["wrk_area_Name"].ToString();
             
            }
        }
    }
    protected DataSet FillTerritory()
    {
        UnListedDR lstDR = new UnListedDR();
        dsListedDR = lstDR.FetchTerritory(sf_code);
        if (dsListedDR.Tables[0].Rows.Count <= 1)
        {
            Response.Redirect("../Territory/TerritoryCreation.aspx");
           // menu1.Status = "Territory must be created prior to Doctor creation";
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Territory must be created prior to Doctor creation');</script>");
        }

        return dsListedDR;
    }

    protected DataSet FillCategory()
    {
        UnListedDR lstDR = new UnListedDR();
        dsListedDR = lstDR.FetchCategory(sf_code);
        return dsListedDR;
    }

    protected DataSet FillSpeciality()
    {
        UnListedDR lstDR = new UnListedDR();
        dsListedDR = lstDR.FetchSpeciality(sf_code);
        return dsListedDR;
    }

    protected DataSet FillClass()
    {
        UnListedDR lstDR = new UnListedDR();
        dsListedDR = lstDR.FetchClass(sf_code);
        return dsListedDR;
    }

    protected DataSet FillQualification()
    {
        UnListedDR lstDR = new UnListedDR();
        dsListedDR = lstDR.FetchQualification(sf_code);
        return dsListedDR;
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        int iReturn = -1;
        foreach (GridViewRow gridRow in grdListedDR.Rows)
        {
            TextBox txt_ListedDR_Name = (TextBox)gridRow.Cells[1].FindControl("ListedDR_Name");
            Listed_DR_Name = txt_ListedDR_Name.Text.ToString();
            TextBox txt_ListedDR_Address1 = (TextBox)gridRow.Cells[2].FindControl("ListedDR_Address1");
            Listed_DR_Address = txt_ListedDR_Address1.Text.ToString();
            DropDownList ddl_Catg = (DropDownList)gridRow.Cells[3].FindControl("ddlCatg");
            Listed_DR_Catg = ddl_Catg.SelectedValue.ToString();
            DropDownList ddl_Spec = (DropDownList)gridRow.Cells[4].FindControl("ddlspcl");
            Listed_DR_Spec = ddl_Spec.SelectedValue.ToString();
            DropDownList ddl_Qual = (DropDownList)gridRow.Cells[5].FindControl("ddlQual");
            Listed_DR_Qual = ddl_Qual.SelectedValue.ToString();
            DropDownList ddl_Class = (DropDownList)gridRow.Cells[6].FindControl("ddlClass");
            Listed_DR_Class = ddl_Class.SelectedValue.ToString();
            DropDownList ddl_Terr = (DropDownList)gridRow.Cells[7].FindControl("ddlTerr");
            Listed_DR_Terr = ddl_Terr.SelectedValue.ToString();

            if ((Listed_DR_Name.Trim().Length > 0) && (Listed_DR_Address.Trim().Length > 0) && (Listed_DR_Catg.Trim().Length > 0) && (Listed_DR_Spec.Trim().Length > 0) && (Listed_DR_Qual.Trim().Length > 0)  && (Listed_DR_Class.Trim().Length > 0) && (Listed_DR_Terr.Trim().Length > 0))
            {
                // Add New Listed Doctor
                UnListedDR unlstDR = new UnListedDR();
                iReturn = unlstDR.RecordAdd(Listed_DR_Name, Listed_DR_Address, Listed_DR_Catg, Listed_DR_Spec, Listed_DR_Qual, Listed_DR_Class, Listed_DR_Terr, Session["sf_code"].ToString());
            }
            else
            {
                
                //menu1.Status = "Enter all the values!!";
               // ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Enter all the values');</script>");
              
            }
        }

        if (iReturn > 0)
        {
            //menu1.Status = "UnListed Doctor Created Successfully!!";
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Created Successfully');</script>");
            FillListedDR();
        }
        else if (iReturn == -2)
        {
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Doctor Name Already Exist');</script>");

        }
         
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        FillListedDR();
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        try
        {
            Server.Transfer("UnLstDoctorList.aspx");
        }
        catch (Exception ex)
        {

        }
    }

}