using DBase_EReport;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MIS_Reports_rpt_Retailing_Summary : System.Web.UI.Page
{
    string sfCode = string.Empty;
    string sfname = string.Empty;
    string divcode = string.Empty;
    string month = string.Empty;
    string year = string.Empty;
    string subdiv_code = string.Empty;
    DataTable dt = null;
	int cellCount = 0;
    GridViewRow row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Normal);
	
    protected void Page_Load(object sender, EventArgs e)
    {
        divcode = Session["div_code"].ToString();
        sfCode = Request.QueryString["SF_Code"].ToString();
        sfname = Request.QueryString["SF_Name"].ToString();
        month = Request.QueryString["month"].ToString();
        year = Request.QueryString["year"].ToString();
        subdiv_code = Request.QueryString["subdiv"].ToString();
        lblHead.Text = "Retailing Summary";
        lblsf_name.Text = sfname;
        Fillsummary();
    }
    private void Fillsummary()
    {

        DataSet dsGV = new DataSet();
        DataSet dsGc = new DataSet();
        DB_EReporting db_ER = new DB_EReporting();
        //test dc = new test();

        dsGV = db_ER.Exec_DataSet("exec sp_retailing_summary '"+ divcode + "','"+ sfCode + "','"+ month + "','"+ year + "'");
        
		if(divcode=="29" || divcode == "4")
        {
            if (dsGV.Tables[0].Rows.Count > 0)
            {
                dsGV.Tables[0].Columns.RemoveAt(1);
                dsGV.Tables[0].Columns.RemoveAt(2);
                dsGV.Tables[0].Columns.RemoveAt(3);
                dsGV.Tables[0].Columns.RemoveAt(4);
                dsGV.Tables[0].Columns.RemoveAt(5);
                dsGV.Tables[0].Columns.RemoveAt(5);
                dsGV.Tables[0].Columns.RemoveAt(5);
                dsGV.Tables[0].Columns.RemoveAt(5);
                dsGV.Tables[0].Columns.RemoveAt(5);
                dsGV.Tables[0].Columns.RemoveAt(5);
				dsGV.Tables[0].Columns.RemoveAt(7);
                gvtotalorder.DataSource = dsGV;
                dt = dsGV.Tables[0];
                gvtotalorder.DataBind();
            }
            else
            {
                gvtotalorder.DataSource = null;
                gvtotalorder.DataBind();
            }
        }
		else
		{
		if (dsGV.Tables[0].Rows.Count > 0)
        {
            dsGV.Tables[0].Columns.RemoveAt(1);
            dsGV.Tables[0].Columns.RemoveAt(2);
            dsGV.Tables[0].Columns.RemoveAt(3);
            dsGV.Tables[0].Columns.RemoveAt(5);
            dsGV.Tables[0].Columns.RemoveAt(6);
            dsGV.Tables[0].Columns.RemoveAt(6);
            dsGV.Tables[0].Columns.RemoveAt(6);
            dsGV.Tables[0].Columns.RemoveAt(6);
            dsGV.Tables[0].Columns.RemoveAt(6);
            dsGV.Tables[0].Columns.RemoveAt(6);
            gvtotalorder.DataSource = dsGV;
            dt = dsGV.Tables[0];
            gvtotalorder.DataBind();
        }
        else
        {
            gvtotalorder.DataSource = null;
            gvtotalorder.DataBind();
        }
		}
    }

    protected void OnDataBound(object sender, EventArgs e)
    {
		
        decimal totcalls = 0;
        decimal prodcalls = 0;
        TimeSpan duration;
        TimeSpan ff=TimeSpan.Zero;
		if(divcode=="207")
	   {
        for (int i = 0; i < gvtotalorder.Rows.Count; i++)
        {
            if (gvtotalorder.Rows[i].Cells[6].Text == "&nbsp;" || gvtotalorder.Rows[i].Cells[7].Text == "&nbsp;")
            {
                duration = new TimeSpan(0, 0, 0);
                gvtotalorder.Rows[i].Cells[8].Text = Convert.ToString(duration);
            }
            else
            {
                duration = Convert.ToDateTime(gvtotalorder.Rows[i].Cells[7].Text).Subtract(Convert.ToDateTime(gvtotalorder.Rows[i].Cells[6].Text));
                gvtotalorder.Rows[i].Cells[8].Text = Convert.ToString(duration);
            }
			
			ff += duration;
            totcalls += Convert.ToDecimal(gvtotalorder.Rows[i].Cells[9].Text);
            prodcalls += Convert.ToDecimal(gvtotalorder.Rows[i].Cells[10].Text);
			
        }
		this.AddTotalRoww("Grand Total", totcalls.ToString(), prodcalls.ToString(), ff.ToString());
		for (int t = 12; t < cellCount; t++)
        {
            decimal ptot = 0;
            for (int k = 0; k < gvtotalorder.Rows.Count; k++)
            {
                ptot += Convert.ToDecimal(gvtotalorder.Rows[k].Cells[t].Text);
            }
            this.AddTotalRoww1(ptot.ToString(),t);
        }
		}
		

    }
    private void AddTotalRoww(string labelText, string value0, string value1,string value2)
    { 
        //total for duration,total,productive calls Only
        row.CssClass = "GrndTot";
        row.Cells.AddRange(new TableCell[] { new TableCell (), //Empty Cell
                                        new TableCell { Text = labelText, HorizontalAlign = HorizontalAlign.Center,ColumnSpan=7},
                                        new TableCell { Text = value2, HorizontalAlign = HorizontalAlign.Left } ,
                                        new TableCell { Text = value0, HorizontalAlign = HorizontalAlign.Left } ,
                                        new TableCell { Text = value1, HorizontalAlign = HorizontalAlign.Left } ,
                                        new TableCell { Text = "-", HorizontalAlign = HorizontalAlign.Center } ,


        });

        gvtotalorder.Controls[0].Controls.Add(row);
    }
	
	private void AddTotalRoww1(string value,int t)
    {
        //Total for all Product_categary 
        row.CssClass = "GrndTot";
        row.Cells.AddRange(new TableCell[] {
             new TableCell { Text = value, HorizontalAlign = HorizontalAlign.Left } ,

              });

        if((t+1)== cellCount)
        {
            gvtotalorder.Controls[0].Controls.Add(row);
        }
        
    }
	
	
	private void AddTotalRoww12(string labelText, string value0, string value1,string value2,string value3,string value4)
    {
        //string empty = "";
        GridViewRow row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Normal);
        //row.BackColor = ColorTranslator.FromHtml("#ecf19f");

        row.CssClass = "GrndTot";
		
		if(divcode=="176")
		{
			
			row.Cells.AddRange(new TableCell[] { new TableCell (), //Empty Cell
                                        new TableCell { Text = labelText, HorizontalAlign = HorizontalAlign.Center,ColumnSpan=5},

                                        new TableCell { Text = value0, HorizontalAlign = HorizontalAlign.Left } ,
                                        new TableCell { Text = value1, HorizontalAlign = HorizontalAlign.Left } ,
                                        new TableCell { Text = "-", HorizontalAlign = HorizontalAlign.Center } ,
                                        new TableCell { Text = value2, HorizontalAlign = HorizontalAlign.Left } ,
                                        new TableCell { Text = value3, HorizontalAlign = HorizontalAlign.Left } ,
                                        new TableCell { Text = value4, HorizontalAlign = HorizontalAlign.Left } ,


        });
		}

        gvtotalorder.Controls[0].Controls.Add(row);
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            int count = gvtotalorder.Rows.Count;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
				cellCount = e.Row.Cells.Count;// Get the no. of columns in the first row.
 
                e.Row.Cells[6].CssClass = "totcalls";
                e.Row.Cells[7].CssClass = "prodcalls";
                e.Row.Cells[6].Attributes.Add("value", e.Row.Cells[6].Text);
                e.Row.Cells[7].Attributes.Add("value", e.Row.Cells[7].Text);
                for (int colIndex = 9; colIndex < e.Row.Cells.Count; colIndex++)
                {
                    e.Row.Cells[colIndex].CssClass = "aclass" + colIndex;
                    //gvtotalorder.CssClass = "aclass";
                    //gvtotalorder.Attributes.Add("class", "aclass");
                    
                    //Label txtName = new Label();
                    ////txtName.Width = 20;
                    //txtName.ID = "txtboxname" + colIndex;
                    //txtName.Text = e.Row.Cells[colIndex].Text;
                    if (e.Row.Cells[colIndex].Text == "&nbsp;")
                    {
                        e.Row.Cells[colIndex].Text="0";
                    }
                    //Label Salary = (Label)e.Row.FindControl("lblSalary");
                    //int addedSalary = 10 + int.Parse(Salary.Text);
                }
                //cnt++;
            }
            //if(count==cnt)
            //{
            //    Decimal Totalcalls = Convert.ToDecimal(dt.Compute("SUM(TotalCalls)", string.Empty));//4379M
            //    Decimal TotalPrice = Convert.ToDecimal(dt.Compute("SUM(ProdCalls)", string.Empty));//2162M
            //    //gvtotalorder.Columns[5].Text = "Grand Total";
            //}
            //if (e.Row.RowType == DataControlRowType.Footer)
            //{
            //    Decimal Totalcalls = Convert.ToDecimal(dt.Compute("SUM(TotalCalls)", string.Empty));//4379M
            //    Decimal TotalPrice = Convert.ToDecimal(dt.Compute("SUM(ProdCalls)", string.Empty));//2162M
            //    //gvtotalorder.Columns[5].FooterText = "Grand Total";
            //    gvtotalorder.FooterRow.Cells[0].Text = Totalcalls.ToString();
            //}
        }
        catch (Exception ex)
        {

        }
    }
 }