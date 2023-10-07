using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
using System.Configuration;
using System.IO;
using System.Data.SqlClient;
using Newtonsoft.Json;
using System.Web.Services;
using DBase_EReport;

public partial class MasterFiles_ProductDetail : System.Web.UI.Page
{
    DataSet dsPro = null;
    public string ProdCode = string.Empty;
    public static string div_code = string.Empty;
    DataSet dsProduct = null;
    DataSet dsProduct1 = null;
    DataSet dsDivision = null;
    DataSet dsSubDivision = null;
    DataSet dsState = null;
    DataSet dsImg = null;
    string[] statecd;
    string contenttype = string.Empty;
    string state_cd = string.Empty;
    string sState = string.Empty;
    string state_code = string.Empty;
    string sChkLocation = string.Empty;
    string sChkLocation1 = string.Empty;
    int iIndex;
    string subdivision_code = string.Empty;
    string sub_division = string.Empty;
    string Prod_mode = string.Empty;
    string sam_Erp = string.Empty;
    string sale_Erp = string.Empty;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    string ddlmode = string.Empty;
    string filename = string.Empty;
    string filstrname = string.Empty;
    string serverfolder = string.Empty;
    string serverpath = string.Empty;
    string filetype = string.Empty;
    string strCon = Globals.ConnString;
    int time;
    string imgname = string.Empty;
    string sf_type = string.Empty;
    string aft = string.Empty;
    private IFormatProvider provider;

    protected override void OnPreInit(EventArgs e)
    {
        base.OnPreInit(e);
        sf_type = Session["sf_type"].ToString();
        if (sf_type == "3")
        {
            this.MasterPageFile = "~/Master.master";
        }
        else if (sf_type == "2")
        {
            this.MasterPageFile = "~/Master_MGR.master";
        }
        else if (sf_type == "1")
        {
            this.MasterPageFile = "~/Master_MR.master";
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["backurl"] = "ProductList.aspx";
        div_code = Session["div_code"].ToString();
        ProdCode = Request.QueryString["Product_Detail_Code"];
        txtProdDetailCode.Focus();
        if (!IsPostBack)
        {
            BindGridviewData();
           // Bindprodlunch();
        }
        if (!Page.IsPostBack)
        {

            FillCategory();
            FillGroup();
            FillBrand();

            //menu1.Title = this.Page.Title;
            Product dv = new Product();
            dsPro = dv.getProdforCode(div_code, ProdCode);
            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);
            if (dsPro.Tables[0].Rows.Count > 0)
            {
                txtProdDetailCode.Text = dsPro.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                txtProdDetailName.Text = dsPro.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                ddlbaseunit.SelectedValue = dsPro.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();
                string u1 = dsPro.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();
                ddlunit.SelectedValue = dsPro.Tables[0].Rows[0].ItemArray.GetValue(3).ToString();
                string u2 = dsPro.Tables[0].Rows[0].ItemArray.GetValue(3).ToString();
                Txt_UOM.Text = dsPro.Tables[0].Rows[0].ItemArray.GetValue(4).ToString();
                if (u1 == u2)
                {
                    Txt_UOM.Text = "1";
                    Txt_UOM.Enabled = false;
                }
                else
                {
                    Txt_UOM.Text = dsPro.Tables[0].Rows[0].ItemArray.GetValue(4).ToString();
                    Txt_UOM.Enabled = true;
                }
                ddlCat.SelectedValue = dsPro.Tables[0].Rows[0].ItemArray.GetValue(5).ToString();
                ddlGroup.SelectedValue = dsPro.Tables[0].Rows[0].ItemArray.GetValue(6).ToString();
                ddlBrand.SelectedValue = dsPro.Tables[0].Rows[0].ItemArray.GetValue(7).ToString();//Brand

                RblType.SelectedValue = dsPro.Tables[0].Rows[0].ItemArray.GetValue(8).ToString();
                //ddlmode.SelectedValue = dsPro.Tables[0].Rows[0].ItemArray.GetValue(9).ToString();
                txtProdDesc.Text = dsPro.Tables[0].Rows[0].ItemArray.GetValue(10).ToString();
                txtPacksize.Text = dsPro.Tables[0].Rows[0].ItemArray.GetValue(11).ToString();
                txtGrosswt.Text = dsPro.Tables[0].Rows[0].ItemArray.GetValue(12).ToString();
                txtNetwt.Text = dsPro.Tables[0].Rows[0].ItemArray.GetValue(13).ToString();
                txtsale.Text = dsPro.Tables[0].Rows[0].ItemArray.GetValue(14).ToString();


                state_code = dsPro.Tables[0].Rows[0].ItemArray.GetValue(15).ToString();
                subdivision_code = dsPro.Tables[0].Rows[0].ItemArray.GetValue(16).ToString(); // Sub Division

                txttarget.Text = dsPro.Tables[0].Rows[0].ItemArray.GetValue(17).ToString();
                txtProdShortName.Text = dsPro.Tables[0].Rows[0].ItemArray.GetValue(18).ToString() == "" ? dsPro.Tables[0].Rows[0].ItemArray.GetValue(1).ToString() : dsPro.Tables[0].Rows[0].ItemArray.GetValue(18).ToString();
                Txt_Hsn.Text = dsPro.Tables[0].Rows[0].ItemArray.GetValue(19).ToString();
                Txtunitwg.Text = dsPro.Tables[0].Rows[0].ItemArray.GetValue(20).ToString();
                Txtprovalid.Text = dsPro.Tables[0].Rows[0].ItemArray.GetValue(21).ToString();
                imgname = dsPro.Tables[0].Rows[0].ItemArray.GetValue(22).ToString();
                txtProdDetailCode.Enabled = false;

                FillCheckBoxList();
            }
            else
            {
                ListedDR frl = new ListedDR();
                Division comp = new Division();
                DataSet div = comp.getDivision(div_code);
                DataSet dsPro1 = frl.GetProduct_Detail_Code("", "");
                txtProdDetailCode.Text = div.Tables[0].Rows[0].ItemArray.GetValue(0).ToString().ToUpper() + dsPro1.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                txtProdDetailCode.Enabled = false;
            }
            FillUOM();
            FillCheckBoxList();
            FillCheckBoxList_New();
            ImgDisplay(imgname);

        }
    }
	 protected void fold_SelectedIndexChanged(object sender, EventArgs e)
    {
       

        string selectedValue = fold.SelectedValue;
        string selectedText = fold.SelectedItem.Text;
        if (selectedValue == "1")
        {
            BindGridviewData();
        }
        else if (selectedValue == "2")
        {
            DataTable dt = new DataTable();
            gvDetails.DataSource = dt;
            gvDetails.DataBind();
        }
        else if (selectedValue == "3")
        {
            Bindprodlunch();
        }

       
      //  ResultLabel.Text = "Selected Value: " + selectedValue + "<br />Selected Text: " + selectedText;
    }
    private void Bindprodlunch()
    {
        using (SqlConnection con = new SqlConnection(strCon))
        {

            con.Open();
            using (SqlCommand cmd = new SqlCommand("select ID,File_Name,File_Type,convert(varchar(10),Effective_From,105)as Effect_From,convert(varchar(10),Effective_To,105) as Effect_To from Product_New_Launch where Prod_Code='" + ProdCode + "' and convert(date,Effective_To)>=Convert(date,Getdate())"))
            {
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        //gvDetails.DataSource = dt;
                        //gvDetails.DataBind();
                        if (dt.Rows.Count != 0)
                        {
                            gvDetails.DataSource = dt;
                            gvDetails.DataBind();
                            string value = "3";
                            var item = fold.Items.FindByValue(value);
                            item.Selected = true;
                        }

                    }
                }
            }


            con.Close();
        }
    }

    private void BindGridviewData()
    {
        using (SqlConnection con = new SqlConnection(strCon))
        {
            con.Open();
            using (SqlCommand cmd = new SqlCommand("select ID,File_Name,File_Type,convert(varchar(10),Effect_From,105)as Effect_From,convert(varchar(10),Effect_To,105) as Effect_To from Product_Td_Detailing where Prod_Code='" + ProdCode + "' and  isnull(File_status,0)=0 and convert(date,Effect_To)>=Convert(date,Getdate())"))
            {
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        //gvDetails.DataSource = dt;
                        //gvDetails.DataBind();
                        if (dt.Rows.Count != 0)
                        {
                            gvDetails.DataSource = dt;
                            gvDetails.DataBind();
                            string value = "1";
                            var item = fold.Items.FindByValue(value);
                            item.Selected = true;
                        }

                    }
                }
            }


            con.Close();
        }
    }
    private void BindGriddelviewData()
    {
        using (SqlConnection con = new SqlConnection(strCon))
        {
            con.Open();
            using (SqlCommand cmd = new SqlCommand("select ID,File_Name,File_Type,convert(varchar(10),Effect_From,105)as Effect_From,convert(varchar(10),Effect_To,105) as Effect_To from Product_Td_Detailing where Prod_Code='" + ProdCode + "' and  isnull(File_status,0)=0 and convert(date,Effect_To)>=Convert(date,Getdate())"))
            {
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        gvDetails.DataSource = dt;
                       gvDetails.DataBind();
                        if (dt.Rows.Count != 0)
                        {
                            gvDetails.DataSource = dt;
                            gvDetails.DataBind();
                            string value = "1";
                            var item = fold.Items.FindByValue(value);
                            item.Selected = true;
                        }

                    }
                }
            }


            con.Close();
        }
    }
    private void Binddeleprodlunch()
    {
        using (SqlConnection con = new SqlConnection(strCon))
        {

            con.Open();
            using (SqlCommand cmd = new SqlCommand("select ID,File_Name,File_Type,convert(varchar(10),Effective_From,105)as Effect_From,convert(varchar(10),Effective_To,105) as Effect_To from Product_New_Launch where Prod_Code='" + ProdCode + "' and convert(date,Effective_To)>=Convert(date,Getdate())"))
            {
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        gvDetails.DataSource = dt;
                        gvDetails.DataBind();
                        if (dt.Rows.Count != 0)
                        {
                            gvDetails.DataSource = dt;
                            gvDetails.DataBind();
                            string value = "3";
                            var item = fold.Items.FindByValue(value);
                            item.Selected = true;
                        }

                    }
                }
            }


            con.Close();
        }
    }
    public int getdetailing()
    {
        DB_EReporting db = new DB_EReporting();

        DataSet ds = db.Exec_DataSet("select count(Prod_Code)Numbers from Product_Td_Detailing where Prod_Code='" + ProdCode + "' and  isnull(File_status,0)=0 and convert(date,Effect_To)>=Convert(date,Getdate())");
        int routenos = Convert.ToInt16(ds.Tables[0].Rows[0]["Numbers"]);
        return routenos;
    }

    public int getlaunchitm()
    {
        DB_EReporting db = new DB_EReporting();
        DataSet ds = db.Exec_DataSet("select count(Prod_Code)Numbers from Product_New_Launch where Prod_Code='" + ProdCode + "' and convert(date,Effective_To)>=Convert(date,Getdate()) ");
        int routenos = Convert.ToInt16(ds.Tables[0].Rows[0]["Numbers"]);
        return routenos;
    }

    protected void OnLnkUpload_Click(object sender, EventArgs e)

    {
        label12.Text = "";
        try
        {
            string selectedfold = fold.SelectedItem.Text;
            if (selectedfold == "Others")
            {
                filename = Path.GetFileName(fileUpload1.PostedFile.FileName);
                string filetype = Path.GetExtension(fileUpload1.FileName);
                if (filetype.ToLower() == ".docx" || filetype.ToLower() == ".pdf" || filetype.ToLower() == ".txt" || filetype.ToLower() == ".doc" || filetype.ToLower() == ".mp3" || filetype.ToLower() == ".mp4" || filetype.ToLower() == ".jpg" || filetype.ToLower() == ".bmp" || filetype.ToLower() == ".gif" || filetype.ToLower() == ".ico" || filetype.ToLower() == ".jpeg" || filetype.ToLower() == ".png" || filetype.ToLower() == ".wmf" || filetype.ToLower() == ".wav" || filetype.ToLower() == ".avi" || filetype.ToLower() == ".wmv" || filetype.ToLower() == ".flv" || filetype.ToLower() == ".mpg" || filetype.ToLower() == ".mpeg" || filetype.ToLower() == ".mov")
                {
                    switch (filetype)
                    {
                        case ".doc":
                        case ".docx":
                            serverfolder = Server.MapPath(@"Files\document\");

                            if (!Directory.Exists(serverfolder))
                            {
                                // create Folder
                                Directory.CreateDirectory(serverfolder);
                            }
                            serverpath = serverfolder + Path.GetFileName(filename);
                            fileUpload1.SaveAs(serverpath);
                            label12.Text += "[" + fileUpload1.FileName + "]- document file uploaded  successfully<br/>";
                            contenttype = "application/msword";

                            break;
                        case ".pdf":
                            serverfolder = Server.MapPath(@"Files\pdf\");

                            if (!Directory.Exists(serverfolder))
                            {

                                Directory.CreateDirectory(serverfolder);
                            }
                            serverpath = serverfolder + Path.GetFileName(filename);
                            fileUpload1.SaveAs(serverpath);
                            label12.Text += "[" + fileUpload1.FileName + "]- pdf file uploaded  successfully<br/>";
                            contenttype = "application/pdf";
                            break;

                        case ".txt":
                            serverfolder = Server.MapPath(@"Files\text_document\");

                            if (!Directory.Exists(serverfolder))
                            {

                                Directory.CreateDirectory(serverfolder);
                            }
                            serverpath = serverfolder + Path.GetFileName(filename);
                            fileUpload1.SaveAs(serverpath);
                            label12.Text += "[" + fileUpload1.FileName + "]- text_document file uploaded  successfully <br/>";
                            contenttype = "text/plain";
                            break;

                        case ".mp3":
                        case ".wav":

                            serverfolder = Server.MapPath(@"Files\MP3\");

                            if (!Directory.Exists(serverfolder))
                            {

                                Directory.CreateDirectory(serverfolder);
                            }
                            serverpath = serverfolder + Path.GetFileName(filename);
                            fileUpload1.SaveAs(serverpath);
                            label12.Text += "[" + fileUpload1.FileName + "]- Audio file uploaded  successfully <br/>";
                            filetype = "A";
                            contenttype = "audio/mpeg";
                            break;

                        case ".mp4":
                        case ".avi":
                        case ".wmv":
                        case ".flv":
                        case ".mpg":
                        case ".mpeg":
                        case ".mov":

                            serverfolder = Server.MapPath(@"Files\MP4\");

                            if (!Directory.Exists(serverfolder))
                            {

                                Directory.CreateDirectory(serverfolder);
                            }
                            serverpath = serverfolder + Path.GetFileName(filename);
                            fileUpload1.SaveAs(serverpath);
                            label12.Text += "[" + fileUpload1.FileName + "]- Video file uploaded  successfully <br/>";
                            filetype = "V";
                            contenttype = "video/mp4";
                            break;

                        case ".bmp":
                        case ".gif":
                        case ".ico":
                        case ".jpg":
                        case ".jpeg":
                        case ".png":

                            serverfolder = Server.MapPath(@"Files\JPG\");

                            if (!Directory.Exists(serverfolder))
                            {

                                Directory.CreateDirectory(serverfolder);
                            }
                            serverpath = serverfolder + Path.GetFileName(filename);
                            fileUpload1.SaveAs(serverpath);
                            label12.Text += "[" + fileUpload1.FileName + "]- Image file uploaded  successfully <br/>";
                            filetype = "I";
                            contenttype = "image/jpeg";
                            break;
                    }
                }
                else
                {
                    fileUpload1.SaveAs(Server.MapPath("Files/" + filename));
                    //Response.Write("File uploaded sucessfully.");
                    label12.Text = "[" + fileUpload1.FileName + "]-  file uploaded  successfully <br/>";
                    contenttype = "application/notype";
                }
            }
            else if (selectedfold == "Detailing")
            {
                //int details = getlaunchitm();
                //if (details > 0)
                //{
                //    string message = "These Product already have Product launch..So cannot insert the Detailing.";
                //    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                //    sb.Append("<script type = 'text/javascript'>");
                //    sb.Append("window.onload=function(){");
                //    sb.Append("alert('");
                //    sb.Append(message);
                //    sb.Append("')};");
                //    sb.Append("</script>");
                //    ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", sb.ToString());
                //    return;
                //}
                string StartsDate = Request.Form["txtFrom"];

                string EndsDate = Request.Form["txtTo"];
                filename = Path.GetFileName(fileUpload1.PostedFile.FileName);
                if (StartsDate == "" || EndsDate == "" || filename == "")
                {
                    string message = "Please select the upload file and Effective date from and to both.";
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    sb.Append("<script type = 'text/javascript'>");
                    sb.Append("window.onload=function(){");
                    sb.Append("alert('");
                    sb.Append(message);
                    sb.Append("')};");
                    sb.Append("</script>");
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", sb.ToString());
                    return;
                }


                filename = Path.GetFileName(fileUpload1.PostedFile.FileName);
                string filetype = Path.GetExtension(fileUpload1.FileName);
                switch (filetype)
                {
                    case ".doc":
                    case ".docx":
                        contenttype = "application/msword";
                        break;
                    case ".pdf":
                        contenttype = "application/pdf";
                        break;

                    case ".txt":
                        contenttype = "text/plain";
                        break;
                    case ".mp3":
                    case ".wav":
                        filetype = "A";
                        contenttype = "audio/mpeg";
                        break;

                    case ".mp4":
                    case ".avi":
                    case ".wmv":
                    case ".flv":
                    case ".mpg":
                    case ".mpeg":
                    case ".mov":
                        filetype = "V";
                        contenttype = "video/mp4";
                        break;

                    case ".bmp":
                    case ".gif":
                    case ".ico":
                    case ".jpg":
                    case ".jpeg":
                    case ".png":
                        filetype = "I";
                        contenttype = "image/jpeg";
                        break;
                }
                serverfolder = Server.MapPath(@"Files\Detailing\");

                if (!Directory.Exists(serverfolder))
                {

                    Directory.CreateDirectory(serverfolder);
                }
                serverpath = serverfolder + Path.GetFileName(filename);
                fileUpload1.SaveAs(serverpath);
                label12.Text += "[" + fileUpload1.FileName + "]- Detailing file uploaded  successfully <br/>";
                using (SqlConnection con = new SqlConnection(strCon))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = "insert into Product_Td_Detailing(Prod_Code,File_Name,File_Type,Effect_From,Effect_To,Content_Type) values(@Pro_code,@Name,@Type,@From,@To,@CType)";
                        try
                        {
                            cmd.Parameters.AddWithValue("@Pro_code", txtProdDetailCode.Text);
                        }
                        catch
                        {
                            cmd.Parameters.AddWithValue("@Pro_code", ProdCode);
                        }
                        cmd.Parameters.AddWithValue("@Name", filename);
                        cmd.Parameters.AddWithValue("@Type", filetype);
                        cmd.Parameters.AddWithValue("@CType", contenttype);
                        System.Globalization.CultureInfo enUS = new System.Globalization.CultureInfo("en-US");
                        DateTime paramFromDate;
                        DateTime paramToDate;
                        string StartDate = Convert.ToDateTime(Request.Form["txtFrom"]).ToString("MM/dd/yyyy");
                        string EndDate = Convert.ToDateTime(Request.Form["txtTo"]).ToString("MM/dd/yyyy");
                        Boolean goodStartDate = false;
                        Boolean goodEndDate = false;
                        goodStartDate
                            = DateTime.TryParseExact(StartDate, "MM/dd/yyyy", enUS,
                                                     System.Globalization.DateTimeStyles.None,
                                                     out paramFromDate);
                        goodEndDate
                            = DateTime.TryParseExact(EndDate, "MM/dd/yyyy", enUS,
                                                     System.Globalization.DateTimeStyles.None,
                                                     out paramToDate);
                        if (goodStartDate) Console.WriteLine(paramFromDate);
                        if (goodEndDate) Console.WriteLine(paramToDate);
                        if (goodStartDate && goodEndDate)
                        {
                            String FromDate
                                = paramFromDate.ToString("yyyy-MM-dd HH:mm:ss.fff",
                                                         System.Globalization.CultureInfo.InvariantCulture);
                            String ToDate
                                = paramToDate.ToString("yyyy-MM-dd HH:mm:ss.fff",
                                                       System.Globalization.CultureInfo.InvariantCulture);
                            cmd.Parameters.AddWithValue("@From", FromDate);
                            cmd.Parameters.AddWithValue("@fstatus", 0);
                            cmd.Parameters.AddWithValue("@To", ToDate);
                            cmd.Connection = con;
                            con.Open();
                            cmd.ExecuteNonQuery();
                            con.Close();
                            BindGridviewData();
                        }
                    }
                }

            }
            else if (selectedfold == "Product Launch")
            {
                int coun = 0;
                 //int details = getdetailing();
                //if (details > 0)
                //{
                //    string message = "These Product already have detailing..So cannot insert the product Launch.";
                //    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                //    sb.Append("<script type = 'text/javascript'>");
                //    sb.Append("window.onload=function(){");
                //    sb.Append("alert('");
                //    sb.Append(message);
                //    sb.Append("')};");
                //    sb.Append("</script>");
                //    ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", sb.ToString());
                //    return;
                //}
                string StartsDate = Request.Form["txtFrom"];
                string EndsDate = Request.Form["txtTo"];
				filstrname = Path.GetFileName(fileUpload1.PostedFile.FileName);
                if (StartsDate == "" || EndsDate == "" || filstrname == "")
                {
                    string message = "Please select the upload file and Effective date from and to both.";
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    sb.Append("<script type = 'text/javascript'>");
                    sb.Append("window.onload=function(){");
                    sb.Append("alert('");
                    sb.Append(message);
                    sb.Append("')};");
                    sb.Append("</script>");
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", sb.ToString());
                    return;
                }

                
                DateTime myDates = Convert.ToDateTime((DateTime.Parse(EndsDate)).ToString("dd/M/yyyy"));
                DateTime today = Convert.ToDateTime(DateTime.Now.ToString("dd/M/yyyy"));
                if (today > myDates)
                {
                    string message = "The Effective date should be greater than today.";
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    sb.Append("<script type = 'text/javascript'>");
                    sb.Append("window.onload=function(){");
                    sb.Append("alert('");
                    sb.Append(message);
                    sb.Append("')};");
                    sb.Append("</script>");
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", sb.ToString());
                    return;
                }


                

                //filstrname = Path.GetFileName(fileUpload1.PostedFile.FileName);
                //string filetype = Path.GetExtension(fileUpload1.FileName);

                //filename = ProdCode + "_" + DateTime.Now.ToString("yyyy-MM-dd") + "" + filetype;
                //serverfolder = Server.MapPath(@"Files\ProductLaunch\");

                //if (!Directory.Exists(serverfolder))
                //{
                //    Directory.CreateDirectory(serverfolder);
                //}
                //serverpath = serverfolder + Path.GetFileName(filename);

                //fileUpload1.SaveAs(serverpath);

                foreach (HttpPostedFile postedFile in fileUpload1.PostedFiles) 
                {
                    string fileName = Path.GetFileName(postedFile.FileName);
                    
                    string filetype = Path.GetExtension(postedFile.FileName);
                    //filename = ProdCode + "_" + DateTime.Now.ToString("yyyy_MM_dd_hh_mm_ss")  + "" + filetype;
                   filename = ProdCode + "_" + DateTime.Now.Ticks.ToString() + "" + filetype;
                    //postedFile.SaveAs(Server.MapPath("~/Masterfiles/Files/ProductLaunch/") + fileName);
                    postedFile.SaveAs(Server.MapPath("~/Masterfiles/Files/ProductLaunch/") + filename);
                    //postedFile.SaveAs(serverpath);
                    coun++;



                    //label12.Text += "[" + fileUpload1.FileName + "]- Product Launch file uploaded  successfully <br/>";
                    //filetype = "PL";
                    using (SqlConnection con = new SqlConnection(strCon))
                    {
                        using (SqlCommand cmd = new SqlCommand())
                        {
                            cmd.CommandText = "insert into Product_New_Launch(ID,Prod_Code,File_Name,File_Type,Effective_From,Effective_To,Content_Type,division_code) values((select max(id)+1 from Product_New_Launch),@Pro_code,@Name,@Type,@From,@To,@CType,@div)";
                            try
                            {
                                cmd.Parameters.AddWithValue("@Pro_code", txtProdDetailCode.Text);
                            }
                            catch
                            {
                                cmd.Parameters.AddWithValue("@Pro_code", ProdCode);
                            }
                            cmd.Parameters.AddWithValue("@Name", filename);
                            cmd.Parameters.AddWithValue("@Type", filetype);
                            cmd.Parameters.AddWithValue("@CType", contenttype);
                            cmd.Parameters.AddWithValue("@div", div_code);
                            System.Globalization.CultureInfo enUS = new System.Globalization.CultureInfo("en-US");
                            DateTime paramFromDate;
                            DateTime paramToDate;
                            string StartDate = Convert.ToDateTime(Request.Form["txtFrom"]).ToString("MM/dd/yyyy");
                            string EndDate = Convert.ToDateTime(Request.Form["txtTo"]).ToString("MM/dd/yyyy");
                            Boolean goodStartDate = false;
                            Boolean goodEndDate = false;
                            goodStartDate
                                = DateTime.TryParseExact(StartDate, "MM/dd/yyyy", enUS,
                                                         System.Globalization.DateTimeStyles.None,
                                                         out paramFromDate);
                            goodEndDate
                                = DateTime.TryParseExact(EndDate, "MM/dd/yyyy", enUS,
                                                         System.Globalization.DateTimeStyles.None,
                                                         out paramToDate);
                            if (goodStartDate) Console.WriteLine(paramFromDate);
                            if (goodEndDate) Console.WriteLine(paramToDate);
                            if (goodStartDate && goodEndDate)
                            {
                                String FromDate
                                    = paramFromDate.ToString("yyyy-MM-dd HH:mm:ss.fff",
                                                             System.Globalization.CultureInfo.InvariantCulture);
                                String ToDate
                                    = paramToDate.ToString("yyyy-MM-dd HH:mm:ss.fff",
                                                           System.Globalization.CultureInfo.InvariantCulture);
                                cmd.Parameters.AddWithValue("@From", FromDate);
                                cmd.Parameters.AddWithValue("@fstatus", 0);
                                cmd.Parameters.AddWithValue("@To", ToDate);
                                cmd.Connection = con;
                                con.Open();
                                cmd.ExecuteNonQuery();
                                con.Close();
                                Bindprodlunch();
                            }
                        }
                    }
                }
				label12.Text += "Product Launch file uploaded  successfully <br/>";
            }

        }


        catch (Exception ex)
        {
            label12.Text += "<b>Upload Failed!!!</b></br>";
            throw ex;

        }


    }

    protected void gvDetails_RowCancelingEdit(object sender, System.Web.UI.WebControls.GridViewCancelEditEventArgs e)
    {
        //Setting the EditIndex property to -1 to cancel the Edit mode in Gridview  
        string selectedfold = fold.SelectedItem.Text;
        if (selectedfold == "Detailing")
        {
            gvDetails.EditIndex = -1;
            BindGridviewData();
        }
        else if (selectedfold == "Product Launch")
        {
            gvDetails.EditIndex = -1;
            Bindprodlunch();
        }
        label12.Text = "";

    }
    protected void gvDetails_OnRowEditing(object sender, GridViewEditEventArgs e)
    {
        string selectedfold = fold.SelectedItem.Text;
        if (selectedfold == "Detailing")
        {
            gvDetails.EditIndex = e.NewEditIndex;
            BindGridviewData();
        }
        else if (selectedfold == "Product Launch")
        {
            gvDetails.EditIndex = e.NewEditIndex;
            Bindprodlunch();
        }
        label12.Text = "";
    }
    protected void gvDetails_OnRowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int index = e.RowIndex;
        GridViewRow row = (GridViewRow)gvDetails.Rows[index];

        try
        {
            if (hiddenval1.Value == "Yes")
            {
                using (SqlConnection con = new SqlConnection(strCon))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        string selectedfold = fold.SelectedItem.Text;
                        if (selectedfold == "Detailing")
                        {
                            cmd.CommandText = "update Product_Td_Detailing set File_status=2 where ID=@id and Prod_Code=@Pro_code ";

                            try
                            {
                                cmd.Parameters.AddWithValue("@Pro_code", txtProdDetailCode.Text);
                            }
                            catch
                            {
                                cmd.Parameters.AddWithValue("@Pro_code", ProdCode);
                            }
                            Label eid = (Label)row.FindControl("lbleid");
                            cmd.Parameters.AddWithValue("@id", eid.Text);

                            cmd.Connection = con;
                            con.Open();
                            cmd.ExecuteNonQuery();
                            con.Close();
                            gvDetails.EditIndex = -1;
                            BindGriddelviewData();
                        }

                        else if (selectedfold == "Product Launch")
                        {
                            cmd.CommandText = "delete from  Product_New_Launch where ID=@id and Prod_Code=@Pro_code ";
                            try
                            {
                                cmd.Parameters.AddWithValue("@Pro_code", txtProdDetailCode.Text);
                            }
                            catch
                            {
                                cmd.Parameters.AddWithValue("@Pro_code", ProdCode);
                            }
                            Label eid = (Label)row.FindControl("lbleid");
                            cmd.Parameters.AddWithValue("@id", eid.Text);

                            cmd.Connection = con;
                            con.Open();
                            cmd.ExecuteNonQuery();
                            con.Close(); gvDetails.EditIndex = -1;
                            Binddeleprodlunch();                            
                            label12.Text = "";
                        }
                    }
                }
            }
        }
        catch
        {
            label12.Text += "<b>Upload Failed!!!</b></br>";
        }
    }
    protected void gvDetails_OnRowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        int index = e.RowIndex;
        GridViewRow row = (GridViewRow)gvDetails.Rows[index];
        // FileUpload fileUpload11 = (FileUpload)row.FindControl("fileUpload11");       
        Label eid = (Label)row.FindControl("lbleid");
        TextBox txtEffect_from = (TextBox)row.Cells[0].FindControl("txtEffect_from");
        TextBox txtEffect_To = (TextBox)row.Cells[0].FindControl("txtEffect_To");
        try
        {
            if (hiddenval1.Value == "Yes")
            {
                string selectedfold = fold.SelectedItem.Text;
                if (selectedfold == "Detailing")
                {
                    using (SqlConnection con = new SqlConnection(strCon))
                    {
                        using (SqlCommand cmd = new SqlCommand())
                        {
                            //cmd.CommandText = "update Product_Td_Detailing set  File_status=@fstatus, File_Name=@Name , File_Type=@Type ,Content_Type=@CType Effect_From=@From,Effect_To=@To where Prod_Code=@Pro_code and ID=@id";
                            cmd.CommandText = "update Product_Td_Detailing set Effect_From=@From,Effect_To=@To where Prod_Code=@Pro_code and ID=@id";
                            try
                            {
                                cmd.Parameters.AddWithValue("@Pro_code", txtProdDetailCode.Text);
                            }
                            catch
                            {
                                cmd.Parameters.AddWithValue("@Pro_code", ProdCode);
                            }
                            //cmd.Parameters.AddWithValue("@Name", filename);
                            //cmd.Parameters.AddWithValue("@Type", filetype);
                            cmd.Parameters.AddWithValue("id", eid.Text);
                            cmd.Parameters.AddWithValue("@From", DateTime.Parse(txtEffect_from.Text));
                            cmd.Parameters.AddWithValue("@To", DateTime.Parse(txtEffect_To.Text));
                            //cmd.Parameters.AddWithValue("@fstatus", 1);
                            //cmd.Parameters.AddWithValue("@CType", contenttype);
                            cmd.Connection = con;
                            con.Open();
                            cmd.ExecuteNonQuery();
                            con.Close();
                            gvDetails.EditIndex = -1;
                            BindGridviewData();

                        }
                    }
                }
                else if (selectedfold == "Product Launch")
                {
					string StartsDate = txtEffect_from.Text;
                    string EndsDate = txtEffect_To.Text;
                    //DateTime myDate = DateTime.Parse(EndsDate);
                    DateTime myDates = Convert.ToDateTime((DateTime.Parse(EndsDate)).ToString("dd/M/yyyy"));
                    DateTime STDates = Convert.ToDateTime((DateTime.Parse(StartsDate)).ToString("dd/M/yyyy"));
                    DateTime today = Convert.ToDateTime(DateTime.Now.ToString("dd/M/yyyy"));
                    if (STDates > myDates)
                    {
                        string message = "The Effective from date should be greater than To date.";
                        System.Text.StringBuilder sb = new System.Text.StringBuilder();
                        sb.Append("<script type = 'text/javascript'>");
                        sb.Append("window.onload=function(){");
                        sb.Append("alert('");
                        sb.Append(message);
                        sb.Append("')};");
                        sb.Append("</script>");
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", sb.ToString());
                        return;
                    }
                    using (SqlConnection con = new SqlConnection(strCon))
                    {
                        using (SqlCommand cmd = new SqlCommand())
                        {
                            //cmd.CommandText = "update Product_Td_Detailing set  File_status=@fstatus, File_Name=@Name , File_Type=@Type ,Content_Type=@CType Effect_From=@From,Effect_To=@To where Prod_Code=@Pro_code and ID=@id";
                            cmd.CommandText = "update Product_New_Launch set Effective_From=@From,Effective_To=@To where Prod_Code=@Pro_code and ID=@id";
                            try
                            {
                                cmd.Parameters.AddWithValue("@Pro_code", txtProdDetailCode.Text);
                            }
                            catch
                            {
                                cmd.Parameters.AddWithValue("@Pro_code", ProdCode);
                            }
                            //cmd.Parameters.AddWithValue("@Name", filename);
                            //cmd.Parameters.AddWithValue("@Type", filetype);
                            cmd.Parameters.AddWithValue("id", eid.Text);
                            cmd.Parameters.AddWithValue("@From", DateTime.Parse(txtEffect_from.Text));
                            cmd.Parameters.AddWithValue("@To", DateTime.Parse(txtEffect_To.Text));
                            //cmd.Parameters.AddWithValue("@fstatus", 1);
                            //cmd.Parameters.AddWithValue("@CType", contenttype);
                            cmd.Connection = con;
                            con.Open();
                            cmd.ExecuteNonQuery();
                            con.Close();
                            gvDetails.EditIndex = -1;
                            Bindprodlunch();
                        }
                    }
                }
            }
        }
        catch
        {
            label12.Text += "<b>Upload Failed!!!</b></br>";
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
    //change chkallbox done by Giri 06-06-16
    private void FillUOM()
    {
        Division dv = new Division();
        dsDivision = dv.getStatePerDivision(div_code);
        if (dsDivision.Tables[0].Rows.Count > 0)
        {
            int i = 0;
            state_cd = string.Empty;
            sState = dsDivision.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
            statecd = sState.Split(',');
            foreach (string st_cd in statecd)
            {
                if (i == 0)
                {
                    state_cd = state_cd + st_cd;
                }
                else
                {
                    if (st_cd.Trim().Length > 0)
                    {
                        state_cd = state_cd + "," + st_cd;
                    }
                }
                i++;
            }

            State st = new State();
            TextBox t1 = new TextBox();
            dsState = st.getUOMChkBox(div_code);
            ddlbaseunit.DataTextField = "Move_MailFolder_Name";
            ddlbaseunit.DataValueField = "Move_MailFolder_Id";
            ddlbaseunit.DataSource = dsState;
            ddlbaseunit.DataBind();
            ddlunit.DataTextField = "Move_MailFolder_Name";
            ddlunit.DataValueField = "Move_MailFolder_Id";
            ddlunit.DataSource = dsState;
            ddlunit.DataBind();

        }
        //string[] state;
        //if (state_code != "")
        //{
        //    iIndex = -1;
        //    state = state_code.Split(',');
        //    foreach (string st in state)
        //    {
        //        for (iIndex = 0; iIndex < ChkBox_Multiunit.Items.Count; iIndex++)
        //        {
        //            if (st == ChkBox_Multiunit.Items[iIndex].Value)
        //            {
        //                ChkBox_Multiunit.Items[iIndex].Selected = true;
        //                ChkBox_Multiunit.Items[iIndex].Attributes.Add("style", "Color: Red; font-weight:Bold ");
        //            }
        //        }
        //    }
        //    int countSelected = ChkBox_Multiunit.Items.Cast<ListItem>().Where(i => i.Selected).Count();
        //    if (countSelected == ChkBox_Multiunit.Items.Count)
        //    {
        //        ChkBoxAll.Checked = true;
        //    }
        //}
        //else
        //{
        //    ChkBoxAll.Checked = true;
        //    for (iIndex = 0; iIndex < ChkBox_Multiunit.Items.Count; iIndex++)
        //    {
        //        ChkBox_Multiunit.Items[iIndex].Selected = true;
        //        //chkboxLocation.Items[iIndex].Attributes.Add("style", "Color: Blue; font-weight:Bold ");                    
        //    }

        //}


    }
    //change chkallbox done by saravanan 05-08-14 
    private void FillCheckBoxList()
    {
        //List of States are loaded into the checkbox list from Division Class
        Division dv = new Division();
        dsDivision = dv.getStatePerDivision(div_code);
        if (dsDivision.Tables[0].Rows.Count > 0)
        {
            int i = 0;
            state_cd = string.Empty;
            sState = dsDivision.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
            statecd = sState.Split(',');
            foreach (string st_cd in statecd)
            {
                if (i == 0)
                {
                    state_cd = state_cd + st_cd;
                }
                else
                {
                    if (st_cd.Trim().Length > 0)
                    {
                        state_cd = state_cd + "," + st_cd;
                    }
                }
                i++;
            }

            State st = new State();
            dsState = st.getStateChkBox(state_cd);
            chkboxLocation.DataTextField = "statename";
            chkboxLocation.DataValueField = "state_code";
            chkboxLocation.DataSource = dsState;
            chkboxLocation.DataBind();
        }
        string[] state;
        if (state_code != "")
        {
            iIndex = -1;
            state = state_code.Split(',');
            foreach (string st in state)
            {
                for (iIndex = 0; iIndex < chkboxLocation.Items.Count; iIndex++)
                {
                    if (st == chkboxLocation.Items[iIndex].Value)
                    {
                        chkboxLocation.Items[iIndex].Selected = true;
                        chkboxLocation.Items[iIndex].Attributes.Add("style", "Color: Red; font-weight:Bold ");
                    }
                }
            }
            int countSelected = chkboxLocation.Items.Cast<ListItem>().Where(i => i.Selected).Count();
            if (countSelected == chkboxLocation.Items.Count)
            {
                ChkAll.Checked = true;
            }
        }
        else
        {
            ChkAll.Checked = true;
            for (iIndex = 0; iIndex < chkboxLocation.Items.Count; iIndex++)
            {
                chkboxLocation.Items[iIndex].Selected = true;
                //chkboxLocation.Items[iIndex].Attributes.Add("style", "Color: Blue; font-weight:Bold ");                    
            }

        }

    }
    private void FillCheckBoxList_New()
    {
        //List of Sub division are loaded into the checkbox list from Division Class

        SubDivision dv = new SubDivision();
        dsSubDivision = dv.getSubDiv(div_code);
        // dsSubDivision.Tables[0].DefaultView.RowFilter = "SubDivision_Active_Flag = 0";
        DataTable dt = (dsSubDivision.Tables[0].DefaultView).ToTable();
        chkSubdiv.DataTextField = "subdivision_name";
        chkSubdiv.DataSource = dt;
        chkSubdiv.DataBind();
        string[] subdiv;

        if (subdivision_code != "")
        {
            iIndex = -1;
            subdiv = subdivision_code.Split(',');
            foreach (string st in subdiv)
            {
                for (iIndex = 0; iIndex < chkSubdiv.Items.Count; iIndex++)
                {
                    if (st == chkSubdiv.Items[iIndex].Value)
                    {
                        chkSubdiv.Items[iIndex].Selected = true;
                        chkSubdiv.Items[iIndex].Attributes.Add("style", "Color: Red; font-weight:Bold");
                        //chkNil.Checked = false;

                    }
                    else
                    {
                        //chkSubdiv.Items[iIndex].Enabled = false;
                    }
                }
            }

        }

    }
    private void FillCategory()
    {
        Product prd = new Product();
        dsProduct = prd.getProductCategory(div_code);
        if (dsProduct.Tables[0].Rows.Count > 0)
        {
            ddlCat.DataTextField = "Product_Cat_Name";
            ddlCat.DataValueField = "Product_Cat_Code";
            ddlCat.DataSource = dsProduct;
            ddlCat.DataBind();
        }
    }

    private void FillGroup()
    {
        Product prd = new Product();
        dsProduct = prd.getProductGroup(div_code);
        if (dsProduct.Tables[0].Rows.Count > 0)
        {
            ddlGroup.DataTextField = "Product_Grp_Name";
            ddlGroup.DataValueField = "Product_Grp_Code";
            ddlGroup.DataSource = dsProduct;
            ddlGroup.DataBind();
        }
    }

    private void FillBrand()
    {
        Product prd = new Product();
        dsProduct = prd.getProductBrand(div_code);
        if (dsProduct.Tables[0].Rows.Count > 0)
        {
            ddlBrand.DataTextField = "Product_Brd_Name";
            ddlBrand.DataValueField = "Product_Brd_Code";
            ddlBrand.DataSource = dsProduct;
            ddlBrand.DataBind();
        }
    }

    private void ResetAll()
    {
        txtProdDetailCode.Text = "";
        txtProdDetailName.Text = "";
        ddlunit.SelectedIndex = 0;
        ddlbaseunit.SelectedIndex = 0;
        //txtSamp1.Text = "";
        //txtSamp2.Text = "";
        //txtSamp3.Text = "";
        ddlCat.SelectedIndex = 0;
        //ddlGroup.SelectedIndex = 0;
        ddlBrand.SelectedIndex = 0;
        RblType.SelectedIndex = -1;
        txtProdDesc.Text = "";
        Txt_UOM.Text = "1";
        txtsale.Text = "";
        txtNetwt.Text = "";
        ChkAll.Checked = true;
        for (iIndex = 0; iIndex < chkboxLocation.Items.Count; iIndex++)
        {
            chkboxLocation.Items[iIndex].Selected = false;
        }
        //chkNil.Checked = true;
        for (iIndex = 0; iIndex < chkSubdiv.Items.Count; iIndex++)
        {
            chkSubdiv.Items[iIndex].Selected = false;
        }
    }

    protected void Submit_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        string unitwt = Txtunitwg.Text == string.Empty ? "0" : Convert.ToString(Txtunitwg.Text);
        string Prod_Name = txtProdDetailName.Text.Trim();
        string Prod_SName = txtProdShortName.Text.Trim();
        string Prod_des = txtProdDesc.Text.Trim();
        for (int i = 0; i < chkboxLocation.Items.Count; i++)
        {
            if (chkboxLocation.Items[i].Selected)
            {
                sChkLocation = sChkLocation + chkboxLocation.Items[i].Value + ",";
            }
        }
        for (int i = 0; i < chkSubdiv.Items.Count; i++)
        {
            if (chkSubdiv.Items[i].Selected)
            {
                sChkLocation1 = sChkLocation1 + chkSubdiv.Items[i].Value + ",";
            }
        }
        Int32 target = txttarget.Text == string.Empty ? 0 : Convert.ToInt32(txttarget.Text);
        if (Prod_Name != "" && Prod_SName != "" && Prod_des != "")
        {
            if (ProdCode == null)
            {
                // Add New Product            
                Product prd = new Product();
                if (Txtprovalid.Text == "")
                {
                    Txtprovalid.Text = "0";
                }
                int iReturn = prd.RecordAdd(txtProdDetailCode.Text.Trim(), Prod_Name, Convert.ToString(ddlbaseunit.SelectedItem), Convert.ToInt32(ddlbaseunit.SelectedValue), Convert.ToString(ddlunit.SelectedItem), Convert.ToInt32(ddlunit.SelectedValue), Convert.ToInt32(ddlCat.SelectedValue), Convert.ToInt32(ddlGroup.SelectedValue), Convert.ToString(RblType.SelectedItem.Value), Prod_des, Convert.ToInt32(Session["div_code"].ToString()), sChkLocation, sChkLocation1, ddlmode, Txt_UOM.Text.Trim(), txtsale.Text.Trim(), Convert.ToInt32(ddlBrand.SelectedValue), txtPacksize.Text.Trim(), txtGrosswt.Text.Trim(), txtNetwt.Text.Trim(), target, Prod_SName, Txt_Hsn.Text.Trim(), unitwt, Convert.ToInt16(Txtprovalid.Text));

                if (iReturn > 0)
                {
                    // menu1.Status = "Product Details Created Successfully";
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Created Successfully');window.location='ProductDetail.aspx';</script>");
                    ResetAll();

                }

                if (iReturn == -2)
                {
                    //  menu1.Status = "Product exist with the same short name!!";
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Product Name Already Exist ');</script>");
                    txtProdDetailName.Focus();

                }
                if (iReturn == -3)
                {
                    //  menu1.Status = "Product exist with the same short name!!";
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Product Code Already Exist');</script>");
                    txtProdDetailCode.Focus();

                }

            }
            else
            {
                //Update Product
                txtProdDetailCode.Enabled = true;
                Product dv = new Product();
                if (Txtprovalid.Text == "")
                {
                    Txtprovalid.Text = "0";
                }
                int iReturn = dv.RecordUpdateProd(txtProdDetailCode.Text.Trim(), Prod_Name, Convert.ToString(ddlbaseunit.SelectedItem), Convert.ToInt32(ddlbaseunit.SelectedValue), Convert.ToString(ddlunit.SelectedItem), Convert.ToInt32(ddlunit.SelectedValue), Convert.ToInt32(ddlCat.SelectedValue), Convert.ToInt32(ddlGroup.SelectedValue), Convert.ToString(RblType.SelectedItem.Value), Prod_des, Session["div_code"].ToString(), sChkLocation, sChkLocation1, ddlmode, Txt_UOM.Text.Trim(), txtsale.Text.Trim(), Convert.ToInt32(ddlBrand.SelectedValue), txtPacksize.Text.Trim(), txtGrosswt.Text.Trim(), txtNetwt.Text.Trim(), target, Prod_SName, Txt_Hsn.Text.Trim(), unitwt, Convert.ToInt16(Txtprovalid.Text));
                if (iReturn > 0)
                {
                    // menu1.Status = "Product Updated Successfully ";
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Updated Successfully');window.location='ProductList.aspx';</script>");
                }
                else if (iReturn == -2)
                {
                    // menu1.Status = "Product already exist!!";
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Product Name already exist');</script>");
                    txtProdDetailName.Focus();
                }
            }
        }
        else
        {
            if (txtProdDetailName.Text.Trim() == "")
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Fill Product Name...');</script>");
                txtProdDetailName.Focus();
            }
            if (txtProdShortName.Text.Trim() == "")
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Fill Product Short Name...');</script>");
                txtProdShortName.Focus();
            }
            if (txtProdDesc.Text.Trim() == "")
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Enter Product Description...');</script>");
                txtProdDesc.Focus();
            }
        }

    }

    protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
    {
        chkboxLocation.Attributes.Add("onclick", "checkAll(this);");
    }
    //protected void chkNil_CheckedChanged(object sender, EventArgs e)
    //{
    //    chkboxLocation.Attributes.Add("onclick", "checkNIL(this);");
    //}
    protected void ChkBoxAll_CheckedChanged(object sender, EventArgs e)
    {
        // ChkBox_Multiunit.Attributes.Add("onclick", "ChkBoxAll(this);");

    }


    //protected void ddlbaseunit_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    foreach (ListItem item in ddlunit.Items)
    //    {
    //        if (item.Value == ddlbaseunit.SelectedValue)
    //        {
    //            item.Attributes.Add("disabled", "disabled");
    //        }
    //    }
    //}
    protected void ddlunit_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (ddlbaseunit.SelectedValue == ddlunit.SelectedValue)
        {
            Txt_UOM.Text = "1";
            Txt_UOM.Enabled = false;
        }
        else
        {
            Txt_UOM.Text = "1";
            Txt_UOM.Enabled = true;
        }

    }
    protected void ddlbaseunit_SelectedIndexChanged1(object sender, EventArgs e)
    {
        if (ddlunit.SelectedValue == ddlbaseunit.SelectedValue)
        {
            Txt_UOM.Text = "1";
            Txt_UOM.Enabled = false;
        }
        else
        {
            Txt_UOM.Text = "1";
            Txt_UOM.Enabled = true;
        }
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        ProdCode = Request.QueryString["Product_Detail_Code"];
        div_code = Session["div_code"].ToString();


        try
        {
            string filename = Path.GetFileName(fileup2.PostedFile.FileName);
            string imagepath = Server.MapPath(@"PImage\" + filename);
            string filetype = Path.GetExtension(fileup2.FileName);
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

                        serverfolder = Server.MapPath(@"PImage\");

                        if (!Directory.Exists(serverfolder))
                        {
                            Directory.CreateDirectory(serverfolder);
                        }
                        serverpath = serverfolder + Path.GetFileName(filename);
                        fileup2.SaveAs(serverpath);
                        pimage.Text += "[" + fileup2.FileName + "]- Image file uploaded  successfully <br/>";
                        filetype = "I";
                        break;
                }
            }
            Product dv = new Product();
            if (ProdCode != null)
            {
                dsImg = dv.uploadImage(div_code, filename, ProdCode);
            }
            else
            {
                dsImg = dv.uploadImage(div_code, filename, txtProdDetailCode.Text);
            }
            ImgDisplay(filename);
        }
        catch
        {
            pimage.Text += "<b>Upload Failed!!!</b></br>";
        }
    }
    protected void ImgDisplay(string filename)
    {
        prodImg.ImageUrl = @"PImage\" + filename;
    }
    [WebMethod]
    public static string GetUOMddl()
    {
        State st = new State();
        DataSet dsState = st.getUOMChkBox(div_code);
        return JsonConvert.SerializeObject(dsState.Tables[0]);
    }
    [WebMethod]
    public static string GetUOMpro(string procode, string UOT)
    {
        State st = new State();
        DataSet dsState = st.get_unitconv(procode, div_code, UOT);
        return JsonConvert.SerializeObject(dsState.Tables[0]);
    }

    [WebMethod]
    public static string saveUOMPopup(string Data, string ddlbaseunit, string ProdDetailCode)
    {
        string msg = string.Empty;
        string Div_Code = HttpContext.Current.Session["div_code"].ToString();
        Div_Code = Div_Code.TrimEnd(',');

        var items = JsonConvert.DeserializeObject<List<UOMpopup>>(Data);
        Product Ord = new Product();

        string sxml = "<ROOT>";
        for (int k = 0; k < items.Count; k++)
        {
            if (items[k].baseuom != "" && items[k].qty != "")
            {
                sxml += "<Prod  ddluom=\"" + items[k].baseuom + "\" DefUOM=\"" + items[k].defaultuom + "\" qnty=\"" + items[k].qty + "\"/>";
                //        // PCode varchar(50),Qty int,Val float,Rate float,FQty float,DAmt float,Dval int,Md varchar(50),Mfg varchar(80),Cl float
            }
        }
        sxml += "</ROOT>";
        msg = Ord.UnitConv_AddRecord(sxml, Div_Code, ddlbaseunit, ProdDetailCode);
        return msg;
    }
    public class UOMpopup
    {
        public string baseuom { get; set; }
        public string qty { get; set; }
        public string aval { get; set; }
        public string defaultuom { get; set; }

    }
    [WebMethod]
    public static string gettax(string divcode)
    {

        DataSet dds = getAlltax(divcode);
        return JsonConvert.SerializeObject(dds.Tables[0]);
    }

    public static DataSet getAlltax(string divcode)
    {
        DB_EReporting db_ER = new DB_EReporting();
        DataSet ds = null;
        string strQry = "SELECT 0 as Tax_Id,'---Select---' as Tax_Name union all select Tax_Id,Tax_Name+'@'+convert(varchar,Value) Tax_Name  " +
                "from Tax_Master where Tax_Active_Flag=0 AND dIVISION_CODE='" + div_code + "' ";
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


    [WebMethod]
    public static string getstates(string divcode)
    {

        DataSet dds = getAllstate(divcode);
        return JsonConvert.SerializeObject(dds.Tables[0]);
    }
    public static DataSet getAllstate(string divcode)
    {
        DB_EReporting db_ER = new DB_EReporting();

        DataSet dsState = null;

        string strQry = " SELECT S.State_Code,StateName,ShortName FROM mas_state S inner join Mas_Salesforce M on m.State_Code=S.State_Code where charindex(',' + cast('" + divcode + "' as varchar) + ',',',' + Division_Code + ',')> 0 Group by S.State_Code,StateName,ShortName ";

        try
        {
            dsState = db_ER.Exec_DataSet(strQry);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return dsState;
    }
    [WebMethod]
    public static string deletedata(string Product_code)
    {
        string msg = "";
        string Product_codee = Product_code.Trim();


        string divcode = null;

        if (HttpContext.Current.Session["div_code"] != null)
        {
            if (HttpContext.Current.Session["div_code"].ToString() != "")
            {
                divcode = HttpContext.Current.Session["div_code"].ToString();

            }
        }
        if (Product_code != "")
        {

            SqlConnection con = new SqlConnection(Globals.ConnString);
            con.Open();
            SqlCommand cmd = new SqlCommand("delete from Mas_StateProduct_TaxDetails  where Product_code='" + Product_codee + "' and Division_Code='" + divcode + "' ", con);

            int i = cmd.ExecuteNonQuery();

            if (i >= 0)
            {
                msg = "true";
            }
            else
            {
                msg = "false";
            }



        }
        return msg;
    }
    [WebMethod]
    public static string saveprotax(string Product_code, string newtax)
    {
        DB_EReporting db = new DB_EReporting();
        int ds = 0;
        string Product_codee = Product_code.Trim();
        div_code = HttpContext.Current.Session["div_code"].ToString();
        string msg = string.Empty;
        DataTable dt = JsonConvert.DeserializeObject<DataTable>(newtax);
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            string[] state = dt.Rows[i]["sname"].ToString().Split(',');
            for (int j = 0; j < state.Length; j++)
            {
                string strQry = "insert into Mas_StateProduct_TaxDetails(State_Code,Product_Code,Tax_Id,Division_Code) values('" + state[j] + "', '" + Product_codee + "','" + dt.Rows[i]["tname"] + "','" + div_code + "')";

                try
                {
                    ds = db.ExecQry(strQry);
                    msg = "Success";
                    if (ds > 0)
                    {
                        msg = "Success";
                    }
                    else
                    {
                        msg = "error occured";
                    }
                }
                catch (Exception ex)
                {
                    msg = ex.Message;
                }

            }
        }



        return msg;
    }
    public class TaxData
    {

        public string tname { get; set; }
        public string sname { get; set; }

    }
    [WebMethod]
    public static string gettaxesdets(string Product_Code, string div_code)
    {

        DataSet dsAdmin = new DataSet();
        DB_EReporting db = new DB_EReporting();

        string strQry = "Select State_code,Product_Code,Tax_Id from Mas_StateProduct_TaxDetails where Product_Code = '" + Product_Code + "' and Division_code= '" + div_code + "'";
        try
        {
            dsAdmin = db.Exec_DataSet(strQry);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return JsonConvert.SerializeObject(dsAdmin.Tables[0]);

    }


}
