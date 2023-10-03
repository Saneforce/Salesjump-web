using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using Bus_EReport;

public partial class MasterFiles_ProductGroup : System.Web.UI.Page
{
#region "Declaration"
    DataSet dsPgrp = null;
    string ProdgrpCode = string.Empty;
    string divcode = string.Empty;
    string Product_Grp_SName = string.Empty;
    string ProGrpName = string.Empty;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int time;
#endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["backurl"] = "ProductGroupList.aspx";
        divcode = Convert.ToString(Session["div_code"]);
        ProdgrpCode = Request.QueryString["Product_Grp_Code"];
        txtProduct_Grp_SName.Focus();
        if (!Page.IsPostBack)
        {
            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);
            if (ProdgrpCode != "" && ProdgrpCode != null)
            {
                Product dv = new Product();
                dsPgrp = dv.getProGroup(divcode, ProdgrpCode);

                if (dsPgrp.Tables[0].Rows.Count > 0)
                {
                    txtProduct_Grp_SName.Text = dsPgrp.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                    txtProGrpName.Text = dsPgrp.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                }

            }

         
            menu1.Title = this.Page.Title;
           
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
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
         System.Threading.Thread.Sleep(time);
        Product_Grp_SName = txtProduct_Grp_SName.Text;
        ProGrpName = txtProGrpName.Text;

        if (ProdgrpCode == null)
        {
            // Add New Product Group
            Product dv = new Product();
            int iReturn = dv.RecordAddGrp(divcode, Product_Grp_SName, ProGrpName);

             if (iReturn > 0 )
            {
               // menu1.Status = "Product Group Created Successfully ";
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Created Successfully');</script>");
                Resetall();
            }
             else if (iReturn == -2)
             {
                 ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Group Name Already Exist');</script>");
                 txtProGrpName.Focus();
             }
             else if (iReturn == -3)
             {
                 ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Group Code Already Exist');</script>");
                 txtProduct_Grp_SName.Focus();
             }
        }
        else
        {
            // Update Product Group
            Product dv = new Product();
            int iReturn = dv.RecordUpdateGrp(Convert.ToInt16(ProdgrpCode), Product_Grp_SName, ProGrpName, divcode);
             if (iReturn > 0 )
            {
               // menu1.Status = "Product Group Updated Successfully ";
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Updated Successfully');window.location='ProductGroupList.aspx';</script>");
            }
            else if (iReturn == -2)
            {               
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Group Name Already Exist');</script>");
                txtProGrpName.Focus();
            }
             else if (iReturn == -3)
             {
                 ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Group Code Already Exist');</script>");
                 txtProduct_Grp_SName.Focus();
             }
        }

    }
    private void Resetall()
    {
        txtProduct_Grp_SName.Text = "";
        txtProGrpName.Text = "";
    }
  
}