using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;

public partial class MasterFiles_MR_ProductRate : System.Web.UI.Page
{
    DataSet dsSF = null;
    DataSet dsProd = null;
    string sf_code = string.Empty;
    string div_code = string.Empty;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int time;
    string strMultiDiv = string.Empty;
    Product prd = new Product();
    DataSet dsdiv = new DataSet();
    DataSet dsState = new DataSet();
    string state_code = string.Empty;
    string sub_code = string.Empty;
    string sState = string.Empty;
    DataSet dsDivision = null;    
    string[] statecd;
    string state_cd = string.Empty;
    DataSet dsSub = null;
    protected void Page_Load(object sender, EventArgs e)
    {
       
        sf_code = Session["sf_code"].ToString();
        div_code = Session["div_code"].ToString();
        if (!Page.IsPostBack)
        {
           
           // FillProd();
           // menu1.Title = this.Page.Title;
           // menu1.FindControl("btnBack").Visible = false;
            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);
            Product prd = new Product();
            DataSet dsdiv = new DataSet();
            dsdiv = prd.getMultiDivsf_Name(sf_code);
            lblSelect.Visible = true;
            if (dsdiv.Tables[0].Rows.Count > 0)
            {
                if (dsdiv.Tables[0].Rows[0]["IsMultiDivision"].ToString() == "1")
                {
                    strMultiDiv = dsdiv.Tables[0].Rows[0][1].ToString().Remove(dsdiv.Tables[0].Rows[0][1].ToString().Length - 1, 1);
                    ddlDivision.Visible = true;
                    lblDivision.Visible = true;
                    getDivision();
                }
                else
                {
                    btnGo.Visible = false;
                    lblSelect.Visible = false;
                    ddlDivision.Visible = false;
                    lblDivision.Visible = false;
                    FillProd();
                }
            }
            if (Session["sf_type"].ToString() == "" || Session["sf_type"].ToString() == "3")
            {
                pnlstate.Visible = true;
                FillState(div_code);
                // ddlState.SelectedIndex = 0;
            }
        }
        if (Session["sf_type"].ToString() == "1")
        {

            //UserControl_MR_Menu c1 =
            //    (UserControl_MR_Menu)LoadControl("~/UserControl/MR_Menu.ascx");
            //Divid.Controls.Add(c1);
            //c1.FindControl("btnBack").Visible = false;
            //c1.Title = this.Page.Title;
            pnldivision.Visible = false;


        }
        else if (Session["sf_type"].ToString() == "2")
        {
            //UserControl_MGR_Menu c1 =
            //(UserControl_MGR_Menu)LoadControl("~/UserControl/MGR_Menu.ascx");
            //Divid.Controls.Add(c1);
            //c1.FindControl("btnBack").Visible = false;
            //c1.Title = this.Page.Title;
            pnldivision.Visible = true;
         
        }
        else if (Session["sf_type"].ToString() == "" || Session["sf_type"].ToString() == "3")
        {
            div_code = Session["div_code"].ToString();
            if (div_code.Contains(','))
            {
                div_code = div_code.Remove(div_code.Length - 1);
            }
           // UserControl_MenuUserControl c1 =
           //(UserControl_MenuUserControl)LoadControl("~/UserControl/MenuUserControl.ascx");
           // Divid.Controls.Add(c1);
           // c1.Title = Page.Title;
           // c1.FindControl("btnBack").Visible = false;
            pnldivision.Visible = false;
            pnlstate.Visible = true;
            GrdDoctor.Visible = false;
            pnlprint.Visible = false;
            //  FillState(div_code);
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
            dsState = st.getState_new(state_cd);
            ddlState.DataTextField = "statename";
            ddlState.DataValueField = "state_code";
            ddlState.DataSource = dsState;
            ddlState.DataBind();
        }
    }
    
    private void getDivision()
    {
        Division dv = new Division();
        DataSet dsDivision = new DataSet();
        dsDivision = dv.getMultiDivision(strMultiDiv);
        if (dsDivision.Tables[0].Rows.Count > 0)
        {
            ddlDivision.DataTextField = "Division_Name";
            ddlDivision.DataValueField = "Division_Code";
            ddlDivision.DataSource = dsDivision;
            ddlDivision.DataBind();
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
    protected void grdProduct_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblsam1 = (Label)e.Row.FindControl("lblsam1");
            if (lblsam1 != null)
            {
                decimal mrp_price = Convert.ToDecimal(lblsam1.Text.ToString().Trim());
                lblsam1.Text = String.Format("{0:F2}", mrp_price);
            }

            Label lblsam2 = (Label)e.Row.FindControl("lblsam2");
            if (lblsam2 != null)
            {
                decimal ret_price = Convert.ToDecimal(lblsam2.Text.ToString().Trim());
                lblsam2.Text = String.Format("{0:F2}", ret_price);
            }

            Label lblsam3 = (Label)e.Row.FindControl("lblsam3");
            if (lblsam3 != null)
            {
                decimal dist_price = Convert.ToDecimal(lblsam3.Text.ToString().Trim());
                lblsam3.Text = String.Format("{0:F2}", dist_price);                
            }

            Label lblsam4 = (Label)e.Row.FindControl("lblsam4");
            if (lblsam4 != null)
            {
                decimal targ_price = Convert.ToDecimal(lblsam4.Text.ToString().Trim());
                lblsam4.Text = String.Format("{0:F2}", targ_price);
            }

        }
    }

    //private void FillProd()
    //{        

        //dsdiv = prd.getMultiDivsf_Name(sf_code);
        //if (dsdiv.Tables[0].Rows.Count > 0)
        //{
        //    if (dsdiv.Tables[0].Rows[0]["IsMultiDivision"].ToString() == "1")
        //    {
        //        div_code = ddlDivision.SelectedValue;
        //    }

        //}
        //UnListedDR LstDR = new UnListedDR();
        //if (Session["sf_type"].ToString() == "" || Session["sf_type"].ToString() == "3")
        //{
        //    state_code = ddlState.SelectedValue.ToString();
        //}
        //else
        //{
        //    dsState = LstDR.getState(sf_code);
        //    if (dsState.Tables[0].Rows.Count > 0)
        //    {
        //        state_code = dsState.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
        //    }
        //}
        //SubDivision sb = new SubDivision();
        //dsSub = sb.getSub_sf(sf_code);
        //if (dsSub.Tables[0].Rows.Count > 0)
        //{
        //    sub_code = dsSub.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
        //}
        //Product dv = new Product();

        //dsSF = dv.getProductRate_sf(sf_code, div_code, state_code, sub_code);

        //if (dsSF.Tables[0].Rows.Count > 0)
        //{
        //    GrdDoctor.Visible = true;
        //    GrdDoctor.DataSource = dsSF;
        //    GrdDoctor.DataBind();
        //}
        //else
        //{
        //    GrdDoctor.DataSource = dsSF;
        //    GrdDoctor.DataBind();
        //}
    //}

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
        dtGrid = dv.getProductRate_DataTable(sf_code, div_code);
        return dtGrid;
    }

    //protected void grdProduct_Sorting(object sender, GridViewSortEventArgs e)
    //{

    //    string sortingDirection = string.Empty;
    //    if (dir == SortDirection.Ascending)
    //    {
    //    dir = SortDirection.Descending;
    //    sortingDirection = "Desc";
    //    }
    //    else
    //    {
    //    dir = SortDirection.Ascending;
    //    sortingDirection = "Asc";
    //    }

    //    DataView sortedView = new DataView(BindGridView());
    //    sortedView.Sort = e.SortExpression + " " + sortingDirection;
    //    GrdDoctor.DataSource = sortedView;
    //    GrdDoctor.DataBind();
    //}

    protected void grdProduct_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GrdDoctor.PageIndex = e.NewPageIndex;
        FillProd();
    }
    protected void btnGo_Click(object sender, EventArgs e)
    {     

        if (Session["sf_type"].ToString() == "1")
        {
            lblSelect.Visible = false;
            FillProd();
        }
        else if (Session["sf_type"].ToString() == "2")
        {
            lblSelect.Visible = false;
            FillProd();
        }
    }
    protected void btnstate_Click(object sender, EventArgs e)
    {
        GrdDoctor.Visible = true;
        pnlprint.Visible = true;
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
        Product dv = new Product();
        dsdiv = prd.getMultiDivsf_Name(sf_code);
        if (dsdiv.Tables[0].Rows.Count > 0)
        {
            if (dsdiv.Tables[0].Rows[0]["IsMultiDivision"].ToString() == "1")
            {
                div_code = ddlDivision.SelectedValue;
            }

        }
        UnListedDR LstDR = new UnListedDR();
        if (Session["sf_type"].ToString() == "" || Session["sf_type"].ToString() == "3")
        {
            state_code = ddlState.SelectedValue.ToString();
        }
        else
        {
            dsState = LstDR.getState(sf_code);
            if (dsState.Tables[0].Rows.Count > 0)
            {
                state_code = dsState.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
            }
        }

        DataSet DsAudit = dv.getProductRate_all(div_code);
        if (DsAudit.Tables[0].Rows.Count > 0 || Session["sf_type"].ToString() == "3")
        {
            dsProd = dv.getProductRate(ddlState.SelectedValue.ToString(), div_code);

            GrdDoctor.DataSource = dsProd;
            GrdDoctor.DataBind();
          
        }
    }

}