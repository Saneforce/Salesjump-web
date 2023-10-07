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
public partial class MasterFiles_Options_Homepage_ImgUpload : System.Web.UI.Page
{
  //  private SqlConnection con = new SqlConnection("Data Source=PRIYA;Integrated Security=true;Initial Catalog=Old_eReporting");
    SqlConnection con = new SqlConnection(Globals.ConnString);
    string div_code = string.Empty;
    DataSet ds = new DataSet();
    int Id;
    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        if (!IsPostBack)
        {
            menu1.Title = this.Page.Title;
            menu1.FindControl("btnBack").Visible = false;
            BindGridviewData();
            txtSub.Focus();
        }
    }
    private void BindGridviewData()
    {
        ds.Clear();
        SqlCommand cmd = new SqlCommand("select * from Mas_HomePage_Image where Division_Code = '" + div_code + "'", con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);

        da.Fill(ds);
        con.Close();
        gvDetails.DataSource = ds.Tables[0];
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

            //if (ds.Tables[0].Rows.Count > 0)
            //{
            //    cmd = new SqlCommand("update Mas_HomePage_Image set FileName=@Name, FilePath=@Path,subject=@Subject,Upload_Date=@Upload_Date where Id=@Id ", con);
            //    cmd.Parameters.Add(new SqlParameter("@Id", ds.Tables[0].Rows[0][0].ToString()));
            //    cmd.Parameters.AddWithValue("@Name", filename);
            //    cmd.Parameters.AddWithValue("@Path", "~/MasterFiles/Options/Files/" + filename);
            //    cmd.Parameters.Add(new SqlParameter("@Subject", txtSub.Text));
            //    cmd.Parameters.Add(new SqlParameter("@Upload_Date", DateTime.Now));
            //}
            //else
            //{
            cmd = new SqlCommand("SELECT isnull(max(Id)+1,'1') Id from Mas_HomePage_Image", con);

            SqlDataAdapter daimage = new SqlDataAdapter(cmd);
            DataSet dsimage = new DataSet();
            daimage.Fill(dsimage);
            cmd = new SqlCommand("insert into Mas_HomePage_Image(Id,FileName,FilePath,subject,Upload_Date,Division_Code) values('" + dsimage.Tables[0].Rows[0][0].ToString() + "',@Name,@Path,@Subject,@Upload_Date,@Division_Code)", con);
            // cmd.Parameters.Add(new SqlParameter("@Id", ds.Tables[0].Rows[0][0].ToString()));
            cmd.Parameters.AddWithValue("@Name", filename);
            cmd.Parameters.AddWithValue("@Path", "~/MasterFiles/Options/Files/" + filename);
            cmd.Parameters.Add(new SqlParameter("@Subject", txtSub.Text));
            cmd.Parameters.Add(new SqlParameter("@Upload_Date", DateTime.Now));
            cmd.Parameters.Add(new SqlParameter("@Division_Code", div_code));
            //}


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
                cmd.CommandText = "delete from Mas_HomePage_Image where Id=@Id";
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