<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Master.master" CodeFile="Retailer_Details_New.aspx.cs" Inherits="MasterFiles_MR_Retailer_Details_New" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <!DOCTYPE>
    <html xmlns="http://www.w3.org/1999/xhtml">
        <head>
            <title></title>            
            <link type="text/css" href="../../css/style.css" rel="stylesheet" /> 
            <link href="../../css_new/searchddl/select2.min.css" type="text/css" rel="stylesheet" />

           
            <style type="text/css">
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
                    padding-top: 100px; /* Location of the box */
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
                    width: 80%;
                    height: 80%;
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
                    top: 45px;
                    right: 35px;
                    color: #f1f1f1;
                    font-size: 50px;
                    font-weight: bold;
                    transition: 0.3s;
                }

                .close:hover, .close:focus {
                    color: #bbb;
                    text-decoration: none;
                    cursor: pointer;
                }

                /* 100% Image Width on Smaller Screens */
                @media only screen and (max-width: 700px){
                    .modal-content {
                        width: 100%;
                    }
                }

                .scrollmenu a {
                    height: 70% !important;
                }
                .form-control {
                    border-radius: 0;
                    box-shadow: none;
                    border-color: #d2d6de
                }

                .select2{
                    padding:3px 4px !important;
                }

                .select2-hidden-accessible {
                    border: 0 !important;
                    clip: rect(0 0 0 0) !important;
                    height: 1px !important;
                    margin: -1px !important;
                    overflow: hidden !important;
                    /*padding: 0 !important;*/
                    padding:3px 4px !important;
                    position: absolute !important;
                    width: 1px !important
                }

                .form-control {
                    display: block;
                    width: 100%;
                    height: 34px;
                    padding: 3px 4px !important;
                    font-size: 14px;
                    line-height: 1.42857143;
                    color: #555;
                    background-color: #fff;
                    background-image: none;
                    border: 1px solid #ccc;
                    border-radius: 4px;
                    -webkit-box-shadow: inset 0 1px 1px rgba(0, 0, 0, .075);
                    box-shadow: inset 0 1px 1px rgba(0, 0, 0, .075);
                    -webkit-transition: border-color ease-in-out .15s, -webkit-box-shadow ease-in-out .15s;
                    -o-transition: border-color ease-in-out .15s, box-shadow ease-in-out .15s;
                    transition: border-color ease-in-out .15s, box-shadow ease-in-out .15s
                }

                .select2-container--default .select2-selection--single,
                .select2-selection .select2-selection--single {
                    border: 1px solid #d2d6de;
                    border-radius: 0;
                    padding: 3px 4px !important;
                    height: 30px
                }

                .select2-container--default .select2-selection--single {
                    background-color: #fff;
                    border: 1px solid #aaa;
                    border-radius: 4px
                }

                .select2-container .select2-selection--single {
                    box-sizing: border-box;
                    cursor: pointer;
                    display: block;
                    height: 28px;
                    user-select: none;
                    -webkit-user-select: none
                }
                .select2-container .select2-selection--single .select2-selection__rendered {
                    padding: 3px 4px !important;
                }
                
                .select2-container .select2-selection--single .select2-selection__rendered {
                    padding-left: 0;
                    padding-right: 0;
                    height: auto;
                    margin-top: -3px
                }

                .select2-container--default .select2-selection--single .select2-selection__rendered {
                    color: #444;
                    line-height: 20px
                }

                .select2-container--default .select2-selection--single,
                .select2-selection .select2-selection--single {
                    border: 1px solid #d2d6de;
                    border-radius: 0 !important;
                     padding: 3px 3px !important;
                    height: 30px !important
                }

                .select2-container--default .select2-selection--single .select2-selection__arrow {
                    height: 20px;
                    position: absolute;
                    top: 6px !important;
                    right: 1px;
                    width: 20px
                }
            </style>
        </head>
        <body>
            <form runat="server">
                <div class="card table-responsive">

                    <div class="row" style="margin-bottom: 1rem;">
                        <div class="col-md-12" style="margin-bottom: 1rem;">
                            <div class="col-md-6 sub-header" style="float:left">
                                 Retailer Master
                            </div>
                            
                            <div class="col-md-6"  style="float: right">                               
                                <div class="col-md-3 sub-header" style="float: right">
                                    <asp:ImageButton ID="ImageButton1" runat="server" align="right" ImageUrl="~/img/Excel-icon.png" 
                                        Style="height: 40px; width: 40px; border-width: 0px; position: absolute; right: 15px; " OnClick="ExportToExcel" />
                                </div>
                                 <div class="col-md-3 sub-header" style="float: right;width:10%;">                                  
                                    <span style="float: right;border-width: 0px; position: absolute; top: 5px;height: 40px; width: 40px;">
                                        <a href="ListedDoctor/ListedDr_DetailAdd_Custom.aspx" class="btn btn-primary btn-update" id="newsf">
                                            Add Retailer
                                        </a>
                                    </span>
                                </div>
                            </div>
                        </div>                        
                    </div>

                    <div class="row">
                        <div class="col-md-12">
                            <div class="col-sm-3" style="margin-bottom: 1rem;">
                                <select id="ddlstate" class="form-control select2"></select>
                            </div>
                            <div class="col-sm-3" style="margin-bottom: 1rem;">
                                <select id="ddlhq" class="form-control select2">
                                    <option value="">Select HQ</option>
                                </select>
                            </div>
                            <div class="col-sm-3" style="margin-bottom: 1rem;">
                                <select id="ddlsf" class="form-control select2">
                                    <option value="">Select FieldForce</option>
                                </select>
                            </div>
                            <div class="col-sm-3" style="margin-bottom: 1rem;">
                                <select id="ddlroute" class="form-control select2" data-show-subtext="true" data-live-search="true">
                                    <option value="">Select Route</option>
                                </select>
                            </div>
                        </div>    
                    </div>

                    <div class="card-body table-responsive" style="overflow-x: auto;">
                        <div style="white-space: nowrap">
                            <div class="fieldsetting" style="width:30px;height:30px;float: right;padding-bottom:10px;padding-left:10px;padding-right:10px;padding-top:10px;">
                                <button type="button" class="btnsettings" id="btnsettings"><i class="fa fa-cog"></i></button>
                            </div>

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
                            
                            <div style="float: right;padding-top: 3px;">
                                <ul class="segment">
                                    <li data-va='All'>ALL</li>
                                    <li data-va='0' class="active">Active</li>                                   
                                </ul>
                            </div>
                        </div>
                        <br />
                        <table class="table table-hover" id="OrderList" style="font-size: 12px">
                            <thead class="text-warning">
                                <tr style="background-color: #37a4c6; color: #fff; white-space: nowrap;">
                                    <th class="one" style="text-align: left; color: #fff">Sl.No</th>
                                    <th class="one" style="text-align: left; color: #fff">Profile Pic</th>
                                    <th class="one" style="text-align: left; color: #fff;">Retailer Code</th>
                                    <th class="one" style="text-align: left; color: #fff">Retailer Name</th>
                                    <th class="one" style="text-align: left; color: #fff">Route Name</th>
                                    <th class="one" style="text-align: left; color: #fff">Mobile Number</th>
                                    <th class="one" style="text-align: left; color: #fff">Edit</th>
                                    <th class="one" style="text-align: left; color: #fff">Deactivate</th>
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
                        <div id="myModal" class="modal" style="opacity: 1;">
                            <!-- The Close Button -->
                            <span class="close" style="opacity: 1;">&times;</span>
                            <!-- Modal Content (The Image) -->
                            <img alt="" class="modal-content" id="img01" />
                            <!-- Modal Caption (Image Text) -->
                            <div id="caption"></div>
                        </div>
                        <div class="modal fade" id="CustomFieldModal" style="z-index: 10000000; background: transparent;" tabindex="0" aria-hidden="true">
                            <div class="modal-dialog" role="document" style="width: 30% !important">
                                <div class="modal-content">
                                    <div class="modal-header">
                                        <h5 class="modal-title" id="CustomFieldModalLabel"></h5>
                                    </div>
                                    <div class="modal-body" style="padding-top: 10px">
                                        <div class="row">
                                            <div class="col-sm-12" style="max-height:500px !important; overflow-x:scroll;">
                                                <table id="CustoFielddets" cellpadding="0" cellspacing="0" class="table" style="width: 100%; font-size: 12px;">
                                                    <thead class="text-warning">
                                                        <tr>
                                                            <th style="text-align: left">S.No</th>
                                                            <th style="text-align: left">Field Name</th>
                                                            <th class="hide" style="text-align: left">Field Column</th>
                                                            <th class="hide" style="text-align: left">Status</th>
                                                            <th style="text-align: left"><input type="checkbox" name="checkAll" id="checkAll" class="checkAll" /></th>
                                                        </tr>
                                                    </thead>
                                                    <tbody></tbody>
                                                </table>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="modal-footer">
                                        <button type="button" class="btn btn-primary" onclick="ApplyFields()" data-dismiss="modal">Apply</button>
                                        <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </form>

            <script type="text/javascript" src="../../js/plugins/datatables/jquery.dataTables.js"></script>
            <script type="text/javascript" src="../../js/plugins/datatables/dataTables.bootstrap.js"></script>
            <script type="text/javascript" src="../../css_new/searchddl/jquery.min.js"></script>            
            <script type="text/javascript"  src="../../css_new/searchddl/select2.min.js"></script>

            <script language="javascript" type="text/javascript">
                var AllOrders = []; var Allsf = []; var hqsf = []; var Allstate = []; var Ostate = []; var AllHQ = []; var SFHQ = [];
                var DisPlayF = [];
                var Orders = []; pgNo = 1; PgRecords = 10; TotalPg = 0; searchKeys = "ListedDrCode,ListedDr_Name,Territory_Name,ListedDr_Mobile,";
                var filtrkey = '0'; var AllRoutes = []; var MasFrms = []; var ARetailer = [];
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
                    });
                }

                function ReloadTable() {
                    var $td = "";
                  
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


                            if (DisPlayF.length > 0) {
                                $td = "";
                                if (DisPlayF.length > 0) {
                                    for (var $k = 0; $k < DisPlayF.length; $k++) {
                                        $fldnm = DisPlayF[$k].DisPlayName;
                                        $fldnc = DisPlayF[$k].ColumnName;  

                                        $('#OrderList thead>tr').each(function () {
                                            $('th', this).each(function () {
                                                var ortblhn = $(this).text();
                                                if ($fldnm == ortblhn) {

                                                    fillAdditionalRetailer($fldnc);

                                                    console.log(ARetailer);
                                                    let CustItem = ARetailer.filter(function (a) { return Orders[$i].ListedDrCode == a.Retail_code });
                                                    console.log(CustItem);
                                                    if (CustItem.length > 0) {
                                                        var $val = CustItem[0][$fldnc];
                                                        console.log($val);
                                                        if (($val == null || $val == '' || $val == "")) {
                                                            $val = "";

                                                            $td += "<td></td>";
                                                            if (Orders[$i].Profilepic == "") {
                                                                tr = $("<tr rname='" + Orders[$i].ListedDr_Name + "' rocode='" + Orders[$i].ListedDrCode + "'></tr>");
                                                                $(tr).html("<td>" + ($i + 1) + "</td><td>" + Orders[$i].Profilepic + "</td><td>" + Orders[$i].ListedDrCode + "</td><td>" + Orders[$i].ListedDr_Name + "</td><td>" + Orders[$i].Territory_Name + "</td><td>" + Orders[$i].ListedDr_Mobile + "</td><td class='roedit'><a href='#'>Edit</a></td><td class='rodeact'><a href='#'>" + Orders[$i].Status + "</a></td>" + $td + "");
                                                            }
                                                            else {
                                                                tr = $("<tr rname='" + Orders[$i].ListedDr_Name + "' rocode='" + Orders[$i].ListedDrCode + "'></tr>");
                                                                $(tr).html("<td>" + ($i + 1) + "</td><td><img width=30 height=30 class='phimg' onclick='imgPOP(this)' src=" + Orders[$i].Profilepic + "></td><td>" + Orders[$i].ListedDrCode + "</td><td>" + Orders[$i].ListedDr_Name + "</td><td>" + Orders[$i].Territory_Name + "</td><td>" + Orders[$i].ListedDr_Mobile + "</td><td class='roedit'><a href='#'>Edit</a></td><td class='rodeact'><a href='#'>" + Orders[$i].Status + "</a></td>" + $td + "");
                                                            }
                                                        }                                                       
                                                        else {
                                                            //alert($val);
                                                            //if (($val.includes(".xlsx") || $val.includes(".xlx") || $val.includes(".doc"))) {
                                                            //    alert('files');
                                                            //    DownloadFiels($val);
                                                            //    //alert('doc');
                                                            //}
                                                            //else if (($val.includes(".jpg") || $val.includes(".jpeg") || $val.includes(".png"))) {
                                                            //    console.log($val);
                                                            //    DownloadFiels($val);
                                                            //}
                                                            //else if (($val.includes(".mp3") || $val.includes(".mp4") || $val.includes(".gif"))) {
                                                            //    DownloadFiels($val);
                                                            //}
                                                            var ans = $val.includes('.') ? "true" : "false";
                                                            if (ans == "true") {
                                                                //alert('file');
                                                            }
                                                            else {
                                                               // alert('text');
                                                            }

                                                            $td += "<td>" + $val + "</td>";
                                                            if (Orders[$i].Profilepic == "") {
                                                                tr = $("<tr rname='" + Orders[$i].ListedDr_Name + "' rocode='" + Orders[$i].ListedDrCode + "'></tr>");
                                                                $(tr).html("<td>" + ($i + 1) + "</td><td>" + Orders[$i].Profilepic + "</td><td>" + Orders[$i].ListedDrCode + "</td><td>" + Orders[$i].ListedDr_Name + "</td><td>" + Orders[$i].Territory_Name + "</td><td>" + Orders[$i].ListedDr_Mobile + "</td><td class='roedit'><a href='#'>Edit</a></td><td class='rodeact'><a href='#'>" + Orders[$i].Status + "</a></td>" + $td + "");
                                                            }
                                                            else {
                                                                tr = $("<tr rname='" + Orders[$i].ListedDr_Name + "' rocode='" + Orders[$i].ListedDrCode + "'></tr>");
                                                                $(tr).html("<td>" + ($i + 1) + "</td><td><img width=30 height=30 class='phimg' onclick='imgPOP(this)' src=" + Orders[$i].Profilepic + "></td><td>" + Orders[$i].ListedDrCode + "</td><td>" + Orders[$i].ListedDr_Name + "</td><td>" + Orders[$i].Territory_Name + "</td><td>" + Orders[$i].ListedDr_Mobile + "</td><td class='roedit'><a href='#'>Edit</a></td><td class='rodeact'><a href='#'>" + Orders[$i].Status + "</a></td>" + $td + "");
                                                            }
                                                        }
                                                    }

                                                }
                                                else {

                                                    if (Orders[$i].Profilepic == "") {
                                                        tr = $("<tr rname='" + Orders[$i].ListedDr_Name + "' rocode='" + Orders[$i].ListedDrCode + "'></tr>");
                                                        $(tr).html("<td>" + ($i + 1) + "</td><td>" + Orders[$i].Profilepic + "</td><td>" + Orders[$i].ListedDrCode + "</td><td>" + Orders[$i].ListedDr_Name + "</td><td>" + Orders[$i].Territory_Name + "</td><td>" + Orders[$i].ListedDr_Mobile + "</td><td class='roedit'><a href='#'>Edit</a></td><td class='rodeact'><a href='#'>" + Orders[$i].Status + "</a></td>");
                                                    }
                                                    else {
                                                        tr = $("<tr rname='" + Orders[$i].ListedDr_Name + "' rocode='" + Orders[$i].ListedDrCode + "'></tr>");
                                                        $(tr).html("<td>" + ($i + 1) + "</td><td><img width=30 height=30 class='phimg' onclick='imgPOP(this)' src=" + Orders[$i].Profilepic + "></td><td>" + Orders[$i].ListedDrCode + "</td><td>" + Orders[$i].ListedDr_Name + "</td><td>" + Orders[$i].Territory_Name + "</td><td>" + Orders[$i].ListedDr_Mobile + "</td><td class='roedit'><a href='#'>Edit</a></td><td class='rodeact'><a href='#'>" + Orders[$i].Status + "</a></td>");
                                                    }
                                                }
                                            });
                                        });
                                    }
                                }
                            }
                            else {

                                if (Orders[$i].Profilepic == "") {
                                    tr = $("<tr rname='" + Orders[$i].ListedDr_Name + "' rocode='" + Orders[$i].ListedDrCode + "'></tr>");
                                    $(tr).html("<td>" + ($i + 1) + "</td><td>" + Orders[$i].Profilepic + "</td><td>" + Orders[$i].ListedDrCode + "</td><td>" + Orders[$i].ListedDr_Name + "</td><td>" + Orders[$i].Territory_Name + "</td><td>" + Orders[$i].ListedDr_Mobile + "</td><td class='roedit'><a href='#'>Edit</a></td><td class='rodeact'><a href='#'>" + Orders[$i].Status + "</a></td>");
                                }
                                else {
                                    tr = $("<tr rname='" + Orders[$i].ListedDr_Name + "' rocode='" + Orders[$i].ListedDrCode + "'></tr>");
                                    $(tr).html("<td>" + ($i + 1) + "</td><td><img width=30 height=30 class='phimg' onclick='imgPOP(this)' src=" + Orders[$i].Profilepic + "></td><td>" + Orders[$i].ListedDrCode + "</td><td>" + Orders[$i].ListedDr_Name + "</td><td>" + Orders[$i].Territory_Name + "</td><td>" + Orders[$i].ListedDr_Mobile + "</td><td class='roedit'><a href='#'>Edit</a></td><td class='rodeact'><a href='#'>" + Orders[$i].Status + "</a></td>");
                                }
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
                        url: "Retailer_Details_New.aspx/getStates",
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
                        url: "Retailer_Details_New.aspx/getHQ",
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
                    //$("#ddlhq").select2('referesh');
                }

                function fillSF(shq) {
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        async: false,
                        url: "Retailer_Details_New.aspx/getSF",
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
                        url: "Retailer_Details_New.aspx/getRetailerRoutes",
                        data: "{'divcode':'<%=Session["div_code"]%>','hq':'" + hq + "'}",
                        dataType: "json",
                        success: function (data) {
                            AllRoutes = JSON.parse(data.d);                           
                            //console.log(AllRoutes);
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
                        url: "Retailer_Details_New.aspx/getRetailers",
                        data: "{'divcode':'<%=Session["div_code"]%>','sf':'" + sfs + "','route':'" + routes + "'}",
                        dataType: "json",
                        success: function (data) {
                            AllOrders = JSON.parse(data.d) || [];
                            Orders = AllOrders; ReloadTable();
                        }
                    });
                }

                function fillAdditionalRetailer(ColumnName) {
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        async: false,
                        url: "Retailer_Details_New.aspx/GetAdditionalRetailer",
                        data: "{'ModuleId':'3','divcode':'<%=Session["div_code"]%>','ColumnName':'" + ColumnName + "'}",
                        dataType: "json",
                        success: function (data) {
                            ARetailer = JSON.parse(data.d) || [];

                        }
                    });
                }



                function DownloadFiels(fileName) {
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        async: false,
                        url: "Retailer_Details_New.aspx/DownloadImageFromS3",
                        data: "{'filename':'" + fileName + "'}",
                        dataType: "json",
                        success: function (data) {

                            alert(data.d);

                        }
                    });
                }



                $(document).ready(function () {
                    
                    fillstate(); 

                    fillAdditionalRetailer();

                    $(document).on('click', '.roedit', function () {
                        var route_C = parseInt($(this).closest('tr').attr('rocode'));
                        window.location.href = "ListedDoctor/ListedDr_DetailAdd_Custom.aspx?type=2&ListedDrCode=" + route_C + "";
                    });

                    $(document).on("click", ".rodeact", function () {
                        var route_C = $(this).closest('tr').attr('rocode');
                        let oindex = Orders.findIndex(x => x.ListedDrCode == route_C);
                        var sfco = $('#ddlsf').val();
                        var route = $('#ddlroute').val();
                        let flg = (parseInt(Orders[oindex]["ListedDr_Active_Flag"]) == 0) ? 1 : 0;
                        if (confirm("Do you want Deactivate the Retailer ?")) {
                            $.ajax({
                                type: "POST",
                                contentType: "application/json; charset=utf-8",
                                async: false,
                                url: "Retailer_Details_New.aspx/retDeact",
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
                    });


                    loadDisPlayCustomFields();

                    loadCustomFields();

                    ApplyFields();

                    $(document).on('click', '.btnsettings', function () {
                        $('#CustomFieldModal').modal('toggle');
                        $('#CustomFieldModalLabel').text("Addtional Fields for Retailer");
                        loadCustomFields();
                        CheckFields();
                    });
                   
                });

                //$('.select2').select2({
                //    closeOnSelect: true
                //});

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

                function loadDisPlayCustomFields() {
                    var ModuleId = "3";                    
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        async: false,
                        url: "Retailer_Details_New.aspx/DisPlayCutomFields",
                        data: "{'ModuleId':'" + ModuleId + "'}",
                        dataType: "json",
                        success: function (data) {                           
                           
                            DisPlayF = JSON.parse(data.d) || [];                          
                           
                        },
                        error: function (data) {
                            alert(JSON.stringify(data.d));
                        }
                    });

                }

                function loadCustomFields() {
                    var ModuleId = "3";
                    var Sf = 'admin';
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        async: false,
                        url: "Retailer_Details_New.aspx/GetCustomFormsFieldsColumns",
                        data: "{'divcode':'<%=Session["div_code"]%>','ModuleId':'" + ModuleId + "','Sf':'" + Sf + "'}",
                        dataType: "json",
                        success: function (data) {
                            $('#CustoFielddets TBODY').html("");
                            //Orders = JSON.parse(data.d) || [];
                            MasFrms = JSON.parse(data.d) || [];
                            //console.log(MasFrms);
                            if (MasFrms.length > 0) {
                                for (var i = 0; i < MasFrms.length; i++) {

                                    tr = $("<tr class='" + MasFrms[i].Field_Col + " row-select' id='" + MasFrms[i].Field_Col + "'></tr>");
                                    //var hq = filtered[$i].Sf_Name.split('-');
                                    slno = i + 1;
                                    var fldtype = MasFrms[i].Field_Type;
                                    var fldview = MasFrms[i].Field_Visible;
                                    //alert(fldtype);
                                    var checkbox = "";
                                    if (fldview == "1") {
                                        checkbox = "<input type='checkbox' checked='checked'  class='fldName' id='" + i + "' name='fldName' value='" + MasFrms[i].Field_Name + "' />";
                                    }
                                    else {
                                        checkbox = "<input type='checkbox' class='fldName' id='" + i + "' name='fldName' value='" + MasFrms[i].Field_Name + "' />";
                                    }
                                    $(tr).html('<td><i class="fa fa-ellipsis-v tbltrmove"></i></td><td class="fldsName">' + MasFrms[i].Field_Name +
                                        '</td><td class="hide fldcol">' + MasFrms[i].Field_Col +
                                        '</td><td class="hide fldtype"> ' + MasFrms[i].Field_Type +
                                        '</td><td class="hide fldvb"> ' + MasFrms[i].Field_Visible +
                                        '</td><td class="hide fldorder"> ' + MasFrms[i].Field_Order +
                                        '</td><td>' + checkbox + '</td>');
                                    $("#CustoFielddets TBODY").append(tr);

                                }
                            }
                        },
                        error: function (data) {
                            alert(JSON.stringify(data.d));
                        }
                    });

                }

                $('#checkAll').click(function () {
                    isChecked = $(this).prop("checked");
                    /*$('#CustoFielddets tbody').find('input[type="checkbox"]').prop('checked', isChecked);*/
                    //MasFrms[0].Field_Visible = isChecked

                    $('#CustoFielddets tbody').find('input[type="checkbox"]').each(function () {
                        if ($(this).prop("checked") == false) {
                            $(this).prop("checked", isChecked);
                            var tblIndex = $(this).closest('tr').index();
                            if (MasFrms.length > 0) {
                                MasFrms[tblIndex].Field_Visible = $(this).prop("checked");
                            }
                            var cbval = $(this).val();
                            //alert(cbval);
                            var ActiveView = "1";
                            UpdateCutomRetailerData(cbval, ActiveView);
                            
                        }
                        else {
                            $(this).prop("checked", isChecked);
                            var tblIndex = $(this).closest('tr').index();
                            if (MasFrms.length > 0) {
                                MasFrms[tblIndex].Field_Visible = $(this).prop("checked");
                            }
                            var cbval = $(this).val();
                            //alert(cbval);
                            var ActiveView = "0";
                            UpdateCutomRetailerData(cbval, ActiveView);
                            
                        }
                    });
                });

                $('#CustoFielddets tbody').on('click', 'input[type="checkbox"]', function (e) {

                    var isChecked = $(this).prop("checked");

                    var tblIndex = $(this).closest('tr').index();

                    //MasFrms[tblIndex].Field_Visible = $(this).prop("checked");

                    if (isChecked == true) {
                        var cbval = $(this).val();
                        //alert(cbval);
                        var ActiveView = "1";
                        UpdateCutomRetailerData(cbval, ActiveView);
                        
                    }
                    else {
                        var cbval = $(this).val();
                        //alert(cbval);
                        var ActiveView = "0";
                        UpdateCutomRetailerData(cbval, ActiveView);
                        
                    }

                    if (MasFrms.length > 0) {
                        MasFrms[tblIndex].Field_Visible = isChecked;

                        var isHeaderChecked = $("#checkAll").prop("checked");

                        if (isChecked == false && isHeaderChecked) {
                            $("#checkAll").prop('checked', isChecked);
                        }
                        else {
                            $('#CustoFielddets tbody').find('input[type="checkbox"]').each(function () {
                                if ($(this).prop("checked") == false) {
                                    isChecked = false;
                                }
                            });
                            //console.log(isChecked);
                            $("#checkAll").prop('checked', isChecked);
                        }
                    }

                    //console.log(MasFrms);
                });


                function ApplyFields() {
                    loadDisPlayCustomFields();
                    var html_table_data = "";
                    var bRowStarted = true; var $tctd = ""; var colflg = true;

                    var tblheader = new Array();
                    $('#OrderList thead>tr').each(function () {
                        $('th', this).each(function () {
                            tblheader.push($(this).text());
                        });
                    });
                    var i = 0;
                    if (DisPlayF.length > 0) {
                        $tctd = "";
                        for (var i = 0; i < DisPlayF.length; i++) {
                            var tcbval = DisPlayF[i].DisPlayName;

                            $("#CustoFielddets TBODY").find('tr').each(function () {
                                $(this).find("input[type='checkbox']:checked").each(function () {

                                    var cbval = $(this).val();

                                    if (tcbval == cbval) {
                                        let CustItem = tblheader.filter(e => e.includes(tcbval));
                                        //console.log(CustItem);
                                        if (CustItem == 0) {

                                            $tctd += "<th style='text-align:left; color: #fff'>" + cbval + "</th>";
                                        }
                                    }

                                });

                            });

                        }
                    }


                    //console.log($tctd);
                    $('#OrderList thead > tr').append($tctd);

                    //ReloadTable();

                }

                function CheckFields() {

                    var html_table_data = "";
                    var bRowStarted = true; var $td = ""; var colflg = true;

                    $('#OrderList thead>tr').each(function () {
                        $('th', this).each(function () {

                            //html_table_data += $(this).text();
                            var ortblhn = $(this).text();
                            $("#CustoFielddets TBODY").find('tr').each(function () {
                                $(this).find("input[type='checkbox']").each(function () {
                                    var cbval = $(this).val();
                                    //alert(id);
                                    if (ortblhn == cbval) {
                                        if ($(this).prop('checked') == false) {
                                            $(this).prop('checked', true);
                                            $(this).prop('checked', 'checked');
                                        }
                                    }
                                });
                            });
                        });
                    });

                }

                function UpdateCutomRetailerData(columnName, ActiveView) {
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        async: false,
                        url: "Retailer_Details_New.aspx/InsertUpdateDisplayFileds",
                        data: "{'columnName':'" + columnName + "','ActiveView':'" + ActiveView + "'}",
                        dataType: "json",
                        success: function (data) {
                            //alert(data.d);
                            //CFBindData = JSON.parse(data.d) || [];
                            //loadDisPlayCustomFields();
                        },
                        error: function (data) {
                            alert(JSON.stringify(data.d));
                        }
                    });
                }

            </script>
        </body>
    </html> 
	
</asp:Content>
