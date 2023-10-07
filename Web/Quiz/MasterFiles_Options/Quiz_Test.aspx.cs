using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Net.Mail;
using Bus_EReport;
using System.Web.Services;
using System.Web.Script.Services;
using System.Web.Script.Serialization;
using System.Collections.Generic;
using System.Text;

public partial class MasterFiles_Options_Quiz_Test : System.Web.UI.Page
{
    string div_code;
    string sf_code;
    string sf_Name;
    string surveyID = string.Empty;
    DataSet dsatt = new DataSet();
    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        sf_code = Session["sf_code"].ToString();
        sf_Name = Session["sf_name"].ToString();
        if (!Page.IsPostBack)
        {
            LblUser.Text = "Welcome " + Session["sf_name"] + " - " + Session["Designation_Short_Name"] + " - " + Session["Sf_HQ"];
            lbldiv.Text = Session["div_name"].ToString();

            if (Request.QueryString["Survey_Id"] != "" || Request.QueryString["Survey_Id"] != null)
            {
                surveyID = Request.QueryString["Survey_Id"].ToString();
                Session["SurveyID"] = surveyID.Trim(' ');
            }
        }
    }

    protected void btnHomepage_Click(object sender, EventArgs e)
    {
        AdminSetup dv1 = new AdminSetup();

        string surveyID = Session["SurveyID"].ToString();

        DataSet dsquiz = dv1.GetSecondQuiz(div_code, sf_code, surveyID);
        string Survey_Id = dsquiz.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
        dsatt = dv1.Quiz_Attempt(div_code, sf_code, surveyID);
        if (dsatt.Tables[0].Rows[0].ItemArray.GetValue(0).ToString() == "1")
        {
            if (Session["sf_type"].ToString() == "2") // MGR Login
            {
                Response.Redirect("~/Default_MGR.aspx");

            }
            else
            {
                Response.Redirect("~/Default_MR.aspx");
            }
        }
        else if (dsatt.Tables[0].Rows[0].ItemArray.GetValue(0).ToString() == "2")
        {
            if (dsquiz.Tables[0].Rows[0].ItemArray.GetValue(0).ToString() == "1")
            {

                Response.Redirect("~/Cover_Page.aspx?Survey_Id=" + dsquiz.Tables[0].Rows[0]["Survey_Id"].ToString() + " &res=" + dsquiz.Tables[0].Rows[0]["Status"].ToString() + "");
            }
            else
            {
                if (Session["sf_type"].ToString() == "2") // MGR Login
                {
                    Response.Redirect("~/Default_MGR.aspx");

                }
                else
                {
                    Response.Redirect("~/Default_MR.aspx");
                }
            }
        }
        else
        {

            if (Session["sf_type"].ToString() == "2") // MGR Login
            {
                Response.Redirect("~/Default_MGR.aspx");

            }
            else
            {
                Response.Redirect("~/Default_MR.aspx");
            }
        }
    }

}


