using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;

public partial class MasterFiles_MR_ListedDoctor_RoutePlanView : System.Web.UI.Page
{
    string sf_code = string.Empty;
    DataSet dsTerritory = null;
    DateTime ServerStartTime;
    bool terrexist = false;
    DateTime ServerEndTime;
    int time;
    DataSet dsTerr;
    DataSet dsDoc = null;
    string strTerritoryCode = string.Empty;
    int docvisit = 0;
    DataTable dtDoctor;
    DataTable dtCatg;
    DataTable dtDoctor1;
    DataTable dtDoctor2;
    DataTable dtDoctor3;
    DataTable dtDoctor4;
    DataTable dtDoctor5;
    DataTable dtDoctor6;
    string[] terr_cd;
    string div_code = string.Empty;

    int maxrows = 0;
    int currow = 0;
    int i = 0;

    int multidoc = -1;
    DataSet dsadmin = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        sf_code = Session["sf_code"].ToString();
        div_code = Session["div_code"].ToString();

        AdminSetup dv = new AdminSetup();
        dsadmin = dv.getAdminSetup();
        if (dsadmin.Tables[0].Rows.Count > 0)
        {
            multidoc = Convert.ToInt16(dsadmin.Tables[0].Rows[0].ItemArray.GetValue(3).ToString());
        }
        if (!Page.IsPostBack)
        {

            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);
          
            menu1.Title = this.Page.Title;
            menu1.FindControl("btnBack").Visible = false;
            FillTerritory();
            GetWorkName();
            sf_code = Session["sf_code"].ToString();
         
        }
        RoutePlan cp = new RoutePlan();

        int MissedCount = cp.getMissedDr(sf_code);
        if (MissedCount > 0)
        {
            lblAllocate.Text = MissedCount.ToString() + " Missed Doctor(s)  in Plan";
        }
        else
        {
            lblAllocate.Text = "All Drs have been Allocated";
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
    private void GetWorkName()
    {

        Territory terr = new Territory();
        dsTerritory = terr.getWorkAreaName(div_code);
        if (dsTerritory.Tables[0].Rows.Count > 0)
        {
            menu1.Title = dsTerritory.Tables[0].Rows[0]["wrk_area_Name"] + " Categorywise";
            lblSDP.Text = dsTerritory.Tables[0].Rows[0]["wrk_area_Name"].ToString();
        }
    }
    private void FillTerritory()
    {
        ListedDR lstDR = new ListedDR();
        dsTerritory = lstDR.Load_Territory(sf_code);
        ddlTerritory.DataTextField = "Territory_Name";
        ddlTerritory.DataValueField = "Territory_Code";
        ddlTerritory.DataSource = dsTerritory;
        ddlTerritory.DataBind();
        //FillColor();
        //dsTerritory = lstDR.Load_Territory_catg(sf_code);

    }
    private void FillColor()
    {
        int j = 0;


        foreach (ListItem ColorItems in ddlTerritory.Items)
        {
            if (ColorItems.Text == "Missed DRs")
                ddlTerritory.Items[j].Attributes.Add("style", "background-color: Yellow");

            j = j + 1;
        }
    }

    protected void btnGo_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        FillDoc(ddlTerritory.SelectedValue.ToString());
        fillcatg();
        lblNote1.Visible = true;
        lblNote2.Visible = true;
    }

    private void fillcatg()
    {
        int icnt = 0;
        int icnt_catg = 0;
        int itotal = 0;
        if (ViewState["dt_catg"] != null)
        {
            dtCatg = (DataTable) ViewState["dt_catg"];
            if (dtCatg.Rows.Count > 0)
            {
                RoutePlan rpn = new RoutePlan();
                TableRow tr_header = new TableRow();
                tr_header.BorderStyle = BorderStyle.Solid;
                tr_header.BorderWidth = 1;

                foreach (DataRow dataRow in dtCatg.Rows)
                {
                    icnt_catg = rpn.get_ListedDoctor_TerritoryCatgwise(sf_code, ddlTerritory.SelectedValue.ToString() , dataRow["Doc_Cat_Code"].ToString());
                    icnt = icnt + 1;
                    itotal = itotal + icnt_catg; 
                    TableCell tc_catg = new TableCell();
                    Literal lit_catg = new Literal();
                    lit_catg.Text = dataRow["Doc_Cat_SName"].ToString() + " :";
                    tc_catg.Controls.Add(lit_catg);
                    tc_catg.BackColor = System.Drawing.Color.LightPink;
                    tc_catg.HorizontalAlign = HorizontalAlign.Right;
                    tr_header.Cells.Add(tc_catg);
                    //tr_header.BackColor = System.Drawing.Color.FromName("#A6A6D2");

                    TableCell tc_catg_cnt = new TableCell();
                    TextBox txtcatg = new TextBox();
                    txtcatg.Width = 30;
                    txtcatg.Text = icnt_catg.ToString();
                    //txtcatg.BackColor = System.Drawing.Color.AliceBlue;

                    tc_catg_cnt.Controls.Add(txtcatg);
                    tr_header.Cells.Add(tc_catg_cnt);
                }

                TableCell tc_catg_total = new TableCell();
                Literal lit_catg_total = new Literal();
                lit_catg_total.Text = "Total :";
                tc_catg_total.BackColor = System.Drawing.Color.LightPink;
                tc_catg_total.Controls.Add(lit_catg_total);
                tc_catg_total.HorizontalAlign = HorizontalAlign.Right;
                tr_header.Cells.Add(tc_catg_total);

                TableCell tc_catg_total_cnt = new TableCell();
                TextBox txtcatg_total = new TextBox();
                txtcatg_total.Width = 30;
               // txtcatg_total.BackColor = System.Drawing.Color.AliceBlue;
                txtcatg_total.Text = itotal.ToString();
                tc_catg_total_cnt.Controls.Add(txtcatg_total);
                tr_header.Cells.Add(tc_catg_total_cnt);

                tbl.Rows.Add(tr_header);
            }
        }
    }

    protected void grdDoctor1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        int catg1_cnt = 0;
        bool ismap = false;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            i = 0;
            
            CheckBox chkSelect = (CheckBox)e.Row.FindControl("chkSelect");
            Label lblDocCode = (Label)e.Row.FindControl("lblDocCode");
            Label lblSNo = (Label)e.Row.FindControl("lblSNo");
            
            Label lblTerrCode = (Label)e.Row.FindControl("lblTerrCode");
            Label lblDocName = (Label)e.Row.FindControl("lblDocName");

            if (lblTerrCode.Text.Trim().Length > 0)
            {
                terr_cd = lblTerrCode.Text.Trim().Split(',');
                foreach (string terr_code in terr_cd)
                {
                    if (terr_code.Trim().Length > 0)
                    {
                        ismap = true;
                        i = i + 1;
                    }
                }

                lblDocName.Text = lblDocName.Text + " (" + i + ") ";
            }
            
            RoutePlan rp = new RoutePlan();
            dtDoctor = rp.get_ListedDoctor_Territorywise(Session["sf_code"].ToString(), ddlTerritory.SelectedValue.ToString(), lblDocCode.Text);
            if (dtDoctor != null)
            {
                foreach (DataRow drDoc in dtDoctor.Rows)
                {
                    catg1_cnt = catg1_cnt + 1;
                    chkSelect.Checked = true;   
                }

            }
            if(dtDoctor.Rows.Count  == 0)
            {
                if (multidoc == 0)
                {
                    if (ismap == true)
                    {
                        e.Row.Enabled = false;
                        e.Row.BackColor = System.Drawing.Color.PapayaWhip;
                    }
                    else
                    {
                        if (lblDocName.Text.Trim().Length > 0)
                        {
                            e.Row.Enabled = true;
                            e.Row.BackColor = System.Drawing.Color.LightGreen;
                        }
                    }
                }
              
            }
            
          
            
            if (lblDocCode.Text.Trim() == "0")
            {
                chkSelect.Visible = false;
                lblSNo.Visible = false;
            }

            dsTerr = rp.get_ListedDoctor_TerritoryCode(Session["sf_code"].ToString(), lblDocCode.Text);
            if (dsTerr.Tables[0].Rows.Count > 0)
            {
                strTerritoryCode = dsTerr.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
            }
            string[] strStateSplit = strTerritoryCode.Split(',');
            docvisit = 0;
            foreach (string strstate in strStateSplit)
            {
                if (strstate != "")
                {
                    if (strstate == ddlTerritory.SelectedValue.ToString())
                    {
                        terrexist = true;
                    }
                    docvisit = docvisit + 1;
                }
                
            }
            if (docvisit.ToString() == ViewState["visit1"].ToString())
            {
                if (terrexist == false)
                {
                    
                    e.Row.Enabled = false;
                    e.Row.BackColor = System.Drawing.Color.LightCoral;
                }
            }
          
            ViewState["catg1_cnt"] = catg1_cnt.ToString();
        }
    }

    protected void grdDoctor2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        int catg2_cnt = 0;
        bool ismap = false;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            i = 0;
            CheckBox chkSelect = (CheckBox)e.Row.FindControl("chkSelect");
            Label lblDocCode = (Label)e.Row.FindControl("lblDocCode");
            Label lblSNo = (Label)e.Row.FindControl("lblSNo");

            Label lblTerrCode = (Label)e.Row.FindControl("lblTerrCode");
            Label lblDocName = (Label)e.Row.FindControl("lblDocName");

            if (lblTerrCode.Text.Trim().Length > 0)
            {
                terr_cd = lblTerrCode.Text.Trim().Split(',');
                foreach (string terr_code in terr_cd)
                {
                    if (terr_code.Trim().Length > 0)
                    {
                        ismap = true;
                        i = i + 1;
                    }
                }

                lblDocName.Text = lblDocName.Text + " (" + i + ") ";
            }

            RoutePlan rp = new RoutePlan();
            dtDoctor = rp.get_ListedDoctor_Territorywise(Session["sf_code"].ToString(), ddlTerritory.SelectedValue.ToString(), lblDocCode.Text);
            if (dtDoctor != null)
            {
                foreach (DataRow drDoc in dtDoctor.Rows)
                {
                    chkSelect.Checked = true;
                    catg2_cnt = catg2_cnt  + 1;
                }
            }
            if (dtDoctor.Rows.Count == 0)
            {
                if (multidoc == 0)
                {
                    if (ismap == true)
                    {
                        e.Row.Enabled = false;
                        e.Row.BackColor = System.Drawing.Color.PapayaWhip;
                    }
                    else
                    {
                        if (lblDocName.Text.Trim().Length > 0)
                        {
                            e.Row.Enabled = true;
                            e.Row.BackColor = System.Drawing.Color.LightGreen;
                        }
                    }
                }

            }
            
          
           
            if (lblDocCode.Text.Trim() == "0")
            {
                chkSelect.Visible = false;
                lblSNo.Visible = false;
            }

            dsTerr = rp.get_ListedDoctor_TerritoryCode(Session["sf_code"].ToString(), lblDocCode.Text);
            if (dsTerr.Tables[0].Rows.Count > 0)
            {
                strTerritoryCode = dsTerr.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
            }
            string[] strStateSplit = strTerritoryCode.Split(',');
            docvisit = 0;
            foreach (string strstate in strStateSplit)
            {
                if (strstate != "")
                {
                    if (strstate == ddlTerritory.SelectedValue.ToString())
                    {
                        terrexist = true;
                    }
                    docvisit = docvisit + 1;
                }

            }
            if (docvisit.ToString() == ViewState["visit2"].ToString())
            {
                if (terrexist == false)
                {
                    e.Row.Enabled = false;
                    e.Row.BackColor = System.Drawing.Color.LightCoral;
                }
            }

            ViewState["catg2_cnt"] = catg2_cnt.ToString();
        }
    }

    protected void grdDoctor3_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        int catg3_cnt  = 0;
        bool ismap = false;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            i = 0;
            CheckBox chkSelect = (CheckBox)e.Row.FindControl("chkSelect");
            Label lblDocCode = (Label)e.Row.FindControl("lblDocCode");
            Label lblSNo = (Label)e.Row.FindControl("lblSNo");

            Label lblTerrCode = (Label)e.Row.FindControl("lblTerrCode");
            Label lblDocName = (Label)e.Row.FindControl("lblDocName");

            if (lblTerrCode.Text.Trim().Length > 0)
            {
                terr_cd = lblTerrCode.Text.Trim().Split(',');
                foreach (string terr_code in terr_cd)
                {
                    if (terr_code.Trim().Length > 0)
                    {
                        ismap = true;
                        i = i + 1;
                    }
                }


                lblDocName.Text = lblDocName.Text + " (" + i + ") ";
            }

            RoutePlan rp = new RoutePlan();
            dtDoctor = rp.get_ListedDoctor_Territorywise(Session["sf_code"].ToString(), ddlTerritory.SelectedValue.ToString(), lblDocCode.Text);
            if (dtDoctor != null)
            {
                foreach (DataRow drDoc in dtDoctor.Rows)
                {
                    catg3_cnt = catg3_cnt + 1;
                    chkSelect.Checked = true;
                }
            }
            if (dtDoctor.Rows.Count == 0)
            {
                if (multidoc == 0)
                {
                    if (ismap == true)
                    {
                        e.Row.Enabled = false;
                        e.Row.BackColor = System.Drawing.Color.PapayaWhip;
                    }
                    else
                    {
                        if (lblDocName.Text.Trim().Length > 0)
                        {
                            e.Row.Enabled = true;
                            e.Row.BackColor = System.Drawing.Color.LightGreen;
                        }
                    }
                }

            }
            
          
            
            if (lblDocCode.Text.Trim() == "0")
            {
                chkSelect.Visible = false;
                lblSNo.Visible = false;
            }

            dsTerr = rp.get_ListedDoctor_TerritoryCode(Session["sf_code"].ToString(), lblDocCode.Text);
            if (dsTerr.Tables[0].Rows.Count > 0)
            {
                strTerritoryCode = dsTerr.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
            }
            string[] strStateSplit = strTerritoryCode.Split(',');
            docvisit = 0;
            foreach (string strstate in strStateSplit)
            {
                if (strstate != "")
                {
                    if (strstate == ddlTerritory.SelectedValue.ToString())
                    {
                        terrexist = true;
                    }
                    docvisit = docvisit + 1;
                }

            }
            if (docvisit.ToString() == ViewState["visit3"].ToString())
            {
                if (terrexist == false)
                {
                    e.Row.Enabled = false;
                    e.Row.BackColor = System.Drawing.Color.LightCoral;
                }
            }

            ViewState["catg3_cnt"] = catg3_cnt.ToString();
        }
    }

    protected void grdDoctor4_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        int catg4_cnt  = 0;
        bool ismap = false;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            i = 0;
            CheckBox chkSelect = (CheckBox)e.Row.FindControl("chkSelect");
            Label lblDocCode = (Label)e.Row.FindControl("lblDocCode");
            Label lblSNo = (Label)e.Row.FindControl("lblSNo");

            Label lblTerrCode = (Label)e.Row.FindControl("lblTerrCode");
            Label lblDocName = (Label)e.Row.FindControl("lblDocName");

            if (lblTerrCode.Text.Trim().Length > 0)
            {
                terr_cd = lblTerrCode.Text.Trim().Split(',');
                foreach (string terr_code in terr_cd)
                {
                    if (terr_code.Trim().Length > 0)
                    {
                        ismap = true;
                        i = i + 1;
                    }
                }

                lblDocName.Text = lblDocName.Text + " (" + i + ") ";
            }

            RoutePlan rp = new RoutePlan();
            dtDoctor = rp.get_ListedDoctor_Territorywise(Session["sf_code"].ToString(), ddlTerritory.SelectedValue.ToString(), lblDocCode.Text);
            if (dtDoctor != null)
            {
                foreach (DataRow drDoc in dtDoctor.Rows)
                {
                    catg4_cnt  = catg4_cnt + 1;
                    chkSelect.Checked = true;
                }
            }
            if (dtDoctor.Rows.Count == 0)
            {
                if (multidoc == 0)
                {
                    if (ismap == true)
                    {
                        e.Row.Enabled = false;
                        e.Row.BackColor = System.Drawing.Color.PapayaWhip;
                    }
                    else
                    {
                        if (lblDocName.Text.Trim().Length > 0)
                        {
                            e.Row.Enabled = true;
                            e.Row.BackColor = System.Drawing.Color.LightGreen;
                        }
                    }
                }

            }
            
          
            
            if (lblDocCode.Text.Trim() == "0")
            {
                chkSelect.Visible = false;
                lblSNo.Visible = false;
            }

            dsTerr = rp.get_ListedDoctor_TerritoryCode(Session["sf_code"].ToString(), lblDocCode.Text);
            if (dsTerr.Tables[0].Rows.Count > 0)
            {
                strTerritoryCode = dsTerr.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
            }
            string[] strStateSplit = strTerritoryCode.Split(',');
            docvisit = 0;
            foreach (string strstate in strStateSplit)
            {
                if (strstate != "")
                {
                    if (strstate == ddlTerritory.SelectedValue.ToString())
                    {
                        terrexist = true;
                    }
                    docvisit = docvisit + 1;
                }

            }
            if (docvisit.ToString() == ViewState["visit4"].ToString())
            {
                if (terrexist == false)
                {
                    e.Row.Enabled = false;
                    e.Row.BackColor = System.Drawing.Color.LightCoral;
                }
            }

            ViewState["catg4_cnt"] = catg4_cnt.ToString();
        }
    }

    protected void grdDoctor5_RowDataBound(object sender, GridViewRowEventArgs e)
    {
         int catg5_cnt  = 0;
         bool ismap = false;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            i = 0;
            CheckBox chkSelect = (CheckBox)e.Row.FindControl("chkSelect");
            Label lblDocCode = (Label)e.Row.FindControl("lblDocCode");
            Label lblSNo = (Label)e.Row.FindControl("lblSNo");

            Label lblTerrCode = (Label)e.Row.FindControl("lblTerrCode");
            Label lblDocName = (Label)e.Row.FindControl("lblDocName");

            if (lblTerrCode.Text.Trim().Length > 0)
            {
                terr_cd = lblTerrCode.Text.Trim().Split(',');
                foreach (string terr_code in terr_cd)
                {
                    if (terr_code.Trim().Length > 0)
                    {
                        ismap = true;
                        i = i + 1;
                    }
                }

                lblDocName.Text = lblDocName.Text + " (" + i + ") ";
            }

            RoutePlan rp = new RoutePlan();
            dtDoctor = rp.get_ListedDoctor_Territorywise(Session["sf_code"].ToString(), ddlTerritory.SelectedValue.ToString(), lblDocCode.Text);
            if (dtDoctor != null)
            {
                foreach (DataRow drDoc in dtDoctor.Rows)
                {
                    catg5_cnt = catg5_cnt + 1;
                    chkSelect.Checked = true;
                }
            }
            if (dtDoctor.Rows.Count == 0)
            {
                if (multidoc == 0)
                {
                    if (ismap == true)
                    {
                        e.Row.Enabled = false;
                        e.Row.BackColor = System.Drawing.Color.PapayaWhip;
                    }
                    else
                    {
                        if (lblDocName.Text.Trim().Length > 0)
                        {
                            e.Row.Enabled = true;
                            e.Row.BackColor = System.Drawing.Color.LightGreen;
                        }
                    }
                }

            }
            
          
            
            if (lblDocCode.Text.Trim() == "0")
            {
                chkSelect.Visible = false;
                lblSNo.Visible = false;
            }

            dsTerr = rp.get_ListedDoctor_TerritoryCode(Session["sf_code"].ToString(), lblDocCode.Text);
            if (dsTerr.Tables[0].Rows.Count > 0)
            {
                strTerritoryCode = dsTerr.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
            }
            string[] strStateSplit = strTerritoryCode.Split(',');
            docvisit = 0;
            foreach (string strstate in strStateSplit)
            {
                if (strstate != "")
                {
                    if (strstate == ddlTerritory.SelectedValue.ToString())
                    {
                        terrexist = true;
                    }
                    docvisit = docvisit + 1;
                }

            }
            if (docvisit.ToString() == ViewState["visit5"].ToString())
            {
                if (terrexist == false)
                {
                    e.Row.Enabled = false;
                    e.Row.BackColor = System.Drawing.Color.LightCoral;
                }
            }

            ViewState["catg5_cnt"] = catg5_cnt.ToString();
        }
    }

    protected void grdDoctor6_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        int catg6_cnt = 0;
        bool ismap = false;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            i = 0;
            CheckBox chkSelect = (CheckBox)e.Row.FindControl("chkSelect");
            Label lblDocCode = (Label)e.Row.FindControl("lblDocCode");
            Label lblSNo = (Label)e.Row.FindControl("lblSNo");

            Label lblTerrCode = (Label)e.Row.FindControl("lblTerrCode");
            Label lblDocName = (Label)e.Row.FindControl("lblDocName");

            if (lblTerrCode.Text.Trim().Length > 0)
            {
                terr_cd = lblTerrCode.Text.Trim().Split(',');
                foreach (string terr_code in terr_cd)
                {
                    if (terr_code.Trim().Length > 0)
                    {
                        ismap = true;
                        i = i + 1;
                    }
                }

                lblDocName.Text = lblDocName.Text + " (" + i + ") ";
            }

            RoutePlan rp = new RoutePlan();
            dtDoctor = rp.get_ListedDoctor_Territorywise(Session["sf_code"].ToString(), ddlTerritory.SelectedValue.ToString(), lblDocCode.Text);
            if (dtDoctor != null)
            {
                foreach (DataRow drDoc in dtDoctor.Rows)
                {
                     catg6_cnt = catg6_cnt + 1;
                    chkSelect.Checked = true;
                }
            }
            if (dtDoctor.Rows.Count == 0)
            {
                if (multidoc == 0)
                {
                    if (ismap == true)
                    {
                        e.Row.Enabled = false;
                        e.Row.BackColor = System.Drawing.Color.PapayaWhip;
                    }
                    else
                    {
                        if (lblDocName.Text.Trim().Length > 0)
                        {
                            e.Row.Enabled = true;
                            e.Row.BackColor = System.Drawing.Color.LightGreen;
                        }
                    }
                }

            }
            
          
            
            if (lblDocCode.Text.Trim() == "0")
            {
                chkSelect.Visible = false;
                lblSNo.Visible = false;
            }

            dsTerr = rp.get_ListedDoctor_TerritoryCode(Session["sf_code"].ToString(), lblDocCode.Text);
            if (dsTerr.Tables[0].Rows.Count > 0)
            {
                strTerritoryCode = dsTerr.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
            }
            string[] strStateSplit = strTerritoryCode.Split(',');
            docvisit = 0;
            foreach (string strstate in strStateSplit)
            {
                if (strstate != "")
                {
                    if (strstate == ddlTerritory.SelectedValue.ToString())
                    {
                        terrexist = true;
                    }
                    docvisit = docvisit + 1;
                }

            }
            if (docvisit.ToString() == ViewState["visit6"].ToString())
            {
                if (terrexist == false)
                {
                    e.Row.Enabled = false;
                    e.Row.BackColor = System.Drawing.Color.LightCoral;
                }
            }

            ViewState["catg6_cnt"] = catg6_cnt.ToString();
        }
    }

    private void FillDoc(string Terr_Code)
    {
        int icount = 1;
        int maxrows1 = 0;
        int maxrows2 = 0;
        int maxrows3 = 0;
        int maxrows4 = 0;
        int maxrows5 = 0;
        int maxrows6 = 0;

        dtDoctor1 = null;
        dtDoctor2 = null;
        dtDoctor3 = null;
        dtDoctor4 = null;
        dtDoctor5 = null;
        dtDoctor6 = null;

        RoutePlan LstDoc = new RoutePlan();
        dtCatg = LstDoc.get_catg_SF(sf_code);
        ViewState["dt_catg"] = dtCatg;
        if (dtCatg != null)
        {
            ViewState["dt_catg"] = dtCatg;
            btnSubmit.Visible = true;
            foreach (DataRow drFF in dtCatg.Rows)
            {
                if (icount == 1)
                {
                    lblCatg1.Text = drFF["Doc_Cat_SName"].ToString() + " ( " + drFF["No_of_visit"].ToString() + " ) ";
                    ViewState["visit1"] = drFF["No_of_visit"].ToString();
                    dtDoctor = LstDoc.get_ListedDoctor_Territory_Catg(sf_code, drFF["Doc_Cat_Code"].ToString());

                    if (dtDoctor != null)
                    {
                        //grdDoctor1.Visible = true;
                        //grdDoctor1.DataSource = dtDoctor;
                        //grdDoctor1.DataBind();
                        dtDoctor1 = dtDoctor;
                        maxrows1 = dtDoctor.Rows.Count;                        
                        maxrows = maxrows1;
                    }

                }
                else if (icount == 2)
                {
                    lblCatg2.Text = drFF["Doc_Cat_SName"].ToString() + " ( " + drFF["No_of_visit"].ToString() + " ) ";
                    ViewState["visit2"] = drFF["No_of_visit"].ToString();
                    dtDoctor = LstDoc.get_ListedDoctor_Territory_Catg(sf_code, drFF["Doc_Cat_Code"].ToString());

                    if (dtDoctor != null)
                    {
                        //grdDoctor2.Visible = true;
                        //grdDoctor2.DataSource = dtDoctor;
                        //grdDoctor2.DataBind();
                        dtDoctor2 = dtDoctor;
                        maxrows2 = dtDoctor.Rows.Count;
                        if (maxrows < maxrows2)
                            maxrows = maxrows2;
                    }

                }
                else if (icount == 3)
                {
                    lblCatg3.Text = drFF["Doc_Cat_SName"].ToString() + " ( " + drFF["No_of_visit"].ToString() + " ) ";
                    ViewState["visit3"] = drFF["No_of_visit"].ToString();
                    dtDoctor = LstDoc.get_ListedDoctor_Territory_Catg(sf_code, drFF["Doc_Cat_Code"].ToString());

                    if (dtDoctor != null)
                    {
                        //grdDoctor3.Visible = true;
                        //grdDoctor3.DataSource = dtDoctor;
                        //grdDoctor3.DataBind();
                        dtDoctor3 = dtDoctor;
                        maxrows3 = dtDoctor.Rows.Count;
                        if (maxrows < maxrows3)
                            maxrows = maxrows3;
                    }

                }
                else if (icount == 4)
                {
                    lblCatg4.Text = drFF["Doc_Cat_SName"].ToString() + " ( " + drFF["No_of_visit"].ToString() + " ) ";
                    ViewState["visit4"] = drFF["No_of_visit"].ToString();
                    dtDoctor = LstDoc.get_ListedDoctor_Territory_Catg(sf_code, drFF["Doc_Cat_Code"].ToString());

                    if (dtDoctor != null)
                    {
                        //grdDoctor4.Visible = true;
                        //grdDoctor4.DataSource = dtDoctor;
                        //grdDoctor4.DataBind();
                        dtDoctor4 = dtDoctor;
                        maxrows4 = dtDoctor.Rows.Count;
                        if (maxrows < maxrows4)
                            maxrows = maxrows4;

                    }

                }
                else if (icount == 5)
                {
                    lblCatg5.Text = drFF["Doc_Cat_SName"].ToString() + " ( " + drFF["No_of_visit"].ToString() + " ) ";
                    ViewState["visit5"] = drFF["No_of_visit"].ToString();
                    dtDoctor = LstDoc.get_ListedDoctor_Territory_Catg(sf_code, drFF["Doc_Cat_Code"].ToString());

                    if (dtDoctor != null)
                    {
                        //grdDoctor5.Visible = true;
                        //grdDoctor5.DataSource = dtDoctor;
                        //grdDoctor5.DataBind();
                        dtDoctor5 = dtDoctor;
                        maxrows5 = dtDoctor.Rows.Count;
                        if (maxrows < maxrows5)
                            maxrows = maxrows5;

                    }

                }
                else if (icount == 6)
                {
                    lblCatg6.Text = drFF["Doc_Cat_SName"].ToString() + " ( " + drFF["No_of_visit"].ToString() + " ) ";
                    ViewState["visit6"] = drFF["No_of_visit"].ToString();
                    dtDoctor = LstDoc.get_ListedDoctor_Territory_Catg(sf_code, drFF["Doc_Cat_Code"].ToString());

                    if (dtDoctor != null)
                    {
                        //grdDoctor6.Visible = true;
                        //grdDoctor6.DataSource = dtDoctor;
                        //grdDoctor6.DataBind();
                        dtDoctor6 = dtDoctor;
                        maxrows6 = dtDoctor.Rows.Count;
                        if (maxrows < maxrows6)
                            maxrows = maxrows6;

                    }
                }

                icount = icount + 1;

            }

        }

        if (dtDoctor1 != null)
        {
            if (maxrows > dtDoctor1.Rows.Count)
            {
                currow = dtDoctor1.Rows.Count;
                while (currow < maxrows)
                {
                    dtDoctor1.Rows.Add(0, "", "");
                    currow = currow + 1;
                }
            }

            grdDoctor1.Visible = true;
            grdDoctor1.DataSource = dtDoctor1;
            grdDoctor1.DataBind();
        }

        if (dtDoctor2 != null)
        {
            if (maxrows > dtDoctor2.Rows.Count)
            {
                currow = dtDoctor2.Rows.Count;
                while (currow < maxrows)
                {
                    dtDoctor2.Rows.Add(0, "", "");
                    currow = currow + 1;
                }
            }

            grdDoctor2.Visible = true;
            grdDoctor2.DataSource = dtDoctor2;
            grdDoctor2.DataBind();
        }

        if (dtDoctor3 != null)
        {
            if (maxrows > dtDoctor3.Rows.Count)
            {
                currow = dtDoctor3.Rows.Count;
                while (currow < maxrows)
                {
                    dtDoctor3.Rows.Add(0, "", "");
                    currow = currow + 1;
                }
            }

            grdDoctor3.Visible = true;
            grdDoctor3.DataSource = dtDoctor3;
            grdDoctor3.DataBind();
        }
        if (dtDoctor4 != null)
        {
            if (maxrows > dtDoctor4.Rows.Count)
            {
                currow = dtDoctor4.Rows.Count;
                while (currow < maxrows)
                {
                    dtDoctor4.Rows.Add(0, "", "");
                    currow = currow + 1;
                }
            }

            grdDoctor4.Visible = true;
            grdDoctor4.DataSource = dtDoctor4;
            grdDoctor4.DataBind();
        }
        if (dtDoctor5 != null)
        {
            if (maxrows > dtDoctor5.Rows.Count)
            {
                currow = dtDoctor5.Rows.Count;
                while (currow < maxrows)
                {
                    dtDoctor5.Rows.Add(0, "", "");
                    currow = currow + 1;
                }
            }

            grdDoctor5.Visible = true;
            grdDoctor5.DataSource = dtDoctor5;
            grdDoctor5.DataBind();
        }
        if (dtDoctor6 != null)
        {
            if (maxrows > dtDoctor6.Rows.Count)
            {
                currow = dtDoctor6.Rows.Count;
                while (currow < maxrows)
                {
                    dtDoctor6.Rows.Add(0, "", "");
                    currow = currow + 1;
                }
            }

            grdDoctor6.Visible = true;
            grdDoctor6.DataSource = dtDoctor6;
            grdDoctor6.DataBind();
        }


    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        int iReturn = -1;
        foreach (GridViewRow gridRow in grdDoctor1.Rows)
        {
           
            CheckBox chkSelect = (CheckBox)gridRow.Cells[1].FindControl("chkSelect");
            Label lblDocCode = (Label)gridRow.Cells[1].FindControl("lblDocCode"); 
            iReturn =  UpdatePlan(lblDocCode.Text,chkSelect.Checked);
           
        }

        //2nd grid

        foreach (GridViewRow gridRow in grdDoctor2.Rows)
        {

            CheckBox chkSelect = (CheckBox)gridRow.Cells[1].FindControl("chkSelect");
            Label lblDocCode = (Label)gridRow.Cells[1].FindControl("lblDocCode");
            iReturn =  UpdatePlan(lblDocCode.Text,chkSelect.Checked);   
        }

        //3rd grid

        foreach (GridViewRow gridRow in grdDoctor3.Rows)
        {

            CheckBox chkSelect = (CheckBox)gridRow.Cells[1].FindControl("chkSelect");
            Label lblDocCode = (Label)gridRow.Cells[1].FindControl("lblDocCode");
           iReturn =  UpdatePlan(lblDocCode.Text,chkSelect.Checked);   
        }

        //4th grid

        foreach (GridViewRow gridRow in grdDoctor4.Rows)
        {

            CheckBox chkSelect = (CheckBox)gridRow.Cells[1].FindControl("chkSelect");
            Label lblDocCode = (Label)gridRow.Cells[1].FindControl("lblDocCode");
            iReturn =  UpdatePlan(lblDocCode.Text,chkSelect.Checked);   
        }

        //5th grid

        foreach (GridViewRow gridRow in grdDoctor5.Rows)
        {

            CheckBox chkSelect = (CheckBox)gridRow.Cells[1].FindControl("chkSelect");
            Label lblDocCode = (Label)gridRow.Cells[1].FindControl("lblDocCode");
            iReturn =  UpdatePlan(lblDocCode.Text,chkSelect.Checked);   
        }

        //6th grid

        foreach (GridViewRow gridRow in grdDoctor6.Rows)
        {

            CheckBox chkSelect = (CheckBox)gridRow.Cells[1].FindControl("chkSelect");
            Label lblDocCode = (Label)gridRow.Cells[1].FindControl("lblDocCode");
            iReturn =  UpdatePlan(lblDocCode.Text,chkSelect.Checked);   
        }
        if (iReturn != 0)
        {
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Updated successfully');</script>");
            FillDoc(ddlTerritory.SelectedValue.ToString());
            RoutePlan cp = new RoutePlan();

            int MissedCount = cp.getMissedDr(sf_code);
            if (MissedCount > 0)
            {
                lblAllocate.Text = MissedCount.ToString() + " Missed Doctor(s)  in Plan";
            }
            else
            {
                lblAllocate.Text = "All Drs have been Allocated";
            }
      
        }

           
    }
    private int UpdatePlan(string DocCode, bool IsChecked)
    {
        terrexist = false;
        string strTerrCode = string.Empty;
        string strTerrCode_new = string.Empty;
        int iReturn = -1;
        RoutePlan rp = new RoutePlan();

        dsTerr = rp.get_ListedDoctor_TerritoryCode(Session["sf_code"].ToString(), DocCode);
        if (dsTerr.Tables[0].Rows.Count > 0)
        {
            strTerrCode = dsTerr.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
        }
        if (multidoc == 0)
        {
            if (IsChecked)
            {
                if (strTerrCode != ddlTerritory.SelectedValue.ToString())
                {
                    strTerrCode_new = ddlTerritory.SelectedValue.ToString();
                    iReturn = rp.Edit_Lstdoc_Plan(Session["sf_code"].ToString(), strTerrCode_new.ToString(), DocCode);
                }
            }
            else
            {
                if (strTerrCode == ddlTerritory.SelectedValue.ToString())
                {
                    strTerrCode_new = "";
                    iReturn = rp.Edit_Lstdoc_Plan(Session["sf_code"].ToString(), strTerrCode_new.ToString(), DocCode);
                }
            }
        }
        else
        {
            string[] strStateSplit = strTerrCode.Split(',');
            foreach (string strstate in strStateSplit)
            {
                if (strstate != "")
                {
                    if (strstate == ddlTerritory.SelectedValue.ToString())
                    {
                        terrexist = true;
                    }
                }
            }

            if (IsChecked)
            {
                if (terrexist == false)
                {
                    if (strTerrCode.IndexOf(",") != -1)
                    {
                        strTerrCode_new = strTerrCode + ddlTerritory.SelectedValue.ToString() + ",";
                    }
                    else
                    {
                        strTerrCode_new = strTerrCode + "," + ddlTerritory.SelectedValue.ToString() + ",";

                    }
                    //update strterrcode 
                    iReturn = rp.Edit_Lstdoc_Plan(Session["sf_code"].ToString(), strTerrCode_new.ToString(), DocCode);
                }

                // Check if ddl-terrcode exist in strTerrCode. 
                //if exist
                // do nothing
                //else
                //append ddl-terrcode in strTerrCode
            }
            else
            {
                // Check if ddl-terrcode exist in strTerrCode. 
                //if exist
                // remove terrcode from strTerrCode
                //else
                //do nothing
                if (terrexist)
                {
                    if (strTerrCode.IndexOf(",") != -1)
                    {
                        strTerrCode_new = strTerrCode.Replace(ddlTerritory.SelectedValue.ToString() + ",", "");
                    }
                    else
                    {
                        strTerrCode_new = strTerrCode.Replace(ddlTerritory.SelectedValue.ToString(), "");
                    }
                    strTerrCode_new = strTerrCode_new.Trim();
                    iReturn = rp.Edit_Lstdoc_Plan(Session["sf_code"].ToString(), strTerrCode_new.ToString(), DocCode);

                }

            }

        }
        return iReturn;
    }

}