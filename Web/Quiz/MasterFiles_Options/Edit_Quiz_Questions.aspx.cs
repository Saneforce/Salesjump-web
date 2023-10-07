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
using System.Web.Services;
using System.Web.Script.Services;
using System.Collections.Generic;
using Bus_EReport;

public partial class MasterFiles_Options_Edit_Quiz_Questions : System.Web.UI.Page
{

    SqlDataAdapter da;
    DataSet ds;
    SqlCommand cmd;
    SqlConnection con;
    DataSet dsSalesForce = null;
    string div_code = string.Empty;
    string sf_code = string.Empty;
	 string sf_type = string.Empty;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int time;
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
        sf_code = Session["sf_code"].ToString();

        if (!Page.IsPostBack)
        {
            //menu1.Title = this.Page.Title;
            if (Request.QueryString["surveyId"] != null)
                hidSurveyId.Value = Request.QueryString["surveyId"].ToString();

            Session["SurveyId"] = Request.QueryString["surveyId"].ToString();

            LoadSurveyQuestions();
            this.FillMasterList();
        }
    }


    private void LoadSurveyQuestions()
    {
        int surveyID = Convert.ToInt32(hidSurveyId.Value);
        SqlDataAdapter daSurveyQuest;
        SqlConnection con = new SqlConnection(Globals.ConnString);

        con.Open();
        SqlCommand cmdSurveyQuest = new SqlCommand("select a.Question_type_id,Question_Type_Name,a.Question_Id,Question_Text, b.Input_Text  from AddQuestions a inner join QuestionType q on a.Question_type_id=q.Question_type_id inner join AddInputOptions b on b.Question_Id = a.Question_Id and b.Correct_Ans=1 and SurveyId=" + surveyID + "", con);
        DataSet dsSurveyQuest = new DataSet();
        daSurveyQuest = new SqlDataAdapter(cmdSurveyQuest);
        daSurveyQuest.Fill(dsSurveyQuest);
        gvEditQuiz.DataSource = dsSurveyQuest;
        gvEditQuiz.DataBind();
        con.Close();
        cmdSurveyQuest.Dispose();
    }
    protected override void OnLoadComplete(EventArgs e)
    {

        ServerEndTime = DateTime.Now;
        TrackPageTime();
    }
    public void TrackPageTime()
    {
        TimeSpan serverTimeDiff = ServerEndTime.Subtract(ServerStartTime);
        time = serverTimeDiff.Minutes;
    }
    private void FillMasterList()
    {


    }
    protected void Submit(object sender, EventArgs e)
    {

    }
    public void grdSurveyQuestions_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            con = new SqlConnection(Globals.ConnString);
            con.Open();
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.DataItem != null)
            {
                int questionTypeId = Convert.ToInt32(((System.Data.DataRowView)(e.Row.DataItem)).Row.ItemArray[0]);
                int questionId = Convert.ToInt32(((System.Data.DataRowView)(e.Row.DataItem)).Row.ItemArray[2]);
                System.Web.UI.WebControls.Panel pnl = (System.Web.UI.WebControls.Panel)e.Row.FindControl("pnlAnswerOptions");

                cmd = new SqlCommand("select Input_id,Input_Text,Correct_Ans from AddInputOptions where Question_id=" + questionId + "", con);
                da = new SqlDataAdapter(cmd);
                ds = new DataSet();
                da.Fill(ds);

                Session["Count"] = ds.Tables[0].Rows.Count;

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    Table tbl = new Table();
                    TableRow tr = new TableRow();
                    tbl.ID = "tblOption" + i;
                    TableCell cl = new TableCell();
                    TableCell cl2 = new TableCell();
                    RadioButton raid = new RadioButton();
                    raid.ID = "raid" + i;
                    raid.GroupName = "RbtOpt";


                    TextBox txt = new TextBox();
                    txt.ID = "txt" + i;
                    txt.CssClass = "TexOption";
                    string strVal = ds.Tables[0].Rows[i]["Input_Text"].ToString();
                    txt.Text = strVal;


                    if (ds.Tables[0].Rows[i]["Correct_Ans"].ToString() == "1")
                    {
                        raid.Checked = true;
                    }

                    cl2.Controls.Add(raid);
                    cl.Controls.Add(txt);

                    tr.Controls.Add(cl2);
                    tr.Controls.Add(cl);

                    tbl.Controls.Add(tr);
                    pnl.Controls.Add(tbl);

                }

            }
        }

    }

    public void grdSurveyQuestions_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Delete")
        {
            try
            {
                int questionId = Convert.ToInt32(e.CommandArgument);
                SqlConnection con = new SqlConnection(Globals.ConnString);
                con.Open();
                SqlCommand cmd;
                cmd = new SqlCommand("delete from AddInputOptions where Question_Id=@Question_Id", con);
                cmd.Parameters.Add(new SqlParameter("@Question_Id", questionId));
                cmd.ExecuteNonQuery();


                cmd = new SqlCommand("delete from AddQuestions where Question_Id=@Question_Id", con);
                cmd.Parameters.Add(new SqlParameter("@Question_Id", questionId));
                cmd.ExecuteNonQuery();
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Selected Questions deleted successfully');window.location='AddQuizQuestions.aspx?surveyId=" + hidSurveyId.Value + "'</script>");


            }
            catch (Exception ex)
            {

            }
        }
    }
    public void grdSurveyQuestions_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }



}

