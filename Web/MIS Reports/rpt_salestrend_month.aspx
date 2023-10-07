﻿<%@ Page Language="C#"  EnableEventValidation="false" AutoEventWireup="true" CodeFile="rpt_salestrend_month.aspx.cs" Inherits="MIS_Reports_rpt_salestrend_month" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Sales Trend Analysis Product Detail</title>


  <script src="../JsFiles/canvasjs.min.js"></script>

    <link type="text/css" rel="stylesheet" href="../../css/repstyle.css" />
     <script language="Javascript">
         function RefreshParent() {
             //window.opener.document.getElementById('form1').click();
             window.close();
         }
    </script>
 
    <script type="text/javascript">

        function genChart(arrDta) {

            var chart = new CanvasJS.Chart("chartContainer", {
                theme: "theme2", //theme1
                title: {
                //text: ""
            },
            animationEnabled: true,   // change to true
            data: [{
                type: "line",       // Change type to "bar", "area", "spline", "pie",etc.
                dataPoints: arrDta
            }]
        });
        chart.render();
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
    <div>
      <asp:Panel ID="pnlbutton" runat="server">
            <table width="100%">
                <tr>
                <td width="20%"></td>
                    <td width="80%" align="center" >
                    <asp:Label ID="lblHead" Text="Trend Analysis" SkinID="lblMand" Font-Bold="true"  Font-Underline="true"
                runat="server"/>
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

       <asp:Panel ID="pnlContents" runat="server" Width="100%">
    <div>
        <div align="center">
          
        </div>
        <div>
                <table width="100%" align="center">
                    <tr>
                  
                        
                       
                        <td align="Center"  style="width:250px;">
                            <asp:Label ID="lblIDstatename" Text="Statename :" Font-Bold="True"  runat="server" SkinID="lblMand"></asp:Label>
                            <asp:Label ID="lblstate" runat="server" SkinID="lblMand"></asp:Label>
                        </td>
                        <td align="Center"  style="width:250px;">
                            <asp:Label ID="lblIDarea" Text="Area :" Font-Bold="True" runat="server" SkinID="lblMand"></asp:Label>
                            <asp:Label ID="lblarea" runat="server" SkinID="lblMand"></asp:Label>
                        </td>
                      <td align="Center"  style="width:250px;">
                            <asp:Label ID="lblIDZone" Text="Zone :" runat="server"  Font-Bold="True"  SkinID="lblMand"></asp:Label>
                            <asp:Label ID="lblzones" runat="server" SkinID="lblMand"></asp:Label>
                        </td>
   <td align="Center"  style="width:250px;">
                            <asp:Label ID="lblIDTerritory" Text="Territory :" Font-Bold="True" runat="server" SkinID="lblMand"></asp:Label>
                            <asp:Label ID="lblterritory" runat="server" SkinID="lblMand"></asp:Label>
                        </td>
                        
                    </tr>
                </table>
           </div>
            <br />
         <div id="chartContainer" style="text-align:center;height: 300px; width: 95%;"></div>
            </div>
            <table width="70%" align="center">
                <tbody>
                    <tr>
                        <td align="center">
                            <asp:Table ID="tbl"  runat="server" Style="border-collapse: collapse;  border: solid 1px Black;
                                 font-family: Calibri" Font-Size="8pt" GridLines="Both" Width="68%"/>
                          
                        </td>

                    </tr>
                    <tr><td>
                        &nbsp;</td></tr>
                </tbody>
            </table>  
            
    </asp:Panel>
    </form>
</body>
</html>
