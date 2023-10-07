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

/// <summary>
/// Summary description for Quiz_QuestionWS
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class Quiz_QuestionWS : System.Web.Services.WebService
{

    public Quiz_QuestionWS()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    //------------- Quiz UserList Process---------------//

    [WebMethod(EnableSession = true)]
    [ScriptMethod]
    public List<UserDetail> BindUserList(string objData)
    {
        int ProcessId;
        string SfCode;
        string SfName;
        ArrayList arr = new ArrayList();
        DataSet dtUser = new DataSet();
        string[] arr2 = objData.Split('^');
        string div_code = HttpContext.Current.Session["div_code"].ToString();
        string surveyID = HttpContext.Current.Session["SurveyId"].ToString();
        string sf_code = HttpContext.Current.Session["sf_code"].ToString();

        List<UserDetail> objUserData = new List<UserDetail>();

        Product obj = new Product();

    //    dtUser = obj.Process_UserList(div_code, sf_code);
        if (arr2[0].ToString() == "0")
        {
            dtUser = obj.Process_UserList(div_code, sf_code);
        }
        else if (arr2[0].ToString() == "1")
        {
            dtUser = obj.Process_UserList_jod(div_code, sf_code, arr2[1].ToString(), arr2[2].ToString(), arr2[3].ToString());
        }
        else if (arr2[0].ToString() == "2")
        {
            dtUser = obj.Process_UserList_Desig(div_code, sf_code, arr2[4].ToString());
        }
        else if (arr2[0].ToString() == "3")
        {
            dtUser = obj.Process_UserList_Subdiv(div_code, sf_code, arr2[5].ToString());
        }

        if (dtUser.Tables[0].Rows.Count > 0)
        {
            string SurveyId = surveyID.ToString();

            DataSet dsProcess = new DataSet();
            dsProcess = obj.Process_SelectedUser_List(div_code, SurveyId);

            if (dsProcess.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < dsProcess.Tables[0].Rows.Count; i++)
                {
                    ProcessId = Convert.ToInt32(dsProcess.Tables[0].Rows[i]["ProcessId"].ToString());
                    SfCode = dsProcess.Tables[0].Rows[i]["Sf_Code"].ToString();
                    SfName = dsProcess.Tables[0].Rows[i]["Sf_Name"].ToString();

                    arr.Add(SfCode);
                }
            }

            foreach (DataRow dr in dtUser.Tables[0].Rows)
            {
                UserDetail objUser = new UserDetail();
                objUser.RowNo = Convert.ToInt32(dr["order_id"].ToString());


                objUser.SF_Code = dr["SF_Code"].ToString();
                for (int j = 0; j < arr.Count; j++)
                {
                    string sfCode = arr[j].ToString();
                    if (objUser.SF_Code == sfCode)
                    {
                        objUser.ChkBox = "True";
                        objUser.Sf_Code1 = objUser.SF_Code;
                    }

                }
                objUser.FieldForceName = dr["Sf_Name"].ToString();
                objUser.HQ = dr["sf_hq"].ToString();
                objUser.Designation = dr["Designation"].ToString();
                objUser.State = dr["StateName"].ToString();

                objUserData.Add(objUser);

            }
        }
        return objUserData;
    }

    //[WebMethod(EnableSession = true)]
    //[ScriptMethod]
    //public  void AddProcessDetails(List<string> objUserData)
    //{
    //    string U_Data;
    //    string surveyID = HttpContext.Current.Session["SurveyId"].ToString();
    //    string div_code = HttpContext.Current.Session["div_code"].ToString();



    //    for (int i = 0; i < objUserData.Count; i++)
    //    {
    //        U_Data = objUserData[i].ToString();

    //        string[] values = U_Data.Split(',');

    //        UserDetail objUser = new UserDetail();
    //        objUser.RowNo = Convert.ToInt32(values[0].ToString());
    //        objUser.FieldForceName = values[1].ToString();
    //        objUser.HQ = values[2].ToString();
    //        objUser.Designation = values[3].ToString();
    //        objUser.State = values[4].ToString();
    //        objUser.SF_Code = values[5].ToString();
    //        objUser.Time = values[7].ToString();
    //        objUser.ProcessDate = values[8].ToString();
    //        objUser.NoOfAttempts = values[9].ToString();
    //        objUser.Type = values[10].ToString();
    //        objUser.Month = values[11].ToString();
    //        objUser.Year = values[12].ToString();
    //        objUser.surveyID = surveyID.ToString();

    //        int lmonth = -1;
    //        int lyear = -1;

    //        //calculating opening balance from last month opening balance
    //        lmonth = GetLastMonth(Convert.ToString(objUser.Month));

    //        if (lmonth == 12)
    //            lyear = Convert.ToInt32(objUser.Year) - 1;
    //        else
    //            lyear = Convert.ToInt32(objUser.Year);


    //        Product objProduct = new Product();
    //        int _Res = objProduct.AddProcessing_Details(objUser.SF_Code, objUser.FieldForceName, objUser.HQ, objUser.Designation, objUser.State, objUser.Time, objUser.ProcessDate, objUser.Type, objUser.NoOfAttempts, objUser.RowNo, div_code, objUser.surveyID, objUser.Month, objUser.Year,lmonth.ToString(),lyear.ToString());

    //    }


    //}


    [WebMethod(EnableSession = true)]
    [ScriptMethod]
    public void AddProcessDetails(List<string> objUserData, string objMonth)
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString);

        string U_Data;
        string surveyID = HttpContext.Current.Session["SurveyId"].ToString();
        string div_code = HttpContext.Current.Session["div_code"].ToString();

        string[] ArrMon = objMonth.Split('^');

        string iMonth = ArrMon[0];
        string iYear = ArrMon[1];

        int lmonth = -1;
        int lyear = -1;

        //calculating opening balance from last month opening balance
        lmonth = GetLastMonth(Convert.ToString(iMonth));

        if (lmonth == 12)
            lyear = Convert.ToInt32(iYear) - 1;
        else
            lyear = Convert.ToInt32(iYear);

        StringBuilder Sb_Qus = new StringBuilder();
        Sb_Qus.Append("<root>");

        for (int i = 0; i < objUserData.Count; i++)
        {
            U_Data = objUserData[i].ToString();

            string[] values = U_Data.Split(',');

            UserDetail objUser = new UserDetail();
            objUser.RowNo = Convert.ToInt32(values[0].ToString());
            objUser.FieldForceName = values[1].ToString();
            objUser.HQ = values[2].ToString();
            objUser.Designation = values[3].ToString();
            objUser.State = values[4].ToString();
            objUser.SF_Code = values[5].ToString();
            objUser.Time = values[7].ToString();
            objUser.ProcessDate = values[8].ToString();
            objUser.NoOfAttempts = values[9].ToString();
            objUser.Type = values[10].ToString();
            objUser.Month = values[11].ToString();
            objUser.Year = values[12].ToString();
            objUser.FromDate = values[13].ToString();
            objUser.ToDate = values[14].ToString();
            objUser.surveyID = surveyID.ToString();

            Sb_Qus.Append("<QuestionData  Sf_Code='" + objUser.SF_Code + "' Sf_Name='" + objUser.FieldForceName + "' " +
            " Sf_HQ='" + objUser.HQ + "' Designation='" + objUser.Designation + "' State='" + objUser.State + "' " +
            " TimeLimit='" + objUser.Time + "' ProcessDate='" + objUser.ProcessDate + "'  Type='" + objUser.Type + "' " +
            " NoOfAttempts='" + objUser.NoOfAttempts + "'  Status='0' Sf_UserID='" + objUser.RowNo + "' Month='" + objUser.Month + "' Year='" + objUser.Year + "' From_Date='" + objUser.FromDate + "' To_Date='" + objUser.ToDate + "'/>");


            // Sb_Qus.Append("<QuestionData  Sf_Code='" + objUser.SF_Code + "' Sf_Name='" + objUser.FieldForceName + "' Sf_HQ='" + objUser.HQ + "' Designation='" + objUser.Designation + "' State='" + objUser.State + "' TimeLimit=" + objUser.Time + " ProcessDate='" + objUser.ProcessDate + "'  Type='" + objUser.Type + "' NoOfAttempts='" + objUser.NoOfAttempts + "'  Status='" + objUser.RowNo + "'  />");

            // Product objProduct = new Product();
            // int _Res = objProduct.AddProcessing_Details(objUser.SF_Code, objUser.FieldForceName, objUser.HQ, objUser.Designation, objUser.State, objUser.Time, objUser.ProcessDate, objUser.Type, objUser.NoOfAttempts, objUser.RowNo, div_code, objUser.surveyID, objUser.Month, objUser.Year, lmonth.ToString(), lyear.ToString());

        }

        Sb_Qus.Append("</root>");

        conn.Open();
        SqlCommand cmd = new SqlCommand("SP_Add_UserListProcessing_Quiz", conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@Div_Code", SqlDbType.VarChar);
        cmd.Parameters[0].Value = div_code;
        cmd.Parameters.Add("@SurveyId", SqlDbType.VarChar);
        cmd.Parameters[1].Value = surveyID;
        cmd.Parameters.Add("@XMLQuestion_Det", SqlDbType.VarChar);
        cmd.Parameters[2].Value = Sb_Qus.ToString();
        cmd.Parameters.Add("@Month", SqlDbType.VarChar);
        cmd.Parameters[3].Value = iMonth;
        cmd.Parameters.Add("@Year", SqlDbType.VarChar);
        cmd.Parameters[4].Value = iYear;
        cmd.Parameters.Add("@lmonth", SqlDbType.VarChar);
        cmd.Parameters[5].Value = lmonth;
        cmd.Parameters.Add("@lyear", SqlDbType.VarChar);
        cmd.Parameters[6].Value = lyear;
        cmd.Parameters.Add("@retValue", SqlDbType.Int);
        cmd.Parameters["@retValue"].Direction = ParameterDirection.Output;
        cmd.ExecuteNonQuery();
        int iReturn = Convert.ToInt32(cmd.Parameters["@retValue"].Value.ToString());
        conn.Close();
    }

    private int GetLastMonth(string sMonth)
    {
        int iMonth = 0;

        if (sMonth == "1")
            iMonth = 12;
        else if (sMonth == "2")
            iMonth = 1;
        else if (sMonth == "3")
            iMonth = 2;
        else if (sMonth == "4")
            iMonth = 3;
        else if (sMonth == "5")
            iMonth = 4;
        else if (sMonth == "6")
            iMonth = 5;
        else if (sMonth == "7")
            iMonth = 6;
        else if (sMonth == "8")
            iMonth = 7;
        else if (sMonth == "9")
            iMonth = 8;
        else if (sMonth == "10")
            iMonth = 9;
        else if (sMonth == "11")
            iMonth = 10;
        else if (sMonth == "12")
            iMonth = 11;

        return iMonth;

    }

    //---------------- Quiz_Question Frm---------------//

    [WebMethod(EnableSession = true)]
    [ScriptMethod]
    public void AddQuestionData(QuestionDetail QueData)
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

        SqlConnection con = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString);
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

    //------------------Preview & Edit Frm-----------//

    [WebMethod(EnableSession = true)]
    [ScriptMethod]
    public string UpdateQusData(List<string> ObjQusData)
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
            string[] Option = InpOpt.Split('#');
            CrtAns = objData.ConrrectAns;

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

    //------------------ Quiz Test ----------------//

    [WebMethod(EnableSession = true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string GetQuestion()
    {
        string div_code = HttpContext.Current.Session["div_code"].ToString();
        string sf_code = HttpContext.Current.Session["sf_code"].ToString();
        string sf_Name = HttpContext.Current.Session["sf_Name"].ToString();

        string SurveyId = HttpContext.Current.Session["SurveyID"].ToString();

        // SurveyId = "3";

        List<QusAnsOpt> QusAnsOpt = new List<QusAnsOpt>();
        SecSale SS = new SecSale();
        DataSet dsQus = SS.Get_Question_AnsOption(div_code, SurveyId.Trim());
        foreach (DataRow dr in dsQus.Tables[0].Rows)
        {
            QusAnsOpt objQus = new QusAnsOpt();
            objQus.R_Id = dr["R_Id"].ToString();
            objQus.Question_Id = dr["Question_Id"].ToString();
            objQus.question = dr["Question_Text"].ToString();
            objQus.Question_Type_Name = dr["Question_Type_Name"].ToString();
            objQus.Input_Text = dr["Input_Text"].ToString();
            objQus.Input_ID = dr["Input_Id"].ToString();
            objQus.Cor_Ans = dr["Correct_Ans"].ToString();

            string Input_Text = objQus.Input_Text.Replace("&lt;", "<")
                                                 .Replace("&amp;", "&")
                                                 .Replace("&gt;", ">")
                                                 .Replace("&quot;", "\"")
                                                 .Replace("&apos;", "'");

            string[] Data = Input_Text.Split('$');

            //string[] Data = objQus.Input_Text.Split('$');
            string[] InputArr = objQus.Input_ID.Split(',');

            objQus.Cor_Ans = objQus.Cor_Ans.Replace("&", "&amp;");

            int index = Array.FindIndex(Data, row => row.Contains(objQus.Cor_Ans));
            objQus.correctAnswer = Convert.ToString(index);
            objQus.choices = Data;
            objQus.ChoiceID = InputArr;
            objQus.CorrectId = dr["Correct_Id"].ToString();
            QusAnsOpt.Add(objQus);
        }

        return new JavaScriptSerializer().Serialize(QusAnsOpt); ;
    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod]
    public int Add_QuizResult(string Result)
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString);

        string div_code = HttpContext.Current.Session["div_code"].ToString();
        string sf_code = HttpContext.Current.Session["sf_code"].ToString();
        string sf_Name = HttpContext.Current.Session["sf_Name"].ToString();

        string SurveyId = HttpContext.Current.Session["SurveyID"].ToString();

        // SurveyId = "3";

        int iReturn = 0;
        string R_data = string.Empty;

        string[] Data = Result.Split('#');
        string[] AnsQusId = Data[0].Split(',');
        string StartTime = Data[1];
        string EndTime = Data[2];

        StringBuilder Sb_Qus = new StringBuilder();
        Sb_Qus.Append("<root>");

        for (int i = 0; i < AnsQusId.Length; i++)
        {
            string[] QueAns = AnsQusId[i].Split('^');
            string Qus_Id = QueAns[0];
            string Input_Id = QueAns[1];

            Sb_Qus.Append("<QuestionData  Sf_Code='" + sf_code + "' Sf_Name='" + sf_Name + "' Div_code='" + div_code + "' Question_Id='" + Qus_Id + "' Input_Id='" + Input_Id + "' Survey_Id='" + SurveyId + "' StartTime='" + StartTime + "'  EndTime='" + EndTime + "'  />");
        }

        Sb_Qus.Append("</root>");

        //SP_AddQuiz_Result

        conn.Open();
        SqlCommand cmd = new SqlCommand("SP_AddQuiz_Result_Test", conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@Div_Code", SqlDbType.VarChar);
        cmd.Parameters[0].Value = div_code;
        cmd.Parameters.Add("@SF_Code", SqlDbType.VarChar);
        cmd.Parameters[1].Value = sf_code;
        cmd.Parameters.Add("@XMLQuestion_Det", SqlDbType.VarChar);
        cmd.Parameters[2].Value = Sb_Qus.ToString();
        cmd.Parameters.Add("@SurvID", SqlDbType.VarChar);
        cmd.Parameters[3].Value = SurveyId;
        cmd.Parameters.Add("@retValue", SqlDbType.Int);
        cmd.Parameters["@retValue"].Direction = ParameterDirection.Output;
        cmd.ExecuteNonQuery();
        iReturn = Convert.ToInt32(cmd.Parameters["@retValue"].Value.ToString());
        conn.Close();

        //if (iReturn != 0)
        //{
        //    AdminSetup adm1 = new AdminSetup();
        //    DataSet dsquiz = adm1.GetSecondQuiz(div_code, sf_code,SurveyId);
        //    string status = dsquiz.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
        //   // string survey = "3";

        //    R_data = status + "^" + SurveyId;
        //}

        return iReturn;
    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod]
    public string Get_QuizTimeLimit()
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString);
        string div_code = HttpContext.Current.Session["div_code"].ToString();
        string sf_code = HttpContext.Current.Session["sf_code"].ToString();
        string sf_Name = HttpContext.Current.Session["sf_Name"].ToString();
        string SurveyId = HttpContext.Current.Session["SurveyID"].ToString();

        SecSale SS = new SecSale();
        DataSet dsQus = SS.Get_QuizTimeLimit(div_code, SurveyId.Trim(), sf_code);

        string Dataval = "";

        int TimeLimit = 0;
        string SuffType = "";

        if (dsQus.Tables[0].Rows.Count > 0)
        {
            TimeLimit = Convert.ToInt32(dsQus.Tables[0].Rows[0]["Seconds"].ToString());
            SuffType = dsQus.Tables[0].Rows[0]["SuffType"].ToString();

        }

        Dataval = TimeLimit + "^" + SuffType;

        return Dataval;
    }

}
public class UserDetail
{
    public string FieldForceName { get; set; }
    public string HQ { get; set; }
    public string Designation { get; set; }
    public string State { get; set; }
    public int RowNo { get; set; }
    public string Time { get; set; }
    public string NoOfAttempts { get; set; }
    public string ProcessDate { get; set; }
    public string Type { get; set; }
    public string surveyID { get; set; }
    public string SF_Code { get; set; }
    public string Month { get; set; }
    public string Year { get; set; }
    public string ChkBox { get; set; }
    public string Sf_Code1 { get; set; }
    public string FromDate { get; set; }
    public string ToDate { get; set; }
}

public class QuestionDetail
{
    public int QuestionTypeId { get; set; }
    public string QuestionText { get; set; }
    public string InputOption { get; set; }
    public string CorrectAnswer { get; set; }

}

public class QuestionData
{
    public string AnsOption { get; set; }
    public string Question { get; set; }
    public string ConrrectAns { get; set; }
    public int QueID { get; set; }
    public string CorrectAnswer { get; set; }
}

public class EditQuestion
{
    public string AnsOption { get; set; }
    public string Question { get; set; }
    public string ConrrectAns { get; set; }
    public int QueID { get; set; }
    public string CorrectAnswer { get; set; }
}

public class QusAnsOpt
{
    public string R_Id { get; set; }
    public string Question_Id { get; set; }
    public string question { get; set; }
    public string Question_Type_Name { get; set; }
    public string Input_Text { get; set; }
    public string Input_ID { get; set; }
    public string Cor_Ans { get; set; }
    public string correctAnswer { get; set; }
    public string[] choices { get; set; }
    public string[] ChoiceID { get; set; }
    public string CorrectId { get; set; }
}
