using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Xml;
using System.Drawing;
using System.Configuration;
using Bus_EReport;

public partial class MasterFiles_MGR_DCR_Bulk_Approval : System.Web.UI.Page
{
    DataSet dsDCR = null;
    DataSet dsdate = null;
    string sf_code = string.Empty;
    string mon = string.Empty;
    string year = string.Empty;
     string sFile = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        sf_code = Request.QueryString["sfcode"].ToString();
        mon = Request.QueryString["mon"].ToString();
        year = Request.QueryString["year"].ToString();
        
        if (!Page.IsPostBack)
        {
            SalesForce sf = new SalesForce();
            DataSet dssf = sf.getSfName(sf_code);
            if (dssf.Tables[0].Rows.Count > 0)
            {
                lblText.Text = lblText.Text + Convert.ToString(dssf.Tables[0].Rows[0].ItemArray.GetValue(0).ToString()) + ":-" + getMonthName(mon) + " - " + year;
            }

            FillDCR();
     
        }


    }
    private string getMonthName(string sMonth)
    {
        string sReturn = string.Empty;

        if (sMonth == "1")
        {
            sReturn = "January";
        }
        else if (sMonth == "2")
        {
            sReturn = "February";
        }
        else if (sMonth == "2")
        {
            sReturn = "February";
        }
        else if (sMonth == "3")
        {
            sReturn = "March";
        }
        else if (sMonth == "4")
        {
            sReturn = "April";
        }
        else if (sMonth == "5")
        {
            sReturn = "May";
        }
        else if (sMonth == "6")
        {
            sReturn = "June";
        }
        else if (sMonth == "7")
        {
            sReturn = "July";
        }
        else if (sMonth == "8")
        {
            sReturn = "August";
        }
        else if (sMonth == "9")
        {
            sReturn = "September";
        }
        else if (sMonth == "10")
        {
            sReturn = "October";
        }
        else if (sMonth == "11")
        {
            sReturn = "November";
        }
        else if (sMonth == "12")
        {
            sReturn = "December";
        }

        return sReturn;
    }

  
    private void FillDCR()
    {
        grdDCR.DataSource = null;
        grdDCR.DataBind();

        DCR dr = new DCR();


        dsDCR = dr.get_DCR_Pending_Approval_All(sf_code, mon, year);
        if (dsDCR.Tables[0].Rows.Count > 0)
        {
            grdDCR.Visible = true;
            grdDCR.DataSource = dsDCR;
            grdDCR.DataBind();
        }
        else
        {
            grdDCR.DataSource = dsDCR;
            grdDCR.DataBind();
        }
    }
    protected void btnApprove_Click(object sender, EventArgs e)
    {
        int iReturn = -1;
        foreach (GridViewRow gridRow in grdDCR.Rows)
        {
            CheckBox chkDR = (CheckBox)gridRow.Cells[0].FindControl("chkAppDCR");
            bool bCheck = chkDR.Checked;
            Label lbltrans_slno = (Label)gridRow.Cells[2].FindControl("lbltrans_slno");
            string trans_slno = lbltrans_slno.Text.ToString();
            int slno = Convert.ToInt32(trans_slno);
            if ((bCheck == true))
            {
                DCR dsdcr = new DCR();

                iReturn = dsdcr.Create_DCRHead_Trans(sf_code, slno);

                dsdate = dsdcr.getDCR_ActivityDate(slno);
                if (dsdate.Tables[0].Rows.Count > 0)
                {
                    DateTime dtDCR = Convert.ToDateTime(dsdate.Tables[0].Rows[0].ItemArray.GetValue(0).ToString());
                    ViewState["curdate"] = dtDCR.Day.ToString() + dtDCR.Month.ToString() + dtDCR.Year.ToString();
                }
            }
            else
            {
                //menu1.Status = "Enter all the values!!";
            }
        }

        if (iReturn != -1)
        {
            removexml();
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Approved Successfully');</script>");
            FillDCR();
        }
    }
    private void removexml()
    {
        //Delete the Listed Doctor XML file
        //string FilePath = Server.MapPath("DailCalls.xml");
        //sFile = sf_code + sCurDate + "ListedDR.xml";
  
        string sFileHeader = sf_code + "_" + ViewState["curdate"].ToString() + "_Header.xml";
        string strHead = AppDomain.CurrentDomain.BaseDirectory + "MasterFiles\\MR\\DCR\\" + sFileHeader;
        if (File.Exists(strHead))
            File.Delete(strHead);
        //string headerFilePath = Server.MapPath(sFileHeader);
        //if (File.Exists(headerFilePath))
        //    File.Delete(headerFilePath);

        sFile = sf_code + "_" + ViewState["curdate"].ToString() + "_ListedDR.xml";
        string strLdr = AppDomain.CurrentDomain.BaseDirectory + "MasterFiles\\MR\\DCR\\" + sFile;
        if (File.Exists(strLdr))
            File.Delete(strLdr);
        //string FilePath = Server.MapPath(sFile);
        //if (File.Exists(FilePath))
        //    File.Delete(FilePath);

        //Delete the Chemists XML file
        //FilePath = Server.MapPath("Chem_DCR.xml");
        string sChemFile = sf_code + "_" + ViewState["curdate"].ToString() + "_Chem.xml";
        string strChem = AppDomain.CurrentDomain.BaseDirectory + "MasterFiles\\MR\\DCR\\" + sChemFile;
        if (File.Exists(strChem))
            File.Delete(strChem);
        //string chemFilePath = Server.MapPath(sChemFile);
        //if (File.Exists(chemFilePath))
        //    File.Delete(chemFilePath);

        //Delete the Stockiest XML file
        string sStockFile = sf_code + "_" + ViewState["curdate"].ToString() + "_Stockiest.xml";
        //string stockFilePath = Server.MapPath(sStockFile);
        //if (File.Exists(stockFilePath))
        //    File.Delete(stockFilePath);
        string strStk = AppDomain.CurrentDomain.BaseDirectory + "MasterFiles\\MR\\DCR\\" + sStockFile;
        if (File.Exists(strStk))
            File.Delete(strStk);

        //Delete the Hospital XML file
        string sHosFile = sf_code + "_" + ViewState["curdate"].ToString() + "_Hospital.xml";
        //string hosFilePath = Server.MapPath(sHosFile);
        //if (File.Exists(hosFilePath))
        //    File.Delete(hosFilePath);
        string strHos = AppDomain.CurrentDomain.BaseDirectory + "MasterFiles\\MR\\DCR\\" + sHosFile;
        if (File.Exists(strHos))
            File.Delete(strHos);

        //Delete the Un-:isted XML file                
        string sUnLstFile = sf_code + "_" + ViewState["curdate"].ToString() + "UnLstDR.xml";
        //string unlstFilePath = Server.MapPath(sUnLstFile);
        //if (File.Exists(unlstFilePath))
        //    File.Delete(unlstFilePath);
        string strUnlst = AppDomain.CurrentDomain.BaseDirectory + "MasterFiles\\MR\\DCR\\" + sUnLstFile;
        if (File.Exists(strUnlst))
            File.Delete(strUnlst);

        string sFileHeadermgr = sf_code + "_" + ViewState["curdate"].ToString() + "_Header.xml";
        string headerFilePath = AppDomain.CurrentDomain.BaseDirectory + "MasterFiles\\MGR\\DCR\\" + sFileHeadermgr;
        if (File.Exists(headerFilePath))
            File.Delete(headerFilePath);

        sFile = sf_code + "_" + ViewState["curdate"].ToString() + "_WorkArea.xml";
        string FilePath = AppDomain.CurrentDomain.BaseDirectory + "MasterFiles\\MGR\\DCR\\" + sFile;
        if (File.Exists(FilePath))
            File.Delete(FilePath);
    }
    protected void grdDCR_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        
    }
    //protected void chkapp_CheckedChanged(object sender, EventArgs e)
    //{
    //    CheckBox chkapp = (CheckBox)sender;
    //    GridViewRow gridRow = (GridViewRow)chkapp.Parent.Parent;
    //    CheckBox chkrej = (CheckBox)gridRow.Cells[1].FindControl("chkRjtDCR");
    //    TextBox txtReason = (TextBox)gridRow.Cells[12].FindControl("txtReason");
    //    if (chkapp.Checked == true)
    //    {
    //        chkrej.Checked = false;
    //        chkrej.Enabled = false;
    //        txtReason.Enabled = false;
    //    }
    //    if (chkapp.Checked == false)
    //    {
    //        txtReason.Enabled = true;
    //        chkrej.Enabled = true;
    //    }
    //}
    //protected void chkrej_CheckedChanged(object sender, EventArgs e)
    //{
    //    CheckBox chkrej = (CheckBox)sender;
    //    GridViewRow gridRow = (GridViewRow)chkrej.Parent.Parent;
    //    CheckBox chkapp = (CheckBox)gridRow.Cells[0].FindControl("chkAppDCR");
    //    TextBox txtReason = (TextBox)gridRow.Cells[12].FindControl("txtReason");
       
    //    if (chkrej.Checked == true)
    //    {
    //        txtReason.Enabled = true;
    //        chkapp.Enabled = false;
    //        chkapp.Checked = false;
    //    }
    //    if (chkrej.Checked == false)
    //    {
    //        chkapp.Enabled = true;
    //        txtReason.Enabled = true;
    //    }
    //}
    protected void btnReject_Click(object sender, EventArgs e)
    {
        int iReturn = -1;
        foreach (GridViewRow gridRow in grdDCR.Rows)
        {
            CheckBox chkRjtDCR = (CheckBox)gridRow.Cells[0].FindControl("chkRjtDCR");
            bool bCheck = chkRjtDCR.Checked;
            Label lbltrans_slno = (Label)gridRow.Cells[2].FindControl("lbltrans_slno");
            string trans_slno = lbltrans_slno.Text.ToString();
            TextBox txtReason = (TextBox)gridRow.Cells[12].FindControl("txtReason");
            int slno = Convert.ToInt32(trans_slno);
            if ((bCheck == true))
            {
                DCR dsdcr = new DCR();
                iReturn = dsdcr.Reject_DCR(sf_code, slno, txtReason.Text);
                
            }
            else
            {
                //menu1.Status = "Enter all the values!!";
            }
        }

        if (iReturn != -1)
        {
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Rejected Successfully');</script>");
            FillDCR();
        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        if (Session["sf_type"].ToString() == "" || Session["sf_type"].ToString() == null || Session["sf_type"].ToString() == "3")
        {
            Response.Redirect("~/BasicMaster.aspx");
        }
        else
        {
            Response.Redirect("~/MasterFiles/MGR/MGR_Index.aspx");
        }
    }
}