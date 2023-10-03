using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;

public partial class MasterFiles_Options_SetupScreenAccess : System.Web.UI.Page
{
    DataSet dsSalesForce = null;
    string div_code = string.Empty;
    DataSet dsDivision = null;
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
           FillState(div_code);

            menu1.Title = this.Page.Title;
            menu1.FindControl("btnBack").Visible = false;
        }

    }

    private void FillSalesForce()
    {
        Table tblFF = new Table();
        tblFF.ID = "Table1";

        TableRow tr_header = new TableRow();
        tr_header.BorderStyle = BorderStyle.Solid;
        tr_header.BorderWidth = 1;
        tr_header.BackColor = System.Drawing.Color.LightGray;

        TableCell tc_SF_Code = new TableCell();
        tc_SF_Code.BorderStyle = BorderStyle.Solid;
        tc_SF_Code.BorderWidth = 1;
        tc_SF_Code.Width = 400;
        Literal lit_SF_Code = new Literal();
        lit_SF_Code.Text = "<center>Field Force Code</center>";
        tc_SF_Code.Controls.Add(lit_SF_Code);
        tc_SF_Code.RowSpan = 2;
        tc_SF_Code.Visible = false;
        tr_header.Cells.Add(tc_SF_Code);

        TableCell tc_FF = new TableCell();
        tc_FF.BorderStyle = BorderStyle.Solid;
        tc_FF.BorderWidth = 1;
        tc_FF.Width = 400;
        Literal lit_FF = new Literal();
        lit_FF.Text = "<center>Field Force Name</center>";
        tc_FF.Controls.Add(lit_FF);
        tc_FF.RowSpan = 2;
        tr_header.Cells.Add(tc_FF);

        TableCell tc_doctor = new TableCell();
        Literal lit_doctor = new Literal();
        lit_doctor.Text = "<center>Doctor</center>";
        tc_doctor.Controls.Add(lit_doctor);
        tc_doctor.BorderStyle = BorderStyle.Solid;
        tc_doctor.BorderWidth = 1;
        tc_doctor.ColumnSpan = 4;
        tr_header.Cells.Add(tc_doctor);

        TableCell tc_Newdoctor = new TableCell();
        Literal lit_Newdoctor = new Literal();
        lit_Newdoctor.Text = "<center>UnListed Doctor</center>";
        tc_Newdoctor.Controls.Add(lit_Newdoctor);
        tc_Newdoctor.BorderStyle = BorderStyle.Solid;
        tc_Newdoctor.BorderWidth = 1;
        tc_Newdoctor.ColumnSpan = 2;
        tr_header.Cells.Add(tc_Newdoctor);

        TableCell tc_Chemist = new TableCell();
        Literal lit_Chemist = new Literal();
        lit_Chemist.Text = "<center>Chemist</center>";
        tc_Chemist.Controls.Add(lit_Chemist);
        tc_Chemist.BorderStyle = BorderStyle.Solid;
        tc_Chemist.BorderWidth = 1;
        tc_Chemist.ColumnSpan = 3;
        tr_header.Cells.Add(tc_Chemist);

        TableCell tc_Terr = new TableCell();
        Literal lit_Terr = new Literal();
        lit_Terr.Text = "<center>Territory</center>";
        tc_Terr.Controls.Add(lit_Terr);
        tc_Terr.BorderStyle = BorderStyle.Solid;
        tc_Terr.BorderWidth = 1;
        tc_Terr.ColumnSpan = 4;
        tr_header.Cells.Add(tc_Terr);

        //tbl.Rows.Add(tr_header);
        tblFF.Rows.Add(tr_header);

        TableRow tr_Subheader = new TableRow();
        tr_Subheader.BorderStyle = BorderStyle.Solid;
        tr_Subheader.BorderWidth = 1;
        tr_Subheader.BackColor = System.Drawing.Color.LightGray;

        TableCell tc_doctor_add = new TableCell();
        Literal lit_doctor_add = new Literal();
        lit_doctor_add.Text = "<center>Add</center>";
        tc_doctor_add.Controls.Add(lit_doctor_add);
        tc_doctor_add.BorderStyle = BorderStyle.Solid;
        tc_doctor_add.BorderWidth = 1;
        tc_doctor_add.Width = 50;
        tr_Subheader.Cells.Add(tc_doctor_add);

        TableCell tc_doctor_edit = new TableCell();
        Literal lit_doctor_edit = new Literal();
        lit_doctor_edit.Text = "<center>Edit</center>";
        tc_doctor_edit.Controls.Add(lit_doctor_edit);
        tc_doctor_edit.BorderStyle = BorderStyle.Solid;
        tc_doctor_edit.BorderWidth = 1;
        tc_doctor_add.Width = 50;
        tr_Subheader.Cells.Add(tc_doctor_edit);

        TableCell tc_doctor_del = new TableCell();
        Literal lit_doctor_del = new Literal();
        lit_doctor_del.Text = "<center>Del.</center>";
        tc_doctor_del.Controls.Add(lit_doctor_del);
        tc_doctor_del.BorderStyle = BorderStyle.Solid;
        tc_doctor_del.BorderWidth = 1;
        tc_doctor_del.Width = 50;
        tr_Subheader.Cells.Add(tc_doctor_del);

        TableCell tc_doctor_deact = new TableCell();
        Literal lit_doctor_deact = new Literal();
        lit_doctor_deact.Text = "<center>Deact.</center>";
        tc_doctor_deact.Controls.Add(lit_doctor_deact);
        tc_doctor_deact.BorderStyle = BorderStyle.Solid;
        tc_doctor_deact.BorderWidth = 1;
        tc_doctor_deact.Width = 50;
        tr_Subheader.Cells.Add(tc_doctor_deact);


        TableCell tc_Newdoctor_add = new TableCell();
        Literal lit_Newdoctor_add = new Literal();
        lit_Newdoctor_add.Text = "<center>Add</center>";
        tc_Newdoctor_add.Controls.Add(lit_Newdoctor_add);
        tc_Newdoctor_add.BorderStyle = BorderStyle.Solid;
        tc_Newdoctor_add.BorderWidth = 1;
        tc_Newdoctor_add.Width = 50;
        tr_Subheader.Cells.Add(tc_Newdoctor_add);

        TableCell tc_Newdoctor_deact = new TableCell();
        Literal lit_Newdoctor_deact = new Literal();
        lit_Newdoctor_deact.Text = "<center>Deact.</center>";
        tc_Newdoctor_deact.Controls.Add(lit_Newdoctor_deact);
        tc_Newdoctor_deact.BorderStyle = BorderStyle.Solid;
        tc_Newdoctor_deact.BorderWidth = 1;
        tc_Newdoctor_deact.Width = 50;
        tr_Subheader.Cells.Add(tc_Newdoctor_deact);


        TableCell tc_chem_add = new TableCell();
        Literal lit_chem_add = new Literal();
        lit_chem_add.Text = "<center>Add</center>";
        tc_chem_add.Controls.Add(lit_chem_add);
        tc_chem_add.BorderStyle = BorderStyle.Solid;
        tc_chem_add.BorderWidth = 1;
        tc_chem_add.Width = 50;
        tr_Subheader.Cells.Add(tc_chem_add);

        TableCell tc_chem_edit = new TableCell();
        Literal lit_chem_edit = new Literal();
        lit_chem_edit.Text = "<center>Edit</center>";
        tc_chem_edit.Controls.Add(lit_chem_edit);
        tc_chem_edit.BorderStyle = BorderStyle.Solid;
        tc_chem_edit.BorderWidth = 1;
        tc_chem_edit.Width = 50;
        tr_Subheader.Cells.Add(tc_chem_edit);

        TableCell tc_chem_deact = new TableCell();
        Literal lit_chem_deact = new Literal();
        lit_chem_deact.Text = "<center>Deact.</center>";
        tc_chem_deact.Controls.Add(lit_chem_deact);
        tc_chem_deact.BorderStyle = BorderStyle.Solid;
        tc_chem_deact.BorderWidth = 1;
        tc_chem_deact.Width = 50;
        tr_Subheader.Cells.Add(tc_chem_deact);

        TableCell tc_Terr_add = new TableCell();
        Literal lit_Terr_add = new Literal();
        lit_Terr_add.Text = "<center>Add</center>";
        tc_Terr_add.Controls.Add(lit_Terr_add);
        tc_Terr_add.BorderStyle = BorderStyle.Solid;
        tc_Terr_add.BorderWidth = 1;
        tc_Terr_add.Width = 50;
        tr_Subheader.Cells.Add(tc_Terr_add);

        TableCell tc_Terr_edit = new TableCell();
        Literal lit_Terr_edit = new Literal();
        lit_Terr_edit.Text = "<center>Edit</center>";
        tc_Terr_edit.Controls.Add(lit_Terr_edit);
        tc_Terr_edit.BorderStyle = BorderStyle.Solid;
        tc_Terr_edit.BorderWidth = 1;
        tc_Terr_edit.Width = 50;
        tr_Subheader.Cells.Add(tc_Terr_edit);

        TableCell tc_Terr_del = new TableCell();
        Literal lit_Terr_del = new Literal();
        lit_Terr_del.Text = "<center>Del.</center>";
        tc_Terr_del.Controls.Add(lit_Terr_del);
        tc_Terr_del.BorderStyle = BorderStyle.Solid;
        tc_Terr_del.BorderWidth = 1;
        tc_Terr_del.Width = 50;
        tr_Subheader.Cells.Add(tc_Terr_del);

        TableCell tc_Terr_deact = new TableCell();
        Literal lit_Terr_deact = new Literal();
        lit_Terr_deact.Text = "<center>Deact.</center>";
        tc_Terr_deact.Controls.Add(lit_Terr_deact);
        tc_Terr_deact.BorderStyle = BorderStyle.Solid;
        tc_Terr_deact.BorderWidth = 1;
        tc_Terr_deact.Width = 50;
        tr_Subheader.Cells.Add(tc_Terr_deact);

        //tbl.Rows.Add(tr_Subheader);
        tblFF.Rows.Add(tr_Subheader);

        // Details Section
        SalesForce sf = new SalesForce();
        dsSalesForce = sf.getDoctorCount_statewise(div_code, ddlState.SelectedValue.ToString());
        int row_id = 2;
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            foreach (DataRow drFF in dsSalesForce.Tables[0].Rows)
            {
                
                TableRow tr_det = new TableRow();
                tr_det.BackColor = System.Drawing.Color.White;
                TableCell tc_det_FF_code = new TableCell();
                Literal lit_det_FF_code = new Literal();
                lit_det_FF_code.ID = "sf_code_" + row_id;
                lit_det_FF_code.Text = "&nbsp;" + drFF["sf_code"].ToString();
                tc_det_FF_code.BorderStyle = BorderStyle.Solid;
                tc_det_FF_code.BorderWidth = 1;
                tc_det_FF_code.Controls.Add(lit_det_FF_code);
                tc_det_FF_code.Visible = false;
                tr_det.Cells.Add(tc_det_FF_code);

                TableCell tc_det_FF = new TableCell();
                Literal lit_det_FF = new Literal();
                lit_det_FF.Text = "&nbsp;" + drFF["sf_name"].ToString();
                tc_det_FF.BorderStyle = BorderStyle.Solid;
                tc_det_FF.BorderWidth = 1;
                tc_det_FF.Controls.Add(lit_det_FF);
                tr_det.Cells.Add(tc_det_FF);

                TableCell tc_doctor_add_chk = new TableCell();
                CheckBox chk_doctor_add = new CheckBox();
                tc_doctor_add_chk.Controls.Add(chk_doctor_add);
                tc_doctor_add_chk.BorderStyle = BorderStyle.Solid;
                tc_doctor_add_chk.BorderWidth = 1;
                tc_doctor_add_chk.HorizontalAlign = HorizontalAlign.Center;
                tc_doctor_add_chk.VerticalAlign = VerticalAlign.Middle;
                tr_det.Cells.Add(tc_doctor_add_chk);

                TableCell tc_doctor_edit_chk = new TableCell();
                CheckBox chk_doctor_edit = new CheckBox();
                tc_doctor_edit_chk.Controls.Add(chk_doctor_edit);
                tc_doctor_edit_chk.BorderStyle = BorderStyle.Solid;
                tc_doctor_edit_chk.BorderWidth = 1;
                tc_doctor_edit_chk.HorizontalAlign = HorizontalAlign.Center;
                tc_doctor_edit_chk.VerticalAlign = VerticalAlign.Middle;
                tr_det.Cells.Add(tc_doctor_edit_chk);

                TableCell tc_doctor_del_chk = new TableCell();
                CheckBox chk_doctor_del = new CheckBox();
                tc_doctor_del_chk.Controls.Add(chk_doctor_del);
                tc_doctor_del_chk.BorderStyle = BorderStyle.Solid;
                tc_doctor_del_chk.BorderWidth = 1;
                tc_doctor_del_chk.HorizontalAlign = HorizontalAlign.Center;
                tc_doctor_del_chk.VerticalAlign = VerticalAlign.Middle;
                tr_det.Cells.Add(tc_doctor_del_chk);

                TableCell tc_doctor_deact_chk = new TableCell();
                CheckBox chk_doctor_deact = new CheckBox();
                tc_doctor_deact_chk.Controls.Add(chk_doctor_deact);
                tc_doctor_deact_chk.BorderStyle = BorderStyle.Solid;
                tc_doctor_deact_chk.BorderWidth = 1;
                tc_doctor_deact_chk.HorizontalAlign = HorizontalAlign.Center;
                tc_doctor_deact_chk.VerticalAlign = VerticalAlign.Middle;
                tr_det.Cells.Add(tc_doctor_deact_chk);


                TableCell tc_Newdoctor_add_chk = new TableCell();
                CheckBox chk_Newdoctor_add = new CheckBox();
                tc_Newdoctor_add_chk.Controls.Add(chk_Newdoctor_add);
                tc_Newdoctor_add_chk.BorderStyle = BorderStyle.Solid;
                tc_Newdoctor_add_chk.BorderWidth = 1;
                tc_Newdoctor_add_chk.HorizontalAlign = HorizontalAlign.Center;
                tc_Newdoctor_add_chk.VerticalAlign = VerticalAlign.Middle;
                tr_det.Cells.Add(tc_Newdoctor_add_chk);

                TableCell tc_Newdoctor_deact_chk = new TableCell();
                CheckBox chk_Newdoctor_deact = new CheckBox();
                tc_Newdoctor_deact_chk.Controls.Add(chk_Newdoctor_deact);
                tc_Newdoctor_deact_chk.BorderStyle = BorderStyle.Solid;
                tc_Newdoctor_deact_chk.BorderWidth = 1;
                tc_Newdoctor_deact_chk.HorizontalAlign = HorizontalAlign.Center;
                tc_Newdoctor_deact_chk.VerticalAlign = VerticalAlign.Middle;
                tr_det.Cells.Add(tc_Newdoctor_deact_chk);


                TableCell tc_chem_add_chk = new TableCell();
                CheckBox chk_chem_add = new CheckBox();
                tc_chem_add_chk.Controls.Add(chk_chem_add);
                tc_chem_add_chk.BorderStyle = BorderStyle.Solid;
                tc_chem_add_chk.BorderWidth = 1;
                tc_chem_add_chk.HorizontalAlign = HorizontalAlign.Center;
                tc_chem_add_chk.VerticalAlign = VerticalAlign.Middle;
                tr_det.Cells.Add(tc_chem_add_chk);

                TableCell tc_chem_edit_chk = new TableCell();
                CheckBox chk_chem_edit = new CheckBox();
                tc_chem_edit_chk.Controls.Add(chk_chem_edit);
                tc_chem_edit_chk.BorderStyle = BorderStyle.Solid;
                tc_chem_edit_chk.BorderWidth = 1;
                tc_chem_edit_chk.HorizontalAlign = HorizontalAlign.Center;
                tc_chem_edit_chk.VerticalAlign = VerticalAlign.Middle;
                tr_det.Cells.Add(tc_chem_edit_chk);

                TableCell tc_chem_deact_chk = new TableCell();
                CheckBox chk_chem_deact = new CheckBox();
                tc_chem_deact_chk.Controls.Add(chk_chem_deact);
                tc_chem_deact_chk.BorderStyle = BorderStyle.Solid;
                tc_chem_deact_chk.BorderWidth = 1;
                tc_chem_deact_chk.HorizontalAlign = HorizontalAlign.Center;
                tc_chem_deact_chk.VerticalAlign = VerticalAlign.Middle;
                tr_det.Cells.Add(tc_chem_deact_chk);

                TableCell tc_Terr_add_chk = new TableCell();
                CheckBox chk_Terr_add = new CheckBox();
                tc_Terr_add_chk.Controls.Add(chk_Terr_add);
                tc_Terr_add_chk.BorderStyle = BorderStyle.Solid;
                tc_Terr_add_chk.BorderWidth = 1;
                tc_Terr_add_chk.HorizontalAlign = HorizontalAlign.Center;
                tc_Terr_add_chk.VerticalAlign = VerticalAlign.Middle;
                tr_det.Cells.Add(tc_Terr_add_chk);

                TableCell tc_Terr_edit_chk = new TableCell();
                CheckBox chk_Terr_edit = new CheckBox();
                tc_Terr_edit_chk.Controls.Add(chk_Terr_edit);
                tc_Terr_edit_chk.BorderStyle = BorderStyle.Solid;
                tc_Terr_edit_chk.BorderWidth = 1;
                tc_Terr_edit_chk.HorizontalAlign = HorizontalAlign.Center;
                tc_Terr_edit_chk.VerticalAlign = VerticalAlign.Middle;
                tr_det.Cells.Add(tc_Terr_edit_chk);

                TableCell tc_Terr_del_chk = new TableCell();
                CheckBox chk_Terr_del = new CheckBox();
                tc_Terr_del_chk.Controls.Add(chk_Terr_del);
                tc_Terr_del_chk.BorderStyle = BorderStyle.Solid;
                tc_Terr_del_chk.BorderWidth = 1;
                tc_Terr_del_chk.HorizontalAlign = HorizontalAlign.Center;
                tc_Terr_del_chk.VerticalAlign = VerticalAlign.Middle;
                tr_det.Cells.Add(tc_Terr_del_chk);

                TableCell tc_Terr_deact_chk = new TableCell();
                CheckBox chk_Terr_deact = new CheckBox();
                tc_Terr_deact_chk.Controls.Add(chk_Terr_deact);
                tc_Terr_deact_chk.BorderStyle = BorderStyle.Solid;
                tc_Terr_deact_chk.BorderWidth = 1;
                tc_Terr_deact_chk.HorizontalAlign = HorizontalAlign.Center;
                tc_Terr_deact_chk.VerticalAlign = VerticalAlign.Middle;
                tr_det.Cells.Add(tc_Terr_deact_chk);

                //tbl.Rows.Add(tr_det);
                tblFF.Rows.Add(tr_det);

                row_id = row_id + 1;
            }
        }

        pnlTable.Controls.Add(tblFF);
        //Session["tbl"] = tbl;
    }

    private void FillState(string div_code)
    {
        Division dv = new Division();
        dsDivision = dv.getStatePerDivision(div_code);
        if (dsDivision.Tables[0].Rows.Count > 0)
        {
            int i = 0;
            state_cd = string.Empty;
            sState = dsDivision.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
            statecd = sState.Split(',');
            foreach (string st_cd in statecd)
            {
                if (i == 0)
                {
                    state_cd = state_cd + st_cd;
                }
                else
                {
                    if (st_cd.Trim().Length > 0)
                    {
                        state_cd = state_cd + "," + st_cd;
                    }
                }
                i++;
            }

            State st = new State();
            dsState = st.getState(state_cd);
            ddlState.DataTextField = "statename";
            ddlState.DataValueField = "state_code";
            ddlState.DataSource = dsState;
            ddlState.DataBind();
            ddlState.SelectedIndex = 0;

        }
    }

    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlState.SelectedIndex > 0)
        {
            FillSalesForce();
            btnClear.Visible = true;
            btnSubmit.Visible = true;
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        int rowsCount = 5;
        string sf_code = string.Empty;
        string sf_name = string.Empty;

        Table table = (Table)Page.FindControl("Table1");
        //Table table = (Table)Page.FindControl("tbl");
        //if (Session["tbl"] != null)
        //    tbl = (Table)Session["tbl"];

        if ((table != null) && (table.Rows.Count > 0))
        {
            for (int i = 2; i < rowsCount; i++)
            {
                for (int j = 0; j < 15; j++)
                {
                    //Extracting the Dynamic Controls from the Table
                    Literal lit_sf_code = (Literal)table.Rows[i].Cells[j].FindControl("sf_code_" + i);
                    sf_code = lit_sf_code.Text;
                }
            }
        }
    }
}