using Bus_EReport;
using DBase_EReport;
using Newtonsoft.Json;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Web;
using System.Web.Services;

public partial class MasterFiles_UploadExcelFile : System.Web.UI.Page
{
    string Div_Code = string.Empty;
    string SF_Code = string.Empty;
    DataTable Dt = null;
    DataTable dt = null;
    public static string excelName = string.Empty;
    public static string div_code = string.Empty;
    string sf_type = string.Empty;
    protected override void OnPreInit(EventArgs e)
    {
        base.OnPreInit(e);
        sf_type = Session["sf_type"].ToString();
        div_code = HttpContext.Current.Session["div_code"].ToString();
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
        div_code = HttpContext.Current.Session["div_code"].ToString();
    }
    public static string getdate(string date)
    {
        DateTime Fdate;
        string fdate;
        try
        {
            try
            {
                Fdate = DateTime.ParseExact(date, "dd-MM-yyyy", CultureInfo.InvariantCulture); //HH:mm:ss
                fdate = Fdate.ToString("yyyy-MM-dd");
            }
            catch
            {
                Fdate = DateTime.ParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture); //HH:mm:ss
                fdate = Fdate.ToString("yyyy-MM-dd");
            }
        }
        catch
        {
            fdate = string.Empty;
        }
        return fdate;
    }

    [WebMethod]
    public static string getTable(string divcode)
    {
        divcode = string.Empty;
        SalesForce SFD = new SalesForce();
        DataSet dds = SFD.GeUploadExcelTableName(divcode);
        return JsonConvert.SerializeObject(dds.Tables[0]);
    }

    [WebMethod]
    public static string getAliseName(string divcode, string toolname)
    {
        divcode = string.Empty;
        SalesForce SFD = new SalesForce();
        DataSet dds = SFD.GetAliseName(divcode, toolname);
        return JsonConvert.SerializeObject(dds.Tables[0]);
    }
    [WebMethod]
    public static string getStates()
    {
        tax tx = new tax();
        DataSet dds = tx.GetState();
        return JsonConvert.SerializeObject(dds.Tables[0]);
    }
    [WebMethod]
    public static string getTaxmas()
    {
        tax tx = new tax();
        DataSet dds = tx.GetTax();
        return JsonConvert.SerializeObject(dds.Tables[0]);
    }
    [WebMethod]
    public static string InsertExcelData(string data, string Table_Name, string column_name, string div, string ToolNm)
    {
        string msg = string.Empty;
        string sQry = string.Empty;
        string svQry = string.Empty;
       
        string sfcode = string.Empty;
        char ch = '\n';
        SalesForce SFD = new SalesForce();

        dynamic Data = JsonConvert.DeserializeObject(data);

        string[] Cols = column_name.Split(',');

        if (HttpContext.Current.Session["div_code"] != null)
        {
            if (HttpContext.Current.Session["div_code"].ToString() != "")
            {
                div_code = HttpContext.Current.Session["div_code"].ToString();
            }
        }
        else
        {
            div_code = "";
        }
              

        if (div_code == "156")
        {            
            if (ToolNm == "TP Upload" && Data["User"] != "")
            {

                if (ToolNm == "Scheme Mapping")
                {
                    sQry = "update " + Table_Name + " set ";
                    svQry = " where ";
                }
                else
                {
                    sQry = "insert into " + Table_Name + "(";
                    svQry = "select ";
                }

                DataSet dsd = SFD.getDefault_AutoCols("", ToolNm);//2 4

                for (int i = 0; i < dsd.Tables[0].Rows.Count; i++)
                {
                    if ((dsd.Tables[0].Rows[i]["Name_To_Code"].ToString() == "4"))
                    {
                        sQry += dsd.Tables[0].Rows[i]["Field_Name"].ToString() + ",";
                        if (dsd.Tables[0].Rows[i]["Target_Field"].ToString() == "@Div")
                        {
                            svQry += div + ",";

                        }
                        else
                        {
                            svQry += dsd.Tables[0].Rows[i]["Target_Field"].ToString() + ",";
                        }
                    }
                    else if ((dsd.Tables[0].Rows[i]["Name_To_Code"].ToString() == "5"))
                    {
                        sQry += dsd.Tables[0].Rows[i]["Field_Name"].ToString() + ",";
                        if (dsd.Tables[0].Rows[i]["Target_Field"].ToString() == "@Div")
                        {
                            svQry += div + ",";

                        }
                        else
                        {
                            svQry += dsd.Tables[0].Rows[i]["Target_Field"].ToString() + ",";
                        }
                    }
                }

                int j = 0;
                foreach (var item in Data)
                {
                    string itme1 = item.Name; string fldStr = "";

                    string itme2 = item.Value.ToString();

                    if (itme2 == null)
                    {
                        itme2 = "";
                    }

                    if (Cols[j] == itme1 && itme2 != "")
                    {                       

                        if (Cols[j] == itme1)
                        {
                            DataSet ds = SFD.getUploadSettings("", ToolNm, Cols[j]);

                            int n = 0;
                            n = Convert.ToInt32(ds.Tables[0].Rows.Count);

                            if (n > 1)
                            {
                                for (int k = 0; k < ds.Tables[0].Rows.Count; k++)
                                {
                                    if ((ds.Tables[0].Rows[k]["Name_To_Code"].ToString() == "4" || ds.Tables[0].Rows[k]["Name_To_Code"].ToString() == "5"))
                                    {
                                        if ((ds.Tables[0].Rows[k]["Mantatory"].ToString() == "1" && ds.Tables[0].Rows[k]["SubQuery"].ToString() != ""))
                                        {
                                            fldStr = "";
                                            string squery = ""; string retval = ""; string sfuser = ""; string sfcod = "";
                                            string fname = Convert.ToString(ds.Tables[0].Rows[k]["Field_Name"]);

                                            switch (fname)
                                            {
                                                case "WorkType_Code_B":
                                                    sfuser = "";
                                                    sfuser = Data["User"].ToString();
                                                    sfcod = "";
                                                    sfcod = Data["WorkType"];
                                                    squery = "";
                                                    squery = ds.Tables[0].Rows[k]["SubQuery"].ToString().Replace("@Div", div).Replace("@value", sfcod.ToString()).Replace("u0027", "''").Replace("@SF", sfuser);

                                                    //string squery = ds.Tables[0].Rows[k]["SubQuery"].ToString().Replace("@Div", div).Replace("@value", item.Value.ToString()).Replace("u0027", "''").Replace("@SF", username);
                                                    retval = "";
                                                    retval = getValues(squery);
                                                    if (retval == null)
                                                    {
                                                        msg = "No Values Found for " + Cols[j];
                                                        msg = "{\"success\":\"" + msg + "\",\"Qry\":\"" + sQry + "\"}";
                                                        return msg;
                                                    }
                                                    else
                                                    {
                                                        fldStr = "'" + retval + "'";

                                                        int WorkTypeCode = 0;
                                                        if (retval == "" || retval == null)
                                                        {
                                                            WorkTypeCode = 0;
                                                        }
                                                        else
                                                        {
                                                            WorkTypeCode = Convert.ToInt32(retval);
                                                        }

                                                        retval = "" + WorkTypeCode + "";

                                                        svQry = svQry.Replace("@WorkType_Code_B", retval.ToString());
                                                    }


                                                    break;

                                                case "Worktype_Name_B":
                                                    sfuser = "";
                                                    sfuser = Data["User"].ToString();
                                                    sfcod = "";
                                                    sfcod = Data["WorkType"];
                                                    squery = "";
                                                    squery = ds.Tables[0].Rows[k]["SubQuery"].ToString().Replace("@Div", div).Replace("@value", sfcod.ToString()).Replace("u0027", "''").Replace("@SF", sfuser);

                                                    //string squery = ds.Tables[0].Rows[k]["SubQuery"].ToString().Replace("@Div", div).Replace("@value", item.Value.ToString()).Replace("u0027", "''").Replace("@SF", username);
                                                    retval = "";
                                                    retval = getValues(squery);
                                                    if (retval == null)
                                                    {
                                                        msg = "No Values Found for " + Cols[j];
                                                        msg = "{\"success\":\"" + msg + "\",\"Qry\":\"" + sQry + "\"}";
                                                        return msg;
                                                    }
                                                    else
                                                    {
                                                        fldStr = "'" + retval + "'";
                                                        svQry = svQry.Replace("@Worktype_Name_B", fldStr.ToString());
                                                    }


                                                    break;

                                                case "Tour_Schedule1":

                                                    sfuser = "";
                                                    sfuser = Data["User"].ToString();
                                                    sfcod = "";
                                                    sfcod = Data["Beat"];


                                                    squery = "";

                                                    squery = ds.Tables[0].Rows[k]["SubQuery"].ToString().Replace("@value", sfcod.ToString()).Replace("u0027", "''").Replace("@SF", sfuser);

                                                    //squery = ds.Tables[0].Rows[k]["SubQuery"].ToString().Replace("@value", Beat.ToString());

                                                    //string squery = ds.Tables[0].Rows[k]["SubQuery"].ToString().Replace("@Div", div).Replace("@value", item.Value.ToString()).Replace("u0027", "''").Replace("@SF", username);
                                                    retval = "";
                                                    retval = getValues(squery);

                                                    if (retval == null)
                                                    {
                                                        msg = "No Values Found for " + Cols[j];
                                                        msg = "{\"success\":\"" + msg + "\",\"Qry\":\"" + sQry + "\"}";
                                                        return msg;
                                                    }
                                                    else
                                                    {
                                                        fldStr = "'" + retval + "'";
                                                        svQry = svQry.Replace("@Tour_Schedule1", fldStr.ToString());
                                                    }


                                                    break;

                                                case "Territory_Code1":
                                                    sfuser = "";
                                                    sfuser = Data["User"].ToString();
                                                    sfcod = "";
                                                    sfcod = Data["Beat"];
                                                    squery = "";
                                                    squery = ds.Tables[0].Rows[k]["SubQuery"].ToString().Replace("@value", sfcod.ToString()).Replace("u0027", "''").Replace("@SF", sfuser);

                                                    //squery = ds.Tables[0].Rows[k]["SubQuery"].ToString().Replace("@value", Beat1.ToString());

                                                    retval = "";
                                                    retval = getValues(squery);

                                                    if (retval == null)
                                                    {
                                                        msg = "No Values Found for " + Cols[j];
                                                        msg = "{\"success\":\"" + msg + "\",\"Qry\":\"" + sQry + "\"}";
                                                        return msg;
                                                    }
                                                    else
                                                    {
                                                        fldStr = "'" + retval + "'";
                                                        svQry = svQry.Replace("@Territory_Code1", fldStr.ToString());
                                                    }


                                                    break;

                                                case "HQ_Code":
                                                    sfuser = ""; sfuser = Data["User"].ToString();

                                                    sfcod = ""; sfcod = Data["HQ"];

                                                    squery = "";
                                                    squery = ds.Tables[0].Rows[k]["SubQuery"].ToString().Replace("@value", sfcod.ToString()).Replace("u0027", "''").Replace("@SF", sfuser);


                                                    //squery = ds.Tables[0].Rows[k]["SubQuery"].ToString().Replace("@value", sfcod.ToString());

                                                    //string squery = ds.Tables[0].Rows[k]["SubQuery"].ToString().Replace("@Div", div).Replace("@value", item.Value.ToString()).Replace("u0027", "''").Replace("@SF", username);
                                                    retval = "";
                                                    retval = getValues(squery);
                                                    if (retval == null)
                                                    {
                                                        msg = "No Values Found for " + Cols[j];
                                                        msg = "{\"success\":\"" + msg + "\",\"Qry\":\"" + sQry + "\"}";
                                                        return msg;
                                                    }
                                                    else
                                                    {
                                                        fldStr = "'" + retval + "'";
                                                        svQry = svQry.Replace("@HQ_Code", fldStr.ToString());
                                                    }

                                                    break;

                                                case "HQ_Name":
                                                    sfuser = ""; sfuser = Data["User"].ToString();

                                                    sfcod = ""; sfcod = Data["HQ"];

                                                    squery = "";
                                                    squery = ds.Tables[0].Rows[k]["SubQuery"].ToString().Replace("@value", sfcod.ToString()).Replace("u0027", "''").Replace("@SF", sfuser);

                                                    retval = "";
                                                    retval = getValues(squery);
                                                    if (retval == null)
                                                    {
                                                        msg = "No Values Found for " + Cols[j];
                                                        msg = "{\"success\":\"" + msg + "\",\"Qry\":\"" + sQry + "\"}";
                                                        return msg;
                                                    }
                                                    else
                                                    {
                                                        fldStr = "'" + retval + "'";
                                                        svQry = svQry.Replace("@HQ_Name", fldStr.ToString());
                                                    }


                                                    break;

                                                case "Distributor_Name":

                                                    sfuser = ""; sfuser = Data["User"].ToString();

                                                    sfcod = ""; sfcod = Data["Distributor"];

                                                    squery = "";
                                                    //squery = ds.Tables[0].Rows[k]["SubQuery"].ToString().Replace("@value", sfcod.ToString()).Replace("u0027", "''").Replace("@SF", sfuser);

                                                    squery = ds.Tables[0].Rows[k]["SubQuery"].ToString().Replace("@Div", div).Replace("@value", sfcod.ToString());

                                                    retval = "";
                                                    retval = getValues(squery);
                                                    if (retval == null)
                                                    {
                                                        msg = "No Values Found for " + Cols[j];
                                                        msg = "{\"success\":\"" + msg + "\",\"Qry\":\"" + sQry + "\"}";
                                                        return msg;
                                                    }
                                                    else
                                                    {
                                                        fldStr = "'" + retval + "'";
                                                        svQry = svQry.Replace("@Distributor_Name", fldStr.ToString());
                                                    }


                                                    break;

                                                case "Distributor_Code":
                                                    sfuser = ""; sfuser = Data["User"].ToString();

                                                    sfcod = ""; sfcod = Data["Distributor"];

                                                    squery = "";
                                                    //squery = ds.Tables[0].Rows[k]["SubQuery"].ToString().Replace("@value", sfcod.ToString()).Replace("u0027", "''").Replace("@SF", sfuser);

                                                    squery = ds.Tables[0].Rows[k]["SubQuery"].ToString().Replace("@Div", div).Replace("@value", sfcod.ToString());

                                                    retval = "";
                                                    retval = getValues(squery);

                                                    if (retval == null)
                                                    {
                                                        msg = "No Values Found for " + Cols[j];
                                                        msg = "{\"success\":\"" + msg + "\",\"Qry\":\"" + sQry + "\"}";
                                                        return msg;
                                                    }
                                                    else
                                                    {
                                                        fldStr = "'" + retval + "'";
                                                        svQry = svQry.Replace("@Distributor_Code", fldStr.ToString());
                                                    }

                                                    break;

                                                default:
                                                    break;
                                            }

                                        }
                                    }
                                }
                            }
                            else
                            {
                                if ((ds.Tables[0].Rows[0]["Mantatory"].ToString() == "1" && ds.Tables[0].Rows[0]["SubQuery"].ToString() != ""))
                                {
                                    fldStr = "";
                                    if (ds.Tables[0].Rows[0]["Field_Name"].ToString() == "SF_Code" && Cols[j] == "User")
                                    {
                                        string sfcod = Data["User"];

                                        string squery = ds.Tables[0].Rows[0]["SubQuery"].ToString().Replace("@Div", div).Replace("@value", sfcod.ToString()).Replace("u0027", "''");

                                        //string squery = ds.Tables[0].Rows[k]["SubQuery"].ToString().Replace("@Div", div).Replace("@value", item.Value.ToString()).Replace("u0027", "''").Replace("@SF", sfcod);

                                        string retval = getValues(squery);
                                        if (retval == null)
                                        {
                                            msg = "No Values Found for " + Cols[j];
                                            msg = "{\"success\":\"" + msg + "\",\"Qry\":\"" + sQry + "\"}";
                                            return msg;
                                        }
                                        else
                                        {
                                            fldStr = "'" + retval + "'";
                                        }

                                        svQry = svQry.Replace("@SF_Code", fldStr.ToString());
                                    }
                                }
                                else
                                {
                                    if ((ds.Tables[0].Rows[0]["Mantatory"].ToString() == "1" && ds.Tables[0].Rows[0]["SubQuery"].ToString() == ""))
                                    {
                                        if (ds.Tables[0].Rows[0]["Field_Name"].ToString() == "Tour_Date")
                                        {
                                            //DateTime gtdate = Convert.ToDateTime(Data["Date"]);

                                            string tdate = Convert.ToString(Data["Tour_Date"]);
                                            if (tdate != "" && tdate != null)
                                            {

                                                DateTime gtdate = Convert.ToDateTime(Data["Tour_Date"]);
                                                item.Value = gtdate.ToString("yyyy-MM-dd");
                                                svQry = svQry.Replace("@Mn", gtdate.Month.ToString());
                                                svQry = svQry.Replace("@Yr", gtdate.Year.ToString());

                                                string day = Convert.ToString(gtdate.Day.ToString());
                                                int dayLength = day.Length;
                                                if (dayLength < 2)
                                                {
                                                    day = "0" + day;
                                                }

                                                string month = Convert.ToString(gtdate.Month.ToString());
                                                int MonthLength = month.Length;
                                                if (MonthLength < 2)
                                                {
                                                    month = "0" + month;
                                                }

                                                string tdate1 = Convert.ToString(gtdate.Year.ToString() + "-" + month + "-" + day);

                                                svQry = svQry.Replace("@Tour_Date", "'" + tdate1.ToString() + "'");
                                            }
                                            else
                                            {
                                                svQry = svQry.Replace("@Tour_Date", "");
                                            }

                                        }
                                        else
                                        {

                                            if (ds.Tables[0].Rows[0]["Field_Name"].ToString() == "Objective")
                                            {
                                                string Remarks = Convert.ToString(Data["Remarks"]);

                                                svQry = svQry.Replace("@Remarks", "'" + Remarks.ToString() + "'");
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    j++;
                }
            }            
        }
        else
        {
            if (div_code != "156")
            {

                if (ToolNm == "Scheme Mapping")
                {
                    sQry = "update " + Table_Name + " set ";
                    svQry = " where ";
                }
                else
                {
                    sQry = "insert into " + Table_Name + "(";
                    svQry = "select ";
                }


                int il = 0;
                DataSet dsd = SFD.getDefault_AutoCols("", ToolNm);//2 4
                for (int ij = 0; ij < dsd.Tables[0].Rows.Count; ij++)
                {
                    //if (Cols[il] != "")
                    //{
                    if (dsd.Tables[0].Rows[ij]["Name_To_Code"].ToString() == "2")
                    {
                        DataSet dsA = SFD.getAuto_ID(dsd.Tables[0].Rows[ij]["Alise_Name"].ToString(), ToolNm);//autoID
                        var autoID = dsA.Tables[0].Rows[0]["ID"].ToString();
                        sQry += dsd.Tables[0].Rows[ij]["Field_Name"].ToString() + ",";
                        svQry += autoID + ",";
                        DataSet dsD = SFD.getIdentical_Fields(dsd.Tables[0].Rows[ij]["Alise_Name"].ToString(), ToolNm);
                        for (int ji = 0; ji < dsD.Tables[0].Rows.Count; ji++)
                        {
                            sQry += dsD.Tables[0].Rows[ji]["Field_Name"].ToString() + ",";
                            svQry += autoID + ",";
                        }
                    }
                    else if (dsd.Tables[0].Rows[ij]["Name_To_Code"].ToString() == "4")
                    {
                        sQry += dsd.Tables[0].Rows[ij]["Field_Name"].ToString() + ",";
                        if (dsd.Tables[0].Rows[ij]["Target_Field"].ToString() == "@Div")
                        {
                            svQry += div + ",";
                        }
                        else
                        {
                            svQry += dsd.Tables[0].Rows[ij]["Target_Field"].ToString() + ",";
                        }
                    }
                    else if (dsd.Tables[0].Rows[ij]["Name_To_Code"].ToString() == "5" && ToolNm != "Tax")
                    {
                        svQry += dsd.Tables[0].Rows[ij]["Field_Name"].ToString() + "='" + Data[dsd.Tables[0].Rows[ij]["Alise_Name"].ToString()].ToString() + "'";
                    }
                }
                foreach (var item in Data)
                {
                    string itme1 = item.Name;

                    string itme2 = item.Value.ToString();

                    if (itme2 == null)
                    {
                        itme2 = "";
                    }

                    if (Cols[il] == itme1 && itme2 != "")
                    {
                        

                        DataSet ds = SFD.getUploadSettings("", ToolNm, Cols[il]);
                        for (int ij = 0; ij < ds.Tables[0].Rows.Count; ij++)
                        {
                            if (ToolNm == "Scheme Mapping" && ds.Tables[0].Rows[ij]["Name_To_Code"].ToString() != "5")
                            {
                                string fldStr = ((ds.Tables[0].Rows[ij]["Name_To_Code"].ToString() == "1") ? ds.Tables[0].Rows[ij]["SubQuery"].ToString() : "'@value'");
                                sQry += ds.Tables[0].Rows[ij]["Field_Name"].ToString() + "=" + fldStr.Replace("@Div", div).Replace("@value", item.Value.ToString()).Replace("u0027", "''") + ",";
                            }
                            else if (ds.Tables[0].Rows[ij]["Name_To_Code"].ToString() != "5")
                            {
                                sQry += ds.Tables[0].Rows[ij]["Field_Name"].ToString() + ",";
                                var itmval = item.Value.ToString();
                                if (ToolNm == "TP Upload")
                                {
                                    if (Cols[il] == "Tour_Date")
                                    {
                                        DateTime gtdate = Convert.ToDateTime(Data["Tour_Date"]);
                                        item.Value = gtdate.ToString("yyyy-MM-dd");
                                        svQry = svQry.Replace("@Mn", gtdate.Month.ToString());
                                        svQry = svQry.Replace("@Yr", gtdate.Year.ToString());
                                    }
                                    svQry = svQry.Replace("@SF", Data["SF_Code"].ToString());
                                }
                                string fldStr;
                                if (ToolNm == "TP Upload")
                                {
                                    if ((ds.Tables[0].Rows[ij]["Name_To_Code"].ToString() == "1"))
                                    {
                                        if ((ds.Tables[0].Rows[ij]["Mantatory"].ToString() == "1"))
                                        {
                                            string sfcod = Data["SF_Code"];
                                            string squery = ds.Tables[0].Rows[ij]["SubQuery"].ToString().Replace("@Div", div).Replace("@value", item.Value.ToString()).Replace("u0027", "''").Replace("@SF", sfcod);
                                            string retval = getValues(squery);
                                            if (retval == null)
                                            {
                                                msg = "No Values Found for " + Cols[il];
                                                msg = "{\"success\":\"" + msg + "\",\"Qry\":\"" + sQry + "\"}";
                                                return msg;
                                            }
                                            else
                                            {
                                                fldStr = "'" + retval + "'";
                                            }
                                        }
                                        else
                                        {
                                            fldStr = ds.Tables[0].Rows[ij]["SubQuery"].ToString();
                                        }
                                    }
                                    else
                                    {
                                        fldStr = "'@value'";
                                    }
                                }
                                else
                                {
                                    fldStr = ((ds.Tables[0].Rows[ij]["Name_To_Code"].ToString() == "1") ? ds.Tables[0].Rows[ij]["SubQuery"].ToString() : "'@value'");
                                    svQry += fldStr.Replace("@Div", div).Replace("@value", item.Value.ToString()).Replace("u0027", "''") + ",";
                                    if (ToolNm == "Outlet Upload")
                                    {
                                        svQry = svQry.Replace("@secval", Data["Territory"].ToString());
                                    }
                                    if (ToolNm == "Tax")
                                    {
                                        svQry = svQry.Replace("@secval", Data["TaxValue"].ToString());
                                    }
                                }
                            }
                        }
                    }
                    il++;
                }
            }
        }

        if (ToolNm == "Scheme Mapping")
        {
            sQry = sQry.TrimEnd(',') + " " + svQry.TrimEnd(',');
        }
        else
        {
            sQry = sQry.TrimEnd(',') + ") " + svQry.TrimEnd(',');
        }

        //msg = SFD.ExcelData(sQry);
        if (ToolNm == "Tax")
        {
            tax tx = new tax();
            DataSet dst = tx.GetStCd(Data.StateName.Value);
            var res = JsonConvert.SerializeObject(dst.Tables[0]);
          
            using (SqlConnection con = new SqlConnection(Globals.ConnString))
            {
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = sQry;
                    try
                    {
                        if (con.State != ConnectionState.Open)
                        {
                            con.Open();
                        }
                        int i = cmd.ExecuteNonQuery();
                        if (i > 0)
                        {
                            msg = "Success";
                        }
                    }
                    catch (Exception ex)
                    {
                        msg = ex.Message.Replace(ch, '.');
                    }
                }
            }
            msg = "{\"success\":\"" + msg + "\",\"Qry\":\"" + sQry + "\"}";
            //}
        }       
        else
        {
            using (SqlConnection con = new SqlConnection(Globals.ConnString))
            {
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = sQry;

                    try
                    {
                        if (con.State != ConnectionState.Open)
                        {
                            con.Open();
                        }

                        int i = cmd.ExecuteNonQuery();
                        if (i > 0)
                        {
                            msg = "Success";
                        }
                    }
                    catch (Exception ex)
                    {
                        msg = ex.Message.Replace(ch, '.');
                    }
                }
            }
            msg = "{\"success\":\"" + msg + "\",\"Qry\":\"" + sQry + "\"}";
        }
        return msg;
    }

    [WebMethod]
    public static string getUploadSettings(string divcode, string toolname)
    {
        SalesForce SFD = new SalesForce();
        DataSet dds = SFD.getUploadSettings(divcode, toolname, "");
        return JsonConvert.SerializeObject(dds.Tables[0]);
    }
    public static string getValues(string sqry)
    {
        string strQry = string.Empty;
        DataSet dsTerritory = new DataSet();
        SqlConnection con = new SqlConnection(Globals.ConnString);
        con.Open();
        SqlCommand cmd = new SqlCommand("select " + sqry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dsTerritory);
        con.Close();
        if (dsTerritory.Tables[0].Rows.Count > 0)
        {
            return dsTerritory.Tables[0].Rows[0][0].ToString();
        }
        else
        {
            return null;
        }
    }
    public class tax
    {
        public DataSet chkdup(string Tax_Value, string tax_nm, string Prod_cd, string State_Cd)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet dsAdmin = null;
            string strQry = "select tm.Tax_Name,tm.value,tm.Tax_Id,stx.Product_Code,stx.Tax_Id from tax_master tm left join mas_stateproduct_taxdetails stx on tm.Tax_Id=stx.Tax_Id where tm.Tax_Name='" + tax_nm + "' and tm.value='" + Tax_Value + "' and stx.Product_Code='" + Prod_cd + "' and stx.State_Code='" + State_Cd + "'";
            try
            {
                dsAdmin = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsAdmin;
        }
        public DataSet GetStCd(string stnm)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet dsAdmin = null;
            string strQry = "select State_Code from mas_state where StateName='" + stnm + "'";
            try
            {
                dsAdmin = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsAdmin;
        }
        public DataSet GetState()
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet dsAdmin = null;
            string strQry = "select StateName from mas_state where State_Active_Flag=0";
            try
            {
                dsAdmin = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsAdmin;
        }
        public DataSet GetTax()
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet dsAdmin = null;
            string strQry = "select Tax_Name as TaxName,Value as TaxValue from tax_master where tax_active_flag = 0";
            try
            {
                dsAdmin = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsAdmin;
        }
    }
}