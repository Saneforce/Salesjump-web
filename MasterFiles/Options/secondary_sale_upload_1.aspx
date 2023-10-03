<%@ Page Title="Closing Upload" Language="C#" AutoEventWireup="true" MasterPageFile="~/Master.master" CodeFile="secondary_sale_upload_1.aspx.cs"
    Inherits="MasterFiles_Options_sec_sale_upload" %>
    <asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<%--<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>--%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Upload</title>
    <style type="text/css">
        .modal
        {
            position: fixed;
            top: 0;
            left: 0;
            background-color: black;
            z-index: 99;
            opacity: 0.8;
            filter: alpha(opacity=80);
            -moz-opacity: 0.8;
            min-height: 100%;
            width: 100%;
        }
        .loading
        {
            font-family: Arial;
            font-size: 10pt;
            border: 5px solid #67CFF5;
            width: 200px;
            height: 100px;
            display: none;
            position: fixed;
            background-color: White;
            z-index: 999;
        }
        #divdr
        {
            font-family: Verdana;
            font-size: small;
            font-weight: bold;
            padding: 2px;
            border: solid 1px;
            width: 130px;
        }
        #detail
        {
            font-family: Verdana;
            font-size: Smaller;
            border: solid 1px;
            width: 130px;
            padding: 2px;
        }
          #divcat
        {
            font-family: Verdana;
            font-size: small;
            font-weight: bold;
            padding: 2px;
            border: solid 1px;
            width: 90px;
        }
        #detailcat
        {
            font-family: Verdana;
            font-size: Smaller;
            border: solid 1px;
            width: 90px;
            padding: 2px;
        }
            #divTerr
        {
            font-family: Verdana;
            font-size: small;
            font-weight: bold;
            padding: 2px;
            border: solid 1px;
            width: 120px;
        }
        #detailTerr
        {
            font-family: Verdana;
            font-size: Smaller;
            border: solid 1px;
            width: 120px;
            padding: 2px;
        }
     
            #divcls
        {
            font-family: Verdana;
            font-size: small;
            font-weight: bold;
            padding: 2px;
            border: solid 1px;
            width: 120px;
        }
        #detailCls
        {
            font-family: Verdana;
            font-size: Smaller;
            border: solid 1px;
            width: 120px;
            padding: 2px;
        }
        
    </style>
   
    <link type="text/css" rel="stylesheet" href="../../css/style.css" />
</head>
<body>
    <form id="form1" runat="server">
    <%--<ucl:Menu ID="menu1" runat="server" />--%>
    <br />
    <asp:Panel ID="pnlDoctor" Width="90%" runat="server">
        <table width="100%" style="margin-left: 10%">
            <tr>
                <td>
                    <table width="95%" cellpadding="5px" cellspacing="5px" style="border: 1px solid Black; background-color:White">
                      
      
                        <tr>
                            <td style="padding-right:auto">
                          
                          
                           
                            <asp:Label ID="lblExcel" runat="server" class="pad HDBg" style="position:relative;color: #336277;font-family: Verdana;font-weight: bold;text-transform: capitalize;font-size: 14px;">Excel file</asp:Label>
                              <br />
                                <asp:FileUpload ID="FlUploadcsv" runat="server" style="padding-left:20px;"  />
                            </td>
                        </tr>
                       
                      
                        <tr>
                        
                            <td align="center">
                                <asp:ImageButton ID="Button1" runat="server"  Width="100px" Height="30px" ImageUrl="~/Images/button_upload.jpg" Text="Upload" OnClick="btnUpload_Click" />
                               
                                   
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td style="border: 1px solid Black; padding-left:80px; ">
                                <asp:Label ID="lblIns" runat="server" Text="Upload File Details:" ForeColor="Red"></asp:Label>
                           <br />
                                <asp:Label ID="Label1" Font-Size="11px" Font-Names="Verdana" runat="server" Width="280px" Text="1) File Name:"></asp:Label>
                                <br />
                                <asp:Label ID="Label2" Font-Size="11px" Font-Names="Verdana" runat="server" Text="2) File size:"></asp:Label>
                                <br />
                                <asp:Label ID="Label3" Font-Size="11px" Font-Names="Verdana" runat="server" Text="3) File Analysis:"></asp:Label>
                                <br />
                                <asp:Label ID="Label4" Font-Size="11px" Font-Names="Verdana" runat="server" Text="4) Status:"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                               <asp:Label ID="lblExc" runat="server" Text="Excel Format File" style="position:relative;color: #336277;font-family: Verdana;font-weight: bold;text-transform: capitalize;font-size: 12px;"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                
                                <asp:ImageButton ID="lnkDownload" runat="server" ImageUrl="~/Images/button_download-here.png" OnClick="lnkDownload_Click" />
                               
                            </td>
                        </tr>
                    </table>
                   
                    <td width="20%" style="border: 1px solid Black; background-color:White;padding-top:auto">
                     <asp:Label ID="Label5" runat="server" Text="Upload File Details:" ForeColor="Red"></asp:Label>
                    </td>
                  
                    </td>
              
            </tr>
        </table>
    </asp:Panel>
  
    </form>
</body>
</html>
</asp:Content>