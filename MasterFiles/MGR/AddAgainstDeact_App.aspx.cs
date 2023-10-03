using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
public partial class MasterFiles_MGR_AddAgainstDeact_App : System.Web.UI.Page
{
    #region "Declaration"
    DataSet dsDoc = null;
    DataSet dsListedDR = null;
    string sfCode = string.Empty;
    string sf_code = string.Empty;
    string ListedDrCode = string.Empty;
    string div_code = string.Empty;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int time;
    string sQryStr = string.Empty;
    string sfcode = string.Empty;
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        sfCode = Session["sf_code"].ToString();
        sQryStr = Request.QueryString["sfcode"].ToString();
        // MR_Code = sQryStr.Substring(0, sQryStr.IndexOf('-'));
        if (!Page.IsPostBack)
        {
            Session["backurl"] = "ListedDR_Add_Approve.aspx";
            // menu1.Title = this.Page.Title;          
            FillDoc();
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
    private void FillDoc()
    {
        ListedDR LstDoc = new ListedDR();
        dsDoc = LstDoc.getListedDr_addAgainst(sQryStr);
        if (dsDoc.Tables[0].Rows.Count > 0)
        {
            grdDoctor.Visible = true;
            grdDoctor.DataSource = dsDoc;
            grdDoctor.DataBind();
        }
        else
        {
            btnApprove.Visible = false;
            //btnReject.Visible = false;
            grdDoctor.DataSource = dsDoc;
            grdDoctor.DataBind();
        }
        foreach (GridViewRow gridRow in grdDoctor.Rows)
        {
            CheckBox checklist = (CheckBox)gridRow.FindControl("chkListedDR");

            foreach (DataRow drDoc in dsDoc.Tables[0].Rows)
            {             
      
                if (dsDoc.Tables[0].Rows[gridRow.RowIndex][8].ToString() == "3")
                {
                    checklist.Visible = false;
                    gridRow.Style.Add("background-color", "LightBlue");                   

                }
            }
        }
     
    }
    protected void grdDoctor_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        foreach (GridViewRow gridRow in grdDoctor.Rows)
        {
            DataTable dtdoc = new DataTable();
            CheckBox checklist = (CheckBox)gridRow.FindControl("chkListedDR");
            Label SLVNo = (Label)gridRow.FindControl("SLVNo");

            dtdoc = dsDoc.Tables[0];
            dtdoc.DefaultView.ToTable(true, "SLVNo");

            //foreach (DataRow drDoc in dsDoc.Tables[0].Rows)
            //{

            //    if (dsDoc.Tables[0].Rows[gridRow.RowIndex][2].ToString() == SLVNo.Text)
            //    {
            //        e.Row.Cells[0].RowSpan = 2;
                
            //    }
                
            //}
        }
    }
  
    protected void btnBack_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        Response.Redirect("MGR_Index.aspx");
    }
    protected void btnApprove_Click(object sender, EventArgs e)
    {
        int iReturn = -1;
        int iflag = -1;
        ListedDR LstDoc = new ListedDR();
        dsDoc = LstDoc.getListedDr_addAgainst(sQryStr);
       
        foreach (GridViewRow gridRow in grdDoctor.Rows)
        {
            CheckBox chkDR = (CheckBox)gridRow.Cells[0].FindControl("chkListedDR");
            bool bCheck = chkDR.Checked;
            Label lblDR = (Label)gridRow.Cells[2].FindControl("lblDocCode");
            string ListedDR = lblDR.Text.ToString();
            Label lblSlvno = (Label)gridRow.Cells[4].FindControl("lblslvno");
            string slvno = lblSlvno.Text;
            Label lblmode = (Label)gridRow.Cells[5].FindControl("lblmode");

            DataTable dt = new DataTable();
           
            DataRow[] DataRow = dsDoc.Tables[0].Select("SLVNo=" + slvno);
            
            //dt = result.
           
            if ((bCheck == true))
            {
                foreach(DataRow dr in DataRow)
                {
                    //ListedDR LstDoc = new ListedDR();
                    
                    if (dr["mode"].ToString() == "Addition")
                    {
                        iflag = 0;
                    }
                    if (dr["mode"].ToString() == "Deactivation")
                    {
                        iflag = 1;
                    }
                    else
                    {
                        iflag = Convert.ToInt32(dr["ListedDr_Active_Flag"].ToString());
                    }
                    iReturn = LstDoc.ApproveAddDeact(sQryStr, dr["ListedDrCode"].ToString(), iflag, Session["sf_name"].ToString(), slvno);

                    if (iReturn > 0)
                    {
                        ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Listed Doctor has been Approved Successfully');</script>");                     
                    }                    
                 }

              }
             else
             {

             }
            
        }

        if (iReturn != -1)
        {
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Approved Successfully');</script>");
            FillDoc();
        }
    }
    //protected void btnReject_Click(object sender, EventArgs e)
    //{

    //}
}