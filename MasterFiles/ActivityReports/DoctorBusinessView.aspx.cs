using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Bus_EReport;
using System.Drawing;

public partial class DoctorBusinessView : System.Web.UI.Page
{
    DataSet dsSalesForce = null;
    DataSet dsTP = null;
    DataSet dsTarget = null;
    string div_code = string.Empty;
    string sfCode = string.Empty, fromMonth = string.Empty, fromYear = string.Empty, toMonth = string.Empty, toYear = string.Empty;
    SampleDespatch objSample = new SampleDespatch();
    SalesForce sf = new SalesForce();
    DCRBusinessEntry objBusiness = new DCRBusinessEntry();
    
    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        sfCode = Session["sf_code"].ToString();

        if (!IsPostBack)
        {
            menu.FindControl("btnBack").Visible = false;
            this.FillMasterList();
            TourPlan tp = new TourPlan();
            dsTP = tp.Get_TP_Edit_Year(div_code);
            if (dsTP.Tables[0].Rows.Count > 0)
            {
                for (int k = Convert.ToInt16(dsTP.Tables[0].Rows[0]["Year"]); k <= DateTime.Now.Year + 1; k++)
                {
                    ddlFromYear.Items.Add(k.ToString());
                    ddlFromYear.SelectedValue = DateTime.Now.Year.ToString();
                    ddlToYear.Items.Add(k.ToString());
                    ddlToYear.SelectedValue = DateTime.Now.Year.ToString();
                }
            }
        }
    }

    private void BindGrid(string sfCode, string fromMonthYear, string strToMonthYear)
    {
       
        dsTarget = objBusiness.GetDCRBusinessReport(sfCode, fromMonthYear, strToMonthYear);
        gvDoctorBusiness.DataSource = dsTarget;
        gvDoctorBusiness.DataBind();
    }
    protected void gvDoctorBusiness_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        ////if (e.Row.RowType == DataControlRowType.DataRow)
        ////{
        ////    GridViewRow grow = e.Row;



        ////    for (int cnt = 4; cnt < grow.Cells.Count; cnt++)
        ////    {
                
        ////         //HiddenField hdnCntl = new HiddenField();
        ////            HiddenField hdnCntl = new HiddenField();
        ////            hdnCntl.ID = "hdnCntl"+cnt.ToString();
        ////            hdnCntl.Value = gvDoctorBusiness.HeaderRow.Cells[cnt].Text;
        ////            grow.Cells[cnt].Controls.Add(hdnCntl);
                   

        ////    }
        ////}

        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridViewRow gheaderrow = e.Row;
            
            gheaderrow.Cells[1].ForeColor = Color.White;
            gheaderrow.Cells[2].ForeColor = Color.White;
            gheaderrow.Cells[3].ForeColor = Color.White;

            for (int cnt = 4; cnt < gheaderrow.Cells.Count; cnt++)
            {
                gheaderrow.Cells[cnt].ForeColor = Color.White;
                if ((cnt % 2) == 0)
                {
                    //HiddenField hdnCntl = new HiddenField();
                    HiddenField hdnCntl = new HiddenField();
                    hdnCntl.ID = "hdnCntl" + cnt.ToString();
                    hdnCntl.Value = gheaderrow.Cells[cnt].Text;
                    gheaderrow.Cells[cnt].Controls.Add(hdnCntl);
                    Label lblCntl = new Label();
                    lblCntl.ID = "lblCntl" + cnt.ToString();
                    lblCntl.Text = "Qty";
                    gheaderrow.Cells[cnt].Controls.Add(lblCntl);
                    //gheaderrow.Cells[cnt].Text = "Qty";

                    
                }
                else
                {
                    gheaderrow.Cells[cnt].Text = "Value";
                }

            }
        }
        else if (e.Row.RowType == DataControlRowType.DataRow)
        {
            GridViewRow grow = e.Row;
            for (int cnt = 4; cnt < grow.Cells.Count; cnt++)
            {
                if ((cnt % 2) == 0)
                {
                    HyperLink link = new HyperLink();
                    if (e.Row.Cells[cnt].Text != string.Empty && e.Row.Cells[cnt].Text != "&nbsp;")
                    {
                        HiddenField hdnCntl = (HiddenField)gvDoctorBusiness.HeaderRow.FindControl("hdnCntl" + cnt.ToString());
                        link.Text = e.Row.Cells[cnt].Text;
                        link.Font.Underline = true;
                        string headerText = gvDoctorBusiness.HeaderRow.Cells[cnt].Text;
                        string strSfCode = e.Row.Cells[0].Text;
                        //link.NavigateUrl = "javascript:OpenReport('" + strSfCode + "','" + headerText + "');";
                        //link.Attributes.Add("onclick", "window.opener.location =DoctorBusinessViewProjects.aspx?sfCode="+ strSfCode + "&monthYear=" + headerText +"; window.close();");
                        link.Attributes.Add("onclick", "var windowHandle = window.open('DoctorBusinessViewProjects.aspx?sfCode=" + strSfCode + "&sfName=" + hdnCntl.Value + "&monthYear=" + hdnCntl.Value + "','','height=600,width=600');return false");
                        e.Row.Cells[cnt].Controls.Add(link);
                        //gvDoctorBusiness.HeaderRow.Cells[cnt].Text = "Qty";
                    }
                    
                }
            }

            e.Row.Cells[0].Visible = false;
            gvDoctorBusiness.HeaderRow.Cells[0].Visible = false;
            gvDoctorBusiness.HeaderStyle.ForeColor = System.Drawing.Color.White;
        }
        else if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[0].Visible = false;
            GridViewRow grow = e.Row;
            e.Row.Cells[3].Text = "Total";
            e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Right;

            for (int cnt = 4; cnt < grow.Cells.Count; cnt++)
            {
                if ((cnt % 2) != 0)
                {
                    //HiddenField hdnCntl = (HiddenField)gvDoctorBusiness.HeaderRow.FindControl("hdnTotal" + cnt.ToString());
                    //e.Row.Cells[cnt].Text = hdnCntl.Value;

                    int rowValue = 0;
                    if (e.Row.Cells[cnt].Text != string.Empty && e.Row.Cells[cnt].Text != "&nbsp;")
                    {
                        rowValue = Convert.ToInt32(e.Row.Cells[cnt].Text);
                    }

                    DataSet dsDoctor = (DataSet)gvDoctorBusiness.DataSource;
                    if (dsDoctor.Tables.Count > 0)
                    {
                        DataTable dtDoctor = dsDoctor.Tables[0];
                        object objTotal;
                        objTotal = dtDoctor.Compute("SUM([" + dtDoctor.Columns[cnt].ColumnName + "])", "");
                        e.Row.Cells[cnt].Text = Convert.ToString(objTotal);
                        //HiddenField hdnTotal = new HiddenField();
                        //hdnTotal.ID = "hdnTotal" + cnt;
                        //hdnTotal.Value = Convert.ToString(objTotal);
                        //gvDoctorBusiness.HeaderRow.Cells[cnt].Controls.Add(hdnTotal);
                    }
                }
            }
        }
    }

    private void FillMasterList()
    {
        dsSalesForce = sf.UserList_Alpha(div_code, sfCode);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            ddlFieldForce.DataTextField = "sf_name";
            ddlFieldForce.DataValueField = "sf_code";
            ddlFieldForce.DataSource = dsSalesForce;
            ddlFieldForce.DataBind();
        }
    }
    protected void CmdView_Click(object sender, EventArgs e)
    {
        string strFromMonthYear = string.Empty, strToMonthYear = string.Empty;
        fromMonth = ddlFromMonth.SelectedValue;
        fromYear = ddlFromYear.SelectedValue;
        toMonth = ddlToMonth.SelectedValue;
        toYear = ddlToYear.SelectedValue;

        if (fromMonth != string.Empty && fromYear != string.Empty)
        {
            strFromMonthYear = Convert.ToDateTime("01/" + fromMonth + "/" + fromYear).ToString("yyyyMMdd");
        }

        if (toMonth != string.Empty && toYear != string.Empty)
        {
            strToMonthYear = Convert.ToDateTime("01/" + toMonth + "/" + toYear).ToString("yyyyMMdd");
        }

        this.BindGrid(ddlFieldForce.SelectedValue, strFromMonthYear,strToMonthYear);
    }
    protected void gvDoctorBusiness_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {

            GridView HeaderGrid = (GridView)sender;

            GridViewRow HeaderRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            if (dsTarget != null)
            {
                if (dsTarget.Tables.Count > 0)
                {
                    DataTable dtTarget = dsTarget.Tables[0];

                    TableCell Cell_Header = new TableCell();

                    Cell_Header.Text = "";

                    Cell_Header.HorizontalAlign = HorizontalAlign.Center;

                    Cell_Header.ColumnSpan = 3;

                    HeaderRow.Cells.Add(Cell_Header);

                    for (int i = 4; i < dtTarget.Columns.Count; i++)
                    {
                        if ((i % 2) == 0)
                        {
                            Cell_Header = new TableCell();

                            Cell_Header.Text = dtTarget.Columns[i].ColumnName;

                            Cell_Header.HorizontalAlign = HorizontalAlign.Center;

                            Cell_Header.ColumnSpan = 2;

                            HeaderRow.Cells.Add(Cell_Header);
                        }
                    }
                }

                gvDoctorBusiness.Controls[0].Controls.AddAt(0, HeaderRow);
            }

        }
    }
}