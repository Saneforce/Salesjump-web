using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;

public partial class MasterFiles_TerritoryCreation : System.Web.UI.Page
{

    #region "Declaration"
    DataSet dsTerritory = null;
    string sf_code = string.Empty;
    string sf_type = string.Empty;
    string Territory_Type = string.Empty;
    string Territory_Name = string.Empty;
    string Territory_Code = string.Empty;
    string Territory_SName = string.Empty;
    string Target = string.Empty;
    string min_prod = string.Empty;
    string div_code = string.Empty;
    string dsm_code = string.Empty;
    string Route_Population = string.Empty;
    int i;
    int iReturn = -1;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int time;
    int iCnt = -1;
    #endregion
    protected override void OnPreInit(EventArgs e)
    {
        base.OnPreInit(e);
        sf_type = Session["sf_type"].ToString();
        if (sf_type == "3")
        {
            this.MasterPageFile = "~/Master.master";
        }
        else if (sf_type == "2")
        {
            this.MasterPageFile = "~/Master_MGR.master";
        }
        else if (sf_type == "1")
        {
            this.MasterPageFile = "~/Master_MR.master";
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {

        div_code = Session["div_code"].ToString();
        if (Session["sf_type"].ToString() == "1")
        {
            sf_code = Session["sf_code"].ToString();
           // UserControl_MR_Menu Usc_MR =
           //(UserControl_MR_Menu)LoadControl("~/UserControl/MR_Menu.ascx");
           // Divid.Controls.Add(Usc_MR);
           // Usc_MR.Title = this.Page.Title;
            lblTerrritory.Text = "( " + "<span style='font-weight: bold;color:Maroon;'>DSM Name: " + Session["sfName"] + " </span>" + " )";
       
            Territory terr = new Territory();
            dsTerritory = terr.getWorkAreaName(div_code);
            if (dsTerritory.Tables[0].Rows.Count > 0)
            {
                //Usc_MR.Title = dsTerritory.Tables[0].Rows[0]["wrk_area_Name"] + " " + "Creation";
            }

        }
        else
        {
            sf_code = Session["sf_code"].ToString();
            //UserControl_MenuUserControl c1 =
            // (UserControl_MenuUserControl)LoadControl("~/UserControl/MenuUserControl.ascx");
            //Divid.Controls.Add(c1);            
            //c1.Title = this.Page.Title;          
         
            Session["backurl"] = "Territory.aspx";
            lblTerrritory.Text = "( " + "<span style='font-weight: bold;color:Maroon;'>DSM Name: " + Session["sfName"] + " </span>" + " )";
            Territory terr = new Territory();
            dsTerritory = terr.getWorkAreaName(div_code);
            if (dsTerritory.Tables[0].Rows.Count > 0)
            {
                //c1.Title = dsTerritory.Tables[0].Rows[0]["wrk_area_Name"] + " " + "Creation";
            }
        }


       
        if (!Page.IsPostBack)
        {
            Session["backurl"] = "Territory.aspx";
            GetWorkName();
            FillTerritory();
            ViewTerritory();

            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);
            //grdTerr.UseAccessibleHeader = true;
            //grdTerr.HeaderRow.TableSection = TableRowSection.TableHeader;       
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

    private void FillTerritory()
    {
        Territory terr = new Territory();
        iCnt = terr.RecordCountdiv(sf_code,div_code);
        ViewState["iCnt"] = iCnt.ToString();
        dsTerritory = terr.getEmptyTerritory();
        if (dsTerritory.Tables[0].Rows.Count > 0)
        {
            grdTerritory.Visible = true;
            grdTerritory.DataSource = dsTerritory;
            grdTerritory.DataBind();
        }
    }

    private void ViewTerritory()
    {
        Territory terr = new Territory();
        dsTerritory = terr.getTerritorydiv(Session["sf_code"].ToString(),div_code);
        if (dsTerritory.Tables[0].Rows.Count > 0)
        {
            grdTerr.Visible = true;         
            grdTerr.DataSource = dsTerritory;
            grdTerr.DataBind();
          

        }
    }
    protected void getTerritory_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblSNo = (Label)e.Row.FindControl("lblSNo");
            lblSNo.Text = Convert.ToString(Convert.ToInt32(lblSNo.Text) + Convert.ToInt32(ViewState["iCnt"].ToString()));
            DropDownList ddlQual = new DropDownList();
            ddlQual = (DropDownList)e.Row.FindControl("Territory_DistName");

            if (ddlQual != null)
            {
                string sfname = Session["sf_code"].ToString();
                Territory terr = new Territory();
                DataSet ds = terr.getTerritory_dm(sfname);
               
                    if (ds.Tables[0].Rows.Count > 0)
                    {

                        ddlQual.DataSource = ds;
                        ddlQual.DataTextField = "Stockist_Name";
                        ddlQual.DataValueField = "Stockist_Code";
                        ddlQual.DataBind();
                        Lab_DSM.Text = "( " + "<span style='font-weight: bold;color:Maroon;'>Distributor Name: " + ddlQual.SelectedItem.ToString() + " </span>" + " )";
                        //Territory_Type = ddlQual.SelectedValue;
                    }
                }
            DropDownList ddlQual1 = new DropDownList();
            ddlQual1 = (DropDownList)e.Row.FindControl("Territory_DisName");
            Territory terr1 = new Territory();
            dsTerritory = terr1.getSF_Code(sf_code);
            if (dsTerritory.Tables[0].Rows.Count > 0)
            {
                ddlQual1.DataTextField = "DSM_name";
                ddlQual1.DataValueField = "DSM_code";
                ddlQual1.DataSource = dsTerritory;
                ddlQual1.DataBind();
                //ddlQual1.Items.Insert(1, new ListItem("---select---", ""));

                //Set the default item as selected.
                ddlQual1.Items[0].Selected = true;

                //Disable the default item.
                ddlQual1.Items[0].Attributes["disabled"] = "disabled";
                //dsm_code = ddlQual1.SelectedValue;
            }
               
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
        dtGrid = terr.getTerritory_DataTable(sf_code);
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
   
    protected void btnSave_Click(object sender, EventArgs e)
    {
         System.Threading.Thread.Sleep(time);
        int iReturn = -1;
        foreach (GridViewRow gridRow in grdTerritory.Rows)
        {

            TextBox Territorys_Code = (TextBox)gridRow.Cells[1].FindControl("Territory_Code");
            Territory_Code = Territorys_Code.Text.ToString(); 
            TextBox Territorys_Name = (TextBox)gridRow.Cells[1].FindControl("Territory_Name");
            Territory_Name = Territorys_Name.Text.ToString();
            DropDownList Territory_DistName = (DropDownList)gridRow.Cells[1].FindControl("Territory_DistName");
            Territory_Type = Territory_DistName.SelectedValue.ToString();
            DropDownList Territory_DisName = (DropDownList)gridRow.Cells[1].FindControl("Territory_DisName");
            dsm_code = Territory_DisName.SelectedValue.ToString();
            TextBox Territorys_Target = (TextBox)gridRow.Cells[1].FindControl("Territory_Target");
            Target = Territorys_Target.Text.ToString();
            TextBox Territory_MinProd = (TextBox)gridRow.Cells[1].FindControl("Territory_MinProd");
            min_prod = Territory_MinProd.Text.ToString();
            TextBox Territory_Cenus = (TextBox)gridRow.Cells[1].FindControl("Territory_Cenus");
            Route_Population = Territory_Cenus.Text.ToString();
           
             if ((Territory_Name.Trim().Length > 0))
            {
                 Territory terr = new Territory();

                 iReturn = terr.RecordAdd(Territory_Code, Territory_Name, Territory_Type, dsm_code, Target, min_prod, div_code, Route_Population);
            }
            else
            {
                //menu1.Status = "Enter all the values!!";
            }
        }
              
                if (iReturn > 0)
                {                  
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Created Successfully');</script>");
                    FillTerritory();
                    ViewTerritory();
                }
                else if (iReturn == -2)
                {                    
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Name Already Exist');</script>");                   
                }                          
        
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        FillTerritory();
    }
    private void GetWorkName()
    {
        //UserControl_MenuUserControl c1 =
        //    (UserControl_MenuUserControl)LoadControl("~/UserControl/MenuUserControl.ascx");
        Territory terr = new Territory();
        dsTerritory = terr.getWorkAreaName(div_code);
        if (dsTerritory.Tables[0].Rows.Count > 0)
        {
            //c1.Title = dsTerritory.Tables[0].Rows[0]["wrk_area_Name"] + " " + "Creation";
        }
    }
    private void ViewDSM()
    {
       
    }

   
}