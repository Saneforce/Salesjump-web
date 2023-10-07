using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
using System.Drawing.Imaging;
public partial class Reports_Menu_Hide : System.Web.UI.Page
{
    DataSet dsMenu = null;
    DataSet dsSalesForce = null;
    DataSet dsdiv = new DataSet();
    DataSet dsTP = null;
    DataSet dsDivision = null;
    string strMultiDiv = string.Empty;
    string div_code = string.Empty;
    string sf_code = string.Empty;
    DataSet dsSf = null;
    string sf_type = string.Empty;
    string sQryStr = string.Empty;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    string Plandate=string.Empty;
    int time;
    //my day plan
    string Menu_type = string.Empty;
    string work_type_name = string.Empty;
    string work_type_code = string.Empty;
    string Head_Quarters_name = string.Empty;
    string Head_Quarters_code = string.Empty;
    string Dist_name = string.Empty;
    string Dist_code = string.Empty;
    string Route_name = string.Empty;
    string Route_code = string.Empty;
    string Remarks = string.Empty;
    string sChkLocation = string.Empty;
    int iIndex = -1;

    protected void Page_PreInit(object sender, EventArgs e)
    {
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
        sf_code = Session["sf_code"].ToString();
        sf_type = Session["sf_type"].ToString();

        if (Session["sf_type"].ToString() == "1")
        {
           

        }

        else if (Session["sf_type"].ToString() == "2")
        {

            

        }

        else if (Session["sf_type"].ToString() == "3")
        {
            if (!Page.IsPostBack)
            {
                FillCheckBoxList1();
            }
        }

       

    }

    private void FillCheckBoxList1()
    {
        //List of States are loaded into the checkbox list from Division Class
        SubDivision dv = new SubDivision();
        dsMenu = dv.getMenuType(div_code);
        Chk_Menu.DataTextField = "Menu_Name";
        Chk_Menu.DataValueField = "Menu_code";
        Chk_Menu.DataSource = dsMenu;
        Chk_Menu.DataBind();
        string[] subdiv;
        if (Menu_type != "")
        {
            iIndex = -1;
            subdiv = Menu_type.Split(',');
            foreach (string st in subdiv)
            {
                for (iIndex = 0; iIndex < Chk_Menu.Items.Count; iIndex++)
                {
                    if (st == Chk_Menu.Items[iIndex].Value)
                    {
                        Chk_Menu.Items[iIndex].Selected = true;
                        Chk_Menu.Items[iIndex].Attributes.Add("style", "Color: #8A2BE2;font-weight:Bold");
                    }
                }
            }
        }
    }


    protected void button_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        string username = Txt_User_Name.Text;
        string password = Txt_Password.Text;

        for (int i = 0; i < Chk_Menu.Items.Count; i++)
            {
                if (Chk_Menu.Items[i].Selected)
                {
                    sChkLocation = sChkLocation + Chk_Menu.Items[i].Value + ",";
                }
            }

        if ((username != "") && (password != "") && (sChkLocation !=""))
        {
            // Add 
            TP_New tpRP = new TP_New();

            int iReturn = tpRP.Access_ctl_RecordAdd(username, password, div_code, sChkLocation);

            if (iReturn > 0)
            {

               
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Control Successfully');</script>");
                Resetall();
            }
            else if (iReturn == -2)
            {

                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Username Already Exist');</script>");

            }
            else if (iReturn == -3)
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Password Already Exist');</script>");

            }
        }
        else
        {

            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Plz select Value');</script>");

        }

    }

    public void Resetall()
    {
        Txt_User_Name.Text = "";
        Txt_Password.Text = "";
        Chk_Menu.SelectedIndex = -1;
    }
}

