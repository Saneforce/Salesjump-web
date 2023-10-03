using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using Bus_EReport;

public partial class Invoice_Print : System.Web.UI.Page
{
    string divcode = string.Empty;
    DataSet dsDivision = null;
    DataSet dsDivision1 = null;
    string sl_no = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        divcode = Convert.ToString(Session["div_code"]);
        if (!Page.IsPostBack)
        {


           

            try
            {
                if (!string.IsNullOrEmpty(Request.QueryString["Or_Date"]) && !string.IsNullOrEmpty(Request.QueryString["Dis_code"]))
                {
                    string or_date = Request.QueryString["Or_Date"];
                    DateTime orderdate = Convert.ToDateTime(or_date);
                    string ord_date = orderdate.ToString("yyyy-MM-dd");
                    string dis_code = Request.QueryString["Dis_code"];
                    string cus_code = Request.QueryString["Cus_code"];
                    Division dv = new Division();
                    dsDivision = dv.getslno(ord_date, dis_code, cus_code);
                    if (dsDivision.Tables[0].Rows.Count > 0)
                    {
                        sl_no = dsDivision.Tables[0].Rows[0].ItemArray.GetValue(0).ToString().Trim();

                    }
                    imgLoad(divcode);
                    Filladdress(divcode);
                    Fillorder_in();
                    Fillsale_info();
                    FillTab();

                }
            }
            catch (Exception)
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Invoice Not generator');</script>");
            }


        }


    }

    private void imgLoad(string div_code)
    {
        Division dv = new Division();
        dsDivision = dv.getDivision_Logo(div_code);
        if (dsDivision.Tables[0].Rows.Count > 0)
        {
            imglog.Src = "limg/" + dsDivision.Tables[0].Rows[0].ItemArray.GetValue(0).ToString().Trim() + "_logo.png";
          //  Response.Write("limg/" + dsDivision.Tables[0].Rows[0].ItemArray.GetValue(0).ToString().Trim() + "_logo.png");
        }
        
    }

    private void Filladdress(string div_code)
    {
        Division dv = new Division();
        dsDivision = dv.getDivision_Add(div_code);
        if (dsDivision.Tables[0].Rows.Count > 0)
        {
            Lab_cmp_name.Text = dsDivision.Tables[0].Rows[0].ItemArray.GetValue(0).ToString().Trim();
            Lab_add.Text = dsDivision.Tables[0].Rows[0].ItemArray.GetValue(1).ToString().Trim();
            Lab_city.Text = dsDivision.Tables[0].Rows[0].ItemArray.GetValue(2).ToString().Trim();
        }
    }
    private void Fillorder_in()
    {
        Division dv = new Division();
        dsDivision = dv.getorder_Add(sl_no);
        if (dsDivision.Tables[0].Rows.Count > 0)
        {

            Lab_invno.Text = dsDivision.Tables[0].Rows[0].ItemArray.GetValue(0).ToString().Trim();
            Lab_inv_date.Text = dsDivision.Tables[0].Rows[0].ItemArray.GetValue(1).ToString().Trim();
            Lab_or_date.Text = dsDivision.Tables[0].Rows[0].ItemArray.GetValue(2).ToString().Trim();
            Lab_date.Text = DateTime.Now.ToString("d/M/yyyy");
        }
    }
    private void Fillsale_info()
    {
        Division dv = new Division();
        dsDivision = dv.getsale_info(sl_no);
        if (dsDivision.Tables[0].Rows.Count > 0)
        {

            Lab_saleman.Text = dsDivision.Tables[0].Rows[0].ItemArray.GetValue(0).ToString().Trim();
            Lab_pay_term.Text = dsDivision.Tables[0].Rows[0].ItemArray.GetValue(1).ToString().Trim();
            Lab_pay_due.Text = dsDivision.Tables[0].Rows[0].ItemArray.GetValue(2).ToString().Trim();
            Lab_Sh_mt.Text = dsDivision.Tables[0].Rows[0].ItemArray.GetValue(3).ToString().Trim();
            Lab_sh_term.Text = dsDivision.Tables[0].Rows[0].ItemArray.GetValue(4).ToString().Trim();
            Lab_delivery_date.Text = dsDivision.Tables[0].Rows[0].ItemArray.GetValue(5).ToString().Trim();
        }
    }
    private void FillTab()
    {
        Division dv = new Division();
        dsDivision = dv.getOrder_info(sl_no, divcode);
        if (dsDivision.Tables[0].Rows.Count > 0)
        {

            rptOrders.DataSource = dsDivision;
            rptOrders.DataBind();

        }
    }




    protected void rptOrders_ItemDataBound1(object sender, RepeaterItemEventArgs e)
    {
        int totalItems = 0;
        totalItems = dsDivision.Tables[0].Rows.Count;
        //string sl_no = string.Empty;
        if (e.Item.ItemType == ListItemType.Item ||
       e.Item.ItemType == ListItemType.AlternatingItem)
        {
            if (e.Item.ItemIndex == totalItems - 1)
            {
                Repeater childRepeater = (Repeater)e.Item.FindControl("rptnext");
                Division dv = new Division();
                dsDivision1 = dv.getOrder_Tot(sl_no);
                if (dsDivision1.Tables[0].Rows.Count > 0)
                {

                    childRepeater.DataSource = dsDivision1;
                    childRepeater.DataBind();
                }
            }
        }
    }
}