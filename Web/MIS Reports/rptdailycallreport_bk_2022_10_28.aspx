<%@  Language="C#"  EnableEventValidation="false" AutoEventWireup="true" CodeFile="rptdailycallreport.aspx.cs" Inherits="MIS_Reports_rptdailycallreport" %>


<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <title>Sales Man Call Tracking(Daily)</title>
        <script src="http://canvasjs.com/assets/script/canvasjs.min.js"></script>
        <link href="../css/bootstrap-3.3.2.min.css" rel="stylesheet" type="text/css" />
        <link href="../css/style.css" rel="stylesheet" type="text/css" />
        <script  type="text/javascript">
            function RefreshParent() {
                //  window.opener.document.getElementById('form1').click();
                window.close();
            }
        </script>
        <style type="text/css">
            .rptCellBorder {
                border: 1px solid;
                border-color: #999999;
            }

            .remove {
                text-decoration: none;
            }
        </style>
        <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
        <script src="https://cdnjs.cloudflare.com/ajax/libs/moment.js/2.11.1/moment.min.js"></script>
        <script type="text/javascript">
            removecol = function () {
                for (i = 0; i < tbl.length; i++) {
                }
            }
            $(document).ready(function () {
                var indx = 2;
                function loadAddrss() {
                    $(".geoaddr").css("display", "none");
                    var tbl = $('#tbl tr');
                    var lat1 = $(tbl[indx]).find('td')[2];
                    var long1 = $(tbl[indx]).find('td')[3];
                    var lat2 = $(lat1).text().trim();
                    var long2 = $(long1).text().trim();
                    var addrs = '';

                    var geocodingAPI = "https://maps.googleapis.com/maps/api/geocode/json?latlng=" + lat2 + "," + long2 + "&key=asknAAP_AIzaSyAER5hPywUW-5DRlyKJZEfsqgZlaqytxoU";
                    $.getJSON(geocodingAPI, function (json) {
                        if (json.status == "OK") {
                            var result = json.results[0];
                            addrs = result.formatted_address;
                            $x = $('#Addr');
                            $($(tbl[indx]).find('.Addr')).text(result.formatted_address); indx++; loadAddrss();
                        }
                        else {
                            $x = $("#Addr");
                            $($(tbl[indx]).find('.Addr')).text(json.status); indx++; loadAddrss();
                        }
                    })
                }
                loadAddrss();
                $(document).on('click', "#btnPrint", function () {
                    var originalContents = $("body").html();
                    var printContents = $("#content").html();
                    $("body").html(printContents);
                    window.print();
                    $("body").html(originalContents);
                    return false;
                });
            });
        </script>
    </head>
    <body>
        <form id="form1" runat="server">
            <div>
                <asp:Panel ID="pnlbutton" runat="server">
                    <table style="width:100%">
                        <tr>
                            <td style="width:60%;" align="center">
                                <asp:Label ID="lblHead" Text="Sales Man Call Tracking(Daily)" SkinID="lblMand" Font-Bold="true"  Font-Underline="true" runat="server"></asp:Label>
                            </td>
                            <td  style="width:40%;" align="right">
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
            <br />
            <div>
                <asp:Panel ID="pnlContents" runat="server" Width="100%">
                    <div>
                        <table width="100%" align="center">
                            <tr>
                                <td width="2.5%"></td>
                                <td align="left">&nbsp;</td>
                                <td align="left">
                                    <asp:Label ID="lblIDMonth" Text="Month :" runat="server" SkinID="lblMand" Font-Bold="True"></asp:Label>
                                    <asp:Label ID="lblMonth" runat="server" SkinID="lblMand"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label ID="lblIDYear" Text="Year :" runat="server" SkinID="lblMand" Font-Bold="True"></asp:Label>
                                    <asp:Label ID="lblYear" runat="server" SkinID="lblMand"></asp:Label>
                                </td>
                            </tr>
                        </table>
                        <table width="100%" align="center">
                            <tr>
                                <td width="2.5%"></td>
                                <td align="left">&nbsp;</td>
                                <td align="center">
                                    <asp:Label ID="Label1" Text="SalesMan Name  :" runat="server"  ForeColor="#0099CC" Font-Bold="True" Font-Names="Andalus" Font-Underline="True"></asp:Label>
                                    <asp:Label ID="distname" runat="server" SkinID="lblMand"  Font-Bold="True"></asp:Label>
                                </td>
                                <td>&nbsp&nbsp</td>                     
                            </tr>
                        </table>
                    </div>
                    <br /><%--<br />--%>
                    <div id="norecordfound" runat="server" align="center" style="border: 1px solid #99FFCC; background-color: #CCFFFF;">No Record Found</div>
                    <table width="100%" align="center">
                        <tbody>
                            <tr>
                                <td align="center">
                                    <asp:Table ID="tbl"  runat="server"  class="newStly" Style="border-collapse: collapse;  border: solid 1px Black;" 
                                        GridLines="Both" Width="90%">

                                    </asp:Table>
                                </td>
                            </tr>
                        </tbody>
                    </table>  
                    <br /><%--<br /><br />--%>
                    <div ID="detail" runat="server" style="padding-left:100px;">
                        <table align="center">
                            <tbody style="border:1px solid black;">
                                <tr>
                                    <td>
                                        <asp:Label ID="Label2" runat="server"  Font-Bold="True" Font-Names="Andalus" Font-Underline="True" ForeColor="#0099CC" Text="Total Calls :"></asp:Label>
                                    </td>
                                    <td>&nbsp&nbsp</td>
                                    <td>
                                        <asp:Label ID="callcount" runat="server" Font-Bold="True" SkinID="lblMand"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label4" runat="server" Font-Bold="True" Font-Names="Andalus" Font-Underline="True" ForeColor="#0099CC" Text="Total Productive Calls :"></asp:Label>
                                    </td>
                                    <td>&nbsp&nbsp</td>
                                    <td>
                                        <asp:Label ID="productive" runat="server" Font-Bold="True" SkinID="lblMand"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label7" runat="server" Font-Bold="True" Font-Names="Andalus" Font-Underline="True" ForeColor="#0099CC" Text="Drop Size :"></asp:Label>
                                    </td>
                                    <td>&nbsp&nbsp</td>
                                    <td>
                                        <asp:Label ID="drop_size" runat="server" Font-Bold="True" SkinID="lblMand"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label5" runat="server" Font-Bold="True" Font-Names="Andalus" Font-Underline="True" ForeColor="#0099CC" Text="Call Average :"></asp:Label>
                                    </td>
                                    <td>&nbsp&nbsp</td>
                                    <td>
                                        <asp:Label ID="call_average" runat="server" Font-Bold="True" SkinID="lblMand"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label6" runat="server" Font-Bold="True" Font-Names="Andalus" Font-Underline="True" ForeColor="#0099CC" Text="Closing Time :"></asp:Label>
                                    </td>
                                    <td>&nbsp&nbsp</td>
                                    <td>
                                        <asp:Label ID="closingtime" runat="server" Font-Bold="True" SkinID="lblMand"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Names="Andalus" Font-Underline="True" ForeColor="#0099CC" Text="Total Hours Worked :"></asp:Label>
                                    </td>
                                    <td>&nbsp&nbsp</td>
                                    <td>
                                        <asp:Label ID="tot_hours" runat="server" Font-Bold="True" SkinID="lblMand"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label9" runat="server" Font-Bold="True" Font-Names="Andalus" Font-Underline="True" ForeColor="#0099CC" Text="Total New Retailers :"></asp:Label>  
                                    </td>
                                    <td>&nbsp&nbsp</td>
                                    <td>
                                        <asp:Label ID="Total_new_rt" runat="server" Font-Bold="True" SkinID="lblMand"></asp:Label>
                                    </td>
                                </tr>
                                <tr> 
                                    <td>
                                        <asp:Label ID="Label8" runat="server" Font-Bold="True" Font-Names="Andalus" Font-Underline="True" ForeColor="#0099CC" Text="Visited New Retailers :"></asp:Label>
                                    </td>
                                    <td>&nbsp&nbsp</td>
                                    <td>
                                        <asp:Label ID="Tot_new_ret" runat="server" Font-Bold="True" SkinID="lblMand"></asp:Label>
                                    </td>
                                </tr>
                            </tbody> 
                        </table>
                    </div>
                </asp:Panel>
            </div>
        </form>
    </body>
</html>

