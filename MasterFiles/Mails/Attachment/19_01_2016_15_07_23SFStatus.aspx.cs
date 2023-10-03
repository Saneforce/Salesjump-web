using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;

public partial class MasterFiles_SFStatus : System.Web.UI.Page
{
    DataSet dsSalesForce = null;
    DataSet dsDivision = null;
    DataSet dsSF = null;
    DataSet dsState = null;
    string sState = string.Empty;
    string div_code = string.Empty;
    string[] statecd;
    string state_cd = string.Empty;
    string sf_code = string.Empty;
    string sf_name = string.Empty;
    string usr_name = string.Empty;
    int state = -1;
    string hq = string.Empty;
    int total_MR = 0;
    int total_MGR = 0;
    int total_ActMR = 0;
    int total_ActMGR = 0;
    int total_DeActMR = 0;
    int total_DeActMGR = 0;
    int total_MRBlock = 0;
    int total_MGRBlock = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        Session["backurl"] = "SalesForceList.aspx";
        div_code = Session["div_code"].ToString();
        if (!Page.IsPostBack)
        {
            FillSalesForce();
            menu1.Title = this.Page.Title;
          
        }

    }

    protected void grdSalesForce_PreRender(object sender, EventArgs e)
    {
        var gridView = (GridView)sender;
        var header = (GridViewRow)gridView.Controls[0].Controls[1];

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
        GridViewRow objgridviewrow = new GridViewRow	(1, 0, DataControlRowType.Header, DataControlRowState.Insert);
 
        //Creating a table cell object
        TableCell objtablecell = new TableCell();
 
        #region Merge cells

        AddMergedCells(objgridviewrow, objtablecell, 0, "S.No", "#666699", true);
        AddMergedCells(objgridviewrow, objtablecell, 0, "State", "#666699", true);
        AddMergedCells(objgridviewrow, objtablecell, 0, "No of MR", "#666699", true);
        AddMergedCells(objgridviewrow, objtablecell, 0, "No of MGR", "#666699", true);
        AddMergedCells(objgridviewrow, objtablecell, 2, "Active", "#666699", true);
        AddMergedCells(objgridviewrow, objtablecell, 2, "Vacant", "#666699", true);
        AddMergedCells(objgridviewrow, objtablecell, 2, "Blocked", "#666699", true);
        AddMergedCells(objgridviewrow, objtablecell, 0, "Total", "#666699", true);
        
 
        //Lastly add the gridrow object to the gridview object at the 0th position
        //Because, the header row position is 0.
        objGridView.Controls[0].Controls.AddAt(0, objgridviewrow);

        GridViewRow objgridviewrow2 = new GridViewRow(2, 0, DataControlRowType.Header, DataControlRowState.Insert);
        TableCell objtablecell2 = new TableCell();
        AddMergedCells(objgridviewrow2, objtablecell2, 0, "MR Count", "#666699", false);
        AddMergedCells(objgridviewrow2, objtablecell2, 0, "MGR Count", "#666699", false);
        AddMergedCells(objgridviewrow2, objtablecell2, 0, "MR Count", "#666699", false);
        AddMergedCells(objgridviewrow2, objtablecell2, 0, "MGR Count", "#666699", false);
        AddMergedCells(objgridviewrow2, objtablecell2, 0, "MR Count", "#666699", false);
        AddMergedCells(objgridviewrow2, objtablecell2, 0, "MGR Count", "#666699", false);
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
    objtablecell.Style.Add("color", "White");
    objtablecell.HorizontalAlign = HorizontalAlign.Center;
    objgridviewrow.Cells.Add(objtablecell);
}

    private void FillSalesForce()
    {
        SalesForce sf = new SalesForce();
        dsSalesForce = sf.getSF_Status(div_code);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            grdSalesForce.Visible = true;
            grdSalesForce.DataSource = dsSalesForce;
            grdSalesForce.DataBind();
            
        }
    }

    protected void grdSalesForce_RowDataBound(object sender, GridViewRowEventArgs e)
    {
       
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblsfName = (Label)e.Row.FindControl("lblsfName");
            int MR_Count = Int32.Parse(lblsfName.Text);
            total_MR = total_MR + MR_Count;

            Label lblSFType = (Label)e.Row.FindControl("lblSFType");
            int MGR_Count = Int32.Parse(lblSFType.Text);
            total_MGR = total_MGR + MGR_Count;

            Label lblActive_count = (Label)e.Row.FindControl("lblActive_count");
            int Active_MR_Count = Int32.Parse(lblActive_count.Text);
            total_ActMR = total_ActMR + Active_MR_Count;

            Label lblmgr_count = (Label)e.Row.FindControl("lblmgr_count");
            int Active_MGR_Count = Int32.Parse(lblmgr_count.Text);
            total_ActMGR = total_ActMGR + Active_MGR_Count;

            Label lblDeactive_count = (Label)e.Row.FindControl("lblDeactive_count");
            int DeActive_MR_Count = Int32.Parse(lblDeactive_count.Text);
            total_DeActMR = total_DeActMR + DeActive_MR_Count;

            Label lblDeact_mgr_count = (Label)e.Row.FindControl("lblDeact_mgr_count");
            int DeActive_MGR_Count = Int32.Parse(lblDeact_mgr_count.Text);
            total_DeActMGR = total_DeActMGR + DeActive_MGR_Count;

            Label lblBlock_count = (Label)e.Row.FindControl("lblBlock_count");
            int Block_MR_Count = Int32.Parse(lblBlock_count.Text);
            total_MRBlock = total_MRBlock + Block_MR_Count;

            Label lblBlock_mgr = (Label)e.Row.FindControl("lblBlock_mgr");
            int Block_MGR_Count = Int32.Parse(lblBlock_mgr.Text);
            total_MGRBlock = total_MGRBlock + Block_MGR_Count;


            int count = 0;

            count = MR_Count + MGR_Count + Active_MR_Count + Active_MGR_Count + DeActive_MR_Count + DeActive_MGR_Count + Block_MR_Count + Block_MGR_Count;

            Label total = (Label)e.Row.FindControl("total");
            total.Text = count.ToString();

        }
        if (e.Row.RowType == DataControlRowType.Footer) 
        {
            Label lblMR_Count = (Label)e.Row.FindControl("lblMR_Count");
            lblMR_Count.Text = total_MR.ToString();

            Label lblMGR_Count = (Label)e.Row.FindControl("lblMGR_Count");
            lblMGR_Count.Text = total_MGR.ToString();

            Label lblActMR_Count = (Label)e.Row.FindControl("lblActMR_Count");
            lblActMR_Count.Text = total_ActMR.ToString();

            Label lblActMGR_Count = (Label)e.Row.FindControl("lblActMGR_Count");
            lblActMGR_Count.Text = total_ActMGR.ToString();

            Label lblDeActMR_Count = (Label)e.Row.FindControl("lblDeActMR_Count");
            lblDeActMR_Count.Text = total_DeActMR.ToString();

            Label lblDeActMGR_Count = (Label)e.Row.FindControl("lblDeActMGR_Count");
            lblDeActMGR_Count.Text = total_DeActMGR.ToString();

            Label lblBlockMR_Count = (Label)e.Row.FindControl("lblBlockMR_Count");
            lblBlockMR_Count.Text = total_MRBlock.ToString();

            Label lblBlockMGR_Count = (Label)e.Row.FindControl("lblBlockMGR_Count");
            lblBlockMGR_Count.Text = total_MGRBlock.ToString();
        }
    }

}