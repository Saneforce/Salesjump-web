<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rptView_CustomerWise_analysis.aspx.cs" Inherits="MIS_Reports_rptView_CustomerWise_analysis" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <script src="/tdk/scripts/jquery.min.js" type="text/javascript"></script>
<script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
<script src="/tdk/scripts/bootstrap.js" type="text/javascript"></script>
<script src="/tdk/scripts/jquery.dataTables.js" type="text/javascript"></script>
<script src="/tdk/scripts/dataTables.bootstrap.js" type="text/javascript"></script>
    <script src="//ajax.googleapis.com/ajax/libs/jquery/1.9.1/jquery.min.js"></script>
    <link href="../css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <style>
        .sticky-header.scrolled {
            background-color: #eee;
        }

        .spinner {
            margin: 100px auto;
            width: 50px;
            height: 40px;
            text-align: center;
            font-size: 10px;
        }
        .truncate {
            max-width: 160px;
            overflow: hidden;
            /*display: inline-block;*/
            text-overflow: ellipsis;
            white-space: nowrap;
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
    <title></title>
</head>
<script type="text/javascript">
    var AnalysisSales = [];
    var CatDet = [];
    var PrdCatList = [];
    const monthNames = ["January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"];
    $(document).ready(function () {
        $(function() {
            $(".tooltip").tooltip();            
         });
        $("#spinnner_div").show();
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            async: false,
            url: "rptView_CustomerWise_analysis.aspx/GetCustWise_Analysis",
            dataType: "json",
            success: function (data) {
                AnalysisSales = JSON.parse(data.d) || [];

            },
            error: function (result) {
                alert(JSON.stringify(result));
                $("#spinnner_div").hide();
            }
        });
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            async: false,
            url: "rptView_CustomerWise_analysis.aspx/GetCat",
            dataType: "json",
            success: function (data) {
                PrdCatList = JSON.parse(data.d) || [];
            },
            error: function (result) {
                alert(JSON.stringify(result));
                $("#spinnner_div").hide();
            }
        });
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            async: false,
            url: "rptView_CustomerWise_analysis.aspx/GetCustOvrlRate",
            dataType: "json",
            success: function (data) {
                CatDet = JSON.parse(data.d) || [];
            },
            error: function (result) {
                alert(JSON.stringify(result));
                $("#spinnner_div").hide();
            }
        }); ReloadTable();
        $("#spinnner_div").hide();
    });
    function ReloadTable() {
        var strhead = "";
        var strbody = "";
        let tot = 0;
        let totval = 0;
        let Ovrlltot = 0;
        let Otot = 0;
        $("#cnt_retailerdet thead").html("");
        var fdate = new Date('<%=frmdt%>');
        var year = fdate.getFullYear();
        var fmonth = fdate.getMonth() + 1;
        var tdate = new Date('<%=todt%>');
        var tyear = tdate.getFullYear();
        var tmonth = tdate.getMonth() + 1;
        var strt = monthNames[parseInt(fmonth, 10) - 1];
        var end = monthNames[parseInt(tmonth, 10) - 1];
        var prdcat = '<%=Prdcat%>'.split(',');
        strhead = "<tr>"
        strhead += "<td rowspan='2' colspan='5'>NEC- MANAGER WISE -OUTLET ANALYSIS (" + strt + '`' + year + '-' + end + '`' + tyear + ")</td>";
        if (fmonth < tmonth) {
            for (var i = fmonth; i <= parseInt(tmonth); i++) {
                strhead += "<td colspan='" + prdcat.length + "'>" + monthNames[parseInt(i, 10)- 1] + "</td><td rowspan='2'>Fous products Value</td><td rowspan='2'>Total Value</td>";
            }
            strhead += "<td rowspan='2'>Fous products Value</td><td rowspan='2'>Total Value</td><td>Total Value-Focus products Value</td>";
            strhead += "</tr>"
            strhead += "<tr>"
            for (var i = fmonth; i <= parseInt(tmonth); i++) {
                for (var j = 0; j < prdcat.length; j++) {
                    Prd = PrdCatList.filter(function (a) {
                        return a.Product_Cat_Code == prdcat[j]
                    })
                    strhead += "<td>" + Prd[0].Product_Cat_SName + "</td>";
                }
            }
            strhead += "</tr>"
            strhead += "<tr>"
            for (var i = fmonth; i <= parseInt(tmonth); i++) {
                if (i == fmonth)
                    strhead += "<td>Manager</td><td>SO NAME</td><td>BEAT</td><td>Retailer Code</td><td>Retailer Name</td>";
                for (var k = 0; k < prdcat.length; k++) {
                    strhead += "<td>Qty (pcs)</td>"
                }
                strhead += "<td>FP Value</td><td>ALL PRO Value</td>";
            }

            strhead += "<td>FP Value</td><td>ALL PRO Value</td><td>Diff in Value</td>";
            strhead += "</tr>"
        }
        else {
            strhead += "<td colspan='" + prdcat.length + "'>" + monthNames[parseInt(tmonth, 10) - 1] + "</td><td rowspan='2'>Fous products Value</td><td rowspan='2'>Total Value</td>";
            strhead += "<td rowspan='2'>Fous products Value</td><td rowspan='2'>Total Value</td><td rowspan='2'>Total Value-Focus products Value</td>";

            strhead += "</tr>"
            strhead += "<tr>"
            for (var i = 0; i < prdcat.length; i++) {
                Prd = PrdCatList.filter(function (a) {
                    return a.Product_Cat_Code == prdcat[i]
                })
                strhead += "<td>" + Prd[0].Product_Cat_SName + "</td>";
            }

            strhead += "</tr>"
            strhead += "<tr>"
            strhead += "<td>Manager</td><td>SO NAME</td><td style='max-width:50px;'>BEAT</td><td>Retailer Code</td><td>Retailer Name</td>"
            for (var i = 0; i < prdcat.length; i++) {
                strhead += "<td>Qty (pcs)</td>"
            }
            strhead += "<td>FP Value</td><td>ALL PRO Value</td>";
            strhead += "<td>FP Value</td><td>ALL PRO Value</td><td>Diff in Value</td>";
            strhead += "</tr>"
        }
        $("#cnt_retailerdet thead").append(strhead);

        $("#cnt_retailerdet tbody").html("");
        CustCode = '';
        AnalysisSales.sort((a, b) => b.Cust_Code - a.Cust_Code);
        for (var i = 0; i < AnalysisSales.length; i++) {
            Analysis = {};
            Analysis = AnalysisSales[i];
            if (AnalysisSales[i].Cust_Code != CustCode) {
                CustCode = AnalysisSales[i].Cust_Code;
                strbody += "<tr>";
                strbody += "<td>" + AnalysisSales[i].RptNm + "</td><td>" + AnalysisSales[i].Sf_Name + "</td><td class='truncate' title='"+AnalysisSales[i].Territory_Name+"'  >" + AnalysisSales[i].Territory_Name + "</td><td>" + AnalysisSales[i].Cust_Code + "</td><td>" + AnalysisSales[i].ListedDr_Name + "</td>";//style='text-overflow:ellipsis;max-width:25px;white-space:nowrap;overflow:hidden;'
                if (fmonth < tmonth) {
                    OvrTOt = 0;
                    Ovrlltot = 0;
                    for (var l = fmonth; l <= parseInt(tmonth); l++) {
                        Otot = 0;
                        tot = 0;
                        totval = 0;
                        for (var j = 0; j < prdcat.length; j++) {
                            AnalysisFltr = AnalysisSales.filter(function (a) {
                                return a.Product_Cat_Code == prdcat[j] && a.mnth == l && Analysis.Cust_Code == a.Cust_Code && a.sf_code == Analysis.sf_code;
                            });
                            if (AnalysisFltr.length > 0) {
                                for (var k = 0; k < AnalysisFltr.length; k++) {
                                    tot += parseFloat(AnalysisFltr[k].Quantity);
                                    totval += parseFloat(AnalysisFltr[k].value);
                                }
                            }
                            strbody += "<td>" + tot + "</td>";
                            tot = 0;
                        }
                        FltrCat = CatDet.filter(function (a) {
                            return a.Order_Date == l && Analysis.Cust_Code == a.cust_code && a.sf_code == Analysis.sf_code;
                        });
                        if (FltrCat.length > 0) {
                            for (var m = 0; m < FltrCat.length; m++) {
                                Otot += parseFloat(FltrCat[m].value);
                            }
                        }
                        strbody += "<td>" + totval.toFixed(2) + "</td><td>" + Otot.toFixed(2) + "</td>";
                        OvrTOt += Otot;
                        Ovrlltot += totval;
                    }
                    strbody += "<td>" + Ovrlltot.toFixed(2) + "</td><td>" + OvrTOt.toFixed(2) + "</td><td>" + (OvrTOt - Ovrlltot).toFixed(2) + "</td>";

                }
                else {
                    Ovrlltot = 0;
                    Otot = 0;
                    tot = 0;
                    totval = 0;
                    for (var j = 0; j < prdcat.length; j++) {
                        AnalysisFltr = AnalysisSales.filter(function (a) {
                            return a.Product_Cat_Code == prdcat[j] && a.mnth == tmonth && Analysis.Cust_Code == a.Cust_Code && a.sf_code == Analysis.sf_code;
                        });

                        if (AnalysisFltr.length > 0) {

                            for (var k = 0; k < AnalysisFltr.length; k++) {
                                tot += parseFloat(AnalysisFltr[k].Quantity);
                                totval += parseFloat(AnalysisFltr[k].value);
                            }
                        }
                        strbody += "<td>" + tot + "</td>";
                    }
                    FltrCat = CatDet.filter(function (a) {
                        return a.Order_Date == tmonth && Analysis.Cust_Code == a.cust_code && a.sf_code == Analysis.sf_code;
                    });
                    if (FltrCat.length > 0) {
                        for (var m = 0; m < FltrCat.length; m++) {
                            Otot += parseFloat(FltrCat[m].value);
                        }
                    }
                    strbody += "<td>" + totval + "</td><td>" + Otot.toFixed(2) + "</td>";
                    Ovrlltot += totval;
                    strbody += "<td>" + Ovrlltot + "</td><td>" + Otot.toFixed(2) + "</td><td>" + (Otot - totval).toFixed(2) + "</td>";
                }
                
            }
        }
        strbody += "</tr>";
        $("#cnt_retailerdet tbody").append(strbody);
        tot = 0; totval = 0; Ovrlltot = 0;
    }


</script>
<body>
    <div class="spinnner_div" style="display: none; width: 100%">
        <div class="spinner" style="position: absolute; left: 525px; top: 133px;">
            <div class="rect1" style="background: #1a60d3;"></div>
            <div class="rect2" style="background: #DB4437;"></div>
            <div class="rect3" style="background: #F4B400;"></div>
            <div class="rect4" style="background: #0F9D58;"></div>
            <div class="rect5" style="background: orangered;"></div>
        </div>
    </div>
    <form id="form1" runat="server">
        <div>
            <div>
                <div class="row m-0" style="padding: 30px">
                    <h4>Customer Wise Sales Analysis</h4>
                    <div class="table table-responsive col-10" style="height: 640px;">
                        <table id="cnt_retailerdet" style="border: 1px solid gray !important; min-width: 100%;" class="table table-bordered table-hover grids">
                            <thead>
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
        </div>
    </form>
</body>
</html>
