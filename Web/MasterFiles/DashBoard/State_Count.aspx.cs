using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;

public partial class User_Count : System.Web.UI.Page
{
    DataSet dsSalesForce = null;
    DataSet dsdoc = null;
    DataSet dsDoctor = null;
    DataSet dsTP = null;
    string iPendingCount = string.Empty;
    string div_code = string.Empty;
    string sf_code = string.Empty;
 	string sf_type = string.Empty;
    //DataSet dsState = null;
    string Month = string.Empty;
    string Year = string.Empty;
    int count_tot = 0;
    int count_tot1 = 0;
    DataSet dsDivision = null;
    DataSet dsState = null;
    string state_cd = string.Empty;
    string sState = string.Empty;
    string[] statecd;
	protected void Page_PreInit(object sender, EventArgs e)
        {
           sf_type = Session["sf_type"].ToString();
           if (sf_type == "3")
           {
               this.MasterPageFile = "~/Master.master";
           }
           else if(sf_type == "2")
           {
               this.MasterPageFile = "~/Master_MGR.master";
           }
 	   else if(sf_type == "1")
           {
               this.MasterPageFile = "~/Master_MR.master";
           }
        }

    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        div_code = div_code.Trim(",".ToCharArray());
       
        if (!Page.IsPostBack)
        {

            ViewState["dsSalesForce"] = null;
            ViewState["dsDoctor"] = null;
            FillSalesForce();
        }
    }


    private void FillSalesForce()
    {
        int tot_rows = 0;
        int tot_cols = 0;

       
        Division dv = new Division();
        dsDivision = dv.getStatePerDivision(div_code);
        if (dsDivision.Tables[0].Rows.Count > 0)
        {
            int i = 0;
            state_cd = string.Empty;
            sState = dsDivision.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
            statecd = sState.Split(',');
            foreach (string st_cd in statecd)
            {
                if (i == 0)
                {
                    state_cd = state_cd + st_cd;
                }
                else
                {
                    if (st_cd.Trim().Length > 0)
                    {
                        state_cd = state_cd + "," + st_cd;
                    }
                }
                i++;
            }

            State st = new State();
            dsSalesForce = st.getState_new(state_cd);

            if (dsSalesForce.Tables[0].Rows.Count > 0)
            {
                tot_rows = dsSalesForce.Tables[0].Rows.Count;
                ViewState["dsSalesForce"] = dsSalesForce;
            }

            CreateDynamicTable(tot_rows, tot_cols);
        }
    }
    private void CreateDynamicTable(int tblRows, int tblCols)
    {

        if (ViewState["dsSalesForce"] != null)
        {
            ViewState["HQ_Det"] = null;


            TableRow tr_header = new TableRow();
            tr_header.BorderStyle = BorderStyle.Solid;
            tr_header.BorderWidth = 1;

            TableCell tc_SNo = new TableCell();
            tc_SNo.BorderStyle = BorderStyle.Solid;
            tc_SNo.BorderColor = System.Drawing.Color.Black;
            tc_SNo.BorderWidth = 1;

            tc_SNo.ForeColor = System.Drawing.Color.Yellow;
            Literal lit_SNo = new Literal();
            lit_SNo.Text = "<center>S.No</center>";

            tc_SNo.Controls.Add(lit_SNo);

            tc_SNo.Style.Add("font-family", "Tahoma");
            tc_SNo.Style.Add("font-size", "10pt");
            tr_header.Cells.Add(tc_SNo);

            tr_header.BackColor = System.Drawing.Color.FromName("#337ab7");

            TableCell tc_FFT = new TableCell();
            tc_FFT.BorderStyle = BorderStyle.Solid;
            tc_FFT.BorderWidth = 1;

            tc_FFT.BorderColor = System.Drawing.Color.Black;

            Literal lit_FFF = new Literal();


            lit_FFF.Text = "<center>State</center>";

            tc_FFT.Controls.Add(lit_FFF);
            tc_FFT.ForeColor = System.Drawing.Color.Yellow;

            tc_FFT.Style.Add("margin-top", "20px");
            tc_FFT.Style.Add("font-family", "Tahoma");
            tc_FFT.Style.Add("font-size", "10pt");

            tr_header.Cells.Add(tc_FFT);

            TableCell tc_FF = new TableCell();
            tc_FF.BorderStyle = BorderStyle.Solid;
            tc_FF.BorderWidth = 1;

            tc_FF.BorderColor = System.Drawing.Color.Black;

            Literal lit_FF = new Literal();
            Territory terr = new Territory();
            // lit_FF.Text = "<center>Territory</center>";
            DataSet dsTerritory = new DataSet();

            lit_FF.Text = "<center>Area</center>";

            tc_FF.Controls.Add(lit_FF);
            tc_FF.ForeColor = System.Drawing.Color.Yellow;

            tc_FF.Style.Add("margin-top", "20px");
            tc_FF.Style.Add("font-family", "Tahoma");
            tc_FF.Style.Add("font-size", "10pt");

            tr_header.Cells.Add(tc_FF);


            TableCell tc_Total = new TableCell();
            tc_Total.BorderStyle = BorderStyle.Solid;
            tc_Total.BorderWidth = 1;
            tc_Total.BorderColor = System.Drawing.Color.Black;

            tc_Total.ForeColor = System.Drawing.Color.Yellow;
            Literal lit_Total = new Literal();
            lit_Total.Text = "<center>Zone</center>";
            tc_Total.Controls.Add(lit_Total);

            tc_Total.Style.Add("font-family", "Tahoma");
            tc_Total.Style.Add("font-size", "10pt");
            tr_header.Cells.Add(tc_Total);


            TableCell tc_Totalcnt = new TableCell();
            tc_Totalcnt.BorderStyle = BorderStyle.Solid;
            tc_Totalcnt.BorderWidth = 1;
            tc_Totalcnt.BorderColor = System.Drawing.Color.Black;

            tc_Totalcnt.ForeColor = System.Drawing.Color.Yellow;
            Literal lit_Totalcnt = new Literal();
            lit_Totalcnt.Text = "<center>Territory</center>";
            tc_Totalcnt.Controls.Add(lit_Totalcnt);

            tc_Totalcnt.Style.Add("font-family", "Tahoma");
            tc_Totalcnt.Style.Add("font-size", "10pt");
            tr_header.Cells.Add(tc_Totalcnt);
            TableCell tc_distributor = new TableCell();
            tc_distributor.BorderStyle = BorderStyle.Solid;
            tc_distributor.BorderWidth = 1;
            tc_distributor.BorderColor = System.Drawing.Color.Black;

            tc_distributor.ForeColor = System.Drawing.Color.Yellow;
            Literal tc_distributorr = new Literal();
            tc_distributorr.Text = "<center>Distributor</center>";
            tc_distributor.Controls.Add(tc_distributorr);

            tc_distributor.Style.Add("font-family", "Tahoma");
            tc_distributor.Style.Add("font-size", "10pt");
            tr_header.Cells.Add(tc_distributor);

            tbl.Rows.Add(tr_header);

            string sURL = string.Empty;
            string sURL1 = string.Empty;
            string sURL2 = string.Empty;   
            string sURL3 = string.Empty;
            int iCount = 0;
            dsSalesForce = (DataSet)ViewState["dsSalesForce"];
            foreach (DataRow drFF in dsSalesForce.Tables[0].Rows)
            {
                TableRow tr_det = new TableRow();
                iCount += 1;
                TableCell tc_det_SNo = new TableCell();
                Literal lit_det_SNo = new Literal();
                lit_det_SNo.Text = "<center>" + iCount.ToString() + "</center>";
                tc_det_SNo.BorderStyle = BorderStyle.Solid;
                tc_det_SNo.BorderWidth = 1;
                tc_det_SNo.Controls.Add(lit_det_SNo);
                tc_det_SNo.Width = 40;
              
                tc_det_SNo.Style.Add("font-family", "Tahoma");
                tc_det_SNo.Style.Add("font-size", "10pt");
                tr_det.Cells.Add(tc_det_SNo);
             


            
                TableCell tc_det_FF = new TableCell();
                Literal lit_det_FF = new Literal();
                lit_det_FF.Text = "&nbsp" + drFF["statename"].ToString();
                tc_det_FF.BorderStyle = BorderStyle.Solid;
                tr_det.BackColor = System.Drawing.Color.FromName("#fcf8e3");
                tr_det.BorderColor = System.Drawing.Color.FromName("#d6e9c6");
                tr_det.HorizontalAlign = HorizontalAlign.Center; ;
                tc_det_FF.Width = 180;
                tc_det_FF.BorderWidth = 1;
                tc_det_FF.Controls.Add(lit_det_FF);
                tc_det_FF.Style.Add("text-align", "left");
                tc_det_FF.Style.Add("font-family", "Tahoma");
                tc_det_FF.Style.Add("color", "Maroon");
                tc_det_FF.Style.Add("font-size", "10pt");
                tr_det.Cells.Add(tc_det_FF);



                DCR dcr = new DCR();
              
            
                TableCell tc_det_tot = new TableCell();
                //Literal lit_det_tot = new Literal();
                HyperLink lit_det_tot1 = new HyperLink();

                State st = new State();
              
                dsdoc = st.get_area_count(drFF["State_code"].ToString(),div_code);

              
                if (dsdoc.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dataRow in dsdoc.Tables[0].Rows)
                    {
                       
                      lit_det_tot1.Text = dataRow["areacount"].ToString();
                      sURL = "Customer.aspx?state_Name=" + drFF["statename"].ToString() + "&state_code=" + drFF["State_code"] + "&catagory=1";
                        lit_det_tot1.Attributes["onclick"] = "javascript:window.open('" + sURL + "' ,'','resizable=yes,toolbar=no,menubar=no,scrollbars=1,status=no,width=500,height=600,left=0,top=0,zoom=50%');";
                       
                       
                        lit_det_tot1.NavigateUrl = "#";
                       
                        tc_det_tot.BorderStyle = BorderStyle.Solid;
                        tc_det_tot.Style.Add("font-family", "Calibri");
                        tc_det_tot.BorderWidth = 1;
                        tc_det_tot.Width = 180;
 tc_det_tot.Style.Add("text-align", "right");
                        tc_det_tot.Controls.Add(lit_det_tot1);
                        tr_det.Cells.Add(tc_det_tot);
                        iPendingCount = "0";
                       

                       


                        TableCell tc_det_cnt = new TableCell();
                        HyperLink lit_det_cnt = new HyperLink();
                        SalesForce sf = new SalesForce();
                        sURL1 = "Customer.aspx?state_Name=" + drFF["statename"].ToString() + "&state_code=" + drFF["State_code"] + "&catagory=2";
                        lit_det_cnt.Attributes["onclick"] = "javascript:window.open('" + sURL1 + "' ,'','resizable=yes,toolbar=no,menubar=no,scrollbars=1,status=no,width=500,height=600,left=0,top=0,zoom=50%');";
                        lit_det_cnt.NavigateUrl = "#";

                        
                        lit_det_cnt.Text = dataRow["zonecount"].ToString();
                       
                        tc_det_cnt.Width = 180;
                        tc_det_cnt.BorderStyle = BorderStyle.Solid;
                        tc_det_cnt.BorderWidth = 1;
                        tc_det_cnt.Controls.Add(lit_det_cnt);
                        tc_det_cnt.Style.Add("text-align", "right");
                        tc_det_cnt.Style.Add("font-family", "Calibri");
                        tc_det_cnt.Style.Add("color", "Black");
                        tc_det_cnt.Style.Add("font-size", "12pt");
                        tr_det.Cells.Add(tc_det_cnt);



                        TableCell tc_distribut = new TableCell();
                        HyperLink lit_distribut = new HyperLink();
                        sURL2 = "Customer.aspx?state_Name=" + drFF["statename"].ToString() + "&state_code=" + drFF["State_code"] + "&catagory=3";
                        lit_distribut.Attributes["onclick"] = "javascript:window.open('" + sURL + "' ,'','resizable=yes,toolbar=no,menubar=no,scrollbars=1,status=no,width=500,height=600,left=0,top=0,zoom=50%');";
                        lit_distribut.NavigateUrl = "#";
                        lit_distribut.Text = dataRow["territory"].ToString();
                         tc_distribut.Width = 180;
                        tc_distribut.BorderStyle = BorderStyle.Solid;
                        tc_distribut.BorderWidth = 1;
                        tc_distribut.Controls.Add(lit_distribut);
                        tc_distribut.Style.Add("text-align", "right");
                        tc_distribut.Style.Add("font-family", "Calibri");
                        tc_distribut.Style.Add("color", "Black");
                        tc_distribut.Style.Add("font-size", "12pt");
                        tr_det.Cells.Add(tc_distribut);

                        TableCell tc_det_FFp = new TableCell();
                        HyperLink lit_det_FFp = new HyperLink();
                        sURL3= "Customer.aspx?state_Name=" + drFF["statename"].ToString() + "&state_code=" + drFF["State_code"] + "&catagory=4";
                        lit_det_FFp.Attributes["onclick"] = "javascript:window.open('" + sURL + "' ,'','resizable=yes,toolbar=no,menubar=no,scrollbars=1,status=no,width=500,height=600,left=0,top=0,zoom=50%');";
                        lit_det_FFp.NavigateUrl = "#";
                        lit_det_FFp.Text =  dataRow["distributor"].ToString();
                        tc_det_FFp.BorderStyle = BorderStyle.Solid;
                        tc_det_FFp.Width = 180;
                        tc_det_FFp.BorderWidth = 1;
                        tc_det_FFp.Controls.Add(lit_det_FFp);
                        tc_det_FFp.Style.Add("text-align", "right");
                        tc_det_FFp.Style.Add("font-family", "Tahoma");
                        tc_det_FFp.Style.Add("color", "Maroon");
                        tc_det_FFp.Style.Add("font-size", "10pt");
                        tr_det.Cells.Add(tc_det_FFp);
                     
                        tbl.Rows.Add(tr_det);


                    }
                }

             

            }

         
        }
    }
}