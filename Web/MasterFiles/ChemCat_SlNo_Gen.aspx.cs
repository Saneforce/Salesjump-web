using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;

public partial class MasterFiles_ChemCat_SlNo_Gen : System.Web.UI.Page
{

    #region "Declaration"
    DataSet dsChemCat = null;
    string div_code = string.Empty;
    string Chem_Cat_SName = string.Empty;
    string Chem_Cat_Name = string.Empty;
    string Chem_Cat_Code = string.Empty;
    string txtSlNo = string.Empty;
    string divcode = string.Empty;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int time;
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        Session["backurl"] = "ChemistCategoryList.aspx";
        div_code = Session["div_code"].ToString();
        if (!Page.IsPostBack)
        {
            FillChemCat();
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
    //Sorting 

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
        Chemist chem = new Chemist();
        dtGrid = chem.getChemistCategorylist_DataTable(div_code);
        btnSubmit.Text = "Generate - Sl No";
        return dtGrid;
    }
    protected void grdChemCat_Sorting(object sender, GridViewSortEventArgs e)
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
        grdChemCat.DataSource = sortedView;
        grdChemCat.DataBind();

    }
    private void FillChemCat()
    {
        Chemist chem = new Chemist();
        dsChemCat = chem.getChemCat(div_code);
        if (dsChemCat.Tables[0].Rows.Count > 0)
        {
            grdChemCat.Visible = true;
            grdChemCat.DataSource = dsChemCat;
            grdChemCat.DataBind();
        }
        else
        {
            btnClear.Visible = false;
            btnSubmit.Visible = false;
            grdChemCat.DataSource = dsChemCat;
            grdChemCat.DataBind();
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        bool isError = false;
        System.Threading.Thread.Sleep(time);
        if (btnSubmit.Text == "Generate - Sl No")
        {
            int i = 1;
            int iVal = 1;
            string sVal = string.Empty;
            sVal = ",";

            foreach (GridViewRow gridRow in grdChemCat.Rows)
            {
                TextBox txtSNo = (TextBox)gridRow.Cells[1].FindControl("txtSlNo");
                if (txtSNo.Text.Length > 0)
                {
                    if (grdChemCat.Rows.Count >= Convert.ToInt16(txtSNo.Text.Trim()))
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
                    foreach (GridViewRow gridRow in grdChemCat.Rows)
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
                    foreach (GridViewRow gridRow in grdChemCat.Rows)
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
            System.Threading.Thread.Sleep(time);
            // Save
            foreach (GridViewRow gridRow in grdChemCat.Rows)
            {
                TextBox txtSNo = (TextBox)gridRow.Cells[1].FindControl("txtSlNo");
                txtSlNo = txtSNo.Text;
                Label lChem_Cat_Code = (Label)gridRow.Cells[0].FindControl("Chem_Cat_Code");
                Chem_Cat_Code = lChem_Cat_Code.Text;


                // Update Division
                Chemist chem = new Chemist();
                int iReturn = chem.Update_ChemCatSno(Chem_Cat_Code, txtSlNo);
                if (iReturn > 0)
                {
                    // menu1.Status = "Sl No Updated Successfully ";
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Sl No Updated Successfully');window.location='ChemistCategoryList.aspx';</script>");
                }
                else if (iReturn == -2)
                {
                    //menu1.Status = "SlNo could not be updated!!";
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('SlNo could not be updated');</script>");
                }
            }

        }
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow gridRow in grdChemCat.Rows)
        {
            TextBox txtSNo = (TextBox)gridRow.Cells[1].FindControl("txtSlNo");
            txtSNo.Text = "";
        }
    }


}