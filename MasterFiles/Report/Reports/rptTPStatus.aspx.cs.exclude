using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;

public partial class Reports_rptTPStatus : System.Web.UI.Page
{
    int state_code = -1;
    int iMonth = -1;
    int iYear = -1;
    bool isVacant;
    string div_code = string.Empty;

    DataSet dsFF = null;
    DataSet dsState = null;
    DateTime entry_date;
    DateTime confirm_date;
    

    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        state_code = Convert.ToInt32( Request.QueryString["state_code"].ToString());
        iMonth  = Convert.ToInt32(Request.QueryString["cur_month"].ToString());
        iYear  = Convert.ToInt32(Request.QueryString["cur_year"].ToString());
        isVacant = Convert.ToBoolean(Request.QueryString["vacant"].ToString());
        FillSalesForce();
    }

    private void FillSalesForce()
    {

        // Fetch the total rows for the table
        SalesForce sf = new SalesForce();

        if (isVacant)
        {
            dsFF = sf.UserList_TP_Status(div_code, state_code);
        }
        else
        {
            dsFF = sf.UserList_TP_Status_All(div_code, state_code);
        }

        if (dsFF.Tables[0].Rows.Count > 0)
            CreateDynamicTable(dsFF);
    }

    private void CreateDynamicTable(DataSet dsFF)
    {
        if (dsFF != null)
        {
            TourPlan tp = new TourPlan();
            TableRow tr_header = new TableRow();
            tr_header.BorderStyle = BorderStyle.Solid;
            tr_header.BorderWidth = 1;

            TableCell tc_SNo = new TableCell();
            tc_SNo.BorderStyle = BorderStyle.Solid;
            tc_SNo.BorderWidth = 1;
            tc_SNo.Width = 50;
            Literal lit_SNo = new Literal();
            lit_SNo.Text = "<center><b>S.No</b></center>";
            tc_SNo.Controls.Add(lit_SNo);
            tc_SNo.RowSpan = 2;
            tr_header.Cells.Add(tc_SNo);

            TableCell tc_user = new TableCell();
            tc_user.BorderStyle = BorderStyle.Solid;
            tc_user.BorderWidth = 1;
            tc_user.Width = 100;
            Literal lit_user = new Literal();
            lit_user.Text = "<center><b>User Name</b></center>";
            tc_user.Controls.Add(lit_user);
            tc_user.RowSpan = 2;
            tr_header.Cells.Add(tc_user);

            TableCell tc_FF = new TableCell();
            tc_FF.BorderStyle = BorderStyle.Solid;
            tc_FF.BorderWidth = 1;
            tc_FF.Width = 400;
            Literal lit_FF = new Literal();
            lit_FF.Text = "<center><b>Field Force Name</b></center>";
            tc_FF.Controls.Add(lit_FF);
            tc_FF.RowSpan = 2;
            tr_header.Cells.Add(tc_FF);

            TableCell tc_tp = new TableCell();
            Literal lit_tp = new Literal();
            lit_tp.Text = "<center><b>Tour Plan</b></center>";
            tc_tp.Controls.Add(lit_tp);
            tc_tp.BorderStyle = BorderStyle.Solid;
            tc_tp.BorderWidth = 1;
            tc_tp.ColumnSpan = 2;
            tr_header.Cells.Add(tc_tp);
            
            tbl.Rows.Add(tr_header);

            TableRow tr_tp = new TableRow();

            TableCell tc_entry = new TableCell();
            tc_entry.BorderStyle = BorderStyle.Solid;
            tc_entry.BorderWidth = 1;
            tc_entry.Width = 100;
            Literal lit_entry = new Literal();
            lit_entry.Text = "<center><b>Entry Date</b></center>";
            tc_entry.Controls.Add(lit_entry);
            tr_tp.Cells.Add(tc_entry);

            TableCell tc_confirm = new TableCell();
            tc_confirm.BorderStyle = BorderStyle.Solid;
            tc_confirm.BorderWidth = 1;
            tc_confirm.Width = 100;
            Literal lit_confirm = new Literal();
            lit_confirm.Text = "<center><b>Confirm Date</b></center>";
            tc_confirm.Controls.Add(lit_confirm);
            tr_tp.Cells.Add(tc_confirm);

            tbl.Rows.Add(tr_tp);

            // Details Section
            string sURL = string.Empty;
            int iCount = 0;
            string sTab = string.Empty;

            foreach (DataRow drFF in dsFF.Tables[0].Rows)
            {
                TableRow tr_det = new TableRow();
                iCount += 1;
                TableCell tc_det_SNo = new TableCell();
                Literal lit_det_SNo = new Literal();
                lit_det_SNo.Text = "<center>" + iCount.ToString() + "</center>";
                tc_det_SNo.BorderStyle = BorderStyle.Solid;
                tc_det_SNo.BorderWidth = 1;
                tc_det_SNo.Controls.Add(lit_det_SNo);
                tr_det.Cells.Add(tc_det_SNo);

                TableCell tc_det_FF = new TableCell();
                Literal lit_det_FF = new Literal();
                if (drFF["sf_color"].ToString().Trim() == "Level1")
                {
                    sTab = "&nbsp;";
                }
                else if (drFF["sf_color"].ToString().Trim() == "Level2")
                {
                    sTab = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                }
                else if (drFF["sf_color"].ToString().Trim() == "Level3")
                {
                    sTab = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                }
                else if (drFF["sf_color"].ToString().Trim() == "Level4")
                {
                    sTab = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                }

                lit_det_FF.Text = "&nbsp;" + drFF["sf_username"].ToString();
                tc_det_FF.BorderStyle = BorderStyle.Solid;
                tc_det_FF.BorderWidth = 1;
                tc_det_FF.Controls.Add(lit_det_FF);
                tr_det.Cells.Add(tc_det_FF);

                //tc_det_SNo.Height = 10;
                
                //tr_det.Height = 10;

                TableCell tc_det_user = new TableCell();
                Literal lit_det_user = new Literal();
                lit_det_user.Text = "&nbsp;" + sTab + drFF["sf_name"].ToString();
                tc_det_user.BorderStyle = BorderStyle.Solid;
                tc_det_user.BorderWidth = 1;
                tc_det_user.Controls.Add(lit_det_user);
                tr_det.Cells.Add(tc_det_user);

                TableCell tc_det_entry = new TableCell();
                Literal lit_det_entry = new Literal();
                lit_det_entry.Text = "";
                tc_det_entry.BorderStyle = BorderStyle.Solid;
                tc_det_entry.BorderWidth = 1;

                TableCell tc_det_confirm = new TableCell();
                Literal lit_det_confirm = new Literal();
                lit_det_confirm.Text = "";
                tc_det_confirm.BorderStyle = BorderStyle.Solid;
                tc_det_confirm.BorderWidth = 1;


                dsState = tp.get_TP_Entry_Confirm(drFF["sf_code"].ToString(),iMonth, iYear);
                if (dsState.Tables[0].Rows.Count > 0)
                {
                    if(dsState.Tables[0].Rows[0].ItemArray.GetValue(0).ToString().Length >0)
                        entry_date = Convert.ToDateTime(dsState.Tables[0].Rows[0].ItemArray.GetValue(0).ToString());

                    if (dsState.Tables[0].Rows[0].ItemArray.GetValue(1).ToString().Length > 0)
                        confirm_date = Convert.ToDateTime(dsState.Tables[0].Rows[0].ItemArray.GetValue(1).ToString());

                    lit_det_entry.Text = "<center>" + entry_date.ToString("MM-dd-yyyy") + "</center>";
                    lit_det_confirm.Text = "<center>" + confirm_date.ToString("MM-dd-yyyy") + "</center>";
                }

                tc_det_entry.Controls.Add(lit_det_entry);
                tr_det.Cells.Add(tc_det_entry);

                tc_det_confirm.Controls.Add(lit_det_confirm);
                tr_det.Cells.Add(tc_det_confirm);

                tbl.Rows.Add(tr_det);
            }

        }
    }

}