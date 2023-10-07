using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
using System.Drawing.Imaging;
public partial class MasterFiles_Options_SetupScreen : System.Web.UI.Page
{

    DataSet dsSalesForce = null;
    string div_code = string.Empty;
    DataSet dsDivision = null;
    DataSet dsState = null;
    string sState = string.Empty;

    string[] statecd;
    string state_cd = string.Empty;

    int iDoctorAdd = 1;
    int iDoctorEdit = 1;
    int iDoctorDeAct = 1;
    int iDoctorView = 1;

    int iDoctorName = 1;

    int iNewDoctorAdd = 1;
    int iNewDoctorEdit = 1;
    int iNewDoctorDeAct = 1;
    int iNewDoctorView = 1;

    int iChemAdd = 1;
    int iChemEdit = 1;
    int iChemDeAct = 1;
    int iChemView = 1;

    int iTerrAdd = 1;
    int iTerrEdit = 1;    
    int iTerrDeAct = 1;
    int iTerrView = 1;

    int iClassAdd = 1;
    int iClassEdit = 1;
    int iClassDeAct = 1;
    int iClassView = 1;

    int iDoctorReAct = 1;
    int iNewDoctorReAct = 1;
    int iChemReAct = 1;
    int iClassReAct = 1;

    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        btnGo.Focus();
        if (!Page.IsPostBack)
        {
            FillManagers();
            FillColor();

            menu1.Title = this.Page.Title;
            menu1.FindControl("btnBack").Visible = false;
        }
        FillColor();
    }

    private void FillColor()
    {
        int j = 0;


        foreach (ListItem ColorItems in ddlSF.Items)
        {
            //ddlFieldForce.Items[j].Selected = true;
            string bcolor = "#" + ColorItems.Text;
            ddlFieldForce.Items[j].Attributes.Add("style", "background-color:" + bcolor);
         
            j = j + 1;

        }
    }

    protected void ddlAlpha_SelectedIndexChanged(object sender, EventArgs e)
    {
        SalesForce sf = new SalesForce();
        dsSalesForce = sf.UserListTP_Alphasearch(div_code, "admin", ddlAlpha.SelectedValue);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            ddlFieldForce.DataTextField = "sf_name";
            ddlFieldForce.DataValueField = "sf_code";
            ddlFieldForce.DataSource = dsSalesForce;
            ddlFieldForce.DataBind();

            ddlSF.DataTextField = "des_color";
            ddlSF.DataValueField = "sf_code";
            ddlSF.DataSource = dsSalesForce;
            ddlSF.DataBind();

        }

    }

    private void FillManagers()
    {
        SalesForce sf = new SalesForce();

        if (ddlFFType.SelectedValue.ToString() == "1")
        {
            ddlAlpha.Visible = false;
            dsSalesForce = sf.UserListTP_Hierarchy(div_code, "admin");
        }
        else if (ddlFFType.SelectedValue.ToString() == "0")
        {
            FillSF_Alpha();
            ddlAlpha.Visible = true;
            dsSalesForce = sf.UserListTP_Alpha(div_code, "admin");
        }
        else if (ddlFFType.SelectedValue.ToString() == "2")
        {
            dsSalesForce = sf.UserList_HQ(div_code, "admin");
        }

        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            ddlFieldForce.DataTextField = "sf_name";
            ddlFieldForce.DataValueField = "sf_code";
            ddlFieldForce.DataSource = dsSalesForce;
            ddlFieldForce.DataBind();

            ddlSF.DataTextField = "des_color";
            ddlSF.DataValueField = "sf_code";
            ddlSF.DataSource = dsSalesForce;
            ddlSF.DataBind();

        }
    }

    private void FillSF_Alpha()
    {
        SalesForce sf = new SalesForce();
        dsSalesForce = sf.getSalesForcelist_Alphabet(div_code);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            ddlAlpha.DataTextField = "sf_name";
            ddlAlpha.DataValueField = "val";
            ddlAlpha.DataSource = dsSalesForce;
            ddlAlpha.DataBind();
            ddlAlpha.SelectedIndex = 0;
        }
    }

    private void FillSalesForce()
    {
        AdminSetup adm = new AdminSetup();
        dsSalesForce = adm.getMR_MGR_New(ddlFieldForce.SelectedValue.ToString(), div_code);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            btnSubmit.Visible = true;
            btnClear.Visible = true;
            grdSalesForce.Visible = true;
            grdSalesForce.DataSource = dsSalesForce;
            grdSalesForce.DataBind();
        }
        else
        {
            btnSubmit.Visible = false;
            btnClear.Visible = false;
            grdSalesForce.Visible = true;
            grdSalesForce.DataSource = null;
            grdSalesForce.DataBind();
         

        }
    }

    protected void ddlFFType_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillManagers();
        FillColor();
    }

    protected void grdSalesForce_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblsf_code = (Label)e.Row.FindControl("lblsf_code");
            CheckBox chkDoctorAdd = (CheckBox)e.Row.FindControl("chkDoctorAdd");
            CheckBox chkDoctorEdit = (CheckBox)e.Row.FindControl("chkDoctorEdit");            
            CheckBox chkDoctorDeAct = (CheckBox)e.Row.FindControl("chkDoctorDeAct");
            CheckBox chkDoctorView = (CheckBox)e.Row.FindControl("chkDoctorView");
            CheckBox chkDoctorName = (CheckBox)e.Row.FindControl("chkDoctorName");

            CheckBox chkNewDoctorAdd = (CheckBox)e.Row.FindControl("chkNewDoctorAdd");
            CheckBox chkNewDoctorEdit = (CheckBox)e.Row.FindControl("chkNewDoctorEdit");
            CheckBox chkNewDoctorDeAct = (CheckBox)e.Row.FindControl("chkNewDoctorDeAct");
            CheckBox chkNewDoctorView = (CheckBox)e.Row.FindControl("chkNewDoctorView");

            CheckBox chkChemAdd = (CheckBox)e.Row.FindControl("chkChemAdd");
            CheckBox chkChemEdit = (CheckBox)e.Row.FindControl("chkChemEdit");
            CheckBox chkChemDeAct = (CheckBox)e.Row.FindControl("chkChemDeAct");
            CheckBox chkChemView = (CheckBox)e.Row.FindControl("chkChemView");

            CheckBox chkTerrAdd = (CheckBox)e.Row.FindControl("chkTerrAdd");
            CheckBox chkTerrEdit = (CheckBox)e.Row.FindControl("chkTerrEdit");            
            CheckBox chkTerrDeAct = (CheckBox)e.Row.FindControl("chkTerrDeAct");
            CheckBox chkTerrView = (CheckBox)e.Row.FindControl("chkTerrView");

            CheckBox chkClassAdd = (CheckBox)e.Row.FindControl("chkClassAdd");
            CheckBox chkClassEdit = (CheckBox)e.Row.FindControl("chkClassEdit");
            CheckBox chkClassDeAct = (CheckBox)e.Row.FindControl("chkClassDeAct");
            CheckBox chkClassView = (CheckBox)e.Row.FindControl("chkClassView");

            CheckBox chkDoctorReAct=(CheckBox)e.Row.FindControl("chkDoctorReAct");
            CheckBox chkNewDoctorReAct=(CheckBox)e.Row.FindControl("chkNewDoctorReAct");
            CheckBox chkChemReAct = (CheckBox)e.Row.FindControl("chkChemReAct");
            CheckBox chkClassReAct = (CheckBox)e.Row.FindControl("chkClassReAct");

            AdminSetup adm = new AdminSetup();
            dsSalesForce = adm.Get_Admin_FieldForce_Setup(lblsf_code.Text,div_code);
            if (dsSalesForce.Tables[0].Rows.Count > 0)
            {
                iDoctorAdd = Convert.ToInt16( dsSalesForce.Tables[0].Rows[0].ItemArray.GetValue(2).ToString());
                iDoctorEdit = Convert.ToInt16(dsSalesForce.Tables[0].Rows[0].ItemArray.GetValue(3).ToString());
                iDoctorDeAct = Convert.ToInt16(dsSalesForce.Tables[0].Rows[0].ItemArray.GetValue(4).ToString());
                iDoctorView = Convert.ToInt16(dsSalesForce.Tables[0].Rows[0].ItemArray.GetValue(5).ToString());
                iDoctorName = Convert.ToInt16(dsSalesForce.Tables[0].Rows[0].ItemArray.GetValue(26).ToString());

                iNewDoctorAdd = Convert.ToInt16(dsSalesForce.Tables[0].Rows[0].ItemArray.GetValue(6).ToString());
                iNewDoctorEdit = Convert.ToInt16(dsSalesForce.Tables[0].Rows[0].ItemArray.GetValue(7).ToString());
                iNewDoctorDeAct = Convert.ToInt16(dsSalesForce.Tables[0].Rows[0].ItemArray.GetValue(8).ToString());
                iNewDoctorView = Convert.ToInt16(dsSalesForce.Tables[0].Rows[0].ItemArray.GetValue(9).ToString());

                iChemAdd = Convert.ToInt16(dsSalesForce.Tables[0].Rows[0].ItemArray.GetValue(10).ToString());
                iChemEdit = Convert.ToInt16(dsSalesForce.Tables[0].Rows[0].ItemArray.GetValue(11).ToString());
                iChemDeAct = Convert.ToInt16(dsSalesForce.Tables[0].Rows[0].ItemArray.GetValue(12).ToString());
                iChemView = Convert.ToInt16(dsSalesForce.Tables[0].Rows[0].ItemArray.GetValue(13).ToString());

                iTerrAdd = Convert.ToInt16(dsSalesForce.Tables[0].Rows[0].ItemArray.GetValue(14).ToString());
                iTerrEdit = Convert.ToInt16(dsSalesForce.Tables[0].Rows[0].ItemArray.GetValue(15).ToString());
                iTerrDeAct = Convert.ToInt16(dsSalesForce.Tables[0].Rows[0].ItemArray.GetValue(16).ToString());
                iTerrView  = Convert.ToInt16(dsSalesForce.Tables[0].Rows[0].ItemArray.GetValue(17).ToString());

                iClassAdd = Convert.ToInt16(dsSalesForce.Tables[0].Rows[0].ItemArray.GetValue(18).ToString());
                iClassEdit = Convert.ToInt16(dsSalesForce.Tables[0].Rows[0].ItemArray.GetValue(19).ToString());
                iClassDeAct = Convert.ToInt16(dsSalesForce.Tables[0].Rows[0].ItemArray.GetValue(20).ToString());
                iClassView = Convert.ToInt16(dsSalesForce.Tables[0].Rows[0].ItemArray.GetValue(21).ToString());

                iDoctorReAct = Convert.ToInt16(dsSalesForce.Tables[0].Rows[0].ItemArray.GetValue(22).ToString());
                iNewDoctorReAct = Convert.ToInt16(dsSalesForce.Tables[0].Rows[0].ItemArray.GetValue(23).ToString());
                iChemReAct = Convert.ToInt16(dsSalesForce.Tables[0].Rows[0].ItemArray.GetValue(24).ToString());
                iClassReAct = Convert.ToInt16(dsSalesForce.Tables[0].Rows[0].ItemArray.GetValue(25).ToString());

                if (iDoctorAdd == 0) 
                    chkDoctorAdd.Checked = true;

                if (iDoctorEdit == 0)
                    chkDoctorEdit.Checked = true;
                
                if (iDoctorDeAct == 0)
                    chkDoctorDeAct.Checked = true;

                if (iDoctorView == 0)
                    chkDoctorView.Checked = true;

                if (iDoctorName == 0)
                    chkDoctorName.Checked = true;

                if (iNewDoctorAdd == 0)
                    chkNewDoctorAdd.Checked = true;

                if (iNewDoctorEdit == 0)
                    chkNewDoctorEdit.Checked = true;

                if (iNewDoctorDeAct == 0)
                    chkNewDoctorDeAct.Checked = true;

                if (iNewDoctorView == 0)
                    chkNewDoctorView.Checked = true;

                if (iChemAdd  == 0)
                    chkChemAdd.Checked = true;

                if (iChemEdit  == 0)
                    chkChemEdit.Checked = true;

                if (iChemDeAct  == 0)
                    chkChemDeAct.Checked = true;

                if (iChemView == 0)
                    chkChemView.Checked = true;

                if (iTerrAdd == 0)
                    chkTerrAdd.Checked = true;

                if (iTerrEdit == 0)
                    chkTerrEdit.Checked = true;

                if (iTerrDeAct == 0)
                    chkTerrDeAct.Checked = true;

                if (iTerrView == 0)
                    chkTerrView.Checked = true;

                if (iClassAdd == 0)
                    chkClassAdd.Checked = true;

                if (iClassEdit == 0)
                    chkClassEdit.Checked = true;

                if (iClassDeAct == 0)
                    chkClassDeAct.Checked = true;

                if (iClassView == 0)
                    chkClassView.Checked = true;
                
                if (iDoctorReAct == 0)
                    chkDoctorReAct.Checked = true;

                if (iNewDoctorReAct == 0)
                    chkNewDoctorReAct.Checked = true;

                if (iChemReAct == 0)
                    chkChemReAct.Checked = true;

                if (iClassReAct == 0)
                    chkClassReAct.Checked = true;
            }
        }

        if (e.Row.RowType == DataControlRowType.Header)
        {
            
        }
    }

    protected void grdSalesForce_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            //Creating a gridview object            
            GridView objGridView = (GridView)sender;

            //Creating a gridview row object
            GridViewRow objgridviewrow = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);

            //Creating a table cell object
            TableCell objtablecell = new TableCell();
            Territory terr = new Territory();
            DataSet dsTerritory = new DataSet();
            dsTerritory = terr.getWorkAreaName(div_code);
            if (dsTerritory.Tables[0].Rows.Count > 0)
            {
               // e.Row.Cells[7].Text = dsTerritory.Tables[0].Rows[0]["wrk_area_Name"].ToString();
                string terri = dsTerritory.Tables[0].Rows[0]["wrk_area_Name"].ToString();
            }
            #region Merge cells

            AddMergedCells(objgridviewrow, objtablecell, 0, "Field Force Name", System.Drawing.Color.Lavender.Name, true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "Designation", System.Drawing.Color.Lavender.Name, true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "HQ", System.Drawing.Color.Lavender.Name, true);
            AddMergedCells(objgridviewrow, objtablecell, 6, "Listed Doctor", System.Drawing.Color.Lavender.Name, true);
            AddMergedCells(objgridviewrow, objtablecell, 5, "UnListed Doctor", System.Drawing.Color.Lavender.Name, true);
            AddMergedCells(objgridviewrow, objtablecell, 5, "Chemist", System.Drawing.Color.Lavender.Name, true);
           // AddMergedCells(objgridviewrow, objtablecell, 4, "" + dsTerritory.Tables[0].Rows[0]["wrk_area_Name"].ToString() + "", System.Drawing.Color.Lavender.Name, true);
            AddMergedCells(objgridviewrow, objtablecell, 4, "Territory", System.Drawing.Color.Lavender.Name, true);
            AddMergedCells(objgridviewrow, objtablecell, 5, "Hospital", System.Drawing.Color.Lavender.Name, true);
            //Lastly add the gridrow object to the gridview object at the 0th position
            //Because, the header row position is 0.
            objGridView.Controls[0].Controls.AddAt(0, objgridviewrow);

            GridViewRow objgridviewrow2 = new GridViewRow(2, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell objtablecell2 = new TableCell();
            AddMergedCells(objgridviewrow2, objtablecell2, 0, "Add", System.Drawing.Color.Lavender.Name, false);
            AddMergedCells(objgridviewrow2, objtablecell2, 0, "Edit", System.Drawing.Color.Lavender.Name, false);
            AddMergedCells(objgridviewrow2, objtablecell2, 0, "Deact.", System.Drawing.Color.Lavender.Name, false);
            AddMergedCells(objgridviewrow2, objtablecell2, 0, "View", System.Drawing.Color.Lavender.Name, false);
            AddMergedCells(objgridviewrow2, objtablecell2, 0, "React.", System.Drawing.Color.Lavender.Name, false);

            AddMergedCells(objgridviewrow2, objtablecell2, 0, "NameChg.", System.Drawing.Color.Lavender.Name, false);

            AddMergedCells(objgridviewrow2, objtablecell2, 0, "Add", System.Drawing.Color.Lavender.Name, false);
            AddMergedCells(objgridviewrow2, objtablecell2, 0, "Edit", System.Drawing.Color.Lavender.Name, false);
            AddMergedCells(objgridviewrow2, objtablecell2, 0, "Deact.", System.Drawing.Color.Lavender.Name, false);
            AddMergedCells(objgridviewrow2, objtablecell2, 0, "View", System.Drawing.Color.Lavender.Name, false);
            AddMergedCells(objgridviewrow2, objtablecell2, 0, "React.", System.Drawing.Color.Lavender.Name, false);

            AddMergedCells(objgridviewrow2, objtablecell2, 0, "Add", System.Drawing.Color.Lavender.Name, false);
            AddMergedCells(objgridviewrow2, objtablecell2, 0, "Edit", System.Drawing.Color.Lavender.Name, false);
            AddMergedCells(objgridviewrow2, objtablecell2, 0, "Deact.", System.Drawing.Color.Lavender.Name, false);
            AddMergedCells(objgridviewrow2, objtablecell2, 0, "View", System.Drawing.Color.Lavender.Name, false);
            AddMergedCells(objgridviewrow2, objtablecell2, 0, "React.", System.Drawing.Color.Lavender.Name, false);

            AddMergedCells(objgridviewrow2, objtablecell2, 0, "Add", System.Drawing.Color.Lavender.Name, false);
            AddMergedCells(objgridviewrow2, objtablecell2, 0, "Edit", System.Drawing.Color.Lavender.Name, false);
            AddMergedCells(objgridviewrow2, objtablecell2, 0, "Deact.", System.Drawing.Color.Lavender.Name, false);
            AddMergedCells(objgridviewrow2, objtablecell2, 0, "View", System.Drawing.Color.Lavender.Name, false);
            //AddMergedCells(objgridviewrow2, objtablecell2, 0, "React.", System.Drawing.Color.Lavender.Name, false);

            AddMergedCells(objgridviewrow2, objtablecell2, 0, "Add", System.Drawing.Color.Lavender.Name, false);
            AddMergedCells(objgridviewrow2, objtablecell2, 0, "Edit", System.Drawing.Color.Lavender.Name, false);
            AddMergedCells(objgridviewrow2, objtablecell2, 0, "Deact.", System.Drawing.Color.Lavender.Name, false);
            AddMergedCells(objgridviewrow2, objtablecell2, 0, "View", System.Drawing.Color.Lavender.Name, false);
            AddMergedCells(objgridviewrow2, objtablecell2, 0, "React.", System.Drawing.Color.Lavender.Name, false);

            objGridView.Controls[0].Controls.AddAt(1, objgridviewrow2);
            #endregion
        }
    }

    protected void AddMergedCells(GridViewRow objgridviewrow, TableCell objtablecell, int colspan, string celltext, string backcolor, bool bRowspan)
    {
        objtablecell = new TableCell();
        objtablecell.Text = celltext;
        objtablecell.ColumnSpan = colspan;
        if ((colspan == 0) && bRowspan)
        {
            objtablecell.RowSpan = 2;
        }
        objtablecell.Style.Add("background-color", backcolor);
        objtablecell.HorizontalAlign = HorizontalAlign.Center;
        objgridviewrow.Cells.Add(objtablecell);
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string cur_sf_code = string.Empty;
        int iReturn = -1;

        foreach (GridViewRow gridRow in grdSalesForce.Rows)
        {
            Label lblsf_code = (Label)gridRow.Cells[0].FindControl("lblsf_code");
            cur_sf_code = lblsf_code.Text;

            iDoctorAdd = 1;
            iDoctorEdit = 1;            
            iDoctorDeAct = 1;
            iDoctorView = 1;
            iDoctorName = 1;

            iNewDoctorAdd = 1;
            iNewDoctorEdit = 1;
            iNewDoctorDeAct = 1;
            iNewDoctorView = 1;
            iChemAdd = 1;
            iChemEdit = 1;
            iChemDeAct = 1;
            iChemView = 1;
            iTerrAdd = 1;
            iTerrEdit = 1;            
            iTerrDeAct = 1;
            iTerrView = 1;
            iClassAdd = 1;
            iClassEdit = 1;
            iClassDeAct = 1;
            iClassView = 1;
            iDoctorReAct = 1;
            iNewDoctorReAct = 1;
            iChemReAct = 1;
            iClassReAct = 1;            

            CheckBox chkDoctorAdd = (CheckBox)gridRow.Cells[2].FindControl("chkDoctorAdd");
            CheckBox chkDoctorEdit = (CheckBox)gridRow.Cells[3].FindControl("chkDoctorEdit");            
            CheckBox chkDoctorDeAct = (CheckBox)gridRow.Cells[4].FindControl("chkDoctorDeAct");
            CheckBox chkDoctorView = (CheckBox)gridRow.Cells[5].FindControl("chkDoctorView");
            CheckBox chkDoctorName = (CheckBox)gridRow.Cells[26].FindControl("chkDoctorName");       

            CheckBox chkNewDoctorAdd = (CheckBox)gridRow.Cells[6].FindControl("chkNewDoctorAdd");
            CheckBox chkNewDoctorEdit = (CheckBox)gridRow.Cells[7].FindControl("chkNewDoctorEdit");
            CheckBox chkNewDoctorDeAct = (CheckBox)gridRow.Cells[8].FindControl("chkNewDoctorDeAct");
            CheckBox chkNewDoctorView = (CheckBox)gridRow.Cells[9].FindControl("chkNewDoctorView");
               
            CheckBox chkChemAdd = (CheckBox)gridRow.Cells[10].FindControl("chkChemAdd");
            CheckBox chkChemEdit = (CheckBox)gridRow.Cells[11].FindControl("chkChemEdit");
            CheckBox chkChemDeAct = (CheckBox)gridRow.Cells[12].FindControl("chkChemDeAct");
            CheckBox chkChemView = (CheckBox)gridRow.Cells[13].FindControl("chkChemView");

            CheckBox chkTerrAdd = (CheckBox)gridRow.Cells[14].FindControl("chkTerrAdd");
            CheckBox chkTerrEdit = (CheckBox)gridRow.Cells[15].FindControl("chkTerrEdit");            
            CheckBox chkTerrDeAct = (CheckBox)gridRow.Cells[16].FindControl("chkTerrDeAct");
            CheckBox chkTerrView = (CheckBox)gridRow.Cells[17].FindControl("chkTerrView");

            CheckBox chkClassAdd = (CheckBox)gridRow.Cells[18].FindControl("chkClassAdd");
            CheckBox chkClassEdit = (CheckBox)gridRow.Cells[19].FindControl("chkClassEdit");
            CheckBox chkClassDeAct = (CheckBox)gridRow.Cells[20].FindControl("chkClassDeAct");
            CheckBox chkClassView = (CheckBox)gridRow.Cells[21].FindControl("chkClassView");

            CheckBox chkDoctorReAct = (CheckBox)gridRow.Cells[22].FindControl("chkDoctorReAct");
            CheckBox chkNewDoctorReAct = (CheckBox)gridRow.Cells[23].FindControl("chkNewDoctorReAct");
            CheckBox chkChemReAct = (CheckBox)gridRow.Cells[24].FindControl("chkChemReAct");
            CheckBox chkClassReAct = (CheckBox)gridRow.Cells[25].FindControl("chkClassReAct");

            if (cur_sf_code != "")
            {
                if (chkDoctorAdd.Checked)
                    iDoctorAdd = 0;

                if (chkDoctorEdit.Checked)
                    iDoctorEdit = 0;

                if (chkDoctorDeAct.Checked)
                    iDoctorDeAct = 0;

                if (chkDoctorView.Checked)
                    iDoctorView = 0;

                if (chkDoctorName.Checked)
                    iDoctorName = 0;

                if (chkNewDoctorAdd.Checked)
                    iNewDoctorAdd = 0;

                if (chkNewDoctorEdit.Checked)
                    iNewDoctorEdit = 0;

                if (chkNewDoctorDeAct.Checked)
                    iNewDoctorDeAct  = 0;

                if (chkNewDoctorView.Checked)
                    iNewDoctorView = 0;

                if (chkChemAdd.Checked)
                    iChemAdd = 0;

                if (chkChemEdit.Checked)
                    iChemEdit = 0;

                if (chkChemDeAct.Checked)
                    iChemDeAct = 0;

                if (chkChemView.Checked)
                    iChemView = 0;

                if (chkTerrAdd.Checked)
                    iTerrAdd = 0;

                if (chkTerrEdit.Checked)
                    iTerrEdit = 0;

                if (chkTerrDeAct.Checked)
                    iTerrDeAct = 0;

                if (chkTerrView.Checked)
                    iTerrView = 0;

                if (chkClassAdd.Checked)
                    iClassAdd = 0;

                if (chkClassEdit.Checked)
                    iClassEdit = 0;

                if (chkClassDeAct.Checked)
                    iClassDeAct = 0;

                if (chkClassView.Checked)
                    iClassView = 0;

                if (chkDoctorReAct.Checked)
                    iDoctorReAct = 0;

                if (chkNewDoctorReAct.Checked)
                    iNewDoctorReAct = 0;

                if (chkChemReAct.Checked)
                    iChemReAct = 0;

                if (chkClassReAct.Checked)
                    iClassReAct = 0;

                AdminSetup adm = new AdminSetup();
                iReturn = adm.Add_Admin_FieldForce_Setup(cur_sf_code, div_code, iDoctorAdd, iDoctorEdit, iDoctorDeAct, iDoctorView, iNewDoctorAdd, iNewDoctorEdit, iNewDoctorDeAct, iNewDoctorView, iChemAdd, iChemEdit, iChemDeAct, iChemView, iTerrAdd, iTerrEdit, iTerrDeAct, iTerrView, iClassAdd, iClassEdit, iClassDeAct, iClassView, iDoctorReAct, iNewDoctorReAct, iChemReAct, iClassReAct, iDoctorName);
            }
        }
        if (iReturn > 0)
        {
            //menu1.Status = "Setup for Screen Access have been updated successfully";
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Setup for Screen Access have been updated successfully');</script>");
        }

    }
    protected void btnGo_Click(object sender, EventArgs e)
    {
        if (ddlFieldForce.SelectedValue.ToString().Trim().Length > 0)
        {
            FillSalesForce();
        }

    }


    protected void ddlFieldForce_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if (ddlFieldForce.SelectedIndex > 0)
        //{
        //    FillSalesForce();
         
        //    menu1.Status = "";
        //}

    }
}