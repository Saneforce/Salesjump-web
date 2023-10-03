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

public partial class MasterFiles_Options_Loginpage_ImgUpload : System.Web.UI.Page
{
    //private SqlConnection con = new SqlConnection("Data Source=PRIYA;Integrated Security=true;Initial Catalog=Old_eReporting");
    SqlConnection con = new SqlConnection(Globals.ConnString);
    string div_code = string.Empty;
    DataSet ds = new DataSet();
    int Id;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtSub.Focus();
            menu1.Title = this.Page.Title;
            menu1.FindControl("btnBack").Visible = false;
            BindGridviewData();
        }
    }
    private void BindGridviewData()
    {
        ds.Clear();
        SqlCommand cmd = new SqlCommand("select * from Mas_LoginPage_Image", con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        
        da.Fill(ds);
        con.Close();
        gvDetails.DataSource = ds;
        gvDetails.DataBind();
    }
    // Save files to Folder and files path in database
    protected void btnUpload_Click(object sender, EventArgs e)
    {

        if (fileUpload1.HasFile)
        {

            BindGridviewData();
            string filename = Path.GetFileName(fileUpload1.PostedFile.FileName);
            fileUpload1.SaveAs(Server.MapPath("~/MasterFiles/Options/Files/" + filename));
            con.Open();
            SqlCommand cmd = new SqlCommand();

            if (ds.Tables[0].Rows.Count > 0)
            {
                cmd = new SqlCommand("update Mas_LoginPage_Image set FileName=@Name, FilePath=@Path,subject=@Subject,Upload_Date=@Upload_Date where Id=@Id ", con);
                cmd.Parameters.Add(new SqlParameter("@Id", ds.Tables[0].Rows[0][0].ToString()));
                cmd.Parameters.AddWithValue("@Name", filename);
                cmd.Parameters.AddWithValue("@Path", "~/MasterFiles/Options/Files/" + filename);
                cmd.Parameters.Add(new SqlParameter("@Subject", txtSub.Text));
                cmd.Parameters.Add(new SqlParameter("@Upload_Date", DateTime.Now));
            }
            else
            {
                cmd = new SqlCommand("SELECT isnull(max(Id)+1,'1') Id from Mas_LoginPage_Image", con);

                SqlDataAdapter daimage = new SqlDataAdapter(cmd);
                DataSet dsimage = new DataSet();
                daimage.Fill(dsimage);
                cmd = new SqlCommand("insert into Mas_LoginPage_Image(Id,FileName,FilePath,subject,Upload_Date) values('" + dsimage.Tables[0].Rows[0][0].ToString() + "',@Name,@Path,@Subject,@Upload_Date)", con);
                // cmd.Parameters.Add(new SqlParameter("@Id", ds.Tables[0].Rows[0][0].ToString()));
                cmd.Parameters.AddWithValue("@Name", filename);
                cmd.Parameters.AddWithValue("@Path", "~/MasterFiles/Options/Files/" + filename);
                cmd.Parameters.Add(new SqlParameter("@Subject", txtSub.Text));
                cmd.Parameters.Add(new SqlParameter("@Upload_Date", DateTime.Now));
            }

            cmd.ExecuteNonQuery();
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Uploaded Sucessfully');</script>");
            txtSub.Text = "";
            con.Close();
            BindGridviewData();
        }
        else
        {
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select a File');</script>");
        }
    }
    // This button click event is used to download files from gridview
    protected void lnkDownload_Click(object sender, EventArgs e)
    {
       BindGridviewData();
        LinkButton lnkbtn = sender as LinkButton;
        GridViewRow gvrow = lnkbtn.NamingContainer as GridViewRow;
        string filePath = ds.Tables[0].Rows[0][2].ToString();
        Response.ContentType = "image/jpg";
        Response.AddHeader("Content-Disposition", "attachment;filename=\"" + filePath + "\"");
        Response.TransmitFile(Server.MapPath(filePath));
        Response.End();
    }
    public void gvDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
    }
    public void gvDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
      
                if (e.CommandName == "Delete")
                {                 

                    try
                    {
                        int Id = Convert.ToInt32(e.CommandArgument);
                        SqlCommand cmd = new SqlCommand();
                        cmd.CommandText = "delete from Mas_LoginPage_Image where Id=@Id";
                        cmd.Connection = con;
                        cmd.Parameters.Add(new SqlParameter("@Id", Id));
                        con.Open();
                        cmd.ExecuteNonQuery();
                        ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Deleted Successfully');</script>");
                        BindGridviewData();
                        con.Close();
                    }
                    catch (Exception ex)
                    {

                    }
                }
         
    }
}