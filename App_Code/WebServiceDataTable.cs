using Bus_EReport;
using DBase_EReport;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;


/// <summary>
/// Summary description for WebServiceDataTable
/// </summary>

[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
//[System.ComponentModel.ToolboxItem(false)]

// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
[System.Web.Script.Services.ScriptService]


public class WebServiceDataTable : System.Web.Services.WebService
{
    public WebServiceDataTable()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string HelloWorld()
    {
        return "Hello World";
    }



    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public List<SudivList> GetSubDivision(string div_code)
    {
        SalesForce sd = new SalesForce();
        DataSet dsSalesForce = sd.Getsubdivisionwise(div_code);
        List<SudivList> customer = new List<SudivList>();
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            for (int k = 0; k < dsSalesForce.Tables[0].Rows.Count; k++)
            {
                customer.Add(new SudivList
                {
                    subdivision_code = Convert.ToInt32(dsSalesForce.Tables[0].Rows[k]["subdivision_code"]),
                    subdivision_name = Convert.ToString(dsSalesForce.Tables[0].Rows[k]["subdivision_name"])
                });
            }
        }

        //string str = new JavaScriptSerializer().Serialize(customer);
        //return str;

        return customer;

        //return string.Format("Name: {0}{2}Age: {1}{2}TimeStamp: {3}", name, age, Environment.NewLine, DateTime.Now.ToString());
    }


    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public List<yearlist> FillYear(string div_code)
    {

        TourPlan tp = new TourPlan();
        DataSet dsTP = tp.Get_TP_Edit_Year(div_code);
        List<yearlist> customer = new List<yearlist>();

        if (dsTP.Tables[0].Rows.Count > 0)
        {
            for (int k = Convert.ToInt16(dsTP.Tables[0].Rows[0]["Year"]); k <= DateTime.Now.Year + 1; k++)
            {
                customer.Add(new yearlist
                {
                    Value = k.ToString(),
                    Text = k.ToString()
                });
            }
        }       

        return customer;
    }


    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public List<UserList> FillMRManagers(string divcode, string sfcode, string SubDiv)
    {
        if (sfcode == null || sfcode == "")
        { sfcode = "admin"; }

        if (SubDiv == null || SubDiv == "")
        { SubDiv = "0"; }


        SalesForce sf = new SalesForce();

        DataSet dsSalesForce = sf.SalesForceListMgrGet_MgrOnly(divcode, sfcode, SubDiv);

        List<UserList> customer = new List<UserList>();

        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            for (int k = 0; k < dsSalesForce.Tables[0].Rows.Count; k++)
            {
                customer.Add(new UserList
                {
                    sf_Code = Convert.ToString(dsSalesForce.Tables[0].Rows[k]["sf_code"]),
                    sf_name = Convert.ToString(dsSalesForce.Tables[0].Rows[k]["sf_name"])
                });
            }
        }

        //string str = new JavaScriptSerializer().Serialize(customer);
        //return str;

        return customer;
    }

    //[ScriptMethod(ResponseFormat = ResponseFormat.Json, XmlSerializeString = false)]
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void GetDataForDataTable1()
    {

        Int32 ajaxDraw = Convert.ToInt32(HttpContext.Current.Request.Form["draw"]);

        Int32 OffsetValue = Convert.ToInt32(HttpContext.Current.Request.Form["start"]);

        Int32 PagingSize = Convert.ToInt32(HttpContext.Current.Request.Form["length"]);

        string searchby = HttpContext.Current.Request.Form["search[value]"];

        string sortColumn = HttpContext.Current.Request.Form["order[0][column]"];

        string sortOrder = HttpContext.Current.Request.Form["order[0][dir]"];

        if (searchby == null)
        { searchby = ""; }

        if (OffsetValue == 0)
        { OffsetValue = 1; }

        if (PagingSize == 0)
        { PagingSize = 1000; }

        if (sortOrder == null || sortOrder == "")
        { sortOrder = "ASC"; }

        if (sortColumn == null || sortColumn == "")
        { sortColumn = "Id"; }

        string sfcode = HttpContext.Current.Request.Form["sfcode"].ToString();
        string divcode = HttpContext.Current.Request.Form["divcode"].ToString(); //Convert.ToString(HttpContext.Current.Session["div_code"]);
        string subdiv = HttpContext.Current.Request.Form["subdiv"].ToString();
        string years = HttpContext.Current.Request.Form["years"].ToString(); 

        string strQry = " EXEC getCusDetails1 '" + sfcode + "','" + divcode + "', '" + subdiv + "','" + years + "'";
        strQry += " ,'" + searchby + "'," + OffsetValue + "," + PagingSize + ",'" + sortOrder + "','" + sortColumn + "'";



        DB_EReporting dbER = new DB_EReporting();
        DataTable dt = new DataTable();
        dt = dbER.Exec_DataTable(strQry);

        //DataTable dt = pv.spCustomer_Details(Sf_Code, divcode, subdiv, year, searchby, OffsetValue, PagingSize, sortDirection, sortColumn);

        Int32 recordTotal = 0;

        List<CustomerDetails> customer = new List<CustomerDetails>();

        //Binding the Data from datatable to the List

        if (dt != null)

        {

            for (int i = 0; i < dt.Rows.Count; i++)

            {

                CustomerDetails cusd = new CustomerDetails();
                cusd.Id = Convert.IsDBNull(dt.Rows[i]["Id"]) ? default(int) : Convert.ToInt32(dt.Rows[i]["Id"]);
                cusd.Customer_Code = Convert.IsDBNull(dt.Rows[i]["Customer_Code"]) ? default(string) : Convert.ToString(dt.Rows[i]["Customer_Code"]);
                cusd.Customer_Name = Convert.IsDBNull(dt.Rows[i]["Customer_Name"]) ? default(string) : Convert.ToString(dt.Rows[i]["Customer_Name"]);
                cusd.Channel = Convert.IsDBNull(dt.Rows[i]["Channel"]) ? default(string) : Convert.ToString(dt.Rows[i]["Channel"]);
                cusd.Category = Convert.IsDBNull(dt.Rows[i]["Category"]) ? default(string) : Convert.ToString(dt.Rows[i]["Category"]);
                cusd.Phone = Convert.IsDBNull(dt.Rows[i]["Phone"]) ? default(string) : Convert.ToString(dt.Rows[i]["Phone"]);
                cusd.Address = Convert.IsDBNull(dt.Rows[i]["Address"]) ? default(string) : Convert.ToString(dt.Rows[i]["Address"]);
                cusd.Route = Convert.IsDBNull(dt.Rows[i]["Route"]) ? default(string) : Convert.ToString(dt.Rows[i]["Route"]);
                cusd.HQ = Convert.IsDBNull(dt.Rows[i]["HQ"]) ? default(string) : Convert.ToString(dt.Rows[i]["HQ"]);

                cusd.Janv = Convert.IsDBNull(dt.Rows[i]["Janv"]) ? default(string) : Convert.ToString(dt.Rows[i]["Janv"]);
                cusd.Febv = Convert.IsDBNull(dt.Rows[i]["Febv"]) ? default(string) : Convert.ToString(dt.Rows[i]["Febv"]);
                cusd.Marv = Convert.IsDBNull(dt.Rows[i]["Marv"]) ? default(string) : Convert.ToString(dt.Rows[i]["Marv"]);
                cusd.Aprv = Convert.IsDBNull(dt.Rows[i]["Aprv"]) ? default(string) : Convert.ToString(dt.Rows[i]["Aprv"]);
                cusd.Mayv = Convert.IsDBNull(dt.Rows[i]["Mayv"]) ? default(string) : Convert.ToString(dt.Rows[i]["Mayv"]);
                cusd.Junv = Convert.IsDBNull(dt.Rows[i]["Junv"]) ? default(string) : Convert.ToString(dt.Rows[i]["Junv"]);
                cusd.Julv = Convert.IsDBNull(dt.Rows[i]["Julv"]) ? default(string) : Convert.ToString(dt.Rows[i]["Julv"]);
                cusd.Augv = Convert.IsDBNull(dt.Rows[i]["Augv"]) ? default(string) : Convert.ToString(dt.Rows[i]["Augv"]);
                cusd.Sepv = Convert.IsDBNull(dt.Rows[i]["Sepv"]) ? default(string) : Convert.ToString(dt.Rows[i]["Sepv"]);
                cusd.Octv = Convert.IsDBNull(dt.Rows[i]["Octv"]) ? default(string) : Convert.ToString(dt.Rows[i]["Octv"]);
                cusd.Novv = Convert.IsDBNull(dt.Rows[i]["Novv"]) ? default(string) : Convert.ToString(dt.Rows[i]["Novv"]);
                cusd.Decv = Convert.IsDBNull(dt.Rows[i]["Decv"]) ? default(string) : Convert.ToString(dt.Rows[i]["Decv"]);

                cusd.Jan = Convert.IsDBNull(dt.Rows[i]["Jan"]) ? default(string) : Convert.ToString(dt.Rows[i]["Jan"]);
                cusd.Feb = Convert.IsDBNull(dt.Rows[i]["Feb"]) ? default(string) : Convert.ToString(dt.Rows[i]["Feb"]);
                cusd.Mar = Convert.IsDBNull(dt.Rows[i]["Mar"]) ? default(string) : Convert.ToString(dt.Rows[i]["Mar"]);
                cusd.Apr = Convert.IsDBNull(dt.Rows[i]["Apr"]) ? default(string) : Convert.ToString(dt.Rows[i]["Apr"]);
                cusd.May = Convert.IsDBNull(dt.Rows[i]["May"]) ? default(string) : Convert.ToString(dt.Rows[i]["May"]);
                cusd.Jun = Convert.IsDBNull(dt.Rows[i]["Jun"]) ? default(string) : Convert.ToString(dt.Rows[i]["Jun"]);
                cusd.Jul = Convert.IsDBNull(dt.Rows[i]["Jul"]) ? default(string) : Convert.ToString(dt.Rows[i]["Jul"]);
                cusd.Aug = Convert.IsDBNull(dt.Rows[i]["Aug"]) ? default(string) : Convert.ToString(dt.Rows[i]["Aug"]);
                cusd.Sep = Convert.IsDBNull(dt.Rows[i]["Sep"]) ? default(string) : Convert.ToString(dt.Rows[i]["Sep"]);
                cusd.Oct = Convert.IsDBNull(dt.Rows[i]["Oct"]) ? default(string) : Convert.ToString(dt.Rows[i]["Oct"]);
                cusd.Nov = Convert.IsDBNull(dt.Rows[i]["Nov"]) ? default(string) : Convert.ToString(dt.Rows[i]["Nov"]);
                cusd.Dec = Convert.IsDBNull(dt.Rows[i]["Dec"]) ? default(string) : Convert.ToString(dt.Rows[i]["Dec"]);
                cusd.Total = Convert.IsDBNull(dt.Rows[i]["Total"]) ? default(string) : Convert.ToString(dt.Rows[i]["Total"]);

                customer.Add(cusd);

            }

            recordTotal = dt.Rows.Count > 0 ? Convert.ToInt32(dt.Rows[0]["FilterTotalCount"]) : 0;

        }

        Int32 recordFiltered = recordTotal;

        List<DataTableResponse> objres = new List<DataTableResponse>();
        DataTableResponse obj_res = new DataTableResponse();

        obj_res.draw = ajaxDraw;
        obj_res.recordsFiltered = recordTotal;
        obj_res.recordsTotal = recordTotal;
        obj_res.data = customer;

        objres.Add(obj_res);


        DataTableResponse objDataTableResponse = new DataTableResponse()
        {
            
            draw = ajaxDraw,

            recordsFiltered = recordTotal,

            recordsTotal = recordTotal,

            data = customer

        };


        var result = new
        {
            draw = ajaxDraw,

            recordsFiltered = recordTotal,

            recordsTotal = recordTotal,

            data = customer
        };

        //return Json(result, JsonRequestBehavior.AllowGet);

        var Json = Newtonsoft.Json.JsonConvert.SerializeObject(result);
        this.Context.Response.Clear();
        this.Context.Response.ContentType = "application/json; charset=UTF-8";
        this.Context.Response.Write(new JavaScriptSerializer().Serialize(result));
        

        //return Json;

        //return objres;

        //return  new JavaScriptSerializer().Serialize(result);
    }





    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string GetDataForDataTable(string parameter)
    {
        string[] pram = parameter.Split('_');

        string sfcode = pram[0].ToString();
        string divcode = pram[1].ToString();
        string subdiv = pram[2].ToString();
        string years = pram[3].ToString();


        //HttpContext context = HttpContext.CurrentHandler.ToString();

        Int32 ajaxDraw = Convert.ToInt32(HttpContext.Current.Request.Form["draw"]);

        Int32 OffsetValue = Convert.ToInt32(HttpContext.Current.Request.Form["start"]);

        Int32 PagingSize = Convert.ToInt32(HttpContext.Current.Request.Form["length"]);

        string searchby = HttpContext.Current.Request.Form["search[value]"];

        string sortColumn = HttpContext.Current.Request.Form["order[0][column]"];

        string sortOrder = HttpContext.Current.Request.Form["order[0][dir]"];

        if (searchby == null)
        { searchby = ""; }

        if (OffsetValue == 0)
        { OffsetValue = 1; }

        if (PagingSize == 0)
        { PagingSize = 1000; }
      
        if (sortOrder == null || sortOrder == "")
        { sortOrder = "ASC"; }

        if (sortColumn == null || sortColumn == "")
        { sortColumn = "Id"; }

        string strQry = " EXEC getCusDetails1 '" + sfcode + "','" + divcode + "', '" + subdiv + "','" + years + "'";
        strQry += " ,'" + searchby + "'," + OffsetValue + "," + PagingSize + ",'" + sortOrder + "','" + sortColumn + "'";

       

        DB_EReporting dbER = new DB_EReporting();
        DataTable dt = new DataTable();
        dt = dbER.Exec_DataTable(strQry);

        //DataTable dt = pv.spCustomer_Details(Sf_Code, divcode, subdiv, year, searchby, OffsetValue, PagingSize, sortDirection, sortColumn);

        Int32 recordTotal = 0;

        List<CustomerDetails> customer = new List<CustomerDetails>();

        //Binding the Data from datatable to the List

        if (dt != null)
        {

            for (int i = 0; i < dt.Rows.Count; i++)

            {

                CustomerDetails cusd = new CustomerDetails();
                cusd.Id = Convert.IsDBNull(dt.Rows[i]["Id"]) ? default(int) : Convert.ToInt32(dt.Rows[i]["Id"]);
                cusd.Customer_Code = Convert.IsDBNull(dt.Rows[i]["Customer_Code"]) ? default(string) : Convert.ToString(dt.Rows[i]["Customer_Code"]);
                cusd.Customer_Name = Convert.IsDBNull(dt.Rows[i]["Customer_Name"]) ? default(string) : Convert.ToString(dt.Rows[i]["Customer_Name"]);
                cusd.Channel = Convert.IsDBNull(dt.Rows[i]["Channel"]) ? default(string) : Convert.ToString(dt.Rows[i]["Channel"]);
                cusd.Category = Convert.IsDBNull(dt.Rows[i]["Category"]) ? default(string) : Convert.ToString(dt.Rows[i]["Category"]);
                cusd.Phone = Convert.IsDBNull(dt.Rows[i]["Phone"]) ? default(string) : Convert.ToString(dt.Rows[i]["Phone"]);
                cusd.Address = Convert.IsDBNull(dt.Rows[i]["Address"]) ? default(string) : Convert.ToString(dt.Rows[i]["Address"]);
                cusd.Route = Convert.IsDBNull(dt.Rows[i]["Route"]) ? default(string) : Convert.ToString(dt.Rows[i]["Route"]);
                cusd.HQ = Convert.IsDBNull(dt.Rows[i]["HQ"]) ? default(string) : Convert.ToString(dt.Rows[i]["HQ"]);

                cusd.Janv = Convert.IsDBNull(dt.Rows[i]["Janv"]) ? default(string) : Convert.ToString(dt.Rows[i]["Janv"]);
                cusd.Febv = Convert.IsDBNull(dt.Rows[i]["Febv"]) ? default(string) : Convert.ToString(dt.Rows[i]["Febv"]);
                cusd.Marv = Convert.IsDBNull(dt.Rows[i]["Marv"]) ? default(string) : Convert.ToString(dt.Rows[i]["Marv"]);
                cusd.Aprv = Convert.IsDBNull(dt.Rows[i]["Aprv"]) ? default(string) : Convert.ToString(dt.Rows[i]["Aprv"]);
                cusd.Mayv = Convert.IsDBNull(dt.Rows[i]["Mayv"]) ? default(string) : Convert.ToString(dt.Rows[i]["Mayv"]);
                cusd.Junv = Convert.IsDBNull(dt.Rows[i]["Junv"]) ? default(string) : Convert.ToString(dt.Rows[i]["Junv"]);
                cusd.Julv = Convert.IsDBNull(dt.Rows[i]["Julv"]) ? default(string) : Convert.ToString(dt.Rows[i]["Julv"]);
                cusd.Augv = Convert.IsDBNull(dt.Rows[i]["Augv"]) ? default(string) : Convert.ToString(dt.Rows[i]["Augv"]);
                cusd.Sepv = Convert.IsDBNull(dt.Rows[i]["Sepv"]) ? default(string) : Convert.ToString(dt.Rows[i]["Sepv"]);
                cusd.Octv = Convert.IsDBNull(dt.Rows[i]["Octv"]) ? default(string) : Convert.ToString(dt.Rows[i]["Octv"]);
                cusd.Novv = Convert.IsDBNull(dt.Rows[i]["Novv"]) ? default(string) : Convert.ToString(dt.Rows[i]["Novv"]);
                cusd.Decv = Convert.IsDBNull(dt.Rows[i]["Decv"]) ? default(string) : Convert.ToString(dt.Rows[i]["Decv"]);

                cusd.Jan = Convert.IsDBNull(dt.Rows[i]["Jan"]) ? default(string) : Convert.ToString(dt.Rows[i]["Jan"]);
                cusd.Feb = Convert.IsDBNull(dt.Rows[i]["Feb"]) ? default(string) : Convert.ToString(dt.Rows[i]["Feb"]);
                cusd.Mar = Convert.IsDBNull(dt.Rows[i]["Mar"]) ? default(string) : Convert.ToString(dt.Rows[i]["Mar"]);
                cusd.Apr = Convert.IsDBNull(dt.Rows[i]["Apr"]) ? default(string) : Convert.ToString(dt.Rows[i]["Apr"]);
                cusd.May = Convert.IsDBNull(dt.Rows[i]["May"]) ? default(string) : Convert.ToString(dt.Rows[i]["May"]);
                cusd.Jun = Convert.IsDBNull(dt.Rows[i]["Jun"]) ? default(string) : Convert.ToString(dt.Rows[i]["Jun"]);
                cusd.Jul = Convert.IsDBNull(dt.Rows[i]["Jul"]) ? default(string) : Convert.ToString(dt.Rows[i]["Jul"]);
                cusd.Aug = Convert.IsDBNull(dt.Rows[i]["Aug"]) ? default(string) : Convert.ToString(dt.Rows[i]["Aug"]);
                cusd.Sep = Convert.IsDBNull(dt.Rows[i]["Sep"]) ? default(string) : Convert.ToString(dt.Rows[i]["Sep"]);
                cusd.Oct = Convert.IsDBNull(dt.Rows[i]["Oct"]) ? default(string) : Convert.ToString(dt.Rows[i]["Oct"]);
                cusd.Nov = Convert.IsDBNull(dt.Rows[i]["Nov"]) ? default(string) : Convert.ToString(dt.Rows[i]["Nov"]);
                cusd.Dec = Convert.IsDBNull(dt.Rows[i]["Dec"]) ? default(string) : Convert.ToString(dt.Rows[i]["Dec"]);
                cusd.Total = Convert.IsDBNull(dt.Rows[i]["Total"]) ? default(string) : Convert.ToString(dt.Rows[i]["Total"]);

                customer.Add(cusd);

            }

            recordTotal = dt.Rows.Count > 0 ? Convert.ToInt32(dt.Rows[0]["FilterTotalCount"]) : 0;

        }

        Int32 recordFiltered = recordTotal;

        var result = new
        {
            draw = ajaxDraw,

            recordsFiltered = recordTotal,

            recordsTotal = recordTotal,

            data = customer
        };

        //return Json(result, JsonRequestBehavior.AllowGet);

        return new JavaScriptSerializer().Serialize(result);

        //string str = new JavaScriptSerializer().Serialize(dataTableResponses);
        //return str;


        //DataTableResponse objDataTableResponse = new DataTableResponse()
        //{

        //    draw = ajaxDraw,

        //    recordsFiltered = recordTotal,

        //    recordsTotal = recordTotal,

        //    data = customer

        //};

        ////writing the response


        //return dataTableResponses;

        //context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(objDataTableResponse));

    }

    

}
