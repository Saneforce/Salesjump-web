<%@ Page Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="newtpreport.aspx.cs" Inherits="MIS_Reports_newtpreport" %>

<asp:Content ID="Content1" class=".content" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        #grid {
            border: 1px solid #ddd;
            border-collapse: collapse;
            width: 80%;
        }

        th {
            position: sticky;
            top: 0;
            background: #177a9e;
            text-align: center;
            font-weight: normal;
            font-size: 15px;
            color: white;
        }

        .a {
            line-height: 22px;
            padding: 3px 4px;
            border-radius: 7px;
        }

        table td, table th {
            padding: 5px;
            border: 1px solid #ddd;
             text-align: center;
        }
        .gosubmit {
            display: inline-block;
            margin: 0;
            cursor: pointer;
            border: 1px solid #bbb;
            overflow: visible;
            font: bold 13px arial, helvetica, sans-serif;
            text-decoration: none;
            white-space: nowrap;
            color: #555;
            border-radius: 11px;
            background-color: #ddd;
            width: 5%;
            height: 26px;
        }
        input[type='text'], select {
            line-height: 22px;
            padding: 6px 21px;
            border: solid 1px #bbbaba;
            border-radius: 7px;
        }
        .gosubmit {
            background-color: #177a9e;
            color: white;
            font-weight: 500;
            display: inline-block;
            font-size: 14px;
            font-weight: normal;
            line-height: 1.428571429;
            text-align: center;
            white-space: nowrap;
            vertical-align: middle;
            cursor: pointer;
            border-radius: 3px;
            border: 1px solid transparent;
            width: 6%;
        }
    </style>
    <form id="formid" class="form-horizontal" runat="server">
        <center>
            <div id="tblMargin" style="margin-top: 35px;">
                <div>
                    <label for="inputcomname">Field Force Name</label>
                    <select id="ddlcomname">
                        <option value="0">--Select Field Force Name--</option>
                    </select>
                </div>
                 <div>
                    <label for="inputdisname">Distributor Name</label>
                    <select id="distname">
                        <option value="0">--Select Distributor Name--</option>
                    </select>
                </div>

                <div>

                    <label for="month">Month</label>
                    <select id="fltpmnth" data-size="5" data-dropup-auto="false">
                        <option value="0">Select Month</option>
                        <option value="1">January</option>
                        <option value="2">February</option>
                        <option value="3">March</option>
                        <option value="4">April</option>
                        <option value="5">May</option>
                        <option value="6">June</option>
                        <option value="7">July</option>
                        <option value="8">August</option>
                        <option value="9">September</option>
                        <option value="10">October</option>
                        <option value="11">November</option>
                        <option value="12">December</option>
                    </select>
                    <label for="year">Year</label>
                    <select id="fltpyr" data-size="5" data-dropup-auto="false">
                    </select>
                </div>
                <button type="button" class="gosubmit" id="gosubmit" >View</button>
                 <img  style="cursor: pointer;float:right;" src="/img/pdf.png" alt="" width="40" height="40" id="btnpdf">
            <img style="cursor: pointer;float:right;" src="/img/excel.png" alt="" width="40" height="40" id="btnExport">

            </div>
            <br />
            <br />
            <table class="grids" id="grid" style='display: none'>
                <thead>
                    <tr>
                        <th rowspan="2">S.No</th>
                        <th rowspan="2">Product</th>
                        <th rowspan="2">Price</th>
                        <th colspan="2">OP</th>
                        <th colspan="2">Purchase</th>
                        <th colspan="2">Sales</th>
                        <th colspan="2">Return</th>
                        <th colspan="2">Closing</th>
                    </tr>
                    <tr>

                        <th>Qty</th>
                        <th>Free</th>
                        <th>Qty</th>
                        <th>Free</th>
                        <th>Qty</th>
                        <th>Free</th>
                        <th>Qty</th>
                        <th>Free</th>
                        <th>Qty</th>
                        <th>Free</th>
                    </tr>
                </thead>
                <tbody></tbody>
            <tfoot></tfoot>
            </table>
        </center>
        <br />
        <br />

    </form>
    <script type="text/javascript" src="../js/plugins/datatables/jquery.dataTables.js"></script>
    <script type="text/javascript" src="../js/plugins/datatables/dataTables.bootstrap.js"></script>
     <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/jspdf/1.3.3/jspdf.min.js"></script>
    <script type="text/javascript" src="https://html2canvas.hertzen.com/dist/html2canvas.js"></script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript">
        var str = "";
        var arr = [];
        var prods = [];
        var clos = [];
       
        $(document).ready(function () {
            fillfilter();
            fillTpYR();
           
       
            $('.gosubmit').on('click', function () {
               
                $("#grid").show();
                $('#grid tbody').html('');
                var fieldforce = $("#ddlcomname").val();
                var disname = $("#distname").val();
                var month = $("#fltpmnth").val();
                var year = $("#fltpyr").val();
                if ($("#fltpmnth").val() == 0) {
                    alert("Please Select Month");
                    return false;
                }
                if ($("#fltpyr").val() == 0) {
                    alert("Please Select Year");
                    return false;
                }
                $.ajax({
                    type: "Post",
                    contentType: "application/json; charset=utf-8",
                    url: "newtpreport.aspx/clsdata",
                    data: "{'ffc':'" + fieldforce + "','emonth':'" + month + "','eyear':'" + year + "','distname':'" + disname + "'}",
                    dataType: "json",
                    success: function (data) {
                        clos = JSON.parse(data.d);
                        FillProduct();
                        
                    }
                });

            });
        });
       
        function fillfilter() {

            $.ajax({
                type: "Post",
                contentType: "application/json; charset=utf-8",
                url: "newtpreport.aspx/GetMR",
                dataType: "json",
                success: function (data) {
                    $.each(data.d, function (data, value) {

                        $('#ddlcomname').append($("<option></option>").val(this['Value']).html(this['Text']));
                    });
                },
                error: function (result) {
                    alert(JSON.stringify(result));
                }
            });


        };
        $('#ddlcomname').change(function () {

            var fieldforce = $('#ddlcomname :selected').val();
            //var fieldforce = $("#ddlcomname").val();
            $.ajax({
                type: "Post",
                contentType: "application/json; charset=utf-8",
                url: "newtpreport.aspx/distname",
                data: "{ 'fieldforcecode':'" + fieldforce + "'}",
                dataType: "json",
                success: function (data) {
                    $.each(data.d, function (data, value) {

                        $('#distname').append($("<option></option>").val(this['Value']).html(this['Text']));
                    });
                },
                error: function (result) {
                    alert(JSON.stringify(result));
                }
            });


        });
        function fillTpYR() {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "newtpreport.aspx/BindDate",
                data: "{'divcode':'<%=Session["div_code"]%>'}",
                dataType: "json",
                success: function (data) {
                    var tpyr = $("#fltpyr");
                    tpyr.empty().append('<option value="0">Select Year</option>');
                    for (var i = 0; i < data.d.length; i++) {
                        tpyr.append($('<option value="' + data.d[i].value + '">' + data.d[i].text + '</option>'));
                    };
                }
            });
        };
        function FillProduct() {
            var opprice = 0;
            var purprice = 0;
            var salprice = 0;
            var clprice = 0;
            var reprice = 0;
            var opfprice = 0;
            var purfprice = 0;
            var salfprice = 0;
            var clfprice = 0;
            var refprice = 0;
            var fieldforce = $("#ddlcomname").val();
           
            $.ajax({
                type: "Post",
                contentType: "application/json; charset=utf-8",
                url: "newtpreport.aspx/FillProd",
                data: "{'ffc':'" + fieldforce + "'}",
                dataType: "json",
                success: function (data) {
                    $('#grid tbody').html('');
                    prods = JSON.parse(data.d);
                    str = '';
                    for (var i = 0; i < prods.length; i++) {
                        var flit = clos.filter(function (a) {
                            return a.Product_Detail_Code == prods[i].Product_Detail_Code;
                        });
                        opprice += flit[0].optpr
                        purprice += flit[0].purtpr
                        salprice += flit[0].satpr
                        clprice += flit[0].cltpr
                        reprice += flit[0].reqpr 
                        opfprice += flit[0].ofrpr
                        purfprice += flit[0].pfrpr
                        salfprice += flit[0].sfrpr
                        clfprice += flit[0].cfrpr 
                        refprice += flit[0].refrpr
                        str += "<tr><td>" + (i + 1) + "</td><td>" + prods[i].Product_Detail_Name + "</td><td>" + flit[0].Distributor_Price + "</td><td>" + flit[0].cqty + "</td><td>" + flit[0].cfree + "</td><td>" + flit[0].pqty + "</td><td>" + flit[0].pfree + "</td><td>" + flit[0].sqty + "</td><td>" + flit[0].sfree + "</td><td>" + flit[0].re_qty + "</td><td>" + flit[0].re_free + "</td><td>" + flit[0].clqty + "</td><td>" + flit[0].clfree + "</td></tr >";
                    }
                    $('#grid tbody').append(str);
                    $('#grid tfoot').html("<tr><td colspan=3 style='text-align: center;font-weight: bold'>Total  Value</td><td><label id='otot'>" + opprice + "</label></td><td><label id='oftot' name='oftot'>" + opfprice + "</label></td>><td><label id='ptot' name='ptot'>" + purprice + "</label></td><td><label id='pftot' name='pftot'>" + purfprice + "</label></td><td><label  id='stot' name='stot'>" + salprice + "</label></td><td><label id='sftot' name='sftot'>" + salfprice + "</label></td><td><label id='retot' name='retot'>" + reprice + "</label></td><td><label id='reftot'>" + refprice +"</label></td><td><label id='cltot' name='cltot'>" + clprice + "</label></td><td><label id='clftot'>" + clfprice + "</label></td></tr>");

                }
            });
        }
        $('#btnpdf').click(function () {

            var HTML_Width = $(".grids").width();
            var HTML_Height = $(".grids").height();
            var top_left_margin = 15;
            var PDF_Width = HTML_Width + (top_left_margin * 2);
            var PDF_Height = (PDF_Width * 1.5) + (top_left_margin * 2);
            var canvas_image_width = HTML_Width;
            var canvas_image_height = HTML_Height;

            var totalPDFPages = Math.ceil(HTML_Height / PDF_Height) - 1;


            html2canvas($(".grids")[0], { allowTaint: true }).then(function (canvas) {
                canvas.getContext('2d');

                console.log(canvas.height + "  " + canvas.width);


                var imgData = canvas.toDataURL("image/jpeg", 1.0);
                var pdf = new jsPDF('p', 'pt', [PDF_Width, PDF_Height]);
                pdf.addImage(imgData, 'JPG', top_left_margin, top_left_margin, canvas_image_width, canvas_image_height);


                for (var i = 1; i <= totalPDFPages; i++) {
                    pdf.addPage(PDF_Width, PDF_Height);
                    pdf.addImage(imgData, 'JPG', top_left_margin, -(PDF_Height * i) + (top_left_margin * 4), canvas_image_width, canvas_image_height);
                }

                pdf.save("orderdetails.pdf");
            });
        });
        $('#btnExport').click(function () {

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
            htmls = document.getElementById("grid").innerHTML;


            var ctx = {
                worksheet: 'Worksheet',
                table: htmls
            }
            var link = document.createElement("a");
            var tets = 'orderview' + '.xls';   //create fname

            link.download = tets;
            link.href = uri + base64(format(template, ctx));
            link.click();
        });
    </script>
</asp:Content>