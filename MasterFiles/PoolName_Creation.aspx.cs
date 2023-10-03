using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using Bus_EReport;
public partial class MasterFiles_PoolName_Creation : System.Web.UI.Page
{
    DataSet dsStockist = null;
    string Pool_Id = string.Empty;
    string divcode = string.Empty;
    string Pool_sname = string.Empty;
    string Pool_name = string.Empty;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int time;
    protected void Page_Load(object sender, EventArgs e)
    {
        divcode = Convert.ToString(Session["div_code"]);
        Session["backurl"] = "PoolName_List.aspx";
        Pool_Id = Request.QueryString["Pool_Id"];
        if (!Page.IsPostBack)
        {
            txtPool_Sname.Focus();
            menu1.Title = this.Page.Title;
            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);
            if (Pool_Id != "" && Pool_Id != null)
            {
               
                Stockist sk = new Stockist();
                dsStockist = sk.getPoolName(divcode, Pool_Id);
                if (dsStockist.Tables[0].Rows.Count > 0)
                {
                    txtPool_Sname.Text = dsStockist.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                    txtPool_Name.Text = dsStockist.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                }
            }
        
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        Pool_sname = txtPool_Sname.Text;
        Pool_name = txtPool_Name.Text;
        if (Pool_Id == null)
        {
            Stockist Sk = new Stockist();
            int iReturn = Sk.RecordAdd_Pool(divcode, Pool_sname, Pool_name);
            if(iReturn > 0)
            {                
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Created Successfully');</script>");
                Resetall();
            }
            
        }
    }
    private void Resetall()
    {
        txtPool_Sname.Text = "";
        txtPool_Name.Text = "";
    }
  
}