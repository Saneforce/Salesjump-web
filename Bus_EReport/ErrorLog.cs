using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DBase_EReport;
using System.Data.SqlClient;

namespace Bus_EReport
{
    public class ErrorLog
    {
        #region "Variable Declarations"
        private string strQry = string.Empty;
        SqlCommand comm;
        string sError = string.Empty;
        #endregion


        public int LogError(int div_code, string Err_Desc, string Source_Screen, string Err_Method)
        {
            int iReturn = -1;

            try
            {
                DB_EReporting db = new DB_EReporting();
                comm = new SqlCommand();
                comm.Parameters.Clear();

                SqlParameter param_div_code = new SqlParameter();
                param_div_code.ParameterName = "@Division_Code";    // Defining Name
                param_div_code.SqlDbType = SqlDbType.Int;           // Defining DataType
                param_div_code.Direction = ParameterDirection.Input;// Setting the direction 

                SqlParameter param_Err_Desc = new SqlParameter();
                param_Err_Desc.ParameterName = "@Err_Desc";    // Defining Name
                param_Err_Desc.SqlDbType = SqlDbType.VarChar;           // Defining DataType
                param_Err_Desc.Direction = ParameterDirection.Input;// Setting the direction 

                SqlParameter param_Source_Screen = new SqlParameter();
                param_Source_Screen.ParameterName = "@Source_Screen";    // Defining Name
                param_Source_Screen.SqlDbType = SqlDbType.VarChar;           // Defining DataType
                param_Source_Screen.Direction = ParameterDirection.Input;// Setting the direction 

                SqlParameter param_Err_Method = new SqlParameter();
                param_Err_Method.ParameterName = "@Err_Method";    // Defining Name
                param_Err_Method.SqlDbType = SqlDbType.VarChar;           // Defining DataType
                param_Err_Method.Direction = ParameterDirection.Input;// Setting the direction 

                // Adding Parameter instances to sqlcommand

                comm.Parameters.Add(param_div_code);
                comm.Parameters.Add(param_Err_Desc);
                comm.Parameters.Add(param_Source_Screen);
                comm.Parameters.Add(param_Err_Method);

                // Setting values of Parameter
                param_div_code.Value = div_code;
                param_Err_Desc.Value = Err_Desc;
                param_Source_Screen.Value = Source_Screen;
                param_Err_Method.Value = Err_Method;

                strQry = "INSERT INTO error_log (Err_Desc,Source_Screen,Err_Method,Division_Code) VALUES "
                    + " ( @Err_Desc, @Source_Screen, @Err_Method, @Division_Code)";

                iReturn = db.ExecQry(strQry, comm);

            }
            catch (Exception ex)
            {
                sError += ex.Message.ToString().Trim();
            }

            return iReturn;
        }    
    }

}
