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
using System.Data.SqlClient;
using System.Data;
using Bus_EReport;

public partial class MasterFiles_Options_Quiz_List : System.Web.UI.Page
{
    int SurveyId;
    string div_code = string.Empty;
	   string sf_type = string.Empty;
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
        div_code = Session["div_code"].ToString();
        if (!IsPostBack)
        {
            if (Request.QueryString["posted"] != null && Request.QueryString["posted"].ToString() == "1")
            {
             //   DisplayMessage("Survey Posted Successfully");
            }
            LoadSurveyList();
            //menu1.Title = this.Page.Title;

        }
    }

    private void LoadSurveyList()
    {
      //  int userID = Convert.ToInt32(Session["User_id"]);

        string lblProcessed;

        SqlDataAdapter daSurvey;
        SqlConnection con = new SqlConnection(Globals.ConnString);
        con.Open();
        SqlCommand cmdSurvey = new SqlCommand("select Survey_Id,Quiz_Title,Created_Date,[Month],[YEAR], " +
                                              "(select  top(1) convert(char(10),From_date,105) from Processing_UserList where  SurveyId=Survey_Id ) as From_date, " +
                                              "(select  top(1) convert(char(10),To_Date,105) from Processing_UserList where  SurveyId=Survey_Id ) as To_Date, " +
                                              " REPLACE(SUBSTRING([FilePath], CHARINDEX(')', [FilePath]), LEN([FilePath])), ')', '') as FilePath " +
                                              " from QuizTitleCreation where Division_code='" + div_code + "' and Active=0 order by Created_Date desc ", con);
        DataSet dsSurvey = new DataSet();
        daSurvey = new SqlDataAdapter(cmdSurvey);
        daSurvey.Fill(dsSurvey);
        DataColumn dc = new DataColumn("Processed");
        dsSurvey.Tables[0].Columns.Add(dc);

        DataColumn dc1 = new DataColumn("NQues");
        dsSurvey.Tables[0].Columns.Add(dc1);

        if (dsSurvey.Tables[0].Rows.Count > 0)
        {

            for (int i = 0; i < dsSurvey.Tables[0].Rows.Count; i++)
            {
                int SurveyId = Convert.ToInt32(dsSurvey.Tables[0].Rows[i]["Survey_Id"].ToString());

                string _qry = "select * from dbo.Processing_UserList where SurveyId=" + SurveyId + "";

                cmdSurvey = new SqlCommand(_qry, con);

                DataSet dsProcess = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(cmdSurvey);
                da.Fill(dsProcess);

                if (dsProcess.Tables[0].Rows.Count > 0)
                {
                    lblProcessed = "Processed";
                    dsSurvey.Tables[0].Rows[i]["Processed"] = lblProcessed;
                }
                else
                {
                    lblProcessed = "";
                    dsSurvey.Tables[0].Rows[i]["Processed"] = lblProcessed;
                }


                string _Qry1 = "select COUNT(SurveyID)as NQues from dbo.AddQuestions where SurveyID=" + SurveyId + "";

                cmdSurvey = new SqlCommand(_Qry1, con);

                DataSet dsNQA = new DataSet();
                SqlDataAdapter da1 = new SqlDataAdapter(cmdSurvey);
                da1.Fill(dsNQA);


                if (dsNQA.Tables[0].Rows.Count > 0)
                {
                    string val = dsNQA.Tables[0].Rows[0]["NQues"].ToString();

                    dsSurvey.Tables[0].Rows[i]["NQues"] = val;
                }
                else
                {
                    string val = "";
                    dsSurvey.Tables[0].Rows[i]["NQues"] = val;

                }

            }
        }

        grdSurvey.DataSource = dsSurvey;
        grdSurvey.DataBind();

        con.Close();
        cmdSurvey.Dispose();
    }

    //protected void GridView1_PreRender(object sender, EventArgs e)
    //{
    //    grdSurvey.UseAccessibleHeader = false;
    //    grdSurvey.HeaderRow.TableSection = TableRowSection.TableHeader;
    //} 

    public void grdSurvey_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        //if (e.CommandName == "Delete")
        //{

        //    try
        //    {
        //        int surveyId = Convert.ToInt32(e.CommandArgument);
        //        SqlConnection con = new SqlConnection(Globals.ConnString);
        //        con.Open();
        //        SqlCommand cmd;
        //        cmd = new SqlCommand("delete from QuizTitleCreation where Survey_Id=@Survey_Id", con);
        //        cmd.Parameters.Add(new SqlParameter("@Survey_Id", SurveyId));
        //        cmd.ExecuteNonQuery();
        //      //  DisplayMessageAddRedirect("Selected Survey deleted successfully", "SurveyList.aspx");
        //    }
        //    catch (Exception ex)
        //    {

        //    }
       // }

        if (e.CommandName == "Deactivate")
        {
            
            int surveyId = Convert.ToInt16(e.CommandArgument);
           
            Product dv = new Product();
            int iReturn = dv.DeActivate_QuizTitle(surveyId);
            if (iReturn > 0)
            {
                ClientScript.RegisterStartupScript(GetType(), "TestAlert", "createCustomAlert(\'Deactivated Successfully.\');", true);
            }
            else
            {
                // menu1.Status ="Unable to Deactivate";
                ClientScript.RegisterStartupScript(GetType(), "TestAlert", "createCustomAlert(\'Unable to Deactivate.\');", true);
            }

            LoadSurveyList();

        }


    }
    public void grdSurvey_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }

    protected void btnADD_Click(object sender, EventArgs e)
    {
        Response.Redirect("QuizTitleCreation.aspx");
    }
}