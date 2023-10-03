using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
using System.Data.SqlClient;
using System.Configuration;

public partial class MasterFiles_MR_Doctor_SubCategory_Map : System.Web.UI.Page
{
    DataSet dsListedDR = null;
    string sf_code = string.Empty;
    string div_code = string.Empty;
    DataSet dsDocSubCat = null;
    bool bsrch = false;
    DataSet dsCatgType = null;
    string Listed_DR_Code = string.Empty;
    string doctype = string.Empty;
    string chkCampaign = string.Empty;
    string Doc_SubCatCode = string.Empty;
    DataSet dsDR = null;
    DataSet dsTerritory = null;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    DataSet dsDoc = null;
    string sCmd = string.Empty;
    int iReturn = -1;
    int time;
    int search = 0;
    int iIndex = -1;
    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        sf_code = Session["sf_code"].ToString();
        if (!Page.IsPostBack)
        {
            menu1.Title = this.Page.Title;
            menu1.FindControl("btnBack").Visible = false;
            FillDoc();
            FillCampaign();
            getWorkName();
        }
        else
        {
            //GetCampaign();
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
        ListedDR LstDoc = new ListedDR();     


        dtGrid = LstDoc.getListedDoctorList_DataTable_camp(sf_code);
       
        return dtGrid;
    }
    protected void grdDoctor_Sorting(object sender, GridViewSortEventArgs e)
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
        grdDoctor.DataSource = sortedView;
        grdDoctor.DataBind();
    }

    private void GetCampaign()
    {        foreach (GridViewRow row in grdDoctor.Rows)
        {
            GridView grdCampaign = row.FindControl("grdCampaign") as GridView;
            foreach (GridViewRow grid in grdCampaign.Rows)
            {
                CheckBox chk = (CheckBox)grid.FindControl("chkCatName");
                Label hf = (Label)grid.FindControl("cbSubCat");

                if (chk.Checked== false)
                {
                    //sChkSalesforce = sChkSalesforce + hf.Value + ",";
                    chk.Checked = false;
                    chk.Style.Add("color", "Black");

                }



            }
        }
    }
    private void FillCampaign()
    {
        foreach (GridViewRow row in grdDoctor.Rows)
        {
            Label lblcode = (Label)row.FindControl("lblDrcode");
            GridView grdCampaign = row.FindControl("grdCampaign") as GridView;
            string str_CateCode = "";
            ListedDR LstDoc = new ListedDR();
            dsListedDR = LstDoc.get_Camp(lblcode.Text,div_code);
            if (dsListedDR.Tables[0].Rows.Count > 0)
            {
                str_CateCode = dsListedDR.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();
            }

            foreach (GridViewRow grid in grdCampaign.Rows)
            {
                Label SubCatCode = (Label)grid.FindControl("cbSubCat");

                //ListedDR LstDoc = new ListedDR();
                //dsListedDR = LstDoc.get_Camp(lblcode.Text);
                //if (dsListedDR.Tables[0].Rows.Count > 0)
                //{
                //    SubCatCode.Text = dsDocSubCat.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                //}

                string[] Salesforce;
                if (str_CateCode != "")
                {
                    iIndex = -1;
                    Salesforce = str_CateCode.Split(',');
                    foreach (string sf in Salesforce)
                    {

                        CheckBox chk = (CheckBox)grid.FindControl("chkCatName");
                        Label hf = (Label)grid.FindControl("cbSubCat");

                        if (sf == hf.Text)
                        {
                            //sChkSalesforce = sChkSalesforce + hf.Value + ",";
                            chk.Checked = true;
                            chk.Attributes.Add("style", "Color: Red; font-weight:Bold; ");


                        }
                       
                      

                    }
                }

            }
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


    private void FillDoc()
    {
        ListedDR LstDoc = new ListedDR();
        dsListedDR = LstDoc.getListedDr_Camp(sf_code, div_code);
        if (dsListedDR.Tables[0].Rows.Count > 0)
        {

            grdDoctor.Visible = true;
            grdDoctor.DataSource = dsListedDR;
            grdDoctor.DataBind();
            
        }
        else
        {
            grdDoctor.DataSource = dsListedDR;
            grdDoctor.DataBind();

        }
        foreach (DataRow drFF in dsListedDR.Tables[0].Rows)
            {
                foreach (GridViewRow grid_row in grdDoctor.Rows)
                {
                    Label lblDrName = (Label)grid_row.FindControl("lblDrName");
                    Label lblDrcode = (Label)grid_row.FindControl("lblDrcode");
                    if (drFF["ListedDrCode"].ToString() == lblDrcode.Text)
                    {
                        if (drFF["Doc_SubCatName"].ToString().Length > 0)
                        {
                            lblDrName.ForeColor = System.Drawing.Color.White;
                            lblDrName.BackColor = System.Drawing.Color.BlueViolet;
                        }
                    }
                }
        }
    }
    protected void ddlSrc2_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ddlSrch_SelectedIndexChanged(object sender, EventArgs e)
    {
        search = Convert.ToInt32(ddlSrch.SelectedValue);
        if (search == 7)
        {
            txtsearch.Visible = true;

            ddlSrc2.Visible = false;
        }
        else
        {
            txtsearch.Visible = false;
            ddlSrc2.Visible = true;

        }

        if (search == 1)
        {
            ddlSrc2.Visible = false;
        }
        if (search == 2)
        {
            FillSpl();
        }
        if (search == 3)
        {
            FillCat();
        }
        if (search == 4)
        {
            FillQual();
        }
        if (search == 5)
        {
            FillCls();
        }
        if (search == 6)
        {
            FillTerr();
        }

    }
    private void FillCat()
    {
        ListedDR lstDR = new ListedDR();
        dsDR = lstDR.FetchCategory(sf_code);
        if (dsDR.Tables[0].Rows.Count > 0)
        {
            ddlSrc2.DataTextField = "Doc_Cat_Name";
            ddlSrc2.DataValueField = "Doc_Cat_Code";
            ddlSrc2.DataSource = dsDR;
            ddlSrc2.DataBind();
        }

    }
    private void FillSpl()
    {
        ListedDR lstDR = new ListedDR();
        dsDR = lstDR.FetchSpeciality(sf_code);
        if (dsDR.Tables[0].Rows.Count > 0)
        {
            ddlSrc2.DataTextField = "Doc_Special_Name";
            ddlSrc2.DataValueField = "Doc_Special_Code";
            ddlSrc2.DataSource = dsDR;
            ddlSrc2.DataBind();
        }

    }
    private void FillQual()
    {
        ListedDR lstDR = new ListedDR();
        dsDR = lstDR.FetchQualification(sf_code);
        if (dsDR.Tables[0].Rows.Count > 0)
        {
            ddlSrc2.DataTextField = "Doc_QuaName";
            ddlSrc2.DataValueField = "Doc_QuaCode";
            ddlSrc2.DataSource = dsDR;
            ddlSrc2.DataBind();
        }

    }
    private void FillCls()
    {
        ListedDR lstDR = new ListedDR();
        dsDR = lstDR.FetchClass(sf_code);
        if (dsDR.Tables[0].Rows.Count > 0)
        {
            ddlSrc2.DataTextField = "Doc_ClsName";
            ddlSrc2.DataValueField = "Doc_ClsCode";
            ddlSrc2.DataSource = dsDR;
            ddlSrc2.DataBind();
        }

    }
    private void FillTerr()
    {
        ListedDR lstDR = new ListedDR();
        dsDR = lstDR.FetchTerritory(sf_code);
        if (dsDR.Tables[0].Rows.Count > 0)
        {
            ddlSrc2.DataTextField = "Territory_Name";
            ddlSrc2.DataValueField = "Territory_Code";
            ddlSrc2.DataSource = dsDR;
            ddlSrc2.DataBind();
        }

    }


    protected void btnOk_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        ListedDR LstDoc = new ListedDR();
        search = Convert.ToInt32(ddlSrch.SelectedValue);
        btnSave.Visible = true;

        if (search == 1)
        {

            FillDoc();
        }
        if (search == 2)
        {
            dsDoc = LstDoc.getListedDrforSpl_Camp(sf_code, ddlSrc2.SelectedValue);
            if (dsDoc.Tables[0].Rows.Count > 0)
            {
                grdDoctor.Visible = true;
                grdDoctor.DataSource = dsDoc;
                grdDoctor.DataBind();
            }
            else
            {
                btnSave.Visible = false;
                grdDoctor.DataSource = dsDoc;
                grdDoctor.DataBind();
            }
        }
        if (search == 3)
        {

            dsDoc = LstDoc.getListedDrforCat_Camp(sf_code, ddlSrc2.SelectedValue);
            if (dsDoc.Tables[0].Rows.Count > 0)
            {
                grdDoctor.Visible = true;
                grdDoctor.DataSource = dsDoc;
                grdDoctor.DataBind();
            }
            else
            {
                btnSave.Visible = false;
                grdDoctor.DataSource = dsDoc;
                grdDoctor.DataBind();
            }
        }
        if (search == 4)
        {
            dsDoc = LstDoc.getListedDrforQual_Camp(sf_code, ddlSrc2.SelectedValue);
            if (dsDoc.Tables[0].Rows.Count > 0)
            {
                grdDoctor.Visible = true;
                grdDoctor.DataSource = dsDoc;
                grdDoctor.DataBind();
            }
            else
            {
                btnSave.Visible = false;
                grdDoctor.DataSource = dsDoc;
                grdDoctor.DataBind();
            }
        }
        if (search == 5)
        {
            dsDoc = LstDoc.getListedDrforClass_Camp(sf_code, ddlSrc2.SelectedValue);
            if (dsDoc.Tables[0].Rows.Count > 0)
            {
                grdDoctor.Visible = true;
                grdDoctor.DataSource = dsDoc;
                grdDoctor.DataBind();
            }
            else
            {
                btnSave.Visible = false;
                grdDoctor.DataSource = dsDoc;
                grdDoctor.DataBind();
            }
        }
        if (search == 6)
        {

            dsDoc = LstDoc.getListedDrforTerr_Camp(sf_code, ddlSrc2.SelectedValue);
            if (dsDoc.Tables[0].Rows.Count > 0)
            {
                grdDoctor.Visible = true;
                grdDoctor.DataSource = dsDoc;
                grdDoctor.DataBind();
            }
            else
            {
                btnSave.Visible = false;
                grdDoctor.DataSource = dsDoc;
                grdDoctor.DataBind();
            }
        }
        if (search == 7)
        {

            dsDoc = LstDoc.getListedDrforName_Camp(sf_code, txtsearch.Text);
            if (dsDoc.Tables[0].Rows.Count > 0)
            {
                grdDoctor.Visible = true;
                grdDoctor.DataSource = dsDoc;
                grdDoctor.DataBind();
            }
            else
            {
                btnSave.Visible = false;
                grdDoctor.DataSource = dsDoc;
                grdDoctor.DataBind();
            }

        }
        FillCampaign();
    }
    protected void grdCampaign_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            e.Row.Attributes.Add("checked", Page.ClientScript.GetPostBackEventReference(sender as GridView, "Select$" + e.Row.RowIndex.ToString()));
        }

    }
    private void getWorkName()
    {
        Territory terr = new Territory();
        dsTerritory = terr.getWorkAreaName(div_code);
        if (dsTerritory.Tables[0].Rows.Count > 0)
        {
            string str = "Doctor " + dsTerritory.Tables[0].Rows[0]["wrk_area_Name"];
            ddlSrch.Items.Add(new ListItem(str, "6"));
        }
    }
    protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            //e.Row.Attributes.Add("onclick", Page.ClientScript.GetPostBackEventReference(sender as GridView,"Select$" + e.Row.RowIndex.ToString()));

            GridView GCamp = (GridView)e.Row.FindControl("grdCampaign");

            Doctor dv = new Doctor();
            dsDocSubCat = dv.getDocSubCat(div_code);
            if (dsDocSubCat.Tables[0].Rows.Count > 0)
            {
                GCamp.Visible = true;
                GCamp.DataSource = dsDocSubCat;
                GCamp.DataBind();
            }
            else
            {
                GCamp.DataSource = dsDocSubCat;
                GCamp.DataBind();
            }

        }

        //else
        //{
        //    GCamp.DataSource = dsDocSubCat;
        //    GCamp.DataBind();
        //}
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DropDownList ddlType = (DropDownList)e.Row.FindControl("ddlType");
            if (ddlType != null)
            {
                DataRowView row = (DataRowView)e.Row.DataItem;
                ddlType.SelectedIndex = ddlType.Items.IndexOf(ddlType.Items.FindByText(row["Doc_Type"].ToString()));
            }
        }
        if (e.Row.RowType == DataControlRowType.Header)
        {
            Territory terr = new Territory();
            DataSet dsTerritory = new DataSet();
            dsTerritory = terr.getWorkAreaName(div_code);
            if (dsTerritory.Tables[0].Rows.Count > 0)
            {
                e.Row.Cells[7].Text = dsTerritory.Tables[0].Rows[0]["wrk_area_Name"].ToString();

            }
        }

    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        btnSubmit_Click(sender, e);
    }
    protected void chkCatName_OnCheckedChanged(object sender, EventArgs e)
    {
        string CampaignName = string.Empty;
        foreach (GridViewRow row in grdDoctor.Rows)
        {
            Label lblcode = (Label)row.FindControl("lblDrcode");
            Label lblCatName = (Label)row.FindControl("Doc_SubCatName");
            
            chkCampaign = "";
            CampaignName = "";
            GridView grdCampaign = row.FindControl("grdCampaign") as GridView;
            foreach (GridViewRow grid in grdCampaign.Rows)
            {

                CheckBox chk = (CheckBox)grid.FindControl("chkCatName");
                Label hf = (Label)grid.FindControl("cbSubCat");
            
                if (chk.Checked)
                {
                    // str = lblcode.Text;
                    //chkCampaign = chkCampaign + chk.Text + ",";
                
                    chkCampaign = chkCampaign + hf.Text + ",";                    
                    CampaignName = CampaignName + chk.Text + ",";
                }

            }
            lblCatName.Text = CampaignName;

          
        }
    }
    protected void Show_Hide_OrdersGrid(object sender, EventArgs e) 
    { 
        ImageButton imgShowHide = (sender as ImageButton); 
        GridViewRow row = (imgShowHide.NamingContainer as GridViewRow); 
        if (imgShowHide.CommandArgument == "Show") 
        {
            row.FindControl("pnlOrders").Visible = true; 
            imgShowHide.CommandArgument = "Hide"; 
            imgShowHide.ImageUrl = "~/images/minus.png";
            row.Focus();
        }
        else
        { 
            row.FindControl("pnlOrders").Visible = false; 
            imgShowHide.CommandArgument = "Show"; 
            imgShowHide.ImageUrl = "~/images/plus.png"; 
             
        }
    } 

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        foreach (GridViewRow row in grdDoctor.Rows)
        {
            Label lblcode = (Label)row.FindControl("lblDrcode");
          
            chkCampaign = "";
            GridView grdCampaign = row.FindControl("grdCampaign") as GridView;
            foreach (GridViewRow grid in grdCampaign.Rows)
            {

                CheckBox chk = (CheckBox)grid.FindControl("chkCatName");
                Label hf = (Label)grid.FindControl("cbSubCat");
                if (chk.Checked)
                {
                   // str = lblcode.Text;
                    //chkCampaign = chkCampaign + chk.Text + ",";
                    chkCampaign = chkCampaign + hf.Text + ",";

                }
               
            }

            if (chkCampaign!= "")
            {
                ListedDR lst = new ListedDR();
                int iReturn = lst.Map_Campaign(chkCampaign, lblcode.Text, div_code);
                if (iReturn > 0)
                {
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Mapped Successfully');</script>");
                    FillDoc();
                    FillCampaign();
                    
                }
                
                
            }
           
        }
       
    }

}