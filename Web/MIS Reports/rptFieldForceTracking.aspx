<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rptFieldForceTracking.aspx.cs"
    Inherits="MIS_Reports_rptFieldForceTracking" %>

<!DOCTYPE html>
<html xmlns="https://www.w3.org/1999/xhtml">
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
            var i = 0; var divcode = '';
            function loadaddrs($tr) {
                //$('#FieldForce tbody tr').each(function () {
                var Long = parseFloat($($tr).find('td').eq(3).text());
                var Lat = parseFloat($($tr).find('td').eq(2).text());
                var add = $($tr).find('td').eq(7).text();
                var addrs = '';
                if (add != '' && add != 'undefined') {
                    var addrs = add;
                }
                else {
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
                }

                $($tr).find('td').eq(6).text(addrs);
                i++;
                setTimeout(function () { loadaddrs($('#FieldForce tbody tr')[i]) }, 10);
                // });
                //console.log(JSON.parse(geocodingAPI));
                //emp["addrs"] = 'Under maintenance';
                /*$.getJSON(geocodingAPI, function (json) {
                if (json.status == "OK") {
                var result = json.results[0];
                emp["addrs"] = result.formatted_address;
                $x = $(".place");
                $($x[index]).text(result.formatted_address);
                }
                else {
                $x = $(".place");
                $($x[index]).text(json.status);

                }
                });*/
            }
            function loaddata() {

                if (sfDate.length > 0) {
			 $('#FieldForce thead').html('');
                    var hstr = '';
                    if (divcode == '150') {
                        hstr = '<tr><th>SlNo.</th><th>Date</th><th>Lat</th><th>Long</th><th>Battery Taken on</th><th>Battery Status %</th><th>Place</th><th style="display: none">address</th><th class="retname">Retailer Name</th></tr>';
                    }
                    else {
                        hstr = '<tr><th>SlNo.</th><th>Date</th><th>Lat</th><th>Long</th><th>Battery Taken on</th><th>Battery Status %</th><th>Place</th></tr>';
                    }
                    $('#FieldForce thead').append(hstr);
                    var employeeTable = $('#FieldForce tbody');
                    
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

                        // employeeTable.append('<tr><td>' + emp.ROWNUMBER + '</td><td>' + emp.DtTm + '</td><td>' + emp.Lat + '</td><td>' + emp.Long + '</td><td>' + batdt + '</td><td>' + bats + '</td></tr>');
                        if (divcode == '150') {

                            employeeTable.append('<tr><td>' + emp.ROWNUMBER + '</td><td>' + emp.DtTm + '</td><td>' + emp.lat + '</td><td>' + emp.lng + '</td><td>' + batdt + '</td><td>' + bats + '</td><td class="place"></td><td style="display:none">' + emp.addr + '</td><td>' + emp.rname + '</td></tr>');
                        } else {
                            $('.retname').hide();
                             employeeTable.append('<tr><td>' + emp.ROWNUMBER + '</td><td>' + emp.DtTm + '</td><td>' + emp.lat + '</td><td>' + emp.lng + '</td><td>' + batdt + '</td><td>' + bats + '</td><td class="place"></td></tr>');
                        }


                    });

                    // sNum = Number(currentPageNumber) + 1;
                    // eNum = currentPageNumber;


                }
            }

            var sfCode = $('#<%=ddlFieldForce.ClientID%>').val();
            var FDt = $('#<%=ddlFDate.ClientID%>').val();
            var TDt = $('#<%=ddlTDate.ClientID%>').val();
            var left = (window.innerWidth / 4);
            var top = (window.innerHeight / 4);
            //console.log(left + ':' + top);
            //loading.css({ top: top, left: left });
            $('#lodimg').css({ 'display': '', top: top, left: left });
            $.ajax({
                contentType: "application/json; charset=utf-8",
                async: false,
                url: 'rptFieldForceTracking.aspx/GetBatteryData',
                type: "POST",
                dataType: "json",
                data: "{'SF_Code':'" + sfCode + "', 'FDate':'" + FDt + "', 'TDate':'" + TDt + "'}",
                success: function (data) {
                    bDat = data.d;

                },
                error: function (erorr) {
                    console.log(erorr);
                }
            });


           var divcode=<%=Session["div_code"]%>;
            if (divcode == 150) {
                $.ajax({
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: 'rptFieldForceTracking.aspx/GetEmployees',
                    type: "POST",
                    dataType: "json",
                    data: "{'SF_Code':'" + sfCode + "', 'FDate':'" + FDt + "', 'TDate':'" + TDt + "'}",
                    success: function (data) {
                        sfDate = data.d;
                        //  console.log(sfDate);
                        if (sfDate.length > 0) {
                            loaddata()

                            //loadPageData(sfDate.length);
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
            } else {
                $.ajax({
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: 'rptFieldForceTracking.aspx/GetEmploydets',
                    type: "POST",
                    dataType: "json",
                    data: "{'SF_Code':'" + sfCode + "', 'FDate':'" + FDt + "', 'TDate':'" + TDt + "'}",
                    success: function (data) {
                        sfDate = data.d;
                        //  console.log(sfDate);
                        if (sfDate.length > 0) {
                            loaddata()

                            //loadPageData(sfDate.length);
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
            }
            





            //            var currentPage = 200;
            //            loadPageData(currentPage);
            //            $(window).scroll(function () {
            //                if ($(window).scrollTop() == $(document).height() - $(window).height()) {
            //                    currentPage += 200;
            //                    loadPageData(currentPage);
            //                }
            //            });

            $(document).on('click', "#btnExcel", function (e) {
                //                var dt = new Date();
                //                var day = dt.getDate();
                //                var month = dt.getMonth() + 1;
                //                var year = dt.getFullYear();
                //                var postfix = day + "_" + month + "_" + year;
                //                //creating a temporary HTML link element (they support setting file names)
                //                var a = document.createElement('a');
                //                document.body.appendChild(a);
                //                //getting data from our div that contains the HTML table
                //                var data_type = 'data:application/vnd.ms-excel';
                //                var table_div = document.getElementById('content');
                //                var table_html = table_div.outerHTML.replace(/ /g, '%20');
                //                a.href = data_type + ', ' + table_html;
                //                //setting the file name

                //                a.download = 'FieldForcetracking_' + postfix + '.xls';
                //                //triggering the function
                //                a.click();
                //                //just in case, prevent default behaviour
                //                e.preventDefault();


                //                var a = document.createElement('a');                
                //                var data_type = 'data:application/vnd.ms-excel';
                //                a.href = data_type + ', ' + encodeURIComponent($('div[id$=content]').html());
                //                document.body.appendChild(a);
                //                a.download = 'FieldForcetracking_.xls';
                //                a.click();
                //                e.preventDefault();

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


            //            function loadPageData(currentPageNumber) {
            //                
            //                if (sfDate.length > 0) {
            //                    var employeeTable = $('#FieldForce tbody');
            //                    dts = sfDate.filter(function (a) {
            //                        return (a.ROWNUMBER >= sNum && a.ROWNUMBER <= currentPageNumber)
            //                    });



            // }


            setTimeout(function () { loadaddrs($('#FieldForce tbody tr')[0]) }, 10);
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
