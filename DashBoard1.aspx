<%@ Page Title="" Language="C#" MasterPageFile="~/Master_MGR.master" AutoEventWireup="true"
    CodeFile="DashBoard1.aspx.cs" Inherits="DashBoard1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <script type="text/javascript" src="https://cdn.fusioncharts.com/fusioncharts/latest/fusioncharts.js"></script>
        <script type="text/javascript" src="https://cdn.fusioncharts.com/fusioncharts/latest/themes/fusioncharts.theme.fusion.js"></script>
        <link rel="stylesheet" href="//code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css">
        <link rel="stylesheet" href="css/dashboard.css">
        <script src="js/canvasjs.min.js" type="text/javascript"></script>
        <script src="../JsFiles/amcharts.js" type="text/javascript"></script>
        <script src="../JsFiles/serial.js" type="text/javascript"></script>
        <script src="https://www.amcharts.com/lib/3/pie.js"></script>
        <script src="//www.amcharts.com/lib/3/themes/light.js"></script>
        <script type="text/javascript" src="js/highmaps.js"></script>
        <%-- <script type="text/javascript" src="js/in-all.js"></script>--%>
        <script src="js/ph-all.js" type="text/javascript"></script>
        <script src="js/mm-all.js" type="text/javascript"></script>
        <script src="js/gh-all.js" type="text/javascript"></script>
        <script src="js/sl-all.js" type="text/javascript"></script>
        <script src="js/ng-all.js" type="text/javascript"></script>
        <style type="text/css">
            canvas, .ChartTop {
                border-radius: 6px;
            }

            .noti {
                border: solid 1px #000000;
            }

            .notification_div {
                border-radius: 6px;
                height: 450px;
                border: solid 1px #cccccc;
                padding-left: 50px;
                margin-left: 50px;
            }

            .ChartTop {
                height: 180px;
                border: solid 1px #cccccc;
                width: 100%;
            }

            .Chartdown {
                border-radius: 6px;
                height: 150px;
                border: solid 1px #cccccc;
            }

            .ddl {
                border: 1px solid #1E90FF;
                border-radius: 4px;
                margin: 2px;
                font-family: Andalus;
                background-image: url('css/download%20(2).png');
                background-position: 88px;
                background-position: 88px;
                background-repeat: no-repeat;
                text-indent: 0.01px; /*In Firefox*/
            }

            .huge {
                font-size: 20px;
            }

            .panel-green {
                border-color: #5cb85c;
            }

                .panel-green > .panel-heading {
                    border-color: #5cb85c;
                    color: white;
                    background-color: #5cb85c;
                }

                .panel-green > a {
                    color: #5cb85c;
                }

                    .panel-green > a:hover {
                        color: #3d8b3d;
                    }

            .list-group-item {
                position: relative;
                padding: 10px 15px;
                margin-bottom: -1px;
                background-color: #fff;
                border: 1px solid #ddd;
            }


            #chartdiv1 {
                width: 100%;
                height: 500px;
            }

            }
        </style>
        <script language="javascript" type="text/javascript">
            function popUp() {
                var sf_code = '<%= Session["sf_code"] %>';

                strOpen = "MIS Reports/rpt_Total_Order_View.aspx?sf_code=" + sf_code + "&div_code=" + div_code + "&cur_month=" + FMonth + "&cur_year=" + FYear +
                    "&Mode=" + "TPMYDayPlan" + "&Sf_Name=" + sf_code + "&Date=" + todate
                window.open(strOpen, 'popWindow', 'toolbar=0,scrollbars=1,location=0,statusbar=1,menubar=0,resizable=1,width=400,height=600,left = 0,top = 0');
            }
        </script>
        <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
        <script language="javascript" type="text/javascript">
            $(document).ready(function () {
                var sfCode = '<%= Session["sf_code"] %>';
                var sfname = '<%= Session["sf_name"] %>';
                var sfType = '<%= Session["sf_type"] %>';
                var DivCode = "";
                var retailcnt = $('#<%=retailer.ClientID%>').text();
                if (sfType == "3") {
                    DivCode = '<%= Session["division_code"] %>';
                }
                else {
                    DivCode = '<%= Session["division_code"] %>';
                }

                var TDate = new Date();
                var TMonth = TDate.getMonth() + 1;
                var TDay = TDate.getDate();
                var TYear = TDate.getFullYear();

                var CDate = TYear + '-' + TMonth + '-' + TDay;
		 $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "DashBoard1.aspx/Get_access_master",
                dataType: "json",
                success: function (data) {
                    window.localStorage.removeItem("Access_Details");
                    Access_data = JSON.parse(data.d) || [];
                    localStorage.setItem('Access_Details', data.d);
                },
                error: function (result) {
                }
            });
                $("#datepicker").datepicker({ dateFormat: "dd-mm-yy", maxDate: new Date() });
                var viewState = $('#<%=hdndate.ClientID %>').val();
                if (viewState != "") {
                    $("#datepicker").val(viewState);
                    var st = viewState.split('-');
                    TMonth = st[1];
                    TDay = st[0];
                    TYear = st[2];

                    CDate = TYear + '-' + TMonth + '-' + TDay;


                }
                else {
                    $("#datepicker").datepicker("setDate", new Date());
                    viewState = TDay + '-' + TMonth + '-' + TYear;;
                }
                $('#manp tbody').html('<tr>Loading...</tr>');
                $.when(getyearorder(), getdayord(), getmonthord()).then(function () {
                    Fillmanp();
                });
                $(document).on('change', "#datepicker", function (e) {
                    $('#manp tbody').html('<tr>Loading...</tr>');
                    var txt = $('#datepicker').val();
                    $('#<%=hdndate.ClientID %>').val(txt);
                    __doPostBack(txt, e);
                    ShowProgress();
                    $.when(getyearorder(), getdayord(), getmonthord()).then(function () {
                        Fillmanp();
                    });
                });


                $(document).on('click', '#view_New_Outlets', function () {
                    var sURL = "MIS Reports/mgr_rptNew_Outlet_List.aspx?SFCode=" + sfCode + "&Sf_Name=" + sfCode + "&Dates=" + CDate + "&FYear=&FMonth=&subdiv=0";
                    window.open(sURL, 'newRetailers', 'resizable=yes,toolbar=no,scrollbars=yes,menubar=no,status=no,width=800,height=600,left=0,top=0');
                });
                $(document).on('click', '#btnroute', function () {
                    var sURL = "MIS Reports/mgr_routeDetailsSFwise.aspx?SFCode=" + sfCode + "&SFName=" + sfname;
                    window.open(sURL, 'nDistributor', 'resizable=yes,toolbar=no,scrollbars=yes,menubar=no,status=no,width=800,height=600,left=0,top=0');
                });
                $(document).on('click', '#btnDistributor', function () {
                    var sURL = "MasterFiles/mgr_DistributorDashbord.aspx?SFCode=" + sfCode;
                    window.open(sURL, 'nDistributor', 'resizable=yes,toolbar=no,scrollbars=yes,menubar=no,status=no,width=800,height=600,left=0,top=0');
                });
                $(document).on('click', '#Btn_order', function () {
                    var sURL = "MIS Reports/mgr_rpt_Total_Order_View.aspx?sf_code=" + sfCode + "&div_code=" + DivCode + "&cur_month=" + TMonth + "&cur_year=" + TYear +
                        "&Mode=" + "TPMYDayPlan" + "&Sf_Name=" + sfname + "&Date=" + CDate + "&Sub_Div=0&Type=" + sfType;
                    window.open(sURL, 'nDistributor', 'resizable=yes,toolbar=no,scrollbars=yes,menubar=no,status=no,width=800,height=600,left=0,top=0');
                });
                $(document).on('click', '#btnRetailers', function () {
                    var sURL = "MIS Reports/mgr_RetailersDetailsSFwise.aspx?SFCode=" + sfCode + "&SFName=" + sfname + "&cnt=" + retailcnt;
                    window.open(sURL, 'nRetailers', 'resizable=yes,toolbar=no,scrollbars=yes,menubar=no,status=no,width=800,height=600,left=0,top=0');
                });


                $('#view_btn').on('click', function () {
                    var sURL = "MIS Reports/Designationwise_wtype.aspx?sf_code=" + sfCode + "&cur_month=" + TMonth + "&cur_year=" + TYear +
                        "&Mode=" + "TP MY Day Plan" + "&Sf_Name=" + sfCode + "&Date=" + CDate + "&Type=" + sfType + "&Designation_code=" + "" + "&Sub_Div=" + "0";
                    window.open(sURL, null, 'resizable=yes,toolbar=no,scrollbars=yes,menubar=no,status=no,width=800,height=600,left=0,top=0');
                });
                $('#view_dash').on('click', function () {
                    var sURL = "MGRDashboard.aspx?sf_code=" + sfCode + "&Sf_Name=" + sfname + "&Date=" + CDate;
                    window.open(sURL, null, 'resizable=yes,toolbar=no,scrollbars=yes,menubar=no,status=no,width=800,height=600,left=0,top=0');
                });
                $('#view_btn_Outlets').on('click', function () {
                    var sURL = "MIS Reports/rpt_Visit_OutLets_View.aspx?sf_code=" + sfCode + "&cur_month=" + TMonth + "&cur_year=" + TYear +
                        "&Mode=" + "TPMYDayPlan" + "&Sf_Name=" + sfCode + "&Date=" + CDate + "&Type=" + sfType + "&subdiv=" + "0";
                    window.open(sURL, null, 'resizable=yes,toolbar=no,scrollbars=yes,menubar=no,status=no,width=800,height=600,left=0,top=0');
                });
				
				$(document).on('click', '#view_fw_dtl', function () {
                    var sURL = "MIS Reports/view_countofsf.aspx?SFCode=" + sfCode + "&Dates=" + CDate + "&wtype=F&subdiv=<%=sub_divc%>";
                    window.open(sURL, 'newRetailers', 'resizable=yes,toolbar=no,scrollbars=yes,menubar=no,status=no,width=800,height=600,left=0,top=0');
                });
                $(document).on('click', '#view_lv_dtl', function () {
                    var sURL = "MIS Reports/view_countofsf.aspx?SFCode=" + sfCode + "&Dates=" + CDate + "&wtype=L&subdiv=<%=sub_divc%>";
                    window.open(sURL, 'newRetailers', 'resizable=yes,toolbar=no,scrollbars=yes,menubar=no,status=no,width=800,height=600,left=0,top=0');
                });
                $(document).on('click', '#view_ot_dtl', function () {
                    var sURL = "MIS Reports/view_countofsf.aspx?SFCode=" + sfCode + "&Dates=" + CDate + "&wtype=N&subdiv=<%=sub_divc%>";
                    window.open(sURL, 'newRetailers', 'resizable=yes,toolbar=no,scrollbars=yes,menubar=no,status=no,width=800,height=600,left=0,top=0');
                });
                $(document).on('click', '#view_nlg_dtl', function () {
                    var sURL = "MIS Reports/view_countofsf.aspx?SFCode=" + sfCode + "&Dates=" + CDate + "&wtype=NL&subdiv=<%=sub_divc%>";
                    window.open(sURL, 'newRetailers', 'resizable=yes,toolbar=no,scrollbars=yes,menubar=no,status=no,width=800,height=600,left=0,top=0');
                });
               <%-- $(document).on('click', '#view_vsal_dtl', function () {
                    var sURL = "MIS Reports/view_countofsf.aspx?SFCode=" + sfCode + "&Dates=" + CDate + "&wtype=NL&subdiv=<%=sub_divc%>";
                    window.open(sURL, 'newRetailers', 'resizable=yes,toolbar=no,scrollbars=yes,menubar=no,status=no,width=800,height=600,left=0,top=0');
                });--%>

                function Fillmanp() {
                    $.ajax({
                        type: "Post",
                        contentType: "application/json; charset=utf-8",
                        url: "DashBoard1.aspx/getmanp",
                        async: true,
                        dataType: "json",
                        success: function (data) {
                            salesnl = JSON.parse(data.d);
                            $('#manp').show();
                            $('#manp tbody').html('');
                            var str = '';
                            var daytot = 0; var mnthtot = 0; var yeartot = 0;
                            var day = ''; var month = ''; var year = '';
                            for (var i = 0; i < salesnl.length; i++) {
                                day = dayor.filter(function (a) {
                                    return a.sf_Designation_Short_Name == salesnl[i].sf_Designation_Short_Name
                                });
                                month = monor.filter(function (a) {
                                    return a.sf_Designation_Short_Name == salesnl[i].sf_Designation_Short_Name
                                });
                                year = yearor.filter(function (a) {
                                    return a.sf_Designation_Short_Name == salesnl[i].sf_Designation_Short_Name
                                });
                                str += '<tr><td>' + (i + 1) + '</td><td>' + salesnl[i].sf_Designation_Short_Name + '</td><td>' + salesnl[i].Active + '</td><td>' + salesnl[i].Vacant + '</td><td>' + (day.length > 0 ? day[0].VAL : '') + '</td><td>' + (month.length > 0 ? month[0].VAL : '') + '</td><td>' + (year.length > 0 ? year[0].VAL : '') + '</td></tr>';
                                daytot += (day.length > 0 ? day[0].VAL : 0);
                                mnthtot += (month.length > 0 ? month[0].VAL : 0);
                                yeartot += (year.length > 0 ? year[0].VAL : 0);
                            }

                            $('#manp tbody').append(str);
                            $('#manp tfoot').html("<tr><td colspan=4 style='text-align: center;font-weight: bold'>Total  Value</td><td>" + daytot.toFixed(2) + "</td><td>" + mnthtot.toFixed(2) + "</td><td>" + yeartot.toFixed(2) + "</td></tr>");


                        }
                    })
                }
                function getdayord() {
                    return $.ajax({
                        type: "Post",
                        contentType: "application/json; charset=utf-8",
                        url: "DashBoard1.aspx/getdayord",
                        async: true,
                        data: "{'date':'" + CDate + "'}",
                        dataType: "json",
                        success: function (data) {
                            dayor = JSON.parse(data.d);
                        }
                    })
                }

                function getmonthord() {
                    return $.ajax({
                        type: "Post",
                        contentType: "application/json; charset=utf-8",
                        url: "DashBoard1.aspx/getmonthord",
                        async: true,
                        data: "{'date':'" + CDate + "'}",
                        dataType: "json",
                        success: function (data) {
                            monor = JSON.parse(data.d);
                        }
                    })
                }
                function getyearorder() {
                    return $.ajax({
                        type: "Post",
                        contentType: "application/json; charset=utf-8",
                        url: "DashBoard1.aspx/getyearorder",
                        async: true,
                        data: "{'date':'" + CDate + "'}",
                        dataType: "json",
                        success: function (data) {
                            yearor = JSON.parse(data.d);
                        }
                    })
                }
            });
            function ShowProgress() {
                setTimeout(function () {
                    var modal = $('<div />');
                    modal.addClass("modal");
                    $('body').append(modal);
                    var loading = $(".loading");
                    loading.show();
                    var top = Math.max($(window).height() / 2 - loading[0].offsetHeight / 2, 0);
                    var left = Math.max($(window).width() / 2 - loading[0].offsetWidth / 2, 0);
                    loading.css({ top: top, left: left });
                }, 200);
            }
        </script>
        <asp:HiddenField ID="hdndate" runat="server"></asp:HiddenField>
        <div class="row1" style="position: absolute; top: 64px; right: 20px;">

            <input type="text" id="datepicker" class="form-control" style="min-width: 110px !important; width: 110px; display: inline-block; border: none; background-color: transparent; color: #336277; font-family: Verdana; font-weight: bold; font-size: 14px; color: red;" />
            <label class="caret" for="datepicker" style="position: relative; top: 7px;"></label>
        </div>
        <div class="col-lg-8" style="padding: 0px;">

            <div class="col-md-3">
                <div class="sm-st clearfix">
                    <div id="btnRetailers" class="info-box bg-pink hover-expand-effect">
                        <span class="sm-st-icon st-red"><i class="fa fa-user"></i></span>
                        <div class="sm-st-info">
                            <span>
                                <asp:Label ID="retailer" runat="server" Text="Label"></asp:Label>
                            </span>Retailer
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-md-3">
                <div class="sm-st clearfix">
                    <div id="btnDistributor" class="info-box bg-purble hover-expand-effect">
                        <span class="sm-st-icon st-violet"><i class="fa fa-user"></i></span>
                        <div class="sm-st-info">
                            <span>
                                <asp:Label ID="Dist_cou" runat="server"></asp:Label>
                            </span>Distributor
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-md-3">
                <div class="sm-st clearfix">
                    <div id="btnroute" class="info-box bg-purble hover-expand-effect">
                        <span class="sm-st-icon st-violet"><i class="fa fa-user"></i></span>
                        <div class="sm-st-info">
                            <span>
                                <asp:Label ID="rout_cou" runat="server"></asp:Label>
                            </span>Route
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-md-3">
                <div class="sm-st clearfix">
                    <div id="Btn_order" class="info-box bg-purble hover-expand-effect">
                        <span class="sm-st-icon st-violet"><i class="fa fa-user"></i></span>
                        <div class="sm-st-info">
                            <span>
                                <asp:Label ID="ordercount" runat="server"></asp:Label>
                            </span>
                            <asp:Label ID="Order_val"
                                runat="server" Font-Size="Small"></asp:Label>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-lg-12" style="padding: 0px;">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        Manpower
                    </div>
                    <table id="manp" style="display: none;">
                        <thead>
                            <tr>
                                <th>S.NO</th>
                                <th>Designation</th>
                                <th>Active</th>
                                <th>Vacant</th>
                                <th>Day Value</th>
                                <th>Monthly Value</th>
                                <th>Yearly Value</th>
                            </tr>
                        </thead>
                        <tbody></tbody>
                        <tfoot></tfoot>
                    </table>
                    <a href="#" id="view_dash">
                        <div class="panel-footer">
                            <span class="pull-left">View More Details</span> <span class="pull-right"><i class="fa fa-arrow-circle-right"></i></span>
                            <div class="clearfix">
                            </div>
                        </div>
                    </a>
                </div>
            </div>

            <div class="col-lg-12" style="padding: 0px;">
                <div id="exTab3" class="container">
                    <ul class="nav nav-pills">
                        <li class="active"><a href="#1b" data-toggle="tab">Orders</a> </li>
                        <li><a href="#2b" data-toggle="tab">Sales</a> </li>
                    </ul>
                </div>
                <div class="tab-content clearfix">
                    <div class="tab-pane active" id="1b">
                        <div class="col-lg-12" style="padding: 0px;">
                            <div class="panel panel-default">
                                <div class="panel-heading">
                                    <i class="fa fa-bar-chart-o fa-fw"></i>Order - 2018 (In Values)
                                </div>
                                <!-- /.panel-heading -->
                                <div class="panel-body">
                                    <div class="row" style="padding: 0px;">
                                        <div class="col-md-6" style="padding: 0px 3px 7px 14px;">
                                            <div id="T5Cate" class="ChartTop">
                                            </div>
                                        </div>
                                        <div class="col-md-6" style="padding: 0px 13px 7px 3px;">
                                            <div id="B5Cate" class="ChartTop">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-6" style="padding: 0px 3px 7px 14px;">
                                            <div id="T5brand" class="ChartTop">
                                            </div>
                                        </div>
                                        <div class="col-md-6" style="padding: 0px 13px 7px 3px;">
                                            <div id="B5brand" class="ChartTop">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-6" style="padding: 0px 3px 7px 14px;">
                                            <div id="T5Pro" class="ChartTop">
                                            </div>
                                        </div>
                                        <div class="col-md-6" style="padding: 0px 13px 7px 3px;">
                                            <div id="B5Pro" class="ChartTop">
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <!-- /.panel-body -->
                            </div>
                        </div>
                    </div>
                    <div class="tab-pane" id="2b">
                        <div class="col-lg-12" style="padding: 0px;">
                            <div class="panel panel-default">
                                <div class="panel-heading">
                                    <i class="fa fa-bar-chart-o fa-fw"></i>Sales - 2018 (In Values)
                                </div>
                                <!-- /.panel-heading -->
                                <div class="panel-body">
                                    <!-- /.col-lg-4 (nested) -->
                                    <div class="row" style="padding: 0px;">
                                        <div class="col-md-6" style="padding: 0px 3px 7px 14px;">
                                            <div id="ST5Cate" class="ChartTop">
                                            </div>
                                        </div>
                                        <div class="col-md-6" style="padding: 0px 13px 7px 3px;">
                                            <div id="SB5Cate" class="ChartTop">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-6" style="padding: 0px 3px 7px 14px;">
                                            <div id="ST5brand" class="ChartTop">
                                            </div>
                                        </div>
                                        <div class="col-md-6" style="padding: 0px 13px 7px 3px;">
                                            <div id="SB5brand" class="ChartTop">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-6" style="padding: 0px 3px 7px 14px;">
                                            <div id="ST5Pro" class="ChartTop">
                                            </div>
                                        </div>
                                        <div class="col-md-6" style="padding: 0px 13px 7px 3px;">
                                            <div id="SB5Pro" class="ChartTop">
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <!-- /.col-lg-8 (nested) -->
                                <!-- /.row -->
                                <!-- /.panel-body -->
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <asp:Panel ID="Panel1" runat="server">
                <div class="col-lg-4 col-md-8">
                    <div class="panel panel-primary">
                        <div class="panel-heading">
                            <div class="row">
                                <div class="col-xs-3">
                                    <i class="fa fa-user fa-5x"></i>
                                    <div class="huge">
                                        <asp:Label ID="Lbl_Tot_User" runat="server" Text="0"></asp:Label>
                                    </div>
                                    <div>
                                        Total Users!
                                    </div>
                                </div>
                                <div class="col-xs-3 text-right">
                                    <div class="huge">
									<a href="#" id="view_fw_dtl" style="color: #fff">
                                        <asp:Label ID="Lbl_Reg_User" runat="server" Text="0"></asp:Label></a>
                                    </div>
                                    <div style="width: 67px;">
                                        In-Market!
                                    </div>
                                    <div class="huge" style="margin-top: 40px;">
									<a href="#" id="view_lv_dtl" style="color: #fff">
                                        <asp:Label ID="Lbl_Lea" runat="server" Text="0"></asp:Label></a>
                                    </div>
                                    <div>
                                        Leave!
                                    </div>
                                    <div class="huge" style="margin-top: 40px;">
									<a href="#" id="view_vsal_dtl" style="color: #fff">
                                        <asp:Label ID="Lbl_vsal" runat="server" Text="0"></asp:Label></a>
                                    </div>
                                    <div>
                                        Vansales!
                                    </div>
                                </div>
                                <div class="col-xs-6 text-right">
                                    <div class="huge">
									<a href="#" id="view_ot_dtl" style="color: #fff">
                                        <asp:Label ID="Lbl_Oth" runat="server" Text="0"></asp:Label></a>
                                    </div>
                                    <div>
                                        Other Works!
                                    </div>
                                    <div class="huge" style="margin-top: 40px; color: #ff9898;">
									<a href="#" id="view_nlg_dtl" style="color: #fff">
                                        <asp:Label ID="Lbl_Inact_User" runat="server" Text="0"></asp:Label></a>
                                    </div>
                                    <div>
                                        Inactive Users!
                                    </div>
                                </div>
                            </div>
                        </div>
                        <a href="#" id="view_btn">
                            <div class="panel-footer">
                                <span class="pull-left">View Details</span> <span class="pull-right"><i class="fa fa-arrow-circle-right"></i></span>
                                <div class="clearfix">
                                </div>
                            </div>
                        </a>
                    </div>
                    <div>
                        <div class="panel panel-green">
                            <div class="panel-heading">
                                <div class="row">
                                    <div class="col-xs-3">
                                        <i class="fa fa-tasks fa-5x"></i>
                                        <div class="huge">
                                            <asp:Label ID="Lbl_Outlets" runat="server" Text="0"></asp:Label>
                                        </div>
                                        <div>
                                            Visited Outlets
                                        </div>
                                    </div>
                                    <div class="col-xs-3 text-right">
                                        <div class="huge">
                                            <a href="#" id="view_New_Outlets" style="color: #fff">
                                                <asp:Label ID="Lbl_Sch_Call" runat="server" Text="0"></asp:Label>
                                            </a>
                                        </div>
                                        <div style="width: 67px;">
                                            NewRetailers
                                        </div>
                                        <div class="huge" style="margin-top: 40px;">
                                            <asp:Label ID="Lbl_Prod" runat="server" Text="0"></asp:Label>
                                        </div>
                                        <div>
                                            Productive!
                                        </div>
                                    </div>
                                    <div class="col-xs-6 text-right">
                                        <div class="huge">
                                            <asp:Label ID="Lbl_Prod_Outlet" runat="server" Text="0%"></asp:Label>
                                        </div>
                                        <div>
                                            Productivity!
                                        </div>
                                        <div class="huge" style="margin-top: 40px;">
                                            <asp:Label ID="Lbl_Vist_Outlet" runat="server" Text="0"></asp:Label>
                                        </div>
                                        <div>
                                            Un-Productive!
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <a href="#" id="view_btn_Outlets">
                                <div class="panel-footer">
                                    <span class="pull-left">View Details</span> <span class="pull-right"><i class="fa fa-arrow-circle-right"></i></span>
                                    <div class="clearfix">
                                    </div>
                                </div>
                            </a>
                        </div>
                        <div class="panel panel-default">
                            <div class="panel-heading">
                                <i class="fa fa-bell fa-fw"></i>Notifications Panel
                            </div>
                            <!-- /.panel-heading -->
                            <div class="panel-body">
                                <div class="list-group">
                                    <asp:DataList ID="DataList1" runat="server" RepeatColumns="1" class="list-group-item"
                                        OnItemDataBound="Item_Bound">
                                        <ItemTemplate>
                                            <i class="fa fa-comment fa-fw"></i>
                                            <asp:Label ID="lblInput" runat="server" Width="255px" Style="padding-top: 10px; padding-left: 5px;"
                                                Text='<%# Eval("comment") %>'></asp:Label>
                                            <span class="pull-right text-muted small"><em>
                                                <asp:Label
                                                    ID="daytime" runat="server" Text='<%# Eval("timee") %>' Style="font-style: bold; font-size: 10px; color: #a8b0b3;"></asp:Label>
                                                <i class="fa fa-clock-o" style="color: #a6aeb1;"></i></em>
                                            </span>
                                            <asp:Label ID="cmttype" runat="server" Text='<%# Eval("Comment_Type") %>' Visible="false"></asp:Label>
                                        </ItemTemplate>
                                    </asp:DataList>
                                </div>
                                <!-- /.list-group -->
                                <a href="../../../../MIS Reports/Notification_inpu.aspx" class="btn btn-default btn-block">Set Alerts</a>
                            </div>
                            <!-- /.panel-body -->
                        </div>
                        <div class="panel panel-default">
                            <div class="panel-heading">
                                <i class="fa fa-bar-chart-o fa-fw"></i>In-Time Statistics
                            </div>
                            <div class="panel-body">
                                <div id="chartContainer" style="height: 200px; width: 100%;">
                                </div>
                                <a href="#" class="btn btn-default btn-block">View Details</a>
                            </div>
                            <!-- /.panel-body -->
                        </div>
                    </div>
                </div>
            </asp:Panel>
        </div>
        <script type="text/javascript">
            function gen1(Obj, value, title) {
                AmCharts.addInitHandler(function (chart) {
                    for (var i = 0; i < chart.graphs.length; i++) {
                        var graph = chart.graphs[i];
                        if (graph.autoColor !== true)
                            continue;
                        var colorKey = "autoColor-" + i;
                        graph.lineColorField = colorKey;
                        graph.fillColorsField = colorKey;
                        for (var x = 0; x < chart.dataProvider.length; x++) {
                            var color = chart.colors[x]
                            chart.dataProvider[x][colorKey] = color;
                        }
                    }

                }, ["serial"]);

                var chart = AmCharts.makeChart(Obj, {
                    "type": "serial",
                    "theme": "light",
                    "type": "serial",
                    "startDuration": 2,
                    "fontSize": 9,
                    "dataProvider": value,
                    "graphs": [{
                        "balloonText": "[[category]]: <b>[[value]]</b>",
                        "fillColorsField": "color",
                        "fillAlphas": 1,
                        "lineAlpha": 0.1,
                        "type": "column",
                        "valueField": "y",
                        "autoColor": true
                    }],
                    "depth3D": 20,
                    "angle": 30,
                    "chartCursor": {
                        "categoryBalloonEnabled": false,
                        "cursorAlpha": 0,
                        "zoomable": false
                    },
                    "categoryField": "label",
                    "categoryAxis": {
                        "gridPosition": "start",
                        "inside": true,
                        "labelRotation": 90
                    },
                    "export": {
                        "enabled": false
                    }

                });

            }
            function gen2(Obj, value, title) {
                var chart = new CanvasJS.Chart(Obj, {
                    exportEnabled: false,
                    animationEnabled: true,

                    legend: {
                        cursor: "pointer",
                        itemclick: explodePie
                    },
                    data: [{
                        type: "pie",
                        showInLegend: false,
                        toolTipContent: "{label}: <strong>{y}</strong>",
                        indexLabel: "{label} - {y}",
                        exploded: true,
                        dataPoints: value


                    }]
                });
                chart.render();
            }

            function explodePie(e) {
                if (typeof (e.dataSeries.dataPoints[e.dataPointIndex].exploded) === "undefined" || !e.dataSeries.dataPoints[e.dataPointIndex].exploded) {
                    e.dataSeries.dataPoints[e.dataPointIndex].exploded = false;
                } else {
                    e.dataSeries.dataPoints[e.dataPointIndex].exploded = false;
                }
                e.chart.render();

            }

            function genChart1(Obj, arrDta, arr1, title) {
            //            var yourSessionValue = '<%= Session["sf_code"] %>';
                //            alert(yourSessionValue);
                //var date = new Date(); alert((date.getMonth() + 1) + '/' + date.getDate() + '/' + date.getFullYear());

                var chart = AmCharts.makeChart(Obj, {
                    "theme": "light",
                    "type": "serial",
                    "startDuration": 0,
                    "dataProvider": arrDta,
                    "valueAxes": [{
                        "axisAlpha": 0,
                        "position": "left"
                    }],
                    "graphs": [{
                        "id": "g1",
                        "balloonText": "[[label]]<br><b><span style='font-size:14px;'>[[y]]</span></b>",
                        "bullet": "round",
                        "bulletSize": 8,
                        "lineColor": "#428bca",
                        "lineThickness": 2,
                        "negativeLineColor": "#637bb6",
                        "type": "smoothedLine",
                        "valueField": "y"
                    }, {
                        "id": "g2",
                        "balloonText": "[[label]]<br><b><span style='font-size:14px;'>[[s]]</span></b>",
                        "bullet": "round",
                        "bulletSize": 8,
                        "lineColor": "#5cb85c",
                        "lineThickness": 2,
                        "negativeLineColor": "#d1655d",
                        "type": "smoothedLine",
                        "valueField": "s"
                    }],
                    "dataDateFormat": "mmm-YY",
                    "categoryField": "label",
                    "categoryAxis": {
                        "gridPosition": "start",
                        "labelRotation": 0
                    },
                    "export": {
                        "enabled": false
                    }

                });

            }

        </script>
    </form>
</asp:Content>