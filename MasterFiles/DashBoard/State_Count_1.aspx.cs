using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;

public partial class User_Count : System.Web.UI.Page
{
    DataSet dsSalesForce = null;
    DataSet dsDoctor = null;
    DataSet dsTP = null;
    string div_code = string.Empty;
    string sf_code = string.Empty;
    //DataSet dsState = null;
    string Month = string.Empty;
    string Year = string.Empty;
    int count_tot = 0;
    int count_tot1 = 0;
    DataSet dsDivision = null;
    DataSet dsState = null;
    string state_cd = string.Empty;
    string sState = string.Empty;
    string[] statecd;
    protected void Page_Load(object sender, EventArgs e)
    {
     div_code = Session["division_code"].ToString();
     div_code = div_code.Trim(",".ToCharArray());
    //    sf_code = Session["sf_code"].ToString();

        if (!Page.IsPostBack)
        {
            FillState(div_code);
         
        }

    }
    private void FillState(string div_code)
    {
       
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
            dsSalesForce = st.getState_new(state_cd);
            DataTable dt=dsSalesForce.Tables[0];
            dt.Columns.RemoveAt(2);
            dt.Columns.RemoveAt(0);
            Grid_View.DataSource = dt;
            Grid_View.DataBind();
           

          
          

        }
    }

    
   
    
   
}