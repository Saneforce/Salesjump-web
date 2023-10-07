using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
using System.Drawing.Imaging;

public partial class Reports_Work_Type_Status_View : System.Web.UI.Page
{
    DataSet dsSalesForce = null;
    DataSet dsDivision = null;
    string div_code = string.Empty;
    string sf_code = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        sf_code = Session["sf_code"].ToString();

        if (!Page.IsPostBack)
        {
            Menu1.Title = Page.Title;
            Menu1.FindControl("btnBack").Visible = false;
            FillWorkType();
            Filldiv();
            FillMRManagers();
            BindDate();            
        }
        FillColor();
    }

    private void FillWorkType()
    {
        DCR dcrWorkType = new DCR();
        DataSet dsChkWT = new DataSet();
        dsChkWT = dcrWorkType.DCR_get_WorkType();
        chkWorkType.DataSource = dsChkWT;
        chkWorkType.DataTextField = "Worktype_Name_B";
        chkWorkType.DataValueField = "WorkType_Code_B";
        chkWorkType.DataBind();
    }

    private void FillMRManagers()
    {
        SalesForce sf = new SalesForce();
        dsSalesForce = sf.UserList_Hierarchy(ddlDivision.SelectedValue.ToString(), "admin");
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

    private void FillColor()
    {
        int j = 0;


        foreach (ListItem ColorItems in ddlSF.Items)
        {
            string bcolor = "#" + ColorItems.Text;
            ddlFieldForce.Items[j].Attributes.Add("style", "background-color:" + bcolor);
            j = j + 1;

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

    protected void ddlDivision_SelectedIndexChanged(object sender, EventArgs e)
    {
        
        FillMRManagers();
    }

    private void BindDate()
    {
        TourPlan tp = new TourPlan();
        DataSet dsTP = new DataSet();

        dsTP = tp.Get_TP_Edit_Year(ddlDivision.SelectedValue.ToString());
        if (dsTP.Tables[0].Rows.Count > 0)
        {
            for (int k = Convert.ToInt16(dsTP.Tables[0].Rows[0]["Year"]); k <= DateTime.Now.Year + 1; k++)
            {
                ddlFrmYear.Items.Add(k.ToString());
                ddlToYear.Items.Add(k.ToString());
            }

            ddlFrmYear.Text = DateTime.Now.Year.ToString();
            ddlToYear.Text = DateTime.Now.Year.ToString();

            ddlFrmMonth.SelectedValue = DateTime.Today.AddMonths(-1).Month.ToString();
            ddlToMonth.SelectedValue = DateTime.Now.Month.ToString();
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            string strWorkTypeName="";

            for (int iIndex = 0; iIndex < chkWorkType.Items.Count; iIndex++)
            {                
                    //chkWorkType.Text = "";
                    //Chkweek.Items[iIndex].Selected = true;

                    if (chkWorkType.Items[iIndex].Selected == true)
                    {
                        //strWorkTypeName += "'" + chkWorkType.Items[iIndex].Text + "'" + ",";
                        strWorkTypeName +=  chkWorkType.Items[iIndex].Value + ",";
                    }                    
            }

            strWorkTypeName = strWorkTypeName.Remove(strWorkTypeName.Length - 1);

            string sURL = "";

            sURL = "rptWrkTypeViewStatus.aspx?sf_code=" + ddlFieldForce.SelectedValue.ToString() +
                   "&Frm_year=" + ddlFrmYear.SelectedValue.ToString() + " &Frm_Month=" + ddlFrmMonth.SelectedValue.ToString() +
                   "&To_year=" + ddlToYear.SelectedValue.ToString() + " &To_Month=" + ddlToMonth.SelectedValue.ToString() + 
                   "&sf_name=" + ddlFieldForce.SelectedItem.Text.ToString() + 
                   "&ChkWorkType="+ strWorkTypeName +
                   "&div_Code=" + ddlDivision.SelectedValue.ToString() + "";

            string newWin = "window.open('" + sURL + "',null,'resizable=yes,toolbar=no,scrollbars=yes,menubar=no,status=no,width=800,height=600,left=0,top=');";
            ClientScript.RegisterStartupScript(this.GetType(), "pop", newWin, true);

        }
        catch (Exception ex)
        {

        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {

        for (int iIndex = 0; iIndex < chkWorkType.Items.Count; iIndex++)
        {
            chkWorkType.Items[iIndex].Selected = false;
        }

        ddlFieldForce.SelectedIndex = -1;
        ddlFrmYear.SelectedIndex = -1;
        ddlToYear.SelectedIndex = -1;
        ddlFrmMonth.SelectedIndex = -1;
        ddlToMonth.SelectedIndex = -1;
    }
}