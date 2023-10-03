using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bus_EReport;
using System.Web.UI.HtmlControls;

public partial class InputDespatchHQ : System.Web.UI.Page
{
    DataSet dsSalesForce = null;
    string div_code = string.Empty;
    string sfCode = string.Empty;
    InputDespatch objInput = new InputDespatch();
    SalesForce sf = new SalesForce();
    Product objProduct = new Product();
    DataSet dsTP = null;

        protected void Page_Init(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        this.BindInputs();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        sfCode = Session["sf_code"].ToString();
        if (!Page.IsPostBack)
        {
            this.FillMasterList();
            this.BindAllInputs();
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

        dsSalesForce = sf.UserList_Hierarchy_Managers(div_code, sfCode);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            lstFieldForce.DataTextField = "sf_name";
            lstFieldForce.DataValueField = "sf_code";
            lstFieldForce.DataSource = dsSalesForce;
            lstFieldForce.DataBind();
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
                string output = objInput.RecordHeadAdd(baseitem.Value, div_code, ddlMonth.SelectedValue, ddlYear.SelectedValue);
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
                                objInput.RecordDetailsAdd(output, baseitem.Value, div_code, strProdAllValue[0], Convert.ToInt32(strProdAllValue[2]));
                            }
                        }
                        else
                        {
                            string[] strProdSingle = strProdArray[j].Split('-');
                            if (strProdSingle.Length > 1)
                            {
                                if (strProdSingle[1] != string.Empty)
                                {
                                    objInput.RecordDetailsAdd(output, baseitem.Value, div_code, strProdSingle[0], Convert.ToInt32(strProdSingle[1]));
                                }
                            }
                        }
                    }
                }
            }
        }

        

        this.ClearControls();
        Page.ClientScript.RegisterStartupScript(this.GetType(), "SuccessMsg", "alert('Input Despatch HQ Saved Successfully!');", true);

    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        this.AddNewRecordRowToGrid();
    }
    
    protected void btnGo_Click(object sender, EventArgs e)
    {
        if (rdoInput.SelectedValue == "All")
        {
            gvSampleProducts.Visible = true;
            tblProducts.Visible = false;
            this.BindAllInputs();
        }
        else
        {
            gvSampleProducts.Visible = false;
            tblProducts.Visible = true;
        }

        btnSave.Visible = true;
    }

    private void BindAllInputs()
    {
        DataSet dsInputs = null;
        dsInputs = objProduct.getGift(div_code);
        if (dsInputs.Tables.Count > 0)
        {
            if (dsInputs.Tables[0].Rows.Count > 0)
            {
                gvSampleProducts.DataSource = dsInputs;
                gvSampleProducts.DataBind();
            }
        }
    }

    protected void rdoInput_SelectedIndexChanged(object sender, EventArgs e)
    {
       
    }
   
    private void ClearControls()
    {
        //lstFieldForce.SelectedIndex = -1;
        lstFieldForce.SelectedIndex = -1;
        lstBaseLevel.Items.Clear();
        ddlYear.SelectedIndex = 0;
        ddlMonth.SelectedIndex = 0;
        rdoInput.SelectedIndex = 0;
        gvSampleProducts.Visible = false;
        tblProducts.Visible = false;
        btnSave.Visible = false;
        //divInputs.Visible = false;
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

    private void BindInputs()
    {
        pnlList.Controls.Clear();

        Product objProduct = new Product();
        DataSet dsInputs = null;

        dsInputs = objProduct.getGift(div_code);
        if (dsInputs.Tables.Count > 0)
        {
            if (dsInputs.Tables[0].Rows.Count > 0)
            {
                //ddlProducts.DataTextField = "Product_Detail_Name";
                //ddlProducts.DataValueField = "Product_Detail_Code";
                //ddlProducts.DataSource = dsProducts;
                //ddlProducts.DataBind();
                DataTable dtInputs = dsInputs.Tables[0];


                TextBox txt;
                HtmlInputCheckBox hck;
                Label lbl;
                HiddenField hdnProductCode;
                HiddenField hdnGiftType;
                HtmlTable htmltbl = new HtmlTable();
                HtmlTableRow row;
                HtmlTableCell cell;
                HtmlTableCell cell1;
                HtmlTableCell cell2;
                string prodtext;
                string prodvalue;
                string strGiftType;
                for (int i = 0; i < dtInputs.Rows.Count; i++)
                {
                    DataRow drProduct = dtInputs.Rows[i];
                    prodtext = Convert.ToString(drProduct["Gift_Name"]);
                    prodvalue = Convert.ToString(drProduct["Gift_Code"]);
                    strGiftType = Convert.ToString(drProduct["Gift_Type"]);

                    txt = new TextBox();
                    hck = new HtmlInputCheckBox();
                    lbl = new Label();
                    hdnProductCode = new HiddenField();
                    hdnGiftType = new HiddenField();

                    txt.ID = "txtNew" + i.ToString();
                    txt.Width = Unit.Pixel(40);
                    txt.Style.Add("display", "none");
                    txt.Attributes.Add("onchange", "ControlVisibility(" + i.ToString() + ");");

                    lbl.Text = prodtext;
                    hdnProductCode.ID = "hdnProductCode" + i.ToString();
                    hdnProductCode.Value = prodvalue;

                    hdnGiftType.ID = "hdnGiftType" + i.ToString();
                    hdnGiftType.Value = strGiftType;
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
                    cell2.Controls.Add(hdnGiftType);
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