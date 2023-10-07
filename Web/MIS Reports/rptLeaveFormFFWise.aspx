<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rptLeaveFormFFWise.aspx.cs"
    Inherits="MIS_Reports_rptLeaveFormFFWise" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../css/bootstrap-3.3.2.min.css" rel="stylesheet" />
    <link href="../css/style.css" rel="stylesheet" />
    <script type="text/javascript" src="https://code.jquery.com/jquery-3.3.1.js"></script>
    <script type="text/javascript">
        jQuery(document).ready(function ($) {

            var sf = $('#<%=hsfCode.ClientID%>').val();
            var fYear = $('#<%=hfYear.ClientID%>').val();

            var lCode = [];
            var ldtls = [];
            var all = [];

            genRpt = function () {
                var tbl = $('#LeaveDetails');
                if (lCode.length > 0) {
                    for (var i = 0; i < lCode.length; i++) {
                        $(tbl).find('tbody').append('<tr  ><th  colspan="8" style="background-color: #19a4c6;">' + lCode[i].Leave_Name + '</th></tr>');
                        LL = all.filter(function (obj) {
                            return (obj.LeaveCode == lCode[i].LeaveCode);
                        });
                        if (LL.length > 0) {
                            for (var k = 0; k < LL.length; k++) {
                                var Remark = '';
                                if (LL[k].Remark == "Approve") {
                                    Remark = 'Approved';
                                }
                                else if (LL[k].Remark == "Reject") {
                                    Remark = 'Rejected';
                                }
                                else {
                                    Remark = LL[k].Remark;
                                }
                                str = '<td>' + LL[k].Created_Date + '</td><td>' + LL[k].From_Date + '</td><td>' + LL[k].To_Date + '</td><td>' + LL[k].Reason + '</td><td>' + LL[k].No_of_Days + '</td><td>' + LL[k].Sf_Name + '</td><td>' + LL[k].LastUpdt_Date + '</td><td>' + Remark + '</td>';
                                $(tbl).find('tbody').append('<tr>' + str + '</tr>');
                            }
                        }
                        else {
                            $(tbl).find('tbody').append('<tr><td colspan="8"  style="color:red">No Records Found..!</td></tr>');
                        }
                    }
                }
                else {
                    $(tbl).find('tbody').append('<tr><td colspan="8" style="color:red">No Record Found..!</td></tr>');
                }
            }

            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "rptLeaveFormFFWise.aspx/GetleaveDetails",
                data: "{'SF_Code':'" + sf + "','FYear':'" + fYear + "' }",
                dataType: "json",
                success: function (data) {
                    all = data.d;
                    console.log(all);
                },
                error: function (rs) {
                    console.log(rs);
                }
            });


            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "rptLeaveFormFFWise.aspx/GetFieldForce",
                data: "{'SF_Code':'" + sf + "' }",
                dataType: "json",
                success: function (data) {
                    lDtls = data.d;
                    var tbl = $('#FFDetails');
                    if (lDtls.length > 0) {
                        str = '<tr><td>Employee Name</td><td>' + lDtls[0].sfName + '</td></tr>'
                        str += '<tr ><td>Employee Code</td><td>' + lDtls[0].empCode + '</td></tr>'
                        str += '<tr ><td>Date of Join</td><td>' + lDtls[0].doj + '</td></tr>'
                        str += '<tr ><td>Division Name</td><td>' + lDtls[0].DivName + '</td></tr>'
                        str += '<tr ><td>HQ</td><td>' + lDtls[0].hq + '</td></tr>'
                        str += '<tr><td>Designation</td><td>' + lDtls[0].desig + '</td></tr>'
                        str += '<tr ><td>User Status</td><td>' + lDtls[0].status + '</td></tr>'
                        str += '<tr><td>Date of Confirm</td><td>' + lDtls[0].doc + '</td></tr>'
                        $(tbl).find('tbody').append(str);
                    }
                    else {
                        $(tbl).find('tbody').append('<tr><td colspan="2" style="color:red">No Record Found..!</td></tr>');
                    }
                },
                error: function (rs) {
                    console.log(rs);
                }
            });

            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "rptLeaveFormFFWise.aspx/GetleaveValues",
                data: "{'SF_Code':'" + sf + "','FYear':'" + fYear + "' }",
                dataType: "json",
                success: function (data) {
                    console.log(data.d);
                    lDtls = data.d;
                    lCode = lDtls;
                    var tbl = $('#LeaveAvailable');
                    if (lDtls.length > 0) {
                        var L = 0, tL = 0, aL = 0;
                        for (var i = 0; i < lDtls.length; i++) {
                            L += Number(lDtls[i].LeaveValue);
                            tL += Number(lDtls[i].LeaveTaken);
                            aL += Number(lDtls[i].LeaveAvailability);

                            str = '<td>' + lDtls[i].Leave_Name + '</td><td style="text-align:right">' + lDtls[i].LeaveValue + '</td><td style="text-align:right">' + lDtls[i].LeaveTaken + '</td><td style="text-align:right">' + lDtls[i].LeaveAvailability + '</td>'
                            $(tbl).find('tbody').append('<tr>' + str + '</tr>');
                        }
                        str = '<th>Total</th><th style="text-align:right">' + L + '</th><th style="text-align:right">' + tL + '</th><th style="text-align:right">' + aL + '</th>';
                        $(tbl).find('tbody').append('<tr>' + str + '</tr>');
                    }
                    else {
                        $(tbl).find('tbody').append('<tr><td colspan="4" style="color:red">No Record Found..!</td></tr>');
                    }
                    genRpt();

                },
                error: function (rs) {
                    console.log(rs);
                }
            });


            $(document).on('click', "#btnExcel", function (e) {
                var dt = new Date();
                var day = dt.getDate();
                var month = dt.getMonth() + 1;
                var year = dt.getFullYear();
                var postfix = day + "_" + month + "_" + year;
                var a = document.createElement('a');
                var blob = new Blob([$('div[id$=dtls]').html()], {
                    type: "application/csv;charset=utf-8;"
                });
                document.body.appendChild(a);
                a.href = URL.createObjectURL(blob);
                a.download = 'OrderDeatils_' + postfix + '.xls';
                a.click();
                e.preventDefault();
            });

            $(document).on('click', '#btnPrint', function (e) {
                var css = '<link href="../css/style.css" rel="stylesheet" />';
                var printContent = document.getElementById("dtls");
                var WinPrint = window.open('', '', 'width=900,height=650');
                WinPrint.document.write(printContent.innerHTML);
                WinPrint.document.head.innerHTML = css;
                WinPrint.document.close();
                WinPrint.focus();
                WinPrint.print();
                WinPrint.close();
            });

        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:HiddenField ID="hsfCode" runat="server" />
    <asp:HiddenField ID="hfYear" runat="server" />
    <br />
    <div class="container " style="max-width: 100%; width: 95%">
        <div class="col-md-12" style="text-align: right">
            <a name="btnExcel" id="btnPrint" type="button" style="padding: 0px 20px;" href="#"
                class="btn btnPrint"></a><a name="btnExcel" id="btnPdf" type="button" style="padding: 0px 20px;
                    display: none" href="#" class="btn btnPdf"></a><a name="btnExcel" id="btnExcel" type="button"
                        style="padding: 0px 20px;" href="#" class="btn btnExcel"></a><a name="btnClose" id="btnClose"
                            type="button" style="padding: 0px 20px;" href="javascript:window.open('','_self').close();"
                            class="btn btnClose"></a>
        </div>
    </div>
    <div id="dtls" class="container " style="max-width: 100%; width: 95%">
        <asp:Label ID="lblhead" runat="server" Style="font-size: x-large"></asp:Label>
        <div class="row">
            <div class="col-md-5">
                <table id="FFDetails" class="newStly" border="1" style="border-collapse: collapse;">
                    <thead>
                        <tr style="background: #496a9a; color: #fff;">
                            <th colspan="2">
                                Employee Details
                            </th>
                        </tr>
                    </thead>
                    <tbody>
                    </tbody>
                </table>
                <br />
                <table id="LeaveAvailable" class="newStly" border="1" style="border-collapse: collapse;">
                    <thead>
                        <tr style="background: #496a9a; color: #fff">
                            <th>
                                Leave Name
                            </th>
                            <th>
                                Total Leave
                            </th>
                            <th>
                                Taken
                            </th>
                            <th>
                                Availability
                            </th>
                        </tr>
                    </thead>
                    <tbody>
                    </tbody>
                </table>
            </div>
            <br />
            <div class="col-md-7">
                <table id="LeaveDetails" class="newStly" border="1" style="border-collapse: collapse;">
                    <thead>
                        <tr style="background: #496a9a; color: #fff">
                            <th>
                                Applied Date
                            </th>
                            <th>
                                From Date
                            </th>
                            <th>
                                To Date
                            </th>
                            <th>
                                Reason
                            </th>
                            <th>
                                No. of Day(s)
                            </th>
                            <th>
                                Approved By
                            </th>
                            <th>
                                Approved date
                            </th>
                             <th>
                                Status
                            </th>
                        </tr>
                    </thead>
                    <tbody>
                    </tbody>
                </table>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
