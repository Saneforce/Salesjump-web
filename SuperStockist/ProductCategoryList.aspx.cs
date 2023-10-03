using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using System.Web.Services;
using Bus_EReport;
using System.Data;
using System.Configuration;
using System.Drawing;
using System.IO;
using DBase_EReport;

public partial class MasterFiles_ProductCategoryList : System.Web.UI.Page
{

    #region "Declaration"
    public string div_code;
    public string div_code1;
    public static  string sf_code = string.Empty;
    public string sf_type = string.Empty;
    #endregion
    protected override void OnPreInit(EventArgs e)
    {
        base.OnPreInit(e);
        sf_type = Session["sf_type"].ToString();
        //if (sf_type == "3")
        //{
        //    //this.MasterPageFile = "~/Master.master";
        //    this.MasterPageFile = "~/Master_DIS.master";
        //}
        //else if (sf_type == "2")
        //{
        //    this.MasterPageFile = "~/Master_MGR.master";
        //}
        //else if (sf_type == "1")
        //{
        //    this.MasterPageFile = "~/Master_MR.master";
        //}
        this.MasterPageFile = "~/Master_SS.master";
    }
    protected void Page_Load(object sender, EventArgs e)
    {

        try
        {
            div_code = Session["div_code"].ToString();
            div_code1 = Session["div_code"].ToString();
            sf_code = Session["Sf_Code"].ToString();
        }
        catch
        {
            div_code = Session["Division_Code"].ToString();
        }

        //Session["Div_code"] = div_code.ToString();

    }
    [WebMethod]
    public static string GetProductCate(string div)
    {
        StockistMaster Rut = new StockistMaster();
        DataSet ds = Stockist_getProCat(div.Replace(",", ""));
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    public static DataSet Stockist_getProCat(string divcode)
    {
        DB_EReporting db_ER = new DB_EReporting();

        DataSet dsProCat = null;

        

        string strQry = "exec getcatlist '" + divcode + "' , '" + sf_code + "' ";
        try
        {
            dsProCat = db_ER.Exec_DataSet(strQry);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return dsProCat;
    }

    [WebMethod]
    public static string Get_product_details (string div)
    {
        StockistMaster Rut = new StockistMaster();
        DataSet ds = Getdivisionproduct(div.Replace(",", ""));
       // DataSet ds = Rut.Getdivisionproduct(div.Replace(",", ""));
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    public static DataSet Getdivisionproduct(string Div_Code)
    {
        DB_EReporting db_ER = new DB_EReporting();
        DataSet ds = null;

       // string strQry = "select * from mas_product_detail where Division_Code='" + Div_Code + "' and Product_Active_Flag='0'";
		  string strQry = "exec getproclist '"+ Div_Code + "' ,'"+ sf_code + "' ";

        try
        {
            ds = db_ER.Exec_DataSet(strQry);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return ds;
    }

    /* #region "Declaration"
     DataSet dsProCat = null;
     int ProCatCode = 0;
     string divcode = string.Empty;
     string Product_Cat_SName = string.Empty;
     string ProCatName = string.Empty;
     string Pro_Div_name=string.Empty;
     string Pro_Div_code = string.Empty;
     DateTime ServerStartTime;
     DateTime ServerEndTime;
     int time;
     #endregion

     protected void Page_Load(object sender, EventArgs e)
     {
         divcode = Convert.ToString(Session["division_code"]);
         if (!Page.IsPostBack)
         {
             FillProCat();
             btnNew.Focus();
             //menu1.Title = this.Page.Title;
             //menu1.FindControl("btnBack").Visible = false;
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
     private void FillProCat()
     {
         StockistMaster sm = new StockistMaster();
         Product dv = new Product();
        dsProCat = sm.Stockist_getProCat(divcode.TrimEnd(','));
         //dsProCat = dv.Stockist_getProCat(divcode.TrimEnd(','));

         if (dsProCat.Tables[0].Rows.Count > 0)
         {
             grdProCat.Visible = true;
             grdProCat.DataSource = dsProCat;
             grdProCat.DataBind();            
             //for (int i = 0; i < dsProCat.Tables[0].Rows.Count; i++)
             //{                
               foreach (GridViewRow row in grdProCat.Rows)
             {
                 LinkButton lnkdeact = (LinkButton)row.FindControl("lnkbutDeactivate");
                   Label lblimg = (Label)row.FindControl("lblimg");
                   LinkButton lnkcount = (LinkButton)row.FindControl("lnkcount");
                // if (Convert.ToInt32(dsProCat.Tables[0].Rows[row.RowIndex][3].ToString()) > 0)
                   if(lnkcount.Text != "0")
                 {
                    // grdProCat.Rows[row.RowIndex].Cells[7].Enabled = false;
                     lnkdeact.Visible = false;
                     lblimg.Visible =true;
                 }
             }
         }
         else
         {
             grdProCat.DataSource = dsProCat;
             grdProCat.DataBind();
         }
     }
     // Sorting
     public SortDirection dir
     {
         get
         {
             if (ViewState["dirState"] == null)
             {
                 ViewState["dirState"] = SortDirection.Ascending;
             }
             return (SortDirection)ViewState["dirState"];
         }
         set
         {
             ViewState["dirState"] = value;
         }
     }
     private DataTable BindGridView()
     {
         DataTable dtGrid = new DataTable();
         Product dv = new Product();
         dtGrid = dv.getProductCategorylist_DataTable(divcode.TrimEnd(','));
         return dtGrid;
     }

     protected void grdProCat_Sorting(object sender, GridViewSortEventArgs e)
     {

         string sortingDirection = string.Empty;
         if (dir == SortDirection.Ascending)
         {
             dir = SortDirection.Descending;
             sortingDirection = "Desc";
         }
         else
         {
             dir = SortDirection.Ascending;
             sortingDirection = "Asc";
         }

         DataView sortedView = new DataView(BindGridView());
         sortedView.Sort = e.SortExpression + " " + sortingDirection;
         DataTable dtGrid = new DataTable();
         dtGrid = sortedView.ToTable();
         grdProCat.DataSource = dtGrid;
         grdProCat.DataBind();

         Product dv = new Product();
       //  dtGrid = dv.getProductCategorylist_DataTable(divcode);
         foreach (GridViewRow row in grdProCat.Rows)
         {
             LinkButton lnkdeact = (LinkButton)row.FindControl("lnkbutDeactivate");
             Label lblimg = (Label)row.FindControl("lblimg");
             if (Convert.ToInt32(dtGrid.Rows[row.RowIndex][3].ToString()) > 0)
             {
                 // grdProCat.Rows[row.RowIndex].Cells[7].Enabled = false;
                 lnkdeact.Visible = false;
                lblimg.Visible = true;
             }
         }
     }
     protected void grdProCat_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
     {
         //This will get invoke when the user clicks Cancel link from "Inline Edit" link
         grdProCat.EditIndex = -1;
         //Fill the State Grid
         FillProCat();
     }
     protected void grdProCat_RowDataBound(object sender, GridViewRowEventArgs e)
     {
         Product st = new Product();
         dsProCat = st.getDiv(divcode.TrimEnd(','));
         if ((e.Row.RowState & DataControlRowState.Edit) > 0)
         {

             DropDownList ddlQual = new DropDownList();
             ddlQual = (DropDownList)e.Row.FindControl("ddlDiv");

             if (ddlQual != null)
             {
                 ddlQual.DataSource = dsProCat;
                 ddlQual.DataTextField = "subdivision_name";
                 ddlQual.DataValueField = "subdivision_code";
                 ddlQual.DataBind();



                 if (e.Row.RowType == DataControlRowType.DataRow)
                 {


                     DropDownList State_Type = (DropDownList)e.Row.FindControl("ddlDiv");
                     if (State_Type != null)
                     {
                         DataRowView row = (DataRowView)e.Row.DataItem;

                         State_Type.SelectedIndex = State_Type.Items.IndexOf(State_Type.Items.FindByText(row["Product_Cat_Div_Name"].ToString()));

                     }
                 }
             }
         }

     }
     protected void grdProCat_RowEditing(object sender, GridViewEditEventArgs e)
     {
         //This will get invoke when the user clicks "Inline Edit" link
         grdProCat.EditIndex = e.NewEditIndex;
         //Fill the State Grid
         FillProCat();
         //Setting the focus to the textbox "Short Name"        
         TextBox ctrl = (TextBox)grdProCat.Rows[e.NewEditIndex].Cells[2].FindControl("txtProduct_Cat_SName");
         ctrl.Focus();
     }
     protected void grdProCat_RowDeleting(object sender, GridViewDeleteEventArgs e)
     {
         Label lblProCatCode = (Label)grdProCat.Rows[e.RowIndex].Cells[1].FindControl("lblProCatCode");
         ProCatCode = Convert.ToInt16(lblProCatCode.Text);

         // Delete Product Category
         Product dv = new Product();
         int iReturn = dv.RecordDelete(ProCatCode);
          if (iReturn > 0 )
         {
            // menu1.Status = "Product Category Deleted Successfully ";
             ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Deleted Successfully');</script>");
         }
         else if (iReturn == -2)
         {
            // menu1.Status = "Product Category cant be deleted";
             ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Unable to Delete');</script>");
         }
         FillProCat();
     }
     protected void grdProCat_RowCommand(object sender, GridViewCommandEventArgs e)
     {
         if (e.CommandName == "Deactivate")
         {
             ProCatCode = Convert.ToInt16(e.CommandArgument);

             //Deactivate
             Product dv = new Product();
             int iReturn = dv.DeActivate(ProCatCode);
              if (iReturn > 0 )
             {
                // menu1.Status = "Product Category has been Deactivated Successfully";
                 ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Deactivated Successfully');</script>");
             }
             else
             {
               //  menu1.Status = "Unable to Deactivate";
                 ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Unable to Deactivate');</script>");
             }
             FillProCat();
         }
     }

     protected void grdProCat_RowUpdating(object sender, GridViewUpdateEventArgs e)
     {
         grdProCat.EditIndex = -1;
         int iIndex = e.RowIndex;
         Update(iIndex);
         FillProCat();
     }
     protected void grdProCat_RowCreated(object sender, GridViewRowEventArgs e)
     {
         if (e.Row.RowType == DataControlRowType.DataRow)
         {
             e.Row.Attributes.Add("onmouseover", "this.className='highlight'");
             e.Row.Attributes.Add("onmouseout", "this.className='normal'");
         }
     }

     protected void grdProCat_PageIndexChanging(object sender, GridViewPageEventArgs e)
     {
         grdProCat.PageIndex = e.NewPageIndex;
         FillProCat();
     }
     private void Update(int eIndex)
     {
         Label lblProCatCode = (Label)grdProCat.Rows[eIndex].Cells[1].FindControl("lblProCatCode");
         ProCatCode = Convert.ToInt16(lblProCatCode.Text);
         TextBox txtProduct_Cat_SName = (TextBox)grdProCat.Rows[eIndex].Cells[2].FindControl("txtProduct_Cat_SName");
         Product_Cat_SName = txtProduct_Cat_SName.Text;
         TextBox txtProCatName = (TextBox)grdProCat.Rows[eIndex].Cells[3].FindControl("txtProCatName");
         ProCatName = txtProCatName.Text;
         DropDownList txt_State = (DropDownList)grdProCat.Rows[eIndex].Cells[3].FindControl("ddlDiv");
         Pro_Div_name = txt_State.SelectedItem.Text;
         Pro_Div_code = txt_State.SelectedValue;
         if (Pro_Div_name == "--Select--")
         {
             ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select Division Name');</script>");
         }

         // Update Product Category

         StockistMaster sm = new StockistMaster();
         Product dv = new Product();
         int iReturn = sm.Stockist_RecordUpdate(ProCatCode, Product_Cat_SName, ProCatName, divcode.TrimEnd(','), Pro_Div_code,Pro_Div_name);
          if (iReturn > 0 )
         {
             //menu1.Status = "Product Category Updated Successfully ";
             ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Updated Successfully');</script>");
         }
          else if (iReturn == -2)
          {
              ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Category Name Already Exist');</script>");
              txtProCatName.Focus();
          }
          else if (iReturn == -3)
          {
              ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Category Code Already Exist');</script>");
              txtProduct_Cat_SName.Focus();
          }
     }

     protected void btnNew_Click(object sender, EventArgs e)
     {
         System.Threading.Thread.Sleep(time);
         Response.Redirect("/Stockist/ProductCategory.aspx");
     }
     protected void btnBulkEdit_Click(object sender, EventArgs e)
     {
          System.Threading.Thread.Sleep(time);
         Response.Redirect("BulkEditProdCat.aspx");
     }
     protected void btnSlNo_Gen_Click(object sender, EventArgs e)
     {
          System.Threading.Thread.Sleep(time);
         Response.Redirect("ProdCat_SlNo_Gen.aspx");
     }
     protected void btnReactivate_Click(object sender, EventArgs e)
     {
         System.Threading.Thread.Sleep(time);
         Response.Redirect("Pro_Cat_React.aspx");
     }
  protected void ExportToExcel(object sender, EventArgs e)
     {
         Response.Clear();
         Response.Buffer = true;
         Response.AddHeader("content-disposition", "attachment;filename=ProductCategoryList.xls");
         Response.Charset = "";
         Response.ContentType = "application/vnd.ms-excel";
         using (StringWriter sw = new StringWriter())
         {
             HtmlTextWriter hw = new HtmlTextWriter(sw);

             //To Export all pages
             grdProCat.AllowPaging = false;
             this.FillProCat();

             grdProCat.HeaderRow.BackColor = Color.White;
             foreach (TableCell cell in grdProCat.HeaderRow.Cells)
             {
                 cell.BackColor = grdProCat.HeaderStyle.BackColor;
             }
             for (int i = 0; i < grdProCat.HeaderRow.Cells.Count; i++)
             {
                 grdProCat.HeaderRow.Cells[i].Style.Add("background-color", "green");
             }

             foreach (GridViewRow row in grdProCat.Rows)
             {
                 row.BackColor = Color.White;
                 foreach (TableCell cell in row.Cells)
                 {
                     if (row.RowIndex % 2 == 0)
                     {
                         cell.BackColor = grdProCat.AlternatingRowStyle.BackColor;
                     }
                     else
                     {
                         cell.BackColor = grdProCat.RowStyle.BackColor;
                     }
                     cell.CssClass = "textmode";
                 }
                 grdProCat.Columns[6].Visible = false;
                 grdProCat.Columns[7].Visible = false;
                 grdProCat.Columns[8].Visible = false;
                 //grdDivision.Columns[7].Visible = false;


             }

             grdProCat.RenderControl(hw);

             //style to format numbers to string
             string style = @"<style> .textmode { } </style>";
             Response.Write(style);
             Response.Output.Write(sw.ToString());
             Response.Flush();
             Response.End();
         }
     }
     public override void VerifyRenderingInServerForm(Control control)
     {
         /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
            server control at run time. *
     }*/
}