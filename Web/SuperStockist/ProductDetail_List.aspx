<%@ Page Title="ProductDetailsList" Language="C#" AutoEventWireup="true"  MasterPageFile="~/Master_DIS.master" CodeFile="ProductDetail_List.aspx.cs" Inherits="MasterFiles_ProductDetail_List" EnableEventValidation="false" %>
<asp:Content ID="Content1"  ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
    <html xmlns="http://www.w3.org/1999/xhtml">
    <head>
    <meta http-equiv="content-type" content="text/plain; charset=UTF-8" />
    <title></title>
    <link type="text/css" rel="stylesheet" href="../css/style.css" /> 
     <style type="text/css">
        .newStly td
        {
            padding-top: 4px;
            padding-bottom: 4px;
            padding-left: 4px;
            padding-right: 4px;
            font-size: 12px;
        }
       </style>
    
    </head>

    <body>
         <form id="form1" runat="server">
    <div class="row">
        <div class="col-lg-12 sub-header">Product List <span style="float:right;display:none">
            <a href="ProductDetail.aspx" class="btn btn-primary btn-update">Add New</a></span>
            <button id="btnExcel" class="btnExcel"  align="right"  style="height: 40px; width: 40px; border-width: 0px;float: right;padding-right: 0px; right: 105px; top: 43px;" >
                <img src="../img/excel.png" /></button>
        </div>                 
    </div>    
    <div class="row hdist" style="margin-top: 24px; margin-left: 4px;">
        <div class="col-lg-4">
            <label id="lblprdbnd">Product Brand</label>
            <select id="ddlbrand" class="form-control hdist" name="distlist" style="width: 207px;height: 29px;">
            </select>
        </div>        
    </div>
    <div class="card">
        <div class="card-body table-responsive">
            <div style="white-space:nowrap">Search&nbsp;&nbsp;<input type="text"  autocomplete="off" id="tSearchOrd" style="width:250px;" />
            <label style="float:right">Show <select class="data-table-basic_length" aria-controls="data-table-basic"><option value="10">10</option><option value="25">25</option><option value="50">50</option><option value="100">100</option></select> entries</label>
            </div>        
            <div id="divtable">
             <table class="table table-hover" id="OrderList">
                <thead class="text-warning">
                    <tr>                          
                        <th style="text-align:left">Sl.No</th>
                        <th id="Product_Detail_Code" style="text-align:left;cursor:pointer">ERP Code</th>
                        <th id="Product_Detail_Name" style="text-align:left;cursor:pointer">Product Name</th>
                        <th id="Product_Brd_Name1" style="text-align:left;cursor:pointer">MRP</th>  
                        <th id="product_unit" style="text-align:left;cursor:pointer">UOM</th>
                        <th id="Hsncode" style="text-align:left;cursor:pointer">HSNCode</th>
                        <th id="Selling_price" style="text-align:left;cursor:pointer">Selling Price</th>
                        </tr>
                </thead>
                <tbody>
                </tbody>
            </table>
            </div>
            <div class="row" style="padding:5px 0px">
                <div class="col-sm-5">
                    <div class="dataTables_info" id="orders_info" role="status" aria-live="polite">Showing 0 to 0 of 0 entries</div>
                </div>
                <div class="col-sm-7">
                    <div class="dataTables_paginate paging_simple_numbers" id="example2_paginate">
                        <ul class="pagination">
                            <li class="paginate_button previous" id="example2_previous"><a href="#" aria-controls="example2" data-dt-idx="0" tabindex="0">Previous</a></li>
                            <li class="paginate_button next" id="example2_next"><a href="#" aria-controls="example2" data-dt-idx="7" tabindex="0">Next</a></li>
                        </ul>
                    </div>
                </div>
            </div>            
        </div>
    </div>
    </form>
    </body>
    <script type="text/javascript" src="../js/plugins/datatables/jquery.dataTables.js"></script>
    <script type="text/javascript" src="../js/plugins/datatables/dataTables.bootstrap.js"></script>
        <script language="javascript" type="text/javascript">
            var AllOrders = []; Allbrands = [];
            var sortid = '';
            var asc = true;
            var Orders = []; pgNo = 1; PgRecords = 10; TotalPg = 0; searchKeys = "Product_Detail_Name,Sale_Erp_Code,Target_Price,product_unit,Product_Sale_Unit,HSN_Code,Distributor_Price";
            $(".data-table-basic_length").on("change",
                function () {
                    pgNo = 1;
                    PgRecords = $(this).val();
                    ReloadTable();
                }
            );
            function loadPgNos() {
                prepg = parseInt($(".paginate_button.previous>a").attr("data-dt-idx"));
                Nxtpg = parseInt($(".paginate_button.next>a").attr("data-dt-idx"));
                $(".pagination").html("");
                TotalPg = parseInt(parseInt(Orders.length / PgRecords) + ((Orders.length % PgRecords) ? 1 : 0)); selpg = 1;
                if (isNaN(prepg)) prepg = 0;
                if (isNaN(Nxtpg)) Nxtpg = 2;
                //  if ((prepg + 1) == pgNo && pgNo > 1) selpg = (parseInt(pgNo) - 1);
                selpg = (pgNo > 7) ? (parseInt(pgNo) + 1) - 7 : 1;
                if ((Nxtpg) == pgNo) {
                    selpg = (parseInt(TotalPg)) - 7;
                    selpg = (selpg > 1) ? selpg : 1;
                }
                spg = '<li class="paginate_button previous" id="example2_previous"><a href="#" aria-controls="example2" data-dt-idx="1" tabindex="0">First</a></li>';
                for (il = selpg - 1; il < selpg + 7; il++) {
                    if (il < TotalPg)
                        spg += '<li class="paginate_button' + ((pgNo == (il + 1)) ? " active" : "") + '"><a href="#" aria-controls="example2" data-dt-idx="' + (il + 1) + '" tabindex="0">' + (il + 1) + '</a></li>';
                }
                spg += '<li class="paginate_button next" id="example2_next"><a href="#" aria-controls="example2" data-dt-idx="' + TotalPg + '" tabindex="0">Last</a></li>';
                $(".pagination").html(spg);

                $(".paginate_button > a").on("click", function () {
                    pgNo = parseInt($(this).attr("data-dt-idx")); ReloadTable();
                    /* $(".paginate_button").removeClass("active");
                     $(this).closest(".paginate_button").addClass("active");*/
                }
                );
            }

            $('th').on('click', function () {
                sortid = this.id;
                asc = $(this).attr('asc');
                if (asc == undefined) asc = 'true';
                Orders.sort(function (a, b) {
                    if (a[sortid].toLowerCase() < b[sortid].toLowerCase() && asc == 'true')
                        return -1;
                    if (a[sortid].toLowerCase() > b[sortid].toLowerCase() && asc == 'true')
                        return 1;

                    if (b[sortid].toLowerCase() < a[sortid].toLowerCase() && asc == 'false')
                        return -1;
                    if (b[sortid].toLowerCase() > a[sortid].toLowerCase() && asc == 'false')
                        return 1;
                    return 0;
                });

                $(this).attr('asc', ((asc == 'true') ? 'false' : 'true'));
                ReloadTable();
            });

            function ReloadTable() {
                $("#OrderList TBODY").html("");
                st = PgRecords * (pgNo - 1);
                for ($i = st; $i < st + PgRecords; $i++) {
                    if ($i < Orders.length) {
                        tr = $("<tr></tr>");
                        //$(tr).html("<td>" + ($i + 1) + "</td><td>" + Orders[$i].Product_Detail_Code + "</td><td>" + Orders[$i].Product_Detail_Name + "</td><td>" + Orders[$i].Product_Brd_Name + "</td><td>" + Orders[$i].Product_Cat_Name + "</td><td>" + Orders[$i].Product_Sale_Unit + "</td><td>" + Orders[$i].product_unit + "</td><td>" + Orders[$i].Sale_Erp_Code + "</td><td>" + Orders[$i].Sample_Erp_Code + "</td><td>" + parseFloat(Orders[$i].RP_Base_Rate).toFixed(2) + "</td><td>" + parseFloat(Orders[$i].RP_Case_Rate).toFixed(2) + "</td>");
                        $(tr).html("<td>" + ($i + 1) + "</td><td>" + Orders[$i].Sale_Erp_Code + "</td><td>" + Orders[$i].Product_Detail_Name + "</td><td>" + parseFloat(Orders[$i].Target_Price).toFixed(2) + "</td><td>" + Orders[$i].product_unit + "/" + Orders[$i].Product_Sale_Unit + "</td><td>" + Orders[$i].HSN_Code + "</td><td>" + parseFloat(Orders[$i].MRP_Price).toFixed(2) + "</td>");
                        $("#OrderList TBODY").append(tr);
                    }
                }
                $("#orders_info").html("Showing " + (st + 1) + " to " + (((st + PgRecords) < Orders.length) ? (st + PgRecords) : Orders.length) + " of " + Orders.length + " entries")
                loadPgNos();
            }

            $("#tSearchOrd").on("keyup", function () {
                if ($(this).val() != "") {
                    shText = $(this).val();
                    Orders = AllOrders.filter(function (a) {
                        chk = false;
                        $.each(a, function (key, val) {
                            if (val != null && val.toString().toLowerCase().indexOf(shText.toLowerCase()) > -1 && (',' + searchKeys).indexOf(',' + key + ',') > -1) {
                                chk = true;
                            }
                        })
                        return chk;
                    })
                }
                else
                    Orders = AllOrders
                pgNo = 1;
                ReloadTable();
            })

            function loadData() {
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    //   async: false,
                    url: "ProductDetail_List.aspx/GetProducts",
                    data: "{'div':'<%=Session["div_code"]%>'}",
                dataType: "json",
                success: function (data) {
                    AllOrders = JSON.parse(data.d) || [];
                    Orders = AllOrders;
                    ReloadTable();
                },
                error: function (result) {
                    //alert(JSON.stringify(result));
                }
            });
            }

            function loadBrands() {
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    url: "ProductDetail_List.aspx/GetProdBrand",
                    data: "{'div':'<%=Session["div_code"]%>'}",
                dataType: "json",
                success: function (data) {
                    Allbrands = JSON.parse(data.d) || [];
                    $("#ddlbrand").empty().append('<option selected="selected" value="0">---Select---</option>');
                    if (Allbrands.length > 0) {                    
                        for (var i = 0; i < Allbrands.length; i++) {
                            $("#ddlbrand").append($('<option value="' + Allbrands[i].Product_Brd_Code + '">' + Allbrands[i].Product_Brd_Name + '</option>'))
                        }
                    }
                    //ReloadTable();
                },
                error: function (result) {
                    //alert(JSON.stringify(result));
                }
            });
        }

        function ReloadTableForExcel() {
            $("#OrderList TBODY").html("");
            for ($i = 0; $i < Orders.length; $i++) {
                tr = $("<tr></tr>");         
                $(tr).html("<td>" + ($i + 1) + "</td><td>" + Orders[$i].Sale_Erp_Code + "</td><td>" + Orders[$i].Product_Detail_Name + "</td><td>" + parseFloat(Orders[$i].MRP_Price).toFixed(2) + "</td><td>" + Orders[$i].product_unit + "/" + Orders[$i].Product_Sale_Unit + "</td><td>" + Orders[$i].HSN_Code + "</td><td>" + parseFloat(Orders[$i].Distributor_Price).toFixed(2) + "</td><td>" + "</td>");
                $("#OrderList TBODY").append(tr);
            }
        }

        $(document).ready(function () {
            loadData();
            loadBrands();
            $('#ddlbrand').on('change', function () {
                var productbrand = $('#ddlbrand').val();
                if (productbrand != 0) {
                    $("#OrderList TBODY").html("");
                    Orders = AllOrders.filter(function (a) {
                        return a.Product_Cat_Code == productbrand;
                    });
                    ReloadTable();
                }
            });
        });
            $('#btnExcel').on('click', function (e) {
                ReloadTableForExcel();
                var data_type = 'data:application/vnd.ms-excel,' + encodeURIComponent($('#divtable').html());
                var a = document.createElement('a');
                a.href = data_type;

                a.download = 'ProductDetailList.xls';               
                a.click();
                e.preventDefault();
                ReloadTable();
        
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
    </html>
</asp:Content>