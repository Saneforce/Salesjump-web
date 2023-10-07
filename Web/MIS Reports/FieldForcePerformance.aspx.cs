using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
using ClosedXML.Excel;
using System.IO;

public partial class MIS_Reports_FieldForcePerformance : System.Web.UI.Page
{
    string div_code = string.Empty;
    DataSet dsTP = null;
    string tot_dr = string.Empty;
    string tot_FWD = string.Empty;
    string tot_dcr_dr = string.Empty;
    string sCurrentDate = string.Empty;
    string sf_code = string.Empty;
    string sf_type = string.Empty;
    DateTime ServerStartTime;
    DataSet dsSalesForce = null;

    protected override void OnPreInit(EventArgs e)
    {
        base.OnPreInit(e);
        if (Session["sf_type"] != null)
        {
            sf_type = Session["sf_type"].ToString();
            if (sf_type == "3")
            {
                this.MasterPageFile = "~/Master.master";
            }
            else if (sf_type == "2")
            {
                this.MasterPageFile = "~/Master_MGR.master";
            }
            else if (sf_type == "1")
            {
                this.MasterPageFile = "~/Master_MR.master";
            }
        }
        else
        {
            Page.Response.Redirect(Page.Request.Url.ToString(), true);
        }
        
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["sf_type"] != null && Session["sf_code"] != null)
        {
            sf_code = Session["sf_code"].ToString();
            sf_type = Session["sf_type"].ToString();

            if (sf_type == "4")
            {
                div_code = Session["Division_Code"].ToString();

                div_code = div_code.TrimEnd(',');
            }
            else
            { div_code = Session["div_code"].ToString(); }
        }
        else { Page.Response.Redirect(Page.Request.Url.ToString(), true); }
        if (!Page.IsPostBack)
        {
            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);
            fillsubdivision();
            FillMRManagers("0");

            if (div_code == "98")
            {
                btnExcel.Visible = true;
            }
            else { btnExcel.Visible = false; }
        }


        //sf_code = Session["sf_code"].ToString();
        //sf_type = Session["sf_type"].ToString();
        //if (sf_type == "3")
        //{
        //    div_code = Session["div_code"].ToString();
        //}
        //else
        //{
        //    div_code = Session["div_code"].ToString();
        //}
        //if (!Page.IsPostBack)
        //{
        //    ServerStartTime = DateTime.Now;
        //    base.OnPreInit(e);

        //    fillsubdivision();
        //    FillMRManagers("0");

        //}
    }

    protected void subdiv_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (subdiv.SelectedValue.ToString() != "0")
        {

            FillMRManagers(subdiv.SelectedValue.ToString());
        }
        else
        {
            FillMRManagers(subdiv.SelectedValue.ToString());
        }
    }

    private void fillsubdivision()
    {
        SalesForce sd = new SalesForce();
		
		if (sf_type == "2")
        { dsSalesForce = sd.Getsubdivisionwise_sfcode(div_code, sf_code); }
        else
        { dsSalesForce = sd.Getsubdivisionwise(div_code); }
		
		
        //dsSalesForce = sd.Getsubdivisionwise(div_code);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            subdiv.DataTextField = "subdivision_name";
            subdiv.DataValueField = "subdivision_code";
            subdiv.DataSource = dsSalesForce;
            subdiv.DataBind();
            subdiv.Items.Insert(0, new ListItem("--Select--", "0"));
        }
    }

    private void FillMRManagers(string Sub_Div_Code)
    {
        SalesForce sf = new SalesForce();
        dsSalesForce = sf.SalesForceList(div_code, sf_code, Sub_Div_Code);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            ddlFieldForce.DataTextField = "sf_name";
            ddlFieldForce.DataValueField = "sf_code";
            ddlFieldForce.DataSource = dsSalesForce;
            ddlFieldForce.DataBind();
            ddlFieldForce.Items.Insert(0, "---Select Field Force---");
        }
    }

    protected void btnExcel_Click(object sender, EventArgs e)
    {
        string sfcode = ddlFieldForce.SelectedValue.ToString();
        string subdivisoncode = subdiv.SelectedValue.ToString();
        string FDT = txtfromdate.Text.ToString();
        string TDT = txttodate.Text.ToString();
        DCR SFD = new DCR();
        DataSet ds = SFD.getDayplanExcel(sfcode, div_code, FDT, TDT, subdivisoncode);
        System.Data.DataTable fdt = new System.Data.DataTable();

        if (ds.Tables.Count > 0)
        {
            fdt = ds.Tables[0];
        }
        if (fdt.Rows.Count > 0)
        {
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(fdt, "Customers");

                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename=FieldForceSummary.xlsx");

                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                }
            }
        }
    }

}