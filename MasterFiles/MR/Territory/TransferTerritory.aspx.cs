using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;

public partial class MasterFiles_MR_Territory_TransferTerritory : System.Web.UI.Page
{
    #region "Declaration"
    DataSet dsTerritory = null;
    DataSet dsTerritoryDDL = null;
    string sf_code = string.Empty;
    string Territory_Code = string.Empty;
    string Territory_Type = string.Empty;
    string Territory_Name = string.Empty;
    string Targer_Territory = string.Empty;
    string WorkArea = string.Empty;
    string div_code = string.Empty;
    int i;
    int iReturn = -1;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int time;
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        //WorkArea=Session["AreaWork"].ToString();
        div_code = Session["div_code"].ToString();
        if (!Page.IsPostBack)
        {
            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);     
           // GetWorkName();
            ViewTerritory();            
            Session["backurl"] = "Territory.aspx";
        }
        if (Session["sf_type"].ToString() == "1")
        {
            sf_code = Session["sf_code"].ToString();
            UserControl_MR_Menu Usc_MR =
       (UserControl_MR_Menu)LoadControl("~/UserControl/MR_Menu.ascx");
            Divid.Controls.Add(Usc_MR);
            Usc_MR.Title = this.Page.Title;
            //menu1.Visible = true;
            btnBack.Visible = false;
            lblTerrritory.Text = "( " + "<span style='font-weight: bold;color:Maroon;'>For " + Session["sfName"] + " </span>" + " - " +
                              "<span style='font-weight: bold;color:Maroon;'> " + Session["sf_Designation_Short_Name"] + " </span>" + " - " +
                               "<span style='font-weight: bold;color:Maroon;'>  " + Session["sf_HQ"] + "</span>" + " )";
            Territory terr = new Territory();
            dsTerritory = terr.getWorkAreaName(div_code);
            if (dsTerritory.Tables[0].Rows.Count > 0)
            {
                Usc_MR.Title = "Transfer" + " " + dsTerritory.Tables[0].Rows[0]["wrk_area_Name"];
            }
        }
        else
        {
            //menu1.Visible = false;
            UserControl_MenuUserControl c1 =
        (UserControl_MenuUserControl)LoadControl("~/UserControl/MenuUserControl.ascx");
            Session["backurl"] = "Territory.aspx";
            Divid.Controls.Add(c1);
            c1.Title = this.Page.Title;
            //menu1.FindControl("btnBack").Visible = false;
            btnBack.Visible = false;
            lblTerrritory.Text = "( " + "<span style='font-weight: bold;color:Maroon;'>For " + Session["sfName"] + " </span>" + " - " +
                            "<span style='font-weight: bold;color:Maroon;'> " + Session["sf_Designation_Short_Name"] + " </span>" + " - " +
                             "<span style='font-weight: bold;color:Maroon;'>  " + Session["sf_HQ"] + "</span>" + " )";
            Territory terr = new Territory();
            dsTerritory = terr.getWorkAreaName(div_code);
            if (dsTerritory.Tables[0].Rows.Count > 0)
            {
                c1.Title = "Transfer" + " " + dsTerritory.Tables[0].Rows[0]["wrk_area_Name"];
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
  
    protected void grdTerritory_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        int total = 0;
        int totalLC = 0;
        int totalCC = 0;
        int totalHC = 0;
        
            for (int i = 0; i < dsTerritory.Tables[0].Rows.Count; i++)
            {
                string lblqy = dsTerritory.Tables[0].Rows[i]["ListedDR_Count"].ToString();                
                int qty = Convert.ToInt16(lblqy);
                total = total + qty;

                string lblLC = dsTerritory.Tables[0].Rows[i]["Chemists_Count"].ToString();
                int qtyLC = Convert.ToInt16(lblLC);
                totalLC = totalLC + qtyLC;

                //string lblCC = dsTerritory.Tables[0].Rows[i]["UnListedDR_Count"].ToString();
                //int qtyCC = Convert.ToInt16(lblCC);
                //totalCC = totalCC + qtyCC;

                //string lblHC = dsTerritory.Tables[0].Rows[i]["Hospital_Count"].ToString();
                //int qtyHC = Convert.ToInt16(lblHC);
                //totalHC = totalHC + qtyHC;
            }           
                Label lblTerritory = (Label)e.Row.FindControl("lblTerritory_Code");
                DropDownList ddlTerritory = (DropDownList)e.Row.FindControl("ddlTerritory");
                if (lblTerritory != null)
                {
                    Territory terr = new Territory();
                    dsTerritoryDDL = terr.getTerritory(Session["sf_code"].ToString(), lblTerritory.Text.ToString());
                    if (dsTerritoryDDL.Tables[0].Rows.Count > 0)
                    {
                        ddlTerritory.DataTextField = "Territory_Name";
                        ddlTerritory.DataValueField = "Territory_Code";
                        ddlTerritory.DataSource = dsTerritoryDDL;
                        ddlTerritory.DataBind();
                    }
                }
          
        
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lblGroupTotal = e.Row.FindControl("lblTotalqty") as Label;
                lblGroupTotal.Text = total.ToString();

                Label lblChemistsCnt = e.Row.FindControl("lblTotalChemists_Count") as Label;
                lblChemistsCnt.Text = totalLC.ToString();

                //Label lblTotalUnListedDR_Count = e.Row.FindControl("lblTotalUnListedDR_Count") as Label;
                //lblTotalUnListedDR_Count.Text = totalCC.ToString();

                //Label lblTotalHospital_Count = e.Row.FindControl("lblTotalHospital_Count") as Label;
                //lblTotalHospital_Count.Text = totalHC.ToString();
            }  
        
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
            btnTransfer.Visible = false;            
        }
        
    }
   
    protected void btnTransfer_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        int iReturn = -1;
        foreach (GridViewRow gridRow in grdTerritory.Rows)
        {
            Label lblTerritory_Code = (Label)gridRow.Cells[1].FindControl("lblTerritory_Code");
            Territory_Code = lblTerritory_Code.Text.ToString();
            DropDownList ddlTerritory = (DropDownList)gridRow.Cells[5].FindControl("ddlTerritory");
            Targer_Territory = ddlTerritory.SelectedValue.ToString();                      
            CheckBox chkTerr = (CheckBox)gridRow.Cells[0].FindControl("chkTerritory");
            bool bCheck = chkTerr.Checked;


            if (Territory_Code.Trim().Length > 0 && Targer_Territory.Trim().Length > 0 && (bCheck == true))
            {
                // Transfer Territory
                Territory terr = new Territory();
                iReturn = terr.TransferTerritory(Territory_Code, Targer_Territory);
            }
            else
            {
                //menu1.Status = "Enter all the values!!";
                //ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Enter all the values');</script>");
            }
        }

        if (iReturn != -1)
        {
            //menu1.Status = "Territory has been transferred Successfully!!";
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Transferred Successfully');</script>");
            ViewTerritory();
        }

    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        Server.Transfer("Territory.aspx");
    }
}