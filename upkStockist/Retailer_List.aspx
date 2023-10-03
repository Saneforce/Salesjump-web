<%@ Page Title="" Language="C#" MasterPageFile="~/Master_DIS.master" AutoEventWireup="true" CodeFile="Retailer_List.aspx.cs" Inherits="Stockist_Retailer_List" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <form id="frm1" runat="server">
        <%--<div class="row">            
                <asp:ImageButton ID="ImageButton1" runat="server" align="right" ImageUrl="~/img/Excel-icon.png"
                        style="height:40px;width:40px;border-width:0px;position: absolute;right: 15px;top: 55px;" OnClick="ExportToExcel" />
        </div>--%>
        <div class="row">
            <div class="col-lg-12 sub-header">Customers<span style="float: right;"><a href="#" class="btn btn-primary btn-update" id="newsf">Add New</a></span></div>
        </div>
        <div class="row" style="margin-top: 10px">
            <div class="col-xs-6" style="min-width: 338px !important; max-width: 340px !important">
                <select id="ddlrt" class="ddrt" name="route"></select>
            </div>
            <button class="btn btn-primary go" type="button" style="display: none;">Go</button>
        </div>
        <div class="card">
            <div class="card-body table-responsive">
                <div style="white-space: nowrap">
                    Search&nbsp;&nbsp;<input type="text" autocomplete="off" id="tSearchOrd" style="width: 250px;" />
                    <span id="filspan" style="margin-left: 10px;">Filter By&nbsp;&nbsp;<select class="form-control" id="txtfilter" name="ddfilter" style="min-width: 315px !important; max-width: 318px !important; display: inline"></select></span>
                    <label style="float: right">
                        Shows
                        <select class="data-table-basic_length" aria-controls="data-table-basic">
                            <option value="10">10</option>
                            <option value="25">25</option>
                            <option value="50">50</option>
                            <option value="100">100</option>
                        </select>
                        entries</label>
                    <div style="float: right">
                        <ul class="segment">
                            <li data-va='All'>ALL</li>
                            <li data-va='0' class="active">Active</li>
                        </ul>
                    </div>
                </div>
                <table class="table table-hover" id="OrderList">
                    <thead class="text-warning">
                        <tr>
                            <th style="text-align: left">Sl NO.</th>
                            <th style="text-align: left">Customer Code</th>
                            <th style="text-align: left">Customer Name</th>
                            <th style="text-align: left">Mobile No.</th>
                            <th style="text-align: left">Route</th>
                            <th style="text-align: left" class="sfedit">Edit</th>
                            <th style="text-align: left">Status</th>
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
                            <ul class="pagination">
                                <li class="paginate_button previous " id="example2_previous"><a href="#" aria-controls="example2" data-dt-idx="0" tabindex="0">Previous</a></li>
                                <li class="paginate_button active"><a href="#" aria-controls="example2" data-dt-idx="1" tabindex="0">1</a></li>
                                <li class="paginate_button next" id="example2_next"><a href="#" aria-controls="example2" data-dt-idx="2" tabindex="0">Next</a></li>
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
        var sf;
        var divcode; filtrkey = '0';
        optStatus = "<li><a href='#' v='0'>Active</a><a href='#' v='1'>Deactivate</a></li>"
        var Orders = []; pgNo = 1; PgRecords = 10; TotalPg = 0; searchKeys = "ListedDrCode,ListedDr_Name,Mobile,Sf_Code,territory_Name,Status,";
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

        function fillRoute(sf) {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "Retailer_List.aspx/getRoute",
                data: "{'divcode':'<%=Session["div_code"]%>','sf':'" + sf + "'}",
                dataType: "json",
                success: function (data) {
                    var sf = $("[id*=ddlrt]");
                    //sf.empty().append('<option selected="selected" value="0">Select Route</option>');
                    for (var i = 0; i < data.d.length; i++) {
                        sf.append($('<option value="' + data.d[i].rtCode + '">' + data.d[i].rtName + '</option>')).trigger('chosen:updated').css("width", "100%");;;
                    };
                }
            });
            $('#ddlrt').chosen();
        }


        function fillfilter(sf) {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "Retailer_List.aspx/getMGR",
                data: "{'divcode':'<%=Session["div_code"]%>','sfcode':'" + sf + "'}",
                dataType: "json",
                success: function (data) {
                    var sf = $("[id*=txtfilter]");
                    sf.empty().append('<option selected="selected" value="0">Select Filter</option>');
                    for (var i = 0; i < data.d.length; i++) {
                        sf.append($('<option value="' + data.d[i].sfcode + '">' + data.d[i].sfname + '</option>'));
                    };
                }
            });
        }

        $('#ddlrt').on('change', function () {
		    pgNo = 1;
            var st = $(this).val();
            if ($('#txtfilter :selected').val() == 0) {
                Orders = AllOrders;
                Orders = Orders.filter(function (a) {
                    return a.Territory_Code == st;
                });
                ReloadTable();
            }
			else if(st==0)
            {
                Orders = AllOrders;                
                ReloadTable();
            }
            else if (st != 0) {
                Orders = AllOrders;
                Orders = Orders.filter(function (a) {
                    return a.Territory_Code == st;
                });
                ReloadTable();
            }
            else if ($(this).val() == 0 && $('#txtfilter :selected').val() == 0) {
                Orders = AllOrders;
                ReloadTable();
            }
            else if ($(this).val() == 0 && $('#txtfilter :selected').val() != 0) {
                Orders = AllOrders;
                Orders = Orders.filter(function (a) {
                    return a.Sf_Code == $('#txtfilter :selected').val();
                });
                ReloadTable();
            }
            else if ($('#txtfilter :selected').val() =='<%=Session["Sf_Code"]%>' && st == 0) {
                Orders = AllOrders;
                ReloadTable();
            }
            else if ($('#txtfilter :selected').val() =='<%=Session["Sf_Code"]%>' && st != 0) {
                Orders = AllOrders;
                Orders = Orders.filter(function (a) {
                    return a.Territory_Code == st;
                });
                ReloadTable();
            }
            else {
                Orders = AllOrders;
                Orders = Orders.filter(function (a) {
                    return a.Territory_Code == st && a.Sf_Code == $('#txtfilter :selected').val();
                });
                ReloadTable();
            }
        });

        $('#txtfilter').on('change', function () {
            var sfs = $(this).val();
            if ($('#ddlrt :selected').val() == 0 && sfs !='<%=Session["Sf_Code"]%>') {
                Orders = AllOrders;
                Orders = Orders.filter(function (a) {
                    return a.Sf_Code == sfs;
                });
                ReloadTable();
            }
            else if ($(this).val() == 0 && $('#ddlrt :selected').val() == 0) {
                Orders = AllOrders;
                ReloadTable();
            }
            else if ($(this).val() == 0 && $('#ddlrt :selected').val() != 0) {
                Orders = AllOrders;
                Orders = Orders.filter(function (a) {
                    return a.Territory_Code == $('#ddlrt :selected').val();
                });
                ReloadTable();
            }
            else if (sfs =='<%=Session["Sf_Code"]%>' && $('#ddlrt :selected').val() == 0) {
                Orders = AllOrders;
                ReloadTable();
            }
            else if (sfs =='<%=Session["Sf_Code"]%>' && $('#ddlrt :selected').val() != 0) {
                Orders = AllOrders;
                Orders = Orders.filter(function (a) {
                    return a.Territory_Code == $('#ddlrt :selected').val();
                });
                ReloadTable();
            }
            else {
                Orders = AllOrders;
                Orders = Orders.filter(function (a) {
                    return a.Territory_Code == $('#ddlrt :selected').val() && a.Sf_Code == sfs;
                });
                ReloadTable();
            }
        });

        function ReloadTable() {
            $("#OrderList TBODY").html("");
            if (filtrkey != "All") {
                Orders = Orders.filter(function (a) {
                    return a.ListedDr_Active_Flag == filtrkey;
                })
            }
            st = PgRecords * (pgNo - 1); slno = 0;
            for ($i = st; $i < st + PgRecords; $i++) {
                //var filtered = Orders.filter(function (x) {
                //    return x.Sf_Code != 'admin';
                //})
                if ($i < Orders.length) {
                    tr = $("<tr></tr>");
                    //var hq = filtered[$i].Sf_Name.split('-');
                    slno = $i + 1;
                    $(tr).html('<td>' + slno + '</td><td>' + Orders[$i].ListedDrCode + '</td><td>' + Orders[$i].ListedDr_Name + '</td><td>' + Orders[$i].Mobile +
                        '</td><td>' + Orders[$i].territory_Name + '</td><td id=' + Orders[$i].ListedDrCode +
                        ' class="sfedit"><a href="#">Edit</a></td>' +
                        '<td><ul class="nav" style="margin:0px"><li class="dropdown"><a href="#" style="padding:0px" class="dropdown-toggle" data-toggle="dropdown">'
                        + '<span><span class="aState" data-val="' + Orders[$i].Status + '">' + Orders[$i].Status + '</span><i class="caret" style="float:right;margin-top:8px;margin-right:0px"></i></span></a>' +
                        '<ul class="dropdown-menu dropdown-custom dropdown-menu-right ddlStatus" style="right:0;left:auto;">' + optStatus + '</ul></li></ul></td>');

                    $("#OrderList TBODY").append(tr);
                  <%--  if(<%=Session["sf_type"]%>==4){
                        $('.sfedit').css('display','none');
                    }--%>
                    hq = [];
                }
            }
            $("#orders_info").html("Showing " + (st + 1) + " to " + (((st + PgRecords) < Orders.length) ? (st + PgRecords) : Orders.length) + " of " + Orders.length + " entries")

            $(".ddlStatus>li>a").on("click", function () {
             //   if (<%=Session["sf_type"]%>== 3) {

                cStus = $(this).closest("td").find(".aState");
                stus = $(this).attr("v");
                $indx = $(this).closest("tr").index();
                cStusNm = $(this).text();
                if ($(cStus).text() == "Approval Pending" || $(cStus).text() == "Reject") {
                    alert("You are not Authorized While in " + $(cStus).text());

                }
                else {
                    if (confirm("Do you want change status " + $(cStus).text() + " to " + cStusNm + " ?")) {
                        sf = Orders[$indx].ListedDrCode;
                        $.ajax({
                            type: "POST",
                            contentType: "application/json; charset=utf-8",
                            async: false,
                            url: "Retailer_List.aspx/SetNewStatus",
                            data: "{'SF':'" + sf + "','stus':'" + stus + "'}",
                            dataType: "json",
                            success: function (data) {
                                Orders[$indx].ListedDr_Active_Flag = stus;
                                Orders[$indx].Status = cStusNm;
                                $(cStus).html(cStusNm);
                                ReloadTable();
                                alert('Status Changed Successfully...');
                            },
                            error: function (result) {
                            }
                        });
                    }

                }
            });
            loadPgNos();
        }
        $(".segment>li").on("click", function () {
            $(".segment>li").removeClass('active');
            $(this).addClass('active');
            filtrkey = $(this).attr('data-va');
            Orders = AllOrders;
			var seg=$('#ddlrt :selected').val();
            if (filtrkey == "All" &&  seg!="0") 
            {
                Orders = Orders.filter(function (a) {
                    return (a.Territory_Code==seg );
                })
            }
			else if(filtrkey=="0"&&  seg!="0")
            {
                Orders = Orders.filter(function (a) {                   
                    return a.Territory_Code==seg && a.ListedDr_Active_Flag == filtrkey;
                })
            }
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
				 pgNo = 1;
            ReloadTable();
        })

        function loadData(sf, rt) {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "Retailer_List.aspx/getRetailers",
                data: "{'divcode':'<%=Session["div_code"]%>','sfcode':'" + sf + "','rtcode':'" + rt + "'}",
                dataType: "json",
                success: function (data) {
                    AllOrders = JSON.parse(data.d) || [];
                    Orders = AllOrders;
                    ReloadTable();
                },
                error: function (result) {
                }
            });
        }

        $(document).ready(function () {
            var rt = 0;
            if ('<%=Session["sf_type"]%>' == '4') {
                $('#filspan').hide();
                sf ='<%=Session["Sf_Code"]%>';
            }
            else {
                sf ='<%=Session["sf_code_MR"]%>';
            }
            loadData(sf, rt); fillRoute(sf);
            if (<%=Session["sf_type"]%>!= 4) {
                fillfilter(sf);
            }
            $(document).on('click', '#newsf', function () {
                 window.location.href="../Stockist/RetailerAdd.aspx";
               // window.location.href="../Stockist/MR/ListedDoctor/RetailerAdd.aspx";
              //  window.location.href = "../Stockist/ListedDR_DetailAdd.aspx";
            });

            $(document).on('click', '.sfedit', function () {
                x = this.id;
                //window.location.href = "../MasterFiles/MR/ListedDoctor/ListedDr_DetailAdd.aspx?type=2&ListedDrCode=" + x;
               // window.location.href = "../Stockist/ListedDr_DetailAdd.aspx?type=2&ListedDrCode=" + x;
              //window.location.href = "../MasterFiles/MR/ListedDoctor/RetailerAdd.aspx?type=2&ListedDrCode=" + x;
                window.location.href="../Stockist/RetailerAdd.aspx?type=2&ListedDrCode=" + x;
            });

            //$('select[name="ddfilter"]').change(function () {
            //    var r = $(this).val();
            //    loadData(r);
            //});
        });

    </script>
</asp:Content>
