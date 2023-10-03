<%@ WebHandler Language="C#" Class="FileUpload" %>

using System;
using System.Web;
using System.IO;

public class FileUpload : IHttpHandler {

    public void ProcessRequest (HttpContext context) {
        if (context.Request.Files.Count > 0)
        {
            HttpFileCollection files = context.Request.Files;
            for (int i = 0; i < files.Count; i++)
            {
                HttpPostedFile file = files[i];
                string fname;
                if (HttpContext.Current.Request.Browser.Browser.ToUpper() == "IE" || HttpContext.Current.Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                {
                    string[] testfiles = file.FileName.Split(new char[] { '\\' });
                    fname = testfiles[testfiles.Length - 1];
                }
                else
                {
                    fname = file.FileName;
                }
                //string targetPath =  @"C:\\Mail\\File";
                //fname = Path.Combine(targetPath, fname);
                //string files1 = Directory.GetFiles(@"C:\Mail\File");
                //fname=Path.Combine(context.Server.MapPath(targetPath), fname);

                fname=Path.Combine(context.Server.MapPath("~/uploads/"), fname);
                file.SaveAs(fname);
                //file.SaveAs(files1);

            }
        }
        context.Response.ContentType = "text/plain";
        context.Response.Write("File Upload Successfully");
    }

    public bool IsReusable {
        get {
            return false;
        }
    }

}