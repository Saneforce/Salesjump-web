using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;

public partial class MIS_Reports_Target_Achievement_Report_New : System.Web.UI.Page
{
    string div_code = string.Empty;
    DataSet dsTP = null;
    string tot_dr = string.Empty;
    string tot_FWD = string.Empty;
    string tot_dcr_dr = string.Empty;
    string sCurrentDate = string.Empty;
    string sf_code = string.Empty;
    string sf_type = string.Empty;
    string sf_name = string.Empty;
    string MultiSf_Code = string.Empty;
    DateTime ServerStartTime;
    DataSet dsSalesForce = null;
    protected override void OnPreInit(EventArgs e)
    {
        base.OnPreInit(e);
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
    protected void Page_Load(object sender, EventArgs e)
    {
        sf_code = Session["sf_code"].ToString();
        sf_type = Session["sf_type"].ToString();
        sf_name = Session["sf_name"].ToString();
        if (sf_type == "3")
        {
            div_code = Session["div_code"].ToString();
        }
        else
        {
            div_code = Session["div_code"].ToString();
        }
        if (!Page.IsPostBack)
        {
            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);
            FillYear();
            fillsubdivision();

        }
    }


    private void FillYear()
    {
        TourPlan tp = new TourPlan();
        dsTP = tp.Get_TP_Edit_Year(div_code);
        if (dsTP.Tables[0].Rows.Count > 0)
        {
            for (int k = Convert.ToInt16(dsTP.Tables[0].Rows[0]["Year"]); k <= DateTime.Now.Year + 1; k++)
            {
                 ddlFYear.Items.Add(k.ToString());
                ddlFYear.SelectedValue = DateTime.Now.Year.ToString();

            }
        }
    }

    private void fillsubdivision()
    {
        SalesForce sd = new SalesForce();
        dsSalesForce = sd.Getsubdivisionwise(div_code);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            subdiv.DataTextField = "subdivision_name";
            subdiv.DataValueField = "subdivision_code";
            subdiv.DataSource = dsSalesForce;
            subdiv.DataBind();
            subdiv.Items.Insert(0, new ListItem("--Select--", "0"));

        }
    }
    protected void btnGo_Click(object sender, EventArgs e)
    {


        Order od = new Order();
        DataSet dsTO = od.get_target_order(div_code,subdiv.SelectedValue.ToString(), ddlFYear.SelectedValue.ToString(),sf_code);

        DataTable Target_Dt = new DataTable();
        Target_Dt.Columns.Add("Year", typeof(string));
        Target_Dt.Columns.Add("Name", typeof(string));
        Target_Dt.Columns.Add("SF_CODE", typeof(string));
        Target_Dt.Columns.Add("JAN_TV", typeof(decimal));
        Target_Dt.Columns.Add("JAN_OV", typeof(decimal));
        Target_Dt.Columns.Add("JAN_ACH", typeof(decimal));

        Target_Dt.Columns.Add("FEB_TV", typeof(decimal));
        Target_Dt.Columns.Add("FEB_OV", typeof(decimal));
        Target_Dt.Columns.Add("FEB_ACH", typeof(decimal));

        Target_Dt.Columns.Add("MAR_TV", typeof(decimal));
        Target_Dt.Columns.Add("MAR_OV", typeof(decimal));
        Target_Dt.Columns.Add("MAR_ACH", typeof(decimal));

        Target_Dt.Columns.Add("APR_TV", typeof(decimal));
        Target_Dt.Columns.Add("APR_OV", typeof(decimal));
        Target_Dt.Columns.Add("APR_ACH", typeof(decimal));

        Target_Dt.Columns.Add("MAY_TV", typeof(decimal));
        Target_Dt.Columns.Add("MAY_OV", typeof(decimal));
        Target_Dt.Columns.Add("MAY_ACH", typeof(decimal));

        Target_Dt.Columns.Add("JUN_TV", typeof(decimal));
        Target_Dt.Columns.Add("JUN_OV", typeof(decimal));
        Target_Dt.Columns.Add("JUN_ACH", typeof(decimal));

        Target_Dt.Columns.Add("JUL_TV", typeof(decimal));
        Target_Dt.Columns.Add("JUL_OV", typeof(decimal));
        Target_Dt.Columns.Add("JUL_ACH", typeof(decimal));

        Target_Dt.Columns.Add("AUG_TV", typeof(decimal));
        Target_Dt.Columns.Add("AUG_oV", typeof(decimal));
        Target_Dt.Columns.Add("AUG_ACH", typeof(decimal));

        Target_Dt.Columns.Add("SEP_TV", typeof(decimal));
        Target_Dt.Columns.Add("SEP_OV", typeof(decimal));
        Target_Dt.Columns.Add("SEP_ACH", typeof(decimal));

        Target_Dt.Columns.Add("OCT_TV", typeof(decimal));
        Target_Dt.Columns.Add("OCT_OV", typeof(decimal));
        Target_Dt.Columns.Add("OCT_ACH", typeof(decimal));

        Target_Dt.Columns.Add("NOV_TV", typeof(decimal));
        Target_Dt.Columns.Add("NOV_OV", typeof(decimal));
        Target_Dt.Columns.Add("NOV_ACH", typeof(decimal));

        Target_Dt.Columns.Add("DEC_TV", typeof(decimal));
        Target_Dt.Columns.Add("DEC_OV", typeof(decimal));
        Target_Dt.Columns.Add("DEC_ACH", typeof(decimal));

        Target_Dt.Columns.Add("TOT_TV", typeof(decimal));
        Target_Dt.Columns.Add("TOT_OV", typeof(decimal));
        Target_Dt.Columns.Add("TOT_ACH", typeof(decimal));



        foreach (DataRow dr in dsTO.Tables[0].Rows)
        {

            decimal jant = dr["January_target_val"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["January_target_val"]);
            decimal jano = dr["January_order_val"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["January_order_val"]);
            decimal jana = jant > 0 ? (jano / jant * 100) : 0;


            decimal febt = dr["February_target_val"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["February_target_val"]);
            decimal febo = dr["February_order_val"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["February_order_val"]);
            decimal feba = febt > 0 ? (febo / febt * 100) : 0;

            decimal mart = dr["March_target_val"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["March_target_val"]);
            decimal maro = dr["March_order_val"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["March_order_val"]);
            decimal mara = mart > 0 ? (maro / mart * 100) : 0;

            decimal aprt = dr["April_target_val"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["April_target_val"]);
            decimal apro = dr["April_order_val"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["April_order_val"]);
            decimal apra = aprt > 0 ? (apro / aprt * 100) : 0;

            decimal mayt = dr["May_target_val"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["May_target_val"]);
            decimal mayo = dr["May_order_val"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["May_order_val"]);
            decimal maya = mayt > 0 ? (mayo / mayt * 100) : 0;

            decimal junt = dr["June_target_val"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["June_target_val"]);
            decimal juno = dr["June_order_val"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["June_order_val"]);
            decimal juna = junt > 0 ? (juno / junt * 100) : 0;

            decimal jult = dr["July_target_val"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["July_target_val"]);
            decimal julo = dr["July_order_val"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["July_order_val"]);
            decimal jula = jult > 0 ? (julo / jult * 100) : 0;

            decimal augt = dr["August_target_val"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["August_target_val"]);
            decimal augo = dr["August_order_val"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["August_order_val"]);
            decimal auga = augt > 0 ? (augo / augt * 100) : 0;

            decimal sept = dr["September_target_val"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["September_target_val"]);
            decimal sepo = dr["September_order_val"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["September_order_val"]);
            decimal sepa = sept > 0 ? (sepo / sept * 100) : 0;

            decimal octt = dr["October_target_val"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["October_target_val"]);
            decimal octo = dr["October_order_val"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["October_order_val"]);
            decimal octa = octt > 0 ? (octo / octt * 100) : 0;

            decimal novt = dr["November_target_val"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["November_target_val"]);
            decimal novo = dr["November_order_val"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["November_order_val"]);
            decimal nova = novt > 0 ? (novo / novt * 100) : 0;

            decimal dect = dr["December_target_val"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["December_target_val"]);
            decimal deco = dr["December_order_val"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["December_order_val"]);
            decimal deca = dect > 0 ? (deco / dect * 100) : 0;

            decimal TOT_T = jant + febt + mart + aprt + mayt + junt + jult + augt + sept + octt + novt + dect;
            decimal TOT_O = jano + febo + maro + apro + mayo + juno + julo + augo + sepo + octo + novo + deco;
            decimal TOT_A = TOT_T > 0 ? (TOT_O / TOT_T * 100) : 0;

            Target_Dt.Rows.Add(ddlFYear.SelectedValue.ToString(),"Over All",sf_code.ToString(), jant, jano.ToString("0.00"), jana.ToString("0.00"), febt, febo.ToString("0.00"), feba.ToString("0.00"), mart, maro.ToString("0.00"), mara.ToString("0.00"), aprt, apro.ToString("0.00"), apra.ToString("0.00"), mayt, mayo.ToString("0.00"), maya.ToString("0.00"), junt, juno.ToString("0.00"), juna.ToString("0.00"), jult, julo.ToString("0.00"), jula.ToString("0.00"), augt, augo.ToString("0.00"), auga.ToString("0.00"), sept, sepo.ToString("0.00"), sepa.ToString("0.00"), octt, octo.ToString("0.00"), octa.ToString("0.00"), novt, novo.ToString("0.00"), nova.ToString("0.00"), dect, deco.ToString("0.00"), deca.ToString("0.00"), TOT_T, TOT_O.ToString("0.00"), TOT_A.ToString("0.00"));
        }

        GVData.DataSource = Target_Dt;
        GVData.DataBind();
    }
    
   

}