<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rpt_pri_VS_sec_saleorder.aspx.cs" Inherits="MIS_Reports_rpt_pri_VS_sec_saleorder" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="https://cdn.fusioncharts.com/fusioncharts/latest/fusioncharts.js"></script>
    <script src="https://cdn.fusioncharts.com/fusioncharts/latest/themes/fusioncharts.theme.fusion.js"></script>
    <script type="text/javascript" src="https://cdn.fusioncharts.com/fusioncharts/latest/fusioncharts.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/fusioncharts/3.19.0/fusioncharts.charts.min.js" integrity="sha512-sOa2f3GBH51Dl2vVdWCr82Oh3XttVv8WiU63KKV8opdn5/qZOkBBKG27uvYGRlQEPmiPbk8cAuNugM/IWQOuug==" crossorigin="anonymous" referrerpolicy="no-referrer"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/fusioncharts/3.19.0/fusioncharts.timeseries.min.js" integrity="sha512-1lB44PE93nKPcIjzZciyZ3Jy6bHjhtMb6+C/Mp/+H8eMRD5cchUeqLpeYNUIHUsPn202L6NeWHtl61KbM5hwpg==" crossorigin="anonymous" referrerpolicy="no-referrer"></script>
    <script type="text/javascript" src="https://cdn.fusioncharts.com/fusioncharts/latest/themes/fusioncharts.theme.fusion.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/fusioncharts/3.19.0/fusioncharts.zoomline.min.js" integrity="sha512-WTW2HuscQranJ9fdXPHI7Tm/g+Pbq79bJrlzxmExRpL8L5NdnZkiul2EHxFe+i9LiRbZ6eEXZ3KUrtbzPKQcLw==" crossorigin="anonymous" referrerpolicy="no-referrer"></script>
    <script src="//ajax.googleapis.com/ajax/libs/jquery/1.9.1/jquery.min.js"></script>
    <link href="../css/SalesForce_New/bootstrap-select.min.css" rel='stylesheet' type='text/css' />
    <style>
        #div {
            display: flex;
            flex-wrap: wrap;
            justify-content: center;
        }

        .spinner {
            margin: 100px auto;
            width: 50px;
            height: 40px;
            text-align: center;
            font-size: 10px;
        }

            .spinner > div {
                background-color: #333;
                height: 100%;
                width: 6px;
                display: inline-block;
                -webkit-animation: sk-stretchdelay 1.2s infinite ease-in-out;
                animation: sk-stretchdelay 1.2s infinite ease-in-out;
            }

            .spinner .rect2 {
                -webkit-animation-delay: -1.1s;
                animation-delay: -1.1s;
            }

            .spinner .rect3 {
                -webkit-animation-delay: -1.0s;
                animation-delay: -1.0s;
            }

            .spinner .rect4 {
                -webkit-animation-delay: -0.9s;
                animation-delay: -0.9s;
            }

            .spinner .rect5 {
                -webkit-animation-delay: -0.8s;
                animation-delay: -0.8s;
            }

        @-webkit-keyframes sk-stretchdelay {
            0%, 40%, 100% {
                -webkit-transform: scaleY(0.4)
            }

            20% {
                -webkit-transform: scaleY(1.0)
            }
        }

        @keyframes sk-stretchdelay {
            0%, 40%, 100% {
                transform: scaleY(0.4);
                -webkit-transform: scaleY(0.4);
            }

            20% {
                transform: scaleY(1.0);
                -webkit-transform: scaleY(1.0);
            }
        }

        .spinnner_div {
            width: 1200px;
            height: 1000px;
            background: rgba(255, 255, 255, 0.1);
            backdrop-filter: blur(2px);
            position: absolute;
            z-index: 100;
            overflow-y: hidden;
        }
    </style>
</head>
<body>
    <script type="text/javascript">
        var SecSales = [], PriSales = [], Prdcat = [], FPrdcat = [];
        $(document).ready(function () {
            $('.spinnner_div').show();
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "rpt_pri_VS_sec_saleorder.aspx/Sec_Saleord",
                dataType: "json",
                success: function (data) {
                    SecSales = JSON.parse(data.d) || [];

                },
                error: function (result) {
                    alert(JSON.stringify(result));
                }
            });
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "rpt_pri_VS_sec_saleorder.aspx/Pri_Saleord",
                dataType: "json",
                success: function (data) {
                    PriSales = JSON.parse(data.d) || [];
                },
                error: function (result) {
                    alert(JSON.stringify(result));
                }
            });
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "rpt_pri_VS_sec_saleorder.aspx/AllProductCat",
                dataType: "json",
                success: function (data) {
                    Prdcat = JSON.parse(data.d) || [];
                    $('.spinnner_div').hide();
                },
                error: function (res) {
                    alert(res);
                }
            }); $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "rpt_pri_VS_sec_saleorder.aspx/ProductCat",
                dataType: "json",
                success: function (data) {
                    FPrdcat = JSON.parse(data.d) || [];
                    $('.spinnner_div').hide();
                },
                error: function (res) {
                    alert(res);
                }
            });
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "rpt_pri_VS_sec_saleorder.aspx/productGrp",
                dataType: "json",
                success: function (data) {
                    PrdGrp = JSON.parse(data.d) || [];
                },
                error: function (res) {
                    alert(res);
                }
            });
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "rpt_pri_VS_sec_saleorder.aspx/productName",
                dataType: "json",
                success: function (data) {
                    PrdName = JSON.parse(data.d) || [];
                },
                error: function (res) {
                    alert(res);
                }
            });
            var PrdList = '';
			'<%=sfNm%>' != '' ? $("#lblSf").text('Sales Force Name :' +'<%=sfNm%>') : "";
            '<%=stateNm%>' != '' ? $("#lblst").text('State :' +'<%=stateNm%>') : "";
            '<%=reporting_sfNm%>' != '' ? $("#lblmgr").text('Manager Name :' +'<%=reporting_sfNm%>') : "";
            '<%=Divnm%>' != '' ? $("#lblDiv").text('Division :' +'<%=Divnm%>') : "";
            if ('<%=PrdNm%>' != '') {
                PrdList = '';
                for (var i = 0; i < PrdName.length; i++) {
                    PrdList += PrdName[i].Product_Detail_Name + ',';
                }
                $("#lblPnm").text('Product Name :  ' + PrdList.slice(0, PrdList.length - 1))
            }

            if ('<%=PrdGrp%>' != '') {
                PrdList = '';
                for (var i = 0; i < PrdGrp.length; i++) {
                    PrdList += PrdGrp[i].Product_Grp_Name + ',';
                }
                $("#lblpgrp").text('Product Group :  ' + PrdList.slice(0, PrdList.length - 1))
            }

            if ('<%=Prdcat%>' != '') {
                PrdList = '';
                for (var i = 0; i < FPrdcat.length; i++) {
                    PrdList += FPrdcat[i].Product_Cat_Name + ',';
                }
                $("#lblcat").text('Product Category :  ' + PrdList.slice(0, PrdList.length - 1))
            }
            ReloadTable();
        });
        function fltrPrd(val, filterval) {
            PriSales = PriSales.filter(function (a) {
                return (val.indexOf(a.PRODUCTCODE) > -1)
            });

            SecSales = SecSales.filter(function (a) {
                return (val.indexOf(a.Product_Detail_Code) > -1)
            });
        }
        function fltrPrdCt(val, filterval) {
            PriSales = PriSales.filter(function (a) {
                return (val.indexOf(a.Product_Cat_Code) > -1)
            });

            SecSales = SecSales.filter(function (a) {
                return (val.indexOf(a.Product_Cat_Code) > -1)
            });
        }
        function fltrPrdGrp(val, filterval) {
            PriSales = PriSales.filter(function (a) {
                return (val.indexOf(a.Product_Grp_Code) > -1)
            });

            SecSales = SecSales.filter(function (a) {
                return (val.indexOf(a.Product_Grp_Code) > -1)
            });
        }

        function ReloadTable() {
            if ('<%=PrdGrp%>' != 'null') {
                fltrPrdGrp('<%=PrdGrp%>', 'Product_Grp_Code')
            }
            if ('<%=Prdcat%>' != 'null') {
                fltrPrdCt('<%=Prdcat%>', 'Product_Cat_Code')
            }
            if ('<%=PrdNm%>' != 'null') {
                fltrPrd('<%=PrdNm%>', 'Product_Detail_Code')
            }
            var str = '', hstr = '';
            let PJan = 0, PFeb = 0, PMar = 0, PApr = 0, PMay = 0, PJun = 0, PJul = 0, PAug = 0, PSep = 0, POct = 0, PNov = 0, PDec = 0;
            let Ptotl = 0, PAvrg = 0;
            let SJan = 0, SFeb = 0, SMar = 0, SApr = 0, SMay = 0, SJun = 0, SJul = 0, SAug = 0, SSep = 0, SOct = 0, SNov = 0, SDec = 0;
            let Stotl = 0, SAvrg = 0;
            if ((SecSales.length > 0 || PriSales.length > 0) && Prdcat.length > 0) {
                $("#cnt_retailerdet tbody").html("");
                if ('<%=stateNm%>' != 'Nothing Select' && '<%=stateNm%>' != '')
                    hstr = "<tr><th colspan='16'>" + '<%=stateNm%>' + "</th></tr>";
                else
                    hstr = "<tr><th colspan='16'>PAN INDIA</th></tr>";
                hstr += "<th class='col-xs-2'>Group</th><th class='col-xs-2'>Sale Type</th><th class='col-xs-2'>Jan-" + '<%=frmdt%>'.slice(2, 4) + "</th><th class='col-xs-2'>Feb-" + '<%=frmdt%>'.slice(2, 4) + "</th><th class='col-xs-2'>Mar-" + '<%=frmdt%>'.slice(2, 4) + "</th><th class='col-xs-2'>Apr-" + '<%=frmdt%>'.slice(2, 4) + "</th><th class='col-xs-2'>May-" + '<%=frmdt%>'.slice(2, 4) + "</th><th class='col-xs-2'>Jun-" + '<%=frmdt%>'.slice(2, 4) + "</th><th class='col-xs-2'>Jul-" + '<%=frmdt%>'.slice(2, 4) + "</th><th class='col-xs-2'>Aug-" + '<%=frmdt%>'.slice(2, 4) + "</th><th class='col-xs-2'>Sep-" + '<%=frmdt%>'.slice(2, 4) + "</th><th class='col-xs-2'>Oct-" + '<%=frmdt%>'.slice(2, 4) + "</th><th class='col-xs-2'>Nov-" + '<%=frmdt%>'.slice(2, 4) + "</th><th class='col-xs-2'>Dec-" + '<%=frmdt%>'.slice(2, 4) + "</th><th class='col-xs-2' style='background: darkseagreen;'>Total</th><th class='col-xs-2' style='background: yellow;'>Average Per Month</th>";
                $("#cnt_retailerdet thead").append(hstr);
                for (var i = 0; i < Prdcat.length; i++) {
                    PJan = 0, PFeb = 0, PMar = 0, PApr = 0, PMay = 0, PJun = 0, PJul = 0, PAug = 0, PSep = 0, POct = 0, PNov = 0, PDec = 0;
                    Ptotl = 0, PAvrg = 0;
                    SJan = 0, SFeb = 0, SMar = 0, SApr = 0, SMay = 0, SJun = 0, SJul = 0, SAug = 0, SSep = 0, SOct = 0, SNov = 0, SDec = 0;
                    Stotl = 0, SAvrg = 0;

                    FltrPrisales = PriSales.filter(function (a) {
                        return a.Product_Cat_Code == Prdcat[i].Product_Cat_Code
                    })

                    FltrSecsales = SecSales.filter(function (a) {
                        return a.Product_Cat_Code == Prdcat[i].Product_Cat_Code
                    })

                    if (FltrPrisales.length > 0 || FltrSecsales.length > 0) {


                        str += "<tr style='text-align: center;'><td rowspan='3' id='" + Prdcat[i].Product_Cat_Code + "'>" + Prdcat[i].Product_Cat_Name + "</td>";
                        for (var j = 0; j < FltrPrisales.length; j++) {
                            PJan += FltrPrisales[j].Jan;
                            PFeb += FltrPrisales[j].Feb;
                            PMar += FltrPrisales[j].Mar;
                            PApr += FltrPrisales[j].Apr;
                            PMay += FltrPrisales[j].May;
                            PJun += FltrPrisales[j].Jun;
                            PJul += FltrPrisales[j].Jul;
                            PAug += FltrPrisales[j].Aug;
                            PSep += FltrPrisales[j].Sep;
                            POct += FltrPrisales[j].Oct;
                            PNov += FltrPrisales[j].Nov;
                            PDec += FltrPrisales[j].Dec;
                            Ptotl += FltrPrisales[j].ttl;
                            PAvrg += FltrPrisales[j].Avrg;
                            /*if (FltrPrisales.length > 1) {
                                PJan = PJan + FltrPrisales[j].Jan;
                                PFeb = PFeb + FltrPrisales[j].Feb;
                                PMar = PMar + FltrPrisales[j].Mar;
                                PApr = PApr + FltrPrisales[j].Apr;
                                PMay = PMay + FltrPrisales[j].May;
                                PJun = PJun + FltrPrisales[j].Jun;
                                PJul = PJul + FltrPrisales[j].Jul;
                                PAug = PAug + FltrPrisales[j].Aug;
                                PSep = PSep + FltrPrisales[j].Sep;
                                POct = POct + FltrPrisales[j].Oct;
                                PNov = PNov + FltrPrisales[j].Nov;
                                PDec = PDec + FltrPrisales[j].Dec;
                                Ptotl = Ptotl + FltrPrisales[j].ttl;
                                PAvrg = PAvrg + FltrPrisales[j].Avrg;
                            }*/
                        }
                        for (var k = 0; k < FltrSecsales.length; k++) {
                            SJan += FltrSecsales[k].Jan;
                            SFeb += FltrSecsales[k].Feb;
                            SMar += FltrSecsales[k].Mar;
                            SApr += FltrSecsales[k].Apr;
                            SMay += FltrSecsales[k].May;
                            SJun += FltrSecsales[k].Jun;
                            SJul += FltrSecsales[k].Jul;
                            SAug += FltrSecsales[k].Aug;
                            SSep += FltrSecsales[k].Sep;
                            SOct += FltrSecsales[k].Oct;
                            SNov += FltrSecsales[k].Nov;
                            SDec += FltrSecsales[k].Dec;
                            Stotl += FltrSecsales[k].ttl;
                            SAvrg += FltrSecsales[k].Avrg;
                            /* if (FltrSecsales.length > 1) {
                                 SJan = SJan + FltrSecsales[k].Jan;
                                 SFeb = SFeb + FltrSecsales[k].Feb;
                                 SMar = SMar + FltrSecsales[k].Mar;
                                 SApr = SApr + FltrSecsales[k].Apr;
                                 SMay = SMay + FltrSecsales[k].May;
                                 SJun = SJun + FltrSecsales[k].Jun;
                                 SJul = SJul + FltrSecsales[k].Jul;
                                 SAug = SAug + FltrSecsales[k].Aug;
                                 SSep = SSep + FltrSecsales[k].Sep;
                                 SOct = SOct + FltrSecsales[k].Oct;
                                 SNov = SNov + FltrSecsales[k].Nov;
                                 SDec = SDec + FltrSecsales[k].Dec;
                                 Stotl = Stotl + FltrSecsales[k].ttl;
                                 SAvrg = SAvrg + FltrSecsales[k].Avrg;
                             }*/
                        }

                        str += "<td class='col-xs-2'>Primary Sale</td><td class='col-xs-2'>" + PJan + "</td><td class='col-xs-2'>" + PFeb + "</td><td class='col-xs-2'>" + PMar + "</td><td class='col-xs-2'>" + PApr + "</td><td class='col-xs-2'>" + PMay + "</td><td class='col-xs-2'>" + PJun + "</td><td class='col-xs-2'>" + PJul + "</td><td class='col-xs-2'>" + PAug + "</td><td class='col-xs-2'>" + PSep + "</td><td class='col-xs-2'>" + POct + "</td><td class='col-xs-2'>" + PNov + "</td><td class='col-xs-2'>" + PDec + "</td><td class='col-xs-2' style='background: darkseagreen;'>" + Ptotl + "</td><td class='col-xs-2' style='background: yellow;'>" + PAvrg + "</td></tr>";
                        str += "<tr style='text-align: center;'><td class='col-xs-2'>Secondary Sale</td><td class='col-xs-2'>" + SJan + "</td><td class='col-xs-2'>" + SFeb + "</td><td class='col-xs-2'>" + SMar + "</td><td class='col-xs-2'>" + SApr + "</td><td class='col-xs-2'>" + SMay + "</td><td class='col-xs-2'>" + SJun + "</td><td class='col-xs-2'>" + SJul + "</td><td class='col-xs-2'>" + SAug + "</td><td class='col-xs-2'>" + SSep + "</td><td class='col-xs-2'>" + SOct + "</td><td class='col-xs-2'>" + SNov + "</td><td class='col-xs-2'>" + SDec + "</td><td class='col-xs-2' style='background: darkseagreen;'>" + Stotl + "</td><td class='col-xs-2' style='background: yellow;'>" + SAvrg + "</td></tr>";
                        str += "<tr style='background: #bcdaf4b8; border-bottom: 1px solid red !important;text-align: center;'><td class='col-xs-2'>SS vs PS</td><td class='col-xs-2'>" + ((isNaN(((parseInt(SJan) / parseInt(PJan)) * 100)) == true || (parseInt(SJan) == 0 || parseInt(PJan) == 0)) ? 0 : Math.round((parseInt(SJan) / parseInt(PJan)) * 100)) + "</td><td class='col-xs-2'>" + ((isNaN(((parseInt(SFeb) / parseInt(PFeb)) * 100)) == true || (parseInt(SFeb) == 0 || parseInt(PFeb) == 0)) ? 0 : Math.round((parseInt(SFeb) / parseInt(PFeb)) * 100)) + "</td><td class='col-xs-2'>" + ((isNaN(((parseInt(SMar) / parseInt(PMar)) * 100)) == true || (parseInt(SMar) == 0 || parseInt(PMar) == 0)) ? 0 : Math.round((parseInt(SMar) / parseInt(PMar)) * 100)) + "</td><td class='col-xs-2'>" + ((isNaN(((parseInt(SApr) / parseInt(PApr)) * 100)) == true || (parseInt(SApr) == 0 || parseInt(PApr) == 0)) ? 0 : Math.round((parseInt(SApr) / parseInt(PApr)) * 100)) + "</td><td class='col-xs-2'>" + ((isNaN(((parseInt(SMay) / parseInt(PMay)) * 100)) == true || (parseInt(SMay) == 0 || parseInt(PMay) == 0)) ? 0 : Math.round((parseInt(SMay) / parseInt(PMay)) * 100)) + "</td><td class='col-xs-2'>" + ((isNaN(((parseInt(SJun) / parseInt(PJun)) * 100)) == true || (parseInt(SJun) == 0 || parseInt(PJun) == 0)) ? 0 : Math.round((parseInt(SJun) / parseInt(PJun)) * 100)) + "</td><td class='col-xs-2'>" + ((isNaN(((parseInt(SJul) / parseInt(PJul)) * 100)) == true || (parseInt(SJul) == 0 || parseInt(PJul) == 0)) ? 0 : Math.round((parseInt(SJul) / parseInt(PJul)) * 100)) + "</td><td class='col-xs-2'>" + ((isNaN(((parseInt(SAug) / parseInt(PAug)) * 100)) == true || (parseInt(SAug) == 0 || parseInt(PAug) == 0)) ? 0 : Math.round((parseInt(SAug) / parseInt(PAug)) * 100)) + "</td><td class='col-xs-2'>" + ((isNaN(((parseInt(SSep) / parseInt(PSep)) * 100)) == true || (parseInt(SSep) == 0 || parseInt(PSep) == 0)) ? 0 : Math.round((parseInt(SSep) / parseInt(PSep)) * 100)) + "</td><td class='col-xs-2'>" + ((isNaN(((parseInt(SOct) / parseInt(POct)) * 100)) == true || (parseInt(SOct) == 0 || parseInt(POct) == 0)) ? 0 : Math.round((parseInt(SOct) / parseInt(POct)) * 100)) + "</td><td class='col-xs-2'>" + ((isNaN(((parseInt(SNov) / parseInt(PNov)) * 100)) == true || (parseInt(SNov) == 0 || parseInt(PNov) == 0)) ? 0 : Math.round((parseInt(SNov) / parseInt(PNov)) * 100)) + "</td><td class='col-xs-2'>" + ((isNaN(((parseInt(SDec) / parseInt(PDec)) * 100)) == true || (parseInt(SDec) == 0 || parseInt(PDec) == 0)) ? 0 : Math.round((parseInt(SDec) / parseInt(PDec)) * 100)) + "</td><td class='col-xs-2'></td><td class='col-xs-2'></td></tr>";

                        div = "<div id='chartDiv" + i + "' class='col-md-4'style='height: 400px; width: 400px;'></div>";
                        $("#div").append(div);
                        var chartContainer = document.createElement("div");
                        chartContainer.id = "chartContainer" + i;
                        chartContainer.style.height = "100%";
                        chartContainer.style.width = "100%";
                        document.getElementById("chartDiv" + i + "").appendChild(chartContainer);


                        // Create chart
                        var chartarr = [{
                            type: "spline",
                            showInLegend: true,
                            name: "Primary",
                            dataPoints: [
                                { label: "JAN", y: PJan },
                                { label: "FEB", y: PFeb },
                                { label: "MAR", y: PMar },
                                { label: "APR", y: PApr },
                                { label: "MAY", y: PJan },
                                { label: "JUN", y: PJun },
                                { label: "JUL", y: PJul },
                                { label: "AUG", y: PAug },
                                { label: "SEP", y: PSep },
                                { label: "OCT", y: POct },
                                { label: "NOV", y: PNov },
                                { label: "DEC", y: PDec }
                            ]
                        },
                        {
                            type: "line",
                            showInLegend: true,
                            name: "Secondary",
                            dataPoints: [
                                { label: "JAN", y: SJan },
                                { label: "FEB", y: SFeb },
                                { label: "MAR", y: SMar },
                                { label: "APR", y: SApr },
                                { label: "MAY", y: SJan },
                                { label: "JUN", y: SJun },
                                { label: "JUL", y: SJul },
                                { label: "AUG", y: SAug },
                                { label: "SEP", y: SSep },
                                { label: "OCT", y: SOct },
                                { label: "NOV", y: SNov },
                                { label: "DEC", y: SDec }
                            ]
                        }
                        ]
                        var chart = new CanvasJS.Chart(chartContainer.id, {
                            theme: "light2",
                            animationEnabled: true,
                            title: {
                                text: Prdcat[i].Product_Cat_Name
                            },
                            axisY: {
                                title: "Number of Quantity"
                            },
                            toolTip: {
                                shared: "true"
                            },
                            legend: {
                                cursor: "pointer",
                                itemclick: toggleDataSeries
                            },
                            data: chartarr

                        });
                        chart.render();

                        function toggleDataSeries(e) {
                            if (typeof (e.dataSeries.visible) === "undefined" || e.dataSeries.visible) {
                                e.dataSeries.visible = false;
                            } else {
                                e.dataSeries.visible = true;
                            }
                            chart.render();
                        }

                    }
                }
                $("#cnt_retailerdet").append(str);
            }
        }
        //window.onload = function () {
        //}
    </script>

    <script src="https://canvasjs.com/assets/script/canvasjs.min.js"></script>

    <form id="form1" runat="server">

        <div>
            <div class="row m-0">
                <h4>Primary Vs Secondary Sale Order Qty</h4>
                <div class="card" style="margin: 15px 0px 0px 0px;">
                    <div class="card-body table-responsive">
                        <div class="row">
                            <div class="col-lg-12">
                                <div class="col-lg-6">
                                    <table>
                                        <tbody>
                                            <tr>
                                                <td>
                                                    <label id="lblDiv"></label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <label id="lblSf"></label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <label id="lblmgr"></label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <label id="lblst"></label>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>
                                <div class="col-lg-6">
                                    <table>
                                        <tbody>
                                            <tr>
                                                <td id="lblPnm"></td>
                                            </tr>
                                            <tr>
                                                <td id="lblpgrp"></td>
                                            </tr>
                                            <tr>
                                                <td id="lblcat"></td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="table table-responsive col-10" style="margin: 25px; margin-bottom: 25px;">
                    <table id="cnt_retailerdet" style="border: 1px solid gray !important; min-width: 100%;" class="table table-bordered table-hover grids">
                        <thead>
                            <tr>
                            </tr>
                        </thead>
                        <tbody>
                        </tbody>
                        <tfoot></tfoot>
                    </table>
                </div>
            </div>
            <div class="col-2">

                <div class="overlay" id="loadover" style="display: none;">
                    <div id="loader">Please Wait Loading...</div>
                </div>
            </div>
        </div>
        <div>
            <div class="row">
                <div class="col-lg-12" id="div">
                </div>
            </div>
        </div>
    </form>
</body>
</html>
