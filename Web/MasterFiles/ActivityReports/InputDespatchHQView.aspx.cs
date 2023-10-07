using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bus_EReport;

public partial class InputDespatchHQView : System.Web.UI.Page
{
    string div_code = string.Empty;
    string sfCode = string.Empty;
    InputDespatch objInput = new InputDespatch();
    SalesForce sf = new SalesForce();
    Product objProduct = new Product();

    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        sfCode = Session["sf_code"].ToString();
        if (!Page.IsPostBack)
        {
            this.FillMasterList();
            menu.FindControl("btnBack").Visible = false;
        }
    }

    private void FillMasterList()
    {
        DataSet dsSalesForce = null;
        dsSalesForce = sf.UserList_getMR(div_code);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            ddlFieldForce.DataTextField = "sf_name";
            ddlFieldForce.DataValueField = "sf_code";
            ddlFieldForce.DataSource = dsSalesForce;
            ddlFieldForce.DataBind();
            ddlFieldForce.Items.Insert(0, new ListItem("--Select--", ""));
        }
    }


   
    protected void btnSave_Click(object sender, EventArgs e)
    {
        //this.AddNewRecordRowToGrid();
    }

    private void AddDefaultFirstRecord()
    {
        //creating dataTable   
        DataTable dt = new DataTable();
        DataRow dr;
        dt.TableName = "SampleDespatch";
        dt.Columns.Add(new DataColumn("ProductCode", typeof(string)));
        dt.Columns.Add(new DataColumn("DespatchQty", typeof(string)));
        dr = dt.NewRow();
        dt.Rows.Add(dr);
        //saving databale into viewstate   
        ViewState["SampleDes"] = dt;
        //bind Gridview  
        gvInputDespatch.DataSource = dt;
        gvInputDespatch.DataBind();
    }
    protected void gvInputDespatch_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        
    }
   
    private void ClearControls(string strClearDropdown)
    {
        if (strClearDropdown == "1")
        {
            ddlFieldForce.SelectedIndex = 0;
            ddlYear.SelectedIndex = 0;
            ddlMonth.SelectedIndex = 0;
        }
        gvInputDespatch.Visible = false;
        hdnTransNo.Value = string.Empty;
        btnUpdate.Visible = false;
        btnDelete.Visible = false;
        //divProducts.Visible = false;
    }


    protected void btnGo_Click(object sender, EventArgs e)
    {
        this.BindGrid(ddlFieldForce.SelectedValue,ddlMonth.SelectedValue,ddlYear.SelectedValue);
    }

    private void BindGrid(string sfCode, string strMonth, string strYear)
    {
        DataSet dsSampleDespatch = null;

        dsSampleDespatch = objInput.GetInputDespatchGifts(sfCode, strMonth, strYear);
        if (dsSampleDespatch.Tables[0].Rows.Count > 0)
        {
            gvInputDespatch.Visible = true;
            btnUpdate.Visible = true;
            btnDelete.Visible = true;

            hdnTransNo.Value = Convert.ToString(dsSampleDespatch.Tables[0].Rows[0]["Trans_sl_No"]);
            gvInputDespatch.DataSource = dsSampleDespatch;
            gvInputDespatch.DataBind();
        }
        else
        {
            this.ClearControls("0");
            Page.ClientScript.RegisterStartupScript(this.GetType(), "GetMsg", "alert('No Records Found!');", true);
        }
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        if (hdnTransNo.Value != string.Empty)
        {
            objInput.RecordDelete_InputDespatchDetails(hdnTransNo.Value);
            foreach(GridViewRow row in gvInputDespatch.Rows)
            {
                HiddenField hdnProdCode = (HiddenField)row.FindControl("hdnProdCode");
                TextBox txtDespatchQty = (TextBox)row.FindControl("txtDespatchQty");
                TextBox txtNewDespatchQty = (TextBox)row.FindControl("txtNewDespatchQty");
                int iNewQty = 0;
                int iQty = 0;

                if (txtDespatchQty.Text != string.Empty)
                {
                    iQty = Convert.ToInt32(txtDespatchQty.Text);
                }

                if (txtNewDespatchQty.Text != string.Empty)
                {
                    iNewQty = Convert.ToInt32(txtNewDespatchQty.Text);
                }

                objInput.RecordDetailsAdd(hdnTransNo.Value, ddlFieldForce.SelectedValue, div_code, hdnProdCode.Value, (iQty + iNewQty));
            }

            Page.ClientScript.RegisterStartupScript(this.GetType(), "UpdateMsg", "alert('Input Despatch HQ Updated Successfully!');", true);
            this.ClearControls("1");
        }
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        if (hdnTransNo.Value != string.Empty)
        {
            objInput.RecordDelete_InputDespatchDetails(hdnTransNo.Value);
            objInput.RecordDelete_InputDespatchHead(hdnTransNo.Value);
            Page.ClientScript.RegisterStartupScript(this.GetType(), "DeleteMsg", "alert('Input Despatch HQ Deleted Successfully!');", true);
            this.ClearControls("1");
        }
    }
}