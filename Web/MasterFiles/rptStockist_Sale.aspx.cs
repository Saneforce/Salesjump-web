using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
public partial class MasterFiles_rptStockist_Sale : System.Web.UI.Page
{
    string div_code = string.Empty;
    string sf_code = string.Empty;
    string imonth = string.Empty;
    string iyear = string.Empty;   
    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        sf_code = Request.QueryString["sf_code"].ToString();
    //    iMonth = Request.QueryString["cur_month"].ToString();
      //  iYear = Request.QueryString["cur_year"].ToString();
      //  lblHead.text= lblHead.Text+"iMonth" +for+""+
        //string month = getMonthName();
     //   lblHead.Text = lblHead.Text + "iMonth" + "iyear";
      //  string sMonth = getMonthName(iMonth) + " - " + iYear.ToString();
        lblHead.Text = lblHead.Text ;
    }
    

    private string getMonthName(int iMonth)
    {
        string sReturn = string.Empty;

        if (iMonth == 1)
        {
            sReturn = "January";
        }
        else if (iMonth == 2)
        {
            sReturn = "February";
        }
        else if (iMonth == 3)
        {
            sReturn = "March";
        }
        else if (iMonth == 4)
        {
            sReturn = "April";
        }
        else if (iMonth == 5)
        {
            sReturn = "May";
        }
        else if (iMonth == 6)
        {
            sReturn = "June";
        }
        else if (iMonth == 7)
        {
            sReturn = "July";
        }
        else if (iMonth == 8)
        {
            sReturn = "August";
        }
        else if (iMonth == 9)
        {
            sReturn = "September";
        }
        else if (iMonth == 10)
        {
            sReturn = "October";
        }
        else if (iMonth == 11)
        {
            sReturn = "November";
        }
        else if (iMonth == 12)
        {
            sReturn = "December";
        }
        return sReturn;
    }

}