using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bus_EReport;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Web.Services;

public partial class DoctorBusinessEntry : System.Web.UI.Page
{
    string sfCode = string.Empty;
    DCRBusinessEntry objDCRBusiness = new DCRBusinessEntry();

    Territory objTerritory = new Territory();
    DataSet dsSalesForce = null;
    ListedDR lstDR = new ListedDR();
    DataSet dsTP = null;

    string div_code = string.Empty;

    protected void Page_Init(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        this.BindProducts();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //div_code = Session["div_code"].ToString();
        
        sfCode = Session["sf_code"].ToString();
        if (!Page.IsPostBack)
        {
            this.FillMasterList();
            this.AddDefaultFirstRecord();
            menu.FindControl("btnBack").Visible = false;
            TourPlan tp = new TourPlan();
            dsTP = tp.Get_TP_Edit_Year(div_code);
            if (dsTP.Tables[0].Rows.Count > 0)
            {
                for (int k = Convert.ToInt16(dsTP.Tables[0].Rows[0]["Year"]); k <= DateTime.Now.Year + 1; k++)
                {
                    ddlYear.Items.Add(k.ToString());
                    ddlYear.SelectedValue = DateTime.Now.Year.ToString();
                }
            }

        }

        
    }

    private void FillMasterList()
    {
        SalesForce sf = new SalesForce();
        Doctor objDoctor = new Doctor();

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

    private void AddDefaultFirstRecord()
    {
        //creating dataTable   
        DataTable dt = new DataTable();
        DataRow dr;
        dt.TableName = "DoctorBusiness";
        dt.Columns.Add(new DataColumn("DoctorCode", typeof(string)));
        dt.Columns.Add(new DataColumn("DoctorName", typeof(string)));
        dt.Columns.Add(new DataColumn("Speciality", typeof(string)));
        dt.Columns.Add(new DataColumn("Category", typeof(string)));
        dt.Columns.Add(new DataColumn("Territory", typeof(string)));
        dt.Columns.Add(new DataColumn("ProductsCode", typeof(string)));
        dt.Columns.Add(new DataColumn("Products", typeof(string)));
        dr = dt.NewRow();
        dt.Rows.Add(dr);
        //saving databale into viewstate   
        ViewState["DoctorBus"] = dt;
        //bind Gridview  
        gvDoctorBusiness.DataSource = dt;
        gvDoctorBusiness.DataBind();
    }

    private void AddNewRecordRowToGrid()
    {
        // check view state is not null  
        if (ViewState["DoctorBus"] != null)
        {
            //get datatable from view state   
            DataTable dtCurrentTable = (DataTable)ViewState["DoctorBus"];
            DataRow drCurrentRow = null;
            AutoCompleteExtender1.ContextKey = ddlFieldForce.SelectedValue;

            int docCode = 0;
            if (lstDoctor.SelectedItem.Text != string.Empty)
            {
                docCode = objDCRBusiness.GetDoctorCodeByName(lstDoctor.SelectedItem.Text);
            }

            if (dtCurrentTable.Rows.Count > 0)
            {
                if (hdnRowID.Value != string.Empty)
                {
                    drCurrentRow = dtCurrentTable.Rows[Convert.ToInt32(hdnRowID.Value)];
                    drCurrentRow["DoctorCode"] = docCode;
                    drCurrentRow["DoctorName"] = lstDoctor.SelectedItem.Text;
                    drCurrentRow["Speciality"] = hdnDCRSpeciality.Value;
                    drCurrentRow["Category"] = hdnDCRCategory.Value;
                    drCurrentRow["Territory"] = hdnDCRTerritory.Value;
                    drCurrentRow["ProductsCode"] = hdnProdCode.Value;
                    drCurrentRow["Products"] = txtProducts.Text;
                    dtCurrentTable.AcceptChanges();
                    hdnRowID.Value = string.Empty;
                }
                else
                {
                    for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                    {

                        //add each row into data table  
                        drCurrentRow = dtCurrentTable.NewRow();

                        drCurrentRow["DoctorCode"] = docCode;
                        drCurrentRow["DoctorName"] = lstDoctor.SelectedItem.Text;
                        drCurrentRow["Speciality"] = hdnDCRSpeciality.Value;
                        drCurrentRow["Category"] = hdnDCRCategory.Value;
                        drCurrentRow["Territory"] = hdnDCRTerritory.Value;
                        drCurrentRow["ProductsCode"] = hdnProdCode.Value;
                        drCurrentRow["Products"] = txtProducts.Text;

                    }
                    //Remove initial blank row  
                    if (dtCurrentTable.Rows[0][0].ToString() == "")
                    {
                        dtCurrentTable.Rows[0].Delete();
                        dtCurrentTable.AcceptChanges();

                    }


                    //add created Rows into dataTable  
                    dtCurrentTable.Rows.Add(drCurrentRow);
                }
                //Save Data table into view state after creating each row  
                ViewState["DoctorBus"] = dtCurrentTable;
                //Bind Gridview with latest Row  
                gvDoctorBusiness.DataSource = dtCurrentTable;
                gvDoctorBusiness.DataBind();

            }

        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        this.AddNewRecordRowToGrid();
    }

    private HtmlTableRow CopyTableRow(HtmlTableRow row)
    {

        HtmlTableRow newRow = new HtmlTableRow();
        foreach (HtmlTableCell cell in row.Cells)
        {
            HtmlTableCell tempCell = new HtmlTableCell();

            //var litControls = cell.Controls.OfType<LiteralControl>().Where(ctype => ctype.GetType().Name.ToLower().Contains("literalcontrol"));
            ControlCollection ctrlcol = cell.Controls;
            ControlCollection ctrlcolnew = cell.Controls;

            for (int i = 0; i < ctrlcol.Count; i++)
            {
                var control = ctrlcolnew[i];

                Type ctype = control.GetType();

                if (ctype.Name.ToLower() == "literalcontrol")
                {
                    tempCell.Controls.Add((LiteralControl)control);
                }
                else if (ctype.Name.ToLower() == "htmlinputcheckbox")
                {
                    tempCell.Controls.Add((HtmlInputCheckBox)control);
                }
                else if (ctype.Name.ToLower() == "htmlselect")
                {
                    tempCell.Controls.Add((HtmlSelect)control);
                }
                else if (ctype.Name.ToLower() == "htmlinputtext")
                {
                    tempCell.Controls.Add((HtmlInputText)control);
                }
            }



            //foreach (var ctrl in ctrlcol)
            //{
            //    var controType = ctrl.GetType();
            //    //if (controType.Equals(LiteralControl))
            //    //{
            //        tempCell.Controls.Add((LiteralControl)ctrl);
            //    //}
            //}


            //tempCell.InnerHtml = cell.InnerHtml;
            newRow.Cells.Add(tempCell);
        }
        return newRow;
    }

    protected void gvDoctorBusiness_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;

        if (e.CommandName == "Delete")
        {
            DataTable dt = ViewState["DoctorBus"] as DataTable;
            dt.Rows.Remove(dt.Rows[row.RowIndex]);
            ViewState["DoctorBus"] = dt;
            this.BindGrid();
        }
        else if (e.CommandName == "Edit")
        {

            hdnRowID.Value = Convert.ToString(row.RowIndex);
            HiddenField hdnDoctor = (HiddenField)row.FindControl("hdnDoctor");
            Label lblDoctor = (Label)row.FindControl("lblDoctor");
            //ListBox lstDoctor = (ListBox)row.FindControl("lstDoctor");

            Label lblSpeciality = (Label)row.FindControl("lblSpeciality");
            Label lblCategory = (Label)row.FindControl("lblCategory");
            Label lblTerritory = (Label)row.FindControl("lblTerritory");
            Label lblProducts = (Label)row.FindControl("lblProducts");
            HiddenField hdnProducts = (HiddenField)row.FindControl("hdnProducts");

            //txtDoctor.Text = lblDoctor.Text;
            ListItem lstItemDcr = lstDoctor.Items.FindByText(lblDoctor.Text);
            lstDoctor.SelectedValue = lstItemDcr.Value;
            lblDCRCategory.Text = lblCategory.Text;
            lblDCRSpeciality.Text = lblSpeciality.Text;
            lblDCRTerritory.Text = lblTerritory.Text;
            hdnDCRCategory.Value = lblCategory.Text;
            hdnDCRSpeciality.Value = lblSpeciality.Text;
            hdnDCRTerritory.Value = lblTerritory.Text;
            txtProducts.Text = lblProducts.Text;

            string[] strProducts = hdnProducts.Value.Split(',');
            HtmlTable htmlProducts = (HtmlTable)pnlList.FindControl("tbl");

            for (int oldrowcount = 0; oldrowcount < htmlProducts.Rows.Count; oldrowcount++)
            {
                HtmlTableRow rowprod = htmlProducts.Rows[oldrowcount];
                HtmlInputCheckBox chkoldProduct = (HtmlInputCheckBox)rowprod.FindControl("chkNew" + oldrowcount);
                TextBox txtoldProduct = (TextBox)rowprod.FindControl("txtNew" + oldrowcount);
                chkoldProduct.Checked = false;
                txtoldProduct.Text = string.Empty;
                txtoldProduct.Style.Add("display", "none");
            }

            for (int count = 0; count < strProducts.Length; count++)
            {
                string strProductCode = strProducts[count];
                for (int rowcount = 0; rowcount < htmlProducts.Rows.Count; rowcount++)
                {
                    HtmlTableRow rowprod = htmlProducts.Rows[rowcount];
                    HiddenField hdnProductCodes = (HiddenField)rowprod.FindControl("hdnProductCode" + rowcount);
                    HtmlInputCheckBox chkProduct = (HtmlInputCheckBox)rowprod.FindControl("chkNew" + rowcount);
                    TextBox txtProduct = (TextBox)rowprod.FindControl("txtNew" + rowcount);
                    string[] strProdDet = strProductCode.Split('-');

                    if (strProdDet.Length > 0)
                    {
                        if (strProdDet[0] == hdnProductCodes.Value)
                        {
                            chkProduct.Checked = true;
                            txtProduct.Text = strProdDet[1];
                            txtProduct.Style.Add("display", "block");
                        }
                    }
                }
            }

            //ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowStatus", "javascript:addMultipleRows('" + hdnProducts.Value + "');", true);
        }
    }

    protected void BindGrid()
    {
        DataTable dtDoctor = (DataTable)ViewState["DoctorBus"];


        gvDoctorBusiness.DataSource = dtDoctor;
        gvDoctorBusiness.DataBind();

        if (dtDoctor.Rows.Count == 0)
        {
            this.AddDefaultFirstRecord();
        }
    }
    protected void gvDoctorBusiness_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {


        //GridViewRow row = (sender as LinkButton).NamingContainer as GridViewRow;

        //DataTable dt = ViewState["DoctorBus"] as DataTable;
        //dt.Rows.Remove(dt.Rows[row.RowIndex]);
        //ViewState["DoctorBus"] = dt;
        //this.BindGrid();
    }
    protected void gvDoctorBusiness_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        this.SaveDoctorBusiness("Submit");
        ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowStatus", "javascript:alert('Edit Option Restricted - Kindly Contact Admin!!');", true);
    }

    private void SaveDoctorBusiness(string strSaveSubmit)
    {
        string activeFlag = string.Empty;
        if (strSaveSubmit == "Save")
        {
            activeFlag = "0";
        }
        else
        {
            activeFlag = "1";
        }

        string result = string.Empty;

        if (hdnTransNo.Value != string.Empty)
        {
            result = hdnTransNo.Value;
            objDCRBusiness.RecordUpdate_BusinessEntryHead(hdnTransNo.Value);
            objDCRBusiness.RecordDelete_BusinessEntryDetails(hdnTransNo.Value);
        }
        else
        {
            result = objDCRBusiness.RecordHeadAdd(ddlFieldForce.SelectedValue, div_code, ddlMonth.SelectedValue, ddlYear.SelectedValue, activeFlag);
        }

        if (result != "0")
        {
            if (activeFlag == "1")
            {
                objDCRBusiness.RecordUpdate_BusinessStatus(result, activeFlag);
            }

            for (int rowcnt = 0; rowcnt < gvDoctorBusiness.Rows.Count; rowcnt++)
            {
                GridViewRow gvrow = gvDoctorBusiness.Rows[rowcnt];

                HiddenField hdnDoctor = (HiddenField)gvrow.FindControl("hdnDoctor");
                HiddenField hdnSpeciality = (HiddenField)gvrow.FindControl("hdnSpeciality");
                HiddenField hdnCategory = (HiddenField)gvrow.FindControl("hdnCategory");
                HiddenField hdnTerritory = (HiddenField)gvrow.FindControl("hdnTerritory");
                HiddenField hdnProducts = (HiddenField)gvrow.FindControl("hdnProducts");
                string[] strProdArray = hdnProducts.Value.Split(',');
                foreach (var prod in strProdArray)
                {
                    string[] strProd = prod.Split('-');

                    objDCRBusiness.RecordDetailsAdd(result, ddlFieldForce.SelectedValue, div_code, hdnDoctor.Value, strProd[0], Convert.ToInt32(strProd[1]));
                }
            }

            hdnTransNo.Value = string.Empty;
            this.ClearControls();
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        this.SaveDoctorBusiness("Save");
    }

    private void ClearControls()
    {
        ddlFieldForce.SelectedIndex = 0;
        ddlYear.SelectedIndex = 0;
        ddlMonth.SelectedIndex = 0;
        this.ClearProductControls();
        this.AddDefaultFirstRecord();
    }

    private void ClearProductControls()
    {
        lstDoctor.SelectedIndex = -1;
        txtDoctor.Text = string.Empty;
        lblDCRCategory.Text = string.Empty;
        lblDCRSpeciality.Text = string.Empty;
        lblDCRTerritory.Text = string.Empty;
        txtProducts.Text = string.Empty;
        this.BindProducts();
    }

    private void BindExistingRows()
    {
        //creating dataTable   
        DataSet dsBusiness = new DataSet();
        dsBusiness = objDCRBusiness.GetDCRExistingBusiness(ddlFieldForce.SelectedValue, ddlYear.SelectedValue, ddlMonth.SelectedValue);
        DataTable dtBusiness = dsBusiness.Tables[0];
        if (dtBusiness.Rows.Count > 0)
        {
            string strActive = Convert.ToString(dtBusiness.Rows[0]["Active"]);
            hdnTransNo.Value = Convert.ToString(dtBusiness.Rows[0]["Trans_sl_No"]);
            //saving databale into viewstate   
            ViewState["DoctorBus"] = dtBusiness;
            //bind Gridview  
            gvDoctorBusiness.DataSource = dtBusiness;
            gvDoctorBusiness.DataBind();

            if (strActive == "0")
            {
                btnSave.Visible = true;
                btnSubmit.Visible = true;
                gvDoctorBusiness.Columns[5].Visible = true;
            }
            else
            {
                btnSave.Visible = false;
                btnSubmit.Visible = false;
                gvDoctorBusiness.Columns[5].Visible = false;
            }
        }
        else
        {
            btnSave.Visible = true;
            btnSubmit.Visible = true;
            gvDoctorBusiness.Columns[5].Visible = true;
            hdnTransNo.Value = string.Empty;
            this.AddDefaultFirstRecord();
        }
    }
    protected void btnGo_Click(object sender, EventArgs e)
    {
        this.BindDoctor();
        this.BindExistingRows();
    }

    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> GetDoctorList(string prefixText, int count, string contextKey)
    {
        DataSet dsListedDR = new DataSet();
        ListedDR lstDR = new ListedDR();
        List<string> doctorList = new List<string>();
        DataTable _objdt = new DataTable();
        dsListedDR = lstDR.GetDoctorBySearch(prefixText, contextKey);
        if (dsListedDR.Tables[0].Rows.Count > 0)
        {
            for (int i = 0; i < dsListedDR.Tables[0].Rows.Count; i++)
            {
                doctorList.Add(dsListedDR.Tables[0].Rows[i]["ListedDr_Name"].ToString());
            }
        }

        // Find All Matching Products
        //var list = from p in doctorList
        //           where p.Contains(prefixText)
        //           select p;

        ////Convert to Array as We need to return Array
        //string[] prefixTextArray = doctorList.ToArray<string>();

        //Return Selected doctors
        return doctorList;
    }


    protected void ddlFieldForce_SelectedIndexChanged(object sender, EventArgs e)
    {
        //AutoCompleteExtender1.ContextKey = ddlFieldForce.SelectedValue;
        //BindDoctor();
    }

    protected void hdnValue_ValueChanged(object sender, EventArgs e)
    {
        string strSelectedDoctor = ((HiddenField)sender).Value;

        if (strSelectedDoctor != string.Empty)
        {
            DataSet dsDoctor = lstDR.getDoctorDetailsByName(div_code, AutoCompleteExtender1.ContextKey, strSelectedDoctor);
            if (dsDoctor.Tables.Count > 0)
            {
                DataTable dtDoctor = dsDoctor.Tables[0];
                if (dtDoctor.Rows.Count > 0)
                {
                    DataRow drDoctor = dtDoctor.Rows[0];

                    lblDCRCategory.Text = Convert.ToString(drDoctor["Doc_Cat_Name"]);
                    lblDCRSpeciality.Text = Convert.ToString(drDoctor["Doc_Special_Name"]);
                    lblDCRTerritory.Text = Convert.ToString(drDoctor["Territory_Name"]);
                }
            }
        }
        else
        {
            lblDCRCategory.Text = string.Empty;
            lblDCRSpeciality.Text = string.Empty;
            lblDCRTerritory.Text = string.Empty;
        }
    }
    protected void btnProdGo_Click(object sender, EventArgs e)
    {
        this.AddNewRecordRowToGrid();
        this.ClearProductControls();
    }

    protected void btnDoctor_Click(object sender, EventArgs e)
    {
        string strSelectedDoctor = string.Empty;

        foreach (ListItem itm in lstDoctor.Items)
        {
            if (itm.Selected)
            {
                strSelectedDoctor = itm.Value;
                //itm.Selected = false;
            }
        }

        if (strSelectedDoctor != string.Empty)
        {
            DataSet dsDoctor = lstDR.getDoctorDetailsByName(div_code, ddlFieldForce.SelectedValue, strSelectedDoctor);
            if (dsDoctor.Tables.Count > 0)
            {
                DataTable dtDoctor = dsDoctor.Tables[0];
                if (dtDoctor.Rows.Count > 0)
                {
                    DataRow drDoctor = dtDoctor.Rows[0];

                    lblDCRCategory.Text = Convert.ToString(drDoctor["Doc_Cat_Name"]);
                    lblDCRSpeciality.Text = Convert.ToString(drDoctor["Doc_Special_Name"]);
                    lblDCRTerritory.Text = Convert.ToString(drDoctor["Territory_Name"]);
                }
            }
        }
        else
        {
            lblDCRCategory.Text = string.Empty;
            lblDCRSpeciality.Text = string.Empty;
            lblDCRTerritory.Text = string.Empty;
        }
    }

    private void BindProducts()
    {
        pnlList.Controls.Clear();

        Product objProduct = new Product();
        DataSet dsProducts = null;

        dsProducts = objProduct.getProdall(div_code);
        if (dsProducts.Tables.Count > 0)
        {
            if (dsProducts.Tables[0].Rows.Count > 0)
            {
                //ddlProducts.DataTextField = "Product_Detail_Name";
                //ddlProducts.DataValueField = "Product_Detail_Code";
                //ddlProducts.DataSource = dsProducts;
                //ddlProducts.DataBind();
                DataTable dtProducts = dsProducts.Tables[0];


                TextBox txt;
                HtmlInputCheckBox hck;
                Label lbl;
                HiddenField hdnProductCode;
                HtmlTable htmltbl = new HtmlTable();
                HtmlTableRow row;
                HtmlTableCell cell;
                HtmlTableCell cell1;
                HtmlTableCell cell2;
                string prodtext;
                string prodvalue;
                for (int i = 0; i < dtProducts.Rows.Count; i++)
                {
                    DataRow drProduct = dtProducts.Rows[i];
                    prodtext = Convert.ToString(drProduct["Product_Detail_Name"]);
                    prodvalue = Convert.ToString(drProduct["Product_Detail_Code"]);

                    txt = new TextBox();
                    hck = new HtmlInputCheckBox();
                    lbl = new Label();
                    hdnProductCode = new HiddenField();

                    txt.ID = "txtNew" + i.ToString();
                    txt.Width = Unit.Pixel(40);
                    txt.Style.Add("display", "none");
                    txt.Attributes.Add("onchange", "ControlVisibility(" + i.ToString() + ");");

                    lbl.Text = prodtext;
                    hdnProductCode.ID = "hdnProductCode" + i.ToString();
                    hdnProductCode.Value = prodvalue;
                    htmltbl.ID = "tbl";

                    row = new HtmlTableRow();
                    cell = new HtmlTableCell();
                    cell1 = new HtmlTableCell();
                    cell2 = new HtmlTableCell();
                    hck.ID = "chkNew" + i.ToString();
                    hck.Attributes.Add("onclick", "ControlVisibility(" + i.ToString() + ");");

                    cell.Controls.Add(hck);
                    cell1.Controls.Add(lbl);
                    cell2.Controls.Add(txt);
                    cell2.Controls.Add(hdnProductCode);
                    cell1.Align = "left";
                    //cell.Style.Add("white-space", "nowrap");
                    //row.Style.Add("white-space", "nowrap");
                    row.Controls.Add(cell);
                    row.Controls.Add(cell1);
                    row.Controls.Add(cell2);
                    htmltbl.Controls.Add(row);

                    pnlList.Controls.Add(htmltbl);

                }
            }
        }
    }

    private void BindDoctor()
    {
        DataSet dsListedDR = new DataSet();
        ListedDR lstDR = new ListedDR();
        List<string> doctorList = new List<string>();
        DataTable _objdt = new DataTable();
        dsListedDR = lstDR.getListedDoctorBySfCode(ddlFieldForce.SelectedValue);
        if (dsListedDR.Tables[0].Rows.Count > 0)
        {
            lstDoctor.DataTextField = "ListedDr_Name";
            lstDoctor.DataValueField = "DoctorDetails";
            lstDoctor.DataSource = dsListedDR;
            lstDoctor.DataBind();

            //for (int i = 0; i < dsListedDR.Tables[0].Rows.Count; i++)
            //{
            //    doctorList.Add(dsListedDR.Tables[0].Rows[i]["ListedDr_Name"].ToString());
            //}
        }
        else
        {
            lstDoctor.Items.Clear();
        }
    }
   
}