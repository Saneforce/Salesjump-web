using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;

public partial class MIS_Reports_ListCallReport : System.Web.UI.Page
{
    string div_code = string.Empty;
    DataSet dsSalesForce = null;
    DataSet dsUserList = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        if (!Page.IsPostBack)
        {
            FillManagers();
            FillColor();
            menu1.Title = this.Page.Title;
            menu1.FindControl("btnBack").Visible = false;
            //btnSumbit.Visible = false;
        }

        if (ViewState["sf_code"] != null)
        {
            FillSF();
        }

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

    private void FillManagers()
    {
        SalesForce sf = new SalesForce();

        if (ddlFFType.SelectedValue.ToString() == "1")
        {
            ddlAlpha.Visible = false;
            dsSalesForce = sf.UserList_Hierarchy(div_code, "admin");
        }
        else if (ddlFFType.SelectedValue.ToString() == "0")
        {
            FillSF_Alpha();
            ddlAlpha.Visible = true;
            dsSalesForce = sf.UserList_Alpha(div_code, "admin");
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

    protected void ddlAlpha_SelectedIndexChanged(object sender, EventArgs e)
    {
        SalesForce sf = new SalesForce();
        dsSalesForce = sf.UserList_Alphasearch(div_code, "admin", ddlAlpha.SelectedValue);
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
    protected void ddlFFType_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblFF.Text = "Field Force";
        FillManagers();
        FillColor();
    }

    protected void btnGo_Click(object sender, EventArgs e)
    {
        //Load_tree();
        FillSF();
    }

    protected DataSet PDataset(string sf_code)
    {
        SalesForce sf = new SalesForce();
        dsUserList = sf.SF_ReportingTo_TourPlan(div_code, sf_code);
        return dsUserList;
    }

    public void Load_tree()
    {
        DataSet PrSet = PDataset(ddlFieldForce.SelectedValue.ToString());
        TreeView1.Nodes.Clear();
        foreach (DataRow dr in PrSet.Tables[0].Rows)
        {
            if (ddlFieldForce.SelectedValue.ToString().Trim().Length > 0)
            {
                TreeNode tnParent = new TreeNode();
                tnParent.Text = dr["Sf_Name"].ToString();
                string value = dr["Sf_code"].ToString();
                tnParent.Expand();
                TreeView1.Nodes.Add(tnParent);
                FillChild(tnParent, value);
            }
        }
    }

    public int FillChild(TreeNode parent, string IID)
    {
        DataSet ds = PDataset(IID);
        if (ds.Tables[0].Rows.Count > 0)
        {
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                TreeNode child = new TreeNode();
                
                child.Text = dr["Sf_Name"].ToString().Trim();
                string temp = dr["Sf_code"].ToString();
                child.Collapse();
                parent.ChildNodes.Add(child);
                FillChild(child, temp);
            }
            return 0;
        }
        else
        {
            return 0;
        }
    }

    private void FillSF()
    {
        tbl.Rows.Clear();

        SalesForce sf = new SalesForce();
        dsSalesForce = sf.SF_ReportingTo_TourPlan(div_code, ddlFieldForce.SelectedValue.ToString());
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
            //tc_DR_Code.Width = 400;
            Literal lit_DR_Code = new Literal();
            lit_DR_Code.Text = "<center>SF Code</center>";
            tc_DR_Code.Controls.Add(lit_DR_Code);
            tc_DR_Code.Visible = false;
            tr_header.Cells.Add(tc_DR_Code);

            TableCell tc_DR_Name = new TableCell();
            tc_DR_Name.BorderStyle = BorderStyle.Solid;
            tc_DR_Name.BorderWidth = 1;
            //tc_DR_Name.Width = 400;
            Literal lit_DR_Name = new Literal();
            lit_DR_Name.Text = "<center>SF Name</center>";
            tc_DR_Name.Controls.Add(lit_DR_Name);
            tr_header.Cells.Add(tc_DR_Name);

            tbl.Rows.Add(tr_header);

            // Details Section
            string sURL = string.Empty;
            int iCount = 0;
            foreach (DataRow drFF in dsSalesForce.Tables[0].Rows)
            {
                TableRow tr_det = new TableRow();
                //tr_det.Height = 10;
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
                lit_det_doc_code.Text = "&nbsp;" + drFF["sf_code"].ToString();
                tc_det_doc_code.BorderStyle = BorderStyle.Solid;
                tc_det_doc_code.BorderWidth = 1;
                tc_det_doc_code.Controls.Add(lit_det_doc_code);
                tc_det_doc_code.Visible = false;
                tr_det.Cells.Add(tc_det_doc_code);

                TableCell tc_det_doc_name = new TableCell();
                ImageButton imgShowHide = new ImageButton();                
                imgShowHide.Height = 25;
                imgShowHide.Width = 25;
                imgShowHide.ImageUrl = "~/images/plus.png";
                Literal lit_det_doc_name = new Literal();
                lit_det_doc_name.Text = "&nbsp;" + drFF["SF_Name"].ToString();                
                tc_det_doc_name.BorderStyle = BorderStyle.Solid;
                tc_det_doc_name.BorderWidth = 1;
                tc_det_doc_name.Controls.Add(imgShowHide);
                tc_det_doc_name.Controls.Add(lit_det_doc_name);
                tc_det_doc_code.VerticalAlign = VerticalAlign.Top;
                tr_det.Cells.Add(tc_det_doc_name);

                if (ViewState["sf_code"] != null)
                {

                    dsUserList = sf.SF_ReportingTo_TourPlan(div_code, ViewState["sf_code"].ToString());

                    if (dsUserList.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow drTreeFF in dsSalesForce.Tables[0].Rows)
                        {

                            TableRow tr_tree = new TableRow();

                            TableCell tc_tree_SNo = new TableCell();
                            Literal lit_tree_SNo = new Literal();
                            lit_tree_SNo.Text = "";
                            tc_tree_SNo.BorderStyle = BorderStyle.Solid;
                            tc_tree_SNo.BorderWidth = 1;
                            tc_tree_SNo.Controls.Add(lit_tree_SNo);
                            tr_tree.Cells.Add(tc_tree_SNo);

                            TableCell tc_tree_doc_code = new TableCell();
                            Literal lit_tree_doc_code = new Literal();
                            lit_tree_doc_code.Text = "&nbsp;" + drTreeFF["sf_code"].ToString();
                            tc_tree_doc_code.BorderStyle = BorderStyle.Solid;
                            tc_tree_doc_code.BorderWidth = 1;
                            tc_tree_doc_code.Controls.Add(lit_tree_doc_code);
                            tc_tree_doc_code.Visible = false;
                            tr_tree.Cells.Add(tc_tree_doc_code);

                            TableCell tc_tree_doc_name = new TableCell();
                            ImageButton imgtreeShowHide = new ImageButton();
                            imgtreeShowHide.Height = 25;
                            imgtreeShowHide.Width = 25;
                            imgtreeShowHide.ImageUrl = "~/images/plus.png";
                            Literal lit_tree_doc_name = new Literal();
                            lit_tree_doc_name.Text = "&nbsp;" + drTreeFF["SF_Name"].ToString();
                            tc_tree_doc_name.BorderStyle = BorderStyle.Solid;
                            tc_tree_doc_name.BorderWidth = 1;
                            tc_tree_doc_name.Controls.Add(imgtreeShowHide);
                            tc_tree_doc_name.Controls.Add(lit_tree_doc_name);
                            tc_tree_doc_code.VerticalAlign = VerticalAlign.Top;
                            tr_tree.Cells.Add(tc_tree_doc_name);
                            tbl.Rows.Add(tr_tree);
                        }
                    }

                    ViewState["sf_code"] = ViewState["sf_code"].ToString() + "," + drFF["sf_code"].ToString();
                }
                else
                {
                    ViewState["sf_code"] = drFF["sf_code"].ToString();
                }


                //TableCell tc_det_doc_level = new TableCell();
                //Literal lit_det_doc_level = new Literal();
                //lit_det_doc_level.Text = "&nbsp;" + drFF["sf_color"].ToString();
                //tc_det_doc_level.BorderStyle = BorderStyle.Solid;
                //tc_det_doc_level.BorderWidth = 1;
                //tc_det_doc_level.Controls.Add(lit_det_doc_level);
                //tr_det.Cells.Add(tc_det_doc_level);

                tbl.Rows.Add(tr_det);

            }
       }
    }

    //private void FillUserList()
    //{
    //    string sMgr = "admin";
    //    if (ddlFieldForce.SelectedIndex > 0)
    //    {
    //        sMgr = ddlFieldForce.SelectedValue;
    //    }
    //    SalesForce sf = new SalesForce();
    //    //dsUserList = sf.UserList(div_code, "admin");  
    //    dsUserList = sf.SF_ReportingTo_TourPlan(div_code, sMgr);
    //    if (dsUserList.Tables[0].Rows.Count > 0)
    //    {
    //        grdSalesForce.Visible = true;
    //        grdSalesForce.DataSource = dsUserList;
    //        grdSalesForce.DataBind();
    //    }
    //}

    //protected void Show_Hide_OrdersGrid(object sender, EventArgs e)
    //{
    //    ImageButton imgShowHide = (sender as ImageButton);
    //    GridViewRow row = (imgShowHide.NamingContainer as GridViewRow);
    //    if (imgShowHide.CommandArgument == "Show")
    //    {
    //        row.FindControl("pnlOrders").Visible = true;
    //        imgShowHide.CommandArgument = "Hide";
    //        imgShowHide.ImageUrl = "~/images/minus.png";
    //        string customerId = gvCustomers.DataKeys[row.RowIndex].Value.ToString();
    //        GridView gvOrders = row.FindControl("gvOrders") as GridView;
    //        BindOrders(customerId, gvOrders);
    //    }
    //    else
    //    {
    //        row.FindControl("pnlOrders").Visible = false;
    //        imgShowHide.CommandArgument = "Show";
    //        imgShowHide.ImageUrl = "~/images/plus.png";
    //    }
    //}

}