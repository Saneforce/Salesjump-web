using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DBase_EReport;
using DBase_EReport;
using System.Data.SqlClient;

namespace Bus_EReport
{
    /// <summary>
    /// Summary description for Employee
    /// </summary>
    public class Employee
    {
        public string FName { get; set; }
        public string LName { get; set; }
        public string EmailId { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}