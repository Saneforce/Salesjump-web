using System;
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
using System.Web.UI.HtmlControls;

public partial class MIS_Reports_rpt_emp_orderv_alue : System.Web.UI.Page
{
    public string sf_code = string.Empty;   
   public string div_code = string.Empty;   
   // string sf_type = string.Empty;  
   
  public  string tmonth = string.Empty;   
   public string Year = string.Empty;   
    string Sub_Div_Code = string.Empty;
    public string sf_name = string.Empty;
    public string sf_type = string.Empty;
    public string CDate = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
           div_code = Session["div_code"].ToString();
        sf_code = Request.QueryString["SF_Code"].ToString(); // Session["SF_code"].ToString();
        Year = Request.QueryString["Year"].ToString();
        Sub_Div_Code = Request.QueryString["Sub_Div"].ToString();
        sf_name=Request.QueryString["SF_Name"].ToString();
        hdSub_Div.Value = Sub_Div_Code;
        lblhead.Text = "   " + Year;
        hd_sfcode.Value = sf_code;
        lblhead1.Text = "SECONDARY ORDERS - <b>" + Year + "</b>";
        lblsf.Text = " " + Request.QueryString["SF_Name"].ToString();
        sf_type = Session["sf_type"].ToString();
		CDate = Request.QueryString["Date"].ToString();
        tmonth = Request.QueryString["cur_month"].ToString();

      
        FillSF();
    }
    private void FillSF()
    {

        DataTable Target_Dt = new DataTable();
     Target_Dt.Columns.Add("sf_code", typeof(string));
        Target_Dt.Columns.Add("SF_CODE", typeof(string));
        Target_Dt.Columns.Add("Name", typeof(string));
        Target_Dt.Columns.Add("cnt", typeof(decimal));
        Target_Dt.Columns.Add("JAN_OV", typeof(decimal));
       

       
        Target_Dt.Columns.Add("FEB_OV", typeof(decimal));
        
        Target_Dt.Columns.Add("MAR_OV", typeof(decimal));
        
        Target_Dt.Columns.Add("APR_OV", typeof(decimal));
       
        Target_Dt.Columns.Add("MAY_OV", typeof(decimal));
        
        Target_Dt.Columns.Add("JUN_OV", typeof(decimal));
       
        Target_Dt.Columns.Add("JUL_OV", typeof(decimal));
        
        Target_Dt.Columns.Add("AUG_oV", typeof(decimal));
        
        Target_Dt.Columns.Add("SEP_OV", typeof(decimal));
       
        Target_Dt.Columns.Add("OCT_OV", typeof(decimal));
       
        Target_Dt.Columns.Add("NOV_OV", typeof(decimal));
        
        Target_Dt.Columns.Add("DEC_OV", typeof(decimal));
        
        Target_Dt.Columns.Add("TOT_OV", typeof(decimal));
       

        SalesForce SF = new SalesForce();
      
        Order od = new Order();


        //decimal ovr_tot_jan_ord = 0;

        //  decimal ovr_tot_feb_ord = 0;

        // decimal ovr_tot_mar_ord = 0;

        //  decimal ovr_tot_apr_ord = 0;

        //   decimal ovr_tot_may_ord = 0;

        //   decimal ovr_tot_jun_ord = 0;

        //  decimal ovr_tot_jul_ord = 0;

        //  decimal ovr_tot_aug_ord = 0;

        //  decimal ovr_tot_sep_ord = 0;

        // decimal ovr_tot_oct_ord = 0;

        //   decimal ovr_tot_nov_ord = 0;

        //  decimal ovr_tot_dec_ord = 0;

        decimal Tjan = 0, tfeb = 0, tmar = 0, tapr = 0, tmay = 0, tjun = 0, tjul = 0, taug = 0, tsep = 0, toct = 0, tnov = 0, tdec = 0;
           DataSet dsTO = null;
          
          DataSet dsTOcnt = null;

        dsTO = od.get_order_value(div_code, Year);
        dsTOcnt = od.get_order_valuecount(div_code, Year);

        int cnt = 0;
        foreach (DataRow dr in dsTO.Tables[0].Rows)
        {

           decimal jano = dr["jan"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["jan"]);
            decimal febo = dr["feb"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["feb"]);
            decimal maro = dr["mar"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["mar"]);
            decimal apro = dr["app"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["app"]);
            decimal mayo = dr["may"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["may"]);
            decimal juno = dr["june"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["june"]);
            decimal julo = dr["july"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["july"]);
            decimal augo = dr["aguest"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["aguest"]);
            decimal sepo = dr["sep"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["sep"]);
            decimal octo = dr["oct"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["oct"]);
            decimal novo = dr["nav"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["nav"]);
            decimal deco = dr["dece"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["dece"]);


            //Target_Dt.Rows.Add(Year, dr["sf_name"].ToString(), dr["sf_code"].ToString(), jant, jano.ToString("0.00"), jana.ToString("0.00"), febt, febo.ToString("0.00"), feba.ToString("0.00"), mart, maro.ToString("0.00"), mara.ToString("0.00"), aprt, apro.ToString("0.00"), apra.ToString("0.00"), mayt, mayo.ToString("0.00"), maya.ToString("0.00"), junt, juno.ToString("0.00"), juna.ToString("0.00"), jult, julo.ToString("0.00"), jula.ToString("0.00"), augt, augo.ToString("0.00"), auga.ToString("0.00"), sept, sepo.ToString("0.00"), sepa.ToString("0.00"), octt, octo.ToString("0.00"), octa.ToString("0.00"), novt, novo.ToString("0.00"), nova.ToString("0.00"), dect, deco.ToString("0.00"), deca.ToString("0.00"), TOT_T, TOT_O.ToString("0.00"), TOT_A.ToString("0.00"));


            decimal TOT_Ot = jano + febo + maro + apro + mayo + juno + julo + augo + sepo + octo + novo + deco;
            Tjan += jano; tfeb += febo; tmar += maro; tapr += apro; tmay += mayo; tjun += julo; tjul += augo; taug += augo; tsep += sepo; toct += octo; tnov += novo; tdec += deco;

            //Target_Dt.Rows.Add(dr["Sf_Code"].ToString(),dr["name"].ToString(), jano,  febo, maro, apro, mayo, juno, julo, augo, sepo, octo, novo, deco, TOT_Ot);

 			foreach (DataRow dr1 in dsTOcnt.Tables[0].Rows)
            {
                if (dr1["sf_code"].ToString() == dr["Sf_Code"].ToString())
                {
                   cnt += Convert.ToInt32(dr1["cnt"]);
                    Target_Dt.Rows.Add(dr["sf_code"].ToString(),dr["Sf_Code"].ToString(), dr["name"].ToString(), dr1["cnt"].ToString(), jano, febo, maro, apro, mayo, juno, julo, augo, sepo, octo, novo, deco, TOT_Ot);
                }
           
            //Target_Dt.Rows.Add(dr["Sf_Code"].ToString(), dr["name"].ToString(), dr["cnt"].ToString(),  jano, febo, maro, apro, mayo, juno, julo, augo, sepo, octo, novo, deco, TOT_Ot);

        
           

        }
}
        //decimal OVR_TOT_O = ovr_tot_jan_ord + ovr_tot_feb_ord + ovr_tot_mar_ord + ovr_tot_apr_ord + ovr_tot_may_ord + ovr_tot_jun_ord + ovr_tot_jul_ord + ovr_tot_aug_ord + ovr_tot_sep_ord + ovr_tot_oct_ord + ovr_tot_nov_ord + ovr_tot_dec_ord;

        decimal TOT_Ov = Tjan + tfeb + tmar + tapr + tmay + tjun + tjul + taug + tsep + toct + tnov + tdec;

        Target_Dt.Rows.Add("","","", cnt, Tjan , tfeb , tmar , tapr , tmay , tjun , tjul ,taug , tsep , toct , tnov , tdec, TOT_Ov);
        GVData.DataSource = Target_Dt;
        GVData.DataBind();

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
        lblhead1.Visible = true;
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
        lblhead1.Visible = false;

        using (StringWriter sw = new StringWriter())
        {
            using (HtmlTextWriter hw = new HtmlTextWriter(sw))
            {

                //this.Page.RenderControl(hw);
                this.RenderControl(hw);

                StringReader sr = new StringReader(sw.ToString());
                Document pdfDoc = new Document(PageSize.A1, 10f, 10f, 10f, 0f);
                //  Document pdfDoc = new Document(new Rectangle(200f, 300f));
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

}