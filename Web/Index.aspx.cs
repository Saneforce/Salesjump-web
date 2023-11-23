using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Web.Services;
using Bus_EReport;
using System.Data;
using System.Data.SqlClient;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using DBase_EReport;
public partial class Index : System.Web.UI.Page
{
    DataSet dsLogin = null;
    DataSet dsSalesForce = null;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    DataSet dsAdmin = null;
    DataSet dsImage = new DataSet();
    DataSet dsBirth = new DataSet();
    DataSet dsImage_FF = new DataSet();
    DataSet dsAdm = null;
    DataSet dsAdmNB = null;
    DataSet dsLogin1 = null;
    string shrtname = string.Empty;
    static string div_code = string.Empty;
    string sf_code = string.Empty;
    int time;
    string fileName = string.Empty;
    string assignedIDs = string.Empty;
    string shortna = string.Empty;
    public string sConnectionString;
    public string sConnectionString1;
    public string sUrls = "";
    public string sUSR = "";
    public string AllowTyp = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        sConnectionString = getDBInfo();

        Global.ConnString = sConnectionString;
        Globals.ConnString = sConnectionString;

        Globals.MasterConnString = ConfigurationManager.ConnectionStrings["MasterDB"].ConnectionString;

        if (!Page.IsPostBack)
        {
            ServerStartTime = DateTime.Now;
            base.OnPreRender(e);
            Session["ID"] = null;
            Session.Abandon();

        }
        shrtname = shortna;

    }
    public string getDBInfo()
    {
        string sConStr = ConfigurationManager.ConnectionStrings["Server1"].ConnectionString;
        sUSR = Request.Url.Host.ToLower().Replace("www.", "").Replace(".sanfmcg.com", "").Replace(".salesjump.in", "").ToLower();
        string DBName = "";
        if (sUSR == "localhost")
        { DBName = "FMCG_RAD"; }
        else
        {
            if (Request.Url.Host.ToLower().IndexOf("salesjump.in") > -1 && sUSR == "fmcg")
            {
                DBName = "FMCG_TRIAL";
            }
            else if (sUSR == "fmcg")
            {
                DBName = "FMCG_Live";
            }
            else if (sUSR == "arasan")
            {
                DBName = "FMCG_ArasanSanitry";
            }
            else if (sUSR == "tiesar")
            {
                DBName = "FMCG_TSR";
            }
            else if (sUSR == "pgdb")
            {
                DBName = "FMCG_BGDB";
            }
            else if (sUSR == "pgkala")
            {
                DBName = "FMCG_Kala";
            }
            else if (sUSR == "allen")
            {
                DBName = "FMCG_AllenLab";
            }
            else if (sUSR == "afripipe")
            {
                DBName = "FMCG_Afripipes";
            }
            else
            {
                DBName = "FMCG_" + sUSR.ToUpper();
            }
        }
        //Session["DBName"] = DBName;
        sConStr = sConStr.Replace("[DATABASE]", DBName);
        return sConStr;
    }
    protected void Page_PreRender(object sender, EventArgs e)
    {

        ServerEndTime = DateTime.Now;
        TrackPageTime();//It will give you page load time  
    }
    public void TrackPageTime()
    {
        TimeSpan serverTimeDiff = ServerEndTime.Subtract(ServerStartTime);
        time = serverTimeDiff.Minutes;

    }

    private void BindImage1()
    {
        div_code = Session["div_code"].ToString();

        if (div_code.Contains(","))
        {
            div_code = div_code.Remove(div_code.Length - 1);
        }

        try
        {
            SqlConnection con = new SqlConnection(sConnectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("select FilePath,subject from Mas_HomePage_Image where Division_Code = '" + div_code + "'", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            da.Fill(dsImage);
            con.Close();
            da.Dispose();
            con.Dispose();

            /*string strQry = " SELECT FilePath,subject from Mas_HomePage_Image where Division_Code = '" + div_code + "'";
            using (var con = new SqlConnection(sConnectionString))
            {
                using (var cmd = con.CreateCommand())
                {
                    cmd.CommandText = strQry;
                    cmd.CommandType = CommandType.Text;
                    //cmd.Parameters.AddWithValue("@div_code", Convert.ToString(div_code));
                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = cmd;
                    con.Open();
                    da.Fill(dsAdmin);
                    con.Close();
                    da.Dispose();
                    con.Dispose();
                }
            }*/
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }
    private void BindImage_FieldForce()
    {

        sf_code = Session["sf_code"].ToString();
        div_code = Session["div_code"].ToString();

        if (div_code.Contains(','))
        {
            div_code = div_code.Remove(div_code.Length - 1);
        }

        try
        {
            SqlConnection con = new SqlConnection(sConnectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("select FilePath from Mas_HomeImage_FieldForce where sf_code='" + sf_code + "' and Division_Code = '" + div_code + "'", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dsImage_FF);
            con.Close();
            da.Dispose();
            con.Dispose();

           /* string strQry = " SELECT FilePath from Mas_HomeImage_FieldForce WHERE sf_code = @sf_code AND  Division_Code = @div_code";
            using (var con = new SqlConnection(sConnectionString))
            {
                using (var cmd = con.CreateCommand())
                {
                    cmd.CommandText = strQry;
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@sf_code", Convert.ToString(sf_code));
                    cmd.Parameters.AddWithValue("@div_code", Convert.ToString(div_code));
                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = cmd;
                    con.Open();
                    da.Fill(dsAdmin);
                    con.Close();
                    da.Dispose();
                    con.Dispose();
                }
            }*/
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        txtUserName.Text = "";
        txtPassWord.Text = "";
    }
    public class Slides
    {
        public string SlideName { get; set; }

    }
    [WebMethod]
    public static List<Slides> getSlides(string shDiv)
    {
        DataTable dt = new DataTable();
        List<Slides> obj = new List<Slides>();
        /*string sUSR=HttpContext.Current.Request.Url.Host.ToLower().Replace("www.","").Replace(".sanfmcg.com","");
		string sWBConnectionString=ConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString;
		if(sUSR=="shivatex") sWBConnectionString=ConfigurationManager.ConnectionStrings["Ereportcon_shivatex"].ConnectionString;
        SqlConnection con = new SqlConnection(sWBConnectionString);
        con.Open();
        SqlCommand com = new SqlCommand("select SlideFiles from SlidesImgList where CompId='" + shDiv + "'", con);
        SqlDataAdapter da = new SqlDataAdapter(com);
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                obj.Add(new Slides

                {
                    SlideName = dt.Rows[i]["SlideFiles"].ToString()

                });

            }
        }*/
        return obj;
    }
    public DataSet DSM_Login(string usr_id, string pwd, string shrtname)
    {
        DB_EReporting db_ER = new DB_EReporting();

        DataSet dsDivision = new DataSet();

        try
        {

            //string strQry = " SELECT a.Sf_Code, a.sf_name,a.Division_Code,a.sf_TP_Active_Flag,a.sf_type,a.sf_status,State_Code,a.SF_UserName ";
            //strQry += " FROM vwUserDetails a WHERE a.UsrDfd_UserName ='" + usr_id + "' and a.Sf_Password='" + pwd + "'";

            string strQry = " SELECT a.Sf_Code, a.sf_name,a.Division_Code,a.sf_TP_Active_Flag,a.sf_type,a.sf_status,State_Code,a.SF_UserName  ";
            strQry += " FROM  vwUserDetails a WHERE a.UsrDfd_UserName = @usr_id  AND  a.Sf_Password=@pwd";
            using (var con = new SqlConnection(Global.ConnString))
            {
                using (var cmd = con.CreateCommand())
                {
                    cmd.CommandText = strQry;
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@usr_id", Convert.ToString(usr_id));
                    cmd.Parameters.AddWithValue("@pwd", Convert.ToString(pwd));
                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = cmd;
                    con.Open();
                    da.Fill(dsDivision);
                    con.Close();
                    con.Dispose();
                }
            }

            //dsDivision = db_ER.Exec_DataSet(strQry);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return dsDivision;
    }
    protected void btnLogin_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>console.time();</script>");
        string Menu_Type = "0";
        System.Threading.Thread.Sleep(time);
        UserLogin ul = new UserLogin();
        user ul1 = new user();
        DataSet dsListeddr = null;
        if (HiddenField1.Value != "undefined")
        {
            shrtname = HiddenField1.Value.Replace(".", "");
            Session["CmpIDKey"] = HiddenField1.Value;
            //shrtname = "sanfmcg";
        }
        if (txtUserName.Text == "")
        {
            msg.Visible = true;
            msg.Text = "Enter User Name.";
            txtUserName.Focus();
            return;
        }
        else if (txtPassWord.Text == "")
        {
            msg.Visible = true;
            msg.Text = "Enter Password.";
            txtPassWord.Focus();
            return;
        }
        try
        {

            dsListeddr = ul.Process_Type(txtUserName.Text, txtPassWord.Text.Trim());
            Menu_Type = dsListeddr.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
        }
        catch (Exception)
        { }



        txtUserName.Text = txtUserName.Text.Replace("--", "");
        txtUserName.Text = txtUserName.Text.Replace("'", "");
        txtPassWord.Text = txtPassWord.Text.Replace("--", "");
        txtPassWord.Text = txtPassWord.Text.Replace("'", "");
        dsLogin = ul.Process_Login(txtUserName.Text, txtPassWord.Text.Trim());
        if (dsLogin.Tables[0].Rows.Count == 0)
        {
            shrtname = "sanfmcg";
            dsLogin1 = ul1.HO_Login(txtUserName.Text, txtPassWord.Text.Trim(), shrtname, Menu_Type);
            if (dsLogin1.Tables[0].Rows.Count != 0 && (dsLogin1.Tables[0].Rows[0]["standby"].ToString() == "0" || dsLogin1.Tables[0].Rows[0]["standby"].ToString() == ""))
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>console.timeEnd();</script>");
                Session["sf_code"] = "admin";
                Session["HO_ID"] = dsLogin1.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                Session["division_code"] = dsLogin1.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();
                Session["sf_name"] = dsLogin1.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                Session["Corporate"] = dsLogin1.Tables[0].Rows[0].ItemArray.GetValue(5).ToString();
                Session["div_Name"] = dsLogin1.Tables[0].Rows[0].ItemArray.GetValue(4).ToString();
                Session["sf_type"] = "3";
                Session["Designation_Short_Name"] = "";
                Session["Sf_HQ"] = "";
                Session["div_code"] = dsLogin1.Tables[0].Rows[0].ItemArray.GetValue(2).ToString().Replace(",", "");
                Session["Title_Admin"] = dsLogin1.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                Session["Menu_type"] = Menu_Type.ToString();
                Session["sub_division"] = dsLogin1.Tables[0].Rows[0]["sub_div_code"].ToString();
                DBase_EReport.Global.AttanceEdit = dsLogin1.Tables[0].Rows[0]["atndceEdit"].ToString();
                Global.ExpenseType = dsLogin1.Tables[0].Rows[0]["ExpType"].ToString();
                AllowTyp = GetAlloTyp();
                Global.AllowanceType = AllowTyp;
                //Globals.AttanceEdit = dsLogin1.Tables[0].Rows[0]["atndceEdit"].ToString();
                Session["sf_code_admin"] = "admin";
                Response.Redirect("~/DashBoard.aspx");

            }

            else
            {
                dsLogin1 = DSM_Login(txtUserName.Text, txtPassWord.Text, shrtname);
                if (dsLogin1.Tables[0].Rows.Count != 0)
                {
                    if (dsLogin1.Tables[0].Rows[0]["sf_status"].ToString() == "0" || dsLogin1.Tables[0].Rows[0]["sf_status"].ToString() == "")
                    {
                        Session["Sf_Code"] = dsLogin1.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                        Session["State"] = dsLogin1.Tables[0].Rows[0].ItemArray.GetValue(6).ToString();
                        //Session["HO_ID"] = dsLogin1.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                        Session["div_code"] = dsLogin1.Tables[0].Rows[0].ItemArray.GetValue(2).ToString().Replace(",", "");
                        Session["Division_Code"] = dsLogin1.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();
                        Session["sf_name"] = dsLogin1.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                        //Session["Corporate"] = dsLogin1.Tables[0].Rows[0].ItemArray.GetValue(5).ToString();
                        //Session["div_Name"] = dsLogin1.Tables[0].Rows[0].ItemArray.GetValue(4).ToString();
                        if (dsLogin1.Tables[0].Rows[0].ItemArray.GetValue(4).ToString() == "5")
                        {
                            Session["sf_type"] = "5";
                        }
                        else
                        {
                            Session["sf_type"] = "4";
                        }
                        Session["Designation_Short_Name"] = "";
                        Session["Sf_HQ"] = "";
                        //  Session["div_code"] = "";
                        Session["Title_DIS"] = dsLogin1.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                        Session["UserName"] = dsLogin1.Tables[0].Rows[0].ItemArray.GetValue(7).ToString();
                        if (dsLogin1.Tables[0].Rows[0].ItemArray.GetValue(4).ToString() == "5")
                            Server.Transfer("~/Default5.aspx");
                        else
                            Server.Transfer("~/Default4.aspx");
                    }
                    else if (dsLogin1.Tables[0].Rows[0]["SF_Status"].ToString() == "1")
                    {
                        Session["sf_type"] = "3";
                        Session["sf_code"] = dsLogin1.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                        Session["div_code"] = dsLogin1.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();
                        Response.Redirect("Block_Vacant.aspx?type=" + dsLogin1.Tables[0].Rows[0]["SF_Status"].ToString());
                    }
                }
                else
                {
                    if (HiddenField1.Value.IndexOf('.') <= -1)
                    {
                        Response.Redirect("#" + shrtname);
                    }
                    txtPassWord.Text = "";
                    txtUserName.Text = "";
                    msg.Visible = true;
                    msg.Text = "Invalid User Name or Password.";
                    txtUserName.Focus();
                }
            }
        }
        else if (dsLogin.Tables[0].Rows[0]["SF_Status"].ToString() == "1")
        {
            Response.Redirect("Vacant.aspx");
        }
        else if (dsLogin.Tables[0].Rows[0]["SF_Status"].ToString() == "3" || dsLogin.Tables[0].Rows[0]["SF_Status"].ToString() == "2")
        {
            Session["sf_type"] = "3";
            Session["sf_code"] = dsLogin.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
            Session["div_code"] = dsLogin.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();
            Response.Redirect("Block_Vacant.aspx?type=" + dsLogin.Tables[0].Rows[0]["SF_Status"].ToString());
        }
        else if (dsLogin.Tables[0].Rows[0]["SF_Status"].ToString() == "2")
        {
            txtPassWord.Text = "";
            txtUserName.Text = "";
            msg.Visible = true;
            msg.Text = "Invalid User Name or Password.";
        }
        else if (dsLogin.Tables[0].Rows[0]["standby"].ToString() == "1")
        {
            Response.Redirect("Standby.aspx");
        }
        else
        {

            Session["sf_code"] = dsLogin.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
            Session["div_code"] = dsLogin.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();
            Session["sf_name"] = dsLogin.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
            Session["sf_type"] = dsLogin.Tables[0].Rows[0].ItemArray.GetValue(4).ToString();
            Session["Designation_Short_Name"] = dsLogin.Tables[0].Rows[0].ItemArray.GetValue(5).ToString();
            Session["Sf_HQ"] = dsLogin.Tables[0].Rows[0].ItemArray.GetValue(6).ToString();
            Session["HO_ID"] = "";
            Session["Corporate"] = "";
            Session["division_code"] = "";
            Session["div_name"] = dsLogin.Tables[0].Rows[0].ItemArray.GetValue(8).ToString();
            BindImage1();
            BindImage_FieldForce();

            //DataSet dsMailCheck = ul.Check_Mail(Session["sf_code"].ToString(), Session["div_code"].ToString());
            if (Session["sf_type"].ToString() == "1") // MR Login
            {
                //dsLogin1 = ul.DSM_Login(txtUserName.Text, txtPassWord.Text,shrtname);
                //if (dsLogin1.Tables[0].Rows.Count != 0 && (dsLogin1.Tables[0].Rows[0]["sf_status"].ToString() == "0" || dsLogin1.Tables[0].Rows[0]["sf_status"].ToString() == ""))
                //{
                AdminSetup adm = new AdminSetup();
                dsAdmin = adm.Get_Quote_Home(Session["div_code"].ToString());
                AdminSetup admin = new AdminSetup();
                dsAdm = admin.Get_Flash_News_Home(Session["div_code"].ToString());
                AdminSetup adm_Nb = new AdminSetup();
                dsAdmNB = adm_Nb.Get_Notice_Home(Session["div_code"].ToString());
                SalesForce sf = new SalesForce();
                dsSalesForce = sf.getFieldForce_Birth(Session["sf_code"].ToString(), Session["div_code"].ToString());
                //SalesForce sf_img = new SalesForce();
                //dsImage_FF = sf_img.Sales_Image(Session["div_code"].ToString(), Session["sf_code"].ToString());
                ListedDR lstDr = new ListedDR();
                int Count;

                Count = admin.get_Mail_MR_MGR_Count(Session["sf_code"].ToString());

                if (dsImage.Tables[0].Rows.Count > 0)
                {
                    System.Threading.Thread.Sleep(time);
                    Response.Redirect("HomePage_Image.aspx");
                }
                else
                {
                    System.Threading.Thread.Sleep(time);
                    Session["sf_type"] = "1";
                    Session["sf_code_MR"] = dsLogin.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                    Session["Title_MR"] = dsLogin.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                    Session["Title_MGR"] = dsLogin.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                    Session["Title_Admin"] = dsLogin.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                    Session["Corporate"] = "";
                    Session["Menu_type"] = Menu_Type.ToString();
                    Server.Transfer("~/DashBoard_MR.aspx");
                }
                //}
            }
            else if (Session["sf_type"].ToString() == "2") // MGR Login
            {

                AdminSetup adm = new AdminSetup();
                dsAdmin = adm.Get_Quote_Home(Session["div_code"].ToString());
                AdminSetup admin = new AdminSetup();
                dsAdm = admin.Get_Flash_News_Home(Session["div_code"].ToString());
                AdminSetup adm_Nb = new AdminSetup();
                dsAdmNB = adm_Nb.Get_Notice_Home(Session["div_code"].ToString());
                SalesForce sf = new SalesForce();
                dsSalesForce = sf.getFieldForce_Birth(Session["sf_code"].ToString(), Session["div_code"].ToString());
                ListedDR lstDr = new ListedDR();
                int Count;

                Count = admin.get_Mail_MR_MGR_Count(Session["sf_code"].ToString());
                if (dsImage.Tables[0].Rows.Count > 0)
                {
                    Response.Redirect("HomePage_Image.aspx");
                }
                else
                {
                    //Server.Transfer("~/Default_MGR.aspx");
                    Session["sf_type"] = "2";
                    Session["sf_code_MR"] = dsLogin.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                    Session["Title_MR"] = dsLogin.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                    Session["Title_MGR"] = dsLogin.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                    Session["Title_Admin"] = dsLogin.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                    Session["sub_division"] = dsLogin.Tables[0].Rows[0]["subdivision_code"].ToString().TrimEnd(',');
                    Session["Corporate"] = "";
                    Global.ExpenseType = dsLogin.Tables[0].Rows[0]["ExpType"].ToString();
                    AllowTyp = GetAlloTyp();
                    Global.AllowanceType = AllowTyp;
                    Session["Menu_type"] = Menu_Type.ToString();
                    Server.Transfer("~/DashBoard1.aspx");
                }
            }
            else if (Session["sf_type"].ToString() == "3") // DSM Login
            {

                AdminSetup adm = new AdminSetup();
                dsAdmin = adm.Get_Quote_Home(Session["div_code"].ToString());
                AdminSetup admin = new AdminSetup();
                dsAdm = admin.Get_Flash_News_Home(Session["div_code"].ToString());
                AdminSetup adm_Nb = new AdminSetup();
                dsAdmNB = adm_Nb.Get_Notice_Home(Session["div_code"].ToString());
                SalesForce sf = new SalesForce();
                dsSalesForce = sf.getFieldForce_Birth(Session["sf_code"].ToString(), Session["div_code"].ToString());
                ListedDR lstDr = new ListedDR();
                int Count;

                Count = admin.get_Mail_MR_MGR_Count(Session["sf_code"].ToString());
                if (dsImage.Tables[0].Rows.Count > 0)
                {
                    Response.Redirect("HomePage_Image.aspx");
                }
                else if (dsImage_FF.Tables[0].Rows.Count > 0)
                {
                    Server.Transfer("HomePage_FieldForcewise.aspx");

                }
                else if (dsAdmin.Tables[0].Rows.Count > 0)
                {
                    Server.Transfer("Quote_Design.aspx");

                }
                else if (dsAdmNB.Tables[0].Rows.Count > 0)
                {
                    Server.Transfer("NoticeBoard_design.aspx");

                }
                else if (dsAdm.Tables[0].Rows.Count > 0)
                {
                    Server.Transfer("FlashNews_Design.aspx");
                }

                else if (dsSalesForce.Tables[0].Rows.Count > 0)
                {
                    Response.Redirect("Birthday_Wish.aspx");
                }
                else if (Count != 0)
                {
                    System.Threading.Thread.Sleep(time);
                    Response.Redirect("MasterFiles/Mails/Mail_Head.aspx");
                }
                else
                {
                    Server.Transfer("~/Default_MGR.aspx");
                }
            }
            else
            {
                Session["Title_Admin"] = "Admin";
                Session["Corporate"] = "Admin Panel";

                Server.Transfer("AdminHome.aspx");
            }
        }
        //AllowTyp = GetAlloTyp(Session["div_code"].ToString());
    }
    public string GetAlloTyp()
    {
        string Div_code = Session["div_code"].ToString();
        DataSet dsSalesForce = new DataSet();
        string Val = "";
        DB_EReporting db_ER = new DB_EReporting();

        try
        {

            //string strQry = "select AllowanceType from Access_Master where division_code=" + Div_code + "";

            string strQry = " select AllowanceType FROM Access_Master WHERE division_code=@Div_code ";

            using (var con = new SqlConnection(Global.ConnString))
            {
                using (var cmd = con.CreateCommand())
                {
                    cmd.CommandText = strQry;
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@Div_code", Convert.ToString(Div_code));
                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = cmd;
                    con.Open();
                    da.Fill(dsSalesForce);
                    con.Close();
                    con.Dispose();
                }
            }

            //dsSalesForce = db_ER.Exec_DataSet(strQry);

            if (dsSalesForce.Tables.Count > 0)
            {
                Val = dsSalesForce.Tables[0].Rows[0]["AllowanceType"].ToString();
                dsSalesForce.Dispose();
            }
            else { Val = ""; }

        }
        catch (Exception ex)
        {
            throw ex;
        }

        return Val;
    }
    public class user
    {

        public DataSet HO_Login(string usr_id, string pwd, string shrtname, string Menu_type)
        {
            string strQry = String.Empty;
            //if (Menu_type == "h")
            //{
            //    //strQry = " DECLARE @SubDivicode as varchar(100) ";
            //    //strQry += " DECLARE @standby as varchar(100) ";
            //    //strQry += " SET @SubDivicode=(SELECT Division_Name FROM Mas_Division WHERE Division_Code = ";
            //    //strQry += " (SELECT reverse(stuff(reverse(Division_Code), 1, 10, '')) FROM Mas_HO_ID_Creation ";
            //    //strQry += " WHERE User_Name= '" + usr_id + "' AND Password = '" + pwd + "' AND HO_Active_Flag =0))  ";
            //    //strQry += " SET @standby=(SELECT standby FROM Mas_Division WHERE Division_Code = (SELECT reverse(stuff(reverse(Division_Code), 1, 10, '')) ";
            //    //strQry += " FROM Mas_HO_ID_Creation ";
            //    //strQry += " WHERE User_Name= '" + usr_id + "' AND Password =  '" + pwd + "' AND HO_Active_Flag =0))  ";
            //    //strQry += " SELECT HO_ID, User_Name,oh.Division_Code,Password,@SubDivicode as name,Name as Corporate,@standby as standby,";
            //    //strQry += " ISNULL(sub_div_code,'0')sub_div_code,atndceEdit,isnull(Exp_Web_Auto,0) ExpType ";
            //    //strQry += " FROM Mas_HO_ID_Creation oh INNER JOIN  Access_Master ac ON ";
            //    //strQry += " replace(oh.Division_Code,',','')=cast(replace(ac.division_code,',','') as varchar)";
            //    //strQry += " WHERE User_Name= '" + usr_id + "' AND Password =  '" + pwd + "' AND HO_Active_Flag=0  ";

            //    strQry = " DECLARE @SubDivicode as varchar(100) ";
            //    strQry += " DECLARE @standby as varchar(100) ";
            //    strQry += " SET @SubDivicode=(SELECT Division_Name FROM Mas_Division WHERE Division_Code = ";
            //    strQry += " (SELECT reverse(stuff(reverse(Division_Code), 1, 10, '')) FROM Mas_HO_ID_Creation ";
            //    strQry += " WHERE User_Name= @usr_id AND Password = @pwd AND HO_Active_Flag =0))  ";
            //    strQry += " SET @standby=(SELECT standby FROM Mas_Division WHERE Division_Code = (SELECT reverse(stuff(reverse(Division_Code), 1, 10, '')) ";
            //    strQry += " FROM Mas_HO_ID_Creation ";
            //    strQry += " WHERE User_Name=@usr_id AND Password=@pwd AND HO_Active_Flag =0))  ";
            //    strQry += " SELECT HO_ID, User_Name,oh.Division_Code,Password,@SubDivicode as name,Name as Corporate,@standby as standby,";
            //    strQry += " ISNULL(sub_div_code,'0')sub_div_code,atndceEdit,isnull(Exp_Web_Auto,0) ExpType ";
            //    strQry += " FROM Mas_HO_ID_Creation oh INNER JOIN  Access_Master ac ON ";
            //    strQry += " replace(oh.Division_Code,',','')=cast(replace(ac.division_code,',','') as varchar)";
            //    strQry += " WHERE User_Name=@usr_id AND Password=@pwd AND HO_Active_Flag=0  ";
            //}
            //else if (shrtname == "sanfmcg")
            //{
            //    //strQry = " DECLARE @SubDivicode as varchar(100) ";
            //    //strQry += " DECLARE @standby as varchar(100) ";
            //    //strQry += " SET @SubDivicode=(SELECT Division_Name FROM Mas_Division WHERE Division_Code = ";
            //    //strQry += " (SELECT reverse(stuff(reverse(Division_Code), 1, 10, '')) FROM Mas_HO_ID_Creation ";
            //    //strQry += " WHERE User_Name= '" + usr_id + "' AND Password = '" + pwd + "' and HO_Active_Flag =0))  ";
            //    //strQry += " SET @standby=(SELECT standby FROM Mas_Division ";
            //    //strQry += " WHERE Division_Code = (SELECT reverse(stuff(reverse(Division_Code), 1, 10, '')) FROM Mas_HO_ID_Creation ";
            //    //strQry += " Where User_Name= '" + usr_id + "' AND Password =  '" + pwd + "' and HO_Active_Flag =0))  ";
            //    //strQry += " SELECT HO_ID, User_Name,oh.Division_Code,Password,@SubDivicode as name,Name as Corporate,@standby as standby,";
            //    //strQry += " ISNULL(sub_div_code,'0')sub_div_code,atndceEdit,isnull(Exp_Web_Auto,0) ExpType FROM Mas_HO_ID_Creation oh ";
            //    //strQry += " INNER JOIN Access_Master ac on replace(oh.Division_Code,',','')=cast(replace(ac.division_code,',','') as varchar)";
            //    //strQry += " WHERE User_Name= '" + usr_id + "' AND Password =  '" + pwd + "' and HO_Active_Flag=0  ";

            //    strQry = " DECLARE @SubDivicode as varchar(100) ";
            //    strQry += " DECLARE @standby as varchar(100) ";
            //    strQry += " SET @SubDivicode=(SELECT Division_Name FROM Mas_Division WHERE Division_Code = ";
            //    strQry += " (SELECT reverse(stuff(reverse(Division_Code), 1, 10, '')) FROM Mas_HO_ID_Creation ";
            //    strQry += " WHERE User_Name=@usr_id AND Password=@pwd and HO_Active_Flag =0))  ";
            //    strQry += " SET @standby=(SELECT standby FROM Mas_Division ";
            //    strQry += " WHERE Division_Code = (SELECT reverse(stuff(reverse(Division_Code), 1, 10, '')) FROM Mas_HO_ID_Creation ";
            //    strQry += " Where User_Name=@usr_id AND Password=@pwd AND HO_Active_Flag =0))  ";
            //    strQry += " SELECT HO_ID, User_Name,oh.Division_Code,Password,@SubDivicode as name,Name as Corporate,@standby as standby,";
            //    strQry += " ISNULL(sub_div_code,'0')sub_div_code,atndceEdit,isnull(Exp_Web_Auto,0) ExpType FROM Mas_HO_ID_Creation oh ";
            //    strQry += " INNER JOIN Access_Master ac on replace(oh.Division_Code,',','')=cast(replace(ac.division_code,',','') as varchar)";
            //    strQry += " WHERE User_Name=@usr_id  AND Password=@pwd  AND HO_Active_Flag=0  ";

            //}
            //else
            //{
            //    //strQry = " DECLARE @SubDivicode as varchar(100) ";
            //    //strQry += " DECLARE @standby as varchar(100) ";
            //    //strQry += " DECLARE @urlshrtname as varchar(80)  ";
            //    //strQry += " SET @SubDivicode=(SELECT Division_Name FROM Mas_Division WHERE Division_Code = ";
            //    //strQry += " (SELECT reverse(stuff(reverse(Division_Code), 1, 10, '')) FROM Mas_HO_ID_Creation ";
            //    //strQry += " WHERE User_Name= '" + usr_id + "' and Password = '" + pwd + "' and HO_Active_Flag =0))  ";
            //    //strQry += " SET @urlshrtname=(SELECT Url_Short_Name FROM Mas_Division  ";
            //    //strQry += " WHERE Div_Sl_No=( SELECT replace(Division_Code,',','') FROM Mas_HO_ID_Creation  ";
            //    //strQry += " WHERE User_Name= '" + usr_id + "' and Password = '" + pwd + "' and HO_Active_Flag =0))  ";
            //    //strQry += " SET @standby=(SELECT standby from Mas_Division where ";
            //    //strQry += " Division_Code = (SELECT reverse(stuff(reverse(Division_Code), 1, 10, '')) FROM Mas_HO_ID_Creation ";
            //    //strQry += " Where User_Name= '" + usr_id + "' and Password =  '" + pwd + "' and HO_Active_Flag =0))  ";
            //    //strQry += " SELECT HO_ID, User_Name,Division_Code,Password,@SubDivicode as name,Name as Corporate,@standby as standby,";
            //    //strQry += " ISNULL(sub_div_code,'0')sub_div_code,atndceEdit,isnull(Exp_Web_Auto,0) ExpType FROM Mas_HO_ID_Creation oh  ";
            //    //strQry += " INNER JOIN Access_Master ac on replace(oh.Division_Code,',','')=cast(replace(ac.division_code,',','') as varchar)";
            //    ////strQry += "  Where User_Name= '" + usr_id + "' and Password =  '" + pwd + "' and @urlshrtname='" + shrtname + "' and HO_Active_Flag=0  ";
            //    //strQry += "  WHERE User_Name= '" + usr_id + "' and Password =  '" + pwd + "'  and HO_Active_Flag=0  ";

            //    strQry = " DECLARE @SubDivicode as varchar(100) ";
            //    strQry += " DECLARE @standby as varchar(100) ";
            //    strQry += " DECLARE @urlshrtname as varchar(80)  ";
            //    strQry += " SET @SubDivicode=(SELECT Division_Name FROM Mas_Division WHERE Division_Code = ";
            //    strQry += " (SELECT reverse(stuff(reverse(Division_Code), 1, 10, '')) FROM Mas_HO_ID_Creation ";
            //    strQry += " WHERE User_Name=@usr_id  AND Password=@pwd  AND HO_Active_Flag =0))  ";
            //    strQry += " SET @urlshrtname=(SELECT Url_Short_Name FROM Mas_Division  ";
            //    strQry += " WHERE Div_Sl_No=( SELECT replace(Division_Code,',','') FROM Mas_HO_ID_Creation  ";
            //    strQry += " WHERE User_Name=@usr_id  AND Password=@pwd  AND HO_Active_Flag =0))  ";
            //    strQry += " SET @standby=(SELECT standby from Mas_Division where ";
            //    strQry += " Division_Code = (SELECT reverse(stuff(reverse(Division_Code), 1, 10, '')) FROM Mas_HO_ID_Creation ";
            //    strQry += " Where User_Name=@usr_id  AND Password=@pwd  AND  HO_Active_Flag =0))  ";
            //    strQry += " SELECT HO_ID, User_Name,Division_Code,Password,@SubDivicode as name,Name as Corporate,@standby as standby,";
            //    strQry += " ISNULL(sub_div_code,'0')sub_div_code,atndceEdit,isnull(Exp_Web_Auto,0) ExpType FROM Mas_HO_ID_Creation oh  ";
            //    strQry += " INNER JOIN Access_Master ac on replace(oh.Division_Code,',','')=cast(replace(ac.division_code,',','') as varchar)";
            //    //strQry += "  Where User_Name= '" + usr_id + "' and Password =  '" + pwd + "' and @urlshrtname='" + shrtname + "' and HO_Active_Flag=0  ";
            //    strQry += "  WHERE User_Name=@usr_id  AND Password=@pwd  AND HO_Active_Flag=0  ";

            //}

            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = new DataSet();

            try
            {
                using (var con = new SqlConnection(Global.ConnString))
                {
                    using (var cmd = con.CreateCommand())
                    {
                        cmd.CommandText = "GET_UserLoginDetails";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@usr_id", Convert.ToString(usr_id));
                        cmd.Parameters.AddWithValue("@pwd", Convert.ToString(pwd));
                        cmd.Parameters.AddWithValue("@shrtname", Convert.ToString(shrtname));
                        cmd.Parameters.AddWithValue("@Menu_type", Convert.ToString(Menu_type));
                        SqlDataAdapter da = new SqlDataAdapter();
                        da.SelectCommand = cmd;
                        con.Open();
                        da.Fill(dsDivision);
                        con.Close();
                        con.Dispose();
                    }
                }



                //dsDivision = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsDivision;
        }
    }
}
