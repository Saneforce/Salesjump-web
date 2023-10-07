using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Web.UI.HtmlControls;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Data;
using Bus_EReport;
using System.Data.SqlClient;
using System.Configuration;
using System.Drawing;
using System.ComponentModel;

public partial class Customer : System.Web.UI.Page
{
    string sfCode = string.Empty;
    string sfName = string.Empty;
    string state_code = string.Empty;
    string state_Name = string.Empty;
    string catagory = string.Empty;
     string div_code=string.Empty;
    
    protected void Page_Load(object sender, EventArgs e)
    {

        state_Name = Request.QueryString["state_Name"].ToString();    
        state_code = Request.QueryString["State_code"].ToString();
        catagory = Request.QueryString["catagory"].ToString();
       div_code = Session["div_code"].ToString();
     
        System.Globalization.DateTimeFormatInfo mfi = new System.Globalization.DateTimeFormatInfo();
      

        lblHead.Text = state_Name;
        BindGrid();


    }
    private void BindGrid()
    {
        DataSet dsRemarks1 = new DataSet();
      
        State st = new State();
        if (catagory == "1")
        {
            dsRemarks1 = st.get_area_Details(state_code,div_code);

            if (dsRemarks1.Tables[0].Rows.Count > 0)
            {

                GridView1.DataSource = dsRemarks1;
                GridView1.DataBind();
                btnClose.Visible = true;

            }
            else
            {
                GridView1.DataSource = null;
                GridView1.DataBind();
                btnClose.Visible = true;

            }
        }
        if (catagory == "2")
        {
            dsRemarks1 = st.get_zone_Details(state_code,div_code);

            if (dsRemarks1.Tables[0].Rows.Count > 0)
            {

                GridView1.DataSource = dsRemarks1;
                GridView1.DataBind();
                btnClose.Visible = true;

            }
            else
            {
                GridView1.DataSource = null;
                GridView1.DataBind();
                btnClose.Visible = true;

            }
        }
        if (catagory == "3")
        {
            dsRemarks1 = st.get_territory_Details(state_code,div_code);

            if (dsRemarks1.Tables[0].Rows.Count > 0)
            {

                GridView1.DataSource = dsRemarks1;
                GridView1.DataBind();
                btnClose.Visible = true;

            }
            else
            {
                GridView1.DataSource = null;
                GridView1.DataBind();
               btnClose.Visible = true;

            }
        }
        if (catagory == "4")
        {
            dsRemarks1 = st.get_area_Details(state_code,div_code);

            if (dsRemarks1.Tables[0].Rows.Count > 0)
            {

                GridView1.DataSource = dsRemarks1;
                GridView1.DataBind();
                btnClose.Visible = true;

            }
            else
            {
                GridView1.DataSource = null;
                GridView1.DataBind();
                btnClose.Visible = true;

            }
        }
    }

    protected void OnDataBound(object sender, EventArgs e)
    {
            for (int i = GridView1.Rows.Count - 1; i > 0; i--)
            {
                GridViewRow row = GridView1.Rows[i];
                GridViewRow previousRow = GridView1.Rows[i - 1];
                for (int j = 0; j < row.Cells.Count; j++)
                {
                    if (row.Cells[j].Text == previousRow.Cells[j].Text)
                    {
                        if (previousRow.Cells[j].RowSpan == 0)
                        {
                            if (row.Cells[j].RowSpan == 0)
                            {
                                previousRow.Cells[j].RowSpan += 2;
                            }
                            else
                            {
                                previousRow.Cells[j].RowSpan = row.Cells[j].RowSpan + 1;
                            }
                            row.Cells[j].Visible = false;
                        }
                    }
                }
            }
        }

    protected void btnClose_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        Session["ctrl"] = pnlContents;
        Control ctrl = (Control)Session["ctrl"];
        PrintWebControl(ctrl);
    }
    public static void PrintWebControl(Control ControlToPrint)
    {
        StringWriter stringWrite = new StringWriter();
        System.Web.UI.HtmlTextWriter htmlWrite = new System.Web.UI.HtmlTextWriter(stringWrite);
        if (ControlToPrint is WebControl)
        {
            Unit w = new Unit(100, UnitType.Percentage);
            ((WebControl)ControlToPrint).Width = w;
        }
        Page pg = new Page();
        pg.EnableEventValidation = false;
        HtmlForm frm = new HtmlForm();
        pg.Controls.Add(frm);
        frm.Attributes.Add("runat", "server");
        frm.Controls.Add(ControlToPrint);
        pg.DesignerInitialize();
        pg.RenderControl(htmlWrite);
        string strHTML = stringWrite.ToString();
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.Write(strHTML);
        HttpContext.Current.Response.Write("<script>window.print();</script>");
        HttpContext.Current.Response.End();

    }

    protected void btnExcel_Click(object sender, EventArgs e)
    {
        string attachment = "attachment; filename=DCRView.xls";
        Response.ClearContent();
        Response.AddHeader("content-disposition", attachment);
        Response.ContentType = "application/ms-excel";
        StringWriter sw = new StringWriter();
        HtmlTextWriter htw = new HtmlTextWriter(sw);
        HtmlForm frm = new HtmlForm();
        form1.Parent.Controls.Add(frm);
        frm.Attributes["runat"] = "server";
        frm.Controls.Add(pnlContents);
        frm.RenderControl(htw);
        Response.Write(sw.ToString());
        Response.End();
    }

   
    }


    
   
