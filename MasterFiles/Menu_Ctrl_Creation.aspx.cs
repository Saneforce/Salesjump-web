using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using Bus_EReport;
using System.Drawing;
using System.Data.SqlClient;
public partial class MasterFiles_Menu_Ctrl_Creation : System.Web.UI.Page
{

    #region "Declaration"
    DataSet dsDivision = null;
    string div_code = string.Empty;
    string div_name = string.Empty;
    string div_addr1 = string.Empty;
    string div_addr2 = string.Empty;
    string div_city = string.Empty;
    string div_pin = string.Empty;
    string div_state = string.Empty;
    string div_sname = string.Empty;
    string div_alias = string.Empty;
    string state_code = string.Empty;
    string sChkLocation = string.Empty;
    string div_year = string.Empty;
    string div_weekoff = string.Empty;
    string sf_type = string.Empty;
    string strChkWeekOffValue = string.Empty;
    int iIndex = -1;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int time;
    #endregion
    string connString = Globals.ConnString;
   
    protected void Page_Load(object sender, EventArgs e)
    {
        sf_type = Session["sf_type"].ToString();
        pnlNew.Visible =true;
        ddl_div.Focus();
        if (!Page.IsPostBack)
        {
            PopulateTreeview();
            PopulateTreeview1();
            FillDivision();
            //FillDesignation();
        }
    }
    protected DataSet FillDivision()
    {
        Division tp = new Division();

        dsDivision = tp.getDivision_Name_all();
        if (dsDivision.Tables[0].Rows.Count > 0)
        {
            ddl_div.DataTextField = "Division_Name";
            ddl_div.DataValueField = "Division_Code";
            ddl_div.DataSource = dsDivision;
            ddl_div.DataBind();
            ddl_div.Items.Insert(0, new ListItem("---Select---", "0"));
        }
        else
        {
            
        }
        return dsDivision;
    }
    protected DataSet FillDesignation()
    {
        Division tp = new Division();

        dsDivision = tp.getDivision_Name_By_Design(ddl_div.SelectedValue);
        if (dsDivision.Tables[0].Rows.Count > 0)
        {
            ddl_Desig.DataTextField = "Designation_Name";
            ddl_Desig.DataValueField = "Designation_Code";
            ddl_Desig.DataSource = dsDivision;
            ddl_Desig.DataBind();
            //ddl_div.Items.Insert(0, new ListItem("---Select---", "0"));
        }
        else
        {

        }
        return dsDivision;
    }
    private void PopulateTreeview()
    {
        //try
        //{
        //    DataSet ds = new DataSet();
        //    DataTable dtparent = new DataTable();
        //    DataTable dtchild = new DataTable();
        //    dtparent = FillParentTable();
        //    dtparent.TableName = "A";
        //    dtchild = FillChildTable();
        //    dtchild.TableName = "B";

        //    ds.Tables.Add(dtparent);
        //    ds.Tables.Add(dtchild);

        //    ds.Relations.Add("children", dtparent.Columns["Menu_ID"], dtchild.
        //                                                   Columns["Menu_ID"]);

        //    if (ds.Tables[0].Rows.Count > 0)
        //    {
        //        TreeView2.Nodes.Clear();

        //        foreach (DataRow masterRow in ds.Tables[0].Rows)
        //        {
        //            TreeNode masterNode = new TreeNode((string)masterRow["Menu_Name"],
        //                                 Convert.ToString(masterRow["Menu_ID"]));
        //            TreeView2.Nodes.Add(masterNode);
        //            TreeView2.CollapseAll();
                    
        //            foreach (DataRow childRow in masterRow.GetChildRows("Children"))
        //            {
        //                TreeNode childNode = new TreeNode((string)childRow["Menu_Name"],
        //                                       Convert.ToString(childRow["Parent_ID"]));
        //                masterNode.ChildNodes.Add(childNode);
        //                childNode.Value = Convert.ToString(childRow["Parent_ID"]);
        //            }
        //        }
        //    }
        //}
        //catch (Exception ex)
        //{
        //    throw new Exception("Unable to populate treeview" + ex.Message);
        //}

        tvTables_Menu.Nodes.Add(new TreeNode("Fruits", "Fruits"));
        tvTables_Menu.Nodes[0].ChildNodes.Add(new TreeNode("Mango", "Mango"));
        tvTables_Menu.Nodes[0].ChildNodes.Add(new TreeNode("Apple", "Apple"));
        tvTables_Menu.Nodes[0].ChildNodes.Add(new TreeNode("Pineapple", "Pineapple"));
        tvTables_Menu.Nodes[0].ChildNodes.Add(new TreeNode("Orange", "Orange"));
        tvTables_Menu.Nodes[0].ChildNodes.Add(new TreeNode("Grapes", "Grapes"));

        tvTables_Menu.Nodes.Add(new TreeNode("Vegetables", "Vegetables"));
        tvTables_Menu.Nodes[1].ChildNodes.Add(new TreeNode("Carrot", "Carrot"));
        tvTables_Menu.Nodes[1].ChildNodes.Add(new TreeNode("Cauliflower", "Cauliflower"));
        tvTables_Menu.Nodes[1].ChildNodes.Add(new TreeNode("Potato", "Potato"));
        tvTables_Menu.Nodes[1].ChildNodes.Add(new TreeNode("Tomato", "Tomato"));
        tvTables_Menu.Nodes[1].ChildNodes.Add(new TreeNode("Onion", "Onion"));
    }

    private DataTable FillParentTable()
    {
        SqlConnection conn = new SqlConnection(connString);  
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        DataTable dtnew = new DataTable();
        conn.Open();
        string cmdstr = "Select * from Mas_Menu_Details";
        SqlCommand cmd = new SqlCommand(cmdstr, conn);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        adp.Fill(ds);
        cmd.ExecuteNonQuery();
        dt = ds.Tables[0];
        conn.Close();
        dtnew = dt.Copy();

        return dtnew;
    }

    private DataTable FillChildTable()
    {
        SqlConnection conn = new SqlConnection(connString);  
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        DataTable dtnew = new DataTable();
        conn.Open();
        string cmdstr = "Select * from Mas_Menu_Details";
        SqlCommand cmd = new SqlCommand(cmdstr, conn);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        adp.Fill(ds);
        cmd.ExecuteNonQuery();
        dt = ds.Tables[0];
        conn.Close();
        dtnew = dt.Copy();
        return dtnew;
    }

    private void PopulateTreeview1()
    {
        try
        {
            DataSet ds = new DataSet();
            DataTable dtparent = new DataTable();
            DataTable dtchild = new DataTable();
            dtparent = FillParentTable1();
            dtparent.TableName = "A";
            dtchild = FillChildTable1();
            dtchild.TableName = "B";

            ds.Tables.Add(dtparent);
            ds.Tables.Add(dtchild);

            ds.Relations.Add("children", dtparent.Columns["M_Menu_Id"], dtchild.
                                                           Columns["M_Menu_Id"]);

            if (ds.Tables[0].Rows.Count > 0)
            {
                TreeView3.Nodes.Clear();

                foreach (DataRow masterRow in ds.Tables[0].Rows)
                {
                    TreeNode masterNode = new TreeNode((string)masterRow["M_Menu_Name"],
                                         Convert.ToString(masterRow["M_Menu_Id"]));
                    TreeView3.Nodes.Add(masterNode);
                    TreeView3.CollapseAll();

                    foreach (DataRow childRow in masterRow.GetChildRows("Children"))
                    {
                        TreeNode childNode = new TreeNode((string)childRow["S_Menu_Name"],
                                               Convert.ToString(childRow["S_Menu_id"]));
                        masterNode.ChildNodes.Add(childNode);
                        childNode.Value = Convert.ToString(childRow["S_Menu_id"]);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            throw new Exception("Unable to populate treeview" + ex.Message);
        }
    }

    private DataTable FillParentTable1()
    {
        SqlConnection conn = new SqlConnection(connString);
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        DataTable dtnew = new DataTable();
        conn.Open();
        string cmdstr = "Select * from Mas_Activities_MMenu";
        SqlCommand cmd = new SqlCommand(cmdstr, conn);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        adp.Fill(ds);
        cmd.ExecuteNonQuery();
        dt = ds.Tables[0];
        conn.Close();
        dtnew = dt.Copy();

        return dtnew;
    }

    private DataTable FillChildTable1()
    {
        SqlConnection conn = new SqlConnection(connString);
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        DataTable dtnew = new DataTable();
        conn.Open();
        string cmdstr = "Select * from Mas_Activities_SMenu";
        SqlCommand cmd = new SqlCommand(cmdstr, conn);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        adp.Fill(ds);
        cmd.ExecuteNonQuery();
        dt = ds.Tables[0];
        conn.Close();
        dtnew = dt.Copy();
        return dtnew;
    }     
   
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        
       

       // strChkWeekOffValue = Session["Value"].ToString();
        if (div_code == null)
        {
            string Div_name = ddl_div.SelectedItem.ToString();
            string Dig = ddl_Desig.SelectedItem.ToString();
            string node=TreeView2.SelectedValue.ToString();
            string value = TreeView2.SelectedNode.Value;
           // strChkWeekOffValue = Session["Value"].ToString();
            // Add New Division
            Division dv = new Division();
            int iReturn = 1;//dv.RecordAdd(div_name, div_addr1, div_addr2, div_city, div_pin, sChkLocation, div_sname, div_alias, div_year, strChkWeekOffValue);

            if (iReturn > 0)
            {
            
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Created Successfully');</script>");
                Resetall();
            }
            else if (iReturn == -2)
            {
              
                ClientScript.RegisterStartupScript(GetType(), "TestAlert", "alert(\'Company already Exist.\');", true);
            }

        }
        else
        {
            string Div_name = ddl_div.SelectedItem.ToString();
            string Dig = ddl_Desig.SelectedItem.ToString();
            //string node = TreeView2.SelectedValue.ToString();
            string value = TreeView2.SelectedNode.Value;
            // Update Division   

            //string div_weekoff1 = "";
            //for (int i = 0; i < Chkweek.Items.Count; i++)
            //{

            //    if (Chkweek.Items[i].Selected)//changed 1 to i  


            //    div_weekoff1 += Chkweek.Items[i].Text.ToString() + ","; //changed 1 to i
            //}

            // txtWeekOff.Text = div_weekoff;
            //txtWeekOff.Text = div_weekoff.TrimEnd(',');
           // strChkWeekOffValue = Session["Value"].ToString();
            Division dv = new Division();
            int iReturn = 1;//dv.RecordUpdate(div_code, div_name, div_addr1, div_addr2, div_city, div_pin, sChkLocation, div_sname, div_alias, div_year, strChkWeekOffValue);
            if (iReturn > 0)
            {
                // menu1.Status = "Division updated Successfully";
                ClientScript.RegisterStartupScript(GetType(), "TestAlert", "alert(\'Updated Successfully.\');window.location='Menu_Ctrl_Creation.aspx';", true);
            }
            else if (iReturn == -2)
            {
                // menu1.Status = "Division exist with the same short name!!";
                ClientScript.RegisterStartupScript(GetType(), "TestAlert", "alert(\'Already exist with the same code.\');", true);
            }

        }
    }
    
    private void Resetall()
    {
        ddl_div.SelectedIndex = 0;
        ddl_Desig.SelectedIndex = -1;
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

    
    protected void lnk_Click(object sender, EventArgs e)
    {
        pnlNew.Visible = true;
    }

    protected void ddl_div_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillDesignation();
    }
}