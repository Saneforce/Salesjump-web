<%@ WebHandler Language="C#" Class="proposal_Handler" %>

using System;
using System.IO;
using System.Net;
using System.Web;
using System.Web.Script.Serialization;

public class proposal_Handler : IHttpHandler {

    public void ProcessRequest (HttpContext context) {

        if (context.Request.Files.Count > 0)
        {
            //Fetch the Uploaded File.
            HttpPostedFile postedFile = context.Request.Files["filename"];
           
            //Set the Folder Path.
            string folderPath = context.Server.MapPath("../Proposals/");
 
            //Set the File Name.
            string fileName = Path.GetFileName(postedFile.FileName);
           
            //Save the File in Folder.
            postedFile.SaveAs(folderPath + fileName);
 
            //Send File details in a JSON Response.
            string json = new JavaScriptSerializer().Serialize(
                new
                {
                    name = fileName
                });
            context.Response.StatusCode = (int)HttpStatusCode.OK;
            context.Response.ContentType = "text/json";
            context.Response.Write(json);
            context.Response.End();
        }
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}