<%@ Page Language="C#" EnableEventValidation="false" AutoEventWireup="true" CodeFile="rpt_order_territorywise_trend.aspx.cs" Inherits="MIS_Reports_rpt_order_territorywise_trend" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Retail Territorywise Trend Analysis</title>


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
                    <asp:Label ID="lblHead" Text="Trend Analysis" SkinID="lblMand" Font-Bold="true"  Font-Underline="true"
                runat="server"></asp:Label>
                    </td>
                    <td width="40%" align="right">
                        <table>
                            <tr>
<td>
                                 <asp:LinkButton ID="btnPrint" runat="Server" style="padding: 0px 20px;" class="btn btnPrint" OnClick="btnPrint_Click"/>
       <asp:LinkButton ID="btnExport"  runat="Server" style="padding: 0px 20px;" class="btn btnPdf"  OnClick="btnExport_Click" />
       <asp:LinkButton ID="btnExcel" runat="Server" style="padding: 0px 20px;" class="btn btnExcel" OnClick="btnExcel_Click"/>
       	<a name="btnClose" id="btnClose" type="button" style="padding: 0px 20px;" href="javascript:window.open('','_self').close();" class="btn btnClose"></a>
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
        <center>
                <table width="100%" align="center">
                    <tr>
                                          <td align="Center"  style="width:250px;">
                            <asp:Label ID="lblIDZone" Text="Zone :" runat="server"  Font-Bold="True"  SkinID="lblMand"></asp:Label>
                            <asp:Label ID="lblzones" runat="server" SkinID="lblMand"></asp:Label>
                        </td>

                        
                    </tr>
                </table>
            </center>
      
            <br />
            <div id="chartContainer" style="height: 300px; width: 100%;"></div>
            <table width="80%" align="center">
                <tbody>
                    <tr>
                        <td align="center">
                            <asp:Table ID="tbl"  runat="server" class="newStly" GridLines="Both" Width="68%">
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
