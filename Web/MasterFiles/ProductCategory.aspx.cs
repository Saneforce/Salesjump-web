using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using Bus_EReport;

public partial class MasterFiles_ProductCategory : System.Web.UI.Page
{
    #region "Declaration"
    DataSet dsPCat= null;
    DataSet dsdiv = null;
    string Pro_Div_name = string.Empty;
    string Pro_Div_code = string.Empty;
    string ProdCatCode = string.Empty;
    string divcode = string.Empty;
    string Product_Cat_SName = string.Empty;
    string ProCatName = string.Empty;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int time;
    #endregion
	string sf_type = string.Empty;
    protected override void OnPreInit(EventArgs e)
    {
        base.OnPreInit(e);
        sf_type = Session["sf_type"].ToString();
        if (sf_type == "3")
        {
            this.MasterPageFile = "~/Master.master";
        }
        else if (sf_type == "2")
        {
            this.MasterPageFile = "~/Master_MGR.master";
        }
        else if (sf_type == "1")
        {
            this.MasterPageFile = "~/Master_MR.master";
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["backurl"] = "ProductCategoryList.aspx";
        divcode = Convert.ToString(Session["div_code"]);
        ProdCatCode = Request.QueryString["Product_Cat_Code"];
        txtProduct_Cat_SName.Focus();
        
        if (!Page.IsPostBack)
        {
           
            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);

            if (ProdCatCode != "" && ProdCatCode != null)
            {

                Product dv = new Product();
                dsPCat = dv.getProCate(divcode, ProdCatCode);
                FillDiv();
                if (dsPCat.Tables[0].Rows.Count > 0)
                {
                    txtProduct_Cat_SName.Text = dsPCat.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                    txtProCatName.Text = dsPCat.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                    string st = dsPCat.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();
                    ddldiv.DataSource = dsdiv;
                    ddldiv.DataTextField = "subdivision_name";
                    ddldiv.DataValueField = "subdivision_code";
                    ddldiv.DataBind();
                    int iCount = 0, iIndex;
                    foreach (ListItem item in ddldiv.Items)
                    {
                        if (st == item.ToString())
                        {
                            iIndex = iCount;
                            ddldiv.SelectedIndex = iIndex;
                            break;
                        }
                        iCount++;
                    }
                }

            }
            else
            {
                FillDiv();
            }
          //menu1.Title = this.Page.Title;
        
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
    public void FillDiv()
    {
        Product dv = new Product();
        dsdiv = dv.getDiv(divcode);
        ddldiv.DataTextField = "subdivision_name";
        ddldiv.DataValueField = "subdivision_code";
        ddldiv.DataSource = dsdiv;
        ddldiv.DataBind();

    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        Product_Cat_SName = txtProduct_Cat_SName.Text;
        ProCatName = txtProCatName.Text;
        Pro_Div_name = ddldiv.SelectedItem.ToString();
        Pro_Div_code = ddldiv.SelectedValue;
        if (ProdCatCode == null)
        {
            // Add New Product Category
            Product dv = new Product();
            int iReturn = dv.RecordAdd(divcode, Product_Cat_SName, ProCatName, Pro_Div_code, Pro_Div_name);

             if (iReturn > 0 )
            {
                //menu1.Status = "Product Category Created Successfully ";
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Created Successfully');</script>");
                Resetall();
            }
            else if (iReturn == -2)
            {                
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Category Name Already Exist');</script>");
                txtProCatName.Focus();
            }
             else if (iReturn == -3)
             {
                 ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Category Code Already Exist');</script>");
                 txtProduct_Cat_SName.Focus();
             }
        }
        else
        {

            // Update Product Category
            Product dv = new Product();
            int iReturn = dv.RecordUpdate(Convert.ToInt16(ProdCatCode), Product_Cat_SName, ProCatName, divcode,Pro_Div_code,Pro_Div_name);
             if (iReturn > 0 )
            {
               // menu1.Status = "Product Category Updated Successfully ";
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Updated Successfully');window.location='ProductCategoryList.aspx';</script>");
            }
             else if (iReturn == -2)
             {
                 ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Category Name Already Exist');</script>");
                 txtProCatName.Focus();
             }
             else if (iReturn == -3)
             {
                 ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Category Code Already Exist');</script>");
                 txtProduct_Cat_SName.Focus();
             }
        }
    }
    private void Resetall()
    {
        txtProduct_Cat_SName.Text = "";
        txtProCatName.Text = "";
        ddldiv.SelectedIndex = 0;
    }

}