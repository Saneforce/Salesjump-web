using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bus_EReport;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Text.RegularExpressions;

public partial class Master : System.Web.UI.MasterPage
{

    public string div_code = string.Empty;
    string allliValues = string.Empty;
    DataSet dsListeddr = null;
    string sState = string.Empty;
    string Menu_code = string.Empty;
    string state_cd = string.Empty;
    string[] statecd;
    string d;
    int i;
    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["division_code"].ToString();
        string hh = Session["Menu_type"].ToString();
        //string strSearch = Request["DummyUsername"];
        string ss = Session["HO_ID"].ToString(); 
        Label1.Text = Session["Title_Admin"].ToString();
        CompNm.Text = Session["Corporate"].ToString();
        string url = HttpContext.Current.Request.Url.AbsoluteUri.Replace("http://", "");
        string[] words = url.Split('.');
        string shortna = words[0];
        if (shortna == "www") shortna = words[1];
        if (Session["CmpIDKey"] != null && Session["CmpIDKey"].ToString() != "") { shortna = Session["CmpIDKey"].ToString(); }
        string filename = shortna + "_logo.png";
        string dynamicFolderPath = "~/limg/";//which used to create                                       dynamic folder
        string path = dynamicFolderPath + filename.ToString();
        logoo.ImageUrl = path;

        if (hh == "h")
        {
            UserLogin dv = new UserLogin();
            dsListeddr = dv.Process_Menu(div_code, ss);
            if (dsListeddr.Tables[0].Rows.Count > 0)
            {
                state_cd = string.Empty;
                state_cd = dsListeddr.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                statecd = state_cd.Split(',');

                foreach (string st_cd in statecd)
                {
                    d = st_cd.ToString();
                    fuc();
                }


            }




        }
        else
        {
            //Li_1.Visible = true;
        }



    }

    public void fuc()
    {

        if (i == 0)
        {
            Control MyList = FindControl("MyList");

            foreach (Control MyControl in MyList.Controls)
            {
                if (MyControl is HtmlGenericControl)
                {
                    HtmlGenericControl li = MyControl as HtmlGenericControl;

                    string resultString = Regex.Match(li.ID, @"\d+").Value;
                    if (resultString == d)
                    {
                        li.Visible = false;
                    }
                    else
                    {

                    }
                }
            }
        }
        else
        {

        }


    }

}