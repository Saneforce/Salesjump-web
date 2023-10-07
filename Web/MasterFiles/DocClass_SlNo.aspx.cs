using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;

public partial class MasterFiles_DocClass_SlNo : System.Web.UI.Page
{
    #region "Declaration"
    DataSet dsDocCat = null;
    string div_code = string.Empty;
    string divcode = string.Empty;
    string Doc_Cat_SName = string.Empty;
    string Doc_Cat_Name = string.Empty;
    string Doc_Cat_Code = string.Empty;
    string txtSlNo = string.Empty;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int time;
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        Session["backurl"] = "DoctorClassList.aspx";
        div_code = Session["div_code"].ToString();
        if (!Page.IsPostBack)
        {
            FillDocCls();
            menu1.Title = this.Page.Title;
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
    {
        DataTable dtGrid = new DataTable();
        Doctor dv = new Doctor();
        dtGrid = dv.getDocClslist_DataTable(div_code);
        btnSubmit.Text = "Generate - Sl No";
        return dtGrid;
    }
    protected void grdDocCls_Sorting(object sender, GridViewSortEventArgs e)
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
        grdDocCat.DataSource = sortedView;
        grdDocCat.DataBind();
    }
    //
    private void FillDocCls()
    {
        Doctor dv = new Doctor();
        dsDocCat = dv.getDocCls(div_code);
        if (dsDocCat.Tables[0].Rows.Count > 0)
        {
            grdDocCat.Visible = true;
            grdDocCat.DataSource = dsDocCat;
            grdDocCat.DataBind();
        }
        else
        {
            btnClear.Visible = false;
            btnSubmit.Visible = false;
            grdDocCat.DataSource = dsDocCat;
            grdDocCat.DataBind();
        }
    }

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

            foreach (GridViewRow gridRow in grdDocCat.Rows)
            {
                TextBox txtSNo = (TextBox)gridRow.Cells[1].FindControl("txtSlNo");
                if (txtSNo.Text.Length > 0)
                {
                    if (grdDocCat.Rows.Count >= Convert.ToInt16(txtSNo.Text.Trim()))
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
                    foreach (GridViewRow gridRow in grdDocCat.Rows)
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
                    foreach (GridViewRow gridRow in grdDocCat.Rows)
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
            foreach (GridViewRow gridRow in grdDocCat.Rows)
            {
                TextBox txtSNo = (TextBox)gridRow.Cells[1].FindControl("txtSlNo");
                txtSlNo = txtSNo.Text;

                Label lDoc_Cat_Code = (Label)gridRow.Cells[0].FindControl("Doc_Cat_Code");
                Doc_Cat_Code = lDoc_Cat_Code.Text;
                // Update Division
                Doctor dv = new Doctor();
                int iReturn = dv.Update_DocClassSno(Doc_Cat_Code, txtSlNo);
                if (iReturn > 0)
                {
                    //menu1.Status = "Sl No Updated Successfully ";
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Sl No Updated Successfully');</script>");
                }
                else if (iReturn == -2)
                {
                    //  menu1.Status = "SlNo could not be updated!!";
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('SlNo could not be updated');</script>");
                }
            }

        }
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow gridRow in grdDocCat.Rows)
        {
            TextBox txtSNo = (TextBox)gridRow.Cells[1].FindControl("txtSlNo");
            txtSNo.Text = "";
        }
    }



}