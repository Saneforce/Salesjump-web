<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="channel_wise.aspx.cs" Inherits="MIS_Reports_channel_wise" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Channel Wise Report</title>
        
    <script type="text/javascript" src="../../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../../JsFiles/jquery-1.10.1.js"></script>
         <link type="text/css" rel="stylesheet" href="../../css/style.css" />
    <style type="text/css">
        #tblDocRpt
        {
            margin-left: 300px;
        }
    </style>
         <script type="text/jscript">
        $(document).ready(function () {
 			document.getElementById('txtDate').valueAsDate = new Date();
document.getElementById('txtFromDate').valueAsDate = new Date();
           
        });
    </script>
        </head>
    <body>
       <form id="form1" runat="server">
  
    <div>
        <div id="Divid" runat="server">

        <br />
        </div>
        <center>
            <table cellpadding="0" cellspacing="5">
                <tr>
                    <td align="left" class="stylespc">
                        <asp:Label ID="Label8" runat="server" SkinID="lblMand" Text="FieldForce" ></asp:Label>
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="ddlFieldForce" runat="server" SkinID="ddlRequired">
                        </asp:DropDownList>
                    </td>
                    </tr>
                <tr>
                    <td align="left" class="stylespc">
                    <asp:Label ID="Label3"  runat="server" SkinID="lblMand" Text="Select Channel "></asp:Label>                       
                    </td>
                    <td align="left">                        
                        <asp:DropDownList ID="ddlchl" runat="server" SkinID="ddlRequired" >
                        </asp:DropDownList>
                    </td>
                </tr>
           
   <tr>
                    <td align="left" class="stylespc">
                        <asp:Label ID="Label5" runat="server" SkinID="lblMand" Text="FromDate" Visible="true"></asp:Label>
                    </td>
                    <td align="left">
                     <input id="txtFromDate" name="txtFrom1" type="date"  CssClass="TEXTAREA" MaxLength="5"
                            onfocus="this.style.backgroundColor='#E0EE9D'" onblur="this.style.backgroundColor='White'"
                            onkeypress="AlphaNumeric_NoSpecialChars(event);" TabIndex="1" SkinID="MandTxtBox" />
                       </td>
                      </tr>
                <tr>
                    <td align="left" class="stylespc">
                        <asp:Label ID="Label1" runat="server" SkinID="lblMand" Text="Date" Visible="true"></asp:Label>
                    </td>
                    <td align="left">
                     <input id="txtDate" name="txtFrom" type="date"  CssClass="TEXTAREA" MaxLength="5"
                            onfocus="this.style.backgroundColor='#E0EE9D'" onblur="this.style.backgroundColor='White'"
                            onkeypress="AlphaNumeric_NoSpecialChars(event);" TabIndex="1" SkinID="MandTxtBox" />
                      
                    </td>
                </tr>               
            </table>
            <br />
            <br />
            <asp:Button ID="btnSubmit" runat="server" Width="60px" Height="25px" 
                Text="View" CssClass="BUTTON" onclick="btnSubmit_Click1"
                 />
           
    </div>
    </form>
</body>
 </html>
</asp:Content>

