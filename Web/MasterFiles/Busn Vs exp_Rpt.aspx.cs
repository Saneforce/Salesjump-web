using DBase_EReport;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterFiles_Busn_Vs_exp_Rpt : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    public class pro_years
    {
        public string years { get; set; }
    }
    [WebMethod(EnableSession = true)]
    public static pro_years[] Get_Year()
    {
        List<pro_years> product = new List<pro_years>();
        B_exp tp = new B_exp();
        DataSet dsTP = null;
        string Div_code = HttpContext.Current.Session["div_code"].ToString();
        dsTP = tp.Get_Edit_Year(Div_code);
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
    [WebMethod]
    public static string Get_FieldForce(string exp_years, string exp_month)
    {
        DataSet ds = new DataSet();
        B_exp sf = new B_exp();
        ds = sf.get_FF(exp_years, exp_month);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    public class B_exp
    {
        public DataSet Get_Edit_Year(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            string strQry = "select max([Year]-1) as Year from Mas_Division where Division_Code='" + div_code + "'";
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

        public DataSet get_FF(string exp_years,string exp_month)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            string strQry = "exec expense_claim '"+ exp_years + "','"+ exp_month + "'";
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