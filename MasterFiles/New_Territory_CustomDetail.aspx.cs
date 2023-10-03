using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
using Newtonsoft.Json;
using System.Web.Services;
using DBase_EReport;


public partial class MasterFiles_New_Territory_CustomDetail : System.Web.UI.Page
{
    #region "Declaration"
    DataSet dsStockist = null;
    DataSet dsdistributor = null;
    DataSet dsTerritory = null;
    DataSet dsTerritory1 = null;
    DataSet dsTerritory2 = null;

    public static string sf_code = string.Empty;
    public static string sf_type = string.Empty;
    public static string Division_code = string.Empty;
    public static string Territory_Type = string.Empty;
    public static string Territory_Name = string.Empty;
    public static string Territory_SName = string.Empty;
    public static string Alias_Name = string.Empty;
    public static string terr_code = string.Empty;
    public static string Territory_Code = string.Empty;
    public static string div_code = string.Empty;
    public static string dis_code = string.Empty;
    public static string dis_name = string.Empty;
    public static string distributor_name = string.Empty;
    public static string sf_code_name = string.Empty;
    public static string sf_codes = string.Empty;

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
        div_code = Session["div_code"].ToString();
        Division_code = Session["Div_code"].ToString();
        Territory_Code = Request.QueryString["Territory_Code"];

        if (!Page.IsPostBack)
        {
            if (Division_code == "32" || Division_code == "43" || Division_code == "48")
                Session["backurl"] = "Territory.aspx";
            else
                Session["backurl"] = "../../Route_Master.aspx";

            ViewDSM();
            //GetTerritoryName();
            //FillCheckBoxList1();
            txtRoutecode.Focus();
            //FillRouteTown();

            if (Territory_Code != "" && Territory_Code != null)
            {

                //Territory sd = new Territory();

                terrcl sd = new terrcl();

                dsTerritory = sd.get_TerritoryCustom(sf_code, Territory_Code, Division_code);

                if (dsTerritory.Tables.Count > 0)
                {
                    string st = ""; string ddlst = ""; string RouteTownCode = ""; int iCount = 0, iIndex;
                    string TerritorySname = "";

                    txtRoutecode.Text = dsTerritory.Tables[0].Rows[0]["Route_Code"].ToString().Trim();
                    hdnRoutecode.Value = dsTerritory.Tables[0].Rows[0]["Route_Code"].ToString().Trim();

                    txtRouteName.Text = dsTerritory.Tables[0].Rows[0]["Territory_Name"].ToString().Trim();
                    txt_Target.Text = dsTerritory.Tables[0].Rows[0]["Target"].ToString().Trim();
                    txtMinProd.Text = dsTerritory.Tables[0].Rows[0]["Min_Prod"].ToString().Trim();
                    txtRoutePopulation.Text = dsTerritory.Tables[0].Rows[0]["Population"].ToString().Trim();
                    //ddldsm.SelectedValue = dsTerritory.Tables[0].Rows[0].ItemArray.GetValue(5).ToString();
                    TerritorySname = dsTerritory.Tables[0].Rows[0]["Territory_Sname"].ToString().Trim();
                    ddlTerritoryName.SelectedValue = Convert.ToString(TerritorySname);
                    distributor_name = dsTerritory.Tables[0].Rows[0]["Dist_Name"].ToString().Trim();
                    sf_code_name = dsTerritory.Tables[0].Rows[0]["SF_Code"].ToString().Trim();

                    st = dsTerritory.Tables[0].Rows[0]["Allowance_Type"].ToString().Trim();
                    ddlst = dsTerritory.Tables[0].Rows[0]["Territory_SNo"].ToString().Trim();
                    RouteTownCode = dsTerritory.Tables[0].Rows[0]["Route_Town_Code"].ToString().Trim();

                    ddl_town.SelectedValue = Convert.ToString(RouteTownCode);
                    //ddl_town.SelectedValue = dsTerritory.Tables[0].Rows[0]["Route_Town_Code"].ToString().Trim();

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
                    DDL_aw_Type_SelectedIndexChanged(sender, e);
                    DDLStay.SelectedValue = ddlst;

                    GetDDL_FOList();
                    GetchkboxLocationList();

                }

            }
            else
            {

                ddlTerritoryName_SelectedIndexChanged(sender, e);
            }

        }
        if (Session["sf_type"].ToString() == "1")
        {
            terr_code = Convert.ToString(Request.QueryString["Territory_Code"]);
            sf_code = Session["sf_code"].ToString();

            lblTerrritory.Text = "( " + "<span style='font-weight: bold;color:Maroon;'>DSM Name: " + Session["sfName"] + " </span>" + " )";
            ViewTerritory();
            Lab_DSM.Text = "( " + "<span style='font-weight: bold;color:Maroon;'>Distributor Name: " + dis_name + " </span>" + " )";
        }
        else
        {
            terr_code = Convert.ToString(Request.QueryString["Territory_Code"]);
            sf_code = Session["sf_code"].ToString();

            ViewTerritory();

            if (Division_code == "32" || Division_code == "43" || Division_code == "48")
                Session["backurl"] = "Territory.aspx";
            else
                Session["backurl"] = "../../Route_Master.aspx";
            lblTerrritory.Text = "( " + "<span style='font-weight: bold;color:Maroon;'>DSM Name: " + Session["sfName"] + " </span>" + " )";
            Lab_DSM.Text = "( " + "<span style='font-weight: bold;color:Maroon;'>Distributor Name: " + dis_name + " </span>" + " )";
        }

    }

    protected void ddlTerritoryName_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if (ddlTerritoryName.SelectedValue != "0")
        //{
        //    FillCheckBoxList1();
        //    Panel2.Visible = true;
        //    if (DDL_aw_Type.SelectedValue == "4")
        //    {
        //        Load_staypln(ddlTerritoryName.SelectedValue.ToString());
        //    }
        //}
        //else
        //{         
        //    Panel2.Visible = false;
        //}
    }

    public void GetchkboxLocationList()
    {
        //dis_code = "";
        //if (chkboxLocation.Items.Count > 0)
        //{

        //    for (int i = 0; i < chkboxLocation.Items.Count; i++)
        //    {
        //        if (chkboxLocation.Items[i].Selected)
        //            dis_code += chkboxLocation.Items[i].Value.ToString() + ",";
        //    }
        //    dis_code = dis_code.TrimEnd(',');
        //}
    }

    protected void chkboxLocation_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetchkboxLocationList();
    }

    public void GetDDL_FOList()
    {
        //sf_codes = "";
        //if (DDL_FO.Items.Count > 0)
        //{

        //    for (int i = 0; i < DDL_FO.Items.Count; i++)
        //    {
        //        if (DDL_FO.Items[i].Selected)
        //            sf_codes += chkboxLocation.Items[i].Value.ToString() + ",";
        //    }
        //    sf_codes = sf_codes.TrimEnd(',');
        //}
    }

    protected void DDL_FO_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetDDL_FOList();
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

    private void GetTerritoryName()
    {
        Territorys dv = new Territorys();
        dsStockist = dv.TerritorygetSF_Code(Division_code, sf_code);
        if (dsStockist.Tables[0].Rows.Count > 0)
        {
            ddlTerritoryName.DataSource = dsStockist;
            ddlTerritoryName.DataTextField = "Territory_name";
            ddlTerritoryName.DataValueField = "Territory_code";
            ddlTerritoryName.DataBind();
        }

    }

    [WebMethod]
    public static string GetBindData(string TerritoryCode)
    {
        //Territory sd = new Territory();

        terrcl sd = new terrcl();

        DataSet dsTerritory = sd.get_TerritoryCustom(sf_code, Territory_Code, Division_code);

        return JsonConvert.SerializeObject(dsTerritory.Tables[0]);
    }


    [WebMethod]
    public static string GetBindCustomFieldData(string TerritoryCode)
    {
        //Territory sd = new Territory();

        terrcl sd = new terrcl();

        DataSet dsTerritory = sd.get_TerritoryCustomField(Territory_Code, Division_code);

        return JsonConvert.SerializeObject(dsTerritory.Tables[0]);

    }


    [WebMethod]
    public static string GetDistributorList(string TerritoryName)
    {
        if (TerritoryName == null || TerritoryName == "")
        {
            TerritoryName = "0";
        }

        SubDivision dv = new SubDivision();
        DataSet dsdistributor = dv.get_distributor(TerritoryName);

        List<ListItem> DistributorName = new List<ListItem>();
        if (dsdistributor.Tables[0].Rows.Count > 0)
        {
            foreach (DataRow sdr in dsdistributor.Tables[0].Rows)
            {
                DistributorName.Add(new ListItem
                {
                    Value = sdr["Stockist_Code"].ToString(),
                    Text = sdr["Stockist_Name"].ToString()
                });
            }
        }

        // return DistributorName;

        return JsonConvert.SerializeObject(dsdistributor.Tables[0]);


    }

    [WebMethod]
    public static string GetFieldForceList(string TerritoryName)
    {
        if (TerritoryName == null || TerritoryName == "")
        {
            TerritoryName = "0";
        }

        Stockist sk = new Stockist();
        DataSet dsStockist = sk.FOName(TerritoryName, Division_code);


        List<ListItem> FieldForce = new List<ListItem>();
        if (dsStockist.Tables[0].Rows.Count > 0)
        {
            foreach (DataRow sdr in dsStockist.Tables[0].Rows)
            {
                FieldForce.Add(new ListItem
                {
                    Value = sdr["Sf_Code"].ToString(),
                    Text = sdr["Sf_Name"].ToString()
                });
            }
        }

        //return FieldForce;

        return JsonConvert.SerializeObject(dsStockist.Tables[0]);
    }

    private void FillCheckBoxList1()
    {

        SubDivision dv = new SubDivision();
        dsdistributor = dv.get_distributor(ddlTerritoryName.SelectedValue);

        //chkboxLocation.DataSource = dsdistributor;
        //chkboxLocation.DataTextField = "Stockist_Name";
        //chkboxLocation.DataValueField = "Stockist_Code";
        //chkboxLocation.DataBind();

        List<ListItem> DistributorName = new List<ListItem>();
        if (dsdistributor.Tables[0].Rows.Count > 0)
        {
            foreach (DataRow sdr in dsdistributor.Tables[0].Rows)
            {
                DistributorName.Add(new ListItem
                {
                    Value = sdr["Stockist_Code"].ToString(),
                    Text = sdr["Stockist_Name"].ToString()
                });
            }
        }

        //chkboxLocation.DataSource = DistributorName;
        //chkboxLocation.DataTextField = "Text";
        //chkboxLocation.DataValueField = "Value";
        // chkboxLocation.DataBind();

        //string[] subdiv;
        //if (distributor_name != "")
        //{
        //    iIndex = -1;
        //    subdiv = distributor_name.Split(',');
        //    foreach (string st in subdiv)
        //    {
        //        for (iIndex = 0; iIndex < chkboxLocation.Items.Count; iIndex++)
        //        {
        //            if (st == chkboxLocation.Items[iIndex].Value)
        //            {
        //                chkboxLocation.Items[iIndex].Selected = true;
        //                chkboxLocation.Items[iIndex].Attributes.Add("style", "Color: #8A2BE2;font-weight:Bold");
        //            }
        //        }
        //    }
        //}

        Stockist sk = new Stockist();
        dsStockist = sk.FOName(ddlTerritoryName.SelectedValue, Division_code);
        //DDL_FO.DataSource = dsStockist;
        //DDL_FO.DataTextField = "Sf_Name";
        //DDL_FO.DataValueField = "Sf_Code";
        //DDL_FO.DataBind();


        //if (sf_code_name != "")
        //{
        //    iIndex = -1;
        //    subdiv = sf_code_name.Split(',');
        //    foreach (string st in subdiv)
        //    {
        //        for (iIndex = 0; iIndex < DDL_FO.Items.Count; iIndex++)
        //        {
        //            if (st == DDL_FO.Items[iIndex].Value)
        //            {
        //                DDL_FO.Items[iIndex].Selected = true;
        //                DDL_FO.Items[iIndex].Attributes.Add("style", "Color: #8A2BE2;font-weight:Bold");
        //            }
        //        }
        //    }
        //}
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

    protected void btnSave_Click(object sender, EventArgs e)
    {


        string Route_code = txtRoutecode.Text;
        string Route_Name = txtRouteName.Text;
        string Target = txt_Target.Text;
        string min_prod = txtMinProd.Text;
        string Route_population = txtRoutePopulation.Text;
        string territory_id = ddlTerritoryName.SelectedValue.ToString();
        string dist_name = "";
        string sf_code = "";
        string route_town = ddl_town.SelectedValue.ToString();

        //foreach (ListItem item in chkboxLocation.Items)
        //{
        //    if (item.Selected)
        //    {
        //        dist_name += item.Value + ",";
        //    }
        //}

        dist_name = dist_name.TrimEnd(',');


        //foreach (ListItem item in DDL_FO.Items)
        //{
        //    if (item.Selected)
        //    {
        //        sf_code += item.Value + ",";
        //    }
        //}
        sf_code = sf_code.TrimEnd(',');
        string dstay = "0";
        if (DDLStay.SelectedValue.ToString() != "0")
        {
            dstay = DDLStay.SelectedValue.ToString();
        }
        if (sf_code != "")
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

                    if (Division_code == "32" || Division_code == "43" || Division_code == "48")
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
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Select Field Force');</script>");
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

    public void Clear()
    {
        txtRoutecode.Text = "";
        txtRouteName.Text = "";
        txt_Target.Text = "0";
        txtMinProd.Text = "0";
        txtRoutePopulation.Text = "0";
        ddldsm.SelectedIndex = 0;
        ddlTerritoryName.SelectedIndex = -1;
        //chkboxLocation.SelectedIndex = -1;
        Panel2.Visible = false;
        DDL_aw_Type.SelectedIndex = 1;

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

    [WebMethod]
    public static List<ListItem> Loadstaypln(string terr_code)
    {
        Territory dv = new Territory();
        DataSet dsStockist = dv.getTerritory_OS(Division_code, terr_code);


        List<ListItem> routetown = new List<ListItem>();

        if (dsStockist.Tables[0].Rows.Count > 0)
        {
            foreach (DataRow sdr in dsStockist.Tables[0].Rows)
            {
                routetown.Add(new ListItem
                {
                    Value = sdr["Territory_Code"].ToString(),
                    Text = sdr["Territory_Name"].ToString()
                });
            }
        }
        return routetown;
    }

    [WebMethod]
    public static List<ListItem> GetRouteTown(string Division_Code)
    {
        DataTable dt = null;
        DB_EReporting db_Er = new DB_EReporting();

        dt = db_Er.Exec_DataTable("select TownCode,TownName from Route_Town_master where Division_Code='" + Division_code + "' and Active_Flag = 0");

        List<ListItem> routetown = new List<ListItem>();

        if (dt.Rows.Count > 0)
        {
            foreach (DataRow sdr in dt.Rows)
            {
                routetown.Add(new ListItem
                {
                    Value = sdr["TownCode"].ToString(),
                    Text = sdr["TownName"].ToString()
                });
            }
        }
        return routetown;
    }

    [WebMethod]
    public static List<ListItem> FillTerritoryName(string Division_Code, string sfcode)
    {
        Territorys dv = new Territorys();
        DataSet dsSl = dv.TerritorygetSF_Code(Division_code, sfcode);
        List<ListItem> TerritoryName = new List<ListItem>();
        if (dsSl.Tables[0].Rows.Count > 0)
        {
            foreach (DataRow sdr in dsSl.Tables[0].Rows)
            {
                TerritoryName.Add(new ListItem
                {
                    Value = sdr["Territory_code"].ToString(),
                    Text = sdr["Territory_name"].ToString()
                });
            }
        }
        return TerritoryName;
        //return JsonConvert.SerializeObject(dt);
    }

    [WebMethod]
    public static string GetCustomFormsFieldsList(string divcode, string ModuleId)
    {
        DataSet ds = new DataSet();
        //AdminSetup Ad = new AdminSetup();
        terrcl Ad = new terrcl();
        ds = Ad.GetCustomFormsFieldsData(divcode, ModuleId);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }

    [WebMethod]
    public static string GetCustomFormsSeclectionMastesList(string TableName, string ColumnsName)
    {
        DataSet ds = new DataSet();
        //AdminSetup Ad = new AdminSetup();
        terrcl Ad = new terrcl();
        ds = Ad.GetCustomFormsSeclectionMastesList(TableName, ColumnsName);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }


    [WebMethod]
    public static string SaveAdditionalField(string fdata)
    {
        string data = "";

        Territory terr = new Territory();
        terrcl terrl = new terrcl();
        DB_EReporting db_ER = new DB_EReporting();
        DataSet ds = new DataSet();

        int iReturn = 0;
        terrcl.RouteMainfld sd = JsonConvert.DeserializeObject<terrcl.RouteMainfld>(fdata);
        //RouteMainfld sd = JsonConvert.DeserializeObject<RouteMainfld>(data);        


        List<terrcl.AddtionalfieldDetails> addfields = sd.Additionsfld;

        string Route_code = Convert.ToString(sd.Route_code);
        string Route_Name = Convert.ToString(sd.Route_Name);
        string Target = Convert.ToString(sd.Target);
        string min_prod = Convert.ToString(sd.min_prod);
        string Route_population = Convert.ToString(sd.Route_population);
        string territory_id = Convert.ToString(sd.territory_id);
        string distname = Convert.ToString(sd.dist_name);
        string dist_name = ""; string sfcode = "";
        //dist_name = Convert.ToString(dis_code.TrimEnd(','));
        string sf_code = Convert.ToString(sd.sf_code);
        dist_name = distname.TrimEnd(',');        
        sfcode = sf_code.TrimEnd(',');
        //sfcode = Convert.ToString(sf_codes.TrimEnd(','));
        string route_town = Convert.ToString(sd.route_town);
        string dstay = Convert.ToString(sd.dstay);

        if (dstay == null || dstay == "")
        { dstay = "0"; }

        string DDL_aw_Type = sd.DDL_aw_Type.ToString();
        if (sf_code != "")
        {
            if (Territory_Code == null)
            {
                terr = new Territory();

                iReturn = terr.RecordAdd(Route_code, Route_Name, Territory_Type, sfcode, Target, min_prod, Division_code,
                    Route_population, territory_id, dist_name, DDL_aw_Type.ToString(), dstay, route_town);

                if (iReturn > 0)
                {
                    data = "Created Successfully";

                    //ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Created Successfully');</script>");
                    //Clear();

                    string fld = ""; string val = "";
                    int i = 0;
                  
                    if (addfields.Count > 0)
                    {
                        for (int k = 0; k < addfields.Count; k++)
                        {
                            if ((addfields[k].Fields == "'undefined'" || addfields[k].Fields == "undefined") && (addfields[k].Values == "'undefined'" || addfields[k].Values == "undefined"))
                            { }
                            else
                            {
                                fld += addfields[k].Fields + ",";

                                if ((addfields[k].Values == null || addfields[k].Values == ""))
                                { val += "'',"; }
                                else
                                { val += "'" + addfields[k].Values + "',"; }
                            }
                        }

                        ds = new DataSet();
                        string Squery = "SELECT MAX(Territory_Code) Territory_Code FROM  Mas_Territory_Creation Where division_Code = " + Division_code + "";

                        db_ER = new DB_EReporting();

                        ds = db_ER.Exec_DataSet(Squery);

                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            string RouteID = Convert.ToString(ds.Tables[0].Rows[0]["Territory_Code"]);

                            string Iquery = "Insert Into Trans_Route_Custom_Field(RouteID, " + fld.TrimEnd(',') + ") Values(" + RouteID + "," + val.TrimEnd(',') + ")";

                            i = db_ER.ExecQry(Iquery);

                            if (i > 0)
                            { data = "Created Successfully"; }
                        }
                    }
                }
                else if (iReturn == -2)
                {
                    data = "Route Code Already Exist";
                    //ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Route Code Already Exist');</script>");
                }
                else if (iReturn == -3)
                {
                    data = "Route Name Already Exist";

                    //ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Route Name Already Exist');</script>");
                }
            }
            else
            {

                terr = new Territory();
                iReturn = terr.RecordUpdate(Territory_Code, Route_code, Route_Name, Territory_Type, sfcode, Target, min_prod,
                    Division_code, Route_population, territory_id, dist_name, DDL_aw_Type.ToString(), dstay, route_town);

                if (iReturn > 0)
                {
                    data = "Updated Successfully";
                    db_ER = new DB_EReporting();
                    ds = new DataSet();

                    string Squery = "SELECT *FROM  Trans_Route_Custom_Field WHERE RouteId = " + Territory_Code + "";
                    ds = db_ER.Exec_DataSet(Squery);

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        string fld = ""; string val = "";

                        if (addfields.Count > 0)
                        {
                            int i = 0;

                            for (int k = 0; k < addfields.Count; k++)
                            {
                                if ((addfields[k].Fields == "'undefined'" || addfields[k].Fields == "undefined") && (addfields[k].Values == "'undefined'" || addfields[k].Values == "undefined"))
                                { }
                                else
                                {
                                    //fld += addfields[k].Fields + ",";//val += "'" + addfields[k].Values + "',";                                        
                                    fld = addfields[k].Fields;
                                    if (addfields[k].Values == null || addfields[k].Values == "")
                                    { val = "'0'"; }
                                    else
                                    { val = "'" + addfields[k].Values + "'"; }

                                    string uquery = "UPDATE Trans_Route_Custom_Field  SET " + fld + " = " + val.TrimEnd(',') + " WHERE RouteID = " + Territory_Code + " ";
                                    i = db_ER.ExecQry(uquery);
                                }
                            }

                            if (i > 0)
                            { data = "Updated Successfully"; }
                        }

                    }
                    else
                    {
                        string fld = ""; string val = "";
                        int i = 0;

                        if (addfields.Count > 0)
                        {
                            for (int k = 0; k < addfields.Count; k++)
                            {
                                if ((addfields[k].Fields == "'undefined'" || addfields[k].Fields == "undefined") && (addfields[k].Values == "'undefined'" || addfields[k].Values == "undefined"))
                                { }
                                else
                                {
                                    fld += addfields[k].Fields + ",";

                                    if (addfields[k].Values == null || addfields[k].Values == "")
                                    { val += "'',"; }
                                    else
                                    { val += "'" + addfields[k].Values + "',"; }
                                }
                            }

                            if ((fld != null || fld != "") && (val != null || val != ""))
                            {
                                string Iquery = "Insert Into Trans_Route_Custom_Field(RouteID, " + fld.TrimEnd(',') + ")Values(" + Territory_Code + "," + val.TrimEnd(',') + ")";

                                db_ER = new DB_EReporting();

                                i = db_ER.ExecQry(Iquery);
                            }

                            if (i > 0)
                            { data = "Updated Successfully"; }
                        }
                        //else { msg = "Error"; }
                    }

                    //if (Division_code == "32" || Division_code == "43" || Division_code == "48")
                    //    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Updated Successfully');window.location='Territory.aspx';</script>");
                    //else
                    //    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Updated Successfully');window.location='../../Route_Master.aspx';</script>");
                }
                else if (iReturn == -2)
                {
                    data = "Route Code Already Exist";
                    //ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Route Code Already Exist');</script>");
                    //txtRoutecode.Focus();
                }
                else if (iReturn == -3)
                {
                    data = "Route Name Already Exist";
                    //ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Route Name Already Exist');</script>");
                    //txtRouteName.Focus();
                }
            }
        }
        else
        {
            data = "Select Field Force";
            //ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Select Field Force');</script>");
        }

        return data;
    }


    //[WebMethod]
    //public static string SaveAdditionalField(List<RouteMainfld> routemainflds)
    //{
    //    string data = "";

    //    if (routemainflds.Count > 0)
    //    {
    //        string Route_code = "";
    //        string Route_Name = "";
    //        string Target = "";
    //        string min_prod = "";
    //        string Route_population = "";
    //        string territory_id = "";
    //        string dist_name = "";
    //        string sfcode = "";
    //        string route_town = "";
    //        string dstay = "0";
    //        string DDL_aw_Type = "";

    //        Territory terr = new Territory();
    //        DB_EReporting db_ER = new DB_EReporting();
    //        DataSet ds = new DataSet();

    //        int iReturn = 0;

    //        foreach (RouteMainfld rmainfld in routemainflds)
    //        {

    //            Route_code = Convert.ToString(rmainfld.Route_code);

    //            Route_Name = Convert.ToString(rmainfld.Route_Name);
    //            Target = Convert.ToString(rmainfld.Target);
    //            min_prod = Convert.ToString(rmainfld.min_prod);
    //            Route_population = Convert.ToString(rmainfld.Route_population);
    //            territory_id = Convert.ToString(rmainfld.territory_id);

    //            dist_name = Convert.ToString(rmainfld.dist_name);

    //            //dist_name = Convert.ToString(dis_code.TrimEnd(','));

    //            sf_code = Convert.ToString(rmainfld.sf_code);

    //            dist_name = dist_name.TrimEnd(',');
    //            sf_code = sf_code.TrimEnd(',');
    //            sfcode = sf_code.TrimEnd(',');
    //            //sfcode = Convert.ToString(sf_codes.TrimEnd(','));

    //            route_town = Convert.ToString(rmainfld.route_town);

    //            dstay = Convert.ToString(rmainfld.dstay);

    //            if (dstay == null || dstay == "")
    //            { dstay = "0"; }

    //            DDL_aw_Type = rmainfld.DDL_aw_Type.ToString();

    //            if (sf_code != "")
    //            {
    //                if (Territory_Code == null)
    //                {
    //                    terr = new Territory();

    //                    iReturn = terr.RecordAdd(Route_code, Route_Name, Territory_Type, sfcode, Target, min_prod, Division_code,
    //                        Route_population, territory_id, dist_name, DDL_aw_Type.ToString(), dstay, route_town);

    //                    if (iReturn > 0)
    //                    {
    //                        data = "Created Successfully";

    //                        //ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Created Successfully');</script>");
    //                        //Clear();

    //                        string fld = ""; string val = "";
    //                        int i = 0;

    //                        List<additionationalfld> addfields = new List<additionationalfld>();

    //                        addfields = rmainfld.Additionsfld.ToList();

    //                        if (addfields.Count > 0)
    //                        {
    //                            for (int k = 0; k < addfields.Count; k++)
    //                            {
    //                                if ((addfields[k].Fields == "'undefined'" || addfields[k].Fields == "undefined") && (addfields[k].Values == "'undefined'" || addfields[k].Values == "undefined"))
    //                                { }
    //                                else
    //                                {
    //                                    fld += addfields[k].Fields + ",";

    //                                    if ((addfields[k].Values == null || addfields[k].Values == ""))
    //                                    { val += "'0',"; }
    //                                    else
    //                                    { val += "'" + addfields[k].Values + "',"; }                                      
    //                                }
    //                            }

    //                            ds = new DataSet();
    //                            string Squery = "SELECT MAX(Territory_Code) Territory_Code FROM  Mas_Territory_Creation Where division_Code = " + Division_code + "";

    //                            db_ER = new DB_EReporting();

    //                            ds = db_ER.Exec_DataSet(Squery);

    //                            if (ds.Tables[0].Rows.Count > 0)
    //                            {
    //                                string RouteID = Convert.ToString(ds.Tables[0].Rows[0]["Territory_Code"]);

    //                                string Iquery = "Insert Into Trans_Route_Custom_Field(RouteID, " + fld.TrimEnd(',') + ") Values(" + RouteID + "," + val.TrimEnd(',') + ")";

    //                                i = db_ER.ExecQry(Iquery);

    //                                if (i > 0)
    //                                { data = "Created Successfully"; }
    //                            }
    //                        }
    //                    }
    //                    else if (iReturn == -2)
    //                    {
    //                        data = "Route Code Already Exist";
    //                        //ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Route Code Already Exist');</script>");
    //                    }
    //                    else if (iReturn == -3)
    //                    {
    //                        data = "Route Name Already Exist";

    //                        //ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Route Name Already Exist');</script>");
    //                    }
    //                }
    //                else
    //                {

    //                    terr = new Territory();
    //                    iReturn = terr.RecordUpdate(Territory_Code, Route_code, Route_Name, Territory_Type, sfcode, Target, min_prod,
    //                        Division_code, Route_population, territory_id, dist_name, DDL_aw_Type.ToString(), dstay, route_town);

    //                    if (iReturn > 0)
    //                    {
    //                        data = "Updated Successfully";
    //                        db_ER = new DB_EReporting();
    //                        ds = new DataSet();

    //                        string Squery = "SELECT *FROM  Trans_Route_Custom_Field WHERE RouteId = " + Territory_Code + "";
    //                        ds = db_ER.Exec_DataSet(Squery);

    //                        if (ds.Tables[0].Rows.Count > 0)
    //                        {
    //                            string fld = ""; string val = "";

    //                            List<additionationalfld> addfields = new List<additionationalfld>();

    //                            addfields = rmainfld.Additionsfld.ToList();

    //                            if (addfields.Count > 0)
    //                            {
    //                                int i = 0;

    //                                for (int k = 0; k < addfields.Count; k++)
    //                                {
    //                                    if ((addfields[k].Fields == "'undefined'" || addfields[k].Fields == "undefined") && (addfields[k].Values == "'undefined'" || addfields[k].Values == "undefined"))
    //                                    { }
    //                                    else
    //                                    {
    //                                        //fld += addfields[k].Fields + ",";//val += "'" + addfields[k].Values + "',";                                        
    //                                        fld = addfields[k].Fields;
    //                                        if (addfields[k].Values == null || addfields[k].Values == "")
    //                                        { val = "'0'"; }
    //                                        else
    //                                        { val = "'" + addfields[k].Values + "'"; }

    //                                        string uquery = "UPDATE Trans_Route_Custom_Field  SET " + fld + " = " + val.TrimEnd(',') + " WHERE RouteID = " + Territory_Code + " ";
    //                                        i = db_ER.ExecQry(uquery);
    //                                    }
    //                                }

    //                                if (i > 0)
    //                                { data = "Updated Successfully"; }
    //                            }

    //                        }
    //                        else
    //                        {
    //                            string fld = ""; string val = "";
    //                            int i = 0;

    //                            List<additionationalfld> addfields = new List<additionationalfld>();

    //                            addfields = rmainfld.Additionsfld.ToList();

    //                            if (addfields.Count > 0)
    //                            {
    //                                for (int k = 0; k < addfields.Count; k++)
    //                                {
    //                                    if ((addfields[k].Fields == "'undefined'" || addfields[k].Fields == "undefined") && (addfields[k].Values == "'undefined'" || addfields[k].Values == "undefined"))
    //                                    { }
    //                                    else
    //                                    {
    //                                        fld += addfields[k].Fields + ",";

    //                                        if (addfields[k].Values == null || addfields[k].Values == "")
    //                                        { val += "'0',"; }
    //                                        else
    //                                        { val += "'" + addfields[k].Values + "',"; }
    //                                    }
    //                                }

    //                                if ((fld != null || fld != "") && (val != null || val != ""))
    //                                {
    //                                    string Iquery = "Insert Into Trans_Route_Custom_Field(RouteID, " + fld.TrimEnd(',') + ")Values(" + Territory_Code + "," + val.TrimEnd(',') + ")";

    //                                    db_ER = new DB_EReporting();

    //                                    i = db_ER.ExecQry(Iquery);
    //                                }

    //                                if (i > 0)
    //                                { data = "Updated Successfully"; }
    //                            }
    //                            //else { msg = "Error"; }
    //                        }

    //                        //if (Division_code == "32" || Division_code == "43" || Division_code == "48")
    //                        //    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Updated Successfully');window.location='Territory.aspx';</script>");
    //                        //else
    //                        //    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Updated Successfully');window.location='../../Route_Master.aspx';</script>");
    //                    }
    //                    else if (iReturn == -2)
    //                    {
    //                        data = "Route Code Already Exist";
    //                        //ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Route Code Already Exist');</script>");
    //                        //txtRoutecode.Focus();
    //                    }
    //                    else if (iReturn == -3)
    //                    {
    //                        data = "Route Name Already Exist";
    //                        //ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Route Name Already Exist');</script>");
    //                        //txtRouteName.Focus();
    //                    }
    //                }
    //            }
    //            else
    //            {
    //                data = "Select Field Force";
    //                //ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Select Field Force');</script>");
    //            }
    //        }
    //    }

    //    return data;
    //}

    

    public class terrcl
    {
        string strQry = "";
        public DataSet get_TerritoryCustom(string sf_code, string terr_Route_Code, string Division_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTerr = null;

            strQry = " EXEC [getTerritory_Details] '" + Division_code + "','" + terr_Route_Code + "'";

            //strQry = " SELECT Route_Code, Territory_Name,Target,Min_Prod,Population,SF_Code,Territory_Sname,Dist_Name,Allowance_Type,Territory_SNo " +
            //         " FROM  Mas_Territory_Creation " +
            //         " where territory_active_flag=0 and Territory_Code= '" + terr_Route_Code + "' AND Division_Code= '" + Division_code + "'";

            try
            {
                dsTerr = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsTerr;
        }

        public DataSet get_TerritoryCustomField(string terr_Route_Code, string Division_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTerr = null;

            //strQry = " SELECT *FROM Trans_Route_Custom_Field Where RouteID = " + terr_Route_Code + "";

            strQry = "EXEC [getTerritoryAddtionalFields_Details] " + terr_Route_Code + "";

            try
            {
                dsTerr = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsTerr;
        }

        public DataSet GetCustomFormsSeclectionMastesList(string TableName, string ColumnsName)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "EXEC [Get_CustomForms_SeclectionMastesList] '" + TableName + "' ,'" + ColumnsName + "'";

            try
            {
                dsAdmin = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsAdmin;
        }

        public DataSet GetCustomFormsFieldsData(string divcode, string ModeleId)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "EXEC [Get_CustomForms_Fields] '" + divcode + "' ,'" + ModeleId + "' ";

            try
            {
                dsAdmin = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsAdmin;
        }

        public class AddtionalfieldDetails
        {
            [JsonProperty("Fields")]
            public string Fields { get; set; }

            [JsonProperty("Values")]
            public string Values { get; set; }
        }

        public class RouteMainfld
        {

            [JsonProperty("Route_code")]
            public string Route_code { get; set; }

            [JsonProperty("Route_Name")]
            public object Route_Name { get; set; }

            [JsonProperty("Target")]
            public object Target { get; set; }

            [JsonProperty("min_prod")]
            public object min_prod { get; set; }

            [JsonProperty("Route_population")]
            public object Route_population { get; set; }

            [JsonProperty("territory_id")]
            public object territory_id { get; set; }

            [JsonProperty("dist_name")]
            public string dist_name { get; set; }

            [JsonProperty("sf_code")]
            public object sf_code { get; set; }

            [JsonProperty("route_town")]
            public object route_town { get; set; }

            [JsonProperty("dstay")]
            public object dstay { get; set; }

            [JsonProperty("DDL_aw_Type")]
            public object DDL_aw_Type { get; set; }


            [JsonProperty("Additionsfld")]
            public List<AddtionalfieldDetails> Additionsfld { get; set; }

        }
    }
}