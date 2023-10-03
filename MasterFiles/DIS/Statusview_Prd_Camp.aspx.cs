using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
using System.Drawing.Imaging;

public partial class MIS_Reports_Statusview_Prd_Camp : System.Web.UI.Page
{
    string div_code = string.Empty;
    DataSet dsSalesForce = null;
    DataSet dsDoc = null;
    DataSet dsDivision = null;
    DataSet dsdiv = null;
    string tot_dr = string.Empty;
    string total_doc = string.Empty;
    string tot_FWD = string.Empty;
    string tot_dcr_dr = string.Empty;
    string sCurrentDate = string.Empty;
    string sf_code = string.Empty;
    string sf_type = string.Empty;
    string MultiSf_Code = string.Empty;
    int mode = -1;
    string strSf_Code = string.Empty;
    DateTime ServerStartTime;
    DateTime ServerEndTime;

    protected void Page_Load(object sender, EventArgs e)
    {
        sf_code = Session["sf_code"].ToString();
        sf_type = Session["sf_type"].ToString();

        if (sf_type == "3")
        {
            div_code = Session["division_code"].ToString();
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
            Filldiv();
            menu1.Title = this.Page.Title;
            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);
            menu1.FindControl("btnBack").Visible = false;
            FillManagers();
        }
        FillColor();
    }

    private void Filldiv()
    {
        Division dv = new Division();
        if (sf_type == "3")
        {
            string[] strDivSplit = div_code.Split(',');
            foreach (string strdiv in strDivSplit)
            {
                if (strdiv != "")
                {
                    dsdiv = dv.getDivisionHO(strdiv);
                    ListItem liTerr = new ListItem();
                    liTerr.Value = dsdiv.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                    liTerr.Text = dsdiv.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                    ddlDivision.Items.Add(liTerr);
                }
            }
        }
        else
        {
            dsDivision = dv.getDivision_Name();
            if (dsDivision.Tables[0].Rows.Count > 0)
            {
                ddlDivision.DataTextField = "Division_Name";
                ddlDivision.DataValueField = "Division_Code";
                ddlDivision.DataSource = dsDivision;
                ddlDivision.DataBind();
            }
        }
    }

    protected void ddlDivision_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillManagers();
        FillColor();
    }

    private void FillManagers()
    {
        SalesForce sf = new SalesForce();

        if (ddlFFType.SelectedValue.ToString() == "1")
        {
            ddlAlpha.Visible = false;
            dsSalesForce = sf.UserList_Hierarchy(ddlDivision.SelectedValue.ToString(), "admin");
        }
        else if (ddlFFType.SelectedValue.ToString() == "0")
        {
            FillSF_Alpha();
            ddlAlpha.Visible = true;
            dsSalesForce = sf.UserList_Alpha(ddlDivision.SelectedValue.ToString(), "admin");
        }

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
    private void FillSF_Alpha()
    {
        SalesForce sf = new SalesForce();
        dsSalesForce = sf.getSalesForcelist_Alphabet(ddlDivision.SelectedValue.ToString());
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            ddlAlpha.DataTextField = "sf_name";
            ddlAlpha.DataValueField = "val";
            ddlAlpha.DataSource = dsSalesForce;
            ddlAlpha.DataBind();
            ddlAlpha.SelectedIndex = 0;
        }
    }

    protected void ddlAlpha_SelectedIndexChanged(object sender, EventArgs e)
    {

        SalesForce sf = new SalesForce();
        dsSalesForce = sf.UserList_Alphasearch(ddlDivision.SelectedValue.ToString(), "admin", ddlAlpha.SelectedValue);
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
        FillColor();

    }

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
        //doctor_total = 0;

        SalesForce sf = new SalesForce();
        dsSalesForce = sf.sp_UserList_getMR_Doc_List(ddlDivision.SelectedValue.ToString(), ddlFieldForce.SelectedValue.ToString());


        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            TableRow tr_header = new TableRow();
            tr_header.BorderStyle = BorderStyle.Solid;
            tr_header.BorderWidth = 1;

            tr_header.BackColor = System.Drawing.Color.FromName("#0097AC");
            tr_header.Style.Add("Color", "White");
            tr_header.BorderColor = System.Drawing.Color.Black;

            TableCell tc_SNo = new TableCell();
            tc_SNo.BorderStyle = BorderStyle.Solid;
            tc_SNo.BorderWidth = 1;
            tc_SNo.Width = 50;
            tc_SNo.RowSpan = 1;
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
            tc_DR_Code.RowSpan = 1;
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
            tc_DR_Name.Width = 200;
            tc_DR_Name.RowSpan = 1;
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
            tc_DR_HQ.Width = 100;
            tc_DR_HQ.RowSpan = 1;
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
            tc_DR_Des.RowSpan = 1;
            Literal lit_DR_Des = new Literal();
            lit_DR_Des.Text = "<center>Designation</center>";
            tc_DR_Des.BorderColor = System.Drawing.Color.Black;
            tc_DR_Des.Style.Add("font-family", "Calibri");
            tc_DR_Des.Style.Add("font-size", "10pt");
            tc_DR_Des.Attributes.Add("Class", "rptCellBorder");
            tc_DR_Des.Controls.Add(lit_DR_Des);
            tr_header.Cells.Add(tc_DR_Des);

            TableCell tc_status = new TableCell();
            tc_status.BorderStyle = BorderStyle.Solid;
            tc_status.BorderWidth = 1;
            tc_status.Width = 80;
            tc_status.RowSpan = 1;
            Literal lit_status = new Literal();
            lit_status.Text = "<center>Status</center>";
            tc_status.BorderColor = System.Drawing.Color.Black;
            tc_status.Attributes.Add("Class", "rptCellBorder");
            tc_status.Style.Add("font-family", "Calibri");
            tc_status.Style.Add("font-size", "10pt");
            tc_status.Controls.Add(lit_status);
            tr_header.Cells.Add(tc_status);


            tbl.Rows.Add(tr_header);

            if (Session["sf_type"].ToString() == "" || Session["sf_type"].ToString() == "3")
            {
                tr_header.BackColor = System.Drawing.Color.FromName("#336277");
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
                lit_det_SNo.Text = "<center>" + iCount.ToString() + "</center>";
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
                lit_det_Designation.Text = "&nbsp;" + drFF["sf_Designation_Short_Name"].ToString();
                tc_det_Designation.BorderStyle = BorderStyle.Solid;
                tc_det_Designation.BorderWidth = 1;
                tc_det_Designation.Style.Add("font-family", "Calibri");
                tc_det_Designation.Style.Add("font-size", "10pt");
                tc_det_Designation.Style.Add("text-align", "left");
                tc_det_Designation.Controls.Add(lit_det_Designation);
                tc_det_Designation.Attributes.Add("Class", "rptCellBorder");
                tr_det.Cells.Add(tc_det_Designation);


                TableCell tc_tot_month = new TableCell();
                HyperLink hyp_month = new HyperLink();
                iTotLstCount = 0;


                //if (ddlmode.SelectedValue == "1")
                //{

                dsDoc = sf.getDrprdMap_Status(drFF["sf_code"].ToString(), ddlDivision.SelectedValue.ToString(), ddlmode.SelectedValue);

                if (dsDoc.Tables[0].Rows.Count > 0)
                    tot_dr = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                total_doc = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                //}



                TableCell tc_lst_month = new TableCell();
                HyperLink hyp_lst_month = new HyperLink();

                if (tot_dr != "0")
                {
                    //iTotLstCount += Convert.ToInt16(tot_dr);
                    hyp_lst_month.Text = tot_dr + "/" + total_doc;

                    //sURL = "&sf_code=" + drFF["Sf_Code"] + "&sf_name=" + drFF["sf_name"] + "&Year=" + cyear + "&Month=" + cmonth + "&Prod_Name=" + Prod_Name + "&Prod=" + Prod + "&sCurrentDate=" + sCurrentDate + "";

                    //hyp_lst_month.Attributes.Add("href", "javascript:showModalPopUp('" + drFF["sf_code"].ToString() + "', '" + drFF["sf_name"] + "', '" + cyear + "', '" + cmonth + "', '" + Prod_Name + "', '" + Prod + "','" + sCurrentDate + "')");
                    //hyp_lst_month.Attributes["onclick"] = "javascript:showModalPopUp('" + sURL + "')";
                    //hyp_lst_month.NavigateUrl = "#";
                    hyp_lst_month.BackColor = System.Drawing.Color.Yellow;
                    //hyp_lst_month.Width = 50;

                }

                else
                {
                    hyp_lst_month.Text = "-";
                }


                tc_lst_month.BorderStyle = BorderStyle.Solid;
                tc_lst_month.BorderWidth = 1;
                tc_lst_month.BackColor = System.Drawing.Color.White;
                tc_lst_month.Width = 200;
                tc_lst_month.HorizontalAlign = HorizontalAlign.Center;
                tc_lst_month.VerticalAlign = VerticalAlign.Middle;
                tc_lst_month.Controls.Add(hyp_lst_month);
                tc_lst_month.Style.Add("font-family", "Calibri");
                tc_lst_month.Style.Add("font-size", "10pt");
                tc_lst_month.Style.Add("text-align", "center");
                tc_lst_month.Attributes.Add("Class", "rptCellBorder");
                tr_det.Cells.Add(tc_lst_month);

                tbl.Rows.Add(tr_det);
            }

        }
        else
        {
            TableRow tr_det_sno = new TableRow();
            TableCell tc_det_SNo = new TableCell();
            Literal lit_det_SNo = new Literal();
            lit_det_SNo.Text = "No Record Found";
            tc_det_SNo.BorderStyle = BorderStyle.Solid;
            tc_det_SNo.Attributes.Add("Class", "NoRecord");

            tc_det_SNo.HorizontalAlign = HorizontalAlign.Center;
            tc_det_SNo.BorderWidth = 1;
            tc_det_SNo.BorderStyle = BorderStyle.None;
            tc_det_SNo.Controls.Add(lit_det_SNo);
            tr_det_sno.Cells.Add(tc_det_SNo);

            tbl.Rows.Add(tr_det_sno);
        }
    }
}