using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;

public partial class MasterFiles_UserList : System.Web.UI.Page
{
    
    DataSet dsSalesForce = null;
    DataSet dsDivision = null;
    DataSet dsSF = null;
    DataSet dsState = null;
    string sState = string.Empty;
    string div_code = string.Empty;
    string[] statecd;
    string state_cd = string.Empty;
    string sf_code = string.Empty;
    string sf_name = string.Empty;
    string usr_name = string.Empty;
    int state = -1;
    string hq = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        if (!Page.IsPostBack)
        {
            menu1.Title = this.Page.Title;
            CommonLoad();
        }
    }

    #region Method

    private void CommonLoad()
    {
        PopulateTree(trvuser);
    }

    private void PopulateTree(TreeView objTreeView)
    {
        
        SalesForce sf = new SalesForce();
        dsSF  = sf.SF_Hierarchy(div_code,"admin");
        if (dsSF.Tables[0].Rows.Count > 0)
        {
            foreach (DataRow dataRow in dsSF.Tables[0].Rows)
            {                
                TreeNode treeRoot = new TreeNode();
                treeRoot.Text = "<font style='color:maroon; background-color:violet'><strong>" + dataRow["Sf_Name"].ToString() + "</strong></font>"; 
                treeRoot.Value = dataRow["sf_code"].ToString();
                //treeRoot.ImageUrl = "../Images/LOOKUP.gif";
                
                treeRoot.ExpandAll();
                objTreeView.Nodes.Add(treeRoot);

                foreach (TreeNode childnode in GetChildNode(dataRow["sf_code"].ToString()))
                {                        
                    treeRoot.ChildNodes.Add(childnode);
                }                
            }
        }
    }

    private TreeNodeCollection GetChildNode(string sf_code)    
    {
        SalesForce sf = new SalesForce();
        dsSalesForce = sf.SF_Hierarchy(div_code, sf_code);

        TreeNodeCollection childtreenodes = new TreeNodeCollection();
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            foreach (DataRow dataRow in dsSalesForce.Tables[0].Rows)
            {
                TreeNode childNode = new TreeNode();
                if (dataRow["sf_Type"].ToString() == "1")
                {
                    childNode.Text = "<font style='color:blue; background-color:aliceblue'><strong>" + dataRow["Sf_Name"].ToString() + "</strong></font>";
                }
                else
                {
                    childNode.Text = "<font style='color:DarkGreen; background-color:LightGreen'><strong>" + dataRow["Sf_Name"].ToString() + "</strong></font>";
                }
                childNode.Value = dataRow["sf_code"].ToString();
                
                //childNode.ImageUrl = "../Images/arr_menu.gif";
                childNode.ExpandAll();

                foreach (TreeNode cnode in GetChildNode(dataRow["sf_code"].ToString()))
                {
                    childNode.ChildNodes.Add(cnode);
                }
                childtreenodes.Add(childNode);
            }

        }

        return childtreenodes;
    }

   
    #endregion

}