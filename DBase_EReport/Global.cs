using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DBase_EReport
{
    public static class Global
    {
        public static string ConnString;
        public static string ServerPath = "D:\\Websites\\SalesJump\\E-Report_DotNet\\";
        public static int counter;
        //Setting Variables
        public static string AttanceEdit;
        public static string ExpenseType;
        public static string AllowanceType;
        public static string HelloWorld()
        {
            return "Hello World";
        }
    }
}
