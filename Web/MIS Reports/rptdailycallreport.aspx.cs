
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
using iTextSharp.tool.xml;
using System.Net;
using System.Drawing;
using DBase_EReport;


public partial class MIS_Reports_rptdailycallreport : System.Web.UI.Page
{
    string sfCode = string.Empty;
    string sfname = string.Empty;
    string net = string.Empty;
    string netw = string.Empty;
    string divcode = string.Empty;
    int productive_count = 0;
    string distributor = string.Empty;
    string FMonth = string.Empty;
    string FYear = string.Empty;
    string TMonth = string.Empty;
    string TYear = string.Empty;
    string Prod = string.Empty;
    int sum_time = 0;
    decimal val;
    decimal val1;
    DataSet dsSalesForce = new DataSet();
    DataSet ss = new DataSet();
    DataSet cc = new DataSet();
    DataSet Ids = new DataSet();
    DataSet dsmgrsf = new DataSet();
    DataSet dsDoc = null;
    DataSet dsDoc1 = null;
    DataSet dsdocto = null;
    DateTime dtCurrent;
    TimeSpan ff;
    string sCurrentDate = string.Empty;
    string endTime = string.Empty;
    string startTime = string.Empty;
    string tot_dr = string.Empty;
    string tot_value = string.Empty;
    string Monthsub = string.Empty;
    string dist_name = string.Empty;
    string dist_code = string.Empty;
    string date = string.Empty;
    string endd = string.Empty;
    DataSet dsDoctor = new DataSet();
    string gg = string.Empty;
    decimal netwgttotal = 0;
    decimal valuetotal = 0;
    string Dist_Code = string.Empty;
    Int64[] totalqty;
    Int64 unit = 0;
    Int64 unittotal = 0;
    public int chk = 0;
    public static string baseUrl = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority + Request.ApplicationPath.TrimEnd('/') + "/";
        if (Session["div_code"] != null)
        {
            divcode = Session["div_code"].ToString(); chk = 1;
            if (divcode.TrimEnd(',') == "107" || divcode.TrimEnd(',') == "128" || divcode.TrimEnd(',') == "4")
            {
                chk = 1;
            }

            sfname = Request.QueryString["sfname"].ToString();
            sfCode = Request.QueryString["sfcode"].ToString();
            dist_code = sfCode.Trim();

            gg = Request.QueryString["DATE"].ToString();
            Dist_Code = "";
            try
            {
                Dist_Code = Request.QueryString["DistCode"].ToString();
            }
            catch { }
            date = gg.Trim();

            DateTime dt = Convert.ToDateTime(date);
            string hdate = dt.ToString("dd-MM-yyyy");
            System.Globalization.DateTimeFormatInfo mfi = new System.Globalization.DateTimeFormatInfo();

            lblHead.Text = "Sales Man Call Tracking(Daily)  for   " + hdate;

            distname.Text = sfname;
            norecordfound.Visible = false;

            lblIDMonth.Visible = false;
            lblIDYear.Visible = false;
            FillSF();
        }
        else { Page.Response.Redirect(baseUrl, true); }

    }

    private void FillSF()
    {
        if (divcode == "86")
        {
            Random rndm = new Random();
            int t = rndm.Next(1, 28);
            locl sf1 = new locl();
            tbl.Rows.Clear();
            Product pd = new Product();
            SalesForce sf = new SalesForce();
            dsSalesForce = sf1.salesmandaily_call_report(divcode, sfCode, date, Dist_Code);
            ss = sf1.getqtydailycall(divcode, sfCode, date);

            cc = pd.getdailycallproduct(sfCode, date);
            totalqty = new Int64[cc.Tables[0].Rows.Count];
            if (dsSalesForce.Tables[0].Rows.Count > 0)
            {
                TableRow tr_header = new TableRow();
                tr_header.BorderStyle = BorderStyle.Solid;
                tr_header.BorderWidth = 1;
                tr_header.BackColor = System.Drawing.Color.FromName("#DDEECC");
                tr_header.Style.Add("border-color", "#DDEECC !important");
                tr_header.Style.Add("Color", "Black");
                tr_header.BorderColor = System.Drawing.Color.Black;

                TableCell tc_SNo = new TableCell();
                tc_SNo.BorderStyle = BorderStyle.Solid;
                tc_SNo.BorderWidth = 1;
                tc_SNo.Width = 100;
                tc_SNo.RowSpan = 2;
                tc_SNo.HorizontalAlign = HorizontalAlign.Center;
                Literal lit_SNo =
                    new Literal();
                lit_SNo.Text = "S.No";
                tc_SNo.BackColor = System.Drawing.Color.FromName("#DDEECC");
                tc_SNo.Style.Add("Color", "#000080");
                tc_SNo.BorderColor = System.Drawing.Color.Black;
                tc_SNo.Controls.Add(lit_SNo);
                tc_SNo.Attributes.Add("Class", "rpthCellBorder");
                tr_header.Cells.Add(tc_SNo);

                TableCell tc_DR_Name = new TableCell();
                tc_DR_Name.BorderStyle = BorderStyle.Solid;
                tc_DR_Name.BorderWidth = 1;
                tc_DR_Name.Width = 250;
                tc_DR_Name.RowSpan = 2;
                tc_DR_Name.HorizontalAlign = HorizontalAlign.Center;
                Literal lit_DR_Name = new Literal();
                lit_DR_Name.Text = "Outlet Visited";
                tc_DR_Name.BorderColor = System.Drawing.Color.Black;
                tc_DR_Name.Attributes.Add("Class", "rpthCellBorder");
                tc_DR_Name.Controls.Add(lit_DR_Name);
                tr_header.Cells.Add(tc_DR_Name);

                TableCell tc_lat = new TableCell();
                tc_lat.BorderStyle = BorderStyle.Solid;
                tc_lat.BorderWidth = 1;
                tc_lat.Width = 250;
                tc_lat.RowSpan = 2;
                tc_lat.CssClass = "geoaddr";
                //tc_lat.Visible = false;
                tc_lat.HorizontalAlign = HorizontalAlign.Center;
                Literal litlat = new Literal();
                litlat.Text = "Lat";
                tc_lat.BorderColor = System.Drawing.Color.Black;
                tc_lat.Attributes.Add("Class", "rpthCellBorder");
                tc_lat.Controls.Add(litlat);
                tr_header.Cells.Add(tc_lat);

                TableCell tc_long = new TableCell();
                tc_long.BorderStyle = BorderStyle.Solid;
                tc_long.BorderWidth = 1;
                tc_long.Width = 250;
                tc_long.RowSpan = 2;
                tc_long.CssClass = "geoaddr";
                //tc_long.Visible = false;
                tc_long.HorizontalAlign = HorizontalAlign.Center;
                Literal latlong = new Literal();
                latlong.Text = "Long";
                tc_long.BorderColor = System.Drawing.Color.Black;
                tc_long.Attributes.Add("Class", "rpthCellBorder");
                tc_long.Controls.Add(latlong);
                tr_header.Cells.Add(tc_long);


                TableCell tc_addr = new TableCell();
                tc_addr.BorderStyle = BorderStyle.Solid;
                tc_addr.BorderWidth = 1;
                tc_addr.Width = 750;
                tc_addr.RowSpan = 2;
                tc_addr.HorizontalAlign = HorizontalAlign.Center;
                Literal lit_addr = new Literal();
                lit_addr.Text = "Address";
                tc_addr.BorderColor = System.Drawing.Color.Black;
                tc_addr.Attributes.Add("Class", "rpthCellBorder");
                tc_addr.Controls.Add(lit_addr);
                tr_header.Cells.Add(tc_addr);

                TableCell tc_DR_Name_pot = new TableCell();
                tc_DR_Name_pot.BorderStyle = BorderStyle.Solid;
                tc_DR_Name_pot.BorderWidth = 1;
                tc_DR_Name_pot.Width = 250;
                tc_DR_Name_pot.RowSpan = 2;
                tc_DR_Name_pot.HorizontalAlign = HorizontalAlign.Center;
                Literal lit_DR_Name_pot = new Literal();
                lit_DR_Name_pot.Text = "Route";
                tc_DR_Name_pot.BorderColor = System.Drawing.Color.Black;
                tc_DR_Name_pot.Attributes.Add("Class", "rpthCellBorder");
                tc_DR_Name_pot.Controls.Add(lit_DR_Name_pot);
                tr_header.Cells.Add(tc_DR_Name_pot);

                TableCell tc_DR_Name_product_name = new TableCell();
                tc_DR_Name_product_name.BorderStyle = BorderStyle.Solid;
                tc_DR_Name_product_name.BorderWidth = 1;
                tc_DR_Name_product_name.RowSpan = 2;
                tc_DR_Name_product_name.HorizontalAlign = HorizontalAlign.Center;
                tc_DR_Name_product_name.Width = 250;
                Literal lit_DR_Name_pot_name = new Literal();
                lit_DR_Name_pot_name.Text = "Channel";
                tc_DR_Name_product_name.BorderColor = System.Drawing.Color.Black;
                tc_DR_Name_product_name.Attributes.Add("Class", "rpthCellBorder");
                tc_DR_Name_product_name.Controls.Add(lit_DR_Name_pot_name);
                tr_header.Cells.Add(tc_DR_Name_product_name);

                TableCell tc_DR_Name_product_named = new TableCell();
                tc_DR_Name_product_named.BorderStyle = BorderStyle.Solid;
                tc_DR_Name_product_named.BorderWidth = 1;
                tc_DR_Name_product_named.RowSpan = 2;
                tc_DR_Name_product_named.Width = 250;
                tc_DR_Name_product_named.HorizontalAlign = HorizontalAlign.Center;
                Literal lit_DR_Name_pot_named = new Literal();
                lit_DR_Name_pot_named.Text = "Stockist";
                tc_DR_Name_product_named.BorderColor = System.Drawing.Color.Black;
                tc_DR_Name_product_named.Attributes.Add("Class", "rpthCellBorder");
                tc_DR_Name_product_named.Controls.Add(lit_DR_Name_pot_named);
                tr_header.Cells.Add(tc_DR_Name_product_named);

                TableCell tc_DR_Name_pott = new TableCell();
                tc_DR_Name_pott.BorderStyle = BorderStyle.Solid;
                tc_DR_Name_pott.BorderWidth = 1;
                tc_DR_Name_pott.Width = 250;
                tc_DR_Name_pott.RowSpan = 1;
                tc_DR_Name_pott.ColumnSpan = 2;
                tc_DR_Name_pott.HorizontalAlign = HorizontalAlign.Center;
                Literal lit_DR_Name_pott = new Literal();
                lit_DR_Name_pott.Text = "Time";
                tc_DR_Name_pott.BorderColor = System.Drawing.Color.Black;
                tc_DR_Name_pott.Attributes.Add("Class", "rpthCellBorder");
                tc_DR_Name_pott.Controls.Add(lit_DR_Name_pott);
                tr_header.Cells.Add(tc_DR_Name_pott);

                TableRow tr_catg = new TableRow();
                tr_catg.BorderStyle = BorderStyle.Solid;
                tr_catg.BorderWidth = 1;
                tr_catg.BackColor = System.Drawing.Color.FromName("#496a9a");
                tr_catg.Style.Add("Color", "White");
                tr_catg.BorderColor = System.Drawing.Color.Black;

                TableCell tc_catg_namee = new TableCell();
                tc_catg_namee.BorderStyle = BorderStyle.Solid;
                tc_catg_namee.BorderWidth = 1;

                Literal lit_catg_namee = new Literal();
                lit_catg_namee.Text = "From";

                tc_catg_namee.Width = 130;
                tc_catg_namee.Attributes.Add("Class", "rpthCellBorder");
                tc_catg_namee.HorizontalAlign = HorizontalAlign.Center;
                tc_catg_namee.Controls.Add(lit_catg_namee);
                tr_catg.Cells.Add(tc_catg_namee);
                TableCell totime = new TableCell();
                totime.BorderStyle = BorderStyle.Solid;
                totime.BorderWidth = 1;
                //text-align: center;

                Literal totimelit = new Literal();
                totimelit.Text = "To";
                tc_catg_namee.Width = 120;

                totime.Attributes.Add("Class", "rpthCellBorder");
                totime.HorizontalAlign = HorizontalAlign.Center;
                totime.Controls.Add(totimelit);
                tr_catg.Cells.Add(totime);

                foreach (DataRow drdoctor in cc.Tables[0].Rows)
                {
                    TableCell tc_pname = new TableCell();
                    tc_pname.BorderStyle = BorderStyle.Solid;
                    tc_pname.BorderWidth = 1;
                    tc_pname.Width = 200;
                    tc_pname.RowSpan = 2;
                    tc_pname.HorizontalAlign = HorizontalAlign.Center;
                    Literal lit_pname = new Literal();
                    lit_pname.Text = drdoctor["Product_Detail_Name"].ToString();
                    tc_pname.BorderColor = System.Drawing.Color.Black;
                    tc_pname.Attributes.Add("Class", "rptCellBorder");
                    tc_pname.Controls.Add(lit_pname);
                    tr_header.Cells.Add(tc_pname);
                }

                TableCell tc_units = new TableCell();
                tc_units.BorderStyle = BorderStyle.Solid;
                tc_units.BorderWidth = 1;
                tc_units.Width = 200;
                tc_units.RowSpan = 2;
                tc_units.HorizontalAlign = HorizontalAlign.Center;
                Literal lit_units = new Literal();
                lit_units.Text = "Units";
                tc_units.BorderColor = System.Drawing.Color.Black;
                tc_units.Attributes.Add("Class", "rpthCellBorder");
                tc_units.Controls.Add(lit_units);
                tr_header.Cells.Add(tc_units);

                TableCell tc_DR_Namee = new TableCell();
                tc_DR_Namee.BorderStyle = BorderStyle.Solid;
                tc_DR_Namee.BorderWidth = 1;
                tc_DR_Namee.Width = 200;
                tc_DR_Namee.RowSpan = 2;
                tc_DR_Namee.HorizontalAlign = HorizontalAlign.Center;
                Literal lit_DR_Namee = new Literal();
                lit_DR_Namee.Text = "Order Value";
                tc_DR_Namee.BorderColor = System.Drawing.Color.Black;
                tc_DR_Namee.Attributes.Add("Class", "rpthCellBorder");
                tc_DR_Namee.Controls.Add(lit_DR_Namee);
                tr_header.Cells.Add(tc_DR_Namee);

                TableCell tc_DR_Nameet = new TableCell();
                tc_DR_Nameet.BorderStyle = BorderStyle.Solid;
                tc_DR_Nameet.BorderWidth = 1;
                tc_DR_Nameet.Width = 200;
                tc_DR_Nameet.RowSpan = 2;
                tc_DR_Nameet.HorizontalAlign = HorizontalAlign.Center;
                Literal lit_DR_Nameet = new Literal();
                lit_DR_Nameet.Text = "Net Weight";
                tc_DR_Nameet.BorderColor = System.Drawing.Color.Black;
                tc_DR_Nameet.Attributes.Add("Class", "rpthCellBorder");
                tc_DR_Nameet.Controls.Add(lit_DR_Nameet);
                tr_header.Cells.Add(tc_DR_Nameet);

                TableCell activity = new TableCell();
                activity.BorderStyle = BorderStyle.Solid;
                activity.BorderWidth = 1;
                activity.Width = 250;
                activity.RowSpan = 2;
                activity.HorizontalAlign = HorizontalAlign.Center;
                Literal activitylit = new Literal();
                activitylit.Text = "Activity";
                activity.BorderColor = System.Drawing.Color.Black;
                activity.Attributes.Add("Class", "rpthCellBorder");
                activity.Controls.Add(activitylit);
                tr_header.Cells.Add(activity);
                TableCell elapsedtime = new TableCell();
                elapsedtime.BorderStyle = BorderStyle.Solid;
                elapsedtime.BorderWidth = 1;
                elapsedtime.Width = 250;
                elapsedtime.RowSpan = 2;
                elapsedtime.HorizontalAlign = HorizontalAlign.Center;
                Literal elapsedtimelit = new Literal();
                elapsedtimelit.Text = "Elapsedtime";
                elapsedtime.BorderColor = System.Drawing.Color.Black;
                elapsedtime.Attributes.Add("Class", "rpthCellBorder");
                elapsedtime.Controls.Add(elapsedtimelit);
                tr_header.Cells.Add(elapsedtime);

                TableCell rehead = new TableCell();
                rehead.BorderStyle = BorderStyle.Solid;
                rehead.BorderWidth = 1;
                rehead.Width = 250;
                rehead.RowSpan = 2;
                rehead.HorizontalAlign = HorizontalAlign.Center;
                Literal reheadlit = new Literal();
                reheadlit.Text = "Remarks";
                rehead.BorderColor = System.Drawing.Color.Black;
                rehead.Attributes.Add("Class", "rpthCellBorder");
                rehead.Controls.Add(reheadlit);
                tr_header.Cells.Add(rehead);
                tbl.Rows.Add(tr_header);

                tbl.Rows.Add(tr_header);
                tbl.Rows.Add(tr_catg);

                if (dsSalesForce.Tables[0].Rows.Count > 0)
                    ViewState["dsSalesForce"] = dsSalesForce;


                int iCount = 0;
                //string iTotLstCount ="0";

                dsDoc = sf.salesmandaily_call_report_time(divcode, sfCode, date, Dist_Code);

                dsDoc1 = sf.salesmandaily_call_report_trans_order_WITHOUT_CUSTCODE(divcode, sfCode, date, Dist_Code);
                int counts = 0; int totaldays = 0;
                foreach (DataRow drFF in dsSalesForce.Tables[0].Rows)
                {
                    TableRow tr_det = new TableRow();
                    iCount += 1;


                    //S.No
                    TableCell tc_det_SNo = new TableCell();
                    Literal lit_det_SNo = new Literal();
                    lit_det_SNo.Text = iCount.ToString();
                    tc_det_SNo.BorderStyle = BorderStyle.Solid;
                    tc_det_SNo.BorderWidth = 1;
                    tc_det_SNo.HorizontalAlign = HorizontalAlign.Right;
                    tc_det_SNo.Attributes.Add("Class", "rptCellBorder");
                    tc_det_SNo.Controls.Add(lit_det_SNo);
                    tr_det.Cells.Add(tc_det_SNo);
                    tr_det.BackColor = System.Drawing.Color.White;

                    //SF_code

                    TableCell tc_det_usr = new TableCell();
                    //   tc_det_usr.Attributes.Add("style", "color:Blue;");
                    Literal retailname = new Literal();
                    if (drFF["ListedDr_Created_Date"].ToString() == gg.ToString())
                    {
                        retailname.Text = "&nbsp;" + drFF["Trans_Detail_Name"].ToString() + "<sup style='color:red;background: yellow;padding: 0px 7px;'>New</sup>";
                        tc_det_usr.BorderStyle = BorderStyle.Solid;
                        tc_det_usr.BorderWidth = 1;
                        //tc_det_usr.BackColor = Color.Yellow;
                        tc_det_usr.Attributes.Add("Class", "rptCellBorder");
                        tc_det_usr.Controls.Add(retailname);
                        tr_det.Cells.Add(tc_det_usr);
                        counts++;
                    }
                    else
                    {
                        retailname.Text = "&nbsp;" + drFF["Trans_Detail_Name"].ToString();
                        tc_det_usr.BorderStyle = BorderStyle.Solid;
                        tc_det_usr.BorderWidth = 1;
                        tc_det_usr.Attributes.Add("Class", "rptCellBorder");
                        tc_det_usr.Controls.Add(retailname);
                        tr_det.Cells.Add(tc_det_usr);

                    }
                    //retailname.Text = "&nbsp;" + drFF["Trans_Detail_Name"].ToString();
                    //tc_det_usr.BorderStyle = BorderStyle.Solid;
                    //tc_det_usr.BorderWidth = 1;

                    //tc_det_usr.Attributes.Add("Class", "rptCellBorder");
                    //tc_det_usr.Controls.Add(retailname);
                    //tr_det.Cells.Add(tc_det_usr);

                    //SF Name

                    TableCell tclat = new TableCell();
                    tclat.BorderStyle = BorderStyle.Solid;
                    tclat.BorderWidth = 1;
                    tclat.Width = 250;
                    tclat.CssClass = "lat";
                    tclat.CssClass = "geoaddr";
                    // tclat.Visible = false;
                    tclat.HorizontalAlign = HorizontalAlign.Center;
                    Literal lit_lat = new Literal();
                    lit_lat.Text = drFF["lat"].ToString();
                    tclat.Attributes.Add("Class", "rptCellBorder");
                    tclat.Controls.Add(lit_lat);
                    tr_det.Cells.Add(tclat);

                    TableCell tclong = new TableCell();
                    tclong.BorderStyle = BorderStyle.Solid;
                    tclong.BorderWidth = 1;
                    tclong.Width = 250;
                    tclong.RowSpan = 1;
                    tclong.CssClass = "long";
                    tclong.CssClass = "geoaddr";
                    //tclong.Visible = false;
                    tclong.HorizontalAlign = HorizontalAlign.Center;
                    Literal lit_long = new Literal();
                    lit_long.Text = drFF["long"].ToString();
                    tclong.Attributes.Add("Class", "rptCellBorder");
                    tclong.Controls.Add(lit_long);
                    tr_det.Cells.Add(tclong);

                    TableCell tcAddress = new TableCell();
                    tcAddress.BorderStyle = BorderStyle.Solid;
                    tcAddress.BorderWidth = 1;
                    tcAddress.Width = 250;
                    tcAddress.RowSpan = 1;
                    tcAddress.CssClass = "Addr";
                    tcAddress.HorizontalAlign = HorizontalAlign.Center;
                    Literal lit_addrs = new Literal();
                    lit_addrs.Text = drFF["ListedDr_Address1"].ToString();
                    tcAddress.Attributes.Add("Class", "rptCellBorder");
                    tcAddress.Controls.Add(lit_addrs);
                    tr_det.Cells.Add(tcAddress);

                    TableCell tc_det_FF = new TableCell();

                    tc_det_FF.HorizontalAlign = HorizontalAlign.Left;
                    tc_det_FF.Width = 300;
                    Literal address = new Literal();
                    address.Text = "&nbsp;" + drFF["SDP_Name"].ToString();
                    tc_det_FF.BorderStyle = BorderStyle.Solid;
                    tc_det_FF.BorderWidth = 1;
                    tc_det_FF.Attributes.Add("Class", "rptCellBorder");
                    tc_det_FF.Controls.Add(address);
                    tr_det.Cells.Add(tc_det_FF);


                    TableCell tc_det_FF_milk = new TableCell();
                    //  tc_det_FF_milk.Attributes.Add("style", "color:Blue;");
                    tc_det_FF_milk.Width = 300;
                    Literal lit_det_FF_milk = new Literal();
                    lit_det_FF_milk.Text = "&nbsp;" + drFF["Doc_Spec_ShortName"].ToString();
                    tc_det_FF_milk.BorderStyle = BorderStyle.Solid;
                    tc_det_FF_milk.BorderWidth = 1;
                    tc_det_FF_milk.Attributes.Add("Class", "rptCellBorder");
                    tc_det_FF_milk.Controls.Add(lit_det_FF_milk);
                    tr_det.Cells.Add(tc_det_FF_milk);

                    TableCell tc_det_FF_milkst = new TableCell();
                    //tc_det_FF_milkst.Attributes.Add("style", "color:Blue;");
                    tc_det_FF_milkst.Width = 300;
                    Literal lit_det_FF_milkst = new Literal();
                    lit_det_FF_milkst.Text = "&nbsp;" + drFF["stockist_name"].ToString();
                    tc_det_FF_milkst.BorderStyle = BorderStyle.Solid;
                    tc_det_FF_milkst.BorderWidth = 1;
                    tc_det_FF_milkst.Attributes.Add("Class", "rptCellBorder");
                    tc_det_FF_milkst.Controls.Add(lit_det_FF_milkst);
                    tr_det.Cells.Add(tc_det_FF_milkst);

                    TableCell tc_det_last6monthsum = new TableCell();
                    tc_det_last6monthsum.Width = 200;

                    tc_det_last6monthsum.HorizontalAlign = HorizontalAlign.Left;
                    Literal lit_det_sum = new Literal();
                    lit_det_sum.Text = drFF["tm"].ToString();
                    startTime = lit_det_sum.Text;
                    tc_det_last6monthsum.BorderStyle = BorderStyle.Solid;
                    tc_det_last6monthsum.BorderWidth = 1;
                    tc_det_last6monthsum.Attributes.Add("Class", "rptCellBorder");
                    tc_det_last6monthsum.Controls.Add(lit_det_sum);
                    tr_det.Cells.Add(tc_det_last6monthsum);

                    TableCell tc_det_contri = new TableCell();
                    tc_det_contri.Width = 200;
                    tc_det_contri.HorizontalAlign = HorizontalAlign.Left;
                    Literal lit_det_contri = new Literal();
                    tc_det_contri.BorderStyle = BorderStyle.Solid;
                    tc_det_contri.BorderWidth = 1;
                    tc_det_contri.Attributes.Add("Class", "rptCellBorder");
                    tc_det_contri.Controls.Add(lit_det_contri);
                    tr_det.Cells.Add(tc_det_contri);

                    int jk = 0;
                    //   dsDoc = sf.salesmandaily_call_report_time(divcode, sfCode, date);
                    foreach (DataRow drdoctor in cc.Tables[0].Rows)
                    {
                        //DataRow[] drp = dsSalesForce.Tables[0].Select("PCode='" + drdoctor["Product_Detail_Code"].ToString() + "'");
                        DataRow[] drp = ss.Tables[0].Select("PCode='" + drdoctor["Product_Detail_Code"].ToString() + "' and DCR_Code='" + drFF["Trans_Detail_Slno"] + "'");
                        int c = jk;
                        TableCell tc_qty = new TableCell();
                        tc_qty.Width = 200;
                        tc_qty.HorizontalAlign = HorizontalAlign.Left;
                        Literal lit_qty = new Literal();
                        lit_qty.Text = (drp.Length > 0) ? drp[0]["Qty"].ToString() : "";
                        if (drp.Length > 0)
                        {
                            totalqty[jk] += Convert.ToInt64(drp[0]["Qty"]);
                            unit += Convert.ToInt64(drp[0]["Qty"]);
                            unittotal += Convert.ToInt64(drp[0]["Qty"]);

                        }
                        tc_qty.BorderStyle = BorderStyle.Solid;
                        tc_qty.BorderWidth = 1;
                        tc_qty.Attributes.Add("Class", "rptCellBorder");
                        tc_qty.Controls.Add(lit_qty);
                        tr_det.Cells.Add(tc_qty);
                        jk++;
                    }

                    TableCell tc_unit = new TableCell();
                    tc_unit.Width = 200;
                    tc_unit.HorizontalAlign = HorizontalAlign.Left;
                    Literal lit_unit = new Literal();
                    lit_unit.Text = unit.ToString();
                    tc_unit.BorderStyle = BorderStyle.Solid;
                    tc_unit.BorderWidth = 1;
                    tc_unit.Attributes.Add("Class", "rptCellBorder");
                    tc_unit.Controls.Add(lit_unit);
                    tr_det.Cells.Add(tc_unit);
                    unit = 0;

                    if (iCount == dsDoc.Tables[0].Rows.Count)
                    {
                        lit_det_contri.Text = "";
                        tot_value = dsDoc.Tables[0].Rows[iCount - 1][0].ToString();
                        endd = dsDoc.Tables[0].Rows[iCount - 1][0].ToString();
                        endTime = tot_value;
                    }
                    else
                    {
                        if (dsDoc.Tables[0].Rows.Count > 0)
                            tot_value = dsDoc.Tables[0].Rows[iCount][0].ToString();
                        endTime = tot_value;
                        lit_det_contri.Text = tot_value.ToString();
                    }

                    tbl.Rows.Add(tr_det);

                    //dsDoc = sf.salesmandaily_call_report_trans_order(divcode, sfCode, date, drFF["Trans_Detail_Info_Code"].ToString());

                    string str = drFF["Trans_Detail_Info_Code"].ToString();

                    DataTable dt = new DataTable();
                    DataRow[] dr = dsDoc1.Tables[0].Select("Cust_Code='" + str + "'");
                    if (dr.Length > 0)
                        dt = dr.CopyToDataTable<DataRow>();


                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow drFFg in dt.Rows)
                        {
                            TableCell tc_det_currentmonthy = new TableCell();
                            tc_det_currentmonthy.Width = 200;
                            tc_det_currentmonthy.HorizontalAlign = HorizontalAlign.Right;
                            HyperLink lit_det_mony = new HyperLink();

                            lit_det_mony.Text = drFFg["Order_Value"] == DBNull.Value ? drFFg["Order_Value"].ToString() : Convert.ToDecimal(drFFg["Order_Value"]).ToString("0.00");

                            net = drFFg["Order_Value"].ToString();
                            if (net != "")
                            {
                                val = decimal.Parse(net);
                                netwgttotal += val;
                            }
                            tc_det_currentmonthy.BorderStyle = BorderStyle.Solid;
                            tc_det_currentmonthy.BorderWidth = 1;
                            tc_det_currentmonthy.Attributes.Add("Class", "rptCellBorder");
                            tc_det_currentmonthy.Controls.Add(lit_det_mony);
                            tr_det.Cells.Add(tc_det_currentmonthy);

                            TableCell tc_det_currentmonthr = new TableCell();
                            tc_det_currentmonthr.Width = 200;
                            tc_det_currentmonthr.HorizontalAlign = HorizontalAlign.Right;
                            HyperLink lit_det_monr = new HyperLink();
                            lit_det_monr.Text = drFFg["net_weight_value"] == DBNull.Value ? drFFg["net_weight_value"].ToString() : Convert.ToDecimal(drFFg["net_weight_value"]).ToString("0.00");
                            string netw = drFFg["net_weight_value"].ToString();
                            if (netw != "")
                            {

                                val1 = decimal.Parse(netw);
                                valuetotal += val1;
                            }
                            tc_det_currentmonthr.BorderStyle = BorderStyle.Solid;
                            tc_det_currentmonthr.BorderWidth = 1;
                            tc_det_currentmonthr.Attributes.Add("Class", "rptCellBorder");
                            tc_det_currentmonthr.Controls.Add(lit_det_monr);
                            tr_det.Cells.Add(tc_det_currentmonthr);
                        }
                    }
                    else
                    {
                        TableCell tc_det_currentmonthy = new TableCell();
                        tc_det_currentmonthy.Width = 200;
                        tc_det_currentmonthy.HorizontalAlign = HorizontalAlign.Right;
                        HyperLink lit_det_mony = new HyperLink();
                        lit_det_mony.Text = "0.00";

                        tc_det_currentmonthy.BorderStyle = BorderStyle.Solid;
                        tc_det_currentmonthy.BorderWidth = 1;
                        tc_det_currentmonthy.Attributes.Add("Class", "rptCellBorder");
                        tc_det_currentmonthy.Controls.Add(lit_det_mony);
                        tr_det.Cells.Add(tc_det_currentmonthy);

                        TableCell tc_det_currentmonthr = new TableCell();
                        tc_det_currentmonthr.Width = 200;
                        tc_det_currentmonthr.HorizontalAlign = HorizontalAlign.Right;
                        HyperLink lit_det_monr = new HyperLink();
                        lit_det_monr.Text = "0.00";
                        tc_det_currentmonthr.BorderStyle = BorderStyle.Solid;
                        tc_det_currentmonthr.BorderWidth = 1;
                        tc_det_currentmonthr.Attributes.Add("Class", "rptCellBorder");
                        tc_det_currentmonthr.Controls.Add(lit_det_monr);
                        tr_det.Cells.Add(tc_det_currentmonthr);
                    }

                    TableCell tc_det_currentmonth = new TableCell();
                    tc_det_currentmonth.Width = 200;
                    tc_det_currentmonth.HorizontalAlign = HorizontalAlign.Left;
                    HyperLink lit_det_mon = new HyperLink();
                    lit_det_mon.Text = drFF["activity"].ToString();

                    tc_det_currentmonth.BorderStyle = BorderStyle.Solid;
                    tc_det_currentmonth.BorderWidth = 1;
                    tc_det_currentmonth.Attributes.Add("Class", "rptCellBorder");
                    tc_det_currentmonth.Controls.Add(lit_det_mon);
                    tr_det.Cells.Add(tc_det_currentmonth);
                    if (lit_det_mon.Text == "Productive")
                    {
                        productive_count += 1;
                    }

                    TimeSpan duration = DateTime.Parse(endTime).Subtract(DateTime.Parse(startTime));

                    ff += duration.Duration();

                    TableCell eltime = new TableCell();
                    eltime.Width = 200;
                    eltime.HorizontalAlign = HorizontalAlign.Left;
                    HyperLink eltimelit = new HyperLink();

                    eltimelit.Text = duration.ToString();
                    eltime.BorderStyle = BorderStyle.Solid;
                    eltime.BorderWidth = 1;
                    eltime.Attributes.Add("Class", "rptCellBorder");
                    eltime.Controls.Add(eltimelit);
                    tr_det.Cells.Add(eltime);

                    TableCell remark = new TableCell();
                    remark.Width = 200;
                    remark.HorizontalAlign = HorizontalAlign.Left;
                    Literal remarklit = new Literal();

                    remarklit.Text = drFF["Activity_Remarks"].ToString();
                    remark.BorderStyle = BorderStyle.Solid;
                    remark.BorderWidth = 1;
                    remark.Attributes.Add("Class", "rptCellBorder");
                    remark.Controls.Add(remarklit);
                    tr_det.Cells.Add(remark);

                    if (Dist_Code == "")
                    {
                        callcount.Text = dsSalesForce.Tables[0].Rows.Count.ToString();
                        closingtime.Text = endd.ToString();
                        tot_hours.Text = ff.ToString();
                        productive.Text = productive_count.ToString();
                        Tot_new_ret.Text = counts.ToString();
                        dsdocto = sf.salesmandaily_Retailer_tot(divcode, sfCode, date);
                        //Total_new_rt.Text = dsdocto.Tables[0].Rows[0][0].ToString();

                        if (Convert.ToDecimal(productive.Text) > 0)
                            drop_size.Text = (Convert.ToDecimal(callcount.Text) / Convert.ToDecimal(productive.Text)).ToString("0.00");
                        else
                            drop_size.Text = "0.00";
                        //if (tot_hours.Text != string.Empty)
                        //{
                        //    var dat = Convert.ToDateTime(tot_hours.Text);

                        //    int hr = dat.Hour;
                        //    int mi = dat.Minute;
                        //    string str_hr = hr.ToString() + "." + mi.ToString();

                        //    if (Convert.ToDecimal(str_hr) > 0)
                        //        call_average.Text = (Convert.ToDecimal(callcount.Text) / Convert.ToDecimal(str_hr)).ToString("0.00");
                        //    else
                        //        call_average.Text = "0";
                        //    //call_average.Text = (Convert.ToDecimal(callcount.Text) / Convert.ToDecimal(str_hr)).ToString("0.00");
                        //}

                        DB_EReporting dbe_ER = new DB_EReporting();

                        DataSet tc = dbe_ER.Exec_DataSet("exec tcandwd_count '" + sfCode + "','" + divcode + "'");
                        call_average.Text = tc.Tables[0].Rows[0].ItemArray[0].ToString();
                        //Label5.Visible = false;

                    }
                    else
                    {
                        Label4.Visible = false;
                        Label7.Visible = false;
                        Label5.Visible = false;
                        Label6.Visible = false;
                        Label3.Visible = false;
                        Label9.Visible = false;
                        Label8.Visible = false;
                        Label2.Visible = false;

                    }
                }

                TableRow tr_total = new TableRow();

                TableCell tc_Count_Total = new TableCell();
                tc_Count_Total.BorderStyle = BorderStyle.Solid;
                tc_Count_Total.BorderWidth = 1;
                //tc_catg_Total.Width = 25;
                Literal lit_Count_Total = new Literal();

                lit_Count_Total.Text = "Total";
                tc_Count_Total.Attributes.Add("style", "color:Red;font-weight:bold;");
                tc_Count_Total.Controls.Add(lit_Count_Total);
                tc_Count_Total.Font.Bold.ToString();
                tc_Count_Total.BackColor = System.Drawing.Color.White;
                tc_Count_Total.Attributes.Add("Class", "tbldetail_main");
                tc_Count_Total.ColumnSpan = 8;
                tc_Count_Total.Style.Add("text-align", "center");
                tc_Count_Total.Style.Add("font-family", "Calibri");
                tc_Count_Total.Attributes.Add("Class", "rptCellBorder");
                tc_Count_Total.Style.Add("font-size", "10pt");

                tr_total.Cells.Add(tc_Count_Total);

                for (int i = 0; i < cc.Tables[0].Rows.Count; i++)
                {
                    TableCell tc_totqty = new TableCell();
                    HyperLink hyp_totqty = new HyperLink();
                    hyp_totqty.Text = totalqty[i].ToString();
                    tc_totqty.BorderStyle = BorderStyle.Solid;
                    tc_totqty.BorderWidth = 1;
                    tc_totqty.BackColor = System.Drawing.Color.White;
                    tc_totqty.Width = 200;
                    tc_totqty.Style.Add("font-family", "Calibri");
                    tc_totqty.Style.Add("font-size", "10pt");
                    tc_totqty.HorizontalAlign = HorizontalAlign.Right;
                    tc_totqty.VerticalAlign = VerticalAlign.Middle;
                    tc_totqty.Controls.Add(hyp_totqty);
                    tc_totqty.Attributes.Add("style", "font-weight:bold;");
                    tc_totqty.Attributes.Add("Class", "rptCellBorder");
                    tr_total.Cells.Add(tc_totqty);
                }


                TableCell tc_totalunits = new TableCell();
                HyperLink hyp_totalunits = new HyperLink();
                hyp_totalunits.Text = unittotal.ToString();
                tc_totalunits.BorderStyle = BorderStyle.Solid;
                tc_totalunits.BorderWidth = 1;
                tc_totalunits.BackColor = System.Drawing.Color.White;
                tc_totalunits.Width = 200;
                tc_totalunits.Style.Add("font-family", "Calibri");
                tc_totalunits.Style.Add("font-size", "10pt");
                tc_totalunits.HorizontalAlign = HorizontalAlign.Right;
                tc_totalunits.VerticalAlign = VerticalAlign.Middle;
                tc_totalunits.Controls.Add(hyp_totalunits);
                tc_totalunits.Attributes.Add("style", "font-weight:bold;");
                tc_totalunits.Attributes.Add("Class", "rptCellBorder");
                tr_total.Cells.Add(tc_totalunits);



                TableCell tc_tot_month = new TableCell();
                HyperLink hyp_month = new HyperLink();
                hyp_month.Text = netwgttotal.ToString();
                tc_tot_month.BorderStyle = BorderStyle.Solid;
                tc_tot_month.BorderWidth = 1;
                tc_tot_month.BackColor = System.Drawing.Color.White;
                tc_tot_month.Width = 200;
                tc_tot_month.Style.Add("font-family", "Calibri");
                tc_tot_month.Style.Add("font-size", "10pt");
                tc_tot_month.HorizontalAlign = HorizontalAlign.Right;
                tc_tot_month.VerticalAlign = VerticalAlign.Middle;
                tc_tot_month.Controls.Add(hyp_month);
                tc_tot_month.Attributes.Add("style", "font-weight:bold;");
                tc_tot_month.Attributes.Add("Class", "rptCellBorder");
                tr_total.Cells.Add(tc_tot_month);
                //   netwgttotal = 0;
                TableCell tc_tot_monthe = new TableCell();
                HyperLink hyp_monthe = new HyperLink();
                hyp_monthe.Text = valuetotal.ToString();
                tc_tot_monthe.BorderStyle = BorderStyle.Solid;
                tc_tot_monthe.BorderWidth = 1;
                tc_tot_monthe.BackColor = System.Drawing.Color.White;
                tc_tot_monthe.Width = 200;
                tc_tot_monthe.Style.Add("font-family", "Calibri");
                tc_tot_monthe.Style.Add("font-size", "10pt");
                tc_tot_monthe.HorizontalAlign = HorizontalAlign.Right;
                tc_tot_monthe.VerticalAlign = VerticalAlign.Middle;
                tc_tot_monthe.Controls.Add(hyp_monthe);
                tc_tot_monthe.Attributes.Add("style", "font-weight:bold;");
                tc_tot_monthe.Attributes.Add("Class", "rptCellBorder");
                tr_total.Cells.Add(tc_tot_monthe);
                //   valuetotal = 0;
                TableCell tc_tot_monthg = new TableCell();
                tc_tot_monthg.ColumnSpan = 3;
                tc_tot_monthg.Attributes.Add("Class", "rptCellBorder");

                tr_total.Cells.Add(tc_tot_monthg);
                tbl.Rows.Add(tr_total);

            }
            else
            {
                norecordfound.Visible = true;
                detail.Visible = false;
                callcount.Visible = false;
                closingtime.Visible = false;
                tot_hours.Visible = false;
                productive.Visible = false;
            }
        }
        else // All Other Clients
        {
            Random rndm = new Random();
            int t = rndm.Next(1, 28);

            tbl.Rows.Clear();
            Product pd = new Product();
            SalesForce sf = new SalesForce();
            dsSalesForce = sf.salesmandaily_call_report(divcode, sfCode, date, Dist_Code);
            cc = pd.getdailycallproduct(sfCode, date);
            totalqty = new Int64[cc.Tables[0].Rows.Count];

            ss = sf.getqtydailycall(divcode, sfCode, date);

            Ids = sf.getdailyFirstcallImg(divcode, sfCode, date);

            if (dsSalesForce.Tables[0].Rows.Count > 0)
            {
                TableRow tr_header = new TableRow();
                tr_header.BorderStyle = BorderStyle.Solid;
                tr_header.BorderWidth = 1;
                tr_header.BackColor = System.Drawing.Color.FromName("#496a9a");
                tr_header.Style.Add("Color", "White");
                tr_header.BorderColor = System.Drawing.Color.Black;

                TableCell tc_SNo = new TableCell();
                tc_SNo.BorderStyle = BorderStyle.Solid;
                tc_SNo.BorderWidth = 1;
                tc_SNo.Width = 100;
                tc_SNo.RowSpan = 2;
                tc_SNo.HorizontalAlign = HorizontalAlign.Center;
                Literal lit_SNo =
                    new Literal();
                lit_SNo.Text = "S.No";
                tc_SNo.Attributes.Add("style", "color:black;font-weight:bold;");
                tc_SNo.Controls.Add(lit_SNo);
                tc_SNo.Font.Bold.ToString();
                //tc_SNo.BackColor = System.Drawing.Color.White;                               
                tc_SNo.Style.Add("text-align", "center");
                tc_SNo.Style.Add("font-family", "Calibri");
                //tc_SNo.Attributes.Add("Class", "rptCellBorder");
                tc_SNo.Style.Add("font-size", "12pt");
                tc_SNo.Style.Add("background-color", "#e8f7ed");

                //tc_SNo.BorderColor = System.Drawing.Color.Black;
                //tc_SNo.Controls.Add(lit_SNo);
                //tc_SNo.Attributes.Add("Class", "rptCellBorder");
                tr_header.Cells.Add(tc_SNo);



                TableCell tc_DR_Name = new TableCell();
                tc_DR_Name.BorderStyle = BorderStyle.Solid;
                tc_DR_Name.BorderWidth = 1;
                tc_DR_Name.Width = 250;
                tc_DR_Name.RowSpan = 2;
                tc_DR_Name.HorizontalAlign = HorizontalAlign.Center;
                Literal lit_DR_Name = new Literal();
                lit_DR_Name.Text = "Outlet Visited";
                tc_DR_Name.Attributes.Add("style", "color:black;font-weight:bold;");
                tc_DR_Name.Controls.Add(lit_DR_Name);
                tc_DR_Name.Font.Bold.ToString();
                //tc_DR_Name.BackColor = System.Drawing.Color.White;                               
                tc_DR_Name.Style.Add("text-align", "center");
                tc_DR_Name.Style.Add("font-family", "Calibri");
                //tc_DR_Name.Attributes.Add("Class", "rptCellBorder");
                tc_DR_Name.Style.Add("font-size", "12pt");
                tc_DR_Name.Style.Add("background-color", "#e8f7ed");
                                
                tr_header.Cells.Add(tc_DR_Name);

                TableCell tc_DR_Name_pot = new TableCell();
                tc_DR_Name_pot.BorderStyle = BorderStyle.Solid;
                tc_DR_Name_pot.BorderWidth = 1;
                tc_DR_Name_pot.Width = 250;
                tc_DR_Name_pot.RowSpan = 2;
                tc_DR_Name_pot.HorizontalAlign = HorizontalAlign.Center;
                Literal lit_DR_Name_pot = new Literal();
                lit_DR_Name_pot.Text = "Route";
                tc_DR_Name_pot.Attributes.Add("style", "color:black;font-weight:bold;");
                tc_DR_Name_pot.Controls.Add(lit_DR_Name_pot);
                tc_DR_Name_pot.Font.Bold.ToString();
                //tc_DR_Name.BackColor = System.Drawing.Color.White;                               
                tc_DR_Name_pot.Style.Add("text-align", "center");
                tc_DR_Name_pot.Style.Add("font-family", "Calibri");
                //tc_DR_Name_pot.Attributes.Add("Class", "rptCellBorder");
                tc_DR_Name_pot.Style.Add("font-size", "12pt");
                tc_DR_Name_pot.Style.Add("background-color", "#e8f7ed");
                
                tr_header.Cells.Add(tc_DR_Name_pot);


                TableCell tc_DR_Name_product_name = new TableCell();
                tc_DR_Name_product_name.BorderStyle = BorderStyle.Solid;
                tc_DR_Name_product_name.BorderWidth = 1;
                tc_DR_Name_product_name.RowSpan = 2;
                tc_DR_Name_product_name.HorizontalAlign = HorizontalAlign.Center;
                tc_DR_Name_product_name.Width = 250;
                Literal lit_DR_Name_pot_name = new Literal();
                lit_DR_Name_pot_name.Text = "Channel";
                tc_DR_Name_product_name.BorderColor = System.Drawing.Color.Black;
                tc_DR_Name_product_name.Attributes.Add("style", "color:black;font-weight:bold;");
                tc_DR_Name_product_name.Controls.Add(lit_DR_Name_pot_name);
                tc_DR_Name_product_name.Font.Bold.ToString();
                //tc_DR_Name_product_name.BackColor = System.Drawing.Color.White;                               
                tc_DR_Name_product_name.Style.Add("text-align", "center");
                tc_DR_Name_product_name.Style.Add("font-family", "Calibri");
                //tc_DR_Name_product_name.Attributes.Add("Class", "rptCellBorder");
                tc_DR_Name_product_name.Style.Add("font-size", "12pt");
                tc_DR_Name_product_name.Style.Add("background-color", "#e8f7ed");    
                
                tr_header.Cells.Add(tc_DR_Name_product_name);



                TableCell tc_DR_Name_product_named = new TableCell();
                tc_DR_Name_product_named.BorderStyle = BorderStyle.Solid;
                tc_DR_Name_product_named.BorderWidth = 1;
                tc_DR_Name_product_named.RowSpan = 2;
                tc_DR_Name_product_named.Width = 250;
                tc_DR_Name_product_named.HorizontalAlign = HorizontalAlign.Center;
                Literal lit_DR_Name_pot_named = new Literal();
                lit_DR_Name_pot_named.Text = "Stockist";
                tc_DR_Name_product_named.BorderColor = System.Drawing.Color.Black;
                tc_DR_Name_product_named.Attributes.Add("style", "color:black;font-weight:bold;");
                tc_DR_Name_product_named.Controls.Add(lit_DR_Name_pot_named);
                tc_DR_Name_product_named.Font.Bold.ToString();
                //tc_DR_Name_product_named.BackColor = System.Drawing.Color.White;                               
                tc_DR_Name_product_named.Style.Add("text-align", "center");
                tc_DR_Name_product_named.Style.Add("font-family", "Calibri");
                //tc_DR_Name_product_named.Attributes.Add("Class", "rptCellBorder");
                tc_DR_Name_product_named.Style.Add("font-size", "12pt");
                tc_DR_Name_product_named.Style.Add("background-color", "#e8f7ed");
                tc_DR_Name_product_named.Attributes.Add("Class", "rpthCellBorder");
                
                tr_header.Cells.Add(tc_DR_Name_product_named);



                TableCell tc_DR_Name_product_names = new TableCell();
                tc_DR_Name_product_names.BorderStyle = BorderStyle.Solid;
                tc_DR_Name_product_names.BorderWidth = 1;
                tc_DR_Name_product_names.RowSpan = 2;
                tc_DR_Name_product_names.Width = 250;
                tc_DR_Name_product_names.HorizontalAlign = HorizontalAlign.Center;
                Literal lit_DR_Name_pot_names = new Literal();
                lit_DR_Name_pot_names.Text = "OrderType";
                tc_DR_Name_product_names.BorderColor = System.Drawing.Color.Black;
                tc_DR_Name_product_names.Attributes.Add("style", "color:black;font-weight:bold;");
                tc_DR_Name_product_names.Controls.Add(lit_DR_Name_pot_names);
                tc_DR_Name_product_names.Font.Bold.ToString();
                //tc_DR_Name_product_names.BackColor = System.Drawing.Color.White;                               
                tc_DR_Name_product_names.Style.Add("text-align", "center");
                tc_DR_Name_product_names.Style.Add("font-family", "Calibri");
                //tc_DR_Name_product_names.Attributes.Add("Class", "rptCellBorder");
                tc_DR_Name_product_names.Style.Add("font-size", "12pt");
                tc_DR_Name_product_names.Style.Add("background-color", "#e8f7ed");
                tc_DR_Name_product_names.Attributes.Add("Class", "rpthCellBorder");

                tr_header.Cells.Add(tc_DR_Name_product_names);


                TableCell tc_DR_Name_pott = new TableCell();
                tc_DR_Name_pott.BorderStyle = BorderStyle.Solid;
                tc_DR_Name_pott.BorderWidth = 1;
                tc_DR_Name_pott.Width = 250;
                tc_DR_Name_pott.RowSpan = 1;
                tc_DR_Name_pott.ColumnSpan = 2;
                tc_DR_Name_pott.HorizontalAlign = HorizontalAlign.Center;
                Literal lit_DR_Name_pott = new Literal();
                lit_DR_Name_pott.Text = "Time";
                tc_DR_Name_pott.BorderColor = System.Drawing.Color.Black;
                tc_DR_Name_pott.Attributes.Add("style", "color:black;font-weight:bold;");
                tc_DR_Name_pott.Controls.Add(lit_DR_Name_pott);
                tc_DR_Name_pott.Font.Bold.ToString();
                //tc_DR_Name_pott.BackColor = System.Drawing.Color.White;                               
                tc_DR_Name_pott.Style.Add("text-align", "center");
                tc_DR_Name_pott.Style.Add("font-family", "Calibri");
                //tc_DR_Name_pott.Attributes.Add("Class", "rptCellBorder");
                tc_DR_Name_pott.Style.Add("font-size", "12pt");
                tc_DR_Name_pott.Style.Add("background-color", "#e8f7ed");
                tc_DR_Name_pott.Attributes.Add("Class", "rpthCellBorder");

                tr_header.Cells.Add(tc_DR_Name_pott);

                TableRow tr_catg = new TableRow();
                tr_catg.BorderStyle = BorderStyle.Solid;
                tr_catg.BorderWidth = 1;
                tr_catg.BackColor = System.Drawing.Color.FromName("#496a9a");
                tr_catg.Style.Add("Color", "White");
                tr_catg.BorderColor = System.Drawing.Color.Black;


                TableCell tc_catg_namee = new TableCell();
                tc_catg_namee.BorderStyle = BorderStyle.Solid;
                tc_catg_namee.BorderWidth = 1;

                Literal lit_catg_namee = new Literal();
                lit_catg_namee.Text = "From";

                tc_catg_namee.Width = 130;
                tc_catg_namee.Attributes.Add("style", "color:black;font-weight:bold;");
                tc_catg_namee.HorizontalAlign = HorizontalAlign.Center;
                tc_catg_namee.Controls.Add(lit_catg_namee);
                tc_catg_namee.Font.Bold.ToString();
                //tc_catg_namee.BackColor = System.Drawing.Color.White;                               
                tc_catg_namee.Style.Add("text-align", "center");
                tc_catg_namee.Style.Add("font-family", "Calibri");
                //tc_catg_namee.Attributes.Add("Class", "rptCellBorder");
                tc_catg_namee.Style.Add("font-size", "12pt");
                tc_catg_namee.Style.Add("background-color", "#e8f7ed");
                tc_catg_namee.Attributes.Add("Class", "rpthCellBorder");

                tr_catg.Cells.Add(tc_catg_namee);
                TableCell totime = new TableCell();
                totime.BorderStyle = BorderStyle.Solid;
                totime.BorderWidth = 1;
                //text-align: center;


                Literal totimelit = new Literal();
                totimelit.Text = "To";
                tc_catg_namee.Width = 130;               
                totime.Attributes.Add("style", "color:black;font-weight:bold;");
                totime.HorizontalAlign = HorizontalAlign.Center;
                totime.Controls.Add(totimelit);
                totime.Font.Bold.ToString();
                //totime.BackColor = System.Drawing.Color.White;                               
                totime.Style.Add("text-align", "center");
                totime.Style.Add("font-family", "Calibri");
                //totime.Attributes.Add("Class", "rptCellBorder");
                totime.Style.Add("font-size", "12pt");
                totime.Style.Add("background-color", "#e8f7ed");
                totime.Attributes.Add("Class", "rpthCellBorder");               
                tr_catg.Cells.Add(totime);

                foreach (DataRow drdoctor in cc.Tables[0].Rows)
                {
                    TableCell tc_pname = new TableCell();
                    tc_pname.BorderStyle = BorderStyle.Solid;
                    tc_pname.BorderWidth = 1;
                    tc_pname.Width = 250;
                    tc_pname.RowSpan = 2;
                    tc_pname.HorizontalAlign = HorizontalAlign.Center;
                    Literal lit_pname = new Literal();
                    lit_pname.Text = drdoctor["Product_Detail_Name"].ToString();
                    tc_pname.BorderColor = System.Drawing.Color.Black;
                    tc_pname.Attributes.Add("style", "color:black;font-weight:bold;");
                    tc_pname.Controls.Add(lit_pname);
                    tc_pname.Font.Bold.ToString();
                    //tc_pname.BackColor = System.Drawing.Color.White;                               
                    tc_pname.Style.Add("text-align", "center");
                    tc_pname.Style.Add("font-family", "Calibri");
                    //tc_pname.Attributes.Add("Class", "rptCellBorder");
                    tc_pname.Style.Add("font-size", "12pt");
                    tc_pname.Style.Add("background-color", "#e8f7ed");
                    tc_pname.Attributes.Add("Class", "rpthCellBorder");                    
                    tr_header.Cells.Add(tc_pname);
                }

                TableCell tc_units = new TableCell();
                tc_units.BorderStyle = BorderStyle.Solid;
                tc_units.BorderWidth = 1;
                tc_units.Width = 200;
                tc_units.RowSpan = 2;
                tc_units.HorizontalAlign = HorizontalAlign.Center;
                Literal lit_units = new Literal();
                lit_units.Text = "Units";
                tc_units.BorderColor = System.Drawing.Color.Black;
                tc_units.Attributes.Add("style", "color:black;font-weight:bold;");
                tc_units.Controls.Add(lit_units);
                tc_units.Font.Bold.ToString();
                //tc_units.BackColor = System.Drawing.Color.White;                               
                tc_units.Style.Add("text-align", "center");
                tc_units.Style.Add("font-family", "Calibri");
                //tc_units.Attributes.Add("Class", "rptCellBorder");
                tc_units.Style.Add("font-size", "12pt");
                tc_units.Style.Add("background-color", "#e8f7ed");
                tc_units.Attributes.Add("Class", "rpthCellBorder");                
                tr_header.Cells.Add(tc_units);


                TableCell tc_DR_Namee = new TableCell();
                tc_DR_Namee.BorderStyle = BorderStyle.Solid;
                tc_DR_Namee.BorderWidth = 1;
                tc_DR_Namee.Width = 200;
                tc_DR_Namee.RowSpan = 2;
                tc_DR_Namee.HorizontalAlign = HorizontalAlign.Center;
                Literal lit_DR_Namee = new Literal();
                lit_DR_Namee.Text = "Order Value";
                tc_DR_Namee.BorderColor = System.Drawing.Color.Black;
                tc_DR_Namee.Attributes.Add("style", "color:black;font-weight:bold;");
                tc_DR_Namee.Controls.Add(lit_DR_Namee);
                tc_DR_Namee.Font.Bold.ToString();
                //tc_DR_Namee.BackColor = System.Drawing.Color.White;                               
                tc_DR_Namee.Style.Add("text-align", "center");
                tc_DR_Namee.Style.Add("font-family", "Calibri");
                //tc_DR_Namee.Attributes.Add("Class", "rptCellBorder");
                tc_DR_Namee.Style.Add("font-size", "12pt");
                tc_DR_Namee.Style.Add("background-color", "#e8f7ed");                
                tr_header.Cells.Add(tc_DR_Namee);

                TableCell tc_DR_Nameet = new TableCell();
                tc_DR_Nameet.BorderStyle = BorderStyle.Solid;
                tc_DR_Nameet.BorderWidth = 1;
                tc_DR_Nameet.Width = 200;
                tc_DR_Nameet.RowSpan = 2;
                tc_DR_Nameet.HorizontalAlign = HorizontalAlign.Center;
                Literal lit_DR_Nameet = new Literal();
                lit_DR_Nameet.Text = "Net Weight";
                tc_DR_Nameet.BorderColor = System.Drawing.Color.Black;
                tc_DR_Nameet.Attributes.Add("style", "color:black;font-weight:bold;");
                tc_DR_Nameet.Controls.Add(lit_DR_Nameet);
                tc_DR_Nameet.Font.Bold.ToString();
                //tc_DR_Nameet.BackColor = System.Drawing.Color.White;                               
                tc_DR_Nameet.Style.Add("text-align", "center");
                tc_DR_Namee.Style.Add("font-family", "Calibri");
                //tc_DR_Nameet.Attributes.Add("Class", "rptCellBorder");
                tc_DR_Nameet.Style.Add("font-size", "12pt");
                tc_DR_Nameet.Style.Add("background-color", "#e8f7ed");      
                tr_header.Cells.Add(tc_DR_Nameet);

                TableCell activity = new TableCell();
                activity.BorderStyle = BorderStyle.Solid;
                activity.BorderWidth = 1;
                activity.Width = 250;
                activity.RowSpan = 2;
                activity.HorizontalAlign = HorizontalAlign.Center;
                Literal activitylit = new Literal();
                activitylit.Text = "Activity";
                activity.BorderColor = System.Drawing.Color.Black;
                activity.Attributes.Add("style", "color:black;font-weight:bold;");
                activity.Controls.Add(activitylit);
                activity.Font.Bold.ToString();
                //activity.BackColor = System.Drawing.Color.White;                               
                activity.Style.Add("text-align", "center");
                activity.Style.Add("font-family", "Calibri");
                //activity.Attributes.Add("Class", "rptCellBorder");
                activity.Style.Add("font-size", "12pt");
                activity.Style.Add("background-color", "#e8f7ed");      
                tr_header.Cells.Add(activity);

                TableCell elapsedtime = new TableCell();
                elapsedtime.BorderStyle = BorderStyle.Solid;
                elapsedtime.BorderWidth = 1;
                elapsedtime.Width = 250;
                elapsedtime.RowSpan = 2;
                elapsedtime.HorizontalAlign = HorizontalAlign.Center;
                Literal elapsedtimelit = new Literal();
                elapsedtimelit.Text = "Elapsed Time";
                elapsedtime.BorderColor = System.Drawing.Color.Black;
                elapsedtime.Attributes.Add("style", "color:black;font-weight:bold;");
                elapsedtime.Controls.Add(elapsedtimelit);
                elapsedtime.Font.Bold.ToString();
                //elapsedtime.BackColor = System.Drawing.Color.White;                               
                elapsedtime.Style.Add("text-align", "center");
                elapsedtime.Style.Add("font-family", "Calibri");
                //elapsedtime.Attributes.Add("Class", "rptCellBorder");
                elapsedtime.Style.Add("font-size", "12pt");
                elapsedtime.Style.Add("background-color", "#e8f7ed");         
                tr_header.Cells.Add(elapsedtime);

                if (divcode == "128")
                {
                    TableCell visitcell = new TableCell();
                    visitcell.BorderStyle = BorderStyle.Solid;
                    visitcell.BorderWidth = 1;
                    visitcell.Width = 250;
                    visitcell.RowSpan = 2;
                    visitcell.HorizontalAlign = HorizontalAlign.Center;
                    Literal visitcelllit = new Literal();
                    visitcelllit.Text = "Purpose of Visit";
                    visitcell.BorderColor = System.Drawing.Color.Black;
                    visitcell.Attributes.Add("style", "color:black;font-weight:bold;");
                    visitcell.Controls.Add(visitcelllit);
                    visitcell.Font.Bold.ToString();
                    //visitcell.BackColor = System.Drawing.Color.White;                               
                    visitcell.Style.Add("text-align", "center");
                    visitcell.Style.Add("font-family", "Calibri");
                    //visitcell.Attributes.Add("Class", "rptCellBorder");
                    visitcell.Style.Add("font-size", "12pt");
                    visitcell.Style.Add("background-color", "#e8f7ed");
                    tr_header.Cells.Add(visitcell);
                }

                TableCell rehead = new TableCell();
                rehead.BorderStyle = BorderStyle.Solid;
                rehead.BorderWidth = 1;
                rehead.Width = 250;
                rehead.RowSpan = 2;
                rehead.HorizontalAlign = HorizontalAlign.Center;
                Literal reheadlit = new Literal();
                reheadlit.Text = "Remarks";
                rehead.BorderColor = System.Drawing.Color.Black;
                rehead.Attributes.Add("style", "color:black;font-weight:bold;");
                rehead.Controls.Add(reheadlit);
                rehead.Font.Bold.ToString();
                //rehead.BackColor = System.Drawing.Color.White;                               
                rehead.Style.Add("text-align", "center");
                rehead.Style.Add("font-family", "Calibri");
                //rehead.Attributes.Add("Class", "rptCellBorder");
                rehead.Style.Add("font-size", "12pt");
                rehead.Style.Add("background-color", "#e8f7ed");
                tr_header.Cells.Add(rehead);


                TableCell imghead = new TableCell();
                imghead.BorderStyle = BorderStyle.Solid;
                imghead.BorderWidth = 1;
                imghead.Width = 150;
                imghead.RowSpan = 2;
                imghead.HorizontalAlign = HorizontalAlign.Center;
                Literal imgheadlit = new Literal();
                imgheadlit.Text = "First Call Image";
                imghead.BorderColor = System.Drawing.Color.Black;
                imghead.Attributes.Add("style", "color:black;font-weight:bold;");
                imghead.Controls.Add(imgheadlit);
                imghead.Font.Bold.ToString();
                //imghead.BackColor = System.Drawing.Color.White;                               
                imghead.Style.Add("text-align", "center");
                imghead.Style.Add("font-family", "Calibri");
                //imghead.Attributes.Add("Class", "rptCellBorder");
                imghead.Style.Add("font-size", "12pt");
                imghead.Style.Add("background-color", "#e8f7ed");                
                tr_header.Cells.Add(imghead);

                //tbl.Rows.Add(tr_header);
                tbl.Rows.Add(tr_header);

                tbl.Rows.Add(tr_catg);


                if (dsSalesForce.Tables[0].Rows.Count > 0)
                    ViewState["dsSalesForce"] = dsSalesForce;



                int iCount = 0;
                //string iTotLstCount ="0";

                dsDoc = sf.salesmandaily_call_report_time(divcode, sfCode, date, Dist_Code);

                dsDoc1 = sf.salesmandaily_call_report_trans_order_WITHOUT_CUSTCODE(divcode, sfCode, date, Dist_Code);
                int counts = 0;
                foreach (DataRow drFF in dsSalesForce.Tables[0].Rows)
                {
                    TableRow tr_det = new TableRow();
                    iCount += 1;


                    //S.No
                    TableCell tc_det_SNo = new TableCell();
                    Literal lit_det_SNo = new Literal();
                    lit_det_SNo.Text = iCount.ToString();
                    tc_det_SNo.BorderStyle = BorderStyle.Solid;
                    tc_det_SNo.BorderWidth = 1;
                    tc_det_SNo.HorizontalAlign = HorizontalAlign.Right;
                    tc_det_SNo.Attributes.Add("Class", "rptCellBorder");
                    tc_det_SNo.Controls.Add(lit_det_SNo);
                    tr_det.Cells.Add(tc_det_SNo);
                    tr_det.BackColor = System.Drawing.Color.White;

                    //SF_code

                    TableCell tc_det_usr = new TableCell();
                    //   tc_det_usr.Attributes.Add("style", "color:Blue;");
                    Literal retailname = new Literal();
                    if (drFF["ListedDr_Created_Date"].ToString() == gg.ToString())
                    {
                        retailname.Text = "&nbsp;" + drFF["Trans_Detail_Name"].ToString() + "<sup style='color:red;background: yellow;padding: 0px 7px;'>New</sup>";
                        tc_det_usr.BorderStyle = BorderStyle.Solid;
                        tc_det_usr.BorderWidth = 1;
                        //tc_det_usr.BackColor = Color.Yellow;
                        tc_det_usr.Attributes.Add("Class", "rptCellBorder");
                        tc_det_usr.Controls.Add(retailname);
                        tr_det.Cells.Add(tc_det_usr);
                        counts++;
                    }
                    else
                    {
                        retailname.Text = "&nbsp;" + drFF["Trans_Detail_Name"].ToString();
                        tc_det_usr.BorderStyle = BorderStyle.Solid;
                        tc_det_usr.BorderWidth = 1;
                        tc_det_usr.Attributes.Add("Class", "rptCellBorder");
                        tc_det_usr.Controls.Add(retailname);
                        tr_det.Cells.Add(tc_det_usr);

                    }
                    //retailname.Text = "&nbsp;" + drFF["Trans_Detail_Name"].ToString();
                    //tc_det_usr.BorderStyle = BorderStyle.Solid;
                    //tc_det_usr.BorderWidth = 1;

                    //tc_det_usr.Attributes.Add("Class", "rptCellBorder");
                    //tc_det_usr.Controls.Add(retailname);
                    //tr_det.Cells.Add(tc_det_usr);

                    //SF Name
                    TableCell tc_det_FF = new TableCell();

                    tc_det_FF.HorizontalAlign = HorizontalAlign.Left;
                    tc_det_FF.Width = 300;
                    Literal address = new Literal();
                    address.Text = "&nbsp;" + drFF["SDP_Name"].ToString();
                    tc_det_FF.BorderStyle = BorderStyle.Solid;
                    tc_det_FF.BorderWidth = 1;
                    tc_det_FF.Attributes.Add("Class", "rptCellBorder");
                    tc_det_FF.Controls.Add(address);
                    tr_det.Cells.Add(tc_det_FF);


                    TableCell tc_det_FF_milk = new TableCell();
                    //  tc_det_FF_milk.Attributes.Add("style", "color:Blue;");
                    tc_det_FF_milk.Width = 300;
                    Literal lit_det_FF_milk = new Literal();
                    lit_det_FF_milk.Text = "&nbsp;" + drFF["Doc_Spec_ShortName"].ToString();
                    tc_det_FF_milk.BorderStyle = BorderStyle.Solid;
                    tc_det_FF_milk.BorderWidth = 1;
                    tc_det_FF_milk.Attributes.Add("Class", "rptCellBorder");
                    tc_det_FF_milk.Controls.Add(lit_det_FF_milk);
                    tr_det.Cells.Add(tc_det_FF_milk);

                    TableCell tc_det_FF_milkst = new TableCell();
                    //tc_det_FF_milkst.Attributes.Add("style", "color:Blue;");
                    tc_det_FF_milkst.Width = 300;
                    Literal lit_det_FF_milkst = new Literal();
                    lit_det_FF_milkst.Text = "&nbsp;" + drFF["stockist_name"].ToString();
                    tc_det_FF_milkst.BorderStyle = BorderStyle.Solid;
                    tc_det_FF_milkst.BorderWidth = 1;
                    tc_det_FF_milkst.Attributes.Add("Class", "rptCellBorder");
                    tc_det_FF_milkst.Controls.Add(lit_det_FF_milkst);
                    tr_det.Cells.Add(tc_det_FF_milkst);

                    TableCell tc_det_FF_milksta = new TableCell();
                    //tc_det_FF_milksta.Attributes.Add("style", "color:Blue;");
                    tc_det_FF_milksta.Width = 300;
                    Literal lit_det_FF_milksta = new Literal();
                    lit_det_FF_milksta.Text = "&nbsp;" + drFF["OrderTyp"].ToString();
                    tc_det_FF_milksta.BorderStyle = BorderStyle.Solid;
                    tc_det_FF_milksta.BorderWidth = 1;
                    tc_det_FF_milksta.Attributes.Add("Class", "rptCellBorder");
                    tc_det_FF_milksta.Controls.Add(lit_det_FF_milksta);
                    tr_det.Cells.Add(tc_det_FF_milksta);



                    TableCell tc_det_last6monthsum = new TableCell();
                    tc_det_last6monthsum.Width = 200;

                    tc_det_last6monthsum.HorizontalAlign = HorizontalAlign.Left;
                    Literal lit_det_sum = new Literal();

                    //lit_det_sum.Text = (chk == 1) ? ((drFF["StartOrder_Time"] == DBNull.Value) ? "" : Convert.ToDateTime(drFF["StartOrder_Time"]).ToString("hh:mm tt")) : drFF["tm"].ToString();

                    //lit_det_sum.Text = (drFF["StartOrder_Time"] == DBNull.Value) ? "" : Convert.ToDateTime(drFF["StartOrder_Time"]).ToString("hh:mm:ss");

                    lit_det_sum.Text = (drFF["StartOrder_Time"] == DBNull.Value) ? "" : Convert.ToString(drFF["StartOrder_Time"]);

                    startTime = lit_det_sum.Text;
                    tc_det_last6monthsum.BorderStyle = BorderStyle.Solid;
                    tc_det_last6monthsum.BorderWidth = 1;
                    tc_det_last6monthsum.Attributes.Add("Class", "rptCellBorder");
                    tc_det_last6monthsum.Controls.Add(lit_det_sum);
                    tr_det.Cells.Add(tc_det_last6monthsum);



                    TableCell tc_det_contri = new TableCell();
                    tc_det_contri.Width = 200;
                    tc_det_contri.HorizontalAlign = HorizontalAlign.Left;
                    Literal lit_det_contri = new Literal();
                    tc_det_contri.BorderStyle = BorderStyle.Solid;
                    tc_det_contri.BorderWidth = 1;
                    tc_det_contri.Attributes.Add("Class", "rptCellBorder");
                    tc_det_contri.Controls.Add(lit_det_contri);
                    tr_det.Cells.Add(tc_det_contri);

                    int jk = 0;
                    //   dsDoc = sf.salesmandaily_call_report_time(divcode, sfCode, date);
                    foreach (DataRow drdoctor in cc.Tables[0].Rows)
                    {
                        //DataRow[] drp = dsSalesForce.Tables[0].Select("PCode='" + drdoctor["Product_Detail_Code"].ToString() + "'");
                        //DataRow[] drp = ss.Tables[0].Select("PCode='" + drdoctor["Product_Detail_Code"].ToString() + "' and DCR_Code='" + drFF["Trans_Detail_Slno"] + "'");
                        DataRow[] drp = ss.Tables[0].Select("PCode='" + drdoctor["Product_Detail_Code"].ToString() + "' and Trans_sl_no='" + drFF["Order_No"] + "' and DCR_Code='" + drFF["Trans_Detail_Slno"] + "'");
                        int c = jk;
                        TableCell tc_qty = new TableCell();
                        tc_qty.Width = 200;
                        tc_qty.HorizontalAlign = HorizontalAlign.Left;
                        Literal lit_qty = new Literal();
                        lit_qty.Text = (drp.Length > 0) ? drp[0]["Qty"].ToString() : "";
                        if (drp.Length > 0)
                        {
                            totalqty[jk] += Convert.ToInt64(drp[0]["Qty"]);
                            unit += Convert.ToInt64(drp[0]["Qty"]);
                            unittotal += Convert.ToInt64(drp[0]["Qty"]);
                        }
                        tc_qty.BorderStyle = BorderStyle.Solid;
                        tc_qty.BorderWidth = 1;
                        tc_qty.Attributes.Add("Class", "rptCellBorder");
                        tc_qty.Controls.Add(lit_qty);
                        tr_det.Cells.Add(tc_qty);
                        jk++;
                    }

                    TableCell tc_unit = new TableCell();
                    tc_unit.Width = 200;
                    tc_unit.HorizontalAlign = HorizontalAlign.Left;
                    Literal lit_unit = new Literal();
                    lit_unit.Text = unit.ToString();
                    tc_unit.BorderStyle = BorderStyle.Solid;
                    tc_unit.BorderWidth = 1;
                    tc_unit.Attributes.Add("Class", "rptCellBorder");
                    tc_unit.Controls.Add(lit_unit);
                    tr_det.Cells.Add(tc_unit);
                    unit = 0;

                    //if (chk == 1)
                    //{
                    //    lit_det_contri.Text = (drFF["EndOrder_Time"] == DBNull.Value) ? "" : Convert.ToDateTime(drFF["EndOrder_Time"]).ToString("hh:mm tt");
                    //}
                    //else
                    //{
                    //    if (iCount == dsDoc.Tables[0].Rows.Count)
                    //    {

                    //        lit_det_contri.Text = "";
                    //        tot_value = dsDoc.Tables[0].Rows[iCount - 1][0].ToString();
                    //        endd = dsDoc.Tables[0].Rows[iCount - 1][0].ToString();
                    //        endTime = tot_value;

                    //    }
                    //    else
                    //    {
                    //        if (dsDoc.Tables[0].Rows.Count > 0)
                    //            tot_value = dsDoc.Tables[0].Rows[iCount][0].ToString();
                    //        endTime = tot_value;
                    //        lit_det_contri.Text = tot_value.ToString();
                    //    }
                    //}

                    //lit_det_contri.Text = (drFF["EndOrder_Time"] == DBNull.Value) ? "" : Convert.ToDateTime(drFF["EndOrder_Time"]).ToString("hh:mm:ss");

                    lit_det_contri.Text = (drFF["EndOrder_Time"] == DBNull.Value) ? "" : Convert.ToString(drFF["EndOrder_Time"]);


                    tbl.Rows.Add(tr_det);


                    //dsDoc = sf.salesmandaily_call_report_trans_order(divcode, sfCode, date, drFF["Trans_Detail_Info_Code"].ToString());


                    string str = drFF["Trans_Detail_Info_Code"].ToString();

                    string strdslno = drFF["Trans_Detail_Slno"].ToString();

                    DataTable dt = new DataTable();
                    DataRow[] dr = dsDoc1.Tables[0].Select("Cust_Code='" + str + "' and Trans_Sl_No='" + drFF["Order_No"] + "'");
                    if (dr.Length > 0)
                        dt = dr.CopyToDataTable<DataRow>();


                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow drFFg in dt.Rows)
                        {
                            TableCell tc_det_currentmonthy = new TableCell();
                            tc_det_currentmonthy.Width = 200;
                            tc_det_currentmonthy.HorizontalAlign = HorizontalAlign.Right;
                            HyperLink lit_det_mony = new HyperLink();

                            lit_det_mony.Text = drFFg["Order_Value"] == DBNull.Value ? drFFg["Order_Value"].ToString() : Convert.ToDecimal(drFFg["Order_Value"]).ToString("0.00");

                            net = drFFg["Order_Value"].ToString();
                            if (net != "")
                            {
                                val = decimal.Parse(net);
                                netwgttotal += val;
                            }
                            tc_det_currentmonthy.BorderStyle = BorderStyle.Solid;
                            tc_det_currentmonthy.BorderWidth = 1;
                            tc_det_currentmonthy.Attributes.Add("Class", "rptCellBorder");
                            tc_det_currentmonthy.Controls.Add(lit_det_mony);
                            tr_det.Cells.Add(tc_det_currentmonthy);

                            TableCell tc_det_currentmonthr = new TableCell();
                            tc_det_currentmonthr.Width = 200;
                            tc_det_currentmonthr.HorizontalAlign = HorizontalAlign.Right;
                            HyperLink lit_det_monr = new HyperLink();
                            lit_det_monr.Text = drFFg["net_weight_value"] == DBNull.Value ? drFFg["net_weight_value"].ToString() : Convert.ToDecimal(drFFg["net_weight_value"]).ToString("0.00");
                            string netw = drFFg["net_weight_value"].ToString();
                            if (netw != "")
                            {
                                val1 = decimal.Parse(netw);
                                valuetotal += val1;
                            }
                            tc_det_currentmonthr.BorderStyle = BorderStyle.Solid;
                            tc_det_currentmonthr.BorderWidth = 1;
                            tc_det_currentmonthr.Attributes.Add("Class", "rptCellBorder");
                            tc_det_currentmonthr.Controls.Add(lit_det_monr);
                            tr_det.Cells.Add(tc_det_currentmonthr);
                        }
                    }
                    else
                    {
                        TableCell tc_det_currentmonthy = new TableCell();
                        tc_det_currentmonthy.Width = 200;
                        tc_det_currentmonthy.HorizontalAlign = HorizontalAlign.Right;
                        HyperLink lit_det_mony = new HyperLink();
                        lit_det_mony.Text = "0.00";

                        tc_det_currentmonthy.BorderStyle = BorderStyle.Solid;
                        tc_det_currentmonthy.BorderWidth = 1;
                        tc_det_currentmonthy.Attributes.Add("Class", "rptCellBorder");
                        tc_det_currentmonthy.Controls.Add(lit_det_mony);
                        tr_det.Cells.Add(tc_det_currentmonthy);

                        TableCell tc_det_currentmonthr = new TableCell();
                        tc_det_currentmonthr.Width = 200;
                        tc_det_currentmonthr.HorizontalAlign = HorizontalAlign.Right;
                        HyperLink lit_det_monr = new HyperLink();
                        lit_det_monr.Text = "0.00";
                        tc_det_currentmonthr.BorderStyle = BorderStyle.Solid;
                        tc_det_currentmonthr.BorderWidth = 1;
                        tc_det_currentmonthr.Attributes.Add("Class", "rptCellBorder");
                        tc_det_currentmonthr.Controls.Add(lit_det_monr);
                        tr_det.Cells.Add(tc_det_currentmonthr);
                    }

                    TableCell tc_det_currentmonth = new TableCell();
                    tc_det_currentmonth.Width = 200;
                    tc_det_currentmonth.HorizontalAlign = HorizontalAlign.Left;
                    HyperLink lit_det_mon = new HyperLink();
                    lit_det_mon.Text = drFF["activity"].ToString();

                    tc_det_currentmonth.BorderStyle = BorderStyle.Solid;
                    tc_det_currentmonth.BorderWidth = 1;
                    tc_det_currentmonth.Attributes.Add("Class", "rptCellBorder");
                    tc_det_currentmonth.Controls.Add(lit_det_mon);
                    tr_det.Cells.Add(tc_det_currentmonth);
                    if (lit_det_mon.Text == "Productive")
                    {
                        productive_count += 1;
                    }


                    TimeSpan duration;
                    if (chk == 1)
                    {
                        if (drFF["EndOrder_Time"] == DBNull.Value || drFF["StartOrder_Time"] == DBNull.Value || Convert.ToDateTime(drFF["StartOrder_Time"]).Year < 2000 || Convert.ToDateTime(drFF["EndOrder_Time"]).Year < 2000)
                        {
                            duration = new TimeSpan(0, 0, 0);
                        }
                        else
                        {
                            duration = Convert.ToDateTime(drFF["EndOrder_Time"]).Subtract(Convert.ToDateTime(drFF["StartOrder_Time"]));
                        }
                    }
                    else
                    {
                        duration = DateTime.Parse(endTime).Subtract(DateTime.Parse(startTime));
                    }

                    ff += duration;

                    TableCell eltime = new TableCell();
                    eltime.Width = 200;
                    eltime.HorizontalAlign = HorizontalAlign.Left;
                    HyperLink eltimelit = new HyperLink();

                    eltimelit.Text = duration.ToString();
                    eltime.BorderStyle = BorderStyle.Solid;
                    eltime.BorderWidth = 1;
                    eltime.Attributes.Add("Class", "rptCellBorder");
                    eltime.Controls.Add(eltimelit);
                    tr_det.Cells.Add(eltime);

                    if (divcode == "128")
                    {
                        TableCell visitcell = new TableCell();
                        visitcell.Width = 200;
                        visitcell.HorizontalAlign = HorizontalAlign.Left;
                        Literal visitcelllit = new Literal();

                        visitcelllit.Text = drFF["visit_name"].ToString();
                        visitcell.BorderStyle = BorderStyle.Solid;
                        visitcell.BorderWidth = 1;
                        visitcell.Attributes.Add("Class", "rptCellBorder");
                        visitcell.Controls.Add(visitcelllit);
                        tr_det.Cells.Add(visitcell);
                    }

                    TableCell remark = new TableCell();
                    remark.Width = 200;
                    remark.HorizontalAlign = HorizontalAlign.Left;
                    Literal remarklit = new Literal();

                    remarklit.Text = drFF["Activity_Remarks"].ToString();
                    remark.BorderStyle = BorderStyle.Solid;
                    remark.BorderWidth = 1;
                    remark.Attributes.Add("Class", "rptCellBorder");
                    remark.Controls.Add(remarklit);
                    tr_det.Cells.Add(remark);

                    DataTable dt1 = new DataTable();
                    DataRow[] dr1 = Ids.Tables[0].Select("DCRDetNo='" + strdslno + "'");
                    if (dr1.Length > 0)
                        dt1 = dr1.CopyToDataTable<DataRow>();

                    if (dt1.Rows.Count > 0)
                    {
                        foreach (DataRow drFFg in dt1.Rows)
                        {
                            TableCell tc_det_currentmonthy = new TableCell();
                            tc_det_currentmonthy.Width = 50;
                            tc_det_currentmonthy.HorizontalAlign = HorizontalAlign.Left;

                            string image = Convert.ToString(drFFg["imgurl"]);

                            string ImageUrl = "";

                            if ((image != "" || image != null))
                            {
                                ImageUrl = baseUrl + "photos/" + sfCode + "_" + image;
                            }
                            else
                            {
                                ImageUrl = "";
                            }
                            tc_det_currentmonthy.Text = string.Format("<img  id='myImg' src='" + ImageUrl + "' style='width:50px;height:50px' class='fcimg' onclick='imgPOP(this)' />");

                            tc_det_currentmonthy.BorderStyle = BorderStyle.Solid;
                            tc_det_currentmonthy.BorderWidth = 1;
                            tc_det_currentmonthy.Attributes.Add("Class", "rptCellBorder");
                            tr_det.Cells.Add(tc_det_currentmonthy);
                        }
                    }
                    else
                    {
                        TableCell tc_det_currentmonthy = new TableCell();
                        tc_det_currentmonthy.Width = 50;
                        tc_det_currentmonthy.HorizontalAlign = HorizontalAlign.Left;

                        string ImageUrl = "";

                        tc_det_currentmonthy.Text = string.Format("<img id='myImg' src='" + ImageUrl + "' style='width:50px;height:50px' class='fcimg' onclick='imgPOP(this)' />");

                        tc_det_currentmonthy.BorderStyle = BorderStyle.Solid;
                        tc_det_currentmonthy.BorderWidth = 1;
                        tc_det_currentmonthy.Attributes.Add("Class", "rptCellBorder");
                        tr_det.Cells.Add(tc_det_currentmonthy);
                    }


                    if (dsSalesForce.Tables[0].Rows.Count > 0)
                    {
                        callcount.Text = dsSalesForce.Tables[0].Rows.Count.ToString();
                        closingtime.Text = endd.ToString();
                        tot_hours.Text = ff.ToString();
                        productive.Text = productive_count.ToString();
                        Tot_new_ret.Text = counts.ToString();
                        dsdocto = sf.salesmandaily_Retailer_tot(divcode, sfCode, date);
                        Total_new_rt.Text = dsdocto.Tables[0].Rows[0][0].ToString();



                        if (Convert.ToDecimal(productive.Text) > 0)
                            drop_size.Text = (Convert.ToDecimal(callcount.Text) / Convert.ToDecimal(productive.Text)).ToString("0.00");
                        else
                            drop_size.Text = "0.00";
                        if (tot_hours.Text != string.Empty)
                        {
                            var dat = Convert.ToDateTime(tot_hours.Text);

                            int hr = dat.Hour;
                            int mi = dat.Minute;
                            string str_hr = hr.ToString() + "." + mi.ToString();

                            if (Convert.ToDecimal(str_hr) > 0 && hr > 24)
                                call_average.Text = (Convert.ToDecimal(callcount.Text) / Convert.ToDecimal(str_hr)).ToString("0.00");
                            else
                                //call_average.Text = "0";
                                call_average.Text = callcount.Text;
                            //call_average.Text = (Convert.ToDecimal(callcount.Text) / Convert.ToDecimal(str_hr)).ToString("0.00");
                        }

                    }
                    else
                    {
                        Label4.Visible = false;
                        Label7.Visible = false;
                        Label5.Visible = false;
                        Label6.Visible = false;
                        Label3.Visible = false;
                        Label9.Visible = false;
                        Label8.Visible = false;
                        Label2.Visible = false;

                    }
                }

                TableRow tr_total = new TableRow();

                TableCell tc_Count_Total = new TableCell();
                tc_Count_Total.BorderStyle = BorderStyle.Solid;
                tc_Count_Total.BorderWidth = 1;
                //tc_catg_Total.Width = 25;
                Literal lit_Count_Total = new Literal();

                lit_Count_Total.Text = "Total";
                tc_Count_Total.Attributes.Add("style", "color:Red;font-weight:bold;");
                tc_Count_Total.Controls.Add(lit_Count_Total);
                tc_Count_Total.Font.Bold.ToString();
                tc_Count_Total.BackColor = System.Drawing.Color.White;
                tc_Count_Total.Attributes.Add("Class", "tbldetail_main");
                tc_Count_Total.ColumnSpan = 8;
                tc_Count_Total.Style.Add("text-align", "right");
                tc_Count_Total.Style.Add("font-family", "Calibri");
                tc_Count_Total.Attributes.Add("Class", "rptCellBorder");
                tc_Count_Total.Style.Add("font-size", "10pt");

                tr_total.Cells.Add(tc_Count_Total);


                for (int i = 0; i < cc.Tables[0].Rows.Count; i++)
                {
                    TableCell tc_totqty = new TableCell();
                    HyperLink hyp_totqty = new HyperLink();
                    hyp_totqty.Text = totalqty[i].ToString();
                    tc_totqty.BorderStyle = BorderStyle.Solid;
                    tc_totqty.BorderWidth = 1;
                    tc_totqty.BackColor = System.Drawing.Color.White;
                    tc_totqty.Width = 200;
                    tc_totqty.Style.Add("font-family", "Calibri");
                    tc_totqty.Style.Add("font-size", "10pt");
                    tc_totqty.HorizontalAlign = HorizontalAlign.Right;
                    tc_totqty.VerticalAlign = VerticalAlign.Middle;
                    tc_totqty.Controls.Add(hyp_totqty);
                    tc_totqty.Attributes.Add("style", "font-weight:bold;");
                    tc_totqty.Attributes.Add("Class", "rptCellBorder");
                    tr_total.Cells.Add(tc_totqty);
                }


                TableCell tc_totalunits = new TableCell();
                HyperLink hyp_totalunits = new HyperLink();
                hyp_totalunits.Text = unittotal.ToString();
                tc_totalunits.BorderStyle = BorderStyle.Solid;
                tc_totalunits.BorderWidth = 1;
                tc_totalunits.BackColor = System.Drawing.Color.White;
                tc_totalunits.Width = 200;
                tc_totalunits.Style.Add("font-family", "Calibri");
                tc_totalunits.Style.Add("font-size", "10pt");
                tc_totalunits.HorizontalAlign = HorizontalAlign.Right;
                tc_totalunits.VerticalAlign = VerticalAlign.Middle;
                tc_totalunits.Controls.Add(hyp_totalunits);
                tc_totalunits.Attributes.Add("style", "font-weight:bold;");
                tc_totalunits.Attributes.Add("Class", "rptCellBorder");
                tr_total.Cells.Add(tc_totalunits);



                TableCell tc_tot_month = new TableCell();
                HyperLink hyp_month = new HyperLink();
                hyp_month.Text = netwgttotal.ToString();
                tc_tot_month.BorderStyle = BorderStyle.Solid;
                tc_tot_month.BorderWidth = 1;
                tc_tot_month.BackColor = System.Drawing.Color.White;
                tc_tot_month.Width = 200;
                tc_tot_month.Style.Add("font-family", "Calibri");
                tc_tot_month.Style.Add("font-size", "10pt");
                tc_tot_month.HorizontalAlign = HorizontalAlign.Right;
                tc_tot_month.VerticalAlign = VerticalAlign.Middle;
                tc_tot_month.Controls.Add(hyp_month);
                tc_tot_month.Attributes.Add("style", "font-weight:bold;");
                tc_tot_month.Attributes.Add("Class", "rptCellBorder");
                tr_total.Cells.Add(tc_tot_month);
                //   netwgttotal = 0;
                TableCell tc_tot_monthe = new TableCell();
                HyperLink hyp_monthe = new HyperLink();
                hyp_monthe.Text = valuetotal.ToString();
                tc_tot_monthe.BorderStyle = BorderStyle.Solid;
                tc_tot_monthe.BorderWidth = 1;
                tc_tot_monthe.BackColor = System.Drawing.Color.White;
                tc_tot_monthe.Width = 200;
                tc_tot_monthe.Style.Add("font-family", "Calibri");
                tc_tot_monthe.Style.Add("font-size", "10pt");
                tc_tot_monthe.HorizontalAlign = HorizontalAlign.Right;
                tc_tot_monthe.VerticalAlign = VerticalAlign.Middle;
                tc_tot_monthe.Controls.Add(hyp_monthe);
                tc_tot_monthe.Attributes.Add("style", "font-weight:bold;");
                tc_tot_monthe.Attributes.Add("Class", "rptCellBorder");
                tr_total.Cells.Add(tc_tot_monthe);
                //   valuetotal = 0;
                TableCell tc_tot_monthg = new TableCell();
                if (divcode == "128")
                {
                    tc_tot_monthg.ColumnSpan = 4;
                }
                else
                {
                    tc_tot_monthg.ColumnSpan = 3;
                }
                tc_tot_monthg.Attributes.Add("Class", "rptCellBorder");

                tr_total.Cells.Add(tc_tot_monthg);
                tbl.Rows.Add(tr_total);

            }
            else
            {
                norecordfound.Visible = true;
                detail.Visible = false;
                callcount.Visible = false;
                closingtime.Visible = false;
                tot_hours.Visible = false;
                productive.Visible = false;
            }
        }
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
        string strFileName = Page.Title;
        string attachment = "attachment; filename='" + strFileName + "'.xls";
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

    protected void btnExport_Click(object sender, EventArgs e)
    {

        string strFileName = Page.Title;

        using (StringWriter sw = new StringWriter())
        {
            using (HtmlTextWriter hw = new HtmlTextWriter(sw))
            {
                //this.Page.RenderControl(hw);

                StringReader sr = new StringReader(sw.ToString());
                Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
                PdfWriter writer = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                pdfDoc.Open();
                XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                pdfDoc.Close();
                Response.ContentType = "application/pdf";
                Response.AddHeader("content-disposition", "attachment;filename= '" + strFileName + "'.pdf");
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.Write(pdfDoc);
                Response.End();
            }

        }
    }



    protected void btnClose_Click(object sender, EventArgs e)
    {

    }

    public class locl
    {

        public DataSet salesmandaily_call_report(string div_code, string sfcode, string date, string dist_Code = "")
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;
            string strQry = " SELECT * FROM ( SELECT M.Trans_Detail_Slno,A.Sf_Code,A.Sf_Name,a.WorkType_Name, ";
            strQry += " m.Trans_Detail_Name,m.SDP_Name,m.Trans_Detail_Info_Code,a.Activity_Date,m.Activity_Remarks,m.tm, ";
            strQry += " ( SELECT g.stockist_name from Mas_stockist g WHERE g.stockist_code = m.stockist_code) as stockist_name, ";
            strQry += " l.Doc_Spec_ShortName,isnull(l.ListedDr_Address1,'')ListedDr_Address1,CONVERT(VARCHAR(26),l.ListedDr_Created_Date, 23) ListedDr_Created_Date, ";
            strQry += " (case when isnull(M.Order_Value,0)> 0 then'Productive' else 'Visited' end ) as activity, ";
            strQry += " gc.lat,gc.long,M.[Time],isnull(M.Order_Value,0) as Order_Value  ";
            strQry += " from [vwActivity_MSL_Details] M  ";
            strQry += " Inner Join [vwActivity_Report] A on M.Trans_SlNo = A.Trans_SlNo and m.Division_Code = a.Division_Code   ";
            strQry += " Inner Join Mas_ListedDr l on l.ListedDrCode = m.Trans_Detail_Info_Code  ";
            strQry += " left join map_GEO_Customers gc on gc.Cust_Code = m.Trans_Detail_Info_Code    ";
            strQry += " WHERE A.sf_code = '" + sfcode + "'  and a.Division_Code = '" + div_code + "'    ";
            strQry += " and CONVERT(VARCHAR(25), a.Activity_Date, 126) LIKE '" + date + "%' ) as t ORDER BY Trans_Detail_Name   ";

            try
            {
                dsAdmin = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsAdmin;
        }


        public DataSet getqtydailycall(string div_code, string sfcode, string date)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            string strQry = " SELECT od.Trans_sl_no Trans_sl_no,m.Trans_Detail_Slno Trans_Detail_Slno,od.DCR_Code DCR_Code,d.Product_Code Pcode,d.Product_Name Pname, ";
            strQry += " d.Quantity Qty,d.[value] Order_Value,d.[Rate],d.[net_weight] FROM  [vwActivity_MSL_Details] m  ";
            strQry += " left outer join trans_order_head od on od.DCR_Code = m.Trans_Detail_SlNo  ";
            strQry += " left outer join trans_order_details d on od.Trans_sl_no = d.trans_sl_no ";
            strQry += " WHERE od.sf_code = '" + sfcode + "' and convert(date, order_date)= '" + date + "' and od.Div_ID = '" + div_code + "'  ";
            strQry += " GROUP BY od.Trans_sl_no,m.Trans_Detail_Slno,od.DCR_Code,d.Product_Code,d.Product_Name,d.Quantity,d.[value] ,d.[Rate],d.[net_weight]  ";


            //strQry = "select od.Trans_sl_no Trans_sl_no,m.Trans_Detail_Slno Trans_Detail_Slno,od.DCR_Code DCR_Code,d.Product_Code Pcode,d.Product_Name Pname,d.Quantity Qty from [vwActivity_MSL_Details] m left outer join trans_order_head od on od.DCR_Code=m.Trans_Detail_SlNo left outer join trans_order_details d on od.Trans_sl_no=d.trans_sl_no " +
            //         " where od.sf_code = '" + sfcode + "' and convert(date, order_date)= '" + date + "' and od.Div_ID = '" + div_code + "'   group by od.Trans_sl_no,m.Trans_Detail_Slno,od.DCR_Code,d.Product_Code,d.Product_Name,d.Quantity";

            try
            {
                dsAdmin = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsAdmin;

        }

    }
}