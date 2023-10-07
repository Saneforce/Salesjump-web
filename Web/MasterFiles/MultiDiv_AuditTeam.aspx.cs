using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
using System.Drawing.Imaging;

public partial class MasterFiles_MultiDiv_AuditTeam : System.Web.UI.Page
{
    #region "Declaration"
    DataSet dsUserList = null;
    DataSet dsDivision = null;
    string div_code = string.Empty;
    string ProdCode = string.Empty;
    string ProdSaleUnit = string.Empty;
    string ProdName = string.Empty;
    DateTime ServerStartTime;
    string sMgr = string.Empty;
    DateTime ServerEndTime;
    string sf_code = string.Empty;
    string ff = string.Empty;
    string fieldforce = string.Empty;
    string sfname = string.Empty;
    string div_codes = string.Empty;
    string[] divcodes;
    string division_name = string.Empty;
    string division_code = string.Empty;
    int time;
    int div;
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        Session["backurl"] = "MultiDivision.aspx";
        if (!Page.IsPostBack)
        {
            lblSelect.Visible = true;
            if (Request.QueryString["sfcode"] != null)
            {
                sf_code = Request.QueryString["sfcode"].ToString();
                Session["ff_code"] = sf_code;
            }
            if (Session["ff_code"] != null)
            {
                btnSubmit.Visible = true;
                lblFilter.Visible = true;
                ddlFilter.Visible = true;
                btnGo.Visible = true;
                sf_code = Session["ff_code"].ToString();

                div = Convert.ToInt16(Request.QueryString["div"].ToString());
                if (div == 1)
                {
                    division_code = Session["div_code_1"].ToString();
                }
                else if (div == 2)
                {
                    division_code = Session["div_code_2"].ToString();
                }
                else if (div == 3)
                {
                    division_code = Session["div_code_3"].ToString();
                }
                else if (div == 4)
                {
                    division_code = Session["div_code_4"].ToString();
                }
                else if (div == 5)
                {
                    division_code = Session["div_code_5"].ToString();
                }
                else if (div == 6)
                {
                    division_code = Session["div_code_6"].ToString();
                }
                Session["division_code"] = division_code;

            }

            //CreateLinkBut();

            FillDivisionLinks();
            SalesForce sf = new SalesForce();
            DataSet dssf = sf.getSfName(sf_code);
            if (dssf.Tables[0].Rows.Count > 0)
            {
                sfname = Convert.ToString(dssf.Tables[0].Rows[0].ItemArray.GetValue(0).ToString());
            }

            menu1.Title = sfname + " - " + this.Page.Title;
            //menu1.FindControl("btnBack").Visible = false;
            if (division_code != "")
            {
                FillUserList();
                FillReporting();
            }
            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);
        }
            FillColor();
            FillgridColor();
        
    }
    private void FillColor()
    {
        int j = 0;


        foreach (ListItem ColorItems in ddlSF.Items)
        {
            string bcolor = "#" + ColorItems.Text;
            ddlFilter.Items[j].Attributes.Add("style", "background-color:" + bcolor);
           
            j = j + 1;

        }
    }

    private void FillDivisionLinks()
    {
        int i = 1;
        Division dv = new Division();
        dsUserList = dv.getDivision_List(sf_code);
        if (dsUserList.Tables[0].Rows.Count > 0)
            div_codes = dsUserList.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

        if (div_codes.Trim().Length > 0)
        {
            divcodes = div_codes.Split(',');
            foreach (string div_cd in divcodes)
            {
                division_name = "";
                dsDivision = dv.getDivision_Name(div_cd);
                if (dsDivision.Tables[0].Rows.Count > 0)
                    division_name = dsDivision.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

                if (division_name.Trim().Length > 0)
                {
                    if (i == 1)
                    {
                        hypDiv1.Text = division_name;
                        Session["div_code_1"] = div_cd;
                    }
                    else if (i == 2)
                    {
                        hypDiv2.Text = division_name;
                        Session["div_code_2"] = div_cd;
                    }
                    else if (i == 3)
                    {
                        hypDiv3.Text = division_name;
                        Session["div_code_3"] = div_cd;
                    }
                    else if (i == 4)
                    {
                        hypDiv4.Text = division_name;
                        Session["div_code_4"] = div_cd;
                    }
                    else if (i == 5)
                    {
                        hypDiv5.Text = division_name;
                        Session["div_code_5"] = div_cd;
                    }
                    else if (i == 6)
                    {
                        hypDiv6.Text = division_name;
                        Session["div_code_6"] = div_cd;
                    }

                }

                i = i + 1;
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

    protected void grdSalesForce_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        
    }


    private void FillUserList()
    {
        lblSelect.Visible = false;
        sMgr = "admin";
        if (ddlFilter.SelectedIndex > 0)
        {
            sMgr = ddlFilter.SelectedValue;
        }
        SalesForce sf = new SalesForce();
        //dsUserList = sf.UserList(div_code, "admin");
        //dsUserList = sf.UserList_Self(division_code, sMgr);
        DataTable dtUserList = new DataTable();
        dtUserList = sf.getUserList_Managers(division_code, sMgr, 0);
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
    private void FillgridColor()
    {

        foreach (GridViewRow grid_row in grdSalesForce.Rows)
        {
           
            Label lblBackColor = (Label)grid_row.FindControl("lblBackColor");
            string bcolor = "#" + lblBackColor.Text;
            grid_row.BackColor = System.Drawing.Color.FromName(bcolor);

        }
    }
    private void FillReporting()
    {
        SalesForce sf = new SalesForce();
        dsUserList = sf.UserList_Hierarchy(division_code,"admin");
        if (dsUserList.Tables[0].Rows.Count > 0)
        {
            ddlFilter.DataTextField = "Sf_Name";
            ddlFilter.DataValueField = "Sf_Code";
            ddlFilter.DataSource = dsUserList;
            ddlFilter.DataBind();

            ddlSF.DataTextField = "des_color";
            ddlSF.DataValueField = "sf_code";
            ddlSF.DataSource = dsUserList;
            ddlSF.DataBind();

        }
    }
    protected void btnGo_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        division_code = getDivision();
        FillUserList();
        FillgridColor();
    }

    private string getDivision()
    {
        div = Convert.ToInt16(Request.QueryString["div"].ToString());
        if (div == 1)
        {
            division_code = Session["div_code_1"].ToString();
        }
        else if (div == 2)
        {
            division_code = Session["div_code_2"].ToString();
        }
        else if (div == 3)
        {
            division_code = Session["div_code_3"].ToString();
        }
        else if (div == 4)
        {
            division_code = Session["div_code_4"].ToString();
        }
        else if (div == 5)
        {
            division_code = Session["div_code_5"].ToString();
        }
        else if (div == 6)
        {
            division_code = Session["div_code_6"].ToString();
        }
        return division_code;
    }

    protected void chksf_CheckedChanged(object sender, EventArgs e)
    {
        lblSelect.Visible = false;
        CheckBox chksf = (CheckBox)sender;
        GridViewRow gridRow = (GridViewRow)chksf.Parent.Parent;
        //string sTest = GrdCopyMove.DataKeys[gr.RowIndex].Value.ToString();

        Label lblsfCode = (Label)gridRow.Cells[2].FindControl("lblsfCode");

        division_code = getDivision();

        SalesForce sf = new SalesForce();
        dsUserList = sf.UserList(division_code, lblsfCode.Text);

        if (chksf.Checked == true)
        {
            if (dsUserList.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow drUser in dsUserList.Tables[0].Rows)
                {
                    foreach (GridViewRow grid_row in grdSalesForce.Rows)
                    {
                        CheckBox chk_sfCode = (CheckBox)grid_row.Cells[1].FindControl("chksf");
                        Label lbl_sfCode = (Label)grid_row.Cells[2].FindControl("lblsfCode");
                        if (drUser["sf_code"].ToString().Trim() == lbl_sfCode.Text.Trim())
                        {
                            chk_sfCode.Checked = false;
                            grid_row.Enabled = false;
                        }
                    } 
                }
                
            }
        }
        else
        {
            if (dsUserList.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow drUser in dsUserList.Tables[0].Rows)
                {
                    foreach (GridViewRow grid_row in grdSalesForce.Rows)
                    {
                        CheckBox chk_sfCode = (CheckBox)grid_row.Cells[1].FindControl("chksf");
                        Label lbl_sfCode = (Label)grid_row.Cells[2].FindControl("lblsfCode");
                        if (drUser["sf_code"].ToString().Trim() == lbl_sfCode.Text.Trim())
                            grid_row.Enabled = true;                  
                    }
                    
                }
                
            }
        }
        FillgridColor();
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        int iReturn = -1;
        bool isentered = false;
        foreach (GridViewRow gridRow in grdSalesForce.Rows)
        {
            CheckBox chksf = (CheckBox)gridRow.Cells[1].FindControl("chksf");
            Label lblsfCode = (Label)gridRow.Cells[2].FindControl("lblsfCode");

            if (chksf.Checked)
            {
                isentered = true;
                ff = lblsfCode.Text.ToString();
                fieldforce  = fieldforce + ff +',';
            }
        }
        
        FillgridColor();
        SalesForce sf = new SalesForce();
        iReturn = sf.CreateAuditMgr(Session["ff_code"].ToString(), fieldforce,sMgr, Session["division_code"].ToString());
        if (isentered == false)
        {
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select');</script>");
        }
        if (iReturn > 0)
        {
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Updated Successfully');</script>");
            ClearGrid();
        }
    }
    private void ClearGrid()
    {
        foreach (GridViewRow gridRow in grdSalesForce.Rows)
        {
            CheckBox chksf = (CheckBox)gridRow.Cells[1].FindControl("chksf");
            chksf.Checked = false;
        }
    }
    
}