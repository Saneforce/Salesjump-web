<%@ Page Title="Location Finder" Language="C#" AutoEventWireup="true" CodeFile="locfinder.aspx.cs"
    Inherits="locfinder" %>

<html>
<head>
<link rel="stylesheet" href="https://unpkg.com/leaflet@1.7.1/dist/leaflet.css" />
    <link href="css/font-awesome.css" rel="stylesheet" type="text/css" />
    <link href="css/DCR_Entry.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css">
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
		.leaflet-popup-content-wrapper, .leaflet-popup-tip {
    background: white;
    color: #333;
    box-shadow: 0 3px 14px rgba(0,0,0,0.4);
    width: 150%;
}
.leaflet-container a.leaflet-popup-close-button {
    position: absolute;
    top: 0;
    right: -144px;
    border: none;
    text-align: center;
    width: 18px;
    height: 14px;
    font: 16px/14px Tahoma, Verdana, sans-serif;
    color: #c3c3c3;
    text-decoration: none;
    font-weight: bold;
    background: transparent;
   
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
                <div id="sideMnu">
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
	
<script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
<script src="https://unpkg.com/leaflet@1.7.1/dist/leaflet.js"></script>
    <script type="text/javascript" src="treeView/js/jquery.smarttree.all.js"></script>
    <script type="text/javascript" src="treeView/js/jquery.mousewheel.min.js"></script>
    <script src="MonthViewer/kendo.all.min.js"></script>

    <script type="text/javascript">
        function GotoHome() {
            if ("<%=Session["sf_type"] %>" == "2") {
        window.location.href = "DashBoard1.aspx"
    }
    if ("<%=Session["sf_type"] %>" == "3" && "<%=Session["sf_code"]%>" == "admin") {
                window.location.href = "DashBoard.aspx"
            }
        }
     $(document).ready(function () {
	  $("#calendar").kendoCalendar();
            var calendar = $("#calendar").data("kendoCalendar");

            var current = calendar.current();
            $(".k-footer").css("display", "none");
            calendar.bind("change", function () {
			clearLocations();
                dte = new Date(this.value());
                selDt = dte.getFullYear() + '-' + (dte.getMonth() + 1) + '-' + dte.getDate() + ' 00:00:00.000';
                $("#modal").css('display', 'block');
				map.removeLayer(L.marker);
                getMyTP(selDt);
                Callback(selDt);

            });
            $(document).on('click', '.listLink', function () {
			clearLocations();
                $("#selSF").val($(this).attr('sfCode'));
                getMyTP();
                Callback();
            });
		 });
		     var datas = [];


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
 			 var mapOptions = {
                mapTypeId: 'roadmap', zoom: 8, center: { lat: ((<%=Session["div_code"]%>) == 101) ? 5.9506081 : 13, lng: ((<%=Session["div_code"]%>) == 101) ? -0.6606011 : 80 }
            };
		var map = L.map('map_canvas',mapOptions);

        var layer = new L.TileLayer('http://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png');
	    map.addLayer(layer);
		//var markersLayer = L.featureGroup().addTo(map);
		 var infoWindow;
		 var prvMarkers = [];
		  var NavMarkers = [];
		  var slatlng = "";
		   var poly;
		 selDt = "";
		 var lmar=[];
		 var nemars=[];
        cdt = new Date();
        selDt = cdt.getFullYear() + '-' + (cdt.getMonth() + 1) + '-' + cdt.getDate() + ' 00:00:00.000';
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
		 var timer;
        function Callback(DtTm, movMark) {
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

        function getAddress(lat, log) {

        }
		
		 roundNumber = function (number, precision) {
            precision = Math.abs(parseInt(precision)) || 0;
            var multiplier = Math.pow(10, precision);
            return (Math.round(number * multiplier) / multiplier);
        }
		 function initMarkers() {
            path = [];
            poly = new L.Polyline({
			color: 'red',
			weight: 3,
			opacity: 0.5,
			smoothFactor: 1
		});
            poly.addTo(map);
			   infoWindow = new L.popup()
          sStr = '<ul>';
          CusCnt = 0; tpval = 0; tltrs = 0;
            moveMarker(0);
 }
   var pPos = null;
   
        function moveMarker(lastIndx) {
            var latLng = new L.LatLng(49.47805, -123.84716);
            var homeLatLng = new L.LatLng(49.47805, -123.84716);

			var im = 'img/bluecircle.png';
            var position, marker, i;
var pointList=[];
            var src = new L.LatLng(markers[0].lat, markers[0].lng);
			Vstlbl = 0;
            for (i = lastIndx; i < markers.length; i++) {

                if (markers[i].lat != '') {
                    pos = new L.LatLng(markers[i].lat, markers[i].lng);

                    if (slatlng.indexOf(markers[i].lat + ":" + markers[i].lng + ";") > -1) {
                        var rews = new RegExp(markers[i].lat + ":" + markers[i].lng + ";", "gi");
                        var re = new RegExp(markers[i].lat + ":" + markers[i].lng, "gi");
                        rptcnt = slatlng.replace(re, '').length - slatlng.replace(rews, '').length;
                        lt = pos.lat + (rptcnt / (1000000 - 7));
                        lg = pos.lng + (rptcnt / (100000 - 7));
                        position = new L.LatLng(lt, lg);
                    }
                    else
                        position = pos;

                    if (i == 0 || NavMarkers.length < 1 || markers[i].ordfld == 0) {

                    
                        if (markers[i].ordfld == 0) {
                            Vstlbl++;

                            //var marker = L.Marker(position).bindLabel("<div class='" + ((markers[i].typ == '2') ? 'my-custom-class-for-label2' : 'my-custom-class-for-label') + "'>" + Vstlbl.toString() + "</div>", { noHide: true }).addTo(map).showLabel();
							//marker.addClass(((markers[i].POB_Value != '0') ? "Ordered" : "my-custom-class-for-label1"));
							
							  lmar=pointList.push(position);
							 L.marker(position).addTo(map);
							 var firstpolyline = new L.Polyline(pointList, {
			color: 'red',
			weight: 3,
			opacity: 0.5,
			smoothFactor: 1
		});
		
		firstpolyline.addTo(map);
	                           
                               
		
                        }
                        else {
                            marker = new L.Marker(position,{
                                title: markers[i].DtTime
                            });
                        }
						       
                        slatlng += markers[i].lat + ":" + markers[i].lng + ";"
                        nemars=prvMarkers.push(marker);

                        if (i > 0 && NavMarkers.length < 1 && markers[i].ordfld != 0) {
                            marker.setIcon(im);
                            NavMarkers.push(marker);
                        }
    
                       

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
                            CusCnt++;
                            tpval += ((markers[i].POB_Value != '0') ? parseFloat(markers[i].POB_Value) : 0);
                            tltrs += ((markers[i].net_weight_value != '0') ? parseFloat(markers[i].net_weight_value) : 0);
                            sStr += '<li><div style="display:block;"><table style="width:100%"><tr style="background:#dcdcdc;"><td style="padding:5px ;border-left:solid 2px #dcdcdc;"><font style="color:darkblue;font:15px tahoma;">' + markers[i].Trans_Detail_Name + '</td>'
                                + '<td style="padding:5px;border-right:solid 2px #dcdcdc;"><a style="color:#00303f; font:bold 10px verdana;float: right;">' + sHH + ":" + ((mm < 10) ? '0' : '') + mm + ":" + ((ss < 10) ? '0' : '') + ss + " " + ((hh > 11) ? 'PM' : 'AM') + '</a></td></tr>'
                                + '<tr><td style="padding:5px;border-left:solid 2px #dcdcdc;"><font style="color:black;font:11px tahoma;">POB ( Value ) : ' + ((markers[i].POB_Value != '0') ? '' + markers[i].POB_Value : '-') + '</td>'
                                + '<td style="padding:5px;border-right:solid 2px #dcdcdc;"><font style="color:black;font:11px tahoma;">POB ( Liters ) : ' + ((markers[i].net_weight_value != '0') ? '' + markers[i].net_weight_value : '-') + '</td></tr>'
                                + '<tr><td colspan=2 style="padding:5px;font:12px verdana;border:solid 2px #dcdcdc;border-top-width:0px;border-radius: 0px 0px 5px 5px;"><font style="color:darkgreen;"></font></td></tr>'
                                + '</table></div></li>';
                            marker.setIcon('img/shop.png');
							
							
                      
                         

                                    var place = '';
                                    var vsplace = '';
                                  
                                    var url = "//nominatim.openstreetmap.org/reverse?format=json&lon=" + parseFloat(markers[i].lng) + '&lat=' + parseFloat(markers[i].lat) + "";
                                    $.ajax({
                                        url: url,
                                        async: false,
                                        dataType: 'json',
                                        success: function (data) {
                                            $x = $(".vsplace");
                                            vsplace = data.display_name;
                                        }
                                    });

                                   L.marker(position).bindPopup('<div style="display:block;width:500px;"><table style="width:100%" border="1"><tr><td style="width:50%;padding:0px 5px 10px 5px ;border-bottom:solid 2px #dcdcdc;text-transform: capitalize;"><font style="color:darkblue;font:15px tahoma;">' + markers[i].Trans_Detail_Name + ' <br><font style="color:darkgreen;"> ' + markers[i].ListedDr_Address1 + '</font> </td>'
                                        + '<td style="width:50%;padding:0px 5px 10px 5px ;border-bottom:solid 2px #dcdcdc;"><a style="color:#00303f; font:bold 10px verdana;float: right;">' + markers[i].DtTime + '</a></td></tr>'
                                        + '<tr><td style="padding:5px;"><font style="color:black;font:11px tahoma;">POB ( Value ) : ' + ((markers[i].POB_Value != '0') ? '' + markers[i].POB_Value : '-') + '</td>'
                                        + '<td style="padding:5px;"><font style="color:black;font:11px tahoma;">POB ( Liters ) : ' + ((markers[i].net_weight_value != '0') ? '' + markers[i].net_weight_value : '-') + '</td></tr>'
                                        + '<tr><td colspan=2 style="padding:5px;font:12px verdana;"> Submitted Address : <br> <font style="color:darkgreen;"><h6 class="vsplace">' + place + '</h6></font></td></tr>'
                                        + '<tr><td colspan=2 style="padding:5px;font:12px verdana;"> Tagged Address  : <br> <font style="color:darkgreen;"> <h6 class="place">' + vsplace + '</h6></font></td></tr>'
                                        //  + '<tr><td colspan=2 style="padding:5px;font:12px verdana;"> Current Place : <br> <font style="color:darkgreen;">' + markers[i].ListedDr_Address1 + '</font></td></tr>' 
                                        + '</table></div>').addTo(map)
                                    //infoWindow.open(map, marker);

                       
                              
                          

									
                          
                        }


                    }                    
                   // path.push(position);
                    //poly.setPath(path);
					//poly.addTo(map);
                    if ((i + 1) < markers.length) {
                        if (markers[i + 1].lat != '') {
                            // v = parseFloat(markers[i+1].lat);
                            // console.log(markers[i+1].lat +":"+ markers[i+1].lng);            
                            // if(isNaN(v)==false){ 
                            // if(isNaN(v)>0){
                            var des = new L.LatLng(markers[i + 1].lat, markers[i + 1].lng);

                            dis = getDistance(src, des, "K");
                            if (dis > 0.130)
                                var src = des; else dis = 0;
                            KMTot += dis;
                        }
                        //  }}
                    }
                    if (NavMarkers.length > 0) {
					var marker1=NavMarkers[0];
                        marker1=new L.Marker(position,{
                                title: markers[i].DtTime
                            });
                    }
                    map.panTo(position);
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
                        tpval += ((markers[i].POB_Value != '0') ? parseFloat(markers[i].POB_Value) : 0);
                        tltrs += ((markers[i].net_weight_value != '0') ? parseFloat(markers[i].net_weight_value) : 0);
                        sStr += '<li><div style="display:block;"><table style="width:100%"><tr style="background:#dcdcdc;"><td style="padding:5px ;border-left:solid 2px #dcdcdc;"><font style="color:darkblue;font:15px tahoma;">' + markers[i].Trans_Detail_Name + '</td>'
                            + '<td style="padding:5px;border-right:solid 2px #dcdcdc;"><a style="color:#00303f; font:bold 10px verdana;float: right;">' + sHH + ":" + ((mm < 10) ? '0' : '') + mm + ":" + ((ss < 10) ? '0' : '') + ss + " " + ((hh > 11) ? 'PM' : 'AM') + '</a></td></tr>'
                            + '<tr><td style="padding:5px;border-left:solid 2px #dcdcdc;"><font style="color:black;font:11px tahoma;">POB ( Value ) : ' + ((markers[i].POB_Value != '0') ? '' + markers[i].POB_Value : '-') + '</td>'
                            + '<td style="padding:5px;border-right:solid 2px #dcdcdc;"><font style="color:black;font:11px tahoma;">POB ( Liters ) : ' + ((markers[i].net_weight_value != '0') ? '' + markers[i].net_weight_value : '-') + '</td></tr>'
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
       
        }
		
		 function getDistance(latlon1, latlon2, unit) {
            var radlat1 = Math.PI * latlon1 / 180;
            var radlat2 = Math.PI * latlon2 / 180;
            var theta = latlon1 - latlon2;
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
		 function clearLocations() {		 
			map.remove();
			map = L.map('map_canvas',mapOptions);
			layer = new L.TileLayer('http://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png');
			map.addLayer(layer);
             $("#sTM").html("&nbsp;");
            $("#eTM").html("&nbsp;");
            $("#tpval").html(roundNumber(0, 2));
            $("#tltrs").html(roundNumber(0, 2));
            $("#vstCnt").html(0);
            $("#vstDet").html("");
			
        }
    </script>
    </form>
</body>
</html>
