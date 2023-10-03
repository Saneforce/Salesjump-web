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
using System.Data.SqlTypes;
using System.Text;
using System.Net;
using System.Web.Services;
using Newtonsoft.Json;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class attachementwithajax : System.Web.UI.Page
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
    //string fileNames = string.Empty;
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
    #region Private Member Variables
    private static string UPLOADFOLDER = "Uploads";
    
    #endregion

    #region Web Methods
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!this.IsPostBack)
        {
            //Reserve a spot in Session for the UploadDetail object
            this.Session["UploadDetail"] = new UploadDetail { IsReady = false };
            LoadUploadedFilesdelete(ref gvNewFiles);
             LoadUploadedFiles(ref gvNewFiles);

            getMenu();
            string com = "Select * from Mas_Mail_Folder_Name";
            SqlDataAdapter adpt = new SqlDataAdapter(com, connection);
            DataTable dtt = new DataTable();
            adpt.Fill(dtt);
            DropDownList1.DataSource = dtt;
            DropDownList1.DataTextField = "Move_MailFolder_Name";
            DropDownList1.DataValueField = "Move_MailFolder_Id";
            DropDownList1.DataBind();
            DropDownList1.Items.Insert(0, new ListItem("Move To", "0"));

            //GridView grd = GridView1; //grdTest is Id of gridview
            //BindGrid(grd);






            //bind();
            //CRUD_Service.ServiceCS service = new CRUD_Service.ServiceCS();
            //GridView1.DataSource = bind();

            //if (!IsPostBack)
            //{
            //    string f = menuBar.SelectedItem.Text;
            //    if (menuBar.SelectedItem.Value == "null")
            //    {
            //    }
            //    else
            //    {
            //        string gg = menuBar.SelectedItem.Value;
            //        Response.Write(gg);
            //    }
            //}
         





            //fillsalesforce();
            
        }
        sf_code = Session["sf_code"].ToString();
        sf_Type = Session["sf_type"].ToString();


        // Modified bySridevi  - 08Oct
        if (Session["div_code"] != null)
        {
            if (Session["div_code"].ToString() != "")
            {
                div_code = Session["div_code"].ToString();
                div_Name = Session["sf_name"].ToString();
            }
        }
        else if (sf_Type == "3")
        {
            div_code = Session["division_code"].ToString();
            div_Name = Session["div_Name"].ToString();
        }

        if (div_code.Contains(','))
        {
            div_code = div_code.Remove(div_code.Length - 1);
        }

        // Ends Here

        HO_ID = Session["HO_ID"].ToString();

        string productName = Request.QueryString["ProductName"];
        //lblSfName.Text = div_Name;
        // Changes done by Priya --Start
        if (sf_Type == "2" || sf_Type == "1")
        {
            //lblSfName.Text = div_Name + " - " + Session["Designation_Short_Name"] + " - " + Session["Sf_HQ"];
        }
        else
        {
            string division_Name = Session["div_Name"].ToString();
            //lblSfName.Text = division_Name;
        }
        SqlConnection con = new SqlConnection(connection);
        con.Open();
        SqlCommand cmd = new SqlCommand("select count(*) from  Trans_Mail_Detail where Mail_Read_Date is null and Open_Mail_Id= '" + sf_code + "' and Mail_Active_Flag=0 ", con);
        SqlDataReader dr = cmd.ExecuteReader();
        if (dr.Read())
        {
            mailcount.Text = dr.GetValue(0).ToString();
            dr.Close();

        }
        con.Close();
        if (!Page.IsPostBack)
        {
            //  Session["WinPostBack"] = "1";
            Session["Inbox"] = "Inbox";
            //ScriptManager.RegisterStartupScript(this, typeof(Page), "OnClientClicking", "ShowInbox();", true);

            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);
            //  AdminMailVisible();

            //FillInbox();

            //FillMoveFolder();
            //FillEMail();

            TourPlan tp = new TourPlan();
            DataSet dsTP = new DataSet();
            dsTP = tp.Get_HOID_TP_Edit_Year(sf_Type, div_code);
            if (div_code != "")
            {
                dsTP = tp.Get_HOID_TP_Edit_Year(sf_Type, div_code);

                if (dsTP.Tables[0].Rows.Count > 0)
                {
                    for (int k = Convert.ToInt16(dsTP.Tables[0].Rows[0]["Year"]); k <= DateTime.Now.Year + 1; k++)
                    {
                        ddlYr.Items.Add(k.ToString());
                        ddlMon.SelectedValue = DateTime.Now.Month.ToString();
                        ddlYr.SelectedValue = DateTime.Now.Year.ToString();
                    }
                }
            }
            else
            {
                string dt = DateTime.Now.ToShortDateString() + "-" + DateTime.Now.ToShortTimeString() + "-" + DateTime.Now.Minute;
                ddlMon.SelectedValue = DateTime.Now.Month.ToString();
                ddlYr.Items.Add(DateTime.Now.Year.ToString());
            }


        }
        GridView grdd = GridView1; //grdTest is Id of gridview
        BindGrid(grdd);

    }

    protected void OnMenuItemDataBound(object sender, MenuEventArgs e)
    {

        string hh = e.Item.Text;

       

    }
    private void getMenu()
    {
        using (SqlConnection con = new SqlConnection(Globals.ConnString))
        {

            con.Open();

            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            string sql = "Select * from  Mas_Mail_Folder_Name";
            SqlDataAdapter da = new SqlDataAdapter(sql, con);
            da.Fill(ds);
            dt = ds.Tables[0];
            DataRow[] drowpar = dt.Select("Folder_id=" + 0);

            foreach (DataRow dr in drowpar)
            {
                menuBar.Items.Add(new MenuItem(dr["Move_MailFolder_Name"].ToString(),
                        dr["Move_MailFolder_Id"].ToString(), ""));

            }

            foreach (DataRow dr in dt.Select("Folder_id >" + 0))
            {
                MenuItem mnu = new MenuItem(dr["Move_MailFolder_Name"].ToString(),
                               dr["Move_MailFolder_Id"].ToString(),
                               "");
                menuBar.FindItem(dr["Folder_id"].ToString()).ChildItems.Add(mnu);
            }
            con.Close();
        }
    }
    public void gtid()
    {

    }
    [WebMethod]
    public static bool filldelete(string selectValue)
    {
        string sf_code;string sf_Type; string div_code; string div_Name;
        AdminSetup adm = new AdminSetup();
        string conString = Globals.ConnString;
        sf_code = HttpContext.Current.Session["sf_code"].ToString();
        sf_Type = HttpContext.Current.Session["sf_type"].ToString();


        // Modified bySridevi  - 08Oct
        if (HttpContext.Current.Session["div_code"] != null)
        {
            if (HttpContext.Current.Session["div_code"].ToString() != "")
            {
                div_code = HttpContext.Current.Session["div_code"].ToString();
                div_Name = HttpContext.Current.Session["sf_name"].ToString();
            }
        }
        else if (sf_Type == "3")
        {
            div_code = HttpContext.Current.Session["division_code"].ToString();
            div_Name = HttpContext.Current.Session["div_Name"].ToString();
        }

        //if (div_code.Contains(','))
        //{
        //    div_code = div_code.Remove(div_code.Length - 1);
        //}

        // Ends Here
        using (SqlConnection con = new SqlConnection(conString))
        {
        //    int iReturn = adm.ChangeMailStatus(sf_code, Convert.ToInt32(lblslNo.Text), 1, "");
            using (SqlCommand cmd = new SqlCommand("update Trans_Mail_detail set Mail_Delete_date=GETDATE(), open_mail_id = '" + sf_code + "' , Mail_Active_Flag =1 where Trans_Sl_No ='"+selectValue+"' and open_mail_id = '" + sf_code + "'"))
            {
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@CustomerId", selectValue);
                con.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                con.Close();
                return rowsAffected > 0;
            }
        }



      

    }


    [WebMethod]
    public static bool updatereadstatus(string selectValue)
    {
        string sf_code; string sf_Type; string div_code; string div_Name; int rowsAffected=1;
        AdminSetup adm = new AdminSetup();
        string conString = Globals.ConnString;
        sf_code = HttpContext.Current.Session["sf_code"].ToString();
        sf_Type = HttpContext.Current.Session["sf_type"].ToString();
     
       
        attachementwithajax clear = new attachementwithajax();
        clear.GetFilesInDirectorydelete(HttpContext.Current.Server.MapPath(UPLOADFOLDER));
        //GetFilesInDirectorydelete(HttpContext.Current.Server.MapPath(UPLOADFOLDER));
        //// Modified bySridevi  - 08Oct
        if (HttpContext.Current.Session["div_code"] != null)
        {
            if (HttpContext.Current.Session["div_code"].ToString() != "")
            {
                div_code = HttpContext.Current.Session["div_code"].ToString();
                div_Name = HttpContext.Current.Session["sf_name"].ToString();
            }
        }
        else if (sf_Type == "3")
        {
            div_code = HttpContext.Current.Session["division_code"].ToString();
            div_Name = HttpContext.Current.Session["div_Name"].ToString();
        }

        //if (div_code.Contains(','))
        //{
        //    div_code = div_code.Remove(div_code.Length - 1);
        //}

        // Ends Here
        using (SqlConnection con = new SqlConnection(conString))
        {
            //strQry = "update Trans_Mail_detail set Mail_Read_Date=GETDATE(), open_mail_id = '" + sf_code + "' , Mail_Active_Flag = " + status + " where Trans_Sl_No = " + mail_id + " and open_mail_id = '" + sf_code + "'";
            //using (SqlCommand cmd = new SqlCommand("update Trans_Mail_detail set Mail_Read_Date=GETDATE(), open_mail_id = '" + sf_code + "' , Mail_Active_Flag = 10  where Trans_Sl_No = " + selectValue + " and open_mail_id = '" + sf_code + "'"))
            ////    int iReturn = adm.ChangeMailStatus(sf_code, Convert.ToInt32(lblslNo.Text), 1, "");
          
            //{
            //    cmd.Connection = con;
            //    cmd.Parameters.AddWithValue("@CustomerId", selectValue);
            //    con.Open();
               //int rowsAffected = cmd.ExecuteNonQuery();
          
            //    con.Close();
            //    return rowsAffected > 0;
            //}
        }
       
        return rowsAffected > 0;


        //using (SqlConnection con = new SqlConnection(Globals.ConnString))
        //{

        //    con.Open();
        //    SqlCommand cmd = new SqlCommand("Delete  from Trans_Mail_Head  where Trans_SL_No='3'", con);
        //    SqlDataAdapter da = new SqlDataAdapter(cmd);
        //    DataTable dt = new DataTable();
        //    da.Fill(dt);
        //    //GridView grdd= GridView1;
        //    //grdd.DataSource = dt;
        //    //grdd.DataBind();
        //}


    }

    public static void composeclk()
    {
        
        attachementwithajax clear = new attachementwithajax();
        clear.GetFilesInDirectorydelete(HttpContext.Current.Server.MapPath(UPLOADFOLDER));
          
           

    }








    [WebMethod]
    public static bool sendforwarddetails(string feildfrce,string bcc,string cc,string subj,string message,string attach)
    {
        string sf_code; string sf_Type; string div_code; string div_Name; 
        AdminSetup adm = new AdminSetup();
        string conString = Globals.ConnString;
        sf_code = HttpContext.Current.Session["sf_code"].ToString();
        sf_Type = HttpContext.Current.Session["sf_type"].ToString();

              
        
        if (HttpContext.Current.Session["div_code"] != null)
        {
            if (HttpContext.Current.Session["div_code"].ToString() != "")
            {
                div_code = HttpContext.Current.Session["div_code"].ToString();
                div_Name = HttpContext.Current.Session["sf_name"].ToString();
            }
        }
        else if (sf_Type == "3")
        {
            div_code = HttpContext.Current.Session["division_code"].ToString();
            div_Name = HttpContext.Current.Session["div_Name"].ToString();
        }
        string datetime = System.DateTime.Now.ToString();
        //if (div_code.Contains(','))
        //{
        //    div_code = div_code.Remove(div_code.Length - 1);
        //}

        // Ends Here
        using (SqlConnection con = new SqlConnection(conString))
        {
            //strQry = "update Trans_Mail_detail set Mail_Read_Date=GETDATE(), open_mail_id = '" + sf_code + "' , Mail_Active_Flag = " + status + " where Trans_Sl_No = " + mail_id + " and open_mail_id = '" + sf_code + "'";
            using (SqlCommand cmd = new SqlCommand("insert into  Trans_Mail_detail (Mail_SF_To,Mail_CC,Mail_BCC,Mail_Subject,Mail_Content,Mail_Attachement,Mail_Sent_Time,sender_Reciver_Flag)values('" + feildfrce + "' ,'" + cc + "' ,'" + bcc + "' ,'" + subj + "' ,'" + message + "' ,'" + attach + "' ,'" + datetime + "' ,'1'"))
            //    int iReturn = adm.ChangeMailStatus(sf_code, Convert.ToInt32(lblslNo.Text), 1, "");
            {
                cmd.Connection = con;
                //cmd.Parameters.AddWithValue("@CustomerId", selectValue);
                
                con.Open();
                int rowsAffected = cmd.ExecuteNonQuery();

                con.Close();
                return rowsAffected > 0;
            }
        }

       


       


    }


    [WebMethod]
    public static bool sendreplydetails(string feildfrce, string bcc, string cc, string subj, string message, string attach)
    {
        string sf_code; string sf_Type; string div_code; string div_Name;
        AdminSetup adm = new AdminSetup();
        string conString = Globals.ConnString;
        sf_code = HttpContext.Current.Session["sf_code"].ToString();
        sf_Type = HttpContext.Current.Session["sf_type"].ToString();



        if (HttpContext.Current.Session["div_code"] != null)
        {
            if (HttpContext.Current.Session["div_code"].ToString() != "")
            {
                div_code = HttpContext.Current.Session["div_code"].ToString();
                div_Name = HttpContext.Current.Session["sf_name"].ToString();
            }
        }
        else if (sf_Type == "3")
        {
            div_code = HttpContext.Current.Session["division_code"].ToString();
            div_Name = HttpContext.Current.Session["div_Name"].ToString();
        }
        string datetime = System.DateTime.Now.ToString();
       
        using (SqlConnection con = new SqlConnection(conString))
        {
            
            using (SqlCommand cmd = new SqlCommand("insert into  Trans_Mail_detail (Mail_SF_To,Mail_CC,Mail_BCC,Mail_Subject,Mail_Content,Mail_Attachement,Mail_Sent_Time,sender_Reciver_Flag)values('" + feildfrce + "' ,'" + cc + "' ,'" + bcc + "' ,'" + subj + "' ,'" + message + "' ,'" + attach + "' ,'" + datetime + "' ,'2'"))
            //    int iReturn = adm.ChangeMailStatus(sf_code, Convert.ToInt32(lblslNo.Text), 1, "");
            {
                cmd.Connection = con;
                //cmd.Parameters.AddWithValue("@CustomerId", selectValue);

                con.Open();
                int rowsAffected = cmd.ExecuteNonQuery();

                con.Close();
                return rowsAffected > 0;
            }
        }




     


    }
    [WebMethod]
    public static bool composedetails(string cfeildfrce, string cbcc, string ccc, string csubj, string cmessage, string cattach)
    {
        string sf_code; string sf_Type; string div_code; string div_Name;
        AdminSetup adm = new AdminSetup();
        string conString = Globals.ConnString;
        sf_code = HttpContext.Current.Session["sf_code"].ToString();
        sf_Type = HttpContext.Current.Session["sf_type"].ToString();



        if (HttpContext.Current.Session["div_code"] != null)
        {
            if (HttpContext.Current.Session["div_code"].ToString() != "")
            {
                div_code = HttpContext.Current.Session["div_code"].ToString();
                div_Name = HttpContext.Current.Session["sf_name"].ToString();
            }
        }
        else if (sf_Type == "3")
        {
            div_code = HttpContext.Current.Session["division_code"].ToString();
            div_Name = HttpContext.Current.Session["div_Name"].ToString();
        }
        string datetime = System.DateTime.Now.ToString();

        using (SqlConnection con = new SqlConnection(conString))
        {

            using (SqlCommand cmd = new SqlCommand("insert into  Trans_Mail_detail (Mail_SF_To,Mail_CC,Mail_BCC,Mail_Subject,Mail_Content,Mail_Attachement,Mail_Sent_Time,sender_Reciver_Flag)values('" + cfeildfrce + "' ,'" + ccc + "' ,'" + cbcc + "' ,'" + csubj + "' ,'" + cmessage + "' ,'" + cattach + "' ,'" + datetime + "' ,'1'"))
            //    int iReturn = adm.ChangeMailStatus(sf_code, Convert.ToInt32(lblslNo.Text), 1, "");
            {
                cmd.Connection = con;
                //cmd.Parameters.AddWithValue("@CustomerId", selectValue);

                con.Open();
                int rowsAffected = cmd.ExecuteNonQuery();

                con.Close();
                return rowsAffected > 0;
            }
        }







    }
    protected void Inbox_Click(object sender, EventArgs e)
    {

        GridView grdd = GridView1; //grdTest is Id of gridview
        BindGrid(grdd);


        
    }
     protected void btnSentItem_Click(object sender, EventArgs e)
    {
        

       

        AdminSetup adm = new AdminSetup();
        dsMail = adm.getMailInbox(sf_code, div_code, "sent", "", ddlMon.SelectedValue.ToString(), ddlYr.SelectedValue.ToString(), "Mail_Sent_Time", "Desc", "");
        if (dsMail.Tables[0].Rows.Count > 0)
        {
             
          
            GridView1.DataSource = dsMail;
            GridView1.DataBind();

            //foreach (GridViewRow row in GridView1.Rows)
            //{
            //    if (dsMail.Tables[0].Rows.Count > 0)
            //    {
            //        Label lblSubject = (Label)row.FindControl("lblMail_subject");
            //        lblSubject.Text = dsMail.Tables[0].Rows[row.RowIndex]["Mail_subject"].ToString();
            //        lblSubject.Text = lblSubject.Text.Replace("asdf", "'");
            //    }
            //}


        }
        else
        {
            //grdSent.Style.Add("margin-top", "250px");
            //grdSent.Style.Add("margin-left", "500px");
            GridView1.DataSource = null;
            GridView1.DataBind();
        }
    }
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

    protected void OnMenuItemClick(object sender, MenuEventArgs e)
    {

        string hh = e.Item.Text;
        using (SqlConnection con = new SqlConnection(Globals.ConnString))
        {

            con.Open();
            SqlCommand cmd = new SqlCommand("select * from Trans_Mail_Head h  INNER JOIN Trans_Mail_Detail d  on d.Trans_Sl_No=h.Trans_Sl_No where D.MAIL_MOVED_TO='SAVORIT' ", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }
    }
    [WebMethod]
    public void BindGrid(GridView grd)
    {
        string CurrentMonth = String.Format("{0:MMM}", DateTime.Now);
        string Currentyear = string.Format("{0:yyy}", DateTime.Now);
        string cmonthval = DateTime.Now.Month.ToString("0");
        AdminSetup adm = new AdminSetup();
        dsMail = adm.getMailInbox(sf_code, div_code, "inbox", "", cmonthval, Currentyear, "Mail_Sent_Time", "Desc", "");
        if (dsMail.Tables[0].Rows.Count > 0)
        {
            //gv1.Visible = true;
          
            grd.DataSource = dsMail;
            grd.DataBind();

            //foreach (GridViewRow row in grd.Rows)
            //{
            //    if (dsMail.Tables[0].Rows.Count > 0)
            //    {
            //        Label lblSubject = (Label)row.FindControl("lblMail_subject");
            //        lblSubject.Text = dsMail.Tables[0].Rows[row.RowIndex]["Mail_subject"].ToString();
            //        lblSubject.Text = lblSubject.Text.Replace("asdf", "'");
            //    }
            //}


        }
        else
        {
           
            grd.DataSource = null;
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




    protected void OnSelectedIndexChanged(object sender, EventArgs e)
    {

    }

    [WebMethod]
    public static bool Movetofolder(string trno,string movefolder)
    {

        string sf_code; string sf_Type; string div_code; string div_Name;
        AdminSetup adm = new AdminSetup();
        string conString = Globals.ConnString;
        sf_code = HttpContext.Current.Session["sf_code"].ToString();
        sf_Type = HttpContext.Current.Session["sf_type"].ToString();


        // Modified bySridevi  - 08Oct
        if (HttpContext.Current.Session["div_code"] != null)
        {
            if (HttpContext.Current.Session["div_code"].ToString() != "")
            {
                div_code = HttpContext.Current.Session["div_code"].ToString();
                div_Name = HttpContext.Current.Session["sf_name"].ToString();
            }
        }
        else if (sf_Type == "3")
        {
            div_code = HttpContext.Current.Session["division_code"].ToString();
            div_Name = HttpContext.Current.Session["div_Name"].ToString();
        }
        

        using (SqlConnection con = new SqlConnection(conString))
        {

            //using (SqlCommand cmd = new SqlCommand("UPDATE  Trans_MAIL_DETAIL  SET mail_moved_to='SAVORIT' where Trans_SL_No='" + hdn + "'"))
           using (SqlCommand cmd1 = new SqlCommand("update Trans_Mail_detail set open_mail_id = '" + sf_code + "' ,Mail_Moved_to='" + movefolder + "', Mail_Active_Flag =2 where Trans_Sl_No = " + trno+""))
            {
                cmd1.Connection = con;
                cmd1.Parameters.AddWithValue("@CustomerId", trno);
                con.Open();
                int rowsAffected = cmd1.ExecuteNonQuery();
                con.Close();
                return rowsAffected > 0;
            }
        }
    }


    public class DetailsClass //Class for binding data
    {
        public string Mail_Subject { get; set; }
        public string Mail_SF_From { get; set; }
        public string Mail_Content { get; set; }
        public string Mail_Sent_Time { get; set; }
        public string Trans_Sl_No { get; set; }
        public string Mail_Attachement { get; set; }

    }
    public class addressClass //Class for binding data
    {
        public string sfid { get; set; }
        public string sfname { get; set; }
        public string sfshrtname { get; set; }
        public string sfhq { get; set; }

    }

    [WebMethod]
    public static DetailsClass[] GetData(string selectValue) //GetData function
    {
        List<DetailsClass> Detail = new List<DetailsClass>();

        SqlConnection con = new SqlConnection(Globals.ConnString);
        con.Open();
        SqlCommand cmd = new SqlCommand("select Mail_Subject,Mail_SF_From,Mail_Content,Mail_Sent_Time,Trans_Sl_No,Mail_Attachement from Trans_Mail_Head where Mail_Subject like  '%" + selectValue + "%'", con);
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
            DataObj.Trans_Sl_No = dtRow["Trans_Sl_No"].ToString();
            DataObj.Mail_Attachement = dtRow["Mail_Attachement"].ToString();
            Detail.Add(DataObj);
        }

        return Detail.ToArray();
    }

    [WebMethod]
    public static addressClass[] Getaddress(string selectV) //GetData function
    {
        List<addressClass> Detail = new List<addressClass>();

        SqlConnection con = new SqlConnection(Globals.ConnString);
        con.Open();
        SqlCommand cmd = new SqlCommand("Execute [getaddressbook_Details] '" + selectV + "'", con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);



        DataTable dtGetData = new DataTable();

        da.Fill(dtGetData);

        foreach (DataRow dtRow in dtGetData.Rows)
        {
            addressClass DataObj = new addressClass();
            DataObj.sfid = dtRow["id"].ToString();
            DataObj.sfname = dtRow["name"].ToString();
            DataObj.sfshrtname = dtRow["sf_Designation_Short_Name"].ToString();
            DataObj.sfhq = dtRow["Sf_HQ"].ToString();

            Detail.Add(DataObj);
        }

        return Detail.ToArray();
    }

    public class attachClass //Class for binding data
    {

        public string Mail_Attachement { get; set; }

    }
    [WebMethod]
    public static attachClass[] Getattach(string Translno) //GetData function
    {
        List<attachClass> Detailk = new List<attachClass>();

        SqlConnection coon = new SqlConnection(Globals.ConnString);
        coon.Open();
        //SqlCommand cmdd = new SqlCommand("select  Mail_Attachement from trans_mail_head where Trans_Sl_No='" + Translno + "'", coon);
        SqlCommand cmdd = new SqlCommand("select * from Trans_Mail_Head where Mail_Subject like  '%" + Translno + "%'", coon);
        SqlDataAdapter daA = new SqlDataAdapter(cmdd);



        DataTable dtGetDataA = new DataTable();

        daA.Fill(dtGetDataA);

        foreach (DataRow dtRowW in dtGetDataA.Rows)
        {
            attachClass DataObjj = new attachClass();
            DataObjj.Mail_Attachement = dtRowW["Mail_Attachement"].ToString();

            Detailk.Add(DataObjj);
        }

        return Detailk.ToArray();
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
        var thisPage = new attachementwithajax();
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

    [WebMethod]
    public void BindGridData(string para1)
    {

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
        try
        {
            //Page.

            ClientScript.RegisterStartupScript(this.GetType(), null, "downgg();", true);
            //Request.Form["HiddenField1"].ToString();
            string fname = HiddenField1.Value;
            //string codeBehindValue = hdnResultValue.Value;
            //Response.Write(fname);
            string filename = Server.MapPath(fname);
            Response.ContentType = ContentType;
            Response.AppendHeader("Content-Disposition", "attachment;filename=" + Path.GetFileName(filename));
            Response.WriteFile(filename);
            Response.End();
        }
        catch (Exception)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "abc", "alert('No Files')", true);
        }
    }

    [WebMethod]
    public static void download(string selectValue)
    {

        string fname = selectValue;
        string filename = System.Web.HttpContext.Current.Server.MapPath(fname);
        HttpContext.Current.Response.ContentType = "application/octet-stream";
        HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment;filename=" + Path.GetFileName(filename));
        HttpContext.Current.Response.WriteFile(filename);
        HttpContext.Current.Response.End();
        //return fname;

    }
    protected void send(object sender, EventArgs e)
    {
        string hostName = Dns.GetHostName(); // Retrive the Name of HOST
        Console.WriteLine(hostName);
        // Get the IP
        string myIP = Dns.GetHostByName(hostName).AddressList[0].ToString();

        value = message1.Text;
      

       
       
    }
    [System.Web.Services.WebMethod]
    [System.Web.Script.Services.ScriptMethod]
    public static object GetUploadStatus()
    {
        string fileNames = string.Empty;
        //    string fileNames=string.Empty;
        //Get the length of the file on disk and divide that by the length of the stream
        UploadDetail info = (UploadDetail)HttpContext.Current.Session["UploadDetail"];
        //string h = info.FileName;
        //HttpContext.Current.Response.Write(h);
        fileNames += info.FileName + ",";
        //foreach (GridViewRow row in this.GridView1.Rows)
        //{
        //   
        //}



        //string constr = ConfigurationManager.ConnectionStrings["conString"].ConnectionString;
        //string sqlStatment = "UPDATE FilesUploaded SET FileName  = @FileName WHERE Id =@Id";
        //using (SqlConnection con = new SqlConnection(constr))
        //{
        //    using (SqlCommand cmd = new SqlCommand(sqlStatment, con))
        //    {
        //        con.Open();
        //        cmd.Parameters.AddWithValue("@FileName", files);
        //        cmd.Parameters.AddWithValue("@Id", Convert.ToInt32(this.txtId.Text.Trim()));
        //        cmd.ExecuteNonQuery();
        //        con.Close();
        //    }
        //}
        if (info != null && info.IsReady)
        {


            int soFar = info.UploadedLength;
            int total = info.ContentLength;
            int percentComplete = (int)Math.Ceiling((double)soFar / (double)total * 100);
            string message = "Uploading...";
            string fileName = string.Format("{0}", info.FileName);
            using (SqlConnection con = new SqlConnection(Globals.ConnString))
            {

                con.Open();
                SqlCommand cmd = new SqlCommand("update Trans_Mail_Head set Mail_Attachement= '" + fileName + "' where Trans_Sl_No='15' ", con);
                cmd.ExecuteNonQuery();
                con.Close();
            }
            string downloadBytes = string.Format("{0} of {1} Bytes", soFar, total);
            return new
            {
                percentComplete = percentComplete,
                message = message,
                fileName = fileName,
                downloadBytes = downloadBytes
            };
        }
        //Not ready yet
        return null;
    }
    #endregion


    #region Web Callbacks
    protected void gvNewFiles_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "eventMouseOver(this)");
            e.Row.Attributes.Add("onmouseout", "eventMouseOut(this)");
        }
    }
    protected void gvNewFiles_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "deleteFile":
                DeleteFile(e.CommandArgument.ToString());
                LoadUploadedFiles(ref gvNewFiles);
                break;
            case "downloadFile":
                string strFolder = "Uploads";
                string filePath = Path.Combine(strFolder, e.CommandArgument.ToString());
                DownloadFile(filePath);
                break;
        }
    }

     [System.Web.Services.WebMethod]
    [System.Web.Script.Services.ScriptMethod]
    public static object GetUploadStatus1()
    {
        string fileNames = string.Empty;
        //    string fileNames=string.Empty;
        //Get the length of the file on disk and divide that by the length of the stream
        UploadDetail info = (UploadDetail)HttpContext.Current.Session["UploadDetail"];
        //string h = info.FileName;
        //HttpContext.Current.Response.Write(h);
        //fileNames += info.FileName + ",";
        //foreach (GridViewRow row in this.GridView1.Rows)
        //{
        //   
        //}



        //string constr = ConfigurationManager.ConnectionStrings["conString"].ConnectionString;
        //string sqlStatment = "UPDATE FilesUploaded SET FileName  = @FileName WHERE Id =@Id";
        //using (SqlConnection con = new SqlConnection(constr))
        //{
        //    using (SqlCommand cmd = new SqlCommand(sqlStatment, con))
        //    {
        //        con.Open();
        //        cmd.Parameters.AddWithValue("@FileName", files);
        //        cmd.Parameters.AddWithValue("@Id", Convert.ToInt32(this.txtId.Text.Trim()));
        //        cmd.ExecuteNonQuery();
        //        con.Close();
        //    }
        //}
        if (info != null && info.IsReady)
        {


            int soFar = info.UploadedLength;
            int total = info.ContentLength;
            int percentComplete = (int)Math.Ceiling((double)soFar / (double)total * 100);
            string message = "Uploading...";
            string fileName = string.Format("{0}", info.FileName);
            string downloadBytes = string.Format("{0} of {1} Bytes", soFar, total);
            return new
            {
                percentComplete = percentComplete,
                message = message,
                fileName = fileName,
                downloadBytes = downloadBytes
            };
        }
        //Not ready yet
        return null;
    }
    #endregion

    

    
    #region Web Callbacks
    protected void GridViewfrd_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "eventMouseOver(this)");
            e.Row.Attributes.Add("onmouseout", "eventMouseOut(this)");
        }
    }
   


    protected void hdRefereshGrid_ValueChanged(object sender, EventArgs e)
    {
        LoadUploadedFiles(ref gvNewFiles);
        //System.IO.DirectoryInfo di = new DirectoryInfo("Uploads");

        //foreach (FileInfo file in di.GetFiles())
        //{
        //    file.Delete();
        //}
        

    }
  
   
   

    #region Support Methods
    public void LoadUploadedFiles(ref GridView gv)
    {



        DataTable dtFiles = GetFilesInDirectory(HttpContext.Current.Server.MapPath(UPLOADFOLDER));
        

        gv.DataSource = dtFiles;
        gv.DataBind();
        if (dtFiles != null && dtFiles.Rows.Count > 0)
        {
            double totalSize = Convert.ToDouble(dtFiles.Compute("SUM(Size)", ""));
            if (totalSize > 0) lblTotalSize.Text = CalculateFileSize(totalSize);
        }
        //System.IO.DirectoryInfo di = GetFilesInDirectory(HttpContext.Current.Server.MapPath(UPLOADFOLDER));
        //foreach (DirectoryInfo dir in di.GetDirectories())
        //{
        //    dir.Delete(true);
        //}
    }
    #endregion
    #region Support Methods
    public void LoadUploadedFilesdelete(ref GridView gv)
    {



        DataTable dtFiles = GetFilesInDirectorydelete(HttpContext.Current.Server.MapPath(UPLOADFOLDER));


        gv.DataSource = dtFiles;
        gv.DataBind();
        if (dtFiles != null && dtFiles.Rows.Count > 0)
        {
            double totalSize = Convert.ToDouble(dtFiles.Compute("SUM(Size)", ""));
            if (totalSize > 0) lblTotalSize.Text = CalculateFileSize(totalSize);
        }
        //System.IO.DirectoryInfo di = GetFilesInDirectory(HttpContext.Current.Server.MapPath(UPLOADFOLDER));
        //foreach (DirectoryInfo dir in di.GetDirectories())
        //{
        //    dir.Delete(true);
        //}
    }
    #endregion
    public DataTable GetFilesInDirectory(string sourcePath)
    {

        System.Data.DataTable dtFiles = new System.Data.DataTable();
        if ((Directory.Exists(sourcePath)))
        {

            dtFiles.Columns.Add(new System.Data.DataColumn("Name"));
            dtFiles.Columns.Add(new System.Data.DataColumn("Size"));
            dtFiles.Columns["Size"].DataType = typeof(double);
            dtFiles.Columns.Add(new System.Data.DataColumn("ConvertedSize"));
            DirectoryInfo dir = new DirectoryInfo(sourcePath);
          
            foreach (FileInfo files in dir.GetFiles("*.*"))
            {
                //files.Delete();

                System.Data.DataRow drFile = dtFiles.NewRow();
                drFile["Name"] = files.Name;
                drFile["Size"] = files.Length;
                drFile["ConvertedSize"] = CalculateFileSize(files.Length);
                dtFiles.Rows.Add(drFile);
            }
        }
        return dtFiles;
    }
    public DataTable GetFilesInDirectorydelete(string sourcePath)
    {

        System.Data.DataTable dtFiles = new System.Data.DataTable();
        if ((Directory.Exists(sourcePath)))
        {

            dtFiles.Columns.Add(new System.Data.DataColumn("Name"));
            dtFiles.Columns.Add(new System.Data.DataColumn("Size"));
            dtFiles.Columns["Size"].DataType = typeof(double);
            dtFiles.Columns.Add(new System.Data.DataColumn("ConvertedSize"));
            DirectoryInfo dir = new DirectoryInfo(sourcePath);

            foreach (FileInfo files in dir.GetFiles("*.*"))
            {
                files.Delete();

                //System.Data.DataRow drFile = dtFiles.NewRow();
                //drFile["Name"] = files.Name;
                //drFile["Size"] = files.Length;
                //drFile["ConvertedSize"] = CalculateFileSize(files.Length);
                //dtFiles.Rows.Add(drFile);
            }
        }
        return dtFiles;
    }



  
  
   
    
  
 
    protected void btn_Go(object sender, EventArgs e)
    {

        //lblAttachment.Text = FileUpload1.FileName;
        //if (lblAttachment.Text != "")
        //{
        //    strFileDateTime = DateTime.Now.ToString("dd_MM_yyyy_HH_mm_ss");
        //    Session["strFileDateTime"] = strFileDateTime;
        //    FileUpload1.PostedFile.SaveAs(Server.MapPath("~/MasterFiles/Mails/Attachment/" + strFileDateTime + FileUpload1.FileName));
        //}

        //PnlAttachment.Style.Add("visibility", "hidden");
        //pnlCompose.Style.Add("visibility", "visible");
        //ViewState["PnlAttachment"] = "";
        //ViewState["from"] = "";
    }

   
   
   
    
  
   

    public void DownloadFile(string filePath)
    {

        if (File.Exists(Server.MapPath(filePath)))
        {
            string strFileName = Path.GetFileName(filePath).Replace(" ", "%20");
            Response.ContentType = "application/octet-stream";
            Response.AddHeader("Content-Disposition", "attachment; filename=" + strFileName);
            Response.Clear();
            Response.WriteFile(Server.MapPath(filePath));
            Response.End();
        }
    }
 
    
    public  string DeleteFile(string FileName)
    {
        string strMessage = "";
        try

        {
            
            string strPath = Path.Combine(UPLOADFOLDER, FileName);
            if (File.Exists(Server.MapPath(strPath)) == true)
            {
                File.Delete(Server.MapPath(strPath));
                strMessage = "File Deleted";
            }
            else
                strMessage = "File Not Found";
        }
        catch (Exception ex)
        {
            strMessage = ex.Message;
        }
        return strMessage;
    }
    
    public string CalculateFileSize(double FileInBytes)
    {
        string strSize = "00";
        if (FileInBytes < 1024)

            strSize = FileInBytes + " B";//Byte
        else if (FileInBytes > 1024 & FileInBytes < 1048576)
            strSize = Math.Round((FileInBytes / 1024), 2) + " KB";//Kilobyte
        else if (FileInBytes > 1048576 & FileInBytes < 107341824)
            strSize = Math.Round((FileInBytes / 1024) / 1024, 2) + " MB";//Megabyte
        else if (FileInBytes > 107341824 & FileInBytes < 1099511627776)
            strSize = Math.Round(((FileInBytes / 1024) / 1024) / 1024, 2) + " GB";//Gigabyte
        else
            strSize = Math.Round((((FileInBytes / 1024) / 1024) / 1024) / 1024, 2) + " TB";//Terabyte
        return strSize;
    }
    #endregion

}