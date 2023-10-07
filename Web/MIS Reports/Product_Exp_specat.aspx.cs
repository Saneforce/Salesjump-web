using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;

public partial class MIS_Reports_Product_Exp_specat : System.Web.UI.Page
{
    string div_code = string.Empty;
    DataSet dsSalesForce = null;
    DataSet dsUserList = null;
    DataSet dsDoc = null;
    DataSet dsDCR = null;
    int product_total = 0;
    int FWD_total = 0;
    DateTime dtCurrent;
    DataSet dsProduct = null;
    DataSet dsCatg = null;
    int MonColspan = 0;
    DataSet dsTP = null;
    string tot_dr = string.Empty;
    string tot_FWD = string.Empty;
    string tot_dcr_dr = string.Empty;
    int imissed_dr = -1;
    string sCurrentDate = string.Empty;
    string sf_code = string.Empty;
    DataSet dsSf = null;
    string sf_type = string.Empty;
    string MultiSf_Code = string.Empty;
    int mode = -1;
    DateTime ServerStartTime;
    DateTime ServerEndTime;

    protected void Page_Load(object sender, EventArgs e)
    {

        sf_code = Session["sf_code"].ToString();
        sf_type = Session["sf_type"].ToString();
        if (sf_type == "3")
        {
            div_code = Session["div_code"].ToString();
        }
        else
        {
            div_code = Session["div_code"].ToString();
        }

        if (!Page.IsPostBack)
        {
            //  menu1.Title = this.Page.Title;
            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);
            //  menu1.FindControl("btnBack").Visible = false;

            if (Session["sf_type"].ToString() == "2")
            {
                div_code = Session["div_code"].ToString();
                UserControl_MGR_Menu c1 =
               (UserControl_MGR_Menu)LoadControl("~/UserControl/MGR_Menu.ascx");
                Divid.Controls.Add(c1);
                c1.Title = Page.Title;
                c1.FindControl("btnBack").Visible = false;
                ddlFFType.Visible = false;
                FillMRManagers1();
                //  FillManagers();
                Fill_Product_Name();
                FillYear();

            }
            else if (Session["sf_type"].ToString() == "" || Session["sf_type"].ToString() == "3")
            {
                div_code = Session["div_code"].ToString();
                if (div_code.Contains(','))
                {
                    div_code = div_code.Remove(div_code.Length - 1);
                }
                UserControl_MenuUserControl c1 =
               (UserControl_MenuUserControl)LoadControl("~/UserControl/MenuUserControl.ascx");
                Divid.Controls.Add(c1);
                c1.Title = Page.Title;
                c1.FindControl("btnBack").Visible = false;
                FillManagers();
                Fill_Product_Name();
                FillYear();
            }


        }
        else
        {
            if (Session["sf_type"].ToString() == "2")
            {
                div_code = Session["div_code"].ToString();
                UserControl_MGR_Menu c1 =
               (UserControl_MGR_Menu)LoadControl("~/UserControl/MGR_Menu.ascx");
                Divid.Controls.Add(c1);
                c1.Title = Page.Title;
                c1.FindControl("btnBack").Visible = false;

            }
            else if (Session["sf_type"].ToString() == "" || Session["sf_type"].ToString() == "3")
            {
                div_code = Session["div_code"].ToString();
                if (div_code.Contains(','))
                {
                    div_code = div_code.Remove(div_code.Length - 1);
                }
                UserControl_MenuUserControl c1 =
               (UserControl_MenuUserControl)LoadControl("~/UserControl/MenuUserControl.ascx");
                Divid.Controls.Add(c1);
                c1.Title = Page.Title;
                c1.FindControl("btnBack").Visible = false;

            }
        }

        foreach (ListItem item in ddlprod.Items)
        {
            if (item.Value == "-1")
            {
                item.Attributes.Add("style", "color:red");
            }

        }
        FillColor();
    }

    private void FillYear()
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

    private void FillMRManagers1()
    {
        SalesForce sf = new SalesForce();

        dsSalesForce = sf.SalesForceListMgrGet_MgrOnly(div_code, sf_code);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            ddlFieldForce.DataTextField = "sf_name";
            ddlFieldForce.DataValueField = "sf_code";
            ddlFieldForce.DataSource = dsSalesForce;
            ddlFieldForce.DataBind();

            //ddlSF.DataTextField = "Desig_Color";
            //ddlSF.DataValueField = "sf_code";
            //ddlSF.DataSource = dsSalesForce;
            //ddlSF.DataBind();

            ddlSF.DataTextField = "des_color";
            ddlSF.DataValueField = "sf_code";
            ddlSF.DataSource = dsSalesForce;
            ddlSF.DataBind();
        }
        FillColor();


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
        FillColor();

    }
    protected void btnGo_Click(object sender, EventArgs e)
    {
        int FYear = Convert.ToInt32(ddlFYear.SelectedValue);
        int TYear = Convert.ToInt32(ddlTYear.SelectedValue);
        int FMonth = Convert.ToInt32(ddlFMonth.SelectedValue);
        int TMonth = Convert.ToInt32(ddlTMonth.SelectedValue);
        if (FMonth > TMonth && TYear == FYear)
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
                    string strspec = "";
                    //string strspec_name = "";
                    string strcat = "";
                    //string strcat_name = "";

                if (rdoType.SelectedValue == "0")
                {
                
                    for (int iIndex = 0; iIndex < chkspec.Items.Count; iIndex++)
                    {
                        if (chkspec.Items[iIndex].Selected == true)
                        {
                            strspec += chkspec.Items[iIndex].Value + ",";
                            //strspec_name += "'" + chkspec.Items[iIndex].Text + "'" + ",";
                        }
                    }

                    strspec = strspec.Remove(strspec.Length - 1);
                    //strspec_name = strspec_name.Remove(strspec.Length - 1);
                }
                else if (rdoType.SelectedValue == "1")
                {                 
                    for (int iIndex = 0; iIndex < chkcat.Items.Count; iIndex++)
                    {
                        if (chkcat.Items[iIndex].Selected == true)
                        {                         
                            strcat += chkcat.Items[iIndex].Value + ",";
                            //strcat_name += "'" + chkcat.Items[iIndex].Text + "'" + ",";
                        }
                    }

                    strcat = strcat.Remove(strcat.Length - 1);
                    //strcat_name = strcat_name.Remove(strcat.Length - 1);
                }

                string sURL = string.Empty;
                if (ddlprod.SelectedValue != "-1")
                {

                    if (ddlprod.SelectedIndex > 0)
                    {
                        sURL = "rptProduct_Exp_specat.aspx?sfcode=" + ddlFieldForce.SelectedValue.ToString() + "&FMonth=" + ddlFMonth.SelectedValue.ToString() + "&FYear=" + ddlFYear.SelectedValue.ToString() + "&TMonth=" + ddlTMonth.SelectedValue.ToString() + "&TYear=" + ddlTYear.SelectedValue.ToString() + "&Prod=" + ddlprod.SelectedValue.ToString() + "&Prod_Name=" + ddlprod.SelectedItem.Text.ToString() + "&sf_name=" + ddlFieldForce.SelectedItem.Text.ToString() + "&chkspec=" + strspec + "&chkcat=" + strcat + "&Type=" + rdoType.SelectedValue.ToString();
                    }
                    else
                    {
                        sURL = "rptProduct_Exp_specat.aspx?sfcode=" + ddlFieldForce.SelectedValue.ToString() + "&FMonth=" + ddlFMonth.SelectedValue.ToString() + "&FYear=" + ddlFYear.SelectedValue.ToString() + "&TMonth=" + ddlTMonth.SelectedValue.ToString() + "&TYear=" + ddlTYear.SelectedValue.ToString() + "&Prod=" + ddlprod.SelectedValue.ToString() + "&Prod_Name=" + ddlprod.SelectedItem.Text.ToString() + "&sf_name=" + ddlFieldForce.SelectedItem.Text.ToString() + "&chkspec=" + strspec + "&chkcat=" + strcat + "&Type=" + rdoType.SelectedValue.ToString();
                    }


                    string newWin = "window.open('" + sURL + "',null,'resizable=yes,toolbar=no,scrollbars=yes,menubar=no,status=no,width=800,height=600,left=0,top=0');";
                    ClientScript.RegisterStartupScript(this.GetType(), "pop", newWin, true);
                }
                else
                {

                    if (ddlprod.SelectedIndex > 0)
                    {
                        if (ddlMR.SelectedValue == "0")
                        {
                            sURL = "rptProduct_Exp_specat.aspx?sfcode=" + ddlFieldForce.SelectedValue.ToString() + "&FMonth=" + ddlFMonth.SelectedValue.ToString() + "&FYear=" + ddlFYear.SelectedValue.ToString() + "&TMonth=" + ddlTMonth.SelectedValue.ToString() + "&TYear=" + ddlTYear.SelectedValue.ToString() + "&Prod=" + ddlprod.SelectedValue.ToString() + "&Prod_Name=" + ddlprod.SelectedItem.Text.ToString() + "&sf_name=" + ddlFieldForce.SelectedItem.Text.ToString() + "&chkspec=" + strspec + "&chkcat=" + strcat + "&Type=" + rdoType.SelectedValue.ToString();
                        }
                        else
                        {

                            sURL = "rptProduct_Exp_specat.aspx?sfcode=" + ddlMR.SelectedValue.ToString() + "&FMonth=" + ddlFMonth.SelectedValue.ToString() + "&FYear=" + ddlFYear.SelectedValue.ToString() + "&TMonth=" + ddlTMonth.SelectedValue.ToString() + "&TYear=" + ddlTYear.SelectedValue.ToString() + "&Prod=" + ddlprod.SelectedValue.ToString() + "&Prod_Name=" + ddlprod.SelectedItem.Text.ToString() + "&sf_name=" + ddlMR.SelectedItem.Text.ToString() + "&chkspec=" + strspec + "&chkcat=" + strcat + "&Type=" + rdoType.SelectedValue.ToString();
                        }
                    }
                    else
                    {
                        sURL = "rptProduct_Exp_specat.aspx?sfcode=" + ddlMR.SelectedValue.ToString() + "&FMonth=" + ddlFMonth.SelectedValue.ToString() + "&FYear=" + ddlFYear.SelectedValue.ToString() + "&TMonth=" + ddlTMonth.SelectedValue.ToString() + "&TYear=" + ddlTYear.SelectedValue.ToString() + "&Prod=" + ddlprod.SelectedValue.ToString() + "&Prod_Name=" + ddlprod.SelectedItem.Text.ToString() + "&sf_name=" + ddlMR.SelectedItem.Text.ToString() + "&chkspec=" + strspec + "&chkcat=" + strcat + "&Type=" + rdoType.SelectedValue.ToString();
                    }


                    string newWin = "window.open('" + sURL + "',null,'resizable=yes,toolbar=no,scrollbars=yes,menubar=no,status=no,width=800,height=600,left=0,top=0');";
                    ClientScript.RegisterStartupScript(this.GetType(), "pop", newWin, true);
                }
            }
        }
    }
    protected void ddlFFType_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblFF.Text = "Field Force";
        FillManagers();
        FillColor();
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

    private void Fill_Product_Name()
    {
        Product pr = new Product();
        dsProduct = pr.getProduct_Exp(div_code);
        if (dsProduct.Tables[0].Rows.Count > 0)
        {
            ddlprod.DataTextField = "Product_Detail_Name";
            ddlprod.DataValueField = "Product_Code_SlNo";
            ddlprod.DataSource = dsProduct;
            ddlprod.DataBind();
        }
        else
        {
            ddlprod.DataSource = dsProduct;
            ddlprod.DataBind();
        }
    }
    protected void ddlprod_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlprod.SelectedValue == "-1")
        {


            SalesForce sf = new SalesForce();
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
            lblMR.Visible = false;
            ddlMR.Visible = false;
        }
    }

    private void FillSpeciality()
    {
        Doctor spec = new Doctor();
        DataSet dsChkSp = new DataSet();
        dsChkSp = spec.getSpec_Exp(div_code);
        chkspec.DataSource = dsChkSp;
        chkspec.DataTextField = "Doc_Special_SName";
        chkspec.DataValueField = "Doc_Special_Code";
        chkspec.DataBind();
        chkspec.Visible = true;
        chkcat.Visible = false;
    }
    private void FillCategory()
    {
        Doctor cat = new Doctor();
        DataSet dsChkCat = new DataSet();
        dsChkCat = cat.getCat_Exp(div_code);
        chkcat.DataSource = dsChkCat;
        chkcat.DataTextField = "Doc_Cat_SName";
        chkcat.DataValueField = "Doc_Cat_Code";
        chkcat.DataBind();
        chkcat.Visible = true;
        chkspec.Visible = false;
    }
    protected void rdoType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rdoType.SelectedValue == "0")
        {
            FillSpeciality();
            
        }
        else if(rdoType.SelectedValue == "1")
        {
            FillCategory();
           
        }

    }
}