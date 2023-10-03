<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ShowMap.aspx.cs" Inherits="MasterFiles_ShowMap"
    EnableEventValidation="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>ShowMap</title>
    <link href="css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="css/ionicons.min.css" rel="stylesheet" type="text/css" />
    <script src="<%=Page.ResolveUrl("~/js/jquery.min.js")%>" type="text/javascript"></script>
    <script src="<%=Page.ResolveUrl("~/js/Director/app.js")%>" type="text/javascript"></script>
    <script src="<%=Page.ResolveUrl("~/js/bootstrap.min.js")%>" type="text/javascript"></script>
    <style type="text/css">
        canvas, .ChartTop
        {
            border-radius: 6px;
        }
        .noti
        {
            border: solid 1px #000000;
        }
        .notification_div
        {
            border-radius: 6px;
            width: 400px;
            height: 450px;
            border: solid 1px #cccccc;
            padding-left: 50px;
            margin-left: 50px;
        }
        .ChartTop
        {
            display: inline-block;
            width: 232px;
            height: 120px;
            border: solid 1px #cccccc;
        }
        .Chartdown
        {
            border-radius: 6px;
            display: inline-block;
            width: 696px;
            height: 150px;
            border: solid 1px #cccccc;
        }
        .ddl
        {
            border: 1px solid #1E90FF;
            border-radius: 4px;
            margin: 2px;
            font-family: Andalus;
            background-image: url('css/download%20(2).png');
            background-position: 88px;
            background-position: 88px;
            background-repeat: no-repeat;
            text-indent: 0.01px; /*In Firefox*/
        }
    </style>
    <script type="text/javascript" src="http://maps.googleapis.com/maps/api/js?key=AIzaSyBG-dwpBFBzcI8Y5AOoBK2H8SFQh9bRnY4"></script>
    <script type="text/javascript">
		var map,infoWindow;var prvMarkers=new Array();
        window.onload = function () {
		
			var mapOptions = {
                center: new google.maps.LatLng(markers[0].lat, markers[0].lng),
                zoom: 12,
                mapTypeId: google.maps.MapTypeId.ROADMAP
            };
			map = new google.maps.Map(document.getElementById("dvMap"), mapOptions);
            resetMap();
			var myVar = setInterval(function(){getMapInformation();}, 30000);
		}
		function clearLocations() {
			/*if(CreateMarker && CreateMarker.innerHTML)
				CreateMarker.innerHTML = "";*/
			//infoWindow.close();
			for (var i = 0; i < prvMarkers.length; i++) {
				prvMarkers[i].setMap(null);
			}
			prvMarkers=[]; 
			map.setCenter(new google.maps.LatLng(36.1611, -116.4775), 6);
		}
		function resetMap()
		{
			var infoWindow = new google.maps.InfoWindow();
			var lat_lng = new Array();
			geocodeLatLng(0, markers[0].lat, markers[0].lng);
			var latlngbounds = new google.maps.LatLngBounds();
			for (i = 0; i < markers.length; i++) {
				var data = markers[i];
				var myLatlng = new google.maps.LatLng(data.lat, data.lng);
				var geocoder = new google.maps.Geocoder();
				lat_lng.push(myLatlng);
				var marker = new google.maps.Marker({
				   	position: myLatlng,
				   	map: map,
				   	title: data.title,
					icon: new google.maps.MarkerImage(data.icon)
				}); 
				//if(i> 0 && i < (markers.length-1)) marker.setMap(null);
				prvMarkers.push(marker)
				latlngbounds.extend(marker.position);
				(function (marker, data) {
					google.maps.event.addListener(marker, "click", function (e) {

						infoWindow.setContent('<div id="content" style="margin-left:15px;margin-top:2px;overflow:hidden;">' + '<br><font style="color:darkblue;font:15px tahoma; margin-left:5px;">' + data.title + '</font>' + '<a style="color:#00303f; font:bold 12px verdana;float: right;">' + data.dteT + '</a></div>'  + '<br><font style="color:black;font:15px tahoma; margin-left:5px;">'+((data.POB!='0')?'POB:' + data.POB:'') + '</font>'+'<br><div style="font:14px verdana;color:darkgreen; margin-left:5px;">' + data.description + '</div>' + '</div>');                                                         
						infoWindow.open(map, marker);
					});
				})(marker, data);
			}
			map.setCenter(latlngbounds.getCenter());
			map.fitBounds(latlngbounds);

			//***********ROUTING****************//
			var totalKM=0
			for (var i = 0; i < lat_lng.length; i++) {
				var src = lat_lng[i];
				var values=[src,des];
			    var waypts=[];
	
				for (var j = 0; j < 20; j++) {
					if (i< lat_lng.length-1){		
				     	waypts.push({location:lat_lng[i],stopover:true}); 
						i++;
					}
				}
				var des = lat_lng[i];

				var service = new google.maps.DirectionsService();
	            var request = 
				{
		            origin: src,
	                destination: des,
		        	waypoints: waypts,
		        	optimizeWaypoints: true,
		        	travelMode: google.maps.DirectionsTravelMode.WALKING
	            };
	            service.route(request, function (response, status) {
		            if (status == google.maps.DirectionsStatus.OK) {
			            directionsDisplay = new google.maps.DirectionsRenderer({
							suppressMarkers: false,
				            suppressInfoWindows: false,
	            			//polylineOptions: { strokeColor: '#0093f0', strokeOpacity: 0.5 },
				            preserveViewport: true,
							markerOptions : {icon: 'bus5c.png'}
						});
		            	directionsDisplay.setDirections(response);
		                directionsDisplay.setMap(map);

					    tot=computeTotalDistance(directionsDisplay.getDirections());
						totalKM+=tot;
						console.log(tot+"-"+totalKM);
						document.getElementById('total').innerHTML = roundNo(totalKM,2) + ' km';
					}
	            });
			}
		}
		function roundNo(number, precision) {
                    precision = Math.abs(parseInt(precision)) || 0;
                    var multiplier = Math.pow(10, precision);
                    return (Math.round(number * multiplier) / multiplier);
                }
		function computeTotalDistance(result) {
	        var total = 0;
	        var myroute = result.routes[0];
	        for (var i = 0; i < myroute.legs.length; i++) {
	          total += myroute.legs[i].distance.value;
	        }
	        total = total / 1000;
			return total;
	        
	   	}

		getMapInformation=function(){
			dSFCode="<%=Request.QueryString["sf_Code"].ToString()%>";
			dRDate=markers[markers.length-1].dteT;
			PageMethods.GetMapData(dSFCode,dRDate, function (data) {
			 clearLocations();
				nmarkers=JSON.parse(data);
				markers=$.extend(markers,nmarkers);
				resetMap();
			});

			//	setTimeout(getMapInformation(),8000);
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
</head>
<body>
    <form id="form1" runat="server">
		<table width="100%">
			<tr>
				<td width="77%" valign="top">
					<div id="dvMap" style="height: 650px; width: 100%; float: left">
					</div>
				</td>
				<td width="23%">
					<div id="left" style="width:98%;padding-top: 5px;">
						<!--chat start-->
						<input type="hidden" name="hSFCode" id="hSFCode" runat="server"/>
						<input type="hidden" name="hRDate" id="hRDate" runat="server"/>
						<div  class="panel-heading" style="display:block;white-space:nowrap;padding: 10px 15px;background: #ff865c;color: #fff;">Kilometers Traveled</div>
						<span id="total" style="display:block;white-space:nowrap;padding: 10px 15px;font-weight:bold;border:solid 1px #000;border-radius: 0px 0px 5px 5px;border-top-width: 0px;"></span>
						<small><small><br></small></small>
						<section class="panel">
							<header class="panel-heading" style="background-color:#ff865c;color:White; font-size:14px; width:100%; font-family:Sans-Serif">
								Location Detail
							</header>
							<div class="panel-body" id="noti-box" style="Display:block;overflow:auto; height:500px; width:100%;border:solid 1px #000;border-radius: 0px 0px 5px 5px;border-top-width: 0px;">
								<asp:DataList ID="DataList1" runat="server" RepeatColumns="1" OnItemDataBound="Item_Bound">
									<ItemTemplate>
										<div class="desc">
											<asp:Label ID="lblInput" runat="server" Width="255px" style="padding-top:10px;padding-left:5px;"  Text='<%# Eval("Trans_Detail_Name") %>'></asp:Label><br/>
											<asp:Label ID="cmttype" class="addrs" runat="server" Text='Address Loading...'></asp:Label>
											<div class="thumb">
												<span class="badge bg-theme" style="background-color:transparent;text-align:right;"></span>
												<asp:Label ID="Lab_lati" runat="server" Visible="false" Text='<%# Eval("lati") %>' style="font-style:bold;font-size:10px;color:#a8b0b3;" ></asp:Label>
												<asp:Label ID="Lab_long" runat="server" Visible="false" Text='<%# Eval("long") %>' style="font-style:bold;font-size:10px;color:#a8b0b3;" ></asp:Label>
												<asp:Label ID="daytime" runat="server" Text='<%# Eval("DtTm") %>' style="font-style:bold;font-size:10px;color:#a8b0b3;" ></asp:Label><i class="fa fa-clock-o" style="color:#a6aeb1;"></i>
													 
											</div>
										  
										</div>
										   
									</ItemTemplate>
								</asp:DataList>
							</div>
						</section>
					</div>
				</td>
			</tr>
		</table>
	<asp:ScriptManager ID="ScriptService" EnablePageMethods="true" runat="server">
    </asp:ScriptManager>
    </form>
</body>
</html>
