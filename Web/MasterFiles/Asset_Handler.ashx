<%@ WebHandler Language="C#" Class="Asset_Handler" %>

using System;
using System.Web;
using System.Net;
using System.Web.Script.Serialization;
using System.IO;

public class Asset_Handler : IHttpHandler {

    public void ProcessRequest (HttpContext context) {
        if (context.Request.Files.Count > 0)
        {

                //Fetch the Uploaded File.
                HttpPostedFile postedFile = context.Request.Files[0];

                //Set the Folder Path.
                string folderPath = context.Server.MapPath("../Asset_Imags/");

                //Set the File Name.
                string fileName = Path.GetFileName(postedFile.FileName);

                //Save the File in Folder.
                postedFile.SaveAs(folderPath + fileName);
                
                string json = new JavaScriptSerializer().Serialize(
                new
                {
                    name = fileName
                });
                context.Response.StatusCode = (int)HttpStatusCode.OK;
                context.Response.ContentType = "text/json";
                context.Response.Write(json);


            //Send File details in a JSON Response.

            context.Response.End();
        }
        //context.Response.ContentType = "text/plain";
        //context.Response.Write("Hello World");
    }

    public bool IsReusable {
        get {
            return false;
        }
    }

}