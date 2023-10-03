using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;

public partial class MasterFiles_MR_ListedDRCreation : System.Web.UI.Page
{

    #region "Declaration"
    DataSet dsListedDR = null;
    DataSet ds = null;
    DataSet dsadm = null;
    string Listed_DR_Code = string.Empty;
    string codee= string.Empty;
    string Listed_DR_Name = string.Empty;
    string Listed_DR_Address = string.Empty;
    string Listed_DR_Address1 = string.Empty;
    string Listed_DR_Catg = string.Empty;
    string Listed_DR_Spec = string.Empty;
    string Listed_DR_Class = string.Empty;
    string Listed_DR_Qual = string.Empty;
    string Listed_DR_Terr = string.Empty;
    string Catg_Code = string.Empty;
    string Spec_Code = string.Empty;
    string Doc_ClsCode = string.Empty;
    string Qual_Code = string.Empty; 
    string Terr_Code = string.Empty;
    string sf_code = string.Empty;
    string sf_type = string.Empty;
    string ListedDR_Name = string.Empty;
    string div_code = string.Empty;
    string DoCatSName = string.Empty;
    string DoSpecSName = string.Empty;
    string DocQuaName = string.Empty;
    string DoClaSName = string.Empty;
    string Contact = string.Empty;
    string MobileNo = string.Empty;
    string Creditdays = string.Empty;
    int i;
    int iReturn = -1;
    int iCnt = -1;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int time;
    #endregion
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
        if (Session["sf_type"].ToString() == "1")
        {
            sf_code = Session["sf_code"].ToString();
            Terr_Code = Session["Terr_Code"].ToString();
            //UserControl_MR_Menu Usc_MR =
            //(UserControl_MR_Menu)LoadControl("~/UserControl/MR_Menu.ascx");
            //Divid.Controls.Add(Usc_MR);
            //Usc_MR.Title = this.Page.Title;
            //Usc_MR.FindControl("btnBack").Visible = false;
            lblTerrritory.Text = "( " + "<span style='font-weight: bold;color:Maroon;'>For " + Session["Stockist_Name"] + " </span>" + " - " +

                               "<span style='font-weight: bold;color:Maroon;'>  " + Session["Terr_Name"] + "</span>" + " )";        
            btnBack.Visible = true;
            Fillcount();

        }
        else
        {

            sf_code = Session["Sf_code"].ToString();

            Terr_Code = Session["Terr_Code"].ToString();
            //UserControl_MenuUserControl Admin =
            //(UserControl_MenuUserControl)LoadControl("~/UserControl/MenuUserControl.ascx");
            //Divid.Controls.Add(Admin);
            //Admin.FindControl("btnBack").Visible = false;        
            //btnBack.Visible = true;
            //Admin.Title = this.Page.Title;
            lblTerrritory.Text = "( " + "<span style='font-weight: bold;color:Maroon;'>For " + Session["sf_Name"] + " </span>" + " - " +

                                 "<span style='font-weight: bold;color:Maroon;'>  " + Session["Terr_Name"] + "</span>" + " )";
            Fillcount();
          
        }
         
        if (!Page.IsPostBack)
        {           
            FillListedDR();
            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);
           // Fillcount();
        }
       
       // ShowHideTerritory();
    }

    private void Fillcount()
    {
        ListedDR lstdr = new ListedDR();
        dsListedDR = lstdr.getListedDrCount_MR(div_code, sf_code, Terr_Code);
        if (dsListedDR.Tables[0].Rows.Count > 0)
        {
            lblDrcount.Text = dsListedDR.Tables[0].Rows[0][1].ToString();
            lblapp.Text = dsListedDR.Tables[0].Rows[0][2].ToString();
            lbldeact.Text = dsListedDR.Tables[0].Rows[0][3].ToString();
            lbladddeact.Text = dsListedDR.Tables[0].Rows[0][4].ToString();
        }
    }
    private void Fillcount_MR()
    {
        ListedDR lstdr = new ListedDR();
        dsListedDR = lstdr.getListedDrCount_MR(div_code, sf_code, Terr_Code);
        if (dsListedDR.Tables[0].Rows.Count > 0)
        {
            lblDrcount.Text = dsListedDR.Tables[0].Rows[0][1].ToString();
            lblapp.Text = dsListedDR.Tables[0].Rows[0][2].ToString();
            lbldeact.Text = dsListedDR.Tables[0].Rows[0][3].ToString();
            lbladddeact.Text = dsListedDR.Tables[0].Rows[0][4].ToString();
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
    private void FillListedDR()
    {
        ListedDR lstDR = new ListedDR();
        iCnt = lstDR.RecordCount(sf_code,Terr_Code);
        ViewState["iCnt"] = iCnt.ToString();

        dsListedDR  = lstDR.getEmptyListedDR();
        if (dsListedDR.Tables[0].Rows.Count > 0)
        {
            grdListedDR.Visible = true;
            grdListedDR.DataSource = dsListedDR;
            grdListedDR.DataBind();
        }
        else
        {
            grdListedDR.DataSource = dsListedDR;
            grdListedDR.DataBind();
        }
    }

    private void ShowHideTerritory()
    {
        ListedDR lstDR = new ListedDR();
        iCnt = lstDR.Single_Multi_Select_Territory(div_code);
        ViewState["ShowHideTerritory"] = iCnt.ToString();
        if (iCnt < 1)
        {
            grdListedDR.Columns[8].Visible = false;
            grdListedDR.Columns[9].Visible = true;
        }
        else
        {
            grdListedDR.Columns[8].Visible = true;
            grdListedDR.Columns[9].Visible = false;
        }
    }

    //protected DataSet FillTerritory()
    //{
    //    ListedDR lstDR = new ListedDR();
    //    dsListedDR = lstDR.FetchTerritory(sf_code);
    //    if (dsListedDR.Tables[0].Rows.Count <= 1)
    //    {            
    //        Response.Redirect("../Territory/TerritoryCreation.aspx");
    //       // menu1.Status = "Territory must be created prior to Doctor creation";
    //        ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Territory must be created prior to Doctor creation');</script>");
    //    }
    //    return dsListedDR;
    //}

    protected DataSet FillCategory()
    {
        ListedDR lstDR = new ListedDR();
        dsListedDR = lstDR.FetchCategory(sf_code);
        return dsListedDR;
    }

    protected DataSet FillSpeciality()
    {
        ListedDR lstDR = new ListedDR();
        dsListedDR = lstDR.FetchSpeciality(sf_code);
        return dsListedDR;
    }

    protected DataSet FillClass()
    {
        ListedDR lstDR = new ListedDR();
        dsListedDR = lstDR.FetchClass(sf_code);
        return dsListedDR;
    }
    protected DataSet FillQualification()
    {
        ListedDR lstDR = new ListedDR();
        dsListedDR = lstDR.FetchQualification(sf_code);
        return dsListedDR;
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        int iReturn = -1;
        foreach (GridViewRow gridRow in grdListedDR.Rows)
        {
            
            TextBox code = (TextBox)gridRow.Cells[0].FindControl("code");
            codee = code.Text.ToString();
            TextBox txt_ListedDR_Name = (TextBox)gridRow.Cells[1].FindControl("ListedDR_Name");
            Listed_DR_Name = txt_ListedDR_Name.Text.ToString();
            TextBox txt_ListedDR_Address1 = (TextBox)gridRow.Cells[2].FindControl("ListedDR_Address1");
            Listed_DR_Address = txt_ListedDR_Address1.Text.ToString();
             TextBox txt_ListedDR_Address2 = (TextBox)gridRow.Cells[3].FindControl("ListedDR_Address2");
             Listed_DR_Address1 = txt_ListedDR_Address2.Text.ToString();

             TextBox mobile = (TextBox)gridRow.Cells[4].FindControl("Mobileno");
             MobileNo = mobile.Text.ToString();
             TextBox Contactpe = (TextBox)gridRow.Cells[5].FindControl("Contactperso");
             Contact = Contactpe.Text.ToString();
             TextBox creditday = (TextBox)gridRow.Cells[6].FindControl("credi");
             Creditdays = creditday.Text.ToString();
             //string Contactperson = string.Empty;
             //string MobileNo = string.Empty;
             //string Creditdays = string.Empty;




            //DropDownList ddl_Catg = (DropDownList)gridRow.Cells[3].FindControl("ListedDR_Address2");
            //Listed_DR_Catg = ddl_Catg.SelectedValue.ToString();
            DropDownList ddl_Spec = (DropDownList)gridRow.Cells[4].FindControl("ddlspcl");
            Listed_DR_Spec = ddl_Spec.SelectedValue.ToString();
            //DropDownList ddl_Qual = (DropDownList)gridRow.Cells[5].FindControl("ddlQual");
            //Listed_DR_Qual = ddl_Qual.SelectedValue.ToString();
            DropDownList ddl_Class = (DropDownList)gridRow.Cells[5].FindControl("ddlClass");
            Listed_DR_Class = ddl_Class.SelectedValue.ToString();

            //if (ViewState["ShowHideTerritory"].ToString() == "1")
            //{
            //HiddenField hdnStateId = (HiddenField)gridRow.Cells[6].FindControl("hdnTerritoryId");
            //CheckBoxList chkst = (CheckBoxList)gridRow.Cells[6].FindControl("ChkTerritory");
            //for (int i = 0; i < chkst.Items.Count; i++)
            //{
            //    if (chkst.Items[i].Selected)
            //    {
            //        if (chkst.Items[i].Text != "ALL")
            //        {
            //            Listed_DR_Terr += chkst.Items[i].Value + ",";
            //        }
            //    }
            //}

           // }
           // else
            //{
                //DropDownList ddl_Terr = (DropDownList)gridRow.Cells[7].FindControl("ddlTerr");
                //Listed_DR_Terr = ddl_Terr.SelectedValue.ToString();
           // }

            ListedDR LisDr = new ListedDR();
            DataSet dsListedDr_Admin = LisDr.getListDr_Allow_Admin(div_code);
            DataSet dsListDr_Count = LisDr.getListDr_Count(sf_code, div_code,Terr_Code);

            if (dsListDr_Count.Tables[0].Rows[0][0].ToString() == dsListedDr_Admin.Tables[0].Rows[0][0].ToString())
            {
                {

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", " alert('Cannot enter more than " + dsListedDr_Admin.Tables[0].Rows[0][0].ToString() + " Listed Customer');", true);

                }
            }

            else
            {
                int iflag = 0;
                
                Doctor DocSpec = new Doctor();
                ds = DocSpec.getDoctorSpec(ddl_Spec.SelectedValue, div_code);
                if (ds.Tables[0].Rows.Count > 0)
                    DoSpecSName = ds.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

                //Doctor DocQua = new Doctor();
                //ds = DocQua.getDoctorQua(ddl_Qual.SelectedValue, div_code);
                //if (ds.Tables[0].Rows.Count > 0)
                //    DocQuaName = ds.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

                Doctor DoClass = new Doctor();
                ds = DoClass.getDoctorClass(ddl_Class.SelectedValue, div_code);
                if (ds.Tables[0].Rows.Count > 0)
                    DoClaSName = ds.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                //if (Session["sf_type"].ToString() == "1")
                //{

                //    ListedDR lisapp = new ListedDR();
                //    dsadm = lisapp.getListDr_allow_app(div_code);
                //    if (dsadm.Tables[0].Rows.Count > 0)
                //    {
                //        if (dsadm.Tables[0].Rows[0].ItemArray.GetValue(0).ToString() == "0")
                //        {
                //            iflag = 0;
                //        }
                //        else
                //        {
                //            iflag = 2;
                //        }
                //    }
                //}
                //else
                //{
                    iflag = 0;
                    string route = Terr_Code;
                //}
                    if ((Listed_DR_Name.Trim().Length > 0) && (Listed_DR_Address.Trim().Length > 0) && (codee.Trim().Length > 0) && (Listed_DR_Spec.Trim().Length > 0) && (Listed_DR_Class.Trim().Length > 0) && (Listed_DR_Address1.Trim().Length > 0))
                {
                    // Add New Listed Doctor
                    ListedDR lstDR = new ListedDR();
                    iReturn = lstDR.RecordAddLDr(codee, Listed_DR_Name, Listed_DR_Address, Listed_DR_Address1, Listed_DR_Spec, Listed_DR_Class, sf_code, DoSpecSName, DoClaSName, iflag, MobileNo, Contact, Creditdays, route);
                }
                else
                {                  
                }
            }


            if (iReturn > 0)
            {
                //menu1.Status = "Listed Doctor Created Successfully!!";
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Created Successfully');</script>");
                FillListedDR();
            }
            else if (iReturn == -2)
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Customer Name Already Exist');</script>");

            }
        }
    }

    
    protected void btnClear_Click(object sender, EventArgs e)
    {
        FillListedDR();
    }
    
    //Changes done by Priya
    protected void grdListedDR_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "this.className='highlight_clr'");
            e.Row.Attributes.Add("onmouseout", "this.className='normal'");
        }
    }
    protected void grdListedDR_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblSNo = (Label)e.Row.FindControl("lblSNo");
            lblSNo.Text = Convert.ToString(Convert.ToInt32(lblSNo.Text) + Convert.ToInt32(ViewState["iCnt"].ToString()));

            CheckBoxList chkst = (CheckBoxList)e.Row.FindControl("ChkTerritory");
            TextBox txtstate = (TextBox)e.Row.FindControl("txtTerritory");
            HiddenField hdnStateId = (HiddenField)e.Row.FindControl("hdnTerritoryId");

            ListedDR lstDR = new ListedDR();
            dsListedDR = lstDR.FetchTerritory(sf_code);

            //if (dsListedDR.Tables[0].Rows.Count > 0)
            //{
            //    chkst.DataTextField = "Territory_Name";
            //    chkst.DataValueField = "Territory_Code";
            //    chkst.DataSource = dsListedDR;
            //    chkst.DataBind();
            //}
            
        }
        //if (e.Row.RowType == DataControlRowType.Header)
        //{
        //    Territory terr = new Territory();
        //    DataSet dsTerritory = new DataSet();
        //    dsTerritory = terr.getWorkAreaName(div_code);
        //    if (dsTerritory.Tables[0].Rows.Count > 0)
        //    {
        //        e.Row.Cells[8].Text = dsTerritory.Tables[0].Rows[0]["wrk_area_Name"].ToString();

        //    }
        //}
    }


    protected void ChkTerritory_SelectedIndexChanged(object sender, EventArgs e)
    {
        string name1 = "";
        string id1 = "";
        GridViewRow gv1 = (GridViewRow)((Control)sender).NamingContainer;
        CheckBoxList chkst = (CheckBoxList)gv1.FindControl("ChkTerritory");
        TextBox txtstate = (TextBox)gv1.FindControl("txtTerritory");
        HiddenField hdnStateId = (HiddenField)gv1.FindControl("hdnTerritoryId");
        txtstate.Text = "";
        hdnStateId.Value = "";

        if (chkst.Items[0].Text == "ALL" && chkst.Items[0].Selected == true)
        {
            for (int i = 0; i < chkst.Items.Count; i++)
            {
                chkst.Items[i].Selected = true;
            }
        }

        int countSelected = chkst.Items.Cast<ListItem>().Where(i => i.Selected).Count();
        if (countSelected == chkst.Items.Count - 1)
        {
            for (int i = 0; i < chkst.Items.Count; i++)
            {

                chkst.Items[i].Selected = false;
            }

        }

        for (int i = 0; i < chkst.Items.Count; i++)
        {
            if (chkst.Items[i].Selected)
            {
                if (chkst.Items[i].Text != "ALL")
                {
                    name1 += chkst.Items[i].Text + ",";
                    id1 += chkst.Items[i].Value + ",";
                }
            }
        }

        if (name1 == "")
        {
            name1 = "----Select----";
        }

        txtstate.Text = name1.TrimEnd(',');
        hdnStateId.Value = id1.TrimEnd(',');
        //chkst.Attributes.Add("onclick", "checkAll(this);");


    }
    protected void Check()
    {
        CheckBoxList chkst = (CheckBoxList)grdListedDR.Rows[0].FindControl("ChkTerritory");
        if (chkst.Items[0].Text == "ALL" && chkst.Items[0].Selected == true)
        {
            for (int i = 0; i < chkst.Items.Count; i++)
            {

                chkst.Items[i].Selected = true;
                //chkst.Items[i].Selected = true;            

            }
        }
    }

    protected void UnCheck()
    {
        CheckBoxList chkst = (CheckBoxList)grdListedDR.Rows[0].FindControl("ChkTerritory");
        int countSelected = chkst.Items.Cast<ListItem>().Where(i => i.Selected).Count();
        if (countSelected == 13)
        {
            for (int i = 0; i < chkst.Items.Count; i++)
            {

                chkst.Items[i].Selected = false;
                //chkst.Items[i].Selected = true; 
            }

        }

    }


    protected void grdListedDR_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdListedDR.PageIndex = e.NewPageIndex;
        FillListedDR();

    }
   
   
}