using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using Bus_EReport;
using DocumentFormat.OpenXml.Drawing;
using System.IO;
using System.Data.SqlClient;
using Newtonsoft.Json;
using System.Web.Services;
using DBase_EReport;

public partial class MasterFiles_gift_master : System.Web.UI.Page
{
    DataSet dsPBrd = null; string img;
    string giftCode = string.Empty;
    string divcode = string.Empty;
    string giftnm = string.Empty;
    string pov = string.Empty;
    string gftval = string.Empty;
    string fromdt = string.Empty;
    string todt = string.Empty;
    public string filename = string.Empty;
    string serverfolder = string.Empty;
    string serverpath = string.Empty;
    string filetype = string.Empty;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int time;
    protected void Page_Load(object sender, EventArgs e)
    {
        divcode = Convert.ToString(Session["div_code"]);
        giftCode = Request.QueryString["gift_code"];
        gft_name.Focus();
        if (!Page.IsPostBack)
        {
            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);
        
            if (giftCode != "" && giftCode != null)
            {
                gftmas dv = new gftmas();
                dsPBrd = dv.getgftdtl(divcode, giftCode);

                if (dsPBrd.Tables[0].Rows.Count > 0)
                {
                    gft_name.Text = dsPBrd.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                    point_val.Text = dsPBrd.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                    val_gft.Text = dsPBrd.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();
                    efecdtf.Text = dsPBrd.Tables[0].Rows[0].ItemArray.GetValue(3).ToString();
                    efecdt.Text = dsPBrd.Tables[0].Rows[0].ItemArray.GetValue(4).ToString();
                    prodImg.ImageUrl = "GImage" + "/" + dsPBrd.Tables[0].Rows[0].ItemArray.GetValue(5).ToString();
                    imgid.Value = "GImage" + "/" + prodImg.ImageUrl; ;
               }

            }
            //menu1.Title = this.Page.Title;
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        giftnm = gft_name.Text;
        pov = point_val.Text;
        gftval = val_gft.Text;
        fromdt = efecdtf.Text;
        todt = efecdt.Text;

        if (fileup2.HasFile)
        {
            filename = System.IO.Path.GetFileName(fileup2.PostedFile.FileName);
            string imagepath = Server.MapPath(@"GImage\" + filename);
            string filetype = System.IO.Path.GetExtension(fileup2.FileName);
            if (filetype.ToLower() == ".jpg" || filetype.ToLower() == ".bmp" || filetype.ToLower() == ".gif" || filetype.ToLower() == ".ico" || filetype.ToLower() == ".jpeg" || filetype.ToLower() == ".png")
            {
                switch (filetype)
                {
                    case ".bmp":
                    case ".gif":
                    case ".ico":
                    case ".jpg":
                    case ".jpeg":
                    case ".png":

                        serverfolder = Server.MapPath(@"GImage\");

                        if (!Directory.Exists(serverfolder))
                        {
                            Directory.CreateDirectory(serverfolder);
                        }
                        serverpath = serverfolder + System.IO.Path.GetFileName(filename);
                        fileup2.SaveAs(serverpath);
                        //pimage.Text += "[" + fileup2.FileName + "]- Image file uploaded  successfully <br/>";
                        filetype = "I";
                        break;
                }
                ImgDisplay(filename);
            }
        }
        else
        {
            filename = imgid.Value;
        }
        if (giftCode == null)
        {
            gftmas dv = new gftmas();
        int iReturn = dv.gft_RecordAdd(divcode, giftnm, pov, gftval, fromdt, todt, filename);

        if (iReturn > 0)
        {
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Created Successfully');</script>");
            Resetall();
        }

        else if (iReturn == -2)
        {
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Brand Name Already Exist');</script>");
                gft_name.Focus();
        }

    
    }
        else
        {
            //Update product Brand
            gftmas dv = new gftmas();
            int iReturn = dv.Gift_RecordUpdates(giftCode,divcode, giftnm, pov, gftval, fromdt, todt, filename);
            if (iReturn > 0)
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Updated Successfully');window.location='view_gift_master.aspx';</script>");
            }
          }

    }
    private void Resetall()
    {
        gft_name.Text = "";
        point_val.Text = "";
        val_gft.Text = "";
        efecdt.Text = "";
        efecdt.Text = "";
      }
    protected void ImgDisplay(string filename)
    {
        prodImg.ImageUrl = @"GImage\" + filename;
    }
    public class gftmas
    {
        public int gft_RecordAdd(string divcode, string giftnm, string pov, string gftval, string fromdt, string todt, string filename)
        {
            int iReturn = -1;
            if (!RecordExist_Brd(giftnm, divcode))
            {
                if (!RecordExist_Brd(giftnm, divcode))
                {
                    try
                    {
                        DB_EReporting db = new DB_EReporting();
                        string strQry = "SELECT isnull(max(Gift_Code)+1,'1') Gift_Code from Gift_Master";
                        int Gift_Code = db.Exec_Scalar(strQry);

                        strQry = "INSERT INTO Gift_Master(Gift_Code,Gift_Name,Points_Value,Gift_val,Effective_from,Effective_To,Gift_image,Division_code,created_date,Active_flag,Last_upt_date)" +
                                "values('" + Gift_Code + "','" + giftnm + "', '" + pov + "','" + gftval + "',convert(datetime,'" + fromdt + "',105),convert(datetime,'" + todt + "',105),'" + filename + "','" + divcode + "',getdate(),0,getdate())";


                        iReturn = db.ExecQry(strQry);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
                else
                {
                    iReturn = -2;
                }
            }
            else
            {
                iReturn = -3;
            }
            return iReturn;
        }
        public bool RecordExist_Brd(string giftnm, string div_code)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                string strQry = "SELECT COUNT(Gift_Name) FROM Gift_Master WHERE Gift_Name='" + giftnm + "' and Division_Code = '" + div_code + "' ";
                int iRecordExist = db.Exec_Scalar(strQry);

                if (iRecordExist > 0)
                    bRecordExist = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return bRecordExist;
        }
        public int Gift_RecordUpdates(string giftCode, string divcode, string giftnm, string pov, string gftval, string fromdt, string todt, string filename)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                string strQry = "UPDATE Gift_Master " +
                         " SET Gift_Name = '" + giftnm + "', " +
                         " Points_Value = '" + pov + "' ," +
                         " Gift_val = '" + gftval + "' ," +
                         " Effective_from = convert(datetime,'" + fromdt + "',105) ," +
                          " Effective_To = convert(datetime,'" + todt + "',105) ," +
                         " Gift_image = '" + filename + "' ," +
                         " Last_upt_date = getdate() ," +
                         " created_date = getdate() " +
                         " WHERE Gift_Code = '" + giftCode + "' and Active_flag = 0 and Division_code='" + divcode + "'";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
         return iReturn;
            
        }
        public DataSet getgftdtl(string divcode, string giftCode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProBrd = null;

            string strQry = " select Gift_Name,Points_Value,Gift_val,convert(varchar,Effective_from,103)Effective_from, convert(varchar,Effective_To,103)Effective_To, " +
                             " Gift_image from Gift_Master where Gift_Code ='" + giftCode + "' and Division_code = '" + divcode + "' group by Gift_Name,Points_Value,Gift_val," +
                             " Effective_from,Effective_To,Gift_image order by 2";
            try 
            {
                dsProBrd = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProBrd;
        }

    }
}