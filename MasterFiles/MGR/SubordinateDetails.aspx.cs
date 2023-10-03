using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;
using Bus_EReport;

public partial class MasterFiles_MGR_SubordinateDetails : System.Web.UI.Page
{
    string sf_code = string.Empty;
    string div_code = string.Empty;
    string strMultiDiv = string.Empty;
    SalesForce sf = new SalesForce();
    DataSet dsUserList = new DataSet();
    Product prd = new Product();
    DataSet dsdiv = new DataSet();
    protected void Page_Load(object sender, EventArgs e)
    {
        sf_code = Session["sf_code"].ToString();
        div_code = Session["div_code"].ToString();
        if (!Page.IsPostBack)
        {
            Product prd = new Product();
            DataSet dsdiv = new DataSet();
            menu1.Title = this.Page.Title;
            menu1.FindControl("btnBack").Visible = false;
            dsdiv = prd.getMultiDivsf_Name(sf_code);
            lblSelect.Visible = true;
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
                    lblSelect.Visible = false;
                    ddlDivision.Visible = false;
                    lblDivision.Visible = false;
                    btnGo.Visible = false;
                    BindUserList();
                }
            }          
        }
    }

    private void BindUserList()
    {

        dsdiv = prd.getMultiDivsf_Name(sf_code);
        if (dsdiv.Tables[0].Rows.Count > 0)
        {
            if (dsdiv.Tables[0].Rows[0]["IsMultiDivision"].ToString() == "1")
            {
                div_code = ddlDivision.SelectedValue;
            }

        }
        dsUserList = sf.UserList_Self(div_code, sf_code);

        if (dsUserList.Tables[0].Rows.Count > 0)
        {
            grdSalesForce.Visible = true;
            grdSalesForce.DataSource = dsUserList;
            grdSalesForce.DataBind();

        }
        else
        {
            grdSalesForce.DataSource = dsUserList;
            grdSalesForce.DataBind();
        }
    }
    protected void btnGo_Click(object sender, EventArgs e)
    {
        try
        {
            lblSelect.Visible = false;
            BindUserList();
        }
        catch (Exception ex)
        {

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
   
}