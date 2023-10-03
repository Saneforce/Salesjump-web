using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using Bus_EReport;
using DBase_EReport;
using System.Web.Script.Serialization;
using System.Drawing.Imaging;
public partial class Reports_Campaign_View : System.Web.UI.Page
{
    string div_code = string.Empty;
    string sf_code = string.Empty;
    string strSf_Code = string.Empty;
    DataSet dsSalesForce = null;
    DataSet dsDocSubCat = null;
    DataSet dsState = null;
    DataSet dsSfMR = null;
        string sDRCatg_Count = string.Empty;
       bool isff = false;
    int iDRCatg = -1;

    int iDRmr = -1;
    int iTotal_FF = 0;
    int[] iTotal_Catg = new int[20];
    int tot_catg = 0;
    int i = -1;
    protected void Page_Load(object sender, EventArgs e)
    {
          div_code = Session["div_code"].ToString();
          sf_code = Session["sf_code"].ToString();
          if (!Page.IsPostBack)
          {
             // menu1.Title = this.Page.Title;
           //   menu1.FindControl("btnBack").Visible = false;
            //  FillManagers();
            //  FillColor();
    if (Session["sf_type"].ToString() == "2")
              {
                  div_code = Session["div_code"].ToString();
                  UserControl_MGR_Menu c1 =
                 (UserControl_MGR_Menu)LoadControl("~/UserControl/MGR_Menu.ascx");
                  Divid.Controls.Add(c1);
                  c1.Title = Page.Title;
                  c1.FindControl("btnBack").Visible = false;
                  ddlFFType.Visible = false;

                  FillMRManagers1();
                  FillColor();

              }

              else if (Session["sf_type"].ToString() == "" || Session["sf_type"].ToString() == "3")
              {
                  div_code = Session["div_code"].ToString();
                  if (div_code.Contains(','))
                  {
                      div_code = div_code.Remove(div_code.Length - 1);
                  }
                  UserControl_MenuUserControl c1 =
                 (UserControl_MenuUserControl)LoadControl("~/UserControl/MenuUserControl.ascx");
                  Divid.Controls.Add(c1);
                  c1.Title = Page.Title;
                  c1.FindControl("btnBack").Visible = false;
                  FillManagers();
                  FillColor();

              }

          }

          else
          {
              
              if (Session["sf_type"].ToString() == "2")
              {
                  div_code = Session["div_code"].ToString();
                  UserControl_MGR_Menu c1 =
                 (UserControl_MGR_Menu)LoadControl("~/UserControl/MGR_Menu.ascx");
                  Divid.Controls.Add(c1);
                  c1.Title = Page.Title;
                  c1.FindControl("btnBack").Visible = false;

              }
              else if (Session["sf_type"].ToString() == "" || Session["sf_type"].ToString() == "3")
              {
                  div_code = Session["div_code"].ToString();
                  if (div_code.Contains(','))
                  {
                      div_code = div_code.Remove(div_code.Length - 1);
                  }
                  UserControl_MenuUserControl c1 =
                 (UserControl_MenuUserControl)LoadControl("~/UserControl/MenuUserControl.ascx");
                  Divid.Controls.Add(c1);
                  c1.Title = Page.Title;
                  c1.FindControl("btnBack").Visible = false;

              }
          }

              
              
          
          FillColor();
          
    }
    private void FillColor()
    {
        int j = 0;


        foreach (ListItem ColorItems in ddlSF.Items)
        {
            //ddlFieldForce.Items[j].Selected = true;

            string bcolor = "#" + ColorItems.Text;
            ddlFieldForce.Items[j].Attributes.Add("style", "background-color:" + bcolor);

            j = j + 1;

        }
    }
    private void FillManagers()
    {
        SalesForce sf = new SalesForce();

        if (ddlFFType.SelectedValue.ToString() == "1")
        {
            ddlAlpha.Visible = false;
            dsSalesForce = sf.UserList_Hierarchy(div_code, "admin");
        }
        else if (ddlFFType.SelectedValue.ToString() == "0")
        {
            FillSF_Alpha();
            ddlAlpha.Visible = true;
            dsSalesForce = sf.UserList_Alpha(div_code, "admin");
        }
        else if (ddlFFType.SelectedValue.ToString() == "2")
        {
            dsSalesForce = sf.UserList_HQ(div_code, "admin");
        }

        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            ddlFieldForce.DataTextField = "sf_name";
            ddlFieldForce.DataValueField = "sf_code";
            ddlFieldForce.DataSource = dsSalesForce;
            ddlFieldForce.DataBind();

            ddlSF.DataTextField = "des_color";
            ddlSF.DataValueField = "sf_code";
            ddlSF.DataSource = dsSalesForce;
            ddlSF.DataBind();

        }
    }

    private void FillSF_Alpha()
    {
        SalesForce sf = new SalesForce();
        dsSalesForce = sf.getSalesForcelist_Alphabet(div_code);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            ddlAlpha.DataTextField = "sf_name";
            ddlAlpha.DataValueField = "val";
            ddlAlpha.DataSource = dsSalesForce;
            ddlAlpha.DataBind();
            ddlAlpha.SelectedIndex = 0;
        }
    }

    protected void ddlAlpha_SelectedIndexChanged(object sender, EventArgs e)
    {
        SalesForce sf = new SalesForce();
        if (ddlAlpha.SelectedIndex == 0)
        {
            dsSalesForce = sf.UserList_Alpha(div_code, "admin");
        }
        else
        {
            dsSalesForce = sf.UserList_Alphasearch(div_code, "admin", ddlAlpha.SelectedValue);
        }

        //  dsSalesForce = sf.UserList_Alphasearch(ddlDivision.SelectedValue.ToString(), "admin", ddlAlpha.SelectedValue);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            ddlFieldForce.DataTextField = "sf_name";
            ddlFieldForce.DataValueField = "sf_code";
            ddlFieldForce.DataSource = dsSalesForce;
            ddlFieldForce.DataBind();

            ddlSF.DataTextField = "des_color";
            ddlSF.DataValueField = "sf_code";
            ddlSF.DataSource = dsSalesForce;
            ddlSF.DataBind();
            FillColor();
        }

    }
    protected void ddlFFType_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblFF.Text = "Field Force";
        FillManagers();
        FillColor();
    }

    private void FillSalesForce()
    {
        int tot_rows = 0;
        int tot_cols = 0;

        // Fetch the total rows for the table
        SalesForce sf = new SalesForce();
        Territory terr = new Territory();
        sf_code = ddlFieldForce.SelectedValue.ToString();
       
   
        Doctor dr = new Doctor();
        dsDocSubCat = dr.getDocSubCat(div_code);
        if (dsDocSubCat.Tables[0].Rows.Count > 0)
        {
            tot_rows = dsDocSubCat.Tables[0].Rows.Count;
            ViewState["dsDocSubCat"] = dsDocSubCat;
        }

        SalesForce dssf = new SalesForce();

        dsSalesForce = dssf.Hq_Camp(div_code, sf_code);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            tot_cols = dsSalesForce.Tables[0].Rows.Count;
            ViewState["dsSalesForce"] = dsSalesForce;
        }

    


        CreateDynamicTable(tot_rows, tot_cols);
    }
    private void CreateDynamicTable(int tblRows, int tblCols)
    {

        if (ViewState["dsDocSubCat"] != null)
        {
            ViewState["HQ_Det"] = null;
            dsDocSubCat = (DataSet)ViewState["dsDocSubCat"];
            dsSalesForce = (DataSet)ViewState["dsSalesForce"];
          

            TableRow tr_header = new TableRow();
            tr_header.BorderStyle = BorderStyle.Solid;
            tr_header.BorderWidth = 1;

            TableCell tc_SNo = new TableCell();
            tc_SNo.BorderStyle = BorderStyle.Solid;
            tc_SNo.BorderColor = System.Drawing.Color.Black;
            tc_SNo.BorderWidth = 1;
            tc_SNo.Width = 50;
            tc_SNo.ForeColor = System.Drawing.Color.White;
            Literal lit_SNo = new Literal();
            lit_SNo.Text = "<center>S.No</center>";

            tc_SNo.Controls.Add(lit_SNo);
            tc_SNo.RowSpan = 2;
            tc_SNo.Style.Add("font-family", "Calibri");
            tc_SNo.Style.Add("font-size", "10pt");
            tr_header.Cells.Add(tc_SNo);

            tr_header.BackColor = System.Drawing.Color.FromName("#A6A6D2");
           
            TableCell tc_FF = new TableCell();
            tc_FF.BorderStyle = BorderStyle.Solid;
            tc_FF.BorderWidth = 1;
            tc_FF.Width = 250;
            tc_FF.BorderColor = System.Drawing.Color.Black;

            Literal lit_FF = new Literal();
            lit_FF.Text = "<center>Campaign Name</center>";
            tc_FF.Controls.Add(lit_FF);
            tc_FF.ForeColor = System.Drawing.Color.White;
            tc_FF.RowSpan = 2;
            tc_FF.Style.Add("margin-top", "20px");
            tc_FF.Style.Add("font-family", "Calibri");
            tc_FF.Style.Add("font-size", "10pt");

            tr_header.Cells.Add(tc_FF);

            //TableCell tc_catg = new TableCell();
            //Literal lit_catg = new Literal();          
            //lit_catg.Text = "<center>Category</center>"; 

            //tc_catg.Controls.Add(lit_catg);
            //tc_catg.BorderStyle = BorderStyle.Solid;
            //tc_catg.BorderColor = System.Drawing.Color.Black;
            //tc_catg.BorderWidth = 1;
            //tc_catg.ForeColor = System.Drawing.Color.White;
            //tc_catg.Style.Add("font-family", "Calibri");
            //tc_catg.Style.Add("font-size", "10pt");           
            //tc_catg.ColumnSpan = dsDoctor.Tables[0].Rows.Count;        
            //tr_header.Cells.Add(tc_catg);

            TableCell tc_catg = new TableCell();
            Literal lit_catg = new Literal();
            tc_catg.ForeColor = System.Drawing.Color.White;
            tc_catg.Style.Add("font-family", "Calibri");
            tc_catg.Style.Add("font-size", "10pt");

           lit_catg.Text = "<center>HQ</center>"; 
            tc_catg.Controls.Add(lit_catg);
            tc_catg.BorderStyle = BorderStyle.Solid;
            tc_catg.BorderColor = System.Drawing.Color.Black;
            tc_catg.BorderWidth = 1;
            tc_catg.ForeColor = System.Drawing.Color.White;

        
                tc_catg.ColumnSpan = dsSalesForce.Tables[0].Rows.Count;
           
         tr_header.Cells.Add(tc_catg);


            TableCell tc_Total = new TableCell();
            tc_Total.BorderStyle = BorderStyle.Solid;
            tc_Total.BorderWidth = 1;
            tc_Total.BorderColor = System.Drawing.Color.Black;
            tc_Total.Width = 40;
            tc_Total.ForeColor = System.Drawing.Color.White;
            Literal lit_Total = new Literal();
            lit_Total.Text = "<center>Total</center>";
            tc_Total.Controls.Add(lit_Total);
            tc_Total.RowSpan = 2;
            tc_Total.Style.Add("font-family", "Calibri");
            tc_Total.Style.Add("font-size", "10pt");
            tr_header.Cells.Add(tc_Total);

            tbl.Rows.Add(tr_header);

            TableRow tr_catg = new TableRow();

           
                tr_catg.BackColor = System.Drawing.Color.FromName("#99B7B7");
         

            // dsDoctor = (DataSet)ViewState["dsDoctor"];

            dsSalesForce = (DataSet)ViewState["dsSalesForce"];



            foreach (DataRow dataRow in dsSalesForce.Tables[0].Rows)
            {
                TableCell tc_catg_name = new TableCell();
                tc_catg_name.BorderStyle = BorderStyle.Solid;
                tc_catg_name.HorizontalAlign = HorizontalAlign.Center;
                tc_catg_name.BorderWidth = 1;
                tc_catg_name.BorderColor = System.Drawing.Color.Black;
               // tc_catg_name.Width = 60;
                tc_catg_name.ForeColor = System.Drawing.Color.White;
                Literal lit_catg_name = new Literal();
                lit_catg_name.Text = "<center>" + dataRow["sf_hq"].ToString() + "</center>" ;
                if (dataRow["sf_code"].ToString().Contains("MR"))
                {
                    strSf_Code += "'" + dataRow["sf_code"].ToString() + "'" + ",";
                }
                else
                {
                    strSf_Code += "" + dataRow["sf_code"].ToString() + "" + ",";
                }
                tc_catg_name.ToolTip= dataRow["sf_name"].ToString() ;
                tc_catg_name.Controls.Add(lit_catg_name);
                tc_catg_name.Style.Add("font-family", "Calibri");
                tc_catg_name.Style.Add("font-size", "10pt");
                tc_catg_name.Style.Add("text-align", "center");
                tc_catg_name.BackColor = System.Drawing.Color.FromName("#A6A6D2");
     
                tr_catg.Cells.Add(tc_catg_name);
            }
            string stsf_code = strSf_Code.Remove(strSf_Code.Length - 1);
            if (strSf_Code.Contains("MR"))
            {
            Session["Sf_Code_multiple"] = "" + stsf_code + "";
            }
            else
            {
                Session["Sf_Code_multiple"] = "'" + stsf_code + "'";
            }
            tbl.Rows.Add(tr_catg);

            // Details Section
          string sURL = string.Empty;
            int iCount = 0;
            dsDocSubCat = (DataSet)ViewState["dsDocSubCat"];
            
            foreach (DataRow drFF in dsDocSubCat.Tables[0].Rows)
            {


                TableRow tr_det = new TableRow();
                iCount += 1;
                TableCell tc_det_SNo = new TableCell();
                Literal lit_det_SNo = new Literal();
                lit_det_SNo.Text = "<center>" + iCount.ToString() + "</center>";
                tc_det_SNo.BorderStyle = BorderStyle.Solid;
                tc_det_SNo.BorderWidth = 1;

                tc_det_SNo.Controls.Add(lit_det_SNo);
                //tc_det_SNo.Height = 10;
                tr_det.Style.Add("font-family", "Calibri");
                tr_det.Style.Add("font-size", "10pt");
                tr_det.Cells.Add(tc_det_SNo);
                //tr_det.Height = 10;
                tr_det.BackColor = System.Drawing.Color.White;
                TableCell tc_det_FF = new TableCell();
                Literal lit_det_FF = new Literal();
                lit_det_FF.Text = "&nbsp" + drFF["Doc_SubCatName"].ToString();
                tc_det_FF.BorderStyle = BorderStyle.Solid;
                tc_det_FF.BorderWidth = 1;
                tc_det_FF.Controls.Add(lit_det_FF);
                tc_det_FF.Style.Add("text-align", "left");
                tc_det_FF.Style.Add("font-family", "Calibri");
                tr_det.Cells.Add(tc_det_FF);
                   iTotal_FF = 0;
                i = 0;
               
                foreach (DataRow dataRow in dsSalesForce.Tables[0].Rows)
                {
                    TableCell tc_catg_det_name = new TableCell();
                    HyperLink hyp_catg_det_name = new HyperLink();
                    SalesForce sf1 = new SalesForce();
                    Doctor dr_cat1 = new Doctor();
                    iDRmr = 0;
                    if (dataRow["sf_code"].ToString().Contains("MR"))
                    {
                        iDRCatg = dr_cat1.getCamp_Count(drFF["Doc_SubCatCode"].ToString(), dataRow["sf_code"].ToString());

                        iDRmr += iDRCatg;

                        //   sf_code = ddlFieldForce.SelectedValue.ToString();

                        if (iDRmr == 0)
                        {
                            sDRCatg_Count = " - ";
                        }

                        else
                        {
                            sDRCatg_Count = iDRmr.ToString();
                            iTotal_FF = iTotal_FF + iDRCatg;
                            //hyp_catg_det_name.NavigateUrl = "rptDoctorCategory.aspx?sf_code=" + drFF["sf_code"].ToString() + "&cat_code=" + dataRow["Doc_Cat_Code"].ToString() + "&cat_name=" + dataRow["Doc_Cat_name"].ToString() + "&sf_name=" + drFF["sf_name"].ToString() + "&type=" + rdoType.SelectedValue.ToString();

                            sURL = "rptCampaign_View.aspx?sf_code=" + dataRow["sf_code"].ToString() + "&sf_name=" + dataRow["Sf_Name"].ToString() + "&camp_code=" + drFF["Doc_SubCatCode"].ToString() + " &sf_name=" + dataRow["sf_name"].ToString() + "&sf_hq=" + dataRow["sf_hq"].ToString() + "&div=" + div_code;
                            hyp_catg_det_name.Attributes["onclick"] = "javascript:window.open('" + sURL + "',null,'resizable=yes,toolbar=no,menubar=no,scrollbars=yes,status=no,width=600,height=800,left=0,top=0');";
                            hyp_catg_det_name.NavigateUrl = "#";


                            iTotal_Catg[i] = iTotal_Catg[i] + iDRCatg;
                        }
                    }
                    else
                    {
                        dsSfMR = sf1.SalesForceList_New(div_code, dataRow["sf_code"].ToString());
                        Doctor dr_cat = new Doctor();
                        iDRmr = 0;
                        foreach (DataRow dr in dsSfMR.Tables[0].Rows)
                        {
                            iDRCatg = dr_cat.getCamp_Count(drFF["Doc_SubCatCode"].ToString(), dr["sf_code"].ToString());

                            iDRmr += iDRCatg;

                            //   sf_code = ddlFieldForce.SelectedValue.ToString();

                            if (iDRmr == 0)
                            {
                                sDRCatg_Count = " - ";
                            }

                            else
                            {
                                sDRCatg_Count = iDRmr.ToString();
                                iTotal_FF = iTotal_FF + iDRCatg;
                                //hyp_catg_det_name.NavigateUrl = "rptDoctorCategory.aspx?sf_code=" + drFF["sf_code"].ToString() + "&cat_code=" + dataRow["Doc_Cat_Code"].ToString() + "&cat_name=" + dataRow["Doc_Cat_name"].ToString() + "&sf_name=" + drFF["sf_name"].ToString() + "&type=" + rdoType.SelectedValue.ToString();

                                sURL = "rptCampaign_View.aspx?sf_code=" + dataRow["sf_code"].ToString() + "&sf_name=" + dataRow["Sf_Name"].ToString() + "&camp_code=" + drFF["Doc_SubCatCode"].ToString() + " &sf_name=" + dataRow["sf_name"].ToString() + "&sf_hq=" + dataRow["sf_hq"].ToString() + "&div=" + div_code;
                                hyp_catg_det_name.Attributes["onclick"] = "javascript:window.open('" + sURL + "',null,'resizable=yes,toolbar=no,menubar=no,scrollbars=yes,status=no,width=600,height=800,left=0,top=0');";
                                hyp_catg_det_name.NavigateUrl = "#";


                                iTotal_Catg[i] = iTotal_Catg[i] + iDRCatg;
                            }
                        }
                    }
                 
                        hyp_catg_det_name.Text = "<center>" + sDRCatg_Count + "</center>";

                        tc_catg_det_name.BorderStyle = BorderStyle.Solid;
                        tc_catg_det_name.VerticalAlign = VerticalAlign.Middle;
                        tc_catg_det_name.BorderWidth = 1;

                        tc_catg_det_name.Controls.Add(hyp_catg_det_name);
                        tr_det.Style.Add("text-align", "left");
                        tr_det.Style.Add("font-family", "Calibri");
                        tr_det.Cells.Add(tc_catg_det_name);

                        i++;
                    }
               
                TableCell tc_det_FF_Count = new TableCell();
                //Literal lit_det_FF_Count = new Literal();
                HyperLink hyp_FF_det_total = new HyperLink();
                //lit_det_FF_Count.Text = "&nbsp;" + "<center>" + iTotal_FF.ToString() + "</center>";                
                //hyp_FF_det_total.Height = 10;
                //hyp_FF_det_total.Width = 20;
                hyp_FF_det_total.Text = "<center>" + iTotal_FF.ToString() + "</center>";
                hyp_FF_det_total.Style.Add("font-size", "10pt");
                hyp_FF_det_total.Style.Add("text-align", "Center");

                    sf_code = ddlFieldForce.SelectedValue.ToString();
               
                //if (iTotal_FF > 0)
                  
                        //hyp_FF_det_total.NavigateUrl = "rptDoctorCategory.aspx?sf_code=" + drFF["sf_code"].ToString() + "&sf_name=" + drFF["sf_name"].ToString() + "&type=" + rdoType.SelectedValue.ToString() + "&div=" + div_code;
                        if (iTotal_FF == 0)
                        {
                            iTotal_FF = 0;
                        }
                        else
                        {
                            foreach (DataRow dataRow in dsSalesForce.Tables[0].Rows)
                            {
                                sURL = "rptCampaign_View.aspx?camp_code=" + drFF["Doc_SubCatCode"].ToString() + "&sf_name=" + dataRow["Sf_Name"].ToString()  +"&div=" + div_code;
                                hyp_FF_det_total.Attributes["onclick"] = "javascript:window.open('" + sURL + "',null,'resizable=yes,toolbar=no,menubar=no,scrollbars=yes,status=no,width=600,height=800,left=0,top=0');";
                                hyp_FF_det_total.NavigateUrl = "#";
                            }
                        }
                   
                tc_det_FF_Count.BorderStyle = BorderStyle.Solid;
                tc_det_FF_Count.VerticalAlign = VerticalAlign.Middle;
                tc_det_FF_Count.HorizontalAlign = HorizontalAlign.Center;
                tc_det_FF_Count.Style.Add("text-align", "center");
                tc_det_FF_Count.BorderWidth = 1;
              //  tc_det_FF_Count.Width = 25;
                //tc_det_FF_Count.Controls.Add(lit_det_FF_Count);
                tc_det_FF_Count.Controls.Add(hyp_FF_det_total);
                tr_det.Cells.Add(tc_det_FF_Count);
                tr_det.Style.Add("font-size", "10pt");
                tbl.Rows.Add(tr_det);
            }

            TableRow tr_catg_total = new TableRow();
            TableCell tc_catg_Total = new TableCell();
            tc_catg_Total.BorderStyle = BorderStyle.Solid;
            tc_catg_Total.BorderWidth = 1;
            //tc_catg_Total.Width = 25;

            Literal lit_catg_Total = new Literal();
            lit_catg_Total.Text = "<center>Total</center>";
            tc_catg_Total.Controls.Add(lit_catg_Total);
            tc_catg_Total.BackColor = System.Drawing.Color.White;     
            tc_catg_Total.ColumnSpan = 2;
            tc_catg_Total.Style.Add("text-align", "left");
            tc_catg_Total.Style.Add("font-family", "Calibri");
            tc_catg_Total.Style.Add("font-size", "10pt");
            tr_catg_total.Cells.Add(tc_catg_Total);
            i = 0;

            foreach (DataRow dataRow in dsSalesForce.Tables[0].Rows)
            {
                TableCell tc_FF_Total = new TableCell();
                Literal lit_catg_det_name = new Literal();
                HyperLink hyp_catg_det_total = new HyperLink();
                //lit_catg_det_name.Text = "<center>" + iTotal_Catg[i].ToString() + "</center>";
                
                    sf_code = ddlFieldForce.SelectedValue.ToString();
                    if (iTotal_Catg[i] > 0)
                    // hyp_catg_det_total.NavigateUrl = "rptDoctorCategory.aspx?cat_code=" + dataRow["Doc_Cat_Code"].ToString() + "&cat_name=" + dataRow["Doc_Cat_name"].ToString() + "&type=" + rdoType.SelectedValue.ToString() + "&div=" + div_code;
                    if (iTotal_Catg[i] == 0)
                    {
                        iTotal_Catg[i] = 0;
                    }
                    else
                    {
                        sURL = "rptCampaign_View.aspx?sf_code=" + dataRow["sf_code"].ToString() + "&sf_name=" + dataRow["Sf_Name"].ToString() + "&div=" + div_code;
                        hyp_catg_det_total.Attributes["onclick"] = "javascript:window.open('" + sURL + "',null,'resizable=yes,toolbar=no,menubar=no,scrollbars=yes,status=no,width=600,height=800,left=0,top=0');";
                        hyp_catg_det_total.NavigateUrl = "#";

                    }
                hyp_catg_det_total.Text = "<center>" + iTotal_Catg[i].ToString() + "</center>";
                tot_catg = tot_catg + iTotal_Catg[i];
                     tc_FF_Total.Style.Add("font-size", "10pt");
                tc_FF_Total.BorderStyle = BorderStyle.Solid;
                tc_FF_Total.VerticalAlign = VerticalAlign.Middle;
                tc_FF_Total.BorderWidth = 1;
                tc_FF_Total.BackColor = System.Drawing.Color.White; 
                //tc_FF_Total.Controls.Add(lit_catg_det_name);
                tc_FF_Total.Controls.Add(hyp_catg_det_total);
                tr_catg_total.Cells.Add(tc_FF_Total);
                i++;
            }

            TableCell tc_tot_catg = new TableCell();
            tc_tot_catg.BorderStyle = BorderStyle.Solid;
            tc_tot_catg.BorderWidth = 1;
            tc_tot_catg.Style.Add("font-size", "10pt");
            HyperLink hyp_tot_catg_det_total = new HyperLink();
            //Literal lit_tot_catg = new Literal();
            hyp_tot_catg_det_total.Text = "<center>" + tot_catg.ToString() + "</center>";
            if (Session["sf_type"].ToString() == "1")
            {
                sf_code = Session["sf_code"].ToString();

            }
            else
            {
                sf_code = ddlFieldForce.SelectedValue.ToString();
            }
            if (tot_catg > 0)
                foreach (DataRow dataRow in dsSalesForce.Tables[0].Rows)
                {
                    // hyp_tot_catg_det_total.NavigateUrl = "rptDoctorCategory.aspx?mgr_code=" + sf_code + "&type=" + rdoMGRState.SelectedValue.ToString() + "&div=" + div_code;
                    sURL = "rptCampaign_View.aspx?mgr_code=" + sf_code + "&sf_name=" + dataRow["Sf_Name"].ToString() + "&div=" + div_code;

                    hyp_tot_catg_det_total.Attributes["onclick"] = "javascript:window.open('" + sURL + "',null,'resizable=yes,toolbar=no,menubar=no,scrollbars=yes,status=no,width=500,height=800,left=0,top=0');";
                    hyp_tot_catg_det_total.NavigateUrl = "#";
                }
            tc_tot_catg.Controls.Add(hyp_tot_catg_det_total);
            tr_catg_total.Style.Add("text-align", "left");
            tr_catg_total.Style.Add("font-family", "Calibri");
            tr_catg_total.Style.Add("font-size", "10pt");
            tr_catg_total.Cells.Add(tc_tot_catg);
            tr_catg_total.BackColor = System.Drawing.Color.White; 
            tbl.Rows.Add(tr_catg_total);

            ViewState["dynamictable"] = true;
            lblNoRecord.Visible = false;
        }
        else
        {
            lblNoRecord.Visible = true;
           
        }    
        
        
    }
    protected void btnGo_Click(object sender, EventArgs e)
    {
        FillSalesForce();

        ViewState["dsSalesForce"] = null;
        ViewState["dsDocSubCat"] = null;
    }
    private void FillMRManagers1()
    {
        SalesForce sf = new SalesForce();

        dsSalesForce = sf.SalesForceListMgrGet_MgrOnly(div_code, sf_code);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            ddlFieldForce.DataTextField = "sf_name";
            ddlFieldForce.DataValueField = "sf_code";
            ddlFieldForce.DataSource = dsSalesForce;
            ddlFieldForce.DataBind();

            //ddlSF.DataTextField = "Desig_Color";
            //ddlSF.DataValueField = "sf_code";
            //ddlSF.DataSource = dsSalesForce;
            //ddlSF.DataBind();

            ddlSF.DataTextField = "des_color";
            ddlSF.DataValueField = "sf_code";
            ddlSF.DataSource = dsSalesForce;
            ddlSF.DataBind();
        }
        FillColor();


    }
}