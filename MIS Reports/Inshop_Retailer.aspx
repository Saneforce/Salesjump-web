<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="Inshop_Retailer.aspx.cs" Inherits="MIS_Reports_Inshop_Retailer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link rel="Stylesheet" href="../css/kendo.common.min.css" />
    <link rel="Stylesheet" href="../css/kendo.default.min.css" />
    <style>
        .segment1 {
            display: inline-block;
            padding-left: 0;
            margin: -2px 22px;
            border-radius: 4px;
            font-size: 13px;
            font-family: "Poppins";
            /* border: 1px solid  #66a454   #d3252a  #f59303;*/
        }

            .segment1 > li {
                display: inline-block;
                background: #fafafa;
                color: #666;
                margin-left: -4px;
                padding: 5px 10px;
                min-width: 50px;
                border: 1px solid #ddd;
            }

                .segment1 > li:first-child {
                    border-radius: 4px 0px 0px 4px;
                }

                .segment1 > li:last-child {
                    border-radius: 0px 4px 4px 0px;
                }

                .segment1 > li.active {
                    color: #fff;
                    cursor: default;
                    background-color: #428bca;
                    border-color: #428bca;
                }

        [data-val='Rejected'] {
            color: red;
        }

        [data-val='Approved'] {
            color: green;
        }

        [data-val='Pending'] {
            color: blue;
        }

        th {
            white-space: nowrap;
            cursor: pointer;
        }

        #txtfilter_chzn {
            width: 300px !important;
            top: 10px;
        }

        body {
            font-family: Arial, Helvetica, sans-serif;
        }

        #myImg {
            border-radius: 5px;
            cursor: pointer;
            transition: 0.3s;
        }
        #myImg:hover {
                opacity: 0.7;
            }
     
        #myyImg {
            border-radius: 5px;
            cursor: pointer;
            transition: 0.3s;
        }
        #myyImg:hover {
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
            height: 800%; /* Full height */
            overflow: auto; /* Enable scroll if needed */
            background-color: rgb(0,0,0); /* Fallback color */
            background-color: rgba(0,0,0,0.9); /* Black w/ opacity */
        }

        /* Modal Content (image) */
        .modal-content {
            margin: auto;
            display: block;
            width: 100%;
            height:500px;
            max-width:450px;
        }

        /* Caption of Modal Image */
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

        /* Add Animation */
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
            top: 15px;
            right: 35px;
            color: #f1f1f1;
            font-size: 40px;
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
        .pointer {cursor: pointer;}
    </style>
    <form runat="server" id="frm1">
        <asp:HiddenField ID="hfilter" runat="server" />
        <asp:HiddenField ID="hffilter" runat="server" />
        <asp:HiddenField ID="hsfhq" runat="server" />
        <div class="row">
            <div class="row">
                <asp:ImageButton ID="ImageButton1" runat="server" align="right" ImageUrl="~/img/Excel-icon.png" CssClass="hidden"
                    Style="height: 40px; width: 40px; border-width: 0px; position: absolute; right: 15px; top: 43px;" />
            </div>
            <div class="col-lg-12 sub-header">
                Inshop Sales Activity <span style="position: absolute; right: 0px;">
                    <div id="reportrange" class="form-control mt-5 mb-5" style="background: #fff; cursor: pointer; padding: 5px 10px; border: 1px solid #ccc;">
                        <i class="fa fa-calendar"></i>&nbsp;
        <span id="ordDate"></span><i class="fa fa-caret-down"></i>
                    </div>
                </span>
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
                        entries</label>
                </div>
                <table class="table table-hover" id="OrderList" style="font-size: 12px">
                    <thead class="text-warning">
                        <tr>
                            <th style="text-align: left">Sl.No</th>
                            <th id="WrkDate" style="text-align: left">Date</th>
                            <th id="SF_Name" style="text-align: left">Staff Name</th>
                             <th id="In_Time" style="text-align: left">In Time</th>
                            <th id="Out_Time" style="text-align: left">Out Time</th>
                            <th id="DistName" style="text-align: left;">Retailer Name</th>                           
                            <th id="OrderValue" style="text-align: left">Sale Value</th>
                            <th id="photos" style="text-align: left">C In Selfie</th>
                            <th id="photo" style="text-align: left">C Out Selfie</th>
                            <th id="viewDet" style="text-align: left">View Sale Qty</th>
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
                
                 
            </div>
        </div>

    </form>
    <div class="modal fade in" id="myModal" role="dialog" style="z-index: 10000000; overflow-y: auto; background-color: rgb(1 3 18 / 12%);"aria-hidden="false">
                     <div class="modal-dialog">
                         <div class="modal-content">
                              <i class="fa fa-times-circle close" id="close"  style="font-size:48px;color:red;margin-right:-80PX;margin-top:-25px"  ></i>
                             <img id="img01" style="width: 100%;height:600px;">
                         </div>
                         <div id="caption"></div>
                     </div>
                 </div>

     <div class="modal fade in" id="mymmodal" role="dialog" style="z-index: 10000000; overflow-y: auto; background-color: rgb(1 3 18 / 12%);"aria-hidden="false">
                     <div class="modal-dialog">
                         <div class="modal-content">
                              <i class="fa fa-times-circle close" id="clos"  style="font-size:48px;color:red;margin-right:-80PX;margin-top:-25px"  ></i>
                             <img id="im01" style="width: 100%;height:600px;">
                         </div>
                         <div id="captio"></div>
                     </div>
                 </div>
    <script type="text/javascript" src="../js/kendo.all.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/js/bootstrap.min.js"></script>
    <link rel="stylesheet" href="/path/to/dist/css/image-zoom.css" />
    <script src="/path/to/cdn/jquery.min.js"></script>
    <script src="/path/to/dist/js/image-zoom.min.js"></script>
    <script type="text/javascript" src="../js/plugins/datatables/jquery.dataTables.js"></script>
    <script type="text/javascript" src="../js/plugins/datatables/dataTables.bootstrap.js"></script>
    <script language="javascript" type="text/javascript">
        var AllOrders = [];
        var sftyp ='<%=Session["sf_type"]%>';
        var SFHQs = [];
        var SFMGRs = [];
        var Arrsum = [];
        var AllOrders2 = [];
        var sf;
        var sf1;
        var fdt = '';
        var tdt = '';
        var filtrkey = 'All';
        var fffilterkey = 'AllFF';
        var sortid = '';
        var asc = true;//
        var Orders = []; pgNo = 1; PgRecords = 10; TotalPg = 0; searchKeys = "Sf_Name,WrkDate";
        $(".data-table-basic_length").on("change",
            function () {
                pgNo = 1;
                PgRecords = $(this).val();
                ReloadTable();
            });
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
        function closemodal() {
             $('#myModal').modal('hide');
        }
      

        function ReloadTable() {
            $("#OrderList TBODY").html("");
            var tr = '';

            if (fffilterkey != "AllFF") {
                Orders = Orders.filter(function (a) {
                    return a.Approved_By == fffilterkey;
                })
            }
            if (filtrkey != "All") {
                Orders = Orders.filter(function (a) {
                    return a.dvMin == filtrkey;
                })
            }
            st = PgRecords * (pgNo - 1);
            for ($i = st; $i < st + Number(PgRecords); $i++) {
                if ($i < Orders.length) {
                    //,WrkDate,DistName,Rut_Name,Ret_Name,Promotor,Start_Time,End_Time,Value,Rmks
                    tr = $("<tr class='trclick' id='" + Orders[$i].Sf_Code + ":" + Orders[$i].WrkDate + "'></tr>");

                    $(tr).html("<td>" + ($i + 1) + "</td><td>" + Orders[$i].WrkDate + "</td><td>" + Orders[$i].Sf_Name + "</td><td>" + Orders[$i].In_time + "</td><td>" + Orders[$i].Out_Time + "</td><td>" + Orders[$i].Retailer_Name + "</td><td>" + Orders[$i].Sale_Value + "</td><td><img style='width:35px' onclick=\"ImageZoom(\'myImg" + ($i + 1) + "'\)\" id='myImg" + ($i + 1) + "'  src='http://fmcg.sanfmcg.com/photos/" + Orders[$i].Cin_Img_Url + "'/></td><td><img style='width:35px' onclick=\"ImgZoom(\'myyImg" + ($i + 1) + "'\)\" id='myyImg" + ($i + 1) + "'  src='http://fmcg.sanfmcg.com/photos/" + Orders[$i].Cout_Img_Url + "'/></td><td><a style='white-space:nowrap'class='pointer'  target=\"_popup\" onclick=\"window.open('InshopRetailer_Sale_Report.aspx?sf=" + Orders[$i].Sf_Code + "&Sl_No=" + Orders[$i].Sl_No + "','popup','width=800,height=800,scrollbars=no,resizable=no'); return false;\">View Sales Qty</a></td>"); //<td>" + Orders[$i].Rmks+"</td>
                   // href=\"InshopRetailer_Sale_Report.aspx?sf=" + Orders[$i].Sf_Code + "&Sl_No=" + Orders[$i].Sl_No + "\"

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
        })
        function loadData() {
            dt = new Date();
            $m = dt.getMonth() + 1, $y = dt.getFullYear();
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "Inshop_Retailer.aspx/GetDetails",
                data: "{'SF':'<%=Session["Sf_Code"]%>','Div':'<%=Session["Division_Code"]%>','fdt':'" + fdt + "','tdt':'" + tdt + "'}",
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

        function ImageZoom(ID) {
            var modal = document.getElementById("myModal");

            // Get the image and insert it inside the modal - use its "alt" text as a caption
            var img = document.getElementById(ID);
            var modalImg = document.getElementById("img01");
            var captionText = document.getElementById("caption");
            img.onclick = function () {
                modal.style.display = "block";
                modalImg.src = this.src;
                captionText.innerHTML = this.alt;
            }

            // Get the <span> element that closes the modal

            var span = document.getElementsByClassName("close")[0];           
            // When the user clicks on <span> (x), close the modal
            span.onclick = function () {
                modal.style.display = "none";
            }
        }

        
        function ImgZoom(ID) {
            var modal = document.getElementById("mymmodal");

            // Get the image and insert it inside the modal - use its "alt" text as a caption
            var img = document.getElementById(ID);
            var modalImg = document.getElementById("im01");
            var captionText = document.getElementById("caption");
            img.onclick = function () {
                modal.style.display = "block";
                modalImg.src = this.src;
                captionText.innerHTML = this.alt;
            }

            // Get the <span> element that closes the modal

            var span = document.getElementsByClassName("close")[1];           
            // When the user clicks on <span> (x), close the modal
            span.onclick = function () {
                modal.style.display = "none";
            }
        }

        
        //function closew() {
        //    $('#cphoto1').css("display", 'none');
        //}
        //$(document).on('click', '.picc', function () {
        //    var photo = $(this).attr("src");
        //    $('#photo1').attr("src", $(this).attr("src"));
        //    $('#cphoto1').css("display", 'block');
        //});
        $(document).ready(function () {
            sf = '<%=Session["Sf_Code"]%>';
            loadData();
            //loadData2();
            // loadhq();
            //loadmgrs(sf);
            dv = $('<div style="z-index: 10000000;position:fixed;left:50%;top:50%;width:99%;height:99%;transform: translate(-50%, -50%);border-radius: 15px;background:#ececec;display:none" id="cphoto1"></div>');
            $(dv).html('<span style="position: absolute;z-index: 10000000;padding: 5px;cursor: default;background: #a5a0a0;border-radius: 50%;width: 20px;height: 20px;line-height: 6px;text-align: center;border: solid 1px gray;top: 6px;right: 6px;" onclick="closew()">x</span><img style="width:98%;height:98%;border-radius: 15px;object-fit: contain;transform:translate(1%, 1%)" id="photo1" />')
            $("body").append(dv);
            $('#txtfilter').on('change', function () {
                var sfs = $(this).val();
                var hqn = $('#txtfilter :selected').text();
                $("#<%=hsfhq.ClientID%>").val(hqn);
                if (sfs != 0) {
                    Orders = AllOrders;
                    Orders = Orders.filter(function (a) {
                        return a.SF_Code == sfs || a.HQ_Name == hqn;
                    });
                }
                else {
                    Orders = AllOrders;
                }
                ReloadTable();
            });
            $("#reportrange").on("DOMSubtreeModified", function () {
                id = $('#ordDate').text();
                id = id.split('-');
                fdt = id[2].trim() + '-' + id[1] + '-' + id[0];
                tdt = id[5] + '-' + id[4] + '-' + id[3].trim();
                loadData();
            });
           
        });
        $("#close").click(function () {
            $("myModal").hide();
        });
    </script>

    <script type="text/javascript">
        $(function () {

            var start = moment();
            var end = moment();

            function cb(start, end) {
                $('#reportrange span').html(start.format('DD-MM-YYYY') + ' - ' + end.format('DD-MM-YYYY'));
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

    <%--  <script>
        // Get the modal
        var modal = document.getElementById("myModal");

        // Get the image and insert it inside the modal - use its "alt" text as a caption
        var img = document.getElementById("myImg");
        var modalImg = document.getElementById("img01");
        var captionText = document.getElementById("caption");
        img.onclick = function () {
            modal.style.display = "block";
            modalImg.src = this.src;
            captionText.innerHTML = this.alt;
        }

        // Get the <span> element that closes the modal
        var span = document.getElementsByClassName("close")[0];

        // When the user clicks on <span> (x), close the modal
        span.onclick = function () {
            modal.style.display = "none";
        }
    </script>--%>
</asp:Content>

