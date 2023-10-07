<%@ Page Language="C#"  EnableEventValidation="false" AutoEventWireup="true" CodeFile="rpt_retailer_potential_report.aspx.cs" Inherits="MIS_Reports_rpt_retailer_potential_report" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Retailer Potential</title>


    <script src="http://canvasjs.com/assets/script/canvasjs.min.js"></script>

    <link type="text/css" rel="stylesheet" href="../../css/repstyle.css" />
     <script language="Javascript">
         function RefreshParent() {
          //   window.opener.document.getElementById('form1').click();
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
  
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div align="center">
      <asp:Panel ID="pnlbutton" runat="server">
            <table width="100%">
                <tr>
                <td width="20%"></td>
                    <td width="80%" align="center" >
                    <asp:Label ID="lblHead" Text="Retailer Potential" SkinID="lblMand" Font-Bold="true"  Font-Underline="true"
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
                                    <td> <asp:Button ID="btnExport" runat="server" Text="PDF"  Font-Names="Verdana" Font-Size="10px"  BorderWidth="1" Height="25px" Width="60px"
                                        BorderColor="Black" BorderStyle="Solid" onclick="btnExport_Click" /></td>


                               
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

     
   
     
          
      
                
                <table width="100%" align="center">
                    <tr>
                   
                        <td align="right">
                            <asp:Label ID="feildf" Text="FieldForceName:" runat="server"  ForeColor="#0099CC"
                                Font-Bold="True" Font-Names="Andalus" Font-Underline="True"></asp:Label>
                            <asp:Label ID="feildforc" runat="server" SkinID="lblMand"  Font-Bold="True"></asp:Label>
                        </td>
                        
                        <td align="center">
                            <asp:Label ID="Label1" Text="Distributor Name:" runat="server"  ForeColor="#0099CC"
                                Font-Bold="True" Font-Names="Andalus" Font-Underline="True"></asp:Label>
                            <asp:Label ID="distname" runat="server" SkinID="lblMand"  Font-Bold="True"></asp:Label>
                        </td>
                        
                        <td align="left">
                            <asp:Label ID="prodt" Text="Route Name:" runat="server" 
                                Font-Bold="True" ForeColor="#0099CC" Font-Names="Andalus" 
                                Font-Underline="True"></asp:Label>
                            <asp:Label ID="prdname" runat="server" SkinID="lblMand"  Font-Bold="True"></asp:Label>
                        </td>
                        
                    </tr>
                </table>
           
           <br> </br>
           
            
  <asp:Panel ID="pnlContents" runat="server" Width="100%">

<table width="100%" align="center">
                <tbody>
                    <tr>
                        <td align="center">
                            <asp:Table ID="tbl"  runat="server" Style="border-collapse: collapse;  border: solid 1px Black;
                                 font-family: Calibri" Font-Size="8pt" GridLines="Both" Width="90%">
                            </asp:Table>
                        </td>

                    </tr>
                    
                </tbody>
            </table>  
                  
    </asp:Panel>
    </form>
</body>
</html>
