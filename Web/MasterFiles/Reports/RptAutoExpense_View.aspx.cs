using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data;
using System.Collections;
using System.Configuration;
using Bus_EReport;
public partial class MasterFiles_MR_RptAutoExpense : System.Web.UI.Page
{
    bool isSavedPage  = false;
    string sfcode = "";
    string monthId = "";
    string yearID = "";
    string divCode = "";
    string gt = "";
    
    Hashtable months = new Hashtable();
    Distance_calculation Exp = new Distance_calculation();
    protected void Page_Load(object sender, EventArgs e)
    {
        months.Add("1", "January");
        months.Add("2", "Feb");
        months.Add("3", "March");
        months.Add("4", "April");
        months.Add("5", "May");
        months.Add("6", "June");
        months.Add("7", "July");
        months.Add("8", "August");
        months.Add("9", "Sept");
        months.Add("10", "October");
        months.Add("11", "November");
        months.Add("12", "Decemeber");

        gt = grandTotalName.InnerHtml;
        sfcode = Request.QueryString["sf_code"].ToString();
        divCode = Request.QueryString["divCode"].ToString();
        monthId = Request.QueryString["monthId"].ToString();
        yearID = Request.QueryString["yearID"].ToString();
        mnthtxtId.InnerHtml = months[Request.QueryString["monthId"].ToString()].ToString();
        yrtxtId.InnerHtml = Request.QueryString["yearID"].ToString();
        DataTable ds = Exp.getFieldForce(divCode, sfcode);
        fieldforceId.InnerHtml = "Fieldforce Name :" + ds.Rows[0]["sf_name"].ToString();
        hqId.InnerHtml = "HQ :" + ds.Rows[0]["sf_hq"].ToString();
        //empId.InnerHtml = "Employee Code :" + ds.Rows[0]["sf_code"].ToString();
        DataTable t1 = Exp.getSavedRecord(monthId, yearID, sfcode,divCode);
        double totalAllowance = 0;
        double totalDistance = 0;
        double totalFare = 0;
        double grandTotal = 0;
        foreach (DataRow row in t1.Rows)
        {
            totalAllowance = totalAllowance + Convert.ToDouble(row["Expense_Allowance"].ToString());
            totalDistance = totalDistance + Convert.ToDouble(row["Expense_Distance"].ToString());
            totalFare = totalFare + Convert.ToDouble(row["Expense_Fare"].ToString());
            grandTotal = grandTotal + Convert.ToDouble(row["Expense_Total"].ToString());
        }
        t1.Rows.Add();
        t1.Rows[t1.Rows.Count - 1]["Expense_Allowance"] = totalAllowance;
        t1.Rows[t1.Rows.Count - 1]["Expense_Distance"] = totalDistance;
        t1.Rows[t1.Rows.Count - 1]["Expense_Fare"] = totalFare;
        t1.Rows[t1.Rows.Count - 1]["Expense_Total"] = grandTotal;


        misExp.Visible = true;
        grdExpMain.Visible = true;
        grdExpMain.DataSource = t1;
        grdExpMain.DataBind();
        //generateOtherExpControls(Exp);
        double otherExAmnt = 0;
            DataTable customExpTable = Exp.getSavedFixedExp(monthId, yearID, sfcode); 
            otherExpGrid.DataSource = customExpTable;
            otherExpGrid.DataBind();
            foreach (DataRow r in customExpTable.Rows)
            {
                otherExAmnt = otherExAmnt + Convert.ToDouble(r["Amount"].ToString());

            }
    
            DataTable dtExp = Exp.getSavedOtheExpRecord(monthId, yearID, sfcode);
            expGrid.DataSource = dtExp;
            expGrid.DataBind();
            foreach(DataRow r in dtExp.Rows)
            {
                otherExAmnt = otherExAmnt + Convert.ToDouble(r["Amount"].ToString());

            }
            double tot = otherExAmnt + grandTotal;
            grandTotalName.InnerHtml = tot.ToString();
            DataTable t2 = Exp.getAdmnAdjustExp(sfcode, monthId, yearID);

            if (t2.Rows.Count > 0)
            {
                isSavedPage = true;
                grandTotalName.InnerHtml = t2.Rows[0]["Grand_Total"].ToString();
            }
      
    }
 private void generateOtherExpControls(Distance_calculation dist)
 {



    // DataTable t2 = dist.getSavedOtheExpRecord(monthId, yearID, sfcode);
     HtmlTable htmlTable = (HtmlTable)FindControl("tableId");
     DataTable t2 = dist.getAdmnAdjustExp(sfcode, monthId, yearID);

     for (int p = htmlTable.Rows.Count - 1; p > 0; p--)
     {
         htmlTable.Rows.RemoveAt(p);
     }
     for (int i = 0; i < t2.Rows.Count; i++)
     {

         HtmlTableRow r = new HtmlTableRow();
         DropDownList d = new DropDownList();
         d.ID = "Combovalue_" + i;
         d.CssClass = "Combovalue";
         d.Attributes.Add("onchange","adminAdjustCalc(this,0)");
         //d.Attributes.AddAttributes("onchange", "adminAdjustCalc(this,0)");
             
             d.Items.Insert(0, new ListItem(" --Select-- ", "2"));
             d.Items.Insert(1, new ListItem("+", "1"));
             d.Items.Insert(2, new ListItem("-", "0"));
         string amnt = t2.Rows[i]["amt"].ToString();
         string rm = t2.Rows[i]["Paritulars"].ToString();
         if (rm == "0")
         {
             rm = "";
         }
         if (amnt == "0")
         {
             amnt = "";
         }
         d.SelectedValue = t2.Rows[i]["typ"].ToString();
         HtmlTableCell cell1 = new HtmlTableCell();
         cell1.Controls.Add(d);

         HtmlTableCell cell2 = new HtmlTableCell();
         TextBox box = new TextBox();
         Literal lit = new Literal();
         lit.Text = @"<input type='text' value='"+rm+"' name='tP' size='50' maxlength='50' class='textbox'  onkeydown='if(event.shiftKey==true && event.keyCode==9){this.parentNode.previousSibling.focus();return false}' style='width:90%;height:19px'/>";
         HtmlTable table = new HtmlTable();
         System.Text.StringBuilder sb = new System.Text.StringBuilder("");
         System.IO.StringWriter tw = new System.IO.StringWriter(sb);
         HtmlTextWriter hw = new HtmlTextWriter(tw);
         box.RenderControl(hw);


         cell2.Controls.Add(lit);
         r.Cells.Add(cell2);
         r.Cells.Add(cell1);

         HtmlTableCell cell3 = new HtmlTableCell();
         TextBox box1 = new TextBox();
         lit = new Literal();
         lit.Text = @"<input type='text' value='"+amnt+"' name='tAmt' maxlength='50' onkeypress='_fNvALIDeNTRY(D,7)' onkeyup='adminAdjustCalc(this,0);' class='textbox' style='width:90%;height:19px;text-align:right' />";
         sb = new System.Text.StringBuilder("");
         tw = new System.IO.StringWriter(sb);
         hw = new HtmlTextWriter(tw);
         box1.RenderControl(hw);

         cell3.Controls.Add(lit);
         r.Cells.Add(cell3);
         HtmlTableCell cell4 = new HtmlTableCell();
         Button b1 = new Button();
         lit = new Literal();
         lit.Text = @"<input type='button' id='btnadd' value=' + ' class='btnSave' onclick='_AdRowByCurrElem(this)' />";
         sb = new System.Text.StringBuilder("");
         tw = new System.IO.StringWriter(sb);
         hw = new HtmlTextWriter(tw);
         b1.RenderControl(hw);
         cell4.Controls.Add(lit);
         r.Cells.Add(cell4);
         HtmlTableCell cell5 = new HtmlTableCell();
         Button b2 = new Button();
         lit = new Literal();
         lit.Text = @"<input type='button' id='btndel' value=' - ' class='btnSave' onclick='DRForAdmin(this,this.parentNode.parentNode,1)' />";
         sb = new System.Text.StringBuilder("");
         tw = new System.IO.StringWriter(sb);
         hw = new HtmlTextWriter(tw);
         b2.RenderControl(hw);

         cell5.Controls.Add(lit);
         r.Cells.Add(cell5);

         htmlTable.Rows.Add(r);
     }
 }
 protected override void OnLoadComplete(EventArgs e)
 {
     if (isSavedPage)
     {
         Distance_calculation dist = new Distance_calculation();
         generateOtherExpControls(dist);
     }
 }

       protected void btnSave_Click(object sender, EventArgs e)
    {
      
        saveData(true);
        string close = @"<script type='text/javascript'>
                                window.returnValue = true;
                                window.close();
                                </script>";
        base.Response.Write(close);
        
        //Response.Redirect("RptAutoExpense_Approve.aspx");

    }
      protected void btnField_Click(object sender, EventArgs e)
       {
           Exp.deleteAllExpenseSavedRecord(monthId,yearID,sfcode);
           string close = @"<script type='text/javascript'>
                                window.returnValue = true;
                                window.close();
                                </script>";
           base.Response.Write(close);
          
          // Response.Redirect("RptAutoExpense_Approve.aspx");
       }
    protected void btnSaveDraft_Click(object sender, EventArgs e)
    {
       
        saveData(false);
        string close = @"<script type='text/javascript'>
                                window.returnValue = true;
                                window.close();
                                </script>";
        base.Response.Write(close);

        //Response.Redirect("RptAutoExpense_Approve.aspx");

    }
    
    private void saveData(bool flag)
    {


        isSavedPage = true;
        int iReturn = -1;
        HiddenField otherExpValues = (HiddenField)FindControl("otherExpValues");
       // String gt = grandTotalName.InnerHtml;
        iReturn = Exp.deleteAdminAdjustExp(sfcode, monthId, yearID);
        string[] splitVal = otherExpValues.Value.Split('~');

        string[] pert = splitVal[0].Split(',');
        string[] amount = splitVal[1].Split(',');
        string[] op = splitVal[2].Split(',');
        
        iReturn=Exp.updateHeadFlg(flag?"2":"3",sfcode, monthId, yearID);
        for (int p = 0; p < op.Length; p++)
        {
            string[] e = op[p].Split('=');
            iReturn = Exp.addAdminAdjustmentExpRecord(e[0], pert[p] == "" ? "0" : pert[p], amount[p] == "" ? "0" : amount[p], splitVal[3], sfcode, monthId, yearID);
        }
        if (iReturn > 0)
        {
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Updated Successfully');</script>");
        }
    }
}