using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
public partial class MasterFiles_BulkEditProdCat : System.Web.UI.Page
 
{

    #region "Declaration"
    DataSet dsProd = null;
    string div_code = string.Empty;
    int i;
    int iReturn = -1;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int time;
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        if (!Page.IsPostBack)
        {
            Session["backurl"] = "ProductCategoryList.aspx";
            menu1.Title = this.Page.Title;
            FillProdCat();
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
    private void FillProdCat()
    {
        Product dv = new Product();
        dsProd = dv.getProCat(div_code);
        if (dsProd.Tables[0].Rows.Count > 0)
        {
            btnSubmit.Visible = true;
            grdProdCat.Visible = true;
            grdProdCat.DataSource = dsProd;
            grdProdCat.DataBind();
        }
        else
        {
            grdProdCat.DataSource = dsProd;
            grdProdCat.DataBind();
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        string Product_Cat_Code = string.Empty;
        string Product_Cat_SName = string.Empty;
        string Product_Cat_Name = string.Empty;
        Product dv = new Product();
        int iReturn = -1;
        bool err = false;
        foreach (GridViewRow gridRow in grdProdCat.Rows)
        {
            Label lblProductCatCode = (Label)gridRow.Cells[1].FindControl("lblProductCatCode");
            Product_Cat_Code = lblProductCatCode.Text.ToString();
            TextBox txtProductCatSName = (TextBox)gridRow.Cells[1].FindControl("txtProductCatSName");
            Product_Cat_SName = txtProductCatSName.Text.ToString();
            TextBox txtProductCatName = (TextBox)gridRow.Cells[1].FindControl("txtProductCatName");
            Product_Cat_Name = txtProductCatName.Text.ToString();
            iReturn = dv.RecordUpdate(Convert.ToInt16(Product_Cat_Code), Product_Cat_SName, Product_Cat_Name, div_code);
            if (iReturn > 0)
                err = false;
            if ((iReturn == -2) )
            {
                txtProductCatName.Focus();
                err = true;
                break;
              
            }
            if ((iReturn == -3))
            {
                txtProductCatSName.Focus();
                err = true;
                break;
            }
        }

        //if (iReturn > 0)
        //{
        if (err == false)
        {
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Updated Successfully');window.location='ProductCategoryList.aspx';</script>");
        }
        else if (err == true)
        {
            if (iReturn == -2)
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Category  Name Already Exist');</script>");
                
            }
            else if (iReturn == -3)
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('category Code Already Exist');</script>");               
            }
        }
    }
    
}