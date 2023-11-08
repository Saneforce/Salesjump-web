<%@ WebHandler Language="C#" Class="CustomUploadHandler" %>

using System;
using System.Web;
using Amazon;
using Amazon.S3;
using Amazon.S3.Transfer;
using System.Data;
using System.IO;

public class CustomUploadHandler : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {

        string div_code = Convert.ToString(HttpContext.Current.Session["div_Code"]);

        string error = "";
        Listdrdetails ld = new Listdrdetails();

        DataSet dsDivision = ld.getStatePerDivision(div_code);
        string urlshotName = Convert.ToString(dsDivision.Tables[0].Rows[0]["Url_Short_Name"]);
        string directoryPath = urlshotName + "_" + "Retailer";
        string filepath = HttpContext.Current.Server.MapPath("~/" + directoryPath + "/");

        //Create the Directory.
        if (!Directory.Exists(filepath))
        {
            Directory.CreateDirectory(filepath);
        }

        HttpPostedFile file = context.Request.Files[0];
        string fname = HttpContext.Current.Server.MapPath("~/" + directoryPath + "/" + file.FileName);
        //string fname = context.Server.MapPath("~/" + directoryPath + "/" + file.FileName);
        file.SaveAs(fname);

        var awsKey = "AKIA5OS74MUCASG7HSCG";
        var awsSecretKey = "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";
        var bucketRegion = RegionEndpoint.APSouth1;

        string fileToBackup = HttpContext.Current.Server.MapPath("~/" + directoryPath + "/") + file.FileName; // test file path from the local computer
        string myBucketName = "happic"; // your s3 bucket name goes here
        string s3DirectoryName = directoryPath; // the directory path to a sub folder goes here
        string s3FileName = file.FileName; // the name of the file when its saved into the S3 buscket

        try
        {
            // Upload the file to Amazon S3
            AmazonS3Client s3Client = new AmazonS3Client(awsKey, awsSecretKey, RegionEndpoint.APSouth1);

            TransferUtility fileTransferUtility = new TransferUtility(s3Client);
            if (s3DirectoryName == "" || s3DirectoryName == null)
            {
                //no subdirectory just bucket name
                fileTransferUtility.Upload(fileToBackup, myBucketName, file.FileName);
            }
            else
            {
                // subdirectory and bucket name
                fileTransferUtility.Upload(fileToBackup, myBucketName + @"/" + s3DirectoryName, file.FileName);
            }

            error += "" + file.FileName.ToString() + "  " + " File uploaded to S3..." + "<br>";


        }
        catch (AmazonS3Exception e)
        {
            error += "Error encountered on server. Message:'{0}' when writing an object " + e.Message.ToString();

            //Console.WriteLine("Error encountered on server. Message:'{0}' when writing an object", e.Message);
        }
        catch (Exception e)
        {
            error += "Unknown encountered on server. Message:'{0}' when writing an object" + e.Message.ToString();
            // Console.WriteLine("Unknown encountered on server. Message:'{0}' when writing an object", e.Message);
        }

        //context.Response.ContentType = "text/plain";
        context.Response.Write(error.ToString());
        //context.Response.Write("File Uploaded Successfully!");

    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}