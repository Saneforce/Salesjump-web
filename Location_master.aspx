<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Location_master.aspx.cs" Inherits="Location_master" %>


<html>
<head>
    <link href="css/font-awesome.css" rel="stylesheet" type="text/css" />
    <link href="css/locationmaster.css" rel="stylesheet" />
    <%-- <link rel="stylesheet" type="text/css" href="treeView/css/SmartTreeStyle/jquery.smarttree.css"> 
    <link rel="stylesheet" href="MonthViewer/kendo.common-material.min.css" />
    <link rel="stylesheet" href="MonthViewer/kendo.material.min.css" />
    <link rel="stylesheet" href="MonthViewer/kendo.dataviz.min.css" />
    <link rel="stylesheet" href="MonthViewer/kendo.dataviz.material.min.css" />--%>
    <style type="text/css">
       
    </style>

</head>
<body>
    <form id="form1" runat="server">
        <%--<div id="modal">
        Loading....</div>--%>
        <%-- <div style="width: 100%;height: 1080px;flex-shrink: 0;">--%>
        <div class="sidenav">
            <div class="sidenavc">
                <a href="#" style="font-size: 19px;" onclick="GotoHome();return false;">
                    <img class='homeicon' src='../css/locationsvg/home.svg'><label class="hombtn">Home</label></a>
            </div>
            <div class="sidenavc">
                <a href="#" id="teamattn" class="users">
                    <img class='homeicon' src='../css/locationsvg/users.svg'><label class="hombtn">Team</label></a>
            </div>
            <div class="sidenavc">
                <a href="#" id="sfvisit" class="arrow">
                    <img class='homeicon' src='../css/locationsvg/binoculars.svg'><label class="hombtn">   Visit Details</label></a>
            </div>
            <div class="sidenavc">
                <a href="#Rettag" id="Tagretlr" class="tag">
                    <img class='homeicon' src='../css/locationsvg/map-marker.svg'><label class="hombtn">Tagging</label></a>
            </div>
        </div>
        <div id="Rettag">
            <table id="tbMain" style="margin-left: 60px">
                <tr>
                    <td colspan="3" valign="top" style="height: 1%; background-color: #f1f1f1; border-bottom: solid 1px #cacaca">
                        <div style="height: 45px; padding: 5px">

                            <label style="font-family: 'Inter'; padding-left: 16px; padding-right: 16px;">Employee Name :</label>
                            <select id="mgrattn" style="width: 250px; background-color: #ffffff; height: 30px;" class="select2-hidden-accessible locattn"></select>
                            <input type="date" id="date" class="button home-bt locattn" onchange="Getdetai()" />
                            <select id="mgrvis" style="width: 250px; background-color: #ffffff; height: 30px; display: none;" class="select2-hidden-accessible visitloc"></select>
                            <input type="date" id="visdate" style="display: none;" class="button home-bt visitloc" onchange="Getdatewise()" />
                            <select id="mgr" style="width: 250px; background-color: #ffffff; height: 30px; display: none;" class="select2-hidden-accessible tagg"></select><label class="router" style="display: none; font-family: 'Inter'; padding-left: 16px; padding-right: 16px;">Route</label>
                            <select id="rout" style="width: 250px; display: none; background-color: #ffffff; height: 30px;" class="select2-hidden-accessible router"></select>
                            <a runat="server" onclick="rmAllSFTags();return false;" style="background-color: #ffa9a9; display: none; padding: 5px;" id="A1" class="button home-bt tagg">Remove All Tags</a><div class='hCap' style="width: 300px; overflow: hidden;"></div>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td style="min-width: 380px; border-color: black; border-style: solid; border-width: 1px; display: none" valign="top" class="retnme">
                        <div id="FFsideMnu" style="background-color: #f1f1f1;">
                            <div id="attca" style="display: none; min-width: 450px; width: 250px; min-height: 743px; max-height: 743px; overflow: scroll;"></div>
                            <div id="tagtyp" style="display: none; min-width: 450px; width: 250px; min-height: 743px; max-height: 743px; overflow: scroll;">
                                <ul class="nav nav-tabs" id="typnav">
                                    <li interest="1"><a data-toggle="tab" href="#Retmnu" id="Distab">Distributors</a></li>
                                    <li interest="2" class="active"><a data-toggle="tab" href="#Retmnu" id="Rettab">Retailers</a></li>
                                </ul>
                                <div id="Retmnu" style="display: none; min-width: 450px; width: 250px; min-height: 743px; max-height: 743px; overflow: scroll;"></div>
                            </div>
                            <div id="idDetails" style="display: none">
                                <div style="display: none">
                                    <div class="panel-heading" style="display: block; white-space: nowrap; padding: 10px 15px; background: #39435c; color: #fff;">
                                        Kilometers Traveled
                                    </div>
                                    <span id="total" style="display: block; white-space: nowrap; padding: 10px 15px; font-weight: bold; border: solid 1px #000; border-radius: 0px 0px 5px 5px; border-top-width: 0px;"></span><small><small>
                                        <br>
                                    </small></small>
                                </div>
                                <div style="background: #f1f1f1;">
                                    <table style="width: 100%">
                                        <tr>
                                            <td>Start</td>
                                            <td>:</td>
                                            <td id="sTM" style="width: 100%; color: #00b0ff; background-color: #dbdbdb; padding: 5px;"></td>
                                        </tr>
                                        <tr>
                                            <td>End</td>
                                            <td>:</td>
                                            <td id="eTM" style="width: 100%; color: #00b0ff; background-color: #dbdbdb; padding: 5px;"></td>
                                        </tr>
                                    </table>

                                </div>
                                <div id="mvstDet" style="display: block; height: 90.5%; white-space: nowrap; font-weight: bold; border: solid 1px #000; border-radius: 0px 0px 5px 5px; border-top-width: 0px;">
                                    <div class="panel-heading" style="display: block; white-space: nowrap; padding: 10px 15px; background: #39435c; color: #fff;">
                                        Calls Detail<span id="vstCnt"></span><span id="dspDt" style="float: right;"></span>
                                    </div>
                                    <div id="vstDet" style="display: block; padding: 5px; min-width: 253px; height: 73vh; overflow: auto;">
                                    </div>
                                    <div style="display: block; background: #dcdcdc;">
                                        <table style="width: 100%; color: black; font: 10px tahoma; font-weight: bold">
                                            <tr>
                                                <td style="padding: 5px;">Tot.POB (value) : <span id="tpval" style="color: Green"></span>
                                                </td>
                                                <td style="padding: 5px;">Tot.POB (Ltrs) : <span id="tltrs" style="color: Green"></span>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                                <label style="padding: 5px;">Tot Traveled Km : </label>
                                <span id="tkm" style="color: Green"></span>
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
                            <b style="padding: 10px 10px; background: white; box-shadow: 0px 0px 1px black;"><%--<span style="width: 150px; height: 50px; display: none;">
                                <img src="https://flevix.com/wp-content/uploads/2019/07/Curve-Loading.gif" style="width: 150px; height: 150px;" /></span>--%>Loading...</b>
                        </div>

                    </td>
                </tr>
            </table>
		 <div id="myModal" class="modal" style=" background-color: rgba(0, 0, 0, 0.5);text-align:center">
                <span class="close" id="closeBtn">&times;</span>
                <img class="modal-content" id="modalImage" style="width: 500px; height: 400px;;top:100px;" /><br />
		 <div id="buttonsec" style="display:none">
                <button type="button" style="margin-top:125px;" id="prevBtn" onclick="changeImage(-1)"><img style="width:100px;height:35px;"  src="../css/locationsvg/previmage.png" alt="buttonpng" border="0" /></button>
                <button type="button" style="margin-top:125px;" id="nextBtn" onclick="changeImage(1)"><img style="width:100px;height:35px;"  src="../css/locationsvg/nextimage.png" alt="buttonpng" border="0" /></button>
                </div>
                <%--<div class="modal-button-container">
              
            </div>--%>
            </div>
        </div>
        <%--</div>--%>
        <link href="css/SalesForce_New/bootstrap-select.min.css" rel="stylesheet" />
        <script src='https://kit.fontawesome.com/a076d05399.js' crossorigin='anonymous'></script>
        <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.10.2/jquery.min.js"></script>
        <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
        <script src="https://maps.googleapis.com/maps/api/js?sensor=false&key=AIzaSyAER5hPywUW-5DRlyKJZEfsqgZlaqytxoU"></script>
        <script src="MarkerLabel/MarkerLabel.js"></script>
        <script src="js/lib/leaflet.js"></script>


        <link rel="stylesheet" type="text/css" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css">
        <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-select/1.12.2/css/bootstrap-select.min.css">
        <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery/3.2.1/jquery.js"></script>
        <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
        <script src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-select/1.12.2/js/bootstrap-select.min.js"></script>
        <script src="assets/js/markerclusterer_compiled.js"></script>
        <script src="https://unpkg.com/@googlemaps/markerclusterer/dist/index.min.js"></script>
        <script src="https://developers.google.com/maps/documentation/javascript/examples/markerclusterer/markerclusterer.js"></script>


        <script type="text/javascript">
            var markers = []; var tablemark = []; var cardadd = []; var sfdets = '';var selssf = '<%=selsfdet%>'; var selddt = '<%=seldtdet%>';
            var prvMarkers = []; var Start = ''; var limit = ''; var St = ''; var le = '';
            var NavMarkers = []; var gmarkers = [];
            var lastOpenedInfoWindow;
            var lastAnimMark; var locstatmark = [];
            var infoWins = [];
            var infoWindow;
            var slatlng = "";
            var CusCnt = 0, tpval = 0, tltrs = 0;
            var path;
            var poly;
            var KMTot = 0;
            var z = 14;
            var Loaded = false;
	    var imageUrls = []; let currentIndex = 0;
            const closeBtn = document.getElementById('closeBtn');
            const modal = document.getElementById('myModal');
            const modalImage = document.getElementById('modalImage');
            function openmodel(imag) {
                currentIndex = 0;
                itm = '';
                var originalString = imag;
                const separatedArray = Array.from(
                    originalString.split(",")
                );

                imageUrls = [];
                for (var m = 0; m < separatedArray.length; m++) {
                    itm = 'http://fmcg.sanfmcg.com//photos/' + separatedArray[m] + '';
                    imageUrls.push(itm);
                }
		if (imageUrls.length > 1) {
                    $('#buttonsec').show();
                } else {
                    $('#buttonsec').hide();
                }
                displayCurrentImage(currentIndex);
                closeBtn.addEventListener('click', () => {
                    modal.style.display = 'none';
                });
            }
            window.onclick = function (event) {
                if (event.target == modal) {
                    modal.style.display = 'none';
                }
            };
            function displayCurrentImage(cs) {

                modal.style.display = 'block';
                modalImage.src = imageUrls[cs];
            }
            //$("#nextBtn").click(function () {
            //    alert(this.id);
            //    currentIndex += 1;

            //    if (currentIndex >= imageUrls.length) {
            //        currentIndex = 0;
            //    } else if (currentIndex < 0) {
            //        currentIndex = imageUrls.length - 1;
            //    }
            //    displayCurrentImage(currentIndex);
            //});

            function changeImage(step) {
                currentIndex += step;

                if (currentIndex >= imageUrls.length) {
                    currentIndex = 0;
                } else if (currentIndex < 0) {
                    currentIndex = imageUrls.length - 1;
                }
                displayCurrentImage(currentIndex);
            }



            $("#rmTags").hide();

            function clearmarks() {
                markers = []; cardadd = []; St = 0; le = 0; Start = 0; limit = 0; prvMarkers = []; NavMarkers = []; infoWins = [];locstatmark = [];
            }

            $('#teamattn').on('click', function () {
                $(".sidenavc").removeClass("active");
                $(this).closest(".sidenavc").addClass("active");

                $(".locattn").show();//$("#mgr").val('0').change();$("#mgrvis").val('0').change();
                $(".visitloc").hide();
                $(".router").hide();
                clearLocations();
                clearmarks();
                clearLocorders();
                $(".tagg").hide(); $('#Retmnu').empty(); $('#Retmnu').hide(); $("#idDetails").hide(); $('#tagtyp').hide();
                $("#mgrattn").empty();
                loadmgratten();
            })
            $('#sfvisit').on('click', function () {
                $(".sidenavc").removeClass("active");
                $(this).closest(".sidenavc").addClass("active");//$("#mgr").val('0').change();$("#mgrattn").val('0').change();
                $(".locattn").hide(); $(".router").hide(); clearLocations(); clearmarks(); $("#idDetails").show(); clearLocorders(); $('#attca').empty(); $('#attca').hide();
                $(".tagg").hide(); $(".visitloc").show(); $('.retnme').show(); $('#Retmnu').empty(); $('#Retmnu').hide(); $('#tagtyp').hide();
                loadsfdets();
            });

            $('#Tagretlr').on('click', function () {
                $(".sidenavc").removeClass("active");
                $(this).closest(".sidenavc").addClass("active");//$("#mgrattn").val('0').change();$("#mgrvis").val('0').change();
                $(".locattn").hide(); $(".router").hide(); $(".visitloc").hide(); clearLocations(); clearmarks(); clearLocorders(); $(".tagg").show(); $("#idDetails").hide();
                $('#attca').empty(); $('#attca').hide(); $('#mgr').empty();
                loadmgr(); //$("#mgr").val('0').change();
            });
            $(document).ready(function () {
                var selssf = '<%=selsfdet%>'; var selddt = '<%=seldtdet%>';
                if (selssf != '' && selddt != '') {
                    loadmgratten(); $('.arrow').closest(".sidenavc").addClass("active");
                    $(".locattn").hide(); $(".router").hide(); clearLocations(); clearmarks(); $("#idDetails").show(); clearLocorders(); $('#attca').empty(); $('#attca').hide();
                    $(".tagg").hide(); $(".visitloc").show(); $('.retnme').show(); $('#Retmnu').empty(); $('#Retmnu').hide(); $('#tagtyp').hide(); loadsfdets();
                    $('#mgrvis').val(selssf).change();
                    var parts = selddt.split("/");
                    var year = parts[0];var month = parts[1];var day = parts[2];                    
                    var formattedDate = year + "-" + month + "-" + day;                    
                    var inputElement = document.getElementById("visdate");                  
                    inputElement.value = formattedDate;                    
                    Callback(selddt);

                } else {
                    $('.users').closest(".sidenavc").addClass("active");
                    loadmgratten();
                }

            });

            function loadsfdets() {
                $('#mgrvis').empty();
                $('#mgrvis').append(sfdets);
                $('#mgrvis').selectpicker({
                    liveSearch: true
                });
            }

            var CurrSF;
            function GotoHome() {
                if ("<%=Session["sf_type"] %>" == "2") {
                    window.location.href = "DashBoard1.aspx"
                }
                if ("<%=Session["sf_type"] %>" == "3" && "<%=Session["sf_code"]%>" == "admin") {
                    window.location.href = "DashBoard.aspx"
                }
            }

            function loadmgratten() {
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "Location_master.aspx/loadallmgr",
                    data: "{'div_code':'<%=Session["div_code"]%>'}",
                    dataType: "json",
                    success: function (data) {
                        mgrli = JSON.parse(data.d);
                        if (mgrli.length > 0) {
                            $("#mgrattn").html('');
                            var Sf_name = $("#mgrattn");
                            // Sf_name.empty().append('<option selected="selected" value="0">Select Employee </option>');
                            Sf_name.empty();
                            for (var i = 0; i < mgrli.length; i++) {
                                sfdets += '<option value="' + mgrli[i].Sf_code + '">' + mgrli[i].sf_name + '</option>';
                                Sf_name.append($('<option value="' + mgrli[i].Sf_code + '">' + mgrli[i].sf_name + '</option>'))
                            }

                        }
                    },

                });
                $('#mgrattn').selectpicker({
                    liveSearch: true
                });

            }
            function Getdetai() {
                var se = $("#mgrattn").val(); clearLocations(); $(".retnme").show(); $('#attca').show(); $('#attca').empty(); clearmarks();
                Getlocatt(se); St = 0;
                setTimeout(function () { loadattcard(St) }, 5000);
            }

            $("#mgrattn").change(function () {
                var selesf = $('option:selected', this).val(); clearLocations();
                $(".retnme").show(); $('#attca').show(); $('#attca').empty();
                Getlocatt(selesf); St = 0;
                setTimeout(function () { loadattcard(St) }, 2000);
            });

            var icon = {
                url: "css/locationsvg/marker.png", // url
                scaledSize: new google.maps.Size(24, 30), // size
            };
            var map;
            function Getlocatt(n) {
                $("#idloading").show();
                var sfcode = n;
                CurrSF = n;
                var ddate = $("#date").val();
                if (ddate == "") {
                    document.getElementById('date').valueAsDate = new Date();
                    var ddt = new Date;
                    ddate = ddt.getFullYear() + '-' + (ddt.getMonth() + 1) + '-' + ddt.getDate();
                    //ddate = "2023-02-24";
                }
                $.ajax({
                    url: "Location_master.aspx/SFAttentances",
                    type: "POST",
                    async: false,
                    contentType: "application/json;charset=utf-8",
                    dataType: "json",
                    data: "{'SFCode':'" + sfcode + "','Date':'" + ddate + "','div_code':'<%=Session["div_code"]%>'}",
                    success: function (data) {
                        var marker, i, str;
                        markers = JSON.parse(data.d) || []
                        if (markers.length > 0) {
                            $('#attca').html('');
                            clearLocations();
                            str = "";
                            $("#rmTags").hide();
                            if (markers.length > 0) $("#rmTags").show();
                            var bounds = new google.maps.LatLngBounds();
                            infoWins = [];
                            for (var $i = 0; $i < markers.length; $i++) {

                                if (markers[$i].lat == null && markers[$i].long == null) {
                                    var lat = 28.7041; var long = 77.1025;
                                    marker = new google.maps.Marker({
                                        position: new google.maps.LatLng(lat, long),
                                        icon: icon,
                                        map: map
                                    });
                                    marker.setMap(null);
                                }
                                else {
                                    marker = new google.maps.Marker({
                                        position: new google.maps.LatLng(markers[$i].lat, markers[$i].long),
                                        icon: icon,
                                        map: map
                                    });
                                }
                                if (markers[$i].Profile_Pic == " ") {
                                    var imgtag = "<img class='imag' style='width: 40px;float:left' src='https://www.shareicon.net/data/128x128/2016/07/26/802043_man_512x512.png'>"
                                }
                                else {
                                    var imgtag = "<img class='imag' style='width: 40px;float:left' src='http://fmcg.sanfmcg.com//SalesForce_Profile_Img/" + markers[$i].Profile_Pic + "'>"
                                }
                                //strhtml = "<table><tr><td style='font-size:13px;padding:5px'>"+imgtag+"<b>" + markers[$i].Sf_name + "</b><br><label style='font-size:13px;padding:5px'>" + markers[$i].SF_Mobile + "</label></td></table>";
                                strhtml = "<table><tr><td class='tdt'>" + imgtag + "</td><td class='tdt'><a href='#' id='retailercode' onclick='openRetWin(" + $i + ")'>" + markers[$i].Sf_name + "</a><br>" + markers[$i].SF_Mobile + "</td></tr><tr><td style='padding: 6px;'><img class='svgs' src='../css/locationsvg/route2.svg'></img></td><td style='padding: 6px;'> <label class='names'>" + markers[$i].Route_Name + "</label></td></tr></table> ";

                                addInfoWindow(marker, strhtml);
                                prvMarkers.push(marker);

                                if (markers[$i].lat == null && markers[$i].long == null) {
                                    var lat = 28.7041; var long = 77.1025;
                                    var myLatLng = new google.maps.LatLng(lat, long);
                                    bounds.extend(myLatLng);
                                }
                                else {
                                    var myLatLng = new google.maps.LatLng(markers[$i].lat, markers[$i].long);
                                    bounds.extend(myLatLng);
                                }




                            }
                            map.fitBounds(bounds);
                            $('#selSF').append(str);
                            //$("#idloading").hide();

                        } else {
                            $("#idloading").hide();
                        }



                    },
                    error: function (error) {
                        console.log(`Error ${error}`);
                        $("#idloading").hide();
                    }
                });

            }
            $('#attca').on('scroll', function () {
                let div = $(this).get(0);
                if (div.scrollTop + div.clientHeight >= div.scrollHeight) {
                    le = 20; St = St + le;
                    loadattcard(St);
                }
            });
            function loadattcard(s) {
                const maxi = Math.min(markers.length, 20);
                const max = maxi + s;
                for (var $i = s; $i < max; $i++) {
                    if (markers[$i].Profile_Pic == " ") {
                        var imgtag = "<img class='imag' style='width: 40px;height:40px;float:left' src='https://www.shareicon.net/data/128x128/2016/07/26/802043_man_512x512.png'>"
                    }
                    else {
                        var imgtag = "<img class='imag' style='width: 40px;height:40px;float:left' src='http://fmcg.sanfmcg.com//SalesForce_Profile_Img/" + markers[$i].Profile_Pic + "'>"
                    }

                    str = ""; var addrs = '';
                    var img = ''; var tags = '';
                    if (markers[$i].lat != null && markers[$i].long != null) {
                        tags = "<label style='color:#1696CD;margin-left:75px;background-color:#bad9ff;width:75px;padding:6px;text-align:center;'>On Duty</label>";
                        var url = "//nominatim.openstreetmap.org/reverse?format=json&lon=" + markers[$i].long + '&lat=' + markers[$i].lat + "";
                        $.ajax({
                            url: url,
                            async: false,
                            dataType: 'json',
                            success: function (data) {
                                addrs = data.display_name;
                            }
                        });
                        if (markers[$i].UDistance != 0) {

                            var trave = markers[$i].UDistance / 1000;
                            var trakm = Math.round(trave) + '  Km';
                        }
                        else {
                            var trakm = 'Did Not Closed'
                        }
                        str += "<div class='card'style='margin:15px;padding: 10px;background-color:white'><table><tr><td class='tdt'>" + imgtag + "</td><td class='tdt'><a href='#' id='retailercode' onclick='openRetWin(" + $i + ")'>" + markers[$i].Sf_name + "</a><br>" + markers[$i].SF_Mobile + "</td></tr></table> " +
                            " <hr style='border: 1px solid rgba(46, 48, 50, 0.1);margin-top:5px' /><div style='margin-top:-10px'><table><td><img class='login' src='../css/locationsvg/arrow-small-left.svg'></img></td><td>" + markers[$i].Start_Time + "</td><td><img class='logout' src='../css/locationsvg/arrow-small-right.svg'></img></td><td>" + markers[$i].End_Time + " </td><td> " + tags + "</td></table>" +
                            "<table><tr><td style='padding: 6px;'><img class='svgs' src='../css/locationsvg/route2.svg'></img></td><td style='padding: 6px;'> <label class='names'>" + markers[$i].Route_Name + "</label></td></tr><tr><td style='padding: 6px;'><img class='svgs' src='../css/locationsvg/location-crosshairs.svg'></img></td><td style='padding: 6px;'> <label class='names'>  " + markers[$i].lat + "," + markers[$i].long + "</label></td></tr><tr><td style='padding: 6px;'><img class='svgs' src='../css/locationsvg/marker1.svg'></img></td><td style='padding: 6px;'>  <label class='names'> " + addrs + " </label></td></tr> " +
                            " <tr><td style='padding: 6px;'><img class='svgs' src='../css/locationsvg/motorcycle.svg'></img></td><td style='padding: 6px;'> <label class='names'>" + trakm + "</label></td></tr></table > <div></div > ";

                    }
                    else {
                        tags = "<label style='color:red;background-color:#ffbd9e;padding:8px;text-align:center;float:right;'>Not Marked</label>";
                        str += "<div class='card'style='margin:15px;padding: 10px;background-color:white'><table><tr><td class='tdt'>" + imgtag + "</td><td class='tdt' style='width: 225px;'><a href='#' id='retailercode' onclick='openRetWin(" + $i + ")'>" + markers[$i].Sf_name + "</a><br>" + markers[$i].SF_Mobile + "</td><td>" + tags + "</td></tr></table></div > ";

                    }

                    $('#attca').append(str);
                    $("#idloading").hide();

                }
            }
            $("#mgrvis").change(function () {

                selDt = $('#visdate').val();clearLocations(); clearLocorders();
                if (selDt == '') {
                    var cdt = new Date;
                    selDt = cdt.getFullYear() + '-' + (cdt.getMonth() + 1) + '-' + cdt.getDate();
                }
                Callback(selDt);

            });
            function Getdatewise() {
                selDt = $('#visdate').val();clearLocations(); clearLocorders();
                Callback(selDt);
            }
            var timer; var selDt = '';
            function Callback(DtTm, movMark) {
                if (!movMark) movMark = false;
                KMTot = 0;
                var selff = $("#mgrvis").val();
                $.ajax({
                    url: "Location_master.aspx/SFordergetdets",
                    type: "POST",
                    contentType: "application/json;charset=utf-8",
                    dataType: "json",
                    data: "{'SFCode':'" + selff + "','Date':'" + DtTm + "','div_code':'<%=Session["div_code"]%>'}",
                    success: function (data) {

                        if (movMark == true) {
                            lastIndx = (markers.length > 0) ? markers.length : 0;
                            var newMarkers = JSON.parse(data.d) || [];
                            markers = $.merge(markers, newMarkers);
                            if (newMarkers.length > 0) moveMarker(lastIndx);

                        } else {
                            markers = JSON.parse(data.d) || [];
                            clearLocorders();
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

                    },

                });
                gettravelkm(selff, DtTm);
            }
            function gettravelkm(selff, DtTm) {
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "Location_master.aspx/gettravelkms",
                    data: "{'SFCode':'" + selff + "','Date':'" + DtTm + "','div_code':'<%=Session["div_code"]%>'}",
                    dataType: "json",
                    success: function (data) {
                        kms = JSON.parse(data.d);
                        $("#tkm").html("&nbsp;");

                        if (kms.length > 0) {
                            var km = kms[0].UDistance + ' ' + 'Km';
                            $("#tkm").html(km);
                        }
                        else {
                            var km = 'Did Not Closed';
                            $("#tkm").html(km);
                        }
                    },

                });
            }

            function clearLocorders() {
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

                var src = new google.maps.LatLng(markers[0].lat, markers[0].lng);

                Vstlbl = 0; infoWins = [];
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
				position = new google.maps.LatLng(markers[i].geo_lat, markers[i].geo_log);
                                var marker = new MarkerWithLabel({ /*new MarkerWithLabel({*/
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
				 var gallery = ''; var imageUrl = [];
                                if (markers[i].imgurl != '') {
                                    var imags = markers[i].imgurl;
                                    gallery = "<img src='../css/locationsvg/photo-gallery.svg'  id='imageIcon' style='width: 28px;' onclick='openmodel(&apos;" + imags + "&apos;)' ></img>";

                                }
                                if (markers[i].ordfld == 0) {
                                    CusCnt++;
                                    tpval += ((markers[i].POB_Value != '0' && markers[i].POB_Value != null) ? parseFloat(markers[i].POB_Value) : 0);
                                    tltrs += ((markers[i].net_weight_value != '0') ? parseFloat(markers[i].net_weight_value) : 0);
                                    //slno = 0;
                                    sStr += '<li class="card" onclick="openRetWin(' + slno + ')"><div style="display:block;"><table style="width:100%;"><tr style="background:#dcdcdc;"><td style="padding:5px ;border-left:solid 2px #dcdcdc;width:175px;"><font style="color:darkblue;font:15px tahoma;">' + markers[i].Trans_Detail_Name + '' + customer + '</td>'
                                        + '<td style="padding:5px;border-right:solid 2px #dcdcdc;"><font style="color:black;font:15px Comic Sans;font-weight: bold;">' + markers[i].OrderType + '</font></td><td>' + gallery + '</td></tr>'
                                        + '<tr><td style="padding:5px;border-left:solid 2px #dcdcdc;"><font style="color:black;font:11px tahoma;">Start Time : ' + markers[i].StartOrder_Time + '</font></td><td><font style="color:black;font:11px tahoma;">End Time :' + markers[i].EndOrder_Time + '</font></td><td style="padding:5px;border-right:solid 2px #dcdcdc;"></td></tr>'
                                        + '<tr><td style="padding:5px;border-left:solid 2px #dcdcdc;"><font style="color:black;font:11px tahoma;">POB ( Value ) : ' + ((markers[i].POB_Value != '0' && markers[i].POB_Value != null) ? '' + markers[i].POB_Value : '-') + '</td>'
                                        + '<td><font style="color:black;font:11px tahoma;">POB ( Liters ) : ' + ((markers[i].net_weight_value != '0') ? '' + markers[i].net_weight_value : '-') + '</td><td  style="padding:5px;border-right:solid 2px #dcdcdc;"></td></tr>'
                                        + '<tr><td style="padding:5px;border-left:solid 2px #dcdcdc;"><a style="color:#00303f; font:bold 10px verdana;">' + sHH + ":" + ((mm < 10) ? '0' : '') + mm + ":" + ((ss < 10) ? '0' : '') + ss + " " + ((hh > 11) ? 'PM' : 'AM') + '</a></td><td ><font style="color:black;font:11px tahoma;font-weight: bold;">' + markers[i].Remarks + '</font></td><td style="padding:5px;border-right:solid 2px #dcdcdc;"></td></tr>' +
                                        '<tr><td colspan=3 style="padding:5px;font:12px verdana;border:solid 2px #dcdcdc;border-top-width:0px;border-radius: 0px 0px 5px 5px;"><font style="color:darkgreen;"></font></td></tr>'
                                        + '</table></div></li>';
                                    marker.setIcon('img/shop.png');
                                    slno++;

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
                                    + '<td style="width:50%;padding:0px 5px 10px 5px ;border-bottom:solid 2px #dcdcdc;">' + gallery + '<font style="color:#00303f; font:bold 12px verdana;float: right;">' + markers[i].OrderType + '</font><br><a style="color:#00303f; font:bold 10px verdana;float: right;">' + markers[i].DtTime + '</a></td></tr>'
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



                            }


                        } else {
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
                                var des = new google.maps.LatLng(markers[i + 1].lat, markers[i + 1].lng);

                                dis = getDistance(src, des, "K");
                                if (dis > 0.130)
                                    var src = des; else dis = 0;
                                KMTot += dis;
                            }

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



                            CusCnt++;
                            tpval += ((markers[i].POB_Value != '0' && markers[i].POB_Value != null) ? parseFloat(markers[i].POB_Value) : 0);
                            tltrs += ((markers[i].net_weight_value != '0') ? parseFloat(markers[i].net_weight_value) : 0);
                            sStr += '<li><div style="display:block;"><table style="width:100%"><tr style="background:#dcdcdc;"><td style="padding:5px ;border-left:solid 2px #dcdcdc;"><font style="color:darkblue;font:15px tahoma;">' + markers[i].Trans_Detail_Name + '</td>'
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

            function loadmgr() {
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "Location_master.aspx/loadallmgr",
                    data: "{'div_code':'<%=Session["div_code"]%>'}",
                    dataType: "json",
                    success: function (data) {
                        mgrli = JSON.parse(data.d);
                        if (mgrli.length > 0) {
                            $("#mgr").html('');
                            var Sf_name = $("#mgr");
                            Sf_name.empty().append('<option selected="selected" value="0">Select Employee </option>');
                            for (var i = 0; i < mgrli.length; i++) {
                                Sf_name.append($('<option value="' + mgrli[i].Sf_code + '">' + mgrli[i].sf_name + '</option>'))
                            }

                        }
                    },

                });
                $('#mgr').selectpicker({
                    liveSearch: true
                });

            }

            function Getallroute(c) {
                var SFcode = c;
                $('#rout').selectpicker('destroy');
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "Location_master.aspx/allocateroute",
                    data: "{'SFcode':'" + SFcode +"','div_code':'<%=Session["div_code"]%>'}",
                    dataType: "json",
                    success: function (data) {
                        var router = [];
                        router = JSON.parse(data.d);
                        // if (router.length > 0) {
                        $("#rout").html('');
                        var Territory_Name = $("#rout");
                        Territory_Name.empty().append('<option selected="selected" value="0">All Routes </option>');
                        for (var i = 0; i < router.length; i++) {
                            Territory_Name.append($('<option value="' + router[i].Territory_Code + '">' + router[i].Territory_Name + '</option>'))
                        }

                        //}
                    },

                });

                $('#rout').selectpicker({
                    liveSearch: true
                });
                $('#rout').selectpicker('refresh');

            }


            $("#mgr").change(function () {
                var selesf = $('option:selected', this).val(); $(".router").show();
                $(".retnme").show(); $('#tagtyp').show(); $('#Retmnu').show(); $('#Retmnu').html(''); clearLocations(); clearmarks(); Getallroute(selesf);
                var routsf = $('#rout').val();var interest = $('ul#typnav').find('li.active').attr('interest');
                Getlocation(selesf, routsf,interest);

            });
            $("#rout").change(function () {
                var routsf = $('option:selected', this).val();
                $(".retnme").show(); $('#tagtyp').show(); $('#Retmnu').show(); $('#Retmnu').html(''); clearLocations();//clearmarks();
                var selesf = $('#mgr').val();var interest = $('ul#typnav').find('li.active').attr('interest');
                Getlocation(selesf, routsf,interest);
            });

            function refresh() {
                var selesf = $('#mgr').val();
                $(".retnme").show(); $('#tagtyp').show(); $('#Retmnu').show(); clearLocations(); clearmarks();
                var routsf = $('#rout').val();var interest = $('ul#typnav').find('li.active').attr('interest');
                Getlocation(selesf, routsf,interest);

            }
            $('#Distab').on('click', function () {
                var selesf = $('#mgr').val(); $(".router").show();
                $(".retnme").show(); $('#tagtyp').show(); $('#Retmnu').show(); $('#Retmnu').html(''); clearLocations(); clearmarks(); Getallroute(selesf);
                var routsf = $('#rout').val();
                var interest = 1; 
                Getlocation(selesf, routsf,interest);
            })
             $('#Rettab').on('click', function () {
                var selesf = $('#mgr').val(); $(".router").show();
                $(".retnme").show(); $('#tagtyp').show(); $('#Retmnu').show(); $('#Retmnu').html(''); clearLocations(); clearmarks(); Getallroute(selesf);
                var routsf = $('#rout').val();
                var interest = 2;
                Getlocation(selesf, routsf,interest);
            })

            $('#Retmnu').on('scroll', function () {
                let div = $(this).get(0);
                if (div.scrollTop + div.clientHeight >= div.scrollHeight) {
                    var sele = $('#mgr').val(); limit = 20; Start = Start + limit; var routsf = $('#rout').val();
                    loadaddrs(sele, Start, limit, routsf);
                }
            });

            function loadaddrs(selesf, Start, limit, routsf) {

                const maxi = Math.min(markers.length, 20);
                const max = maxi + Start;

                for (var $i = Start; $i < max; $i++) {
                    str = "";
                    var img = ''; var tags = ''; var ppic = '';
                    if (cardadd[$i].Visit_hours != '' && cardadd[$i].Visit_hours != null && cardadd[$i].Visit_hours != 'null') {
                        ppic = "<img  id='propic' style='width:32px;height: 33px;border-radius:50%;' src='http://fmcg.sanfmcg.com//photos/" + cardadd[$i].Visit_hours + "'>";
                    }
                    else {
                        ppic = "<img class='svgs' src='../css/locationsvg/store-shopper-svgrepo-com.svg'></img>";
                    }
                    if (cardadd[$i].Tag_stat == 'Tagged') {
                        img = "<img style='width: 22px;height: 22px;float:right' src='https://pngimg.com/uploads/pin/pin_PNG81.png'>";
                        tags = "<label class='tagged'>Tagged</label>";
                        latlog = "<label class='names'>" + cardadd[$i].lat + ", " + cardadd[$i].long + "</label>";
                    } else {
                        tags = "<label class='untagged'>Untagged</label>";
                        latlog = "<label class='names'>Location  Not Fetched</label>";
                    }
                    var sl_no = cardadd[$i].RowNum - 1;
                    str += "<div class='card'style='margin:15px;padding:10;background-color:white;' ><table><tr><td class='tdt'>" + ppic + "</td><td class='tdt'style='width:300px;'><a href='#'  class='heade'  id='retailercode' onclick='openRetWin(" + $i + ")'>" + cardadd[$i].Retailer_Name + "</a><br>" +
                        "<label class='names' style='margin-left:10px' >" + cardadd[$i].Retailer_phone + " </label></td><td>" + tags + "</td></tr><tr><td class='tdt'><img class='svgs' src='../css/locationsvg/route2.svg'></img></td><td class='tdt'><label class='names'>" + cardadd[$i].Route + " </label></td></tr><tr><td class='tdt'><img class='svgs' src='../css/locationsvg/location-crosshairs.svg'></img></td><td class='tdt'>" + latlog + "</td></tr><tr><td class='tdt'><img class='svgs' src='../css/locationsvg/marker1.svg'></img></td><td class='tdt'><label class='names'>" + cardadd[$i].Retailer_Address + " </label></td></tr>" +
                        "<tr><td class='tdt'><img class='svgs' src='../css/locationsvg/store-svgrepo-com.svg'></img></td><td class='tdt'><label class='names'>" + markers[$i].Doc_Special_Name + "</label></td></tr><table></div > ";

                    $('#Retmnu').append(str);


                }

                $("#idloading").hide();
            }



            function rmAllSFTags() {
                var routname = $('#rout option:selected').text();
                var roueco = $('#rout').val();

                if (confirm("do you want remove all Taggings for route of " + routname + "?")) {
                    if (confirm("Are sure remove all Taggings for route of" + routname + "?")) {
                        $.ajax({
                            url: "Retailers_Mapping.aspx/rmallrSFTags",
                            type: "POST",
                            contentType: "application/json;charset=utf-8",
                            dataType: "json",
                            data: "{'SFCode':'" + CurrSF + "','roueco':'" + roueco + "'}",
                            success: function (data) {
                                if (data.d == 'success') {
                                    alert('Sucessfully Untagged Retailers for routes..!');
                                    refresh();
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
            function rmRetTag(RetCd, RetNm) {
                if (confirm("Do you want Un-Tag \"" + RetNm + "\"?")) {
                    if (confirm("Are sure Un-Tag \"" + RetNm + "\"?")) {
                        $.ajax({
                            url: "Location_master.aspx/untagRetailers",
                            type: "POST",
                            contentType: "application/json;charset=utf-8",
                            data: "{'custCode':'" + RetCd + "'}",
                            dataType: "json",
                            success: function (data) {
                                if (data.d == 'Sucess') {
                                    alert('Sucessfully Untag Retailer..!');
                                    refresh();
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




            var map;
            function Getlocation(n, r,i) {
                $("#idloading").show();
                var sfcode = n;
                var routcode = r;
                var interest = i;
                CurrSF = n;
               
                if (interest == '2') {
                    $.ajax({
                        url: "Location_master.aspx/GetSFTaggedRetailers",
                        type: "POST",
                        contentType: "application/json;charset=utf-8",
                        dataType: "json",
                        data: "{'SFCode':'" + sfcode + "','div_code':'<%=Session["div_code"]%>','routcode':'" + routcode + "'}",
                        success: function (data) {
                            markers = JSON.parse(data.d) || [];
                            makerappend(n, r);
                        },
                        error: function (error) {
                            console.log(`Error ${error}`);
                            $("#idloading").hide();
                        }
                    });
                }
                else if (interest == '1')
                $.ajax({
                    url: "Location_master.aspx/GetSFTaggeddistr",
                    type: "POST",
                    contentType: "application/json;charset=utf-8",
                    dataType: "json",
                    data: "{'SFCode':'" + sfcode + "','div_code':'<%=Session["div_code"]%>','routcode':'" + routcode + "'}",
                    success: function (data) {
                        markers = JSON.parse(data.d) || [];
                        makerappend(n, r);
                    },
                    error: function (error) {
                        console.log(`Error ${error}`);
                        $("#idloading").hide();
                    }
                });

            }
            function makerappend(n, r) {
                var marker, i, str;


                tablemark = markers;
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
                        "<label class='names' style='margin-left:10px' >" + markers[$i].Retailer_phone + " </label></td><td style='text-align:right' colspan='2'><i style='background-color: #c9c9c9;padding:7px;' id='rmTag' class='button' onclick='rmRetTag(\"" + markers[$i].Retailer_Code + "\",\"" + stri + "\")'>Untag</i></td></tr><tr><td class='tdt'><img class='svgs' src='../css/locationsvg/route2.svg'></img></td><td class='tdt'><label class='names'>" + markers[$i].Route + " </label></td></tr><tr><td class='tdt'><img class='svgs' src='../css/locationsvg/marker1.svg'></img></td><td class='tdt'><label class='names'>" + markers[$i].Retailer_Address + " </label></td></tr>" +
                        "<tr><td class='tdt'><img class='svgs' src='../css/locationsvg/store-svgrepo-com.svg'></img></td><td class='tdt'><label class='names'>" + markers[$i].Doc_Special_Name + "</label></td></tr><table>";

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
                markerCluster = new MarkerClusterer(map, gmarkers,
                    {
                        imagePath:
                            'https://developers.google.com/maps/documentation/javascript/examples/markerclusterer/m'
                   });


                map.fitBounds(bounds);


                Start = 0; limit = 5;
                loadaddrs(n, Start, limit, r);
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
                map.setZoom(21);

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


            // google.maps.event.addDomListener(window, 'load', initMap);
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
                    mapTypeId: 'roadmap', zoom: 5, center: { lat: clat, lng: clng }
                };
                map = new google.maps.Map(document.getElementById("map_canvas"), mapOptions);
                $("#modal").css('display', 'block');
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
                    prvMarkers = [];locstatmark = [];
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
            initMap();
        </script>
    </form>
</body>
</html>

