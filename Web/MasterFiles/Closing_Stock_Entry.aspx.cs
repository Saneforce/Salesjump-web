using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
using System.Data.SqlClient;

public partial class MasterFiles_Closing_Stock_Entry : System.Web.UI.Page
{
    DataSet dsDivision = null;
    DataSet dsState = null;
    DataSet dsProd = null;
    DataSet dsTP = null;
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
            //FillMRManagers();
            Filldis();
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
   

    private void Filldis()
    {
        SalesForce sf = new SalesForce();

        string sub = string.Empty;
        dsDivision = sf.GetStockist_subdivisionwise(div_code, sub, "admin");

        if (dsDivision.Tables[0].Rows.Count > 0)
        {
            ddldis.DataTextField = "Stockist_Name";
            ddldis.DataValueField = "Stockist_code";
            ddldis.DataSource = dsDivision;
            ddldis.DataBind();

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
    
   
   
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        string dis_Code = string.Empty;
        Product dv = new Product();
        int iReturn = -1;
        int iMaxState = 0;

        //iMaxState = dv.getMaxStateSlNo(ddldis.SelectedValue, div_code);

        //if (ddldis.SelectedItem.Text == "ALL")
        //{
        //    iReturn = dv.DeleteProductRate(div_code);
        //}
        //else
        //{
        //    iReturn = dv.DeleteProductRate(ddldis.SelectedValue, div_code);
        //}

        foreach (GridViewRow gridRow in GrdDoctor.Rows)
        {

            Label lblProdCode = (Label)gridRow.Cells[1].FindControl("lblProd_Code");
            prod_code = lblProdCode.Text;

            Label lblProdname = (Label)gridRow.Cells[1].FindControl("lblProdName");
            prod_name = lblProdname.Text;


            Label txtMRP = (Label)gridRow.Cells[1].FindControl("txtMRP");
            mrp_amt = Convert.ToDecimal(txtMRP.Text);

            Label txtRP = (Label)gridRow.Cells[1].FindControl("txtDP");
            ret_amt = Convert.ToDecimal(txtRP.Text);

            TextBox txtDP = (TextBox)gridRow.Cells[1].FindControl("txtRP");
            dist_amt = Convert.ToDecimal(txtDP.Text);

            TextBox txtNSR = (TextBox)gridRow.Cells[1].FindControl("txtNSR");
            nsr_amt = Convert.ToDecimal(txtNSR.Text);

            DataSet dsstate = new DataSet();
            Product st = new Product();

            dsstate = st.SelectCloseRate(prod_code, ddldis.SelectedValue.ToString(), txtEffFrom.Text, System.Math.Round(mrp_amt, 2), System.Math.Round(ret_amt, 2), System.Math.Round(dist_amt, 2), System.Math.Round(nsr_amt, 2), div_code, iMaxState);
            if (dsstate.Tables[0].Rows.Count > 0)
            {

                iReturn = dv.UpdateProductRate(prod_code, ddldis.SelectedValue.ToString(), txtEffFrom.Text, System.Math.Round(mrp_amt, 2), System.Math.Round(ret_amt, 2), System.Math.Round(dist_amt, 2), System.Math.Round(nsr_amt, 2), div_code, iMaxState);
               
            }
            else
            {
                int Op_Qty = 0;
                int Sale_Qty = 0;
                int Rec_Qty = 0;
                int Retailor_Rate = 0;
                int sale_pieces = 0;
                int RP_BaseRate = 0;
                int OP_Pieces = 0;
                int RwFlg = 1;
                int Rec_Pieces = 0;

                string date = Convert.ToDateTime(txtEffFrom.Text).ToString("MM/dd/yyyy");

                iReturn = dv.InsertProductRate(prod_code, ddldis.SelectedValue.ToString(), date, prod_name, Op_Qty, Rec_Qty, System.Math.Round(dist_amt, 2), Sale_Qty, System.Math.Round(mrp_amt, 2), Retailor_Rate, System.Math.Round(nsr_amt, 2), sale_pieces, System.Math.Round(ret_amt, 2), RP_BaseRate, OP_Pieces, RwFlg, Rec_Pieces, "admin");
                //btnSubmit.Visible = false;
            }
            if (iReturn > 0)
            {
             
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Updated Successfully');</script>");
            }
        }

    }
    protected void btnGo_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        tblRate.Visible = true;
        FillProd();
    }

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



            AddMergedCells(objgridviewrow, objtablecell, 0, "S.No", "#ddd", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "Product Name", "#ddd", true);
            //AddMergedCells(objgridviewrow, objtablecell, 0, "UOM", "#DDEECC", true);
            //AddMergedCells(objgridviewrow, objtablecell, 0, "UOM Value", "#DDEECC", true);
            AddMergedCells(objgridviewrow, objtablecell, 2, "Rate", "#ddd", true);
            AddMergedCells(objgridviewrow, objtablecell, 2, "Closing", "#ddd", true);
            //AddMergedCells(objgridviewrow, objtablecell, 0, "MRP Rate", "#DDEECC", true);
            //AddMergedCells(objgridviewrow, objtablecell, 0, "Total Listed Drs", "#DDEECC", true);
            objGridView.Controls[0].Controls.AddAt(0, objgridviewrow);
            GridViewRow objgridviewrow2 = new GridViewRow(2, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell objtablecell2 = new TableCell();
            for (int i = 0; i < 2; i++)
            {
                AddMergedCells(objgridviewrow2, objtablecell2, 0, "Case", "#ddd", false);
                AddMergedCells(objgridviewrow2, objtablecell2, 0, "Pic.", "#ddd", false);
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
        Product dv = new Product();
      
        if (ddldis.SelectedItem.Text == "ALL")
        {
            dsProd = dv.getProductRate_all(div_code);
        }
        else
        {
            dsProd = dv.getCloseRate(ddldis.SelectedValue.ToString(), div_code, txtEffFrom.Text.Trim());
        }

        DataSet DsAudit = dv.getProductRate_all(div_code);
        if (DsAudit.Tables[0].Rows.Count > 0 || Session["sf_type"].ToString() == "3")
        {
            dsProd = dv.getCloseRate(ddldis.SelectedValue.ToString(), div_code, txtEffFrom.Text.Trim());
           
            GrdDoctor.DataSource = dsProd;
            GrdDoctor.DataBind();
            btnSubmit.Visible = true;
        }
    }


   
}