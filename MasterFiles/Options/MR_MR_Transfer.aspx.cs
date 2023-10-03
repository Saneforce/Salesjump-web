using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using Bus_EReport;
using System.Drawing.Imaging;
using System.Configuration;
public partial class MasterFiles_Options_MR_MR_Transfer : System.Web.UI.Page
{
    string div_code = string.Empty;
    string sf_code = string.Empty;
    DataSet dsSalesForce = null;
    DataSet dsChemists = null;
    DataSet dsTerritory = null;
    DataSet dsListedDR = null;
    DataTable dtListedDR = null;
    DataTable dtChemists = null;
    DataTable dtChem = null;
    DataTable dt;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int time;
    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        
        if (!Page.IsPostBack)
        {
            menu1.Title = this.Page.Title;
            menu1.FindControl("btnBack").Visible = false;
            FillSalesForce();
            FillToSalesForce();
            lblSelect.Visible = true;
            ddlToFieldForce.Enabled = false;
            ddlToTerr.Enabled = false;
           // FillSF_Alpha();
            GetWorkName();
           
        }
       
    }
    protected override void OnLoadComplete(EventArgs e)
    {

        ServerEndTime = DateTime.Now;
        TrackPageTime();//It will give you page load time  
    }
    public void TrackPageTime()
    {
        TimeSpan serverTimeDiff = ServerEndTime.Subtract(ServerStartTime);
        time = serverTimeDiff.Minutes;

    }
    private void GetWorkName()
    {

        Territory terr = new Territory();
        dsTerritory = terr.getWorkAreaName(div_code);
        if (dsTerritory.Tables[0].Rows.Count > 0)
        {

            lblterrFrom.Text = "Transfer From " + dsTerritory.Tables[0].Rows[0]["wrk_area_Name"].ToString();
            lblterrTo.Text = "Transfer To " + dsTerritory.Tables[0].Rows[0]["wrk_area_Name"].ToString();
            lblTerr1.Text = "Please Select the Transfer From " + dsTerritory.Tables[0].Rows[0]["wrk_area_Name"].ToString();
            lblToTerr.Text = "Please Select the Transfer To " + dsTerritory.Tables[0].Rows[0]["wrk_area_Name"].ToString();
        }

    }
    private void FillSalesForce()
    {
       
        SalesForce sf = new SalesForce();
        dsSalesForce = sf.getFieldForce_Transfer(div_code);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
           
            ddlFromFieldForce.DataTextField = "sf_name";
            ddlFromFieldForce.DataValueField = "sf_code";
            ddlFromFieldForce.DataSource = dsSalesForce;
            ddlFromFieldForce.DataBind();
        }
    }
   
    private void FillToSalesForce()
    {
        SalesForce sf = new SalesForce();
        dsSalesForce = sf.getFieldForce_Transfer(div_code);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            ddlToFieldForce.DataTextField = "sf_name";
            ddlToFieldForce.DataValueField = "sf_code";
            ddlToFieldForce.DataSource = dsSalesForce;
            ddlToFieldForce.DataBind();
        }
    }
    protected void rdotransfer_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if (rdotransfer.Items[0].Selected)
        //{
        //    grdDoctor.Visible = true;
        //    grdChem.Visible = false;

        //}
        //else if (rdotransfer.Items[1].Selected)
        //{
        //    grdDoctor.Visible = false;         
        //    grdChem.Visible = true;
        //    FillChemists();
        //}
    }
    private void FillChemists()
    {
        Chemist chem = new Chemist();


        dsChemists = chem.getChemists_transfer(ddlFromFieldForce.SelectedValue, ddlFromTerr.SelectedValue);
        if (dsChemists.Tables[0].Rows.Count > 0)
        {
            grdChem.Visible = true;
            grdChem.DataSource = dsChemists;
            grdChem.DataBind();
            dtChem = dsChemists.Tables[0];
            ViewState["SelectedChemist"] = dtChem;
        }
        else
        {
            grdChem.DataSource = dsChemists;
            grdChem.DataBind();
        }
    }
    private void FillChemists_Trans()
    {
        Chemist chem = new Chemist();
        dsChemists = chem.getChemists_transfer(ddlToFieldForce.SelectedValue, ddlToTerr.SelectedValue);
        if (dsChemists.Tables[0].Rows.Count > 0)
        {
            grdChemist.Visible = true;
            grdChemist.DataSource = dsChemists;
            grdChemist.DataBind();
        }
        else
        {
            grdChemist.DataSource = dsChemists;
            grdChemist.DataBind();
        }
    }
    protected void ddlFromFieldForce_SelectedIndexChanged(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        lblSelect.Visible = false;
        lblTerr1.Visible = true;
        Territory terr = new Territory();
        dsTerritory = terr.getTerritory_Transfer(ddlFromFieldForce.SelectedValue);
        if (dsTerritory.Tables[0].Rows.Count > 0)
        {
            ddlFromTerr.DataTextField = "Territory_Name";
            ddlFromTerr.DataValueField = "Territory_Code";
            ddlFromTerr.DataSource = dsTerritory;
            ddlFromTerr.DataBind();
        }
    }
    protected void ddlToFieldForce_SelectedIndexChanged(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        Territory terr = new Territory();
        lblToTerr.Visible = true;
        lblTo.Visible = false;
        dsTerritory = terr.getTerritory_Transfer(ddlToFieldForce.SelectedValue);
        if (dsTerritory.Tables[0].Rows.Count > 0)
        {
            ddlToTerr.DataTextField = "Territory_Name";
            ddlToTerr.DataValueField = "Territory_Code";
            ddlToTerr.DataSource = dsTerritory;
            ddlToTerr.DataBind();
        }
    }
    //private void BindPrimaryGrid()
    //{
    //    string constr = Globals.ConnString;
    //    string query = "SELECT d.ListedDrCode,d.ListedDr_Name, d.ListedDr_Sl_No,c.Doc_Cat_Name,c.Doc_Cat_SName,s.Doc_Special_Name,s.Doc_Special_SName ,dc.Doc_ClsName,dc.Doc_ClsSName ,g.Doc_QuaName, " +
    //        //" (select t.territory_Name FROM Mas_Territory_Creation t where t.Territory_Code like d.Territory_Code) territory_Name "+
    //                 " stuff((select ', '+territory_Name from Mas_Territory_Creation t where t.Sf_Code =  '" + ddlFromFieldForce.SelectedValue + "' and charindex(cast(t.Territory_Code as varchar)+',',d.Territory_Code+',')>0 for XML path('')),1,2,'') territory_Name  FROM " +
    //                 "Mas_ListedDr d,Mas_Doctor_Category c,Mas_Doctor_Speciality s,Mas_Doc_Class dc, Mas_Doc_Qualification g " +
    //                 "WHERE d.Sf_Code =  '" + ddlFromFieldForce.SelectedValue + "'" +
    //                 "and d.Doc_Special_Code = s.Doc_Special_Code " +
    //                 "and d.Doc_ClsCode= dc.Doc_ClsCode " +
    //                 " and d.Doc_QuaCode = g.Doc_QuaCode " +
    //                 "and d.Doc_Cat_Code = c.Doc_Cat_Code  " +
    //                 "and d.ListedDr_Active_Flag = 0" +
    //                 " order by ListedDr_Sl_No";
    //    SqlConnection con = new SqlConnection(constr);
    //    SqlDataAdapter sda = new SqlDataAdapter(query, con);
    //    DataTable dt = new DataTable();
    //    sda.Fill(dt);
    //    GrdTransfer.DataSource = dt;
    //    GrdTransfer.DataBind();
    //}

    protected void ddlFromTerr_SelectedIndexChanged(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        lblTerr1.Visible = false;
        if (rdotransfer.SelectedIndex == 0)
        {
            ListedDR lstdr = new ListedDR();
            if (ddlFromFieldForce.SelectedValue.ToString().Trim().Length > 0)
                dsListedDR = lstdr.getListedDrforTerr_Camp(ddlFromFieldForce.SelectedValue, ddlFromTerr.SelectedValue);
            if (dsListedDR.Tables[0].Rows.Count > 0)
            {
                grdDoctor.Enabled = false;
                grdChem.Visible = false;
                lblTo.Visible = true;
                ddlToFieldForce.Enabled = true;
                ddlToTerr.Enabled = true;
                grdDoctor.DataSource = dsListedDR;
                grdDoctor.DataBind();
                dt = dsListedDR.Tables[0];
                ViewState["SelectedRecords"] = dt;
            }
            else
            {
                grdDoctor.DataSource = dsListedDR;
                grdDoctor.DataBind();
            }
        }
        else if(rdotransfer.SelectedIndex == 1)
        {
            grdChem.Enabled = false;
            lblTo.Visible = true;
            ddlToFieldForce.Enabled = true;
            ddlToTerr.Enabled = true;
            FillChemists();
        }
    }
    protected void grdDoctor_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {

            Territory terr = new Territory();
            DataSet dsTerritory = new DataSet();
            dsTerritory = terr.getWorkAreaName(div_code);
            if (dsTerritory.Tables[0].Rows.Count > 0)
            {
                e.Row.Cells[5].Text = dsTerritory.Tables[0].Rows[0]["wrk_area_Name"].ToString();

            }
        }
    }
    protected void GrdTransfer_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblColor = (Label)e.Row.FindControl("lblColor");
            if (lblColor.Text == "Y")
                e.Row.BackColor = System.Drawing.Color.PapayaWhip;
        }
        if (e.Row.RowType == DataControlRowType.Header)
        {

            Territory terr = new Territory();
            DataSet dsTerritory = new DataSet();
            dsTerritory = terr.getWorkAreaName(div_code);
            if (dsTerritory.Tables[0].Rows.Count > 0)
            {
                  e.Row.Cells[6].Text = dsTerritory.Tables[0].Rows[0]["wrk_area_Name"].ToString();
              
            }
        }
    }
    protected void grdChemist_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblColor = (Label)e.Row.FindControl("lblColor");
            if (lblColor.Text == "Y")
                e.Row.BackColor = System.Drawing.Color.PapayaWhip;
        }
        if (e.Row.RowType == DataControlRowType.Header)
        {

            Territory terr = new Territory();
            DataSet dsTerritory = new DataSet();
            dsTerritory = terr.getWorkAreaName(div_code);
            if (dsTerritory.Tables[0].Rows.Count > 0)
            {
                e.Row.Cells[4].Text = dsTerritory.Tables[0].Rows[0]["wrk_area_Name"].ToString();

            }
        }
    }
    protected void grdChem_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {

            Territory terr = new Territory();
            DataSet dsTerritory = new DataSet();
            dsTerritory = terr.getWorkAreaName(div_code);
            if (dsTerritory.Tables[0].Rows.Count > 0)
            {
                e.Row.Cells[5].Text = dsTerritory.Tables[0].Rows[0]["wrk_area_Name"].ToString();

            }
        }
    }
    protected void ddlToTerr_SelectedIndexChanged(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        if (rdotransfer.SelectedIndex == 0)
        {
            ListedDR lstdr = new ListedDR();
            grdDoctor.Enabled = true;
            grdChemist.Visible = false;
            lblTo.Visible = false;
            lblToTerr.Visible = false;
            btnTran.Enabled = true;
            btnClr.Enabled = true;
            int iReturn = lstdr.RecordCount_Transfer(ddlToFieldForce.SelectedValue, ddlToTerr.SelectedValue);


            lblCount.Text = "Total Listed Doctor Count:" + iReturn.ToString();


            dtListedDR = lstdr.getListedDrforTerr_Trans(ddlToFieldForce.SelectedValue, ddlToTerr.SelectedValue);

            if (dtListedDR != null)
            {
                GrdTransfer.DataSource = dtListedDR;
                GrdTransfer.DataBind();
                //  dtListedDR = dsListedDR.Tables[0];
                ViewState["MoveRecords"] = dtListedDR;
            }
        }
        else if (rdotransfer.SelectedIndex == 1)
        {
            Chemist chem = new Chemist();
            grdChem.Enabled = true;
            
            lblTo.Visible = false;
            lblToTerr.Visible = false;
          //  FillChemists_Trans();
            dtChemists = chem.getChem_transfer(ddlToFieldForce.SelectedValue, ddlToTerr.SelectedValue);
            if (dtChemists != null)
            {
                grdChemist.Visible = true;
                grdChemist.DataSource = dtChemists;
                grdChemist.DataBind();

                ViewState["MoveChem"] = dtChemists;
            }
        }

    }    
   

    protected void CheckBox_CheckChanged(object sender, EventArgs e)
    {
        CheckBox chkListedDR = (CheckBox)sender;
        // GridViewRow gridRow = (GridViewRow)chkListedDR.Parent.Parent;
        CheckBox chkAll = (CheckBox)grdDoctor.HeaderRow.Cells[0].FindControl("chkAll");
        if (chkAll.Checked == true)
        {
            foreach (GridViewRow gridRow in grdDoctor.Rows)
            {

                //Label lblDocCode = (Label)gridRow.Cells[1].FindControl("lblDocCode");
                Label lblDocName = (Label)gridRow.Cells[1].FindControl("lblDocName");
                Label lblcat = (Label)gridRow.Cells[1].FindControl("lblcat");
                Label lblSpl = (Label)gridRow.Cells[1].FindControl("lblSpl");
                Label lblterr = (Label)gridRow.Cells[1].FindControl("lblterr");
                Label lblterr_code = (Label)gridRow.Cells[1].FindControl("lblterr_code");
                CheckBox chkall = (CheckBox)gridRow.Cells[1].FindControl("chkAll");
             //   CheckBox chkListedDR1 = (CheckBox)gridRow.Cells[gridRow.RowIndex].FindControl("chkListedDR");

                dtListedDR = (DataTable)ViewState["MoveRecords"];
                DataRow drAll = dtListedDR.NewRow();

                drAll["ListedDr_Name"] = lblDocName.Text;
                drAll["Doc_Special_Name"] = lblSpl.Text;
                drAll["Doc_Cat_Name"] = lblcat.Text;
                drAll["territory_Name"] = lblterr.Text;
                drAll["color"] = "Y";

                dtListedDR.Rows.InsertAt(drAll, 0);
                dt = (DataTable)ViewState["SelectedRecords"];
                if (chkAll.Checked == true)
                {
                    List<DataRow> rows_to_remove1 = new List<DataRow>();

                    foreach (DataRow row in dt.Rows)
                    {
                        Label lblDocCode = (Label)gridRow.FindControl("lblDocCode");
                        drAll["Listeddrcode"] = lblDocCode.Text;
                        if (row["ListedDrCode"].ToString().Trim() == lblDocCode.Text.ToString().Trim())
                        {
                            rows_to_remove1.Add(row);
                        }
                    }


                    foreach (DataRow row in rows_to_remove1)
                    {
                        dt.Rows.Remove(row);
                        dt.AcceptChanges();
                    }
                }
            }
        }
        else
        {
            GridViewRow gridRow1 = (GridViewRow)chkListedDR.Parent.Parent;
            Label lblDocName = (Label)gridRow1.Cells[1].FindControl("lblDocName");
            Label lblcat = (Label)gridRow1.Cells[1].FindControl("lblcat");
            Label lblSpl = (Label)gridRow1.Cells[1].FindControl("lblSpl");
            Label lblterr = (Label)gridRow1.Cells[1].FindControl("lblterr");
            Label lblterr_code = (Label)gridRow1.Cells[1].FindControl("lblterr_code");

            Label lblDocCode = (Label)gridRow1.Cells[gridRow1.RowIndex].FindControl("lblDocCode");
            dtListedDR = (DataTable)ViewState["MoveRecords"];

            DataRow dr = dtListedDR.NewRow();

            dr["ListedDrCode"] = lblDocCode.Text;
            dr["ListedDr_Name"] = lblDocName.Text;
            dr["Doc_Cat_Name"] = lblcat.Text;
            dr["Doc_Special_Name"] = lblSpl.Text;
            dr["territory_Name"] = lblterr.Text;
            dr["color"] = "Y";

            //   dtListedDR.Rows.Add(dr);
            dtListedDR.Rows.InsertAt(dr, 0);
            dt = (DataTable)ViewState["SelectedRecords"];


            List<DataRow> rows_to_remove = new List<DataRow>();

            foreach (DataRow rowDR in dt.Rows)
            {
                if (rowDR["ListedDrCode"].ToString().Trim() == lblDocCode.Text.ToString().Trim())
                {
                    rows_to_remove.Add(rowDR);
                }
            }

            foreach (DataRow row in rows_to_remove)
            {
                dt.Rows.Remove(row);
                dt.AcceptChanges();
            }
        }

        btnTransfer.Visible = true;
        btnClear.Visible = true;
        btnTran.Enabled = true;
        btnClr.Enabled = true;
        GrdTransfer.Visible = true;

        GrdTransfer.DataSource = dtListedDR;
        GrdTransfer.DataBind();
        ViewState["MoveRecords"] = dtListedDR;

        grdDoctor.Visible = true;
        grdDoctor.DataSource = dt;
        grdDoctor.DataBind();
        ViewState["SelectedRecords"] = dt;


       
    
    }

    protected void chkChemist_Changed(object sender, EventArgs e)
    {
        CheckBox chkChemist = (CheckBox)sender;
        GridViewRow gridRow = (GridViewRow)chkChemist.Parent.Parent;
        Label Chemists_Code = (Label)gridRow.Cells[1].FindControl("Chemists_Code");
        Label lblChemName = (Label)gridRow.Cells[1].FindControl("lblChemName");
        Label lblContact = (Label)gridRow.Cells[1].FindControl("lblContact");
        Label lblterr = (Label)gridRow.Cells[1].FindControl("lblterr");
        if (chkChemist.Checked == true)
        {
            dtChemists = (DataTable)ViewState["MoveChem"];        
               
                DataRow dr = dtChemists.NewRow();
                dr["Chemists_Code"] = Chemists_Code.Text;
                dr["Chemists_Name"] = lblChemName.Text;
                dr["Chemists_Contact"] = lblContact.Text;
                dr["territory_Name"] = lblterr.Text;         
                dr["color"] = "Y";
             
                dtChemists.Rows.InsertAt(dr, 0);
                dtChem = (DataTable)ViewState["SelectedChemist"];


                List<DataRow> rows_to_remove = new List<DataRow>();

                foreach (DataRow rowDR in dtChem.Rows)
                {
                    if (rowDR["Chemists_Code"].ToString().Trim() == Chemists_Code.Text.ToString().Trim())
                    //if (rowDR["plan_no"].ToString().Trim() == lblPlanNo_CopyMove.Text.ToString().Trim())
                    {
                        rows_to_remove.Add(rowDR);
                    }                  
                }

                foreach (DataRow row in rows_to_remove)
                {
                    dtChem.Rows.Remove(row);
                    dtChem.AcceptChanges();
                }
         
                
            
        }
        btnTransfer.Visible = true;
        btnClear.Visible = true;
        btnTran.Enabled = true;
        btnClr.Enabled = true;
        grdChemist.Visible = true;
    
        grdChemist.DataSource = dtChemists;
        grdChemist.DataBind();
        ViewState["MoveChem"] = dtChemists;

        grdChem.Visible = true;
        grdChem.DataSource = dtChem;
        grdChem.DataBind();
        ViewState["SelectedChemist"] = dtChem;

    
    }  

    protected void btnTransfer_Click(object sender, EventArgs e)
    {
        if (rdotransfer.SelectedIndex == 0)
        {
            foreach (GridViewRow gridRow in GrdTransfer.Rows)
            {
                int iReturn = -1;
                //CheckBox chkCopyMove = (CheckBox)gridRow.Cells[1].FindControl("chkCopyMove");
                //CheckBox chkListedDR = (CheckBox)gridRow.Cells[1].FindControl("chkListedDR");
                Label lblDocCode = (Label)gridRow.Cells[1].FindControl("lblDocCode");
                Label lblDocName = (Label)gridRow.Cells[1].FindControl("lblDocName");
                Label lblcat = (Label)gridRow.Cells[1].FindControl("lblcat");
                Label lblSpl = (Label)gridRow.Cells[1].FindControl("lblSpl");
                Label lblterr = (Label)gridRow.Cells[1].FindControl("lblterr");
                Label lblterr_code = (Label)gridRow.Cells[1].FindControl("lblterr_code");

                ListedDR lstDR = new ListedDR();

                if (ViewState["MoveRecords"] != null)
                {
                    iReturn = lstDR.Transfer_Doctor(lblDocCode.Text.ToString(), ddlToTerr.SelectedValue, ddlFromFieldForce.SelectedValue, ddlToFieldForce.SelectedValue);
                }
                else
                {

                }
                if (iReturn > 0)
                {
                    ClientScript.RegisterStartupScript(GetType(), "TestAlert", "alert(\'Transfered Sucessfully.\');", true);
                }

            }
        }
        else if (rdotransfer.SelectedIndex == 1)
        {
            foreach (GridViewRow gridRow in grdChemist.Rows)
            {
                int iReturn = -1;
                //CheckBox chkCopyMove = (CheckBox)gridRow.Cells[1].FindControl("chkCopyMove");
                //CheckBox chkListedDR = (CheckBox)gridRow.Cells[1].FindControl("chkListedDR");
                Label Chemists_Code = (Label)gridRow.Cells[1].FindControl("Chemists_Code");
                Label lblChemName = (Label)gridRow.Cells[1].FindControl("lblChemName");
                Label lblContact = (Label)gridRow.Cells[1].FindControl("lblContact");
                Label lblterr = (Label)gridRow.Cells[1].FindControl("lblterr");

                Chemist chem = new Chemist();

                if (ViewState["MoveChem"] != null)
                {
                    iReturn = chem.Transfer_Chemists(Chemists_Code.Text.ToString(), ddlToTerr.SelectedValue, ddlFromFieldForce.SelectedValue, ddlToFieldForce.SelectedValue);
                }
                else
                {

                }
                if (iReturn > 0)
                {
                    ClientScript.RegisterStartupScript(GetType(), "TestAlert", "alert(\'Transfered Sucessfully.\');", true);
                }

            }
        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        ddlFromFieldForce.SelectedIndex = 0;
        ddlToFieldForce.SelectedIndex = 0;
        ddlFromTerr.SelectedIndex = 0;
        ddlToTerr.SelectedIndex = 0;
        grdDoctor.Visible = false;
        GrdTransfer.Visible = false;
        grdChem.Visible = false;
        grdChemist.Visible = false;
        btnTransfer.Visible = false;
        btnClear.Visible = false;
        lblCount.Text = "";
        
    }
  
}