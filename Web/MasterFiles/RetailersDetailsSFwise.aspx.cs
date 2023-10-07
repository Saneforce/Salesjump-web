using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using Bus_EReport;

public partial class MasterFiles_RetailersDetailsSFwise : System.Web.UI.Page
{
    #region "Declaration"
    string div_code = string.Empty;
	 string sf_code = string.Empty;
     string sf_name = string.Empty;
     DataSet dsSalesForce = new DataSet();
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
		div_code = Session["div_code"].ToString();
        sf_code = Request.QueryString["SFCode"].ToString();
        sf_name = Request.QueryString["SFName"].ToString();
        hdsfName.Value = sf_name;
   Label3.Text = sf_name;
        loadData();
    }


    private void loadData()
    {
	//	DataSet dsProd1 = new DataSet();
     //   ListedDR LstDoc = new ListedDR();
      //  dsProd1 = LstDoc.getListedDrDashbord(div_code);
     //   RetailerGrd.DataSource = dsProd1;
      //  RetailerGrd.DataBind();

 SalesForce SF = new SalesForce();
        dsSalesForce = SF.getDoctorCount_SFWise_new(div_code, "0", sf_code);

        DataTable dtFF = new DataTable();
        dtFF.Columns.Add("sfCode", typeof(string));
        dtFF.Columns.Add("sfName", typeof(string));
        dtFF.Columns.Add("RteCount", typeof(string));
        dtFF.Columns.Add("DisCount", typeof(string));
        dtFF.Columns.Add("RutCount", typeof(string));
        dtFF.Columns.Add("sftype", typeof(string));

        decimal nrtcnt = 0;
        decimal nrocnt = 0;
        decimal ndicnt = 0;


        foreach (DataRow row in dsSalesForce.Tables[0].Rows)
        {
            decimal rtcnt = 0;
            decimal rocnt = 0;
            decimal dicnt = 0;
            DataSet dsCounts = new DataSet();
          //  DataSet distCount = new DataSet();
          //  DataSet routCount = new DataSet();
            if (sf_code == row["sf_code"].ToString())
            {
                
            }
            else
            {
                dsCounts = SF.GetDashbordCnt(div_code, row["sf_code"].ToString());
            }
            if (dsCounts.Tables.Count > 0)
            {
                //foreach (DataRow dr in dsCounts.Tables[0].Rows)
                //{
                //    rtcnt += dr["RetailerCnt"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["RetailerCnt"]);
                //    dicnt += dr["DistCnt"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["DistCnt"]);
                //    rocnt += dr["RouteCnt"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["RouteCnt"]);
                //}
                rtcnt = Convert.ToDecimal(dsCounts.Tables[0].Rows[0][0].ToString());
                dicnt = Convert.ToDecimal(dsCounts.Tables[1].Rows[0][0].ToString());
                rocnt = Convert.ToDecimal(dsCounts.Tables[2].Rows[0][0].ToString());
            }
            nrtcnt += rtcnt;
            nrocnt += rocnt;
            ndicnt += dicnt;
            dtFF.Rows.Add(row["sf_code"].ToString(), row["sf_name"].ToString(), rtcnt.ToString(), dicnt.ToString(), rocnt.ToString(), row["sf_type"].ToString());
        }
        dtFF.Rows.Add("", "Total", nrtcnt.ToString(), ndicnt.ToString(), nrocnt.ToString());
        RetailerGrd.DataSource = dtFF;
        RetailerGrd.DataBind();
    }
}