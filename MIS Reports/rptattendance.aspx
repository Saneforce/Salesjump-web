<%@ Page Language="C#" EnableEventValidation="false" AutoEventWireup="true" CodeFile="rptattendance.aspx.cs" Inherits="MIS_Reports_rptattendance" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Attendance View</title>
<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>

    <script src="http://canvasjs.com/assets/script/canvasjs.min.js"></script>

    <link type="text/css" rel="stylesheet" href="../../css/repstyle.css" />
     <script language="Javascript">
         function RefreshParent() {
             window.opener.document.getElementById('form1').click();
             window.close();
         }
    </script>
    
    <style type="text/css">
        .rptCellBorder
        {
            border: 1px solid;
            border-color :#999999;
        }
        
        .remove  
  {
    text-decoration:none;
  }
    .ttable tr:nth-child(odd)
  {
     background-color:#dbe2f9;
  }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
      <asp:Panel ID="pnlbutton" runat="server">
            <table width="100%">
<tr>&nbsp</tr>
                <tr>
               <td width="100%" align="center" >
                    <asp:Label ID="lblHead"  SkinID="lblMand" style="font-weight:bold;FONT-SIZE: 24pt;COLOR: black;FONT-FAMILY: fantasy;float: left;padding: 5px;"
                runat="server"></asp:Label>
                    </td>
                    <td align="right">
                        <table>
                            <tr>
                                <td>
                                    <asp:Button ID="btnPrint" runat="server" Text="Print" Font-Names="Verdana" Font-Size="10px"
                                        BorderColor="Black" BorderStyle="Solid" BorderWidth="1" Height="25px" Width="60px"
                                        OnClick="btnPrint_Click" />
                                </td>
                                <td>
                                    <asp:Button ID="btnExcel" runat="server" Text="Excel" Font-Names="Verdana" Font-Size="10px"
                                        BorderColor="Black" BorderStyle="Solid" BorderWidth="1" Height="25px" Width="60px"
                                        OnClick="btnExcel_Click" />
                                </td>
                         
                               
                                <td>
                                    <asp:Button ID="btnClose" runat="server" Text="Close" Font-Names="Verdana" Font-Size="10px"
                                        BorderColor="Black" BorderStyle="Solid" BorderWidth="1" Height="25px" Width="60px"
                                        OnClientClick="RefreshParent();" OnClick="btnClose_Click" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </div>

       <asp:Panel ID="pnlContents" runat="server" Width="100%">
 
       
        
       
        <div>
                <table width="100%" align="center">
                    <tr>
                                    
                       
                        <td align="left">
                            <asp:Label ID="lblIDsf_name" Text="Team:" Font-Bold="true"  Font-Underline="true" ForeColor="#476eec" runat="server" ></asp:Label>
                            <asp:Label ID="lblsf_name" runat="server"   Font-Underline="true" SkinID="lblMand"></asp:Label>
                        </td>
                       
                        <td align="right">
                 
                <asp:Image ID="logoo" runat="server" style="width: 28%;border-width:0px;height: 39px;"></asp:Image>
         
            </td>
                    </tr>
                </table>
          
         
        
            <table width="100%" align="center" class="ttable">
                <tbody>
                    <tr>
                        <td align="center">
                            <asp:Table ID="tbl"  runat="server" Style="border-collapse: collapse;  border: solid 1px Black;
                                 font-family: Calibri" Font-Size="8pt" GridLines="Both" Width="100%">
                            </asp:Table>
                        </td>

                    </tr>
                 
                </tbody>
            </table>  
            </div>      
    </asp:Panel>
    </form>
  
</body>
</html>