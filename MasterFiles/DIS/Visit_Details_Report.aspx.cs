using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;

public partial class MIS_Reports_Visit_Details_Report : System.Web.UI.Page
{
    string div_code = string.Empty;
    DataSet dsmgrsf = new DataSet();
    DataSet dsSalesForce = null;
    DataSet dsUserList = null;
    DataSet dsDoc = null;
    DataSet dsDCR = null;
    int doctor_total = 0;
    DateTime dtCurrent;
    DataSet dsDoctor = null;
    DataSet dsCatg = null;
    int mode = -1;
    DataSet dsTP = null;
    string tot_dr = string.Empty;
    string tot_dcr_dr = string.Empty;
    int imissed_dr = -1;
    string sCurrentDate = string.Empty;
    string sf_code = string.Empty;
    DataSet dsSf = null;
    string sf_type = string.Empty;
   
    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        sf_code = Session["sf_code"].ToString();
        lblModelevel.Visible = false;
        if (Session["sf_type"].ToString() == "1")
        {
            UserControl_MR_Menu c1 =
                (UserControl_MR_Menu)LoadControl("~/UserControl/MR_Menu.ascx");
            Divid.Controls.Add(c1);
            c1.FindControl("btnBack").Visible = false;
            c1.Title = this.Page.Title;
            ViewState["sf_type"] = "";
            SalesForce sf = new SalesForce();
            dsSf = sf.getReportingTo(sf_code);
            if (dsSf.Tables[0].Rows.Count > 0)
            {
                sf_code = dsSf.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
            }
            if (!Page.IsPostBack)
            {
                FillMRManagers();
            }
            ddlFieldForce.SelectedValue = Session["sf_code"].ToString();
            ddlFieldForce.Enabled = false;
            ddlMode.SelectedIndex = 1;
            ddlMode.Enabled  = false;
        }

        else if (Session["sf_type"].ToString() == "2")
        {
            UserControl_MGR_Menu c1 =
            (UserControl_MGR_Menu)LoadControl("~/UserControl/MGR_Menu.ascx");
            Divid.Controls.Add(c1);
            c1.FindControl("btnBack").Visible = false;
            c1.Title = this.Page.Title;
            ViewState["sf_type"] = "";
            if (!Page.IsPostBack)
            {
                FillMRManagers();
                ddlFieldForce.SelectedValue = sf_code;
            }
        }
        else
        {
            ViewState["sf_type"] = "admin";
            UserControl_MenuUserControl c1 =
            (UserControl_MenuUserControl)LoadControl("~/UserControl/MenuUserControl.ascx");
            Divid.Controls.Add(c1);
            c1.FindControl("btnBack").Visible = false;
            c1.Title = this.Page.Title;
            if (!Page.IsPostBack)
            {
                FillManagers();
            }
        }

        if (!Page.IsPostBack)
        {
            TourPlan tp = new TourPlan();
            dsTP = tp.Get_TP_Edit_Year(div_code);
            if (dsTP.Tables[0].Rows.Count > 0)
            {
                for (int k = Convert.ToInt16(dsTP.Tables[0].Rows[0]["Year"]); k <= DateTime.Now.Year + 1; k++)
                {
                    ddlFYear.Items.Add(k.ToString());
                    ddlTYear.Items.Add(k.ToString());
                    ddlFYear.SelectedValue = DateTime.Now.Year.ToString();
                    ddlTYear.SelectedValue = DateTime.Now.Year.ToString();
                }
            }

            ddlFMonth.SelectedValue = DateTime.Now.Month.ToString();
            ddlTMonth.SelectedValue = DateTime.Now.Month.ToString();
            
        }
        FillColor();

    }

    private void FillMRManagers()
    {
        SalesForce sf = new SalesForce();
        ddlFFType.Visible = false;
        ddlAlpha.Visible = false;
        DataSet DsAudit = sf.SF_Hierarchy(div_code, sf_code);
        if (DsAudit.Tables[0].Rows.Count > 0 || Session["sf_type"].ToString() == "1")
        {
            dsSalesForce = sf.SalesForceListMgrGet(div_code, sf_code);
            if (dsSalesForce.Tables[0].Rows.Count > 0)
            {
                ddlFieldForce.DataTextField = "sf_name";
                ddlFieldForce.DataValueField = "sf_code";
                ddlFieldForce.DataSource = dsSalesForce;
                ddlFieldForce.DataBind();

                ddlSF.DataTextField = "Desig_Color";
                ddlSF.DataValueField = "sf_code";
                ddlSF.DataSource = dsSalesForce;
                ddlSF.DataBind();


            }
        }
        else
        {
            // Fetch Managers Audit Team
            DataTable dt = sf.getAuditManagerTeam(div_code, sf_code, 0);

            dsmgrsf.Tables.Add(dt);            
            dsTP = dsmgrsf;

            ddlFieldForce.DataTextField = "sf_name";
            ddlFieldForce.DataValueField = "sf_code";
            ddlFieldForce.DataSource = dsTP;
            ddlFieldForce.DataBind();

            ddlSF.DataTextField = "Des_Color";
            ddlSF.DataValueField = "sf_code";
            ddlSF.DataSource = dsTP;
            ddlSF.DataBind();

        }
    }

    private void FillColor()
    {
        int j = 0;


        foreach (ListItem ColorItems in ddlSF.Items)
        {
            //ddlFieldForce.Items[j].Selected = true;

            string bcolor = "#" + ColorItems.Text;
            ddlFieldForce.Items[j].Attributes.Add("style", "background-color:" + bcolor);

            j = j + 1;

        }
    }
   
    private void FillManagers()
    {
        SalesForce sf = new SalesForce();

        if (ddlFFType.SelectedValue.ToString() == "1")
        {
            ddlAlpha.Visible = false;
            dsSalesForce = sf.UserList_Hierarchy(div_code, "admin");
        }
        else if (ddlFFType.SelectedValue.ToString() == "0")
        {
            FillSF_Alpha();
            ddlAlpha.Visible = true;
            dsSalesForce = sf.UserList_Alpha(div_code, "admin");
        }
        else if (ddlFFType.SelectedValue.ToString() == "2")
        {
            dsSalesForce = sf.UserList_HQ(div_code, "admin");
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

    private void FillSF_Alpha()
    {
        SalesForce sf = new SalesForce();
        dsSalesForce = sf.getSalesForcelist_Alphabet(div_code);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            ddlAlpha.DataTextField = "sf_name";
            ddlAlpha.DataValueField = "val";
            ddlAlpha.DataSource = dsSalesForce;
            ddlAlpha.DataBind();
            ddlAlpha.SelectedIndex = 0;
        }
    }

    protected void ddlAlpha_SelectedIndexChanged(object sender, EventArgs e)
    {
        SalesForce sf = new SalesForce();
        dsSalesForce = sf.UserList_Alphasearch(div_code, "admin", ddlAlpha.SelectedValue);
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
    protected void ddlFFType_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblFF.Text = "Field Force";
        FillManagers();
        FillColor();
    }

    protected void ddlFieldForce_SelectedIndexChanged(object sender, EventArgs e)
    {
        SalesForce sf = new SalesForce();
        if (ViewState["sf_type"].ToString() == "admin")
        {
              DataSet DsAudit = sf.SF_Hierarchy(div_code, ddlFieldForce.SelectedValue);
              if (DsAudit.Tables[0].Rows.Count > 0 || Session["sf_type"].ToString() == "1")
              {
                  dsSalesForce = sf.UserList_getMR(div_code, ddlFieldForce.SelectedValue.ToString());
                  if (dsSalesForce.Tables[0].Rows.Count > 0)
                  {
                      lblMR.Visible = true;
                      ddlMR.Visible = true;
                      ddlMR.DataTextField = "sf_name";
                      ddlMR.DataValueField = "sf_code";
                      ddlMR.DataSource = dsSalesForce;
                      ddlMR.DataBind();
                      ddlMR.Items.Insert(0, new ListItem("---Select---", "0"));
                  }
              }
              else
              {
                  // Fetch Managers Audit Team
                  DataTable dt = sf.getAuditManagerTeam(div_code, ddlFieldForce.SelectedValue.ToString(), 0);

                  dsmgrsf.Tables.Add(dt);
                  dsmgrsf.Tables[0].Rows[0].Delete();
                  dsTP = dsmgrsf;

                  lblMR.Visible = true;
                  ddlMR.Visible = true;

                  ddlMR.DataTextField = "sf_name";
                  ddlMR.DataValueField = "sf_code";
                  ddlMR.DataSource = dsTP;
                  ddlMR.DataBind();
                  ddlMR.Items.Insert(0, new ListItem("---Select---", "0"));
              }
        }
        
        dsSf = sf.CheckSFType(ddlFieldForce.SelectedValue.ToString());
        if (dsSf.Tables[0].Rows.Count > 0)
        {
            if (ViewState["sf_type"].ToString() != "admin")
                sf_type = dsSf.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
        }

        if ((ViewState["sf_type"].ToString() == "admin" && ddlMR.SelectedIndex > 0) || (sf_type == "1"))
        {
            ddlMode.SelectedIndex = 1;
            ddlMode.Enabled = false;
        }
        else
        {
            ddlMode.SelectedIndex = 0;
            ddlMode.Enabled = true;
        }
     }

    protected void ddlMR_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlMR.SelectedIndex > 0)
        {
            ddlMode.SelectedIndex = 1;
            ddlMode.Enabled = false;
        }
        else
        {
            ddlMode.SelectedIndex = 0;
            ddlMode.Enabled = true;
        }
    }

    protected void btnGo_Click(object sender, EventArgs e)
    {
        int FYear = Convert.ToInt32(ddlFYear.SelectedValue);
        int TYear = Convert.ToInt32(ddlTYear.SelectedValue);
        int FMonth = Convert.ToInt32(ddlFMonth.SelectedValue);
        int TMonth = Convert.ToInt32(ddlTMonth.SelectedValue);

        if (FMonth > TMonth && FYear == TYear)
        {
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('To Month must be greater than From Month');</script>");
            ddlTMonth.Focus();
        }
        else if (FYear > TYear)
        {
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('To Year must be greater than From Year');</script>");
            ddlTYear.Focus();
        }
        else
        {
            if (FYear <= TYear)
            {
                lblModelevel.Visible = true;
                if (ddlType.SelectedValue.ToString() == "1")
                {
                    FillCatg();
                }
                else if (ddlType.SelectedValue.ToString() == "2")
                {
                    FillSpec();
                }
                else if (ddlType.SelectedValue.ToString() == "3")
                {
                    FillClass();
                }
                else if (ddlType.SelectedValue.ToString() == "4")
                {
                    //FillCamp();
                }
            }
        }
    }
   
    private void FillCatg()
    {
        tbl.Rows.Clear();
        doctor_total = 0;


        SalesForce sf = new SalesForce();
        DCR dcf = new DCR();
        if (ddlMode.SelectedValue.ToString() == "1")
        {
            if (ddlMR.SelectedIndex > 0)
            {
                mode = 1;
                dsSalesForce = dcf.SF_Self_Report(div_code, ddlMR.SelectedValue.ToString());
            }
            else
            {
                mode = 1;
                dsSalesForce = dcf.SF_Self_Report(div_code, ddlFieldForce.SelectedValue.ToString());
            }
        }
        else
        {
            mode = 2;
            DataSet DsAudit = sf.SF_Hierarchy(div_code, ddlFieldForce.SelectedValue.ToString());
             if (DsAudit.Tables[0].Rows.Count > 0 )
             {
                 dsSalesForce = dcf.SF_ReportingTo_TourPlan(div_code, ddlFieldForce.SelectedValue.ToString());
             }
             else
             {
                 DataTable dt = sf.getAuditManagerTeam_GetMGR(div_code, ddlFieldForce.SelectedValue.ToString(), 0);
                 dsmgrsf.Tables.Add(dt);
                 dsmgrsf.Tables[0].Rows[0].Delete();
                 dsSalesForce = dsmgrsf;
             }
        }

        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            lblModelevel.Text = "Listed doctor Category wise Visit Details report " + ddlFMonth.SelectedItem.Text + " " + ddlFYear.SelectedValue + " to " + ddlTMonth.SelectedItem.Text + " " + ddlTYear.SelectedValue;
            TableRow tr_header = new TableRow();
            tr_header.BorderStyle = BorderStyle.Solid;
            tr_header.BorderWidth = 1;                       

            TableCell tc_SNo = new TableCell();
            tc_SNo.BorderStyle = BorderStyle.Solid;
            tc_SNo.BorderWidth = 1;            
            tc_SNo.Width = 50;
            tc_SNo.RowSpan = 3;
            Literal lit_SNo = new Literal();
            lit_SNo.Text = "S.No";
            tc_SNo.Controls.Add(lit_SNo);
            tr_header.Cells.Add(tc_SNo);
            tc_SNo.Attributes.Add("Class", "rptCellBorder");
           
            if (Session["sf_type"].ToString() == "1")
            {
                tr_header.Attributes.Add("Class", "MRBackColor");
            }
            else if (Session["sf_type"].ToString() == "2")
            {
                tr_header.Attributes.Add("Class", "MGRBackColor");
            }
            else
            {
                tr_header.Attributes.Add("Class", "Backcolor");
            }

            TableCell tc_DR_Code = new TableCell();
            tc_DR_Code.BorderStyle = BorderStyle.Solid;
            tc_DR_Code.BorderWidth = 1;
            tc_DR_Code.Style.Add("font-family", "Calibri");
            tc_DR_Code.Style.Add("font-size", "10pt");
            tc_DR_Code.Width = 400;
            tc_DR_Code.RowSpan = 3;
            Literal lit_DR_Code = new Literal();
            lit_DR_Code.Text = "<center>SF Code</center>";
            tc_DR_Code.Controls.Add(lit_DR_Code);
            tc_DR_Code.Visible = false;
            tr_header.Cells.Add(tc_DR_Code);

            TableCell tc_DR_Name = new TableCell();
            tc_DR_Name.BorderStyle = BorderStyle.Solid;
            tc_DR_Name.BorderWidth = 1;
            tc_DR_Name.Attributes.Add("Class", "rptCellBorder");
            tc_DR_Name.Width = 800;
            tc_DR_Name.RowSpan = 3;
            Literal lit_DR_Name = new Literal();
            lit_DR_Name.Text = "<center>Field&nbspForce&nbspName</center>";
            tc_DR_Name.Controls.Add(lit_DR_Name);
            tr_header.Cells.Add(tc_DR_Name);

            TableCell tc_DR_HQ = new TableCell();
            tc_DR_HQ.BorderStyle = BorderStyle.Solid;
            tc_DR_HQ.BorderWidth = 1;
            tc_DR_HQ.Attributes.Add("Class", "rptCellBorder");
            tc_DR_HQ.Width = 500;
            tc_DR_HQ.RowSpan = 3;
            Literal lit_DR_HQ = new Literal();
            lit_DR_HQ.Text = "<center>HQ</center>";
            tc_DR_HQ.Controls.Add(lit_DR_HQ);
            tr_header.Cells.Add(tc_DR_HQ);

            TableCell tc_DR_Des= new TableCell();
            tc_DR_Des.BorderStyle = BorderStyle.Solid;
            tc_DR_Des.BorderWidth = 1;
            tc_DR_Des.Attributes.Add("Class", "rptCellBorder");
            tc_DR_Des.Width = 500;
            tc_DR_Des.RowSpan = 3;
            Literal lit_DR_Des= new Literal();
            lit_DR_Des.Text = "<center>Designation</center>";
            tc_DR_Des.Controls.Add(lit_DR_Des);
            tr_header.Cells.Add(tc_DR_Des);

            int months = (Convert.ToInt32(ddlTYear.SelectedValue.ToString()) - Convert.ToInt32(ddlFYear.SelectedValue.ToString())) * 12 + Convert.ToInt32(ddlTMonth.SelectedValue.ToString()) - Convert.ToInt32(ddlFMonth.SelectedValue.ToString()); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
            int cmonth = Convert.ToInt32(ddlFMonth.SelectedValue.ToString());
            int cyear = Convert.ToInt32(ddlFYear.SelectedValue.ToString());

            ViewState["months"] = months;
            ViewState["cmonth"] = cmonth;
            ViewState["cyear"] = cyear;

            Doctor dr = new Doctor();
            dsDoctor = dr.getDocCat(div_code);

            if (months >= 0)
            {
                for (int j = 1; j <= months + 1; j++)
                {                 
                    TableCell tc_month = new TableCell();
                    tc_month.ColumnSpan = (dsDoctor.Tables[0].Rows.Count * 2) + 3 ;
                    //tc_month.RowSpan = 2;
                    Literal lit_month = new Literal();
                    lit_month.Text = "&nbsp;" + sf.getMonthName(cmonth.ToString()) + "-" + cyear;
                    tc_month.BorderStyle = BorderStyle.Solid;
                    tc_month.BorderWidth = 1;
                    tc_month.Attributes.Add("Class", "rptCellBorder");
                    tc_month.HorizontalAlign = HorizontalAlign.Center;
                    //tc_month.Width = 200;
                    tc_month.Controls.Add(lit_month);
                    tr_header.Cells.Add(tc_month);
                    cmonth = cmonth + 1;
                    if (cmonth == 13)
                    {
                        cmonth = 1;
                        cyear = cyear + 1;
                    }
                }
            }

            tbl.Rows.Add(tr_header);

            //Sub Header
            months = Convert.ToInt16(ViewState["months"].ToString());
            cmonth = Convert.ToInt16(ViewState["cmonth"].ToString());
            cyear = Convert.ToInt16(ViewState["cyear"].ToString());


            if (months >= 0)
            {
                TableRow tr_lst_det = new TableRow();              
               
                for (int j = 1; j <= months + 1; j++)
                {
                    TableCell tc_DR_Total_mon = new TableCell();
                    tc_DR_Total_mon.BorderStyle = BorderStyle.Solid;
                    tc_DR_Total_mon.BorderWidth = 1;                    
                    tc_DR_Total_mon.Width = 300;
                    tc_DR_Total_mon.RowSpan = 2;
                    tc_DR_Total_mon.Attributes.Add("Class", "rptCellBorder");
                    Literal lit_DR_Total_mon = new Literal();
                    lit_DR_Total_mon.Text = "<center>Total Drs List</center>";
                    tc_DR_Total_mon.Controls.Add(lit_DR_Total_mon);
                    tr_lst_det.Cells.Add(tc_DR_Total_mon);

                    TableCell tc_lst_month = new TableCell();
                    HyperLink lit_lst_month = new HyperLink();
                    lit_lst_month.Text = "Lst.Drs";
                    tc_lst_month.BorderStyle = BorderStyle.Solid;
                    tc_lst_month.HorizontalAlign = HorizontalAlign.Center;
                    tc_lst_month.BorderWidth = 1;
                    tc_lst_month.Attributes.Add("Class", "rptCellBorder");                   
                    tc_lst_month.ColumnSpan = dsDoctor.Tables[0].Rows.Count;
                    tc_lst_month.Controls.Add(lit_lst_month);
                    tr_lst_det.Cells.Add(tc_lst_month);

                    TableCell tc_msd_month = new TableCell();
                    HyperLink lit_msd_month = new HyperLink();
                    lit_msd_month.Text = "Met.Drs";
                    tc_msd_month.BorderStyle = BorderStyle.Solid;
                    tc_msd_month.HorizontalAlign = HorizontalAlign.Center;
                    tc_msd_month.BorderWidth = 1;
                    tc_msd_month.Attributes.Add("Class", "rptCellBorder");           
                    tc_msd_month.ColumnSpan = dsDoctor.Tables[0].Rows.Count;
                    tc_msd_month.Controls.Add(lit_msd_month);
                    tr_lst_det.Cells.Add(tc_msd_month);

                    TableCell tc_DR_Total_mon_tot = new TableCell();
                    tc_DR_Total_mon_tot.BorderStyle = BorderStyle.Solid;
                    tc_DR_Total_mon_tot.BorderWidth = 1;
                    tc_DR_Total_mon_tot.Attributes.Add("Class", "rptCellBorder");
                   
                    tc_DR_Total_mon_tot.Width = 300;
                    tc_DR_Total_mon_tot.ColumnSpan  = 2;
                    Literal lit_DR_Total_mon_tot = new Literal();
                    lit_DR_Total_mon_tot.Text = "<center>Total</center>";
                    tc_DR_Total_mon_tot.Controls.Add(lit_DR_Total_mon_tot);
                    tr_lst_det.Cells.Add(tc_DR_Total_mon_tot);

                    //TableRow tt = new TableRow();

                    //TableCell tc_lst_month_tot = new TableCell();
                    //HyperLink lit_lst_month_tot = new HyperLink();
                    //lit_lst_month_tot.Text = "Met";

                    //tc_lst_month_tot.BorderStyle = BorderStyle.Solid;
                    //tc_lst_month_tot.HorizontalAlign = HorizontalAlign.Center;
                    //tc_lst_month_tot.BorderWidth = 1;
                    //tc_lst_month_tot.BackColor = System.Drawing.Color.Honeydew;
                    
                    //tc_lst_month_tot.Controls.Add(lit_lst_month_tot);
                    //tt.Cells.Add(tc_lst_month_tot);

                    //TableCell tc_msd_month_tot = new TableCell();
                    //HyperLink lit_msd_month_tot = new HyperLink();
                    //lit_msd_month_tot.Text = "Missed";

                    //tc_msd_month_tot.BorderStyle = BorderStyle.Solid;
                    //tc_msd_month_tot.HorizontalAlign = HorizontalAlign.Center;
                    //tc_msd_month_tot.BorderWidth = 1;
                    //tc_msd_month_tot.BackColor = System.Drawing.Color.PapayaWhip;
                    
                    //tc_msd_month_tot.Controls.Add(lit_msd_month_tot);
                     //tt.Cells.Add(tc_msd_month_tot);

                    //tbl.Rows.Add(tt);
                    
                    cmonth = cmonth + 1;
                    if (cmonth == 13)
                    {
                        cmonth = 1;
                        cyear = cyear + 1;
                    }
                }

                if (Session["sf_type"].ToString() == "1")
                {
                    tr_lst_det.Attributes.Add("Class", "MRBackColor");

                }
                else if (Session["sf_type"].ToString() == "2")
                {
                    tr_lst_det.Attributes.Add("Class", "MGRBackColor");
                }
                else
                {
                    //tr_lst_det.BackColor = System.Drawing.Color.FromName("#A6A6D2");
                    tr_lst_det.Attributes.Add("Class", "Backcolor");
                }


               // tr_lst_det.BackColor = System.Drawing.Color.FromName("#A6A6D2");
                tbl.Rows.Add(tr_lst_det);
            }

            if (months >= 0)
            {
                TableRow tr_catg = new TableRow();
                tr_catg.Attributes.Add("Class", "Backcolor");
              //  tr_catg.BackColor = System.Drawing.Color.FromName("#A6A6D2");
                if (Session["sf_type"].ToString() == "1")
                {
                    tr_catg.Attributes.Add("Class", "MRBackColor");

                }
                else if (Session["sf_type"].ToString() == "2")
                {
                    tr_catg.Attributes.Add("Class", "MGRBackColor");
                }
                else
                {
                    tr_catg.Attributes.Add("Class", "Backcolor");
                }

                for (int j = 1; j <= (months + 1) * 2; j++)
                {
                    if (dsDoctor.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dataRow in dsDoctor.Tables[0].Rows)
                        {
                            TableCell tc_catg_name = new TableCell();
                            tc_catg_name.BorderStyle = BorderStyle.Solid;
                            tc_catg_name.BorderWidth = 1;
                            tc_catg_name.Attributes.Add("Class", "rptCellBorder");
                            if ((j % 2) == 1)
                            {
                                //tc_catg_name.BackColor = System.Drawing.Color.Honeydew;
                            }
                            else
                            {
                                //tc_catg_name.BackColor = System.Drawing.Color.PapayaWhip;
                            }
                            tc_catg_name.Width = 30;
                            Literal lit_catg_name = new Literal();
                            lit_catg_name.Text = "<center>" + dataRow["Doc_Cat_SName"].ToString() + "</center>";
                            tc_catg_name.Controls.Add(lit_catg_name);
                            tr_catg.Cells.Add(tc_catg_name);
                        }

                        if ((j % 2) == 1)
                        {
                        }
                        else
                        {
                            TableCell tc_catg_tot_met = new TableCell();
                            tc_catg_tot_met.BorderStyle = BorderStyle.Solid;
                            tc_catg_tot_met.BorderWidth = 1;
                            tc_catg_tot_met.Attributes.Add("Class", "rptCellBorder");    
                            tc_catg_tot_met.Width = 30;
                            Literal lit_catg_tot_met = new Literal();
                            lit_catg_tot_met.Text = "<center>Met</center>";
                            tc_catg_tot_met.Controls.Add(lit_catg_tot_met);
                            tr_catg.Cells.Add(tc_catg_tot_met);

                            TableCell tc_catg_tot_miss = new TableCell();
                            tc_catg_tot_miss.BorderStyle = BorderStyle.Solid;
                            tc_catg_tot_miss.BorderWidth = 1;
                            tc_catg_tot_miss.Attributes.Add("Class", "rptCellBorder");   
                            tc_catg_tot_miss.Width = 30;
                            Literal lit_catg_tot_miss = new Literal();
                            lit_catg_tot_miss.Text = "<center>Missed</center>";
                            tc_catg_tot_miss.Controls.Add(lit_catg_tot_miss);
                            tr_catg.Cells.Add(tc_catg_tot_miss);
                        }
                        tbl.Rows.Add(tr_catg);


                        //tbl.Rows.Add(tr_catg);
                    }


                }
            }

            // Details Section
            string sURL = string.Empty;
            int iCount = 0;
            int iCnt = 0;
            int tot_met = 0;
            int tot_miss = 0;

            foreach (DataRow drFF in dsSalesForce.Tables[0].Rows)
            {
                ListedDR lstDR = new ListedDR();
                iCnt = lstDR.RecordCount(drFF["sf_code"].ToString());

                TableRow tr_det = new TableRow();
                tr_det.Attributes.Add("Class", "tblCellFont");
                tr_det.BackColor = System.Drawing.Color.White;
                iCount += 1;
                TableCell tc_det_SNo = new TableCell();
                Literal lit_det_SNo = new Literal();
                lit_det_SNo.Text = "<center>" + iCount.ToString() + "</center>";
                tc_det_SNo.BorderStyle = BorderStyle.Solid;
                tc_det_SNo.BorderWidth = 1;
                
                tc_det_SNo.Width = 50;
                tc_det_SNo.Controls.Add(lit_det_SNo);
                tr_det.Cells.Add(tc_det_SNo);

                TableCell tc_det_doc_code = new TableCell();
                Literal lit_det_doc_code = new Literal();
                lit_det_doc_code.Text = "&nbsp;" + drFF["sf_code"].ToString();
                tc_det_doc_code.BorderStyle = BorderStyle.Solid;
                tc_det_doc_code.BorderWidth = 1;
                
                tc_det_doc_code.Controls.Add(lit_det_doc_code);
                tc_det_doc_code.Visible = false;
                tr_det.Cells.Add(tc_det_doc_code);

                TableCell tc_det_doc_name = new TableCell();
                //Literal lit_det_doc_name = new Literal();
                //lit_det_doc_name.Text = "&nbsp;" + drFF["SF_Name"].ToString();
                HyperLink lit_det_doc_name = new HyperLink();
                lit_det_doc_name.Text = "&nbsp;" + drFF["SF_Name"].ToString();
                tc_det_doc_name.HorizontalAlign = HorizontalAlign.Left;
                if ((drFF["SF_Type"].ToString() == "2")&& mode == 2)
                {
                    lit_det_doc_name.Attributes.Add("href", "javascript:showModalPopUp('" + drFF["sf_code"].ToString() + "', '" + ddlFMonth.SelectedValue.ToString() + "', '" + ddlFYear.SelectedValue.ToString() + "', '" + ddlTMonth.SelectedValue.ToString() + "', '" + ddlTYear.SelectedValue.ToString() + "', '" + ddlType.SelectedValue.ToString() + "')");
                    
                    //sURL = "rptDcrViewDetails.aspx?sf_Name=" + "&sf_code=" + sf_code + "&Year=" + cyear + "&Month=" + cmonth + "&div_code=" + div_code + " &Day=" + lit_det_Ses.Text + "";
                    //lit_det_doc_name.Attributes["onclick"] = "window.open('" + sURL + "','_blank','PopUp',0,'resizable=yes,toolbar=no,menubar=no,status=no,width=1000,height=600,left=0,top=0');";
                    //lit_det_doc_name.NavigateUrl = "#";
                }
                tc_det_doc_name.BorderStyle = BorderStyle.Solid;
                tc_det_doc_name.BorderWidth = 1;
                tc_det_doc_name.Width = 500;
                tc_det_doc_name.Controls.Add(lit_det_doc_name);
                tr_det.Cells.Add(tc_det_doc_name);

                TableCell tc_det_sf_HQ = new TableCell();
                Literal lit_det_sf_hq = new Literal();
                lit_det_sf_hq.Text = "&nbsp;" + drFF["Sf_HQ"].ToString();
                tc_det_sf_HQ.BorderStyle = BorderStyle.Solid;
                tc_det_sf_HQ.BorderWidth = 1;               
                tc_det_sf_HQ.Controls.Add(lit_det_sf_hq);
                //tc_det_sf_HQ.Visible = false;
                tr_det.Cells.Add(tc_det_sf_HQ);


                TableCell tc_det_sf_des = new TableCell();
                Literal lit_det_sf_des = new Literal();
                lit_det_sf_des.Text = "&nbsp;" + drFF["Designation_Short_Name"].ToString();
                tc_det_sf_des.BorderStyle = BorderStyle.Solid;
                tc_det_sf_des.BorderWidth = 1;
                
                tc_det_sf_des.Controls.Add(lit_det_sf_des);
                //tc_det_sf_HQ.Visible = false;
                tr_det.Cells.Add(tc_det_sf_des);


                months = Convert.ToInt16(ViewState["months"].ToString());
                cmonth = Convert.ToInt16(ViewState["cmonth"].ToString());
                cyear = Convert.ToInt16(ViewState["cyear"].ToString());

                if (months >= 0)
                {
                    for (int j = 1; j <= months + 1; j++)
                    {
                        tot_met = 0;
                        tot_miss = 0;
                        doctor_total = 0;
                        if (cmonth == 12)
                        {
                            sCurrentDate = "01-01-" + (cyear + 1);
                        }
                        else
                        {
                            sCurrentDate = (cmonth + 1) + "-01-" + cyear;
                            //sCurrentDate = cmonth  + "-01-" + cyear;
                        }

                        dtCurrent = Convert.ToDateTime(sCurrentDate);

                        if (dsDoctor.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow dataRow in dsDoctor.Tables[0].Rows)
                            {
                                dsDoc = sf.Catg_LST_Report(drFF["sf_code"].ToString(), div_code, dtCurrent,cmonth,cyear, Convert.ToInt32(dataRow["Doc_Cat_Code"].ToString()));

                                if (dsDoc.Tables[0].Rows.Count > 0)
                                    tot_dr = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

                                doctor_total = doctor_total + Convert.ToInt16(tot_dr);
                            }

                            TableCell tc_det_sf_tot = new TableCell();
                            Literal lit_det_sf_tot = new Literal();
                            lit_det_sf_tot.Text = "&nbsp;" + doctor_total.ToString();
                            tc_det_sf_tot.BorderStyle = BorderStyle.Solid;
                            //tc_det_sf_tot.BackColor = System.Drawing.Color.LavenderBlush;
                            tc_det_sf_tot.BorderWidth = 1;
                            
                            tc_det_sf_tot.HorizontalAlign = HorizontalAlign.Center;
                            tc_det_sf_tot.VerticalAlign = VerticalAlign.Middle;
                            tc_det_sf_tot.Controls.Add(lit_det_sf_tot);
                            tr_det.Cells.Add(tc_det_sf_tot);
                            
                            foreach (DataRow dataRow in dsDoctor.Tables[0].Rows)
                            {
                                dsDoc = sf.Catg_LST_Report(drFF["sf_code"].ToString(), div_code, dtCurrent,cmonth,cyear, Convert.ToInt32(dataRow["Doc_Cat_Code"].ToString()));

                                if (dsDoc.Tables[0].Rows.Count > 0)
                                    tot_dr = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                                

                                TableCell tc_lst_month = new TableCell();
                                HyperLink hyp_lst_month = new HyperLink();
                                if (tot_dr != "0")
                                {

                                    tot_miss = tot_miss + Convert.ToInt16( tot_dr);
                                    hyp_lst_month.Text = tot_dr;
                                }
                                else
                                {
                                    hyp_lst_month.Text = "-";
                                }
                                tc_lst_month.BorderStyle = BorderStyle.Solid;
                                tc_lst_month.BorderWidth = 1;
                               
                                //tc_lst_month.BackColor = System.Drawing.Color.Honeydew;
                                tc_lst_month.Width = 200;
                                tc_lst_month.HorizontalAlign = HorizontalAlign.Center;
                                tc_lst_month.VerticalAlign = VerticalAlign.Middle;
                                tc_lst_month.Controls.Add(hyp_lst_month);
                                tr_det.Cells.Add(tc_lst_month);
                            }
                        }

                        DCR dc = new DCR();

                        if (dsDoctor.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow dataRow in dsDoctor.Tables[0].Rows)
                            {
                                dsDoc = sf.Catg_LST_Report(drFF["sf_code"].ToString(), div_code, dtCurrent,cmonth,cyear, Convert.ToInt32(dataRow["Doc_Cat_Code"].ToString()));
                                if (dsDoc.Tables[0].Rows.Count > 0)
                                    tot_dr = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                               
                                dsDCR = dc.Catg_Visit_Report1(drFF["sf_code"].ToString(), div_code, cmonth, cyear, Convert.ToInt32(dataRow["Doc_Cat_Code"].ToString()),mode);
                                if (dsDCR.Tables[0].Rows.Count > 0)
                                    tot_dcr_dr = dsDCR.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

                                TableCell tc_msd_month = new TableCell();
                                HyperLink hyp_msd_month = new HyperLink();
                                //if (tot_dr != "0")
                                if (tot_dcr_dr != "0")
                                {
                                    //imissed_dr = Convert.ToInt16(tot_dr) - Convert.ToInt16(tot_dcr_dr);
                                    imissed_dr = Convert.ToInt16(tot_dcr_dr);
                                    hyp_msd_month.Text = imissed_dr.ToString();
                                    if (imissed_dr > 0)
                                    {
                                        tot_met = tot_met+imissed_dr;                                        
                                        hyp_msd_month.Attributes.Add("href", "javascript:showVisitDR_type('" + drFF["sf_code"].ToString() + "', '" + cmonth.ToString() + "', '" + cyear.ToString() + "', '" + ddlType.SelectedValue.ToString() + "','" + Convert.ToInt32(dataRow["Doc_Cat_Code"].ToString()) + "')");
                                    }
                                }
                                else
                                {
                                    hyp_msd_month.Text = "-";
                                }

                                tc_msd_month.BorderStyle = BorderStyle.Solid;
                                tc_msd_month.BorderWidth = 1;
                                
                                //tc_msd_month.BackColor = System.Drawing.Color.PapayaWhip;
                                tc_msd_month.Width = 200;
                                tc_msd_month.HorizontalAlign = HorizontalAlign.Center;
                                tc_msd_month.VerticalAlign = VerticalAlign.Middle;
                                tc_msd_month.Controls.Add(hyp_msd_month);
                                tr_det.Cells.Add(tc_msd_month);
                            }
                        }

                        TableCell tc_det_sf_tt = new TableCell();
                        //Literal lit_det_sf_tt = new Literal();
                        HyperLink lit_det_sf_tt = new HyperLink();
                        if (tot_met > 0)
                        {
                            lit_det_sf_tt.Text = lit_det_sf_tt.Text + tot_met.ToString();
                            lit_det_sf_tt.Attributes.Add("href", "javascript:showVisitDR_type('" + drFF["sf_code"].ToString() + "', '" + cmonth.ToString() + "', '" + cyear.ToString() + "', '" + ddlType.SelectedValue.ToString() + "','' )");
                        }
                        else
                        {
                            lit_det_sf_tt.Text = "-";
                        }

                        tc_det_sf_tt.BorderStyle = BorderStyle.Solid;
                        //tc_det_sf_tt.BackColor = System.Drawing.Color.LemonChiffon;
                        tc_det_sf_tt.BorderWidth = 1;
                       
                        tc_det_sf_tt.HorizontalAlign = HorizontalAlign.Center;
                        tc_det_sf_tt.VerticalAlign = VerticalAlign.Middle;
                        tc_det_sf_tt.Controls.Add(lit_det_sf_tt);
                        tr_det.Cells.Add(tc_det_sf_tt);

                        //if(tot_met > 
                        tot_miss = tot_miss - tot_met;

                        TableCell tc_det_sf_tt_miss = new TableCell();
                        Literal lit_det_sf_tt_miss = new Literal();
                        lit_det_sf_tt_miss.Text = "&nbsp;" + tot_miss.ToString();
                        tc_det_sf_tt_miss.BorderStyle = BorderStyle.Solid;
                        //tc_det_sf_tt_miss.BackColor = System.Drawing.Color.LemonChiffon;
                        tc_det_sf_tt_miss.BorderWidth = 1;
                        
                        tc_det_sf_tt_miss.HorizontalAlign = HorizontalAlign.Center;
                        tc_det_sf_tt_miss.VerticalAlign = VerticalAlign.Middle;
                        tc_det_sf_tt_miss.Controls.Add(lit_det_sf_tt_miss);
                        tr_det.Cells.Add(tc_det_sf_tt_miss);

                        cmonth = cmonth + 1;
                        if (cmonth == 13)
                        {
                            cmonth = 1;
                            cyear = cyear + 1;
                        }

                    }
                }
                                            
                tbl.Rows.Add(tr_det);

            }
        }
    }

    private void FillSpec()
    {
        tbl.Rows.Clear();
        doctor_total = 0;
        SalesForce sf = new SalesForce();
        DCR dcf = new DCR();

        if (ddlMode.SelectedValue.ToString() == "1")
        {
            if (ddlMR.SelectedIndex > 0)
            {
                mode = 1;
                dsSalesForce = dcf.SF_Self_Report(div_code, ddlMR.SelectedValue.ToString());
            }
            else
            {
                mode = 1;
                dsSalesForce = dcf.SF_Self_Report(div_code, ddlFieldForce.SelectedValue.ToString());
            }
        }
        else
        {
            mode = 2;
            DataSet DsAudit = sf.SF_Hierarchy(div_code, ddlFieldForce.SelectedValue.ToString());
             if (DsAudit.Tables[0].Rows.Count > 0)
             {
               dsSalesForce = sf.SF_ReportingTo_TourPlan(div_code, ddlFieldForce.SelectedValue.ToString());
             }
             else
             {
                 DataTable dt = sf.getAuditManagerTeam_GetMGR(div_code, ddlFieldForce.SelectedValue.ToString(), 0);
                 dsmgrsf.Tables.Add(dt);
                 dsmgrsf.Tables[0].Rows[0].Delete();
                 dsSalesForce = dsmgrsf;
             }
        }

        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            lblModelevel.Text = "Listed Doctor Speciality wise Visit Details report for " + ddlFMonth.SelectedItem.Text + " " + ddlFYear.SelectedValue + " to " + ddlTMonth.SelectedItem.Text + " " + ddlTYear.SelectedValue;
            TableRow tr_header = new TableRow();
            tr_header.BorderStyle = BorderStyle.Solid;
            tr_header.BorderWidth = 1;            

            TableCell tc_SNo = new TableCell();
            tc_SNo.BorderStyle = BorderStyle.Solid;
            tc_SNo.BorderWidth = 1;
            tc_SNo.Width = 50;
            //tc_SNo.Style.Add("font-family", "Calibri");
            //tc_SNo.Style.Add("font-size", "10pt");
            tc_SNo.Attributes.Add("Class", "rptCellBorder");
            tc_SNo.RowSpan = 3;
            Literal lit_SNo = new Literal();
            lit_SNo.Text = "S.No";
            tc_SNo.Controls.Add(lit_SNo);
            tr_header.Cells.Add(tc_SNo);
          //  tr_header.BackColor = System.Drawing.Color.FromName("#A6A6D2");
            if (Session["sf_type"].ToString() == "1")
            {
                tr_header.Attributes.Add("Class", "MRBackColor");
            }
            else if (Session["sf_type"].ToString() == "2")
            {
                tr_header.Attributes.Add("Class", "MGRBackColor");
            }
            else
            {
                //tr_header.BackColor = System.Drawing.Color.FromName("#A6A6D2");
                tr_header.Attributes.Add("Class", "Backcolor");
            }

            TableCell tc_DR_Code = new TableCell();
            tc_DR_Code.BorderStyle = BorderStyle.Solid;
            tc_DR_Code.BorderWidth = 1;
            //tc_DR_Code.Style.Add("font-family", "Calibri");
            //tc_DR_Code.Style.Add("font-size", "10pt");
            tc_DR_Code.Attributes.Add("Class", "rptCellBorder");
            tc_DR_Code.Width = 400;
            tc_DR_Code.RowSpan = 3;
            Literal lit_DR_Code = new Literal();
            lit_DR_Code.Text = "<center>SF Code</center>";
            tc_DR_Code.Controls.Add(lit_DR_Code);
            tc_DR_Code.Visible = false;
            tr_header.Cells.Add(tc_DR_Code);

            TableCell tc_DR_Name = new TableCell();
            tc_DR_Name.BorderStyle = BorderStyle.Solid;
            tc_DR_Name.BorderWidth = 1;
            //tc_DR_Name.Style.Add("font-family", "Calibri");
            //tc_DR_Name.Style.Add("font-size", "10pt");
            tc_DR_Name.Width = 500;
            tc_DR_Name.RowSpan = 3;
            Literal lit_DR_Name = new Literal();
            lit_DR_Name.Text = "<center>Field&nbspForce&nbspName</center>";
            tc_DR_Name.Attributes.Add("Class", "rptCellBorder");
            tc_DR_Name.Controls.Add(lit_DR_Name);
            tr_header.Cells.Add(tc_DR_Name);

            TableCell tc_DR_HQ = new TableCell();
            tc_DR_HQ.BorderStyle = BorderStyle.Solid;
            tc_DR_HQ.BorderWidth = 1;
            //tc_DR_HQ.Style.Add("font-family", "Calibri");
            //tc_DR_HQ.Style.Add("font-size", "10pt");
            tc_DR_HQ.Width = 500;
            tc_DR_HQ.RowSpan = 3;
            Literal lit_DR_HQ = new Literal();
            lit_DR_HQ.Text = "<center>HQ</center>";
            tc_DR_HQ.Attributes.Add("Class", "rptCellBorder");
            tc_DR_HQ.Controls.Add(lit_DR_HQ);
            tr_header.Cells.Add(tc_DR_HQ);

            TableCell tc_DR_Des = new TableCell();
            tc_DR_Des.BorderStyle = BorderStyle.Solid;
            tc_DR_Des.BorderWidth = 1;
            //tc_DR_Des.Style.Add("font-family", "Calibri");
            //tc_DR_Des.Style.Add("font-size", "10pt");
            tc_DR_Des.Width = 500;
            tc_DR_Des.RowSpan = 3;
            Literal lit_DR_Des = new Literal();
            lit_DR_Des.Text = "<center>Designation</center>";
            tc_DR_Des.Attributes.Add("Class", "rptCellBorder");
            tc_DR_Des.Controls.Add(lit_DR_Des);
            tr_header.Cells.Add(tc_DR_Des);

            int months = (Convert.ToInt32(ddlTYear.SelectedValue.ToString()) - Convert.ToInt32(ddlFYear.SelectedValue.ToString())) * 12 + Convert.ToInt32(ddlTMonth.SelectedValue.ToString()) - Convert.ToInt32(ddlFMonth.SelectedValue.ToString()); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
            int cmonth = Convert.ToInt32(ddlFMonth.SelectedValue.ToString());
            int cyear = Convert.ToInt32(ddlFYear.SelectedValue.ToString());

            ViewState["months"] = months;
            ViewState["cmonth"] = cmonth;
            ViewState["cyear"] = cyear;

            Doctor dr = new Doctor();
            dsDoctor = dr.getDocSpec(div_code);

            if (months >= 0)
            {
                for (int j = 1; j <= months + 1; j++)
                {
                    
                    TableCell tc_month = new TableCell();
                    tc_month.ColumnSpan = (dsDoctor.Tables[0].Rows.Count * 2) + 3;
                    //tc_month.RowSpan = 2;
                    Literal lit_month = new Literal();
                    lit_month.Text = "&nbsp;" + sf.getMonthName(cmonth.ToString()) + "-" + cyear;
                    tc_month.BorderStyle = BorderStyle.Solid;
                    tc_month.BorderWidth = 1;
                    //tc_month.Style.Add("font-family", "Calibri");
                    //tc_month.Style.Add("font-size", "10pt");
                    tc_month.Attributes.Add("Class", "rptCellBorder");
                    tc_month.HorizontalAlign = HorizontalAlign.Center;
                    //tc_month.Width = 200;
                    tc_month.Controls.Add(lit_month);
                    tr_header.Cells.Add(tc_month);
                    cmonth = cmonth + 1;
                    if (cmonth == 13)
                    {
                        cmonth = 1;
                        cyear = cyear + 1;
                    }
                }
            }

            tbl.Rows.Add(tr_header);

            //Sub Header
            months = Convert.ToInt16(ViewState["months"].ToString());
            cmonth = Convert.ToInt16(ViewState["cmonth"].ToString());
            cyear = Convert.ToInt16(ViewState["cyear"].ToString());


            if (months >= 0)
            {
                TableRow tr_lst_det = new TableRow();
                for (int j = 1; j <= months + 1; j++)
                {
                    TableCell tc_DR_Total_mon = new TableCell();
                    tc_DR_Total_mon.BorderStyle = BorderStyle.Solid;
                    tc_DR_Total_mon.BorderWidth = 1;
                    //tc_DR_Total_mon.Style.Add("font-family", "Calibri");
                    //tc_DR_Total_mon.Style.Add("font-size", "10pt");
                    tc_DR_Total_mon.Attributes.Add("Class", "rptCellBorder");
                    //tc_DR_Total_mon.BackColor = System.Drawing.Color.LavenderBlush;
                    tc_DR_Total_mon.Width = 300;
                    tc_DR_Total_mon.RowSpan = 2;
                    Literal lit_DR_Total_mon = new Literal();
                    lit_DR_Total_mon.Text = "<center>Total Drs List</center>";
                    tc_DR_Total_mon.Controls.Add(lit_DR_Total_mon);
                    tr_lst_det.Cells.Add(tc_DR_Total_mon);

                    TableCell tc_lst_month = new TableCell();
                    HyperLink lit_lst_month = new HyperLink();
                    lit_lst_month.Text = "Lst.Drs";

                    tc_lst_month.BorderStyle = BorderStyle.Solid;
                    tc_lst_month.HorizontalAlign = HorizontalAlign.Center;
                    tc_lst_month.BorderWidth = 1;
                    //tc_lst_month.Style.Add("font-family", "Calibri");
                    //tc_lst_month.Style.Add("font-size", "10pt");
                    tc_lst_month.Attributes.Add("Class", "rptCellBorder");
                    //tc_lst_month.BackColor = System.Drawing.Color.Honeydew;
                    tc_lst_month.ColumnSpan = dsDoctor.Tables[0].Rows.Count;
                    tc_lst_month.Controls.Add(lit_lst_month);
                    tr_lst_det.Cells.Add(tc_lst_month);

                    TableCell tc_msd_month = new TableCell();
                    HyperLink lit_msd_month = new HyperLink();
                    lit_msd_month.Text = "Met DR";

                    tc_msd_month.BorderStyle = BorderStyle.Solid;
                    tc_msd_month.HorizontalAlign = HorizontalAlign.Center;
                    tc_msd_month.BorderWidth = 1;
                    //tc_msd_month.Style.Add("font-family", "Calibri");
                    //tc_msd_month.Style.Add("font-size", "10pt");
                    tc_msd_month.Attributes.Add("Class", "rptCellBorder");
                    //tc_msd_month.BackColor = System.Drawing.Color.PapayaWhip;
                    tc_msd_month.ColumnSpan = dsDoctor.Tables[0].Rows.Count;
                    tc_msd_month.Controls.Add(lit_msd_month);
                    tr_lst_det.Cells.Add(tc_msd_month);

                    TableCell tc_DR_Total_mon_tot = new TableCell();
                    tc_DR_Total_mon_tot.BorderStyle = BorderStyle.Solid;
                    tc_DR_Total_mon_tot.BorderWidth = 1;
                    //tc_DR_Total_mon_tot.Style.Add("font-family", "Calibri");
                    //tc_DR_Total_mon_tot.Style.Add("font-size", "10pt");
                    tc_DR_Total_mon_tot.Attributes.Add("Class", "rptCellBorder");
                    //tc_DR_Total_mon_tot.BackColor = System.Drawing.Color.LemonChiffon;
                    tc_DR_Total_mon_tot.Width = 300;
                    tc_DR_Total_mon_tot.ColumnSpan = 2;
                    Literal lit_DR_Total_mon_tot = new Literal();
                    lit_DR_Total_mon_tot.Text = "<center>Total</center>";
                    tc_DR_Total_mon_tot.Controls.Add(lit_DR_Total_mon_tot);
                    tr_lst_det.Cells.Add(tc_DR_Total_mon_tot);

                    cmonth = cmonth + 1;
                    if (cmonth == 13)
                    {
                        cmonth = 1;
                        cyear = cyear + 1;
                    }
                }
                //tr_lst_det.BackColor = System.Drawing.Color.FromName("#A6A6D2");
                if (Session["sf_type"].ToString() == "1")
                {
                    tr_lst_det.Attributes.Add("Class", "MRBackColor");

                }
                else if (Session["sf_type"].ToString() == "2")
                {
                    tr_lst_det.Attributes.Add("Class", "MGRBackColor");
                }
                else
                {
                    tr_lst_det.Attributes.Add("Class", "Backcolor");
                }
                tbl.Rows.Add(tr_lst_det);
            }

            if (months >= 0)
            {
                TableRow tr_catg = new TableRow();
             //   tr_catg.BackColor = System.Drawing.Color.FromName("#A6A6D2");
                if (Session["sf_type"].ToString() == "1")
                {
                    tr_catg.Attributes.Add("Class", "MRBackColor");
                }
                else if (Session["sf_type"].ToString() == "2")
                {
                    tr_catg.Attributes.Add("Class", "MGRBackColor");
                }
                else
                {
                    //tr_catg.BackColor = System.Drawing.Color.FromName("#A6A6D2");
                    tr_catg.Attributes.Add("Class", "Backcolor");
                }

                for (int j = 1; j <= (months + 1) * 2; j++)
                {
                    if (dsDoctor.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dataRow in dsDoctor.Tables[0].Rows)
                        {
                            TableCell tc_catg_name = new TableCell();
                            tc_catg_name.BorderStyle = BorderStyle.Solid;
                            tc_catg_name.BorderWidth = 1;
                            tc_catg_name.Style.Add("font-family", "Calibri");
                            tc_catg_name.Style.Add("font-size", "10pt");
                            tc_catg_name.Width = 30;
                            if ((j % 2) == 1)
                            {
                               // tc_catg_name.BackColor = System.Drawing.Color.Honeydew;
                            }
                            else
                            {
                               // tc_catg_name.BackColor = System.Drawing.Color.PapayaWhip;
                            }
                            Literal lit_catg_name = new Literal();
                            lit_catg_name.Text = "<center>" + dataRow["Doc_Cat_Name"].ToString() + "</center>";
                            tc_catg_name.Attributes.Add("Class", "rptCellBorder");
                            tc_catg_name.Controls.Add(lit_catg_name);
                            tr_catg.Cells.Add(tc_catg_name);
                        }


                        if ((j % 2) == 1)
                        {
                        }
                        else
                        {
                            TableCell tc_catg_tot_met = new TableCell();
                            tc_catg_tot_met.BorderStyle = BorderStyle.Solid;
                            tc_catg_tot_met.BorderWidth = 1;
                            //tc_catg_tot_met.Style.Add("font-family", "Calibri");
                            //tc_catg_tot_met.Style.Add("font-size", "10pt");
                            tc_catg_tot_met.Attributes.Add("Class", "rptCellBorder");
                            //tc_catg_tot_met.BackColor = System.Drawing.Color.LemonChiffon;
                            tc_catg_tot_met.Width = 30;
                            Literal lit_catg_tot_met = new Literal();
                            lit_catg_tot_met.Text = "<center>Met</center>";
                            tc_catg_tot_met.Controls.Add(lit_catg_tot_met);
                            tr_catg.Cells.Add(tc_catg_tot_met);

                            TableCell tc_catg_tot_miss = new TableCell();
                            tc_catg_tot_miss.BorderStyle = BorderStyle.Solid;
                            tc_catg_tot_miss.BorderWidth = 1;
                            //tc_catg_tot_miss.Style.Add("font-family", "Calibri");
                            //tc_catg_tot_miss.Style.Add("font-size", "10pt");
                            tc_catg_tot_miss.Attributes.Add("Class", "rptCellBorder");
                            //tc_catg_tot_miss.BackColor = System.Drawing.Color.LemonChiffon;
                            tc_catg_tot_miss.Width = 30;
                            Literal lit_catg_tot_miss = new Literal();
                            lit_catg_tot_miss.Text = "<center>Missed</center>";
                            tc_catg_tot_miss.Controls.Add(lit_catg_tot_miss);
                            tr_catg.Cells.Add(tc_catg_tot_miss);
                        }

                        tbl.Rows.Add(tr_catg);
                    }
                }
            }

            // Details Section
            string sURL = string.Empty;
            int iCount = 0;
            int iCnt = 0;
            int tot_met = 0;
            int tot_miss = 0;

            foreach (DataRow drFF in dsSalesForce.Tables[0].Rows)
            {
                ListedDR lstDR = new ListedDR();
                iCnt = lstDR.RecordCount(drFF["sf_code"].ToString());

                TableRow tr_det = new TableRow();
                tr_det.Attributes.Add("Class", "tblCellFont");
                tr_det.BackColor = System.Drawing.Color.White;
                
                iCount += 1;
                TableCell tc_det_SNo = new TableCell();
                Literal lit_det_SNo = new Literal();
                lit_det_SNo.Text = "<center>" + iCount.ToString() + "</center>";
                
                tc_det_SNo.BorderStyle = BorderStyle.Solid;
                tc_det_SNo.BorderWidth = 1;
                //tc_det_SNo.Style.Add("font-family", "Calibri");
                //tc_det_SNo.Style.Add("font-size", "10pt");
                tc_det_SNo.Width = 50;
                tc_det_SNo.Controls.Add(lit_det_SNo);
                tr_det.Cells.Add(tc_det_SNo);

                TableCell tc_det_doc_code = new TableCell();
                Literal lit_det_doc_code = new Literal();
                lit_det_doc_code.Text = "&nbsp;" + drFF["sf_code"].ToString();
                tc_det_doc_code.BorderStyle = BorderStyle.Solid;
                tc_det_doc_code.BorderWidth = 1;
                //tc_det_doc_code.Style.Add("font-family", "Calibri");
                //tc_det_doc_code.Style.Add("font-size", "10pt");
                tc_det_doc_code.Controls.Add(lit_det_doc_code);
                tc_det_doc_code.Visible = false;
                tr_det.Cells.Add(tc_det_doc_code);

                TableCell tc_det_doc_name = new TableCell();
                //Literal lit_det_doc_name = new Literal();
                //lit_det_doc_name.Text = "&nbsp;" + drFF["SF_Name"].ToString();

                HyperLink lit_det_doc_name = new HyperLink();
                lit_det_doc_name.Text = "&nbsp;" + drFF["SF_Name"].ToString();
                tc_det_doc_name.HorizontalAlign = HorizontalAlign.Left;
                if( (drFF["SF_Type"].ToString() == "2")&& mode == 2)
                {

                    lit_det_doc_name.Attributes.Add("href", "javascript:showModalPopUp('" + drFF["sf_code"].ToString() + "', '" + ddlFMonth.SelectedValue.ToString() + "', '" + ddlFYear.SelectedValue.ToString() + "', '" + ddlTMonth.SelectedValue.ToString() + "', '" + ddlTYear.SelectedValue.ToString() + "', '" + ddlType.SelectedValue.ToString() + "')");
                }
                tc_det_doc_name.BorderStyle = BorderStyle.Solid;
                tc_det_doc_name.BorderWidth = 1;
                //tc_det_doc_name.Style.Add("font-family", "Calibri");
                //tc_det_doc_name.Style.Add("font-size", "10pt");
                tc_det_doc_name.Width = 800;
                tc_det_doc_name.Controls.Add(lit_det_doc_name);
                tr_det.Cells.Add(tc_det_doc_name);

                TableCell tc_det_sf_HQ = new TableCell();
                Literal lit_det_sf_hq = new Literal();
                lit_det_sf_hq.Text = "&nbsp;" + drFF["Sf_HQ"].ToString();
               
                tc_det_sf_HQ.BorderStyle = BorderStyle.Solid;
                tc_det_sf_HQ.BorderWidth = 1;
                //tc_det_sf_HQ.Style.Add("font-family", "Calibri");
                //tc_det_sf_HQ.Style.Add("font-size", "10pt");
                tc_det_sf_HQ.Controls.Add(lit_det_sf_hq);
                //tc_det_sf_HQ.Visible = false;
                tr_det.Cells.Add(tc_det_sf_HQ);


                TableCell tc_det_sf_des = new TableCell();
                Literal lit_det_sf_des = new Literal();
                lit_det_sf_des.Text = "&nbsp;" + drFF["Designation_Short_Name"].ToString();
                
                tc_det_sf_des.BorderStyle = BorderStyle.Solid;
                tc_det_sf_des.BorderWidth = 1;
                //tc_det_sf_des.Style.Add("font-family", "Calibri");
                //tc_det_sf_des.Style.Add("font-size", "10pt");
                tc_det_sf_des.Controls.Add(lit_det_sf_des);
                //tc_det_sf_HQ.Visible = false;
                tr_det.Cells.Add(tc_det_sf_des);

                months = Convert.ToInt16(ViewState["months"].ToString());
                cmonth = Convert.ToInt16(ViewState["cmonth"].ToString());
                cyear = Convert.ToInt16(ViewState["cyear"].ToString());

                if (months >= 0)
                {
                    for (int j = 1; j <= months + 1; j++)
                    {
                        tot_met = 0;
                        tot_miss = 0;
                        doctor_total = 0;
                        if (cmonth == 12)
                        {
                            sCurrentDate = "01-01-" + (cyear + 1);
                        }
                        else
                        {
                            sCurrentDate = (cmonth + 1) + "-01-" + cyear;
                            //sCurrentDate = cmonth + "-01-" + cyear;
                        }

                        dtCurrent = Convert.ToDateTime(sCurrentDate);

                        //dsDoc = sf.MissedCallReport(drFF["sf_code"].ToString(), div_code, dtCurrent);
                        //dsDCR = sf.DCR_MissedCallReport(drFF["sf_code"].ToString(), div_code, cmonth, cyear);

                        //if (dsDoc.Tables[0].Rows.Count > 0)
                        //    tot_dr = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                        //if (dsDCR.Tables[0].Rows.Count > 0)
                        //    tot_dcr_dr = dsDCR.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

                        if (dsDoctor.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow dataRow in dsDoctor.Tables[0].Rows)
                            {
                                dsDoc = sf.Spec_LST_Report(drFF["sf_code"].ToString(), div_code, dtCurrent, cmonth, cyear, Convert.ToInt32(dataRow["Doc_Cat_Code"].ToString()));

                                if (dsDoc.Tables[0].Rows.Count > 0)
                                    tot_dr = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

                                doctor_total = doctor_total + Convert.ToInt16(tot_dr);
                            }

                            TableCell tc_det_sf_tot = new TableCell();
                            Literal lit_det_sf_tot = new Literal();
                            lit_det_sf_tot.Text = "&nbsp;" + doctor_total.ToString();
                            tc_det_sf_tot.BorderStyle = BorderStyle.Solid;
                            
                            tc_det_sf_tot.BorderWidth = 1;
                            tc_det_sf_tot.Style.Add("font-family", "Calibri");
                            tc_det_sf_tot.Style.Add("font-size", "10pt");
                            tc_det_sf_tot.HorizontalAlign = HorizontalAlign.Center;
                            tc_det_sf_tot.VerticalAlign = VerticalAlign.Middle;
                            tc_det_sf_tot.Controls.Add(lit_det_sf_tot);
                            tr_det.Cells.Add(tc_det_sf_tot);


                            foreach (DataRow dataRow in dsDoctor.Tables[0].Rows)
                            {
                                dsDoc = sf.Spec_LST_Report(drFF["sf_code"].ToString(), div_code, dtCurrent, cmonth, cyear, Convert.ToInt32(dataRow["Doc_Cat_Code"].ToString()));

                                if (dsDoc.Tables[0].Rows.Count > 0)
                                    tot_dr = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

                                TableCell tc_lst_month = new TableCell();
                                HyperLink hyp_lst_month = new HyperLink();
                                if (tot_dr != "0")
                                {

                                    tot_miss = tot_miss + Convert.ToInt16(tot_dr);
                                    hyp_lst_month.Text = tot_dr;
                                }
                                else
                                {
                                    hyp_lst_month.Text = "-";
                                }


                                tc_lst_month.BorderStyle = BorderStyle.Solid;
                                tc_lst_month.BorderWidth = 1;
                                tc_lst_month.Style.Add("font-family", "Calibri");
                                tc_lst_month.Style.Add("font-size", "10pt");
                                tc_lst_month.Width = 200;
                                
                                tc_lst_month.HorizontalAlign = HorizontalAlign.Center;
                                tc_lst_month.VerticalAlign = VerticalAlign.Middle;
                                tc_lst_month.Controls.Add(hyp_lst_month);
                                tr_det.Cells.Add(tc_lst_month);
                            }
                        }

                        if (dsDoctor.Tables[0].Rows.Count > 0)
                        {
                            DCR dc = new DCR();
                            foreach (DataRow dataRow in dsDoctor.Tables[0].Rows)
                            {
                                dsDoc = sf.Spec_LST_Report(drFF["sf_code"].ToString(), div_code, dtCurrent, cmonth, cyear, Convert.ToInt32(dataRow["Doc_Cat_Code"].ToString()));
                                if (dsDoc.Tables[0].Rows.Count > 0)
                                    tot_dr = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

                                dsDCR = dc.Spec_Visit_Report1(drFF["sf_code"].ToString(), div_code, cmonth, cyear, Convert.ToInt32(dataRow["Doc_Cat_Code"].ToString()),mode);
                                if (dsDCR.Tables[0].Rows.Count > 0)
                                    tot_dcr_dr = dsDCR.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

                                TableCell tc_msd_month = new TableCell();
                                HyperLink hyp_msd_month = new HyperLink();
                                //if (tot_dr != "0")
                                if (tot_dcr_dr != "0")
                                {
                                    //imissed_dr = Convert.ToInt16(tot_dr) - Convert.ToInt16(tot_dcr_dr);
                                    imissed_dr = Convert.ToInt16(tot_dcr_dr);
                                    hyp_msd_month.Text = imissed_dr.ToString();
                                    if (imissed_dr > 0)
                                    {
                                        tot_met = tot_met + imissed_dr;
                                        hyp_msd_month.Attributes.Add("href", "javascript:showVisitDR_type('" + drFF["sf_code"].ToString() + "', '" + cmonth.ToString() + "', '" + cyear.ToString() + "', '" + ddlType.SelectedValue.ToString() + "','" + Convert.ToInt32(dataRow["Doc_Cat_Code"].ToString()) + "')");
                                    }
                                }
                                else
                                {
                                    hyp_msd_month.Text = "-";
                                }

                                tc_msd_month.BorderStyle = BorderStyle.Solid;
                                tc_msd_month.BorderWidth = 1;
                                tc_msd_month.Style.Add("font-family", "Calibri");
                                tc_msd_month.Style.Add("font-size", "10pt");
                                
                                tc_msd_month.Width = 200;
                                tc_msd_month.HorizontalAlign = HorizontalAlign.Center;
                                tc_msd_month.VerticalAlign = VerticalAlign.Middle;
                                tc_msd_month.Controls.Add(hyp_msd_month);
                                tr_det.Cells.Add(tc_msd_month);
                            }
                        }
                        TableCell tc_det_sf_tt = new TableCell();
                        //Literal lit_det_sf_tt = new Literal();
                        HyperLink lit_det_sf_tt = new HyperLink();
                        if (tot_met > 0)
                        {
                            lit_det_sf_tt.Text = tot_met.ToString();
                            lit_det_sf_tt.Attributes.Add("href", "javascript:showVisitDR_type('" + drFF["sf_code"].ToString() + "', '" + cmonth.ToString() + "', '" + cyear.ToString() + "', '" + ddlType.SelectedValue.ToString() + "','' )");
                        }
                        else
                        {
                            lit_det_sf_tt.Text = "-";
                        }

                        tc_det_sf_tt.BorderStyle = BorderStyle.Solid;
                        
                        tc_det_sf_tt.BorderWidth = 1;
                        tc_det_sf_tt.Style.Add("font-family", "Calibri");
                        tc_det_sf_tt.Style.Add("font-size", "10pt");
                        tc_det_sf_tt.HorizontalAlign = HorizontalAlign.Center;
                        tc_det_sf_tt.VerticalAlign = VerticalAlign.Middle;
                        tc_det_sf_tt.Controls.Add(lit_det_sf_tt);
                        tr_det.Cells.Add(tc_det_sf_tt);

                        //if(tot_met > 
                        tot_miss = tot_miss - tot_met;

                        TableCell tc_det_sf_tt_miss = new TableCell();
                        Literal lit_det_sf_tt_miss = new Literal();
                        lit_det_sf_tt_miss.Text = "&nbsp;" + tot_miss.ToString();
                        tc_det_sf_tt_miss.BorderStyle = BorderStyle.Solid;
                       
                        tc_det_sf_tt_miss.BorderWidth = 1;
                        tc_det_sf_tt_miss.Style.Add("font-family", "Calibri");
                        tc_det_sf_tt_miss.Style.Add("font-size", "10pt");
                        tc_det_sf_tt_miss.HorizontalAlign = HorizontalAlign.Center;
                        tc_det_sf_tt_miss.VerticalAlign = VerticalAlign.Middle;
                        tc_det_sf_tt_miss.Controls.Add(lit_det_sf_tt_miss);
                        tr_det.Cells.Add(tc_det_sf_tt_miss);

                        cmonth = cmonth + 1;
                        if (cmonth == 13)
                        {
                            cmonth = 1;
                            cyear = cyear + 1;
                        }
                    }
                }
                tbl.Rows.Add(tr_det);
                    
            }
        }
    }

    private void FillClass()
    {
        tbl.Rows.Clear();
        doctor_total = 0;

        SalesForce sf = new SalesForce();
        DCR dcf = new DCR();
        if (ddlMode.SelectedValue.ToString() == "1")
        {
            if (ddlMR.SelectedIndex > 0)
            {
                mode = 1;
                dsSalesForce = dcf.SF_Self_Report(div_code, ddlMR.SelectedValue.ToString());
            }
            else
            {
                mode = 1;
                dsSalesForce = dcf.SF_Self_Report(div_code, ddlFieldForce.SelectedValue.ToString());
            }
        }
        else
        {
            mode = 2;
            dsSalesForce = sf.SF_ReportingTo_TourPlan(div_code, ddlFieldForce.SelectedValue.ToString());
        }

        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            lblModelevel.Text = "Listed Doctor Class wise Visit Details report for " + ddlFMonth.SelectedItem.Text + " " + ddlFYear.SelectedValue + " to " + ddlTMonth.SelectedItem.Text + " " + ddlTYear.SelectedValue;
            TableRow tr_header = new TableRow();
            tr_header.BorderStyle = BorderStyle.Solid;
            tr_header.BorderWidth = 1;
            //tr_header.Attributes.Add("Class", "Backcolor");

            TableCell tc_SNo = new TableCell();
            tc_SNo.BorderStyle = BorderStyle.Solid;
            tc_SNo.BorderWidth = 1;            
            tc_SNo.Width = 50;
            tc_SNo.RowSpan = 3;
            Literal lit_SNo = new Literal();
            lit_SNo.Text = "S.No";
            tc_SNo.Attributes.Add("Class", "rptCellBorder");
            tc_SNo.Controls.Add(lit_SNo);
            tr_header.Cells.Add(tc_SNo);
          //  tr_header.BackColor = System.Drawing.Color.FromName("#A6A6D2");
            if (Session["sf_type"].ToString() == "1")
            {
                tr_header.Attributes.Add("Class", "MRBackColor");
            }
            else if (Session["sf_type"].ToString() == "2")
            {
                tr_header.Attributes.Add("Class", "MGRBackColor");
            }
            else
            {
                //tr_header.BackColor = System.Drawing.Color.FromName("#A6A6D2");
                tr_header.Attributes.Add("Class", "Backcolor");
            }

            TableCell tc_DR_Code = new TableCell();
            tc_DR_Code.BorderStyle = BorderStyle.Solid;
            tc_DR_Code.BorderWidth = 1;
            tc_DR_Code.Attributes.Add("Class", "rptCellBorder");
            tc_DR_Code.Width = 400;
            tc_DR_Code.RowSpan = 3;
            Literal lit_DR_Code = new Literal();
            lit_DR_Code.Text = "<center>SF Code</center>";
            tc_DR_Code.Controls.Add(lit_DR_Code);
            tc_DR_Code.Visible = false;
            tr_header.Cells.Add(tc_DR_Code);

            TableCell tc_DR_Name = new TableCell();
            tc_DR_Name.BorderStyle = BorderStyle.Solid;
            tc_DR_Name.BorderWidth = 1;
            //tc_DR_Name.Style.Add("font-family", "Calibri");
            //tc_DR_Name.Style.Add("font-size", "10pt");
            
            tc_DR_Name.Width = 500;
            tc_DR_Name.RowSpan = 3;
            Literal lit_DR_Name = new Literal();
            tc_DR_Name.Attributes.Add("Class", "rptCellBorder");
            lit_DR_Name.Text = "<center>Field&nbspForce&nbspName</center>";
            tc_DR_Name.Controls.Add(lit_DR_Name);
            tr_header.Cells.Add(tc_DR_Name);

            TableCell tc_DR_HQ = new TableCell();
            tc_DR_HQ.BorderStyle = BorderStyle.Solid;
            tc_DR_HQ.BorderWidth = 1;
            //tc_DR_HQ.Style.Add("font-family", "Calibri");
            //tc_DR_HQ.Style.Add("font-size", "10pt");
           
            tc_DR_HQ.Width = 500;
            tc_DR_HQ.RowSpan = 3;
            Literal lit_DR_HQ = new Literal();
            tc_DR_HQ.Attributes.Add("Class", "rptCellBorder");
            lit_DR_HQ.Text = "<center>HQ</center>";
            tc_DR_HQ.Controls.Add(lit_DR_HQ);
            tr_header.Cells.Add(tc_DR_HQ);

            TableCell tc_DR_Des = new TableCell();
            tc_DR_Des.BorderStyle = BorderStyle.Solid;
            tc_DR_Des.BorderWidth = 1;
            //tc_DR_Des.Style.Add("font-family", "Calibri");
            //tc_DR_Des.Style.Add("font-size", "10pt");
            
            tc_DR_Des.Width = 500;
            tc_DR_Des.RowSpan = 3;
            Literal lit_DR_Des = new Literal();
            tc_DR_Des.Attributes.Add("Class", "rptCellBorder");
            lit_DR_Des.Text = "<center>Designation</center>";
            tc_DR_Des.Controls.Add(lit_DR_Des);
            tr_header.Cells.Add(tc_DR_Des);

            int months = (Convert.ToInt32(ddlTYear.SelectedValue.ToString()) - Convert.ToInt32(ddlFYear.SelectedValue.ToString())) * 12 + Convert.ToInt32(ddlTMonth.SelectedValue.ToString()) - Convert.ToInt32(ddlFMonth.SelectedValue.ToString()); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
            int cmonth = Convert.ToInt32(ddlFMonth.SelectedValue.ToString());
            int cyear = Convert.ToInt32(ddlFYear.SelectedValue.ToString());

            ViewState["months"] = months;
            ViewState["cmonth"] = cmonth;
            ViewState["cyear"] = cyear;

            Doctor dr = new Doctor();
            dsDoctor = dr.getDocClass(div_code);

            if (months >= 0)
            {
                for (int j = 1; j <= months + 1; j++)
                {

                    TableCell tc_month = new TableCell();
                    tc_month.ColumnSpan = (dsDoctor.Tables[0].Rows.Count * 2) + 3;
                    //tc_month.RowSpan = 2;
                    Literal lit_month = new Literal();
                    lit_month.Text = "&nbsp;" + sf.getMonthName(cmonth.ToString()) + "-" + cyear;
                    tc_month.Attributes.Add("Class", "rptCellBorder");
                    tc_month.BorderStyle = BorderStyle.Solid;
                    tc_month.BorderWidth = 1;
                    
                    tc_month.HorizontalAlign = HorizontalAlign.Center;
                    //tc_month.Width = 200;
                    tc_month.Controls.Add(lit_month);
                    tr_header.Cells.Add(tc_month);
                    cmonth = cmonth + 1;
                    if (cmonth == 13)
                    {
                        cmonth = 1;
                        cyear = cyear + 1;
                    }
                }
            }

            tbl.Rows.Add(tr_header);

            //Sub Header
            months = Convert.ToInt16(ViewState["months"].ToString());
            cmonth = Convert.ToInt16(ViewState["cmonth"].ToString());
            cyear = Convert.ToInt16(ViewState["cyear"].ToString());


            if (months >= 0)
            {
                TableRow tr_lst_det = new TableRow();

               

                for (int j = 1; j <= months + 1; j++)
                {
                    TableCell tc_DR_Total_mon = new TableCell();
                    tc_DR_Total_mon.BorderStyle = BorderStyle.Solid;
                    tc_DR_Total_mon.BorderWidth = 1;
                   
                    tc_DR_Total_mon.Width = 300;
                    tc_DR_Total_mon.RowSpan = 2;
                    Literal lit_DR_Total_mon = new Literal();
                    lit_DR_Total_mon.Text = "<center>Total Drs List</center>";
                    tc_DR_Total_mon.Attributes.Add("Class", "rptCellBorder");
                    tc_DR_Total_mon.Controls.Add(lit_DR_Total_mon);
                    tr_lst_det.Cells.Add(tc_DR_Total_mon);

                    TableCell tc_lst_month = new TableCell();
                    HyperLink lit_lst_month = new HyperLink();
                    lit_lst_month.Text = "Lst.Drs";
                    tc_lst_month.Attributes.Add("Class", "rptCellBorder");

                    tc_lst_month.BorderStyle = BorderStyle.Solid;
                    tc_lst_month.HorizontalAlign = HorizontalAlign.Center;
                    tc_lst_month.BorderWidth = 1;  
                    tc_lst_month.ColumnSpan = dsDoctor.Tables[0].Rows.Count;
                    tc_lst_month.Controls.Add(lit_lst_month);
                    tr_lst_det.Cells.Add(tc_lst_month);

                    TableCell tc_msd_month = new TableCell();
                    HyperLink lit_msd_month = new HyperLink();
                    lit_msd_month.Text = "Met DR";
                    tc_msd_month.Attributes.Add("Class", "rptCellBorder");

                    tc_msd_month.BorderStyle = BorderStyle.Solid;
                    tc_msd_month.HorizontalAlign = HorizontalAlign.Center;
                    tc_msd_month.BorderWidth = 1;                    
                    tc_msd_month.ColumnSpan = dsDoctor.Tables[0].Rows.Count;
                    tc_msd_month.Controls.Add(lit_msd_month);
                    tr_lst_det.Cells.Add(tc_msd_month);

                    TableCell tc_DR_Total_mon_tot = new TableCell();
                    tc_DR_Total_mon_tot.BorderStyle = BorderStyle.Solid;
                    tc_DR_Total_mon_tot.BorderWidth = 1;
                    tc_DR_Total_mon_tot.Attributes.Add("Class", "rptCellBorder");
                    
                    tc_DR_Total_mon_tot.Width = 300;
                    tc_DR_Total_mon_tot.ColumnSpan = 2;
                    Literal lit_DR_Total_mon_tot = new Literal();
                    lit_DR_Total_mon_tot.Text = "<center>Total</center>";
                    tc_DR_Total_mon_tot.Controls.Add(lit_DR_Total_mon_tot);
                    tr_lst_det.Cells.Add(tc_DR_Total_mon_tot);

                    cmonth = cmonth + 1;
                    if (cmonth == 13)
                    {
                        cmonth = 1;
                        cyear = cyear + 1;
                    }
                }
               // tr_lst_det.BackColor = System.Drawing.Color.FromName("#A6A6D2");
                if (Session["sf_type"].ToString() == "1")
                {
                    tr_lst_det.Attributes.Add("Class", "MRBackColor");

                }
                else if (Session["sf_type"].ToString() == "2")
                {
                    tr_lst_det.Attributes.Add("Class", "MGRBackColor");
                }
                else
                {
                    //tr_lst_det.BackColor = System.Drawing.Color.FromName("#A6A6D2");
                    tr_lst_det.Attributes.Add("Class", "Backcolor");
                }
                tbl.Rows.Add(tr_lst_det);
            }

            if (months >= 0)
            {
                TableRow tr_catg = new TableRow();
                //tr_catg.BackColor = System.Drawing.Color.FromName("#A6A6D2");
                if (Session["sf_type"].ToString() == "1")
                {
                    tr_catg.Attributes.Add("Class", "MRBackColor");

                }
                else if (Session["sf_type"].ToString() == "2")
                {
                    tr_catg.Attributes.Add("Class", "MGRBackColor");
                }
                else
                {
                    //tr_catg.BackColor = System.Drawing.Color.FromName("#A6A6D2");
                    tr_catg.Attributes.Add("Class", "Backcolor");
                }

                for (int j = 1; j <= (months + 1) * 2; j++)
                {
                    if (dsDoctor.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dataRow in dsDoctor.Tables[0].Rows)
                        {
                            TableCell tc_catg_name = new TableCell();
                            tc_catg_name.BorderStyle = BorderStyle.Solid;
                            tc_catg_name.BorderWidth = 1;
                            //tc_catg_name.Style.Add("font-family", "Calibri");
                            //tc_catg_name.Style.Add("font-size", "10pt");
                            if ((j % 2) == 1)
                            {
                                //tc_catg_name.BackColor = System.Drawing.Color.Honeydew;
                            }
                            else
                            {
                                //tc_catg_name.BackColor = System.Drawing.Color.PapayaWhip;
                            }
                            tc_catg_name.Width = 30;
                            Literal lit_catg_name = new Literal();
                            lit_catg_name.Text = "<center>" + dataRow["Doc_Cat_Name"].ToString() + "</center>";
                            tc_catg_name.Attributes.Add("Class", "rptCellBorder");
                            tc_catg_name.Controls.Add(lit_catg_name);
                            tr_catg.Cells.Add(tc_catg_name);
                        }


                        if ((j % 2) == 1)
                        {
                        }
                        else
                        {
                            TableCell tc_catg_tot_met = new TableCell();
                            tc_catg_tot_met.BorderStyle = BorderStyle.Solid;
                            tc_catg_tot_met.BorderWidth = 1;
                           
                            tc_catg_tot_met.Width = 30;
                            Literal lit_catg_tot_met = new Literal();
                            lit_catg_tot_met.Text = "<center>Met</center>";
                            tc_catg_tot_met.Attributes.Add("Class", "rptCellBorder");
                            tc_catg_tot_met.Controls.Add(lit_catg_tot_met);
                            tr_catg.Cells.Add(tc_catg_tot_met);

                            TableCell tc_catg_tot_miss = new TableCell();
                            tc_catg_tot_miss.BorderStyle = BorderStyle.Solid;
                            tc_catg_tot_miss.BorderWidth = 1;
                            //tc_catg_tot_miss.Style.Add("font-family", "Calibri");
                            //tc_catg_tot_miss.Style.Add("font-size", "10pt");
                            //tc_catg_tot_miss.BackColor = System.Drawing.Color.LemonChiffon;
                            tc_catg_tot_miss.Width = 30;
                            Literal lit_catg_tot_miss = new Literal();
                            lit_catg_tot_miss.Text = "<center>Missed</center>";
                            tc_catg_tot_miss.Attributes.Add("Class", "rptCellBorder");
                            tc_catg_tot_miss.Controls.Add(lit_catg_tot_miss);
                            tr_catg.Cells.Add(tc_catg_tot_miss);
                        }

                        tbl.Rows.Add(tr_catg);
                    }
                }
            }

            // Details Section
            string sURL = string.Empty;
            int iCount = 0;
            int iCnt = 0;
            int tot_met = 0;
            int tot_miss = 0;

            foreach (DataRow drFF in dsSalesForce.Tables[0].Rows)
            {
                ListedDR lstDR = new ListedDR();
                iCnt = lstDR.RecordCount(drFF["sf_code"].ToString());

                TableRow tr_det = new TableRow();
                tr_det.Attributes.Add("Class", "tblCellFont");
                tr_det.BackColor = System.Drawing.Color.White;
                iCount += 1;
                TableCell tc_det_SNo = new TableCell();
                Literal lit_det_SNo = new Literal();
                lit_det_SNo.Text = "<center>" + iCount.ToString() + "</center>";
                tc_det_SNo.BorderStyle = BorderStyle.Solid;
                tc_det_SNo.BorderWidth = 1;
                
                tc_det_SNo.Width = 50;
                tc_det_SNo.Controls.Add(lit_det_SNo);
                tr_det.Cells.Add(tc_det_SNo);

                TableCell tc_det_doc_code = new TableCell();
                Literal lit_det_doc_code = new Literal();
                lit_det_doc_code.Text = "&nbsp;" + drFF["sf_code"].ToString();
                tc_det_doc_code.BorderStyle = BorderStyle.Solid;
                tc_det_doc_code.BorderWidth = 1;
               
                tc_det_doc_code.Controls.Add(lit_det_doc_code);
                tc_det_doc_code.Visible = false;
                tr_det.Cells.Add(tc_det_doc_code);

                TableCell tc_det_doc_name = new TableCell();
                //Literal lit_det_doc_name = new Literal();
                //lit_det_doc_name.Text = "&nbsp;" + drFF["SF_Name"].ToString();
                HyperLink lit_det_doc_name = new HyperLink();
                lit_det_doc_name.Text = "&nbsp;" + drFF["SF_Name"].ToString();
                if ((drFF["SF_Type"].ToString() == "2") && mode == 2)
                {

                    lit_det_doc_name.Attributes.Add("href", "javascript:showModalPopUp('" + drFF["sf_code"].ToString() + "', '" + ddlFMonth.SelectedValue.ToString() + "', '" + ddlFYear.SelectedValue.ToString() + "', '" + ddlTMonth.SelectedValue.ToString() + "', '" + ddlTYear.SelectedValue.ToString() + "', '" + ddlType.SelectedValue.ToString() + "')");
                }
                tc_det_doc_name.BorderStyle = BorderStyle.Solid;
                tc_det_doc_name.BorderWidth = 1;
               
                tc_det_doc_name.Width = 500;
                tc_det_doc_name.Controls.Add(lit_det_doc_name);
                tr_det.Cells.Add(tc_det_doc_name);

                TableCell tc_det_sf_HQ = new TableCell();
                Literal lit_det_sf_hq = new Literal();
                lit_det_sf_hq.Text = "&nbsp;" + drFF["Sf_HQ"].ToString();
                tc_det_sf_HQ.BorderStyle = BorderStyle.Solid;
                tc_det_sf_HQ.BorderWidth = 1;
               
                tc_det_sf_HQ.Controls.Add(lit_det_sf_hq);
                //tc_det_sf_HQ.Visible = false;
                tr_det.Cells.Add(tc_det_sf_HQ);


                TableCell tc_det_sf_des = new TableCell();
                Literal lit_det_sf_des = new Literal();
                lit_det_sf_des.Text = "&nbsp;" + drFF["Designation_Short_Name"].ToString();
                tc_det_sf_des.BorderStyle = BorderStyle.Solid;
                tc_det_sf_des.BorderWidth = 1;
                
                tc_det_sf_des.Controls.Add(lit_det_sf_des);
                //tc_det_sf_HQ.Visible = false;
                tr_det.Cells.Add(tc_det_sf_des);

                months = Convert.ToInt16(ViewState["months"].ToString());
                cmonth = Convert.ToInt16(ViewState["cmonth"].ToString());
                cyear = Convert.ToInt16(ViewState["cyear"].ToString());

                if (months >= 0)
                {
                    for (int j = 1; j <= months + 1; j++)
                    {
                        tot_met = 0;
                        tot_miss = 0;
                        doctor_total = 0;
                        if (cmonth == 12)
                        {
                            sCurrentDate = "01-01-" + (cyear + 1);
                        }
                        else
                        {
                            sCurrentDate = (cmonth + 1) + "-01-" + cyear;
                        }

                        dtCurrent = Convert.ToDateTime(sCurrentDate);

                        if (dsDoctor.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow dataRow in dsDoctor.Tables[0].Rows)
                            {
                                dsDoc = sf.Class_LST_Report(drFF["sf_code"].ToString(), div_code, dtCurrent, cmonth, cyear, Convert.ToInt32(dataRow["Doc_Cat_Code"].ToString()));

                                if (dsDoc.Tables[0].Rows.Count > 0)
                                    tot_dr = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

                                doctor_total = doctor_total + Convert.ToInt16(tot_dr);
                            }

                            TableCell tc_det_sf_tot = new TableCell();
                            Literal lit_det_sf_tot = new Literal();
                            lit_det_sf_tot.Text = "&nbsp;" + doctor_total.ToString();
                            tc_det_sf_tot.BorderStyle = BorderStyle.Solid;
                          
                            tc_det_sf_tot.BorderWidth = 1;                            
                            tc_det_sf_tot.HorizontalAlign = HorizontalAlign.Center;
                            tc_det_sf_tot.VerticalAlign = VerticalAlign.Middle;
                            tc_det_sf_tot.Controls.Add(lit_det_sf_tot);
                            tr_det.Cells.Add(tc_det_sf_tot);
                            
                            foreach (DataRow dataRow in dsDoctor.Tables[0].Rows)
                            {
                                dsDoc = sf.Class_LST_Report(drFF["sf_code"].ToString(), div_code, dtCurrent, cmonth, cyear, Convert.ToInt32(dataRow["Doc_Cat_Code"].ToString()));

                                if (dsDoc.Tables[0].Rows.Count > 0)
                                    tot_dr = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

                                TableCell tc_lst_month = new TableCell();
                                HyperLink hyp_lst_month = new HyperLink();
                                if (tot_dr != "0")
                                {

                                    tot_miss = tot_miss + Convert.ToInt16(tot_dr);
                                    hyp_lst_month.Text = tot_dr;
                                }
                                else
                                {
                                    hyp_lst_month.Text = "-";
                                }
                          
                                tc_lst_month.BorderStyle = BorderStyle.Solid;
                                tc_lst_month.BorderWidth = 1;
                               
                               
                                tc_lst_month.Width = 200;
                                tc_lst_month.HorizontalAlign = HorizontalAlign.Center;
                                tc_lst_month.VerticalAlign = VerticalAlign.Middle;
                                tc_lst_month.Controls.Add(hyp_lst_month);
                                tr_det.Cells.Add(tc_lst_month);
                            }
                        }

                        if (dsDoctor.Tables[0].Rows.Count > 0)
                        {
                            DCR dc = new DCR();
                            foreach (DataRow dataRow in dsDoctor.Tables[0].Rows)
                            {
                                dsDoc = sf.Class_LST_Report(drFF["sf_code"].ToString(), div_code, dtCurrent, cmonth, cyear, Convert.ToInt32(dataRow["Doc_Cat_Code"].ToString()));
                                if (dsDoc.Tables[0].Rows.Count > 0)
                                    tot_dr = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

                                dsDCR = dc.Class_Visit_Report1(drFF["sf_code"].ToString(), div_code, cmonth, cyear, Convert.ToInt32(dataRow["Doc_Cat_Code"].ToString()),mode);
                                if (dsDCR.Tables[0].Rows.Count > 0)
                                    tot_dcr_dr = dsDCR.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

                                TableCell tc_msd_month = new TableCell();
                                HyperLink hyp_msd_month = new HyperLink();
                                  //if (tot_dr != "0")
                                if (tot_dcr_dr != "0")
                                {
                                    //imissed_dr = Convert.ToInt16(tot_dr) - Convert.ToInt16(tot_dcr_dr);
                                    imissed_dr = Convert.ToInt16(tot_dcr_dr);
                                    hyp_msd_month.Text = imissed_dr.ToString();
                                    if (imissed_dr > 0)
                                    {
                                        tot_met = tot_met + imissed_dr;
                                        hyp_msd_month.Attributes.Add("href", "javascript:showVisitDR_type('" + drFF["sf_code"].ToString() + "', '" + cmonth.ToString() + "', '" + cyear.ToString() + "', '" + ddlType.SelectedValue.ToString() + "','" + Convert.ToInt32(dataRow["Doc_Cat_Code"].ToString()) + "')");
                                    }
                                    //    hyp_msd_month.Attributes.Add("href", "javascript:showModalPopUp('" + drFF["sf_code"].ToString() + "', '" + cmonth + "', '" + cyear + "')");
                                }
                                else
                                {
                                    hyp_msd_month.Text = "-";
                                }

                                tc_msd_month.BorderStyle = BorderStyle.Solid;
                                tc_msd_month.BorderWidth = 1;
                               
                                tc_msd_month.Width = 200;
                                tc_msd_month.HorizontalAlign = HorizontalAlign.Center;
                                tc_msd_month.VerticalAlign = VerticalAlign.Middle;
                                tc_msd_month.Controls.Add(hyp_msd_month);
                                tr_det.Cells.Add(tc_msd_month);
                            }
                        }

                        TableCell tc_det_sf_tt = new TableCell();
                        //Literal lit_det_sf_tt = new Literal();
                        HyperLink lit_det_sf_tt = new HyperLink();
                        if (tot_met > 0)
                        {
                            lit_det_sf_tt.Text = tot_met.ToString();
                            lit_det_sf_tt.Attributes.Add("href", "javascript:showVisitDR_type('" + drFF["sf_code"].ToString() + "', '" + cmonth.ToString() + "', '" + cyear.ToString() + "', '" + ddlType.SelectedValue.ToString() + "','' )");
                        }
                        else
                        {
                            lit_det_sf_tt.Text = "-";
                        }

                        tc_det_sf_tt.BorderStyle = BorderStyle.Solid;
                        
                        tc_det_sf_tt.BorderWidth = 1;                        
                        tc_det_sf_tt.HorizontalAlign = HorizontalAlign.Center;
                        tc_det_sf_tt.VerticalAlign = VerticalAlign.Middle;
                        tc_det_sf_tt.Controls.Add(lit_det_sf_tt);
                        tr_det.Cells.Add(tc_det_sf_tt);

                        //if(tot_met > 
                        tot_miss = tot_miss - tot_met;

                        TableCell tc_det_sf_tt_miss = new TableCell();
                        Literal lit_det_sf_tt_miss = new Literal();
                        lit_det_sf_tt_miss.Text = "&nbsp;" + tot_miss.ToString();
                        tc_det_sf_tt_miss.BorderStyle = BorderStyle.Solid;
                        
                        tc_det_sf_tt_miss.BorderWidth = 1;                        
                        tc_det_sf_tt_miss.HorizontalAlign = HorizontalAlign.Center;
                        tc_det_sf_tt_miss.VerticalAlign = VerticalAlign.Middle;
                        tc_det_sf_tt_miss.Controls.Add(lit_det_sf_tt_miss);
                        tr_det.Cells.Add(tc_det_sf_tt_miss);

                        cmonth = cmonth + 1;
                        if (cmonth == 13)
                        {
                            cmonth = 1;
                            cyear = cyear + 1;
                        }

                    }
                }

                tbl.Rows.Add(tr_det);

            }
        }
    }


}