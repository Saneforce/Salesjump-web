using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bus_EReport;
using System.Data;
using System.Data.SqlClient;
public partial class HomePage_Image : System.Web.UI.Page
{
    string sfCode = string.Empty;
    string sf_code = string.Empty;
 string div_code = string.Empty;
    DataSet dsAdmin = new DataSet();
    DataSet dsSalesForce = new DataSet();
    DataSet dsImage = new DataSet();
    DataSet dsImage_FF = new DataSet();
    DataSet dsAdm = null;
    DataSet dsAdmNB = null;
    int Count;
    protected void Page_Load(object sender, EventArgs e)
    {
        LblUser.Text = "Welcome " + Session["sf_name"] + " - " + Session["Designation_Short_Name"] + " - " + Session["Sf_HQ"];
        sfCode = Session["sf_code"].ToString();
        div_code = Session["div_code"].ToString();
        if (!Page.IsPostBack)
        {           
            BindImage();
            BindImage_FieldForce();
        }      
    }
    private void BindImage()
    {
        SqlConnection con = new SqlConnection(Globals.ConnString);
        con.Open();
        SqlCommand cmd = new SqlCommand("select FilePath,subject from Mas_HomePage_Image where Division_Code = '" + div_code + "'", con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        con.Close();
        DataList1.DataSource = ds;
        DataList1.DataBind();
    }
    private void BindImage_FieldForce()
    {
        SqlConnection con = new SqlConnection(Globals.ConnString);
        con.Open();
        SqlCommand cmd = new SqlCommand("select FilePath from Mas_HomeImage_FieldForce where Sf_Code='" + sfCode + "' ", con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dsImage_FF);
        con.Close();
    }
    protected void btnHome_Click(object sender, EventArgs e)
    {
        //Response.Redirect("");
        if (Session["sf_type"].ToString() == "1") // MR Login
        {
            AdminSetup adm = new AdminSetup();
            dsAdmin = adm.Get_Quote_Home(Session["div_code"].ToString());
            AdminSetup admin = new AdminSetup();
            dsAdm = admin.Get_Flash_News_Home(Session["div_code"].ToString());
            AdminSetup adm_Nb = new AdminSetup();
            dsAdmNB = adm_Nb.Get_Notice_Home(Session["div_code"].ToString());
            SalesForce sf = new SalesForce();
            dsSalesForce = sf.getFieldForce_Birth(Session["sf_code"].ToString(), Session["div_code"].ToString());    
            BindImage_FieldForce();

            int Count;

            Count = admin.get_Mail_MR_MGR_Count(Session["sf_code"].ToString());

            if (dsImage_FF.Tables[0].Rows.Count > 0)
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
               
                Response.Redirect("MasterFiles/Mails/Mail_Head.aspx");
            }
           
            else
            {
                Server.Transfer("~/Default_MR.aspx");
            }
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
            BindImage_FieldForce();

            int Count;

            Count = admin.get_Mail_MR_MGR_Count(Session["sf_code"].ToString());


                if (dsImage_FF.Tables[0].Rows.Count > 0)
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
                    Response.Redirect("MasterFiles/Mails/Mail_Head.aspx");
                }

                else
                {
                    Server.Transfer("~/Default_MGR.aspx");
                }
            }       
                 
           
    }
    protected void btnLogout_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Index.aspx");
    }

    protected void btnHomepage_Click(object sender, EventArgs e)
    {
        if (Session["sf_type"].ToString() == "2") // MGR Login
        {
            if (Count != 0)
            {
                Response.Redirect("MasterFiles/Mails/Mail_Head.aspx");
            }
            else
            {
                Server.Transfer("~/Default_MGR.aspx");
            }
        }
        else
        {

            if (Count != 0)
            {
                Response.Redirect("MasterFiles/Mails/Mail_Head.aspx");
            }
            else
            {
                Server.Transfer("~/Default_MR.aspx");
            }
        }

    }
}