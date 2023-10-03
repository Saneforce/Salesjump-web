using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
using System.Drawing.Imaging;

public partial class MasterFiles_UserList_NewWindow : System.Web.UI.Page
{
    #region "Declaration"
    DataSet dsUserList = null;
    DataSet dsDivision = null;
    DataSet dsSalesForce = null;
    DataSet dsAT = null;
    DataSet dsATM = null;

    string div_code = string.Empty;
    string ProdCode = string.Empty;
    string ProdSaleUnit = string.Empty;
    string ProdName = string.Empty;
    string sf_type = string.Empty;
    SalesForce sf = new SalesForce();
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    Product prd = new Product();
    DataSet dsdiv = new DataSet();
    string strMultiDiv = string.Empty;
    string sf_code = string.Empty;
    string bcolor = string.Empty;
    string sURL = string.Empty;
    int time;
    string strVacant = "1";
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        sf_code = Session["sf_code"].ToString();
        sf_type = Session["sf_type"].ToString();
        if (sf_type == "3")
        {
            div_code = Session["Division_Code"].ToString();
        }
        else
        {
            div_code = Session["div_code"].ToString();
        }

        if (!Page.IsPostBack)
        {
            if (Session["sf_type"].ToString() == "" || Session["sf_type"].ToString() == null || Session["sf_type"].ToString() == "3")
            {
                Filldiv();
                FillManagers();
                //  ddlDivision.SelectedIndex = 1;
                ddlDivision_SelectedIndexChanged(sender, e);
                ddlFieldForce.SelectedIndex = 1;
                btnGo.Focus();

            }
            else if (Session["sf_type"].ToString() == "2")
            {
                Product prd = new Product();
                DataSet dsdiv = new DataSet();
                dsdiv = prd.getMultiDivsf_Name(sf_code);
                if (dsdiv.Tables[0].Rows.Count > 0)
                {
                    if (dsdiv.Tables[0].Rows[0]["IsMultiDivision"].ToString() == "1")
                    {
                        strMultiDiv = dsdiv.Tables[0].Rows[0][1].ToString().Remove(dsdiv.Tables[0].Rows[0][1].ToString().Length - 1, 1);
                        ddlDivision.Visible = true;
                        lblDivision.Visible = true;
                        btnGo.Visible = true;
                        getDivision();
                    }
                    else
                    {

                        ddlDivision.Visible = false;
                        lblDivision.Visible = false;
                        btnGo.Visible = false;
                        BindUserList();
                    }
                }
            }
        }

        if (Session["sf_type"].ToString() == "2")
        {
      
      
            lblFilter.Visible = false;
            ddlFFType.Visible = false;
            ddlAlpha.Visible = false;
            ddlFieldForce.Visible = false;
            grdSalesForce.Columns[7].Visible = false;

        }
        else if (Session["sf_type"].ToString() == "")
        {
        }
        else if (Session["sf_type"].ToString() == "3")
        {
        
        }
        FillColor();
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

    private void BindUserList()
    {

        dsdiv = prd.getMultiDivsf_Name(sf_code);

        string strVacant = "1";
        if (chkVacant.Checked == true)
        {
            strVacant = "0";
        }
        if (dsdiv.Tables[0].Rows.Count > 0)
        {
            if (dsdiv.Tables[0].Rows[0]["IsMultiDivision"].ToString() == "1")
            {
                div_code = ddlDivision.SelectedValue;
            }
        }
        DataTable dtUserList = new DataTable();
        if (chkVacant.Checked == true)
        {
            dtUserList = sf.getUserListReportingToNew(div_code, sf_code, 0, Session["sf_type"].ToString());
        }
        else
        {
            dtUserList = sf.getUserListReportingToAllNew(div_code, sf_code, 0, Session["sf_type"].ToString());
        }

        if (dtUserList.Rows.Count > 0)
        {
            grdSalesForce.Visible = true;
            grdSalesForce.DataSource = dtUserList;
            grdSalesForce.DataBind();

        }
        else
        {
            grdSalesForce.DataSource = dtUserList;
            grdSalesForce.DataBind();
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
    private void Filldiv()
    {
        Division dv = new Division();
        if (sf_type == "3")
        {
            string[] strDivSplit = div_code.Split(',');
            foreach (string strdiv in strDivSplit)
            {
                if (strdiv != "")
                {
                    dsdiv = dv.getDivisionHO(strdiv);
                    ListItem liTerr = new ListItem();
                    liTerr.Value = dsdiv.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                    liTerr.Text = dsdiv.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                    ddlDivision.Items.Add(liTerr);
                }
            }
        }
        else
        {
            dsDivision = dv.getDivision_Name();
            if (dsDivision.Tables[0].Rows.Count > 0)
            {
                ddlDivision.DataTextField = "Division_Name";
                ddlDivision.DataValueField = "Division_Code";
                ddlDivision.DataSource = dsDivision;
                ddlDivision.DataBind();
            }
        }
    }
    protected void grdSalesForce_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            Label lblBackColor = (Label)e.Row.FindControl("lblBackColor");
            string bcolor = "#" + lblBackColor.Text;
            e.Row.BackColor = System.Drawing.Color.FromName(bcolor);

            Label lblS = (Label)e.Row.FindControl("lblS");
            if (lblS.Text == "Vacant")
            {
                lblS.ForeColor = System.Drawing.Color.Red;
                lblS.Style.Add("font-size", "12pt");
                lblS.Style.Add("font-weight", "Bold");
            }
            Label lblDrsCnt = (Label)e.Row.FindControl("lblDrsCnt");
            Label lblSF_Code = (Label)e.Row.FindControl("lblSF_Code");
            ListedDR lstdr = new ListedDR();
            DataSet dsdr = new DataSet();
            dsdr = lstdr.getListDr_CountNew(lblSF_Code.Text, div_code);

            if (dsdr.Tables[0].Rows.Count > 0)
            {
                lblDrsCnt.Text = dsdr.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
            }
            if (lblDrsCnt.Text == "0")
            {
                lblDrsCnt.Text = "***";
            }

        }
        FillAuditTeam();
    }
    private void FillManagers()
    {
        SalesForce sf = new SalesForce();

        if (ddlFFType.SelectedValue.ToString() == "1")
        {
            ddlAlpha.Visible = false;
            dsSalesForce = sf.UserList_Hierarchy(ddlDivision.SelectedValue.ToString(), "admin");
        }
        else if (ddlFFType.SelectedValue.ToString() == "0")
        {
            FillSF_Alpha();
            ddlAlpha.Visible = true;
            dsSalesForce = sf.UserList_Alpha(ddlDivision.SelectedValue.ToString(), "admin");
        }

        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            ddlFieldForce.DataTextField = "sf_name";
            ddlFieldForce.DataValueField = "sf_code";
            ddlFieldForce.DataSource = dsSalesForce;
            ddlFieldForce.DataBind();

            ddlSF.DataTextField = "des_color";
            ddlSF.DataValueField = "sf_code";
            ddlSF.DataSource = dsSalesForce;
            ddlSF.DataBind();

        }
    }
    private void FillColor()
    {
        int j = 0;


        foreach (ListItem ColorItems in ddlSF.Items)
        {
            string bcolor = "#" + ColorItems.Text;
            ddlFieldForce.Items[j].Attributes.Add("style", "background-color:" + bcolor);
            //ddlFieldForce.Items[j].Selected = true;

            //if (ColorItems.Text == "Level1")
            //    //ColorItems.Attributes.Add("style", "background-color: Wheat");
            //    ddlFieldForce.Items[j].Attributes.Add("style", "background-color: Wheat");

            //if (ColorItems.Text == "Level2")
            //    //ColorItems.Attributes.Add("style", "background-color: Blue");
            //    ddlFieldForce.Items[j].Attributes.Add("style", "background-color: LightGreen");

            //if (ColorItems.Text == "Level3")
            //    //ColorItems.Attributes.Add("style", "background-color: Cyan");
            //    ddlFieldForce.Items[j].Attributes.Add("style", "background-color: Pink");

            //if (ColorItems.Text == "Level4")
            //    //ColorItems.Attributes.Add("style", "background-color: Lavendar");
            //    ddlFieldForce.Items[j].Attributes.Add("style", "background-color: Lavendar");

            j = j + 1;

        }
    }

    private void FillgridColor()
    {

        foreach (GridViewRow grid_row in grdSalesForce.Rows)
        {

            Label lblBackColor = (Label)grid_row.FindControl("lblBackColor");
            string bcolor = "#" + lblBackColor.Text;
            grid_row.BackColor = System.Drawing.Color.FromName(bcolor);


        }
        FillAuditTeam();
    }
    private void FillAuditTeam()
    {
        // To show  audit team.
        SalesForce sf = new SalesForce();
        dsAT = sf.getAuditTeam(ddlDivision.SelectedValue.ToString());
        if (dsAT.Tables[0].Rows.Count > 0)
        {
            foreach (DataRow drFF in dsAT.Tables[0].Rows)
            {
                foreach (GridViewRow grid_row in grdSalesForce.Rows)
                {

                    //string AuditMgr = dsATM.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                    //string[] Audit;
                    //Audit = AuditMgr.Split(',');
                    //foreach (string Au_cd in Audit)
                    //{
                    Label lblsfCode = (Label)grid_row.FindControl("lblSF_Code");
                    Label lblFieldForce = (Label)grid_row.FindControl("lblFieldForce");
                    if (drFF["sf_code"].ToString() == lblsfCode.Text)
                    {
                        // grid_row.BackColor = System.Drawing.Color.Yellow;
                        if (drFF["Audit_team"].ToString().Length > 0)
                        {
                            lblFieldForce.ForeColor = System.Drawing.Color.White;
                            lblFieldForce.BackColor = System.Drawing.Color.Green;
                        }
                        // lblFieldForce.Style.Add("font-size", "12pt");
                        //  lblFieldForce.Style.Add("font-weight", "Bold");
                    }
                    // }
                }
            }
        }
    }
    private void FillSF_Alpha()
    {
        SalesForce sf = new SalesForce();
        dsSalesForce = sf.getSalesForcelist_Alphabet(ddlDivision.SelectedValue.ToString());
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            ddlAlpha.DataTextField = "sf_name";
            ddlAlpha.DataValueField = "val";
            ddlAlpha.DataSource = dsSalesForce;
            ddlAlpha.DataBind();
            ddlAlpha.SelectedIndex = 0;
        }
    }

    private void FillUserList()
    {

        string sMgr = "admin";
        SalesForce sf = new SalesForce();

        if (chkVacant.Checked == true)
        {
            strVacant = "0";
        }
        if (ddlFieldForce.SelectedIndex > 0)
        {
            sMgr = ddlFieldForce.SelectedValue;
        }

        //// Commented the below code //// To fetch UserList by using DataSet & DataTable by Recursive call - Sridevi on 07/23/15
        ////  dsUserList = sf.UserList_Self(ddlDivision.SelectedValue, sMgr);
        //dsUserList = sf.UserList_Self_Vacant(ddlDivision.SelectedValue, sMgr, strVacant);
        //if (dsUserList.Tables[0].Rows.Count > 0)
        //{
        //    grdSalesForce.Visible = true;
        //    grdSalesForce.DataSource = dsUserList;
        //    grdSalesForce.DataBind();
        //}
        //else
        //{
        //    grdSalesForce.DataSource = dsUserList;
        //    grdSalesForce.DataBind();
        //}
        //// To fetch UserList by using DataSet & DataTable by Recursive call - Sridevi on 07/23/15

        DataTable dtUserList = new DataTable();
        if (chkVacant.Checked == true)
        {
            dtUserList = sf.getUserListReportingToNew(ddlDivision.SelectedValue, sMgr, 0, Session["sf_type"].ToString()); // 28-Aug-15 -Sridevi
        }
        else
        {
            dtUserList = sf.getUserListReportingToAllNew(ddlDivision.SelectedValue, sMgr, 0, Session["sf_type"].ToString()); // 28-Aug-15 -Sridevi
        }

        if (dtUserList.Rows.Count > 0)
        {
            grdSalesForce.Visible = true;
            grdSalesForce.DataSource = dtUserList;
            grdSalesForce.DataBind();
        }
        else
        {
            grdSalesForce.DataSource = dtUserList;
            grdSalesForce.DataBind();
        }
    }

    protected void ddlAlpha_SelectedIndexChanged(object sender, EventArgs e)
    {
        SalesForce sf = new SalesForce();
        if (ddlAlpha.SelectedIndex == 0)
        {
            dsSalesForce = sf.UserList_Alpha(ddlDivision.SelectedValue.ToString(), "admin");
        }
        else
        {
            dsSalesForce = sf.UserList_Alphasearch(ddlDivision.SelectedValue.ToString(), "admin", ddlAlpha.SelectedValue);
        }
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            ddlFieldForce.DataTextField = "sf_name";
            ddlFieldForce.DataValueField = "sf_code";
            ddlFieldForce.DataSource = dsSalesForce;
            ddlFieldForce.DataBind();

            ddlSF.DataTextField = "des_color";
            ddlSF.DataValueField = "sf_code";
            ddlSF.DataSource = dsSalesForce;
            ddlSF.DataBind();
            FillColor();
            FillgridColor();
        }


    }

    protected void btnGo_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        if (Session["sf_type"].ToString() == "2")
        {
            BindUserList();
        }
        else
        {
            FillUserList();
        }
        FillgridColor();

    }
    protected void ddlFFType_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillManagers();
        FillColor();
        FillgridColor();
    }

    protected void ddlFieldForce_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillgridColor();
    }

    protected void ddlDivision_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillManagers();
        FillColor();
        FillgridColor();
    }

}