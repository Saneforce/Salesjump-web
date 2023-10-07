<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="Ret_NonOrd_list.aspx.cs" Inherits="MasterFiles_Ret_NonOrd_list" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <a href="../alertstyle/Highly-Customizable-jQuery-Toast-Message-Plugin-Toastr/build/toastr.js.map"></a>
    <link href="../alertstyle/Highly-Customizable-jQuery-Toast-Message-Plugin-Toastr/build/toastr.min.css" rel="stylesheet" />
    <script src="../alertstyle/Highly-Customizable-jQuery-Toast-Message-Plugin-Toastr/build/toastr.min.js"></script>
    <link href="../css/SalesForce_New/bootstrap-select.min.css" rel='stylesheet' type='text/css' />
    <style>
        .table-hover > tbody > tr:hover > td, .table-hover > tbody > tr:hover > th {
            background-color: aliceblue !important;
        }

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

        .toastr-confirm .toast-message {
            float: left;
        }

        .toastr-confirm .toast-actions {
            float: right;
            margin-left: 10px;
        }
    </style>
    <script type="text/javascript">
        var pgNo = 1;
        var PgRecords = 10;
        var TotalPg = 0;
        var currentPage = 0;
        var Orders = [];
        var AllOrders = [];
        var sfco = '';
        var routes = '';
        var i = 0;
        var m = 1;
        searchKeys = "ListedDrCode,ListedDr_Name";
        $(document).ready(function () {
            $("#tSearchOrd").attr('action', 'javascript:void(0);')
            $('.spinnner_div').show();
            $(".data-table-basic_length").on("change",
                function () {
                    pgNo = 1;
                    PgRecords = Number($(this).val());
					getCnt(sfco, routes);
                    loaddata(currentPage, PgRecords, sfco,routes);
                }
            );
            fillstate();
            getCnt('', '');
            $('#ddlstate').on('change', function () {
                var stt = $(this).val();
                if (stt != '') {
                    getAllHQ(stt);
                    //loaddata(0, $(".data-table-basic_length").val(), $('#ddlSf').val(), $('#ddlclstr').val())
                }
                else {
                    alert('Select State');
                    $('#ddlHQ').empty();
                    $('#ddlSf').empty();
                    $('#ddlclstr').empty();
                    return false;
                }
            });

            $('#ddlHQ').on('change', function () {
                var hqn = $('#ddlHQ :selected').text();
                var hqval = $('#ddlHQ :selected').val();
                if (hqn != 'Select HQ') {
                    AllHyrList(hqn)
                    TerritoryCreation(hqval);
                }
                else {
                    alert('Select HQ');
                    $('#ddlSf').empty();
                    return false;
                }
            });

            $('#ddlSf').on('change', function () {
                sfco = $('#ddlSf').val();
                hqval = $('#ddlHQ').val();
                if (sfco != '') {
                    //fillRoute(sfco);				   
                    // fillRetailers(sfco);
                    getCnt(sfco, '');
                    loaddata(0, $(".data-table-basic_length").val(), sfco, '');
                    Sfroutes = AllTerr.filter(function (a) {
                        return a.SF_Code == sfco;
                    });
                    var ddroute = $("#ddlclstr").empty();
                    ddroute.empty().append('<option selected="selected" value="">Route</option>');
                    if (Sfroutes.length > 0) {
                        for (var i = 0; i < Sfroutes.length; i++) {
                            ddroute.append($('<option value="' + Sfroutes[i].Territory_Code + '">' + Sfroutes[i].Territory_Name + '</option>'));
                        };
                    }
                    $("#ddlclstr").selectpicker({
                        liveSearch: true
                    });
                    $("#ddlclstr").selectpicker('refresh');
                }
                else {
                    TerritoryCreation(hqval);
                    //alert('Select Fieldforce');
                    //return false;
                }
            });

            $('#ddlclstr').on('change', function () {
                sfco = $('#ddlSf').val();
                routes = $('#ddlclstr').val();
                if (routes != '') {
                    getCnt(sfco, routes);
                    loaddata(0, $(".data-table-basic_length").val(), sfco, routes);
                }
                else {
                    alert('Select Route');
                    return false;
                }
            });


            $("#tSearchOrd").on("keyup", function () {
                if ($(this).val() != "") {
                    shText = $(this).val().toLowerCase();
                    Orders = AllOrders.filter(function (a) {
                        chk = false;
                        $.each(a, function (key, val) {
                            if (val != null && val.toString().toLowerCase().indexOf(shText) > -1 && (',' + searchKeys + ',').indexOf(',' + key + ',') > -1) {
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
            $(".pagination").on("click", ".paginate_button", function () {
                var page = $(this).text();
                if ($.trim(page) === "«") {
                    // Previous button clicked
                    if (currentPage > 1) {
                        currentPage = 1;
                        $('.spinnner_div').show();
                        setTimeout(loaddata(0, $(".data-table-basic_length").val(), sfco, routes), 500);
                        loadPgNos()
                        //updatePage();
                    }
                } else if ($.trim(page) === "»") {
                    // Next button clicked
                    if (currentPage < TotalPg) {
                        currentPage = TotalPg;
                        $('.spinnner_div').show();
                        setTimeout(loaddata(TotalPg, $(".data-table-basic_length").val(), sfco, routes), 500);
                        loadPgNos()
                        //updatePage();
                    }
                } else {
                    // Page number clicked
                    currentPage = parseInt(page);
                    $('.spinnner_div').show();
                    setTimeout(loaddata(Number(page) - 1, $(".data-table-basic_length").val(), sfco, routes), 500);
                    loadPgNos()
                    //updatePage();            
                }
            });
            loaddata(0, $(".data-table-basic_length").val(), '', '');
            $('.spinnner_div').hide();
        });
        function getCnt(Sfcode, routecode) {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "Ret_NonOrd_list.aspx/GetRetCnt",
                data: "{'Sfcode':'" + Sfcode + "','routecode':'" + routecode + "'}",
                dataType: "json",
                success: function (data) {
                    TotalPg = data.d || 0;
                    TotalPg = TotalPg / $(".data-table-basic_length").val();
                },
                error: function (result) {
                    toastr.error('Alert!!!', JSON.stringify(result));
                    //alert();
                }
            });
        }
        function dllstatus(x) {
            var Status = $(x).text();
            var selectedsts = $(x).closest('td').find('.aState').text();
            var Retcode = $(x).closest('tr').find('.ret').attr('retcode'); var statusdet = $(x).attr('v');
            if (Status != selectedsts) {
                toastr.options = {
                    closeButton: true,
                    timeOut: 0,
                    extendedTimeOut: 0,
                    preventDuplicates: true
                };

                toastr.warning('<div class="toastr-confirm">Are you sure you want to continue?<div class="toast-actions"><button type="button" class="btn btn-success toastr-yes" style="margin-right:15px">OK</button><button type="button" class="btn btn-danger toastr-no">Cancel</button></div></div>', 'Confirmation');

                $(document).on('click', '.toastr-yes', function () {
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        async: false,
                        url: "Ret_NonOrd_list.aspx/ChngRetsts",
                        data: "{'RetCode':" + Retcode + ",'Status':" + statusdet + "}",
                        dataType: "json",
                        success: function (data) {
                            $(x).closest('td').find('.aState').text(Status);
                            $(x).closest('td').find('.aState').attr('v', $(x).attr('v'));
                            $(x).closest('td').find('.aState').css("color", "#3c8dbc");
                            toastr.success(data.d);
                            toastr.clear();
                        },
                        error: function (result) {
                            alert(JSON.stringify(result));
                        }
                    });

                });

                $(document).on('click', '.toastr-no', function () {

                    toastr.warning('Action canceled!');
                    toastr.clear();

                });
                //toastr.options = {
                //    closeButton: true,
                //    progressBar: true,
                //    positionClass: 'toast-top-right',
                //    preventDuplicates: true,
                //    timeOut: 1000,  // Adjust the duration as per your preference
                //    closeButton: '<button id="toastr-ok" class="btn btn-success">OK</button><button id="toastr-cancel" class="btn btn-danger">Cancel</button>'
                //};
                //toastr.warning('Are you sure you want to continue?', 'Confirmation', {
                //    closeButton: true,
                //    onclick: function () {
                //        // Code to handle the confirmation action
                //        // For example, you can redirect to another page or perform an AJAX request 
                //        
                //        toastr.success('Action confirmed!');
                //    }                    
                //});                               
            }
        }

        function loaddata(strt, nxt, Sfcode, Routecode) {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "Ret_NonOrd_list.aspx/GetRetNonOrdList",
                data: "{'Offset':" + strt + ",'FetchNxt':" + nxt + ",'Sfcode':'" + Sfcode + "','routecode':'" + Routecode + "'}",
                dataType: "json",
                success: function (data) {
                    AllOrders = JSON.parse(data.d) || 0;
                    Orders = AllOrders;
                    ReloadTable();
                },
                error: function (result) {
                    toastr.error('Alert!!!', JSON.stringify(result));
                    //alert();
                }
            });
        }

        function ReloadTable() {
            str = '';
            list = '';
            if (Orders.length > 0) {
                $("#RetCnt tbody").html("");
                i = 0;
                i = currentPage == 0 ? 0 : (currentPage - 1) * $(".data-table-basic_length").val();
                for (j = 0; j < Orders.length; j++) {
                    var k = 0;
                    k = i + 1;
                    m = j == 0 ? i : m;
                    list = Orders[j].ListedDr_Active_Flag == 0 ? '<span ><span class="aState" data-val="0">Active</span><i class="caret" style="float:right;margin-top:8px;margin-right:0px"></i></span></a>' : '<span class="aState" style="color: #F89406;"><span class="aState" data-val="6">StandBy</span><i class="caret" style="float:right;margin-top:8px;margin-right:0px"></i></span></a>';
                    dis = '<ul class="nav" style="margin:0px"><li class="dropdown"><a href="#" style="padding:0px" class="dropdown-toggle" data-toggle="dropdown">'
                        + list +
                        '<ul class="dropdown-menu dropdown-custom dropdown-menu-right ddlStatus" style="right:0;left:auto;"><li><a href="#" v="0" Onclick="dllstatus(this)">Active</a><a Onclick="dllstatus(this)" href="#" v="6">StandBy</a></li></ul></li></ul>';
                    str += "<tr><td style='text-align:left'>" + k + "</td>\
                                <td style='text-align:left' class='ret' retcode="+ Orders[j].ListedDrCode + ">" + Orders[j].ListedDrCode + "</td>\
                                <td style='text-align:left' class='ret' ret="+ Orders[j].ListedDrCode + ">" + Orders[j].ListedDr_Name + "</td>\
                                <td style='text-align:left'>"+ dis + "</td></tr>";
                    i = k;
                }
                $("#orders_info").html("Showing " + (currentPage == 0 ? 1 : (m + 1)) + " to " + i + " of " + TotalPg * $(".data-table-basic_length").val() + " entries");
                $("#RetCnt tbody").append(str);
                $('.spinnner_div').hide();
                loadPgNos();                
            }
            else {
                $("#RetCnt tbody").html("");
                $("#orders_info").html("Showing " + (currentPage == 0 ? 1 : (m + 1)) + " to " + i + " of " + TotalPg * $(".data-table-basic_length").val() + " entries");
                $("#RetCnt tbody").append('<div style="color:red;text-align:end;padding:25px;">No Record Found...!</div>');
                $('.spinnner_div').hide();
            }

        }
        function getAllHQ(stcode) {

            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "Ret_NonOrd_list.aspx/getSFHQ",
                data: "{'statecode':'" + stcode + "'}",
                dataType: "json",
                success: function (data) {
                    AllHQ = JSON.parse(data.d) || [];
                    var st = $("#ddlHQ");
                    if (AllHQ.length > 0) {
                        st.empty().append('<option selected="selected" value="">Select HQ</option>');
                        for (var i = 0; i < AllHQ.length; i++) {
                            st.append($('<option terrcode="' + AllHQ[i].Territory_Code + '" value="' + AllHQ[i].Hq_Code + '">' + AllHQ[i].Sf_HQ + '</option>'));
                        }
                        $("#ddlHQ").selectpicker({
                            liveSearch: true
                        });
                    }
                    else {
                        alert('No Cluster Found');
                    }
                },
                error: function (Res) {
                    alert(Res);
                }
            })
        }
        function AllHyrList(Hq) {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                data: "{'Hq':'" + Hq + "'}",
                url: "Ret_NonOrd_list.aspx/getSF",
                dataType: "json",
                success: function (data) {
                    AllHyrListSf = JSON.parse(data.d) || [];
                    var st = $("#ddlSf");
                    if (AllHyrListSf.length > 0) {
                        st.empty().append('<option selected="selected" value="">Select Field Force</option>');
                        for (var i = 0; i < AllHyrListSf.length; i++) {
                            st.append($('<option value="' + AllHyrListSf[i].Sf_Code + '">' + AllHyrListSf[i].Sf_Name + '</option>'));
                        }
                        //loaddata(0, $(".data-table-basic_length").val(), $('#ddlSf').val(), $('#ddlclstr').val())
                    }
                    $("#ddlSf").selectpicker({
                        liveSearch: true
                    });
                    $("#ddlSf").selectpicker('refresh');
                },
                error: function (Res) {
                    alert(Res);
                }
            });
        }
        function TerritoryCreation(Hq) {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "Ret_NonOrd_list.aspx/getRetailerRoutes",
                data: "{'Hq':'" + Hq + "'}",
                dataType: "json",
                success: function (data) {
                    AllTerr = JSON.parse(data.d) || [];
                    var st = $("#ddlclstr");
                    if (AllTerr.length > 0) {
                        st.empty().append('<option selected="selected" value="">Select Route</option>');
                        for (var i = 0; i < AllTerr.length; i++) {
                            st.append($('<option value="' + AllTerr[i].Territory_Code + '">' + AllTerr[i].Territory_Name + '</option>'));
                        }
                        //loaddata(0, $(".data-table-basic_length").val(), $('#ddlSf').val(), $('#ddlclstr').val())
                    }
                    $("#ddlclstr").selectpicker({
                        liveSearch: true
                    });
                    $("#ddlclstr").selectpicker('refresh');
                },
                error: function (Res) {
                    alert(Res);
                }
            });
        }
        function fillstate() {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "Ret_NonOrd_list.aspx/getStates",
                data: "{'divcode':'" +<%=Session["div_code"]%>+"'}",
                dataType: "json",
                success: function (data) {
                    Allstate = JSON.parse(data.d) || [];
                    var st = $("#ddlstate");
                    st.empty().append('<option selected="selected" value="">select state</option>');
                    for (var i = 0; i < Allstate.length; i++) {
                        st.append($('<option value="' + Allstate[i].State_Code + '">' + Allstate[i].StateName + '</option>'));
                    };
                    $("#ddlstate").selectpicker({
                        liveSearch: true
                    });
                    $("#ddlstate").selectpicker('refresh');
                },
                error: function (Res) {
                    alert(Res);
                }
            });
        }
        function loadPgNos() {
            prepg = parseInt($(".paginate_button.previous>a").attr("data-dt-idx"));
            Nxtpg = parseInt($(".paginate_button.next>a").attr("data-dt-idx"));
            pgNo = currentPage;
            $(".pagination").html("");
            //TotalPg = parseInt(parseInt(Orders.length / PgRecords) + ((Orders.length % PgRecords) ? 1 : 0)); selpg = 1;
            if (isNaN(prepg)) prepg = 0;
            if (isNaN(Nxtpg)) Nxtpg = 2;
            //  if ((prepg + 1) == pgNo && pgNo > 1) selpg = (parseInt(pgNo) - 1);
            selpg = (pgNo > 7) ? (parseInt(pgNo) + 1) - 7 : 1;
            if ((Nxtpg) == pgNo) {
                selpg = (parseInt(TotalPg * $(".data-table-basic_length").val())) - 7;
                selpg = (selpg > 1) ? selpg : 1;
            }
            var temp = (currentPage - 1);
            spg = '<li class="paginate_button previous disabled" id="example2_previous"><a href="#" aria-controls="example2" data-dt-idx="1" tabindex="0">«</a></li>';
            for (il = selpg - 1; il < selpg + 7; il++) {
                if (il < TotalPg * $(".data-table-basic_length").val())
                    spg += '<li class="paginate_button' + ((pgNo == (il + 1)) ? " active" : "") + '"><a href="#" aria-controls="example2" data-dt-idx="' + (il + 1) + '" tabindex="0">' + (il + 1) + '</a></li>';
            }
            spg += '<li class="paginate_button next" id="example2_next"><a href="#" aria-controls="example2" data-dt-idx="' + TotalPg * $(".data-table-basic_length").val() + '" tabindex="0">»</a></li>';
            $(".pagination").html(spg);

            $(".paginate_button > a").on("click", function () {
                pgNo = parseInt($(this).attr("data-dt-idx"));
                /* $(".paginate_button").removeClass("active");
                 $(this).closest(".paginate_button").addClass("active");*/
            }
            );

            $('.spinnner_div').hide();
        }
    </script>
    <div class="spinnner_div" style="display: none; width: 100%">
        <div class="spinner" style="position: absolute; left: 525px; top: 133px;">
            <div class="rect1" style="background: #1a60d3;"></div>
            <div class="rect2" style="background: #DB4437;"></div>
            <div class="rect3" style="background: #F4B400;"></div>
            <div class="rect4" style="background: #0F9D58;"></div>
            <div class="rect5" style="background: orangered;"></div>
        </div>
    </div>
    <div>
        <h4>Retailer Non-Order List</h4>
        <div class="row">
            <div class="col-sm-2" style="margin-bottom: 1rem;">
                <select id="ddlstate" style="width: 250px;" class="form-control">
                    <option selected="selected">Select State</option>
                </select>
            </div>
            <div class="col-sm-2" style="margin-bottom: 1rem;">
                <select id="ddlHQ" style="width: 250px;" class="form-control">
                    <option selected="selected">Select HQ</option>
                </select>
            </div>
            <div class="col-sm-2" style="margin-bottom: 1rem;">
                <select id="ddlSf" style="width: 250px;" class="form-control">
                    <option selected="selected">Select Field Force</option>
                </select>
            </div>
            <div class="col-sm-2" style="margin-bottom: 1rem;">
                <select id="ddlclstr" style="width: 250px;" class="form-control">
                    <option selected="selected">Select Route</option>
                </select>
            </div>
        </div>
        <div class="row">
            <div class="col-lg-12">
                <div class="card" style="max-height: 500px;">
                    <div class="card-body table-responsive" style="overflow-x: auto; padding: 0px;">
                        <div style="white-space: nowrap; padding: 15px;">
                            Search&nbsp;&nbsp;<input type="text" id="tSearchOrd" style="width: 250px;" />
                            <label style="float: right">
                                Show
                       
                        <select class="data-table-basic_length" aria-controls="data-table-basic">
                            <option value="10">10</option>
                            <option value="25">25</option>
                            <option value="100">100</option>
                            <%--<option value="1000">1000</option>--%>
                        </select>
                                entries</label>
                        </div>
                        <table id="RetCnt" class="table">
                            <thead style="top: 0; z-index: 5; position: sticky; background: white;">
                                <tr>
                                    <th style="text-align: left">SlNo</th>
                                    <th style="text-align: left">Retailer Code</th>
                                    <th style="text-align: left">Retailer Name</th>
                                    <th style="text-align: left">Status</th>
                                </tr>
                            </thead>
                            <tbody>
                            </tbody>
                        </table>

                    </div>
                </div>
                <div class="row" style="margin-bottom: 70px;">
                    <div class="col-sm-5">
                        <div class="dataTables_info" id="orders_info" role="status" aria-live="polite">Showing 1 to 10 of 57 entries</div>
                    </div>
                    <div class="col-sm-7">
                        <div class="dataTables_paginate paging_simple_numbers" id="example2_paginate">
                            <ul class="pagination">
                                <li class="paginate_button previous disabled" id="example2_previous"><a href="#" aria-controls="example2" data-dt-idx="0" tabindex="0">«</a></li>
                                <li class="paginate_button active"><a href="#" aria-controls="example2" data-dt-idx="1" tabindex="0">1</a></li>
                                <li class="paginate_button "><a href="#" aria-controls="example2" data-dt-idx="2" tabindex="0">2</a></li>
                                <li class="paginate_button "><a href="#" aria-controls="example2" data-dt-idx="3" tabindex="0">3</a></li>
                                <li class="paginate_button "><a href="#" aria-controls="example2" data-dt-idx="4" tabindex="0">4</a></li>
                                <li class="paginate_button "><a href="#" aria-controls="example2" data-dt-idx="5" tabindex="0">5</a></li>
                                <li class="paginate_button "><a href="#" aria-controls="example2" data-dt-idx="6" tabindex="0">6</a></li>
                                <li class="paginate_button next" id="example2_next"><a href="#" aria-controls="example2" data-dt-idx="7" tabindex="0">»</a></li>
                            </ul>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

