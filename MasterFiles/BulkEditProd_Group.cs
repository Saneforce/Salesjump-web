using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
public partial class MasterFiles_BulkEditProd_Group : System.Web.UI.Page
{
    #region "Declaration"
    DataSet dsProGrp = null;
    int i = 0;
    int ProGrpCode = 0;
    string div_code = string.Empty;
    string divcode = string.Empty;
    string Product_Grp_SName = string.Empty;
    string ProGrpName = string.Empty;
    string Product_Grp_Name = string.Empty;
    string Product_Grp_Code = string.Empty;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int time;
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        if (!Page.IsPostBack)
        {
            FillProGrp();
            Session["backurl"] = "ProductGroupList.aspx";
            menu1.Title = this.Page.Title;
            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);
        }
    }
    protected override void OnLoadComplete(EventArgs e)
    {

        ServerEndTime = DateTime.Now;
        TrackPageTime();//It will give you page load time  
    }
    public void TrackPageTime()
    {
        TimeSpan serverTimeDiff = ServerEndTime.Subtract(ServerStartTime);
        time = serverTimeDiff.Minutes;

    } 
    private void FillProGrp()
    {
        Product dv = new Product();
        dsProGrp = dv.getProGrp(div_code);
        if (dsProGrp.Tables[0].Rows.Count > 0)
        {
            btnSubmit.Visible = true;
            grdProGrp.Visible = true;
            grdProGrp.DataSource = dsProGrp;
            grdProGrp.DataBind();
        }
        else
        {
            grdProGrp.DataSource = dsProGrp;
            grdProGrp.DataBind();
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        Product dv = new Product();
        int iReturn = -1;
        bool err = false;
         
        foreach (GridViewRow gridRow in grdProGrp.Rows)
        {
            Label lblProGrpCode = (Label)gridRow.Cells[1].FindControl("lblProGrpCode");
            //   Product_Grp_Code = lblProGrpCode.Text.ToString();
            ProGrpCode = Convert.ToInt16(lblProGrpCode.Text);
            TextBox txtProduct_Grp_SName = (TextBox)gridRow.Cells[1].FindControl("txtProduct_Grp_SName");
            Product_Grp_SName = txtProduct_Grp_SName.Text.ToString();
            TextBox txtProGrpName = (TextBox)gridRow.Cells[1].FindControl("txtProGrpName");
            Product_Grp_Name = txtProGrpName.Text.ToString();
            // iReturn = dv.RecordUpdateGrp(Convert.ToInt16(Product_Grp_Code), Product_Grp_SName, Product_Grp_Name);
            iReturn = dv.RecordUpdateGrp(ProGrpCode, Product_Grp_SName, Product_Grp_Name, div_code);

            if (iReturn > 0)
                err = false;

            if ((iReturn == -2))
            {
                txtProGrpName.Focus();
                err = true;
                break;
            }

            if((iReturn == -3))
            {
                txtProduct_Grp_SName.Focus();
                err = true;
                break;
            }
        }

        if ( err == false )
        {
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Updated Successfully');window.location='ProductGroupList.aspx';</script>");
        }
        else if (err == true)
        {
            if (iReturn == -2)
            {
                // menu1.Status = "State/Location already Exist";
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Name Already Exist');</script>");
                //  txtStateName.Focus();
            }
            else if (iReturn == -3)
            {
                // menu1.Status = "State/Location already Exist";
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Short Name Already Exist');</script>");
                //  txtStateName.Focus();
            }
      }
       
    }

}