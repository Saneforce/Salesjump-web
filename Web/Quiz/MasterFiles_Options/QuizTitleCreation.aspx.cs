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
using System.IO;

public partial class MasterFiles_Options_QuizTitleCreation : System.Web.UI.Page
{
    SqlConnection con;
    DataSet ds;
    SqlDataAdapter da;
    SqlCommand cmd;
    string div_code = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();

        Session["backurl"] = "Quiz_List.aspx";

        //menu1.Title = this.Page.Title;
        if (Request.QueryString["Survey_Id"] != null)
        {
            hidSurveyID.Value = Request.QueryString["Survey_Id"];
            int surveyID = Convert.ToInt32(Request.QueryString["Survey_Id"]);
            LoadSurveyEditData(surveyID);

        }

        if (!IsPostBack)
        {
            LoadCategory();

        }
    }
    private void LoadSurveyEditData(int surveryID)
    {

        SqlConnection con = new SqlConnection(Globals.ConnString);
        con.Open();
        SqlCommand cmdSurvey = new SqlCommand("select Survey_Id,Quiz_Title,CategoryID from QuizTitleCreation where Survey_Id=" + surveryID + " and Division_Code = '" + div_code + "' ", con);
        SqlDataReader dr = cmdSurvey.ExecuteReader();
        if (dr.Read())
        {
            txttitle.Text = dr["Survey_Title"].ToString();
            ddlCategory.SelectedValue = dr["CategoryID"].ToString();
            btnSurvey.Text = "Update";
        }
    }

    private void LoadCategory()
    {
        con = new SqlConnection(WebGlobals.ConnString);
        cmd = new SqlCommand("select Category_Id,Category_Name from [dbo].[QuizCategory_Creation] where Division_Code='" + div_code + "' and Category_Active=0", con);
        da = new SqlDataAdapter(cmd);
        ds = new DataSet();
        da.Fill(ds);
        ddlCategory.DataSource = ds;
        ddlCategory.DataValueField = ds.Tables[0].Columns["Category_Id"].ColumnName;
        ddlCategory.DataTextField = ds.Tables[0].Columns["Category_Name"].ColumnName;
        ddlCategory.DataBind();
        ListItem li = new ListItem();
        li.Text = "-- Select Category --";
        li.Value = "0";
        ddlCategory.Items.Insert(0, li);
    }
    protected void btnadd_Click(object sender, EventArgs e)
    {

        SqlConnection conn = new SqlConnection(Globals.ConnString);
      
        int lmonth = -1;
        int lyear = -1;
        //calculating opening balance from last month opening balance
        lmonth = GetLastMonth(Convert.ToString(ddlMonth.SelectedValue));

        if (lmonth == 12)
            lyear = Convert.ToInt32(hidYear.Value) - 1;
        else
            lyear = Convert.ToInt32(hidYear.Value);

        string filename = Path.GetFileName(fileUpload1.PostedFile.FileName);
        string Quiz_FilePath;

        if (filename == "")
        {
            Quiz_FilePath = "";
        }
        else
        {
            fileUpload1.SaveAs(Server.MapPath("Files/" + filename));

            Quiz_FilePath = "Files_1/" + "(" + txtEffFrom.Text + ")" + filename;
        }

        conn.Open();
        cmd = new SqlCommand("SP_AddQuizTitle_Proc", conn);
        DateTime dt1 = Convert.ToDateTime(txtEffFrom.Text.ToString());
        string dt2 = dt1.ToString("yyyy/MM/dd");
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@QuizTitle", txttitle.Text);
        cmd.Parameters.AddWithValue("@CategoryID", ddlCategory.SelectedValue);
        cmd.Parameters.AddWithValue("@DivCode", div_code);
        cmd.Parameters.AddWithValue("@SurveyId", hidSurveyID.Value);
        cmd.Parameters.AddWithValue("@Month", ddlMonth.SelectedValue);
        cmd.Parameters.AddWithValue("@Year", hidYear.Value);
        cmd.Parameters.AddWithValue("@lMonth", lmonth);
        cmd.Parameters.AddWithValue("@lYear", lyear);
        cmd.Parameters.AddWithValue("@FilePath", Quiz_FilePath);
        cmd.Parameters.AddWithValue("@Effective_Date", dt2);
        int iReturn = Convert.ToInt32(cmd.ExecuteNonQuery());

        if (iReturn == 2)
        {
            //   DisplayMessageAddRedirect("Survey has been Updated Successfully", "SurveyList.aspx");
            Response.Write("<script>alert('Title has been Updated Successfully') ; location.href='Quiz_List.aspx'</script>");
            Clear();
        }
        else if (iReturn != 0)
        {
            ClientScript.RegisterStartupScript(GetType(), "TestAlert", "alert('Title has been Inserted Successfully');", true);
            Clear();
        }

        conn.Close();
    }
    private void Clear()
    {
        txttitle.Text = "";
        ddlCategory.SelectedValue = "0";
    }

    private static int GetLastMonth(string sMonth)
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