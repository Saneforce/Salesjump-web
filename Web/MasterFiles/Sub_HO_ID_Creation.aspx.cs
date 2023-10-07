using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;

public partial class MasterFiles_Sub_HO_ID_Creation : System.Web.UI.Page
{
    DataSet dsDivision = null;
    DataSet dsSales = null;
    DataSet dsHODivision = null;

    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int time;
    int iIndex = -1;
    int iLength = -1;
    string divcode = string.Empty;
    string HO_ID = string.Empty;
    string Ho_Id = string.Empty;
    string HO_div_code = string.Empty;
    string division_code = string.Empty;
    string sf_type = string.Empty;
    string div_code = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        // divcode = Convert.ToString(Session["div_code"]);
        Session["backurl"] = "Sub_HO_ID_View.aspx";
        sf_type = Session["sf_type"].ToString();
        Ho_Id = Session["HO_ID"].ToString();
        if (sf_type == "3")
        {
            div_code = Session["division_code"].ToString();
        }
        else
        {
            div_code = Session["div_code"].ToString();
        }
        HO_ID = Request.QueryString["HO_ID"];
        txtName.Focus();
        //div_code = Request.QueryString["division_code"];

        if (!Page.IsPostBack)
        {
            
            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);
            menu1.Title = this.Page.Title;
            if ((HO_ID != "") && (HO_ID != null))
            {
                menu1.Title = this.Page.Title;
                //Session["backurl"] = "Sub_HO_ID_View.aspx";

                SalesForce sale = new SalesForce();
                dsSales = sale.get_Sub_Ho_Id(HO_ID);

                if (dsSales.Tables[0].Rows.Count > 0)
                {
                    txtName.Text = dsSales.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                    txtUserName.Text = dsSales.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                    txtPassword.Text = dsSales.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();

                    string strPassword;
                    strPassword = txtPassword.Text;
                    txtPassword.Attributes.Add("value", strPassword);

                    division_code = dsSales.Tables[0].Rows[0].ItemArray.GetValue(3).ToString();
                }
                //if (sf_type == "")
                //{
                //    FillCheckBoxList(HO_ID);
                //}
                if (sf_type == "3")
                {
                    FillCheckBoxList(HO_ID);
                }
            }
            else
            {
                FillDiv();
            }
        }
    }

   
    private void FillCheckBoxList(string HO_ID)
    {
        Division dv = new Division();
        dsHODivision = dv.getSubHODiv(Ho_Id, div_code);
        chkDivision.DataTextField = "Division_Name";
        chkDivision.DataSource = dsHODivision;
        chkDivision.DataBind();
        string[] div;
        if (division_code != "")
        {
            iIndex = -1;
            div = division_code.Split(',');
            foreach (string code in div)
            {
                for (iIndex = 0; iIndex < chkDivision.Items.Count; iIndex++)
                {
                    if (code == chkDivision.Items[iIndex].Value)
                    {

                        chkDivision.Items[iIndex].Selected = true;
                        chkDivision.Items[iIndex].Attributes.Add("style", "Color: Red;font-weight:Bold");

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

    private void FillDiv()
    {

        Division dv = new Division();
        dsHODivision = dv.getSubHODivision(Ho_Id, div_code);
        chkDivision.DataTextField = "Division_Name";
        chkDivision.DataSource = dsHODivision;
        chkDivision.DataBind();
        //string[] div;
        //if (div_code != "")
        //{
        //    iIndex = -1;
        //    div = div_code.Split(',');
        //    foreach (string code in div)
        //    {
        //        for (iIndex = 0; iIndex < chkDivision.Items.Count; iIndex++)
        //        {
        //            if (code == chkDivision.Items[iIndex].Value)
        //            {

        //                chkDivision.Items[iIndex].Selected = true;
        //                chkDivision.Items[iIndex].Attributes.Add("style", "Color: Red;font-weight:Bold");

        //            }

        //        }
        //    }
        //}
    }
        

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        string div_code = string.Empty;
        int i;

        for (i = 0; i < chkDivision.Items.Count; i++)
        {
            if (chkDivision.Items[i].Selected)
                div_code = div_code + chkDivision.Items[i].Value.ToString() + ",";
        }


        if (HO_ID == null)
        {
            SalesForce sf = new SalesForce();
            int iReturn = sf.Sub_HO_ID_RecordAdd(txtUserName.Text.Trim(), txtPassword.Text.Trim(), txtName.Text.Trim(),"", div_code,"","", Ho_Id);
            if (iReturn > 0)
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('HO ID created successfully');</script>");
                Reset_Controls();
            }
            else if (iReturn == -2)
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('User Name Already Exist');</script>");
                txtUserName.Focus();
            }
        }

        else
        {
            SalesForce sf = new SalesForce();
            int HO_Id = Convert.ToInt16(HO_ID);
            int iReturn = sf.Update_Sub_HO_Id(HO_Id, txtName.Text, txtUserName.Text, txtPassword.Text, div_code);
            if (iReturn > 0)
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Updated Successfully');window.location='Sub_HO_ID_View.aspx';</script>");
            }
            else if (iReturn == -2)
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('User Name Already Exist');</script>");
                txtUserName.Focus();
            }
        }
    }

    private void Reset_Controls()
    {
        txtName.Text = "";
        txtPassword.Text = "";
        txtUserName.Text = "";

        for (int i = 0; i < chkDivision.Items.Count; i++)
        {
            chkDivision.Items[i].Selected = false;
        }
    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        Reset_Controls();
    }
}