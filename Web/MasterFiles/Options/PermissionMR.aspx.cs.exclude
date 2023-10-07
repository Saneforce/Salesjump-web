using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;

public partial class MasterFiles_Options_PermissionMR : System.Web.UI.Page
{
    DataSet dsSalesForce = null;
    DataSet dsDoctor = null;
    DataSet dsCatg = null;
    DataSet dsSpec = null;
    DataSet dsClass = null;
    DataSet dsQual = null;
    DataSet dsDivision = null;

    int iDRCatg = -1;
    int iTotal_FF = 0;
    int[] iTotal_Catg = new int[20];
    int tot_catg = 0;
    int i = -1;
    string sDRCatg_Count = string.Empty;
    string div_code = string.Empty;
    string cur_sf_code = string.Empty;
    string cur_sf_name = string.Empty;
    DataSet dsState = null;
    string sState = string.Empty;

    string[] statecd;
    string slno;
    string state_cd = string.Empty;

   protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        if (!Page.IsPostBack)
        {
            FillManagers();
            FillColor();
            menu1.Title = this.Page.Title;
            menu1.FindControl("btnBack").Visible = false;
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

   protected void btnSubmit_Click(object sender, EventArgs e)
   {
       if (ddlFieldForce.SelectedValue.ToString().Trim().Length > 0)
       {
           ViewState["dsSalesForce"] = null;
           ViewState["dsDoctor"] = null;
           FillSalesForce();
       }

   }

   private void FillSalesForce()
   {
       int tot_rows = 0;
       int tot_cols = 0;

       // Fetch the total rows for the table
       SalesForce sf = new SalesForce();
       dsSalesForce = sf.UserList_getVacantMR(div_code, ddlFieldForce.SelectedValue.ToString());

       if (dsSalesForce.Tables[0].Rows.Count > 0)
       {
           tot_rows = dsSalesForce.Tables[0].Rows.Count;
           ViewState["dsSalesForce"] = dsSalesForce;
       }

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
           tc_SNo.BorderWidth = 1;
           tc_SNo.Width = 50;
           Literal lit_SNo = new Literal();
           lit_SNo.Text = "S.No";
           tc_SNo.Controls.Add(lit_SNo);
           //tc_SNo.RowSpan = 2;
           tr_header.Cells.Add(tc_SNo);
           tr_header.BackColor = System.Drawing.Color.FromName("#A6A6D2");

           TableCell tc_FF = new TableCell();
           tc_FF.BorderStyle = BorderStyle.Solid;
           tc_FF.BorderWidth = 1;
           tc_FF.Width = 400;
           Literal lit_FF = new Literal();
           lit_FF.Text = "<center>Field Force Name</center>";
           tc_FF.Controls.Add(lit_FF);
           //tc_FF.RowSpan = 2;
           tr_header.Cells.Add(tc_FF);

           TableCell tc_HQ = new TableCell();
           tc_HQ.BorderStyle = BorderStyle.Solid;
           tc_HQ.BorderWidth = 1;
           tc_HQ.Width = 400;
           Literal lit_HQ = new Literal();
           lit_HQ.Text = "<center>HQ</center>";
           tc_HQ.Controls.Add(lit_HQ);
           //tc_HQ.RowSpan = 2;
           tr_header.Cells.Add(tc_HQ);

           TableCell tc_Level1 = new TableCell();
           tc_Level1.BorderStyle = BorderStyle.Solid;
           tc_Level1.BorderWidth = 1;
           tc_Level1.Width = 400;
           Literal lit_Level1 = new Literal();
           lit_Level1.Text = "<center>Level1</center>";
           tc_Level1.Controls.Add(lit_Level1);
           //tc_HQ.RowSpan = 2;
           tr_header.Cells.Add(tc_Level1);

           TableCell tc_Level2 = new TableCell();
           tc_Level2.BorderStyle = BorderStyle.Solid;
           tc_Level2.BorderWidth = 1;
           tc_Level2.Width = 400;
           Literal lit_Level2 = new Literal();
           lit_Level2.Text = "<center>Level2</center>";
           tc_Level2.Controls.Add(lit_Level2);
           //tc_HQ.RowSpan = 2;
           tr_header.Cells.Add(tc_Level2);

           TableCell tc_Level3 = new TableCell();
           tc_Level3.BorderStyle = BorderStyle.Solid;
           tc_Level3.BorderWidth = 1;
           tc_Level3.Width = 400;
           Literal lit_Level3 = new Literal();
           lit_Level3.Text = "<center>Level3</center>";
           tc_Level3.Controls.Add(lit_Level3);
           //tc_HQ.RowSpan = 2;
           tr_header.Cells.Add(tc_Level3);


           tbl.Rows.Add(tr_header);

           // Details Section
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
               tr_det.Cells.Add(tc_det_SNo);
               //tr_det.Height = 10;

               TableCell tc_det_FF = new TableCell();
               Literal lit_det_FF = new Literal();
               lit_det_FF.Text = "&nbsp;" + drFF["sf_name"].ToString();
               tc_det_FF.BorderStyle = BorderStyle.Solid;
               tc_det_FF.BorderWidth = 1;
               tc_det_FF.Controls.Add(lit_det_FF);
               tr_det.Cells.Add(tc_det_FF);

               TableCell tc_det_HQ = new TableCell();
               Literal lit_det_HQ = new Literal();
               lit_det_HQ.Text = "&nbsp;" + drFF["sf_hq"].ToString();
               tc_det_HQ.BorderStyle = BorderStyle.Solid;
               tc_det_HQ.BorderWidth = 1;
               tc_det_HQ.Controls.Add(lit_det_HQ);
               tr_det.Cells.Add(tc_det_HQ);

               cur_sf_code = drFF["sf_code"].ToString().Trim();

               SalesForce sf = new SalesForce();
               dsSalesForce = sf.getReporting(cur_sf_code);

               if (dsSalesForce.Tables[0].Rows.Count > 0)
               {
                   foreach (DataRow drSF in dsSalesForce.Tables[0].Rows)
                   {
                       cur_sf_code = drSF["sf_code"].ToString();
                       cur_sf_name = drSF["sf_name"].ToString();
                   }
               }

               TableCell tc_det_Level1 = new TableCell();               
               CheckBox chk_det_Level1 = new CheckBox();
               tc_det_Level1.BorderStyle = BorderStyle.Solid;
               tc_det_Level1.BorderWidth = 1;
               chk_det_Level1.Text = cur_sf_name;
               tc_det_Level1.Controls.Add(chk_det_Level1);

               HiddenField hdn_det_Level1 = new HiddenField();
               hdn_det_Level1.Value = cur_sf_code;
               tc_det_Level1.Controls.Add(hdn_det_Level1);


               tc_det_Level1.HorizontalAlign = HorizontalAlign.Center;
               tr_det.Cells.Add(tc_det_Level1);

               dsSalesForce = sf.getReporting(cur_sf_code);

               if (dsSalesForce.Tables[0].Rows.Count > 0)
               {
                   foreach (DataRow drSF in dsSalesForce.Tables[0].Rows)
                   {
                       cur_sf_code = drSF["sf_code"].ToString();
                       cur_sf_name = drSF["sf_name"].ToString();
                   }
               }

               TableCell tc_det_Level2 = new TableCell();
               CheckBox chk_det_Level2 = new CheckBox();
               tc_det_Level2.BorderStyle = BorderStyle.Solid;
               tc_det_Level2.BorderWidth = 1;
               chk_det_Level2.Text = cur_sf_name;
               tc_det_Level2.Controls.Add(chk_det_Level2);

               HiddenField hdn_det_Level2 = new HiddenField();
               hdn_det_Level2.Value = cur_sf_code;
               tc_det_Level2.Controls.Add(hdn_det_Level2);
               
               tc_det_Level2.HorizontalAlign = HorizontalAlign.Center;
               tr_det.Cells.Add(tc_det_Level2);

               dsSalesForce = sf.getReporting(cur_sf_code);

               if (dsSalesForce.Tables[0].Rows.Count > 0)
               {
                   foreach (DataRow drSF in dsSalesForce.Tables[0].Rows)
                   {
                       cur_sf_code = drSF["sf_code"].ToString();
                       cur_sf_name = drSF["sf_name"].ToString();
                   }
               }

               TableCell tc_det_Level3 = new TableCell();
               CheckBox chk_det_Level3 = new CheckBox();
               tc_det_Level3.BorderStyle = BorderStyle.Solid;
               tc_det_Level3.BorderWidth = 1;
               chk_det_Level3.Text = cur_sf_name;
               tc_det_Level3.Controls.Add(chk_det_Level3);

               HiddenField hdn_det_Level3 = new HiddenField();
               hdn_det_Level3.Value = cur_sf_code;
               tc_det_Level3.Controls.Add(hdn_det_Level3);               
               
               tc_det_Level3.HorizontalAlign = HorizontalAlign.Center;
               tr_det.Cells.Add(tc_det_Level3);
               
               tbl.Rows.Add(tr_det);
           }

           ViewState["dynamictable"] = true;
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

   protected void btnSave_Click(object sender, EventArgs e)
   {
       int iReturn = -1;
       iReturn = 1;

       if (iReturn != -1)
       {
           ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Vacant MR Login access permission have been created Successfully');</script>");
           ddlFFType.SelectedIndex = 0;
           ddlFieldForce.SelectedIndex = 0;
           ddlAlpha.SelectedIndex = 0;
       }
      
   }
}