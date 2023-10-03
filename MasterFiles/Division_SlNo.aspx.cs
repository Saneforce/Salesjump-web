using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;

public partial class MasterFiles_Division_SlNo : System.Web.UI.Page
{
    DataSet dsDivision = null;
    string div_code = string.Empty;
    string txtNewSlNo = string.Empty;
    string txtSlNo = string.Empty;
    string Division_Code = string.Empty;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int time;
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["backurl"] = "DivisionList.aspx";
        div_code = Convert.ToString(Session["div_code"]);
        if (!Page.IsPostBack)
        {
            FillDivision();
            menu1.Title = this.Page.Title;
            //menu1.FindControl("btnBack").Visible = false;
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
    private void FillDivision()
    {
        Division dv = new Division();
        dsDivision = dv.getDivision();
        if (dsDivision.Tables[0].Rows.Count > 0)
        {
            grdDivision.Visible = true;
            grdDivision.DataSource = dsDivision;
            grdDivision.DataBind();
        }
        else
        {
             btnSubmit.Visible = false;
            btnClear.Visible = false;
            grdDivision.DataSource = dsDivision;
            grdDivision.DataBind();
        }
    }
    //sorting

    // Sorting
    public SortDirection dir
    {
        get
        {
            if (ViewState["dirState"] == null)
            {
                ViewState["dirState"] = SortDirection.Ascending;
            }
            return (SortDirection)ViewState["dirState"];
        }
        set
        {
            ViewState["dirState"] = value;
        }
    }
    private DataTable BindGridView()
    { DataTable dtGrid = new DataTable();
        Division dv = new Division();
        dtGrid = dv.getDivisionlist_DataTable();
        btnSubmit.Text = "Generate - Sl No";
        return dtGrid;
    }

     protected void grdDivision_Sorting(object sender, GridViewSortEventArgs e)
    {

        string sortingDirection = string.Empty;
        if (dir == SortDirection.Ascending)
        {
            dir = SortDirection.Descending;
            sortingDirection = "Desc";
        }
        else
        {
            dir = SortDirection.Ascending;
            sortingDirection = "Asc";
        }

        DataView sortedView = new DataView(BindGridView());
        sortedView.Sort = e.SortExpression + " " + sortingDirection;
        grdDivision.DataSource = sortedView;
        grdDivision.DataBind();
    }
    //end
    protected void btnSubmit_Click(object sender, EventArgs e)
     {
         bool isError = false;
         if (btnSubmit.Text == "Generate - Sl No")
         {
             System.Threading.Thread.Sleep(time);
             int i = 1;
             int iVal = 1;
             string sVal = string.Empty;
             sVal = ",";

             foreach (GridViewRow gridRow in grdDivision.Rows)
             {
                 TextBox txtSNo = (TextBox)gridRow.Cells[1].FindControl("txtSlNo");
                 if (txtSNo.Text.Length > 0)
                 {
                     if (grdDivision.Rows.Count >= Convert.ToInt16(txtSNo.Text.Trim()))
                     {
                         sVal = sVal + txtSNo.Text + ',';
                     }
                     else
                     {
                         isError = true;
                         break;
                     }
                 }
                 i++;
             }
             if (isError == false)
             {

                 if (sVal == "")
                 {
                     foreach (GridViewRow gridRow in grdDivision.Rows)
                     {
                         Label lblSNo = (Label)gridRow.Cells[1].FindControl("lblSNo");
                         TextBox txtSNo = (TextBox)gridRow.Cells[1].FindControl("txtSlNo");
                         txtSNo.Text = lblSNo.Text;
                     }
                 }
                 else
                 {
                     iVal = 1;
                     System.Threading.Thread.Sleep(time);
                     foreach (GridViewRow gridRow in grdDivision.Rows)
                     {
                         TextBox txtSNo = (TextBox)gridRow.Cells[1].FindControl("txtSlNo");
                         if (txtSNo.Text.Length <= 0)
                         {
                             for (iVal = 1; iVal <= i; iVal++)
                             {
                                 string schk = ',' + iVal.ToString() + ',';
                                 if (sVal.IndexOf(schk) != -1)
                                 {
                                     //Do Nothing
                                 }
                                 else
                                 {
                                     sVal = sVal + iVal.ToString() + ',';
                                     break;
                                 }
                             }

                             txtSNo.Text = iVal.ToString();
                         }

                     }
                 }
                 btnSubmit.Focus();
                 btnSubmit.Text = "Save";
             }
             else if (isError == true)
             {
                 ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Enter Valid Number');</script>");
             }
         }
         else
         {
             System.Threading.Thread.Sleep(time);
             // Save
             foreach (GridViewRow gridRow in grdDivision.Rows)
             {
                 TextBox txtSNo = (TextBox)gridRow.Cells[1].FindControl("txtSlNo");
                 txtSlNo = txtSNo.Text;
                 Label lblDivCode = (Label)gridRow.Cells[2].FindControl("lblDivCode");
                 div_code = lblDivCode.Text;

                 Division dv = new Division();
                 int iReturn = dv.Update_DivisionSno(txtSlNo, div_code);
                 if (iReturn > 0)
                 {
                     // menu1.Status = "Sl No Updated Successfully ";
                     ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Sl No Updated Successfully');</script>");
                 }
                 //else if (iReturn == -2)
                 //{
                 //    //menu1.Status = "SlNo could not be updated!!";
                 //    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('SlNo Already Exist');</script>");
                 //}
             }

         }
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow gridRow in grdDivision.Rows)
        {
            TextBox txtSNo = (TextBox)gridRow.Cells[1].FindControl("txtSlNo");
            txtSNo.Text = "";
        }
    }

}
   
