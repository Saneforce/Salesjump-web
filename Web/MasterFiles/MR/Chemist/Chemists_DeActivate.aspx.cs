using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;

public partial class MasterFiles_MR_Chemist_Chemists_DeActivate : System.Web.UI.Page
{
    #region "Declaration"
    DataSet dsChemists = null;
    DataSet dsChemist = null;
    string div_code = string.Empty;
    string sfCode = string.Empty;
    int search = 0;
    string sCmd = string.Empty;
    DataSet dsTerritory = null;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int time;
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        //sfCode = Session["sf_code"].ToString();
        if (Session["sf_type"].ToString() == "1")
        {
            sfCode = Session["sf_code"].ToString();
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
            sfCode = Session["sf_code"].ToString();
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
            FillChemists();
            getWorkName();
            //menu1.Title = this.Page.Title;
            Session["backurl"] = "ChemistList.aspx";
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
    protected void grdChemist_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            Territory terr = new Territory();
            DataSet dsTerritory = new DataSet();
            dsTerritory = terr.getWorkAreaName(div_code);
            if (dsTerritory.Tables[0].Rows.Count > 0)
            {
                //   e.Row .Cells [4].Text =dsTerritory .Tables [0].Rows [0]["wrk_area_Name"].ToString ();
                LinkButton LnkHeaderText = e.Row.Cells[5].Controls[0] as LinkButton;
                LnkHeaderText.Text = dsTerritory.Tables[0].Rows[0]["wrk_area_Name"].ToString();
            }

        }

    }
    private void FillChemists()
    {
        Chemist chem = new Chemist();
        dsChemists = chem.getChemists(sfCode);
        if (dsChemists.Tables[0].Rows.Count > 0)
        {
            btnSave.Visible = true;
            grdChemists.Visible = true;
            grdChemists.DataSource = dsChemists;
            grdChemists.DataBind();
        }
        else
        {         
            grdChemists.DataSource = dsChemists;
            grdChemists.DataBind();
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
        Chemist chem = new Chemist();
        sCmd = Session["GetCmdArgChar"].ToString();
        search = Convert.ToInt32(ddSrch.SelectedValue);
        if (sCmd == "All")
        {
            dtGrid = chem.getChemistslist_DataTable(sfCode);
        }
        else if (sCmd != "")
        {
            dtGrid = chem.getChemistsAlpha_DataTable(sfCode, sCmd);
        }
        else if (txtsearch.Text != "")
        {
            dtGrid = chem.getDTChemist_Nam(sfCode, txtsearch.Text);
        }
        else if (ddSrch.SelectedIndex != -1)
        {
            if (search == 1)
            {
                dtGrid = chem.getChemistslist_DataTable(sfCode);
            }
            else if (search == 3)
            {
                dtGrid = chem.getDTTerr(sfCode, ddSrc2.SelectedValue);
            }

        }
        return dtGrid;   
    }
    protected void grdChemists_Sorting(object sender, GridViewSortEventArgs e)
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
        grdChemists.DataSource = sortedView;
        grdChemists.DataBind();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        int iReturn = -1;
        foreach (GridViewRow gridRow in grdChemists.Rows)
        {
            CheckBox chkDR = (CheckBox)gridRow.Cells[0].FindControl("chkChemists");
            bool bCheck = chkDR.Checked;
            Label lblChemists_code = (Label)gridRow.Cells[2].FindControl("lblChemCode");
            string Chemists_Code = lblChemists_code.Text.ToString();

            if ((Chemists_Code.Trim().Length > 0) && (bCheck == true))
            {
                // De-Activate Chemists                
                Chemist chem = new Chemist();
                iReturn = chem.DeActivate(Chemists_Code);
            }
            else
            {
                //menu1.Status = "Enter all the values!!";
            }
        }

        if (iReturn != -1)
        {
          //  menu1.Status = "Chemists De-Activated Successfully!!";
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('De-Activated Successfully');</script>");
            FillChemists();
        }

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

    protected void ddSrch_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        search = Convert.ToInt32(ddSrch.SelectedValue);
        txtsearch.Text = string.Empty;
        grdChemists.PageIndex = 0;

        if (search == 2)
        {
            txtsearch.Visible = true;
            Btnsrc.Visible = true;
            ddSrc2.Visible = false;
        }
        else
        {
            txtsearch.Visible = false;
            ddSrc2.Visible = true;
            Btnsrc.Visible = true;
        }
        if (search == 1)
        {
            txtsearch.Visible = false;
            ddSrc2.Visible = false;
            Btnsrc.Visible = false;
            FillChemists();

        }

        if (search == 3)
        {
            FillTerr();
        }


    }

    private void FillTerr()
    {
        Chemist ch = new Chemist();
        dsChemist = ch.getTerritory(sfCode);
        if (dsChemist.Tables[0].Rows.Count > 0)
        {
            ddSrc2.DataTextField = "Territory_Name";
            ddSrc2.DataValueField = "Territory_Code";
            ddSrc2.DataSource = dsChemist;
            ddSrc2.DataBind();

        }

    }
    private void Search()
    {
        Chemist chlis = new Chemist();
        search = Convert.ToInt32(ddSrch.SelectedValue);
        if (search == 1)
        {

            FillChemists();
        }
        if (search == 2)
        {

            dsChemist = chlis.getListedChemforName(sfCode, txtsearch.Text);
            if (dsChemist.Tables[0].Rows.Count > 0)
            {

                grdChemists.Visible = true;
                grdChemists.DataSource = dsChemist;
                grdChemists.DataBind();
            }
            else
            {
                grdChemists.DataSource = dsChemist;
                grdChemists.DataBind();

            }

        }
        if (search == 3)
        {

            dsChemist = chlis.getListedChemforTerr(sfCode, ddSrc2.SelectedValue);
            if (dsChemist.Tables[0].Rows.Count > 0)
            {

                grdChemists.Visible = true;
                grdChemists.DataSource = dsChemist;
                grdChemists.DataBind();
            }
            else
            {
                grdChemists.DataSource = dsChemist;
                grdChemists.DataBind();

            }
        }

    }

    protected void Btnsrc_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        Session["GetcmdArgChar"] = string.Empty;
        grdChemists.PageIndex = 0;
        Search();
    }
    protected void ddSrc2_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    private void getWorkName()
    {
        Territory terr = new Territory();
        dsTerritory = terr.getWorkAreaName(div_code);
        if (dsTerritory.Tables[0].Rows.Count > 0)
        {
            string str = dsTerritory.Tables[0].Rows[0]["wrk_area_Name"].ToString();
            ddSrch.Items.Add(new ListItem(str, "3"));
        }
    }

}