using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using Bus_EReport;
public partial class MasterFiles_Subdiv_Salesforcewise : System.Web.UI.Page
{
    #region "Declaration"
    DataSet dsSubDivision = null;
    int subdivcode = 0;
    int subdivision_code = 0;
    string divcode = string.Empty;
    string sfcode = string.Empty;
    string subdiv_sname = string.Empty;
    string subdiv_name = string.Empty;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int time;
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        divcode = Convert.ToString(Session["div_code"]);
        sfcode = Convert.ToString(Session["Sf_Code"]); 
        if (!Page.IsPostBack)
        {
            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);
            //menu1.Title = this.Page.Title;
            //menu1.FindControl("btnBack").Visible = false;
            FillFieldForcediv(divcode);
            ddlSubdiv.Focus();
            GetMyMonthList();
            bind_year_ddl();
        }
    }
    public void GetMyMonthList()
    {
        DateTime month = Convert.ToDateTime("1/1/2012");
        for (int i = 0; i < 12; i++)
        {
            DateTime nextMonth = month.AddMonths(i);
            ListItem list = new ListItem();
            list.Text = nextMonth.ToString("MMMM");
            list.Value = nextMonth.Month.ToString();
            monthId.Items.Add(list);
        }
        monthId.Items.Insert(0, new ListItem("  Select Month  ", "0"));
    }

    private void bind_year_ddl()
    {
        int year = (System.DateTime.Now.Year);
        for (int intCount = 2015; intCount <= year + 1; intCount++)
        {
            yearID.Items.Add(intCount.ToString());
        }
        yearID.Items.Insert(0, new ListItem("  Select Year  ", "0"));
    }
    private void FillFieldForcediv(string divcode)
    {
        Distance_calculation dv = new Distance_calculation();
        dsSubDivision = dv.getRegion(divcode);
        if (dsSubDivision.Tables[0].Rows.Count > 0)
        {
            ddlSubdiv.DataTextField = "sf_name";
            ddlSubdiv.DataValueField = "sf_code";
            ddlSubdiv.DataSource = dsSubDivision;
            ddlSubdiv.DataBind();
        }
    }
    protected void btnSF_Click(object sender, EventArgs e)
    {
         Distance_calculation dv = new Distance_calculation();
        dsSubDivision = dv.getFilterRgn(divcode, ddlSubdiv.SelectedValue.ToString());
        DataTable dtAllFare = dv.getAllowFare(monthId.SelectedValue.ToString(), yearID.SelectedValue.ToString());
        DataTable dt = dv.getOtherExpDetails(divcode, ddlSubdiv.SelectedValue.ToString());
        //DataTable dt1 = dv.getFixedClmnName(divcode);
        //DataTable dt2 = dv.getmis(monthId.SelectedValue.ToString(), yearID.SelectedValue.ToString());
        //DataTable dt3 = dv.getApproveamnt(monthId.SelectedValue.ToString(), yearID.SelectedValue.ToString());
        DataTable mainTable =dsSubDivision.Tables[0];

        mainTable.Columns.Add("allowance");
        //mainTable.Columns.Add("fare");
        //mainTable.Columns.Add("Fixed_Column1");
        //mainTable.Columns.Add("Fixed_Column2");
        //mainTable.Columns.Add("Fixed_Column3");
        //mainTable.Columns.Add("Fixed_Column4");
        //mainTable.Columns.Add("Fixed_Column5");
        //mainTable.Columns.Add("Fixed_Column6");
        //mainTable.Columns.Add("mis_Amt");
        //mainTable.Columns.Add("tot");
        mainTable.Columns.Add("Status");
        //mainTable.Columns.Add("appAmnt");
        if (mainTable.Rows.Count > 0)
        {
//            double totClaimedAmnt = 0;
            foreach(DataRow row in mainTable.Rows)
            {
                double totClaimedAmnt = 0;
                String filter = "SF_Code='" + row["SF_Code"].ToString() + "'";
                DataRow[] rows = dtAllFare.Select(filter);
                DataRow[] othRows = dt.Select();
                //DataRow[] misRows = dt2.Select(filter);
                //DataRow[] appRows = dt3.Select(filter);
                //if (appRows.Count()>0)
                //{
                //    row["appAmnt"] = appRows[0]["grand_total"];
                //}
                string st = "<span style='color:red;font-weight:bold'>Not Prepared</span>";
                string MGR;
                if (rows.Count() > 0)
                {
                    MGR = rows[0].ToString();
                    //row["fare"] = rows[0]["fare"];
                     st=rows[0]["Status"].ToString();
                    if (st=="1")
                    {
                    st="<span style='background-color:yellow;color:blue;font-weight:bold'>Approval Pending</span>";
                    }
                    else if (st=="3")
                    {
                        st = "<span style='color:green;font-weight:bold'>Approved</span>";
                    }
                    else if (st == "2")
                    {
                        st = "<span style='background-color:yellow;font-weight:bold'>Manager Approved</span>";
                    }
                    else 
                    {
                        st = "<span style='color:red;font-weight:bold'>Not Prepared</span>";
                    }
                    
                    //totClaimedAmnt=totClaimedAmnt+Convert.ToDouble(row["allowance"].ToString())+Convert.ToDouble(row["fare"].ToString());
                }
                row["Status"] = st;
            //    if (misRows.Count() > 0)
            //    {
            //        row["mis_Amt"] = misRows[0]["mis_Amt"];
                    
            //        totClaimedAmnt=totClaimedAmnt+Convert.ToDouble(row["mis_Amt"].ToString());
            //    }
            //    for (int i = 0; i < othRows.Count(); i++)
            //    {
            //        row["Fixed_Column" + (i + 1)] = othRows[i]["Amount"];

            //        totClaimedAmnt = totClaimedAmnt + Convert.ToDouble(othRows[i]["Amount"].ToString());
            //    }
            //row["tot"]=totClaimedAmnt;
            }
            //for(int i=0;i<dt1.Rows.Count;i++)
            //{
            //    grdSalesForce.Columns[6+i].Visible = true;
            //    grdSalesForce.Columns[6+i].HeaderText = dt1.Rows[i]["Parameter_Name"].ToString();

            //}

            pnlprint.Visible = true;
            grdSalesForce.Visible = true;
            grdSalesForce.DataSource = mainTable;
            grdSalesForce.DataBind();
            foreach (GridViewRow gridRow in grdSalesForce.Rows)
             {
            
            //TableCell cell = new TableCell();
            HyperLink link = new HyperLink();
            Label lbl = new Label();
            HiddenField name = (HiddenField)gridRow.FindControl("sfNameHidden");
            HiddenField code = (HiddenField)gridRow.FindControl("sfCodeHidden");
            link.Text = "<span>" + name.Value + "</span>";
            lbl.Text = name.Value;
            string sURL = "RptAutoExpense_view.aspx?sf_code=" + code.Value+ "&monthId="+monthId.SelectedValue.ToString()+"&yearId="+yearID.SelectedValue.ToString()+"&divCode="+divcode+"";
                link.Attributes["onclick"] = "javascript:window.open('" + sURL + "',null,'resizable=yes,scrollbars=1,toolbar=no,menubar=no,status=no,width=500,height=600,left=0,top=0');";
                link.NavigateUrl = "#";
                //cell.Controls.Add(link);
              Label label=  (Label)gridRow.FindControl("lblstatus");
                if (label.Text.Contains("Not Prepared"))
                {
                    gridRow.Cells[1].Controls.Add(link);
                }
                else
                {
                    gridRow.Cells[1].Controls.Add(link);

                }

             }

        }
        else
        {
            grdSalesForce.DataSource = dsSubDivision;
            grdSalesForce.DataBind();
        }
    }

}