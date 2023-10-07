using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
using System.Data.SqlClient;
public partial class MasterFiles_Distance_Fixation : System.Web.UI.Page
{
    string div_code = string.Empty;
    DataSet dsSalesForce = new DataSet();
    string sfCode = string.Empty;
    string EX_Distance = string.Empty;
    string OS_Distance = string.Empty;
    string OSEX_Distance = string.Empty;
    string terrName_From = string.Empty;
    string terrName_To = string.Empty;
    DataSet dsTerritory = null;
    string terr_Name = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        if (Session["sf_code"] != null && Session["sf_code"].ToString() != "")
        {
            sfCode = Session["sf_code"].ToString();
        }
        
        if (!Page.IsPostBack)
        {
            menu1.Title = this.Page.Title;
            menu1.FindControl("btnBack").Visible = false;
            //FillReporting();
            FillSalesForce();
        }
        //getHQ_Dist();
        //getOS_Dist();
        //getEx_Dist();
       // getOSEx_Dist();
    }
    //private void FillReporting()
    //{
    //    SalesForce sf = new SalesForce();
    //    dsSalesForce = sf.getUserList_Reporting(div_code);
    //    if (dsSalesForce.Tables[0].Rows.Count > 0)
    //    {
    //        ddlFilter.DataTextField = "Sf_Name";
    //        ddlFilter.DataValueField = "Sf_Code";
    //        ddlFilter.DataSource = dsSalesForce;
    //        ddlFilter.DataBind();
    //    }
    //}
    private void GetHQ()
    {
        Expense terr = new Expense();
        dsTerritory = terr.getTerrritoryView(sfCode);
        if (dsTerritory.Tables[0].Rows.Count > 0)
        {
            lblFieldName.Text = "<span style='font-weight: bold;color:Blue;font-names:verdana'>Field Force Name:-</span>" +
                "<span style='font-weight: bold;color:Red'>  " + dsTerritory.Tables[0].Rows[0]["Sf_Name"].ToString() + "</span>";
            //Session["Sf_Name"] = dsTerritory.Tables[0].Rows[0]["Sf_Name"].ToString();
            lblTerrritory.Text = "<span style='font-weight: bold;color:Blue;font-names:verdana'>HQ Name:-</span>" +
                "<span style='font-weight: bold;color:Red'>  " + dsTerritory.Tables[0].Rows[0]["Sf_HQ"].ToString() + "</span>";

            Session["sf_HQ"] = dsTerritory.Tables[0].Rows[0]["Sf_HQ"].ToString();
            
        }
    }
    protected void grdHQ_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {

            Territory terr = new Territory();
            DataSet dsTerritory = new DataSet();
            dsTerritory = terr.getWorkAreaName(div_code);
            if (dsTerritory.Tables[0].Rows.Count > 0)
            {
                e.Row.Cells[1].Text = dsTerritory.Tables[0].Rows[0]["wrk_area_Name"].ToString();                        


            }
        }
    }
    private void FillSalesForce()
    {
        SalesForce sf = new SalesForce();
        dsSalesForce = sf.getFieldForce_Distance(div_code);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            ddlFieldForce.DataTextField = "sf_name";
            ddlFieldForce.DataValueField = "sf_code";
            ddlFieldForce.DataSource = dsSalesForce;
            ddlFieldForce.DataBind();
        }
    }
    private void getHQ_Dist()
    {
        Expense terr = new Expense();
        dsTerritory = terr.getHQ_Dist(sfCode);
        if (dsTerritory.Tables[0].Rows.Count > 0)
        {
            pnlDist.Visible = true;
            grdHQ.Visible = true;
            grdHQ.DataSource = dsTerritory;
            grdHQ.DataBind();
        }
        else
        {            
            grdHQ.DataSource = dsTerritory;
            grdHQ.DataBind();
            pnlDist.Visible = true;
        }
    }
    private void getOS_Dist()
    {
        Expense terr = new Expense();
        dsTerritory = terr.getDist(sfCode);
        DataRow[] rows=dsTerritory.Tables[0].Select("Territory_Cat=3");
        DataTable data = new DataTable();
        if (rows.Length > 0)
        {
            data = rows.CopyToDataTable();
        }

        if (data.Rows.Count > 0)
        {
            pnlDist.Visible = true;
            grdOS.Visible = true;
            grdOS.DataSource = data;
            grdOS.DataBind();
        }
        else
        {
            grdOS.DataSource = data;
            grdOS.DataBind();
            pnlDist.Visible = true;
        }
    }
    private void getEx_Dist()
    {
        Expense terr = new Expense();
        dsTerritory = terr.getDist(sfCode);
        DataRow[] rows = dsTerritory.Tables[0].Select("Territory_Cat=2");
        DataTable data = new DataTable();
        if (rows.Length > 0)
        {
            data = rows.CopyToDataTable();
        }
        if (data.Rows.Count > 0)
        {
            pnlDist.Visible = true;
            grdEX.Visible = true;
            grdEX.DataSource = data;
            grdEX.DataBind();
        }
        else
        {
            grdEX.DataSource = data;
            grdEX.DataBind();
            pnlDist.Visible = true;
        }
    }
    private void getOSEx_Dist1()
    {
        Expense terr = new Expense();
        dsTerritory = terr.getOSEXDistCondn(sfCode);
        DataSet disDataSet = terr.getOSEXCondn(sfCode);
        DataTable mainTable = disDataSet.Tables[0];
        DataTable disTable = dsTerritory.Tables[0];
        foreach (DataRow row in mainTable.Rows)
        {
            String filter="From_Code='"+row["FCode"].ToString()+"' and To_Code='"+row["TCode"].ToString()+"'";
            DataRow[] rows= disTable.Select(filter);
            if (rows.Length > 0)
            {
                row["Distance"] = rows[0]["Distance"];
            }
        }
      /*  var query = (from tbl1 in data1.AsEnumerable()
                    join tbl2 in data2.AsEnumerable() on tbl1["sf_code"] equals tbl2["sf_code"] 
                    select new
                    {
                        Terr_From = tbl1["Territory_Name"],
                        Terr_to = tbl2["Territory_Name"],
                        Terr_from_code = tbl1["Territory_code"],
                        Terr_To_code = tbl2["Territory_code"],
                        Distance = (tbl2["From_Code"].ToString() == tbl1["To_Code"].ToString() ? tbl2["Distance"] : ""),
                        Territory_Cat = "OSEX",
                        To_Code = tbl2["To_Code"],
                    }).ToList();

        */
        if (mainTable.Rows.Count > 0)
        {
            pnlDist.Visible = true;
            grdOSEX.Visible = true;
            grdOSEX.DataSource = mainTable;
            grdOSEX.DataBind();
        }
        else
        {
            grdOSEX.DataSource = mainTable;
            grdOSEX.DataBind();
            pnlDist.Visible = true;
        }
    }
    private void getOSEx_Dist()
    {
        Expense terr = new Expense();
        dsTerritory = terr.getOSEX_Dist(sfCode);
        if (dsTerritory.Tables[0].Rows.Count > 0)
        {
            pnlDist.Visible = true;
            grdOSEX.Visible = true;
            grdOSEX.DataSource = dsTerritory;
            grdOSEX.DataBind();
        }
        else
        {
            grdOSEX.DataSource = dsTerritory;
            grdOSEX.DataBind();
            pnlDist.Visible = true;
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
     
        try
        {
            lblSelect.Visible = false;
            btnSave.Visible = true;
            sfCode = ddlFieldForce.SelectedValue;
            Session["sf_code"] = sfCode;           
            GetHQ();
            getHQ_Dist();
            getOS_Dist();
            getEx_Dist();
           getOSEx_Dist1();

        }
        catch (Exception ex)
        {

        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        
        int iReturn = -1;
        Distance_calculation dist = new Distance_calculation();
        iReturn = dist.deleteSfDistance(sfCode);
        foreach (GridViewRow gridRow in grdEX.Rows)
        {
            TextBox txtKms = (TextBox)gridRow.FindControl("txtKms");
            // EX_Distance = txtKms.Text.ToString();
            EX_Distance = txtKms.Text;
            HiddenField toCode = (HiddenField)gridRow.FindControl("hdnToTerrCode");
            HiddenField frmCode = (HiddenField)gridRow.FindControl("hdnFrmTerrCode");
            HiddenField catCode = (HiddenField)gridRow.FindControl("hidcat");     
            
            if (EX_Distance != "" && catCode != null)
            {
                
                iReturn = dist.addOrUpdate(sfCode, EX_Distance, toCode.Value, frmCode.Value, catCode.Value);
            }
        }
        foreach (GridViewRow gridRow in grdOS.Rows)
        {
            TextBox txtOsKms = (TextBox)gridRow.FindControl("txtOsKms");
             // EX_Distance = txtKms.Text.ToString();
            OS_Distance = txtOsKms.Text;
            HiddenField toCode = (HiddenField)gridRow.FindControl("hdnOSToTerrCode");
            HiddenField frmCode = (HiddenField)gridRow.FindControl("hdnOSFrmTerrCode");
            HiddenField catCode = (HiddenField)gridRow.FindControl("oSHidCat");

            if (OS_Distance != "" && catCode != null)
            {
                iReturn = dist.addOrUpdate(sfCode, OS_Distance, toCode.Value, frmCode.Value, catCode.Value);
            }
        }
        foreach (GridViewRow gridRow in grdOSEX.Rows)
        {           
            TextBox txtOsExKms = (TextBox)gridRow.FindControl("txtOsExKms");
            OSEX_Distance = txtOsExKms.Text;
            HiddenField toCode = (HiddenField)gridRow.FindControl("hdnOSEXToTerrCode");
            HiddenField frmCode = (HiddenField)gridRow.FindControl("hdnOSEXFrmTerrCode");
            HiddenField catCode = (HiddenField)gridRow.FindControl("oSEXHidCat");
            if (OSEX_Distance != "")
            {
                iReturn = dist.addOrUpdate(sfCode, OSEX_Distance, toCode.Value, frmCode.Value, catCode.Value);
                //iReturn = terr.get_OsExKms(sfCode, OSEX_Distance, Terr_To.Text);
            }
        }
        if (iReturn > 0)
        {
            
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Updated Successfully');</script>");
        }
    }
}