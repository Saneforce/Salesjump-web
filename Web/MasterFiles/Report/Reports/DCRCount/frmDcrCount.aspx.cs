using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
using System.Drawing.Imaging;

public partial class Reports_DCRCount_frmDcrCount : System.Web.UI.Page
{
    DataSet dsSalesForce = null;
    DataSet dsDivision = null;
    DataSet dsUserList = new DataSet();
    DataSet dsdiv = null;
    string div_code = string.Empty;
    string sf_code = string.Empty;
    string sf_type = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
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
            Menu1.Title = Page.Title;
            Menu1.FindControl("btnBack").Visible = false;                        
            Filldiv();
            FillMRManagers();
           // ddlDivision.SelectedIndex = 1;
            ddlFieldForce.SelectedIndex = 1;        
            ddlDivision_SelectedIndexChanged(sender, e);
            BindDate();
            btnSubmit.Focus();

            
        }

        FillColor();
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
                ddlYear.Items.Add(k.ToString());
            }
           
            ddlYear.Text = DateTime.Now.Year.ToString();
            ddlMonth.Text = DateTime.Now.Month.ToString();

            //ddlMonth.SelectedValue = DateTime.Today.AddMonths(-1).Month.ToString();
           
        }
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



    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        SalesForce sf = new SalesForce();
        string strVacant = "1";
        if (chkWOVacant.Checked == true)
        {
            strVacant = "0";
        }

        string sURL = "";

        sURL = "rptDCRCount.aspx?sf_code=" + ddlFieldForce.SelectedValue.ToString() +
               "&Month=" + ddlMonth.SelectedValue.ToString() + " &Year=" + ddlYear.SelectedValue.ToString() +             
               "&sf_name=" + ddlFieldForce.SelectedItem.Text.ToString() +
               "&StrVacant=" + strVacant +
               "&div_Code=" + ddlDivision.SelectedValue.ToString() + "";

        string newWin = "window.open('" + sURL + "',null,'resizable=yes,toolbar=no,scrollbars=yes,menubar=no,status=no,width=500,height=600,left=0,top=0');";
        ClientScript.RegisterStartupScript(this.GetType(), "pop", newWin, true);  

    }


    protected void btnClear_Click(object sender, EventArgs e)
    {

    }
}