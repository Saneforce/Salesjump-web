using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;

public partial class MasterFiles_ProdBulkEdit : System.Web.UI.Page
{
    #region "Declaration"
        DataSet dsProd = null;
        DataSet dsProduct = null;
        DataSet dsDivision = null;
        DataSet dsSubDivision = null;
        DataSet dsState = null;
        bool bsrch = false;
        string div_code = string.Empty;
        string ProdCode = string.Empty;
        string ProdSaleUnit = string.Empty;
        string search = string.Empty;
        string ProdName = string.Empty;
        int i;
        int iReturn = -1;
        string sChkLocation = string.Empty;
        string[] statecd;
        string state_cd = string.Empty;
        string sState = string.Empty;
        string state_code = string.Empty;
        string val = string.Empty;
        int iIndex;
        int kIndex;
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
            menu1.Title = this.Page.Title;
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
    private void FillProd()
    {
        Product dv = new Product();
        dsProd = dv.getProd_Edit(div_code);
        if (dsProd.Tables[0].Rows.Count > 0)
        {
            grdProduct.Visible = true;
            grdProduct.DataSource = dsProd;
            grdProduct.DataBind();

            foreach (GridViewRow gridRow in grdProduct.Rows)
            {
                DropDownList ddlCatg = (DropDownList)gridRow.Cells[1].FindControl("ddlProduct_Cat_Code");
                DropDownList ddlType = (DropDownList)gridRow.Cells[1].FindControl("ddlProduct_Type_Code");
                DropDownList ddlBrd = (DropDownList)gridRow.Cells[1].FindControl("ddlProduct_Brd_Code");
                
                Label lblProdCode = (Label)gridRow.Cells[1].FindControl("lblProdCode");
                ProdCode = lblProdCode.Text.ToString();
                TextBox txtState1 = (TextBox)gridRow.Cells[1].FindControl("txtState");


                CheckBoxList chkstate = (CheckBoxList)gridRow.FindControl("chkstate");
                CheckBoxList chkSubDiv = (CheckBoxList)gridRow.FindControl("CheckBoxList1");
                TextBox TextBox1 = (TextBox)gridRow.Cells[1].FindControl("TextBox1");
                
                

                DataSet dsCatgType = dv.getProdCatgType(ProdCode, div_code);
                if (dsCatgType.Tables[0].Rows.Count > 0)
                {
                    ddlCatg.SelectedValue = dsCatgType.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                    ddlType.SelectedValue = dsCatgType.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                    ddlBrd.SelectedValue = dsCatgType.Tables[0].Rows[0].ItemArray.GetValue(4).ToString();

                    string value = dsCatgType.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();
                    string[] strStateSplit = value.Split(',');
                    foreach (string strstate in strStateSplit)
                    {
                        if (strstate != "")
                        {
                            dsState.Tables[0].DefaultView.RowFilter = "State_Code in ('" + strstate + "')";
                            DataTable dt = dsState.Tables[0].DefaultView.ToTable();
                            txtState1.Text += dt.Rows[0].ItemArray.GetValue(1).ToString() + ", ";
                        }
                        

                        string[] strchkstate;
                        strchkstate = txtState1.Text.Split(',');
                        foreach (string chkst in strchkstate)
                        {                            
                            for (iIndex = 0; iIndex < chkstate.Items.Count; iIndex++)
                            {
                                if (chkst.Trim() == chkstate.Items[iIndex].Text.Trim())
                                {
                                    chkstate.Items[iIndex].Selected = true;

                                }
                            }
                        } 
                    }
                 
                        string strDiv = dsCatgType.Tables[0].Rows[0].ItemArray.GetValue(3).ToString();
                        string[] strDivSplit = strDiv.Split(',');
                        //if (strDiv == "")
                        //{
                        //    TextBox1.Text = "NIL";
                        //}
                        foreach (string strsubdv in strDivSplit)
                        {
                            if (strsubdv.ToString() != "")
                            {

                                dsSubDivision.Tables[0].DefaultView.RowFilter = "subdivision_code in ('" + strsubdv + "')";
                                DataTable dtDiv = dsSubDivision.Tables[0].DefaultView.ToTable();
                                TextBox1.Text += dtDiv.Rows[0].ItemArray.GetValue(2).ToString() + ",";
                            }

                            string[] strchkdiv;
                            strchkdiv = TextBox1.Text.Split(',');
                            foreach (string chkdiv in strchkdiv)
                            {
                                for (kIndex = 0; kIndex < chkSubDiv.Items.Count; kIndex++)
                                {
                                    if (chkdiv.Trim() == chkSubDiv.Items[kIndex].Text.Trim())
                                    {
                                        chkSubDiv.Items[kIndex].Selected = true;

                                    }                                   
                                                                        
                                }
                            }
                        } 
                }

                //string[] strweek;
                //strweek = txtState1.Text.Split(',');
                //foreach (string Wk in strweek)
                //{
                //    for (iIndex = 0; iIndex < chkstate.Items.Count; iIndex++)
                //    {
                //        if (Wk == chkstate.Items[iIndex].Text)
                //        {
                //            chkstate.Items[iIndex].Selected = true;

                //        }
                //    }
                //} 
            }
        }
    }

    protected DataSet FillCategory()
    {
        Product prd = new Product();
        dsProd = prd.getProductCategory(div_code);       
        return dsProd;
    }

    protected DataSet FillBrand()
    {
        Product prd = new Product();
        dsProd = prd.getProductBrand(div_code);
        return dsProd;
    }


    protected void grdProduct_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "this.className='highlight_clr'");
            e.Row.Attributes.Add("onmouseout", "this.className='normal'");
        }
    }
    protected void btnOk_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        Session["Char"] = string.Empty;
        grdProduct.PageIndex = 0;
      
        for (i = 0; i < CblProdCode.Items.Count; i++)
        {
            if (CblProdCode.Items[i].Selected == true)
            {
                bsrch = true;
            }           
        }

              if (bsrch == true)
              {
                  tblProduct.Visible = true;
             
                  for (i = 4; i < ((grdProduct.Columns.Count)); i++)
                  {
                      grdProduct.Columns[i].Visible = false;
                  }

                  for (int j = 0; j < CblProdCode.Items.Count; j++)
                  {
                      for (i = 4; i < grdProduct.Columns.Count; i++)
                      {
                          if (CblProdCode.Items[j].Selected == true)
                          {
                              if (grdProduct.Columns[i].HeaderText.Trim() == CblProdCode.Items[j].Text.Trim())
                              {
                                  grdProduct.Columns[i].Visible = true;
                              }
                          }
                          //else
                          //{
                          //    if (grdProduct.Columns[i].HeaderText.Trim() == "Product Name")
                          //    {
                          //        //grdProduct.Columns[i].
                          //    }
                          //}
                      }
                  }

                  if (CblProdCode.Items[0].Selected == false)
                  {
                      grdProduct.Columns[2].Visible = true;
                  }
                  else
                  {
                      grdProduct.Columns[2].Visible = false;
                  }

                  if (CblProdCode.Items[1].Selected == false)
                  {
                      grdProduct.Columns[3].Visible = true;
                  }
                  else
                  {
                      grdProduct.Columns[3].Visible = false;
                  }
              }
              Product dv = new Product();
              if (ddlSrch.SelectedValue == "1")
              {              
                  dsProd = dv.getProdall(div_code);
              }
              else if (ddlSrch.SelectedValue == "2")
              {
                  dsProd = dv.getProdforname(div_code, TxtSrch.Text);
              }
              else if (ddlSrch.SelectedValue == "3" && val != "")
              {
                  dsProd = dv.getProdforcat(div_code, val);
              }
              else if (ddlSrch.SelectedValue == "4" && val != "")
              {
                  dsProd = dv.getProdforgrp(div_code, val);
              }
              else if (ddlSrch.SelectedValue == "5" && val != "")
              {
                  dsProd = dv.getProdforbrd(div_code, val);
              }
              else if (ddlSrch.SelectedValue == "6" && val != "")
              {
                  dsProd = dv.getProdforSubdiv(div_code, val);
              }
              else if (ddlSrch.SelectedValue == "7" && val != "")
              {
                  dsProd = dv.getProdforState(div_code, val);
              }
              
              else
              {
                  dsProd = dv.getProdall(div_code);
              }

              if (dsProd.Tables[0].Rows.Count > 0)
              {
                  grdProduct.Visible = true;
                  grdProduct.DataSource = dsProd;
                  grdProduct.DataBind();
              }
              else
              {
                  grdProduct.DataSource = dsProd;
                  grdProduct.DataBind();
              }
              foreach (GridViewRow gridRow in grdProduct.Rows)
              {
                  DropDownList ddlCatg = (DropDownList)gridRow.Cells[1].FindControl("ddlProduct_Cat_Code");
                  DropDownList ddlType = (DropDownList)gridRow.Cells[1].FindControl("ddlProduct_Type_Code");
                  DropDownList ddlBrd = (DropDownList)gridRow.Cells[1].FindControl("ddlProduct_Brd_Code");

                  Label lblProdCode = (Label)gridRow.Cells[1].FindControl("lblProdCode");
                  ProdCode = lblProdCode.Text.ToString();
                  TextBox txtState1 = (TextBox)gridRow.Cells[1].FindControl("txtState");


                  CheckBoxList chkstate = (CheckBoxList)gridRow.FindControl("chkstate");
                  CheckBoxList chkSubDiv = (CheckBoxList)gridRow.FindControl("CheckBoxList1");
                  TextBox TextBox1 = (TextBox)gridRow.Cells[1].FindControl("TextBox1");



                  DataSet dsCatgType = dv.getProdCatgType(ProdCode, div_code);
                  if (dsCatgType.Tables[0].Rows.Count > 0)
                  {
                      ddlCatg.SelectedValue = dsCatgType.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                      ddlType.SelectedValue = dsCatgType.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                      ddlBrd.SelectedValue = dsCatgType.Tables[0].Rows[0].ItemArray.GetValue(4).ToString();

                      string value = dsCatgType.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();
                      string[] strStateSplit = value.Split(',');
                      foreach (string strstate in strStateSplit)
                      {
                          if (strstate != "")
                          {
                              dsState.Tables[0].DefaultView.RowFilter = "State_Code in ('" + strstate + "')";
                              DataTable dt = dsState.Tables[0].DefaultView.ToTable();
                              txtState1.Text += dt.Rows[0].ItemArray.GetValue(1).ToString() + ", ";
                          }


                          string[] strchkstate;
                          strchkstate = txtState1.Text.Split(',');
                          foreach (string chkst in strchkstate)
                          {
                              for (iIndex = 0; iIndex < chkstate.Items.Count; iIndex++)
                              {
                                  if (chkst.Trim() == chkstate.Items[iIndex].Text.Trim())
                                  {
                                      chkstate.Items[iIndex].Selected = true;

                                  }
                              }
                          }
                      }

                      string strDiv = dsCatgType.Tables[0].Rows[0].ItemArray.GetValue(3).ToString();
                      string[] strDivSplit = strDiv.Split(',');
                      //if (strDiv == "")
                      //{
                      //    TextBox1.Text = "NIL";
                      //}
                      foreach (string strsubdv in strDivSplit)
                      {
                          if (strsubdv.ToString() != "")
                          {

                              dsSubDivision.Tables[0].DefaultView.RowFilter = "subdivision_code in ('" + strsubdv + "')";
                              DataTable dtDiv = dsSubDivision.Tables[0].DefaultView.ToTable();
                              TextBox1.Text += dtDiv.Rows[0].ItemArray.GetValue(2).ToString() + ",";
                          }

                          string[] strchkdiv;
                          strchkdiv = TextBox1.Text.Split(',');
                          foreach (string chkdiv in strchkdiv)
                          {
                              for (kIndex = 0; kIndex < chkSubDiv.Items.Count; kIndex++)
                              {
                                  if (chkdiv.Trim() == chkSubDiv.Items[kIndex].Text.Trim())
                                  {
                                      chkSubDiv.Items[kIndex].Selected = true;

                                  }

                              }
                          }
                      }
                  }
                 
              }
    }
    
    
    //protected void grdProduct_RowDataBound(object sender, GridViewRowEventArgs e)
    //{
    //    if (e.Row.RowType == DataControlRowType.DataRow)
    //    {
    //        DropDownList ddlCatg = (DropDownList)e.Row.FindControl("Product_Cat_Code");
    //        if (ddlCatg != null)
    //        {
    //            DataRowView row = (DataRowView)e.Row.DataItem;
    //            ddlCatg.SelectedIndex = ddlCatg.Items.IndexOf(ddlCatg.Items.FindByValue(row["Product_Cat_Code"].ToString()));
    //        }
    //    }
    //}

    protected void btnClr_Click(object sender, EventArgs e)
    {
        for (i = 0; i < CblProdCode.Items.Count; i++)
        {
            CblProdCode.Items[i].Enabled = true;
            CblProdCode.Items[i].Selected = false;
        }

        grdProduct.DataSource = null;
        grdProduct.DataBind();
        tblProduct.Visible = false;
    }
    protected void grdProduct_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdProduct.PageIndex = e.NewPageIndex;
        FillProd();
    }
    protected void ddlProCatGrp_SelectedIndexChanged(object sender, EventArgs e)
    {
        val = ddlProCatGrp.SelectedValue;
        FillProd();

    }
    protected void ddlSrch_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlProCatGrp.Visible = true;
        int search = Convert.ToInt32(ddlSrch.SelectedValue);
        TxtSrch.Text = string.Empty;
        if (search == 2)
        {
            TxtSrch.Visible = true;
            Btnsrc.Visible = true;
            ddlProCatGrp.Visible = false;
        }
        else
        {
            TxtSrch.Visible = false;
            ddlProCatGrp.Visible = true;
            Btnsrc.Visible = true;
        }
        if (search == 1)
        {
            TxtSrch.Visible = false;
            ddlProCatGrp.Visible = false;
            Btnsrc.Visible = false;
            FillProd();

        }
        if (search == 3)
        {

            FillCategory1();

        }
        if (search == 4)
        {
            FillGroup();

        }
        if (search == 5)
        {          
            FillBrand1();
        }
        if (search == 6)
        {
            FillSubdiv();           
        }
        if (search == 7)
        {
            FillState(div_code);
        }
        val = "";
       // FillProd();

    }
    //Changes done by Priya
    private void FillState(string div_code)
    {
        Division dv = new Division();
        dsDivision = dv.getStatePerDivision(div_code);
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

            State st = new State();
            dsState = st.getState(state_cd);
            ddlProCatGrp.DataTextField = "statename";
            ddlProCatGrp.DataValueField = "state_code";
            ddlProCatGrp.DataSource = dsState;
            ddlProCatGrp.DataBind();
        }
    }
    private void FillCategory1()
    {
        Product prd = new Product();
        dsProduct = prd.getProductCategory(div_code);
        if (dsProduct.Tables[0].Rows.Count > 0)
        {
            ddlProCatGrp.DataTextField = "Product_Cat_Name";
            ddlProCatGrp.DataValueField = "Product_Cat_Code";
            ddlProCatGrp.DataSource = dsProduct;
            ddlProCatGrp.DataBind();
        }
    }
    private void FillBrand1()
    {
        Product prd = new Product();
        dsProduct = prd.getProductBrand(div_code);
        if (dsProduct.Tables[0].Rows.Count > 0)
        {
            ddlProCatGrp.DataTextField = "Product_Brd_Name";
            ddlProCatGrp.DataValueField = "Product_Brd_Code";
            ddlProCatGrp.DataSource = dsProduct;
            ddlProCatGrp.DataBind();
        }
    }

    private void FillGroup()
    {
        Product prd = new Product();
        dsProduct = prd.getProductGroup(div_code);
        if (dsProduct.Tables[0].Rows.Count > 0)
        {
            ddlProCatGrp.DataTextField = "Product_Grp_Name";
            ddlProCatGrp.DataValueField = "Product_Grp_Code";
            ddlProCatGrp.DataSource = dsProduct;
            ddlProCatGrp.DataBind();
        }

    }

    private void FillSubdiv()
    { 
        Product prd = new Product();
        dsProduct = prd.getSubdiv(div_code);
        if (dsProduct.Tables[0].Rows.Count > 0)
        {
            ddlProCatGrp.DataTextField = "subdivision_name";
            ddlProCatGrp.DataValueField = "subdivision_code";
            ddlProCatGrp.DataSource = dsProduct;
            ddlProCatGrp.DataBind();
        }

    }
 

    private void Search()
    {
        search = ddlSrch.SelectedValue.ToString();
        if (search == "2")
        {
            Product prd = new Product();
            // FindProduct(ddlSrch.SelectedValue, TxtSrch.Text, Session["div_code"].ToString());
            dsProduct = prd.FindProduct(ddlSrch.SelectedValue, TxtSrch.Text, Session["div_code"].ToString());
            if (dsProduct.Tables[0].Rows.Count > 0)
            {
               
                grdProduct.Visible = true;
                grdProduct.DataSource = dsProduct;
                grdProduct.DataBind();
            }
            else
            {
                grdProduct.DataSource = dsProduct;
                grdProduct.DataBind();
              
            }
        }
    }
    //change done by saravanan
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        string cntrl = string.Empty;
        string prod_code = string.Empty;
        string strTextBox = string.Empty;
        string stxt = string.Empty;
        

            for (i = 0; i < grdProduct.Rows.Count; i++)
            {         
                   
                    
                    Label lblProdCode = (Label)grdProduct.Rows[i].Cells[0].FindControl("lblProdCode");
                    if (CblProdCode.Items[1].Selected == true)
                    {
                        TextBox txtProductName = (TextBox)grdProduct.Rows[i].Cells[1].FindControl("txtProduct_Detail_Name");
                        stxt = txtProductName.Text.ToString();
                        strTextBox = strTextBox + CblProdCode.Items[1].Value + "= '" + stxt + "',";
                    }

                    if (CblProdCode.Items[2].Selected == true)
                    {
                        TextBox txtproductDesc = (TextBox)grdProduct.Rows[i].Cells[2].FindControl("txtProduct_Description");
                        stxt = txtproductDesc.Text.ToString();
                        strTextBox = strTextBox + CblProdCode.Items[2].Value + "= '" + stxt + "',";
                    }

                    if (CblProdCode.Items[3].Selected == true)
                    {
                        DropDownList ddlProductCatCode = (DropDownList)grdProduct.Rows[i].Cells[3].FindControl("ddlProduct_Cat_Code");
                        stxt = ddlProductCatCode.SelectedValue;
                        strTextBox = strTextBox + CblProdCode.Items[3].Value + "= '" + stxt + "',";
                    }

                    if (CblProdCode.Items[4].Selected == true)
                    {
                        DropDownList ddlProduct_Type_Code = (DropDownList)grdProduct.Rows[i].Cells[4].FindControl("ddlProduct_Type_Code");
                        stxt = ddlProduct_Type_Code.SelectedValue;
                        strTextBox = strTextBox + CblProdCode.Items[4].Value + "= '" + stxt + "',";
                    }
                    if (CblProdCode.Items[5].Selected == true)
                    {
                        TextBox txtProduct_Sale_Unit = (TextBox)grdProduct.Rows[i].Cells[5].FindControl("txtProduct_Sale_Unit");
                        stxt = txtProduct_Sale_Unit.Text;
                        strTextBox = strTextBox + CblProdCode.Items[5].Value + "= '" + stxt + "',";
                    }
                    if (CblProdCode.Items[6].Selected == true)
                    {
                        TextBox txtProduct_Sample_Unit_one = (TextBox)grdProduct.Rows[i].Cells[6].FindControl("txtProduct_Sample_Unit_one");
                        string stxtone = txtProduct_Sample_Unit_one.Text;
                        TextBox txtProduct_Sample_Unit_Two = (TextBox)grdProduct.Rows[i].Cells[7].FindControl("txtProduct_Sample_Unit_Two");
                        string stxttwo = txtProduct_Sample_Unit_Two.Text;
                        TextBox txtProduct_Sample_Unit_Three = (TextBox)grdProduct.Rows[i].Cells[8].FindControl("txtProduct_Sample_Unit_Three");
                        string stxtthree = txtProduct_Sample_Unit_Three.Text;
                        strTextBox = strTextBox + "Product_Sample_Unit_One" + "= '" + stxtone + "',Product_Sample_Unit_Two" + "= '" + stxttwo + "',Product_Sample_Unit_Three" + "= '" + stxtthree + "',";
                    }

                    if (CblProdCode.Items[7].Selected == true)
                    {
                        CheckBoxList checkst = (CheckBoxList)grdProduct.Rows[i].Cells[9].FindControl("chkstate");
                        string strstate = "";
                        for (int k = 0; k < checkst.Items.Count; k++)
                        {
                            if (checkst.Items[k].Selected)
                            {
                                if (checkst.Items[k].Text != "ALL")
                                {
                                    strstate += checkst.Items[k].Value + ',';
                                }
                            }
                        }
                        stxt = strstate;
                        strTextBox = strTextBox + CblProdCode.Items[7].Value + "= '" + stxt + "',";
                    }

                    if (CblProdCode.Items[8].Selected == true)
                    {
                        CheckBoxList check = (CheckBoxList)grdProduct.Rows[i].Cells[10].FindControl("CheckBoxList1");
                        string strSubstate = "";
                        for (int d = 0; d < check.Items.Count; d++)
                        {
                            if (check.Items[d].Selected)
                            {
                                strSubstate += check.Items[d].Value + ',';
                            }
                        }
                        stxt = strSubstate;
                        strTextBox = strTextBox + CblProdCode.Items[8].Value + "= '" + stxt + "',";
                    }
                    if (CblProdCode.Items[9].Selected == true)
                    {
                        TextBox txtSamp_Erp = (TextBox)grdProduct.Rows[i].Cells[11].FindControl("txtSamp_Erp");
                        stxt = txtSamp_Erp.Text;
                        strTextBox = strTextBox + CblProdCode.Items[9].Value + "= '" + stxt + "',";
                    }
                    if (CblProdCode.Items[10].Selected == true)
                    {
                        TextBox txtSale_Erp = (TextBox)grdProduct.Rows[i].Cells[12].FindControl("txtSale_Erp");
                        stxt = txtSale_Erp.Text;
                        strTextBox = strTextBox + CblProdCode.Items[10].Value + "= '" + stxt + "',";
                    }
                    if (CblProdCode.Items[11].Selected == true)
                    {
                        DropDownList ddlProductBrdCode = (DropDownList)grdProduct.Rows[i].Cells[11].FindControl("ddlProduct_Brd_Code");
                        stxt = ddlProductBrdCode.SelectedValue;
                        strTextBox = strTextBox + CblProdCode.Items[11].Value + "= '" + stxt + "',";
                    }
                    if (strTextBox.Trim().Length > 0)
                    {
                        strTextBox = strTextBox + " LastUpdt_Date = getdate() ";
                        Product prd = new Product();
                        iReturn = prd.BulkEdit(strTextBox, lblProdCode.Text);
                        //iReturn = prd.BulkEdit(lblProdCode.Text, txtProductName.Text, txtproductDesc.Text, ddlProductCatCode.SelectedValue, ddlProduct_Type_Code.SelectedValue, txtProduct_Sale_Unit.Text, txtProduct_Sample_Unit_one.Text, txtProduct_Sample_Unit_Two.Text, txtProduct_Sample_Unit_Three.Text, strstate, strSubstate);
                        strTextBox = "";
                    }

            
        }

            if (iReturn > 0)
            {
                //menu1.Status = "Product detailhave been updated Successfully";
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Updated Successfully');window.location='ProductList.aspx';</script>");
            }

    }
    //Changes done by Priya

    protected void CheckBoxList1_SelectedIndexChanged(object sender, EventArgs e)
    {

        string name1 = "";
        string id1 = "";
        GridViewRow gv = (GridViewRow)((Control)sender).NamingContainer;
        CheckBoxList check = (CheckBoxList)gv.FindControl("CheckBoxList1");
        TextBox txtSubDivision = (TextBox)gv.FindControl("TextBox1");
        HiddenField hdnSubDivisionId = (HiddenField)gv.FindControl("hdnSubDivisionId");
        txtSubDivision.Text = "";
        hdnSubDivisionId.Value = "";
        for (int i = 0; i < check.Items.Count; i++)
        {
            if(check.Items[i].Selected)
            {  
                name1 += check.Items[i].Text + ",";
                id1 += check.Items[i].Value + ",";
            } 
        }
        //if (name1 == "")
        //{
        // //   name1 = "NIL";
        //} 


        txtSubDivision.Text = name1.TrimEnd(',');
        hdnSubDivisionId.Value = id1.TrimEnd(',');
    }
    protected void chkstate_SelectedIndexChanged(object sender, EventArgs e)
    {

        string name = "";
        string id = "";
        GridViewRow gv = (GridViewRow)((Control)sender).NamingContainer;
        CheckBoxList checkst = (CheckBoxList)gv.FindControl("chkstate");
        TextBox txtState = (TextBox)gv.FindControl("txtState");
        HiddenField hdnStateId = (HiddenField)gv.FindControl("hdnStateId");
        txtState.Text = "";
        hdnStateId.Value = "";

        if (checkst.Items[0].Text == "ALL" && checkst.Items[0].Selected == true)
        {
            for (int i = 0; i < checkst.Items.Count; i++)
            {

                checkst.Items[i].Selected = true;
                //checkst.Items[i].Selected = true;            

            }
        }
        int countSelected = checkst.Items.Cast<ListItem>().Where(i => i.Selected).Count();
        if (countSelected == checkst.Items.Count-1)
        {
            for (int i = 0; i < checkst.Items.Count; i++)
            {

                checkst.Items[i].Selected = false;
                //checkst.Items[i].Selected = true; 
            }

        }

        for (int i = 0; i < checkst.Items.Count; i++)
        {
            if (checkst.Items[i].Selected)
            {
                if (checkst.Items[i].Text != "ALL")
                {
                    name += checkst.Items[i].Text + ",";
                    id += checkst.Items[i].Value + ",";
                }

            }
        }
        if (name == "")
        {
            name = "---- Select ----";
        }
        

        
        txtState.Text = name.TrimEnd(',');
        hdnStateId.Value = id.TrimEnd(',');
    }
    protected void grdProduct_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            CheckBoxList check = (CheckBoxList)e.Row.FindControl("CheckBoxList1");
            TextBox txtSubDivision = (TextBox)e.Row.FindControl("TextBox1");
            HiddenField hdnSubDivisionId = (HiddenField)e.Row.FindControl("hdnSubDivisionId");
            SubDivision dv = new SubDivision();
            dsSubDivision = dv.getSubDiv(div_code);
            check.DataTextField = "subdivision_name";
            check.DataSource = dsSubDivision;
            check.DataBind();
            string[] subdiv;
            if (subdivision_code != "")
            {
                iIndex = -1;
                subdiv = subdivision_code.Split(',');
                foreach (string st in subdiv)
                {
                    for (iIndex = 0; iIndex < check.Items.Count; iIndex++)
                    {
                        if (st == check.Items[iIndex].Value)
                        {
                            check.Items[iIndex].Selected = true;
                            check.Items[iIndex].Attributes.Add("style", "Color: #8A2BE2;font-weight:Bold");

                        }
                    }
                }
            }
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

                State st = new State();
                dsState = st.getStateAddChkBox(state_cd);
                chkst.DataTextField = "statename";
                chkst.DataValueField = "state_code";
                chkst.DataSource = dsState;
                chkst.DataBind();
            }
            string[] state;
            if (state_code != "")
            {
                iIndex = -1;
                state = state_code.Split(',');
                foreach (string st in state)
                {
                    for (iIndex = 0; iIndex < chkst.Items.Count; iIndex++)
                    {
                        if (st == chkst.Items[iIndex].Value)
                        {
                            chkst.Items[iIndex].Selected = true;
                            chkst.Items[iIndex].Attributes.Add("style", "Color: Blue; font-weight:Bold ");

                        }
                    }
                }
            }
        }

    }

    protected void btnClose_Click(object sender, EventArgs e)
    {

    }
}