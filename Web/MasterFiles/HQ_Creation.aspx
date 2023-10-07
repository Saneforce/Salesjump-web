<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="HQ_Creation.aspx.cs" Inherits="MasterFiles_HQ_Creation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<link rel="stylesheet" href="https://unpkg.com/leaflet@1.7.1/dist/leaflet.css" />
        <div class="row">
            <div class="col-lg-12 sub-header">
                HQ Creation
            </div>
        </div>
        <div class="col-sm-5" style="margin-top: 21px;">        
            <input type="hidden" id="hscode" />
            <div class="row">
                <div class="col-sm-3">
                    <label>HQ Name</label>
                </div>
                <div class="col-sm-7">
                    <input type="text" id="txtsname" class="form-control" autocomplete="off" />
                </div>
                <button style="display:none;" class="btn btn-primary" type="button" id="findLatLngBtnId">Find</button>
            </div>
            <div class="row" style="margin-top: 10px;">
                <div class="col-sm-3">
                    <label>HQ Type</label>
                </div>
                <div class="col-sm-7">
                    <select class="form-control typ" id="ddltyp">
                        <option value="0">--Select--</option>
                        <option value="1">Metro</option>
                        <option value="2">Major</option>
                        <option value="3">Others</option>
                    </select>
                </div>
            </div>
            <div class="row" style="margin-top: 10px;">
                <div class="col-sm-3">
                    <label>Location</label>
                </div>
                <div class="col-sm-7">
                    <input type="text" class="col-sm-6" id="txtlat" MaxLength="11" placeholder="Latitude" pattern="^[0-9]+$"/>
                    <input type="text" class="col-sm-6" id="txtlong" MaxLength="11" placeholder="Longitude" pattern="^[0-9]+$"/>
                    <%--onkeyup = "CheckSpaceChar(event.keyCode, this);" onkeydown = "return CheckSpaceChar(event.keyCode, this);"--%>
                </div>
            </div>
                <div class="row">
                    <button type="button" class="btn btn-primary" style="margin-top: 65px;margin-left: 159px;" id="btnsave">Save</button>
                </div><br /><br />
        </div>
        <div  class="col-sm-7">
    <div id="dvMap" style="width: 640px; height: 512px;top: -42px;">
    </div></div>
   <script type="text/javascript" src="https://unpkg.com/leaflet@1.7.1/dist/leaflet.js"></script>
    <script type="text/javascript">
        var lat;
        var long;
        var divcode;
		 var dtss = [];
       
        function clear() {
            $('#txtsname').val('');
            $('#ddltyp').val(0);
            $('#txtlat').val('');
            $('#txtlong').val('');
        }


        function fillslot(scode) {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "HQ_Creation.aspx/getHQ1",
                data: "{'divcode':" + divcode + ",'scode':" + scode + "}",
                dataType: "json",
                success: function (data) {
                    var dts = JSON.parse(data.d) || [];
                    $('#hscode').val(dts[0].HQ_ID);
                    $('#txtsname').val(dts[0].HQ_Name);
                    $('#ddltyp').val(dts[0].HQ_Type_ID);
                    var dllong=dts[0].latlong;
                    dllong=dllong.split(':');
                    $('#txtlat').val(dllong[0]);
                    $('#txtlong').val(dllong[1]);
                    var marker = new L.Marker(new L.LatLng(dllong[0], dllong[1]));
                    marker.addTo(map);
                    map.setView([dllong[0], dllong[1]]);
                },
                error: function (data) {
                    alert(JSON.stringify(data));
                }
            });
        }

        function fillHQID(divcode) {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "HQ_Creation.aspx/getHQID",
                data: "{'divcode':" + divcode + "}",
                dataType: "json",
                success: function (data) {
                    var dts = JSON.parse(data.d) || [];
                    $('#hscode').val(dts[0].HQ_ID);
                },
                error: function (data) {
                    alert(JSON.stringify(data));
                }
            });
        }
	  $(document).ready(function () {
				divcode =<%=Session["div_code"]%>;
				//getdatalist();
			 $('#hscode').val(<%=hqcode%>);
			 var mapOptions = {
				 center: new L.LatLng(13.082680, 80.270721),
				 zoom: 8,
				 mapTypeId: 'roadmap'
			 };
			 map = L.map('dvMap', mapOptions);
			 var layer = new L.TileLayer('http://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png');
			 map.addLayer(layer);

			 latlngbounds = L.latLngBounds();
			 map.on('click', function (e) {
				 $(document).find('.leaflet-marker-icon').each(function () {
					 this.remove()
				 });
				 $(document).find('.leaflet-marker-shadow').each(function () {
					 this.remove()
				 });

				 lat = e.latlng.lat.toFixed(6);
				 long = e.latlng.lng.toFixed(6);
				 var marker = new L.Marker(new L.LatLng(lat, long));
				 marker.addTo(map);

				 $('#txtlat').val(lat);
				 $('#txtlong').val(long);
			 });

				$("#findLatLngBtnId").click(function () {
					var geocoder = new google.maps.Geocoder();
					geocoder.geocode({
						"address": $("#txtsname").val()
					}, function (results, status) {
						if (status == google.maps.GeocoderStatus.OK) {
							$("#txtlat").val(results[0].geometry.location.lat().toFixed(6));
							$("#txtlong").val(results[0].geometry.location.lng().toFixed(6));
						} else {
							alert(status);
						}
					});
					return false;
				});
			 if ($('#hscode').val() != '' && $('#hscode').val() != null) {
				 fillslot($('#hscode').val());
			 }
			 else {
				 fillHQID(divcode);
			 }
		 });
        $('#btnsave').on('click', function () {
            var hqcode = $('#hscode').val();
            var hqname = $('#txtsname').val();
            var firstChar = $('#txtsname').val().substr(0, 1);  
            if (hqname == '' || hqname == null) {
                alert('Enter the HQ name');
                return false;
            }
            else if ($('#txtsname').val().match('^[0-9 ]+$')) {
                alert("Enter the valid HQ name");
                return false;
            }
            else if (!($('#txtsname').val().match('^[A-Za-z0-9 _.-]+$')) || (firstChar == ' ')) {
                alert('Enter the valid HQ name');
                return false;
            }
            var htyp = $('#ddltyp :selected').val();
            var hqtyp = $('#ddltyp :selected').text();
            if (htyp == 0) {
                alert('Select the HQ Type');
                return false;
            }
            var lat = $('#txtlat').val();
            var long = $('#txtlong').val();
            var latlong;
            if (lat == "" || lat == null) {
                alert('Enter the Latitude');
                return false;
            }
            else if (!($('#txtlat').val().match('^[0-9.]+$')) || (firstChar == ' ')) {
                alert("Enter the valid Latitude like 12.34567890");
                return false;
            }
            if (long == "" || long == null) {
                alert('Enter the Longitude');
                return false;
            }
            else if (!($('#txtlong').val().match('^[0-9.]+$')) || (firstChar == ' ')) {
                alert("Enter the valid Longitude like 12.34567890");
                return false;
            }
            latlong = lat + ':' + long;
            data = { "Divcode": divcode, "HQCode": hqcode, "HQName": hqname, "HQtyp": hqtyp, "Htyp": htyp, "LatLong": latlong }
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "HQ_Creation.aspx/savehq",
                data: "{'data':'" + JSON.stringify(data) + "'}",
                dataType: "json",
                success: function (data) {
                     if (data.d == "Exist") {
                        //alert("Already Exist this HQ with same Type...");
                        alert("Updated Successfully...");
                    }
                    else {
                        alert(data.d);
                    }
                    window.location.href = "SalesForce_HQ_Creation.aspx";
                },
                error: function (data) {
                    alert(JSON.stringify(data));
                }
            });
        });
    </script>
</asp:Content>

