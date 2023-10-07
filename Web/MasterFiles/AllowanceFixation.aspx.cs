using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using Bus_EReport;
using DBase_EReport;
using System.Web.Script.Serialization;

public partial class MasterFiles_AllowanceFixation : System.Web.UI.Page
{

    string div_code = string.Empty;
    string sfCode = string.Empty;
    SalesForce sf = new SalesForce();
    DataSet dsExp = new DataSet();
    DataSet dsSFMR = new DataSet();
    TextBox TextBox1 = new TextBox();
    static DataTable Sdt = new DataTable();
    static DataTable dt = new DataTable();
   
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            div_code = Session["div_code"].ToString();
            sfCode = Session["sf_code"].ToString();

            string str = txtTPStartDate.Text;
            if (!Page.IsPostBack)
            {
                menu1.Title = Page.Title;
                menu1.FindControl("btnBack").Visible = false;
                FillReporting();
                BindSelectedValue();
                //txtFix_Date.Text = DateTime.Now.Date.ToShortDateString();
                //btnSave.Visible = false; 
               
                
            }

            //BindDate();
        }
        catch (Exception ex)
        {

        }        
    }

    private void FillReporting()
    {
        Territory sf = new Territory();
        DataSet dsSalesForce = new DataSet();
        dsSalesForce = sf.getExp_Managers(div_code);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            ddlRegion.DataTextField = "Sf_Name";
            ddlRegion.DataValueField = "Sf_Code";
            ddlRegion.DataSource = dsSalesForce;
            ddlRegion.DataBind();

        }
    }
    protected void ddlRegion_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            BindSelectedValue();
        }
        catch (Exception ex)
        {

        }
    }

    private void BindSelectedValue()
    {
            
        Territory terr = new Territory();
        grdWTAllowance.DataSource = null;
        grdWTAllowance.DataBind();
        
        dsSFMR = sf.GetExp_AllwanceFixation(div_code, ddlRegion.SelectedValue);      

        if (dsSFMR.Tables[0].Rows.Count > 0)
        {
            //grdWTAllowance.DataSource = dsSFMR;
            //grdWTAllowance.DataBind();
        }
        else
        {
            dsSFMR = sf.SalesForceListMgrGet(div_code, ddlRegion.SelectedValue);
            
            if (dsSFMR.Tables[0].Rows.Count > 0)
            {
                //grdWTAllowance.DataSource = dsSFMR;
                //grdWTAllowance.DataBind();
                btnSave.Visible = true;
                txtTPStartDate.Text = DateTime.Now.Date.ToShortDateString();

               
                
            }
        }

        dsExp = terr.getExp_FixedType1(div_code);
        string strTextbox = string.Empty;
        DataRow dr = null;
        if (dsExp.Tables[0].Rows.Count > 0)
        {
            dt = dsExp.Tables[0];
            dsSFMR.Tables[0].Merge(dt);

            if (dt.Columns.Count == 1)
            {
                grdWTAllowance.Columns[12].Visible = true;
                grdWTAllowance.Columns[12].HeaderText = dt.Columns[0].ColumnName;
            }
            else if (dt.Columns.Count == 2)
            {
                grdWTAllowance.Columns[12].Visible = true;
                grdWTAllowance.Columns[13].Visible = true;
                grdWTAllowance.Columns[12].HeaderText = dt.Columns[0].ColumnName;
                grdWTAllowance.Columns[13].HeaderText = dt.Columns[1].ColumnName;

            }
            else if (dt.Columns.Count == 3)
            {
                grdWTAllowance.Columns[12].Visible = true;
                grdWTAllowance.Columns[13].Visible = true;
                grdWTAllowance.Columns[14].Visible = true;
                grdWTAllowance.Columns[12].HeaderText = dt.Columns[0].ColumnName;
                grdWTAllowance.Columns[13].HeaderText = dt.Columns[1].ColumnName;
                grdWTAllowance.Columns[14].HeaderText = dt.Columns[2].ColumnName;
            }

            else if (dt.Columns.Count == 4)
            {
                grdWTAllowance.Columns[12].Visible = true;
                grdWTAllowance.Columns[13].Visible = true;
                grdWTAllowance.Columns[14].Visible = true;
                grdWTAllowance.Columns[15].Visible = true;
                grdWTAllowance.Columns[12].HeaderText = dt.Columns[0].ColumnName;
                grdWTAllowance.Columns[13].HeaderText = dt.Columns[1].ColumnName;
                grdWTAllowance.Columns[14].HeaderText = dt.Columns[2].ColumnName;
                grdWTAllowance.Columns[15].HeaderText = dt.Columns[3].ColumnName;
            }

            else if (dt.Columns.Count == 5)
            {
                grdWTAllowance.Columns[12].Visible = true;
                grdWTAllowance.Columns[13].Visible = true;
                grdWTAllowance.Columns[14].Visible = true;
                grdWTAllowance.Columns[15].Visible = true;
                grdWTAllowance.Columns[16].Visible = true;
                grdWTAllowance.Columns[12].HeaderText = dt.Columns[0].ColumnName;
                grdWTAllowance.Columns[13].HeaderText = dt.Columns[1].ColumnName;
                grdWTAllowance.Columns[14].HeaderText = dt.Columns[2].ColumnName;
                grdWTAllowance.Columns[15].HeaderText = dt.Columns[3].ColumnName;
                grdWTAllowance.Columns[16].HeaderText = dt.Columns[4].ColumnName;
            }
            int iCount = dsSFMR.Tables[0].Rows.Count-1;
            dsSFMR.Tables[0].Rows.RemoveAt(iCount);
            
        }

     

        grdWTAllowance.DataSource = dsSFMR;
        grdWTAllowance.DataBind();

        foreach (GridViewRow gridRow in grdWTAllowance.Rows)
        {
            TextBox lblEffective = (TextBox)gridRow.FindControl("txtEffective");
            if (lblEffective.Text == "")
            {
                lblEffective.Text = txtTPStartDate.Text;
            }
        }
       // Sdt= dsSFMR.Tables[0];
        //CreateDynamicTable(dsSFMR);
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            string strParametrValue = string.Empty;
            string strColumn = string.Empty;
             int iReturn = -1;
             
             //for (int i = 1; i < grdWTAllowance.Columns.Count; i++)
             //{
             //    foreach (GridViewRow gridrow in grdWTAllowance.Rows)
             //    {
             //        string str1 = grdWTAllowance.HeaderRow.Cells[13].Text;                     
             //    }
             //    for (int j = 0; j < grdWTAllowance.Rows.Count; j++)
             //    {
             //        string str = grdWTAllowance.Rows[j].Cells[i].Text;
             //    }
             //}
            
             //TextBox lnkView = (sender as TextBox);
             //GridViewRow row = (lnkView.NamingContainer as GridViewRow);
             //string id = lnkView.Text;
             //string name = row.Cells[13].Text;
             //string country = (row.FindControl("myTextBox13") as TextBox).Text;
             //ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Id: " + id + " Name: " + name + " Country: " + country + "')", true);
           
            foreach (GridViewRow gridRow in grdWTAllowance.Rows)
            {
                              
                Label lblsfcode = (Label)gridRow.FindControl("lblsfcode");
                TextBox txtHq = (TextBox)gridRow.FindControl("txtHq");
                TextBox txtEXHQ = (TextBox)gridRow.FindControl("txtEXHQ");
                TextBox txtOS = (TextBox)gridRow.FindControl("txtOS");
                TextBox txtHill = (TextBox)gridRow.FindControl("txtHill");
                TextBox txtFareKm = (TextBox)gridRow.FindControl("txtFareKm");
                TextBox txtRangeofFare1 = (TextBox)gridRow.FindControl("txtRangeofFare1");
                TextBox txtRangeofFare2 = (TextBox)gridRow.FindControl("txtRangeofFare2");
                TextBox txtEffective = (TextBox)gridRow.FindControl("txtEffective");

                if (dt.Columns.Count == 1)
                {
                    TextBox txttxtHill_Station_Allowance = (TextBox)gridRow.FindControl("TextBox13"); 
                    strParametrValue="'"+txttxtHill_Station_Allowance.Text+"'";
                    strColumn = "Fixed_Column1";
                }
                else if (dt.Columns.Count == 2)
                {
                    TextBox txttxtHill_Station_Allowance = (TextBox)gridRow.FindControl("TextBox13");
                    TextBox txtMetro_Allowances = (TextBox)gridRow.FindControl("TextBox14"); 
                    strParametrValue="'"+txttxtHill_Station_Allowance.Text+"','"+txtMetro_Allowances.Text+"'";
                    strColumn = "Fixed_Column1,Fixed_Column2";
                }
                else if (dt.Columns.Count == 3)
                {
                    TextBox txttxtHill_Station_Allowance = (TextBox)gridRow.FindControl("TextBox13");
                    TextBox txtMetro_Allowances = (TextBox)gridRow.FindControl("TextBox14");
                    TextBox txtMiscellaneous_Expenses = (TextBox)gridRow.FindControl("TextBox15");
                    strParametrValue = "'" + txttxtHill_Station_Allowance.Text + "','" + txtMetro_Allowances.Text + "','"+ txtMiscellaneous_Expenses.Text+"'";
                    strColumn = "Fixed_Column1,Fixed_Column2,Fixed_Column3";
                }
                else if (dt.Columns.Count == 4)
                {
                    TextBox txttxtHill_Station_Allowance = (TextBox)gridRow.FindControl("TextBox13");
                    TextBox txtMetro_Allowances = (TextBox)gridRow.FindControl("TextBox14");
                    TextBox txtMiscellaneous_Expenses = (TextBox)gridRow.FindControl("TextBox15");
                    TextBox TextBox16 = (TextBox)gridRow.FindControl("TextBox16");
                    strParametrValue = "'" + txttxtHill_Station_Allowance.Text + "','" + txtMetro_Allowances.Text + "','" + txtMiscellaneous_Expenses.Text + "','"+ TextBox16.Text+"'";
                    strColumn = "Fixed_Column1,Fixed_Column2,Fixed_Column3,Fixed_Column4";
                }
                else if (dt.Columns.Count == 5)
                {
                    TextBox txttxtHill_Station_Allowance = (TextBox)gridRow.FindControl("TextBox13");
                    TextBox txtMetro_Allowances = (TextBox)gridRow.FindControl("TextBox14");
                    TextBox txtMiscellaneous_Expenses = (TextBox)gridRow.FindControl("TextBox15");
                    TextBox TextBox16 = (TextBox)gridRow.FindControl("TextBox16");
                    TextBox TextBox17 = (TextBox)gridRow.FindControl("TextBox17");

                    strParametrValue = "'" + txttxtHill_Station_Allowance.Text + "','" + txtMetro_Allowances.Text + "','" + txtMiscellaneous_Expenses.Text + "','" + TextBox16.Text + "','"+ TextBox17.Text+"'";
                    strColumn = "Fixed_Column1,Fixed_Column2,Fixed_Column3,Fixed_Column4,Fixed_Column5";
                }


                iReturn = sf.ExpSalesforce_RecordAdd(lblsfcode.Text, txtHq.Text, txtEXHQ.Text, txtOS.Text, txtHill.Text, txtFareKm.Text, txtRangeofFare1.Text, txtRangeofFare2.Text, txtEffective.Text, strParametrValue, strColumn);
            }
           
            if (iReturn > 0)
            {
                //   menu1.Status = "Division Created Successfully ";
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Created Successfully');</script>");
                //Server.Transfer("FVExpense_Parameter.aspx");
                // Resetall();
            }

        }
        catch (Exception ex)
        {

        }
    }
  

    private void BindDate()
    {
       

        foreach (GridViewRow gridRow in grdWTAllowance.Rows)
        {
            TextBox lblEffective = (TextBox)gridRow.FindControl("txtEffective");
            lblEffective.Text = txtTPStartDate.Text;
        }
    }
   
    protected void txtTPStartDate_TextChanged(object sender, EventArgs e)
    {
        BindDate();
    }
    protected void Page_PreInit(object sender, EventArgs e)
    {
        //grdWTAllowance.DataSource = Sdt;
        //grdWTAllowance.DataBind();
    }
    protected void grdWTAllowance_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        DataTable dtCurrentTable = new DataTable();
        DataRow drCurrentRow = null;

        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    for (int i = 13; i < grdWTAllowance.Columns.Count; i++)
        //    {
        //        // string str = dsSFMR.Tables[0].Rows[i][23].ToString();
        //        for (int j = 0; j < grdWTAllowance.Rows.Count; j++)
        //        {
        //            TextBox TextBox1 = new TextBox();
        //            grdWTAllowance.HeaderRow.Cells[i].Text = dsSFMR.Tables[0].Columns[i].ToString();
                   
        //        }
        //    }
            //for (int i = 23; i < dsSFMR.Tables[0].Columns.Count; i++)
            //{
            //    //TextBox TextBox1 = new TextBox();
            //    //TextBox1.ID = "myTextBox" + (i).ToString();
            //    ////TextBox1.Text = "myTextBox" + (i).ToString();
            //    //TextBox1.Attributes.Add("runat", "server");
            //    //e.Row.Cells[i].FindControl("myTextBox" + (i));
            //    grdWTAllowance.HeaderRow.Cells[i].Text = dsSFMR.Tables[0].Columns[i].ToString();
            //    //dsSFMR.Tables[0].Rows[i][str] = TextBox1.Text;
            //    //e.Row.Cells[i].Controls.Add(TextBox1);

            //}
        //}

            //        TextBox txtCountry = new TextBox();
            //        txtCountry.ID = "myTextBox" + (i).ToString();
                    
            //            string str = grdWTAllowance.HeaderRow.Cells[i].Text;
            //            txtCountry.Text = (e.Row.DataItem as DataRowView).Row[str].ToString();
            //            e.Row.Cells[i].Controls.Add(txtCountry);
                    
            //    }


            //}
                
        //for (int i = 13; i < grdWTAllowance.Columns.Count; i++)
        //{
        //    // string str = dsSFMR.Tables[0].Rows[i][23].ToString();
        //    //for (int j = 0; j < grdWTAllowance.Rows.Count; j++)
        //    //{
        //    //    TextBox TextBox1 = new TextBox();
        //    //    TextBox1.ID = newserialno + (Convert.ToInt32(grdWTAllowance.Rows[.RowIndex + 1)).ToString();
        //    //    grdWTAllowance.Rows[j].Cells[i].Controls.Add(TextBox1);
        //    //}

        //    foreach (GridViewRow gridRow in grdWTAllowance.Rows)
        //    {
        //        TextBox TextBox1 = new TextBox();
        //        TextBox1.ID = "myTextBox" + (i).ToString();
        //        //TextBox1.Text = "myTextBox" + (i).ToString();
        //        TextBox1.Attributes.Add("runat", "server");
        //        grdWTAllowance.Rows[gridRow.RowIndex].Cells[i].Controls.Add(TextBox1);
        //    }
        //}

        //dsExp = terr.getExp_FixedType();
        //dt = dsExp.Tables[0];
        //Repeater rt = (Repeater)e.Row.FindControl("rptCont");
        //Repeater rt1 = (Repeater)e.Row.FindControl("rptCont1");
        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    rt.DataSource = dsExp;
        //    rt.DataBind();

        //    //rt1.DataSource = dsExp;
        //    //rt1.DataBind();

        //    for (int i = 0; i < dt.Columns.Count; i++)
        //    {
        //        BoundField boundfield = new BoundField();
        //        boundfield.DataField = dt.Rows[i].ToString();
        //        boundfield.HeaderText = dt.Rows[i].Field<string>(0);
        //        grdWTAllowance.Columns.Add(boundfield);
        //    }
        //}
    }

}
