<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TsrrptLeaveFormAll.aspx.cs" Inherits="MIS_Reports_tsr_TsrrptLeaveFormAll" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../css/bootstrap-3.3.2.min.css" rel="stylesheet" />
    <link href="../../css/style.css" rel="stylesheet" />
    <script type="text/javascript" src="https://code.jquery.com/jquery-3.3.1.js"></script>
    <script type="text/javascript">
        jQuery(document).ready(function ($) {
            var CDtls = [];
            var lDtls = [];
            genRepot = function () {
                if (CDtls.length > 0 && lDtls.length > 0) {
                    var tbl = $('#tblFF');
                    str = '<th rowspan="2" >SlNo.</th><th rowspan="2">Name</th><th rowspan="2">Emp.ID</th><th rowspan="2">Zone</th>';
                    stk = '';
                    for (var k = 0; k < lDtls.length; k++) {
                        str += '<th colspan="3">' + lDtls[k].Leave_Name + '</th>';
                        stk += '<th>Eligibility</th><th>Taken</th><th>Available</th>';
                    }
                    $(tbl).append('<tr>' + str + '</tr>');
                    $(tbl).append('<tr>' + stk + '</tr>');


                    var fil = CDtls.filter(function (el, i, x) {
                        return x.some(function (obj, j) {
                            return obj.sfCode === el.sfCode && (x = j);
                        }) && i == x;
                    });


                    for (var i = 0; i < fil.length; i++) {
                        str = '<td style="text-align: right;">' + (i + 1) + '</td><td>' + fil[i].sfName + '</td><td>' + fil[i].sfEmployeeId + '</td><td>' + fil[i].sfZone + '</td>';
                        sf = fil[i].sfCode;

                        for (var k = 0; k < lDtls.length; k++) {
                            lCode = lDtls[k].Leave_code;

                            lR = CDtls.filter(function (obj) {
                                return (obj.sfCode === sf && obj.LeaveCode === lCode)
                            });
                            if (lR.length > 0) {
                                str += '<td style="text-align: right;" >' + lR[0].LeaveValue + '</td><td style="text-align: right;">' + lR[0].LeaveTaken + '</td><td style="text-align: right;">' + lR[0].LeaveAvailability + '</td>';
                            }
                            else {
                                str += '<td></td><td></td><td></td>';
                            }
                        }
                        $(tbl).append('<tr>' + str + '</tr>');
                    }
                }
            }
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "/MIS Reports/tsr/TsrrptLeaveFormAll.aspx/getLeaveCode",

                dataType: "json",
                success: function (data) {
                    lDtls = data.d;
                    genRepot();
                },
                error: function (rs) {
                    console.log(rs);
                }
            });
            var sfcode = $("#<%=hsfcode.ClientID%>").val();
            var fyear = $("#<%=hfyear.ClientID%>").val();
            var subdiv = $("#<%=hsubdiv.ClientID%>").val();

            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "/MIS Reports/tsr/TsrrptLeaveFormAll.aspx/GetLeaveSF",
                data: "{'SF_Code':'" + sfcode + "', 'FYear':'" + fyear + "' ,'SubDiv':'" + subdiv + "' }",
                dataType: "json",
                success: function (data) {
                    CDtls = data.d;
                    console.log(CDtls);
                    genRepot();
                },
                error: function (rs) {
                    console.log(rs);
                }
            });
        });
        function fnExcelReport() {
            var a = document.createElement('a');
            var fileName = 'Test file.xls';
            var blob = new Blob([$('div[id$=excelDiv]').html()], {
                type: "application/csv;charset=utf-8;"
            });
            document.body.appendChild(a);
            a.href = URL.createObjectURL(blob);
            a.download = 'LeaveDetails.xls';
            a.click();
            e.preventDefault();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="container" style="max-width: 100%; width: 95%; text-align: right">
        <a class="btn btnExcel" onclick="fnExcelReport()"></a><a class="btn btnClose" href="javascript:window.open('','_self').close();">
        </a>
    </div>
    <div id="excelDiv" class="container" style="max-width: 100%; width: 95%;">
        <asp:HiddenField ID="hsfcode" runat="server" />
        <asp:HiddenField ID="hfyear" runat="server" />
        <asp:HiddenField ID="hsubdiv" runat="server" />
        <asp:Label ID="lblhead" runat="server" Style="font-size: x-large"></asp:Label>
        <br />
        <asp:Label ID="subhead" runat="server"></asp:Label>
        <table id="tblFF" class="table table-responsive newStly" style="width: 100%;" border="1">
        </table>
    </div>
    </form>
</body>
</html>
