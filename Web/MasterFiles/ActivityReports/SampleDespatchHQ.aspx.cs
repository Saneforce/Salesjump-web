using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bus_EReport;
using System.Web.UI.HtmlControls;

public partial class SampleDespatchHQ : System.Web.UI.Page
{
    DataSet dsSalesForce = null;
    string div_code = string.Empty;
    string sfCode = string.Empty;
    SampleDespatch objSample = new SampleDespatch();
    SalesForce sf = new SalesForce();
    Product objProduct = new Product();
    DataSet dsTP = null;

    protected void Page_Init(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        this.BindProducts();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        sfCode = Session["sf_code"].ToString();
        if (!Page.IsPostBack)
        {
            this.FillMasterList();
            this.BindAllProducts();
            //this.AddDefaultFirstRecord();
            //menu.FindControl("btnBack").Visible = false;
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
        

        DataSet dsProducts = null;

        dsSalesForce = sf.UserList_Hierarchy_Managers(div_code, sfCode);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            lstFieldForce.DataTextField = "sf_name";
            lstFieldForce.DataValueField = "sf_code";
            lstFieldForce.DataSource = dsSalesForce;
            lstFieldForce.DataBind();
        }

        dsProducts = objProduct.getProdall(div_code);
        if (dsProducts.Tables.Count > 0)
        {
            if (dsProducts.Tables[0].Rows.Count > 0)
            {
                //ddlProducts.DataTextField = "Product_Detail_Name";
                //ddlProducts.DataValueField = "Product_Detail_Code";
                //ddlProducts.DataSource = dsProducts;
                //ddlProducts.DataBind();
            }
        }
    }

    private void BindBaseLevelByFieldForce(string strFieldForceID)
    {
        DataSet dsBaseLevel = new DataSet();
        dsBaseLevel = sf.UserList_getAll_Multiple(div_code, strFieldForceID);
        if (dsBaseLevel.Tables[0].Rows.Count > 0)
        {
            lstBaseLevel.DataTextField = "sf_name";
            lstBaseLevel.DataValueField = "sf_code";
            lstBaseLevel.DataSource = dsBaseLevel;
            lstBaseLevel.DataBind();
        }
    }

    private void AddNewRecordRowToGrid()
    {
        string strBaseFieldForceID = "";

        foreach (ListItem baseitem in lstBaseLevel.Items)
        {
            if (baseitem.Selected)
            {
                //strBaseFieldForceID += baseitem.Value + ",";
                string output = objSample.RecordHeadAdd(baseitem.Value, div_code, ddlMonth.SelectedValue, ddlYear.SelectedValue);
                if (output != "0")
                {
                    string[] strProdArray = hdnProdCode.Value.Split(',');
                    for (int j = 0; j < strProdArray.Length; j++)
                    {
                        string[] strProdAllValue = strProdArray[j].Split('|');
                        if (strProdAllValue.Length > 2)
                        {
                            if (strProdAllValue[2] != string.Empty)
                            {
                                objSample.RecordDetailsAdd(output, baseitem.Value, div_code, strProdAllValue[0], Convert.ToInt32(strProdAllValue[2]));
                            }
                        }
                        else
                        {
                            string[] strProdSingle = strProdArray[j].Split('-');
                            if (strProdSingle.Length > 1)
                            {
                                if (strProdSingle[1] != string.Empty)
                                {
                                    objSample.RecordDetailsAdd(output, baseitem.Value, div_code, strProdSingle[0], Convert.ToInt32(strProdSingle[1]));
                                }
                            }
                        }
                    }
                }
            }
        }

        

        this.ClearControls();
        Page.ClientScript.RegisterStartupScript(this.GetType(), "SuccessMsg", "alert('Sample Despatch HQ Saved Successfully!');", true);

    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        this.AddNewRecordRowToGrid();
    }


    protected void gvSampleDespatch_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void gvSampleDespatch_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        if (e.CommandName == "Delete")
        {
            GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;

            DataTable dt = ViewState["SampleDes"] as DataTable;
            dt.Rows.Remove(dt.Rows[row.RowIndex]);
            ViewState["SampleDes"] = dt;
            this.BindGrid();
        }
    }

    protected void BindGrid()
    {
        DataTable dtSample = (DataTable)ViewState["SampleDes"];


        gvSampleDespatch.DataSource = dtSample;
        gvSampleDespatch.DataBind();

        //if (dtSample.Rows.Count == 0)
        //{
        //    this.AddDefaultFirstRecord();
        //}
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
        gvSampleDespatch.DataSource = dt;
        gvSampleDespatch.DataBind();
    }
    protected void gvSampleDespatch_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DropDownList ddlProducts = (e.Row.FindControl("ddlProducts") as DropDownList);

            DataSet dsProducts = null;

            dsProducts = objProduct.getProdall(div_code);
            if (dsProducts.Tables.Count > 0)
            {
                if (dsProducts.Tables[0].Rows.Count > 0)
                {
                    ddlProducts.DataTextField = "Product_Detail_Name";
                    ddlProducts.DataValueField = "Product_Detail_Code";
                    ddlProducts.DataSource = dsProducts;
                    ddlProducts.DataBind();
                }
            }
        }
    }
    protected void btnGo_Click(object sender, EventArgs e)
    {
        if (rdoProduct.SelectedValue == "All")
        {
            gvSampleProducts.Visible = true;
            tblProducts.Visible = false;
            this.BindAllProducts();
        }
        else
        {
            gvSampleProducts.Visible = false;
            tblProducts.Visible = true;
        }

        btnSave.Visible = true;
    }

    private void BindAllProducts()
    {
        DataSet dsProducts = null;
        dsProducts = objProduct.getProdall(div_code);
        if (dsProducts.Tables.Count > 0)
        {
            if (dsProducts.Tables[0].Rows.Count > 0)
            {

                gvSampleProducts.DataSource = dsProducts;
                gvSampleProducts.DataBind();

                //string strProductCodes = string.Empty;
                //for (int cnt = 0; cnt < dsProducts.Tables[0].Rows.Count; cnt++)
                //{
                //    if (strProductCodes != string.Empty)
                //    {
                //        strProductCodes = strProductCodes + "," + Convert.ToString(dsProducts.Tables[0].Rows[cnt]["Product_Detail_Code"]);
                //    }
                //    else
                //    {
                //        strProductCodes = Convert.ToString(dsProducts.Tables[0].Rows[cnt]["Product_Detail_Code"]);
                //    }
                //}

                //hdnProducts.Value = strProductCodes;
            }
        }
    }

    protected void rdoProduct_SelectedIndexChanged(object sender, EventArgs e)
    {
       
    }
   
    private void ClearControls()
    {
        //lstFieldForce.SelectedIndex = -1;
        lstFieldForce.SelectedIndex = -1;
        lstBaseLevel.Items.Clear();
        ddlYear.SelectedIndex = 0;
        ddlMonth.SelectedIndex = 0;
        rdoProduct.SelectedIndex = 0;
        gvSampleProducts.Visible = false;
        tblProducts.Visible = false;
        btnSave.Visible = false;
        //divProducts.Visible = false;
    }

    protected void Submit(object sender, EventArgs e)
    {
        string strFieldForceID = "";

        foreach(ListItem itm in lstFieldForce.Items)
        {
            if (itm.Selected)
            {
                strFieldForceID += itm.Value + ",";
            }
        }

        //foreach (ListItem item in lstFieldForce.Items)
        //{
        //    if (item.Selected)
        //    {
        //        strFieldForceID += item.Value + ",";
        //    }
        //}

        this.BindBaseLevelByFieldForce(strFieldForceID);
        //ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "alert('" + strFieldID + "');", true);
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
                HiddenField hdnPack;
                HtmlTable htmltbl = new HtmlTable();
                HtmlTableRow row;
                HtmlTableCell cell;
                HtmlTableCell cell1;
                HtmlTableCell cell2;
                string prodtext;
                string prodvalue;
                string pack;
                for (int i = 0; i < dtProducts.Rows.Count; i++)
                {
                    DataRow drProduct = dtProducts.Rows[i];
                    prodtext = Convert.ToString(drProduct["Product_Detail_Name"]);
                    prodvalue = Convert.ToString(drProduct["Product_Detail_Code"]);
                    pack = Convert.ToString(drProduct["Product_Sale_Unit"]);

                    txt = new TextBox();
                    hck = new HtmlInputCheckBox();
                    lbl = new Label();
                    hdnProductCode = new HiddenField();
                    hdnPack = new HiddenField();

                    txt.ID = "txtNew" + i.ToString();
                    txt.Width = Unit.Pixel(40);
                    txt.Style.Add("display", "none");
                    txt.Attributes.Add("onchange", "ControlVisibility(" + i.ToString() + ");");

                    lbl.Text = prodtext;
                    hdnProductCode.ID = "hdnProductCode" + i.ToString();
                    hdnProductCode.Value = prodvalue;

                    hdnPack.ID = "hdnPack" + i.ToString();
                    hdnPack.Value = pack;
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
                    cell2.Controls.Add(hdnPack);
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
}