using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DBase_EReport;
using System.Configuration;
using Newtonsoft.Json;
using System.Data.SqlClient;


namespace Bus_EReport
{
    public class Sales
    {
        private string strQry = string.Empty;

        public DataSet Sales_statewise_Trend_analysis_Distributor_descending(string div_code, string fromdate, string todate, string subdivision, string state_cd)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;
            if (div_code != "4")
            {
                strQry = "  select st.state_code,st.statename,st.shortname ,sum(t.Sale_Qty * t.Retailor_Rate)as value  from Trans_Secondary_Sales_Details  t INNER JOIN Mas_Stockist S  " +
                   " ON t.Stockist_code=S.Stockist_Code right outer join mas_state st on st.State_Code=s.State_Code " +
               "where  t.date between '" + fromdate + "' and '" + todate + "' and   st.state_code in (" + state_cd + ") and st.State_Active_Flag=0 and s.Division_Code='" + div_code + "' and subdivision_code='" + subdivision + "' " +
               " group by  st.state_code,st.statename,st.shortname order by value desc ";
            }
            else
            {
                strQry = "  select st.state_code,st.statename,st.shortname ,sum((t.Sale_Qty * t.Retailor_Rate)+(t.sale_pieces * t.RP_BaseRate))as value from Trans_Secondary_Sales_Details  t INNER JOIN Mas_Stockist S  " +
                              " ON t.Stockist_code=S.Stockist_Code right outer join mas_state st on st.State_Code=s.State_Code " +
                          "where  t.date between '" + fromdate + "' and '" + todate + "' and   st.state_code in (" + state_cd + ") and st.State_Active_Flag=0 and s.Division_Code='" + div_code + "' and subdivision_code='" + subdivision + "' " +
                          " group by  st.state_code,st.statename,st.shortname order by value desc ";
            }

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
        public DataSet Sales_statewise_Trend_analysis_totalvalue(string div_code, string fromdate, string todate, string subdivision)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            if (div_code != "4")
            {

                strQry = "   select sum(t.Sale_Qty * t.Retailor_Rate)as value  from Trans_Secondary_Sales_Details  t INNER JOIN Mas_Stockist S " +
                    "   ON t.Stockist_code=S.Stockist_Code right outer join mas_state st on st.State_Code=s.State_Code " +
              " where  t.date between '" + fromdate + "' and '" + todate + "'  and st.State_Active_Flag=0 and s.Division_Code='" + div_code + "' and subdivision_code='" + subdivision + "' " +
              "order by value desc";
            }
            else
            {
                strQry = "   select sum((t.Sale_Qty * t.Retailor_Rate)+(t.sale_pieces * t.RP_BaseRate))as value  from Trans_Secondary_Sales_Details  t INNER JOIN Mas_Stockist S " +
                               "   ON t.Stockist_code=S.Stockist_Code right outer join mas_state st on st.State_Code=s.State_Code " +
                         " where  t.date between '" + fromdate + "' and '" + todate + "'  and st.State_Active_Flag=0 and s.Division_Code='" + div_code + "' and subdivision_code='" + subdivision + "' " +
                         "order by value desc";
            }
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

        public DataSet Sales_statewise_Trend_analysis_value_singleProduct(string product_code, string div_code, int fmonth, int fyear, string subdivision, string statecode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "   select sum(t.Sale_Qty * t.Retailor_Rate)as value  from Trans_Secondary_Sales_Details  t INNER JOIN Mas_Stockist S " +
                "   ON t.Stockist_code=S.Stockist_Code right outer join mas_state st on st.State_Code=s.State_Code " +
          " where   month(date)='" + fmonth + "' and YEAR(date)='" + fyear + "'  and t.Product_Code='" + product_code + "' and st.state_code in (" + statecode + ") and st.State_Active_Flag=0 and s.Division_Code='" + div_code + "' and subdivision_code='" + subdivision + "' " +
          "order by value desc";

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
        public DataSet Sales_statewise_Trend_analysis_value_singleProduct_total(string div_code, int fmonth, int fyear, string subdivision, string statecode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "   select sum(t.Sale_Qty * t.Retailor_Rate)as value  from Trans_Secondary_Sales_Details  t INNER JOIN Mas_Stockist S " +
                "   ON t.Stockist_code=S.Stockist_Code right outer join mas_state st on st.State_Code=s.State_Code " +
          " where   month(date)='" + fmonth + "' and YEAR(date)='" + fyear + "'  and st.State_Active_Flag=0  and st.state_code in (" + statecode + ")and s.Division_Code='" + div_code + "' and subdivision_code='" + subdivision + "' " +
          "order by value desc";

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
        public DataSet Sales_areawise_Trend_analysis_singleproduct(string product_code, string div_code, int fmonth, int fyear, string subdivision, string statecode, string areacode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "select sum(t.Sale_Qty * t.Retailor_Rate)as value  from Trans_Secondary_Sales_Details  t INNER JOIN Mas_Stockist S   " +
     " ON t.Stockist_code=S.Stockist_Code inner join Mas_Territory Tt on Tt.Territory_code=S.Territory_Code INNER JOIN Mas_Zone Z ON Z.Zone_code=Tt.Zone_code  right outer join Mas_Area a  ON a.Area_code=Z.Area_code  " +
     "where  month(date)='" + fmonth + "' and YEAR(date)='" + fyear + "'  and t.Product_Code='" + product_code + "' and  a.Area_code='" + areacode + "'  and a.Area_Active_Flag=0 and s.Division_Code='" + div_code + "'  and subdivision_code='" + subdivision + "' and a.State_Code='" + statecode + "' " +
     " order by value desc";

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
        public DataSet Sales_areawise_Trend_analysis_singleproduct_total(string div_code, int fmonth, int fyear, string subdivision, string statecode, string areacode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "select sum(t.Sale_Qty * t.Retailor_Rate)as value  from Trans_Secondary_Sales_Details  t INNER JOIN Mas_Stockist S   " +
     " ON t.Stockist_code=S.Stockist_Code inner join Mas_Territory Tt on Tt.Territory_code=S.Territory_Code INNER JOIN Mas_Zone Z ON Z.Zone_code=Tt.Zone_code  right outer join Mas_Area a  ON a.Area_code=Z.Area_code  " +
     "where  month(date)='" + fmonth + "' and YEAR(date)='" + fyear + "'   and  a.Area_code='" + areacode + "'  and a.Area_Active_Flag=0 and s.Division_Code='" + div_code + "'  and subdivision_code='" + subdivision + "' and a.State_Code='" + statecode + "' " +
     " order by value desc";

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
        public DataSet Sales_Zonewise_Trend_analysis_totalvalue_singleprdt(string product_code, string div_code, int fmonth, int fyear, string subdivision, string statecode, string area_code, string zonecode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = " select  sum(t.Sale_Qty * t.Retailor_Rate)as value  from Trans_Secondary_Sales_Details  t INNER JOIN Mas_Stockist S " +
            "  ON t.Stockist_code=S.Stockist_Code inner join Mas_Territory Tt on Tt.Territory_code=S.Territory_Code right outer JOIN Mas_Zone Z ON Z.Zone_code=Tt.Zone_code  inner join Mas_Area a  ON a.Area_code=Z.Area_code " +
          " where   month(date)='" + fmonth + "' and YEAR(date)='" + fyear + "'  and t.Product_Code='" + product_code + "' and  s.Division_Code='" + div_code + "'  and a.State_Code='" + statecode + "'    and a.Area_Active_Flag=0 and z.Zone_Active_Flag=0 and s.Division_Code='" + div_code + "'  and subdivision_code='" + subdivision + "' and  a.Area_code='" + area_code + "'and z.Zone_code='" + zonecode + "' " +
           "order by value desc";

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
        public DataSet Sales_Zonewise_Trend_analysis_totalvalue_singleprdt_total(string div_code, int fmonth, int fyear, string subdivision, string statecode, string area_code, string zonecode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = " select  sum(t.Sale_Qty * t.Retailor_Rate)as value  from Trans_Secondary_Sales_Details  t INNER JOIN Mas_Stockist S " +
            "  ON t.Stockist_code=S.Stockist_Code inner join Mas_Territory Tt on Tt.Territory_code=S.Territory_Code right outer JOIN Mas_Zone Z ON Z.Zone_code=Tt.Zone_code  inner join Mas_Area a  ON a.Area_code=Z.Area_code " +
          " where   month(date)='" + fmonth + "' and YEAR(date)='" + fyear + "'  and   s.Division_Code='" + div_code + "'  and a.State_Code='" + statecode + "'    and a.Area_Active_Flag=0 and z.Zone_Active_Flag=0 and s.Division_Code='" + div_code + "'  and subdivision_code='" + subdivision + "' and  a.Area_code='" + area_code + "'and z.Zone_code='" + zonecode + "' " +
           "order by value desc";

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
        public DataSet Sales_areawise_Trend_analysis_Distributor_descending(string div_code, string fromdate, string todate, string subdivision, string state_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            if (div_code != "4")
            {

                strQry = " select a.Area_code,a.Area_name,sum(t.Sale_Qty * t.Retailor_Rate)as value  from Trans_Secondary_Sales_Details  t INNER JOIN Mas_Stockist S  " +
                 "   ON t.Stockist_code=S.Stockist_Code inner join Mas_Territory Tt on Tt.Territory_code=S.Territory_Code INNER JOIN Mas_Zone Z ON Z.Zone_code=Tt.Zone_code  right outer join Mas_Area a  ON a.Area_code=Z.Area_code  " +
                " where  t.date between '" + fromdate + "' and '" + todate + "'  and a.Area_Active_Flag=0 and s.Division_Code='" + div_code + "'  and subdivision_code='" + subdivision + "' and a.State_Code='" + state_code + "' " +
                 " group by  a.Area_code,a.Area_name order by value desc  ";

            }
            else
            {

                strQry = " select a.Area_code,a.Area_name,sum((t.Sale_Qty * t.Retailor_Rate)+(t.sale_pieces * t.RP_BaseRate))as value from Trans_Secondary_Sales_Details  t INNER JOIN Mas_Stockist S  " +
                            "   ON t.Stockist_code=S.Stockist_Code inner join Mas_Territory Tt on Tt.Territory_code=S.Territory_Code INNER JOIN Mas_Zone Z ON Z.Zone_code=Tt.Zone_code  right outer join Mas_Area a  ON a.Area_code=Z.Area_code  " +
                           " where  t.date between '" + fromdate + "' and '" + todate + "'  and a.Area_Active_Flag=0 and s.Division_Code='" + div_code + "'  and subdivision_code='" + subdivision + "' and a.State_Code='" + state_code + "' " +
                            " group by  a.Area_code,a.Area_name order by value desc  ";
            }

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

        public DataSet Sales_areawise_Trend_analysis_totalvalue(string div_code, string fromdate, string todate, string subdivision, string statecode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;
            if (div_code != "4")
            {

                strQry = "select sum(t.Sale_Qty * t.Retailor_Rate)as value  from Trans_Secondary_Sales_Details  t INNER JOIN Mas_Stockist S   " +
       " ON t.Stockist_code=S.Stockist_Code inner join Mas_Territory Tt on Tt.Territory_code=S.Territory_Code INNER JOIN Mas_Zone Z ON Z.Zone_code=Tt.Zone_code  right outer join Mas_Area a  ON a.Area_code=Z.Area_code  " +
    "where  t.date between '" + fromdate + "' and '" + todate + "'   and a.Area_Active_Flag=0 and s.Division_Code='" + div_code + "'  and subdivision_code='" + subdivision + "' and a.State_Code='" + statecode + "' " +
    " order by value desc";

            }
            else
            {
                strQry = "select sum((t.Sale_Qty * t.Retailor_Rate)+(t.sale_pieces * t.RP_BaseRate))as value  from Trans_Secondary_Sales_Details  t INNER JOIN Mas_Stockist S   " +
                   " ON t.Stockist_code=S.Stockist_Code inner join Mas_Territory Tt on Tt.Territory_code=S.Territory_Code INNER JOIN Mas_Zone Z ON Z.Zone_code=Tt.Zone_code  right outer join Mas_Area a  ON a.Area_code=Z.Area_code  " +
                "where  t.date between '" + fromdate + "' and '" + todate + "'   and a.Area_Active_Flag=0 and s.Division_Code='" + div_code + "'  and subdivision_code='" + subdivision + "' and a.State_Code='" + statecode + "' " +
                " order by value desc";
            }

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
        public DataSet Sales_Zonewise_Trend_analysis_Distributor_descending(string div_code, string fromdate, string todate, string subdivision, string state_code, string area_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            if (div_code != "4")
            {

                strQry = " select z.Zone_code,z.Zone_name, sum(t.Sale_Qty * t.Retailor_Rate)as value  from Trans_Secondary_Sales_Details  t INNER JOIN Mas_Stockist S  " +
                 " ON t.Stockist_code=S.Stockist_Code inner join Mas_Territory Tt on Tt.Territory_code=S.Territory_Code right outer JOIN Mas_Zone Z ON Z.Zone_code=Tt.Zone_code  inner join Mas_Area a  ON a.Area_code=Z.Area_code  " +
               " where  t.date between '" + fromdate + "' and '" + todate + "'   and a.Area_Active_Flag=0 and z.Zone_Active_Flag=0 and s.Division_Code='" + div_code + "'  and subdivision_code='" + subdivision + "' and a.State_Code='" + state_code + "' and a.Area_code='" + area_code + "' " +
               " group by  z.Zone_code,z.Zone_name order by value desc";
            }
            else
            {
                strQry = " select z.Zone_code,z.Zone_name,sum((t.Sale_Qty * t.Retailor_Rate)+(t.sale_pieces * t.RP_BaseRate))as value  from Trans_Secondary_Sales_Details  t INNER JOIN Mas_Stockist S  " +
                          " ON t.Stockist_code=S.Stockist_Code inner join Mas_Territory Tt on Tt.Territory_code=S.Territory_Code right outer JOIN Mas_Zone Z ON Z.Zone_code=Tt.Zone_code  inner join Mas_Area a  ON a.Area_code=Z.Area_code  " +
                        " where  t.date between '" + fromdate + "' and '" + todate + "'   and a.Area_Active_Flag=0 and z.Zone_Active_Flag=0 and s.Division_Code='" + div_code + "'  and subdivision_code='" + subdivision + "' and a.State_Code='" + state_code + "' and a.Area_code='" + area_code + "' " +
                        " group by  z.Zone_code,z.Zone_name order by value desc";
            }
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

        public DataSet Sales_Zonewise_Trend_analysis_totalvalue(string div_code, string fromdate, string todate, string subdivision, string statecode, string area_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            if (div_code != "4")
            {
                strQry = " select  sum(t.Sale_Qty * t.Retailor_Rate)as value  from Trans_Secondary_Sales_Details  t INNER JOIN Mas_Stockist S " +
                "  ON t.Stockist_code=S.Stockist_Code inner join Mas_Territory Tt on Tt.Territory_code=S.Territory_Code right outer JOIN Mas_Zone Z ON Z.Zone_code=Tt.Zone_code  inner join Mas_Area a  ON a.Area_code=Z.Area_code " +
              " where  t.date between '" + fromdate + "' and '" + todate + "'   and a.Area_Active_Flag=0 and z.Zone_Active_Flag=0 and s.Division_Code='" + div_code + "'  and subdivision_code='" + subdivision + "' and a.State_Code='" + statecode + "' and a.Area_code='" + area_code + "' " +
               "order by value desc";
            }
            else
            {

                strQry = " select sum((t.Sale_Qty * t.Retailor_Rate)+(t.sale_pieces * t.RP_BaseRate))as value from Trans_Secondary_Sales_Details  t INNER JOIN Mas_Stockist S " +
                          "  ON t.Stockist_code=S.Stockist_Code inner join Mas_Territory Tt on Tt.Territory_code=S.Territory_Code right outer JOIN Mas_Zone Z ON Z.Zone_code=Tt.Zone_code  inner join Mas_Area a  ON a.Area_code=Z.Area_code " +
                        " where  t.date between '" + fromdate + "' and '" + todate + "'   and a.Area_Active_Flag=0 and z.Zone_Active_Flag=0 and s.Division_Code='" + div_code + "'  and subdivision_code='" + subdivision + "' and a.State_Code='" + statecode + "' and a.Area_code='" + area_code + "' " +
                         "order by value desc";
            }

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
        public DataSet Sales_Territorywise_Trend_analysis_Distributor_descending(string div_code, string fromdate, string todate, string subdivision, string state_code, string area_code, string zonecode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            if (div_code != "4")
            {

                strQry = "select Tt.Territory_code,Tt.Territory_Name, sum(t.Sale_Qty * t.Retailor_Rate)as value  from Trans_Secondary_Sales_Details  t INNER JOIN Mas_Stockist S  " +
                "  ON t.Stockist_code=S.Stockist_Code RIGHT OUTER join Mas_Territory Tt on Tt.Territory_code=S.Territory_Code INNER JOIN Mas_Zone Z ON Z.Zone_code=Tt.Zone_code  inner join Mas_Area a  ON a.Area_code=Z.Area_code " +
             "where  t.date between '" + fromdate + "' and '" + todate + "'   and a.Area_Active_Flag=0 and z.Zone_Active_Flag=0 and s.Division_Code='" + div_code + "'  and subdivision_code='" + subdivision + "' and a.State_Code='" + state_code + "' and a.Area_code='" + area_code + "' AND TT.Zone_code='" + zonecode + "' " +
             " group by  Tt.Territory_code,Tt.Territory_Name order by value desc";
            }
            else
            {

                strQry = "select Tt.Territory_code,Tt.Territory_Name, sum((t.Sale_Qty * t.Retailor_Rate)+(t.sale_pieces * t.RP_BaseRate))as value from Trans_Secondary_Sales_Details  t INNER JOIN Mas_Stockist S  " +
                "  ON t.Stockist_code=S.Stockist_Code RIGHT OUTER join Mas_Territory Tt on Tt.Territory_code=S.Territory_Code INNER JOIN Mas_Zone Z ON Z.Zone_code=Tt.Zone_code  inner join Mas_Area a  ON a.Area_code=Z.Area_code " +
             "where  t.date between '" + fromdate + "' and '" + todate + "'   and a.Area_Active_Flag=0 and z.Zone_Active_Flag=0 and s.Division_Code='" + div_code + "'  and subdivision_code='" + subdivision + "' and a.State_Code='" + state_code + "' and a.Area_code='" + area_code + "' AND TT.Zone_code='" + zonecode + "' " +
             " group by  Tt.Territory_code,Tt.Territory_Name order by value desc";

            }
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

        public DataSet Sales_Territorywise_Trend_analysis_totalvalue(string div_code, string fromdate, string todate, string subdivision, string statecode, string area_code, string zonecode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;
            if (div_code != "4")
            {

                strQry = " select  sum(t.Sale_Qty * t.Retailor_Rate)as value  from Trans_Secondary_Sales_Details  t INNER JOIN Mas_Stockist S  " +
                 " ON t.Stockist_code=S.Stockist_Code RIGHT OUTER join Mas_Territory Tt on Tt.Territory_code=S.Territory_Code INNER JOIN Mas_Zone Z ON Z.Zone_code=Tt.Zone_code  inner join Mas_Area a  ON a.Area_code=Z.Area_code  " +
              " where  t.date between '" + fromdate + "' and '" + todate + "'   and a.Area_Active_Flag=0 and z.Zone_Active_Flag=0 and s.Division_Code='" + div_code + "'  and subdivision_code='" + subdivision + "' and a.State_Code='" + statecode + "' and a.Area_code='" + area_code + "' AND TT.Zone_code='" + zonecode + "' " +
               " order by value desc";
            }
            else
            {
                strQry = " select  sum((t.Sale_Qty * t.Retailor_Rate)+(t.sale_pieces * t.RP_BaseRate))as value from Trans_Secondary_Sales_Details  t INNER JOIN Mas_Stockist S  " +
                            " ON t.Stockist_code=S.Stockist_Code RIGHT OUTER join Mas_Territory Tt on Tt.Territory_code=S.Territory_Code INNER JOIN Mas_Zone Z ON Z.Zone_code=Tt.Zone_code  inner join Mas_Area a  ON a.Area_code=Z.Area_code  " +
                         " where  t.date between '" + fromdate + "' and '" + todate + "'   and a.Area_Active_Flag=0 and z.Zone_Active_Flag=0 and s.Division_Code='" + div_code + "'  and subdivision_code='" + subdivision + "' and a.State_Code='" + statecode + "' and a.Area_code='" + area_code + "' AND TT.Zone_code='" + zonecode + "' " +
                          " order by value desc";
            }

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
        public DataSet Sales_Territorywise_Trend_analysis_totalvalue_singleprt(string product_code, string div_code, int fmonth, int fyear, string subdivision, string statecode, string area_code, string zonecode, string territory_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = " select  sum(t.Sale_Qty * t.Retailor_Rate)as value  from Trans_Secondary_Sales_Details  t INNER JOIN Mas_Stockist S  " +
             " ON t.Stockist_code=S.Stockist_Code RIGHT OUTER join Mas_Territory Tt on Tt.Territory_code=S.Territory_Code INNER JOIN Mas_Zone Z ON Z.Zone_code=Tt.Zone_code  inner join Mas_Area a  ON a.Area_code=Z.Area_code  " +
          " where   month(date)='" + fmonth + "' and YEAR(date)='" + fyear + "' and t.Product_Code='" + product_code + "'  and a.Area_Active_Flag=0 and z.Zone_Active_Flag=0 and s.Division_Code='" + div_code + "'  and subdivision_code='" + subdivision + "' and a.State_Code='" + statecode + "' and a.Area_code='" + area_code + "' AND TT.Zone_code='" + zonecode + "' and s.Territory_Code='" + territory_code + "' " +
           " order by value desc";

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

        public DataSet Sales_Territorywise_Trend_analysis_totalvalue_singleprt_total(string div_code, int fmonth, int fyear, string subdivision, string statecode, string area_code, string zonecode, string territory_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = " select  sum(t.Sale_Qty * t.Retailor_Rate)as value  from Trans_Secondary_Sales_Details  t INNER JOIN Mas_Stockist S  " +
             " ON t.Stockist_code=S.Stockist_Code RIGHT OUTER join Mas_Territory Tt on Tt.Territory_code=S.Territory_Code INNER JOIN Mas_Zone Z ON Z.Zone_code=Tt.Zone_code  inner join Mas_Area a  ON a.Area_code=Z.Area_code  " +
          " where   month(date)='" + fmonth + "' and YEAR(date)='" + fyear + "'  and a.Area_Active_Flag=0 and z.Zone_Active_Flag=0 and s.Division_Code='" + div_code + "'  and subdivision_code='" + subdivision + "' and a.State_Code='" + statecode + "' and a.Area_code='" + area_code + "' AND TT.Zone_code='" + zonecode + "' and s.Territory_Code='" + territory_code + "' " +
           " order by value desc";

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
        //   public DataSet Sales_Gettopexception_statewise(string divcode, string year, string state_code)
        public DataSet Sales_Gettopexception_statewise(string divcode, string year, string state_code, string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            //    strQry = " select st.state_code,st.statename,st.shortname ,sum(t.Rec_Qty * t.Distributer_Rate)as value  from Trans_Secondary_Sales_Details  t INNER JOIN Mas_Stockist S " +
            //  "ON t.Stockist_code=S.Stockist_Code right outer join mas_state st on st.State_Code=s.State_Code  " +
            //   "where year( t.date)='" + year + "'  and   st.state_code in (" + state_code + ") and st.State_Active_Flag=0 and s.Division_Code='" + divcode + "' " +
            //   "group by  st.state_code,st.statename,st.shortname order by value desc";
            strQry = "EXEC GET_SALES_EXCEPTION_STATE '" + divcode + "','','" + sf_code + "','" + year + "','" + state_code + "'";
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
        public DataSet Sales_Gettopexception_statewise_total(string divcode, string year, string state_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            strQry = " select sum(t.Rec_Qty * t.Distributer_Rate)as value  from Trans_Secondary_Sales_Details  t INNER JOIN Mas_Stockist S " +
         "ON t.Stockist_code=S.Stockist_Code right outer join mas_state st on st.State_Code=s.State_Code " +
          "where year( t.date)='" + year + "'  and   st.state_code in (" + state_code + ") and st.State_Active_Flag=0 and s.Division_Code='" + divcode + "' ";


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


        //   public DataSet Sales_Gettopexception_areawise(string divcode, string year, string state_code)
        public DataSet Sales_Gettopexception_areawise(string divcode, string year, string state_code, string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            //      strQry = "select a.Area_code,a.Area_name,sum(t.Rec_Qty * t.Distributer_Rate)as value  from Trans_Secondary_Sales_Details  t INNER JOIN Mas_Stockist S  " +
            //     "   ON t.Stockist_code=S.Stockist_Code inner join Mas_Territory Tt on Tt.Territory_code=S.Territory_Code INNER JOIN Mas_Zone Z ON Z.Zone_code=Tt.Zone_code  right outer join Mas_Area a  ON a.Area_code=Z.Area_code  " +
            //    "    where year( t.date)='" + year + "'   and a.Area_Active_Flag=0 and s.Division_Code='" + divcode + "'  and a.State_Code in (" + state_code + ") " +
            //     " group by  a.Area_code,a.Area_name order by value desc ";

            strQry = "EXEC GET_SALES_EXCEPTION_AREA '" + divcode + "','','" + sf_code + "','" + state_code + "','" + year + "'";
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
        public DataSet Sales_Gettopexception_areawise_total(string divcode, string year, string state_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            strQry = "select sum(t.Rec_Qty * t.Distributer_Rate)as value  from Trans_Secondary_Sales_Details  t INNER JOIN Mas_Stockist S  " +
           "   ON t.Stockist_code=S.Stockist_Code inner join Mas_Territory Tt on Tt.Territory_code=S.Territory_Code INNER JOIN Mas_Zone Z ON Z.Zone_code=Tt.Zone_code  right outer join Mas_Area a  ON a.Area_code=Z.Area_code  " +
          "    where year( t.date)='" + year + "'   and a.Area_Active_Flag=0 and s.Division_Code='" + divcode + "'  and a.State_Code in (" + state_code + ")";



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


        //        public DataSet Sales_Gettopexception_zonewise(string divcode, string year, string state_code)
        public DataSet Sales_Gettopexception_zonewise(string divcode, string year, string state_code, string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            //        strQry = " select z.Zone_code,z.Zone_name, sum(t.Rec_Qty * t.Distributer_Rate)as value  from Trans_Secondary_Sales_Details  t INNER JOIN Mas_Stockist S  " +
            //         " ON t.Stockist_code=S.Stockist_Code inner join Mas_Territory Tt on Tt.Territory_code=S.Territory_Code right outer JOIN Mas_Zone Z ON Z.Zone_code=Tt.Zone_code  inner join Mas_Area a  ON a.Area_code=Z.Area_code  " +
            //       " where  year( t.date)='" + year + "'    and a.Area_Active_Flag=0 and z.Zone_Active_Flag=0 and s.Division_Code='" + divcode + "'  and a.State_Code in (" + state_code + ")  " +
            //       " group by  z.Zone_code,z.Zone_name order by value desc";
            strQry = "EXEC GET_SALES_EXCEPTION_ZONE '" + divcode + "','','" + sf_code + "','" + year + "','" + state_code + "'";
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
        public DataSet Sales_Gettopexception_zonewise_total(string divcode, string year, string state_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            strQry = " select sum(t.Rec_Qty * t.Distributer_Rate)as value  from Trans_Secondary_Sales_Details  t INNER JOIN Mas_Stockist S  " +
              " ON t.Stockist_code=S.Stockist_Code inner join Mas_Territory Tt on Tt.Territory_code=S.Territory_Code right outer JOIN Mas_Zone Z ON Z.Zone_code=Tt.Zone_code  inner join Mas_Area a  ON a.Area_code=Z.Area_code  " +
            " where  year( t.date)='" + year + "'    and a.Area_Active_Flag=0 and z.Zone_Active_Flag=0 and s.Division_Code='" + divcode + "'  and a.State_Code in (" + state_code + ")  ";




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

        //  public DataSet Sales_Gettopexception_territorywise(string divcode, string year, string state_code)
        public DataSet Sales_Gettopexception_territorywise(string divcode, string year, string state_code, string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            //        strQry = " select Tt.Territory_code,Tt.Territory_Name, sum(t.Rec_Qty * t.Distributer_Rate)as value  from Trans_Secondary_Sales_Details  t INNER JOIN Mas_Stockist S  " +
            //       "  ON t.Stockist_code=S.Stockist_Code RIGHT OUTER join Mas_Territory Tt on Tt.Territory_code=S.Territory_Code INNER JOIN Mas_Zone Z ON Z.Zone_code=Tt.Zone_code  inner join Mas_Area a  ON a.Area_code=Z.Area_code " +
            //     "where   year( t.date)='" + year + "'    and a.Area_Active_Flag=0 and z.Zone_Active_Flag=0 and s.Division_Code='" + divcode + "'  and a.State_Code in (" + state_code + ") " +
            //      " group by  Tt.Territory_code,Tt.Territory_Name order by value desc";
            strQry = "EXEC GET_SALES_EXCEPTION_TERRITORY '" + divcode + "','','" + sf_code + "','" + year + "','" + state_code + "'";
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
        public DataSet Sales_Gettopexception_territorywise_total(string divcode, string year, string state_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            strQry = " select sum(t.Rec_Qty * t.Distributer_Rate)as value  from Trans_Secondary_Sales_Details  t INNER JOIN Mas_Stockist S  " +
            "  ON t.Stockist_code=S.Stockist_Code RIGHT OUTER join Mas_Territory Tt on Tt.Territory_code=S.Territory_Code INNER JOIN Mas_Zone Z ON Z.Zone_code=Tt.Zone_code  inner join Mas_Area a  ON a.Area_code=Z.Area_code " +
         "where   year( t.date)='" + year + "'    and a.Area_Active_Flag=0 and z.Zone_Active_Flag=0 and s.Division_Code='" + divcode + "'  and a.State_Code in (" + state_code + ") ";




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
        public DataSet GetSalesReturnHeader(string SFCode, string FYear, string FMonth)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            strQry = "exec salReturnHead '" + SFCode + "','" + FYear + "','" + FMonth + "'";
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
        public DataSet GetSalesReturnDetails(string rtCode,string distCode,string cDate)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            strQry = "exec salReturnDetails '" + rtCode + "','" + distCode + "','" + cDate + "'";
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

        public DataSet getRetailBusinessID(string divcode)
        {
            DB_EReporting db = new DB_EReporting();
            DataSet ds = null;
            strQry = "exec getRetailBusinessID '" + divcode + "'";
            try
            {
                ds = db.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
        public string saveRetailBusiness(SaveRetailBusiness ss)
        {
            DB_EReporting db = new DB_EReporting();
            DataSet ds = null;
            string msg = string.Empty;
            strQry = "exec insertRetailBusiness '" + ss.divcode + "','" + ss.rbscode + "','" + ss.rbsname + "','" + ss.rbsdesc + "','" + ss.rbsminvalue + "','" + ss.rbsmaxvalue + "','" + ss.rbsstatus + "','" + ss.rbfdt + "','" + ss.rbtdt + "','" + ss.rdedt + "','" + ss.hqid + "'";
            try
            {
                ds = db.Exec_DataSet(strQry);
                msg = "Success";
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            return msg;
        }
        public class SaveRetailBusiness
        {
            [JsonProperty("Divcode")]
            public object divcode { get; set; }

            [JsonProperty("RBSCode")]
            public object rbscode { get; set; }

            [JsonProperty("RBSName")]
            public object rbsname { get; set; }

            [JsonProperty("RBSDesc")]
            public object rbsdesc { get; set; }

            [JsonProperty("RMinValue")]
            public object rbsminvalue { get; set; }

            [JsonProperty("RMaxValue")]
            public object rbsmaxvalue { get; set; }

            [JsonProperty("RBSStatus")]
            public object rbsstatus { get; set; }

            [JsonProperty("Rbfdt")]
            public object rbfdt { get; set; }

            [JsonProperty("Rbtdt")]
            public object rbtdt { get; set; }

            [JsonProperty("Rdedt")]
            public object rdedt { get; set; }
			
			 [JsonProperty("hq")]
            public object hqid { get; set; }
        }

        public DataSet getAllRetailBusiness(string divcode)
        {
            DB_EReporting db = new DB_EReporting();
            DataSet ds = null;
            strQry = "exec getRetailBusiness_Details '" + divcode + "'";
            try
            {
                ds = db.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet getGiftID(string divcode)
        {
            DB_EReporting db = new DB_EReporting();
            DataSet ds = null;
            strQry = "exec getGiftID '" + divcode + "'";
            try
            {
                ds = db.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
        public string saveGift(SaveGiftSlab ss)
        {
            DB_EReporting db = new DB_EReporting();
            DataSet ds = null;
            string msg = string.Empty;
            strQry = "exec insertGiftDets '" + ss.divcode + "','" + ss.gcode + "','" + ss.gname + "','" + ss.gdesc + "','" + ss.gminvalue + "','" + ss.Gmaxvalue + "','" + ss.gfdt + "','" + ss.gtdt + "','" + ss.rbslab + "','" + ss.gtype + "','" + ss.hqid + "'";
            try
            {
                ds = db.Exec_DataSet(strQry);
                msg = "Success";
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            return msg;
        }

        public class SaveGiftSlab
        {
            [JsonProperty("Divcode")]
            public object divcode { get; set; }

            [JsonProperty("GCode")]
            public object gcode { get; set; }

            [JsonProperty("GName")]
            public object gname { get; set; }

            [JsonProperty("GDesc")]
            public object gdesc { get; set; }

            [JsonProperty("GMinValue")]
            public object gminvalue { get; set; }

            [JsonProperty("GMaxValue")]
            public object Gmaxvalue { get; set; }

            [JsonProperty("Gtype")]
            public object gtype { get; set; }

            [JsonProperty("FDT")]
            public object gfdt { get; set; }

            [JsonProperty("TDT")]
            public object gtdt { get; set; }

            [JsonProperty("RbSlab")]
            public object rbslab { get; set; }
			
			[JsonProperty("hqdtl")]
            public object hqid { get; set; }
        }
        public DataSet getAllGift(string divcode)
        {
            DB_EReporting db = new DB_EReporting();
            DataSet ds = null;
            strQry = "exec getGift_Details '" + divcode + "'";
            try
            {
                ds = db.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
        public DataSet getRetailBSlab(string scode, string divcode)
        {
            DB_EReporting db = new DB_EReporting();
            DataSet ds = null;
            strQry = "select * from Mas_Retail_Business_Slab where Division_Code=" + divcode + " and RetSlabID='" + scode + "'";
            try
            {
                ds = db.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
        public DataSet getGiftSlab(string scode, string divcode)
        {
            DB_EReporting db = new DB_EReporting();
            DataSet ds = null;
            strQry = "select * from Mas_Gift_Slab where Division_Code=" + divcode + " and GiftSlabID='" + scode + "'";
            try
            {
                ds = db.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
        public int Retail_DeActivate(string plcode, string stus)
        {
            int iReturn = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();
                strQry = "update Mas_Retail_Business_Slab set DeActDt=GETDATE(),ActiveFlag=" + stus + " where RetSlabID='" + plcode + "'";
                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }
        public int Gift_DeActivate(string plcode, string stus)
        {
            int iReturn = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();
                strQry = "update Mas_Gift_Slab set DeActDt=GETDATE(),ActiveFlag=" + stus + " where GiftSlabID='" + plcode + "'";
                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }
        public DataSet secondaryReturnHead(string div_code, string sfcode, string fdate, string tdate, string subdivcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = " exec getReturnSecondaryHead '" + sfcode + "','" + div_code + "','" + fdate + "','" + tdate + "'";

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
        public DataSet secondaryReturnDetail(string div_code, string sfcode, string fdate, string tdate, string subdivcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = " exec getReturnSecondaryDetails '" + sfcode + "','" + div_code + "','" + fdate + "','" + tdate + "'";

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

        public DataSet getproductwisecus(string Fdate, string prodcode, string Tdate, string sfcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "exec getprodwisecus '" + sfcode + "', '" + prodcode + "', '" + Fdate + "', '" + Tdate + "'";
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
        public DataSet primaryReturnHead(string div_code, string sfcode, string fdate, string tdate, string subdivcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = " exec getPriReturnSecondaryHead '" + sfcode + "','" + div_code + "','" + fdate + "','" + tdate + "'";

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
        public DataSet primarReturnDetail(string div_code, string sfcode, string fdate, string tdate, string subdivcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = " exec getPriReturnSecondaryDetails '" + sfcode + "','" + div_code + "','" + fdate + "','" + tdate + "'";

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

        public DataSet getproductwisestk(string Fdate, string prodcode, string Tdate, string sfcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "exec getprodwisestk '" + sfcode + "', '" + prodcode + "', '" + Fdate + "', '" + Tdate + "'";
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

        public DataSet getRetailerSlabSummary_Dets(string div_code, string sfcode, string fdate, string tdate, string subdivcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = " exec getRetailerSlabSummary_Dets '" + div_code + "','" + sfcode + "','" + fdate + "','" + tdate + "'";

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
        public DataTable GetPrimary_Sales_Value(string Div, string Mn, string Yr)
        {

            DB_EReporting db_ER = new DB_EReporting();

            DataTable dsSF = null;
            strQry = "EXEC getPrimarySaleValue '" + Div + "','" + Mn + "','" + Yr + "'";

            try
            {
                dsSF = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsSF;
        }
        public string saveGift_Products(SaveGiftProducts ss)
        {
            DB_EReporting db = new DB_EReporting();
            DataSet ds = null;
            string msg = string.Empty;
            strQry = "exec insertGiftProducts " + ss.gpscode + "," + ss.divcode + ",'" + ss.gpPname + "'," + ss.gpsstatus + ",'" + ss.gpfdt + "','" + ss.gptdt + "'";
            try
            {
                ds = db.Exec_DataSet(strQry);
                msg = "Success";
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            return msg;
        }
        public class SaveGiftProducts
        {
            [JsonProperty("Divcode")]
            public object divcode { get; set; }

            [JsonProperty("GPSCode")]
            public object gpscode { get; set; }

            [JsonProperty("GPSName")]
            public object gpPname { get; set; }

            [JsonProperty("GPSStatus")]
            public object gpsstatus { get; set; }

            [JsonProperty("GPfdt")]
            public object gpfdt { get; set; }

            [JsonProperty("GPtdt")]
            public object gptdt { get; set; }
        }
        public DataSet getAllGiftsProd(string divcode)
        {
            DB_EReporting db = new DB_EReporting();
            DataSet ds = null;
            strQry = "exec getGitProducts_Details '" + divcode + "'";
            try
            {
                ds = db.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet getGiftProdID(string divcode)
        {
            DB_EReporting db = new DB_EReporting();
            DataSet ds = null;
            strQry = "exec getGiftProductsid '" + divcode + "'";
            try
            {
                ds = db.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
		public DataSet giftslabhq(string divcode)
        {
            DB_EReporting db = new DB_EReporting();
            DataSet ds = null;
            strQry = "exec HQ_Details_active '" + divcode + "'";
            try
            {
                ds = db.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
        public DataSet getGiftProducts(string scode, string divcode)
        {
            DB_EReporting db = new DB_EReporting();
            DataSet ds = null;
            strQry = "select * from Mas_Gift_Products where Division_Code=" + divcode + " and sl_no='" + scode + "'";
            try
            {
                ds = db.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
		
        public DataSet GetSFMonthlyVisitDetails(string Sfcode, string divcode, string Fmn, string Fyr, string Tmn = "0", string Tyr = "0")
        {
            DB_EReporting db = new DB_EReporting();
            DataSet ds = null;
            strQry = "exec GetSFMonthlyVisitDetails '" + Sfcode + "','" + divcode + "',"+ Fmn + ","+ Fyr + "";
            try
            {
                ds = db.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
        public DataSet GetSFMonthlyCallDetails(string Sfcode, string divcode, string Fmn, string Fyr, string Tmn = "0", string Tyr = "0")
        {
            DB_EReporting db = new DB_EReporting();
            DataSet ds = null;
            strQry = "exec GetSFMonthlyCallDetails '" + Sfcode + "','" + divcode + "'," + Fmn + "," + Fyr + "";
            try
            {
                ds = db.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
        public DataSet GetSFMonthlySSaleDetails(string Sfcode, string divcode, string Fmn, string Fyr, string Tmn = "0", string Tyr = "0")
        {
            DB_EReporting db = new DB_EReporting();
            DataSet ds = null;
            strQry = "exec GetSFMonthlySSaleDetails '" + Sfcode + "','" + divcode + "'," + Fmn + "," + Fyr + "";
            try
            {
                ds = db.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
    }
}
