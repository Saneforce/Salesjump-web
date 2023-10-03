using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bus_EReport;
using System.Data;
using System.Xml;
using System.Drawing;
using System.Configuration;

public partial class MasterFiles_MGR_DCR_DCR_Approve : System.Web.UI.Page
{
    #region "Declaration"
    string mgr_sfCode = string.Empty;
    DateTime dtDCR;
    DataSet dsDCR;
    string sf_code = string.Empty;
    string trans_slno = string.Empty;
    string sCurDate = string.Empty;
    string sFile = string.Empty;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int time;
    int sslno;
    int iret = 0;
    string sQryStr = string.Empty;
    string sfcode = string.Empty;
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        mgr_sfCode = Session["sf_code"].ToString();
        sf_code = Request.QueryString["sfcode"].ToString();
        sCurDate = Request.QueryString["Activity_Date"].ToString();
        trans_slno = Request.QueryString["Trans_Slno"].ToString();
        lblHeader.Text = sCurDate + " - " + dtDCR.DayOfWeek.ToString();
        dtDCR = Convert.ToDateTime(sCurDate);
        ViewState["curdate"] = dtDCR.Day.ToString() + dtDCR.Month.ToString() + dtDCR.Year.ToString();

       
        FillDoc();
        Preview_Chem();
        Preview_Stk();

        FillUnlstDoc();

        Preview_Hos();
       
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
    private void FillDoc()
    {

        DataSet ds = new DataSet();
        //DataTable dt = new DataTable();

        sFile = sf_code + "_" + ViewState["curdate"].ToString() + "_ListedDR.xml";

        string FilePath = Server.MapPath(sFile);
        if (!File.Exists(FilePath))
        {
            //Start writer
            XmlTextWriter dr_writer = new XmlTextWriter(Server.MapPath(sFile), System.Text.Encoding.UTF8);

            //Start XM DOcument
            dr_writer.WriteStartDocument(true);
            dr_writer.Formatting = Formatting.Indented;
            dr_writer.Indentation = 2;

            //ROOT Element
            dr_writer.WriteStartElement("DCR");
            dr_writer.WriteEndElement();
            //End XML Document
            dr_writer.WriteEndDocument();
            //Close writer
            dr_writer.Close();
        }

        ds.ReadXml(Server.MapPath(sFile));
        if (ds != null && ds.HasChanges())
        {
            if (ds.Tables[0].Rows.Count > 0)
            {

                TableRow tr_header = new TableRow();
                tr_header.BorderStyle = BorderStyle.Solid;
                tr_header.BorderWidth = 1;

                TableCell tc_Ses = new TableCell();
                tc_Ses.BorderStyle = BorderStyle.Solid;
                tc_Ses.BorderWidth = 1;
                tc_Ses.Width = 30;
                Literal lit_Ses = new Literal();
                lit_Ses.Text = "Ses";
                tc_Ses.Controls.Add(lit_Ses);
                tr_header.Cells.Add(tc_Ses);
                tr_header.BackColor = System.Drawing.Color.Pink;

                TableCell tc_Time = new TableCell();
                tc_Time.BorderStyle = BorderStyle.Solid;
                tc_Time.BorderWidth = 1;
                tc_Time.Width = 40;
                Literal lit_Time = new Literal();
                lit_Time.Text = "<center>Time</center>";
                tc_Time.Controls.Add(lit_Time);
                tr_header.Cells.Add(tc_Time);

                TableCell tc_DR_Name = new TableCell();
                tc_DR_Name.BorderStyle = BorderStyle.Solid;
                tc_DR_Name.BorderWidth = 1;
                tc_DR_Name.Width = 300;
                Literal lit_DR_Name = new Literal();
                lit_DR_Name.Text = "<center>Listed Doctor Name</center>";
                tc_DR_Name.Controls.Add(lit_DR_Name);
                tr_header.Cells.Add(tc_DR_Name);

                TableCell tc_ww = new TableCell();
                tc_ww.BorderStyle = BorderStyle.Solid;
                tc_ww.BorderWidth = 1;
                tc_ww.Width = 300;
                Literal lit_ww = new Literal();
                lit_ww.Text = "<center>Worked With</center>";
                tc_ww.Controls.Add(lit_ww);
                tr_header.Cells.Add(tc_ww);

                TableCell tc_prod = new TableCell();
                tc_prod.BorderStyle = BorderStyle.Solid;
                tc_prod.BorderWidth = 1;
                tc_prod.Width = 420;
                tc_prod.ColumnSpan = 6;
                Literal lit_prod = new Literal();
                lit_prod.Text = "<center>Product Promoted / Sampled</center>";
                tc_prod.Controls.Add(lit_prod);
                tr_header.Cells.Add(tc_prod);

                TableCell tc_Gift = new TableCell();
                tc_Gift.BorderStyle = BorderStyle.Solid;
                tc_Gift.BorderWidth = 1;
                tc_Gift.Width = 200;
                Literal lit_Gift = new Literal();
                lit_Gift.Text = "<center>Gift</center>";
                tc_Gift.Controls.Add(lit_Gift);
                tr_header.Cells.Add(tc_Gift);

                tbl.Rows.Add(tr_header);

                //Details Section
                foreach (DataRow drFF in ds.Tables[0].Rows)
                {
                    TableRow tr_det = new TableRow();
                    TableCell tc_det_Ses = new TableCell();
                    Literal lit_det_Ses = new Literal();
                    lit_det_Ses.Text = "&nbsp;" + drFF["session"].ToString();
                    tc_det_Ses.BorderStyle = BorderStyle.Solid;
                    tc_det_Ses.BorderWidth = 1;
                    tc_det_Ses.Controls.Add(lit_det_Ses);
                    tr_det.Cells.Add(tc_det_Ses);

                    TableCell tc_det_time = new TableCell();
                    Literal lit_det_time = new Literal();
                    lit_det_time.Text = "&nbsp;" + drFF["time"].ToString();
                    tc_det_time.BorderStyle = BorderStyle.Solid;
                    tc_det_time.BorderWidth = 1;
                    tc_det_time.Controls.Add(lit_det_time);
                    tr_det.Cells.Add(tc_det_time);

                    TableCell tc_det_doc_name = new TableCell();
                    Literal lit_det_doc_name = new Literal();
                    lit_det_doc_name.Text = "&nbsp;" + drFF["drcode"].ToString();
                    tc_det_doc_name.BorderStyle = BorderStyle.Solid;
                    tc_det_doc_name.BorderWidth = 1;
                    tc_det_doc_name.Controls.Add(lit_det_doc_name);
                    tr_det.Cells.Add(tc_det_doc_name);

                    TableCell tc_det_ww = new TableCell();
                    Literal lit_det_ww = new Literal();
                    lit_det_ww.Text = "&nbsp;" + drFF["workwith"].ToString();
                    tc_det_ww.BorderStyle = BorderStyle.Solid;
                    tc_det_ww.BorderWidth = 1;
                    tc_det_ww.Controls.Add(lit_det_ww);
                    tr_det.Cells.Add(tc_det_ww);

                    TableCell tc_det_prod1 = new TableCell();
                    Literal lit_det_prod1 = new Literal();
                    lit_det_prod1.Text = "&nbsp;" + drFF["prod1"].ToString().Replace("~", "").Trim();
                    tc_det_prod1.BorderStyle = BorderStyle.Solid;
                    tc_det_prod1.BorderWidth = 1;
                    tc_det_prod1.Width = 120;
                    tc_det_prod1.Controls.Add(lit_det_prod1);
                    tc_det_prod1.HorizontalAlign = HorizontalAlign.Center;
                    tr_det.Cells.Add(tc_det_prod1);

                    TableCell tc_det_qty1 = new TableCell();
                    Literal lit_det_qty1 = new Literal();
                    lit_det_qty1.Text = "&nbsp;" + drFF["qty1"].ToString();
                    tc_det_qty1.BorderStyle = BorderStyle.Solid;
                    tc_det_qty1.BorderWidth = 1;
                    tc_det_qty1.Width = 20;
                    tc_det_qty1.Controls.Add(lit_det_qty1);
                    tc_det_qty1.HorizontalAlign = HorizontalAlign.Center;
                    tr_det.Cells.Add(tc_det_qty1);

                    TableCell tc_det_prod2 = new TableCell();
                    Literal lit_det_prod2 = new Literal();
                    lit_det_prod2.Text = "&nbsp;" + drFF["prod2"].ToString().Replace("~", "").Trim();
                    tc_det_prod2.BorderStyle = BorderStyle.Solid;
                    tc_det_prod2.BorderWidth = 1;
                    tc_det_prod2.Width = 120;
                    tc_det_prod2.Controls.Add(lit_det_prod2);
                    tc_det_prod2.HorizontalAlign = HorizontalAlign.Center;
                    tr_det.Cells.Add(tc_det_prod2);

                    TableCell tc_det_qty2 = new TableCell();
                    Literal lit_det_qty2 = new Literal();
                    lit_det_qty2.Text = "&nbsp;" + drFF["qty2"].ToString();
                    tc_det_qty2.BorderStyle = BorderStyle.Solid;
                    tc_det_qty2.BorderWidth = 1;
                    tc_det_qty2.Width = 20;
                    tc_det_qty2.Controls.Add(lit_det_qty2);
                    tc_det_qty2.HorizontalAlign = HorizontalAlign.Center;
                    tr_det.Cells.Add(tc_det_qty2);

                    TableCell tc_det_prod3 = new TableCell();
                    Literal lit_det_prod3 = new Literal();
                    lit_det_prod3.Text = "&nbsp;" + drFF["prod3"].ToString().Replace("~", "").Trim();
                    tc_det_prod3.BorderStyle = BorderStyle.Solid;
                    tc_det_prod3.BorderWidth = 1;
                    tc_det_prod3.Width = 120;
                    tc_det_prod3.Controls.Add(lit_det_prod3);
                    tc_det_prod3.HorizontalAlign = HorizontalAlign.Center;
                    tr_det.Cells.Add(tc_det_prod3);

                    TableCell tc_det_qty3 = new TableCell();
                    Literal lit_det_qty3 = new Literal();
                    lit_det_qty3.Text = "&nbsp;" + drFF["qty3"].ToString();
                    tc_det_qty3.BorderStyle = BorderStyle.Solid;
                    tc_det_qty3.BorderWidth = 1;
                    tc_det_qty3.Width = 20;
                    tc_det_qty3.Controls.Add(lit_det_qty3);
                    tc_det_qty3.HorizontalAlign = HorizontalAlign.Center;
                    tr_det.Cells.Add(tc_det_qty3);

                    TableCell tc_det_gift = new TableCell();
                    Literal lit_det_gift = new Literal();
                    lit_det_gift.Text = "&nbsp;" + drFF["gift"].ToString().Replace("~", "").Trim() + " ( " + drFF["gqty"].ToString() + " ) ";
                    tc_det_gift.BorderStyle = BorderStyle.Solid;
                    tc_det_gift.BorderWidth = 1;
                    tc_det_gift.Controls.Add(lit_det_gift);
                    tc_det_gift.HorizontalAlign = HorizontalAlign.Center;
                    tr_det.Cells.Add(tc_det_gift);

                    tbl.Rows.Add(tr_det);
                }
            }
            else
            {
                lblld.Visible = false;
            }

        }
        else
        {
            lblld.Visible = false;
        }

    }
    private void Preview_Chem()
    {
        DataSet ds = new DataSet();

        sFile = sf_code + "_" + ViewState["curdate"].ToString() + "_Chem.xml";

        string FilePath = Server.MapPath(sFile);
        if (!File.Exists(FilePath))
        {
            //Start writer
            XmlTextWriter dr_writer = new XmlTextWriter(Server.MapPath(sFile), System.Text.Encoding.UTF8);

            //Start XM DOcument
            dr_writer.WriteStartDocument(true);
            dr_writer.Formatting = Formatting.Indented;
            dr_writer.Indentation = 2;

            //ROOT Element
            dr_writer.WriteStartElement("DCR");
            dr_writer.WriteEndElement();
            //End XML Document
            dr_writer.WriteEndDocument();
            //Close writer
            dr_writer.Close();
        }

        ds.ReadXml(Server.MapPath(sFile));

        if (ds != null && ds.HasChanges())
        {
            if (ds.Tables[0].Rows.Count > 0)
            {

                TableRow tr_header = new TableRow();
                tr_header.BorderStyle = BorderStyle.Solid;
                tr_header.BorderWidth = 1;

                TableCell tc_Ses = new TableCell();
                tc_Ses.BorderStyle = BorderStyle.Solid;
                tc_Ses.BorderWidth = 1;
                tc_Ses.Width = 300;
                Literal lit_Ses = new Literal();
                lit_Ses.Text = "Chemists Name";
                tc_Ses.Controls.Add(lit_Ses);
                tr_header.Cells.Add(tc_Ses);
                tr_header.BackColor = System.Drawing.Color.Pink;

                TableCell tc_ww = new TableCell();
                tc_ww.BorderStyle = BorderStyle.Solid;
                tc_ww.BorderWidth = 1;
                tc_ww.Width = 300;
                Literal lit_ww = new Literal();
                lit_ww.Text = "<center>Worked With</center>";
                tc_ww.Controls.Add(lit_ww);
                tr_header.Cells.Add(tc_ww);

                TableCell tc_prod = new TableCell();
                tc_prod.BorderStyle = BorderStyle.Solid;
                tc_prod.BorderWidth = 1;
                tc_prod.Width = 420;
                Literal lit_prod = new Literal();
                lit_prod.Text = "<center>POB</center>";
                tc_prod.Controls.Add(lit_prod);
                tr_header.Cells.Add(tc_prod);

                tblChem.Rows.Add(tr_header);

                //Details Section
                foreach (DataRow drFF in ds.Tables[0].Rows)
                {
                    TableRow tr_det = new TableRow();
                    TableCell tc_det_Ses = new TableCell();
                    Literal lit_det_Ses = new Literal();
                    lit_det_Ses.Text = "&nbsp;" + drFF["chemists"].ToString();
                    tc_det_Ses.BorderStyle = BorderStyle.Solid;
                    tc_det_Ses.BorderWidth = 1;
                    tc_det_Ses.Controls.Add(lit_det_Ses);
                    tr_det.Cells.Add(tc_det_Ses);

                    TableCell tc_det_doc_name = new TableCell();
                    Literal lit_det_doc_name = new Literal();
                    lit_det_doc_name.Text = "&nbsp;" + drFF["chemww"].ToString();
                    tc_det_doc_name.BorderStyle = BorderStyle.Solid;
                    tc_det_doc_name.BorderWidth = 1;
                    tc_det_doc_name.Controls.Add(lit_det_doc_name);
                    tr_det.Cells.Add(tc_det_doc_name);

                    TableCell tc_det_ww = new TableCell();
                    Literal lit_det_ww = new Literal();
                    lit_det_ww.Text = "&nbsp;" + drFF["POBNo"].ToString();
                    tc_det_ww.BorderStyle = BorderStyle.Solid;
                    tc_det_ww.BorderWidth = 1;
                    tc_det_ww.Controls.Add(lit_det_ww);
                    tr_det.Cells.Add(tc_det_ww);

                    tblChem.Rows.Add(tr_det);
                }
            }
            else
            {
                lblch.Visible = false;
            }

        }
        else
        {
            lblch.Visible = false;
        }

    }

    private void Preview_Stk()
    {
        DataSet ds = new DataSet();

        sFile = sf_code + "_" + ViewState["curdate"].ToString() + "_Stockiest.xml";

        string FilePath = Server.MapPath(sFile);
        if (!File.Exists(FilePath))
        {
            //Start writer
            XmlTextWriter dr_writer = new XmlTextWriter(Server.MapPath(sFile), System.Text.Encoding.UTF8);

            //Start XM DOcument
            dr_writer.WriteStartDocument(true);
            dr_writer.Formatting = Formatting.Indented;
            dr_writer.Indentation = 2;

            //ROOT Element
            dr_writer.WriteStartElement("DCR");
            dr_writer.WriteEndElement();
            //End XML Document
            dr_writer.WriteEndDocument();
            //Close writer
            dr_writer.Close();
        }

        ds.ReadXml(Server.MapPath(sFile));

        if (ds != null && ds.HasChanges())
        {
            if (ds.Tables[0].Rows.Count > 0)
            {

                TableRow tr_header = new TableRow();
                tr_header.BorderStyle = BorderStyle.Solid;
                tr_header.BorderWidth = 1;

                TableCell tc_Ses = new TableCell();
                tc_Ses.BorderStyle = BorderStyle.Solid;
                tc_Ses.BorderWidth = 1;
                tc_Ses.Width = 300;
                Literal lit_Ses = new Literal();
                lit_Ses.Text = "Stockiest Name";
                tc_Ses.Controls.Add(lit_Ses);
                tr_header.Cells.Add(tc_Ses);
                tr_header.BackColor = System.Drawing.Color.Pink;

                TableCell tc_ww = new TableCell();
                tc_ww.BorderStyle = BorderStyle.Solid;
                tc_ww.BorderWidth = 1;
                tc_ww.Width = 300;
                Literal lit_ww = new Literal();
                lit_ww.Text = "<center>Worked With</center>";
                tc_ww.Controls.Add(lit_ww);
                tr_header.Cells.Add(tc_ww);

                TableCell tc_prod = new TableCell();
                tc_prod.BorderStyle = BorderStyle.Solid;
                tc_prod.BorderWidth = 1;
                tc_prod.Width = 420;
                Literal lit_prod = new Literal();
                lit_prod.Text = "<center>POB</center>";
                tc_prod.Controls.Add(lit_prod);
                tr_header.Cells.Add(tc_prod);

                TableCell tc_visit = new TableCell();
                tc_visit.BorderStyle = BorderStyle.Solid;
                tc_visit.BorderWidth = 1;
                tc_visit.Width = 420;
                Literal lit_visit = new Literal();
                lit_visit.Text = "<center>Visit</center>";
                tc_visit.Controls.Add(lit_visit);
                tr_header.Cells.Add(tc_visit);

                tblstk.Rows.Add(tr_header);

                //Details Section
                foreach (DataRow drFF in ds.Tables[0].Rows)
                {
                    TableRow tr_det = new TableRow();
                    TableCell tc_det_Ses = new TableCell();
                    Literal lit_det_Ses = new Literal();
                    lit_det_Ses.Text = "&nbsp;" + drFF["stockiest"].ToString();
                    tc_det_Ses.BorderStyle = BorderStyle.Solid;
                    tc_det_Ses.BorderWidth = 1;
                    tc_det_Ses.Controls.Add(lit_det_Ses);
                    tr_det.Cells.Add(tc_det_Ses);

                    TableCell tc_det_doc_name = new TableCell();
                    Literal lit_det_doc_name = new Literal();
                    lit_det_doc_name.Text = "&nbsp;" + drFF["stockww"].ToString();
                    tc_det_doc_name.BorderStyle = BorderStyle.Solid;
                    tc_det_doc_name.BorderWidth = 1;
                    tc_det_doc_name.Controls.Add(lit_det_doc_name);
                    tr_det.Cells.Add(tc_det_doc_name);

                    TableCell tc_det_ww = new TableCell();
                    Literal lit_det_ww = new Literal();
                    lit_det_ww.Text = "&nbsp;" + drFF["pob"].ToString();
                    tc_det_ww.BorderStyle = BorderStyle.Solid;
                    tc_det_ww.BorderWidth = 1;
                    tc_det_ww.Controls.Add(lit_det_ww);
                    tr_det.Cells.Add(tc_det_ww);

                    TableCell tc_det_vst = new TableCell();
                    Literal lit_det_vst = new Literal();
                    lit_det_vst.Text = "&nbsp;" + drFF["visit"].ToString();
                    tc_det_vst.BorderStyle = BorderStyle.Solid;
                    tc_det_vst.BorderWidth = 1;
                    tc_det_vst.Controls.Add(lit_det_vst);
                    tr_det.Cells.Add(tc_det_vst);

                    tblstk.Rows.Add(tr_det);
                }
            }
            else
            {
                lblst.Visible = false;
            }
        }
        else
        {
            lblst.Visible = false;
        }

    }

    private void FillUnlstDoc()
    {

        DataSet ds = new DataSet();
        //DataTable dt = new DataTable();

        sFile = sf_code + "_" + ViewState["curdate"].ToString() + "UnLstDR.xml";

        string FilePath = Server.MapPath(sFile);
        if (!File.Exists(FilePath))
        {
            //Start writer
            XmlTextWriter dr_writer = new XmlTextWriter(Server.MapPath(sFile), System.Text.Encoding.UTF8);

            //Start XM DOcument
            dr_writer.WriteStartDocument(true);
            dr_writer.Formatting = Formatting.Indented;
            dr_writer.Indentation = 2;

            //ROOT Element
            dr_writer.WriteStartElement("DCR");
            dr_writer.WriteEndElement();
            //End XML Document
            dr_writer.WriteEndDocument();
            //Close writer
            dr_writer.Close();
        }

        ds.ReadXml(Server.MapPath(sFile));
        if (ds != null && ds.HasChanges())
        {
            if (ds.Tables[0].Rows.Count > 0)
            {

                TableRow tr_header = new TableRow();
                tr_header.BorderStyle = BorderStyle.Solid;
                tr_header.BorderWidth = 1;

                TableCell tc_Ses = new TableCell();
                tc_Ses.BorderStyle = BorderStyle.Solid;
                tc_Ses.BorderWidth = 1;
                tc_Ses.Width = 30;
                Literal lit_Ses = new Literal();
                lit_Ses.Text = "Ses";
                tc_Ses.Controls.Add(lit_Ses);
                tr_header.Cells.Add(tc_Ses);
                tr_header.BackColor = System.Drawing.Color.Pink;

                TableCell tc_Time = new TableCell();
                tc_Time.BorderStyle = BorderStyle.Solid;
                tc_Time.BorderWidth = 1;
                tc_Time.Width = 40;
                Literal lit_Time = new Literal();
                lit_Time.Text = "<center>Time</center>";
                tc_Time.Controls.Add(lit_Time);
                tr_header.Cells.Add(tc_Time);

                TableCell tc_DR_Name = new TableCell();
                tc_DR_Name.BorderStyle = BorderStyle.Solid;
                tc_DR_Name.BorderWidth = 1;
                tc_DR_Name.Width = 300;
                Literal lit_DR_Name = new Literal();
                lit_DR_Name.Text = "<center>UnListed Doctor Name</center>";
                tc_DR_Name.Controls.Add(lit_DR_Name);
                tr_header.Cells.Add(tc_DR_Name);

                TableCell tc_ww = new TableCell();
                tc_ww.BorderStyle = BorderStyle.Solid;
                tc_ww.BorderWidth = 1;
                tc_ww.Width = 300;
                Literal lit_ww = new Literal();
                lit_ww.Text = "<center>Worked With</center>";
                tc_ww.Controls.Add(lit_ww);
                tr_header.Cells.Add(tc_ww);

                TableCell tc_prod = new TableCell();
                tc_prod.BorderStyle = BorderStyle.Solid;
                tc_prod.BorderWidth = 1;
                tc_prod.Width = 420;
                tc_prod.ColumnSpan = 6;
                Literal lit_prod = new Literal();
                lit_prod.Text = "<center>Product Promoted / Sampled</center>";
                tc_prod.Controls.Add(lit_prod);
                tr_header.Cells.Add(tc_prod);

                TableCell tc_Gift = new TableCell();
                tc_Gift.BorderStyle = BorderStyle.Solid;
                tc_Gift.BorderWidth = 1;
                tc_Gift.Width = 200;
                Literal lit_Gift = new Literal();
                lit_Gift.Text = "<center>Gift</center>";
                tc_Gift.Controls.Add(lit_Gift);
                tr_header.Cells.Add(tc_Gift);

                tblunlst.Rows.Add(tr_header);

                //Details Section
                foreach (DataRow drFF in ds.Tables[0].Rows)
                {
                    TableRow tr_det = new TableRow();
                    TableCell tc_det_Ses = new TableCell();
                    Literal lit_det_Ses = new Literal();
                    lit_det_Ses.Text = "&nbsp;" + drFF["session"].ToString();
                    tc_det_Ses.BorderStyle = BorderStyle.Solid;
                    tc_det_Ses.BorderWidth = 1;
                    tc_det_Ses.Controls.Add(lit_det_Ses);
                    tr_det.Cells.Add(tc_det_Ses);

                    TableCell tc_det_time = new TableCell();
                    Literal lit_det_time = new Literal();
                    lit_det_time.Text = "&nbsp;" + drFF["time"].ToString();
                    tc_det_time.BorderStyle = BorderStyle.Solid;
                    tc_det_time.BorderWidth = 1;
                    tc_det_time.Controls.Add(lit_det_time);
                    tr_det.Cells.Add(tc_det_time);

                    TableCell tc_det_doc_name = new TableCell();
                    Literal lit_det_doc_name = new Literal();
                    lit_det_doc_name.Text = "&nbsp;" + drFF["drcode"].ToString();
                    tc_det_doc_name.BorderStyle = BorderStyle.Solid;
                    tc_det_doc_name.BorderWidth = 1;
                    tc_det_doc_name.Controls.Add(lit_det_doc_name);
                    tr_det.Cells.Add(tc_det_doc_name);

                    TableCell tc_det_ww = new TableCell();
                    Literal lit_det_ww = new Literal();
                    lit_det_ww.Text = "&nbsp;" + drFF["workwith"].ToString();
                    tc_det_ww.BorderStyle = BorderStyle.Solid;
                    tc_det_ww.BorderWidth = 1;
                    tc_det_ww.Controls.Add(lit_det_ww);
                    tr_det.Cells.Add(tc_det_ww);

                    TableCell tc_det_prod1 = new TableCell();
                    Literal lit_det_prod1 = new Literal();
                    lit_det_prod1.Text = "&nbsp;" + drFF["prod1"].ToString().Replace("~", "").Trim();
                    tc_det_prod1.BorderStyle = BorderStyle.Solid;
                    tc_det_prod1.BorderWidth = 1;
                    tc_det_prod1.Width = 120;
                    tc_det_prod1.Controls.Add(lit_det_prod1);
                    tc_det_prod1.HorizontalAlign = HorizontalAlign.Center;
                    tr_det.Cells.Add(tc_det_prod1);

                    TableCell tc_det_qty1 = new TableCell();
                    Literal lit_det_qty1 = new Literal();
                    lit_det_qty1.Text = "&nbsp;" + drFF["qty1"].ToString();
                    tc_det_qty1.BorderStyle = BorderStyle.Solid;
                    tc_det_qty1.BorderWidth = 1;
                    tc_det_qty1.Width = 20;
                    tc_det_qty1.Controls.Add(lit_det_qty1);
                    tc_det_qty1.HorizontalAlign = HorizontalAlign.Center;
                    tr_det.Cells.Add(tc_det_qty1);

                    TableCell tc_det_prod2 = new TableCell();
                    Literal lit_det_prod2 = new Literal();
                    lit_det_prod2.Text = "&nbsp;" + drFF["prod2"].ToString().Replace("~", "").Trim();
                    tc_det_prod2.BorderStyle = BorderStyle.Solid;
                    tc_det_prod2.BorderWidth = 1;
                    tc_det_prod2.Width = 120;
                    tc_det_prod2.Controls.Add(lit_det_prod2);
                    tc_det_prod2.HorizontalAlign = HorizontalAlign.Center;
                    tr_det.Cells.Add(tc_det_prod2);

                    TableCell tc_det_qty2 = new TableCell();
                    Literal lit_det_qty2 = new Literal();
                    lit_det_qty2.Text = "&nbsp;" + drFF["qty2"].ToString();
                    tc_det_qty2.BorderStyle = BorderStyle.Solid;
                    tc_det_qty2.BorderWidth = 1;
                    tc_det_qty2.Width = 20;
                    tc_det_qty2.Controls.Add(lit_det_qty2);
                    tc_det_qty2.HorizontalAlign = HorizontalAlign.Center;
                    tr_det.Cells.Add(tc_det_qty2);

                    TableCell tc_det_prod3 = new TableCell();
                    Literal lit_det_prod3 = new Literal();
                    lit_det_prod3.Text = "&nbsp;" + drFF["prod3"].ToString().Replace("~", "").Trim();
                    tc_det_prod3.BorderStyle = BorderStyle.Solid;
                    tc_det_prod3.BorderWidth = 1;
                    tc_det_prod3.Width = 120;
                    tc_det_prod3.Controls.Add(lit_det_prod3);
                    tc_det_prod3.HorizontalAlign = HorizontalAlign.Center;
                    tr_det.Cells.Add(tc_det_prod3);

                    TableCell tc_det_qty3 = new TableCell();
                    Literal lit_det_qty3 = new Literal();
                    lit_det_qty3.Text = "&nbsp;" + drFF["qty3"].ToString();
                    tc_det_qty3.BorderStyle = BorderStyle.Solid;
                    tc_det_qty3.BorderWidth = 1;
                    tc_det_qty3.Width = 20;
                    tc_det_qty3.Controls.Add(lit_det_qty3);
                    tc_det_qty3.HorizontalAlign = HorizontalAlign.Center;
                    tr_det.Cells.Add(tc_det_qty3);

                    TableCell tc_det_gift = new TableCell();
                    Literal lit_det_gift = new Literal();
                    lit_det_gift.Text = "&nbsp;" + drFF["gift"].ToString().Replace("~", "").Trim() + " ( " + drFF["gqty"].ToString() + " ) ";
                    tc_det_gift.BorderStyle = BorderStyle.Solid;
                    tc_det_gift.BorderWidth = 1;
                    tc_det_gift.Controls.Add(lit_det_gift);
                    tc_det_gift.HorizontalAlign = HorizontalAlign.Center;
                    tr_det.Cells.Add(tc_det_gift);

                    tblunlst.Rows.Add(tr_det);
                }
            }
            else
            {
                lblunls.Visible = false;
            }

        }
        else
        {
            lblunls.Visible = false;
        }
    }
    private void Preview_Hos()
    {
        DataSet ds = new DataSet();

        sFile = sf_code + "_" + ViewState["curdate"].ToString() + "_Hospital.xml";

        string FilePath = Server.MapPath(sFile);
        if (!File.Exists(FilePath))
        {
            //Start writer
            XmlTextWriter dr_writer = new XmlTextWriter(Server.MapPath(sFile), System.Text.Encoding.UTF8);

            //Start XM DOcument
            dr_writer.WriteStartDocument(true);
            dr_writer.Formatting = Formatting.Indented;
            dr_writer.Indentation = 2;

            //ROOT Element
            dr_writer.WriteStartElement("DCR");
            dr_writer.WriteEndElement();
            //End XML Document
            dr_writer.WriteEndDocument();
            //Close writer
            dr_writer.Close();
        }

        ds.ReadXml(Server.MapPath(sFile));

        if (ds != null && ds.HasChanges())
        {
            if (ds.Tables[0].Rows.Count > 0)
            {

                TableRow tr_header = new TableRow();
                tr_header.BorderStyle = BorderStyle.Solid;
                tr_header.BorderWidth = 1;

                TableCell tc_Ses = new TableCell();
                tc_Ses.BorderStyle = BorderStyle.Solid;
                tc_Ses.BorderWidth = 1;
                tc_Ses.Width = 300;
                Literal lit_Ses = new Literal();
                lit_Ses.Text = "Hospital Name";
                tc_Ses.Controls.Add(lit_Ses);
                tr_header.Cells.Add(tc_Ses);
                tr_header.BackColor = System.Drawing.Color.Pink;

                TableCell tc_ww = new TableCell();
                tc_ww.BorderStyle = BorderStyle.Solid;
                tc_ww.BorderWidth = 1;
                tc_ww.Width = 300;
                Literal lit_ww = new Literal();
                lit_ww.Text = "<center>Worked With</center>";
                tc_ww.Controls.Add(lit_ww);
                tr_header.Cells.Add(tc_ww);

                TableCell tc_prod = new TableCell();
                tc_prod.BorderStyle = BorderStyle.Solid;
                tc_prod.BorderWidth = 1;
                tc_prod.Width = 420;
                Literal lit_prod = new Literal();
                lit_prod.Text = "<center>POB</center>";
                tc_prod.Controls.Add(lit_prod);
                tr_header.Cells.Add(tc_prod);


                tblhos.Rows.Add(tr_header);

                //Details Section
                foreach (DataRow drFF in ds.Tables[0].Rows)
                {
                    TableRow tr_det = new TableRow();
                    TableCell tc_det_Ses = new TableCell();
                    Literal lit_det_Ses = new Literal();
                    lit_det_Ses.Text = "&nbsp;" + drFF["hospital"].ToString();
                    tc_det_Ses.BorderStyle = BorderStyle.Solid;
                    tc_det_Ses.BorderWidth = 1;
                    tc_det_Ses.Controls.Add(lit_det_Ses);
                    tr_det.Cells.Add(tc_det_Ses);

                    TableCell tc_det_doc_name = new TableCell();
                    Literal lit_det_doc_name = new Literal();
                    lit_det_doc_name.Text = "&nbsp;" + drFF["hosww"].ToString();
                    tc_det_doc_name.BorderStyle = BorderStyle.Solid;
                    tc_det_doc_name.BorderWidth = 1;
                    tc_det_doc_name.Controls.Add(lit_det_doc_name);
                    tr_det.Cells.Add(tc_det_doc_name);

                    TableCell tc_det_ww = new TableCell();
                    Literal lit_det_ww = new Literal();
                    lit_det_ww.Text = "&nbsp;" + drFF["pob"].ToString();
                    tc_det_ww.BorderStyle = BorderStyle.Solid;
                    tc_det_ww.BorderWidth = 1;
                    tc_det_ww.Controls.Add(lit_det_ww);
                    tr_det.Cells.Add(tc_det_ww);

                    tblhos.Rows.Add(tr_det);
                }
            }
            else
            {
                lblhos.Visible = false;
            }
        }
        else
        {
            lblhos.Visible = false;
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        DCR dc = new DCR();

        sslno = dc.get_Trans_SlNo_toIns(sf_code, sCurDate);
        if (sslno > 0)
        {
            iret = dc.Create_DCRHead_Trans(sf_code, sslno);
            if (iret > 0)
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('DCR Approved Successfully');window.location='DCR_Approval.aspx'</script>");
            }
        } 
    }
    protected void btnReject_Click(object sender, EventArgs e)
    {
        txtReason.Visible = true;
        btnSave.Enabled = false;
        btnReject.Enabled = false;
        btnSubmit.Visible = true;       
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (txtReason.Text.Trim() != "")
        {
            DCR dc = new DCR();
            sslno = dc.get_Trans_SlNo_toIns(sf_code, sCurDate);
            if (sslno > 0)
            {
                iret = dc.Reject_DCR(sf_code, sslno, txtReason.Text);
                if (iret > 0)
                {
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('DCR Rejected Successfully');window.location='DCR_Approval.aspx'</script>");
                }
            }
        }
        else
        {
            txtReason.Focus();
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Enter the Reason')</script>");
        }

    }
}