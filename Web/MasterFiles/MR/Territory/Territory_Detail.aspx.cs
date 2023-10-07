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
    DataSet dsStockist = null;
    DataSet dsdistributor = null;
    DataSet dsTerritory = null;
    DataSet dsTerritory1 = null;
    DataSet dsTerritory2 = null;
    string sf_code = string.Empty;
    string sf_type = string.Empty;
    string Division_code = string.Empty;
    string Territory_Type = string.Empty;
    string Territory_Name = string.Empty;
    string Territory_SName = string.Empty;
    string Alias_Name = string.Empty;
    string terr_code = string.Empty;
    string Territory_Code = string.Empty;
    string dis_code = string.Empty;
    string dis_name = string.Empty;
    string distributor_name = string.Empty;
    string sf_code_name = string.Empty;
    int iIndex = -1;
    int iReturn = -1;
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
        sf_code = Session["sf_code"].ToString();
        Division_code = Session["Div_code"].ToString();
        Territory_Code = Request.QueryString["Territory_Code"];
	
        if (!Page.IsPostBack)
        {
            if(Division_code=="32" || Division_code=="43" ||Division_code=="48")
                Session["backurl"] = "Territory.aspx";
            else
                Session["backurl"] = "../../Route_Master.aspx";
            //  menu1.Title = this.Page.Title;
            ViewDSM();
            GetTerritoryName();
            FillCheckBoxList1();
            txtRoutecode.Focus();
			FillRouteTown();
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
                    txtRoutePopulation.Text = dsTerritory.Tables[0].Rows[0].ItemArray.GetValue(4).ToString().Trim();
                    //ddldsm.SelectedValue = dsTerritory.Tables[0].Rows[0].ItemArray.GetValue(5).ToString();
                    ddlTerritoryName.SelectedValue = dsTerritory.Tables[0].Rows[0].ItemArray.GetValue(6).ToString();
                    distributor_name = dsTerritory.Tables[0].Rows[0].ItemArray.GetValue(7).ToString();
                    sf_code_name = dsTerritory.Tables[0].Rows[0].ItemArray.GetValue(5).ToString();

                    string st = dsTerritory.Tables[0].Rows[0].ItemArray.GetValue(8).ToString();
                   string ddlst = dsTerritory.Tables[0].Rows[0].ItemArray.GetValue(9).ToString();

                    int iCount = 0, iIndex;
                    foreach (ListItem item in DDL_aw_Type.Items)
                    {
                        if (st == item.ToString())
                        {
                            iIndex = iCount;
                            DDL_aw_Type.SelectedIndex = iIndex;
                            break;
                        }
                        iCount++;
                    }

                    ddlTerritoryName_SelectedIndexChanged(sender, e);
                    DDL_aw_Type_SelectedIndexChanged(sender,e);
                    // ddldsm.DataSource = dsTerritory1;
                    // ddldsm.DataTextField = "DSM_name";
                    // ddldsm.DataValueField = "DSM_code";
                    // ddldsm.DataBind();

                    DDLStay.SelectedValue = ddlst;

                }
            }
            else
            {

                //ddlTerritoryName.SelectedValue = Session["Terr_code_ms"].ToString();
                ddlTerritoryName_SelectedIndexChanged(sender, e);
            }
			

        }
        if (Session["sf_type"].ToString() == "1")
        {
            terr_code = Convert.ToString(Request.QueryString["Territory_Code"]); ;
            sf_code = Session["sf_code"].ToString();
            // UserControl_MR_Menu Usc_MR =
            //(UserControl_MR_Menu)LoadControl("~/UserControl/MR_Menu.ascx");
            // Divid.Controls.Add(Usc_MR);
            // Usc_MR.Title = this.Page.Title;
            lblTerrritory.Text = "( " + "<span style='font-weight: bold;color:Maroon;'>DSM Name: " + Session["sfName"] + " </span>" + " )";
            ViewTerritory();
            Lab_DSM.Text = "( " + "<span style='font-weight: bold;color:Maroon;'>Distributor Name: " + dis_name + " </span>" + " )";
        }
        else
        {
            terr_code = Convert.ToString(Request.QueryString["Territory_Code"]); ;
            sf_code = Session["sf_code"].ToString();
            //UserControl_MenuUserControl c1 =
            // (UserControl_MenuUserControl)LoadControl("~/UserControl/MenuUserControl.ascx");
            //Divid.Controls.Add(c1);
            //c1.Title = this.Page.Title;
            ViewTerritory();
            //menu1.Visible = false;
            if(Division_code=="32" || Division_code=="43" ||Division_code=="48")
                Session["backurl"] = "Territory.aspx";
            else
                Session["backurl"] = "../../Route_Master.aspx";
            lblTerrritory.Text = "( " + "<span style='font-weight: bold;color:Maroon;'>DSM Name: " + Session["sfName"] + " </span>" + " )";
            Lab_DSM.Text = "( " + "<span style='font-weight: bold;color:Maroon;'>Distributor Name: " + dis_name + " </span>" + " )";
        }



    }
    protected void btnSave_Click(object sender, EventArgs e)
    {


        string Route_code = txtRoutecode.Text.Trim();
        string Route_Name = txtRouteName.Text.Trim();
        string Target = txt_Target.Text.Trim();
        string min_prod = txtMinProd.Text.Trim();
        string Route_population = txtRoutePopulation.Text.Trim();
        string territory_id = ddlTerritoryName.SelectedValue.ToString();
        string dist_name = "";
        string sf_code = "";
		string route_town = ddl_town.SelectedValue.ToString();													  

        foreach (ListItem item in chkboxLocation.Items)
        {
            if (item.Selected)
            {
                dist_name += item.Value + ",";
            }
        }

        dist_name = dist_name.TrimEnd(',');


        foreach (ListItem item in DDL_FO.Items)
        {
            if (item.Selected)
            {
                sf_code += item.Value + ",";
            }
        }
        sf_code = sf_code.TrimEnd(',');
        string dstay = "0";
        if ( DDLStay.SelectedValue.ToString() != "0")
        {
            dstay = DDLStay.SelectedValue.ToString();
        }
        if (sf_code != "")
        {
			if (Route_Name != "")
            {


            if (Territory_Code == null)
            {
                Territory terr = new Territory();

                iReturn = terr.RecordAdd(Route_code, Route_Name, Territory_Type, sf_code, Target, min_prod, Division_code, Route_population, territory_id, dist_name, DDL_aw_Type.SelectedItem.ToString(), dstay, route_town);
                if (iReturn > 0)
                {
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Created Successfully');</script>");
                    Clear();
					ddlTerritoryName_SelectedIndexChanged(sender, e);
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
                //int subdivcode = Convert.to(Territory_Code);

                iReturn = dv.RecordUpdate(Territory_Code, Route_code, Route_Name, Territory_Type, sf_code, Target, min_prod, Division_code, Route_population, territory_id, dist_name, DDL_aw_Type.SelectedItem.ToString(), dstay, route_town);
                if (iReturn > 0)
                {

                     if(Division_code=="32" || Division_code=="43" ||Division_code=="48")
                        ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Updated Successfully');window.location='Territory.aspx';</script>");
                    else
						ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Updated Successfully');window.location='../../Route_Master.aspx';</script>");
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
			else
            {
                if (Route_Name == "")
                {
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Enter Route Name...');</script>");
                    txtRouteName.Focus();
                }
            }
        }
        else
        {
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Select Field Force');</script>");
        }


    }
    public void Clear()
    {
        txtRoutecode.Text = "";
        txtRouteName.Text = "";
        txt_Target.Text = "0";
        txtMinProd.Text = "0";
        txtRoutePopulation.Text = "0";
        ddldsm.SelectedIndex = 0;
        ddlTerritoryName.SelectedIndex = -1;
        chkboxLocation.SelectedIndex = -1;
        Panel2.Visible = false;
        DDL_aw_Type.SelectedIndex = 1;

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
    private void ViewDSM()
    {
        Territory terr = new Territory();
        dsTerritory1 = terr.getSF_Code(sf_code);
        if (dsTerritory1.Tables[0].Rows.Count > 0)
        {
            ddldsm.DataTextField = "DSM_name";
            ddldsm.DataValueField = "DSM_code";
            ddldsm.DataSource = dsTerritory1;
            ddldsm.DataBind();
        }
    }
    private void EditDSM()
    {
        Territory terr = new Territory();
        dsTerritory2 = terr.getDSM_Code(ddldsm.SelectedValue);
        if (dsTerritory2.Tables[0].Rows.Count > 0)
        {
            ddldsm.DataTextField = "DSM_name";
            ddldsm.DataValueField = "DSM_code";
            ddldsm.DataSource = dsTerritory2;
            ddldsm.DataBind();
        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        Clear();
		ddlTerritoryName_SelectedIndexChanged(sender, e);
    }
    protected void chkboxLocation_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    private void GetTerritoryName()
    {
        // Stockist sk = new Stockist();
        // dsStockist = sk.getTer_Name(Division_code);
        Territorys dv = new Territorys();
        dsStockist = dv.TerritorygetSF_Code(Division_code, sf_code);
        if (dsStockist.Tables[0].Rows.Count > 0)
        {
            ddlTerritoryName.DataTextField = "Territory_name";
            ddlTerritoryName.DataValueField = "Territory_code";

            ddlTerritoryName.DataSource = dsStockist;
            ddlTerritoryName.DataBind();
        }

    }
    private void FillCheckBoxList1()
    {

        SubDivision dv = new SubDivision();
        dsdistributor = dv.get_distributor(ddlTerritoryName.SelectedValue);
        chkboxLocation.DataTextField = "Stockist_Name";
        chkboxLocation.DataValueField = "Stockist_Code";
        chkboxLocation.DataSource = dsdistributor;
        chkboxLocation.DataBind();

        string[] subdiv;
        if (distributor_name != "")
        {
            iIndex = -1;
            subdiv = distributor_name.Split(',');
            foreach (string st in subdiv)
            {
                for (iIndex = 0; iIndex < chkboxLocation.Items.Count; iIndex++)
                {
                    if (st == chkboxLocation.Items[iIndex].Value)
                    {
                        chkboxLocation.Items[iIndex].Selected = true;
                        chkboxLocation.Items[iIndex].Attributes.Add("style", "Color: #8A2BE2;font-weight:Bold");
                    }
                }
            }
        }

        Stockist sk = new Stockist();
        dsStockist = sk.FOName(ddlTerritoryName.SelectedValue, Division_code);
        DDL_FO.DataTextField = "Sf_Name";
        DDL_FO.DataValueField = "Sf_Code";
        DDL_FO.DataSource = dsStockist;
        DDL_FO.DataBind();


        if (sf_code_name != "")
        {
            iIndex = -1;
            subdiv = sf_code_name.Split(',');
            foreach (string st in subdiv)
            {
                for (iIndex = 0; iIndex < DDL_FO.Items.Count; iIndex++)
                {
                    if (st == DDL_FO.Items[iIndex].Value)
                    {
                        DDL_FO.Items[iIndex].Selected = true;
                        DDL_FO.Items[iIndex].Attributes.Add("style", "Color: #8A2BE2;font-weight:Bold");
                    }
                }
            }
        }



    }
    protected void ddlTerritoryName_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlTerritoryName.SelectedValue != "0")
        {
            FillCheckBoxList1();
            Panel2.Visible = true;
            if (DDL_aw_Type.SelectedValue == "4")
            {

                Load_staypln(ddlTerritoryName.SelectedValue.ToString());
            }
            
        }
        else
        {
            // DDL_FO.Visible = false;
            // chkboxLocation.Visible = false;
            Panel2.Visible = false;
        }
    }

    private void Load_staypln(string terr_code)
    {
        Territory dv = new Territory();
        dsStockist = dv.getTerritory_OS(Division_code, terr_code);
        DDLStay.DataTextField = "Territory_Name";
        DDLStay.DataValueField = "Territory_Code";
        DDLStay.DataSource = dsStockist;
        DDLStay.DataBind();
    }

    protected void DDL_aw_Type_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DDL_aw_Type.SelectedValue == "4")
        {
            Load_staypln(ddlTerritoryName.SelectedValue.ToString());
            lblstay.Visible = true;
             DDLStay.Visible = true;
        }
        else
        {

            lblstay.Visible = false;
            DDLStay.Visible = false;
        }	 		  
    }
	private void FillRouteTown()
    {
        DataTable dt = null;
        dt = execQuery("select TownCode,TownName from Route_Town_master where Division_Code='" + Division_code + "' and Active_Flag = 0");
        if (dt.Rows.Count > 0)
        {
            ddl_town.DataTextField = "TownName";
            ddl_town.DataValueField = "TownCode";
            ddl_town.DataSource = dt;
            ddl_town.DataBind();
        }
    }
	public static DataTable execQuery(string strQry)
    {
        DataTable dt = null;
        DBase_EReport.DB_EReporting db_ER = new DBase_EReport.DB_EReporting();

        try
        {
            dt = db_ER.Exec_DataTable(strQry);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return dt;
    }
}