using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using DBase_EReport;
using Newtonsoft.Json;

public partial class MasterFiles_Exp_Caln : System.Web.UI.Page
{
    public static string div_code = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
    }
	[WebMethod(EnableSession = true)]
    public static string getprevalue(string divcode)
    {
        loc sf = new loc();
        DataSet dsSalesForce = sf.prevalue(divcode);

        return JsonConvert.SerializeObject(dsSalesForce.Tables[0]);
    }
    [WebMethod]
    public static string insertdata(string res_arr)
    {
        
        string msg = string.Empty;
        DB_EReporting dB = new DB_EReporting();
        try
        {
            var items = JsonConvert.DeserializeObject<List<svmapping>>(res_arr);
            string sxml = "<ROOT>";
            for (int k = 0; k < items.Count; k++)
            {
                sxml += "<Calen Per=\"" + items[k].Period + "\" fdt=\"" + items[k].FDate + "\" tdt=\"" + items[k].TDate + "\" mnth=\"" + items[k].month + "\" yr=\"" + items[k].year + "\" divcode=\"" + div_code + "\"  />";

            }
            sxml += "</ROOT>";
            string svquery = "exec sp_Expense_Calender '" + sxml + "'";
            int result = 0;
            result = dB.ExecQry(svquery);
            msg = "true";
        }
        catch (Exception exp)
        {
            throw exp;
        }
        return msg;
    }
    public class svmapping
    {
        public string Period { get; set; }
        public string FDate { get; set; }
        public string TDate { get; set; }
        public string month { get; set; }
        public string year { get; set; }
        public string div { get; set; }
        //public string distcode { get; set; }
    }
	public class loc
    {
        public DataSet prevalue(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            
            string strQry = "EXEC sp_Exp_Caln '" + divcode + "'";
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
    }
}