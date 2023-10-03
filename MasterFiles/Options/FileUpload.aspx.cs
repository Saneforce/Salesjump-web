using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
using System.IO;
using System.Net;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

public partial class MasterFiles_Options_FileUpload : System.Web.UI.Page
{
    DataSet dsSalesForce = null;
    string div_code = string.Empty;
    string FileName = string.Empty;
    string FileSubject = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
       
        div_code = Session["div_code"].ToString();
        if (!Page.IsPostBack)
        {
            //menu1.Title = this.Page.Title;
            //menu1.FindControl("btnBack").Visible = false;
            BindGridviewData();
            txtFileSubject.Focus();
        }

    }

    // Bind Gridview Data
    private void BindGridviewData()
    {
        //AdminSetup adsp = new AdminSetup();
        //dsSalesForce = adsp.Get_FileUpload(div_code);
        //if (dsSalesForce.Tables[0].Rows.Count > 0)
        //{
        //    gvDetails.DataSource = dsSalesForce;
        //    gvDetails.DataBind();

        //}       
        string constr = Globals.ConnString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "select ID, FileName, FileSubject, Div_Code, Update_dtm,Data,ContentType " +
                        "  from file_info " +
                        "  where div_Code = '" + div_code + "'";
                cmd.Connection = con;
                con.Open();
                gvDetails.DataSource = cmd.ExecuteReader();
                gvDetails.DataBind();
                con.Close();
            }
        }
    }
    protected void Upload(object sender, EventArgs e)
    {

        string filename = Path.GetFileName(fileUpload1.PostedFile.FileName);
        string subject = txtFileSubject.Text.Trim();
        string contentType = fileUpload1.PostedFile.ContentType;
        // string Data = string.Empty;
        //fileUpload1.SaveAs(Server.MapPath("~/MasterFiles/Options/Files/" + filename));
        fileUpload1.SaveAs(Server.MapPath("~/MasterFiles/Files/Circular/" + filename));
       

        if (fileUpload1.HasFile)
        {

            using (Stream fs = fileUpload1.PostedFile.InputStream)
            {
                using (BinaryReader br = new BinaryReader(fs))
                {
                    byte[] bytes = br.ReadBytes((Int32)fs.Length);

                    string constr = Globals.ConnString;
                    using (SqlConnection con = new SqlConnection(constr))
                    {

                        SqlCommand cmd1 = new SqlCommand("SELECT isnull(max(ID)+1,'1') ID from file_info", con);

                        SqlDataAdapter daimage = new SqlDataAdapter(cmd1);
                        DataSet dsimage = new DataSet();
                        con.Open();
                        daimage.Fill(dsimage);
                        con.Close();

                        int ID = Convert.ToInt32(dsimage.Tables[0].Rows[0][0]);

                        string query = " INSERT INTO file_info(ID,FileName,FileSubject,Div_Code,Update_dtm,ContentType,Data) values (@ID,@FileName, @FileSubject,@div_code, getdate(),@ContentType,@Data)";
                        using (SqlCommand cmd = new SqlCommand(query))
                        {
                            cmd.Connection = con;
                            cmd.Parameters.AddWithValue("@ID", ID);
                            cmd.Parameters.AddWithValue("@FileName", filename);
                            cmd.Parameters.AddWithValue("@FileSubject", subject);
                            cmd.Parameters.AddWithValue("@div_code", div_code);
                            cmd.Parameters.AddWithValue("@ContentType", contentType);
                            cmd.Parameters.AddWithValue("@Data", bytes);
                            con.Open();
                            int i = cmd.ExecuteNonQuery();
                            con.Close();

                            if (i > 0)
                            {
                                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Uploaded Successfully');</script>");
                            }
                            else
                            {
                                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Not Uploaded');</script>");
                            }
                        }
                    }
                }
            }
            Response.Redirect(Request.Url.AbsoluteUri);
        }
        else
        {
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select a File');</script>");
        }
    }

    protected void DownloadFile(object sender, EventArgs e)
    {
        int ID = int.Parse((sender as LinkButton).CommandArgument);
        byte[] bytes = null;
        //byte[] bytes ;
        string fileName, contentType;
        string constr = Globals.ConnString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                //cmd.CommandText = "select FileName, FileSubject, Div_Code, Update_dtm,Data,ContentType " +
                //        "  from file_info " +
                //        "  where ID = '" + ID + "'";

                cmd.CommandText = "SELECT FileName, FileSubject, Div_Code, Update_dtm,Data,ContentType " +
                        "  from file_info " +
                        "  where ID = @ID ";

                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Connection = con;
                con.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    sdr.Read();
                    if (!Convert.IsDBNull(sdr["Data"]))
                    {
                        bytes = (byte[])sdr["Data"];
                    }
                                   
                    contentType = sdr["ContentType"].ToString();
                    fileName = sdr["FileName"].ToString();
                }
                con.Close();
            }           
        }
        Response.Clear();
        Response.Buffer = true;
        Response.Charset = "";
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.ContentType = contentType;
        Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName);
        Response.BinaryWrite(bytes);
        Response.Flush();
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
                string constr = Globals.ConnString;
                using (SqlConnection con = new SqlConnection(constr))
                {

                    int ID = Convert.ToInt32(e.CommandArgument);
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = "delete from file_info where ID=@ID";
                    cmd.Connection = con;
                    cmd.Parameters.Add(new SqlParameter("@ID", ID));
                    con.Open();
                    cmd.ExecuteNonQuery();
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Deleted Successfully');</script>");
                    //  BindGridviewData();
                    BindGridviewData();
                    con.Close();
                }
            }
            catch (Exception ex)
            {

            }
        }

      

    }
}