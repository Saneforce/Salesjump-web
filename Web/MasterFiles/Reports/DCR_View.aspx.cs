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
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml;
using ClosedXML.Excel;
using System.Globalization;

public partial class Reports_DCR_View : System.Web.UI.Page
{
    DataSet dsSalesForce = null;
    DataSet dsdiv = new DataSet();
    DataSet dsTP = null;
    DataSet dsDivision = null;
    string strMultiDiv = string.Empty;
    string div_code = string.Empty;
    string sf_code = string.Empty;
    DataSet dsSf = null;
    string sf_type = string.Empty;
    string strMode = string.Empty;
    string Dis_code = string.Empty;
    string Dis_Name = string.Empty;
    string Dis_Sub = string.Empty;
    string sf_code1 = string.Empty;
    protected void Page_PreInit(object sender, EventArgs e)
    {
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
        div_code = Session["div_code"].ToString();
        sf_code = Session["sf_code"].ToString();
        sf_type = Session["sf_type"].ToString();
       
        if (Session["sf_type"].ToString() == "1")
        {
            //UserControl_MR_Menu c1 =
            //    (UserControl_MR_Menu)LoadControl("~/UserControl/MR_Menu.ascx");
            //Divid.Controls.Add(c1);
            //c1.FindControl("btnBack").Visible = false;
            //c1.Title = this.Page.Title;
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
            lblDivision.Visible = false;
            ddlDivision.Visible = false;
            //chkVacant.Visible = false;
        }

        else if (Session["sf_type"].ToString() == "2")
        {
            //UserControl_MGR_Menu c1 =
            //(UserControl_MGR_Menu)LoadControl("~/UserControl/MGR_Menu.ascx");
            //Divid.Controls.Add(c1);
            //c1.FindControl("btnBack").Visible = false;
            //c1.Title = this.Page.Title;
            chkVacant.Visible = false;
            ViewState["sf_type"] = "";
            if (!Page.IsPostBack)
            {
                DataSet dsmgrsf = new DataSet();
                SalesForce sf = new SalesForce();
                DataSet DsAudit = sf.SF_Hierarchy(div_code, sf_code);
                if (DsAudit.Tables[0].Rows.Count > 0)
                {
                    FillMRManagers();
                    ddlFieldForce.SelectedValue = sf_code;
                }
                else
                {
                    DataTable dt = sf.getAuditManagerTeam(div_code, sf_code, 0);
                    dsmgrsf.Tables.Add(dt);
                    dsTP = dsmgrsf;

                    ddlFieldForce.DataTextField = "sf_name";
                    ddlFieldForce.DataValueField = "sf_code";
                    ddlFieldForce.DataSource = dsTP;
                    ddlFieldForce.DataBind();

                    ddlSF.DataTextField = "desig_Color";
                    ddlSF.DataValueField = "sf_code";
                    ddlSF.DataSource = dsTP;
                    ddlSF.DataBind();
                    ddlFFType.Visible = false;
                }
            }
            //chkVacant.Visible = false;
            // lblDivision.Visible = false;
            // ddlDivision.Visible = false;
        }
        else
        {
            ViewState["sf_type"] = "admin";
            //UserControl_MenuUserControl c1 =
            //(UserControl_MenuUserControl)LoadControl("~/UserControl/MenuUserControl.ascx");
            //Divid.Controls.Add(c1);
            //c1.FindControl("btnBack").Visible = false;
            //c1.Title = this.Page.Title;
            if (!Page.IsPostBack)
            {
                Filldiv();
                //chkVacant.Visible = true;
                FillManagers();
            }

            if (Session["div_code"] != null)
            {
                lblDivision.Visible = false;
                ddlDivision.Visible = false;
            }
            chkVacant.Visible = true;
        }

        if (!Page.IsPostBack)
        {
            ddlDivision.SelectedValue = div_code;
            // menu1.FindControl("btnBack").Visible = false;
            TourPlan tp = new TourPlan();
            DataSet dsTP = new DataSet();
            dsTP = tp.Get_TP_Edit_Year(div_code);
            if (dsTP.Tables[0].Rows.Count > 0)
            {
                for (int k = Convert.ToInt16(dsTP.Tables[0].Rows[0]["Year"]); k <= DateTime.Now.Year + 1; k++)
                {
                    ddlYear.Items.Add(k.ToString());
                    ddlYear.SelectedValue = DateTime.Now.Year.ToString();
                }
            }
            ddlMonth.SelectedValue = DateTime.Now.Month.ToString();

            Product prd = new Product();
            dsdiv = prd.getMultiDivsf_Name(sf_code);
            if (dsdiv.Tables[0].Rows.Count > 0)
            {
                if (dsdiv.Tables[0].Rows[0]["IsMultiDivision"].ToString() == "1")
                {
                    strMultiDiv = dsdiv.Tables[0].Rows[0][1].ToString().Remove(dsdiv.Tables[0].Rows[0][1].ToString().Length - 1, 1);
                    ddlDivision.Visible = true;
                    lblDivision.Visible = true;
                    getDivision();
                }
                else
                {
                    ddlDivision.Visible = false;
                    lblDivision.Visible = false;
                }
            }

            Label2.Visible = false;
            Label1.Visible = false;
            ddlYear.Visible = false;
            ddlMonth.Visible = false;
            Lbl3_Form.Visible = true;
            Lbl4_To.Visible = true;
            TextBox1.Visible = true;
            TextBox2.Visible = true;

        }
        //FillColor();
    }

    private void FillMRManagers()
    {
        SalesForce sf = new SalesForce();
        ddlFFType.Visible = false;
        ddlAlpha.Visible = false;
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
    private void Filldiv()
    {
        Division dv = new Division();
        dsDivision = dv.getDivision();
        if (dsDivision.Tables[0].Rows.Count > 0)
        {
            ddlDivision.DataTextField = "Division_Name";
            ddlDivision.DataValueField = "Division_Code";
            ddlDivision.DataSource = dsDivision;
            ddlDivision.DataBind();
        }
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
    protected void ddlDivision_SelectedIndexChanged(object sender, EventArgs e)
    {
        div_code = ddlDivision.SelectedValue.ToString();
        FillMRManagers();
        //FillColor();
        Lbl3_Form.Visible = true;
        TextBox1.Visible = true;
        Lbl4_To.Visible = false;
        TextBox2.Visible = false;
    }
    protected void ddlFFType_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillManagers();
        //FillColor();
    }

    protected void chkVacant_CheckedChanged(object sender, EventArgs e)
    {
        if (chkVacant.Checked == true)
        {
            lblMR.Visible = false;
            ddlMR.Visible = false;
            FillManagers();
        }
        else
        {
            if (sf_type == "2")
            {
                FillMRManagers();
            }
            else
            {
                FillManagers();
            }

        }
        //FillColor();
    }


    private void FillManagers()
    {
        SalesForce sf = new SalesForce();

        if (ddlFFType.SelectedValue.ToString() == "1")
        {
            try
            {
                ddlAlpha.Visible = false;

                if (Session["sf_type"].ToString() == "2")
                { dsSalesForce = sf.UserList_Hierarchy(div_code, sf_code); }
                else { dsSalesForce = sf.UserList_Hierarchy(div_code, "admin"); }
            }
            catch (Exception)
            {
                //if (Session["sf_type"].ToString() == "2")
                //{ dsSalesForce = sf.UserList_Hierarchy(div_code, sf_code); }
                //else { dsSalesForce = sf.UserList_Hierarchy(div_code, "admin"); }

                dsSalesForce = sf.UserList_Hierarchy(div_code, sf_code);
            }
        }
        else if (ddlFFType.SelectedValue.ToString() == "0")
        {
            FillSF_Alpha();
            ddlAlpha.Visible = true;

            if (Session["sf_type"].ToString() == "2")
            { dsSalesForce = sf.UserList_Alpha(div_code, sf_code); }
            else { dsSalesForce = sf.UserList_Alpha(div_code, "admin"); }

            //dsSalesForce = sf.UserList_Alpha(div_code, "admin");
        }
        else if (ddlFFType.SelectedValue.ToString() == "2")
        {
            if (Session["sf_type"].ToString() == "2")
            { dsSalesForce = sf.UserList_HQ(div_code, sf_code); }
            else { dsSalesForce = sf.UserList_HQ(div_code, "admin"); }


            //dsSalesForce = sf.UserList_HQ(div_code, "admin");
        }

        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            ddlFieldForce.DataTextField = "sf_name";
            ddlFieldForce.DataValueField = "sf_code";
            ddlFieldForce.DataSource = dsSalesForce;
            ddlFieldForce.DataBind();
            //ddlFieldForce.Items.RemoveAt(0);
            ddlFieldForce.Items.Insert(0, new ListItem("---Select---", "0"));
            //ddlSF.DataTextField = "des_color";
            //ddlSF.DataValueField = "sf_code";
            //ddlSF.DataSource = dsSalesForce;
            //ddlSF.DataBind();

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

    protected void ddlFieldForce_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rbnList.SelectedIndex == 3 || rbnList.SelectedIndex == 6)
        {
            lblMR.Visible = false;
            ddlMR.Visible = false;
        }
        else
        {
            if (ViewState["sf_type"].ToString() == "admin")
            {
                SalesForce sf = new SalesForce();
                dsSalesForce = sf.UserList_getMR(div_code, ddlFieldForce.SelectedValue.ToString());
                if (dsSalesForce.Tables[0].Rows.Count > 0)
                {
                    lblMR.Visible = true;
                    ddlMR.Visible = true;
                    //Lbl3_Form.Visible = true;
                    //TextBox1.Visible = true;
                    //Lbl4_To.Visible = false;
                    //TextBox2.Visible = false;
                    ddlMR.DataTextField = "sf_name";
                    ddlMR.DataValueField = "sf_code";
                    ddlMR.DataSource = dsSalesForce;
                    ddlMR.DataBind();
                    ddlMR.Items.Insert(0, new ListItem("---Select---", "0"));

                }
            }
        }
    }


    protected void ddlAlpha_SelectedIndexChanged(object sender, EventArgs e)
    {
        SalesForce sf = new SalesForce();
        dsSalesForce = sf.UserList_AlphaAll(ddlDivision.SelectedValue.ToString(), "admin", ddlAlpha.SelectedValue);
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

    protected void btnSubmit_Click1(object sender, EventArgs e)
    {

        SalesForce sf = new SalesForce();
        dsSf = sf.CheckSFType(ddlFieldForce.SelectedValue.ToString());

        if (dsSf.Tables[0].Rows.Count > 0)
        {
            if (ViewState["sf_type"].ToString() != "admin")
                sf_type = dsSf.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
        }

        //if (chkVacant.Checked == true)
        //{
        //    if (rbnList.SelectedItem.Text.Trim() == "Sales Return")
        //    {
        //        string sURL = "rptSalesReturnNew.aspx?sf_code=" + ddlFieldForce.SelectedValue + "&div_code=" + div_code + "&cur_month=" + ddlMonth.SelectedValue + "&cur_year=" + ddlYear.SelectedItem.Text +
        //                       "&Mode=" + rbnList.SelectedItem.Value + "&Sf_Name=" + ddlFieldForce.SelectedItem.Text + "&FDate=" + TextBox1.Text + "&TDate=" + TextBox2.Text + "&Chk=" + "1";
        //        string newWin = "window.open('" + sURL + "',null,'resizable=yes,toolbar=no,scrollbars=yes,menubar=no,status=no,width=800,height=600,left=0,top=0');";
        //        ClientScript.RegisterStartupScript(this.GetType(), "pop", newWin, true);
        //    }
        //    else
        //    {
        //        string sURL = "Rpt_DCR_View.aspx?sf_code=" + ddlFieldForce.SelectedValue + "&div_code=" + div_code + "&cur_month=" + ddlMonth.SelectedValue + "&cur_year=" + ddlYear.SelectedItem.Text +
        //                       "&Mode=" + rbnList.SelectedItem.Value + "&Sf_Name=" + ddlFieldForce.SelectedItem.Text + "&FDate=" + TextBox1.Text + "&TDate=" + TextBox2.Text + "&Chk=" + "1";
        //        string newWin = "window.open('" + sURL + "',null,'resizable=yes,toolbar=no,scrollbars=yes,menubar=no,status=no,width=800,height=600,left=0,top=0');";
        //        ClientScript.RegisterStartupScript(this.GetType(), "pop", newWin, true);
        //    }
        //}
        //else
        //{
        if (rbnList.SelectedItem.Text.Trim() == "Closing Stock View")
        {
            if (ddlMR.SelectedIndex != -1 && ddlMR.SelectedIndex != 0)
            {
                string sURL = "rptClosingStock.aspx?sf_code=" + ddlMR.SelectedValue + "&div_code=" + div_code + "&cur_month=" + ddlMonth.SelectedValue + "&cur_year=" + ddlYear.SelectedItem.Text +
                       "&Mode=" + rbnList.SelectedItem.Text + "&Sf_Name=" + ddlMR.SelectedItem.Text + "&FDate=" + TextBox1.Text + "&TDate=" + TextBox2.Text;
                string newWin = "window.open('" + sURL + "',null,'resizable=yes,toolbar=no,scrollbars=yes,menubar=no,status=no,width=800,height=600,left=0,top=0');";
                ClientScript.RegisterStartupScript(this.GetType(), "pop", newWin, true);
            }
            else
            {
                string sURL = "rptClosingStock.aspx?sf_code=" + ddlFieldForce.SelectedValue + "&div_code=" + div_code + "&cur_month=" + ddlMonth.SelectedValue + "&cur_year=" + ddlYear.SelectedItem.Text +
                      "&Mode=" + rbnList.SelectedItem.Text + "&Sf_Name=" + ddlFieldForce.SelectedItem.Text + "&FDate=" + TextBox1.Text + "&TDate=" + TextBox2.Text;
                string newWin = "window.open('" + sURL + "',null,'resizable=yes,toolbar=no,scrollbars=yes,menubar=no,status=no,width=800,height=600,left=0,top=0');";
                ClientScript.RegisterStartupScript(this.GetType(), "pop", newWin, true);
            }
        }
        else if (rbnList.SelectedItem.Text == "Primary Vs Secondary")
        {
            if (ddlMR.SelectedIndex != -1 && ddlMR.SelectedIndex != 0)
            {
                string sURL = "rptPrimvsSec.aspx?sf_code=" + ddlMR.SelectedValue + "&div_code=" + div_code + "&cur_month=" + ddlMonth.SelectedValue + "&cur_year=" + ddlYear.SelectedItem.Text +
                        "&Mode=" + rbnList.SelectedItem.Text + "&Sf_Name=" + ddlMR.SelectedItem.Text + "&FDate=" + TextBox1.Text + "&TDate=" + TextBox2.Text;
                string newWin = "window.open('" + sURL + "',null,'resizable=yes,toolbar=no,scrollbars=yes,menubar=no,status=no,width=800,height=600,left=0,top=0');";
                ClientScript.RegisterStartupScript(this.GetType(), "pop", newWin, true);
            }
            else
            {
                string sURL = "rptPrimvsSec.aspx?sf_code=" + ddlFieldForce.SelectedValue + "&div_code=" + div_code + "&cur_month=" + ddlMonth.SelectedValue + "&cur_year=" + ddlYear.SelectedItem.Text +
                         "&Mode=" + rbnList.SelectedItem.Text + "&Sf_Name=" + ddlFieldForce.SelectedItem.Text + "&FDate=" + TextBox1.Text + "&TDate=" + TextBox2.Text;
                string newWin = "window.open('" + sURL + "',null,'resizable=yes,toolbar=no,scrollbars=yes,menubar=no,status=no,width=800,height=600,left=0,top=0');";
                ClientScript.RegisterStartupScript(this.GetType(), "pop", newWin, true);
            }
        }
        else if (rbnList.SelectedItem.Text.Trim() == "Sales Return")
        {
            if (ddlMR.SelectedIndex != -1 && ddlMR.SelectedIndex != 0)
            {
                string sURL = "rptSalesReturnNew.aspx?sf_code=" + ddlMR.SelectedValue + "&div_code=" + div_code + "&cur_month=" + ddlMonth.SelectedValue + "&cur_year=" + ddlYear.SelectedItem.Text +
                               "&Mode=" + rbnList.SelectedItem.Value + "&Sf_Name=" + ddlMR.SelectedItem.Text + "&FDate=" + TextBox1.Text + "&TDate=" + TextBox2.Text + "&Chk=" + "1";
                string newWin = "window.open('" + sURL + "',null,'resizable=no,toolbar=no,scrollbars=yes,menubar=no,status=no,width=1200,height=600,left=0,top=0');";
                ClientScript.RegisterStartupScript(this.GetType(), "pop", newWin, true);
            }
            else
            {
                string sURL = "rptSalesReturnNew.aspx?sf_code=" + ddlFieldForce.SelectedValue + "&div_code=" + div_code + "&cur_month=" + ddlMonth.SelectedValue + "&cur_year=" + ddlYear.SelectedItem.Text +
                              "&Mode=" + rbnList.SelectedItem.Value + "&Sf_Name=" + ddlFieldForce.SelectedItem.Text + "&FDate=" + TextBox1.Text + "&TDate=" + TextBox2.Text + "&Chk=" + "1";
                string newWin = "window.open('" + sURL + "',null,'resizable=no,toolbar=no,scrollbars=yes,menubar=no,status=no,width=1200,height=600,left=0,top=0');";
                ClientScript.RegisterStartupScript(this.GetType(), "pop", newWin, true);
            }
        }
        else
        {
             if (rbnList.SelectedItem.Value == "View_All_DCR_Date(s)")
            {
                if (chkVacant.Checked == true)
                {
                    string sURL = "viewalldate_dcr.aspx?sf_code=" + ddlFieldForce.SelectedValue + "&div_code=" + div_code + "&cur_month=" + ddlMonth.SelectedValue + "&cur_year=" + ddlYear.SelectedItem.Text +
                                "&Mode=" + rbnList.SelectedItem.Value + "&Sf_Name=" + ddlFieldForce.SelectedItem.Text + "&FDate=" + TextBox1.Text + "&TDate=" + TextBox2.Text;
                    string newWin = "window.open('" + sURL + "',null,'resizable=yes,toolbar=no,scrollbars=yes,menubar=no,status=no,width=800,height=600,left=0,top=0');";
                    ClientScript.RegisterStartupScript(this.GetType(), "pop", newWin, true);
                }
                else
                {


                    if (ddlMR.SelectedIndex != -1 && ddlMR.SelectedIndex != 0)
                    {
                        string sURL = "viewaldate_dcr.aspx?sf_code=" + ddlMR.SelectedValue + "&div_code=" + div_code + "&cur_month=" + ddlMonth.SelectedValue + "&cur_year=" + ddlYear.SelectedItem.Text +
                                "&Mode=" + rbnList.SelectedItem.Value + "&Sf_Name=" + ddlMR.SelectedItem.Text + "&FDate=" + TextBox1.Text + "&TDate=" + TextBox2.Text;
                        string newWin = "window.open('" + sURL + "',null,'resizable=yes,toolbar=no,scrollbars=yes,menubar=no,status=no,width=800,height=600,left=0,top=0');";
                        ClientScript.RegisterStartupScript(this.GetType(), "pop", newWin, true);
                    }

                    else
                    {
                        string sURL = "viewaldate_dcr.aspx?sf_code=" + ddlFieldForce.SelectedValue + "&div_code=" + div_code + "&cur_month=" + ddlMonth.SelectedValue + "&cur_year=" + ddlYear.SelectedItem.Text +
                                "&Mode=" + rbnList.SelectedItem.Value + "&Sf_Name=" + ddlFieldForce.SelectedItem.Text + "&FDate=" + TextBox1.Text + "&TDate=" + TextBox2.Text;
                        string newWin = "window.open('" + sURL + "',null,'resizable=yes,toolbar=no,scrollbars=yes,menubar=no,status=no,width=800,height=600,left=0,top=0');";
                        ClientScript.RegisterStartupScript(this.GetType(), "pop", newWin, true);

                    }
                }
            }
            else if (rbnList.SelectedItem.Value.ToString().Trim() == "Detailed View")
            {
                if (chkVacant.Checked == true)
                {
                    string sURL = "DCR.aspx?sf_code=" + ddlFieldForce.SelectedValue + "&div_code=" + div_code + "&cur_month=" + ddlMonth.SelectedValue + "&cur_year=" + ddlYear.SelectedItem.Text +
                            "&Mode=" + rbnList.SelectedItem.Value + "&Sf_Name=" + ddlFieldForce.SelectedItem.Text + "&FDate=" + TextBox1.Text + "&TDate=" + TextBox2.Text + "&typemod=" + 1;
                    string newWin = "window.open('" + sURL + "',null,'resizable=yes,toolbar=no,scrollbars=yes,menubar=no,status=no,width=800,height=600,left=0,top=0');";
                    ClientScript.RegisterStartupScript(this.GetType(), "pop", newWin, true);
                }
                else
                {                    
                    if (ddlMR.SelectedIndex != -1 && ddlMR.SelectedIndex != 0)
                    {
                        string sURL = "DCR.aspx?sf_code=" + ddlMR.SelectedValue + "&div_code=" + div_code + "&cur_month=" + ddlMonth.SelectedValue + "&cur_year=" + ddlYear.SelectedItem.Text +
                                "&Mode=" + rbnList.SelectedItem.Value + "&Sf_Name=" + ddlMR.SelectedItem.Text + "&FDate=" + TextBox1.Text + "&TDate=" + TextBox2.Text + "&typemod=" + 0;
                        string newWin = "window.open('" + sURL + "',null,'resizable=yes,toolbar=no,scrollbars=yes,menubar=no,status=no,width=800,height=600,left=0,top=0');";
                        ClientScript.RegisterStartupScript(this.GetType(), "pop", newWin, true);
                    }
                    else
                    {
                        string sURL = "DCR.aspx?sf_code=" + ddlFieldForce.SelectedValue + "&div_code=" + div_code + "&cur_month=" + ddlMonth.SelectedValue + "&cur_year=" + ddlYear.SelectedItem.Text +
                                "&Mode=" + rbnList.SelectedItem.Value + "&Sf_Name=" + ddlFieldForce.SelectedItem.Text + "&FDate=" + TextBox1.Text + "&TDate=" + TextBox2.Text + "&typemod=" + 0;
                        string newWin = "window.open('" + sURL + "',null,'resizable=yes,toolbar=no,scrollbars=yes,menubar=no,status=no,width=800,height=600,left=0,top=0');";
                        ClientScript.RegisterStartupScript(this.GetType(), "pop", newWin, true);

                    }
                }
            }
            else
            {
                if (ddlMR.SelectedIndex != -1 && ddlMR.SelectedIndex != 0)
                {
                    string sURL = "Rpt_DCR_View.aspx?sf_code=" + ddlMR.SelectedValue + "&div_code=" + div_code + "&cur_month=" + ddlMonth.SelectedValue + "&cur_year=" + ddlYear.SelectedItem.Text +
                            "&Mode=" + rbnList.SelectedItem.Value + "&Sf_Name=" + ddlMR.SelectedItem.Text + "&FDate=" + TextBox1.Text + "&TDate=" + TextBox2.Text;
                    string newWin = "window.open('" + sURL + "',null,'resizable=yes,toolbar=no,scrollbars=yes,menubar=no,status=no,width=800,height=600,left=0,top=0');";
                    ClientScript.RegisterStartupScript(this.GetType(), "pop", newWin, true);
                }

                else
                {
                    string sURL = "Rpt_DCR_View.aspx?sf_code=" + ddlFieldForce.SelectedValue + "&div_code=" + div_code + "&cur_month=" + ddlMonth.SelectedValue + "&cur_year=" + ddlYear.SelectedItem.Text +
                            "&Mode=" + rbnList.SelectedItem.Value + "&Sf_Name=" + ddlFieldForce.SelectedItem.Text + "&FDate=" + TextBox1.Text + "&TDate=" + TextBox2.Text;
                    string newWin = "window.open('" + sURL + "',null,'resizable=yes,toolbar=no,scrollbars=yes,menubar=no,status=no,width=800,height=600,left=0,top=0');";
                    ClientScript.RegisterStartupScript(this.GetType(), "pop", newWin, true);

                }
            }
        }




    }
    private void Resetall()
    {
        TextBox1.Text = "";
        TextBox2.Text = "";
        ddlFieldForce.SelectedIndex = 0;
        ddlMR.SelectedIndex = -1;
    }
    protected void rbnList_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rbnList.SelectedIndex == 0 || rbnList.SelectedIndex == 2 || rbnList.SelectedIndex == 5 || rbnList.SelectedIndex == 7 || rbnList.SelectedIndex == 8)
        {
            Label2.Visible = false;
            Label1.Visible = false;
            ddlYear.Visible = false;
            ddlMonth.Visible = false;
            Lbl3_Form.Visible = true;
            Lbl4_To.Visible = true;
            TextBox1.Visible = true;
            TextBox2.Visible = true;
            Resetall();

        }
        else if (rbnList.SelectedIndex == 1)
        {
            Label2.Visible = false;
            Label1.Visible = false;
            ddlYear.Visible = false;
            ddlMonth.Visible = false;
            Lbl3_Form.Visible = true;
            Lbl4_To.Visible = true;
            TextBox1.Visible = true;
            TextBox2.Visible = true;
            Resetall();

        }
        else if (rbnList.SelectedIndex == 4)
        {
            Label2.Visible = true;
            Label1.Visible = true;
            ddlYear.Visible = true;
            ddlMonth.Visible = true;
            Lbl3_Form.Visible = false;
            Lbl4_To.Visible = false;
            TextBox1.Visible = false;
            TextBox2.Visible = false;
            Resetall();

        }
        else if (rbnList.SelectedIndex == 3 || rbnList.SelectedIndex == 6)
        {
            lblMR.Visible = false;
            ddlMR.Visible = false;
            Label2.Visible = false;
            Label1.Visible = false;
            ddlYear.Visible = false;
            ddlMonth.Visible = false;
            Lbl3_Form.Visible = true;
            Lbl4_To.Visible = true;
            TextBox1.Visible = true;
            TextBox2.Visible = true;
            Resetall();

        }
    }
    protected void ddlMR_SelectedIndexChanged(object sender, EventArgs e)
    {
        //Lbl3_Form.Visible = true;
        //TextBox1.Visible = true;
        //Lbl4_To.Visible = true;
        //TextBox2.Visible = true;
    }


    public void ExportData()
    {
        //Exporting to Excel
        string divcode = div_code.ToString();

        string Sf_Code = "";

        Sf_Code = ddlMR.SelectedValue.ToString();

        if (Sf_Code == "-1" || Sf_Code == "0" || Sf_Code == "")
        {
            Sf_Code = ddlFieldForce.SelectedValue.ToString();
        }


        string FDate = TextBox1.Text.ToString();
        string TDate = TextBox2.Text.ToString();
        string subdiv_code = "0";
        string statecode = "0";

        string[] FDate1 = FDate.Split('/');
        string[] TDate1 = TDate.Split('/');

        FDate = Convert.ToString(FDate1[2] + "-" + FDate1[1] + "-" + FDate1[0]);
        TDate = Convert.ToString(TDate1[2] + "-" + TDate1[1] + "-" + TDate1[0]);

        DataTable ot = generateSecondaryExcel(div_code, Sf_Code, FDate, TDate, subdiv_code, statecode);
        DataTable ot1 = generateSecondaryExcel1(div_code, Sf_Code, FDate, TDate, subdiv_code, statecode);

        DataSet ds = new DataSet();

        ds.Tables.Add(ot);
        ds.Tables.Add(ot1);

        string filename = System.IO.Path.GetTempPath() + "Daily_Call_Report_" + div_code + "_" + (System.DateTime.Now.ToString()).Replace(":", "_").Replace("/", "_").Replace(" ", "_") + ".xls";
        if (File.Exists(filename))
        {
            filename = System.IO.Path.GetTempPath() + "Daily_Call_Report_" + div_code + "_" + (System.DateTime.Now.ToString()).Replace(":", "_").Replace("/", "_").Replace(" ", "_") + ".xls";
        }

        string attachment = "attachment; filename=" + filename;

        Response.ClearContent();
        Response.AddHeader("content-disposition", attachment);
        Response.ContentType = "application/vnd.ms-excel";       
        
        
        using (System.IO.StringWriter sw = new System.IO.StringWriter())
        {
            using (System.Web.UI.HtmlTextWriter htw = new System.Web.UI.HtmlTextWriter(sw))
            {

                if(ot.Rows.Count > 0)
                {

                    GridView grid = new GridView();
                    grid.DataSource = ot;
                    grid.DataBind();
                    grid.RenderControl(htw);


                }

                htw.RenderBeginTag(HtmlTextWriterTag.Label);
                htw.WriteLine("Distributor Details {0}",
                    CultureInfo.CurrentCulture);
                htw.RenderEndTag();
               

                if (ot1.Rows.Count > 0)
                {
                    GridView grid = new GridView();
                    grid.DataSource = ot1;
                    grid.DataBind();
                    grid.RenderControl(htw);
                }

                Response.Write(sw.ToString());
            }
        }

        Response.End();


        //Codes for the Closed XML
        //using (XLWorkbook wb = new XLWorkbook())
        //{
        //    if(ot.Rows.Count>1)
        //    {
        //        var worksheet = wb.Worksheets.Add(ot);
        //        worksheet.Cell(1, 1).InsertTable(ot);
        //        worksheet.Columns().AdjustToContents();
        //    }


        //    if (ot1.Rows.Count > 1)
        //    {
        //        var worksheet = wb.Worksheets.Add(ot1);
        //        worksheet.Cell(1, 1).InsertTable(ot1);
        //        worksheet.Columns().AdjustToContents();
        //    }


        //    //foreach (DataTable dt in ds.Tables)
        //    //{
        //    //    var worksheet = wb.Worksheets.Add(dt.TableName);
        //    //    worksheet.Cell(1, 1).InsertTable(dt);
        //    //    worksheet.Columns().AdjustToContents();
        //    //}

        //    //wb.SaveAs(folderPath + "DataGridViewExport.xlsx");
        //    //string myName = Server.UrlEncode("Test" + "_" + DateTime.Now.ToShortDateString() + ".xlsx");
        //    MemoryStream stream = GetStream(wb);// The method is defined below
        //    Response.Clear();
        //    Response.Buffer = true;
        //    Response.AddHeader("content-disposition", attachment);
        //    Response.ContentType = "application/vnd.ms-excel";
        //    Response.BinaryWrite(stream.ToArray());
        //    Response.End();
        //}





    }
	
	
    public MemoryStream GetStream(XLWorkbook excelWorkbook)
    {
        MemoryStream fs = new MemoryStream();
        excelWorkbook.SaveAs(fs);
        fs.Position = 0;
        return fs;
    }



    protected void exceldld_Click(object sender, EventArgs e)
    {

        //string divcode = div_code.ToString();

        //string Sf_Code = "";

        //Sf_Code = ddlMR.SelectedValue.ToString();

        //if (Sf_Code == "-1" || Sf_Code == "0" || Sf_Code == "")
        //{
        //    Sf_Code = ddlFieldForce.SelectedValue.ToString();
        //}


        //string FDate = TextBox1.Text.ToString();
        //string TDate = TextBox2.Text.ToString();
        //string subdiv_code = "0";
        //string statecode = "0";

        //string[] FDate1 = FDate.Split('/');
        //string[] TDate1 = TDate.Split('/');

        //FDate = Convert.ToString(FDate1[2] + "-" + FDate1[1] + "-" + FDate1[0]);
        //TDate = Convert.ToString(TDate1[2] + "-" + TDate1[1] + "-" + TDate1[0]);

        //DataTable ot = generateSecondaryExcel(div_code, Sf_Code, FDate,  TDate, subdiv_code, statecode);
        //DataTable ot1 = generateSecondaryExcel1(div_code, Sf_Code, FDate, TDate, subdiv_code, statecode);
        //DataSet ds = new DataSet();

        //ds.Tables.Add(ot);
        //ds.Tables.Add(ot1);

        //string filename = System.IO.Path.GetTempPath() + "Daily_Call_Report_" + div_code + "_" + (System.DateTime.Now.ToString()).Replace(":", "_").Replace("/", "_").Replace(" ", "_") + ".xlsx";
        //if (File.Exists(filename))
        //{
        //    filename = System.IO.Path.GetTempPath() + "Daily_Call_Report_" + div_code + "_" + (System.DateTime.Now.ToString()).Replace(":", "_").Replace("/", "_").Replace(" ", "_") + ".xlsx";
        //}
        //uint sheetId = 1; 
        //SpreadsheetDocument spreadsheetDocument = SpreadsheetDocument.Create(filename, SpreadsheetDocumentType.Workbook);
        //WorkbookPart workbookpart = spreadsheetDocument.AddWorkbookPart();
        //workbookpart.Workbook = new Workbook();
        //var worksheetPart = workbookpart.AddNewPart<WorksheetPart>();
        //var sheetData = new SheetData();
        //worksheetPart.Worksheet = new Worksheet(sheetData);
        //Sheets sheets;
        //sheets = spreadsheetDocument.WorkbookPart.Workbook.AppendChild<Sheets>(new Sheets());

        //var sheet = new Sheet()
        //{
        //    Id = spreadsheetDocument.WorkbookPart.
        //               GetIdOfPart(worksheetPart),
        //    SheetId = sheetId,
        //    Name = "Sheet" + sheetId
        //};
        //sheets.Append(sheet);
        //var headerRow = new Row();
        //foreach (DataColumn column in ot.Columns)
        //{
        //    var cell = new Cell { DataType = CellValues.String, CellValue = new CellValue(column.ColumnName) };
        //    headerRow.AppendChild(cell);
        //}
        //sheetData.AppendChild(headerRow);
        //foreach (DataRow row in ot.Rows)
        //{
        //    var newRow = new Row();
        //    foreach (DataColumn col in ot.Columns)
        //    {
        //        var cell = new Cell
        //        {
        //            DataType = CellValues.String,
        //            CellValue = new CellValue(row[col].ToString())
        //        };
        //        newRow.AppendChild(cell);
        //    }

        //    sheetData.AppendChild(newRow);
        //}



        //var sheet1 = new Sheet()
        //{
        //    Id = spreadsheetDocument.WorkbookPart.
        //              GetIdOfPart(worksheetPart),
        //    SheetId = 2,
        //    Name = "Sheet" + sheetId
        //};
        //sheets.Append(sheet1);

        //var headerRow1 = new Row();
        //foreach (DataColumn column in ot1.Columns)
        //{
        //    var cell = new Cell { DataType = CellValues.String, CellValue = new CellValue(column.ColumnName) };
        //    headerRow1.AppendChild(cell);
        //}
        //sheetData.AppendChild(headerRow1);
        //foreach (DataRow row in ot1.Rows)
        //{
        //    var newRow = new Row();
        //    foreach (DataColumn col in ot1.Columns)
        //    {
        //        var cell = new Cell
        //        {
        //            DataType = CellValues.String,
        //            CellValue = new CellValue(row[col].ToString())
        //        };
        //        newRow.AppendChild(cell);
        //    }

        //    sheetData.AppendChild(newRow);
        //}

        //workbookpart.Workbook.Save();
        //spreadsheetDocument.Close();

        //try
        //{
        //    Response.ClearContent();
        //    using (FileStream objFileStream = new FileStream(filename, FileMode.Open, FileAccess.Read))
        //    {
        //        byte[] data1 = new byte[objFileStream.Length];
        //        objFileStream.Read(data1, 0, data1.Length);
        //        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        //        Response.AddHeader("content-disposition", string.Format("attachment; filename ={0}", "Daily_Call_Report_" + DateTime.Now.ToString() + ".xlsx"));
        //        Response.BinaryWrite(data1);
        //    }
        //    FileInfo currentfile = new FileInfo(filename);
        //    currentfile.Delete();
        //}
        //catch (Exception ex)
        //{
        //}
        //Response.End();


        ExportData();
    }

    public DataTable generateSecondaryExcel(string divcode, string Sf_Code, string FDate, string TDate, string subdiv_code, string statecode)
    {
        //divcode = div_code.ToString();
        //Sf_Code = ddlMR.SelectedValue.ToString();
        //FDate = TextBox1.Text.ToString();
        //TDate = TextBox2.Text.ToString();
        //subdiv_code = "0";
        //statecode = "0";

        Product SFD = new Product();
        
        DataSet dsGV = SFD.dcrgetAllBrd_Qty(divcode, Sf_Code, FDate, TDate, subdiv_code, statecode);
        DataSet ds = SFD.dcrgetAllBrd_USR(divcode, Sf_Code, FDate, TDate, subdiv_code, statecode);
        DataSet ds1 = SFD.pridcrgetAllBrd_USR(divcode, Sf_Code, FDate, TDate, subdiv_code, statecode);


        DataTable dtOrders = new DataTable();
        dtOrders.Columns.Add("S.No", typeof(string));
        dtOrders.Columns.Add("sf_code", typeof(string));
        dtOrders.Columns.Add("Date", typeof(string));
        dtOrders.Columns.Add("SR", typeof(string));
        dtOrders.Columns.Add("Retailer Code", typeof(string));
        dtOrders.Columns.Add("Retailer Name", typeof(string));
        dtOrders.Columns.Add("Mobile", typeof(string));
        dtOrders.Columns.Add("Channel", typeof(string));
        dtOrders.Columns.Add("Class", typeof(string));        
        dtOrders.Columns.Add("Route", typeof(string));

        if (divcode == "179")
        {
            dtOrders.Columns.Add("Joint Work", typeof(string));
        }

        dtOrders.Columns.Add("Distributor", typeof(string));
        

        var ProductNames = (from row in ds.Tables[0].AsEnumerable()
                            orderby row.Field<string>("Product_Detail_Name")
                            select new
                            {
                                Product_Code = row.Field<string>("Product_Code"),
                                Product_Name = row.Field<string>("Product_Detail_Name")
                            }).Distinct().ToList();

        foreach (var str in ProductNames)
        {
            dtOrders.Columns.Add(str.Product_Name.ToString(), typeof(double));
        }
        dtOrders.Columns.Add("Order Type", typeof(string));
        dtOrders.Columns.Add("Order Value", typeof(double));
        dtOrders.Columns.Add("Remarks", typeof(string));
        //dtOrders.Columns.Add("Submitted Address", typeof(string));


        int i = 1;
        foreach (DataRow dr in dsGV.Tables[0].Rows)
        {
            DataRow rw = dtOrders.NewRow();

            string SfCode = dr["Sf_Code"].ToString();
            string Cust_Code = dr["Trans_Detail_Info_Code"].ToString();
            string Activity_Date = dr["Activity_Date"].ToString();

            rw["S.No"] = i.ToString();
            rw["sf_code"] = dr["Sf_Code"].ToString();            
            rw["Date"] = dr["Activity_Date"].ToString();
            rw["SR"] = dr["SF_Name"].ToString();
            rw["Retailer Code"] = dr["Trans_Detail_Info_Code"].ToString();
            rw["Retailer Name"] = dr["Trans_Detail_Name"].ToString();
            rw["Mobile"] = dr["Mobile"].ToString();
            rw["Channel"] = dr["Special"].ToString();            
            rw["Class"] = dr["Class"].ToString();
            rw["Route"] = dr["SDP_Name"].ToString();

            if (divcode == "179")
            {
                rw["Joint Work"] = dr["Worked_with_Name"].ToString();                
            }

            rw["Distributor"] = dr["stockist_name"].ToString();            

            DataRow[] drp = ds.Tables[0].Select("Sf_Code='" + SfCode + "' and Cust_Code='" + Cust_Code + "' and Activity_Date='" + Activity_Date + "'");
            for (int i1 = 0; i1 < drp.Length; i1++)
            {
                rw[(drp[i1]["Product_Detail_Name"].ToString())] =
                    Convert.ToDouble((string.IsNullOrEmpty(Convert.ToString(rw[drp[i1]["Product_Detail_Name"].ToString()]))) ? 0 : rw[(drp[i1]["Product_Detail_Name"].ToString())]) + Convert.ToDouble(drp[i1]["Quantity"]);

                rw["Order Value"] = (drp[i1]["value"]).Equals(DBNull.Value) ? 0 : Convert.ToDouble(drp[i1]["value"]);

                rw["Order Type"] = (drp[i1]["OrderType"]).Equals(DBNull.Value) ? "" : Convert.ToString(drp[i1]["OrderType"]);
            }

            rw["Remarks"] = Convert.ToString(dr["Activity_Remarks"]);
            //rw["Submitted Address"] = Convert.ToString(dr["SubmittedAddress"]);


            //rw["Net Weight Value"] = (dr["net_weight_value"]).Equals(DBNull.Value) ? 0 : Convert.ToDouble(dr["net_weight_value"]);


            dtOrders.Rows.Add(rw);
            i++;
        }


        return dtOrders;
    }


    public DataTable generateSecondaryExcel1(string divcode, string Sf_Code, string FDate, string TDate, string subdiv_code, string statecode)
    {
        //divcode = div_code.ToString();
        //Sf_Code = ddlMR.SelectedValue.ToString();
        //FDate = TextBox1.Text.ToString();
        //TDate = TextBox2.Text.ToString();
        //subdiv_code = "0";
        //statecode = "0";

        Product SFD = new Product();
        
        DataSet dsGV = SFD.pridcrgetAllBrd_Qty(divcode, Sf_Code, FDate, TDate, subdiv_code, statecode);
        DataSet ds = SFD.dcrgetAllBrd_USR(divcode, Sf_Code, FDate, TDate, subdiv_code, statecode);
        DataSet ds1 = SFD.pridcrgetAllBrd_USR(divcode, Sf_Code, FDate, TDate, subdiv_code, statecode);


        DataTable dtOrders = new DataTable();
        dtOrders.Columns.Add("S.No", typeof(string));
        dtOrders.Columns.Add("Date", typeof(string));
        dtOrders.Columns.Add("SR", typeof(string));
        dtOrders.Columns.Add("Stockist Code", typeof(string));
        dtOrders.Columns.Add("Stockist Name", typeof(string));
        dtOrders.Columns.Add("Route", typeof(string));
        dtOrders.Columns.Add("Order Type", typeof(string));

        var ProductNames = (from row in ds1.Tables[0].AsEnumerable()
                            orderby row.Field<string>("Product_Name")
                            select new
                            {
                                Product_Code = row.Field<string>("Product_Code"),
                                Product_Name = row.Field<string>("Product_Name")
                            }).Distinct().ToList();

        foreach (var str in ProductNames)
        {
            dtOrders.Columns.Add(str.Product_Name.ToString(), typeof(string));
        }
        
        dtOrders.Columns.Add("Order Value", typeof(double));
        dtOrders.Columns.Add("Remarks", typeof(string));
        
        int i = 1;
        foreach (DataRow dr in dsGV.Tables[0].Rows)
        {
            DataRow rw = dtOrders.NewRow();

            string SfCode = dr["Sf_Code"].ToString();
            string Cust_Code = dr["Trans_Detail_Info_Code"].ToString();
            string Activity_Date = dr["Activity_Date"].ToString();

            rw["S.No"] = i.ToString();
            rw["Date"] = dr["Activity_Date"].ToString();
            rw["SR"] = dr["SF_Name"].ToString();
            rw["Stockist Code"] = dr["Trans_Detail_Info_Code"].ToString();
            rw["Stockist Name"] = dr["stockist"].ToString();
            rw["Route"] = dr["SDP_Name"].ToString();
           
           

            DataRow[] drp = ds1.Tables[0].Select("Sf_Code='" + SfCode + "' and Activity_Date='" + Activity_Date + "'");
            for (int i1 = 0; i1 < drp.Length; i1++)
            {
                rw[(drp[i1]["Product_Name"].ToString())] =
                    Convert.ToDouble((string.IsNullOrEmpty(Convert.ToString(rw[drp[i1]["Product_Name"].ToString()]))) ? 0 : rw[(drp[i1]["Product_Name"].ToString())]) + Convert.ToDouble(drp[i1]["Quantity"]);

                rw["Order Type"] = (drp[i1]["OrderType"]).Equals(DBNull.Value) ? "" : Convert.ToString(drp[i1]["OrderType"]);

                

                
            }
            rw["Order Value"] = (dr["POB_Value"]).Equals(DBNull.Value) ? 0 : Convert.ToDouble(dr["POB_Value"]);
            rw["Remarks"] = Convert.ToString(dr["Activity_Remarks"]);
            
            dtOrders.Rows.Add(rw);
            i++;
        }


        return dtOrders;
    }
}
   