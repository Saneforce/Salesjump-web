<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rptCustomerGeotagging.aspx.cs" Inherits="MasterFiles_rptCustomerGeotagging" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.0/css/bootstrap.min.css" />
    <link href="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.9/themes/start/jquery-ui.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="//code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css" />
    <link href="../css/style.css" rel="stylesheet" />
    <style type="text/css">
        .ui-dialog {
            width: 500px !important;
        }

        #dvMap {
            background: #cfd2d6;
            width: 600px;
            height: 550px;
            display: none;
            position: fixed;
            top: 195px;
            left: 300px;
            margin: -150px 0 0 -150px;
            z-index: 10000;
        }
    </style>

    <script src="https://code.jquery.com/jquery-1.12.4.js"></script>
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
    <script src="  https://cdnjs.cloudflare.com/ajax/libs/jquery.blockUI/2.70/jquery.blockUI.js"></script>

   <script src="https://maps.googleapis.com/maps/api/js?sensor=false&key=ASDAAP_AIzaSyAER5hPywUW-5DRlyKJZEfsqgZlaqytxoU&libraries=geometry,places&ext=.js"></script>

    <script type="text/javascript">

        $(document).ready(function () {
            var dValue = [];
            var sfCode = $('#<%=hidn_sf_code.ClientID%>').val();
            var fYear = $('#<%=hFYear.ClientID%>').val();
            var fMonth = $('#<%=hFMonth.ClientID%>').val();


            genTable = function () {

                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "rptCustomerGeotagging.aspx/GetGEORetailers",
                    data: "{'SF_Code':'" + sfCode + "'}",
                    dataType: "json",
                    success: function (data) {
                        dValue = data.d;
                        var tbl = $('#cTable');
                        $(tbl).find('tbody tr').remove();
                        if (dValue.length > 0) {
                            str = '';
                            for (var i = 0; i < dValue.length; i++) {
                                str = '<td>' + (i + 1) + '</td><td><input type="hidden" name="currAddress" value="' + dValue[i].curAddress + '"/><input type="hidden" name="custCode" value="' + dValue[i].custCode + '"/> <input type="hidden" name="mlat" value="' + dValue[i].lat + '"/><input type="hidden" name="mlog" value="' + dValue[i].log + '"/>   ' + dValue[i].custName + '</td><td>' + dValue[i].routeCode + '</td><td>' + dValue[i].createDate + '</td><td style="text-align: center; width:200px;"><a id="btnView" class="btn btn-link" >View</a></td>';
                                $(tbl).find('tbody').append('<tr>' + str + '</tr>');
                            }

                        }
                        else {
                            $(tbl).find('tbody').append('<tr><td colspan="6" >No Records Found..!</td></tr>');
                        }

                    },
                    error: function (rs) {
                        console.log(rs);
                    }
                });
            }

            genTable();

            $(document).on('click', '#btnExcel', function (e) {
                var a = document.createElement('a');
                var data_type = 'data:application/vnd.ms-excel';
                a.href = data_type + ', ' + encodeURIComponent($('div[id$=excelDiv]').html());
                document.body.appendChild(a);
                a.download = 'GEO_Retailers.xls';
                a.click();
                e.preventDefault();
            });

            $(document).on('click', '#btnPrint', function (e) {
                var css = '<link href="../css/style.css" rel="stylesheet" />';
                var printContent = document.getElementById("excelDiv");
                var WinPrint = window.open('', '', 'width=900,height=650');
                WinPrint.document.write(printContent.innerHTML);
                WinPrint.document.head.innerHTML = css;
                WinPrint.document.close();
                WinPrint.focus();
                WinPrint.print();
                WinPrint.close();
            });

            //var stk = [];
            //for (var i = 0; i < dValue.length; i++) {
            //    stk.push({
            //        lat: dValue[i].lat,
            //        lng: dValue[i].log,
            //        title: dValue[i].custName,
            //        description: dValue[i].custName
            //    });
            //}
            $(document).on('click', '#btnclose', function () {
                $("#dvMap").hide();
            });

            $(document).on('click', '#btnUntag', function () {

                if (confirm('Are you want Untag  Retailer..!')) {
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        async: false,
                        url: "rptCustomerGeotagging.aspx/untagRetailers",
                        data: "{'custCode':'" + $('#custCode').val() + "'}",
                        dataType: "json",
                        success: function (data) {
                            if (data.d == 'Sucess') {
                                alert('Sucessfully Untag Retailer..!');
                                genTable();
                                $("#dvMap").hide();
                            }
                            else {
                                alert(data.d);
                            }
                        },
                        error: function (result) {
                            alert(result);
                        }
                    });
                }
            });

            $(document).on('click', '#btnView', function (e) {

                var stk = [];
                $('#routeName').text($(this).closest('tr').find('td').eq(1).text());
                $('#custName').text($(this).closest('tr').find('td').eq(2).text());
                $('#custCode').val($(this).closest('tr').find('input[name="custCode"]').val());
                $('#currAddress').text('Current Address : ' + $(this).closest('tr').find('input[name="currAddress"]').val());




                var addrs = '';
               //var geocodingAPI = "https://maps.googleapis.com/maps/api/geocode/json?latlng=" + $(this).closest('tr').find('input[name="mlat"]').val() + "," + $(this).closest('tr').find('input[name="mlog"]').val() + "&key=ADDAAP_AIzaSyAER5hPywUW-5DRlyKJZEfsqgZlaqytxoU";


                //$.getJSON(geocodingAPI, function (json) {
                //    if (json.status == "OK") {
                //        var result = json.results[0];
                //        // emp["addrs"] = result.formatted_address;
                //        //  $x = $(".place");
                //        //   $($x[index]).text(result.formatted_address);
                //        $('#latAddress').text('Latitude Address : ' + result.formatted_address);
                //    }
                //    else {
                //        // $x = $(".place");
                //        // $($x[index]).text(json.status);
                //        $('#latAddress').text('Latitude Address : ' + json.status);

                //    }
                //});
				
                var url = "//nominatim.openstreetmap.org/reverse?format=json&lon=" + $(this).closest('tr').find('input[name="mlog"]').val() + '&lat=' + $(this).closest('tr').find('input[name="mlat"]').val() + "";
                $.ajax({
                    url: url,
                    async: false,
                    dataType: 'json',
                    success: function (data) {
                        addrs = data.display_name;
                        $('#latAddress').text('Latitude Address : ' + addrs);
                    }
                });


                stk.push({
                    lat: $(this).closest('tr').find('input[name="mlat"]').val(),
                    lng: $(this).closest('tr').find('input[name="mlog"]').val(),
                    title: 'Test',
                    description: 'Newtest'
                });
                markers = stk;
                var mapOptions = {
                    center: new google.maps.LatLng(markers[0].lat, markers[0].lng),
                    zoom: 15,
                    mapTypeId: google.maps.MapTypeId.ROADMAP
                };
                var infoWindow = new google.maps.InfoWindow();
                var map = new google.maps.Map(document.getElementById("mapslocation"), mapOptions);
                for (i = 0; i < markers.length; i++) {
                    var data = markers[i]
                    var myLatlng = new google.maps.LatLng(markers[i].lat, markers[i].lng);

                    marker = new google.maps.Circle({
                        center: myLatlng,
                        map: map,
                        strokeColor: '#cccccc',
                        strokeWeight: 2,
                        strokeOpacity: 0.5,
                        fillColor: '#0000df',
                        fillOpacity: 0.5,
                        radius: 0.5 * 1000
                    });
                    var marker = new google.maps.Marker({
                        position: myLatlng,
                        map: map,
                        title: data.title
                    });
                    (function (marker, data) {
                        google.maps.event.addListener(marker, "click", function (e) {
                            infoWindow.setContent(data.description);
                            infoWindow.open(map, marker);
                            alert('hi');
                        });
                    })(marker, data);
                }
                $("#dvMap").show();
            });
        });
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div class="container" style="max-width: 100%; width: 100%; text-align: right; padding-right: 50px">
            <asp:LinkButton ID="btnPrint" runat="Server" Style="padding: 0px 20px;" class="btn btnPrint" />
            <asp:LinkButton ID="btnExport" runat="Server" Style="padding: 0px 20px; display:none" class="btn btnPdf" />
            <asp:LinkButton ID="btnExcel" runat="Server" Style="padding: 0px 20px;" class="btn btnExcel" />
            <a name="btnClose" id="btnClose" type="button" style="padding: 0px 20px;" href="javascript:window.open('','_self').close();" class="btn btnClose"></a>
        </div>
        <div id="excelDiv" class="container" style="max-width: 100%; width: 100%">
            <div style="text-align: left; padding: 2px 50px; font-size: large">
                UnTagging Retailers
            </div>
            <div style="text-align: left; padding: 2px 50px;">
                <b>
                    <asp:Label ID="lblsf_name" runat="server"></asp:Label>
                    <asp:HiddenField ID="hidn_sf_code" runat="server" />
                    <asp:HiddenField ID="hFYear" runat="server" />
                    <asp:HiddenField ID="hFMonth" runat="server" />
                </b>
            </div>
            <div>
            </div>
            <div style="width: 95%; margin: 0px auto;">

                <asp:Label ID="lblyear" runat="server"></asp:Label>
                <br />
                <table id="cTable" class="newStly" border="1" style="border-collapse: collapse;">
                    <thead>
                        <tr>
                            <th>SlNo.</th>
                            <th>Retailer Name</th>
                            <th>Route Name</th>
                            <th>Create Date</th>
                            <th></th>
                        </tr>
                    </thead>
                    <tbody>
                    </tbody>
                </table>
            </div>
        </div>
        <br />
        <br />
        <div id="dvMap">
            <div style="width: 100%; text-align: right">
                <a class="btn btn-primary" id="btnclose">Close</a>
            </div>
            <div class="row">
                <div class="col-md-12" style="text-align: center; height: 379px">
                    <div id="mapslocation" style="width: 100%; height: 100%"></div>
                </div>
            </div>
            <br />
            <div class="row">
                <div class="col-sm-5">
                    <label style="font-size: small !important">Route Name : </label>
                    <label id="custName" style="font-size: small !important"></label>
                </div>
                <div class="col-sm-5">
                    <label style="font-size: small!important">Retailer Name : </label>
                    <label id="routeName" style="font-size: small!important"></label>
                </div>
                <div class="col-sm-2">
                    <input type="hidden" value="" id="custCode" />
                    <a id="btnUntag" class="btn btn-primary">UnTag</a>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-12">
                    <label id="currAddress" style="font-size: small!important"></label>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-12">
                    <label id="latAddress" style="font-size: small!important"></label>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
