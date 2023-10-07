
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bus_EReport;
using System.Data;
using System.Web.Services;

public partial class Master : System.Web.UI.MasterPage
{

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            Label1.Text = Session["Title_MGR"].ToString();
        }
        catch
        {
            Label1.Text = Session["sf_Name"].ToString();
        }
        string url = HttpContext.Current.Request.Url.AbsoluteUri.Replace("http://", "");

        string[] words = url.Split('.');
        string shortna = words[0];
        if (shortna == "www") shortna = words[1];
        string filename = shortna + "_logo.png";
        string dynamicFolderPath = "~/limg/";    //which used to create dynamic folder
        string path = dynamicFolderPath + filename.ToString();
        logoo.ImageUrl = path;

    }

    [WebMethod(EnableSession = true)]
    public static Details[] display(string divCode, string sf_code)
    {
        MenuCreation mc1 = new MenuCreation();
        DataTable dt1 = new DataTable();
        dt1 = mc1.getMenuByUser(divCode, sf_code);
        List<Details> details = new List<Details>();
        foreach (DataRow row1 in dt1.Rows)
        {
            Details dt = new Details();
            dt.Menu_ID = Convert.ToInt32(row1["Menu_ID"]);
            dt.Menu_Name = row1["Menu_Name"].ToString();
            dt.Menu_Type = row1["Menu_Type"].ToString();
            dt.Parent_Menu = row1["Parent_Menu"].ToString();
            //dt.MMnu = row1["MMnu"].ToString();
            //dt.lvl = row1["lvl"].ToString();
            details.Add(dt);

        }

        return details.ToArray();
    }
    public class Details
    {
        public int Menu_ID { get; set; }
        public string Menu_Name { get; set; }
        public string Menu_Type { get; set; }
        public string Parent_Menu { get; set; }
        public string lvl { get; set; }
        public string MMnu { get; set; }
    }
}

