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

public partial class MIS_Reports_rpt_emp_order_valueSTKwise : System.Web.UI.Page
{

   public string sf_code = string.Empty;
    public string sfname = string.Empty;
    string div_code = string.Empty;
    decimal iTotLstCount1 = 0;
    decimal iTotLstCount = 0;
    //string sf_type = string.Empty;
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
    public string Year = string.Empty;

    public string SF_Name=string.Empty;
    DataSet dsprd = new DataSet();
    string sCurrentDate = string.Empty;
    string stockist_code = string.Empty;
    string stURL = string.Empty;
    string Stock_name = string.Empty;
    string Sub_Div_Code = string.Empty;
 public string CDate = string.Empty;
    public string tmonth = string.Empty;
    public string sf_type = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        sf_code = Request.QueryString["SF_Code"].ToString(); // Session["SF_code"].ToString();
        Year = Request.QueryString["Year"].ToString();
        SF_Name=Request.QueryString["SF_Name"].ToString();
        //Sub_Div_Code = Request.QueryString["Sub_Div"].ToString();
        // hdSub_Div.Value = Sub_Div_Code;
        lblhead.Text = "   " + Year;
        hd_sfcode.Value = sf_code;
        lblhead1.Text = "SECONDARY ORDERS - <b>" + Year + "</b>";
        lblsf.Text = " " + Request.QueryString["SF_Name"].ToString();
 CDate = Request.QueryString["Date"].ToString();
        tmonth = Request.QueryString["cur_month"].ToString();
        sf_type = Session["sf_type"].ToString();
        FillSF();
    }
    private void FillSF()
    {

        DataTable Target_Dt = new DataTable();
        Target_Dt.Columns.Add("sf_code", typeof(string));
        Target_Dt.Columns.Add("Sf_Name", typeof(string));
        Target_Dt.Columns.Add("Stockist_Code", typeof(string));
        Target_Dt.Columns.Add("Stockist_Name", typeof(string));
Target_Dt.Columns.Add("Sf_Code1", typeof(string));
        Target_Dt.Columns.Add("Stockist_Code1", typeof(string));
        Target_Dt.Columns.Add("cnt", typeof(int));
        Target_Dt.Columns.Add("JAN_TV", typeof(int));
        Target_Dt.Columns.Add("JAN_wTOV", typeof(decimal));
        Target_Dt.Columns.Add("JAN_TOV", typeof(decimal));
        Target_Dt.Columns.Add("JAN_OV", typeof(decimal));
        Target_Dt.Columns.Add("FEB_TV", typeof(int));
        Target_Dt.Columns.Add("FEB_wTOV", typeof(decimal));
        Target_Dt.Columns.Add("FEB_TOV", typeof(decimal));
        Target_Dt.Columns.Add("FEB_OV", typeof(decimal));
        Target_Dt.Columns.Add("MAR_TV", typeof(int));
        Target_Dt.Columns.Add("MAR_wTOV", typeof(decimal));
        Target_Dt.Columns.Add("MAR_TOV", typeof(decimal));
        Target_Dt.Columns.Add("MAR_OV", typeof(decimal));
        Target_Dt.Columns.Add("APR_TV", typeof(int));
        Target_Dt.Columns.Add("APR_wTOV", typeof(decimal));
        Target_Dt.Columns.Add("APR_TOV", typeof(decimal));
        Target_Dt.Columns.Add("APR_OV", typeof(decimal));
        Target_Dt.Columns.Add("MAY_TV", typeof(int));
        Target_Dt.Columns.Add("MAY_wTOV", typeof(decimal));
        Target_Dt.Columns.Add("MAY_TOV", typeof(decimal));
        Target_Dt.Columns.Add("MAY_OV", typeof(decimal));
        Target_Dt.Columns.Add("JUN_TV", typeof(int));
        Target_Dt.Columns.Add("JUN_wTOV", typeof(decimal));
        Target_Dt.Columns.Add("JUN_TOV", typeof(decimal));
        Target_Dt.Columns.Add("JUN_OV", typeof(decimal));
        Target_Dt.Columns.Add("JUL_TV", typeof(int));
        Target_Dt.Columns.Add("JUL_wTOV", typeof(decimal));
        Target_Dt.Columns.Add("JUL_TOV", typeof(decimal));
        Target_Dt.Columns.Add("JUL_OV", typeof(decimal));
        Target_Dt.Columns.Add("AUG_TV", typeof(int));
        Target_Dt.Columns.Add("AUG_wTOV", typeof(decimal));
        Target_Dt.Columns.Add("AUG_TOV", typeof(decimal));
        Target_Dt.Columns.Add("AUG_oV", typeof(decimal));
        Target_Dt.Columns.Add("SEP_TV", typeof(int));
        Target_Dt.Columns.Add("SEP_wTOV", typeof(decimal));
        Target_Dt.Columns.Add("SEP_TOV", typeof(decimal));
        Target_Dt.Columns.Add("SEP_OV", typeof(decimal));
        Target_Dt.Columns.Add("OCT_TV", typeof(int));
        Target_Dt.Columns.Add("OCT_wTOV", typeof(decimal));
        Target_Dt.Columns.Add("OCT_TOV", typeof(decimal));
        Target_Dt.Columns.Add("OCT_OV", typeof(decimal));
        Target_Dt.Columns.Add("NOV_TV", typeof(int));
        Target_Dt.Columns.Add("NOV_wTOV", typeof(decimal));
        Target_Dt.Columns.Add("NOV_TOV", typeof(decimal));
        Target_Dt.Columns.Add("NOV_OV", typeof(decimal));
        Target_Dt.Columns.Add("DEC_TV", typeof(int));
        Target_Dt.Columns.Add("DEC_wTOV", typeof(decimal));
        Target_Dt.Columns.Add("DEC_TOV", typeof(decimal));
        Target_Dt.Columns.Add("DEC_OV", typeof(decimal));
        Target_Dt.Columns.Add("TOT_TV", typeof(int));
        Target_Dt.Columns.Add("TOT_wTOV", typeof(decimal));
        Target_Dt.Columns.Add("TOT_TOV", typeof(decimal));
        Target_Dt.Columns.Add("TOT_OV", typeof(decimal));
       // Target_Dt.Columns.Add("TOT_ACH", typeof(decimal));
        SalesForce SF = new SalesForce();

        Order od = new Order();     

        decimal Tjan = 0, tfeb = 0, tmar = 0, tapr = 0, tmay = 0, tjun = 0, tjul = 0, taug = 0, tsep = 0, toct = 0, tnov = 0, tdec = 0;
        int Tcjan = 0, tcfeb = 0, tcmar = 0, tcapr = 0, tcmay = 0, tcjun = 0, tcjul = 0, tcaug = 0, tcsep = 0, tcoct = 0, tcnov = 0, tcdec = 0;
        decimal Tejan = 0, tefeb = 0, temar = 0, teapr = 0, temay = 0, tejun = 0, tejul = 0, teaug = 0, tesep = 0, teoct = 0, tenov = 0, tedec = 0;
        decimal Tdjan = 0, tdfeb = 0, tdmar = 0, tdapr = 0, tdmay = 0, tdjun = 0, tdjul = 0, tdaug = 0, tdsep = 0, tdoct = 0, tdnov = 0, tddec = 0;
        DataSet dsTO = null;
        DataSet dsTOcnt = null;

        dsTO = od.get_order_valueSTK( Year, div_code);
        dsTOcnt = od.get_order_valueSTKcnt(Year, div_code);
        int cnt = 0;
        int cnt1 = 0;
        int cnt2 = 0;
        int j = 0;
        int i = 1;
        decimal Tjan1 = 0,  tfeb1 = 0, tmar1 = 0, tapr1 = 0, tmay1 = 0, tjun1 = 0, tjul1 = 0, taug1 = 0, tsep1 = 0, toct1 = 0, tnov1 = 0, tdec1 = 0;
        decimal Tejan1 = 0, tefeb1 = 0, temar1 = 0, teapr1 = 0, temay1 = 0, tejun1 = 0, tejul1 = 0, teaug1 = 0, tesep1 = 0, teoct1 = 0, tenov1 = 0, tedec1 = 0;
        decimal Tdjan1 = 0, tdfeb1 = 0, tdmar1 = 0, tdapr1 = 0, tdmay1 = 0, tdjun1 = 0, tdjul1 = 0, tdaug1 = 0, tdsep1 = 0, tdoct1 = 0, tdnov1 = 0, tddec1 = 0;
       
        int Tcjan1 = 0; int tcfeb1 = 0; int tcmar1 = 0; int tcapr1 = 0; int tcmay1 = 0; int tcjun1 = 0; int tcjul1 = 0; int tcaug1 = 0;
        int tcsep1 = 0; int tcoct1 = 0; int tcnov1 = 0; int tcdec1 = 0;
        decimal TOT_Ov1 = 0; decimal TOT_Oev1 = 0; decimal TOT_Odv1 = 0;
        int TOT_Ocv1 = 0;
        
        foreach (DataRow dr in dsTO.Tables[0].Rows)
               // for(int x=0; x<dsTO.Tables[0].Rows.Count;x++)
            {
                int cjan = dr["cjan"] == DBNull.Value ? 0 : Convert.ToInt16(dr["cjan"]);
                decimal jano = dr["jan"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["jan"]);
                int cfeb = dr["cfeb"] == DBNull.Value ? 0 : Convert.ToInt16(dr["cfeb"]);
                decimal febo = dr["feb"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["feb"]);
                int cmar = dr["cmar"] == DBNull.Value ? 0 : Convert.ToInt16(dr["cmar"]);
                decimal maro = dr["mar"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["mar"]);
                int capr = dr["capr"] == DBNull.Value ? 0 : Convert.ToInt16(dr["capr"]);
                decimal apro = dr["app"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["app"]);
                int cmay = dr["cmay"] == DBNull.Value ? 0 : Convert.ToInt16(dr["cmay"]);
                decimal mayo = dr["may"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["may"]);
                int cjun = dr["cjun"] == DBNull.Value ? 0 : Convert.ToInt16(dr["cjun"]);
                decimal juno = dr["june"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["june"]);
                int cjul = dr["cjul"] == DBNull.Value ? 0 : Convert.ToInt16(dr["cjul"]);
                decimal julo = dr["july"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["july"]);
                int caug = dr["caug"] == DBNull.Value ? 0 : Convert.ToInt16(dr["caug"]);
                decimal augo = dr["aguest"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["aguest"]);
                int csep = dr["csep"] == DBNull.Value ? 0 : Convert.ToInt16(dr["csep"]);
                decimal sepo = dr["sep"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["sep"]);
                int coct = dr["coct"] == DBNull.Value ? 0 : Convert.ToInt16(dr["coct"]);
                decimal octo = dr["oct"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["oct"]);
                int cnav = dr["cnov"] == DBNull.Value ? 0 : Convert.ToInt16(dr["cnov"]);
                decimal novo = dr["nav"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["nav"]);
                int cdec = dr["cdec"] == DBNull.Value ? 0 : Convert.ToInt16(dr["cdec"]);
                decimal deco = dr["dece"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["dece"]);


                decimal ejan = dr["ejan"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["ejan"]);
                decimal efeb = dr["efeb"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["efeb"]);
                decimal emar = dr["emar"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["emar"]);
                decimal eapr = dr["eapr"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["eapr"]);
                decimal emay = dr["emay"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["emay"]);
                decimal ejun = dr["ejun"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["ejun"]);
                decimal ejul = dr["ejul"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["ejul"]);
                decimal eaug = dr["eaug"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["eaug"]);
                decimal esep = dr["esep"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["esep"]);
                decimal eoct = dr["eoct"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["eoct"]);
                decimal enav = dr["enov"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["enov"]);
                decimal edec = dr["edec"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["edec"]);

                decimal djan = dr["djan"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["djan"]);
                decimal dfeb = dr["dfeb"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["dfeb"]);
                decimal dmar = dr["dmar"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["dmar"]);
                decimal dapr = dr["dapr"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["dapr"]);
                decimal dmay = dr["dmay"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["dmay"]);
                decimal djun = dr["djun"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["djun"]);
                decimal djul = dr["djul"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["djul"]);
                decimal daug = dr["daug"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["daug"]);
                decimal dsep = dr["dsep"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["dsep"]);
                decimal doct = dr["doct"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["doct"]);
                decimal dnav = dr["dnov"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["dnov"]);
                decimal ddec = dr["ddec"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["ddec"]);

            //Target_Dt.Rows.Add(Year, dr["sf_name"].ToString(), dr["sf_code"].ToString(), jant, jano.ToString("0.00"), jana.ToString("0.00"), febt, febo.ToString("0.00"), feba.ToString("0.00"), mart, maro.ToString("0.00"), mara.ToString("0.00"), aprt, apro.ToString("0.00"), apra.ToString("0.00"), mayt, mayo.ToString("0.00"), maya.ToString("0.00"), junt, juno.ToString("0.00"), juna.ToString("0.00"), jult, julo.ToString("0.00"), jula.ToString("0.00"), augt, augo.ToString("0.00"), auga.ToString("0.00"), sept, sepo.ToString("0.00"), sepa.ToString("0.00"), octt, octo.ToString("0.00"), octa.ToString("0.00"), novt, novo.ToString("0.00"), nova.ToString("0.00"), dect, deco.ToString("0.00"), deca.ToString("0.00"), TOT_T, TOT_O.ToString("0.00"), TOT_A.ToString("0.00"));


                decimal TOT_Ot = jano + febo + maro + apro + mayo + juno + julo + augo + sepo + octo + novo + deco;
                Tjan += jano; tfeb += febo; tmar += maro; tapr += apro; tmay += mayo; tjun += juno; 
                tjul += julo; taug += augo; tsep += sepo; toct += octo; tnov += novo; tdec += deco;


                int TOT_Oc = cjan + cfeb + cmar + capr + cmay + cjun + cjul + caug + csep + coct + cnav + cdec;
                Tcjan += cjan; tcfeb += cfeb; tcmar += cmar; tcapr += capr; tcmay += cmay; tcjun += cjun; 
                tcjul += cjul; tcaug += caug; tcsep += csep; tcoct += coct; tcnov += cnav; tcdec += cdec;

                decimal TOT_Oe = ejan + efeb + emar + eapr + emay + ejun + ejul + eaug + esep + eoct + enav + edec;
                Tejan += ejan; tefeb += efeb; temar += emar; teapr += eapr; temay += emay; tejun += ejun; 
                tejul += ejul; teaug += eaug; tesep += esep; teoct += eoct; tenov += enav; tedec += edec;

                decimal TOT_Od = djan + dfeb + dmar + dapr + dmay + djun + djul + daug + dsep + doct + dnav + ddec;
                Tdjan += djan; tdfeb += dfeb; tdmar += dmar; tdapr += dapr; tdmay += dmay; tdjun += djun; 
                tdjul += djul; tdaug += daug; tdsep += dsep; tdoct += doct; tdnov += dnav; tddec += ddec;
          
            foreach (DataRow dr1 in dsTOcnt.Tables[0].Rows)
                {
                     if (dr1["Sf_Code1"].ToString() == dr["sf_code"].ToString() && dr["Stockist_Code"].ToString() == dr1["Stockist_Code1"].ToString() )
                    {
                    cnt1 = Convert.ToInt32(dr1["cnt"]);
                    cnt += Convert.ToInt32(dr1["cnt"]);
                       Target_Dt.Rows.Add(dr["sf_code"].ToString(), dr["Sf_Name"].ToString(), dr["Stockist_Code"].ToString(), dr["Stockist_Name"].ToString(), 
                           dr1["Sf_Code1"].ToString(), dr1["Stockist_Code1"].ToString(), dr1["cnt"].ToString(),
                           cjan, ejan, djan, jano, cfeb, efeb, dfeb, febo, cmar, emar, dmar, maro, capr, eapr, dapr, apro, cmay, emay, dmay, mayo, cjun, ejun, djun, juno,
                           cjul, ejul, djul, julo, caug, eaug, daug, augo, csep, dsep, dsep, sepo, coct, eoct, doct, octo, cnav, dnav, dnav, novo, cdec, edec, ddec, deco, TOT_Oc, TOT_Oe, TOT_Od, TOT_Ot);
                    }

                }
            //decimal OVR_TOT_O = ovr_tot_jan_ord + ovr_tot_feb_ord + ovr_tot_mar_ord + ovr_tot_apr_ord + ovr_tot_may_ord + ovr_tot_jun_ord + ovr_tot_jul_ord + ovr_tot_aug_ord + ovr_tot_sep_ord + ovr_tot_oct_ord + ovr_tot_nov_ord + ovr_tot_dec_ord;
            Tjan1 += jano; tfeb1 += febo; tmar1 += maro; tapr1 += apro; tmay1 += mayo; tjun1 += julo; tjul1 += augo; taug1 += augo; tsep1 += sepo; toct1 += octo; tnov1 += novo; tdec1 += deco;
            Tcjan1 += cjan; tcfeb1 += cfeb; tcmar1 += cmar; tcapr1 += capr; tcmay1 += cmay; tcjun1 += cjun; tcjul1 += caug; tcaug1 += caug; tcsep1 += csep; tcoct1 += coct; tcnov1 += cnav; tcdec1 += cdec;
            Tejan1 += ejan; tefeb1 += efeb; temar1 += emar; teapr1 += eapr; temay1 += emay; tejun1 += ejun; tejul1 += ejul; teaug1 += eaug; tesep1 += esep; teoct1 += eoct; tenov1 += enav; tedec1 += edec;
            Tdjan1 += djan; tdfeb1 += dfeb; tdmar1 += dmar; tdapr1 += dapr; tdmay1 += dmay; tdjun1 += djun; tdjul1 += djul; tdaug1 += daug; tdsep1 += dsep; tdoct1 += doct; tdnov1 += dnav; tddec1 += ddec;

            TOT_Ov1 =    tfeb1 + tmar1 + tapr1 + tmay1 + tjun1 + tjul1 + taug1 + tsep1 + toct1 + tnov1 + tdec1;
            TOT_Ocv1 = Tcjan1 + tcfeb1 + tcmar1 + tcapr1 + tcmay1 + tcjun1 + tcjul1 + tcaug1 + tcsep1 + tcoct1 + tcnov1 + tcdec1;
            TOT_Ocv1 = Tcjan1 + tcfeb1 + tcmar1 + tcapr1 + tcmay1 + tcjun1 + tcjul1 + tcaug1 + tcsep1 + tcoct1 + tcnov1 + tcdec1;
            TOT_Ocv1 = Tcjan1 + tcfeb1 + tcmar1 + tcapr1 + tcmay1 + tcjun1 + tcjul1 + tcaug1 + tcsep1 + tcoct1 + tcnov1 + tcdec1;

            cnt2 += cnt1;


            if (dsTO.Tables[0].Rows.Count > i)
            {
                if (dsTO.Tables[0].Rows[j].ItemArray.GetValue(0).ToString() == dsTO.Tables[0].Rows[i].ItemArray.GetValue(0).ToString())
                {                    
                   
                }
                else
                {
                    Target_Dt.Rows.Add("", "Total", "", "", "", "", cnt2, Tcjan1, Tejan1, Tdjan1, Tjan1, tcfeb1, tefeb1, tdfeb1, tfeb1, tcmar1, temar1, tdmar1, tmar1,
                        tcapr1, teapr1, tdapr1, tapr1, tcmay1, temay1, tdmay1, tmay1, tcjun1, tejun1, tdjun1, tjun1,
                        tcjul1, tejul1, tdjul1, tjul1, tcaug1, teaug1, tdaug1, taug1, tcsep1, tesep1, tdsep1, tsep1,
                        tcoct1, teoct1, tdoct1, toct1, tcnov1, tenov1, tdnov1, tnov1, tcdec1, tedec1, tddec1, tdec1, TOT_Ocv1, TOT_Oev1, TOT_Odv1, TOT_Ov1);
                    Tjan1 = 0;  tfeb1 = 0;  tmar1 = 0;  tapr1 = 0;  tmay1 = 0;  tjun1 = 0;  tjul1 = 0;  taug1 = 0;
                     tsep1 = 0;  toct1 = 0;  tnov1 = 0;  tdec1 = 0;
                     Tcjan1 = 0; tcfeb1 = 0;  tcmar1 = 0;  tcapr1 = 0;  tcmay1 = 0;  tcjun1 = 0;  tcjul1 = 0;  tcaug1 = 0;
                     tcsep1 = 0; tcoct1 = 0; tcnov1 = 0; tcdec1 = 0;
                     Tejan1 = 0; tefeb1 = 0; temar1 = 0; teapr1 = 0; temay1 = 0; tejun1 = 0; tejul1 = 0; teaug1 = 0;
                     tesep1 = 0; teoct1 = 0; tenov1 = 0; tedec1 = 0;
                     Tdjan1 = 0; tdfeb1 = 0; tdmar1 = 0; tdapr1 = 0; tdmay1 = 0; tdjun1 = 0; tejul1 = 0; tdaug1 = 0;
                     tdsep1 = 0; tdoct1 = 0; tdnov1 = 0; tddec1 = 0;
                     cnt2 = 0; TOT_Ov1 = 0; TOT_Ocv1 = 0; TOT_Oev1 = 0; TOT_Odv1 = 0;
                }
            }
            else
            {
                TOT_Ov1 = Tjan1 + tfeb1 + tmar1 + tapr1 + tmay1 + tjun1 + tjul1 + taug1 + tsep1 + toct1 + tnov1 + tdec1;
                TOT_Ocv1 = Tcjan1 + tcfeb1 + tcmar1 + tcapr1 + tcmay1 + tcjun1 + tcjul1 + tcaug1 + tcsep1 + tcoct1 + tcnov1 + tcdec1;
                TOT_Oev1 = Tejan1 + tefeb1 + temar1 + teapr1 + temay1 + tejun1 + tejul1 + teaug1 + tesep1 + teoct1 + tenov1 + tedec1;
                TOT_Odv1 = Tdjan1 + tdfeb1 + tdmar1 + tdapr1 + tdmay1 + tdjun1 + tdjul1 + tdaug1 + tdsep1 + tdoct1 + tdnov1 + tddec1;

                Target_Dt.Rows.Add("", "Total", "", "", "", "", cnt2, Tcjan1, Tejan1, Tdjan1, Tjan1, tcfeb1, tefeb1, tdfeb1, tfeb1, tcmar1, temar1, tdmar1, tmar1,
                    tcapr1, teapr1, tdapr1, tapr1, tcmay1, temay1, tdmay1, tmay1, tcjun1, tejun1, tdjun1, tjun1,
                    tcjul1, tejul1, tdjul1, tjul1, tcaug1, teaug1, tdaug1, taug1, tcsep1, tesep1, tdsep1, tsep1,
                    tcoct1, teoct1, tdoct1, toct1, tcnov1, tenov1, tdnov1, tnov1, tcdec1, tedec1, tddec1, tdec1, TOT_Ocv1, TOT_Oev1, TOT_Odv1, TOT_Ov1);
                //Target_Dt.Rows.Add("", "Total","", "", "", "", cnt2, Tcjan1, Tjan1, tcfeb1, tfeb1, tcmar1, tmar1, tcapr1, tapr1, tcmay1, tmay1, tcjun1, tjun1, tcjul1, tjul1, tcaug1, taug1, tcsep1, tsep1, tcoct1, toct1, tcnov1, tnov1, tcdec1, tdec1, TOT_Ocv1, TOT_Ov1);
                Tjan1 = 0; tfeb1 = 0; tmar1 = 0; tapr1 = 0; tmay1 = 0; tjun1 = 0; tjul1 = 0; taug1 = 0;
                tsep1 = 0; toct1 = 0; tnov1 = 0; tdec1 = 0;
                Tcjan1 = 0; tcfeb1 = 0; tcmar1 = 0; tcapr1 = 0; tcmay1 = 0; tcjun1 = 0; tcjul1 = 0; tcaug1 = 0;
                tcsep1 = 0; tcoct1 = 0; tcnov1 = 0; tcdec1 = 0;
                Tejan1 = 0; tefeb1 = 0; temar1 = 0; teapr1 = 0; temay1 = 0; tejun1 = 0; tejul1 = 0; teaug1 = 0;
                tesep1 = 0; teoct1 = 0; tenov1 = 0; tedec1 = 0;
                Tdjan1 = 0; tdfeb1 = 0; tdmar1 = 0; tdapr1 = 0; tdmay1 = 0; tdjun1 = 0; tejul1 = 0; tdaug1 = 0;
                tdsep1 = 0; tdoct1 = 0; tdnov1 = 0; tddec1 = 0;
                cnt2 = 0; TOT_Ov1 = 0; TOT_Ocv1 = 0; TOT_Oev1 = 0; TOT_Odv1 = 0;
            }
            i++;
            j++;
        }             
       int cn = 0;
        decimal TOT_Ov = Tjan + tfeb + tmar + tapr + tmay + tjun + tjul + taug + tsep + toct + tnov + tdec;
        int TOT_Ocv = Tcjan + tcfeb + tcmar + tcapr + tcmay + tcjun + tcjul + tcaug + tcsep + tcoct + tcnov + tcdec;
        decimal TOT_Oev = Tejan + tefeb + temar + teapr + temay + tejun + tejul + teaug + tesep + teoct + tenov + tedec;
        decimal TOT_Odv = Tdjan + tdfeb + tdmar + tdapr + tdmay + tdjun + tdjul + tdaug + tdsep + tdoct + tdnov + tddec;
        Target_Dt.Rows.Add("", "Grand Total", "", "", "", "", cn, Tcjan, Tejan, Tdjan, Tjan, tcfeb, tefeb, tdfeb, tfeb, tcmar, temar, tdmar, tmar, tcapr, teapr, tdapr, tapr, tcmay, temay, tdmay, tmay,
            tcjun, tejun, tdjun, tjun, tcjul, tejul, tdjul, tjul, tcaug, teaug, tdaug, taug, tcsep, tesep, tdsep, tsep,
            tcoct, tdoct, tdoct, toct, tcnov, tenov, tdnov, tnov, tcdec, tedec, tddec, tdec, TOT_Ocv, TOT_Oev, TOT_Odv, TOT_Ov);
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
