using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;

namespace FMCGAPI.Controllers
{
    public class ProductController : ApiController
    {
        // GET api/<controller>
        private SqlConnection conn = new SqlConnection("data source=85.195.81.194, 10433;Initial catalog=FMCG_Live1;user id=sa;pwd=?u7aze143renumeM;");
        private SqlDataAdapter sqdata;
        [HttpGet]
        public string GetProducts()
        {
            DataTable dt = new DataTable();
            var sql = "select * from Mas_Product_Detail";
            sqdata = new SqlDataAdapter
            {
                SelectCommand = new SqlCommand(sql, conn)
            };
            sqdata.Fill(dt);
            return JsonConvert.SerializeObject(dt);
        }
        [HttpGet]
        public string GetCompanyProducts(int divcode)
        {
            DataTable dt = new DataTable();
            var sql = "select * from Mas_Product_Detail where Division_Code='" + divcode + "'";
            sqdata = new SqlDataAdapter
            {
                SelectCommand = new SqlCommand(sql, conn)
            };
            sqdata.Fill(dt);
            return JsonConvert.SerializeObject(dt);
        }
    }
}