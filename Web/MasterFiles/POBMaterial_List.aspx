<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="POBMaterial_List.aspx.cs" Inherits="MasterFiles_POBMaterial_List" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="../css/jquery.multiselect.css" rel="stylesheet" />
    <style>
        #ddlsf_chzn {
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
         .ms-options {
                min-height: 100px;
                max-width: 321px;
                max-height: 332.938px;
                width: 20px;
                overflow: hidden;
            }

            .ms-options-wrap {
                overflow: hidden;
                max-width: 250px;
            }
    </style>
    <form runat="server">
        <div class="row">
          <%--  <div class="row">
                <button img="~/img/Excel-icon.png"  tyle="height: 40px; width: 40px; border-width: 0px; position: absolute; right: 15px; top: 43px;" onclick="lnkDownload_Click" ></button>
            </div>--%>
            <div class="col-lg-12 sub-header">
                POP Material Detail 
               
                <button type="button" style="float: right" class="btn btn-primary" data-toggle="modal" data-target="#exampleModal" onclick="openmodal()">Add Product</button>
            </div>
        </div>
        <div class="card">
            <div class="card-body table-responsive">
                <div style="white-space: nowrap">
                    Search&nbsp;&nbsp;<input type="text" id="tSearchOrd" style="width: 250px;" />


                    <label style="float: right">
                        Show
                    <select class="data-table-basic_length" aria-controls="data-table-basic">
                        <option value="10">10</option>
                        <option value="25">25</option>
                        <option value="50">50</option>
                        <option value="100">100</option>
                    </select>
                        entries</label><%--<div style="float: right; padding-top: 3px;">
                            <ul class="segment">
                                <li data-va='All'>ALL</li>
                                <li data-va='0' class="active">Active</li>
                            </ul>
                        </div>--%>
                </div>
                <table class="table table-hover" id="OrderList" style="font-size: 12px">
                    <thead class="text-warning">
                        <tr>
                            <th style="text-align: left">Sl.No</th>
                            <th id="Product_Code" style="text-align: left">Product Code</th>
                            <th id="Sale_Erp_Code" style="text-align: left">ERP Code</th>
                            <th id="Product_Image" style="text-align: left">Product Name</th>
                            <th id="Base_UOM" style="text-align: left" class="hidden">Base UOM</th>
                            <th id="divi" style="text-align: left">Division</th>
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
        <div class="modal fade" id="materialModal" style="z-index: 10000000; background: transparent; overflow-y: scroll;" tabindex="0" aria-hidden="true">
            <div class="modal-dialog" role="document" style="width: 50% !important">
                <div class="modal-content" style="width:600px">
                    <div class="modal-header">
                        <h5 class="modal-title" id="RouteModalLabel"></h5>
                    </div>
                    <div class="modal-body" style="padding-top: 10px">
                        <div class="row">
                            <div class="col-sm-12">
                                <table id="retailerdets" style="width: 100%; font-size: 12px;">
                                    <thead class="text-warning">
                                        <tr>
                                            <th style="text-align: left; font-size: 15px;" class="hidden">Code</th>
                                            <th style="text-align: left; font-size: 15px;">Material Name</th>
                                            <th style="text-align: left; font-size: 15px;">Division</th>
                                            <th style="text-align: left; font-size: 15px;" class='hidden'>UOM</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr>
                                            <td class="hidden">
                                                <input id="mcod" style="width: 275px; height: 30px; font-size: 15px;" /></td>
                                            <td style="width:300px">
                                                <input id="mname" style="width: 275px; height: 30px; font-size: 15px;" /></td>
                                            <td style="width:250px">
                                                <select id="mdiv" style="width: 200px; height: 30px; font-size: 15px;" multiple="multiple"></select></td>
                                            <td class='hidden'>
                                                <select id="muom" style="width: 200px; height: 30px; font-size: 15px;"></select></td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-primary" id="savedets" onclick="savedetails()">Save</button>
                        <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                    </div>
                </div>
            </div>
        </div>
    </form>
    <script type="text/javascript" src="../js/plugins/datatables/jquery.dataTables.js"></script>
    <script type="text/javascript" src="../js/plugins/datatables/dataTables.bootstrap.js"></script>
    <link href="../css/SalesForce_New/bootstrap-select.min.css" rel="stylesheet" />
    <script language="javascript" type="text/javascript">
        var AllOrders = [];
        var SFDivisions = [];
        var SFbrnd = [];
        var Orders = []; pgNo = 1; PgRecords = 10; TotalPg = 0; searchKeys = "POP_Code,POP_Name,ERP_Code,subdivision_name,";
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
        function loadData() {

            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "POBMaterial_List.aspx/GetDetails",
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
        function ReloadTable() {

            var tr = '';
            $("#OrderList TBODY").html("");
            //if (filtrkey != "All") {
            //    Orders = Orders.filter(function (a) {
            //        return a.Product_Active_Flag == filtrkey;
            //    })
            //}
            st = PgRecords * (pgNo - 1);
            for ($i = st; $i < st + Number(PgRecords); $i++) {
                if ($i < Orders.length) {

                    tr = $("<tr rname='" + Orders[$i].POP_Name + "'  rocode='" + Orders[$i].POP_Code + "'></tr>");

                    $(tr).html("<td>" + ($i + 1) + "</td><td>" + Orders[$i].POP_Code + "</td><td>" + Orders[$i].ERP_Code + "</td><td>" + Orders[$i].POP_Name + "</td><td class='hidden'>" + Orders[$i].POP_UOM + "</td><td>" + Orders[$i].subdivision_name + "</td><td class='roedit'><a href='#'>Edit</a></td><td class='rodeact'><a href='#'>" + Orders[$i].Status + "</a></td>");


                    $("#OrderList TBODY").append(tr);
                }
            }
            $("#orders_info").html("Showing " + (st + 1) + " to " + (((st + PgRecords) < Orders.length) ? (st + PgRecords) : Orders.length) + " of " + Orders.length + " entries")
            loadPgNos();
        }

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
        function openmodal() {
            $('#materialModal').modal('toggle');
            $('#mname').val('');
            loaddivision();

            //loaduom();
        }
        function loaddivision() {

            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "POBMaterial_List.aspx/getdivision",
                data: "{'divcode':'<%=Session["div_code"]%>'}",
                dataType: "json",
                success: function (data) {
                    sub = JSON.parse(data.d);
                    if (sub.length > 0) {
                        var subdivision_name = '';
                        sub_N = "";
                        subdivision_name = $("#mdiv").empty();
                        for (var i = 0; i < sub.length; i++) {
                            sub_N += '<option value="' + sub[i].subdivision_code + '">' + sub[i].subdivision_name + '</option>';
                            subdivision_name.append($('<option value="' + sub[i].subdivision_code + '">' + sub[i].subdivision_name + '</option>'))
                        }
                        $("#mdiv").multiselect({
                            columns: 1,
                            placeholder: 'Select Divisions',
                            search: true,
                            searchOptions: {
                                'default': 'Select Divisions'
                            },
                            selectAll: true
                        }).multiselect('reload');
                        $('.ms-options ul').css('column-count', '1');

                    }
                },
            });
        }
        function loaduom() {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "POBMaterial_List.aspx/GetUOM",
                data: "{'divcode':'<%=Session["div_code"]%>'}",
                dataType: "json",
                success: function (data) {
                    uom = JSON.parse(data.d);
                    if (uom.length > 0) {

                        Move_MailFolder_Name = "";
                        Move_MailFolder_Name = $("#muom").empty();
                        for (var i = 0; i < uom.length; i++) {
                            //Prds += '<option value="' + uom[i].Move_MailFolder_Id + '">' + uom[i].Move_MailFolder_Name + '</option>';
                            Move_MailFolder_Name.append($('<option value="' + uom[i].Move_MailFolder_Id + '">' + uom[i].Move_MailFolder_Name + '</option>'))
                        }

                    }
                    //$('#uombox1 option:contains(EA)').attr('selected', true);
                },
                error: function (result) {
                }


            });
        }
        function savedetails() {
            var popcode = $('#mcod').val();
            var name = $('#mname').val();
            var divi = $('#mdiv').val();
            //var uomv = $('#muom option:selected').text();
            var uomv = "EA"
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "POBMaterial_List.aspx/savedets",
                data: "{'divcode':'<%=Session["div_code"]%>','name':'" + name + "','divi':'" + divi + "','uomv':'" + uomv + "','pcode':'" + popcode + "'}",
                dataType: "json",
                success: function (data) {
                    alldets = JSON.parse(data.d);
                    if (alldets == '') {
                        alert('error occured');
                    }
                    else {
                        alert('Record Successfully Inserted');
                        $('#materialModal').modal('hide');
                        loadData();
                    }

                },
            });

        }

        $(document).ready(function () {
            sf = '<%=Session["Sf_Code"]%>';
            loadData();

            $(document).on('click', '.rocount', function () {
                $('#materialModal').modal('toggle');
                var route_C = $(this).closest('tr').attr('rocode');
                var route_N = $(this).closest('tr').attr('rname');
                $('#RouteModalLabel').text("Routes for " + route_N);

            });
            $(document).on('click', '.roedit', function () {
                var route_C = $(this).closest('tr').attr('rocode');
                $('#materialModal').modal('toggle');
                loaddivision();
                loaduom();
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "POBMaterial_List.aspx/getdets",
                    data: "{'divcode':'<%=Session["div_code"]%>','mcode':'" + route_C + "'}",
                    dataType: "json",
                    success: function (data) {
                        alldets = JSON.parse(data.d);
                        $('#mcod').val(alldets[0].POP_Code);
                        $('#mname').val(alldets[0].POP_Name);
                       //$('#mdiv').val(alldets[0].Sub_Division_Code);

                         var dicode = alldets[0].Sub_Division_Code.split(',') || [];
                            for (var i = 0; i < dicode.length; i++) {
                                for (var j = 0; j < sub.length; j++) {
                                    if (dicode[i] == sub[j].subdivision_code) {
                                        $("#mdiv option[value='" + sub[j].subdivision_code + "']").attr('selected', true);
                                        $('#mdiv').multiselect('select', sub[j].subdivision_code);
                                    }
                                }
                            }
                            $("#mdiv").multiselect('reload');
                        //$('#muom').val(alldets[0].Move_MailFolder_Id);
                    },
                });


            });
            $(document).on("click", ".rodeact", function () {
                let flg = 0;
                var route_C = $(this).closest('tr').attr('rocode');
                let oindex = Orders.findIndex(x => x.POP_Code == route_C);
                let disstat = Orders[oindex]["Status"].toString();
                //let flg = (parseInt(Orders[oindex]["Active_Flag"]) == 0) ? 1 : 0;

                if (disstat == 'Deactivate') {
                    flg = 1;
                }
                var rocnt = isNaN(parseInt($(this).closest('tr').find('.rocount>a').html())) ? 0 : parseInt($(this).closest('tr').find('.rocount>a').html());

                if ((rocnt < 1 && flg == 1) || (flg == 0)) {
                    if (confirm("Do you want " + disstat + " the Product ?")) {
                        $.ajax({
                            type: "POST",
                            contentType: "application/json; charset=utf-8",
                            async: false,
                            url: "POBMaterial_List.aspx/deactivateprodtl",
                            data: "{'arcode':'" + route_C + "','stat':'" + flg + "','divcode':'<%=Session["div_code"]%>'}",
                            dataType: "json",
                            success: function (data) {
                                if (data.d == 'Success') {
                                    //Orders[oindex]["Product_Active_Flag"] = flg;
                                    //Orders[oindex]["Status"] = (flg == 0) ? "Deactivate" : "Activate";                                    
                                    alert('Status Changed Successfully...');
                                    loadData();

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

        });

    </script>

</asp:Content>

