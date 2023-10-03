<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="Brandwise_Sales.aspx.cs" Inherits="MIS_Reports_Brandwise_Sales" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        .table-bordered > thead > tr > th, .table-bordered > tbody > tr > th, .table-bordered > tfoot > tr > th, .table-bordered > thead > tr > td, .table-bordered > tbody > tr > td, .table-bordered > tfoot > tr > td {
            border: 1px solid #b4b4b4;
        }
    </style>
    <style media="print">
        .printtab {
            page-break-after: always;
        }
    </style>
    <form runat="server">
        <input type="hidden" id="fdtt" />
        <input type="hidden" id="tdtt" />
        <div class="row">
            <div class="col-lg-12 sub-header">
                Sales Brandwise
                 <span style="float: right; margin-right: 15px;">
                     <button type="button" class="btn btn-primary btn" id="btngo">Go</button>
                 </span>
                <span style="float: right; margin-right: 15px;">
                    <div id="reportrange" class="form-control mt-5 mb-5" style="background: #fff; cursor: pointer; padding: 5px 10px; border: 1px solid #ccc;">
                        <i class="fa fa-calendar"></i>&nbsp;
                <span id="ordDate"></span><i class="fa fa-caret-down"></i>
                    </div>
                </span>
                <span style="float: right; margin-right: 15px;">
                    <div>
                        <select class="form-control" id="ddlsf"></select>
                    </div>
                </span>

            </div>
        </div>
        <center>
            <div class="row" style="margin-top: 1rem;">
            <select id="ddlcust" style="float: left;margin-left: 16px;"></select>
                <img onclick="exportToExcel()"
                     style="cursor: pointer;float:right;" src="../../img/Excel-icon.png"
                     alt="" width="40" height="40" id="btnExport" />
                <img onclick="exportToprint()"
                        style="cursor: pointer;float:right;" src="../../img/print.png"
                        alt="" width="40" height="40" id="btnprint" />
                <img onclick="getPDF()"
                        style="cursor: pointer;float:right;" src="../../img/pdf.png"
            alt="" width="40" height="40" id="btnpdf" />
                </div>
        <div id="expex" class="canvas_div_pdf">
        </div>
        </center>
    </form>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jspdf/1.3.3/jspdf.min.js"></script>
    <script src="https://html2canvas.hertzen.com/dist/html2canvas.js"></script>
    <script type="text/javascript">
        let sf = '';
        let FDt = '';
        let TDT = '';
        let AllPGrp = [], AllOrders = [], Orders = [], AllCust = [], AllUOM = [], SNJGrp = [], CMPGRp = [], BGRp = [], UOMs = [], BUOMs = [], FAllCust = [];

        function exportToprint() {
            // var divToPrint = document.getElementById("expex");
            //newWin = window.open("");
            // newWin.document.write(divToPrint.innerHTML);
            // newWin.print();
            // newWin.close();
            var originalContents = $("body").html();
            var printContents = $("#expex").html();
            $("body").html(printContents);
            window.print();
            $("body").html(originalContents);
            return false;
        }
        function exportToExcel() {
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
            htmls = document.getElementById("expex").innerHTML;

            var ctx = {
                worksheet: 'Worksheet',
                table: htmls
            }
            var link = document.createElement("a");
            var tets = 'categorywisesales' + '.xls';   //create fname

            link.download = tets;
            link.href = uri + base64(format(template, ctx));
            link.click();
        }
        function getPDF() {

            var HTML_Width = $(".canvas_div_pdf").width();
            var HTML_Height = $(".canvas_div_pdf").height();
            var top_left_margin = 15;
            var PDF_Width = HTML_Width + (top_left_margin * 2);
            var PDF_Height = (PDF_Width * 1.5) + (top_left_margin * 2);
            var canvas_image_width = HTML_Width;
            var canvas_image_height = HTML_Height;

            var totalPDFPages = Math.ceil(HTML_Height / PDF_Height) - 1;


            html2canvas($(".canvas_div_pdf")[0], { allowTaint: true }).then(function (canvas) {
                canvas.getContext('2d');

                console.log(canvas.height + "  " + canvas.width);


                var imgData = canvas.toDataURL("image/jpeg", 1.0);
                var pdf = new jsPDF('p', 'pt', [PDF_Width, PDF_Height]);
                pdf.addImage(imgData, 'JPG', top_left_margin, top_left_margin, canvas_image_width, canvas_image_height);


                for (var i = 1; i <= totalPDFPages; i++) {
                    pdf.addPage(PDF_Width, PDF_Height);
                    pdf.addImage(imgData, 'JPG', top_left_margin, -(PDF_Height * i) + (top_left_margin * 4), canvas_image_width, canvas_image_height);
                }

                pdf.save("categorywisesales.pdf");
            });
        }
        function ReloadTable() {
            $('#expex').html('');
            for ($i = 0; $i < FAllCust.length; $i++) {
                let divs = $('<div class="card"></div>');
                let pdivs = $('<p class="printtab" style=" page-break-after: always;"></p>');
                let rdiv = $('<div class="row"></div>');
                let pdiv2 = $('<table></table>');
                let pdiv3 = $('<tbody></tbody>');
                let pdiv1 = $('<tr></tr>');
                let div1 = $('<td></td>');
                let div2 = $('<td valign="top"></td>');
                let tab1 = $('<table style="width: 100%;"></table>');
                let tbody1 = $('<tbody></tbody>');
                let tr6 = '<tr><td>Name of TSO : ' + ($('#ddlsf :selected').text()) + '</td><td>' + FAllCust[$i].ListedDr_Name + '</td></tr>';
                tbody1.html(tr6);
                tab1.html(tbody1);
                $(divs).append(tab1);
                let tab = $('<table class="table-bordered" style="width: 100%;"></table>');
                let tbody = $('<tbody></tbody>');
                let tr1 = `<tr><td align="center" rowspan="3" style="background-color:#0097AC;color:white;">Brand</td><td align="center" colspan="${(UOMs.length) + (UOMs.length)}" style="background-color:#0097AC;color:white;">Date of Visit - ${FAllCust[$i].Order_Date}</td></tr>`;
                let tr2 = `<tr><td align="center" colspan="${(UOMs.length)}" style="background-color:#0097AC;color:white;">Stock</td><td align="center" colspan="${(UOMs.length)}" style="background-color:#0097AC;color:white;">Sales - ${FAllCust[$i].Order_Date}</td></tr>`;
                let tr3 = '<tr>';
                let tr4 = '<tr align="left" valign="middle" style="font-family:calibri;font-size:Small;background-color:LightBlue;font-bold:true; font-size:18px; Color:Red; border-color:Black"><td>Our Brand</td>';

                let btab = $('<table class="table-bordered" style="width: 100%;"></table>');
                let btbody = $('<tbody></tbody>');
                let btr1 = `<tr><td align="center" rowspan="3" style="background-color:#0097AC;color:white;">Brand</td><td align="center" colspan="${(BUOMs.length) + (BUOMs.length)}" style="background-color:#0097AC;color:white;">Date of Visit from ${($('#fdtt').val())} to ${($('#tdtt').val())}</td></tr>`;
                let btr2 = `<tr><td align="center" colspan="${(BUOMs.length)}" style="background-color:#0097AC;color:white;">Stock</td><td align="center" colspan="${(BUOMs.length)}" style="background-color:#0097AC;color:white;">Sales from ${($('#fdtt').val())} to ${($('#tdtt').val())}</td></tr>`;
                let btr3 = '<tr>';
                let btr4 = '<tr align="left" valign="middle" style="font-family:calibri;font-size:Small;background-color:LightBlue;font-bold:true; font-size:18px; Color:Red; border-color:Black"><td>Our Brand</td>';

                let tr5 = '<tr align="left" valign="middle" style="font-family:calibri;font-size:Small;background-color:LightBlue;font-bold:true; font-size:18px; Color:Red; border-color:Black"><td>Competitor Brand</td>';
                var filtUOM = UOMs
                //    AllUOM.filter(function (a) {
                //    return a.Product_Cat_Code != 987;
                //});
                var filtUOM1 = BUOMs;
                //    AllUOM.filter(function (a) {
                //    return a.Product_Cat_Code == 987;
                //});
                for ($j = 0; $j < filtUOM.length; $j++) {
                    tr3 += '<td align="center" style="background-color:#0097AC;color:white;">' + filtUOM[$j].UOM_Weight + '</td>';
                    tr4 += '<td></td>';
                    tr5 += '<td></td>';
                }
                for ($j = 0; $j < filtUOM.length; $j++) {
                    tr3 += '<td align="center" style="background-color:#0097AC;color:white;">' + filtUOM[$j].UOM_Weight + '</td>';
                    tr4 += '<td></td>';
                    tr5 += '<td></td>';
                }
                for ($j = 0; $j < filtUOM1.length; $j++) {
                    btr3 += '<td align="center" style="background-color:#0097AC;color:white;">' + filtUOM1[$j].UOM_Weight + '</td>';
                    btr4 += '<td></td>';
                }
                for ($j = 0; $j < filtUOM1.length; $j++) {
                    btr3 += '<td align="center" style="background-color:#0097AC;color:white;">' + filtUOM1[$j].UOM_Weight + '</td>';
                    btr4 += '<td></td>';
                }
                tr3 += '</tr>';
                tr4 += '</tr>';
                tr5 += '</tr>';
                btr3 += '</tr>';
                btr4 += '</tr>';
                for ($k = 0; $k < SNJGrp.length; $k++) {
                    tr4 += '<tr><td>' + SNJGrp[$k].Product_Grp_Name + '</td>';
                    for ($l = 0; $l < filtUOM.length; $l++) {
                        var filtsstock = Orders.filter(function (a) {
                            return (a.Product_Grp_Code == SNJGrp[$k].Product_Grp_Code) && (a.UOM_Weight == filtUOM[$l].UOM_Weight) && (a.Cust_Code == FAllCust[$i].ListedDrCode);
                        });
                        tr4 += '<td>' + ((filtsstock.length > 0) ? filtsstock[0].ClStock : '') + '</td>';
                    }
                    for ($l = 0; $l < filtUOM.length; $l++) {
                        var filtsstock = Orders.filter(function (a) {
                            return (a.Product_Grp_Code == SNJGrp[$k].Product_Grp_Code) && (a.UOM_Weight == filtUOM[$l].UOM_Weight) && (a.Cust_Code == FAllCust[$i].ListedDrCode);
                        });
                        tr4 += '<td>' + ((filtsstock.length > 0) ? filtsstock[0].Quantity : '') + '</td>';
                    }
                    tr4 += '</tr>';
                }
                for ($k = 0; $k < BGRp.length; $k++) {
                    btr4 += '<tr><td>' + BGRp[$k].Product_Grp_Name + '</td>';
                    for ($l = 0; $l < filtUOM1.length; $l++) {
                        var filtsstock = Orders.filter(function (a) {
                            return (a.Product_Grp_Code == BGRp[$k].Product_Grp_Code) && (a.UOM_Weight == filtUOM1[$l].UOM_Weight) && (a.Cust_Code == FAllCust[$i].ListedDrCode);
                        });
                        btr4 += '<td>' + ((filtsstock.length > 0) ? filtsstock[0].ClStock : '') + '</td>';
                    }
                    for ($l = 0; $l < filtUOM1.length; $l++) {
                        var filtsstock = Orders.filter(function (a) {
                            return (a.Product_Grp_Code == BGRp[$k].Product_Grp_Code) && (a.UOM_Weight == filtUOM1[$l].UOM_Weight) && (a.Cust_Code == FAllCust[$i].ListedDrCode);
                        });
                        btr4 += '<td>' + ((filtsstock.length > 0) ? filtsstock[0].Quantity : '') + '</td>';
                    }
                    btr4 += '</tr>';
                }
                for ($k = 0; $k < CMPGRp.length; $k++) {
                    tr5 += '<tr><td>' + CMPGRp[$k].Product_Grp_Name + '</td>';
                    for ($l = 0; $l < filtUOM.length; $l++) {
                        var filtsstock = Orders.filter(function (a) {
                            return (a.Product_Grp_Code == CMPGRp[$k].Product_Grp_Code) && (a.UOM_Weight == filtUOM[$l].UOM_Weight) && (a.Cust_Code == FAllCust[$i].ListedDrCode);
                        });
                        tr5 += '<td>' + ((filtsstock.length > 0) ? filtsstock[0].ClStock : '') + '</td>';
                    }
                    for ($l = 0; $l < filtUOM.length; $l++) {
                        var filtsstock = Orders.filter(function (a) {
                            return (a.Product_Grp_Code == CMPGRp[$k].Product_Grp_Code) && (a.UOM_Weight == filtUOM[$l].UOM_Weight) && (a.Cust_Code == FAllCust[$i].ListedDrCode);
                        });
                        tr5 += '<td>' + ((filtsstock.length > 0) ? filtsstock[0].Quantity : '') + '</td>';
                    }
                    tr5 += '</tr>';
                }
                let Atr = tr1 + tr2 + tr3 + tr4 + tr5;

                let BAtr = btr1 + btr2 + btr3 + btr4;
                $(tbody).html(Atr);
                $(btbody).html(BAtr);
                //$(tbody).html(tr2);
                //$(tbody).html(tr3);
                //$(tbody).html(tr4);
                //$(tbody).html(tr5);
                $(tab).html(tbody);
                $(btab).html(btbody);
                $(div1).append(tab);
                $(div2).append(btab);
                $(pdiv1).append(div1);
                $(pdiv1).append(div2);
                $(pdiv3).append(pdiv1);
                $(pdiv2).append(pdiv3)
                $(divs).append(pdiv2);
                $('#expex').append(divs);
                $('#expex').append(pdivs);
            }
        }
        function getProductGroup() {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "Brandwise_Sales.aspx/getProductGroup",
                data: "{'Div':'<%=Session["Division_Code"]%>'}",
                dataType: "json",
                success: function (data) {
                    AllPGrp = JSON.parse(data.d) || [];
                    SNJGrp = AllPGrp.filter(function (a) {
                        return a.Product_Cat_Code == 937;
                    });
                    CMPGRp = AllPGrp.filter(function (a) {
                        return a.Product_Cat_Code == 938;
                    });
                    BGRp = AllPGrp.filter(function (a) {
                        return a.Product_Cat_Code == 987;
                    });
                },
                error: function (result) {
                }
            });
        }
        function getUOM() {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "Brandwise_Sales.aspx/getUOM",
                data: "{'Div':'<%=Session["Division_Code"]%>'}",
                dataType: "json",
                success: function (data) {
                    AllUOM = JSON.parse(data.d) || [];
                    var pre = '';
                    for ($i = 0; $i < AllUOM.length; $i++) {
                        if (AllUOM[$i].Product_Cat_Code != 987 && pre != AllUOM[$i].UOM_Weight) {
                            UOMs.push({
                                UOM_Weight: AllUOM[$i].UOM_Weight
                            });
                            pre = AllUOM[$i].UOM_Weight;
                        }
                    }
                    pre = '';
                    for ($i = 0; $i < AllUOM.length; $i++) {
                        if (AllUOM[$i].Product_Cat_Code == 987 && pre != AllUOM[$i].UOM_Weight) {
                            BUOMs.push({
                                UOM_Weight: AllUOM[$i].UOM_Weight
                            });
                            pre = AllUOM[$i].UOM_Weight;
                        }
                    }
                },
                error: function (result) {
                }
            });
        }
        function getCustomers() {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "Brandwise_Sales.aspx/getCust",
                data: "{'Div':'<%=Session["Division_Code"]%>', 'SF':'" + sf + "', 'FDt':'" + FDt + "', 'TDt':'" + TDT + "'}",
                dataType: "json",
                success: function (data) {
                    AllCust = JSON.parse(data.d) || [];
                    FAllCust = AllCust;
                    $('#ddlcust').empty().append('<option value="">Select</option>');
                    for (var i = 0; i < AllCust.length; i++) {
                        $('#ddlcust').append('<option value="' + AllCust[i].ListedDrCode + '">' + AllCust[i].ListedDr_Name + '</option>')
                    }
                },
                error: function (result) {
                }
            });
        }
        function getSalesStock() {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "Brandwise_Sales.aspx/getSalesStock",
                data: "{'Div':'<%=Session["Division_Code"]%>', 'SF':'" + sf + "', 'FDt':'" + FDt + "', 'TDt':'" + TDT + "','custcode':''}",
                dataType: "json",
                success: function (data) {
                    AllOrders = JSON.parse(data.d) || [];
                    Orders = AllOrders;
                },
                error: function (result) {
                }
            });
        }
        function fillsf() {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "Brandwise_Sales.aspx/GetSF",
                data: "{'Div':'<%=Session["Division_Code"]%>'}",
                dataType: "json",
                success: function (data) {
                    var sfdets = JSON.parse(data.d) || [];
                    var ddsf = $('#ddlsf');
                    ddsf.empty().append('<option value="">Select</option>');
                    for (var i = 0; i < sfdets.length; i++) {
                        if (sfdets[i].sf_type == 1) {
                            ddsf.append($('<option value="' + sfdets[i].Sf_Code + '">' + sfdets[i].Sf_Name + '</option>'));
                        }
                    }
                },
                error: function (result) {
                }
            });
        }
        function loadData() {
            $('#expex').html('')
            AllPGrp = [], AllOrders = [], Orders = [], AllCust = [], AllUOM = [], SNJGrp = [], CMPGRp = [], BGRp = [], UOMs = [], BUOMs = []
            sf = $('#ddlsf').val();
            let id = $('#fdtt').val();
            id = id.split('-');
            FDt = id[2].trim() + '-' + id[1] + '-' + id[0] + ' 00:00:00';
            id = $('#tdtt').val();
            id = id.split('-');
            TDT = id[2].trim() + '-' + id[1] + '-' + id[0] + ' 00:00:00';
            getUOM();
            getCustomers();
            getProductGroup();
            getSalesStock();
            ReloadTable();
        }
        $(document).ready(function () {
            fillsf();
            $('#btngo').on('click', function () {
                loadData();;
            });
            $('#ddlcust').on('change', function () {
                if ($('#ddlcust').val() != '') {
                    getProductGroup();
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        async: false,
                        url: "Brandwise_Sales.aspx/getSalesStock",
                        data: "{'Div':'<%=Session["Division_Code"]%>', 'SF':'" + sf + "', 'FDt':'" + FDt + "', 'TDt':'" + TDT + "','custcode':'" + $('#ddlcust').val() + "'}",
                        dataType: "json",
                        success: function (data) {
                            AllOrders = JSON.parse(data.d) || [];
                            Orders = AllOrders;
                        },
                        error: function (result) {
                        }
                    });
                    FAllCust = AllCust.filter(function (a) {
                        return a.ListedDrCode == $('#ddlcust').val();
                    });
                    ReloadTable();
                }
                else {
                    loadData();
                }
            })
            //loadData(sf);
            //$('#ddlsf').on('change', function () {
            //    sf = $(this).val();
            //    Orders = AllOrders;
            //    if (sf == 'admin' || sf == '0') {
            //        Orders = AllOrders;
            //    }
            //    else {
            //        Orders = Orders.filter(function (a) {
            //            return a.SF_Code == sf;
            //        });
            //    }
            //    ReloadTable();
            //})
        });
    </script>
    <script type="text/javascript">
        $(function () {

            var start = moment();
            var end = moment();

            function cb(start, end) {
                $('#reportrange span').html(start.format('DD-MM-YYYY') + ' - ' + end.format('DD-MM-YYYY'));
                $('#fdtt').val(start.format('DD-MM-YYYY'));
                $('#tdtt').val(end.format('DD-MM-YYYY'));
            }

            $('#reportrange').daterangepicker({
                startDate: start,
                endDate: end,
                ranges: {
                    'Today': [moment(), moment()],
                    'Yesterday': [moment().subtract(1, 'days'), moment().subtract(1, 'days')],
                    'Last 7 Days': [moment().subtract(6, 'days'), moment()],
                    'Last 30 Days': [moment().subtract(29, 'days'), moment()],
                    'This Month': [moment().startOf('month'), moment().endOf('month')],
                    'Last Month': [moment().subtract(1, 'month').startOf('month'), moment().subtract(1, 'month').endOf('month')]
                }
            }, cb);

            cb(start, end);

        });
    </script>
</asp:Content>

