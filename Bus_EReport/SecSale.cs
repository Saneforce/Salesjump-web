using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DBase_EReport;
using System.Data.SqlClient;

namespace Bus_EReport
{
    public class SecSale
    {
        #region "Variable Declarations"
            private string strQry = string.Empty;
            SqlCommand comm;
            SqlCommand sCommand;
            string sError = string.Empty;
            int iErrReturn = -1;
        #endregion

        public DataSet getSaleMaster()
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSale = null;
            strQry = "SELECT  Sec_Sale_Code, " +
                            " Sec_Sale_Name, " +
                            " Sec_Sale_Short_Name, " +
                            " Sec_Sale_Sub_Name, " +
                            " Sel_Sale_Operator " +
                     " FROM Mas_Sec_Sale_Param " +
                     " ORDER BY 1";
            try
            {
                dsSale = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(0, ex.Message.ToString().Trim(), "Sec Sales Setup", "getSaleMaster()");
            }
            return dsSale;
        }


        public DataSet getSaleMaster(string opr, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSale = null;
            strQry = "SELECT  Sec_Sale_Code, " +
                            " Sec_Sale_Name, " +
                            " Sec_Sale_Short_Name, " +
                            " Sec_Sale_Sub_Name, " +
                            " Sel_Sale_Operator " +
                     " FROM Mas_Sec_Sale_Param " +
                     " WHERE Sel_Sale_Operator = '" + opr + "' " + 
                     " AND Division_Code = '" + div_code + "' " +
                     " ORDER BY 1";
            try
            {
                dsSale = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(0, ex.Message.ToString().Trim(), "Sec Sales Setup", "getSaleMaster()");
            }
            return dsSale;
        }

        public DataSet getSaleMaster(string div_code, bool bIncludeEmptyRow)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSale = null;

            if (bIncludeEmptyRow)
            {
                strQry = " SELECT  -1 AS Sec_Sale_Code, '' Sec_Sale_Name, ''  Sec_Sale_Short_Name, '' Sec_Sale_Sub_Name, '' Sel_Sale_Operator " +
                         " UNION ALL " +
                         " SELECT  Sec_Sale_Code, " +
                                " Sec_Sale_Name, " +
                                " Sec_Sale_Short_Name, " +
                                " Sec_Sale_Sub_Name, " +
                                " Sel_Sale_Operator " +
                         " FROM Mas_Sec_Sale_Param " +
                         " WHERE Division_Code = '" + div_code + "' " +
                         " AND Sec_Sale_Sub_Name != 'Tot+' " +
                         " AND Sec_Sale_Sub_Name != 'cust_col' " +
                         " ORDER BY 1";
            }
            try
            {
                dsSale = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(0, ex.Message.ToString().Trim(), "Sec Sales Setup", "getSaleMaster()");
            }
            return dsSale;
        }

        public DataSet getSaleMaster(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSale = null;
            strQry = "SELECT  Sec_Sale_Code, " +
                            " Sec_Sale_Name, " +
                            " Sec_Sale_Short_Name, " +
                            " Sec_Sale_Sub_Name, " +
                            " Sel_Sale_Operator " +
                     " FROM Mas_Sec_Sale_Param " +
                     " WHERE Division_Code = '" + div_code + "' " +
                     " AND Sec_Sale_Sub_Name != 'Tot+' " + 
                     " ORDER BY 1";
            try
            {
                dsSale = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(0, ex.Message.ToString().Trim(), "Sec Sales Setup", "getSaleMaster()");
            }
            return dsSale;
        }

        public DataSet getSaleMaster(bool includeTotal, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSale = null;
            sCommand = new SqlCommand();

            SqlParameter par_div_code = new SqlParameter();
            par_div_code.ParameterName = "@Par_Division_Code";    // Defining Name
            par_div_code.SqlDbType = SqlDbType.Int;           // Defining DataType
            par_div_code.Direction = ParameterDirection.Input;// Setting the direction 

            // Adding Parameter instances to sqlcommand
            sCommand.Parameters.Add(par_div_code);

            // Setting values of Parameter
            par_div_code.Value = Convert.ToInt16(div_code);

            if (includeTotal)
            {
                ////strQry = "SELECT mssp.Sec_Sale_Code, mssp.Sec_Sale_Name, " +
                ////        " mssp.Sec_Sale_Short_Name, mssp.Sec_Sale_Sub_Name, mssp.Sel_Sale_Operator, " +
                ////        " msss.value_needed, msss.calc_needed  " +
                ////        " FROM Mas_Sec_Sale_Param mssp,  Mas_Sec_Sale_Setup msss" +
                ////        " WHERE mssp.Sel_Sale_Operator = '+' " +
                ////        " AND mssp.Sec_Sale_Code = msss.Sec_Sale_Code " +
                ////        " AND msss.Division_Code = @Par_Division_Code " +
                ////        " AND msss.Display_Needed = 1 " + 
                ////        " UNION " +
                ////        " SELECT  3.1 AS Sec_Sale_Code, 'Total (+)' AS Sec_Sale_Name, " +
                ////        " 'Total' AS Sec_Sale_Short_Name, 'Total' AS Sec_Sale_Sub_Name,  " +
                ////        " '+' AS Sel_Sale_Operator , '0' AS value_needed, '0' AS calc_needed  " +
                ////        " FROM Mas_Sec_Sale_Param " +
                ////        " UNION " +
                ////        " SELECT  mssp.Sec_Sale_Code, mssp.Sec_Sale_Name, " +
                ////        " mssp.Sec_Sale_Short_Name, mssp.Sec_Sale_Sub_Name, mssp.Sel_Sale_Operator, " +
                ////        " msss.value_needed, msss.calc_needed  " +
                ////        " FROM Mas_Sec_Sale_Param mssp,  Mas_Sec_Sale_Setup msss" +
                ////        " WHERE Sel_Sale_Operator = '-' " +
                ////        " AND mssp.Sec_Sale_Code = msss.Sec_Sale_Code " +
                ////        " AND msss.Division_Code = @Par_Division_Code " +
                ////        " AND msss.Display_Needed = 1 " + 
                ////        " UNION " +
                ////        " SELECT  9.1 AS Sec_Sale_Code, 'Total (-)' AS Sec_Sale_Name, " +
                ////        " 'Total' AS Sec_Sale_Short_Name, 'Total' AS Sec_Sale_Sub_Name, " +
                ////        " '-' AS Sel_Sale_Operator , '0' AS value_needed, '0' AS calc_needed " +
                ////        " FROM Mas_Sec_Sale_Param " +
                ////        " UNION " +
                ////        " SELECT  mssp.Sec_Sale_Code, mssp.Sec_Sale_Name, " +
                ////        " mssp.Sec_Sale_Short_Name, mssp.Sec_Sale_Sub_Name, mssp.Sel_Sale_Operator, " +
                ////        " msss.value_needed, msss.calc_needed  " +
                ////        " FROM Mas_Sec_Sale_Param mssp,  Mas_Sec_Sale_Setup msss" +
                ////        " WHERE Sel_Sale_Operator = 'C' " +
                ////        " AND mssp.Sec_Sale_Code = msss.Sec_Sale_Code " +
                ////        " AND msss.Division_Code = @Par_Division_Code " +
                ////        " AND msss.Display_Needed = 1 " + 
                ////        " ORDER BY 1";

                //strQry = "SELECT mssp.Sec_Sale_Code, mssp.Sec_Sale_Name, " +
                //        " mssp.Sec_Sale_Short_Name, mssp.Sec_Sale_Sub_Name, mssp.Sel_Sale_Operator, " +
                //        " msss.value_needed, msss.calc_needed, msss.Sub_Needed, msss.Sub_Label, msss.Order_by, msss.Carry_Fwd_Needed, msss.Carry_Fwd_Field " +
                //        " FROM Mas_Sec_Sale_Param mssp,  Mas_Sec_Sale_Setup msss" +
                //        " WHERE mssp.Sel_Sale_Operator = '+' " +
                //        " AND mssp.Sec_Sale_Code = msss.Sec_Sale_Code " +
                //        " AND msss.Division_Code = @Par_Division_Code " +
                //        " AND mssp.Division_Code = @Par_Division_Code " +
                //        " AND msss.Display_Needed = 1 " +
                //        " UNION " +
                //        " SELECT  3.1 AS Sec_Sale_Code, 'Total (+)' AS Sec_Sale_Name, " +
                //        " 'Total' AS Sec_Sale_Short_Name, 'Total' AS Sec_Sale_Sub_Name,  " +
                //        " '+' AS Sel_Sale_Operator , " +                        
                //        " (select value_needed from mas_common_sec_sale_setup  where Division_Code = @Par_Division_Code) AS value_needed, " +
                //        " '0' AS calc_needed, '' AS Sub_Needed, '' AS  Sub_Label, 3.1 AS  Order_by, 0 AS Carry_Fwd_Needed, 0 AS Carry_Fwd_Field" +
                //        " FROM Mas_Sec_Sale_Param " +
                //        " UNION " +
                //        " SELECT  mssp.Sec_Sale_Code, mssp.Sec_Sale_Name, " +
                //        " mssp.Sec_Sale_Short_Name, mssp.Sec_Sale_Sub_Name, mssp.Sel_Sale_Operator, " +
                //        " msss.value_needed, msss.calc_needed, msss.Sub_Needed, msss.Sub_Label, msss.Order_by, msss.Carry_Fwd_Needed, msss.Carry_Fwd_Field  " +
                //        " FROM Mas_Sec_Sale_Param mssp,  Mas_Sec_Sale_Setup msss" +
                //        " WHERE Sel_Sale_Operator = '-' " +
                //        " AND mssp.Sec_Sale_Code = msss.Sec_Sale_Code " +
                //        " AND msss.Division_Code = @Par_Division_Code " +
                //        " AND mssp.Division_Code = @Par_Division_Code " +
                //        " AND msss.Display_Needed = 1 " +
                //        " UNION " +
                //        " SELECT  mssp.Sec_Sale_Code, mssp.Sec_Sale_Name, " +
                //        " mssp.Sec_Sale_Short_Name, mssp.Sec_Sale_Sub_Name, mssp.Sel_Sale_Operator, " +
                //        " msss.value_needed, msss.calc_needed, msss.Sub_Needed, msss.Sub_Label, msss.Order_by, msss.Carry_Fwd_Needed, msss.Carry_Fwd_Field  " +
                //        " FROM Mas_Sec_Sale_Param mssp,  Mas_Sec_Sale_Setup msss" +
                //        " WHERE Sel_Sale_Operator = 'C' " +
                //        " AND mssp.Sec_Sale_Code = msss.Sec_Sale_Code " +
                //        " AND msss.Division_Code = @Par_Division_Code " +
                //        " AND mssp.Division_Code = @Par_Division_Code " +
                //        " AND msss.Display_Needed = 1 " +
                //        " ORDER BY Sel_Sale_Operator, msss.Order_by";

                strQry = "SELECT mssp.Sec_Sale_Code, mssp.Sec_Sale_Name, " +
                        " mssp.Sec_Sale_Short_Name, mssp.Sec_Sale_Sub_Name, mssp.Sel_Sale_Operator, " +
                        " msss.value_needed, msss.calc_needed, msss.Sub_Needed, msss.Sub_Label, msss.Order_by, msss.Carry_Fwd_Needed, msss.Carry_Fwd_Field, '' Der_Formula " +
                        " FROM Mas_Sec_Sale_Param mssp,  Mas_Sec_Sale_Setup msss" +
                        " WHERE mssp.Sel_Sale_Operator = '+' " +
                        " AND mssp.Sec_Sale_Code = msss.Sec_Sale_Code " +
                        " AND msss.Division_Code = @Par_Division_Code " +
                        " AND mssp.Division_Code = @Par_Division_Code " +
                        " AND msss.Display_Needed = 1 " +
                        " UNION " +
                        " SELECT  3.1 AS Sec_Sale_Code, 'Total (+)' AS Sec_Sale_Name, " +
                        " 'Total' AS Sec_Sale_Short_Name, 'Total' AS Sec_Sale_Sub_Name,  " +
                        " '+' AS Sel_Sale_Operator , " +
                        " (select value_needed from mas_common_sec_sale_setup  where Division_Code = @Par_Division_Code) AS value_needed, " +
                        " '0' AS calc_needed, '' AS Sub_Needed, '' AS  Sub_Label, 3.1 AS  Order_by, 0 AS Carry_Fwd_Needed, 0 AS Carry_Fwd_Field, '' Der_Formula" +
                        " FROM Mas_Sec_Sale_Param " +
                        " UNION " +
                        " SELECT  mssp.Sec_Sale_Code, mssp.Sec_Sale_Name, " +
                        " mssp.Sec_Sale_Short_Name, mssp.Sec_Sale_Sub_Name, mssp.Sel_Sale_Operator, " +
                        " msss.value_needed, msss.calc_needed, msss.Sub_Needed, msss.Sub_Label, msss.Order_by, msss.Carry_Fwd_Needed, msss.Carry_Fwd_Field, '' Der_Formula  " +
                        " FROM Mas_Sec_Sale_Param mssp,  Mas_Sec_Sale_Setup msss" +
                        " WHERE Sel_Sale_Operator = '-' " +
                        " AND mssp.Sec_Sale_Code = msss.Sec_Sale_Code " +
                        " AND msss.Division_Code = @Par_Division_Code " +
                        " AND mssp.Division_Code = @Par_Division_Code " +
                        " AND msss.Display_Needed = 1 " +
                        " UNION " +
                        " SELECT  mssp.Sec_Sale_Code, mssp.Sec_Sale_Name, " +
                        " mssp.Sec_Sale_Short_Name, mssp.Sec_Sale_Sub_Name, mssp.Sel_Sale_Operator, " +
                        " msss.value_needed, msss.calc_needed, msss.Sub_Needed, msss.Sub_Label, msss.Order_by, msss.Carry_Fwd_Needed, msss.Carry_Fwd_Field, '' Der_Formula  " +
                        " FROM Mas_Sec_Sale_Param mssp,  Mas_Sec_Sale_Setup msss" +
                        " WHERE Sel_Sale_Operator = 'C' " +
                        " AND mssp.Sec_Sale_Code = msss.Sec_Sale_Code " +
                        " AND msss.Division_Code = @Par_Division_Code " +
                        " AND mssp.Division_Code = @Par_Division_Code " +
                        " AND msss.Display_Needed = 1 " +
                        " UNION " +
                        " SELECT  mssp.Sec_Sale_Code, mssp.Sec_Sale_Name, " +
                        " mssp.Sec_Sale_Short_Name, mssp.Sec_Sale_Sub_Name, mssp.Sel_Sale_Operator, " +
                        " msss.value_needed, msss.calc_needed, msss.Sub_Needed, msss.Sub_Label, msss.Order_by, msss.Carry_Fwd_Needed, msss.Carry_Fwd_Field, mcf.Der_Formula  " +
                        " FROM Mas_Sec_Sale_Setup msss,  Mas_Sec_Sale_Param mssp " +
                        " left outer join Mas_Common_SS_Setup_Formula mcf " +
                        " mssp.Cust_Col_SNo = mcf.Col_SNo " +
                        " WHERE Sel_Sale_Operator = 'D' " +
                        " AND mssp.Sec_Sale_Code = msss.Sec_Sale_Code " +
                        " AND msss.Division_Code = @Par_Division_Code " +
                        " AND mssp.Division_Code = @Par_Division_Code " +
                        " AND msss.Display_Needed = 1 " +
                        " ORDER BY msss.Order_by";
            }
            else
            {
                strQry = "SELECT mssp.Sec_Sale_Code, mssp.Sec_Sale_Name, " +
                        " mssp.Sec_Sale_Short_Name, mssp.Sec_Sale_Sub_Name, mssp.Sel_Sale_Operator, " +
                        " msss.value_needed, msss.calc_needed, msss.Sub_Needed, msss.Sub_Label, msss.Order_by, msss.Carry_Fwd_Needed, msss.Carry_Fwd_Field, '' Der_Formula    " +
                        " FROM Mas_Sec_Sale_Param mssp,  Mas_Sec_Sale_Setup msss" +
                        " WHERE mssp.Sel_Sale_Operator = '+' " +
                        " AND mssp.Sec_Sale_Code = msss.Sec_Sale_Code " +
                        " AND msss.Division_Code = @Par_Division_Code " +
                        " AND mssp.Division_Code = @Par_Division_Code " +
                        " AND msss.Display_Needed = 1 " +
                        " UNION " +
                        " SELECT  mssp.Sec_Sale_Code, mssp.Sec_Sale_Name, " +
                        " mssp.Sec_Sale_Short_Name, mssp.Sec_Sale_Sub_Name, mssp.Sel_Sale_Operator, " +
                        " msss.value_needed, msss.calc_needed, msss.Sub_Needed, msss.Sub_Label, msss.Order_by, msss.Carry_Fwd_Needed, msss.Carry_Fwd_Field, '' Der_Formula     " +
                        " FROM Mas_Sec_Sale_Param mssp,  Mas_Sec_Sale_Setup msss" +
                        " WHERE Sel_Sale_Operator = '-' " +
                        " AND mssp.Sec_Sale_Code = msss.Sec_Sale_Code " +
                        " AND msss.Division_Code = @Par_Division_Code " +
                        " AND mssp.Division_Code = @Par_Division_Code " +
                        " AND msss.Display_Needed = 1 " +
                        " UNION " +
                        " SELECT  mssp.Sec_Sale_Code, mssp.Sec_Sale_Name, " +
                        " mssp.Sec_Sale_Short_Name, mssp.Sec_Sale_Sub_Name, mssp.Sel_Sale_Operator, " +
                        " msss.value_needed, msss.calc_needed, msss.Sub_Needed, msss.Sub_Label, msss.Order_by, msss.Carry_Fwd_Needed, msss.Carry_Fwd_Field, '' Der_Formula     " +
                        " FROM Mas_Sec_Sale_Param mssp,  Mas_Sec_Sale_Setup msss" +
                        " WHERE Sel_Sale_Operator = 'C' " +
                        " AND mssp.Sec_Sale_Code = msss.Sec_Sale_Code " +
                        " AND msss.Division_Code = @Par_Division_Code " +
                        " AND mssp.Division_Code = @Par_Division_Code " +
                        " AND msss.Display_Needed = 1 " +
                        " UNION " +
                        " SELECT  mssp.Sec_Sale_Code, mssp.Sec_Sale_Name, " +
                        " mssp.Sec_Sale_Short_Name, mssp.Sec_Sale_Sub_Name, mssp.Sel_Sale_Operator, " +
                        " msss.value_needed, msss.calc_needed, msss.Sub_Needed, msss.Sub_Label, msss.Order_by, msss.Carry_Fwd_Needed, msss.Carry_Fwd_Field, mcf.Der_Formula   " +
                        " FROM Mas_Sec_Sale_Setup msss, Mas_Sec_Sale_Param mssp " +
                        " left outer join Mas_Common_SS_Setup_Formula mcf " +
                        " on mssp.Cust_Col_SNo = mcf.Col_SNo " +
                        " WHERE Sel_Sale_Operator = 'D' " +
                        " AND mssp.Sec_Sale_Code = msss.Sec_Sale_Code " +
                        " AND msss.Division_Code = @Par_Division_Code " +
                        " AND mssp.Division_Code = @Par_Division_Code " +
                        " AND msss.Display_Needed = 1 " +
                        " ORDER BY msss.Order_by";

            }
            try
            {
                dsSale = db_ER.Exec_DataSet(strQry, sCommand);
            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(0, ex.Message.ToString().Trim(), "Sec Sales Setup", "getSaleMaster()");
            }
            
            return dsSale;
        }

        public DataSet getSaleMaster_ValueNeeded(bool includeTotal, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSale = null;
            sCommand = new SqlCommand();

            SqlParameter par_div_code = new SqlParameter();
            par_div_code.ParameterName = "@Par_Division_Code";    // Defining Name
            par_div_code.SqlDbType = SqlDbType.Int;           // Defining DataType
            par_div_code.Direction = ParameterDirection.Input;// Setting the direction 

            // Adding Parameter instances to sqlcommand
            sCommand.Parameters.Add(par_div_code);

            // Setting values of Parameter
            par_div_code.Value = Convert.ToInt16(div_code);

            if (includeTotal)
            {
                strQry = "SELECT mssp.Sec_Sale_Code, mssp.Sec_Sale_Name, " +
                        " mssp.Sec_Sale_Short_Name, mssp.Sec_Sale_Sub_Name, mssp.Sel_Sale_Operator, " +
                        " msss.value_needed, msss.calc_needed, msss.Sub_Needed, msss.Sub_Label, msss.Order_by " +
                        " FROM Mas_Sec_Sale_Param mssp,  Mas_Sec_Sale_Setup msss" +
                        " WHERE mssp.Sel_Sale_Operator = '+' " +
                        " AND mssp.Sec_Sale_Code = msss.Sec_Sale_Code " +
                        " AND msss.Division_Code = @Par_Division_Code " +
                        " AND mssp.Division_Code = @Par_Division_Code " +
                        " AND msss.Display_Needed = 1 " +
                        " AND msss.Value_Needed = 1 " +
                        " UNION " +
                        " SELECT  3.1 AS Sec_Sale_Code, 'Total (+)' AS Sec_Sale_Name, " +
                        " 'Total' AS Sec_Sale_Short_Name, 'Total' AS Sec_Sale_Sub_Name,  " +
                        " '+' AS Sel_Sale_Operator , " +
                        " (select value_needed from mas_common_sec_sale_setup  where Division_Code=@Par_Division_Code and value_needed = 1) AS value_needed, " +
                        " '0' AS calc_needed, '' AS Sub_Needed, '' AS  Sub_Label, 3.1 AS  Order_by" +
                        " FROM Mas_Sec_Sale_Param " +
                        " UNION " +
                        " SELECT  mssp.Sec_Sale_Code, mssp.Sec_Sale_Name, " +
                        " mssp.Sec_Sale_Short_Name, mssp.Sec_Sale_Sub_Name, mssp.Sel_Sale_Operator, " +
                        " msss.value_needed, msss.calc_needed, msss.Sub_Needed, msss.Sub_Label, msss.Order_by  " +
                        " FROM Mas_Sec_Sale_Param mssp,  Mas_Sec_Sale_Setup msss" +
                        " WHERE Sel_Sale_Operator = '-' " +
                        " AND mssp.Sec_Sale_Code = msss.Sec_Sale_Code " +
                        " AND msss.Division_Code = @Par_Division_Code " +
                        " AND mssp.Division_Code = @Par_Division_Code " +
                        " AND msss.Display_Needed = 1 " +
                        " AND msss.Value_Needed = 1 " +
                        " UNION " +
                        " SELECT  mssp.Sec_Sale_Code, mssp.Sec_Sale_Name, " +
                        " mssp.Sec_Sale_Short_Name, mssp.Sec_Sale_Sub_Name, mssp.Sel_Sale_Operator, " +
                        " msss.value_needed, msss.calc_needed, msss.Sub_Needed, msss.Sub_Label, msss.Order_by  " +
                        " FROM Mas_Sec_Sale_Param mssp,  Mas_Sec_Sale_Setup msss" +
                        " WHERE Sel_Sale_Operator = 'C' " +
                        " AND mssp.Sec_Sale_Code = msss.Sec_Sale_Code " +
                        " AND msss.Division_Code = @Par_Division_Code " +
                        " AND mssp.Division_Code = @Par_Division_Code " +
                        " AND msss.Display_Needed = 1 " +
                        " AND msss.Value_Needed = 1 " +
                        " ORDER BY Sel_Sale_Operator, msss.Order_by";
            }
            else
            {
                strQry = "SELECT mssp.Sec_Sale_Code, mssp.Sec_Sale_Name, " +
                        " mssp.Sec_Sale_Short_Name, mssp.Sec_Sale_Sub_Name, mssp.Sel_Sale_Operator, " +
                        " msss.value_needed, msss.calc_needed, msss.Sub_Needed, msss.Sub_Label, msss.Order_by " +
                        " FROM Mas_Sec_Sale_Param mssp,  Mas_Sec_Sale_Setup msss" +
                        " WHERE mssp.Sel_Sale_Operator = '+' " +
                        " AND mssp.Sec_Sale_Code = msss.Sec_Sale_Code " +
                        " AND msss.Division_Code = @Par_Division_Code " +
                        " AND mssp.Division_Code = @Par_Division_Code " +
                        " AND msss.Display_Needed = 1 " +
                        " AND msss.Value_Needed = 1 " +
                        " UNION " +
                        " SELECT  mssp.Sec_Sale_Code, mssp.Sec_Sale_Name, " +
                        " mssp.Sec_Sale_Short_Name, mssp.Sec_Sale_Sub_Name, mssp.Sel_Sale_Operator, " +
                        " msss.value_needed, msss.calc_needed, msss.Sub_Needed, msss.Sub_Label, msss.Order_by  " +
                        " FROM Mas_Sec_Sale_Param mssp,  Mas_Sec_Sale_Setup msss" +
                        " WHERE Sel_Sale_Operator = '-' " +
                        " AND mssp.Sec_Sale_Code = msss.Sec_Sale_Code " +
                        " AND msss.Division_Code = @Par_Division_Code " +
                        " AND mssp.Division_Code = @Par_Division_Code " +
                        " AND msss.Display_Needed = 1 " +
                        " AND msss.Value_Needed = 1 " +
                        " UNION " +
                        " SELECT  mssp.Sec_Sale_Code, mssp.Sec_Sale_Name, " +
                        " mssp.Sec_Sale_Short_Name, mssp.Sec_Sale_Sub_Name, mssp.Sel_Sale_Operator, " +
                        " msss.value_needed, msss.calc_needed, msss.Sub_Needed, msss.Sub_Label, msss.Order_by  " +
                        " FROM Mas_Sec_Sale_Param mssp,  Mas_Sec_Sale_Setup msss" +
                        " WHERE Sel_Sale_Operator = 'C' " +
                        " AND mssp.Sec_Sale_Code = msss.Sec_Sale_Code " +
                        " AND msss.Division_Code = @Par_Division_Code " +
                        " AND mssp.Division_Code = @Par_Division_Code " +
                        " AND msss.Display_Needed = 1 " +
                        " AND msss.Value_Needed = 1 " +
                        " ORDER BY Sel_Sale_Operator, msss.Order_by";
            }

            try
            {
                dsSale = db_ER.Exec_DataSet(strQry, sCommand);
            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(0, ex.Message.ToString().Trim(), "Sec Sales Setup", "getSaleMaster()");
            }
            
            return dsSale;
        }

        public DataSet getSaleEnteredQty(string div_code, string sf_code, int cmonth, int cyear, int stock_code, string prod_code, string sec_sale_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSale = null;
            sCommand = new SqlCommand();

            SqlParameter par_div_code = new SqlParameter();
            par_div_code.ParameterName = "@Par_Division_Code";    // Defining Name
            par_div_code.SqlDbType = SqlDbType.Int;           // Defining DataType
            par_div_code.Direction = ParameterDirection.Input;// Setting the direction 

            SqlParameter param_sfcode = new SqlParameter();
            param_sfcode.ParameterName = "@Par_SF_Code";    // Defining Name
            param_sfcode.SqlDbType = SqlDbType.VarChar;           // Defining DataType
            param_sfcode.Direction = ParameterDirection.Input;// Setting the direction 

            SqlParameter par_month = new SqlParameter();
            par_month.ParameterName = "@Par_Month";    // Defining Name
            par_month.SqlDbType = SqlDbType.Int;           // Defining DataType
            par_month.Direction = ParameterDirection.Input;// Setting the direction 

            SqlParameter par_year = new SqlParameter();
            par_year.ParameterName = "@Par_Year";    // Defining Name
            par_year.SqlDbType = SqlDbType.Int;           // Defining DataType
            par_year.Direction = ParameterDirection.Input;// Setting the direction 

            SqlParameter par_stock_code = new SqlParameter();
            par_stock_code.ParameterName = "@Par_Stock_Code";    // Defining Name
            par_stock_code.SqlDbType = SqlDbType.Int;           // Defining DataType
            par_stock_code.Direction = ParameterDirection.Input;// Setting the direction 

            SqlParameter par_prod_code = new SqlParameter();
            par_prod_code.ParameterName = "@Par_Prod_Code";    // Defining Name
            par_prod_code.SqlDbType = SqlDbType.VarChar;           // Defining DataType
            par_prod_code.Direction = ParameterDirection.Input;// Setting the direction 

            SqlParameter param_secsale_code = new SqlParameter();
            param_secsale_code.ParameterName = "@Sec_Sale_Code";    // Defining Name
            param_secsale_code.SqlDbType = SqlDbType.VarChar;           // Defining DataType
            param_secsale_code.Direction = ParameterDirection.Input;// Setting the direction 
                       
            // Adding Parameter instances to sqlcommand
            sCommand.Parameters.Add(par_div_code);
            sCommand.Parameters.Add(param_sfcode);
            sCommand.Parameters.Add(par_month);
            sCommand.Parameters.Add(par_year);
            sCommand.Parameters.Add(par_stock_code);
            sCommand.Parameters.Add(par_prod_code);
            sCommand.Parameters.Add(param_secsale_code);

            // Setting values of Parameter

            sec_sale_code = Convert.ToString((int)Convert.ToDouble(sec_sale_code));

            par_div_code.Value = Convert.ToInt16(div_code);
            param_sfcode.Value = sf_code;
            par_month.Value = cmonth;
            par_year.Value = cyear;
            par_stock_code.Value = stock_code;
            par_prod_code.Value = prod_code;
            param_secsale_code.Value = sec_sale_code;

            strQry = " SELECT tsedv.Sec_Sale_Qty, tsedv.Sec_Sale_Value, tsedv.Sec_Sale_Sub " +
                    " FROM Trans_SS_Entry_Head tseh, Trans_SS_Entry_Detail tsed, Trans_SS_Entry_Detail_Value tsedv " +
                    " WHERE tseh.SS_Head_Sl_No = tsed.SS_Head_Sl_No " +
                    " AND tsed.SS_Det_Sl_No = tsedv.SS_Det_Sl_No " +
                    " AND tseh.SF_Code = @Par_SF_Code " +  
                    " AND tseh.Division_Code  = @Par_Division_Code " +
                    " AND tseh.Month = @Par_Month " +
                    " AND tseh.Year = @Par_Year " +
                    " AND tseh.Stockiest_Code = @Par_Stock_Code " +
                    " AND tsed.Product_Detail_Code = @Par_Prod_Code " +
                    " AND tsedv.Sec_Sale_Code = @Sec_Sale_Code " +
                    " ORDER BY 1";

            try
            {
                dsSale = db_ER.Exec_DataSet(strQry, sCommand);
            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(0, ex.Message.ToString().Trim(), "Sec Sales Setup", "getSaleEnteredQty()");
            }

            return dsSale;
        }


        public DataSet getDtlSNo(string div_code, string sf_code, int cmonth, int cyear, int stock_code, string prod_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSale = null;
            sCommand = new SqlCommand();

            SqlParameter par_div_code = new SqlParameter();
            par_div_code.ParameterName = "@Par_Division_Code";    // Defining Name
            par_div_code.SqlDbType = SqlDbType.Int;           // Defining DataType
            par_div_code.Direction = ParameterDirection.Input;// Setting the direction 

            SqlParameter param_sfcode = new SqlParameter();
            param_sfcode.ParameterName = "@Par_SF_Code";    // Defining Name
            param_sfcode.SqlDbType = SqlDbType.VarChar;           // Defining DataType
            param_sfcode.Direction = ParameterDirection.Input;// Setting the direction 

            SqlParameter par_month = new SqlParameter();
            par_month.ParameterName = "@Par_Month";    // Defining Name
            par_month.SqlDbType = SqlDbType.Int;           // Defining DataType
            par_month.Direction = ParameterDirection.Input;// Setting the direction 

            SqlParameter par_year = new SqlParameter();
            par_year.ParameterName = "@Par_Year";    // Defining Name
            par_year.SqlDbType = SqlDbType.Int;           // Defining DataType
            par_year.Direction = ParameterDirection.Input;// Setting the direction 

            SqlParameter par_stock_code = new SqlParameter();
            par_stock_code.ParameterName = "@Par_Stock_Code";    // Defining Name
            par_stock_code.SqlDbType = SqlDbType.Int;           // Defining DataType
            par_stock_code.Direction = ParameterDirection.Input;// Setting the direction 

            SqlParameter par_prod_code = new SqlParameter();
            par_prod_code.ParameterName = "@Par_Prod_Code";    // Defining Name
            par_prod_code.SqlDbType = SqlDbType.VarChar;           // Defining DataType
            par_prod_code.Direction = ParameterDirection.Input;// Setting the direction 

            // Adding Parameter instances to sqlcommand
            sCommand.Parameters.Add(par_div_code);
            sCommand.Parameters.Add(param_sfcode);
            sCommand.Parameters.Add(par_month);
            sCommand.Parameters.Add(par_year);
            sCommand.Parameters.Add(par_stock_code);
            sCommand.Parameters.Add(par_prod_code);

            // Setting values of Parameter
            par_div_code.Value = Convert.ToInt16(div_code);
            param_sfcode.Value = sf_code;
            par_month.Value = cmonth;
            par_year.Value = cyear;
            par_stock_code.Value = stock_code;
            par_prod_code.Value = prod_code;

            strQry = " SELECT tsed.SS_Det_Sl_No " +
                    " FROM Trans_SS_Entry_Head tseh, Trans_SS_Entry_Detail tsed " +
                    " WHERE tseh.SS_Head_Sl_No = tsed.SS_Head_Sl_No " +
                    " AND tseh.SF_Code = @Par_SF_Code " +
                    " AND tseh.Division_Code  = @Par_Division_Code " +
                    " AND tseh.Month = @Par_Month " +
                    " AND tseh.Year = @Par_Year " +
                    " AND tseh.Stockiest_Code = @Par_Stock_Code " +
                    " AND tsed.Product_Detail_Code = @Par_Prod_Code ";

            try
            {
                dsSale = db_ER.Exec_DataSet(strQry, sCommand);
            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(0, ex.Message.ToString().Trim(), "Sec Sales Setup", "getSaleEnteredQty()");
            }

            return dsSale;
        }

        public int RecordAdd(int div_code, int Sec_Sale_Code, int Display_Needed, int Value_Needed, int Carry_Fwd_Needed, int Disable_Mode, int Calc_Needed, int Calc_Disable,
            int Sale_Calc, int Carry_Fwd_Field, int Order_by, bool bRecordExist, int sub_needed, string sub_label)
        {
            int iReturn = -1;

            ErrorLog err = new ErrorLog();
            try
            {
                DB_EReporting db = new DB_EReporting();
                comm = new SqlCommand();
                comm.Parameters.Clear();

                SqlParameter param_div_code = new SqlParameter();
                param_div_code.ParameterName = "@Division_Code";    // Defining Name
                param_div_code.SqlDbType = SqlDbType.Int;           // Defining DataType
                param_div_code.Direction = ParameterDirection.Input;// Setting the direction 

                SqlParameter param_Sec_Sale_Code = new SqlParameter();
                param_Sec_Sale_Code.ParameterName = "@Sec_Sale_Code";    // Defining Name
                param_Sec_Sale_Code.SqlDbType = SqlDbType.Int;           // Defining DataType
                param_Sec_Sale_Code.Direction = ParameterDirection.Input;// Setting the direction 

                SqlParameter param_Display_Needed = new SqlParameter();
                param_Display_Needed.ParameterName = "@Display_Needed";    // Defining Name
                param_Display_Needed.SqlDbType = SqlDbType.Int;           // Defining DataType
                param_Display_Needed.Direction = ParameterDirection.Input;// Setting the direction 

                SqlParameter param_Value_Needed = new SqlParameter();
                param_Value_Needed.ParameterName = "@Value_Needed";    // Defining Name
                param_Value_Needed.SqlDbType = SqlDbType.Int;           // Defining DataType
                param_Value_Needed.Direction = ParameterDirection.Input;// Setting the direction 

                SqlParameter param_Carry_Fwd_Needed = new SqlParameter();
                param_Carry_Fwd_Needed.ParameterName = "@Carry_Fwd_Needed";    // Defining Name
                param_Carry_Fwd_Needed.SqlDbType = SqlDbType.Int;           // Defining DataType
                param_Carry_Fwd_Needed.Direction = ParameterDirection.Input;// Setting the direction 

                SqlParameter param_Disable_Mode = new SqlParameter();
                param_Disable_Mode.ParameterName = "@Disable_Mode";    // Defining Name
                param_Disable_Mode.SqlDbType = SqlDbType.Int;           // Defining DataType
                param_Disable_Mode.Direction = ParameterDirection.Input;// Setting the direction 

                SqlParameter param_Calc_Needed = new SqlParameter();
                param_Calc_Needed.ParameterName = "@Calc_Needed";    // Defining Name
                param_Calc_Needed.SqlDbType = SqlDbType.Int;           // Defining DataType
                param_Calc_Needed.Direction = ParameterDirection.Input;// Setting the direction 

                SqlParameter param_Calc_Disable = new SqlParameter();
                param_Calc_Disable.ParameterName = "@Calc_Disable";    // Defining Name
                param_Calc_Disable.SqlDbType = SqlDbType.Int;           // Defining DataType
                param_Calc_Disable.Direction = ParameterDirection.Input;// Setting the direction 

                SqlParameter param_Sale_Calc = new SqlParameter();
                param_Sale_Calc.ParameterName = "@Sale_Calc";    // Defining Name
                param_Sale_Calc.SqlDbType = SqlDbType.Int;           // Defining DataType
                param_Sale_Calc.Direction = ParameterDirection.Input;// Setting the direction 

                SqlParameter param_Carry_Fwd_Field = new SqlParameter();
                param_Carry_Fwd_Field.ParameterName = "@Carry_Fwd_Field";    // Defining Name
                param_Carry_Fwd_Field.SqlDbType = SqlDbType.Int;           // Defining DataType
                param_Carry_Fwd_Field.Direction = ParameterDirection.Input;// Setting the direction 

                SqlParameter param_sub_needed = new SqlParameter();
                param_sub_needed.ParameterName = "@Sub_Needed";    // Defining Name
                param_sub_needed.SqlDbType = SqlDbType.Int;           // Defining DataType
                param_sub_needed.Direction = ParameterDirection.Input;// Setting the direction 

                SqlParameter param_sub_label = new SqlParameter();
                param_sub_label.ParameterName = "@Sub_Label";    // Defining Name
                param_sub_label.SqlDbType = SqlDbType.VarChar;           // Defining DataType
                param_sub_label.Direction = ParameterDirection.Input;// Setting the direction 

                SqlParameter param_Order_by = new SqlParameter();
                param_Order_by.ParameterName = "@Order_by";    // Defining Name
                param_Order_by.SqlDbType = SqlDbType.Int;           // Defining DataType
                param_Order_by.Direction = ParameterDirection.Input;// Setting the direction 

                SqlParameter param_Created_dt = new SqlParameter();
                param_Created_dt.ParameterName = "@Created_dt";    // Defining Name
                param_Created_dt.SqlDbType = SqlDbType.VarChar;           // Defining DataType
                param_Created_dt.Direction = ParameterDirection.Input;// Setting the direction 

                SqlParameter param_Updated_dt = new SqlParameter();
                param_Updated_dt.ParameterName = "@Updated_dt";    // Defining Name
                param_Updated_dt.SqlDbType = SqlDbType.VarChar;           // Defining DataType
                param_Updated_dt.Direction = ParameterDirection.Input;// Setting the direction 


                // Adding Parameter instances to sqlcommand

                comm.Parameters.Add(param_div_code);
                comm.Parameters.Add(param_Sec_Sale_Code);
                comm.Parameters.Add(param_Display_Needed);
                comm.Parameters.Add(param_Value_Needed);
                comm.Parameters.Add(param_Carry_Fwd_Needed);
                comm.Parameters.Add(param_Disable_Mode);
                comm.Parameters.Add(param_Calc_Needed);
                comm.Parameters.Add(param_Calc_Disable);
                comm.Parameters.Add(param_Sale_Calc);
                comm.Parameters.Add(param_Carry_Fwd_Field);
                comm.Parameters.Add(param_sub_needed);
                comm.Parameters.Add(param_sub_label);
                comm.Parameters.Add(param_Order_by);
                comm.Parameters.Add(param_Created_dt);
                comm.Parameters.Add(param_Updated_dt);

                // Setting values of Parameter
                param_div_code.Value = div_code;
                param_Sec_Sale_Code.Value = Sec_Sale_Code;
                param_Display_Needed.Value = Display_Needed;
                param_Value_Needed.Value = Value_Needed;
                param_Carry_Fwd_Needed.Value = Carry_Fwd_Needed;
                param_Disable_Mode.Value = Disable_Mode;
                param_Calc_Needed.Value = Calc_Needed;
                param_Calc_Disable.Value = Calc_Disable;
                param_Sale_Calc.Value = Sale_Calc;
                param_Carry_Fwd_Field.Value = Carry_Fwd_Field;
                param_sub_needed.Value = sub_needed;
                param_sub_label.Value = sub_label;
                param_Order_by.Value = Order_by;
                param_Created_dt.Value = DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss");
                param_Updated_dt.Value = DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss");

                if (!bRecordExist) //Creating Setup for the Division
                {
                    strQry = "INSERT INTO Mas_Sec_Sale_Setup (Division_Code,Sec_Sale_Code,Display_Needed,Value_Needed,Carry_Fwd_Needed,Disable_Mode,"
                        + " Calc_Needed, Calc_Disable, Sale_Calc, Carry_Fwd_Field, Sub_Needed, Sub_Label, Order_by, Created_dt, Updated_dt) VALUES "
                        + " ( @Division_Code, @Sec_Sale_Code, @Display_Needed, @Value_Needed, @Carry_Fwd_Needed, @Disable_Mode, "
                        + " @Calc_Needed , @Calc_Disable, @Sale_Calc, @Carry_Fwd_Field, @Sub_Needed, @Sub_Label, @Order_by, @Created_dt, @Updated_dt)";
                }
                else //Update the Setup records for the division
                {
                    strQry = "UPDATE Mas_Sec_Sale_Setup " +
                            " SET Display_Needed = @Display_Needed, " +
                            " Value_Needed = @Value_Needed, " +
                            " Carry_Fwd_Needed = @Carry_Fwd_Needed, " +
                            " Disable_Mode = @Disable_Mode, " +
                            " Calc_Needed = @Calc_Needed, " +
                            " Calc_Disable = @Calc_Disable, " +
                            " Sale_Calc = @Sale_Calc, " +
                            " Carry_Fwd_Field = @Carry_Fwd_Field, " +
                            " Sub_Needed = @Sub_Needed, " +
                            " Sub_Label = @Sub_Label, " +
                            " Order_by = @Order_by, " +
                            " Updated_dt = @Updated_dt " +
                            " WHERE Division_Code = @Division_Code " +
                            " AND Sec_Sale_Code = @Sec_Sale_Code " ;
                }

                iReturn = db.ExecQry(strQry, comm);

            }
            catch (Exception ex)
            {                
                iErrReturn = err.LogError(div_code, ex.Message.ToString().Trim(), "Sec Sales Setup", "Record Add()");
            }

            return iReturn;
        }

        public bool sRecordExist(string div_code, int sec_sale_code)
        {
            sCommand = new SqlCommand();
            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                SqlParameter par_div_code = new SqlParameter();
                par_div_code.ParameterName = "@Par_Division_Code";    // Defining Name
                par_div_code.SqlDbType = SqlDbType.Int;           // Defining DataType
                par_div_code.Direction = ParameterDirection.Input;// Setting the direction 

                SqlParameter par_sec_sale_code = new SqlParameter();
                par_sec_sale_code.ParameterName = "@Par_Sec_Sale_Code";    // Defining Name
                par_sec_sale_code.SqlDbType = SqlDbType.Int;           // Defining DataType
                par_sec_sale_code.Direction = ParameterDirection.Input;// Setting the direction 

                // Adding Parameter instances to sqlcommand
                sCommand.Parameters.Add(par_div_code);
                sCommand.Parameters.Add(par_sec_sale_code);

                // Setting values of Parameter
                par_div_code.Value = Convert.ToInt16(div_code);
                par_sec_sale_code.Value = sec_sale_code;

                strQry = " SELECT count(sl_no) " +
                         " FROM Mas_Sec_Sale_Setup " +
                         " WHERE Division_Code = @Par_Division_Code " + 
                         " AND Sec_Sale_Code = @Par_Sec_Sale_Code ";
                int iRecordExist = db.Exec_Scalar(strQry, sCommand);

                if (iRecordExist > 0)
                        bRecordExist = true;
            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Setup", "sRecordExists()");
            }
            return bRecordExist;
        }

        public DataSet getSaleSetup(int div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();
            comm = new SqlCommand();
            DataSet dsSale = null;

            SqlParameter param_div_code = new SqlParameter();
            param_div_code.ParameterName = "@Division_Code";    // Defining Name
            param_div_code.SqlDbType = SqlDbType.Int;           // Defining DataType
            param_div_code.Direction = ParameterDirection.Input;// Setting the direction 

            // Adding Parameter instances to sqlcommand
            comm.Parameters.Add(param_div_code);

            // Setting values of Parameter
            param_div_code.Value = div_code;

            strQry = " SELECT Sec_Sale_Code, " +
                            " Display_Needed, " +
                            " Value_Needed, " +
                            " Carry_Fwd_Needed, " +
                            " Disable_Mode, " +
                            " Calc_Needed, " +
                            " Calc_Disable, " +
                            " Sale_Calc, " +
                            " Carry_Fwd_Field, " +
                            " Order_by " +
                     " FROM Mas_Sec_Sale_Setup " +
                     " WHERE Division_Code = @Division_Code " +
                     " ORDER BY 1 ";

            try
            {
                dsSale = db_ER.Exec_DataSet(strQry, comm);
            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Setup", "getSaleSetup()");
            }
            return dsSale;
        }

        public DataSet getSaleSetup(int div_code, int sec_sale_code)
        {
            DB_EReporting db_ER = new DB_EReporting();
            comm = new SqlCommand();
            DataSet dsSale = null;

            SqlParameter param_div_code = new SqlParameter();
            param_div_code.ParameterName = "@Division_Code";    // Defining Name
            param_div_code.SqlDbType = SqlDbType.Int;           // Defining DataType
            param_div_code.Direction = ParameterDirection.Input;// Setting the direction 

            SqlParameter param_Sec_Sale_Code = new SqlParameter();
            param_Sec_Sale_Code.ParameterName = "@Sec_Sale_Code";    // Defining Name
            param_Sec_Sale_Code.SqlDbType = SqlDbType.Int;           // Defining DataType
            param_Sec_Sale_Code.Direction = ParameterDirection.Input;// Setting the direction 

            // Adding Parameter instances to sqlcommand
            comm.Parameters.Add(param_div_code);
            comm.Parameters.Add(param_Sec_Sale_Code);

            // Setting values of Parameter
            param_div_code.Value = div_code;
            param_Sec_Sale_Code.Value = sec_sale_code;

            strQry = " SELECT Display_Needed, " +
                            " Value_Needed, " +
                            " Carry_Fwd_Needed, " +
                            " Disable_Mode, " +
                            " Calc_Needed, " +
                            " Calc_Disable, " +
                            " Sale_Calc, " +
                            " Carry_Fwd_Field, " +
                            " Sub_Needed, " +
                            " Sub_Label, " +
                            " Order_by " +
                     " FROM Mas_Sec_Sale_Setup " +
                     " WHERE Division_Code = @Division_Code " +
                     " AND Sec_Sale_Code = @Sec_Sale_Code " +
                     " ORDER BY 1 ";

            try
            {
                dsSale = db_ER.Exec_DataSet(strQry, comm);
            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Setup", "getSaleSetup()");
            }
            return dsSale;
        }

        public DataSet getProduct(string div_code, string state, DateTime cdate)
        {
            DB_EReporting db_ER = new DB_EReporting();
            comm = new SqlCommand();
            DataSet dsProduct = null;

            SqlParameter param_div_code = new SqlParameter();
            param_div_code.ParameterName = "@Division_Code";    // Defining Name
            param_div_code.SqlDbType = SqlDbType.Int;           // Defining DataType
            param_div_code.Direction = ParameterDirection.Input;// Setting the direction 

            SqlParameter param_date = new SqlParameter();
            param_date.ParameterName = "@Sel_Date";    // Defining Name
            param_date.SqlDbType = SqlDbType.DateTime;           // Defining DataType
            param_date.Direction = ParameterDirection.Input;// Setting the direction 

            // Adding Parameter instances to sqlcommand
            comm.Parameters.Add(param_div_code);
            comm.Parameters.Add(param_date);

            // Setting values of Parameter
            param_div_code.Value = div_code;
            param_date.Value = cdate;

            //strQry = " SELECT DISTINCT b.Product_Detail_Code, " +
            //          " b.Product_Description, " +
            //          " b.Product_Detail_Name," +
            //          " b.Product_Sale_Unit,  " +
            //          " isnull(rtrim(MRP_Price),0) MRP_Price,  " +
            //          " isnull(rtrim(Retailor_Price),0) Retailor_Price, " +
            //          " isnull(rtrim(Distributor_Price),0) Distributor_Price, " +
            //          " isnull(rtrim(NSR_Price),0) NSR_Price, " +
            //          " isnull(rtrim(Target_Price),0) Target_Price " +
            //          " From Mas_Product_Detail b" +
            //          " INNER JOIN Trans_SS_Entry_Detail c  " +
            //          " ON b.Product_Detail_Code = c.Product_Detail_Code  " +
            //          " WHERE b.Division_Code= @Division_Code " +
            //          " AND b.Product_Active_Flag = 0 ";

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
                      " WHERE b.Division_Code= @Division_Code " +
                      " AND b.Product_Active_Flag = 0" +
                      " AND c.state_code = '" + state + "' " +
                      " AND (b.state_code like '" + state + ',' + "%'  or b.state_code like '%" + ',' + state + ',' + "%') ";

            try
            {
                dsProduct = db_ER.Exec_DataSet(strQry, comm );
            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Transaction", "getProduct()");
            }
            return dsProduct;
        }

        public DataSet getProduct_Total(string div_code, string state, DateTime cdate, string prod_grp)
        {
            DB_EReporting db_ER = new DB_EReporting();
            comm = new SqlCommand();
            DataSet dsProduct = null;

            SqlParameter param_div_code = new SqlParameter();
            param_div_code.ParameterName = "@Division_Code";    // Defining Name
            param_div_code.SqlDbType = SqlDbType.Int;           // Defining DataType
            param_div_code.Direction = ParameterDirection.Input;// Setting the direction 

            SqlParameter param_date = new SqlParameter();
            param_date.ParameterName = "@Sel_Date";    // Defining Name
            param_date.SqlDbType = SqlDbType.DateTime;           // Defining DataType
            param_date.Direction = ParameterDirection.Input;// Setting the direction 

            // Adding Parameter instances to sqlcommand
            comm.Parameters.Add(param_div_code);
            comm.Parameters.Add(param_date);

            // Setting values of Parameter
            param_div_code.Value = div_code;
            param_date.Value = cdate;

            if ((prod_grp == "C") || (prod_grp == "G"))
            {
                strQry = " EXEC sp_ProdList_SecSales '" + div_code + "', '" + state + "', '" + prod_grp + "' ";
            }
            else
            {
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
                          " WHERE b.Division_Code= @Division_Code " +
                          " AND b.Product_Active_Flag = 0 " +
                          " AND c.state_code = '" + state + "' " +
                          " AND (b.state_code like '" + state + ',' + "%'  or b.state_code like '%" + ',' + state + ',' + "%') " +
                          " UNION ALL" +
                          " SELECT 'Tot_Prod' as Product_Detail_Code, '' as Product_Description, '' as Product_Detail_Name, '' as Product_Sale_Unit, " +
                          " '0' as MRP_Price, '0' as Retailor_Price, '0' as Distributor_Price, '0' as NSR_Price, '0' as Target_Price ";
            }
            try
            {
                dsProduct = db_ER.Exec_DataSet(strQry, comm);
            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Transaction", "getProduct()");
            }
            return dsProduct;
        }

        public bool isValueNeeded(string div_code, int sec_sale_code, int val)
        {
            sCommand = new SqlCommand();
            bool bValue = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                SqlParameter par_div_code = new SqlParameter();
                par_div_code.ParameterName = "@Par_Division_Code";    // Defining Name
                par_div_code.SqlDbType = SqlDbType.Int;           // Defining DataType
                par_div_code.Direction = ParameterDirection.Input;// Setting the direction 

                SqlParameter par_sec_sale_code = new SqlParameter();
                par_sec_sale_code.ParameterName = "@Par_Sec_Sale_Code";    // Defining Name
                par_sec_sale_code.SqlDbType = SqlDbType.Int;           // Defining DataType
                par_sec_sale_code.Direction = ParameterDirection.Input;// Setting the direction 

                SqlParameter par_val = new SqlParameter();
                par_val.ParameterName = "@Par_Val";    // Defining Name
                par_val.SqlDbType = SqlDbType.Int;           // Defining DataType
                par_val.Direction = ParameterDirection.Input;// Setting the direction 

                // Adding Parameter instances to sqlcommand
                sCommand.Parameters.Add(par_div_code);
                sCommand.Parameters.Add(par_sec_sale_code);
                sCommand.Parameters.Add(par_val);

                // Setting values of Parameter
                par_div_code.Value = Convert.ToInt16(div_code);
                par_sec_sale_code.Value = sec_sale_code;
                par_val.Value = val;

                //if (sec_sale_code == 3.1 || sec_sale_code == 9.1)
                //{
                //    strQry = " SELECT count(Total_Needed) " +
                //             " FROM mas_common_sec_sale_setup " +
                //             " WHERE Division_Code = @Par_Division_Code " +
                //             " AND value_needed = @Par_Val ";
                //}
                //else
                //{
                    strQry = " SELECT count(sl_no) " +
                             " FROM Mas_Sec_Sale_Setup " +
                             " WHERE Division_Code = @Par_Division_Code " +
                             " AND Sec_Sale_Code = @Par_Sec_Sale_Code " +
                             " AND value_needed = @Par_Val ";
                //}
                int iRecordExist = db.Exec_Scalar(strQry, sCommand);

                if (iRecordExist > 0)
                    bValue = true;
            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Entry", "isValueNeeded()");
            }
            return bValue;
        }


        public bool isTotalValueNeeded(string div_code, int val)
        {
            sCommand = new SqlCommand();
            bool bValue = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                SqlParameter par_div_code = new SqlParameter();
                par_div_code.ParameterName = "@Par_Division_Code";    // Defining Name
                par_div_code.SqlDbType = SqlDbType.Int;           // Defining DataType
                par_div_code.Direction = ParameterDirection.Input;// Setting the direction 

                SqlParameter par_val = new SqlParameter();
                par_val.ParameterName = "@Par_Val";    // Defining Name
                par_val.SqlDbType = SqlDbType.Int;           // Defining DataType
                par_val.Direction = ParameterDirection.Input;// Setting the direction 

                // Adding Parameter instances to sqlcommand
                sCommand.Parameters.Add(par_div_code);
                sCommand.Parameters.Add(par_val);

                // Setting values of Parameter
                par_div_code.Value = Convert.ToInt16(div_code);
                par_val.Value = val;

                strQry = " SELECT count(Total_Needed) " +
                         " FROM mas_common_sec_sale_setup " +
                         " WHERE Division_Code = @Par_Division_Code " +
                         " AND Value_Needed = @Par_Val ";

                int iRecordExist = db.Exec_Scalar(strQry, sCommand);

                if (iRecordExist > 0)   
                    bValue = true;
            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Entry", "isTotalValueNeeded()");
            }
            return bValue;
        }
        
        public bool isSubNeeded(string div_code, int sec_sale_code, int val)
        {
            sCommand = new SqlCommand();
            bool bValue = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                SqlParameter par_div_code = new SqlParameter();
                par_div_code.ParameterName = "@Par_Division_Code";    // Defining Name
                par_div_code.SqlDbType = SqlDbType.Int;           // Defining DataType
                par_div_code.Direction = ParameterDirection.Input;// Setting the direction 

                SqlParameter par_sec_sale_code = new SqlParameter();
                par_sec_sale_code.ParameterName = "@Par_Sec_Sale_Code";    // Defining Name
                par_sec_sale_code.SqlDbType = SqlDbType.Int;           // Defining DataType
                par_sec_sale_code.Direction = ParameterDirection.Input;// Setting the direction 

                SqlParameter par_val = new SqlParameter();
                par_val.ParameterName = "@Par_Val";    // Defining Name
                par_val.SqlDbType = SqlDbType.Int;           // Defining DataType
                par_val.Direction = ParameterDirection.Input;// Setting the direction 

                // Adding Parameter instances to sqlcommand
                sCommand.Parameters.Add(par_div_code);
                sCommand.Parameters.Add(par_sec_sale_code);
                sCommand.Parameters.Add(par_val);

                // Setting values of Parameter
                par_div_code.Value = Convert.ToInt16(div_code);
                par_sec_sale_code.Value = sec_sale_code;
                par_val.Value = val;

                strQry = " SELECT count(sl_no) " +
                         " FROM Mas_Sec_Sale_Setup " +
                         " WHERE Division_Code = @Par_Division_Code " +
                         " AND Sec_Sale_Code = @Par_Sec_Sale_Code " +
                         " AND Sub_needed = @Par_Val ";
                int iRecordExist = db.Exec_Scalar(strQry, sCommand);

                if (iRecordExist > 0)
                    bValue = true;
            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Entry", "isSubNeeded()");
            }
            return bValue;
        }

        public bool isDisableNeeded(string div_code, int sec_sale_code, int val)
        {
            sCommand = new SqlCommand();
            bool bValue = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                SqlParameter par_div_code = new SqlParameter();
                par_div_code.ParameterName = "@Par_Division_Code";    // Defining Name
                par_div_code.SqlDbType = SqlDbType.Int;           // Defining DataType
                par_div_code.Direction = ParameterDirection.Input;// Setting the direction 

                SqlParameter par_sec_sale_code = new SqlParameter();
                par_sec_sale_code.ParameterName = "@Par_Sec_Sale_Code";    // Defining Name
                par_sec_sale_code.SqlDbType = SqlDbType.Int;           // Defining DataType
                par_sec_sale_code.Direction = ParameterDirection.Input;// Setting the direction 

                SqlParameter par_val = new SqlParameter();
                par_val.ParameterName = "@Par_Val";    // Defining Name
                par_val.SqlDbType = SqlDbType.Int;           // Defining DataType
                par_val.Direction = ParameterDirection.Input;// Setting the direction 

                // Adding Parameter instances to sqlcommand
                sCommand.Parameters.Add(par_div_code);
                sCommand.Parameters.Add(par_sec_sale_code);
                sCommand.Parameters.Add(par_val);

                // Setting values of Parameter
                par_div_code.Value = Convert.ToInt16(div_code);
                par_sec_sale_code.Value = sec_sale_code;
                par_val.Value = val;

                strQry = " SELECT count(sl_no) " +
                         " FROM Mas_Sec_Sale_Setup " +
                         " WHERE Division_Code = @Par_Division_Code " +
                         " AND Sec_Sale_Code = @Par_Sec_Sale_Code " +
                         " AND calc_disable = @Par_Val ";
                int iRecordExist = db.Exec_Scalar(strQry, sCommand);

                if (iRecordExist > 0)
                    bValue = true;
            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Entry", "isDisableNeeded()");
            }
            return bValue;
        }

        public int getmaxrecord(string div_code)
        {
            int sl_no = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();
                //strQry = "SELECT COUNT(SS_Head_Sl_No) FROM Trans_SS_Entry_Head";
                strQry = "SELECT ISNULL(MAX(Cast(SS_Head_Sl_No as int)),0) FROM Trans_SS_Entry_Head";
                sl_no = db.Exec_Scalar(strQry);
            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Entry", "getmaxrecord()");
            }
            return sl_no;
        }

        public int getDetmaxrecord(string div_code)
        {
            int sl_no = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();
                //strQry = "SELECT COUNT(SS_Det_Sl_No) FROM Trans_SS_Entry_Detail";
                strQry = "SELECT ISNULL(MAX(Cast(SS_Det_Sl_No as int)),0) FROM Trans_SS_Entry_Detail";
                sl_no = db.Exec_Scalar(strQry);
            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Entry", "getmaxrecord()");
            }
            return sl_no;
        }

        public int RecordAdd(string div_code, string sf_code, int state_code, int stockiest_code, int iMonth, int iYear, int iStatus, bool bRecordExist)
        {
            int iReturn = -1;
            int sl_no = -1;

            ErrorLog err = new ErrorLog();
            try
            {
                DB_EReporting db = new DB_EReporting();

                //strQry = "SELECT COUNT(SS_Head_Sl_No) + 1 FROM Trans_SS_Entry_Head";
                //sl_no = db.Exec_Scalar(strQry);

                sl_no = getmaxrecord(div_code);
                sl_no += 1;

                comm = new SqlCommand();
                comm.Parameters.Clear();

                SqlParameter param_sl_no = new SqlParameter();
                param_sl_no.ParameterName = "@sl_no";    // Defining Name
                param_sl_no.SqlDbType = SqlDbType.VarChar;           // Defining DataType
                param_sl_no.Direction = ParameterDirection.Input;// Setting the direction 

                SqlParameter param_div_code = new SqlParameter();
                param_div_code.ParameterName = "@Division_Code";    // Defining Name
                param_div_code.SqlDbType = SqlDbType.VarChar;           // Defining DataType
                param_div_code.Direction = ParameterDirection.Input;// Setting the direction 

                SqlParameter param_SF_Code = new SqlParameter();
                param_SF_Code.ParameterName = "@SF_Code";    // Defining Name
                param_SF_Code.SqlDbType = SqlDbType.VarChar;           // Defining DataType
                param_SF_Code.Direction = ParameterDirection.Input;// Setting the direction 

                SqlParameter param_state_code = new SqlParameter();
                param_state_code.ParameterName = "@State_Code";    // Defining Name
                param_state_code.SqlDbType = SqlDbType.Int;           // Defining DataType
                param_state_code.Direction = ParameterDirection.Input;// Setting the direction 

                SqlParameter param_stockiest_code = new SqlParameter();
                param_stockiest_code.ParameterName = "@Stockiest_Code";    // Defining Name
                param_stockiest_code.SqlDbType = SqlDbType.BigInt;           // Defining DataType
                param_stockiest_code.Direction = ParameterDirection.Input;// Setting the direction 

                SqlParameter param_month = new SqlParameter();
                param_month.ParameterName = "@Month";    // Defining Name
                param_month.SqlDbType = SqlDbType.Int;           // Defining DataType
                param_month.Direction = ParameterDirection.Input;// Setting the direction 

                SqlParameter param_year = new SqlParameter();
                param_year.ParameterName = "@Year";    // Defining Name
                param_year.SqlDbType = SqlDbType.Int;           // Defining DataType
                param_year.Direction = ParameterDirection.Input;// Setting the direction 

                SqlParameter param_submitted_dtm = new SqlParameter();
                param_submitted_dtm.ParameterName = "@submitted_dtm";    // Defining Name
                param_submitted_dtm.SqlDbType = SqlDbType.VarChar;           // Defining DataType
                param_submitted_dtm.Direction = ParameterDirection.Input;// Setting the direction 

                SqlParameter param_Status = new SqlParameter();
                param_Status.ParameterName = "@Status";    // Defining Name
                param_Status.SqlDbType = SqlDbType.Int;           // Defining DataType
                param_Status.Direction = ParameterDirection.Input;// Setting the direction 

                SqlParameter param_Updated_dt = new SqlParameter();
                param_Updated_dt.ParameterName = "@updated_dtm";    // Defining Name
                param_Updated_dt.SqlDbType = SqlDbType.VarChar;           // Defining DataType
                param_Updated_dt.Direction = ParameterDirection.Input;// Setting the direction 


                // Adding Parameter instances to sqlcommand
                comm.Parameters.Add(param_sl_no);
                comm.Parameters.Add(param_div_code);
                comm.Parameters.Add(param_SF_Code);
                comm.Parameters.Add(param_state_code);
                comm.Parameters.Add(param_stockiest_code);
                comm.Parameters.Add(param_month);
                comm.Parameters.Add(param_year);
                comm.Parameters.Add(param_submitted_dtm);
                comm.Parameters.Add(param_Status);
                comm.Parameters.Add(param_Updated_dt);

                // Setting values of Parameter
                param_sl_no.Value = sl_no.ToString();
                param_div_code.Value = div_code;
                param_SF_Code.Value = sf_code ;
                param_state_code.Value = state_code ;
                param_stockiest_code.Value = stockiest_code ;
                param_month.Value = iMonth;
                param_year.Value = iYear ;
                param_submitted_dtm.Value = DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss"); ;
                param_Status.Value = iStatus;
                param_Updated_dt.Value = DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss");

                if (!bRecordExist) //Creating SS Entry Head
                {
                    strQry = "INSERT INTO Trans_SS_Entry_Head (SS_Head_Sl_No,SF_Code,State_Code,Division_Code,Stockiest_Code,Month,"
                        + " Year, submitted_dtm, Status, updated_dtm) VALUES "
                        + " ( @sl_no , @SF_Code, @State_Code, @Division_Code, @Stockiest_Code, @Month, "
                        + "  @Year, @submitted_dtm , @Status, @updated_dtm)";
                }
                else //Update the Setup records for the division
                {
                    strQry = "UPDATE Trans_SS_Entry_Head " +
                            " SET Status = @Status, " +
                            " updated_dtm = @updated_dtm " +
                            " WHERE Division_Code = @Division_Code " +
                            " AND SF_Code = @SF_Code " +
                            " AND Stockiest_Code = @Stockiest_Code " +
                            " AND Month = @Month " +
                            " AND Year = @Year ";
                }

                iReturn = db.ExecQry(strQry, comm);

            }
            catch (Exception ex)
            {
                iErrReturn = err.LogError(Convert.ToInt32( div_code), ex.Message.ToString().Trim(), "Sec Sales Setup", "Record Add()");
            }

            return iReturn;
        }

        public int RecordUpdate(string div_code, string sf_code, int stockiest_code, int iMonth, int iYear, string reject_by, int status)
        {
            int iReturn = -1;

            ErrorLog err = new ErrorLog();
            try
            {
                DB_EReporting db = new DB_EReporting();
                string sDate = iMonth.ToString() + "-01-" + iYear.ToString();
                DateTime dtEditDate = Convert.ToDateTime(sDate);

                strQry = "EXEC sp_ss_option_edit  '" + div_code + "', '" + sf_code + "', " + stockiest_code + ", '" + dtEditDate + "', '" + reject_by + "', 2, 4 ";

                iReturn = db.ExecQry(strQry);

            }
            catch (Exception ex)
            {
                iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Setup - Option Edit", "Record Update()");
            }

            return iReturn;
        }


        public int RecordUpdate(string div_code, string sf_code, int stockiest_code, int iMonth, int iYear, string reject_by)
        {
            int iReturn = -1;

            ErrorLog err = new ErrorLog();
            try
            {
                DB_EReporting db = new DB_EReporting();

                comm = new SqlCommand();
                comm.Parameters.Clear();

                SqlParameter param_div_code = new SqlParameter();
                param_div_code.ParameterName = "@Division_Code";    // Defining Name
                param_div_code.SqlDbType = SqlDbType.VarChar;           // Defining DataType
                param_div_code.Direction = ParameterDirection.Input;// Setting the direction 

                SqlParameter param_SF_Code = new SqlParameter();
                param_SF_Code.ParameterName = "@SF_Code";    // Defining Name
                param_SF_Code.SqlDbType = SqlDbType.VarChar;           // Defining DataType
                param_SF_Code.Direction = ParameterDirection.Input;// Setting the direction 

                SqlParameter param_stockiest_code = new SqlParameter();
                param_stockiest_code.ParameterName = "@Stockiest_Code";    // Defining Name
                param_stockiest_code.SqlDbType = SqlDbType.BigInt;           // Defining DataType
                param_stockiest_code.Direction = ParameterDirection.Input;// Setting the direction 

                SqlParameter param_month = new SqlParameter();
                param_month.ParameterName = "@Month";    // Defining Name
                param_month.SqlDbType = SqlDbType.Int;           // Defining DataType
                param_month.Direction = ParameterDirection.Input;// Setting the direction 

                SqlParameter param_year = new SqlParameter();
                param_year.ParameterName = "@Year";    // Defining Name
                param_year.SqlDbType = SqlDbType.Int;           // Defining DataType
                param_year.Direction = ParameterDirection.Input;// Setting the direction 

                SqlParameter param_Updated_dt = new SqlParameter();
                param_Updated_dt.ParameterName = "@updated_dtm";    // Defining Name
                param_Updated_dt.SqlDbType = SqlDbType.VarChar;           // Defining DataType
                param_Updated_dt.Direction = ParameterDirection.Input;// Setting the direction 

                SqlParameter param_reject_by = new SqlParameter();
                param_reject_by.ParameterName = "@reject_by";    // Defining Name
                param_reject_by.SqlDbType = SqlDbType.VarChar;           // Defining DataType
                param_reject_by.Direction = ParameterDirection.Input;// Setting the direction 

                // Adding Parameter instances to sqlcommand
                comm.Parameters.Add(param_div_code);
                comm.Parameters.Add(param_SF_Code);
                comm.Parameters.Add(param_stockiest_code);
                comm.Parameters.Add(param_month);
                comm.Parameters.Add(param_year);
                comm.Parameters.Add(param_Updated_dt);
                comm.Parameters.Add(param_reject_by);

                // Setting values of Parameter
                param_div_code.Value = div_code;
                param_SF_Code.Value = sf_code;
                param_stockiest_code.Value = stockiest_code;
                param_month.Value = iMonth;
                param_year.Value = iYear;
                param_Updated_dt.Value = DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss");
                param_reject_by.Value = reject_by;

                strQry = "UPDATE Trans_SS_Entry_Head " +
                        " SET Approval_Mgr = @reject_by, " +
                        " updated_dtm = @updated_dtm " +
                        " WHERE Division_Code = @Division_Code " +
                        " AND SF_Code = @SF_Code " +
                        " AND Stockiest_Code = @Stockiest_Code " +
                        " AND Month = @Month " +
                        " AND Year = @Year ";

                iReturn = db.ExecQry(strQry, comm);

            }
            catch (Exception ex)
            {
                iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Setup - Rejection", "Record Update()");
            }

            return iReturn;
        }


        public int RecordUpdate(string div_code, string sf_code, int stockiest_code, int iMonth, int iYear, string reject_by, string reject_reason)
        {
            int iReturn = -1;

            ErrorLog err = new ErrorLog();
            try
            {
                DB_EReporting db = new DB_EReporting();

                comm = new SqlCommand();
                comm.Parameters.Clear();

                SqlParameter param_div_code = new SqlParameter();
                param_div_code.ParameterName = "@Division_Code";    // Defining Name
                param_div_code.SqlDbType = SqlDbType.VarChar;           // Defining DataType
                param_div_code.Direction = ParameterDirection.Input;// Setting the direction 

                SqlParameter param_SF_Code = new SqlParameter();
                param_SF_Code.ParameterName = "@SF_Code";    // Defining Name
                param_SF_Code.SqlDbType = SqlDbType.VarChar;           // Defining DataType
                param_SF_Code.Direction = ParameterDirection.Input;// Setting the direction 

                SqlParameter param_stockiest_code = new SqlParameter();
                param_stockiest_code.ParameterName = "@Stockiest_Code";    // Defining Name
                param_stockiest_code.SqlDbType = SqlDbType.BigInt;           // Defining DataType
                param_stockiest_code.Direction = ParameterDirection.Input;// Setting the direction 

                SqlParameter param_month = new SqlParameter();
                param_month.ParameterName = "@Month";    // Defining Name
                param_month.SqlDbType = SqlDbType.Int;           // Defining DataType
                param_month.Direction = ParameterDirection.Input;// Setting the direction 

                SqlParameter param_year = new SqlParameter();
                param_year.ParameterName = "@Year";    // Defining Name
                param_year.SqlDbType = SqlDbType.Int;           // Defining DataType
                param_year.Direction = ParameterDirection.Input;// Setting the direction 

                SqlParameter param_Updated_dt = new SqlParameter();
                param_Updated_dt.ParameterName = "@updated_dtm";    // Defining Name
                param_Updated_dt.SqlDbType = SqlDbType.VarChar;           // Defining DataType
                param_Updated_dt.Direction = ParameterDirection.Input;// Setting the direction 

                SqlParameter param_reject_by = new SqlParameter();
                param_reject_by.ParameterName = "@reject_by";    // Defining Name
                param_reject_by.SqlDbType = SqlDbType.VarChar;           // Defining DataType
                param_reject_by.Direction = ParameterDirection.Input;// Setting the direction 

                SqlParameter param_reject_reason = new SqlParameter();
                param_reject_reason.ParameterName = "@reject_reason";    // Defining Name
                param_reject_reason.SqlDbType = SqlDbType.VarChar;           // Defining DataType
                param_reject_reason.Direction = ParameterDirection.Input;// Setting the direction 

                // Adding Parameter instances to sqlcommand
                comm.Parameters.Add(param_div_code);
                comm.Parameters.Add(param_SF_Code);
                comm.Parameters.Add(param_stockiest_code);
                comm.Parameters.Add(param_month);
                comm.Parameters.Add(param_year);
                comm.Parameters.Add(param_Updated_dt);
                comm.Parameters.Add(param_reject_by);
                comm.Parameters.Add(param_reject_reason);

                // Setting values of Parameter
                param_div_code.Value = div_code;
                param_SF_Code.Value = sf_code;
                param_stockiest_code.Value = stockiest_code;
                param_month.Value = iMonth;
                param_year.Value = iYear;
                param_Updated_dt.Value = DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss");
                param_reject_by.Value = reject_by;
                param_reject_reason.Value = reject_reason;

                strQry = "UPDATE Trans_SS_Entry_Head " +
                        " SET Reject_Reason = @reject_reason, " +
                        " Approval_Mgr = @reject_by, " + 
                        " updated_dtm = @updated_dtm " +
                        " WHERE Division_Code = @Division_Code " +
                        " AND SF_Code = @SF_Code " +
                        " AND Stockiest_Code = @Stockiest_Code " +
                        " AND Month = @Month " +
                        " AND Year = @Year ";

                iReturn = db.ExecQry(strQry, comm);

            }
            catch (Exception ex)
            {
                iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Setup - Rejection", "Record Update()");
            }

            return iReturn;
        }


        public int DetailRecordAdd(int SS_Head_Sl_No, string div_code, string prod_code, string mrp_price, string ret_price, string dist_price, string target_price, string nsr_price, bool bRecordExist)
        {
            int iReturn = -1;
            int sl_no = -1;

            ErrorLog err = new ErrorLog();
            try
            {
                DB_EReporting db = new DB_EReporting();

                //strQry = "SELECT COUNT(SS_Det_Sl_No) + 1 FROM Trans_SS_Entry_Detail";

                strQry = "SELECT ISNULL(MAX(Cast(SS_Det_Sl_No as int)),0)+1 FROM Trans_SS_Entry_Detail";
                sl_no = db.Exec_Scalar(strQry);

                comm = new SqlCommand();
                comm.Parameters.Clear();

                SqlParameter param_sl_no = new SqlParameter();
                param_sl_no.ParameterName = "@Head_sl_no";    // Defining Name
                param_sl_no.SqlDbType = SqlDbType.VarChar;           // Defining DataType
                param_sl_no.Direction = ParameterDirection.Input;// Setting the direction 

                SqlParameter param_det_sl_no = new SqlParameter();
                param_det_sl_no.ParameterName = "@Det_sl_no";    // Defining Name
                param_det_sl_no.SqlDbType = SqlDbType.VarChar;           // Defining DataType
                param_det_sl_no.Direction = ParameterDirection.Input;// Setting the direction 

                SqlParameter param_prod_code = new SqlParameter();
                param_prod_code.ParameterName = "@Prod_Code";    // Defining Name
                param_prod_code.SqlDbType = SqlDbType.VarChar;           // Defining DataType
                param_prod_code.Direction = ParameterDirection.Input;// Setting the direction 

                SqlParameter param_mrp_price = new SqlParameter();
                param_mrp_price.ParameterName = "@MRP_Price";    // Defining Name
                param_mrp_price.SqlDbType = SqlDbType.VarChar;           // Defining DataType
                param_mrp_price.Direction = ParameterDirection.Input;// Setting the direction 

                SqlParameter param_ret_price = new SqlParameter();
                param_ret_price.ParameterName = "@Ret_Price";    // Defining Name
                param_ret_price.SqlDbType = SqlDbType.VarChar;           // Defining DataType
                param_ret_price.Direction = ParameterDirection.Input;// Setting the direction 

                SqlParameter param_dist_price = new SqlParameter();
                param_dist_price.ParameterName = "@Dist_Price";    // Defining Name
                param_dist_price.SqlDbType = SqlDbType.VarChar;           // Defining DataType
                param_dist_price.Direction = ParameterDirection.Input;// Setting the direction 

                SqlParameter param_target_price = new SqlParameter();
                param_target_price.ParameterName = "@Target_Price";    // Defining Name
                param_target_price.SqlDbType = SqlDbType.VarChar;           // Defining DataType
                param_target_price.Direction = ParameterDirection.Input;// Setting the direction 

                SqlParameter param_nsr_price = new SqlParameter();
                param_nsr_price.ParameterName = "@NSR_Price";    // Defining Name
                param_nsr_price.SqlDbType = SqlDbType.VarChar;           // Defining DataType
                param_nsr_price.Direction = ParameterDirection.Input;// Setting the direction 

                SqlParameter param_div_code = new SqlParameter();
                param_div_code.ParameterName = "@Div_Code";    // Defining Name
                param_div_code.SqlDbType = SqlDbType.VarChar;           // Defining DataType
                param_div_code.Direction = ParameterDirection.Input;// Setting the direction 

                SqlParameter param_created_dtm = new SqlParameter();
                param_created_dtm.ParameterName = "@created_dtm";    // Defining Name
                param_created_dtm.SqlDbType = SqlDbType.VarChar;           // Defining DataType
                param_created_dtm.Direction = ParameterDirection.Input;// Setting the direction 

                SqlParameter param_Updated_dt = new SqlParameter();
                param_Updated_dt.ParameterName = "@updated_dtm";    // Defining Name
                param_Updated_dt.SqlDbType = SqlDbType.VarChar;           // Defining DataType
                param_Updated_dt.Direction = ParameterDirection.Input;// Setting the direction 


                // Adding Parameter instances to sqlcommand
                comm.Parameters.Add(param_sl_no);
                comm.Parameters.Add(param_det_sl_no);
                comm.Parameters.Add(param_prod_code);
                comm.Parameters.Add(param_mrp_price);
                comm.Parameters.Add(param_ret_price);
                comm.Parameters.Add(param_dist_price);
                comm.Parameters.Add(param_target_price);
                comm.Parameters.Add(param_nsr_price);
                comm.Parameters.Add(param_div_code);
                comm.Parameters.Add(param_created_dtm);
                comm.Parameters.Add(param_Updated_dt);

                // Setting values of Parameter
                param_sl_no.Value = SS_Head_Sl_No.ToString();
                param_det_sl_no.Value = sl_no.ToString();
                param_prod_code.Value = prod_code;
                param_mrp_price.Value = mrp_price;
                param_ret_price.Value = ret_price;
                param_dist_price.Value = dist_price;
                param_target_price.Value = target_price;
                param_nsr_price.Value = nsr_price; 
                param_div_code.Value = div_code;
                param_created_dtm.Value = DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss");
                param_Updated_dt.Value = DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss");

                if (!bRecordExist) //Creating SS Entry Head
                {
                    strQry = "INSERT INTO Trans_SS_Entry_Detail (SS_Det_Sl_No, SS_Head_Sl_No, Product_Detail_Code, MRP_Price, Retailor_Price, " +
                        " Distributor_Price, Target_Price, NSR_Price, Division_Code, Created_dtm, updated_dtm) VALUES "
                        + " ( @Det_sl_no, @Head_sl_no, @Prod_Code, @MRP_Price, @Ret_Price, @Dist_Price, @Target_Price, "
                        + "  @NSR_Price, @Div_Code, @created_dtm, @updated_dtm)";
                }               

                iReturn = db.ExecQry(strQry, comm);

            }
            catch (Exception ex)
            {
                iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Setup", "Record Add()");
            }

            return iReturn;
        }


        public int DetailValueRecordAdd(int SS_Det_Sl_No, string div_code, string sec_sale_code, string sec_sale_qty, string sec_sale_val, string sec_sale_sub, bool bRecordExist)
        {
            int iReturn = -1;
            int sl_no = -1;
            int col_sno = -1;
            DataSet ds;

            ErrorLog err = new ErrorLog();
            try
            {
                DB_EReporting db = new DB_EReporting();

               // strQry = "SELECT COUNT(SS_Det_Sl_No) + 1 FROM Trans_SS_Entry_Detail_Value";

                strQry = "SELECT ISNULL(MAX(Cast(SS_Det_Sl_No as int)),0)+1 FROM Trans_SS_Entry_Detail_Value";
                sl_no = db.Exec_Scalar(strQry);

                if (sl_no > 1)
                {
                   // strQry = "SELECT MAX(cast(SS_Sec_Sl_No as int)) + 1 FROM Trans_SS_Entry_Detail_Value";
                    strQry = "SELECT MAX(cast(SS_Sec_Sl_No as int)) + 1 FROM Trans_SS_Entry_Detail_Value";
                    sl_no = db.Exec_Scalar(strQry);
                }

                comm = new SqlCommand();
                comm.Parameters.Clear();

                SqlParameter param_det_sl_no = new SqlParameter();
                param_det_sl_no.ParameterName = "@Det_sl_no";    // Defining Name
                param_det_sl_no.SqlDbType = SqlDbType.VarChar;           // Defining DataType
                param_det_sl_no.Direction = ParameterDirection.Input;// Setting the direction 
                    
                SqlParameter param_detval_sl_no = new SqlParameter();
                param_detval_sl_no.ParameterName = "@DetVal_sl_no";    // Defining Name
                param_detval_sl_no.SqlDbType = SqlDbType.VarChar;           // Defining DataType
                param_detval_sl_no.Direction = ParameterDirection.Input;// Setting the direction 

                SqlParameter param_secsale_code = new SqlParameter();
                param_secsale_code.ParameterName = "@Sec_Sale_Code";    // Defining Name
                param_secsale_code.SqlDbType = SqlDbType.VarChar;           // Defining DataType
                param_secsale_code.Direction = ParameterDirection.Input;// Setting the direction 

                SqlParameter param_secsale_qty = new SqlParameter();
                param_secsale_qty.ParameterName = "@Sec_Sale_Qty";    // Defining Name
                param_secsale_qty.SqlDbType = SqlDbType.VarChar;           // Defining DataType
                param_secsale_qty.Direction = ParameterDirection.Input;// Setting the direction 

                SqlParameter param_secsale_val = new SqlParameter();
                param_secsale_val.ParameterName = "@Sec_Sale_Val";    // Defining Name
                param_secsale_val.SqlDbType = SqlDbType.VarChar;           // Defining DataType
                param_secsale_val.Direction = ParameterDirection.Input;// Setting the direction 

                SqlParameter param_secsale_sub = new SqlParameter();
                param_secsale_sub.ParameterName = "@Sec_Sale_Sub";    // Defining Name
                param_secsale_sub.SqlDbType = SqlDbType.VarChar;           // Defining DataType
                param_secsale_sub.Direction = ParameterDirection.Input;// Setting the direction 

                SqlParameter param_div_code = new SqlParameter();
                param_div_code.ParameterName = "@Div_Code";    // Defining Name
                param_div_code.SqlDbType = SqlDbType.VarChar;           // Defining DataType
                param_div_code.Direction = ParameterDirection.Input;// Setting the direction 

                SqlParameter param_created_dtm = new SqlParameter();
                param_created_dtm.ParameterName = "@created_dtm";    // Defining Name
                param_created_dtm.SqlDbType = SqlDbType.VarChar;           // Defining DataType
                param_created_dtm.Direction = ParameterDirection.Input;// Setting the direction 

                SqlParameter param_Updated_dt = new SqlParameter();
                param_Updated_dt.ParameterName = "@updated_dtm";    // Defining Name
                param_Updated_dt.SqlDbType = SqlDbType.VarChar;           // Defining DataType
                param_Updated_dt.Direction = ParameterDirection.Input;// Setting the direction 


                // Adding Parameter instances to sqlcommand
                comm.Parameters.Add(param_det_sl_no);
                comm.Parameters.Add(param_detval_sl_no);
                comm.Parameters.Add(param_secsale_code);
                comm.Parameters.Add(param_secsale_qty);
                comm.Parameters.Add(param_secsale_val);
                comm.Parameters.Add(param_secsale_sub);
                comm.Parameters.Add(param_div_code);
                comm.Parameters.Add(param_created_dtm);
                comm.Parameters.Add(param_Updated_dt);

                // Setting values of Parameter
                sec_sale_code = Convert.ToString( (int)Convert.ToDouble(sec_sale_code));
                param_det_sl_no.Value = SS_Det_Sl_No.ToString();
                param_detval_sl_no.Value = sl_no.ToString();
                param_secsale_code.Value = sec_sale_code;
                param_secsale_qty.Value = sec_sale_qty;

                if (sec_sale_val.Trim().Length > 0)
                    param_secsale_val.Value = sec_sale_val;
                else
                    param_secsale_val.Value = DBNull.Value;

                if (sec_sale_sub.Trim().Length > 0)
                    param_secsale_sub.Value = sec_sale_sub;
                else
                    param_secsale_sub.Value = DBNull.Value;

                param_div_code.Value = div_code;
                param_created_dtm.Value = DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss");
                param_Updated_dt.Value = DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss");

                if (!bRecordExist) //Creating SS Entry Detail Value
                {
                    strQry = "INSERT INTO Trans_SS_Entry_Detail_Value (SS_Sec_Sl_No, SS_Det_Sl_No, Sec_Sale_Code, Sec_Sale_Qty, Sec_Sale_Value, Sec_Sale_Sub, " +
                        " Division_Code, Created_dtm, updated_dtm) VALUES "
                        + " ( @DetVal_sl_no, @Det_sl_no, @Sec_Sale_Code, @Sec_Sale_Qty, @Sec_Sale_Val, @Sec_Sale_Sub, "
                        + "   @Div_Code, @created_dtm, @updated_dtm)";
                }
                else //Update the SS Entry Detail Value records for the division
                 {
                    strQry = "UPDATE Trans_SS_Entry_Detail_Value " +
                            " SET Sec_Sale_Qty = @Sec_Sale_Qty, " +
                            " Sec_Sale_Value = @Sec_Sale_Val, " +
                            " Sec_Sale_Sub = @Sec_Sale_Sub, " +
                            " Updated_dtm = @updated_dtm " +
                            " WHERE Division_Code = @Div_Code " +
                            " AND Sec_Sale_Code = @Sec_Sale_Code " +
                            " AND SS_Det_Sl_No = @Det_sl_no ";
                }

                iReturn = db.ExecQry(strQry, comm);

                ds = getColSNo_Formula(div_code, Convert.ToInt32(sec_sale_code));
                if (ds != null)
                {
                    if(ds.Tables[0].Rows[0].ItemArray.GetValue(0).ToString().Trim().Length > 0)
                        col_sno = Convert.ToInt32(ds.Tables[0].Rows[0].ItemArray.GetValue(0).ToString());

                    if (col_sno > 0)
                    {
                        strQry = "UPDATE Mas_Common_SS_Setup_Formula " +
                                " SET SS_Entry_Done = 1 , " +
                                " updated_dtm = getdate() " +
                                " WHERE Division_Code = '" + div_code + "' " +
                                " AND Col_SNo = '" + col_sno + "' ";

                        iReturn = db.ExecQry(strQry);
                    }
                }

            }
            catch (Exception ex)
            {
                iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Entry - Value", "DetailValueRecordAdd()");
            }

            return iReturn;
        }

        
        public bool sRecordExist(string div_code, string sf_code, int imonth, int iyear, int stockiest_code)
        {
            sCommand = new SqlCommand();
            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                SqlParameter par_div_code = new SqlParameter();
                par_div_code.ParameterName = "@Par_Division_Code";    // Defining Name
                par_div_code.SqlDbType = SqlDbType.Int;           // Defining DataType
                par_div_code.Direction = ParameterDirection.Input;// Setting the direction 

                SqlParameter par_sf_code = new SqlParameter();
                par_sf_code.ParameterName = "@Par_SF_Code";    // Defining Name
                par_sf_code.SqlDbType = SqlDbType.VarChar;           // Defining DataType
                par_sf_code.Direction = ParameterDirection.Input;// Setting the direction 

                SqlParameter par_month = new SqlParameter();
                par_month.ParameterName = "@Par_Month";    // Defining Name
                par_month.SqlDbType = SqlDbType.Int;           // Defining DataType
                par_month.Direction = ParameterDirection.Input;// Setting the direction 

                SqlParameter par_year = new SqlParameter();
                par_year.ParameterName = "@Par_Year";    // Defining Name
                par_year.SqlDbType = SqlDbType.Int;           // Defining DataType
                par_year.Direction = ParameterDirection.Input;// Setting the direction 

                SqlParameter par_stockiest = new SqlParameter();
                par_stockiest.ParameterName = "@Par_Stockiest";    // Defining Name
                par_stockiest.SqlDbType = SqlDbType.Int;           // Defining DataType
                par_stockiest.Direction = ParameterDirection.Input;// Setting the direction 

                // Adding Parameter instances to sqlcommand
                sCommand.Parameters.Add(par_div_code);
                sCommand.Parameters.Add(par_sf_code);
                sCommand.Parameters.Add(par_month);
                sCommand.Parameters.Add(par_year);
                sCommand.Parameters.Add(par_stockiest);

                // Setting values of Parameter
                par_div_code.Value = Convert.ToInt16(div_code);
                par_sf_code.Value = sf_code;
                par_month.Value = imonth;
                par_year.Value = iyear;
                par_stockiest.Value = stockiest_code;

                strQry = " SELECT COUNT(SS_Head_Sl_No) " +
                         " FROM Trans_SS_Entry_Head " +
                         " WHERE Division_Code  = @Par_Division_Code " +
                         " AND SF_Code          = @Par_SF_Code " +
                         " AND Month            = @Par_Month " +
                         " AND Year             = @Par_Year " +
                         " AND Stockiest_Code   = @Par_Stockiest ";
                
                int iRecordExist = db.Exec_Scalar(strQry, sCommand);

                if (iRecordExist > 0)
                    bRecordExist = true;
            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Setup", "sRecordExists()");
            }
            return bRecordExist;
        }

        public bool sRecordExist(string div_code, string sf_code, int imonth, int iyear, int stockiest_code, int status)
        {
            sCommand = new SqlCommand();
            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                SqlParameter par_div_code = new SqlParameter();
                par_div_code.ParameterName = "@Par_Division_Code";    // Defining Name
                par_div_code.SqlDbType = SqlDbType.Int;           // Defining DataType
                par_div_code.Direction = ParameterDirection.Input;// Setting the direction 

                SqlParameter par_sf_code = new SqlParameter();
                par_sf_code.ParameterName = "@Par_SF_Code";    // Defining Name
                par_sf_code.SqlDbType = SqlDbType.VarChar;           // Defining DataType
                par_sf_code.Direction = ParameterDirection.Input;// Setting the direction 

                SqlParameter par_month = new SqlParameter();
                par_month.ParameterName = "@Par_Month";    // Defining Name
                par_month.SqlDbType = SqlDbType.Int;           // Defining DataType
                par_month.Direction = ParameterDirection.Input;// Setting the direction 

                SqlParameter par_year = new SqlParameter();
                par_year.ParameterName = "@Par_Year";    // Defining Name
                par_year.SqlDbType = SqlDbType.Int;           // Defining DataType
                par_year.Direction = ParameterDirection.Input;// Setting the direction 

                SqlParameter par_stockiest = new SqlParameter();
                par_stockiest.ParameterName = "@Par_Stockiest";    // Defining Name
                par_stockiest.SqlDbType = SqlDbType.Int;           // Defining DataType
                par_stockiest.Direction = ParameterDirection.Input;// Setting the direction 

                SqlParameter par_status = new SqlParameter();
                par_status.ParameterName = "@Par_Status";    // Defining Name
                par_status.SqlDbType = SqlDbType.Int;           // Defining DataType
                par_status.Direction = ParameterDirection.Input;// Setting the direction 

                // Adding Parameter instances to sqlcommand
                sCommand.Parameters.Add(par_div_code);
                sCommand.Parameters.Add(par_sf_code);
                sCommand.Parameters.Add(par_month);
                sCommand.Parameters.Add(par_year);
                sCommand.Parameters.Add(par_stockiest);
                sCommand.Parameters.Add(par_status);

                // Setting values of Parameter
                par_div_code.Value = Convert.ToInt16(div_code);
                par_sf_code.Value = sf_code;
                par_month.Value = imonth;
                par_year.Value = iyear;
                par_stockiest.Value = stockiest_code;
                par_status.Value = status;

                strQry = " SELECT COUNT(SS_Head_Sl_No) " +
                         " FROM Trans_SS_Entry_Head " +
                         " WHERE Division_Code  = @Par_Division_Code " +
                         " AND SF_Code          = @Par_SF_Code " +
                         " AND Month            = @Par_Month " +
                         " AND Year             = @Par_Year " +
                         " AND Stockiest_Code   = @Par_Stockiest " +
                         " AND Status           = @Par_Status ";

                int iRecordExist = db.Exec_Scalar(strQry, sCommand);

                if (iRecordExist > 0)
                    bRecordExist = true;
            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Setup", "sRecordExists()");
            }
            return bRecordExist;
        }

        public bool sDetailRecordExist(string div_code, string prod_code, string head_sl_no)
        {
            sCommand = new SqlCommand();
            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                SqlParameter par_div_code = new SqlParameter();
                par_div_code.ParameterName = "@Par_Division_Code";    // Defining Name
                par_div_code.SqlDbType = SqlDbType.VarChar;           // Defining DataType
                par_div_code.Direction = ParameterDirection.Input;// Setting the direction 

                SqlParameter par_prod_code = new SqlParameter();
                par_prod_code.ParameterName = "@Par_Prod_Code";    // Defining Name
                par_prod_code.SqlDbType = SqlDbType.VarChar;           // Defining DataType
                par_prod_code.Direction = ParameterDirection.Input;// Setting the direction 

                SqlParameter par_head_sno = new SqlParameter();
                par_head_sno.ParameterName = "@Par_Head_SNo";    // Defining Name
                par_head_sno.SqlDbType = SqlDbType.VarChar;           // Defining DataType
                par_head_sno.Direction = ParameterDirection.Input;// Setting the direction 

                // Adding Parameter instances to sqlcommand
                sCommand.Parameters.Add(par_div_code);
                sCommand.Parameters.Add(par_prod_code);
                sCommand.Parameters.Add(par_head_sno);

                // Setting values of Parameter
                par_div_code.Value = div_code;
                par_prod_code.Value = prod_code;
                par_head_sno.Value = head_sl_no;

                strQry = " SELECT COUNT(SS_Det_Sl_No) " +
                         " FROM Trans_SS_Entry_Detail " +
                         " WHERE Division_Code      = @Par_Division_Code " +
                         " AND Product_Detail_Code  = @Par_Prod_Code " +
                         " AND SS_Head_Sl_No        = @Par_Head_SNo ";

                int iRecordExist = db.Exec_Scalar(strQry, sCommand);

                if (iRecordExist > 0)
                    bRecordExist = true;
            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Setup", "sRecordExists()");
            }
            return bRecordExist;
        }


        public bool sDetailValRecordExist(string div_code, int cmon, int cyear, string prod_code, string sec_sale_code, int stock_code)
        {
            sCommand = new SqlCommand();
            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                SqlParameter par_head_div_code = new SqlParameter();
                par_head_div_code.ParameterName = "@Par_Head_Division_Code";    // Defining Name
                par_head_div_code.SqlDbType = SqlDbType.VarChar;           // Defining DataType
                par_head_div_code.Direction = ParameterDirection.Input;// Setting the direction 

                SqlParameter par_det_div_code = new SqlParameter();
                par_det_div_code.ParameterName = "@Par_Det_Division_Code";    // Defining Name
                par_det_div_code.SqlDbType = SqlDbType.VarChar;           // Defining DataType
                par_det_div_code.Direction = ParameterDirection.Input;// Setting the direction 

                SqlParameter par_val_div_code = new SqlParameter();
                par_val_div_code.ParameterName = "@Par_Val_Division_Code";    // Defining Name
                par_val_div_code.SqlDbType = SqlDbType.VarChar;           // Defining DataType
                par_val_div_code.Direction = ParameterDirection.Input;// Setting the direction 

                SqlParameter par_month = new SqlParameter();
                par_month.ParameterName = "@Par_Month";    // Defining Name
                par_month.SqlDbType = SqlDbType.Int;           // Defining DataType
                par_month.Direction = ParameterDirection.Input;// Setting the direction 

                SqlParameter par_year = new SqlParameter();
                par_year.ParameterName = "@Par_Year";    // Defining Name
                par_year.SqlDbType = SqlDbType.Int;           // Defining DataType
                par_year.Direction = ParameterDirection.Input;// Setting the direction 

                SqlParameter param_secsale_code = new SqlParameter();
                param_secsale_code.ParameterName = "@Sec_Sale_Code";    // Defining Name
                param_secsale_code.SqlDbType = SqlDbType.VarChar;           // Defining DataType
                param_secsale_code.Direction = ParameterDirection.Input;// Setting the direction 

                SqlParameter par_prod_code = new SqlParameter();
                par_prod_code.ParameterName = "@Par_Prod_Code";    // Defining Name
                par_prod_code.SqlDbType = SqlDbType.VarChar;           // Defining DataType
                par_prod_code.Direction = ParameterDirection.Input;// Setting the direction 

                SqlParameter par_stock_code = new SqlParameter();
                par_stock_code.ParameterName = "@Par_Stock_Code";    // Defining Name
                par_stock_code.SqlDbType = SqlDbType.Int;           // Defining DataType
                par_stock_code.Direction = ParameterDirection.Input;// Setting the direction 

                // Adding Parameter instances to sqlcommand
                sCommand.Parameters.Add(par_head_div_code);
                sCommand.Parameters.Add(par_det_div_code);
                sCommand.Parameters.Add(par_val_div_code);
                sCommand.Parameters.Add(par_month);
                sCommand.Parameters.Add(par_year);
                sCommand.Parameters.Add(param_secsale_code);
                sCommand.Parameters.Add(par_prod_code);
                sCommand.Parameters.Add(par_stock_code);

                sec_sale_code = Convert.ToString((int)Convert.ToDouble(sec_sale_code));

                // Setting values of Parameter
                par_head_div_code.Value = div_code;
                par_det_div_code.Value = div_code;
                par_val_div_code.Value = div_code;
                par_month.Value = cmon;
                par_year.Value = cyear;
                param_secsale_code.Value = sec_sale_code;
                par_prod_code.Value = prod_code;
                par_stock_code.Value = stock_code;

                strQry = " SELECT COUNT(SS_Sec_Sl_No) " +
                         " FROM Trans_SS_Entry_Head tseh, Trans_SS_Entry_Detail_Value tsdv, Trans_SS_Entry_Detail tsed " +
                         " WHERE tseh.SS_Head_Sl_No = tsed.SS_Head_Sl_No " +
                         " AND tsed.SS_Det_Sl_No = tsdv.SS_Det_Sl_No " +
                         " AND tseh.Division_Code = @Par_Head_Division_Code " +
                         " AND tsed.Division_Code  = @Par_Det_Division_Code " +
                         " AND tsdv.Division_Code = @Par_Val_Division_Code " +
                         " AND tseh.Month = @Par_Month " +
                         " AND tseh.Year = @Par_Year " +
                         " AND tsdv.Sec_Sale_Code = @Sec_Sale_Code " +
                         " AND tsed.Product_Detail_Code = @Par_Prod_Code " +
                         " AND tseh.Stockiest_Code = @Par_Stock_Code ";

                int iRecordExist = db.Exec_Scalar(strQry, sCommand);

                if (iRecordExist > 0)
                    bRecordExist = true;
            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sale Entry", "sDetailValRecordExist()");
            }
            return bRecordExist;
        }

        public DataSet get_SecSales_Pending_Approval(string sf_code, int istatus)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSecSale = null;

            if (istatus == 1)
            {
                strQry = " SELECT DISTINCT a.sf_code, " +
                                         " a.sf_name, " +
                                         " a.Sf_HQ, " +
                                         " a.sf_Designation_Short_Name, " +
                                         " b.Month as Cur_Month, " +
                                         " b.Year as Cur_Year, " +
                                         " b.Month + '-' + b.YEAR as Mon, " +
                                         " a.sf_code + '-' + cast(b.Month as varchar) + '-' + cast(b.Year as varchar) + '-' + cast(b.Stockiest_Code as varchar) as key_field, " +
                                         " 'Click here to Approve - ' + cast(DateName( month , DateAdd( month , b.Month , 0 ) - 1 ) as varchar) + ' ' + convert(char(4), b.Year) as Month " +
                            " FROM Mas_Salesforce a, Trans_SS_Entry_Head b, Mas_Salesforce_AM c " +
                            " WHERE a.sf_code = b.sf_code " +
                            " AND a.sf_code=c.sf_code  " +
                            " AND c.SS_AM  = '" + sf_code + "' " +
                            " AND b.Status= " + istatus;
            }
            else if (istatus == 3)
            {
                strQry = " SELECT DISTINCT a.sf_code, " +
                                         " a.sf_name, " +
                                         " a.Sf_HQ, " +
                                         " a.sf_Designation_Short_Name, " +
                                         " b.Month as Cur_Month, " +
                                         " b.Year as Cur_Year, " +
                                         " b.Month + '-' + b.YEAR as Mon, " +
                                         " a.sf_code + '-' + cast(b.Month as varchar) + '-' + cast(b.Year as varchar) + '-' + cast(b.Stockiest_Code as varchar) as key_field, " +
                                         " 'Click here for the month - ' + cast(DateName( month , DateAdd( month , b.Month , 0 ) - 1 ) as varchar) + ' ' + convert(char(4), b.Year) as Month " +
                            " FROM Mas_Salesforce a, Trans_SS_Entry_Head b, Mas_Salesforce_AM c " +
                            " WHERE a.sf_code = b.sf_code " +
                            " AND a.sf_code = c.sf_code  " +
                            " AND b.sf_code = '" + sf_code + "' " +
                            " AND b.Status= " + istatus;
            }

            try
            {
                dsSecSale = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dsSecSale;
        }

        public DataSet get_SecSales_Rejection(string sf_code, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSecSale = null;

            strQry = " SELECT Stockiest_Code, Month, Year, Reject_Reason " +
                        " FROM Trans_SS_Entry_Head " +
                        " WHERE division_code = '" + div_code +"' " +
                        " AND sf_code = '" + sf_code + "' " +
                        " AND Status = 3";

            try
            {
                dsSecSale = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                //throw ex;
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sale Entry - Rejection", "get_SecSales_Rejection()");

            }

            return dsSecSale;
        }

        public DataSet getsecsalecode(string div_code, string sec_sub)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSecSale = null;

            strQry = " SELECT Sec_Sale_Code " +
                        " FROM Mas_Sec_Sale_Param " +
                        " WHERE Division_Code = '" + div_code + "' " +
                        " AND Sec_Sale_Sub_Name = '" + sec_sub + "' ";

            try
            {
                dsSecSale = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                //throw ex;
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sale Entry", "getsecsalecode()");

            }

            return dsSecSale;
        }


        public DataSet getClosingBalance(int div_code, string sf_code, int stock_code, int iMonth, int iYear, string prod_code, int sec_sale_code)
        {
            DB_EReporting db_ER = new DB_EReporting();
            comm = new SqlCommand();
            DataSet dsSale = null;

            SqlParameter param_div_code = new SqlParameter();
            param_div_code.ParameterName = "@Division_Code";    // Defining Name
            param_div_code.SqlDbType = SqlDbType.Int;           // Defining DataType
            param_div_code.Direction = ParameterDirection.Input;// Setting the direction 

            SqlParameter param_sf_code = new SqlParameter();
            param_sf_code.ParameterName = "@SF_Code";    // Defining Name
            param_sf_code.SqlDbType = SqlDbType.VarChar;           // Defining DataType
            param_sf_code.Direction = ParameterDirection.Input;// Setting the direction 

            SqlParameter param_stock_code = new SqlParameter();
            param_stock_code.ParameterName = "@Stockiest_Code";    // Defining Name
            param_stock_code.SqlDbType = SqlDbType.Int;           // Defining DataType
            param_stock_code.Direction = ParameterDirection.Input;// Setting the direction 

            SqlParameter param_month = new SqlParameter();
            param_month.ParameterName = "@Month";    // Defining Name
            param_month.SqlDbType = SqlDbType.Int;           // Defining DataType
            param_month.Direction = ParameterDirection.Input;// Setting the direction 

            SqlParameter param_year = new SqlParameter();
            param_year.ParameterName = "@Year";    // Defining Name
            param_year.SqlDbType = SqlDbType.Int;           // Defining DataType
            param_year.Direction = ParameterDirection.Input;// Setting the direction

            SqlParameter param_prod_code = new SqlParameter();
            param_prod_code.ParameterName = "@Product_Detail_Code";    // Defining Name
            param_prod_code.SqlDbType = SqlDbType.VarChar;           // Defining DataType
            param_prod_code.Direction = ParameterDirection.Input;// Setting the direction 

            SqlParameter param_Sec_Sale_Code = new SqlParameter();
            param_Sec_Sale_Code.ParameterName = "@Sec_Sale_Code";    // Defining Name
            param_Sec_Sale_Code.SqlDbType = SqlDbType.Int;           // Defining DataType
            param_Sec_Sale_Code.Direction = ParameterDirection.Input;// Setting the direction 

            // Adding Parameter instances to sqlcommand
            comm.Parameters.Add(param_div_code);
            comm.Parameters.Add(param_sf_code);
            comm.Parameters.Add(param_stock_code);
            comm.Parameters.Add(param_month);
            comm.Parameters.Add(param_year);
            comm.Parameters.Add(param_prod_code);
            comm.Parameters.Add(param_Sec_Sale_Code);

            // Setting values of Parameter
            param_div_code.Value = div_code;
            param_sf_code.Value = sf_code;
            param_stock_code.Value = stock_code;
            param_month.Value = iMonth;
            param_year.Value = iYear;
            param_prod_code.Value = prod_code;
            param_Sec_Sale_Code.Value = sec_sale_code;

            strQry = " SELECT tsedv.Sec_Sale_Qty, tsedv.Sec_Sale_Value  " +
                     " FROM Trans_SS_Entry_Detail_Value tsedv, Trans_SS_Entry_Detail tsed, Trans_SS_Entry_Head tseh " +
                     " WHERE tseh.SS_Head_Sl_No = tsed.SS_Head_Sl_No " +
                     " and tsed.SS_Det_Sl_No = tsedv.SS_Det_Sl_No " +
                     " and tseh.SF_Code = @SF_Code " +
                     " and tseh.Division_Code = @Division_Code " +
                     " and tseh.Stockiest_Code = @Stockiest_Code" +
                     " and tseh.Month = @Month" +
                     " and tseh.Year = @Year" +
                     " and tsed.Product_Detail_Code = @Product_Detail_Code " +                      
                     " and Sec_Sale_Code = @Sec_Sale_Code ";

                    //" and tseh.Status = 2 " +

            try
            {
                dsSale = db_ER.Exec_DataSet(strQry, comm);
            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Entry", "getClosingBalance()");
            }
            return dsSale;
        }

        public int ParamRecordAdd(string div_code, string sale_name, string short_name, string opr)
        {
            int iReturn = -1;

            ErrorLog err = new ErrorLog();
            try
            {                
                DB_EReporting db = new DB_EReporting();
                comm = new SqlCommand();
            
                strQry = "INSERT INTO Mas_Sec_Sale_Param (Division_Code,Sec_Sale_Name,Sec_Sale_Short_Name, " 
                    + " Sec_Sale_Sub_Name,Sel_Sale_Operator, Update_dtm) VALUES "
                    + " ( '" + div_code + "', '" + sale_name + "' , '" + short_name + "' , "
                    + " '" + short_name + "' , '" + opr + "' , getdate())";
                
                iReturn = db.ExecQry(strQry, comm);                
            }
            catch (Exception ex)
            {
                iErrReturn = err.LogError(Convert.ToInt16( div_code), ex.Message.ToString().Trim(), "Sec Sales Setup", "ParamRecordAdd()");
            }

            return iReturn;
        }

        public int ParamRecordUpdate(string div_code, string sale_name, string sale_code)
        {
            int iReturn = -1;

            ErrorLog err = new ErrorLog();
            try
            {
                DB_EReporting db = new DB_EReporting();
                comm = new SqlCommand();

                strQry = "UPDATE Mas_Sec_Sale_Param " +
                        " SET Sec_Sale_Name = '" + sale_name + "' , " +
                        " Update_dtm = getdate() " +
                        " WHERE Sec_Sale_Code = '" + sale_code + "' ";

                iReturn = db.ExecQry(strQry, comm);
            }
            catch (Exception ex)
            {
                iErrReturn = err.LogError(Convert.ToInt16(div_code), ex.Message.ToString().Trim(), "Sec Sales Setup", "ParamRecordAdd()");
            }

            return iReturn;
        }

        public bool sParamRecordExist(string div_code, string sec_sale_name)
        {
            sCommand = new SqlCommand();
            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                SqlParameter par_div_code = new SqlParameter();
                par_div_code.ParameterName = "@Par_Division_Code";    // Defining Name
                par_div_code.SqlDbType = SqlDbType.Int;           // Defining DataType
                par_div_code.Direction = ParameterDirection.Input;// Setting the direction 

                SqlParameter par_sec_sale_code = new SqlParameter();
                par_sec_sale_code.ParameterName = "@Par_Sec_Sale_Name";    // Defining Name
                par_sec_sale_code.SqlDbType = SqlDbType.VarChar;           // Defining DataType
                par_sec_sale_code.Direction = ParameterDirection.Input;// Setting the direction 

                // Adding Parameter instances to sqlcommand
                sCommand.Parameters.Add(par_div_code);
                sCommand.Parameters.Add(par_sec_sale_code);

                // Setting values of Parameter
                par_div_code.Value = Convert.ToInt16(div_code);
                par_sec_sale_code.Value = sec_sale_name;

                strQry = " SELECT count(sec_sale_code) " +
                         " FROM Mas_Sec_Sale_Param " +
                         " WHERE Division_Code = @Par_Division_Code " +
                         " AND Sec_Sale_Name = @Par_Sec_Sale_Name ";
                int iRecordExist = db.Exec_Scalar(strQry, sCommand);

                if (iRecordExist > 0)
                    bRecordExist = true;
            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Setup", "sParamRecordExist()");
            }
            return bRecordExist;
        }

        public DataSet getStockiest(string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            strQry = " select -1 Stockist_Code, '---Select---' Stockist_Name, 1 cind " +
                     " UNION " +
                     " select 999 Stockist_Code, '---All---' Stockist_Name, 2 cind " +
                     " UNION " +
                     " select Stockist_Code,Stockist_Name, 3 cind " +
                     " from Mas_Stockist " +
                     " where Stockist_Active_Flag=0 " +
                     " and Sf_Code like '%" + sf_code + "%' " +
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

        public DataSet getStockiestDet(string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            strQry = " select Stockist_Code,Stockist_Name " +
                     " from Mas_Stockist " +
                     " where Stockist_Active_Flag=0 " +
                     " and Sf_Code like '%" + sf_code + "%' " +
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

        public DataSet getrptfield(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSecSale = null;

            strQry = " SELECT Sec_Sale_Code, Sec_Sale_Name, Sec_Sale_Short_Name , Sel_Sale_Operator" +
                        " FROM Mas_Sec_Sale_Param " +
                        " WHERE Division_Code = '" + div_code + "' " +
                        " AND is_rpt_field = 0 " +
                        " ORDER BY Sel_Sale_Operator ";

            try
            {
                dsSecSale = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                //throw ex;
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sale Report", "getrptfield()");

            }

            return dsSecSale;
        }

        public DataSet getrptvalues(string div_code, string sf_code, int stock_code, int fmonth, int fyear, int tmonth, int tyear, string prod_code,
            double rate, int secsalecode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSecSale = null;

            strQry = "EXEC sp_get_sec_sale_details  '" + div_code + "' , '" + sf_code + "', " + stock_code  + ", " + fmonth  + ", " + fyear  + ", " + tmonth  + ", " +
                                                    tyear + " , '" + prod_code  + "', " + rate  + ", " + secsalecode + " ";

            try
            {
                dsSecSale = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                //throw ex;
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sale Report", "getrptfield()");

            }

            return dsSecSale;
        }


        public DataSet getrptvalues_clbal(string div_code, string sf_code, int stock_code, int fmonth, int fyear, string prod_code,
            double rate, int secsalecode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSecSale = null;

            strQry = "EXEC sp_get_sec_sale_details_clbal  '" + div_code + "' , '" + sf_code + "', " + stock_code + ", " + fmonth + ", " +
                                                    fyear + " , '" + prod_code + "', " + rate + ", " + secsalecode + " ";

            try
            {
                dsSecSale = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                //throw ex;
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sale Report", "getrptfield()");

            }

            return dsSecSale;
        }

        public bool SetupRecordExist(string div_code)
        {
            sCommand = new SqlCommand();
            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                SqlParameter par_div_code = new SqlParameter();
                par_div_code.ParameterName = "@Par_Division_Code";    // Defining Name
                par_div_code.SqlDbType = SqlDbType.Int;           // Defining DataType
                par_div_code.Direction = ParameterDirection.Input;// Setting the direction 

                // Adding Parameter instances to sqlcommand
                sCommand.Parameters.Add(par_div_code);

                // Setting values of Parameter
                par_div_code.Value = Convert.ToInt16(div_code);

                strQry = " SELECT COUNT(Division_Code) " +
                         " FROM mas_common_sec_sale_setup " +
                         " WHERE Division_Code  = @Par_Division_Code ";

                int iRecordExist = db.Exec_Scalar(strQry, sCommand);

                if (iRecordExist > 0)
                    bRecordExist = true;
            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Additional Setup", "SetupRecordExists()");
            }
            return bRecordExist;
        }

        public int RecordAdd(string div_code, int total_needed, int value_needed, string calc_rate, int approval_needed, string Prod_Grp, bool bRecordExist)
        {
            int iReturn = -1;

            ErrorLog err = new ErrorLog();
            try
            {
                DB_EReporting db = new DB_EReporting();

                comm = new SqlCommand();
                comm.Parameters.Clear();

                SqlParameter param_div_code = new SqlParameter();
                param_div_code.ParameterName = "@Division_Code";    // Defining Name
                param_div_code.SqlDbType = SqlDbType.Int;           // Defining DataType
                param_div_code.Direction = ParameterDirection.Input;// Setting the direction 

                SqlParameter param_total_needed = new SqlParameter();
                param_total_needed.ParameterName = "@Total_Needed";    // Defining Name
                param_total_needed.SqlDbType = SqlDbType.Int;           // Defining DataType
                param_total_needed.Direction = ParameterDirection.Input;// Setting the direction 

                SqlParameter param_value_needed = new SqlParameter();
                param_value_needed.ParameterName = "@Value_Needed";    // Defining Name
                param_value_needed.SqlDbType = SqlDbType.Int;           // Defining DataType
                param_value_needed.Direction = ParameterDirection.Input;// Setting the direction 

                SqlParameter param_calc_rate = new SqlParameter();
                param_calc_rate.ParameterName = "@Calc_Rate";    // Defining Name
                param_calc_rate.SqlDbType = SqlDbType.VarChar;           // Defining DataType
                param_calc_rate.Direction = ParameterDirection.Input;// Setting the direction 

                SqlParameter param_approval_needed = new SqlParameter();
                param_approval_needed.ParameterName = "@Approval_Needed";    // Defining Name
                param_approval_needed.SqlDbType = SqlDbType.Int;           // Defining DataType
                param_approval_needed.Direction = ParameterDirection.Input;// Setting the direction 

                SqlParameter param_prod_grp = new SqlParameter();
                param_prod_grp.ParameterName = "@Prod_Grp";    // Defining Name
                param_prod_grp.SqlDbType = SqlDbType.VarChar;           // Defining DataType
                param_prod_grp.Direction = ParameterDirection.Input;// Setting the direction 

                SqlParameter param_created_dtm = new SqlParameter();
                param_created_dtm.ParameterName = "@created_dtm";    // Defining Name
                param_created_dtm.SqlDbType = SqlDbType.VarChar;           // Defining DataType
                param_created_dtm.Direction = ParameterDirection.Input;// Setting the direction 

                SqlParameter param_Updated_dt = new SqlParameter();
                param_Updated_dt.ParameterName = "@updated_dtm";    // Defining Name
                param_Updated_dt.SqlDbType = SqlDbType.VarChar;           // Defining DataType
                param_Updated_dt.Direction = ParameterDirection.Input;// Setting the direction 

                // Adding Parameter instances to sqlcommand
                comm.Parameters.Add(param_div_code);
                comm.Parameters.Add(param_total_needed);
                comm.Parameters.Add(param_value_needed);                
                comm.Parameters.Add(param_calc_rate);
                comm.Parameters.Add(param_approval_needed);
                comm.Parameters.Add(param_prod_grp);
                comm.Parameters.Add(param_created_dtm);
                comm.Parameters.Add(param_Updated_dt);

                //Commented as the below code is no longer required on 01/31/16
                //if (value_needed == 1)
                //    value_needed = 0;
                //else if (value_needed == 0)
                //    value_needed = 1;

                // Setting values of Parameter
                param_div_code.Value = Convert.ToInt16( div_code);
                param_total_needed.Value = total_needed;
                param_value_needed.Value = value_needed;
                param_calc_rate.Value = calc_rate;
                param_approval_needed.Value = approval_needed;
                param_prod_grp.Value = Prod_Grp;
                param_created_dtm.Value = DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss"); ;
                param_Updated_dt.Value = DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss");

                if (!bRecordExist) //Creating common secondary setup
                {
                    strQry = "INSERT INTO mas_common_sec_sale_setup (Division_Code, Total_Needed, Value_Needed, calc_rate, Approval_Needed, Prod_Grp, created_dtm, updated_dtm) VALUES "
                        + " ( @Division_Code, @Total_Needed, @Value_Needed, @Calc_Rate, @Approval_Needed, @Prod_Grp, @created_dtm, @updated_dtm)";
                }
                else //Update the common secondary setup for the division
                {
                    strQry = "UPDATE mas_common_sec_sale_setup " +
                            " SET Total_Needed = @Total_Needed, " +
                            " Value_Needed = @Value_Needed, " + 
                            " calc_rate = @Calc_Rate, " +
                            " Approval_Needed = @Approval_Needed, " + 
                            " Prod_Grp = @Prod_Grp, " +
                            " created_dtm = @created_dtm, " +
                            " updated_dtm = @updated_dtm " +
                            " WHERE Division_Code = @Division_Code ";
                }

                iReturn = db.ExecQry(strQry, comm);

            }
            catch (Exception ex)
            {
                iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Additional Setup", "Record Add()");
            }

            return iReturn;
        }

        public int IsReportField(string div_code, int sec_sale_code)
        {
            int iReturn = -1;

            ErrorLog err = new ErrorLog();
            try
            {
                DB_EReporting db = new DB_EReporting();

                comm = new SqlCommand();
                comm.Parameters.Clear();

                SqlParameter param_div_code = new SqlParameter();
                param_div_code.ParameterName = "@Division_Code";    // Defining Name
                param_div_code.SqlDbType = SqlDbType.Int;           // Defining DataType
                param_div_code.Direction = ParameterDirection.Input;// Setting the direction 

                SqlParameter param_sec_sale = new SqlParameter();
                param_sec_sale.ParameterName = "@sec_sale_code";    // Defining Name
                param_sec_sale.SqlDbType = SqlDbType.Int;           // Defining DataType
                param_sec_sale.Direction = ParameterDirection.Input;// Setting the direction 

                SqlParameter param_Updated_dt = new SqlParameter();
                param_Updated_dt.ParameterName = "@updated_dtm";    // Defining Name
                param_Updated_dt.SqlDbType = SqlDbType.VarChar;           // Defining DataType
                param_Updated_dt.Direction = ParameterDirection.Input;// Setting the direction 

                // Adding Parameter instances to sqlcommand
                comm.Parameters.Add(param_div_code);
                comm.Parameters.Add(param_sec_sale);
                comm.Parameters.Add(param_Updated_dt);

                // Setting values of Parameter
                param_div_code.Value = Convert.ToInt16(div_code);
                param_sec_sale.Value = sec_sale_code;
                param_Updated_dt.Value = DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss");

                strQry = "UPDATE Mas_Sec_Sale_Param " +
                        " SET is_rpt_field = 0, " +
                        " update_dtm = @updated_dtm " +
                        " WHERE Division_Code = @Division_Code " +
                        " AND Sec_Sale_Code = @sec_sale_code " ;

                iReturn = db.ExecQry(strQry, comm);

            }
            catch (Exception ex)
            {
                iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Additional Setup", "IsReportField()");
            }

            return iReturn;
        }


        public DataSet getAddionalSaleMaster(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSale = null;
            strQry = "SELECT  Total_Needed, " +
                            " Value_Needed, " +
                            " calc_rate, " +
                            " Approval_Needed, " +
                            " Prod_Grp " +
                     " FROM mas_common_sec_sale_setup " +
                     " WHERE Division_Code = '" + div_code + "'";
            try
            {
                dsSale = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(0, ex.Message.ToString().Trim(), "Sec Sales Setup", "getAddionalSaleMaster()");
            }
            return dsSale;
        }


        public DataSet getAddionalRptSaleMaster(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSale = null;
            strQry = "SELECT  Total_Needed, " +
                            " Value_Needed, " +
                            " calc_rate, " +
                            " Prod_Grp " +
                     " FROM mas_common_sec_sale_setup " +
                     " WHERE Division_Code = '" + div_code + "'";
            try
            {
                dsSale = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(0, ex.Message.ToString().Trim(), "Sec Sales Setup", "getAddionalSaleMaster()");
            }
            return dsSale;
        }

        public bool isParamRecordExist_TotalField(string div_code)
        {
            sCommand = new SqlCommand();
            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                SqlParameter par_div_code = new SqlParameter();
                par_div_code.ParameterName = "@Par_Division_Code";    // Defining Name
                par_div_code.SqlDbType = SqlDbType.Int;           // Defining DataType
                par_div_code.Direction = ParameterDirection.Input;// Setting the direction 

                // Adding Parameter instances to sqlcommand
                sCommand.Parameters.Add(par_div_code);

                // Setting values of Parameter
                par_div_code.Value = Convert.ToInt16(div_code);

                strQry = " SELECT count(sec_sale_code) " +
                         " FROM Mas_Sec_Sale_Param " +
                         " WHERE Division_Code = @Par_Division_Code " +
                         " AND Sec_Sale_Sub_Name = 'Tot+' ";
                int iRecordExist = db.Exec_Scalar(strQry, sCommand);

                if (iRecordExist > 0)
                    bRecordExist = true;
            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Setup", "sRecordExists()");
            }
            return bRecordExist;
        }


        public DataSet Get_SecSaleCode_TotalField(string div_code)
        {
            sCommand = new SqlCommand();
            bool bRecordExist = false;
            DataSet ds = new DataSet();
            try
            {
                DB_EReporting db = new DB_EReporting();

                SqlParameter par_div_code = new SqlParameter();
                par_div_code.ParameterName = "@Par_Division_Code";    // Defining Name
                par_div_code.SqlDbType = SqlDbType.Int;           // Defining DataType
                par_div_code.Direction = ParameterDirection.Input;// Setting the direction 

                // Adding Parameter instances to sqlcommand
                sCommand.Parameters.Add(par_div_code);

                // Setting values of Parameter
                par_div_code.Value = Convert.ToInt16(div_code);

                strQry = " SELECT sec_sale_code " +
                         " FROM Mas_Sec_Sale_Param " +
                         " WHERE Division_Code = @Par_Division_Code " +
                         " AND Sec_Sale_Sub_Name = 'Tot+' ";

                ds = db.Exec_DataSet(strQry, sCommand);
                
            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Setup", "sRecordExists()");
            }
            return ds;
        }



        public int RecordAdd_TotalValue_Needed(string div_code, int total_needed, int value_needed)
        {
            int iReturn = -1;

            ErrorLog err = new ErrorLog();
            try
            {
                DB_EReporting db = new DB_EReporting();
                DataSet dsSale = new DataSet();
                int sec_sale_code = -1;

                //if (total_needed == 1)
                //{
                //strQry = "SELECT MAX(sec_sale_code) + 1 FROM Mas_Sec_Sale_Param";
                //    int sec_sale_code = db.Exec_Scalar(strQry);

                //    strQry = "SELECT MAX(sl_no) + 1 FROM Mas_Sec_Sale_Setup";
                //    int setup_sl_no = db.Exec_Scalar(strQry);

                    if (!isParamRecordExist_TotalField(div_code))
                    {
                        strQry = "INSERT INTO Mas_Sec_Sale_Param " +
                            " (Sec_Sale_Name, Sec_Sale_Short_Name, Sec_Sale_Sub_Name, Sel_Sale_Operator, Is_Rpt_Field, Division_Code, Update_dtm ) " +
                            " VALUES ('Total +', 'Tot+', 'Tot+', '+', NULL, '" + div_code + "', GETDATE()) ";
                        iReturn = db.ExecQry(strQry);
                    }
                    //else
                    //{
                        dsSale = Get_SecSaleCode_TotalField(div_code);
                        if (dsSale != null)
                            sec_sale_code = Convert.ToInt32( dsSale.Tables[0].Rows[0].ItemArray.GetValue(0).ToString());
                    //}

                    if (!sRecordExist(div_code, sec_sale_code))
                    {
                        strQry = "INSERT INTO Mas_Sec_Sale_Setup (Division_Code, Sec_Sale_Code, Display_Needed, Value_Needed, Carry_Fwd_Needed, Disable_Mode, Calc_Needed, " +
                        " Calc_Disable, Sale_Calc, Carry_Fwd_Field, Sub_Needed, Sub_Label, Order_by, Created_dt, Updated_dt) " +
                        " VALUES('" + div_code + "', '" + sec_sale_code + "', '" + total_needed + "', '" + value_needed + "', 0, 1, 1, 1, 0, 0, 0, NULL, 9, getdate(), getdate()) ";
                        iReturn = db.ExecQry(strQry);
                    }
                    else
                    {
                        strQry = "UPDATE Mas_Sec_Sale_Setup " +
                                    " SET Display_Needed = '" + total_needed + "', " +
                                    " Value_Needed = '" + value_needed + "' " +
                                    " WHERE division_code = '" + div_code + "' " +
                                    " AND Sec_Sale_Code = '" + sec_sale_code + "' ";

                        iReturn = db.ExecQry(strQry);
                    }
                //}

            }
            catch (Exception ex)
            {
                iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Additional Setup", "Record Add()");
            }

            return iReturn;
        }


        public bool isSaleEntered(string sf_code, string div_code, int imonth, int iyear, int stockiest_code)
        {
            sCommand = new SqlCommand();
            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = " SELECT COUNT(SS_Head_Sl_No) " +
                         " FROM Trans_SS_Entry_Head " +
                         " WHERE SF_Code = '" + sf_code + "' " +
                         " AND Division_Code  = '" + div_code + "' " + 
                         " AND Month = '" + imonth + "' " +
                         " AND Year = '" + iyear + "' " +
                         " AND Stockiest_Code = '" + stockiest_code + "' " +
                         " AND Status = 2";

                int iRecordExist = db.Exec_Scalar(strQry);

                if (iRecordExist > 0)
                    bRecordExist = true;
            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Additional Setup", "isSaleEntered()");
            }
            return bRecordExist;
        }

        public DataSet Get_Div_Year(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "select max([Year]-1) as Year from Mas_Division where Division_Code='" + div_code + "'";
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

        public bool getcount_ssentry(string div_code, string sf_code, string stock_code, int imonth, int iyear)
        {
            sCommand = new SqlCommand();
            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = " select COUNT(SS_Head_Sl_No) from Trans_SS_Entry_Head where sf_code='" + sf_code + "' and Stockiest_Code = '" + stock_code + "' and Division_Code = '" + div_code + "' and MONTH = " + imonth + " and YEAR = " + iyear + " ";
                int iRecordExist = db.Exec_Scalar(strQry);

                if (iRecordExist > 0)
                    bRecordExist = true;
            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Additional Setup", "SetupRecordExists()");
            }
            return bRecordExist;
        }

        public DataSet getSubmittedDate(string div_code, string sf_code, string stock_code, int imonth, int iyear)
        {
            DataSet ds = new DataSet();
            try
            {
                DB_EReporting db = new DB_EReporting();
                

                strQry = " select updated_dtm from Trans_SS_Entry_Head where sf_code='" + sf_code + "' and Stockiest_Code = '" + stock_code + "' and Division_Code = '" + div_code + "' and MONTH = " + imonth + " and YEAR = " + iyear + " ";
                ds = db.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Stockiest Report", "getSubmittedDate()");
            }
            return ds;
        }

        public DataSet Get_SS_Stockiest_Details(string div_code, string sf_code, int imon, int iyr)
        {   
            DataSet ds = new DataSet();
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "select a.Stockiest_Code, b.Stockist_Name from Trans_SS_Entry_Head a, Mas_Stockist b where a.Stockiest_Code = b.Stockist_Code and a.sf_code='" + sf_code + "' and a.Division_Code='" + div_code + "' and a.MONTH=" + imon + " and a.YEAR=" + iyr + " and a.status=2 ";
                ds = db.Exec_DataSet(strQry);

            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Edit", "Get_SS_Stockiest_Details()");
            }
            return ds;
        }

        public DataSet Get_SS_Option_Edit(string div_code, string sf_code, int istatus)
        {
            DataSet ds = new DataSet();
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "select SS_Head_Sl_No, MONTH, YEAR, Approval_Mgr, Stockiest_Code " +
                            " from Trans_SS_Entry_Head " + 
                            " WHERE Division_Code = '" + div_code + "' " + 
                            " AND SF_Code = '" + sf_code + "' "  +
                            " AND Status = " + istatus + " " +
                            " AND submitted_dtm in ( " +
                            " select MIN(submitted_dtm) from Trans_SS_Entry_Head " +
                            " WHERE Division_Code = '" + div_code + "' " + 
                            " AND SF_Code = '" + sf_code + "' " + 
                            " AND Status = " + istatus + ") ";

                ds = db.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Edit", "Get_SS_Stockiest_Details()");
            }
            return ds;
        }

        public DataSet Get_SS_ClBal_Sub(string div_code)
        {
            DataSet ds = new DataSet();
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = " select mssp.Sec_Sale_Sub_Name " +
                            " from Mas_Sec_Sale_Param mssp, Mas_Sec_Sale_Setup msss " +
                            " WHERE mssp.Division_Code = '" + div_code + "' " +
                            " AND msss.Division_Code =  '" + div_code + "' " +
                            " AND mssp.Sec_Sale_Code = msss.Sec_Sale_Code " +
                            " AND msss.Carry_Fwd_Field = 1 " ;

                ds = db.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Entry - Closing Balance", "Get_SS_ClBal_Sub()");
            }
            return ds;
        }

        public bool FormulaRecordExist(string div_code, string col_name)
        {
            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = " SELECT COUNT(Col_SNo) " +
                         " FROM Mas_Common_SS_Setup_Formula " +
                         " WHERE Division_Code = '" + div_code + "' " + 
                         " AND Col_Name = '" + col_name + "' ";

                int iRecordExist = db.Exec_Scalar(strQry);

                if (iRecordExist > 0)
                    bRecordExist = true;
            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Additional Setup - Formula", "FormulaRecordExist()");
            }
            return bRecordExist;
        }

        public int Formula_RecordAdd(string div_code, string col_name, string dis_mode, string order_by, string der_formula, bool bRecordExist)
        {
            int iReturn = -1;
            DataSet dsSale = null;
            int sec_sale_code = -1;
            int dismode = 0;

            ErrorLog err = new ErrorLog();
            try
            {
                DB_EReporting db = new DB_EReporting();
                int col_sno = 0;

                if (!bRecordExist) //Creating common secondary setup
                {
                    strQry = "SELECT COUNT(Col_SNo) + 1 FROM Mas_Common_SS_Setup_Formula";
                    col_sno = db.Exec_Scalar(strQry);

                    if (col_sno > 1)
                    {
                        strQry = "SELECT MAX(Col_SNo) + 1 FROM Mas_Common_SS_Setup_Formula";
                        col_sno = db.Exec_Scalar(strQry);
                    }

                    strQry = "INSERT INTO Mas_Common_SS_Setup_Formula (Col_SNo, Col_Name, Division_Code, Dis_Mode, Order_By, Der_Formula, created_dtm, updated_dtm) VALUES "
                        + " ( '" + col_sno + "', '" + col_name + "', '" + div_code + "', '" + dis_mode + "', '" + order_by + "', '" + der_formula + "', getdate(), getdate())";
                }
                //else //Update the common secondary setup for the division
                //{
                //    strQry = "UPDATE Mas_Common_SS_Setup_Formula " +
                //            " SET Col_Name = '" + col_name + "', " +
                //            " Dis_Mode = '" + dis_mode + "', " +
                //            " Order_By = '" + order_by + "', " +
                //            " der_formula = '" + der_formula + "', " +
                //            " updated_dtm = @updated_dtm " +
                //            " WHERE Division_Code = '" + div_code + "' " +
                //            " AND Col_SNo = '" + col_sno + "' ";
                //}

                iReturn = db.ExecQry(strQry);

                strQry = "INSERT INTO Mas_Sec_Sale_Param " +
                    " (Sec_Sale_Name, Sec_Sale_Short_Name, Sec_Sale_Sub_Name, Sel_Sale_Operator, Is_Rpt_Field, Cust_Col_SNo, Division_Code, Update_dtm ) " +
                    " VALUES ('" + col_name + "', '" + col_name + "', 'cust_col', 'D', NULL, " + col_sno + ", '" + div_code + "', GETDATE()) ";

                iReturn = db.ExecQry(strQry);

                dsSale = Get_SecSaleCode_CustCol(div_code, col_sno);
                if (dsSale != null)
                    sec_sale_code = Convert.ToInt32(dsSale.Tables[0].Rows[0].ItemArray.GetValue(0).ToString());

                if (!sRecordExist(div_code, sec_sale_code))
                {
                    if (dis_mode == "Y")
                        dismode = 1;

                    strQry = "INSERT INTO Mas_Sec_Sale_Setup (Division_Code, Sec_Sale_Code, Display_Needed, Value_Needed, Carry_Fwd_Needed, Disable_Mode, Calc_Needed, " +
                    " Calc_Disable, Sale_Calc, Carry_Fwd_Field, Sub_Needed, Sub_Label, Order_by, Created_dt, Updated_dt) " +
                    " VALUES('" + div_code + "', '" + sec_sale_code + "', 1, 0, 0, '" + dismode + "', 1, 1, 0, 0, 0, NULL, '" + order_by + "', getdate(), getdate()) ";
                    iReturn = db.ExecQry(strQry);
                }
                //else
                //{
                //    strQry = "UPDATE Mas_Sec_Sale_Setup " +
                //                " SET Display_Needed = '" + total_needed + "', " +
                //                " Value_Needed = '" + value_needed + "' " +
                //                " WHERE division_code = '" + div_code + "' " +
                //                " AND Sec_Sale_Code = '" + sec_sale_code + "' ";

                //    iReturn = db.ExecQry(strQry);
                //}


            }
            catch (Exception ex)
            {
                iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Additional Formula Setup", "Formula_RecordAdd()");
            }

            return iReturn;
        }

        public int Formula_RecordUpdate(string div_code, string col_sno, string col_name, string dis_mode, string order_by, string der_formula)
        {
            int iReturn = -1;

            ErrorLog err = new ErrorLog();
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "UPDATE Mas_Common_SS_Setup_Formula " +
                        " SET Col_Name = '" + col_name + "', " +
                        " Dis_Mode = '" + dis_mode + "', " +
                        " Order_By = '" + order_by + "', " +
                        " der_formula = '" + der_formula + "', " +
                        " updated_dtm = getdate() " +
                        " WHERE Division_Code = '" + div_code + "' " +
                        " AND Col_SNo = '" + col_sno + "' ";

                iReturn = db.ExecQry(strQry);

            }
            catch (Exception ex)
            {
                iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Additional Formula Setup", "Formula_RecordUpdate()");
            }

            return iReturn;
        }

        public int Formula_RecordDelete(string div_code, string col_sno)
        {
            int iReturn = -1;

            ErrorLog err = new ErrorLog();
            try
            {
                DB_EReporting db = new DB_EReporting();

                //if (!FormulaExists_Entry(div_code, Convert.ToInt32(col_sno)))
                //{
                    if (FormulaExists_Param(div_code, Convert.ToInt32(col_sno)))
                    {
                        strQry = "DELETE Mas_Sec_Sale_Setup " +
                                " WHERE Division_Code = '" + div_code + "' " +
                                " AND Sec_Sale_Code in " +
                                " ( " +
                                " SELECT Sec_Sale_Code FROM Mas_Sec_Sale_Param " +
                                " WHERE Division_Code = '" + div_code + "' " +
                                " AND Sec_Sale_Sub_Name = 'cust_col' and Sel_Sale_Operator='D' " +
                                " AND Cust_Col_SNo = '" + col_sno + "' " +
                                " ) ";

                        iReturn = db.ExecQry(strQry);

                        strQry = "DELETE Mas_Sec_Sale_Param " +
                                " WHERE Division_Code = '" + div_code + "' " +
                                " AND Sec_Sale_Sub_Name = 'cust_col' and Sel_Sale_Operator='D' " +
                                " AND Cust_Col_SNo = '" + col_sno + "' ";

                        iReturn = db.ExecQry(strQry);

                    }

                    strQry = "DELETE Mas_Common_SS_Setup_Formula " +
                            " WHERE Division_Code = '" + div_code + "' " +
                            " AND Col_SNo = '" + col_sno + "' ";

                    iReturn = db.ExecQry(strQry);
                }
            //}
            catch (Exception ex)
            {
                iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Additional Formula Setup", "Formula_RecordDelete()");
            }

            return iReturn;
        }



        public DataSet getSaleMaster_Formula(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSale = null;
            strQry = "SELECT  Col_SNo, " +
                            " Col_Name, " +
                            " Dis_Mode, " +
                            " Order_By, " +
                            " Der_Formula " +
                     " FROM Mas_Common_SS_Setup_Formula " +
                     " WHERE Division_Code = '" + div_code + "' " +
                     " ORDER BY 1";
            try
            {
                dsSale = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(0, ex.Message.ToString().Trim(), "Sec Sales Setup - Formula", "getSaleMaster_Formula()");
            }
            return dsSale;
        }

        public DataSet getSaleMaster_Det(string div_code, int secsale_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSale = null;
            strQry = "SELECT Sec_Sale_Name, Sec_Sale_Short_Name, Sec_Sale_Sub_Name, Sel_Sale_Operator " +
                     " FROM Mas_Sec_Sale_Param " +
                     " WHERE Division_Code = '" + div_code + "' " +
                     " AND Sec_Sale_Code = " + secsale_code + 
                     " ORDER BY 1";
            try
            {
                dsSale = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(0, ex.Message.ToString().Trim(), "Sec Sales Setup - Formula", "getSaleMaster_Formula()");
            }
            return dsSale;
        }

        public DataSet getSaleMaster_Formula(string div_code, int col_sno)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSale = null;
            strQry = "SELECT Col_Name, " +
                            " Dis_Mode, " +
                            " Order_By, " +
                            " Der_Formula,  " +
                            " SS_Entry_Done " +
                     " FROM Mas_Common_SS_Setup_Formula " +
                     " WHERE Division_Code = '" + div_code + "' " +
                     " AND Col_SNo = " + col_sno +
                     " ORDER BY 1";
            try
            {
                dsSale = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(0, ex.Message.ToString().Trim(), "Sec Sales Setup - Formula", "getSaleMaster_Formula()");
            }
            return dsSale;
        }

        public bool OrderByExists(string div_code, string order_by)
        {
            sCommand = new SqlCommand();
            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "select COUNT(Sl_No) from Mas_Sec_Sale_Setup where Order_by = '" + order_by + "' and Division_Code = '" + div_code + "' ";
                int iRecordExist = db.Exec_Scalar(strQry);

                if (iRecordExist > 0)
                    bRecordExist = true;
            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Additional Setup", "OrderByExists()");
            }
            return bRecordExist;
        }

        public bool OrderByExists_Formula(string div_code, string order_by)
        {
            sCommand = new SqlCommand();
            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "select COUNT(Col_SNo) from Mas_Common_SS_Setup_Formula where Order_by = '" + order_by + "' and Division_Code = '" + div_code + "' ";
                int iRecordExist = db.Exec_Scalar(strQry);

                if (iRecordExist > 0)
                    bRecordExist = true;
            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Additional Setup", "OrderByExists_Formula()");
            }
            return bRecordExist;
        }

        public DataSet Get_SecSaleCode_CustCol(string div_code, int col_sno)
        {
            DataSet ds = new DataSet();
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = " SELECT sec_sale_code " +
                         " FROM Mas_Sec_Sale_Param " +
                         " WHERE Division_Code = '" + div_code + "' " +
                         " AND Cust_Col_SNo = '" + col_sno + "' " +
                         " AND Sec_Sale_Sub_Name = 'cust_col' ";

                ds = db.Exec_DataSet(strQry);

            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Setup", "sRecordExists()");
            }
            return ds;
        }

        public bool FormulaExists_Entry(string div_code, int Cust_Col_SNo)
        {
            sCommand = new SqlCommand();
            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "select COUNT(Col_SNo) from Mas_Common_SS_Setup_Formula where SS_Entry_Done = 1 and Col_SNo = " + Cust_Col_SNo + " and Division_Code = '" + div_code + "' ";
                int iRecordExist = db.Exec_Scalar(strQry);

                if (iRecordExist > 0)
                    bRecordExist = true;
            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Additional Setup", "FormulaExists_Entry()");
            }
            return bRecordExist;
        }

        public bool FormulaExists_Param(string div_code, int Cust_Col_SNo)
        {
            sCommand = new SqlCommand();
            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "select COUNT(Sec_Sale_Code) from Mas_Sec_Sale_Param " + 
                          " where Sec_Sale_Sub_Name = 'cust_col' and Sel_Sale_Operator='D' " +
                          " and Cust_Col_SNo = " + Cust_Col_SNo + " and Division_Code = '" + div_code + "' ";

                int iRecordExist = db.Exec_Scalar(strQry);

                if (iRecordExist > 0)
                    bRecordExist = true;
            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Formula Setup", "FormulaExists_Param()");
            }
            return bRecordExist;
        }

        public DataSet getColSNo_Formula(string div_code, int sec_sale_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSale = null;
            strQry = "SELECT Cust_Col_SNo " +
                     " FROM Mas_Sec_Sale_Param " +
                     " WHERE Division_Code = '" + div_code + "' " +
                     " AND Sec_Sale_Code = " + sec_sale_code +
                     " ORDER BY 1";
            try
            {
                dsSale = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(0, ex.Message.ToString().Trim(), "Sec Sales Setup - Formula", "getColSNo_Formula()");
            }
            return dsSale;
        }

    }
}
