using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
using System.Data.SqlClient;
using System.Web.Services;
using System.Configuration;  
using DBase_EReport;

public partial class Fare_fixation_ft : System.Web.UI.Page
{
    string div_code = string.Empty;
    DataSet dsSalesForce = null;
    DataSet dsUserList = null;
    DataSet dsDoc = null;
    DataSet dsDCR = null;
    int product_total = 0;
    int FWD_total = 0;
    DateTime dtCurrent;
    DataSet dsProduct = null;
    DataSet dsCatg = null;
    int MonColspan = 0;
    DataSet dsTP = null;
    string tot_dr = string.Empty;
    string sCurrentDate = string.Empty;
    string sf_code = string.Empty;
    DataSet dsSf = null;
  
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    Notice Addcomment = new Notice();
	string sf_type = string.Empty;
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
        div_code = Session["div_code"].ToString();
        BindGridd();
    }
    protected void BindGridd()
    {
   far gg = new far();
       
        DataSet ff = new DataSet();
         DataSet ff1 = new DataSet();
         ff1 = gg.getDesignation_div(div_code);
             if (ff1.Tables.Count > 0)
        {
            if (ff1.Tables[0].Rows.Count > 0)
            {


                Griddesig.DataSource = ff1;
                Griddesig.DataBind();


            }
        }
        ff = gg.getSalesForce_Fare_With_Fare(div_code);


        if (ff.Tables.Count > 0)
        {
            if (ff.Tables[0].Rows.Count > 0)
            {


                GridView1.DataSource = ff;
                GridView1.DataBind();


            }
        }
    }
	  [WebMethod(EnableSession = true)]
    public static Allowance_Data[] GetFare_fixation_ftValues()
    {

        far nt = new far();
        List<Allowance_Data> FFD = new List<Allowance_Data>();
        string Div_code = HttpContext.Current.Session["div_code"].ToString();
        string Sf_Code = HttpContext.Current.Session["Sf_Code"].ToString();

        DataSet dsAlowVal = null;
        dsAlowVal = nt.getSalesForce_Fare_With_date(Div_code);

        foreach (DataRow row in dsAlowVal.Tables[0].Rows)
        {
            Allowance_Data ffd = new Allowance_Data();
            ffd.SF_Code = row["sf_code"].ToString();
            ffd.SF_name = row["sf_name"].ToString();
            ffd.Des_code = row["designation_name"].ToString();
            ffd.EffDt = row["Effective_Date"].ToString();
            if (row["Effective_Date"] != DBNull.Value)
            {
                ffd.EffDt = Convert.ToDateTime(row["Effective_Date"]).ToString("yyyy-MM-dd");
            }
            else
            {
                ffd.EffDt = System.DateTime.Now.ToString("yyyy-MM-dd");
            }
            FFD.Add(ffd);
        }
        return FFD.ToArray();
    }
	    public class Allowance_Data
    {
        public string SF_Code { get; set; }
        public string SF_name { get; set; }
        public string Des_code { get; set; }
        public string EffDt { get; set; }
    }
    [WebMethod]
    public static string insertdata(string sf_name, string sf_code, string Quantity, string date)
    {
        string msg = "";
        string div_code = string.Empty;
        string sf_namee = sf_name.Trim();
        string sf_codee = sf_code.Trim();
        string myString = string.Empty;
        int i = 0;
        if (HttpContext.Current.Session["div_code"] != null)
        {
            if (HttpContext.Current.Session["div_code"].ToString() != "")
            {
                div_code = HttpContext.Current.Session["div_code"].ToString();
                
            }
        }
        if (sf_namee != "" && sf_codee != "")
        {

            SqlConnection con = new SqlConnection(Globals.ConnString);
            con.Open();
            SqlCommand cmdp = new SqlCommand(" select Fareid from Mas_Salesforcefare_KM where sf_code='" + sf_codee + "' and Division_code='" + div_code + "' and Effective_Date='" + date + "'", con);

            using (SqlDataReader rdr = cmdp.ExecuteReader())
            {
                while (rdr.Read())
                {
                    myString = rdr["Fareid"].ToString(); ; //The 0 stands for "the 0'th column", so the first column of the result.


                }
            }
            if (myString != "")
            {
                SqlCommand cmd1 = new SqlCommand("update Mas_Salesforcefare_KM  set fare=@Quantity where sf_code=@sf_code and Division_code ='" + div_code + "' and Effective_Date='" + date + "'", con);
                cmd1.Parameters.AddWithValue("@Quantity", Quantity);
                cmd1.Parameters.AddWithValue("@sf_code", sf_codee);
                i = cmd1.ExecuteNonQuery();

               
            }
            else
            {
                SqlCommand cmd = new SqlCommand("insert into Mas_Salesforcefare_KM (Sf_Name,Sf_Code,Fare,Division_code,Effective_Date) values(@sf_name, @sf_code,@Quantity,'"+div_code+"','" + date + "')", con);

                cmd.Parameters.AddWithValue("@sf_name", sf_namee);
                cmd.Parameters.AddWithValue("@sf_code", sf_codee);
                cmd.Parameters.AddWithValue("@Quantity", Quantity);
				 cmd.Parameters.AddWithValue("@date", date);
                i = cmd.ExecuteNonQuery();   
            }
          
            if (i == 1)
            {
                msg = "true";
            }
            else
            {
                msg = "false";
            }
            con.Close();
        }
        return msg;

    }
    protected void btsubmit_Click(object sender, EventArgs e)
    {

    }
	   public class far
    {
        public DataSet getSalesForce_Fare_With_Fare(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
          string  strQry = "SELECT distinct s.sf_code, s.sf_name ,d.designation_name ,s.sf_HQ ,d.Designation_Code,f.fare,s.division_code,Effective_Date " +
           " FROM mas_salesforce s left join Mas_SF_Designation d on d.Designation_code=s.designation_code left  join Mas_Salesforcefare_KM f on f.sf_code=s.sf_code and Effective_Date=(select max(Effective_Date) from Mas_Salesforcefare_KM " +
         "  where Division_Code = '" + div_code + "') " +
         " where  s.Division_Code = '" + div_code + ",' and s.sf_status=0";


            try
            {
                dsDivision = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsDivision;
        }
		   public DataSet getSalesForce_Fare_With_date(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            string strQry = "SELECT distinct s.sf_code, s.sf_name ,d.designation_name ,s.sf_HQ ,d.Designation_Code,f.fare,s.division_code,Effective_Date " +
             " FROM mas_salesforce s left join Mas_SF_Designation d on d.Designation_code=s.designation_code left  join Mas_Salesforcefare_KM f on f.sf_code=s.sf_code  " +
           " where  s.Division_Code = '" + div_code + ",' and s.sf_status=0 and s.sf_name<>'admin'";


            try
            {
                dsDivision = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsDivision;
        }
		 public DataSet getDesignation_div(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;

          string strQry = " SELECT Designation_Code,Designation_Short_Name,Designation_Name,Designation_Short_Name + ' / ' + Designation_Name AS Name " +
                     " FROM Mas_SF_Designation where Division_Code = '" + div_code + "'  and Designation_Active_Flag=0 " +
                     " ORDER BY 2";
            try
            {
                dsSF = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsSF;
        }
	}
}