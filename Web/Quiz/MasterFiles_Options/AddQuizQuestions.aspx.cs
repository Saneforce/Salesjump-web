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

public partial class MasterFiles_Options_AddQuizQuestions : System.Web.UI.Page
{

    SqlDataAdapter da;
    DataSet ds;
    SqlCommand cmd;
    SqlConnection con;
    DataSet dsSalesForce = null;
    string div_code = string.Empty;
    string sf_code = string.Empty;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int time;


    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        sf_code = Session["sf_code"].ToString();
        Session["backurl"] = "Quiz_List.aspx";

        if (!Page.IsPostBack)
        {
            //menu1.Title = this.Page.Title;
            if (Request.QueryString["surveyId"] != null)
                hidSurveyId.Value = Request.QueryString["surveyId"].ToString();

            //Session["SurveyId"] = Request.QueryString["surveyId"].ToString();

            isEditMode = false;

            LoadSurveyQuestions();
            this.FillMasterList();
        }
    }
    private void LoadSurveyQuestions()
    {

        // int surveyID = Convert.ToInt32(hidSurveyId.Value);

        try
        {
            //int surveyID = Convert.ToInt32(Session["SurveyId"].ToString());
			int surveyID = Convert.ToInt32(hidSurveyId.Value);
            SqlDataAdapter daSurveyQuest;
            SqlConnection con = new SqlConnection(Globals.ConnString);

            con.Open();
            SqlCommand cmdSurveyQuest = new SqlCommand("select a.Question_type_id,Question_Type_Name,a.Question_Id,Question_Text, b.Input_Text  from AddQuestions a inner join QuestionType q on a.Question_type_id=q.Question_type_id inner join AddInputOptions b on b.Question_Id = a.Question_Id and b.Correct_Ans=1 and SurveyId=" + surveyID + "", con);
            DataSet dsSurveyQuest = new DataSet();
            daSurveyQuest = new SqlDataAdapter(cmdSurveyQuest);
            daSurveyQuest.Fill(dsSurveyQuest);
            grdSurveyQuestions.DataSource = dsSurveyQuest;
            grdSurveyQuestions.DataBind();
            con.Close();
            cmdSurveyQuest.Dispose();
        }
        catch (Exception ex)
        {
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

                if (!(bool)IsInEditMode == true)
                {
                    RadioButtonList rbl = new RadioButtonList();
                    cmd = new SqlCommand("select Input_id,Input_Text,Correct_Ans from AddInputOptions where Question_id=" + questionId + "", con);
                    da = new SqlDataAdapter(cmd);
                    ds = new DataSet();
                    da.Fill(ds);
                    rbl.DataSource = ds;
                    rbl.DataValueField = ds.Tables[0].Columns[0].ColumnName;
                    rbl.DataTextField = ds.Tables[0].Columns[1].ColumnName;

                    rbl.Visible = !(bool)IsInEditMode;

                    // rbl.AutoPostBack = true;


                    //  raid.CheckedChanged += Radio_CheckedChanged;  

                    //  raid.CheckedChanged += (s,e) => {raid.AutoPostBack = true;}


                    //  rbl.SelectedIndexChanged += new System.EventHandler(this.Radio_CheckedChanged);

                    rbl.DataBind();
                    pnl.Controls.Add(rbl);
                }
                else
                {
                    //TextBox txt = new TextBox();
                    cmd = new SqlCommand("select Input_id,Input_Text from AddInputOptions where Question_id=" + questionId + "", con);
                    da = new SqlDataAdapter(cmd);
                    ds = new DataSet();
                    da.Fill(ds);

                    //txt.Text = ds.Tables[0].Columns[1].ColumnName;
                    //txt.Visible = IsInEditMode;
                    //pnl.Controls.Add(txt);

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
                        raid.Visible = IsInEditMode;
                        raid.GroupName = "RbtOpt";
                        // raid.AutoPostBack = true;


                        //  raid.CheckedChanged += Radio_CheckedChanged;  

                        //  raid.CheckedChanged += (s,e) => {raid.AutoPostBack = true;}


                        //   raid.CheckedChanged += new System.EventHandler(this.Radio_CheckedChanged);



                        TextBox txt = new TextBox();
                        txt.ID = "txt" + i;
                        txt.CssClass = "TexOption";
                        string strVal = ds.Tables[0].Rows[i]["Input_Text"].ToString();
                        txt.Text = strVal;
                        txt.Visible = IsInEditMode;

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

    }


    protected void Radio_CheckedChanged(object sender, EventArgs e)
    {
        RadioButton rb = new RadioButton();

        for (int i = 0; i < grdSurveyQuestions.Rows.Count; i++)
        {
            Panel pnl = (grdSurveyQuestions.Rows[i].FindControl("pnlAnswerOptions")) as Panel;

            //if (rb.Checked == true)
            //{
            //    txtEventId.Text = grdSurveyQuestions.Rows[i].Cells[1].Text;
            //}

            foreach (Panel pn in pnl.Controls)
            {
                foreach (TableRow tr in pn.Controls)
                {
                    foreach (TableCell tc in tr.Controls)
                    {
                        if (tc.Controls[0] is RadioButton)
                        {
                            rb = (RadioButton)tc.Controls[0];
                            if (rb.Checked)
                            {
                                string aa = rb.ID;
                            }
                            break;
                        }
                    }
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
                //  DisplayMessageAddRedirect("Selected Questions deleted successfully", "AddSurveyQuestionList.aspx?surveyId=" + hidSurveyId.Value + "");

            }
            catch (Exception ex)
            {
                //   DisplayMessage("Error on Page");
            }
        }
    }
    public void grdSurveyQuestions_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
    }

    protected void ImageButton1_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        Response.Redirect("Quiz_Questions.aspx?surveyId=" + hidSurveyId.Value + "");
    }
    protected void btn_Click(object sender, EventArgs e)
    {
        //string strDate = txtEffFrom.Text;
        //string[] arrDate = strDate.Split('/');
        //string Month = arrDate[0].ToString();
        //string day = arrDate[1].ToString();
        //string Year = arrDate[2].ToString();
        //int surveyID = Convert.ToInt32(hidSurveyId.Value);

        // Response.Redirect("Quiz_Process.aspx?Survey_Id=" + surveyID + "&Month=" + Month + "&Year=" + Year + "");

    }


    private bool isEditMode = false;

    protected bool IsInEditMode
    {

        get { return this.isEditMode; }

        set { this.isEditMode = value; }

    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        isEditMode = true;

        LoadSurveyQuestions();

    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow gvr in grdSurveyQuestions.Rows)
        {

            Panel pnl = (Panel)gvr.FindControl("pnlAnswerOptions");

            TextBox txtQuestion = (TextBox)gvr.FindControl("txtQuestion");

            Label lblCorrectAns = (Label)gvr.FindControl("lblCorrectAns");

            int Count = Convert.ToInt32(Session["Count"]);

            for (int i = 0; i < Count; i++)
            {
                RadioButton rbbtn = (RadioButton)pnl.FindControl("raid" + i);
                TextBox txtOpt = (TextBox)pnl.FindControl("txt" + i);

                // string txt = txtOpt.Text;
            }

            string Question = txtQuestion.Text;
            string CorrectAns = lblCorrectAns.Text;
        }
    }
    [WebMethod(EnableSession = true)]
    public static string UpdateQusData(List<string> ObjQusData)
    {
        string DivCode = HttpContext.Current.Session["div_code"].ToString();
        string SurveyId = HttpContext.Current.Session["SurveyId"].ToString();

        string U_Data = string.Empty;
        string InpOpt = string.Empty;
        string CrtAns = string.Empty;

        for (int i = 0; i < ObjQusData.Count; i++)
        {
            U_Data = ObjQusData[i].ToString();
            string[] values = U_Data.Split('/');

            EditQuestion objData = new EditQuestion();

            objData.Question = values[0].ToString();
            objData.AnsOption = values[1].ToString();
            objData.ConrrectAns = values[2].ToString();
            objData.QueID = Convert.ToInt32(values[3].ToString());

            Product objInput = new Product();
            DataSet dsOption = new DataSet();

            dsOption = objInput.GetQuizInputOption(objData.QueID, DivCode);

            InpOpt = objData.AnsOption;
            string[] Option = InpOpt.Split(',');
            CrtAns = objData.ConrrectAns.Trim();

            int _QueRes = objInput.Quiz_Update_Question(objData.QueID, objData.Question, DivCode);

            for (int j = 0; j < dsOption.Tables[0].Rows.Count; j++)
            {
                int InputId = Convert.ToInt32(dsOption.Tables[0].Rows[j]["Input_Id"].ToString());
                string CorrectAns = dsOption.Tables[0].Rows[j]["Correct_Ans"].ToString();

                if (Option[j] == CrtAns)
                {
                    objData.CorrectAnswer = "1";
                }
                else
                {
                    objData.CorrectAnswer = "0";
                }

                int _Res = objInput.QuizOption_Update(objData.QueID, InputId, Option[j], objData.CorrectAnswer);

            }

        }

        return SurveyId;

    }
    public class EditQuestion
    {
        public string AnsOption { get; set; }
        public string Question { get; set; }
        public string ConrrectAns { get; set; }
        public int QueID { get; set; }
        public string CorrectAnswer { get; set; }
    }
}

