<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rptSFWiseCalls.aspx.cs" Inherits="MIS_Reports_rptSFWiseCalls" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../css/bootstrap-3.3.2.min.css" rel="stylesheet" />
    <link type="text/css" rel="stylesheet" href="../css/style.css" />
    <style type="text/css">
    </style>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            var SFCode = $("#<%=HSFCode.ClientID%>").val();
            var SubDiv = $("#<%=HSubDive.ClientID%>").val();
            var Fdate = $("#<%=HFDate.ClientID%>").val();


            var SFData = [];
            var TCData = [];
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "rptSFWiseCalls.aspx/GetSalesforce",
                data: "{'sfCode':'" + SFCode + "', 'SubDiv':'" + SubDiv + "'}",
                dataType: "json",
                success: function (data) {
                    SFData = data.d;
                },
                error: function (jqXHR, exception) {
                    console.log(jqXHR);
                    console.log(exception);
                    var msg = '';
                    if (jqXHR.status === 0) {
                        msg = 'Not connect.\n Verify Network.';
                    } else if (jqXHR.status == 404) {
                        msg = 'Requested page not found. [404]';
                    } else if (jqXHR.status == 500) {
                        msg = 'Internal Server Error [500].';
                    } else if (exception === 'parsererror') {
                        msg = 'Requested JSON parse failed.';
                    } else if (exception === 'timeout') {
                        msg = 'Time out error.';
                    } else if (exception === 'abort') {
                        msg = 'Ajax request aborted.';
                    } else {
                        msg = 'Uncaught Error.\n' + jqXHR.responseText;
                    }
                    alert(msg);
                }
            });


            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "rptSFWiseCalls.aspx/GetTCData",
                data: "{'sfCode':'" + SFCode + "', 'SubDiv':'" + SubDiv + "', 'FDates':'" + Fdate + "'}",
                dataType: "json",
                success: function (data) {
                    TCData = data.d;
                     console.log(TCData);
                },
                error: function (jqXHR, exception) {
                    console.log(jqXHR);
                    console.log(exception);
                    var msg = '';
                    if (jqXHR.status === 0) {
                        msg = 'Not connect.\n Verify Network.';
                    } else if (jqXHR.status == 404) {
                        msg = 'Requested page not found. [404]';
                    } else if (jqXHR.status == 500) {
                        msg = 'Internal Server Error [500].';
                    } else if (exception === 'parsererror') {
                        msg = 'Requested JSON parse failed.';
                    } else if (exception === 'timeout') {
                        msg = 'Time out error.';
                    } else if (exception === 'abort') {
                        msg = 'Ajax request aborted.';
                    } else {
                        msg = 'Uncaught Error.\n' + jqXHR.responseText;
                    }
                    alert(msg);
                }
            });


            var hsT = $("#<%=HsTime.ClientID%>").val();
            var heT = $("#<%=HeTime.ClientID%>").val();
            var htS = $("#<%=HtSlot.ClientID%>").val();
            //if (Number(heT) == 24) {
            heT = heT-1;
            //}
            var hrs = Number(hsT) <= 9 ? '0' + hsT : hsT;
            var hre = Number(heT) <= 9 ? '0' + heT : heT;

            
            

            if (SFData.length > 0) {
                var tb = $('#sfTable');
                var str = '<th style="min-width: 250px">Name</th>';

                var time = new Date('2019/01/09 ' + hrs + ':00:00');
                var etime = new Date('2019/01/09 ' + hre + ':59:00');
                console.log(time);
                console.log(etime);
                var startTime = time.getHours() + ':' + time.getMinutes();
                var endTime = etime.getHours() + ':' + etime.getMinutes();
                var timeChange = htS; //50 minutes
                while (time.getTime() < etime.getTime()) {
                    var startHour = startTime.split(':')[0];
                    var startMin = startTime.split(':')[1];

                    var ct = time;
                    str += '<th style="min-width: 75px">' + getFormattedTime(time) + '-';



                    time.setMinutes(time.getMinutes() + timeChange);
                    startTime = getFormattedTime(time);
                    if (startTime == "0:00 ") {
                        str += ' 24:00' + '</th>';
                    }
                    else {
                        str += ' ' + startTime + '</th>';
                    }
                    time.setMinutes(time.getMinutes() + 1);
                    var timeChange = htS - 1; //50 minutes

                }
                str += '<th style="min-width: 75px">Total</th>';


                $(tb).append('<tr>' + str + '</tr>');

                for (var i = 0; i < SFData.length; i++) {
                    tot = 0;
                    str = '<td>' + SFData[i].sf_Name + '</td>';
                    var time = new Date('2019/01/09 ' + hrs + ':00:00');
                    var etime = new Date('2019/01/09 ' + hre + ':59:00');
                    var startTime = time.getHours() + ':' + time.getMinutes();
                    var endTime = etime.getHours() + ':' + etime.getMinutes();
                    var timeChange = htS; //50 minutes
                    while (time.getTime() < etime.getTime()) {
                        var startHour = startTime.split(':')[0];
                        var startMin = startTime.split(':')[1];

                        ctime = time.getMinutes();
                        chr = time.getHours();

                        time.setMinutes(time.getMinutes() + timeChange);
                        startTime = getFormattedTime(time);

                        cD = TCData.filter(function (a) {
                            var ss = '2019/01/09 ' + a.Timestamp;
                            var kk = new Date(ss);



                            var cs = '2019/01/09 ' + chr + ':' + ctime;
                            var ck = new Date(cs);

                            return (kk.getTime() >= ck.getTime() && kk.getTime() <= time.getTime() && a.Sf_Code == SFData[i].Sf_Code)
                        });
                        console.log(cD);
                        var tC = '';
                        if (cD.length > 0) {
                            for (var l = 0; l < cD.length; l++) {
                                tC = Number(tC == '' ? 0 : tC) + Number(cD[l].TC);
                                tot += Number(cD[l].TC);
                            }
                        }

                        time.setMinutes(time.getMinutes() + 1);
                        var timeChange = htS - 1;


                        str += '<td>' + tC + '</td>';
                    }

                    str += '<td style="min-width: 75px">'+tot+'</td>';

                    $(tb).append('<tr>' + str + '</tr>');
                }


            }


            function getFormattedTime(time) {
                //  var postfix = "AM";
                var hour = time.getHours();
                var min = time.getMinutes();

                //format hours
                //if (hour > 12) {
                //    hour = hour - 12;
                //    postfix = "PM";
                //}

                //format minutes
                min = ('' + min).length > 1 ? min : '0' + min;
                return hour + ':' + min + ' ';
            }
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:HiddenField ID="HSFCode" runat="server" />
        <asp:HiddenField ID="HSubDive" runat="server" />
        <asp:HiddenField ID="HFDate" runat="server" />
        <asp:HiddenField ID="HsTime" runat="server" />
        <asp:HiddenField ID="HeTime" runat="server" />
        <asp:HiddenField ID="HtSlot" runat="server" />

        <br />
        <div class="container" style="max-width: 100%; width: 98%">
            <div class="row">
                <div class="col-md-8">
                </div>
                <div class="col-md-4" style="text-align: right">
                    <a name="btnExcel" id="btnPrint" type="button" style="padding: 0px 20px; display: none" href="#" class="btn btnPrint"></a>
                    <a name="btnExcel" id="btnPdf" type="button" style="padding: 0px 20px; display: none" href="#" class="btn btnPdf"></a>
                    <a name="btnExcel" id="btnExcel" type="button" style="padding: 0px 20px;" href="#" class="btn btnExcel"></a>
                    <a name="btnClose" id="btnClose" type="button" style="padding: 0px 20px;" href="javascript:window.open('','_self').close();" class="btn btnClose"></a>

                </div>
            </div>
        </div>

        <div class="container" id="excelDiv" style="max-width: 100%; width: 98%">
            <asp:Label runat="server" ID="lblHead" Style="font-size: x-large"></asp:Label>
            <br />
            <asp:Label runat="server" ID="lblsfname"></asp:Label>
            <table id="sfTable" class="newStly" runat="server">
            </table>
        </div>
    </form>
</body>
</html>
