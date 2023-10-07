
using Bus_EReport;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using DBase_EReport;

public partial class MIS_Reports_new_view_dcrcals : System.Web.UI.Page
{
    public static string sfn = string.Empty;
    public static string subd = string.Empty;
    public static string div = string.Empty;
    public static string sf_code = string.Empty;
    public static string fdt = string.Empty;
    public static string tdt = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        sfn = Request.QueryString["Sf_Name"].ToString();
        subd = Request.QueryString["subdiv"].ToString();
        div = Request.QueryString["div_code"].ToString();
        sf_code = Request.QueryString["sf_code"].ToString();
        fdt = Request.QueryString["FromDate"].ToString();
        tdt = Request.QueryString["ToDate"].ToString();
        DateTime d1 = Convert.ToDateTime(fdt);
        DateTime d2 = Convert.ToDateTime(tdt);
        lblHead.Text = "Channel DCR Entry Report From" + "  " + d1.ToString("dd-MM-yyyy") + " to " + d2.ToString("dd-MM-yyyy");
        lblsf_name.Text = Convert.ToString(sfn);

    }
    [WebMethod(EnableSession = true)]
    public static string getSFdets()
    {
        chnl sf = new chnl();
        DataSet dsProd = sf.get_dcr_channelcall();
        return JsonConvert.SerializeObject(dsProd.Tables[0]);
    }
    [WebMethod(EnableSession = true)]
    public static string getchnlval()
    {
        chnl sf = new chnl();
        DataSet dsProd = sf.get_dcr_channelval();
        return JsonConvert.SerializeObject(dsProd.Tables[0]);

    }
    [WebMethod(EnableSession = true)]
    public static string getcallval()
    {
        chnl sf = new chnl();
        DataSet dsProd = sf.get_dcr_calval();
        return JsonConvert.SerializeObject(dsProd.Tables[0]);

    }
    [WebMethod(EnableSession = true)]
    public static string getchnl()
    {
        chnl sf = new chnl();
        DataSet dsProd = sf.Get_channel();
        return JsonConvert.SerializeObject(dsProd.Tables[0]);
    }
    [WebMethod(EnableSession = true)]
    public static string getcall()
    {
        chnl sf = new chnl();
        DataSet dsProd = sf.Get_call();
        return JsonConvert.SerializeObject(dsProd.Tables[0]);
    }
    public class chnl
    {
        public DataSet get_dcr_channelcall()
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            string strQry = "exec get_dcr_channelcall '" + div + "','" + sf_code + "','" + fdt + "','" + tdt + "'";
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
        public DataSet get_dcr_channelval()
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            string strQry = "exec get_dcr_channelval '" + div + "','" + sf_code + "','" + fdt + "','" + tdt + "'";
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
        public DataSet get_dcr_calval()
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            string strQry = "exec get_dcr_calval '" + div + "','" + sf_code + "','" + fdt + "','" + tdt + "'";
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
        public DataSet Get_channel()
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            string strQry = "select ChannelId,ChannelName from ChannelDCR where Division_Code='" + div + "' group by ChannelId,ChannelName order by ChannelName";
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
        public DataSet Get_call()
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            string strQry = "select CallsId,CallslName from ChannelDCRCalls where Division_Code='" + div + "' group by CallsId,CallslName order by CallslName";
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


//using Bus_EReport;
//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Data.SqlClient;
//using System.Linq;
//using System.Web;
//using System.Web.Services;
//using System.Web.UI;
//using System.Web.UI.WebControls;
//using Newtonsoft.Json;
//using DBase_EReport;

//public partial class MIS_Reports_new_view_dcrcals : System.Web.UI.Page
//{
//    public static string sfn = string.Empty;
//    public static string subd = string.Empty;
//    public static string div = string.Empty;
//    public static string sf_code = string.Empty;
//    public static string fdt = string.Empty;
//    public static string tdt = string.Empty;
//    protected void Page_Load(object sender, EventArgs e)
//    {
//        sfn = Request.QueryString["Sf_Name"].ToString();
//        subd = Request.QueryString["subdiv"].ToString();
//        div = Request.QueryString["div_code"].ToString();
//        sf_code = Request.QueryString["sf_code"].ToString();
//        fdt = Request.QueryString["FromDate"].ToString();
//        tdt = Request.QueryString["ToDate"].ToString();
//        DateTime d1 = Convert.ToDateTime(fdt);
//        DateTime d2 = Convert.ToDateTime(tdt);
//        lblHead.Text= "Channel DCR Entry Report From" + "  " + d1.ToString("dd-MM-yyyy") + " to " + d2.ToString("dd-MM-yyyy");
//        lblsf_name.Text = Convert.ToString(sfn);
       
//    }
//    [WebMethod(EnableSession = true)]
//    public static string getSFdets()
//    {
//        chnl sf = new chnl();
//        DataSet dsProd = sf.get_dcr_channelcall();
//        return JsonConvert.SerializeObject(dsProd.Tables[0]);
//    }
//    [WebMethod(EnableSession = true)]
//    public static string getchnlval()
//    {
//        chnl sf = new chnl();
//        DataSet dsProd = sf.get_dcr_channelval();
//        return JsonConvert.SerializeObject(dsProd.Tables[0]);

//    }
//    [WebMethod(EnableSession = true)]
//    public static string getcallval()
//    {
//        chnl sf = new chnl();
//        DataSet dsProd = sf.get_dcr_calval();
//        return JsonConvert.SerializeObject(dsProd.Tables[0]);

//    }
//    [WebMethod(EnableSession = true)]
//    public static string getchnl()
//    {
//        chnl sf = new chnl();
//        DataSet dsProd = sf.Get_channel();
//        return JsonConvert.SerializeObject(dsProd.Tables[0]);
//    }
//    [WebMethod(EnableSession = true)]
//    public static string getcall()
//    {
//        chnl sf = new chnl();
//        DataSet dsProd = sf.Get_call();
//        return JsonConvert.SerializeObject(dsProd.Tables[0]);
//    }
//    public class chnl
//    {
//        public DataSet get_dcr_channelcall()
//        {
//            DB_EReporting db_ER = new DB_EReporting();

//            DataSet dsDivision = null;
//            string strQry = "exec get_dcr_channelcall '" + div + "','" + sf_code + "','" + fdt + "','" + tdt + "'";
//            try
//            {
//                dsDivision = db_ER.Exec_DataSet(strQry);
//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }
//            return dsDivision;
//        }
//        public DataSet get_dcr_channelval()
//        {
//            DB_EReporting db_ER = new DB_EReporting();

//            DataSet dsDivision = null;
//            string strQry = "exec get_dcr_channelval '" + div + "','" + sf_code + "','" + fdt + "','" + tdt + "'";
//            try
//            {
//                dsDivision = db_ER.Exec_DataSet(strQry);
//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }
//            return dsDivision;
//        }
//        public DataSet get_dcr_calval()
//        {
//            DB_EReporting db_ER = new DB_EReporting();

//            DataSet dsDivision = null;
//            string strQry = "exec get_dcr_calval '" + div + "','" + sf_code + "','" + fdt + "','" + tdt + "'";
//            try
//            {
//                dsDivision = db_ER.Exec_DataSet(strQry);
//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }
//            return dsDivision;
//        }
//        public DataSet Get_channel()
//        {
//            DB_EReporting db_ER = new DB_EReporting();

//            DataSet dsDivision = null;
//            string strQry = "select ChannelId,ChannelName from ChannelDCR where Division_Code='" + div + "' group by ChannelId,ChannelName order by ChannelName";
//            try
//            {
//                dsDivision = db_ER.Exec_DataSet(strQry);
//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }
//            return dsDivision;
//        }
//        public DataSet Get_call()
//        {
//            DB_EReporting db_ER = new DB_EReporting();

//            DataSet dsDivision = null;
//            string strQry = "select CallsId,CallslName from ChannelDCRCalls where Division_Code='" + div + "' group by CallsId,CallslName order by CallslName";
//            try
//            {
//                dsDivision = db_ER.Exec_DataSet(strQry);
//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }
//            return dsDivision;
//        }
//    }
//    }