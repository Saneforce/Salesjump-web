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


public partial class MasterFiles_Distance_Entry : System.Web.UI.Page
{

    #region "Declaration"
    DataSet dsStockist = null;
    string Division_code = string.Empty;
    string sf_code = string.Empty;
    string sf_type = string.Empty;
    #endregion
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
        Division_code = Session["Div_code"].ToString();
        if (!Page.IsPostBack)
        {
            GetTerritoryName();
        }
    }
    private void GetTerritoryName()
    {
        Stockist sk = new Stockist();
        dsStockist = sk.getTer_Name(Division_code);
        if (dsStockist.Tables[0].Rows.Count > 0)
        {
            ddlTerritoryName.DataTextField = "Territory_name";
            ddlTerritoryName.DataValueField = "Territory_code";

            ddlTerritoryName.DataSource = dsStockist;
            ddlTerritoryName.DataBind();
        }

    }

    public class Terr_Details
    {
        public string terr_code { get; set; }
        public string terr_Name { get; set; }
        public string alw_type { get; set; }

    }


    [WebMethod(EnableSession = true)]
    public static Terr_Details[] Get_Details(string terr_code)
    {

        Territory hq = new Territory();
        List<Terr_Details> Terrs = new List<Terr_Details>();
        string Div_code = HttpContext.Current.Session["div_code"].ToString();
        string Sf_Code = HttpContext.Current.Session["Sf_Code"].ToString();
        DataSet dsProduct = null;
        dsProduct = hq.get_Territory(Div_code, terr_code);

        DataRow[] Drr = null;
        Drr = dsProduct.Tables[0].Select("Allowance_Type <>'OS-EX' or Allowance_Type is null");

        if (Drr != null)
        {
            foreach (DataRow row in Drr)
            {
                Terr_Details terr = new Terr_Details();
                terr.terr_code = row["Territory_Code"].ToString();
                terr.terr_Name = row["Territory_Name"].ToString();
                terr.alw_type = row["Allowance_Type"].ToString();
                Terrs.Add(terr);

                DataRow[] dr =null;
                dr= dsProduct.Tables[0].Select("Territory_SNo='" + row["Territory_Code"].ToString() + "'");
                if (dr != null)
                {
                    foreach (DataRow ro in dr)
                    {

                        Terr_Details ter = new Terr_Details();
                        ter.terr_code = ro["Territory_Code"].ToString();
                        ter.terr_Name = ro["Territory_Name"].ToString();
                        ter.alw_type = ro["Allowance_Type"].ToString();
                        Terrs.Add(ter);
                    }

                }

            }
        }

        //foreach (DataRow row in dsProduct.Tables[0].Rows)
        //{
        //    Terr_Details terr = new Terr_Details();
        //    terr.terr_code = row["Territory_Code"].ToString();
        //    terr.terr_Name = row["Territory_Name"].ToString();
        //    Terrs.Add(terr);
        //}

        return Terrs.ToArray();
    }


    [WebMethod(EnableSession = true)]
    public static string savedata(string data)
    {

        string div_code = "";
        try
        {
            div_code = HttpContext.Current.Session["div_code"].ToString();
        }
        catch
        {
            div_code = HttpContext.Current.Session["Division_Code"].ToString();
        }
        var items = JsonConvert.DeserializeObject<List<Details>>(data);
        int co = 0;
        for (int i = 0; i < items.Count; i++)
        {
           
            Territorys ty = new Territorys();
            int iReturn = ty.Distance_add(div_code, items[i].terrhq, items[i].fromc, items[i].toc, Convert.ToDecimal(items[i].distance), items[i].Place_Type);
            co++;
        }
        if (co > 0)
        {
            return "Sucess";
        }
        else
        {
            return "Error";
        }
        // return items[0].week_name.ToString();

    }
    public class Details
    {
        public string terrhq { get; set; }
        public string distance { get; set; }
        public string fromc { get; set; }
        public string toc { get; set; }
        public string Place_Type { get; set; }  

    }

    [WebMethod(EnableSession = true)]
    public static Details[] getDistanceValue(string data)
    {
        string div_code = "";
        try
        {
            div_code = HttpContext.Current.Session["div_code"].ToString();

        }
        catch
        {
            div_code = HttpContext.Current.Session["Division_Code"].ToString();
        }

        Territorys ty = new Territorys();
        List<Details> mst = new List<Details>();     

        DataSet dsAccessmas = ty.Get_Distance_Values(div_code.TrimEnd(','), data);
        foreach (DataRow row in dsAccessmas.Tables[0].Rows)
        {
            Details ms = new Details();
            ms.fromc = row["Frm_Plc_Code"].ToString();
            ms.toc = row["To_Plc_Code"].ToString();
            ms.distance = row["Distance_KM"].ToString();
            mst.Add(ms);
        }
        return mst.ToArray();
    }
 
}