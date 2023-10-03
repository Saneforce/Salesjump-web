using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Data.SqlClient;

using Bus_EReport;

public partial class DCR_DCR_Entry : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            DtInf.InnerHtml = "Anbazhagan J";
            
            DCR dcr=new DCR();
            DataSet dsWType=dcr.DCR_get_WorkType();

            ddl_WorkType.Focus();
            ddl_WorkType.DataSource=dsWType;
            ddl_WorkType.DataTextField = "Worktype_Name_B";
            ddl_WorkType.DataValueField = "WorkType_Code_B";
            ddl_WorkType.DataBind();
            ddl_WorkType.Items.Insert(0,new ListItem("--Select the Work Type--", ""));
             //tbD.Rows[0].Cells[0].Visible = false;
           // UserData data = JsonConvert.DeserializeObject<UserData>(jsonData);

        }
    }
    
}