using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
using System.IO;
using System.Drawing;
using System.Configuration;
using System.Data.SqlClient;
using ClosedXML.Excel;

public partial class MasterFiles_DistributorList : System.Web.UI.Page
{
    #region "Declaration"
    DataSet dsSalesForce = null;
    DataSet dsStockist = null;
    DataSet dsTerritory = null;
    string divcode = string.Empty;
    string stockist_code = string.Empty;
    string stockist_name = string.Empty;
    string stockist_Address = string.Empty;
    string stockist_ContactPerson = string.Empty;
    string stockist_Designation = string.Empty;
    string stockist_mobileno = string.Empty;
    string Territory = string.Empty;
    string sf_code = string.Empty;
    string sCmd = string.Empty;
    string sChkSalesforce = string.Empty;
    string div_code = string.Empty;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int time;
    #endregion
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
        divcode = Convert.ToString(Session["div_code"]);
        if (!Page.IsPostBack)
        {
            Session["GetcmdArgChar"] = "All";
            FillStockist();
            FillReporting();
            FillSF_Alpha();
            //menu1.Title = this.Page.Title;
            //menu1.FindControl("btnBack").Visible = false;
            getWorkName();
            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);
        }
    }
    private void FillReporting()
    {
        SalesForce sf = new SalesForce();
        dsSalesForce = sf.UserList_getMR(divcode, "admin");
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            ddlFilter.DataTextField = "Sf_Name";
            ddlFilter.DataValueField = "Sf_Code";
            ddlFilter.DataSource = dsSalesForce;
            ddlFilter.DataBind();
            ddlFilter.Items.Insert(0, new ListItem("--Select--", "0"));
            //ddlSF.DataTextField = "des_color";
            //ddlSF.DataValueField = "sf_code";
            //ddlSF.DataSource = dsSalesForce;
            //ddlSF.DataBind();
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
    private void getWorkName()
    {
        Territory terr = new Territory();
        dsTerritory = terr.getWorkAreaName(div_code);
        if (dsTerritory.Tables[0].Rows.Count > 0)
        {
            string str = dsTerritory.Tables[0].Rows[0]["wrk_area_Name"].ToString();
            ddlFields.Items.Add(new ListItem(str, "Territory Name", true));
            //CblDoctorCode.Items.Add(new ListItem(dsTerritory.Tables[0].Rows[0]["wrk_area_Name"].ToString(), "Territory_Code", true));

        }
    }
    protected void grdStockist_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            Territory terr = new Territory();
            DataSet dsTerritory = new DataSet();
            dsTerritory = terr.getWorkAreaName(div_code);
            if (dsTerritory.Tables[0].Rows.Count > 0)
            {
                e.Row.Cells[5].Text = dsTerritory.Tables[0].Rows[0]["wrk_area_Name"].ToString();

            }
        }
    }
    private void FillStockist()
    {
        Stockist sk = new Stockist();
        dsStockist = sk.GET_DISTRIBUTOR_Home(divcode, "", "", "", "0");
        if (dsStockist.Tables[0].Rows.Count > 0)
        {
            grdStockist.Visible = true;
            grdStockist.DataSource = dsStockist;
            grdStockist.DataBind();
            foreach (GridViewRow row in grdStockist.Rows)
            {
                LinkButton lnkdeact = (LinkButton)row.FindControl("lnkbutDeactivate");
                Label lblimg = (Label)row.FindControl("lblimg");
                LinkButton lblSubDiv_count = (LinkButton)row.FindControl("lblSubDiv_count");
                LinkButton lblSubfield_count = (LinkButton)row.FindControl("LinkDSM");
                if ((lblSubDiv_count.Text != "0" || lblSubfield_count.Text != "0"))
                {
                    lnkdeact.Visible = false;
                    lblimg.Visible = true;
                }

            }
        }
        else
        {
            grdStockist.DataSource = dsStockist;
            grdStockist.DataBind();
        }
    }
    // Alphabat Order
    private void FillSF_Alpha()
    {
        Stockist sk = new Stockist();
        dsStockist = sk.getSalesForcelist_Alphabet(divcode);
        if (dsStockist.Tables[0].Rows.Count > 0)
        {
            dlAlpha.DataSource = dsStockist;
            dlAlpha.DataBind();
        }
    }
    private void FillStockist(string sAlpha)
    {
        Stockist sk = new Stockist();
        dsStockist = sk.GET_DISTRIBUTOR_Home(divcode, "", sAlpha, "", "0"); //getStockist_Alphabat(divcode, sAlpha);
        if (dsStockist.Tables[0].Rows.Count > 0)
        {
            grdStockist.Visible = true;
            grdStockist.DataSource = dsStockist;
            grdStockist.DataBind();
            foreach (GridViewRow row in grdStockist.Rows)
            {
                LinkButton lnkdeact = (LinkButton)row.FindControl("lnkbutDeactivate");
                Label lblimg = (Label)row.FindControl("lblimg");
                LinkButton lblSubDiv_count = (LinkButton)row.FindControl("lblSubDiv_count");
                LinkButton lblSubfield_count = (LinkButton)row.FindControl("LinkDSM");
                if ((lblSubDiv_count.Text != "0" || lblSubfield_count.Text != "0"))
                {
                    lnkdeact.Visible = false;
                    lblimg.Visible = true;
                }

            }
        }
    }
    protected void dlAlpha_ItemCommand(object source, DataListCommandEventArgs e)
    {
        LinkButton lnk = (LinkButton)e.CommandSource;
        string sCmd = e.CommandArgument.ToString();
        Session["GetcmdArgChar"] = sCmd;

        if (sCmd == "All")
        {

            FillStockist();
        }
        else
        {
            grdStockist.PageIndex = 0;
            FillStockist(sCmd);
        }
        //grdSalesForce.EditIndex = -1;
        //Fill the SalesForce Grid
        //FillSalesForce();
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
        Stockist sk = new Stockist();
        //dtGrid = sk.getStockistList_DataTable(divcode);
        sCmd = Session["GetCmdArgChar"].ToString();

        if (sCmd == "All")
        {
            dtGrid = sk.getStockistList_DataTable(divcode);
        }
        else if (sCmd != "")
        {
            dtGrid = sk.getStockistFilter_DataTable(divcode, sCmd);
        }
        else if (txtsearch.Text != "")
        {
            dtGrid = sk.getStockistSearch_DataTable(Session["div_code"].ToString(), txtsearch.Text, ddlFields.SelectedValue);
        }
        return dtGrid;
    }
    protected void grdStockist_Sorting(object sender, GridViewSortEventArgs e)
    {
        string sortingDirection = string.Empty;
        if (dir == SortDirection.Ascending)
        {
            dir = SortDirection.Descending;
            sortingDirection = "Desc";
        }
        else
        {
            dir = SortDirection.Ascending;
            sortingDirection = "Asc";
        }
        DataView sortedView = new DataView(BindGridView());
        sortedView.Sort = e.SortExpression + " " + sortingDirection;
        grdStockist.DataSource = sortedView;
        grdStockist.DataBind();
    }
    protected void btnNew_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        Response.Redirect("Distributor_Creation.aspx");
    }
    protected void grdStockist_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        grdStockist.EditIndex = -1;
        sCmd = Session["GetCmdArgChar"].ToString();

        if (sCmd == "All")
        {
            FillStockist();
        }
        else if (sCmd != "")
        {
            FillStockist(sCmd);
        }
        else if (txtsearch.Text != "")
        {
            FindStockist(ddlFields.SelectedValue, txtsearch.Text, Session["div_code"].ToString());
        }
    }
    protected void grdStockist_RowEditing(object sender, GridViewEditEventArgs e)
    {
        grdStockist.EditIndex = e.NewEditIndex;
        sCmd = Session["GetCmdArgChar"].ToString();

        if (sCmd == "All")
        {
            FillStockist();
        }
        else if (sCmd != "")
        {
            FillStockist(sCmd);
        }
        else if (txtsearch.Text != "")
        {
            FindStockist(ddlFields.SelectedValue, txtsearch.Text, Session["div_code"].ToString());
        }
        TextBox ctrl = (TextBox)grdStockist.Rows[e.NewEditIndex].Cells[2].FindControl("txtStockist_Name");
        ctrl.Focus();
    }
    protected void grdStockist_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        grdStockist.EditIndex = -1;
        int iIndex = e.RowIndex;
        UpdateStockist(iIndex);
        sCmd = Session["GetCmdArgChar"].ToString();

        if (sCmd == "All")
        {
            FillStockist();
        }
        else if (sCmd != "")
        {
            FillStockist(sCmd);
        }
        else if (txtsearch.Text != "")
        {
            FindStockist(ddlFields.SelectedValue, txtsearch.Text, Session["div_code"].ToString());
        }
    }
    protected void grdStockist_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Deactivate")
        {
            stockist_code = Convert.ToString(e.CommandArgument);

            //Deactivate the Stockist Details
            Stockist dv = new Stockist();
            int iReturn = dv.DeActivate(stockist_code,"1");
            if (iReturn > 0)
            {
                //  menu1.Status = "Stockist has been Deactivated Successfully";
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Deactivated Successfully');</script>");
            }
            else
            {
                // menu1.Status = "Unable to Deactivate";
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Unable to Deactivate');</script>");
            }
            FillStockist();
        }
    }
    protected void grdStockist_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdStockist.PageIndex = e.NewPageIndex;
        sCmd = Session["GetCmdArgChar"].ToString();

        if (sCmd == "All")
        {
            FillStockist();
        }
        else if (sCmd != "")
        {
            FillStockist(sCmd);
        }
        else if (txtsearch.Text != "")
        {
            FindStockist(ddlFields.SelectedValue, txtsearch.Text, Session["div_code"].ToString());
        }
        else if (ddlFilter.SelectedIndex > 0)
        {
            FillSalesForce_Reporting();
        }
        else if (ddlSrc.SelectedIndex > 0)
        {
            FindStockist1(ddlFields.SelectedValue, ddlSrc.SelectedItem.ToString(), Session["div_code"].ToString());
        }

    }
    protected void grdStockist_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "this.className='highlight'");
            e.Row.Attributes.Add("onmouseout", "this.className='normal'");
        }
    }
    // Search By Text
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        Session["GetcmdArgChar"] = string.Empty;
        if (ddlSrc.SelectedValue != "")
        {
            FindStockist1(ddlFields.SelectedValue, ddlSrc.SelectedItem.ToString(), Session["div_code"].ToString());
        }
        else if (ddlFields.SelectedValue != "")
        {
            FindStockist(ddlFields.SelectedValue, txtsearch.Text, Session["div_code"].ToString());
        }
        else
        {
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Plz Select any one');</script>");
        }
    }

    private void FindStockist1(string sSearchBy, string sSearchText, string div_code)
    {
        string sFind = string.Empty;
        sFind = " AND " + sSearchBy + " like '" + sSearchText + "%' AND Division_Code = '" + div_code + "' ";
        Stockist sk = new Stockist();
        dsStockist = sk.FindStockistlist(sFind);
        if (dsStockist.Tables[0].Rows.Count > 0)
        {
            grdStockist.Visible = true;
            grdStockist.DataSource = dsStockist;
            grdStockist.DataBind();
            foreach (GridViewRow row in grdStockist.Rows)
            {
                LinkButton lnkdeact = (LinkButton)row.FindControl("lnkbutDeactivate");
                Label lblimg = (Label)row.FindControl("lblimg");
                LinkButton lblSubDiv_count = (LinkButton)row.FindControl("lblSubDiv_count");
                LinkButton lblSubfield_count = (LinkButton)row.FindControl("LinkDSM");
                if ((lblSubDiv_count.Text != "0" || lblSubfield_count.Text != "0"))
                {
                    lnkdeact.Visible = false;
                    lblimg.Visible = true;
                }

            }
        }
        else
        {
            grdStockist.DataSource = dsStockist;
            grdStockist.DataBind();
        }

    }
    private void FindStockist(string sSearchBy, string sSearchText, string div_code)
    {
        string sFind = string.Empty;
        sFind = " AND " + sSearchBy + " like '" + sSearchText + "%' AND Division_Code = '" + div_code + "' ";
        Stockist sk = new Stockist();
        dsStockist = sk.FindStockistlist(sFind);
        if (dsStockist.Tables[0].Rows.Count > 0)
        {
            grdStockist.Visible = true;
            grdStockist.DataSource = dsStockist;
            grdStockist.DataBind();
            foreach (GridViewRow row in grdStockist.Rows)
            {
                LinkButton lnkdeact = (LinkButton)row.FindControl("lnkbutDeactivate");
                Label lblimg = (Label)row.FindControl("lblimg");
                LinkButton lblSubDiv_count = (LinkButton)row.FindControl("lblSubDiv_count");
                LinkButton lblSubfield_count = (LinkButton)row.FindControl("LinkDSM");
                if ((lblSubDiv_count.Text != "0" || lblSubfield_count.Text != "0"))
                {
                    lnkdeact.Visible = false;
                    lblimg.Visible = true;
                }

            }
        }
        else
        {
            grdStockist.DataSource = dsStockist;
            grdStockist.DataBind();
        }

    }
    private void UpdateStockist(int eIndex)
    {
        Label lblStockist_Code = (Label)grdStockist.Rows[eIndex].Cells[1].FindControl("lblStockist_Code");
        stockist_code = lblStockist_Code.Text;
        TextBox txtStockist_Name = (TextBox)grdStockist.Rows[eIndex].Cells[2].FindControl("txtStockist_Name");
        stockist_name = txtStockist_Name.Text;
        TextBox txtStockist_ContactPerson = (TextBox)grdStockist.Rows[eIndex].Cells[3].FindControl("txtStockist_ContactPerson");
        stockist_ContactPerson = txtStockist_ContactPerson.Text;
        TextBox txtStockist_Mobile = (TextBox)grdStockist.Rows[eIndex].Cells[4].FindControl("txtStockist_Mobile");
        stockist_mobileno = txtStockist_Mobile.Text;
        TextBox txtTerritory = (TextBox)grdStockist.Rows[eIndex].Cells[5].FindControl("txtTerritory");
        Territory = txtTerritory.Text;

        //Update Stockist
        Stockist sk = new Stockist();
        int iReturn = sk.RecordUpdate(divcode, stockist_code, stockist_name, stockist_ContactPerson, stockist_mobileno, Territory, "");
        if (iReturn > 0)
        {
            //menu1.Status = "Stockist Updated Successfully";
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Updated Successfully');</script>");
        }
        else if (iReturn == -2)
        {
            //menu1.Status = "Stockist exit with the same name !!";
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Already Exist with the Same Name');</script>");
        }
    }




    protected void btnClear_Click(object sender, EventArgs e)
    {
        txtsearch.Text = "";
        ddlFields.SelectedValue = "";
        FillStockist();
        ddlSrc.SelectedIndex = -1;
        //grdStockist.Visible = true;
        //grdStockist.DataSource = dsStockist;
        //grdStockist.DataBind();
    }
    protected void Link_DSM(object sender, EventArgs e)
    {
        GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer as GridViewRow;
        string sURL = string.Empty;
        Label ID1 = (Label)grdrow.FindControl("DSM");
        sURL = "Count.aspx?count=" + ID1.Text.ToString();
        string newWin = "window.open('" + sURL + "',null,'resizable=yes,toolbar=no,scrollbars=yes,menubar=no,status=no,width=900,height=600,left=0,top=0');";
        ClientScript.RegisterStartupScript(this.GetType(), "pop", newWin, true);
    }
    protected void ExportToExcel(object sender, EventArgs e)
    {
        
        DataTable dsProd1 = null;
        Stockist LstDoc = new Stockist();
        try
        {
            dsProd1 = LstDoc.getStockist_Ex_MGR(divcode, "", "", "", "0");
            DataTable dt = dsProd1;
            if (divcode != "109")
            {
                dt.Columns.Remove("Vendor_Code");
            }
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt, "DistributorList");
                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename=DistributorMaster.xlsx");
                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                }
            }
        }
        catch (Exception ex)
        {
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('" + ex.Message + "');</script>");
        }
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
           server control at run time. */
    }
    protected void btnGo_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        if (ddlFilter.SelectedItem.ToString() == "")
        {

            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Select Any One');</script>");
        }
        else
        {
            if (ddlFilter.SelectedIndex > 0)
            {
                grdStockist.PageIndex = 0;
                FillSalesForce_Reporting();
                txtsearch.Text = string.Empty;
                Session["GetCmdArgChar"] = string.Empty;
                if (ddlSrc.SelectedIndex != -1)
                {
                    ddlSrc.SelectedIndex = 0;
                }
            }
            else
            {
                FillStockist();
            }
        }
    }
    private void FillSalesForce_Reporting()
    {
        string sReport = ddlFilter.SelectedValue.ToString();
        Stockist sf = new Stockist();
        //dsSalesForce = sf.getSalesForcelist_Reporting(div_code, sReport);

        //dsSalesForce = sf.SalesForceList(div_code, sReport);

        DataTable dtUserList = new DataTable();
        dtUserList = sf.getStockist_Filter1(divcode, sReport); // 28-Aug-15 -Sridevi



        if (dtUserList.Rows.Count > 0)
        {
            grdStockist.Visible = true;
            grdStockist.DataSource = dtUserList;
            grdStockist.DataBind();
            foreach (GridViewRow row in grdStockist.Rows)
            {
                LinkButton lnkdeact = (LinkButton)row.FindControl("lnkbutDeactivate");
                Label lblimg = (Label)row.FindControl("lblimg");
                LinkButton lblSubDiv_count = (LinkButton)row.FindControl("lblSubDiv_count");
                LinkButton lblSubfield_count = (LinkButton)row.FindControl("LinkDSM");
                if ((lblSubDiv_count.Text != "0" || lblSubfield_count.Text != "0"))
                {
                    lnkdeact.Visible = false;
                    lblimg.Visible = true;
                }

            }
        }
        else
        {
            grdStockist.DataSource = dtUserList;
            grdStockist.DataBind();
        }
    }
    protected void Search(object sender, EventArgs e)
    {
        this.BindGrid();
    }

    protected void ddlFields_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlFields.SelectedValue == "Territory")
        {
            txtsearch.Visible = false;
            ddlSrc.Visible = true;
            GetTerritoryName();
        }
        else
        {
            ddlSrc.Visible = false;
            txtsearch.Visible = true;
        }

    }

    private void GetTerritoryName()
    {
        Stockist sk = new Stockist();
        dsStockist = sk.getTer_Name(divcode);
        if (dsStockist.Tables[0].Rows.Count > 0)
        {
            ddlSrc.DataTextField = "Territory_name";
            ddlSrc.DataValueField = "Territory_code";
            ddlSrc.DataSource = dsStockist;
            ddlSrc.DataBind();
        }
    }

    private void BindGrid()
    {
        string constr = Globals.ConnString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand())
            {

                cmd.CommandText = "SELECT a.User_Entry_Code,a.Stockist_Code,a.Stockist_Name,a.Stockist_ContactPerson,a.Stockist_Mobile,a.Territory,a.Distributor_Code,a.Field_Name, " +
                     "COUNT(z.DSM_name) as 'Sub_Coun' from Mas_DSM z full join " +
                     "mas_stockist a on a.Stockist_Code=z.Distributor_Code and z.DSM_Active_Flag=0 where a.Stockist_Active_Flag=0 and a.Division_Code='" + divcode + "' and Stockist_Name LIKE '%' + @Stockist_Name + '%' group by a.User_Entry_Code,a.Stockist_Code,a.Stockist_Name,a.Stockist_ContactPerson,a.Stockist_Mobile,a.Territory,a.Distributor_Code,a.Field_Name";
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@Stockist_Name", txtSearch1.Text.Trim());
                DataTable dt = new DataTable();
                using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                {
                    sda.Fill(dt);
                    grdStockist.DataSource = dt;
                    grdStockist.DataBind();
                    foreach (GridViewRow row in grdStockist.Rows)
                    {
                        LinkButton lnkdeact = (LinkButton)row.FindControl("lnkbutDeactivate");
                        Label lblimg = (Label)row.FindControl("lblimg");
                        LinkButton lblSubDiv_count = (LinkButton)row.FindControl("lblSubDiv_count");
                        LinkButton lblSubfield_count = (LinkButton)row.FindControl("LinkDSM");
                        if ((lblSubDiv_count.Text != "0" || lblSubfield_count.Text != "0"))
                        {
                            lnkdeact.Visible = false;
                            lblimg.Visible = true;
                        }

                    }
                }
            }
        }
    }
}