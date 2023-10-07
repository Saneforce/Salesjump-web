<%@ Page Title="Location Finder" Language="C#" AutoEventWireup="true" CodeFile="Location_Finder.aspx.cs"
    Inherits="Default4" %>

<html>
<head>
    <link href="css/font-awesome.css" rel="stylesheet" type="text/css" />
    <link href="css/DCR_Entry.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="treeView/css/SmartTreeStyle/jquery.smarttree.css">
    <link rel="stylesheet" href="MonthViewer/kendo.common-material.min.css" />
    <link rel="stylesheet" href="MonthViewer/kendo.material.min.css" />
    <link rel="stylesheet" href="MonthViewer/kendo.dataviz.min.css" />
    <link rel="stylesheet" href="MonthViewer/kendo.dataviz.material.min.css" />
    <style type="text/css">
        #map_canvas, #tbMain, #modal, body
        {
            width: 100%;
            height: 100%;
            padding: 0px;
            margin: 0px;
        }
        #modal
        {
            display: none;
            position: absolute;
            background-color: rgba(0,0,0,0.5);
            z-index: 10000;
            line-height: 50;
            height: 100%;
            text-align: center;
            color: #fff;
        }
        #vstDet > ul
        {
            list-style-type: none;
            -webkit-margin-start: 0em;
            -webkit-margin-before: 0em;
            -webkit-padding-start: 0px;
        }
        #vstDet > ul > li
        {
            margin-bottom: 5px;
        }
        table
        {
            border-collapse: collapse;
        }
        .k-icon
        {
            margin: 13px 5px;
        }
        #vstCnt
        {
            position: absolute;
            display: block;
            width: 20px;
            height: 20px;
            margin: -27px 76px;
            line-height: 20px;
            text-align: center;
            border-radius: 90%;
            font-size: 75%;
            background: rgb(82, 208, 139);
        }
        .ColHeader
        {
            display: block;
            background: #f1f1f1;
            margin: -1px -2px;
            padding: 5px;
        }
        form
        {
            margin: 0px;
        }
        .pad
        {
            padding: 0px;
        }
        .mnu-bt, .home-bt
        {
            margin: 3px;
        }
        .my-custom-class-for-label1, .Ordered
        {
        }
        .my-custom-class-for-label
        {
            width: 20px;
            height: 20px;
            padding: 2px;
            border: 1px solid #eb3a44;
            border-radius: 100%;
            background: #fee1d7;
            text-align: center;
            line-height: 20px;
            font-weight: bold;
            font-size: 14px;
            color: #eb3a44;
        }
        .my-custom-class-for-label2
        {
            width: 20px;
            height: 20px;
            padding: 2px;
            border: 1px solid #eb3a44;
            border-radius: 100%;
            background: #fee1d7;
            text-align: center;
            line-height: 20px;
            font-weight: bold;
            font-size: 14px;
            color: #eb3a44;
        }
        .Ordered > .my-custom-class-for-label
        {
            color: #00b0ff;
            background: #d3f1ff;
            border: 1px solid #00b0ff;
        }
        .Ordered > .my-custom-class-for-label2
        {
            color: #d2ded7;
            background: #51bd7d;
            border: 1px solid #00b0ff;
        }
        .my-custom-class-for-label1:after, .Ordered:after
        {
            position: fixed;
            border: solid transparent;
            content: " ";
            height: 0;
            width: 0;
            pointer-events: none;
            border-color: rgba(255, 255, 255, 0);
            border-top-color: #e8616b;
            border-width: 10px;
            margin: 3px;
            margin-top: -4px;
        }
        .Ordered:after
        {
            border-top-color: #00b0ff;
        }
        
        ul > li
        {
            padding: 5px 8px;
            border: 1px #dcdcdc dashed;
            margin-top: -1px;
        }
        ul > li:hover
        {
            background: #fee1d7;
        }
	  .ztree .ztree-node:hover {
            background-color:#b6e8ff;
        }

        .ztree .ztree-node.focus {
            background-color: #b6e8ff;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div id="modal">
        Loading....</div>
    <table id="tbMain">
        <tr>
            <td colspan="3" valign="top" style="height: 1%; background-color: #f1f1f1; border-bottom: solid 1px #cacaca">
                <div class="pad HDBg">
                    <a href="#" class="button mnu-bt" onclick="toggleMenu()"><i class="fa fa-bars"></i>
                    </a><a runat="server" onclick="GotoHome();return false;" id="aHome" class="button home-bt">
                        Home</a><div class='hCap' style="width: 300px; overflow: hidden;">
                            Location Finder</div>
                    <div class='hCap' id="spWT" style="width: 230px; white-space: nowrap; overflow: hidden;
                        text-overflow: ellipsis;">
                        </span></div>
                    <div class='hCap' id="spDis" style="width: 300px; white-space: nowrap; overflow: hidden;
                        text-overflow: ellipsis">
                    </div>
                    <div class='hCap' id="spRT" style="width: 300px; white-space: nowrap; overflow: hidden;
                        text-overflow: ellipsis">
                    </div>
                </div>
                <table style="display: none">
                    <tr>
                        <td>
                            Employee Name :
                        </td>
                        <td valign="top">
                            <asp:DropDownList ID="selSF" onchange="getMyTP();Callback()" runat="server" SkinID="ddlRequired"
                                Width="210px" CssClass="ddl">
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="min-width: 250px;" valign="top">
                <div class="ColHeader" style="display: block">
                    Employee Name :
                    <input type="text" id="txtFilter" name="txtFilter" onkeyup="return getListView()"
                        autocomplete="off" /></div>
                <div id="sideMnu" style="max-width: 262px;">
                    <div id="smartTree" style="display: block; height: 320px; min-width: 250px; width: 250px;
                        overflow: auto;">
                    </div>
                    <ul id="filmenu" style="display: block; height: 320px; min-width: 250px; width: 250px;
                        overflow: auto;">
                    </ul>
                    <div id="calendar">
                    </div>
                </div>
            </td>
            <td width="100%" valign="top">
                <div id="map_canvas" class="mapping">
                </div>
            </td>
            <td valign="top" style="width: 100% !important;" id="idDets">
                <div style="display: none">
                    <div class="panel-heading" style="display: block; white-space: nowrap; padding: 10px 15px;
                        background: #ff865c; color: #fff;">
                        Kilometers Traveled</div>
                    <span id="total" style="display: block; white-space: nowrap; padding: 10px 15px;
                        font-weight: bold; border: solid 1px #000; border-radius: 0px 0px 5px 5px; border-top-width: 0px;">
                    </span><small><small>
                        <br>
                    </small></small>
                </div>
                <div style="background: #f1f1f1;">
                    <table style="width: 100%">
                        <tr>
                            <td>
                                Start
                            </td>
                            <td>
                                :
                            </td>
                            <td id="sTM" style="width: 100%; color: #00b0ff; background-color: #dbdbdb; padding: 5px;">
                            </td>
                        </tr>
                        <tr>
                            <td>
                                End
                            </td>
                            <td>
                                :
                            </td>
                            <td id="eTM" style="width: 100%; color: #00b0ff; background-color: #dbdbdb; padding: 5px;">
                            </td>
                        </tr>
                    </table>
                </div>
                <div id="mvstDet" style="display: block; height: 90.5%; white-space: nowrap; font-weight: bold;
                    border: solid 1px #000; border-radius: 0px 0px 5px 5px; border-top-width: 0px;">
                    <div class="panel-heading" style="display: block; white-space: nowrap; padding: 10px 15px;
                        background: #ff865c; color: #fff;">
                        Calls Detail<span id="vstCnt"></span><span id="dspDt" style="float: right;"></span></div>
                    <div id="vstDet" style="display: block; padding: 5px; min-width: 253px; height: 83vh;
                        overflow: auto;">
                    </div>
                    <div style="display: block; background: #dcdcdc;">
                        <table style="width: 100%; color: black; font: 10px tahoma; font-weight: bold">
                            <tr>
                                <td style="padding: 5px;">
                                    Tot.POB (value) : <span id="tpval" style="color: Green"></span>
                                </td>
                                <td style="padding: 5px;">
                                    Tot.POB (Ltrs) : <span id="tltrs" style="color: Green"></span>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </td>
        </tr>
    </table>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.10.2/jquery.min.js"></script>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
    <script type="text/javascript" src="treeView/js/jquery.smarttree.all.js"></script>
    <script type="text/javascript" src="treeView/js/jquery.mousewheel.min.js"></script>
    <script src="MonthViewer/kendo.all.min.js"></script>
    <script src="https://maps.googleapis.com/maps/api/js?sensor=false&key=AIzaSyAER5hPywUW-5DRlyKJZEfsqgZlaqytxoU"></script>
    <script src="MarkerLabel/MarkerLabel.js"></script>
    <script src="js/lib/leaflet.js"></script>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css">

    <script type="text/javascript">
var markers = []; var tablemark = []; var cardadd = []; var sfdets = '';
            var prvMarkers = []; var Start = ''; var limit = ''; var St = ''; var le = '';
            var NavMarkers = []; var gmarkers = [];
            var lastOpenedInfoWindow;
            var lastAnimMark;
            var infoWins = [];
            var infoWindow;
            var slatlng = "";
            var CusCnt = 0, tpval = 0, tltrs = 0;
            var path;
            var poly;
            var KMTot = 0;
            var z = 14;
            var Loaded = false;
        function GotoHome() {
            if ("<%=Session["sf_type"] %>" == "2") {
                window.location.href = "DashBoard1.aspx"
            }
            if ("<%=Session["sf_type"] %>" == "3" && "<%=Session["sf_code"]%>" == "admin") {
                window.location.href = "DashBoard.aspx"
            }
        }

        var datas = [];

var icon = {
                url: "css/locationsvg/marker.png", // url
                scaledSize: new google.maps.Size(24, 30), // size
            };
        function getListView() {
            var old = datas;
            $("#smartTree").css("display", "none");
            $("#filmenu").css("display", "block");
            var txtFill = $('#txtFilter').val();
            if (txtFill.length > 0) {
                old = datas.filter(function (obj) {
                    return (obj.name.toString().toLocaleLowerCase().indexOf(txtFill.toString().toLocaleLowerCase()) >= 0);
                });
                $('#filmenu').empty();
                var li = $('#filmenu');
                if (old.length > 0) {
                    for (var k = 0; k < old.length; k++) {

                        li.append("<li class='ztree-node ellipsis' data-toggle='tooltip' title='" + old[k].name.toString() + "'> <a class='listLink' href='#' sfCode='" + old[k].id.toString() + "'>" + old[k].name + "</a></li>");
                    }
                }
                else {
                    li.append("<li class='ztree-node ellipsis' style='color:red'>No match Found..!</li>");
                }
            }
            else {
                $("#smartTree").css("display", "block");
                $("#filmenu").css("display", "none");
            }
        }

        $(document).ready(function () {
            $(document).on('click', '.listLink', function () {
                $("#selSF").val($(this).attr('sfCode'));
                getMyTP();
                Callback();
            });
        });

        function genTreeView() {
            $("#smartTree").css("display", "block");
            $("#filmenu").css("display", "none");
            $.fn.smartTree.init($("#smartTree"), {
                data: {
                    simpleData: {
                        enable: true
                    }
                },
                edit: {
                    enable: true,
                    drag: {}
                },
                callback: {
                    onClick: function (e, itm, n) {

                        $("#selSF").val(n.id);
                        $("#modal").css('display', 'block');
                        getMyTP();
                        Callback();
                        return true;
                    },
                    beforeDrag: function () {
                        console.log("=beforeDrag");
                        return true;
                    },
                    beforeDragOpen: null,
                    beforeDrop: null,
                    beforeEditName: null,
                    beforeRename: null,
                    onDrag: function () {
                        console.log("=onDrag")
                    },
                    onDragMove: function () {
                        console.log("=onDragMove")
                    },
                    onDrop: function () {
                        console.log("=onDrop")
                    },
                    onRename: null
                }
            }, datas);

        }

        toggleMenu = function () {
            if ($("#sideMnu").css("margin-left") == "0px") {
                $("#sideMnu").css("margin-left", "-250px");
                $("#sideMnu").parent().css("display", "none");
                $("#idDets").css("display", "none");
            } else {
                $("#sideMnu").css("margin-left", "0px");
                $("#sideMnu").parent().css("display", "table-cell");
                $("#idDets").css("display", "table-cell");
            }
        }

        //Multiple Markers

        var infoWindow;
        var slatlng = "";
        var CusCnt = 0, tpval = 0, tltrs = 0;
        var markers = [];
        var prvMarkers = [];var locstatmark = [];
        var NavMarkers = [];
        var path;
        var poly;
        var map;
        var KMTot = 0;
        var z = 14;
        var Loaded = false;
	
        selDt = "";
        cdt = new Date();
        selDt = cdt.getFullYear() + '-' + (cdt.getMonth() + 1) + '-' + cdt.getDate() + ' 00:00:00.000';
	
        google.maps.event.addDomListener(window, 'load', initMap);
        function initMap() {
            let clat = 13;
            let clng = 80;
            if ('<%=Session["div_code"]%>' == '101') {
                clat = 5.9506081;
                clng = -0.6606011;
            }
            else if ('<%=Session["div_code"]%>' == '140') {
                clat = 11.9980465;
                clng = 8.470859;
            }
            var mapOptions = {
                mapTypeId: 'roadmap', zoom: 20, center: { lat: clat, lng: clng }
            };
            map = new google.maps.Map(document.getElementById("map_canvas"), mapOptions);
            $("#modal").css('display', 'block');
            Callback(selDt);
            getMyTP(selDt);
        }

        var timer;
        function Callback(DtTm, movMark) {
			var slno = 0;
            if (!movMark) movMark = false;
            if (!DtTm) DtTm = selDt;
            KMTot = 0;
            $.get("/server/db.php?axn=get/track&SF_Code=" + $("#selSF").val() + "&Dt=" + DtTm + "",
                function (response) {
                    if (movMark == true) {
                        lastIndx = (markers.length > 0) ? markers.length : 0;
                        var newMarkers = JSON.parse(response) || [];
                        markers = $.merge(markers, newMarkers);
                        if (newMarkers.length > 0) moveMarker(lastIndx);

                    } else {
                        markers = JSON.parse(response);
                        clearLocations();
                        if (markers.length > 0) initMarkers();
                    }


                    cdt = new Date();
                    $("#modal").css('display', 'none');
                    if (selDt == cdt.getFullYear() + '-' + (cdt.getMonth() + 1) + '-' + cdt.getDate() + ' 00:00:00.000') {

                        window.clearTimeout(timer);
                        timer = setTimeout(function () {
                            var aSync;
                            if (markers.length > 0) {
                                lastDt = markers[markers.length - 1].DtTm.date + '.000';
                                aSync = true;
                            } else {
                                lastDt = selDt;
                                aSync = false;
                            }
                            Callback(lastDt, aSync);
                        }, 1000 * 5);
                    }
                })
                .fail(function (response) {
                    $("#modal").css('display', 'none');
                    console.log("Connection Failed");
                });
        }

        function clearLocations() {
            for (var i = 0; i < prvMarkers.length; i++) {
                prvMarkers[i].setMap(null);
            }
            for (var i = 0; i < locstatmark.length; i++) {
                        locstatmark[i].setMap(null);
                    }
                    prvMarkers = [];locstatmark = [];
            NavMarkers = [];
            if (poly != null) {
                poly.setPath([]);
                poly.setMap(null);
            }

            $("#sTM").html("&nbsp;");
            $("#eTM").html("&nbsp;");
            $("#tpval").html(roundNumber(0, 2));
            $("#tltrs").html(roundNumber(0, 2));
            $("#vstCnt").html(0);
            $("#vstDet").html("");
        }
	var slno = 0;
        var pPos = null;
        function moveMarker(lastIndx) {
		var slno = 0;
            var latLng = new google.maps.LatLng(49.47805, -123.84716);
            var homeLatLng = new google.maps.LatLng(49.47805, -123.84716);


            var im = 'img/bluecircle.png';
            var position, marker, i;
		infoWins=[];
            var src = new google.maps.LatLng(markers[0].lat, markers[0].lng);

            //                               markers = markers.filter(function (a) {
            //                        return (a.lat !='');
            //                    });
            Vstlbl = 0;
            for (i = lastIndx; i < markers.length; i++) {

                if (markers[i].lat != '') {
                    pos = new google.maps.LatLng(markers[i].lat, markers[i].lng);

                    if (slatlng.indexOf(markers[i].lat + ":" + markers[i].lng + ";") > -1) {
                        var rews = new RegExp(markers[i].lat + ":" + markers[i].lng + ";", "gi");
                        var re = new RegExp(markers[i].lat + ":" + markers[i].lng, "gi");
                        rptcnt = slatlng.replace(re, '').length - slatlng.replace(rews, '').length;
                        lt = pos.lat() + (rptcnt / (1000000 - 7));
                        lg = pos.lng() + (rptcnt / (100000 - 7));
                        position = new google.maps.LatLng(lt, lg);
                    }
                    else
                        position = pos;

                    if (i == 0 || i == markers.length - 1 || markers[i].ordfld == 0) {


                        if (markers[i].ordfld == 0) {
                            Vstlbl++;

                            var marker = new MarkerWithLabel({
                                position: position,
                                map: map,
                                icon: '/img/sticker/empty.png',
                                shadow: '/img/sticker/bubble_shadow.png',
                                transparent: '/img/sticker/bubble_transparent.png',
                                draggable: false,
                                raiseOnDrag: false,
                                labelContent: "<div class='" + ((markers[i].typ == '2') ? 'my-custom-class-for-label2' : 'my-custom-class-for-label') + "'>" + Vstlbl.toString() + "</div>",
                                labelAnchor: new google.maps.Point(13, 65),
                                labelClass: ((markers[i].POB_Value != '0' && markers[i].POB_Value != null) ? "Ordered" : "my-custom-class-for-label1"), // the CSS class for the label
                                labelInBackground: false
                            });
                        }
                        else {

                            NSAddrss = "";
                            var url = "//nominatim.openstreetmap.org/reverse?format=json&lon=" + parseFloat(markers[i].lng) + '&lat=' + parseFloat(markers[i].lat) + "";
                            $.ajax({
                                url: url,
                                async: false,
                                dataType: 'json',
                                success: function (data) {

                                    console.log(markers[i].lat + ":" + markers[i].lng);
                                    NSAddrss = data.display_name;
                                }
                            });
                            marker = new google.maps.Marker({
                                position: position,
                                map: map,
                                title: markers[i].DtTime,
                                label: {
                                    text: NSAddrss,
                                    color: "#0300d1",
                                    fontWeight: "bold",
                                    fontSize: "16px",
                                    backgroundColor: "#ffffff"
                                }
                            });
				locstatmark.push(marker);
                        }

                        slatlng += markers[i].lat + ":" + markers[i].lng + ";"
                        //prvMarkers.push(marker);

                        if (i > 0 && NavMarkers.length < 1 && markers[i].ordfld != 0) {
                            marker.setIcon(im);
                            NavMarkers.push(marker);
                        }
					    var customer = '';
                        if (markers[i].typ == 2) {
                          customer = '(Distributor)';
                        } else {
                           customer = '';
                        }

                        // console.log(markers);

                        if (markers[i].ordfld == 0) {

                            dte = new Date(markers[i].DtTime);
                            dy = dte.getDate();
                            mn = dte.getMonth() + 1;
                            hh = dte.getHours();
                            mm = dte.getMinutes();
                            ss = dte.getSeconds()
                            ddmm = ((dy < 10) ? '0' : '') + dy + ' / ' + ((mn < 10) ? '0' : '') + mn + ' / ' + dte.getFullYear();
                            sHH = ((hh > 12) ? hh - 12 : hh)
                            sHH = ((sHH < 10) ? '0' : '') + sHH
                            $("#dspDt").html(ddmm);
                            if (markers[i].ordfld == 0) {
                                CusCnt++;
                                tpval += ((markers[i].POB_Value != '0' && markers[i].POB_Value != null) ? parseFloat(markers[i].POB_Value) : 0);
                                tltrs += ((markers[i].net_weight_value != '0') ? parseFloat(markers[i].net_weight_value) : 0);
                                sStr += '<li onclick="openRetWin(' + slno + ')"><div style="display:block;"><table style="width:100%"><tr style="background:#dcdcdc;"><td style="padding:5px ;border-left:solid 2px #dcdcdc;"><font style="color:darkblue;font:15px tahoma;">' + markers[i].Trans_Detail_Name + ''+customer+'</td>'
                                        + '<td style="padding:5px;border-right:solid 2px #dcdcdc;"><a style="color:#00303f; font:bold 10px verdana;float: right;">' + sHH + ":" + ((mm < 10) ? '0' : '') + mm + ":" + ((ss < 10) ? '0' : '') + ss + " " + ((hh > 11) ? 'PM' : 'AM') + '</a></td></tr>'
                                        + '<tr><td style="padding:5px;border-left:solid 2px #dcdcdc;"><font style="color:black;font:11px tahoma;">Start Time : ' + markers[i].StartOrder_Time + '</font></td><td style="padding:5px;border-right:solid 2px #dcdcdc;"><font style="color:black;font:11px tahoma;">End Time :' + markers[i].EndOrder_Time + '</font></td></tr>'
                                        + '<tr><td style="padding:5px;border-left:solid 2px #dcdcdc;"><font style="color:black;font:11px tahoma;">POB ( Value ) : ' + ((markers[i].POB_Value != '0' && markers[i].POB_Value != null) ? '' + markers[i].POB_Value : '-') + '</td>'
                                        + '<td style="padding:5px;border-right:solid 2px #dcdcdc;"><font style="color:black;font:11px tahoma;">POB ( Liters ) : ' + ((markers[i].net_weight_value != '0') ? '' + markers[i].net_weight_value : '-') + '</td></tr>'
                                        + '<tr><td style="padding:5px;border-left:solid 2px #dcdcdc;"><font style="color:black;font:11px tahoma;font-weight: bold;">' + markers[i].OrderType + '</font></td><td style="padding:5px;border-right:solid 2px #dcdcdc;"><font style="color:black;font:11px tahoma;font-weight: bold;">' + markers[i].Remarks + '</font></td></tr>' +
                                        '<tr><td colspan=2 style="padding:5px;font:12px verdana;border:solid 2px #dcdcdc;border-top-width:0px;border-radius: 0px 0px 5px 5px;"><font style="color:darkgreen;"></font></td></tr>'
                                        + '</table></div></li>';
                                marker.setIcon('img/shop.png');
				slno ++;

                            }
				var place = ''; var vsplace = ''; var latitude1 = ''; var longitude1 = ''; var latitude2 = ''; var longitude2 = ''; var unit = ''; var meters = ''; var Km = ''
                                latitude1 = markers[i].geo_lat; longitude1 = markers[i].geo_log; latitude2 = markers[i].lat; longitude2 = markers[i].lng; unit = 'K'

                                var radlat1 = Math.PI * latitude1 / 180;
                                var radlat2 = Math.PI * latitude2 / 180;
                                var theta = longitude1 - longitude2;
                                var radtheta = Math.PI * theta / 180;
                                var dist = Math.sin(radlat1) * Math.sin(radlat2) + Math.cos(radlat1) * Math.cos(radlat2) * Math.cos(radtheta);
                                if (dist > 1) {
                                    dist = 1;
                                }
                                dist = Math.acos(dist);
                                dist = dist * 180 / Math.PI;
                                dist = dist * 60 * 1.1515;
                                if (unit == "K") {
                                    dist = dist * 1.609344
                                    //var totalkm = Math.round(dist);
                                    var totalkm = dist.toFixed(3)
                                    var dis = dist * 1000;
                                    var total = Math.round(dis);
                                }


                                if (markers[i].geo_lat == '' && markers[i].geo_log == '') {
                                    var totaldis = "Location Not Fetched/Captured";
                                }
                                else {
                                    var totaldis = totalkm + ' Km ';
                                }

                                var geoAddress = getAddress(markers[i].geo_lat, markers[i].geo_log);

                                var geoadd = '';
                                geoadd = markers[i].GeoAddrs;
                                if (geoadd == '' || geoadd == 'NA') {
                                    var geoAddress = getAddress(markers[i].geo_lat, markers[i].geo_log);
                                    var url = "//nominatim.openstreetmap.org/reverse?format=json&lon=" + parseFloat(markers[i].geo_log) + '&lat=' + parseFloat(markers[i].geo_lat) + "";
                                    $.ajax({
                                        url: url,
                                        async: false,
                                        dataType: 'json',
                                        success: function (data) {
                                            $x = $(".place");
                                            place = data.display_name;
                                        }
                                    });
                                }
                                else {
                                    place = geoadd;
                                }



                                vsplace = markers[i].addrs;
                                var customer = '';
                                if (markers[i].typ == 2) {
                                    customer = '(Distributor)';
                                } else {
                                    customer = '';
                                }
                                strhtml = '<div style="display:block;width:500px;"><table style="width:100%" border="1"><tr><td style="width:50%;padding:0px 5px 10px 5px ;border-bottom:solid 2px #dcdcdc;text-transform: capitalize;"><font style="color:darkblue;font:15px tahoma;">' + markers[i].Trans_Detail_Name + '' + customer + ' <br><font style="color:darkgreen;"> ' + markers[i].ListedDr_Address1 + '</font> </td>'
                                    + '<td style="width:50%;padding:0px 5px 10px 5px ;border-bottom:solid 2px #dcdcdc;"><font style="color:#00303f; font:bold 12px verdana;float: right;">' + markers[i].OrderType + '</font><br><a style="color:#00303f; font:bold 10px verdana;float: right;">' + markers[i].DtTime + '</a></td></tr>'
                                    + '<tr><td style="padding:5px;"><font style="color:black;font:11px tahoma;">POB ( Value ) : ' + ((markers[i].POB_Value != '0' && markers[i].POB_Value != null) ? '' + markers[i].POB_Value : '-') + '</td>'
                                    + '<td style="padding:5px;"><font style="color:black;font:11px tahoma;">POB ( Liters ) : ' + ((markers[i].net_weight_value != '0') ? '' + markers[i].net_weight_value : '-') + '</td></tr>'
                                    + '<tr><td style="padding:5px;"><font style="color:black;font:11px tahoma;">Start Time:' + markers[i].StartOrder_Time + '</font></td><td style="padding:5px;"><font style="color:black;font:11px tahoma;">End Time:' + markers[i].EndOrder_Time + '</font></td></tr>'
                                    + '<tr><td colspan=2 style="padding:5px;font:12px verdana;"> Submitted Address : <br> <font style="color:darkgreen;"><h6 class="vsplace">' + place + '</h6></font></td></tr>'
                                    + '<tr><td colspan=2 style="padding:5px;font:12px verdana;"> Total Distance between Submitted Address and Tagged Address : <br> <font style="color:darkgreen;"><h6 class="distance">' + totaldis + '</h6></font></td></tr>'
                                    // + '<tr><td colspan=2 style="padding:5px;font:12px verdana;"> Tagged Address  : <br> <font style="color:darkgreen;"> <h6 class="place">' + vsplace + '</h6></font></td></tr>'
                                    //  + '<tr><td colspan=2 style="padding:5px;font:12px verdana;"> Current Place : <br> <font style="color:darkgreen;">' + markers[i].ListedDr_Address1 + '</font></td></tr>' 
                                    + '</table></div>';
                                addInfoWindow(marker, strhtml);
                                prvMarkers.push(marker);
			//google.maps.event.addListener(marker, 'click', (function (marker, i) {
                                //    return function () {

                                //        var place = ''; var vsplace = ''; var latitude1 = ''; var longitude1 = ''; var latitude2 = ''; var longitude2 = ''; var unit = ''; var meters = ''; var Km = ''
                                //        latitude1 = markers[i].geo_lat; longitude1 = markers[i].geo_log; latitude2 = markers[i].lat; longitude2 = markers[i].lng; unit = 'K'

                                //        var radlat1 = Math.PI * latitude1 / 180;
                                //        var radlat2 = Math.PI * latitude2 / 180;
                                //        var theta = longitude1 - longitude2;
                                //        var radtheta = Math.PI * theta / 180;
                                //        var dist = Math.sin(radlat1) * Math.sin(radlat2) + Math.cos(radlat1) * Math.cos(radlat2) * Math.cos(radtheta);
                                //        if (dist > 1) {
                                //            dist = 1;
                                //        }
                                //        dist = Math.acos(dist);
                                //        dist = dist * 180 / Math.PI;
                                //        dist = dist * 60 * 1.1515;
                                //        if (unit == "K") {
                                //            dist = dist * 1.609344
                                //            //var totalkm = Math.round(dist);
                                //            var totalkm = dist.toFixed(3)
                                //            var dis = dist * 1000;
                                //            var total = Math.round(dis);
                                //        }


                                //        if (markers[i].geo_lat == '' && markers[i].geo_log == '') {
                                //            var totaldis = "Location Not Fetched/Captured";
                                //        }
                                //        else {
                                //            var totaldis = totalkm + ' Km ';
                                //        }

                                //        var geoAddress = getAddress(markers[i].geo_lat, markers[i].geo_log);

                                //        var geoadd = '';
                                //        geoadd = markers[i].GeoAddrs;
                                //        if (geoadd == '' || geoadd == 'NA') {
                                //            var geoAddress = getAddress(markers[i].geo_lat, markers[i].geo_log);
                                //            var url = "//nominatim.openstreetmap.org/reverse?format=json&lon=" + parseFloat(markers[i].geo_log) + '&lat=' + parseFloat(markers[i].geo_lat) + "";
                                //            $.ajax({
                                //                url: url,
                                //                async: false,
                                //                dataType: 'json',
                                //                success: function (data) {
                                //                    $x = $(".place");
                                //                    place = data.display_name;
                                //                }
                                //            });
                                //        }
                                //        else {
                                //            place = geoadd;
                                //        }



                                //        vsplace = markers[i].addrs;
                                //        var customer = '';
                                //        if (markers[i].typ == 2) {
                                //            customer = '(Distributor)';
                                //        } else {
                                //            customer = '';
                                //        }


                                //        infoWindow.setContent('<div style="display:block;width:500px;"><table style="width:100%" border="1"><tr><td style="width:50%;padding:0px 5px 10px 5px ;border-bottom:solid 2px #dcdcdc;text-transform: capitalize;"><font style="color:darkblue;font:15px tahoma;">' + markers[i].Trans_Detail_Name + '' + customer + ' <br><font style="color:darkgreen;"> ' + markers[i].ListedDr_Address1 + '</font> </td>'
                                //            + '<td style="width:50%;padding:0px 5px 10px 5px ;border-bottom:solid 2px #dcdcdc;"><font style="color:#00303f; font:bold 12px verdana;float: right;">' + markers[i].OrderType + '</font><br><a style="color:#00303f; font:bold 10px verdana;float: right;">' + markers[i].DtTime + '</a></td></tr>'
                                //            + '<tr><td style="padding:5px;"><font style="color:black;font:11px tahoma;">POB ( Value ) : ' + ((markers[i].POB_Value != '0' && markers[i].POB_Value != null) ? '' + markers[i].POB_Value : '-') + '</td>'
                                //            + '<td style="padding:5px;"><font style="color:black;font:11px tahoma;">POB ( Liters ) : ' + ((markers[i].net_weight_value != '0') ? '' + markers[i].net_weight_value : '-') + '</td></tr>'
                                //            + '<tr><td style="padding:5px;"><font style="color:black;font:11px tahoma;">Start Time:' + markers[i].StartOrder_Time + '</font></td><td style="padding:5px;"><font style="color:black;font:11px tahoma;">End Time:' + markers[i].EndOrder_Time + '</font></td></tr>'
                                //            + '<tr><td colspan=2 style="padding:5px;font:12px verdana;"> Submitted Address : <br> <font style="color:darkgreen;"><h6 class="vsplace">' + place + '</h6></font></td></tr>'
                                //            + '<tr><td colspan=2 style="padding:5px;font:12px verdana;"> Total Distance between Submitted Address and Tagged Address : <br> <font style="color:darkgreen;"><h6 class="distance">' + totaldis + '</h6></font></td></tr>'
                                //            // + '<tr><td colspan=2 style="padding:5px;font:12px verdana;"> Tagged Address  : <br> <font style="color:darkgreen;"> <h6 class="place">' + vsplace + '</h6></font></td></tr>'
                                //            //  + '<tr><td colspan=2 style="padding:5px;font:12px verdana;"> Current Place : <br> <font style="color:darkgreen;">' + markers[i].ListedDr_Address1 + '</font></td></tr>' 
                                //            + '</table></div>'
                                //        );
                                //        infoWindow.open(map, marker);
                                //        addInfoWindow(marker, infoWindow.content);
                                //        prvMarkers.push(marker);
                                //    }
                                //})(marker, i))

                            
                        }


                    }else {
                            var lat = 28.7041; var long = 77.1025;
                            marker = new google.maps.Marker({
                                position: new google.maps.LatLng(lat, long),
                                icon: icon,
                                map: map
                            });
                            marker.setMap(null);
                        }

                    path.push(position);
                    poly.setPath(path);
                    if ((i + 1) < markers.length) {
                        if (markers[i + 1].lat != '') {
                            // v = parseFloat(markers[i+1].lat);
                            // console.log(markers[i+1].lat +":"+ markers[i+1].lng);            
                            // if(isNaN(v)==false){ 
                            // if(isNaN(v)>0){
                            var des = new google.maps.LatLng(markers[i + 1].lat, markers[i + 1].lng);

                            dis = getDistance(src, des, "K");
                            if (dis > 0.130)
                                var src = des; else dis = 0;
                            KMTot += dis;
                        }
                        //  }}
                    }
                    if (NavMarkers.length > 0) {
                        NavMarkers[0].setTitle(markers[i].DtTime);
                        NavMarkers[0].setPosition(position);
                    }
                    map.setCenter(position);
                }
                else {
                    if (markers[i].ordfld == 0) {

                        dte = new Date(markers[i].DtTime);
                        dy = dte.getDate();
                        mn = dte.getMonth() + 1;
                        hh = dte.getHours();
                        mm = dte.getMinutes();
                        ss = dte.getSeconds()
                        ddmm = ((dy < 10) ? '0' : '') + dy + ' / ' + ((mn < 10) ? '0' : '') + mn + ' / ' + dte.getFullYear();
                        sHH = ((hh > 12) ? hh - 12 : hh)
                        sHH = ((sHH < 10) ? '0' : '') + sHH
                        $("#dspDt").html(ddmm);

                        // var loca =   markers[i].GeoAddrs == NA ? "Location Not Find" :markers[i].GeoAddrs; 

                        CusCnt++;
                        tpval += ((markers[i].POB_Value != '0' && markers[i].POB_Value != null) ? parseFloat(markers[i].POB_Value) : 0);
                        tltrs += ((markers[i].net_weight_value != '0') ? parseFloat(markers[i].net_weight_value) : 0);
                        sStr += '<li onclick="openRetWin(-1)"><div style="display:block;"><table style="width:100%"><tr style="background:#dcdcdc;"><td style="padding:5px ;border-left:solid 2px #dcdcdc;"><font style="color:darkblue;font:15px tahoma;">' + markers[i].Trans_Detail_Name + '</td>'
                                + '<td style="padding:5px;border-right:solid 2px #dcdcdc;"><a style="color:#00303f; font:bold 10px verdana;float: right;">' + sHH + ":" + ((mm < 10) ? '0' : '') + mm + ":" + ((ss < 10) ? '0' : '') + ss + " " + ((hh > 11) ? 'PM' : 'AM') + '</a></td></tr>'
                                + '<tr><td style="padding:5px;border-left:solid 2px #dcdcdc;"><font style="color:black;font:11px tahoma;">Start Time : ' + markers[i].StartOrder_Time + '</font></td><td style="padding:5px;border-right:solid 2px #dcdcdc;"><font style="color:black;font:11px tahoma;">End Time :' + markers[i].EndOrder_Time + '</font></td></tr>'
                                + '<tr><td style="padding:5px;border-left:solid 2px #dcdcdc;"><font style="color:black;font:11px tahoma;">POB ( Value ) : ' + ((markers[i].POB_Value != '0' && markers[i].POB_Value != null) ? '' + markers[i].POB_Value : '-') + '</td>'
                                + '<td style="padding:5px;border-right:solid 2px #dcdcdc;"><font style="color:black;font:11px tahoma;">POB ( Liters ) : ' + ((markers[i].net_weight_value != '0') ? '' + markers[i].net_weight_value : '-') + '</td></tr>'
                                + '<tr><td style="padding:5px;border-left:solid 2px #dcdcdc;"><font style="color:black;font:11px tahoma;font-weight: bold;">' + markers[i].OrderType + '</font></td><td style="padding:5px;border-right:solid 2px #dcdcdc;"><font style="color:black;font:11px tahoma;font-weight: bold;">' + markers[i].Remarks + '</font></td></tr>'
                                + '<tr><td colspan=2 style="padding:5px;font:12px verdana;border:solid 2px #dcdcdc;border-top-width:0px;border-radius: 0px 0px 5px 5px;"><font style="color:darkgreen;">' + (markers[i].GeoAddrs == 'NA' ? '' : markers[i].GeoAddrs) + '</font></td></tr>'
                                + '</table></div></li>';
                        
                    }
                }
            }
            sStr += "</ul>";
            $("#sTM").html(markers[0].DtTime);
            $("#eTM").html(markers[markers.length - 1].DtTime);
            $("#tpval").html(roundNumber(tpval, 2));
            $("#tltrs").html(roundNumber(tltrs, 2));
            $("#vstCnt").html(CusCnt);
            $("#vstDet").html(sStr);
            $("#total").html(roundNumber(KMTot, 2) + " Km");
            /*var lat = markers[markers.length-1].lat;
            var lng = markers[markers.length-1].lng;
            cpos=new google.maps.LatLng(lat, lng);
            if(pPos!=cpos){
                NavMarkers[0].setPosition(cpos);
                path.push(cpos);
                poly.setPath(path);
                pPos!=cpos
            }*/
        }


        function getAddress(lat, log) {

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

        function getDistance(latlon1, latlon2, unit) {
            var radlat1 = Math.PI * latlon1.lat() / 180;
            var radlat2 = Math.PI * latlon2.lat() / 180;
            var theta = latlon1.lng() - latlon2.lng();
            var radtheta = Math.PI * theta / 180
            var dist = Math.sin(radlat1) * Math.sin(radlat2) + Math.cos(radlat1) * Math.cos(radlat2) * Math.cos(radtheta);
            dist = Math.acos(dist)
            dist = dist * 180 / Math.PI
            dist = dist * 60 * 1.1515
            if (unit == "K") { dist = dist * 1.609344 }
            if (unit == "N") { dist = dist * 0.8684 }
            if (isNaN(dist)) dist = 0;
            return dist
        }

        roundNumber = function (number, precision) {
            precision = Math.abs(parseInt(precision)) || 0;
            var multiplier = Math.pow(10, precision);
            return (Math.round(number * multiplier) / multiplier);
        }
 
        $(document).ready(function () {
            // create Calendar from div HTML element
            $("#calendar").kendoCalendar();
            var calendar = $("#calendar").data("kendoCalendar");

            var current = calendar.current();
            $(".k-footer").css("display", "none");
            calendar.bind("change", function () {
                dte = new Date(this.value());
                selDt = dte.getFullYear() + '-' + (dte.getMonth() + 1) + '-' + dte.getDate() + ' 00:00:00.000';
                $("#modal").css('display', 'block');
                getMyTP(selDt);
                Callback(selDt);

            });
        });

        getMyTP = function (TPDt) {
            if (!TPDt) TPDt = selDt;
            $('#spWT').html("Loading...");
            $('#spDis').html("");
            $('#spRT').html("");
            $.get("/server/db.php?axn=get/mytp&SF_Code=" + $("#selSF").val() + "&Dt=" + TPDt + "",
                function (response) {
                    var myTP = JSON.parse(response) || [];
                    $('#spWT').html("");
                    if (myTP.length > 0) {
                        $('#spWT').html("Worktype : <b style='color:#0cad01'>" + myTP[0].Wtype + "</b>");
                        $('#spDis').html("Distributor : <b style='color:blue'>" + myTP[0].Stockist_Name + "</b>");
                        $('#spRT').html("Route : <b style='color:blue'>" + myTP[0].ClstrName + "</b>");
                    }
                })
                .fail(function (response) {
                    $('#spWT').html("");
                    console.log("Connection Failed");
                });

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
                    if (lastAnimMark != undefined) lastAnimMark.setAnimation(null);
                    marker.setAnimation(google.maps.Animation.BOUNCE);
                    lastAnimMark = marker;
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
                //map.setZoom(21);

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

    </script>
    </form>
</body>
</html>
