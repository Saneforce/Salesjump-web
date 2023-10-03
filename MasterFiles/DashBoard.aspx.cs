using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;

public partial class DashBoard : System.Web.UI.Page
{
    DataSet dsSalesForce = null;
    DataSet dsdoc = null;
    DataSet dsDoctor = null;
    DataSet dsTP = null;
    string iPendingCount = string.Empty;
    string div_code = string.Empty;
    string sf_code = string.Empty;
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

    protected void Page_Load(object sender, EventArgs e)
    {
        
        div_code = Session["division_code"].ToString();
        div_code = div_code.Trim(",".ToCharArray());
        string scrpt = "arr=[" + Fillcate() + "];arr1=[" + Fillbrand() + "];arr2=[" + Fillpro() + "];arr3=[" + saleFillcate() + "];arr4=[" + saleFillbrand() + "];arr5=[" + saleFillpro() + "];window.onload = function () {genChart('T10brand',arr,'Purchase Top 10 Categorys');genChart('T10Cate',arr1,'Purchase Top 10 Brands');genChart('T10Pro',arr2,'Purchase Top 10 Products');genChart('saleT10Cate',arr3,'Sale Top 10 Categorys');genChart('saleT10brand',arr4,'Sale Top 10 brands');genChart('saleT10Pro',arr5,'Sale Top 10 Products');}";
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "SetupData", scrpt, true);
  
        if (!Page.IsPostBack)
        {
            FillYear();
            ViewState["dsSalesForce"] = null;
            ViewState["dsDoctor"] = null;
         
           
        }
    }
    private string Fillcate()
    {
        string sURL = string.Empty;
        string stCrtDtaPnt = string.Empty;
        //Year = viewdrop.SelectedItem.ToString();
        SalesForce sf = new SalesForce();
        dsSalesForce = sf.Gettop10value_category(div_code, DateTime.Now.Year.ToString());
        foreach (DataRow drFF in dsSalesForce.Tables[0].Rows)
        {
            iTotLstCount1 = drFF["value"].ToString();
      
            stCrtDtaPnt += "{label:\"" + drFF["Product_Cat_Name"].ToString() + "\",y: ";
            stCrtDtaPnt += Convert.ToString(iTotLstCount1) + "},";
          

        }
         return stCrtDtaPnt;
         
    }

  private string Fillbrand()
    {
        string sURL = string.Empty;
        string stCrtDtaPnt1 = string.Empty;

        SalesForce sf = new SalesForce();
        dsSalesForce = sf.Gettop10value_Brand(div_code, DateTime.Now.Year.ToString());
        foreach (DataRow drFF in dsSalesForce.Tables[0].Rows)
        {
            iTotLstCount2 = drFF["value"].ToString();
            stCrtDtaPnt1 += "{label:\"" + drFF["Product_Brd_Name"].ToString() + "\",y:";
            stCrtDtaPnt1 += Convert.ToString(iTotLstCount2) + "},";
        }
         return stCrtDtaPnt1;
    }
  private string Fillpro()
  {
      string sURL = string.Empty;
      string stCrtDtaPnt2 = string.Empty;

      SalesForce sf = new SalesForce();
      dsSalesForce = sf.Gettop10value_Product(div_code, DateTime.Now.Year.ToString());
      foreach (DataRow drFF in dsSalesForce.Tables[0].Rows)
      {
          iTotLstCount3 = drFF["value"].ToString();
          stCrtDtaPnt2 += "{label:\"" + drFF["Product_Detail_Name"].ToString() + "\",y:";
          stCrtDtaPnt2 += Convert.ToString(iTotLstCount3) + "},";
      }
      return stCrtDtaPnt2;
  }
  private string saleFillcate()
  {
      string sURL = string.Empty;
      string stCrtDtaPnts1 = string.Empty;

      SalesForce sf = new SalesForce();
      dsSalesForce = sf.Sales_Gettop10value_category(div_code, DateTime.Now.Year.ToString());
      foreach (DataRow drFF in dsSalesForce.Tables[0].Rows)
      {
          iTotLstCounts1 = drFF["value"].ToString();

          stCrtDtaPnts1 += "{label:\"" + drFF["Product_Cat_Name"].ToString() + "\",y: ";
          stCrtDtaPnts1 += Convert.ToString(iTotLstCounts1) + "},";


      }
      return stCrtDtaPnts1;

  }
  private string saleFillbrand()
  {
      string sURL = string.Empty;
      string stCrtDtaPnts2 = string.Empty;

      SalesForce sf = new SalesForce();
      dsSalesForce = sf.Sales_Gettop10value_Brand(div_code, DateTime.Now.Year.ToString());
      foreach (DataRow drFF in dsSalesForce.Tables[0].Rows)
      {
          iTotLstCounts2 = drFF["value"].ToString();
          stCrtDtaPnts2 += "{label:\"" + drFF["Product_Brd_Name"].ToString() + "\",y:";
          stCrtDtaPnts2 += Convert.ToString(iTotLstCounts2) + "},";
      }
      return stCrtDtaPnts2;
  }

  private string saleFillpro()
  {
      string sURL = string.Empty;
      string stCrtDtaPnts3 = string.Empty;

      SalesForce sf = new SalesForce();
      dsSalesForce = sf.Sales_Gettop10value_Product(div_code, DateTime.Now.Year.ToString());
      foreach (DataRow drFF in dsSalesForce.Tables[0].Rows)
      {
          iTotLstCounts3 = drFF["value"].ToString();
          stCrtDtaPnts3 += "{label:\"" + drFF["Product_Detail_Name"].ToString() + "\",y:";
          stCrtDtaPnts3 += Convert.ToString(iTotLstCounts3) + "},";
      }
      return stCrtDtaPnts3;
  }

  private string RetailFillcate()
  {
      string sURL = string.Empty;
      string stCrtDtaPnts1 = string.Empty;

      SalesForce sf = new SalesForce();
      dsSalesForce = sf.Sales_Gettop10value_category(div_code, DateTime.Now.Year.ToString());
      foreach (DataRow drFF in dsSalesForce.Tables[0].Rows)
      {
          iTotLstCounts1 = drFF["value"].ToString();

          stCrtDtaPnts1 += "{label:\"" + drFF["Product_Cat_Name"].ToString() + "\",y: ";
          stCrtDtaPnts1 += Convert.ToString(iTotLstCounts1) + "},";


      }
      return stCrtDtaPnts1;

  }
  private string RetailFillbrand()
  {
      string sURL = string.Empty;
      string stCrtDtaPnts2 = string.Empty;

      SalesForce sf = new SalesForce();
      dsSalesForce = sf.Sales_Gettop10value_Brand(div_code, DateTime.Now.Year.ToString());
      foreach (DataRow drFF in dsSalesForce.Tables[0].Rows)
      {
          iTotLstCounts2 = drFF["value"].ToString();
          stCrtDtaPnts2 += "{label:\"" + drFF["Product_Brd_Name"].ToString() + "\",y:";
          stCrtDtaPnts2 += Convert.ToString(iTotLstCounts2) + "},";
      }
      return stCrtDtaPnts2;
  }

  private string RetailFillpro()
  {
      string sURL = string.Empty;
      string stCrtDtaPnts3 = string.Empty;

      SalesForce sf = new SalesForce();
      dsSalesForce = sf.Sales_Gettop10value_Product(div_code, DateTime.Now.Year.ToString());
      foreach (DataRow drFF in dsSalesForce.Tables[0].Rows)
      {
          iTotLstCounts3 = drFF["value"].ToString();
          stCrtDtaPnts3 += "{label:\"" + drFF["Product_Detail_Name"].ToString() + "\",y:";
          stCrtDtaPnts3 += Convert.ToString(iTotLstCounts3) + "},";
      }
      return stCrtDtaPnts3;
  }

  private void FillYear()
  {
      TourPlan tp = new TourPlan();
      dsTP = tp.Get_TP_Edit_Year(div_code);
      if (dsTP.Tables[0].Rows.Count > 0)
      {
          for (int k = Convert.ToInt16(dsTP.Tables[0].Rows[0]["Year"]); k <= DateTime.Now.Year + 1; k++)
          {
              viewdrop.Items.Add(k.ToString());
              viewdrop.SelectedValue = DateTime.Now.Year.ToString();
          }
      }

     
  }

  [WebMethod(EnableSession = true)]
    public static string Get_access_master()
    {
        DataSet ds = new DataSet();
        string div_code = HttpContext.Current.Session["div_code"].ToString();
        StockistMaster sm = new StockistMaster();
        ds = sm.get_access_master_details(div_code);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }

  
}
