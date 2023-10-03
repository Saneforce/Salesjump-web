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

using System.Web.Services;
using System.Runtime.Serialization.Json;

public partial class MIS_Reports_rpt_Fieldforce_sales_analysis : System.Web.UI.Page
{
    string sfCode = string.Empty;
    string sfname = string.Empty;
    string divcode = string.Empty;
	string subdiv = string.Empty;
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
		subdiv = Request.QueryString["subdiv"].ToString();
        hidn_sf_code.Value = sfCode;
        hidnYears.Value = FYear;
        lblsf_name.Text = Request.QueryString["SF_Name"].ToString(); ;
        lblyear.Text = FYear;
		subdivision.Value = subdiv;

        FillSF();
    }

    private void FillSF()
    {
        GV_DATA.DataSource = null;
        GV_DATA.DataBind();
        RoutePlan rop = new RoutePlan();
        //ListedDR ldr = new ListedDR();
        //DataSet DsRoute = rop.get_Route_Name(divcode, sfCode);
        // DataSet DsRetailer = ldr.Get_Retailer_sal(divcode, FYear, sfCode);
        // DataSet tDsRetailer = ldr.tGet_Retailer_sal(divcode, FYear, sfCode);
        DataSet DsState = get_State_Name(divcode, sfCode,subdiv);              
        DataSet DsRetailer = Get_FeildForce_sale(divcode, FYear, sfCode, subdiv);       
        DataSet tDsRetailer = tGet_FeildForce_sal(divcode, FYear, sfCode, subdiv);       

        DataTable dsData = new DataTable();
        dsData.Columns.Add("Code", typeof(string));
        dsData.Columns.Add("Name", typeof(string));
        dsData.Columns.Add("HQ", typeof(string));
        dsData.Columns.Add("MobileNo", typeof(string));         
        dsData.Columns.Add("JAN", typeof(string));
        dsData.Columns.Add("FEB", typeof(string));
        dsData.Columns.Add("MAR", typeof(string));
        dsData.Columns.Add("APR", typeof(string));
        dsData.Columns.Add("MAY", typeof(string));
        dsData.Columns.Add("JUN", typeof(string));
        dsData.Columns.Add("JUL", typeof(string));
        dsData.Columns.Add("AUG", typeof(string));
        dsData.Columns.Add("SEP", typeof(string));
        dsData.Columns.Add("OCT", typeof(string));
        dsData.Columns.Add("NOV", typeof(string));
        dsData.Columns.Add("DEC", typeof(string));
        dsData.Columns.Add("TOTAL", typeof(decimal));
        int i = 1;
        dsData.Rows.Add("1", "Order Given FieldForce (Out of Total " + DsRetailer.Tables[0].Rows.Count.ToString() + ")");
        dsData.Rows.Add("1", "Order Total");

        decimal jan_ot = 0;
        decimal feb_ot = 0;
        decimal mar_ot = 0;
        decimal apr_ot = 0;
        decimal may_ot = 0;
        decimal jun_ot = 0;
        decimal jul_ot = 0;
        decimal aug_ot = 0;
        decimal sep_ot = 0;
        decimal oct_ot = 0;
        decimal nov_ot = 0;
        decimal dec_ot = 0;

        int jan_count = 0;
        int feb_count = 0;
        int mar_count = 0;
        int apr_count = 0;
        int may_count = 0;
        int jun_count = 0;
        int jul_count = 0;
        int aug_count = 0;
        int sep_count = 0;
        int oct_count = 0;
        int nov_count = 0;
        int dec_count = 0;

        foreach (DataRow dr in DsState.Tables[0].Rows)
        {
            decimal jan_tot = 0;
            decimal feb_tot = 0;
            decimal mar_tot = 0;
            decimal apr_tot = 0;
            decimal may_tot = 0;
            decimal jun_tot = 0;
            decimal jul_tot = 0;
            decimal aug_tot = 0;
            decimal sep_tot = 0;
            decimal oct_tot = 0;
            decimal nov_tot = 0;
            decimal dec_tot = 0;

            DataRow[] drow = DsRetailer.Tables[0].Select("State_Code = '" + dr["State_Code"].ToString() + "'");

            int stCount = drow.Length;
            dsData.Rows.Add("0", "State:- " + (i++) + " " + dr["StateName"].ToString() + ",(" + stCount.ToString() + ") ");
            if (drow.Length > 0)
            {

                foreach (DataRow row in drow)
                {
                    decimal jan_val = row["jan"] == DBNull.Value ? 0 : Convert.ToDecimal(row["jan"]);
                    jan_tot += jan_val;
                    if (jan_val > 0)
                    {
                        jan_count++;
                    }
                    DataRow[] drpj = tDsRetailer.Tables[0].Select("Sf_Code='" + row["Sf_Code"].ToString() + "'");
                    string jdt = Convert.ToString(jan_val) + "(" + drpj[0]["jan"].ToString() + ")";
                    decimal feb_val = row["feb"] == DBNull.Value ? 0 : Convert.ToDecimal(row["feb"]);
                    feb_tot += feb_val;
                    if (feb_val > 0)
                    {
                        feb_count++;
                    }
                    string fdt = Convert.ToString(feb_val) + "(" + drpj[0]["feb"].ToString() + ")";
                    decimal mar_val = row["mar"] == DBNull.Value ? 0 : Convert.ToDecimal(row["mar"]);
                    mar_tot += mar_val;
                    if (mar_val > 0)
                    {
                        mar_count++;
                    }
                    string mdt = Convert.ToString(mar_val) + "(" + drpj[0]["mar"].ToString() + ")";
                    decimal apr_val = row["apr"] == DBNull.Value ? 0 : Convert.ToDecimal(row["apr"]);
                    apr_tot += apr_val;
                    if (apr_val > 0)
                    {
                        apr_count++;
                    }
                    string adt = Convert.ToString(apr_val) + "(" + drpj[0]["apr"].ToString() + ")";
                    decimal may_val = row["may"] == DBNull.Value ? 0 : Convert.ToDecimal(row["may"]);
                    may_tot += may_val;
                    if (may_val > 0)
                    {
                        may_count++;
                    }
                    string mydt = Convert.ToString(may_val) + "(" + drpj[0]["may"].ToString() + ")";
                    decimal jun_val = row["jun"] == DBNull.Value ? 0 : Convert.ToDecimal(row["jun"]);
                    jun_tot += jun_val;
                    if (jun_val > 0)
                    {
                        jun_count++;
                    }
                    string jndt = Convert.ToString(jun_val) + "(" + drpj[0]["jun"].ToString() + ")";
                    decimal jul_val = row["jul"] == DBNull.Value ? 0 : Convert.ToDecimal(row["jul"]);
                    jul_tot += jul_val;
                    if (jul_val > 0)
                    {
                        jul_count++;
                    }
                    string jldt = Convert.ToString(jul_val) + "(" + drpj[0]["jul"].ToString() + ")";
                    decimal aug_val = row["aug"] == DBNull.Value ? 0 : Convert.ToDecimal(row["aug"]);
                    aug_tot += aug_val;
                    if (aug_val > 0)
                    {
                        aug_count++;
                    }
                    string audt = Convert.ToString(aug_val) + "(" + drpj[0]["aug"].ToString() + ")";
                    decimal sep_val = row["sep"] == DBNull.Value ? 0 : Convert.ToDecimal(row["sep"]);
                    sep_tot += sep_val;
                    if (sep_val > 0)
                    {
                        sep_count++;
                    }
                    string sedt = Convert.ToString(sep_val) + "(" + drpj[0]["sep"].ToString() + ")";
                    decimal oct_val = row["oct"] == DBNull.Value ? 0 : Convert.ToDecimal(row["oct"]);
                    oct_tot += oct_val;
                    if (oct_val > 0)
                    {
                        oct_count++;
                    }
                    string ocdt = Convert.ToString(oct_val) + "(" + drpj[0]["oct"].ToString() + ")";
                    decimal nov_val = row["nov"] == DBNull.Value ? 0 : Convert.ToDecimal(row["nov"]);
                    nov_tot += nov_val;
                    if (nov_val > 0)
                    {
                        nov_count++;
                    }
                    string nodt = Convert.ToString(nov_val) + "(" + drpj[0]["nov"].ToString() + ")";
                    decimal dec_val = row["dec"] == DBNull.Value ? 0 : Convert.ToDecimal(row["dec"]);
                    dec_tot += dec_val;
                    if (dec_val > 0)
                    {
                        dec_count++;
                    }
                    string dedt = Convert.ToString(dec_val) + "(" + drpj[0]["dec"].ToString() + ")";
                    decimal cur_tot = jan_val + feb_val + mar_val + apr_val + may_val + jun_val + jul_val + aug_val + sep_val + oct_val + nov_val + dec_val;
                    if (divcode == "109")
                    {
                        dsData.Rows.Add(row["Sf_Code"].ToString(), row["SF_Name"].ToString(), row["Sf_HQ"].ToString(), row["SF_Mobile"].ToString(), jdt, fdt, mdt, adt, mydt, jndt, jldt, audt, sedt, ocdt, nodt, dedt, cur_tot);
                    }
                    else
                    {
                        dsData.Rows.Add(row["Sf_Code"].ToString(), row["SF_Name"].ToString(), row["Sf_HQ"].ToString(), row["SF_Mobile"].ToString(), jan_val, feb_val, mar_val, apr_val, may_val, jun_val, jul_val, aug_val, sep_val, oct_val, nov_val, dec_val, cur_tot);
                    }

                }
            }
            jan_ot += jan_tot;
            feb_ot += feb_tot;
            mar_ot += mar_tot;
            apr_ot += apr_tot;
            may_ot += may_tot;
            jun_ot += jun_tot;
            jul_ot += jul_tot;
            aug_ot += aug_tot;
            sep_ot += sep_tot;
            oct_ot += oct_tot;
            nov_ot += nov_tot;
            dec_ot += dec_tot;
            decimal c_tot = jan_tot + feb_tot + mar_tot + apr_tot + may_tot + jun_tot + jul_tot + aug_tot + sep_tot + oct_tot + nov_tot + dec_tot;
            dsData.Rows.Add("", "Total", "", "", jan_tot, feb_tot, mar_tot, apr_tot, may_tot, jun_tot, jul_tot, aug_tot, sep_tot, oct_tot, nov_tot, dec_tot, c_tot);
        }
        // order count add in first row 
        int cou = 0;
        dsData.Rows[0][cou + 4] = jan_count;
        dsData.Rows[0][cou + 5] = feb_count;
        dsData.Rows[0][cou + 6] = mar_count;
        dsData.Rows[0][cou + 7] = apr_count;
        dsData.Rows[0][cou + 8] = may_count;
        dsData.Rows[0][cou + 9] = jun_count;
        dsData.Rows[0][cou + 10] = jul_count;
        dsData.Rows[0][cou + 11] = aug_count;
        dsData.Rows[0][cou + 12] = sep_count;
        dsData.Rows[0][cou + 13] = oct_count;
        dsData.Rows[0][cou + 14] = nov_count;
        dsData.Rows[0][cou + 15] = dec_count;
        dsData.Rows[0][cou + 16] = jan_count + feb_count + mar_count + apr_count + may_count + jun_count + jul_count + aug_count + sep_count + oct_count + nov_count + dec_count;




        // over all total in second row add 
        dsData.Rows[1][cou + 4] = jan_ot;
        dsData.Rows[1][cou + 5] = feb_ot;
        dsData.Rows[1][cou + 6] = mar_ot;
        dsData.Rows[1][cou + 7] = apr_ot;
        dsData.Rows[1][cou + 8] = may_ot;
        dsData.Rows[1][cou + 9] = jun_ot;
        dsData.Rows[1][cou + 10] = jul_ot;
        dsData.Rows[1][cou + 11] = aug_ot;
        dsData.Rows[1][cou + 12] = sep_ot;
        dsData.Rows[1][cou + 13] = oct_ot;
        dsData.Rows[1][cou + 14] = nov_ot;
        dsData.Rows[1][cou + 15] = dec_ot;
        dsData.Rows[1][cou + 16] = jan_ot + feb_ot + mar_ot + apr_ot + may_ot + jun_ot + jul_ot + aug_ot + sep_ot + oct_ot + nov_ot + dec_ot;

        GV_DATA.DataSource = dsData;
        GV_DATA.DataBind();

    }

    protected void Dgv_SKU_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            GridViewRow grv = e.Row;
            if (grv.Cells[0].Text.Equals("0"))
            {
                e.Row.BackColor = System.Drawing.Color.FromName("#39435C");
                e.Row.ForeColor = System.Drawing.Color.FromName("#fff");
                e.Row.Font.Bold = true;

            }
            if (grv.Cells[0].Text.Equals("1"))
            {
                e.Row.BackColor = System.Drawing.Color.FromName("#6b7794");
                e.Row.ForeColor = System.Drawing.Color.FromName("#fff");
                e.Row.Font.Bold = true;

            }
            if (grv.Cells[1].Text.Equals("Total"))
            {
                e.Row.BackColor = System.Drawing.Color.FromName("#99FFFF");
                e.Row.Font.Bold = true;

            }

            e.Row.Cells[0].Visible = true;
            // e.Row.Cells[0].Width = 100;
            e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Left;
            e.Row.Cells[1].Width = 250;
            e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Left;
            e.Row.Cells[2].Width = 100;
            e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Left;
            e.Row.Cells[3].Width = 100;
            e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Left;
            e.Row.Cells[4].Width = 80;
            e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Left;

            for (int i = 5; i < e.Row.Cells.Count; i++)
            {
                //  e.Row.Cells[i].Width = 100;
                e.Row.Cells[i].HorizontalAlign = HorizontalAlign.Right;

            }


        }
        catch (Exception ex)
        { }

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
        string attachment = "attachment; filename=" + strFileName + ".xls";

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

    protected void btnClose_Click(object sender, EventArgs e)
    {
        // Response.Write("Purchase_Register_Distributor_wise.aspx");

    }
    protected void btnExport_Click(object sender, EventArgs e)
    {

        string strFileName = Page.Title;

        using (StringWriter sw = new StringWriter())
        {
            using (HtmlTextWriter hw = new HtmlTextWriter(sw))
            {
                //this.Page.RenderControl(hw);
                this.RenderControl(hw);
                StringReader sr = new StringReader(sw.ToString());
                Document pdfDoc = new Document(PageSize.A3, 10f, 10f, 10f, 0f);
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

    public class VisitedList
    {
        public string custCode { get; set; }
        public string months { get; set; }
        public string counts { get; set; }
    }

    public class RepeatOrders
    {
        public string custCode { get; set; }
        public string months { get; set; }
        public string counts { get; set; }
    }


    public class TotCounts
    {
        public List<RepeatOrders> rOrder = new List<RepeatOrders>();
        public List<VisitedList> rVisit = new List<VisitedList>();

    }


    [WebMethod(EnableSession = true)]
    public static string GetVisitedDtls(string SF_Code, string FYears)
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
        ListedDR ldr = new ListedDR();
        DataSet dsVisited = new DataSet();
        dsVisited = ldr.Get_Retailer_salVisted(div_code, SF_Code, FYears);
        TotCounts Tcount = new TotCounts();
        foreach (DataRow row in dsVisited.Tables[0].Rows)
        {
            VisitedList vl = new VisitedList();
            vl.custCode = row["Trans_Detail_Info_Code"].ToString();
            vl.months = row["Months"].ToString();
            vl.counts = row["CNTS"].ToString();
            Tcount.rVisit.Add(vl);
        }

        foreach (DataRow row in dsVisited.Tables[1].Rows)
        {
            RepeatOrders vl = new RepeatOrders();
            vl.custCode = row["Trans_Detail_Info_Code"].ToString();
            vl.months = row["Months"].ToString();
            vl.counts = row["CNTS"].ToString();
            Tcount.rOrder.Add(vl);
        }

        JSonHelper helper = new JSonHelper();
        String jsonResult = helper.ConvertObjectToJSon(Tcount);
        return jsonResult;

    }
    public class JSonHelper
    {
        public string ConvertObjectToJSon<T>(T obj)
        {
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
            MemoryStream ms = new MemoryStream();
            ser.WriteObject(ms, obj);
            string jsonString = Encoding.UTF8.GetString(ms.ToArray());
            ms.Close();
            return jsonString;

        }
        public T ConverJSonToObject<T>(string jsonString)
        {
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T));
            MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonString));
            T obj = (T)serializer.ReadObject(ms);
            return obj;
        }
    }
    public DataSet Get_Retailer_salVisted(string Div_Code, string SF_code, string fyear)
    {
        string strQry = string.Empty;
        DataSet dsAdmin = null;
        DBase_EReport.DB_EReporting db_ER = new DBase_EReport.DB_EReporting();

        strQry = "exec Get_FieldForce_sal_VISIT '" + Div_Code + "','" + SF_code + "','" + fyear + "'";

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

    public  DataSet get_State_Name(string divcode, string sf_Code ,string subdiv = "0")
    {        
        string strQry = string.Empty;
        DataSet dsState = null;
        DBase_EReport.DB_EReporting db_ER = new DBase_EReport.DB_EReporting();

        strQry = "exec get_Fieldforce_State_Name '" + sf_Code + "','" + divcode + "','" + subdiv + "'";
        try
        {
            dsState = db_ER.Exec_DataSet(strQry);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return dsState;
    }
    public DataSet Get_FeildForce_sale(string Div_Code, string fyear, string SF_code = "0", string subdiv = "0")
    {
        string strQry = string.Empty;
        DataSet dsAdmin = null;
        DBase_EReport.DB_EReporting db_ER = new DBase_EReport.DB_EReporting();
        
        strQry = "exec Get_FieldForce_sal '" + Div_Code + "','" + fyear + "','" + SF_code + "','" + subdiv + "'";

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
    
    public DataSet tGet_FeildForce_sal(string Div_Code, string fyear, string SF_code = "0", string subdiv = "0")
    {
        string strQry = string.Empty;
        DataSet dsAdmin = null;

        DBase_EReport.DB_EReporting db_ER = new DBase_EReport.DB_EReporting();

        strQry = "exec tGet_FieldForce_sal '" + Div_Code + "','" + fyear + "','" + SF_code + "','" + subdiv + "'";

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