using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using Bus_EReport;
public partial class MasterFiles_Options_HomePage_FieldForcewise : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(Globals.ConnString);
    string div_code = string.Empty;
    DataSet dsSalesForce = new DataSet();
    DataSet dsAdmin = null;
   
    string Image_Id = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
      
        if(!Page.IsPostBack)
        {
        menu1.Title = this.Page.Title;
        menu1.FindControl("btnBack").Visible = false;
        //FillSalesForce();
        FillReporting();
        ddlFilter.Focus();
        }
      
    }
    private void FillReporting()
    {
        SalesForce sf = new SalesForce();
        dsSalesForce = sf.getUserList_Reporting(div_code);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            ddlFilter.DataTextField = "Sf_Name";
            ddlFilter.DataValueField = "Sf_Code";
            ddlFilter.DataSource = dsSalesForce;
            ddlFilter.DataBind();
        }        
    }

    //private void FillSalesForce()
    //{
    //    SalesForce sf = new SalesForce();
    //    dsSalesForce = sf.getSales(div_code);
    //    if (dsSalesForce.Tables[0].Rows.Count > 0)
    //    {
    //        btnUpload.Visible = true; ;
    //        grdSalesForce.Visible = true;
    //        grdSalesForce.DataSource = dsSalesForce;
    //        grdSalesForce.DataBind();
    //    }
    //    else
    //    {
         
    //        grdSalesForce.DataSource = dsSalesForce;
    //        grdSalesForce.DataBind();
    //    }
    //}  

  

    private void FillSalesForce_Reporting()
    {
        string sReport = ddlFilter.SelectedValue.ToString();
      
        SalesForce sf = new SalesForce();
     // dsSalesForce = sf.getSalesForcelist_Reporting(div_code, sReport);
       // dsSalesForce = sf.getSales(div_code, sReport, Image_Id);
        dsSalesForce = sf.Sales_Image(div_code, sReport);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            grdSalesForce.Visible = true;
            grdSalesForce.DataSource = dsSalesForce;
            grdSalesForce.DataBind();
        }      
       
    }

    protected void bt_upload_OnClick(object sender, EventArgs e)
    {
        FileUpload FilUpImage =new FileUpload();
        string sReport = ddlFilter.SelectedValue.ToString();
        //DataSet dsSalesForce = new DataSet();
        SqlConnection con = new SqlConnection(Globals.ConnString);
        for (int i = 0; i < grdSalesForce.Rows.Count; i++)
        {

            FilUpImage = (FileUpload)grdSalesForce.Rows[i].FindControl("FilUpImage");
            Button btnUpload = (Button)grdSalesForce.Rows[i].FindControl("btnUpload");
            Label lblSF_Code = (Label)grdSalesForce.Rows[i].FindControl("lblSF_Code");

            //  string fileName = FilUpImage.FileName;


            string fileName = Path.GetFileName(FilUpImage.PostedFile.FileName);
            if(fileName!="")
            {
            FilUpImage.SaveAs(Server.MapPath("~/MasterFiles/Options/Files/" + fileName));
            
            con.Open();

            SqlCommand cmd = new SqlCommand();

            //FillReporting();
            SqlCommand cmdImage = new SqlCommand("select * from Mas_HomeImage_FieldForce where Sf_Code='" + lblSF_Code.Text + "' and Division_Code='" + div_code + "'", con);
            //cmd.Parameters.AddWithValue("@Division_Code", div_code);
            //cmd.Parameters.AddWithValue("@Sf_Code", sReport);
            SqlDataAdapter da = new SqlDataAdapter(cmdImage);
            DataSet ds = new DataSet();
            da.Fill(ds);

            if (ds.Tables[0].Rows.Count > 0)
            {

                cmd = new SqlCommand("update Mas_HomeImage_FieldForce set FileName=@Name, FilePath=@Path ,Upload_Date=@Upload_Date where Division_Code=@Division_Code and Sf_Code=@Sf_Code", con);

                // cmd.Parameters.Add(new SqlParameter("@Id", ds.Tables[0].Rows[0][0].ToString()));
                cmd.Parameters.AddWithValue("@Name", fileName);
                cmd.Parameters.AddWithValue("@Path", "~/MasterFiles/Options/Files/" + fileName);
                cmd.Parameters.Add(new SqlParameter("@Upload_Date", DateTime.Now));
                cmd.Parameters.AddWithValue("@Division_Code", div_code);
                cmd.Parameters.AddWithValue("@Sf_Code", lblSF_Code.Text);
                cmd.ExecuteNonQuery();               
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Image Updated Successfully');</script>");
               


            }
            else
            {
                cmd = new SqlCommand("SELECT isnull(max(Image_Id)+1,'1') Image_Id from Mas_HomeImage_FieldForce", con);

                SqlDataAdapter daimage = new SqlDataAdapter(cmd);
                DataSet dsimage = new DataSet();
                daimage.Fill(dsimage);

                cmd = new SqlCommand("insert into Mas_HomeImage_FieldForce(Image_Id,FileName,FilePath,Upload_Date,Division_Code,Sf_Code) values('" + dsimage.Tables[0].Rows[0][0].ToString() + "',@Name,@Path,@Upload_Date,@Division_Code,@Sf_Code)", con);

                // cmd.Parameters.Add(new SqlParameter("@Id", ds.Tables[0].Rows[0][0].ToString()));
                cmd.Parameters.AddWithValue("@Name", fileName);
                cmd.Parameters.AddWithValue("@Path", "~/MasterFiles/Options/Files/" + fileName);
                cmd.Parameters.Add(new SqlParameter("@Upload_Date", DateTime.Now));
                cmd.Parameters.AddWithValue("@Division_Code", div_code);
                cmd.Parameters.AddWithValue("@Sf_Code", lblSF_Code.Text);
                cmd.ExecuteNonQuery();
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Image Inserted Successfully');</script>");
                  

            }
            }
        }
            FillSalesForce_Reporting();       
        con.Close();
    }
   
    protected void btnGo_Click(object sender, EventArgs e)
    {
        
        lblSelect.Visible = false;       
        FillSalesForce_Reporting();
     
    }
    public void grdSalesForce_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
    }
    public void grdSalesForce_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        if (e.CommandName == "Delete")
        {

            try
            {
                int Image_Id = Convert.ToInt32(e.CommandArgument);
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "delete from Mas_HomeImage_FieldForce where Image_Id=@Image_Id";
                cmd.Connection = con;
                cmd.Parameters.Add(new SqlParameter("@Image_Id", Image_Id));
                con.Open();
                cmd.ExecuteNonQuery();
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Deleted Successfully');</script>");
              //  BindGridviewData();
                FillSalesForce_Reporting();
                con.Close();
            }
            catch (Exception ex)
            {

            }
        }

    }
}