using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;

public partial class MIS_Reports_Delayed_DCR_Status : System.Web.UI.Page
{
    string div_code = string.Empty;
    DataSet dsSalesForce = null;
    DataSet dsUserList = null;
    DataSet dsDoc = null;
    DataSet dsDCR = null;
    int product_total = 0;
    int FWD_total = 0;
    DateTime dtCurrent;
    DataSet dsProduct = null;
    DataSet dsCatg = null;
    int MonColspan = 0;
    DataSet dsTP = null;
    string tot_dr = string.Empty;
    string tot_FWD = string.Empty;
    string tot_dcr_dr = string.Empty;
    int imissed_dr = -1;
    string sCurrentDate = string.Empty;
    string sf_code = string.Empty;
    DataSet dsSf = null;
    string sf_type = string.Empty;
    string MultiSf_Code = string.Empty;
    int mode = -1;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    string strSf_Code = string.Empty;
    string Monthsub = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {

        sf_code = Session["sf_code"].ToString();
        sf_type = Session["sf_type"].ToString();

        if (sf_type == "3")
        {
            div_code = Session["div_code"].ToString();
        }
        else
        {
            div_code = Session["div_code"].ToString();
        }

        if (div_code.Contains(','))
        {
            div_code = div_code.Remove(div_code.Length - 1);
        }

        if (!Page.IsPostBack)
        {
            menu1.Title = this.Page.Title;
            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);
            menu1.FindControl("btnBack").Visible = false;
            FillManagers();
            FillYear();
        }
        FillColor();

    }

    private void FillYear()
    {
        TourPlan tp = new TourPlan();
        dsTP = tp.Get_TP_Edit_Year(div_code);
        if (dsTP.Tables[0].Rows.Count > 0)
        {
            for (int k = Convert.ToInt16(dsTP.Tables[0].Rows[0]["Year"]); k <= DateTime.Now.Year + 1; k++)
            {
                ddlFYear.Items.Add(k.ToString());
                ddlTYear.Items.Add(k.ToString());
                ddlFYear.SelectedValue = DateTime.Now.Year.ToString();
                ddlTYear.SelectedValue = DateTime.Now.Year.ToString();
            }
        }

        ddlFMonth.SelectedValue = DateTime.Now.Month.ToString();
        ddlTMonth.SelectedValue = DateTime.Now.Month.ToString();
    }

    private void FillManagers()
    {
        SalesForce sf = new SalesForce();

        //if (ddlFFType.SelectedValue.ToString() == "1")
        //{
        //    ddlAlpha.Visible = false;
            dsSalesForce = sf.UserList_Hierarchy(div_code, "admin");
        //}
        //else if (ddlFFType.SelectedValue.ToString() == "0")
        //{
        //    FillSF_Alpha();
        //    ddlAlpha.Visible = true;
        //    dsSalesForce = sf.UserList_Alpha(div_code, "admin");
        //}

        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            ddlFieldForce.DataTextField = "sf_name";
            ddlFieldForce.DataValueField = "sf_code";
            ddlFieldForce.DataSource = dsSalesForce;
            ddlFieldForce.DataBind();

            ddlSF.DataTextField = "des_color";
            ddlSF.DataValueField = "sf_code";
            ddlSF.DataSource = dsSalesForce;
            ddlSF.DataBind();

        }
    }
    //private void FillSF_Alpha()
    //{
    //    SalesForce sf = new SalesForce();
    //    dsSalesForce = sf.getSalesForcelist_Alphabet(div_code);
    //    if (dsSalesForce.Tables[0].Rows.Count > 0)
    //    {
    //        ddlAlpha.DataTextField = "sf_name";
    //        ddlAlpha.DataValueField = "val";
    //        ddlAlpha.DataSource = dsSalesForce;
    //        ddlAlpha.DataBind();
    //        ddlAlpha.SelectedIndex = 0;
    //    }
    //}

    //protected void ddlAlpha_SelectedIndexChanged(object sender, EventArgs e)
    //{

    //    SalesForce sf = new SalesForce();
    //    dsSalesForce = sf.UserList_Alphasearch(div_code, "admin", ddlAlpha.SelectedValue);
    //    if (dsSalesForce.Tables[0].Rows.Count > 0)
    //    {
    //        ddlFieldForce.DataTextField = "sf_name";
    //        ddlFieldForce.DataValueField = "sf_code";
    //        ddlFieldForce.DataSource = dsSalesForce;
    //        ddlFieldForce.DataBind();

    //        ddlSF.DataTextField = "des_color";
    //        ddlSF.DataValueField = "sf_code";
    //        ddlSF.DataSource = dsSalesForce;
    //        ddlSF.DataBind();

    //    }
    //    FillColor();

    //}

    protected void ddlFFType_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblFF.Text = "Field Force";
        FillManagers();
        FillColor();
    }

    private void FillColor()
    {
        int j = 0;

        foreach (ListItem ColorItems in ddlSF.Items)
        {
            //ddlFieldForce.Items[j].Selected = true;

            string bcolor = "#" + ColorItems.Text;
            ddlFieldForce.Items[j].Attributes.Add("style", "background-color:" + bcolor);

            j = j + 1;

        }
    }
    protected void btnGo_Click(object sender, EventArgs e)
    {
        FillSalesForce();
    }

    private void FillSalesForce()
    {
        string sURL = string.Empty;

        tbl.Rows.Clear();

        SalesForce sf = new SalesForce();
        dsSalesForce = sf.UserList_get_SelfMail(div_code, ddlFieldForce.SelectedValue.ToString());

        //if (ddlFieldForce.SelectedValue.ToString() == "admin")
        //{
        //    dsSalesForce.Tables[0].Rows[0].Delete();
        //}


        if(dsSalesForce.Tables[0].Rows.Count > 0)
        {
            TableRow tr_header = new TableRow();
            tr_header.BorderStyle = BorderStyle.Solid;
            tr_header.BorderWidth = 1;

            tr_header.BackColor = System.Drawing.Color.FromName("#666699");
            tr_header.Style.Add("Color", "White");
            tr_header.BorderColor = System.Drawing.Color.Black;

            TableCell tc_SNo = new TableCell();
            tc_SNo.BorderStyle = BorderStyle.Solid;
            tc_SNo.BorderWidth = 1;
            tc_SNo.Width = 50;
            tc_SNo.RowSpan = 2;
            Literal lit_SNo =
                new Literal();
            lit_SNo.Text = "<center>S.No</center>";
            tc_SNo.BorderColor = System.Drawing.Color.Black;
            tc_SNo.Controls.Add(lit_SNo);
            tc_SNo.Style.Add("font-family", "Calibri");
            tc_SNo.Style.Add("font-size", "10pt");
            tc_SNo.Attributes.Add("Class", "rptCellBorder");
            tr_header.Cells.Add(tc_SNo);

            TableCell tc_DR_Code = new TableCell();
            tc_DR_Code.BorderStyle = BorderStyle.Solid;
            tc_DR_Code.BorderWidth = 1;
            tc_DR_Code.Width = 400;
            tc_DR_Code.RowSpan = 2;
            Literal lit_DR_Code = new Literal();
            lit_DR_Code.Text = "<center>SF Code</center>";
            tc_DR_Code.Controls.Add(lit_DR_Code);
            tc_DR_Code.Style.Add("font-family", "Calibri");
            tc_DR_Code.Style.Add("font-size", "10pt");
            tc_DR_Code.Attributes.Add("Class", "rptCellBorder");
            tc_DR_Code.BorderColor = System.Drawing.Color.Black;
            tc_DR_Code.Visible = false;
            tr_header.Cells.Add(tc_DR_Code);

            TableCell tc_DR_Name = new TableCell();
            tc_DR_Name.BorderStyle = BorderStyle.Solid;
            tc_DR_Name.BorderWidth = 1;
            tc_DR_Name.Width = 500;
            tc_DR_Name.RowSpan = 2;
            Literal lit_DR_Name = new Literal();
            lit_DR_Name.Text = "<center>Fieldforce&nbspName</center>";
            tc_DR_Name.BorderColor = System.Drawing.Color.Black;
            tc_DR_Name.Style.Add("font-family", "Calibri");
            tc_DR_Name.Style.Add("font-size", "10pt");
            tc_DR_Name.Attributes.Add("Class", "rptCellBorder");
            tc_DR_Name.Controls.Add(lit_DR_Name);
            tr_header.Cells.Add(tc_DR_Name);


            TableCell tc_DR_HQ = new TableCell();
            tc_DR_HQ.BorderStyle = BorderStyle.Solid;
            tc_DR_HQ.BorderWidth = 1;
            tc_DR_HQ.Width = 200;
            tc_DR_HQ.RowSpan = 2;
            Literal lit_DR_HQ = new Literal();
            lit_DR_HQ.Text = "<center>HQ</center>";
            tc_DR_HQ.BorderColor = System.Drawing.Color.Black;
            tc_DR_HQ.Style.Add("font-family", "Calibri");
            tc_DR_HQ.Style.Add("font-size", "10pt");
            tc_DR_HQ.Attributes.Add("Class", "rptCellBorder");
            tc_DR_HQ.Controls.Add(lit_DR_HQ);
            tr_header.Cells.Add(tc_DR_HQ);


            TableCell tc_DR_Des = new TableCell();
            tc_DR_Des.BorderStyle = BorderStyle.Solid;
            tc_DR_Des.BorderWidth = 1;
            tc_DR_Des.Width = 80;
            tc_DR_Des.RowSpan = 2;
            Literal lit_DR_Des = new Literal();
            lit_DR_Des.Text = "<center>Designation</center>";
            tc_DR_Des.BorderColor = System.Drawing.Color.Black;
            tc_DR_Des.Style.Add("font-family", "Calibri");
            tc_DR_Des.Style.Add("font-size", "10pt");
            tc_DR_Des.Attributes.Add("Class", "rptCellBorder");
            tc_DR_Des.Controls.Add(lit_DR_Des);
            tr_header.Cells.Add(tc_DR_Des);


            int months = (Convert.ToInt32(ddlTYear.SelectedValue) - Convert.ToInt32(ddlFYear.SelectedValue)) * 12 + Convert.ToInt32(ddlTMonth.SelectedValue ) - Convert.ToInt32(ddlFMonth.SelectedValue); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
            int cmonth = Convert.ToInt32(ddlFMonth.SelectedValue);
            int cyear = Convert.ToInt32(ddlFYear.SelectedValue);


            ViewState["months"] = months;
            ViewState["cmonth"] = cmonth;
            ViewState["cyear"] = cyear;

            if (months >= 0)
            {
                for (int j = 1; j <= months + 1; j++)
                {
                    TableCell tc_month = new TableCell();
                    tc_month.ColumnSpan = 1;
                    Literal lit_month = new Literal();
                    Monthsub = sf.getMonthName(cmonth.ToString()) + "-" + cyear;
                    lit_month.Text = Monthsub.Substring(0, 3) + "-" + cyear;
                    tc_month.Attributes.Add("Class", "rptCellBorder");
                    tc_month.BorderStyle = BorderStyle.Solid;
                    tc_month.BorderWidth = 1;
                    tc_month.HorizontalAlign = HorizontalAlign.Center;
                    //tc_month.Width = 200;
                    tc_month.Controls.Add(lit_month);
                    tr_header.Cells.Add(tc_month);
                    cmonth = cmonth + 1;
                    if (cmonth == 13)
                    {
                        cmonth = 1;
                        cyear = cyear + 1;
                    }
                }
            }
            tbl.Rows.Add(tr_header);

            //Sub Header
            months = Convert.ToInt16(ViewState["months"].ToString());
            cmonth = Convert.ToInt16(ViewState["cmonth"].ToString());
            cyear = Convert.ToInt16(ViewState["cyear"].ToString());

            if (months >= 0)
            {
                TableRow tr_lst_det = new TableRow();
                for (int j = 1; j <= months + 1; j++)
                {
                    TableCell tc_lst_month = new TableCell();
                    HyperLink lit_lst_month = new HyperLink();
                    lit_lst_month.Text = "Delayed Dates";
                    tc_lst_month.BorderStyle = BorderStyle.Solid;
                    tc_lst_month.HorizontalAlign = HorizontalAlign.Center;
                    tc_lst_month.BorderWidth = 1;
                    tc_lst_month.Attributes.Add("Class", "rptCellBorder");
                    tc_lst_month.Controls.Add(lit_lst_month);
                    tr_lst_det.Cells.Add(tc_lst_month);


                    cmonth = cmonth + 1;
                    if (cmonth == 13)
                    {
                        cmonth = 1;
                        cyear = cyear + 1;
                    }
                }
                tr_lst_det.BackColor = System.Drawing.Color.FromName("#666699");
                tr_lst_det.Style.Add("Color", "White");

                tr_lst_det.Attributes.Add("Class", "Backcolor");

                tbl.Rows.Add(tr_lst_det);
            }

             if (dsSalesForce.Tables[0].Rows.Count > 0)
                ViewState["dsSalesForce"] = dsSalesForce;


            int iCount = 0;
            int iTotLstCount = 0;
            dsSalesForce = (DataSet)ViewState["dsSalesForce"];

            foreach (DataRow drFF in dsSalesForce.Tables[0].Rows)
            {
                TableRow tr_det = new TableRow();
                iCount += 1;
                strSf_Code += "'" + drFF["sf_code"].ToString() + "'" + ",";


                //S.No
                TableCell tc_det_SNo = new TableCell();
                Literal lit_det_SNo = new Literal();
                lit_det_SNo.Text = iCount.ToString();
                tc_det_SNo.BorderStyle = BorderStyle.Solid;
                tc_det_SNo.BorderWidth = 1;
                tc_det_SNo.Style.Add("font-family", "Calibri");
                tc_det_SNo.Style.Add("font-size", "10pt");
                tc_det_SNo.Style.Add("text-align", "left");
                tc_det_SNo.Attributes.Add("Class", "rptCellBorder");
                tc_det_SNo.Controls.Add(lit_det_SNo);
                tr_det.Cells.Add(tc_det_SNo);
                tr_det.BackColor = System.Drawing.Color.White;

                //SF_code
                TableCell tc_det_usr = new TableCell();
                Literal lit_det_usr = new Literal();
                lit_det_usr.Text = "&nbsp;" + drFF["Sf_Code"].ToString();
                tc_det_usr.BorderStyle = BorderStyle.Solid;
                tc_det_usr.BorderWidth = 1;
                tc_det_usr.Visible = false;
                tc_det_usr.Style.Add("font-family", "Calibri");
                tc_det_usr.Style.Add("font-size", "10pt");
                tc_det_usr.Style.Add("text-align", "left");
                tc_det_usr.Attributes.Add("Class", "rptCellBorder");
                tc_det_usr.Controls.Add(lit_det_usr);
                tr_det.Cells.Add(tc_det_usr);

                //SF Name
                TableCell tc_det_FF = new TableCell();
                Literal lit_det_FF = new Literal();
                lit_det_FF.Text = "&nbsp;" + drFF["sf_name"].ToString();
                tc_det_FF.BorderStyle = BorderStyle.Solid;
                tc_det_FF.BorderWidth = 1;
                tc_det_FF.Style.Add("font-family", "Calibri");
                tc_det_FF.Style.Add("font-size", "10pt");
                tc_det_FF.Style.Add("text-align", "left");
                tc_det_FF.Attributes.Add("Class", "rptCellBorder");
                tc_det_FF.Controls.Add(lit_det_FF);
                tr_det.Cells.Add(tc_det_FF);

                //hq
                TableCell tc_det_hq = new TableCell();
                Literal lit_det_hq = new Literal();
                lit_det_hq.Text = "&nbsp;" + drFF["sf_hq"].ToString();
                tc_det_hq.BorderStyle = BorderStyle.Solid;
                tc_det_hq.BorderWidth = 1;
                tc_det_hq.Style.Add("font-family", "Calibri");
                tc_det_hq.Style.Add("font-size", "10pt");
                tc_det_hq.Style.Add("text-align", "left");
                tc_det_hq.Attributes.Add("Class", "rptCellBorder");
                tc_det_hq.Controls.Add(lit_det_hq);
                tr_det.Cells.Add(tc_det_hq);

                //SF Designation Short Name
                TableCell tc_det_Designation = new TableCell();
                Literal lit_det_Designation = new Literal();
                lit_det_Designation.Text = "&nbsp;" + drFF["Designation_Short_Name"].ToString();
                tc_det_Designation.BorderStyle = BorderStyle.Solid;
                tc_det_Designation.BorderWidth = 1;
                tc_det_Designation.Style.Add("font-family", "Calibri");
                tc_det_Designation.Style.Add("font-size", "10pt");
                tc_det_Designation.Style.Add("text-align", "left");
                tc_det_Designation.Controls.Add(lit_det_Designation);
                tc_det_Designation.Attributes.Add("Class", "rptCellBorder");
                tr_det.Cells.Add(tc_det_Designation);

                months = Convert.ToInt16(ViewState["months"].ToString());
                cmonth = Convert.ToInt16(ViewState["cmonth"].ToString());
                cyear = Convert.ToInt16(ViewState["cyear"].ToString());


                if (months >= 0)
                {

                    for (int j = 1; j <= months + 1; j++)
                    {

                        if (cmonth == 12)
                        {
                            sCurrentDate = "01-01-" + (cyear + 1);
                        }
                        else
                        {
                            sCurrentDate = (cmonth + 1) + "-01-" + cyear;
                        }

                        dtCurrent = Convert.ToDateTime(sCurrentDate);


                        dsDoc = sf.getDCR_Delayed_Dates(drFF["sf_code"].ToString(), div_code, cmonth, cyear);


                        if (dsDoc.Tables[0].Rows.Count > 0)
                            tot_dr = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();


                        TableCell tc_lst_month = new TableCell();
                        HyperLink hyp_lst_month = new HyperLink();

                        if (tot_dr != "")
                        {
                            //iTotLstCount += Convert.ToInt16(tot_dr);
                            hyp_lst_month.Text = tot_dr;

                            //sURL = "&sf_code=" + drFF["Sf_Code"] + "&sf_name=" + drFF["sf_name"] + "&Year=" + cyear + "&Month=" + cmonth + "&Prod_Name=" + Prod_Name + "&Prod=" + Prod + "&sCurrentDate=" + sCurrentDate + "";

                            //hyp_lst_month.Attributes.Add("href", "javascript:showModalPopUp('" + drFF["sf_code"].ToString() + "', '" + drFF["sf_name"] + "', '" + cyear + "', '" + cmonth + "', '" + Prod_Name + "', '" + Prod + "','" + sCurrentDate + "')");
                            ////hyp_lst_month.Attributes["onclick"] = "javascript:showModalPopUp('" + sURL + "')";
                            //hyp_lst_month.NavigateUrl = "#";

                        }

                        else
                        {
                            hyp_lst_month.Text = "";
                        }


                        tc_lst_month.BorderStyle = BorderStyle.Solid;
                        tc_lst_month.BorderWidth = 1;
                        tc_lst_month.BackColor = System.Drawing.Color.White;
                        tc_lst_month.Width = 200;
                        tc_lst_month.HorizontalAlign = HorizontalAlign.Center;
                        tc_lst_month.VerticalAlign = VerticalAlign.Middle;
                        tc_lst_month.Controls.Add(hyp_lst_month);
                        tc_lst_month.Attributes.Add("Class", "rptCellBorder");
                        tr_det.Cells.Add(tc_lst_month);

                        cmonth = cmonth + 1;
                        if (cmonth == 13)
                        {
                            cmonth = 1;
                            cyear = cyear + 1;
                        }
                        tot_dr = "";
                    }
                    //

                }

                tbl.Rows.Add(tr_det);

            
            }
        }
    }
}