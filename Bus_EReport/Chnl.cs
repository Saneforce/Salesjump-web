using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DBase_EReport;
using System.Configuration;

namespace Bus_EReport
{
    public class Chnl
    {
        public DataSet get_dcr_channelcall(string div, string sf_code, string fdt, string tdt, string subd = "0", string statecode = "0")
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;

            string strQry = "";

            if (div == "98")
            {
                strQry = "exec get_dcr_channelcall '" + div + "','" + sf_code + "','" + fdt + "','" + tdt + "','" + subd + "'," + Convert.ToInt32(statecode) + "";
            }
            else
            {
                strQry = "exec get_dcr_channelcall '" + div + "','" + sf_code + "','" + fdt + "','" + tdt + "','" + subd + "'," + Convert.ToInt32(statecode) + "";
            }

            //strQry = "exec get_dcr_channelcall '" + div + "','" + sf_code + "','" + fdt + "','" + tdt + "'";

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
        public DataSet get_dcr_channelval(string div, string sf_code, string fdt, string tdt, string subd = "0", string statecode = "0")
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;

            string strQry = "";

            if (div == "98")
            {
                strQry = "exec get_dcr_channelval '" + div + "','" + sf_code + "','" + fdt + "','" + tdt + "','" + subd + "'," + Convert.ToInt32(statecode) + "";
            }
            else
            {
                strQry = "exec get_dcr_channelval '" + div + "','" + sf_code + "','" + fdt + "','" + tdt + "','" + subd + "'," + Convert.ToInt32(statecode) + "";
            }

            //strQry = "exec get_dcr_channelval '" + div + "','" + sf_code + "','" + fdt + "','" + tdt + "'";

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
        public DataSet get_dcr_calval(string div, string sf_code, string fdt, string tdt, string subd = "0", string statecode = "0")
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;

            string strQry = "";
            if (div == "98")
            {
                strQry = "exec get_dcr_calval '" + div + "','" + sf_code + "','" + fdt + "','" + tdt + "','" + subd + "'," + Convert.ToInt32(statecode) + "";
            }
            else
            {
                strQry = "exec get_dcr_calval '" + div + "','" + sf_code + "','" + fdt + "','" + tdt + "','" + subd + "'," + Convert.ToInt32(statecode) + "";
            }
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
        public DataSet Get_channel(string div)
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
        public DataSet Get_call(string div)
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
