<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rpt_Distributor_target_VS_sales_analysis.aspx.cs" Inherits="MIS_Reports_rpt_Distributor_target_VS_sales_analysis" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../css/bootstrap-3.3.2.min.css" rel="stylesheet" />
    <link href="../css/style.css" rel="stylesheet" />
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript">


        $(document).ready(function () {


            var dDtls = [];
            var dValue = [];

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

            genReport = function () {
                if (dDtls.length > 0) {
                    var tbl = $('[id*=cTable]');
                    $(tbl).find('thead').find('tr').remove();
                    $(tbl).find('tbody').find('tr').remove();
                    var len = 1;
                    var fd = new Date(fYear + '/' + fMonth + '/' + "01/");
                    var td = new Date(tYear + '/' + tMonth + '/' + "01/");
                    cd = fd;
                    var str = '<th rowspan="2">SlNo.</th><th rowspan="2">Name</th>';
                    var st2 = '';
                    var st3 = '<th colspan="2" style="text-align: left;">Order Given Distributor</th>';
                    var st4 = '<th colspan="2" style="text-align: left;">Order Total</th>';

                    while (cd <= td) {
                        str += '<th colspan="2"> ' + month[cd.getMonth()] + ' - ' + cd.getFullYear() + ' </th>';
                        st2 += '<th>Target</th><th>Sales</th>';
                        st3 += '<th class="ogTc"></th><th class="ogSc"></th>';
                        st4 += '<th class="ogT"></th><th class="ogS"></th>';
                        cd.setMonth(cd.getMonth() + 1);
                        len++;
                    }
                    $(tbl).find('thead').append('<tr>' + str + '<th colspan="2">Total</th></tr>');
                    $(tbl).find('thead').append('<tr>' + st2 + '<th>Target</th><th>Sales</th></tr>');
                    $(tbl).find('thead').append('<tr >' + st3 + '<th class="ogTcT"></th><th class="ogScT"></th></tr>');
                    $(tbl).find('thead').append('<tr >' + st4 + '<th class="ogTT"></th><th class="ogST"></th></tr>');

                    var ogcTTot = [];
                    var ogcSTot = [];

                    var ogVTTot = [];
                    var ogVSTot = [];

                    var roTTot = [];
                    var roSTot = [];

                    var fd = new Date(fYear + '/' + fMonth + '/' + "01/");
                    var td = new Date(tYear + '/' + tMonth + '/' + "01/");


                    console.log(dValue);

                    for (var i = 0; i < dDtls.length; i++) {
                        var cT = 0;
                        var cV = 0;
                        var lTT = 0;
                        var lTS = 0;
                        str = '<td>' + (i + 1) + '</td><td>' + dDtls[i].distName + '</td>';
                        md = new Date(fYear + '/' + fMonth + '/' + "01/");
                        var j = 0;
                        while (md <= td) {
                            console.log(dDtls[i].distCode);
                            cR = dValue.filter(function (obj) {
                                return (Number(obj.cMonth) === Number((md.getMonth() + 1)) && Number(obj.cYear) === Number(md.getFullYear()) && obj.dCode === dDtls[i].distCode);
                            });

                            var tVal = '';
                            var sVal = '';

                            if (cR.length > 0) {

                                tVal = cR[0].tarVal == '' ? 0 : cR[0].tarVal;
                                sVal = cR[0].ordVal == '' ? 0 : cR[0].ordVal; ;

                                if (Number(tVal) > 0) {
                                    ogcTTot[j] += Number(Number(ogcTTot[j] || 0) + 1);
                                }

                                else {
                                    num = Number(ogcTTot[j] || 0);
                                    ogcTTot[j] = Number(num + 0);
                                }

                                if (Number(sVal) > 0) {
                                    ogcSTot[j] = Number(Number(ogcSTot[j] || 0) + 1)
                                }
                                else {
                                    ogcSTot[j] = Number(Number(ogcSTot[j] || 0) + 0)
                                }
                            }
                            lTT += Number(tVal || 0);
                            lTS += Number(sVal || 0);

                            roTTot[j] = Number((roTTot[j] || 0) + Number(tVal || 0));
                            roSTot[j] = Number((roSTot[j] || 0) + Number(sVal || 0));

                            ogVTTot[j] = Number((ogVTTot[j] || 0) + Number(tVal || 0));
                            ogVSTot[j] = Number((ogVSTot[j] || 0) + Number(sVal || 0));

                            ogcSTot[j] = Number(Number(ogcSTot[j] || 0) + 0)

                            num = Number(ogcTTot[j] || 0);
                            ogcTTot[j] = Number(num + 0);

                            md.setMonth(md.getMonth() + 1);
                            str += '<td>' + tVal + '</td><td>' + sVal + '</td>';
                            j++;
                        }

                        $(tbl).find('tbody').append('<tr>' + str + '<td>' + lTT + '</td><td>' + lTS + '</td> </tr>');
                    }

                    var stk = '';
                    var ovTTot = 0;
                    var ovSTot = 0;

                    console.log(roTTot.length);


                    if (roTTot.length > 0) {

                        for (v = 0; v < roTTot.length; v++) {
                            stk += '<td>' + Number(roTTot[v]).toFixed(2) + '</td><td>' + Number(roSTot[v]).toFixed(2) + '</td>';
                            //console.log(Number(ovSTot || 0) +' + '+ Number(roSTot[v]).toFixed(2));
                            //console.log(Number(ovSTot || 0) + Number(roSTot[v]).toFixed(2));
                            ovTTot = Number(Number(ovTTot || 0) + Number(roTTot[v])).toFixed(2);
                            ovSTot = Number(Number(ovSTot || 0) + Number(roSTot[v])).toFixed(2);
                        }
                    }
                    else {
                        for (v = 0; v < len - 1; v++) {
                            stk += '<td></td><td></td>';
                        }
                    }
                    //Route Total 
                    $(tbl).find('tbody').append('<tr style="color:blue;background-color: #99FFFF;"><td  colspan="2">Total</td>' + stk + ' <td>' + Number(ovTTot).toFixed(2) + '</td>  <td>' + Number(ovSTot).toFixed(2) + '</td> </tr>');
                }

                //var ogcTTot = [];
                //var ogcSTot = [];

                //var ogVTTot = [];
                //var ogVSTot = [];
                //                console.log(ogcTTot);
                //                console.log(ogcSTot);
                //                console.log(ogVTTot);
                //                console.log(ogVSTot);
                var sum = 0;
                $('.ogTc').each(function (index) {
                    $(this).text(ogcTTot[index]);
                    sum += Number(ogcTTot[index]);
                });
                $('.ogTcT').text(sum);
                sum = 0;
                $('.ogSc').each(function (index) {
                    $(this).text(ogcSTot[index]);
                    sum += Number(ogcSTot[index]);
                });
                $('.ogScT').text(sum);
                sum = 0;
                $('.ogT').each(function (index) {
                    $(this).text(ogVTTot[index]);
                    sum += Number(ogVTTot[index]);
                });
                $('.ogTT').text(sum.toFixed(2));
                sum = 0;
                $('.ogS').each(function (index) {
                    $(this).text(ogVSTot[index]);
                    sum += Number(ogVSTot[index]);
                });
                $('.ogST').text(sum.toFixed(2));


            }

            var sfCode = $('#<%=hidn_sf_code.ClientID%>').val();
            var fYear = $('#<%=hFYear.ClientID%>').val();
            var fMonth = $('#<%=hFMonth.ClientID%>').val();
            var tYear = $('#<%=hTYear.ClientID%>').val();
            var tMonth = $('#<%=hTMonth.ClientID%>').val();

            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "rpt_Distributor_target_VS_sales_analysis.aspx/getDistributorSales",
                data: "{'SF_Code':'" + sfCode + "', 'FYear':'" + fYear + "', 'FMonth':'" + fMonth + "', 'TYear':'" + tYear + "', 'TMonth':'" + tMonth + "'}",
                dataType: "json",
                success: function (data) {
                    dValue = data.d;

                },
                error: function (rs) {
                    console.log(rs);
                }
            });

            $.ajax({
                type: "POST",

                url: "rpt_Distributor_target_VS_sales_analysis.aspx/GetDistributor",
                data: "{'SF_Code':'" + sfCode + "'}",
                contentType: "application/json; charset=utf-8",
                async: false,
                dataType: "json",
                success: function (data) {
                    dDtls = data.d;
                    if (dDtls.length > 0) {
                        genReport();
                    }
                    else {
                        alert('There is No Distributor...!');
                        window.open('', '_self').close();
                    }
                    // console.log(data.d);
                },
                error: function (rs) {
                    console.log(rs);
                }
            });





            $(document).on('click', '#btnExcel', function (e) {
                //window.open('data:application/vnd.ms-excel,' + encodeURIComponent($('#excelDiv').html()));
                //e.preventDefault();
                var a = document.createElement('a');
                var data_type = 'data:application/vnd.ms-excel';
                a.href = data_type + ', ' + encodeURIComponent($('div[id$=excelDiv]').html());
                document.body.appendChild(a);
                a.download = 'TargetVsSales.xls';
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

</head>
<body>
    <form id="form1" runat="server">
        <br />

        <div class="container" style="max-width: 100%; width: 100%; text-align: right; padding-right: 50px">
            <asp:LinkButton ID="btnPrint" runat="Server" Style="padding: 0px 20px;" class="btn btnPrint" />
            <asp:LinkButton ID="btnExport" runat="Server" Style="padding: 0px 20px;" class="btn btnPdf" />
            <asp:LinkButton ID="btnExcel" runat="Server" Style="padding: 0px 20px;" class="btn btnExcel" />
            <a name="btnClose" id="btnClose" type="button" style="padding: 0px 20px;" href="javascript:window.open('','_self').close();" class="btn btnClose"></a>
        </div>
        <div id="excelDiv" class="container" style="max-width: 100%; width: 100%">
            <div style="text-align: left; padding: 2px 50px; font-size: large">
                DISTRIBUTOR WISE TARGET VS SALES ANALYSIS REPORT of : <b>
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
