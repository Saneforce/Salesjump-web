using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using Bus_EReport;


public partial class MasterFiles_ProductBrand : System.Web.UI.Page
{
    #region "Declaration"
    DataSet dsPBrd = null;
    DataSet dsCategory = null;
    DataSet dsdiv = null;
    string ProdBrdCode = string.Empty;
    string divcode = string.Empty;
    string Product_Brd_SName = string.Empty;
    string ProBrdName = string.Empty;
    string ProdCatCode = string.Empty;
    string ProdCatName = string.Empty;
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
        Session["backurl"] = "ProductBrandList.aspx";
        divcode = Convert.ToString(Session["div_code"]);
        ProdBrdCode = Request.QueryString["Product_Brd_Code"];
        txtProduct_Brd_SName.Focus();

        if (!Page.IsPostBack)
        {
            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);
            //FillCategory(divcode);
            FillDiv();
            if (ProdBrdCode != "" && ProdBrdCode != null)
            {
                Product dv = new Product();
                dsPBrd = dv.getProdBrd(divcode, ProdBrdCode);

                if (dsPBrd.Tables[0].Rows.Count > 0)
                {
                    txtProduct_Brd_SName.Text = dsPBrd.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                    txtProBrdName.Text = dsPBrd.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                    string cate = dsPBrd.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();
                    string st = dsPBrd.Tables[0].Rows[0].ItemArray.GetValue(3).ToString();
                    //ddlCategory.DataSource = dsCategory;
                    //ddlCategory.DataTextField = "Product_Cat_Name";
                    //ddlCategory.DataValueField = "Product_Cat_Code";
                    //ddlCategory.DataBind();
                    int iCount = 0, iIndex;
                    //foreach (ListItem item in ddlCategory.Items)
                    //{
                    //    if (cate == item.ToString())
                    //    {
                    //        iIndex = iCount;
                    //        ddlCategory.SelectedIndex = iIndex;
                    //        break;
                    //    }
                    //    iCount++;
                    //}
                    ddldiv.DataSource = dsdiv;
                    ddldiv.DataTextField = "subdivision_name";
                    ddldiv.DataValueField = "subdivision_code";
                    ddldiv.DataBind();
                    int iCount1 = 0, iIndex1;
                    foreach (ListItem item in ddldiv.Items)
                    {
                        if (st == item.ToString())
                        {
                            iIndex1 = iCount1;
                            ddldiv.SelectedIndex = iIndex1;
                            break;
                        }
                        iCount1++;
                    }
                }

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
        Product_Brd_SName = txtProduct_Brd_SName.Text;
        ProBrdName = txtProBrdName.Text;
        //ProdCatName = ddlCategory.SelectedItem.ToString();
        //ProdCatCode =ddlCategory.SelectedValue;
        string Pro_Div_name = ddldiv.SelectedItem.ToString();
        string Pro_Div_code = ddldiv.SelectedValue;
        if (ProdBrdCode == null)
        {
            //add new brand
            Product dv = new Product();
            int iReturn = dv.Brd_RecordAdd(divcode, Product_Brd_SName, ProBrdName, 0, "", Pro_Div_code, Pro_Div_name);

            if (iReturn > 0)
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Created Successfully');</script>");
                Resetall();
            }

            else if (iReturn == -2)
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Brand Name Already Exist');</script>");
                txtProBrdName.Focus();
            }

            else if (iReturn == -3)
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Brand Code Already Exist');</script>");
                txtProduct_Brd_SName.Focus();
            }
        }
        else
        {
            //Update product Brand
            Product dv = new Product();
            int iReturn = dv.Brd_RecordUpdate(Convert.ToInt16(ProdBrdCode), Product_Brd_SName, ProBrdName, divcode, 0, "", Pro_Div_code, Pro_Div_name);
            if (iReturn > 0)
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Updated Successfully');window.location='ProductBrandList.aspx';</script>");
            }

            else if (iReturn == -2)
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Brand Name Already Exist');</script>");
                txtProBrdName.Focus();
            }

            else if (iReturn == -3)
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Brand Code Already Exist');</script>");
                txtProduct_Brd_SName.Focus();
            }
        }
    }

    private void Resetall()
    {
        txtProduct_Brd_SName.Text = "";
        txtProBrdName.Text = "";
        //ddlCategory.SelectedIndex = 0;
        ddldiv.SelectedIndex = 0;
    }
}