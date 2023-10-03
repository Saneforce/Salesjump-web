﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rpt_distributor_trend.aspx.cs" EnableEventValidation="false" Inherits="MIS_Reports_rpt_distributor_trend" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Purchase Trend Analysis</title>


  <script src="../JsFiles/canvasjs.min.js"></script>

   <link href="../css/bootstrap-3.3.2.min.css" rel="stylesheet" />
    <link href="../css/style.css" rel="stylesheet" />

     <script language="Javascript">
         function RefreshParent() {
            // window.opener.document.getElementById('form1').click();
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
            animationEnabled: false,   // change to true
            data: [{
                type: "column",       // Change type to "bar", "area", "spline", "pie",etc.
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
              
                    <td width="60%" align="center" >
                    <asp:Label ID="lblHead" Text="Trend Analysis"   Font-Size="Large" Font-Bold="true"  Font-Underline="true"
                runat="server"></asp:Label>
                    </td>
                    <td  width="60%"  align="right" >
                   
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
    
       <center>
          
       
        <div>
                <table width="100%" align="center">
                    <tr>
                    <td width="2.5%"></td>
                        <td align="left">
                            &nbsp;</td>
                       
                        <td align="left">
                            <asp:Label ID="lblIDMonth" Text="Month :" runat="server" SkinID="lblMand"></asp:Label>
                            <asp:Label ID="lblMonth" runat="server" SkinID="lblMand"></asp:Label>
                        </td>
                        <td align="left">
                            <asp:Label ID="lblIDYear" Text="Year :" runat="server" SkinID="lblMand"></asp:Label>
                            <asp:Label ID="lblYear" runat="server" SkinID="lblMand"></asp:Label>
                        </td>
                        
                    </tr>
                </table>
           </div>
            <br />
            <div id="chartContainer" style="height: 300px; width: 100%;"></div>
            <table width="80%" align="center">
                <tbody>
                    <tr>
                        <td align="center">
                            <asp:Table ID="tbl"  runat="server" class="newStly" Font-Size="8pt" GridLines="Both" >
                            </asp:Table>
                        </td>

                    </tr>
                    <tr><td>
                        &nbsp;</td></tr>
                </tbody>
            </table>  
            </center>    
    </asp:Panel>
    </form>
</body>
</html>
