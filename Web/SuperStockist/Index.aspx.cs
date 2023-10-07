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
    string div_code = string.Empty;
    string sf_code = string.Empty;
    int time;
    string fileName = string.Empty;
    string assignedIDs = string.Empty;
    string shortna = string.Empty;
    public string sConnectionString;
    public string sUrls = "";
    public string sUSR = "";
    protected void Page_Load(object sender, EventArgs e)
    {
       
        sConnectionString = ConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString;
        sUSR=Request.Url.Host.ToLower().Replace("www.","").Replace(".sanfmcg.com","").ToLower();
       /* 
        if (sUSR=="shivatex") sConnectionString=ConfigurationManager.ConnectionStrings["Ereportcon_shivatex"].ConnectionString;
        if (sUSR == "arasan") sConnectionString = ConfigurationManager.ConnectionStrings["Ereportcon_araran"].ConnectionString;
        if (sUSR == "trident") sConnectionString = ConfigurationManager.ConnectionStrings["Ereportcon_Trident"].ConnectionString;
        if (sUSR == "avantika") sConnectionString = ConfigurationManager.ConnectionStrings["Ereportcon_Avantika"].ConnectionString;
        if (sUSR == "organomix") sConnectionString = ConfigurationManager.ConnectionStrings["Ereportcon_Organomix"].ConnectionString;
        if (sUSR == "marie") sConnectionString = ConfigurationManager.ConnectionStrings["Ereportcon_Marie"].ConnectionString;
        if (sUSR == "tiesar") sConnectionString = ConfigurationManager.ConnectionStrings["Ereportcon_TSR"].ConnectionString;
        if (sUSR == "easysol") sConnectionString = ConfigurationManager.ConnectionStrings["Ereportcon_easysol"].ConnectionString; 
        if (sUSR == "durga") sConnectionString = ConfigurationManager.ConnectionStrings["Ereportcon_durga"].ConnectionString; 
        */
        Global.ConnString = sConnectionString;
        Globals.ConnString = sConnectionString;

        if (!Page.IsPostBack)
        {
            ServerStartTime = DateTime.Now;
            base.OnPreRender(e);
            Session["ID"] = null;
            Session.Abandon();

        }
        shrtname = shortna;

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

        if (div_code.Contains(','))
        {
            div_code = div_code.Remove(div_code.Length - 1);
        }
        SqlConnection con = new SqlConnection(sConnectionString);
        con.Open();
        SqlCommand cmd = new SqlCommand("select FilePath,subject from Mas_HomePage_Image where Division_Code = '" + div_code + "'", con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);

        da.Fill(dsImage);
        con.Close();

    }
    private void BindImage_FieldForce()
    {

        sf_code = Session["sf_code"].ToString();
        div_code = Session["div_code"].ToString();

        if (div_code.Contains(','))
        {
            div_code = div_code.Remove(div_code.Length - 1);
        }


        SqlConnection con = new SqlConnection(sConnectionString);
        con.Open();
        SqlCommand cmd = new SqlCommand("select FilePath from Mas_HomeImage_FieldForce where sf_code='" + sf_code + "' and Division_Code = '" + div_code + "'", con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dsImage_FF);
        con.Close();
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
		string sUSR=HttpContext.Current.Request.Url.Host.ToLower().Replace("www.","").Replace(".sanfmcg.com","");
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
        }
        return obj;
    }
    protected void btnLogin_Click(object sender, EventArgs e)
    {
		ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>console.time();</script>");
        string Menu_Type = "0";
        System.Threading.Thread.Sleep(time);
        UserLogin ul = new UserLogin();
        DataSet dsListeddr = null;
        if (HiddenField1.Value != "undefined")
        {
            shrtname = HiddenField1.Value.Replace(".", "");
            Session["CmpIDKey"] = HiddenField1.Value;
            //shrtname = "sanfmcg";
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
            dsLogin1 = ul.HO_Login(txtUserName.Text, txtPassWord.Text.Trim(), shrtname, Menu_Type);

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
                Session["sf_code_admin"] = "admin";
                Response.Redirect("~/DashBoard.aspx");

            }

            else
            {
                //dsLogin1 = ul.DSM_Login(txtUserName.Text, txtPassWord.Text, shrtname);
                //if (dsLogin1.Tables[0].Rows.Count != 0 && (dsLogin1.Tables[0].Rows[0]["sf_status"].ToString() == "0" || dsLogin1.Tables[0].Rows[0]["sf_status"].ToString() == ""))
                //{
                //    Session["Sf_Code"] = dsLogin1.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                //    //Session["HO_ID"] = dsLogin1.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                //    Session["Division_Code"] = dsLogin1.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();
                //    Session["sf_name"] = dsLogin1.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                //    //Session["Corporate"] = dsLogin1.Tables[0].Rows[0].ItemArray.GetValue(5).ToString();
                //    //Session["div_Name"] = dsLogin1.Tables[0].Rows[0].ItemArray.GetValue(4).ToString();
                //    Session["sf_type"] = "4";
                //    Session["Designation_Short_Name"] = "";
                //    Session["Sf_HQ"] = "";
                //    //  Session["div_code"] = "";
                //    Session["Title_DIS"] = dsLogin1.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                //    Server.Transfer("~/Default4.aspx");
                //}
                dsLogin1 = ul.DSM_Login(txtUserName.Text, txtPassWord.Text, shrtname);
                if (dsLogin1.Tables[0].Rows.Count != 0 && (dsLogin1.Tables[0].Rows[0]["sf_status"].ToString() == "0" || dsLogin1.Tables[0].Rows[0]["sf_status"].ToString() == ""))
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
                    //Server.Transfer("~/Default4.aspx");
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
                    msg.Text = "Invalid User Name and Password.";
                    txtUserName.Focus();
                }
            }
        }
        else if (dsLogin.Tables[0].Rows[0]["sf_TP_Active_Flag"].ToString() == "1")
        {
            Response.Redirect("Vacant.aspx");
        }
        else if (dsLogin.Tables[0].Rows[0]["SF_Status"].ToString() == "1")
        {
            Session["sf_type"] = "3";
            Session["sf_code"] = dsLogin.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
            Session["div_code"] = dsLogin.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();
            Response.Redirect("Block_Vacant.aspx");
        }
        else if (dsLogin.Tables[0].Rows[0]["SF_Status"].ToString() == "2")
        {
            txtPassWord.Text = "";
            txtUserName.Text = "";
            msg.Visible = true;
            msg.Text = "Invalid User Name and Password.";
            //txtUserName.Focus();
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
               /* else if (dsImage_FF.Tables[0].Rows.Count > 0)
                {
                    System.Threading.Thread.Sleep(time);
                    Server.Transfer("HomePage_FieldForcewise.aspx");

                }
                else if (dsAdmin.Tables[0].Rows.Count > 0)
                {
                    System.Threading.Thread.Sleep(time);
                    Server.Transfer("Quote_Design.aspx");

                }
                else if (dsAdmNB.Tables[0].Rows.Count > 0)
                {
                    System.Threading.Thread.Sleep(time);
                    Server.Transfer("NoticeBoard_design.aspx");

                }
                else if (dsAdm.Tables[0].Rows.Count > 0)
                {
                    System.Threading.Thread.Sleep(time);
                    Server.Transfer("FlashNews_Design.aspx");
                }

                else if (dsSalesForce.Tables[0].Rows.Count > 0)
                {
                    System.Threading.Thread.Sleep(time);
                    Response.Redirect("Birthday_Wish.aspx");
                }
                else if (Count != 0)
                {
                    System.Threading.Thread.Sleep(time);
                    Response.Redirect("MasterFiles/Mails/Mail_Head.aspx");
                }*/
                else
                {
                    System.Threading.Thread.Sleep(time);
                    //Server.Transfer("~/Default_MR.aspx");
                    //Session["sf_code_MR"] = dsLogin.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                    //Session["Title_MR"] = dsLogin.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                    //Server.Transfer("Default1.aspx");

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
                /*else if (dsImage_FF.Tables[0].Rows.Count > 0)
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
                }*/
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
    }
}
