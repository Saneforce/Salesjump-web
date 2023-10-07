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
using System.Web.Services;
using System.Web.Script.Services;
using System.Collections.Generic;
using Bus_EReport;
using System.Text;

public partial class MasterFiles_Options_Quiz_Process : System.Web.UI.Page
{
    static string div_code = string.Empty;
    static int surveyID;
    static string sf_code = string.Empty;
    static string[] SfName_Arr;
    static string Month;
    static string Year;
    string[] statecd;
    string state_cd = string.Empty;
    string sState = string.Empty;
    DataSet dsDesig = new DataSet();
    protected void Page_Load(object sender, EventArgs e)
    {
        //menu1.Title = this.Page.Title;

        div_code = Session["div_code"].ToString();
        sf_code = Session["sf_code"].ToString();

        Session["backurl"] = "Quiz_List.aspx";

        // string Month = Session["Month"].ToString();
        //string Year = Session["Year"].ToString();

        //menu1.Title = this.Page.Title;
        if (Request.QueryString["Survey_Id"] != null)
        {
            BindDate();
            Get_State();
            FillDesig();
            FillSub_Division();
            // hidSurveyID.Value = Request.QueryString["Survey_Id"];
            surveyID = Convert.ToInt32(Request.QueryString["Survey_Id"]);
            Month = Request.QueryString["Month"];
            Year = Request.QueryString["Year"];
            Session["SurveyID"] = surveyID.ToString();
            Session["Month"] = Month.ToString();
            Session["Year"] = Year.ToString();
        }
    }
    private void FillSub_Division()
    {
        SalesForce sf = new SalesForce();
        DataSet dsDiv = new DataSet();
        dsDiv = sf.Getsubdivisionwise(div_code);
        if (dsDiv.Tables[0].Rows.Count > 0)
        {
            ddlsubdiv.DataTextField = "subdivision_name";
            ddlsubdiv.DataValueField = "subdivision_code";
            ddlsubdiv.DataSource = dsDiv;
            ddlsubdiv.DataBind();
        }
    }
    private void Get_State()
    {
        Division dv = new Division();
        DataSet dsDivision;
        dsDivision = dv.getStatePerDivision(div_code);
        if (dsDivision.Tables[0].Rows.Count > 0)
        {
            int i = 0;
            state_cd = string.Empty;
            sState = dsDivision.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
            statecd = sState.Split(',');
            foreach (string st_cd in statecd)
            {
                if (i == 0)
                {
                    state_cd = state_cd + st_cd;
                }
                else
                {
                    if (st_cd.Trim().Length > 0)
                    {
                        state_cd = state_cd + "," + st_cd;
                    }
                }
                i++;
            }

            State st = new State();
            DataSet dsState;
            // dsState = st.getStateChkBox(state_cd);

            dsState = st.getSt(state_cd);

            ddlst.DataTextField = "statename";
            ddlst.DataValueField = "state_code";
            ddlst.DataSource = dsState;
            ddlst.DataBind();
            ddlst.Items.Insert(0, new ListItem("ALL", "0"));

        }
    }
    private void FillDesig()
    {
        Designation des = new Designation();
        dsDesig = des.getDesignation_count(div_code);
        if (dsDesig.Tables[0].Rows.Count > 0)
        {
            ddlDesig.DataTextField = "Designation_Short_Name";
            ddlDesig.DataValueField = "Designation_Code";
            ddlDesig.DataSource = dsDesig;
            ddlDesig.DataBind();
        }
    }
   
    private void BindDate()
    {
        TourPlan tp = new TourPlan();
        DataSet dsTP = new DataSet();

        dsTP = tp.Get_TP_Edit_Year(div_code);
        if (dsTP.Tables[0].Rows.Count > 0)
        {
            for (int k = Convert.ToInt16(dsTP.Tables[0].Rows[0]["Year"]); k <= DateTime.Now.Year + 1; k++)
            {
                ddlTo.Items.Add(k.ToString());

            }

            ddlTo.Text = DateTime.Now.Year.ToString();

            //   ddlFrom.SelectedValue = DateTime.Now.Month.ToString();

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

    [WebMethod(EnableSession = true)]
    public static List<UserDetail> BindUserList(string objData)
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
    [WebMethod(EnableSession = true)]
    public static void AddProcessDetails(List<string> objUserData, string objMonth)
    {
        SqlConnection conn = new SqlConnection(Globals.ConnString);

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
    public static int GetLastMonth(string sMonth)
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
}

