using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;

public partial class User_Count : System.Web.UI.Page
{
    #region Declarations
    DataSet dsSalesForce = null;
    DataSet dsDoctor = null;
    DataSet dsTP = null;
    string div_code = string.Empty;
    string sf_code = string.Empty;
    DataSet dsState = null;
    string Month = string.Empty;
    string Year = string.Empty;
    int count_tot = 0;
    int count_tot1 = 0;
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
      //  div_code = Session["division_code"].ToString();
    //    sf_code = Session["sf_code"].ToString();

        if (!Page.IsPostBack)
        {
            TourPlan tp = new TourPlan();
            dsTP = tp.Get_TP_Edit_Year_div();
            if (dsTP.Tables[0].Rows.Count > 0)
            {
                for (int k = Convert.ToInt16(dsTP.Tables[0].Rows[0]["Year"]); k <= DateTime.Now.Year + 1; k++)
                {
                    ddlYear.Items.Add(k.ToString());
                    ddlYear.SelectedValue = DateTime.Now.Year.ToString();
                    ddlMonth.SelectedValue = DateTime.Now.Month.ToString();
                }
            }
            ViewState["dsSalesForce"] = null;
            ViewState["dsDoctor"] = null;
         
        }

    }
    private void FillSalesForce()
    {
        int tot_rows = 0;
        int tot_cols = 0;

        // Fetch the total rows for the table
        Division div = new Division();
        Territory terr = new Territory();

        dsSalesForce = div.getDivision_Name();

        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            tot_rows = dsSalesForce.Tables[0].Rows.Count;
            ViewState["dsSalesForce"] = dsSalesForce;
        }

        Month = ddlMonth.SelectedValue;
        Year = ddlYear.SelectedValue; ; 
        CreateDynamicTable(tot_rows, tot_cols);
    }
    private void CreateDynamicTable(int tblRows, int tblCols)
    {

        if (ViewState["dsSalesForce"] != null)
        {
            ViewState["HQ_Det"] = null;


            TableRow tr_header = new TableRow();
            tr_header.BorderStyle = BorderStyle.Solid;
            tr_header.BorderWidth = 1;

            TableCell tc_SNo = new TableCell();
            tc_SNo.BorderStyle = BorderStyle.Solid;
            tc_SNo.BorderColor = System.Drawing.Color.Red;
            tc_SNo.BorderWidth = 1;

            tc_SNo.ForeColor = System.Drawing.Color.Yellow;
            Literal lit_SNo = new Literal();
            lit_SNo.Text = "<center>S.No</center>";

            tc_SNo.Controls.Add(lit_SNo);
          
            tc_SNo.Style.Add("font-family", "Tahoma");
            tc_SNo.Style.Add("font-size", "10pt");
            tr_header.Cells.Add(tc_SNo);
          
                tr_header.BackColor = System.Drawing.Color.FromName("#7AA3CC");

           
              TableCell tc_FF = new TableCell();
                tc_FF.BorderStyle = BorderStyle.Solid;
                tc_FF.BorderWidth = 1;

                tc_FF.BorderColor = System.Drawing.Color.Red;

                Literal lit_FF = new Literal();
                Territory terr = new Territory();
                // lit_FF.Text = "<center>Territory</center>";
                DataSet dsTerritory = new DataSet();
               
                lit_FF.Text = "<center>Customer Name</center>";
               
                tc_FF.Controls.Add(lit_FF);
                tc_FF.ForeColor = System.Drawing.Color.Yellow;
             
                tc_FF.Style.Add("margin-top", "20px");
                tc_FF.Style.Add("font-family", "Tahoma");
                tc_FF.Style.Add("font-size", "10pt");

                tr_header.Cells.Add(tc_FF);


                TableCell tc_Total = new TableCell();
                tc_Total.BorderStyle = BorderStyle.Solid;
                tc_Total.BorderWidth = 1;
                tc_Total.BorderColor = System.Drawing.Color.Red;

                tc_Total.ForeColor = System.Drawing.Color.Yellow;
                Literal lit_Total = new Literal();
                lit_Total.Text = "<center>User Count</center>";
                tc_Total.Controls.Add(lit_Total);
             
                tc_Total.Style.Add("font-family", "Tahoma");
                tc_Total.Style.Add("font-size", "10pt");
                tr_header.Cells.Add(tc_Total);


                TableCell tc_Totalcnt = new TableCell();
                tc_Totalcnt.BorderStyle = BorderStyle.Solid;
                tc_Totalcnt.BorderWidth = 1;
                tc_Totalcnt.BorderColor = System.Drawing.Color.Black;

                tc_Totalcnt.ForeColor = System.Drawing.Color.White;
                Literal lit_Totalcnt = new Literal();
                lit_Totalcnt.Text = "<center>Team Count</center>";
                tc_Totalcnt.Controls.Add(lit_Totalcnt);

                tc_Totalcnt.Style.Add("font-family", "Tahoma");
                tc_Totalcnt.Style.Add("font-size", "10pt");
                tr_header.Cells.Add(tc_Totalcnt);

                tbl.Rows.Add(tr_header);

                   string sURL = string.Empty;
            int iCount = 0;
            dsSalesForce = (DataSet)ViewState["dsSalesForce"];
            foreach (DataRow drFF in dsSalesForce.Tables[0].Rows)
            {
                TableRow tr_det = new TableRow();
                iCount += 1;
                TableCell tc_det_SNo = new TableCell();
                Literal lit_det_SNo = new Literal();
                lit_det_SNo.Text = "<center>" + iCount.ToString() + "</center>";
                tc_det_SNo.BorderStyle = BorderStyle.Solid;
                tc_det_SNo.BorderWidth = 1;
                tc_det_SNo.Controls.Add(lit_det_SNo);
                //tc_det_SNo.Height = 10;
                tc_det_SNo.Style.Add("font-family", "Tahoma");
                tc_det_SNo.Style.Add("font-size", "10pt");
                tr_det.Cells.Add(tc_det_SNo);
                //tr_det.Height = 10;


            //    tr_det.BackColor = System.Drawing.Color.Red;
                TableCell tc_det_FF = new TableCell();
                Literal lit_det_FF = new Literal();
                lit_det_FF.Text = "&nbsp" + drFF["division_name"].ToString();
                tc_det_FF.BorderStyle = BorderStyle.Solid;
                tc_det_FF.BorderWidth = 1;
                tc_det_FF.Controls.Add(lit_det_FF);
                tc_det_FF.Style.Add("text-align", "left");
                tc_det_FF.Style.Add("font-family", "Tahoma");
				tc_det_FF.Style.Add("color", "Maroon");
                tc_det_FF.Style.Add("font-size", "10pt");
                tr_det.Cells.Add(tc_det_FF);

                DCR dcr = new DCR();

                dsTerritory = dcr.get_All_dcr_Sf_Code_date_Count(Convert.ToInt16(Month), Convert.ToInt16(Year), drFF["division_code"].ToString());
                tr_det.BackColor = System.Drawing.Color.White;
                TableCell tc_det_tot = new TableCell();
                Literal lit_det_tot = new Literal();
                int iPendingCount = 0;
               for(int i=0;i<dsTerritory.Tables[0].Rows.Count ;i++)
                {
                  
                    iPendingCount += 1;
                    lit_det_tot.Text = iPendingCount.ToString();
                }
                if (lit_det_tot.Text != "")
               {
                   count_tot += Convert.ToInt32(lit_det_tot.Text);

               }
           
                tc_det_tot.BorderStyle = BorderStyle.Solid;
                tc_det_tot.BorderWidth = 1;
                tc_det_tot.Controls.Add(lit_det_tot);
                tc_det_tot.Style.Add("text-align", "Center");
                tc_det_tot.Style.Add("font-family", "Verdana");
				tc_det_tot.Style.Add("color", "Red");
                tc_det_tot.Style.Add("font-size", "12pt");

                tr_det.Cells.Add(tc_det_tot);


                TableCell tc_det_cnt = new TableCell();
                Literal lit_det_cnt = new Literal();
                SalesForce sf = new SalesForce();

                dsTP = sf.getSalesForce_count(drFF["division_code"].ToString());
                lit_det_cnt.Text = dsTP.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                if (lit_det_cnt.Text != "")
                {
                    count_tot1 += Convert.ToInt32(lit_det_cnt.Text);

                }
                tc_det_cnt.BorderStyle = BorderStyle.Solid;
                tc_det_cnt.BorderWidth = 1;
                tc_det_cnt.Controls.Add(lit_det_cnt);
                tc_det_cnt.Style.Add("text-align", "center");
                tc_det_cnt.Style.Add("font-family", "Calibri");
                tc_det_cnt.Style.Add("color", "Red");
                tc_det_cnt.Style.Add("font-size", "12pt");
                tr_det.Cells.Add(tc_det_cnt);

              //  iTotal_FF = 0;
              //  i = 0;
                tbl.Rows.Add(tr_det);
            
            }
        }
        TableRow tr_total = new TableRow();

        TableCell tc_Count_Total = new TableCell();
        tc_Count_Total.BorderStyle = BorderStyle.Solid;
        tc_Count_Total.BorderWidth = 1;
        //tc_catg_Total.Width = 25;
        Literal lit_Count_Total = new Literal();
        lit_Count_Total.Text = "<center>Total</center>";
        tc_Count_Total.Attributes.Add("style", "color:Red;font-weight:bold;");
        tc_Count_Total.Controls.Add(lit_Count_Total);
        tc_Count_Total.Font.Bold.ToString();
        tc_Count_Total.BackColor = System.Drawing.Color.White;
        tc_Count_Total.Attributes.Add("Class", "tbldetail_main");
        tc_Count_Total.ColumnSpan = 2;
        tc_Count_Total.Style.Add("text-align", "left");
        tc_Count_Total.Style.Add("font-family", "Calibri");
        tc_Count_Total.Attributes.Add("Class", "rptCellBorder");
        tc_Count_Total.Style.Add("font-size", "10pt");

        tr_total.Cells.Add(tc_Count_Total);

        TableCell tc_tot_catg = new TableCell();
        tc_tot_catg.BorderStyle = BorderStyle.Solid;
        tc_tot_catg.BorderWidth = 1;
        tc_tot_catg.BackColor = System.Drawing.Color.White;
        HyperLink hyp_tot_catg_det_total = new HyperLink();
        Literal lit_tot_catg = new Literal();
        lit_tot_catg.Text = "<center>" + count_tot + "</center>";
        tc_tot_catg.Controls.Add(lit_tot_catg);

        tr_total.Cells.Add(tc_tot_catg);

        TableCell tc_tot_catg1 = new TableCell();
        tc_tot_catg1.BorderStyle = BorderStyle.Solid;
        tc_tot_catg1.BorderWidth = 1;
        tc_tot_catg1.BackColor = System.Drawing.Color.White;

        Literal lit_tot_catg1 = new Literal();
        lit_tot_catg1.Text = "<center>" + count_tot1 + "</center>";
        tc_tot_catg1.Controls.Add(lit_tot_catg1);

        tr_total.Cells.Add(tc_tot_catg1);

        tbl.Rows.Add(tr_total);
    }
    protected void btnGo_Click(object sender, EventArgs e)
    {
        FillSalesForce();
    }
}