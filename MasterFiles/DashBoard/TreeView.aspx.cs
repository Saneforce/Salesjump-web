using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

using Bus_EReport;
using System.Web.UI.DataVisualization.Charting;
using System.Drawing;
using System.Web.Services;
using System.Configuration;
using System.Data.SqlClient;
using System.Text;

public partial class MasterFiles_DashBoard_TreeView : System.Web.UI.Page
{

    DataSet dsSalesForce = null;
    DataSet dsDivision = null;
    public static DataSet dsDoctor = new DataSet();
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
    string sf_type = string.Empty;
    string hq = string.Empty;
    public static int iddvalue;
    public static int iDRCatg = -1;
    public static string imrvalue = string.Empty;
    StringBuilder str = new StringBuilder();
    DataTable dt = new DataTable();
     public static string Chart_mas = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        // div_code = Session["div_code"].ToString();
        Chart_mas = "";
        if (sf_type == "3")
        {
            div_code = Session["division_code"].ToString();
        }
        else
        {
            div_code = Session["div_code"].ToString();
        }

        if (!Page.IsPostBack)
        {
          //  menu1.Title = this.Page.Title;
            CommonLoad();
            trvuser.CollapseAll();
            //pnldoc.Visible = false;
            //pnldoc1.Visible = false;
            //pnltreeview.Visible = false;
            //pnlgraph.Visible = false;
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
        dsSF = sf.SF_Hierarchy(div_code, "admin");
        if (dsSF.Tables[0].Rows.Count > 0)
        {
                TreeNode treeHead = new TreeNode();
                treeHead.Text = "Geography";
                treeHead.Value = "admin";
              //  treeHead.ExpandAll();
                objTreeView.Nodes.Add(treeHead);
                foreach (TreeNode childnode in GetChildNode("admin"))
                {
                    treeHead.ChildNodes.Add(childnode);
                }            
            
            //foreach (DataRow dataRow in dsSF.Tables[0].Rows)
            //{               
            //    TreeNode treeRoot = new TreeNode();
            //    treeRoot.Text = "<font style='color:maroon; background-color:violet'><strong>" + dataRow["Sf_Name"].ToString() + "</strong></font>";
            //    treeRoot.Value = dataRow["sf_code"].ToString();
            //    //treeRoot.ImageUrl = "../Images/LOOKUP.gif";

            //    treeRoot.ExpandAll();
            //    objTreeView.Nodes.Add(treeRoot);

            //    //foreach (TreeNode childnode in GetChildNode(dataRow["sf_code"].ToString()))
            //    //{
            //    //    treeRoot.ChildNodes.Add(childnode);
            //    //}
            //}
        }
    }
    //protected void buttonSearch_Click(object sender, EventArgs e) 
    //{ 
    //    trvuser.CollapseAll();
    //    //string sFind = string.Empty;
    //    //sFind = " AND a." + sSearchBy + " like '" + sSearchText + "%' AND (a.Division_Code like '" + div_code + ',' + "%'  or " +
    //    //             " a.Division_Code like '%" + ',' + div_code + ',' + "%') ";
    //    //SalesForce sf = new SalesForce();
    //    //dsSalesForce = sf.FindSalesForcelist(sFind);
    //    TreeNode searchNode = trvuser.FindNode(textSearch.Text);
    //    if (searchNode != null) 
    //        searchNode.ExpandAll(); 
    //}

    protected void lstmas_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        Chart_mas = "Chart";
        Doctor dr = new Doctor();
        Lblsf_name.Text = sf_name;
        if (lstmas.SelectedValue == "1")
        {
            dsDoctor = dr.getDocCat(div_code);
            if (dsDoctor.Tables[0].Rows.Count > 0)
            {
                //  lstmas = dsDoctor.Tables[0].Rows.Count;
                ViewState["dsDoctor"] = dsDoctor;
            }
        }
        else if (lstmas.SelectedValue == "2")
        {
            dsDoctor = dr.getDocSpec(div_code);
            if (dsDoctor.Tables[0].Rows.Count > 0)
            {
                ViewState["dsDoctor"] = dsDoctor;
            }
        }
        else if (lstmas.SelectedValue == "3")
        {
            dsDoctor = dr.getDocClass(div_code);
            if (dsDoctor.Tables[0].Rows.Count > 0)
            {
                ViewState["dsDoctor"] = dsDoctor;
            }
        }
        else if (lstmas.SelectedValue == "4")
        {
            dsDoctor = dr.getDocQual(div_code);
            if (dsDoctor.Tables[0].Rows.Count > 0)
            {
                ViewState["dsDoctor"] = dsDoctor;
            }
        }
        //  sf_code = trvuser.SelectedValue;
        //  CreateDynamicTable();
        iddvalue = Convert.ToInt16(lstmas.SelectedValue);
        imrvalue = trvuser.SelectedValue;
        GetData();

     //   BindChart_Stack();
      //  Bindchart_Dough();
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
                    childNode.Text = "<font style='color:Black; background-color:#faf0e6'><strong>" + dataRow["Sf_Name"].ToString() + "</strong></font>";
                }
                else
                {
                    childNode.Text = "<font style='color:Black; background-color:#f0fff0'><strong>" + dataRow["Sf_Name"].ToString() + "</strong></font>";
                }
                childNode.Value = dataRow["sf_code"].ToString();
                //childNode.Text = dataRow["sf_name"].ToString();

            //    childNode.ExpandAll();

                //childNode.ImageUrl = "../Images/arr_menu.gif";
            //    childNode.ExpandAll();

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

    protected void btnsfe_OnClick(object sender, EventArgs e)
    {
        pnldoc.Visible = false;
        pnldoc1.Visible = true;
    }
    protected void btnmaster_Click(object sender, EventArgs e)
    {
        pnldoc1.Visible = false;
        pnldoc.Visible = true;
        
    }
    protected void trvuser_SelectedNodeChanged(object sender, EventArgs e)
    {
        sf_code = trvuser.SelectedValue;
        sf_name = trvuser.SelectedNode.Text;
        pnlgraph.Visible = true;
        Lblsf_name.Text = sf_name;
      
     
    }
    private void CreateDynamicTable()
    {
        dsDoctor = (DataSet)ViewState["dsDoctor"];
        iddvalue = Convert.ToInt16(lstmas.SelectedValue);
        imrvalue = trvuser.SelectedValue; 

    }
    [WebMethod]
    public static List<Data> GetData()
    {
     
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
    //    DataSet dsDoctor = new DataSet();
        TableCell tc_catg_det_name = new TableCell();
        Literal lit_catg_det_name = new Literal();

        Doctor dr_cat = new Doctor();

        //ds.Merge(dsDoctor);
        //  dt = ds.Tables[0];
        List<Data> dataList = new List<Data>();
        string cat = "";
        int val = 0;
        if (Chart_mas == "Chart")
        {

            foreach (DataRow dr in dsDoctor.Tables[0].Rows)
            {
                if (iddvalue == 1)
                {
                    iDRCatg = dr_cat.getDoctorcount(imrvalue, dr["Doc_Cat_Code"].ToString());

                }
                else if (iddvalue == 2)
                {
                    iDRCatg = dr_cat.getSpecialcount(imrvalue, dr["Doc_Cat_Code"].ToString());

                }
                else if (iddvalue == 3)
                {
                    iDRCatg = dr_cat.getClasscount(imrvalue, dr["Doc_Cat_Code"].ToString());

                }
                else if (iddvalue == 4)
                {
                    iDRCatg = dr_cat.getQualcount(imrvalue, dr["Doc_Cat_Code"].ToString());

                }

                //foreach (DataRow dr in dt.Rows)
                //{
                cat = dr[2].ToString();
                val = Convert.ToInt32(iDRCatg);

                dataList.Add(new Data(cat, val));
                //}
            }
        }
        
        return dataList;
        // }
    }


    public class Data
    {
        public string ColumnName = "";
        public int Value = 0;

        public Data(string columnName, int value)
        {

            ColumnName = columnName;
            Value = value;
        }
    }


    private DataTable GetData_stack()
    {
      
        DataSet ds = new DataSet();
       
        //    DataSet dsDoctor = new DataSet();
        TableCell tc_catg_det_name = new TableCell();
        Literal lit_catg_det_name = new Literal();

        Doctor dr_cat = new Doctor();

        //ds.Merge(dsDoctor);
        //  dt = ds.Tables[0];
        List<Data> dataList = new List<Data>();
        string cat = "";
        int val = 0;

        foreach (DataRow dr in dsDoctor.Tables[0].Rows)
        {
            if (iddvalue == 1)
            {
                iDRCatg = dr_cat.getDoctorcount(imrvalue, dr["Doc_Cat_Code"].ToString());

            }
            else if (iddvalue == 2)
            {
                iDRCatg = dr_cat.getSpecialcount(imrvalue, dr["Doc_Cat_Code"].ToString());

            }
            else if (iddvalue == 3)
            {
                iDRCatg = dr_cat.getClasscount(imrvalue, dr["Doc_Cat_Code"].ToString());

            }
            else if (iddvalue == 4)
            {
                iDRCatg = dr_cat.getQualcount(imrvalue, dr["Doc_Cat_Code"].ToString());

            }

            //foreach (DataRow dr in dt.Rows)
            //{
            cat = dr[2].ToString();
            val = Convert.ToInt32(iDRCatg);

            dataList.Add(new Data(cat, val));
            dt.Rows.Add(cat, val);
          
            //}
        }

        return dt;
    
    }

    private void BindChart_bar()
    {
        DataTable dt = new DataTable();
        try
        {
            dt = GetData_stack();
 

            str.Append(@"<script type=*text/javascript*> google.load( *visualization*, *1*, {packages:[*corechart*]});
                       google.setOnLoadCallback(drawChart);
                       function drawChart() {
        var data = new google.visualization.DataTable();
      public Data(string columnName, int value)
        {

            ColumnName = columnName;
            Value = value;
        } 
 

        data.addRows(" + dt.Rows.Count + ");");
 

            for (int i = 0; i <= dt.Rows.Count - 1; i++)
            {
                str.Append("data.setValue( " + i + "," + 0 + "," + "'" + dt.Rows[i]["columnName"].ToString() + "');");
                str.Append("data.setValue(" + i + "," + 1 + "," + dt.Rows[i]["value"].ToString() + ") ;");             
            }
 

            str.Append(" var chart = new google.visualization.ColumnChart(document.getElementById('chart_div'));");
            str.Append(" chart.draw(data, {width: 550, height: 300, title: 'Company Performance',");
            str.Append("hAxis: {title: 'Year', titleTextStyle: {color: 'green'}}");
            str.Append("}); }");
            str.Append("</script>");
            lt.Text = str.ToString().Replace('*', '"');
        }
        catch
        {   }

    }


    private void BindChart_Stack()
    {
       
        try
        {
            dt = GetData_stack();

            str.Append(@"<script type=text/javascript> google.load( *visualization*, *1*, {packages:[*corechart*]});
            google.setOnLoadCallback(drawChart);
            function drawChart() {
            var data = new google.visualization.DataTable();
            data.addColumn('string', 'ListedDr_Name');
            data.addColumn('number', 'iDRCatg');
         

            data.addRows(" + dt.Rows.Count + ");");

            for (int i = 0; i <= dt.Rows.Count - 1; i++)
            {
                str.Append("data.setValue( " + i + "," + 0 + "," + "'" + dt.Rows[i]["ListedDr_Name"].ToString() + "');");
                str.Append("data.setValue(" + i + "," + 1 + "," + dt.Rows[i]["iDRCatg"].ToString() + ") ;");
               // str.Append("data.setValue(" + i + "," + 2 + "," + dt.Rows[i]["Doc_Cat_Code"].ToString() + ") ;");
            }

            str.Append(" var chart = new google.visualization.ColumnChart(document.getElementById('chart_div'));");
            str.Append(" chart.draw(data,{isStacked:true, width:600, height:350, hAxis: {showTextEvery:1, slantedText:true}});}");

            str.Append("</script>");
            lt.Text = str.ToString().TrimEnd(',').Replace('*', '"');
        }
        catch
        {
        }
    }

  
 
    
}


