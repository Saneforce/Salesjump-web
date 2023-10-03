<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Rpt_Daywise_My_Day_Plan_View.aspx.cs" Inherits="MasterFiles_Reports_Rpt_Daywise_My_Day_Plan_View" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link type="text/css" rel="Stylesheet" href="../../css/Report.css" />
    <link href="../css/bootstrap-3.3.2.min.css" rel="stylesheet" />
    <link href="../../css/style.css" rel="stylesheet" />
    <script src="//ajax.googleapis.com/ajax/libs/jquery/1.9.1/jquery.min.js"></script>
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css">
    <script type="text/javascript">

        $(document).ready(function () {
            var indx = 1;
            var i = 0;
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "Rpt_Daywise_My_Day_Plan_View.aspx/getplandets",
                data: "{'Div':'<%=Session["div_code"]%>'}",
                dataType: "json",
                success: function (data) {
                    var TPdets = JSON.parse(data.d) || [];
                    if (TPdets.length > 0) {
                        var slno = 0; str = '';
                        $('#report TBODY').html("");
                        for (var i = 0; i < TPdets.length; i++) {
                            slno = i + 1;
                            var tic = (TPdets[i].Login != '') ? "<i class='fa fa-check' style='font-size:31px;margin-left: 13px;;color:green'></i>" : "<i class='fa fa-close'style='font-size:31px;margin-left: 13px;color:red'></i>";
                            var order = (TPdets[i].order_status != 0) ? "<i class='fa fa-check' style='font-size:31px;margin-left: 13px;color:green'></i>" : "<i class='fa fa-close' style='font-size:31px;margin-left: 13px;color:red'></i>";
                            if (TPdets[i].Activity_Date != '') {
                                if (TPdets[i].FWFlg != 'L' && TPdets[i].FWFlg != 'H' && TPdets[i].FWFlg != 'W') {
                                    str += "<tr><td>" + slno + "</td><td>" + TPdets[i].Activity + "</td><td>" + TPdets[i].DayOfWeek + "</td><td>" + TPdets[i].Employee_Id + "</td><td>" + TPdets[i].Field_Force_Name + "</td><td>" + TPdets[i].Zone + "</td><td>" + TPdets[i].StateName + "</td>" +
                                        "<td>" + TPdets[i].Division_Name + "</td><td>" + TPdets[i].Reporting_Manager + "</td><td>" + TPdets[i].Mobile_No + "</td>" +
                                        "<td>" + TPdets[i].WorkType_Name + "</td><td>" + TPdets[i].Route_Name + "</td><td>" + TPdets[i].Remarks + "</td><td>" + TPdets[i].Joint_Work_With + "</td><td>" + TPdets[i].Lat + "</td><td>" + TPdets[i].Long + "</td>" +
                                        "<td></td><td>" + TPdets[i].Login + "</td><td>" + TPdets[i].Log_Out + "</td><td>" + TPdets[i].DRToday_New_Retailers + "</td><td>" + TPdets[i].version + "</td><td>" + tic + "</td><td>" + order + "</td></tr > ";
                                }
                                else {
                                    str += "<tr><td>" + slno + "</td><td>" + TPdets[i].Activity + "</td><td>" + TPdets[i].DayOfWeek + "</td><td colspan=20 style='background:darksalmon;'><h2 style='text-align: center;'>" + TPdets[i].WorkType_Name + "</h2></td></tr>";
                                }
                            }
                            else if (TPdets[i].DayOfWeek == 'Sunday') {
                                str += "<tr><td>" + slno + "</td><td>" + TPdets[i].Activity + "</td><td>" + TPdets[i].DayOfWeek + "</td><td colspan=20 style='background:orange;'><h2 style='text-align: center;'>Weekly Off</h2></td></tr>";
                            }
                            else {
                                 str += "<tr><td>" + slno + "</td><td>" + TPdets[i].Activity + "</td><td>" + TPdets[i].DayOfWeek + "</td><td colspan=20 style='background:orangered;'><h2 style='text-align: center;'>Absent</h2></td></tr>";
                            }

                        }


                        $('#report TBODY').append(str);



                    }



                },
                error: function (rs) {

                }
            });


            function loadAddrss($tr) {
                var tbl = $('#report tr');
                var lat1 = $($tr).find('td')[14];
                var long1 = $($tr).find('td')[15];
                var lat2 = $(lat1).text().trim();
                var long2 = $(long1).text().trim();
                var addrs = '';

                //var geocodingAPI = "https://maps.googleapis.com/maps/api/geocode/json?latlng=" + lat2 + "," + long2 + "&key=sdvAAP_AIzaSyAER5hPywUW-5DRlyKJZEfsqgZlaqytxoU";
                var url = "//nominatim.openstreetmap.org/reverse?format=json&lon=" + parseFloat(long2) + '&lat=' + parseFloat(lat2) + "";
                $.ajax({
                    url: url,
                    async: false,
                    dataType: 'json',
                    success: function (data) {
                        addrs = data.display_name;
                    }
                });
                $($($tr).find('td')[16]).text(addrs); i++;
                setTimeout(function () { loadAddrss($('#report tr')[i]) }, 25); //loadAddrss(); 

            }
             setTimeout(function () { loadAddrss($('#report tr')[0]) }, 25);

        });
        function btnExcel_Click() {
            var htmls = "";
            var uri = 'data:application/vnd.ms-excel;base64,';
            var template = '<html xmlns:o="urn:schemas-microsoft-com:office:office" xmlns:x="urn:schemas-microsoft-com:office:excel" xmlns="http://www.w3.org/TR/REC-html40"><head><!--[if gte mso 9]><xml><x:ExcelWorkbook><x:ExcelWorksheets><x:ExcelWorksheet><x:Name>{worksheet}</x:Name><x:WorksheetOptions><x:DisplayGridlines/></x:WorksheetOptions></x:ExcelWorksheet></x:ExcelWorksheets></x:ExcelWorkbook></xml><![endif]--></head><body><table>{table}</table></body></html>';
            var base64 = function (s) {
                return window.btoa(unescape(encodeURIComponent(s)))
            };
            var format = function (s, c) {
                return s.replace(/{(\w+)}/g, function (m, p) {
                    return c[p];
                })
            };

            $("tr").each(function () {
                if (this.innerText === '') {
                    this.closest('tr').remove();
                }
            });
            htmls = document.getElementById("report").innerHTML;
            var ctx = {
                worksheet: 'Daywise plan report',
                table: htmls
            }
            var link = document.createElement("a");
            var tets = 'Daywise plan report' + '.xls';

            link.download = tets;
            link.href = uri + base64(format(template, ctx));
            link.click();
        }


    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Panel ID="pnlbutton" runat="server">
                <br />
                <table width="95%">
                    <tr>
                        <td width="70%"></td>
                        <td width="30%" align="right">
                            <table>
                                <tr>
                                    <img style="cursor: pointer; float: right;" src="/img/excel.png" alt="" width="40" height="40" id="btnExport" onclick="btnExcel_Click()" />

                                    <td style="padding-left: 5px">
                                        <asp:LinkButton ID="btnClose" runat="server" href="javascript:window.open('','_self').close();"
                                            class="btn btnClose" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <div class="row">
                        <div class="col-sm-8">
                            <asp:Label ID="Label1" runat="server" Text="Brandwise Sales" Style="margin-left: 500px; font-size: x-large"></asp:Label>

                        </div>

                    </div>
                    <div class="row" style="margin: 6px 0px 0px 11px;">
                        <asp:Label ID="Label2" Text="Field Force Name :" runat="server" Style="font-size: larger"></asp:Label>
                        <asp:Label ID="lblsf_name" runat="server" Style="font-size: larger"></asp:Label>
                    </div>
                </table>
            </asp:Panel>
            <div>

                <table id="report" class="newStly">
                    <thead>
                        <th>S.No</th>
                        <th>Activity Date</th>
                        <th>Day</th>
                        <th>Employee Id</th>
                        <th>Field Force Name</th>
                        <th>Zone</th>
                        <th>State Name</th>
                        <th>Division Name</th>
                        <th>Reporting Manager</th>
                        <th>Mobile No</th>
                        <th>WorkType Name</th>
                        <th>Route Name	</th>
                        <th>Remarks	</th>
                        <th>Joint Work With</th>
                        <th>Lat</th>
                        <th>Long</th>
                        <th>Address	</th>
                        <th>Login</th>
                        <th>Log Out</th>
                        <th>Today New Retailers</th>
                        <th>App Version	</th>
                        <th>Attendance </th>
                        <th>Order</th>

                    </thead>
                    <tbody>
                    </tbody>


                </table>
            </div>
        </div>
    </form>
</body>
</html>
