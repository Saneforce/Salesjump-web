using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;

public partial class MasterFiles_Mails_cntrlMail : System.Web.UI.UserControl
{
    DataSet dsMail = null;
    string div_code = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        FillEMail();
    }

    private void FillEMail()
    {
        AdminSetup adm = new AdminSetup();
        dsMail = adm.getMail(div_code);
        if (dsMail.Tables[0].Rows.Count > 0)
        {
            foreach (DataRow drMail in dsMail.Tables[0].Rows)
            {
                TableRow tr_det = new TableRow();

                TableCell tc_img = new TableCell();
                tc_img.VerticalAlign = 0;
                Image img = new Image();
                img.AlternateText = "Inbox";
                img.ImageUrl = "../../images/Closed.ICO";
                img.Width = 16;
                img.Height = 16;
                tc_img.Controls.Add(img);
                tc_img.Width = 20;
                tc_img.BorderStyle = BorderStyle.None;
                tr_det.Cells.Add(tc_img);

                TableCell tc_det = new TableCell();
                tc_det.VerticalAlign = 0;
                Literal lit_det = new Literal();
                lit_det.Text = "&nbsp;" + drMail["Move_MailFolder_Name"].ToString();
                tc_det.Controls.Add(lit_det);
                tc_det.Width = 100;
                tc_img.BorderStyle = BorderStyle.None;
                tr_det.Cells.Add(tc_det);
                tbl.Rows.Add(tr_det);
            }

        }
    }
}