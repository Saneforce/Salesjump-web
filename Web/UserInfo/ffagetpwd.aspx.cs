using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
using System.Data.SqlClient;
public partial class UserInfo_ffagetpwd : System.Web.UI.Page
{
    DataSet dsDivision = null;
    DataSet dsSubDivision = null;
    DataSet dsSalesForce = null;
    string search = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ddlserver.SelectedIndex = 1;
            dropbind();
        }
    }
    protected void ddlFields_SelectedIndexChanged(object sender, EventArgs e)
    {
          //   FillDivision();
        search = ddlFields.SelectedValue.ToString();
       // grdSalesForce.PageIndex = 0;

        if (search == "UsrDfd_UserName" || search == "Sf_Name" || search == "Sf_HQ")
        {
            txtsearch.Visible = true;         
          
        }
        else
        {
            txtsearch.Visible = false;
          
           
        }
      
    }
    protected void dropbind()
    {
        SqlConnection con = new SqlConnection(Globals.ConnString);
        con.Open();
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "select name from sys.databases";
        cmd.Connection = con;
        SqlDataReader dr;
        dr = cmd.ExecuteReader();
        ddldatabase.DataTextField = "name";
        //  DropDownList1.DataValueField = "database_id";
        ddldatabase.DataSource = dr;
        ddldatabase.DataBind();
        //   cmd.ExecuteNonQuery();
        cmd.Dispose();
        con.Close();
    }
    private void FillDivision()
    {
        Division dv = new Division();
        dsDivision = dv.getDivision_Name();
            if (dsDivision.Tables[0].Rows.Count > 0)
            {
                ddlcompany.DataValueField = "Division_Code";
                ddlcompany.DataTextField = "Division_Name";
                ddlcompany.SelectedIndex = 0;

                ddlcompany.DataSource = dsDivision;
                ddlcompany.DataBind();
            }
        }

    protected void ddldatabase_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddldatabase.Text == "Zafodo_Net")
        {
            FillDivision();
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        //FindSalesForce(ddlFields.SelectedValue, txtsearch.Text, ddlcompany.SelectedValue);  
        Search();
    }
    private void Search()
    {
        search = ddlFields.SelectedValue.ToString();
        if (search == "UsrDfd_UserName" || search == "Sf_Name" || search == "Sf_HQ")
        {
            FindSalesForce(ddlFields.SelectedValue, txtsearch.Text,ddlcompany.SelectedValue);
        }
        else
        {
            FillSalesForce();
        }
    }
    private void FillSalesForce()
    {
        SalesForce sf = new SalesForce();
        dsSalesForce = sf.getSalesForcelist_NewUser(ddlcompany.SelectedValue);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            grdSalesForce.Visible = true;
            grdSalesForce.DataSource = dsSalesForce;
            grdSalesForce.DataBind();
        }
        else
        {
            grdSalesForce.DataSource = dsSalesForce;
            grdSalesForce.DataBind();
        }
    }
    private void FindSalesForce(string sSearchBy, string sSearchText, string div_code)
    {
        string sFind = string.Empty;
        sFind = " AND a." + sSearchBy + " like '" + sSearchText + "%' AND (a.Division_Code like '" + div_code + ',' + "%'  or " +
                     " a.Division_Code like '%" + ',' + div_code + ',' + "%') ";
        SalesForce sf = new SalesForce();
        dsSalesForce = sf.FindSalesForcelist_NewUser(sFind, div_code);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            grdSalesForce.Visible = true;
            grdSalesForce.DataSource = dsSalesForce;
            grdSalesForce.DataBind();
        }
        else
        {
            grdSalesForce.DataSource = dsSalesForce;
            grdSalesForce.DataBind();
        }

    }
    protected void ddlcompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        Search();
    }
    protected void btnLog_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Index.aspx");
    }

}