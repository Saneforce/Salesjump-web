using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
public partial class MasterFiles_Territory_SlNo_Gen : System.Web.UI.Page
{
    #region "Declaration"
    DataSet dsTerritory = null;
    string sf_code = string.Empty;
    string Territory_Type = string.Empty;
    string Territory_Name = string.Empty;
    string Territory_SName = string.Empty;
    string div_code = string.Empty;
    int i;
    int iReturn = -1;
    string txtSlNo = string.Empty;
    string Territory_Code = string.Empty;
    string lblTerritory_Code = string.Empty;
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
            
            //menu1.Title = this.Page.Title;
            Session["backurl"] = "Territory.aspx";
            ViewTerritory();
        }
        if (Session["sf_type"].ToString() == "1")
        {
            sf_code = Session["sf_code"].ToString();
            UserControl_MR_Menu Usc_MR =
      (UserControl_MR_Menu)LoadControl("~/UserControl/MR_Menu.ascx");
            Divid.Controls.Add(Usc_MR);
            Usc_MR.Title = this.Page.Title;          
            btnBack.Visible = false;
            lblTerrritory.Text = "( " + "<span style='font-weight: bold;color:Maroon;'>For " + Session["sfName"] + " </span>" + " - " +
                                "<span style='font-weight: bold;color:Maroon;'> " + Session["sf_Designation_Short_Name"] + " </span>" + " - " +
                                 "<span style='font-weight: bold;color:Maroon;'>  " + Session["sf_HQ"] + "</span>" + " )";
            Territory terr = new Territory();
            dsTerritory = terr.getWorkAreaName(div_code);
            if (dsTerritory.Tables[0].Rows.Count > 0)
            {
                Usc_MR.Title = dsTerritory.Tables[0].Rows[0]["wrk_area_Name"] + " " + "Creation";
            }
        }
        else
        {          
            UserControl_MenuUserControl c1 =
      (UserControl_MenuUserControl)LoadControl("~/UserControl/MenuUserControl.ascx");
            Session["backurl"] = "Territory.aspx";
            Divid.Controls.Add(c1);
            c1.Title = this.Page.Title;
            btnBack.Visible = false;
            lblTerrritory.Text = "( " + "<span style='font-weight: bold;color:Maroon;'>For " + Session["sfName"] + " </span>" + " - " +
                                  "<span style='font-weight: bold;color:Maroon;'> " + Session["sf_Designation_Short_Name"] + " </span>" + " - " +
                                   "<span style='font-weight: bold;color:Maroon;'>  " + Session["sf_HQ"] + "</span>" + " )";
            Territory terr = new Territory();
            dsTerritory = terr.getWorkAreaName(div_code);
            if (dsTerritory.Tables[0].Rows.Count > 0)
            {
                c1.Title = dsTerritory.Tables[0].Rows[0]["wrk_area_Name"] + " " + "Serial No Generation";
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
        dsTerritory = terr.getTerritory_Slno(Session["sf_code"].ToString());
        if (dsTerritory.Tables[0].Rows.Count > 0)
        {
            grdTerr.Visible = true;
            grdTerr.DataSource = dsTerritory;
            grdTerr.DataBind();
        }
        else
        {
            grdTerr.DataSource = dsTerritory;
            grdTerr.DataBind();
            btnSubmit.Visible = false;
            btnClear.Visible = false;
        }
    }
   
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        bool isError = false;
        System.Threading.Thread.Sleep(time);
        if (btnSubmit.Text == "Generate - Sl No")
        {
            int i = 1;
            int iVal = 1;
            string sVal = string.Empty;
            sVal = ",";

            foreach (GridViewRow gridRow in grdTerr.Rows)
            {
                TextBox txtSNo = (TextBox)gridRow.Cells[1].FindControl("txtSlNo");
                if (txtSNo.Text.Length > 0)
                {
                    if (grdTerr.Rows.Count >= Convert.ToInt16(txtSNo.Text.Trim()))
                    {
                        sVal = sVal + txtSNo.Text + ',';
                    }
                    else
                    {
                        isError = true;
                        break;
                    }
                }
                i++;
            }
            if (isError == false)
            {

                if (sVal == "")
                {
                    foreach (GridViewRow gridRow in grdTerr.Rows)
                    {
                        Label lblSNo = (Label)gridRow.Cells[1].FindControl("lblSNo");
                        TextBox txtSNo = (TextBox)gridRow.Cells[1].FindControl("txtSlNo");
                        txtSNo.Text = lblSNo.Text;
                    }
                }
                else
                {
                    iVal = 1;
                    System.Threading.Thread.Sleep(time);
                    foreach (GridViewRow gridRow in grdTerr.Rows)
                    {
                        TextBox txtSNo = (TextBox)gridRow.Cells[1].FindControl("txtSlNo");
                        if (txtSNo.Text.Length <= 0)
                        {
                            for (iVal = 1; iVal <= i; iVal++)
                            {
                                string schk = ',' + iVal.ToString() + ',';
                                if (sVal.IndexOf(schk) != -1)
                                {
                                    //Do Nothing
                                }
                                else
                                {
                                    sVal = sVal + iVal.ToString() + ',';
                                    break;
                                }
                            }

                            txtSNo.Text = iVal.ToString();
                        }

                    }
                }
                btnSubmit.Focus();
                btnSubmit.Text = "Save";
            }
            else if (isError == true)
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Enter Valid Number');</script>");
            }
        }
        else
        {
            System.Threading.Thread.Sleep(time);
            // Save
            foreach (GridViewRow gridRow in grdTerr.Rows)
            {
                TextBox txtSNo = (TextBox)gridRow.Cells[1].FindControl("txtSlNo");
                txtSlNo = txtSNo.Text;
                Label Territory_Code = (Label)gridRow.Cells[0].FindControl("lblTerritory_Code");
                lblTerritory_Code = Territory_Code.Text;

                //Doc_Cat_Code  = gridRow.Cells[0].Text;

                // Update Division
                Territory Terr = new Territory();
                int iReturn = Terr.Terr_SlNO(lblTerritory_Code, txtSlNo);
                if (iReturn > 0)
                {
                    // menu1.Status = "Sl No Updated Successfully ";
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Sl No Updated Successfully');</script>");
                }
                else if (iReturn == -2)
                {
                    //menu1.Status = "SlNo could not be updated!!";
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('SlNo could not be updated');</script>");
                }
            }

        }
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow gridRow in grdTerr.Rows)
        {
            TextBox txtSNo = (TextBox)gridRow.Cells[1].FindControl("txtSlNo");
            txtSNo.Text = "";
        }
    }
    // Sorting 
    public SortDirection dir
    {
        get
        {
            if (ViewState["dirState"] == null)
            {
                ViewState["dirState"] = SortDirection.Ascending;
            }
            return (SortDirection)ViewState["dirState"];
        }
        set
        {
            ViewState["dirState"] = value;
        }
    }
    private DataTable BindGridView()
    {
        DataTable dtGrid = new DataTable();
        Territory terr = new Territory();
        dtGrid = terr.getTerr_DtTable(Session["sf_code"].ToString());
        return dtGrid;
    }

    protected void grdTerr_Sorting(object sender, GridViewSortEventArgs e)
    {
        string sortingDirection = string.Empty;
        if (dir == SortDirection.Ascending)
        {
            dir = SortDirection.Descending;
            sortingDirection = "Desc";
        }
        else
        {
            dir = SortDirection.Ascending;
            sortingDirection = "Asc";
        }
        DataView sortedView = new DataView(BindGridView());
        sortedView.Sort = e.SortExpression + " " + sortingDirection;
        grdTerr.DataSource = sortedView;
        grdTerr.DataBind();
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        Server.Transfer("Territory.aspx");
    }
}