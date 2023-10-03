﻿<%@ Page Language="C#" EnableEventValidation="false" AutoEventWireup="true" CodeFile="rptzonewise_purchase_report.aspx.cs" Inherits="MIS_Reports_rptzonewise_purchase_report" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Purchase Register- ZoneWise View</title>
   <link href="../css/bootstrap-3.3.2.min.css" rel="stylesheet" />
    <link href="../css/style.css" rel="stylesheet" />


     <script language="Javascript">
         function RefreshParent() {
            // window.opener.document.getElementById('form1').click();
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
            animationEnabled: true,   // change to true
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
               
                    <td width="70%" align="center" >
                    <asp:Label ID="lblHead" Text="Purchase Register-ZoneWise" SkinID="lblMand" Font-Bold="true"  Font-Underline="true"
                runat="server"></asp:Label>
                    </td>
                   <td width="30%" align="right">
                      <asp:LinkButton ID="btnPrint" runat="Server" style="padding: 0px 20px;" class="btn btnPrint" OnClick="btnPrint_Click"/>
                                                                        <asp:LinkButton ID="btnExcel" runat="Server" style="padding: 0px 20px;" class="btn btnExcel" OnClick="btnExcel_Click"/>
   
								  <asp:LinkButton ID="btnExport"  runat="Server" style="padding: 0px 20px;" class="btn btnPdf"  OnClick="btnExport_Click" />
<asp:LinkButton ID="LinkButton1" runat="Server" style="padding: 0px 20px;" class="btn btnClose"   OnClientClick="javascript:window.open('','_self').close();"/>




  </td>


                </tr>
            </table>
        </asp:Panel>
    </div>

       <asp:Panel ID="pnlContents" runat="server" Width="100%">
    <div>
        <center>
                <table  align="center">
                    <tr>                   
                       
                                             <td align="Center"  style="width:250px;">
                            <asp:Label ID="lblIDarea" Text="Area :" Font-Bold="True" runat="server" SkinID="lblMand"></asp:Label>
                            <asp:Label ID="lblarea" runat="server" SkinID="lblMand"></asp:Label>
                        </td>

                        
                    </tr>
                </table>
                    </center>
            <br />
 <div id="chartContainer" style="text-align:center;height: 300px; width: 95%;"></div>
            <table width="100%" align="center">
                <tbody>
                    <tr>
                        <td align="center">
                            <asp:Table ID="tbl"  runat="server" class="newStly"  GridLines="Both" Width="95%">
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
