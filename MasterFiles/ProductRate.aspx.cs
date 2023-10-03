using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
using System.Data.SqlClient;
using System.Transactions;
using DBase_EReport;

public partial class MasterFiles_ProductRate : System.Web.UI.Page
{
    DataSet dsDivision = null;
    DataSet dsState = null;
    DataSet dsProd = null;
    DataSet dsSalesForce = null;
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
    decimal SS_BR;
    decimal SS_CR;
	decimal VS_BR;
    decimal VS_CR;
    decimal target_amt;
    decimal distrbutor_discout_amt;
    decimal retailer_discount_amt;
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
            fillsubdivision();
            FillState(div_code);
            //menu1.FindControl("btnBack").Visible = false;
            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);
            txtEffFrom.Text = DateTime.Now.ToShortDateString();
            btnGo.Focus();
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
    private void fillsubdivision()
    {
        SalesForce sd = new SalesForce();
        dsSalesForce = sd.Getsubdivisionwise(div_code);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            DDL_div.DataTextField = "subdivision_name";
            DDL_div.DataValueField = "subdivision_code";
            DDL_div.DataSource = dsSalesForce;
            DDL_div.DataBind();
            DDL_div.Items.Insert(0, new ListItem("--Select--", "0"));

        }
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
        using (var scope = new TransactionScope())
        {
            try
            {
        System.Threading.Thread.Sleep(time);
        string State_Code = string.Empty;
        Product dv = new Product();
		loc h = new loc();
        int iReturn = -1;
        int iMaxState = 0;

        iMaxState = dv.getMaxStateSlNo(ddlState.SelectedValue, div_code);

        if (ddlState.SelectedItem.Text == "ALL")
        {
            iReturn = dv.DeleteProductRate(div_code);
        }
        else
        {
            iReturn = dv.DeleteProductRate(ddlState.SelectedValue, div_code, DDL_div.SelectedValue.ToString());
        }

        foreach (GridViewRow gridRow in GrdDoctor.Rows)
        {

            Label lblProdCode = (Label)gridRow.Cells[1].FindControl("lblProd_Code");
            prod_code = lblProdCode.Text;

            TextBox txtMRP = (TextBox)gridRow.Cells[1].FindControl("txtMRP");
            mrp_amt = Convert.ToDecimal(txtMRP.Text);

            TextBox txtRP = (TextBox)gridRow.Cells[1].FindControl("txtRP");
            ret_amt = Convert.ToDecimal(txtRP.Text);

            TextBox txtDP = (TextBox)gridRow.Cells[1].FindControl("txtDP");
            dist_amt = Convert.ToDecimal(txtDP.Text);

            TextBox txtNSR = (TextBox)gridRow.Cells[1].FindControl("txtNSR");
            nsr_amt = Convert.ToDecimal(txtNSR.Text);

            TextBox txtTarg = (TextBox)gridRow.Cells[1].FindControl("txtTarg");
            target_amt = Convert.ToDecimal(txtTarg.Text);

            TextBox txtdistri_disct = (TextBox)gridRow.Cells[1].FindControl("txtdist_dsc");
            distrbutor_discout_amt = Convert.ToDecimal(txtdistri_disct.Text);
            TextBox txtretl_disct = (TextBox)gridRow.Cells[1].FindControl("txtretl_dsc");
            retailer_discount_amt = Convert.ToDecimal(txtretl_disct.Text);


            TextBox TxtSSBR = (TextBox)gridRow.Cells[1].FindControl("TSS_Base_Rate");
            SS_BR = Convert.ToDecimal(TxtSSBR.Text);

            TextBox TxtSSCRP = (TextBox)gridRow.Cells[1].FindControl("TSS_Case_Rate");
            SS_CR = Convert.ToDecimal(TxtSSCRP.Text);
			
			TextBox TxtVSBR = (TextBox)gridRow.Cells[1].FindControl("txtvanp");
            VS_BR = Convert.ToDecimal(TxtVSBR.Text);

             TextBox TxtVSCRP = (TextBox)gridRow.Cells[1].FindControl("txtvanc");
             VS_CR = Convert.ToDecimal(TxtVSCRP.Text);


            // Update Division
            if (ddlState.SelectedItem.Text == "ALL")
            {
                DataSet dsstate = new DataSet();
                Product st = new Product();
                string[] strState;
                dsstate = st.getProduct_State(div_code, prod_code);
                if (dsstate.Tables[0].Rows.Count > 0)
                {
                    State_Code = dsstate.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

                }
                State_Code = State_Code.Remove(State_Code.Length - 1);
                strState = State_Code.Split(',');


                foreach (string state in strState)
                {
                    iReturn = h.UpdateProductRate(prod_code, state, txtEffFrom.Text, System.Math.Round(mrp_amt, 2), System.Math.Round(ret_amt, 2), System.Math.Round(dist_amt, 2), System.Math.Round(nsr_amt, 2), System.Math.Round(target_amt, 2), div_code, iMaxState, distrbutor_discout_amt, retailer_discount_amt, SS_BR, SS_CR, VS_BR, VS_CR);
                }
            }
            else
            {
                     iReturn = h.UpdateProductRate(prod_code, ddlState.SelectedValue.ToString(), txtEffFrom.Text, System.Math.Round(mrp_amt, 2), System.Math.Round(ret_amt, 2), System.Math.Round(dist_amt, 2), System.Math.Round(nsr_amt, 2), System.Math.Round(target_amt, 2), div_code, iMaxState, distrbutor_discout_amt, retailer_discount_amt, SS_BR, SS_CR, VS_BR, VS_CR);
            }
            if (iReturn > 0)
            {
                // menu1.Status = "Produce Rate Updated Successfully ";
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Updated Successfully');</script>");
            }
        }
        scope.Complete();
        scope.Dispose();
            }
            catch (Exception ex)
            {
                scope.Dispose();
                throw ex;
            }
        }

    }
    protected void btnGo_Click(object sender, EventArgs e)
    {

        System.Threading.Thread.Sleep(time);
        tblRate.Visible = true;
        FillProd();
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



            AddMergedCells(objgridviewrow, objtablecell, 0, "S.No", "#4697ce", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "Product Name", "#4697ce", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "UOM", "#4697ce", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "UOM Value", "#4697ce", true);
            AddMergedCells(objgridviewrow, objtablecell, 2, "Super Stockist", "#4697ce", true);
            AddMergedCells(objgridviewrow, objtablecell, 2, "Distributor Price", "#4697ce", true);
            AddMergedCells(objgridviewrow, objtablecell, 2, "Retailer Price", "#4697ce", true);  
 AddMergedCells(objgridviewrow, objtablecell, 2, "Vansale Price", "#4697ce", true);			
            AddMergedCells(objgridviewrow, objtablecell, 0, "MRP Rate", "#4697ce", true);            
            AddMergedCells(objgridviewrow, objtablecell, 2, "Scheme", "#4697ce", true);            
            //AddMergedCells(objgridviewrow, objtablecell, 0, "Total Listed Drs", "#DDEECC", true);
            objGridView.Controls[0].Controls.AddAt(0, objgridviewrow);
            GridViewRow objgridviewrow2 = new GridViewRow(2, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell objtablecell2 = new TableCell();
            for (int i = 0; i < 4; i++)
            {
                AddMergedCells(objgridviewrow2, objtablecell2, 0, "Base Rate", "#4697ce", false);
                AddMergedCells(objgridviewrow2, objtablecell2, 0, "Case Rate", "#4697ce", false);
            }
            AddMergedCells(objgridviewrow2, objtablecell2, 0, "Distributor Discount", "#4697ce", false);
            AddMergedCells(objgridviewrow2, objtablecell2, 0, "Retailer Discount", "#4697ce", false);
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
        Product dv = new Product();
        //dsProd = dv.getProdRate(ddlState.SelectedValue.ToString(), div_code);
        if (ddlState.SelectedItem.Text == "ALL")
        {
            dsProd = dv.getProductRate_all(div_code);
        }
        else
        {
            dsProd = dv.getProductRate(ddlState.SelectedValue.ToString(), div_code, DDL_div.SelectedValue.ToString());
        }

        DataSet DsAudit = dv.getProductRate_all(div_code);
        if (DsAudit.Tables[0].Rows.Count > 0 || Session["sf_type"].ToString() == "3")
        {
            dsProd = dv.getProductRate(ddlState.SelectedValue.ToString(), div_code, DDL_div.SelectedValue.ToString());

            GrdDoctor.DataSource = dsProd;
            GrdDoctor.DataBind();
            btnSubmit.Visible = true;
        }
    }

    protected void btn_Rate_Gen_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        Response.Redirect("ProductRate_New.aspx");
    }

    protected void btn_Rate_View_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        Response.Redirect("ProductRate_View.aspx");
    }
	
	
	public class loc
    {
        public int UpdateProductRate(string prod_code, string state_code, string effective_from, decimal mrp_amt, decimal ret_amt, decimal dist_amt, decimal nsr_amt, decimal target_amt, string div_code, int iStateSlNo, decimal distrbutor_discout_amt, decimal retailer_discount_amt, decimal SS_Base_Rate, decimal SS_Case_Rate, decimal VS_Base_Rate, decimal VS_Case_Rate)
        {
            int iReturn = -1;
            int iSlNo = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();
                string strQry = "SELECT ISNULL(MAX(Sl_No),0)+1 FROM mas_Product_State_Rates ";
                iSlNo = db.Exec_Scalar(strQry);
                string strQry1 = "INSERT INTO mas_Product_State_Rates (Sl_No, Max_State_Sl_No, State_Code, Product_Detail_Code, MRP_Price, Retailor_Price, " +
                         " Distributor_Price, Target_Price, NSR_Price, Effective_From_Date, Division_Code, Created_Date,LastUpdt_Date,Distributor_Discount_Price,Retailer_Discount_Price,SS_Base_Rate,SS_Case_Rate,VanSale_Price,vansale_caseprice) VALUES " +
                         " ( '" + iSlNo + "', '" + iStateSlNo + "', '" + state_code + "', '" + prod_code + "', '" + mrp_amt + "', '" + ret_amt + "', '" + dist_amt + "', " +
                         " '" + target_amt + "', '" + nsr_amt + "', '" + effective_from.Substring(6, 4) + "-" + effective_from.Substring(3, 2) + "-" + effective_from.Substring(0, 2) + "', '" + div_code + "', getdate(),getdate(),'" + distrbutor_discout_amt + "','" + retailer_discount_amt + "','" + SS_Base_Rate + "','" + SS_Case_Rate + "','" + VS_Base_Rate + "','" + VS_Case_Rate + "')";
                iReturn = db.ExecQry(strQry1);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }

	}

}