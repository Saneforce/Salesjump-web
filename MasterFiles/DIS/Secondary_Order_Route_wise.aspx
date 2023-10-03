﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Secondary_Order_Route_wise.aspx.cs" Inherits="MIS_Reports_Secondary_Order_Route_wise" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Purchase Register Route Wise</title>
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
  
    </style>
 <script src="../JsFiles/canvasjs.min.js"></script>
	<script type="text/javascript">

    function genChart(arrDta) {
		
        var chart = new CanvasJS.Chart("chartContainer", {
            theme: "theme2", //theme1
            title: {
                //text: ""
            },
            animationEnabled: false,   // change to true
	        data: [{				
				type: "column",       // Change type to "bar", "area", "spline", "pie",etc.
				dataPoints: arrDta
			}]
        });
        chart.render();
    }
	</script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
      <asp:Panel ID="pnlbutton" runat="server">

            <table width="100%">
                <tr>
                <td width="20%"></td>
                    <td width="80%" align="center" >
                    <asp:Label ID="lblHead" Text="Purchase Register Route Wise" SkinID="lblMand" Font-Bold="true"  Font-Underline="true"
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
        <div align="center">
          
        </div>
        <div>
           </div>
<div id="chartContainer" style="text-align:center;height: 300px; width: 95%;"></div>
            </div>
            <table align="center" width="100%">
                <tr>
                    <td width="2.5%">
                    </td>
                    <td align="left">
                        &nbsp;</td>
                    <td align="left">
                        <asp:Label ID="lblIDMonth" runat="server" SkinID="lblMand" Text="Month :"></asp:Label>
                        <asp:Label ID="lblMonth" runat="server" SkinID="lblMand"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:Label ID="lblIDYear" runat="server" SkinID="lblMand" Text="Year :"></asp:Label>
                        <asp:Label ID="lblYear" runat="server" SkinID="lblMand"></asp:Label>
                    </td>
                </tr>
        </table>
        <table align="center"><tr><td>
            <asp:Label ID="DistributorName" runat="server" Text="Distributor Name" 
                Font-Bold="True" Font-Names="Andalus"></asp:Label></td><td>
                <asp:Label ID="dist" runat="server" Font-Size="12px" Font-Underline="True"></asp:Label></td></tr></table>
            <br />
            <table width="100%" align="center">
                <tbody>
                    <tr>
                        <td align="center">
                            <asp:Table ID="tbl"  runat="server" Style="border-collapse: collapse;  border: solid 1px Black;
                                 font-family: Calibri" Font-Size="8pt" GridLines="Both" Width="95%">
                            </asp:Table>
                        </td>

                    </tr>
                    <tr><td>
                        &nbsp;</td></tr>
                </tbody>
            </table>  
            </div>      
    </asp:Panel>
  </form>
</body>
</html>