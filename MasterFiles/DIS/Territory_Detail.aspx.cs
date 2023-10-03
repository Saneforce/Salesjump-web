using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
public partial class MasterFiles_Territory_Detail : System.Web.UI.Page
{
    #region "Declaration"
    DataSet dsTerritory = null;
    string sf_code = string.Empty;
    string Division_code = string.Empty;
    string Territory_Type = string.Empty;
    string Territory_Name = string.Empty;
    string Territory_SName = string.Empty;
    string Alias_Name = string.Empty;
    string terr_code = string.Empty;
    string Territory_Code = string.Empty;
    string dis_code = string.Empty;
    string dis_name = string.Empty;
    int iReturn = -1;
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        sf_code = Session["sf_code"].ToString();
        Division_code = Session["Division_Code"].ToString().Replace(",", "");
        Territory_Code = Request.QueryString["Territory_Code"];
        if (!Page.IsPostBack)
        {
            Session["backurl"] = "Territory.aspx";
          //  menu1.Title = this.Page.Title;
              txtRoutecode.Focus();
            if (Territory_Code != "" && Territory_Code != null)
            {
                Territory sd = new Territory();
                dsTerritory = sd.get_Territory(sf_code, Territory_Code, Division_code);
                if (dsTerritory.Tables[0].Rows.Count > 0)
                {
                    txtRoutecode.Text = dsTerritory.Tables[0].Rows[0].ItemArray.GetValue(0).ToString().Trim();
                    txtRouteName.Text = dsTerritory.Tables[0].Rows[0].ItemArray.GetValue(1).ToString().Trim();
                    txt_Target.Text = dsTerritory.Tables[0].Rows[0].ItemArray.GetValue(2).ToString().Trim();
                    txtMinProd.Text = dsTerritory.Tables[0].Rows[0].ItemArray.GetValue(3).ToString().Trim();
                  
                }
            }
       
           
        }
        if (Session["sf_type"].ToString() == "1")
        {
            terr_code = Convert.ToString(Request.QueryString["Territory_Code"]); ;
            sf_code = Session["sf_code"].ToString();
            UserControl_MR_Menu Usc_MR =
           (UserControl_MR_Menu)LoadControl("~/UserControl/MR_Menu.ascx");
            Divid.Controls.Add(Usc_MR);
            Usc_MR.Title = this.Page.Title;
            lblTerrritory.Text = "( " + "<span style='font-weight: bold;color:Maroon;'>DSM Name: " + Session["sfName"] + " </span>" + " )";
            ViewTerritory();
            Lab_DSM.Text = "( " + "<span style='font-weight: bold;color:Maroon;'>Distributor Name: " + dis_name + " </span>" + " )";
        }
        else
        {
            terr_code = Convert.ToString(Request.QueryString["Territory_Code"]); ;
            sf_code = Session["sf_code"].ToString();
            UserControl_DIS_Menu c3 =
                           (UserControl_DIS_Menu)LoadControl("~/UserControl/DIS_Menu.ascx");
            Divid.Controls.Add(c3);
            c3.Title = this.Page.Title;
            ViewTerritory();
            //menu1.Visible = false;
            Session["backurl"] = "Territory.aspx";
            lblTerrritory.Text = "( " + "<span style='font-weight: bold;color:Maroon;'>DSM Name: " + Session["sfName"] + " </span>" + " )";
            Lab_DSM.Text = "( " + "<span style='font-weight: bold;color:Maroon;'>Distributor Name: " + dis_name + " </span>" + " )";
        }



    }
    protected void btnSave_Click(object sender, EventArgs e)
    {

        
        string Route_code = txtRoutecode.Text;
        string Route_Name = txtRouteName.Text;
        string Target = txt_Target.Text;
        string min_prod = txtMinProd.Text;


        if (Territory_Code == null)
        {
            Territory terr = new Territory();
            if (iReturn < 0)
            {
                iReturn = terr.RecordAdd(Route_code, Route_Name, Territory_Type, Session["sf_code"].ToString(), Target, min_prod,Division_code);

                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Created Successfully');</script>");
                Clear();
            }

            else if (iReturn == -2)
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Route Code Already Exist');</script>");
            }
            else if (iReturn == -3)
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Route Name Already Exist');</script>");
            }
        }
        else
        {
            Territory dv = new Territory();
            int subdivcode = Convert.ToInt16(Territory_Code);
            iReturn = dv.RecordUpdate(Territory_Code,Route_code, Route_Name, Territory_Type, Session["sf_code"].ToString(), Target, min_prod, Division_code);
            if (iReturn > 0)
            {
               
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Updated Successfully');window.location='Territory.aspx';</script>");
            }
            else if (iReturn == -2)
            {

                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Route Code Already Exist');</script>");
                txtRoutecode.Focus();
            }
            else if (iReturn == -3)
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Route Name Already Exist');</script>");
                txtRouteName.Focus();
            }
        }


    }
    public void Clear()
    {
        txtRoutecode.Text = "";
        txtRouteName.Text = "";
        txt_Target.Text = "";
        txtMinProd.Text = "";

    }
    private void ViewTerritory()
    {

        Territory terr = new Territory();
        dsTerritory = terr.getTerritory_dm(sf_code);
        if (dsTerritory.Tables[0].Rows.Count > 0)
        {

            dis_name = dsTerritory.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
            Territory_Type = dsTerritory.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();

        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        Clear();
    }
}