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

public partial class MasterFiles_Options_Usermanual_Upload : System.Web.UI.Page
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
                        "  from UserManual " +
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
        if (fileUpload1.HasFile)
        {
            string filename = Path.GetFileName(fileUpload1.PostedFile.FileName);
            string subject = txtFileSubject.Text.Trim();
            string contentType = fileUpload1.PostedFile.ContentType;
            // string Data = string.Empty;
            fileUpload1.SaveAs(Server.MapPath("~/MasterFiles/Options/Files/" + filename));
            using (Stream fs = fileUpload1.PostedFile.InputStream)
            {
                using (BinaryReader br = new BinaryReader(fs))
                {
                    byte[] bytes = br.ReadBytes((Int32)fs.Length);
                    string constr = Globals.ConnString;
                    using (SqlConnection con = new SqlConnection(constr))
                    {
                        //string query = "INSERT INTO file_info(FileName,FileSubject,Div_Code,Update_dtm,Data, ContentType) " +
                        //     " VALUES ( '" + filename + "' , '" + txtFileSubject.Text.Trim() + "', '" + div_code + "',  getdate(),'"+Data+"','" + ContentType + "') ";
                        SqlCommand cmd1 = new SqlCommand("SELECT isnull(max(Id)+1,'1') Id from UserManual", con);

                        SqlDataAdapter daimage = new SqlDataAdapter(cmd1);
                        DataSet dsimage = new DataSet();
                        daimage.Fill(dsimage);
                        string query = "insert into UserManual values ('" + dsimage.Tables[0].Rows[0][0].ToString() + "',@FileName, @FileSubject,@div_code, getdate(),@ContentType,@Data)";
                        using (SqlCommand cmd = new SqlCommand(query))
                        {
                            cmd.Connection = con;
                            cmd.Parameters.AddWithValue("@FileName", filename);
                            cmd.Parameters.AddWithValue("@FileSubject", subject);
                            cmd.Parameters.AddWithValue("@div_code", div_code);

                            cmd.Parameters.AddWithValue("@ContentType", contentType);
                            cmd.Parameters.AddWithValue("@Data", bytes);
                            con.Open();
                            cmd.ExecuteNonQuery();
                            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Uploaded Successfully');</script>");
                            con.Close();
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
        byte[] bytes;
        string fileName, contentType;
        string constr = Globals.ConnString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "select FileName, FileSubject, Div_Code, Update_dtm,Data,ContentType " +
                        "  from UserManual " +
                        "  where ID = '" + ID + "' and div_Code = '" + div_code + "'";
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Connection = con;
                con.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    sdr.Read();
                    bytes = (byte[])sdr["Data"];
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
        Response.AddHeader("Content-Disposition", "attachment;filename=\"" + fileName + "\"");
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
                    cmd.CommandText = "delete from UserManual where ID=@ID and div_Code = '" + div_code + "'";
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