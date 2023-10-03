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

public partial class MasterFiles_MR_UserManual_View : System.Web.UI.Page
{
    DataSet dsSalesForce = null;
    DataSet dsAdmin = null;
    string FileName = string.Empty;

    string div_code = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        if (!Page.IsPostBack)
        {
            //menu1.Title = this.Page.Title;
            //menu1.FindControl("btnBack").Visible = false;
            BindGridviewData();
            if (Session["sf_type"].ToString() == "1")
            {

                UserControl_MR_Menu c1 =
                    (UserControl_MR_Menu)LoadControl("~/UserControl/MR_Menu.ascx");
                Divid.Controls.Add(c1);
                c1.FindControl("btnBack").Visible = false;
                c1.Title = this.Page.Title;


            }
            else if (Session["sf_type"].ToString() == "2")
            {
                UserControl_MGR_Menu c1 =
                (UserControl_MGR_Menu)LoadControl("~/UserControl/MGR_Menu.ascx");
                Divid.Controls.Add(c1);
                c1.FindControl("btnBack").Visible = false;
                c1.Title = this.Page.Title;
            }
        }
    }
    private void BindGridviewData()
    {
        AdminSetup adsp = new AdminSetup();
        dsSalesForce = adsp.Get_UserManual(div_code);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            gvDetails.DataSource = dsSalesForce;
            gvDetails.DataBind();

        }
        else
        {
            gvDetails.DataSource = dsSalesForce;
            gvDetails.DataBind();
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
                cmd.CommandText = "select ID, FileName, FileSubject, Div_Code, Update_dtm,Data,ContentType " +
                        "  from UserManual " +
                        "  where ID = '" + ID + "'";
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
        //   Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName);
        Response.AddHeader("Content-Disposition", "attachment;filename=\"" + fileName + "\"");
        Response.BinaryWrite(bytes);
        Response.Flush();
        Response.End();
    }
}