using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;

public partial class ErrorPage : System.Web.UI.Page
{
    string sErr =string.Empty;
    string sUrl = string.Empty;
    string div_code = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        //Get the error description from Application Error event thru Global
        if (Session["sError"] != null)
            sErr = Session["sError"].ToString();

        //Get the screen details from Application Error event thru Global
        if (Session["sUrl"] != null)
            sUrl = Session["sUrl"].ToString();
        
        if(Session["div_code"] != null)
            div_code = Session["div_code"].ToString();

        ErrorLog err = new ErrorLog();
        int iErrReturn = err.LogError(Convert.ToInt16( div_code), sErr , sUrl, sErr);
    }
}