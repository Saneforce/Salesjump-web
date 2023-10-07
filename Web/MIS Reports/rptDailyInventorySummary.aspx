<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rptDailyInventorySummary.aspx.cs" Inherits="MIS_Reports_rptDailyInventorySummary" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../css/bootstrap-3.3.2.min.css" rel="stylesheet" />
    <link href="../css/style.css" rel="stylesheet" />
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {

            var pDtl = [];
            var sfD = [];
            var Dtls = [];

            var sfCode = $('#<%=hsfCode.ClientID%>').val();
            var subDiv = $('#<%=hSubDiv.ClientID%>').val();
            var fDate = $('#<%=hFDate.ClientID%>').val();
            var tDate = $('#<%=hTDate.ClientID%>').val();

            function genReport() {
                if (Dtls.length > 0 && pDtl.length > 0 && sfD.length > 0) {
                    var tbl = $('#invTable');
                    $(tbl).find('tr').remove();

                    strHead = '<th rowspan="2">SlNo.</th><th style="min-width: 250px;" rowspan="2">Field Force Name</th>';
                    strHead2 = '';
                    for (var p = 0; p < pDtl.length; p++) {
                        strHead += '<th style="min-width: 75px;" colspan="3">' + pDtl[p].product_name + '</th>';
                        strHead2 += '<th style="min-width: 30px;">VAN</th><th style="min-width: 30px;">SAL</th><th style="min-width: 30px;">BAL</th>';
                    }
                    $(tbl).append('<tr>' + strHead + '<th colspan="3">Total</th></tr>');
                    $(tbl).append('<tr>' + strHead2 + '<th>VAN TOT</th><th>SAL TOT</th><th>BAL TOT</th></tr>');
                    strBody = '';
                    var tarr = [];
                    var starr = [];
                    for (var i = 0; i < sfD.length; i++) {
                        strBody = '<td>' + (i + 1) + '</td><td>' + sfD[i].sf_name + '</td>';
                        var sum = 0;
                        var ssum = 0;
                        for (var j = 0; j < pDtl.length; j++) {
                            dt = Dtls.filter(function (obj) {
                                return (obj.sfCode == sfD[i].sf_code && obj.pCode == pDtl[j].product_id)
                            });
                            qty = '';
                            if (dt.length > 0) {
                                qty = dt[0].OrderQty;
                                sum = Number(sum) + Number(dt[0].OrderQty);
                            }

                            dt1 = salDt.filter(function (obj) {
                                return (obj.sfCode == sfD[i].sf_code && obj.pCode == pDtl[j].product_id)
                            });

                            sqty = '';
                            if (dt1.length > 0) {
                                sqty = dt1[0].OrderQty;
                                ssum = Number(ssum) + Number(dt1[0].OrderQty);
                            }

                            tarr[j] = Number(tarr[j] || 0) + Number(qty || 0);
                            starr[j] = Number(starr[j] || 0) + Number(sqty || 0);
                            strBody += '<td>' + qty + '</td><td>' + sqty + '</td><td>' + Math.abs(Number(qty) - Number(sqty)) + '</td>';
                        }
                        $(tbl).append('<tr>' + strBody + '<td>' + sum + '</td><td>' + ssum + '</td><td>' + Math.abs(Number(sum) - Number(ssum)) + '</td></tr>');
                    }
                    strTot = '<td colspan="2">Total</td>';
                    tSum = 0;
                    tsSum = 0;
                    for (var k = 0; k < tarr.length; k++) {
                        strTot += '<td>' + tarr[k] + '</td><td>' + starr[k] + '</td><td>' + Math.abs(Number(tarr[k]) - Number(starr[k])) + '</td>';
                        tSum = Number(tSum) + Number(tarr[k]);
                        tsSum = Number(tsSum) + Number(starr[k]);
                    }
                    $(tbl).append('<tr style="background-color: #496a9a; color:#fff; ">' + strTot + '<td>' + tSum + '</td><td>' + tsSum + '</td><td>' + Math.abs(Number(tSum) - Number(tsSum)) + '</td></tr>');
                }
            }

            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "rptDailyInventorySummary.aspx/getfo",
                data: "{'sfCode':'" + sfCode + "','SubDiv':'" + subDiv + "'}",
                dataType: "json",
                success: function (data) {
                    sfD = data.d;
                    genReport();
                    //  console.log(data.d);
                },
                error: function (result) {
                    console.log(result);
                    //alert(JSON.stringify(result));
                }
            });

            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "rptDailyInventorySummary.aspx/getProduct",
                data: "{'SubDiv':'" + subDiv + "'}",
                dataType: "json",
                success: function (data) {
                    pDtl = data.d;
                    genReport();
                    //console.log(data.d);
                },
                error: function (result) {
                    console.log(result);
                    //alert(JSON.stringify(result));
                }
            });
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "rptDailyInventorySummary.aspx/getInventorySummary",
                data: "{'sfCode':'" + sfCode + "','SubDiv':'" + subDiv + "','fDate':'" + fDate + "','tDate':'" + tDate + "'}",
                dataType: "json",
                success: function (data) {

                    obj = JSON.parse(data.d);
                    console.log(obj.van);
                    console.log(obj.sal);

                    Dtls = obj.van;
                    salDt = obj.sal;
                    if (Dtls.length > 0) {
                        $('.visb').css('visibility', 'hidden');
                        genReport();
                    }
                    else {
                        $('.visb').css('visibility', 'visible');
                    }
                    //console.log(data.d);
                },
                error: function (result) {
                    console.log(result);
                    //alert(JSON.stringify(result));
                }
            });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:HiddenField ID="hsfCode" runat="server" />
        <asp:HiddenField ID="hSubDiv" runat="server" />
        <asp:HiddenField ID="hFDate" runat="server" />
        <asp:HiddenField ID="hTDate" runat="server" />
        <br />
        <div class="container" style="max-width: 100%; width: 95%; text-align: right; padding-right: 50px">
            <asp:LinkButton ID="btnPrint" runat="Server" Style="padding: 0px 20px; display: none" class="btn btnPrint" />
            <asp:LinkButton ID="btnExport" runat="Server" Style="padding: 0px 20px; display: none" class="btn btnPdf" />
            <asp:LinkButton ID="btnExcel" runat="Server" Style="padding: 0px 20px;" class="btn btnExcel" />
            <a name="btnClose" id="btnClose" type="button" style="padding: 0px 20px;" href="javascript:window.open('','_self').close();" class="btn btnClose"></a>
        </div>
        <div id="excelDiv" class="container" style="max-width: 100%; width: 95%;">
            <div>
                <asp:Label ID="lblHead" runat="server" Style="font-size: x-large;"></asp:Label>
                <br />
                <asp:Label ID="lblsfname" runat="server"></asp:Label>
                <br />
                <asp:Label ID="Label1" CssClass="visb" Style="color: red; font-size: x-large;" runat="server" Text="No Recods Found..!"></asp:Label>
            </div>
            <table id="invTable" class="table table-bordered table-responsive newStly"></table>
        </div>
    </form>
</body>
</html>
