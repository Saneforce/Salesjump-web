<%@ Page Language="C#" AutoEventWireup="true"  EnableEventValidation="false" CodeFile="rpt_primary_vs_secondary.aspx.cs" Inherits="MIS_Reports_rpt_primary_vs_secondary" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Primary Vs Secondary</title>


   <script src="../JsFiles/canvasjs.min.js"></script>

    <link type="text/css" rel="stylesheet" href="../../css/repstyle.css" />
     <script language="Javascript">
         function RefreshParent() {
             //window.opener.document.getElementById('form1').click();
             window.close();
         }
    </script>
<script type="text/javascript">

        function genChart(arrDta,arr1) {

            var chart = new CanvasJS.Chart("chartContainer", {
                theme: "theme2", //theme1
                title: {
                //text: ""
            },
            animationEnabled: true,   // change to true
            data: [{
                type: "line", 
 showInLegend: true,
legendText: "Primary",
      // Change type to "bar", "area", "spline", "pie",etc.
                dataPoints: arrDta
            },{
                type: "line",  
 showInLegend: true, 
legendText: "Secondary",    // Change type to "bar", "area", "spline", "pie",etc.
                dataPoints: arr1
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
                    <asp:Label ID="lblHead" Text="Primary Vs Secondary" SkinID="lblMand" Font-Bold="true"  Font-Underline="true"
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
                                        BorderColor="Black" BorderStyle="Solid" onclick="btnExport_Click" visible="false" /></td>

                               
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
   
       
          
      <center>
                <table  align="center">
                    <tr>                 
                       
                            <td width="260px">

            <asp:Label ID="DistributorName" runat="server" Text="Distributor Name :" 
                Font-Bold="True" Font-Names="Andalus"/>
                <asp:Label ID="dist" runat="server" Font-Size="12px" Font-Underline="True"></asp:Label></td>
                        <td >
                            <asp:Label ID="Ffname" Text="Fieldforcename :"  Font-Bold="True" Font-Names="Andalus" runat="server" SkinID="lblMand"></asp:Label>
                            <asp:Label ID="fdname" runat="server" SkinID="lblMand"/>
                        </td>
                        
                    </tr>
                </table>
         </center>
            <br />
       <table align="left"><tr></tr></table>
<div id="chartContainer" style="text-align:center;height: 300px; width: 95%;"></div>
            
            <table width="100%" align="center">
                <tbody>
                    <tr>
                        <td align="center">
                           
						 <asp:Table ID="tbl"  runat="server" Style="border-collapse: collapse;  border: solid 1px Black; font-family: Calibri" Font-Size="8pt" GridLines="Both" Width="95%"/>

                           
                        </td>

                    </tr>

                      <tr><td>  &nbsp;</td></tr>
                </tbody>
            </table>  
                  
    </asp:Panel>
    </form>
</body>
</html>
