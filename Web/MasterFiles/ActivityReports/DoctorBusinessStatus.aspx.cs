using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bus_EReport;
using System.Data;
using System.Web.UI.HtmlControls;

public partial class DoctorBusinessStatus : System.Web.UI.Page
{
    string sfCode = string.Empty;
    DCRBusinessEntry objDCRBusiness = new DCRBusinessEntry();
    
    Territory objTerritory = new Territory();
    DataSet dsSalesForce = null;
    string div_code = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        //div_code = Session["div_code"].ToString();
        div_code = Session["div_code"].ToString();
        sfCode = Session["sf_code"].ToString();
        if (!Page.IsPostBack)
        {
            this.FillMasterList();
            this.AddDefaultFirstRecord();
            menu.FindControl("btnBack").Visible = false;
        }
    }

    private void FillMasterList()
    {
        SalesForce sf = new SalesForce();
        Doctor objDoctor = new Doctor();
        Product objProduct = new Product();
        dsSalesForce = sf.UserList_Hierarchy(div_code, sfCode);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            ddlFieldForce.DataTextField = "sf_name";
            ddlFieldForce.DataValueField = "sf_code";
            ddlFieldForce.DataSource = dsSalesForce;
            ddlFieldForce.DataBind();
            ddlFieldForce.Items.Insert(0, new ListItem("--Select--",""));
        }

       
    }

    private void AddDefaultFirstRecord()
    {
        //creating dataTable   
        DataTable dt = new DataTable();
        DataRow dr;
        dt.TableName = "BusinessStatus";
        dt.Columns.Add(new DataColumn("FieldForceName", typeof(string)));
        dt.Columns.Add(new DataColumn("HQ", typeof(string)));
        dt.Columns.Add(new DataColumn("Designation", typeof(string)));
        dt.Columns.Add(new DataColumn("Status", typeof(string)));
        dt.Columns.Add(new DataColumn("Trans_sl_No", typeof(string)));
        dr = dt.NewRow();
        dt.Rows.Add(dr);
       
        //bind Gridview  
        gvDoctorBusiness.DataSource = dt;
        gvDoctorBusiness.DataBind();
    }

    protected void gvDoctorBusiness_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;

        if (e.CommandName == "Delete")
        {
            //DataTable dt = ViewState["DoctorBus"] as DataTable;
            //dt.Rows.Remove(dt.Rows[row.RowIndex]);
            //ViewState["DoctorBus"] = dt;
            //this.BindGrid();
        }
        else if (e.CommandName == "Edit")
        {

            //hdnRowID.Value = Convert.ToString(row.RowIndex);
            HiddenField hdnTransNo = (HiddenField)row.FindControl("hdnTransNo");
            if (hdnTransNo.Value != string.Empty)
            {
                objDCRBusiness.RecordUpdate_BusinessStatus(hdnTransNo.Value, "0");
                this.BindExistingRows();
            }

            //Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "addMultipleRows('" + hdnProducts.Value + "')", true);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowStatus", "javascript:alert('Clicked record Activated for Submit!');", true);
        }
    }

    protected void gvDoctorBusiness_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }
    
    private void BindExistingRows()
    {
        //creating dataTable   
        DataSet dsBusiness = new DataSet();
        dsBusiness = objDCRBusiness.GetDCRBusinessStatus(ddlFieldForce.SelectedValue, ddlYear.SelectedValue, ddlMonth.SelectedValue);
        DataTable dtBusiness = dsBusiness.Tables[0];
        if (dtBusiness.Rows.Count > 0)
        {
            //bind Gridview  
            gvDoctorBusiness.DataSource = dtBusiness;
            gvDoctorBusiness.DataBind();
        }
    }
    protected void btnGo_Click(object sender, EventArgs e)
    {
        this.BindExistingRows();
    }
    protected void gvDoctorBusiness_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            HiddenField hdnStatus = (HiddenField)e.Row.FindControl("hdnStatus");
            LinkButton lnkActivate = (LinkButton)e.Row.FindControl("lnkActivate");

            if (hdnStatus.Value == "1")
            {
                lnkActivate.Enabled = true;
            }
            else
            {
                lnkActivate.Enabled = false;
            }
        }
    }
}