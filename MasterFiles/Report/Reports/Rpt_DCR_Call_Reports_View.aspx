<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Rpt_DCR_Call_Reports_View.aspx.cs" Inherits="Rpt_DCR_Call_Reports_View" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "https://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="https://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>DCR Call Reports</title>
 <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>

    <script src="https://canvasjs.com/assets/script/canvasjs.min.js"></script>

    <link type="text/css" rel="stylesheet" href="../../css/repstyle.css" />
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
</head>
<body>
    <form id="form1" runat="server">
    <div>
      <asp:Panel ID="pnlbutton" runat="server">
            <table width="100%">
                <tr>
                <td width="20%"></td>
                    <td width="80%" align="center" >
                    <asp:Label ID="lblHead" Text="Retail Lost  Details" SkinID="lblMand" Font-Bold="true"  Font-Underline="true"
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

       <asp:Panel ID="pnlContents" runat="server" Width="100%">
    <div>
        <div align="center">
          
        </div>
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
            <%--<div id="chartContainer" style="height: 300px; width: 100%;"></div>--%>
            <table width="100%" align="center">
                <tbody>
                    <tr>
                        <td align="center">
                            <asp:Table ID="tbl"  runat="server" Style="border-collapse: collapse;  border: solid 1px Black;
                                 font-family: Calibri" Font-Size="8pt" GridLines="Both" Width="98%">
                            </asp:Table>
                        </td>

                    </tr>
                    <tr><td align="center">
                        <asp:Label runat="server" id="lblResultMsg" ForeColor="Red" Visible="False" 
                            Width="750px" height="25px" BorderColor="#66CCFF" BorderWidth="1px" Font-Size="Medium"  BackColor="#d6e9c6"  /></td></tr>
                </tbody>
            </table>  
            </div>      
    </asp:Panel>
    </form>
<script type="text/javascript" src="https://maps.googleapis.com/maps/api/js?key=AIzaSyCmkCCNa1rg3qdip1yBtgnN53wz9M5jQ80&sensor=false&libraries=places"></script>
<script type="text/javascript">
    $(document).ready(function () {
        console.log("ready!");
        var directionsService = new google.maps.DirectionsService();
        ikk = 0;
        function CalcDistbyCtrl() {
            $xc = $(".distDisp");
            if (ikk < $xc.length) {
                ikk++;
                console.log(ikk);
                dis = findDistance($xc[ikk - 1]);
                dis.CalDist(dis);
            }
        }
        CalcDistbyCtrl();
        function findDistance(x) {
            var DistCalc = {
                waypoints: JSON.parse($(x).closest('tr').attr('data-locs').replace(/&quot;/g, '"')),
                ctrl: x,
                iloop: 1,
                TotDist: 0,
                CalDist: function (tx) {
                    if (this.waypoints.length > 1) {
                        source = new google.maps.LatLng(this.waypoints[this.iloop - 1].lat, this.waypoints[this.iloop - 1].lng);
                        destination = new google.maps.LatLng(this.waypoints[this.iloop].lat, this.waypoints[this.iloop].lng);

                        //*********DISTANCE AND DURATION**********************//
                        var service = new google.maps.DistanceMatrixService();
                        service.getDistanceMatrix(
                        {
                            origins: [source],
                            destinations: [destination],
                            travelMode: google.maps.TravelMode.WALKING,
                            unitSystem: google.maps.UnitSystem.METRIC,
                            avoidHighways: false,
                            avoidTolls: false
                        },
                        function (response, status) {
                            if (status == google.maps.DistanceMatrixStatus.OK && response.rows[0].elements[0].status != "ZERO_RESULTS") {
                                var distance = response.rows[0].elements[0].distance.text;
                                if (distance.indexOf(' km') > -1 && parseFloat(distance.replace(' km', '')) > 0) tx.TotDist += parseFloat(distance.replace(' km', '')) * 1000
                                if (distance.indexOf(' m') > -1) tx.TotDist += parseFloat(distance.replace(' m', ''));
                                console.log(tx.iloop + "," + tx.waypoints.length + ' :-- ' + source + ' - ' + destination + " = " + distance + ' ->' + tx.TotDist + '===' + (tx.TotDist / 1000) + " Km");
                                tx.iloop++
                                if (tx.iloop < tx.waypoints.length) {
                                    tx.ctrl.innerHTML = (tx.TotDist / 1000) + " Km";
                                    setTimeout(function () { tx.CalDist(tx); }, 500);
                                }
                                else {
                                    tx.ctrl.innerHTML = (tx.TotDist / 1000) + " Km";
                                    setTimeout(function () { CalcDistbyCtrl(); }, 300);
                                }
                            } else {
                                //tx.ctrl.innerHTML += "!";
                                console.log("Unable to find the distance via road. : " + status);
                                if (tx.iloop < tx.waypoints.length) {
                                    tx.ctrl.innerHTML = (tx.TotDist / 1000) + " Km!";
                                    setTimeout(function () { tx.CalDist(tx); }, 1000);
                                }
                                else {

                                    tx.ctrl.innerHTML = (tx.TotDist / 1000) + " Km!";
                                    setTimeout(function () { CalcDistbyCtrl(); }, 1000);
                                }
                            }
                        });
                    } else {
                        tx.ctrl.innerHTML += "-";
                        setTimeout(function () { CalcDistbyCtrl(); }, 300);
                    }
                }
            }
            return DistCalc;
        }
    });
</script>
</body>
</html>