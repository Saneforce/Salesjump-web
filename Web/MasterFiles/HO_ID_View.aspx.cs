using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using Bus_EReport;

public partial class MasterFiles_HO_ID_View : System.Web.UI.Page
{

    #region "Declaration"
    DataSet dsHO = null;
    DataSet dsDivision = null;
    int Ho_id = 0;
    string divcode = string.Empty;
    string Name = string.Empty;
    string User_Name = string.Empty;
    string Rep_to = string.Empty;
    string Password = string.Empty;
    string sf_type = string.Empty;
    string div_code = string.Empty;
    string HO_ID = string.Empty;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int time;
    int iLength = -1;
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        
      //  divcode = Convert.ToString(Session["div_code"]);
            sf_type = Session["sf_type"].ToString();
            HO_ID = Session["HO_ID"].ToString();//changes done by reshmi
            if (sf_type == "3")
            {
                div_code = Session["division_code"].ToString();
            }
            else
            {
                div_code = Session["div_code"].ToString();
            }
        if (!Page.IsPostBack)
        {
            FillHo();
            menu1.Title = this.Page.Title;
            menu1.FindControl("btnBack").Visible = false;
            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);
            Session["backurl"] = "SalesForceList.aspx";
            if (Session["sf_type"].ToString() == "3")
            {
                btnNew.Visible = false;               
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

    protected void btnNew_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
       
        Response.Redirect("HO_ID_Creation.aspx");
    }

    private void FillHo()
    {
        SalesForce dv = new SalesForce();
        Division dv1 = new Division();
        if (sf_type == "3")
        {

            //iLength = div_code.Trim().Length;
            //div_code = div_code.Substring(0, iLength - 1);
            dsDivision = dv.getHO_new(div_code, HO_ID);
            if (dsDivision.Tables[0].Rows.Count > 0)
            {
                grdHoID.Visible = true;
                grdHoID.DataSource = dsDivision;
                grdHoID.DataBind();
            }
            else
            {
                grdHoID.DataSource = dsDivision;
                grdHoID.DataBind();
            }
            //}
        }


        else
        {
            dsHO = dv.getHO();
            if (dsHO.Tables[0].Rows.Count > 0)
            {
                grdHoID.Visible = true;
                grdHoID.DataSource = dsHO;
                grdHoID.DataBind();
            }
            else
            {
                grdHoID.DataSource = dsHO;
                grdHoID.DataBind();
            }
        }
    }
    protected void grdHoID_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Deactivate")
        {
            Ho_id  = Convert.ToInt16(e.CommandArgument);

            //Deactivate
            SalesForce  dv = new SalesForce ();
            int iReturn = dv.HO_DeActivate(Ho_id);
            if (iReturn > 0)
            {
                //menu1.Status = "Doctor Category has been Deactivated Successfully";
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Deactivated Successfully');</script>");
            }
            else
            {
                // menu1.Status = "Unable to Deactivate";
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Unable to Deactivate');</script>");
            }
            FillHo();

        }
    }
}