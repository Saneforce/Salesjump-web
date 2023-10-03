using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace DBase_EReport
{
    public class ExpenseDL
    {

        public string SaveDistanceDetails(string SF, string xml)
        {
            DB_EReporting DL = new DB_EReporting();
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@SF", SF),  
                    new SqlParameter("@DisDet", xml)
                };
                DL.Exec_NonQueryWithParam("svMasDistance", CommandType.StoredProcedure, parameters);
                return "{'success':true}";
            }
            catch { throw; }
            finally { DL = null; }
        }
    }
}
