using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
using System.IO;
using System.Net;
public partial class MasterFiles_Options_FileUpload_Category : System.Web.UI.Page
{
    DataSet dsSalesForce = null;
    string div_code = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        if (!Page.IsPostBack)
        {
            menu1.Title = this.Page.Title;
            menu1.FindControl("btnBack").Visible = false;            
        }
    }
}