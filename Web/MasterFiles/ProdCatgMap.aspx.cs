using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;

public partial class MasterFiles_ProdCatgMap : System.Web.UI.Page
{
    string div_code = string.Empty;
    DataSet dsProduct = null;
    int iReturn = -1;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int time;
    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();

        if (!Page.IsPostBack)
        {            
            FillCategory();            
            menu1.Title = this.Page.Title;
            Session["backurl"] = "ProductList.aspx";
            ViewState["Cat_NilCode"] = getNilCode(div_code);
            ViewState["div_code"] = Session["div_code"].ToString();
            //menu1.FindControl("pnlHead").Visible = true;
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

    private int getNilCode(string div_code)
    {
        Product prd = new Product();
        iReturn = prd.getNilCode(div_code);
        return iReturn;
    }

    private void FillCategory()
    {
        Product prd = new Product();
        dsProduct = prd.getProductCategory(div_code);
        if (dsProduct.Tables[0].Rows.Count > 0)
        {
            ddlCat.DataTextField = "Product_Cat_Name";
            ddlCat.DataValueField = "Product_Cat_Code";
            ddlCat.DataSource = dsProduct;
            ddlCat.DataBind();
        }
    }

    protected void ddlCat_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillProduct(ddlCat.SelectedValue);
        tblProduct.Visible = true;
    }

    private void FillProduct(string cat_code)
    {
        Product prd = new Product();
        dsProduct = prd.getProductForCategory(div_code, cat_code, ViewState["Cat_NilCode"].ToString());
        if (dsProduct.Tables[0].Rows.Count > 0)
        {
            chkProduct.DataTextField = "Product_Detail_Name";
            chkProduct.DataValueField = "Product_Detail_Code";            
            chkProduct.DataSource = dsProduct;
            chkProduct.DataBind();

            for (int i = 0; i < chkProduct.Items.Count; i++)
            {
                string[] sCatg = chkProduct.Items[i].Text.ToString().Split('-');
                chkProduct.Items[i].Text = sCatg[0];
                if (sCatg[1] == "Nil Category")
                {
                    chkProduct.Items[i].Selected = false;
                }
                else
                {
                    chkProduct.Items[i].Selected = true;
                }
            }
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        iReturn = -1;
        for (int i = 0; i < chkProduct.Items.Count; i++)
        {
            Product prd = new Product();
            if (chkProduct.Items[i].Selected == false)
            {                
                iReturn = prd.RecordUpdate_NilCode(chkProduct.Items[i].Value, ViewState["Cat_NilCode"].ToString(), ViewState["div_code"].ToString());
            }
            else
            {
                iReturn = prd.RecordUpdate_NilCode(chkProduct.Items[i].Value, ddlCat.SelectedValue.ToString(), ViewState["div_code"].ToString());
            }
        }

        if (iReturn != -1)
        {
            //menu1.Status = "Product Category is mapped Successfully";
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Product Category is mapped Successfully');</script>");
            ddlCat.SelectedIndex = 0;
            tblProduct.Visible = false;    
        }
    }
}