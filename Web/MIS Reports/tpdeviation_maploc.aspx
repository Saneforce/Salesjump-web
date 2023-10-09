<%@ Page Language="C#" AutoEventWireup="true" CodeFile="tpdeviation_maploc.aspx.cs" Inherits="MIS_Reports_tpdeviation_maploc" %>

<!DOCTYPE html>
<html>
<head>
    <link href="../css/SalesForce_New/bootstrap-select.min.css" rel="stylesheet" />
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script src="https://maps.googleapis.com/maps/api/js?sensor=false&key=AIzaSyAER5hPywUW-5DRlyKJZEfsqgZlaqytxoU"></script>
    <script src="https://unpkg.com/@googlemaps/markerclusterer/dist/index.min.js"></script>
    <script src="https://developers.google.com/maps/documentation/javascript/examples/markerclusterer/markerclusterer.js"></script>



    <script src='https://kit.fontawesome.com/a076d05399.js' crossorigin='anonymous'></script>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.10.2/jquery.min.js"></script>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
    <script src="../MarkerLabel/MarkerLabel.js"></script>
    <script src="../js/lib/leaflet.js"></script>


    <link href="../css/locationmaster.css" rel="stylesheet" />
    <script src="../assets/js/markerclusterer_compiled.js"></script>
    <script src="https://unpkg.com/@googlemaps/markerclusterer/dist/index.min.js"></script>
    <script src="https://developers.google.com/maps/documentation/javascript/examples/markerclusterer/markerclusterer.js"></script>
</head>
<body>
    <script type="text/javascript">
        var markers = []; var tablemark = []; var SFCode = $("#<%=hSfCode.ClientID%>").val();
        var month = $("#<%=hFmonth.ClientID%>").val();
        var prvMarkers = [];
        var NavMarkers = []; var gmarkers = [];
        var lastOpenedInfoWindow;
        var lastAnimMark; var cardadd = [];
        var infoWins = []; var markers1 = [];
        var infoWindow;
        $(document).ready(function () {
            loadMap();
            Getlocation();
        });

        var datas = [];
        var map;
        var icon = {
            url: "../css/locationsvg/icon-google-maps-9.jpg", // url
            scaledSize: new google.maps.Size(26, 37), // size
        };
        function Getlocation() {
            var SFCode = $("#<%=hSfCode.ClientID%>").val();
            var month = $("#<%=hFmonth.ClientID%>").val();
            var year = $("#<%=hFyear.ClientID%>").val();
            $("#idloading").show();
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "tpdeviation_maploc.aspx/GetTaggedRetdate",
                data: "{'SFCode':'" + SFCode +"','div_code':'<%=Session["div_code"]%>','month':'" + month + "','year':'" + year + "'}",
                dataType: "json",
                success: function (data) {
                    markers = JSON.parse(data.d);
                    markers1 = markers;
                    // makerappend();
                    var groupedMarkers = groupMarkers(markers);
                    // Place markers on the map
                    placeMarkers(groupedMarkers);
                },
            });

        }
        function makerappend() {
            var marker, i, str;
            cardadd = markers;
            $('#Retmnu').html('');
            clearLocations(); //clearMarkers();
            str = "";
            $("#rmTags").hide();
            if (markers.length > 0) $("#rmTags").show();
            var bounds = new google.maps.LatLngBounds();
            infoWins = [];
            var goomarkers = []; var gmarkers = [];


            for (var $i = 0; $i < markers.length; $i++) {

                var stri = markers[$i].Retailer_Name;
                stri = stri.replace(/'/g, '');
                //strhtml = "<table><tr><td style='font-size:13px;padding:5px;margin-left: -555px;'><img id='propic' style='width:32px;height:32px;border-radius:50%;'src='http://fmcg.sanfmcg.com//photos/" + markers[$i].Visit_hours + "'><b>" + markers[$i].Retailer_Name + "</b><br>" + cardadd[$i].Retailer_phone + " </td></tr><tr><td class='tdt'><img class='svgs' src='../css/locationsvg/marker1.svg'></img></td><td class='tdt'><label class='names'>" + cardadd[$i].Retailer_Address + " </label></td></tr><tr><td style='font-size:13px;padding:5px'><i style='font-size:21px;' class='fa fa-mobile-phone'></i>  :  " + markers[$i].Retailer_phone + "</td></tr><tr><td class='tdt'><img class='svgs' src='../css/locationsvg/route2.svg'></img></td>:" + markers[$i].Route + "</td></tr><tr><td class='tdt'><img class='svgs' src='../css/locationsvg/store-svgrepo-com.svg'></img></td><td class='tdt'><label class='names'>:" + markers[$i].Doc_Special_Name + "</label></td></tr><tr></tr></table>";

                strhtml = "<table><tr><td class='tdt'><img  id='propic' style='width:32px;height: 33px;border-radius:50%;' src='http://fmcg.sanfmcg.com//photos/" + markers[$i].Visit_hours + "'></td><td class='tdt'style='width:300px;'><label class='heade'>" + markers[$i].Retailer_Name + "</label><br>" +
                    "<label class='names' style='margin-left:10px' >" + markers[$i].Retailer_phone + " </label></td></tr><tr><td class='tdt'><img class='svgs' src='../css/locationsvg/route2.svg'></img></td><td class='tdt'><label class='names'>" + markers[$i].Route + " </label></td></tr><tr><td class='tdt'><img class='svgs' src='../css/locationsvg/dollar_bill_money_stack.svg'></img></td><td class='tdt'><label class='names'>" + markers[$i].Order_Value + " </label></td></tr><tr><td class='tdt'><img class='svgs' src='../css/locationsvg/marker1.svg'></img></td><td class='tdt'><label class='names'>" + markers[$i].Retailer_Address + " </label></td></tr>" +
                    "<tr><td class='tdt'><img class='svgs' style='width:25px' src='../css/locationsvg/store-svgrepo-com.svg'></img></td><td class='tdt'><label class='names'>" + markers[$i].Doc_Special_Name + "</label></td></tr><table>";

                itm = {
                    coords: { lat: parseFloat(markers[$i].lat), lng: parseFloat(markers[$i].lng) },
                    content: strhtml,
                    status: markers[$i].Tag_stat,
                }
                goomarkers.push(itm);
            }

            var glemarkers = [];
            for (var i = 0; i < goomarkers.length; i++) {

                gmarkers.push(addMarker(goomarkers[i]));
                glemarkers = marker;


            }


            function addMarker(props) {
                if (props.status == 'Tagged') {
                    var marker = new google.maps.Marker({
                        position: props.coords,
                        icon: icon,//"css/locationsvg/marker.png",
                        map: map,

                    });
                    var myLatLng = new google.maps.LatLng(props.coords);
                    bounds.extend(myLatLng);
                }
                else {
                    var marker = new google.maps.Marker({
                        position: props.coords,
                        icon: icon,//"css/locationsvg/marker.png",
                        map: map,

                    });
                    var myLatLng = new google.maps.LatLng(props.coords);
                    bounds.extend(myLatLng);
                    marker.setMap(null);
                }

                addInfoWindow(marker, props.content);
                prvMarkers.push(marker);

                return marker;




            }
            //markerCluster = new MarkerClusterer(map, gmarkers,
            //    {
            //        imagePath:
            //            'https://developers.google.com/maps/documentation/javascript/examples/markerclusterer/m',
            //        gridSize: 50, // Adjust the grid size as needed
            //        maxZoom: 15
            //    });


            map.fitBounds(bounds);


            Start = 0; limit = 5;
            //loadaddrs(Start, limit);
        }

        function groupMarkers(markers) {
            var groupedMarkers = {};
            for (var i = 0; i < markers.length; i++) {
                var marker = markers[i];
                var key = marker.lat + "_" + marker.lng;
                if (!groupedMarkers[key]) {
                    groupedMarkers[key] = [];
                }
                groupedMarkers[key].push(marker);
            }
            return groupedMarkers;
        }

        var visit = '';
        function placeMarkers(groupedMarkers) {
            for (var key in groupedMarkers) {
                if (groupedMarkers.hasOwnProperty(key)) {
                    var markers = groupedMarkers[key];

                    markers.forEach(function (markerData) {
                        if (markerData.Order_Value != 0) {
                            var position = new google.maps.LatLng(markerData.lat, markerData.lng);
                            var marker = new google.maps.Marker({
                                position: position,
                                icon: icon,

                                map: map,
                                label: {
                                    text: markers.length.toString(),
                                    color: 'white',
                                    fontWeight: 'bold',


                                },
                                title: markerData.Retailer_Code,
                            });
                        } else {
                            var position = new google.maps.LatLng(markerData.lat, markerData.lng);
                            var marker = new google.maps.Marker({
                                position: position,
                                //icon:icon,
                                map: map,
                                label: {
                                    text: markers.length.toString(),
                                    color: 'white',
                                    fontWeight: 'bold',

                                },
                                title: markerData.Retailer_Code,
                            });
                        }


                        var activityDates = []; var visit = '';
                        activityDates.push(markerData.Activity_Date);

                        for (var i = 0; i < markers1.length > 0; i++) {
                            if (markers1[i].Retailer_Code == markerData.Retailer_Code) {
                                var dates = markers1[i].Activity_Date + ",";
                                visit += dates;
                            }

                        }
                        var stri = markerData.Retailer_Name;
                        stri = stri.replace(/'/g, '');
                        strhtml = "<table><tr><td class='tdt'><img  id='propic' style='width:32px;height: 33px;border-radius:50%;' src='http://fmcg.sanfmcg.com//photos/" + markerData.Visit_hours + "'></td><td class='tdt'style='width:300px;'><label class='heade'>" + markerData.Retailer_Name + "</label><br>" +
                            "<label class='names' style='margin-left:10px' >" + markerData.Retailer_phone + " </label></td></tr><tr><td class='tdt'><img class='svgs' src='../css/locationsvg/dollar_bill_money_stack.svg'></img></td><td class='tdt'><label class='names'>" + markerData.Order_Value + " </label></td></tr><tr><td class='tdt'><img class='svgs' src='../css/locationsvg/route2.svg'></img></td><td class='tdt'><label class='names'>" + markerData.Route + " </label></td></tr><tr><td class='tdt'><img class='svgs' src='../css/locationsvg/marker1.svg'></img></td><td class='tdt'><label class='names'>" + markerData.Retailer_Address + " </label></td></tr>" +
                            "<tr><td class='tdt'><img class='svgs' style='width:25px' src='../css/locationsvg/store-svgrepo-com.svg'></img></td><td class='tdt'><label class='names'>" + markerData.Doc_Special_Name + "</label></td></tr><tr><td class='tdt'><img class='svgs' style='width:25px' src='../css/locationsvg/calendar-50.svg'></img></td><td><label class='names'>" + visit + "</label></td></tr><table>";

                        var infoContent = '<strong>' + strhtml + '</strong> ';
                        addInfoWindow(marker, infoContent)
                        prvMarkers.push(marker);

                    });
                }
            }
            Start = 0; limit = 5;
            loadaddrs(Start, limit)
        }
        var lastScrollTop = 0;

        $('#Retmnu').on('scroll', function () {
            var div = $(this).get(0);
            var scrollTop = div.scrollTop;
            if (scrollTop > lastScrollTop) {
                var scrollHeight = div.scrollHeight;
                var clientHeight = div.clientHeight;
                var scrollBottom = scrollHeight - (scrollTop + clientHeight);
                var threshold = 20;
                if (scrollBottom <= threshold) {
                    var limit = 20;
                    Start = Start + limit;
                    loadaddrs(Start, limit);
                }
            }
            lastScrollTop = scrollTop;
        });

        function loadaddrs(Start, limit) {

            const maxi = Math.min(markers.length, 20);
            const max = maxi + Start;
            cardadd = markers;
            for (var $i = Start; $i < cardadd.length; $i++) {
                str = "";
                var img = ''; var tags = ''; var ppic = '';
                if (cardadd[$i].Visit_hours != '' && cardadd[$i].Visit_hours != null && cardadd[$i].Visit_hours != 'null') {
                    ppic = "<img  id='propic' style='width:32px;height: 33px;border-radius:50%;' src='http://fmcg.sanfmcg.com//photos/" + cardadd[$i].Visit_hours + "'>";
                }
                else {
                    ppic = "<img style='width:32px;height: 33px;'  class='svgs' src='../css/locationsvg/store-shopper-svgrepo-com.svg'></img>";
                }
                if (cardadd[$i].OrderTyp == 'Field Order') {
                    img = "<img style='width: 22px;height: 22px;float:right' src='https://pngimg.com/uploads/pin/pin_PNG81.png'>";
                    tags = "<label class='tagged' style='float:right'>Field Order</label>";
                    latlog = "<label class='names'>" + cardadd[$i].lat + ", " + cardadd[$i].long + "</label>";
                } else if (cardadd[$i].OrderTyp == 'Phone Order') {
                    tags = "<label class='untagged'  style='float:right'>Phone Order</label>";
                    latlog = "<label class='names'>" + cardadd[$i].lat + ", " + cardadd[$i].long + "</label>";
                }
                var sl_no = cardadd[$i].RowNum - 1;
                str += "<div class='card'style='margin:15px;padding:10;background-color:white;' ><table><tr><td class='tdt'>" + ppic + "</td><td class='tdt' style='width: 378px'><a href='#'  class='heade'  id='retailercode' onclick='openRetWin(" + $i + ")'>" + cardadd[$i].Retailer_Name + "</a>" + tags + "<br>" +
                    "<label class='names' style='margin-left:10px' >" + cardadd[$i].Retailer_phone + " </label></td></tr><tr><td class='tdt'><img class='svgs' src='../css/locationsvg/dollar_bill_money_stack.svg'></img></td><td class='tdt'><label class='names'>" + markers[$i].Order_Value + " </label></td></tr><tr><td class='tdt'><img class='svgs' src='../css/locationsvg/route2.svg'></img></td><td class='tdt'><label class='names'>" + cardadd[$i].Route + " </label></td></tr><tr><td class='tdt'><img class='svgs' src='../css/locationsvg/location-crosshairs.svg'></img></td><td class='tdt'>" + latlog + "</td></tr><tr><td class='tdt'><img class='svgs' src='../css/locationsvg/marker1.svg'></img></td><td class='tdt'><label class='names'>" + cardadd[$i].Retailer_Address + " </label></td></tr>" +
                    "<tr><td class='tdt'><img class='svgsh' style='width:32px;height: 33px;'  src='../css/locationsvg/store-svgrepo-com.svg'></img></td><td class='tdt'><label class='names'>" + markers[$i].Doc_Special_Name + "</label></td></tr><tr><td class='tdt'><img class='svgs' style='width:25px' src='../css/locationsvg/calendar-50.svg'></img></td><td><label class='names'>" + markers[$i].Activity_Date + "</label></td></tr><table></div > ";

                $('#Retmnu').append(str);


            }

            $("#idloading").hide();
        }

        function addInfoWindow(marker, message) {

            var infoWindow = new google.maps.InfoWindow({
                content: message
            });
            infoWins.push(infoWindow);
            google.maps.event.addListener(marker, 'click', function () {
                closeLastOpenedInfoWindow();
                infoWindow.open(map, marker);

                lastOpenedInfoWindow = infoWindow;
                //if (lastAnimMark != undefined) lastAnimMark.setAnimation(null);
                //marker.setAnimation(google.maps.Animation.BOUNCE);
                //lastAnimMark = marker;
            });

        }


        function closeLastOpenedInfoWindow() {
            if (lastOpenedInfoWindow) {
                lastOpenedInfoWindow.close();
            }
        }



        function openRetWin(i) {
            if (lastOpenedInfoWindow != null) lastOpenedInfoWindow.close();
            infoWins[i].open(map, prvMarkers[i]);
            lastOpenedInfoWindow = infoWins[i];
            if (lastAnimMark != undefined) lastAnimMark.setAnimation(null);
            prvMarkers[i].setAnimation(google.maps.Animation.BOUNCE);
            lastAnimMark = prvMarkers[i];
            stopani(prvMarkers[i]);
            map.setZoom(15);

        }
        function stopani(prvMarkers) {
            prvMarkers.setAnimation(null);
        }
        function highlightMarker(i) {
            infoWins[i].open(map, prvMarkers[i]);

            if (markers[i].getAnimation() !== null) {
                markers[i].setAnimation(null);
            } else {

                markers[i].setAnimation(google.maps.Animation.BOUNCE);
            }
        }



        var markerCluster = null;

        function clearLocations() {
            if (prvMarkers.length > 0) {
                for (var i = 0; i < prvMarkers.length; i++) {
                    prvMarkers[i].setMap(null);
                }
                for (var i = 0; i < locstatmark.length; i++) {
                    locstatmark[i].setMap(null);
                }
                prvMarkers = []; locstatmark = [];
                NavMarkers = [];
            }
            if (markerCluster != null) {
                markerCluster.clearMarkers(); markerCluster = null;
            }
        }

        function initMarkers() {
            path = new google.maps.MVCArray();
            poly = new google.maps.Polyline({
                strokeColor: '#FF8200',
                strokeOpacity: 0.5,
                strokeWeight: 15
            });
            poly.setMap(map);

            infoWindow = new google.maps.InfoWindow()
            sStr = '<ul>';
            // Display multiple markers on a map
            CusCnt = 0; tpval = 0; tltrs = 0;
            moveMarker(0);
        }

        function loadMap() {

            var mapOptions = {
                center: new google.maps.LatLng(17.377631, 78.478603),
                zoom: 5
            }
            map = new google.maps.Map(document.getElementById("sample"), mapOptions);


        }
    </script>


    <form id="form1" runat="server">
        <asp:HiddenField ID="hSfCode" runat="server" />
        <asp:HiddenField ID="hFmonth" runat="server" />
        <asp:HiddenField ID="hFyear" runat="server" />
        <div id="tagtyp" style="min-width: 450px; width: 250px; min-height: 780px; max-height: 780px; overflow: scroll; float: left">
            <div id="Retmnu" style="min-width: 450px; width: 250px; min-height: 780px; max-height: 780px; overflow: scroll;"></div>
        </div>

        <div id="sample" style="height: 800px;"></div>
        <div id="idloading" style="display: none; position: absolute; top: 0px; left: 0px; width: 100% !important; height: 100% !important; margin: auto; text-align: center; line-height: 100vh; background-color: #f5f5f569;">
            <b style="padding: 10px 10px; background: white; box-shadow: 0px 0px 1px black;"><%--<span style="width: 150px; height: 50px; display: none;">
                                <img src="https://flevix.com/wp-content/uploads/2019/07/Curve-Loading.gif" style="width: 150px; height: 150px;" /></span>--%>Loading...</b>
        </div>

    </form>


</body>

</html>

