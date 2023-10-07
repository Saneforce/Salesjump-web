using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;


public partial class MasterFiles_ProductBulkCreate : System.Web.UI.Page
{
    #region "Declaration"
    DataSet dsProd = null;
    DataSet dsSubDivision = null;
    DataSet dsState = null;
    DataSet dsDivision = null;
    string[] statecd;
    string state_cd = string.Empty;
    string sState = string.Empty;
    string state_code = string.Empty;
    string div_code = string.Empty;
    string ProdCode = string.Empty;
    string ProdSaleUnit = string.Empty;
    string ProdName = string.Empty;
    int i;
    int iReturn = -1;
    int iCnt = -1;
    int iIndex;
    string subdivision_code = string.Empty;
    string sub_division = string.Empty;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int time;
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {

        div_code = Session["div_code"].ToString();
        
        if (!Page.IsPostBack)
        {
            Session["backurl"] = "ProductList.aspx";
            //menu1.Title = this.Page.Title;
            FillProd();
            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);
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
    protected DataSet FillCategory()
    {
        Product prd = new Product();
        dsProd = prd.getProductCategory(div_code);
        return dsProd;
    }

    protected DataSet FillGroup()
    {
        Product prd = new Product();
        dsProd = prd.FillProductGroup(div_code);
        return dsProd;
    }
    protected DataSet FillBrand()
    {
        Product prd = new Product();
        dsProd = prd.FillProductBrand(div_code);
        return dsProd;
    }
    protected DataSet FillUOM()
    {
        Product prd = new Product();
        dsProd = prd.FillUOM(div_code);
        return dsProd;
    }
      
    private void FillProd()
    {
        Product dv = new Product();
        iCnt = dv.RecordCount(div_code);
        ViewState["iCnt"] = iCnt.ToString();
        dsProd = dv.getEmptyProd();
        if (dsProd.Tables[0].Rows.Count > 0)
        {
            grdProduct.Visible = true;
            grdProduct.DataSource = dsProd;
            grdProduct.DataBind();

        }
    }


    protected void btnSave_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        int iReturn = -1;
        foreach (GridViewRow gridRow in grdProduct.Rows)
        {
            string StateId = "";
            string SubId = "";
            TextBox txtProdCode = (TextBox)gridRow.Cells[1].FindControl("Product_Detail_Code");
            string sProdCode = txtProdCode.Text.ToString();
            TextBox txtProdName = (TextBox)gridRow.Cells[1].FindControl("Product_Detail_Name");
            string sProdName = txtProdName.Text.ToString();
            TextBox txtProdDesc = (TextBox)gridRow.Cells[1].FindControl("Product_Description");
            string sProdDesc = txtProdDesc.Text.ToString();
            DropDownList txtSaleUnit1 = (DropDownList)gridRow.Cells[1].FindControl("Product_Base_uom");
            string sSaleUnit1 = txtSaleUnit1.SelectedItem.ToString();
            DropDownList txtSaleUnit2 = (DropDownList)gridRow.Cells[1].FindControl("Product_Base_uom");
            string sSaleUnit2 = txtSaleUnit2.SelectedValue.ToString();
            DropDownList txtSaleUnit3 = (DropDownList)gridRow.Cells[1].FindControl("Product_uom");
            string sSaleUnit3 = txtSaleUnit3.SelectedItem.ToString();
            DropDownList txtSaleUnit4 = (DropDownList)gridRow.Cells[1].FindControl("Product_uom");
            string sSaleUnit4 = txtSaleUnit4.SelectedValue.ToString();
            TextBox txtUOM = (TextBox)gridRow.Cells[1].FindControl("UOM_Value");
            string stxtuom = txtUOM.Text.ToString();
            DropDownList ddlGroup = (DropDownList)gridRow.Cells[1].FindControl("Product_Group");
            string sGroup = ddlGroup.SelectedValue.ToString();
            DropDownList ddlCatg = (DropDownList)gridRow.Cells[1].FindControl("Product_Cat_Code");
            string sCatg = ddlCatg.SelectedValue.ToString();
            DropDownList ddlBrand = (DropDownList)gridRow.Cells[1].FindControl("Product_Brd_Code");
            string sBrand = ddlBrand.SelectedValue.ToString();

            HiddenField hdnStateId = (HiddenField)gridRow.Cells[1].FindControl("hdnStateId");
            CheckBoxList chkst = (CheckBoxList)grdProduct.Rows[0].FindControl("chkstate");
            for (int i = 0; i < chkst.Items.Count; i++)
            {
                if (chkst.Items[i].Selected)
                {
                    if (chkst.Items[i].Text != "ALL")
                    {
                        //name1 += chkst.Items[i].Text + ",";
                        StateId += chkst.Items[i].Value + ",";
                    }
                }
            }
            string stateId = StateId;

            HiddenField hdnSubDivisionId = (HiddenField)gridRow.Cells[1].FindControl("hdnSubDivisionId");
            CheckBoxList check = (CheckBoxList)grdProduct.Rows[0].FindControl("CheckBoxList1");
            for (int i = 0; i < check.Items.Count; i++)
            {
                if (check.Items[i].Selected)
                {
                    if (check.Items[i].Text != "ALL")
                    {
                        //name1 += chkst.Items[i].Text + ",";
                        SubId += check.Items[i].Value + ",";
                    }
                    
                }
            }
            string subDivID = SubId;

            if (sProdCode.Trim().Length > 0 && sProdName.Trim().Length > 0 && sProdDesc.Trim().Length > 0 && sSaleUnit1.Trim().Length > 0 && sSaleUnit3.Trim().Length > 0 && sCatg.Trim().Length > 0 && sGroup.Trim().Length > 0 && sBrand.Trim().Length > 0)
            {
                // Add New Product            
                Product prd = new Product();
                iReturn = prd.RecordBulkAdd(sProdCode, sProdName,sProdDesc, sSaleUnit1, sSaleUnit2, sSaleUnit3, sSaleUnit4, stxtuom, Convert.ToInt32(sCatg), Convert.ToInt32(sGroup), stateId, subDivID, Convert.ToInt32(Session["div_code"].ToString()), Convert.ToInt32(sBrand));
                if (iReturn > 0)
                {
                    // menu1.Status = "Product Details Created Successfully!!";
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Created Successfully');</script>");
                    FillProd();
                }
                if (iReturn == -2)
                {
                    //  menu1.Status = "Product exist with the same short name!!";
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Product Name Already Exist ');</script>");
                    txtProdName.Focus();

                }
                if (iReturn == -3)
                {
                    //  menu1.Status = "Product exist with the same short name!!";
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Product Code Already Exist');</script>");
                    txtProdCode.Focus();

                }
            }
            else
            {
                // menu1.Status = "Enter all the values!!";
               // ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Enter all the values');</script>");
            }
        }

    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        FillProd();
    }
    protected void grdProduct_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "this.className='highlight_clr'");
            e.Row.Attributes.Add("onmouseout", "this.className='normal'");
        }
    }
    //Changes done by Priya
    protected void grdProduct_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblSNo = (Label)e.Row.FindControl("lblSNo");
            lblSNo.Text = Convert.ToString(Convert.ToInt32(lblSNo.Text) + Convert.ToInt32(ViewState["iCnt"].ToString()));
        }
       

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            CheckBoxList check = (CheckBoxList)e.Row.FindControl("CheckBoxList1");                     
            TextBox txtSubDivision = (TextBox)e.Row.FindControl("TextBox1");
            HiddenField hdnSubDivisionId = (HiddenField)e.Row.FindControl("hdnSubDivisionId");
            SubDivision dv = new SubDivision();
            dsSubDivision = dv.getSubDiv_Create(div_code);
            check.DataTextField = "subdivision_name";
            check.DataValueField = "subdivision_code";
            check.DataSource = dsSubDivision;
            check.DataBind();
            string[] subdiv;
            //if (subdivision_code != "")
            //{
            //    iIndex = -1;
            //    subdiv = subdivision_code.Split(',');
            //    foreach (string st in subdiv)
            //    {
                    for (iIndex = 0; iIndex < check.Items.Count; iIndex++)
                    {
                        //if (st == check.Items[iIndex].Value)
                        //{
                            check.Items[iIndex].Selected = true;
                           // check.Items[iIndex].Attributes.Add("style", "Color: #8A2BE2;font-weight:Bold");

                        //}
                    }
             //   }
          //  }

            CheckBoxList chkst = (CheckBoxList)e.Row.FindControl("chkstate");
            TextBox txtstate = (TextBox)e.Row.FindControl("txtstate");
            HiddenField hdnStateId = (HiddenField)e.Row.FindControl("hdnStateId");
            Division dv1 = new Division();
            dsDivision = dv1.getStatePerDivision(div_code);
            if (dsDivision.Tables[0].Rows.Count > 0)
            {
                int i = 0;
                state_cd = string.Empty;
                sState = dsDivision.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                statecd = sState.Split(',');
                foreach (string st_cd in statecd)
                {
                    if (i == 0)
                    {
                        state_cd = state_cd + st_cd;
                    }
                    else
                    {
                        if (st_cd.Trim().Length > 0)
                        {
                            state_cd = state_cd + "," + st_cd;
                        }
                    }
                    i++;
                }

                State st1 = new State();
                dsState = st1.getStateAddChkBox(state_cd);
                chkst.DataTextField = "statename";
                chkst.DataValueField = "state_code";
                chkst.DataSource = dsState;
                chkst.DataBind();
            }

            

            string[] state;
            if (state_cd != "")
            {
                iIndex = -1;
                state = state_cd.Split(',');
                foreach (string st1 in state)
                {
                    for (iIndex = 0; iIndex < chkst.Items.Count; iIndex++)
                    {
                        //if (st == chkst.Items[iIndex].Value)
                        //{
                            chkst.Items[iIndex].Selected = true;
                            //chkst.Items[iIndex].Attributes.Add("style", "Color: Blue; font-weight:Bold ");

                        //}
                    }
                }
            }
        }

        

    }
    protected void Chkstate_SelectedIndexChanged(object sender, EventArgs e)
    {
        string name1 = "";
        string id1 = "";
        GridViewRow gv1 = (GridViewRow)((Control)sender).NamingContainer;
        CheckBoxList chkst = (CheckBoxList)gv1.FindControl("chkstate");
        TextBox txtstate = (TextBox)gv1.FindControl("txtstate");
        HiddenField hdnStateId = (HiddenField)gv1.FindControl("hdnStateId");
        txtstate.Text = "";
        hdnStateId.Value = "";

        if (chkst.Items[0].Text == "ALL" && chkst.Items[0].Selected == true)
        {
            for (int i = 0; i < chkst.Items.Count; i++)
            {

                chkst.Items[i].Selected = true;

            }
        }

        int countSelected = chkst.Items.Cast<ListItem>().Where(i => i.Selected).Count();
        if (countSelected == chkst.Items.Count-1)
        {
            for (int i = 0; i < chkst.Items.Count; i++)
            {

                chkst.Items[i].Selected = false;
                
            }

        }        

        for (int i = 0; i < chkst.Items.Count; i++)
        {
            if (chkst.Items[i].Selected)
            {
                if (chkst.Items[i].Text != "ALL")
                {
                    name1 += chkst.Items[i].Text + ",";
                    id1 += chkst.Items[i].Value + ",";
                }
            }
        }

        if(name1=="")
        {
            name1 = "----Select----";
        }
        
            txtstate.Text = name1.TrimEnd(',');
            hdnStateId.Value = id1.TrimEnd(',');
            //chkst.Attributes.Add("onclick", "checkAll(this);");
    
       
    }
    protected void Check()
    {

        CheckBoxList chkst = (CheckBoxList)grdProduct.Rows[0].FindControl("chkstate");
        if (chkst.Items[0].Text == "ALL" && chkst.Items[0].Selected == true)
        {
            for (int i = 0; i < chkst.Items.Count; i++)
            {
               
                    chkst.Items[i].Selected = true;
                    //chkst.Items[i].Selected = true;            
                
            }
        }
    }

    protected void UnCheck()
    {
        
        CheckBoxList chkst = (CheckBoxList)grdProduct.Rows[0].FindControl("chkstate");
        int countSelected = chkst.Items.Cast<ListItem>().Where(i => i.Selected).Count();
        if (countSelected == 13)
        {
            for (int i = 0; i < chkst.Items.Count; i++)
            {

                chkst.Items[i].Selected = false;
                //chkst.Items[i].Selected = true; 
            }

        }
        
            
    }
   

    protected void CheckBoxList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        string name = "";
        string id = "";
        GridViewRow gv = (GridViewRow)((Control)sender).NamingContainer;
        CheckBoxList check = (CheckBoxList)gv.FindControl("CheckBoxList1");
        TextBox txtSubDivision = (TextBox)gv.FindControl("TextBox1");
        HiddenField hdnSubDivisionId = (HiddenField)gv.FindControl("hdnSubDivisionId");
        txtSubDivision.Text = "";
        hdnSubDivisionId.Value = "";     

        if (check.Items[0].Text == "ALL" && check.Items[0].Selected == true)
        {
            for (int i = 0; i < check.Items.Count; i++)
            {

                check.Items[i].Selected = true;

            }
        }

        int countSelected = check.Items.Cast<ListItem>().Where(i => i.Selected).Count();
        if (countSelected == check.Items.Count - 1)
        {
            for (int i = 0; i < check.Items.Count; i++)
            {

                check.Items[i].Selected = false;
            }

        }

        for (int i = 0; i < check.Items.Count; i++)
        {
            if (check.Items[i].Selected)
            {
                if (check.Items[i].Text != "ALL")
                {
                    name += check.Items[i].Text + ",";
                    id += check.Items[i].Value + ",";
                }
            }
        }

        if (name == "")
        {
            name = "----Select----";
        }
        //if (name == "")
        //{
        //    name = "NIL";
        //}

        txtSubDivision.Text = name.TrimEnd(',');
        hdnSubDivisionId.Value = id.TrimEnd(',');
    }
    //End
    protected void Chkstate1_SelectedIndexChanged(object sender, EventArgs e)
    {
        string name1 = "";
        string id1 = "";
        GridViewRow gv1 = (GridViewRow)((Control)sender).NamingContainer;
        CheckBoxList chkst = (CheckBoxList)gv1.FindControl("Chkstate1");
        TextBox txtstate = (TextBox)gv1.FindControl("txtUOM");
        HiddenField hdnStateId = (HiddenField)gv1.FindControl("hdnStateId1");
        txtstate.Text = "";
        hdnStateId.Value = "";

        if (chkst.Items[0].Text == "ALL" && chkst.Items[0].Selected == true)
        {
            for (int i = 0; i < chkst.Items.Count; i++)
            {

                chkst.Items[i].Selected = true;

            }
        }

        int countSelected = chkst.Items.Cast<ListItem>().Where(i => i.Selected).Count();
        if (countSelected == chkst.Items.Count - 1)
        {
            for (int i = 0; i < chkst.Items.Count; i++)
            {

                chkst.Items[i].Selected = false;

            }

        }

        for (int i = 0; i < chkst.Items.Count; i++)
        {
            if (chkst.Items[i].Selected)
            {
                if (chkst.Items[i].Text != "ALL")
                {
                    name1 += chkst.Items[i].Text + ",";
                    id1 += chkst.Items[i].Value + ",";
                }
            }
        }

        if (name1 == "")
        {
            name1 = "----Select----";
        }

        txtstate.Text = name1.TrimEnd(',');
        hdnStateId.Value = id1.TrimEnd(',');
        //chkst.Attributes.Add("onclick", "checkAll(this);");


    }
    protected void btnClose_Click(object sender, EventArgs e)
    {

    }
    protected void Product_Base_uom_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewRow gv = (GridViewRow)((Control)sender).NamingContainer;
        DropDownList ddlunit = (DropDownList)gv.FindControl("Product_uom");
        DropDownList ddlbaseunit = (DropDownList)gv.FindControl("Product_Base_uom");

        foreach (ListItem item in ddlunit.Items)
        {
            if (item.Value == ddlbaseunit.SelectedValue)
            {
                item.Attributes.Add("disabled", "disabled");
            }
        }
    }
}