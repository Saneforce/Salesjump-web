<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rpt_FieldForceTracking.aspx.cs" Inherits="MIS_Reports_rpt_FieldForceTracking" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../css/bootstrap-3.3.2.min.css" rel="stylesheet" />
    <link href="../css/style.css" rel="stylesheet" />
    <style type="text/css">
        #lodimg {
            position: fixed;
            top: 0;
            left: 0;
            background-color: black;
            z-index: 99;
            opacity: 0.8;
            filter: alpha(opacity=80);
            -moz-opacity: 0.8;
            min-height: 30%;
            width: 30%;
        }
    </style>
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/moment.js/2.11.1/moment.min.js"></script>
    <script type="text/javascript" src="../js/lib/leaflet.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            var bDat = [];
            var sfDate = [];
            var sNum = 1;
            var i = 0;
            function loadaddrs($tr) {
                //$('#FieldForce tbody tr').each(function () {
                    var Long = parseFloat($($tr).find('td').eq(4).text());
                    var Lat = parseFloat($($tr).find('td').eq(3).text());
                    var addrs = '';
                    //var geocodingAPI = "https://maps.googleapis.com/maps/api/geocode/json?latlng=" + emp.Lat + "," + emp.Long + "&key=AAAAP_AIzaSyAER5hPywUW-5DRlyKJZEfsqgZlaqytxoU";
                    var url = "//nominatim.openstreetmap.org/reverse?format=json&lon=" + Long + '&lat=' + Lat + "";
                    $.ajax({
                        url: url,
                        async: false,
                        dataType: 'json',
                        success: function (data) {
                            addrs = data.display_name;
                        }
                    });
                    $($tr).find('td').eq(7).text(addrs);
                    i++;
                    setTimeout(function () { loadaddrs($('#FieldForce tbody tr')[i]) }, 10);
                //});
            }
            //const myTimeout = setTimeout(loaddata, 5000);
            function loaddata() {

                if (sfDate.length > 0) {
                    var employeeTable = $('#FieldForce tbody');
                    Slno = 0;
                    $(sfDate).each(function (index, emp) {
                        var bats = '';
                        var batdt = '';
                        var d;
                        var dd;
                        d = new Date(emp.chdate);
                        d.setSeconds(d.getSeconds() - 60);
                        dd = new Date(emp.chdate);
                        dd.setSeconds(dd.getSeconds() + 60);

                        bS = bDat.filter(function (a) {
                            return (new Date(a.DateandTime) >= d && new Date(a.DateandTime) <= dd)
                        });

                        if (bS.length > 0) {
                            bats = bS[0].Battery_Status;
                            batdt = bS[0].dateDisplay;
                        }

                        //var Long = emp.Long;
                        //var Lat = emp.Lat;
                        //var addrs = '';

                        //var url = "//nominatim.openstreetmap.org/reverse?format=json&lon=" + Long + '&lat=' + Lat + "";
                        //$.ajax({
                        //    url: url,
                        //    async: false,
                        //    dataType: 'json',
                        //    success: function (data) {
                        //        addrs = data.display_name;
                        //    }
                        //});

                        //var addrs = addrs;



                        Slno += 1;
                        // employeeTable.append('<tr><td>' + emp.ROWNUMBER + '</td><td>' + emp.DtTm + '</td><td>' + emp.Lat + '</td><td>' + emp.Long + '</td><td>' + batdt + '</td><td>' + bats + '</td></tr>');
                        employeeTable.append('<tr><td>' + Slno + '</td><td>' + emp.DtTm + '</td><td>' + emp.Sf_Name + '</td><td>' + emp.Lat + '</td><td>' + emp.Long + '</td><td>' + batdt + '</td><td>' + bats + '</td></tr>');/*<td class="place"></td>*/

                    });




                }

            }

            var sfCode = $('#<%=ddlFieldForce.ClientID%>').val();
            var FDt = $('#<%=ddlFDate.ClientID%>').val();
            var divcode = '<%=Session["div_code"]%>';
            var left = (window.innerWidth / 4);
            var top = (window.innerHeight / 4);
            //console.log(left + ':' + top);
            //loading.css({ top: top, left: left });
            $('#lodimg').css({ 'display': '', top: top, left: left });
            $.ajax({
                contentType: "application/json; charset=utf-8",
                async: false,
                url: 'rpt_FieldForceTracking.aspx/GetBatteryData',
                type: "POST",
                dataType: "json",
                data: "{'SF_Code':'" + sfCode + "', 'FDate':'" + FDt + "','divcode':'" + divcode + "'}",
                success: function (data) {
                    bDat = data.d;
                    //  loaddata();
                    //  console.log(data.d);
                },
                error: function (erorr) {
                    console.log(erorr);
                }
            });

            $.ajax({
                contentType: "application/json; charset=utf-8",
                async: false,
                url: 'rpt_FieldForceTracking.aspx/GetEmployees',
                type: "POST",
                dataType: "json",
                data: "{'SF_Code':'" + sfCode + "', 'FDate':'" + FDt + "','divcode':'" + divcode + "'}",
                success: function (data) {
                    sfDate = data.d;

                    if (sfDate.length > 0) {
                        loaddata();

                        //setTimeout(function () { loaddata() }, 10);
                        $('#lodimg').css({ 'display': 'none' });
                    }
                    else {
                        $('#FieldForce tbody ').append('<tr><td colspan="6" style="color:red">No Records Not Found..!</td></tr>');
                        $('#lodimg').css({ 'display': 'none' });
                    }
                },
                error: function (err) {
                    $('#lodimg').css({ 'display': 'none' });
                    console.log(err);
                }
            });






            $(document).on('click', "#btnExcel", function (e) {


                var a = document.createElement('a');

                var fileName = 'Test file.xls';
                var blob = new Blob([$('div[id$=content]').html()], {
                    type: "application/csv;charset=utf-8;"
                });
                document.body.appendChild(a);
                a.href = URL.createObjectURL(blob);
                a.download = 'FieldForcetracking_.xls';
                a.click();
                e.preventDefault();


            });




            loadaddrs($('#FieldForce tbody tr')[0]);
            //setTimeout(function () { loadaddrs($('#FieldForce tbody tr')[0]) }, 10);
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:HiddenField ID="ddlFieldForce" runat="server" />
        <asp:HiddenField ID="ddlFDate" runat="server" />
        <asp:HiddenField ID="ddlTDate" runat="server" />
        <asp:HiddenField ID="SubDivCode" runat="server" />
        <div class="container" style="width: 90%">
            <br />
            <div class="row">
                <div class="col-md-12" style="text-align: right">
                    <a name="btnExcel" id="btnPrint" type="button" style="padding: 0px 20px; display: none"
                        href="#" class="btn btnPrint"></a><a name="btnExcel" id="btnPdf" type="button" style="padding: 0px 20px; display: none"
                            href="#" class="btn btnPdf"></a><a name="btnExcel" id="btnExcel" type="button"
                                style="padding: 0px 20px;" href="#" class="btn btnExcel"></a><a name="btnClose" id="btnClose"
                                    type="button" style="padding: 0px 20px;" href="javascript:window.open('','_self').close();"
                                    class="btn btnClose"></a>
                </div>
                <br />
                <div id="content" style="width: 100%; margin: auto;">
                    <div class="col-md-12">
                        <asp:Label ID="Label1" runat="server" Text="Field Force Tracking" Style="font-weight: bold; font-size: x-large;"></asp:Label>
                        <br />
                        <asp:Label ID="Label2" runat="server" Text="" Style="font-weight: bold; font-size: medium"></asp:Label>
                    </div>
                    <table id="FieldForce" class="newStly" border="1">
                        <thead>
                            <tr>
                                <th>SlNo.
                            </th>
                                <th>Date
                            </th>
                                <th>Sf_Name
                            </th>
                                <th>Lat
                            </th>
                                <th>Long
                            </th>
                                <th>Battery Taken on
                            </th>
                                <th>Battery Status %
                            </th>
                               <%-- <th>Place
                            </th>--%>
                            </tr>
                        </thead>
                        <tbody>
                        </tbody>
                    </table>
                    <br />
                    <br />
                    <br />
                </div>
            </div>
        </div>
        <img id="lodimg" src="../Images/loadingN.gif" style="display: none" alt="" />
    </form>
</body>
</html>
