using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
using System.Data.SqlClient;

public partial class MasterFiles_FVExpense_Parameter : System.Web.UI.Page
{
    string div_Code = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            div_Code = Session["div_code"].ToString();
            if (!Page.IsPostBack)
            {
                ddlLevel.SelectedIndex = 1;
                FillExpParameter();
                menu1.FindControl("btnBack").Visible = false;
            }
        }
        catch (Exception ex)
        {

        }

    }
    private void FillExpParameter()
    {
        Territory terr = new Territory();
        DataSet dsExp = new DataSet();
        if (ddlLevel.SelectedIndex == 1)
        {
            dsExp = terr.getExp_ParameterBL(ddlLevel.SelectedIndex, div_Code);
        }
        else
        {
            dsExp = terr.getExp_ParameterMGR(ddlLevel.SelectedIndex,div_Code);
        }
        if (dsExp.Tables[0].Rows.Count > 0)
        {
            grdFVeExpParameter.DataSource = dsExp;
            grdFVeExpParameter.DataBind();
        }
        else
        {
            grdFVeExpParameter.DataSource = null;
            grdFVeExpParameter.DataBind();
        }


    }
    protected void btnNew_Click(object sender, EventArgs e)
    {
        Response.Redirect("FVAddExpense_Parameter.aspx");
    }

    protected void grdFVeExpParameter_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "this.className='highlight'");
            e.Row.Attributes.Add("onmouseout", "this.className='normal'");
        }
    }

    protected void grdFVeExpParameter_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        grdFVeExpParameter.EditIndex = -1;
        
        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    TextBox txtLimit = (TextBox)e.Row.FindControl("txtLimit");
        //}
        FillExpParameter();
        grdFVeExpParameter.Columns[4].Visible = false;
    }

    protected void grdFVeExpParameter_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        grdFVeExpParameter.EditIndex = -1;
        int iIndex = e.RowIndex;
        UpdateDesignation(iIndex);
        FillExpParameter();
    }
    protected void grdFVeExpParameter_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DropDownList ddlStateCode = (DropDownList)e.Row.FindControl("ddlExpenseType");
                Label lblExpenseType = (Label)e.Row.FindControl("lblExpenseType");
                TextBox txtLimit = (TextBox)e.Row.FindControl("txtLimit");

                if (ddlStateCode != null)
                {
                    DataRowView row = (DataRowView)e.Row.DataItem;
                    ddlStateCode.SelectedIndex = ddlStateCode.Items.IndexOf(ddlStateCode.Items.FindByText(row["Param_type"].ToString()));
                }

                if (ddlStateCode.SelectedItem.Text == "Variable with Limit")
                {
                    txtLimit.Visible = true;
                    
                    grdFVeExpParameter.Columns[4].Visible = true;
                }
            }
        }
        catch (Exception ex)
        {

        }
    }
    private void UpdateDesignation(int eIndex)
    {
        try
        {
            //System.Threading.Thread.Sleep(time);
            Label lblExpParameter_Code = (Label)grdFVeExpParameter.Rows[eIndex].Cells[1].FindControl("lblExpParameter_Code");         
            TextBox txtExpParameter_Name = (TextBox)grdFVeExpParameter.Rows[eIndex].Cells[2].FindControl("txtExpParameter_Name");           
            DropDownList ddlExpenseType = (DropDownList)grdFVeExpParameter.Rows[eIndex].Cells[3].FindControl("ddlExpenseType");
            TextBox txtLimit = (TextBox)grdFVeExpParameter.Rows[eIndex].Cells[4].FindControl("txtLimit");

            //Designation_Name = txtDesignationName.Text;
            Territory des = new Territory();
            int iReturn = des.ExpRecordUpdate(lblExpParameter_Code.Text, txtExpParameter_Name.Text, ddlExpenseType.SelectedValue,txtLimit.Text);
            if (iReturn > 0)
            {
                //menu1.Status = "State/Location Updated Successfully ";
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Updated Successfully');</script>");
            }
            else if (iReturn == -2)
            {
                //menu1.Status = "State/Location already Exist";
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Name Already Exist');</script>");
            }
        }
        catch (Exception ex)
        {

        }
    }

    protected void grdFVeExpParameter_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            int iExpParameter_Code =0;
            Label lblExpParameter_Code = (Label)grdFVeExpParameter.Rows[e.RowIndex].Cells[1].FindControl("lblExpParameter_Code");
            iExpParameter_Code = Convert.ToInt16(lblExpParameter_Code.Text);
            Territory Terr = new Territory();
            int iReturn = Terr.Exp_Parameter_RecordDelete(iExpParameter_Code);
            if (iReturn > 0)
            {
                // menu1.Status = "Designation deleted Successfully ";
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Deleted Successfully');</script>");
            }

            FillExpParameter();
        }
        catch (Exception ex)
        {

        }
    }

    protected void grdFVeExpParameter_RowEditing(object sender, GridViewEditEventArgs e)
    {
        grdFVeExpParameter.EditIndex = e.NewEditIndex;
        FillExpParameter();
        TextBox ctrl = (TextBox)grdFVeExpParameter.Rows[e.NewEditIndex].Cells[2].FindControl("txtExpParameter_Name");
        //TextBox  = (TextBox)grdFVeExpParameter.Rows[e.NewEditIndex].Cells[2].FindControl("lblExpenseType");
        ctrl.Focus();
    }
    protected void ddlLevel_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillExpParameter();
    }
}