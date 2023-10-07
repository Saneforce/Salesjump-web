using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for UploadDetail
/// </summary>
public class UploadDetail
{
    public bool IsReady { get; set; }
    public int ContentLength { get; set; }
    public int UploadedLength { get; set; }
    public string FileName { get; set; }
	public UploadDetail()
	{
		//
		// TODO: Add constructor logic here
		//
	}
}