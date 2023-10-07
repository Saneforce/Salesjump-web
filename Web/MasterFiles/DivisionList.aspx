<%@ Page Title="Company List" Language="C#" AutoEventWireup="true" MasterPageFile="~/Master.master" CodeFile="DivisionList.aspx.cs" EnableEventValidation="false"
    Inherits="MasterFiles_DivisionList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "https://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

    <title>Company</title>

        <link type="text/css" rel="stylesheet" href="../css/style.css" />
        <style type="text/css">
            .modal {
                position: fixed;
                top: 0;
                left: 0;
                background-color: gray;
                z-index: 99;
                opacity: 0.8;
                filter: alpha(opacity=80);
                -moz-opacity: 0.8;
                min-height: 100%;
                width: 100%;
            }

            .loading {
                font-family: Arial;
                font-size: 10pt;
                border: 5px solid #67CFF5;
                width: 200px;
                height: 100px;
                display: none;
                position: fixed;
                background-color: White;
                z-index: 999;
            }

            .auto-style1 {
                width: 83%;
            }

            .auto-style2 {
                width: 21px;
            }
        </style>

    <form id="frm1" runat="server">
    <div class="row">
        <asp:ImageButton ID="ImageButton1" runat="server" align="right" ImageUrl="~/img/Excel-icon.png"
                Style="height: 40px; width: 40px; border-width: 0px; position: absolute; right: 15px; top: 55px;" OnClick="ExportToExcel" />
        
        <div class="col-lg-12 sub-header">Company<span style="float: right"></span><div style="float: right;padding-top: 3px;">
                    <ul class="segment">
                        <li data-va='All'>ALL</li>
                        <li data-va='0' class="active">Active</li>
                    </ul>
               </div> </div>
    </div>

    <div class="card">
            <div class="card-body table-responsive">
                <div style="white-space: nowrap">
                    Search&nbsp;&nbsp;<input type="text" id="tSearchOrd" style="width: 250px;" />
                    <%--<label style="white-space: nowrap; margin-left: 57px;">Filter By&nbsp;&nbsp;</label>
                    <select id="txtfilter" name="ddfilter" style="width: 100px;"></select>--%>
                    <label style="float: right">Show
                        <select class="data-table-basic_length" aria-controls="data-table-basic">
                            <option value="10">10</option>
                            <option value="25">25</option>
                            <option value="50">50</option>
                            <option value="100">100</option>
                        </select>
                        entries</label>
                </div>
                <table class="table table-hover" id="OrderList" style="font-size: 12px;">
                    <thead class="text-warning">
                        <tr style="white-space: nowrap;">
                            <th style="text-align:left">SlNo</th>
                            <th id="Company Name" style="text-align:left">CompanyName</th>
                            <th id="Alias Name" style="text-align:left">AliasName</th>
                            <th id="City" style="text-align:left">City</th>
                            <th id="Edit" style="text-align:left">Edit</th>
                            <th id="Deactivate" style="text-align: left">Status</th>
                        </tr>
                    </thead>
                    <tbody>
                    </tbody>
                </table>
                <div class="row">
                    <div class="col-sm-5">
                        <div class="dataTables_info" id="orders_info" role="status" aria-live="polite">Showing 1 to 10 of 57 entries</div>
                    </div>
                    <div class="col-sm-7">
                        <div class="dataTables_paginate paging_simple_numbers" id="example2_paginate">
                            <ul class="pagination">
                                <li class="paginate_button previous disabled" id="example2_previous"><a href="#" aria-controls="example2" data-dt-idx="0" tabindex="0">Previous</a></li>
                                <li class="paginate_button active"><a href="#" aria-controls="example2" data-dt-idx="1" tabindex="0">1</a></li>
                                <li class="paginate_button "><a href="#" aria-controls="example2" data-dt-idx="2" tabindex="0">2</a></li>
                                <li class="paginate_button "><a href="#" aria-controls="example2" data-dt-idx="3" tabindex="0">3</a></li>
                                <li class="paginate_button "><a href="#" aria-controls="example2" data-dt-idx="4" tabindex="0">4</a></li>
                                <li class="paginate_button "><a href="#" aria-controls="example2" data-dt-idx="5" tabindex="0">5</a></li>
                                <li class="paginate_button "><a href="#" aria-controls="example2" data-dt-idx="6" tabindex="0">6</a></li>
                                <li class="paginate_button next" id="example2_next"><a href="#" aria-controls="example2" data-dt-idx="7" tabindex="0">Next</a></li>
                            </ul>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        </form>
    

    <script src="<%=Page.ResolveUrl("~/js/jquery.min.js")%>" type="text/javascript"></script>
    <script type="text/javascript" src="../js/plugins/datatables/jquery.dataTables.js"></script>
    <script type="text/javascript" src="../js/plugins/datatables/dataTables.bootstrap.js"></script>
	<script type="text/javascript" src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
    <script lang="javascript" src="https://cdnjs.cloudflare.com/ajax/libs/xlsx/0.15.2/xlsx.full.min.js"></script>
<script lang="javascript" src="https://cdnjs.cloudflare.com/ajax/libs/FileSaver.js/1.3.8/FileSaver.min.js"></script>
    <script language="javascript" type="text/javascript">
        var AllOrders = [];
        var filtrkey = '0';
        var Orders = []; pgNo = 1; PgRecords = 10; TotalPg = 0; searchKeys = "Division_Name,Alias_Name,Division_City";

        function ReloadTable() {
            var tr = '';
            $("#OrderList TBODY").html("");
            if (filtrkey != "All") {
                Orders = Orders.filter(function (a) {
                    return a.division_active_flag == filtrkey;
                });
            }
            st = PgRecords * (pgNo - 1); slno = 0;
            for ($i = st; $i < st + PgRecords; $i++) {
                if ($i < Orders.length) {
                    tr = $("<tr rname='" + Orders[$i].Division_Name + "'  rocode='" + Orders[$i].Division_Code + "'></tr>");
                    slno = $i + 1;
                    $(tr).html('<td>' + slno + "</td><td>" + Orders[$i].Division_Name + '</td><td>' + Orders[$i].Alias_Name + '</td><td>' + Orders[$i].Division_City + '</td><td id=' + Orders[$i].Division_Code +
                        " class= 'roedit' > <a href='#'>Edit</a></td> <td class='rodeact'><a href='#' style=" + ((parseInt(Orders[$i].division_active_flag) == 0) ? 'color:green;' : 'color:red;') + " >" + ((parseInt(Orders[$i].division_active_flag) == 0) ? 'Active' : 'Deactive') + "</a></td>> ");
                    $("#OrderList TBODY").append(tr);
                    hq = [];
                }
				
            }
            $("#orders_info").html("Showing " + (st + 1) + " to " + (((st + PgRecords) < Orders.length) ? (st + PgRecords) : Orders.length) + " of " + Orders.length + " entries");
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
        function loadPgNos() {
            prepg = parseInt($(".paginate_button.previous>a").attr("data-dt-idx"));
            Nxtpg = parseInt($(".paginate_button.next>a").attr("data-dt-idx"));
            $(".pagination").html("");
            TotalPg = parseInt(parseInt(Orders.length / PgRecords) + ((Orders.length % PgRecords) ? 1 : 0)); selpg = 1;
            if (isNaN(prepg)) prepg = 0;
            if (isNaN(Nxtpg)) Nxtpg = 2;
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
                pgNo = parseInt($(this).attr("data-dt-idx"));
                ReloadTable();
            });
        }
        function loadData(div_code) {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "DivisionList.aspx/GetList",
                data: "{'div_code':'" + div_code + "'}",
                dataType: "json",
                success: function (data) {
                    AllOrders = JSON.parse(data.d) || [];
                    Orders = AllOrders; ReloadTable();
                    
                },
                error: function (result) {
                    
                }
            });
        }
        //function ExportToExcel(type, fn, dl) {
        //    var elt = document.getElementById('OrderList' );
        //    var file = XLSX.utils.table_to_book(elt, { sheet: "Company" });
        //    XLSX.write(file, { bookType: 'xlsx', bookSST: true, type: 'base64' });
        //    XLSX.writeFile(file, 'Company List.xlsx');
        //}
        $(document).ready(function () {
            div_code = Number(<%=Session["div_code"]%>);
            loadData(div_code);
            $(document).on('click', '.roedit', function () {
                x = this.id;
                window.location.href = "DivisionCreation.aspx?Div_Code=" + x;
            });
            
            $(document).on("click", ".rodeact", function () {
                var row = $(this).closest('tr');
                var route_C = $(this).closest('tr').attr('rocode');
                let oindex = Orders.findIndex(x => x.Division_Code == route_C);
                let disstat = (parseInt(Orders[oindex]["division_active_flag"]) == 0) ? 'Deactive' : 'Active';
                let flg = (parseInt(Orders[oindex]["division_active_flag"]) == 0) ? 1 : 0;
                let flag = 0;
                if ((flg == 1) || (flg == 0)) {
                    
                    if (flag == 0) {
                        if (confirm("Do you want " + disstat + " the Division ?")) {

                            $.ajax({
                                type: "POST",
                                contentType: "application/json; charset=utf-8",
                                async: false,
                                url: "DivisionList.aspx/deactivate",
                                data: "{'div_code':'" + route_C + "','stat':'" + flg + "'}",
                                dataType: "json",
                                success: function (data) {
                                    if (data.d == 'Success') {
                                        Orders[oindex]["division_active_flag"] = flg;
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
                }
                
            });
            
        });
    </script>
    </asp:Content>