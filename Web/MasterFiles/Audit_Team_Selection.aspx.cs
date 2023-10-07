using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
using System.Drawing.Imaging;

public partial class MasterFiles_Audit_Team_Selection : System.Web.UI.Page
{
    #region "Declaration"
    DataSet dsUserList = null;
    DataSet dsDivision = null;
    DataSet ds = null;
    string div_code = string.Empty;
    string ProdCode = string.Empty;
    string ProdSaleUnit = string.Empty;
    string ProdName = string.Empty;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    string sf_code = string.Empty;
    string ff = string.Empty;
    string fieldforce = string.Empty;
    string sfname = string.Empty;
    string div_codes = string.Empty;
    DataSet dsmgr = null;
    DataSet dsAT = null;
    DataSet dsAtm = null;
    string Auditteammgr = string.Empty;
    string division_name = string.Empty;
    
    int time;
    int div;
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        Session["backurl"] = "Audit_Team.aspx";
        div_code = Session["div_code"].ToString();       
        if (!Page.IsPostBack)
        {
            
            if (Request.QueryString["sfcode"] != null)
            {
                sf_code = Request.QueryString["sfcode"].ToString();
                Session["ff_code"] = sf_code;
            }
            if (Session["ff_code"] != null)
            {
                btnSubmit.Visible = true;
              
                sf_code = Session["ff_code"].ToString();
                
                
               // div_code = div_code;
            }

            //CreateLinkBut();


            SalesForce sf = new SalesForce();
            DataSet dssf = sf.getSfName(sf_code);
            if (dssf.Tables[0].Rows.Count > 0)
            {
                sfname = Convert.ToString(dssf.Tables[0].Rows[0].ItemArray.GetValue(0).ToString());
            }

            menu1.Title = sfname + " - " + this.Page.Title;
            //menu1.FindControl("btnBack").Visible = false;
            if (div_code != "")
            {
                FillUserList();
                FillAuditTeam();
            }
            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);
        }
          
            FillgridColor();
        
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
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
           
            
        }
    }


    private void FillUserList()
    {

        string sMgr = Session["ff_code"].ToString();
        SalesForce sf = new SalesForce();
        dsmgr = sf.getReportingTo(sMgr);
        if (dsmgr.Tables[0].Rows.Count > 0)
        {
            DataTable dtUserList = new DataTable();
            ViewState["Auditteammgr"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
            dtUserList = sf.getUserList_Managers_Audit(div_code, dsmgr.Tables[0].Rows[0].ItemArray.GetValue(0).ToString(), 0, sMgr);
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
    }
    private void FillgridColor()
    {
        foreach (GridViewRow grid_row in grdSalesForce.Rows)
        {
            Label lblBackColor = (Label)grid_row.FindControl("lblBackColor");
            string bcolor = "#" + lblBackColor.Text;
            grid_row.BackColor = System.Drawing.Color.FromName(bcolor);
        }

        // To show  audit team.
        SalesForce sf = new SalesForce();
        dsAT = sf.getAuditTeam(Session["div_code"].ToString());
        if (dsAT.Tables[0].Rows.Count > 0)
        {
            foreach (DataRow drFF in dsAT.Tables[0].Rows)
            {
                if (drFF["sf_code"].ToString() != Session["ff_code"].ToString())
                {
                    dsAtm = sf.getAuditTeam(drFF["sf_code"].ToString(), Session["div_code"].ToString());
                    string AuditMgr = dsAtm.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                    string[] Audit;
                    Audit = AuditMgr.Split(',');
                    foreach (GridViewRow grid_row in grdSalesForce.Rows)
                    {
                        foreach (string Au_cd in Audit)
                        {
                            Label lblsfCode = (Label)grid_row.FindControl("lblsfCode");
                            Label lblFieldForce = (Label)grid_row.FindControl("lblSfName");
                            if (Au_cd == lblsfCode.Text)
                            {
                                // grid_row.BackColor = System.Drawing.Color.Yellow;
                                lblFieldForce.ForeColor = System.Drawing.Color.White;
                                lblFieldForce.BackColor = System.Drawing.Color.Green;
                                //grid_row.Enabled = false;
                                //dsUserList = sf.UserList(div_code, lblsfCode.Text);
                                //foreach (DataRow drUser in dsUserList.Tables[0].Rows)
                                //{
                                //    foreach (GridViewRow grid in grdSalesForce.Rows)
                                //    {
                                //        CheckBox chk_sfCode = (CheckBox)grid.Cells[1].FindControl("chksf");
                                //        Label lbl_sfCode = (Label)grid.Cells[2].FindControl("lblsfCode");
                                //        if (drUser["sf_code"].ToString().Trim() == lbl_sfCode.Text.Trim())
                                //        {
                                //            grid.Enabled = false;
                                //        }
                                //    }
                                //}

                                // lblFieldForce.Style.Add("font-size", "12pt");
                                //  lblFieldForce.Style.Add("font-weight", "Bold");
                            }
                        }
                    }
                }
            }
        }
    }
    private void FillAuditTeam()
    {
        foreach (GridViewRow grid_row in grdSalesForce.Rows)
        {
            // To show existing audit team.
            SalesForce sf = new SalesForce();
            dsAT = sf.getAuditTeam(Session["ff_code"].ToString(), Session["div_code"].ToString());
            if (dsAT.Tables[0].Rows.Count > 0)
            {
                string AuditMgr = dsAT.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                string[] Audit;
                Audit = AuditMgr.Split(',');
                foreach (string Au_cd in Audit)
                {
                    Label lblsfCode = (Label)grid_row.FindControl("lblsfCode");
                    if (Au_cd == lblsfCode.Text)
                    {
                        CheckBox chksf = (CheckBox)grid_row.FindControl("chksf");
                        chksf.Checked = true;
                        dsUserList = sf.UserList(div_code, lblsfCode.Text);
                        foreach (DataRow drUser in dsUserList.Tables[0].Rows)
                        {
                            foreach (GridViewRow grid in grdSalesForce.Rows)
                            {
                                CheckBox chk_sfCode = (CheckBox)grid.Cells[1].FindControl("chksf");
                                Label lbl_sfCode = (Label)grid.Cells[2].FindControl("lblsfCode");
                                if (drUser["sf_code"].ToString().Trim() == lbl_sfCode.Text.Trim())
                                {
                                    grid.Enabled = false;
                                }
                            }
                        }

                    }
                }
            }
        }
    }
    protected void chksf_CheckedChanged(object sender, EventArgs e)
    {
       
        CheckBox chksf = (CheckBox)sender;
        GridViewRow gridRow = (GridViewRow)chksf.Parent.Parent;
        //string sTest = GrdCopyMove.DataKeys[gr.RowIndex].Value.ToString();

        Label lblsfCode = (Label)gridRow.Cells[2].FindControl("lblsfCode");

         SalesForce sf = new SalesForce();
        dsUserList = sf.UserList(div_code, lblsfCode.Text);

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
        iReturn = sf.CreateAuditMgr(Session["ff_code"].ToString(), fieldforce, ViewState["Auditteammgr"].ToString(), Session["div_code"].ToString());
        //if (isentered == false)
        //{
        //    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select');</script>");
        //}
        if (iReturn > 0)
        {
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Updated Successfully');</script>");
            FillUserList();
            FillAuditTeam();
            FillgridColor();
        }
    }
    
}