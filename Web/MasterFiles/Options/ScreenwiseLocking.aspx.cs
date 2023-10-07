using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;

public partial class MasterFiles_Options_ScreenwiseLocking : System.Web.UI.Page
{
    string div_code = string.Empty;
    DataSet dsSalesForce = null;
    string sfCode = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        sfCode = Session["sf_code"].ToString();
        if (!Page.IsPostBack)
        {
            FillManagers();
            FillColor();
            menu1.Title = this.Page.Title;
            menu1.FindControl("btnBack").Visible = false;
        }

    }

    protected void ddlFFType_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillManagers();
        FillColor();
    }

    private void FillColor()
    {
        int j = 0;


        foreach (ListItem ColorItems in ddlSF.Items)
        {
            //ddlFieldForce.Items[j].Selected = true;

            if (ColorItems.Text == "Level1")
                //ColorItems.Attributes.Add("style", "background-color: Wheat");
                ddlFieldForce.Items[j].Attributes.Add("style", "background-color: Wheat");

            if (ColorItems.Text == "Level2")
                //ColorItems.Attributes.Add("style", "background-color: Blue");
                ddlFieldForce.Items[j].Attributes.Add("style", "background-color: LightGreen");

            if (ColorItems.Text == "Level3")
                //ColorItems.Attributes.Add("style", "background-color: Cyan");
                ddlFieldForce.Items[j].Attributes.Add("style", "background-color: Pink");

            if (ColorItems.Text == "Level4")
                //ColorItems.Attributes.Add("style", "background-color: Lavendar");
                ddlFieldForce.Items[j].Attributes.Add("style", "background-color: Lavendar");

            j = j + 1;

        }
    }

    protected void ddlAlpha_SelectedIndexChanged(object sender, EventArgs e)
    {
        SalesForce sf = new SalesForce();
        dsSalesForce = sf.UserListTP_Alphasearch(div_code, "admin", ddlAlpha.SelectedValue);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            ddlFieldForce.DataTextField = "sf_name";
            ddlFieldForce.DataValueField = "sf_code";
            ddlFieldForce.DataSource = dsSalesForce;
            ddlFieldForce.DataBind();

            ddlSF.DataTextField = "sf_color";
            ddlSF.DataValueField = "sf_code";
            ddlSF.DataSource = dsSalesForce;
            ddlSF.DataBind();

        }

    }

    private void FillManagers()
    {
        SalesForce sf = new SalesForce();

        if (ddlFFType.SelectedValue.ToString() == "1")
        {
            ddlAlpha.Visible = false;
            dsSalesForce = sf.UserListTP_Hierarchy(div_code, "admin");
        }
        else if (ddlFFType.SelectedValue.ToString() == "0")
        {
            FillSF_Alpha();
            ddlAlpha.Visible = true;
            dsSalesForce = sf.UserListTP_Alpha(div_code, "admin");
        }
        else if (ddlFFType.SelectedValue.ToString() == "2")
        {
            dsSalesForce = sf.UserList_HQ(div_code, "admin");
        }

        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            ddlFieldForce.DataTextField = "sf_name";
            ddlFieldForce.DataValueField = "sf_code";
            ddlFieldForce.DataSource = dsSalesForce;
            ddlFieldForce.DataBind();

            ddlSF.DataTextField = "sf_color";
            ddlSF.DataValueField = "sf_code";
            ddlSF.DataSource = dsSalesForce;
            ddlSF.DataBind();

        }
    }

    private void FillSF_Alpha()
    {
        SalesForce sf = new SalesForce();
        dsSalesForce = sf.getSalesForcelist_Alphabet(div_code);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            ddlAlpha.DataTextField = "sf_name";
            ddlAlpha.DataValueField = "val";
            ddlAlpha.DataSource = dsSalesForce;
            ddlAlpha.DataBind();
            ddlAlpha.SelectedIndex = 0;
        }
    }



    protected void btnGo_Click(object sender, EventArgs e)
    {
        if (ddlFieldForce.SelectedValue.ToString().Trim().Length > 0)
        {
            FillSalesForce();
        }

    }

    private void FillSalesForce()
    {
        SalesForce sf = new SalesForce();
        dsSalesForce = sf.FillSF_ScreenAccess(ddlFieldForce.SelectedValue.ToString().Trim(), div_code);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            TableRow tr_header = new TableRow();
            tr_header.BorderStyle = BorderStyle.Solid;
            tr_header.BorderWidth = 1;

            TableCell tc_SNo = new TableCell();
            tc_SNo.BorderStyle = BorderStyle.Solid;
            tc_SNo.BorderWidth = 1;
            tc_SNo.Width = 50;
            Literal lit_SNo = new Literal();
            lit_SNo.Text = "S.No";
            tc_SNo.Controls.Add(lit_SNo);
            tr_header.Cells.Add(tc_SNo);
            tr_header.BackColor = System.Drawing.Color.FromName("#A6A6D2");

            TableCell tc_DR_Code = new TableCell();
            tc_DR_Code.BorderStyle = BorderStyle.Solid;
            tc_DR_Code.BorderWidth = 1;
            tc_DR_Code.Width = 400;
            Literal lit_DR_Code = new Literal();
            lit_DR_Code.Text = "<center>SF Code</center>";
            tc_DR_Code.Controls.Add(lit_DR_Code);
            tc_DR_Code.Visible = false;
            tr_header.Cells.Add(tc_DR_Code);

            TableCell tc_DR_Name = new TableCell();
            tc_DR_Name.BorderStyle = BorderStyle.Solid;
            tc_DR_Name.BorderWidth = 1;
            tc_DR_Name.Width = 400;
            Literal lit_DR_Name = new Literal();
            lit_DR_Name.Text = "<center>SF Name</center>";
            tc_DR_Name.Controls.Add(lit_DR_Name);
            tr_header.Cells.Add(tc_DR_Name);


            TableCell tc_DR_Level1 = new TableCell();
            tc_DR_Level1.BorderStyle = BorderStyle.Solid;
            tc_DR_Level1.BorderWidth = 1;
            tc_DR_Level1.Width = 400;
            Literal lit_DR_Level1 = new Literal();
            lit_DR_Level1.Text = "<center>DCR Entry</center>";
            tc_DR_Level1.Controls.Add(lit_DR_Level1);
            tr_header.Cells.Add(tc_DR_Level1);

            TableCell tc_DR_Level2 = new TableCell();
            tc_DR_Level2.BorderStyle = BorderStyle.Solid;
            tc_DR_Level2.BorderWidth = 1;
            tc_DR_Level2.Width = 400;
            Literal lit_DR_Level2 = new Literal();
            lit_DR_Level2.Text = "<center>TP Entry</center>";
            tc_DR_Level2.Controls.Add(lit_DR_Level2);
            tr_header.Cells.Add(tc_DR_Level2);

            TableCell tc_DR_Level3 = new TableCell();
            tc_DR_Level3.BorderStyle = BorderStyle.Solid;
            tc_DR_Level3.BorderWidth = 1;
            tc_DR_Level3.Width = 400;
            Literal lit_DR_Level3 = new Literal();
            lit_DR_Level3.Text = "<center>SDP Entry</center>";
            tc_DR_Level3.Controls.Add(lit_DR_Level3);
            tr_header.Cells.Add(tc_DR_Level3);

            TableCell tc_DR_Level4 = new TableCell();
            tc_DR_Level4.BorderStyle = BorderStyle.Solid;
            tc_DR_Level4.BorderWidth = 1;
            tc_DR_Level4.Width = 400;
            Literal lit_DR_Level4 = new Literal();
            lit_DR_Level4.Text = "<center>Level4  </center>";
            tc_DR_Level4.Controls.Add(lit_DR_Level4);
            tr_header.Cells.Add(tc_DR_Level4);

            tbl.Rows.Add(tr_header);

            // Details Section
            string sURL = string.Empty;
            int iCount = 0;
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
                tr_det.Cells.Add(tc_det_SNo);
                //tr_det.Height = 10;

                TableCell tc_det_doc_code = new TableCell();
                Literal lit_det_doc_code = new Literal();
                lit_det_doc_code.Text = "&nbsp;" + drFF["SF_Code"].ToString();
                tc_det_doc_code.BorderStyle = BorderStyle.Solid;
                tc_det_doc_code.BorderWidth = 1;
                tc_det_doc_code.Controls.Add(lit_det_doc_code);
                tc_det_doc_code.Visible = false;
                tr_det.Cells.Add(tc_det_doc_code);

                TableCell tc_det_doc_name = new TableCell();
                Literal lit_det_doc_name = new Literal();
                lit_det_doc_name.Text = "&nbsp;" + drFF["Sf_Name"].ToString();
                tc_det_doc_name.BorderStyle = BorderStyle.Solid;
                tc_det_doc_name.BorderWidth = 1;
                tc_det_doc_name.Controls.Add(lit_det_doc_name);
                tr_det.Cells.Add(tc_det_doc_name);

                TableCell tc_det_doc_Level1 = new TableCell();
                CheckBox chk_det_doc_Level1 = new CheckBox();
                tc_det_doc_Level1.BorderStyle = BorderStyle.Solid;
                tc_det_doc_Level1.BorderWidth = 1;
                tc_det_doc_Level1.Controls.Add(chk_det_doc_Level1);
                tc_det_doc_Level1.HorizontalAlign = HorizontalAlign.Center;
                tr_det.Cells.Add(tc_det_doc_Level1);

                TableCell tc_det_doc_Level2 = new TableCell();
                CheckBox chk_det_doc_Level2 = new CheckBox();
                tc_det_doc_Level2.BorderStyle = BorderStyle.Solid;
                tc_det_doc_Level2.BorderWidth = 1;
                tc_det_doc_Level2.Controls.Add(chk_det_doc_Level2);
                tc_det_doc_Level2.HorizontalAlign = HorizontalAlign.Center;
                tr_det.Cells.Add(tc_det_doc_Level2);

                TableCell tc_det_doc_Level3 = new TableCell();
                CheckBox chk_det_doc_Level3 = new CheckBox();
                tc_det_doc_Level3.BorderStyle = BorderStyle.Solid;
                tc_det_doc_Level3.BorderWidth = 1;
                tc_det_doc_Level3.Controls.Add(chk_det_doc_Level3);
                tc_det_doc_Level3.HorizontalAlign = HorizontalAlign.Center;
                tr_det.Cells.Add(tc_det_doc_Level3);

                TableCell tc_det_doc_Level4 = new TableCell();
                CheckBox chk_det_doc_Level4 = new CheckBox();
                chk_det_doc_Level4.Checked = true;
                tc_det_doc_Level4.BorderStyle = BorderStyle.Solid;
                tc_det_doc_Level4.BorderWidth = 1;
                tc_det_doc_Level4.Controls.Add(chk_det_doc_Level4);
                tc_det_doc_Level4.HorizontalAlign = HorizontalAlign.Center;
                tr_det.Cells.Add(tc_det_doc_Level4);

                tbl.Rows.Add(tr_det);
            }
        }


    }

}