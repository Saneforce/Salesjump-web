using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;

public partial class MIS_Reports_Secondary_Sales_View : System.Web.UI.Page
{
    string div_code = string.Empty;
    DataSet dsSalesForce = null;
    DataSet dsUserList = null;
    DataSet dsDoc = null;
    DataSet dsDCR = null;
    int product_total = 0;
    int FWD_total = 0;
    DateTime dtCurrent;
    DataSet dsProduct = null;
    DataSet dsCatg = null;
    int MonColspan = 0;
    DataSet dsTP = null;
    string tot_dr = string.Empty;  
    string sCurrentDate = string.Empty;
    string sf_code = string.Empty;
    DataSet dsSf = null;
    string sf_type = string.Empty;   
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    protected void Page_Load(object sender, EventArgs e)
    {
        sf_code = Session["sf_code"].ToString();
        sf_type = Session["sf_type"].ToString();

        if (sf_type == "3")
        {
            div_code = Session["Division_Code"].ToString().Replace(",", "");
        }
        else
        {
            div_code = Session["Division_Code"].ToString().Replace(",", "");
        }
        if (!Page.IsPostBack)
        {
            menu1.Title = this.Page.Title;
            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);
            menu1.FindControl("btnBack").Visible = false;

            FillYear();
         fillsubdivision();
         filldistributor();
         
        }

    }
 private void fillsubdivision()
    {
        SalesForce sd = new SalesForce();
        dsSalesForce = sd.DISGetsubdivisionwise(sf_code,div_code);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            subdiv.DataTextField = "subdivision_name";
            subdiv.DataValueField = "subdivision_code";
            subdiv.DataSource = dsSalesForce;
            subdiv.DataBind();
          //subdiv.Items.Insert(0, new ListItem("--Select--", "0"));
            filldistributor();
        }

    }
 protected void subdiv_SelectedIndexChanged(object sender, EventArgs e)
    {

filldistributor();

    }
  private void filldistributor()
    {
        
        SalesForce sd = new SalesForce();
        dsSalesForce = sd.DISGetStockist_subdivisionwise(div_code, sf_code);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            Distributor.DataTextField = "Stockist_Name";
            Distributor.DataValueField = "Stockist_code";
            Distributor.DataSource = dsSalesForce;
            Distributor.DataBind();
            //Distributor.Items.Insert(0, new ListItem("--Select--", "0"));

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
                //ddlTYear.Items.Add(k.ToString());
                ddlFYear.SelectedValue = DateTime.Now.Year.ToString();
                //ddlTYear.SelectedValue = DateTime.Now.Year.ToString();
            }
        }

        ddlFMonth.SelectedValue = DateTime.Now.Month.ToString();


    }
    protected void btnGo_Click(object sender, EventArgs e)
    {

        int FYear = Convert.ToInt32(ddlFYear.SelectedValue);

        int FMonth = Convert.ToInt32(ddlFMonth.SelectedValue);




        string sURL = string.Empty;

      
        sURL = "rpt_Secondary_Sales_View.aspx?&FMonth=" + ddlFMonth.SelectedValue.ToString() + "&FYear=" + ddlFYear.SelectedValue.ToString()+"&Dist_Name="+Distributor.SelectedItem.Text+"&subdivision="+subdiv.SelectedValue.ToString()+"&Distributor="+Distributor.SelectedValue.ToString();

        string newWin = "window.open('" + sURL + "',null,'resizable=yes,toolbar=no,scrollbars=yes,menubar=no,status=no,width=800,height=600,left=0,top=0');";
         ClientScript.RegisterStartupScript(this.GetType(), "pop", newWin, true);
            }
        }
    
