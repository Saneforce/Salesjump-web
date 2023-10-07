using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;

public partial class Reports_rptDoctorDetails : System.Web.UI.Page
{
    DataSet dsSalesForce = null;
    DataSet dsDoctor = null;
    string div_code = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        if (!Page.IsPostBack)
        {
            FillManagers();
            menu1.Title = this.Page.Title;
            menu1.FindControl("btnBack").Visible = false;
        }


    }

    private void FillManagers()
    {
        SalesForce sf = new SalesForce();
        dsSalesForce  = sf.getUserList_Reporting(div_code);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            ddlFieldForce.DataTextField = "sf_name";
            ddlFieldForce.DataValueField = "sf_code";
            ddlFieldForce.DataSource = dsSalesForce;
            ddlFieldForce.DataBind();
        }
    }


    protected void grdSalesForce_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton lnkView = new LinkButton();
            lnkView.ID = "lnkView";
            lnkView.Text = "View";
            //lnkView.Click += ViewDetails;
            lnkView.CommandArgument = (e.Row.DataItem as DataRowView).Row["Id"].ToString();
            e.Row.Cells[2].Controls.Add(lnkView);
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

            #region Merge cells

            //Lastly add the gridrow object to the gridview object at the 0th position
            //Because, the header row position is 0.
            objGridView.Controls[0].Controls.AddAt(0, objgridviewrow);

            GridViewRow objgridviewrow2 = new GridViewRow(2, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell objtablecell2 = new TableCell();

            Doctor dr = new Doctor();
            dsDoctor = dr.getDocCat(div_code);
            int icount = 0;
            foreach (DataRow dataRow in dsDoctor.Tables[0].Rows)
            {
                icount = icount + 1;
                AddMergedCells(objgridviewrow2, objtablecell2, 0, dataRow["Doc_Cat_Name"].ToString(), System.Drawing.Color.LightSkyBlue.Name, false);
            }

            AddMergedCells(objgridviewrow, objtablecell, 0, "S.No", "", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "Field Force Name", System.Drawing.Color.LightSkyBlue.Name, true);
            AddMergedCells(objgridviewrow, objtablecell, icount, "Category", System.Drawing.Color.LightSkyBlue.Name, true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "Total", System.Drawing.Color.LightSkyBlue.Name, true);

            objGridView.Controls[0].Controls.AddAt(1, objgridviewrow2);
            #endregion
        }
        else if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton lnkView = new LinkButton();
            lnkView.ID = "lnkView";
            lnkView.Text = "View";
            //lnkView.Click += ViewDetails;
            lnkView.CommandArgument = (e.Row.DataItem as DataRowView).Row["sf_name"].ToString();
            e.Row.Cells[2].Controls.Add(lnkView);

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

    private void FillSalesForce()
    {
        SalesForce sf = new SalesForce();
        dsSalesForce = sf.getDoctorCount_SFWise(div_code,ddlFieldForce.SelectedValue.ToString());
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            grdSalesForce.Visible = true;
            grdSalesForce.DataSource = dsSalesForce;
            grdSalesForce.DataBind();
        }
    }


    protected void ddlFieldForce_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlFieldForce.SelectedValue.ToString().Trim().Length > 0)
            FillSalesForce();
    }
}