<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="PromotionMail.aspx.cs" Inherits="MasterFiles_PromotionMail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!DOCTYPE html>
    <html>
    <head>
        <meta charset="utf-8" />
        <meta http-equiv="X-UA-Compatible" content="IE=edge" />
        <title>AdminLTE 3 | Mailbox</title>
        <meta name="viewport" content="width=device-width, initial-scale=1.0" />
        <link rel="stylesheet" href="../css/Mail/css/all.min.css" />
        <link rel="stylesheet" href="../css/Mail/font-awesome.css" />
        <%--<link rel="stylesheet" href="https://code.ionicframework.com/ionicons/2.0.1/css/ionicons.min.css">--%>
        <link rel="stylesheet" href="../css/Mail/icheck-bootstrap.min.css" />
        <link href="https://fonts.googleapis.com/css?family=Source+Sans+Pro:300,400,400i,700" rel="stylesheet" />
        <style type="text/css">
            .box-header > .box-tools {
                position: absolute;
                right: 10px;
                top: 5px;
            }

            .box-header.with-border {
                border-bottom: 1px solid #f4f4f4;
            }

            .box .nav-stacked > li {
                border-bottom: 1px solid #f4f4f4;
                margin: 0;
            }

            .nav-stacked > li.active > a, .nav-stacked > li.active > a:hover {
                background: transparent;
                color: #444;
                border-top: 0;
                border-left-color: #3c8dbc;
            }

            .margin-bottom {
                margin-bottom: 20px;
            }


            .text-yellow {
                color: #f39c12 !important;
            }
        </style>
    </head>
    <body class="hold-transition sidebar-mini">
        <div class="wrapper">
            <div class="content-wrapper">
                <section class="content">
                    <div class="row">
                        <div class="col-md-3">
                            <a href="Compose_Mail.aspx" class="btn btn-primary btn-block margin-bottom">Compose</a>

                            <div class="box box-solid">
                                <div class="box-header with-border">
                                    <h3 class="box-title">Folders</h3>

                                    <div class="box-tools">
                                        <button type="button" style="background: transparent;" class="btn btn-box-tool" data-widget="collapse">
                                            <i class="fa fa-minus"></i>
                                        </button>
                                    </div>
                                </div>
                                <div class="box-body no-padding" style="">
                                    <ul class="nav nav-pills nav-stacked" style="margin: 0;">
                                        <li><a href="MailBox.aspx"><i class="fa fa-inbox"></i>Inbox
                  <%--<span class="label label-primary pull-right">12</span></a></li>--%>
                                            <li><a href="SentMail.aspx"><i class="fa fa-envelope-o"></i>Sent</a></li>
                                            <%--<li><a href="#"><i class="fa fa-file-text-o"></i>Drafts</a></li>
                                        <li><a href="#"><i class="fa fa-filter"></i>Junk <span class="label label-warning pull-right">65</span></a>
                                        </li>--%>
                                            <li><a href="StarViewMail.aspx"><i class='fa fa-star-o text-black'></i>Starred</a></li>
                                            <li><a href="DeleteMail.aspx"><i class="fa fa-trash-o"></i>Trash</a></li>
                                    </ul>
                                </div>
                                <!-- /.box-body -->
                            </div>
                            <!-- /.card -->

                            <div class="box box-solid">
                                <div class="box-header with-border">
                                    <h3 class="box-title">Labels</h3>

                                    <div class="box-tools">
                                        <button type="button" class="btn btn-box-tool" data-widget="collapse">
                                            <i class="fa fa-minus"></i>
                                        </button>
                                    </div>
                                </div>
                                <!-- /.box-header -->
                                <div class="box-body no-padding">
                                    <ul class="nav nav-pills nav-stacked">
                                        <li><a href="ViewImportant.aspx"><i class="fa fa-circle-o text-red"></i>Important</a></li>
                                        <li><a href="PromotionMail.aspx"><i class="fa fa-circle-o text-yellow"></i>Promotions</a></li>
                                        <li><a href="SocialMail.aspx"><i class="fa fa-circle-o text-light-blue"></i>Social</a></li>
                                    </ul>
                                </div>
                                <!-- /.box-body -->
                            </div>

                        </div>
                        <!-- /.col -->
                        <div class="col-md-9">
                            <div class="box box-primary">
                                <div class="box-header with-border">
                                    <%--<h3 class="box-title"><i class="fa fa-circle-o text-yellow"></i> Promotion</h3>--%>
                                    <table>
                                        <tr>
                                            <%--<td>
                                                <h3 class="box-title" id="h3inbox"><a href="MailBox.aspx">
                                                    <img src="../FileImage/primary.png" />Inbox </a></h3>
                                            </td>
                                            <td>
                                                <h3 class="box-title" id="h3Social"><a href="SocialMail.aspx">
                                                    <img src="../FileImage/social.png" />
                                                    Social</a></h3>
                                            </td>--%>
                                            <td>
                                                <h3 class="box-title" id="h3Promotions"><a href="PromotionMail.aspx">
                                                    <img src="../FileImage/promotion-png.png" />
                                                    Promotions</a></h3>
                                            </td>
                                        </tr>

                                    </table>

                                    <div class="box-tools pull-right">
                                        <div class="has-feedback">
                                            <input type="text" class="form-control input-sm" id="tSearchOrd" placeholder="Search Mail">
                                            <%-- <span class="glyphicon glyphicon-search form-control-feedback"></span>--%>
                                        </div>
                                    </div>
                                    <!-- /.box-tools -->
                                </div>
                                <!-- /.box-header -->
                                <div class="box-body no-padding">
                                    <div class="mailbox-controls">
                                        <!-- Check all button -->
                                        <button type="button" class="btn btn-default btn-sm checkbox-toggle">
                                            <i class="fa fa-square-o"></i>
                                        </button>
                                        <div class="btn-group">
                                            <button type="button" id="delete" class="btn btn-default btn-sm"><i class="fa fa-trash-o"></i></button>
                                            <%--<img src="../FileImage/drive_file_move_black_20dp.png" />--%>
                                            <select style="box-shadow: none; border: 0px; outline: 0px;" id="Promolbl">
                                                <option>Select Labels</option>
                                                <option>Inbox</option>
                                                <option>Important</option>
                                                <option>Social</option>
                                            </select>

                                            <%--<button type="button" class="btn btn-default btn-sm"><i class="fa fa-reply"></i></button>
                                            <button type="button" class="btn btn-default btn-sm"><i class="fa fa-share"></i></button>--%>
                                        </div>
                                        <!-- /.btn-group -->
                                        <%-- <button type="button" class="btn btn-default btn-sm"><i class="fa fa-refresh"></i></button>--%>
                                        <%-- <div class="pull-right" id="orders_info" role="status" aria-live="polite">--%>
                                        <%-- 1-50/200--%>
                                        <%-- Showing 0 to 0 of 0 entries--%>
                                        <%-- <div class="dataTables_info"  role="status" aria-live="polite">Showing 0 to 0 of 0 entries</div>--%>
                                        <%-- <div  id="example2_paginate">
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
                        </div>--%>

                                        <!-- /.btn-group -->
                                    </div>
                                    <!-- /.pull-right -->
                                </div>
                                <div class="table-responsive mailbox-messages" style="padding-left: 1px;padding-top: 5px;">
                                    <table class="table table-hover table-striped" id="Read_table">
                                        <tbody>
                                            <%-- <tr>
                                                    <input type="hidden" id="hide" />
                                                </tr>   --%>
                                        </tbody>
                                    </table>
                                    <!-- /.table -->
                                </div>

                                <div class="mailbox-controls">
                                    <!-- Check all button -->
                                    <button type="button" class="btn btn-default btn-sm checkbox-toggle">
                                        <i class="fa fa-square-o"></i>
                                    </button>
                                    <div class="btn-group">
                                        <button type="button" id="delete1" class="btn btn-default btn-sm"><i class="fa fa-trash-o"></i></button>
                                        <%--<button type="button" class="btn btn-default btn-sm"><i class="fa fa-reply"></i></button>
                                            <button type="button" class="btn btn-default btn-sm"><i class="fa fa-share"></i></button>--%>
                                    </div>
                                    <!-- /.btn-group -->
                                    <%--<button type="button" class="btn btn-default btn-sm"><i class="fa fa-refresh"></i></button>--%>
                                    <%--  <div class="pull-right" id="orders_info1" role="status" aria-live="polite">
  
                                            Showing 0 to 0 of 0 entries
                                            <div class="dataTables_info" id="orders_info1" role="status" aria-live="polite">Showing 0 to 0 of 0 entries</div>


                                        <div class="btn-group">
                                            <button type="button" class="btn btn-default btn-sm"><i class="fa fa-chevron-left"></i></button>
                                            <button type="button" class="btn btn-default btn-sm"><i class="fa fa-chevron-right"></i></button>
                                        </div>
                                        <!-- /.btn-group -->
                                    </div>--%>
                                    <!-- /.pull-right -->
                                </div>
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
                                <!-- /.mail-box-messages -->
                            </div>
                            <!-- /.box-body -->
                            <div class="box-footer no-padding">
                            </div>
                        </div>
                        <!-- /. box -->
                    </div>
                    <!-- /.row -->
                </section>
            </div>
        </div>
    <script src="<%=Page.ResolveUrl("~/js/jquery.min.js")%>" type="text/javascript"></script>
        <script type="text/javascript">
            var AllOrders = [];
            var sf;
            var sf1;
            var fdt = '';
            var tdt = '';
            //var filtrkey = 'All';
            var sortid = '';
            var fa = '';
            var asc = true;
            var star;
            var Orders = []; pgNo = 1; PgRecords = 10; TotalPg = 0; searchKeys = "Trans_Sl_No,Mail_SF_From,To_SFName,Mail_Subject,MonthDateYear,";

            $(function () {
                //Enable check and uncheck all functionality
                $('.checkbox-toggle').click(function () {
                    var clicks = $(this).data('clicks')
                    if (clicks) {
                        //Uncheck all checkboxes
                        $('.mailbox-messages input[type=\'checkbox\']').prop('checked', false)
                        $('.checkbox-toggle .far.fa-check-square').removeClass('fa-check-square').addClass('fa-square')
                    } else {
                        //Check all checkboxes
                        $('.mailbox-messages input[type=\'checkbox\']').prop('checked', true)
                        $('.checkbox-toggle .far.fa-square').removeClass('fa-square').addClass('fa-check-square')
                    }
                    $(this).data('clicks', !clicks)
                })

                //Handle starring for glyphicon and font awesome              

                // $(document).on('click', '.mailbox-star', function (e) {



            });

            function ReloadTable() {

                $("#Read_table").html("");
                st = PgRecords * (pgNo - 1);
				$("#Read_table").append("<tr><td></td><td><b>Team Name</b></td><td><b>Subject</b></td><td><b>Date</b></td></tr>");
                for ($i = st; $i < st + PgRecords; $i++) {
                    if ($i < Orders.length) {
                        if (Orders[$i].MailRead == 0)
                        {
                            $("#Read_table").append("<tr><td><input type='checkbox' class='case'></td><input type='hidden' id='hide' class='hiden' value=" + Orders[$i].Trans_Sl_No + " /><td><b><a class='MailReadid' href='Read_Mail.aspx?Trans_Sl_No=" + Orders[$i].Trans_Sl_No + "'>" + Orders[$i].Sf_Name + "</b></a></td><td><b>" + Orders[$i].Mail_Subject + "</b></td><td><b>" + Orders[$i].MonthDateYear + "</b></td>" + "</tr>");
                        }
                        else if (Orders[$i].MailRead == 1)
                        {
                            $("#Read_table").append("<tr><td><input type='checkbox' class='case'></td><input type='hidden' id='hide' class='hiden' value=" + Orders[$i].Trans_Sl_No + " /><td><a href='Read_Mail.aspx?Trans_Sl_No=" + Orders[$i].Trans_Sl_No + "'>" + Orders[$i].Sf_Name + "</a></td><td>" + Orders[$i].Mail_Subject + "</td><td>" + Orders[$i].MonthDateYear + "</td>" + "</tr>");
                        }
                                               
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
                    Orders = AllOrders;
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


            $(document).ready(function () {
                <%-- $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    url: "PromotionMail.aspx/GetPromotionData",
                    data: "{'divcode':'<%=Session["Division_Code"]%>','sfcode':'<%=Session["sf_code"]%>'}",
                        dataType: "json",
                        success: function (data) {
                            Orders = JSON.parse(data.d) || [];
                            AllOrders = Orders;
                            ReloadTable();
                            // loadPgNos();
                        },
                        error: function (result) {
                            alert("Error");
                        }
                    });--%>

                loadData();


                    $(document).on('click', '#delete', function (e) {

                        if ($(".case:checked").length > 0) {

                            $(".case:checked").each(function () {
                                var row = $(this).closest('tr');
                                var inv_no = row.find('#hide').val();
                                $('.case:checkbox:checked').parents("tr").remove();

                                $.ajax({
                                    type: "POST",
                                    contentType: "application/json; charset=utf-8",
                                    async: false,
                                    url: "MailBox.aspx/DeleteData",
                                    data: "{'divcode':'<%=Session["Division_Code"]%>','trans':'" + inv_no + "'}",
                                dataType: "json",
                                success: function (data) {
                                },
                                error: function (exception) {
                                    console.log(exception);
                                }
                            });
                        });
                        alert('successfully Delete');
                    }
                    else {
                        alert('Please Select Inbox to Delete');
                        }

                        loadData();


                });


                    $(document).on('click', '#delete1', function (e) {

                        if ($(".case:checked").length > 0) {

                            $(".case:checked").each(function () {
                                var row = $(this).closest('tr');
                                var inv_no = row.find('#hide').val();
                                $('.case:checkbox:checked').parents("tr").remove();

                                $.ajax({
                                    type: "POST",
                                    contentType: "application/json; charset=utf-8",
                                    async: false,
                                    url: "MailBox.aspx/DeleteData",
                                    data: "{'divcode':'<%=Session["Division_Code"]%>','trans':'" + inv_no + "'}",
                                dataType: "json",
                                success: function (data) {
                                },
                                error: function (exception) {
                                    console.log(exception);
                                }
                            });
                        });
                        alert('successfully Delete');
                    }
                    else {
                        alert('Please Select Inbox to Delete');
                        }

                        loadData();

                });

                    $(document).on('change', '#Promolbl', function (e) {

                        if ($(".case:checked").length > 0) {

                            $(".case:checked").each(function () {
                                var row = $(this).closest('tr');
                                var inv_no = row.find('#hide').val();
                                var sellabel = $("#Promolbl").val();
                                if (sellabel == "Important") {
                                    selectlbl = 1;
                                    var social = 0;
                                    var promo = 0;
                                    //$('.case:checkbox:checked').parents("tr").remove();                                                       
                                    data = { "ImportantMessage": selectlbl, "SocialMessage": social, "Promotions": promo, "Trans_Sl_No": inv_no, "DivCode": "<%=Session["div_code"]%>" }
                                $.ajax({
                                    type: "POST",
                                    contentType: "application/json; charset=utf-8",
                                    async: false,
                                    url: "MailBox.aspx/UpdateImpMsgData",
                                    //data: "{'data':'" + JSON.stringify(data) + "','Itype':'" + Itype + "'}",
                                    data: "{'data':'" + JSON.stringify(data) + "'}",
                                    dataType: "json",
                                    success: function (data) {
                                        var successtext = data.d;
                                        if (successtext == "ImpMsg inserted") {
                                            alert('ImpMsg inserted');
                                            //clearfields();
                                            //window.location.href = 'SalesForce_List.aspx';
                                        }
                                        else {
                                            //alert('StarMsg Inserted');
                                        }
                                    },
                                    error: function (rs) {
                                        alert(rs);
                                    }




                                });
                            }
                            else if (sellabel == "Inbox") {
                                selectlbl = 0;
                                var important = 0;
                                var promo1 = 0;
                                $('.case:checkbox:checked').parents("tr").remove();
                                data = { "ImportantMessage": important, "SocialMessage": selectlbl, "Promotions": promo1, "Trans_Sl_No": inv_no, "DivCode": "<%=Session["div_code"]%>" }
                            $.ajax({
                                type: "POST",
                                contentType: "application/json; charset=utf-8",
                                async: false,
                                url: "MailBox.aspx/UpdateImpMsgData",
                                //data: "{'data':'" + JSON.stringify(data) + "','Itype':'" + Itype + "'}",
                                data: "{'data':'" + JSON.stringify(data) + "'}",
                                dataType: "json",
                                success: function (data) {
                                    var successtext = data.d;
                                    if (successtext == "ImpMsg inserted") {
                                        alert('ImpMsg inserted');
                                        //clearfields();
                                        //window.location.href = 'SalesForce_List.aspx';
                                    }
                                    else {
                                        //alert('StarMsg Inserted');
                                    }
                                },
                                error: function (rs) {
                                    alert(rs);
                                }
                            });
                        }
                        else {
                            selectlbl = 1;
                            var important1 = 0;
                            var promo = 0;
                            $('.case:checkbox:checked').parents("tr").remove();
                            data = { "ImportantMessage": important1, "SocialMessage": selectlbl, "Promotions": promo, "Trans_Sl_No": inv_no, "DivCode": "<%=Session["div_code"]%>" }
                            $.ajax({
                                type: "POST",
                                contentType: "application/json; charset=utf-8",
                                async: false,
                                url: "MailBox.aspx/UpdateImpMsgData",
                                //data: "{'data':'" + JSON.stringify(data) + "','Itype':'" + Itype + "'}",
                                data: "{'data':'" + JSON.stringify(data) + "'}",
                                dataType: "json",
                                success: function (data) {
                                    var successtext = data.d;
                                    if (successtext == "ImpMsg inserted") {
                                        alert('ImpMsg inserted');
                                        //clearfields();
                                        //window.location.href = 'SalesForce_List.aspx';
                                    }
                                    else {
                                        //alert('StarMsg Inserted');
                                    }
                                },
                                error: function (rs) {
                                    alert(rs);
                                }
                            });
                        }

                        });
                        //alert('successfully Delete');
            }
            else {
                        //alert('Please Select Inbox to Delete');
                        }
                        loadData();

                    });


            });

            $(document).on('click', '.MailReadid', function (e) {
                    var MailReadflag = 1;
                    var row = $(this).closest('tr');
                    var inv_no = row.find('#hide').val();                    

                    data = { "MailRead": MailReadflag, "Trans_Sl_No": inv_no, "DivCode": "<%=Session["div_code"]%>" }
                    $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    url: "MailBox.aspx/UpdateMailRead",
                    data: "{'data':'" + JSON.stringify(data) + "'}",
                    dataType: "json",
                    success: function (data) {                        
                        var successtext = data.d;
                        if (successtext == "ImpMsg inserted") {
                            alert('ImpMsg inserted');                            
                        }
                        else {
                            //alert('StarMsg Inserted');
                        }
                    },
                    error: function (result) {
                        alert("Error");
                    }
                });                              
            });

            function loadData() {

                 $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    url: "PromotionMail.aspx/GetPromotionData",
                    data: "{'divcode':'<%=Session["Division_Code"]%>','sfcode':'<%=Session["sf_code"]%>'}",
                        dataType: "json",
                        success: function (data) {
                            Orders = JSON.parse(data.d) || [];
                            AllOrders = Orders;
                            ReloadTable();
                            // loadPgNos();
                        },
                        error: function (result) {
                            alert("Error");
                        }
                    });
            }


        </script>
    </body>
    </html>
</asp:Content>

