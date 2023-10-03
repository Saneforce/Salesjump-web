<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Rpt_DCR_View.aspx.cs" Inherits="Reports_Rpt_DCR_View" EnableEventValidation = "false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>DCR View Report</title>
    <link type="text/css" rel="Stylesheet" href="../../css/Report.css" />
    
    <style>
        .tr_det_head
        {
            font-family: Verdana;
            color: White;
            font-size: 9pt;
            height: 22px;
            font-weight: bold;
            font-family: Calibri;
            background: #0097AC;
            border-color: Black;
    		
        }
        .tbldetail_main
        {
            font-family: Verdana;
            font-size: 7.8pt;
            height: 17px;
            border: 1px solid;
            border-color: #999999;
        }
        .tbldetail_Data
        {
            height: 18px;
        }
        .Holiday
        {
            color: Red;
            font-size: 9pt;
            font-family: Calibri;
        }
        .NoRecord
        {
            font-size: 10pt;
            font-weight: bold;
            color: Red;
            background-color: AliceBlue;
        }
		.table td
		{
			padding: 2px 5px;
            white-space: nowrap;
		}
		.gridviewStyle td
		{
    	border: thin solid #000000;
    	text-align:right;
    	padding-right:3px;
    	max-width:300px;
		}
    </style>
    <script language="Javascript">
        function RefreshParent() {
            //window.opener.document.getElementById('form1').click();
            window.close();
        }
    </script>
<script type="text/javascript">
         function PrintGridData() {
            // alert('test');
             var prtGrid = document.getElementById('<%=pnlContents.ClientID %>');
             prtGrid.border = 1;
             var prtwin = window.open('', 'PrintGridViewData', 'left=0,top=0,width=800,height=500,tollbar=0,scrollbars=1,status=0,resizable=yes');
             prtwin.document.write(prtGrid.outerHTML);
             prtwin.document.close();
             prtwin.focus();
             prtwin.print();
             prtwin.close();
         }

    </script>
     <script type="text/javascript" src="http://maps.googleapis.com/maps/api/js?key=AIzaSyBG-dwpBFBzcI8Y5AOoBK2H8SFQh9bRnY4"></script>
    <script type="text/javascript">

        window.onload = function () {
            var mapOptions = {
                center: new google.maps.LatLng(markers[0].lat, markers[0].lng),
                zoom: 12,
                mapTypeId: google.maps.MapTypeId.ROADMAP
            };
            //------------


            //-----------
            var map = new google.maps.Map(document.getElementById("dvMap"), mapOptions);
            var infoWindow = new google.maps.InfoWindow();
            var lat_lng = new Array();
            geocodeLatLng(0, markers[0].lat, markers[0].lng);
            var latlngbounds = new google.maps.LatLngBounds();
            for (i = 0; i < markers.length; i++) {
                var data = markers[i];
                /*https://maps.googleapis.com/maps/api/geocode/json?latlng=40.714224,-73.961452*/
                var myLatlng = new google.maps.LatLng(data.lat, data.lng);
                var geocoder = new google.maps.Geocoder();
                lat_lng.push(myLatlng);
                var marker = new google.maps.Marker({
                    position: myLatlng,
                    map: map,
                    title: data.title,
                    icon: new google.maps.MarkerImage(data.icon)
                });
                latlngbounds.extend(marker.position);
                (function (marker, data) {
                    google.maps.event.addListener(marker, "click", function (e) {

                        infoWindow.setContent('<div id="content" style="margin-left:15px;margin-top:2px;overflow:hidden;">' + '<br><font style="color:darkblue;font:15px tahoma; margin-left:5px;">' + data.title + '</font>' + '<a style="color:#00303f; font:bold 12px verdana;float: right;">' + data.dteT + '</a></div>' + '<br><font style="color:black;font:15px tahoma; margin-left:5px;">POB:' + data.POB + '</font>' + '<br><div style="font:14px verdana;color:darkgreen; margin-left:5px;">' + data.description + '</div>' + '</div>');
                        infoWindow.open(map, marker);
                    });
                })(marker, data);
            }
            map.setCenter(latlngbounds.getCenter());
            map.fitBounds(latlngbounds);

            //***********ROUTING****************//

            //Initialize the Path Array
            var path = new google.maps.MVCArray();

            //Initialize the Direction Service
            var service = new google.maps.DirectionsService();

            //Set the Path Stroke Color
            var poly = new google.maps.Polyline({ map: map, strokeColor: '#4986E7' });

            //Loop and Draw Path Route between the Points on MAP
            for (var i = 0; i < lat_lng.length; i++) {
                if ((i + 1) < lat_lng.length) {
                    var src = lat_lng[i];
                    var des = lat_lng[i + 1];
                    path.push(src);
                    poly.setPath(path);
                    service.route({
                        origin: src,
                        destination: des,
                        travelMode: google.maps.DirectionsTravelMode.DRIVING
                    }, function (result, status) {
                        if (status == google.maps.DirectionsStatus.OK) {
                            for (var i = 0, len = result.routes[0].overview_path.length; i < len; i++) {
                                path.push(result.routes[0].overview_path[i]);
                            }
                        }
                    });
                }
            }
        }
        function geocodeLatLng(pi, slat, slng) {
            var geocoder;
            geocoder = new google.maps.Geocoder();
            var latlng = { lat: parseFloat(slat), lng: parseFloat(slng) };
            geocoder.geocode({ 'location': latlng }, function (results, status) {
                noti = $('.addrs')[pi];
                if (status === 'OK') {
                    if (results[1]) {
                        $(noti).html(results[1].formatted_address);
                    } else {
                        $(noti).html('No results found');
                    }

                } else {
                    $(noti).html('Geocoder failed due to: ' + status);
                }

                pi++;
                if (pi < markers.length) { setTimeout(function () { geocodeLatLng(pi, markers[pi].lat, markers[pi].lng) }, 2000); }
            });
            //---------------------
        }
                      
    </script>
<script type="text/javascript" src="../../JsFiles/jquery-1.10.1.js"></script>
     <script type="text/jscript">
         $(document).ready(function () {
             //alert('hi');
             var k = 3;

            
             k = 0;
             $('#<%=gvtotalorder.ClientID%>').find('tr').each(function () {

                 tds = $(this).find("td");
                 $(tds[2]).hide();
                 if (k > 0) {
                     if ($(tds[1]).text() != "TOTAL") {
                         $(tds[0]).text(k);

                         if ($(tds[2]).text() != "0") {
                             $(tds[1]).html('<a href=Rpt_DCR_View.aspx?&sf_code=' + $(tds[2]).text() + '&Mode=New_View_All_DCR_Date(s)&FDate=' + $('#<%=DCRdt.ClientID%>').val() + '&cur_month=0&cur_year=0&Tdate=' + $('#<%=DCRdt.ClientID%>').val() + '&Sf_Name=>' + $(tds[1]).html() + '</a>')
                             // $(this).html('<a href=Rpt_DCR_View.aspx?&sf_code=' + $(this).closest('tr').find('td:eq(2)').text() + '&Sf_Name=' + "" + '&cur_month=' + cmonth + '&cur_year=' + cyear + '&Mode=' + "SKY Summary" + '&FDate=' + "2017-12-01" +  '>' + $(this).html() + '</a>')

                         } 
                     }
                 }

                 k++;
             });



             $("a").click(function () {
                 //alert($(this).attr('href'));

                 event.preventDefault();
                 window.open($(this).attr("href"), "popupWindow", "width=600,height=600,scrollbars=yes");

             });
         });
    </script>
</head>
<body>
  
    <form id="form1" runat="server">
 <asp:HiddenField runat="server" Value="" ID="DCRdt" />
    <div>
        <center>
            <asp:Panel ID="pnlbutton" runat="server">
                <table width="100%">
                    <tr>
                       
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
                                        <asp:Button ID="btnPDF" runat="server" Text="PDF" Font-Names="Verdana" Font-Size="10px"
                                            BorderColor="Black" BorderStyle="Solid" BorderWidth="1" Height="25px" Width="60px"
                                            OnClick="btnPDF_Click" Visible="false" />
                                    </td>
                                    <td>
                                        <asp:Button ID="btnClose" runat="server" Text="Close" Font-Names="Verdana" Font-Size="10px"
                                            BorderColor="Black" BorderStyle="Solid" BorderWidth="1" Height="25px" Width="60px"
                                            OnClientClick="RefreshParent();" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
          	<asp:Panel ID="pnlContents" runat="server" Width="100%">
                <table border="0" width="90%">
                    <tr align="center">
                        <td>
                            <asp:Label ID="lblTitle" runat="server" Font-Size="Small" Font-Bold="True" Font-Underline="true"></asp:Label>
                            <span style="color: Red"></span>
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <asp:Label ID="lblHead" runat="server" Text="Daily Call Report for " Font-Underline="True"
                                Font-Size="Small" Font-Bold="True"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <%--<td align="center">
                            <span style="font-family: Verdana">Field Force Name :</span>
                            <asp:Label ID="lblFieldForceName" Font-Bold="true" Font-Names="Verdana" runat="server"></asp:Label>
                        </td>--%>
                    </tr>
                    <tr>
                        <td>
                            <asp:GridView ID="gvMyDayPlan" runat="server" Width="100%" HorizontalAlign="Center"
                                BorderWidth="1" CellPadding="2" EmptyDataText="No Data found for View" AutoGenerateColumns="false"
                                CssClass="mGrid">
                                <Columns>
                                    <asp:TemplateField HeaderText="S.No" ItemStyle-Width="50" HeaderStyle-BorderWidth="1"
                                        HeaderStyle-Font-Size="8pt" ItemStyle-BorderWidth="1" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSNo" runat="server" Font-Size="9pt" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Activity Date" ItemStyle-Width="70" HeaderStyle-BorderWidth="1"
                                        HeaderStyle-Font-Size="8pt" ItemStyle-BorderWidth="1" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="Pln_Date" runat="server" Font-Size="9pt" Text='<%#Eval("Pln_Date")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Start Time" ItemStyle-Width="70" HeaderStyle-BorderWidth="1"
                                        HeaderStyle-Font-Size="8pt" ItemStyle-BorderWidth="1" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="Pln_Time" runat="server" Font-Size="9pt" Text='<%#Eval("Pln_Time")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

									  <asp:TemplateField HeaderText="FieldForce Name" ItemStyle-Width="120" HeaderStyle-BorderWidth="1"
                                        HeaderStyle-Font-Size="8pt" ItemStyle-BorderWidth="1" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="txtFOName" runat="server" Font-Size="9pt" Text='<%#Eval("FO_name")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                     <asp:TemplateField HeaderText="Distributor Name" ItemStyle-Width="120" HeaderStyle-BorderWidth="1"
                                        HeaderStyle-Font-Size="8pt" ItemStyle-BorderWidth="1" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="txtWorkTypeName" runat="server" Font-Size="9pt" Text='<%#Eval("dist_name")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                   
                                     <asp:TemplateField HeaderText="WorkType Name" ItemStyle-Width="120" HeaderStyle-BorderWidth="1"
                                        HeaderStyle-Font-Size="8pt" ItemStyle-BorderWidth="1" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="txtWorkTypeName" runat="server" Font-Size="9pt" Text='<%#Eval("Worktype_Name_B")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Route Name" ItemStyle-Width="120" HeaderStyle-BorderWidth="1"
                                        HeaderStyle-Font-Size="8pt" ItemStyle-BorderWidth="1" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="txtClustername" runat="server" Font-Size="9pt" Text='<%#Eval("ClstrName")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Remarks" HeaderStyle-BorderWidth="1" HeaderStyle-Font-Size="8pt"
                                        ItemStyle-BorderWidth="1" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="txtremarks" runat="server" Font-Size="9pt" Text='<%#Eval("remarks")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <EmptyDataRowStyle ForeColor="Black" BackColor="AliceBlue" Height="5px" BorderColor="Black"
                                    BorderStyle="Solid" BorderWidth="2" Font-Bold="True" HorizontalAlign="Center"
                                    VerticalAlign="Middle" />
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <br />
            <asp:Panel ID="Panel2" runat="server" Width="100%">
                <table border="0" width="90%">
                    <tr align="center">
                        <td>
                            <asp:Label ID="Label2" runat="server" Font-Size="Small" Font-Bold="True" Font-Underline="true"></asp:Label>
                            <span style="color: Red"></span>
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                           <%-- <asp:Label ID="Label2" runat="server" Text="Daily Call Report for " Font-Underline="True"
                                Font-Size="Small" Font-Bold="True"></asp:Label>--%>
                        </td>
                    </tr>
                    <tr>
                        <%--<td align="center">
                            <span style="font-family: Verdana">Field Force Name :</span>
                            <asp:Label ID="lblFieldForceName" Font-Bold="true" Font-Names="Verdana" runat="server"></asp:Label>
                        </td>--%>
                    </tr>
                    <tr>
                        <td>
                            <asp:GridView ID="GridView2" runat="server" Width="100%"   ShowHeader="false" Font-Size="10pt"
                                HorizontalAlign="Center" OnDataBound = "OnDataBound1" OnRowCreated = "OnRowCreated1"   OnRowDataBound="GridView1_RowDataBound1"
                                BorderWidth="1px" CellPadding="2" EmptyDataText="No Data found for View" 
                                AutoGenerateColumns="true" BackColor="#ffffe0"   RowStyleCssClass="gridpager"
                                HeaderStyle-HorizontalAlign="Center" BorderColor="Black" BorderStyle="Solid"   ShowFooter="true">                               
                                <Columns>
                                
                                </Columns>
                                <EmptyDataRowStyle ForeColor="Black" BackColor="AliceBlue" Height="5px" BorderColor="Black"
                                    BorderStyle="Solid" BorderWidth="2" Font-Bold="True" HorizontalAlign="Center"
                                    VerticalAlign="Middle" />
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <br />
 <asp:Panel ID="Panel1" runat="server" Width="100%">
                <table border="0" width="90%">
                    <tr align="center">
                        <td>
                            <asp:Label ID="Label1" runat="server" Font-Size="Small" Font-Bold="True" Font-Underline="true"></asp:Label>
                            <span style="color: Red"></span>
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                           <%-- <asp:Label ID="Label2" runat="server" Text="Daily Call Report for " Font-Underline="True"
                                Font-Size="Small" Font-Bold="True"></asp:Label>--%>
                        </td>
                    </tr>
                    <tr>
                        <%--<td align="center">
                            <span style="font-family: Verdana">Field Force Name :</span>
                            <asp:Label ID="lblFieldForceName" Font-Bold="true" Font-Names="Verdana" runat="server"></asp:Label>
                        </td>--%>
                    </tr>
                    <tr>
                        <td>
                            <asp:GridView ID="GridView1" runat="server" Width="100%" HorizontalAlign="Center"
                                BorderWidth="1" CellPadding="2" EmptyDataText="No Data found for View" AutoGenerateColumns="false"
                                CssClass="mGrid">
                                <Columns>
                                    <asp:TemplateField HeaderText="S.No" ItemStyle-Width="50" HeaderStyle-BorderWidth="1"
                                        HeaderStyle-Font-Size="8pt" ItemStyle-BorderWidth="1" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSNo" runat="server" Font-Size="9pt" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Area" ItemStyle-Width="70" HeaderStyle-BorderWidth="1"
                                        HeaderStyle-Font-Size="8pt" ItemStyle-BorderWidth="1" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="Pln_Date" runat="server" Font-Size="9pt" Text='<%#Eval("area")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Shop Name" ItemStyle-Width="70" HeaderStyle-BorderWidth="1"
                                        HeaderStyle-Font-Size="8pt" ItemStyle-BorderWidth="1" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="Pln_Time" runat="server" Font-Size="9pt" Text='<%#Eval("Shop_Name")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Contact Person" ItemStyle-Width="120" HeaderStyle-BorderWidth="1"
                                        HeaderStyle-Font-Size="8pt" ItemStyle-BorderWidth="1" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="txtWorkTypeName" runat="server" Font-Size="9pt" Text='<%#Eval("Contact_Person")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                   
                                     <asp:TemplateField HeaderText="Remarks" ItemStyle-Width="120" HeaderStyle-BorderWidth="1"
                                        HeaderStyle-Font-Size="8pt" ItemStyle-BorderWidth="1" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="txtWorkTypeName" runat="server" Font-Size="9pt" Text='<%#Eval("Remarks")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Date" ItemStyle-Width="120" HeaderStyle-BorderWidth="1"
                                        HeaderStyle-Font-Size="8pt" ItemStyle-BorderWidth="1" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="txtClustername" runat="server" Font-Size="9pt" Text='<%#Eval("Date")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                 
                                </Columns>
                                <EmptyDataRowStyle ForeColor="Black" BackColor="AliceBlue" Height="5px" BorderColor="Black"
                                    BorderStyle="Solid" BorderWidth="2" Font-Bold="True" HorizontalAlign="Center"
                                    VerticalAlign="Middle" />
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
          
        </center>
    </div>
 </asp:Panel>
 <br />
 	<asp:Panel ID="Panel3" runat="server" Width="100%">
      <asp:Table ID="Table1" runat="server" Height="100%" Width="100%">
            <asp:TableRow ID="TableRow1" runat="server">
                <asp:TableCell ID="TableCell1" runat="server"></asp:TableCell>
                
                <asp:TableCell ID="TableCell2" runat="server" style="padding-left:50%;"></asp:TableCell>
            <%--    <asp:TableCell ID="TableCell3" runat="server"></asp:TableCell>--%>
            </asp:TableRow>
            <asp:TableRow ID="TableRow2" runat="server">
                <asp:TableCell ID="TableCell4" runat="server"></asp:TableCell>
                <asp:TableCell ID="TableCell5" runat="server" style="padding-left:50%;"></asp:TableCell>
               <%-- <asp:TableCell ID="TableCell6" runat="server"></asp:TableCell>--%>
            </asp:TableRow>
            <asp:TableRow ID="TableRow3" runat="server">
                <asp:TableCell ID="TableCell7" runat="server"></asp:TableCell>
                <asp:TableCell ID="TableCell8" runat="server" style="padding-left:50%;"></asp:TableCell>
              <%--  <asp:TableCell ID="TableCell9" runat="server"></asp:TableCell>--%>
            </asp:TableRow>
        </asp:Table>
                <table border="0" width="100%">
                   
                    <tr>
                        <td>
                             <asp:GridView ID="gvtotalorder" runat="server" Width="100%"   ShowHeader="false" Font-Size=10pt
                                HorizontalAlign="Center" OnDataBound = "OnDataBound" OnRowCreated = "OnRowCreated"   OnRowDataBound="GridView1_RowDataBound"
                                BorderWidth="1px" CellPadding="2" EmptyDataText="No Data found for View" 
                                AutoGenerateColumns="true" BackColor="#ffffe0" RowStyleCssClass="gridviewStyle"
                                HeaderStyle-HorizontalAlign="Center" BorderColor="Black" BorderStyle="Solid"  CssClass="mGrid" ShowFooter="true">                               
                                <Columns>

                                </Columns>
                                <EmptyDataRowStyle ForeColor="Black" BackColor="AliceBlue" Height="5px" BorderColor="Black"
                                    BorderStyle="Solid" BorderWidth="2" Font-Bold="True" HorizontalAlign="Center"
                                    VerticalAlign="Middle" />
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <br />
    </form>
 
</body>
</html>