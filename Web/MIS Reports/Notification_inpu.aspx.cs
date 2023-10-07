using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
using System.Web.Services;
using Newtonsoft.Json;
using DBase_EReport;
using System.Net;
using System.IO;

public partial class MIS_Reports_Notification_inpu : System.Web.UI.Page
{
    string div_code = string.Empty;   
	string sf_type = string.Empty;
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
   
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    Notice Addcomment = new Notice();
 string sl_no = string.Empty;
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
        sl_no = Request.QueryString["Sl_No"];
        hdnslno.Value = sl_no;
    }

    protected void ClearControls()
    {
        comment.Text = "";
        commenttype.Items.Insert(0, new ListItem("--Select--", "0"));
        //commenttype.SelectedItem.ToString() = "";
        //Request.Form["tdate"] = "";

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        string date = Request.Form["tdate"];
        if (date == "" || comment.Text == "" || commenttype.SelectedItem.Value == "0")
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "SuccessMsg", "alert('Fill the Required Feilds!');", true);
        }
        else
        {
            if ((Convert.ToString(sl_no) == "")|| (Convert.ToString(sl_no) == null))
            {
                Addcomment.Notice_Comment_Add(div_code, comment.Text, commenttype.SelectedItem.Text, date);
                this.ClearControls();
                Page.ClientScript.RegisterStartupScript(this.GetType(), "SuccessMsg", "alert('Comment Saved Successfully!');", true);
            }
            else
            {
                Addcomment.Notice_Comment_update(div_code, comment.Text, commenttype.SelectedItem.Text, date, sl_no);
                this.ClearControls();
                Page.ClientScript.RegisterStartupScript(this.GetType(), "SuccessMsg", "alert('Updated Successfully!');", true);

            }
        }
    }
    [WebMethod]
    public static string sentnoti(string div_code, string comment, string type, string data,string name)
    {

        string msg = string.Empty;
        DataTable dt = JsonConvert.DeserializeObject<DataTable>(data);
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            string sf_code = dt.Rows[i]["Sf_Code"].ToString();
            var url = "http://www."+name+"/server/native_Db_V13.php?axn=web/notification_push&sfcode=" + sf_code + "&msg=" + comment + "&title="+ type + "&event_id="+ type + "";//Paste ur url here  

            WebRequest request = HttpWebRequest.Create(url);

            WebResponse response = request.GetResponse();

            StreamReader reader = new StreamReader(response.GetResponseStream());

            string responseText = reader.ReadToEnd();
        }
        return msg;
    }
    [WebMethod]
    public static  string savenotifi(string div_code, string comment,string type,string date,string sl_no)
    {

        string status = string.Empty;
        Notice sf = new Notice();
        string ds = string.Empty;
        if (Convert.ToString(sl_no) == "" || (Convert.ToString(sl_no) == null))
        {
            ds = sf.Notice_Comment_Add(div_code, comment, type, date);
            return status="Saved";
        }
        else
        {
            ds = sf.Notice_Comment_update(div_code, comment, type, date,sl_no);
            return status = "Updated";
        }
        

    }

    [WebMethod(EnableSession = true)]
    public static string fillData(string sl_no)
    {
        string Div_Code = HttpContext.Current.Session["div_code"].ToString();
        Notice sf = new Notice();
        DataSet ds = new DataSet();
        if(Convert.ToString(sl_no)!="")
            ds = sf.getNoticeboardedit(Div_Code.Trim(','), sl_no);   
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    [WebMethod]
    public static string userlistitm(string sf_type, string div_code)
    {
        string sfcode = string.Empty;
        DataSet dsAdmin = new DataSet();
        DB_EReporting db = new DB_EReporting();
        string  Sub_Div_Code = string.Empty;
        if (sf_type == "3")
        {
            sfcode = "admin";
        }
            string strQry = "exec Useritmlist '"+ sfcode + "','" + div_code + "','" + Sub_Div_Code + "'";
        try
        {
            dsAdmin = db.Exec_DataSet(strQry);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return JsonConvert.SerializeObject(dsAdmin.Tables[0]);
    }

}