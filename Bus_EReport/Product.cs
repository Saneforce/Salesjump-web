using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DBase_EReport;
using System.Configuration;

namespace Bus_EReport
{
    public class Product
    {
        private string strQry = string.Empty;


        #region tsr

        public DataSet fillpromotiondtl(string Div, string sfcode, string FDTs, string TDTs, string stcode = "0", string distcode = "0")
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            //strQry = "EXEC dcrpromotion_image '" + Div + "', '" + sfcode + "', '" + FDTs + "', '" + TDTs + "','" + stcode + "','" + distcode + "'";

            strQry = "EXEC Get_Tsr_dcrpromotion_image '" + Div + "', '" + sfcode + "', '" + FDTs + "', '" + TDTs + "','" + stcode + "','" + distcode + "'";

            try
            {
                dsProCat = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProCat;
        }

        public DataSet fillpromotiondtl_img(string Ekey)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            strQry = "EXEC dcrpromotion_image_view '" + Ekey + "'";
            try
            {
                dsProCat = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProCat;
        }

        public DataSet getTsrDCRUsers(string DivCode, string sfcode, string fdt, string tdt, string stcode = "0", string distcode = "0")
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "exec get_TSRSFUsers " + 156 + ", '" + sfcode + "','" + fdt + "','" + tdt + "','" + stcode + "','" + distcode + "'";

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

        public DataSet getTsrDayplan(string DivCode, string sfcode, string fdt, string tdt, string stcode = "0", string discode = "0")
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "exec get_TsrPerDaySumaary " + 156 + ", '" + sfcode + "','" + fdt + "','" + tdt + "','" + stcode + "','" + discode + "'";

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

        public DataSet getTsrPerDayCalls(string DivCode, string sfcode, string fdt, string tdt, string stcode = "0", string discode = "0")
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "exec get_TsrPerDayCallDetails " + 156 + ", '" + sfcode + "','" + fdt + "','" + tdt + "','" + stcode + "','" + discode + "'";

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

        public DataSet getTsrRetailerCount(string DivCode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "exec get_TsrRoutewiseCnt '" + DivCode.TrimEnd(',') + "'";

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


        public DataSet getTsrLoginTimes(string DivCode, string sfcode, string fdt, string tdt, string stcode = "0", string discode = "0")
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "exec get_TsrDayAttnDets " + 156 + ", '" + sfcode + "','" + fdt + "','" + tdt + "','" + stcode + "','" + discode + "'";

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

        public DataSet getTsrTPDates(string fdt, string tdt)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "exec get_TsrTPDates '" + fdt + "','" + tdt + "'";

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

        public DataSet getTsrNRetailerPOB(string DivCode, string sfcode, string fdt, string tdt, string stcode = "0", string discode = "0")
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "exec get_TsrNewRetailersPOB " + 156 + ", '" + sfcode + "','" + fdt + "','" + tdt + "','" + stcode + "','" + discode + "'";

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

        public DataSet getTsrDayProfilePic(string DivCode = "156")
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "exec PR_GET_TSR_GetTbMyDayProfilePic  '" + DivCode + "'";

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




        #endregion






        public DataSet getProd(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            strQry = " SELECT Product_Detail_Code,Product_Detail_Name,Product_Sale_Unit,Product_Type_Code,Product_Description, " +
                     " Product_Code_SlNo,Product_Sale_Unit,product_unit,Sample_Erp_Code " +
                     " FROM  Mas_Product_Detail " +
                     " WHERE Product_Active_Flag=0 AND Division_Code= '" + divcode + "' " +
                     " ORDER BY 2";
            try
            {
                dsProCat = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProCat;
        }
        public DataSet getProd_Subdivisionwise(string divcode, string subdivision)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            strQry = " SELECT Product_Detail_Code,Product_Detail_Name,Product_Sale_Unit,Product_Type_Code,Product_Description, " +
                     " Product_Code_SlNo " +
                     " FROM  Mas_Product_Detail " +
                     " WHERE Product_Active_Flag=0 AND Division_Code= '" + divcode + "' and charindex(','+cast('" + subdivision + "' as varchar)+',',','+subdivision_code+',')>0 " +
                     " ORDER BY 2";
            try
            {
                dsProCat = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProCat;
        }
        public DataSet getProd(string divcode, string sAlpha)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            strQry = " SELECT Product_Detail_Code,Product_Detail_Name,Product_Sale_Unit,Product_Type_Code,Product_Description " +

                     " FROM  Mas_Product_Detail " +
                     " WHERE Product_Active_Flag=0 AND Division_Code= '" + divcode + "' " +
                     " AND LEFT(Product_Detail_Name,1) = N'" + sAlpha + "' " +
                     " ORDER BY 2";

            try
            {
                dsProCat = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProCat;
        }
        public DataSet getProd_Alphabet(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;
            strQry = "select '1' val,'All' Product_Detail_Name " +
                     " union " +
                     " select distinct LEFT(Product_Detail_Name,1) val, LEFT(Product_Detail_Name,1) Product_Detail_Name" +
                     " FROM mas_Product_Detail " +
                // " WHERE SF_Status=0 " +
                // " AND lower(sf_code) != 'admin' " +
                     " WHERE Division_Code = '" + divcode + "' " +
                     " AND Product_Active_flag = 0 " +
                     " ORDER BY 1";
            try
            {
                dsProCat = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProCat;
        }
        public DataSet getEmptyProd()
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            strQry = " SELECT TOP 10 '' code,'' name,'' descr,'' sale_unit, '' sample_unit1, '' sample_unit2, '' sample_unit3 " +
                     " FROM  sys.tables ";
            try
            {
                dsProCat = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProCat;
        }
 public DataSet get_Target_Qnty(string div_code, string sfcode, string mon,string year)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsState = null;
            strQry = "exec get_Target_Qnty '" + div_code + "','" + mon + "','" + year + "','" + sfcode + "'";

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

        // Sorting For ProductList
        public DataTable getProductlist_DataTable(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dtProCat = null;

            strQry = " SELECT Product_Detail_Code,Product_Detail_Name,Product_Sale_Unit,Product_Type_Code,Product_Description " +

                     " FROM  Mas_Product_Detail " +
                     " WHERE Product_Active_Flag=0 AND Division_Code= '" + divcode + "' " +

                     " ORDER BY 2";
            try
            {
                dtProCat = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtProCat;
        }

        public DataTable getProductlist_DataTable(string divcode, string sAlpha)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dtProCat = null;

            strQry = " SELECT Product_Detail_Code,Product_Detail_Name,Product_Sale_Unit,Product_Type_Code,Product_Description " +

                     " FROM  Mas_Product_Detail " +
                     " WHERE Product_Active_Flag=0 AND Division_Code= '" + divcode + "' " +
                     "AND LEFT(Product_Detail_Name,1) = '" + sAlpha + "' " +
                     " ORDER BY 2";
            try
            {
                dtProCat = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtProCat;
        }








        public DataSet getProdall_sfcode(string sf_code, string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            strQry = " SELECT a.Product_Detail_Code,a.Product_Detail_Name,a.Product_Sale_Unit,a.Product_Description, " +
                     " b.Product_Cat_Name,c.Product_Grp_Name" +
                     " FROM  Mas_Product_Detail a,Mas_Product_Category b,Mas_Product_Group c, Mas_Product_State_Rates d, Mas_Salesforce e" +
                     " WHERE a.product_cat_code = b.product_cat_code AND " +
                     " a.Product_Grp_Code = c.product_grp_code AND " +
                     " a.Product_Active_Flag=0 AND a.Product_Detail_Code = d.Product_Detail_Code AND " +
                     " d.State_Code = e.State_Code AND a.Division_Code= '" + divcode + "'  AND " +
                     " e.Sf_Code = '" + sf_code + "' " +
                     " ORDER BY 2";
            try
            {
                dsProCat = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProCat;
        }
  public DataSet distributertarget(string divcode, string sf_code, string fmonths, string fyear, string Tmonths, string Tyear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            // strQry = "select * from PRODUCT_TARGET_MONTHLY where Division_Code='"+divcode +"' and YEAR='"+year+"' and month='"+months+"'";
            strQry = "exec dis_target_detail '" + fmonths + "','" + fyear + "','" + Tmonths + "','" + Tyear + "','" + sf_code + "','" + divcode + "'";
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

  public DataSet get_stockist_target(string divcode, string sf_code, string fmonths, string fyear, string Tmonths, string Tyear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            // strQry = "select * from PRODUCT_TARGET_MONTHLY where Division_Code='"+divcode +"' and YEAR='"+year+"' and month='"+months+"'";
            strQry = "exec get_stockist_target '" + fmonths + "','" + fyear + "','" + Tmonths + "','" + Tyear + "','" + sf_code + "','" + divcode + "'";
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

  public DataSet pro_target_detail(string divcode, string sf_code, string fmonths, string fyear, string Tmonths, string Tyear,string st_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            // strQry = "select * from PRODUCT_TARGET_MONTHLY where Division_Code='"+divcode +"' and YEAR='"+year+"' and month='"+months+"'";
            strQry = "exec pro_target_detail '" + fmonths + "','" + fyear + "','" + Tmonths + "','" + Tyear + "','" + sf_code + "','" + st_code + "','" + divcode + "'";
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
 public DataSet dis_ProMonth_detail(string divcode, string sf_code, string fmonths, string fyear, string Tmonths, string Tyear,string st_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            // strQry = "select * from PRODUCT_TARGET_MONTHLY where Division_Code='"+divcode +"' and YEAR='"+year+"' and month='"+months+"'";
            strQry = "exec dis_ProMonth_detail '" + fmonths + "','" + fyear + "','" + Tmonths + "','" + Tyear + "','" + sf_code + "','" + divcode + "','" + st_code + "'";
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

  

        public DataSet getEffDate(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            strQry = " SELECT 'All' Effective_From_To UNION SELECT DISTINCT (convert(varchar(12),gift_Effective_from,103)+' To '+convert(varchar(12),gift_effective_to,103)) Effective_From_To FROM mas_Gift " +
                       " WHERE gift_active_flag=0 AND division_code=  '" + divcode + "' " +
                       " and (Gift_Effective_From >= Convert(Date, GetDate(), 101) or Gift_Effective_To >= Convert(Date, GetDate(), 101)) " +
                       " ORDER BY 1 DESC";

            try
            {
                dsProCat = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProCat;
        }

        public DataSet getEffDate_deact(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            strQry = " SELECT 'All' Effective_From_To UNION SELECT DISTINCT (convert(varchar(12),gift_Effective_from,103)+' To '+convert(varchar(12),gift_effective_to,103)) Effective_From_To FROM mas_Gift " +
                       " WHERE gift_active_flag=0 AND division_code=  '" + divcode + "' " +
                       " and (Gift_Effective_From < DATEADD(day, DATEDIFF(day, 0, getDate()) - 1, 0)" +
                       " and Gift_Effective_To <  DATEADD(day, DATEDIFF(day, 0, getDate()),  0))" +
                       " order by 1 desc";
            try
            {
                dsProCat = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProCat;
        }

        // Changes done by Saravanan 05/08/2014
        public DataSet getGift(string divcode, string giftcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            //strQry = "  SELECT Gift_SName,Gift_Name,Gift_Value,Gift_Type,Gift_Effective_From,Gift_Effective_To,State_Code" +
            //            " FROM Mas_Gift  WHERE Division_Code=" +
            //            "'" + divcode + "' AND Gift_Code = '"+ giftcode + "' " +
            //            " ORDER BY 2";

            strQry = " SELECT Gift_SName,Gift_Name,Gift_Value,Gift_Type,convert(varchar(10),Gift_Effective_From,103) Gift_Effective_From," +
                     " convert(varchar(10),Gift_Effective_To,103) Gift_Effective_To,State_Code,subdivision_code" +
                     " FROM Mas_Gift  WHERE Division_Code=" +
                     " '" + divcode + "' AND Gift_Code = '" + giftcode + "' ORDER BY 2";
            try
            {
                dsProCat = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProCat;
        }


        public DataSet getProductRate(string state_code, string div_code, string suDiv = "0")
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsRate = null;
            strQry = "EXEC Product_stateWise_Rate_View '" + state_code + "', '" + div_code + "', '" + suDiv + "' ";

            try
            {
                dsRate = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsRate;
        }
        public DataSet getProductCateRate(string state_code, string div_code,string subDiv="0")
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsRate = null;
            strQry = "EXEC Product_CateWise_Rate_View '" + state_code + "', '" + div_code + "', '" + subDiv + "' ";

            try
            {
                dsRate = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsRate;
        }
        public DataSet getProductRate_all(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsRate = null;
            //strQry = " select  p.product_Detail_Code,product_Detail_Name,Product_Sale_Unit,isnull(rtrim(MRP_Price),0) MRP_Price, " +     
            //         " isnull(rtrim(Retailor_Price),0) Retailor_Price,isnull(rtrim(Distributor_Price),0) Distributor_Price, " +     
            //          " isnull(rtrim(NSR_Price),0) NSR_Price,isnull(rtrim(Target_Price),0) Target_Price  From Mas_Product_Detail p left outer " +       
            //          " join Mas_Product_State_Rates R on R.product_Detail_code=P.Product_Detail_code and " +       
            //           " Sl_No in (select max(Max_State_Sl_No) from mas_Product_State_Rates " +
            //            " where  Division_Code = '" + div_code + "') where p.Product_Active_Flag=0 and p.Division_Code = '" + div_code + "' ";

            strQry = " select  p.product_Detail_Code,product_Detail_Name,product_unit,Sample_Erp_Code,isnull(rtrim(MRP_Price),0) as DP_Base_Rate, " +
                       " isnull(rtrim(Retailor_Price),0) as RP_Base_Rate ,isnull(rtrim(Distributor_Price),0) as DP_Case_Rate," +
                       " isnull(rtrim(NSR_Price),0) as RP_Case_Rate,isnull(rtrim(Target_Price),0) as MRP_Rate,isnull(rtrim(Distributor_Discount_Price),0) as Distributor_Discount_Price ,isnull(rtrim(Retailer_Discount_Price),0)  Retailer_Discount_Price From Mas_Product_Detail p left outer " +
                       " join Mas_Product_State_Rates R on R.product_Detail_code=P.Product_Detail_code " +
                       " and R.state_code =1 where Product_Active_Flag=0 and p.Division_code='" + div_code + "' " +
                       " ORDER BY product_Detail_Name";

            try
            {
                dsRate = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsRate;
        }
        public DataSet getProdRate(string state_code, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            strQry = " select  p.product_Detail_Code,product_Detail_Name,Product_Sale_Unit,isnull(MRP_Price,0) MRP_Price, " +
                     " isnull(Retailor_Price,0) Retailor_Price,isnull(Distributor_Price,0) Distributor_Price, " +
                     " isnull(NSR_Price,0) NSR_Price,isnull(Target_Price,0) Target_Price " +
                     " From Mas_Product_Detail p left outer join Mas_Product_State_Rates R " +
                     " on R.product_Detail_code=p.Product_Detail_code and " +
                     " Max_State_Sl_No in (select max(Max_State_Sl_No) from mas_Product_State_Rates " +
                     " where state_code='" + state_code + "' and division_code= '" + div_code + "') " +
                     " where Product_Active_Flag=0 and p.Division_code= '" + div_code + "' and p.state_code  like '%" + state_code + "%'" +
                     " ORDER BY Prod_Detail_Sl_No";
            try
            {
                dsProCat = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProCat;
        }
        // Sorting For ProductRate 
        public DataTable getProductRatelist_DataTable(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dtProCat = null;

            strQry = " select  p.product_Detail_Code,product_Detail_Name,Product_Sale_Unit,isnull(MRP_Price,0) MRP_Price, " +
                     " isnull(Retailor_Price,0) Retailor_Price,isnull(Distributor_Price,0) Distributor_Price, " +
                     " isnull(NSR_Price,0) NSR_Price,isnull(Target_Price,0) Target_Price " +
                     " From Mas_Product_Detail p left outer join Mas_Product_State_Rates R " +
                     " on R.product_Detail_code=p.Product_Detail_code and " +
                     " Max_State_Sl_No in (select max(Max_State_Sl_No) from mas_Product_State_Rates " +
                     " where division_code= '" + div_code + "') " +
                     " where Product_Active_Flag=0 and p.Division_code= '" + div_code + "' " +
                     " ORDER BY product_Detail_Name";
            try
            {
                dtProCat = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtProCat;
        }

        public DataSet getProCate(string divcode, string ProdCatcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            strQry = " SELECT Product_Cat_SName,Product_Cat_Name,Product_Cat_Div_Name,Product_Cat_Div_Code FROM  Mas_Product_Category " +
                     " WHERE Product_Cat_Code= '" + ProdCatcode + "'AND Division_Code= '" + divcode + "' " +
                     " ORDER BY 2";
            try
            {
                dsProCat = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProCat;
        }

        public DataSet getProCat(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            strQry = " SELECT c.Product_Cat_Code,c.Product_Cat_SName,c.Product_Cat_Name,c.Product_Cat_Div_Name, " +
                     " (select COUNT(p.Product_Cat_Code) from Mas_Product_Detail p where p.Product_Active_Flag=0 and p.Product_Cat_Code = c.Product_Cat_Code ) as cat_count   FROM  Mas_Product_Category c" +
                     " WHERE c.Product_Cat_Active_Flag=0 AND Division_Code= '" + divcode + "' " +
                     " ORDER BY c.ProdCat_SNo";
            try
            {
                dsProCat = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProCat;
        }
        // Sorting For ProductCategoryList 
        public DataTable getProductCategorylist_DataTable(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dtProCat = null;

            //strQry = " SELECT Product_Cat_Code,Product_Cat_SName,Product_Cat_Name FROM  Mas_Product_Category " +
            //         " WHERE Product_Cat_Active_Flag=0 AND Division_Code= '" + divcode + "' " +
            //         " ORDER BY 2";

            strQry = " SELECT c.Product_Cat_Code,c.Product_Cat_SName,c.Product_Cat_Name, " +
                   " (select COUNT(p.Product_Cat_Code) from Mas_Product_Detail p where p.Product_Active_Flag=0 and p.Product_Cat_Code = c.Product_Cat_Code ) as cat_count   FROM  Mas_Product_Category c" +
                   " WHERE c.Product_Cat_Active_Flag=0 AND Division_Code= '" + divcode + "' " +
                   " ORDER BY c.ProdCat_SNo";
            try
            {
                dtProCat = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtProCat;
        }
        //..test
        public DataSet getProductCategory(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            strQry = " SELECT '' as Product_Cat_Code, '---Select---' as Product_Cat_Name " +
                     " UNION " +
                     " SELECT Product_Cat_Code,Product_Cat_Name FROM  Mas_Product_Category " +
                     " WHERE Product_Cat_Active_Flag=0 AND Product_Cat_Code> 0 AND Division_Code= '" + divcode + "' " +
                     " ORDER BY 2";
            try
            {
                dsProCat = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProCat;
        }

        public DataSet FillProductGroup(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            strQry = " SELECT '' as Product_Grp_Code, '---Select---' as Product_Grp_Name " +
                     " UNION " +
                     " SELECT Product_Grp_Code,Product_Grp_Name FROM  Mas_Product_Group " +
                     " WHERE Product_Grp_Active_Flag=0 AND Division_Code= '" + divcode + "' " +
                     " ORDER BY 2";

            try
            {
                dsProCat = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProCat;
        }




        public DataSet getProductForCategory(string divcode, string cat_code, string nil_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            //strQry = " SELECT Product_Detail_Code, Product_Cat_Code, Product_Detail_Name " +
            //         " FROM  Mas_Product_Detail " +
            //         " WHERE Product_Active_Flag=0 AND Division_Code= '" + divcode + "' " +
            //         " AND Product_Cat_Code IN ('" + cat_code + "','" + nil_code + "') " +
            //         " ORDER BY 2";

            strQry = "SELECT a.Product_Detail_Code, a.Product_Cat_Code, a.Product_Detail_Name + '-' + b.Product_Cat_Name as Product_Detail_Name " +
                     " FROM  Mas_Product_Detail a, Mas_Product_Category b " +
                     " WHERE a.Product_Cat_Code = b.Product_Cat_Code AND a.Product_Active_Flag=0  " +
                     " AND a.Product_Cat_Code IN ('" + cat_code + "','" + nil_code + "') " +
                     " AND a.Division_Code= '" + divcode + "' " +
                     " ORDER BY 3";

            try
            {
                dsProCat = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProCat;
        }

        public DataSet getProGroup(string divcode, string prodgrpcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            strQry = " SELECT Product_Grp_SName,Product_Grp_Name FROM  Mas_Product_Group " +
                     " WHERE Product_Grp_Code=  '" + prodgrpcode + "' AND Division_Code= '" + divcode + "' " +
                     " ORDER BY 2";
            try
            {
                dsProCat = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProCat;
        }
        // Sorting For ProductGroupList 
        public DataTable getProductGrouplist_DataTable(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dtProCat = null;

            //strQry = " SELECT Product_Grp_Code,Product_Grp_SName,Product_Grp_Name FROM  Mas_Product_Group " +
            //         " WHERE  Product_Grp_Active_Flag=0 AND Division_Code= '" + divcode + "' " +
            //         " ORDER BY 2";
            strQry = " SELECT c.Product_Grp_Code,c.Product_Grp_SName,c.Product_Grp_Name, " +
                 " (select COUNT(p.Product_Grp_Code) from Mas_Product_Detail p where p.Product_Active_Flag=0 and p.Product_Grp_Code = c.Product_Grp_Code ) as Grp_count   FROM  Mas_Product_Group c" +
                 " WHERE c.Product_Grp_Active_Flag=0 AND Division_Code= '" + divcode + "' " +
                 " ORDER BY c.ProdGrp_Sl_No";
            try
            {
                dtProCat = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtProCat;
        }
        //...
        public DataSet getProductGroup(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            strQry =
                     " SELECT Product_Grp_Code,Product_Grp_Name FROM  Mas_Product_Group " +
                     " WHERE Product_Grp_Active_Flag=0 AND Division_Code= '" + divcode + "' " +
                     " ORDER BY 2";
            try
            {
                dsProCat = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProCat;
        }

        public DataSet getProductRate_sf(string sf_code, string div_code, string state, string subdiv)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            //strQry = " select distinct b.Product_Detail_Code,b.Product_Description,b.Product_Detail_Name,b.Product_Sale_Unit, " +
            //         " c.MRP_Price,c.Retailor_Price,c.Distributor_Price,c.Target_Price " +
            //         " from Mas_Salesforce a, Mas_Product_Detail b, Mas_Product_State_Rates c " +
            //         " where a.Sf_Code = '" + sf_code + "' and b.Division_Code= '" + div_code + "'  " +
            //         " and b.Product_Active_Flag=0 and b.Product_Detail_Code = c.Product_Detail_Code " +
            //         " and b.Division_Code = c.Division_Code and a.State_Code = c.State_Code " +
            //         " ORDER BY 2";
            //strQry =   " select distinct b.Product_Detail_Code,b.Product_Description,b.Product_Detail_Name," +
            //          " b.Product_Sale_Unit,  isnull(rtrim(MRP_Price),0) MRP_Price,  isnull(rtrim(Retailor_Price),0) Retailor_Price,isnull(rtrim(Distributor_Price),0) Distributor_Price, " +
            //          " isnull(rtrim(NSR_Price),0) NSR_Price,isnull(rtrim(Target_Price),0) Target_Price  From Mas_Product_Detail b" +
            //          " left outer join Mas_Product_State_Rates c  on b.Product_Detail_Code = c.Product_Detail_Code  " +
            //      //    " and  Sl_No  in (select max(Max_State_Sl_No) from mas_Product_State_Rates) " +
            //          " where  b.Division_Code= '" + div_code + "' and b.Product_Active_Flag = 0" +
            //          " and  (b.state_code like '" + state + ',' + "%'  or b.state_code like '%" + ',' + state + ',' + "%') " ;
            //strQry = " select * " +
            //            " from Mas_Product_State_Rates where Division_Code = '" + div_code + "' and (state_code like '" + state + ',' + "%'  or state_code like '%" + ',' + state + ',' + "%' or state_code like '%" + ',' + state + "') ";


            strQry = " select * from Mas_Product_Detail where Division_Code = '" + div_code + "' and (state_code like '" + state + ',' + "%'  or state_code like '%" + ',' + state + ',' + "%' or state_code like '%" + ',' + state + "' or state_code like '" + state + "') ";

            dsProCat = db_ER.Exec_DataSet(strQry);

            if (dsProCat.Tables[0].Rows.Count > 0)
            {
                if (subdiv.Contains(','))
                    subdiv = subdiv.Substring(0, subdiv.Length - 1);
                strQry = " SELECT DISTINCT b.Product_Detail_Code, " +
                    " b.Product_Description, " +
                    " b.Product_Detail_Name," +
                    " b.Product_Sale_Unit,  " +
                    " isnull(rtrim(MRP_Price),0) MRP_Price,  " +
                    " isnull(rtrim(Retailor_Price),0) Retailor_Price, " +
                    " isnull(rtrim(Distributor_Price),0) Distributor_Price, " +
                    " isnull(rtrim(NSR_Price),0) NSR_Price, " +
                    " isnull(rtrim(Target_Price),0) Target_Price " +
                    " From Mas_Product_Detail b" +
                    " INNER JOIN Mas_Product_State_Rates c  " +
                    " ON b.Product_Detail_Code = c.Product_Detail_Code  " +
                    " WHERE b.Division_Code= '" + div_code + "' " +
                    " AND b.Product_Active_Flag = 0" +
                    " AND c.state_code = '" + state + "' " +
                    " AND (b.state_code like '" + state + ',' + "%'  or b.state_code like '%" + ',' + state + ',' + "%' or b.state_code like '%" + ',' + state + "') " +
                     " And (b.subdivision_code like '" + subdiv + ',' + "%'  or b.subdivision_code like '%" + ',' + subdiv + ',' + "%' or b.subdivision_code like '%" + ',' + subdiv + "')";
            }

            else
            {
                strQry = " select distinct b.Product_Detail_Code,b.Product_Description,b.Product_Detail_Name," +
                          " b.Product_Sale_Unit,  '0' as MRP_Price,  '0'as Retailor_Price,'0' as Distributor_Price, " +
                          " '0' as NSR_Price,'0' as Target_Price  From Mas_Product_Detail b" +
                          " where  b.Division_Code= '" + div_code + "' and b.Product_Active_Flag = 0" +
                          " and  (b.state_code like '" + state + ',' + "%'  or b.state_code like '%" + ',' + state + ',' + "%'  or b.state_code like '%" + ',' + state + "') " +
                            " And (b.subdivision_code like '" + subdiv + ',' + "%'  or b.subdivision_code like '%" + ',' + subdiv + ',' + "%' or b.subdivision_code like '%" + ',' + subdiv + "')";

            }

            try
            {
                dsProCat = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProCat;
        }

        public DataTable getProductRate_DataTable(string sf_code, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dtProCat = null;

            strQry = " select distinct b.Product_Detail_Code,b.Product_Description,b.Product_Detail_Name,b.Product_Sale_Unit, " +
                     " c.MRP_Price,c.Retailor_Price,c.Distributor_Price,c.Target_Price " +
                     " from Mas_Salesforce a, Mas_Product_Detail b, Mas_Product_State_Rates c " +
                     " where a.Sf_Code = '" + sf_code + "' and b.Division_Code = '" + div_code + "' " +
                     " and b.Product_Active_Flag=0 and b.Product_Detail_Code = c.Product_Detail_Code " +
                     " and b.Division_Code = c.Division_Code and a.State_Code = c.State_Code " +
                     " ORDER BY 2";
            try
            {
                dtProCat = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtProCat;
        }

        public bool RecordExist(string Product_Cat_SName, string div_code)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(Product_Cat_SName) FROM Mas_Product_Category WHERE Product_Cat_SName='" + Product_Cat_SName + "' and Division_Code = '" + div_code + "' ";
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
        //changes done by Priya
        //public bool sRecordExist(string Product_Cat_Name, string div_code)
        //{

        //    bool bRecordExist = false;
        //    try
        //    {
        //        DB_EReporting db = new DB_EReporting();

        //        strQry = "SELECT COUNT(Product_Cat_Name) FROM Mas_Product_Category WHERE Product_Cat_Name='" + Product_Cat_Name + "' and Division_Code = '" + div_code + "' ";
        //        int iRecordExist = db.Exec_Scalar(strQry);

        //        if (iRecordExist > 0)
        //            bRecordExist = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    return bRecordExist;
        //}
        public bool sRecordExist(int Product_Cat_Code, string Product_Cat_Name, string divcode)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(Product_Cat_Name) FROM Mas_Product_Category WHERE Product_Cat_Name = '" + Product_Cat_Name + "' AND Product_Cat_Code!='" + Product_Cat_Code + "' and Division_Code = '" + divcode + "'";

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
        public bool sRecordExist(int Product_Cat_Code, string Product_Cat_Name)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(Product_Cat_Name) FROM Mas_Product_Category WHERE Product_Cat_Name = '" + Product_Cat_Name + "' AND Product_Cat_Code!='" + Product_Cat_Code + "' ";

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


        //end

        //public bool RecordExist(int Product_Cat_Code, string Product_Cat_SName)
        //{

        //    bool bRecordExist = false;
        //    try
        //    {
        //        DB_EReporting db = new DB_EReporting();

        //        strQry = "SELECT COUNT(Product_Cat_SName) FROM Mas_Product_Category WHERE Product_Cat_SName = '" + Product_Cat_SName + "' AND Product_Cat_Code!='" + Product_Cat_Code + "' ";

        //        int iRecordExist = db.Exec_Scalar(strQry);

        //        if (iRecordExist > 0)
        //            bRecordExist = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    return bRecordExist;
        //}
        public bool RecordExist(int Product_Cat_Code, string Product_Cat_SName, string divcode)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(Product_Cat_SName) FROM Mas_Product_Category WHERE Product_Cat_SName = '" + Product_Cat_SName + "' AND Product_Cat_Code!='" + Product_Cat_Code + "' AND Division_Code= '" + divcode + "' ";

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

        public bool RecordExistdet(string Product_Detail_Code, int divcode)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(Product_Detail_Code) FROM Mas_Product_Detail WHERE Product_Detail_Code='" + Product_Detail_Code + "' AND Division_Code= '" + divcode + "' ";
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
        public bool sRecordExistdet(string Product_Detail_Name, int divcode)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(Product_Detail_Name) FROM Mas_Product_Detail WHERE Product_Detail_Name='" + Product_Detail_Name + "' AND Division_Code= '" + divcode + "' ";
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


        //Record Exist

        public bool RecordExistGiftSN(string GiftSName)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(Gift_SName) FROM Mas_Gift WHERE Gift_SName='" + GiftSName + "' ";
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
        public bool RecordExistGiftN(string GiftName)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(Gift_Name) FROM Mas_Gift WHERE Gift_Name='" + GiftName + "' ";
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

        public bool snRecordExist(string Gift_Code, string Gift_SName)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(Gift_Code) FROM mas_Gift WHERE Gift_Code != '" + Gift_Code + "' AND Gift_SName='" + Gift_SName + "' ";

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
        public bool nRecordExist(string Gift_Code, string Gift_Name)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(Gift_Code) FROM mas_Gift WHERE Gift_Code != '" + Gift_Code + "' AND Gift_Name='" + Gift_Name + "' ";

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
        //end
        // Insert Product Reminder 


        public int RecordAddGift(string GiftSName, string GiftName, int GiftType, string Giftvalue, DateTime EffFrom, DateTime EffTo, int Division_Code, string State_Code, string subdivision_code)
        {
            int iReturn = -1;
            //    if (!RecordExist(GiftName,GiftType,EffFrom,EffTo,Division_Code, state))
            //   {

            if (!RecordExistGiftSN(GiftSName))
            {
                if (!RecordExistGiftN(GiftName))
                {
                    try
                    {
                        DB_EReporting db = new DB_EReporting();

                        strQry = "SELECT isnull(max(Gift_Code)+1,'1') Gift_Code from Mas_Gift ";
                        int Gift_Code = db.Exec_Scalar(strQry);

                        string EffFromdate = EffFrom.Month.ToString() + "-" + EffFrom.Day + "-" + EffFrom.Year;
                        string EffTodate = EffTo.Month.ToString() + "-" + EffTo.Day + "-" + EffTo.Year;

                        strQry = "INSERT INTO Mas_Gift(Gift_Code,Gift_SName,Gift_Name,Gift_Type,Gift_Value,Gift_Effective_From,Gift_Effective_To,Division_Code,State_Code,subdivision_code, Created_Date,Gift_Active_flag,LastUpdt_Date)" +
                                 "values('" + Gift_Code + "','" + GiftSName + "','" + GiftName + "', '" + GiftType + "' , '" + Giftvalue + "' , " +
                                 " '" + EffFromdate + "' ,'" + EffTodate + "', " + Division_Code + "," +
                                 " '" + State_Code + "','" + subdivision_code + "', getdate(), '0',getdate()) ";

                        // ",getdate(),getdate()," + Division_Code + ", getdate(), '0')";


                        iReturn = db.ExecQry(strQry);
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
            }
            else
            {
                iReturn = -3;
            }

            return iReturn;
        }





        public bool RecordExist(string GiftName, int GiftType, DateTime EffFrom, DateTime EffTo, int divcode, string state)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(Gift_Name) FROM Mas_Gift WHERE " +
                            " Gift_Name = '" + GiftName + "' AND Gift_Type =" + GiftType + "  " +
                            " AND Gift_Effective_From ='" + EffFrom + "' " +
                            " AND Gift_Effective_To = '" + EffTo + "' AND Division_Code =" + divcode + " AND state_code = '" + state + "' ";

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
        public bool RecordExistgift(string GiftName, int GiftType, int divcode)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(Gift_Name) FROM Mas_Gift WHERE " +
                            " Gift_Name = '" + GiftName + "' AND Gift_Type =" + GiftType + " " +
                            " AND Division_Code =" + divcode + " ";

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


        public int RecordAdd(string divcode, string Product_Cat_SName, string Product_Cat_Name, string Pro_Div_code, string Pro_Div_name)
        {
            int iReturn = -1;
            if (!RecordExist(Product_Cat_SName, divcode))
            {
                if (!sRecordExist(Product_Cat_Name, divcode))
                {
                    try
                    {
                        DB_EReporting db = new DB_EReporting();
                        strQry = "SELECT isnull(max(Product_Cat_Code)+1,'1') Product_Cat_Code from Mas_Product_Category ";
                        int Product_Cat_Code = db.Exec_Scalar(strQry);

                        strQry = "INSERT INTO Mas_Product_Category(Product_Cat_Code,Division_Code,Product_Cat_SName,Product_Cat_Name,Product_Cat_Div_Code,Product_Cat_Div_Name,Product_Cat_Active_Flag,Created_Date,LastUpdt_Date)" +
                                 "values('" + Product_Cat_Code + "','" + divcode + "',N'" + Product_Cat_SName + "', N'" + Product_Cat_Name + "','" + Pro_Div_code + "','" + Pro_Div_name + "',0,getdate(),getdate())";


                        iReturn = db.ExecQry(strQry);
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
            }
            else
            {
                iReturn = -3;
            }
            return iReturn;
        }
        public bool sRecordExist(string Product_Cat_Name, string div_code)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(Product_Cat_Name) FROM Mas_Product_Category WHERE Product_Cat_Name=N'" + Product_Cat_Name + "' and Division_Code = '" + div_code + "' ";
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
        //public int RecordUpdate(int Product_Cat_Code, string Product_Cat_SName, string Product_Cat_Name)
        //{
        //    int iReturn = -1;
        //    if (!RecordExist(Product_Cat_Code, Product_Cat_SName))
        //    {
        //        if (!sRecordExist(Product_Cat_Code, Product_Cat_Name))
        //        {
        //        try
        //        {

        //            DB_EReporting db = new DB_EReporting();

        //            strQry = "UPDATE Mas_Product_Category " +
        //                     " SET Product_Cat_SName = '" + Product_Cat_SName + "', " +
        //                     " Product_Cat_Name = '" + Product_Cat_Name + "' ," +
        //                     " LastUpdt_Date = getdate() " +
        //                     " WHERE Product_Cat_Code = '" + Product_Cat_Code + "' ";

        //            iReturn = db.ExecQry(strQry);
        //        }
        //        catch (Exception ex)
        //        {
        //            throw ex;
        //        }
        //     }
        //        else
        //        {
        //            iReturn = -2;
        //        }
        //    }
        //    else
        //    {
        //        iReturn = -3;
        //    }
        //    return iReturn;
        //}
        //....re
        public int RecordUpdate(int Product_Cat_Code, string Product_Cat_SName, string Product_Cat_Name, string divcode, string Pro_Div_code, string Pro_Div_name)
        {
            int iReturn = -1;
            if (!RecordExist(Product_Cat_Code, Product_Cat_SName, divcode))
            {
                if (!sRecordExist(Product_Cat_Code, Product_Cat_Name, divcode))
                {
                    try
                    {

                        DB_EReporting db = new DB_EReporting();

                        strQry = "UPDATE Mas_Product_Category " +
                                 " SET Product_Cat_SName = N'" + Product_Cat_SName + "', " +
                                 " Product_Cat_Name = N'" + Product_Cat_Name + "' ," +
                                 " Product_Cat_Div_Name = '" + Pro_Div_name + "' ," +
                                 " Product_Cat_Div_Code = '" + Pro_Div_code + "' ," +
                                 " LastUpdt_Date = getdate() " +
                                 " WHERE Product_Cat_Code = '" + Product_Cat_Code + "' and Product_Cat_Active_Flag = 0 ";

                        iReturn = db.ExecQry(strQry);

                       /* strQry = "UPDATE Mas_Product_Brand SET Product_Cat_Name = '" + Product_Cat_Name + "' WHERE Product_Cat_Code = '" + Product_Cat_Code + "' ";
                        iReturn = db.ExecQry(strQry);

                        strQry = "UPDATE Mas_Product_Brand SET Product_Cat_Div_Name = '" + Pro_Div_name + "' WHERE Product_Cat_Div_Code = '" + Pro_Div_code + "' ";
                        iReturn = db.ExecQry(strQry);*/
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
            }
            else
            {
                iReturn = -3;
            }
            return iReturn;
        }
        public int RecordUpdateGift(string GiftCode, string GiftSName, string GiftName, int GiftType, string GiftVal, DateTime efffrom, DateTime effto, int divcode, string State_Code, string subdivision_code)
        {
            int iReturn = -1;
            // if (!RecordExistgift(GiftName, GiftType, divcode))
            // {
            if (!snRecordExist(GiftCode, GiftSName))
            {
                if (!nRecordExist(GiftCode, GiftName))
                {
                    try
                    {

                        DB_EReporting db = new DB_EReporting();

                        strQry = "UPDATE Mas_Gift " +
                                 " SET Gift_Name = '" + GiftName + "', " +
                                 " Gift_SName = '" + GiftSName + "', " +
                                 " Gift_Value = '" + GiftVal + "', " +
                                 " Gift_Type = " + GiftType + " ," +
                            //" StateCode  = " +state +"," +
                                 " Gift_Effective_From = '" + efffrom.Month + '-' + efffrom.Day + '-' + efffrom.Year + "'," +
                                 " Gift_Effective_To = '" + effto.Month + '-' + effto.Day + '-' + effto.Year + "' , " +
                                 " State_Code = '" + State_Code + "', subdivision_code='" + subdivision_code + "', " +
                                 " LastUpdt_Date = getdate() " +
                                 " WHERE Gift_Code = '" + GiftCode + "'  AND  Division_Code=" + divcode + " ";

                        iReturn = db.ExecQry(strQry);
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
            }
            else
            {
                iReturn = -3;
            }
            return iReturn;

        }
        public int RecordUpdateGift(string GiftCode, string GiftName, string GiftSName, string GiftVal, int GiftType, int divcode)
        {
            int iReturn = -1;
            // if (!RecordExistgift(GiftName, GiftType,divcode))
            //  {
            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "UPDATE Mas_Gift " +
                         " SET Gift_Name = '" + GiftName + "', " +
                         " Gift_SName = '" + GiftSName + "', " +
                         " Gift_Value = '" + GiftVal + "', " +
                         " LastUpdt_Date = getdate() " +
                         " WHERE Gift_Code = '" + GiftCode + "' AND Gift_Type = '" + GiftType + "' AND  Division_Code= " + divcode + " ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            // }
            //   else
            //  {
            // iReturn = -2;
            //  }
            return iReturn;

        }

        public int RecordUpdateProd(string Product_Detail_Code, string Product_Detail_Name, string ProdDescr, string divcode)
        {
            int iReturn = -1;
            if (!sRecordExistDetail(Product_Detail_Name, Product_Detail_Code, divcode))
            {
                try
                {

                    DB_EReporting db = new DB_EReporting();

                    strQry = "UPDATE Mas_Product_Detail " +
                             " SET Product_Detail_Name = '" + Product_Detail_Name + "', " +

                             " Product_Description ='" + ProdDescr + "'," +
                             " LastUpdt_Date = getdate() " +
                             " WHERE Product_Detail_Code = '" + Product_Detail_Code + "' ";

                    iReturn = db.ExecQry(strQry);
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

        public int getMaxStateSlNo(string state_code, string div_code)
        {
            int iReturn = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT ISNULL(MAX(Max_State_Sl_No),0)+1 FROM mas_Product_State_Rates WHERE state_code = '" + state_code + "' AND Division_Code='" + div_code + "' ";
                iReturn = db.Exec_Scalar(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }


        public int UpdateProductRate(string prod_code, string state_code, string effective_from, decimal mrp_amt, decimal ret_amt, decimal dist_amt, decimal nsr_amt, decimal target_amt, string div_code, int iStateSlNo, decimal distrbutor_discout_amt, decimal retailer_discount_amt, decimal SS_Base_Rate, decimal SS_Case_Rate)
        {
            int iReturn = -1;

            int iSlNo = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT ISNULL(MAX(Sl_No),0)+1 FROM mas_Product_State_Rates ";
                iSlNo = db.Exec_Scalar(strQry);

                strQry = "INSERT INTO mas_Product_State_Rates (Sl_No, Max_State_Sl_No, State_Code, Product_Detail_Code, MRP_Price, Retailor_Price, " +
                         " Distributor_Price, Target_Price, NSR_Price, Effective_From_Date, Division_Code, Created_Date,LastUpdt_Date,Distributor_Discount_Price,Retailer_Discount_Price,SS_Base_Rate,SS_Case_Rate) VALUES " +
                         " ( '" + iSlNo + "', '" + iStateSlNo + "', '" + state_code + "', '" + prod_code + "', '" + mrp_amt + "', '" + ret_amt + "', '" + dist_amt + "', " +
                         " '" + target_amt + "', '" + nsr_amt + "', '" + effective_from.Substring(6, 4) + "-" + effective_from.Substring(3, 2) + "-" + effective_from.Substring(0, 2) + "', '" + div_code + "', getdate(),getdate(),'" + distrbutor_discout_amt + "','" + retailer_discount_amt + "','" + SS_Base_Rate + "','" + SS_Case_Rate + "')";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }




        public int RecordUpdate_NilCode(string Product_Detail_Code, string Nil_Code, string div_code)
        {
            int iReturn = -1;
            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "UPDATE Mas_Product_Detail " +
                            " SET Product_Cat_Code = '" + Nil_Code + "' ," +
                            " LastUpdt_Date = getdate() " +
                            " WHERE Product_Detail_Code = '" + Product_Detail_Code + "' " +
                            " AND Division_Code = '" + div_code + "' ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;

        }


        public int UpdateProdSno(string ProdCode, string ProdSno)
        {
            int iReturn = -1;
            //if (!RecordExist(Product_Cat_Code, Product_Cat_SName))
            //{
            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "UPDATE Mas_Product_Detail " +
                         " SET Prod_Detail_Sl_No = '" + ProdSno + "', " +
                         " LastUpdt_Date = getdate() " +
                         " WHERE Product_Detail_Code = '" + ProdCode + "' ";

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

        public int DeActivate(String ProdCode)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "UPDATE Mas_Product_Detail " +
                            " SET Product_Active_Flag=1 ," +
                            " LastUpdt_Date = getdate() " +
                            " WHERE Product_Detail_Code = '" + ProdCode + "' ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;

        }
        public int DeActivateGift(String GiftCode)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "UPDATE Mas_Gift " +
                            " SET Gift_Active_Flag=1 ," +
                            " LastUpdt_Date = getdate() " +
                            " WHERE Gift_Code = '" + GiftCode + "' ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;

        }
        public int Insert_Pritarget(string sxml, string month, string year, string rsf, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();
            int iReturn = -1;
            strQry = "exec Insert_Pritarget '" + sxml + "','" + month + "','" + year + "','" + div_code + "','" + rsf + "'";
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
        public int Brd_RecordDelete(int ProBrdCode)
        {
            int iReturn = -1;
            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "DELETE FROM  Mas_Product_Brand WHERE Product_Brd_Code = '" + ProBrdCode + "' ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;

        }

        public int BulkEdit(string str, string Product_Code)
        {
            int iReturn = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();
                //strQry = "UPDATE Mas_Product_Detail " +
                //         " SET Product_Detail_Name = '" + ProductName + "', " +
                //         " Product_Sale_Unit = '" + Product_Sale_Unit + "', " +
                //           " Product_Sample_Unit_One = '" + Product_Sample_Unit_one + "', " +
                //             " Product_Sample_Unit_Two = '" + Product_Sample_Unit_Two + "', " +
                //               " Product_Sample_Unit_Three = '" + Product_Sample_Unit_Three + "', " +
                //                 " Product_Cat_Code = " + ProductCatCode + " ," +                               
                //             " Product_Type_Code = '" + Product_Type_Code + "', " +
                //             " Product_Description = '" + ProductDescr + "'," +
                //             " LastUpdt_Date = getdate() , " +
                //             " State_Code = '" + strstate + "',"+
                //             " subdivision_code = '" + strSubState + "' " +
                //         " WHERE Product_Detail_Code = '" + ProdCode + "' ";

                strQry = "UPDATE Mas_Product_Detail SET " + str + "  Where Product_Detail_Code='" + Product_Code + "'";
                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }


        public int DeActivate(int Product_Cat_Code)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "UPDATE Mas_Product_Category " +
                            " SET Product_Cat_Active_Flag=1 ," +
                           " LastUpdt_Date = getdate() " +
                            " WHERE Product_Cat_Code = '" + Product_Cat_Code + "' ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;

        }
        public DataSet getProGrp(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProGrp = null;

            //strQry = " SELECT Product_Grp_Code,Product_Grp_SName,Product_Grp_Name FROM  Mas_Product_Group " +
            //         " WHERE Product_Grp_Active_Flag=0 AND Division_Code= '" + divcode + "' " +
            //         " ORDER BY ProdGrp_Sl_No";

            strQry = " SELECT c.Product_Grp_Code,c.Product_Grp_SName,c.Product_Grp_Name, " +
                     " (select COUNT(p.Product_Grp_Code) from Mas_Product_Detail p where p.Product_Active_Flag=0 and  p.Product_Grp_Code = c.Product_Grp_Code ) as Grp_count   FROM  Mas_Product_Group c" +
                     " WHERE c.Product_Grp_Active_Flag=0 AND Division_Code= '" + divcode + "' " +
                     " ORDER BY c.ProdGrp_Sl_No";
            try
            {
                dsProGrp = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProGrp;
        }

        public bool RecordExistGrp(string Product_Grp_SName, string divcode)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(Product_Grp_SName) FROM Mas_Product_Group WHERE Product_Grp_SName='" + Product_Grp_SName + "' AND Division_Code= '" + divcode + "' ";
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
        public bool RecordExistGrp(int Product_Grp_Code, string Product_Grp_SName, string divcode)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(Product_Grp_SName) FROM Mas_Product_Group WHERE Product_Grp_SName = '" + Product_Grp_SName + "' AND Product_Grp_Code!='" + Product_Grp_Code + "' AND Division_Code= '" + divcode + "'";

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
        public bool sRecordExistGrp(string Product_Grp_Name, string divcode)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(Product_Grp_Name) FROM Mas_Product_Group WHERE Product_Grp_Name='" + Product_Grp_Name + "' AND Division_Code= '" + divcode + "' ";
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
        public bool sRecordExistGrp(int Product_Grp_Code, string Product_Grp_Name, string divcode)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(Product_Grp_Name) FROM Mas_Product_Group WHERE Product_Grp_Name = '" + Product_Grp_Name + "' AND Product_Grp_Code!='" + Product_Grp_Code + "' AND Division_Code= '" + divcode + "' ";

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
        public int getNilCode(string div_code)
        {

            int iReturn = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT Product_Cat_Code FROM Mas_Product_Category WHERE Division_Code='" + div_code + "' and Product_Cat_Active_Flag=2 ";
                iReturn = db.Exec_Scalar(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }

        public bool RecordExistGrp(int Product_Grp_Code, string Product_Grp_SName)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(Product_Grp_SName) FROM Mas_Product_Group WHERE Product_Grp_SName = '" + Product_Grp_SName + "' AND Product_Grp_Code!='" + Product_Grp_Code + "' ";

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
        public bool sRecordExistGrp(int Product_Grp_Code, string Product_Grp_Name)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(Product_Grp_Name) FROM Mas_Product_Group WHERE Product_Grp_Name = '" + Product_Grp_Name + "' AND Product_Grp_Code!='" + Product_Grp_Code + "' ";

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

        public int RecordAddGrp(string divcode, string Product_Grp_SName, string Product_Grp_Name)
        {
            int iReturn = -1;
            if (!RecordExistGrp(Product_Grp_SName, divcode))
            {
                if (!sRecordExistGrp(Product_Grp_Name, divcode))
                {
                    try
                    {
                        DB_EReporting db = new DB_EReporting();

                        strQry = "SELECT isnull(max(Product_Grp_Code)+1,'1') Product_Grp_Code from Mas_Product_Group ";
                        int Product_Grp_Code = db.Exec_Scalar(strQry);

                        strQry = "INSERT INTO Mas_Product_Group(Product_Grp_Code,Division_Code,Product_Grp_SName,Product_Grp_Name,Product_Grp_Active_Flag,Created_Date,LastUpdt_Date)" +
                                 "values('" + Product_Grp_Code + "','" + divcode + "','" + Product_Grp_SName + "', '" + Product_Grp_Name + "',0,getdate(),getdate())";


                        iReturn = db.ExecQry(strQry);
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
            }
            else
            {
                iReturn = -3;
            }
            return iReturn;
        }
        //public int RecordUpdateGrp(int Product_Grp_Code, string Product_Grp_SName, string Product_Grp_Name)
        //{
        //    int iReturn = -1;
        //   if (!RecordExistGrp(Product_Grp_Code,Product_Grp_SName))
        //    {
        //        if (!sRecordExistGrp(Product_Grp_Code,Product_Grp_Name))
        //        {
        //        try
        //        {

        //            DB_EReporting db = new DB_EReporting();

        //            strQry = "UPDATE Mas_Product_Group " +
        //                     " SET Product_Grp_SName = '" + Product_Grp_SName + "', " +
        //                     " Product_Grp_Name = '" + Product_Grp_Name + "', " +
        //                     " LastUpdt_Date = getdate() " +
        //                     " WHERE Product_Grp_Code = '" + Product_Grp_Code + "' ";

        //            iReturn = db.ExecQry(strQry);
        //        }
        //        catch (Exception ex)
        //        {
        //            throw ex;
        //        }
        //    }

        //        else
        //        {
        //            iReturn = -2;
        //        }
        //    }
        //    else
        //    {
        //        iReturn = -3;
        //    }
        //    return iReturn;
        //}

        public int RecordUpdateGrp(int Product_Grp_Code, string Product_Grp_SName, string Product_Grp_Name, string divcode)
        {
            int iReturn = -1;
            if (!RecordExistGrp(Product_Grp_Code, Product_Grp_SName, divcode))
            {
                if (!sRecordExistGrp(Product_Grp_Code, Product_Grp_Name, divcode))
                {
                    try
                    {

                        DB_EReporting db = new DB_EReporting();

                        strQry = "UPDATE Mas_Product_Group " +
                                 " SET Product_Grp_SName = '" + Product_Grp_SName + "', " +
                                 " Product_Grp_Name = '" + Product_Grp_Name + "', " +
                                 " LastUpdt_Date = getdate() " +
                                 " WHERE Product_Grp_Code = '" + Product_Grp_Code + "' ";

                        iReturn = db.ExecQry(strQry);
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
            }
            else
            {
                iReturn = -3;
            }
            return iReturn;
        }
        public int RecordDeleteGrp(int Product_Grp_Code)
        {
            int iReturn = -1;
            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "DELETE FROM  Mas_Product_Group WHERE Product_Grp_Code = '" + Product_Grp_Code + "' ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;

        }

        public DataSet getProdgift(string divcode, string sAlpha)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            strQry = " SELECT Gift_Code,Gift_SName,Gift_Name,Gift_Value,case when Gift_Type = '1' then 'Literature/Lable' " +
                       " else case when Gift_Type = '2' then 'Special Gift'" +
                       " else case when Gift_Type = '3' then 'Doctor Kit'" +
                       " else 'Ordinary Gift' " +
                       " end end end as Gift_Type," +
                       " Gift_Effective_From,Gift_Effective_To,State_Code" +
                       " FROM Mas_Gift  WHERE Division_Code=" +
                       "'" + divcode + "' AND LEFT(Gift_Name,1) = '" + sAlpha + "' AND Gift_Active_Flag='0'  " +
                        " and (Gift_Effective_From >= Convert(Date, GetDate(), 101) or Gift_Effective_To >= Convert(Date, GetDate(), 101)) " +
                        " ORDER BY 2";
            try
            {
                dsProCat = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProCat;
        }


        public DataSet getProdgift_Alphabet(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;
            strQry = "select '1' val,'All' Gift_Name " +
                     " union " +
                     " select distinct LEFT(Gift_Name,1) val, LEFT(Gift_Name,1) Gift_Name" +
                     " FROM Mas_Gift " +
                     " WHERE Division_Code = '" + divcode + "' " +
                      " and (Gift_Effective_From >= Convert(Date, GetDate(), 101) or Gift_Effective_To >= Convert(Date, GetDate(), 101)) " +
                     " AND Gift_Active_flag = 0 " +
                     " ORDER BY 1";
            try
            {
                dsProCat = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProCat;
        }
        public int DeActivateGrp(int Product_Grp_Code)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "UPDATE Mas_Product_Group " +
                            " SET Product_Grp_Active_Flag=1 ," +
                            " LastUpdt_Date = getdate() " +
                            " WHERE Product_Grp_Code = '" + Product_Grp_Code + "' ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;

        }

        //Changes done by Priya -15jul // 
        // Begin 

        public int RecordUpdateGift_new(string GiftCode, string GiftName, string GiftSName, string GiftVal, int GiftType, int divcode)
        {
            int iReturn = -1;
            // if (!RecordExistgift(GiftName, GiftType,divcode))
            //  {
            if (!snRecordExist(GiftCode, GiftSName))
            {
                if (!nRecordExist(GiftCode, GiftName))
                {
                    try
                    {

                        DB_EReporting db = new DB_EReporting();

                        strQry = "UPDATE Mas_Gift " +
                                 " SET Gift_Name = '" + GiftName + "', " +
                                 " Gift_SName = '" + GiftSName + "', " +
                                 " Gift_Value = '" + GiftVal + "', " +
                                " Gift_Type = '" + GiftType + "'," +
                                 " LastUpdt_Date = getdate() " +
                                 " WHERE Gift_Code = '" + GiftCode + "' AND  Division_Code= " + divcode + " ";

                        iReturn = db.ExecQry(strQry);
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
            }
            else
            {
                iReturn = -3;
            }
            return iReturn;
        }
        //end

        //Changes done by Priya
        //begin
        public DataSet getGift(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;


            strQry = "  SELECT Gift_Code,Gift_Name,Gift_SName,Gift_Value," +
                        " case when Gift_Type = '1' then 'Literature/Lable' " +
                        " else case when Gift_Type = '2' then 'Special Gift'" +
                        " else case when Gift_Type = '3' then 'Doctor Kit'" +
                        "else 'Ordinary Gift' " +
                        "end end end as Gift_Type" +
                        " FROM Mas_Gift  WHERE Division_Code=" +
                        "'" + divcode + "' AND Gift_Active_Flag='0'" +
                       " and (Gift_Effective_From >= Convert(Date, GetDate(), 101) or Gift_Effective_To >= Convert(Date, GetDate(), 101)) " +
                     " ORDER BY 2";
            try
            {
                dsProCat = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProCat;
        }

        public DataSet getGift_React(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            strQry = "  SELECT Gift_Code,Gift_Name,Gift_SName,Gift_Value," +
                      " case when Gift_Type = '1' then 'Literature/Lable' " +
                      " else case when Gift_Type = '2' then 'Special Gift'" +
                      " else case when Gift_Type = '3' then 'Doctor Kit'" +
                      "else 'Ordinary Gift' " +
                      "end end end as Gift_Type" +
                      " FROM Mas_Gift  WHERE Division_Code=" +
                      "'" + divcode + "' AND Gift_Active_Flag=0" +
                //" ORDER BY 2";
                    " and (Gift_Effective_From < DATEADD(day, DATEDIFF(day, 0, getDate()) - 1, 0)" +
                    " and Gift_Effective_To <  DATEADD(day, DATEDIFF(day, 0, getDate()),  0))" +
                    " order by 1 desc";


            try
            {
                dsProCat = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProCat;
        }
        //Changes done by Priya
        //begin
        // Sorting For ProductReminderList(i.e)-GiftList
        //public DataTable getGiftlist_DataTable(string divcode)
        //{
        //    DB_EReporting db_ER = new DB_EReporting();

        //    DataTable dtProCat = null;

        //    //strQry = "  SELECT Gift_Code,Gift_Name,Gift_SName,Gift_Value," +
        //    //            " case when Gift_Type = '1' then 'Literature/Lable' " +
        //    //            " else case when Gift_Type = '2' then 'Special Gift' else 'Doctor Kit' end end Gift_Type" +
        //    //            " FROM Mas_Gift  WHERE Division_Code=" +
        //    //            "'" + divcode + "' AND Gift_Active_Flag='0'" +
        //    //         " ORDER BY 2";
        //    strQry = "  SELECT Gift_Code,Gift_Name,Gift_SName,Gift_Value," +
        //                " case when Gift_Type = '1' then 'Literature/Lable' " +
        //                " else case when Gift_Type = '2' then 'Special Gift'" +
        //                " else case when Gift_Type = '3' then 'Doctor Kit'" +
        //                "else 'Ordinary Gift' " +
        //                "end end end as Gift_Type" +
        //                " FROM Mas_Gift  WHERE Division_Code=" +
        //                "'" + divcode + "' AND Gift_Active_Flag='0'" +
        //             " ORDER BY 2";
        //    try
        //    {
        //        dtProCat = db_ER.Exec_DataTable(strQry);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    return dtProCat;
        //}
        public DataTable getGiftlist_DataTable(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dtProCat = null;


            strQry = "  SELECT Gift_Code,Gift_Name,Gift_SName,Gift_Value," +
                        " case when Gift_Type = '1' then 'Literature/Lable' " +
                        " else case when Gift_Type = '2' then 'Special Gift'" +
                        " else case when Gift_Type = '3' then 'Doctor Kit'" +
                        "else 'Ordinary Gift' " +
                        "end end end as Gift_Type" +
                        " FROM Mas_Gift  WHERE Division_Code=" +
                        "'" + divcode + "' AND Gift_Active_Flag='0'" +
                        " and (Gift_Effective_From >= Convert(Date, GetDate(), 101) or Gift_Effective_To >= Convert(Date, GetDate(), 101)) " +
                     " ORDER BY 2";
            try
            {
                dtProCat = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtProCat;
        }


 public string UnitConv_AddRecord(string sxml, string div, string baseunit, string ProdCode)
        {
            DataSet ds = null;
            //int iReturn = -1;
            string msg = string.Empty;
            DB_EReporting db = new DB_EReporting();
                strQry = "exec UnitConv_AddRecord '" + sxml + "','" + div + "','"+baseunit+"','"+ ProdCode + "'";
                                        
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
        public DataTable getGiftlist_DataTable(string divcode, string sAlpha)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dtProCat = null;


            //strQry = "  SELECT Gift_Code,Gift_Name,Gift_SName,Gift_Value," +
            //            " case when Gift_Type = '1' then 'Literature/Lable' " +
            //            " else case when Gift_Type = '2' then 'Special Gift'" +
            //            " else case when Gift_Type = '3' then 'Doctor Kit'" +
            //            "else 'Ordinary Gift' " +
            //            "end end end as Gift_Type" +
            //            " FROM Mas_Gift  WHERE Division_Code=" +
            //            "'" + divcode + "' AND LEFT(Gift_Name,1) = '" + sAlpha + "' AND Gift_Active_Flag='0'" +
            //         " ORDER BY 2";
            strQry = "SELECT Gift_Code,Gift_Name,Gift_SName,Gift_Value," +
                    " case when Gift_Type = '1' then 'Literature/Lable' " +
                    " else case when Gift_Type = '2' then 'Special Gift'" +
                    " else case when Gift_Type = '3' then 'Doctor Kit'" +
                    "else 'Ordinary Gift' " +
                    "end end end as Gift_Type" +
                    " FROM Mas_Gift  WHERE Division_Code=" +
                    "'" + divcode + "' AND  Gift_Name like '" + sAlpha + "%'" +
                    "AND Gift_Active_Flag='0'" +
                      " and (Gift_Effective_From >= Convert(Date, GetDate(), 101) or Gift_Effective_To >= Convert(Date, GetDate(), 101)) " +
                 " ORDER BY 2";
            try
            {
                dtProCat = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtProCat;
        }
        //end


        //Reactivate sorting

        public DataTable getGiftlist_DataTable_React(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dtProCat = null;


            strQry = "  SELECT Gift_Code,Gift_Name,Gift_SName,Gift_Value," +
                        " case when Gift_Type = '1' then 'Literature/Lable' " +
                        " else case when Gift_Type = '2' then 'Special Gift'" +
                        " else case when Gift_Type = '3' then 'Doctor Kit'" +
                        "else 'Ordinary Gift' " +
                        "end end end as Gift_Type" +
                        " FROM Mas_Gift  WHERE Division_Code=" +
                        "'" + divcode + "' AND Gift_Active_Flag='0'" +
                      " and (Gift_Effective_From < DATEADD(day, DATEDIFF(day, 0, getDate()) - 1, 0)" +
                       " and Gift_Effective_To <  DATEADD(day, DATEDIFF(day, 0, getDate()),  0))" +
                     " ORDER BY 2";
            try
            {
                dtProCat = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtProCat;
        }

        //changes done by Saravanan
        public DataTable getGiftDateDiff(string divcode, DateTime efffrom, DateTime effto)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dtProCat = null;


            string EffFromdate = efffrom.Month.ToString() + "-" + efffrom.Day + "-" + efffrom.Year;
            string EffTodate = effto.Month.ToString() + "-" + effto.Day + "-" + effto.Year;

            strQry = " SELECT Gift_Code,Gift_Name,Gift_SName,Gift_Value," +
                     " case when Gift_Type = '1' then 'Literature/Lable' " +
                     " else case when Gift_Type = '2' then 'Special Gift'" +
                     " else case when Gift_Type = '3' then 'Doctor Kit'" +
                     " else 'Ordinary Gift' " +
                     " end end end as Gift_Type" +
                     " FROM Mas_Gift  WHERE Division_Code=" +
                     "'" + divcode + "' AND Gift_Active_Flag='0' AND" +
                //" Gift_Effective_From >= '" + EffFromdate + "' AND Gift_Effective_To <= '" + EffTodate + "'" +
                     " (Gift_Effective_From >= Convert(Date, GetDate(), 101) or Gift_Effective_To >= Convert(Date, GetDate(), 101))" +
                     " ORDER BY 2";
            try
            {
                dtProCat = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtProCat;
        }
        //end
        //Changes done by Priya
        //begin
        public DataSet getGift(string divcode, DateTime efffrom, DateTime effto)
        //  public DataSet getGift(string divcode, string efffrom, string effto)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            string EffFromdate = efffrom.Month.ToString() + "-" + efffrom.Day + "-" + efffrom.Year;
            string EffTodate = effto.Month.ToString() + "-" + effto.Day + "-" + effto.Year;

            //strQry = "  SELECT Gift_Code,Gift_Name,Gift_SName,Gift_Value," +
            //            " case when Gift_Type = '1' then 'Literature/Lable' " +
            //            " else case when Gift_Type = '2' then 'Special Gift' else 'Doctor Kit' end end Gift_Type" +
            //            " FROM Mas_Gift  WHERE Division_Code=" +
            //            "'" + divcode + "' AND Gift_Active_Flag='0' AND" +
            //            " Gift_Effective_From = '" + efffrom + "' AND Gift_Effective_To = '" + effto + "'" +
            //         " ORDER BY 2";

            strQry = "  SELECT Gift_Code,Gift_Name,Gift_SName,Gift_Value," +
                     " case when Gift_Type = '1' then 'Literature/Lable' " +
                     " else case when Gift_Type = '2' then 'Special Gift'" +
                     " else case when Gift_Type = '3' then 'Doctor Kit'" +
                     "else 'Ordinary Gift' " +
                     "end end end as Gift_Type" +
                     " FROM Mas_Gift  WHERE Division_Code=" +
                     "'" + divcode + "' AND Gift_Active_Flag='0' AND" +
                     " Gift_Effective_From = '" + EffFromdate + "' AND Gift_Effective_To = '" + EffTodate + "'" +
                     " ORDER BY 2";
            try
            {
                dsProCat = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProCat;
        }

        public DataSet getGift_React(string divcode, DateTime efffrom, DateTime effto)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            string EffFromdate = efffrom.Month.ToString() + "-" + efffrom.Day + "-" + efffrom.Year;
            string EffTodate = effto.Month.ToString() + "-" + effto.Day + "-" + effto.Year;

            strQry = "  SELECT Gift_Code,Gift_Name,Gift_SName,Gift_Value," +
                     " case when Gift_Type = '1' then 'Literature/Lable' " +
                     " else case when Gift_Type = '2' then 'Special Gift'" +
                     " else case when Gift_Type = '3' then 'Doctor Kit'" +
                     "else 'Ordinary Gift' " +
                     "end end end as Gift_Type" +
                     " FROM Mas_Gift  WHERE Division_Code=" +
                     "'" + divcode + "' AND Gift_Active_Flag='0' AND" +
                " Gift_Effective_From = '" + EffFromdate + "' AND Gift_Effective_To = '" + EffTodate + "'" +
                     " ORDER BY 2";
            try
            {
                dsProCat = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProCat;
        }
        //end



        //Changes done by Priya
        //begin
        public int ReActivate(String ProdCode)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "UPDATE Mas_Product_Detail " +
                            " SET Product_Active_Flag=0 ," +
                            " LastUpdt_Date = getdate() " +
                            " WHERE Product_Detail_Code = '" + ProdCode + "' ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;

        }

        //end

        //changes done by Priya//19 jul

        public DataSet getProdforstat(string sf_code, string val, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            //strQry = " SELECT a.Product_Detail_Code,a.Product_Detail_Name,a.Product_Sale_Unit,a.Product_Description, " +
            //         " a.Product_Sample_Unit_One,a.Product_Sample_Unit_Two,a.Product_Sample_Unit_Three,b.Product_Cat_Name,c.Product_Grp_Name" +
            //         " FROM  Mas_Product_Detail a,Mas_Product_Category b,Mas_Product_Group c" +
            //         " WHERE a.product_cat_code = b.product_cat_code AND " +
            //         " a.Product_Grp_Code = c.product_grp_code AND " +
            //         " a.product_cat_code = '" + val + "' AND " +
            //         " a.Product_Active_Flag=0 AND a.Division_Code= '" + divcode + "' " +
            //         " ORDER BY 2";
            strQry = " SELECT a.Product_Detail_Code,a.Product_Detail_Name,a.Product_Sale_Unit,a.Product_Description, " +
                    "  b.Product_Cat_Name,c.Product_Grp_Name" +
                    " FROM  Mas_Product_Detail a,Mas_Product_Category b,Mas_Product_Group c, Mas_Product_State_Rates d, Mas_Salesforce e" +
                    " WHERE a.product_cat_code = b.product_cat_code AND " +
                    " a.Product_Grp_Code = c.product_grp_code AND " +
                    " a.Product_Active_Flag=0 AND a.Product_Detail_Code = d.Product_Detail_Code AND " +
                    " d.State_Code = e.State_Code AND a.Division_Code= '" + div_code + "' AND " +
                    " d.State_Code= '" + val + "' AND " +
                    " e.Sf_Code = '" + sf_code + "' " +
                    " ORDER BY 2";
            try
            {
                dsProCat = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProCat;
        }
        //end

        //Changes done by Priya 

        public int Update_ProdCatSno(string Product_Cat_Code, string Sno)
        {
            int iReturn = -1;
            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "UPDATE Mas_Product_Category " +
                         " SET ProdCat_SNo = '" + Sno + "', " +
                         " LastUpdt_Date = getdate() " +
                         " WHERE Product_Cat_Code = '" + Product_Cat_Code + "' ";

                iReturn = db.ExecQry(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }
        //end

        //Changes done by Priya--jul21
        public int Update_ProdGrpSno(string Product_Grp_Code, string Sno)
        {
            int iReturn = -1;
            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "UPDATE Mas_Product_Group " +
                         " SET ProdGrp_Sl_No = '" + Sno + "', " +
                         " LastUpdt_Date = getdate() " +
                         " WHERE Product_Grp_Code = '" + Product_Grp_Code + "' ";

                iReturn = db.ExecQry(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }
        //end
        //Changes done by Priya--jul24
        public DataSet getGiftName(string divcode, string Name)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet dsProCat = null;
            {
                strQry = "  SELECT Gift_Code,Gift_Name,Gift_SName,Gift_Value," +
                       " case when Gift_Type = '1' then 'Literature/Lable' " +
                       " else case when Gift_Type = '2' then 'Special Gift'" +
                       " else case when Gift_Type = '3' then 'Doctor Kit'" +
                       "else 'Ordinary Gift' " +
                       "end end end as Gift_Type" +
                       " FROM Mas_Gift  WHERE Division_Code=" +
                       "'" + divcode + "' AND  Gift_Name like '" + Name + "%'" +
                       "AND Gift_Active_Flag='0'" +
                         " and (Gift_Effective_From >= Convert(Date, GetDate(), 101) or Gift_Effective_To >= Convert(Date, GetDate(), 101)) " +
                    " ORDER BY 2";
                try
                {
                    dsProCat = db_ER.Exec_DataSet(strQry);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                return dsProCat;
            }
        }

        //end
        //Changes done by Priya --jul24
        public DataSet FetchState(string divcode, string state_code)
        {
            DB_EReporting db_ER = new DB_EReporting();
            int div_code = -1;

            DataSet dsProCat = null;

            strQry = "select state_code from Mas_State where state_code = '" + state_code + "' ";
            div_code = db_ER.Exec_Scalar(strQry);

            strQry = " SELECT 0 as State_Code,'---Select---' as State_Name " +
                         " UNION " +
                     " SELECT State_Code,State_Name " +
                     " FROM  Mas_State where division_Code = '" + div_code + "' AND state_active_flag=0 ";
            try
            {
                dsProCat = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProCat;
        }
        //End
        public DataSet getGifttype(string div_code, string Gift_Type)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet dsProCat = null;
            {
                strQry = "  SELECT Gift_Code,Gift_Name,Gift_SName,Gift_Value" +

                       " FROM Mas_Gift  WHERE Division_Code=" +
                       "'" + div_code + "' AND  Gift_Type='" + Gift_Type + "'" +
                       "AND Gift_Active_Flag='0'" +
                    " ORDER BY 2";
                try
                {
                    dsProCat = db_ER.Exec_DataSet(strQry);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                return dsProCat;
            }
        }
        //Changes done by Priya---jul 24 and aug 6
        public DataSet getStategift(string div_code, int State_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            //strQry = "  SELECT Gift_Code,Gift_SName,Gift_Name,Gift_Value,Gift_Type,Gift_Effective_From,Gift_Effective_To" +
            //           " FROM Mas_Gift  WHERE Division_Code=" +
            //            "'" + divcode + "' And State_Code='" + State_Code + "' " +
            //            " ORDER BY 2";

            //strQry = " SELECT a.Gift_SName,a.Gift_Name,a.Gift_Value,a.Gift_Type,a.Gift_Effective_From,a.Gift_Effective_To,a.State_Code,c.statename" +
            //               " FROM Mas_Gift a, mas_state c" +
            //               " WHERE a.state_code=c.state_code And c.state_code='" + State_Code + "' AND a.Division_Code ='" + div_code + "' and a.Gift_Code='" + Gift_Code + "'";

            strQry = "SELECT a.Gift_Code,a.Gift_SName,a.Gift_Name,a.Gift_Value," +
                 " case when Gift_Type = '1' then 'Literature/Lable' " +
                     " else case when Gift_Type = '2' then 'Special Gift'" +
                     " else case when Gift_Type = '3' then 'Doctor Kit'" +
                     "else 'Ordinary Gift' " +
                     "end end end as Gift_Type," +
                    " a.Gift_Effective_From,a.Gift_Effective_To,a.State_Code" +
                    " FROM mas_state c join Mas_Gift a" +
                     " on" +
                     " a.state_code like '" + State_Code + ',' + "%'  or " +
                     " a.state_code like '%" + ',' + State_Code + "' or" +
                     " a.state_code like '%" + ',' + State_Code + ',' + "%' or" +
                      " a.state_code like '%" + State_Code + "%'" +
                     " WHERE convert(varchar,c.state_code)='" + State_Code + "' and Gift_Active_Flag=0 and " +
                     " a.Division_Code ='" + div_code + "' " +
                      " and (Gift_Effective_From >= Convert(Date, GetDate(), 101) or Gift_Effective_To >= Convert(Date, GetDate(), 101)) ";

            try
            {
                dsProCat = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProCat;

        }
        //end
        //Changes done by Priya--jul 26
        public DataSet getLatest_date(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet dsProCat = null;
            strQry = " select Gift_code,Gift_Name,Gift_Type,Gift_Value,Gift_SName,Division_Code,Gift_Effective_From,Gift_Effective_To,Created_Date,State_Code from Mas_Gift" +
                " where Gift_Active_Flag='0'" +
                " And LastUpdt_Date IN (SELECT max(LastUpdt_Date) FROM Mas_Gift)";
            try
            {
                dsProCat = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProCat;
        }
        //end  

        //Changes done by Priya

        public DataSet Fillsub_div(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            strQry = "SELECT subdivision_code,subdivision_sname,subdivision_name " +
                     " FROM mas_subdivision WHERE subdivision_active_flag=0 And Div_Code= '" + divcode + "'" +
                     " ORDER BY 2";

            try
            {
                dsProCat = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProCat;
        }
        //End
        //Changes done by Priya
        public DataSet getProd_Edit(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            strQry = " SELECT Product_Detail_Code,Product_Detail_Name,Product_Sale_Unit,Product_Type_Code,Product_Description, " +
                     " Product_Sample_Unit_One,Product_Sample_Unit_Two,Product_Sample_Unit_Three,State_Code,subdivision_code, Sample_Erp_Code, Sale_Erp_Code " +
                     " FROM  Mas_Product_Detail " +
                     " WHERE Product_Active_Flag=0 AND Division_Code= '" + divcode + "' " +
                     " ORDER BY 2";
            try
            {
                dsProCat = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProCat;
        }
        //
        //Changes done by Saravanan
        public DataSet ViewGift(string div_code, int State_Code, int iFrom, int iTo)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            //strQry = " select Gift_code,Gift_Name,Gift_Type,Gift_Value,Gift_SName,Division_Code,Gift_Effective_From,Gift_Effective_To,Created_Date,State_Code from Mas_Gift"+
            //         " where Gift_Active_Flag='0'" +            
            //         " And Division_Code = '" + div_code + "' " +
            //         " and year(Gift_Effective_From) = " + iFrom + " and year(Gift_Effective_To) = " + iTo + " order by 1";

            strQry = " SELECT b.Gift_Code,b.Gift_SName,b.Gift_Name,b.Gift_Value," +
                     " case when Gift_Type = '1' then 'Literature/Lable' " +
                     " else case when Gift_Type = '2' then 'Special Gift'" +
                     " else case when Gift_Type = '3' then 'Doctor Kit'" +
                     " else 'Ordinary Gift' " +
                     " end end end as Gift_Type," +
                     " convert(varchar(10),Gift_Effective_From,103) Gift_Effective_From,convert(varchar(10),Gift_Effective_To,103) Gift_Effective_To,b.State_Code" +
                     " FROM mas_state a join Mas_Gift b" +
                     " on" +
                     " b.state_code like '" + State_Code + ',' + "%'  or " +
                     " b.state_code like '%" + ',' + State_Code + "' or" +
                     " b.state_code like '%" + ',' + State_Code + ',' + "%' or" +
                      " b.state_code like '%" + State_Code + "%'" +
                     " WHERE convert(varchar,a.state_code)='" + State_Code + "' and Gift_Active_Flag=0 and " +
                     " b.Division_Code ='" + div_code + "'" +
                     " and year(Gift_Effective_From) >= " + iFrom + " and year(Gift_Effective_To) <= " + iTo + " order by 1";
            try
            {
                dsProCat = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProCat;
        }
        //end

        //End
        public int RecordCount(string div_code)
        {
            int iReturn = -1;

            try
            {
                DB_EReporting db = new DB_EReporting();

                //strQry = "select ROW_NUMBER() OVER(ORDER BY Product_Detail_Code DESC) AS Row,* from Mas_Product_Detail WHERE Division_Code = '" + div_code + "' ";
                strQry = "SELECT count(product_detail_code) FROM Mas_Product_Detail WHERE Division_Code = '" + div_code + "' and Product_Active_Flag=0";
                iReturn = db.Exec_Scalar(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }

        public DataSet getSubdiv(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            strQry = " SELECT '' as subdivision_code, '---Select---' as subdivision_name " +
                 " UNION " +
                     " SELECT subdivision_code,subdivision_name " +
                     " FROM mas_subdivision WHERE subdivision_active_flag=0 And Div_Code= '" + divcode + "'" +
                     " ORDER BY 2";

            try
            {
                dsProCat = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProCat;
        }


        //Changes done by Sarvanan
        public DataSet getMultiDivsf_Name(string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            strQry = "select len(Division_Code) Div_CodeLen,Division_Code,IsMultiDivision from Mas_Salesforce where Sf_Code='" + sf_code + "'";
            try
            {
                dsProCat = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProCat;
        }

        //Changes done by Priya

        public DataTable getGiftlist_React_Sort(string divcode, string sAlpha)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dtProCat = null;


            strQry = "  SELECT Gift_Code,Gift_Name,Gift_SName,Gift_Value," +
                        " case when Gift_Type = '1' then 'Literature/Lable' " +
                        " else case when Gift_Type = '2' then 'Special Gift'" +
                        " else case when Gift_Type = '3' then 'Doctor Kit'" +
                        "else 'Ordinary Gift' " +
                        "end end end as Gift_Type" +
                        " FROM Mas_Gift  WHERE Division_Code=" +
                        "'" + divcode + "' AND Gift_Active_Flag=0" +
                        " AND  Gift_Name like '" + sAlpha + "%'" +
                      " and (Gift_Effective_From < DATEADD(day, DATEDIFF(day, 0, getDate()) - 1, 0)" +
                       " and Gift_Effective_To <  DATEADD(day, DATEDIFF(day, 0, getDate()),  0))" +
                     " ORDER BY 2";
            try
            {
                dtProCat = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtProCat;
        }

        public DataSet getGiftName_React(string divcode, string Name)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet dsProCat = null;
            {
                strQry = "  SELECT Gift_Code,Gift_Name,Gift_SName,Gift_Value," +
                       " case when Gift_Type = '1' then 'Literature/Lable' " +
                       " else case when Gift_Type = '2' then 'Special Gift'" +
                       " else case when Gift_Type = '3' then 'Doctor Kit'" +
                       "else 'Ordinary Gift' " +
                       "end end end as Gift_Type" +
                       " FROM Mas_Gift  WHERE Division_Code=" +
                       "'" + divcode + "' AND  Gift_Name like '" + Name + "%'" +
                       "AND Gift_Active_Flag='0'" +
                      " and (Gift_Effective_From < DATEADD(day, DATEDIFF(day, 0, getDate()) - 1, 0)" +
                       " and Gift_Effective_To <  DATEADD(day, DATEDIFF(day, 0, getDate()),  0))" +
                    " ORDER BY 2";
                try
                {
                    dsProCat = db_ER.Exec_DataSet(strQry);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                return dsProCat;
            }
        }
        public DataSet getStategift_React(string div_code, int State_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;


            strQry = "SELECT a.Gift_Code,a.Gift_SName,a.Gift_Name,a.Gift_Value," +
                 " case when Gift_Type = '1' then 'Literature/Lable' " +
                     " else case when Gift_Type = '2' then 'Special Gift'" +
                     " else case when Gift_Type = '3' then 'Doctor Kit'" +
                     "else 'Ordinary Gift' " +
                     "end end end as Gift_Type," +
                    " a.Gift_Effective_From,a.Gift_Effective_To,a.State_Code" +
                    " FROM mas_state c join Mas_Gift a" +
                     " on" +
                     " a.state_code like '" + State_Code + ',' + "%'  or " +
                     " a.state_code like '%" + ',' + State_Code + "' or" +
                     " a.state_code like '%" + ',' + State_Code + ',' + "%' or" +
                      " a.state_code like '%" + State_Code + "%'" +
                     " WHERE convert(varchar,c.state_code)='" + State_Code + "' and Gift_Active_Flag=0 and " +
                     " a.Division_Code ='" + div_code + "' " +
                     " and (Gift_Effective_From < DATEADD(day, DATEDIFF(day, 0, getDate()) - 1, 0)" +
                     " and Gift_Effective_To <  DATEADD(day, DATEDIFF(day, 0, getDate()),  0))" +
                     " ORDER BY 2";
            try
            {
                dsProCat = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProCat;

        }


        //Changes done by Reshmi
        public bool sRecordExistDetail(string Product_Detail_Name, string Product_Detail_Code, string divcode)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(Product_Detail_Name) FROM Mas_Product_Detail WHERE Product_Detail_Name = N'" + Product_Detail_Name + "' AND Product_Detail_Code!='" + Product_Detail_Code + "' AND Division_Code= '" + divcode + "'and Product_Active_Flag=0";

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
        //Changes done by Reshmi
        public DataSet getProCat_React(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            strQry = " SELECT Product_Cat_Code,Product_Cat_SName,Product_Cat_Name FROM  Mas_Product_Category " +
                     " WHERE Product_Cat_Active_Flag=1 AND Division_Code= '" + divcode + "' " +
                     " ORDER BY ProdCat_SNo";
            try
            {
                dsProCat = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProCat;
        }
        public int ReActivate(int Prod_Cat_Code)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "UPDATE Mas_Product_Category " +
                            " SET Product_Cat_Active_Flag=0 ," +
                            " LastUpdt_Date = getdate() " +
                            " WHERE Product_Cat_Code = '" + Prod_Cat_Code + "' and  Product_Cat_Active_Flag =1";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;
        }
        public DataSet getProGrp_React(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProGrp = null;

            strQry = " SELECT Product_Grp_Code,Product_Grp_SName,Product_Grp_Name FROM  Mas_Product_Group " +
                     " WHERE Product_Grp_Active_Flag=1 AND Division_Code= '" + divcode + "' " +
                     " ORDER BY ProdGrp_Sl_No";
            try
            {
                dsProGrp = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProGrp;
        }
        public int ReActivateGrp(int Prod_Grp_Code)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "UPDATE Mas_Product_Group " +
                            " SET Product_Grp_Active_Flag=0 ," +
                            " LastUpdt_Date = getdate() " +
                            " WHERE Product_Grp_Code = '" + Prod_Grp_Code + "' and  Product_Grp_Active_Flag =1";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;
        }
        //Done By Reshmi

        public DataSet getProBrd(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProBrd = null;

            strQry = " SELECT b.Product_Brd_Code,b.Product_Brd_SName,b.Product_Brd_Name,b.Product_Cat_Name,b.Product_Cat_Div_Code,b.Product_Cat_Div_Name," +
                     " (select COUNT(p.Product_Brd_Code) from Mas_Product_Detail p where p.Product_Active_Flag=0 and p.Product_Brd_Code = b.Product_Brd_Code ) as brd_count   FROM  Mas_Product_Brand b" +
                     " WHERE b.Product_Brd_Active_Flag=0 AND Division_Code= '" + divcode + "' " +
                     " ORDER BY b.Product_Brd_SNO";
            try
            {
                dsProBrd = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProBrd;
        }

        public DataTable getProductBrandlist_DataTable(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dtProBrd = null;

            strQry = " SELECT b.Product_Brd_Code,b.Product_Brd_SName,b.Product_Brd_Name, " +
                   " (select COUNT(p.Product_Brd_Code) from Mas_Product_Detail p where p.Product_Active_Flag =0 and  p.Product_Brd_Code = b.Product_Brd_Code ) as brd_count   FROM  Mas_Product_Brand b" +
                   " WHERE b.Product_Brd_Active_Flag=0 AND Division_Code= '" + divcode + "' " +
                   " ORDER BY b.Product_Brd_SNO";
            try
            {
                dtProBrd = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtProBrd;
        }

        public int RecordDelete(int Product_Cat_Code)
        {
            int iReturn = -1;
            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "DELETE FROM  Mas_Product_Category WHERE Product_Cat_Code = '" + Product_Cat_Code + "' ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }

        public int Brd_DeActivate(int ProBrdCode)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "UPDATE Mas_Product_Brand " +
                            " SET Product_Brd_Active_Flag=1 ," +
                           " LastUpdt_Date = getdate() " +
                            " WHERE Product_Brd_Code = '" + ProBrdCode + "' ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;
        }
        //..
        public int Brd_RecordUpdate(int ProBrdCode, string Product_Brd_SName, string Product_Brd_Name, string divcode, int Product_Cat_Code, string Product_Cat_Name, string Pro_Div_code, string Pro_Div_name)
        {
            int iReturn = -1;
            if (!RecordExistbrd(ProBrdCode, Product_Brd_SName, divcode))
            {
                if (!nRecordExist(ProBrdCode, Product_Brd_Name, divcode))
                {
                    try
                    {

                        DB_EReporting db = new DB_EReporting();

                        strQry = "UPDATE Mas_Product_Brand " +
                                 " SET Product_Brd_SName = N'" + Product_Brd_SName + "', " +
                                 " Product_Brd_Name = N'" + Product_Brd_Name + "' ," +
                                 " Product_Cat_Div_Name = '" + Pro_Div_name + "' ," +
                                 " Product_Cat_Div_Code = '" + Pro_Div_code + "' ," +
                                 " LastUpdt_Date = getdate() ," +
                                 " Product_Cat_Code = " + Product_Cat_Code + " ," +
                                 " Product_Cat_Name = '" + Product_Cat_Name + "' " +
                                 " WHERE Product_Brd_Code = '" + ProBrdCode + "' and Product_Brd_Active_Flag = 0 ";

                        iReturn = db.ExecQry(strQry);
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
            }
            else
            {
                iReturn = -3;
            }
            return iReturn;
        }

        public bool RecordExistbrd(int ProBrdCode, string Product_Brd_SName, string divcode)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(Product_Brd_SName) FROM Mas_Product_Brand WHERE Product_Brd_SName = N'" + Product_Brd_SName + "' AND Product_Brd_Code!='" + ProBrdCode + "' AND Division_Code= '" + divcode + "' ";

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
        public bool nRecordExist(int ProBrdCode, string Product_Brd_Name, string divcode)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(Product_Brd_Name) FROM Mas_Product_Brand WHERE Product_Brd_Name = N'" + Product_Brd_Name + "' AND Product_Brd_Code!='" + ProBrdCode + "' and Division_Code = '" + divcode + "'";

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
        public DataSet getProdBrd(string divcode, string ProdBrdCode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProBrd = null;

            strQry = " SELECT Product_Brd_SName,Product_Brd_Name,Product_Cat_Name,Product_Cat_Div_Name,Product_Cat_Div_Code FROM  Mas_Product_Brand " +
                     " WHERE Product_Brd_Code= '" + ProdBrdCode + "'AND Division_Code= '" + divcode + "' " +
                     " ORDER BY 2";
            try
            {
                dsProBrd = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProBrd;
        }
        public int Brd_RecordAdd(string divcode, string Product_Brd_SName, string Product_Brd_Name, int Product_Cat_Code, string Product_Cat_Name, string Pro_Div_code, string Pro_Div_name)
        {
            int iReturn = -1;
            if (!RecordExist_Brd(Product_Brd_SName, divcode))
            {
                if (!sRecordExist_Brd(Product_Brd_Name, divcode))
                {
                    try
                    {
                        DB_EReporting db = new DB_EReporting();
                        strQry = "SELECT isnull(max(Product_Brd_Code)+1,'1') Product_Brd_Code from Mas_Product_Brand ";
                        int Product_Brd_Code = db.Exec_Scalar(strQry);

                        strQry = "INSERT INTO Mas_Product_Brand(Product_Brd_Code,Division_Code,Product_Brd_SName,Product_Brd_Name,Product_Brd_Active_Flag,Created_Date,LastUpdt_Date,Product_Cat_Code,Product_Cat_Name,Product_Cat_Div_Code,Product_Cat_Div_Name)" +
                                 "values('" + Product_Brd_Code + "','" + divcode + "',N'" + Product_Brd_SName + "', N'" + Product_Brd_Name + "',0,getdate(),getdate()," + Product_Cat_Code + ",N'" + Product_Cat_Name + "','" + Pro_Div_code + "','" + Pro_Div_name + "')";


                        iReturn = db.ExecQry(strQry);
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
            }
            else
            {
                iReturn = -3;
            }
            return iReturn;
        }
        public bool RecordExist_Brd(string Product_Brd_SName, string div_code)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(Product_Brd_SName) FROM Mas_Product_Brand WHERE Product_Brd_SName=N'" + Product_Brd_SName + "' and Division_Code = '" + div_code + "' ";
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
        public bool sRecordExist_Brd(string Product_Brd_Name, string div_code)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(Product_Brd_Name) FROM Mas_Product_Brand WHERE Product_Brd_Name=N'" + Product_Brd_Name + "' and Division_Code = '" + div_code + "' ";
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
        public int Update_ProdBrdSno(string Product_Brd_Code, string Sno)
        {
            int iReturn = -1;
            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "UPDATE Mas_Product_Brand " +
                         " SET Product_Brd_SNO = '" + Sno + "', " +
                         " LastUpdt_Date = getdate() " +
                         " WHERE Product_Brd_Code = '" + Product_Brd_Code + "' ";

                iReturn = db.ExecQry(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }
        public DataSet getProBrd_React(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProBrd = null;

            strQry = " SELECT Product_Brd_Code,Product_Brd_SName,Product_Brd_Name FROM  Mas_Product_Brand " +
                     " WHERE Product_Brd_Active_Flag=1 AND Division_Code= '" + divcode + "' " +
                     " ORDER BY Product_Brd_SNO";
            try
            {
                dsProBrd = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProBrd;
        }
        public int Brd_ReActivate(int Product_Brd_Code)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "UPDATE Mas_Product_Brand " +
                            " SET Product_Brd_Active_Flag=0 ," +
                            " LastUpdt_Date = getdate() " +
                            " WHERE Product_Brd_Code = '" + Product_Brd_Code + "' and  Product_Brd_Active_Flag =1";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;
        }
        public DataSet getProdforbrd(string divcode, string val)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProBrd = null;

            strQry = " SELECT a.Product_Detail_Code,a.Product_Detail_Name,a.Product_Sale_Unit,a.Product_Description, " +
                     " b.Product_Cat_Name,d.Product_Brd_Name,a.Product_Mode,a.Sale_Erp_Code,a.Sample_Erp_Code" +
                     " FROM  Mas_Product_Detail a,Mas_Product_Category b,Mas_Product_Brand d" +
                     " WHERE a.product_cat_code = b.product_cat_code AND " +
                     " a.Product_Brd_Code = d.Product_Brd_Code AND " +
                     " a.Product_Brd_Code = '" + val + "' AND " +
                     " a.Product_Active_Flag=0 AND a.Division_Code= '" + divcode + "' " +
                     " ORDER BY 2";
            try
            {
                dsProBrd = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProBrd;
        }
        public DataSet getProdforcat(string divcode, string val)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            strQry = " SELECT a.Product_Detail_Code,a.Product_Detail_Name,a.Product_Sale_Unit,a.Product_Description, " +
                     " b.Product_Cat_Name,d.Product_Brd_Name,a.Product_Mode,a.Sale_Erp_Code,a.Sample_Erp_Code" +
                     " FROM  Mas_Product_Detail a,Mas_Product_Category b,Mas_Product_Brand d" +
                     " WHERE a.product_cat_code = b.product_cat_code AND " +
                     " a.Product_Brd_Code = d.Product_Brd_Code AND " +
                     " a.product_cat_code = '" + val + "' AND " +
                     " a.Product_Active_Flag=0 AND a.Division_Code= '" + divcode + "' " +
                     " ORDER BY 2";
            try
            {
                dsProCat = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProCat;
        }

        public DataSet getProdforDiv(string divcode, string val)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            //strQry = " SELECT a.Product_Detail_Code,a.Product_Detail_Name,a.Product_Sale_Unit,a.Product_Description, " +
            //         " b.Product_Cat_Name,d.Product_Brd_Name,a.Product_Mode,a.Sale_Erp_Code,a.Sample_Erp_Code" +
            //         " FROM  Mas_Product_Detail a,Mas_Product_Category b,Mas_Product_Brand d" +
            //         " WHERE a.product_cat_code = b.product_cat_code AND " +
            //         " a.Product_Brd_Code = d.Product_Brd_Code AND " +
            //         " a.product_cat_code = '" + val + "' AND " +
            //         " a.Product_Active_Flag=0 AND a.Division_Code= '" + divcode + "' " +
            //         " ORDER BY 2";
            strQry = "SELECT a.Product_Detail_Code,a.Product_Detail_Name,a.Product_Sale_Unit,a.Product_Description,a.Product_Mode,a.Sale_Erp_Code,a.Sample_Erp_Code " +
                     " FROM  Mas_Product_Detail a,mas_subdivision b " +
                     " WHERE   charindex(','+ cast(b.subdivision_code as varchar)+',',','+ a.subdivision_code +',')>0  AND " +
                     " a.Product_Active_Flag=0 AND a.Division_Code= '" + divcode + "' and b.subdivision_code='" + val + "' " +
                     " ORDER BY 2";

            try
            {
                dsProCat = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProCat;
        }
        public DataSet getFieldforcou(string divcode, string val)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            //strQry = " SELECT a.Product_Detail_Code,a.Product_Detail_Name,a.Product_Sale_Unit,a.Product_Description, " +
            //         " b.Product_Cat_Name,d.Product_Brd_Name,a.Product_Mode,a.Sale_Erp_Code,a.Sample_Erp_Code" +
            //         " FROM  Mas_Product_Detail a,Mas_Product_Category b,Mas_Product_Brand d" +
            //         " WHERE a.product_cat_code = b.product_cat_code AND " +
            //         " a.Product_Brd_Code = d.Product_Brd_Code AND " +
            //         " a.product_cat_code = '" + val + "' AND " +
            //         " a.Product_Active_Flag=0 AND a.Division_Code= '" + divcode + "' " +
            //         " ORDER BY 2";
            strQry = " SELECT a.Sf_Code,a.Sf_Name,a.SF_Mobile,a.Sf_HQ " +
                   "  FROM  Mas_Salesforce a,mas_subdivision b " +
                   "  WHERE   charindex(','+ cast(b.subdivision_code as varchar)+',',','+ a.subdivision_code +',')>0  AND " +
                   " a.SF_Status=0 AND a.Division_Code= '" + divcode + ",' and b.subdivision_code='" + val + "' " +
                   "  ORDER BY 2";
            try
            {
                dsProCat = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProCat;
        }
        public DataSet getProdforgrp(string divcode, string val)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            strQry = " SELECT a.Product_Detail_Code,a.Product_Detail_Name,a.Product_Sale_Unit,a.Product_Description, " +
                     " b.Product_Cat_Name,c.Product_Grp_Name,d.Product_Brd_Name,a.Product_Mode,a.Sale_Erp_Code,a.Sample_Erp_Code" +
                     " FROM  Mas_Product_Detail a,Mas_Product_Category b,Mas_Product_Group c,Mas_Product_Brand d" +
                     " WHERE a.product_cat_code = b.product_cat_code AND " +
                     " a.Product_Grp_Code = c.product_grp_code AND " +
                     " a.Product_Brd_Code = d.Product_Brd_Code AND " +
                     " a.Product_Grp_Code = '" + val + "' AND " +
                     " a.Product_Active_Flag=0 AND a.Division_Code= '" + divcode + "' " +
                     " ORDER BY 2";
            try
            {
                dsProCat = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProCat;
        }
     public DataSet Get_Primary_Scheme_Names(string DivCode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;
            strQry = "select convert(varchar,Effective_To,103) EfDate,Scheme_Name from Mas_Primary_Scheme where division_code='" + DivCode + "' and cast(convert(varchar,Effective_To,101)as datetime) >=cast(convert(varchar,getdate(),101)as datetime)  GROUP BY Scheme_Name,convert(varchar,Effective_To,103)";

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
  public int Delete_priScheme_Values(string DivCode, string schemName)
        {
            int iReturn = -1;
            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "DELETE FROM  Mas_Primary_Scheme WHERE  division_code='" + DivCode + "' and Scheme_Name='" + schemName + "'";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }
 public DataSet getprischemaval(string DivCode, string schemName)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;
            //  strQry = "select Product_Code,Scheme,Free,Discount,Package,State_Code,Stockist_Code,Scheme_Name,Effective_From,Effective_To from mas_scheme where division_code='" + DivCode + "' and Scheme_Name='" + schemName + "'";
            strQry = "getprischemaval '" + DivCode + "','" + schemName + "'";

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
        public int ReActivate_Brd(string Product_Brd_Code)
        {
            int iReturn = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = " UPDATE Mas_Product_Brand" +
                         " SET Product_Brd_Active_Flag=0, " +
                         " LastUpdt_Date = getdate() " +
                         " where Product_Brd_Code = '" + Product_Brd_Code + "' ";
                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }
        public DataTable getDTProduct_Brd(string divcode, string val)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dsProBrd = null;

            strQry = " SELECT a.Product_Detail_Code,a.Product_Detail_Name,a.Product_Sale_Unit,a.Product_Description, " +
                     " b.Product_Cat_Name,c.Product_Grp_Name,d.Product_Brd_Name" +
                     " FROM  Mas_Product_Detail a,Mas_Product_Category b,Mas_Product_Group c,Mas_Product_Brand d" +
                     " WHERE a.product_cat_code = b.product_cat_code AND " +
                     " a.Product_Grp_Code = c.product_grp_code AND " +
                     " a.Product_Brd_Code = d.Product_Brd_Code AND " +
                     " a.Product_Brd_Code = '" + val + "' AND " +
                     " a.Product_Active_Flag=0 AND a.Division_Code= '" + divcode + "' " +
                     " ORDER BY 2";
            try
            {
                dsProBrd = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProBrd;
        }
        public DataSet getProductBrand(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProBrd = null;

            strQry = " SELECT '' as Product_Brd_Code, '---Select---' as Product_Brd_Name " +
                     " UNION " +
                     " SELECT Product_Brd_Code,Product_Brd_Name FROM  Mas_Product_Brand " +
                     " WHERE Product_Brd_Active_Flag=0 AND Product_Brd_Code> 0 AND Division_Code= '" + divcode + "' " +
                     " ORDER BY 2";
            try
            {
                dsProBrd = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProBrd;
        }
        public DataSet get_cat_base_ProductBrand(string Cat_code, string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProBrd = null;

            strQry = " SELECT '' as Product_Brd_Code, '---Select---' as Product_Brd_Name " +
                     " UNION " +
                     " SELECT Product_Brd_Code,Product_Brd_Name FROM  Mas_Product_Brand " +
                     " WHERE Product_Brd_Active_Flag=0 AND Product_Brd_Code> 0 AND Product_Cat_Code='" + Cat_code + "' AND Division_Code= '" + divcode + "' " +
                     " ORDER BY 2";
            try
            {
                dsProBrd = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProBrd;
        }
        public DataSet getProdforCode(string divcode, string prodcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            strQry = "  select Product_Detail_Code,Product_Detail_Name,Base_Unit_code,Unit_code,Sample_Erp_Code, Product_Cat_Code,Product_Grp_Code,Product_Brd_Code,Product_Type_Code,Product_Mode,Product_Description,product_packsize,product_grosswt,product_netwt,Sale_Erp_Code,state_code,subdivision_code,target,Product_Short_Name,HSN_Code,UOM_Weight,Product_Validity,Product_Image " +
                     " FROM Mas_Product_Detail WHERE Product_Detail_Code= '" + prodcode + "' AND Division_Code= '" + divcode + "' " +
                     " ORDER BY 2";
            try
            {
                dsProCat = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProCat;
        }

        public DataSet uploadImage(string div_code, string filename, string ProdCode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsState = null;

            strQry = " update Mas_Product_Detail set Product_Image = '" + filename + "' where Product_Detail_Code='" + ProdCode + "' and Division_Code=" + div_code + " ";
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

        //...
        public int RecordAdd(string Product_Detail_Code, string Product_Detail_Name, string Product_Sale_Unit, int Base_unit_code, string uom, int uom_code, int Product_Cat_Code, int Product_Grp_Code, string Product_Type_Code, string Product_Description, int Division_Code, string state, string sub_division, string mode, string sample, string sale, int Product_Brd_Code, string txtPacksize, string txtGrosswt, string txtNetwt, int target, string Product_Short_Name, string HSN_Code, string Netwt,int Txtprovalid)
        {
            int iReturn = -1;
            int iSlNo = -1;
            int icodeSlNo = -1;
            if (!RecordExistdet(Product_Detail_Code, Division_Code))
            {
                if (!sRecordExistdet(Product_Detail_Name, Division_Code))
                {
                    try
                    {

                        DB_EReporting db = new DB_EReporting();

                        strQry = "SELECT ISNULL(MAX(Prod_Detail_Sl_No),0)+1 FROM Mas_Product_Detail";
                        iSlNo = db.Exec_Scalar(strQry);


                        strQry = "SELECT ISNULL(MAX(product_code_slno),0)+1 FROM Mas_Product_Detail";
                        icodeSlNo = db.Exec_Scalar(strQry);

                        strQry = "INSERT INTO Mas_Product_Detail(Product_Detail_Code,Product_Detail_Name,Product_Sale_Unit,Base_Unit_code,product_unit,Unit_code, " +
                                    " Product_Cat_Code,Product_Type_Code,Product_Description, " +
                                    " Division_Code,Created_Date,Product_Active_Flag,Prod_Detail_Sl_No,Product_Grp_Code,LastUpdt_Date,state_code,subdivision_code,Product_Mode,Sample_Erp_Code,Sale_Erp_Code,Product_Brd_Code,product_code_slno,product_packsize,product_grosswt,product_netwt,target,Product_Short_Name,HSN_Code,UOM_Weight,Product_Validity) " +
                                    " VALUES('" + Product_Detail_Code + "',N'" + Product_Detail_Name + "', '" + Product_Sale_Unit + "','" + Base_unit_code + "','" + uom + "','" + uom_code + "','" + Product_Cat_Code + "', " +
                                    " '" + Product_Type_Code + "',N'" + Product_Description + "', " + Division_Code + ", getdate(), 0, " + iSlNo + ", " + Product_Grp_Code + ",getdate(),'" + state + "','" + sub_division + "','" + mode + "','" + sample + "','" + sale + "','" + Product_Brd_Code + "', " + icodeSlNo + ",'" + txtPacksize + "','" + txtGrosswt + "','" + txtNetwt + "','" + target + "',N'" + Product_Short_Name + "','" + HSN_Code + "','" + Netwt + "','"+ Txtprovalid +"')";


                        iReturn = db.ExecQry(strQry);
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
            }
            else
            {
                iReturn = -3;
            }
            return iReturn;
        }
        public int RecordUpdateProd(string Product_Detail_Code, string Product_Detail_Name, string Product_Sale_Unit, int Base_unit_code, string uom, int uom_code, int Product_Cat_Code, int Product_Grp_Code, string Product_Type_Code, string Product_Description, string divcode, string state, string sub_division, string mode, string sample, string sale, int Product_Brd_Code, string txtPacksize, string txtGrosswt, string txtNetwt, int target, string Product_Short_Name, string Hsn_Code, string Netwt,int Txtprovalid)
        {
            int iReturn = -1;
            if (!sRecordExistDetail(Product_Detail_Name, Product_Detail_Code, divcode))
            {
                try
                {

                    DB_EReporting db = new DB_EReporting();

                    strQry = "UPDATE Mas_Product_Detail " +
                             " SET Product_Detail_Name =N'" + Product_Detail_Name + "', " +
                             " Product_Sale_Unit = '" + Product_Sale_Unit + "', " +
                              " Base_Unit_code = '" + Base_unit_code + "', " +
                               " product_unit = '" + uom + "', " +
                               " Unit_code = '" + uom_code + "', " +

                                     " Product_Cat_Code = " + Product_Cat_Code + " ," +
                                    " Product_Grp_Code = " + Product_Grp_Code + "," +

                                 " Product_Type_Code = '" + Product_Type_Code + "', " +
                                 " Product_Description =N'" + Product_Description + "'," +
                                    " Product_Short_Name =N'" + Product_Short_Name + "'," +
                                 " LastUpdt_Date = getdate() , " +
                                 " State_Code = '" + state + "',subdivision_code = '" + sub_division + "', Product_Mode ='" + mode + "', Sample_Erp_Code = '" + sample + "', Sale_Erp_Code ='" + sale + "' ,Product_Brd_Code ='" + Product_Brd_Code + "',product_packsize='" + txtPacksize + "',product_grosswt='" + txtGrosswt + "',product_netwt='" + txtNetwt + "', target ='" + target + "', HSN_Code ='" + Hsn_Code + "', UOM_Weight ='" + Netwt + "',Product_Validity = '" + Txtprovalid + "'  " +
                             " WHERE  Division_Code = " + divcode + " AND Product_Detail_Code = '" + Product_Detail_Code + "' ";

                    iReturn = db.ExecQry(strQry);
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
       public DataSet getProdall_bystk(string divcode, string state_code,string stk_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            //  strQry = "EXEC sp_getallproduct_byorder '"+ divcode + "','"+ state_code + "'";
            strQry = "EXEC get_product_details_view '" + divcode + "','" + state_code + "','" + stk_code + "'";
            try
            {
                dsProCat = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProCat;
        }
		
		public DataSet getProdall(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet dsProCat = null;
            strQry = " SELECT a.Product_Detail_Code,a.Product_Detail_Name,a.product_unit,a.Product_Sale_Unit,a.Product_Description, " +
                     " b.Product_Cat_Name,d.Product_Brd_Name,a.Product_Mode,a.Sale_Erp_Code,a.Sample_Erp_Code" +
                     " FROM  Mas_Product_Detail a,Mas_Product_Category b,Mas_Product_Brand d" +
                     " WHERE a.product_cat_code = b.product_cat_code AND " +
                     "a.Product_Brd_Code = d.Product_Brd_Code AND " +
                     "a.Product_Active_Flag=0 AND a.Division_Code= '" + divcode + "' " +
                     " ORDER BY Prod_Detail_Sl_No";
            try
            {
                dsProCat = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProCat;
        }
		
		
		 public DataSet get_pro_all(string ffc,string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocCat = null;


            strQry = " EXEC get_product '" + ffc + "', '" + divcode + "' ";

            try
            {
                dsDocCat = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsDocCat;
        }
		 public DataSet getclosProdall(string ffc, string emonth, string eyear,string divcode, string distname)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocCat = null;
           

            strQry = " EXEC closstock '" + ffc + "', '" + emonth + "', '" + eyear + "', '" + divcode + "', '" + distname + "'  ";

            try
            {
                dsDocCat = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsDocCat;
        }
        //
        public DataSet getProdforname(string divcode, string val)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            strQry = " SELECT a.Product_Detail_Code,a.Product_Detail_Name,a.Product_Sale_Unit,a.Product_Description, " +
                     " b.Product_Cat_Name,c.Product_Grp_Name,d.Product_Brd_Name,a.Product_Mode,a.Sale_Erp_Code,a.Sample_Erp_Code" +
                     " FROM  Mas_Product_Detail a,Mas_Product_Category b,Mas_Product_Group c,Mas_Product_Brand d" +
                     " WHERE a.product_cat_code = b.product_cat_code AND " +
                     " a.Product_Grp_Code = c.product_grp_code AND " +
                     " a.Product_Brd_Code = d.Product_Brd_Code AND " +
                      " a.Product_Detail_Name like '" + val + "%' and " +
                     " a.Product_Active_Flag=0 AND a.Division_Code= '" + divcode + "' " +
                     " ORDER BY 2";

            try
            {
                dsProCat = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProCat;
        }
        public DataSet getProdforSubdiv(string divcode, string val)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            //strQry = " SELECT a.Product_Detail_Code,a.Product_Detail_Name,a.Product_Sale_Unit,a.Product_Description, " +
            //         " a.Product_Sample_Unit_One,a.Product_Sample_Unit_Two,a.Product_Sample_Unit_Three,b.Product_Cat_Name,c.Product_Grp_Name,d.subdivision_name" +
            //         " FROM  Mas_Product_Detail a,Mas_Product_Category b,Mas_Product_Group c,mas_subdivision d" +
            //         " WHERE a.product_cat_code = b.product_cat_code AND " +
            //         " a.Product_Grp_Code = c.product_grp_code AND " +
            //         " a.subdivision_code = '" + val + "' AND " +                     
            //         " a.subdivision_code = convert(varchar(10),d.subdivision_code) and" +
            //         " a.Product_Active_Flag=0 AND a.Division_Code= '" + divcode + "' " +
            //         " ORDER BY 2";
            strQry = "  select Product_Detail_Code,Product_Detail_Name,Product_Sale_Unit,Product_Description,PC.Product_Cat_Name,BP.Product_Brd_Name,PD.Product_Mode,PD.Sale_Erp_Code,PD.Sample_Erp_Code " +
                    "  from Mas_Product_Detail PD  inner join Mas_Product_Category PC on PC.Product_Cat_Code=PD.Product_Cat_Code  " +
                    "  inner join Mas_Product_Brand BP on BP.Product_Brd_Code=PD.Product_Brd_Code  where PD.Division_Code=" + divcode + "" +
                    "  and PD.Product_Active_Flag=0 and CHARINDEX(','+CAST(" + val + " as varchar)+',',','+PD.subdivision_code+',')>0 order by Product_Detail_Name ";
            try
            {
                dsProCat = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProCat;
        }
        //Changes done by priya And Reshmi

        public DataSet getProdforState(string divcode, string val)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            strQry = " select Product_Detail_Code, Product_Detail_Name, Product_Sale_Unit, Product_Description, PC.Product_Cat_Name,BP.Product_Brd_Name,PD.Product_Mode,PD.Sale_Erp_Code,PD.Sample_Erp_Code " +
                    " from Mas_Product_Detail PD  inner join Mas_Product_Category PC on PC.Product_Cat_Code = PD.Product_Cat_Code " +
                    " inner join Mas_Product_Brand BP on BP.Product_Brd_Code = PD.Product_Brd_Code  where PD.Division_Code = " + divcode + "and PD.Product_Active_Flag = 0 " +
                    " and CHARINDEX(',' + CAST(" + val + " as varchar) + ',',',' + PD.State_Code + ',')> 0 order by Product_Detail_Name";
            try
            {
                dsProCat = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProCat;
        }
        public DataTable getDTProduct_Cat(string divcode, string val)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dsProCat = null;

            strQry = " SELECT a.Product_Detail_Code,a.Product_Detail_Name,a.Product_Sale_Unit,a.Product_Description, " +
                     " b.Product_Cat_Name,c.Product_Grp_Name,d.Product_Brd_Name" +
                     " FROM  Mas_Product_Detail a,Mas_Product_Category b,Mas_Product_Group c,Mas_Product_Brand d" +
                     " WHERE a.product_cat_code = b.product_cat_code AND " +
                     " a.Product_Grp_Code = c.product_grp_code AND " +
                     " a.Product_Brd_Code = d.Product_Brd_Code AND " +
                     " a.product_cat_code = '" + val + "' AND " +
                     " a.Product_Active_Flag=0 AND a.Division_Code= '" + divcode + "' " +
                     " ORDER BY 2";
            try
            {
                dsProCat = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProCat;
        }
        public DataTable getDTProduct_Nam(string search, string searchname, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dsProCat = null;

            strQry = " SELECT Product_Detail_Code,Product_Detail_Name,Product_Sale_Unit,Product_Description, " +
                     " Product_Cat_Name,Product_Grp_Name,Product_Brd_Name" +
                     " FROM  Mas_Product_Detail a,Mas_Product_Category b,Mas_Product_Group c,Mas_Product_Brand d" +
                     " WHERE a.product_cat_code = b.product_cat_code AND " +
                     "a.Product_Grp_Code = c.product_grp_code AND " +
                     "a.Product_Brd_Code = d.Product_Brd_Code AND " +
                     "a.Product_Active_Flag=0 " +
                     " and a.Product_Detail_Name like '" + searchname + "%'  and  a.Division_Code= '" + div_code + "' " +
                     " ORDER BY 2";
            try
            {
                dsProCat = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProCat;
        }
        public DataTable getDTProduct_Grp(string divcode, string val)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dsProCat = null;

            strQry = " SELECT a.Product_Detail_Code,a.Product_Detail_Name,a.Product_Sale_Unit,a.Product_Description, " +
                     " b.Product_Cat_Name,c.Product_Grp_Name,d.Product_Brd_Name" +
                     " FROM  Mas_Product_Detail a,Mas_Product_Category b,Mas_Product_Group c,Mas_Product_Brand d" +
                     " WHERE a.product_cat_code = b.product_cat_code AND " +
                     " a.Product_Grp_Code = c.product_grp_code AND " +
                     " a.Product_Brd_Code = d.Product_Brd_Code AND " +
                     " a.Product_Grp_Code = '" + val + "' AND " +
                     " a.Product_Active_Flag=0 AND a.Division_Code= '" + divcode + "' " +
                     " ORDER BY 2";
            try
            {
                dsProCat = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProCat;
        }
        public DataTable getDTProduct_Sbdiv(string divcode, string val)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dsProCat = null;

            //strQry = " SELECT a.Product_Detail_Code,a.Product_Detail_Name,a.Product_Sale_Unit,a.Product_Description, " +
            //         " a.Product_Sample_Unit_One,a.Product_Sample_Unit_Two,a.Product_Sample_Unit_Three,b.Product_Cat_Name,c.Product_Grp_Name,d.subdivision_name" +
            //         " FROM  Mas_Product_Detail a,Mas_Product_Category b,Mas_Product_Group c,mas_subdivision d" +
            //         " WHERE a.product_cat_code = b.product_cat_code AND " +
            //         " a.Product_Grp_Code = c.product_grp_code AND " +
            //         " a.subdivision_code = '" + val + "' AND " +
            //         " a.subdivision_code = convert(varchar(10),d.subdivision_code) and" +
            //         " a.Product_Active_Flag=0 AND a.Division_Code= '" + divcode + "' " +
            //         " ORDER BY 2";
            strQry = " SELECT a.Product_Detail_Code,a.Product_Detail_Name,a.Product_Sale_Unit,a.Product_Description, " +
                   " b.Product_Cat_Name," +
                    " c.Product_Grp_Name,e.Product_Brd_Name, d.subdivision_code FROM  mas_subdivision d,Mas_Product_Category b, " +
                    " Mas_Product_Group c , Mas_Product_Brand e join Mas_Product_Detail a on a.subdivision_code like '" + val + ',' + "%'  or " +
                     " a.subdivision_code like '%" + ',' + val + "' or a.subdivision_code like '%" + ',' + val + ',' + "%' " +
                     " WHERE a.product_cat_code = b.product_cat_code AND a.Product_Grp_Code = c.product_grp_code AND a.Product_Brd_Code = e.Product_Brd_Code" +
                     " AND a.Product_Active_Flag=0 AND  a.Division_Code= '" + divcode + "' and d.subdivision_code ='" + val + "'" +
                     "order by Product_Detail_Name";
            try
            {
                dsProCat = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProCat;
        }
        public DataTable getDTProduct_State(string divcode, string val)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dsProCat = null;

            strQry = " SELECT a.Product_Detail_Code,a.Product_Detail_Name,a.Product_Sale_Unit,a.Product_Description, " +
                     "  b.Product_Cat_Name," +
                      " c.Product_Grp_Name,e.Product_Brd_Name d.State_Code FROM  Mas_State d,Mas_Product_Category b, " +
                      " Mas_Product_Group c ,Mas_Product_Brand e join Mas_Product_Detail a on a.State_Code like '" + val + ',' + "%'  or " +
                       " a.State_Code like '%" + ',' + val + "' or a.State_Code like '%" + ',' + val + ',' + "%' " +
                       " WHERE a.product_cat_code = b.product_cat_code AND a.Product_Grp_Code = c.product_grp_code AND a.Product_Brd_Code = e.Product_Brd_Code" +
                       " AND a.Product_Active_Flag=0 AND a.Division_Code= '" + divcode + "' and d.State_Code ='" + val + "'" +
                       "order by Product_Detail_Name";
            try
            {
                dsProCat = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProCat;
        }
        //changes done by Reshmi
        public DataSet FindProduct(string search, string searchname, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            strQry = " SELECT Product_Detail_Code,Product_Detail_Name,Product_Description,Product_Sale_Unit " +

                     " FROM  Mas_Product_Detail a,Mas_Product_Category b,Mas_Product_Brand d" +
                     " WHERE a.product_cat_code = b.product_cat_code AND " +

                     "a.Product_Brd_Code = d.Product_Brd_Code AND " +
                     "a.Product_Active_Flag=0 " +
                     " and a.Product_Detail_Name like N'%" + searchname + "%'  and  a.Division_Code= '" + div_code + "' " +
                     " ORDER BY 2";
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
        //Changes done by Priya And Reshmi
        //begin
        public DataSet getProd_Re(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            //strQry = " SELECT Product_Detail_Code,Product_Detail_Name,Product_Sale_Unit,Product_Type_Code,Product_Description, " +
            //         " Product_Sample_Unit_One,Product_Sample_Unit_Two,Product_Sample_Unit_Three " +
            //         " FROM  Mas_Product_Detail " +
            //         " WHERE Product_Active_Flag=1 AND Division_Code= '" + divcode + "' " +
            //         " ORDER BY 2";
            strQry = " SELECT a.Product_Detail_Code,a.Product_Detail_Name,a.Product_Sale_Unit,a.Product_Description, " +
                " b.Product_Cat_Name,c.Product_Grp_Name,d.Product_Brd_Name,a.Product_Mode,a.Sale_Erp_Code,a.Sample_Erp_Code" +
                " FROM  Mas_Product_Detail a,Mas_Product_Category b,Mas_Product_Group c,Mas_Product_Brand d" +
                " WHERE a.product_cat_code = b.product_cat_code AND " +
                "a.Product_Grp_Code = c.product_grp_code AND " +
                "a.Product_Brd_Code = d.Product_Brd_Code AND " +
                "a.Product_Active_Flag=1 AND a.Division_Code= '" + divcode + "' " +
                " ORDER BY Prod_Detail_Sl_No";
            try
            {
                dsProCat = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProCat;
        }
        //end

        // Sorting For ProductViewList 
        public DataTable getProductallList_DataTable(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dtProCat = null;

            strQry = " SELECT a.Product_Detail_Code,a.Product_Detail_Name,a.Product_Sale_Unit,a.Product_Description, " +
                     " b.Product_Cat_Name,c.Product_Grp_Name,d.Product_Brd_Name" +
                     " FROM  Mas_Product_Detail a,Mas_Product_Category b,Mas_Product_Group c,Mas_Product_Brand d" +
                     " WHERE a.product_cat_code = b.product_cat_code AND " +
                     "a.Product_Grp_Code = c.product_grp_code AND " +
                     "a.Product_Brd_Code = d.Product_Brd_Code AND " +
                     "a.Product_Active_Flag=0 AND a.Division_Code= '" + divcode + "' " +
                     " ORDER BY 2";
            try
            {
                dtProCat = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtProCat;
        }

        public DataSet getProdSlNo(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            strQry = " SELECT P.Product_Detail_Code,P.Product_Detail_Name,P.Product_Description,P.Product_Sale_Unit," +
                     " case when P.Product_Type_Code = 'R' then 'Regular' " +
                     " else case when P.Product_Type_Code = 'N' then 'New' else 'Others' end end Product_Type_Name, " +
                     " C.Product_Cat_Name, G.Product_Grp_Name, B.Product_Brd_Name" +
                     " FROM  Mas_Product_Detail P,Mas_Product_Category C,Mas_Product_Group G,Mas_Product_Brand B" +
                     " WHERE P.Product_Cat_Code = C.Product_Cat_Code AND " +
                     " P.Product_Grp_Code = G.Product_Grp_Code AND " +
                     " P.Product_Brd_Code = B.Product_Brd_Code AND " +
                     " P.Product_Active_Flag=0 AND P.Division_Code= '" + divcode + "' " +
                     " ORDER BY Prod_Detail_Sl_No";
            try
            {
                dsProCat = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProCat;
        }
        //Changes done by Priya  AND Reshmi
        public DataTable getProductdet_DataTable(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dtProCat = null;

            strQry = " SELECT P.Product_Detail_Code,P.Product_Detail_Name,P.Product_Description,P.Product_Sale_Unit," +
                    " case when P.Product_Type_Code = 'R' then 'Regular' " +
                    " else case when P.Product_Type_Code = 'N' then 'New' else 'Others' end end Product_Type_Name, " +
                    " C.Product_Cat_Name, G.Product_Grp_Name,B.Product_Brd_Name" +
                    " FROM  Mas_Product_Detail P,Mas_Product_Category C,Mas_Product_Group G,Mas_Product_Brand B" +
                    " WHERE P.Product_Cat_Code = C.Product_Cat_Code AND " +
                    " P.Product_Grp_Code = G.Product_Grp_Code AND " +
                    " P.Product_Brd_Code = B.Product_Brd_Code AND " +
                    " P.Product_Active_Flag=0 AND P.Division_Code= '" + divcode + "' " +
                    " ORDER BY 2";
            try
            {
                dtProCat = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtProCat;
        }

        public int RecordBulkAdd(string Product_Detail_Code, string Product_Detail_Name, string Product_Description, string sSaleUnit1, string sSaleUnit2, string sSaleUnit3, string sSaleUnit4, string Sample_Erp_Code, int Product_Cat_Code, int Product_Grp_Code, string State_Code, string subdivision_code, int Division_Code, int Product_Brd_Code)
        {
            int iReturn = -1;
            int iSlNo = -1;
            int icodeSlNo = -1;
            if (!RecordExistdet(Product_Detail_Code, Division_Code))
            {
                if (!sRecordExistdet(Product_Detail_Name, Division_Code))
                {
                    try
                    {

                        DB_EReporting db = new DB_EReporting();

                        strQry = "SELECT ISNULL(MAX(Prod_Detail_Sl_No),0)+1 FROM Mas_Product_Detail WHERE Division_Code = '" + Division_Code + "' ";
                        iSlNo = db.Exec_Scalar(strQry);


                        strQry = "SELECT ISNULL(MAX(product_code_slno),0)+1 FROM Mas_Product_Detail";
                        icodeSlNo = db.Exec_Scalar(strQry);


                        strQry = " insert into Mas_Product_Detail (Product_Detail_Code,Product_Detail_Name,Product_Description,Product_Sale_Unit,Base_Unit_code,product_unit,Unit_code,Sample_Erp_Code, " +
                                 " Product_Cat_Code,Product_Grp_Code, " +
                                 " Product_Type_Code,State_Code,subdivision_code,Division_Code,Created_Date,LastUpdt_Date,Product_Active_Flag ,Prod_Detail_Sl_No ,Product_Brd_Code,product_code_slno ) " +
                                 " VALUES('" + Product_Detail_Code + "', '" + Product_Detail_Name + "', '" + Product_Description + "', '" + sSaleUnit1 + "','" + sSaleUnit2 + "','" + sSaleUnit3 + "','" + sSaleUnit4 + "','" + Sample_Erp_Code + "','" + Product_Cat_Code + "', '" + Product_Grp_Code + "','N','" + State_Code + "','" + subdivision_code + "','" + Division_Code + "',getdate(),getdate(),0," + iSlNo + ",'" + Product_Brd_Code + "'," + icodeSlNo + ")";



                        iReturn = db.ExecQry(strQry);
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
            }
            else
            {
                iReturn = -3;
            }
            return iReturn;
        }
        public DataSet FillProductBrand(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProBrd = null;

            strQry = " SELECT '' as Product_Brd_Code, '---Select---' as Product_Brd_Name " +
                    " UNION " +
                    " SELECT Product_Brd_Code,Product_Brd_Name FROM  Mas_Product_Brand " +
                    " WHERE Product_Brd_Active_Flag=0 AND Product_Brd_Code> 0 AND Division_Code= '" + divcode + "' " +
                    " ORDER BY 2";

            try
            {
                dsProBrd = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProBrd;
        }
        public DataSet FillUOM(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProBrd = null;

            strQry = " SELECT 0 as Move_MailFolder_Id,'---Select---' as Move_MailFolder_Name " +
                         " UNION " +
                         " SELECT Move_MailFolder_Id,Move_MailFolder_Name " +
                         " FROM Mas_Multi_Unit_Entry " +
                         " WHERE Division_Code in  (" + divcode + ") and Folder_Act_flag=0";

            try
            {
                dsProBrd = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProBrd;
        }
        public DataSet getProdCatgType(string prod_code, string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            strQry = " SELECT Product_Cat_Code,Product_Type_Code,State_Code,subdivision_code,Product_Brd_Code FROM  Mas_Product_Detail " +
                     " WHERE Product_Detail_Code='" + prod_code + "' AND Division_Code= '" + divcode + "' ";
            try
            {
                dsProCat = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProCat;
        }
        public int DeleteProductRate(string state_code, string div_code, string SubDiv = "0")
        {
            int iReturn = -1;

            int iSlNo = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();


                //  strQry = "Delete from Mas_Product_State_Rates where State_Code='" + state_code + "' and Division_Code='" + div_code + "'";
                strQry = "delete r  from  Mas_Product_State_Rates r inner join Mas_Product_Detail p on R.product_Detail_code=P.Product_Detail_code  where r.State_Code='" + state_code + "' and r.Division_Code='" + div_code + "'   and ( '" + SubDiv + "'='0' or charindex(','+'" + SubDiv + "'+',',','+subdivision_code +',')>0) ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }
        public int DeleteProductRate(string div_code)
        {
            int iReturn = -1;

            int iSlNo = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();


                strQry = "Delete from Mas_Product_State_Rates where Division_Code='" + div_code + "'";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }

        public DataSet getProduct_State(string Division_Code, string product_Detail_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsState = null;
            strQry = "SELECT State_Code  " +
                     " FROM Mas_Product_Detail " +
                     " where Division_Code = '" + Division_Code + "' and product_Detail_Code='" + product_Detail_Code + "'";

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

        public DataSet getProduct_Exp(string Division_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsState = null;
            strQry = "SELECT '0' as Product_Code_SlNo,'---Select the Product---' as Product_Detail_Name union all " +
                     "SELECT '-1' as Product_Code_SlNo,'All Product' as Product_Detail_Name union all " +
                     "SELECT Product_Code_SlNo,Product_Detail_Name  " +
                     " FROM Mas_Product_Detail " +
                     " where Division_Code = '" + Division_Code + "' and Product_Active_Flag=0 ";

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

        //Prod Code Change


        public int RecordUpdateProductCode(string ProdCode, string NewCode, string divcode)
        {
            int iReturn = -1;
            if (!RecordExistcode(NewCode, divcode))
            {

                try
                {
                    DB_EReporting db = new DB_EReporting();



                    strQry = "UPDATE Mas_Product_State_Rates " +
                          " SET Product_Detail_Code = '" + NewCode + "' " +
                          " WHERE Product_Detail_Code='" + ProdCode + "' and Division_Code = '" + divcode + "'";

                    iReturn = db.ExecQry(strQry);

                    strQry = "UPDATE Trans_SS_Entry_Detail " +
                      " SET Product_Detail_Code = '" + NewCode + "' " +
                      " WHERE Product_Detail_Code='" + ProdCode + "' and Division_Code = '" + divcode + "'";

                    iReturn = db.ExecQry(strQry);

                    strQry = "UPDATE Trans_TargetFixation_Product_Details " +
                      " SET Product_Code = '" + NewCode + "' " +
                      " WHERE Product_Code='" + ProdCode + "' and Division_Code = '" + divcode + "'";

                    iReturn = db.ExecQry(strQry);

                    strQry = "UPDATE Trans_Sample_Despatch_Details " +
                   " SET Product_Code = '" + NewCode + "' " +
                   " WHERE Product_Code='" + ProdCode + "' and Division_Code = '" + divcode + "'";

                    iReturn = db.ExecQry(strQry);

                    strQry = "UPDATE Trans_DCR_BusinessEntry_Details " +
                   " SET Product_Code = '" + NewCode + "' " +
                   " WHERE Product_Code='" + ProdCode + "' and Division_Code = '" + divcode + "'";

                    iReturn = db.ExecQry(strQry);

                    strQry = "UPDATE Mas_Product_Detail " +
                                                   "SET Product_Detail_Code='" + NewCode + "' " +
                                                   "WHERE Product_Detail_Code='" + ProdCode + "' AND Division_Code='" + divcode + "' and Product_Active_Flag=0";


                    iReturn = db.ExecQry(strQry);
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
        public bool RecordExistcode(string NewCode, string divcode)
        {
            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(Product_Detail_Code) FROM Mas_Product_Detail WHERE Product_Detail_Code='" + NewCode + "'AND Division_Code='" + divcode + "' ";
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

        // product Upload

        public DataSet getProductGroup_UP(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            strQry = " SELECT Move_MailFolder_Id,Move_MailFolder_Name FROM  Mas_Multi_Unit_Entry " +
                     " WHERE Folder_Act_flag=0 AND Division_Code= '" + divcode + "' " +
                     " ORDER BY 2";
            try
            {
                dsProCat = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProCat;
        }
        public DataSet getProductCategory_UP(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            strQry = " SELECT Product_Cat_Code,Product_Cat_Name FROM  Mas_Product_Category " +
                     " WHERE Product_Cat_Active_Flag=0 AND Product_Cat_Code> 0 AND Division_Code= '" + divcode + "' " +
                     " ORDER BY 2";
            try
            {
                dsProCat = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProCat;
        }

        public DataSet getProductBrand_UP(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProBrd = null;

            strQry = " SELECT Product_Brd_Code,Product_Brd_Name FROM  Mas_Product_Brand " +
                     " WHERE Product_Brd_Active_Flag=0 AND Product_Brd_Code> 0 AND Division_Code= '" + divcode + "' " +
                     " ORDER BY 2";
            try
            {
                dsProBrd = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProBrd;
        }

        public int GetProductCode()
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT ISNULL(MAX(Product_Code_SlNo),0)+1 FROM Mas_Product_Detail";

                iReturn = db.Exec_Scalar(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;

        }

        public DataSet GetProd_Cat_Code(string Cat_Name, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;
            strQry = "select Product_Cat_Code,Product_Cat_Name from Mas_Product_Category where Product_Cat_Name='" + Cat_Name + "' and Division_Code = '" + div_code + "' and Product_Cat_Active_Flag=0";

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
        public DataSet GetProd_Grp_Code(string Qrp_Name, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;
            strQry = "select Product_Grp_Code,Product_Grp_Name from Mas_Product_Group where Product_Grp_Name='" + Qrp_Name + "' and Division_Code = '" + div_code + "' and Product_Grp_Active_Flag=0";

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

        public DataSet GetProd_Brd_Code(string Brd_Name, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;
            strQry = "select Product_Brd_Code,Product_Brd_Name from Mas_Product_Brand where Product_Brd_Name='" + Brd_Name + "' and Division_Code = '" + div_code + "' and Product_Brd_Active_Flag=0";

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
        public DataSet Visit_Doc_Prd(string doc_code, int cmon, int cyear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocCat = null;
            string swhere = string.Empty;

            strQry = " EXEC sp_DCR_Visit_Count_Product '" + doc_code + "', '" + cmon + "', '" + cyear + "' ";

            try
            {
                dsDocCat = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsDocCat;
        }
        public DataSet getPrd_For_Mapp(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocCat = null;
            strQry = " SELECT Product_Code_SlNo,Product_Detail_Name,Product_Sale_Unit  FROM  Mas_Product_Detail " +
                      " WHERE Product_Active_Flag=0 AND Division_Code= '" + div_code + "'  ORDER BY 2 ";

            try
            {
                dsDocCat = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsDocCat;
        }

        public DataSet getprdfor_Mappdr(string Listeddr_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocCat = null;
            strQry = " select Product_Code as Product_Code_SlNo ,Product_Name  from Map_LstDrs_Product " +
                      " where Listeddr_Code ='" + Listeddr_Code + "' ";

            try
            {
                dsDocCat = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsDocCat;
        }
        public DataSet getProdCate(string Div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsState = null;

            strQry = " SELECT 0 as Product_Cat_Code,'--Select--' as Product_Cat_Name " +
                     " UNION " +
                     " SELECT Product_Cat_Code,Product_Cat_Name " +
                     " FROM Mas_Product_Category " +
                     " WHERE Division_Code='" + Div_code + "' and Product_Cat_Active_Flag=0 ";
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
        public DataSet getDiv(string Div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsState = null;

            strQry = " SELECT 0 as subdivision_code,'--Select--' as subdivision_name " +
                     " UNION " +
                     " SELECT subdivision_code,subdivision_name " +
                     " FROM mas_subdivision " +
                     " WHERE Div_Code='" + Div_code + "' and SubDivision_Active_Flag=0 ";
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
        public DataSet prgetDiv(string Div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsState = null;

            strQry = " SELECT '0' as Product_Cat_Div_Code,'--Select--' as Product_Cat_Div_Name " +
                     " UNION " +
                      " SELECT Product_Cat_Div_Code,Product_Cat_Div_Name " +
                     " FROM Mas_Product_Category " +
                     " WHERE Division_Code='" + Div_code + "' and Product_Cat_Active_Flag=0 ";
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
        public DataSet getProdDiv(string ddlCategore, string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsState = null;

            strQry = " SELECT Product_Cat_Div_Code,Product_Cat_Div_Name " +
                     " FROM Mas_Product_Category " +
                     " WHERE Product_Cat_Code='" + ddlCategore + "' AND Division_Code='" + divcode + "' and Product_Cat_Active_Flag=0 ";
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
        public DataSet getRou_Detail(string divcode, string val)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProBrd = null;

            //strQry = " SELECT a.Territory_Code,a.Territory_Name,a.Target,a.Min_Prod " +
            //         " FROM  Mas_Territory_Creation a" +
            //         " WHERE charindex(','+'" + val + "'+',',','+a.Dist_Name+',')>0 AND " +
            //         " a.Territory_Active_Flag=0 AND a.Division_Code= '" + divcode + "' " +
            //         " ORDER BY 2";

            strQry = "SELECT kk.Territory_Code,kk.Territory_Name ,kk.FieldForce, COUNT(md.ListedDrCode) ListedDR_Count from  (  SELECT a.Territory_Code,a.Territory_Name, (select SF_Name+', ' from  mas_salesforce ms where charindex(','+ms.sf_code +',',','+a.sf_code+',')>0  for XML path('')) as FieldForce " +
                     "  FROM  Mas_Territory_Creation a  inner join mas_stockist mt on charindex(','+mt.Stockist_Code +',',','+a.Dist_Name+',')>0 " +
                     " WHERE charindex(','+'" + val + "'+',',','+a.Dist_Name+',')>0 AND a.Territory_Active_Flag=0 AND a.Division_Code= '" + divcode + "' " +
                     " group by a.Territory_Code,a.Territory_Name,a.sf_code )kk left outer join  mas_listeddr md on md.Territory_Code = kk.Territory_Code and md.ListedDr_Active_Flag=0  " +
                      " group by  kk.Territory_Code,kk.Territory_Name,kk.FieldForce order by 2 ";


            try
            {
                dsProBrd = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProBrd;
        }

        public DataTable getProdall_EX(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dsProCat = null;

            strQry = "  SELECT a.Product_Detail_Code as ProductCode ,a.Product_Detail_Name as ProductName,a.Product_Short_Name as Short_Name,a.Product_Description as ProductDescription,a.Sample_Erp_Code as ConversionFactor," +
                     " a.product_grosswt as Grossweight ,a.product_netwt as Netweight,a.Product_Sale_Unit as UOM,a.product_unit as Base_UOM,c.subdivision_name as Sub_Division_Name, a.Sale_Erp_Code as ERP_Code, d.Product_Brd_Name as Brand, b.Product_Cat_Name " +
                     " FROM  Mas_Product_Detail a,Mas_Product_Category b,mas_subdivision c,Mas_Product_Brand d " +
                     " WHERE a.product_cat_code = b.product_cat_code AND " +
                     "a.Product_Brd_Code = d.Product_Brd_Code AND " +
                     " CharIndex(',' + Cast(c.subdivision_code as varchar) + ',', ',' + Cast(a.subdivision_code as varchar) + ',') > 0 AND "+
                     "a.Product_Active_Flag=0 AND a.Division_Code= '" + divcode + "' " +
                     " ORDER BY Prod_Detail_Sl_No";
            try
            {
                dsProCat = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProCat;
        }

        public int insert_target(string div_code, string sf_code, string target, string pcode, string year, string month)
        {
            DB_EReporting db_ER = new DB_EReporting();
            int iReturn = -1;
            strQry = "exec [Insert_target] '" + div_code + "','" + sf_code + "','" + pcode + "','" + target + "','" + month + "','" + year + "'";
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
        public DataSet get_pro_target(string divcode, string sf_code, string year, string months)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            // strQry = "select * from PRODUCT_TARGET_MONTHLY where Division_Code='"+divcode +"' and YEAR='"+year+"' and month='"+months+"'";
            strQry = "exec get_target_values '" + sf_code + "','" + divcode + "','" + year + "','" + months + "'";
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

        public DataSet gettarget(string div_code, string sf_code, string years, string months)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsState = null;

            //     strQry = "select PRODUCT_CODE,sum(cast(TARGET as int)) as target from PRODUCT_TARGET_MONTHLY where Division_Code='"+div_code+"' and MONTH='"+months+"' and YEAR = '"+years+"' and Reporting_To_SF = '"+sf_code+"' group by PRODUCT_CODE,MONTH,YEAR";
            strQry = " exec get_product_target '" + sf_code + "','" + div_code + "','" + months + "','" + years + "'";
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

        public DataSet getquantity(string div_code, string sf_code, string years, string months)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsState = null;

            // strQry = "select Product_Code,sum(Quantity) as Quantity, MONTH( H.Order_Date) from  Trans_Order_Details D inner join Trans_Order_Head H on h.Trans_Sl_No = D.Trans_Sl_No inner join Mas_Product_Detail P on P.Product_Detail_Code=D.Product_Code inner join Mas_Salesforce  m on m.Sf_Code = h.Sf_Code where p.Division_Code='"+div_code+"' and  MONTH( H.Order_Date)='"+months+"' and year( H.Order_Date)='"+years+"'  and  Reporting_To_SF= '"+sf_code+"' group by Product_Code,MONTH( H.Order_Date),year( H.Order_Date)";
            strQry = " exec get_product_qty '" + sf_code + "','" + div_code + "','" + months + "','" + years + "'";
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
        public DataSet gettarget_Fo(string div_code, string sf_code, string years, string months)
        {
            SalesForce sf = new SalesForce();
            DataSet ds = sf.getState_BulkEdit(sf_code, div_code);
            string state_code = "0";
            if (ds.Tables[0].Rows.Count > 0)
                state_code = ds.Tables[0].Rows[0][0].ToString();


            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsState = null;

            // strQry = "select SF_CODE,sum(cast(TARGET as int)) as target from PRODUCT_TARGET_MONTHLY where Division_Code='" + div_code + "' and MONTH='" + months + "' and YEAR = '" + years + "' and Reporting_To_SF = '" + sf_code + "' group by SF_CODE,MONTH,YEAR";
            strQry = "DECLARE @Rty TINYINT select @Rty=(case when " + div_code + "=19 then 1 else 0 end) ; select SF_CODE,sum(cast(TARGET as int)) as target ,sum(cast(TARGET as int) * cast((case when @Rty=1 then MRP_Price else Retailor_Price end) as decimal(38,2))) as value from PRODUCT_TARGET_MONTHLY  P  inner join Mas_Product_State_Rates R on r.Product_Detail_Code = P.PRODUCT_CODE where P.Division_Code='" + div_code + "' and MONTH='" + months + "' and YEAR = '" + years + "'  and State_Code='" + state_code + "' group by SF_CODE,MONTH,YEAR  ";
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

        public DataSet getquantity_Fo(string div_code, string sf_code, string years, string months)
        {

            SalesForce sf = new SalesForce();
            DataSet ds = sf.getState_BulkEdit(sf_code, div_code);
            string state_code = "0";
            if (ds.Tables[0].Rows.Count > 0)
                state_code = ds.Tables[0].Rows[0][0].ToString();

            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsState = null;

            // strQry = "select h.Sf_Code,sum(Quantity) as Quantity, MONTH( H.Order_Date) from  Trans_Order_Details D inner join Trans_Order_Head H on h.Trans_Sl_No = D.Trans_Sl_No inner join Mas_Product_Detail P on P.Product_Detail_Code=D.Product_Code inner join Mas_Salesforce  m on m.Sf_Code = h.Sf_Code where p.Division_Code='" + div_code + "' and  MONTH( H.Order_Date)='" + months + "' and year( H.Order_Date)='" + years + "'  and  Reporting_To_SF= '" + sf_code + "' group by h.Sf_Code,MONTH( H.Order_Date),year( H.Order_Date)";
            strQry = "DECLARE @Rty TINYINT 	select @Rty=(case when " + div_code + "=19 then 1 else 0 end); select Sf_Code,sum(Quantity) as Quantity , sum( cast(Quantity as decimal) * cast((case when @Rty=1 then MRP_Price else Retailor_Price end) as decimal(38,2)) ) as Value from Mas_Product_State_Rates  R inner join Trans_Order_Details D on D.Product_Code = R.Product_Detail_Code   inner join  Trans_Order_Head H on   D.Trans_Sl_No = h.Trans_Sl_No    where Division_Code='" + div_code + "' and State_Code='" + state_code + "' and MONTH( H.Order_Date)='" + months + "' and year( H.Order_Date) = '" + years + "'  group by Sf_Code";
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

        public DataSet get_mrp(string div_code, string sf_code)
        {

            SalesForce sf = new SalesForce();
            DataSet ds = sf.getState_BulkEdit(sf_code, div_code);
            string state_code = "0";
            if (ds.Tables[0].Rows.Count > 0)
                state_code = ds.Tables[0].Rows[0][0].ToString();

            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsState = null;

            //strQry = "select Product_Detail_Code,MRP_Price from Mas_Product_State_Rates where Division_Code='" + div_code + "' and State_Code='" + state_code + "'";
            strQry = "DECLARE @Rty TINYINT select @Rty=(case when " + div_code + "=19 then 1 else 0 end); select Product_Detail_Code,(case when @Rty=1 then MRP_Price else Retailor_Price end) MRP_Price from Mas_Product_State_Rates  R  inner join Trans_Order_Details D on D.Product_Code = R.Product_Detail_Code  inner join  Trans_Order_Head H on   D.Trans_Sl_No = h.Trans_Sl_No  where Division_Code='" + div_code + "' and State_Code='" + state_code + "'";
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

        public DataSet Get_SKY_Analysis(string Div_Code, string Sub_Div, string SF_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet dsState = null;
            strQry = "EXEC SKU_Analysis '" + Div_Code + "','" + Sub_Div + "','" + SF_Code + "'";
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

        public DataSet Get_SKY_Analysis_Product(string Div_Code, string Sub_Div, string SF_Code, string Product_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet dsState = null;
            strQry = "EXEC SKU_Analysis_Product '" + Div_Code + "','" + Sub_Div + "','" + SF_Code + "','" + Product_Code + "'";
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


        public DataSet Get_SKY_Analysis_Product_Month(string Div_Code, string Sub_Div, string SF_Code, string Product_Code, string year)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet dsState = null;
            strQry = "EXEC SKY_Analysis_Year '" + Div_Code + "','" + Sub_Div + "','" + SF_Code + "','" + Product_Code + "','" + year + "'";
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


        public DataSet Get_SKY_Analysis_Product_Month_New(string Div_Code, string Sub_Div, string SF_Code, string Product_Code, string year)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet dsState = null;
            strQry = "EXEC SKY_Analysis_Year '" + Div_Code + "','" + Sub_Div + "','" + SF_Code + "','" + Product_Code + "','" + year + "'";
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


        public DataSet Get_Product_Deatils(string product_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            strQry = " SELECT Product_Detail_Code,Product_Detail_Name,Product_Short_Name " +
                     " FROM  Mas_Product_Detail " +
                     " WHERE Product_Detail_Code = '" + product_code + "' " +
                     " ORDER BY 2";
            try
            {
                dsProCat = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProCat;
        }
        public DataSet Get_StateWise_Value(string Div_Code, string Sub_Div, string Sf_Code, string Pro_code)
        {
            DataSet dsStateValue = null;
            DB_EReporting db_ER = new DB_EReporting();
            strQry = "Exec SKU_Analysis_State '" + Div_Code + "','" + Sub_Div + "','" + Sf_Code + "','" + Pro_code + "'";

            try
            {
                dsStateValue = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsStateValue;
        }

        public DataSet getProdforCode_details(string divcode, string prodcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            strQry = "exec Get_Product_deatials '" + divcode + "','" + prodcode + "'";
            try
            {
                dsProCat = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProCat;
        }

        public DataSet getProd_target_sfcode(string divcode, string sf_code, string prodcode, string year)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet dsProCat = null;
            strQry = "exec GET_Target_Sf_code '" + divcode + "','" + sf_code + "','" + prodcode + "','" + year + "'";
            try
            {
                dsProCat = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProCat;
        }
        public DataSet getCloseRate(string state_code, string div_code, string date)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsRate = null;
            strQry = "EXEC Product_stateWise_Rate_View1 '" + state_code + "', '" + div_code + "','" + date + "' ";

            try
            {
                dsRate = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsRate;
        }
        public DataSet SelectCloseRate(string prod_code, string state_code, string effective_from, decimal mrp_amt, decimal ret_amt, decimal dist_amt, decimal nsr_amt, string div_code, int iStateSlNo)
        {
            DataSet iReturn;

            int iSlNo = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();



                strQry = "select * from  Trans_Secondary_Sales_Details Ts  where Ts.Product_Code='" + prod_code + "' and Ts.Stockist_Code='" + state_code + "' " +
                         " and CONVERT(VARCHAR(10), date, 103)='" + effective_from + "'";

                iReturn = db.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }
        public int InsertProductRate(string prod_code, string state_code, string effective_from, string prod_name, int Op_Qty, int Rec_Qty, decimal dist_amt, int Sale_Qty, decimal mrp_amt, int Retailor_Rate, decimal nsr_amt, int sale_pieces, decimal ret_amt, int RP_BaseRate, int OP_Pieces, int RwFlg, int Rec_Pieces, string Sf_code)
        {
            int iReturn = -1;
            int Con_Qty = 0;


            int iSlNo = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT ISNULL(MAX(Sale_Code),0)+1 FROM Trans_Secondary_Sales_Details ";
                iSlNo = db.Exec_Scalar(strQry);
                strQry = "select Sample_Erp_Code from Mas_Product_Detail where Product_Detail_Code='" + prod_code + "'";
                Con_Qty = db.Exec_Scalar(strQry);


                strQry = "INSERT INTO Trans_Secondary_Sales_Details (Sale_Code,Stockist_Code,date,Product_Code,Product_Name,Op_Qty,Rec_Qty,Cl_Qty,Sale_Qty,Distributer_Rate, " +
                         " Retailor_Rate,pieces,sale_pieces,Conversion_Qty,SfCode,DP_BaseRate,RP_BaseRate,OP_Pieces,RwFlg,Rec_Pieces) VALUES " +
                         " ( '" + iSlNo + "', '" + state_code + "', '" + effective_from + "', '" + prod_code + "', '" + prod_name + "', '" + Op_Qty + "','" + Rec_Qty + "','" + dist_amt + "','" + Sale_Qty + "','" + mrp_amt + "', " +
                         " '" + Retailor_Rate + "','" + nsr_amt + "','" + sale_pieces + "','" + Con_Qty + "','" + Sf_code + "','" + ret_amt + "','" + RP_BaseRate + "','" + OP_Pieces + "','" + RwFlg + "','" + Rec_Pieces + "')";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }
        public int UpdateProductRate(string prod_code, string state_code, string effective_from, decimal mrp_amt, decimal ret_amt, decimal dist_amt, decimal nsr_amt, string div_code, int iStateSlNo)
        {
            int iReturn = -1;

            int iSlNo = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = " update Trans_Secondary_Sales_Details set Cl_Qty='" + dist_amt + "',pieces='" + nsr_amt + "' from " +
                         " Trans_Secondary_Sales_Details Ts  where Ts.Product_Code='" + prod_code + "' and Ts.Stockist_Code='" + state_code + "' " +
                         " and  CONVERT(VARCHAR(10), date, 103) ='" + effective_from + "' ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }


        public DataSet Get_GoodsReceived_List(string divcode, string mon, string yrs)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet dsProCat = null;
            strQry = "select * from Trans_GRN_Head where division_code='" + divcode + "' and month(GRN_Date)='" + mon + "' and year(GRN_Date)='" + yrs + "'";
            try
            {
                dsProCat = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProCat;
        }
        public DataSet Get_GoodsReceived_List1(string divcode, string mon, string yrs)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet dsProCat = null;
            strQry = "select * from Trans_Issue_Slip_Head where  Division_Code='" + divcode + "' and month(Issue_Dt)='" + mon + "' and year(Issue_Dt)='" + yrs + "'";
            try
            {
                dsProCat = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProCat;
        }

        public DataSet getproductname(string div_code, string Sub_Div_Code = "0")
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsState = null;
            // strQry = "exec productratewise '" + div_code  + "','" + Sub_Div_Code + "'";

            strQry = "select Product_Detail_Code,isnull(Product_Short_Name,Product_Detail_Name)Product_Short_Name,Product_Detail_Name,Unit_code,Move_MailFolder_Name,Sample_Erp_Code from Mas_Product_Detail MPD INNER JOIN Mas_Multi_Unit_Entry MUE ON MPD.Unit_code = MUE.Move_MailFolder_Id AND MPD.Division_Code =MUE.Division_Code where MPD.Division_Code='" + div_code + "' and ('" + Sub_Div_Code + "' ='0' or  subdivision_code  IS NULL or  charindex(','+cast('" + Sub_Div_Code + "' as varchar)+',',','+subdivision_code+',')>0  ) and Product_Active_Flag='0' ";
            //strQry = "select Product_Detail_Code,Product_Short_Name,Product_Detail_Name from Mas_Product_Detail where Division_Code='" + div_code + "' and Product_Active_Flag='0' ";
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
        public DataSet getproductnamewithrate(string div_code, string Sub_Div_Code = "0")
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsState = null;
            strQry = "exec productratewise '" + div_code + "','" + Sub_Div_Code + "'";

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
        public DataSet Get_GoodsReceived(string divcode, string grn_no, string grn_date, string supp_code)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet dsProCat = null;
            strQry = "select * from Trans_GRN_Head where division_code='" + divcode + "' and GRN_No='" + grn_no + "' and   GRN_Date=cast(CONVERT(VARCHAR, '" + grn_date + "', 111) as date)  and supp_code='" + supp_code + "'";
            try
            {
                dsProCat = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProCat;
        }

        public DataSet Get_GoodsReceived_withoutNo(string divcode, string grn_date, string supp_code, string received_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet dsProCat = null;
            strQry = "select * from Trans_GRN_Head where division_code='" + divcode + "' and   GRN_Date=cast(CONVERT(VARCHAR, '" + grn_date + "', 111) as date)  and supp_code='" + supp_code + "' and Received_Location = '" + received_Code + "'";
            try
            {
                dsProCat = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProCat;
        }
		
		public DataSet Get_GoodsReceived_withoutNo(string divcode, string grn_date, string supp_code, string received_Code, string po_no)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet dsProCat = null;
            strQry = "select * from Trans_GRN_Head where division_code='" + divcode + "' and   GRN_Date=cast(CONVERT(VARCHAR, '" + grn_date + "', 111) as date)  and supp_code='" + supp_code + "' and Received_Location = '" + received_Code + "' and Po_No='" + po_no + "'";
            try
            {
                dsProCat = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProCat;
        }

        public int Insert_GoodsReceived(string Division_Code, string SF_Code, string grn_no, string grn_date, string Supp_Code, string Supp_Name, string Challan, string po_no, string entry_date, string dispa_date, string received_loc, string received_by, string authorized, string sub_divCode, string remark, string tot_goods, string tot_tax, string tot_netVal, string Receved_Name)
        {
            int iReturn = -1;
            int iSlNo = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT ISNULL(MAX(Trans_Sl_No),0)+1 FROM Trans_GRN_Head";
                iSlNo = db.Exec_Scalar(strQry);

                strQry = " INSERT INTO Trans_GRN_Head (Trans_Sl_No,GRN_No,GRN_Date,Supp_Code,Supp_Name,Challan_No,Po_No,Entry_Date,Dispatch_Date,Received_Location,Received_By,Authorized_By,SF_Code,Division_code,SubDiv_Code,remarks,Net_Tot_Goods,Net_Tot_Tax,Net_Tot_Value,Receved_Name) " +
                         " VALUES ('" + iSlNo + "','" + ("GRN" + iSlNo.ToString()) + "','" + grn_date + "','" + Supp_Code + "','" + Supp_Name + "','" + Challan + "','" + po_no + "','" + entry_date + "','" + dispa_date + "','" + received_loc + "','" + received_by + "'," +
                         " '" + authorized + "','" + SF_Code + "','" + Division_Code + "','" + sub_divCode + "','" + remark + "','" + tot_goods + "','" + tot_tax + "','" + tot_netVal + "','" + Receved_Name + "')";
                iReturn = db.ExecQry(strQry);
                iReturn = iSlNo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }

        //public int Insert_GoodsReceived_Details(string Trans_No, string Pro_Code, string Pro_Name, string uom, string batchno, string qty, string price, string good, string damage, string groosval, string netval, string uom_name, string mfgDate)
        //{
        //    int iReturn = -1;
        //    int iSlNo = -1;
        //    try
        //    {
        //        DB_EReporting db = new DB_EReporting();

        //        strQry = "SELECT ISNULL(MAX(Trans_Dtls_Sl_No),0)+1 FROM Trans_GRN_Details";
        //        iSlNo = db.Exec_Scalar(strQry);

        //        strQry = " INSERT INTO Trans_GRN_Details (Trans_Dtls_Sl_No,Trans_Sl_No,PCode,PDetails,UOM,Batch_No,POQTY,Price,Good,Damaged,Gross_Value,Net_Value,uom_name,mfgdate) " +
        //                 " VALUES ('" + iSlNo + "','" + Trans_No + "','" + Pro_Code + "','" + Pro_Name + "','" + uom + "','" + batchno + "','" + qty + "','" + price + "','" + good + "','" + damage + "','" + groosval + "','" + netval + "','" + uom_name + "','" + mfgDate + "')";
        //        iReturn = db.ExecQry(strQry);
        //        iReturn = iSlNo;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    return iReturn;
        //}

        /*
		public int Insert_GoodsReceived_Details(string Trans_No, string Pro_Code, string Pro_Name, string uom, string batchno, string qty, string price, string good, string damage, string groosval, string netval, string uom_name, string mfgDate, string remark)
        {
            int iReturn = -1;
            int iSlNo = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT ISNULL(MAX(Trans_Dtls_Sl_No),0)+1 FROM Trans_GRN_Details";
                iSlNo = db.Exec_Scalar(strQry);

                strQry = " INSERT INTO Trans_GRN_Details (Trans_Dtls_Sl_No,Trans_Sl_No,PCode,PDetails,UOM,Batch_No,POQTY,Price,Good,Damaged,Gross_Value,Net_Value,uom_name,mfgdate,remark) " +
                         " VALUES ('" + iSlNo + "','" + Trans_No + "','" + Pro_Code + "','" + Pro_Name + "','" + uom + "','" + batchno + "','" + qty + "','" + price + "','" + good + "','" + damage + "','" + groosval + "','" + netval + "','" + uom_name + "','" + mfgDate + "','" + remark + "')";
                iReturn = db.ExecQry(strQry);
                iReturn = iSlNo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }
		*/
		
		public int Insert_GoodsReceived_Details(string Trans_No, string Pro_Code, string Pro_Name, string uom, string batchno, string qty, string price, string good, string damage, string groosval, string netval, string uom_name, string mfgDate, string remarks, string Offer_pro_code, string Offer_pro_name, string offer_pro_unit, string con_factor, string free, string dis, string dis_val, string Tax)
        {
            int iReturn = -1;
            int iSlNo = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT ISNULL(MAX(Trans_Dtls_Sl_No),0)+1 FROM Trans_GRN_Details";
                iSlNo = db.Exec_Scalar(strQry);

                strQry = " INSERT INTO Trans_GRN_Details (Trans_Dtls_Sl_No,Trans_Sl_No,PCode,PDetails,UOM,Batch_No,POQTY,Price,Good,Damaged,Gross_Value,Net_Value,uom_name,mfgdate,remark,Offer_pro_code,Offer_pro_name,Cnv_qty,Off_pro_unit_name,Off_pro_unit_code,Tax,free,dis,dis_val) " +
                         " VALUES ('" + iSlNo + "','" + Trans_No + "','" + Pro_Code + "','" + Pro_Name + "','" + uom + "','" + batchno + "','" + qty + "','" + price + "','" + good + "','" + damage + "','" + groosval + "','" + netval + "','" + uom_name + "','" + mfgDate + "','" + remarks + "','" + Offer_pro_code + "','" + Offer_pro_name + "','" + con_factor + "','" + offer_pro_unit + "','0','" + Tax + "','" + free + "','" + dis + "','" + dis_val + "')";

                iReturn = db.ExecQry(strQry);
                iReturn = iSlNo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }

        public int Insert_GoodsReceived_Tax(string Dtls_No, string Trans_No, string taxCode, string taxName, string taxVal)
        {
            int iReturn = -1;
            int iSlNo = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT ISNULL(MAX(Tax_Sl_No),0)+1 FROM Trans_GRN_Tax_Details";
                iSlNo = db.Exec_Scalar(strQry);

                strQry = "INSERT INTO Trans_GRN_Tax_Details (Tax_Sl_No,Trans_Dtls_Sl_No,Trans_Sl_No,Tax_Code,Tax_Name,Tax_Value) VALUES ('" + iSlNo + "','" + Dtls_No + "','" + Trans_No + "','" + taxCode + "','" + taxName + "','" + taxVal + "')";
                iReturn = db.ExecQry(strQry);
                iReturn = iSlNo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }
        public int Update_GoodsReceived(string Division_Code, string SF_Code, string grn_no, string grn_date, string Supp_Code, string Supp_Name, string Challan, string po_no, string entry_date, string dispa_date, string received_loc, string received_by, string authorized, string sub_divCode, string tranid, string remark, string tot_goods, string tot_tax, string tot_netVal, string Receved_Name)
        {
            int iReturn = -1;
            int iSlNo = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();
                strQry = "update Trans_GRN_Head set Challan_No='" + Challan + "',Po_No='" + po_no + "',Dispatch_Date='" + dispa_date + "',Received_Location='" + received_loc + "',Received_By='" + received_by + "',Authorized_By='" + authorized + "',remarks='" + remark + "',Net_Tot_Goods='" + tot_goods + "',Net_Tot_Tax='" + tot_tax + "',Net_Tot_Value='" + tot_netVal + "',Receved_Name='" + Receved_Name + "' where Trans_Sl_No='" + tranid + "'";
                iReturn = db.ExecQry(strQry);
                iReturn = iSlNo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }
        public int Delete_GoodsReceived_Tax(string Trans_No)
        {
            int iReturn = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();
                strQry = "delete from Trans_GRN_Tax_Details where  Trans_Sl_No='" + Trans_No + "'";
                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }
        public int Delect_GoodsReceived_Details(string Trans_No)
        {
            int iReturn = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();
                strQry = "delete from Trans_GRN_Details where Trans_Sl_No='" + Trans_No + "'";
                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }

        public DataSet Get_GoodsReceived_Details(string divcode, string grn_no)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet dsProCat = null;
            strQry = "select * from Trans_GRN_Details where Trans_Sl_No='" + grn_no + "'";
            try
            {
                dsProCat = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProCat;
        }

        public DataSet Get_GoodsReceived_Tax_Details(string divcode, string grn_no)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet dsProCat = null;
            strQry = "select * from Trans_GRN_Tax_Details where Trans_Sl_No='" + grn_no + "'";
            try
            {
                dsProCat = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProCat;
        }
        public DataSet getIssue_slip_Head(string From, string state_code, string div_code, string date)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsRate = null;


            strQry = "select * from Trans_Issue_Slip_Head where Iss_From='" + From + "' and CONVERT(VARCHAR(10), Issue_Dt, 103)='" + date + "' and Division_Code='" + div_code + "'";

            try
            {
                dsRate = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsRate;
        }
        public DataSet getIssue_slip(string From, string state_code, string div_code, string date)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsRate = null;
            strQry = "EXEC Product_Issue_Slip_View '" + From + "','" + state_code + "', '" + div_code + "','" + date + "' ";

            try
            {
                dsRate = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsRate;
        }
        public DataSet getIssue_slip_Details(string From, string state_code, string div_code, string date)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsRate = null;


            strQry = "select Stock_Type from Trans_Issue_Slip_Details a inner join Trans_Issue_Slip_Head b on b.Iss_No=a.Iss_No where b.Iss_From='" + From + "' and CONVERT(VARCHAR(10), b.Issue_Dt, 103)='" + date + "' and b.Division_Code='" + div_code + "'";

            try
            {
                dsRate = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsRate;
        }
        public int getIssue_slip_Update(string grn_no, string prod_code, string prod_name, string stk_type, decimal ret_amt, decimal qty_amt, decimal val_amt, string res_txt)
        {
            DB_EReporting db_ER = new DB_EReporting();

            int dsRate = -1;


            DB_EReporting db = new DB_EReporting();

            strQry = "UPDATE Trans_Issue_Slip_Details " +
                     " SET Prod_Code = '" + prod_code + "', " +
                     " Prod_Name = '" + prod_name + "' ," +
                     " Stock_Type = '" + stk_type + "' ," +
                     " Rate = '" + ret_amt + "' ," +
                     " Qty = '" + qty_amt + "' ," +
                     " Value = '" + val_amt + "' ," +
                     " Reason = '" + res_txt + "' " +
                     " WHERE Prod_Code = '" + prod_code + "' and Iss_Det_No = '" + grn_no + "' ";



            try
            {
                dsRate = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsRate;
        }
        public int Insert_Issue_slip_head(string Slip_No, string Iss_From, string Iss_From_name, string Iss_To, string iss_To_Name, string Issue_dt, string Slip_no, string Div_code)
        {
            int iReturn;

            int iSlNo = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();


                strQry = "INSERT INTO Trans_Issue_Slip_Head (Iss_From,Iss_From_Name,Iss_To,Iss_To_Name,Issue_Dt,Iss_Eno,Division_Code) VALUES " +
                         " ( '" + Iss_From + "', '" + Iss_From_name + "', '" + Iss_To + "', '" + iss_To_Name + "', '" + Issue_dt + "','" + Slip_no + "','" + Div_code + "');SELECT SCOPE_IDENTITY();";

                iReturn = db.Exec_Scalar(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }


        public DataSet Insert_Issue_slip_Dtl(int Iss_No, string prod_code, string prod_name, string stk_type, decimal ret_amt, decimal qty_amt, decimal val_amt, string txtres)
        {
            DataSet iReturn;

            int iSlNo = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();


                strQry = "INSERT INTO Trans_Issue_Slip_Details (Iss_No,Prod_Code,Prod_Name,Stock_Type,Rate,Qty,Value,Reason) VALUES " +
                         " ('" + Iss_No + "','" + prod_code + "', '" + prod_name + "', '" + stk_type + "', '" + ret_amt + "', '" + qty_amt + "','" + val_amt + "','" + txtres + "')";



                iReturn = db.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }
        public DataSet getCloseRate2(string state_code, string div_code, string date)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsRate = null;
            strQry = "EXEC Product_stateWise_Rate_View2 '" + state_code + "', '" + div_code + "','" + date + "' ";

            try
            {
                dsRate = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsRate;
        }




        public string Insert_Stock_Transfer_Head(string Div_Code, string SF_Code, string Transfer_No, string Transfer_Date, string Transfer_From, string Transfer_From_Nm, string Transfer_To, string Transfer_To_Nm)
        {
            string iReturn = "-1";
            int iSlNo = -1;
            string slno = string.Empty;
            try
            {
                DataSet dsProCat = null;
                DB_EReporting db = new DB_EReporting();

                strQry = "select *  from Mas_SF_SlNo  where divison_code='" + Div_Code + "'";
                dsProCat = db.Exec_DataSet(strQry);

                //strQry = "SELECT  isnull(max(cast(replace(Trans_SlNo,'ST" + dsProCat.Tables[0].Rows[0]["Division_SName"].ToString() + "','') as numeric)),0)+1 as num from Trans_Stock_Transfer_Head";
                strQry = "SELECT  isnull(max(cast(replace(Trans_SlNo,'ST" + dsProCat.Tables[0].Rows[0]["Division_SName"].ToString() + "','') as numeric)),0)+1 as num from Trans_Stock_Transfer_Head where isnumeric(replace(Trans_SlNo,'ST" + dsProCat.Tables[0].Rows[0]["Division_SName"].ToString() + "',''))=1";
                iSlNo = db.Exec_Scalar(strQry);

                slno = "ST" + dsProCat.Tables[0].Rows[0]["Division_SName"].ToString() + iSlNo.ToString();

                strQry = "INSERT INTO Trans_Stock_Transfer_Head (Trans_SlNo,Division_code,Transfer_No,Transfer_Date,Transfer_From,Transfer_From_Nm,Transfer_To,Transfer_To_Nm) VALUES ('" + slno + "','" + Div_Code + "','" + slno + "','" + Transfer_Date + "','" + Transfer_From + "','" + Transfer_From_Nm + "','" + Transfer_To + "','" + Transfer_To_Nm + "')";
                iReturn = db.ExecQry(strQry).ToString();
                iReturn = slno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn.ToString();
        }


        public string Update_Stock_Transfer_Head(string Transfer_No, string Transfer_From, string Transfer_To)
        {
            string iReturn = "-1";
            string slno = string.Empty;
            try
            {
                DB_EReporting db = new DB_EReporting();
                strQry = "update Trans_Stock_Transfer_Head  set Transfer_From='" + Transfer_From + "', Transfer_To='" + Transfer_To + "' where Trans_SlNo='" + Transfer_No + "'";
                iReturn = db.ExecQry(strQry).ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn.ToString();
        }

        public DataSet Get_Stock_Transfer_Head(string Div_Code, string Transfer_No, string Transfer_Date, string Transfer_From, string Transfer_To)
        {
            DB_EReporting db = new DB_EReporting();
            DataSet dsStock = null;
            strQry = "Select * from Trans_Stock_Transfer_Head where Division_code='" + Div_Code + "' and Transfer_No='" + Transfer_No + "' and Transfer_Date='" + Transfer_Date + "' and Transfer_From='" + Transfer_From + "' and Transfer_To='" + Transfer_To + "'";
            try
            {
                dsStock = db.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsStock;
        }

        public DataSet Get_Stock_Transfer_Head_List(string Div_Code, string years, string months)
        {
            DB_EReporting db = new DB_EReporting();
            DataSet dsStock = null;
            strQry = "Select * from Trans_Stock_Transfer_Head where Division_code='" + Div_Code + "' and year(Transfer_Date)='" + years + "' and month(Transfer_Date)='" + months + "'";
            try
            {
                dsStock = db.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsStock;
        }

        public DataSet Get_Stock_Transfer_HeadVal(string TransSlNo)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet dsProCat = null;
            strQry = "select * from Trans_Stock_Transfer_Head where  Trans_SlNo='" + TransSlNo + "'";
            try
            {
                dsProCat = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProCat;
        }


        public DataSet Get_Stock_Transfer_DetailsVal(string TransSlNo)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet dsProCat = null;
            strQry = "select * from Trans_Stock_Transfer_Details where Trans_SlNo='" + TransSlNo + "'";
            try
            {
                dsProCat = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProCat;
        }



        public string Insert_Stock_Transfer_Details(string TransSLNo, string pCode, string pName, string pType, string pType_Name, string pqty, string preason)
        {
            //
            string iReturn = "-1";
            int iSlNo = -1;
            string slno = string.Empty;
            try
            {
                DB_EReporting db = new DB_EReporting();
                strQry = "SELECT ISNULL(MAX(Trans_Dtls_SlNo),0)+1 slno from Trans_Stock_Transfer_Details";
                iSlNo = db.Exec_Scalar(strQry);
                strQry = "INSERT INTO Trans_Stock_Transfer_Details (Trans_Dtls_SlNo,Trans_SlNo,PCode,PName,PType,QTY,Reason) VALUES ('" + iSlNo + "','" + TransSLNo + "','" + pCode + "','" + pName + "','" + pType_Name + "','" + pqty + "','" + preason + "')";
                iReturn = db.ExecQry(strQry).ToString();
                iReturn = slno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn.ToString();
        }
        public int Delete_Stock_Transfer_Details(string Trans_No)
        {
            int iReturn = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();
                strQry = "delete from Trans_Stock_Transfer_Details where  Trans_SlNo='" + Trans_No + "'";
                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }
        //stock
        public DataSet Get_Stock_Head(string Div_Code, string Transfer_No, string Transfer_Date, string Transfer_From)
        {
            DB_EReporting db = new DB_EReporting();
            DataSet dsStock = null;
            strQry = "Select * from Trans_Stock_Adjust_Head where Division_Code='" + Div_Code + "' and Adj_Trans_No='" + Transfer_No + "' and Adj_Date='" + Transfer_Date + "' and Loc_Dist_Name='" + Transfer_From + "'";
            try
            {
                dsStock = db.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsStock;
        }

        public string Insert_Stock_Head(string Div_Code, string SF_Code, string Transfer_No, string Transfer_Date, string Transfer_From, string Transfer_From_Nm)
        {
            string iReturn = "-1";
            int iSlNo = -1;
            string slno = string.Empty;
            try
            {
                DataSet dsProCat = null;
                DB_EReporting db = new DB_EReporting();

                strQry = "select *  from Mas_SF_SlNo  where divison_code='" + Div_Code + "'";
                dsProCat = db.Exec_DataSet(strQry);

                strQry = "SELECT  isnull(max(cast(replace(Adj_Trans_No,'AdjTAR','') as numeric)),0)+1 as num from Trans_Stock_Adjust_Head";
                iSlNo = db.Exec_Scalar(strQry);

                slno = "AdjTAR" + iSlNo.ToString();

                strQry = "INSERT INTO Trans_Stock_Adjust_Head (Adj_Trans_No,Division_Code,Adj_SlNo,Adj_Date,Loc_Dist_Code,Loc_Dist_Name) VALUES ('" + slno + "','" + Div_Code + "','" + Transfer_No + "','" + Transfer_Date + "','" + Transfer_From + "','" + Transfer_From_Nm + "')";
                iReturn = db.ExecQry(strQry).ToString();
                iReturn = slno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn.ToString();
        }

        public string Insert_Stock_Details(string TransSLNo, string pCode, string pName, string pType, string pType_Name, string pType1, string pType_Name1, string pqty, string preason, string Div_code)
        {
            string iReturn = "-1";
            int iSlNo = -1;
            string slno = string.Empty;
            try
            {
                DB_EReporting db = new DB_EReporting();
                //strQry = "SELECT ISNULL(MAX(Trans_Dtls_SlNo),0)+1 slno from Trans_Stock_Adjust_Detail";
                //iSlNo = db.Exec_Scalar(strQry);
                strQry = "INSERT INTO Trans_Stock_Adjust_Detail (Adj_Trans_No,Prod_Code,Prod_Name,From_Type,To_Type,Qty,Reason,Division_Code) VALUES ('" + TransSLNo + "','" + pCode + "','" + pName + "','" + pType_Name + "','" + pType_Name1 + "','" + pqty + "','" + preason + "','" + Div_code + "')";
                iReturn = db.ExecQry(strQry).ToString();
                iReturn = slno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn.ToString();
        }
        public int Delete_Stock_Details(string Trans_No)
        {
            int iReturn = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();
                strQry = "delete from Trans_Stock_Adjust_Detail where  Adj_Trans_No='" + Trans_No + "'";
                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }
        public string Update_Stock_Head(string Transfer_No, string Transfer_From, string Transfer_To)
        {
            string iReturn = "-1";
            string slno = string.Empty;
            try
            {
                DB_EReporting db = new DB_EReporting();
                strQry = "update Trans_Stock_Adjust_Head  set Loc_Dist_Code='" + Transfer_From + "' where Adj_Trans_No='" + Transfer_No + "'";
                iReturn = db.ExecQry(strQry).ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn.ToString();
        }

        public DataSet Get_Stock_HeadVal(string TransSlNo)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet dsProCat = null;
            strQry = "select * from Trans_Stock_Adjust_Head where  Adj_Trans_No='" + TransSlNo + "'";
            try
            {
                dsProCat = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProCat;
        }


        public DataSet Get_Stock_DetailsVal(string TransSlNo)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet dsProCat = null;
            strQry = "select * from Trans_Stock_Adjust_Detail where Adj_Trans_No='" + TransSlNo + "'";
            try
            {
                dsProCat = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProCat;
        }

        public DataSet Get_Stock_Head_List(string Div_Code, string years, string months)
        {
            DB_EReporting db = new DB_EReporting();
            DataSet dsStock = null;
            strQry = "Select * from Trans_Stock_Adjust_Head where Division_code='" + Div_Code + "' and year(Adj_Date)='" + years + "' and month(Adj_Date)='" + months + "'";
            try
            {
                dsStock = db.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsStock;
        }
        public DataSet getCurrentStock(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsState = null;

            strQry = "select Dist_Code,Prod_Code,sum(GStock) as GStock, sum(DStock) as DStock from Trans_CurrStock_Batchwise  where Division_Code='" + div_code + "' group by Dist_Code,Prod_Code,Division_Code";
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

        public DataSet getCurrentStock_Batch(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsState = null;

            strQry = "select Dist_Code,Prod_Code,BatchNo,sum(GStock) as GStock, sum(DStock) as DStock from Trans_CurrStock_Batchwise  where Division_Code='" + div_code + "' group by Dist_Code,Prod_Code,BatchNo,Division_Code";
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

        /*public int Insert_Trans_Stock_Ledger(string Div_Code, string Ledg_Date, string Dist_Code, string Prod_Code, string BatchNo, string GStock, string DStock, string CalType, string Reason, string Ref, string fromRef, string toRef, string EntryBy)
        {
            int iReturn = -1;
            int iSlNo = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT ISNULL(MAX(Ledger_ID),0)+1 FROM Trans_Stock_Ledger";
                iSlNo = db.Exec_Scalar(strQry);

                strQry = " INSERT INTO Trans_Stock_Ledger (Ledger_ID,Ledg_Date,Dist_Code,Prod_Code,BatchNo,GStock,DStock,CalType,Reason,Ref,Division_Code,FrmRef,ToRef,EntryBy) " +
                         " VALUES ('" + iSlNo + "','" + Ledg_Date + "','" + Dist_Code + "','" + Prod_Code + "','" + BatchNo + "','" + GStock + "','" + DStock + "','" + CalType + "','" + Reason + "','" + Ref + "','" + Div_Code + "','" + fromRef + "','" + toRef + "','" + EntryBy + "')";
                iReturn = db.ExecQry(strQry);
                iReturn = iSlNo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }*/
		
		public int Insert_Trans_Stock_Ledger(string Div_Code, string Ledg_Date, string Dist_Code, string Prod_Code, string BatchNo, string GStock, string DStock, string CalType, string Reason, string Ref, string fromRef, string toRef, string EntryBy)
        {
            int iReturn = -1;
            int iSlNo = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT ISNULL(MAX(Ledger_ID),0)+1 FROM Trans_Stock_Ledger";
                iSlNo = db.Exec_Scalar(strQry);

                strQry = " INSERT INTO Trans_Stock_Ledger (Ledger_ID,Ledg_Date,Dist_Code,Prod_Code,BatchNo,GStock,DStock,CalType,Reason,Ref,Division_Code,FrmRef,ToRef,EntryBy) " +
                         " VALUES ('" + iSlNo + "','" + Ledg_Date + "','" + Dist_Code + "','" + Prod_Code + "','" + BatchNo + "','" + GStock + "','" + DStock + "','" + CalType + "','" + Reason + "','" + Ref + "','" + Div_Code + "','" + fromRef + "','" + toRef + "','" + EntryBy + "')";
                iReturn = db.ExecQry(strQry);
                iReturn = iSlNo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }


        public int Update_Trans_Stock_Ledger(string Div_Code, string Ledger_ID, string Dist_Code, string Prod_Code, string Ledg_Date, string GStock, string DStock, string BatchNo, string goods, string damage)
        {
            int iReturn = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();
                //strQry = " update Trans_Stock_Ledger set GStock= (isnull( GStock,0)-" + GStock + ")+" + goods + " , DStock= (isnull(DStock,0)-" + DStock + ")+" + damage + " where  Division_Code='" + Div_Code + "' and  Dist_Code='" + Dist_Code + "' and Prod_Code='" + Prod_Code + "' and Ledg_Date='" + Ledg_Date + "' and BatchNo='" + BatchNo + "'";
                 strQry = " update Trans_Stock_Ledger set GStock= (isnull(GStock,0)+" + GStock + "), DStock= (isnull(DStock,0)+" + DStock + ") where  Division_Code='" + Div_Code + "' and  Dist_Code='" + Dist_Code + "' and Prod_Code='" + Prod_Code + "' and Ledg_Date='" + Ledg_Date + "' and BatchNo='" + BatchNo + "'";
				
                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }


        public DataSet Select_Trans_Stock_Ledger(string Div_Code, string Dist_Code, string Prod_Code, string Ledg_Date, string BatchNo)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet dsProCat = null;
            strQry = "select * from Trans_Stock_Ledger  where Division_Code='" + Div_Code + "' and  Dist_Code='" + Dist_Code + "' and Prod_Code='" + Prod_Code + "' and Ledg_Date='" + Ledg_Date + "' and BatchNo='" + BatchNo + "'";
            try
            {
                dsProCat = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProCat;
        }

        public DataSet Select_Stock_Ledger(string Div_Code, string GRN_No)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet dsProCat = null;
            strQry = "select * from Trans_Stock_Ledger  where Division_Code='" + Div_Code + "' and  ref='" + GRN_No + "'";
            try
            {
                dsProCat = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProCat;
        }
        public int Delect_Stock_Ladger(string Div_Code, string GRN_No)
        {
            int iReturn = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();
                strQry = "delete from Trans_Stock_Ledger  where Division_Code='" + Div_Code + "' and  ref='" + GRN_No + "'";
                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }


       /*
	   public int Insert_Trans_CurrStock_Batchwise(string Div_Code, string Dist_Code, string Prod_Code, string BatchNo, string GStock, string DStock)
        {
            int iReturn = -1;
            int iSlNo = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT ISNULL(MAX(Slno),0)+1 FROM Trans_CurrStock_Batchwise";
                iSlNo = db.Exec_Scalar(strQry);

                strQry = " INSERT INTO Trans_CurrStock_Batchwise (Slno,Dist_Code,Prod_Code,BatchNo,GStock,DStock,Division_Code) " +
                         " VALUES ('" + iSlNo + "','" + Dist_Code + "','" + Prod_Code + "','" + BatchNo + "','" + GStock + "','" + DStock + "','" + Div_Code + "')";
                iReturn = db.ExecQry(strQry);
                iReturn = iSlNo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }
		*/
		
		 public int Insert_Trans_CurrStock_Batchwise(string Div_Code, string Dist_Code, string Prod_Code, string BatchNo, string GStock, string DStock,string Price="0")
        {
            int iReturn = -1;
            int iSlNo = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT ISNULL(MAX(Slno),0)+1 FROM Trans_CurrStock_Batchwise";
                iSlNo = db.Exec_Scalar(strQry);

                strQry = " INSERT INTO Trans_CurrStock_Batchwise (Slno,Dist_Code,Prod_Code,BatchNo,GStock,DStock,Division_Code,Price) " +
                         " VALUES ('" + iSlNo + "','" + Dist_Code + "','" + Prod_Code + "','" + BatchNo + "','" + GStock + "','" + DStock + "','" + Div_Code + "','" + Price + "')";
                iReturn = db.ExecQry(strQry);
                iReturn = iSlNo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }

        public int Update_Trans_CurrStock_Batchwise(string Div_Code, string slno, string Dist_Code, string Prod_Code, string BatchNo, string GStock, string DStock, string goods, string damage)
        {
            int iReturn = -1;

            try
            {
                DB_EReporting db = new DB_EReporting();

                //strQry = " update Trans_CurrStock_Batchwise  set GStock=( isnull(GStock,0)-" + GStock + " )+" + goods + ", DStock=(isnull(DStock,0)-" + DStock + ")+ " + damage + " where Division_Code='" + Div_Code + "' and  Dist_Code='" + Dist_Code + "' and Prod_Code='" + Prod_Code + "' and BatchNo='" + BatchNo + "'";
                  strQry = " update Trans_CurrStock_Batchwise  set GStock=( isnull(GStock,0)+" + GStock + " ), DStock=(isnull(DStock,0)+" + DStock + ") where Division_Code='" + Div_Code + "' and  Dist_Code='" + Dist_Code + "' and Prod_Code='" + Prod_Code + "' and BatchNo='" + BatchNo + "'";
				

                iReturn = db.ExecQry(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }


        public int Update_Trans_CurrStock_Batchwise_Transfer(string Div_Code, string Dist_Code, string Prod_Code, string BatchNo, string values, string mode, string oldVal, string plus)
        {
            int iReturn = -1;

            try
            {
                DB_EReporting db = new DB_EReporting();
                if (mode == "Good")
                {
                    if (plus == "+")
                    {
                        strQry = " update Trans_CurrStock_Batchwise  set GStock= (isnull(GStock,0)-" + oldVal + ")+" + values + " where Division_Code='" + Div_Code + "' and  Dist_Code='" + Dist_Code + "' and Prod_Code='" + Prod_Code + "' and BatchNo='" + BatchNo + "'";
                    }
                    else
                    {
                        strQry = " update Trans_CurrStock_Batchwise  set GStock= (isnull(GStock,0)+" + oldVal + ")-" + values + " where Division_Code='" + Div_Code + "' and  Dist_Code='" + Dist_Code + "' and Prod_Code='" + Prod_Code + "' and BatchNo='" + BatchNo + "'";
                    }
                }
                else
                {
                    if (plus == "+")
                    {
                        strQry = " update Trans_CurrStock_Batchwise  set DStock= (isnull(DStock,0)- " + oldVal + ")+" + values + " where Division_Code='" + Div_Code + "' and  Dist_Code='" + Dist_Code + "' and Prod_Code='" + Prod_Code + "' and BatchNo='" + BatchNo + "'";
                    }
                    else
                    {
                        strQry = " update Trans_CurrStock_Batchwise  set DStock= (isnull(DStock,0)+ " + oldVal + ")-" + values + " where Division_Code='" + Div_Code + "' and  Dist_Code='" + Dist_Code + "' and Prod_Code='" + Prod_Code + "' and BatchNo='" + BatchNo + "'";
                    }
                }
                iReturn = db.ExecQry(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }



        public int Insert_Trans_CurrStock_Batchwise_Transfer(string Div_Code, string Dist_Code, string Prod_Code, string BatchNo, string values, string mode, string plus)
        {
            int iReturn = -1;
            int iSlNo = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT ISNULL(MAX(Slno),0)+1 FROM Trans_CurrStock_Batchwise";
                iSlNo = db.Exec_Scalar(strQry);

                strQry = " INSERT INTO Trans_CurrStock_Batchwise (Slno,Dist_Code,Prod_Code,BatchNo,Division_Code," + mode + ") " +
                         " VALUES ('" + iSlNo + "','" + Dist_Code + "','" + Prod_Code + "','" + BatchNo + "','" + Div_Code + "','" + values + "')";
                iReturn = db.ExecQry(strQry);
                iReturn = iSlNo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }


        public DataSet Select_Trans_CurrStock_Batchwise(string Div_Code, string Dist_Code, string Prod_Code, string BatchNo)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet dsProCat = null;
            strQry = "select * from Trans_CurrStock_Batchwise  where Division_Code='" + Div_Code + "' and   Dist_Code='" + Dist_Code + "' and Prod_Code='" + Prod_Code + "' and BatchNo='" + BatchNo + "'";
            try
            {
                dsProCat = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProCat;
        }

        public DataSet Select_Trans_CurrStock_Distwise(string Div_Code, string Dist_Code, string Prod_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet dsProCat = null;
            strQry = "select * from Trans_CurrStock_Batchwise  where Division_Code='" + Div_Code + "' and   Dist_Code='" + Dist_Code + "' and Prod_Code='" + Prod_Code + "'";
            try
            {
                dsProCat = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProCat;
        }
        public DataSet getCurrentStock_withProduct(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsState = null;

            strQry = "select Dist_Code,Prod_Code,Product_Detail_Name,BatchNo, ISNULL(sum(GStock),0) as GStock , isnull(sum(DStock),0)as DStock from Trans_CurrStock_Batchwise CS inner join Mas_Product_Detail PD on PD.Product_Detail_Code=CS.Prod_Code  where CS.Division_Code='" + div_code + "' group by Dist_Code,Prod_Code,Product_Detail_Name,BatchNo";
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

        //issue slip
        public DataSet Get_Stock_issue_Head_List(string Div_Code, string years, string months)
        {
            DB_EReporting db = new DB_EReporting();
            DataSet dsStock = null;
            strQry = "Select * from Trans_Issue_Slip_Head where Division_Code='" + Div_Code + "' and year(Issue_Dt)='" + years + "' and month(Issue_Dt)='" + months + "'";
            try
            {
                dsStock = db.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsStock;
        }

        public DataSet Get_Stock_issue_Head(string Div_Code, string Transfer_No, string Transfer_Date, string Transfer_From, string Transfer_To)
        {
            DB_EReporting db = new DB_EReporting();
            DataSet dsStock = null;
            strQry = "Select * from Trans_Issue_Slip_Head where Division_code='" + Div_Code + "' and Iss_Eno='" + Transfer_No + "' and Issue_Dt='" + Transfer_Date + "' and Iss_From='" + Transfer_From + "' and Iss_To='" + Transfer_To + "'";
            try
            {
                dsStock = db.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsStock;
        }
        public int Insert_Stock_issue_Head(string Div_Code, string SF_Code, string Transfer_No, string Transfer_Date, string Transfer_From, string Transfer_From_Nm, string Transfer_To, string Auth)
        {
            int iReturn = -1;
            int iSlNo = -1;
            string slno = string.Empty;
            try
            {
                DataSet dsProCat = null;
                DB_EReporting db = new DB_EReporting();

                //strQry = "select *  from Mas_SF_SlNo  where divison_code='" + Div_Code + "'";
                //dsProCat = db.Exec_DataSet(strQry);

                //strQry = "SELECT  isnull(max(cast(replace(Trans_SlNo,'ST" + dsProCat.Tables[0].Rows[0]["Division_SName"].ToString() + "','') as numeric)),0)+1 as num from Trans_Stock_Transfer_Head";
                //iSlNo = db.Exec_Scalar(strQry);

                //slno = "ST" + dsProCat.Tables[0].Rows[0]["Division_SName"].ToString() + iSlNo.ToString();

                strQry = "INSERT INTO Trans_Issue_Slip_Head (Division_code,Iss_Eno,Issue_Dt,Iss_From,Iss_From_Name,Iss_To,Authorised) VALUES ('" + Div_Code + "','" + Transfer_No + "','" + Transfer_Date + "','" + Transfer_From + "','" + Transfer_From_Nm + "','" + Transfer_To + "','" + Auth + "')SELECT SCOPE_IDENTITY();";
                iReturn = db.Exec_Scalar(strQry);
                //iReturn = slno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }
        public string Insert_Stock_issue_Details(string TransSLNo, string pCode, string pName, string pType, string pType_Name, string pqty, string preason)
        {
            //
            string iReturn = "-1";
            int iSlNo = -1;
            string slno = string.Empty;
            try
            {
                DB_EReporting db = new DB_EReporting();
                strQry = "SELECT ISNULL(MAX(Trans_Dtls_SlNo),0)+1 slno from Trans_Stock_Transfer_Details";
                iSlNo = db.Exec_Scalar(strQry);
                strQry = "INSERT INTO Trans_Stock_Transfer_Details (Trans_Dtls_SlNo,Trans_SlNo,PCode,PName,PType,QTY,Reason) VALUES ('" + iSlNo + "','" + TransSLNo + "','" + pCode + "','" + pName + "','" + pType_Name + "','" + pqty + "','" + preason + "')";
                iReturn = db.ExecQry(strQry).ToString();
                iReturn = slno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn.ToString();
        }

        public string Update_Stock_issue_Head(string Transfer_No, string Transfer_From, string Transfer_To)
        {
            string iReturn = "-1";
            string slno = string.Empty;
            try
            {
                DB_EReporting db = new DB_EReporting();
                strQry = "update Trans_Issue_Slip_Head  set Iss_From='" + Transfer_From + "', Iss_To='" + Transfer_To + "' where Iss_No='" + Transfer_No + "'";
                iReturn = db.ExecQry(strQry).ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn.ToString();
        }

        public int Delete_Stock_issue_Details(string Trans_No)
        {
            int iReturn = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();
                strQry = "delete from Trans_Issue_Slip_Details where  Iss_No='" + Trans_No + "'";
                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }

        public string Insert_Stock_issue_Details1(int TransSLNo, string pCode, string pName, string pType, string pType_Name, string prate, string pqty, string pval, string preason)
        {
            //
            string iReturn = "-1";
            int iSlNo = -1;
            string slno = string.Empty;
            try
            {
                DB_EReporting db = new DB_EReporting();
                //strQry = "SELECT ISNULL(MAX(Iss_No),0)+1 slno from Trans_Issue_Slip_Head";
                //iSlNo = db.Exec_Scalar(strQry);
                strQry = "INSERT INTO Trans_Issue_Slip_Details (Iss_No,Prod_Code,Prod_Name,Stock_Type,Rate,Qty,Value,Reason) VALUES ('" + TransSLNo + "','" + pCode + "','" + pName + "','" + pType_Name + "','" + prate + "','" + pqty + "','" + pval + "','" + preason + "')";
                iReturn = db.ExecQry(strQry).ToString();
                iReturn = slno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn.ToString();
        }

        public string Insert_Stock_issue_Details2(string TransSLNo, string pCode, string pName, string pType, string pType_Name, string prate, string pqty, string pval, string preason)
        {
            //
            string iReturn = "-1";
            int iSlNo = -1;
            string slno = string.Empty;
            try
            {
                DB_EReporting db = new DB_EReporting();
                //strQry = "SELECT ISNULL(MAX(Iss_No),0)+1 slno from Trans_Issue_Slip_Head";
                //iSlNo = db.Exec_Scalar(strQry);
                strQry = "INSERT INTO Trans_Issue_Slip_Details (Iss_No,Prod_Code,Prod_Name,Stock_Type,Rate,Qty,Value,Reason) VALUES ('" + TransSLNo + "','" + pCode + "','" + pName + "','" + pType_Name + "','" + prate + "','" + pqty + "','" + pval + "','" + preason + "')";
                iReturn = db.ExecQry(strQry).ToString();
                iReturn = slno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn.ToString();
        }

        public DataSet Get_Stock_issue_HeadVal(string TransSlNo)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet dsProCat = null;
            strQry = "select * from Trans_Issue_Slip_Head where  Iss_No='" + TransSlNo + "'";
            try
            {
                dsProCat = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProCat;
        }

        public DataSet Get_Stock_issue_DetailsVal(string TransSlNo)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet dsProCat = null;
            strQry = "select * from Trans_Issue_Slip_Details where Iss_No='" + TransSlNo + "'";
            try
            {
                dsProCat = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProCat;
        }
        public DataSet getproductname_stock(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsState = null;

            strQry = " select m.Product_Detail_Code,m.Product_Detail_Name,isnull(m.MRP_Price,0)as MRP_Price from (SELECT t1.Product_Detail_Code,t1.Product_Detail_Name,t2.MRP_Price " +
                     " FROM( select c.Product_Detail_Code,c.Product_Detail_Name from Mas_Product_Detail c " +
                     " where c.Division_Code='" + div_code + "'  and c.Product_Active_Flag=0  ) t1  left JOIN " +
                     " (select Ts.Product_Detail_Code,Ts.MRP_Price from " +
                     " Mas_Product_State_Rates Ts  where Division_Code='" + div_code + "') t2 " +
                     " ON t1.Product_Detail_Code = t2.Product_Detail_Code " +
                     " )m group by m.Product_Detail_Code,m.Product_Detail_Name,m.MRP_Price";
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
        public DataSet getSlno_stock(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsState = null;

            strQry = "select Max(Iss_No)+1 slno from Trans_Issue_Slip_Head";
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
        public DataSet getSlno_stock_Adj(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsState = null;

            strQry = "select max(SUBSTRING(Adj_Trans_No, PATINDEX('%[0-9]%', Adj_Trans_No), LEN(Adj_Trans_No)))+1 slno from Trans_Stock_Adjust_Head";
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
        public int Update_Trans_CurrStock_Batchwise_Transfer1(string Div_Code, string Dist_Code, string Prod_Code, string BatchNo, string values, string mode, string oldVal, string plus)
        {
            int iReturn = -1;

            try
            {
                DB_EReporting db = new DB_EReporting();
                if (mode == "Good")
                {
                    if (plus == "+")
                    {
                        strQry = " update Trans_CurrStock_Batchwise  set GStock= (isnull(GStock,0)-" + oldVal + ")+" + values + " where Division_Code='" + Div_Code + "' and  Dist_Code='" + Dist_Code + "' and Prod_Code='" + Prod_Code + "' and BatchNo='" + BatchNo + "'";
                    }
                    else
                    {
                        strQry = " update Trans_CurrStock_Batchwise  set GStock= (isnull(GStock,0)+" + oldVal + ")-" + values + " where Division_Code='" + Div_Code + "' and  Dist_Code='" + Dist_Code + "' and Prod_Code='" + Prod_Code + "' and BatchNo='" + BatchNo + "'";
                    }
                }
                else
                {
                    if (plus == "+")
                    {
                        strQry = " update Trans_CurrStock_Batchwise  set DStock= (isnull(DStock,0)- " + oldVal + ")+" + values + " where Division_Code='" + Div_Code + "' and  Dist_Code='" + Dist_Code + "' and Prod_Code='" + Prod_Code + "' and BatchNo='" + BatchNo + "'";
                    }
                    else
                    {
                        strQry = " update Trans_CurrStock_Batchwise  set DStock= (isnull(DStock,0)+ " + oldVal + ")-" + values + " where Division_Code='" + Div_Code + "' and  Dist_Code='" + Dist_Code + "' and Prod_Code='" + Prod_Code + "' and BatchNo='" + BatchNo + "'";
                    }
                }
                iReturn = db.ExecQry(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }
        public string Update_Stock_issue_Head1(string Transfer_No, string Transfer_From, string Transfer_To, string Aut)
        {
            string iReturn = "-1";
            string slno = string.Empty;
            try
            {
                DB_EReporting db = new DB_EReporting();
                strQry = "update Trans_Issue_Slip_Head  set Iss_From='" + Transfer_From + "', Iss_To_Name='" + Transfer_To + "',Authorised='" + Aut + "' where Iss_No='" + Transfer_No + "'";
                iReturn = db.ExecQry(strQry).ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn.ToString();
        }


        //adj Stock

        public DataSet getproductname_stock_beach(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsState = null;

            strQry = " select m.Product_Detail_Code,m.Product_Detail_Name,isnull(m.BatchNo,0)as BatchNo from " +
                     " (SELECT t1.Product_Detail_Code,t1.Product_Detail_Name,t2.BatchNo  " +
                     " FROM( select c.Product_Detail_Code,c.Product_Detail_Name from Mas_Product_Detail c  " +
                     " where c.Division_Code='" + div_code + "'  and c.Product_Active_Flag=0  ) t1  left JOIN  " +
                     " (select Ts.Prod_Code,Ts.BatchNo from  Trans_CurrStock_Batchwise Ts  where Division_Code='" + div_code + "') t2  " +
                     " ON t1.Product_Detail_Code = t2.Prod_Code  )m group by m.Product_Detail_Code,m.Product_Detail_Name,m.BatchNo";
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

        public DataSet Get_adj_Head(string Div_Code, string Transfer_No, string Transfer_Date, string Transfer_From)
        {
            DB_EReporting db = new DB_EReporting();
            DataSet dsStock = null;
            strQry = "Select * from Trans_Stock_Adjust_Head where Division_Code='" + Div_Code + "' and Adj_Trans_No='" + Transfer_No + "' and Adj_Date='" + Transfer_Date + "' and Loc_Dist_Name='" + Transfer_From + "'";
            try
            {
                dsStock = db.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsStock;
        }

        public DataSet stockProdBrnd(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProBrd = null;

            strQry = " select pb.Product_Brd_Code,pb.Product_Brd_Name,Pd.Product_Detail_Code,pd.Product_Short_Name,pd.product_netwt  from Mas_Product_Brand pb " +
                     " inner join Mas_product_detail pd on pd.Product_Brd_Code=pb.Product_Brd_Code where pb.Division_code=" + divcode + "  and pd.State_Code='12,35,47,24,49,' " + 
                     " group by pb.Product_Brd_Code,pb.Product_Brd_Name,Pd.Product_Detail_Code,pd.Product_Short_Name,pd.product_netwt";
            try
            {
                dsProBrd = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProBrd;
        }

        public DataSet brndprodcnt(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProBrd = null;

            strQry = "select  pb.Product_Brd_Code,count( pd.Product_Brd_Code)cnt  from Mas_Product_Brand pb inner join Mas_product_detail pd on pd.Product_Brd_Code=pb.Product_Brd_Code where pb.Division_code=" + divcode + " and pd.State_Code='12,35,47,24,49,' group by pb.Product_Brd_Code";
            try
            {
                dsProBrd = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProBrd;
        }

        public DataSet stksecsales(string divcode, string distcode, string Fmonth, string Fyear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProBrd = null;

            strQry = " select * from (select row_number() over(partition by Stockist_Code, product_code order by Stockist_Code,[date] desc, product_code)rw," +
                     " Stockist_Code,date,Product_Code,Product_Name,(Op_Qty * Conversion_Qty) + OP_Pieces OP,(Rec_Qty * Conversion_Qty) + Rec_Pieces Rec,((Op_Qty * Conversion_Qty) + OP_Pieces) + ((Rec_Qty * Conversion_Qty) + Rec_Pieces) TPri,(Sale_Qty * Conversion_Qty) + sale_pieces Sal,((Cl_Qty * Conversion_Qty) + pieces) ClStk,(((Cl_Qty * Conversion_Qty) + pieces) * pd.product_netwt) Cl,Conversion_Qty,Distributer_Rate,Retailor_Rate,SfCode,DP_BaseRate,RP_BaseRate from Trans_Secondary_Sales_Details ss inner join mas_product_detail pd " +
                     " on pd.Product_Detail_Code = ss.Product_Code where Stockist_Code = '" + distcode + "' and pd.Division_Code = " + divcode + " and month(date)= " + Fmonth + " and year(date)= " + Fyear + ")as t where rw = 1"; 

            try
            {
                dsProBrd = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProBrd;
        }


        public DataSet prodtar(string divcode, string sfcode, string Fmonth, string Fyear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProBrd = null;

            strQry = "select * from product_target_monthly t where Division_Code=" + divcode + " and Sf_Code='" + sfcode + "' and t.MONTH=" + Fmonth + " and t.YEAR=" + Fyear + "";

            try
            {
                dsProBrd = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProBrd;
        }


        public string Insert_adj_Head(string Div_Code, string SF_Code, string Transfer_No, string Transfer_Date, string Transfer_From, string Transfer_From_Nm, string auth)
        {
            string iReturn = "-1";
            int iSlNo = -1;
            string slno = string.Empty;
            try
            {
                DataSet dsProCat = null;
                DB_EReporting db = new DB_EReporting();

                strQry = "select *  from Mas_SF_SlNo  where divison_code='" + Div_Code + "'";
                dsProCat = db.Exec_DataSet(strQry);

                strQry = "SELECT  isnull(max(cast(replace(Adj_Trans_No,'AdjTAR','') as numeric)),0)+1 as num from Trans_Stock_Adjust_Head";
                iSlNo = db.Exec_Scalar(strQry);

                slno = "AdjTAR" + iSlNo.ToString();

                strQry = "INSERT INTO Trans_Stock_Adjust_Head (Adj_Trans_No,Division_Code,Adj_SlNo,Adj_Date,Loc_Dist_Code,Loc_Dist_Name,Authorised) VALUES ('" + slno + "','" + Div_Code + "','" + Transfer_No + "','" + Transfer_Date + "','" + Transfer_From + "','" + Transfer_From_Nm + "','" + auth + "')";
                iReturn = db.ExecQry(strQry).ToString();
                iReturn = slno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn.ToString();
        }

        public string Insert_adj_Details(string TransSLNo, string pCode, string pName, string pType, string pType_Name, string pType1, string pType_Name1, string pqty, string preason, string Div_code, string pbType, string pbType_Name, string pbType1, string pbType_Name1)
        {
            string iReturn = "-1";
            int iSlNo = -1;
            string slno = string.Empty;
            try
            {
                DB_EReporting db = new DB_EReporting();
                //strQry = "SELECT ISNULL(MAX(Trans_Dtls_SlNo),0)+1 slno from Trans_Stock_Adjust_Detail";
                //iSlNo = db.Exec_Scalar(strQry);
                strQry = "INSERT INTO Trans_Stock_Adjust_Detail (Adj_Trans_No,Prod_Code,Prod_Name,From_Type,To_Type,Qty,Reason,Division_Code,From_Batch,To_Batch) VALUES ('" + TransSLNo + "','" + pCode + "','" + pName + "','" + pType_Name + "','" + pType_Name1 + "','" + pqty + "','" + preason + "','" + Div_code + "','" + pbType + "','" + pbType1 + "')";
                iReturn = db.ExecQry(strQry).ToString();
                iReturn = slno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn.ToString();
        }
        public int Delete_adj_Details(string Trans_No)
        {
            int iReturn = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();
                strQry = "delete from Trans_Stock_Adjust_Detail where  Adj_Trans_No='" + Trans_No + "'";
                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }
        public string Update_adj_Head(string Transfer_No, string Transfer_From, string Transfer_To, string Auth)
        {
            string iReturn = "-1";
            string slno = string.Empty;
            try
            {
                DB_EReporting db = new DB_EReporting();
                strQry = "update Trans_Stock_Adjust_Head  set Loc_Dist_Code='" + Transfer_From + "',Authorised='" + Auth + "' where Adj_Trans_No='" + Transfer_No + "'";
                iReturn = db.ExecQry(strQry).ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn.ToString();
        }
        public DataSet Select_Trans_Curradj_Distwise1(string Div_Code, string Dist_Code, string Prod_Code, string BatchNo)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet dsProCat = null;
            strQry = "select * from Trans_CurrStock_Batchwise  where Division_Code='" + Div_Code + "' and   Dist_Code='" + Dist_Code + "' and Prod_Code='" + Prod_Code + "' and BatchNo='" + BatchNo + "'";
            try
            {
                dsProCat = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProCat;
        }
        public int Update_Trans_Curradj_Batchwise_Transfer1(string Div_Code, string Dist_Code, string Prod_Code, string BatchNo, string values, string mode, string oldVal, string plus)
        {
            int iReturn = -1;

            try
            {
                DB_EReporting db = new DB_EReporting();
                if (mode == "Good")
                {
                    if (plus == "+")
                    {
                        strQry = " update Trans_CurrStock_Batchwise  set GStock= (isnull(GStock,0)-" + oldVal + ")+" + values + " where Division_Code='" + Div_Code + "' and  Dist_Code='" + Dist_Code + "' and Prod_Code='" + Prod_Code + "' and BatchNo='" + BatchNo + "'";
                    }
                    else
                    {
                        strQry = " update Trans_CurrStock_Batchwise  set GStock= (isnull(GStock,0)+" + oldVal + ")-" + values + " where Division_Code='" + Div_Code + "' and  Dist_Code='" + Dist_Code + "' and Prod_Code='" + Prod_Code + "' and BatchNo='" + BatchNo + "'";
                    }
                }
                else
                {
                    if (plus == "+")
                    {
                        strQry = " update Trans_CurrStock_Batchwise  set DStock= (isnull(DStock,0)- " + oldVal + ")+" + values + " where Division_Code='" + Div_Code + "' and  Dist_Code='" + Dist_Code + "' and Prod_Code='" + Prod_Code + "' and BatchNo='" + BatchNo + "'";
                    }
                    else
                    {
                        strQry = " update Trans_CurrStock_Batchwise  set DStock= (isnull(DStock,0)+ " + oldVal + ")-" + values + " where Division_Code='" + Div_Code + "' and  Dist_Code='" + Dist_Code + "' and Prod_Code='" + Prod_Code + "' and BatchNo='" + BatchNo + "'";
                    }
                }
                iReturn = db.ExecQry(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }

        public int Insert_Trans_adj_Ledger(string Div_Code, string Ledg_Date, string Dist_Code, string Prod_Code, string BatchNo, string GStock, string DStock, string CalType, string Reason, string Ref)
        {
            int iReturn = -1;
            int iSlNo = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT ISNULL(MAX(Ledger_ID),0)+1 FROM Trans_Stock_Ledger";
                iSlNo = db.Exec_Scalar(strQry);

                strQry = " INSERT INTO Trans_Stock_Ledger (Ledger_ID,Ledg_Date,Dist_Code,Prod_Code,BatchNo,GStock,DStock,CalType,Reason,Ref,Division_Code) " +
                         " VALUES ('" + iSlNo + "','" + Ledg_Date + "','" + Dist_Code + "','" + Prod_Code + "','" + BatchNo + "','" + GStock + "','" + DStock + "','" + CalType + "','" + Reason + "','" + Ref + "','" + Div_Code + "')";
                iReturn = db.ExecQry(strQry);
                iReturn = iSlNo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }

        public int Update_Trans_Curradj_Batchwise_Transfer(string Div_Code, string Dist_Code, string Prod_Code, string BatchNo, string values, string mode, string plus)
        {
            int iReturn = -1;

            try
            {
                DB_EReporting db = new DB_EReporting();
                if (mode == "Good")
                {
                    strQry = " update Trans_CurrStock_Batchwise  set GStock= isnull(GStock,0) " + plus + "" + values + " where Division_Code='" + Div_Code + "' and  Dist_Code='" + Dist_Code + "' and Prod_Code='" + Prod_Code + "' and BatchNo='" + BatchNo + "'";
                }
                else
                {
                    strQry = " update Trans_CurrStock_Batchwise  set DStock= isnull(DStock,0) " + plus + "" + values + " where Division_Code='" + Div_Code + "' and  Dist_Code='" + Dist_Code + "' and Prod_Code='" + Prod_Code + "' and BatchNo='" + BatchNo + "'";
                }
                iReturn = db.ExecQry(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }

        public int Insert_Trans_Curradj_Batchwise(string Div_Code, string Dist_Code, string Prod_Code, string BatchNo, string GStock, string DStock)
        {
            int iReturn = -1;
            int iSlNo = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT ISNULL(MAX(Slno),0)+1 FROM Trans_CurrStock_Batchwise";
                iSlNo = db.Exec_Scalar(strQry);

                strQry = " INSERT INTO Trans_CurrStock_Batchwise (Slno,Dist_Code,Prod_Code,BatchNo,GStock,DStock,Division_Code) " +
                         " VALUES ('" + iSlNo + "','" + Dist_Code + "','" + Prod_Code + "','" + BatchNo + "','" + GStock + "','" + DStock + "','" + Div_Code + "')";
                iReturn = db.ExecQry(strQry);
                iReturn = iSlNo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }
        public DataSet getCurrentBatch(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsState = null;

            strQry = "select Dist_Code,Prod_Code,BatchNo from Trans_CurrStock_Batchwise  where Division_Code='" + div_code + "'";
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
        public int Update_Trans_CurrStock_Batchwise_Adj1(string Div_Code, string Dist_Code, string Prod_Code, string BatchNo, string values, string mode, string oldVal, string plus)
        {
            int iReturn = -1;

            try
            {
                DB_EReporting db = new DB_EReporting();
                if (mode == "Good")
                {
                    if (plus == "+")
                    {
                        strQry = "update Trans_CurrStock_Batchwise  set GStock= (isnull(GStock,0)-" + values + "),DStock= (isnull(DStock,0)+" + values + ")where Division_Code='" + Div_Code + "' and  Dist_Code='" + Dist_Code + "' and Prod_Code='" + Prod_Code + "' and BatchNo='" + BatchNo + "'";
                        //strQry = " update Trans_CurrStock_Batchwise  set GStock= (isnull(GStock,0)-" + oldVal + ")+" + values + " where Division_Code='" + Div_Code + "' and  Dist_Code='" + Dist_Code + "' and Prod_Code='" + Prod_Code + "' and BatchNo='" + BatchNo + "'";
                    }
                    else
                    {
                        //strQry = " update Trans_CurrStock_Batchwise  set GStock= (isnull(GStock,0)+" + oldVal + ")-" + values + " where Division_Code='" + Div_Code + "' and  Dist_Code='" + Dist_Code + "' and Prod_Code='" + Prod_Code + "' and BatchNo='" + BatchNo + "'";
                        strQry = "update Trans_CurrStock_Batchwise  set GStock= (isnull(GStock,0)+" + oldVal + ")+" + values + ",DStock= (isnull(DStock,0)+" + oldVal + ")-" + values + " where Division_Code='" + Div_Code + "' and  Dist_Code='" + Dist_Code + "' and Prod_Code='" + Prod_Code + "' and BatchNo='" + BatchNo + "'";
                    }
                }
                else
                {
                    if (plus == "+")
                    {
                        strQry = "update Trans_CurrStock_Batchwise  set GStock= (isnull(GStock,0)+" + values + "),DStock= (isnull(DStock,0)-" + values + ") where Division_Code='" + Div_Code + "' and  Dist_Code='" + Dist_Code + "' and Prod_Code='" + Prod_Code + "' and BatchNo='" + BatchNo + "'";
                        //strQry = " update Trans_CurrStock_Batchwise  set DStock= (isnull(DStock,0)- " + oldVal + ")+" + values + " where Division_Code='" + Div_Code + "' and  Dist_Code='" + Dist_Code + "' and Prod_Code='" + Prod_Code + "' and BatchNo='" + BatchNo + "'";
                    }
                    else
                    {
                        //strQry = " update Trans_CurrStock_Batchwise  set DStock= (isnull(DStock,0)+ " + oldVal + ")-" + values + " where Division_Code='" + Div_Code + "' and  Dist_Code='" + Dist_Code + "' and Prod_Code='" + Prod_Code + "' and BatchNo='" + BatchNo + "'";
                        strQry = "update Trans_CurrStock_Batchwise  set GStock= (isnull(GStock,0)+" + oldVal + ")-" + values + ",DStock= (isnull(DStock,0)+" + oldVal + ")+" + values + " where Division_Code='" + Div_Code + "' and  Dist_Code='" + Dist_Code + "' and Prod_Code='" + Prod_Code + "' and BatchNo='" + BatchNo + "'";
                    }
                }
                iReturn = db.ExecQry(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }
        public int Insert_Trans_Stock_Ledger_adj(string Div_Code, string Ledg_Date, string Dist_Code, string Prod_Code, string BatchNo, string GStock, string DStock, string CalType, string Reason, string Ref, string fromRef, string toRef, string EntryBy)
        {
            int iReturn = -1;
            int iSlNo = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT ISNULL(MAX(Ledger_ID),0)+1 FROM Trans_Stock_Ledger";
                iSlNo = db.Exec_Scalar(strQry);

                strQry = " INSERT INTO Trans_Stock_Ledger (Ledger_ID,Ledg_Date,Dist_Code,Prod_Code,BatchNo,GStock,DStock,CalType,Reason,Ref,Division_Code,FrmRef,ToRef,EntryBy) " +
                         " VALUES ('" + iSlNo + "','" + Ledg_Date + "','" + Dist_Code + "','" + Prod_Code + "','" + BatchNo + "','" + GStock + "','" + DStock + "','" + CalType + "','" + Reason + "','" + Ref + "','" + Div_Code + "','" + fromRef + "','" + toRef + "','" + EntryBy + "')";
                iReturn = db.ExecQry(strQry);
                iReturn = iSlNo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }

      //  public DataSet Get_Stock_Out_standing(string TransSlNo, string Div_code)
     //   {
      //      DB_EReporting db_ER = new DB_EReporting();
     //       DataSet dsProCat = null;
      //      strQry = "select isnull(a.Out_standing_Amt,0)Out_standing_Amt,isnull(b.Credit_Limit,0)Credit_Limit from " +
     //                "Order_Collection_Details a right outer join Mas_ListedDr b on a.Cust_Code=b.ListedDrCode " +
     //                "where b.Division_Code='" + TransSlNo + "' and  b.ListedDrCode='" + Div_code + "'";

     //       try
     //       {
     //           dsProCat = db_ER.Exec_DataSet(strQry);
     //       }
    //        catch (Exception ex)
     //       {
     //           throw ex;
     //       }
     //       return dsProCat;
     //   }

        public DataSet Get_Opning_Ledger(string div_code, string Cust_Code, string fromDate, string toDate)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsState = null;

            strQry = "SELECT prod_code, [1] AS plus, [0] AS minus, isnull([1],0) - ISNULL([0],0) as tot FROM (select  prod_code, Dist_Code,calType, sum(GStock) as Goods   from Trans_Stock_Ledger where Dist_Code='" + Cust_Code + "' and ledg_date<'" + fromDate + "' AND Division_Code='" + div_code + "' group by prod_code,Dist_Code,CalType) AS SourceTable PIVOT (  SUM(Goods)  FOR CalType IN ([1], [0]) ) AS PivotTable; ";
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
        public DataSet Get_Ledger_PurRet(string div_code, string Cust_Code, string fromDate, string toDate)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsState = null;

            strQry = "select  prod_code, Dist_Code,calType,ENTRYbY, sum(GStock) as Goods   from Trans_Stock_Ledger where Dist_Code='" + Cust_Code + "' and ledg_date>='" + fromDate + "' AND ledg_date<='" + toDate + "'  AND Division_Code='" + div_code + "' group by prod_code,Dist_Code,CalType,ENTRYbY ";
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

        public DataSet getVanCurrentStock_withProduct(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsState = null;

            strQry = "select SF_Code,PCode,Product_Detail_Name,Batch_No, ISNULL(sum(GQty),0) as GStock , isnull(sum(DQty),0)as DStock from Trans_VanCurrStock CS inner join Mas_Product_Detail PD on PD.Product_Detail_Code=CS.PCode  where CS.Division_Code='" + div_code + "' group by SF_Code,PCode,Product_Detail_Name,Batch_No";
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
        public int getMaxCateSlNo(string state_code, string div_code)
        {
            int iReturn = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT ISNULL(MAX(Max_State_Sl_No),0)+1 FROM Mas_Product_Category_Rates WHERE Cate_Code = '" + state_code + "' AND Division_Code='" + div_code + "' ";
                iReturn = db.Exec_Scalar(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }
        public int DeleteCateRate(string div_code)
        {
            int iReturn = -1;

            int iSlNo = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();


                strQry = "Delete from Mas_Product_Category_Rates where Division_Code='" + div_code + "'";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }
        public int DeleteCateRate(string state_code, string div_code, string subDiv="0")
        {
            int iReturn = -1;

            int iSlNo = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();

                //strQry = "Delete from Mas_Product_Category_Rates where Cate_Code='" + state_code + "' and Division_Code='" + div_code + "'";
                strQry ="delete st  from Mas_Product_Detail p inner join  Mas_Product_Category_Rates st on st.product_Detail_code=P.Product_Detail_code  where st.Cate_Code='"+state_code+"' and st.Division_Code='"+div_code+"' and ( '"+subDiv+"'  ='0' or CHARINDEX(','+ '"+subDiv+"' +',',','+subdivision_code+',')>0 )";
                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }
        public DataSet getProduct_Cate(string Division_Code, string product_Detail_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsState = null;
            strQry = "SELECT Product_Cat_Code  " +
                     " FROM Mas_Product_Detail " +
                     " where Division_Code = '" + Division_Code + "' and product_Detail_Code='" + product_Detail_Code + "'";

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
        public int UpdateProductCateRate(string prod_code, string state_code, string effective_from, decimal mrp_amt, decimal ret_amt, decimal dist_amt, decimal nsr_amt, decimal target_amt, string div_code, int iStateSlNo, decimal distrbutor_discout_amt, decimal retailer_discount_amt)
        {
            int iReturn = -1;

            int iSlNo = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT ISNULL(MAX(Sl_No),0)+1 FROM Mas_Product_Category_Rates ";
                iSlNo = db.Exec_Scalar(strQry);

                strQry = "INSERT INTO Mas_Product_Category_Rates (Sl_No, Max_State_Sl_No, Cate_Code, Product_Detail_Code, Discount, RP_Base_Rate, " +
                         " RP_Case_Rate, MRP_Rate, Effective_From_Date, Division_Code, Created_Date,LastUpdt_Date) VALUES " +
                         " ( '" + iSlNo + "', '" + iStateSlNo + "', '" + state_code + "', '" + prod_code + "', '" + mrp_amt + "', '" + ret_amt + "',  " +
                         " '" + nsr_amt + "', '" + target_amt + "', '" + effective_from.Substring(6, 4) + "-" + effective_from.Substring(3, 2) + "-" + effective_from.Substring(0, 2) + "', '" + div_code + "', getdate(),getdate())";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }
        public DataSet GetCategorywiseOrderMonth(string div_code, string SF_Code, string FYear, string FMonth, string SubDivCode="0")
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsState = null;

            strQry = "exec CategorywiseOrderMonth '" + div_code + "','" + SF_Code + "','" + FYear + "','" + FMonth + "','" + SubDivCode + "'";
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



        public DataSet GetCategorywiseOrderDay(string div_code, string SF_Code, string Fdate, string SubDivCode="0")
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsState = null;

            strQry = "exec CategorywiseOrderDay '" + div_code + "','" + SF_Code + "','" + Fdate + "','" + SubDivCode + "'";
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

        public DataSet GetProductAnalysisData(string div_code, string SF_Code, string FYear, string FMonth, string SubDivCode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsState = null;

            strQry = "exec ProductPerformanceforMonth '" + div_code + "','" + FYear + "','" + FMonth + "'";
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
        public DataSet GetCategorywiseOrderFieldForce(string div_code, string Fdate, string Tdate)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsState = null;

            strQry = "exec CategorywiseOrderFieldForce '" + div_code + "','" + Fdate + "','" + Tdate + "'";
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

        public DataSet getProd_Cus(string divcode, string Retail_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            strQry = " select  p.product_Detail_Code,product_Detail_Name, " +
                     " isnull(rtrim(Retailor_Price), 0) as RP_Base_Rate  From Mas_ListedDr dr " +
                     " join mas_salesforce ms on charindex(',' + cast(ms.sf_code as varchar) + ',', ',' + dr.Sf_Code + ',')> 0 " +
                     " join Mas_Product_Detail p on  charindex(',' + cast(ms.State_Code as varchar) + ',', ',' + P.State_Code + ',') > 0 " +
                     " join Mas_Product_State_Rates R on R.product_Detail_code = P.Product_Detail_code and " +
                     " r.State_Code = ms.State_Code " +
                     " where Product_Active_Flag = 0 and ListedDrCode = '" + Retail_code + "'  and p.Division_code = '" + divcode + "' " +
                     " group by  p.product_Detail_Code,product_Detail_Name,Retailor_Price " +
                     " ORDER BY 2";
            try
            {
                dsProCat = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProCat;
        }

        public string Get_svDCRMain(string Div_Code, string sf_code, string ADt)
        {
            DB_EReporting db = new DB_EReporting();
            string iReturn = "";
            strQry = "select Trans_SlNo from DCRMain_Trans where Division_Code='" + Div_Code + "' and  Sf_Code='" + sf_code + "' and CONVERT(varchar, Activity_Date, 23)='" + ADt + "'";
            try
            {
                iReturn = db.Exec_Scalar_s(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }

        public string InsertsvDCRMain(string ARCd, string SF, string STy, string ADt, int Wtyp, string TwnCd, string div, string Rmks, string SysIP, string ETyp)
        {
            DB_EReporting db_ER = new DB_EReporting();
            string iReturn = "";
            DataSet dsState = null;
            int wotyp = -1;

            if (SysIP == "")
            {
                strQry = "select type_code from vwMas_WorkType_App where Division_Code='" + div + "' and SFTyp='" + STy + "' and FWFlg='F'";
            }
            else
            {
                strQry = "select type_code from vwMas_WorkType_App where Division_Code='" + div + "' and SFTyp='" + STy + "' and Wtype='" + SysIP + "'";
            }
            wotyp = db_ER.Exec_Scalar(strQry);

            //strQry = "INSERT INTO Trans_Issue_Slip_Head (Division_code,Iss_Eno,Issue_Dt,Iss_From,Iss_From_Name,Iss_To,Authorised) VALUES ('" + Div_Code + "','" + Transfer_No + "','" + Transfer_Date + "','" + Transfer_From + "','" + Transfer_From_Nm + "','" + Transfer_To + "','" + Auth + "')SELECT SCOPE_IDENTITY();";
            strQry = "exec svDCRMain_Web '" + SF + "','" + ADt + "','" + wotyp + "','" + TwnCd + "','" + div + "','" + Rmks + "','" + ARCd + "'";
            try
            {
                iReturn = db_ER.Exec_Scalar_s(strQry);
                //dsState = db_ER.Exec_DataSet(strQry);
            }

            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }

        public string InsertDCRLstDet(string @ARCd, string @ARDCd, string @SF, string @vTyp, string @vCode, string @CusNm, string @vTime, string @POB, string @WW, string @cProd, string @nProd, string @cAProd, string @nAProd, string @GC, string @GN, string @GQ, string @cAGft, string @nAGft, string @TwnCd, string @Rmks, string @div, string @Rx, string @ModTm, string @lat, string @lon, string @DtaSF, string @GeoAdd, string @POB_Value, string @netwightval, string @stockist_code, string @stockist_name, string @discountprice, string @rateMode, string @DemoTo)
        {
            DB_EReporting db_ER = new DB_EReporting();
            string iReturn = "";
            DataSet dsState = null;
            int wotyp = -1;

            strQry = "exec svDCRLstDet_Web '" + ARCd + "','" + @ARDCd + "','" + @SF + "','" + @vTyp + "','" + @vCode + "','" + @CusNm + "','" + @vTime + "', " +
               " '" + @POB + "','" + @WW + "','" + @cProd + "','" + @nProd + "','" + @cAProd + "','" + @nAProd + "' ,'" + @GC + "' ,'" + @GN + "' ,'" + @GQ + "', " +
               " '" + @cAGft + "' ,'" + @nAGft + "','" + @TwnCd + "','" + @Rmks + "','" + @div + "' ,'" + @Rx + "' ,'" + @ModTm + "' ,'" + @lat + "','" + @lon + "' , " +
               " '" + @DtaSF + "' ,'" + @GeoAdd + "','" + @POB_Value + "' ,'" + @netwightval + "','" + @stockist_code + "','" + @stockist_name + "' , " +
               " '" + @discountprice + "','" + @rateMode + "','" + @DemoTo + "'";
            try
            {
                iReturn = db_ER.Exec_Scalar_s(strQry);
                //dsState = db_ER.Exec_DataSet(strQry);
            }

            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }

        public string InsertDCRCSHLstDet(string @ARCd, string @ARDCd, string @SF, string @vTyp, string @vCode, string @CusNm, string @vTime, string @POB, string @WW, string @cAProd, string @nAProd, string @cAGft, string @nAGft, string @TwnCd, string @Rmks, string @div, string @Rx, string @ModTm, string @lat, string @lon, string @DtaSF, string @GeoAdd, string @POB_Value, string @instrument_type, string @dateofinstrument)
        {
            DB_EReporting db_ER = new DB_EReporting();
            string iReturn = "";
            DataSet dsState = null;
            int wotyp = -1;

            strQry = "exec svDCRCSHDet_Web '" + ARCd + "','" + @ARDCd + "','" + @SF + "','" + @vTyp + "','" + @vCode + "','" + @CusNm + "','" + @vTime + "', " +
               " '" + @POB + "','" + @WW + "','" + @cAProd + "','" + @nAProd + "','" + @cAGft + "','" + @nAGft + "' ,'" + @TwnCd + "' ,'" + @Rmks + "' ,'" + @div + "', " +
               " '" + @Rx + "' ,'" + @ModTm + "','" + @lat + "','" + @lon + "' , " +
               " '" + @DtaSF + "' ,'" + @GeoAdd + "','" + Convert.ToDecimal(@POB_Value) + "' ,'" + @instrument_type + "','" + @dateofinstrument + "'";
            try
            {
                iReturn = db_ER.Exec_Scalar_s(strQry);
                //dsState = db_ER.Exec_DataSet(strQry);
            }

            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }

        public string InsertSecOrder(string @SF, string @CustCd, string @Stk, string @RutCd, string @RutTar, string @OrdDt, string @OrdVal, string @CollAmt, string @NWT, string @Rmk, string @Disc, string @DisAmt, string @RateMode, string @ARC, string @Div, string @OrdDet)
        {
            DB_EReporting db_ER = new DB_EReporting();
            string iReturn = "";
            DataSet dsState = null;
            int wotyp = -1;

            strQry = "exec svSecOrder '" + @SF + "','" + @CustCd + "','" + @Stk + "','" + @RutCd + "','" + @RutTar + "','" + @OrdDt + "','" + @OrdVal + "', " +
                     " '" + @CollAmt + "','" + @NWT + "','" + @Rmk + "','" + @Disc + "','" + @DisAmt + "','" + @RateMode + "' ,'" + @ARC + "' ,'" + @Div + "' ,'" + @OrdDet + "'";

            try
            {
                iReturn = db_ER.Exec_Scalar_s(strQry);
                //dsState = db_ER.Exec_DataSet(strQry);
            }

            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }

        public string InsertPriOrder(string @SF, string @Stk, string @OrdDt, string @OrdVal, string @PayTyp, string @PayDt, string @CollAmt, string @Rmk, string @ARC, string @Div, string @OrdDet, string @OrdID, string @POT)
        {
            DB_EReporting db_ER = new DB_EReporting();
            string iReturn = "";
            DataSet dsState = null;
            int wotyp = -1;

            strQry = "exec svPriOrder '" + @SF + "','" + @Stk + "','" + @OrdDt + "','" + @OrdVal + "', " +
                     " '" + @PayTyp + "','" + @PayDt + "','" + @CollAmt + "','" + @Rmk + "','" + @ARC + "','" + @Div + "' ,'" + @OrdDet + "' ,'" + @OrdID + "' ,'" + @POT + "'";

            try
            {
                iReturn = db_ER.Exec_Scalar_s(strQry);
                //dsState = db_ER.Exec_DataSet(strQry);
            }

            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }

        public int MydayPlanRecordAdd(string sf_code, string sf_mb_code, string Pln_Date, string cluster, string remarks, string Divi_Code, string STy, string ClstrName, string stockist)
        {
            int iReturn = -1;

            try
            {
                DateTime dt_TourPlan = DateTime.Now;
                string day = dt_TourPlan.ToString("yyyy-MM-dd");
                DB_EReporting db = new DB_EReporting();
                DataSet dsListedDR = new DataSet();

                int Division_Code = -1;
                int iCount = -1;
                int wotyp = 0;
                string fg = string.Empty;

                if (sf_mb_code == "")
                {
                    strQry = "select type_code from vwMas_WorkType_App where Division_Code='" + Divi_Code + "' and SFTyp='" + STy + "' and FWFlg='F'";
                    fg = "F";
                }
                else
                {
                    strQry = "select type_code from vwMas_WorkType_App where Division_Code='" + Divi_Code + "' and SFTyp='" + STy + "' and Wtype='" + sf_mb_code + "'";
                    fg = "";

                }
                wotyp = db.Exec_Scalar(strQry);

                if (fg == "")
                {
                    strQry = "select FWFlg from vwMas_WorkType_App where Division_Code='" + Divi_Code + "' and SFTyp='" + STy + "' and type_code='" + wotyp + "'";
                    fg = db.Exec_Scalar_s(strQry);
                }
                else
                { }



                strQry = " select * from TbMyDayPlan " +
                            " where sf_code='" + sf_code + "' and CONVERT(VARCHAR(10),Pln_Date,126)='" + Pln_Date + "' " +
                            " and Division_Code= '" + Divi_Code + "' ";

                dsListedDR = db.Exec_DataSet(strQry);

                if (dsListedDR.Tables[0].Rows.Count == 0)
                {
                    strQry = "insert into TbMyDayPlan (sf_code,sf_member_code,Pln_Date,cluster,remarks,Division_Code,wtype,FWFlg,ClstrName,stockist)" +
                            " VALUES('" + sf_code + "', '" + sf_mb_code + "','" + Pln_Date + "', '" + cluster + "', " +
                            "'" + remarks + "','" + Divi_Code + "','" + wotyp + "','" + fg + "',  '" + ClstrName + "', '" + stockist + "') ";

                    iReturn = db.ExecQry(strQry);
                }
                else
                {

                    strQry = "update TbMyDayPlan set sf_code='" + sf_code + "', sf_member_code='" + sf_mb_code + "',Pln_Date='" + Pln_Date + "'," +
                             "cluster = '" + cluster + "', remarks='" + remarks + "',Division_Code='" + Divi_Code + "',wtype='" + wotyp + "', " +
                             " FWFlg='" + fg + "', ClstrName='" + ClstrName + "', stockist='" + stockist + "' " +
                             "where SF_Code='" + sf_code + "' and Division_Code= '" + Divi_Code + "' and CONVERT(VARCHAR(10),Pln_Date,126)='" + Pln_Date + "' ";

                    iReturn = db.ExecQry(strQry);


                }



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
        public DataSet Get_Product_Target_vs_Sal(string SF_Code, string FYear, string FMonth, string TYear, string TMonth)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;
            strQry = "exec Get_Product_Target_vs_Sal '" + SF_Code + "','" + FYear + "','" + FMonth + "','" + TYear + "','" + TMonth + "'";

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

        public DataSet getProductUOM(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            strQry = " select Product_Cat_Code,UOM_Weight from mas_product_detail where division_code='" + divcode + "' and Product_Active_Flag='0' group by Product_Cat_Code,UOM_Weight order by UOM_Weight";
            try
            {
                dsProCat = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProCat;
        }
        public DataSet Get_Scheme_Names(string DivCode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;
            strQry = "select convert(varchar,Effective_To,103) EfDate,Scheme_Name from mas_scheme where division_code='" + DivCode + "' and cast(convert(varchar,Effective_To,101)as datetime) >=cast(convert(varchar,getdate(),101)as datetime)  GROUP BY Scheme_Name,convert(varchar,Effective_To,103)";

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
        public DataSet Get_Scheme_Values(string DivCode, string schemName)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;
            //  strQry = "select Product_Code,Scheme,Free,Discount,Package,State_Code,Stockist_Code,Scheme_Name,Effective_From,Effective_To from mas_scheme where division_code='" + DivCode + "' and Scheme_Name='" + schemName + "'";
            strQry = "getschemaval '" + DivCode + "','" + schemName + "'";

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

        public DataSet getProductCategory(string divcode, string subdivcode)
        {
            DB_EReporting dbe_ER = new DB_EReporting();

            DataSet dsAdmin = null;
            strQry = "SELECT Product_Cat_Code,Product_Cat_Name FROM  Mas_Product_Category WHERE Product_Cat_Active_Flag=0 AND Product_Cat_Code> 0 AND Division_Code= '" + divcode + "' and Product_Cat_Div_Code='" + subdivcode + "'";

            try
            {
                dsAdmin = dbe_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsAdmin;
        }

        public DataSet getProCat(string divcode, string subdivcode)
        {
            DB_EReporting dbe_ER = new DB_EReporting();

            DataSet dsAdmin = null;
            strQry = "select Product_Detail_Code,Product_Detail_Name,Product_Cat_Code,Prod_Detail_Sl_No,Product_Brd_Code,row_number() over(partition by Product_Cat_Code order by Product_Code_SlNo) Product_Code_SlNo  from mas_product_detail where division_code='" + divcode + "' and charindex(','+cast(" + subdivcode + " as varchar)+',',','+subdivision_code+',')>0 and Product_Active_Flag=0 order by Product_Detail_Name";

            try
            {
                dsAdmin = dbe_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsAdmin;
        }

        public int updateSequence(string divcode, string prodcode, string sequenceNo, string printsequence)
        {
            DB_EReporting dbe_ER = new DB_EReporting();

            int iReturn = -1;

            strQry = "update mas_product_detail set Prod_Detail_Sl_No=" + sequenceNo + ",Product_Code_SlNo="+ printsequence + " where Division_Code=" + divcode + " and Product_Detail_Code='" + prodcode + "'";

            try
            {
                iReturn = dbe_ER.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;
        }

        public int Delete_Scheme_Values(string DivCode, string schemName)
        {
            int iReturn = -1;
            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "DELETE FROM  mas_scheme WHERE  division_code='" + DivCode + "' and Scheme_Name='" + schemName + "'";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }

        public DataSet getdailycallproduct(string sfcode, string odate)
        {
            DB_EReporting dbe_ER = new DB_EReporting();

            DataSet dsAdmin = null;
            strQry = "select  distinct d.Product_Code Product_Detail_Code,d.Product_Name Product_Detail_Name from trans_order_head od  inner join trans_order_details d on od.Trans_sl_no=d.trans_sl_no where od.Sf_Code='" + sfcode + "' and CONVERT(date,Order_Date)='" + odate + "'";

            try
            {
                dsAdmin = dbe_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsAdmin;
        }

        public DataSet getslbProduct(string divcode, string code, string fdt, string tdt, string dcode)
        {
            DB_EReporting dbe_ER = new DB_EReporting();

            DataSet dsAdmin = null;
            strQry = "select distinct d.Product_Code,pd.Product_Short_Name Product_Name from Trans_Order_Head h inner join Trans_Order_Details d on d.Trans_Sl_No=h.Trans_Sl_No inner join Mas_ListedDr dr on dr.ListedDrCode=h.Cust_Code inner join Mas_Product_Detail pd on pd.Product_Detail_Code=d.Product_Code where h.Div_ID='" + divcode + "' and h.Stockist_Code='" + dcode + "' and dr.Purchase_slab='" + code + "' and CONVERT(date,h.Order_Date) between '" + fdt + "'  and '" + tdt + "'";

            try
            {
                dsAdmin = dbe_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsAdmin;
        }

public DataSet getBrandname(string div_code, string Sub_Div_Code = "0")
         {
             DB_EReporting db_ER = new DB_EReporting();

             DataSet dsState = null;

             strQry = "select Product_Brd_Code,Product_Brd_Name from  Mas_Product_Brand where  Product_Brd_Active_Flag=0  and Division_Code='" + div_code + "'";
             //strQry = "select Product_Detail_Code,Product_Short_Name,Product_Detail_Name from Mas_Product_Detail where Division_Code='" + div_code + "' and Product_Active_Flag='0' ";
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


        public DataTable getProdUpload(string divcode)
        {
            DB_EReporting dber = new DB_EReporting();

            DataTable dsAdmin = null;
            strQry = "declare @slno int " +
                     " select @slno=0 " +
                     " select @slno+ROW_NUMBER() over( order by Product_Detail_Name) SlNo,Product_Detail_Code ProductCode,Sale_Erp_Code Erp_Code,Product_Detail_Name ProductName,Sample_Erp_Code Conversion_Factor from Mas_Product_Detail where Division_Code='" + divcode + "' and Product_Active_Flag=0 ";

            try
            {
                dsAdmin = dber.Exec_DataTable(strQry);
            }

            catch (Exception ex)
            {
                throw ex;
            }

            return dsAdmin;
        }

        public DataSet newgetProgrp(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            strQry = "exec get_productgrp '" + divcode + "'";
            try
            {
                dsProCat = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProCat;
        }

        public string savProgrp(string divcode, string grpname, string grpsname, string grpcode)
        {
            DB_EReporting db_ER = new DB_EReporting();
            string msg = string.Empty;
            DataSet dsProCat = null;

            if (grpcode != "" && grpcode != null)
            {
                strQry = "update mas_product_group set Product_Grp_Name=N'" + grpname + "',Product_Grp_SName=N'" + grpsname + "',LastUpdt_Date=GETDATE() where Division_Code='" + divcode + "' and Product_Grp_Code=" + grpcode + "";
            }
            else
            {
                strQry = "exec insertMasProductGroup N'" + grpname + "',N'" + grpsname + "','" + divcode + "'";
            }

            try
            {
                dsProCat = db_ER.Exec_DataSet(strQry);
                msg = "Success";
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            return msg;
        }

        public DataSet editProgrp(string divcode, string grpcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            strQry = "select * from mas_product_group where Division_Code='" + divcode + "' and Product_Grp_Code='" + grpcode + "'";
            try
            {
                dsProCat = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProCat;
        }

        public DataSet getStkProds(string Stk)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            strQry = "exec GetProductStk '" + Stk + "',-1";
            try
            {
                dsProCat = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProCat;
        }

        public DataSet Get_GoodsReceived_Details1(string divcode, string grn_no, string grn_date, string code)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet dsProCat = null;
            strQry = "select tgd.*, mpd.Sample_Erp_Code from Trans_GRN_Head tgh join [Trans_GRN_Details] tgd on tgh.Trans_Sl_No=tgd.Trans_Sl_No join Mas_Product_Detail mpd on tgd.PCode=mpd.Product_Detail_Code where tgh.division_code='" + divcode + "' and " +
                     " tgd.Trans_Sl_No= '" + grn_no + "' and GRN_Date = cast(CONVERT(VARCHAR, '" + grn_date + "', 111) as date)  and supp_code = '" + code + "'";
            try
            {
                dsProCat = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProCat;
        }

        public DataSet Get_Stock_Out_standing(int Cus_code, string Div_code, string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet dsProCat = null;
            //strQry = "select isnull(a.Out_standing_Amt,0)Out_standing_Amt,isnull(b.Credit_Limit,0)Credit_Limit from " +
            //         "Order_Collection_Details a right outer join Mas_ListedDr b on a.Cust_Code=b.ListedDrCode " +
            //         "where b.Division_Code='" + Div_code + "' and  b.ListedDrCode='" + Cus_code + "'";
            strQry = "EXEC check_retailer_outstanding '" + Cus_code + "','" + Div_code + "' ";

            try
            {
                dsProCat = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProCat;
        }
		
		public int DeActivate_QuizTitle(int SurveyID)
        {
            int iReturn = -1;

            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "UPDATE dbo.QuizTitleCreation SET Active=1,LastUpdate_Date=getdate() WHERE Survey_Id = " + SurveyID + "";
                iReturn = db.ExecQry(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;

        }
        public DataSet Quiz_Category_List(string divCode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsCategory = null;
            strQry = "select Category_Id,Category_ShortName,Category_Name from dbo.QuizCategory_Creation where Division_Code='" + divCode + "' and Category_Active=0 ORDER BY Category_Name";

            try
            {
                dsCategory = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsCategory;
        }
        public DataTable Quiz_Category_List_Sorting(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dtCategory = null;

            strQry = "select Category_Id,Category_ShortName,Category_Name from dbo.QuizCategory_Creation where Division_Code='" + divcode + "' and Category_Active=0 ORDER BY Category_ShortName,Category_Name";

            try
            {
                dtCategory = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtCategory;
        }
        public int Update_Quiz_Category(string Category_SName, string Category_Name, string Div_Code, int CategoryId)
        {
            int iReturn = -1;
            //if (!S_RecordQuiz_SubName(Category_SName, Div_Code))
            //{
            //    if (!Record_Category_Name(Category_Name, Div_Code))
            //    {
                    try
                    {

                        DB_EReporting db = new DB_EReporting();

                        strQry = "UPDATE dbo.QuizCategory_Creation SET Category_ShortName = '" + Category_SName + "',Category_Name = '" + Category_Name + "',Last_Update_Date = GETDATE(),Division_Code='" + Div_Code + "' WHERE Category_Id = " + CategoryId + "";


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
            //}
            //else
            //{
            //    iReturn = -3;
            //}
            return iReturn;

        }
        public bool S_RecordQuiz_SubName(string Category_SName, string divcode)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT * FROM dbo.QuizCategory_Creation WHERE Category_ShortName='" + Category_SName + "' and Division_Code= '" + divcode + "' and Category_Active=0 ";
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

        /*-------------------------- Quiz Category Name Exist(29/01/2018) -------------------------------------*/

        public bool Record_Category_Name(string Category_Name, string divcode)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT * FROM dbo.QuizCategory_Creation WHERE Category_Name='" + Category_Name + "' and Division_Code= '" + divcode + "' and Category_Active=0 ";
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

        public int DeActivate_Category(int CategoryId)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "UPDATE dbo.QuizCategory_Creation SET Category_Active=1,Last_Update_Date = GETDATE() WHERE Category_Id = " + CategoryId + "";


                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;

        }
        public int AddQuiz_Category_Details(string Category_SName, string Category_Name, string Div_Code)
        {


            int iReturn = -1;
            if (!S_RecordQuiz_SubName(Category_SName, Div_Code))
            {
                if (!Record_Category_Name(Category_Name, Div_Code))
                {
                    try
                    {
                        DB_EReporting db = new DB_EReporting();

                        int CategoryId = -1;
                        strQry = "SELECT ISNULL(MAX(Category_Id),0)+1 FROM dbo.QuizCategory_Creation ";
                        CategoryId = db.Exec_Scalar(strQry);

                        strQry = "INSERT INTO dbo.QuizCategory_Creation(Category_Id,Category_ShortName,Category_Name,Created_Date,Division_Code,Category_Active)" +
                                 "values(" + CategoryId + ",'" + Category_SName + "', '" + Category_Name + "',GETDATE(),'" + Div_Code + "',0) ";

                        iReturn = db.ExecQry(strQry);
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
            }
            else
            {
                iReturn = -3;
            }
            return iReturn;

        }
        public DataSet Get_Quiz_Category(string divcode, int Category_Id)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsCategory = null;

            strQry = "SELECT Category_ShortName,Category_Name " +
                     " FROM dbo.QuizCategory_Creation WHERE Category_Id= '" + Category_Id + "' AND Division_Code= '" + divcode + "' and Category_Active=0 " +
                     " ORDER BY 2";
            try
            {
                dsCategory = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsCategory;
        }
        public DataSet GetQuizInputOption(int QueId, string DivCode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsOption = null;

            strQry = "select Input_Id,Correct_Ans from dbo.AddInputOptions where Question_Id=" + QueId + " and Division_Code='" + DivCode + "'";
            try
            {
                dsOption = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsOption;

        }
        public int QuizOption_Update(int QusId, int InputOpt_Id, string AnsOpt, string CorAns)
        {
            int iReturn = -1;

            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = " update dbo.AddInputOptions set Input_Text='" + AnsOpt + "', Correct_Ans='" + CorAns + "' where Input_Id=" + InputOpt_Id + " and Question_Id=" + QusId + "";
                iReturn = db.ExecQry(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;

        }

        public int Quiz_Update_Question(int QusId, string Ques_text, string DivCode)
        {
            int iReturn = -1;

            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "update dbo.AddQuestions set Question_Text='" + Ques_text + "', Division_Code='" + DivCode + "' where Question_Id=" + QusId + "";
                iReturn = db.ExecQry(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;

        }
        public DataSet Process_SelectedUser_List(string div_Code, string Survey_ID)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProcess = null;
            strQry = "select ProcessId,Sf_Code,Sf_Name from dbo.Processing_UserList where SurveyId='" + Survey_ID + "' and Div_Code='" + div_Code + "'";

            try
            {
                dsProcess = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProcess;
        }
        public DataSet Process_UserList_Subdiv(string div_Code, string Sf_Code, string sub_div)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsUserList = null;
            strQry = " EXEC [dbo].[UserList_Process_Quiz_Subdiv] '" + div_Code + "', '" + Sf_Code + "', '" + sub_div + "'";

            try
            {
                dsUserList = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsUserList;
        }
        public DataSet Process_UserList_Desig(string div_Code, string Sf_Code, string desig)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsUserList = null;
            strQry = " EXEC [dbo].[UserList_Process_Quiz_Desig] '" + div_Code + "', '" + Sf_Code + "', '" + desig + "'";

            try
            {
                dsUserList = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsUserList;
        }
        public DataSet Process_UserList_jod(string div_Code, string Sf_Code, string imonth, string iyear, string st)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsUserList = null;


            //sp_UserList_Process

            //sp_SalesForceMgrGet

            strQry = " EXEC [dbo].[UserList_Process_Quiz_Jod] '" + div_Code + "', '" + Sf_Code + "', '" + imonth + "','" + iyear + "','" + st + "'";

            try
            {
                dsUserList = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsUserList;
        }

        public DataSet Process_UserList(string div_Code, string Sf_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsUserList = null;
            strQry = " EXEC [dbo].[sp_UserList_Process_Quiz] '" + div_Code + "', '" + Sf_Code + "'";

            try
            {
                dsUserList = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsUserList;
        }
  public int Insert_Pritarget(string div_code, string sf_code, string target, string pcode, string year, string month)
        {
            DB_EReporting db_ER = new DB_EReporting();
            int iReturn = -1;
            strQry = "exec Insert_Pritarget '" + div_code + "','" + sf_code + "','" + pcode + "','" + target + "','" + month + "','" + year + "'";
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
 public DataSet get_Pritarget_values(string divcode, string sf_code, string year, string months)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            // strQry = "select * from PRODUCT_TARGET_MONTHLY where Division_Code='"+divcode +"' and YEAR='"+year+"' and month='"+months+"'";
            strQry = "exec get_Pritarget_values '" + sf_code + "','" + divcode + "','" + year + "','" + months + "'";
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
	public DataSet get_Target_QntyFF(string div_code, string sfcode,  string year)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsState = null;
            strQry = "exec get_Target_Qnty_FF '" + div_code + "','" + year + "','" + sfcode + "'";

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
		
	  public DataSet Get_Orderwise_count_details(string divcode, string sfCode, string Month,string FYear)
        {

            DB_EReporting db_ER = new DB_EReporting();
            DataSet ds = null;

            try
            {
                strQry = "exec Sec_OrderWiseCount_details '" + divcode + "','" + sfCode + "','" + Month + "','"+ FYear + "'";
                ds = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return ds;

        }
    public DataSet Get_Orderwise_count(string divcode, string sfCode, string FYear)
        {

            DB_EReporting db_ER = new DB_EReporting();
            DataSet ds = null;

            try
            {
                strQry = "exec OrderWise_Count_Details '" + sfCode + "','" + divcode + "','" + FYear + "'";
                ds = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return ds;

        }

        public DataSet stksecsalesprog(string divcode, string distcode, string Fmonth, string Tmonth, string Fyear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProBrd = null;

            //strQry = " select DATENAME(MONTH,date)Mnth,row_number() over(partition by Stockist_Code, product_code order by Stockist_Code,[date] desc, product_code)rw," +
            //          " Stockist_Code,date,Product_Code,Product_Name,(Op_Qty * Conversion_Qty) + OP_Pieces OP,((Cl_Qty * Conversion_Qty) + pieces) ClStk,Conversion_Qty,Distributer_Rate,Retailor_Rate,SfCode,DP_BaseRate,RP_BaseRate from Trans_Secondary_Sales_Details ss inner join mas_product_detail pd " +
            //          " on pd.Product_Detail_Code = ss.Product_Code where Stockist_Code = '" + distcode + "' and pd.Division_Code = " + divcode + " and month(date) between " + Fmonth + " and " + Tmonth + " and year(date)= " + Fyear + "";

            strQry = "exec getClOpStk '" + distcode + "'," + Fmonth + "," + Tmonth + ",'" + Fyear + "','" + divcode + "'";
            try
            {
                dsProBrd = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProBrd;
        }

        public DataSet stksecsalesprod(string divcode, string subdivcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProBrd = null;

            strQry = "exec getStkSecSalesProdDets "+divcode+",'"+subdivcode+"'";
            try
            {
                dsProBrd = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProBrd;
        }

        public DataSet stksecsalesmfgdt(string distcode, string Fmonth, string Tmonth, string Fyear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProBrd = null;

            strQry = "select row_number() over(partition by Stockist_Code, product_code order by Stockist_Code,Purchase_Date desc, product_code)rw," +
                     " Stockist_code,Product_Code,Mgf_date,Datename(MONTH,Purchase_Date)MN,Purchase_Date from Trans_Stock_Updation_Details" +
                     " where Stockist_Code = '" + distcode + "' and MONTH(Purchase_Date)between " + Fmonth + " and " + Tmonth + " and YEAR(Purchase_Date)=" + Fyear + " and Mgf_date<>''";
            try
            {
                dsProBrd = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProBrd;
        }

        public DataSet getSecSalesPriamry(string distcode, string Fmonth, string Tmonth, string Fyear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProBrd = null;

            strQry = "exec getSecSalesPriamry '" + distcode + "'," + Fmonth + "," + Tmonth + ",'" + Fyear + "'";
            try
            {
                dsProBrd = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProBrd;
        }

        public DataSet getSecSalesSecondary(string distcode, string Fmonth, string Tmonth, string Fyear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProBrd = null;

            strQry = "exec getSecSalesSecondary '" + distcode + "'," + Fmonth + "," + Tmonth + ",'" + Fyear + "'";
            try
            {
                dsProBrd = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProBrd;
        }

        public DataSet getProdUOM(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProBrd = null;

            strQry = "select * from Mas_Multi_Unit_Entry where Division_Code='" + divcode + "' and Folder_Act_flag=0";
            try
            {
                dsProBrd = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProBrd;
        }

        public DataSet getProdGrpName(string divcode, string SF, string FDt, string TDt, string subdiv,string statecode="0")
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProBrd = null;

            strQry = "exec getOrderedProdGroup '" + SF + "'," + divcode + ",'" + FDt + "','" + TDt + "','" + subdiv + "',"+statecode+"";
            try
            {
                dsProBrd = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProBrd;
        }
		

        public DataSet get_SNJ_Product_Group(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProBrd = null;

            strQry = "exec get_SNJ_Product_Group '" + divcode + "'";
            try
            {
                dsProBrd = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProBrd;
        }

        public DataSet get_SNJ_Brandwise_Sales(string divcode, string SF, string FDt, string TDt, string custcode="0")
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProBrd = null;

            strQry = "exec get_SNJ_Brandwise_Sales '" + SF + "','" + divcode + "','" + FDt + "','" + TDt + "'," + custcode + "";
            try
            {
                dsProBrd = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProBrd;
        }
        public DataSet get_SNJ_Brandwise_Sales_Cust(string divcode, string SF, string FDt, string TDt)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProBrd = null;

            strQry = "exec get_SNJ_Brandwise_Sales_Cust '" + SF + "','" + divcode + "','" + FDt + "','" + TDt + "'";
            try
            {
                dsProBrd = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProBrd;
        }
        public DataSet get_SNJ_Brandwise_Sales_UOM(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProBrd = null;

            strQry = "select distinct Product_Cat_Code,UOM_Weight from Mas_Product_Detail where Division_Code='"+ divcode + "' and Product_Active_Flag=0 order by 2 desc";
            try
            {
                dsProBrd = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProBrd;
        }
        public DataSet getBrandwise_Sales(string divcode, string sfcode, string subdiv, string FDT, string TDT, string BrandCode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProBrd = null;

            strQry = "exec getBrandwise_Sales '" + sfcode + "','" + divcode + "','" + FDT + "','" + TDT + "','" + BrandCode + "','" + subdiv + "'";
            try
            {
                dsProBrd = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProBrd;
        }
        public DataSet getpobdata(string div_code, string subdiv, string month, string year, string statecode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocCat = null;


            strQry = " EXEC mothly_pob_report '" + div_code + "', '" + subdiv + "', '" + month + "', '" + year + "', '" + statecode + "'  ";

            try
            {
                dsDocCat = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsDocCat;
        }
 public DataSet dissale(string div_code, string fromdate, string todate, string stcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocCat = null;


            strQry = " EXEC brandsales '" + div_code + "', '" + fromdate + "', '" + todate + "', '" + stcode + "'";

            try
            {
                dsDocCat = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsDocCat;
        }
		public DataSet dissaledr(string div_code, string fromdate, string todate, string stcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocCat = null;


            strQry = " EXEC brandsalesdr '" + div_code + "', '" + fromdate + "', '" + todate + "', '" + stcode + "'";

            try
            {
                dsDocCat = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsDocCat;
        }
        public DataSet get_Target_QntyFF_Primary(string div_code, string sfcode, string year)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsState = null;
            strQry = "exec get_Target_Qnty_FF_Primary '" + div_code + "','" + year + "','" + sfcode + "'";

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
        public DataSet getAllBrd_Qty(string divcode, string sfcode, string FDT, string TDT, string subdiv = "0", string StateCode = "0")
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsState = null;
            strQry = "exec getBrdUsers_Qty '" + sfcode + "','" + divcode + "','" + FDT + "','" + TDT + "','" + subdiv + "','" + StateCode + "'";

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
        public DataSet getAllBrd_USR(string divcode, string sfcode, string FDT, string TDT, string subdiv = "0", string StateCode = "0")
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsState = null;
            strQry = "exec getBrdUsers  '" + sfcode + "','" + divcode + "','" + FDT + "','" + TDT + "','" + subdiv + "','" + StateCode + "'";

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

        public DataSet dcrgetProductBrandlist_DataTable(string divcode, string sfcode, string FDT, string TDT, string subdiv = "0", string StateCode = "0")
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsState = null;
            strQry = "exec getSecAllDCR_PRODUCT  '" + sfcode + "','" + divcode + "','" + FDT + "','" + TDT + "','" + subdiv + "','" + StateCode + "'";

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
		 public DataSet dcrgetAllBrd_Qty(string divcode, string sfcode, string FDT, string TDT, string subdiv = "0", string StateCode = "0")
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsState = null;
            strQry = "exec getSecAllDCRdtl_date '" + sfcode + "','" + divcode + "','" + FDT + "','" + TDT + "','" + subdiv + "','" + StateCode + "'";

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
		 public DataSet dcrgetAllBrd_USR(string divcode, string sfcode, string FDT, string TDT, string subdiv = "0", string StateCode = "0")
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsState = null;
            strQry = "exec getSecAllDCRdtl_odate  '" + sfcode + "','" + divcode + "','" + FDT + "','" + TDT + "','" + subdiv + "','" + StateCode + "'";

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
		public DataSet pridcrgetAllBrd_Qty(string divcode, string sfcode, string FDT, string TDT, string subdiv = "0", string StateCode = "0")
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsState = null;
            strQry = "exec getprimAllDCRdtl_date '" + sfcode + "','" + divcode + "','" + FDT + "','" + TDT + "','" + subdiv + "','" + StateCode + "'";

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
		public DataSet pridcrgetAllBrd_USR(string divcode, string sfcode, string FDT, string TDT, string subdiv = "0", string StateCode = "0")
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsState = null;
            strQry = "exec getpriAllDCRdtl_odate  '" + sfcode + "','" + divcode + "','" + FDT + "','" + TDT + "','" + subdiv + "','" + StateCode + "'";

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
		public DataSet primdcrgetProductBrandlist_DataTable(string divcode, string sfcode, string FDT, string TDT, string subdiv = "0", string StateCode = "0")
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsState = null;
            strQry = "exec getprimAllDCR_PRODUCT  '" + sfcode + "','" + divcode + "','" + FDT + "','" + TDT + "','" + subdiv + "','" + StateCode + "'";

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
		  public DataSet dissaledr_visdate(string div_code, string fromdate, string todate, string stcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocCat = null;


            strQry = " EXEC brandsalesdr_vdate '" + div_code + "', '" + fromdate + "', '" + todate + "', '" + stcode + "'";

            try
            {
                dsDocCat = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsDocCat;
        }
		
     public DataSet validateSchemeByDate(string dt2, string State_Code, string div_code, string InsertType, string tdate, string schemename)
        {

            DB_EReporting db_ER = new DB_EReporting();
            DataSet ds = null;

            try
            {
                strQry = "exec sp_schem_validate '" + dt2 + "','" + tdate + "','" + State_Code + "','" + InsertType + "','" + div_code + "','" + schemename + "'";
                ds = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return ds;

        }
        public DataSet getroute_dtls(string sfcode, string divcode, string subdiv)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocCat = null;


            strQry = " EXEC get_Route_Name_Excel '" + sfcode + "','" + divcode + "', '" + subdiv + "'";

            try
            {
                dsDocCat = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsDocCat;
        }
        public DataSet getsale_dtls(string subdivc, string yr, string sfcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocCat = null;


            strQry = " EXEC new_Get_Retailer_sal_Excel '" + subdivc + "', '" + yr + "','" + sfcode + "'";

            try
            {
                dsDocCat = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsDocCat;
        }
        public DataSet getDaywiseDCROrders(string sfcode, string DivCode, string fdt, string tdt, string subdiv = "0")
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "exec getDaywiseDCROrders '" + sfcode + "','" + DivCode.TrimEnd(',') + "','" + fdt + "','" + tdt + "','" + subdiv + "'";

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
        public DataSet getDCRUsers(string sfcode, string DivCode, string fdt, string tdt, string subdiv = "0")
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "exec getSFUsers '" + sfcode + "'," + DivCode.TrimEnd(',') + ",'" + fdt + "','" + tdt + "','" + subdiv + "'";

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
        public DataSet getDaywiseDCRDets(string sfcode, string DivCode, string fdt, string tdt, string subdiv = "0")
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "exec getDaywiseDCR '" + sfcode + "','" + DivCode.TrimEnd(',') + "','" + fdt + "','" + tdt + "','" + subdiv + "'";

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
		
		public int Insert_free_Details(string Pro_Code, string Pro_Name, string free, string freeUnit,string batchno, string entry_Date, string entry_by)
        {
            int iReturn = -1;
            int iSlNo = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT ISNULL(MAX(sl_No),0)+1 FROM Mas_free_Stock";
                iSlNo = db.Exec_Scalar(strQry);

                //strQry = " INSERT INTO Mas_free_Stock (sl_No,Prod_Code,Prod_Name,freeQty,freeUnit,Batch_No,EntryDate,EntryBy) " +
                //         " VALUES ('" + iSlNo + "','" + Pro_Code + "','" + Pro_Name + "','" + free + "','" + freeUnit + "','" + batchno + "','" + entry_Date + "','" + entry_by + "')";

                strQry = " INSERT INTO Mas_free_Stock (Prod_Code,Prod_Name,freeQty,freeUnit,Batch_No,EntryDate,EntryBy) " +
                         " VALUES ('" + Pro_Code + "','" + Pro_Name + "','" + free + "','" + freeUnit + "','" + batchno + "','" + entry_Date + "','" + entry_by + "')";

                iReturn = db.ExecQry(strQry);
                iReturn = iSlNo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }
        public DataSet spCustomerOrders(string sfcode, string divcode, string subdiv, string years)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocCat = null;


            strQry = " EXEC getCuswiseYrOrder '" + sfcode + "','"+ divcode + "', '" + subdiv + "','"+ years + "'";

            try
            {
                dsDocCat = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsDocCat;
        }
        public DataSet spCustomerDetails(string sfcode, string divcode, string subdiv, string years)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocCat = null;


            strQry = " EXEC getCusDetails '" + sfcode + "','" + divcode + "', '" + subdiv + "','" + years + "'";

            try
            {
                dsDocCat = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsDocCat;
        }
        public DataSet spCustomerVisitDetails(string sfcode, string divcode, string subdiv, string years)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocCat = null;


            strQry = " EXEC getCuswiseVisitDets '" + sfcode + "','" + divcode + "', '" + subdiv + "','" + years + "'";

            try
            {
                dsDocCat = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsDocCat;
        }
		public DataSet getProductBrands(string divcode, string subdivcode)
        {
            DB_EReporting dbe_ER = new DB_EReporting();

            DataSet dsAdmin = null;
            strQry = "SELECT Product_Brd_Code,Product_Brd_Name FROM  Mas_Product_Brand WHERE Product_Brd_Active_Flag=0 and Division_Code='" + divcode + "'";

            try
            {
                dsAdmin = dbe_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsAdmin;
        }
    }
}
