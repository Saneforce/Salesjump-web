<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Retailers_Mapping.aspx.cs" Inherits="Retailers_Mapping" %>


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
        #map_canvas, #tbMain, #modal, body {
            width: 100%;
            height: 100%;
            padding: 0px;
            margin: 0px;
        }

        #modal {
            display: none;
            position: absolute;
            background-color: rgba(0,0,0,0.5);
            z-index: 10000;
            line-height: 50;
            height: 100%;
            text-align: center;
            color: #fff;
        }

        .notmap {
            height: 50px;
            width: 400px;
            border: 1px solid red;
            background-color: green;
        }

        .current {
            background-color: blue;
        }

        #wrapper {
            width: 1005px;
            margin: 0 auto;
        }

        #leftcolumn,
        #rightcolumn {
            border: 1px solid white;
            float: left;
            min-height: 450px;
            width: 500px;
            color: white;
        }

        #leftcolumn {
            width: 500px;
            ;
            background-color: #111;
        }

        #rightcolumn {
            width: 400px;
            background-color: #777;
        }

        #vstDet > ul {
            list-style-type: none;
            -webkit-margin-start: 0em;
            -webkit-margin-before: 0em;
            -webkit-padding-start: 0px;
        }

            #vstDet > ul > li {
                margin-bottom: 5px;
            }

        table {
            border-collapse: collapse;
        }

        .k-icon {
            margin: 13px 5px;
        }

        #vstCnt {
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

        .ColHeader {
            display: block;
            background: #f1f1f1;
            margin: -1px -2px;
            padding: 5px;
        }

        form {
            margin: 0px;
        }

        .pad {
            padding: 0px;
        }

        .mnu-bt, .home-bt {
            margin: 3px;
        }

        .my-custom-class-for-label1, .Ordered {
        }

        .my-custom-class-for-label {
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

        .my-custom-class-for-label2 {
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

        .Ordered > .my-custom-class-for-label {
            color: #00b0ff;
            background: #d3f1ff;
            border: 1px solid #00b0ff;
        }

        .Ordered > .my-custom-class-for-label2 {
            color: #d2ded7;
            background: #51bd7d;
            border: 1px solid #00b0ff;
        }

        .my-custom-class-for-label1:after, .Ordered:after {
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

        .Ordered:after {
            border-top-color: #00b0ff;
        }

        ul > li {
            padding: 5px 8px;
            border: 1px #dcdcdc dashed;
            margin-top: -1px;
        }

            ul > li:hover {
                background: #fee1d7;
            }
            #selerut_chosen {
            width: 240px !important;
            position: absolute;
        }
    </style>

</head>
<body>
    <form id="form1" runat="server">
        <%--<div id="modal">
        Loading....</div>--%>
        <table id="tbMain">
            <tr>
                <td colspan="3" valign="top" style="height: 1%; background-color: #f1f1f1; border-bottom: solid 1px #cacaca">
                    <div class="pad HDBg">
                        <a href="#" class="button mnu-bt" onclick="toggleMenu()"><i class="fa fa-bars"></i>
                        </a><a runat="server" onclick="GotoHome();return false;" id="aHome" class="button home-bt">Home</a><a runat="server" onclick="rmAllSFTags();return false;" style="background-color: #dd2424;" id="rmTags" class="button home-bt">Remove All Tags</a><div class='hCap' style="width: 300px; overflow: hidden;">
                            Retailers Map Tagging
                        </div>
                    </div>

                </td>
            </tr>
            <tr>
                <td style="min-width: 250px;" valign="top">
                    <div class="ColHeader" style="display: block">
                        Employee Name :
                   
                        <input type="text" id="txtFilter" name="txtFilter" onkeyup="return getListView()"
                            autocomplete="off" />
                    </div>
                    <div id="sideMnu">
                        <div id="smartTree" style="display: block; height: 747px; min-width: 250px; width: 250px; overflow: auto;">
                            <table>
                                <tr>
                                    Employee Name
                       
                                    <td valign="top">
                                        <asp:DropDownList ID="selSF" onchange="Callback()" runat="server" SkinID="ddlRequired"
                                            Width="210px" CssClass="ddl">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <ul id="filmenu" style="display: block; height: 745px; min-width: 250px; width: 250px; overflow: auto;">
                        </ul>
                    </div>
                </td>

                <td style="min-width: 250px; border-color: black; border-style: solid; border-width: 1px;" valign="top">
                    <div id="FFsideMnu" style="background-color: #f1f1f1;">
                        <div style="box-shadow: 1px 1px 1px #999; padding: 5px 5px 4px 5px; max-height: 1% !important;">
                            Retailers Name 					
                        <div>
                            <input type="text" id="txtRFilter" name="txtRFilter" onkeyup="getFilterListView()"
                                autocomplete="off" /><br />
                            Route Name<br /><select id="selerut" style="width: 240px"></select></div>                           
                        </div>

                        <div class="notemap" style="display: block; background-color: #ffffff; max-height: 87vh !important; overflow: auto;margin-top:25px">
                            <ul id="Retmnu" style="display: block; min-width: 250px; width: 250px; max-height: 100% !important; overflow: auto;">
                            </ul>
                        </div>
                    </div>
                </td>
                <td width="100%" valign="top">
                    <div id="map_canvas" class="mapping">
                    </div>
                </td>
                <td valign="top" style="width: 100% !important;" id="idDets">

                    <div style="background: #f1f1f1;">
                    </div>

                    <div style="display: block; background: #dcdcdc;">
                    </div>
                    <div id="idloading" style="display: none; position: absolute; top: 0px; left: 0px; width: 100% !important; height: 100% !important; margin: auto; text-align: center; line-height: 100vh; background-color: #f5f5f569;">
                        <b style="padding: 10px 10px; background: white; box-shadow: 0px 0px 1px black;">Loading...</b>
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

      <%-- <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.0.2/dist/css/bootstrap.min.css" rel="stylesheet">
<script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
<link href="https://cdn.jsdelivr.net/npm/select2@4.1.0-rc.0/dist/css/select2.min.css" rel="stylesheet" />
<script src="https://cdn.jsdelivr.net/npm/select2@4.1.0-rc.0/dist/js/select2.min.js"></script>--%>
        <script src="js/chosen.jquery.js"></script>
        <link href="css/chosen.css" rel="stylesheet" />
            
       

        <script type="text/javascript">
            var markers = [];
            var prvMarkers = [];
            var NavMarkers = [];
            var lastOpenedInfoWindow;
            var lastAnimMark;
            var infoWins = [];
            var mgrli = [];
            var selesf = '';
            $("#rmTags").hide();

            var CurrSF;
            function GotoHome() {
                if ("<%=Session["sf_type"] %>" == "2") {
                    window.location.href = "DashBoard1.aspx"
                }
                if ("<%=Session["sf_type"] %>" == "3" && "<%=Session["sf_code"]%>" == "admin") {
                    window.location.href = "DashBoard.aspx"
                }
            }
            function rmAllSFTags() {
                var routname = $('#selerut option:selected').text();
                var roueco = $('#selerut').val();                

                if (confirm("do you want remove all Taggings for route of  "+routname+"?")) {
                    if (confirm("Are sure remove all Taggings for route of "+routname+"?")) {
                        $.ajax({
                            url: "Retailers_Mapping.aspx/rmallrSFTags",
                            type: "POST",
                            contentType: "application/json;charset=utf-8",
                            dataType: "json",
                            data: "{'SFCode':'" + CurrSF + "','roueco':'"+roueco+"'}",
                            success: function (data) {
                                //Getlocation(CurrSF);
                                routelocation(roueco,CurrSF);
                            },
                            error: function (error) {
                                console.log(`Error ${error}`);
                            }
                        });
                    }
                }
            }
            function rmRetTag(RetCd, RetNm) {
                if (confirm("Do you want Un-Tag \"" + RetNm + "\"?")) {
                    if (confirm("Are sure Un-Tag \"" + RetNm + "\"?")) {
                        $.ajax({
                            url: "MasterFiles/rptCustomerGeotagging.aspx/untagRetailers",
                            type: "POST",
                            contentType: "application/json;charset=utf-8",
                            data: "{'custCode':'" + RetCd + "'}",
                            dataType: "json",
                            success: function (data) {
                                if (data.d == 'Sucess') {
                                    alert('Sucessfully Untag Retailer..!');
                                    //Getlocation(CurrSF);
                                    var routse = $('#selerut').val();
                                    routelocation(routse,CurrSF);
                                }
                                else {
                                    alert(data.d);
                                }
                            },
                            error: function (error) {
                                console.log(`Error ${error}`);
                            }
                        });
                    }
                }
            }
            var datas = [];

            function getFilterListView() {
                stx = $("#txtRFilter").val();
                $('#Retmnu>li').hide();
                if (stx == '')
                    $('#Retmnu>li').show();
                else {
                    $('#Retmnu>li').each(function () {
                        if ($(this).text().toLocaleLowerCase().indexOf(stx.toLocaleLowerCase()) > -1) $(this).show();
                    })
                }
            }
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

                            li.append("<li class='ztree-node ellipsis' data-toggle='tooltip' title='" + old[k].name.toString() + "'> <a class='listLink' href='#' onclick=\"listsearch(\'" + old[k].id.toString() + "\')\" sfCode='" + old[k].id.toString() + "'>" + old[k].name + "</a></li>");
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
            function listsearch(n) {
                var sf = n;
                selesf=sf
                Getallroute(sf);
                Getlocation(sf)

            }

            $(document).ready(function () {
                //getListView();               
                $(document).on('click', '.listLink', function () {
                    $("#selSF").val($(this).attr('sfCode'));
                    // getMyTP();
                    Callback();
                    
                });
            });

            function Getallroute(c) {
                var sfcode = c;
                 
                $.ajax({
                    url: "Retailers_Mapping.aspx/allocateroute",
                    type: "POST",
                    contentType: "application/json;charset=utf-8",
                    dataType: "json",
                    data: "{'SFcode':'" + sfcode + "','div_code':'<%=Session["div_code"]%>'}",
                    success: function (data) {
                        mgrli = JSON.parse(data.d);
                        if (mgrli.length > 0) {
                            $("#selerut").html('');
                            var Territory_Name = $("#selerut");
                            Territory_Name.empty().append('<option selected="selected" value="0">All Routes</option>');
                            for (var i = 0; i < mgrli.length; i++) {
                                Territory_Name.append($('<option value="' + mgrli[i].Territory_Code + '">' + mgrli[i].Territory_Name + '</option>')).trigger('chosen:updated').css("width", "100%");;;
                            }

                        }
                    },
                });
                
                $("#selerut").chosen();
                 
            }
            $("#selerut").change(function () {
                var seler = $('option:selected', this).val();
                //if (seler == "0") {
                //    selesf = selesf;
                //    Getlocation(selesf);
                //}
                //else {
                //    selesf = selesf;
                //    routelocation(seler, selesf);
                //}
                selesf = selesf;
                    routelocation(seler, selesf)

            });
            function routelocation(n, sf) {
                $("#idloading").show();
                var route = n;
                var selecsf = sf;
                $.ajax({
                    url: "Retailers_Mapping.aspx/RoutTaggedRetailers",
                    type: "POST",
                    contentType: "application/json;charset=utf-8",
                    dataType: "json",
                    data: "{'route':'" + route + "','div_code':'<%=Session["div_code"]%>','SFcode':'" + selecsf + "'}",
                    success: function (data) {
                        var marker, i, str;
                        markers = JSON.parse(data.d) || []
                        $('#Retmnu').html('');
                        clearLocations();
                        str = "";
                        $("#rmTags").hide();
                        if (markers.length > 0) $("#rmTags").show();
                        var bounds = new google.maps.LatLngBounds();
                        infoWins = [];
			if (markers.length > 0) {
                        for (var $i = 0; $i < markers.length; $i++) {
                            str += "<li style='list-style:none; padding:5px' class='ztree-node ellipsis'><a href='#' id='retailercode' onclick='openRetWin(" + $i + ")'>" + markers[$i].Retailer_Name + "</a></li>";
                            marker = new google.maps.Marker({
                                position: new google.maps.LatLng(markers[$i].lat, markers[$i].long),
                                map: map
                            });
							var stri = markers[$i].Retailer_Name;
                            stri = stri.replace(/'/g, '');
                            strhtml = "<table><tr><td style='font-size:13px;padding:5px'><b>" + markers[$i].Retailer_Name + "</b></td></tr><tr><td style='font-size:13px;padding:5px'>" + markers[$i].Retailer_Address + "</td></tr><tr><td style='text-align:right' colspan='2'><i style='background-color: #dd2424;' id='rmTag' class='button home-bt' onclick='rmRetTag(\"" + markers[$i].Retailer_Code + "\",\"" + stri + "\")'>Untag</i></td></tr></table>";

                            addInfoWindow(marker, strhtml);
                            prvMarkers.push(marker);

                            var myLatLng = new google.maps.LatLng(markers[$i].lat, markers[$i].long);
                            bounds.extend(myLatLng);
                        }
			}
			else {
                            str += "<li style='list-style:none; padding:5px' class='ztree-node ellipsis'>No Retailers Found</li>";
                        }
                        map.fitBounds(bounds);
                        //$('#selSF').append(str);
                        $('#Retmnu').append(str);

                        $("#idloading").hide();
                    },
                    error: function (error) {
                        console.log(`Error ${error}`);
                        $("#idloading").hide();
                    }
                });
            }

            var map;
            function Getlocation(n) {
                $("#idloading").show();
                var sfcode = n;
                CurrSF = n;
                $.ajax({
                    url: "Retailers_Mapping.aspx/GetSFTaggedRetailers",
                    type: "POST",
                    contentType: "application/json;charset=utf-8",
                    dataType: "json",
                    data: "{'SFCode':'" + sfcode + "'}",
                    success: function (data) {
                        var marker, i, str;

                        markers = JSON.parse(data.d) || []
                        $('#Retmnu').html('');
                        clearLocations();
                        str = "";
                        $("#rmTags").hide();
                        if (markers.length > 0) $("#rmTags").show();
                        var bounds = new google.maps.LatLngBounds();
                        infoWins = [];
			if (markers.length > 0) {
                        for (var $i = 0; $i < markers.length; $i++) {
                            str += "<li style='list-style:none; padding:5px' class='ztree-node ellipsis'><a href='#' id='retailercode' onclick='openRetWin(" + $i + ")'>" + markers[$i].Retailer_Name + "</a></li>";
                            marker = new google.maps.Marker({
                                position: new google.maps.LatLng(markers[$i].lat, markers[$i].long),
                                map: map
                            });
							var stri = markers[$i].Retailer_Name;
                            stri = stri.replace(/'/g, '');
                            strhtml = "<table><tr><td style='font-size:13px;padding:5px'><b>" + markers[$i].Retailer_Name + "</b></td></tr><tr><td style='font-size:13px;padding:5px'>" + markers[$i].Retailer_Address + "</td></tr><tr><td style='text-align:right' colspan='2'><i style='background-color: #dd2424;' id='rmTag' class='button home-bt' onclick='rmRetTag(\"" + markers[$i].Retailer_Code + "\",\"" + stri + "\")'>Untag</i></td></tr></table>";

                            addInfoWindow(marker, strhtml);
                            prvMarkers.push(marker);

                            var myLatLng = new google.maps.LatLng(markers[$i].lat, markers[$i].long);
                            bounds.extend(myLatLng);
                        }
			}	
			else {
                            str += "<li style='list-style:none; padding:5px' class='ztree-node ellipsis'>No Retailers Found</li>";
                        }
                        map.fitBounds(bounds);
                        // $('#selSF').append(str);
                        $('#Retmnu').append(str);

                        $("#idloading").hide();
                    },
                    error: function (error) {
                        console.log(`Error ${error}`);
                        $("#idloading").hide();
                    }
                });
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
                            selesf = n.id;
                            Getallroute(n.id);
                            Getlocation(n.id);
                            $("#modal").css('display', 'block');
                            //getMyTP();
                            //Callback();
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




            function addInfoWindow(marker, message) {

                var infoWindow = new google.maps.InfoWindow({
                    content: message
                });
                infoWins.push(infoWindow);
                google.maps.event.addListener(marker, 'click', function () {
                    infoWindow.open(map, marker);
                    closeLastOpenedInfoWindow();
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

            //$(".notemap").click(function () {
            //    highlightMarker($(this).markers());
            //}, function () {
            //    stopHighlightMarker($(this).markers());
            //}); 

            //function stopHighlightMarker(i) {
            //    if (markers[i].getAnimation() !== null) {
            //        markers[i].setAnimation(null);
            //    }
            //}

            function openRetWin(i) {
                if (lastOpenedInfoWindow != null) lastOpenedInfoWindow.close();
                infoWins[i].open(map, prvMarkers[i]);
                lastOpenedInfoWindow = infoWins[i];
                if (lastAnimMark != undefined) lastAnimMark.setAnimation(null);
                prvMarkers[i].setAnimation(google.maps.Animation.BOUNCE);
                lastAnimMark = prvMarkers[i];
                stopani(prvMarkers[i]);
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



            google.maps.event.addDomListener(window, 'load', initMap);
            function initMap() {
                let clat = 13.0575;
                let clng = 80.2822;
                if ('<%=Session["div_code"]%>' == '101') {
                    clat = 5.9506081;
                    clng = -0.6606011;
                }
                else if ('<%=Session["div_code"]%>' == '140') {
                    clat = 11.9980465;
                    clng = 8.470859;
                }
                var mapOptions = {
                    mapTypeId: 'roadmap', zoom: 19, center: { lat: clat, lng: clng }
                };
                map = new google.maps.Map(document.getElementById("map_canvas"), mapOptions);
                $("#modal").css('display', 'block');

            }
            //var timer;
            //function Callback(DtTm, movMark) {
            //    if (!movMark) movMark = false;
            //    if (!DtTm) DtTm = selDt;
            //    KMTot = 0;
            //    $.get("/server/db.php?axn=get/track&SF_Code=" + $("#selSF").val() + "&Dt=" + DtTm + "",
            //        function (response) {
            //            if (movMark == true) {
            //                lastIndx = (markers.length > 0) ? markers.length : 0;
            //                var newMarkers = JSON.parse(response) || [];
            //                markers = $.merge(markers, newMarkers);
            //                if (newMarkers.length > 0) moveMarker(lastIndx);

            //            } else {
            //                markers = JSON.parse(response);
            //                clearLocations();
            //                if (markers.length > 0) initMarkers();
            //            }





            //            cdt = new Date();
            //            $("#modal").css('display', 'none');
            //            if (selDt == cdt.getFullYear() + '-' + (cdt.getMonth() + 1) + '-' + cdt.getDate() + ' 00:00:00.000') {

            //                window.clearTimeout(timer);
            //                timer = setTimeout(function () {
            //                    var aSync;
            //                    if (markers.length > 0) {
            //                        lastDt = markers[markers.length - 1].DtTm.date + '.000';
            //                        aSync = true;
            //                    } else {
            //                        lastDt = selDt;
            //                        aSync = false;
            //                    }
            //                    Callback(lastDt, aSync);
            //                }, 1000 * 5);
            //            }
            //        })
            //        .fail(function (response) {
            //            $("#modal").css('display', 'none');
            //            console.log("Connection Failed");
            //        });
            //}


            function clearLocations() {
                if (prvMarkers.length > 0) {
                    for (var i = 0; i < prvMarkers.length; i++) {
                        prvMarkers[i].setMap(null);
                    }
                    prvMarkers = [];
                    NavMarkers = [];
                    //if (poly != null) {
                    //    poly.setPath([]);
                    //    poly.setMap(null);
                    //}
                }
                //$("#sTM").html("&nbsp;");
                //$("#eTM").html("&nbsp;");
                //$("#tpval").html(roundNumber(0, 2));
                //$("#tltrs").html(roundNumber(0, 2));
                //$("#vstCnt").html(0);
                //$("#vstDet").html("");
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


        </script>
    </form>
</body>
</html>
