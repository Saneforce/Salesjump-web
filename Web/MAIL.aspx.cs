using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using Bus_EReport;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Data.SqlTypes;
using System.Text;
using System.Net;
using System.Web.Services;
using Newtonsoft.Json;

public partial class MIS_Reports_MAIL : System.Web.UI.Page
{
    DataSet dsMail = null;
    DataSet dsFrom = null;
    string value = string.Empty;
    string fileName = string.Empty;
    string sf_code = string.Empty;
    string div_code = string.Empty;
    string sf_Type = string.Empty;
    string HO_ID = string.Empty;
    DataSet dsUserList = new DataSet();
    string sLevel = string.Empty;
    string temp_code = string.Empty;
    string mail_to_sf_code = string.Empty;
    string temp_Name = string.Empty;
    string mail_to_sf_Name = string.Empty;
    string mail_cc_sf_code = string.Empty;
    string strSF_Name = string.Empty;
    string mail_bcc_sf_code = string.Empty;
    SalesForce sf = new SalesForce();
    DataSet dsSalesForce = null;
    string strMail_CC = string.Empty;
    string sf_Name = string.Empty;
    string strMail_To = string.Empty;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int time;
    string strFileDateTime = string.Empty;
    string div_Name = string.Empty;
    string gh;
    string connection = Globals.ConnString;
    protected void Page_Load(object sender, EventArgs e)
    {
        //FillInbox();
        //bind();
        //CRUD_Service.ServiceCS service = new CRUD_Service.ServiceCS();
        //GridView1.DataSource = bind();
        //GridView1.DataBind();
        //GridView grd = GridView1; //grdTest is Id of gridview
        //BindGrid(grd);
        //BindDummyItem();
        //popupdiv.visible = false;
        GridView grd = GridView1; //grdTest is Id of gridview
        BindGrid(grd);
        //fillsalesforce();
        SqlConnection con = new SqlConnection(connection);
        con.Open();
        SqlCommand cmd = new SqlCommand("select count(*) from [Trans_Mail_Head]", con);
        SqlDataReader dr = cmd.ExecuteReader();
        if (dr.Read())
        {
            mailcount.Text = dr.GetValue(0).ToString();
            dr.Close();

        }
        con.Close();
    }
    //private void fillsalesforce()
    //{
    //    SalesForce sd = new SalesForce();
    //    dsSalesForce = sd.Getsalesforceli("4");
    //    if (dsSalesForce.Tables[0].Rows.Count > 0)
    //    {
    //        to.DataTextField = "Sf_Name";
    //        to.DataValueField = "Sf_Code";
    //        to.DataSource = dsSalesForce;
    //        to.DataBind();
    //        to.Items.Insert(0, new ListItem("--Select--", "0"));

    //    }
    //}
    public void BindDummyItem()
    {
        DataTable dtGetData = new DataTable();
        dtGetData.Columns.Add("Mail_Subject");
        dtGetData.Columns.Add("Mail_SF_From");
        dtGetData.Columns.Add("Mail_Content");
        dtGetData.Columns.Add("Mail_Sent_Time");
        dtGetData.Rows.Add();

        grdDemo.DataSource = dtGetData;
        grdDemo.DataBind();
    }
    [WebMethod]
    public void BindGrid(GridView grd)
    {
        using (SqlConnection con = new SqlConnection(Globals.ConnString))
        {
           
            con.Open();
            SqlCommand cmd = new SqlCommand("select * from Trans_Mail_Head", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            grd.DataSource = dt;
            grd.DataBind();
        }

    }
    protected void bind()
    {
        SqlConnection con = new SqlConnection(connection);
        con.Open();
        SqlCommand cmd = new SqlCommand("select * from Trans_Mail_Head", con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        GridView1.DataSource = dt;
        GridView1.DataBind();
        //GridView1.Columns[4].Visible = false;
        //GridView1.Columns[5].Visible = false;
        //GridView1.Columns[7].Visible = false;
    }
    private void FillInbox()
    {

        AdminSetup adm = new AdminSetup();
        dsMail = adm.getMailInbox("Admin", "1", "inbox", "", "11", "2016", "Mail_Sent_Time", "Desc", "");
        //if (dsMail.Tables[0].Rows.Count > 0)
        //{
        //gv1.Visible = true;
        gvInbox.Style.Add("margin-top", "20px");
        gvInbox.Style.Add("margin-left", "0px");
        gvInbox.DataSource = dsMail;
        gvInbox.DataBind();

        foreach (GridViewRow row in gvInbox.Rows)
        {
            if (dsMail.Tables[0].Rows.Count > 0)
            {
                Label lblSubject = (Label)row.FindControl("lblMail_subject");
                lblSubject.Text = dsMail.Tables[0].Rows[row.RowIndex]["Mail_subject"].ToString();
                lblSubject.Text = lblSubject.Text.Replace("asdf", "'");
            }
        }


        //}

        //else
        //{
        //gvInbox.Style.Add("margin-top", "250px");
        //gvInbox.Style.Add("margin-left", "500px");
        //gvInbox.DataSource = null;
        //gvInbox.DataBind();
        //}
    }
    protected void cbSelectAll_OnCheckedChanged(object sender, EventArgs e)
    {
        //PanelDisable();
        CheckBox ChkBoxHeader = (CheckBox)gvInbox.HeaderRow.FindControl("cbSelectAll");
        foreach (GridViewRow row in gvInbox.Rows)
        {
            CheckBox ChkBoxRows = (CheckBox)row.FindControl("chkId");
            if (ChkBoxHeader.Checked == true)
            {
                ChkBoxRows.Checked = true;
                //btnDelete.Enabled = true;
                //btnDelete.ForeColor = System.Drawing.Color.Black;
                //ddlMoved.Enabled = true;
            }
            else
            {
                ChkBoxRows.Checked = false;
                //btnDelete.Enabled = false;
                //ddlMoved.Enabled = false;
                //btnDelete.ForeColor = System.Drawing.Color.Gray;
            }
        }
        ScriptManager.RegisterStartupScript(this, typeof(Page), "OnClientClicking", "ShowInbox();", true);
    }
    protected void chkId_OnCheckedChanged(object sender, EventArgs e)
    {
        //pnlViewInbox.Style.Add("visibility", "hidden");
        //pnlViewMailInbox.Style.Add("visibility", "hidden");
        //pnlCompose.Style.Add("visibility", "hidden");
        //pnlpopup.Style.Add("visibility", "hidden");

        //CheckBox ChkBoxHeader = (CheckBox)gvInbox.HeaderRow.FindControl("cbSelectAll");
        foreach (GridViewRow row in gvInbox.Rows)
        {
            CheckBox ChkBoxRows = (CheckBox)row.FindControl("chkId");
            if (ChkBoxRows.Checked == true)
            {
                //btnDelete.Enabled = true;
                //btnDelete.ForeColor = System.Drawing.Color.Black;
                //ddlMoved.Enabled = true;
                //btnReply.Enabled = true;
                //btnReply.ForeColor = System.Drawing.Color.Black;
                //btnForward.Enabled = true;
                //btnForward.ForeColor = System.Drawing.Color.Black;
            }

        }
        ScriptManager.RegisterStartupScript(this, typeof(Page), "OnClientClicking", "ShowInbox();", true);

    }
    protected void gvInbox_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblslNo = (Label)e.Row.FindControl("lblslNo");

            e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor='#e3f2fd'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=''");
            //e.Row.Attributes.Add("style", "cursor:pointer;");
            e.Row.ToolTip = "Click Subject to view mail";
            // e.Row.Attributes.Add("onclick", "location='Mail_Head.aspx?inbox_id=" + lblslNo.Text + "'");            
        }

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            AdminSetup adm = new AdminSetup();
            Label lblslNo = (Label)e.Row.FindControl("lblslNo");

            DataSet dsMailAttach = adm.getMailAttach(lblslNo.Text);
            string str = dsMailAttach.Tables[0].Rows[0]["Mail_Attachement"].ToString();
            if (str == string.Empty)
            {
                e.Row.Cells[4].Visible = false;

            }
        }
    }
    protected void gvInbox_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "ViewMail")
        {
            int slno = Convert.ToInt16(e.CommandArgument);
            ViewState["inbox_id"] = slno.ToString();
            //OpenPopup(slno.ToString());
        }
    }
    protected void OnSelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void gvInbox_OnPageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvInbox.PageIndex = e.NewPageIndex;
        FillInbox();
        //pnlViewInbox.Style.Add("visibility", "hidden");
        //pnlViewMailInbox.Style.Add("visibility", "hidden");
        //pnlCompose.Style.Add("visibility", "hidden");
        //// New Changes done by saravanan Start 
        //pnlpopup.Style.Add("visibility", "hidden");
        // New Changes done by sararavanan End
        ScriptManager.RegisterStartupScript(this, typeof(Page), "OnClientClicking", "ShowInbox();", true);
    }
    //protected void search_TextChanged(object sender, EventArgs e)
    //{
    //    SqlConnection con = new SqlConnection(connection);
    //    con.Open();
    //    string tt = Searchtxt.Text;
    //    SqlCommand cmd = new SqlCommand("select * from Trans_Mail_Head where Mail_Subject like  '%" + tt + "%'", con);
    //    SqlDataAdapter da = new SqlDataAdapter(cmd);
    //    DataTable dt = new DataTable();
    //    da.Fill(dt);
    //    GridView1.DataSource = dt;
    //    GridView1.DataBind();

    //}
   
  
       public class DetailsClass //Class for binding data
       {
           public string Mail_Subject { get; set; }
           public string Mail_SF_From { get; set; }
           public string Mail_Content { get; set; }
           public string Mail_Sent_Time { get; set; }

       }

       [WebMethod]
       public static DetailsClass[] GetData(string selectValue) //GetData function
       {
           List<DetailsClass> Detail = new List<DetailsClass>();

           SqlConnection con = new SqlConnection(Globals.ConnString);
           con.Open();
           SqlCommand cmd = new SqlCommand("select Mail_Subject,Mail_SF_From,Mail_Content,Mail_Sent_Time from Trans_Mail_Head where Mail_Subject like  '%" + selectValue + "%'", con);
           SqlDataAdapter da = new SqlDataAdapter(cmd);
           

          
           DataTable dtGetData = new DataTable();

           da.Fill(dtGetData);

           foreach (DataRow dtRow in dtGetData.Rows)
           {
               DetailsClass DataObj = new DetailsClass();
               DataObj.Mail_Subject = dtRow["Mail_Subject"].ToString();
               DataObj.Mail_SF_From = dtRow["Mail_SF_From"].ToString();
               DataObj.Mail_Content = dtRow["Mail_Content"].ToString();
               DataObj.Mail_Sent_Time = dtRow["Mail_Sent_Time"].ToString();

               Detail.Add(DataObj);
           }

           return Detail.ToArray();
       }


    [WebMethod]
    public static string fillrou(string selectValue)
    {
        string div_code = string.Empty;
        string sf_type = string.Empty;
        string distcode = selectValue;
        //string grdid = grd;
        ////div_code = "1";
        //GridView grdd = Eval(Gridview1);


        DataTable dt = new DataTable();
       
        SqlConnection con = new SqlConnection(Globals.ConnString);
        con.Open();
        SqlCommand cmd = new SqlCommand("select Mail_Subject,Mail_SF_From,Mail_Content,Mail_Sent_Time from Trans_Mail_Head where Mail_Subject like  '%" + distcode + "%'", con);
          SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dtt = new DataTable();
        da.Fill(dtt);

        //grdd.DataSource = dtt;
        //grdd.DataBind();
        var thisPage = new MIS_Reports_MAIL();
        DataTable df = thisPage.GRIDBIND(dtt);


        return JsonConvert.SerializeObject(dtt);
    }
    public DataTable GRIDBIND(DataTable data)
    {
        GridView grd = GridView1; //grdTest is Id of gridview
      
        GridView1.DataSource = data;
        GridView1.DataBind();
        return data;
    }
    //protected void bindd(datatable dt)
    //{
    //    //SqlConnection con = new SqlConnection(connection);
    //    //con.Open();
    //    //SqlCommand cmd = new SqlCommand("select * from Trans_Mail_Head", con);
    //    //SqlDataAdapter da = new SqlDataAdapter(cmd);
    //    //DataTable dt = new DataTable();
    //    //da.Fill(dt);
    //    GridView1.DataSource = dt;
    //    GridView1.DataBind();
    //    //GridView1.Columns[4].Visible = false;
    //    //GridView1.Columns[5].Visible = false;
    //    //GridView1.Columns[7].Visible = false;
    //}
    [WebMethod]
    public  void BindGridData(string para1)
    {
        //DataTable dt = ClassName.GridData(para1, para2);
        //List<ClassName> list = new List<ClassName>();
        //foreach (DataRow dr in dt.Rows)
        //{
        //    ClassName pa = new ClassName();
        //    pa.para1 = Convert.ToString(dr["para1"]);
        //    pa.para2 = Convert.ToString(dr["para2"]);
        //    list.Add(pa);
        //}
        //return list;
            SqlConnection con = new SqlConnection(Globals.ConnString);
        con.Open();
     
        SqlCommand cmd = new SqlCommand("select * from Trans_Mail_Head where Mail_Subject like  '%" + para1 + "%'", con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        GridView1.DataSource = dt;
        GridView1.DataBind();
    }
    protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
    {
        //try
        //{
        //    string filename = Server.MapPath(name);
        //    Response.ContentType = ContentType;
        //    Response.AppendHeader("Content-Disposition", "attachment;filename=" + Path.GetFileName(filename));
        //    Response.WriteFile(filename);
        //    Response.End();
        //}
        //catch (Exception)
        //{
        //    ScriptManager.RegisterStartupScript(this, GetType(), "abc", "alert('No Files')", true);
        //}
    }
    protected void send(object sender, EventArgs e)
    {
        string hostName = Dns.GetHostName(); // Retrive the Name of HOST
        Console.WriteLine(hostName);
        // Get the IP
        string myIP = Dns.GetHostByName(hostName).AddressList[0].ToString();

       
        //string d = message1.Text;
        //int r = d.Length;
        //if (r > 20)
        //{
        //    StringBuilder er = new StringBuilder();
        //    er.Append(message1.Text);
        //    er.Remove(21, er.Length - 21);
        //    er.Append("......");
        //    value = er.ToString();
        //}
        //else
        //{
            value = message1.Text;
            //Response.Write(value);
        //}

        string dt = System.DateTime.Now.ToString();
        if (FileUpload1.HasFile)
        {
            FileUpload1.SaveAs(Server.MapPath("~/attach/") + Path.GetFileName(FileUpload1.PostedFile.FileName));
            string fileNamee = FileUpload1.PostedFile.FileName;
            fileName = "~/attach/" + fileNamee;
            Label1.Text = fileNamee;
        }
        else
        {
            fileName = "no";
        }
        SqlConnection con = new SqlConnection(connection);
        con.Open();
        SqlCommand cmd = new SqlCommand("insert into Trans_Mail_Head(Trans_Sl_No,[Mail_SF_From],[Mail_SF_To],Mail_Sent_Time,Mail_Attachement,Mail_Content,Mail_Subject,Division_Code,System_Ip)values('13','admin','" + to.Text + "','2016-11-23 16:28:48.847','" + fileName + "','" + message1.Text + "','" + subject.Text + "','" + div_code + "','" + myIP + "')", con);
        cmd.ExecuteNonQuery();
        //ScriptManager.RegisterStartupScript(this, GetType(), "abc", "alert('Message send sucessfully');window.location='compose.aspx';", true);
        con.Close();
    }

}