using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
using System.Data.SqlClient;

public partial class MasterFiles_Issue_Slip_Edit : System.Web.UI.Page
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
    string Div_Code = string.Empty;
    string SF_Code = string.Empty;
    string Sub_DivCode = string.Empty;
    string mode = string.Empty;
    string grn_no = string.Empty;
    string grn_dt = string.Empty;
    string supp_code = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {

        div_code = Session["div_code"].ToString();
        if (!Page.IsPostBack)
        {
            
            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);
            FillMRManagers();
            Filldis();
                mode = Request.QueryString["Mode"].ToString();
                if (mode == "1")
                {
                    Product dv = new Product();
                    grn_no = Request.QueryString["GRN_No"].ToString();
                    grn_dt = Request.QueryString["GRN_Date"].ToString();
                    txtEffFrom.Text = grn_dt;
                    supp_code = Request.QueryString["Supp_Code"].ToString();
                    dsProd = dv.getIssue_slip_Head(supp_code.ToString(), grn_no, div_code, grn_dt);
                    if (dsProd.Tables[0].Rows.Count > 0)
                    {
                        From_dis.SelectedValue = dsProd.Tables[0].Rows[0].ItemArray.GetValue(2).ToString().Trim();
                        ddldis.SelectedValue = dsProd.Tables[0].Rows[0].ItemArray.GetValue(4).ToString().Trim();
                        Txt_Slip_no.Value = dsProd.Tables[0].Rows[0].ItemArray.GetValue(7).ToString().Trim();
                        From_dis.Enabled = false;
                        ddldis.Enabled = false;
                        txtEffFrom.Enabled = false;
                    }
                }
            FillProd();
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
   
    private void FillProd()
    {
        Product dv = new Product();

        if (supp_code == " ")
        {
            //dsProd = dv.getProductRate_all(div_code);
        }
        else
        {
            dsProd = dv.getIssue_slip(supp_code.ToString(), grn_no, div_code, grn_dt);
        }

        DataSet DsAudit = dv.getProductRate_all(div_code);
        if (DsAudit.Tables[0].Rows.Count > 0 || Session["sf_type"].ToString() == "3")
        {
            dsProd = dv.getIssue_slip(supp_code.ToString(), grn_no, div_code, grn_dt);
           
            GrdDoctor.DataSource = dsProd;
            GrdDoctor.DataBind();
            //btnSubmit.Visible = false;
        }
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
    //public SortDirection dir
    //{
    //    get
    //    {
    //        if (ViewState["dirState"] == null)
    //        {
    //            ViewState["dirState"] = SortDirection.Ascending;
    //        }
    //        return (SortDirection)ViewState["dirState"];
    //    }
    //    set
    //    {
    //        ViewState["dirState"] = value;
    //    }
    //}
    //private DataTable BindGridView()
    //{
    //    DataTable dtGrid = new DataTable();
    //    Product dv = new Product();
    //    dtGrid = dv.getProductRatelist_DataTable(div_code);
    //    return dtGrid;
    //}

    private void FillMRManagers()
    {
        string sub = string.Empty;
        try
        {
            SalesForce sf = new SalesForce();
            dsDivision = sf.Get_Warehouse_Stockist_subdivisionwise(div_code, sub, "admin");
            if (dsDivision.Tables[0].Rows.Count > 0)
            {
                From_dis.DataTextField = "stockist_Name";
                From_dis.DataValueField = "Stockist_Code";
                From_dis.DataSource = dsDivision;
                From_dis.DataBind();



            }

        }
        catch (Exception)
        {

        }
    }

   
    //protected void btnSubmit_Click(object sender, EventArgs e)
    //{
    //    System.Threading.Thread.Sleep(time);
    //    string dis_Code = string.Empty;
    //    Product dv = new Product();
    //    int iReturn = -1;
    //    int iMaxState = 0;

    //    iMaxState = dv.getMaxStateSlNo(ddldis.SelectedValue, div_code);

    //    if (ddldis.SelectedItem.Text == "ALL")
    //    {
    //        iReturn = dv.DeleteProductRate(div_code);
    //    }
    //    else
    //    {
    //        iReturn = dv.DeleteProductRate(ddldis.SelectedValue, div_code);
    //    }

    //    foreach (GridViewRow gridRow in GrdDoctor.Rows)
    //    {

    //        Label lblProdCode = (Label)gridRow.Cells[1].FindControl("lblProd_Code");
    //        prod_code = lblProdCode.Text;

    //        Label txtMRP = (Label)gridRow.Cells[1].FindControl("txtMRP");
    //        mrp_amt = Convert.ToDecimal(txtMRP.Text);

    //        Label txtRP = (Label)gridRow.Cells[1].FindControl("txtDP");
    //        ret_amt = Convert.ToDecimal(txtRP.Text);

    //        TextBox txtDP = (TextBox)gridRow.Cells[1].FindControl("txtRP");
    //        dist_amt = Convert.ToDecimal(txtDP.Text);

    //        TextBox txtNSR = (TextBox)gridRow.Cells[1].FindControl("txtNSR");
    //        nsr_amt = Convert.ToDecimal(txtNSR.Text);

          

    //        // Update Division
    //        if (ddldis.SelectedItem.Text == "ALL")
    //        {
    //            DataSet dsstate = new DataSet();
    //            Product st = new Product();
    //            string[] strState;
    //            dsstate = st.getProduct_State(div_code, prod_code);
    //            if (dsstate.Tables[0].Rows.Count > 0)
    //            {
    //                dis_Code = dsstate.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

    //            }
    //            dis_Code = dis_Code.Remove(dis_Code.Length - 1);
    //            strState = dis_Code.Split(',');


    //            foreach (string state in strState)
    //            {
    //               iReturn = dv.UpdateProductRate(prod_code, state, txtEffFrom.Text,System.Math.Round(mrp_amt,2),System.Math.Round(ret_amt,2),System.Math.Round(dist_amt,2),System.Math.Round(nsr_amt,2), div_code, iMaxState);
    //            }
    //        }
    //        else
    //        {
    //            iReturn = dv.UpdateProductRate(prod_code, ddldis.SelectedValue.ToString(), txtEffFrom.Text, System.Math.Round(mrp_amt, 2), System.Math.Round(ret_amt, 2), System.Math.Round(dist_amt, 2), System.Math.Round(nsr_amt, 2), div_code, iMaxState);
    //        }
    //        if (iReturn > 0)
    //        {
    //            // menu1.Status = "Produce Rate Updated Successfully ";
    //            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Updated Successfully');</script>");
    //        }
    //    }

    //}
    //protected void btnGo_Click(object sender, EventArgs e)
    //{
    //    System.Threading.Thread.Sleep(time);
    //    tblRate.Visible = true;
    //    FillProd();
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


  

    



    //protected void sf_Name_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    getddlSF_Code();
    //}
    //private void getddlSF_Code()
    //{
    //    TP_New tp = new TP_New();

    //    dsTP = tp.Get_Sf_By_Dis(div_code, From_dis.SelectedValue);
    //    if (dsTP.Tables[0].Rows.Count > 0)
    //    {
    //        ddldis.DataTextField = "Stockist_Name";
    //        ddldis.DataValueField = "Distributor_Code";
    //        ddldis.DataSource = dsTP;
    //        ddldis.DataBind();
    //        ddldis.Items.Insert(0, new ListItem("--Select--", "0"));
    //    }
    //    else
    //    {
    //        Territory terr = new Territory();
    //        dsTP = terr.getSF_Code_distributor(div_code);
    //        if (dsTP.Tables[0].Rows.Count > 0)
    //        {
    //            ddldis.DataTextField = "stockist_Name";
    //            ddldis.DataValueField = "Stockist_Code";
    //            ddldis.DataSource = dsTP;
    //            ddldis.DataBind();

    //            if (Session["sf_code"] == null || Session["sf_code"].ToString() == "admin")
    //            {
    //                ddldis.SelectedIndex = 0;
    //            }

    //        }
    //        else
    //        {
    //            ddldis.SelectedIndex = 0;
    //        }
    //    }

    //}
    protected void GrdDoctor_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //Find the DropDownList in the Row
            DataRowView row = (DataRowView)e.Row.DataItem;

            Product dv = new Product();
            dsProd = dv.getIssue_slip_Details(supp_code.ToString(), grn_no, div_code, grn_dt);
            if (dsProd.Tables[0].Rows.Count > 0)
            {
                DropDownList ddlCountries = (e.Row.FindControl("Stoc_Type") as DropDownList);

                ddlCountries.DataSource = dsProd;
                ddlCountries.DataTextField = "Stock_Type";
                ddlCountries.DataBind();
                ddlCountries.SelectedIndex = ddlCountries.Items.IndexOf(ddlCountries.Items.FindByText(row["Stock_Type"].ToString()));
            }

          
        }
        
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        Product dv = new Product();

        foreach (GridViewRow gridRow in GrdDoctor.Rows)
        {

            Label lblProdCode = (Label)gridRow.Cells[1].FindControl("lblProd_Code");
            prod_code = lblProdCode.Text;

            Label lblProdname = (Label)gridRow.Cells[1].FindControl("lblProdName");
            prod_name = lblProdname.Text;


            DropDownList txtStk = (DropDownList)gridRow.Cells[1].FindControl("Stoc_Type");
            string stk_type = txtStk.SelectedItem.ToString();

            TextBox txtRP = (TextBox)gridRow.Cells[1].FindControl("LabDP");
            ret_amt = Convert.ToDecimal(txtRP.Text);

            TextBox txtqty = (TextBox)gridRow.Cells[1].FindControl("Labqty");
            decimal qty_amt = Convert.ToDecimal(txtqty.Text);


            TextBox txtval = (TextBox)gridRow.Cells[1].FindControl("LabVal");
            string va = txtval.Text;
            decimal  val_amt = Convert.ToDecimal(va);

            TextBox txtres = (TextBox)gridRow.Cells[1].FindControl("LabRea");
            string res_txt = txtres.Text;

            int r = dv.getIssue_slip_Update(Request.QueryString["GRN_No"].ToString(), prod_code, prod_name, stk_type, ret_amt, qty_amt, val_amt, res_txt);

            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Updated Successfully');</script>");
        }
       
        

    }
}