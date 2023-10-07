using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
using System.Web.Services;

/// <summary>
/// Summary description for WebService
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]

[System.ComponentModel.ToolboxItem(false)]
[System.Web.Script.Services.ScriptService]

public class WebService : System.Web.Services.WebService
{
    string div_code = string.Empty;
    string sf_code = string.Empty;

    public WebService()
    {
        //  div_code = Session["div_code"].ToString();
        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }


    [WebMethod(EnableSession = true)]
    public string[] AutoCompleteAjaxRequest(string prefixText, int count)
    {

        div_code = Session["div_code"].ToString();
        sf_code = Session["sf_code"].ToString();

        DataSet dsListedDR = new DataSet();
        ListedDR lstDR = new ListedDR();
        List<string> ajaxDataCollection = new List<string>();
        DataTable _objdt = new DataTable();


        dsListedDR = lstDR.GetListedDoctorAutoFill(prefixText, div_code, sf_code);
        if (dsListedDR.Tables[0].Rows.Count > 0)
        {
            for (int i = 0; i < dsListedDR.Tables[0].Rows.Count; i++)
            {
                ajaxDataCollection.Add(dsListedDR.Tables[0].Rows[i]["ListedDr_Name"].ToString());
            }
        }
        return ajaxDataCollection.ToArray();
    }

    [WebMethod]
    public string[] AutoCompleteAjaxUnlistedDr_Name(string prefixText, int count)
    {
        DataSet dsListedDR = new DataSet();
        UnListedDR UnlstDR = new UnListedDR();
        List<string> ajaxDataCollection = new List<string>();
        DataTable _objdt = new DataTable();
        dsListedDR = UnlstDR.GetNameFromDataBase(prefixText);
        if (dsListedDR.Tables[0].Rows.Count > 0)
        {
            for (int i = 0; i < dsListedDR.Tables[0].Rows.Count; i++)
            {
                ajaxDataCollection.Add(dsListedDR.Tables[0].Rows[i]["UnListedDr_Name"].ToString());
            }
        }
        return ajaxDataCollection.ToArray();
    }


}


