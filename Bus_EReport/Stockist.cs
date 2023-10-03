using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DBase_EReport;

namespace Bus_EReport
{
    public class Stockist
    {
        private string strQry = string.Empty;

        public DataSet getSalesforce(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSalesforce = null;
            //strQry = " SELECT sf_code,Sf_Name " + 
            //         " FROM mas_salesforce  " +                                        
            //         " WHERE Division_Code='" + divcode + "'  AND SF_Status = 0 AND sf_type = 1   "; 

            //strQry = "SELECT sf_code,sf_Name,sf_hq,Reporting_To_SF, " +
            //        " (Select Sf_HQ from mas_salesforce where sf_code = a.Reporting_To_SF)HQ " +
            //        " FROM Mas_Salesforce a " +
            //        " WHERE (Division_Code like '" + divcode + ',' + "%'  or " +
            //         " Division_Code like '%" + ',' + divcode + ',' + "%') AND SF_Status = 0 AND sf_type = 1  ";

            strQry = "SELECT sf_code,sf_Name + ' - ' + sf_Designation_Short_Name + ' - ' +Sf_HQ as sf_Name,sf_hq,Reporting_To_SF, " +
                    " (Select Sf_HQ from mas_salesforce where sf_code = a.Reporting_To_SF)HQ " +
                    " FROM Mas_Salesforce a " +
                    " WHERE (Division_Code like '" + divcode + ',' + "%'  or " +
                     " Division_Code like '%" + ',' + divcode + ',' + "%') AND SF_Status = 0 AND sf_TP_Active_Flag=0 and  sf_type = 1  ";



            //SELECT sf_code,Sf_Name,Reporting_To_SF, (select Sf_HQ from mas_salesforce
            //    where sf_code = a.Reporting_To_SF)HQ  FROM mas_salesforce a WHERE Division_Code='28'
            //AND SF_Status = 0 AND sf_type = 1    order by HQ



            //   " order by sf_code";
            try
            {
                dsSalesforce = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsSalesforce;
        }
        // SS sale Entry 
        public DataSet getStockist_Name_SE()
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsStockist = null;
            strQry = " SELECT '' as stockist_code, ' --- Select --- ' as Stockist_Name " +
                      " UNION " +
                " select Stockist_Code,Stockist_Name  from Mas_Stockist_FM where Sale_Entry=1   ";
            //  " where sf_TP_Active_Flag in (0,2)  and Sf_Code in " +
            //  " (select TP_Reporting_SF from Mas_Salesforce where sf_code != 'admin' and Division_Code = '" + div_code + "' ) " +
            // " order by Sf_Name ";

            try
            {
                dsStockist = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsStockist;
        }
        public DataSet getStockistCreate_StockistName_SE()
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;

            /*strQry = " SELECT stockist_code,Stockist_Name FROM mas_stockist a " +
                     " WHERE a.Division_Code='" + divcode + "' AND Stockist_Code='" + stockist_code + "'  " +
                     " order by 2"; */

            strQry = " SELECT '' as stockist_code, ' --- Select --- ' as Stockist_Name " +
                      " UNION " +
                "SELECT stockist_code,Stockist_Name " +
                   " FROM mas_Stockist_FM " +
                   " WHERE Sale_Entry=1 ";
            //  " AND Division_Code= '" + divcode + "' " +
            //  " AND stockist_code = '" + stockist_code + "'  ";
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
        public DataSet getStockist_Reporting(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSalesforce = null;
            strQry = " SELECT '' as sf_code, ' --- Select --- ' as Sf_Name " +
                      " UNION " +
                      " select Sf_Code,Sf_Name  from mas_salesforce " +
                      " where sf_TP_Active_Flag in (0,2)  and Sf_Code in " +
                      " (select TP_Reporting_SF from Mas_Salesforce where sf_code != 'admin' AND (Division_Code like '" + div_code + ',' + "%'  or " +
                      " Division_Code like '%" + ',' + div_code + ',' + "%')) ";
            //" (select TP_Reporting_SF from Mas_Salesforce where sf_code != 'admin' and Division_Code = '" + div_code + "' ) " +
            //" order by Sf_Name ";

            try
            {
                dsSalesforce = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsSalesforce;
        }
public DataSet Ord_Product_Daywise(string str3, string div_code, string str1, string str2)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsStockDet = null;
            strQry = " EXEC [dbo].[Ord_Product_Daywise] '" + str3 + "', '" + div_code + "',' " + str1 + "','" + str2 + "'";

            try
            {
                dsStockDet = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsStockDet;

        }
        public DataSet Visit_Daywise(string str3, string div_code, string str1, string str2)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsStockDet = null;
            strQry = " EXEC [dbo].[Visit_Daywise] '" + str3 + "', '" + div_code + "',' " + str1 + "','" + str2 + "'";

            try
            {
                dsStockDet = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsStockDet;

        }
        public DataSet Sales_Brandwise(string str3, string div_code, string str1, string str2, string drcd)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsStockDet = null;
            strQry = " EXEC [dbo].[Sales_Brandwise] '" + str3 + "', '" + div_code + "',' " + str1 + "','" + str2 + "','" + drcd + "'";

            try
            {
                dsStockDet = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsStockDet;

        }
		public DataSet drcnt(string str3, string div_code, string str1, string str2)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsStockDet = null;
            strQry = " EXEC [dbo].[GetDrDetails] '" + str3 + "', '" + div_code + "',' " + str1 + "','" + str2 + "'";

            try
            {
                dsStockDet = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsStockDet;

        }
		public DataSet drcntc(string str3, string div_code, string str1, string str2, string shc)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsStockDet = null;
            strQry = " EXEC [dbo].[GetDrDetailsfilt] '" + str3 + "', '" + div_code + "',' " + str1 + "','" + str2 + "','" + shc + "'";

            try
            {
                dsStockDet = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsStockDet;

        }
         public DataSet TowngetSubDiv(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSubDiv = null;

           
            string strQry = " SELECT '' as Town_code, '--Select--' as Town_name " +
                           " UNION " +
                           " select a.Town_code,a.Town_name From Mas_Town a WHERE a.Town_Active_Flag=0 and " +
                            " a.Div_Code= '" + divcode + "' order by 2";
            try
            {
                dsSubDiv = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsSubDiv;
        }
        public DataSet Ord_Product_Daywisec(string str3, string div_code, string str1, string str2)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsStockDet = null;
            strQry = " EXEC [dbo].[Ord_Product_Daywisec] '" + str3 + "', '" + div_code + "',' " + str1 + "','" + str2 + "'";

            try
            {
                dsStockDet = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsStockDet;

        }
       
        // Fieldforce stockist entry Map
        public DataSet getStockist_Name(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsStockist = null;
            strQry = " SELECT '' as stockist_code, ' --- Select the Stockist --- ' as Stockist_Name, '-1' as  State_Code,'' Username" +
                    " UNION " +
                      " select Stockist_Code,Stockist_Name,State_Code,Username from mas_Stockist where stockist_active_flag=0 AND Division_Code = '" + div_code + "' order by 2  ";
            //  " where sf_TP_Active_Flag in (0,2)  and Sf_Code in " +
            //  " (select TP_Reporting_SF from Mas_Salesforce where sf_code != 'admin' and Division_Code = '" + div_code + "' ) " +
            // " order by Sf_Name ";

            try
            {
                dsStockist = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsStockist;
        }
        public DataSet getStockistCreate_StockistName(string divcode, string stockist_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;

            /*strQry = " SELECT stockist_code,Stockist_Name FROM mas_stockist a " +
                     " WHERE a.Division_Code='" + divcode + "' AND Stockist_Code='" + stockist_code + "'  " +
                     " order by 2"; */

            strQry = "SELECT stockist_code,Stockist_Name,Stockist_Address,Stockist_ContactPerson,Stockist_Designation,Stockist_Mobile, SF_Code " +
                   " FROM mas_Stockist " +
                   " WHERE stockist_active_flag=0 " +
                   " AND Division_Code= '" + divcode + "' " +
                   " AND stockist_code = '" + stockist_code + "'  ";
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
        // Alphabet order
        public DataSet getS_C_A(string divcode, string sAlpha)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;

            /*strQry = " SELECT stockist_code,Stockist_Name FROM mas_stockist a " +
                     " WHERE a.Division_Code='" + divcode + "' AND Stockist_Code='" + stockist_code + "'  " +
                     " order by 2"; */

            strQry = "SELECT stockist_code,Stockist_Name,Stockist_Address,Stockist_ContactPerson,Stockist_Designation,Stockist_Mobile, SF_Code " +
                   " FROM mas_Stockist " +
                   " WHERE stockist_active_flag=0 " +
                   " AND Division_Code= '" + divcode + "' " +
                   " AND LEFT(Stockist_Name,1) = '" + sAlpha + "' ";
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
        public DataSet getSalesForcelist_Alphabet(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            strQry = "select '1' val,'All' stockist_name " +
                     " union " +
                     " select distinct LEFT(stockist_name,1) val, LEFT(stockist_name,1) stockist_name" +
                     " FROM mas_stockist " +
                     " WHERE stockist_active_flag=0 " +
                     " AND Division_Code = '" + divcode + "' " +
                     " ORDER BY 1";
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
        public DataSet getStockist_N(string ddltext)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;


            strQry = "SELECT stockist_code,Stockist_Name,Stockist_Address,Stockist_ContactPerson,Stockist_Designation,Stockist_Mobile,Territory, SF_Code " +
                    " FROM mas_Stockist " +
                    " WHERE stockist_active_flag=0 " +
                //  " AND Division_Code= '" + divcode + "' " + 
                    " AND Stockist_Name = '" + ddltext + "'  ";


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
        // alphbet
        public DataSet getStockist_Alphabet(string ddlvar)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;

            //strQry = "select '1' val,'All' stockist_name " +
            //         " union " +
            //         " select distinct LEFT(Stockist_Code,stockist_name,1) val, LEFT(Stockist_Code,stockist_name,1) stockist_name" +
            //       //  "SELECT Stockist_Code,Stockist_Name " +
            //         " FROM mas_Stockist " +
            //       " WHERE stockist_active_flag=0 " +
            //    //  " AND Division_Code= '" + divcode + "' " +
            //       " AND LEFT(Stockist_Name,1) = '" + ddlvar + "' ";


            strQry = " SELECT '' as stockist_code, ' --- Select the Stockist --- ' as Stockist_Name " +
                      " UNION " +
                     "SELECT Stockist_Code,Stockist_Name " +
                    " FROM mas_Stockist " +
                    " WHERE stockist_active_flag=0 " +
                //  " AND Division_Code= '" + divcode + "' " +
                    " AND LEFT(Stockist_Name,1) = '" + ddlvar + "' ";
            //" AND Stockist_Name Like % '" + ddlvar + "' %  ";


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
        public DataSet getStockist_Alphabet_N()
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;

            //strQry = "select '1' val,'All' stockist_name " +
            //         " union " +
            //         " select distinct LEFT(Stockist_Code,stockist_name,1) val, LEFT(Stockist_Code,stockist_name,1) stockist_name" +
            //       //  "SELECT Stockist_Code,Stockist_Name " +
            //         " FROM mas_Stockist " +
            //       " WHERE stockist_active_flag=0 " +
            //    //  " AND Division_Code= '" + divcode + "' " +
            //       " AND LEFT(Stockist_Name,1) = '" + ddlvar + "' ";


            strQry = " SELECT '' as stockist_code, ' --- Select the Stockist --- ' as Stockist_Name " +
                      " UNION " +
                    "SELECT Stockist_Code,Stockist_Name " +
                    " FROM mas_Stockist " +
                    " WHERE stockist_active_flag=0 ";
            //  " AND Division_Code= '" + divcode + "' " +
            //" AND LEFT(Stockist_Name,1) = '" + ddlvar + "' ";
            //" AND Stockist_Name Like % '" + ddlvar + "' %  ";


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

        public DataSet getStockist_View(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;



            //strQry = "SELECT a.stockist_code,a.Stockist_Name,a.Stockist_Address,a.Stockist_Mobile,Sf_Name " +
            //         " FROM mas_Stockist a " +
            //         " inner join Mas_Stockist_FM b On a.Stockist_Code=b.Stockist_Code inner join Mas_Salesforce sfc on b.SF_Code=sfc.Sf_Code" +
            //         " WHERE a.stockist_active_flag=0 " +
            //         " AND a.Division_Code= '" + divcode + "' " +
            //         " order by stockist_code ";           
            strQry = " SELECT a.stockist_code,a.Stockist_Name,a.Stockist_Address,a.Stockist_ContactPerson,a.Stockist_Designation," +
                     " a.Stockist_Mobile,a.Territory, a.SF_Code, " +
                     " stuff((select ', '+SF_Name from Mas_Salesforce b where charindex(b.Sf_Code+',',a.SF_Code)>0 for XML path('')),1,2,'') sfName " +
                      " FROM mas_stockist a where  stockist_active_flag=0 AND a.Division_Code= '" + divcode + "' ";

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

        public DataSet getStockistCreate_Reporting(string divcode, string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            if (sf_code != "admin")
            {
                //strQry = " SELECT sf_code,Sf_Name,Sf_Hq FROM mas_salesforce a " +
                //         " WHERE (a.Division_Code like '" + divcode + ',' + "%'  or " +
                //         " a.Division_Code like '%" + ',' + divcode + ',' + "%')  AND SF_Status = 0 AND lower(sf_code) != 'admin' AND sf_type = 1 AND a.TP_Reporting_SF = '" + sf_code + "'" +
                //         " AND a.SF_Status = 0 AND a.sf_Tp_Active_flag = 0 " +
                //         " order by Sf_Hq";

                strQry = " SELECT sf_code,Sf_Name,Sf_Hq , " +
                         " (select sf_name from mas_salesforce where sf_code=a.Reporting_To_SF) as Reporting_To_SF," +
                         "(select sf_hq from mas_salesforce where sf_code=a.Reporting_To_SF) as HQ FROM mas_salesforce a " +
                         " WHERE (a.Division_Code like '" + divcode + ',' + "%'  or " +
                         " a.Division_Code like '%" + ',' + divcode + ',' + "%')  AND SF_Status = 0 AND lower(sf_code) != 'admin' AND sf_type = 1 AND a.TP_Reporting_SF = '" + sf_code + "'" +
                         " AND a.SF_Status = 0 AND a.sf_Tp_Active_flag = 0 " +
                         " order by Sf_Hq";
            }
            else
            {
                //strQry = " Select Sf_code,Sf_Name,Sf_Hq From mas_salesforce a" +
                //         " where (a.Division_Code like '" + divcode + ',' + "%'  or " +
                //         " a.Division_Code like '%" + ',' + divcode + ',' + "%') and SF_Status = 0 And lower(sf_code) != 'admin' And sf_type =1 " +
                //         " And a.Sf_status = 0 and a.sf_Tp_Active_flag = 0" +
                //         " order by Sf_Hq";

                strQry = " Select Sf_code,Sf_Name,Sf_Hq, " +
                         " (select sf_name from mas_salesforce where sf_code=a.Reporting_To_SF) as Reporting_To_SF," +
                         " (select sf_hq from mas_salesforce where sf_code=a.Reporting_To_SF) as HQ From mas_salesforce a" +
                         " where (a.Division_Code like '" + divcode + ',' + "%'  or " +
                         " a.Division_Code like '%" + ',' + divcode + ',' + "%') and SF_Status = 0 And lower(sf_code) != 'admin' And sf_type =1 " +
                         " And a.Sf_status = 0 and a.sf_Tp_Active_flag = 0" +
                         " order by Sf_Hq";
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
        // I want to use list screen Gridview Option using select from Database           
        public DataSet getStockist(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsStockist = null;
            //strQry = "SELECT a.User_Entry_Code,a.Stockist_Code,a.Stockist_Name,a.Stockist_ContactPerson,a.Stockist_Mobile,a.Territory,a.Distributor_Code,a.Field_Name, " +
            //         "COUNT(z.DSM_name) as 'Sub_Coun' from Mas_DSM z full join "+
            ////         "mas_stockist a on a.Stockist_Code=z.Distributor_Code and z.DSM_Active_Flag=0 where a.Stockist_Active_Flag=0 and a.Division_Code='" + divcode + "' group by a.User_Entry_Code,a.Stockist_Code,a.Stockist_Name,a.Stockist_ContactPerson,a.Stockist_Mobile,a.Territory,a.Distributor_Code,a.Field_Name,";
            //strQry = "SELECT a.User_Entry_Code,a.Stockist_Code,a.Stockist_Name,a.Stockist_ContactPerson,a.Stockist_Mobile,a.Territory,a.Distributor_Code," +
            //         "a.Field_Name,a.Username,a.Password,(select count(x.Territory_Code)as 'Sub_Couns' from Mas_Territory_Creation x where  x.Territory_Active_Flag=0 and  charindex(','+ a.Stockist_Code +',',','+x.Dist_Name+',')>0 )Sub_Couns," +
            //         "COUNT(z.DSM_name) as 'Sub_Coun' from Mas_DSM z full join mas_stockist a on a.Stockist_Code=z.Distributor_Code " +
            //         "and z.DSM_Active_Flag=0 " +
            //         "where a.Stockist_Active_Flag=0 and a.Division_Code='"+divcode+"' group by a.User_Entry_Code,a.Stockist_Code," +
            //         "a.Stockist_Name,a.Stockist_ContactPerson,a.Stockist_Mobile,a.Territory,a.Distributor_Code,a.Field_Name,a.Username,a.Password";

            strQry = " SELECT a.User_Entry_Code,a.Stockist_Code,a.Stockist_Name,a.Stockist_ContactPerson,a.Stockist_Mobile,T.Territory_name Territory,a.Distributor_Code, " +
                      " a.Field_Name,a.Username,a.Password,COUNT(x.Territory_Code) Sub_Couns, COUNT(z.DSM_name) as 'Sub_Coun' from Mas_DSM z  " +
                      " full join mas_stockist a on a.Stockist_Code=z.Distributor_Code and z.DSM_Active_Flag=0  inner join mas_territory T on t.Territory_code=a.Territory_Code left outer join Mas_Territory_Creation x  on charindex(','+a.Stockist_Code+',' ,','+x.Dist_Name +',')>0  and  x.Territory_Active_Flag=0 " +
                      " where a.Stockist_Active_Flag=0 and a.Division_Code='" + divcode + "' group by a.User_Entry_Code,a.Stockist_Code," +
                      " a.Stockist_Name,a.Stockist_ContactPerson,a.Stockist_Mobile,T.Territory_name,a.Distributor_Code,a.Field_Name,a.Username,a.Password";



            try
            {
                dsStockist = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsStockist;
        }
        public DataSet getStockist_Alphabat(string divcode, string sAlpha)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsStockist = null;
            //strQry =  "SELECT a.User_Entry_Code,a.Stockist_Code,a.Stockist_Name,a.Stockist_ContactPerson,a.Stockist_Mobile,a.Territory,a.Distributor_Code,a.Field_Name, " +
            //          "COUNT(z.DSM_name) as 'Sub_Coun' from Mas_DSM z full join "+
            //          "mas_stockist a on a.Stockist_Code=z.Distributor_Code and z.DSM_Active_Flag=0 "+
            //         " WHERE stockist_active_flag=0 AND Division_Code= '" + divcode + "' " +
            //         " AND LEFT(stockist_name,1) = '" + sAlpha + "'group by a.User_Entry_Code,a.Stockist_Code,a.Stockist_Name,a.Stockist_ContactPerson,a.Stockist_Mobile,a.Territory,a.Distributor_Code,a.Field_Name  " +
            //         " ORDER BY 2";

            strQry = "SELECT a.User_Entry_Code,a.Stockist_Code,a.Stockist_Name,a.Stockist_ContactPerson,a.Stockist_Mobile,T.Territory_name Territory,a.Distributor_Code,a.Field_Name,a.Username,a.Password, " +
                     "(select count(x.Territory_Code)as 'Sub_Couns' from Mas_Territory_Creation x where  x.Territory_Active_Flag=0 and charindex(','+ a.Stockist_Code +',',','+x.Dist_Name+',')>0 )Sub_Couns, " +
                     "COUNT(z.DSM_name) as 'Sub_Coun' from Mas_DSM z full join " +
                     "mas_stockist a on a.Stockist_Code=z.Distributor_Code and z.DSM_Active_Flag=0 inner join mas_territory T on t.Territory_code=a.Territory_Code " +
                    " WHERE stockist_active_flag=0 AND Division_Code= '" + divcode + "' " +
                    " AND LEFT(stockist_name,1) = '" + sAlpha + "'group by a.User_Entry_Code,a.Stockist_Code,a.Stockist_Name,a.Stockist_ContactPerson,a.Stockist_Mobile,T.Territory_name,a.Distributor_Code,a.Field_Name,a.Username,a.Password " +
                    " ORDER BY 2";
            try
            {
                dsStockist = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsStockist;
        }
        public DataSet getStockistview_Alphabet(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            strQry = "select '1' val,'All' stockist_name " +
                     " union " +
                     " select distinct LEFT(stockist_name,1) val, LEFT(stockist_name,1) stockist_name" +
                     " FROM mas_stockist " +
                     " WHERE stockist_active_flag=0 " +
                     " AND Division_Code = '" + divcode + "' " +
                     " ORDER BY 1";
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

        public DataSet getStockistview_Alphabat(string divcode, string sAlpha)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsStockist = null;
            strQry = "SELECT a.stockist_code,a.Stockist_Name,a.Stockist_Address,a.Stockist_Mobile,Sf_Name " +
                   " FROM mas_Stockist a " +
                   " inner join Mas_Stockist_FM b On a.Stockist_Code=b.Stockist_Code inner join Mas_Salesforce sfc on b.SF_Code=sfc.Sf_Code" +
                   " WHERE a.stockist_active_flag=0 " +
                   " AND a.Division_Code= '" + divcode + "' " +
                  " AND LEFT(b.stockist_name,1) = '" + sAlpha + "' " +
                  " ORDER BY 2";
            try
            {
                dsStockist = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsStockist;
        }

        // Search by Text
        public DataSet FindStockistlist(string sFindQry)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsStockist = null;
            strQry = "SELECT a.User_Entry_Code,a.Stockist_Code,a.Stockist_Name,a.Stockist_ContactPerson,a.Stockist_Mobile,a.Territory,a.Distributor_Code,a.Field_Name,a.Username,a.Password, " +
                      "(select count(x.Territory_Code)as 'Sub_Couns' from Mas_Territory_Creation x where x.Territory_Active_Flag=0 and  charindex(','+ a.Stockist_Code +',',','+x.Dist_Name+',')>0 )Sub_Couns," +
                      "COUNT(z.DSM_name) as 'Sub_Coun' from Mas_DSM z full join " +
                      "mas_stockist a on a.Stockist_Code=z.Distributor_Code and z.DSM_Active_Flag=0 where a.Stockist_Active_Flag=0 " + sFindQry + "group by a.User_Entry_Code,a.Stockist_Code,a.Stockist_Name,a.Stockist_ContactPerson,a.Stockist_Mobile,a.Territory,a.Distributor_Code,a.Field_Name,a.Username,a.Password";
            try
            {
                dsStockist = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsStockist;
        }
        // Sorting For StockistList 
        public DataTable getStockistList_DataTable(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dtStockist = null;
            strQry = "SELECT Stockist_Code,Stockist_Name,Stockist_ContactPerson,Stockist_Mobile,Territory " +
                     " FROM mas_stockist " +
                     " WHERE stockist_active_flag=0 AND Division_Code= '" + divcode + "' " +
                     " ORDER BY 2";
            try
            {
                dtStockist = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtStockist;
        }
        public DataTable getStockistFilter_DataTable(string divcode, string sAlpha)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dtStockist = null;
            strQry = "SELECT Stockist_Code,Stockist_Name,Stockist_ContactPerson,Stockist_Mobile,Territory " +
                     " FROM mas_stockist " +
                     " WHERE stockist_active_flag=0 AND Division_Code= '" + divcode + "' AND LEFT(Stockist_Name,1) = '" + sAlpha + "' " +
                     " ORDER BY 2";
            try
            {
                dtStockist = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtStockist;
        }

        public DataTable getStockistSearch_DataTable(string divcode, string sAlpha, string SearchBy)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dtStockist = null;
            strQry = "SELECT Stockist_Code,Stockist_Name,Stockist_ContactPerson,Stockist_Mobile,Territory " +
                     " FROM mas_stockist " +
                     " WHERE stockist_active_flag=0 AND Division_Code= '" + divcode + "' AND LEFT(" + SearchBy + ",1) = '" + sAlpha + "' " +
                     " ORDER BY 2";
            try
            {
                dtStockist = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtStockist;
        }

        // I want to Create Stockist Details using select from Database
        public DataSet getStockist_Create(string divcode, string stockist_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsStockist = null;
            strQry = "SELECT User_Entry_Code,Stockist_Name,Stockist_Address,Stockist_ContactPerson,Stockist_Designation,Stockist_Mobile,SF_Code,ERP_Code,Dist_Code,Username,Password,Territory_Code,subdivision_code,Norm_Val,(select sf_name +',' from Mas_Salesforce s where charindex(','+ s.sf_code +',' ,','+a.field_code +',')>0  for xml path(''))  Field_Name,Field_Code,Head_Quaters,Type,gstn,Dis_Cat_Code,Taluk_Name,isnull(Stockist_Address1,'')Stockist_Email" +
                     " FROM mas_stockist a" +
                     " WHERE stockist_active_flag=0 " +
                     " AND Division_Code= '" + divcode + "' " +
                     " AND stockist_code = '" + stockist_code + "'  ";

            try
            {
                dsStockist = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsStockist;
        }
        // Same Stockist_name could not be Created return will be Exit
        public bool RecordExist(string stockist_name)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(stockist_code) FROM mas_stockist WHERE stockist_name ='" + stockist_name + "'  ";
                int iRecordExist = db.Exec_Scalar(strQry);

                if (iRecordExist > 0)
                    bRecordExist = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return bRecordExist;
        }
        public bool RecordExist(string Stock_code, string stockist_code, string div_code)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(stockist_code) FROM mas_stockist WHERE stockist_code !='" + stockist_code + "' and stockist_code = '" + Stock_code + "' AND Division_Code='" + div_code + "'";

                int iRecordExist = db.Exec_Scalar(strQry);

                if (iRecordExist > 0)
                    bRecordExist = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return bRecordExist;
        }

        public bool stRecordExist(string Territor_Code, string stockist_code, string stock_Name, string div_code)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(stockist_code) FROM mas_stockist WHERE  CHARINDEX(','+'" + Territor_Code + "'+',',','+ CAST(Territory_Code as varchar)+',')>0  and  stockist_code !='" + stockist_code + "' and Stockist_Name = '" + stock_Name + "' AND Division_Code='" + div_code + "'";

                int iRecordExist = db.Exec_Scalar(strQry);

                if (iRecordExist > 0)
                    bRecordExist = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return bRecordExist;
        }



        public bool NRecordExist(string Territor_Code, string stockist_name, string div_code)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(Stockist_Name) FROM mas_stockist WHERE   CHARINDEX(','+'" + Territor_Code + "'+',',','+ CAST(Territory_Code as varchar)+',')>0   and Stockist_Name = '" + stockist_name + "' AND Division_Code='" + div_code + "'";

                int iRecordExist = db.Exec_Scalar(strQry);

                if (iRecordExist > 0)
                    bRecordExist = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return bRecordExist;
        }
        // Insert the Values in Stockist Details
        public int RecordAdd(string divcode, string SF_Name, string stockist_name, string stockist_Address, string stockist_ContactPerson, string stockist_Designation, string stockist_mobilno, string ERP_Code, string Town_Name, string Town_code, string Territor_Code, string Territory_Name, string Username, string Password, string sub_division, int norm, string Fo_Name, string Fo_Code, string head_quaters, string type, string gstnNo, string dis_cat_code, string stkemail, string Taluk_Name, string Taluk_code)
        {
            int iReturn = -1;
            if (!NRecordExist(Territor_Code, stockist_name, divcode))
            {
                try
                {
                    int stockist_code = 0;
                    string STATE_CD = "";
                    DataSet dsstcode = null;
                    DB_EReporting db = new DB_EReporting();


                    strQry = "SELECT CASE WHEN COUNT(Distributor_Code)>0 THEN MAX(Distributor_Code) ELSE 0 END FROM mas_stockist";
                    stockist_code = db.Exec_Scalar(strQry);
                    stockist_code += 1;
                    // strQry = "select State_code from mas_Salesforce where sf_Code='" + Fo_Code + "'";
                    strQry = "select State_code from mas_Salesforce where charindex( ',' + cast(sf_Code as varchar) + ',','," + Fo_Code + "') > 0 group by State_code";
                    //STATE_CD = db.Exec_Scalar(strQry).ToString();

                    dsstcode = db.Exec_DataSet(strQry);
                    if (dsstcode.Tables[0].Rows.Count > 1)
                    {
                        return iReturn;
                    }
                    else
                    {
                        foreach (DataRow row in dsstcode.Tables[0].Rows)
                        {
                            STATE_CD += row["State_code"].ToString() + ",";
                        }
                        STATE_CD = STATE_CD.TrimEnd(',');
                    }



                    strQry = " INSERT INTO mas_stockist(Division_Code,Stockist_Code, SF_Code, Stockist_Name, Stockist_Address, Stockist_ContactPerson, Stockist_Designation, Stockist_Active_Flag, Stockist_Mobile, Territory, Created_Date,ERP_Code,Dist_Name,Dist_Code,Username,Password,Territory_Code,Distributor_Code,subdivision_code,Norm_Val,Field_Name,Field_Code,State_Code,User_Entry_Code,Head_Quaters,Type,gstn,Dis_Cat_Code,Stockist_Address1,Taluk_code,Taluk_Name) " +
                             " values('" + divcode + "', '" + stockist_code + "', '" + stockist_code + "','" + stockist_name + "', '" + stockist_Address + "', '" + stockist_ContactPerson + "', '" + stockist_Designation + "', '0' ,'" + stockist_mobilno + "','" + Territory_Name + "',getdate(),'" + ERP_Code + "','" + Town_Name + "','" + Town_code + "','" + Username + "','" + Password + "','" + Territor_Code + "','" + stockist_code + "','" + sub_division + "','" + norm + "','" + Fo_Name + "','" + Fo_Code + "','" + STATE_CD + "','" + SF_Name + "','" + head_quaters + "','" + type + "','" + gstnNo + "','" + dis_cat_code + "','" + stkemail + "','" + Taluk_code + "','" + Taluk_Name + "')";

                    iReturn = db.ExecQry(strQry);
                    if (iReturn > 0)
                    {
                        iReturn = stockist_code; //Inorder to maintain the same sl_no on detail table
                    }
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }
            else
            {
                iReturn = -2;
            }
            return iReturn;

        }

        // Insert into Another Table
        public int RecordAdd_FM(string divcode, int stockist_code, string stockist_name, string SF_Code, string Sale_Entry)
        {
            int iReturn = -1;
            // if (!RecordExist(stockist_name))
            //  {
            try
            {
                //int SF_Code = -1;

                DB_EReporting db = new DB_EReporting();

                //strQry = "SELECT CASE WHEN COUNT(SF_Code)>0 THEN MAX(SF_Code) ELSE 0 END FROM mas_stockist_FM";
                //SF_Code = db.Exec_Scalar(strQry);
                //SF_Code += 1;

                strQry = " INSERT INTO mas_stockist_FM(Stockist_Code,Stockist_Name ,SF_Code ,Sale_Entry,Division_Code) " +
                         " values( '" + stockist_code + "', '" + stockist_name + "','" + SF_Code + "', 0 ,'" + divcode + "')";

                iReturn = db.ExecQry(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            //}
            //else
            //{
            //    iReturn = -2;
            //}
            return iReturn;

        }

        // InlineEdit Option Update the Stockist Details
        public int RecordUpdate(string divcode, string stockist_code, string stockist_name, string stockist_ContactPerson, string stockist_mobile, string Territory, string gstnNo)
        {
            int iReturn = -1;
            if (!RecordExist(stockist_code, stockist_name, divcode))
            {
                try
                {
                    DB_EReporting db = new DB_EReporting();

                    strQry = "UPDATE mas_stockist " +
                             " SET stockist_name = '" + stockist_name + "' , " +
                             " stockist_ContactPerson = '" + stockist_ContactPerson + "' , " +
                             " stockist_mobile = '" + stockist_mobile + "' , " +
                             " Territory = '" + Territory + "', " +
                             " gstn = '" + gstnNo + "', " +
                             " LastUpdt_Date = getdate() " +

                             " WHERE stockist_code = '" + stockist_code + "' AND  Division_Code = '" + divcode + "' ";

                    iReturn = db.ExecQry(strQry);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
                iReturn = -3;
            }
            return iReturn;
        }
        // Edit Option Update the Stockist Details
        public int RecordUpdate(string divcode, string stockist_code, string Stock_code, string stockist_name, string stockist_Address, string stockist_ContactPerson, string stockist_Designation, string stockist_mobilno, string ERP_Code, string Town_Name, string Town_code, string Territor_Code, string Territory_Name, string Username, string Password, int norm, string Fo_Name, string Fo_Code, string sub_division, string headquarters, string type, string gstnNo,string Dis_Cat_Code,string stkemail, string Taluk_Name, string Taluk_code)
        {
            int iReturn = -1;
            if (!stRecordExist(Territor_Code, stockist_code, stockist_name, divcode))
            {
                // if (!NRecordExist(stockist_code, stockist_name, divcode))
                // {
                try
                {
                    string STATE_CD = "";
                    DataSet dsstcode = null;
                    DB_EReporting db = new DB_EReporting();

                    // strQry = "select State_code from mas_Salesforce where sf_Code='" + Fo_Code + "'";
                    strQry = "select State_code from mas_Salesforce where charindex( ',' + cast(sf_Code as varchar) + ',','," + Fo_Code + "') > 0 group by State_code";
                    // STATE_CD = db.Exec_Scalar(strQry).ToString();

                    dsstcode = db.Exec_DataSet(strQry);
                    if (dsstcode.Tables[0].Rows.Count > 1)
                    {
                        foreach (DataRow row in dsstcode.Tables[0].Rows)
                        {
                            STATE_CD += row["State_code"].ToString() + ",";
                        }
                        STATE_CD = STATE_CD.TrimEnd(',');
                    }
                    /*else
                    {
                        return iReturn;
                    }*/

                    strQry = "UPDATE mas_stockist " +
                             " SET stockist_name = '" + stockist_name + "' , " +
                             " User_Entry_Code = '" + Stock_code + "' , " +
                             " stockist_Address = '" + stockist_Address + "', " +
                             " stockist_ContactPerson = '" + stockist_ContactPerson + "' , " +
                             " stockist_Designation = '" + stockist_Designation + "' , " +
                             " stockist_mobile = '" + stockist_mobilno + "' , " +
                             " Head_Quaters = '" + headquarters + "'  ,   Type = '" + type + "' ," +
                             " Territory = '" + Territory_Name + "', ERP_Code = '" + ERP_Code + "', " +
                             " Dist_Name = '" + Town_Name + "' ,Dist_Code = '" + Town_code + "', " +
                             " Username = '" + Username + "' ,Password = '" + Password + "', " +
                             " Norm_Val ='" + norm + "', " +
                             " Field_Name = '" + Fo_Name + "' ,Field_Code = '" + Fo_Code + "', " +
                             " Territory_Code = '" + Territor_Code + "' , " +
                              " subdivision_code ='" + sub_division + "'," +
                              " State_Code='" + STATE_CD + "'," +
                              " gstn='" + gstnNo + "',Stockist_Address1='" + stkemail + "'," +


                              " Dis_Cat_Code='" + Dis_Cat_Code + "'," +
                              " Taluk_code='" + Taluk_code + "'," +
                              " Taluk_Name='" + Taluk_Name + "'," +
                              

                             " LastUpdt_Date = getdate() " +
                             " WHERE stockist_code = '" + stockist_code + "' AND Division_Code = '" + divcode + "' ";
                    iReturn = db.ExecQry(strQry);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                // }
                //  else
                //  {
                //      iReturn = -2;
                //  }
            }
            else
            {
                iReturn = -3;
            }
            return iReturn;
        }
  public int inserttarget(string xml, string Mon, string year, string sf_code,string div_code)
        {
            int iReturn = -1;

            DB_EReporting db = new DB_EReporting();
           // if(stockist_code=)
          //  strQry = "select cast(isnull(max(cast(Target_sl_no as int)),0)+1 as varchar) from Trans_target_head";
       // int  dsstcode = db.Exec_Scalar(strQry);
            try 
            {
                strQry = "exec insert_target_hd'" + xml + " ','"+ Mon + "','"+ year + "','"+ div_code + "','" + sf_code + "'";

                iReturn = db.ExecQry(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }
          
            return iReturn;

        }
        public int RecordUpdate_Sale_Entry(string divcode, string sChkSalesforce)
        {
            int iReturn = -1;

            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = " UPDATE mas_stockist_FM " +
                         " SET Sale_Entry= 1 " +
                         " WHERE SF_Code = '" + sChkSalesforce + "' AND Division_Code = '" + divcode + "' ";
                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }
        // DeActivate Option Deactivate in Stockist Details
        public int DeActivate(string stockist_code,string stat)
        {
            int iReturn = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "exec Stockist_Deactivate '" + stockist_code + "',"+ stat + " ";
                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }
		 public int terDeActivate(string terCode, string stat)
        {
            int iReturn = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "exec territory_Deactivate '" + terCode + "'," + stat + " ";
                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }
		 public DataTable getterexcel(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();
            string sub = "";
            DataTable dsStockist = null;
          
            strQry = "exec getterritoryMaster '" + divcode + "'";
            try
            {
                dsStockist = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsStockist;
        }
        public int RecordAdd_Pool(string divcode, string Pool_SName, string Pool_Name)
        {
            int iReturn = -1;

            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT isnull(max(Pool_Id)+1,'1') Pool_Id from Mas_Pool_Area_Name ";
                int Pool_Id = db.Exec_Scalar(strQry);

                strQry = "INSERT INTO Mas_Pool_Area_Name(Pool_Id,Division_Code,Pool_SName,Pool_Name,created_Date,LastUpdt_Date)" +
                         "values('" + Pool_Id + "','" + divcode + "','" + Pool_SName + "', '" + Pool_Name + "',getdate(),getdate()) ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;
        }
        public DataSet getPoolName(string divcode, string Pool_Id)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSubDiv = null;
            strQry = "SELECT Pool_SName,Pool_Name " +
                     " FROM Mas_Pool_Area_Name WHERE Pool_Id= '" + Pool_Id + "' AND Division_Code= '" + divcode + "'" +
                     " ORDER BY Pool_Name";
            try
            {
                dsSubDiv = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsSubDiv;
        }
        public DataSet getPoolName_List(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSubDiv = null;
            strQry = "SELECT Pool_Id,Pool_SName,Pool_Name " +
                     " FROM Mas_Pool_Area_Name WHERE Division_Code= '" + divcode + "'" +
                     " ORDER BY Pool_Name";
            try
            {
                dsSubDiv = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsSubDiv;
        }
        public DataSet getPool_Name(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsStockist = null;
            strQry = " SELECT '' as Dist_code, '--Select--' as Dist_name " +
                     " UNION " +
                     " select Dist_code,Dist_name from Mas_District where Dist_Active_Flag=0 and Div_Code = '" + div_code + "'order by 2";

            try
            {
                dsStockist = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsStockist;
        }
        public DataSet getTer_Name(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsStockist = null;
            strQry = " SELECT '' as Territory_code, '--Select--' as Territory_name " +
                     " UNION " +
                     " select Territory_code,Territory_name from Mas_Territory where Div_Code = '" + div_code + "'and Territory_Active_Flag=0 order by 2";

            try
            {
                dsStockist = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsStockist;
        }
        public DataSet getCheck(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsStockist = null;
            strQry = "select isnull(MAX(cast(Stockist_Code as numeric)),1) as Num from Mas_Stockist where isnumeric(Stockist_Code)>0";
            try
            {
                dsStockist = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsStockist;
        }
        public DataSet getStockistCheck(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsStockist = null;
            //strQry = "EXEC sp_UserList_MR_Stockist '" + divcode + "', 'admin' ";

            //strQry = " SELECT a.sf_Code, a.sf_Name, a.Sf_UserName, a.sf_Type, " +
            //         " (select sf_name from mas_salesforce where sf_code=a.Reporting_To_SF) as Reporting_To_SF," +
            //          " (select sf_hq from mas_salesforce where sf_code=a.Reporting_To_SF) as rep_hq, " +
            //          " a.sf_hq, a.sf_password FROM mas_salesforce a " +
            //          " WHERE a.SF_Status=0 and a.sf_Tp_Active_flag = 0 and sf_type=1 and (a.Division_Code like '" + divcode + ',' + "%'  or " +
            //         " a.Division_Code like '%" + ',' + divcode + ',' + "%') ";

            strQry = " SELECT a.sf_Code, a.sf_Name +' - ' + a.sf_Designation_Short_Name +' - '+ a.Sf_HQ as sf_Name, a.Sf_UserName, a.sf_Type, " +
                    " (select sf_name from mas_salesforce where sf_code=a.Reporting_To_SF) as Reporting_To_SF," +
                     " (select sf_hq from mas_salesforce where sf_code=a.Reporting_To_SF) as rep_hq, " +
                     " a.sf_hq, a.sf_password FROM mas_salesforce a " +
                     " WHERE a.SF_Status=0 and a.sf_Tp_Active_flag = 0 and sf_type=1 and (a.Division_Code like '" + divcode + ',' + "%'  or " +
                    " a.Division_Code like '%" + ',' + divcode + ',' + "%') ";

            try
            {
                dsStockist = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsStockist;
        }
        public DataSet getStockist_Filter(string divcode, string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsStockist = null;
            //strQry = "EXEC sp_UserList_MR_Stockist '" + divcode + "', 'admin' ";
            if (sf_code != "admin")
            {
                strQry = " SELECT a.sf_Code, a.sf_Name, a.Sf_UserName, a.sf_Type, " +
                         " (select sf_name from mas_salesforce where sf_code=a.Reporting_To_SF) as Reporting_To_SF," +
                          " (select sf_hq from mas_salesforce where sf_code=a.Reporting_To_SF) as rep_hq, " +
                          " a.sf_hq, a.sf_password FROM mas_salesforce a " +
                          " WHERE a.SF_Status=0 and a.sf_Tp_Active_flag = 0 and sf_type=1 AND (a.Division_Code like '" + divcode + ',' + "%'  or " +
                          " a.Division_Code like '%" + ',' + divcode + ',' + "%') and a.Reporting_To_SF ='" + sf_code + "' ";
            }
            else
            {
                strQry = " SELECT a.sf_Code, a.sf_Name, a.Sf_UserName, a.sf_Type, " +
                        " (select sf_name from mas_salesforce where sf_code=a.Reporting_To_SF) as Reporting_To_SF," +
                         " (select sf_hq from mas_salesforce where sf_code=a.Reporting_To_SF) as rep_hq, " +
                         " a.sf_hq, a.sf_password FROM mas_salesforce a " +
                         " WHERE a.SF_Status=0 and a.sf_Tp_Active_flag = 0 and sf_type=1 AND (a.Division_Code like '" + divcode + ',' + "%'  or " +
                         " a.Division_Code like '%" + ',' + divcode + ',' + "%')  ";
            }
            try
            {
                dsStockist = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsStockist;
        }
        //changes done By Reshmi
        public DataSet getStockist_Allow_Admin(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet dsStockist = null;

            strQry = "Select No_Of_Sl_StockistAllowed from Admin_Setups where Division_Code='" + div_code + "'";

            try
            {
                dsStockist = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsStockist;
        }
      public int inserttarget(string divcode, string stockist_code, string Mon, string year, string P_code, string T_Qnty,string sf_code)
        {
            int iReturn = -1;

            DB_EReporting db = new DB_EReporting();
           // if(stockist_code=)
            strQry = "select cast(isnull(max(cast(Target_sl_no as int)),0)+1 as varchar) from Trans_target_head";
          int  dsstcode = db.Exec_Scalar(strQry);
            try 
            {   
                strQry = "exec insert_target_hd'"+ dsstcode + " ','"+ stockist_code + "','"+ Mon + "','"+ year + "','"+ divcode + "','"+ P_code + "','"+ T_Qnty + "','" + sf_code + "'";

                iReturn = db.ExecQry(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }
          
            return iReturn;

        }
        public DataSet getStockist_Count(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet dsStockist = null;

            strQry = "SELECT COUNT(Stockist_Code) from mas_stockist WHERE Division_Code='" + div_code + "' and Stockist_Active_Flag= 0";

            try
            {
                dsStockist = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsStockist;

        }
        public int GetStockistCode()
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT isnull(max(Stockist_Code)+1,'1') Stockist_Code from Mas_Stockist";

                iReturn = db.Exec_Scalar(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;

        }

        public DataSet GetPool(string Pool_Name, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;
            strQry = " select Pool_Name from Mas_Pool_Area_Name where Pool_Name='" + Pool_Name + "'  and Division_Code = '" + div_code + "' ";

            try
            {
                dsListedDR = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsListedDR;
        }

        public int GetPrimary()
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT isnull(max(SNo)+1,'1') SNo from JS_Primary_Upload";

                iReturn = db.Exec_Scalar(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;

        }
        public DataSet FOName(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet dsStockist = null;

            strQry = "select Sf_Code,Sf_Name from Mas_Salesforce where  Division_Code like '" + div_code + ",%' and sf_TP_Active_Flag=0 AND SF_Status!=2 order by 2";

            try
            {
                dsStockist = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsStockist;

        }

        public DataSet FOName(string terr, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet dsStockist = null;

            strQry = "select Sf_Code,Sf_Name from Mas_Salesforce where  Division_Code like '" + div_code + ",%' and sf_TP_Active_Flag=0 AND SF_Status!=2 and sf_type!=2 and Territory_Code='" + terr + "' order by 2";

            try
            {
                dsStockist = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsStockist;

        }

        public DataSet getffO_Name(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsStockist = null;
            strQry = "SELECT '' as Sf_Code, '--Select--' as Sf_Name " +
                     " UNION " +
                     " select Sf_Code,Sf_Name from Mas_Salesforce a where sf_TP_Active_Flag=0 and Division_Code like '" + div_code + ",%' or Division_Code like '%," + div_code + ",%' order by 2";

            try
            {
                dsStockist = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsStockist;
        }
        public DataTable getStockist_Filter1(string divcode, string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dsStockist = null;
            //strQry = "EXEC sp_UserList_MR_Stockist '" + divcode + "', 'admin' ";
            if (sf_code != "admin")
            {
                strQry = " SELECT a.User_Entry_Code,a.Stockist_Code,a.Stockist_Name,a.Stockist_ContactPerson,a.Stockist_Mobile,a.Territory,a.Distributor_Code,a.Field_Name,a.Username,a.Password, " +
                         " (select count(x.Territory_Code)as 'Sub_Couns' from Mas_Territory_Creation x where  x.Territory_Active_Flag=0 and  charindex(','+ a.Stockist_Code +',',','+x.Dist_Name+',')>0 )Sub_Couns," +
                         " COUNT(z.DSM_name) as 'Sub_Coun' from Mas_DSM z full join " +
                         "mas_stockist a on a.Stockist_Code=z.Distributor_Code and z.DSM_Active_Flag=0 where a.Stockist_Active_Flag=0 " +
                          " and a.Division_Code = '" + divcode + "' and  charindex(','+'" + sf_code + "'+',',',' + a.Field_Code+',')>0 group by a.User_Entry_Code,a.Stockist_Code,a.Stockist_Name,a.Stockist_ContactPerson,a.Stockist_Mobile,a.Territory,a.Distributor_Code,a.Field_Name,a.Username,a.Password order by Stockist_Name";
            }
            else
            {
                strQry = " SELECT a.User_Entry_Code,a.Stockist_Code,a.Stockist_Name,a.Stockist_ContactPerson,a.Stockist_Mobile,a.Territory,a.Distributor_Code,a.Field_Name,a.Username,a.Password, " +
                         " (select count(x.Territory_Code)as 'Sub_Couns' from Mas_Territory_Creation x where x.Territory_Active_Flag=0 and  charindex(','+ a.Stockist_Code +',',','+x.Dist_Name+',')>0 )Sub_Couns, " +
                         " COUNT(z.DSM_name) as 'Sub_Coun' from Mas_DSM z full join " +
                         " mas_stockist a on a.Stockist_Code=z.Distributor_Code and z.DSM_Active_Flag=0 where a.Stockist_Active_Flag=0 " +
                          " and a.Division_Code = '" + divcode + "'group by a.User_Entry_Code,a.Stockist_Code,a.Stockist_Name,a.Stockist_ContactPerson,a.Stockist_Mobile,a.Territory,a.Distributor_Code,a.Field_Name,a.Username,a.Password";
            }
            try
            {
                dsStockist = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsStockist;
        }
        public int DeActivate1(string Re_code)
        {
            int iReturn = -1;

            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = " UPDATE Mas_ListedDr " +
                         " SET ListedDr_Active_Flag=1 , " +
                         " ListedDr_Deactivate_Date = getdate() " +
                         " WHERE ListedDrCode = '" + Re_code + "' ";
                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }
        public DataTable getStockist_Ex_MGR(string divcode, string sf_code, string Alpa, string sFind, string subDivCode = "0")
        {
            DB_EReporting db_ER = new DB_EReporting();
            string sub = "";
            DataTable dsStockist = null;
            //strQry = "SELECT a.User_Entry_Code,a.Stockist_Code,a.Stockist_Name,a.Stockist_ContactPerson,a.Stockist_Mobile,a.Territory,a.Distributor_Code,a.Field_Name, " +
            //         "COUNT(z.DSM_name) as 'Sub_Coun' from Mas_DSM z full join "+
            //         "mas_stockist a on a.Stockist_Code=z.Distributor_Code and z.DSM_Active_Flag=0 where a.Stockist_Active_Flag=0 and a.Division_Code='" + divcode + "' group by a.User_Entry_Code,a.Stockist_Code,a.Stockist_Name,a.Stockist_ContactPerson,a.Stockist_Mobile,a.Territory,a.Distributor_Code,a.Field_Name";
            //strQry = "SELECT a.User_Entry_Code,a.Stockist_Code,a.Stockist_Name,a.Stockist_ContactPerson,a.Stockist_Mobile,a.Territory,a.Distributor_Code," +
            //         "a.Field_Name,a.Username,a.Password,(select count(x.Territory_Code)as 'Sub_Couns' from Mas_Territory_Creation x where a.Stockist_Code=x.Dist_Name and x.Territory_Active_Flag=0)Sub_Couns," +
            //         "COUNT(z.DSM_name) as 'Sub_Coun' from Mas_DSM z full join mas_stockist a on a.Stockist_Code=z.Distributor_Code " +
            //         "and z.DSM_Active_Flag=0 " +
            //         "where a.Stockist_Active_Flag=0 and a.Division_Code='" + divcode + "' group by a.User_Entry_Code,a.Stockist_Code," +
            //         "a.Stockist_Name,a.Stockist_ContactPerson,a.Stockist_Mobile,a.Territory,a.Distributor_Code,a.Field_Name,a.Username,a.Password";
            strQry = "exec [GET_DISTRIBUTOR_List_Detail] '" + divcode + "','" + sf_code + "','" + Alpa + "','" + sFind + "','" + subDivCode + "'";
            try
            {
                dsStockist = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsStockist;
        }



        public DataSet GET_DISTRIBUTOR_Home(string divcode, string sf_code, string Alpa, string sFind, string subDivCode = "0")
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet dsStockist = null;
            strQry = "exec [GET_DISTRIBUTOR_Home] '" + divcode + "','" + sf_code + "','" + Alpa + "','" + sFind + "','" + subDivCode + "'";
            try
            {
                dsStockist = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsStockist;
        }


        public int insert_Supplier(string div_code, string Sub_Div, string sname, string scontact, string smobile, string serp, string slno)
        {
            DB_EReporting db_ER = new DB_EReporting();
            int iReturn = -1;
            strQry = "exec SupplierAddUpade '" + div_code + "','" + Sub_Div + "','" + sname + "','" + scontact + "','" + smobile + "','" + serp + "','" + slno + "'";
            try
            {
                iReturn = db_ER.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }

        public DataSet GetStockist_subdivisionwise(string divcode, string subdivision, string sf_code = "admin")
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;

            // strQry = " SELECT 'ALL' as Stockist_code, '---ALL---' as Stockist_Name " +
            //      " UNION  all " +

            //  "select  Stockist_code,Stockist_Name from Mas_Stockist where Division_Code='" + divcode + "' and subdivision_code='" + subdivision + "' and Stockist_Active_Flag=0 ";
            strQry = "EXEC GET_DISTRIBUTOR '" + divcode + "','" + sf_code + "','" + subdivision + "'";
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
        public DataSet GetStockist_FieldForceWise(string divcode, string subdivision, string sf_code = "admin")
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;

            // strQry = " SELECT 'ALL' as Stockist_code, '---ALL---' as Stockist_Name " +
            //      " UNION  all " +

            //  "select  Stockist_code,Stockist_Name from Mas_Stockist where Division_Code='" + divcode + "' and subdivision_code='" + subdivision + "' and Stockist_Active_Flag=0 ";
            strQry = "EXEC GET_DISTRIBUTOR_SFWise '" + divcode + "','" + sf_code + "','" + subdivision + "'";
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

        public DataSet GetStockist_All_Details(string divcode, string sf_code, string FYear, string FMonth, string SubDiveCode = "0")
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;

            // strQry = " SELECT 'ALL' as Stockist_code, '---ALL---' as Stockist_Name " +
            //      " UNION  all " +

            //  "select  Stockist_code,Stockist_Name from Mas_Stockist where Division_Code='" + divcode + "' and subdivision_code='" + subdivision + "' and Stockist_Active_Flag=0 ";
            strQry = "EXEC GET_DISTRIBUTOR_All_Details '" + divcode + "','" + sf_code + "','" + FYear + "','" + FMonth + "','" + SubDiveCode + "'";
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

        public DataSet GetStockist_All_Details_notmatch(string divcode, string sf_code, string FYear, string FMonth, string SubDiveCode = "0")
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            strQry = "EXEC GET_DISTRIBUTOR_All_Details_notmatch '" + divcode + "','" + sf_code + "','" + FYear + "','" + FMonth + "','" + SubDiveCode + "'";
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


        public DataSet CategorywiseDistandSF(string divcode, string sf_code, string FYear, string FMonth, string SubDiveCode = "0")
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;

            // strQry = " SELECT 'ALL' as Stockist_code, '---ALL---' as Stockist_Name " +
            //      " UNION  all " +

            //  "select  Stockist_code,Stockist_Name from Mas_Stockist where Division_Code='" + divcode + "' and subdivision_code='" + subdivision + "' and Stockist_Active_Flag=0 ";
            strQry = "EXEC CategorywiseOrderFieldForce_Distributor '" + divcode + "','" + sf_code + "','" + FYear + "','" + FMonth + "','" + SubDiveCode + "'";
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

        public DataSet CategorywiseDistandSF_notmatch(string divcode, string sf_code, string FYear, string FMonth, string SubDiveCode = "0")
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;

            // strQry = " SELECT 'ALL' as Stockist_code, '---ALL---' as Stockist_Name " +
            //      " UNION  all " +

            //  "select  Stockist_code,Stockist_Name from Mas_Stockist where Division_Code='" + divcode + "' and subdivision_code='" + subdivision + "' and Stockist_Active_Flag=0 ";
            strQry = "EXEC CategorywiseOrderFieldForce_Distributor_notmatch '" + divcode + "','" + sf_code + "','" + FYear + "','" + FMonth + "','" + SubDiveCode + "'";
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


        public DataSet GetSuppliers(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            strQry = "select * from Supplier_Master where Division_Code='" + divcode + "'";
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
        public DataSet GetState_subdivisionwise(string divcode, string subdivision, string sf_code = "admin")
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            strQry = "EXEC GET_State '" + divcode + "','" + sf_code + "','" + subdivision + "'";
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


        public DataSet Get_Distributor_Target_vs_Sal(string SF_Code, string FYear, string FMonth, string TYear, string TMonth)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;
            strQry = "exec Get_Distributor_Target_vs_Sal '" + SF_Code + "','" + FYear + "','" + FMonth + "','" + TYear + "','" + TMonth + "'";

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

        public DataSet GetGEODistributor(string div_code, string sfCode, string subDiv, string FYear, string FMonth)
        {

            DataSet dsSF = null;
            DB_EReporting db = new DB_EReporting();
            strQry = "exec geoDistributors '" + div_code + "','" + sfCode + "','" + FYear + "','" + FMonth + "','" + subDiv + "'";
            try
            {
                dsSF = db.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsSF;

        }
        public int untagRetailers_confirm(string custCode)
        {

            int iReturn = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();
                strQry = "delete from Map_GEO_Distributors where Cust_Code='" + custCode + "'";
                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }
			
		public DataSet getState_name(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsStockist = null;
            strQry = " SELECT '' as State_Code, '--Select State--' as StateName " +
                     " UNION " +
                     " select State_Code,StateName from Mas_State ms where State_Code in (select SUBSTRING(md.State_Code, CHARINDEX(','+cast(ms.State_Code as varchar)+',',','+md.State_Code+','), LEN(ms.State_Code)) from Mas_Division md where md.Division_Code='" + div_code + "') order by 2";

            try
            {
                dsStockist = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsStockist;
        }

 public int inserttargetFF(string xml,  string year, string sf_code, string div_code,string subdiv)
        {
            int iReturn = -1;

            DB_EReporting db = new DB_EReporting();
            // if(stockist_code=)
            //  strQry = "select cast(isnull(max(cast(Target_sl_no as int)),0)+1 as varchar) from Trans_target_head";
            // int  dsstcode = db.Exec_Scalar(strQry);
            try
            {
                strQry = "exec insert_target_FF'" + xml + " ','" + year + "','" + div_code + "','" + sf_code + "','"+ subdiv + "'";

                iReturn = db.ExecQry(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;

        }
        public DataSet getStockistDetails(string divcode, string sf_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "exec getStockistMaster '" + sf_Code + "','" + divcode + "'";

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
        public DataSet getRoute_Stockist(string divcode, string route_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "exec get_Route_Details '"+ route_code + "','"+ divcode + "'";

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
        public int inserttargetFF_Primary(string xml, string year, string sf_code, string div_code, string subdiv)
        {
            int iReturn = -1;

            DB_EReporting db = new DB_EReporting();
            // if(stockist_code=)
            //  strQry = "select cast(isnull(max(cast(Target_sl_no as int)),0)+1 as varchar) from Trans_target_head";
            // int  dsstcode = db.Exec_Scalar(strQry);
            try
            {
                strQry = "exec insert_target_FF_Primary'" + xml + " ','" + year + "','" + div_code + "','" + sf_code + "','" + subdiv + "'";

                iReturn = db.ExecQry(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;

        }
        public DataSet getMappedDistSF(string stkcode)
        {
            DataSet iReturn = new DataSet();
            DB_EReporting db = new DB_EReporting();
            try
            {
                strQry = "select ((select Sf_Name+',' from Mas_Salesforce ms where SF_Status=0 and CHARINDEX(','+ms.Sf_Code+',',','+Field_Code+',')>0 for xml path('')))sf_name from Mas_Stockist where Stockist_Code='" + stkcode + " '";
                iReturn = db.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }
        public int updateMappedDistSF(string TerritoryCode,string FieldCode,string stkcode)
        {
            int iReturn = -1;
            DB_EReporting db = new DB_EReporting();
            try
            {
                strQry = "update Mas_territory_creation set Sf_Code=Sf_Code+'," + FieldCode.TrimEnd(',') + "' where Territory_Sname="+ TerritoryCode + " and CHARINDEX(',"+ stkcode + ",',','+Dist_Name+',')>0 and CHARINDEX(',"+ FieldCode.TrimEnd(',') + ",',','+SF_Code+',')<1";
                iReturn = db.ExecQry(strQry);
                strQry = "update Mas_ListedDr set Sf_Code=Sf_Code+'," + FieldCode.TrimEnd(',') + "' where TerrCode='" + TerritoryCode + "' and CHARINDEX('," + stkcode + ",',','+Dist_Name+',')>0 and CHARINDEX('," + FieldCode.TrimEnd(',') + ",',','+SF_Code+',')<1";
                iReturn = db.ExecQry(strQry);
				
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }
		
     public DataSet validate_Pri_sch_Name(string Scheme_Name, string Div_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet ds = null;
            strQry = "select* from Mas_Primary_Scheme where Scheme_Name = '" + Scheme_Name + "' and Division_Code = '" + Div_Code + "'";
            try
            {
                ds = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
		
      public DataSet getratebystk(string Div_Code, string Stockist_Code, string State)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet ds = null;
            try
            {
                strQry = "exec getrate '" + Div_Code + "','" + Stockist_Code + "','" + State + "'";

                ds = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
		
        public DataSet getStockist_Name_Primary(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsStockist = null;
            strQry = " SELECT '' as stockist_code, ' --- Select the Stockist --- ' as Stockist_Name, '-1' as  State_Code" +
                     " UNION " +
                     " select Stockist_Code,Stockist_Name,State_Code  from mas_Stockist where stockist_active_flag=0 AND Division_Code = '" + div_code + "'" +
                     " UNION " +
                     "select S_No as Stockist_Code,S_Name as Stockist_Name,State_Code as State_Code from supplier_master where Act_flg = 0 and division_code = '" + div_code + "'";

            //  " where sf_TP_Active_Flag in (0,2)  and Sf_Code in " +
            //  " (select TP_Reporting_SF from Mas_Salesforce where sf_code != 'admin' and Division_Code = '" + div_code + "' ) " +
            // " order by Sf_Name ";

            try
            {
                dsStockist = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsStockist;
        }
        public int DeActivate1(string Re_code,string stat)
        {
            int iReturn = -1;

            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = " UPDATE Mas_ListedDr " +
                         " SET ListedDr_Active_Flag="+ stat + " , " +
                         " ListedDr_Deactivate_Date = getdate() " +
                         " WHERE ListedDrCode = '" + Re_code + "' ";
                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }

  public int arDeActivate(string arcode, string stat)
        {
            int iReturn = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "exec area_Deactivate '" + arcode + "'," + stat + " ";
                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }
        public int znDeActivate(string zncode, string stat)
        {
            int iReturn = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "exec [zone_Deactivate] '" + zncode + "'," + stat + " ";
                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }
        public int disDeActivate(string dtcode, string stat)
        {
            int iReturn = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "exec [distr_Deactivate] '" + dtcode + "'," + stat + " ";
                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }
		 public DataTable getareamaster(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();
            string sub = "";
            DataTable dsStockist = null;
          
            strQry = "exec [getareaMaster] '" + divcode + "'";
            try
            {
                dsStockist = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsStockist;
        }

        public DataTable getzonemaster(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();
            string sub = "";
            DataTable dsStockist = null;

            strQry = "exec [getzoneMaster] '" + divcode + "'";
            try
            {
                dsStockist = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsStockist;
        }
        public int prodtlDeActivate(string arcode, string stat)
        {
            int iReturn = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "exec prodtl_DeActivate '" + arcode + "'," + stat + " ";
                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }
        public DataTable getdistmaster(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();
            string sub = "";
            DataTable dsStockist = null;

            strQry = "exec [getdistrMaster] '" + divcode + "'";
            try
            {
                dsStockist = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsStockist;
        }
    }
}