using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
using System.Drawing.Imaging;
using System.IO;
using iTextSharp.text.pdf;
using iTextSharp.text.html;
using iTextSharp.text.html.simpleparser;
using System.Text;
using System.Web.UI.HtmlControls;
using iTextSharp.text;
using System.Net;
using System.Web.UI.DataVisualization.Charting;
using DBase_EReport;


public partial class MasterFiles_User_List : System.Web.UI.Page
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
        int time;
    #endregion
        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);
            sf_type = Session["sf_type"].ToString();
            if (sf_type == "3")
            {
                this.MasterPageFile = "~/Master.master";
                //sf_code = Session["sf_code"].ToString();
            }
            else if (sf_type == "2")
            {
                this.MasterPageFile = "~/Master_MGR.master";
                //sf_code = Session["sf_code"].ToString();
            }
            else if (sf_type == "1")
            {
                this.MasterPageFile = "~/Master_MR.master";
                //sf_code = Session["sf_code"].ToString();
            }
        }
    protected void Page_Load(object sender, EventArgs e)
    {
        sf_code = Session["sf_code"].ToString();
        sf_type = Session["sf_type"].ToString();
        if (sf_type == "3")
        {
            div_code = Session["division_code"].ToString();
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
                btnmgrgo.Visible = false;
            }
            else if(Session["sf_type"].ToString() == "2")
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
                        btnmgrgo.Visible = true;
                        BindUserList();
                    }
                }
            }
        }

        if (Session["sf_type"].ToString() == "2")
        {
           // UserControl_MGR_Menu c1 =
           //(UserControl_MGR_Menu)LoadControl("~/UserControl/MGR_Menu.ascx");
           // Divid.Controls.Add(c1);
           // c1.Title = Page.Title;
           // c1.FindControl("btnBack").Visible = false;
            lblFilter.Visible = false;
            ddlFFType.Visible = false;
            ddlAlpha.Visible = false;
            ddlFieldForce.Visible = false;
            grdSalesForce.Columns[8].Visible = false;
           
        }       
        else if (Session["sf_type"].ToString() == "")
        {
           // UserControl_pnlMenu c1 =
           //(UserControl_pnlMenu)LoadControl("~/UserControl/pnlMenu.ascx");
           // Divid.Controls.Add(c1);
           // c1.Title = Page.Title;
           // c1.FindControl("btnBack").Visible = false;
        }
        else if (Session["sf_type"].ToString() == "3")
        {
           // UserControl_pnlMenu c1 =
           //(UserControl_pnlMenu)LoadControl("~/UserControl/pnlMenu.ascx");
           // Divid.Controls.Add(c1);
           // c1.Title = Page.Title;
           // c1.FindControl("btnBack").Visible = false;
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
             DataSet dsmgrsf = new DataSet();
            SalesForce ds = new SalesForce();
			

            // Check if the manager has a team
            DataSet DsAudit = ds.SF_Hierarchy(div_code, sf_code);
            if (DsAudit.Tables[0].Rows.Count > 0)
            {
            dtUserList = sf.getUserListReportingToNew(div_code, sf_code, 0, Session["sf_type"].ToString());
            }
            else
            {
                // Fetch Managers Audit Team
                dtUserList = ds.getAuditManagerTeam_User(div_code, sf_code, 0);
                //  dsmgrsf.Tables.Add(dt);
                //  dsSalesForce = dsmgrsf;
            }
        }
        else
        {
              DataSet dsmgrsf = new DataSet();
            SalesForce ds = new SalesForce();

            // Check if the manager has a team
            DataSet DsAudit = ds.SF_Hierarchy(div_code, sf_code);
            if (DsAudit.Tables[0].Rows.Count > 0)
            {
            dtUserList = sf.getUserListReportingToAllNew(div_code, sf_code, 0, Session["sf_type"].ToString());
            }
            else
            {
                dtUserList = ds.getAuditManagerTeam_UserAll(div_code, sf_code, 0);
            }
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
                     System.Web.UI.WebControls.ListItem liTerr = new System.Web.UI.WebControls.ListItem();
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
              // Added by Sridevi - To Highlight Managers in UserList - 8-Nov-15
              Label lblSFType = (Label)e.Row.FindControl("lblSFType");
              if (lblSFType.Text  == "2") // Manager
              {
                  e.Row.Style.Add("font-size", "16pt");
                  e.Row.Style.Add("font-weight", "Bold");
              }
              // Ends Here  
              Label lblDrsCnt = (Label)e.Row.FindControl("lblDrsCnt");
              Label lblSF_Code = (Label)e.Row.FindControl("lblSF_Code");
              ListedDR lstdr = new ListedDR();
              DataSet dsdr = new DataSet();
              if (Session["sf_type"].ToString() == "2")
              {
                  div_code = Session["div_code"].ToString();
              }
              else
              {
                  div_code = ddlDivision.SelectedValue;
              }
              if (chkdoctor.Checked == false)
              {
                  dsdr = lstdr.getListDr_CountNew(lblSF_Code.Text, div_code);
                  grdSalesForce.Columns[1].Visible = true;

                  if (dsdr.Tables[0].Rows.Count > 0)
                  {
                      lblDrsCnt.Text = dsdr.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                  }
                  if (lblDrsCnt.Text == "0")
                  {
                      lblDrsCnt.Text = "0";
                  }
              }
              else
              {
                  grdSalesForce.Columns[1].Visible = false;
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


        foreach (System.Web.UI.WebControls.ListItem ColorItems in ddlSF.Items)
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
        //SalesForce sf = new SalesForce();
		loc sf = new loc();
        string strVacant = "1";
        if (chkVacant.Checked == true)
        {
            strVacant = "0";
        }
        //if (ddlFieldForce.SelectedIndex > 0)
        //{
        sMgr = ddlFieldForce.SelectedValue;
        //}
       
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
        pnlprint.Visible = true;
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
        grdSalesForce.Visible = false;
        FillManagers();
        FillColor();
        FillgridColor();
    }
    protected void btnExcel_Click(object sender, EventArgs e)
    {
        string attachment = "attachment; filename=UserList.xls";
        Response.ClearContent();
        Response.AddHeader("content-disposition", attachment);
        Response.ContentType = "application/ms-excel";
        StringWriter sw = new StringWriter();
        HtmlTextWriter htw = new HtmlTextWriter(sw);
        HtmlForm frm = new HtmlForm();
        grdSalesForce.Parent.Controls.Add(frm);
        frm.Attributes["runat"] = "server";
        frm.Controls.Add(grdSalesForce);
        frm.RenderControl(htw);
        Response.Write(sw.ToString());
        Response.End();
    }
    protected void btnPDF_Click(object sender, EventArgs e)
    {
        string strFileName = "UserList";
        Response.ContentType = "application/pdf";
        Response.AddHeader("content-disposition", "attachment;filename=" + strFileName + ".pdf");
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        StringWriter sw = new StringWriter();
        HtmlTextWriter hw = new HtmlTextWriter(sw);
        HtmlForm frm = new HtmlForm();
        grdSalesForce.HeaderRow.Style.Add("font-size", "10px");
        grdSalesForce.Style.Add("text-decoration", "none");
        grdSalesForce.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
        grdSalesForce.Style.Add("font-size", "8px");

        grdSalesForce.Parent.Controls.Add(frm);
        frm.Attributes["runat"] = "server";
        frm.Controls.Add(grdSalesForce);
        frm.RenderControl(hw);
        StringReader sr = new StringReader(sw.ToString());
        //  Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 4f);

        Document pdfDoc = new Document(PageSize.A2, 7f, 7f, 7f, 0f);

        iTextSharp.text.html.simpleparser.HTMLWorker htmlparser = new iTextSharp.text.html.simpleparser.HTMLWorker(pdfDoc);
        PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
        pdfDoc.Open();
        htmlparser.Parse(sr);
        pdfDoc.Close();
        Response.Write(pdfDoc);
        Response.End();
    }
    protected void btnmgrgo_Click(object sender, EventArgs e)
    {
        btnGo_Click(sender, e);
    }
	public class loc
    {
        private string strQry = string.Empty;
        DataTable dt = new DataTable();
        DataTable dt_recursive_Aud = new DataTable();
        DataRow dr = null;
        string Audit_mgr = string.Empty; // Added by Sri - 29Aug15
        string Audit_mgr_All = string.Empty; // Added by Sri - 29Aug15
        int iReturn_Backup = -1;
        public DataTable getUserListReportingToNew(string div_code, string sf_code, int order_id, string sf_type)//23-oct-15
        {
            DB_EReporting db_ER = new DB_EReporting();
            if (order_id == 0)
            {
                DataSet dsmgr = null;

                dt.Columns.Add(new DataColumn("order_id", typeof(int)));
                dt.Columns.Add(new DataColumn("sf_Code", typeof(string)));
                dt.Columns.Add(new DataColumn("sf_Name", typeof(string)));
                dt.Columns.Add(new DataColumn("Sf_UserName", typeof(string)));
                dt.Columns.Add(new DataColumn("Division", typeof(string)));
                dt.Columns.Add(new DataColumn("sf_Type", typeof(string)));
                dt.Columns.Add(new DataColumn("Sf_Joining_Date", typeof(string)));
                dt.Columns.Add(new DataColumn("Reporting_To_SF", typeof(string)));
                dt.Columns.Add(new DataColumn("sf_hq", typeof(string)));
                dt.Columns.Add(new DataColumn("sf_password", typeof(string)));
                dt.Columns.Add(new DataColumn("Designation_Short_Name", typeof(string)));
                dt.Columns.Add(new DataColumn("Desig_Color", typeof(string)));
                dt.Columns.Add(new DataColumn("sf_Tp_Active_flag", typeof(string)));
                dt.Columns.Add(new DataColumn("UsrDfd_UserName", typeof(string)));
                dt.Columns.Add(new DataColumn("Lst_drCount", typeof(string)));
                dt.Columns.Add(new DataColumn("StateName", typeof(string)));
                dt.Columns.Add(new DataColumn("Employee_Id", typeof(string)));
                dt.Columns.Add(new DataColumn("Territory", typeof(string)));
                dt.Columns.Add(new DataColumn("SF_Mobile", typeof(string)));




                strQry = " SELECT a.sf_Code, a.sf_Name, a.Sf_UserName,(select subdivision_name+',' from mas_subdivision sd where charindex (','+cast(subdivision_code as varchar)+',', ','+a.subdivision_code)>0 for xml path('')) Division, " +
                     "  a.sf_Type,convert(char(10),Sf_Joining_Date,105) Sf_Joining_Date, (select sf_name from mas_salesforce where sf_code=a.Reporting_To_SF) as Reporting_To_SF, a.sf_hq, a.sf_password, " +
                     " b.Designation_Short_Name, b.Desig_Color, CASE a.sf_Tp_Active_flag   " +
                     " when '0' then 'Active'  " +
                     " when '1' then 'Vacant'  " +
                     " end sf_Tp_Active_flag, a.UsrDfd_UserName, '' Lst_drCount, c.StateName,a.sf_emp_id Employee_Id,a.Territory,SF_Mobile   " +
                     " FROM mas_salesforce a, Mas_SF_Designation b, mas_state c " +
                     " WHERE a.SF_Status=0 and a.sf_Tp_Active_flag = 0 and  " +
                     " (a.Division_Code like  + '" + div_code + ",'+'%' or a.Division_Code like '%'+','+ '" + div_code + ",'+'%') " +
                     " and a.sf_code = '" + sf_code + "' and a.Designation_Code=b.Designation_Code and a.state_code = c.state_code ORDER BY a.sf_type";
                try
                {
                    dsmgr = db_ER.Exec_DataSet(strQry);
                    if (sf_code == "admin")
                    {
                        strQry = "SELECT HO_ID,Name,User_Name,Password " +
                                  " FROM mas_ho_id_creation " +
                                   " WHERE HO_Active_flag = 0  and  " +
                                   "(Division_Code like '" + div_code + "%'  or " +
                                    "Division_Code like '%" + ',' + div_code + "%') and " +
                                   " (Sub_HO_ID is null or Sub_HO_ID = '0')";

                        DataSet dsmgr1 = db_ER.Exec_DataSet(strQry);
                        if (dsmgr1.Tables[0].Rows.Count > 0)
                        {
                            dr = dt.NewRow();
                            dr["order_id"] = order_id;
                            dr["sf_Code"] = "admin";
                            dr["sf_Name"] = "admin";
                            dr["Sf_UserName"] = dsmgr1.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();
                            dr["Division"] = "";
                            dr["sf_Type"] = "";
                            dr["Sf_Joining_Date"] = "";
                            dr["Reporting_To_SF"] = "";
                            dr["sf_hq"] = "";
                            dr["sf_password"] = dsmgr1.Tables[0].Rows[0].ItemArray.GetValue(3).ToString();
                            dr["Designation_Short_Name"] = "Admin";
                            dr["Desig_Color"] = "33ff96";
                            dr["sf_Tp_Active_flag"] = "";
                            dr["UsrDfd_UserName"] = dsmgr1.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();
                            dr["Lst_drCount"] = "";
                            dr["StateName"] = "";
                            dr["Employee_Id"] = "";
                            dr["Territory"] = "";
                            dr["SF_Mobile"] = "";
                            dt.Rows.Add(dr);
                        }
                    }
                    if (sf_code != "admin")
                    {
                        if (dsmgr.Tables[0].Rows[0].ItemArray.GetValue(4).ToString() == "2")
                    {
                        dr = dt.NewRow();
                        dr["order_id"] = order_id;
                        dr["sf_Code"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                        dr["sf_Name"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                        dr["Sf_UserName"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();
                        dr["Division"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(3).ToString();
                        dr["sf_Type"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(4).ToString();
                        dr["Sf_Joining_Date"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(5).ToString();
                        dr["Reporting_To_SF"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(6).ToString();
                        dr["sf_hq"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(7).ToString();
                        dr["sf_password"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(8).ToString();
                        dr["Designation_Short_Name"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(9).ToString();
                        dr["Desig_Color"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(10).ToString();
                        dr["sf_Tp_Active_flag"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(11).ToString();
                        dr["UsrDfd_UserName"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(12).ToString();
                        dr["Lst_drCount"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(13).ToString();
                        dr["StateName"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(14).ToString();
                        dr["Employee_Id"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(15).ToString();
                        dr["Territory"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(16).ToString() == "--Select--" ? "" : dsmgr.Tables[0].Rows[0].ItemArray.GetValue(16).ToString();
                        dr["SF_Mobile"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(17).ToString();
                        dt.Rows.Add(dr);
                    }
                }

                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }


            DataTable dt_recursive = new DataTable();
            DataSet dsDivision = null;
            DataSet dsA = null;

            if (div_code == "156" && sf_code == "admin")
            {

                strQry = " SELECT a.sf_Code, a.sf_Name, a.Sf_UserName,(select subdivision_name+',' from mas_subdivision sd where charindex (','+cast(subdivision_code as varchar)+',', ','+a.subdivision_code)>0 for xml path('')) Division, " +
                     "  a.sf_Type,convert(char(10),Sf_Joining_Date,105) Sf_Joining_Date, (select sf_name from mas_salesforce where sf_code=a.Reporting_To_SF) as Reporting_To_SF, a.sf_hq, a.sf_password, " +
                     " b.Designation_Short_Name, b.Desig_Color, CASE a.sf_Tp_Active_flag   " +
                     " when '0' then 'Active'  " +
                     " when '1' then 'Vacant'  " +
                     " end sf_Tp_Active_flag, a.UsrDfd_UserName, '' Lst_drCount, c.StateName,a.sf_emp_id Employee_Id,a.Territory,SF_Mobile  " +
                     " FROM mas_salesforce a, Mas_SF_Designation b, mas_state c " +
                     " WHERE a.SF_Status=0  and (a.sf_Tp_Active_flag = 0 or a.sf_Tp_Active_flag = 1 and sf_type='1') and  " +
                     " (a.Division_Code like  + '" + div_code + ",'+'%' or a.Division_Code like '%'+','+ '" + div_code + ",'+'%') " +
                     // " and a.Reporting_To_SF = '" + sf_code + "'  " +
                     " and a.Designation_Code=b.Designation_Code and a.state_code = c.state_code ORDER BY a.sf_type";
            }
            else if ((div_code == "156" && sf_code != "admin"))
            {
                strQry = " SELECT a.sf_Code, a.sf_Name, a.Sf_UserName,(select subdivision_name+',' from mas_subdivision sd where charindex (','+cast(subdivision_code as varchar)+',', ','+a.subdivision_code)>0 for xml path('')) Division, " +
                    "  a.sf_Type,convert(char(10),Sf_Joining_Date,105) Sf_Joining_Date, (select sf_name from mas_salesforce where sf_code=a.Reporting_To_SF) as Reporting_To_SF, a.sf_hq, a.sf_password, " +
                    " b.Designation_Short_Name, b.Desig_Color, CASE a.sf_Tp_Active_flag   " +
                    " when '0' then 'Active'  " +
                    " when '1' then 'Vacant'  " +
                    " end sf_Tp_Active_flag, a.UsrDfd_UserName, '' Lst_drCount, c.StateName,a.sf_emp_id Employee_Id,a.Territory,SF_Mobile  " +
                    " FROM mas_salesforce a, Mas_SF_Designation b, mas_state c " +
                    " WHERE a.SF_Status=0  and (a.sf_Tp_Active_flag = 0 or a.sf_Tp_Active_flag = 1 and sf_type='1') and  " +
                    " (a.Division_Code like  + '" + div_code + ",'+'%' or a.Division_Code like '%'+','+ '" + div_code + ",'+'%') " +
                    " and a.Reporting_To_SF = '" + sf_code + "'  " +
                    " and a.Designation_Code=b.Designation_Code and a.state_code = c.state_code ORDER BY a.sf_type";
            }
            else
            {

                strQry = " SELECT a.sf_Code, a.sf_Name, a.Sf_UserName,(select subdivision_name+',' from mas_subdivision sd where charindex (','+cast(subdivision_code as varchar)+',', ','+a.subdivision_code)>0 for xml path('')) Division, " +
                         "  a.sf_Type,convert(char(10),Sf_Joining_Date,105) Sf_Joining_Date, (select sf_name from mas_salesforce where sf_code=a.Reporting_To_SF) as Reporting_To_SF, a.sf_hq, a.sf_password, " +
                         " b.Designation_Short_Name, b.Desig_Color, CASE a.sf_Tp_Active_flag   " +
                         " when '0' then 'Active'  " +
                         " when '1' then 'Vacant'  " +
                         " end sf_Tp_Active_flag, a.UsrDfd_UserName, '' Lst_drCount, c.StateName,a.sf_emp_id Employee_Id,a.Territory,SF_Mobile  " +
                         " FROM mas_salesforce a, Mas_SF_Designation b, mas_state c " +
                         " WHERE a.SF_Status=0  and (a.sf_Tp_Active_flag = 0 or a.sf_Tp_Active_flag = 1 and sf_type='1') and  " +
                         " (a.Division_Code like  + '" + div_code + ",'+'%' or a.Division_Code like '%'+','+ '" + div_code + ",'+'%') " +
                         " and a.Reporting_To_SF = '" + sf_code + "' and a.Designation_Code=b.Designation_Code and a.state_code = c.state_code ORDER BY a.sf_type";
            }

            try
            {
                dsDivision = db_ER.Exec_DataSet(strQry);

                if (dsDivision.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drFF in dsDivision.Tables[0].Rows)
                    {
                        DataSet ds = CheckforAudit(drFF["sf_code"].ToString(), div_code);
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow dsloop in ds.Tables[0].Rows)
                            {
                                int am = 0;
                                foreach (DataRow draud in dt.Rows)
                                {
                                    if (draud["sf_Code"].ToString() == dsloop["Sf_Code"].ToString())
                                    {
                                        am = 1;
                                    }
                                }
                                if (am == 0)
                                {
                                    DataSet ds1 = CheckforAudit(dsloop["Sf_Code"].ToString(), div_code);
                                    if (ds1.Tables[0].Rows.Count > 0)
                                    {
                                        int am1 = 0;
                                        foreach (DataRow draud in dt.Rows)
                                        {
                                            if (draud["sf_Code"].ToString() == ds1.Tables[0].Rows[0].ItemArray.GetValue(0).ToString())
                                            {
                                                am1 = 1;
                                            }
                                        }
                                        if (am1 == 0)
                                        {
                                            DataSet ds2 = CheckforAudit(ds1.Tables[0].Rows[0].ItemArray.GetValue(0).ToString(), div_code);
                                            if (ds2.Tables[0].Rows.Count > 0)
                                            {
                                                int am2 = 0;
                                                foreach (DataRow draud in dt.Rows)
                                                {
                                                    if (draud["sf_Code"].ToString() == ds2.Tables[0].Rows[0].ItemArray.GetValue(0).ToString())
                                                    {
                                                        am2 = 1;
                                                    }
                                                }
                                                if (am2 == 0)
                                                {
                                                    dsA = getAuditMgr(ds2.Tables[0].Rows[0].ItemArray.GetValue(0).ToString(), div_code);
                                                    if (dsA.Tables[0].Rows.Count > 0)
                                                    {
                                                        order_id = order_id + 1;
                                                        dr = dt.NewRow();
                                                        dr["order_id"] = order_id;
                                                        dr["sf_Code"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                                                        Audit_mgr = dsA.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                                                        dr["sf_Name"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                                                        dr["Sf_UserName"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();
                                                        dr["Division"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(3).ToString();
                                                        dr["sf_Type"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(4).ToString();
                                                        dr["Sf_Joining_Date"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(5).ToString();
                                                        dr["Reporting_To_SF"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(6).ToString();
                                                        dr["sf_hq"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(7).ToString();
                                                        dr["sf_password"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(8).ToString();
                                                        dr["Designation_Short_Name"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(9).ToString();
                                                        dr["Desig_Color"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(10).ToString();
                                                        dr["sf_Tp_Active_flag"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(11).ToString();
                                                        dr["UsrDfd_UserName"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(12).ToString();
                                                        dr["Lst_drCount"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(13).ToString();
                                                        dr["StateName"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(14).ToString();
                                                        dr["Employee_Id"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(15).ToString();
                                                        dr["Territory"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(16).ToString() == "--Select--" ? "" : dsA.Tables[0].Rows[0].ItemArray.GetValue(16).ToString();
                                                        dr["SF_Mobile"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(17).ToString();
                                                        dt.Rows.Add(dr);
                                                    }
                                                }
                                            }

                                            dsA = getAuditMgr(ds1.Tables[0].Rows[0].ItemArray.GetValue(0).ToString(), div_code);
                                            if (dsA.Tables[0].Rows.Count > 0)
                                            {
                                                order_id = order_id + 1;
                                                dr = dt.NewRow();
                                                dr["order_id"] = order_id;
                                                dr["sf_Code"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                                                Audit_mgr = dsA.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                                                dr["sf_Name"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                                                dr["Sf_UserName"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();
                                                dr["Division"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(3).ToString();
                                                dr["sf_Type"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(4).ToString();
                                                dr["Sf_Joining_Date"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(5).ToString();
                                                dr["Reporting_To_SF"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(6).ToString();
                                                dr["sf_hq"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(7).ToString();
                                                dr["sf_password"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(8).ToString();
                                                dr["Designation_Short_Name"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(9).ToString();
                                                dr["Desig_Color"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(10).ToString();
                                                dr["sf_Tp_Active_flag"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(11).ToString();
                                                dr["UsrDfd_UserName"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(12).ToString();
                                                dr["Lst_drCount"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(13).ToString();
                                                dr["StateName"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(14).ToString();
                                                dr["Employee_Id"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(15).ToString();
                                                dr["Territory"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(16).ToString() == "--Select--" ? "" : dsA.Tables[0].Rows[0].ItemArray.GetValue(16).ToString();
                                                dr["SF_Mobile"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(17).ToString();
                                                dt.Rows.Add(dr);
                                            }
                                        }
                                    }
                                    dsA = getAuditMgr(dsloop["Sf_Code"].ToString(), div_code);
                                    if (dsA.Tables[0].Rows.Count > 0)
                                    {
                                        order_id = order_id + 1;
                                        dr = dt.NewRow();
                                        dr["order_id"] = order_id;
                                        dr["sf_Code"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                                        Audit_mgr = dsA.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                                        dr["sf_Name"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                                        dr["Sf_UserName"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();
                                        dr["Division"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(3).ToString();
                                        dr["sf_Type"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(4).ToString();
                                        dr["Sf_Joining_Date"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(5).ToString();
                                        dr["Reporting_To_SF"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(6).ToString();
                                        dr["sf_hq"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(7).ToString();
                                        dr["sf_password"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(8).ToString();
                                        dr["Designation_Short_Name"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(9).ToString();
                                        dr["Desig_Color"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(10).ToString();
                                        dr["sf_Tp_Active_flag"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(11).ToString();
                                        dr["UsrDfd_UserName"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(12).ToString();
                                        dr["Lst_drCount"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(13).ToString();
                                        dr["StateName"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(14).ToString();
                                        dr["Employee_Id"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(15).ToString();
                                        dr["Territory"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(16).ToString() == "--Select--" ? "" : dsA.Tables[0].Rows[0].ItemArray.GetValue(16).ToString();
                                        dr["SF_Mobile"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(17).ToString();
                                        dt.Rows.Add(dr);
                                    }
                                }
                            }
                        }
                        dsA = getAuditTeam(drFF["sf_code"].ToString(), div_code);
                        if ((dsA.Tables[0].Rows.Count == 0) || (dsA.Tables[0].Rows[0].ItemArray.GetValue(0).ToString() == ""))
                        {
                            order_id = order_id + 1;
                            dr = dt.NewRow();
                            dr["order_id"] = order_id;
                            dr["sf_Code"] = drFF["sf_code"].ToString();
                            dr["sf_Name"] = drFF["sf_Name"].ToString();
                            dr["Sf_UserName"] = drFF["Sf_UserName"].ToString();
                            dr["Division"] = drFF["Division"].ToString();
                            dr["sf_Type"] = drFF["sf_Type"].ToString();
                            dr["Sf_Joining_Date"] = drFF["Sf_Joining_Date"].ToString();
                            dr["Reporting_To_SF"] = drFF["Reporting_To_SF"].ToString();
                            dr["sf_hq"] = drFF["sf_hq"].ToString();
                            dr["sf_password"] = drFF["sf_password"].ToString();
                            dr["Designation_Short_Name"] = drFF["Designation_Short_Name"].ToString();
                            dr["Desig_Color"] = drFF["Desig_Color"].ToString();
                            dr["sf_Tp_Active_flag"] = drFF["sf_Tp_Active_flag"].ToString();
                            dr["UsrDfd_UserName"] = drFF["UsrDfd_UserName"].ToString();
                            dr["Lst_drCount"] = drFF["Lst_drCount"].ToString();
                            dr["StateName"] = drFF["StateName"].ToString();
                            dr["Employee_Id"] = drFF["Employee_Id"].ToString();
                            dr["Territory"] = drFF["Territory"].ToString() == "--Select--" ? "" : drFF["Territory"].ToString();
                            dr["SF_Mobile"] = drFF["SF_Mobile"].ToString();
                            dt.Rows.Add(dr);
                        }

                        if (order_id == 0)
                            order_id = order_id + 1;
                        dt_recursive = getUserListReportingToNew(div_code, drFF["sf_code"].ToString(), order_id, dr["sf_Name"].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }
        public DataTable getUserListReportingToAllNew(string div_code, string sf_code, int order_id, string sf_type) // 23-oct-15
        {
            DB_EReporting db_ER = new DB_EReporting();
            if (order_id == 0)
            {
                DataSet dsmgr = null;
                dt.Columns.Add(new DataColumn("order_id", typeof(int)));
                dt.Columns.Add(new DataColumn("sf_Code", typeof(string)));
                dt.Columns.Add(new DataColumn("sf_Name", typeof(string)));
                dt.Columns.Add(new DataColumn("Sf_UserName", typeof(string)));
                dt.Columns.Add(new DataColumn("Division", typeof(string)));
                dt.Columns.Add(new DataColumn("sf_Type", typeof(string)));
                dt.Columns.Add(new DataColumn("Sf_Joining_Date", typeof(string)));
                dt.Columns.Add(new DataColumn("Reporting_To_SF", typeof(string)));
                dt.Columns.Add(new DataColumn("sf_hq", typeof(string)));
                dt.Columns.Add(new DataColumn("sf_password", typeof(string)));
                dt.Columns.Add(new DataColumn("Designation_Short_Name", typeof(string)));
                dt.Columns.Add(new DataColumn("Desig_Color", typeof(string)));
                dt.Columns.Add(new DataColumn("sf_Tp_Active_flag", typeof(string)));
                dt.Columns.Add(new DataColumn("UsrDfd_UserName", typeof(string)));
                dt.Columns.Add(new DataColumn("Lst_drCount", typeof(string)));
                dt.Columns.Add(new DataColumn("StateName", typeof(string)));
                dt.Columns.Add(new DataColumn("Employee_Id", typeof(string)));
                dt.Columns.Add(new DataColumn("Territory", typeof(string)));
                dt.Columns.Add(new DataColumn("SF_Mobile", typeof(string)));

                strQry = " SELECT a.sf_Code, a.sf_Name, a.Sf_UserName, a.sf_Type,convert(char(10),Sf_Joining_Date,105) Sf_Joining_Date,(select subdivision_name+',' from mas_subdivision sd where charindex (','+cast(subdivision_code as varchar)+',', ','+a.subdivision_code)>0 for xml path('')) Division, " +
                   " (select sf_name from mas_salesforce where sf_code=a.Reporting_To_SF) as Reporting_To_SF, a.sf_hq, a.sf_password, " +
                   " b.Designation_Short_Name, b.Desig_Color, CASE a.SF_Status   " +
                   " when '0' then 'Active'  " +
                   " when '1' then 'Vacant'  " +
                   " end sf_Tp_Active_flag, a.UsrDfd_UserName, '' Lst_drCount, c.StateName,a.sf_emp_id Employee_Id,a.Territory,SF_Mobile " +
                   " FROM mas_salesforce a, Mas_SF_Designation b, mas_state c " +
                   " WHERE (a.SF_Status=0 or a.SF_Status=1) and " +
                   " (a.Division_Code like  + '" + div_code + ",'+'%' or a.Division_Code like '%'+','+ '" + div_code + ",'+'%') " +
                   " and a.sf_code = '" + sf_code + "' and a.Designation_Code=b.Designation_Code and a.State_Code=c.State_Code ORDER BY a.sf_type";
                try
                {
                    dsmgr = db_ER.Exec_DataSet(strQry);
                    if (sf_code == "admin")
                    {
                        strQry = "SELECT HO_ID,Name,User_Name,Password " +
                                  " FROM mas_ho_id_creation " +
                                   " WHERE HO_Active_flag = 0  and  " +
                                   "(Division_Code like '" + div_code + "%'  or " +
                                    "Division_Code like '%" + ',' + div_code + "%') and " +
                                   "Sub_HO_ID is null";

                        DataSet dsmgr1 = db_ER.Exec_DataSet(strQry);
                        if (dsmgr1.Tables[0].Rows.Count > 0)
                        {
                            dr = dt.NewRow();
                            dr["order_id"] = order_id;
                            dr["sf_Code"] = "admin";
                            dr["sf_Name"] = "admin";
                            dr["Sf_UserName"] = dsmgr1.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();
                            dr["Division"] = "";
                            dr["sf_Type"] = "";
                            dr["Sf_Joining_Date"] = "";
                            dr["Reporting_To_SF"] = "";
                            dr["sf_hq"] = "";
                            dr["sf_password"] = dsmgr1.Tables[0].Rows[0].ItemArray.GetValue(3).ToString();
                            dr["Designation_Short_Name"] = "Admin";
                            dr["Desig_Color"] = "33ff96";

                            dr["sf_Tp_Active_flag"] = "";
                            dr["UsrDfd_UserName"] = dsmgr1.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();
                            dr["Lst_drCount"] = "";
                            dr["StateName"] = "";
                            dr["Employee_Id"] = "";
                            dr["Territory"] = "";
                            dr["SF_Mobile"] = "";
                            dt.Rows.Add(dr);
                        }
                    }
                    if (sf_code != "admin")
                    {
                        if (dsmgr.Tables[0].Rows[0].ItemArray.GetValue(4).ToString() == "2")
                        {

                            dr = dt.NewRow();
                            dr["order_id"] = order_id;
                            dr["sf_Code"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

                            dr["sf_Name"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();


                            dr["Sf_UserName"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();
                            dr["Division"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(3).ToString();
                            dr["sf_Type"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(4).ToString();
                            dr["Sf_Joining_Date"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(5).ToString();
                            dr["Reporting_To_SF"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(6).ToString();
                            dr["sf_hq"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(7).ToString();
                            dr["sf_password"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(8).ToString();
                            dr["Designation_Short_Name"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(9).ToString();
                            dr["Desig_Color"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(10).ToString();
                            dr["sf_Tp_Active_flag"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(11).ToString();
                            dr["UsrDfd_UserName"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(12).ToString();
                            dr["Lst_drCount"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(13).ToString();
                            dr["StateName"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(14).ToString();
                            dr["Employee_Id"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(15).ToString();
                            dr["Territory"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(16).ToString() == "--Select--" ? "" : dsmgr.Tables[0].Rows[0].ItemArray.GetValue(16).ToString();
                            dr["SF_Mobile"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(17).ToString();
                            dt.Rows.Add(dr);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }


            DataTable dt_recursive = new DataTable();
            DataSet dsDivision = null;
            DataSet dsA = null;

            if (div_code == "156" && sf_code == "admin")
            {
                strQry = " SELECT a.sf_Code, a.sf_Name, a.Sf_UserName, a.sf_Type,convert(char(10),Sf_Joining_Date,105) Sf_Joining_Date, (select subdivision_name+',' from mas_subdivision sd where charindex (','+cast(subdivision_code as varchar)+',', ','+a.subdivision_code)>0 for xml path('')) Division, " +
                   " (select sf_name from mas_salesforce where sf_code=a.Reporting_To_SF) as Reporting_To_SF, a.sf_hq, a.sf_password, " +
                   " b.Designation_Short_Name, b.Desig_Color, CASE a.SF_Status   " +
                   " when '0' then 'Active'  " +
                   " when '1' then 'Vacant'  " +
                   " end sf_Tp_Active_flag, a.UsrDfd_UserName, '' Lst_drCount, c.StateName,a.sf_emp_id Employee_Id,a.Territory,SF_Mobile  " +
                   " FROM mas_salesforce a, Mas_SF_Designation b, mas_state c " +
                   " WHERE (a.SF_Status=0  or a.SF_Status=1) and  " +
                   " (a.Division_Code like  + '" + div_code + ",'+'%' or a.Division_Code like '%'+','+ '" + div_code + ",'+'%') " +
                   //" and a.Reporting_To_SF = '" + sf_code + "' " +
                   " and a.Designation_Code=b.Designation_Code and a.state_code = c.state_code ORDER BY a.sf_type";
            }
            else if (div_code == "156" && sf_code != "admin")
            {
                strQry = " SELECT a.sf_Code, a.sf_Name, a.Sf_UserName, a.sf_Type,convert(char(10),Sf_Joining_Date,105) Sf_Joining_Date, (select subdivision_name+',' from mas_subdivision sd where charindex (','+cast(subdivision_code as varchar)+',', ','+a.subdivision_code)>0 for xml path('')) Division, " +
                  " (select sf_name from mas_salesforce where sf_code=a.Reporting_To_SF) as Reporting_To_SF, a.sf_hq, a.sf_password, " +
                  " b.Designation_Short_Name, b.Desig_Color, CASE a.SF_Status   " +
                  " when '0' then 'Active'  " +
                  " when '1' then 'Vacant'  " +
                  " end sf_Tp_Active_flag, a.UsrDfd_UserName, '' Lst_drCount, c.StateName,a.sf_emp_id Employee_Id,a.Territory,SF_Mobile  " +
                  " FROM mas_salesforce a, Mas_SF_Designation b, mas_state c " +
                  " WHERE (a.SF_Status=0  or a.SF_Status=1) and  " +
                  " (a.Division_Code like  + '" + div_code + ",'+'%' or a.Division_Code like '%'+','+ '" + div_code + ",'+'%') " +
                  " and a.Reporting_To_SF = '" + sf_code + "' " +
                  " and a.Designation_Code=b.Designation_Code and a.state_code = c.state_code ORDER BY a.sf_type";
            }
            else
            {
                strQry = " SELECT a.sf_Code, a.sf_Name, a.Sf_UserName, a.sf_Type,convert(char(10),Sf_Joining_Date,105) Sf_Joining_Date, (select subdivision_name+',' from mas_subdivision sd where charindex (','+cast(subdivision_code as varchar)+',', ','+a.subdivision_code)>0 for xml path('')) Division, " +
                     " (select sf_name from mas_salesforce where sf_code=a.Reporting_To_SF) as Reporting_To_SF, a.sf_hq, a.sf_password, " +
                     " b.Designation_Short_Name, b.Desig_Color, CASE a.SF_Status   " +
                     " when '0' then 'Active'  " +
                     " when '1' then 'Vacant'  " +
                     " end sf_Tp_Active_flag, a.UsrDfd_UserName, '' Lst_drCount, c.StateName,a.sf_emp_id Employee_Id,a.Territory,SF_Mobile  " +
                     " FROM mas_salesforce a, Mas_SF_Designation b, mas_state c " +
                     " WHERE (a.SF_Status=0  or a.SF_Status=1) and  " +
                     " (a.Division_Code like  + '" + div_code + ",'+'%' or a.Division_Code like '%'+','+ '" + div_code + ",'+'%') " +
                     " and a.Reporting_To_SF = '" + sf_code + "' and a.Designation_Code=b.Designation_Code and a.state_code = c.state_code ORDER BY a.sf_type";
            }
            try
            {
                dsDivision = db_ER.Exec_DataSet(strQry);

                if (dsDivision.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drFF in dsDivision.Tables[0].Rows)
                    {
                        //Check for Audit
                        // DataSet ds = CheckforAuditteammgr(drFF["sf_code"].ToString(), div_code);
                        DataSet ds = CheckforAudit(drFF["sf_code"].ToString(), div_code);
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow dsloop in ds.Tables[0].Rows)
                            {
                                int am = 0;
                                foreach (DataRow draud in dt.Rows)
                                {
                                    if (draud["sf_Code"].ToString() == dsloop["Sf_Code"].ToString())
                                    {
                                        am = 1;
                                    }
                                }
                                if (am == 0)
                                {
                                    DataSet ds1 = CheckforAudit(dsloop["Sf_Code"].ToString(), div_code);
                                    if (ds1.Tables[0].Rows.Count > 0)
                                    {
                                        int am1 = 0;
                                        foreach (DataRow draud in dt.Rows)
                                        {
                                            if (draud["sf_Code"].ToString() == ds1.Tables[0].Rows[0].ItemArray.GetValue(0).ToString())
                                            {
                                                am1 = 1;
                                            }
                                        }
                                        if (am1 == 0)
                                        {
                                            DataSet ds2 = CheckforAudit(ds1.Tables[0].Rows[0].ItemArray.GetValue(0).ToString(), div_code);
                                            if (ds2.Tables[0].Rows.Count > 0)
                                            {
                                                int am2 = 0;
                                                foreach (DataRow draud in dt.Rows)
                                                {
                                                    if (draud["sf_Code"].ToString() == ds2.Tables[0].Rows[0].ItemArray.GetValue(0).ToString())
                                                    {
                                                        am2 = 1;
                                                    }
                                                }
                                                if (am2 == 0)
                                                {
                                                    dsA = getAuditMgr(ds2.Tables[0].Rows[0].ItemArray.GetValue(0).ToString(), div_code);
                                                    if (dsA.Tables[0].Rows.Count > 0)
                                                    {
                                                        order_id = order_id + 1;
                                                        dr = dt.NewRow();
                                                        dr["order_id"] = order_id;
                                                        dr["sf_Code"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                                                        Audit_mgr = dsA.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                                                        dr["sf_Name"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                                                        dr["Sf_UserName"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();
                                                        dr["Division"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(3).ToString();
                                                        dr["sf_Type"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(4).ToString();
                                                        dr["Sf_Joining_Date"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(5).ToString();
                                                        dr["Reporting_To_SF"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(6).ToString();
                                                        dr["sf_hq"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(7).ToString();
                                                        dr["sf_password"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(8).ToString();
                                                        dr["Designation_Short_Name"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(9).ToString();
                                                        dr["Desig_Color"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(10).ToString();
                                                        dr["sf_Tp_Active_flag"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(11).ToString();
                                                        dr["UsrDfd_UserName"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(12).ToString();
                                                        dr["Lst_drCount"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(13).ToString();
                                                        dr["StateName"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(14).ToString();
                                                        dr["Employee_Id"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(15).ToString();
                                                        dr["Territory"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(16).ToString() == "--Select--" ? "" : dsA.Tables[0].Rows[0].ItemArray.GetValue(16).ToString();
                                                        dr["SF_Mobile"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(17).ToString();
                                                        dt.Rows.Add(dr);
                                                    }
                                                }
                                            }
                                            dsA = getAuditMgr(ds1.Tables[0].Rows[0].ItemArray.GetValue(0).ToString(), div_code);
                                            if (dsA.Tables[0].Rows.Count > 0)
                                            {
                                                order_id = order_id + 1;
                                                dr = dt.NewRow();
                                                dr["order_id"] = order_id;
                                                dr["sf_Code"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                                                Audit_mgr = dsA.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                                                dr["sf_Name"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                                                dr["Sf_UserName"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();
                                                dr["Division"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(3).ToString();
                                                dr["sf_Type"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(4).ToString();
                                                dr["Sf_Joining_Date"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(5).ToString();
                                                dr["Reporting_To_SF"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(6).ToString();
                                                dr["sf_hq"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(7).ToString();
                                                dr["sf_password"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(8).ToString();
                                                dr["Designation_Short_Name"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(9).ToString();
                                                dr["Desig_Color"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(10).ToString();
                                                dr["sf_Tp_Active_flag"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(11).ToString();
                                                dr["UsrDfd_UserName"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(12).ToString();
                                                dr["Lst_drCount"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(13).ToString();
                                                dr["StateName"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(14).ToString();
                                                dr["Employee_Id"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(15).ToString();
                                                dr["Territory"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(16).ToString() == "--Select--" ? "" : dsA.Tables[0].Rows[0].ItemArray.GetValue(16).ToString();
                                                dr["SF_Mobile"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(17).ToString();
                                                dt.Rows.Add(dr);
                                            }
                                        }
                                    }
                                    dsA = getAuditMgr(dsloop["Sf_Code"].ToString(), div_code);
                                    if (dsA.Tables[0].Rows.Count > 0)
                                    {
                                        order_id = order_id + 1;
                                        dr = dt.NewRow();
                                        dr["order_id"] = order_id;
                                        dr["sf_Code"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                                        Audit_mgr = dsA.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                                        dr["sf_Name"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                                        dr["Sf_UserName"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();
                                        dr["Division"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(3).ToString();
                                        dr["sf_Type"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(4).ToString();
                                        dr["Sf_Joining_Date"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(5).ToString();
                                        dr["Reporting_To_SF"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(6).ToString();
                                        dr["sf_hq"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(7).ToString();
                                        dr["sf_password"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(8).ToString();
                                        dr["Designation_Short_Name"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(9).ToString();
                                        dr["Desig_Color"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(10).ToString();
                                        dr["sf_Tp_Active_flag"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(11).ToString();
                                        dr["UsrDfd_UserName"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(12).ToString();
                                        dr["Lst_drCount"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(13).ToString();
                                        dr["StateName"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(14).ToString();
                                        dr["Employee_Id"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(15).ToString();
                                        dr["Territory"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(16).ToString() == "--Select--" ? "" : dsA.Tables[0].Rows[0].ItemArray.GetValue(16).ToString();
                                        dr["SF_Mobile"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(17).ToString();
                                        dt.Rows.Add(dr);
                                    }
                                }
                            }
                        }
                        dsA = getAuditTeam(drFF["sf_code"].ToString(), div_code);
                        if ((dsA.Tables[0].Rows.Count == 0) || (dsA.Tables[0].Rows[0].ItemArray.GetValue(0).ToString() == ""))
                        {
                            order_id = order_id + 1;
                            dr = dt.NewRow();
                            dr["order_id"] = order_id;
                            dr["sf_Code"] = drFF["sf_code"].ToString();
                            dr["sf_Name"] = drFF["sf_Name"].ToString();
                            dr["Sf_UserName"] = drFF["Sf_UserName"].ToString();
                            dr["Division"] = drFF["Division"].ToString();
                            dr["sf_Type"] = drFF["sf_Type"].ToString();
                            dr["Sf_Joining_Date"] = drFF["Sf_Joining_Date"].ToString();
                            dr["Reporting_To_SF"] = drFF["Reporting_To_SF"].ToString();
                            dr["sf_hq"] = drFF["sf_hq"].ToString();
                            dr["sf_password"] = drFF["sf_password"].ToString();
                            dr["Designation_Short_Name"] = drFF["Designation_Short_Name"].ToString();
                            dr["Desig_Color"] = drFF["Desig_Color"].ToString();
                            dr["sf_Tp_Active_flag"] = drFF["sf_Tp_Active_flag"].ToString();
                            dr["UsrDfd_UserName"] = drFF["UsrDfd_UserName"].ToString();
                            dr["Lst_drCount"] = drFF["Lst_drCount"].ToString();
                            dr["StateName"] = drFF["StateName"].ToString();
                            dr["Employee_Id"] = drFF["Employee_Id"].ToString();
                            dr["Territory"] = drFF["Territory"].ToString() == "--Select--" ? "" : drFF["Territory"].ToString();
                            dr["SF_Mobile"] = drFF["SF_Mobile"].ToString();
                            dt.Rows.Add(dr);
                        }
                        if (order_id == 0)
                            order_id = order_id + 1;
                        dt_recursive = getUserListReportingToAllNew(div_code, drFF["sf_code"].ToString(), order_id, dr["sf_Name"].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }
        public DataSet CheckforAudit(string sf_code, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;

            strQry = " SELECT Sf_Code from Mas_sf_Audit_Team Where" +
                     " (Audit_Team like '" + sf_code + ',' + "%'  or " +
                      " Audit_Team like '%" + ',' + sf_code + ',' + "%' or  " +
                     " Audit_Team like '%" + sf_code + "%' ) " +
                     " and  Division_code = '" + div_code + "'";
            try
            {
                dsSF = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsSF;
        }
        public DataSet getAuditTeam(string sf_code, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;

            strQry = " Select Audit_team from Mas_Sf_Audit_Team where " +
                     " Division_Code =  + '" + div_code + "'" +
                     " and sf_code = '" + sf_code + "'";
            try
            {
                dsSF = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsSF;
        }
        public DataSet getAuditMgr(string sf_code, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;

            strQry = " SELECT a.sf_Code, a.sf_Name, a.Sf_UserName, a.sf_Type,convert(char(10),Sf_Joining_Date,105) Sf_Joining_Date, " +
                     " (select sf_name from mas_salesforce where sf_code=a.Reporting_To_SF) as Reporting_To_SF, a.sf_hq, a.sf_password, " +
                     " b.Designation_Short_Name, b.Desig_Color, CASE a.sf_Tp_Active_flag   " +
                     " when '0' then 'Active'  " +
                     " when '1' then 'Vacant'  " +
                     " end sf_Tp_Active_flag ,a.UsrDfd_UserName , '' Lst_drCount,c.StateName,a.Employee_Id,convert(varchar,a.Last_TP_Date,103) Last_TP_Date,convert(varchar,a.Sf_TP_DCR_Active_Dt,103) Sf_TP_DCR_Active_Dt,  " +
                     " convert(varchar,a.Last_DCR_Date,103) Last_DCR_Date,a.sf_emp_id ,a.sf_Type as type , (select UsrDfd_UserName from Mas_Salesforce " +
                     " where sf_code=a.sf_code) +'- '+   (select Sf_Name from mas_salesforce where sf_code=a.Reporting_To_SF) Reporting_To,b.Designation_Short_Name as Designation_Name " +
                     " FROM mas_salesforce a, Mas_SF_Designation b, mas_state c,a.Territory " +
                     " WHERE a.SF_Status=0 and a.sf_Tp_Active_flag = 0 and  " +
                     " (a.Division_Code like  + '" + div_code + ",'+'%' or a.Division_Code like '%'+','+ '" + div_code + ",'+'%') " +
                     " and a.sf_code = '" + sf_code + "' and a.Designation_Code=b.Designation_Code and a.state_code = c.state_code ";
            try
            {
                dsSF = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsSF;
        }
    }
}