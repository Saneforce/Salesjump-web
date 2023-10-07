<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="Retailer_Details.aspx.cs" Inherits="MasterFiles_MR_Retailer_Details" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link type="text/css" href="../css/bootstrap-3.3.2.min.css" rel="stylesheet" />
    <link type="text/css" href="../css/style.css" rel="stylesheet" />
    <form runat="server">
        <div class="row" style="margin-bottom: 1rem;">
            <div class="row" style="margin-bottom: 1rem;">
                <asp:ImageButton ID="ImageButton1" runat="server" align="right" ImageUrl="~/img/Excel-icon.png"
                    Style="height: 40px; width: 40px; border-width: 0px; position: absolute; right: 15px; top: 43px;" OnClick="ExportToExcel" />
            </div>
            <div class="col-lg-12 sub-header">Retailer Master<span style="float: right"><a href="ListedDoctor/ListedDr_DetailAdd.aspx" class="btn btn-primary btn-update" id="newsf">Add Retailer</a></span></div>
        </div>
        <div class="row">
            <div class="col-sm-4" style="margin-bottom: 1rem;">
                <select id="ddlstate" class="form-control"></select>
            </div>
            <div class="col-sm-4" style="margin-bottom: 1rem;">
                <select id="ddlhq" class="form-control">
                    <option value="">Select HQ</option>
                </select>
            </div>
            <div class="col-sm-4" style="margin-bottom: 1rem;">
                <select id="ddlsf" class="form-control">
                    <option value="">Select FieldForce</option>
                </select>
            </div>
            <div class="col-sm-4" style="margin-bottom: 1rem;">
                <select id="ddlroute" class="form-control">
                    <option value="">Select Route</option>
                </select>
            </div>
        </div>
        <div class="card">
            <div class="card-body table-responsive" style="overflow-x: auto;">
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
                        entries</label>
                    <div style="float: right; padding-top: 3px;">
                        <ul class="segment">
                            <li data-va='All'>ALL</li>
                            <li data-va='0' class="active">Active</li>
                        </ul>
                    </div>
                </div>
                <table class="table table-hover" id="OrderList" style="font-size: 12px">
                    <thead class="text-warning">
                        <tr style="background-color: #37a4c6; color: #fff; white-space: nowrap;">
                            <th style="text-align: left; color: #fff">Sl.No</th>
                            <th style="text-align: left; color: #fff">Profile Pic</th>
                            <th style="text-align: left; color: #fff;">Retailer Code</th>
                            <th style="text-align: left; color: #fff">Retailer Name</th>
                            <th style="text-align: left; color: #fff">Route Name</th>
                            <th style="text-align: left; color: #fff">Mobile Number</th>
                            <th style="text-align: left; color: #fff">Edit</th>
                            <th style="text-align: left; color: #fff">Deactivate</th>
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
                                <li class="paginate_button previous disabled" id="example2_previous"><a href="#" aria-controls="example2" data-dt-idx="0" tabindex="0">Previous</a></li>
                                <li class="paginate_button next" id="example2_next"><a href="#" aria-controls="example2" data-dt-idx="7" tabindex="0">Next</a></li>
                            </ul>
                        </div>
                    </div>
                </div>
                <!--<div id="myModal" class="modal" style="opacity: 1;"> -->
                <!-- The Close Button -->
                <!-- <span class="close" style="opacity: 1;">&times;</span> -->

                <!-- Modal Content (The Image) -->
                <!-- <img class="modal-content" id="img01"> -->

                <!-- Modal Caption (Image Text) -->
                <!-- <div id="caption"></div> -->
                <!-- </div> -->
                <div id="myModal" class="modal" style="z-index: 10000000; overflow-y: auto; background-color: rgb(1 3 18 / 12%); opacity: 1;" aria-hidden="false">
                    <!-- The Close Button -->
                    <div class="modal-content" style="margin-top: 80px">
                        <span class="close" style="opacity: 1; color: red;">&times;</span>
                        <%--<i class="fa fa-times-circle close" id="close"  style="font-size:48px;color:red;margin-right:-80PX;margin-top:-25px"  ></i>--%>
                        <!-- Modal Content (The Image) -->
                        <img alt="" id="img01" style="width: 100%; height: 450px;">
                    </div>
                    <!-- Modal Caption (Image Text) -->
                    <div id="caption"></div>
                </div>
            </div>
        </div>
    </form>
    <script type="text/javascript" src="../../js/plugins/datatables/jquery.dataTables.js"></script>
    <script type="text/javascript" src="../../js/plugins/datatables/dataTables.bootstrap.js"></script>
    <script language="javascript" type="text/javascript">
        var AllOrders = []; var Allsf = []; var hqsf = []; var Allstate = []; var Ostate = []; var AllHQ = []; var SFHQ = [];
        var Orders = []; pgNo = 1; PgRecords = 10; TotalPg = 0; searchKeys = "ListedDrCode,ListedDr_Name,Territory_Name,ListedDr_Mobile,";
        var filtrkey = '0'; var AllRoutes = []; var routecont = ''; var allotroutecont = '';
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
        function ReloadTable() {
            $("#OrderList TBODY").html("");
            if (filtrkey != "All") {
                Orders = Orders.filter(function (a) {
                    return a.ListedDr_Active_Flag == filtrkey;
                })
            }
            st = PgRecords * (pgNo - 1);
            for ($i = st; $i < st + Number(PgRecords); $i++) {
                if ($i < Orders.length) {
                    tr = $("<tr rname='" + Orders[$i].ListedDr_Name + "' rocode='" + Orders[$i].ListedDrCode + "'></tr>");
                    if (Orders[$i].Profilepic == "") {
                        $(tr).html("<td>" + ($i + 1) + "</td><td>" + Orders[$i].Profilepic + "</td><td>" + Orders[$i].ListedDrCode + "</td><td>" + Orders[$i].ListedDr_Name + "</td><td>" + Orders[$i].Territory_Name + "</td><td>" + Orders[$i].ListedDr_Mobile + "</td><td class='roedit'><a href='#'>Edit</a></td><td class='rodeact'><a href='#'>" + Orders[$i].Status + "</a></td>");
                    }
                    else {
                        $(tr).html("<td>" + ($i + 1) + "</td><td><img width=30 height=30 class='phimg' onclick='imgPOP(this)' src=" + Orders[$i].Profilepic + "></td><td>" + Orders[$i].ListedDrCode + "</td><td>" + Orders[$i].ListedDr_Name + "</td><td>" + Orders[$i].Territory_Name + "</td><td>" + Orders[$i].ListedDr_Mobile + "</td><td class='roedit'><a href='#'>Edit</a></td><td class='rodeact'><a href='#'>" + Orders[$i].Status + "</a></td>");
                    }
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
                        // if (val != null && val.toString().toLowerCase().indexOf(shText) > -1 && (',' + searchKeys).indexOf(',' + key + ',') > -1) {
                        if (val != null && val.toString().toLowerCase().substring(0, shText.length) == shText && (',' + searchKeys).indexOf(',' + key + ',') > -1) {
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
        $(".segment>li").on("click", function () {
            $(".segment>li").removeClass('active');
            $(this).addClass('active');
            filtrkey = $(this).attr('data-va');
            Orders = AllOrders;
            $("#tSearchOrd").val('');
            ReloadTable();
        });
        function fillstate() {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "Retailer_Details.aspx/getStates",
                data: "{'divcode':'<%=Session["div_code"]%>'}",
                dataType: "json",
                success: function (data) {
                    Allstate = JSON.parse(data.d) || [];
                    var st = $("#ddlstate");
                    st.empty().append('<option selected="selected" value="">Select State</option>');
                    for (var i = 0; i < Allstate.length; i++) {
                        st.append($('<option value="' + Allstate[i].State_Code + '">' + Allstate[i].StateName + '</option>'));
                    };
                }
            });
        }

        function fillHQ(sst) {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "Retailer_Details.aspx/getHQ",
                data: "{'divcode':'<%=Session["div_code"]%>','Sstate':'" + sst + "'}",
                dataType: "json",
                success: function (data) {
                    Allsf = JSON.parse(data.d);
                    hqsf = Allsf;
                    var hq = $("#ddlhq");
                    hq.empty().append('<option selected="selected" value="">Select HQ</option>');
                    for (var i = 0; i < hqsf.length; i++) {
                        hq.append($('<option value="' + hqsf[i].Hq_Code + '">' + hqsf[i].Sf_HQ + '</option>'));
                    };
                }
            });
        }

        function fillSF(shq) {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "Retailer_Details.aspx/getSF",
                data: "{'divcode':'<%=Session["div_code"]%>','Hq':'" + shq + "'}",
                dataType: "json",
                success: function (data) {
                    AllHQ = JSON.parse(data.d);
                    SFHQ = AllHQ;
                    var ddsf = $("#ddlsf");
                    ddsf.empty().append('<option selected="selected" value="">Select Fieldforce</option>');
                    for (var i = 0; i < SFHQ.length; i++) {
                        ddsf.append($('<option value="' + SFHQ[i].Sf_Code + '">' + SFHQ[i].Sf_Name + '</option>'));
                    };
                }
            });
        }
        function fillRoute(hq) {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "Retailer_Details.aspx/getRetailerRoutes",
                data: "{'divcode':'<%=Session["div_code"]%>','hq':'" + hq + "'}",
                dataType: "json",
                success: function (data) {
                    AllRoutes = JSON.parse(data.d);
                    var ddroute = $("#ddlroute");
                    ddroute.empty().append('<option selected="selected" value="">Select Route</option>');
                    for (var i = 0; i < AllRoutes.length; i++) {
                        ddroute.append($('<option value="' + AllRoutes[i].Territory_Code + '">' + AllRoutes[i].Territory_Name + '</option>'));
                    };
                }
            });
        }
        function fillRetailers(sfs, routes) {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "Retailer_Details.aspx/getRetailers",
                data: "{'divcode':'<%=Session["div_code"]%>','sf':'" + sfs + "','route':'" + routes + "'}",
                dataType: "json",
                success: function (data) {
                    AllOrders = JSON.parse(data.d) || [];
                    Orders = AllOrders; ReloadTable();
                }
            });
        }
        $(document).ready(function () {
            fillstate();
            $('#ddlstate').on('change', function () {
                var stt = $(this).val();
                if (stt != '') {
                    fillHQ(stt);
                }
                else {
                    alert('Select State');
                    $('#ddlhq').empty();
                    $('#ddlsf').empty();
                    $('#ddlroute').empty();
                    return false;
                }
            });
            $('#ddlhq').on('change', function () {
                var hqn = $('#ddlhq :selected').text();
                var hqval = $('#ddlhq :selected').val();
                if (hqn != 'Select HQ') {
                    fillSF(hqn);
                    fillRoute(hqval);
                }
                else {
                    alert('Select HQ');
                    $('#ddlsf').empty();
                    return false;
                }
            });
            $('#ddlsf').on('change', function () {
                var sfco = $('#ddlsf').val();
                var hqval = $('#ddlhq :selected').val();
                if (sfco != '') {
                    //fillRoute(sfco);				   
                    // fillRetailers(sfco);
                    fillRetailers(sfco, 0);
                    Sfroutes = AllRoutes.filter(function (a) {
                        return a.Sf_Code == sfco;
                    });
                    var ddroute = $("#ddlroute");
                    ddroute.empty().append('<option selected="selected" value="">Select Route</option>');
                    if (Sfroutes.length > 0) {
                        for (var i = 0; i < Sfroutes.length; i++) {
                            ddroute.append($('<option value="' + Sfroutes[i].Territory_Code + '">' + Sfroutes[i].Territory_Name + '</option>'));
                        };
                    }
                }
                else {
                    fillRoute(hqval);
                    //alert('Select Fieldforce');
                    //return false;
                }
            });
            $('#ddlroute').on('change', function () {
                var sfco = $('#ddlsf').val();
                var routes = $('#ddlroute').val();
                if (routes != '') {
                    fillRetailers(sfco, routes);
                }
                else {
                    alert('Select Route');
                    return false;
                }
            });
            $(document).on('click', '.roedit', function () {
                var route_C = parseInt($(this).closest('tr').attr('rocode'));
                window.location.href = "ListedDoctor/ListedDr_DetailAdd.aspx?type=2&ListedDrCode=" + route_C + "";
            });
            $(document).on("click", ".rodeact", function () {
                var route_C = $(this).closest('tr').attr('rocode');
                let oindex = Orders.findIndex(x => x.ListedDrCode == route_C);
                var sfco = $('#ddlsf').val();
                var route = $('#ddlroute').val();
                let flg = (parseInt(Orders[oindex]["ListedDr_Active_Flag"]) == 0) ? 1 : 0;
                if (flg != '0') {
                    if (confirm("Do you want Deactivate the Retailer ?")) {
                        $.ajax({
                            type: "POST",
                            contentType: "application/json; charset=utf-8",
                            async: false,
                            url: "Retailer_Details.aspx/retDeact",
                            data: "{'retcode':'" + route_C + "','stat':'" + flg + "'}",
                            dataType: "json",
                            success: function (data) {
                                if (data.d == 'Deactivated Successfully') {
                                    fillRetailers(sfco, route);
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
                    var div = '<%=Session["div_code"]%>';
                    if (div == '109') {
                        if (confirm("Do you want activate the Retailer ?")) {
                            $.ajax({
                                type: "POST",
                                contentType: "application/json; charset=utf-8",
                                async: false,
                                url: "Retailer_Details.aspx/routecnt",
                                data: "{'route':'" + route +"','divcode':'<%=Session["div_code"]%>'}",
                                dataType: "json",
                                success: function (data) {
                                    routecont = data.d;
                                    if (routecont == "Overcount") {
                                        alert('The seleted Route has more than alloted customers,Select Another Route.');
                                        return false;
                                    }
                                    else if (routecont == "Belowcount") {
                                        $.ajax({
                                            type: "POST",
                                            contentType: "application/json; charset=utf-8",
                                            async: false,
                                            url: "Retailer_Details.aspx/retDeact",
                                            data: "{'retcode':'" + route_C + "','stat':'" + flg + "'}",
                                            dataType: "json",
                                            success: function (data) {
                                                if (data.d == 'Deactivated Successfully') {
                                                    fillRetailers(sfco, route);
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

                                },
                            });
                        }

                    } else {
                        if (confirm("Do you want Activate the Retailer ?")) {
                            $.ajax({
                                type: "POST",
                                contentType: "application/json; charset=utf-8",
                                async: false,
                                url: "Retailer_Details.aspx/retDeact",
                                data: "{'retcode':'" + route_C + "','stat':'" + flg + "'}",
                                dataType: "json",
                                success: function (data) {
                                    if (data.d == 'Deactivated Successfully') {
                                        fillRetailers(sfco, route);
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
    <script type="text/javascript">
        // Get the modal
        var modal = document.getElementById('myModal');

        function imgPOP(x) {
            // Get the image and insert it inside the modal - use its "alt" text as a caption
            var img = document.getElementById('myImg');
            var modalImg = document.getElementById("img01");
            var captionText = document.getElementById("caption");
            modal.style.display = "block";
            modalImg.src = x.src;
            captionText.innerHTML = x.alt;
        }

        // Get the <span> element that closes the modal
        var span = document.getElementsByClassName("close")[0];

        // When the user clicks on <span> (x), close the modal
        span.onclick = function () {
            modal.style.display = "none";
        }
    </script>
    <style>
        /*popup image*/

        .phimg img {
            border-radius: 5px;
            cursor: pointer;
            transition: 0.3s;
        }

            .phimg img:hover {
                opacity: 0.7;
            }

        /* The Modal (background) */
        .modal {
            display: none; /* Hidden by default */
            position: fixed; /* Stay in place */
            z-index: 1; /* Sit on top */
            padding-top: 50px; /* Location of the box */
            left: 0;
            top: 0;
            width: 100%; /* Full width */
            height: 100%; /* Full height */
            overflow: auto; /* Enable scroll if needed */
            background-color: rgb(0,0,0); /* Fallback color */
            background-color: rgba(0,0,0,0.9); /* Black w/ opacity */
        }

        /* Modal Content (Image) */
        .modal-content {
            margin: auto;
            display: block;
            width: 30%;
            height: 60%;
            margin-top: 10%;
        }

        /* Caption of Modal Image (Image Text) - Same Width as the Image */
        #caption {
            margin: auto;
            display: block;
            width: 80%;
            max-width: 700px;
            text-align: center;
            color: #ccc;
            padding: 10px 0;
            height: 150px;
        }

        /* Add Animation - Zoom in the Modal */
        .modal-content, #caption {
            -webkit-animation-name: zoom;
            -webkit-animation-duration: 0.6s;
            animation-name: zoom;
            animation-duration: 0.6s;
        }

        @-webkit-keyframes zoom {
            from {
                -webkit-transform: scale(0)
            }

            to {
                -webkit-transform: scale(1)
            }
        }

        @keyframes zoom {
            from {
                transform: scale(0)
            }

            to {
                transform: scale(1)
            }
        }

        /* The Close Button */
        .close {
            position: absolute;
            top: 0px;
            right: 3px;
            /*color: #f1f1f1;*/
            font-size: 50px;
            font-weight: bold;
            transition: 0.3s;
        }

            .close:hover,
            .close:focus {
                color: #bbb;
                text-decoration: none;
                cursor: pointer;
            }

        /* 100% Image Width on Smaller Screens */
        @media only screen and (max-width: 700px) {
            .modal-content {
                width: 100%;
            }
        }
    </style>
</asp:Content>
