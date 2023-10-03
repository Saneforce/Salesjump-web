using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;

public partial class Count : System.Web.UI.Page
{
    public int count;
    public DataSet dsStockist;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            count = Convert.ToInt32(Request.QueryString["count"].ToString());
            Bind(count);
        }

    }
    public void Bind(int count)
    {
        DSM sk = new DSM();
        dsStockist = sk.DSM_Count(count);
        if (dsStockist.Tables[0].Rows.Count > 0)
        {
            grdStockist.Visible = true;
            grdStockist.DataSource = dsStockist;
            grdStockist.DataBind();
        }
 
    }

}
