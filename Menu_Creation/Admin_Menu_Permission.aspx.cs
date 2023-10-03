using Bus_EReport;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;

public partial class Menu_Creation_Admin_Menu_Permission : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    [WebMethod(EnableSession = true)]
    public static List<ListItem> bindadmn(string comnm)
    {
        MenuCreation mc = new MenuCreation();
        List<ListItem> adminnm = new List<ListItem>();
        DataSet ds = new DataSet();
        ds = mc.getadminName_mashoid(comnm);
        foreach (DataRow row in ds.Tables[0].Rows)
        {
            adminnm.Add(new ListItem
            {
                Value = row["HO_ID"].ToString(),
                Text = row["User_Name"].ToString(),

            });

        }
        return adminnm;
    }
    [WebMethod(EnableSession = true)]
    public static string findmenuids(string adname)
    {
        MenuCreation mc1 = new MenuCreation();


        DataSet dsProd = mc1.admingetselids(adname);
        return JsonConvert.SerializeObject(dsProd.Tables[0]);
    }
    [WebMethod(EnableSession = true)]
    public static List<ListItem> bindcomdrop()
    {
        MenuCreation mc = new MenuCreation();
        List<ListItem> comname = new List<ListItem>();
        DataSet ds = new DataSet();
        ds = mc.getComName();
        foreach (DataRow row in ds.Tables[0].Rows)
        {
            comname.Add(new ListItem
            {
                Value = row["Division_Code"].ToString(),
                Text = row["Division_Name"].ToString(),

            });

        }
        return comname;
    }
    [WebMethod(EnableSession = true)]
    public static int savedata(int adid, string adname, string arr)
    {
        int getreturn;
        MenuCreation mc = new MenuCreation();
        getreturn = mc.AddMenuPermissionValues_mashoid(adid, adname, arr);

        if (getreturn > 0)
        {

        }

        return getreturn;

    }

}