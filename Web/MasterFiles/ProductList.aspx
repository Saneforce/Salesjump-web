<%@ Page Title="Product Detail List" Language="C#" AutoEventWireup="true" MasterPageFile="~/Master.master"
    CodeFile="ProductList.aspx.cs" Inherits="MasterFiles_ProductList" EnableEventValidation="false" %>

<%--<%@ Register Src ="~/UserControl/MenuUserControl.ascx" TagName ="Menu" TagPrefix="ucl" %>--%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
     <link href="../css/jquery.multiselect.css" rel="stylesheet" />
    <style type="text/css">
        #ddlsf_chzn     {
            width: 300px !important;
            font-weight: 500;
        }

        #txtfilter_chzn {
            width: 300px !important;
            top: 10px;
        }

        th {
            white-space: nowrap;
            cursor: pointer;
        }
    </style>
    <form runat="server">
        <div class="row">
            <div class="row">
                <asp:ImageButton ID="ImageButton1" runat="server" align="right" ImageUrl="~/img/Excel-icon.png"
                    Style="height: 40px; width: 40px; border-width: 0px; position: absolute; right: 15px; top: 43px;" OnClick="lnkDownload_Click" />
            </div>
            <div class="col-lg-12 sub-header">
                Product Detail<span style="float: right"><a href="ProductDetail.aspx" class="btn btn-primary btn-update" id="newsf">Add Product</a></span>
            </div>
        </div>
        <div class="card">
            <div class="card-body table-responsive">
                <div style="white-space: nowrap">
                    Search&nbsp;&nbsp;<input type="text" id="tSearchOrd" style="width: 250px;" />
                    <span id="filspan" style="margin-left: 10px;">Filter By&nbsp;&nbsp;            
                    <div class="export btn-group">
                        <button class="btn btn-default dropdown-toggle" aria-label="Export" data-toggle="dropdown" type="button" title="Export data">
                            <i class="fa fa-th-list"></i>
                            <span class="caret"></span>
                        </button>
                        <ul class="dropdown-menu" role="menu">
                            <li role="menuitem" class="" onclick="radiochange(this)" value="1">
                                <a href="#">Product Category</a>
                            </li>
                            <li role="menuitem" class="" onclick="radiochange(this)" value="2">
                                <a href="#">Product Brand</a>
                            </li>
                            <li role="menuitem" class="" onclick="radiochange(this)" value="3">
                                <a href="#">Sub Division</a>
                            </li>
                            <li role="menuitem" class="" onclick="radiochange(this)" value="4">
                                <a href="#">State</a>
                            </li>
                        </ul>
                    </div>
                        <select class="form-control" id="txtfilter" name="ddfilter" style="min-width: 315px !important; max-width: 318px !important; display: inline"></select></span>
                    <label style="float: right">
                        Show
                    <select class="data-table-basic_length" aria-controls="data-table-basic">
                        <option value="10">10</option>
                        <option value="25">25</option>
                        <option value="50">50</option>
                        <option value="100">100</option>
                    </select>
                        entries</label><div style="float: right; padding-top: 3px;">
                            <ul class="segment">
                                <li data-va='All'>ALL</li>
                                <li data-va='0' class="active">Active</li>
                            </ul>
                        </div>
                </div>
                <table class="table table-hover" id="OrderList" style="font-size: 12px">
                    <thead class="text-warning">
                        <tr>
                            <th style="text-align: left">Sl.No</th>
                            <th id="Product_Code" style="text-align: left">Product Code</th>
                            <th id="Sale_Erp_Code" style="text-align: left">ERP Code</th>
                            <th id="Product_Name" style="text-align: left">Product Image</th>
                            <th id="Product_Image" style="text-align: left">Product Name</th>
                            <th id="Description" style="text-align: left">Description</th>
                            <th id="Base_UOM" style="text-align: left">Base UOM</th>
                            <th id="Edit" style="text-align: left">Edit</th>
                            <th id="Deactivate" style="text-align: left">Deactivate</th>
                        </tr>
                    </thead>
                    <tbody>
                    </tbody>
                </table>
                <div class="row" style="padding: 5px 0px">
                    <div class="col-sm-5">
                        <div class="dataTables_info" id="orders_info" role="status" aria-live="polite">Showing 0 to 0 of 0 entries</div>
                    </div>
                    <div class="col-sm-7">
                        <div class="dataTables_paginate paging_simple_numbers" id="example2_paginate">
                            <ul class="pagination" style="float: right; margin: -11px 0px">
                                <li class="paginate_button previous disabled" id="example2_previous"><a href="#" aria-controls="example2" data-dt-idx="0" tabindex="0">First</a></li>
                                <li class="paginate_button next" id="example2_next"><a href="#" aria-controls="example2" data-dt-idx="7" tabindex="0">Last</a></li>
                            </ul>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
    <script type="text/javascript" src="../js/plugins/datatables/jquery.dataTables.js"></script>
    <script type="text/javascript" src="../js/plugins/datatables/dataTables.bootstrap.js"></script>
    <script language="javascript" type="text/javascript">
        var AllOrders = [];
        var SFDivisions = [];
        var SFbrnd = [];
        var SFHQs = [];
        var SFstat = [];
        var sf;
        var fdt = '';
        var tdt = '';
        var filtrkey = '0';
        var sortid = '';
        var asc = true;
        var Orders = []; pgNo = 1; PgRecords = 10; TotalPg = 0; searchKeys = "Product_Detail_Code,Product_Detail_Name,Product_Sale_Unit,Product_Description,Sale_Erp_Code,";
        $(".data-table-basic_length").on("change",
            function () {
                pgNo = 1;
                PgRecords = $(this).val();
                ReloadTable();
            }
        );

        function radiochange(x) {
            Orders = AllOrders;
            ReloadTable();
            $('#txtfilter_chzn').remove();
            $('#txtfilter').removeClass('chzn-done');
            var sfmgs = $("#txtfilter");
            if (x.value == 2) {
                sfmgs.empty().append('<option value="0">Select Brand</option>');
                for (var i = 0; i < SFbrnd.length; i++) {
                    sfmgs.append($('<option value="' + SFbrnd[i].Product_Brd_Code + '">' + SFbrnd[i].Product_Brd_Name + '</option>')).trigger('chosen:updated');
                };
            }
            if (x.value == 3) {
                sfmgs.empty().append('<option value="0">Select Division</option>');
                for (var i = 0; i < SFDivisions.length; i++) {
                    sfmgs.append($('<option value="' + SFDivisions[i].subdivision_code + '">' + SFDivisions[i].subdivision_name + '</option>')).trigger('chosen:updated');
                };
            }
            if (x.value == 4) {

                sfmgs.empty().append('<option value="0">Select State</option>');
                for (var i = 0; i < SFstat.length; i++) {
                    sfmgs.append($('<option value="' + SFstat[i].scode + '">' + SFstat[i].sname + '</option>')).trigger('chosen:updated');
                };
            }
            if (x.value == 1) {

                sfmgs.empty().append('<option value="0">Select Category</option>');
                for (var i = 0; i < SFHQs.length; i++) {
                    sfmgs.append($('<option value="' + SFHQs[i].Product_Cat_Code + '">' + SFHQs[i].Product_Cat_Name + '</option>')).trigger('chosen:updated');
                };
            }
            $('#txtfilter').chosen();
        }
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
            spg = '<li class="paginate_button previous disabled" id="example2_previous"><a href="#" aria-controls="example2" data-dt-idx="1" tabindex="0">First</a></li>';
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
        function ReloadTable() {

            var tr = '';
            $("#OrderList TBODY").html("");
            if (filtrkey != "All") {
                Orders = Orders.filter(function (a) {
                    return a.Product_Active_Flag == filtrkey;
                })
            }
            st = PgRecords * (pgNo - 1);
            for ($i = st; $i < st + Number(PgRecords) ; $i++) {
                if ($i < Orders.length) {

                    tr = $("<tr rname='" + Orders[$i].Product_Detail_Name + "'  rocode='" + Orders[$i].Product_Detail_Code + "'></tr>");
                    if (Orders[$i].image == "") {
                        $(tr).html("<td>" + ($i + 1) + "</td><td>" + Orders[$i].Product_Detail_Code + "</td><td>" + Orders[$i].Sale_Erp_Code + "</td><td></td><td>" + Orders[$i].Product_Detail_Name + "</td><td>" + Orders[$i].Product_Description + "</td><td class='rocount'><a href='#'>" + Orders[$i].Product_Sale_Unit + "</a></td><td class='roedit'><a href='#'>Edit</a></td><td class='rodeact'><a href='#'>" + Orders[$i].Status + "</a></td>");
                    }
                    else {
                        $(tr).html("<td>" + ($i + 1) + "</td><td>" + Orders[$i].Product_Detail_Code + "</td><td>" + Orders[$i].Sale_Erp_Code + "</td><td><img id='prodImg' src='" + Orders[$i].image + "' style='border-width:0px;width: 50px;height: 60px;'></td><td>" + Orders[$i].Product_Detail_Name + "</td><td>" + Orders[$i].Product_Description + "</td><td class='rocount'><a href='#'>" + Orders[$i].Product_Sale_Unit + "</a></td><td class='roedit'><a href='#'>Edit</a></td><td class='rodeact'><a href='#'>" + Orders[$i].Status + "</a></td>");

                    }
                    $("#OrderList TBODY").append(tr);
                }
            }
            $("#orders_info").html("Showing " + (st + 1) + " to " + (((st + PgRecords) < Orders.length) ? (st + PgRecords) : Orders.length) + " of " + Orders.length + " entries")
            loadPgNos();
        }
        $(".segment>li").on("click", function () {
            $(".segment>li").removeClass('active');
            $(this).addClass('active');
            filtrkey = $(this).attr('data-va');
            Orders = AllOrders;
            $("#tSearchOrd").val('');
            ReloadTable();
        });


        $("#tSearchOrd").on("keyup", function () {
            if ($(this).val() != "") {
                shText = $(this).val().toLowerCase();
                Orders = AllOrders.filter(function (a) {
                    chk = false;
                    $.each(a, function (key, val) {
                        if (val != null && val.toString().toLowerCase().indexOf(shText) > -1 && (',' + searchKeys).indexOf(',' + key + ',') > -1) {
                            chk = true;
                        }
                    })
                    return chk;
                })
            }
            else
                Orders = AllOrders
            ReloadTable();
        });

        function loadData() {

            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "ProductList.aspx/GetDetails",
                data: "{'divcode':'<%=Session["div_code"]%>'}",
                dataType: "json",
                success: function (data) {
                    AllOrders = JSON.parse(data.d) || [];
                    Orders = AllOrders; ReloadTable();
                },
                error: function (result) {
                    //alert(JSON.stringify(result));
                }
            });
        }

        function loadcat() {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "ProductList.aspx/GetcatDetails",
                data: "{'divcode':'<%=Session["div_code"]%>'}",
                dataType: "json",
                success: function (data) {
                    SFHQs = JSON.parse(data.d) || [];
                    var sfhq = $("[id*=txtfilter]");
                    sfhq.empty().append('<option selected="selected" value="0">Select Category</option>');
                    for (var i = 0; i < SFHQs.length; i++) {
                        sfhq.append($('<option value="' + SFHQs[i].Product_Cat_Code + '">' + SFHQs[i].Product_Cat_Name + '</option>'));
                    };
                }
            });
            $('#txtfilter').chosen();
        }
        function loadbrand() {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "ProductList.aspx/getbrand",
                data: "{'divcode':'<%=Session["div_code"]%>'}",
                dataType: "json",
                success: function (data) {
                    SFbrnd = JSON.parse(data.d) || [];
                }
            });
        }
        function loadsubdivision() {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "ProductList.aspx/getsubDivisions",
                data: "{'divcode':'<%=Session["div_code"]%>'}",
                dataType: "json",
                success: function (data) {
                    SFDivisions = JSON.parse(data.d) || [];
                }
            });
        }
        function loadstat() {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "ProductList.aspx/getstat",
                data: "{'divcode':'<%=Session["div_code"]%>'}",
                dataType: "json",
                success: function (data) {
                    SFstat = JSON.parse(data.d) || [];
                }
            });
        }
        $(document).ready(function () {
            sf = '<%=Session["Sf_Code"]%>';
            loadData();
            loadcat();
            loadbrand();
            loadsubdivision();
            loadstat();


            $(document).on('click', '.rocount', function () {
                $('#RouteModal').modal('toggle');
                var route_C = $(this).closest('tr').attr('rocode');
                var route_N = $(this).closest('tr').attr('rname');
                $('#RouteModalLabel').text("Routes for " + route_N);

            });
            $(document).on('click', '.roedit', function () {
                var route_C = $(this).closest('tr').attr('rocode');
                window.location.href = "ProductDetail.aspx?Product_Detail_Code=" + route_C + "";
            });
            $(document).on("click", ".rodeact", function () {
                var route_C = $(this).closest('tr').attr('rocode');
                let oindex = Orders.findIndex(x => x.Product_Detail_Code == route_C);
                let disstat = Orders[oindex]["Status"].toString();
                let flg = (parseInt(Orders[oindex]["Product_Active_Flag"]) == 0) ? 1 : 0;
                var rocnt = isNaN(parseInt($(this).closest('tr').find('.rocount>a').html())) ? 0 : parseInt($(this).closest('tr').find('.rocount>a').html());

                if ((rocnt < 1 && flg == 1) || (flg == 0)) {
                    if (confirm("Do you want " + disstat + " the Product ?")) {
                        $.ajax({
                            type: "POST",
                            contentType: "application/json; charset=utf-8",
                            async: false,
                            url: "ProductList.aspx/deactivateprodtl",
                            data: "{'arcode':'" + route_C + "','stat':'" + flg + "'}",
                            dataType: "json",
                            success: function (data) {
                                if (data.d == 'Success') {
                                    Orders[oindex]["Product_Active_Flag"] = flg;
                                    Orders[oindex]["Status"] = (flg == 0) ? "Deactivate" : "Activate";
                                    ReloadTable();
                                    alert('Status Changed Successfully...');
                                }
                                else {
                                    alert("Status Can't be Changed");
                                }
                            },
                            error: function (result) {
                            }
                        });
                    }
                }
                else {
                    alert("Status Can't be Changed");
                }
            });
            $('#txtfilter').on('change', function () {
                var sfs = $(this).val();
                var hqn = $('#txtfilter_chosen a').text().trim();
                if (sfs != 0) {
                    Orders = AllOrders;
                    Orders = Orders.filter(function (a) {
                        return a.Product_Brd_Name == sfs || a.Product_Cat_Name == sfs || (',' + a.subdivision_code + ',').indexOf(',' + sfs + ',') > -1 || (((',' + a.State_Code + ',').indexOf(',' + sfs + ',')) > -1);
                    });
                }
                else {
                    Orders = AllOrders;
                }
                ReloadTable();
            });
        });
    </script>

</asp:Content>
