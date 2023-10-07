using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
using System.Drawing;
public partial class DoctorBirthday_View : System.Web.UI.Page
{
    DataSet dsListedDR = null;
    DataSet dsDivision = null;
    DataSet dsdiv = new DataSet();
    string div_code = string.Empty;

    string request_doctor = string.Empty;
    string sf_code = string.Empty;
    string sf_type = string.Empty;
    string doctorcode = string.Empty;
    Product prd = new Product();
    string strMultiDiv = string.Empty;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int time;
    protected void Page_Load(object sender, EventArgs e)
    {
     
        sf_code = Session["sf_code"].ToString();
        sf_type = Session["sf_type"].ToString();
        if (sf_type == "3")
        {
            div_code = Session["division_code"].ToString();
        }
        else
        {
            div_code = Session["div_code"].ToString();
        }
      
        LblUser.Text = "Field Force Name : " + Session["sf_name"];
        //  doctorcode = Request.QueryString["ListedDrCode"];

        if (!Page.IsPostBack)
        {
            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);
            if (Session["sf_type"].ToString() == "" || Session["sf_type"].ToString() == null || Session["sf_type"].ToString() == "3")
            {
                Filldiv();
                ddlDivision.SelectedIndex = 1;
                LblUser.Visible = false;
                ddlMonth.SelectedValue = DateTime.Now.Month.ToString();
                Dob_View();
                Dow_View();
                DobDow_View();
            }


            else if (Session["sf_type"].ToString() == "1")
            {
                ddlDivision.Visible = false;
                lblDivision.Visible = false;
                ddlMonth.SelectedValue = DateTime.Now.Month.ToString();
                Dob_View();
                Dow_View();
                DobDow_View();
            }
            else if (Session["sf_type"].ToString() == "2")
            {
                ddlMonth.SelectedValue = DateTime.Now.Month.ToString();
                Dob_View();
                Dow_View();
                DobDow_View();
                Product prd = new Product();
                DataSet dsdiv = new DataSet();
                dsdiv = prd.getMultiDivsf_Name(sf_code);
                if (dsdiv.Tables[0].Rows.Count > 0)
                {
                    if (dsdiv.Tables[0].Rows[0]["IsMultiDivision"].ToString() == "1")
                    {
                        strMultiDiv = dsdiv.Tables[0].Rows[0][1].ToString().Remove(dsdiv.Tables[0].Rows[0][1].ToString().Length - 1, 1);
                        ddlDivision.Visible = true;
                        lblDivision.Visible = true;
                        Filldiv();
                   
                    }
                    else
                    {
                        btnGo.Visible = true;
                        ddlDivision.Visible = false;
                        lblDivision.Visible = false;
                     
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
    private void Filldiv()
    {
        Division dv = new Division();
        DataSet dsDivision = new DataSet();
        if (sf_type == "3")
        {
            string[] strDivSplit = div_code.Split(',');
            foreach (string strdiv in strDivSplit)
            {
                if (strdiv != "")
                {
                    dsdiv = dv.getDivisionHO(strdiv);
                    ListItem liTerr = new ListItem();
                    liTerr.Value = dsdiv.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                    liTerr.Text = dsdiv.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                    ddlDivision.Items.Add(liTerr);
                }
            }
        }
        else
        {
            if (strMultiDiv != "")
            {
                dsDivision = dv.getMultiDivision(strMultiDiv);
            }
            else
            {
                dsDivision = dv.getDivision_Name();
            }
            if (dsDivision.Tables[0].Rows.Count > 0)
            {
                ddlDivision.DataTextField = "Division_Name";
                ddlDivision.DataValueField = "Division_Code";
                ddlDivision.DataSource = dsDivision;
                ddlDivision.DataBind();
            }

        }
    }


    private void Dob_View()
    {

       
        if (Session["sf_type"].ToString() == "1")
        {
            ListedDR lst = new ListedDR();
            dsListedDR = lst.ViewListedDr_Dob(sf_code, ddlMonth.SelectedValue.ToString(), txtDate.Text);
            if (dsListedDR.Tables[0].Rows.Count > 0)
            {
                grdDoctor.DataSource = dsListedDR;
                grdDoctor.DataBind();
            }
            else
            {
                grdDoctor.DataSource = dsListedDR;
                grdDoctor.DataBind();
            }
        }
        else
        {
            if (Session["sf_type"].ToString() == "2")
            {
                dsdiv = prd.getMultiDivsf_Name(sf_code);
                if (dsdiv.Tables[0].Rows.Count > 0)
                {
                    if (dsdiv.Tables[0].Rows[0]["IsMultiDivision"].ToString() == "1")
                    {
                        div_code = ddlDivision.SelectedValue;
                    }

                }
                else
                {
                    div_code = Session["div_code"].ToString();
                }
            }
            else
            {
                div_code = ddlDivision.SelectedValue.ToString();
            }
            ListedDR lst = new ListedDR();
            dsListedDR = lst.doc_dob(sf_code, ddlMonth.SelectedValue.ToString(), txtDate.Text, div_code);
            if (dsListedDR.Tables[0].Rows.Count > 0)
            {
                grdDoctor.DataSource = dsListedDR;
                grdDoctor.DataBind();
            }
            else
            {
                grdDoctor.DataSource = dsListedDR;
                grdDoctor.DataBind();
            }
        }
    }
    private void Dow_View()
    {
        if (Session["sf_type"].ToString() == "1")
        {

            ListedDR lst = new ListedDR();
            dsListedDR = lst.ViewListedDr_Dow(sf_code, ddlMonth.SelectedValue.ToString(), txtDate.Text);
            if (dsListedDR.Tables[0].Rows.Count > 0)
            {
                grdDoctor_Dow.DataSource = dsListedDR;
                grdDoctor_Dow.DataBind();
            }
            else
            {
                grdDoctor_Dow.DataSource = dsListedDR;
                grdDoctor_Dow.DataBind();
            }
        }
        else
        {
            if (Session["sf_type"].ToString() == "2")
            {
                dsdiv = prd.getMultiDivsf_Name(sf_code);
                if (dsdiv.Tables[0].Rows.Count > 0)
                {
                    if (dsdiv.Tables[0].Rows[0]["IsMultiDivision"].ToString() == "1")
                    {
                        div_code = ddlDivision.SelectedValue;
                    }

                }
                else
                {
                    div_code = Session["div_code"].ToString();
                }
            }
            else
            {
                div_code = ddlDivision.SelectedValue.ToString();
            }
            ListedDR lst = new ListedDR();
            dsListedDR = lst.doc_dow(sf_code, ddlMonth.SelectedValue.ToString(), txtDate.Text, div_code);
            if (dsListedDR.Tables[0].Rows.Count > 0)
            {
                grdDoctor_Dow.DataSource = dsListedDR;
                grdDoctor_Dow.DataBind();
            }
            else
            {
                grdDoctor_Dow.DataSource = dsListedDR;
                grdDoctor_Dow.DataBind();
            }
        }

    }
    private void DobDow_View()
    {
        if (Session["sf_type"].ToString() == "1")
        {

            ListedDR lst = new ListedDR();
            dsListedDR = lst.ViewListedDr_DobDow(sf_code, ddlMonth.SelectedValue.ToString(), txtDate.Text);
            if (dsListedDR.Tables[0].Rows.Count > 0)
            {
                grdDobDow.DataSource = dsListedDR;
                grdDobDow.DataBind();
            }
            else
            {
                grdDobDow.DataSource = dsListedDR;
                grdDobDow.DataBind();
            }
        }
        else
        {
            if (Session["sf_type"].ToString() == "2")
            {
                dsdiv = prd.getMultiDivsf_Name(sf_code);
                if (dsdiv.Tables[0].Rows.Count > 0)
                {
                    if (dsdiv.Tables[0].Rows[0]["IsMultiDivision"].ToString() == "1")
                    {
                        div_code = ddlDivision.SelectedValue;
                    }

                }
                else
                {
                    div_code = Session["div_code"].ToString();
                }
            }
            else
            {
                div_code = ddlDivision.SelectedValue.ToString();
            }
            ListedDR lst = new ListedDR();
            dsListedDR = lst.doc_dob_dow(sf_code, ddlMonth.SelectedValue.ToString(), txtDate.Text, div_code);
            if (dsListedDR.Tables[0].Rows.Count > 0)
            {
                grdDobDow.DataSource = dsListedDR;
                grdDobDow.DataBind();
            }
            else
            {
                grdDobDow.DataSource = dsListedDR;
                grdDobDow.DataBind();
            }
        }
    }
    protected void grdDoctor_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblDOB = (Label)e.Row.FindControl("lblDOB");
            if (lblDOB.Text == "01/Jan/1900")
            {
                lblDOB.Text = "";
            }
          //  Label lblDOB = (Label)e.Row.FindControl("lblDOB");
            if (lblDOB != null)
            {
               
            
                DateTime holiday = Convert.ToDateTime(lblDOB.Text.ToString());

                //string Holi_Month = HolidayDate.Text.Substring(3, 2);
                string Holi_Month =holiday.Day + "/" + holiday.Month.ToString();
                string cur_Month = DateTime.Now.Day.ToString() +"/"+ DateTime.Now.Month.ToString();

                if (Holi_Month == cur_Month)
                //if (Holi_Month == DateTime.Now.ToString().Substring(3,2))
                {
                    //gridRow.BackColor = Color.Red;
                    e.Row.BackColor = Color.LightBlue;
                }
            }
        }
        if (e.Row.RowType == DataControlRowType.Header)
        {
            Territory terr = new Territory();
            DataSet dsTerritory = new DataSet();
            dsTerritory = terr.getWorkAreaName(ddlDivision.SelectedValue.ToString());
            if (dsTerritory.Tables[0].Rows.Count > 0)
            {
                e.Row.Cells[7].Text = dsTerritory.Tables[0].Rows[0]["wrk_area_Name"].ToString();            
            }
        }
    }
    protected void grdDoctor_Dow_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblDOW = (Label)e.Row.FindControl("lblDOW");
            if (lblDOW.Text == "01/Jan/1900")
            {
                lblDOW.Text = "";
            }
            if (lblDOW != null)
            {


                DateTime holiday = Convert.ToDateTime(lblDOW.Text.ToString());

                //string Holi_Month = HolidayDate.Text.Substring(3, 2);
                string Holi_Month = holiday.Day + "/" + holiday.Month.ToString();
                string cur_Month = DateTime.Now.Day.ToString() + "/" + DateTime.Now.Month.ToString();

                if (Holi_Month == cur_Month)
                //if (Holi_Month == DateTime.Now.ToString().Substring(3,2))
                {
                    //gridRow.BackColor = Color.Red;
                    e.Row.BackColor = Color.LightBlue;
                }
            }
        }
        if (e.Row.RowType == DataControlRowType.Header)
        {

            Territory terr = new Territory();
            DataSet dsTerritory = new DataSet();
            dsTerritory = terr.getWorkAreaName(ddlDivision.SelectedValue.ToString());
            if (dsTerritory.Tables[0].Rows.Count > 0)
            {
                e.Row.Cells[7].Text = dsTerritory.Tables[0].Rows[0]["wrk_area_Name"].ToString();                       


            }
        }
    }
    protected void grdDobDow_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblDOB = (Label)e.Row.FindControl("lblDOB1");
            Label lblDOW = (Label)e.Row.FindControl("lblDOW1");
            if (lblDOB.Text == "01/Jan/1900" || lblDOW.Text == "01/Jan/1900")
            {
                lblDOB.Text = "";
                lblDOW.Text = "";
            }

        }
        if (e.Row.RowType == DataControlRowType.Header)
        {

            Territory terr = new Territory();
            DataSet dsTerritory = new DataSet();
            dsTerritory = terr.getWorkAreaName(ddlDivision.SelectedValue.ToString());
            if (dsTerritory.Tables[0].Rows.Count > 0)
            {
                e.Row.Cells[7].Text = dsTerritory.Tables[0].Rows[0]["wrk_area_Name"].ToString();             

            }
        }
    }
    protected void btnGo_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        Dob_View();
        Dow_View();
        DobDow_View();

    }
    protected void ddlDivision_SelectedIndexChanged(object sender, EventArgs e)
    {
      
    }
}