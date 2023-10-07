using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;

public partial class Delayed_Status : System.Web.UI.Page
{
    DataSet dsListedDR = null;
    DataSet dsDivision = null;
    DataSet dsdiv = new DataSet();
    DataSet dsSalesForce = null;
    string div_code = string.Empty;
    string strMultiDiv = string.Empty;
    string request_doctor = string.Empty;
    string sf_code = string.Empty;
    string sf_type = string.Empty;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int time;
    DataSet dsTP = null;
    protected void Page_Load(object sender, EventArgs e)
    {
      
     //   sf_code = Session["sf_code"].ToString();
        sf_type = Session["sf_type"].ToString();    
        if (sf_type == "3")
        {
            div_code = Session["division_code"].ToString();
        }
        else
        {
            div_code = Session["div_code"].ToString();
        }
      
        if (!Page.IsPostBack)
        {
            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);
            TourPlan tp = new TourPlan();
            dsTP = tp.Get_TP_Edit_Year_New(div_code);
            if (dsTP.Tables[0].Rows.Count > 0)
            {
                for (int k = Convert.ToInt16(dsTP.Tables[0].Rows[0]["Year"]); k <= DateTime.Now.Year + 1; k++)
                {
                    ddlYear.Items.Add(k.ToString());
                    ddlYear.SelectedValue = DateTime.Now.Year.ToString();
                    ddlMonth.SelectedValue = DateTime.Now.Month.ToString();
                }
            }
            if (Session["sf_type"].ToString() == "" || Session["sf_type"].ToString() == null || Session["sf_type"].ToString() == "3")
            {
                Filldiv();
                ddlDivision.SelectedIndex = 1;
                ddlFieldForce.SelectedIndex = 1;
                btnGo.Focus();
                FillMRManagers();
                lblHead.Text = "Delayed Status for the month of " + getMonthName(DateTime.Now.Month) + "  " + DateTime.Now.Year.ToString() + " ";
            }
        }
        FillColor();
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
    private string getMonthName(int iMonth)
    {
        string sReturn = string.Empty;

        if (iMonth == 1)
        {
            sReturn = "January";
        }
        else if (iMonth == 2)
        {
            sReturn = "February";
        }
        else if (iMonth == 3)
        {
            sReturn = "March";
        }
        else if (iMonth == 4)
        {
            sReturn = "April";
        }
        else if (iMonth == 5)
        {
            sReturn = "May";
        }
        else if (iMonth == 6)
        {
            sReturn = "June";
        }
        else if (iMonth == 7)
        {
            sReturn = "July";
        }
        else if (iMonth == 8)
        {
            sReturn = "August";
        }
        else if (iMonth == 9)
        {
            sReturn = "September";
        }
        else if (iMonth == 10)
        {
            sReturn = "October";
        }
        else if (iMonth == 11)
        {
            sReturn = "November";
        }
        else if (iMonth == 12)
        {
            sReturn = "December";
        }
        return sReturn;
    }
    private void FillMRManagers()
    {
        SalesForce sf = new SalesForce();
        dsSalesForce = sf.UserList_Hierarchy(ddlDivision.SelectedValue.ToString(), "admin");
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

        FillColor();
    }

    protected void ddlDivision_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillMRManagers();
    }
    private void FillColor()
    {
        int j = 0;

        foreach (ListItem ColorItems in ddlSF.Items)
        {
            string bcolor = "#" + ColorItems.Text;
            ddlFieldForce.Items[j].Attributes.Add("style", "background-color:" + bcolor);
            j = j + 1;

        }
    }

    protected void btnGo_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        FillUserList();
    }

    private void FillUserList()
    {
        SalesForce sf = new SalesForce();
        DataSet dsSalesForce = new DataSet();

        dsSalesForce = sf.UserList_Self_Vacant(ddlDivision.SelectedValue, ddlFieldForce.SelectedValue, "1");
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
           
                TableRow tr_header = new TableRow();
                tr_header.BorderStyle = BorderStyle.Solid;
                tr_header.BorderWidth = 1;
                TableCell tc_SNo = new TableCell();
                tc_SNo.BorderStyle = BorderStyle.Solid;
                tc_SNo.BorderWidth = 1;
                tc_SNo.Width = 50;
                tc_SNo.RowSpan = 1;
                Literal lit_SNo = new Literal();
                lit_SNo.Text = "S.No";
                tc_SNo.Controls.Add(lit_SNo);
                tc_SNo.BackColor = System.Drawing.ColorTranslator.FromHtml("#0097AC");
                tc_SNo.Style.Add("color", "white");
                tc_SNo.Style.Add("font-weight", "bold");
                tc_SNo.Style.Add("border-color", "Black");
                tc_SNo.HorizontalAlign = HorizontalAlign.Center;
                tc_SNo.Style.Add("font-family", "Calibri");
                tr_header.Cells.Add(tc_SNo);
                //tr_header.BackColor = System.Drawing.Color.FromName("#A6A6D2");

                TableCell tc_DR_Code = new TableCell();
                tc_DR_Code.BorderStyle = BorderStyle.Solid;
                tc_DR_Code.BorderWidth = 1;
                tc_DR_Code.Width = 40;
                tc_DR_Code.RowSpan = 1;
                Literal lit_DR_Code = new Literal();
                lit_DR_Code.Text = "<center>SF Code</center>";
                tc_DR_Code.Controls.Add(lit_DR_Code);
                tc_DR_Code.BackColor = System.Drawing.ColorTranslator.FromHtml("#0097AC");
                tc_DR_Code.Style.Add("color", "white");
                tc_DR_Code.Style.Add("font-weight", "bold");
                tc_DR_Code.Style.Add("font-family", "Calibri");
                tc_DR_Code.Style.Add("border-color", "Black");
                tc_DR_Code.Visible = false;
                tr_header.Cells.Add(tc_DR_Code);

                TableCell tc_DR_Name = new TableCell();
                tc_DR_Name.BorderStyle = BorderStyle.Solid;
                tc_DR_Name.BorderWidth = 1;
                tc_DR_Name.Width = 200;
                tc_DR_Name.RowSpan = 1;
                Literal lit_DR_Name = new Literal();
                lit_DR_Name.Text = "<center>Field Force</center>";
                tc_DR_Name.Controls.Add(lit_DR_Name);
                tc_DR_Name.BackColor = System.Drawing.ColorTranslator.FromHtml("#0097AC");
                tc_DR_Name.Style.Add("color", "white");
                tc_DR_Name.Style.Add("font-weight", "bold");
                tc_DR_Name.Style.Add("font-family", "Calibri");
                tc_DR_Name.Style.Add("border-color", "Black");
                tr_header.Cells.Add(tc_DR_Name);

                TableCell tc_DR_HQ = new TableCell();
                tc_DR_HQ.BorderStyle = BorderStyle.Solid;
                tc_DR_HQ.BorderWidth = 1;
                tc_DR_HQ.Width = 50;
                tc_DR_HQ.RowSpan = 1;
                Literal lit_DR_HQ = new Literal();
                lit_DR_HQ.Text = "<center>HQ</center>";
                tc_DR_HQ.Controls.Add(lit_DR_HQ);
                tc_DR_HQ.BackColor = System.Drawing.ColorTranslator.FromHtml("#0097AC");
                tc_DR_HQ.Style.Add("color", "white");
                tc_DR_HQ.Style.Add("font-weight", "bold");
                tc_DR_HQ.Style.Add("font-family", "Calibri");
                tc_DR_HQ.Style.Add("border-color", "Black");
                tr_header.Cells.Add(tc_DR_HQ);

                TableCell tc_DR_Des = new TableCell();
                tc_DR_Des.BorderStyle = BorderStyle.Solid;
                tc_DR_Des.BorderWidth = 1;
                tc_DR_Des.Width = 50;
                tc_DR_Des.RowSpan = 1;
                Literal lit_DR_Des = new Literal();
                lit_DR_Des.Text = "<center>Designation</center>";
                tc_DR_Des.HorizontalAlign = HorizontalAlign.Center;
                tc_DR_Des.Controls.Add(lit_DR_Des);
                tc_DR_Des.BackColor = System.Drawing.ColorTranslator.FromHtml("#0097AC");
                tc_DR_Des.Style.Add("color", "white");
                tc_DR_Des.Style.Add("font-weight", "bold");
                tc_DR_Des.Style.Add("font-family", "Calibri");
                tc_DR_Des.Style.Add("border-color", "Black");
                tr_header.Cells.Add(tc_DR_Des);

                TableCell tc_DR_Date = new TableCell();
                tc_DR_Date.BorderStyle = BorderStyle.Solid;
                tc_DR_Date.BorderWidth = 1;
                tc_DR_Date.Width = 50;
                tc_DR_Date.RowSpan = 1;
                Literal lit_DR_Date = new Literal();
                lit_DR_Date.Text = "<center>Delayed_Date</center>";
                tc_DR_Date.HorizontalAlign = HorizontalAlign.Center;
                tc_DR_Date.Controls.Add(lit_DR_Date);
                tc_DR_Date.BackColor = System.Drawing.ColorTranslator.FromHtml("#0097AC");
                tc_DR_Date.Style.Add("color", "white");
                tc_DR_Date.Style.Add("font-weight", "bold");
                tc_DR_Date.Style.Add("font-family", "Calibri");
                tc_DR_Date.Style.Add("border-color", "Black");
                tr_header.Cells.Add(tc_DR_Date);


                tbl.Rows.Add(tr_header);
            
        }

        int iCount = 0;
        int iCnt = 0;

        foreach (DataRow drFF in dsSalesForce.Tables[0].Rows)
        {
            ListedDR lstDR = new ListedDR();
            iCnt = lstDR.RecordCount(drFF["sf_code"].ToString());

            TableRow tr_det = new TableRow();
            tr_det.Attributes.Add("style", "background-color:" + "#" + drFF["des_color"].ToString());
            iCount += 1;
            TableCell tc_det_SNo = new TableCell();
            Literal lit_det_SNo = new Literal();
            lit_det_SNo.Text = "<center>" + iCount.ToString() + "</center>";
            tc_det_SNo.BorderStyle = BorderStyle.Solid;
            tc_det_SNo.BorderWidth = 1;
            tc_det_SNo.Width = 50;
            tc_det_SNo.Controls.Add(lit_det_SNo);
            tr_det.Cells.Add(tc_det_SNo);

            TableCell tc_det_doc_code = new TableCell();
            Literal lit_det_doc_code = new Literal();
            lit_det_doc_code.Text = "&nbsp;" + drFF["sf_code"].ToString();
            tc_det_doc_code.BorderStyle = BorderStyle.Solid;
            tc_det_doc_code.BorderWidth = 1;
            tc_det_doc_code.Controls.Add(lit_det_doc_code);
            tc_det_doc_code.Visible = false;
            tr_det.Cells.Add(tc_det_doc_code);

            TableCell tc_det_doc_name = new TableCell();
            HyperLink lit_det_doc_name = new HyperLink();
            lit_det_doc_name.Text = "&nbsp;" + drFF["SF_Name"].ToString();
            tc_det_doc_name.HorizontalAlign = HorizontalAlign.Left;
            tc_det_doc_name.BorderStyle = BorderStyle.Solid;
            tc_det_doc_name.Style.Add("font-family", "Calibri");
            tc_det_doc_name.BorderWidth = 1;
            tc_det_doc_name.Width = 200;
            tc_det_doc_name.Controls.Add(lit_det_doc_name);
            tr_det.Cells.Add(tc_det_doc_name);           

            TableCell tc_det_Designation = new TableCell();
            Literal lit_det_Designation = new Literal();
            lit_det_Designation.Text = "&nbsp;" + drFF["Designation_Short_Name"].ToString();
            tc_det_Designation.BorderStyle = BorderStyle.Solid;
            tc_det_Designation.BorderWidth = 1;
            tc_det_Designation.Style.Add("font-family", "Calibri");
            tc_det_Designation.Style.Add("font-size", "10pt");
            tc_det_Designation.Style.Add("text-align", "left");
            tc_det_Designation.Controls.Add(lit_det_Designation);
            tr_det.Cells.Add(tc_det_Designation);
           

            TableCell tc_det_HQ = new TableCell();
            Literal lit_det_HQ = new Literal();
            lit_det_HQ.Text = "&nbsp;" + drFF["sf_hq"].ToString();
            tc_det_HQ.BorderStyle = BorderStyle.Solid;
            tc_det_HQ.BorderWidth = 1;
            tc_det_HQ.Style.Add("font-family", "Calibri");
            tc_det_HQ.Style.Add("font-size", "10pt");
            tc_det_HQ.Style.Add("text-align", "left");
            tc_det_HQ.Controls.Add(lit_det_HQ);
            tr_det.Cells.Add(tc_det_HQ);
           

            DataSet dsDoc = new DataSet();
            DCR dcr = new DCR();
            string tot_DcrPendingDate = string.Empty;
            string iPendingDate = string.Empty;

            dsDoc = dcr.get_delayed_Status(drFF["sf_code"].ToString(), ddlMonth.SelectedValue, ddlYear.SelectedValue);

            TableCell tc_det_delayed_Status = new TableCell();
            Literal lit_det_tc_det_delayed_Status = new Literal();
            if (dsDoc.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < dsDoc.Tables[0].Rows.Count; i++)
                {
                    tot_DcrPendingDate = dsDoc.Tables[0].Rows[i].ItemArray.GetValue(0).ToString();                    
                    iPendingDate += Convert.ToInt16(tot_DcrPendingDate.Substring(0, 2)) + ", ";
                    lit_det_tc_det_delayed_Status.Text = "&nbsp;" + iPendingDate;

                }
            }
            else
            {
                lit_det_tc_det_delayed_Status.Text = "";
            }

            tc_det_delayed_Status.BorderStyle = BorderStyle.Solid;
            tc_det_delayed_Status.BorderWidth = 1;
            tc_det_delayed_Status.Style.Add("font-family", "Calibri");
            tc_det_delayed_Status.HorizontalAlign = HorizontalAlign.Center;
            tc_det_delayed_Status.Controls.Add(lit_det_tc_det_delayed_Status);
            //tc_det_sf_HQ.Visible = false;
            tr_det.Cells.Add(tc_det_delayed_Status);

            tbl.Rows.Add(tr_det);
        }


    }
}