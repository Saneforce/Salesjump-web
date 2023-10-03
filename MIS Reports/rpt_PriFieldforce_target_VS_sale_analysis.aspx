<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rpt_PriFieldforce_target_VS_sale_analysis.aspx.cs" Inherits="MIS_Reports_rpt_PriFieldforce_target_VS_sale_analysis" %>


<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../css/bootstrap-3.3.2.min.css" rel="stylesheet" />
    <link href="../css/style.css" rel="stylesheet" />
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {

            var sfCode = $('#<%=hidn_sf_code.ClientID%>').val();
     var fYear = $('#<%=hFYear.ClientID%>').val();
     var fMonth = $('#<%=hFMonth.ClientID%>').val();
     var tYear = $('#<%=hTYear.ClientID%>').val();
     var tMonth = $('#<%=hTMonth.ClientID%>').val();
            var allFF = [];
     var cDtls = [];

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
                if ( cDtls.length > 0) {
                    var tbl = $('[id*=cTable]');
                    $(tbl).find('thead').find('tr').remove();
                    $(tbl).find('tbody').find('tr').remove();
                    var len = 1;

                    var fd = new Date(fYear + '/' + fMonth + '/' + "01/");
                    var td = new Date(tYear + '/' + tMonth + '/' + "01/");
                    cd = fd;
                    var str = '<th rowspan="2">FieldForce Name</th>';
                    var st2 = '';
                    var st3 = '<th  style="text-align: left;">Order Taken FieldForce</th>';
                    var st4 = '<th style="text-align: left;">Order Total</th>';

                    while (cd <= td) {
                        str += '<th colspan="2"> ' + month[cd.getMonth()] + ' - ' + cd.getFullYear() + ' </th>';
                        st2 += '<th>Target</th><th>Sales</th>';
                        st3 += '<th class="ogTc"></th><th class="ogSc"></th>';
                        st4 += '<th class="ogT"></th><th class="ogS"></th>';
                        cd.setMonth(cd.getMonth() + 1);
                        len++;
                    }
                    $(tbl).find('thead').append('<tr style="background-color: #496a9a; color: white;">' + str + '<th colspan="2">Total</th></tr>');
                    $(tbl).find('thead').append('<tr style="background-color: #496a9a; color: white;">' + st2 + '<th >Target</th><th>Sales</th></tr>');
                    $(tbl).find('thead').append('<tr style="background-color: #496a9a; color: white;">' + st3 + '<th class="ogTcT"></th><th class="ogScT"></th></tr>');
                    $(tbl).find('thead').append('<tr style="background-color: #496a9a; color: white;">' + st4 + '<th class="ogTT"></th><th class="ogST"></th></tr>');


                    var ogcTTot = [];
                    var ogcSTot = [];

                    var ogVTTot = [];
                    var ogVSTot = [];

                        var roTTot = [];
                        var roSTot = [];

                    if (cDtls.length > 0) {
                       
                            var lTT = 0;
                            var lTS = 0;
                            var sfname = "";
                            //sf = cDtls.filter(function (a) {
                            //    if (("," + sfname + ",").indexOf("," + a.SF_Code + ",") < 0) {
                            //        sfname += a.SF_Code + ",";
                            //        return true
                            //    }
                            //});
                        for (var k = 0; k < allFF.length; k++) {
                            str = '<td>' + allFF[k].Sf_Name + '</td>';

                            md = new Date(fYear + '/' + fMonth + '/' + "01/");

                            var j = 0;
                            while (md <= td) {

                                cR = cDtls.filter(function (obj) {
                                    return (Number(obj.cmonth) === Number((md.getMonth() + 1)) && Number(obj.cyear) === Number(md.getFullYear()) && obj.SF_Code === allFF[k].Sf_Code);
                                });
                                var tVal = '';
                                var sVal = '';

                                if (cR.length > 0) {

                                    tVal = cR[0].target_val == '' ? 0 : cR[0].target_val;
                                    sVal = cR[0].order_val == '' ? 0 : cR[0].order_val;
                                    console.log();
                                    if (Number(tVal) > 0) {
                                        num = Number(ogcTTot[j] || 0);
                                        ogcTTot[j] = Number(num + 1);
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

                                if (md.getMonth() % 2 == 0) {

                                    str += '<td style="background-color: #b1adae;"  >' + (tVal == 0 ? '' : tVal)+ '</td><td style="background-color: #b1adae;">' + (sVal == 0 ? '' : sVal) + '</td>';
                                }
                                else {
                                    str += '<td  >' + (tVal == 0 ? '' : tVal)+ '</td><td>' + (sVal == 0 ? '' : sVal) + '</td>';
                                }
                                j++;

                            }
                                $(tbl).find('tbody').append('<tr>' + str + '<td style="background-color: #b3a46d; color: white;">' + lTT.toFixed(2) + '</td><td style="background-color: #b3a46d; color: white;">' + lTS.toFixed(2) + '</td></tr>');
                                lTT = 0;
                                lTS = 0;
                        }
                    }

                   
                    console.log(ogcTTot);
                    console.log(ogcSTot);
                    console.log(ogVTTot);
                    console.log(ogVSTot);
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

                }
            }
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "rpt_PriFieldforce_target_VS_sale_analysis.aspx/GetFF",
                data: "{'ddlFieldForce':'" + sfCode + "'}",
                dataType: "json",
                success: function (data) {
                    FF = JSON.parse(data.d) || [];
                    allFF = FF;
                },
                error: function (data) {
                    alert(JSON.stringify(data));
                }
            });

            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "rpt_PriFieldforce_target_VS_sale_analysis.aspx/getFieldForceSales",
                data: "{'SF_Code':'" + sfCode + "', 'FYear':'" + fYear + "', 'FMonth':'" + fMonth + "', 'TYear':'" + tYear + "', 'TMonth':'" + tMonth + "'}",
                dataType: "json",
                success: function (data) {
                    cDtls = data.d;
                    genReport();
                    //console.log(data.d);
                },
                error: function (rs) {
                    console.log(rs);
                }

            });
           
            //var Subdiv = $('[id*=subdiv]').val();
          
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
        @media print
        {
            thead
            {
                display: table-header-group;
            }
        
            tfoot
            {
                display: table-footer-group;
            }
        }
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
            PRIMARY FIELDFORCE WISE TARGET VS SALES ANALYSIS REPORT of - <b>
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
