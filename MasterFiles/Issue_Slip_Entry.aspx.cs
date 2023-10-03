using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
using System.Data.SqlClient;

public partial class MasterFiles_Issue_Slip_Entry : System.Web.UI.Page
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
    string stk_type ;
    decimal ret_amt;
    decimal qty_amt;
    decimal val_amt;
    string res_txt;
    string effective_from = string.Empty;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int time;
    string mode = string.Empty;
    string grn_no = string.Empty;
    string grn_dt = string.Empty;
    string supp_code = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {

        div_code = Session["div_code"].ToString();
        if (!Page.IsPostBack)
        {
            
                FillMRManagers();
                Filldis();
                ServerStartTime = DateTime.Now;
                base.OnPreInit(e);
                txtEffFrom.Text = DateTime.Now.ToShortDateString();
                btnGo_Click(sender, e);
            

     
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
    protected void sf_Name_SelectedIndexChanged(object sender, EventArgs e)
    {
        getddlSF_Code();
       
    }
    private void getddlSF_Code()
    {
        TP_New tp = new TP_New();

        dsTP = tp.Get_Sf_By_Dis(div_code, From_dis.SelectedValue);
        if (dsTP.Tables[0].Rows.Count > 0)
        {
            ddldis.DataTextField = "Stockist_Name";
            ddldis.DataValueField = "Distributor_Code";
            ddldis.DataSource = dsTP;
            ddldis.DataBind();
            ddldis.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        else
        {
            Territory terr = new Territory();
            dsTP = terr.getSF_Code_distributor(div_code);
            if (dsTP.Tables[0].Rows.Count > 0)
            {
                ddldis.DataTextField = "stockist_Name";
                ddldis.DataValueField = "Stockist_Code";
                ddldis.DataSource = dsTP;
                ddldis.DataBind();

                if (Session["sf_code"] == null || Session["sf_code"].ToString() == "admin")
                {
                    ddldis.SelectedIndex = 0;
                }

            }
            else
            {
                ddldis.SelectedIndex = 0;
            }
        }

    }
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
   
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        string dis_Code = string.Empty;
        Product dv = new Product();
        int iReturn ;
        int iMaxState = 0;

        DataSet dsstate = new DataSet();
        Product st = new Product();
        string date = Convert.ToDateTime(txtEffFrom.Text).ToString("MM/dd/yyyy");
        iReturn = st.Insert_Issue_slip_head(Txt_Slip_no.Value, From_dis.SelectedItem.Value, From_dis.SelectedItem.Text, ddldis.SelectedItem.Value, ddldis.SelectedItem.Text, date, Txt_Slip_no.Value,div_code);
        if (iReturn > 0)
        {
        foreach (GridViewRow gridRow in GrdDoctor.Rows)
        {

            Label lblProdCode = (Label)gridRow.Cells[1].FindControl("lblProd_Code");
            prod_code = lblProdCode.Text;

            Label lblProdname = (Label)gridRow.Cells[1].FindControl("lblProdName");
            prod_name = lblProdname.Text;


            DropDownList txtStk = (DropDownList)gridRow.Cells[1].FindControl("Stoc_Type");
            stk_type = txtStk.SelectedItem.ToString();

            TextBox txtRP = (TextBox)gridRow.Cells[1].FindControl("txtDP");
            ret_amt = Convert.ToDecimal (txtRP.Text);

            TextBox txtqty = (TextBox)gridRow.Cells[1].FindControl("txtqty");
            qty_amt = Convert.ToDecimal(txtqty.Text);


            TextBox txtval = (TextBox)gridRow.Cells[1].FindControl("txtVal");
            string va = txtval.Text;
            val_amt = Convert.ToDecimal(va);

            TextBox txtres = (TextBox)gridRow.Cells[1].FindControl("txtRea");
            res_txt = txtres.Text;

            dsstate = st.Insert_Issue_slip_Dtl(iReturn, prod_code, prod_name, stk_type, ret_amt, qty_amt, val_amt, res_txt);
               
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
            dsProd = dv.getCloseRate2(ddldis.SelectedValue.ToString(), div_code, txtEffFrom.Text.Trim());
        }

        DataSet DsAudit = dv.getProductRate_all(div_code);
        if (DsAudit.Tables[0].Rows.Count > 0 || Session["sf_type"].ToString() == "3")
        {
            dsProd = dv.getCloseRate2(ddldis.SelectedValue.ToString(), div_code, txtEffFrom.Text.Trim());
           
            GrdDoctor.DataSource = dsProd;
            GrdDoctor.DataBind();
            btnSubmit.Visible = true;
        }
    }


   
}