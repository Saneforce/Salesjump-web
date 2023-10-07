using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;

public partial class MasterFiles_MR_Territory_BulkEdit : System.Web.UI.Page
{
    #region "Declaration"
    DataSet dsTerritory = null;
    string sf_code = string.Empty;
    string Territory_Code = string.Empty;
    string Territory_Type = string.Empty;
    string Territory_Name = string.Empty;
    string Territory_SName = string.Empty;
    string div_code = string.Empty;
    int i;
    int iReturn = -1;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int time;
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        if (!Page.IsPostBack)
        {
            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);
            //GetWorkName();
            ViewTerritory();           
            Session["backurl"] = "Territory.aspx";
            //ViewTerritory();
        }
        if (Session["sf_type"].ToString() == "1")
        {
            sf_code = Session["sf_code"].ToString();
            UserControl_MR_Menu Usc_MR =
       (UserControl_MR_Menu)LoadControl("~/UserControl/MR_Menu.ascx");
            Divid.Controls.Add(Usc_MR);
            Usc_MR.Title = this.Page.Title;
            btnBack.Visible = false;
            //menu1.Visible = true;
            //menu1.FindControl("btnBack").Visible = false; 
            lblTerrritory.Text = "( " + "<span style='font-weight: bold;color:Maroon;'>For " + Session["sfName"] + " </span>" + " - " +
                                "<span style='font-weight: bold;color:Maroon;'> " + Session["sf_Designation_Short_Name"] + " </span>" + " - " +
                                 "<span style='font-weight: bold;color:Maroon;'>  " + Session["sf_HQ"] + "</span>" + " )";
            Territory terr = new Territory();
            dsTerritory = terr.getWorkAreaName(div_code);
            if (dsTerritory.Tables[0].Rows.Count > 0)
            {
                Usc_MR.Title = "Edit all" + " - " + dsTerritory.Tables[0].Rows[0]["wrk_area_Name"];
            }

        }
        else
        {
            //menu1.Visible = false;
            UserControl_MenuUserControl c1 =
        (UserControl_MenuUserControl)LoadControl("~/UserControl/MenuUserControl.ascx");
            Divid.Controls.Add(c1);
            c1.Title = this.Page.Title;
            Session["backurl"] = "Territory.aspx";           
            lblTerrritory.Text = "( " + "<span style='font-weight: bold;color:Maroon;'>For " + Session["sfName"] + " </span>" + " - " +
                              "<span style='font-weight: bold;color:Maroon;'> " + Session["sf_Designation_Short_Name"] + " </span>" + " - " +
                               "<span style='font-weight: bold;color:Maroon;'>  " + Session["sf_HQ"] + "</span>" + " )";
            btnBack.Visible = false;
            Territory terr = new Territory();
            dsTerritory = terr.getWorkAreaName(div_code);
            if (dsTerritory.Tables[0].Rows.Count > 0)
            {
                c1.Title = "Edit all" + " - " + dsTerritory.Tables[0].Rows[0]["wrk_area_Name"];
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
  

    private void ViewTerritory()
    {
        Territory terr = new Territory();
        dsTerritory = terr.getTerritory(Session["sf_code"].ToString());
        if (dsTerritory.Tables[0].Rows.Count > 0)
        {
            grdTerritory.Visible = true;
            grdTerritory.DataSource = dsTerritory;
            grdTerritory.DataBind();
        }
        else
        {
            grdTerritory.DataSource = dsTerritory;
            grdTerritory.DataBind();
            btnSubmit.Visible = false;           
        }
    }

    protected void grdTerritory_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //Label lblSNo = (Label)e.Row.FindControl("lblSNo");
            //lblSNo.Text = Convert.ToString(Convert.ToInt32(lblSNo.Text) + Convert.ToInt32(ViewState["iCnt"].ToString()));


            // BIND THE "DROPDOWNLIST" WITH THE DATASET FILLED WITH "QUALIFICATION" DETAILS.
            DropDownList ddlQual = new DropDownList();
            ddlQual = (DropDownList)e.Row.FindControl("Territory_DisName");
             if (ddlQual != null)
            {
                string sfname = Session["sf_code"].ToString();
                Territory terr = new Territory();
                DataSet ds = terr.getTerritory_dm(sfname);
               
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlQual.DataSource = ds;
                        ddlQual.DataTextField = "Distributor_Name";
                        ddlQual.DataValueField = "Distributor_Code";
                        ddlQual.DataBind();
                    }
                }
               
            }
           
        }
    

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        //Doctor dv = new Doctor();
        System.Threading.Thread.Sleep(time);
        foreach (GridViewRow gridRow in grdTerritory.Rows)
        {
            Label lbl_Territory_Code = (Label)gridRow.Cells[1].FindControl("lblSNo");
            Territory_Code = lbl_Territory_Code.Text;
            Label txt_Territorys_Code = (Label)gridRow.Cells[2].FindControl("Territory_Code");
            string code = txt_Territorys_Code.Text;
            TextBox txt_Territory_Name = (TextBox)gridRow.Cells[3].FindControl("Territory_Name");
            Territory_Name = txt_Territory_Name.Text;
            DropDownList ddl_Territory_Type = (DropDownList)gridRow.Cells[4].FindControl("Territory_DisName");
            Territory_Type = ddl_Territory_Type.SelectedValue.ToString();
            TextBox txt_Territory_Sname = (TextBox)gridRow.Cells[5].FindControl("Territory_Target");
            Territory_SName = txt_Territory_Sname.Text;
            TextBox txt_Territory_min = (TextBox)gridRow.Cells[6].FindControl("Territory_MinProd");
            string min = txt_Territory_min.Text;

          
            // Update Territory
            Territory Terr = new Territory();
            int iReturn = Terr.RecordUpdate(Territory_Code, code, Territory_Name, Territory_Type, Territory_SName, min);
            if (iReturn > 0)
            {
                // menu1.Status = "Doctor Category(s) have been updated Successfully";
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Updated Successfully');</script>");
            }
            
        }

      
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        Server.Transfer("Territory.aspx");
    }
}