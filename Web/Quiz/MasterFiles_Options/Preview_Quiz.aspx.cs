using System;
using System.Collections;
using System.Configuration;
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
public partial class MasterFiles_Options_Preview_Quiz : System.Web.UI.Page
{
    SqlDataAdapter da;
    DataSet ds;
    SqlCommand cmd;
    SqlConnection con;
    int rowNo = 1;
    int iRet = -1;
    string div_code = string.Empty;
    string sf_code = string.Empty;
    string sf_Name = string.Empty;
    string surveyID = string.Empty;
    DataSet dsquiz = null;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int time;
    DateTime end_date;
    DateTime start_date;
    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        sf_code = Session["sf_code"].ToString();
        sf_Name = Session["sf_name"].ToString();
        //if (!SM1.IsInAsyncPostBack && (Session["timeout"] == null || Convert.ToString(Session["timeout"]).Trim() == ""))
        //    Session["timeout"] = DateTime.Now.AddMinutes(10).ToString();
        if (!Page.IsPostBack)
        {
            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);
            LblUser.Text = "Welcome " + Session["sf_name"] + " - " + Session["Designation_Short_Name"] + " - " + Session["Sf_HQ"];
            lbldiv.Text = Session["div_name"].ToString();
            if (Request.QueryString["Survey_Id"] != "" || Request.QueryString["Survey_Id"] != null)
            {
                surveyID = Request.QueryString["Survey_Id"].ToString();
            }
            start_date = DateTime.Now;
            ViewState["Start"] = start_date.ToString();
            LoadSurveyPreview();

            // LoadContactGroup();


        }
        //if (!SM1.IsInAsyncPostBack)

        //    Session["timeout"] = DateTime.Now.AddMinutes(10).ToString();
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
    private void LoadSurveyPreview()
    {
        
       //  int surveyID = Convert.ToInt32(hidSurveyId.Value);
        SqlDataAdapter daPreviewSurvey;
        SqlConnection con = new SqlConnection(Globals.ConnString);

        con.Open();
        SqlCommand cmdPreviewSurvey = new SqlCommand("select a.Question_type_id,Question_Id,Question_Text  from AddQuestions a inner join QuestionType q on a.Question_type_id=q.Question_type_id where SurveyId=" + surveyID + "", con);
        DataSet dsPreviewSurvey = new DataSet();
        daPreviewSurvey = new SqlDataAdapter(cmdPreviewSurvey);
        daPreviewSurvey.Fill(dsPreviewSurvey);
        grdPreviewQuestion.DataSource = dsPreviewSurvey;
        grdPreviewQuestion.DataBind();
        con.Close();
        cmdPreviewSurvey.Dispose();
    }
    public void grdPreviewSurvey_RowCreated(object sender, GridViewRowEventArgs e)
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
                int questionId = Convert.ToInt32(((System.Data.DataRowView)(e.Row.DataItem)).Row.ItemArray[1]);

                Label lblRowNo = (System.Web.UI.WebControls.Label)e.Row.FindControl("lblRowNo");
                lblRowNo.Text = rowNo.ToString() + ". ";
                rowNo = rowNo + 1;

                System.Web.UI.WebControls.Panel pnl = (System.Web.UI.WebControls.Panel)e.Row.FindControl("pnlAnswerOptions");

                Label lblQuestion = (System.Web.UI.WebControls.Label)e.Row.FindControl("lblQuestion");
                RadioButtonList rblans = (System.Web.UI.WebControls.RadioButtonList)e.Row.FindControl("rblans");
                lblQuestion.Text = (((System.Data.DataRowView)(e.Row.DataItem)).Row.ItemArray[2]).ToString();

              
                    //   RadioButtonList rbl = new RadioButtonList();
                    cmd = new SqlCommand("select Input_id,Input_Text from AddInputOptions where Question_id=" + questionId + " and Division_code ='"+div_code+"'" , con);
                    da = new SqlDataAdapter(cmd);
                    ds = new DataSet();
                    da.Fill(ds);
                    rblans.DataSource = ds;
                    rblans.DataValueField = ds.Tables[0].Columns[0].ColumnName;
                    rblans.DataTextField = ds.Tables[0].Columns[1].ColumnName;
                    rblans.DataBind();
                   // pnl.Controls.Add(rblans);
           

                //Label lbl = new Label();
                //lbl.Text = "txt";
                //TemplateField tm = new TemplateField();
            }
        }
        //e.Row.TemplateControl
        //  e.Row.TemplateControl.FindControl(
    }
    protected DataSet BindAns()
    {
        foreach (GridViewRow gridRow in grdPreviewQuestion.Rows)
        {
        System.Web.UI.WebControls.Panel pnl = (System.Web.UI.WebControls.Panel)gridRow.FindControl("pnlAnswerOptions");
        Label lblQus = (Label)gridRow.Cells[1].FindControl("lblqusid");
        RadioButtonList rblans = (RadioButtonList)pnl.FindControl("rblans");
        string Qus = lblQus.Text;
        cmd = new SqlCommand("select Input_id,Input_Text from AddInputOptions where Question_id='" + Qus + "'", con);
        da = new SqlDataAdapter(cmd);
        ds = new DataSet();
        da.Fill(ds);
        rblans.DataSource = ds;
        rblans.DataValueField = ds.Tables[0].Columns[0].ColumnName;
        rblans.DataTextField = ds.Tables[0].Columns[1].ColumnName;
        rblans.DataBind();
        pnl.Controls.Add(rblans);
        }
        return ds;
    }

    //protected void timer1_tick(object sender, EventArgs e)
    //{
    //    try
    //    {

    //        if (0 > DateTime.Compare(DateTime.Now, DateTime.Parse(Session["timeout"].ToString())))
    //        {
    //            lblTimer.Text = string.Format("Time Left: 00:{0}:{1}", ((Int32)DateTime.Parse(Session["timeout"].ToString()).Subtract(DateTime.Now).TotalMinutes).ToString(), ((Int32)DateTime.Parse(Session["timeout"].ToString()).Subtract(DateTime.Now).Seconds).ToString());
    //        }
    //        else
    //        {
    //            timer1.Enabled = true;
    //            if (Session["sf_type"].ToString() == "1")
    //            {
    //                Response.Redirect("~/Default_MR.aspx");
    //            }
    //            else if (Session["sf_type"].ToString() == "2")
    //            {
    //                Response.Redirect("~/Default_MGR.aspx");
    //            }

    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //    }
    //}


    protected void GridView1_PreRender(object sender, EventArgs e)
    {
        grdPreviewQuestion.UseAccessibleHeader = false;
        grdPreviewQuestion.HeaderRow.TableSection = TableRowSection.TableHeader;
    }


    //protected void grdPreviewQuestion_PageIndexChanging(object sender, GridViewPageEventArgs e)
    //{

    //    grdPreviewQuestion.PageIndex = e.NewPageIndex; 
    //    LoadSurveyPreview();

    //}

    //protected void imgprev_Click(object sender, ImageClickEventArgs e)
    //{
    //    int i = grdPreviewQuestion.PageCount;
    //    if (grdPreviewQuestion.PageIndex > 0)        
    //    {
    //        grdPreviewQuestion.PageIndex = grdPreviewQuestion.PageIndex - 1;

    //    } 

    //}
    //protected void imgNext_Click(object sender, ImageClickEventArgs e)
    //{

    //    int i = grdPreviewQuestion.PageIndex + 1;

    //    if (i <= grdPreviewQuestion.PageCount)
    //    {
    //        grdPreviewQuestion.PageIndex = i;
    //    }


    //}

    protected void btnFinalSubmit_Click(object sender, ImageClickEventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        end_date = DateTime.Now;
        ViewState["End"] = end_date.ToString();
        foreach (GridViewRow gridRow in grdPreviewQuestion.Rows)
        {
          //  System.Web.UI.WebControls.Panel pnl = (System.Web.UI.WebControls.Panel)gridRow.FindControl("pnlAnswerOptions");

            RadioButtonList rblans = (RadioButtonList)gridRow.Cells[1].FindControl("rblans");
            
            string answer = rblans.SelectedValue;
            Label lblQus = (Label)gridRow.Cells[1].FindControl("lblqusid");
            string Qus = lblQus.Text;
            AdminSetup adm = new AdminSetup();
            iRet = adm.AddQuiz(sf_code, sf_Name, div_code, Qus, answer, Request.QueryString["Survey_Id"], ViewState["Start"].ToString(), ViewState["End"].ToString());
            if (iRet > 0)
            {
             //   ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Quiz has been added Sucessfully');window.location='AnswerPage.aspx';</script>");
            }
        }
        AdminSetup adm1 = new AdminSetup();
        dsquiz = adm1.GetSecondQuiz(div_code, sf_code, Request.QueryString["Survey_Id"]);
        string status = dsquiz.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
        string survey = Request.QueryString["Survey_Id"].ToString();
        Response.Redirect("AnswerPage.aspx?Survey_Id=" + survey + " &status=" + status + "");
    }
}