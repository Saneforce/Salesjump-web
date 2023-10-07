<%@ WebHandler Language="C#" Class="UploadHandler" %>

using System;
using System.Web;

public class UploadHandler : IHttpHandler {

    public void ProcessRequest (HttpContext context) {
        //if (context.Request.Files.Count > 0)
        //    {
        //        HttpFileCollection UploadedFilesCollection = context.Request.Files;
        //        for (int i = 0; i < UploadedFilesCollection.Count; i++)
        //        {
        //            System.Threading.Thread.Sleep(2000);
        //            HttpPostedFile PostedFiles = UploadedFilesCollection[i];
        //            string FilePath = context.Server.MapPath("~/uploads/" + System.IO.Path.GetFileName(PostedFiles.FileName));
        //            PostedFiles.SaveAs(FilePath);
        //        }
        //    }
        //context.Response.ContentType = "text/plain";
        //context.Response.Write("File Upload Successfully");

        HttpPostedFile file = context.Request.Files[0];
        string fname = context.Server.MapPath("~/uploads/" + file.FileName);
        file.SaveAs(fname);
        context.Response.ContentType = "text/plain";
        context.Response.Write("File Uploaded Successfully!");
    }

    public bool IsReusable {
        get {
            return false;
        }
    }

}