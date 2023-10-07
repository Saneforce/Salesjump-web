<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SF_Modal_Map.aspx.cs" Inherits="MIS_Reports_SF_Modal_Map" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    <div id="dvMap" style="width: 1354px;height: 661px;position: absolute;overflow: hidden;">
    </div>
    </div>
    </form>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/2.0.2/jquery.min.js"></script>
    <script type="text/javascript" src="https://maps.googleapis.com/maps/api/js?sensor=false&key=ASLAAP_AIzaSyAER5hPywUW-5DRlyKJZEfsqgZlaqytxoU&libraries=geometry,places&ext=.js"></script>
    <script type="text/javascript">
        var lat=<%=lat%>;
        var lng=<%=lng%>;
        var divcode;
        window.onload = function () {
            var mapOptions = {
                center: new google.maps.LatLng(lat,lng),
                zoom: 14,
                mapTypeId: google.maps.MapTypeId.ROADMAP
            };
            var infoWindow = new google.maps.InfoWindow();
            var latlngbounds = new google.maps.LatLngBounds();
            var map = new google.maps.Map(document.getElementById("dvMap"), mapOptions);
            var myLatlng = new google.maps.LatLng(lat, lng);
            var marker = new google.maps.Marker({
                position: myLatlng,
                map: map
            });
        }
        </script>
</body>
</html>