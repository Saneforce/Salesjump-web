using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;




using System.Data;
using Bus_EReport;

using System.Web.Services;
using System.Data.SqlClient;
using Newtonsoft.Json;

using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Web.Script.Serialization;
using System.IO;
using System.Text;
using System.Configuration;
using System.Globalization;

public partial class MasterFiles_Allowance_Types_Updation : System.Web.UI.Page
{

    #region "Declaration"
    DataSet dsTerritory = null;
    DataSet dsStockist = new DataSet();
    string sf_code = string.Empty;
    string sf_type = string.Empty;
    string Alow_Type = string.Empty;
    string stay_ply = string.Empty;
    string Terr_hq = string.Empty;
    string Territory_Name = string.Empty;
    string Territory_Code = string.Empty;
    string Territory_SName = string.Empty;
    string Target = string.Empty;
    string min_prod = string.Empty;
    string div_code = string.Empty;
    string dsm_code = string.Empty;
    string Route_Population = string.Empty;
    int i;
    int iReturn = -1;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int time;
    int iCnt = -1;
    #endregion
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

        div_code = Session["div_code"].ToString();
        if (!Page.IsPostBack)
        {
            Session["backurl"] = "Territory.aspx";
            Terr_hq = Request.QueryString["TerrHq"].ToString();
            lblterr.Text = "Territory Name : " + Request.QueryString["TerrHqName"].ToString();
            GetRoute(Terr_hq);

            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);

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

    private void GetRoute(string terr_hq)
    {
        Territory terr = new Territory();
        dsTerritory = terr.getTerritorySF(terr_hq, div_code);
        GrdRoute.DataSource = dsTerritory;
        GrdRoute.DataBind();
    }

    public class Allw_Data
    {
        public string Territory_Code { get; set; }
        public string Territory_Name { get; set; }
        public string Alow_Type { get; set; }
        public string stay_ply { get; set; }

    }
    [WebMethod(EnableSession = true)]
    public static string SaveDate(string data)
    {

        var items = JsonConvert.DeserializeObject<List<Allw_Data>>(data);
        int co = 0;
        for (int i = 0; i < items.Count; i++)
        {

            Territory terr = new Territory();
            int iReturn = terr.Allowance_type_updation(items[i].Territory_Code, items[i].Territory_Name, items[i].Alow_Type, items[i].stay_ply);
            co++;
        }
        if (co > 0)
        {
            return "Sucess";
        }
        else
        {
            return "Error";
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        int iReturn = -1;
        foreach (GridViewRow gridRow in GrdRoute.Rows)
        {
            Label Territorys_Code = (Label)gridRow.Cells[1].FindControl("lblTerritorys_Code");
            Territory_Code = Territorys_Code.Text.ToString();
            TextBox Territorys_Name = (TextBox)gridRow.Cells[1].FindControl("Territory_Code");
            Territory_Name = Territorys_Name.Text.ToString();
            DropDownList Territory_alwtype = (DropDownList)gridRow.Cells[1].FindControl("Territory_Type");
            Alow_Type = Territory_alwtype.SelectedValue.ToString();

            DropDownList Stay_Place = (DropDownList)gridRow.Cells[1].FindControl("Stay_Place");
            stay_ply = Stay_Place.SelectedValue.ToString();

            if ((Territory_Name.Trim().Length > 0))
            {
                Territory terr = new Territory();
                iReturn = terr.Allowance_type_updation(Territory_Code, Territory_Name, Alow_Type, stay_ply);
            }

        }

        if (iReturn > 0)
        {
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Created Successfully');</script>");
        }
        else if (iReturn == -2)
        {
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Name Already Exist');</script>");
        }

    }


    protected void Territory_Type_SelectedIndexChanged(object sender, EventArgs e)
    {
        //DropDownList ddl_status = (DropDownList)sender;
        //GridViewRow row = (GridViewRow)ddl_status.Parent.Parent;
        //int idx = row.RowIndex;
        //DropDownList Territory_Type = (DropDownList)row.Cells[0].FindControl("Territory_Type");
        //DropDownList Stay_Place = (DropDownList)row.Cells[0].FindControl("Stay_Place");
        //if (Territory_Type.SelectedValue.ToString() == "OS-EX")
        //{
        //    Stay_Place.Enabled = true;
        //}
    }

    protected void GrdRoute_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        Territory dv = new Territory();
        DropDownList Stay_Place;
        dsStockist = dv.getTerritory_OS(div_code, Terr_hq);
        foreach (GridViewRow gv in GrdRoute.Rows)
        {
            Stay_Place = (DropDownList)gv.FindControl("Stay_Place");
            Stay_Place.DataSource = dsStockist;
            Stay_Place.DataTextField = "Territory_Name";
            Stay_Place.DataValueField = "Territory_Code";
            Stay_Place.DataBind();

        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DropDownList ddlCountries = (e.Row.FindControl("Territory_Type") as DropDownList);
             string country = (e.Row.FindControl("lblTerritory_Type") as Label).Text == "" ? "0" : (e.Row.FindControl("lblTerritory_Type") as Label).Text;
            ddlCountries.Items.FindByValue(country).Selected = true;
        }

    }

}