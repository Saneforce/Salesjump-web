<%@ Page Title="" Language="C#" MasterPageFile="~/Master_SS.master" AutoEventWireup="true" CodeFile="SS_Sales_by_Invoice_Report.aspx.cs" Inherits="SuperStockist_Reports_Sales_SS_Sales_by_Invoice_Report" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="content-type" content="text/plain; charset=UTF-8" />
    <title></title>
    <link href="../../../../css/bootstrap-3.3.2.min.css" rel="stylesheet" />
    <link href="../../../../css/style.css" rel="stylesheet" />
    <link href="../../../../css/chosen.css" rel="stylesheet" />
    <style type="text/css">
        .newStly td
        {
            border-collapse: collapse;
            padding-top: 4px;
            padding-bottom: 4px;
            padding-left: 4px;
            padding-right: 4px;
            font-size: 12px;
        }
        .newStly th {
            background-color: #428bca;
            color: white;
        }
        table {
            border-collapse: collapse;
            text-indent: initial;
            border-spacing: 2px;
        }
        
        .num2
        {
            text-align: right;
        }
    </style>
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
     
    <script type="text/javascript">
        bindArray = [];
        $i = 0;
        $(document).ready(function () {
           $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "SS_Sales_by_Invoice_Report.aspx/Get_sales_data",
                data: "{}",
                dataType: "json",
                success: function (data) {
                    DCR_ProdDts = JSON.parse(data.d) || [];
                    bindArray = DCR_ProdDts;
                    ReloadTable();
                },
                error: function (result) {
                    //alert(JSON.stringify(result));
                }
            });  
            $(".AAA").click(function () {
                event.preventDefault();
                window.open($(this).attr("href"), "popupWindow", "width=600,height=600,scrollbars=yes");

            });
        });
        function ReloadTable() {      
            $("#OrderList TBODY").html("");          
            if ($i < bindArray.length) {   
                for ($i; $i < bindArray.length; $i++) {
                    var tax = bindArray[$i].tax;
                    slno = $i + 1;
                    var rwStr = "";                     
                        rwStr += "<tr><td>" + slno + "</td><td>" + bindArray[$i].billdate +"</td><td><a  class='AAA' href='SS_Sales_Invoice_Products.aspx?order_id="+bindArray[$i].billno +"'>"+ bindArray[$i].billno + "</a></td><td>" + bindArray[$i].Stockist_Name + "</td><td>"+ bindArray[$i].ListedDr_Name + "</td><td>"+ parseFloat(bindArray[$i].gross).toFixed(2)+ "</td><td>"+ bindArray[$i].discount + "</td><td>"+ tax.toFixed(2) +"</td><td>"+ parseFloat(bindArray[$i].Total).toFixed(2) +"</td></tr>";
                    $("#OrderList tbody").append(rwStr);
                }                    
            }               
        }


        $(document).on('click', '#btnExcel', function (e) {
                var data_type = 'data:application/vnd.ms-excel,' + encodeURIComponent($('#divtable').html());
                var a = document.createElement('a');
                a.href = data_type;

                a.download = 'SalesInvoiceReport.xls';               
                a.click();
                e.preventDefault();
            });
        
    </script>
    <script type="text/javascript">
        var array1 = new Array();
        var array2 = new Array();
        var n = 2; //Total table
        for (var x = 1; x <= n; x++) {
            array1[x - 1] = x;
            array2[x - 1] = x + 'th';
        }

        var tablesToExcel = (function () {
            var uri = 'data:application/vnd.ms-excel;base64,'
        , template = '<html xmlns:o="urn:schemas-microsoft-com:office:office" xmlns:x="urn:schemas-microsoft-com:office:excel" xmlns="http://www.w3.org/TR/REC-html40"><head><!--[if gte mso 9]><xml><x:ExcelWorkbook><x:ExcelWorksheets>'
        , templateend = '</x:ExcelWorksheets></x:ExcelWorkbook></xml><![endif]--></head>'
        , body = '<body>'
        , tablevar = '<table>{table'
        , tablevarend = '}</table>'
        , bodyend = '</body></html>'
        , worksheet = '<x:ExcelWorksheet><x:Name>'
        , worksheetend = '</x:Name><x:WorksheetOptions><x:DisplayGridlines/></x:WorksheetOptions></x:ExcelWorksheet>'
        , worksheetvar = '{worksheet'
        , worksheetvarend = '}'
        , base64 = function (s) { return window.btoa(unescape(encodeURIComponent(s))) }
        , format = function (s, c) { return s.replace(/{(\w+)}/g, function (m, p) { return c[p]; }) }
        , wstemplate = ''
        , tabletemplate = '';

            return function (table, name, filename) {
                var tables = table;

                for (var i = 0; i < tables.length; ++i) {
                    wstemplate += worksheet + worksheetvar + i + worksheetvarend + worksheetend;
                    tabletemplate += tablevar + i + tablevarend;
                }

                var allTemplate = template + wstemplate + templateend;
                var allWorksheet = body + tabletemplate + bodyend;
                var allOfIt = allTemplate + allWorksheet;

                var ctx = {};
                for (var j = 0; j < tables.length; ++j) {
                    ctx['worksheet' + j] = name[j];
                }

                for (var k = 0; k < tables.length; ++k) {
                    var exceltable;
                    if (!tables[k].nodeType) exceltable = document.getElementById(tables[k]);
                    ctx['table' + k] = exceltable.innerHTML;
                }
               
                window.location.href = uri + base64(format(allOfIt, ctx));

            }
        })();
        // onclick="tablesToExcel(array1, array2, 'myfile.xls')"
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="row" style="max-width: 100%; width: 98%">
        <br />
         <div class="col-lg-12 sub-header" style ="font-size:16px;text-align:center;font-weight: bolder;">
                Sales By Retailer Report            
         </div>
        
        <button id="btnExcel" runat="server" align="right"  style="height: 40px; width: 40px; border-width: 0px; position: absolute; right: 105px; top: 43px;" onclick="lnkDownload_Click"><img src="../../img/excel.png"/></button>        
    </div>
    <div id="divtable" style="padding-top: 50px;"> 
        <table id="OrderList" border="1" class="newStly" style="margin-left: 15px;width: 95%;">
            <thead>
                <tr>
                    <th>Sl.No</th>
                    <th>Date</th>
                    <th>InvoiceNo</th>
                    <th>DistributorName</th>
                    <th>RetailerName</th>
                    <th>Gross</th>
                    <th>Discount</th>
                    <th>Tax</th>
                    <th>Amount</th>
                </tr>
            </thead>
            <tbody>
            </tbody>
        </table>
    </div>
    </form>
</body>
</asp:Content>

