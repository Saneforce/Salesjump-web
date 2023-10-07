using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
public partial class NoticeBoard_design : System.Web.UI.Page
{
    string div_code = string.Empty; 
    DataSet dsAdmin = new DataSet();
    DataSet dsSalesForce = new DataSet();
    DataSet dsImage = new DataSet();
    DataSet dsLogin = null;
    DataSet dsdiv = null;
    string sf_type = string.Empty;
    string Start_Date = string.Empty;
    string str = string.Empty;
    string End_Date = string.Empty;
    string strMultiDiv = string.Empty;
    int Count;
    protected void Page_Load(object sender, EventArgs e)
    {
       
        sf_type = Session["sf_type"].ToString();
       
        if (sf_type == "3")
        {
            div_code = Session["division_code"].ToString();
        }
        else
        {
            div_code = Session["div_code"].ToString();
        }
      
        str = Request.QueryString["id"];
        if (str == "MRLink")
        {
            btnHome.Visible = false;
            btnLogout.Visible = false;
            btnNext.Visible = false;
        }
        if (!Page.IsPostBack)
        {
            LblUser.Text = "Welcome " + Session["sf_name"] + " - " + Session["Designation_Short_Name"] + " - " + Session["Sf_HQ"];
            lbldiv.Text = Session["div_name"].ToString();

            if (Session["sf_type"].ToString() == "" || Session["sf_type"].ToString() == null || Session["sf_type"].ToString() == "3")
            {
                pnldivi.Visible = true;
                ddlDivision.SelectedIndex = 1;
                Filldiv();
                btnGo_Click(sender, e);
            }
            else
            {
                pnldivi.Visible = false;
                FillNotice();
            }
        }
    }
    protected void btnGo_Click(object sender, EventArgs e)
    {
        //AdminSetup adm = new AdminSetup();
        //dsAdmin = adm.Get_Notice(ddlDivision.SelectedValue);
        //if (dsAdmin.Tables[0].Rows.Count > 0)
        //{
        //    lblCon1.Text = dsAdmin.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
        //    lblCon2.Text = dsAdmin.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();
        //    lblCon3.Text = dsAdmin.Tables[0].Rows[0].ItemArray.GetValue(3).ToString();
        //}
        //else
        //{
        //    lblCon1.Visible = false;
        //    lblCon2.Visible = false;
        //    lblCon3.Visible = false;
        //    lblnorecords.Visible = true;
        //}
        FillNotice();
    }

    private void FillNotice()
    {
        AdminSetup adm = new AdminSetup();
        if (Session["sf_type"].ToString() == "" || Session["sf_type"].ToString() == null || Session["sf_type"].ToString() == "3")
        {
            div_code = ddlDivision.SelectedValue;
        }
        else
        {
            div_code = Session["div_code"].ToString();
        }
        dsAdmin = adm.Get_Notice(div_code);
        if (dsAdmin.Tables[0].Rows.Count > 0)
        {
            lblCon1.Text = dsAdmin.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
            lblCon2.Text = dsAdmin.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();
            lblCon3.Text = dsAdmin.Tables[0].Rows[0].ItemArray.GetValue(3).ToString();
        }
        else
        {
            lblCon1.Visible = false;
            lblCon2.Visible = false;
            lblCon3.Visible = false;
            lblnorecords.Visible = true;
        }
    }
    private void Filldiv()
    {
        Division dv = new Division();
        DataSet dsDivision = new DataSet();
        if (sf_type == "3")
        {
            string[] strDivSplit = div_code.Split(',');
            foreach (string strdiv in strDivSplit)
            {
                if (strdiv != "")
                {
                    dsdiv = dv.getDivisionHO(strdiv);
                    ListItem liTerr = new ListItem();
                    liTerr.Value = dsdiv.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                    liTerr.Text = dsdiv.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                    ddlDivision.Items.Add(liTerr);
                }
            }
        }
        else
        {
            if (strMultiDiv != "")
            {
                dsDivision = dv.getMultiDivision(strMultiDiv);
            }
            else
            {
                dsDivision = dv.getDivision_Name();
            }
           
            if (dsDivision.Tables[0].Rows.Count > 0)
            {
                ddlDivision.DataTextField = "Division_Name";
                ddlDivision.DataValueField = "Division_Code";
                ddlDivision.DataSource = dsDivision;
                ddlDivision.DataBind();
            }
        }

    }
    protected void btnHome_Click(object sender, EventArgs e)
    {
        if (Session["sf_type"].ToString() == "1") // MR Login
        {
            SalesForce sf = new SalesForce();
            dsSalesForce = sf.getFieldForce_Birth(Session["sf_code"].ToString(), Session["div_code"].ToString());
            AdminSetup admin = new AdminSetup();
            dsAdmin = admin.Get_Flash_News_Home(Session["div_code"].ToString());

            int Count;

            Count = admin.get_Mail_MR_MGR_Count(Session["sf_code"].ToString());

           if (dsAdmin.Tables[0].Rows.Count > 0)
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
            AdminSetup admin = new AdminSetup();
            dsAdmin = admin.Get_Flash_News_Home(Session["div_code"].ToString());

            SalesForce sf = new SalesForce();
            dsSalesForce = sf.getFieldForce_Birth(Session["sf_code"].ToString(), Session["div_code"].ToString());
           

            Count = admin.get_Mail_MR_MGR_Count(Session["sf_code"].ToString());
            if (dsAdmin.Tables[0].Rows.Count > 0)
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