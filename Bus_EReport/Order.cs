using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DBase_EReport;
using System.Configuration;


namespace Bus_EReport
{
    public class Order
    {
        private string strQry = string.Empty;

        public DataSet Order_statewise_Trend_analysis_Distributor_descending(string div_code, string fromOrder_Date, string toOrder_Date, string subdivision, string state_cd)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "  select st.state_code,st.statename,st.shortname ,sum(t.Order_Value)as value, sum(t.net_weight_value) as net_weight_value from Trans_Order_Head  t  INNER JOIN Mas_Stockist S  " +
               " ON t.Stockist_code=S.Stockist_Code right outer join mas_state st on st.State_Code=s.State_Code " +
           "where  t.Order_Date between '" + fromOrder_Date + "' and '" + toOrder_Date + "' and   st.state_code in (" + state_cd + ") and st.State_Active_Flag=0 and s.Division_Code='" + div_code + "' and subdivision_code='" + subdivision + "' " +
           " group by  st.state_code,st.statename,st.shortname order by value desc ";

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
        public DataSet Order_statewise_Trend_analysis_totalvalue(string div_code, string fromOrder_Date, string toOrder_Date, string subdivision)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "   select sum(t.Order_Value)as value, sum(t.net_weight_value) as net_weight_value  from Trans_Order_Head  t INNER JOIN Mas_Stockist S " +
                "   ON t.Stockist_code=S.Stockist_Code right outer join mas_state st on st.State_Code=s.State_Code " +
          " where  t.Order_Date between '" + fromOrder_Date + "' and '" + toOrder_Date + "'  and st.State_Active_Flag=0 and s.Division_Code='" + div_code + "' and subdivision_code='" + subdivision + "' " +
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


        public DataSet Order_statewise_Trend_analysis_value_singleProduct(string product_code, string div_code, int fmonth, int fyear, string subdivision, string statecode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "   select sum(d.value)as value,sum(d.net_weight * d.quantity) as net_weight_value  from Trans_Order_Head  t inner join  Trans_Order_Details d on t.Trans_Sl_No=d.Trans_Sl_No  INNER JOIN Mas_Stockist S " +
                "   ON t.Stockist_code=S.Stockist_Code right outer join mas_state st on st.State_Code=s.State_Code " +
          " where   month(Order_Date)='" + fmonth + "' and YEAR(Order_Date)='" + fyear + "'  and d.Product_Code='" + product_code + "' and st.state_code in (" + statecode + ") and st.State_Active_Flag=0 and s.Division_Code='" + div_code + "' and subdivision_code='" + subdivision + "' " +
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
        public DataSet Order_statewise_Trend_analysis_value_singleProduct_total(string div_code, int fmonth, int fyear, string subdivision, string statecode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "   select sum(d.value)as value  from Trans_Order_Head  t inner join  Trans_Order_Details d on t.Trans_Sl_No=d.Trans_Sl_No INNER JOIN Mas_Stockist S " +
                "   ON t.Stockist_code=S.Stockist_Code right outer join mas_state st on st.State_Code=s.State_Code " +
          " where   month(Order_Date)='" + fmonth + "' and YEAR(Order_Date)='" + fyear + "'  and st.State_Active_Flag=0  and st.state_code in (" + statecode + ")and s.Division_Code='" + div_code + "' and subdivision_code='" + subdivision + "' " +
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
        public DataSet Order_areawise_Trend_analysis_singleproduct(string product_code, string div_code, int fmonth, int fyear, string subdivision, string statecode, string areacode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "select sum(d.value)as value,sum(d.net_weight * d.quantity) as net_weight_value  from Trans_Order_Head  t   inner join  Trans_Order_Details d on t.Trans_Sl_No=d.Trans_Sl_No INNER JOIN Mas_Stockist S   " +
     " ON t.Stockist_code=S.Stockist_Code inner join Mas_Territory Tt on Tt.Territory_code=S.Territory_Code INNER JOIN Mas_Zone Z ON Z.Zone_code=Tt.Zone_code  right outer join Mas_Area a  ON a.Area_code=Z.Area_code  " +
     "where  month(Order_Date)='" + fmonth + "' and YEAR(Order_Date)='" + fyear + "'  and d.Product_Code='" + product_code + "' and  a.Area_code='" + areacode + "'  and a.Area_Active_Flag=0 and s.Division_Code='" + div_code + "'  and subdivision_code='" + subdivision + "' and a.State_Code='" + statecode + "' " +
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
        public DataSet Order_areawise_Trend_analysis_singleproduct_total(string div_code, int fmonth, int fyear, string subdivision, string statecode, string areacode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "select sum(d.value)as value  from Trans_Order_Head  t inner join  Trans_Order_Details d on t.Trans_Sl_No=d.Trans_Sl_No INNER JOIN Mas_Stockist S   " +
     " ON t.Stockist_code=S.Stockist_Code inner join Mas_Territory Tt on Tt.Territory_code=S.Territory_Code INNER JOIN Mas_Zone Z ON Z.Zone_code=Tt.Zone_code  right outer join Mas_Area a  ON a.Area_code=Z.Area_code  " +
     "where  month(Order_Date)='" + fmonth + "' and YEAR(Order_Date)='" + fyear + "'   and  a.Area_code='" + areacode + "'  and a.Area_Active_Flag=0 and s.Division_Code='" + div_code + "'  and subdivision_code='" + subdivision + "' and a.State_Code='" + statecode + "' " +
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
        public DataSet Order_Zonewise_Trend_analysis_totalvalue_singleprdt(string product_code, string div_code, int fmonth, int fyear, string subdivision, string statecode, string area_code, string zonecode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = " select  sum(d.value)as value,sum(d.net_weight * d.quantity) as net_weight_value  from Trans_Order_Head  t   inner join  Trans_Order_Details d on t.Trans_Sl_No=d.Trans_Sl_No INNER JOIN Mas_Stockist S " +
            "  ON t.Stockist_code=S.Stockist_Code inner join Mas_Territory Tt on Tt.Territory_code=S.Territory_Code right outer JOIN Mas_Zone Z ON Z.Zone_code=Tt.Zone_code  inner join Mas_Area a  ON a.Area_code=Z.Area_code " +
          " where   month(Order_Date)='" + fmonth + "' and YEAR(Order_Date)='" + fyear + "'  and d.Product_Code='" + product_code + "' and  s.Division_Code='" + div_code + "'  and a.State_Code='" + statecode + "'    and a.Area_Active_Flag=0 and z.Zone_Active_Flag=0 and s.Division_Code='" + div_code + "'  and subdivision_code='" + subdivision + "' and  a.Area_code='" + area_code + "'and z.Zone_code='" + zonecode + "' " +
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
        public DataSet Order_Zonewise_Trend_analysis_totalvalue_singleprdt_total(string div_code, int fmonth, int fyear, string subdivision, string statecode, string area_code, string zonecode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = " select  sum(d.value)as value  from Trans_Order_Head  t    inner join  Trans_Order_Details d on t.Trans_Sl_No=d.Trans_Sl_No INNER JOIN Mas_Stockist S " +
            "  ON t.Stockist_code=S.Stockist_Code inner join Mas_Territory Tt on Tt.Territory_code=S.Territory_Code right outer JOIN Mas_Zone Z ON Z.Zone_code=Tt.Zone_code  inner join Mas_Area a  ON a.Area_code=Z.Area_code " +
          " where   month(Order_Date)='" + fmonth + "' and YEAR(Order_Date)='" + fyear + "'  and   s.Division_Code='" + div_code + "'  and a.State_Code='" + statecode + "'    and a.Area_Active_Flag=0 and z.Zone_Active_Flag=0 and s.Division_Code='" + div_code + "'  and subdivision_code='" + subdivision + "' and  a.Area_code='" + area_code + "'and z.Zone_code='" + zonecode + "' " +
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
        public DataSet Order_Territorywise_Trend_analysis_totalvalue_singleprt(string product_code, string div_code, int fmonth, int fyear, string subdivision, string statecode, string area_code, string zonecode, string territory_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = " select  sum(d.value)as value,sum(d.net_weight * d.quantity) as net_weight_value  from Trans_Order_Head  t  inner join  Trans_Order_Details d on t.Trans_Sl_No=d.Trans_Sl_No INNER JOIN Mas_Stockist S  " +
             " ON t.Stockist_code=S.Stockist_Code RIGHT OUTER join Mas_Territory Tt on Tt.Territory_code=S.Territory_Code INNER JOIN Mas_Zone Z ON Z.Zone_code=Tt.Zone_code  inner join Mas_Area a  ON a.Area_code=Z.Area_code  " +
          " where   month(Order_Date)='" + fmonth + "' and YEAR(Order_Date)='" + fyear + "' and d.Product_Code='" + product_code + "'  and a.Area_Active_Flag=0 and z.Zone_Active_Flag=0 and s.Division_Code='" + div_code + "'  and subdivision_code='" + subdivision + "' and a.State_Code='" + statecode + "' and a.Area_code='" + area_code + "' AND TT.Zone_code='" + zonecode + "' and s.Territory_Code='" + territory_code + "' " +
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

        public DataSet Order_Territorywise_Trend_analysis_totalvalue_singleprt_total(string div_code, int fmonth, int fyear, string subdivision, string statecode, string area_code, string zonecode, string territory_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = " select  sum(d.value)as value  from Trans_Order_Head  t inner join  Trans_Order_Details d on t.Trans_Sl_No=d.Trans_Sl_No INNER JOIN Mas_Stockist S  " +
             " ON t.Stockist_code=S.Stockist_Code RIGHT OUTER join Mas_Territory Tt on Tt.Territory_code=S.Territory_Code INNER JOIN Mas_Zone Z ON Z.Zone_code=Tt.Zone_code  inner join Mas_Area a  ON a.Area_code=Z.Area_code  " +
          " where   month(Order_Date)='" + fmonth + "' and YEAR(Order_Date)='" + fyear + "'  and a.Area_Active_Flag=0 and z.Zone_Active_Flag=0 and s.Division_Code='" + div_code + "'  and subdivision_code='" + subdivision + "' and a.State_Code='" + statecode + "' and a.Area_code='" + area_code + "' AND TT.Zone_code='" + zonecode + "' and s.Territory_Code='" + territory_code + "' " +
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
        public DataSet Order_areawise_Trend_analysis_Distributor_descending(string div_code, string fromOrder_Date, string toOrder_Date, string subdivision, string state_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = " select a.Area_code,a.Area_name,sum(t.Order_Value)as value, sum(t.net_weight_value) as net_weight_value  from Trans_Order_Head  t INNER JOIN Mas_Stockist S  " +
             "   ON t.Stockist_code=S.Stockist_Code inner join Mas_Territory Tt on Tt.Territory_code=S.Territory_Code INNER JOIN Mas_Zone Z ON Z.Zone_code=Tt.Zone_code  right outer join Mas_Area a  ON a.Area_code=Z.Area_code  " +
            " where  t.Order_Date between '" + fromOrder_Date + "' and '" + toOrder_Date + "'  and a.Area_Active_Flag=0 and s.Division_Code='" + div_code + "'  and subdivision_code='" + subdivision + "' and a.State_Code='" + state_code + "' " +
             " group by  a.Area_code,a.Area_name order by value desc  ";

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

public int updatepriorder( string flag, string orderno, string sfcode,string stockistcode,string supersc,string Order_Value,string div,string Order_date )
        {
            int iReturn = -1;
            DB_EReporting db_ER = new DB_EReporting();        

            try
            {
                strQry = "EXEC updatepriorder '" + flag + "','" + orderno + "','" + sfcode + "', '" + stockistcode + "','" + supersc + "','" + Order_Value + "','"+div+"','"+ Order_date + "'";

                iReturn = db_ER.ExecQry(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }
 public int updatepriorderdetail(string Flag,string orderno,string order_value,string product_code,string product_name,string cqty,string pqty,string rate,string Value,string casecnfqty,string pcscnfqty, string superstockist, string Trans_POrd_No, string neworderno,string div,string Order_date,string sf_code)
        {
            int iReturn = -1;
            DB_EReporting db_ER = new DB_EReporting();
            try
            {
                strQry= "Exec Priorder_editdetail '"+ Flag + "','"+ orderno + "','"+ product_code + "','"+ product_name + "','"+ cqty + "','"+ pqty + "','"+ rate + "','"+ Value + "','"+ casecnfqty + "','"+ pcscnfqty + "','" + superstockist + "','" + div + "','"+ order_value + "','"+ Order_date + "','" + Trans_POrd_No + "','"+ neworderno + "','"+ sf_code+"'";
                iReturn = db_ER.ExecQry(strQry);
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return iReturn;
           }

  public DataSet get_secStockist(string div_code,string sfcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;
            strQry = "Exec get_secStockist '" + div_code + "','"+ sfcode + "'";

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
 public int updatesecorder(string flag, string div, string retailercode, string transslno)
        {
            int iReturn = -1;
            DB_EReporting db_ER = new DB_EReporting();

            try
            {
                strQry = "EXEC updatesecorder '" + flag + "','" + div + "','" + retailercode + "', '" + transslno + "'";

                iReturn = db_ER.ExecQry(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }
 public DataSet schemedetail(string div_code, string productcode, string stockistcode, string orderdate,string qnty)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "EXEC schemedetail '" + div_code + "','" + productcode + "','" + stockistcode + "','" + orderdate + "','"+ qnty + "'";


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
   public int updateSecorderdetail(string Flag, string Transslno, string Product_Code, string Qty, string Qtycnf, string free, string rate, string freecnf, string divcode,string Trans_Order_No, string neworderno, string retailercode, string txtdiscount, string txtdisprice)
        {
            int iReturn = -1;
            DB_EReporting db_ER = new DB_EReporting();
            try
            {
                strQry = "Exec Secorder_editdetail '" + Flag + "','" + Transslno + "','" + Product_Code + "','" + Qty + "','" + Qtycnf + "','" + free + "','" + rate + "','" + freecnf + "','" + divcode + "','" + Trans_Order_No + "','" + neworderno + "','" + retailercode + "','" + txtdiscount + "','" + txtdisprice + "'";
                iReturn = db_ER.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }
        public DataSet Order_areawise_Trend_analysis_totalvalue(string div_code, string fromOrder_Date, string toOrder_Date, string subdivision, string statecode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "select sum(t.Order_Value)as value  from Trans_Order_Head  t INNER JOIN Mas_Stockist S   " +
   " ON t.Stockist_code=S.Stockist_Code inner join Mas_Territory Tt on Tt.Territory_code=S.Territory_Code INNER JOIN Mas_Zone Z ON Z.Zone_code=Tt.Zone_code  right outer join Mas_Area a  ON a.Area_code=Z.Area_code  " +
"where  t.Order_Date between '" + fromOrder_Date + "' and '" + toOrder_Date + "'   and a.Area_Active_Flag=0 and s.Division_Code='" + div_code + "'  and subdivision_code='" + subdivision + "' and a.State_Code='" + statecode + "' " +
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
        public DataSet Order_Zonewise_Trend_analysis_Distributor_descending(string div_code, string fromOrder_Date, string toOrder_Date, string subdivision, string state_code, string area_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = " select z.Zone_code,z.Zone_name, sum(t.Order_Value)as value ,sum(t.net_weight_value) as net_weight_value  from Trans_Order_Head  t INNER JOIN Mas_Stockist S  " +
             " ON t.Stockist_code=S.Stockist_Code inner join Mas_Territory Tt on Tt.Territory_code=S.Territory_Code right outer JOIN Mas_Zone Z ON Z.Zone_code=Tt.Zone_code  inner join Mas_Area a  ON a.Area_code=Z.Area_code  " +
           " where  t.Order_Date between '" + fromOrder_Date + "' and '" + toOrder_Date + "'   and a.Area_Active_Flag=0 and z.Zone_Active_Flag=0 and s.Division_Code='" + div_code + "'  and subdivision_code='" + subdivision + "' and a.State_Code='" + state_code + "' and a.Area_code='" + area_code + "' " +
           " group by  z.Zone_code,z.Zone_name order by value desc";

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

        public DataSet Order_Zonewise_Trend_analysis_totalvalue(string div_code, string fromOrder_Date, string toOrder_Date, string subdivision, string statecode, string area_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = " select  sum(t.Order_Value)as value  from Trans_Order_Head  t INNER JOIN Mas_Stockist S " +
            "  ON t.Stockist_code=S.Stockist_Code inner join Mas_Territory Tt on Tt.Territory_code=S.Territory_Code right outer JOIN Mas_Zone Z ON Z.Zone_code=Tt.Zone_code  inner join Mas_Area a  ON a.Area_code=Z.Area_code " +
          " where  t.Order_Date between '" + fromOrder_Date + "' and '" + toOrder_Date + "'   and a.Area_Active_Flag=0 and z.Zone_Active_Flag=0 and s.Division_Code='" + div_code + "'  and subdivision_code='" + subdivision + "' and a.State_Code='" + statecode + "' and a.Area_code='" + area_code + "' " +
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
        public DataSet Order_Territorywise_Trend_analysis_Distributor_descending(string div_code, string fromOrder_Date, string toOrder_Date, string subdivision, string state_code, string area_code, string zonecode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = " select Tt.Territory_code,Tt.Territory_Name, sum(t.Order_Value)as value,sum(t.net_weight_value) as net_weight_value  from Trans_Order_Head  t INNER JOIN Mas_Stockist S  " +
            "  ON t.Stockist_code=S.Stockist_Code RIGHT OUTER join Mas_Territory Tt on Tt.Territory_code=S.Territory_Code INNER JOIN Mas_Zone Z ON Z.Zone_code=Tt.Zone_code  inner join Mas_Area a  ON a.Area_code=Z.Area_code " +
         "where  t.Order_Date between '" + fromOrder_Date + "' and '" + toOrder_Date + "'   and a.Area_Active_Flag=0 and z.Zone_Active_Flag=0 and s.Division_Code='" + div_code + "'  and subdivision_code='" + subdivision + "' and a.State_Code='" + state_code + "' and a.Area_code='" + area_code + "' AND TT.Zone_code='" + zonecode + "' " +
         " group by  Tt.Territory_code,Tt.Territory_Name order by value desc";

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

        public DataSet Order_Territorywise_Trend_analysis_totalvalue(string div_code, string fromOrder_Date, string toOrder_Date, string subdivision, string statecode, string area_code, string zonecode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = " select  sum(t.Order_Value)as value  from Trans_Order_Head  t INNER JOIN Mas_Stockist S  " +
             " ON t.Stockist_code=S.Stockist_Code RIGHT OUTER join Mas_Territory Tt on Tt.Territory_code=S.Territory_Code INNER JOIN Mas_Zone Z ON Z.Zone_code=Tt.Zone_code  inner join Mas_Area a  ON a.Area_code=Z.Area_code  " +
          " where  t.Order_Date between '" + fromOrder_Date + "' and '" + toOrder_Date + "'   and a.Area_Active_Flag=0 and z.Zone_Active_Flag=0 and s.Division_Code='" + div_code + "'  and subdivision_code='" + subdivision + "' and a.State_Code='" + statecode + "' and a.Area_code='" + area_code + "' AND TT.Zone_code='" + zonecode + "' " +
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
        public DataSet View_Route_deviation_plans(string div_code, string sfcode, string Date)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = " SELECT  Route = STUFF  " +
            " ((SELECT   ',' +  (t.SDP_Name +'('+ stockist_name +')') " +
            " FROM DCRMain_Trans  As T2  inner join DCRDetail_Lst_Trans t on t.Trans_SlNo=T2.Trans_SlNo " +
         "  WHERE  CONVERT(VARCHAR(25), T2.Activity_Date, 126) like '" + Date + "%' and T2.Sf_Code='" + sfcode + "' and T2.Division_code='" + div_code + "'  " +
        "  group BY t.SDP_Name,t.SDP,T2.Sf_Code ,t.stockist_name " +
        " FOR XML PATH (''), TYPE " +
        "   ).value('.', 'varchar(max)') " +
        "  , 1, 1, '') " +
        " FROM DCRMain_Trans As T1 inner join DCRDetail_Lst_Trans t on t.Trans_SlNo=T1.Trans_SlNo  WHERE CONVERT(VARCHAR(25), T1.Activity_Date, 126) like '" + Date + "%' and T1.Sf_Code='" + sfcode + "' and  T1.Division_code='" + div_code + "' " +
         " GROUP BY SDP_Name,stockist_name";

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
        public DataSet View_Retailercount(string div_code, string sfcode, string Date)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = " select count( DISTINCT t.Trans_Detail_Info_Code) as Retailers  from DCRMain_Trans h " +
             " inner join DCRDetail_Lst_Trans t on t.Trans_SlNo=h.Trans_SlNo where CONVERT(VARCHAR(25),h.Activity_Date, 126) like '" + Date + "%' AND h.Sf_Code='" + sfcode + "' and h.Division_code='" + div_code + "'";

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

        public DataSet lost_distributor_feildforcewise(string div_code, string sfcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;
            strQry = "Exec lost_dist_userlist '" + sfcode + "','" + div_code + "'";

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
		 public DataSet get_superstockist(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;
            strQry = "Exec get_superstockist '" + div_code + "'";

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


        public DataSet View_Non_Retailercount(string div_code, string sfcode, string Date)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet territory = null;
            string territory_code = string.Empty;
            DataSet dsAdmin = null;
            strQry = "	  SELECT stuff((  " +
    "  SELECT ', ' + cast(QUOTENAME(T.SDP,'''') as varchar(max))  " +
   "   FROM DCRMain_Trans As T1 	 inner join DCRDetail_Lst_Trans t on t.Trans_SlNo=T1.Trans_SlNo   " +
  "  WHERE CONVERT(VARCHAR(25), Activity_Date, 126) like '" + Date + "%' and T1.Sf_Code='" + sfcode + "' and  T1.Division_code='" + div_code + "' FOR XML PATH('')), 1, 2, '')  ";
            territory = db_ER.Exec_DataSet(strQry);
            if (territory.Tables[0].Rows.Count > 0)
            {
                territory_code = territory.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
            }
            if (territory_code == "")
            {
            }
            else
            {
                strQry = " select count(ListedDrcode) from MAS_listeddr where territory_code in (" + territory_code + ") and Division_code='" + div_code + "'AND ListedDr_Active_Flag=0 and  ListedDrCode NOT IN  (select distinct t.Trans_Detail_Info_Code   from DCRMain_Trans h " +
           " inner join DCRDetail_Lst_Trans t on t.Trans_SlNo=h.Trans_SlNo where CONVERT(VARCHAR(25), h.Activity_Date, 126) like '" + Date + "%' AND h.Sf_Code='" + sfcode + "' and h.Division_code='" + div_code + "')";
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
        public DataSet View_Non_Retailer_view(string div_code, string sfcode, string Date)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet territory = null;
            string territory_code = string.Empty;
            DataSet dsAdmin = null;
            strQry = "	  SELECT stuff((  " +
    "  SELECT ', ' + cast(QUOTENAME(T.SDP,'''') as varchar(max))  " +
   "   FROM DCRMain_Trans As T1 	 inner join DCRDetail_Lst_Trans t on t.Trans_SlNo=T1.Trans_SlNo   " +
  "  WHERE CONVERT(VARCHAR(25), Activity_Date, 126) like '" + Date + "%' and T1.Sf_Code='" + sfcode + "' and  T1.Division_code='" + div_code + "' FOR XML PATH('')), 1, 2, '')  ";
            territory = db_ER.Exec_DataSet(strQry);
            if (territory.Tables[0].Rows.Count > 0)
            {
                territory_code = territory.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
            }
            if (territory_code == "")
            {
            }
            else
            {
                strQry = " select   ListedDr_Name from MAS_listeddr where territory_code in (" + territory_code + ") and Division_code='" + div_code + "'AND ListedDr_Active_Flag=0 and  ListedDrCode NOT IN  (select  distinct t.Trans_Detail_Info_Code   from DCRMain_Trans h " +
           " inner join DCRDetail_Lst_Trans t on t.Trans_SlNo=h.Trans_SlNo where CONVERT(VARCHAR(25), h.Activity_Date, 126) like '" + Date + "%' AND h.Sf_Code='" + sfcode + "' and h.Division_code='" + div_code + "')";
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

        public DataSet View_Retailerdetailview(string div_code, string sfcode, string Date)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet territory = null;
            string territory_code = string.Empty;
            DataSet dsAdmin = null;

            strQry = "select DISTINCT t.Trans_Detail_Info_Code, t.Trans_Detail_Name,t.SDP_Name  from DCRMain_Trans h " +
                    "inner join DCRDetail_Lst_Trans t on t.Trans_SlNo=h.Trans_SlNo " +
                    "inner join MAS_listeddr a on a.ListedDrCode=t.Trans_Detail_Info_Code and ListedDr_Active_Flag=0 " +
                    "where charindex(','+cast('" + sfcode + "'  as varchar)+',',','+a.Sf_Code+',')>0 and   CONVERT(VARCHAR(25),ListedDr_Created_Date, 126) <=  '" + Date + "' and " +
                    " CONVERT(VARCHAR(25),h.Activity_Date, 126) like '" + Date + "'  AND h.Sf_Code='" + sfcode + "' and h.Division_code='" + div_code + "'";

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

        public DataSet View_Retailerdetailview_day(string div_code, string sfcode, string Date)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet territory = null;
            string territory_code = string.Empty;
            DataSet dsAdmin = null;

            strQry = "select DISTINCT t.Trans_Detail_Info_Code, t.Trans_Detail_Name,t.SDP_Name  from DCRMain_Trans h " +
                    "inner join DCRDetail_Lst_Trans t on t.Trans_SlNo=h.Trans_SlNo " +
                    "inner join MAS_listeddr a on a.ListedDrCode=t.Trans_Detail_Info_Code and ListedDr_Active_Flag=0 " +
                    "where charindex(','+cast('" + sfcode + "'  as varchar)+',',','+a.Sf_Code+',')>0 and  CONVERT(VARCHAR(25),ListedDr_Created_Date, 126) <= '" + Date + "%' and " +
                    "CONVERT(VARCHAR(25),h.Activity_Date, 126) like '" + Date + "%'  AND h.Sf_Code='" + sfcode + "' and h.Division_code='" + div_code + "'";

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

        public DataSet Total_Retailercount(string div_code, string sfcode, string Date)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "SELECT COUNT(ListedDrCode) as dr_count FROM Mas_ListedDr a  WHERE  a.Sf_Code = '" + sfcode + "' and a.Division_Code ='" + div_code + "' and a.ListedDr_Active_Flag = 0";

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

        public DataSet View_DCR_Order_Val(string div_code, string sfcode, string Date)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "select sum(a.Quantity + a.net_weight)vol from Trans_Order_Details a inner join Trans_Order_Head b on a.Trans_Sl_No=b.Trans_Sl_No where b.Sf_Code='" + sfcode + "' and CONVERT(VARCHAR(25), b.Order_Date, 126) like '" + Date + "%' ";

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
        public DataSet View_total_Retailercount(string div_code, string sfcode, string Date)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet territory = null;
            string territory_code = string.Empty;
            DataSet dsAdmin = null;
            strQry = " SELECT stuff((  " +
               "  SELECT ',' + cast(QUOTENAME(T.SDP,'''') as varchar(max))  " +
              "   FROM DCRMain_Trans As T1 	 inner join DCRDetail_Lst_Trans t on t.Trans_SlNo=T1.Trans_SlNo   " +
             "  WHERE CONVERT(VARCHAR(25), Activity_Date, 126) like '" + Date + "%' and T1.Sf_Code='" + sfcode + "' and  T1.Division_code='" + div_code + "' FOR XML PATH('')), 1, 2, '')  ";

            territory = db_ER.Exec_DataSet(strQry);

            if (territory.Tables[0].Rows.Count > 0)
            {
                territory_code = territory.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
            }
            if (territory_code == "")
            {
            }
            else
            {
                strQry = "select count(ListedDrCode) from MAS_listeddr where territory_code in  ('" + territory_code + ") and  ListedDr_Active_Flag=0 and Division_code='" + div_code + "'";
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


        public DataSet view_stockist_feildforcewise(string div_code, string sfcode, string subdivision)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "EXEC GET_DISTRIBUTOR_SAM '" + div_code + "','" + subdivision + "','" + sfcode + "'";


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

        public DataTable view_stockist_Pri(string div_code, string sfcode, string subdivision)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dsAdmin = null;

            strQry = "EXEC GET_DISTRIBUTOR_PRI '" + div_code + "','" + subdivision + "','" + sfcode + "'";


            try
            {
                dsAdmin = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsAdmin;
        }
        public DataSet Get_Order_Count(string Div_Code, string fyear, string fmonth)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;
            strQry = "select SF_Code,Count(Cust_Code) ec,sum((case when cnt>1 then 1 else 0 end)) rc  from( select SF_Code,Cust_Code,count(Cust_Code) cnt from OrderCustomerlist where Mnth='" + fmonth + "' and Yr='" + fyear + "' and Division_Code='" + Div_Code + "' group by SF_Code,Cust_Code) as stb group by SF_Code	";

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
        public DataSet Get_Total_Vol(string Div_Code, string fyear, string fmonth)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;
            // strQry = "select Sf_Code,count(Cust_Code)ec,sum(net_weight_value)net from (select a.Sf_Code,a.Cust_Code,a.net_weight_value from Trans_Order_Head a inner join Mas_ListedDr b on b.ListedDrCode=a.Cust_Code where month(a.Order_Date)='"+fmonth+"' and year(a.Order_Date)='"+fyear+"' and b.Division_Code='"+Div_Code+"' group by a.Sf_Code,a.Cust_Code,a.net_weight_value)as std group by Sf_Code";
            strQry = "select a.Sf_Code,SUM(d.Quantity) QTY from Trans_Order_Head a inner join trans_order_details D on  a.trans_sl_no = d.trans_sl_no INNER JOIN MAS_SALESFORCE MS ON MS.SF_CODE = A.Sf_Code where month(a.Order_Date)='" + fmonth + "' and year(a.Order_Date)='" + fyear + "' and MS.Division_Code='" + Div_Code + ",' group by  a.Sf_Code";

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

        public DataSet Get_Total_Val(string Div_Code, string fyear, string fmonth)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;
            //strQry = "select Sf_Code,count(Cust_Code)ec,sum(Order_Value)val from (select a.Sf_Code,a.Cust_Code,a.Order_Value from Trans_Order_Head a inner join Mas_ListedDr b on b.ListedDrCode=a.Cust_Code where month(a.Order_Date)='" + fmonth + "' and year(a.Order_Date)='" + fyear + "' and b.Division_Code='" + Div_Code + "' group by a.Sf_Code,a.Cust_Code,a.Order_Value)as std group by Sf_Code";
            //			strQry = "select Sf_Code,count(Cust_Code)ec,sum(Order_Value)val from (select a.Sf_Code,a.Cust_Code,a.Order_Value from Trans_Order_Head a inner join Mas_ListedDr b on b.ListedDrCode=a.Cust_Code where month(a.Order_Date)='" + fmonth + "' and year(a.Order_Date)='" + fyear + "' and b.Division_Code='" + Div_Code + "' group by a.Sf_Code,a.Cust_Code,a.Order_Value)as std group by Sf_Code";
            strQry = " select Sf_Code,count(Cust_Code)ec,round(sum(Value),2) val, isnull(sum(Quantity),2) qty  from ( select a.Sf_Code,a.Cust_Code,sum(d.Value) Value,sum(d.Quantity) Quantity,cast(convert(varchar,Order_date,101)as datetime)Order_date from Trans_Order_Head a  inner join trans_order_details D on  a.trans_sl_no = d.trans_sl_no  inner join Mas_ListedDr b on b.ListedDrCode=a.Cust_Code  where month(a.Order_Date)='" + fmonth + "' and year(a.Order_Date)='" + fyear + "' and b.Division_Code='" + Div_Code + "' group by a.Sf_Code,a.Cust_Code,cast(convert(varchar,Order_date,101)as datetime))as std group by Sf_Code";
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

        public DataSet Get_TVolume(string Div_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;
            strQry = "select TVALUES from  Trans_Target_Settings a inner join Mas_Target_Settings b on b.ID=a.ID where a.DIVISION_CODE='24' and  b.ID='1'";

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
        public DataSet Get_TVALUES(string Div_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;
            strQry = "select TVALUES from  Trans_Target_Settings a inner join Mas_Target_Settings b on b.ID=a.ID where a.DIVISION_CODE='24' and  b.ID='2'";

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

        public DataSet get_target_order(string Div_Code, string Sub_Div_Code, string year, string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;
            strQry = "Exec Target_order '" + Div_Code + "', '" + Sub_Div_Code + "', '" + sf_code + "', '" + year + "'";

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

        public DataSet get_order_valuecount(string Div_Code, string year)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;
            strQry = "select sf_code,	count( distinct trans_sl_no) cnt from trans_order_head where Div_ID = '" + Div_Code + "' and year(Order_Date)= '" + year + "'  group by sf_code";

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

 public DataSet get_order_valueSTKcnt(string year, string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;
            strQry = "select jj.Sf_Code  Sf_Code1 ,  m.Sf_Name ,(ms.Stockist_Code)Stockist_Code1 , ms.Stockist_Name, count(distinct jj.Cust_Code) cnt" +
                " from Trans_Order_Head jj inner join Mas_stockist ms on ms.Stockist_Code = jj.Stockist_Code inner join Mas_Salesforce m on m.Sf_Code = jj.Sf_Code" +
                "  where year(jj.Order_Date)= '"+ year + "' and jj.DIV_ID = '"+ divcode + "'  group by ms.Stockist_Code,  ms.Stockist_Name ,m.Sf_Name,jj.Sf_Code";

                
                
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
public DataSet get_order_value(string Div_Code,  string year)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;
            strQry = "select Sf_Code, name,[1] jan,[2] feb,[3] mar,[4] app,[5] may,[6] june,[7] july,[8] aguest,[9] sep,[10] oct,[11] nav,[12] dece " +
                "from (select ms.Sf_Code, ms.Sf_Name as name ,cast(isnull(sum(jj.Order_Value), 0) as decimal) as order_val,year(Order_Date) as yar," +
                "month(Order_Date) as mont from Trans_Order_Head jj inner  join Mas_Salesforce ms on jj.Sf_Code = ms.Sf_Code where jj.Div_ID = '"+ Div_Code + "' " +
                "and year(jj.Order_Date)= '"+ year+"'  group by ms.Sf_Name,ms.Sf_Code ,year(jj.Order_Date),month(jj.Order_Date),ms.Sf_Code) as tab1 " +
                "pivot(sum(order_val)  for mont in ([1],[2],[3],[4],[5],[6],[7],[8],[9],[10],[11],[12]) )as t";

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
        public DataSet get_order_valueSTK( string year,string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;
             strQry = "Exec get_order_valueSTK '" + year + "', '" + divcode + "'";
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
           public DataSet get_dis_order_value(string Div_Code, string year, string SF_Code)
        {
           
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;
            strQry = "Exec sp_dis_ordervalue '" + Div_Code + "', '" + year + "','" + SF_Code + "'";

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
        public DataSet get_dis_orderRUT_value(string Div_Code, string year, string Stockist_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;
            strQry = "select Territory_Code,Territory_Name,[1] jan,[2] feb,[3] mar,[4] app,[5] may,[6] june,[7] july,[8] aguest,[9] sep,[10] oct,[11] nav,[12] dece" +
                " from(select cast(isnull(sum(jj.Order_Value), 0) as decimal) as order_val,year(Order_Date) as yar,mtc.Territory_Name,mtc.Territory_Code," +
                " month(Order_Date) as mont from Trans_Order_Head jj inner join Mas_Territory_Creation mtc on jj.Route = mtc.Territory_Code where " +
                "jj.Div_ID = '"+ Div_Code + "' and year(jj.Order_Date)= '"+ year + "'  and jj.Stockist_Code= '"+ Stockist_Code + "' group by" +
                " mtc.Territory_Name , year(jj.Order_Date),mtc.Territory_Code, month(jj.Order_Date)) as tab1" +
                " pivot(" +
                "sum(order_val)  for mont in ([1],[2],[3],[4],[5],[6],[7],[8],[9],[10],[11],[12]) )as t";

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
        public DataSet get_dis_orderCUS_value(string Div_Code, string year, string Stockist_Code, string Territory_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;
            strQry = "select ListedDr_Name,ListedDrCode,[1] jan,[2] feb,[3] mar,[4] app,[5] may,[6] june,[7] july,[8] aguest,[9] sep,[10] oct,[11] nav,[12] dece" +
                " from(select cast(isnull(sum(jj.Order_Value), 0) as decimal) as order_val,year(Order_Date) as yar, mtc.ListedDr_Name,mtc.ListedDrCode," +
                " month(Order_Date) as mont from Trans_Order_Head jj inner join Mas_ListedDr mtc on jj.Cust_code = mtc.ListedDrCode" +
                " where jj.Div_ID = '"+ Div_Code + "' and year(jj.Order_Date)= '"+ year + "'  and jj.Stockist_Code= '"+ Stockist_Code + "' and jj.Route= '"+ Territory_Code + "' group by" +
                " mtc.ListedDrCode , mtc.ListedDr_Name, year(jj.Order_Date), month(jj.Order_Date)) as tab1" +
                "   pivot(" +
                " sum(order_val)  for mont in ([1],[2],[3],[4],[5],[6],[7],[8],[9],[10],[11],[12]) )as t";

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
 public DataSet get_product_details(string SF ,string Div_Code, string Mnth,string Year)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;
            /*strQry = "select Product_Code,Product_Name,CONCAT(Cb_Qty,'.',pieces) as qty,ms.Stockist_code,ms.Stockist_name,t.Last_Updation_Date," +
                "ROW_NUMBER() over(partition by ms.Stockist_code order by t.Last_Updation_Date desc) rw from " +
                "(select Stockist_code, Last_Updation_Date, ROW_NUMBER() over(partition by Stockist_code order by Last_Updation_Date desc) rw " +
                "from Trans_Current_Stock_details where Division_Code = '" + Div_Code + "') t" +
                "  inner join Trans_Current_Stock_details tc on " +
                " t.Stockist_code = tc.Stockist_code and t.Last_Updation_Date = tc.Last_Updation_Date and rw = 1" +
                "  inner join  Mas_Stockist ms on ms.Stockist_Code = tc.Stockist_code where ms.Division_Code = '" + Div_Code + "'";*/
            strQry = "exec LastUpdtStockDet '" + SF + "','" + Div_Code + "'," + Mnth + "," + Year + "";
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
 public DataSet get_Stockist(string SF, string Div_Code, string Mnth, string Year)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;
            
            /*strQry = "select distinct ms.stockist_code,ms.Stockist_name,tc.Last_Updation_Date  from " +
                "(select Stockist_code, Last_Updation_Date, ROW_NUMBER() " +
                "over(partition by Stockist_code order by Last_Updation_Date desc) rw" +
                " from Trans_Current_Stock_details where Division_Code = '" + Div_Code + "') t" +
                " inner join Trans_Current_Stock_details tc on" +
                " t.Stockist_code = tc.Stockist_code and t.Last_Updation_Date = tc.Last_Updation_Date " +
                "and rw = 1" +
                "  inner join  Mas_Stockist ms on ms.Stockist_Code = tc.Stockist_code where ms.Division_Code = '" + Div_Code + "'";*/
            strQry = "exec LastUpdtUniStks '" + SF + "','" + Div_Code + "'," + Mnth + "," + Year + "";
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
 public DataSet get_product_basStockist(string Div_Code,string stkcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;
            strQry = "select Product_Code,Product_Name,CONCAT(Cb_Qty,',',pieces) as qty,cast(convert(varchar, Purchase_Date,101)as datetime) as up_date,isnull(Mgf_date,'') as MFG,(((Conversion_Qty*Cb_Qty)+pieces)*mp.product_netwt) as netweight,isnull(((Cb_Qty* Crate)+(pieces*Prate)),0) StateRate  from Trans_Stock_Updation_Details h inner join mas_product_detail mp on Product_Code=mp.Product_Detail_Code where h.Division_Code = '" + Div_Code + "' and Stockist_code='" + stkcode + "'";
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
        public DataSet get_allStockist(string Div_Code,string stkcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = " select  distinct cast(convert(varchar, Purchase_Date,101)as datetime) as up_date  from Trans_Stock_Updation_Details   where Division_Code = '"+ Div_Code + "' and Stockist_code = '"+ stkcode + "'";
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
        public DataSet get_target_order_mgr(string Div_Code, string SF_Code, string year)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;
            strQry = "Exec Target_order_mgr '" + Div_Code + "', '" + SF_Code + "','" + year + "'";

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
        public DataSet Total_RetailerNetWgt(string div_code, string sfcode, string Date)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "  select sum(net_weight_value)vol from Trans_Order_Head  where Sf_Code='" + sfcode + "' and CONVERT(VARCHAR(25), Order_Date, 126) like '" + Date + "%'";

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

        public DataSet Total_Retailervalue(string div_code, string sfcode, string Date)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "  select sum(Order_Value)vol from Trans_Order_Head  where Sf_Code='" + sfcode + "' and CONVERT(VARCHAR(25), Order_Date, 126) like '" + Date + "%'";

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

        //DEV
        public DataSet View_total_Retailercount_Year(string div_code, string sfcode, string Date)
        {
            DB_EReporting db_ER = new DB_EReporting();
            //DataSet territory = null;
            //string territory_code = "0";
            DataSet dsAdmin = null;

            strQry = "select count( distinct ListedDrCode) from TbMyDayPlan t " +
                "inner join MAS_listeddr a on a.territory_code = t.cluster and ListedDr_Active_Flag = 0 " +
                " where t.sf_code = '"+ sfcode + "' and year(pln_Date)  = '"+ Date +"'";

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

        public DataSet View_mappedRetailers(string div_code, string sfcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "select count(ListedDrCode) from mas_listeddr where Division_Code=" + div_code + " and  charindex(',' + cast('" + sfcode + "' as varchar) + ',',',' + Sf_Code + ',')> 0 and ListedDr_Active_Flag = 0";

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




        public DataSet View_effective_Retailercount_year(string div_code, string sfcode, string Date)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = " select count(distinct t.Trans_Detail_Info_Code)  from DCRMain_Trans h " +
                     "inner join DCRDetail_Lst_Trans t on t.Trans_SlNo=h.Trans_SlNo " +
                     "inner join MAS_listeddr a on a.ListedDrCode=t.Trans_Detail_Info_Code and ListedDr_Active_Flag=0 " +
                     "where charindex(','+cast('" + sfcode + "'  as varchar)+',',','+a.Sf_Code+',')>0 and  year(ListedDr_Created_Date)<='" + Date + "' and " +
                     "year(Activity_Date)<='" + Date + "' AND h.Sf_Code='" + sfcode + "' and h.Division_code='" + div_code + "'  and t.Pob_value!=0";

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
        public DataSet View_Retailercount_year(string div_code, string sfcode, string Date)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = " select count(distinct t.Trans_Detail_Info_Code)  from DCRMain_Trans h " +
                       "inner join DCRDetail_Lst_Trans t on t.Trans_SlNo=h.Trans_SlNo " +
                       "inner join MAS_listeddr a on a.ListedDrCode=t.Trans_Detail_Info_Code and ListedDr_Active_Flag=0 " +
                       "where charindex(','+cast('" + sfcode + "'  as varchar)+',',','+a.Sf_Code+',')>0 and  year(ListedDr_Created_Date)<='" + Date + "' and " +
                       "year(Activity_Date)<='" + Date + "' AND h.Sf_Code='" + sfcode + "' and h.Division_code='" + div_code + "'";

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
        public DataSet View_Non_Retailercount_year(string div_code, string sfcode, string Date)
        {
            DB_EReporting db_ER = new DB_EReporting();
            //DataSet territory = null;
            //string territory_code = string.Empty;
            DataSet dsAdmin = null;

            strQry = " select count(ListedDrCode) from MAS_listeddr a " +
                     " where charindex(','+cast('" + sfcode + "'  as varchar)+',',','+a.Sf_Code+',')>0  and  ListedDr_Active_Flag=0 and year(ListedDr_Created_Date)<='" + Date + "' and " +
                     " ListedDrCode  not IN  (select  t.Trans_Detail_Info_Code   from DCRMain_Trans h " +
                     " inner join DCRDetail_Lst_Trans t on t.Trans_SlNo=h.Trans_SlNo " +
                     " inner join MAS_listeddr a on a.ListedDrCode=t.Trans_Detail_Info_Code  and  ListedDr_Active_Flag=0 " +
                     " where charindex(','+cast('" + sfcode + "'  as varchar)+',',','+a.Sf_Code+',')>0 and  year(ListedDr_Created_Date)<='" + Date + "' and " +
                     " year(Activity_Date)<='" + Date + "' AND h.Sf_Code='" + sfcode + "' and h.Division_code='" + div_code + "')";



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
        public DataSet View_total_Retailercount_mon(string div_code, string sfcode, string year, string FDate, string TDate)
        {
            DB_EReporting db_ER = new DB_EReporting();
            //DataSet territory = null;
            //string territory_code = "0";
            DataSet dsAdmin = null;

            strQry = " select count( distinct ListedDrCode) from TbMyDayPlan t inner join MAS_listeddr a on a.territory_code = t.cluster and ListedDr_Active_Flag = 0 where a.Division_Code='" + div_code + "' and t.sf_code = '" + sfcode + "' " +
                     " and convert(date, pln_Date) between '" + FDate + "' and '" + TDate + "'";

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
        public DataSet View_effective_Retailercount_mon(string div_code, string sfcode, string year, string FDate, string TDate)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;


            strQry = "select count(distinct Trans_Detail_Info_Code)  from DCRMain_Trans h inner join DCRDetail_Lst_Trans t on t.Trans_SlNo = h.Trans_SlNo inner join MAS_listeddr a " +
                     " on a.ListedDrCode = t.Trans_Detail_Info_Code and ListedDr_Active_Flag = 0 where charindex(',' + cast('" + sfcode + "' as varchar) + ',',',' + a.Sf_Code + ',')> 0 " +
                     " and convert(date,ListedDr_Created_Date)<='" + TDate + "' and convert(date, h.Activity_Date) between '" + FDate + "' and '" + TDate + "'  " +
                     " AND h.Sf_Code = '" + sfcode + "' and h.Division_code = '" + div_code + "'  and t.Pob_value != 0";

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
        public DataSet View_Retailercount(string div_code, string sfcode, string FDate, string TDate)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = " select count(distinct trans_detail_info_code) from (select distinct trans_detail_Info_Code from DCRMain_Trans h inner join DCRDetail_Lst_Trans tb  " +
                     " on tb.Trans_SlNo = h.Trans_SlNo where charindex(',' + cast('" + sfcode + "' as varchar) + ',',',' + tb.Sf_Code + ',')> 0 and tb.Division_code = '" + div_code + "' " +
                     " and convert(date, h.Activity_Date) between '" + FDate + "' and '" + TDate + "') as t inner join Mas_ListedDr a on a.ListedDrCode = t.Trans_Detail_Info_Code " +
                     " and ListedDr_Active_Flag = 0 ";


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
        public DataSet View_Non_Retailercount_mon(string div_code, string sfcode, string year, string month)
        {
            DB_EReporting db_ER = new DB_EReporting();
            //DataSet territory = null;
            //string territory_code = string.Empty;
            DataSet dsAdmin = null;


            strQry = "select count(distinct ListedDrCode) from MAS_listeddr a " +
                     " where charindex(','+cast('" + sfcode + "'  as varchar)+',',','+a.Sf_Code+',')>0  and  ListedDr_Active_Flag=0 and convert(varchar,ListedDr_Created_Date,101)<=EOMONTH('" + year + "/" + month + "/01') and " +
                     " ListedDrCode  not IN  (select distinct t.Trans_Detail_Info_Code   from DCRMain_Trans h " +
                     " inner join DCRDetail_Lst_Trans t on t.Trans_SlNo=h.Trans_SlNo " +
                     " inner join MAS_listeddr a on a.ListedDrCode=t.Trans_Detail_Info_Code and  ListedDr_Active_Flag=0 " +
                     " where charindex(','+cast('" + sfcode + "'  as varchar)+',',','+a.Sf_Code+',')>0 and  convert(varchar,ListedDr_Created_Date,101)<=EOMONTH('" + year + "/" + month + "/01') and " +
                     "cast(convert(varchar,h.Activity_Date,101) as datetime)>=('" + year + "/" + month + "/01') and convert(varchar,Activity_Date,101)<=EOMONTH('" + year + "/" + month + "/01') AND h.Sf_Code='" + sfcode + "' and h.Division_code='" + div_code + "')";



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
        public DataSet View_total_Retailercount_day(string div_code, string sfcode, string date)
        {
            DB_EReporting db_ER = new DB_EReporting();
            //DataSet territory = null;
            //string territory_code = string.Empty;
            DataSet dsAdmin = null;


             strQry = "select count(distinct ListedDrCode) from TbMyDayPlan t " +
                "inner join MAS_listeddr a on a.territory_code = t.cluster and ListedDr_Active_Flag = 0 " +
                "where t.sf_code = '"+ sfcode + "' and cast(convert(varchar, pln_Date,101) as datetime)  = '"+ date + "'";

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
        public DataSet View_effective_Retailercount_day(string div_code, string sfcode, string Date)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "select count(distinct t.Trans_Detail_Info_Code)  from DCRMain_Trans h " +
                     "inner join DCRDetail_Lst_Trans t on t.Trans_SlNo=h.Trans_SlNo " +
                     "inner join MAS_listeddr a on a.ListedDrCode=t.Trans_Detail_Info_Code and ListedDr_Active_Flag=0 " +
                     "where charindex(','+cast('" + sfcode + "'  as varchar)+',',','+a.Sf_Code+',')>0 and  CONVERT(VARCHAR(25),ListedDr_Created_Date, 126) <= '" + Date + "%' and " +
                     "CONVERT(VARCHAR(25),h.Activity_Date, 126) like '" + Date + "%'  AND h.Sf_Code='" + sfcode + "' and h.Division_code='" + div_code + "'  and t.Pob_value!=0 ";

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
        public DataSet View_Retailercount_day(string div_code, string sfcode, string date)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "select count(distinct t.Trans_Detail_Info_Code)  from DCRMain_Trans h " +
                     "inner join DCRDetail_Lst_Trans t on t.Trans_SlNo=h.Trans_SlNo " +
                     "inner join MAS_listeddr a on a.ListedDrCode=t.Trans_Detail_Info_Code and ListedDr_Active_Flag=0 " +
                     "where charindex(','+cast('" + sfcode + "'  as varchar)+',',','+a.Sf_Code+',')>0 and  CONVERT(VARCHAR(25),ListedDr_Created_Date, 126) <= '" + date + "%' and " +
                     "CONVERT(VARCHAR(25),h.Activity_Date, 126) like '" + date + "%'  AND h.Sf_Code='" + sfcode + "' and h.Division_code='" + div_code + "'";

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
        public DataSet View_Non_Retailercount_day(string div_code, string sfcode, string date)
        {
            DB_EReporting db_ER = new DB_EReporting();
            //DataSet territory = null;
            //string territory_code = string.Empty;
            DataSet dsAdmin = null;

            strQry = " select count(distinct ListedDrCode) from Mas_ListedDr ld " +
                     " where charindex(','+cast('" + sfcode + "' as varchar)+',',','+ld.Sf_Code+',')>0 " +
                     " and ld.Division_code='" + div_code + "' and ListedDr_Active_Flag=0 and  CONVERT(VARCHAR(25),ListedDr_Created_Date, 126) <= '" + date + "%' and " +
                     " ListedDrCode  not IN  (select distinct t.Trans_Detail_Info_Code   from DCRMain_Trans h " +
                     " inner join DCRDetail_Lst_Trans t on t.Trans_SlNo=h.Trans_SlNo " +
                     " inner join MAS_listeddr a on a.ListedDrCode=t.Trans_Detail_Info_Code and  ListedDr_Active_Flag=0 " +
                     " where charindex(','+cast('" + sfcode + "'  as varchar)+',',','+a.Sf_Code+',')>0 and CONVERT(VARCHAR(25),ListedDr_Created_Date, 126) <= '" + date + "%' and " +
                     " CONVERT(VARCHAR(25),h.Activity_Date, 126) like '" + date + "%'  AND h.Sf_Code='" + sfcode + "' and h.Division_code='" + div_code + "') ";

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
        public DataSet View_Retailerdetailview_eff(string div_code, string sfcode, string Date)
        {
            DB_EReporting db_ER = new DB_EReporting();

            string territory_code = string.Empty;
            DataSet dsAdmin = null;

            strQry = "select DISTINCT t.Trans_Detail_Info_Code, t.Trans_Detail_Name,t.SDP_Name  from DCRMain_Trans h " +
                   "inner join DCRDetail_Lst_Trans t on t.Trans_SlNo=h.Trans_SlNo " +
                   "inner join MAS_listeddr a on a.ListedDrCode=t.Trans_Detail_Info_Code and ListedDr_Active_Flag=0 " +
                   "where charindex(','+cast('" + sfcode + "'  as varchar)+',',','+a.Sf_Code+',')>0 and  year(ListedDr_Created_Date) <= '" + Date + "' and " +
                   " year(h.Activity_Date) <= '" + Date + "'  AND h.Sf_Code='" + sfcode + "' and h.Division_code='" + div_code + "'  and t.Pob_value!=0 ";

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

        public DataSet View_Retailerdetailview_eff_day(string div_code, string sfcode, string Date)
        {
            DB_EReporting db_ER = new DB_EReporting();

            string territory_code = string.Empty;
            DataSet dsAdmin = null;

            strQry = "select DISTINCT t.Trans_Detail_Info_Code, t.Trans_Detail_Name,t.SDP_Name  from DCRMain_Trans h " +
                     "inner join DCRDetail_Lst_Trans t on t.Trans_SlNo=h.Trans_SlNo " +
                     "inner join MAS_listeddr a on a.ListedDrCode=t.Trans_Detail_Info_Code and ListedDr_Active_Flag=0 " +
                     "where charindex(','+cast('" + sfcode + "'  as varchar)+',',','+a.Sf_Code+',')>0 and CONVERT(VARCHAR(25),ListedDr_Created_Date, 126) <= '" + Date + "%' and " +
                     "CONVERT(VARCHAR(25),h.Activity_Date, 126) like '" + Date + "%'  AND h.Sf_Code='" + sfcode + "' and h.Division_code='" + div_code + "'  and t.Pob_value!=0 ";

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
        public DataSet View_Retailerdetailview_eff_Mon(string div_code, string sfcode, string FDate, string TDate)
        {
            DB_EReporting db_ER = new DB_EReporting();

            string territory_code = string.Empty;
            DataSet dsAdmin = null;

            strQry = "select DISTINCT t.Trans_Detail_Info_Code, t.Trans_Detail_Name,t.SDP_Name  from DCRMain_Trans h inner join DCRDetail_Lst_Trans t " +
                     " on t.Trans_SlNo=h.Trans_SlNo inner join MAS_listeddr a on a.ListedDrCode=t.Trans_Detail_Info_Code and ListedDr_Active_Flag=0 " +
                     " where charindex(','+cast('" + sfcode + "'  as varchar)+',',','+a.Sf_Code+',')>0 and   convert(date,ListedDr_Created_Date)<='" + TDate + "' " +
                     " and convert(date,h.Activity_Date) between '" + FDate + "' and '" + TDate + "' AND h.Sf_Code='" + sfcode + "' and h.Division_code='" + div_code + "'  and t.Pob_value!=0 ";

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

        public DataSet View_Non_Retailer_view_mon(string div_code, string sfcode, string FDate, string TDate)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            //strQry = " select * from(select  distinct ListedDrCode,ListedDr_Name,t.ClstrName Territory_name from TbMyDayPlan t inner join MAS_listeddr a " +
                   //  " on a.territory_code = t.cluster and ListedDr_Active_Flag = 0 where t.sf_code = '" + sfcode + "' " +
                   //  " and convert(date, Pln_Date) between'" + FDate + "' and '" + TDate + "' and t.Division_code = ' " + div_code + "' " +
                   //  " and ListedDrCode not IN(select Trans_Detail_Info_Code from (select distinct Trans_Detail_Info_Code from DCRDetail_Lst_Trans " +
                   //  " where charindex(',' + cast('" + sfcode + "' as varchar) + ',',',' + Sf_Code + ',')> 0 and Division_code = '" + div_code + "' " +
                   //  " and convert(date, ModTime) between '" + FDate + "' and '" + TDate + "') as t inner join MAS_listeddr a on a.ListedDrCode = t.Trans_Detail_Info_Code " +
                   //  " and ListedDr_Active_Flag = 0 group by Trans_Detail_Info_Code ) )tb";

	   strQry = " select * from(select  distinct ListedDrCode,ListedDr_Name,tc.Territory_Name  from TbMyDayPlan t "+
             "inner join DCRDetail_Lst_Trans lt on lt.sf_code = t.sf_code and convert(date, lt.ModTime)= convert(date, t.Pln_Date) " +
             "inner join MAS_listeddr a on a.territory_code = t.cluster or lt.SDP = a.Territory_Code " +
             "inner join Mas_Territory_Creation tc on tc.Territory_Code = a.Territory_Code " +
             "where t.sf_code = '" + sfcode + "' and convert(date, Pln_Date) between '" + FDate + "' and '" + TDate + "' and t.Division_code = '" + div_code + "' and ListedDr_Active_Flag = 0 " +
             "and ListedDrCode not IN(select Trans_Detail_Info_Code from (select distinct Trans_Detail_Info_Code from DCRDetail_Lst_Trans " +
             "where charindex(',' + cast('" + sfcode + "' as varchar) + ',',',' + Sf_Code + ',')> 0 and Division_code = '" + div_code + "' " +
             "and convert(date, ModTime) between '" + FDate + "' and '" + TDate + "') as t inner join MAS_listeddr a on a.ListedDrCode = t.Trans_Detail_Info_Code " +
             "and ListedDr_Active_Flag = 0 group by Trans_Detail_Info_Code ))tb";

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
        public DataSet View_Non_Retailer_view_mon(string div_code, string sfcode, string Date)
        {
            DB_EReporting db_ER = new DB_EReporting();
            //DataSet territory = null;
            //string territory_code = string.Empty;
            DataSet dsAdmin = null;

            strQry = " select a.ListedDr_Name,c.Territory_Name from MAS_listeddr a inner join Mas_Territory_Creation c on c.Territory_Code=a.Territory_Code " +
                   " where charindex(','+cast('" + sfcode + "'  as varchar)+',',','+a.Sf_Code+',')>0  and  ListedDr_Active_Flag=0 and year(ListedDr_Created_Date) <= '" + Date + "' and " +
                   " ListedDrCode  not IN  (select distinct t.Trans_Detail_Info_Code   from DCRMain_Trans h " +
                   " inner join DCRDetail_Lst_Trans t on t.Trans_SlNo=h.Trans_SlNo " +
                   " inner join MAS_listeddr a on a.ListedDrCode=t.Trans_Detail_Info_Code and  ListedDr_Active_Flag=0 " +
                   " where charindex(','+cast('" + sfcode + "'  as varchar)+',',','+a.Sf_Code+',')>0 and year(ListedDr_Created_Date) <= '" + Date + "' and " +
                   " year(h.Activity_Date) <= '" + Date + "'  AND h.Sf_Code='" + sfcode + "' and h.Division_code='" + div_code + "') ";

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

        public DataSet View_Non_Retailer_view_mon_day(string div_code, string sfcode, string Date)
        {
            DB_EReporting db_ER = new DB_EReporting();
            //DataSet territory = null;
            //string territory_code = string.Empty;
            DataSet dsAdmin = null;

            /*strQry = " select a.ListedDr_Name,c.Territory_Name from MAS_listeddr a inner join Mas_Territory_Creation c on c.Territory_Code=a.Territory_Code  " +
                   " where charindex(','+cast('" + sfcode + "' as varchar)+',',','+a.Sf_Code+',')>0 " +
                   " and a.Division_code='" + div_code + "' and a.ListedDr_Active_Flag=0 and  CONVERT(VARCHAR(25),a.ListedDr_Created_Date, 126) <= '" + Date + "%' and " +
                   " ListedDrCode  not IN  (select distinct t.Trans_Detail_Info_Code   from DCRMain_Trans h " +
                   " inner join DCRDetail_Lst_Trans t on t.Trans_SlNo=h.Trans_SlNo " +
                   " inner join MAS_listeddr a on a.ListedDrCode=t.Trans_Detail_Info_Code and  ListedDr_Active_Flag=0 " +
                   " where charindex(','+cast('" + sfcode + "'  as varchar)+',',','+a.Sf_Code+',')>0 and CONVERT(VARCHAR(25),ListedDr_Created_Date, 126) <= '" + Date + "%' and " +
                   " CONVERT(VARCHAR(25),h.Activity_Date, 126) like '" + Date + "%'  AND h.Sf_Code='" + sfcode + "' and h.Division_code='" + div_code + "') ";
            */
            strQry = "select ListedDr_Name,ClstrName Territory_Name from MAS_listeddr MS inner join (select distinct cluster,sf_code,convert(date,Pln_Date)Pln_Date,ClstrName from TbMyDayPlan as TB)TB " + " on TB.cluster=Ms.Territory_Code where TB.sf_code='" + sfcode + "' and convert(date,Pln_Date)='" + Date + "' and ListedDr_Active_Flag=0 " + " and ListedDrCode not IN(select Trans_Detail_Info_Code from   DCRDetail_Lst_Trans where  charindex(','+cast('" + sfcode + "' as varchar)+',',','+Sf_Code+',')>0 " + " and Division_code='" + div_code + "' and   CONVERT(date,ModTime)= '" + Date + "')   order by ListedDr_Name";
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

        public DataSet View_Retailerdetailview_mon(string div_code, string sfcode, string FDate, string TDate)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet territory = null;
            string territory_code = string.Empty;
            DataSet dsAdmin = null;

            strQry = "select  trans_detail_Info_Code,a.ListedDr_Name Trans_Detail_Name,tc.Territory_Name SDP_Name from (select distinct trans_detail_Info_Code from DCRDetail_Lst_Trans " +
                     " where charindex(','+cast('" + sfcode + "' as varchar)+',',','+Sf_Code+',')>0 and Division_code='" + div_code + "' and convert(date,ModTime) between '" + FDate + "' and '" + TDate + "' " +
                     " and sf_code='" + sfcode + "') as t inner join Mas_ListedDr a on a.ListedDrCode=t.Trans_Detail_Info_Code inner join mas_territory_creation tc on tc.Territory_Code=a.Territory_Code " +
                     " where  a.ListedDr_Active_Flag=0 group by trans_detail_Info_Code,ListedDr_Name,tc.Territory_Name";
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
        public DataSet view_stockist_statewise(string div_code, string statecode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "select stockist_code,stockist_name  from Mas_Stockist where State_Code='" + statecode + "' and  Division_Code='" + div_code + "' and stockist_Active_Flag=0";


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
        public DataSet get_traveldistance_callreport(string div_code, string sfcode, string Date)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "EXEC GetGEORouteDets '" + sfcode + "','" + Date + "'";

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
        public DataSet GetOrderTimesSFWise(string div_code, string Fdate, string Tdate)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsState = null;

            strQry = "select sf_code,cast(convert(varchar,Activity_Date,101) as date) adate,min(tm) minTime,max(tm) maxTime from vwActivity_Msl_Details where division_code='" + div_code + "' and convert(date,Activity_Date) between '" + Fdate + "' and '" + Tdate + "' "
                     + " group by sf_code,cast(convert(varchar, Activity_Date, 101) as date) order by sf_code,cast(convert(varchar, Activity_Date, 101) as date) ";
            try
            {
                dsState = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsState;
        }



        public DataSet GetOrderCountSFWise(string div_code, string Fdate, string Tdate)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsState = null;

            strQry = "select sf_code,cast(convert(varchar,Activity_Date,101) as date) aDate, count(Order_No)  cnt from vwActivity_Msl_Details where convert(date,Activity_Date) between '" + Fdate + "' and '" + Tdate + "'  and division_code = '" + div_code + "' "
                   + " group by sf_code, cast(convert(varchar, Activity_Date, 101) as date)";
            try
            {
                dsState = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsState;
        }
       public DataSet GetStartTimes(string div_code, string Fdate, string Tdate)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsState = null;

            strQry = "select Sf_Code,cast(convert(varchar,login_date,101) as datetime) login_date,Start_Time,End_Time,Start_Lat,Start_Long from Attendance_history where division_code='" + div_code + "' and convert(date,login_date) between '" + Fdate + "' and '" + Tdate + "'";
            try
            {
                dsState = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsState;
        }
        public DataSet GetChannewiseOrderDay(string div_code, string SF_Code, string Fdate, string SubDivCode="0")
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsState = null;

            strQry = "exec ChannelwiseOrderDay '" + div_code + "','" + SF_Code + "','" + Fdate + "','" + SubDivCode + "'";
            try
            {
                dsState = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsState;
        }
        public DataSet GetChannewiseOrderMonth(string div_code, string SF_Code, string FYear, string FMonth, string SubDivCode="0")
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsState = null;

            strQry = "exec ChannelwiseOrderMonth '" + div_code + "','" + SF_Code + "','" + FYear + "','" + FMonth + "','" + SubDivCode + "'";
            try
            {
                dsState = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsState;
        }

        public DataSet GetBrandwiseOrderDay(string div_code, string SF_Code, string Fdate, string SubDivCode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsState = null;

            strQry = "exec BrandwiseOrderday '" + div_code + "','" + SF_Code + "','" + Fdate + "','" + SubDivCode + "'";
            try
            {
                dsState = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsState;
        }
        public DataSet GetBrandwiseOrderMonth(string div_code, string SF_Code, string FYear, string FMonth, string SubDivCode="0")
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsState = null;

            strQry = "exec BrandwiseOrderMonth '" + div_code + "','" + SF_Code + "','" + FYear + "','" + FMonth + "','" + SubDivCode + "'";
            try
            {
                dsState = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsState;
        }

        public DataSet GetStatewiseOrderDay(string div_code, string SF_Code, string Fdate, string SubDivCode="0")
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsState = null;

            strQry = "exec statewiseOrderDay '" + div_code + "','" + SF_Code + "','" + Fdate + "','" + SubDivCode + "'";
            try
            {
                dsState = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsState;
        }

        public DataSet GetStatewiseOrderMonth(string div_code, string SF_Code, string FYear, string FMonth, string SubDivCode="0")
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsState = null;

            strQry = "exec statewiseOrderMonth '" + div_code + "','" + SF_Code + "','" + FYear + "','" + FMonth + "','" + SubDivCode + "'";
            try
            {
                dsState = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsState;
        }

        public DataSet PRIGetBrandwiseOrderDay(string div_code, string SF_Code, string Fdate, string SubDivCode="0")
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsState = null;

            strQry = "exec pri_order_brand_day '" + div_code + "','" + SF_Code + "','" + Fdate + "','" + SubDivCode + "'";
            try
            {
                dsState = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsState;
        }
        public DataSet PriGetStatewiseOrderMonth(string div_code, string SF_Code, string FYear, string FMonth, string SubDivCode="0")
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsState = null;

            strQry = "exec PRI_ORDER_STATE_MONTH '" + div_code + "','" + SF_Code + "','" + FYear + "','" + FMonth + "','" + SubDivCode + "'";
            try
            {
                dsState = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsState;
        }
        public DataSet PriGetStatewiseOrderDay(string div_code, string SF_Code, string Fdate, string SubDivCode="0")
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsState = null;

            strQry = "exec PRI_ORDER_STATE_DAY '" + div_code + "','" + SF_Code + "','" + Fdate + "','" + SubDivCode + "'";
            try
            {
                dsState = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsState;
        }




        public DataSet PRIGetBrandwiseOrderMonth(string div_code, string SF_Code, string FYear, string FMonth, string SubDivCode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsState = null;

            strQry = "exec pri_order_brand_month '" + div_code + "','" + SF_Code + "','" + FYear + "','" + FMonth + "','" + SubDivCode + "'";
            try
            {
                dsState = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsState;
        }
        public DataSet GetSecProductOrderYear(string div_code, string SF_Code, string FYear, string SubDivCode="0")
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsState = null;

            strQry = "exec ProductwiseOrderYear '" + div_code + "','" + SF_Code + "','" + FYear + "','" + SubDivCode + "'";
            try
            {
                dsState = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsState;
        }
        public DataSet GetSecProductOrderMonth(string div_code, string SF_Code, string FYear, string FMonth, string SubDivCode="0")
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsState = null;

            strQry = "exec ProductwiseOrderMonth '" + div_code + "','" + SF_Code + "','" + FYear + "','" + FMonth + "','" + SubDivCode + "'";
            try
            {
                dsState = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsState;
        }

        public DataSet GetSecProductOrderDay(string div_code, string SF_Code, string FDay, string SubDivCode="0")
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsState = null;

            strQry = "exec ProductwiseOrderDay '" + div_code + "','" + SF_Code + "','" + FDay + "','" + SubDivCode + "'";
            try
            {
                dsState = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsState;
        }



        public DataSet GetPRIProductOrderYear(string div_code, string SF_Code, string FYear, string SubDivCode="0")
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsState = null;

            strQry = "exec Pri_ProductwiseOrderYear '" + div_code + "','" + SF_Code + "','" + FYear + "','" + SubDivCode + "'";
            try
            {
                dsState = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsState;
        }

        public DataSet GetPRIProductOrderMonth(string div_code, string SF_Code, string FYear, string FMonth, string SubDivCode="0")
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsState = null;

            strQry = "exec Pri_ProductwiseOrderMonth '" + div_code + "','" + SF_Code + "','" + FYear + "','" + FMonth + "','" + SubDivCode + "'";
            try
            {
                dsState = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsState;
        }

        public DataSet GetPRIProductOrderDay(string div_code, string SF_Code, string FDay, string SubDivCode="0")
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsState = null;

            strQry = "exec Pri_ProductwiseOrderDay '" + div_code + "','" + SF_Code + "','" + FDay + "','" + SubDivCode + "'";
            try
            {
                dsState = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsState;
        }


        public DataSet GetTPDeivationRetailers(string SF_Code, string FDay, string oType, string rType, string phType = "0")
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsState = null;

            strQry = "exec GET_TP_RetValues '" + SF_Code + "','" + FDay + "','" + oType + "','" + rType + "','" + phType + "'";
            try
            {
                dsState = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsState;
        }

        public DataSet GetTCECDeviation(string SF_Code, string FYear, string FMonth)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsState = null;

            // strQry = "select sf_code,Worked_with_Name, Activity_Date, count(Trans_Detail_Info_Code) TC,count( DISTINCT Trans_Detail_Info_Code) RTC,  sum(case when  Order_Value>0 then 1 else 0 end) EC,  sum(Order_Value) Order_Value from ( select sf_code, Worked_with_Name, cast(convert(varchar, Activity_Date, 101) as datetime) Activity_Date, Trans_Detail_Info_Code, sum(ISNULL(Order_Value, 0)) Order_Value from vwActivity_MSL_Details  where sf_code = '" + SF_Code + "'  and year(Activity_Date) = '" + FYear + "' group by sf_code, Worked_with_Name, cast(convert(varchar, Activity_Date, 101) as datetime), Trans_Detail_Info_Code)kk     group by sf_code, Worked_with_Name, Activity_Date";
            //  strQry = "select sf_code,stockist_name,Worked_with_Name, Activity_Date, count(Trans_Detail_Info_Code) TC, count( DISTINCT Trans_Detail_Info_Code) RTC, sum(case when  Order_Value>0 then 1 else 0 end) EC,  sum(Order_Value) Order_Value,Doc_Special_Code from ( select h.sf_code,stockist_name, Worked_with_Name, cast(convert(varchar, Activity_Date, 101) as datetime) Activity_Date,Doc_Special_Code " +
            //         " Trans_Detail_Info_Code, sum(ISNULL(Order_Value, 0)) Order_Value,Doc_Special_Code from vwActivity_MSL_Details h  inner  join Mas_ListedDr d on d.ListedDrCode=h.Trans_Detail_Info_Code " +
            //       " where h.sf_code = '" + SF_Code + "'  and year(Activity_Date) = '" + FYear + "' group by h.sf_code, stockist_name,Worked_with_Name, cast(convert(varchar, Activity_Date, 101) as datetime), Trans_Detail_Info_Code,Doc_Special_Code)kk    group by sf_code, stockist_name,Worked_with_Name, Activity_Date,Doc_Special_Code";

            if (SF_Code.Contains("MGR"))
            {
                strQry = "select sf_code, isnull(stockist_name,'')stockist_name,Worked_with_Name, Activity_Date, count(Trans_Detail_Info_Code) TC, count( DISTINCT Trans_Detail_Info_Code) RTC,  sum(case when  Order_Value>0 then 1 else 0 end) EC,  sum(Order_Value) Order_Value,Doc_Special_Code,Doc_Special_Name,SDP_Name,Activity_Remarks,ListedDr_Address1,sum(phoneOrder)phoneOrder   from (  select h.sf_code,stockist_name, Sf_Name Worked_with_Name, cast(convert(varchar, Activity_Date, 101) as datetime) Activity_Date,d.Doc_Special_Code, " +
                        " Trans_Detail_Info_Code,SDP_Name, sum(ISNULL(Order_Value, 0)) Order_Value,Doc_Special_Name,Activity_Remarks,ListedDr_Address1,sum( cast( isnull( OrderType,0) as int))  phoneOrder  from vwActivity_MSL_Details h   inner  join Mas_ListedDr d on d.ListedDrCode=h.Trans_Detail_Info_Code  inner join Mas_Doctor_Speciality m on m.Doc_Special_Code= d.Doc_Special_Code   inner join Mas_Salesforce ss on ss.sf_code=h.sf_code where (h.sf_code = '" + SF_Code + "' or charindex('&&'+'" + SF_Code + "'+'&&','&&'+Worked_with_Code+'&&')>0) and year(Activity_Date) = '" + FYear + "'  " +
                        " group by h.sf_code, stockist_name,Sf_Name, cast(convert(varchar, Activity_Date, 101) as datetime), Trans_Detail_Info_Code,d.Doc_Special_Code,Doc_Special_Name,SDP_Name,Activity_Remarks,ListedDr_Address1    )kk    group by sf_code, stockist_name,Worked_with_Name, Activity_Date,Doc_Special_Code,Doc_Special_Name,SDP_Name,Activity_Remarks,ListedDr_Address1 ";
            }
            else
            {
                //strQry = "select sf_code,stockist_name,Worked_with_Name, Activity_Date, count(Trans_Detail_Info_Code) TC, count( DISTINCT Trans_Detail_Info_Code) RTC,  sum(case when  Order_Value>0 then 1 else 0 end) EC,  sum(Order_Value) Order_Value,Doc_Special_Code,Doc_Special_Name from (  select h.sf_code,stockist_name, Worked_with_Name, cast(convert(varchar, Activity_Date, 101) as datetime) Activity_Date,d.Doc_Special_Code,Doc_Special_Name " +
                //       " Trans_Detail_Info_Code, sum(ISNULL(Order_Value, 0)) Order_Value,Doc_Special_Name from vwActivity_MSL_Details h   inner  join Mas_ListedDr d on d.ListedDrCode=h.Trans_Detail_Info_Code  inner join Mas_Doctor_Speciality m on m.Doc_Special_Code= d.Doc_Special_Code  where h.sf_code = '" + SF_Code + "' and year(Activity_Date) = '" + FYear + "' " +
                //       " group by h.sf_code, stockist_name,Worked_with_Name, cast(convert(varchar, Activity_Date, 101) as datetime), Trans_Detail_Info_Code,d.Doc_Special_Code,Doc_Special_Name   )kk    group by sf_code, stockist_name,Worked_with_Name, Activity_Date,Doc_Special_Code,Doc_Special_Name";

                strQry = " select sf_code,isnull(stockist_name,'')stockist_name,Worked_with_Name, Activity_Date, count(Trans_Detail_Info_Code) TC, count( DISTINCT Trans_Detail_Info_Code) RTC,  sum(case when  Order_Value>0 then 1 else 0 end) EC, " +
  " sum(Order_Value) Order_Value,Doc_Special_Code,Doc_Special_Name,SDP_Name,Activity_Remarks,ListedDr_Address1 , sum(phoneOrder)phoneOrder from (  select h.sf_code,stockist_name, Worked_with_Name, cast(convert(varchar, Activity_Date, 101) as datetime) Activity_Date,d.Doc_Special_Code, " +
" Trans_Detail_Info_Code, sum(ISNULL(Order_Value, 0)) Order_Value,Doc_Special_Name,SDP_Name,Activity_Remarks,ListedDr_Address1, sum( cast( isnull( OrderType,0) as int))  phoneOrder  from vwActivity_MSL_Details h   inner  join Mas_ListedDr d on d.ListedDrCode=h.Trans_Detail_Info_Code   inner join Mas_Doctor_Speciality m on m.Doc_Special_Code= d.Doc_Special_Code  " +
 " where h.sf_code = '" + SF_Code + "' and  year(Activity_Date) = '" + FYear + "' group by h.sf_code, stockist_name,Worked_with_Name, cast(convert(varchar, Activity_Date, 101) as datetime), Trans_Detail_Info_Code,d.Doc_Special_Code,Doc_Special_Name,SDP_Name,Activity_Remarks,ListedDr_Address1    )kk  " +
 "  group by sf_code, stockist_name,Worked_with_Name, Activity_Date,kk.Doc_Special_Code,Doc_Special_Name,SDP_Name,Activity_Remarks,ListedDr_Address1 ";
            }
            try
            {
                dsState = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsState;
        }

        public DataSet GetPrmaryDevaition(string SF_Code, string FYear, string FMonth,string divCode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsState = null;

            // strQry = "select sf_code,Worked_with_Name, Activity_Date, count(Trans_Detail_Info_Code) TC,count( DISTINCT Trans_Detail_Info_Code) RTC,  sum(case when  Order_Value>0 then 1 else 0 end) EC,  sum(Order_Value) Order_Value from ( select sf_code, Worked_with_Name, cast(convert(varchar, Activity_Date, 101) as datetime) Activity_Date, Trans_Detail_Info_Code, sum(ISNULL(Order_Value, 0)) Order_Value from vwActivity_MSL_Details  where sf_code = '" + SF_Code + "'  and year(Activity_Date) = '" + FYear + "' group by sf_code, Worked_with_Name, cast(convert(varchar, Activity_Date, 101) as datetime), Trans_Detail_Info_Code)kk     group by sf_code, Worked_with_Name, Activity_Date";

            if (SF_Code.Contains("MGR"))
            {
                strQry = "select sf_code,Worked_with_Name,ModTime,Trans_Detail_Info_Code,Stockist_Name,SDP_Name,sum(POB_Value)POB_Value from (select h.sf_code, Sf_Name Worked_with_Name,cast(convert(varchar, ModTime, 101) as datetime) ModTime,Trans_Detail_Info_Code,Stockist_Name,SDP_Name,isnull((POB_Value),0) POB_Value from vwActivity_CSH_Detail h   inner join (SELECT Stockist_Code,Stockist_Name FROM  Mas_Stockist WHERE Division_Code='" + divCode + "' AND Stockist_Active_Flag=0 ) s on s.Stockist_Code=Trans_Detail_Info_Code  inner join mas_salesforce ss on ss.sf_code=h.sf_code   inner join TbMyDayPlan tp on tp.sf_code=h.sf_code and   cast(convert(varchar, ModTime, 101) as datetime)=  cast(convert(varchar, pln_date, 101) as datetime)" +
                        " where  ( tp.sf_code = '" + SF_Code + "' or charindex('&&'+'" + SF_Code + "'+'&&','&&'+tp.Worked_with_Code+'&&')>0  ) and year(ModTime) = '" + FYear + "'   group by h.sf_code,Sf_Name, cast(convert(varchar, ModTime, 101) as datetime),Trans_Detail_Info_Code,Stockist_Name,SDP_Name,POB_Value)as tab group by sf_code,Worked_with_Name,ModTime,Trans_Detail_Info_Code,Stockist_Name,SDP_Name order by ModTime,SDP_Name";
            }
            else
            {
                strQry = "select sf_code,Worked_with_Name,ModTime,Trans_Detail_Info_Code,Stockist_Name,SDP_Name,sum(POB_Value)POB_Value from (select h.sf_code, Worked_with_Name,cast(convert(varchar, ModTime, 101) as datetime) ModTime,Trans_Detail_Info_Code,Stockist_Name,SDP_Name,isnull((POB_Value),0) POB_Value from vwActivity_CSH_Detail h   inner join (SELECT Stockist_Code,Stockist_Name FROM  Mas_Stockist WHERE Division_Code='" + divCode + "' AND Stockist_Active_Flag=0 ) s on s.Stockist_Code=Trans_Detail_Info_Code " +
                     " where   h.sf_code = '" + SF_Code + "' and year(ModTime) = '" + FYear + "'  group by h.sf_code,Worked_with_Name, cast(convert(varchar, ModTime, 101) as datetime),Trans_Detail_Info_Code,Stockist_Name,SDP_Name,POB_Value)as tab group by sf_code,Worked_with_Name,ModTime,Trans_Detail_Info_Code,Stockist_Name,SDP_Name order by ModTime,SDP_Name";

            }
            try
            {
                dsState = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsState;
        }

        public DataSet GetDistributorDeviation(string SF_Code, string FYear, string FMonth)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsState = null;

            strQry = "select sf_code,mnth, count(Trans_Detail_Info_Code) cnt ,sum(orderVal) orderVal from (  select sf_code, month(vstTime) mnth,Trans_Detail_Info_Code, isnull(sum(POB_Value),0) orderVal from vwActivity_CSH_Detail where sf_code='" + SF_Code + "' and year(vstTime) = '" + FYear + "'  group by sf_code,Trans_Detail_Info_Code, month(vstTime))kk group by sf_code,mnth";
            try
            {
                dsState = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsState;
        }
        public DataSet GetOrderDetailsWithPrice(string DivCode, string SFCode, string FYear, string FMonth, string SubDivCode = "0")
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsState = null;

            if (DivCode == "98")
            {
                strQry = "Exec GetOrderDetails1 '" + DivCode + "','" + SFCode + "','" + FYear + "','" + FMonth + "','" + SubDivCode + "'";
            }
            else { strQry = "Exec GetOrderDetails '" + DivCode + "','" + SFCode + "','" + FYear + "','" + FMonth + "','" + SubDivCode + "'"; }

            try
            {
                dsState = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsState;
        }
        public DataSet GetIssuSlipDayTCEC(string DivCode, string SFCode, string FYear, string FMonth, string SubDivCode = "0")
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsState = null;

            strQry = "Exec GET_IssuSlip_Day_TcEc '" + DivCode + "','" + SFCode + "','" + FYear + "','" + FMonth + "','" + SubDivCode + "'";
            try
            {
                dsState = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsState;
        }

        public DataSet GetIssuSlipDayTCECSFOnly(string DivCode, string SFCode, string FYear, string FMonth, string SubDivCode = "0")
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsState = null;

            strQry = "Exec GET_IssuSlip_Day_TcEc_SFonly '" + DivCode + "','" + SFCode + "','" + FYear + "','" + FMonth + "','" + SubDivCode + "'";
            try
            {
                dsState = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsState;
        }
        public DataSet GetNewOutletPenetration(string DivCode, string SFCode, string FYear, string FMonth, string SubDivCode = "0")
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsState = null;

            strQry = "Exec GET_New_Retailer_values '" + DivCode + "','" + SFCode + "','" + FYear + "','" + FMonth + "','" + SubDivCode + "'";
            try
            {
                dsState = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsState;
        }

        public DataSet GetNewOutletPenetrationSFOnly(string DivCode, string SFCode, string FYear, string FMonth, string SubDivCode = "0")
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsState = null;

            strQry = "Exec GET_New_Retailer_values_sfOnly '" + DivCode + "','" + SFCode + "','" + FYear + "','" + FMonth + "','" + SubDivCode + "'";
            try
            {
                dsState = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsState;
        }

        public DataSet GetSFCalls(string DivCode, string SFCode, string FDate, string SubDivCode = "0")
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsState = null;

            strQry = "Exec callSFwise '" + DivCode + "','" + SFCode + "','" + FDate + "','" + SubDivCode + "'";
            try
            {
                dsState = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsState;
        }
        public DataSet View_total_Ret_Ac_data(string div_code, string sfcode, int year, int month)
        {
            DB_EReporting db_ER = new DB_EReporting();
            //DataSet territory = null;
            //string territory_code = "0";
            DataSet dsAdmin = null;

            strQry = "select count(distinct ListedDrCode) from Mas_ListedDr ld " +
                    "where charindex(','+cast('" + sfcode + "' as varchar)+',',','+ld.Sf_Code+',')>0 " +
                    "and ld.Division_code='" + div_code + "' and ListedDr_Active_Flag=0 " +
                    "and cast( convert(varchar,ListedDr_Created_Date,101) as datetime)<=EOMONTH('" + year + "/" + month + "/01')";


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

        public DataSet View_Retailercount_act_Data_v(string div_code, string sfcode, int year, int month)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "  select count(distinct t.Trans_Detail_Info_Code)  from DCRMain_Trans h " +
                   " inner join DCRDetail_Lst_Trans t on t.Trans_SlNo=h.Trans_SlNo " +
                   " inner join MAS_listeddr a on a.ListedDrCode=t.Trans_Detail_Info_Code and ListedDr_Active_Flag=0 " +
                   " where charindex(','+cast('" + sfcode + "'  as varchar)+',',','+a.Sf_Code+',')>0 and   cast( convert(varchar,ListedDr_Created_Date,101) as datetime)<=EOMONTH('" + year + "/" + month + "/01') and " +
                   " cast(convert(varchar,h.Activity_Date,101) as datetime)>=('" + year + "/" + month + "/01') and cast( convert(varchar,h.Activity_Date,101) as datetime)<=EOMONTH('" + year + "/" + month + "/01') AND h.Sf_Code='" + sfcode + "' and h.Division_code='" + div_code + "'";


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
        public DataSet View_effective_Retailercount_mon_act_data(string div_code, string sfcode, int year, int month)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;


            strQry = "  select count(distinct t.Trans_Detail_Info_Code)  from DCRMain_Trans h " +
                     " inner join DCRDetail_Lst_Trans t on t.Trans_SlNo=h.Trans_SlNo " +
                     " inner join MAS_listeddr a on a.ListedDrCode=t.Trans_Detail_Info_Code and ListedDr_Active_Flag=0 " +
                     " where charindex(','+cast('" + sfcode + "'  as varchar)+',',','+a.Sf_Code+',')>0 and cast( convert(varchar,ListedDr_Created_Date,101) as datetime)<=EOMONTH('" + year + "/" + month + "/01') and " +
                     " cast(convert(varchar,h.Activity_Date,101) as datetime)>=('" + year + "/" + month + "/01') and cast( convert(varchar,h.Activity_Date,101) as datetime)<=EOMONTH('" + year + "/" + month + "/01') AND h.Sf_Code='" + sfcode + "' and h.Division_code='" + div_code + "'  and t.Pob_value!=0 ";

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
        public DataSet View_total_Ret_Ac_data_new(string div_code, string sfcode, int year, int month)
        {
            DB_EReporting db_ER = new DB_EReporting();
            //DataSet territory = null;
            //string territory_code = "0";
            DataSet dsAdmin = null;

            strQry = "select count(distinct ListedDrCode) from Mas_ListedDr ld " +
                    "where charindex(','+cast('" + sfcode + "' as varchar)+',',','+ld.Sf_Code+',')>0 " +
                    "and ld.Division_code='" + div_code + "' and ListedDr_Active_Flag=0 " +
                    "and month(ListedDr_Created_Date)='" + month + "' and year(ListedDr_Created_Date)='" + year + "'";


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
        public DataSet GetCallDSR(string SFCode, string fYear, string fMonth, string Types)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet dsState = null;

            strQry = "exec getCallsDCR '" + SFCode + "','" + fYear + "','" + fMonth + "','" + Types + "'";
            try
            {
                dsState = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsState;
        }
        public DataSet GetTotalRetailSal(string DivCode, string SFCode, string FYear, string FMonth, string SubDiv)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet dsState = null;

            strQry = " exec [Get_Total_Sal]  '" + DivCode + "','" + SFCode + "','" + FYear + "','" + FMonth + "','" + SubDiv + "'";
            try
            {
                dsState = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsState;
        }
        public DataSet GetInventorySummary(string DivCode, string SFCode, string fDate, string tDate, string SubDiv)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet dsState = null;

            strQry = "exec getDailyInventory '" + DivCode + "','" + SFCode + "','" + fDate + "','" + tDate + "','" + SubDiv + "'";
            try
            {
                dsState = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsState;
        }
        public DataSet GetDistVal(string DivCode, string FYear, string FMonth, string sfCode, string desig)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet dsState = null;

            strQry = "exec SOB_POB_Data '" + DivCode + "','" + FYear + "','" + FMonth + "','" + sfCode + "','" + desig + "'";
            try
            {
                dsState = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsState;
        }

        public DataSet get_OrderValues_UOMWise(string DivCode, string SF_Code, string FDate, string TDate, string SubDiv)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet dsState = null;

            strQry = "exec GET_FFWiseUOM_Order '" + DivCode + "','" + SF_Code + "','" + FDate + "','" + TDate + "','" + SubDiv + "'";
            try
            {
                dsState = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsState;
        }
 public DataSet get_ProductValues_UOMWise(string DivCode, string FDate, string TDate,string sfCode="admin")
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet dsState = null;

            /*strQry = "select h.sf_code,cast(convert(varchar, ModTime, 101) as datetime) Order_Date ,UOM_Weight, sum(Quantity) Quantity,Product_Cat_Code from vwActivity_MSL_Details h inner join Trans_Order_Details d on d.Trans_Sl_No = h.Order_No inner join mas_product_detail p on p.Product_Detail_Code = d.Product_Code where cast(convert(varchar, ModTime,101) as datetime)>= '" + FDate + "'" +
                " and cast(convert(varchar, ModTime,101) as datetime)<= '" + TDate + "'  and h.division_code = '" + DivCode + "' group by h.sf_code,cast(convert(varchar, ModTime, 101) as datetime),UOM_Weight,Product_Cat_Code";
            */
            strQry = "exec SFFldAnalysis '" + sfCode + "','" + DivCode + "','" + FDate + "','" + TDate + "'";
            try
            {
                dsState = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsState;
        }


        public DataSet get_Payment_Detals_SFWise(string sfCode, string FDate, string TDate)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet dsState = null;

            strQry = "exec getPayDetailsSF '" + sfCode + "','" + FDate + "','" + TDate + "'";
            try
            {
                dsState = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsState;
        }

        public DataSet get_Payment_Detals_CustomerWise(string CustCode, string FDate, string TDate)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet dsState = null;

            strQry = "exec getPayDetailsCustomer '" + CustCode + "','" + FDate + "','" + TDate + "'";
            try
            {
                dsState = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsState;
        }

        public DataSet getclosstk(string sfCode, string divcode, string fDate)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet dsState = null;

            strQry = "exec Retailer_Closing_new '" + sfCode + "','" + divcode + "','" + fDate + "'";
            try
            {
                dsState = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsState;
        }

        public DataSet getclosstk_qty(string sfCode, string divcode, string fDate)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet dsState = null;

            strQry = "exec Retailer_Closing_new_qty '" + sfCode + "','" + divcode + "','" + fDate + "'";
            try
            {
                dsState = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsState;
        }
        public DataSet getRetailOrdersByDist(string Stk, string FDate, string TDate, string type, string div)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet dsState = null;

            strQry = "exec getRetailOrderByStk '" + Stk + "','" + FDate + "','" + TDate + "','" + type + "','" + div + "'";
            try
            {
                dsState = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsState;
        }


        public string savorders(string sxml, string Remark, string Ordrval, string RetCode, string RecDate, string stkcode, string divcode, string netwt,string dcrcode,string Type,string ref_order,string sub_total,string dis_total,string tax_total)
        {
            DataSet ds = null;
            DB_EReporting db_ER = new DB_EReporting();
            string msg = string.Empty;
            string routc = null;
            string routar = null;
            string CollAmt = null;
            string Disc = null;
            string DisAmt = null;
            string RateMode = null;
            string ARC = null;
            strQry = "exec svsecorder '" + stkcode + "','" + RetCode + "','" + stkcode + "','" + routc + "','" + routar + "','" + RecDate + "','" + Ordrval + "','" + CollAmt + "','" + netwt + "','" + Remark + "','" + Disc + "','" + DisAmt + "','" + RateMode + "','" + ARC + "'," + divcode + ",'" + sxml + "','"+ Type + "','"+ref_order+"','"+ sub_total + "','"+ dis_total + "','"+ tax_total + "'";
            try
            {
                ds = db_ER.Exec_DataSet(strQry);
                msg = ds.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            return msg;
        }
		
		
        public DataSet GetSecondaryOrders(string sfcode, string divcode, string FDate, string TDate, string subdivcode = "0", string statecode = "0")
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet dsState = null;

            strQry = "exec getSecondaryOrders '" + sfcode + "','" + divcode + "','" + FDate + "','" + TDate + "','" + subdivcode + "',"+statecode+"";
            try
            {
                dsState = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsState;
        }
		public DataSet GetpgroupOrderDay(string div_code, string SF_Code, string Fdate)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsState = null;

            strQry = "exec productgrpwiseOrderDay '" + div_code + "','" + SF_Code + "','" + Fdate + "'";
            try
            {
                dsState = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsState;
        }
      public DataSet GepgrpOrderMonth(string div_code, string SF_Code, string FYear, string FMonth)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsState = null;

            strQry = "exec productgrpwiseOrdermonth '" + div_code + "','" + SF_Code + "','" + FYear + "','" + FMonth + "'";
            try
            {
                dsState = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsState;
        }

    }
}