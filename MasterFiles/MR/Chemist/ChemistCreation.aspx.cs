using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;

public partial class MasterFiles_MR_Chemist_ChemistCreation : System.Web.UI.Page
{
    DataSet dsChemist = null;
    string sf_code = string.Empty;
    string Chemists_Name = string.Empty;
    string Chemists_Address1 = string.Empty;
    string Chemists_Contact = string.Empty;
    string Chemists_Phone = string.Empty;
    string Chemists_Terr = string.Empty;
    string Chemists_Cat = string.Empty;
    string div_code = string.Empty;
    int iCnt = -1;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int time;
    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        if (Session["sf_type"].ToString() == "1")
        {
            sf_code = Session["sf_code"].ToString();
          //  menu1.Visible = true;
            UserControl_MR_Menu Usc_MR =
           (UserControl_MR_Menu)LoadControl("~/UserControl/MR_Menu.ascx");
            Divid.Controls.Add(Usc_MR);
            Usc_MR.Title = this.Page.Title;
            lblTerrritory.Text = "( " + "<span style='font-weight: bold;color:Maroon;'>For " + Session["sfName"] + " </span>" + " - " +
                                 "<span style='font-weight: bold;color:Maroon;'> " + Session["sf_Designation_Short_Name"] + " </span>" + " - " +
                                  "<span style='font-weight: bold;color:Maroon;'>  " + Session["sf_HQ"] + "</span>" + " )";
            btnBack.Visible = false;

        }
        else
        {
            sf_code = Session["sf_code"].ToString();
            UserControl_MenuUserControl Usc_Menu =
         (UserControl_MenuUserControl)LoadControl("~/UserControl/MenuUserControl.ascx");
           // menu1.Visible = false;
            Session["backurl"] = "ChemistList.aspx";
            Divid.Controls.Add(Usc_Menu);
            Usc_Menu.Title = this.Page.Title;
            btnBack.Visible = false;
            lblTerrritory.Text = "( " + "<span style='font-weight: bold;color:Maroon;'>For " + Session["sfName"] + " </span>" + " - " +
                              "<span style='font-weight: bold;color:Maroon;'> " + Session["sf_Designation_Short_Name"] + " </span>" + " - " +
                               "<span style='font-weight: bold;color:Maroon;'>  " + Session["sf_HQ"] + "</span>" + " )";
        }
        //sf_code = Session["sf_code"].ToString();
        if (!Page.IsPostBack)
        {
            Session["backurl"] = "ChemistList.aspx";
           // menu1.Title = this.Page.Title;
            FillChemist();
            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);
        }

    }
    protected override void OnLoadComplete(EventArgs e)
    {

        ServerEndTime = DateTime.Now;
        TrackPageTime();//It will give you page load time  
    }
    public void TrackPageTime()
    {
        TimeSpan serverTimeDiff = ServerEndTime.Subtract(ServerStartTime);
        time = serverTimeDiff.Minutes;
    }

    protected void grdChemist_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "this.className='highlight_clr'");
            e.Row.Attributes.Add("onmouseout", "this.className='normal'");
        }
    }
    private void FillChemist()
    {
        Chemist chem = new Chemist();
        iCnt = chem.RecordCount(sf_code);
        ViewState["iCnt"] = iCnt.ToString();

        dsChemist = chem.getEmptyChemist();
        if (dsChemist.Tables[0].Rows.Count > 0)
        {
            grdChemist.Visible = true;
            grdChemist.DataSource = dsChemist;
            grdChemist.DataBind();
        }
        else
        {
            grdChemist.DataSource = dsChemist;
            grdChemist.DataBind();
        }
    }

    protected DataSet FillTerritory()
    {
        ListedDR lstDR = new ListedDR();
        dsChemist = lstDR.FetchTerritory(sf_code);
        return dsChemist;
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        int iReturn = -1;
        foreach (GridViewRow gridRow in grdChemist.Rows)
        {
            TextBox txt_Chemist_Name = (TextBox)gridRow.Cells[1].FindControl("Chemists_Name");
            Chemists_Name = txt_Chemist_Name.Text.ToString();
            TextBox txt_chemist_address = (TextBox)gridRow.Cells[2].FindControl("Chemists_Address1");
            Chemists_Address1 = txt_chemist_address.Text.ToString();
            TextBox txt_Chemist_contact = (TextBox)gridRow.Cells[3].FindControl("Chemists_Contact");
            Chemists_Contact = txt_Chemist_contact.Text.ToString();
            TextBox txt_Chemists_Phone = (TextBox)gridRow.Cells[4].FindControl("Chemists_Phone");
            Chemists_Phone = txt_Chemists_Phone.Text.ToString();
            DropDownList ddl_Terr = (DropDownList)gridRow.Cells[6].FindControl("ddlTerr");
            Chemists_Terr = ddl_Terr.SelectedValue.ToString();
            DropDownList ddl_Cat = (DropDownList)gridRow.Cells[5].FindControl("ddlCat");
            Chemists_Cat = ddl_Cat.SelectedValue.ToString();

            Chemist chemi = new Chemist();
            DataSet dschemist_Admin = chemi.getChemist_Allow_Admin(div_code);
            DataSet dschemist_Count = chemi.getChemist_Count(sf_code, div_code);

            if (dschemist_Count.Tables[0].Rows[0][0].ToString() == dschemist_Admin.Tables[0].Rows[0][0].ToString())
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", " alert('Cannot enter more than " + dschemist_Admin.Tables[0].Rows[0][0].ToString() + " Chemists');", true);

            }

            else
            {

                if ((Chemists_Name.Trim().Length > 0) && (Chemists_Terr.Trim().Length > 0))
                {
                    // Add New Listed Doctor                
                    Chemist chem = new Chemist();
                    iReturn = chem.RecordAdd(Chemists_Name, Chemists_Address1, Chemists_Contact, Chemists_Phone, Chemists_Terr, Chemists_Cat, Session["sf_code"].ToString());
                }
                else
                {
                    //menu1.Status = "Enter all the values!!";
                    //ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Enter all the values');</script>");
                }
            }

            if (iReturn > 0)
            {
                //menu1.Status = "Chemists Created Successfully!!";
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Created Successfully');</script>");
                FillChemist();
            }
            else if (iReturn == -2)
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Chemist Name Already Exist');</script>");

            }

        }
    }

    protected void grdChemist_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblSNo = (Label)e.Row.FindControl("lblSNo");
            lblSNo.Text = Convert.ToString(Convert.ToInt32(lblSNo.Text) + Convert.ToInt32(ViewState["iCnt"].ToString()));
        }
        if (e.Row.RowType == DataControlRowType.Header)
        {
            Territory terr = new Territory();
            DataSet dsTerritory = new DataSet();
            dsTerritory = terr.getWorkAreaName(div_code);
            if (dsTerritory.Tables[0].Rows.Count > 0)
            {
                e.Row.Cells[6].Text = dsTerritory.Tables[0].Rows[0]["wrk_area_Name"].ToString();

            }
        }
    }    

    protected void btnClear_Click(object sender, EventArgs e)
    {
        FillChemist(); 
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        try
        {
            Server.Transfer("ChemistList.aspx");
        }
        catch (Exception ex)
        {

        }
    }

    protected DataSet FillCategory()
    {
        Chemist chem = new Chemist();
        dsChemist = chem.FetchCategory(sf_code);
        return dsChemist;
    }
}