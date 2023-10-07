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
using Bus_EReport;
using System.Web.Services;
using System.Web.Script.Services;
using System.Collections.Generic;

public partial class MasterFiles_Options_Quiz_Questions : System.Web.UI.Page
{
    SqlConnection con;
    DataSet ds;
    SqlDataAdapter da;
    SqlCommand cmd;
    DataSet dsProduct = null;
    string div_code = string.Empty;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    static int surveyID;
    int time;
    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();


        surveyID = Convert.ToInt32(Request.QueryString["Survey_Id"]);

        // Session["backurl"] = "AddQuizQuestions.aspx?surveyId=" + hidSurveyId.Value + "";

        Session["backurl"] = "Quiz_List.aspx";

        if (!Page.IsPostBack)
        {
            //menu1.Title = this.Page.Title;
            if (Request.QueryString["Survey_Id"] != null)
            {
                hidSurveyId.Value = Request.QueryString["Survey_Id"].ToString();

                Session["Survey_Id"] = hidSurveyId.Value;
            }
            LoadQuestionType();
            //    FillProd();
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
    private void LoadQuestionType()
    {
        con = new SqlConnection(WebGlobals.ConnString);
        cmd = new SqlCommand("select Question_Type_Id,Question_Type_Name from QuestionType where Division_Code='" + div_code + "' ", con);
        da = new SqlDataAdapter(cmd);
        ds = new DataSet();
        da.Fill(ds);
        ddlQuestionType.DataSource = ds;
        ddlQuestionType.DataValueField = ds.Tables[0].Columns["Question_Type_Id"].ColumnName;
        ddlQuestionType.DataTextField = ds.Tables[0].Columns["Question_Type_Name"].ColumnName;

        ddlQuestionType.DataBind();
        //ListItem li = new ListItem();
        //li.Text = "-- Select Question Type--";
        //li.Value = "0";
        //ddlQuestionType.Items.Insert(0, li);
    }
    public class QuestionDetail
    {
        public int QuestionTypeId { get; set; }
        public string QuestionText { get; set; }
        public string InputOption { get; set; }
        public string CorrectAnswer { get; set; }

    }
    [WebMethod(EnableSession = true)]
    public static void AddQuestionData(QuestionDetail QueData)
    {
        QuestionDetail objData = new QuestionDetail();

        string DivCode = HttpContext.Current.Session["div_code"].ToString();

        string surveyID = HttpContext.Current.Session["Survey_Id"].ToString();

        //objData.SurveyId = surveyID;
        objData.QuestionTypeId = QueData.QuestionTypeId;
        objData.QuestionText = QueData.QuestionText;
        objData.InputOption = QueData.InputOption;
        objData.CorrectAnswer = QueData.CorrectAnswer;

        //if (objData.QuestionTypeId == 3)
        //{
        //    objData.InputOption = "";
        //}
        //else
        //{

        SqlConnection con = new SqlConnection(Globals.ConnString);
        con.Open();
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandType = CommandType.StoredProcedure;
        //  cmd.CommandText = "AddSurveyQuestions";
        cmd.CommandText = "AddSurveyQuestions_Answer";
        cmd.Parameters.Add(new SqlParameter("@SurveyID", surveyID));
        cmd.Parameters.Add(new SqlParameter("@Question_Type_Id", objData.QuestionTypeId));
        cmd.Parameters.Add(new SqlParameter("@Question_Text", objData.QuestionText));
        cmd.Parameters.Add(new SqlParameter("@inputOptions", objData.InputOption));
        cmd.Parameters.Add(new SqlParameter("@div_code", DivCode));
        cmd.Parameters.Add(new SqlParameter("@Correct_Ans", objData.CorrectAnswer));

        if (cmd.ExecuteNonQuery() > 0)
        {
            con.Close();
        }

        // }

        // return SurverId;
    }

}


