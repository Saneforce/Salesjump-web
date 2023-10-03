using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using Bus_EReport;
using System.Drawing.Imaging;
using System.Configuration;
public partial class MasterFiles_Options_testing : System.Web.UI.Page
{
      string div_code = string.Empty;
    string sf_code = string.Empty;
    DataSet dsSalesForce = null;
    DataSet dsTerritory = null;
    DataSet dsListedDR = null;
    DataTable dtListedDR = null;
    DataTable dt;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int time;
    protected void Page_Load(object sender, EventArgs e)
    {
        //DataTable dt = new DataTable();
        //dt.Columns.AddRange(new DataColumn[3] { new DataColumn("Id", typeof(int)),
        //                new DataColumn("Name", typeof(string)),
        //                new DataColumn("Country",typeof(string)) });
        //dt.Rows.Add(1, "John Hammond", "United States");
        //dt.Rows.Add(2, "Mudassar Khan", "India");
        //dt.Rows.Add(3, "Suzanne Mathews", "France");
        //dt.Rows.Add(4, "Robert Schidner", "Russia");
        //GridView1.DataSource = dt;
        //GridView1.DataBind();
        //GridView2.DataSource = null;
        //GridView2.DataBind();
        div_code = Session["div_code"].ToString();
        if (!Page.IsPostBack)
        {
            menu1.Title = this.Page.Title;
            menu1.FindControl("btnBack").Visible = false;
            FillSalesForce();
            FillToSalesForce();
            ddlToFieldForce.Enabled = false;
            ddlToTerr.Enabled = false;
            
        }
    }
    protected override void OnLoadComplete(EventArgs e)
    {

        ServerEndTime = DateTime.Now;
        TrackPageTime();//It will give you page load time  
    }
    public void TrackPageTime()
    {
        TimeSpan serverTimeDiff = ServerEndTime.Subtract(ServerStartTime);
        time = serverTimeDiff.Minutes;

    } 
    private void FillSalesForce()
    {
        SalesForce sf = new SalesForce();
        dsSalesForce = sf.getFieldForce_Transfer(div_code);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            ddlFromFieldForce.DataTextField = "sf_name";
            ddlFromFieldForce.DataValueField = "sf_code";
            ddlFromFieldForce.DataSource = dsSalesForce;
            ddlFromFieldForce.DataBind();
        }
    }
    protected void ddlFromFieldForce_SelectedIndexChanged(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        Territory terr = new Territory();
        dsTerritory = terr.getTerritory_Transfer(ddlFromFieldForce.SelectedValue);
        if (dsTerritory.Tables[0].Rows.Count > 0)
        {
            ddlFromTerr.DataTextField = "Territory_Name";
            ddlFromTerr.DataValueField = "Territory_Code";
            ddlFromTerr.DataSource = dsTerritory;
            ddlFromTerr.DataBind();
        }
    }
    private void FillToSalesForce()
    {
        SalesForce sf = new SalesForce();
        dsSalesForce = sf.getFieldForce_Transfer(div_code);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            ddlToFieldForce.DataTextField = "sf_name";
            ddlToFieldForce.DataValueField = "sf_code";
            ddlToFieldForce.DataSource = dsSalesForce;
            ddlToFieldForce.DataBind();
        }
    }
    protected void rdotransfer_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ddlToFieldForce_SelectedIndexChanged(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        Territory terr = new Territory();
        dsTerritory = terr.getTerritory_Transfer(ddlToFieldForce.SelectedValue);
        if (dsTerritory.Tables[0].Rows.Count > 0)
        {
            ddlToTerr.DataTextField = "Territory_Name";
            ddlToTerr.DataValueField = "Territory_Code";
            ddlToTerr.DataSource = dsTerritory;
            ddlToTerr.DataBind();
        }
    }
    //private void FirstGrid()
    //{
    //    ListedDR lstdr = new ListedDR();
    //    dsListedDR = lstdr.getListedDrforTerr_Camp(ddlFromFieldForce.SelectedValue, ddlFromTerr.SelectedValue);
    //    if (dsListedDR.Tables[0].Rows.Count > 0)
    //    {
    //        GridView1.Visible = true;
    //        GridView1.DataSource = dsListedDR;
    //        GridView1.DataBind();
    //     //   dt = dsListedDR.Tables[0];
    //    }
    //    else
    //    {
    //        GridView1.DataSource = dsListedDR;
    //        GridView1.DataBind();
    //    }
    //}
    protected void ddlFromTerr_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlToFieldForce.Enabled = true;
        ddlToTerr.Enabled = true;
        System.Threading.Thread.Sleep(time);
        ListedDR lstdr = new ListedDR();
        dsListedDR = lstdr.getListedDrforTerr_Camp(ddlFromFieldForce.SelectedValue, ddlFromTerr.SelectedValue);
        if (dsListedDR.Tables[0].Rows.Count > 0)
        {
            btnTransfer.Visible = true;
            btnClear.Visible = true;
            GridView1.Visible = true;
            GridView1.DataSource = dsListedDR;
            GridView1.DataBind();
            //   dt = dsListedDR.Tables[0];
            GridView2.DataSource = null;
            GridView2.DataBind();
        }
        else
        {
            GridView1.DataSource = dsListedDR;
            GridView1.DataBind();
        }
    }


    protected void ddlToTerr_SelectedIndexChanged(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        ListedDR lstdr = new ListedDR();
        int iReturn = lstdr.RecordCount_Transfer(ddlToFieldForce.SelectedValue, ddlToTerr.SelectedValue);


        lblCount.Text = "Total Listed Doctor Count:" + iReturn.ToString();

    }

    protected void btnTransfer_Click(object sender, EventArgs e)
    {

    }
    protected void btnClear_Click(object sender, EventArgs e)
    {

    }
}