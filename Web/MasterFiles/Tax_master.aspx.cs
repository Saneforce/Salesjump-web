using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bus_EReport;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Web.Services;
using System.Configuration;  


public partial class MasterFiles_Tax_Master : System.Web.UI.Page
{
    string div_code = string.Empty;
    DataSet dsSalesForce = null;
    DataSet dsUserList = null;
    DataSet dsDoc = null;
    DataSet dsDCR = null;
    int product_total = 0;
    int FWD_total = 0;
    DateTime dtCurrent;
    DataSet dsProduct = null;
    DataSet dsCatg = null;
    int MonColspan = 0;
    DataSet dsTP = null;
    string tot_dr = string.Empty;
    string sCurrentDate = string.Empty;
    string sf_code = string.Empty;
    DataSet dsSf = null;
    string sf_type = string.Empty;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    Notice Addcomment = new Notice();
    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();

    }

    protected void ClearControls()
    {
        //comment.Text = "";
        //commenttype.Items.Insert(0, new ListItem("--Select--", "0"));
        //commenttype.SelectedItem.ToString() = "";
        //Request.Form["tdate"] = "";

    }
    [WebMethod]
    public static string inserttaxdata(string Tax_Name, string Tax_Type, string Value)
    {
        string id = string.Empty;
        string msg = "";
        string Active_flag = "0";
        string date = System.DateTime.Now.ToString();
        DateTime created_date = Convert.ToDateTime(date);
string div_code;
      div_code = HttpContext.Current.Session["div_code"].ToString();
        if (Tax_Name != "" && Value != "")
        {
            SqlConnection con = new SqlConnection(Globals.ConnString);


            SqlCommand cmd = new SqlCommand("insert into Tax_Master (Tax_Name,Tax_Type,Value,Tax_Active_Flag,Created_Date,Division_Code) values(@Tax_Name, @Tax_Type,@Value,@Tax_Active_Flag,@Created_Date,@Division_Code)", con);
            cmd.Parameters.AddWithValue("@Tax_Name", Tax_Name);
            cmd.Parameters.AddWithValue("@Tax_Type", Tax_Type);
            cmd.Parameters.AddWithValue("@Value", Value);
            cmd.Parameters.AddWithValue("@Tax_Active_Flag", Active_flag);
            cmd.Parameters.AddWithValue("@Created_Date", created_date);
           cmd.Parameters.AddWithValue("@Division_Code", div_code);

            con.Open();
            int i = cmd.ExecuteNonQuery();
            if (i == 1)
            {
                msg = "true";
            }
            else
            {
                msg = "false";
            }


        }
        return msg;

    }

    //protected void Button1_Click(object sender, EventArgs e)
    //{
    //    string date = Request.Form["tdate"];
    //    if (date == "" || comment.Text == "" || commenttype.SelectedItem.ToString() == "")
    //    {
    //        Page.ClientScript.RegisterStartupScript(this.GetType(), "SuccessMsg", "alert('Fill the Required Feilds!');", true);
    //    }
    //    else
    //    {
    //        Addcomment.Notice_Comment_Add(div_code, comment.Text, commenttype.SelectedItem.Text, date);
    //        this.ClearControls();
    //        Page.ClientScript.RegisterStartupScript(this.GetType(), "SuccessMsg", "alert('Comment Saved Successfully!');", true);
    //    }
    //}
}