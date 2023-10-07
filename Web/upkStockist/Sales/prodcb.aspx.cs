using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bus_EReport;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Services;
using Newtonsoft.Json;
using System.Globalization;

public partial class Stockist_Sales_prodcb : System.Web.UI.Page
{
    DataSet dsDoctor = null;
    string sMode = string.Empty;
    string sf_code = string.Empty;
    string FMonth = string.Empty;
    string FYear = string.Empty;
    string TMonth = string.Empty;
    string TYear = string.Empty;
    string sReturn = string.Empty;
    string Stok_code = string.Empty;
    string StName = string.Empty;

    string div_code = string.Empty;
    DataSet dsSalesForce = null;
    DataSet dsUserList = null;
    DataSet dsDoc = null;
    DataSet dsDCR = null;
    int product_total = 0;
    int FWD_total = 0;
    DateTime dtCurrent;
    DataSet dsProduct = null;
    DataSet dsCatg = null;
    int MonColspan = 0;
    DataSet dsTP = null;
    string tot_dr = string.Empty;
    string sCurrentDate = string.Empty;
    DataSet dsSf = null;
    string sf_type = string.Empty;
    DateTime ServerStartTime;
    DateTime ServerEndTime;

    DataSet dsSal = new DataSet();
    string strFieledForceName = string.Empty;
    DataTable dtrowClr = new System.Data.DataTable();
    SalesForce dcrdoc = new SalesForce();
    string strCase = string.Empty;
    string stockist = string.Empty;
    string fDate = string.Empty;
    string tDate = string.Empty;

    List<int> iLstVstCnt = new List<int>();
    List<int> iLstVstmnt = new List<int>();
    List<int> iLstVstyr = new List<int>();
    List<string> istock = new List<string>();
    DataSet dsSub = new DataSet();
    string sub_code = string.Empty;
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
        else if (sf_type == "4")
        {
            this.MasterPageFile = "~/Master_DIS.master";
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        sf_code = Session["sf_code"].ToString();
        sf_type = Session["sf_type"].ToString();

        if (sf_type == "1" || sf_type == "2")
        {
            sf_code = Session["Title_MR"].ToString();
            div_code = Session["div_code"].ToString();
           

        }
        else
        {


        }

        if (sf_type == "3")
        {
            div_code = Session["div_code"].ToString();
        }
        else
        {
            div_code = Session["div_code"].ToString();
        }
        if (!Page.IsPostBack)
        {

            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);

            BindStockiest();

        }

    }

    private void BindStockiest()
    {
        string Stockist_Code = HttpContext.Current.Session["Sf_Code"].ToString();
        string Div_Code = HttpContext.Current.Session["div_code"].ToString();
        string DSub_DivCode = string.Empty;
        DataSet dspono = null;
        StockistMaster sm = new StockistMaster();
        dspono = sm.getdist(Div_Code.TrimEnd(','));
        if (dspono.Tables[0].Rows.Count > 0)
        {
            idpro.DataSource = dspono;
            idpro.DataTextField = "Stockist_Name";
            idpro.DataValueField = "Stockist_Code";
            idpro.DataBind();
            idpro.Items.Insert(0, new ListItem("---SELECT---", "0"));
        }
    }
    
    protected void btnSF_Click(object sender, EventArgs e)
    {       
            stockist = prod.Value;
            fDate = fdt.Value;
            tDate = tdt.Value;

        BindProduct(stockist, fDate, tDate);
        BindProductc(stockist, fDate, tDate);
    }

    private void BindProduct(string stockist, string fDate, string tDate)
    {
        SalesForce sf = new SalesForce();
        DCR dcc = new DCR();
        //DB_EReporting db = new DB_EReporting();
        string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["Ereportcon"].ToString();
        SqlConnection con = new SqlConnection(strConn);
        con.Open();
        SqlCommand cmd = new SqlCommand("Ord_Product_Daywise", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Dep", stockist);
        cmd.Parameters.AddWithValue("@div", Convert.ToInt32(div_code));
        cmd.Parameters.AddWithValue("@fdt", fDate);
        cmd.Parameters.AddWithValue("@tdt", tDate);

        // cmd.Parameters.AddWithValue("@MnthTbl", dtMnYr);
        
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet dsts = new DataSet();
        da.Fill(dsts);
        con.Close();
        dtrowClr = dsts.Tables[1].Copy();
        //dsts.Tables[1].Columns.RemoveAt(5);
        //dsts.Tables[1].Columns.RemoveAt(1);
        //dsts.Tables[1].Columns.RemoveAt(0);
        GrdFixation.DataSource = dsts.Tables[1];
        GrdFixation.DataBind();

    }
    private void BindProductc(string stockist, string fDate, string tDate)
    {
        SalesForce sf = new SalesForce();
        DCR dcc = new DCR();
        //DB_EReporting db = new DB_EReporting();
        string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["Ereportcon"].ToString();
        SqlConnection con = new SqlConnection(strConn);
        con.Open();
        SqlCommand cmd = new SqlCommand("Ord_Product_Daywisec", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Dep", stockist);
        cmd.Parameters.AddWithValue("@div", Convert.ToInt32(div_code));
        cmd.Parameters.AddWithValue("@fdt", fDate);
        cmd.Parameters.AddWithValue("@tdt", tDate);

        // cmd.Parameters.AddWithValue("@MnthTbl", dtMnYr);

        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet dsts = new DataSet();
        da.Fill(dsts);
        con.Close();
        dtrowClr = dsts.Tables[1].Copy();
        //dsts.Tables[1].Columns.RemoveAt(5);
        //dsts.Tables[1].Columns.RemoveAt(1);
        //dsts.Tables[1].Columns.RemoveAt(0);
        GrdFixation1.DataSource = dsts.Tables[1];
        GrdFixation1.DataBind();

    }
    protected void GrdFixation_RowCreated1(object sender, GridViewRowEventArgs e)
    {
        SalesForce sf = new SalesForce();
        DataSet dsSalesForce = new DataSet();
        string Sf_Code = string.Empty;
        if (e.Row.RowType == DataControlRowType.Header)
        {
            //Creating a gridview object            
            GridView objGridView = (GridView)sender;

            //Creating a gridview row object
            GridViewRow objgridviewrow = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);

            //Creating a table cell object
            TableCell objtablecell = new TableCell();
            GridViewRow objgridviewrow2 = new GridViewRow(2, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell objtablecell2 = new TableCell();
            GridViewRow objgridviewrow3 = new GridViewRow(3, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell objtablecell3 = new TableCell();
            #region Merge cells

            AddMergedCells(objgridviewrow, objtablecell, 3, 0, "Day", "#0097AC", true);


            SalesForce sf1 = new SalesForce();
            DCR dc = new DCR();

            Stockist objStock = new Stockist();

            dsDoctor = objStock.Ord_Product_Daywisec(stockist, div_code, fDate, tDate);
            SecSale sc = new SecSale();

            foreach (DataRow dtRow in dsDoctor.Tables[0].Rows)
            {

                AddMergedCells(objgridviewrow2, objtablecell2, 0, 2, dtRow["prod_name"].ToString(), "#0097AC", false);

                AddMergedCells(objgridviewrow3, objtablecell3, 0, 0, "Sale", "#0097AC", false);
                AddMergedCells(objgridviewrow3, objtablecell3, 0, 0, "Cum", "#0097AC", false);

            }
            objGridView.Controls[0].Controls.AddAt(0, objgridviewrow);

            objGridView.Controls[0].Controls.AddAt(1, objgridviewrow2);
            objGridView.Controls[0].Controls.AddAt(2, objgridviewrow3);


            #endregion
        }
    }
    protected void GrdFixation_RowCreated(object sender, GridViewRowEventArgs e)
    {
        SalesForce sf = new SalesForce();
        DataSet dsSalesForce = new DataSet();
        string Sf_Code = string.Empty;
        if (e.Row.RowType == DataControlRowType.Header)
        {
            //Creating a gridview object            
            GridView objGridView = (GridView)sender;

            //Creating a gridview row object
            GridViewRow objgridviewrow = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);

            //Creating a table cell object
            TableCell objtablecell = new TableCell();
            GridViewRow objgridviewrow2 = new GridViewRow(2, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell objtablecell2 = new TableCell();
            GridViewRow objgridviewrow3 = new GridViewRow(3, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell objtablecell3 = new TableCell();
            #region Merge cells

            AddMergedCells(objgridviewrow, objtablecell, 3, 0, "Day", "#0097AC", true);
            

            SalesForce sf1 = new SalesForce();
            DCR dc = new DCR();

            Stockist objStock = new Stockist();

            dsDoctor = objStock.Ord_Product_Daywise(stockist, div_code, fDate, tDate);
            SecSale sc = new SecSale();

            foreach (DataRow dtRow in dsDoctor.Tables[0].Rows)
            {

                AddMergedCells(objgridviewrow2, objtablecell2, 0, 3, dtRow["prod_name"].ToString(), "#0097AC", false);

                AddMergedCells(objgridviewrow3, objtablecell3, 0, 0, "Sale", "#0097AC", false);
                AddMergedCells(objgridviewrow3, objtablecell3, 0, 0, "Cum", "#0097AC", false);
                AddMergedCells(objgridviewrow3, objtablecell3, 0, 0, "CB", "#0097AC", false);

            }
            objGridView.Controls[0].Controls.AddAt(0, objgridviewrow);

            objGridView.Controls[0].Controls.AddAt(1, objgridviewrow2);
            objGridView.Controls[0].Controls.AddAt(2, objgridviewrow3);
            

            #endregion
        }
    }
    protected void AddMergedCells(GridViewRow objgridviewrow, TableCell objtablecell, int rowSpan, int colspan, string celltext, string backcolor, bool bRowspan)
    {
        objtablecell = new TableCell();
        objtablecell.Text = celltext;
        objtablecell.ColumnSpan = colspan;
        if ((colspan == 0) && bRowspan)
        {
            objtablecell.RowSpan = 3;
        }
        objtablecell.Style.Add("background-color", backcolor);
        objtablecell.Style.Add("color", "white");
        objtablecell.Style.Add("border-color", "black");
        objtablecell.HorizontalAlign = HorizontalAlign.Center;
        objgridviewrow.Cells.Add(objtablecell);
    }
    protected void GrdFixation_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int indx = e.Row.RowIndex;
            int k = e.Row.Cells.Count - 5;
            //
            e.Row.Cells[1].Wrap = false;
            #region Calculations



            for (int i = 3, j = 0; i < e.Row.Cells.Count; i++)
            {

                e.Row.Cells[i].CssClass = "";
                e.Row.Cells[i].Attributes.Add("align", "Right");
            }


            #endregion
            //
            //if (dtrowClr.Rows[indx][1].ToString() == "Grand Total")
            //{
            //    e.Row.Attributes.Add("style", "background-color:LightBlue;font-bold:true; font-size:14px; Color:Red; border-color:Black");
            //    e.Row.Cells[0].Text = "";
            //    for (int l = 3, j = 0; l < e.Row.Cells.Count; l++)
            //    {
            //        e.Row.Cells[l].Text = "";

            //        l += 1;
            //    }

            //}
            //for (int l = 4, j = 0; l < e.Row.Cells.Count; l++)
            //{
            //    if (e.Row.RowIndex == dtrowClr.Rows.Count - 1)
            //    {
            //        e.Row.Cells[l].Text = dtrowClr.Rows[dtrowClr.Rows.Count - 1][l].ToString();
            //        //  e.Row.Cells[l + 1].Text = dtrowClr.Rows[dtrowClr.Rows.Count - 1][l + 1].ToString();
            //       // e.Row.Cells[l].Attributes.Add("style", "color:red;font-weight:normal;");
            //        //   e.Row.Cells[l + 1].Attributes.Add("style", "color:red;font-weight:normal;");


            //    }
            //    //  Double app = (e.Row.Cells[l + 1].Text == "") || (e.Row.Cells[l + 1].Text == "&nbsp;") || (e.Row.Cells[l + 1].Text == "-") ? 0 : Convert.ToDouble(e.Row.Cells[l + 1].Text);
            //    Double appl = (e.Row.Cells[l].Text == "") || (e.Row.Cells[l].Text == "&nbsp;") || (e.Row.Cells[l].Text == "-") ? 0 : Convert.ToDouble(e.Row.Cells[l].Text);

            //    // Double pApprT = (dtrowClr.Rows[dtrowClr.Rows.Count - 1][l + 1] == null) || (dtrowClr.Rows[dtrowClr.Rows.Count - 1][l + 1].ToString() == "") ? 0 : Convert.ToDouble(dtrowClr.Rows[dtrowClr.Rows.Count - 1][l + 1].ToString());
            //    Double pApplT = (dtrowClr.Rows[dtrowClr.Rows.Count - 1][l] == null) || (dtrowClr.Rows[dtrowClr.Rows.Count - 1][l].ToString() == "") ? 0 : Convert.ToDouble(dtrowClr.Rows[dtrowClr.Rows.Count - 1][l].ToString());


            //    // dtrowClr.Rows[dtrowClr.Rows.Count - 1][l + 1] = (pApprT + app).ToString();
            //    dtrowClr.Rows[dtrowClr.Rows.Count - 1][l] = (pApplT + appl).ToString();
            //    l++;

            //    j++;
            //}
            for (int i = 3; i < e.Row.Cells.Count; i++)
            {

                if (e.Row.Cells[i].Text == "0" || e.Row.Cells[i].Text == "-")
                {
                    e.Row.Cells[i].Text = "";
                    e.Row.Cells[i].Attributes.Add("style", "color:black;font-weight:normal;");
                }
                // e.Row.Cells[i].Attributes.Add("align", "center");
                e.Row.Cells[i].Attributes.Add("align", "Right");
            }

        }


    }
    protected void GrdFixation_RowDataBound1(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int indx = e.Row.RowIndex;
            int k = e.Row.Cells.Count - 5;
            //
            e.Row.Cells[1].Wrap = false;
            #region Calculations



            for (int i = 3, j = 0; i < e.Row.Cells.Count; i++)
            {

                e.Row.Cells[i].CssClass = "";
                e.Row.Cells[i].Attributes.Add("align", "Right");
            }


            #endregion
            //
            //if (dtrowClr.Rows[indx][1].ToString() == "Grand Total")
            //{
            //    e.Row.Attributes.Add("style", "background-color:LightBlue;font-bold:true; font-size:14px; Color:Red; border-color:Black");
            //    e.Row.Cells[0].Text = "";
            //    for (int l = 3, j = 0; l < e.Row.Cells.Count; l++)
            //    {
            //        e.Row.Cells[l].Text = "";

            //        l += 1;
            //    }

            //}
            //for (int l = 4, j = 0; l < e.Row.Cells.Count; l++)
            //{
            //    if (e.Row.RowIndex == dtrowClr.Rows.Count - 1)
            //    {
            //        e.Row.Cells[l].Text = dtrowClr.Rows[dtrowClr.Rows.Count - 1][l].ToString();
            //        //  e.Row.Cells[l + 1].Text = dtrowClr.Rows[dtrowClr.Rows.Count - 1][l + 1].ToString();
            //        // e.Row.Cells[l].Attributes.Add("style", "color:red;font-weight:normal;");
            //        //   e.Row.Cells[l + 1].Attributes.Add("style", "color:red;font-weight:normal;");


            //    }
            //    //  Double app = (e.Row.Cells[l + 1].Text == "") || (e.Row.Cells[l + 1].Text == "&nbsp;") || (e.Row.Cells[l + 1].Text == "-") ? 0 : Convert.ToDouble(e.Row.Cells[l + 1].Text);
            //    Double appl = (e.Row.Cells[l].Text == "") || (e.Row.Cells[l].Text == "&nbsp;") || (e.Row.Cells[l].Text == "-") ? 0 : Convert.ToDouble(e.Row.Cells[l].Text);

            //    // Double pApprT = (dtrowClr.Rows[dtrowClr.Rows.Count - 1][l + 1] == null) || (dtrowClr.Rows[dtrowClr.Rows.Count - 1][l + 1].ToString() == "") ? 0 : Convert.ToDouble(dtrowClr.Rows[dtrowClr.Rows.Count - 1][l + 1].ToString());
            //    Double pApplT = (dtrowClr.Rows[dtrowClr.Rows.Count - 1][l] == null) || (dtrowClr.Rows[dtrowClr.Rows.Count - 1][l].ToString() == "") ? 0 : Convert.ToDouble(dtrowClr.Rows[dtrowClr.Rows.Count - 1][l].ToString());


            //    // dtrowClr.Rows[dtrowClr.Rows.Count - 1][l + 1] = (pApprT + app).ToString();
            //    dtrowClr.Rows[dtrowClr.Rows.Count - 1][l] = (pApplT + appl).ToString();
            //    l++;

            //    j++;
            //}
            for (int i = 3; i < e.Row.Cells.Count; i++)
            {

                if (e.Row.Cells[i].Text == "0" || e.Row.Cells[i].Text == "-")
                {
                    e.Row.Cells[i].Text = "";
                    e.Row.Cells[i].Attributes.Add("style", "color:black;font-weight:normal;");
                }
                // e.Row.Cells[i].Attributes.Add("align", "center");
                e.Row.Cells[i].Attributes.Add("align", "Right");
            }

        }


    }
}
