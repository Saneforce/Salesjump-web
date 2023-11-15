using Amazon.S3.Transfer;
using Amazon.S3;
using System;
using System.Data;
using System.IO;
using System.Net;
using System.Web;
using System.Web.Services;
using Amazon;

/// <summary>
/// Summary description for UploadService
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class UploadService : System.Web.Services.WebService
{


    public UploadService()
    {
        //Uncomment the following line if using designed components
        //InitializeComponent();
    }

    //[WebMethod]
    [WebMethod(EnableSession = true)]   
    public void UploadFiles()
    {
        string error = "";

        string divcode = Convert.ToString(Session["div_code"]);

        Listdrdetails ld = new Listdrdetails();

        DataSet dsDivision = ld.getStatePerDivision(divcode);
        string Folder = Convert.ToString(dsDivision.Tables[0].Rows[0]["Url_Short_Name"]);


        Folder = Folder.ToString().ToLower() + "_" + "Retailer";

        string path = HttpContext.Current.Server.MapPath("~/" + Folder + "/");

        //Create the Directory.
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        string currentDirectory = HttpContext.Current.Server.MapPath("~");
        string relativePath = Folder;
              

        //Fetch the File.
        HttpPostedFile postedFile = HttpContext.Current.Request.Files[0];

        //Fetch the File Name.
        string fileName = HttpContext.Current.Request.Form["fileName"] + Path.GetExtension(postedFile.FileName);

        //Save the File.
        postedFile.SaveAs(path + fileName);


        var awsKey = "AKIA5OS74MUCASG7HSCG";
        var awsSecretKey = "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";
        var bucketRegion = RegionEndpoint.APSouth1;

        string fileToBackup = currentDirectory + "/" + Folder + "/" + postedFile.FileName;

        //string fileToBackup = HttpContext.Current.Server.MapPath("~/" + path + "/") + postedFile.FileName; // test file path from the local computer
        string myBucketName = "happic"; // your s3 bucket name goes here
        string s3DirectoryName = path; // the directory path to a sub folder goes here
        string s3FileName = postedFile.FileName; // the name of the file when its saved into the S3 buscket

        try
        {
            // Upload the file to Amazon S3
            AmazonS3Client s3Client = new AmazonS3Client(awsKey, awsSecretKey, RegionEndpoint.APSouth1);

            TransferUtility fileTransferUtility = new TransferUtility(s3Client);
            if (s3DirectoryName == "" || s3DirectoryName == null)
            {
                //no subdirectory just bucket name
                fileTransferUtility.Upload(fileToBackup, myBucketName, postedFile.FileName);
            }
            else
            {
                // subdirectory and bucket name
                fileTransferUtility.Upload(fileToBackup, myBucketName + @"/" + s3DirectoryName, postedFile.FileName);
            }

            error += "" + postedFile.FileName.ToString() + "  " + " File uploaded to S3..." + "<br>";


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

        //Send OK Response to Client.
        HttpContext.Current.Response.StatusCode = (int)HttpStatusCode.OK;
        HttpContext.Current.Response.Write(fileName);
        HttpContext.Current.Response.Flush();
    }

}
