using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
using System.Drawing;
using System.Data.SqlClient;
using System.Configuration;

public partial class MasterFiles_Calendar_Consolidated : System.Web.UI.Page
{
    DataSet dsHoliday = null;
    string div_code = string.Empty;
    string sf_code = string.Empty;
    string state_code = string.Empty;
    DataSet dsState = null;   
    string sState = string.Empty;
    string slno;   
    string state_cd = string.Empty;
    string[] statecd;
    DataSet dsDivision = null;
    DataSet dsTP = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        State st = new State();
        div_code = Session["div_code"].ToString();
        sf_code = Session["sf_code"].ToString();

        dsHoliday = st.getState();
        if (dsHoliday.Tables[0].Rows.Count > 0)
        {
            state_code = dsHoliday.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
        }
        if (!Page.IsPostBack)
        {
           // menu1.Title = this.Page.Title;
          //  menu1.FindControl("btnBack").Visible = false;
          //  GetData();
            TourPlan tp = new TourPlan();
            dsTP = tp.Get_TP_Edit_Year(div_code);
            if (dsTP.Tables[0].Rows.Count > 0)
            {
                for (int k = Convert.ToInt16(dsTP.Tables[0].Rows[0]["Year"]); k <= DateTime.Now.Year + 1; k++)
                {
                    ddlYear.Items.Add(k.ToString());
                    ddlYear.SelectedValue = DateTime.Now.Year.ToString();
                }
            }
            FillState(div_code);
        }
        if (Session["sf_type"].ToString() == "3" || Session["sf_type"].ToString() == null || Session["sf_type"].ToString() == "")
        {

            UserControl_MenuUserControl c1 =
                (UserControl_MenuUserControl)LoadControl("~/UserControl/MenuUserControl.ascx");
            Divid.Controls.Add(c1);
            c1.FindControl("btnBack").Visible = false;
            c1.Title = this.Page.Title;

        }
        else if (Session["sf_type"].ToString() == "2")
        {
            UserControl_MGR_Menu c1 =
            (UserControl_MGR_Menu)LoadControl("~/UserControl/MGR_Menu.ascx");
            Divid.Controls.Add(c1);
            c1.FindControl("btnBack").Visible = false;
            c1.Title = this.Page.Title;


        }


    }
    private void FillState(string div_code)
    {
        //Holiday st = new Holiday();
        //dsState = st.getState(div_code);
        //ddlState.DataTextField = "statename";
        //ddlState.DataValueField = "statecode";
        //ddlState.DataSource = dsState;
        //ddlState.DataBind();
        Division dv = new Division();
        dsDivision = dv.getStatePerDivision(div_code);
        if (dsDivision.Tables[0].Rows.Count > 0)
        {
            int i = 0;
            state_cd = string.Empty;
            sState = dsDivision.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
            statecd = sState.Split(',');
            foreach (string st_cd in statecd)
            {
                if (i == 0)
                {
                    state_cd = state_cd + st_cd;
                }
                else
                {
                    if (st_cd.Trim().Length > 0)
                    {
                        state_cd = state_cd + "," + st_cd;
                    }
                }
                i++;
            }

            State st = new State();
            dsState = st.getSt(state_cd);
            string[] stateCount;
            stateCount = state_cd.Split(',');
            for (int j = 0; j < dsState.Tables[0].Rows.Count; j++)
            {
                GridView gv = new GridView();
                gv.CssClass = "aclass";
                gv.Attributes.Add("class", "aclass");
               
                Label lbl = new Label();
                lbl.CssClass = "lbl";
                lbl.Attributes.Add("class", "lbl");
                DataSet ds = null;
                if (dsState.Tables[0].Rows.Count > 0)
                {
                    lbl.Text = dsState.Tables[0].Rows[j]["StateName"].ToString();
                    state_code = dsState.Tables[0].Rows[j]["State_Code"].ToString();
                    Holiday hol = new Holiday();
                    
                    foreach (string st1 in stateCount)
                    {
                        if (Convert.ToString(state_code) == st1)
                        {
                            ds = hol.getHolidays_Consol(st1, div_code, ddlYear.SelectedValue);
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                gv.DataSource = ds;
                                gv.DataBind();
                            }
                            
                        }
                      
                    }
                   
                }
                  if (ds.Tables[0].Rows.Count > 0)
                {
                    lbl.Attributes.Add("align", "center");
                    pnl.Controls.Add(lbl);
                    pnl.Controls.Add(gv);
                }
                else
                {
                   // lblNoRecord.Visible = true;

                }
               
            }
           

        }
    }

    protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillState(div_code);
    }
}