using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bus_EReport;
using System.Data;

public partial class Default1 : System.Web.UI.Page
{

    #region Declaration
    DataSet dsSalesForce = null;
    DataSet dsdoc = null;
    DataSet dsDoctor = null;
    DataSet dsTP = null;
    string iPendingCount = string.Empty;
    string div_code = string.Empty;
    string sf_code = string.Empty;
    string sto_code = string.Empty;
    //DataSet dsState = null;
    string Month = string.Empty;
    string Year = string.Empty;
    int count_tot = 0;
    int count_tot1 = 0;
    DataSet dsDivision = null;
    DataSet dsState = null;
    string state_cd = string.Empty;
    string sState = string.Empty;
    string[] statecd;
    string stCrtDtaPnt = string.Empty;
    string iTotLstCount1 = string.Empty;
    string stCrtDtaPnt1 = string.Empty;
    string iTotLstCount2 = string.Empty;
    string iTotLstCount3 = string.Empty;
    string iTotLstCounts1 = string.Empty;
    string iTotLstCounts2 = string.Empty;
    string iTotLstCounts3 = string.Empty;
    string sf_type = string.Empty;
    Notice viewnoti = new Notice();
    string day = string.Empty;
    string type = string.Empty;
    string comment = string.Empty;
    public static string baseUrl = "";
    #endregion

    #region Page_Load
    protected void Page_Load(object sender, EventArgs e)
    {
        baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority + Request.ApplicationPath.TrimEnd('/') + "/";
        if ((Convert.ToString(Session["sf_type"]) != null || Convert.ToString(Session["sf_type"]) != ""))
        {
            sf_type = Session["sf_type"].ToString();
            sf_code = Session["sf_code"].ToString();
            //HO_ID = Session["HO_ID"].ToString();
            if (sf_type == "3")
            {
                div_code = Session["division_code"].ToString();
            }
            else
            {
                div_code = Session["division_code"].ToString();
            }
            if (sf_type == "1")
            {
                div_code = Session["div_code"].ToString();
            }
            else
            {
                div_code = Session["div_code"].ToString();
            }
            if (sf_type == "2")
            {
                div_code = Session["div_code"].ToString();
            }
            else
            {
                div_code = Session["div_code"].ToString();
            }
            //div_code = "1";
            getsto();
            div_code = div_code.Trim(",".ToCharArray());
            string scrpt = "arr=[" + Fillcate() + "];arr1=[" + Fillbrand() + "];arr2=[" + Fillpro() + "];arr3=[" + saleFillcate() + "];arr4=[" + saleFillbrand() + "];arr5=[" + saleFillpro() + "];arr6=[" + Fillcate() + "];arr7=[" + saleFillbrand() + "];arr8=[" + saleFillpro() + "];window.onload = function () {genChart('T10brand',arr,'Purchase Top 10 Categorys');genChart('T10Cate',arr1,'Purchase Top 10 Brands');genChart('T10Pro',arr2,'Purchase Top 10 Products');genChart('saleT10Cate',arr3,'Sale Top 10 Categorys');genChart('saleT10brand',arr4,'Sale Top 10 brands');genChart('saleT10Pro',arr5,'Sale Top 10 Products');genChart1('RetailerT10Cate',arr6,'Sale Top 10 Products');genChart('RetailerT10brand',arr6,'Retailer Top 10 Categorys');genChart('RetailerT10Pro',arr7,'Retailer Top 10 brands');genChart('RetailerT10',arr8,'Retailer Top 10 Products');}";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "SetupData", scrpt, true);
            if (!Page.IsPostBack)
            {

                ViewState["dsSalesForce"] = null;
                ViewState["dsDoctor"] = null;
                DataSet ff = new DataSet();

                ff = viewnoti.retailercount_mr(div_code, sf_code);
                if (ff.Tables[0].Rows.Count > 0)
                {
                    retailer.Text = ff.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

                }
                DateTime dateTime = DateTime.UtcNow.Date;
                string todate = dateTime.ToString("yyyy-MM-dd");

                ff = viewnoti.orderdashboard(div_code, todate);
                if (ff.Tables[0].Rows.Count > 0)
                {
                    ordercount.Text = ff.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

                }
                BindData();

            }
        }
        else { Page.Response.Redirect(baseUrl, true); }
    }
    #endregion

    #region BindData
    protected void BindData()
    {
        DataSet ff = new DataSet();

        ff = viewnoti.Notification_view(div_code);

        if (ff.Tables.Count > 0)
        {
            if (ff.Tables[0].Rows.Count > 0)
            {
                DataList1.DataSource = ff;
                DataList1.DataBind();
            }
        }
    }
    #endregion

    #region Item_Bound
    protected void Item_Bound(Object sender, DataListItemEventArgs e)
    {
        foreach (DataListItem item in DataList1.Items)
        {

            day = (item.FindControl("daytime") as Label).Text;
            type = (item.FindControl("cmttype") as Label).Text;
            comment = (item.FindControl("lblInput") as Label).Text;
            if (type == "News")
            {
                item.BackColor = System.Drawing.Color.LightYellow;
            }
            if (type == "Wishes")
            {
                item.BackColor = System.Drawing.Color.FromName("#d9edf7");
            }
            if (type == "Messages")
            {
                item.BackColor = System.Drawing.Color.FromName("#fad5d5");
            }
            if (type == "Important")
            {
                item.BackColor = System.Drawing.Color.FromName("#cffabd");
            }
        }
    }
    #endregion

    #region getsto()
    public void getsto()
    {
        SalesForce sf = new SalesForce();
        dsSalesForce = sf.getsto(sf_code, div_code);
        foreach (DataRow drFF in dsSalesForce.Tables[0].Rows)
        {
            sto_code = drFF[0].ToString();
            //Fillcate();
        }
    }
    #endregion

    #region Fillcate()
    private string Fillcate()
    {
        string sURL = string.Empty;
        string stCrtDtaPnt = string.Empty;
        //Year = viewdrop.SelectedItem.ToString();
        SalesForce sf = new SalesForce();
        dsSalesForce = sf.Gettop10value_category(div_code);
        foreach (DataRow drFF in dsSalesForce.Tables[0].Rows)
        {
            iTotLstCount1 = drFF["value"].ToString();

            stCrtDtaPnt += "{label:\"" + drFF["Product_Cat_Name"].ToString() + "\",y: ";
            stCrtDtaPnt += Convert.ToString(iTotLstCount1) + "},";
        }
        return stCrtDtaPnt;
    }
    #endregion

    #region Fillbrand()
    private string Fillbrand()
    {
        string sURL = string.Empty;
        string stCrtDtaPnt1 = string.Empty;

        SalesForce sf = new SalesForce();
        dsSalesForce = sf.Gettop10value_Brand(div_code);
        foreach (DataRow drFF in dsSalesForce.Tables[0].Rows)
        {
            iTotLstCount2 = drFF["value"].ToString();
            stCrtDtaPnt1 += "{label:\"" + drFF["Product_Brd_Name"].ToString() + "\",y:";
            stCrtDtaPnt1 += Convert.ToString(iTotLstCount2) + "},";
        }
        return stCrtDtaPnt1;
    }
    #endregion

    #region Fillpro()
    private string Fillpro()
    {
        string sURL = string.Empty;
        string stCrtDtaPnt2 = string.Empty;

        SalesForce sf = new SalesForce();
        dsSalesForce = sf.Gettop10value_Product(div_code);
        foreach (DataRow drFF in dsSalesForce.Tables[0].Rows)
        {
            iTotLstCount3 = drFF["value"].ToString();
            stCrtDtaPnt2 += "{label:\"" + drFF["Product_Detail_Name"].ToString() + "\",y:";
            stCrtDtaPnt2 += Convert.ToString(iTotLstCount3) + "},";
        }
        return stCrtDtaPnt2;
    }
    #endregion

    #region saleFillcate()
    private string saleFillcate()
    {
        string sURL = string.Empty;
        string stCrtDtaPnts1 = string.Empty;

        SalesForce sf = new SalesForce();
        dsSalesForce = sf.Sales_Gettop10value_category(div_code);
        foreach (DataRow drFF in dsSalesForce.Tables[0].Rows)
        {
            iTotLstCounts1 = drFF["value"].ToString();

            stCrtDtaPnts1 += "{label:\"" + drFF["Product_Cat_Name"].ToString() + "\",y: ";
            stCrtDtaPnts1 += Convert.ToString(iTotLstCounts1) + "},";
        }
        return stCrtDtaPnts1;
    }
    #endregion

    #region saleFillbrand()
    private string saleFillbrand()
    {
        string sURL = string.Empty;
        string stCrtDtaPnts2 = string.Empty;

        SalesForce sf = new SalesForce();
        dsSalesForce = sf.Sales_Gettop10value_Brand(div_code);
        foreach (DataRow drFF in dsSalesForce.Tables[0].Rows)
        {
            iTotLstCounts2 = drFF["value"].ToString();
            stCrtDtaPnts2 += "{label:\"" + drFF["Product_Brd_Name"].ToString() + "\",y:";
            stCrtDtaPnts2 += Convert.ToString(iTotLstCounts2) + "},";
        }
        return stCrtDtaPnts2;
    }
    #endregion

    #region saleFillpro()
    private string saleFillpro()
    {
        string sURL = string.Empty;
        string stCrtDtaPnts3 = string.Empty;

        SalesForce sf = new SalesForce();
        dsSalesForce = sf.Sales_Gettop10value_Product(div_code);
        foreach (DataRow drFF in dsSalesForce.Tables[0].Rows)
        {
            iTotLstCounts3 = drFF["value"].ToString();
            stCrtDtaPnts3 += "{label:\"" + drFF["Product_Detail_Name"].ToString() + "\",y:";
            stCrtDtaPnts3 += Convert.ToString(iTotLstCounts3) + "},";
        }
        return stCrtDtaPnts3;
    }
    #endregion

    #region RetailFillcate()
    private string RetailFillcate()
    {
        string sURL = string.Empty;
        string stCrtDtaPnts1 = string.Empty;

        SalesForce sf = new SalesForce();
        dsSalesForce = sf.Sales_Gettop10value_category(div_code);
        foreach (DataRow drFF in dsSalesForce.Tables[0].Rows)
        {
            iTotLstCounts1 = drFF["value"].ToString();

            stCrtDtaPnts1 += "{label:\"" + drFF["Product_Cat_Name"].ToString() + "\",y: ";
            stCrtDtaPnts1 += Convert.ToString(iTotLstCounts1) + "},";
        }
        return stCrtDtaPnts1;
    }
    #endregion

    #region RetailFillbrand()
    private string RetailFillbrand()
    {
        string sURL = string.Empty;
        string stCrtDtaPnts2 = string.Empty;

        SalesForce sf = new SalesForce();
        dsSalesForce = sf.Sales_Gettop10value_Brand(div_code);
        foreach (DataRow drFF in dsSalesForce.Tables[0].Rows)
        {
            iTotLstCounts2 = drFF["value"].ToString();
            stCrtDtaPnts2 += "{label:\"" + drFF["Product_Brd_Name"].ToString() + "\",y:";
            stCrtDtaPnts2 += Convert.ToString(iTotLstCounts2) + "},";
        }
        return stCrtDtaPnts2;
    }
    #endregion

    #region RetailFillpro()
    private string RetailFillpro()
    {
        string sURL = string.Empty;
        string stCrtDtaPnts3 = string.Empty;

        SalesForce sf = new SalesForce();
        dsSalesForce = sf.Sales_Gettop10value_Product(div_code);
        foreach (DataRow drFF in dsSalesForce.Tables[0].Rows)
        {
            iTotLstCounts3 = drFF["value"].ToString();
            stCrtDtaPnts3 += "{label:\"" + drFF["Product_Detail_Name"].ToString() + "\",y:";
            stCrtDtaPnts3 += Convert.ToString(iTotLstCounts3) + "},";
        }
        return stCrtDtaPnts3;
    }
    #endregion

}