using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
using System.Collections;

public partial class MasterFiles_Options_Mgrwise_CoreDR_Map : System.Web.UI.Page
{
    DataSet dsSalesForce = null;
    DataSet dsDoc = null;
    string div_code = string.Empty;
    string sLevel1_Code = string.Empty;
    string sLevel1_Name = string.Empty;
    string sLevel2_Code = string.Empty;
    string sLevel2_Name = string.Empty;
    string sLevel3_Code = string.Empty;
    string sLevel3_Name = string.Empty;

    ArrayList arrDoctor = new ArrayList(); 

    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        if (!Page.IsPostBack)
        {
            FillSalesForce(div_code);
            menu1.Title = this.Page.Title;
            menu1.FindControl("btnBack").Visible = false;
        }

    }

    private void FillSalesForce(string div_code)
    {
        SalesForce sf = new SalesForce();
        dsSalesForce = sf.UserList_getMR(div_code);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            ddlMR.DataTextField = "sf_name";
            ddlMR.DataValueField = "sf_code";            
            ddlMR.DataSource = dsSalesForce;
            ddlMR.DataBind();

        }


    }

    private void FillDoc()
    {
        ListedDR LstDoc = new ListedDR();
        dsDoc = LstDoc.getListedDr(ddlMR.SelectedValue.ToString().Trim());
        if (dsDoc.Tables[0].Rows.Count > 0)
        {

            SalesForce sf = new SalesForce();
            dsSalesForce = sf.getReporting(ddlMR.SelectedValue.ToString().Trim());

            if (dsSalesForce.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow drSF in dsSalesForce.Tables[0].Rows)
                {
                    sLevel3_Code = drSF["sf_code"].ToString();
                    sLevel3_Name = drSF["sf_name"].ToString();
                }
            }

            dsSalesForce = sf.getReporting(sLevel3_Code);

            if (dsSalesForce.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow drSF in dsSalesForce.Tables[0].Rows)
                {
                    sLevel2_Code = drSF["sf_code"].ToString();
                    sLevel2_Name = drSF["sf_name"].ToString();
                }
            }

            dsSalesForce = sf.getReporting(sLevel2_Code);

            if (dsSalesForce.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow drSF in dsSalesForce.Tables[0].Rows)
                {
                    sLevel1_Code = drSF["sf_code"].ToString();
                    sLevel1_Name = drSF["sf_name"].ToString();
                }
            }

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
           lit_DR_Code.Text = "<center>Doc Code</center>";
           tc_DR_Code.Controls.Add(lit_DR_Code );
           tc_DR_Code.Visible=false;
           tr_header.Cells.Add(tc_DR_Code);

           TableCell tc_DR_Name = new TableCell();
           tc_DR_Name.BorderStyle = BorderStyle.Solid;
           tc_DR_Name.BorderWidth = 1;
           tc_DR_Name.Width = 400;
           Literal lit_DR_Name = new Literal();
           lit_DR_Name.Text = "<center>Doctor Name</center>";
           tc_DR_Name.Controls.Add(lit_DR_Name);
           tr_header.Cells.Add(tc_DR_Name);

           TableCell tc_DR_Terr = new TableCell();
           tc_DR_Terr.BorderStyle = BorderStyle.Solid;
           tc_DR_Terr.BorderWidth = 1;
           tc_DR_Terr.Width = 400;
           Literal lit_DR_Terr = new Literal();
           lit_DR_Terr.Text = "<center>Territory</center>";
           tc_DR_Terr.Controls.Add(lit_DR_Terr);
           tr_header.Cells.Add(tc_DR_Terr);

           TableCell tc_DR_Spec = new TableCell();
           tc_DR_Spec.BorderStyle = BorderStyle.Solid;
           tc_DR_Spec.BorderWidth = 1;
           tc_DR_Spec.Width = 400;
           Literal lit_DR_Spec = new Literal();
           lit_DR_Spec.Text = "<center>Specialty</center>";
           tc_DR_Spec.Controls.Add(lit_DR_Spec);
           tr_header.Cells.Add(tc_DR_Spec);

           TableCell tc_DR_Catg = new TableCell();
           tc_DR_Catg.BorderStyle = BorderStyle.Solid;
           tc_DR_Catg.BorderWidth = 1;
           tc_DR_Catg.Width = 400;
           Literal lit_DR_Catg = new Literal();
           lit_DR_Catg.Text = "<center>Category</center>";
           tc_DR_Catg.Controls.Add(lit_DR_Catg);
           tr_header.Cells.Add(tc_DR_Catg);

           TableCell tc_DR_Level1 = new TableCell();
           tc_DR_Level1.BorderStyle = BorderStyle.Solid;
           tc_DR_Level1.BorderWidth = 1;
           tc_DR_Level1.Width = 400;
           Literal lit_DR_Level1 = new Literal();
           lit_DR_Level1.Text = "<center>Level1 (" + sLevel1_Name + ") </center>";
           tc_DR_Level1.Controls.Add(lit_DR_Level1); 
           tr_header.Cells.Add(tc_DR_Level1);

           TableCell tc_DR_Level2 = new TableCell();
           tc_DR_Level2.BorderStyle = BorderStyle.Solid;
           tc_DR_Level2.BorderWidth = 1;
           tc_DR_Level2.Width = 400;
           Literal lit_DR_Level2 = new Literal();
           lit_DR_Level2.Text = "<center>Level2 (" + sLevel2_Name + ")</center>";
           tc_DR_Level2.Controls.Add(lit_DR_Level2);
           tr_header.Cells.Add(tc_DR_Level2);

           TableCell tc_DR_Level3 = new TableCell();
           tc_DR_Level3.BorderStyle = BorderStyle.Solid;
           tc_DR_Level3.BorderWidth = 1;
           tc_DR_Level3.Width = 400;
           Literal lit_DR_Level3 = new Literal();
           lit_DR_Level3.Text = "<center>Level3 (" + sLevel3_Name + ")</center>";
           tc_DR_Level3.Controls.Add(lit_DR_Level3);
           tr_header.Cells.Add(tc_DR_Level3);

           TableCell tc_DR_Level4 = new TableCell();
           tc_DR_Level4.BorderStyle = BorderStyle.Solid;
           tc_DR_Level4.BorderWidth = 1;
           tc_DR_Level4.Width = 400;
           Literal lit_DR_Level4 = new Literal();
           lit_DR_Level4.Text = "<center>Level4 (" + ddlMR.SelectedItem.Text.Trim() + ")</center>";
           tc_DR_Level4.Controls.Add(lit_DR_Level4);
           tr_header.Cells.Add(tc_DR_Level4);

            tbl.Rows.Add(tr_header);

           // Details Section
           string sURL = string.Empty;
           int iCount = 0;
           foreach (DataRow drFF in dsDoc .Tables[0].Rows)
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
               lit_det_doc_code.Text = "&nbsp;" + drFF["ListedDrCode"].ToString();
               tc_det_doc_code.BorderStyle = BorderStyle.Solid;
               tc_det_doc_code.BorderWidth = 1;
               tc_det_doc_code.Controls.Add(lit_det_doc_code);
               tc_det_doc_code.Visible=false;
               tr_det.Cells.Add(tc_det_doc_code);

               TableCell tc_det_doc_name = new TableCell();
               Literal lit_det_doc_name= new Literal();
               lit_det_doc_name.Text = "&nbsp;" + drFF["ListedDr_Name"].ToString();
               tc_det_doc_name.BorderStyle = BorderStyle.Solid;
               tc_det_doc_name.BorderWidth = 1;
               tc_det_doc_name.Controls.Add(lit_det_doc_name);
               tr_det.Cells.Add(tc_det_doc_name);

               TableCell tc_det_doc_Terr = new TableCell();
               Literal lit_det_doc_Terr= new Literal();
               lit_det_doc_Terr.Text = "&nbsp;" + drFF["territory_Name"].ToString();
               tc_det_doc_Terr.BorderStyle = BorderStyle.Solid;
               tc_det_doc_Terr.BorderWidth = 1;
               tc_det_doc_Terr.Controls.Add(lit_det_doc_Terr);
               tr_det.Cells.Add(tc_det_doc_Terr);

               TableCell tc_det_doc_Spec = new TableCell();
               Literal lit_det_doc_Spec= new Literal();
               lit_det_doc_Spec.Text = "&nbsp;" + drFF["Doc_Special_Name"].ToString();
               tc_det_doc_Spec.BorderStyle = BorderStyle.Solid;
               tc_det_doc_Spec.BorderWidth = 1;
               tc_det_doc_Spec.Controls.Add(lit_det_doc_Spec);
               tr_det.Cells.Add(tc_det_doc_Spec);

               TableCell tc_det_doc_catg = new TableCell();
               Literal lit_det_doc_catg= new Literal();
               lit_det_doc_catg.Text = "&nbsp;" + drFF["Doc_Cat_Name"].ToString();
               tc_det_doc_catg.BorderStyle = BorderStyle.Solid;
               tc_det_doc_catg.BorderWidth = 1;
               tc_det_doc_catg.Controls.Add(lit_det_doc_catg);
               tr_det.Cells.Add(tc_det_doc_catg);

               TableCell tc_det_doc_Level1 = new TableCell();
               CheckBox chk_det_doc_Level1 = new CheckBox();
               //chk_det_doc_Level1.AutoPostBack = true;
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
               //_rows = tr_det;

               arrDoctor.Add(lit_det_doc_code.Text + "," + sLevel1_Code + "-" + chk_det_doc_Level1.Checked + "," + sLevel2_Code + "-" + chk_det_doc_Level2.Checked + "," + sLevel3_Code + "-" + chk_det_doc_Level3.Checked + "," + ddlMR.SelectedValue.ToString().Trim() + "-" + chk_det_doc_Level4.Checked);

           }
            
           ViewState["tot_rows"] = iCount;
           ViewState["arrDoctor"] = arrDoctor;
           //_rows =  tbl.Rows;
       }

        
    }

    protected void ddlMR_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlMR.SelectedValue.ToString().Trim().Length > 0)
            FillDoc();
    }


    protected void btnSave_Click(object sender, EventArgs e)
    {
        int iReturn = -1;
        iReturn = 1;
        int iTotRows = Convert.ToInt32(ViewState["tot_rows"]);
        string sRowText = string.Empty;
        string sDoc_Code = string.Empty;
        string Mgr_Code = string.Empty;

        if (ViewState["arrDoctor"] != null)
        {
            arrDoctor = (ArrayList)ViewState["arrDoctor"];

            for (int i = 0; i < iTotRows; i++)
            {

                sRowText = arrDoctor[i].ToString();
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Core Doctor(s) have been mapped Successfully');</script>");
                ddlMR.SelectedIndex = 0;
            }
        }
    }
}