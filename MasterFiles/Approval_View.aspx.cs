using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;

public partial class MasterFiles_Approval_View : System.Web.UI.Page
{
    #region "Declaration"
    DataSet dsSalesForce = null;
    DataSet dsDivision = null;
    DataSet dsUserList = null;
    DataSet dsSF = null;
    DataSet dsReport = null;
    string div_code = string.Empty;
    string sf_code = string.Empty;
    string Reporting_To_SF = string.Empty;
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["backurl"] = "Approval_List.aspx";
        div_code = Session["div_code"].ToString();
        if (!Page.IsPostBack)
        {
            FillReporting();
            FillSalesForce_Reporting();
            menu1.Title = this.Page.Title;
            menu1.FindControl("btnBack").Visible = true;
        }

    }
    protected void grdSalesForce_PreRender(object sender, EventArgs e)
    {
        var gridView = (GridView)sender;
        //var header = (GridViewRow)gridView.Controls[0].Controls[1];

        //header.Cells[0].Visible = false;
        //header.Cells[4].ColumnSpan = 2;
        //header.Cells[6].ColumnSpan = 2;
        //header.Cells[0].RowSpan = 2;
        //header.Cells[1].RowSpan = 2;
        //header.Cells[2].RowSpan = 2;
        //header.Cells[3].RowSpan = 2;
        //header.Cells[1].Text = "Header";
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

            #region Merge cells

            AddMergedCells(objgridviewrow, objtablecell, 0, "S.No", System.Drawing.Color.LightSkyBlue.Name, true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "FieldForce Name", System.Drawing.Color.LightSkyBlue.Name, true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "HQ", System.Drawing.Color.LightSkyBlue.Name, true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "Reporting-To", System.Drawing.Color.LightSkyBlue.Name, true);
            AddMergedCells(objgridviewrow, objtablecell, 7, "Approved By Manager", System.Drawing.Color.LightSkyBlue.Name, true);
            //AddMergedCells(objgridviewrow, objtablecell, 2, "Vacant", System.Drawing.Color.LightSkyBlue.Name, true);
            //AddMergedCells(objgridviewrow, objtablecell, 2, "Blocked", System.Drawing.Color.LightSkyBlue.Name, true);


            //Lastly add the gridrow object to the gridview object at the 0th position
            //Because, the header row position is 0.
            objGridView.Controls[0].Controls.AddAt(0, objgridviewrow);

            GridViewRow objgridviewrow2 = new GridViewRow(2, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell objtablecell2 = new TableCell();
            AddMergedCells(objgridviewrow2, objtablecell2, 0, "DCR", System.Drawing.Color.LightSkyBlue.Name, false);
            AddMergedCells(objgridviewrow2, objtablecell2, 0, "TP", System.Drawing.Color.LightSkyBlue.Name, false);
            AddMergedCells(objgridviewrow2, objtablecell2, 0, "Listed Dr", System.Drawing.Color.LightSkyBlue.Name, false);
            AddMergedCells(objgridviewrow2, objtablecell2, 0, "Leave", System.Drawing.Color.LightSkyBlue.Name, false);
            AddMergedCells(objgridviewrow2, objtablecell2, 0, "Secondary Sales", System.Drawing.Color.LightSkyBlue.Name, false);
            AddMergedCells(objgridviewrow2, objtablecell2, 0, "Expense", System.Drawing.Color.LightSkyBlue.Name, false);
            AddMergedCells(objgridviewrow2, objtablecell2, 0, "Other", System.Drawing.Color.LightSkyBlue.Name, false);
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

    // Function of Grid Values
    private void FillSalesForce_Reporting()
    {
        string sReport = ddlFilter.SelectedValue.ToString();
        SalesForce sf = new SalesForce();
        dsSalesForce = sf.getView_AM(div_code,sReport);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            grdSalesForce.Visible = true;
            grdSalesForce.DataSource = dsSalesForce;
            grdSalesForce.DataBind();


        }

    }
    private void FillReporting()
    {
        SalesForce sf = new SalesForce();
        dsSalesForce = sf.getUserList_Reporting(div_code);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            ddlFilter.DataTextField = "Sf_Name";
            ddlFilter.DataValueField = "Sf_Code";
            ddlFilter.DataSource = dsSalesForce;
            ddlFilter.DataBind();
        }
    }

    protected void grdSalesForce_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdSalesForce.PageIndex = e.NewPageIndex;
        FillSalesForce_Reporting();
    }

    protected void ddlFilter_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void btnGo_Click(object sender, EventArgs e)
    {
        //btnApproval.Visible = true;
        FillSalesForce_Reporting();
    }
}