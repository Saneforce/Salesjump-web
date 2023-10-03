using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


using System.Data;
using Bus_EReport;

using System.Web.Services;
using System.Data.SqlClient;
using Newtonsoft.Json;

using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Web.Script.Serialization;
using System.IO;
using System.Text;
using System.Configuration;
using System.Globalization;

using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Windows;
using System.IO;
using System.Data;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html;
using iTextSharp.text.html.simpleparser;
using System.Text;
using Bus_EReport;
using System.Net;
using iTextSharp.tool.xml;

public partial class MasterFiles_rpt_Expense_Entry : System.Web.UI.Page
{

    string mode = string.Empty;
    string fyear = string.Empty;
    string fmonth = string.Empty;
    string fo_code = string.Empty;
    string fo_name = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            hdnMonth.Value = Request.QueryString["FMonth"].ToString();
            hdnYear.Value = Request.QueryString["FYear"].ToString();
            hdnsfcode.Value = Request.QueryString["SF_Code"].ToString();
        }
    }
    public class pro_years
    {
        public string years { get; set; }
    }
    [WebMethod(EnableSession = true)]
    public static pro_years[] Get_Year()
    {
        List<pro_years> product = new List<pro_years>();
        TourPlan tp = new TourPlan();
        DataSet dsTP = null;
        string Div_code = HttpContext.Current.Session["div_code"].ToString();
        dsTP = tp.Get_TP_Edit_Year(Div_code);
        if (dsTP.Tables[0].Rows.Count > 0)
        {
            for (int k = Convert.ToInt16(dsTP.Tables[0].Rows[0]["Year"]); k <= DateTime.Now.Year + 1; k++)
            {
                pro_years pd = new pro_years();
                pd.years = k.ToString();
                product.Add(pd);

            }
        }
        return product.ToArray();
    }

    public class AllwonceType_Details
    {
        public string ALW_code { get; set; }
        public string ALW_name { get; set; }
        public string ALW_type { get; set; }
        public string user_enter { get; set; }
    }
    [WebMethod(EnableSession = true)]
    public static AllwonceType_Details[] GetAllType()
    {

        Notice nt = new Notice();
        List<AllwonceType_Details> FFD = new List<AllwonceType_Details>();
        string Div_code = HttpContext.Current.Session["div_code"].ToString();
        string Sf_Code = HttpContext.Current.Session["Sf_Code"].ToString();

        DataSet dsAlowtype = null;
        dsAlowtype = nt.get_Allowance_Type(Div_code);

        foreach (DataRow row in dsAlowtype.Tables[0].Rows)
        {
            AllwonceType_Details ffd = new AllwonceType_Details();
            ffd.ALW_code = row["ID"].ToString();
            ffd.ALW_name = row["Short_Name"].ToString();
            ffd.ALW_type = row["type"].ToString();
            ffd.user_enter = row["user_enter"].ToString();
            FFD.Add(ffd);
        }
        return FFD.ToArray();
    }



    public class Expance_Details
    {
        public string sf_code { get; set; }
        public string Activity_Date { get; set; }
        public string WorkType_Name { get; set; }
        public string Route_Name { get; set; }
    }


    [WebMethod(EnableSession = true)]
    public static Expance_Details[] GetExpanceDetails(string sf_code, string years, string months)
    {

        Expense nt = new Expense();
        List<Expance_Details> FFD = new List<Expance_Details>();
        string Div_code = HttpContext.Current.Session["div_code"].ToString();
        //string Sf_Code = HttpContext.Current.Session["Sf_Code"].ToString();

        DataSet dsAlowtype = null;
        dsAlowtype = nt.get_Expance_Data(Div_code, sf_code, years, months);

        foreach (DataRow row in dsAlowtype.Tables[0].Rows)
        {
            Expance_Details ffd = new Expance_Details();
            ffd.sf_code = row["sf_code"].ToString();
            ffd.Activity_Date = Convert.ToDateTime(row["Activity_Date"]).ToString("dd-MM-yyyy");
            ffd.WorkType_Name = row["WorkType_Name"].ToString();
            ffd.Route_Name = row["Route_Name"].ToString();
            FFD.Add(ffd);
        }
        return FFD.ToArray();
    }

    public class Expance_Values
    {
        public string sf_code { get; set; }
        public string Allowance_Code { get; set; }
        public string Allowance_Value { get; set; }
        public string Created_Date { get; set; }
    }

    [WebMethod(EnableSession = true)]
    public static Expance_Values[] GetExpanceValues(string sf_code, string years, string months)
    {

        Expense nt = new Expense();
        List<Expance_Values> FFD = new List<Expance_Values>();
        string Div_code = HttpContext.Current.Session["div_code"].ToString();
        //string Sf_Code = HttpContext.Current.Session["Sf_Code"].ToString();

        DataSet dsAlowtype = null;
        dsAlowtype = nt.get_Expance_Values(Div_code, sf_code, years, months);

        foreach (DataRow row in dsAlowtype.Tables[0].Rows)
        {
            Expance_Values ffd = new Expance_Values();
            ffd.sf_code = row["sf_code"].ToString();
            ffd.Created_Date = Convert.ToDateTime(row["Created_Date"]).ToString("dd-MM-yyyy");
            ffd.Allowance_Code = row["Allowance_Code"].ToString();
            ffd.Allowance_Value = row["Allowance_Value"].ToString();
            FFD.Add(ffd);
        }
        return FFD.ToArray();
    }

    public class Visit_Details
    {
        public string slno { get; set; }
        public string dcr_code { get; set; }
        public string sf_code { get; set; }
        public string vdate { get; set; }
        public string route_code { get; set; }
        public string vtype { get; set; }
        public string vstcnt { get; set; }
    }




    [WebMethod(EnableSession = true)]
    public static Visit_Details[] GetVisitDetails(string sf_code, string years, string months)
    {
        Expense nt = new Expense();
        List<Visit_Details> FFD = new List<Visit_Details>();
        string Div_code = HttpContext.Current.Session["div_code"].ToString();
        //string Sf_Code = HttpContext.Current.Session["Sf_Code"].ToString();

        DataSet dsAlowtype = null;
        dsAlowtype = nt.get_Visit_Details(Div_code, sf_code, years, months);

        foreach (DataRow row in dsAlowtype.Tables[0].Rows)
        {
            Visit_Details ffd = new Visit_Details();
            ffd.sf_code = row["SF"].ToString();
            ffd.vdate = Convert.ToDateTime(row["vDate"]).ToString("dd-MM-yyyy");
            ffd.slno = row["SlNo"].ToString();
            ffd.dcr_code = row["DCR_Code"].ToString();
            ffd.route_code = row["Route_Code"].ToString();
            ffd.vtype = row["vType"].ToString();
            ffd.vstcnt = row["vstCnt"].ToString();
            FFD.Add(ffd);
        }
        return FFD.ToArray();
    }


    public class Distance_Details
    {
        public string Territory_Ho { get; set; }
        public string Frm_Plc_Code { get; set; }
        public string To_Plc_Code { get; set; }
        public string Distance_KM { get; set; }
        public string Place_Type { get; set; }

    }


    [WebMethod(EnableSession = true)]
    public static Distance_Details[] GetDistanceDetails(string sf_code)
    {
        Expense nt = new Expense();
        List<Distance_Details> FFD = new List<Distance_Details>();
        string Div_code = HttpContext.Current.Session["div_code"].ToString();
        //string Sf_Code = HttpContext.Current.Session["Sf_Code"].ToString();

        DataSet dsAlowtype = null;
        dsAlowtype = nt.get_Distance_Details(Div_code, sf_code);

        foreach (DataRow row in dsAlowtype.Tables[0].Rows)
        {
            Distance_Details ffd = new Distance_Details();
            ffd.Territory_Ho = row["Territory_Ho"].ToString();
            ffd.Frm_Plc_Code = row["Frm_Plc_Code"].ToString();
            ffd.To_Plc_Code = row["To_Plc_Code"].ToString();
            ffd.Distance_KM = row["Distance_KM"].ToString();
            ffd.Place_Type = row["Place_Type"].ToString();

            FFD.Add(ffd);
        }
        return FFD.ToArray();
    }


    [WebMethod(EnableSession = true)]
    public static string GetTerr_Code(string sf_code)
    {
        SalesForce sf = new SalesForce();
        List<Distance_Details> FFD = new List<Distance_Details>();
        string Div_code = HttpContext.Current.Session["div_code"].ToString();
        //string Sf_Code = HttpContext.Current.Session["Sf_Code"].ToString();

        DataSet dsAlowtype = null;
        dsAlowtype = sf.Get_terr_code(Div_code, sf_code);

        return dsAlowtype.Tables[0].Rows[0][0].ToString();
    }



    public class Fare_Details
    {
        public string Sf_Code { get; set; }
        public string Sf_Name { get; set; }
        public string Fare { get; set; }
        public string Fareid { get; set; }

    }

    [WebMethod(EnableSession = true)]
    public static Fare_Details[] GetfAREDetails(string sf_code)
    {
        Expense nt = new Expense();
        List<Fare_Details> FFD = new List<Fare_Details>();
        string Div_code = HttpContext.Current.Session["div_code"].ToString();
        //string Sf_Code = HttpContext.Current.Session["Sf_Code"].ToString();

        DataSet dsAlowtype = null;
        dsAlowtype = nt.get_Fare_Details(Div_code, sf_code);

        foreach (DataRow row in dsAlowtype.Tables[0].Rows)
        {
            Fare_Details ffd = new Fare_Details();
            ffd.Sf_Code = row["Sf_Code"].ToString();
            ffd.Sf_Name = row["Sf_Name"].ToString();
            ffd.Fare = row["Fare"].ToString();
            ffd.Fareid = row["Fareid"].ToString();


            FFD.Add(ffd);
        }
        return FFD.ToArray();
    }

    //public class 

    public class Exp_App_Details
    {
        public string expName { get; set; }
        public string eDate { get; set; }
        public string expCode { get; set; }
        public string Amt { get; set; }

    }



    [WebMethod(EnableSession = true)]
    public static Exp_App_Details[] GetAppDetails(string SF_Code, string ExpYear, string ExpMonth)
    {
        Expense nt = new Expense();
        List<Exp_App_Details> FFD = new List<Exp_App_Details>();
        string Div_code = HttpContext.Current.Session["div_code"].ToString();
        //string Sf_Code = HttpContext.Current.Session["Sf_Code"].ToString();

        DataSet dsAlowtype = null;
        dsAlowtype = nt.get_appexp_Details(Div_code, SF_Code, ExpYear, ExpMonth);


        foreach (DataRow row in dsAlowtype.Tables[0].Rows)
        {
            Exp_App_Details ffd = new Exp_App_Details();
            ffd.eDate = Convert.ToDateTime(row["eDate"]).ToString("dd-MM-yyyy"); //row["eDate"].ToString();
            ffd.expCode = row["expCode"].ToString();
            ffd.expName = row["Short_Name"].ToString();
            ffd.Amt = row["Amt"].ToString();


            FFD.Add(ffd);
        }
        return FFD.ToArray();
    }




    [WebMethod(EnableSession = true)]
    public static string SaveDate(string data)
    {
        var msg = string.Empty;
        int itemid;
        string sqlQry = string.Empty;
        string tranid = string.Empty;
        string Div_code = HttpContext.Current.Session["div_code"].ToString();
        string Sf_Type = HttpContext.Current.Session["sf_type"].ToString();

        string exp_mode = string.Empty;
        if (Sf_Type == "1")
        {
            exp_mode = "1";
        }
        else if (Sf_Type == "2")
        {
            exp_mode = "2";
        }
        else
        {
            exp_mode = "3";
        }
        MainTransExpense JMTE = new MainTransExpense();

        JSonHelper helper = new JSonHelper();
        JMTE = helper.ConverJSonToObject<MainTransExpense>(data);


        string kk = JMTE.TED[0].adddtls[0].alw_code;
        SqlConnection con = new SqlConnection(Globals.ConnString);

        con.Open();
        sqlQry = "select * from Trans_Expense_Head1 where SF_Code='" + JMTE.MTED[0].sf_code + "' and Expense_Year='" + JMTE.MTED[0].exp_year + "' and Expense_Month='" + JMTE.MTED[0].exp_month + "'";
        SqlCommand cmmd = new SqlCommand(sqlQry);
        cmmd.Connection = con;
        DataTable dtt = new DataTable();
        SqlDataAdapter sdda = new SqlDataAdapter(cmmd);
        sdda.Fill(dtt);
        con.Close();



        if (dtt.Rows.Count < 1)
        {

            SqlTransaction tran;
            con.Open();
            tran = con.BeginTransaction();
            try
            {

                sqlQry = "select Division_Code,sf_name,Territory_Code,Territory,sf_Designation_Short_Name from mas_salesforce where sf_code='" + JMTE.MTED[0].sf_code + "'";
                SqlCommand cmd = new SqlCommand(sqlQry);
                cmd.Connection = con;
                cmd.Transaction = tran;
                DataTable dt = new DataTable();
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);

                if (dt.Rows.Count > 0)
                {

                    string div_code = dt.Rows[0][0].ToString();
                    string sf_name = dt.Rows[0][1].ToString();
                    string terr_code = dt.Rows[0][2].ToString();
                    string terr_name = dt.Rows[0][3].ToString();
                    string des_name = dt.Rows[0][4].ToString();


                    sqlQry = "insert into Trans_Expense_Head1(Division_Code,SF_Code,SF_Name,Desig_Name,Territory_Code,Territory_Name,Expense_Mode,Expense_Year,Expense_Month,Expense_Amt,Exp_Sent_date,Approval_Date)values(@Division_Code,@SF_Code,@SF_Name,@Desig_Name,@Territory_Code,@Territory_Name,@Expense_Mode,@Expense_Year,@Expense_Month,@Expense_Amt,getdate(),getdate());SELECT SCOPE_IDENTITY()";
                    cmd = new SqlCommand(sqlQry);
                    cmd.Connection = con;
                    cmd.Transaction = tran;
                    cmd.Parameters.AddWithValue("@Division_Code", div_code.TrimEnd(','));
                    cmd.Parameters.AddWithValue("@SF_Code", JMTE.MTED[0].sf_code);
                    cmd.Parameters.AddWithValue("@SF_Name", sf_name);
                    cmd.Parameters.AddWithValue("@Desig_Name", des_name);
                    cmd.Parameters.AddWithValue("@Territory_Code", terr_code);
                    cmd.Parameters.AddWithValue("@Territory_Name", terr_name);
                    cmd.Parameters.AddWithValue("@Expense_Mode", exp_mode);
                    cmd.Parameters.AddWithValue("@Expense_Year", JMTE.MTED[0].exp_year);
                    cmd.Parameters.AddWithValue("@Expense_Month", JMTE.MTED[0].exp_month);
                    cmd.Parameters.AddWithValue("@Expense_Amt", JMTE.MTED[0].exp_amt);
                    itemid = Convert.ToInt32(cmd.ExecuteScalar());

                    for (int i = 0; i < JMTE.TED.Count; i++)
                    {

                        sqlQry = "insert into Trans_Expense_Detail1(trans_dt_slno,Expense_Date,Expense_wtype,Place_of_Work,Expense_All_Type,Expense_Distance,Expense_Fare,Expense_DA,Daily_Total)values(@trans_dt_slno,@Expense_Date,@Expense_wtype,@Place_of_Work,@Expense_All_Type,@Expense_Distance,@Expense_Fare,@Expense_DA,@Daily_Total)";
                        cmd = new SqlCommand(sqlQry);
                        cmd.Connection = con;
                        cmd.Transaction = tran;
                        cmd.Parameters.AddWithValue("@trans_dt_slno", itemid);

                        DateTime newDate = DateTime.ParseExact(JMTE.TED[i].exp_dt, "dd-MM-yyyy", null);
                        string str = newDate.ToString("MM-dd-yyyy");
                        cmd.Parameters.AddWithValue("@Expense_Date", str);
                        cmd.Parameters.AddWithValue("@Expense_wtype", JMTE.TED[i].work_type);
                        cmd.Parameters.AddWithValue("@Place_of_Work", JMTE.TED[i].place_work);
                        cmd.Parameters.AddWithValue("@Expense_All_Type", JMTE.TED[i].al_type);
                        cmd.Parameters.AddWithValue("@Expense_Distance", JMTE.TED[i].distance);
                        cmd.Parameters.AddWithValue("@Expense_Fare", JMTE.TED[i].fare);
                        cmd.Parameters.AddWithValue("@Expense_DA", JMTE.TED[i].exp_da);
                        cmd.Parameters.AddWithValue("@Daily_Total", JMTE.TED[i].dly_tot);
                        cmd.ExecuteNonQuery();

                        for (int j = 0; j < JMTE.TED[i].adddtls.Count; j++)
                        {

                            sqlQry = "insert into Trans_Daily_Allowance_Details(Trans_Dt_SlNo,Trans_Exp_Param_Code,Trans_Exp_Amt,User_Entered,exp_date)values(@Trans_Dt_SlNo,@Trans_Exp_Param_Code,@Trans_Exp_Amt,@User_Entered,@exp_date)";
                            cmd = new SqlCommand(sqlQry);
                            cmd.Connection = con;
                            cmd.Transaction = tran;
                            cmd.Parameters.AddWithValue("@Trans_Dt_SlNo", itemid);
                            cmd.Parameters.AddWithValue("@Trans_Exp_Param_Code", JMTE.TED[i].adddtls[j].alw_code);
                            cmd.Parameters.AddWithValue("@Trans_Exp_Amt", JMTE.TED[i].adddtls[j].value);
                            cmd.Parameters.AddWithValue("@User_Entered", JMTE.TED[i].adddtls[j].user_enter);
                            cmd.Parameters.AddWithValue("@exp_date", str);
                            cmd.ExecuteNonQuery();
                        }
                    }

                    for (int i = 0; i < JMTE.MED.Count; i++)
                    {
                        sqlQry = "insert into Trans_Monthly_Allowance_Details(Trans_Dt_SlNo,Trans_Exp_Param_Code,Trans_Exp_Amt,User_Entered)values(@Trans_Dt_SlNo,@Trans_Exp_Param_Code,@Trans_Exp_Amt,@User_Entered)";
                        cmd = new SqlCommand(sqlQry);
                        cmd.Connection = con;
                        cmd.Transaction = tran;
                        cmd.Parameters.AddWithValue("@Trans_Dt_SlNo", itemid);
                        cmd.Parameters.AddWithValue("@Trans_Exp_Param_Code", JMTE.MED[i].alw_code);
                        cmd.Parameters.AddWithValue("@Trans_Exp_Amt", JMTE.MED[i].value);
                        cmd.Parameters.AddWithValue("@User_Entered", JMTE.MED[i].user_enter);
                        cmd.ExecuteNonQuery();
                    }
                    msg = "Record Update Success";
                    tran.Commit();

                }
                else
                {
                    msg = "Field Force Not Fount";
                }

            }
            catch (Exception exp)
            {
                if (tran != null)
                    tran.Rollback();
                msg = exp.Message.ToString() + "\nTransaction Rolledback, Tim didn't make it.";
            }

            finally
            {
                con.Close();
            }
        }
        else
        {
            tranid = dtt.Rows[0][0].ToString();
            SqlTransaction tran;
            con.Open();
            tran = con.BeginTransaction();
            try
            {


                sqlQry = "update Trans_Expense_Head1 set Expense_Amt=@Expense_Amt where Trans_Sl_No=@Trans_Sl_No";
                SqlCommand cmd = new SqlCommand(sqlQry);
                cmd = new SqlCommand(sqlQry);
                cmd.Connection = con;
                cmd.Transaction = tran;
                cmd.Parameters.AddWithValue("@Expense_Amt", JMTE.MTED[0].exp_amt);
                cmd.Parameters.AddWithValue("@Trans_Sl_No", tranid);
                cmd.ExecuteNonQuery();
                itemid = Convert.ToInt16(tranid);


                sqlQry = "delete from Trans_Expense_Detail1 where trans_dt_slno=@trans_dt_slno";
                cmd = new SqlCommand(sqlQry);
                cmd.Connection = con;
                cmd.Transaction = tran;
                cmd.Parameters.AddWithValue("@trans_dt_slno", tranid);
                cmd.ExecuteNonQuery();

                sqlQry = "delete from Trans_Daily_Allowance_Details where trans_dt_slno=@trans_dt_slno";
                cmd = new SqlCommand(sqlQry);
                cmd.Connection = con;
                cmd.Transaction = tran;
                cmd.Parameters.AddWithValue("@trans_dt_slno", tranid);
                cmd.ExecuteNonQuery();

                for (int i = 0; i < JMTE.TED.Count; i++)
                {

                    sqlQry = "insert into Trans_Expense_Detail1(trans_dt_slno,Expense_Date,Expense_wtype,Place_of_Work,Expense_All_Type,Expense_Distance,Expense_Fare,Expense_DA,Daily_Total)values(@trans_dt_slno,@Expense_Date,@Expense_wtype,@Place_of_Work,@Expense_All_Type,@Expense_Distance,@Expense_Fare,@Expense_DA,@Daily_Total)";
                    cmd = new SqlCommand(sqlQry);
                    cmd.Connection = con;
                    cmd.Transaction = tran;
                    cmd.Parameters.AddWithValue("@trans_dt_slno", itemid);
                    DateTime newDate = DateTime.ParseExact(JMTE.TED[i].exp_dt, "dd-MM-yyyy", null);
                    string str = newDate.ToString("MM-dd-yyyy");
                    cmd.Parameters.AddWithValue("@Expense_Date", str);

                    // cmd.Parameters.AddWithValue("@Expense_Date", JMTE.TED[i].exp_dt);
                    cmd.Parameters.AddWithValue("@Expense_wtype", JMTE.TED[i].work_type);
                    cmd.Parameters.AddWithValue("@Place_of_Work", JMTE.TED[i].place_work);
                    cmd.Parameters.AddWithValue("@Expense_All_Type", JMTE.TED[i].al_type);
                    cmd.Parameters.AddWithValue("@Expense_Distance", JMTE.TED[i].distance);
                    cmd.Parameters.AddWithValue("@Expense_Fare", JMTE.TED[i].fare);
                    cmd.Parameters.AddWithValue("@Expense_DA", JMTE.TED[i].exp_da);
                    cmd.Parameters.AddWithValue("@Daily_Total", JMTE.TED[i].dly_tot);
                    cmd.ExecuteNonQuery();

                    for (int j = 0; j < JMTE.TED[i].adddtls.Count; j++)
                    {

                        sqlQry = "insert into Trans_Daily_Allowance_Details(Trans_Dt_SlNo,Trans_Exp_Param_Code,Trans_Exp_Amt,User_Entered,exp_date)values(@Trans_Dt_SlNo,@Trans_Exp_Param_Code,@Trans_Exp_Amt,@User_Entered,@exp_date)";
                        cmd = new SqlCommand(sqlQry);
                        cmd.Connection = con;
                        cmd.Transaction = tran;
                        cmd.Parameters.AddWithValue("@Trans_Dt_SlNo", itemid);
                        cmd.Parameters.AddWithValue("@Trans_Exp_Param_Code", JMTE.TED[i].adddtls[j].alw_code);
                        cmd.Parameters.AddWithValue("@Trans_Exp_Amt", JMTE.TED[i].adddtls[j].value);
                        cmd.Parameters.AddWithValue("@User_Entered", JMTE.TED[i].adddtls[j].user_enter);
                        cmd.Parameters.AddWithValue("@exp_date", str);
                        cmd.ExecuteNonQuery();
                    }
                }

                sqlQry = "delete from Trans_Monthly_Allowance_Details where trans_dt_slno=@trans_dt_slno";
                cmd = new SqlCommand(sqlQry);
                cmd.Connection = con;
                cmd.Transaction = tran;
                cmd.Parameters.AddWithValue("@trans_dt_slno", tranid);
                cmd.ExecuteNonQuery();

                for (int i = 0; i < JMTE.MED.Count; i++)
                {
                    sqlQry = "insert into Trans_Monthly_Allowance_Details(Trans_Dt_SlNo,Trans_Exp_Param_Code,Trans_Exp_Amt,User_Entered)values(@Trans_Dt_SlNo,@Trans_Exp_Param_Code,@Trans_Exp_Amt,@User_Entered)";
                    cmd = new SqlCommand(sqlQry);
                    cmd.Connection = con;
                    cmd.Transaction = tran;
                    cmd.Parameters.AddWithValue("@Trans_Dt_SlNo", itemid);
                    cmd.Parameters.AddWithValue("@Trans_Exp_Param_Code", JMTE.MED[i].alw_code);
                    cmd.Parameters.AddWithValue("@Trans_Exp_Amt", JMTE.MED[i].value);
                    cmd.Parameters.AddWithValue("@User_Entered", JMTE.MED[i].user_enter);
                    cmd.ExecuteNonQuery();
                }
                msg = "Record Update Success";
                tran.Commit();

            }
            catch (Exception exp)
            {
                if (tran != null)
                    tran.Rollback();
                msg = exp.Message.ToString() + "\nTransaction Rolledback, Tim didn't make it.";
            }

            finally
            {
                con.Close();
            }
        }
        return msg;

    }

    public class JSonHelper
    {
        public string ConvertObjectToJSon<T>(T obj)
        {
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
            MemoryStream ms = new MemoryStream();
            ser.WriteObject(ms, obj);
            string jsonString = Encoding.UTF8.GetString(ms.ToArray());
            ms.Close();
            return jsonString;

        }
        public T ConverJSonToObject<T>(string jsonString)
        {
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T));
            MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonString));
            T obj = (T)serializer.ReadObject(ms);
            return obj;
        }
    }

    public class MainTransExpense
    {
        public List<Main_Trans_Expense_Details> MTED = new List<Main_Trans_Expense_Details>();
        public List<Trans_Expense_Details> TED = new List<Trans_Expense_Details>();
        public List<Daily_Expense_Details> DED = new List<Daily_Expense_Details>();
        public List<Month_Expense_Details> MED = new List<Month_Expense_Details>();
    }
    public class Main_Trans_Expense_Details
    {
        public string terr_hq { get; set; }
        public string sf_code { get; set; }
        public string sf_name { get; set; }
        public string desig_name { get; set; }
        public string terr_name { get; set; }
        public string exp_divcode { get; set; }
        public string exp_month { get; set; }
        public string exp_year { get; set; }
        public string exp_mode { get; set; }
        public string exp_sentdt { get; set; }
        public string exp_approdt { get; set; }
        public string exp_amt { get; set; }
        public string Trans_Sl_No { get; set; }

    }

    public class Trans_Expense_Details
    {
        public string exp_dt { get; set; }
        public string work_type { get; set; }
        public string work_anme { get; set; }
        public string place_work { get; set; }
        public string al_type { get; set; }
        public string distance { get; set; }
        public string fare { get; set; }
        public string exp_da { get; set; }
        public string dly_tot { get; set; }
        public List<Daily_Expense_Details> adddtls { get; set; }

        public Trans_Expense_Details()
        {
            adddtls = new List<Daily_Expense_Details>(0);
        }
    }
    public class Daily_Expense_Details
    {
        public string alw_code { get; set; }
        public string alw_name { get; set; }
        public string user_enter { get; set; }
        public string value { get; set; }
        public string exp_dt { get; set; }
    }
    public class Month_Expense_Details
    {
        public string alw_code { get; set; }
        public string alw_name { get; set; }
        public string user_enter { get; set; }
        public string value { get; set; }
    }

    [WebMethod(EnableSession = true)]
    public static string GetDate(string SF_Code, string ExpYear, string ExpMonth)
    {
        MainTransExpense JMTED = new MainTransExpense();
        Main_Trans_Expense_Details MTE;
        Trans_Expense_Details TE;
        Daily_Expense_Details DE;
        Month_Expense_Details ME;
        string sqlQry = string.Empty;
        string trans_id = string.Empty;
        SqlConnection con = new SqlConnection(Globals.ConnString);
        sqlQry = "select * from Trans_Expense_Head1 where SF_Code='" + SF_Code + "' and Expense_Year='" + ExpYear + "' and Expense_Month='" + ExpMonth + "'";
        SqlCommand cmd = new SqlCommand(sqlQry, con);
        con.Open();
        SqlDataReader reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            MTE = new Main_Trans_Expense_Details();
            MTE.Trans_Sl_No = reader["Trans_Sl_No"].ToString();
            trans_id = reader["Trans_Sl_No"].ToString();
            MTE.sf_code = reader["SF_Code"].ToString();
            MTE.sf_name = reader["SF_Name"].ToString();
            MTE.exp_amt = reader["Expense_Amt"].ToString();
            JMTED.MTED.Add(MTE);
        }
        reader.Close();
        con.Close();

        if (trans_id != string.Empty)
        {
            sqlQry = string.Format("select * from  Trans_Expense_Detail1  where  trans_dt_slno='" + trans_id + "'");
            cmd = new SqlCommand(sqlQry, con);
            con.Open();
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            sda.Dispose();
            con.Close();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow drr = dt.Rows[i];
                TE = new Trans_Expense_Details();
                TE.al_type = drr["Expense_All_Type"].ToString();
                TE.fare = drr["Expense_Fare"].ToString();
                TE.exp_da = drr["Expense_DA"].ToString();
                TE.distance = drr["Expense_Distance"].ToString();
                TE.exp_dt = Convert.ToDateTime(drr["Expense_Date"]).ToString("dd-MM-yyyy"); //drr["Expense_Date"].ToString();
                string exp_date = Convert.ToDateTime(drr["Expense_Date"]).ToString("yyyy-MM-dd");

                sqlQry = string.Format("select * from  Trans_Daily_Allowance_Details ad inner join Mas_Allowance_Type al on al.ID = ad.Trans_Exp_Param_Code  where  ad.trans_dt_slno='" + trans_id + "' and ad.exp_date =  cast(CONVERT(VARCHAR, '" + exp_date + "', 101) as datetime)");
                con.Open();
                cmd = new SqlCommand(sqlQry, con);
                DataTable dtt = new DataTable();
                sda = new SqlDataAdapter(cmd);
                sda.Fill(dtt);
                sda.Dispose();
                con.Close();
                JMTED.TED.Add(TE);
                for (int j = 0; j < dtt.Rows.Count; j++)
                {
                    DataRow dr = dtt.Rows[j];
                    DE = new Daily_Expense_Details();
                    DE.alw_code = dr["Trans_Exp_Param_Code"].ToString();
                    DE.alw_name = dr["Short_Name"].ToString();
                    DE.user_enter = dr["User_Entered"].ToString();
                    DE.value = dr["Trans_Exp_Amt"].ToString();
                    JMTED.TED[i].adddtls.Add(DE);
                }
            }

            sqlQry = string.Format("select * from  Trans_Monthly_Allowance_Details  where  trans_dt_slno='" + trans_id + "'");
            cmd = new SqlCommand(sqlQry, con);
            con.Open();
            DataTable dtm = new DataTable();
            SqlDataAdapter sdam = new SqlDataAdapter(cmd);
            sdam.Fill(dtm);
            sdam.Dispose();
            con.Close();
            for (int k = 0; k < dtm.Rows.Count; k++)
            {
                DataRow dr = dtm.Rows[k];
                ME = new Month_Expense_Details();
                ME.alw_code = dr["Trans_Exp_Param_Code"].ToString();
                ME.user_enter = dr["User_Entered"].ToString();
                ME.value = dr["Trans_Exp_Amt"].ToString();
                JMTED.MED.Add(ME);
            }
        }
        JSonHelper helper = new JSonHelper();
        String jsonResult = helper.ConvertObjectToJSon(JMTED);
        return jsonResult;
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        Session["ctrl"] = pnlContents;
        Control ctrl = (Control)Session["ctrl"];
        PrintWebControl(ctrl);
    }
    public static void PrintWebControl(Control ControlToPrint)
    {
        StringWriter stringWrite = new StringWriter();
        System.Web.UI.HtmlTextWriter htmlWrite = new System.Web.UI.HtmlTextWriter(stringWrite);
        if (ControlToPrint is WebControl)
        {
            Unit w = new Unit(100, UnitType.Percentage);
            ((WebControl)ControlToPrint).Width = w;
        }
        Page pg = new Page();
        pg.EnableEventValidation = false;
        HtmlForm frm = new HtmlForm();
        pg.Controls.Add(frm);
        frm.Attributes.Add("runat", "server");
        frm.Controls.Add(ControlToPrint);
        pg.DesignerInitialize();
        pg.RenderControl(htmlWrite);
        string strHTML = stringWrite.ToString();
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.Write(strHTML);
        HttpContext.Current.Response.Write("<script>window.print();</script>");
        HttpContext.Current.Response.End();

    }
}