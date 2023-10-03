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
    Distance_calculation Exp = new Distance_calculation();
    protected void Page_Load(object sender, EventArgs e)
    {
        gt = grandTotalName.InnerHtml;
        sfcode = Request.QueryString["sf_code"].ToString();
        divCode = Request.QueryString["divCode"].ToString();
        monthId = Request.QueryString["month"].ToString();
        yearID = Request.QueryString["year"].ToString();
        mnthtxtId.InnerHtml = monthId;
        yrtxtId.InnerHtml = yearID;
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
            DataTable adminExp = Exp.getSavedAdminExpRecord(monthId, yearID, sfcode);
            adminExpGrid.DataSource = adminExp;
            adminExpGrid.DataBind();
            foreach (DataRow r in adminExp.Rows)
            {
                if (r["typ"].ToString() == "1")
                {
                    otherExAmnt = otherExAmnt + Convert.ToDouble(r["Amount"].ToString());
                }
                else if (r["typ"].ToString() == "0")
                {
                    otherExAmnt = otherExAmnt - Convert.ToDouble(r["Amount"].ToString());
                }

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
 
}