using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DBase_EReport;
using System.Configuration;


namespace Bus_EReport
{
    public class Purchase
    {
        private string strQry = string.Empty;

      public DataSet statewise_Trend_analysis_Distributor_descending(string div_code, string fromdate, string todate, string subdivision, string state_cd)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;
           if (div_code != "4")
            {

            strQry = "  select st.state_code,st.statename,st.shortname ,sum(t.Rec_Qty * t.Distributer_Rate)as value  from Trans_Stock_Updation_Details  t INNER JOIN Mas_Stockist S  " +
               " ON t.Stockist_code=S.Stockist_Code right outer join mas_state st on st.State_Code=s.State_Code " +
           "where  t.Purchase_Date between '" + fromdate + "' and '" + todate + "' and   st.state_code in (" + state_cd + ") and st.State_Active_Flag=0 and s.Division_Code='" + div_code + "' and subdivision_code='" + subdivision + "' " +
           " group by  st.state_code,st.statename,st.shortname order by value desc ";
              }
            else
            {
                strQry = "  select st.state_code,st.statename,st.shortname ,sum((t.Rec_Qty * t.Distributer_Rate)+ (t.Rec_Pieces* t.DP_BaseRate))as value  from Trans_Stock_Updation_Details  t INNER JOIN Mas_Stockist S  " +
                  " ON t.Stockist_code=S.Stockist_Code right outer join mas_state st on st.State_Code=s.State_Code " +
              "where  t.Purchase_Date between '" + fromdate + "' and '" + todate + "' and   st.state_code in (" + state_cd + ") and st.State_Active_Flag=0 and s.Division_Code='" + div_code + "' and subdivision_code='" + subdivision + "' " +
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
     public DataSet statewise_Trend_analysis_totalvalue(string div_code, string fromdate, string todate, string subdivision)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;
          if (div_code != "4")
            {

            strQry = "   select sum(t.Rec_Qty * t.Distributer_Rate)as value  from Trans_Stock_Updation_Details  t INNER JOIN Mas_Stockist S " +
                "   ON t.Stockist_code=S.Stockist_Code right outer join mas_state st on st.State_Code=s.State_Code " +
          " where  t.Purchase_Date between '" + fromdate + "' and '" + todate + "'  and st.State_Active_Flag=0 and s.Division_Code='" + div_code + "' and subdivision_code='" + subdivision + "' " +
          "order by value desc";

              }
            else
            {

                strQry = "  select sum((t.Rec_Qty * t.Distributer_Rate)+ (Rec_Pieces* DP_BaseRate))as value  from Trans_Stock_Updation_Details  t INNER JOIN Mas_Stockist S " +
                    "   ON t.Stockist_code=S.Stockist_Code right outer join mas_state st on st.State_Code=s.State_Code " +
              " where  t.Purchase_Date between '" + fromdate + "' and '" + todate + "'  and st.State_Active_Flag=0 and s.Division_Code='" + div_code + "' and subdivision_code='" + subdivision + "' " +
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


     public DataSet statewise_Trend_analysis_value_singleProduct(string product_code,string div_code, int fmonth, int fyear, string subdivision,string statecode)
     {
         DB_EReporting db_ER = new DB_EReporting();

         DataSet dsAdmin = null;

         strQry = "   select sum(t.Rec_Qty * t.Distributer_Rate)as value  from Trans_Stock_Updation_Details  t INNER JOIN Mas_Stockist S " +
             "   ON t.Stockist_code=S.Stockist_Code right outer join mas_state st on st.State_Code=s.State_Code " +
       " where   month(Purchase_Date)='" + fmonth + "' and YEAR(Purchase_Date)='" + fyear + "'  and t.Product_Code='" + product_code + "' and st.state_code in (" + statecode + ") and st.State_Active_Flag=0 and s.Division_Code='" + div_code + "' and subdivision_code='" + subdivision + "' " +
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
     public DataSet statewise_Trend_analysis_value_singleProduct_total(string div_code, int fmonth, int fyear, string subdivision,string statecode)
     {
         DB_EReporting db_ER = new DB_EReporting();

         DataSet dsAdmin = null;

         strQry = "   select sum(t.Rec_Qty * t.Distributer_Rate)as value  from Trans_Stock_Updation_Details  t INNER JOIN Mas_Stockist S " +
             "   ON t.Stockist_code=S.Stockist_Code right outer join mas_state st on st.State_Code=s.State_Code " +
       " where   month(Purchase_Date)='" + fmonth + "' and YEAR(Purchase_Date)='" + fyear + "'  and st.State_Active_Flag=0  and st.state_code in (" + statecode + ")and s.Division_Code='" + div_code + "' and subdivision_code='" + subdivision + "' " +
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
        
    public DataSet areawise_Trend_analysis_Distributor_descending(string div_code, string fromdate, string todate, string subdivision,string state_code)
 {
     DB_EReporting db_ER = new DB_EReporting();

     DataSet dsAdmin = null;
if (div_code != "4")
     {

     strQry = " select a.Area_code,a.Area_name,sum(t.Rec_Qty * t.Distributer_Rate)as value  from Trans_Stock_Updation_Details  t INNER JOIN Mas_Stockist S  "+
      "   ON t.Stockist_code=S.Stockist_Code inner join Mas_Territory Tt on Tt.Territory_code=S.Territory_Code INNER JOIN Mas_Zone Z ON Z.Zone_code=Tt.Zone_code  right outer join Mas_Area a  ON a.Area_code=Z.Area_code  "+
     " where  t.Purchase_Date between '"+fromdate+"' and '"+todate+"'  and a.Area_Active_Flag=0 and s.Division_Code='"+div_code+"'  and subdivision_code='"+subdivision+"' and a.State_Code='"+state_code+"' "+
      " group by  a.Area_code,a.Area_name order by value desc  ";

 }
     else
     {
         strQry = " select a.Area_code,a.Area_name,sum ((t.Rec_Qty * t.Distributer_Rate) + (t.Rec_Pieces* t.DP_BaseRate))as value  from Trans_Stock_Updation_Details  t INNER JOIN Mas_Stockist S  " +
          "   ON t.Stockist_code=S.Stockist_Code inner join Mas_Territory Tt on Tt.Territory_code=S.Territory_Code INNER JOIN Mas_Zone Z ON Z.Zone_code=Tt.Zone_code  right outer join Mas_Area a  ON a.Area_code=Z.Area_code  " +
         " where  t.Purchase_Date between '" + fromdate + "' and '" + todate + "'  and a.Area_Active_Flag=0 and s.Division_Code='" + div_code + "'  and subdivision_code='" + subdivision + "' and a.State_Code='" + state_code + "' " +
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

   public DataSet areawise_Trend_analysis_totalvalue(string div_code, string fromdate, string todate, string subdivision,string statecode)
             {
                 DB_EReporting db_ER = new DB_EReporting();

                 DataSet dsAdmin = null;
if (div_code != "4")
                 {

                 strQry =   "select sum(t.Rec_Qty * t.Distributer_Rate)as value  from Trans_Stock_Updation_Details  t INNER JOIN Mas_Stockist S   "+
        " ON t.Stockist_code=S.Stockist_Code inner join Mas_Territory Tt on Tt.Territory_code=S.Territory_Code INNER JOIN Mas_Zone Z ON Z.Zone_code=Tt.Zone_code  right outer join Mas_Area a  ON a.Area_code=Z.Area_code  "+
  "where  t.Purchase_Date between '"+fromdate+"' and '"+todate+"'   and a.Area_Active_Flag=0 and s.Division_Code='"+div_code+"'  and subdivision_code='"+subdivision+"' and a.State_Code='"+statecode+"' "+
  " order by value desc";
 }
                 else
                 {
                     strQry = "select sum((t.Rec_Qty * t.Distributer_Rate)+ (t.Rec_Pieces* t.DP_BaseRate))as value  from Trans_Stock_Updation_Details  t INNER JOIN Mas_Stockist S   " +
           " ON t.Stockist_code=S.Stockist_Code inner join Mas_Territory Tt on Tt.Territory_code=S.Territory_Code INNER JOIN Mas_Zone Z ON Z.Zone_code=Tt.Zone_code  right outer join Mas_Area a  ON a.Area_code=Z.Area_code  " +
     "where  t.Purchase_Date between '" + fromdate + "' and '" + todate + "'   and a.Area_Active_Flag=0 and s.Division_Code='" + div_code + "'  and subdivision_code='" + subdivision + "' and a.State_Code='" + statecode + "' " +
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
   public DataSet areawise_Trend_analysis_singleproduct(string product_code,string div_code, int fmonth, int fyear, string subdivision, string statecode,string areacode)
   {
       DB_EReporting db_ER = new DB_EReporting();

       DataSet dsAdmin = null;

       strQry = "select sum(t.Rec_Qty * t.Distributer_Rate)as value  from Trans_Stock_Updation_Details  t INNER JOIN Mas_Stockist S   " +
" ON t.Stockist_code=S.Stockist_Code inner join Mas_Territory Tt on Tt.Territory_code=S.Territory_Code INNER JOIN Mas_Zone Z ON Z.Zone_code=Tt.Zone_code  right outer join Mas_Area a  ON a.Area_code=Z.Area_code  " +
"where  month(Purchase_Date)='" + fmonth + "' and YEAR(Purchase_Date)='" + fyear + "'  and t.Product_Code='" + product_code + "' and  a.Area_code='" + areacode + "'  and a.Area_Active_Flag=0 and s.Division_Code='" + div_code + "'  and subdivision_code='" + subdivision + "' and a.State_Code='" + statecode + "' " +
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
   public DataSet areawise_Trend_analysis_singleproduct_total(string div_code, int fmonth, int fyear, string subdivision, string statecode, string areacode)
   {
       DB_EReporting db_ER = new DB_EReporting();

       DataSet dsAdmin = null;

       strQry = "select sum(t.Rec_Qty * t.Distributer_Rate)as value  from Trans_Stock_Updation_Details  t INNER JOIN Mas_Stockist S   " +
" ON t.Stockist_code=S.Stockist_Code inner join Mas_Territory Tt on Tt.Territory_code=S.Territory_Code INNER JOIN Mas_Zone Z ON Z.Zone_code=Tt.Zone_code  right outer join Mas_Area a  ON a.Area_code=Z.Area_code  " +
"where  month(Purchase_Date)='" + fmonth + "' and YEAR(Purchase_Date)='" + fyear + "'   and  a.Area_code='" + areacode + "'  and a.Area_Active_Flag=0 and s.Division_Code='" + div_code + "'  and subdivision_code='" + subdivision + "' and a.State_Code='" + statecode + "' " +
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
   public DataSet Zonewise_Trend_analysis_Distributor_descending(string div_code, string fromdate, string todate, string subdivision, string state_code,string area_code)
   {
       DB_EReporting db_ER = new DB_EReporting();

       DataSet dsAdmin = null;
 if (div_code != "4")
       {

       strQry = " select z.Zone_code,z.Zone_name, sum(t.Rec_Qty * t.Distributer_Rate)as value  from Trans_Stock_Updation_Details  t INNER JOIN Mas_Stockist S  "+ 
        " ON t.Stockist_code=S.Stockist_Code inner join Mas_Territory Tt on Tt.Territory_code=S.Territory_Code right outer JOIN Mas_Zone Z ON Z.Zone_code=Tt.Zone_code  inner join Mas_Area a  ON a.Area_code=Z.Area_code  "+
      " where  t.Purchase_Date between '"+fromdate+"' and '"+todate+"'   and a.Area_Active_Flag=0 and z.Zone_Active_Flag=0 and s.Division_Code='"+div_code+"'  and subdivision_code='"+subdivision+"' and a.State_Code='"+state_code+"' and a.Area_code='"+area_code+"' "+
      " group by  z.Zone_code,z.Zone_name order by value desc";
 }
       else
       {
           strQry = " select z.Zone_code,z.Zone_name, sum((t.Rec_Qty * t.Distributer_Rate)+ (t.Rec_Pieces* t.DP_BaseRate))as value  from Trans_Stock_Updation_Details  t INNER JOIN Mas_Stockist S  " +
          " ON t.Stockist_code=S.Stockist_Code inner join Mas_Territory Tt on Tt.Territory_code=S.Territory_Code right outer JOIN Mas_Zone Z ON Z.Zone_code=Tt.Zone_code  inner join Mas_Area a  ON a.Area_code=Z.Area_code  " +
        " where  t.Purchase_Date between '" + fromdate + "' and '" + todate + "'   and a.Area_Active_Flag=0 and z.Zone_Active_Flag=0 and s.Division_Code='" + div_code + "'  and subdivision_code='" + subdivision + "' and a.State_Code='" + state_code + "' and a.Area_code='" + area_code + "' " +
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

   public DataSet Zonewise_Trend_analysis_totalvalue(string div_code, string fromdate, string todate, string subdivision, string statecode,string area_code)
   {
       DB_EReporting db_ER = new DB_EReporting();

       DataSet dsAdmin = null;
if (div_code != "4")
       {
       strQry = " select  sum(t.Rec_Qty * t.Distributer_Rate)as value  from Trans_Stock_Updation_Details  t INNER JOIN Mas_Stockist S "+ 
       "  ON t.Stockist_code=S.Stockist_Code inner join Mas_Territory Tt on Tt.Territory_code=S.Territory_Code right outer JOIN Mas_Zone Z ON Z.Zone_code=Tt.Zone_code  inner join Mas_Area a  ON a.Area_code=Z.Area_code "+ 
     " where  t.Purchase_Date between '"+fromdate+"' and '"+todate+"'   and a.Area_Active_Flag=0 and z.Zone_Active_Flag=0 and s.Division_Code='"+div_code+"'  and subdivision_code='"+subdivision+"' and a.State_Code='"+statecode+"' and a.Area_code='"+area_code+"' "+
      "order by value desc";
  }
       else
       {
           strQry = " select  sum((t.Rec_Qty * t.Distributer_Rate)+ (t.Rec_Pieces* t.DP_BaseRate))as value  from Trans_Stock_Updation_Details  t INNER JOIN Mas_Stockist S " +
                    "  ON t.Stockist_code=S.Stockist_Code inner join Mas_Territory Tt on Tt.Territory_code=S.Territory_Code right outer JOIN Mas_Zone Z ON Z.Zone_code=Tt.Zone_code  inner join Mas_Area a  ON a.Area_code=Z.Area_code " +
                  " where  t.Purchase_Date between '" + fromdate + "' and '" + todate + "'   and a.Area_Active_Flag=0 and z.Zone_Active_Flag=0 and s.Division_Code='" + div_code + "'  and subdivision_code='" + subdivision + "' and a.State_Code='" + statecode + "' and a.Area_code='" + area_code + "' " +
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


   public DataSet Zonewise_Trend_analysis_totalvalue_singleprdt(string product_code,string div_code, int fmonth, int fyear, string subdivision, string statecode, string area_code,string zonecode)
   {
       DB_EReporting db_ER = new DB_EReporting();

       DataSet dsAdmin = null;

       strQry = " select  sum(t.Rec_Qty * t.Distributer_Rate)as value  from Trans_Stock_Updation_Details  t INNER JOIN Mas_Stockist S " +
       "  ON t.Stockist_code=S.Stockist_Code inner join Mas_Territory Tt on Tt.Territory_code=S.Territory_Code right outer JOIN Mas_Zone Z ON Z.Zone_code=Tt.Zone_code  inner join Mas_Area a  ON a.Area_code=Z.Area_code " +
     " where   month(Purchase_Date)='" + fmonth + "' and YEAR(Purchase_Date)='" + fyear + "'  and t.Product_Code='" + product_code + "' and  s.Division_Code='" + div_code + "'  and a.State_Code='" + statecode + "'    and a.Area_Active_Flag=0 and z.Zone_Active_Flag=0 and s.Division_Code='" + div_code + "'  and subdivision_code='" + subdivision + "' and  a.Area_code='" + area_code + "'and z.Zone_code='"+zonecode+"' " +
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
   public DataSet Zonewise_Trend_analysis_totalvalue_singleprdt_total(string div_code, int fmonth, int fyear, string subdivision, string statecode, string area_code, string zonecode)
   {
       DB_EReporting db_ER = new DB_EReporting();

       DataSet dsAdmin = null;

       strQry = " select  sum(t.Rec_Qty * t.Distributer_Rate)as value  from Trans_Stock_Updation_Details  t INNER JOIN Mas_Stockist S " +
       "  ON t.Stockist_code=S.Stockist_Code inner join Mas_Territory Tt on Tt.Territory_code=S.Territory_Code right outer JOIN Mas_Zone Z ON Z.Zone_code=Tt.Zone_code  inner join Mas_Area a  ON a.Area_code=Z.Area_code " +
     " where   month(Purchase_Date)='" + fmonth + "' and YEAR(Purchase_Date)='" + fyear + "'  and   s.Division_Code='" + div_code + "'  and a.State_Code='" + statecode + "'    and a.Area_Active_Flag=0 and z.Zone_Active_Flag=0 and s.Division_Code='" + div_code + "'  and subdivision_code='" + subdivision + "' and  a.Area_code='" + area_code + "'and z.Zone_code='" + zonecode + "' " +
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
   public DataSet Territorywise_Trend_analysis_Distributor_descending(string div_code, string fromdate, string todate, string subdivision, string state_code, string area_code,string zonecode)
   {
       DB_EReporting db_ER = new DB_EReporting();

       DataSet dsAdmin = null;

       strQry = " select Tt.Territory_code,Tt.Territory_Name, sum(t.Rec_Qty * t.Distributer_Rate)as value  from Trans_Stock_Updation_Details  t INNER JOIN Mas_Stockist S  "+
       "  ON t.Stockist_code=S.Stockist_Code RIGHT OUTER join Mas_Territory Tt on Tt.Territory_code=S.Territory_Code INNER JOIN Mas_Zone Z ON Z.Zone_code=Tt.Zone_code  inner join Mas_Area a  ON a.Area_code=Z.Area_code "+ 
    "where  t.Purchase_Date between '"+ fromdate+"' and '"+todate+"'   and a.Area_Active_Flag=0 and z.Zone_Active_Flag=0 and s.Division_Code='"+ div_code+"'  and subdivision_code='"+subdivision+"' and a.State_Code='"+state_code+"' and a.Area_code='"+area_code+"' AND TT.Zone_code='"+zonecode+"' "+
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

   public DataSet Territorywise_Trend_analysis_totalvalue(string div_code, string fromdate, string todate, string subdivision, string statecode, string area_code,string zonecode)
   {
       DB_EReporting db_ER = new DB_EReporting();

       DataSet dsAdmin = null;
 if (div_code != "4")
{
       strQry = " select  sum(t.Rec_Qty * t.Distributer_Rate)as value  from Trans_Stock_Updation_Details  t INNER JOIN Mas_Stockist S  "+ 
        " ON t.Stockist_code=S.Stockist_Code RIGHT OUTER join Mas_Territory Tt on Tt.Territory_code=S.Territory_Code INNER JOIN Mas_Zone Z ON Z.Zone_code=Tt.Zone_code  inner join Mas_Area a  ON a.Area_code=Z.Area_code  "+
     " where  t.Purchase_Date between '"+ fromdate+"' and '"+todate+"'   and a.Area_Active_Flag=0 and z.Zone_Active_Flag=0 and s.Division_Code='"+div_code+"'  and subdivision_code='"+subdivision+"' and a.State_Code='"+statecode+"' and a.Area_code='"+area_code+"' AND TT.Zone_code='"+zonecode+"' "+
      " order by value desc";
 }
       else
       {
 strQry = " select  sum((t.Rec_Qty * t.Distributer_Rate)+ (t.Rec_Pieces* t.DP_BaseRate))as value  from Trans_Stock_Updation_Details  t INNER JOIN Mas_Stockist S  "+ 
        " ON t.Stockist_code=S.Stockist_Code RIGHT OUTER join Mas_Territory Tt on Tt.Territory_code=S.Territory_Code INNER JOIN Mas_Zone Z ON Z.Zone_code=Tt.Zone_code  inner join Mas_Area a  ON a.Area_code=Z.Area_code  "+
     " where  t.Purchase_Date between '"+ fromdate+"' and '"+todate+"'   and a.Area_Active_Flag=0 and z.Zone_Active_Flag=0 and s.Division_Code='"+div_code+"'  and subdivision_code='"+subdivision+"' and a.State_Code='"+statecode+"' and a.Area_code='"+area_code+"' AND TT.Zone_code='"+zonecode+"' "+
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


   public DataSet Territorywise_Trend_analysis_totalvalue_singleprt(string product_code, string div_code, int fmonth, int fyear, string subdivision, string statecode, string area_code, string zonecode,string territory_code)
   {
       DB_EReporting db_ER = new DB_EReporting();

       DataSet dsAdmin = null;

       strQry = " select  sum(t.Rec_Qty * t.Distributer_Rate)as value  from Trans_Stock_Updation_Details  t INNER JOIN Mas_Stockist S  " +
        " ON t.Stockist_code=S.Stockist_Code RIGHT OUTER join Mas_Territory Tt on Tt.Territory_code=S.Territory_Code INNER JOIN Mas_Zone Z ON Z.Zone_code=Tt.Zone_code  inner join Mas_Area a  ON a.Area_code=Z.Area_code  " +
     " where   month(Purchase_Date)='" + fmonth + "' and YEAR(Purchase_Date)='" + fyear + "' and t.Product_Code='" + product_code + "'  and a.Area_Active_Flag=0 and z.Zone_Active_Flag=0 and s.Division_Code='" + div_code + "'  and subdivision_code='" + subdivision + "' and a.State_Code='" + statecode + "' and a.Area_code='" + area_code + "' AND TT.Zone_code='" + zonecode + "' and s.Territory_Code='" + territory_code + "' " +
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

   public DataSet Territorywise_Trend_analysis_totalvalue_singleprt_total(string div_code, int fmonth, int fyear, string subdivision, string statecode, string area_code, string zonecode, string territory_code)
   {
       DB_EReporting db_ER = new DB_EReporting();

       DataSet dsAdmin = null;

       strQry = " select  sum(t.Rec_Qty * t.Distributer_Rate)as value  from Trans_Stock_Updation_Details  t INNER JOIN Mas_Stockist S  " +
        " ON t.Stockist_code=S.Stockist_Code RIGHT OUTER join Mas_Territory Tt on Tt.Territory_code=S.Territory_Code INNER JOIN Mas_Zone Z ON Z.Zone_code=Tt.Zone_code  inner join Mas_Area a  ON a.Area_code=Z.Area_code  " +
     " where   month(Purchase_Date)='" + fmonth + "' and YEAR(Purchase_Date)='" + fyear + "'  and a.Area_Active_Flag=0 and z.Zone_Active_Flag=0 and s.Division_Code='" + div_code + "'  and subdivision_code='" + subdivision + "' and a.State_Code='" + statecode + "' and a.Area_code='" + area_code + "' AND TT.Zone_code='" + zonecode + "' and s.Territory_Code='" + territory_code + "' " +
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
// public DataSet Gettopexception_statewise(string divcode, string year,string state_code)
public DataSet Gettopexception_statewise(string divcode, string year, string state_code, string sf_code)
   {
       DB_EReporting db_ER = new DB_EReporting();

       DataSet dsSF = null;
//if (divcode != "4")
  //     {
 //      strQry = " select st.state_code,st.statename,st.shortname ,sum(t.Rec_Qty * t.Distributer_Rate)as value  from Trans_Stock_Updation_Details  t INNER JOIN Mas_Stockist S "+
 //    "ON t.Stockist_code=S.Stockist_Code right outer join mas_state st on st.State_Code=s.State_Code  "+
 //     "where year( t.Purchase_Date)='"+year+"'  and   st.state_code in (" + state_code + ") and st.State_Active_Flag=0 and s.Division_Code='" + divcode + "' " +
 //     "group by  st.state_code,st.statename,st.shortname order by value desc";

//  }
 //      else
 //      {
//strQry = " select st.state_code,st.statename,st.shortname ,sum((t.Rec_Qty * t.Distributer_Rate)+ (t.Rec_Pieces* t.DP_BaseRate))as value   from Trans_Stock_Updation_Details  t INNER JOIN Mas_Stockist S "+
//     "ON t.Stockist_code=S.Stockist_Code right outer join mas_state st on st.State_Code=s.State_Code  "+
//      "where year( t.Purchase_Date)='"+year+"'  and   st.state_code in (" + state_code + ") and st.State_Active_Flag=0 and s.Division_Code='" + divcode + "' " +
//      "group by  st.state_code,st.statename,st.shortname order by value desc";
//}

  strQry = "exec GET_PURCHASE_EXCEPTION_STATE '" + divcode + "','','" + sf_code + "','" + year + "','" + state_code + "'";
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
   public DataSet Gettopexception_statewise_total(string divcode, string year,string state_code)
   {
       DB_EReporting db_ER = new DB_EReporting();

       DataSet dsSF = null;
if (divcode != "4")
       {
       strQry = " select sum(t.Rec_Qty * t.Distributer_Rate)as value  from Trans_Stock_Updation_Details  t INNER JOIN Mas_Stockist S " +
    "ON t.Stockist_code=S.Stockist_Code right outer join mas_state st on st.State_Code=s.State_Code " +
     "where year( t.Purchase_Date)='" + year + "'  and   st.state_code in (" + state_code + ") and st.State_Active_Flag=0 and s.Division_Code='" + divcode + "' ";
}else
       {
       strQry = " select sum((t.Rec_Qty * t.Distributer_Rate)+ (t.Rec_Pieces* t.DP_BaseRate))as value from Trans_Stock_Updation_Details  t INNER JOIN Mas_Stockist S " +
    "ON t.Stockist_code=S.Stockist_Code right outer join mas_state st on st.State_Code=s.State_Code " +
     "where year( t.Purchase_Date)='" + year + "'  and   st.state_code in (" + state_code + ") and st.State_Active_Flag=0 and s.Division_Code='" + divcode + "' ";
}
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


//   public DataSet Gettopexception_areawise(string divcode, string year, string state_code)
 public DataSet Gettopexception_areawise(string divcode, string year, string state_code, string sf_code)
   {
       DB_EReporting db_ER = new DB_EReporting();

       DataSet dsSF = null;
    //   if (divcode != "4")
    //   {
    //       strQry = "select a.Area_code,a.Area_name,sum(t.Rec_Qty * t.Distributer_Rate)as value  from Trans_Stock_Updation_Details  t INNER JOIN Mas_Stockist S  " +
    //      "   ON t.Stockist_code=S.Stockist_Code inner join Mas_Territory Tt on Tt.Territory_code=S.Territory_Code INNER JOIN Mas_Zone Z ON Z.Zone_code=Tt.Zone_code  right outer join Mas_Area a  ON a.Area_code=Z.Area_code  " +
    //     "    where year( t.Purchase_Date)='" + year + "'   and a.Area_Active_Flag=0 and s.Division_Code='" + divcode + "'  and a.State_Code in (" + state_code + ") " +
   //       " group by  a.Area_code,a.Area_name order by value desc ";
   //    }
   //    else
   //    {
     //      strQry = "select a.Area_code,a.Area_name,sum((t.Rec_Qty * t.Distributer_Rate)+ (t.Rec_Pieces* t.DP_BaseRate))as value  from Trans_Stock_Updation_Details  t INNER JOIN Mas_Stockist S  " +
   //             "   ON t.Stockist_code=S.Stockist_Code inner join Mas_Territory Tt on Tt.Territory_code=S.Territory_Code INNER JOIN Mas_Zone Z ON Z.Zone_code=Tt.Zone_code  right outer join Mas_Area a  ON a.Area_code=Z.Area_code  " +
   //            "    where year( t.Purchase_Date)='" + year + "'   and a.Area_Active_Flag=0 and s.Division_Code='" + divcode + "'  and a.State_Code in (" + state_code + ") " +
   //             " group by  a.Area_code,a.Area_name order by value desc ";

   //    }

  strQry = "exec GET_PURCHASE_EXCEPTION_AREA '" + divcode + "','','" + sf_code + "','" + state_code + "','" + year + "'";
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
   public DataSet Gettopexception_areawise_total(string divcode, string year, string state_code)
   {
       DB_EReporting db_ER = new DB_EReporting();

       DataSet dsSF = null;
if (divcode != "4")
       {
       strQry = "select sum(t.Rec_Qty * t.Distributer_Rate)as value  from Trans_Stock_Updation_Details  t INNER JOIN Mas_Stockist S  " +
      "   ON t.Stockist_code=S.Stockist_Code inner join Mas_Territory Tt on Tt.Territory_code=S.Territory_Code INNER JOIN Mas_Zone Z ON Z.Zone_code=Tt.Zone_code  right outer join Mas_Area a  ON a.Area_code=Z.Area_code  " +
     "    where year( t.Purchase_Date)='" + year + "'   and a.Area_Active_Flag=0 and s.Division_Code='" + divcode + "'  and a.State_Code in (" + state_code + ")" ;
}
else
       {
  strQry = "select sum((t.Rec_Qty * t.Distributer_Rate)+ (t.Rec_Pieces* t.DP_BaseRate))as value  from Trans_Stock_Updation_Details  t INNER JOIN Mas_Stockist S  " +
      "   ON t.Stockist_code=S.Stockist_Code inner join Mas_Territory Tt on Tt.Territory_code=S.Territory_Code INNER JOIN Mas_Zone Z ON Z.Zone_code=Tt.Zone_code  right outer join Mas_Area a  ON a.Area_code=Z.Area_code  " +
     "    where year( t.Purchase_Date)='" + year + "'   and a.Area_Active_Flag=0 and s.Division_Code='" + divcode + "'  and a.State_Code in (" + state_code + ")" ;
    
}
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


//   public DataSet Gettopexception_zonewise(string divcode, string year, string state_code)
public DataSet Gettopexception_zonewise(string divcode, string year, string state_code,string sf_code)
   {
       DB_EReporting db_ER = new DB_EReporting();

       DataSet dsSF = null;

//if (divcode != "4")
 //      {

//       strQry = " select z.Zone_code,z.Zone_name, sum(t.Rec_Qty * t.Distributer_Rate)as value  from Trans_Stock_Updation_Details  t INNER JOIN Mas_Stockist S  " +
//        " ON t.Stockist_code=S.Stockist_Code inner join Mas_Territory Tt on Tt.Territory_code=S.Territory_Code right outer JOIN Mas_Zone Z ON Z.Zone_code=Tt.Zone_code  inner join Mas_Area a  ON a.Area_code=Z.Area_code  " +
 //     " where  year( t.Purchase_Date)='" + year + "'    and a.Area_Active_Flag=0 and z.Zone_Active_Flag=0 and s.Division_Code='" + divcode + "'  and a.State_Code in (" + state_code + ")  " +
 //     " group by  z.Zone_code,z.Zone_name order by value desc";
//}

// else
//       {
// strQry = " select z.Zone_code,z.Zone_name, sum((t.Rec_Qty * t.Distributer_Rate)+ (t.Rec_Pieces* t.DP_BaseRate))as value  from Trans_Stock_Updation_Details  t INNER JOIN Mas_Stockist S  " +
//        " ON t.Stockist_code=S.Stockist_Code inner join Mas_Territory Tt on Tt.Territory_code=S.Territory_Code right outer JOIN Mas_Zone Z ON Z.Zone_code=Tt.Zone_code  inner join Mas_Area a  ON a.Area_code=Z.Area_code  " +
 //     " where  year( t.Purchase_Date)='" + year + "'    and a.Area_Active_Flag=0 and z.Zone_Active_Flag=0 and s.Division_Code='" + divcode + "'  and a.State_Code in (" + state_code + ")  " +
 //     " group by  z.Zone_code,z.Zone_name order by value desc";
//}
 strQry = "exec GET_PURCHASE_EXCEPTION_ZONE '" + divcode + "','','" + sf_code + "','" + year + "','" + state_code + "'";
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
   public DataSet Gettopexception_zonewise_total(string divcode, string year, string state_code)
   {
       DB_EReporting db_ER = new DB_EReporting();

       DataSet dsSF = null;

if (divcode != "4")
       {
       strQry = " select sum(t.Rec_Qty * t.Distributer_Rate)as value  from Trans_Stock_Updation_Details  t INNER JOIN Mas_Stockist S  " +
         " ON t.Stockist_code=S.Stockist_Code inner join Mas_Territory Tt on Tt.Territory_code=S.Territory_Code right outer JOIN Mas_Zone Z ON Z.Zone_code=Tt.Zone_code  inner join Mas_Area a  ON a.Area_code=Z.Area_code  " +
       " where  year( t.Purchase_Date)='" + year + "'    and a.Area_Active_Flag=0 and z.Zone_Active_Flag=0 and s.Division_Code='" + divcode + "'  and a.State_Code in (" + state_code + ")  ";
     }
else
       {
       strQry = " select sum((t.Rec_Qty * t.Distributer_Rate)+ (t.Rec_Pieces* t.DP_BaseRate))as value  from Trans_Stock_Updation_Details  t INNER JOIN Mas_Stockist S  " +
         " ON t.Stockist_code=S.Stockist_Code inner join Mas_Territory Tt on Tt.Territory_code=S.Territory_Code right outer JOIN Mas_Zone Z ON Z.Zone_code=Tt.Zone_code  inner join Mas_Area a  ON a.Area_code=Z.Area_code  " +
       " where  year( t.Purchase_Date)='" + year + "'    and a.Area_Active_Flag=0 and z.Zone_Active_Flag=0 and s.Division_Code='" + divcode + "'  and a.State_Code in (" + state_code + ")  ";

}
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

   //public DataSet Gettopexception_territorywise(string divcode, string year, string state_code)
public DataSet Gettopexception_territorywise(string divcode, string year, string state_code, string sf_code)
   {
       DB_EReporting db_ER = new DB_EReporting();

       DataSet dsSF = null;
//if (divcode != "4")
  //     {
   //    strQry = " select Tt.Territory_code,Tt.Territory_Name, sum(t.Rec_Qty * t.Distributer_Rate)as value  from Trans_Stock_Updation_Details  t INNER JOIN Mas_Stockist S  " +
  //    "  ON t.Stockist_code=S.Stockist_Code RIGHT OUTER join Mas_Territory Tt on Tt.Territory_code=S.Territory_Code INNER JOIN Mas_Zone Z ON Z.Zone_code=Tt.Zone_code  inner join Mas_Area a  ON a.Area_code=Z.Area_code " +
//   "where   year( t.Purchase_Date)='" + year + "'    and a.Area_Active_Flag=0 and z.Zone_Active_Flag=0 and s.Division_Code='" + divcode + "'  and a.State_Code in (" + state_code + ") " +
//   " group by  Tt.Territory_code,Tt.Territory_Name order by value desc";
//}else
//       {
// strQry = " select Tt.Territory_code,Tt.Territory_Name, sum((t.Rec_Qty * t.Distributer_Rate)+ (t.Rec_Pieces* t.DP_BaseRate))as value from Trans_Stock_Updation_Details  t INNER JOIN Mas_Stockist S  " +
//      "  ON t.Stockist_code=S.Stockist_Code RIGHT OUTER join Mas_Territory Tt on Tt.Territory_code=S.Territory_Code INNER JOIN Mas_Zone Z ON Z.Zone_code=Tt.Zone_code  inner join Mas_Area a  ON a.Area_code=Z.Area_code " +
//   "where   year( t.Purchase_Date)='" + year + "'    and a.Area_Active_Flag=0 and z.Zone_Active_Flag=0 and s.Division_Code='" + divcode + "'  and a.State_Code in (" + state_code + ") " +
//   " group by  Tt.Territory_code,Tt.Territory_Name order by value desc";
//}

  strQry = "exec GET_PURCHASE_EXCEPTION_TERRITORY    '" + divcode + "','','" + sf_code + "','" + year + "','" + state_code + "'";
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
   public DataSet Gettopexception_territorywise_total(string divcode, string year, string state_code)
   {
       DB_EReporting db_ER = new DB_EReporting();

       DataSet dsSF = null;

if (divcode != "4")
       {
       strQry = " select sum(t.Rec_Qty * t.Distributer_Rate)as value  from Trans_Stock_Updation_Details  t INNER JOIN Mas_Stockist S  " +
       "  ON t.Stockist_code=S.Stockist_Code RIGHT OUTER join Mas_Territory Tt on Tt.Territory_code=S.Territory_Code INNER JOIN Mas_Zone Z ON Z.Zone_code=Tt.Zone_code  inner join Mas_Area a  ON a.Area_code=Z.Area_code " +
    "where   year( t.Purchase_Date)='" + year + "'    and a.Area_Active_Flag=0 and z.Zone_Active_Flag=0 and s.Division_Code='" + divcode + "'  and a.State_Code in (" + state_code + ") ";
 }else
       {
 strQry = " select sum((t.Rec_Qty * t.Distributer_Rate)+ (t.Rec_Pieces* t.DP_BaseRate))as value  from Trans_Stock_Updation_Details  t INNER JOIN Mas_Stockist S  " +
       "  ON t.Stockist_code=S.Stockist_Code RIGHT OUTER join Mas_Territory Tt on Tt.Territory_code=S.Territory_Code INNER JOIN Mas_Zone Z ON Z.Zone_code=Tt.Zone_code  inner join Mas_Area a  ON a.Area_code=Z.Area_code " +
    "where   year( t.Purchase_Date)='" + year + "'    and a.Area_Active_Flag=0 and z.Zone_Active_Flag=0 and s.Division_Code='" + divcode + "'  and a.State_Code in (" + state_code + ") ";
 
}

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
