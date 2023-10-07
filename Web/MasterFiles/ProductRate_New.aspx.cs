using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
using System.Data.SqlClient;

public partial class MasterFiles_ProductRate_New : System.Web.UI.Page
{
    DataSet dsDivision = null;

    DataSet dsState = null;
    DataSet dsProd = null;
    string sState = string.Empty;
    string div_code = string.Empty;
    string[] statecd;
    string state_cd = string.Empty;
    string prod_code = string.Empty;
    string prod_name = string.Empty;
    decimal mrp_amt;
    decimal ret_amt;
    decimal dist_amt;
    decimal nsr_amt;
    decimal target_amt;
    string effective_from = string.Empty;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int time;
	string sf_type = string.Empty;
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
        if (!Page.IsPostBack)
        {
            //menu1.Title = this.Page.Title;
            FillState(div_code);
            //menu1.FindControl("btnBack").Visible = false;
            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);
            txtEffFrom.Text = DateTime.Now.ToShortDateString();          
            chkbox_states.SelectedIndex = -1;
            //btnGo.Focus();
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
    private void FillState(string div_code)
    {
        Division dv = new Division();
        dsDivision = dv.getStatePerDivision(div_code);
        if (dsDivision.Tables[0].Rows.Count > 0)
        {
            int i = 0;
            state_cd = string.Empty;
            sState = dsDivision.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
            statecd = sState.Split(',');
            foreach (string st_cd in statecd)
            {
                if (i == 0)
                {
                    state_cd = state_cd + st_cd;
                }
                else
                {
                    if (st_cd.Trim().Length > 0)
                    {
                        state_cd = state_cd + "," + st_cd;
                    }
                }
                i++;
            }

            State st = new State();
            dsState = st.getStateProd(state_cd);
            ddlState.DataTextField = "statename";
            ddlState.DataValueField = "state_code";
            ddlState.DataSource = dsState;
            ddlState.DataBind();
            ddlState.Items.Insert(0, new ListItem("--Select--"));   
          dsState = st.getStateProd(state_cd);
            chkbox_states.DataTextField = "statename";
            chkbox_states.DataValueField = "state_code";
            chkbox_states.DataSource = dsState;
            chkbox_states.DataBind();

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
        Product dv = new Product();
        dtGrid = dv.getProductRatelist_DataTable(div_code);
        return dtGrid;
    }

    //protected void grdProdRate_Sorting(object sender, GridViewSortEventArgs e)
    //{

    //    string sortingDirection = string.Empty;
    //    if (dir == SortDirection.Ascending)
    //    {
    //        dir = SortDirection.Descending;
    //        sortingDirection = "Desc";
    //    }
    //    else
    //    {
    //        dir = SortDirection.Ascending;
    //        sortingDirection = "Asc";
    //    }
    //    DataView sortedView = new DataView(BindGridView());
    //    sortedView.Sort = e.SortExpression + " " + sortingDirection;
    //    grdProdRate.DataSource = sortedView;
    //    grdProdRate.DataBind();

    //}



    //private void FillProd()
    //{
    //    Product dv = new Product();
    //    //dsProd = dv.getProdRate(ddlState.SelectedValue.ToString(), div_code);
    //    if (ddlState.SelectedItem.Text == "ALL")
    //    {
    //        dsProd = dv.getProductRate_all(div_code);
    //    }
    //    else
    //    {
    //        dsProd = dv.getProductRate(ddlState.SelectedValue.ToString(), div_code);
    //    }

    //    if (dsProd.Tables[0].Rows.Count > 0)
    //    {
    //        btnSubmit.Visible = true;
    //        grdProdRate.Visible = true;
    //        grdProdRate.DataSource = dsProd;
    //        grdProdRate.DataBind();
    //    }
    //    else
    //    {
    //        btnSubmit.Visible = false;
    //        grdProdRate.DataSource = dsProd;
    //        grdProdRate.DataBind();
    //    }
    //}

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        FillProd();
      
        
       
    }
    protected void btnGo_Click(object sender, EventArgs e)
    {               
        
    }
    //protected void GVMissedCall_RowCreated(object sender, GridViewRowEventArgs e)
    //{
    //    if (e.Row.RowType == DataControlRowType.Header)
    //    {
    //        //Creating a gridview object            
    //        GridView objGridView = (GridView)sender;

    //        //Creating a gridview row object
    //        GridViewRow objgridviewrow = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);

    //        //Creating a table cell object
    //        TableCell objtablecell = new TableCell();

    //        //#region Merge cells

    //        AddMergedCells(objgridviewrow, objtablecell, 0, "S.No", "#DDEECC", true);
    //        AddMergedCells(objgridviewrow, objtablecell, 0, "Product Name", "#DDEECC", true);
    //        AddMergedCells(objgridviewrow, objtablecell, 0, "UOM", "#DDEECC", true);
    //        AddMergedCells(objgridviewrow, objtablecell, 2, "Dist Price", "#DDEECC", true);
    //        AddMergedCells(objgridviewrow, objtablecell, 2, "Retailed Price", "#DDEECC", true);
    //        AddMergedCells(objgridviewrow, objtablecell, 0, "MRP Rate", "#DDEECC", true);
    //        //AddMergedCells(objgridviewrow, objtablecell, 0, "Total Listed Drs", "#DDEECC", true);
    //        objGridView.Controls[0].Controls.AddAt(0, objgridviewrow);
    //        GridViewRow objgridviewrow2 = new GridViewRow(2, 0, DataControlRowType.Header, DataControlRowState.Insert);
    //        TableCell objtablecell2 = new TableCell();
    //        for (int i = 0; i < 2; i++)
    //        {
    //            AddMergedCells(objgridviewrow2, objtablecell2, 0, "Base Rate", "#DDEECC", true);
    //            AddMergedCells(objgridviewrow2, objtablecell2, 0, "Case Rate", "#DDEECC", true);
    //        }
    //            objGridView.Controls[0].Controls.AddAt(1, objgridviewrow2);
    //    }
    //}


    //protected void AddMergedCells(GridViewRow objgridviewrow, TableCell objtablecell, int colspan, string celltext, string backcolor, bool bRowspan)
    //{
    //    objtablecell = new TableCell();
    //    objtablecell.Text = celltext;
    //    objtablecell.ColumnSpan = colspan;
    //    if ((colspan == 0) && bRowspan)
    //    {
    //        objtablecell.RowSpan = 2;
    //    }
    //    objtablecell.Style.Add("background-color", backcolor);
    //    objtablecell.HorizontalAlign = HorizontalAlign.Center;

    //    if (celltext == "FieldForce Name")
    //    {
    //        objtablecell.Wrap = false;
    //    }
    //    objgridviewrow.Cells.Add(objtablecell);
    //}

    protected void GVMissedCall_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            //Creating a gridview object            
            GridView objGridView = (GridView)sender;
            //Creating a gridview row object
            GridViewRow objgridviewrow = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);
            //Creating a table cell object
            TableCell objtablecell = new TableCell();
            AddMergedCells(objgridviewrow, objtablecell, 0, "S.No", "#DDEECC", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "Product Name", "#DDEECC", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "UOM", "#DDEECC", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "UOM Value", "#DDEECC", true);
            AddMergedCells(objgridviewrow, objtablecell, 2, "Distributor Price", "#DDEECC", true);
            AddMergedCells(objgridviewrow, objtablecell, 2, "Retailed Price", "#DDEECC", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "MRP Rate", "#DDEECC", true);
          
            //AddMergedCells(objgridviewrow, objtablecell, 0, "Total Listed Drs", "#DDEECC", true);
            objGridView.Controls[0].Controls.AddAt(0, objgridviewrow);
            GridViewRow objgridviewrow2 = new GridViewRow(2, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell objtablecell2 = new TableCell();
            for (int i = 0; i < 2; i++)
            {
                AddMergedCells(objgridviewrow2, objtablecell2, 0, "Base Rate", "#DDEECC", false);
                AddMergedCells(objgridviewrow2, objtablecell2, 0, "Case Rate", "#DDEECC", false);
            }
            objGridView.Controls[0].Controls.AddAt(1, objgridviewrow2);

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

        if (celltext == "FieldForce Name")
        {
            objtablecell.Wrap = false;
        }
        objgridviewrow.Cells.Add(objtablecell);
    }



    private void FillProd()
    {

        int iReturn = -1;
        Product dv = new Product();
        dsProd = dv.getProductRate(ddlState.SelectedValue.ToString(), div_code);
     

        string sChkLocation = string.Empty;
        int iMaxState = 0;
        iMaxState = dv.getMaxStateSlNo(ddlState.SelectedValue, div_code);        
        List<String> str = new List<String>();
        for (int i = 0; i < chkbox_states.Items.Count; i++)
        {
            if (chkbox_states.Items[i].Selected)
            {                
                str.Add(chkbox_states.Items[i].Value);               
            }
        }

        if (ddlState.SelectedItem.Text == "ALL")
        {
            iReturn = dv.DeleteProductRate(div_code);
        }
        else
        {
            for (int k = 0; k < str.Count; k++)
            {
                if (str[k] != ddlState.SelectedValue.ToString())
                {
                    iReturn = dv.DeleteProductRate(str[k], div_code);
                }
            }
        }

        foreach (DataRow row in dsProd.Tables[0].Rows)
        {
            prod_code = row["Product_Detail_Code"].ToString();            
            mrp_amt = Convert.ToDecimal(row["DP_Base_Rate"].ToString());            
            ret_amt = Convert.ToDecimal(row["RP_Base_Rate"].ToString());            
            dist_amt = Convert.ToDecimal(row["DP_Case_Rate"].ToString());            
            nsr_amt = Convert.ToDecimal(row["RP_Case_Rate"].ToString());            
            target_amt = Convert.ToDecimal(row["MRP_Rate"].ToString());
			decimal ddp =  Convert.ToDecimal(row["Distributor_Discount_Price"].ToString());
			decimal rdp =  Convert.ToDecimal(row["Retailer_Discount_Price"].ToString());

            decimal ssbr = Convert.ToDecimal(row["SS_Base_Rate"].ToString());
            decimal sscr = Convert.ToDecimal(row["SS_Case_Rate"].ToString());
	
            for (int k = 0; k < str.Count; k++)
            {
                if (str[k] != ddlState.SelectedValue.ToString())
                {
                    iReturn = dv.UpdateProductRate(prod_code, str[k], txtEffFrom.Text, System.Math.Round(mrp_amt, 2), System.Math.Round(ret_amt, 2), System.Math.Round(dist_amt, 2), System.Math.Round(nsr_amt, 2), System.Math.Round(target_amt, 2), div_code, iMaxState, ddp, rdp, ssbr, sscr);
                }
            }
        }
        if (iReturn > 0)
        {
            // menu1.Status = "Produce Rate Updated Successfully ";
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Updated Successfully');</script>");
        }       
    }
}