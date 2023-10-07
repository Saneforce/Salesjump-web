using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;


public partial class MasterFiles_ProdSlNo_Gen : System.Web.UI.Page
{
    #region "Declaration"
        DataSet dsProd = null;
        string div_code = string.Empty;
        string ProdCode = string.Empty;
        string txtSlNo = string.Empty;
        string ProdSaleUnit = string.Empty;
        string ProdName = string.Empty;
        DateTime ServerStartTime;
        DateTime ServerEndTime;
        int time;
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        Session["backurl"] = "ProductList.aspx";
        div_code = Session["div_code"].ToString();
        if (!Page.IsPostBack)
        {
            FillProd();
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
    private void FillProd()
    {
        Product dv = new Product();
        dsProd = dv.getProdSlNo(div_code);
        if (dsProd.Tables[0].Rows.Count > 0)
        {
            grdProduct.Visible = true;
            grdProduct.DataSource = dsProd;
            grdProduct.DataBind();
        }
        else
        {
            btnClear.Visible = false;
            btnSubmit.Visible = false;
            grdProduct.DataSource = dsProd;
            grdProduct.DataBind();
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
            sVal = ","; // Initialized by Sri
            foreach (GridViewRow gridRow in grdProduct.Rows)
            {
                TextBox txtSNo = (TextBox)gridRow.Cells[1].FindControl("txtSlNo");
                if (txtSNo.Text.Length > 0)
                {
                    if(grdProduct.Rows.Count >= Convert.ToInt16(txtSNo.Text.Trim())) // This is to check if the user entered a valid number
                    {
                        sVal =  sVal + txtSNo.Text +','; // Modified..
                    }
                    else//Added by sri - to validate
                    {
                        isError = true; // User entered a number greater than the count..
                        break;
                    }
                }
                i++;
            }
            if (isError == false) // Included to validate
            {
                if (sVal == "")
                {
                    foreach (GridViewRow gridRow in grdProduct.Rows)
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
                    foreach (GridViewRow gridRow in grdProduct.Rows)
                    {
                        TextBox txtSNo = (TextBox)gridRow.Cells[1].FindControl("txtSlNo");
                        if (txtSNo.Text.Length <= 0)
                        {
                            for (iVal = 1; iVal <= i; iVal++)
                            {
                                string schk = ',' + iVal.ToString() + ',';// Added by sri
                                if (sVal.IndexOf(schk) != -1)// Modified by Sri
                                {
                                    //Do Nothing
                                }
                                else
                                {
                                    sVal = sVal + iVal.ToString() + ',';// Modified by Sri
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
            else if (isError == true) //Added by Sri - whole else part
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Enter Valid Number');</script>");
            }
           
        }
        else
        {
            System.Threading.Thread.Sleep(time);
            // Save
            foreach (GridViewRow gridRow in grdProduct.Rows)
            {
                TextBox txtSNo = (TextBox)gridRow.Cells[1].FindControl("txtSlNo"); 
                txtSlNo = txtSNo.Text;
                ProdCode = gridRow.Cells[0].Text;                
               
                Product dv = new Product();
                int iReturn = dv.UpdateProdSno(ProdCode, txtSlNo);
                 if (iReturn > 0 )
                {
                    //menu1.Status = "Sl No Updated Successfully ";
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Sl No Updated Successfully');window.location='ProductList.aspx';</script>");
                   
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
        foreach (GridViewRow gridRow in grdProduct.Rows)
        {
            TextBox txtSNo = (TextBox)gridRow.Cells[1].FindControl("txtSlNo");
            txtSNo.Text = "";
        }
    }

    // Changes done by Priya
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
        Product dv = new Product();
        dtGrid = dv.getProductdet_DataTable(div_code);
        btnSubmit.Text = "Generate - Sl No";
        return dtGrid;
    }

    protected void grdProduct_Sorting(object sender, GridViewSortEventArgs e)
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
        grdProduct.DataSource = sortedView;
        grdProduct.DataBind();

    }

}