<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rpt_primaryPDTwise_FO_Target.aspx.cs" Inherits="MIS_Reports_rpt_primaryPDTwise_FO_Target" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../css/bootstrap-3.3.2.min.css" rel="stylesheet" />
    <link href="../css/style.css" rel="stylesheet" />
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            var sfCode = $('#<%=hidn_sf_code.ClientID%>').val();
            var fYear = $('#<%=hFYear.ClientID%>').val();
            var fMonth = $('#<%=hFMonth.ClientID%>').val();
            var tYear = $('#<%=hTYear.ClientID%>').val();
            var tMonth = $('#<%=hTMonth.ClientID%>').val();
            var SubDiv = $('#<%=subDiv.ClientID%>').val();


            var month = new Array();
            month[0] = "Jan";
            month[1] = "Feb";
            month[2] = "Mar";
            month[3] = "Apr";
            month[4] = "May";
            month[5] = "Jun";
            month[6] = "Jul";
            month[7] = "Aug";
            month[8] = "Sep";
            month[9] = "Oct";
            month[10] = "Nov";
            month[11] = "Dec";

            var pDs = [];
            var Dtls = [];

            genReport = function () {
                if (pDs.length > 0) {
                    var tbl = $('[id*=cTable]');
                    $(tbl).find('thead').find('tr').remove();
                    $(tbl).find('tbody').find('tr').remove();

                    var len = 1;
                    var fd = new Date(fYear + '/' + fMonth + '/' + "01/");
                    var td = new Date(tYear + '/' + tMonth + '/' + "01/");
                    cd = fd;
                    var str = '<th rowspan="3"   >Product Name</th>';
                    var st2 = '';
                    var stkDt = '';
                    var st3 = '<th  style="text-align: left;">No.of Products </th>';
                    var st4 = '<th  style="text-align: left;">Total </th>';

                    while (cd <= td) {
                        str += '<th colspan="6"> ' + month[cd.getMonth()] + ' - ' + cd.getFullYear() + ' </th>';
                        st2 += '<th colspan="3">Target</th><th colspan="3">Sales</th>';
                        stkDt += '<th>Case QTY</th><th>Piece QTY</th><th>VALUE</th><th>Case QTY</th><th>Piece QTY</th><th>VALUE</th>';
                        st3 += '<th class="ogTc" colspan="3"></th><th class="ogSc" colspan="3"></th>';
                        st4 += '<th class="ogTQ"></th><th class="ogTPQ"></th><th class="ogT"></th><th class="ogSQ"></th><th class="ogSPQ"></th><th class="ogS"></th>';
                        cd.setMonth(cd.getMonth() + 1);
                        len++;
                    }
                    $(tbl).find('thead').append('<tr style="background-color: #496a9a; color: white;">' + str + '<th colspan="6">Total</th></tr>');
                    $(tbl).find('thead').append('<tr style="background-color: #496a9a; color: white;">' + st2 + '<th colspan="3">Target</th><th colspan="3">Sales</th></tr>');
                    $(tbl).find('thead').append('<tr style="background-color: #496a9a; color: white;">' + stkDt + '<th>Case QTY</th><th>Piece QTY</th><th>VALUE</th><th>Case QTY</th><th><Piece QTY</th><th>VALUE</th></tr>');
                    $(tbl).find('thead').append('<tr style="background-color: #496a9a; color: white;">' + st3 + '<th class="ogTcT" colspan="3"></th><th class="ogScT" colspan="3"></th></tr>');
                    $(tbl).find('thead').append('<tr style="background-color: #496a9a; color: white;">' + st4 + '<th class="ogTTQ"></th><th class="ogTTPQ"></th><th class="ogTT"></th><th class="ogSTQ"></th><th class="ogSTPQ"></th><th class="ogST"></th></tr>');

                    var ogcTTot = [];
                    var ogcSTot = [];

                    var ogVTTot = [];
                    var ogVPTTot = [];
                    var ogVSTot = [];
                    var ogVPSTot = [];

                    //var ogcTTotQ = [];
                    //var ogcSTotQ = [];

                    var ogVTTotQ = [];
                    var ogVSTotQ = [];

                    for (var i = 0; i < pDs.length; i++) {
                        var str = '<td>' + pDs[i].product_name + '</td> ';
                        md = new Date(fYear + '/' + fMonth + '/' + "01/");
                        var j = 0;
                        TT = 0;
                        TS = 0;
                        TTQ = 0;
                        TTPQ = 0;
                        TSQ = 0;
                        TSPQ = 0;
						var tst = 0;
                        while (md <= td) {
                            cR = Dtls.filter(function (obj) {
                                return (Number(obj.cmonth) === Number((md.getMonth() + 1)) && Number(obj.cyear) === Number(md.getFullYear()) && obj.pCode === pDs[i].product_id);
                            });
                            var T = '';
                            var S = '';
                            var TQ = '';
                            var SQ = '';
                            var TPQ = '';
                            var SPQ = '';
                            if (cR.length > 0) {
							tst = 1;
                                T = cR[0].target;
                                S = cR[0].orderVal;
                                TQ = cR[0].Ctarqnty;
                                TPQ = cR[0].Ptarqnty;
                                SQ = cR[0].CQty;
                                SPQ = cR[0].PQty;


                                if (Number(T) > 0) {
                                    num = Number(ogcTTot[j] || 0);
                                    ogcTTot[j] = Number(num + 1);
                                }
                                else {
                                    num = Number(ogcTTot[j] || 0);
                                    ogcTTot[j] = Number(num + 0);
                                }
                                if (Number(S) > 0) {
                                    num = Number(ogcSTot[j] || 0);
                                    ogcSTot[j] = Number(num + 1);
                                }
                                else {
                                    num = Number(ogcSTot[j] || 0);
                                    ogcSTot[j] = Number(num + 0);
                                }
                            }
							else {
                                T = '0';
                                S = '0';
                                TQ = '0';
                                TPQ = '0';
                                SQ = '0';
                                SPQ = '0';
                                if (Number(T) > 0) {
                                    num = Number(ogcTTot[j] || 0);
                                    ogcTTot[j] = Number(num + 1);
                                }
                                else {
                                    num = Number(ogcTTot[j] || 0);
                                    ogcTTot[j] = Number(num + 0);
                                }
                                if (Number(S) > 0) {
                                    num = Number(ogcSTot[j] || 0);
                                    ogcSTot[j] = Number(num + 1);
                                }
                                else {
                                    num = Number(ogcSTot[j] || 0);
                                    ogcSTot[j] = Number(num + 0);
                                }
                            }

                            TT += Number(T || 0);
                            TS += Number(S || 0);
                            TTQ += Number(TQ || 0);
                            TSQ += Number(SQ || 0);
                            TTPQ += Number(TPQ || 0);
                            TSPQ += Number(SPQ || 0);
                            ogVTTot[j] = Number((ogVTTot[j] || 0) + Number(T || 0));
                            ogVPTTot[j] = Number((ogVPTTot[j] || 0) + Number(TPQ || 0));
                            ogVSTot[j] = Number((ogVSTot[j] || 0) + Number(S || 0));
                            ogVPSTot[j] = Number((ogVPSTot[j] || 0) + Number(SPQ || 0));
                            ogVTTotQ[j] = Number((ogVTTotQ[j] || 0) + Number(TQ || 0));
                            ogVSTotQ[j] = Number((ogVSTotQ[j] || 0) + Number(SQ || 0));



                            if (md.getMonth() % 2 == 0) {
                                str += '<td  style="background-color: #b1adae;">' + TQ + '</td><td  style="background-color: #b1adae;">' + TPQ + '</td><td  style="background-color: #b1adae;">' + T + '</td><td style="background-color: #b1adae;">' + SQ + '</td><td style="background-color: #b1adae;">' + SPQ + '</td><td  style="background-color: #b1adae;">' + S + '</td>';
                            }
                            else {
                                str += '<td>' + TQ + '</td><td>' + TPQ + '</td><td>' + T + '</td><td>' + SQ + '</td><td>' + SPQ + '</td><td>' + S + '</td>';
                            }

                            md.setMonth(md.getMonth() + 1);
                            j++;
                        }
						if (str != "" && tst == 1) {
                        $(tbl).find('tbody').append('<tr>' + str + ' <td style="background-color: #b3a46d; color: white;"> ' + TTQ + ' </td><td style="background-color: #b3a46d; color: white;"> ' + TTPQ + ' </td> <td style="background-color: #b3a46d; color: white;"> ' + TT.toFixed(2) + ' </td><td style="background-color: #b3a46d; color: white;">' + TSQ + '</td><td style="background-color: #b3a46d; color: white;">' + TSPQ + '</td> <td style="background-color: #b3a46d; color: white;">' + TS.toFixed(2) + '</td> </tr> ');
						}

                    }
                    var sum = 0;
                    $('.ogT').each(function (index) {
                        $(this).text(Number(ogVTTot[index]).toFixed(2));
                        sum += Number(ogVTTot[index]);
                    });
                    $('.ogTT').text(sum.toFixed(2));
                    sum = 0;
                    $('.ogS').each(function (index) {
                        $(this).text(Number(ogVSTot[index]).toFixed(2));
                        sum += Number(ogVSTot[index]);
                    });
                    $('.ogST').text(sum.toFixed(2));
                    sum = 0;
                    $('.ogTc').each(function (index) {
                        $(this).text(Number(ogcTTot[index]||0));
                        var vl = Number(ogcTTot[index] || 0);
                        sum = sum + vl;
                    });
                    $('.ogTcT').text(sum);
                    sum = 0;
                    $('.ogSc').each(function (index) {
                        $(this).text(Number(ogcSTot[index]||0));
                        // sum += Number(ogcSTot[index]);

                        var vl = Number(ogcSTot[index] || 0);
                        sum = sum + vl;

                    });
                    $('.ogScT').text(sum);

                    sum = 0;
                    $('.ogTQ').each(function (index) {
                        $(this).text(ogVTTotQ[index]);
                        sum += Number(ogVTTotQ[index]);
                    });
                    $('.ogTTQ').text(sum);
                    sum = 0;
                    
                    $('.ogTPQ').each(function (index) {
                        $(this).text(ogVPTTot[index]);
                        sum += Number(ogVPTTot[index]);
                    });
                    $('.ogTTPQ').text(sum);
                    sum = 0;
                    $('.ogSQ').each(function (index) {
                        $(this).text(ogVSTotQ[index]);
                        sum += Number(ogVSTotQ[index]);
                    });
                    $('.ogSTQ').text(sum);
                    sum = 0;
                    $('.ogSPQ').each(function (index) {
                        $(this).text(ogVPSTot[index]);
                        sum += Number(ogVPSTot[index]);
                    });
                    $('.ogSTPQ').text(sum);
                }
            }


            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "rpt_primaryPDTwise_FO_Target.aspx/getProductTargetSale",
                data: "{'sf_Code':'" + sfCode + "','fYear':'" + fYear + "','fMonth':'" + fMonth + "','tYear':'" + tYear + "','tMonth':'" + tMonth + "'}",
                dataType: "json",
                success: function (data) {
                    Dtls = data.d;
                    console.log(data.d);
                },
                error: function (result) {
                    console.log(result);
                    alert(JSON.stringify(result));
                }
            });


            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "rpt_primaryPDTwise_FO_Target.aspx/getdata",
                data: "{'SubDiv':'" + SubDiv + "'}",
                dataType: "json",
                success: function (data) {
                    pDs = data.d;
                    console.log(data.d);
                    genReport();

                },
                error: function (result) {
                    console.log(result);
                    alert(JSON.stringify(result));
                }
            });
            $(document).on('click', '#btnExcel', function (e) {
                //window.open('data:application/vnd.ms-excel,' + encodeURIComponent($('#excelDiv').html()));
                //e.preventDefault();
                //                var a = document.createElement('a');
                //                var data_type = 'data:application/vnd.ms-excel';
                //                a.href = data_type + ', ' + encodeURIComponent($('div[id$=excelDiv]').html());
                //                document.body.appendChild(a);
                //                a.download = 'TargetVsSales.xls';
                //                a.click();
                //                e.preventDefault();

                var dt = new Date();
                var day = dt.getDate();
                var month = dt.getMonth() + 1;
                var year = dt.getFullYear();
                var postfix = day + "_" + month + "_" + year;
                var a = document.createElement('a');
                var blob = new Blob([$('div[id$=excelDiv]').html()], {
                    type: "application/csv;charset=utf-8;"
                });
                document.body.appendChild(a);
                a.href = URL.createObjectURL(blob);
                a.download = 'TargetVsSales_' + postfix + '.xls';
                a.click();
                e.preventDefault();

            });

            $(document).on('click', '#btnPrint', function (e) {
                var css = '<link href="../css/style.css" rel="stylesheet" />';
                var printContent = document.getElementById("excelDiv");
                var WinPrint = window.open('', '', 'width=900,height=650');
                WinPrint.document.write(printContent.innerHTML);
                WinPrint.document.head.innerHTML = css;
                WinPrint.document.close();
                WinPrint.focus();
                WinPrint.print();
                WinPrint.close();

            });
        });
        function monthDiff(d1, d2) {
            var months;
            months = (d2.getFullYear() - d1.getFullYear()) * 12;
            months -= d1.getMonth();
            months += d2.getMonth();
            return months <= 0 ? 0 : months;
        }
    </script>
    <style type="text/css">
        .odd
        {
            background-color: #b1adae;
        }
    </style>
</head>
    <body>
    <form id="form1" runat="server">
    <br />
    <div class="container" style="max-width: 100%; width: 100%; text-align: right; padding-right: 50px">
        <asp:LinkButton ID="btnPrint" runat="Server" Style="padding: 0px 20px;" class="btn btnPrint" />
        <asp:LinkButton ID="btnExport" runat="Server" Style="padding: 0px 20px; display: none"
            class="btn btnPdf" />
        <asp:LinkButton ID="btnExcel" runat="Server" Style="padding: 0px 20px;" class="btn btnExcel" />
        <a name="btnClose" id="btnClose" type="button" style="padding: 0px 20px;" href="javascript:window.open('','_self').close();"
            class="btn btnClose"></a>
    </div>
    <div id="excelDiv" class="container" style="max-width: 100%; width: 100%">
        <div style="text-align: left; padding: 2px 50px; font-size: large">
            PRODUCT WISE TARGET VS SALES ANALYSIS REPORT of - <b>
                <asp:Label ID="lblyear" runat="server"></asp:Label></b>
        </div>
        <div style="text-align: left; padding: 2px 50px;">
            <b>
                <asp:Label ID="lblsf_name" runat="server"></asp:Label>
                <asp:HiddenField ID="hidn_sf_code" runat="server" />
                <asp:HiddenField ID="hFYear" runat="server" />
                <asp:HiddenField ID="hTYear" runat="server" />
                <asp:HiddenField ID="hFMonth" runat="server" />
                <asp:HiddenField ID="hTMonth" runat="server" />
                <asp:HiddenField ID="subDiv" runat="server" />
            </b>
        </div>
        <div>
        </div>
        <div style="width: 95%; margin: 0px auto;">
            <table id="cTable" class="newStly" border="1" style="border-collapse: collapse;">
                <thead>
                </thead>
                <tbody>
                </tbody>
            </table>
        </div>
    </div>
    <br />
    <br />
    </form>
</body>
</html>
