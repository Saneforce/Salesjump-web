using System;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Windows;
using System.IO;
using System.Data;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html;
using iTextSharp.text.html.simpleparser;
using System.Text;
using Bus_EReport;
using System.Net;
using iTextSharp.tool.xml;


using Newtonsoft.Json;
using System.Web.Services;
using System.Data.SqlClient;

public partial class MIS_Reports_rpt_Customer_sales_analysis_productwise : System.Web.UI.Page
{
    string sfCode = string.Empty;
    string sfname = string.Empty;
    string divcode = string.Empty;
    decimal iTotLstCount1 = 0;
    decimal iTotLstCount = 0;
    decimal iTotLstCount2 = 0;
    int iMonth = -1;
    int iYear = -1;
    DataSet dsDoctor = null;
    string sMode = string.Empty;
    string FMonth = string.Empty;
    string FYear = string.Empty;
    string TMonth = string.Empty;
    string TYear = string.Empty;
    string stype = string.Empty;
    string Prod_Name = string.Empty;
    string sReturn = string.Empty;

    DataSet dsSalesForce = new DataSet();

    DataSet dsMGR = new DataSet();
    DataSet dsMr = new DataSet();
    DataSet dsDoc = null;
    DateTime dtCurrent;
    string tot_dr = string.Empty;
    string tot_Drr = string.Empty;

    string Monthsub = string.Empty;
    string strSf_Code = string.Empty;

    DataSet dsprd = new DataSet();
    string sCurrentDate = string.Empty;
    string stockist_code = string.Empty;
    string stURL = string.Empty;
    string Stock_name = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        divcode = Session["div_code"].ToString();
        sfCode = Request.QueryString["SF_Code"].ToString();
        FYear = Request.QueryString["FYear"].ToString();
        FMonth = Request.QueryString["FMonth"].ToString();
        hidn_sf_code.Value = sfCode;
        lblsf_name.Text = Request.QueryString["SF_Name"].ToString(); ;
        lblyear.Text = Request.QueryString["MonthNa"].ToString() + " - " + FYear;
        hdnYear.Value = FYear;
        hdnMonth.Value = FMonth;

        //FillSF();
        FillSFProd();
    }
    public class Item
    {
        public string product_id { get; set; }
        public string product_name { get; set; }
    }

    [WebMethod(EnableSession = true)]
    public static Item[] getdata()
    {
        string div_code = "";
        try
        {
            div_code = HttpContext.Current.Session["div_code"].ToString();

        }
        catch
        {
            div_code = HttpContext.Current.Session["Division_Code"].ToString();
        }

        Product pro = new Product();
        List<Item> empList = new List<Item>();
        DataSet dsAccessmas = pro.getproductname(div_code.TrimEnd(','));
        foreach (DataRow row in dsAccessmas.Tables[0].Rows)
        {
            Item emp = new Item();
            emp.product_id = row["Product_Detail_Code"].ToString();
            emp.product_name = row["Product_Short_Name"].ToString();
            empList.Add(emp);
        }
        return empList.ToArray();
    }



    public class Routs
    {
        public string RouteCode { get; set; }
        public string RouteName { get; set; }
    }


    [WebMethod(EnableSession = true)]
    public static Routs[] GetRoutes(string SF_Code)
    {
        string div_code = "";
        string sf_Code = "";
        try
        {
            div_code = HttpContext.Current.Session["div_code"].ToString();

        }
        catch
        {
            div_code = HttpContext.Current.Session["Division_Code"].ToString();
        }

        sf_Code = HttpContext.Current.Session["SF_Code"].ToString();
        RoutePlan rop = new RoutePlan();
        DataSet DsRoute = rop.get_Route_Name(div_code, SF_Code);
        List<Routs> rts = new List<Routs>();
        foreach (DataRow row in DsRoute.Tables[0].Rows)
        {
            Routs rt = new Routs();
            rt.RouteCode = row["Territory_Code"].ToString();
            rt.RouteName = row["Territory_Name"].ToString();
            rts.Add(rt);
        }
        return rts.ToArray();
    }





    [WebMethod(EnableSession = true)]
    public static RetailerValues[] GetRetaile(string SF_Code, string FYear)
    {
        string div_code = "";
        string sf_Code = "";
        try
        {
            div_code = HttpContext.Current.Session["div_code"].ToString();

        }
        catch
        {
            div_code = HttpContext.Current.Session["Division_Code"].ToString();
        }

        //sf_Code = HttpContext.Current.Session["SF_Code"].ToString();

        ListedDR ldr = new ListedDR();
        DataSet DsRetailer = ldr.Get_Retailer(div_code, "0", SF_Code);

        List<RetailerValues> rts = new List<RetailerValues>();
        foreach (DataRow row in DsRetailer.Tables[0].Rows)
        {
            RetailerValues rt = new RetailerValues();
            rt.Route = row["Route"].ToString();
            rt.ListedDrCode = row["ListedDrCode"].ToString();
            rt.ListedDr_Name = row["ListedDr_Name"].ToString();
            rt.Doc_Special_Name = row["Doc_Special_Name"].ToString();
            rts.Add(rt);
        }
        return rts.ToArray();
    }

    public class RetailerValues
    {
        public string Route { get; set; }
        public string ListedDrCode { get; set; }
        public string ListedDr_Name { get; set; }
        public string ord_val { get; set; }
        public string Doc_Special_Name { get; set; }
        public string prd_code { get; set; }
        public string prd_name { get; set; }

    }

    [WebMethod(EnableSession = true)]
    public static RetailerValues[] GetRetailerProdctValues(string SF_Code, string FYear, string FMonth)
    {
        string div_code = "";
        string sf_Code = "";
        try
        {
            div_code = HttpContext.Current.Session["div_code"].ToString();

        }
        catch
        {
            div_code = HttpContext.Current.Session["Division_Code"].ToString();
        }

      //  sf_Code = HttpContext.Current.Session["SF_Code"].ToString();

        ListedDR ldr = new ListedDR();
        DataSet DsRetailer = ldr.Get_Retailer_sal_ProductWise(div_code, FYear, FMonth, SF_Code);
        List<RetailerValues> rts = new List<RetailerValues>();
        foreach (DataRow row in DsRetailer.Tables[0].Rows)
        {
            RetailerValues rt = new RetailerValues();
            rt.ListedDrCode = row["Cust_Code"].ToString();
            rt.ListedDr_Name = row["ListedDr_Name"].ToString();
            rt.ord_val = row["ord_val"].ToString();
            rt.prd_code = row["prd_code"].ToString();
            rts.Add(rt);
        }
        return rts.ToArray();
    }

    private void FillSF()
    {


        Product pro = new Product();

        DataSet dsAccessmas = pro.getproductname(divcode.TrimEnd(','));


        // RoutePlan rop = new RoutePlan();
        // DataSet DsRoute = rop.get_Route_Name(divcode, sfCode);

        ListedDR ldr = new ListedDR();
        // DataSet DsRetailer = ldr.Get_Retailer(divcode, FYear, sfCode);




        DataSet DsProductsVAl = ldr.Get_Retailer_sal_ProductWise(divcode, FYear, FMonth, sfCode);
        tbl.Rows.Clear();

        if (dsAccessmas.Tables[0].Rows.Count > 0)
        {
            TableRow tr_header = new TableRow();
            tr_header.BorderStyle = BorderStyle.Solid;
            tr_header.BorderWidth = 1;
            tr_header.BackColor = System.Drawing.Color.FromName("#496a9a");
            tr_header.Style.Add("Color", "White");
            tr_header.BorderColor = System.Drawing.Color.Black;

            TableCell tcName = new TableCell();
            tcName.BorderStyle = BorderStyle.Solid;
            tcName.BorderWidth = 1;
            tcName.Width = 300;
            tcName.Height = 25;

            tcName.RowSpan = 1;
            tcName.HorizontalAlign = HorizontalAlign.Center;
            Literal litName = new Literal();
            litName.Text = "Name";
            tcName.Controls.Add(litName);
            tcName.BorderColor = System.Drawing.Color.Black;
            tcName.Attributes.Add("Class", "rptCellBorder");

            tcName.Attributes.Add("style", "min-width:300px;");
            tr_header.Cells.Add(tcName);

            //TableCell tcChanel = new TableCell();
            //tcChanel.BorderStyle = BorderStyle.Solid;
            //tcChanel.BorderWidth = 1;
            //tcChanel.Width = 150;
            //tcChanel.RowSpan = 1;
            //tcChanel.HorizontalAlign = HorizontalAlign.Center;
            //Literal litChanel = new Literal();
            //litChanel.Text = "Chanel";
            //tcChanel.Controls.Add(litChanel);
            //tcChanel.BorderColor = System.Drawing.Color.Black;
            //tcChanel.Attributes.Add("Class", "rptCellBorder");
            //tcChanel.Attributes.Add("style", "min-width:150px;");
            //tr_header.Cells.Add(tcChanel);

            foreach (DataRow row in dsAccessmas.Tables[0].Rows)
            {
                TableCell tcProName = new TableCell();
                tcProName.BorderStyle = BorderStyle.Solid;
                tcProName.BorderWidth = 1;
                tcProName.Width = 150;
                tcProName.RowSpan = 1;
                tcProName.HorizontalAlign = HorizontalAlign.Center;
                Literal litProName = new Literal();
                litProName.Text = row["Product_Short_Name"].ToString();
                tcProName.Controls.Add(litProName);
                tcProName.BorderColor = System.Drawing.Color.Black;
                tcProName.Attributes.Add("Class", "rptCellBorder");
                tcProName.Attributes.Add("style", "min-width:150px;");
                tr_header.Cells.Add(tcProName);
            }

            tbl.Rows.Add(tr_header);

            //form1.Controls.Add(tb1);

            //foreach (DataRow dr in DsRoute.Tables[0].Rows)
            //{
            //    TableRow tr_det = new TableRow();

            //    TableCell tcRoute = new TableCell();
            //    Literal litRoute = new Literal();
            //    litRoute.Text = "&nbsp;" + dr["Territory_Name"].ToString();
            //    tcRoute.BorderStyle = BorderStyle.Solid;
            //    tcRoute.BorderWidth = 1;
            //    tcRoute.Attributes.Add("Class", "rptCellBorder");
            //    tcRoute.Controls.Add(litRoute);
            //    tr_det.Cells.Add(tcRoute);
            //    tb1.Rows.Add(tr_det);

            //    DataRow[] drow = DsRetailer.Tables[0].Select("Route = '" + dr["Territory_Code"].ToString() + "'");
            //    if (drow.Length > 0)
            //    {
            foreach (DataRow row in DsProductsVAl.Tables[0].Rows)
            {
                TableRow itr_det = new TableRow();
                TableCell tcRetailer = new TableCell();
                Literal litRetailer = new Literal();
                litRetailer.Text = "&nbsp;" + row["ListedDr_Name"].ToString(); // row["ListedDrCode"].ToString(),, row["Doc_Special_Name"].ToString()
                tcRetailer.BorderStyle = BorderStyle.Solid;
                tcRetailer.BorderWidth = 1;
                tcRetailer.Attributes.Add("Class", "rptCellBorder");
                tcRetailer.Controls.Add(litRetailer);
                itr_det.Cells.Add(tcRetailer);


                //TableCell tcChanelV = new TableCell();
                //Literal littcChanelV = new Literal();
                //littcChanelV.Text = "&nbsp;" + row["Doc_Special_Name"].ToString(); // row["ListedDrCode"].ToString(),, row["Doc_Special_Name"].ToString()
                //tcChanelV.BorderStyle = BorderStyle.Solid;
                //tcChanelV.BorderWidth = 1;
                //tcChanelV.Attributes.Add("Class", "rptCellBorder");
                //tcChanelV.Controls.Add(littcChanelV);
                //itr_det.Cells.Add(tcChanelV);



                foreach (DataRow rw in dsAccessmas.Tables[0].Rows)
                {

                    TableCell tcProValue = new TableCell();
                    Literal litProValue = new Literal();
                    //  string pVal = "";

                    //if (rw["Product_Detail_Code"].ToString() == row["prd_code"].ToString())
                    //    {
                    //        pVal = "&nbsp;" + row["ord_val"].ToString();
                    //    }


                    //  litProValue.Text = "&nbsp;" + i++; // row["ListedDrCode"].ToString(),, row["Doc_Special_Name"].ToString()
                    if (rw["Product_Detail_Code"].ToString() == row["prd_code"].ToString())
                    {
                        litProValue.Text = "&nbsp;" + row["ord_val"].ToString(); ; // row["ListedDrCode"].ToString(),, row["Doc_Special_Name"].ToString()
                    }
                    else
                    {
                        litProValue.Text = ""; // row["ListedDrCode"].ToString(),, row["Doc_Special_Name"].ToString()
                    }
                    tcProValue.BorderStyle = BorderStyle.Solid;
                    tcProValue.BorderWidth = 1;
                    tcProValue.Attributes.Add("Class", "rptCellBorder");
                    tcProValue.Attributes.Add("style", "text-align: right;");

                    tcProValue.Controls.Add(litProValue);
                    itr_det.Cells.Add(tcProValue);


                }
                tbl.Rows.Add(itr_det);


            }

        }

    }









    private void FillSFProd()
    {


        Product pro = new Product();
        DataSet dsAccessmas = pro.getproductname(divcode.TrimEnd(','));


        ListedDR ldr = new ListedDR();
        DataSet DsProductsVAl = ldr.Get_Retailer_sal_ProductWise_pro(divcode, FYear, FMonth, sfCode);
        tbl.Rows.Clear();

        if (dsAccessmas.Tables[0].Rows.Count > 0)
        {
            TableRow tr_header = new TableRow();
            tr_header.BorderStyle = BorderStyle.Solid;
            tr_header.BorderWidth = 1;
            tr_header.BackColor = System.Drawing.Color.FromName("#496a9a");
            tr_header.Style.Add("Color", "White");
            tr_header.BorderColor = System.Drawing.Color.Black;

            TableCell tcName = new TableCell();
            tcName.BorderStyle = BorderStyle.Solid;
            tcName.BorderWidth = 1;
            tcName.Width = 300;
            tcName.Height = 25;

            tcName.RowSpan = 1;
            tcName.HorizontalAlign = HorizontalAlign.Center;
            Literal litName = new Literal();
            litName.Text = "Name";
            tcName.Controls.Add(litName);
            tcName.BorderColor = System.Drawing.Color.Black;
            tcName.Attributes.Add("Class", "rptCellBorder");

            tcName.Attributes.Add("style", "min-width:300px;");
            tr_header.Cells.Add(tcName);



            foreach (DataRow row in dsAccessmas.Tables[0].Rows)
            {
                TableCell tcProName = new TableCell();
                tcProName.BorderStyle = BorderStyle.Solid;
                tcProName.BorderWidth = 1;
                tcProName.Width = 150;
                tcProName.RowSpan = 1;
                tcProName.HorizontalAlign = HorizontalAlign.Center;
                Literal litProName = new Literal();
                litProName.Text = row["Product_Short_Name"].ToString();
                tcProName.Controls.Add(litProName);
                tcProName.BorderColor = System.Drawing.Color.Black;
                tcProName.Attributes.Add("Class", "rptCellBorder");
                tcProName.Attributes.Add("style", "min-width:150px;");
                tr_header.Cells.Add(tcProName);
            }

            tbl.Rows.Add(tr_header);

            int len = dsAccessmas.Tables[0].Rows.Count;

            foreach (DataRow row in DsProductsVAl.Tables[0].Rows)
            {
                TableRow itr_det = new TableRow();
                TableCell tcRetailer = new TableCell();
                Literal litRetailer = new Literal();
                litRetailer.Text = "&nbsp;" + row["ListedDr_Name"].ToString();
                tcRetailer.BorderStyle = BorderStyle.Solid;
                tcRetailer.BorderWidth = 1;
                tcRetailer.Attributes.Add("Class", "rptCellBorder");
                tcRetailer.Controls.Add(litRetailer);
                itr_det.Cells.Add(tcRetailer);

                int col = 0;
                for (int k = 0; k < len; k++)
                {
                    col = k + 2;
                    TableCell tcProValue = new TableCell();
                    Literal litProValue = new Literal();
                    litProValue.Text = row[col] == DBNull.Value ? "" : row[col].ToString();  // row["ListedDrCode"].ToString(),, row["Doc_Special_Name"].ToString()
                    tcProValue.BorderStyle = BorderStyle.Solid;
                    tcProValue.BorderWidth = 1;
                    tcProValue.Attributes.Add("Class", "rptCellBorder");
                    tcProValue.Attributes.Add("style", "text-align: right;");
                    tcProValue.Controls.Add(litProValue);
                    itr_det.Cells.Add(tcProValue);
                }
                tbl.Rows.Add(itr_det);

            }

        }

    }

}