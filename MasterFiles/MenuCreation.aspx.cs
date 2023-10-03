using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data;
using System.Configuration;
using Bus_EReport;
using System.Windows.Forms;


public partial class MasterFiles_MenuCreation : System.Web.UI.Page
{
    #region "Declaration"
    DataSet dsSubDiv = null;
    string Subdivision_Code = string.Empty;
    string divcode = string.Empty;
    string subdiv_sname = string.Empty;
    string subdiv_name = string.Empty;
    string Area_name = string.Empty;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int time;
    DataSet dsDivision = null;
    DataSet dsState = null;
    string state_cd = string.Empty;
    string sState = string.Empty;
    string Area_cd = string.Empty;
    string[] statecd;
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        divcode = Convert.ToString(Session["div_code"]);
        Session["backurl"] = "MenuList.aspx";
        Subdivision_Code = Request.QueryString["Subdivision_Code"];

        if (!Page.IsPostBack)
        {
            
            //menu1.Title = this.Page.Title;
            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);
            FillState(divcode);
            txtSubDivision_Sname.Focus();
            if (Subdivision_Code != "" && Subdivision_Code != null)
            {
                Zone sd = new Zone();
                dsSubDiv = sd.getMenu(divcode, Subdivision_Code);
                if (dsSubDiv.Tables[0].Rows.Count > 0)
                {
                    txtSubDivision_Sname.Text = dsSubDiv.Tables[0].Rows[0].ItemArray.GetValue(0).ToString().Trim();
                    txtSubDivision_Name.Text = dsSubDiv.Tables[0].Rows[0].ItemArray.GetValue(1).ToString().Trim();
                    Txt_url.Text = dsSubDiv.Tables[0].Rows[0].ItemArray.GetValue(3).ToString().Trim();
                    string st = dsSubDiv.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();
                    ddlParent.DataSource = dsState;
                    ddlParent.DataTextField = "Menu_Name";
                    ddlParent.DataValueField = "Menu_ID";
                    ddlParent.DataBind();
                    int iCount = 0, iIndex;
                    ddlParent.Items.Insert(1, "- Main Menu -");
                    foreach (ListItem item in ddlParent.Items)
                    {
                        if (st == item.ToString())
                        {
                            iIndex = iCount;
                            ddlParent.SelectedIndex = iIndex;
                            break;
                        }
                        iCount++;
                    }

                }
            }

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
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        if (txtSubDivision_Sname.Text.Length <= 5 && txtSubDivision_Name.Text.Length <= 50)
        {
            subdiv_sname = txtSubDivision_Sname.Text.Trim();
            subdiv_name = txtSubDivision_Name.Text.Trim();
            Area_name = ddlParent.SelectedValue.ToString();
            Area_cd = Txt_url.Text;
            string Icon = Txt_Icon.Text.Trim();
            //Zone dv_code = new Zone();
            //dsDivision = dv_code.area_code(Area_name);
            //if (dsDivision.Tables[0].Rows.Count > 0)
            //{
            //    //int i = 0;

            //    Area_cd = dsDivision.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
            //}
            if (Subdivision_Code == null)
            {

                // Add New Sub Division
                Zone dv = new Zone();
                int iReturn = dv.MenuRecordAdd(divcode, subdiv_sname, subdiv_name, Area_name, Area_cd, Icon);

                if (iReturn > 0)
                {

                    // menu1.Status = "Sub Division created Successfully ";
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Created Successfully');window.location='MenuList.aspx';</script>");
                    
                    Resetall();
                }
                else if (iReturn == -2)
                {

                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Menu Name Already Exist');</script>");
                    txtSubDivision_Name.Focus();
                }
                else if (iReturn == -3)
                {
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Menu Code Already Exist');</script>");
                    txtSubDivision_Sname.Focus();
                }
            }
            else
            {
                // Update Sub Division
                Zone dv = new Zone();
                int subdivcode = 0;
                int iReturn = dv.MenuRecordUpdate(subdivcode, subdiv_sname, subdiv_name, Area_name, divcode, Area_cd,Icon);
                if (iReturn == 1)
                {
                    // menu1.Status = "Sub Division Updated Successfully ";
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Updated Successfully');window.location='MenuList.aspx';</script>");
                }
                else if (iReturn == -2)
                {

                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Menu Name Already Exist');</script>");
                    txtSubDivision_Name.Focus();
                }
                else if (iReturn == -3)
                {
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Menu Code Already Exist');</script>");
                    txtSubDivision_Sname.Focus();
                }
            }
        }
        else
        {
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('please Enter minimum length Value');</script>");
        }
    }
    private void Resetall()
    {
        txtSubDivision_Sname.Text = "";
        txtSubDivision_Name.Text = "";
        Txt_url.Text = "";
        ddlParent.SelectedIndex = 0;
        Txt_Icon.Text = "";
    }
    //AreaList Create by Giri 14-06-2016
    private void FillState(string div_code)
    {
        Zone dv = new Zone();
        dsDivision = dv.getStatePerDivision(div_code);
        if (dsDivision.Tables[0].Rows.Count > 0)
        {
            //int i = 0;
            state_cd = string.Empty;
            state_cd = dsDivision.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
            //statecd = sState.Split(',');
            //foreach (string st_cd in statecd)
            //{
            //    if (i == 0)
            //    {
            //        state_cd = state_cd + st_cd;
            //    }
            //    else
            //    {
            //        if (st_cd.Trim().Length > 0)
            //        {
            //            state_cd = state_cd + "," + st_cd;
            //        }
            //    }
            //    i++;
            //}

            Zone st = new Zone();
            dsState = st.getMenuP(div_code);
            ddlParent.DataTextField = "Menu_Name";
            ddlParent.DataValueField = "Menu_ID";
            ddlParent.DataSource = dsState;
            ddlParent.DataBind();
            ddlParent.Items.Insert(1, "- Main Menu -");
            
        }
    }
    
}