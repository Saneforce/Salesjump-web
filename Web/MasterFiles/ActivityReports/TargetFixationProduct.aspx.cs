using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bus_EReport;

public partial class TargetFixationProduct : System.Web.UI.Page
{
    DataSet dsSalesForce = null;
    string div_code = string.Empty;
    string sfCode = string.Empty;
    SampleDespatch objSample = new SampleDespatch();
    SalesForce sf = new SalesForce();
    Product objProduct = new Product();
    TargetFixation objTarget = new TargetFixation();

    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        sfCode = Session["sf_code"].ToString();
        if (!IsPostBack)
        {
            this.FillMasterList();
            //this.BindGrid();
            //menu.FindControl("btnBack").Visible = false;
        }
    }

    private void FillMasterList()
    {
        dsSalesForce = sf.UserList_getMR(div_code, sfCode);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            ddlFieldForce.DataTextField = "sf_name";
            ddlFieldForce.DataValueField = "sf_code";
            ddlFieldForce.DataSource = dsSalesForce;
            ddlFieldForce.DataBind();

            ddlFieldForce.Items.Insert(0, new ListItem("--Select--", ""));
        }
    }

    protected void BindGrid()
    {
        DataSet dsProducts = null;
        dsProducts = objTarget.GetTargetFixationList(ddlFieldForce.SelectedValue, div_code, ddlYear.SelectedValue);
        if (dsProducts.Tables.Count > 0)
        {
            var dtProducts = dsProducts.Tables[0];
            //var transSlNo = from dt in dtProducts
            //                where dt.Field<string>("Trans_sl_No") != string.Empty
            //                select dt;

            DataRow drow = dtProducts.AsEnumerable().Where(p => p.Field<decimal>("Trans_sl_No") > 0).FirstOrDefault();

            if (drow != null)
            {
                hdnTransSlNo.Value = Convert.ToString(drow["Trans_sl_No"]);
            }

            gvTarget.Visible = true;
            btnSubmit.Visible = true;
            gvTarget.DataSource = dsProducts;
            gvTarget.DataBind();
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string output = string.Empty;
        if (hdnTransSlNo.Value == string.Empty)
        {
            output = objTarget.RecordHeadAdd(ddlFieldForce.SelectedValue, div_code, ddlYear.SelectedValue);
        }
        else
        {
            objTarget.RecordUpdate_TargetMain(hdnTransSlNo.Value);
            objTarget.RecordDelete_TargetDetails(hdnTransSlNo.Value);
            output = hdnTransSlNo.Value;
            hdnTransSlNo.Value = string.Empty;
        }

        if (output != "0")
        {
            for (int rowcnt = 0; rowcnt < gvTarget.Rows.Count; rowcnt++)
            {
                GridViewRow gvrow = gvTarget.Rows[rowcnt];
                HiddenField hdnProdCode = (HiddenField)gvrow.FindControl("hdnProdCode");
                for (int monCnt = 1; monCnt <= 12; monCnt++)
                {
                    TextBox txtMonthValue = (TextBox)gvrow.FindControl("txtMonth" + Convert.ToString(monCnt));
                    if (txtMonthValue != null)
                    {
                        if(txtMonthValue.Text != string.Empty)
                            objTarget.RecordDetailsAdd(output, hdnProdCode.Value, txtMonthValue.ID.Replace("txtMonth", ""), Convert.ToInt32(txtMonthValue.Text));
                    }
                }
            }

            this.ClearControls();
            Page.ClientScript.RegisterStartupScript(this.GetType(), "SuccessMsg", "alert('Target Fixation Saved Successfully!');", true);
        }
    }
    protected void CmdGo_Click(object sender, EventArgs e)
    {
        this.BindGrid();
    }

    private void ClearControls()
    {
        gvTarget.Visible = false;
        btnSubmit.Visible = false;
        ddlFieldForce.SelectedIndex = 0;
        ddlYear.SelectedIndex = 0;
    }
}