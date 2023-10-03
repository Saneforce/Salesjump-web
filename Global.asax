<%@ Application Codebehind="Global.asax.cs" Language="C#" %>

<script runat="server">
    void Application_Start(object sender, EventArgs e)
    {

        /*GlobalConfiguration.Configure(WebApiConfig.Register);
    AreaRegistration.RegisterAllAreas();
    RouteConfig.RegisterRoutes(RouteTable.Routes);*/


        // Code that runs on application startup
        /*System.Web.Http.HttpConfiguration config;
            RouteTable.Routes.MapHttpRoute(
            name: "DefaultApi",
            routeTemplate: "api/{controller}/{id}",
            defaults: new { id = System.Web.Http.RouteParameter.Optional }
        );*/
    }

    void Application_End(object sender, EventArgs e)
    {
        //  Code that runs on application shutdown

    }

    void Application_Error(object sender, EventArgs e)
    {
        // Code that runs when an unhandled error occurs

        string div_code = "";
        string sf_type = "";
        string sf_code = "";
        // string sf_name = "";
        // string desig_name = "";
        // string div_name = "";
        // string Corporate = "";
        // string Designation_Short_Name = "";
        //string HO_ID = "";


        string sf_hq = "";

        /* if (Session["div_code"] == null || Session["sf_type"] == null || Session["sf_code"] == null )
         {
             try
             {
                 div_code = Session["div_code"].ToString();
                 sf_type = Session["sf_type"].ToString();
                 sf_code = Session["sf_code"].ToString();
                 div_code = Session["division_code"].ToString();                            

             }
             catch (Exception ex)
             {
                // Response.Redirect("http://sanffa.info//Index.aspx");
                 Response.Redirect("http://fmcg.sanfmcg.com//Login.aspx");  
                 Response.Write(ex.Message);
             }
         }*/



    }

    void Session_Start(object sender, EventArgs e)
    {
        // Code that runs when a new session is started
        string sessionId = Session.SessionID;

        // Code that runs when a new session is started
        // Code that runs when a new session is started
        if (Session["div_code"] != null)
        {
            //Redirect to Welcome Page if Session is not null  
            Response.Redirect("Default.aspx");

        }
        else
        {
            //Redirect to Login Page if Session is null & Expires   
            //  Response.Redirect("Login.aspx");  
            string str = HttpContext.Current.Request.Url.OriginalString;
            if (str.ToLower().IndexOf("index") == -1)
            {
                Response.Redirect("~/Login.aspx");
            }
        }

    }

    void Session_End(object sender, EventArgs e)
    {
        // Code that runs when a session ends. 
        // Note: The Session_End event is raised only when the sessionstate mode
        // is set to InProc in the Web.config file. If session mode is set to StateServer 
        // or SQLServer, the event is not raised.



    }

</script>
